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
            DeathTrap,
            RandomTrap,
            Off,
        }

        public bool connected
        {
            get { return session != null ? session.Socket.Connected : false; }
        }

        private const string DataStorageKeyRandomizedLocations = "NotRandomizedLocations";
        private const string DataStorageKeyDefeatedBosses = "DefeatedBosses";
        private const string DataStorageKeyItemIndex = "ItemIndex";
        private string LastConnectionString = "";
        public ArchipelagoSession session;
        private IEnumerator incomingItemHandler;
        private IEnumerator outgoingItemHandler;
        private IEnumerator checkItemsReceived;
        private ConcurrentQueue<(ItemInfo ItemInfo, int index)> incomingItems;
        private ConcurrentQueue<Location> locationsToSend = new ConcurrentQueue<Location>();
        private DeathLinkService deathLinkService;
        private readonly float delay = 0.1f;
        public Dictionary<string, object> slotData;
        private List<string> SlainBosses = [];
        public bool sentCompletion = false;
        public int ItemIndex
        {
            get;
            set
            {
                Logger.LogInfo($"ItemIndex - Current: {field} | Incoming: {value}");
                if (field < value)
                {
                    SetItemIndex(value);
                    field = value;
                } else if (value == 0)
                    field = value;
            }
        } = 0;

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
                    Logger.LogError($"Failed to create archipelago session!\n{e.Message}");
                    ArchipelagoConsole.Instance.LogMessage($"Failed to create archipelago session! {e.Message}");
                }
            }

            incomingItemHandler = IncomingItemHandler();
            outgoingItemHandler = OutgoingItemHandler();
            checkItemsReceived = CheckItemsReceived();
            incomingItems = new ConcurrentQueue<(ItemInfo ItemInfo, int index)>();
            locationsToSend = new ConcurrentQueue<Location>();

            session.MessageLog.OnMessageReceived += ArchipelagoConsole.Instance.LogApMessage;
            session.Socket.ErrorReceived += Session_ErrorReceived;
            session.Socket.SocketClosed += Session_SocketClosed;

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
                string CurrentConnectionString = Randomizer.Configuration.archipelagoUri.Value + Randomizer.Configuration.archipelagoUsername.Value;
                Logger.LogInfo("Successfully connected to Archipelago Multiworld server!");

                Randomizer.Settings = new Settings(LoginSuccess.SlotData);

                deathLinkService = session.CreateDeathLinkService();
                deathLinkService.OnDeathLinkReceived += HandleDeathlink;

                CheckDeathlink();
                SetupDataStorage();

                if (LastConnectionString != CurrentConnectionString)
                {
                    LastConnectionString = CurrentConnectionString;
                    SaveDataManager.ResetState();
                    Randomizer.LocationTracker.Reset();
                    Randomizer.ItemTracker.Reset(Randomizer.Settings);
                    Randomizer.IngameDispenser.Reset();

                    ItemIndex = 0;
                    Items.ItemList.Clear();
                    session.Locations.ScoutLocationsAsync(session.Locations.AllLocations.ToArray()).ContinueWith(locationInfoPacket => {
                    foreach (ItemInfo ItemInfo in locationInfoPacket.Result.Values) {
                        Items.ItemList.Add(ItemInfo.LocationId, ItemInfo);
                        Logger.LogDebug($"Adding Item '{ItemInfo.ItemName}' to Location '{ItemInfo.LocationDisplayName}' with Location ID {ItemInfo.LocationId}");
                    }
                    }).Wait(TimeSpan.FromSeconds(10.0f));
                    Logger.LogInfo("Successfully scouted locations for item placements");
                }

                session.Locations.CheckedLocationsUpdated += Randomizer.LocationTracker.Resync;
                Resync();
            }
            else
            {
                LoginFailure loginFailure = (LoginFailure)LoginResult;
                ArchipelagoConsole.Instance.LogMessage("Error connecting to Archipelago:");
                string errorList = string.Join("\n", loginFailure.Errors);
                string fullMessage =
                    $"Failed to connect to Archipelago!\nCheck your settings and/or log output.\n{errorList}";

                ArchipelagoConsole.Instance.LogMessage(fullMessage);
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

        void Session_SocketClosed(string reason)
        {
            Logger.LogError("Connection to Archipelago lost: " + reason);
            ArchipelagoConsole.Instance.LogMessage($"<color=orange>Connection to Archipelago lost.</color>");
            TryDisconnect();
        }

         void Session_ErrorReceived(Exception e, string message)
        {
            ArchipelagoConsole.Instance.LogMessage($"<color=orange>Received an error from APSession.Socket. This means you may have lost connection to the AP server.</color>");
            Logger.LogError($"Received error from APSession.Socket: '{message}'\n");
            if (e != null) Logger.LogError(e.ToString());
            TryDisconnect();
        }

        private static void HandleDeathlink(DeathLink deathLinkObject)
        {
            ArchipelagoConsole.Instance.LogDeathlink(deathLinkObject);
            Randomizer.IngameDispenser.QueueDeathLink(deathLinkObject.Source);
        }

        public void CheckDeathlink()
        {
            if (connected)
            {
                if (Randomizer.Configuration.archipelagoDeathlinkType.Value != DeathLinkType.Off)
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
                    ArchipelagoConsole.Instance.LogMessage("Disconnecting from Archipelago");
                }

                if (deathLinkService != null)
                {
                    deathLinkService.OnDeathLinkReceived -= HandleDeathlink;
                    deathLinkService = null;
                }

                if (session != null)
                {
                    session.MessageLog.OnMessageReceived -= ArchipelagoConsole.Instance.LogApMessage;
                    session.Locations.CheckedLocationsUpdated -= Randomizer.LocationTracker.Resync;
                    session.Socket.ErrorReceived -= Session_ErrorReceived;
                    session.Socket.SocketClosed -= Session_SocketClosed;

                    _ = session.Socket.DisconnectAsync();
                    session = null;
                }

                incomingItemHandler = null;
                outgoingItemHandler = null;
                checkItemsReceived = null;
                incomingItems = new ConcurrentQueue<(ItemInfo ItemInfo, int ItemIndex)>();
                locationsToSend = new ConcurrentQueue<Location>();
                ItemIndex = 0;

                ArchipelagoConsole.Instance.LogMessage("Disconnected from Archipelago");
            }
            catch (Exception e)
            {
                Logger.LogError($"Encountered an error disconnecting from Archipelago!\n{e.Message}");
            }
        }

        public void Resync()
        {
            if (!connected)
                return;

            Logger.LogInfo(
                "Running Location resync with "
                    + Randomizer.LocationTracker.LocationsCollected.Count
                    + " locally checked locations."
            );
            Randomizer.LocationTracker.Resync(Randomizer.LocationTracker.LocationsCollected.Select(loc => loc.ArchipelagoId).ToList().AsReadOnly());

            Logger.LogInfo(
                "Running Location resync with "
                    + session.Locations.AllLocationsChecked.Count
                    + " officially checked locations."
            );
            Randomizer.LocationTracker.Resync(session.Locations.AllLocationsChecked);

            long[] ids = session.DataStorage[Scope.Slot, DataStorageKeyRandomizedLocations].To<long[]>();
            if(ids != null)
            {
                Logger.LogInfo(
                    "Running Location resync with "
                        + ids.Length + " hidden checked locations."
                );
                Randomizer.LocationTracker.Resync(
                    (ids ?? System.Array.Empty<long>()).ToList().AsReadOnly()
                );
            }

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
                var itemSender = itemInfo.Player.Alias;

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

                // Delay after scene change
                while (Randomizer.SceneActiveTime < 10.0f)
                    yield return true;

                Randomizer.ItemTracker.SetCollectedItem(
                    itemId,
                    pendingItem.index,
                    true,
                    itemSender
                );
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

        public void SendDeathLink(string levelId, AttackID attackID = AttackID.None)
        {
            if (Randomizer.TimeSinceLastDeathlink < 10f)
                return;
            Randomizer.TimeSinceLastDeathlink = 0f;

            string Player = Randomizer.Configuration.archipelagoUsername.Value;

            HashSet<string> MessageOptions = new HashSet<string>();
            foreach (string generic in DeathLinkMessages.Areas["Generic"])
                MessageOptions.Add(generic);
            if (Lookup.IsChallengeLevelId(levelId))
                foreach (string generic in DeathLinkMessages.Areas["Torment"])
                    MessageOptions.Add(generic);
            else
                foreach (string generic in DeathLinkMessages.Areas[levelId])
                    MessageOptions.Add(generic);


            // TODO: attack id messages
            // if(attackID != AttackID.None)
            // {
                // string hitBy = attackID.ToString();
                // if (hitBy != "" && DeathLinkMessages.HitTriggerDescriptions.ContainsKey(hitBy))
                // { }
            // }


            if(connected)
            {
                string cause = $"{Player}{MessageOptions.ToList()[new System.Random().Next(MessageOptions.Count)]}";
                ArchipelagoConsole.Instance.LogMessage($"Sending deathlink: {cause}");
                deathLinkService.SendDeathLink(
                    new DeathLink(
                        Player,
                        cause
                    )
                );
            }
        }

        public void SendArchipelagoMessage(string message)
        {
            session.Socket.SendPacketAsync(new SayPacket { Text = message });
        }

        public void ShowNotConnectedError()
        {
            ArchipelagoConsole.Instance.LogMessage(
                "[archipelago] ERROR: Lost connection to Archipelago!"
            );
            ArchipelagoConsole.Instance.LogMessage(
                "Unable to send or receive items. Re-connect and try again."
            );
        }

        private void SetupDataStorage()
        {
            if (session != null)
            {
                Logger.LogInfo("Initializing DataStorage values");
                session
                    .DataStorage[Scope.Slot, DataStorageKeyRandomizedLocations]
                    .Initialize(new long[] { });

                Logger.LogInfo(
                    $"DataStorage {DataStorageKeyRandomizedLocations} is at: {string.Join(", ", session
                    .DataStorage[Scope.Slot, DataStorageKeyRandomizedLocations]
                    .To<long[]>())}"
                );

                session
                    .DataStorage[Scope.Slot, DataStorageKeyDefeatedBosses]
                    .Initialize(new string[] { });

                SlainBosses = session
                    .DataStorage[Scope.Slot, DataStorageKeyDefeatedBosses]
                    .To<string[]>().ToList();

                Logger.LogInfo(
                    $"DataStorage {DataStorageKeyDefeatedBosses} is at: {string.Join(", ", SlainBosses)}"
                );

                session
                    .DataStorage[Scope.Slot, DataStorageKeyItemIndex]
                    .Initialize(0);

                ItemIndex = session.DataStorage[Scope.Slot, DataStorageKeyItemIndex].To<int>();
                Logger.LogInfo(
                    $"DataStorage {DataStorageKeyItemIndex} is at: {session.DataStorage[Scope.Slot, DataStorageKeyItemIndex]
                    .To<int>()}"
                );
            }
        }

        public void SynchronizeNotRandomizedLocation(Location[] locationsCollected)
        {
            if (session == null) return;

            var localLocations = locationsCollected.Select(loc => loc.ArchipelagoId).ToList();
            Logger.LogDebug($"Checking if locations {string.Join(", ", localLocations)} are to be added");

            session.DataStorage[Scope.Slot, DataStorageKeyRandomizedLocations].GetAsync<long[]>().ContinueWith(task =>
            {
                var externalLocations = (task.Result ?? Array.Empty<long>()).ToList();
                Logger.LogDebug($"Retrieved locations {string.Join(", ", externalLocations)}");

                List<long> missingLocally = externalLocations.Except(localLocations).ToList();
                long[] missingExternally = localLocations.Except(externalLocations).ToArray();

                if (missingExternally.Length > 0)
                {
                    Logger.LogInfo($"Adding locations {string.Join(", ", missingExternally)} to DataStorage");
                    session.DataStorage[Scope.Slot, DataStorageKeyRandomizedLocations] += missingExternally;
                }

                if (missingLocally.Count > 0)
                {
                    Logger.LogInfo($"Adding locations {string.Join(", ", missingLocally)} to local tracker");
                    Randomizer.LocationTracker.Resync(missingLocally.AsReadOnly());
                }
            });
        }

        internal long[] GetOpenLocations()
        {
            if (!connected)
                return new long[]{};
            return session.Locations.AllMissingLocations.ToArray();

        }

        internal void CheckGoalCompletion()
        {
            Logger.LogDebug($"Retrieved slain bosses:  {string.Join(", ", SlainBosses)}");

            int aspectSlain = 0;
            if (SlainBosses.Contains("The Lost Unknown: Leviathan defeated"))
                aspectSlain--;

            foreach (var bossSlain in Lookup.LevelToDefeatedBossLocationName.Values)
                if (SlainBosses.Contains(bossSlain))
                    aspectSlain++;

            Logger.LogDebug($"Has slain {aspectSlain} aspects");

            bool IsHellsRelevant =
                Randomizer.Settings.RequireHellsCompletion
                || Randomizer.Settings.RequireSheolCompletion;
            bool IsLeviathanRelevant = Randomizer.Settings.RequireLeviathanCompletion;
            bool IsAspectsDone = aspectSlain >= Randomizer.Settings.RequiredHellsCompletion;
            bool IsRedJudgeDefeated = SlainBosses.Contains(
                "Red Judge - Worldbreaker: Sheol defeated"
            );
            bool IsHellsDone =
                (!Randomizer.Settings.RequireHellsCompletion || IsAspectsDone)
                && (!Randomizer.Settings.RequireSheolCompletion || IsRedJudgeDefeated);

            bool IsLeviathanDone = SlainBosses.Contains("The Lost Unknown: Leviathan defeated");

            Logger.LogInfo(
                $"Completion evaluation: HellsDone: {IsHellsDone} (Relevant: {IsHellsRelevant}), LeviathanDone: {IsLeviathanDone} (Relevant: {IsLeviathanRelevant}), CompletionSent: {Randomizer.Archipelago.sentCompletion}"
            );

            if (
                !Randomizer.Archipelago.sentCompletion
                && (!IsHellsRelevant || IsHellsDone)
                && (!IsLeviathanRelevant || IsLeviathanDone)
            )
                Randomizer.Archipelago.SendCompletion();
        }

        internal void AddSlainBoss(string slainBoss)
        {
            Logger.LogInfo($"Adding slain Boss '{slainBoss}'");
            SlainBosses.Add(slainBoss);
            if(connected)
                session.DataStorage[Scope.Slot, DataStorageKeyDefeatedBosses] = SlainBosses.ToArray();
        }

        private void SetItemIndex(int itemIndex)
        {
            session.DataStorage[Scope.Slot, DataStorageKeyItemIndex] = itemIndex;
        }

    }
}
