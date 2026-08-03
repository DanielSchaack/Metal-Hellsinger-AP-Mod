using HarmonyLib;

namespace Randomizer
{
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
}
