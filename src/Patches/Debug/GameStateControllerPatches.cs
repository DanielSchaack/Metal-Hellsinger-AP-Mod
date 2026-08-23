using HarmonyLib;

namespace Randomizer
{
    [HarmonyPatch(typeof(GameStateController))]
    public class GameStateControllerPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(GameStateController.LoadingCompleted))]
        static bool LoadingCompletedPrefix(GameStateController __instance)
        {
            Logger.LogDebug("GameStateController LoadingCompleted Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(GameStateController.LoadingCompleted))]
        static void LoadingCompletedPostfix(GameStateController __instance)
        {
            Logger.LogDebug("GameStateController LoadingCompleted Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(GameStateController.DisableStateUpdating))]
        static bool DisableStateUpdatingPrefix()
        {
            Logger.LogDebug($"GameStateController DisableStateUpdating Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(GameStateController.DisableStateUpdating))]
        static void DisableStateUpdatingPostfix()
        {
            Logger.LogDebug($"GameStateController DisableStateUpdating Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(GameStateController.EnableStateUpdating))]
        static bool EnableStateUpdatingPrefix()
        {
            Logger.LogDebug($"GameStateController EnableStateUpdating Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(GameStateController.EnableStateUpdating))]
        static void EnableStateUpdatingPostfix()
        {
            Logger.LogDebug($"GameStateController EnableStateUpdating Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(GameStateController.PreExitEvent))]
        static bool PreExitEventPrefix()
        {
            Logger.LogDebug($"GameStateController PreExitEvent Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(GameStateController.PreExitEvent))]
        static void PreExitEventPostfix()
        {
            Logger.LogDebug($"GameStateController PreExitEvent Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(GameStateController.SetupGameStates))]
        static bool SetupGameStatesPrefix(ContextProvider baseContext)
        {
            Logger.LogDebug($"GameStateController SetupGameStates Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(GameStateController.SetupGameStates))]
        static void SetupGameStatesPostfix(ContextProvider baseContext)
        {
            Logger.LogDebug($"GameStateController SetupGameStates Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(GameStateController.ExitCurrentState))]
        static bool ExitCurrentStatePrefix()
        {
            Logger.LogDebug($"GameStateController ExitCurrentState Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(GameStateController.ExitCurrentState))]
        static void ExitCurrentStatePostfix()
        {
            Logger.LogDebug($"GameStateController ExitCurrentState Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(GameStateController.TellStateSceneIsReady))]
        static bool TellStateSceneIsReadyPrefix(LevelDefinition levelDefinition)
        {
            Logger.LogDebug(
                $"GameStateController TellStateSceneIsReady Prefix called with level {levelDefinition.ID} in state {levelDefinition.State}"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(GameStateController.TellStateSceneIsReady))]
        static void TellStateSceneIsReadyPostfix(LevelDefinition levelDefinition)
        {
            Logger.LogDebug(
                $"GameStateController TellStateSceneIsReady Postfix called with level {levelDefinition.ID} in state {levelDefinition.State}"
            );
        }
    }
}
