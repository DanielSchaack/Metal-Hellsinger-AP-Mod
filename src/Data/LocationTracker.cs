using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Outsiders.GUI;
using UnityEngine;
using static Randomizer.Locations;
using static Randomizer.Lookup;
using static Randomizer.Settings;

namespace Randomizer
{
    public class LocationTracker
    {
        public List<Location> LocationsCollected = new List<Location>();
        public Dictionary<string, bool> CheckedLocations = new Dictionary<string, bool>();

        public LocationTracker()
        {
            Reset();
        }

        public void Reset()
        {
            LocationsCollected.Clear();
            CheckedLocations.Clear();
        }

        private List<Location> NextMultipliers = new List<Location>();
        private List<Location> MaxMultiplier = new List<Location>();
        private List<Location> SecretMultipliers = new List<Location>();
        private List<Location> CoatOfArmsPickups = new List<Location>();
        private List<Location> WeaponPickups = new List<Location>();
        private List<Location> AnguishGates = new List<Location>();
        private List<Location> Ammostashes = new List<Location>();
        private List<Location> HealthCrystals = new List<Location>();
        private List<Location> ChaosCrystals = new List<Location>();

        public void SetupLevel(string levelId)
        {
            Logger.LogInfo($"Setting up collectibles from Level {levelId}");
            ClearCollections();

            if (!Lookup.LevelIdToZoneDictionary.ContainsKey(levelId))
            {
                Logger.LogInfo(
                    $"Level {levelId} does not contain any locations to pickup, skipping setup"
                );
                return;
            }

            EZone zone = Lookup.LevelIdToZoneDictionary[levelId];
            var zoneLocations = Locations.LocationDataByName.Values.Where(loc => loc.Zone == zone);

            NextMultipliers = zoneLocations
                .Where(loc => loc.LocationType == ELocationType.NextMultiplier)
                .ToList();
            MaxMultiplier = zoneLocations
                .Where(loc => loc.LocationType == ELocationType.MaxMultiplier)
                .ToList();
            SecretMultipliers = zoneLocations
                .Where(loc => loc.LocationType == ELocationType.SecretMultiplier)
                .ToList();
            AnguishGates = zoneLocations
                .Where(loc => loc.LocationType == ELocationType.AnguishGate)
                .ToList();
            WeaponPickups = zoneLocations
                .Where(loc => loc.LocationType == ELocationType.WeaponPickup)
                .ToList();
            Ammostashes = zoneLocations
                .Where(loc => loc.LocationType == ELocationType.Ammostash)
                .ToList();
            HealthCrystals = zoneLocations
                .Where(loc => loc.LocationType == ELocationType.HealthCrystal)
                .ToList();
            ChaosCrystals = zoneLocations
                .Where(loc => loc.LocationType == ELocationType.ChaosCrystal)
                .ToList();

            SetupCollections();
        }

        private void ClearCollections()
        {
            NextMultipliers.Clear();
            MaxMultiplier.Clear();
            SecretMultipliers.Clear();
            AnguishGates.Clear();
            WeaponPickups.Clear();
            Ammostashes.Clear();
            HealthCrystals.Clear();
            ChaosCrystals.Clear();
            CoatOfArmsPickups.Clear();
            Logger.LogInfo("Cleared all location lists");
        }

        private void SetupCollections()
        {
            SetupCollectionReferences(NextMultipliers, ELocationType.NextMultiplier);
            SetupCollectionReferences(MaxMultiplier, ELocationType.MaxMultiplier);
            SetupCollectionReferences(SecretMultipliers, ELocationType.SecretMultiplier);
            SetupCollectionReferences(AnguishGates, ELocationType.AnguishGate);
            SetupCollectionReferences(WeaponPickups, ELocationType.WeaponPickup);
            SetupCollectionReferences(Ammostashes, ELocationType.Ammostash);
            SetupCollectionReferences(HealthCrystals, ELocationType.HealthCrystal);
            SetupCollectionReferences(ChaosCrystals, ELocationType.ChaosCrystal);
            Logger.LogInfo("Setup all pickup locations");
        }

        // Used to reload on Akkeron boss to activate the mults pickups
        public void ResetUpCollections()
        {
            SetupCollectionReferences(NextMultipliers, ELocationType.NextMultiplier);
            SetupCollectionReferences(MaxMultiplier, ELocationType.MaxMultiplier);
            Logger.LogInfo("Re-setup mult pickup locations");
        }

