using HarmonyLib;

namespace Randomizer
{
    [HarmonyPatch(typeof(EndlessModeController))]
    public class EndlessModeControllerPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(EndlessModeController.ArenaCompleted))]
        static bool ArenaCompletedPrefix(
            EndlessModeController __instance
        )
        {
            Logger.LogInfo(
                $"EndlessModeController ArenaCompleted Prefix called for arena index{__instance.ArenaIndex}"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(EndlessModeController.ArenaCompleted))]
        static void ArenaCompletedPostfix(
            EndlessModeController __instance
        )
        {
            Logger.LogInfo(
                $"EndlessModeController ArenaCompleted Postfix called for arena index{__instance.ArenaIndex}"
            );
        }
    }

    [HarmonyPatch(typeof(EndlessLobbyUIController))]
    public class EndlessLobbyUIControllerPatches
    {
        public static EndlessModeData data;

        [HarmonyPrefix]
        [HarmonyPatch(nameof(EndlessLobbyUIController._DisplayEndlessModeView_b__27_5))]
        static bool _DisplayEndlessModeView_b__27_5Prefix(
            EndlessLobbyUIController __instance
        )
        {
            // Logger.LogInfo(
            //     $"EndlessLobbyUIController _DisplayEndlessModeView_b__27_5 Prefix called"
            // );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(EndlessLobbyUIController._DisplayEndlessModeView_b__27_5))]
        static void _DisplayEndlessModeView_b__27_5Postfix(
            EndlessLobbyUIController __instance,
            ref EndlessModeData __result

        )
        {
            if(data == null && __result != null){
                data = __result;
                Logger.LogInfo(
                    $"EndlessLobbyUIController _DisplayEndlessModeView_b__27_5 Postfix called"
                );

                Logger.LogInfo($"data aquired, arena levels {data.LevelsPerArena}, amount arenas {data.NumberOfArenas}");
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(EndlessLobbyUIController.DisplayEndlessModeView))]
        static bool DisplayEndlessModeViewPrefix(EndlessLobbyUIController __instance)
        {
            Logger.LogInfo($"EndlessLobbyUIController DisplayEndlessModeView Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(EndlessLobbyUIController.DisplayEndlessModeView))]
        static void DisplayEndlessModeViewPostfix(EndlessLobbyUIController __instance)
        {
            Logger.LogInfo($"EndlessLobbyUIController DisplayEndlessModeView Postfix called");

            for (int i = 0; i < __instance.m_endlessModeData.RewardList.Count; i++)
            {
                var reward = __instance.m_endlessModeData.RewardList[i].Reward;
                Logger.LogDebug(
                    $"Reward {i} - category: {reward.Category}, type: {reward.RewardType}"
                );

                if (reward.Weapons != null && reward.Weapons.Configuration != null)
                    Logger.LogDebug(
                        $"Reward {i} weapon - type: {reward.Weapons.Type}, config: {reward.Weapons.Configuration.WeaponAbilityType}"
                    );

                if (
                    reward.WeaponCurseProperties != null
                    && reward.WeaponCurseProperties.Bullet != null
                )
                    Logger.LogDebug(
                        $"Reward {i} weapon - type: {reward.WeaponCurseProperties.WeaponGroup}, group: {reward.WeaponCurseProperties.WeaponGroup}, attackId: {reward.WeaponCurseProperties.Bullet.AttackID.GetValue(true)}"
                    );
            }
        }
    }

}
