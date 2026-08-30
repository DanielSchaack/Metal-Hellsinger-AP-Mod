using System;
using System.Collections.Generic;
using HarmonyLib;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppSystem.Collections.Generic;
using Outsiders.GUI;
using TMPro;
using static Randomizer.Locations;

namespace Randomizer
{
    [HarmonyPatch(typeof(StageSelectCampaignView))]
    public class StageSelectCampaignViewPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(StageSelectCampaignView.LevelHasBeenViewed))]
        static bool LevelHasBeenViewedPrefix(LevelCode levelCode)
        {
            Logger.LogDebug(
                $"StageSelectCampaignView LevelHasBeenViewed Prefix for level {levelCode.Level}, sublevel {levelCode.SubLevel} called"
            );
            return false;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(StageSelectCampaignView.LevelHasBeenViewed))]
        static void LevelHasBeenViewedPostfix(LevelCode levelCode)
        {
            Logger.LogDebug($"StageSelectCampaignView LevelHasBeenViewed Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(StageSelectCampaignView.OnLevelSelected))]
        static bool OnLevelSelectedPrefix(string levelID, bool isLocked)
        {
            Logger.LogDebug(
                $"StageSelectCampaignView OnLevelSelected Prefix for {levelID} called and is locked: {isLocked}"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(StageSelectCampaignView.OnLevelSelected))]
        static void OnLevelSelectedPostfix(string levelID, bool isLocked)
        {
            Logger.LogDebug(
                $"StageSelectCampaignView OnLevelSelected Postfix for {levelID} called and is locked: {isLocked}"
            );
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(StageSelectCampaignView.OnShowMenu))]
        static bool OnShowMenuPrefix(ref StageSelectCampaignView __instance)
        {
            Randomizer.IsLoadingHellsSelection = false;
            Logger.LogDebug($"StageSelectCampaignView OnShowMenu Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(StageSelectCampaignView.OnShowMenu))]
        static void OnShowMenuPostfix()
        {
            Logger.LogDebug($"StageSelectCampaignView OnShowMenu Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(StageSelectCampaignView.OnStartLevel))]
        static bool OnStartLevelPrefix(LevelCode levelCode)
        {
            Logger.LogDebug(
                $"StageSelectCampaignView OnStartLevel Prefix for level {levelCode.Level}, sublevel {levelCode.SubLevel} called"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(StageSelectCampaignView.OnStartLevel))]
        static void OnStartLevelPostfix(LevelCode levelCode)
        {
            Logger.LogDebug($"StageSelectCampaignView OnStartLevel Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(StageSelectCampaignView.OnRequestLeaderboard))]
        static bool OnRequestLeaderboardPrefix(LevelCode levelCode)
        {
            Logger.LogDebug(
                $"StageSelectCampaignView OnRequestLeaderboard Prefix for level {levelCode.Level}, sublevel {levelCode.SubLevel} called"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(StageSelectCampaignView.OnRequestLeaderboard))]
        static void OnRequestLeaderboardPostfix(LevelCode levelCode)
        {
            Logger.LogDebug($"StageSelectCampaignView OnRequestLeaderboard Postfix called");
        }
    }

    [HarmonyPatch(typeof(CampaignManager))]
    public class CampaignManagerPatches
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(CampaignManager.BuildStageDataForUI))]
        static void BuildStageDataForUIPostfix(
            ref CampaignManager __instance,
            ref Il2CppReferenceArray<StageData> __result,
            string tutorialID
        )
        {
            Logger.LogDebug($"CampaignManager BuildStageDataForUI Postfix for {tutorialID} called");
            foreach (StageData stage in __result)
            {
                Logger.LogDebug(
                    $"CampaignManager Returning {stage.LevelID} being locked: {stage.Locked}, being cleared before: {stage.Cleared}"
                );
                foreach (ChallengeData challenge in stage.Challenges)
                {
                    Logger.LogDebug(
                        $"CampaignManager Stage {stage.LevelID} includes challenge {challenge.LevelID}, {challenge.LevelCode} with having the required weapons: {challenge.HaveRequiredWeapons} for sigil {challenge.Sigil}. Has reached tier {challenge.TierReached}, unlock order {challenge.UnlockOrder}, locked parent {challenge.LockedParentStageID}, previous locked {challenge.PreviouslyLockedChallengeID}"
                    );
                }
            }
            Randomizer.IsLoadingHellsSelection = true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(CampaignManager.HasStartedCampaign))]
        static void HasStartedCampaignPrefix(
            ref CampaignManager __instance,
            ref bool __result,
            ref bool ignoreTutorialLevel
        )
        {
            Logger.LogDebug(
                $"CampaignManager HasStartedCampaign Prefix for {ignoreTutorialLevel} called, returning {__result.ToString()}"
            );
            ignoreTutorialLevel = true;
            __result = true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(CampaignManager.HasStartedCampaign))]
        static void HasStartedCampaignPostfix(
            ref CampaignManager __instance,
            ref bool __result,
            ref bool ignoreTutorialLevel
        )
        {
            Logger.LogDebug(
                $"CampaignManager HasStartedCampaign Postfix for {ignoreTutorialLevel} called, returning {__result}"
            );
            __result = true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(CampaignManager.IsStageUnlocked))]
        static bool IsStageUnlockedPrefix(
            ref CampaignManager __instance,
            ref bool __result,
            string levelID
        )
        {
            // Logger.LogDebug(
            //     $"CampaignManager IsStageUnlocked Prefix for {levelID} called, returning {__result}"
            // );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(CampaignManager.IsStageUnlocked))]
        static void IsStageUnlockedPostfix(
            ref CampaignManager __instance,
            ref bool __result,
            string levelID
        )
        {
            // Logger.LogDebug(
            //     $"CampaignManager IsStageUnlocked Postfix for {levelID} called, returning {__result}"
            // );
            __result = true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(CampaignManager.IsStageCompleted))]
        static void IsStageCompletedPrefix(
            ref CampaignManager __instance,
            ref bool __result,
            string levelID
        )
        {
            // Logger.LogDebug(
            //     $"CampaignManager IsStageCompleted Prefix for {levelID} called, returning {__result}"
            // );
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(CampaignManager.IsStageCompleted))]
        static void IsStageCompletedPostfix(
            ref CampaignManager __instance,
            ref bool __result,
            string levelID
        )
        {
            // Logger.LogDebug(
            //     $"CampaignManager IsStageCompleted Postfix for {levelID} called, returning {__result}"
            // );
            __result = true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(CampaignManager.IsAnyCompanionItemUnviewed))]
        static void IsAnyCompanionItemUnviewedPrefix(
            ref CampaignManager __instance,
            EndlessModeController endlessmodeController
        )
        {
            Logger.LogDebug($"CampaignManager IsAnyCompanionItemUnviewed Prefix called");
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(CampaignManager.IsAnyCompanionItemUnviewed))]
        static void IsAnyCompanionItemUnviewedPostfix(ref CampaignManager __instance)
        {
            Logger.LogDebug($"CampaignManager IsAnyCompanionItemUnviewed Postfix called");
        }
    }

    [HarmonyPatch(typeof(AlbumStageRow))]
    public class AlbumStageRowPatches
    {
        private static System.Collections.Generic.Dictionary<int, StageData> RowToData = new();

        private static bool IsStageAvailable(string levelID)
        {
            return levelID != "LOCKED" && Randomizer.ItemTracker.HasLevelUnlocked(levelID);
        }

        private static void SetLockMessageLabel(AlbumStageRow __instance, string text)
        {
            Logger.LogInfo($"Setting lock message for {__instance.m_label.text} to \"{text}\"");
            __instance.m_lockMessageContainer.gameObject.SetActive(true);
            var lockLabel =
                __instance.m_lockMessageContainer.GetComponentInChildren<TextMeshProUGUI>();
            lockLabel.text = text;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumStageRow.SetDifficulty))]
        static void SetDifficultyPostfix(ref AlbumStageRow __instance, EDifficulty difficulty)
        {
            Logger.LogDebug(
                $"AlbumStageRow SetDifficulty Postfix for level {__instance.m_label.text} and difficulty {difficulty} called"
            );

            bool isStageAvailable = IsStageAvailable(RowToData[__instance.GetInstanceID()].LevelID);
            __instance.m_lockIconContainer.SetActive(!isStageAvailable);
            __instance.m_unlocked = isStageAvailable;

            if (!__instance.m_unlocked)
            {
                if(!Randomizer.Configuration.archipelagoSpoilLevelNames.Value)
                    __instance.m_label.text = "LOCKED";
                __instance.m_lockIconContainer.SetActive(!__instance.m_unlocked);
                __instance.SetViewedIconVisible(false);
            }
            else
            {
                string actualLevelID = Randomizer.ItemTracker.GetRandomizedLevel(
                    RowToData[__instance.GetInstanceID()].LevelID
                );

                if(actualLevelID == "Sheol")
                    __instance.m_unlocked = Randomizer.LocationTracker.IsSheolUnlocked();
                __instance.m_lockIconContainer.SetActive(!__instance.m_unlocked);

                bool hasClearedLevel = Randomizer.LocationTracker.HasClearedLevel(actualLevelID);
                __instance.m_cleared = hasClearedLevel;
                __instance.m_clearedBadge.gameObject.SetActive(hasClearedLevel);
            }

            Logger.LogInfo($"Stage {__instance.m_label.text} is unlocked: {__instance.m_unlocked}");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumStageRow.ShowLockMessage))]
        static bool ShowLockMessagePrefix(AlbumStageRow __instance)
        {
            Logger.LogDebug("AlbumStageRow ShowLockMessage Prefix called");
            var data = RowToData[__instance.GetInstanceID()];
            string msg = GetStageLockMessage(data.LevelID);
            if (msg != null)
                SetLockMessageLabel(__instance, msg);
            return true;
        }

        private static string GetStageLockMessage(string LevelID)
        {
            string item = Randomizer.Settings.HellsUnlockMode switch
            {
                Settings.HellsMode.Progressive =>
                    $"<b>{Randomizer.ItemTracker.GetProgressiveStagesUntilUnlock(LevelID)}</b> more Progressive Stage(s)",

                Settings.HellsMode.UnlockAsCollectible =>
                    $"the item(s) <b>{string.Join(", ", Randomizer.ItemTracker.GetMissingItemsUntilLevelUnlocked(LevelID))}</b>",

                _ => null,
            };

            if(!string.IsNullOrWhiteSpace(item) && item.EndsWith("<b></b>"))
                item = null;
            item = AlbumChallengeRowPatches.GetSheolLockMessage(LevelID, item);

            return !string.IsNullOrWhiteSpace(item) ? $"Requires {item} to unlock" : null;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumStageRow.OnClick))]
        static bool OnClickPrefix(AlbumStageRow __instance)
        {
            Logger.LogDebug("AlbumStageRow OnClick Prefix called");
            if (!__instance.m_unlocked)
                return true;

            string actualLevelId = Randomizer.ItemTracker.GetRandomizedLevel(
                RowToData[__instance.GetInstanceID()].LevelID
            );
            if (Lookup.IsLeviathanLevelId(actualLevelId))
            {
                TitleStatePatches.Instance.ShowEndlessLobbyScreen();
                return false;
            }

            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumStageRow.OnClick))]
        static void OnClickPostfix(AlbumStageRow __instance)
        {
            Logger.LogDebug("AlbumStageRow OnClick Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumStageRow.SetCollectibleData))]
        static bool SetCollectibleDataPrefix(
            AlbumStageRow __instance,
            CollectiblesStageData.CollectibleData collectibleData
        )
        {
            Logger.LogDebug(
                $"AlbumStageRow SetCollectibleData Prefix called with {collectibleData.CollectedPickupAmount} collected of a total of {collectibleData.TotalPickupsInLevel}"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumStageRow.SetCollectibleData))]
        static void SetCollectibleDataPostfix(AlbumStageRow __instance)
        {
            Logger.LogDebug("AlbumStageRow SetCollectibleData Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumStageRow.Select))]
        static bool SelectPrefix(AlbumStageRow __instance)
        {
            Logger.LogDebug("AlbumStageRow Select Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumStageRow.Select))]
        static void SelectPostfix(AlbumStageRow __instance)
        {
            Logger.LogDebug("AlbumStageRow Select Postfix called");
            bool hasChecksOpen = Randomizer.LocationTracker.HasChecksOpen(
                RowToData[__instance.GetInstanceID()].LevelID,
                Randomizer.Settings.RandomizedLevelsEnabled
            );
            __instance.SetViewedIconVisible(hasChecksOpen);
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumStageRow.SetData))]
        static bool SetDataPrefix(
            AlbumStageRow __instance,
            ref StageData data,
            EDifficulty currentDifficulty
        )
        {
            Logger.LogDebug(
                $"AlbumStageRow SetData Prefix for stage {data.LevelID} and difficulty {currentDifficulty} called"
            );

            if (!RowToData.ContainsKey(__instance.GetInstanceID()))
            {
                RowToData.Add(__instance.GetInstanceID(), data);
                Logger.LogDebug($"AlbumStageRow Adding instance id {__instance.GetInstanceID()}");
            }

            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumStageRow.SetData))]
        static void SetDataPostfix(
            ref AlbumStageRow __instance,
            StageData data,
            EDifficulty currentDifficulty
        )
        {
            Logger.LogDebug(
                $"AlbumStageRow SetData Postfix for stage {data.LevelID} and difficulty {currentDifficulty} called"
            );

            bool isBackContainer =
                __instance.transform.parent != null
                && __instance.transform.parent.GetComponent<AlbumBackContainer>() != null;

            if (isBackContainer)
                return;

            bool isStageAvailable = IsStageAvailable(data.LevelID);
            __instance.m_lockIconContainer.SetActive(!isStageAvailable);
            __instance.m_unlocked = isStageAvailable;

            if (!__instance.m_unlocked)
            {
                if(!Randomizer.Configuration.archipelagoSpoilLevelNames.Value)
                    __instance.m_label.text = "LOCKED";
                else
                {
                    string actualLevelID = Randomizer.ItemTracker.GetRandomizedLevel(data.LevelID);
                    string showcaseName = Lookup.LevelIdToActualName[actualLevelID];
                    __instance.m_label.text = showcaseName.ToUpper();
                }
                __instance.m_lockIconContainer.SetActive(!__instance.m_unlocked);
                __instance.SetViewedIconVisible(false);
            }
            else
            {
                string actualLevelID = Randomizer.ItemTracker.GetRandomizedLevel(data.LevelID);
                string showcaseName = Lookup.LevelIdToActualName[actualLevelID];
                __instance.m_label.text = showcaseName;

                bool hasClearedLevel = Randomizer.LocationTracker.HasClearedLevel(actualLevelID);
                __instance.m_cleared = hasClearedLevel;
                __instance.m_clearedBadge.gameObject.SetActive(hasClearedLevel);
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumStageRow.SetDifficulty))]
        static bool SetDifficultyPrefix(ref AlbumStageRow __instance, EDifficulty difficulty)
        {
            Logger.LogDebug(
                $"AlbumStageRow SetDifficulty Prefix for difficulty {difficulty} called"
            );
            if (__instance.m_unlocked)
            {
                bool hasChecksOpen = Randomizer.LocationTracker.HasChecksOpen(
                    RowToData[__instance.GetInstanceID()].LevelID,
                    Randomizer.Settings.RandomizedLevelsEnabled
                );
                __instance.SetViewedIconVisible(hasChecksOpen);
            }
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumStageRow.SetViewedIconVisible))]
        static bool SetViewedIconVisiblePrefix(AlbumStageRow __instance, bool visible)
        {
            Logger.LogDebug(
                $"AlbumStageRow SetViewedIconVisible Prefix should show icon: {visible} called"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumStageRow.SetViewedIconVisible))]
        static void SetViewedIconVisiblePostfix(AlbumStageRow __instance, bool visible)
        {
            Logger.LogDebug(
                $"AlbumStageRow SetViewedIconVisible Postfix should show icon: {visible} called"
            );
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumStageRow.SetShowNewSongsIcon))]
        static bool SetShowNewSongsIconPrefix(AlbumStageRow __instance, ref bool show)
        {
            bool hasUncheckedSongs = Randomizer.LocationTracker.HasUncheckedSongs();
            show = hasUncheckedSongs;
            Logger.LogDebug(
                $"AlbumStageRow SetShowNewSongsIcon Prefix should show icon: {show} called"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumStageRow.SetShowNewSongsIcon))]
        static void SetShowNewSongsIconPostfix(AlbumStageRow __instance, bool show)
        {
            Logger.LogDebug(
                $"AlbumStageRow SetShowNewSongsIcon Postfix should show icon: {show} called"
            );
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumStageRow.ShowLockMessage))]
        static void ShowLockMessagePostfix(AlbumStageRow __instance)
        {
            Logger.LogDebug("AlbumStageRow ShowLockMessage Postfix called");
        }
    }

    [HarmonyPatch(typeof(AlbumBackContainer))]
    public class AlbumBackContainerPatches
    {
        private static bool LoadRandomizedLevel = true;

        [HarmonyPrefix]
        [HarmonyPatch(
            nameof(AlbumBackContainer.SetData),
            new Type[] { typeof(StageData), typeof(EDifficulty), typeof(CollectiblesStageData) }
        )]
        static bool SetDataStagePrefix(
            AlbumBackContainer __instance,
            ref StageData data,
            EDifficulty currentDifficulty,
            ref CollectiblesStageData collectibleData
        )
        {
            Logger.LogInfo(
                $"AlbumBackContainer SetData StageData Prefix for {data.LevelID} in difficulty {currentDifficulty} called"
            );

            if (LoadRandomizedLevel)
            {
                string actualLevelId = Randomizer.ItemTracker.GetRandomizedLevel(data.LevelID);
                if (Lookup.IsHellsLevelId(actualLevelId))
                {
                    data = Lookup.GetStageDataByLevelId(actualLevelId);
                    collectibleData = Randomizer.LocationTracker.GetCollectiblesForHells(
                        actualLevelId
                    );
                }
                else if (Lookup.IsChallengeLevelId(actualLevelId))
                {
                    LoadRandomizedLevel = false;
                    var challengeData = Lookup.GetChallengeDataByLevelId(actualLevelId);
                    __instance.SetData(challengeData);
                    return false;
                }
            }
            else
                LoadRandomizedLevel = true;
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(
            nameof(AlbumBackContainer.SetData),
            new Type[] { typeof(StageData), typeof(EDifficulty), typeof(CollectiblesStageData) }
        )]
        static void SetDataStagePostfix(
            AlbumBackContainer __instance,
            StageData data,
            EDifficulty currentDifficulty,
            CollectiblesStageData collectibleData
        )
        {
            Logger.LogInfo(
                $"AlbumBackContainer SetData StageData Postfix for {data.LevelID} in difficulty {currentDifficulty} called"
            );
            __instance.m_leaderboardList.gameObject.SetActive(false);
            __instance.m_LoadingIndicator.gameObject.SetActive(false);
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumBackContainer.SetData), new Type[] { typeof(ChallengeData) })]
        static bool SetDataChallengePrefix(AlbumBackContainer __instance, ref ChallengeData data)
        {
            Logger.LogInfo(
                $"AlbumBackContainer SetData ChallengeData Prefix for {data.LevelID} called"
            );

            if (LoadRandomizedLevel)
            {
                string actualLevelId = Randomizer.ItemTracker.GetRandomizedLevel(data.LevelID);
                if (Lookup.IsHellsLevelId(actualLevelId))
                {
                    LoadRandomizedLevel = false;
                    StageData stageData = Lookup.GetStageDataByLevelId(actualLevelId);
                    CollectiblesStageData collectiblesData =
                        Randomizer.LocationTracker.GetCollectiblesForHells(actualLevelId);
                    __instance.SetData(stageData, Randomizer.SelectedDifficulty, collectiblesData);
                    return false;
                }
                else if (Lookup.IsChallengeLevelId(actualLevelId))
                {
                    data = Lookup.GetChallengeDataByLevelId(actualLevelId);
                }
            }
            else
                LoadRandomizedLevel = true;
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumBackContainer.SetData), new Type[] { typeof(ChallengeData) })]
        static void SetDataChallengePostfix(AlbumBackContainer __instance, ChallengeData data)
        {
            Logger.LogInfo(
                $"AlbumBackContainer SetData ChallengeData Postfix for {data.LevelID} called"
            );
            __instance.m_leaderboardList.gameObject.SetActive(false);
            __instance.m_LoadingIndicator.gameObject.SetActive(false);
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumBackContainer.OnPlayButtonClicked))]
        static bool OnPlayButtonClickedPrefix(AlbumBackContainer __instance)
        {
            Logger.LogInfo($"AlbumBackContainer OnPlayButtonClicked Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumBackContainer.OnPlayButtonClicked))]
        static void OnPlayButtonClickedPostfix(AlbumBackContainer __instance)
        {
            Logger.LogInfo($"AlbumBackContainer OnPlayButtonClicked Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumBackContainer.SetLeaderBoardData))]
        static bool SetLeaderBoardDataPrefix(AlbumBackContainer __instance)
        {
            Logger.LogInfo($"AlbumBackContainer SetLeaderBoardData Prefix called");
            return false;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumBackContainer.SetLeaderBoardData))]
        static void SetLeaderBoardDataPostfix(AlbumBackContainer __instance)
        {
            Logger.LogInfo($"AlbumBackContainer SetLeaderBoardData Postfix called");
            __instance.m_leaderboardList.gameObject.SetActive(false);
            __instance.m_LoadingIndicator.gameObject.SetActive(false);
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumBackContainer.Select))]
        static bool SelectPrefix(AlbumBackContainer __instance)
        {
            Logger.LogInfo($"AlbumBackContainer Select Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumBackContainer.Select))]
        static void SelectPostfix(AlbumBackContainer __instance)
        {
            Logger.LogInfo($"AlbumBackContainer Select Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumBackContainer.SetDifficulty))]
        static bool SetDifficultyPrefix(AlbumBackContainer __instance, EDifficulty difficulty)
        {
            Logger.LogInfo(
                $"AlbumBackContainer SetDifficulty Prefix for difficulty {difficulty} called"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumBackContainer.SetDifficulty))]
        static void SetDifficultyPostfix(AlbumBackContainer __instance, EDifficulty difficulty)
        {
            Logger.LogInfo(
                $"AlbumBackContainer SetDifficulty Postfix for difficulty {difficulty} called"
            );
        }
    }

    [HarmonyPatch(typeof(AlbumChallengeRow))]
    public class AlbumChallengeRowPatches
    {
        private static void SetLockMessageLabel(AlbumChallengeRow __instance, string text)
        {
            Logger.LogInfo($"Setting lock message for {__instance.m_data.LevelID} to \"{text}\"");
            __instance.m_lockMessageContainer.gameObject.SetActive(true);
            __instance.m_lockMessageLabel.richText = true;
            __instance.m_lockMessageLabel.text = text;
        }

        private static string GetChallengeLockMessage(string LevelID)
        {
            string item = Randomizer.Settings.RandomizedChallengesEnabled
                ? Randomizer.Settings.ChallengeUnlockMode switch
                {
                    Settings.ChallengeMode.Progressive => GetProgressiveUnlockMessage(LevelID),

                    Settings.ChallengeMode.UnlockAsCollectible =>
                        $"the item(s) <b>{string.Join(", ", Randomizer.ItemTracker.GetMissingItemsUntilLevelUnlocked(LevelID))}</b>",

                    _ => null,
                }
                : null;

            if(!string.IsNullOrWhiteSpace(item) && item.EndsWith("<b></b>"))
                item = null;
            item = GetSheolLockMessage(LevelID, item);

            return item;
        }

        private static string GetProgressiveUnlockMessage(string LevelID)
        {
            string msg = "";

            if (
                Randomizer.Settings.RequireStageForChallenges
                && !Randomizer.ItemTracker.HasHellOfChallenge(LevelID)
            )
            {
                msg += $"<b>{Lookup.ChallengeToHellDictionary[LevelID]}</b>";
            }

            if (!Randomizer.ItemTracker.HasLevelUnlocked(LevelID))
            {
                if (msg.Length > 0)
                    msg += " and ";
                msg +=
                    $"<b>{string.Join(", ", Randomizer.ItemTracker.GetMissingItemsUntilLevelUnlocked(LevelID))}</b>";
            }

            int progressiveUnlockCount = Randomizer.ItemTracker.GetProgressiveChallengesUntilUnlock(
                LevelID
            );
            if (progressiveUnlockCount > 0)
            {
                if (msg.Length > 0)
                    msg += " and ";
                msg +=
                    $"<b>{progressiveUnlockCount}</b> more <b>Progressive {Lookup.ChallengeIdToDisplayDictionary.GetValueOrDefault(LevelID, LevelID)}</b>(s)";
            }
            return msg;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumChallengeRow.SetDifficulty))]
        static bool SetDifficultyPrefix(
            ref AlbumChallengeRow __instance,
            ref EDifficulty difficulty
        )
        {
            string LevelID = __instance.m_data.LevelID;
            var originalDifficulty = difficulty;

            bool isBackContainer =
                __instance.transform.parent != null
                && __instance.transform.parent.GetComponent<AlbumBackContainer>() != null;

            if (isBackContainer)
                return true;

            Logger.LogDebug(
                $"AlbumChallengeRow SetDifficulty Prefix for challenge {LevelID} difficulty {difficulty} called"
            );

            difficulty = difficulty == EDifficulty.VeryHard ? EDifficulty.Hard : difficulty;
            Logger.LogDebug($"AlbumChallengeRow SetDifficulty adjusted difficulty to {difficulty}");

            string actualLevelID = Randomizer.ItemTracker.GetRandomizedLevel(
                LevelID
            );
            string showcaseName = Lookup.LevelIdToActualName[actualLevelID];

            if (Randomizer.Settings.RandomizedChallengesEnabled)
            {
                Logger.LogDebug(
                    $"Randomized challenges enabled, checking if {LevelID} is available"
                );

                __instance.m_unlocked = IsChallengeUnlocked(LevelID, originalDifficulty);

                var data = __instance.m_data;
                data.TierReached = Randomizer.LocationTracker.GetReachedChallengeMedaillon(
                    actualLevelID
                );
                __instance.m_data = data;
            }
            else
            {
                __instance.m_unlocked = false;
            }

            if (!__instance.m_unlocked)
            {
                var lockedLabel = Randomizer.Configuration.archipelagoSpoilLevelNames.Value? showcaseName : "LOCKED";
                __instance.m_nameLabel.text = Randomizer.Settings.RandomizedChallengesEnabled
                    ? lockedLabel.ToUpper()
                    : "NOT INCLUDED";
                __instance.m_lockIconContainer.SetActive(!__instance.m_unlocked);
                __instance.m_tormentConqueredHighlight.gameObject.SetActive(false);
            }
            else
            {
                __instance.m_nameLabel.text = showcaseName.ToUpper();

                if(showcaseName == "Sheol")
                    __instance.m_unlocked = Randomizer.LocationTracker.IsSheolUnlocked();
                __instance.m_lockIconContainer.SetActive(!__instance.m_unlocked);

                __instance.m_tormentConqueredHighlight.gameObject.SetActive(
                    Randomizer.LocationTracker.HasClearedLevel(actualLevelID)
                );

                if (Lookup.IsLeviathanLevelId(actualLevelID))
                {
                    Action<int> managedCallback = (int buttonId) =>
                    {
                        TitleStatePatches.Instance.ShowEndlessLobbyScreen();
                    };

                    Il2CppSystem.Action<int> il2cppCallback = new Action<int>(managedCallback);
                    __instance.SetClickCallback(il2cppCallback);
                }
                __instance.m_lockMessageContainer.gameObject.SetActive(!__instance.m_unlocked);
            }

            var skulls = __instance.transform.Find("Skulls");
            AdjustSkullVisibility(LevelID, __instance.m_unlocked, skulls);
            Logger.LogInfo(
                $"Challenge {LevelID} is available: {__instance.m_unlocked} and has reached tier {__instance.m_data.TierReached}"
            );
            return true;
        }

        private static void AdjustSkullVisibility(
            String levelID,
            bool isUnlocked,
            UnityEngine.Transform skulls
        )
        {
            if (
                skulls != null
                && (
                    Randomizer.Settings.RandomizedLevelsEnabled
                    || !Randomizer.Settings.ChallengeMedaillonsEnabled
                )
            )
                skulls.gameObject.SetActive(false);
            else if (skulls != null && Randomizer.Settings.ChallengeMedaillonsEnabled)
                skulls.gameObject.SetActive(isUnlocked);
        }

        private static bool IsChallengeUnlocked(string LevelID, EDifficulty difficulty)
        {
            return Randomizer.ItemTracker.HasDifficultyUnlocked(difficulty)
                && Randomizer.ItemTracker.HasLevelUnlocked(LevelID);
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumChallengeRow.ShowLockMessage))]
        static bool ShowLockMessagePrefix(AlbumChallengeRow __instance)
        {
            Logger.LogDebug("AlbumChallengeRow ShowLockMessage Prefix called");
            if (Randomizer.Settings.RandomizedChallengesEnabled)
            {
                string item = GetChallengeLockMessage(__instance.m_data.LevelID);
                if (item != null)
                    SetLockMessageLabel(__instance, $"Requires {item} to unlock");
            }
            return true;
        }

        internal static string GetSheolLockMessage(string levelID, string item)
        {
            if (!IsChallengeUnlocked(levelID, Randomizer.SelectedDifficulty))
                return item;

            Logger.LogDebug($"Level {levelID} is unlocked");

            string actualLevelID = Randomizer.ItemTracker.GetRandomizedLevel(levelID);
            string showcaseName = Lookup.LevelIdToActualName[actualLevelID];

            Logger.LogDebug($"Checking if level is actually Sheol: {showcaseName}");

            if (showcaseName == "Sheol" && !Randomizer.LocationTracker.IsSheolUnlocked())
            {
                string missingText = $"<b>{string.Join(" & ", Randomizer.ItemTracker.GetMissingSheolItems())}</b>";

                item = string.IsNullOrWhiteSpace(item) 
                    ? missingText 
                    : $"{item} and {missingText}";
            }

            return item;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumChallengeRow.SetDifficulty))]
        static void SetDifficultyPostfix(AlbumChallengeRow __instance, EDifficulty difficulty)
        {
            if (__instance.m_unlocked)
            {
                bool hasChecksOpen = Randomizer.LocationTracker.HasChecksOpen(
                    __instance.m_data.LevelID,
                    Randomizer.Settings.RandomizedLevelsEnabled
                );
                __instance.SetViewedIconVisible(hasChecksOpen);
                __instance.m_lockIconContainer.SetActive(false);
            }
            Logger.LogDebug(
                $"AlbumChallengeRow SetDifficulty Postfix for difficulty {difficulty} called"
            );
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumChallengeRow.SetViewedIconVisible))]
        static bool SetViewedIconVisiblePrefix(AlbumChallengeRow __instance, bool visible)
        {
            Logger.LogDebug(
                $"AlbumChallengeRow SetViewedIconVisible Prefix should show icon: {visible} called"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumChallengeRow.SetViewedIconVisible))]
        static void SetViewedIconVisiblePostfix(AlbumChallengeRow __instance, bool visible)
        {
            Logger.LogDebug(
                $"AlbumChallengeRow SetViewedIconVisible Postfix should show icon: {visible} called"
            );
        }

        // [HarmonyPrefix]
        // [HarmonyPatch(nameof(AlbumChallengeRow.Select))]
        // static bool SelectPrefix(AlbumChallengeRow __instance)
        // {
        //     Logger.LogDebug(
        //         $"AlbumChallengeRow Select Prefix called"
        //     );
        //     return true;
        // }
        //
        // [HarmonyPostfix]
        // [HarmonyPatch(nameof(AlbumChallengeRow.Select))]
        // static void SelectPostfix(AlbumChallengeRow __instance)
        // {
        //     Logger.LogDebug(
        //         $"AlbumChallengeRow Select Postfix called"
        //     );
        //     bool hasChecksOpen = Randomizer.LocationTracker.HasChecksOpen(
        //         __instance.m_data.LevelID,
        //         Randomizer.Settings.RandomizedLevelsEnabled
        //     );
        //     __instance.SetViewedIconVisible(hasChecksOpen);
        // }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumChallengeRow.ShowLockMessage))]
        static void ShowLockMessagePostfix(AlbumChallengeRow __instance)
        {
            Logger.LogDebug("AlbumChallengeRow ShowLockMessage Postfix called");
        }
    }

    [HarmonyPatch(typeof(AlbumFrontContainer))]
    public class AlbumFrontContainerPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumFrontContainer.AddStageRow))]
        static bool AddStageRowPrefix(
            AlbumFrontContainer __instance,
            Action songSelectionOpenedCallback,
            bool hasNewSongs
        )
        {
            Logger.LogDebug($"AlbumFrontContainer AddStageRow Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumFrontContainer.AddStageRow))]
        static void AddStageRowPostfix(
            AlbumFrontContainer __instance,
            Action songSelectionOpenedCallback,
            bool hasNewSongs
        )
        {
            Logger.LogDebug($"AlbumFrontContainer AddStageRow Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumFrontContainer.AddChallengeRows))]
        static bool AddChallengeRowsPrefix(AlbumFrontContainer __instance)
        {
            Logger.LogDebug($"AlbumFrontContainer AddChallengeRows Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumFrontContainer.AddChallengeRows))]
        static void AddChallengeRowsPostfix(AlbumFrontContainer __instance)
        {
            Logger.LogDebug($"AlbumFrontContainer AddChallengeRows Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumFrontContainer.ChallengeSelected))]
        static bool ChallengeSelectedPrefix(AlbumFrontContainer __instance, int index)
        {
            Logger.LogDebug(
                $"AlbumFrontContainer ChallengeSelected Prefix for index {index} called"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumFrontContainer.ChallengeSelected))]
        static void ChallengeSelectedPostfix(AlbumFrontContainer __instance, int index)
        {
            Logger.LogDebug(
                $"AlbumFrontContainer ChallengeSelected Postfix for index {index} called"
            );
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumFrontContainer.OnRowHighlighted))]
        static bool OnRowHighlightedPrefix(AlbumFrontContainer __instance, int index)
        {
            Logger.LogDebug(
                $"AlbumFrontContainer OnRowHighlighted Prefix for index {index} called"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumFrontContainer.OnRowHighlighted))]
        static void OnRowHighlightedPostfix(AlbumFrontContainer __instance, int index)
        {
            Logger.LogDebug(
                $"AlbumFrontContainer OnRowHighlighted Postfix for index {index} called"
            );
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumFrontContainer.Select))]
        static bool SelectPrefix(AlbumFrontContainer __instance)
        {
            Logger.LogDebug($"AlbumFrontContainer Select Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumFrontContainer.Select))]
        static void SelectPostfix(AlbumFrontContainer __instance)
        {
            Logger.LogDebug($"AlbumFrontContainer Select Postfix called");
            for (int i = 1; i < 4 && i < __instance.m_rows.Count; i++)
            {
                var challengeRow = __instance.m_rows[i].TryCast<AlbumChallengeRow>();
                if (challengeRow.m_unlocked)
                {
                    bool hasChecksOpen = Randomizer.LocationTracker.HasChecksOpen(
                        challengeRow.m_data.LevelID,
                        Randomizer.Settings.RandomizedLevelsEnabled
                    );
                    challengeRow.SetViewedIconVisible(hasChecksOpen);
                    challengeRow.m_lockMessageContainer.gameObject.SetActive(false);
                }
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumFrontContainer.SetDifficulty))]
        static bool SetDifficultyPrefix(AlbumFrontContainer __instance, EDifficulty difficulty)
        {
            Logger.LogDebug(
                $"AlbumFrontContainer SetDifficulty Prefix for difficulty {difficulty} called"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumFrontContainer.SetDifficulty))]
        static void SetDifficultyPostfix(AlbumFrontContainer __instance, EDifficulty difficulty)
        {
            Logger.LogDebug(
                $"AlbumFrontContainer SetDifficulty Postfix for difficulty {difficulty} called"
            );
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumFrontContainer.SetHasNewSongs))]
        static bool SetHasNewSongsPrefix(AlbumFrontContainer __instance, bool hasNewSongs)
        {
            Logger.LogDebug(
                $"AlbumFrontContainer SetHasNewSongs Prefix with new songs: {hasNewSongs} called"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumFrontContainer.SetHasNewSongs))]
        static void SetHasNewSongsPostfix(AlbumFrontContainer __instance, bool hasNewSongs)
        {
            Logger.LogDebug(
                $"AlbumFrontContainer SetHasNewSongs Postfix with new songs: {hasNewSongs} called"
            );
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumFrontContainer.ShowLockMessageOnSelectedRow))]
        static bool ShowLockMessageOnSelectedRowPrefix(AlbumFrontContainer __instance)
        {
            Logger.LogDebug($"AlbumFrontContainer ShowLockMessageOnSelectedRow Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumFrontContainer.ShowLockMessageOnSelectedRow))]
        static void ShowLockMessageOnSelectedRowPostfix(AlbumFrontContainer __instance)
        {
            Logger.LogDebug($"AlbumFrontContainer ShowLockMessageOnSelectedRow Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumFrontContainer.StageSelected))]
        static bool StageSelectedPrefix(AlbumFrontContainer __instance, int index)
        {
            Logger.LogDebug($"AlbumFrontContainer StageSelected Prefix for index {index} called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumFrontContainer.StageSelected))]
        static void StageSelectedPostfix(AlbumFrontContainer __instance, int index)
        {
            Logger.LogDebug($"AlbumFrontContainer StageSelected Postfix for index {index} called");
        }
    }

    [HarmonyPatch(typeof(AlbumItem))]
    public class AlbumItemPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumItem.OnSelectStage))]
        static bool OnSelectStagePrefix(int stageIndex)
        {
            Logger.LogDebug($"AlbumItem OnSelectStage Prefix for index {stageIndex} called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumItem.OnSelectStage))]
        static void OnSelectStagePostfix(int stageIndex)
        {
            Logger.LogDebug($"AlbumItem OnSelectStage Postfix for index {stageIndex} called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumItem.OnSelectChallenge))]
        static bool OnSelectChallengePrefix(int challengeIndex)
        {
            Logger.LogDebug(
                $"AlbumItem OnSelectChallenge Prefix for index {challengeIndex} called"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumItem.OnSelectChallenge))]
        static void OnSelectChallengePostfix(int challengeIndex)
        {
            Logger.LogDebug(
                $"AlbumItem OnSelectChallenge Postfix for index {challengeIndex} called"
            );
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumItem.Reselect))]
        static bool ReselectPrefix()
        {
            Logger.LogDebug($"AlbumItem Reselect Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumItem.Reselect))]
        static void ReselectPostfix()
        {
            Logger.LogDebug($"AlbumItem Reselect Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumItem.ResetSelection))]
        static bool ResetSelectionPrefix()
        {
            Logger.LogDebug($"AlbumItem ResetSelection Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumItem.ResetSelection))]
        static void ResetSelectionPostfix()
        {
            Logger.LogDebug($"AlbumItem ResetSelection Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumItem.SetClickCallback))]
        static bool SetClickCallbackPrefix(Action<int> callback)
        {
            Logger.LogDebug(
                $"AlbumItem SetClickCallback Prefix for method {callback.Method.Name} called"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumItem.SetClickCallback))]
        static void SetClickCallbackPostfix(Action<int> callback)
        {
            Logger.LogDebug(
                $"AlbumItem SetClickCallback Postfix for method {callback.Method.Name} called"
            );
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumItem.SetDifficulty))]
        static bool SetDifficultyPrefix(EDifficulty difficulty)
        {
            Logger.LogDebug($"AlbumItem SetDifficulty Prefix for difficulty {difficulty} called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumItem.SetDifficulty))]
        static void SetDifficultyPostfix(EDifficulty difficulty)
        {
            Logger.LogDebug($"AlbumItem SetDifficulty Postfix for difficulty {difficulty} called");
        }
    }

    [HarmonyPatch(typeof(AlbumList))]
    public class AlbumListPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumList.OnItemClicked))]
        static bool OnItemClickedPrefix(int index)
        {
            Logger.LogDebug($"AlbumList OnItemClicked Prefix for index {index} called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumList.OnItemClicked))]
        static void OnItemClickedPostfix(int index)
        {
            Logger.LogDebug($"AlbumList OnItemClicked Postfix for index {index} called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumList.OnNavigateLeft))]
        static bool OnNavigateLeftPrefix()
        {
            Logger.LogDebug("AlbumList OnNavigateLeft Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumList.OnNavigateLeft))]
        static void OnNavigateLeftPostfix()
        {
            Logger.LogDebug("AlbumList OnNavigateLeft Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumList.OnNavigateRight))]
        static bool OnNavigateRightPrefix()
        {
            Logger.LogDebug("AlbumList OnNavigateRight Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumList.OnNavigateRight))]
        static void OnNavigateRightPostfix()
        {
            Logger.LogDebug("AlbumList OnNavigateRight Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumList.UpdateNavigation))]
        static bool UpdateNavigationPrefix()
        {
            Logger.LogDebug("AlbumList UpdateNavigation Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumList.UpdateNavigation))]
        static void UpdateNavigationPostfix()
        {
            Logger.LogDebug("AlbumList UpdateNavigation Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumList.SelectItem))]
        static bool SelectItemPrefix(int index)
        {
            Logger.LogDebug($"AlbumList SelectItem Prefix for index {index} called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumList.SelectItem))]
        static void SelectItemPostfix(int index)
        {
            Logger.LogDebug($"AlbumList SelectItem Postfix for index {index} called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumList.SetData))]
        static bool SetDataPrefix(
            Il2CppReferenceArray<StageData> data,
            int selectedIndex,
            Action<LevelCode> requestLeaderboardCallback,
            Action<LevelCode> playGameCallback,
            Action refreshLegendCallback,
            Action clearLegendCallback,
            Action<LevelCode> levelViewedCallback,
            UIMaster uiMaster,
            EDifficulty currentDifficulty,
            Il2CppSystem.Collections.Generic.List<CollectiblesStageData> collectiblesData,
            Action<LevelCode> songSelectionOpenedCallback,
            Il2CppReferenceArray<SongPreviewInfo> songPreviewInfo,
            Action<string, bool> onLevelSelectedCallback,
            bool hasNewSongs
        )
        {
            Logger.LogDebug($"AlbumList SetData Prefix for index {selectedIndex} called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumList.SetData))]
        static void SetDataPostfix(
            Il2CppReferenceArray<StageData> data,
            int selectedIndex,
            Action<LevelCode> requestLeaderboardCallback,
            Action<LevelCode> playGameCallback,
            Action refreshLegendCallback,
            Action clearLegendCallback,
            Action<LevelCode> levelViewedCallback,
            UIMaster uiMaster,
            EDifficulty currentDifficulty,
            Il2CppSystem.Collections.Generic.List<CollectiblesStageData> collectiblesData,
            Action<LevelCode> songSelectionOpenedCallback,
            Il2CppReferenceArray<SongPreviewInfo> songPreviewInfo,
            Action<string, bool> onLevelSelectedCallback,
            bool hasNewSongs
        )
        {
            Logger.LogDebug($"AlbumList SetData Postfix for index {selectedIndex} called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumList.SetSelectedIndex))]
        static bool SetSelectedIndexPrefix(int index, bool animate)
        {
            Logger.LogDebug(
                $"AlbumList SetSelectedIndex Prefix for index {index} with animate: {animate} called"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumList.SetSelectedIndex))]
        static void SetSelectedIndexPostfix(int index, bool animate)
        {
            Logger.LogDebug(
                $"AlbumList SetSelectedIndex Postfix for index {index} with animate: {animate} called"
            );
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AlbumList.SetDifficulty))]
        static bool SetDifficultyPrefix(AlbumList __instance, EDifficulty difficulty)
        {
            Logger.LogDebug($"AlbumList SetDifficulty Prefix for difficulty {difficulty} called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AlbumList.SetDifficulty))]
        static void SetDifficultyPostfix(AlbumList __instance, EDifficulty difficulty)
        {
            Logger.LogDebug($"AlbumList SetDifficulty Postfix for difficulty {difficulty} called");
        }
    }

    [HarmonyPatch(typeof(CollectibleProgressBar))]
    public class CollectibleProgressBarPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(CollectibleProgressBar.Init))]
        static bool InitPrefix(ref int currentPickupCount)
        {
            currentPickupCount = Randomizer.ItemTracker.GetCollectedCoatOfArms();
            Logger.LogDebug(
                $"CollectibleProgressBar Init Prefix for {currentPickupCount} pickups called"
            );
            return true;
        }
    }

    [HarmonyPatch(typeof(CollectibleTracker))]
    public class CollectibleTrackerPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(CollectibleTracker.Init))]
        static bool InitPrefix(ref int currentPickupCount, SkinConfiguration skinConfig)
        {
            currentPickupCount = Randomizer.ItemTracker.GetCollectedCoatOfArms();
            Logger.LogDebug(
                $"CollectibleTracker Init Prefix for {currentPickupCount} pickups called"
            );
            return true;
        }
    }

    [HarmonyPatch(typeof(DifficultySelector))]
    public class DifficultySelectorPatches
    {
        public static DifficultySelector Instance;

        public static void UpdateSelection()
        {
            if(Instance != null && !Instance.WasCollected)
                Instance.SetDifficulty(Randomizer.SelectedDifficulty, true);
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(DifficultySelector.SetIsDifficultyLocked))]
        static bool SetIsDifficultyLockedPrefix(
            DifficultySelector __instance,
            EDifficulty difficulty,
            ref bool isLocked
        )
        {
            isLocked = Randomizer.ItemTracker.HasDifficultyUnlocked(difficulty);
            Logger.LogDebug(
                $"DifficultySelector SetIsDifficultyLocked Prefix called for {difficulty} and is locked: {isLocked}"
            );
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(DifficultySelector.IsDifficultyInteractable))]
        static bool IsDifficultyInteractablePrefix(
            DifficultySelector __instance,
            EDifficulty difficulty,
            ref bool __result
        )
        {
            Logger.LogDebug(
                $"DifficultySelector IsDifficultyInteractable Prefix called for {difficulty} and is locked: {__result}"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(DifficultySelector.IsDifficultyInteractable))]
        static void IsDifficultyInteractablePostfix(
            DifficultySelector __instance,
            EDifficulty difficulty,
            ref bool __result
        )
        {
            __result = Randomizer.ItemTracker.HasDifficultyUnlocked(difficulty);
            Logger.LogDebug(
                $"DifficultySelector IsDifficultyInteractable Postfix called for {difficulty} and is locked: {__result}"
            );
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(DifficultySelector.SelectNextDifficulty))]
        static bool SelectNextDifficultyPrefix(DifficultySelector __instance)
        {
            Logger.LogDebug(
                $"DifficultySelector SelectNextDifficulty Prefix called with current difficulty: {__instance.m_difficulty}"
            );

            if (__instance.m_difficulty == EDifficulty.Easy)
            {
                if (Randomizer.ItemTracker.HasDifficultyUnlocked(EDifficulty.Medium))
                    __instance.SetIsMediumDifficulty();
                else if (Randomizer.ItemTracker.HasDifficultyUnlocked(EDifficulty.Hard))
                    __instance.SetIsHardDifficulty();
                else if (Randomizer.ItemTracker.HasDifficultyUnlocked(EDifficulty.VeryHard))
                    __instance.SetIsVeryHardDifficulty();
            }
            else if (__instance.m_difficulty == EDifficulty.Medium)
            {
                if (Randomizer.ItemTracker.HasDifficultyUnlocked(EDifficulty.Hard))
                    __instance.SetIsHardDifficulty();
                else if (Randomizer.ItemTracker.HasDifficultyUnlocked(EDifficulty.VeryHard))
                    __instance.SetIsVeryHardDifficulty();
            }
            else if (__instance.m_difficulty == EDifficulty.Hard)
            {
                if (Randomizer.ItemTracker.HasDifficultyUnlocked(EDifficulty.VeryHard))
                    __instance.SetIsVeryHardDifficulty();
            }

            return false;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(DifficultySelector.SelectNextDifficulty))]
        static void SelectNextDifficultyPostfix(DifficultySelector __instance)
        {
            Logger.LogDebug($"DifficultySelector SelectNextDifficulty Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(DifficultySelector.SelectPreviousDifficulty))]
        static bool SelectPreviousDifficultyPrefix(DifficultySelector __instance)
        {
            Logger.LogDebug(
                $"DifficultySelector SelectPreviousDifficulty Prefix called with current difficulty: {__instance.m_difficulty}"
            );

            if (__instance.m_difficulty == EDifficulty.VeryHard)
            {
                if (Randomizer.ItemTracker.HasDifficultyUnlocked(EDifficulty.Hard))
                    __instance.SetIsHardDifficulty();
                else if (Randomizer.ItemTracker.HasDifficultyUnlocked(EDifficulty.Medium))
                    __instance.SetIsMediumDifficulty();
                else if (Randomizer.ItemTracker.HasDifficultyUnlocked(EDifficulty.Easy))
                    __instance.SetIsEasyDifficulty();
            }
            else if (__instance.m_difficulty == EDifficulty.Hard)
            {
                if (Randomizer.ItemTracker.HasDifficultyUnlocked(EDifficulty.Medium))
                    __instance.SetIsMediumDifficulty();
                else if (Randomizer.ItemTracker.HasDifficultyUnlocked(EDifficulty.Easy))
                    __instance.SetIsEasyDifficulty();
            }
            else if (__instance.m_difficulty == EDifficulty.Medium)
            {
                if (Randomizer.ItemTracker.HasDifficultyUnlocked(EDifficulty.Easy))
                    __instance.SetIsEasyDifficulty();
            }

            return false;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(DifficultySelector.SelectPreviousDifficulty))]
        static void SelectPreviousDifficultyPostfix(DifficultySelector __instance)
        {
            Logger.LogDebug($"DifficultySelector SelectPreviousDifficulty Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(DifficultySelector.RefreshSelection))]
        static bool RefreshSelectionPrefix(DifficultySelector __instance)
        {
            Logger.LogDebug($"DifficultySelector RefreshSelection Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(DifficultySelector.RefreshSelection))]
        static void RefreshSelectionPostfix(ref DifficultySelector __instance)
        {
            Instance = __instance;
            Logger.LogDebug($"DifficultySelector RefreshSelection Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(DifficultySelector.SetDifficulty))]
        static bool SetDifficultyPrefix(
            DifficultySelector __instance,
            EDifficulty difficulty,
            bool invokeCallback
        )
        {
            Logger.LogDebug(
                $"DifficultySelector SetDifficulty Prefix called for {difficulty} and with callback: {invokeCallback}"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(DifficultySelector.SetDifficulty))]
        static void SetDifficultyPostfix(DifficultySelector __instance)
        {
            Logger.LogDebug($"DifficultySelector SetDifficulty Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(DifficultySelector.SetIsEasyDifficulty))]
        static bool SetIsEasyDifficultyPrefix(DifficultySelector __instance)
        {
            Logger.LogDebug($"DifficultySelector SetIsEasyDifficulty Prefix called");
            Randomizer.SelectedDifficulty = EDifficulty.Easy;
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(DifficultySelector.SetIsEasyDifficulty))]
        static void SetIsEasyDifficultyPostfix(DifficultySelector __instance)
        {
            Logger.LogDebug($"DifficultySelector SetIsEasyDifficulty Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(DifficultySelector.SetIsMediumDifficulty))]
        static bool SetIsMediumDifficultyPrefix(DifficultySelector __instance)
        {
            Logger.LogDebug($"DifficultySelector SetIsMediumDifficulty Prefix called");
            Randomizer.SelectedDifficulty = EDifficulty.Medium;
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(DifficultySelector.SetIsMediumDifficulty))]
        static void SetIsMediumDifficultyPostfix(DifficultySelector __instance)
        {
            Logger.LogDebug($"DifficultySelector SetIsMediumDifficulty Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(DifficultySelector.SetIsHardDifficulty))]
        static bool SetIsHardDifficultyPrefix(DifficultySelector __instance)
        {
            Logger.LogDebug($"DifficultySelector SetIsHardDifficulty Prefix called");
            Randomizer.SelectedDifficulty = EDifficulty.Hard;
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(DifficultySelector.SetIsHardDifficulty))]
        static void SetIsHardDifficultyPostfix(DifficultySelector __instance)
        {
            Logger.LogDebug($"DifficultySelector SetIsHardDifficulty Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(DifficultySelector.SetIsVeryHardDifficulty))]
        static bool SetIsVeryHardDifficultyPrefix(DifficultySelector __instance)
        {
            Logger.LogDebug($"DifficultySelector SetIsVeryHardDifficulty Prefix called");
            Randomizer.SelectedDifficulty = EDifficulty.VeryHard;
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(DifficultySelector.SetIsVeryHardDifficulty))]
        static void SetIsVeryHardDifficultyPostfix(DifficultySelector __instance)
        {
            Logger.LogDebug($"DifficultySelector SetIsVeryHardDifficulty Postfix called");
        }
    }

    [HarmonyPatch(typeof(DifficultySelectorItem))]
    public class DifficultySelectorItemPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(DifficultySelectorItem.RefreshGraphics))]
        static bool RefreshGraphicsPrefix(ref DifficultySelectorItem __instance)
        {
            __instance.m_isLocked = !Randomizer.ItemTracker.HasDifficultyForButtonname(
                __instance.name
            );
            __instance.m_lockContainer.SetActive(__instance.m_isLocked);
            Logger.LogDebug(
                $"DifficultySelectorItem RefreshGraphics Prefix called and is locked: {__instance.m_isLocked}"
            );
            return true;
        }
    }

    [HarmonyPatch(typeof(ImageDatabase))]
    public class ImageDatabasePatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(ImageDatabase.GetSprite))]
        static bool GetSpritePrefix(ImageDatabase __instance, string id)
        {
            Logger.LogDebug($"ImageDatabase GetSprite Prefix called for: {id}");
            return true;
        }
    }

    [HarmonyPatch(typeof(StageSelectProgressBar))]
    public class StageSelectProgressBarPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(StageSelectProgressBar.OnItemClicked))]
        static bool OnItemClickedPrefix(StageSelectProgressBar __instance, int index)
        {
            Logger.LogDebug($"StageSelectProgressBar OnItemClicked Prefix called for: {index}");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(StageSelectProgressBar.OnItemClicked))]
        static void OnItemClickedPostfix(StageSelectProgressBar __instance, int index)
        {
            Logger.LogDebug($"StageSelectProgressBar OnItemClicked Postfix called for: {index}");
        }
    }

    [HarmonyPatch(typeof(StageSelectProgressItem))]
    public class StageSelectProgressItemPatches
    {
        private static EZone IndexToZone(int index) =>
            index switch
            {
                0 => EZone.Tutorial,
                1 => EZone.Voke,
                2 => EZone.Stygia,
                3 => EZone.Yhelm,
                4 => EZone.Incaustis,
                5 => EZone.Gehenna,
                6 => EZone.Nihil,
                7 => EZone.Acheron,
                8 => EZone.Sheol,
                _ => EZone.Global,
            };

        [HarmonyPrefix]
        [HarmonyPatch(nameof(StageSelectProgressItem.SetViewedIconVisible))]
        static bool SetViewedIconVisiblePrefix(StageSelectProgressItem __instance, ref bool visible)
        {
            var zone = IndexToZone(__instance.m_index);

            var reachable = LocationAccessibility.CanAccessRegion(zone);
            __instance.gameObject.SetActive(reachable);

            if(reachable)
                visible = Randomizer.LocationTracker.IsRegionUnchecked(zone);
            Logger.LogInfo(
                $"StageSelectProgressItem SetViewedIconVisible Prefix called for item at index {__instance.m_index} and is reachable: {reachable}, is visible: {visible}"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(StageSelectProgressItem.SetViewedIconVisible))]
        static void SetViewedIconVisiblePostfix(StageSelectProgressItem __instance)
        {
            Logger.LogDebug(
                $"StageSelectProgressItem SetViewedIconVisible Postfix called"
            );
        }
    }
}
