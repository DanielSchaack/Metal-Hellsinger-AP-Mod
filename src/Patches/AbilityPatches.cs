using HarmonyLib;

namespace Randomizer
{
    [HarmonyPatch(typeof(BeatStreakController))]
    public class BeatStreakControllerPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(BeatStreakController.Activate))]
        static bool ActivatePrefix(
            BeatStreakController __instance,
            BeatStreakEffectConfiguration beatStreakEffect
        )
        {
            Logger.LogInfo(
                $"BeatStreakController Activate Prefix called for {beatStreakEffect.name}"
            );
            return Randomizer.ItemTracker.HasBoonByBeatSreakEffect(beatStreakEffect.Effect);
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(BeatStreakController.Activate))]
        static void ActivatePostfix(
            BeatStreakController __instance,
            BeatStreakEffectConfiguration beatStreakEffect
        )
        {
            Logger.LogInfo(
                $"BeatStreakController Activate Postfix called for {beatStreakEffect.name}"
            );
        }
    }

    [HarmonyPatch(typeof(BeatSequencer))]
    public class BeatSequencerPatches
    {
        public static BeatSequencer Instance;

        [HarmonyPrefix]
        [HarmonyPatch(nameof(BeatSequencer.SetSequenceMode))]
        static bool SetSequenceModePrefix(BeatSequencer __instance, BeatSequenceMode mode)
        {
            Logger.LogDebug($"BeatSequencer SetSequenceMode Prefix called for {mode}");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(BeatSequencer.SetSequenceMode))]
        static void SetSequenceModePostfix(BeatSequencer __instance, BeatSequenceMode mode)
        {
            Logger.LogDebug($"BeatSequencer SetSequenceMode Postfix called for {mode}");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(BeatSequencer.GetSequenceMode))]
        static bool GetSequenceModePrefix(BeatSequencer __instance)
        {
            // Logger.LogDebug($"BeatSequencer GetSequenceMode Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(BeatSequencer.GetSequenceMode))]
        static void GetSequenceModePostfix(ref BeatSequencer __instance, ref BeatSequenceMode __result)
        {
            if (Instance == null)
            {
                Logger.LogInfo($"BeatSequencer GetSequenceMode Postfix called for {__result}");
                Instance = __instance;
            }
            // INFO: is called each update when in pause menu for EighthNotes
            // if(Randomizer.CurrentGameState == GameStateController.GameStateName.InGame )
            //     Logger.LogDebug($"BeatSequencer GetSequenceMode Postfix InGame called for {__result}");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(BeatSequencer.SetSequenceData))]
        static bool SetSequenceDataPrefix(BeatSequencer __instance, BeatMatchingSequencerData data)
        {
            Logger.LogDebug(
                $"BeatSequencer SetSequenceData Prefix called BeatMatchingSequencerData"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(BeatSequencer.SetSequenceData))]
        static void SetSequenceDataPostfix(BeatSequencer __instance, BeatMatchingSequencerData data)
        {
            Logger.LogDebug($"BeatSequencer SetSequenceData Postfix called");
        }
    }

    [HarmonyPatch(typeof(OverkillController))]
    public class OverkillControllerPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(OverkillController.ToggleOverkillUI))]
        private static bool ToggleOverkillUIPrefix(
            OverkillController __instance,
            ref bool shouldBeVisible,
            Enemy targetEnemy
        )
        {
            // Removes the call to action UI for slaughter
            shouldBeVisible = shouldBeVisible && Randomizer.ItemTracker.CanSlaughter();
            return true;
        }
    }

    [HarmonyPatch(typeof(MovementStateMachine))]
    public class MovementStateMachinePatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(MovementStateMachine.MoveTo))]
        private static bool MoveToPrefix(
            MovementStateMachine __instance,
            MovementStateType stateType
        )
        {
            Logger.LogInfo($"MovementStateMachine MoveTo Prefix called and moves to: {stateType}");

            if (stateType == MovementStateType.Overkill)
            {
                if (!Randomizer.ItemTracker.CanSlaughter())
                    return false;
                Randomizer.LocationTracker.CheckMisc("Slaughter");
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(ClinchIndicatorContainer))]
    public class ClinchIndicatorContainerPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(ClinchIndicatorContainer.OnOverkillTriggered))]
        private static bool OnOverkillTriggeredPrefix(
            ClinchIndicatorContainer __instance,
            bool onBeat
        )
        {
            Logger.LogInfo(
                $"ClinchIndicatorContainer OnOverkillTriggered Prefix called and is onBeat: {onBeat}"
            );
            return Randomizer.ItemTracker.CanSlaughter();
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(ClinchIndicatorContainer.OnOverkillTriggered))]
        private static void OnOverkillTriggeredPostfix(
            ClinchIndicatorContainer __instance,
            bool onBeat
        )
        {
            Logger.LogInfo(
                $"ClinchIndicatorContainer OnOverkillTriggered Postfix called and is onBeat: {onBeat}"
            );
        }
    }
}
