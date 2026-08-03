using HarmonyLib;
using Il2CppInterop.Runtime.InteropTypes.Arrays;

namespace Randomizer
{
    [HarmonyPatch(typeof(Main))]
    public class MainPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(Main.Awake))]
        static bool AwakePrefix(Main __instance)
        {
            Main.DisplayDebugInfo = true;
            Logger.LogInfo("Main Awake Prefix called");
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(Main.LoadLevel))]
        static bool LoadLevelPrefix(
            Main __instance,
            LevelDefinition levelDefinition,
            bool showLoadingScreen,
            Il2CppReferenceArray<Il2CppSystem.Object> stateParameters
        )
        {
            Logger.LogInfo(
                $"Main LoadLevel Prefix called, loading {levelDefinition.ID} with loading screen: {showLoadingScreen}"
            );

            Randomizer.IsFinalLevel = levelDefinition.IsFinalLevel;
            Logger.LogInfo($"Is Final Level: {Randomizer.IsFinalLevel}");

            Randomizer.CurrentGameMode = levelDefinition.gameplayInfo.GameMode;
            Logger.LogInfo($"Current Game Mode set to: {Randomizer.CurrentGameMode}");

            Randomizer.CurrentLevel = levelDefinition.ID;
            Logger.LogInfo($"Current Level set to: {Randomizer.CurrentLevel}");

            Randomizer.CurrentGameState = levelDefinition.State;
            Logger.LogInfo($"Current Game State set to: {Randomizer.CurrentGameState}");

            if (stateParameters != null)
            {
                foreach (Il2CppSystem.Object parameter in stateParameters)
                {
                    string typeName = parameter.GetIl2CppType().FullNameOrDefault;
                    Logger.LogInfo($"Main Entering level with parameter {typeName}");
                    if (typeName.Contains("EDifficulty"))
                    {
                        EDifficulty currentDifficulty = parameter.Unbox<EDifficulty>();
                        Randomizer.CurrentDifficulty = currentDifficulty;
                        Logger.LogInfo($"Current Difficulty set to: {Randomizer.CurrentDifficulty}");
                    }
                }
            }

            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(Main.LoadLevel))]
        static void LoadLevelPostfix(
            Main __instance,
            LevelDefinition levelDefinition,
            bool showLoadingScreen,
            Il2CppReferenceArray<Il2CppSystem.Object> stateParameters
        )
        {
            Logger.LogInfo(
                $"Main LoadLevel Postfix called, loading {levelDefinition.ID} with loading screen: {showLoadingScreen}"
            );
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(Main.OpenLoadingView))]
        static bool OpenLoadingViewPrefix(
            Main __instance,
            LevelDefinition levelDefinition,
            Il2CppReferenceArray<Il2CppSystem.Object> stateParameters,
            Il2CppSystem.Action onLoadingViewClosed
        )
        {
            Logger.LogInfo($"Main OpenLoadingView Prefix called, loading {levelDefinition.ID}");
            if (stateParameters != null)
            {
                foreach (Il2CppSystem.Object parameter in stateParameters)
                    Logger.LogInfo(
                        $"Main Entering level with parameter {parameter.GetIl2CppType().FullNameOrDefault}"
                    );
            }
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(Main.OpenLoadingView))]
        static void OpenLoadingViewPostfix(
            Main __instance,
            LevelDefinition levelDefinition,
            Il2CppReferenceArray<Il2CppSystem.Object> stateParameters,
            Il2CppSystem.Action onLoadingViewClosed
        )
        {
            Logger.LogInfo($"Main OpenLoadingView Postfix called, loading {levelDefinition.ID}");
        }
    }
}
