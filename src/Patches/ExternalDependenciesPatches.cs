using System;
using System.Collections.Generic;
using Backend;
using HarmonyLib;

namespace Randomizer
{
    [HarmonyPatch(typeof(DLC))]
    public class DLCPatches
    {
        public static DLC Instance = null;

        [HarmonyPrefix]
        [HarmonyPatch(nameof(DLC.GetDLCStoreItemsAsync))]
        static bool GetDLCStoreItemsAsyncPrefix(
            Action<List<StoreItem>> onSuccess,
            Action<EBackendError> onError
        )
        {
            Logger.LogDebug("GetDLCStoreItemsAsync Prefix called");
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(DLC.OpenDLCStore))]
        static bool OpenDLCStorePrefix(EDLC dlc)
        {
            Logger.LogDebug("OpenDLCStore Prefix called");
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(DLC.HasDLC))]
        static bool HasDLCPrefix(ref DLC __instance, ref bool __result, EDLC dlc)
        {
            if (Instance == null)
            {
                Instance = __instance;
                Logger.LogInfo("Set DLC instance");
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(Leaderboard))]
    public class LeaderboardPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(Leaderboard.SubmitScore))]
        static bool SubmitScorePrefix(Action<EBackendError> onComplete)
        {
            Logger.LogInfo("Leaderboard SubmitScore Prefix called, disabling function");
            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(Leaderboard.SubmitLeaderboardScore))]
        static bool SubmitLeaderboardScorePrefix(
            ref int score,
            ref int scoreMask,
            string levelConfigId,
            EDifficulty difficulty,
            Action<EBackendError> onComplete
        )
        {
            Logger.LogInfo(
                $"Leaderboard SubmitLeaderboardScore Prefix called with score {score}, scoreMask {scoreMask}, levelConfigId {levelConfigId}, difficulty {difficulty}"
            );

            score = 0;
            scoreMask = 0;
            Logger.LogInfo($"Setting score to {score} and scoreMask to {scoreMask}");

            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(Leaderboard.SubmitLeaderboardScore))]
        static void SubmitLeaderboardScorePostfix(
            ref int score,
            ref int scoreMask,
            string levelConfigId,
            EDifficulty difficulty,
            Action<EBackendError> onComplete
        )
        {
            Logger.LogInfo(
                $"Leaderboard SubmitLeaderboardScore Postfix called with score {score}, scoreMask {scoreMask}, levelConfigId {levelConfigId}, difficulty {difficulty}, disabling function"
            );
        }
    }
}
