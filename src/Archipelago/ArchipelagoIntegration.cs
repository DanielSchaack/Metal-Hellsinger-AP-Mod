using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Models;
using Archipelago.MultiClient.Net.Packets;
using UnityEngine;
using static Randomizer.Locations;

namespace Randomizer
{

    public class ArchipelagoIntegration : MonoBehaviour
    {
        public enum DeathLinkType
        {
            Death,
            Off,
        }

        public bool connected
        {
            get { return session != null ? session.Socket.Connected : false; }
        }

        private const string DataStorageKeyNotLocalLocations = "NotRandomizedLocations";
        public ArchipelagoSession session;
        private IEnumerator incomingItemHandler;
        private IEnumerator outgoingItemHandler;
        private IEnumerator checkItemsReceived;
        private ConcurrentQueue<(ItemInfo ItemInfo, int index)> incomingItems;
        private ConcurrentQueue<Location> locationsToSend = new ConcurrentQueue<Location>();
        private DeathLinkService deathLinkService;
        private readonly float delay = 0.25f;
        public Dictionary<string, object> slotData;
        public bool sentCompletion = false;
        public int ItemIndex = 0;
        private Version archipelagoVersion = new Version("0.6.7");

        public void Update()
        {
            if (!connected)
            {
                return;
            }

            if (incomingItemHandler != null)
            {
                incomingItemHandler.MoveNext();
            }

            if (outgoingItemHandler != null)
            {
                outgoingItemHandler.MoveNext();
            }

            if (checkItemsReceived != null)
            {
                checkItemsReceived.MoveNext();
            }
        }

        public void OnDestroy()
        {
            TryDisconnect();
        }

        public string TryConnect()
        {
            if (
                connected
                && Randomizer.Configuration.archipelagoUsername.Value
                    == session.Players.GetPlayerName(session.ConnectionInfo.Slot)
                && Randomizer.Configuration.archipelagoUri.Value == session.Socket.Uri.AbsoluteUri
            )
            {
                return "";
            }

            TryDisconnect();

            LoginResult LoginResult;

            if (session == null)
            {
                try
                {
                    session = ArchipelagoSessionFactory.CreateSession(
                        Randomizer.Configuration.archipelagoUri.Value
                    );
                }
                catch (Exception e)
                {
                    Logger.LogError("Failed to create archipelago session!");
                    Logger.LogError(e.Message);
                }
            }

            incomingItemHandler = IncomingItemHandler();
            outgoingItemHandler = OutgoingItemHandler();
            checkItemsReceived = CheckItemsReceived();
            incomingItems = new ConcurrentQueue<(ItemInfo ItemInfo, int index)>();
            locationsToSend = new ConcurrentQueue<Location>();

            session.MessageLog.OnMessageReceived += ArchipelagoConsole.instance.LogApMessage;
            session.Locations.CheckedLocationsUpdated += Randomizer.LocationTracker.Resync;

            try
            {
                LoginResult = session.TryConnectAndLogin(
                    Randomizer.Game,
                    Randomizer.Configuration.archipelagoUsername.Value,
                    ItemsHandlingFlags.AllItems,
                    version: archipelagoVersion,
                    requestSlotData: true,
                    password: Randomizer.Configuration.archipelagoPassword.Value
                );
            }
            catch (Exception e)
            {
                LoginResult = new LoginFailure(e.GetBaseException().Message);
            }

            if (LoginResult is LoginSuccessful LoginSuccess)
            {
                Logger.LogInfo("Successfully connected to Archipelago Multiworld server!");

                Randomizer.Settings = new Settings(LoginSuccess.SlotData);

                deathLinkService = session.CreateDeathLinkService();
                deathLinkService.OnDeathLinkReceived += HandleDeathlink();
                CheckDeathlink();
                Resync();
            }
            else
            {
                LoginFailure loginFailure = (LoginFailure)LoginResult;
                ArchipelagoConsole.instance.LogMessage("Error connecting to Archipelago:");
                string errorList = string.Join("\n", loginFailure.Errors);
                string fullMessage =
                    $"Failed to connect to Archipelago!\nCheck your settings and/or log output.\n{errorList}";

                ArchipelagoConsole.instance.LogMessage(fullMessage);
                Logger.LogError(fullMessage);

                foreach (ConnectionRefusedError Error in loginFailure.ErrorCodes)
                {
                    Logger.LogError(Error.ToString());
                }
                TryDisconnect();
                return fullMessage;
            }
            return "";
        }

