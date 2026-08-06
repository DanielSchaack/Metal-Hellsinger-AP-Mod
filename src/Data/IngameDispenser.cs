using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using static Randomizer.ArchipelagoIntegration;
using static Randomizer.Lookup;

namespace Randomizer
{
    public class IngameDispenser : MonoBehaviour
    {
        private ConcurrentQueue<(ItemData, string)> ItemsToDispense =
            new ConcurrentQueue<(ItemData, string)>();

        private float ItemActiveTime = 0f;
        private float activeItemDuration = 10f;
        public ItemData ActiveItem { get; private set; }

        private bool IsItemActive = false;

        private bool IsDeathlinkQueued = false;
        private string DeathlinkSender = "";

        public bool WeaponTrickeryTrapActive { get; private set; } = false;

        public void Reset()
        {
            ItemsToDispense.Clear();
            ItemActiveTime = 0f;
            IsItemActive = false;
            IsDeathlinkQueued = false;
        }

        internal void QueueItem(ItemData item, string sender)
        {
            ItemsToDispense.Enqueue((item, sender));
        }

        public void QueueDeathLink(string sender)
        {
            IsDeathlinkQueued = true;
            DeathlinkSender = sender;
        }

        private float timer = 0f;
        private float checkInterval = 0.1f;
        private float randomItemTimer = 0f;

        private void OnGUI()
        {
            if (
                !IsItemActive
                || ActiveItem == null
                || Randomizer.IsPaused
                || !Randomizer.Configuration.trapShowActiveItemBox.Value
            )
                return;

            float remainingTime = Mathf.Max(0f, activeItemDuration - ItemActiveTime);
            string labelText = $"Active Item: {ActiveItem.Name}\n{remainingTime:F1}s";

            float boxWidth = 200;
            float boxHeight = 35f;

            float posX = (Screen.width * Randomizer.Configuration.trapItemboxHorizontalPositioning.Value)- boxWidth/2;
            float posY = (Screen.height * Randomizer.Configuration.trapItemboxVerticalPositioning.Value) - boxHeight;

            Rect boxRect = new Rect(posX, posY, boxWidth, boxHeight);
            GUI.Box(boxRect, labelText);
        }

        public void Update()
        {
            if (Randomizer.CurrentLevel == "TitleScene")
                return;

            if (timer >= checkInterval)
            {
                HandleConfigurationItems();

                timer = 0f;

                if (!AreItemsDispensible())
                    return;

                if (IsDeathlinkQueued)
                {
                    HandleDeathlink();
                    return;
                }

                if (IsItemActive && ItemActiveTime <= activeItemDuration)
                {
                    HandleActiveItem();
                    return; // Item with duration active
                }

                if (IsItemActive)
                    DisableActiveItem();

                if (
                    UnityEngine.Random.Range(0f, 100f)
                    > Randomizer.Configuration.trapChanceToTrigger.Value
                )
                    return;

                if (!TryDispenseItem())
                    return;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                randomItemTimer += Time.unscaledDeltaTime;
            }

            if (IsItemActive)
            {
                ItemActiveTime += Time.unscaledDeltaTime;
            }
        }

        private bool TryDispenseItem()
        {
            if (!ItemsToDispense.TryPeek(out var pendingItem))
                return false;

            Logger.LogInfo($"Activating item {pendingItem.Item1.Name}");
            ActivateItem(pendingItem.Item1, pendingItem.Item2);

            ItemsToDispense.TryDequeue(out var _);
            return true;
        }

        private void DisableActiveItem()
        {
            if (ActiveItem == null)
                return;

            switch (ActiveItem.Name)
            {
                case "Double Time":
                    if (Time.timeScale != 0f)
                        Time.timeScale = 1f;
                    Logger.LogInfo($"Disabling {ActiveItem.Name}");
                    break;
                case "Half Time":
                    if (Time.timeScale != 0f)
                        Time.timeScale = 1f;
                    Logger.LogInfo($"Disabling {ActiveItem.Name}");
                    break;
                case "Invisible Weapons":
                    WeaponAbilityControllerPatches.ToggleWeaponInvisibility(false);
                    Logger.LogInfo($"Disabling {ActiveItem.Name}");
                    break;
                case "Weapon Trickery":
                    WeaponTrickeryTrapActive = false;
                    Logger.LogInfo($"Disabling {ActiveItem.Name}");
                    break;
                case "Always on Beat":
                    PlayerPatches.ToggleAssistMode(false);
                    Logger.LogInfo($"Disabling {ActiveItem.Name}");
                    break;
                default:
                    break;
            }

            ActiveItem = null;
            IsItemActive = false;
            ItemActiveTime = 0f;
        }

