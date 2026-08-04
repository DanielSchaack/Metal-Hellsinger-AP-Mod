using System;
using System.Collections.Concurrent;
using UnityEngine;
using static Randomizer.ArchipelagoIntegration;

namespace Randomizer
{
    public class IngameDispenser : MonoBehaviour
    {
        private ConcurrentQueue<(ItemData, string)> ItemsToDispense = new ConcurrentQueue<(ItemData, string)>();
        private float ItemActiveTime = 0f;
        private float activeItemDuration = 10f;
        private bool IsItemActive = false;
        private bool IsDeathlinkQueued = false;
        private string DeathlinkSender = "";
        public bool InvisibleWeaponTrapActive = false;// TODO:
        public bool AlwaysOnBeatActive = false;// TODO:
        public bool WeaponTrickeryTrapActive = false;//TODO:
        public bool DoubleTimeTrapActive = false;//TODO:
        public bool HalfTimeTrapActive = false;//TODO:

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


        public void Update()
        {
            if (Randomizer.CurrentLevel == "TitleScene")
                return;

            HandleConfigurationItems();

            if (timer >= checkInterval)
            {
                timer = 0f;

                if (!AreItemsDispensible())
                    return;

                if (IsDeathlinkQueued)
                {
                    Logger.LogInfo($"Deathlink is triggered, killing player");
                    if(Randomizer.Configuration.archipelagoDeathlinkType.Value == DeathLinkType.Death)
                        PlayerPatches.KillPlayer(DeathlinkSender);
                    else if(Randomizer.Configuration.archipelagoDeathlinkType.Value == DeathLinkType.Trap)
                        ItemsToDispense.Enqueue((Items.ItemDataByName["Death"], DeathlinkSender));

                    IsDeathlinkQueued = false;
                    DeathlinkSender = "";
                    return;
                }

                if (IsItemActive && ItemActiveTime <= activeItemDuration)
                {
                    HandleActiveItem();
                    ItemActiveTime += Time.fixedUnscaledDeltaTime;
                    return;
                }

                IsItemActive = false;
                ItemActiveTime = 0f;

                if (!ItemsToDispense.TryPeek(out var pendingItem))
                    return;

                Logger.LogInfo($"Activating item {pendingItem.Item1.Name}");
                ActivateItem(pendingItem.Item1, pendingItem.Item2);

                ItemsToDispense.TryDequeue(out var _);
            }
            else
            {
                timer += Time.unscaledDeltaTime;
            }
        }

        // TODO: Implement item dispension
        private void ActivateItem(ItemData item1, string item2)
        {
            return;
        }

        private bool AreItemsDispensible()
        {
            return Randomizer.CurrentGameState == GameStateController.GameStateName.InGame
                && Randomizer.LevelActiveTime > 10f
                && !Randomizer.IsPaused;
        }

        private void HandleActiveItem()
        {
            throw new NotImplementedException();
        }

        private void HandleConfigurationItems()
        {
            if(Randomizer.Configuration.gameplayDoubletimeActive.Value)
                Time.timeScale = 1.65f;

            if(!Randomizer.Configuration.gameplayDoubletimeActive.Value
                    && Randomizer.Configuration.gameplayHalftimeActive.Value)
                Time.timeScale = 0.60f;
        }
    }
}