        private void SetupCollectionReferences(List<Location> locations, ELocationType locationType)
        {
            if (locations == null)
                return;

            for (int i = 0; i < locations.Count; i++)
            {
                Location location = locations[i];
                Logger.LogInfo($"Location {location.LocationId} is being checked for collecting");

                location.IsCollected = CheckedLocations.ContainsKey(location.LocationId);

                if (
                    Locations.GameObjectTypes.Contains(locationType)
                    && location.GameObjectName != null
                )
                {
                    location.LoadedGameObject = GameObject.Find(location.GameObjectName);

                    if (location.LoadedGameObject == null)
                    {
                        Logger.LogWarning(
                            $"Could not find main object for location: {location.LocationId}, looked at {location.GameObjectName}"
                        );
                        continue;
                    }
                    else
                    {
                        location.LoadedGameObject.name = location.LocationId;
                        Logger.LogInfo($"Location {location.LocationId} has its gameObject loaded");
                    }
                }

                if (Locations.TypesWithReferences.Contains(locationType))
                {
                    location.ReferenceGameObject = GameObject.Find(
                        location.ReferenceGameObjectName
                    );
                    if (location.ReferenceGameObject == null)
                    {
                        Logger.LogWarning(
                            $"Could not find reference object for location: {location.LocationId}, looked at {location.ReferenceGameObjectName}"
                        );
                        continue;
                    }
                    Logger.LogInfo(
                        $"Location {location.LocationId} has its reference gameObject loaded"
                    );
                }

                if (location.IsCollected)
                {
                    Logger.LogInfo($"Location {location.LocationId} is already collected");
                    locations[i] = location;
                    continue;
                }

                if (
                    IsLocationTypeRandomized(location.LocationType)
                    && !location.IsSetupForCollection
                )
                {
                    if (Locations.MultiplierTypes.Contains(location.LocationType))
                    {
                        Transform defaultSkin = location.LoadedGameObject.transform.Find(
                            "DefaultSkin"
                        );
                        Transform morningStar = location.LoadedGameObject.transform.Find(
                            "MorningStarSkin"
                        );
                        Transform blackMetal = location.LoadedGameObject.transform.Find(
                            "BlackMetalSkin"
                        );
                        if (defaultSkin != null)
                            defaultSkin.gameObject.SetActive(true);
                        if (blackMetal != null)
                            blackMetal.gameObject.SetActive(true);
                        if (morningStar != null)
                            morningStar.gameObject.SetActive(true);
                    }
                    location.IsSetupForCollection = true;
                    Logger.LogInfo($"Location {location.LocationId} is ready for collecting");
                }

                locations[i] = location;
            }
        }

        private static bool IsLocationTypeRandomized(ELocationType locationType)
        {
            return locationType switch
            {
                ELocationType.NextMultiplier => Randomizer.Settings.HellsNextMultiplierEnabled,
                ELocationType.MaxMultiplier => Randomizer.Settings.HellsMaxMultiplierEnabled,
                ELocationType.SecretMultiplier => Randomizer.Settings.HellsSecretMultiplierEnabled,
                ELocationType.ChallengePickup => false, // only for tracking
                ELocationType.WeaponPickup => true, // required for check count
                ELocationType.CoatOfArms => Randomizer.Settings.HellsCoatOfArmsEnabled || Randomizer.Settings.RequireCoatOfArmsForSheol,
                ELocationType.AnguishGate => true, // maybe in the future adjustable, for now for check count
                ELocationType.Ammostash => false, // only for tracking destructibles
                ELocationType.HealthCrystal => false, // only for tracking destructibles
                ELocationType.ChaosCrystal => false, // only for tracking destructibles
                ELocationType.FirstMiscellaneous => Randomizer.Settings.IncludeMiscellaneousChecks, // collection of individual locations
                ELocationType.Boon => Randomizer.Settings.RandomizedBoonsEnabled,
                ELocationType.Bestiary => true, // required for check count
                ELocationType.Codex => true, // required for check count
                ELocationType.LevelCompletion => Randomizer.Settings.RandomizedHellsEnabled,
                ELocationType.LevelAmmostashCompletion => Randomizer.Settings.RandomizedChallengesEnabled,
                ELocationType.LevelHealthCrystalCompletion => Randomizer.Settings.RandomizedChallengesEnabled,
                ELocationType.LevelChaosCrystalCompletion => Randomizer.Settings.RandomizedChallengesEnabled,
                ELocationType.LevelSpeed => Randomizer.Settings.HellsCoatOfArmsEnabled,
                ELocationType.ArenaAmmostashCompletion => Randomizer.Settings.DestructibleLocationsEnabled && Randomizer.Settings.DestructibleLocationsMode == DestructibleMode.PerDestructibleType,
                ELocationType.ArenaHealthCrystalCompletion => Randomizer.Settings.DestructibleLocationsEnabled && Randomizer.Settings.DestructibleLocationsMode == DestructibleMode.PerDestructibleType,
                ELocationType.ArenaChaosCrystalCompletion => Randomizer.Settings.DestructibleLocationsEnabled && Randomizer.Settings.DestructibleLocationsMode == DestructibleMode.PerDestructibleType,
                ELocationType.ArenaDestructibleCompletion => Randomizer.Settings.DestructibleLocationsEnabled && Randomizer.Settings.DestructibleLocationsMode == DestructibleMode.PerEntireArena,
                ELocationType.SectionClearMainSong => Randomizer.Settings.RandomizedSongsEnabled,
                ELocationType.SectionClearBossSong => Randomizer.Settings.RandomizedSongsEnabled,
                ELocationType.SectionClearWeapon => Randomizer.Settings.HellsRandomizedWeaponsEnabled,
                ELocationType.SectionClearOutfit => Randomizer.Settings.RandomizedOutfitsEnabled,
                ELocationType.WeaponSkin => Randomizer.Settings.HellsRandomizedWeaponSkinsEnabled,
                ELocationType.TormentBronze => Randomizer.Settings.ChallengeMedaillonsEnabled,
                ELocationType.TormentSilver => Randomizer.Settings.ChallengeMedaillonsEnabled,
                ELocationType.TormentGold => Randomizer.Settings.ChallengeMedaillonsEnabled,
                ELocationType.TormentCompletion => Randomizer.Settings.RandomizedChallengesEnabled,
                ELocationType.BossAchievement => Randomizer.Settings.RequireHellsCompletion,
                ELocationType.XpEgg => false, // TODO: Leviathan integration
                ELocationType.NightmareCrystal => false, // TODO: Leviathan integration
                _ => false,
            };
        }