        private void HandleDeathlink()
        {
            if (Randomizer.Configuration.archipelagoDeathlinkType.Value == DeathLinkType.Death)
            {
                Logger.LogInfo($"Deathlink is triggered, killing player directly");
                PlayerPatches.KillPlayer(DeathlinkSender);
            }
            else if (
                Randomizer.Configuration.archipelagoDeathlinkType.Value == DeathLinkType.DeathTrap
            )
            {
                ItemsToDispense.Enqueue((Items.ItemDataByName["Death"], DeathlinkSender));
                Logger.LogInfo($"Deathlink is triggered, adding a Death item to the queue");
            }
            else if (
                Randomizer.Configuration.archipelagoDeathlinkType.Value == DeathLinkType.RandomTrap
            )
            {
                if (!GetRandomItem(Lookup.GetTrapItems(), out var item))
                    return;
                ItemsToDispense.Enqueue((item, DeathlinkSender));
                Logger.LogInfo($"Deathlink is triggered, adding a random {item.Name} to the queue");
            }

            IsDeathlinkQueued = false;
            DeathlinkSender = "";
        }

        private void ActivateItem(ItemData item, string sender)
        {
            switch (item.Type)
            {
                case ItemType.Weapon:
                    DispenseWeapon(item, sender);
                    break;
                case ItemType.Combat:
                    DispenseCombat(item, sender);
                    break;
                default:
                    break;
            }
            return;
        }

        private void DispenseCombat(ItemData item, string sender)
        {
            switch (item.Name)
            {
                case "Next Multiplier":
                    ScoreControllerPatches.NextTiersToApply++;
                    break;

                case "Max Multiplier":
                    ScoreControllerPatches.MaxTiersToApply++;
                    break;

                case "Reset Multiplier":
                    ScoreControllerPatches.ResetTiersToApply++;
                    break;

                case "Trigger Ultimate":
                    WeaponAbilityControllerPatches.TriggerUltimate();
                    break;

                case "Complementing Voiceline":
                    SoundEmitterSystemPatches.PlayComplementingVoiceline();
                    break;

                case "Encouraging Voiceline":
                    SoundEmitterSystemPatches.PlayEncouragingVoiceline();
                    break;

                case "Failing Voiceline":
                    SoundEmitterSystemPatches.PlayFailingVoiceline();
                    break;

                case "Death":
                    PlayerPatches.KillPlayer(sender);
                    break;

                case "Double Time":
                    ActiveItem = item;
                    ItemActiveTime = 0f;
                    activeItemDuration = Randomizer.Configuration.trapDoubleTimeActiveTime.Value;
                    IsItemActive = true;
                    IngameMessagesPatches.DisplayItemActivated($"Double Time");
                    break;

                case "Half Time":
                    ActiveItem = item;
                    ItemActiveTime = 0f;
                    activeItemDuration = Randomizer.Configuration.trapHalfTimeActiveTime.Value;
                    IsItemActive = true;
                    IngameMessagesPatches.DisplayItemActivated($"Half Time");
                    break;

                case "Invisible Weapons":
                    ActiveItem = item;
                    ItemActiveTime = 0f;
                    activeItemDuration = Randomizer
                        .Configuration
                        .trapInvisibleWeaponActiveTime
                        .Value;
                    IsItemActive = true;
                    IngameMessagesPatches.DisplayItemActivated($"Invisible Weapons");
                    break;

                case "Weapon Trickery":
                    ActiveItem = item;
                    ItemActiveTime = 0f;
                    activeItemDuration = Randomizer
                        .Configuration
                        .trapWeaponTrickeryActiveTime
                        .Value;
                    IsItemActive = true;
                    IngameMessagesPatches.DisplayItemActivated($"Weapon Trickery");
                    break;

                case "Always on Beat":
                    ActiveItem = item;
                    ItemActiveTime = 0f;
                    activeItemDuration = Randomizer.Configuration.trapAlwaysOnBeatActiveTime.Value;
                    IsItemActive = true;
                    IngameMessagesPatches.DisplayItemActivated($"Always on Beat");
                    break;

                default:
                    break;
            }
        }