        private static DeathLinkService.DeathLinkReceivedHandler HandleDeathlink()
        {
            return (deathLinkObject) =>
            {
                Logger.LogInfo("Death link received.");
                // TODO: ?
                // PlayerCharacterPatches.DeathLinkMessage =
                //     deathLinkObject.Cause == null
                //         ? $"\"{deathLinkObject.Source} died and took you with them.\""
                //         : $"\"{deathLinkObject.Cause}\"";
                // PlayerCharacterPatches.DiedToDeathLink = true;
                Randomizer.IngameDispenser.QueueDeathLink();
            };
        }

        public void CheckDeathlink()
        {
            if (connected)
            {
                if (Randomizer.Configuration.archipelagoDeathlinkType.Value == DeathLinkType.Death)
                {
                    EnableDeathLink();
                }
                else
                {
                    DisableDeathLink();
                }
            }
        }

        public void TrySilentReconnect()
        {
            LoginResult LoginResult;
            try
            {
                LoginResult = session.TryConnectAndLogin(
                    Randomizer.Game,
                    Randomizer.Configuration.archipelagoUsername.Value,
                    ItemsHandlingFlags.AllItems,
                    version: archipelagoVersion,
                    requestSlotData: true,
                    password: Randomizer.Configuration.archipelagoPassword.Value
                );
            }
            catch (Exception e)
            {
                LoginResult = new LoginFailure(e.GetBaseException().Message);
            }
        }

        public void TryDisconnect()
        {
            try
            {
                if (connected)
                {
                    ArchipelagoConsole.instance.LogMessage("Disconnecting from Archipelago");
                }
                if (session != null)
                {
                    session.Socket.DisconnectAsync();
                    session = null;
                }

                incomingItemHandler = null;
                outgoingItemHandler = null;
                checkItemsReceived = null;
                incomingItems = new ConcurrentQueue<(ItemInfo ItemInfo, int ItemIndex)>();
                locationsToSend = new ConcurrentQueue<Location>();
                deathLinkService = null;
                slotData = null;
                ItemIndex = 0;
                Randomizer.LocationTracker.Reset();
                Randomizer.ItemTracker.Reset();
                Randomizer.IngameDispenser.Reset();

                ArchipelagoConsole.instance.LogMessage("Disconnected from Archipelago");
            }
            catch (Exception e)
            {
                Logger.LogError("Encountered an error disconnecting from Archipelago!");
                Logger.LogError(e.Message);
            }
        }

        public void Resync()
        {
            if (!connected)
                return;

            Logger.LogInfo(
                "Running Location resync with "
                    + Randomizer.LocationTracker.LocationsCollected.Count
                    + " locations."
            );
            Randomizer.LocationTracker.Resync(session.Locations.AllLocationsChecked);
            Randomizer.LocationTracker.Resync(
                session
                    .DataStorage[DataStorageKeyNotLocalLocations]
                    .To<long[]>()
                    .ToList()
                    .AsReadOnly()
            );

            Logger.LogInfo(
                "Running Item resync with " + session.Items.AllItemsReceived.Count + " items."
            );
            Randomizer.ItemTracker.Resync(session.Items.AllItemsReceived);
        }

