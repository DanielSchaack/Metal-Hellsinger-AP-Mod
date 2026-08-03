using System;
using HarmonyLib;
using static ProgressionSaveData;

namespace Randomizer
{
    public class SaveStateManager
    {
        public static ProgressionSaveData SaveData;
        public static CampaignManager CampaignManager;
        public static GameDataConfigurationProvider GameData;

        public static void DebugLogState()
        {
            foreach (WeaponState weapon in SaveData.Weapons)
            {
                string weaponId = weapon.Id;
                var type = weapon.WeaponType;
                var flags = weapon.Flags;
                var skin = weapon.Skin;
                Logger.LogDebug(
                    $"[Weapon] ID: {weaponId} | Type: {type} | Flags: {flags} | Current Skin: {skin}"
                );
            }

            foreach (LevelState level in SaveData.LevelStates)
            {
                Logger.LogDebug(
                    $"[Level] ID: {level.LevelID} | Highest Cleared Difficulty: {level.HighestClearedDifficulty} | Flags: {level.Flags}"
                );
            }

            foreach (SigilState sigil in SaveData.Sigils)
            {
                Logger.LogDebug(
                    $"[Sigil] ID: {sigil.Type} | Level: {sigil.Level} | Viewed: {sigil.Viewed}"
                );
            }

            foreach (ChallengeResult cr in SaveData.ChallengeResults)
            {
                Logger.LogDebug(
                    $"[Challenge] ID: {cr.LevelID} | Highest Difficulty: {cr.HighestClearedDifficulty} | TierReached: {cr.TierReached} | Viewed {cr.Viewed}"
                );
            }

            foreach (CompanionItemState ci in SaveData.CompanionStates)
            {
                Logger.LogDebug($"Companion {ci.ItemId} is viewed: {ci.Viewed}");
            }

            foreach (string discoveredItem in SaveData.DiscoveredWorldItems)
            {
                Logger.LogDebug("World Item is discovered: " + discoveredItem);
            }

            foreach (string seenInstruction in SaveData.SeenInstructions)
            {
                Logger.LogDebug("Instruction seen: " + seenInstruction);
            }

            foreach (string seenSong in SaveData.SeenSongs)
            {
                Logger.LogDebug("Songs seen: " + seenSong);
            }

            for (int i = 0; i < SaveData.EndlessModeSaveData.Items.Count; i++)
            {
                var reward = SaveData.EndlessModeSaveData.Items[i];
                Logger.LogDebug($"Endless Item {i} - item type: {reward.ItemType}, reward: {reward.Reward}, amount: {reward.AmountOfItems}");
            }

            for (int i = 0; i < SaveData.EndlessModeSaveData.BoughtRewards.Count; i++)
            {
                var reward = SaveData.EndlessModeSaveData.BoughtRewards[i];
                Logger.LogDebug($"Reward {i} - reward type: {reward.RewardType}, weapon type: {reward.WeaponType}, amount: {reward.Amount}");
            }
        }

        //TODO: set on connect to starting level
        public string LastPlayedLevelID { get; set; }

        public static void SetupBaseState(ProgressionSaveData progressionSaveData)
        {
            if (progressionSaveData != null)
            {
                SaveStateManager.SaveData = progressionSaveData;
            }

            Logger.LogInfo("ProgressionSaveData is available");
            SaveStateManager.DebugLogState();

            Logger.LogInfo("Resetting ProgressionSaveData");
            SaveStateManager.SaveData.Reset();
            AddDefaultWeapons();
            AddDefaultStages();
            AddDefaultChallenges();
            AddDefaultSigils();
            AddDefaultSkins();
            AddDefaultEndlessSave();
            SetSeenMessageIds();
            SetLastStates();
            SetDefaultFuryCombos();
            SetSeenCompanions();
            SetSeenSongs();
            SetSeenWorldItems();
            SetSeenInstructions();
            LoadoutOutfitItemPatches.HasSkinEquipped = false;
            Logger.LogInfo("Resetted ProgressionSaveData");

            SaveStateManager.DebugLogState();
        }