        public void CheckMultiplierPickups()
        {
            IsPickupCollected(NextMultipliers);
            IsPickupCollected(MaxMultiplier);
            IsPickupCollected(SecretMultipliers);
        }

        public void CheckWeaponPickups(PlayerWeaponType weaponType)
        {
            string weaponName = Randomizer.ItemTracker.GetWeaponNameByType(weaponType);
            Logger.LogInfo($"Checking weapon pickup for {weaponName}");

            for (int i = 0; i < WeaponPickups.Count; i++)
            {
                Location location = WeaponPickups[i];

                if (location.IsCollected)
                    continue;

                if (location.IsSetupForCollection && location.OriginalItemName.Equals(weaponName))
                {
                    location.IsCollected = true;
                    CollectLocation(location, IsLocationTypeRandomized(location.LocationType));
                }
                WeaponPickups[i] = location;
            }
        }

        private void IsPickupCollected(List<Location> locations)
        {
            for (int i = 0; i < locations.Count; i++)
            {
                Location location = locations[i];

                if (location.IsCollected)
                    continue;

                // if no longer active then it is collected
                if (location.IsSetupForCollection && !location.LoadedGameObject.activeSelf)
                {
                    location.IsCollected = true;
                    CollectLocation(location, IsLocationTypeRandomized(location.LocationType));
                }
                locations[i] = location;
            }
        }

        private void CollectLocation(
            Location location,
            bool isRandomized = false,
            bool isResync = false
        )
        {
            Logger.LogInfo(
                $"Checking location {location.LocationId}, which is randomized: {isRandomized}"
            );

            if (!CheckedLocations.ContainsKey(location.LocationId))
            {
                Logger.LogInfo($"Location {location.LocationId} is new, adding and sending check");

                LocationsCollected.Add(location);
                CheckedLocations.Add(location.LocationId, true);
                Randomizer.Archipelago.CompleteLocationCheck(location);
                if (isRandomized)
                    IngameMessagesPatches.DisplayCheckCollected($"{location.Description}");
                else
                {
                    if (!isResync)
                        Randomizer.Archipelago.SynchronizeNotRandomizedLocation(
                            LocationsCollected.ToArray()
                        );

                    if (Items.ItemDataByName.ContainsKey(location.OriginalItemName))
                        Randomizer.ItemTracker.SetCollectedItem(
                            Items.ItemDataByName[location.OriginalItemName].ArchipelagoId,
                            null,
                            false,
                            true
                        );
                }

                CheckGoalCompletion();
            }
        }

        private void CheckGoalCompletion()
        {
            bool IsHellsRelevant =
                Randomizer.Settings.RequireHellsCompletion
                || Randomizer.Settings.RequireSheolCompletion;
            bool IsLeviathanRelevant = Randomizer.Settings.RequireLeviathanCompletion;
            bool IsAspectsDone =
                Randomizer.ItemTracker.GetBossesDefeated(ItemGamemode.HELL).Count
                >= Randomizer.Settings.RequiredHellsCompletion;
            bool IsRedJudgeDefeated = Randomizer
                .ItemTracker.GetBossesDefeated(ItemGamemode.HELL)
                .Contains("Red Judge - Worldbreaker: Sheol defeated");
            bool IsHellsDone =
                (!Randomizer.Settings.RequireHellsCompletion || IsAspectsDone)
                && (!Randomizer.Settings.RequireSheolCompletion || IsRedJudgeDefeated);
            bool IsLeviathanDone =
                Randomizer.ItemTracker.GetBossesDefeated(ItemGamemode.LEVIATHAN).Count == 1;

            if (
                !Randomizer.Archipelago.sentCompletion
                && (!IsHellsRelevant || IsHellsDone)
                && (IsLeviathanRelevant || IsLeviathanDone)
            )
                Randomizer.Archipelago.SendCompletion();
        }

        public void CheckAnguishGates(string anguishGateName)
        {
            Logger.LogInfo(
                $"Checking for anguish gate '{anguishGateName}' in {AnguishGates.Count} gates"
            );

            for (int i = 0; i < AnguishGates.Count; i++)
            {
                Location location = AnguishGates[i];

                if (!location.LocationId.Equals(anguishGateName))
                    continue;

                Logger.LogDebug($"'{anguishGateName}'-'{location.LocationId}'");
                if (location.ReferenceGameObject != null)
                    location.ReferenceGameObject.SetActive(false);

                if (location.IsCollected)
                {
                    Logger.LogInfo($"Location '{location.LocationId}' is already collected");
                    continue;
                }

                location.IsCollected = true;
                CollectLocation(location, IsLocationTypeRandomized(location.LocationType));

                AnguishGates[i] = location;
            }
        }

        public void CheckCoatOfArms(string id)
        {
            if (Lookup.CoatOfArmToLocationName.TryGetValue(id, out var locationName))
            {
                Location location = LocationDataByName[locationName];
                CollectLocation(location, IsLocationTypeRandomized(location.LocationType));
            }
        }

