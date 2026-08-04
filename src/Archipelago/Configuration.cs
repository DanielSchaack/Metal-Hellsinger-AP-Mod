using BepInEx.Configuration;
using UnityEngine;
using static Randomizer.Lookup;
using static Randomizer.Lookup.OutfitId;
using static Randomizer.Lookup.SongId;
using static Randomizer.ArchipelagoIntegration;

namespace Randomizer
{
    public class Configuration
    {
        public ConfigFile config;

        public ConfigEntry<string> archipelagoUri;
        public ConfigEntry<string> archipelagoUsername;
        public ConfigEntry<string> archipelagoPassword;
        public ConfigEntry<bool> archipelagoConsoleEnabled;
        public ConfigEntry<DeathLinkType> archipelagoDeathlinkType;

        internal ConfigEntry<bool> gameplayInvisibleWeaponsActive;
        internal ConfigEntry<bool> gameplayWeaponTrickeryModeActive;
        internal ConfigEntry<bool> gameplayDoubletimeActive;
        internal ConfigEntry<bool> gameplayHalftimeActive;

        internal ConfigEntry<bool> weaponLoadAllAvailableWeapons;
        internal ConfigEntry<bool> weaponExcludePazFromLoadout;
        internal ConfigEntry<bool> weaponExcludeTerminusFromLoadout;
        internal ConfigEntry<bool> weaponRandomizePersephoneType;
        internal ConfigEntry<ExtendedWeaponType> weaponPersephoneType;
        internal ConfigEntry<bool> weaponRandomizeHoundsType;
        internal ConfigEntry<WeaponType> weaponHoundsType;
        internal ConfigEntry<bool> weaponRandomizeVulcanType;
        internal ConfigEntry<WeaponType> weaponVulcanType;

        internal ConfigEntry<bool> skinsAutoSetWeaponSkin;
        internal ConfigEntry<bool> skinsRandomizeOutfits;
        internal ConfigEntry<bool> skinsPrioritizeNewOutfits;
        internal ConfigEntry<OutfitId> skinsOutfitsToInclude;

        internal ConfigEntry<bool> songsRandomizeMainSongs;
        internal ConfigEntry<bool> songsRandomizeMainSongsInHellsSelect;
        internal ConfigEntry<bool> songsRandomizeBossSongs;
        internal ConfigEntry<bool> songsRandomizeBossSongsInHellsSelect;
        internal ConfigEntry<bool> songsRestrictAndEnforceNoTomorrowToOnlyTheFinalBoss;
        internal ConfigEntry<bool> songsRandomizeSongsInTutorialAndTorments;
        internal ConfigEntry<bool> songsApplyBossSongFilterForPrioritizedSongs;
        internal ConfigEntry<bool> songsPrioritizeNewSongs;
        internal ConfigEntry<SongId> songsMainSongsToInclude;
        internal ConfigEntry<SongId> songsBossSongsToInclude;

        public ConfigEntry<bool> hellsingerPauseGameOutOfFocused;

