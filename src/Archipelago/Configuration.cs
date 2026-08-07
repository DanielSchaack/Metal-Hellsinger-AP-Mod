using BepInEx.Configuration;
using static Randomizer.Lookup;
using static Randomizer.Lookup.OutfitId;
using static Randomizer.Lookup.SongId;
using static Randomizer.ArchipelagoIntegration;
using UnityEngine;

namespace Randomizer
{
    public class Configuration
    {
        public ConfigFile config;

        internal ConfigEntry<bool> hellsingerPauseGameOutOfFocused;

        internal ConfigEntry<string> archipelagoUri;
        internal ConfigEntry<string> archipelagoUsername;
        internal ConfigEntry<string> archipelagoPassword;
        internal ConfigEntry<bool> archipelagoConsoleEnabled;
        internal ConfigEntry<DeathLinkType> archipelagoDeathlinkType;
        internal ConfigEntry<ItemClassification> archipelagoPopupForClassification;

        internal ConfigEntry<bool> fillerRandomizedFillerDispensionActive;
        internal ConfigEntry<FillerId> fillerRandomizedFillerBag;
        internal ConfigEntry<int> fillerRandomizedFillerRate;

        internal ConfigEntry<bool> trapShowActiveItemBox;
        internal ConfigEntry<float> trapItemboxHorizontalPositioning;
        internal ConfigEntry<float> trapItemboxVerticalPositioning;
        internal ConfigEntry<float> trapChanceToTrigger;
        internal ConfigEntry<int> trapDoubleTimeActiveTime;
        internal ConfigEntry<float> trapDoubleTimeScale;
        internal ConfigEntry<int> trapHalfTimeActiveTime;
        internal ConfigEntry<float> trapHalfTimeScale;
        internal ConfigEntry<int> trapAlwaysOnBeatActiveTime;
        internal ConfigEntry<int> trapInvisibleWeaponActiveTime;
        internal ConfigEntry<int> trapWeaponTrickeryActiveTime;

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

