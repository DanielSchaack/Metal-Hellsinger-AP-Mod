using HarmonyLib;
using Outsiders.GUI;

namespace Randomizer
{
    [HarmonyPatch(typeof(WorldItemDiscoverySystem))]
    public class WorldItemDiscoverySystemPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(WorldItemDiscoverySystem.OnWorldItemTriggerEntered))]
        static bool OnWorldItemTriggerEnteredPrefix(
            WorldItemDiscoverySystem __instance,
            WorldItemDiscoveryTrigger component
        )
        {
            Logger.LogInfo(
                $"WorldItemDiscoverySystem OnWorldItemTriggerEntered Prefix called for {component.WorldItemDiscoverySaveID}"
            );
            Randomizer.LocationTracker.CheckWorldItem(component.WorldItemDiscoverySaveID);
            return false;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(WorldItemDiscoverySystem.OnWorldItemTriggerEntered))]
        static void OnWorldItemTriggerEnteredPostfix(WorldItemDiscoverySystem __instance)
        {
            Logger.LogDebug($"WorldItemDiscoverySystem OnWorldItemTriggerEntered Postfix called");
        }
    }

    [HarmonyPatch(typeof(EnemyDiscoveredSystem))]
    public class EnemyDiscoveredSystemPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(EnemyDiscoveredSystem.OnEnemyDiscoveredTriggerEntered))]
        static bool OnEnemyDiscoveredTriggerEnteredPrefix(
            EnemyDiscoveredSystem __instance,
            EnemyDiscoveredTrigger component
        )
        {
            Logger.LogInfo(
                $"EnemyDiscoveredSystem OnEnemyDiscoveredTriggerEntered Prefix called for {component.WorldItemDiscoverySaveID}"
            );
            Randomizer.LocationTracker.CheckWorldItem(component.WorldItemDiscoverySaveID);
            return false;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(EnemyDiscoveredSystem.OnEnemyDiscoveredTriggerEntered))]
        static void OnEnemyDiscoveredTriggerEnteredPostfix(EnemyDiscoveredSystem __instance)
        {
            Logger.LogDebug(
                $"EnemyDiscoveredSystem OnEnemyDiscoveredTriggerEntered Postfix called"
            );
        }
    }

    [HarmonyPatch(typeof(BeatGradingItem))]
    public class BeatGradingItemPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(BeatGradingItem.Show))]
        static bool ShowPrefix(BeatGradingItem __instance, EBeatGrading beatGrade, EFuryComboType comboType)
        {
            if(comboType != EFuryComboType.None)
                Logger.LogInfo(
                    $"BeatGradingItem Show Prefix called for beat grading {beatGrade} and combo type {comboType}"
                );
            Randomizer.LocationTracker.CheckFuryCombo(comboType);
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(BeatGradingItem.Show))]
        static void ShowPostfix(BeatGradingItem __instance)
        {
            Logger.LogDebug($"BeatGradingItem Show Postfix called");
        }
    }

    [HarmonyPatch(typeof(InGameGUIController))]
    public class IngameMessagesPatches
    {
        public static InGameGUIController Instance;
        private static bool customMessage = false;

        public static void DisplayCheckCollected()
        {
            BonusScoreContainerPatches.DisplayCheckCollected("Check");
        }

        public static void DisplayCheckCollected(string checkName)
        {
            BonusScoreContainerPatches.DisplayCheckCollected(checkName);
        }

        public static void DisplayWeaponGiven(string itemName)
        {
            string message = $"{itemName} given!".ToUpper();
            BeatChainContainerPatches.DisplayItemActivated(message);
        }

        public static void DisplayItemActivated(string itemName)
        {
            string message = $"{itemName} activated!".ToUpper();
            BeatChainContainerPatches.DisplayItemActivated(message);
        }

        public static void DisplayItemReceived(ItemData item, string sender)
        {
            if (
                Instance != null
                && Randomizer.LevelActiveTime > 5f
                && Randomizer.Configuration.archipelagoPopupForClassification.Value.HasFlag(
                    item.Classification
                )
            )
            {
                var message = string.IsNullOrEmpty(sender)
                    ? $"{item.Name.ToUpper()} received!"
                    : $"{item.Name.ToUpper()} from {sender.ToUpper()} received!";

                Logger.LogInfo($"Showing item received: '{message}'");

                customMessage = true;
                Instance.ShowSpecialMomentMessage(
                    message,
                    true,
                    false,
                    0,
                    SpecialMomentMessageSize.Small,
                    SpecialMomentMessageStyle.EndlessMode
                );
                customMessage = false;
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(InGameGUIController.Initialize))]
        static bool InitializePrefix(ref InGameGUIController __instance)
        {
            Instance = __instance;
            Logger.LogDebug($"InGameGUIController Initialize Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(InGameGUIController.Initialize))]
        static void InitializePostfix(InGameGUIController __instance)
        {
            Logger.LogDebug($"InGameGUIController Initialize Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(InGameGUIController.InitChallengeTracker))]
        static bool InitChallengeTrackerPrefix(ref InGameGUIController __instance)
        {
            Instance = __instance;
            Logger.LogDebug($"InGameGUIController InitChallengeTracker Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(InGameGUIController.InitChallengeTracker))]
        static void InitChallengeTrackerPostfix(InGameGUIController __instance)
        {
            Logger.LogDebug($"InGameGUIController InitChallengeTracker Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(InGameGUIController.TearDown))]
        static bool TearDownPrefix(InGameGUIController __instance)
        {
            Logger.LogDebug($"InGameGUIController TearDown Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(InGameGUIController.TearDown))]
        static void TearDownPostfix(InGameGUIController __instance)
        {
            Logger.LogDebug($"InGameGUIController TearDown Postfix called");
            Instance = null;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(InGameGUIController.ShowComboCompletedMessage))]
        static bool ShowComboCompletedMessagePrefix(ref InGameGUIController __instance)
        {
            Logger.LogDebug($"InGameGUIController ShowComboCompletedMessage Prefix called");
            return false;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(InGameGUIController.ShowComboCompletedMessage))]
        static void ShowComboCompletedMessagePostfix(InGameGUIController __instance)
        {
            Logger.LogDebug($"InGameGUIController ShowComboCompletedMessage Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(InGameGUIController.ShowSimpleSpecialMomentMessage))]
        static bool ShowSimpleSpecialMomentMessagePrefix(InGameGUIController __instance)
        {
            Logger.LogDebug($"InGameGUIController ShowSimpleSpecialMomentMessage Prefix called");
            return customMessage;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(InGameGUIController.ShowSimpleSpecialMomentMessage))]
        static void ShowSimpleSpecialMomentMessagePostfix(InGameGUIController __instance)
        {
            Logger.LogDebug($"InGameGUIController ShowSimpleSpecialMomentMessage Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(InGameGUIController.ShowSpecialMomentMessage))]
        static bool ShowSpecialMomentMessagePrefix(InGameGUIController __instance)
        {
            Logger.LogDebug($"InGameGUIController ShowSpecialMomentMessage Prefix called");
            return customMessage;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(InGameGUIController.ShowSpecialMomentMessage))]
        static void ShowSpecialMomentMessagePostfix(InGameGUIController __instance)
        {
            Logger.LogDebug($"InGameGUIController ShowSpecialMomentMessage Postfix called");
        }
    }

    [HarmonyPatch(typeof(BonusScoreContainer))]
    public class BonusScoreContainerPatches
    {
        public static BonusScoreContainer Instance;

        private static bool isManualCall = true;
        private static string customMessage = "";

        public static void DisplayCheckCollected(string checkName)
        {
            if (Instance != null)
            {
                customMessage = $"'{checkName}' collected!".ToUpper();
                Logger.LogInfo($"Showing location pickup: {customMessage}");

                isManualCall = true;
                if(Lookup.IsChallengeLevelId(Randomizer.CurrentLevel))
                {
                    Instance.gameObject.SetActive(true);
                    Instance.SetVisible(true, false);
                    Instance.m_canvasGroup.alpha = 1;
                    Instance.m_tierFill.gameObject.SetActive(false);
                    Instance.m_tierLabel.gameObject.SetActive(false);
                    Instance.transform.Find("MultiplierBarBkg")?.gameObject.SetActive(false);
                    Instance.m_pickupMessage.gameObject.SetActive(true);
                }
                Instance.PlayTierGainedSequence();
                Instance.ShowPickupMessage(MultiplierBoostEventType.AdvancedToNextTier);
                isManualCall = false;
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(BonusScoreContainer.Awake))]
        static bool AwakePrefix(BonusScoreContainer __instance)
        {
            Logger.LogDebug($"BonusScoreContainer Awake Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(BonusScoreContainer.Awake))]
        static void AwakePostfix(ref BonusScoreContainer __instance)
        {
            Instance = __instance;
            Logger.LogDebug($"BonusScoreContainer Awake Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(BonusScoreContainer.OnDestroy))]
        static bool OnDestroyPrefix(BonusScoreContainer __instance)
        {
            Logger.LogDebug($"BonusScoreContainer OnDestroy Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(BonusScoreContainer.OnDestroy))]
        static void OnDestroyPostfix(ref BonusScoreContainer __instance)
        {
            Instance = null;
            Logger.LogDebug($"BonusScoreContainer OnDestroy Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(BonusScoreContainer.ShowPickupMessage))]
        static bool ShowPickupMessagePrefix(BonusScoreContainer __instance)
        {
            Logger.LogDebug($"BonusScoreContainer ShowPickupMessage Prefix called");
            return isManualCall; // Only show when we want to
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(BonusScoreContainer.ShowPickupMessage))]
        static void ShowPickupMessagePostfix(BonusScoreContainer __instance)
        {
            Logger.LogDebug($"BonusScoreContainer ShowPickupMessage Postfix called");
            __instance.m_pickupMessage.m_label.text = customMessage;
        }
    }

    [HarmonyPatch(typeof(BeatChainContainer))]
    public class BeatChainContainerPatches
    {
        public static BeatChainContainer Instance;

        public static void DisplayItemActivated(string message)
        {
            if (Instance != null)
            {
                Logger.LogInfo($"Showing item activated: {message}");

                BeatStreakMessageContainerPatches.isManualCall = true;
                Instance.InitializeAnimations();
                Instance.ResetAllStreakTweens();
                Instance.gameObject.SetActive(true);

                if(Lookup.IsChallengeLevelId(Randomizer.CurrentLevel))
                {
                    Instance.SetVisible(true, false);
                    Instance.m_canvasGroup.alpha = 1;
                }

                Instance.m_boonMessageContainer.gameObject.SetActive(true);
                Instance.m_boonMessageContainer.SetText(message);
                Instance.m_boonMessageContainer.Show(true, true);
                BepInEx.Unity.IL2CPP.Utils.MonoBehaviourExtensions.StartCoroutine(
                    Instance,
                    CloseAfterDelay(Instance.m_boonMessageContainer)
                );
                BeatStreakMessageContainerPatches.isManualCall = false;
            }
        }

        private static System.Collections.IEnumerator CloseAfterDelay(
            BeatStreakMessageContainer container
        )
        {
            yield return new UnityEngine.WaitForSeconds(2f);
            container._InitializeAnimations_b__18_0();
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(BeatChainContainer.Init))]
        static bool InitPrefix(BeatChainContainer __instance)
        {
            Logger.LogDebug($"BeatChainContainer Init Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(BeatChainContainer.Init))]
        static void InitPostfix(ref BeatChainContainer __instance)
        {
            Instance = __instance;
            Logger.LogDebug($"BeatChainContainer Init Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(BeatChainContainer.InitializeAnimations))]
        static bool InitializeAnimationsPrefix(BeatChainContainer __instance)
        {
            Logger.LogDebug($"BeatChainContainer InitializeAnimations Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(BeatChainContainer.InitializeAnimations))]
        static void InitializeAnimationsPostfix(ref BeatChainContainer __instance)
        {
            Instance = __instance;
            Logger.LogDebug($"BeatChainContainer InitializeAnimations Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(BeatChainContainer.ResetAllStreakTweens))]
        static bool ResetAllStreakTweensPrefix(BeatChainContainer __instance)
        {
            Logger.LogDebug($"BeatChainContainer ResetAllStreakTweens Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(BeatChainContainer.ResetAllStreakTweens))]
        static void ResetAllStreakTweensPostfix(ref BeatChainContainer __instance)
        {
            Logger.LogDebug($"BeatChainContainer ResetAllStreakTweens Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(BeatChainContainer.OnDestroy))]
        static bool OnDestroyPrefix(BeatChainContainer __instance)
        {
            Logger.LogDebug($"BeatChainContainer OnDestroy Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(BeatChainContainer.OnDestroy))]
        static void OnDestroyPostfix(ref BeatChainContainer __instance)
        {
            Instance = null;
            Logger.LogDebug($"BeatChainContainer OnDestroy Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(BeatChainContainer.Show))]
        static bool ShowPrefix(BeatChainContainer __instance)
        {
            Logger.LogDebug($"BeatChainContainer Show Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(BeatChainContainer.Show))]
        static void ShowPostfix(BeatChainContainer __instance)
        {
            Logger.LogDebug($"BeatChainContainer Show Postfix called");
        }
    }

    [HarmonyPatch(typeof(BeatStreakMessageContainer))]
    public class BeatStreakMessageContainerPatches
    {
        public static BeatStreakMessageContainer Instance;

        public static bool isManualCall = false;

        public static void DisplayItemActivated(string message)
        {
            if (Instance != null)
            {
                Logger.LogInfo($"Showing item activated: '{message}'");

                isManualCall = true;
                Instance.InitializeAnimations();
                Instance.ResetTweens();
                Instance.gameObject.SetActive(true);
                Instance.SetText(message);
                Instance.Show(true, false);
                Instance._InitializeAnimations_b__18_0();
                isManualCall = false;
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(BeatStreakMessageContainer.Init))]
        static bool InitPrefix(BeatStreakMessageContainer __instance)
        {
            Logger.LogDebug($"BeatStreakMessageContainer Init Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(BeatStreakMessageContainer.Init))]
        static void InitPostfix(ref BeatStreakMessageContainer __instance)
        {
            Instance = __instance;
            Logger.LogDebug($"BeatStreakMessageContainer Init Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(BeatStreakMessageContainer.InitializeAnimations))]
        static bool InitializeAnimationsPrefix(BeatStreakMessageContainer __instance)
        {
            Logger.LogDebug($"BeatStreakMessageContainer InitializeAnimations Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(BeatStreakMessageContainer.InitializeAnimations))]
        static void InitializeAnimationsPostfix(ref BeatStreakMessageContainer __instance)
        {
            Instance = __instance;
            Logger.LogDebug($"BeatStreakMessageContainer InitializeAnimations Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(BeatStreakMessageContainer.ResetTweens))]
        static bool ResetTweensPrefix(BeatStreakMessageContainer __instance)
        {
            Logger.LogDebug($"BeatStreakMessageContainer ResetTweens Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(BeatStreakMessageContainer.ResetTweens))]
        static void ResetTweensPostfix(ref BeatStreakMessageContainer __instance)
        {
            Instance = __instance;
            Logger.LogDebug($"BeatStreakMessageContainer ResetTweens Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(BeatStreakMessageContainer.OnDestroy))]
        static bool OnDestroyPrefix(BeatStreakMessageContainer __instance)
        {
            Logger.LogDebug($"BeatStreakMessageContainer OnDestroy Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(BeatStreakMessageContainer.OnDestroy))]
        static void OnDestroyPostfix(ref BeatStreakMessageContainer __instance)
        {
            Instance = null;
            Logger.LogDebug($"BeatStreakMessageContainer OnDestroy Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(BeatStreakMessageContainer.Show))]
        static bool ShowPrefix(BeatStreakMessageContainer __instance)
        {
            bool isAvailableBoon = __instance.m_label.text switch
            {
                "ENDURING FURY!" => Randomizer.ItemTracker.HasBoonByBeatSreakEffect(
                    EBeatStreakEffect.SlowerFuryDecay
                ),
                "FASTER ULTIMATE GAIN!" => Randomizer.ItemTracker.HasBoonByBeatSreakEffect(
                    EBeatStreakEffect.IncreasedUltimateBuildSpeed
                ),
                "DEADLIER DASH!" => Randomizer.ItemTracker.HasBoonByBeatSreakEffect(
                    EBeatStreakEffect.IncreasedDashDamage
                ),
                "EXPLOSIVE SLAUGHTER!" => Randomizer.ItemTracker.HasBoonByBeatSreakEffect(
                    EBeatStreakEffect.ExplosiveSlaughters
                ),
                _ => false,
            };
            Logger.LogDebug(
                $"BeatStreakMessageContainer Show Prefix called for '{__instance.m_label.text}' and is manually called: {isManualCall} or is available boon: {isAvailableBoon}"
            );
            return isManualCall || isAvailableBoon; // Only show when we want to
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(BeatStreakMessageContainer.Show))]
        static void ShowPostfix(BeatStreakMessageContainer __instance)
        {
            Logger.LogDebug($"BeatStreakMessageContainer Show Postfix called");
        }
    }
}
