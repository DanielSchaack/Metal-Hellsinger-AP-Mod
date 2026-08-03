using System.Collections.Concurrent;
using UnityEngine;

namespace Randomizer
{
    public class IngameDispenser : MonoBehaviour
    {
        private ConcurrentQueue<ItemData> ItemsToDispense = new ConcurrentQueue<ItemData>();
        private float ItemActiveTime = 0f;
        private float activeItemDuration = 10f;
        private bool IsItemActive = false;
        private bool IsDeathlinkQueued = false;
        public bool InvisibleWeaponTrapActive = true;
        public bool WeaponTrickeryTrapActive = false;
        public bool DoubleTimeTrapActive = false;//TODO:
        public bool HalfTimeTrapActive = false;//TODO:

        public void Reset()
        {
            ItemsToDispense.Clear();
            ItemActiveTime = 0f;
            IsItemActive = false;
            IsDeathlinkQueued = false;
        }

        internal void QueueItem(ItemData item)
        {
            ItemsToDispense.Enqueue(item);
        }

        public void QueueDeathLink()
        {
            IsDeathlinkQueued = true;
        }

        private float timer = 0f;
        private float checkInterval = 0.1f;


        public void Update()
        {
            if (Randomizer.CurrentLevel == "TitleScene")
                return;

            if (timer >= checkInterval)
            {
                timer = 0f;
                if (IsDeathlinkQueued && Randomizer.LevelActiveTime > 5f)
                {
                    Logger.LogInfo($"Deathlink is triggered, killing player");
                    PlayerPatches.KillPlayer();
                    IsDeathlinkQueued = false;
                }
                if (IsItemActive && ItemActiveTime <= activeItemDuration)
                {
                    ItemActiveTime += Time.fixedUnscaledDeltaTime;
                    return;
                }
                IsItemActive = false;
                ItemActiveTime = 0f;

                if (!ItemsToDispense.TryPeek(out var pendingItem))
                    return;

                Logger.LogInfo($"Activating item {pendingItem.Name}");
                IsItemActive = true;

                // TODO: Implement item dispension

                ItemsToDispense.TryDequeue(out var _);
            }
            else
            {
                timer += Time.unscaledDeltaTime;
            }
        }
    }
}