        public Configuration(ConfigFile config)
        {
            this.config = config;
            archipelagoUri = config.Bind(
                "Archipelago",
                "ArchipelagoServerUri",
                "archipelago.gg:38281",
                "Archipelago room server URI."
            );
            archipelagoUri.SettingChanged += (sender, args) =>
            {
                ArchipelagoConnectorGui.archipelagoUri = archipelagoUri.Value;
                config.Save();
            };
            ;

            archipelagoUsername = config.Bind(
                "Archipelago",
                "ArchipelagoPlayerName",
                "",
                "Archipelago slot player name."
            );
            archipelagoUsername.SettingChanged += (sender, args) =>
            {
                ArchipelagoConnectorGui.archipelagoUsername = archipelagoUsername.Value;
                config.Save();
            };

            archipelagoPassword = config.Bind(
                "Archipelago",
                "ArchipelagoRoomPassword",
                "",
                "Archipelago room password."
            );
            archipelagoPassword.SettingChanged += (sender, args) =>
            {
                ArchipelagoConnectorGui.archipelagoPassword = archipelagoPassword.Value;
            };

            archipelagoConsoleEnabled = config.Bind(
                "Archipelago.Console",
                "ArchipelagoConsoleEnabled",
                true,
                "En/Disable the archipelago itemfeed."
            );
            archipelagoConsoleEnabled.SettingChanged += addOnChangeSave(config);

            archipelagoDeathlinkType = config.Bind(
                "Archipelago.Override",
                "ArchipelagoDeathlinkType",
                DeathLinkType.Death,
                "Overrides the slot's deathlink settings for deathlink type.\n'Death' applies immediately.\n'Trap' queues the death as a trap"
            );
            archipelagoDeathlinkType.SettingChanged += (sender, args) =>
            {
                Randomizer.Archipelago.CheckDeathlink();
                addOnChangeSave(config);
            };

            // ---

            gameplayInvisibleWeaponsActive = config.Bind(
                "Hellsinger.Gameplay",
                "InvisibleWeaponsActive",
                false,
                "Turns most weapons invisble."
            );
            gameplayInvisibleWeaponsActive.SettingChanged += addOnChangeSave(config);

            gameplayWeaponTrickeryModeActive = config.Bind(
                "Hellsinger.Gameplay",
                "WeaponTrickeryModeActive",
                false,
                "Turns any level into the Weapon Trickery challenge.\nSwaps weapons on kill while disabling manual weapon swaps."
            );
            gameplayWeaponTrickeryModeActive.SettingChanged += addOnChangeSave(config);

            gameplayDoubletimeActive = config.Bind(
                "Hellsinger.Gameplay",
                "DoubletimeActive",
                false,
                "Increases gamespeed without increasing the speed of the music.\nSee the trap settings to adjust the speed.\nThis takes precedence over Halftime."
            );
            gameplayDoubletimeActive.SettingChanged += addOnChangeSave(config);

            gameplayHalftimeActive = config.Bind(
                "Hellsinger.Gameplay",
                "HalftimeActive",
                false,
                "Decreases gamespeed without decreasing the speed of the music.\nSee the trap settings to adjust the speed.\nThis is ignored while Doubletime is active."
            );
            gameplayHalftimeActive.SettingChanged += addOnChangeSave(config);

            // ---

            weaponLoadAllAvailableWeapons = config.Bind(
                "Hellsinger.Weapons",
                "LoadAllAvailableWeapons",
                false,
                "Load all unlocked weapons beyond the visibly shown ones.\nUse Next/Previous Weapon to scroll through them all!\nAll loaded weapons will be used for the Weapon Trickery toggle/trap."
            );
            weaponLoadAllAvailableWeapons.SettingChanged += addOnChangeSave(config);

            weaponExcludePazFromLoadout = config.Bind(
                "Hellsinger.Weapons",
                "ExcludePazFromLoadout",
                false,
                "Excludes Paz from the loadout.\nThis option is ignored if Paz is selected as the primary/secondary Weapon."
            );
            weaponExcludePazFromLoadout.SettingChanged += addOnChangeSave(config);

            weaponExcludeTerminusFromLoadout = config.Bind(
                "Hellsinger.Weapons",
                "ExcludeTerminusFromLoadout",
                false,
                "Excludes Terminus from the loadout.\nThis option is ignored if Terminus is selected as the primary/secondary Weapon."
            );
            weaponExcludeTerminusFromLoadout.SettingChanged += addOnChangeSave(config);

            weaponRandomizePersephoneType = config.Bind(
                "Hellsinger.Weapons",
                "RandomizePersephoneType",
                false,
                "Randomizes Persephones equipped type, chosen from the unlocked types.\nThis takes precedence over the chosen type.\nThe randomization prioritizes missing locations."
            );
            weaponRandomizePersephoneType.SettingChanged += addOnChangeSave(config);

            weaponPersephoneType = config.Bind(
                "Hellsinger.Weapons",
                "PersephoneType",
                ExtendedWeaponType.Regular,
                "Sets Persephones equipped type.\nIf the option isn't unlocked it will fall back to an unlocked one, while prioritizing the regular type."
            );
            weaponPersephoneType.SettingChanged += addOnChangeSave(config);

            weaponRandomizeHoundsType = config.Bind(
                "Hellsinger.Weapons",
                "RandomizeHoundType",
                false,
                "Randomizes Hounds equipped type, chosen from the unlocked types.\nThis takes precedence over the chosen type.\nThe randomization prioritizes missing locations."
            );
            weaponRandomizeHoundsType.SettingChanged += addOnChangeSave(config);

            weaponHoundsType = config.Bind(
                "Hellsinger.Weapons",
                "HoundsType",
                WeaponType.Regular,
                "Sets Hounds equipped type.\nIf the option isn't unlocked it will fall back to an unlocked one, while prioritizing the regular type."
            );
            weaponHoundsType.SettingChanged += addOnChangeSave(config);

            weaponRandomizeVulcanType = config.Bind(
                "Hellsinger.Weapons",
                "RandomizeHoundType",
                false,
                "Randomizes Vulcans equipped type, chosen from the unlocked types.\nThis takes precedence over the chosen type.\nThe randomization prioritizes missing locations."
            );
            weaponRandomizeVulcanType.SettingChanged += addOnChangeSave(config);

            weaponVulcanType = config.Bind(
                "Hellsinger.Weapons",
                "VulcanType",
                WeaponType.Regular,
                "Sets Vulcans equipped type.\nIf the option isn't unlocked it will fall back to an unlocked one, while prioritizing the regular type."
            );
            weaponVulcanType.SettingChanged += addOnChangeSave(config);
            // ---

            skinsAutoSetWeaponSkin = config.Bind(
                "Hellsinger.Skins",
                "AutoSetWeaponSkin",
                true,
                "Automatically sets the weapon skin to its corrupted version if unlocked."
            );
            skinsAutoSetWeaponSkin.SettingChanged += addOnChangeSave(config);

            skinsRandomizeOutfits = config.Bind(
                "Hellsinger.Skins",
                "RandomizeOutfits",
                false,
                "Randomizes available outfits.\nWarning: This overwrites the selected outfit from the chosen loadout."
            );
            skinsRandomizeOutfits.SettingChanged += addOnChangeSave(config);

            skinsPrioritizeNewOutfits = config.Bind(
                "Hellsinger.Skins",
                "PrioritizeNewOutfits",
                false,
                "Prioritizes outfits that didn't complete their checks yet."
            );
            skinsPrioritizeNewOutfits.SettingChanged += addOnChangeSave(config);

            skinsOutfitsToInclude = config.Bind(
                "Hellsinger.Skins",
                "OutfitsToInclude",
                TheUnknown
                | LeviathanOutfit
                | DarkDevotee
                | MorningStar
                | AngelEyes
                | Obsidian
                | Amethyst
                | Chromatica,
                "Unless otherwise prioritized, include the chosen Outfits for the randomization.\nIf no outfits remain after applying this filter, then all unlocked outfits are chosen for randomization\n! Important: This still requires the respective DLCs to play their outfits !"
            );
            skinsOutfitsToInclude.SettingChanged += addOnChangeSave(config);

            // ---

            songsRandomizeMainSongs = config.Bind(
                "Hellsinger.Songs",
                "RandomizeMainSongs",
                false,
                "Randomizes unlocked main songs without teasing the song in the Hells select screen."
            );
            songsRandomizeMainSongs.SettingChanged += addOnChangeSave(config);

            songsRandomizeMainSongsInHellsSelect = config.Bind(
                "Hellsinger.Songs",
                "RandomizeMainSongsInHellsSelect",
                false,
                "Randomizes the main song previews on the Hells select screen.\nInfo: While active, the song in the select screen takes precedence over RandomizeMainSongs."
            );
            songsRandomizeMainSongsInHellsSelect.SettingChanged += addOnChangeSave(config);

            songsRandomizeBossSongs = config.Bind(
                "Hellsinger.Songs",
                "RandomizeBossSongs",
                false,
                "Randomizes unlocked boss songs without teasing the song in the Hells select screen."
            );
            songsRandomizeBossSongs.SettingChanged += addOnChangeSave(config);

            songsRandomizeBossSongsInHellsSelect = config.Bind(
                "Hellsinger.Songs",
                "RandomizeBossSongsInHellsSelect",
                false,
                "Randomizes the boss song previews on the Hells select screen.\nInfo: While active, the song in the select screen takes precedence over RandomizeBossSongs."
            );
            songsRandomizeBossSongsInHellsSelect.SettingChanged += addOnChangeSave(config);

            songsRestrictAndEnforceNoTomorrowToOnlyTheFinalBoss = config.Bind(
                "Hellsinger.Songs",
                "RestrictNoTomorrowToOnlyTheFinalBoss",
                false,
                "If the song is unlocked, restricts the boss song 'No Tomorrow' to only play for the boss fight in Sheol."
            );
            songsRestrictAndEnforceNoTomorrowToOnlyTheFinalBoss.SettingChanged += addOnChangeSave(config);

            songsRandomizeSongsInTutorialAndTorments = config.Bind(
                "Hellsinger.Songs",
                "RandomizeSongsInTutorialAndTorments",
                false,
                "Randomizes songs inside of the tutorial and inside of torments(/challenges).\nInfo: Torments were not balanced around faster/slower songs. This option may make the torments easier/harder depending on the song."
            );
            songsRandomizeSongsInTutorialAndTorments.SettingChanged += addOnChangeSave(config);

            songsPrioritizeNewSongs = config.Bind(
                "Hellsinger.Songs",
                "PrioritizeNewSongs",
                false,
                "Prioritizes songs that didn't complete their checks yet."
            );
            songsPrioritizeNewSongs.SettingChanged += addOnChangeSave(config);

            songsApplyBossSongFilterForPrioritizedSongs = config.Bind(
                "Hellsinger.Songs",
                "ApplyBossSongFilterForPrioritizedSongs",
                false,
                "Applies the filter BossSongsToInclude for the missing check songs."
            );
            songsApplyBossSongFilterForPrioritizedSongs.SettingChanged += addOnChangeSave(config);

            songsMainSongsToInclude = config.Bind(
                "Hellsinger.Songs",
                "MainSongsToInclude",
                ThisIsTheEnd
                | Stygia
                | BurialAtNight
                | ThisDevastation
                | PoetryOfCinder
                | Dissolution
                | Acheron
                | SilentNoMore
                | LeviathanSong
                | DreamOfTheBeast
                | SwallowTheFire
                | MouthOfHell
                | GoodbyeMorningStar
                | DownWithTheSickness
                | Uprising
                | MiseryBusiness
                | Tsunami_OriginalMix
                | Runaway_UI
                | FeelGoodInc
                | ILoveIt
                | PersonalJesus
                | DepartureToDestruction
                | HandCannon
                | BurnInHell
                | MurderMachineInc
                | Endless
                | MineControl
                | Sacrifice
                | ErebusReaction
                | BleedingOut,
                "Unless otherwise prioritized, include the chosen songs for the randomization of the main songs.\nIf no songs remain after applying this filter, then all unlocked songs are chosen for randomization\n! Important: This still requires the respective DLCs to play their songs !"
            );
            songsMainSongsToInclude.SettingChanged += addOnChangeSave(config);

            songsBossSongsToInclude = config.Bind(
                "Hellsinger.Songs",
                "BossSongsToInclude",
                BloodAndLaw
                | InfernalInvocation1_HopesAndFears
                | InfernalInvocation2_Defiance
                | InfernalInvocation3_DreamingInDistortion
                | NoTomorrow,
                "Unless otherwise prioritized, include the chosen songs for the randomization of the boss songs.\nMain songs like 'ThereIsNoEnd' are ignored.\nIf no songs remain after applying this filter, then all unlocked songs are chosen for randomization\n! Important: This still requires the respective DLCs to play their songs !"
            );
            songsBossSongsToInclude .SettingChanged += addOnChangeSave(config);

            // ---

            hellsingerPauseGameOutOfFocused = config.Bind(
                "Hellsinger",
                "HellsingerPauseGameOutOfFocused",
                true,
                "Pauses the game while it is unfocused.\nLets the GPU idle while unfocused.\n! May be less stable than the ingame's option !"
            );
            hellsingerPauseGameOutOfFocused.SettingChanged += (sender, args) =>
            {
                Application.runInBackground = !hellsingerPauseGameOutOfFocused.Value;
                Logger.LogInfo(
                    "Set run in runInBackground to "
                        + (!hellsingerPauseGameOutOfFocused.Value).ToString()
                );
                config.Save();
            };

        }

        private static System.EventHandler addOnChangeSave(ConfigFile config)
        {
            return (sender, args) =>
            {
                config.Save();
            };
        }
    }
}
