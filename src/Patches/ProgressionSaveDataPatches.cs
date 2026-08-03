using HarmonyLib;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Outsiders.GUI;
using static ProgressionSaveData;

namespace Randomizer
{
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
            Logger.LogInfo($"ProgressionSaveData SetComboCompleted Postfix called for {comboType}");
            Randomizer.LocationTracker.CheckFuryCombo(comboType);
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(ProgressionSaveData.SetWorldItemAsDiscovered))]
        static bool SetWorldItemAsDiscoveredPrefix(string id)
        {
            Logger.LogInfo($"ProgressionSaveData SetWorldItemAsDiscovered Prefix called for {id}");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(ProgressionSaveData.SetWorldItemAsDiscovered))]
        static void SetWorldItemAsDiscoveredPostfix(string id)
        {
            Logger.LogInfo($"ProgressionSaveData SetWorldItemAsDiscovered Postfix called for {id}");
            Randomizer.LocationTracker.CheckWorldItem(id);
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
            Logger.LogError(
                $"Is loading songs: {Randomizer.IsLoadingSongs}, randomize boss songs: {Randomizer.Configuration.songsRandomizeBossSongs.Value}"
            );
            if (
                Randomizer.IsLoadingHells
                && Randomizer.Configuration.songsRandomizeBossSongsInHellsSelect.Value
            )
                __result = Randomizer.ItemTracker.GetRandomizedBossSong();
            else if (
                Randomizer.IsLoadingSongs
                && (Randomizer.CurrentGameMode == EGameMode.Stage || Randomizer.CurrentGameMode == EGameMode.Endless)
                && !Randomizer.Configuration.songsRandomizeBossSongsInHellsSelect.Value
                && Randomizer.Configuration.songsRandomizeBossSongs.Value
            )
                __result = Randomizer.ItemTracker.GetRandomizedBossSong();
            Logger.LogDebug($"ProgressionSaveData GetBossSongIDForLevel Postfix called for {levelId}, returning {__result}");

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
                Randomizer.IsLoadingHells
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
