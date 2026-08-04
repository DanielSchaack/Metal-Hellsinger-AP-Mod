using HarmonyLib;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppSystem.Collections.Generic;
using Outsiders.GUI;
using UnityEngine;

namespace Randomizer
{
    [HarmonyPatch(typeof(InGameGUIController))]
    public class InGameGUIControllerPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(InGameGUIController.ShowChallengeTierLevel))]
        static bool ShowChallengeTierLevelPrefix(
            InGameGUIController __instance,
            ChallengeTracker.ChallengeResult result
        )
        {
            Logger.LogDebug($"InGameGUIController ShowChallengeTierLevel Prefix called for {result}");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(InGameGUIController.ShowChallengeTierLevel))]
        static void ShowChallengeTierLevelPostfix(
            InGameGUIController __instance,
            ChallengeTracker.ChallengeResult result
        )
        {
            Logger.LogDebug(
                $"InGameGUIController ShowChallengeTierLevel Postfix called for {result}"
            );
            Randomizer.LocationTracker.CheckChallengeProgress(result, Randomizer.CurrentLevel);
        }
    }
    [HarmonyPatch(typeof(InGameState))]
    public class InGameStatePatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(InGameState.CreateChallengeUnlocksData))]
        static bool CreateChallengeUnlocksDataPrefix(
            InGameState __instance,
            StageUnlocksData __result
        )
        {
            Logger.LogDebug($"InGameState CreateChallengeUnlocksData Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(InGameState.CreateChallengeUnlocksData))]
        static void CreateChallengeUnlocksDataPostfix(
            InGameState __instance,
            StageUnlocksData __result
        )
        {
            Logger.LogDebug($"InGameState CreateChallengeUnlocksData Postfix called");
            Logger.LogDebug(
                $"Challenge Unlock - "
                    + $"Game Mode: {__result.CurrentGameMode} | "
                    + $"Challenge ID: {__result.UnlockedStageID} | "
                    + $"Orbs Earned: {__result.Orbs} | "
                    + $"Challenges Unlocked Count: {__result.UnlockedChallenges?.Count ?? 0}"
            );

            Logger.LogDebug(
                $"Challenge Unlock - "
                    + $"Weapon: {__result.UnlockedWeapon} | "
                    + $"Ultimate: {__result.UnlockedWeaponUltimate} | "
                    + $"Secondary Ultimate: {__result.UnlockedWeaponUltimateSecondary} | "
                    + $"Boon: {__result.UnlockedBoon} | "
                    + $"Skins Unlocked Count: {__result.UnlockedSkins?.Count ?? 0}"
            );

            Logger.LogDebug(
                $"Challenge Unlock - "
                    + $"Sigil Type: {__result.UnlockedSigil} | "
                    + $"Sigil Level: {__result.UnlockedSigilLevel} | "
                    + $"Param Count: {__result.UnlockedSigilsDescriptionParameters?.Count ?? 0}"
            );
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(InGameState.CreateEndlessModeUnlocksData))]
        static bool CreateEndlessModeUnlocksDataPrefix(
            InGameState __instance,
            StageUnlocksData __result
        )
        {
            Logger.LogDebug($"InGameState CreateEndlessModeUnlocksData Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(InGameState.CreateEndlessModeUnlocksData))]
        static void CreateEndlessModeUnlocksDataPostfix(
            InGameState __instance,
            StageUnlocksData __result
        )
        {
            Logger.LogDebug($"InGameState CreateEndlessModeUnlocksData Postfix called");
            Logger.LogDebug(
                $"EndlessMode Unlock - "
                    + $"Game Mode: {__result.CurrentGameMode} | "
                    + $"EndlessMode ID: {__result.UnlockedStageID} | "
                    + $"Orbs Earned: {__result.Orbs} | "
                    + $"Challenges Unlocked Count: {__result.UnlockedChallenges?.Count ?? 0}"
            );

            Logger.LogDebug(
                $"EndlessMode Unlock - "
                    + $"Weapon: {__result.UnlockedWeapon} | "
                    + $"Ultimate: {__result.UnlockedWeaponUltimate} | "
                    + $"Secondary Ultimate: {__result.UnlockedWeaponUltimateSecondary} | "
                    + $"Boon: {__result.UnlockedBoon} | "
                    + $"Skins Unlocked Count: {__result.UnlockedSkins?.Count ?? 0}"
            );

            Logger.LogDebug(
                $"EndlessMode Unlock - "
                    + $"Sigil Type: {__result.UnlockedSigil} | "
                    + $"Sigil Level: {__result.UnlockedSigilLevel} | "
                    + $"Param Count: {__result.UnlockedSigilsDescriptionParameters?.Count ?? 0}"
            );

            Randomizer.LocationTracker.CheckLeviathanCompletion(__result);
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(InGameState.CreateStageUnlocksData))]
        static bool CreateStageUnlocksDataPrefix(
            InGameState __instance,
            bool bossDefeated,
            StageUnlocksData __result
        )
        {
            Logger.LogDebug($"InGameState CreateStageUnlocksData Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(InGameState.CreateStageUnlocksData))]
        static void CreateStageUnlocksDataPostfix(
            InGameState __instance,
            bool bossDefeated,
            StageUnlocksData __result
        )
        {
            Logger.LogInfo($"InGameState CreateStageUnlocksData Postfix called");
            Logger.LogDebug(
                $"Stage Unlock - "
                    + $"Game Mode: {__result.CurrentGameMode} | "
                    + $"Stage ID unlocked: {__result.UnlockedStageID} | "
                    + $"Orbs Earned: {__result.Orbs} | "
                    + $"Challenges Unlocked Count: {__result.UnlockedChallenges?.Count ?? 0}"
            );

            Logger.LogDebug(
                $"Stage Unlock - "
                    + $"Weapon: {__result.UnlockedWeapon} | "
                    + $"Ultimate: {__result.UnlockedWeaponUltimate} | "
                    + $"Secondary Ultimate: {__result.UnlockedWeaponUltimateSecondary} | "
                    + $"Boon: {__result.UnlockedBoon} | "
                    + $"Skins Unlocked Count: {__result.UnlockedSkins?.Count ?? 0}"
            );

            Logger.LogDebug(
                $"Stage Unlock - "
                    + $"Sigil Type: {__result.UnlockedSigil} | "
                    + $"Sigil Level: {__result.UnlockedSigilLevel} | "
                    + $"Param Count: {__result.UnlockedSigilsDescriptionParameters?.Count ?? 0}"
            );
            Randomizer.LocationTracker.CheckStageCompletion(
                __result,
                bossDefeated,
                Randomizer.CurrentLevel
            );
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(InGameState.CreateUnlocksData))]
        static bool CreateUnlocksDataPrefix(
            InGameState __instance,
            bool bossDefeated,
            StageUnlocksData __result
        )
        {
            Logger.LogDebug($"InGameState CreateUnlocksData Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(InGameState.CreateUnlocksData))]
        static void CreateUnlocksDataPostfix(
            InGameState __instance,
            bool bossDefeated,
            StageUnlocksData __result
        )
        {
            Logger.LogInfo($"InGameState CreateUnlocksData Postfix called");
            Logger.LogInfo(
                $"Stage Unlock - "
                    + $"Game Mode: {__result.CurrentGameMode} | "
                    + $"Stage ID unlocked: {__result.UnlockedStageID} | "
                    + $"Orbs Earned: {__result.Orbs} | "
                    + $"Challenges Unlocked Count: {__result.UnlockedChallenges?.Count ?? 0}"
            );

            Logger.LogInfo(
                $"Stage Unlock - "
                    + $"Weapon: {__result.UnlockedWeapon} | "
                    + $"Ultimate: {__result.UnlockedWeaponUltimate} | "
                    + $"Secondary Ultimate: {__result.UnlockedWeaponUltimateSecondary} | "
                    + $"Boon: {__result.UnlockedBoon} | "
                    + $"Skins Unlocked Count: {__result.UnlockedSkins?.Count ?? 0}"
            );

            Logger.LogInfo(
                $"Stage Unlock - "
                    + $"Sigil Type: {__result.UnlockedSigil} | "
                    + $"Sigil Level: {__result.UnlockedSigilLevel} | "
                    + $"Param Count: {__result.UnlockedSigilsDescriptionParameters?.Count ?? 0}"
            );
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(InGameState.EquipSigil))]
        static bool EquipSigilPrefix(InGameState __instance, SigilConfiguration sigilConfig)
        {
            Logger.LogDebug(
                $"InGameState EquipSigil Prefix called for sigil {sigilConfig.Type} level {sigilConfig.Level}"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(InGameState.EquipSigil))]
        static void EquipSigilPostfix(InGameState __instance, SigilConfiguration sigilConfig)
        {
            Logger.LogDebug($"InGameState EquipSigil Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(InGameState._IGameState_Enter_b__96_0))]
        static bool _IGameState_Enter_b__96_0Prefix(InGameState __instance, ProgressionSaveData.SongSelectionLevelData s)
        {
            Logger.LogDebug(
                $"InGameState _IGameState_Enter_b__96_0 Prefix called for level {s.LevelID}, main song {s.MainSongID}, boss song {s.BossSongID}"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(InGameState._IGameState_Enter_b__96_0))]
        static void _IGameState_Enter_b__96_0Postfix(InGameState __instance, ProgressionSaveData.SongSelectionLevelData s)
        {
            Logger.LogDebug($"InGameState _IGameState_Enter_b__96_0 Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(InGameState.CreateSystems))]
        static void CreateSystemsPrefix(InGameState __instance)
        {
            Logger.LogDebug($"InGameState CreateSystems Prefix called");
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(InGameState.CreateSystems))]
        static void CreateSystemsPostfix(InGameState __instance)
        {
            Logger.LogDebug($"InGameState CreateSystems Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(InGameState.GetActiveSigils))]
        static bool GetActiveSigilsPrefix(InGameState __instance, List<SigilConfiguration> sigils)
        {
            Logger.LogInfo(
                $"InGameState GetActiveSigils Prefix called, with {sigils.Count} sigil configs"
            );
            foreach (SigilConfiguration sigil in sigils)
            {
                Logger.LogInfo($"For sigil {sigil.Type} level {sigil.Level}");
            }
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(InGameState.GetActiveSigils))]
        static void GetActiveSigilsPostfix(InGameState __instance)
        {
            Logger.LogDebug($"InGameState GetActiveSigils Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(InGameState.GetUnlockedSigil))]
        static bool GetUnlockedSigilPrefix(InGameState __instance)
        {
            Logger.LogDebug($"InGameState GetUnlockedSigil Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(InGameState.GetUnlockedSigil))]
        static void GetUnlockedSigilPostfix(InGameState __instance, ESigilType __result)
        {
            Logger.LogDebug($"InGameState GetUnlockedSigil Postfix called, returning {__result}");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(InGameState.OnLevelCompleted))]
        static bool OnLevelCompletedPrefix(InGameState __instance, GameManager.EEndCause cause)
        {
            Logger.LogDebug($"InGameState OnLevelCompleted Prefix called with cause {cause}");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(InGameState.OnLevelCompleted))]
        static void OnLevelCompletedPostfix(InGameState __instance)
        {
            Logger.LogDebug($"InGameState OnLevelCompleted Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(InGameState.OnPlayerDied))]
        static bool OnPlayerDiedPrefix(InGameState __instance)
        {
            Logger.LogDebug($"InGameState OnPlayerDied Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(InGameState.OnPlayerDied))]
        static void OnPlayerDiedPostfix(InGameState __instance)
        {
            Logger.LogDebug($"InGameState OnPlayerDied Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(InGameState.SetupSigils))]
        static bool SetupSigilsPrefix(
            InGameState __instance,
            IReadOnlyList<SigilConfiguration> sigilConfigs
        )
        {
            Logger.LogInfo($"InGameState SetupSigils Prefix called ");

            var collectionWrapper = sigilConfigs.Cast<IReadOnlyCollection<SigilConfiguration>>();
            int count = collectionWrapper.Count;
            for (int i = 0; i < count; i++)
            {
                SigilConfiguration sigil = sigilConfigs[i];

                Logger.LogInfo($"For sigil {sigil.Type} level {sigil.Level}");
            }
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(InGameState.SetupSigils))]
        static void SetupSigilsPostfix(
            InGameState __instance,
            IReadOnlyList<SigilConfiguration> sigilConfigs
        )
        {
            Logger.LogDebug($"InGameState SetupSigils Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(InGameState.IGameState_LoadingCompleted))]
        static bool IGameState_LoadingCompletedPrefix(InGameState __instance)
        {
            Logger.LogDebug($"InGameState IGameState_LoadingCompleted Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(InGameState.IGameState_LoadingCompleted))]
        static void IGameState_LoadingCompletedPostfix(InGameState __instance)
        {
            Logger.LogDebug($"InGameState IGameState_LoadingCompleted Postfix called");
            Randomizer.SceneTracker.ResetLevelActiveTime();
            Randomizer.IsPaused = false;
        }
    }

    [HarmonyPatch(typeof(GameManager))]
    public class GameManagerPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(GameManager.PlayerDied))]
        static void PlayerDiedPrefix(GameManager __instance)
        {
            Logger.LogDebug("GameManager PlayerDied Prefix called");
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(GameManager.PlayerDied))]
        static void PlayerDiedPostfix(GameManager __instance)
        {
            Logger.LogDebug("GameManager PlayerDied Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(GameManager.LevelEntered))]
        static void LevelEnteredPrefix(GameManager __instance)
        {
            Logger.LogDebug("GameManager LevelEntered Prefix called");
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(GameManager.LevelEntered))]
        static void LevelEnteredPostfix(GameManager __instance)
        {
            Logger.LogDebug("GameManager LevelEntered Postfix called");
            Randomizer.IsLoadingSongs = true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(GameManager.EnterLevel))]
        static void EnterLevelPrefix(
            GameManager __instance,
            string levelID,
            Il2CppReferenceArray<Il2CppSystem.Object> parametersForNextLevel
        )
        {
            Logger.LogDebug($"GameManager EnterLevel Prefix for {levelID} called");
            Randomizer.IsLoadingDefinition = true;

            if(Randomizer.Configuration.skinsRandomizeOutfits.Value){
                var outfitType = Randomizer.ItemTracker.GetRandomizedOutfit();
                SaveStateManager.SaveData.EquipSkin(SkinTargetType.Outfit, outfitType);
                Randomizer.CurrentOutfit = outfitType;
            }

            if (parametersForNextLevel != null)
            {
                for (int i = 0; i < parametersForNextLevel.Count; i++)
                 {
                    Il2CppSystem.Object parameter = parametersForNextLevel[i];

                    Logger.LogDebug(
                        $"GameManager Entering level with parameter {parameter.GetIl2CppType().FullNameOrDefault}"
                    );
                    if (parameter.GetIl2CppType().FullNameOrDefault == "LevelLoadoutParameters")
                    {
                        LevelLoadoutParameters loadoutParameters =
                            parameter.Cast<LevelLoadoutParameters>();

                        string availableWeaponsStr =
                            loadoutParameters.AvailableWeapons != null
                                ? string.Join(", ", loadoutParameters.AvailableWeapons)
                                : "null";

                        Logger.LogDebug(
                            $"AvailableWeapons: {availableWeaponsStr}, Fav1: {loadoutParameters.fav1}, Fav2: {loadoutParameters.fav2}, EquippedOutfit: {loadoutParameters.EquippedOutfit}"
                        );

                    }
                }
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(GameManager.EnterLevel))]
        static void EnterLevelPostfix(
            GameManager __instance,
            string levelID,
            Il2CppReferenceArray<Il2CppSystem.Object> parametersForNextLevel
        )
        {
            Randomizer.IsLoadingDefinition = false;
            Logger.LogDebug($"GameManager EnterLevel Postfix for {levelID} called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(GameManager.BeginEndOfLevelFlow))]
        static void BeginEndOfLevelFlowPrefix(GameManager __instance, GameManager.EEndCause cause)
        {
            Logger.LogInfo($"GameManager BeginEndOfLevelFlow Prefix called with {cause}");
            Randomizer.LocationTracker.CheckLevelCompletion(cause, Randomizer.CurrentLevel);
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(GameManager.BeginEndOfLevelFlow))]
        static void BeginEndOfLevelFlowPostfix(GameManager __instance, GameManager.EEndCause cause)
        {
            Logger.LogDebug($"GameManager BeginEndOfLevelFlow Postfix called with {cause}");
        }
    }

    [HarmonyPatch(typeof(LoadingView))]
    public class LoadingViewPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(LoadingView.OnGameLoadingComplete))]
        static bool OnGameLoadingCompletePrefix(LoadingView __instance)
        {
            Logger.LogDebug($"LoadingView OnGameLoadingComplete Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(LoadingView.OnGameLoadingComplete))]
        static void OnGameLoadingCompletePostfix(LoadingView __instance)
        {
            Logger.LogDebug($"LoadingView OnGameLoadingComplete Postfix called");
            Randomizer.LocationTracker.SetupLevel(Randomizer.CurrentLevel);
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(LoadingView.EnableContinueButton))]
        static bool EnableContinueButtonPrefix(LoadingView __instance, bool enable)
        {
            Logger.LogDebug($"LoadingView EnableContinueButton Prefix called and is enabled: {enable}");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(LoadingView.EnableContinueButton))]
        static void EnableContinueButtonPostfix(LoadingView __instance, bool enable)
        {
            Logger.LogDebug($"LoadingView EnableContinueButton Postfix called and is enabled: {enable}");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(LoadingView.InitWithContract))]
        static bool InitWithContractPrefix(LoadingView __instance)
        {
            Logger.LogDebug($"LoadingView InitWithContract Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(LoadingView.InitWithContract))]
        static void InitWithContractPostfix(LoadingView __instance)
        {
            Logger.LogDebug($"LoadingView InitWithContract Postfix called");
        }

    }

    [HarmonyPatch(typeof(LevelLoader._LoadLevel_d__0))]
    public class LevelLoaderPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(LevelLoader._LoadLevel_d__0.MoveNext))]
        static bool LoadLevelPrefix(ref LevelLoader._LoadLevel_d__0 __instance)
        {
            LevelDefinition levelDefinition = __instance.levelDefinition;
            Logger.LogInfo("LevelLoader LoadLevel Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(LevelLoader._LoadLevel_d__0.MoveNext))]
        static void LoadLevelPostfix(ref LevelLoader._LoadLevel_d__0 __instance)
        {
            LevelDefinition levelDefinition = __instance.levelDefinition;
            Logger.LogInfo(
                $"LevelLoader LoadLevel Postfix called with state {__instance.__1__state}"
            );

            Logger.LogInfo(
                $"LevelLoader Level loads {levelDefinition.ID} in state {levelDefinition.State} as final level? {levelDefinition.IsFinalLevel}"
            );

            foreach (
                LevelLoadConfiguration.BundleSceneTuple bundleSceneTuple in levelDefinition
                    .LoadSetup
                    .scenes
            )
            {
                Logger.LogInfo(
                    $"LevelLoader Level loads scene with bundle {bundleSceneTuple.bundle} and scene {bundleSceneTuple.scene.SceneName} on path {bundleSceneTuple.scene.ScenePath}"
                );
            }

            Logger.LogInfo($"Level loads with gamemode {levelDefinition.gameplayInfo.GameMode}");
            Logger.LogInfo(
                $"LevelLoader Level loads with weapon unlock {levelDefinition.gameplayInfo.WeaponUnlock}"
            );

            if (levelDefinition.State != GameStateController.GameStateName.Title)
            {
                foreach (PlayerWeaponType weapon in levelDefinition.gameplayInfo.WeaponLoadout)
                {
                    Logger.LogInfo($"LevelLoader Level loads with weapon {weapon} in loadout");
                }
                foreach (PlayerWeaponType weapon in levelDefinition.gameplayInfo.ForbiddenWeapons)
                {
                    Logger.LogInfo($"LevelLoader Level loads with forbidden weapon {weapon}");
                }
            }

            foreach (PlayerWeaponType weapon in levelDefinition.gameplayInfo.ForbiddenWeapons)
            {
                Logger.LogInfo($"LevelLoader Level loads with forbidden weapon {weapon}");
            }

            SongInformation levelMusic = levelDefinition.AudioInfo.LevelMusic;
            Logger.LogInfo(
                $"LevelLoader Level loads with level music {levelMusic.Name} (ID: {levelMusic.ID}), that has a bpm of {levelMusic.BPM}, whatever event is {levelMusic.Event}, bank is {levelMusic.Bank}"
            );
            Randomizer.CurrentMainSong = Randomizer.ItemTracker.GetSongNameById(levelMusic.Name);

            SongInformation bossMusic = levelDefinition.AudioInfo.BossMusic;
            if (bossMusic != null)
            {
                Logger.LogInfo(
                    $"LevelLoader Level loads with boss music {bossMusic.Name} (ID: {bossMusic.ID}), that has a bpm of {bossMusic.BPM}, whatever event is {bossMusic.Event}, bank is {bossMusic.Bank}"
                );
                Randomizer.CurrentBossSong = Randomizer.ItemTracker.GetSongNameById(bossMusic.Name);
            }

            foreach (LevelCollectiblePickupData pickup in levelDefinition.CollectiblePickups)
            {
                Logger.LogInfo(
                    $"LevelLoader Level loads with collectible {pickup.ID} of type {pickup.Type} that is enabled for {pickup.EnabledOnDifficulty}"
                );
            }
        }
    }

    [HarmonyPatch(typeof(ConfigHelper))]
    public class ConfigHelperPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(ConfigHelper.GetLevelDefinition))]
        static bool GetLevelDefinitionPrefix(ref string levelID)
        {
            // Logger.LogDebug($"ConfigHelper GetLevelDefinition Prefix called for {levelID}");

            // if (Randomizer.IsLoadingDefinition)
            // {
            //     var randomizedLevel = Randomizer.ItemTracker.GetRandomizedLevel(levelID);
            //     Logger.LogInfo($"Randomizing level {levelID} to {randomizedLevel}");
            //     levelID = randomizedLevel;
            // }
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(ConfigHelper.GetLevelDefinition))]
        static void GetLevelDefinitionPostfix(string levelID, ref LevelDefinition __result)
        {
            // Logger.LogDebug($"ConfigHelper GetLevelDefinition Postfix called for {levelID}");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(ConfigHelper.GetWeaponDataConfiguration))]
        static bool GetWeaponDataConfigurationPrefix(PlayerWeaponType weaponType)
        {
            Logger.LogInfo(
                $"ConfigHelper GetWeaponDataConfiguration Prefix called for {weaponType}"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(ConfigHelper.GetWeaponDataConfiguration))]
        static void GetWeaponDataConfigurationPostfix(PlayerWeaponType weaponType)
        {
            Logger.LogInfo(
                $"ConfigHelper GetWeaponDataConfiguration Postfix called for {weaponType}"
            );
        }
    }

    [HarmonyPatch(typeof(SoundSystem))]
    public class SoundSystemPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(SoundSystem.Init))]
        static bool InitPrefix(
            Messenger messenger,
            ConfigurationCollection.AudioEventMappingCollection audioEventMappingCollection
        )
        {
            Logger.LogInfo($"SoundSystem Init Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(SoundSystem.Init))]
        static void InitPostfix(
            SoundSystem __instance,
            Messenger messenger,
            ConfigurationCollection.AudioEventMappingCollection audioEventMappingCollection
        )
        {
            Logger.LogInfo($"SoundSystem Init Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(SoundSystem.TryLoadBanksFromMusicData))]
        static bool TryLoadBanksFromMusicDataPrefix(
            Il2CppReferenceArray<MusicData> songs,
            AudioLoader bankLoader
        )
        {
            Logger.LogDebug(
                $"SoundSystem TryLoadBanksFromMusicData Prefix called for {songs.Count} songs"
            );
            for (int i = 0; i < songs.Count; i++)
            {
                var song = songs[i];
                Logger.LogDebug(
                    $"Song {i}, bank: {song.Bank.Name}, bank load mode: {song.Bank.LoadMode}"
                );
            }
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(SoundSystem.TryLoadBanksFromMusicData))]
        static void TryLoadBanksFromMusicDataPostfix(
            Il2CppReferenceArray<MusicData> songs,
            AudioLoader bankLoader
        )
        {
            Logger.LogInfo($"SoundSystem TryLoadBanksFromMusicData Postfix called");
        }
    }

    [HarmonyPatch(typeof(MusicData))]
    public class MusicDataPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(MusicData.CreateFromSongInformation))]
        static bool CreateFromSongInformationPrefix(SongInformation songInfo, ModState modState)
        {
            if(songInfo != null)
                Logger.LogDebug(
                    $"MusicData CreateFromSongInformation Prefix called for {songInfo.Name}, mod state: {modState}"
                );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(MusicData.CreateFromSongInformation))]
        static void CreateFromSongInformationPostfix(
            SongInformation songInfo,
            ModState modState,
            MusicData __result
        )
        {
            Logger.LogDebug($"MusicData CreateFromSongInformation Postfix called");
        }
    }

    [HarmonyPatch(typeof(LevelMusic))]
    public class LevelMusicPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(LevelMusic.CreateFromSongInformation))]
        static bool CreateFromSongInformationPrefix(
            SongInformation mainSong,
            bool mainSongIsModded,
            SongInformation bossSong,
            bool bossSongIsModded
        )
        {
            if(mainSong != null)
                Logger.LogDebug(
                    $"LevelMusic CreateFromSongInformation Prefix called for main song {mainSong.Name} that is modded: {mainSongIsModded}"
                );

            if(bossSong != null)
                Logger.LogDebug(
                    $"LevelMusic CreateFromSongInformation Prefix called for boss song {bossSong.Name} that is modded: {bossSongIsModded}"
                );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(LevelMusic.CreateFromSongInformation))]
        static void CreateFromSongInformationPostfix(
            SongInformation mainSong,
            bool mainSongIsModded,
            SongInformation bossSong,
            bool bossSongIsModded,
            LevelMusic __result
        )
        {
            Logger.LogDebug($"LevelMusic CreateFromSongInformation Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(LevelMusic.CreateFromLevelDefinition))]
        static bool CreateFromLevelDefinitionPrefix(LevelDefinition levelDefinition)
        {
            Logger.LogDebug(
                $"LevelMusic CreateFromLevelDefinition Prefix called for main song {levelDefinition.AudioInfo.LevelMusic.Name} and boss music: {levelDefinition.AudioInfo.BossMusic.Name}"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(LevelMusic.CreateFromLevelDefinition))]
        static void CreateFromLevelDefinitionPostfix(
            LevelDefinition levelDefinition,
            LevelMusic __result
        )
        {
            Logger.LogDebug($"LevelMusic CreateFromLevelDefinition Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(LevelMusic.GetLevelMusicForLevel))]
        static bool GetLevelMusicForLevelPrefix(LevelDefinition levelDefinition)
        {
            if (levelDefinition.AudioInfo != null && levelDefinition.AudioInfo.LevelMusic != null)
                Logger.LogDebug(
                    $"LevelMusic GetLevelMusicForLevel Prefix called for main song {levelDefinition.AudioInfo.LevelMusic.Name}"
                );

            if (levelDefinition.AudioInfo != null && levelDefinition.AudioInfo.BossMusic != null)
                Logger.LogDebug(
                    $"LevelMusic GetLevelMusicForLevel Prefix called for boss music: {levelDefinition.AudioInfo.BossMusic.Name}"
                );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(LevelMusic.GetLevelMusicForLevel))]
        static void GetLevelMusicForLevelPostfix(
            LevelDefinition levelDefinition,
            LevelMusic __result
        )
        {
            Logger.LogDebug($"LevelMusic GetLevelMusicForLevel Postfix called");
        }
    }


    [HarmonyPatch(typeof(LevelScenarioSystem))]
    public static class LevelScenarioSystemPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(LevelScenarioSystem.StartScenario))]
        public static void StartScenarioPrefix(LevelScenario levelScenario)
        {
            if (levelScenario == null)
                return;

            GameObject scenarioGo = levelScenario.gameObject;
            string scenarioName = scenarioGo.name;

            Logger.LogDebug($"LevelScenarioSystem StartScenario Prefix called for scenario {scenarioName}");

            if (scenarioName == "Phase1_Damage Boss")
                Randomizer.LocationTracker.ResetUpCollections();
            else if(Lookup.BossStartScenarioNames.Contains(scenarioName)){
                Randomizer.LocationTracker.CheckSectionCompletion(Randomizer.CurrentPrimary, Randomizer.CurrentSecondary, Randomizer.CurrentOutfit, Randomizer.CurrentMainSong);
                Randomizer.SceneTracker.ResetLevelActiveTime();
            }
            else if(Lookup.BossEndScenarioNames.Contains(scenarioName)){
                Randomizer.LocationTracker.CheckSectionCompletion(Randomizer.CurrentPrimary, Randomizer.CurrentSecondary, Randomizer.CurrentOutfit, Randomizer.CurrentBossSong);
                Randomizer.IsPaused = true;
            }
        }
    }
}