        public void CheckWorldItem(string id)
        {
            if (Lookup.WorldItemToLocationName.TryGetValue(id, out var locationName))
            {
                Location location = LocationDataByName[locationName];
                CollectLocation(location, IsLocationTypeRandomized(location.LocationType));
            }
        }

        public void CheckFuryCombo(EFuryComboType combo)
        {
            if (Lookup.FuryComboToLocationName.TryGetValue(combo, out var locationName))
            {
                Location location = LocationDataByName[locationName];
                CollectLocation(location, IsLocationTypeRandomized(location.LocationType));
            }
        }

        public bool IsDestructible(DestructibleObject destructible)
        {
            bool IsDestructible = true;
            bool HasNoTomorrow = true;
            string destructibleName =
                destructible.Root != null ? destructible.Root.name : destructible.name;
            if (destructibleName.EndsWith("Anguish Gate 4"))
                IsDestructible = Randomizer.ItemTracker.HasAspectOfLevel(Randomizer.CurrentLevel);
            if (destructibleName.Equals("Sheol Anguish Gate 4"))
                HasNoTomorrow = RequiresAndHasSheolBossSong();
            return IsDestructible
                && HasNoTomorrow
                && Randomizer.ItemTracker.IsDestructible(destructibleName);
        }

        private bool RequiresAndHasSheolBossSong()
        {
            bool hasNoTomorrow =
                !Randomizer.Settings.RequireNoTomorrowForSheol
                || (
                    Randomizer.ItemTracker.Has("No Tomorrow")
                    && Randomizer.CurrentBossSong == "No Tomorrow"
                );
            return hasNoTomorrow;
        }

        internal bool IsSheolUnlocked()
        {
            if (
                !Randomizer.Settings.RequireCoatOfArmsForSheol
                && !Randomizer.Settings.RequireNoTomorrowForSheol
                && !Randomizer.Settings.RequireNumberOfAspectDefeatedForSheol
            )
                return true;
            bool hasCoatOfArms =
                !Randomizer.Settings.RequireCoatOfArmsForSheol
                || Randomizer.ItemTracker.GetCollectedCoatOfArms()
                    >= Randomizer.Settings.RequiredCoatOfArmsForSheol;
            bool hasNoTomorrow =
                !Randomizer.Settings.RequireNoTomorrowForSheol
                || Randomizer.ItemTracker.Has("No Tomorrow");
            bool hasEnoughAspects = 
                !Randomizer.Settings.RequireNumberOfAspectDefeatedForSheol
                || Randomizer.ItemTracker.GetBossesDefeated(ItemGamemode.HELL).Count
                    >= Randomizer.Settings.RequiredAspectDefeatedForSheol;
            return hasCoatOfArms && hasNoTomorrow;
        }

        public void CheckDestructible(string currentLevel, DestructibleObject destructible)
        {
            string destructibleName =
                destructible.Root != null ? destructible.Root.name : destructible.name;

            Logger.LogDebug($"Checking destructible with name {destructibleName}");

            if (destructibleName.Contains("Anguish Gate"))
                CheckAnguishGates(destructible.name);
            else if (destructibleName.Contains("Ammostash"))
                CheckDestructible(destructibleName, Ammostashes);
            else if (destructibleName.Contains("Health Crystal"))
                CheckDestructible(destructibleName, HealthCrystals);
            else if (destructibleName.Contains("Chaos Crystal"))
                CheckDestructible(destructibleName, ChaosCrystals);
        }

        private void CheckDestructible(string destructibleName, List<Location> locations)
        {
            Logger.LogInfo(
                $"Checking for destructible '{destructibleName}' in {locations.Count} destructibles"
            );
            for (int i = 0; i < locations.Count; i++)
            {
                Location location = locations[i];

                if (!location.LocationId.Equals(destructibleName))
                    continue;

                if (location.IsCollected)
                {
                    Logger.LogDebug($"Location '{location.LocationId}' is already collected");
                    continue;
                }

                CheckFirstDestructions(location.LocationType);

                location.IsCollected = true;
                CollectLocation(location, IsLocationTypeRandomized(location.LocationType));
                locations[i] = location;
            }

            if (locations.Count > 0)
            {
                Logger.LogDebug($"Checking for arena and level completions");
                var location = locations[0];
                CheckCompletions(location.Zone, location.Arena, location.LocationType, locations);
                CheckDestructionCompletions(location.Zone, location.Arena, location.LocationType);
                CheckLevelCompletions(location.Zone, location.LocationType);
            }
        }

        private void CheckFirstDestructions(ELocationType locationType)
        {
            if (
                !CheckedLocations.ContainsKey("First Miscellaneous: Ammostash")
                && locationType == ELocationType.Ammostash
            )
            {
                var location = Locations.LocationDataByName["First Miscellaneous: Ammostash"];
                CollectLocation(location, IsLocationTypeRandomized(location.LocationType));
            }
            else if (
                !CheckedLocations.ContainsKey("First Miscellaneous: Health Crystal")
                && locationType == ELocationType.HealthCrystal
            )
            {
                var location = Locations.LocationDataByName["First Miscellaneous: Health Crystal"];
                CollectLocation(location, IsLocationTypeRandomized(location.LocationType));
            }
            else if (
                !CheckedLocations.ContainsKey("First Miscellaneous: Chaos Crystal")
                && locationType == ELocationType.ChaosCrystal
            )
            {
                var location = Locations.LocationDataByName["First Miscellaneous: Chaos Crystal"];
                CollectLocation(location, IsLocationTypeRandomized(location.LocationType));
            }
        }

