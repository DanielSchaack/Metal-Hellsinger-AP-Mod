using System;
using System.Collections.Generic;
using HarmonyLib;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Outsiders.GUI;
using static ProgressionSaveData;

namespace Randomizer
{
    public class SaveDataManager
    {
        public static ProgressionSaveData SaveData;
        public static CampaignManager CampaignManager;
        public static GameDataConfigurationProvider GameData;
        private static List<EndlessModeBoughtRewardData> rewardCache = [];

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
                Logger.LogDebug(
                    $"Reward {i} - reward type: {reward.RewardType}, weapon type: {reward.WeaponType}, amount: {reward.Amount}"
                );
                if (
                    reward.RewardType
                    is EndlessReward.UnlockDash
                        or EndlessReward.EnablePazCharge
                        or EndlessReward.UltimatePots
                        or EndlessReward.EnemiesOverkillable
                        or EndlessReward.MultiplierDropRate
                )
                {
                    rewardCache.Add(SaveData.EndlessModeSaveData.BoughtRewards[i]);
                }
            }
        }


        public static void ResetState()
        {
            Logger.LogInfo("Resetting ProgressionSaveData");
            SaveDataManager.SaveData.Reset();
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

            SaveDataManager.DebugLogState();
        }
        public static void SetupBaseState(ProgressionSaveData progressionSaveData)
        {
            if (progressionSaveData != null)
            {
                SaveDataManager.SaveData = progressionSaveData;
            }

            Logger.LogInfo("ProgressionSaveData is available");
            SaveDataManager.DebugLogState();
            SaveDataManager.ResetState();
        }

        private static void SetSeenInstructions()
        {
            SaveDataManager.SaveData.SeenInstructions.Clear();
            foreach (string instruction in Lookup.InstructionIDs)
            {
                SaveDataManager.SaveData.SeenInstructions.System_Collections_IList_Add(
                    instruction
                );
            }
        }

        private static void SetSeenWorldItems()
        {
            SaveDataManager.SaveData.DiscoveredWorldItems.Clear();
            foreach (string worldItem in Lookup.WorldItemIDs)
            {
                SaveDataManager.SaveData.DiscoveredWorldItems.System_Collections_IList_Add(
                    worldItem
                );
            }
        }

        private static void AddDefaultEndlessSave()
        {
            var EndlessSaveData = SaveDataManager.SaveData.EndlessModeSaveData;
            EndlessSaveData.HaveInteractedWithActiveMemories = true;
            EndlessSaveData.HavePlayedEndless = true;
            EndlessSaveData.Orbs = 4000;
            EndlessSaveData.HighestLevel = 30;
            EndlessSaveData.RespecCount = 0;
            EndlessSaveData.DidDefeatEndlessBoss = true;
            foreach (var item in rewardCache)
                EndlessSaveData.BoughtRewards.System_Collections_IList_Add(item.BoxIl2CppObject());

            SaveDataManager.SaveData.EndlessModeSaveData = EndlessSaveData;

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
            foreach(var combo in Lookup.FuryComboToLocationName.Keys)
                SaveData.CompletedCombos.System_Collections_IList_Add((int)combo);
        }

        private static void SetSeenCompanions()
        {
            SaveDataManager.SaveData.CompanionStates.Clear();

            foreach (var companionId in Lookup.CompanionIds)
            {
                var companion = new ProgressionSaveData.CompanionItemState { ItemId = companionId, Viewed = true, };
                SaveDataManager.SaveData.CompanionStates.System_Collections_IList_Add(companion);
            }
        }

        private static void SetSeenSongs()
        {
            SaveDataManager.SaveData.SeenSongs.Clear();
            foreach (string song in Lookup.SongIdToName.Keys)
            {
                SaveDataManager.SaveData.SeenSongs.System_Collections_IList_Add(song);
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
            SaveDataManager.SetupBaseState(__instance.ProgressionSave);
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
            SaveDataManager.GameData = __instance;

            DebugGameData(SaveDataManager.GameData);
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

    [HarmonyPatch(typeof(ProgressionSaveData))]
    public class ProgressionSaveDataPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(ProgressionSaveData.IsWeaponUnlocked))]
        static bool IsWeaponUnlockedPrefix(ref bool __result, PlayerWeaponType type)
        {
            Logger.LogDebug(
                $"ProgressionSaveData IsWeaponUnlocked Prefix called for {type}, returning {__result}"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(ProgressionSaveData.IsWeaponUnlocked))]
        static void IsWeaponUnlockedPostfix(ref bool __result, PlayerWeaponType type)
        {
            Logger.LogDebug(
                $"ProgressionSaveData IsWeaponUnlocked Postfix called for {type}, returning {__result}"
            );

            // Provide weapon as ingame pickup
            if (
                Randomizer.CurrentGameMode == EGameMode.Stage
                && Randomizer.CurrentGameState == GameStateController.GameStateName.InGame
            )
            {
                __result = false;
                Logger.LogInfo($"Overwriting {type} unlock to false (forcing pickup).");
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(ProgressionSaveData.GetUnlockedWeapons))]
        static bool GetUnlockedWeaponsPrefix(ref Il2CppStructArray<PlayerWeaponType> __result)
        {
            Logger.LogDebug($"ProgressionSaveData GetUnlockedWeapons called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(ProgressionSaveData.GetUnlockedWeapons))]
        static void GetUnlockedWeaponsPostfix(ref Il2CppStructArray<PlayerWeaponType> __result)
        {
            int amountOfDlc = 0;
            if (DLCPatches.Instance.HasDLC(EDLC.DreamOfTheBeast))
                amountOfDlc++;
            if (DLCPatches.Instance.HasDLC(EDLC.Purgatory))
                amountOfDlc++;

            Il2CppStructArray<PlayerWeaponType> availableWeapons =
                new Il2CppStructArray<PlayerWeaponType>(6 + amountOfDlc);
            availableWeapons[0] = PlayerWeaponType.Boomerang;
            availableWeapons[1] = PlayerWeaponType.Falx;
            availableWeapons[2] = PlayerWeaponType.Pistols;
            availableWeapons[3] = PlayerWeaponType.RhythmWeapon;
            availableWeapons[4] = PlayerWeaponType.Shotgun;
            availableWeapons[5] = PlayerWeaponType.Vulcan;
            if (
                DLCPatches.Instance.HasDLC(EDLC.DreamOfTheBeast)
                && !DLCPatches.Instance.HasDLC(EDLC.Purgatory)
            )
            {
                availableWeapons[6] = PlayerWeaponType.AssaultRifle;
            }
            else if (
                DLCPatches.Instance.HasDLC(EDLC.Purgatory)
                && !DLCPatches.Instance.HasDLC(EDLC.DreamOfTheBeast)
            )
            {
                availableWeapons[6] = PlayerWeaponType.Bow;
            }

            if (
                DLCPatches.Instance.HasDLC(EDLC.DreamOfTheBeast)
                && DLCPatches.Instance.HasDLC(EDLC.Purgatory)
            )
            {
                availableWeapons[6] = PlayerWeaponType.AssaultRifle;
                availableWeapons[7] = PlayerWeaponType.Bow;
            }
            __result = availableWeapons;

            string weaponList = string.Join(", ", availableWeapons);
            Logger.LogInfo(
                $"ProgressionSaveData GetUnlockedWeapons Postfix called, returning {weaponList}"
            );
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(ProgressionSaveData.GetIsWorldItemDiscovered))]
        static bool GetIsWorldItemDiscoveredPrefix(string id)
        {
            Logger.LogDebug(
                $"ProgressionSaveData GetIsWorldItemDiscovered Prefix called for {id}"
            );
            if(!Randomizer.IsPaused && Randomizer.CurrentGameState == GameStateController.GameStateName.InGame)
                Randomizer.LocationTracker.CheckWorldItem(id);
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(ProgressionSaveData.GetIsWorldItemDiscovered))]
        static void GetIsWorldItemDiscoveredPostfix(string id, ref bool __result)
        {
            Logger.LogDebug(
                $"ProgressionSaveData GetIsWorldItemDiscovered Postfix called for {id}, returning {__result}"
            );
            // Enable world item discovery events
            if (
                Randomizer.CurrentGameState == GameStateController.GameStateName.InGame
                && Randomizer.IsLoadingSongs
            )
                __result = false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(ProgressionSaveData.GetWeaponFlags))]
        static bool GetWeaponFlagsPrefix(ref EWeaponFlags __result, PlayerWeaponType weaponType)
        {
            Logger.LogDebug(
                $"ProgressionSaveData GetWeaponFlags Prefix called for {weaponType}, returning {__result}"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(ProgressionSaveData.GetWeaponFlags))]
        static void GetWeaponFlagsPostfix(ref EWeaponFlags __result, PlayerWeaponType weaponType)
        {
            Logger.LogDebug(
                $"ProgressionSaveData GetWeaponFlags Postfix called for {weaponType}, returning {__result}"
            );
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(ProgressionSaveData.GetWeaponEquipState))]
        static bool GetWeaponEquipStatePrefix(
            ref WeaponEquipState __result,
            PlayerWeaponType weaponType
        )
        {
            Logger.LogDebug(
                $"ProgressionSaveData GetWeaponEquipState Prefix called for {weaponType}, returning {__result}"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(ProgressionSaveData.GetWeaponEquipState))]
        static void GetWeaponEquipStatePostfix(
            ref WeaponEquipState __result,
            PlayerWeaponType weaponType
        )
        {
            Logger.LogDebug(
                $"ProgressionSaveData GetWeaponEquipState Postfix called for {weaponType}, returning {__result}"
            );
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(ProgressionSaveData.GetPrimaryWeapon))]
        static bool GetPrimaryWeaponPrefix(ref PlayerWeaponType __result)
        {
            Logger.LogInfo(
                $"ProgressionSaveData GetPrimaryWeapon Prefix called, returning {__result}"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(ProgressionSaveData.GetPrimaryWeapon))]
        static void GetPrimaryWeaponPostfix(ref PlayerWeaponType __result)
        {
            Logger.LogInfo(
                $"ProgressionSaveData GetPrimaryWeapon Postfix called, returning {__result}"
            );
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(ProgressionSaveData.GetSecondaryWeapon))]
        static bool GetSecondaryWeaponPrefix(ref PlayerWeaponType __result)
        {
            Logger.LogDebug(
                $"ProgressionSaveData GetSecondaryWeapon Prefix called, returning {__result}"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(ProgressionSaveData.GetSecondaryWeapon))]
        static void GetSecondaryWeaponPostfix(ref PlayerWeaponType __result)
        {
            Logger.LogDebug(
                $"ProgressionSaveData GetSecondaryWeapon Postfix called, returning {__result}"
            );
        }

        // WARN: This breaks loadout selection by setting a wrong internal state?
        // [HarmonyPrefix]
        // [HarmonyPatch(nameof(ProgressionSaveData.SetWeaponEquipState))]
        // static bool SetWeaponEquipStatePrefix(
        //     PlayerWeaponType weaponType,
        //     WeaponEquipState equipState
        // )
        // {
        //     Logger.LogInfo(
        //         $"ProgressionSaveData SetWeaponEquipState Prefix called for {weaponType} with equip state {equipState}"
        //     );
        //     return true;
        // }
        //
        // [HarmonyPostfix]
        // [HarmonyPatch(nameof(ProgressionSaveData.SetWeaponEquipState))]
        // static void SetWeaponEquipStatePostfix(
        //     PlayerWeaponType weaponType,
        //     WeaponEquipState equipState
        // )
        // {
        //     Logger.LogInfo(
        //         $"ProgressionSaveData SetWeaponEquipState Postfix called for {weaponType} with equip state {equipState}"
        //     );
        // }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(ProgressionSaveData.SetComboCompleted))]
        static bool SetComboCompletedPrefix(EFuryComboType comboType)
        {
            Logger.LogDebug($"ProgressionSaveData SetComboCompleted Prefix called for {comboType}");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(ProgressionSaveData.SetComboCompleted))]
        static void SetComboCompletedPostfix(EFuryComboType comboType)
        {
            Logger.LogDebug($"ProgressionSaveData SetComboCompleted Postfix called for {comboType}");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(ProgressionSaveData.SetWorldItemAsDiscovered))]
        static bool SetWorldItemAsDiscoveredPrefix(string id)
        {
            Logger.LogDebug($"ProgressionSaveData SetWorldItemAsDiscovered Prefix called for {id}");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(ProgressionSaveData.SetWorldItemAsDiscovered))]
        static void SetWorldItemAsDiscoveredPostfix(string id)
        {
            Logger.LogDebug($"ProgressionSaveData SetWorldItemAsDiscovered Postfix called for {id}");
        }

        // WARN: Messes with WeaponEquipState
        // [HarmonyPrefix]
        // [HarmonyPatch(nameof(ProgressionSaveData.SetSigilEquipedState))]
        // static bool SetSigilEquipedStatePrefix(ESigilType type, WeaponEquipState equipState)
        // {
        //     Logger.LogInfo(
        //         $"ProgressionSaveData SetSigilEquipedState Prefix called for {type} to state {equipState}"
        //     );
        //     return true;
        // }
        //
        // [HarmonyPostfix]
        // [HarmonyPatch(nameof(ProgressionSaveData.SetSigilEquipedState))]
        // static void SetSigilEquipedStatePostfix(ESigilType type, WeaponEquipState equipState)
        // {
        //     Logger.LogInfo(
        //         $"ProgressionSaveData SetSigilEquipedState Postfix called for {type} to state {equipState}"
        //     );
        // }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(ProgressionSaveData.IsCollectiblePickupCollected))]
        static bool IsCollectiblePickupCollectedPrefix(string id)
        {
            Logger.LogDebug($"ProgressionSaveData IsCollectiblePickupCollected Prefix called for {id}");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(ProgressionSaveData.IsCollectiblePickupCollected))]
        static void IsCollectiblePickupCollectedPostfix(string id, ref bool __result)
        {
            Logger.LogInfo(
                $"ProgressionSaveData IsCollectiblePickupCollected Postfix called for {id}, returning unlocked: {__result}"
            );
            __result = false;
            Logger.LogInfo($"ProgressionSaveData IsCollectiblePickupCollected Setting unlocked to: {__result}");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(ProgressionSaveData.IsSigilUnlocked))]
        static bool IsSigilUnlockedPrefix(ESigilType type)
        {
            Logger.LogDebug($"ProgressionSaveData IsSigilUnlocked Prefix called for {type}");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(ProgressionSaveData.IsSigilUnlocked))]
        static void IsSigilUnlockedPostfix(ESigilType type, ref bool __result)
        {
            Logger.LogInfo(
                $"ProgressionSaveData IsSigilUnlocked Postfix called for {type}, returning unlocked: {__result}"
            );
            __result = Randomizer.ItemTracker.GetSigilLevelByType(type) > 0;
            Logger.LogInfo($"Setting unlocked to: {__result}");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(ProgressionSaveData.IsSigilEquiped))]
        static bool IsSigilEquipedPrefix(ESigilType type)
        {
            Logger.LogDebug($"ProgressionSaveData IsSigilEquiped Prefix called for {type}");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(ProgressionSaveData.IsSigilEquiped))]
        static void IsSigilEquipedPostfix(ESigilType type)
        {
            Logger.LogDebug($"ProgressionSaveData IsSigilEquiped Postfix called for {type}");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(ProgressionSaveData.GetSigil))]
        static bool GetSigilPrefix(ESigilType type)
        {
            Logger.LogDebug($"ProgressionSaveData GetSigil Prefix called for {type}");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(ProgressionSaveData.GetSigil))]
        static void GetSigilPostfix(ESigilType type, ref ProgressionSaveData.SigilState __result)
        {
            Logger.LogInfo(
                $"ProgressionSaveData GetSigil Postfix called for {type}, returning {__result.Type} of level {__result.Level}"
            );
            SigilState newState = new ProgressionSaveData.SigilState();
            newState.Type = type;
            newState.Level = (byte)Randomizer.ItemTracker.GetSigilLevelByType(type);
            newState.Viewed = true;
            __result = newState;
            Logger.LogInfo($"Setting state for {type} to level {newState.Level}");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(ProgressionSaveData.GetSigilIndex))]
        static bool GetSigilIndexPrefix(ESigilType type)
        {
            Logger.LogDebug($"ProgressionSaveData GetSigilIndex Prefix called for {type}");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(ProgressionSaveData.GetSigilIndex))]
        static void GetSigilIndexPostfix(ESigilType type, ref int __result)
        {
            Logger.LogDebug(
                $"ProgressionSaveData GetSigilIndex Postfix called for {type}, returning {__result}"
            );
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(ProgressionSaveData.GetCollectibleDataForLevelAndDifficulty))]
        static bool GetCollectibleDataForLevelAndDifficultyPrefix(
            Il2CppReferenceArray<LevelCollectiblePickupData> collectiblePickupDatas,
            EDifficulty difficulty
        )
        {
            Logger.LogDebug(
                $"ProgressionSaveData GetCollectibleDataForLevelAndDifficulty Prefix called for difficulty {difficulty}"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(ProgressionSaveData.GetCollectibleDataForLevelAndDifficulty))]
        static void GetCollectibleDataForLevelAndDifficultyPostfix(
            ref CollectiblesStageData.CollectibleData __result,
            Il2CppReferenceArray<LevelCollectiblePickupData> collectiblePickupDatas,
            EDifficulty difficulty
        )
        {
            var data = new CollectiblesStageData.CollectibleData(0,1);
            __result = data;
            Logger.LogDebug(
                $"ProgressionSaveData GetCollectibleDataForLevelAndDifficulty Postfix called for difficulty {difficulty}, Total pickups {__result.TotalPickupsInLevel}, collected pickups {__result.CollectedPickupAmount}"
            );
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(ProgressionSaveData.GetNumCollectedCollectiblePickupsOfType))]
        static void GetNumCollectedCollectiblePickupsOfTypePostfix(
            ref int __result
        )
        {
            __result = 32;
            Logger.LogDebug(
                $"ProgressionSaveData GetNumCollectedCollectiblePickupsOfType Postfix called and returning collected pickups {__result}"
            );
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(ProgressionSaveData.IsAnySkinUnviewed))]
        static void IsAnySkinUnviewedPostfix(
            ref bool __result
        )
        {
            __result = false;
            Logger.LogDebug(
                $"ProgressionSaveData IsAnySkinUnviewed Postfix called and returning {__result}"
            );
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(ProgressionSaveData.IsBeastViewed))]
        static void IsBeastViewedPostfix(
            ref bool __result
        )
        {
            __result = true;
            Logger.LogDebug(
                $"ProgressionSaveData IsBeastViewed Postfix called and returning {__result}"
            );
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(ProgressionSaveData.IsCompanionItemViewed))]
        static void IsCompanionItemViewedPostfix(
            ref bool __result
        )
        {
            __result = true;
            // Logger.LogDebug(
            //     $"ProgressionSaveData IsCompanionItemViewed Postfix called and returning {__result}"
            // );
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(ProgressionSaveData.IsSongViewed))]
        static void IsSongViewedPostfix(
            ref bool __result
        )
        {
            __result = true;
            // Logger.LogDebug(
            //     $"ProgressionSaveData IsSongViewed Postfix called and returning {__result}"
            // );
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(ProgressionSaveData.IsArtBookViewed))]
        static void IsArtBookViewedPostfix(
            ref bool __result
        )
        {
            __result = true;
            Logger.LogDebug(
                $"ProgressionSaveData IsArtBookViewed Postfix called and returning {__result}"
            );
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(ProgressionSaveData.GetBossSongIDForLevel))]
        static bool GetBossSongIDForLevelPrefix(string levelId)
        {
            Logger.LogDebug($"ProgressionSaveData GetBossSongIDForLevel Prefix called for {levelId}");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(ProgressionSaveData.GetBossSongIDForLevel))]
        static void GetBossSongIDForLevelPostfix(string levelId, ref string __result)
        {
            Logger.LogDebug(
                $"Is loading songs: {Randomizer.IsLoadingSongs}, randomize boss songs: {Randomizer.Configuration.songsRandomizeBossSongs.Value}"
            );
            if (
                Randomizer.IsLoadingHellsSelection
                && Randomizer.Configuration.songsRandomizeBossSongsInHellsSelect.Value
            )
                __result = Randomizer.ItemTracker.GetRandomizedBossSong(levelId);
            else if (
                Randomizer.IsLoadingSongs
                && (Randomizer.CurrentGameMode == EGameMode.Stage || Randomizer.CurrentGameMode == EGameMode.Endless)
                && !Randomizer.Configuration.songsRandomizeBossSongsInHellsSelect.Value
                && Randomizer.Configuration.songsRandomizeBossSongs.Value
            )
                __result = Randomizer.ItemTracker.GetRandomizedBossSong(levelId);

            Logger.LogDebug($"ProgressionSaveData GetBossSongIDForLevel Postfix called for {levelId}, returning {__result}");

            if(Randomizer.IsLoadingSongs)
                Randomizer.CurrentBossSong = Lookup.SongIdToName[__result];
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(ProgressionSaveData.ClearBossSongForLevel))]
        static bool ClearBossSongForLevelPrefix(string levelId)
        {
            Logger.LogDebug($"ProgressionSaveData ClearBossSongForLevel Prefix called for {levelId}");
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(ProgressionSaveData.GetMainSongIDForLevel))]
        static bool GetMainSongIDForLevelPrefix(ref string levelId)
        {
            Logger.LogDebug(
                $"ProgressionSaveData GetMainSongIDForLevel Prefix called for {levelId}"
            );
            if (Randomizer.IsLoadingSongs)
            {
                string unrandomizedLevel = Randomizer.ItemTracker.GetLevelForRandomizedLevel(
                    levelId
                );
                string hellOfUnrandom = Lookup.ChallengeToHellDictionary[unrandomizedLevel];
                Logger.LogDebug(
                    $"OG levelId: {levelId}, unrandomizedLevel: {unrandomizedLevel}, hell of unrandom level: {hellOfUnrandom}, is loading songs: {Randomizer.IsLoadingSongs}, randomize main songs: {Randomizer.Configuration.songsRandomizeMainSongs.Value}"
                );
                levelId = hellOfUnrandom;
            }
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(ProgressionSaveData.GetMainSongIDForLevel))]
        static void GetMainSongIDForLevelPostfix(string levelId, ref string __result)
        {
            if (
                Randomizer.IsLoadingHellsSelection
                && Randomizer.Configuration.songsRandomizeMainSongsInHellsSelect.Value
            )
                __result = Randomizer.ItemTracker.GetRandomizedMainSong();
            else if (
                Randomizer.IsLoadingSongs
                && (Randomizer.CurrentGameMode == EGameMode.Tutorial || Randomizer.CurrentGameMode == EGameMode.Challenge)
                && Randomizer.Configuration.songsRandomizeSongsInTutorialAndTorments.Value
            )
                __result = Randomizer.ItemTracker.GetRandomizedMainSong();
            else if (
                Randomizer.IsLoadingSongs 
                && (Randomizer.CurrentGameMode == EGameMode.Stage || Randomizer.CurrentGameMode == EGameMode.Endless)
                && !Randomizer.Configuration.songsRandomizeMainSongsInHellsSelect.Value
                && Randomizer.Configuration.songsRandomizeMainSongs.Value
            )
                __result = Randomizer.ItemTracker.GetRandomizedMainSong();


            Logger.LogDebug($"ProgressionSaveData GetMainSongIDForLevel Postfix called for {levelId}, returning {__result}");
            if(Randomizer.IsLoadingSongs)
                Randomizer.CurrentMainSong = Lookup.SongIdToName[__result];

        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(ProgressionSaveData.ClearMainSongForLevel))]
        static bool ClearMainSongForLevelPrefix(string levelId)
        {
            Logger.LogDebug($"ProgressionSaveData ClearMainSongForLevel Prefix called for {levelId}");
            return true;
        }
    }
}
