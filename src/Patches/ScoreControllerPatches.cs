using HarmonyLib;

namespace Randomizer
{
    [HarmonyPatch(typeof(ScoreController))]
    public class ScoreControllerPatches
    {
        public static int NextTiersToApply = 0;
        public static int MaxTiersToApply = 0;

        [HarmonyPrefix]
        [HarmonyPatch(nameof(ScoreController.Update))]
        static bool UpdatePrefix(ref ScoreController __instance, float dt)
        {
            if (NextTiersToApply > 0 && !__instance.IsMaxMultiplierTier())
            {
                Logger.LogInfo(
                    $"ScoreController Next multiplier tier to apply is at {NextTiersToApply}, increasing by one"
                );
                __instance.AdvanceToNextTier();
                NextTiersToApply--;
            }

            if (MaxTiersToApply > 0 && !__instance.IsMaxMultiplierTier())
            {
                Logger.LogInfo(
                    $"ScoreController Max multiplier tier to apply is at {MaxTiersToApply}, increasing by one"
                );
                __instance.AdvanceToMaxTier();
                MaxTiersToApply--;
            }
            return true;
        }
    }
}
