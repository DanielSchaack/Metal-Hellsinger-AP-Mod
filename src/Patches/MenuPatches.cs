using HarmonyLib;

namespace Randomizer
{
    [HarmonyPatch(typeof(TitleState))]
    public class TitleStatePatches
    {
        public static TitleState Instance;

        [HarmonyPrefix]
        [HarmonyPatch(nameof(TitleState.CreateTitleScreen))]
        static bool CreateTitleScreenPrefix(TitleState __instance)
        {
            Logger.LogInfo($"TitleState CreateTitleScreen Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(TitleState.CreateTitleScreen))]
        static void CreateTitleScreenPostfix(ref TitleState __instance)
        {
            Logger.LogInfo($"TitleState CreateTitleScreen Postfix called");
            if (Instance == null)
                Instance = __instance;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(TitleState.OpenTitleScreen))]
        static bool OpenTitleScreenPrefix(TitleState __instance, bool skipLogo)
        {
            Logger.LogInfo($"TitleState OpenTitleScreen Prefix called and skips logo: {skipLogo}");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(TitleState.OpenTitleScreen))]
        static void OpenTitleScreenPostfix(TitleState __instance)
        {
            Logger.LogInfo($"TitleState OpenTitleScreen Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(TitleState.OpenMainMenu))]
        static bool OpenMainMenuPrefix(TitleState __instance)
        {
            Logger.LogInfo($"TitleState OpenMainMenu Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(TitleState.OpenMainMenu))]
        static void OpenMainMenuPostfix(TitleState __instance)
        {
            Logger.LogInfo($"TitleState OpenMainMenu Postfix called");
            if (__instance.m_endlessLobbyController != null)
                Logger.LogInfo($"TitleState Has endless lobby controller");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(TitleState.OnMenuOptionSelected))]
        static bool OnMenuOptionSelectedPrefix(TitleState __instance)
        {
            Logger.LogInfo($"TitleState OnMenuOptionSelected Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(TitleState.OnMenuOptionSelected))]
        static void OnMenuOptionSelectedPostfix(TitleState __instance)
        {
            Logger.LogInfo($"TitleState OnMenuOptionSelected Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(TitleState.GetMenuItems))]
        static bool GetMenuItemsPrefix(TitleState __instance)
        {
            // Logger.LogInfo($"TitleState GetMenuItems Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(TitleState.GetMenuItems))]
        static void GetMenuItemsPostfix(TitleState __instance)
        {
            // Logger.LogInfo($"TitleState GetMenuItems Postfix called");
        }
    }

    [HarmonyPatch(typeof(CompanionController))]
    public class CompanionControllerPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(CompanionController.CreateCompanion))]
        static bool CreateCompanionPrefix(CompanionController __instance)
        {
            Logger.LogInfo($"CompanionController CreateCompanion Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(CompanionController.CreateCompanion))]
        static void CreateCompanionPostfix(CompanionController __instance)
        {
            Logger.LogInfo($"CompanionController CreateCompanion Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(CompanionController.Show))]
        static bool ShowPrefix(CompanionController __instance)
        {
            Logger.LogInfo($"CompanionController Show Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(CompanionController.Show))]
        static void ShowPostfix(CompanionController __instance)
        {
            Logger.LogInfo($"CompanionController Show Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(CompanionController.IsAnyCompanionItemUnviewed))]
        static bool IsAnyCompanionItemUnviewedPrefix(CompanionController __instance)
        {
            Logger.LogInfo($"CompanionController IsAnyCompanionItemUnviewed Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(CompanionController.IsAnyCompanionItemUnviewed))]
        static void IsAnyCompanionItemUnviewedPostfix(CompanionController __instance)
        {
            Logger.LogInfo($"CompanionController IsAnyCompanionItemUnviewed Postfix called");
        }
    }
}