        private static void SetSeenInstructions()
        {
            SaveStateManager.SaveData.SeenInstructions.Clear();
            foreach (string instruction in Lookup.InstructionIDs)
            {
                SaveStateManager.SaveData.SeenInstructions.System_Collections_IList_Add(
                    instruction
                );
            }
        }

        private static void SetSeenWorldItems()
        {
            SaveStateManager.SaveData.DiscoveredWorldItems.Clear();
            foreach (string worldItem in Lookup.WorldItemIDs)
            {
                SaveStateManager.SaveData.DiscoveredWorldItems.System_Collections_IList_Add(
                    worldItem
                );
            }
        }

        private static void AddDefaultEndlessSave()
        {
            var EndlessSaveData = SaveStateManager.SaveData.EndlessModeSaveData;
            EndlessSaveData.HaveInteractedWithActiveMemories = true;
            EndlessSaveData.HavePlayedEndless = true;
            EndlessSaveData.Orbs = 4000;
            EndlessSaveData.HighestLevel = 0;
            EndlessSaveData.RespecCount = 0;
            SaveStateManager.SaveData.EndlessModeSaveData = EndlessSaveData;

            // SaveStateManager.EndlessSaveData.LevelsPerArena = 5;
            // SaveStateManager.EndlessSaveData.MaxInvestableOrbsAmount = 5000;
            // SaveStateManager.EndlessSaveData.NumberOfArenas = 1;
        }

        private static void AddDefaultSkins()
        {
            SaveData.Skins.Clear();
            SkinTargetType[] types =
            [
                SkinTargetType.Shotgun,
                SkinTargetType.Pistols,
                SkinTargetType.RhythmWeapon,
                SkinTargetType.Vulcan,
                SkinTargetType.Boomerang,
                SkinTargetType.Falx,
                SkinTargetType.Outfit,
            ];
            foreach (SkinTargetType type in types)
            {
                SkinState state = new SkinState();
                state.SkinTarget = type;
                state.Seen = true;
                if (type == SkinTargetType.Outfit)
                {
                    state.Equipped = true;
                    state.Skin = SkinType.None;
                }
                else
                {
                    state.Equipped = true;
                    state.Skin = SkinType.None;
                }
                SaveData.Skins.System_Collections_IList_Add(state.BoxIl2CppObject());
            }
        }

        private static void SetDefaultFuryCombos()
        {
            SaveData.CompletedCombos.Clear();
        }

        private static void SetSeenCompanions()
        {
            SaveStateManager.SaveData.CompanionStates.Clear();

            foreach (var companionId in Lookup.CompanionIds)
            {
                var companion = new ProgressionSaveData.CompanionItemState { ItemId = companionId, Viewed = true, };
                SaveStateManager.SaveData.CompanionStates.System_Collections_IList_Add(companion);
            }
        }

        private static void SetSeenSongs()
        {
            SaveStateManager.SaveData.SeenSongs.Clear();
            foreach (string song in Lookup.SongIdToName.Keys)
            {
                SaveStateManager.SaveData.SeenSongs.System_Collections_IList_Add(song);
            }
        }