        private void CheckDestructionCompletions(
            EZone zone,
            EArena arena,
            ELocationType locationType
        )
        {
            var arenaStr = arena.ToString();
            char lastChar = arenaStr[arenaStr.Length - 1];
            var arenaName = char.IsDigit(lastChar) ? $"Arena {lastChar}" : arenaStr;

            var arenaCheckupName = $"{zone} {arenaName} Destructible Completion";

            Logger.LogDebug($"Checking if '{arenaCheckupName}'s requirements has been met");
            if (
                Lookup.RequiredSubCompletionsForArena.TryGetValue(
                    arenaCheckupName,
                    out var arenaLocationIds
                )
                && HasAllCompletions(arenaLocationIds)
                && Locations.LocationDataByName.TryGetValue(arenaCheckupName, out var arenaLocation)
            )
                CollectLocation(
                    arenaLocation,
                    IsLocationTypeRandomized(arenaLocation.LocationType)
                );
        }

        private bool HasAllCompletions(List<string> arenaLocationIds)
        {
            foreach (var locationId in arenaLocationIds)
            {
                if (
                    !CheckedLocations.TryGetValue(locationId, out bool hasCollected)
                    || !hasCollected
                )
                {
                    Logger.LogDebug($"Requirements have not been met");
                    return false;
                }
            }
            Logger.LogDebug($"Requirements has been met");
            return true;
        }

        private void CheckLevelCompletions(EZone zone, ELocationType locationType)
        {
            var locationTypeName = locationType.ToString();
            if (locationType == ELocationType.HealthCrystal)
                locationTypeName = "Health Crystal";
            else if (locationType == ELocationType.ChaosCrystal)
                locationTypeName = "Chaos Crystal";

            var levelCompletionName = $"{zone} {locationTypeName} Destruction";
            if (
                Lookup.RequiredSubCompletionsForArena.TryGetValue(
                    levelCompletionName,
                    out var levelLocationIds
                )
                && HasRequiredCompletions(levelLocationIds)
                && Locations.LocationDataByName.TryGetValue(
                    levelCompletionName,
                    out var levelLocation
                )
            )
                CollectLocation(
                    levelLocation,
                    IsLocationTypeRandomized(levelLocation.LocationType)
                );
        }

        private bool HasRequiredCompletions(List<string> levelLocationIds)
        {
            int count = 0;
            foreach (var locationId in levelLocationIds)
            {
                if (CheckedLocations.TryGetValue(locationId, out bool hasCollected) && hasCollected)
                    count++;
            }
            return count
                >= Math.Min(
                    levelLocationIds.Count,
                    Randomizer.Settings.RequiredDestructionCompletions
                );
        }

        private void CheckCompletions(
            EZone zone,
            EArena arena,
            ELocationType locationType,
            List<Location> locations
        )
        {
            var checkupName = $"{zone} {locationType} {arena}";
            Logger.LogDebug($"Checking if '{checkupName}'s requirements has been met");
            if (
                Lookup.LocationDestructionCountRequired.TryGetValue(
                    checkupName,
                    out var requiredAmount
                )
            )
            {
                var collectedCount = locations.Count(loc =>
                    loc.IsCollected && loc.Arena == arena && loc.LocationType == locationType
                );

                Logger.LogDebug(
                    $"Collected Amount: {collectedCount} - Required Amount: {requiredAmount}"
                );

                if (
                    collectedCount == requiredAmount
                    && Locations.LocationDataByName.TryGetValue(
                        Lookup.LocationDestructionToCompletionId[checkupName],
                        out var location
                    )
                )
                    CollectLocation(location, IsLocationTypeRandomized(location.LocationType));
            }
        }

        public void CheckChallengeProgress(ChallengeTracker.ChallengeResult result, string levelId)
        {
            if (result == ChallengeTracker.ChallengeResult.Fail)
                return;

            string checkBaseName = GetChallengeBaseName(levelId);
            Logger.LogInfo($"Challenge {checkBaseName} achieved {result}");

            Location location = Locations.LocationDataByName[$"{checkBaseName} {result}"];
            CollectLocation(location, IsLocationTypeRandomized(location.LocationType), false);
        }

        public string GetChallengeBaseName(string levelId)
        {
            string displayName = Lookup.ChallengeIdToDisplayDictionary.GetValueOrDefault(levelId, levelId);
            string challengeLevel = levelId[^1].ToString();
            return $"{displayName}: {challengeLevel}";
        }

        public void CheckLevelCompletion(GameManager.EEndCause endCause, string levelId)
        {
            Logger.LogInfo($"Level {levelId} ended due to {endCause}");
            if (
                endCause != GameManager.EEndCause.ChallengePlayed
                && endCause != GameManager.EEndCause.StageCompleted
                && endCause != GameManager.EEndCause.TutorialCompleted
            )
            {
                return;
            }

            if (Randomizer.ItemTracker.IsChallenge(levelId) && HasChallengeAnyResults(levelId))
            {
                string checkBaseName = GetChallengeBaseName(levelId);

                Location location = Locations.LocationDataByName[$"{checkBaseName} Completion"];
                CollectLocation(location, IsLocationTypeRandomized(location.LocationType), false);
            }
            else if (!Randomizer.ItemTracker.IsChallenge(levelId))
            {
                Location location = Locations.LocationDataByName[$"{levelId} Completion"];
                CollectLocation(location, IsLocationTypeRandomized(location.LocationType), false);
            }
        }

