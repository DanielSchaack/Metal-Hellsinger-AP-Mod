using System.IO;
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
        public const string Game = "Metal: Hellsinger";
        public const string ModInfo = $"{PluginInfo.NAME} v{PluginInfo.VERSION}";

        public static ItemTracker ItemTracker;
        public static LocationTracker LocationTracker;
        public static SceneTracker SceneTracker;
        public static IngameDispenser IngameDispenser;
        public static Configuration Configuration;
        public static Settings Settings;
        public static ArchipelagoIntegration Archipelago;

        public static float SceneActiveTime { get; set; } = 0f;
        public static float LevelActiveTime { get; set; } = 0f;
        public static float TimeSinceLastDeathlink { get; set; } = 0f;

        public static bool IsLoadingDefinition
        {
            get;
            set
            {
                Logger.LogInfo($"IsLoadingDefinition - Current: {field} | Incoming: {value}");
                field = value;
            }
        } = false;

        public static bool IsLoadingHellsSelection
        {
            get;
            set
            {
                Logger.LogInfo($"IsLoadingHellsSelection - Current: {field} | Incoming: {value}");
                field = value;
            }
        } = false;

        public static bool IsLoadingHells
        {
            get;
            set
            {
                Logger.LogInfo($"IsLoadingHells - Current: {field} | Incoming: {value}");
                field = value;
            }
        } = false;

        public static bool IsLoadingEndless
        {
            get;
            set
            {
                Logger.LogInfo($"IsLoadingEndless - Current: {field} | Incoming: {value}");
                field = value;
            }
        } = false;

        public static bool IsLoadingSongs
        {
            get;
            set
            {
                Logger.LogInfo($"IsLoadingSongs - Current: {field} | Incoming: {value}");
                field = value;
            }
        } = false;

        public static bool IsFinalLevel
        {
            get;
            set
            {
                Logger.LogInfo($"IsFinalLevel - Current: {field} | Incoming: {value}");
                field = value;
            }
        } = false;

        public static bool IsPaused
        {
            get;
            set
            {
                Logger.LogInfo($"IsPaused - Current: {field} | Incoming: {value}");
                field = value;
            }
        } = true;

        public static EGameMode CurrentGameMode
        {
            get;
            set
            {
                Logger.LogInfo($"CurrentGameMode - Current: {field} | Incoming: {value}");
                field = value;
            }
        } = EGameMode.None;

        public static EDifficulty CurrentDifficulty
        {
            get;
            set
            {
                Logger.LogInfo($"CurrentDifficulty - Current: {field} | Incoming: {value}");
                field = value;
            }
        } = EDifficulty.Easy;

        public static GameStateController.GameStateName CurrentGameState
        {
            get;
            set
            {
                Logger.LogInfo($"CurrentGameState - Current: {field} | Incoming: {value}");
                field = value;
            }
        } = GameStateController.GameStateName.Title;

        public static string CurrentLevel
        {
            get;
            set
            {
                Logger.LogInfo($"CurrentLevel - Current: {field} | Incoming: {value}");
                field = value;
            }
        } = "";

        public static PlayerWeaponType CurrentPrimary
        {
            get;
            set
            {
                Logger.LogInfo($"CurrentPrimary - Current: {field} | Incoming: {value}");
                field = value;
            }
        } = PlayerWeaponType.None;

        public static PlayerWeaponType CurrentSecondary
        {
            get;
            set
            {
                Logger.LogInfo($"CurrentSecondary - Current: {field} | Incoming: {value}");
                field = value;
            }
        } = PlayerWeaponType.None;

        public static SkinType CurrentOutfit
        {
            get;
            set
            {
                Logger.LogInfo($"CurrentOutfit - Current: {field} | Incoming: {value}");
                field = value;
            }
        } = SkinType.None;

        public static string CurrentMainSong
        {
            get;
            set
            {
                Logger.LogInfo($"CurrentMainSong - Current: {field} | Incoming: {value}");
                field = value;
            }
        } = "";

        public static string CurrentBossSong
        {
            get;
            set
            {
                Logger.LogInfo($"CurrentBossSong - Current: {field} | Incoming: {value}");
                field = value;
            }
        } = "";

        public static EDifficulty SelectedDifficulty
        {
            get;
            set
            {
                Logger.LogInfo($"SelectedDifficulty - Current: {field} | Incoming: {value}");
                field = value;
            }
        } = EDifficulty.Easy;

        public static ExtendedWeaponType CurrentPersephoneConfig
        {
            get;
            set
            {
                Logger.LogInfo($"CurrentPersephoneConfig - Current: {field} | Incoming: {value}");
                field = value;
            }
        } = ExtendedWeaponType.Regular;

        public static WeaponType CurrentHoundsConfig
        {
            get;
            set
            {
                Logger.LogInfo($"CurrentHoundsConfig - Current: {field} | Incoming: {value}");
                field = value;
            }
        } = WeaponType.Regular;

        public static WeaponType CurrentVulcanConfig
        {
            get;
            set
            {
                Logger.LogInfo($"CurrentVulcanConfig - Current: {field} | Incoming: {value}");
                field = value;
            }
        } = WeaponType.Regular;

        private static bool shouldExportLocations = false;
        private static bool shouldExportItems = false;

        public override void Load()
        {
            Logger.SetLogger(Log);
            Logger.LogInfo($"{ModInfo} loaded!");

            ItemTracker = new ItemTracker();
            LocationTracker = new LocationTracker();
            SceneTracker = new SceneTracker();
            Configuration = new Configuration(Config);
            Settings = new Settings();

            try
            {
                Logger.LogInfo("Creating GameObjects");

                Archipelago = RegisterTypeAndCreateObject<ArchipelagoIntegration>("ArchipelagoIntegration");
                RegisterTypeAndCreateObject<ArchipelagoConsole>("AP Console");
                RegisterTypeAndCreateObject<ArchipelagoConnectorGui>("AP Connector UI");

                IngameDispenser = RegisterTypeAndCreateObject<IngameDispenser>("IngameDispenser");
                RegisterTypeAndCreateObject<SceneTracker>("SceneTracker");

                Application.runInBackground = !Randomizer
                    .Configuration
                    .hellsingerPauseGameOutOfFocused
                    .Value;

                Logger.LogInfo("Objects initialized");

                if(shouldExportLocations)
                {
                    string outputPath = Path.Combine(BepInEx.Paths.ConfigPath, "location_region_mapping.py");

                    LocationMappingExporter.ExportLocationRegionMapping(
                        Locations.LocationDataByName,
                        outputPath
                    );
                    Logger.LogInfo($"Locations available at: {outputPath}");
                }

                if(shouldExportItems)
                {
                    string outputPath = Path.Combine(BepInEx.Paths.ConfigPath, "item_dict.py");

                    ItemExporter.ExportItemTable(
                        Items.ItemDataById,
                        outputPath
                    );
                    Logger.LogInfo($"Items available at: {outputPath}");
                }

            }
            catch (System.Exception ex)
            {
                Logger.LogError($"Failed to create objects: {ex.Message}");
                Logger.LogError(ex.StackTrace);
            }

            Harmony harmony = new Harmony(PluginInfo.GUID);
            harmony.PatchAll();
        }

        public static T RegisterTypeAndCreateObject<T>(string objectName)
            where T : MonoBehaviour
        {
            ClassInjector.RegisterTypeInIl2Cpp<T>();

            GameObject go = new GameObject(objectName);
            Object.DontDestroyOnLoad(go);
            go.hideFlags = HideFlags.HideAndDontSave;

            return go.AddComponent<T>();
        }

        public static T RegisterTypeAndCreateObjectWithCollider<T>(string objectName)
            where T : MonoBehaviour
        {
            GameObject go = new GameObject(objectName);
            Object.DontDestroyOnLoad(go);
            go.hideFlags = HideFlags.HideAndDontSave;
            go.AddComponent<SphereCollider>();

            return go.AddComponent<T>();
        }

        public static bool AreItemsDispensible()
        {
            return Randomizer.CurrentGameState == GameStateController.GameStateName.InGame
                && !Randomizer.IsLoadingHellsSelection
                && Randomizer.LevelActiveTime > 10f
                && !Randomizer.IsPaused;
        }

    }
}
