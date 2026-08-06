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
            Logger.LogDebug($"MovementStateMachine MoveTo Prefix called and moves to: {stateType}");

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

    [HarmonyPatch(typeof(JumpMovementState))]
    public class JumpMovementPatches
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(JumpMovementState.CanPerform))]
        static void JumpMovementCanPerformPostfix(
            JumpMovementState __instance,
            MovementStateType currentStateType,
            ref bool __result
        )
        {
            if (
                currentStateType == MovementStateType.Fall
                && !Randomizer.ItemTracker.CanDoubleJump()
            )
                __result = false;

            __result =
                (__result && Randomizer.ItemTracker.CanJump())
                || Randomizer.ItemTracker.CanInfiniteJump();

            Logger.LogInfo(
                $"JumpMovementState CanPerform Postfix called with current State {currentStateType}, returning {__result}"
            );
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(JumpMovementState.TriggerJump))]
        static bool TriggerJumpPrefix(
            JumpMovementState __instance,
            ref bool isAirJump,
            ref bool isDoubleJump
        )
        {
            Logger.LogDebug(
                $"JumpMovementState TriggerJump Prefix called and is air jump: {isAirJump} and is double jump: {isDoubleJump}"
            );
            return true;
        }
    }

    [HarmonyPatch(typeof(SoarMovementState))]
    public class SoarMovementPatches
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(SoarMovementState.CanPerform))]
        static void SoarMovementCanPerformPostfix(
            SoarMovementState __instance,
            MovementStateType currentStateType,
            ref bool __result
        )
        {
            __result = __result && Randomizer.ItemTracker.CanSoar();
            Logger.LogInfo(
                $"SoarMovementState CanPerform Postfix called with current State {currentStateType}, returning {__result}"
            );
        }
    }

    [HarmonyPatch(typeof(DodgeMovementState))]
    public class DodgeMovementPatches
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(DodgeMovementState.CanPerform))]
        static void DodgeMovementCanPerformPostfix(
            DodgeMovementState __instance,
            MovementStateType currentStateType,
            ref bool __result
        )
        {
            __result = __result && Randomizer.ItemTracker.CanDash();
            Logger.LogInfo(
                $"DodgeMovementState CanPerform Postfix called with current State {currentStateType}, returning {__result}"
            );
        }
    }

    [HarmonyPatch(typeof(ScoreController))]
    public class ScoreControllerPatches
    {
        public static ScoreController Instance;
        public static int NextTiersToApply = 0;
        public static int MaxTiersToApply = 0;
        public static int ResetTiersToApply = 0;

        [HarmonyPrefix]
        [HarmonyPatch(nameof(ScoreController.Update))]
        static bool UpdatePrefix(ref ScoreController __instance, float dt)
        {
            if (MaxTiersToApply > 0 && __instance.GetCurrentTierIndex()>__instance.m_minMultiplierTier)
            {
                Logger.LogInfo(
                    $"ScoreController Reset multiplier tier to apply is at {ResetTiersToApply}, decreasing by one"
                );
                __instance.SetTier(__instance.m_minMultiplierTier, true);
                IngameMessagesPatches.DisplayItemActivated($"Reset Multiplier");

                ResetTiersToApply--;
                return true;
            }

            if (NextTiersToApply > 0 && !__instance.IsMaxMultiplierTier())
            {
                Logger.LogInfo(
                    $"ScoreController Next multiplier tier to apply is at {NextTiersToApply}, increasing by one"
                );
                __instance.AdvanceToNextTier();
                IngameMessagesPatches.DisplayItemActivated($"Next Multiplier");

                NextTiersToApply--;
                return true;
            }

            if (MaxTiersToApply > 0 && !__instance.IsMaxMultiplierTier())
            {
                Logger.LogInfo(
                    $"ScoreController Max multiplier tier to apply is at {MaxTiersToApply}, increasing by one"
                );
                __instance.AdvanceToMaxTier();
                IngameMessagesPatches.DisplayItemActivated($"Max Multiplier");
                MaxTiersToApply--;
                return true;
            }
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(ScoreController.SetMinTier))]
        static bool SetMinTierPrefix(ref ScoreController __instance)
        {
            Instance = __instance;
            return true;
        }
    }
}