        private static void SetSeenMessageIds()
        {
            SaveData.SeenStartupMessages |= StartupMessagesFlags.VeryHardDifficulty;
            SaveData.SeenStartupMessages |= StartupMessagesFlags.CoatOfArms;
            SaveData.SeenStartupMessages |= StartupMessagesFlags.Endless;
            SaveData.SeenStartupMessages |= StartupMessagesFlags.LicensedTracksPack1Bought;
            SaveData.SeenStartupMessages |= StartupMessagesFlags.DLC1dot6Bought;
            SaveData.SeenStartupMessages |= StartupMessagesFlags.DLC1dot8Bought;
            SaveData.SeenStartupMessages |= StartupMessagesFlags.NewBlood1dot7dot2;
            SaveData.SeenStartupMessages |= StartupMessagesFlags.DLC1dot6NotBought;
            SaveData.SeenStartupMessages |= StartupMessagesFlags.DLC1dot7NotBought;
            SaveData.SeenStartupMessages |= StartupMessagesFlags.DLC1dot8NotBought;
            SaveData.SeenStartupMessages |= StartupMessagesFlags.SongSelector;

            SaveData.SeenDLCsInStore.Clear();
            SaveData.SeenDLCsInStore.Add(EDLC.DreamOfTheBeast);
            SaveData.SeenDLCsInStore.Add(EDLC.Purgatory);
            SaveData.SeenDLCsInStore.Add(EDLC.LicensedTracks1);

            SaveData.UserHasCalibratedAudio = true;
            SaveData.UserHasCalibratedBrightness = true;
            SaveData.UserHasSeenArtBook = true;
            SaveData.UserHasSeenCoatOfArmsMessage = true;
            SaveData.UserHasSeenVeryHardDifficultyMessage = true;
        }

        private static void SetLastStates()
        {
            SaveData.LastPrimarySigil = ESigilType.None;
            SaveData.LastSecondarySigil = ESigilType.None;
            SaveData.LastPrimaryWeapon = PlayerWeaponType.None;
            SaveData.LastSecondaryWeapon = PlayerWeaponType.None;
            SaveData.LastUsedDifficulty = EDifficulty.Easy;
            SaveData.LastPlayedLevelID = "Tutorial";
        }

        private static void AddDefaultChallenges()
        {
            SaveData.ChallengeResults.Clear();
            foreach (string id in Lookup.ChallengeToHellDictionary.Keys)
            {
                var cr = new ChallengeResult((Il2CppSystem.String)id, 3);
                cr.HighestClearedDifficulty = EDifficulty.VeryHard;
                SaveData.ChallengeResults.System_Collections_IList_Add(cr);
            }
        }

        private static void AddDefaultSigils()
        {
            SaveData.Sigils.Clear();
            ESigilType[] sigils =
            [
                ESigilType.BeatStreakSave,
                ESigilType.WeaponSwitchBonus,
                ESigilType.BeatStreakThreshold,
                ESigilType.MultiplierTierPostRezz,
                ESigilType.ExtraHp,
                ESigilType.UltimateAutoRefill,
                ESigilType.LongerSlaughter,
            ];
            foreach (var id in sigils)
            {
                var sigil = new SigilState();
                sigil.Type = id;
                sigil.Level = 3;
                sigil.Viewed = true;
                SaveData.Sigils.System_Collections_IList_Add(sigil.BoxIl2CppObject());
            }
        }

        private static void AddDefaultWeapons()
        {
            SaveData.Weapons.Clear();
            PlayerWeaponType[] types =
            [
                PlayerWeaponType.Falx,
                PlayerWeaponType.RhythmWeapon,
                PlayerWeaponType.Shotgun,
                PlayerWeaponType.AssaultRifle,
                PlayerWeaponType.Bow,
                PlayerWeaponType.Pistols,
                PlayerWeaponType.Vulcan,
                PlayerWeaponType.Boomerang,
            ];
            foreach (var id in types)
            {
                SaveData.Weapons.System_Collections_IList_Add(
                    new WeaponState(
                        id,
                        EWeaponFlags.Viewed | EWeaponFlags.Unlocked | EWeaponFlags.UltViewed
                    )
                );
            }
        }

        private static void AddDefaultStages()
        {
            SaveData.LevelStates.Clear();
            foreach (string HellsID in Lookup.HellsIDs)
            {
                LevelState state = new(
                    (Il2CppSystem.String)HellsID,
                    ELevelStateFlags.Unlocked | ELevelStateFlags.Viewed | ELevelStateFlags.Played,
                    EDifficulty.VeryHard
                );
                SaveData.LevelStates.System_Collections_IList_Add(state);
            }
        }
    }