        private IEnumerator IncomingItemHandler()
        {
            while (connected)
            {
                if (!incomingItems.TryPeek(out var pendingItem))
                {
                    yield return true;
                    continue;
                }

                var itemInfo = pendingItem.ItemInfo;
                var itemName = itemInfo.ItemDisplayName;
                var itemId = itemInfo.ItemId;

                if (Randomizer.ItemTracker.HasItemByIndex(pendingItem.index))
                {
                    incomingItems.TryDequeue(out _);
                    Logger.LogInfo(
                        "Skipping item "
                            + itemName
                            + " at index "
                            + pendingItem.index
                            + " as it has already been processed."
                    );
                    continue;
                }

                // TODO:
                // Delay after scene change
                // while (
                //     SaveFile.GetFloat("playtime")
                //     < SceneLoaderPatches.TimeOfLastSceneTransition + 3.0f
                // )
                // {
                //     yield return true;
                // }

                Randomizer.ItemTracker.SetCollectedItem(itemId, pendingItem.index, true, false);
                incomingItems.TryDequeue(out _);

                // Delay item processing
                DateTime postInteractionStart = DateTime.Now;
                while (DateTime.Now < postInteractionStart + TimeSpan.FromSeconds(delay))
                {
                    yield return true;
                }
            }
        }

        private IEnumerator OutgoingItemHandler()
        {
            while (connected)
            {
                if (!locationsToSend.TryPeek(out var pendingLocation))
                {
                    yield return true;
                    continue;
                }

                if (session.Locations.AllLocationsChecked.Contains(pendingLocation.ArchipelagoId))
                {
                    Logger.LogInfo("Skipping queued check: " + pendingLocation.LocationId);
                    locationsToSend.TryDequeue(out _);
                    yield return true;
                    continue;
                }

                Logger.LogInfo("Sending queued check: " + pendingLocation.LocationId);
                try
                {
                    session.Locations.CompleteLocationChecksAsync(pendingLocation.ArchipelagoId);
                    locationsToSend.TryDequeue(out _);
                }
                catch (System.Exception ex)
                {
                    Logger.LogError($"Failed to send location, ex:\n{ex.Message}");
                }
                yield return true;
            }
        }

        private IEnumerator CheckItemsReceived()
        {
            while (connected)
            {
                while (session.Items.AllItemsReceived.Count > ItemIndex)
                {
                    ItemInfo ItemInfo = session.Items.AllItemsReceived[ItemIndex];
                    Logger.LogInfo(
                        "Placing item "
                            + ItemInfo.ItemDisplayName
                            + " with index "
                            + ItemIndex
                            + " in queue."
                    );
                    incomingItems.Enqueue((ItemInfo, ItemIndex));
                    ItemIndex++;
                }
                yield return true;
            }
        }

        public void CompleteLocationCheck(Location location)
        {
            locationsToSend.Enqueue(location);
        }

        public void SendCompletion()
        {
            if(connected){
                Logger.LogInfo("Sending goal completion");
                session.SetGoalAchieved();
                sentCompletion = true;
            }
        }

        private void EnableDeathLink()
        {
            if (deathLinkService == null)
            {
                Logger.LogWarning("Cannot enable death link service as it is null.");
            }

            Logger.LogInfo("Enabled death link service");
            deathLinkService.EnableDeathLink();
        }

        private void DisableDeathLink()
        {
            if (deathLinkService == null)
            {
                Logger.LogWarning("Cannot disable death link service as it is null.");
            }

            Logger.LogInfo("Disabled death link service");
            deathLinkService.DisableDeathLink();
        }

