using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using UnityEngine;
using static Randomizer.Lookup;

namespace Randomizer
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.NAME, PluginInfo.VERSION)]
    public class Randomizer : BasePlugin
    {
        public const string Game = "MetalHellsinger";
        public const string ModInfo = $"{PluginInfo.NAME} v{PluginInfo.VERSION}";

        public static ItemTracker ItemTracker;
        public static LocationTracker LocationTracker;
        public static SceneTracker SceneTracker;
        public static IngameDispenser IngameDispenser;
        public static Configuration Configuration;
        public static Settings Settings;
        public static ArchipelagoIntegration Archipelago;

        public static bool IsLoadingDefinition = false; // Flag for loading LevelDefinition
        public static bool IsLoadingHells = false; //Flag for in title loading
        public static bool IsLoadingSongs = false; // Flag for Hells/Torment loading
        public static bool IsFinalLevel = false; // Flag for Sheol
        public static bool IsPaused = false; // Flag for ingame menu open
        public static float SceneActiveTime = 0f; // Time since scene has been loaded
        public static float LevelActiveTime = 0f; // Time since when loading screen play button has been pressed

        public static EGameMode CurrentGameMode = EGameMode.None;
        public static EDifficulty CurrentDifficulty = EDifficulty.Easy;
        public static GameStateController.GameStateName CurrentGameState = GameStateController .GameStateName .Title;

        public static string CurrentLevel = "";
        public static PlayerWeaponType CurrentPrimary = PlayerWeaponType.None;
        public static PlayerWeaponType CurrentSecondary = PlayerWeaponType.None;
        public static SkinType CurrentOutfit = SkinType.None;
        public static string CurrentMainSong = "";
        public static string CurrentBossSong = "";
        public static EDifficulty SelectedDifficulty = EDifficulty.Easy; // selected Difficulty during Hells select screen

        public static ExtendedWeaponType CurrentPersephoneConfig = ExtendedWeaponType.Regular;
        public static WeaponType CurrentHoundsConfig = WeaponType.Regular;
        public static WeaponType CurrentVulcanConfig = WeaponType.Regular;

        public override void Load()
        {
            Logger.SetLogger(Log);
            Logger.LogInfo($"{ModInfo} loaded!");

            ItemTracker = new ItemTracker();
            LocationTracker = new LocationTracker();
            SceneTracker = new SceneTracker();
            IngameDispenser = new IngameDispenser();
            Configuration = new Configuration(Config);
            Settings = new Settings();
            Archipelago = new ArchipelagoIntegration();

            try
            {
                Logger.LogInfo("Creating GameObjects");

                RegisterTypeAndCreateObject<ArchipelagoIntegration>("ArchipelagoIntegration");
                RegisterTypeAndCreateObject<ArchipelagoConsole>("AP Console");
                RegisterTypeAndCreateObject<ArchipelagoConnectorGui>("AP Connector UI");

                RegisterTypeAndCreateObject<IngameDispenser>("IngameDispenser");
                RegisterTypeAndCreateObject<SceneTracker>("SceneTracker");

                Application.runInBackground = !Configuration.hellsingerPauseGameOutOfFocused.Value;
                Logger.LogInfo("Objects initialized");
            }
            catch (System.Exception ex)
            {
                Logger.LogError($"Failed to create objects: {ex.Message}");
                Logger.LogError(ex.StackTrace);
            }

            Harmony harmony = new Harmony(PluginInfo.GUID);
            harmony.PatchAll();
        }

        private T RegisterTypeAndCreateObject<T>(string objectName)
            where T : MonoBehaviour
        {
            ClassInjector.RegisterTypeInIl2Cpp<T>();

            GameObject go = new GameObject(objectName);
            Object.DontDestroyOnLoad(go);
            go.hideFlags = HideFlags.HideAndDontSave;

            return go.AddComponent<T>();
        }
    }
}
