using HarmonyLib;
using Il2CppSystem.Collections.Generic;
using Outsiders.GUI;
using Il2CppSystem;
using Il2CppInterop.Runtime.InteropTypes.Arrays;

namespace Randomizer
{
    [HarmonyPatch(typeof(LoadoutView))]
    public class LoadoutViewPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(LoadoutView.InitWithContract))]
        static bool InitWithContractPrefix(ref LoadoutView __instance, Object contract)
        {
            Logger.LogInfo($"LoadoutView InitWithContract Prefix called");
            __instance.m_slotCount = 0;
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(LoadoutView.InitWithContract))]
        static void InitWithContractPostfix(ref LoadoutView __instance, Object contract)
        {
            __instance.m_slotCount = 0;
            Logger.LogInfo("LoadoutView InitWithContract Postfix called");

            for (int i = 0; i < 8; i++)
            {
                __instance.OnWeaponEquipStateChanged(i, WeaponEquipState.None);
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(LoadoutView.OnWeaponEquipStateChanged))]
        static bool OnWeaponEquipStateChangedPrefix(
            LoadoutView __instance,
            int index,
            ref WeaponEquipState equipState
        )
        {
            Logger.LogInfo(
                $"LoadoutView OnWeaponEquipStateChanged Prefix for index {index} and equip state {equipState} called"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(LoadoutView.OnWeaponEquipStateChanged))]
        static void OnWeaponEquipStateChangedPostfix(
            ref LoadoutView __instance,
            int index,
            WeaponEquipState equipState
        )
        {
            Logger.LogInfo(
                $"LoadoutView OnWeaponEquipStateChanged Postfix for index {index} and equip state {equipState} called"
            );
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(LoadoutView.OnConfirmButtonSelectionChanged))]
        static bool OnConfirmButtonSelectionChangedPrefix(
            LoadoutView __instance,
            OutsidersButton.SelectionEvent selectionType,
            Object target
        )
        {
            Logger.LogInfo(
                $"LoadoutView OnConfirmButtonSelectionChanged Prefix for event {selectionType} and target {target} of object type {target.GetIl2CppType()} called"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(LoadoutView.OnConfirmButtonSelectionChanged))]
        static void OnConfirmButtonSelectionChangedPostfix(
            ref LoadoutView __instance,
            OutsidersButton.SelectionEvent selectionType,
            Object target
        )
        {
            Logger.LogInfo("LoadoutView OnConfirmButtonSelectionChanged Postfix called");
        }
    }
    [HarmonyPatch(typeof(LoadoutOutfitItem))]
    public class LoadoutOutfitItemPatches
    {
        public static bool HasSkinEquipped = false;

        [HarmonyPrefix]
        [HarmonyPatch(nameof(LoadoutOutfitItem.SetData))]
        static bool SetDataPrefix(LoadoutOutfitItem __instance, LoadoutOutfitData data, int index)
        {
            Logger.LogInfo(
                $"LoadoutOutfitItem SetData Prefix for {data.OutfitType} at index {index} called"
            );

            if (data.IsLockedByDLC)
                return true;

            data.IsUnlocked = Randomizer.ItemTracker.IsOutfitUnlocked(data.OutfitType);

            if (data.IsUnlocked)
                data.IsViewed = !Randomizer.LocationTracker.IsOutfitUnchecked(data.OutfitType);
            else
                data.IsViewed = true;

            if (!HasSkinEquipped && data.IsUnlocked)
            {
                SaveDataManager.SaveData.EquipSkin(SkinTargetType.Outfit, data.OutfitType);
                HasSkinEquipped = true;
            }
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(LoadoutOutfitItem.SetData))]
        static void SetDataPostfix(LoadoutOutfitItem __instance, LoadoutOutfitData data, int index)
        {
            Logger.LogInfo(
                $"LoadoutOutfitItem SetData Postfix for {data.OutfitType} at index {index} called"
            );

            bool hasOutfit = Randomizer.ItemTracker.IsOutfitUnlocked(data.OutfitType);
            __instance.m_lockIconInContainer.gameObject.SetActive(!hasOutfit);
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(LoadoutOutfitItem.OnExitButton))]
        static void OnExitButtonPostfix(LoadoutOutfitItem __instance)
        {
            Logger.LogDebug(
                $"LoadoutOutfitItem OnExitButton Postfix for {__instance.m_data.OutfitType}"
            );
            if (__instance.m_data.IsUnlocked)
                __instance.SetHasSeenOutfit(
                    !Randomizer.LocationTracker.IsOutfitUnchecked(__instance.m_data.OutfitType)
                );
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(LoadoutOutfitItem.OnSelectionChanged))]
        static void OnSelectionChangedPostfix(LoadoutOutfitItem __instance)
        {
            Logger.LogDebug($"LoadoutOutfitItem OnSelectionChanged Postfix for {__instance.m_data.OutfitType}");
            if (__instance.m_data.IsUnlocked)
                __instance.SetHasSeenOutfit(
                    !Randomizer.LocationTracker.IsOutfitUnchecked(__instance.m_data.OutfitType)
                );
        }
    }

    [HarmonyPatch(typeof(LoadoutSkinItem))]
    public class LoadoutSkinItemPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(LoadoutSkinItem.SetData))]
        static bool SetDataPrefix(LoadoutSkinItem __instance, LoadoutWeaponData data, int index)
        {
            Logger.LogInfo($"LoadoutSkinItem SetData Prefix for {data.WeaponType} called");
            var skinData = data.Skins[index];

            skinData.PickupCount = 0;
            skinData.PickupLimit = 1;

            bool weaponSkinUnlocked = Randomizer.ItemTracker.IsWeaponSkinUnlocked(data.WeaponType);

            if (skinData.SkinType != SkinType.Corrupted)
            {
                if (weaponSkinUnlocked && Randomizer.Configuration.skinsAutoSetWeaponSkin.Value)
                    skinData.Equipped = false;
                data.Skins[index] = skinData;
                return true;
            }

            skinData.IsViewed = true;
            skinData.IsUnlocked = Randomizer.ItemTracker.IsWeaponSkinUnlocked(data.WeaponType);

            if (!skinData.IsUnlocked)
                return true;

            if (Randomizer.Configuration.skinsAutoSetWeaponSkin.Value)
                skinData.Equipped = true;

            skinData.PickupCount = skinData.PickupLimit;
            data.Skins[index] = skinData;

            return true;
        }
    }

    [HarmonyPatch(typeof(LoadoutWeaponItem))]
    public class LoadoutWeaponItemPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(LoadoutWeaponItem.SetData))]
        static bool SetDataPrefix(
            LoadoutWeaponItem __instance,
            LoadoutWeaponData data,
            int index,
            bool isInCosmeticsMode
        )
        {
            Logger.LogInfo($"LoadoutWeaponItem SetData Prefix for {data.WeaponType} called");
            bool hasWeapon = Randomizer.ItemTracker.IsWeaponUnlocked(data.WeaponType);
            if (
                ( // any other weapon
                    data.WeaponType != PlayerWeaponType.AssaultRifle
                    && data.WeaponType != PlayerWeaponType.Bow
                )
                || ( // Respect DLCs
                    data.WeaponType == PlayerWeaponType.AssaultRifle
                    && DLCPatches.Instance.HasDLC(EDLC.DreamOfTheBeast)
                )
                || (
                    data.WeaponType == PlayerWeaponType.Bow
                    && DLCPatches.Instance.HasDLC(EDLC.Purgatory)
                )
            )
            {
                data.IsWeaponAvailable = hasWeapon;
                data.IsWeaponUnlocked = true;
                data.IsWeaponSelectable = hasWeapon;
                Logger.LogInfo($"Setting weapon {data.WeaponType} available: {hasWeapon}");
            }
            data.WeaponEquipState = WeaponEquipState.None;
            data.IsUltimateViewed = true;

            if (data.IsWeaponAvailable)
            {
                Logger.LogDebug($"Checking if {data.WeaponType} is unchecked");
                data.IsWeaponViewed = !Randomizer.LocationTracker.IsWeaponUnchecked(
                    data.WeaponType
                );
            }

            for (int i = 0; i < data.Skins.Count; i++)
            {
                var skinData = data.Skins[i];

                skinData.PickupCount = 0;
                skinData.PickupLimit = 1;

                bool weaponSkinUnlocked = Randomizer.ItemTracker.IsWeaponSkinUnlocked(
                    data.WeaponType
                );

                if (skinData.SkinType != SkinType.Corrupted)
                {
                    if (weaponSkinUnlocked && Randomizer.Configuration.skinsAutoSetWeaponSkin.Value)
                        skinData.Equipped = false;
                    data.Skins[i] = skinData;
                    continue;
                }

                skinData.IsViewed = true;
                skinData.IsUnlocked = weaponSkinUnlocked;

                if (!skinData.IsUnlocked)
                    continue;

                if (Randomizer.Configuration.skinsAutoSetWeaponSkin.Value)
                {
                    skinData.Equipped = true;
                    SaveDataManager.SaveData.EquipSkin(
                        Randomizer.ItemTracker.WeaponToSkin(data.WeaponType),
                        skinData.SkinType
                    );
                }

                skinData.PickupCount = skinData.PickupLimit;
                data.Skins[i] = skinData;
            }

            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(LoadoutWeaponItem.SetData))]
        static void SetDataPostfix(
            LoadoutWeaponItem __instance,
            LoadoutWeaponData data,
            int index,
            bool isInCosmeticsMode
        )
        {
            Logger.LogInfo(
                $"Loadout index {index}, is cosmetics mode: {isInCosmeticsMode}, available: {data.IsWeaponAvailable}, unlocked: {data.IsWeaponUnlocked}, selectable: {data.IsWeaponSelectable}, viewed: {data.IsWeaponViewed}, ultimate viewed: {data.IsUltimateViewed}, ultimate unlocked: {data.IsUltimateUnlocked}"
            );
            Logger.LogDebug($"LoadoutWeaponItem SetData Postfix for {data.WeaponType} called");
            bool hasWeapon = Randomizer.ItemTracker.IsWeaponUnlocked(data.WeaponType);
            __instance.m_lockIcon.gameObject.SetActive(!hasWeapon);

            bool weaponSkinUnlocked = Randomizer.ItemTracker.IsWeaponSkinUnlocked(data.WeaponType);
            __instance.SetShowSkinsButton(weaponSkinUnlocked);
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(LoadoutWeaponItem.SetupSlotState))]
        static bool SetupSlotStatePrefix(LoadoutWeaponItem __instance, ref LoadoutWeaponData data)
        {
            Logger.LogDebug(
                $"LoadoutWeaponItem SetupSlotState Prefix for {data.WeaponType} called"
            );
            Logger.LogDebug(
                $"available: {data.IsWeaponAvailable}, unlocked: {data.IsWeaponUnlocked}, selectable: {data.IsWeaponSelectable}, viewed: {data.IsWeaponViewed}, ultimate viewed: {data.IsUltimateViewed}, ultimate unlocked: {data.IsUltimateUnlocked}"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(LoadoutWeaponItem.SetupSlotState))]
        static void SetupSlotStatePostfix(LoadoutWeaponItem __instance, LoadoutWeaponData data)
        {
            Logger.LogDebug(
                $"available: {data.IsWeaponAvailable}, unlocked: {data.IsWeaponUnlocked}, selectable: {data.IsWeaponSelectable}, viewed: {data.IsWeaponViewed}, ultimate viewed: {data.IsUltimateViewed}, ultimate unlocked: {data.IsUltimateUnlocked}"
            );
            Logger.LogDebug(
                $"LoadoutWeaponItem SetupSlotState Postfix for {data.WeaponType} called"
            );
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(LoadoutWeaponItem.SetEquipState))]
        static bool SetEquipStatePrefix(LoadoutWeaponItem __instance, WeaponEquipState equipState)
        {
            Logger.LogInfo($"LoadoutWeaponItem SetEquipState Prefix for {equipState} called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(LoadoutWeaponItem.SetEquipState))]
        static void SetEquipStatePostfix(LoadoutWeaponItem __instance, WeaponEquipState equipState)
        {
            Logger.LogInfo($"LoadoutWeaponItem SetEquipState Postfix for {equipState} called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(LoadoutWeaponItem.Select))]
        static bool SelectPrefix(LoadoutWeaponItem __instance)
        {
            Logger.LogInfo("LoadoutWeaponItem Select Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(LoadoutWeaponItem.OnSelectionChanged))]
        static void OnSelectionChangedPostfix(LoadoutWeaponItem __instance)
        {
            Logger.LogDebug($"LoadoutWeaponItem OnSelectionChanged Postfix for {__instance.m_data.WeaponType}");
            if (__instance.m_data.IsWeaponAvailable)
                __instance.SetHasSeenWeapon(
                    !Randomizer.LocationTracker.IsWeaponUnchecked(__instance.m_data.WeaponType)
                );
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(LoadoutWeaponItem.OnExitButton))]
        static void OnExitButtonPostfix(LoadoutWeaponItem __instance)
        {
            Logger.LogDebug(
                $"LoadoutWeaponItem OnExitButton Postfix for {__instance.m_data.WeaponType}"
            );
            if (__instance.m_data.IsWeaponAvailable)
                __instance.SetHasSeenWeapon(
                    !Randomizer.LocationTracker.IsWeaponUnchecked(__instance.m_data.WeaponType)
                );
        }
    }

    [HarmonyPatch(typeof(LoadoutController))]
    public class LoadoutControllerPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(LoadoutController.LimitLoadout))]
        static bool LimitLoadoutPrefix(
            LoadoutController __instance,
            LevelLoadoutParameters levelParams
        )
        {
            Logger.LogInfo(
                $"LoadoutControllerItem LimitLoadout Prefix called, limiting fav1 {levelParams.fav1}, fav2 {levelParams.fav2}, outfit {levelParams.EquippedOutfit}"
            );
            var collectionWrapper = levelParams.Sigils.TryCast<
                List<Il2CppSystem.ValueTuple<ESigilType, int>>
            >();
            int count = collectionWrapper.Count;
            for (int i = 0; i < count; i++)
            {
                Il2CppSystem.ValueTuple<ESigilType, int> sigil = levelParams.Sigils[i];
                Logger.LogInfo($"For sigil {sigil.Item1} level {sigil.Item2}");
            }
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(LoadoutController.LimitLoadout))]
        static void LimitLoadoutPostfix(LoadoutController __instance)
        {
            Logger.LogInfo("LoadoutControllerItem LimitLoadout Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(LoadoutController.IsSkinLockedByProgression))]
        static bool IsSkinLockedByProgressionPrefix(LoadoutController __instance, SkinData skinData)
        {
            Logger.LogInfo(
                $"LoadoutControllerItem IsSkinLockedByProgression Prefix called for skin {skinData.SkinType} of target {skinData.SkinTargetType}"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(LoadoutController.IsSkinLockedByProgression))]
        static void IsSkinLockedByProgressionPostfix(
            LoadoutController __instance,
            SkinData skinData,
            ref bool __result
        )
        {
            Logger.LogInfo(
                $"LoadoutController IsSkinLockedByProgression Postfix called for skin {skinData.SkinType} of target {skinData.SkinTargetType}: {__result}"
            );
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(LoadoutController.MarkWeaponAsSeen))]
        static bool MarkWeaponAsSeenPrefix(LoadoutController __instance)
        {
            Logger.LogInfo("LoadoutController MarkWeaponAsSeen Prefix called");
            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(LoadoutController.MarkSkinAsSeen))]
        static bool MarkSkinAsSeenPrefix(LoadoutController __instance)
        {
            Logger.LogInfo("LoadoutController MarkSkinAsSeen Prefix called");
            return false;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(LoadoutController.GetIsAnyOutfitUnviewed))]
        static void GetIsAnyOutfitUnviewedPostfix(LoadoutController __instance, ref bool __result)
        {
            Logger.LogInfo("LoadoutController GetIsAnyOutfitUnviewed Postfix called");
            __result = Randomizer.LocationTracker.HasUncheckedOutfits();
        }
    }

    [HarmonyPatch(typeof(SongSelectionController))]
    public class SongSelectionControllerPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(SongSelectionController.SetAsViewed))]
        static bool SetAsViewedPrefix(SongInformation songInformation)
        {
            Logger.LogDebug(
                $"SongSelectionController SetAsViewed Prefix for {songInformation.ID} called"
            );
            return false;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(SongSelectionController.SetAsViewed))]
        static void SetAsViewedPostfix(SongInformation songInformation)
        {
            Logger.LogDebug(
                $"SongSelectionController SetAsViewed Postfix for {songInformation.ID}"
            );
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(SongSelectionController.GetSongLoadoutForLevel))]
        static bool GetSongLoadoutForLevelPrefix(string levelID)
        {
            Logger.LogDebug(
                $"SongSelectionController GetSongLoadoutForLevel Prefix for {levelID} called"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(SongSelectionController.GetSongLoadoutForLevel))]
        static void GetSongLoadoutForLevelPostfix(
            string levelID,
            Il2CppSystem.ValueTuple<SongInformation, SongInformation> __result
        )
        {
            Logger.LogDebug(
                $"SongSelectionController GetSongLoadoutForLevel Postfix for {levelID}"
            );
            if (Randomizer.IsLoadingHellsSelection && __result.Item1 != null)
                SaveDataManager.SaveData.SetMainSongForLevel(levelID, __result.Item1);

            if (Randomizer.IsLoadingHellsSelection && __result.Item2 != null)
                SaveDataManager.SaveData.SetBossSongForLevel(levelID, __result.Item2);
        }
    }

    [HarmonyPatch(typeof(SongSelectionView))]
    public class SongSelectionViewPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(SongSelectionView.InitWithContract))]
        static bool InitWithContractPrefix(SongSelectionView __instance)
        {
            Logger.LogDebug($"SongSelectionView InitWithContract Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(SongSelectionView.InitWithContract))]
        static void InitWithContractPostfix(SongSelectionView __instance)
        {
            Logger.LogDebug($"SongSelectionView InitWithContract Postfix called");
            __instance.m_enterLoadoutButton.gameObject.SetActive(false);
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(SongSelectionView.OnSetAsViewed))]
        static bool OnSetAsViewedPrefix(SongInformation songInfo)
        {
            Logger.LogDebug(
                $"SongSelectionView OnSetAsViewed Prefix for {songInfo.ID} called"
            );
            return false;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(SongSelectionView.OnSetAsViewed))]
        static void OnSetAsViewedPostfix(SongInformation songInfo)
        {
            Logger.LogDebug(
                $"SongSelectionView OnSetAsViewed Postfix for {songInfo.ID}"
            );
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(SongSelectionView.CreateList))]
        static bool CreateListPrefix(SongSelectionView __instance, Dictionary<string, SongSelectionController.SongStateData> songStateData)
        {
            Logger.LogInfo($"SongSelectionView CreateList Prefix called for {songStateData.Count} songs");
            var keys = new List<string>();
            foreach (var key in songStateData.Keys)
            {
                keys.Add(key);
            }

            foreach (var songId in keys)
            {
                var songname = Lookup.SongIdToName[songId];
                var isUnlocked = Randomizer.ItemTracker.Has(songname);
                var isUnchecked = Randomizer.LocationTracker.IsSongUnchecked(songname);
                var songData = new SongSelectionController.SongStateData(isUnlocked, !isUnchecked);
                songStateData[songId] = songData;
                Logger.LogInfo($"Song {songname} is unlocked: {songData.IsUnlocked}, is viewed: {songData.IsViewed}");
            }
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(SongSelectionView.CreateList))]
        static void CreateListPostfix(SongSelectionView __instance)
        {
            Logger.LogDebug($"SongSelectionView CreateList Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(SongSelectionView.OnEnterLoadout))]
        static bool OnEnterLoadoutPrefix(SongSelectionView __instance)
        {
            Logger.LogInfo($"SongSelectionView OnEnterLoadout Prefix called");
            return false;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(SongSelectionView.OnEnterLoadout))]
        static void OnEnterLoadoutPostfix(SongSelectionView __instance)
        {
            Logger.LogDebug($"SongSelectionView OnEnterLoadout Postfix called");
        }
    }

    [HarmonyPatch(typeof(EndlessSongSelectionController))]
    public class EndlessSongSelectionControllerPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(EndlessSongSelectionController.SetAsViewed))]
        static bool SetAsViewedPrefix(SongInformation songInformation)
        {
            Logger.LogDebug(
                $"EndlessSongSelectionController SetAsViewed Prefix for {songInformation.ID} called"
            );
            return false;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(EndlessSongSelectionController.SetAsViewed))]
        static void SetAsViewedPostfix(SongInformation songInformation)
        {
            Logger.LogDebug(
                $"EndlessSongSelectionController SetAsViewed Postfix for {songInformation.ID}"
            );
        }
    }

    [HarmonyPatch(typeof(TabBarItem))]
    public class TabBarItemPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(TabBarItem.Show))]
        static bool ShowPrefix(TabBarItem __instance)
        {
            Logger.LogDebug(
                $"TabBarItem Show Prefix for {__instance.m_text} called"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(TabBarItem.Show))]
        static void ShowPostfix(ref TabBarItem __instance)
        {
            Logger.LogDebug($"TabBarItem Show Postfix for {__instance.m_text} called");
            if (
                (__instance.m_text == "ARSENAL" || __instance.m_text == "WEAPON SKINS")
                && Randomizer.LocationTracker.HasUncheckedWeapons()
            )
                __instance.SetNewIconVisible(true);
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(TabBarItem.RefreshLayout))]
        static bool RefreshLayoutPrefix(TabBarItem __instance)
        {
            Logger.LogDebug(
                $"TabBarItem RefreshLayout Prefix for {__instance.m_text} called"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(TabBarItem.RefreshLayout))]
        static void RefreshLayoutPostfix(ref TabBarItem __instance)
        {
            Logger.LogDebug(
                $"TabBarItem RefreshLayout Postfix for {__instance.m_text} called"
            );
            if(__instance.m_text == "ARSENAL" && Randomizer.LocationTracker.HasUncheckedWeapons())
                __instance.SetNewIconVisible(true);
        }
    }

    [HarmonyPatch(typeof(LoadoutSigilItem))]
    public class LoadoutSigilItemPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(LoadoutSigilItem.SetData))]
        static bool SetDataPrefix(LoadoutSigilItem __instance, LoadoutSigilData data, int index)
        {
            Logger.LogInfo(
                $"LoadoutSigilItem SetData Prefix for {data.SigilType} on level {data.Level} called"
            );
            int sigilLevel = Randomizer.ItemTracker.GetSigilLevelByType(data.SigilType);

            Logger.LogInfo($"Setting sigil {data.SigilType} to level: {sigilLevel}");

            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(LoadoutSigilItem.SetData))]
        static void SetDataPostfix(LoadoutSigilItem __instance, LoadoutSigilData data, int index)
        {
            Logger.LogInfo(
                $"Loadout index {index}, sigil: {data.SigilType}, unlocked: {data.Unlocked}, level: {data.Level}"
            );
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(LoadoutSigilItem.Select))]
        static bool SelectPrefix(LoadoutSigilItem __instance)
        {
            Logger.LogInfo("LoadoutSigilItem Select Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(LoadoutSigilItem.Select))]
        static void SelectPostfix(LoadoutSigilItem __instance)
        {
            Logger.LogInfo("LoadoutSigilItem Select Postfix called");
        }
    }

    [HarmonyPatch(typeof(LoadoutWeaponList))]
    public class LoadoutWeaponListPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(LoadoutWeaponList.SetData))]
        static bool SetDataPrefix(
            LoadoutWeaponList __instance,
            Il2CppReferenceArray<LoadoutWeaponData> data,
            bool isInCosmeticsMode
        )
        {
            Logger.LogInfo($"LoadoutWeaponList SetData Prefix for {data.Count} weapons called and is in cosmestics mode: {isInCosmeticsMode} ");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(LoadoutWeaponList.SetData))]
        static void SetDataPostfix(
            LoadoutWeaponList __instance,
            Il2CppReferenceArray<LoadoutWeaponData> data,
            bool isInCosmeticsMode
        )
        {
            Logger.LogInfo($"LoadoutWeaponList SetData Postfix for {data.Count} weapons called and is in cosmestics mode: {isInCosmeticsMode} ");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(LoadoutWeaponList.Select))]
        static bool SelectPrefix(LoadoutWeaponList __instance)
        {
            Logger.LogInfo("LoadoutWeaponList Select Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(LoadoutWeaponList.Select))]
        static void SelectPostfix(LoadoutWeaponList __instance)
        {
            Logger.LogInfo("LoadoutWeaponList Select Postfix called");
        }
    }
}