        public void SendDeathLink(AttackID attackID)
        {
            string Player = Randomizer.Configuration.archipelagoUsername.Value;

            HashSet<string> MessageOptions = new HashSet<string>();

            // TODO:
            // string hitBy = PlayerCharacterPatches.lastHitTriggerHitBy;
            string hitBy = "Nothing";

            if (hitBy != "" && DeathLinkMessages.HitTriggerCauses.ContainsKey(hitBy))
            {
                MessageOptions.Add(DeathLinkMessages.HitTriggerCauses[hitBy]);
            }

            if (hitBy != "" && DeathLinkMessages.HitTriggerDescriptions.ContainsKey(hitBy))
            {
                foreach (string cause in DeathLinkMessages.GenericMessages)
                {
                    MessageOptions.Add($"{cause}{DeathLinkMessages.HitTriggerDescriptions[hitBy]}");
                }
            }

            // TODO:
            // if (DeathLinkMessages.Causes.ContainsKey(SceneLoaderPatches.SceneName))
            if (DeathLinkMessages.Causes.ContainsKey("CurrentScene"))
            {
                // foreach (string cause in DeathLinkMessages.Causes[SceneLoaderPatches.SceneName])
                // {
                //     MessageOptions.Add(cause);
                // }
            }

            if (MessageOptions.Count == 0)
            {
                foreach (string cause in DeathLinkMessages.Causes["Generic"])
                {
                    MessageOptions.Add(cause);
                }
            }


            if(connected)
                deathLinkService.SendDeathLink(
                    new DeathLink(
                        Player,
                        $"{Player}{MessageOptions.ToList()[new System.Random().Next(MessageOptions.Count)]}"
                    )
                );
        }

        public void SendArchipelagoMessage(string message)
        {
            session.Socket.SendPacketAsync(new SayPacket { Text = message });
        }

        public void ShowNotConnectedError()
        {
            ArchipelagoConsole.instance.LogMessage(
                "[archipelago] ERROR: Lost connection to Archipelago!"
            );
            ArchipelagoConsole.instance.LogMessage(
                "Unable to send or receive items. Re-connect and try again."
            );
        }

        public int GetPlayerSlot()
        {
            return session.ConnectionInfo.Slot;
        }

        public string GetPlayerName(int Slot)
        {
            return session.Players.GetPlayerName(Slot).Replace("{", "").Replace("}", "");
        }

        public string GetPlayerGame(int Slot)
        {
            return session.Players.Players[0][Slot].Game;
        }

        public bool IsHellsingerPlayer(int Slot)
        {
            return GetPlayerGame(Slot) == Randomizer.Game
                && session.Players.GetPlayerInfo(Slot).GetGroupMembers(session.Players) == null;
        }

        private void SetupDataStorage()
        {
            if (session != null)
            {
                Logger.LogInfo("Initializing DataStorage values");
                session
                    .DataStorage[Scope.Slot, DataStorageKeyNotLocalLocations]
                    .Initialize(new long[] { });
            }
        }

        public void SynchronizeNotRandomizedLocation(Location[] locationsCollected)
        {
            if (session != null)
            {
                var localLocations = locationsCollected.Select(loc => loc.ArchipelagoId).ToList();
                Logger.LogDebug($"Checking if location {localLocations} are to be added");
                var externalLocations = session
                    .DataStorage[Scope.Slot, DataStorageKeyNotLocalLocations]
                    .To<long[]>()
                    .ToList();
                Logger.LogDebug($"Retrieved locations {externalLocations}");

                List<long> missingLocally = externalLocations.Except(localLocations).ToList();
                long[] missingExternally = localLocations.Except(externalLocations).ToArray();

                if (missingExternally.Count() > 0)
                {
                    Logger.LogInfo($"Adding locations {missingExternally} to DataStorage");
                    session.DataStorage[Scope.Slot, DataStorageKeyNotLocalLocations] +=
                        missingExternally;
                }

                if (missingLocally.Count() > 0)
                {
                    Logger.LogInfo($"Adding locations {missingLocally} to local tracker");
                    Randomizer.LocationTracker.Resync(missingLocally.AsReadOnly());
                }
            }
        }

        internal long[] GetOpenLocations()
        {
            if (!connected)
                return new long[]{};
            return session.Locations.AllMissingLocations.ToArray();

        }
    }
}