        private int ChallengeResults(string levelId)
        {
            string checkBaseName = GetChallengeBaseName(levelId);
            return CheckedLocations.Keys.Count(loc =>
                loc == $"{checkBaseName} Bronze"
                || loc == $"{checkBaseName} Silver"
                || loc == $"{checkBaseName} Gold"
            );
        }

        private bool HasChallengeAnyResults(string levelId)
        {
            string checkBaseName = GetChallengeBaseName(levelId);
            bool hasLocationsCollected = LocationsCollected.Any(loc =>
                loc.LocationId == $"{checkBaseName} Bronze"
                || loc.LocationId == $"{checkBaseName} Silver"
                || loc.LocationId == $"{checkBaseName} Gold"
            );
            return hasLocationsCollected;
        }

        public void CheckStageCompletion(
            StageUnlocksData unlocksData,
            bool bossDefeated,
            string levelId
        )
        {
            Logger.LogInfo($"Level {levelId} ended and has boss defeated: {bossDefeated}");
            if (bossDefeated)
            {
                string checkName = Lookup.LevelToDefeatedBossLocationName[levelId];
                Location location = Locations.LocationDataByName[checkName];
                CollectLocation(location, IsLocationTypeRandomized(location.LocationType), false);
            }
        }

        public void CheckSectionCompletion(
            PlayerWeaponType primaryWeapon,
            PlayerWeaponType secondaryWeapon,
            SkinType equippedOutfit,
            string songName
        )
        {
            Logger.LogInfo(
                $"Section cleared with primary {primaryWeapon}, secondary {secondaryWeapon} and song {songName}"
            );
            if (primaryWeapon != PlayerWeaponType.None)
            {
                var pName = Lookup.GetCurrentWeaponName(primaryWeapon);
                CheckFirstSectionClear(pName);
            }

            if (secondaryWeapon != PlayerWeaponType.None)
            {
                var sName = Lookup.GetCurrentWeaponName(secondaryWeapon);
                CheckFirstSectionClear(sName);
            }

            if (equippedOutfit != SkinType.Corrupted)
            {
                string equippedSkinName = Randomizer.ItemTracker.GetOutfitNameByType(
                    equippedOutfit
                );
                CheckFirstSectionClear(equippedSkinName);
            }

            CheckFirstSectionClear(songName);
        }

        internal void CheckFirstSectionClear(string sectionItem)
        {
            var locationId = $"Section Cleared with: {sectionItem}";
            if (Locations.LocationDataByName.TryGetValue(locationId, out var location))
                CollectLocation(location, IsLocationTypeRandomized(location.LocationType), true);
        }

        public void Resync(ReadOnlyCollection<long> allLocationsChecked)
        {
            List<Location> locations = allLocationsChecked
                .Where(id => Locations.LocationDataById.ContainsKey(id))
                .Select(id => Locations.LocationDataById[id])
                .ToList();

            foreach (var location in locations)
            {
                CollectLocation(location, IsLocationTypeRandomized(location.LocationType), true);
            }
        }

        internal void CheckSkinUnlocks(int coatOfArmsCount)
        {
            int skinLocationCount = GetSkinLocationAmount();
            if (coatOfArmsCount >= 1)
                GrantSkinLocation("Paz");
            if (coatOfArmsCount >= 2)
                GrantSkinLocation("Terminus");
            if (coatOfArmsCount >= 3)
                GrantSkinLocation("Persephone");
            if (coatOfArmsCount >= 4)
                GrantSkinLocation("The Hounds");
            if (coatOfArmsCount >= 5)
                GrantSkinLocation("Vulcan");
            if (coatOfArmsCount >= 6)
                GrantSkinLocation("Hellcrow");
        }

        private void GrantSkinLocation(string weaponName)
        {
            var locationId = $"{weaponName} Weapon Skin Unlock";
            var location = Locations.LocationDataByName[locationId];
            CollectLocation(location, IsLocationTypeRandomized(location.LocationType), true);
        }

        internal void CheckMisc(string itemName)
        {
            var locationId = $"First Miscellaneous: {itemName}";
            if (Locations.LocationDataByName.TryGetValue(locationId, out var location))
                CollectLocation(location, IsLocationTypeRandomized(location.LocationType), true);
        }

        internal int GetSkinLocationAmount()
        {
            int skinLocationCount =
                Randomizer.ItemTracker.GetCollectedCoatOfArms() >= 2
                    ? 1
                    : 0 + Randomizer.ItemTracker.GetCollectedCoatOfArms() / 6;
            Logger.LogInfo($"Returning skin location amount: {skinLocationCount}");
            return skinLocationCount;
        }

        internal List<string> GetItemsWithMissingChecks(List<string> unlockedItems)
        {
            List<string> missingItems = new List<string>();
            foreach (string unlockedItem in unlockedItems)
            {
                var checkName = $"Section Cleared with: {unlockedItem}";
                if (!CheckedLocations.ContainsKey(checkName))
                {
                    missingItems.Add(unlockedItem);
                    Logger.LogDebug(
                        $"Missing Check {checkName}, adding {unlockedItem} to missing items"
                    );
                }
            }
            Logger.LogInfo($"Returning missing items: {string.Join(", ", missingItems)}");
            return missingItems;
        }