        private void DispenseWeapon(ItemData item, string sender)
        {
            PlayerWeaponType type = Lookup.ExtendedWeaponNameToType[item.Name];
            WeaponAbilityControllerPatches.GiveWeapon(type, item.Name);
        }

        private bool AreItemsDispensible()
        {
            return Randomizer.CurrentGameState == GameStateController.GameStateName.InGame
                && !Randomizer.IsLoadingHellsSelection
                && Randomizer.LevelActiveTime > 10f
                && !Randomizer.IsPaused;
        }

        private void HandleActiveItem()
        {
            switch (ActiveItem.Name)
            {
                case "Double Time":
                    if (Time.timeScale != 0f)
                        Time.timeScale = Randomizer.Configuration.trapDoubleTimeScale.Value;
                    break;
                case "Half Time":
                    if (
                        Time.timeScale != 0f
                        && !Randomizer.Configuration.gameplayDoubletimeActive.Value
                    )
                        Time.timeScale = Randomizer.Configuration.trapHalfTimeScale.Value;
                    break;
                case "Invisible Weapons":
                    WeaponAbilityControllerPatches.ToggleWeaponInvisibility(true);
                    break;
                case "Weapon Trickery":
                    WeaponAbilityControllerPatches.ToggleWeaponTrickery(true);
                    EnemyPatches.ToggleWeaponTrickery(true);
                    WeaponTrickeryTrapActive = true;
                    break;
                case "Always on Beat":
                    PlayerPatches.ToggleAssistMode(true);
                    break;

                default:
                    break;
            }
        }

        private bool ConfigSpeedChangeActive = false;

        private void HandleConfigurationItems()
        {
            if (Time.timeScale != 0f && Randomizer.Configuration.gameplayDoubletimeActive.Value)
            {
                ConfigSpeedChangeActive = true;
                Time.timeScale = Randomizer.Configuration.trapDoubleTimeScale.Value;
            }
            else if (
                Time.timeScale != 0f
                && !Randomizer.Configuration.gameplayDoubletimeActive.Value
                && Randomizer.Configuration.gameplayHalftimeActive.Value
            )
            {
                ConfigSpeedChangeActive = true;
                Time.timeScale = Randomizer.Configuration.trapHalfTimeScale.Value;
            }

            if (
                Time.timeScale != 0f
                && !Randomizer.Configuration.gameplayDoubletimeActive.Value
                && !Randomizer.Configuration.gameplayHalftimeActive.Value
                && !(
                    ActiveItem != null
                    && (ActiveItem.Name == "Double Time" || ActiveItem.Name == "Half Time")
                )
                && ConfigSpeedChangeActive
            )
            {
                ConfigSpeedChangeActive = false;
                Time.timeScale = 1f;
            }

            if (
                Randomizer.Configuration.fillerRandomizedFillerDispensionActive.Value
                && Randomizer.CurrentGameState == GameStateController.GameStateName.InGame
                && !Randomizer.IsPaused
                && randomItemTimer >= Randomizer.Configuration.fillerRandomizedFillerRate.Value
            )
            {
                randomItemTimer = 0f;
                QueueRandomItem(Randomizer.Configuration.fillerRandomizedFillerBag.Value);
            }
        }

        private void QueueRandomItem(FillerId fillerRandomizedFillerBag)
        {
            if (!GetRandomItem(fillerRandomizedFillerBag, out var item))
                return;

            string message = $"Adding {item.Name} from the random item bag to the item queue";
            ArchipelagoConsole.Instance.LogMessage(message);

            ItemsToDispense.Enqueue((item, "Filler Bag"));
        }

        private static bool GetRandomItem(FillerId fillerRandomizedFillerBag, out ItemData item)
        {
            List<string> fillerNames = new() { };
            foreach (var kvp in Lookup.FillerIdToName)
            {
                if (fillerRandomizedFillerBag.HasFlag(kvp.Key))
                    fillerNames.Add(kvp.Value);
            }

            if (fillerNames.Count == 0)
            {
                item = null;
                return false;
            }

            var fillerName = fillerNames[new System.Random().Next(fillerNames.Count)];
            item = Items.ItemDataByName[fillerName];
            return true;
        }
    }
}