    [HarmonyPatch(typeof(GameSaveProvider))]
    public class GameSaveProviderPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(GameSaveProvider.LoadProgression))]
        static bool LoadProgressionPrefix(GameSaveProvider __instance, ref bool __result)
        {
            Logger.LogDebug("GameSaveProvider LoadProgression Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(GameSaveProvider.LoadProgression))]
        static void LoadProgressionPostfix(GameSaveProvider __instance, ref bool __result)
        {
            Logger.LogInfo(
                "GameSaveProvider LoadProgression Postfix called, setting up save state"
            );
            SaveStateManager.SetupBaseState(__instance.ProgressionSave);
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(GameSaveProvider.SaveProgression))]
        static bool SaveProgressionPrefix(Action<bool> onDoneCallback)
        {
            Logger.LogDebug("GameSaveProvider SaveProgression Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(GameSaveProvider.SaveProgression))]
        static void SaveProgressionPostfix(Action<bool> onDoneCallback)
        {
            Logger.LogDebug("GameSaveProvider SaveProgression Postfix called");
        }
    }

    [HarmonyPatch(typeof(DataController))]
    public class DataControllerPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(DataController.LoadProgression))]
        static bool LoadProgressionPrefix(ProgressionSaveData progression)
        {
            Logger.LogDebug("DataController LoadProgression Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(DataController.LoadProgression))]
        static void LoadProgressionPostfix(ProgressionSaveData progression)
        {
            Logger.LogDebug("DataController LoadProgression Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(DataController.SaveProgression))]
        static bool SaveProgressionPrefix(ProgressionSaveData progression)
        {
            Logger.LogDebug("DataController SaveProgression Prefix called, don't allow saving");
            return false;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(DataController.SaveProgression))]
        static void SaveProgressionPostfix(ProgressionSaveData progression)
        {
            Logger.LogDebug("DataController SaveProgression Postfix called");
        }
    }

    [HarmonyPatch(typeof(GameDataConfigurationProvider))]
    public class GameDataConfigurationProviderPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(GameDataConfigurationProvider.Init))]
        static bool InitPrefix(GameDataConfigurationProvider __instance)
        {
            Logger.LogDebug("GameDataConfigurationProvider Init Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(GameDataConfigurationProvider.Init))]
        static void InitPostfix(GameDataConfigurationProvider __instance)
        {
            Logger.LogDebug("GameDataConfigurationProvider Init Postfix called");
            SaveStateManager.GameData = __instance;

            DebugGameData(SaveStateManager.GameData);
        }

        private static void DebugGameData(GameDataConfigurationProvider gameData)
        {
            foreach (var kvp in gameData.WeaponConfigs)
            {
                Logger.LogDebug($"Weapon {kvp.Key}, type: {kvp.Value.WeaponAbilityType}, group {kvp.Value.WeaponGroup}, original weapon type: {kvp.Value.WeaponType} ");
            }

            foreach (var section in gameData.EndlessModeItemCollection.ItemSections)
            {
                Logger.LogDebug($"Section category: {section.Category}");
                foreach (var item in section.Items)
                {
                    Logger.LogDebug($"Item type: {item.ItemType}, reward: {item.UnlockByReward}, unlock type: {item.UnlockType}");
                    if(item.reward != null){
                        var reward = item.reward.Reward;
                        Logger.LogDebug($"Reward category: {reward.Category}, type: {reward.RewardType}");
                        if(reward.WeaponCurseProperties != null)
                            Logger.LogDebug($"Curse property type: {reward.WeaponCurseProperties.WeaponCurseType}, group: {reward.WeaponCurseProperties.WeaponGroup}");
                        if(reward.Weapons != null)
                            Logger.LogDebug($"Weapon type: {reward.Weapons.Type}, lifespan: {reward.Weapons.m_lifespan}");
                    }

                }
            }
        }
    }
}