        internal bool HasUncheckedSongs()
        {
            bool isUnchecked =
                Randomizer.Settings.RandomizedSongsEnabled
                && (
                    LocationAccessibility.CanReachAny(
                        getUncheckedLocationsByType(ELocationType.SectionClearMainSong)
                    )
                    || LocationAccessibility.CanReachAny(
                        getUncheckedLocationsByType(ELocationType.SectionClearBossSong)
                    )
                );
            Logger.LogInfo($"Has unchecked songs: {isUnchecked}");
            return isUnchecked;
        }

        internal bool HasUncheckedOutfits()
        {
            bool isUnchecked =
                Randomizer.Settings.RandomizedOutfitsEnabled
                && (
                    LocationAccessibility.CanReachAny(
                        getUncheckedLocationsByType(ELocationType.SectionClearOutfit)
                    )
                );
            Logger.LogInfo($"Has unchecked outfits: {isUnchecked}");
            return isUnchecked;
        }

        internal bool HasUncheckedWeapons()
        {
            bool isUnchecked =
                Randomizer.Settings.HellsRandomizedWeaponsEnabled
                && (
                    LocationAccessibility.CanReachAny(
                        getUncheckedLocationsByType(ELocationType.SectionClearWeapon)
                    )
                );
            Logger.LogInfo($"Has unchecked weapons: {isUnchecked}");
            return isUnchecked;
        }

        internal List<PlayerWeaponType> GetUncheckedWeapons(List<PlayerWeaponType> availableWeapons)
        {
            Logger.LogDebug($"Checking if {availableWeapons.Count} are unchecked");
            List<PlayerWeaponType> uncheckedWeapons = new() { };

            if (!Randomizer.Settings.HellsRandomizedWeaponsEnabled)
                return uncheckedWeapons;

            foreach (var weapon in availableWeapons)
            {
                Logger.LogDebug($"Currently checking weapon {weapon}");
                foreach (var name in Lookup.WeaponTypeToAllWeaponNames[weapon])
                {
                    var locationName = $"Section Cleared with: {name}";
                    Logger.LogDebug(locationName);
                    if (
                        !CheckedLocations.ContainsKey(locationName)
                        && LocationAccessibility.CanReach(locationName)
                    )
                        uncheckedWeapons.Add(weapon);
                }
            }
            Logger.LogDebug($"Returning unchecked weapons: {string.Join(", ", uncheckedWeapons)}");
            return uncheckedWeapons;
        }

        private List<Location> getUncheckedLocationsByType(ELocationType type)
        {
            return Locations
                .LocationDataByName.Where(kvp =>
                    kvp.Value.LocationType == type && !CheckedLocations.ContainsKey(kvp.Key)
                )
                .Select(kvp => kvp.Value)
                .ToList();
        }

        internal bool HasUncheckedCodex()
        {
            return LocationAccessibility.CanReachAny(
                getUncheckedLocationsByType(ELocationType.Codex)
            );
        }

        internal bool HasUncheckedBestiary()
        {
            return LocationAccessibility.CanReachAny(
                getUncheckedLocationsByType(ELocationType.Bestiary)
            );
        }

        internal bool HasUncheckedCompanion()
        {
            return HasUncheckedBestiary() || HasUncheckedCodex();
        }

        internal bool HasChecksOpen(string levelID, bool shouldRandomizeLevel)
        {
            string actualLevelId = levelID;
            if (shouldRandomizeLevel)
                actualLevelId = Randomizer.ItemTracker.GetRandomizedLevel(levelID);
            EZone zone = Lookup.LevelIdToEZone[actualLevelId];
            EArena arena = Lookup.LevelIdToArena[actualLevelId];

            var openLocationIds = Randomizer.Archipelago.GetOpenLocations();
            foreach (long id in openLocationIds)
            {
                if (
                    Locations.LocationDataById.TryGetValue(id, out Location location)
                    && location.Zone == zone
                    && arena.HasFlag(location.Arena)
                    && LocationAccessibility.CanReach(location)
                )
                    return true;
            }

            return false;
        }

        internal CollectiblesStageData GetCollectiblesForHells(string levelId)
        {
            Il2CppSystem.Collections.Generic.Dictionary<
                EDifficulty,
                CollectiblesStageData.CollectibleData
            > stageData = new() { };
            stageData.System_Collections_IDictionary_Add(
                (int)EDifficulty.Easy,
                new CollectiblesStageData.CollectibleData(0, 1).BoxIl2CppObject()
            );
            stageData.System_Collections_IDictionary_Add(
                (int)EDifficulty.Medium,
                new CollectiblesStageData.CollectibleData(0, 1).BoxIl2CppObject()
            );
            stageData.System_Collections_IDictionary_Add(
                (int)EDifficulty.Hard,
                new CollectiblesStageData.CollectibleData(0, 1).BoxIl2CppObject()
            );
            stageData.System_Collections_IDictionary_Add(
                (int)EDifficulty.VeryHard,
                new CollectiblesStageData.CollectibleData(0, 1).BoxIl2CppObject()
            );

            return new CollectiblesStageData
            {
                LevelID = levelId,
                DifficultyCollectibleData = stageData,
            };
        }