        public Configuration(ConfigFile config)
        {
            this.config = config;

            hellsingerPauseGameOutOfFocused = config.Bind(
                             "Hellsinger",
                             "HellsingerPauseGameOutOfFocused",
                             false,
                             "! EXPERIMENTAL !\nPauses the game while it is unfocused.\nLets the GPU idle while unfocused.\n! May be less stable than the ingame's option!"
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

            // ---

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
                "Archipelago room password.\nIf for whatever reason you want to set a default password that is visible in plain text.\nPLAIN TEXT - CAREFUL\nThis field isn't updated when using the ingame connection popup"
            );
            archipelagoPassword.SettingChanged += (sender, args) =>
            {
                ArchipelagoConnectorGui.archipelagoPassword = archipelagoPassword.Value;
            };

            archipelagoConsoleEnabled = config.Bind(
                "Archipelago",
                "ArchipelagoConsoleEnabled",
                true,
                "En/Disable the archipelago itemfeed."
            );
            archipelagoConsoleEnabled.SettingChanged += addOnChangeSave(config);

            archipelagoPopupForClassification = config.Bind(
                "Archipelago",
                "PopupForClassification",
                ItemClassification.useful | ItemClassification.progression,
                "Filter to show which messages get a popup during gameplay."
            );
            archipelagoPopupForClassification .SettingChanged += addOnChangeSave(config);

            archipelagoDeathlinkType = config.Bind(
                "Archipelago",
                "ArchipelagoDeathlinkType",
                DeathLinkType.Death,
                "Overrides the slot's deathlink settings for deathlink type.\n'Death' applies immediately.\n'DeathTrap' queues the death as a trap.\n'RandomTrap' queues any of the available trap items, including the Death item."
            );
            archipelagoDeathlinkType.SettingChanged += (sender, args) =>
            {
                addOnChangeSave(config);
                Randomizer.Archipelago.CheckDeathlink();
            };

            // ---

            fillerRandomizedFillerDispensionActive = config.Bind(
                "Hellsinger.Filler",
                "RandomizedFillerDispensionActive",
                false,
                "When enabled adds random filler items to the item queue.\nThose items are chosen from the random item bag, see RandomizedFillerBag."
            );
            fillerRandomizedFillerDispensionActive.SettingChanged += addOnChangeSave(config);

            fillerRandomizedFillerRate = config.Bind( //<- line 152
                "Hellsinger.Filler",
                "RandomizedFillerDispension",
                30,
                new ConfigDescription(
                    "Rate in seconds as in how often to add a random item to the queue.",
                    new AcceptableValueRange<int>(1, 120)
                )
            );
            fillerRandomizedFillerRate.SettingChanged += addOnChangeSave(config);

            fillerRandomizedFillerBag = config.Bind(
                "Hellsinger.Filler",
                "RandomizedFillerBag",
                FillerId.AlwaysOnBeat
                    | FillerId.DoubleTime
                    | FillerId.HalfTime
                    | FillerId.WeaponTrickery
                    | FillerId.NextMultiplier
                    | FillerId.MaxMultiplier
                    | FillerId.ResetMultiplier
                    | FillerId.UltimateTrigger
                    | FillerId.InvisibleWeapons
                    | FillerId.Complement
                    | FillerId.Failure
                    | FillerId.Encouragement,
                "The included items to randomly dispense."
            );
            fillerRandomizedFillerBag.SettingChanged += addOnChangeSave(config);

            // ---

            trapShowActiveItemBox = config.Bind(
                "Hellsinger.Traps",
                "ShowActiveItemBox",
                true,
                new ConfigDescription("En/Disables the textbox that pops up when an item with duration is active.\nNote: The box doesn't show up for the gameplay toggles, see above.")
            );
            trapShowActiveItemBox.SettingChanged += addOnChangeSave(config);

            trapItemboxHorizontalPositioning = config.Bind(
                "Hellsinger.Traps",
                "ItemboxHorizontalPositioning",
                0.5f,
                new ConfigDescription(
                    "Horizontal positioning of the active item box in percent, 0 is completely to the left, 1 is completely to the right.",
                    new AcceptableValueRange<float>(0, 1)
                )
            );
            trapItemboxHorizontalPositioning.SettingChanged += addOnChangeSave(config);

            trapItemboxVerticalPositioning = config.Bind(
                "Hellsinger.Traps",
                "ItemboxVerticalPositioning",
                0.2f,
                new ConfigDescription(
                    "Vertical positioning of the active item box in percent, 0 is completely at the top, 1 is completely at the bottom.",
                    new AcceptableValueRange<float>(0, 1)
                )
            );
            trapItemboxVerticalPositioning.SettingChanged += addOnChangeSave(config);

            trapChanceToTrigger = config.Bind(
                "Hellsinger.Traps",
                "ChanceToTrigger",
                100f,
                new ConfigDescription(
                    "Chance in percent as in how high the chance is to pull an item from the item queue.\nThis mod checks about every 0.1 seconds for an item.\nExample: If you want a 1% change to trigger an item every second, set the value to 0.1\nInfo: This mod only checks for an item while actively for more than 10seconds in game AND the game isn't paused.",
                    new AcceptableValueRange<float>(0, 100)
                )
            );
            trapChanceToTrigger.SettingChanged += addOnChangeSave(config);

            trapDoubleTimeActiveTime = config.Bind(
                "Hellsinger.Traps",
                "DoubleTimeActiveTime",
                15,
                new ConfigDescription(
                    "Rate in seconds as in how long the trap should be active.",
                    new AcceptableValueRange<int>(0, 120)
                )
            );
            trapDoubleTimeActiveTime.SettingChanged += addOnChangeSave(config);

            trapDoubleTimeScale = config.Bind(
                "Hellsinger.Traps",
                "DoubleTimeScale",
                1.35f,
                new ConfigDescription(
                    "Timescale for how fast the game should be during Double Time.\nAdjusting this too low/high may make the game unplayable.",
                    new AcceptableValueRange<float>(1, 2)
                )
            );
            trapDoubleTimeScale.SettingChanged += addOnChangeSave(config);

            trapHalfTimeActiveTime = config.Bind(
                "Hellsinger.Traps",
                "HalfTimeActiveTime",
                15,
                new ConfigDescription(
                    "Rate in seconds as in how long the trap should be active.",
                    new AcceptableValueRange<int>(0, 120)
                )
            );
            trapHalfTimeActiveTime.SettingChanged += addOnChangeSave(config);

            trapHalfTimeScale = config.Bind(
                "Hellsinger.Traps",
                "HalfTimeScale",
                0.70f,
                new ConfigDescription(
                    "Timescale for how slow the game should be during Half Time.\nAdjusting this too low/high may make the game unplayable.",
                    new AcceptableValueRange<float>(0, 1)
                )
            );
            trapHalfTimeScale.SettingChanged += addOnChangeSave(config);

            trapInvisibleWeaponActiveTime = config.Bind(
                "Hellsinger.Traps",
                "InvisibleWeaponActiveTime",
                15,
                new ConfigDescription(
                    "Rate in seconds as in how long the trap should be active.",
                    new AcceptableValueRange<int>(0, 120)
                )
            );
            trapInvisibleWeaponActiveTime.SettingChanged += addOnChangeSave(config);

            trapWeaponTrickeryActiveTime = config.Bind(
                "Hellsinger.Traps",
                "WeaponTrickeryActiveTime",
                15,
                new ConfigDescription(
                    "Rate in seconds as in how filler the trap should be active.",
                    new AcceptableValueRange<int>(0, 120)
                )
            );
            trapWeaponTrickeryActiveTime.SettingChanged += addOnChangeSave(config);

            trapAlwaysOnBeatActiveTime = config.Bind(
                "Hellsinger.Traps",
                "AlwaysOnBeatActiveTime",
                15,
                new ConfigDescription(
                    "Rate in seconds as in how filler the filler should be active.",
                    new AcceptableValueRange<int>(0, 120)
                )
            );
            trapAlwaysOnBeatActiveTime.SettingChanged += addOnChangeSave(config);

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