        internal int GetOpenCoatOfArmsChecks(EZone zone)
        {
            int totalCount = zone == EZone.Global ? 32 : 4;

            Func<Location, bool> predicate =
                zone == EZone.Global
                    ? loc => loc.LocationType == ELocationType.CoatOfArms
                    : loc => loc.LocationType == ELocationType.CoatOfArms && loc.Zone == zone;

            int collectedCount = LocationsCollected
                .Where(predicate)
                .Select(loc => loc.ArchipelagoId)
                .Distinct()
                .Count();

            return totalCount - collectedCount;
        }

        internal bool HasClearedLevel(string levelID)
        {
            string actualLevelName = Lookup.LevelIdToActualName[levelID];
            return CheckedLocations.ContainsKey($"{actualLevelName} Completion");
        }

        internal int GetReachedChallengeMedaillon(string levelID)
        {
            int results = 0;

            if (Randomizer.Settings.ChallengeMedaillonsEnabled)
                results = ChallengeResults(levelID);

            return results;
        }

        internal bool IsOutfitUnchecked(SkinType outfitType)
        {
            if (!Randomizer.ItemTracker.IsOutfitUnlocked(outfitType))
                return false;

            string outfitName = Randomizer.ItemTracker.GetOutfitNameByType(outfitType);
            string locationName = $"Section Cleared with: {outfitName}";
            if (CheckedLocations.ContainsKey(locationName))
                return false;

            bool v = LocationAccessibility.CanReach(locationName);
            Logger.LogInfo($"Is outfit {outfitName} unchecked: {v}");
            return v;
        }

        internal bool IsSongUnchecked(string songName)
        {
            if (!Randomizer.ItemTracker.Has(songName))
                return false;

            string locationName = $"Section Cleared with: {songName}";
            if (CheckedLocations.ContainsKey(locationName))
                return false;

            bool reachable = LocationAccessibility.CanReach(locationName);
            Logger.LogInfo($"Is song {songName} unchecked: {reachable}");
            return reachable;
        }

        internal bool IsWeaponUnchecked(PlayerWeaponType weaponType)
        {
            Logger.LogDebug($"Checking if {weaponType} is unchecked");
            if (!Randomizer.ItemTracker.IsWeaponUnlocked(weaponType))
                return false;

            bool v = GetUncheckedWeapons(new() { weaponType }).Count > 0;
            Logger.LogInfo($"Is weapon {weaponType} unchecked: {v}");
            return v;
        }

        internal List<ExtendedWeaponType> GetUncheckedPersephoneLocations(
            List<ExtendedWeaponType> availablePersephoneTypes
        )
        {
            List<ExtendedWeaponType> missingTypes = new() { };
            foreach (var type in availablePersephoneTypes)
            {
                string name = Lookup.PersephoneTypeToName[type];
                string locationName = $"Section Cleared with: {name}";
                if (
                    !CheckedLocations.ContainsKey(locationName)
                    && LocationAccessibility.CanReach(locationName)
                )
                    missingTypes.Add(type);
            }
            return missingTypes;
        }

        internal List<WeaponType> GetUncheckedHoundsLocations(List<WeaponType> availableTypes)
        {
            List<WeaponType> missingTypes = new() { };
            foreach (var type in availableTypes)
            {
                string name = Lookup.HoundsTypeToName[type];
                string locationName = $"Section Cleared with: {name}";
                if (
                    !CheckedLocations.ContainsKey(locationName)
                    && LocationAccessibility.CanReach(locationName)
                )
                    missingTypes.Add(type);
            }
            return missingTypes;
        }

        internal List<WeaponType> GetUncheckedVulcanLocations(List<WeaponType> availableTypes)
        {
            List<WeaponType> missingTypes = new() { };
            foreach (var type in availableTypes)
            {
                string name = Lookup.VulcanTypeToName[type];
                string locationName = $"Section Cleared with: {name}";
                if (
                    !CheckedLocations.ContainsKey(locationName)
                    && LocationAccessibility.CanReach(locationName)
                )
                    missingTypes.Add(type);
            }
            return missingTypes;
        }

        internal bool IsBestiaryReachable(EnemyClassType classType)
        {
            Logger.LogDebug($"Checking if Bestiary is reachable: {classType}");
            var locationName = Lookup.EnemyClassToLocationName(classType);
            return LocationAccessibility.CanReach(locationName);
        }

        internal bool IsBestiaryUnchecked(EnemyClassType classType)
        {
            Logger.LogDebug($"Checking if Bestiary is checked: {classType}");
            var locationName = EnemyClassToLocationName(classType);
            return !CheckedLocations.ContainsKey(locationName) && IsBestiaryReachable(classType);
        }

        internal bool IsRegionUnchecked(EZone hells)
        {
            var ZoneArenaTuple = Lookup.EZoneToIndividualLevels[hells];
            foreach (var ZoneArena in ZoneArenaTuple)
            {
                if (
                    HasChecksOpen(
                        Lookup.ZoneArenaToName(ZoneArena.Item1, ZoneArena.Item2),
                        Randomizer.Settings.RandomizedLevelsEnabled
                    )
                )
                    return true;
            }
            return false;
        }

        //TODO: Leviathan integration
        public void CheckLeviathanCompletion(StageUnlocksData unlocksData) { }
    }
}
