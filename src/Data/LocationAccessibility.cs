using System.Linq;
using System.Collections.Generic;
using static Randomizer.Locations;

namespace Randomizer
{
    public class LocationAccessibility
    {
        public static bool CanReach(string locationId)
        {
            if (Locations.LocationDataByName.TryGetValue(locationId, out var location))
                return CanReach(location);
            return false;
        }

        public static bool CanReach(Location location)
        {
            EZone zone = location.Zone;
            EArena arena = location.Arena;
            ELocationType type = location.LocationType;
            string id = location.LocationId;

            bool hasZoneAccess = CanAccessZone(zone, arena);
            if (!hasZoneAccess)
                return false;
            bool hasArenaAccess = CanAccessArena(zone, arena);
            if (!hasArenaAccess)
                return false;
            return CanAccessLocation(zone, arena, type, id);
        }

        public static bool CanReachAny(List<Location> locations)
        {
            foreach (var location in locations)
            {
                if (CanReach(location))
                    return true;
            }
            return false;
        }

        public static bool CanReachAll(List<Location> locations)
        {
            foreach (var location in locations)
            {
                if (!CanReach(location))
                    return false;
            }
            return true;
        }

        internal static bool CanAccessZone(EZone zone, EArena arena){
            return zone switch
            {
                EZone.Global => true,
                EZone.Tutorial => Randomizer.ItemTracker.HasRandomizedLevelUnlocked("Tutorial"),
                EZone.Voke => Randomizer.ItemTracker.HasRandomizedLevelUnlocked("Voke"),
                EZone.Stygia => Randomizer.ItemTracker.HasRandomizedLevelUnlocked("Stygia"),
                EZone.Yhelm => Randomizer.ItemTracker.HasRandomizedLevelUnlocked("Yhelm"),
                EZone.Incaustis => Randomizer.ItemTracker.HasRandomizedLevelUnlocked("Incaustis"),
                EZone.Gehenna => Randomizer.ItemTracker.HasRandomizedLevelUnlocked("Gehenna"),
                EZone.Nihil => Randomizer.ItemTracker.HasRandomizedLevelUnlocked("Nihil"),
                EZone.Acheron => Randomizer.ItemTracker.HasRandomizedLevelUnlocked("Acheron"),
                EZone.Sheol => Randomizer.ItemTracker.HasRandomizedLevelUnlocked("Sheol")
                    && Randomizer.LocationTracker.IsSheolUnlocked(),

                EZone.KillingWithRhythm => CanAccessChallengeArena(zone, arena),
                EZone.Giantslayer => CanAccessChallengeArena(zone, arena),
                EZone.UltimateMastery => CanAccessChallengeArena(zone, arena),
                EZone.SlaughterMastery => CanAccessChallengeArena(zone, arena),
                EZone.RelicThief => CanAccessChallengeArena(zone, arena),
                EZone.WeaponTrickery => CanAccessChallengeArena(zone, arena),
                EZone.DeathsEdge => CanAccessChallengeArena(zone, arena),

                EZone.Leviathan => Randomizer.ItemTracker.HasRandomizedLevelUnlocked("EndlessModeBase"),
                _ => false,
            };
        }

        private static bool CanAccessArena(EZone zone, EArena arena)
        {
            return zone switch
            {
                EZone.Global => true,
                EZone.Tutorial => HasBaseMovement(),
                EZone.Voke => CanAccessVokeArenas(arena),
                EZone.Stygia => CanAccessStygiaArenas(arena),
                EZone.Yhelm => CanAccessYhelmArenas(arena),
                EZone.Incaustis => CanAccessIncaustisArenas(arena),
                EZone.Gehenna => CanAccessGehennaArenas(arena),
                EZone.Nihil => CanAccessNihilArenas(arena),
                EZone.Acheron => CanAccessAcheronArenas(arena),
                EZone.Sheol => CanAccessSheolArenas(arena),

                EZone.KillingWithRhythm => CanAccessChallengeArena(zone, arena),
                EZone.Giantslayer => CanAccessChallengeArena(zone, arena),
                EZone.UltimateMastery => CanAccessChallengeArena(zone, arena),
                EZone.SlaughterMastery => CanAccessChallengeArena(zone, arena),
                EZone.RelicThief => CanAccessChallengeArena(zone, arena),
                EZone.WeaponTrickery => CanAccessChallengeArena(zone, arena),
                EZone.DeathsEdge => CanAccessChallengeArena(zone, arena),

                EZone.Leviathan => CanAccessLeviathanArenas(arena),
                _ => false,
            };
        }

        private static bool CanAccessVokeArenas(EArena arena)
        {
            return arena switch
            {
                EArena.Global => true,
                EArena.Arena1 => true,
                EArena.Arena2 => (
                    Randomizer.ItemTracker.CanDoubleJump() || Randomizer.ItemTracker.CanDash()
                ) && HasGenericArena2Requirements(),
                EArena.Arena3 => (
                    Randomizer.ItemTracker.CanDoubleJump() || Randomizer.ItemTracker.CanDash()
                ) && HasGenericArena3Requirements(),
                EArena.Arena4 => (
                    Randomizer.ItemTracker.CanDoubleJump() || Randomizer.ItemTracker.CanDash()
                ) && HasGenericArena4Requirements(),
                EArena.Boss => HasGenericBossRequirements()
                    && Randomizer.ItemTracker.HasAspectOfLevel("Voke"),
                _ => false,
            };
        }

        private static bool CanAccessStygiaArenas(EArena arena)
        {
            return arena switch
            {
                EArena.Global => true,
                EArena.Arena1 => true,
                EArena.Arena2 => HasGenericArena2Requirements(),
                EArena.Arena3 => HasGenericArena3Requirements(),
                EArena.Arena4 => HasGenericArena4Requirements(),
                EArena.Boss => HasGenericBossRequirements()
                    && Randomizer.ItemTracker.HasAspectOfLevel("Stygia"),
                _ => false,
            };
        }

        private static bool CanAccessYhelmArenas(EArena arena)
        {
            return arena switch
            {
                EArena.Global => true,
                EArena.Arena1 => true,
                EArena.Arena2 => HasGenericArena2Requirements(),
                EArena.Arena3 => HasGenericArena3Requirements(),
                EArena.Arena4 => HasGenericArena4Requirements(),
                EArena.Boss => HasGenericBossRequirements()
                    && Randomizer.ItemTracker.HasAspectOfLevel("Yhelm"),
                _ => false,
            };
        }

        private static bool CanAccessIncaustisArenas(EArena arena)
        {
            return arena switch
            {
                EArena.Global => true,
                EArena.Arena1 => true,
                EArena.Arena2 => HasGenericArena2Requirements(),
                EArena.Arena3 => HasGenericArena3Requirements(),
                EArena.Arena4 => HasGenericArena4Requirements(),
                EArena.Boss => HasGenericBossRequirements()
                    && Randomizer.ItemTracker.HasAspectOfLevel("Incaustis"),
                _ => false,
            };
        }

        private static bool CanAccessGehennaArenas(EArena arena)
        {
            return arena switch
            {
                EArena.Global => true,
                EArena.Arena1 => true,
                EArena.Arena2 => HasBaseMovement() && HasGenericArena2Requirements(),
                EArena.Arena3 => HasGenericArena3Requirements(),
                EArena.Arena4 => HasGenericArena4Requirements(),
                EArena.Boss => HasGenericBossRequirements()
                    && Randomizer.ItemTracker.HasAspectOfLevel("Gehenna"),
                _ => false,
            };
        }

        private static bool CanAccessNihilArenas(EArena arena)
        {
            return arena switch
            {
                EArena.Global => true,
                EArena.Arena1 => true,
                EArena.Arena2 => HasGenericArena2Requirements(),
                EArena.Arena3 => HasGenericArena3Requirements(),
                EArena.Arena4 => HasGenericArena4Requirements(),
                EArena.Boss => HasGenericBossRequirements()
                    && Randomizer.ItemTracker.HasAspectOfLevel("Nihil"),
                _ => false,
            };
        }

        private static bool CanAccessAcheronArenas(EArena arena)
        {
            return arena switch
            {
                EArena.Global => true,
                EArena.Arena1 => true,
                EArena.Arena2 => HasGenericArena2Requirements(),
                EArena.Arena3 => HasGenericArena3Requirements(),
                EArena.Arena4 => HasGenericArena4Requirements(),
                EArena.Boss => HasGenericBossRequirements()
                    && Randomizer.ItemTracker.HasAspectOfLevel("Acheron"),
                _ => false,
            };
        }

        private static bool CanAccessSheolArenas(EArena arena)
        {
            return arena switch
            {
                EArena.Global => true,
                EArena.Arena1 => true,
                EArena.Arena2 => HasGenericArena2Requirements(),
                EArena.Arena3 => HasGenericArena3Requirements(),
                EArena.Arena4 => HasGenericArena4Requirements(),
                EArena.Boss => HasCloseAndLongRangeWeapon()
                    && HasGenericBossRequirements(),
                _ => false,
            };
        }

        private static bool HasCloseAndLongRangeWeapon()
        {
            return HasCloseRangeWeapon() && HasLongRangeWeapon();
        }

        private static bool HasCompleteReloading()
        {
            return Randomizer.ItemTracker.CanQuickReload()
                && Randomizer.ItemTracker.CanManualReload();
        }

        private static bool HasGenericArena2Requirements()
        {
            return HasBaseMovement() && HasNonPazRangedWeapon() && HasAnyHeal();
        }

        private static bool HasGenericArena3Requirements()
        {
            return HasGenericArena2Requirements() && (
                        (HasAllHeal() && Randomizer.ItemTracker.HasDifficultyUnlocked(EDifficulty.VeryHard) && HasAdvancedMovement())
                        || (Randomizer.ItemTracker.HasDifficultyUnlocked(EDifficulty.VeryHard) && HasAdvancedMovement())
                        || IsNotBeast()
                    ) 
                && (HasRangedWeaponWithUltimate() || Randomizer.ItemTracker.CanQuickReload());
        }

        private static bool HasGenericArena4Requirements()
        {
            return HasGenericArena3Requirements() && (
                        (HasAllHeal() && Randomizer.ItemTracker.HasDifficultyUnlocked(EDifficulty.VeryHard) && HasAdvancedMovement()) 
                        || (Randomizer.ItemTracker.HasDifficultyUnlocked(EDifficulty.VeryHard) && HasAdvancedMovement())
                        || IsNotBeast()
                    ) 
                && (HasRangedWeaponWithUltimate() || Randomizer.ItemTracker.CanQuickReload());
        }

        private static bool HasGenericBossRequirements()
        {
            return HasGenericArena4Requirements() && HasAdvancedMovement()
                && HasRangedWeaponWithUltimate() && Randomizer.ItemTracker.CanQuickReload()
                && (IsNotBeast() || HasAllHeal());
        }

        private static bool HasCloseRangeWeapon()
        {
            return Randomizer.ItemTracker.IsWeaponUnlocked(PlayerWeaponType.Falx)
                || Randomizer.ItemTracker.IsWeaponUnlocked(PlayerWeaponType.Shotgun)
                || Randomizer.ItemTracker.IsWeaponUnlocked(PlayerWeaponType.Boomerang);
        }

        private static bool HasLongRangeWeapon()
        {
            return Randomizer.ItemTracker.IsWeaponUnlocked(PlayerWeaponType.Pistols)
                || Randomizer.ItemTracker.IsWeaponUnlocked(PlayerWeaponType.Vulcan)
                || Randomizer.ItemTracker.IsWeaponUnlocked(PlayerWeaponType.AssaultRifle)
                || Randomizer.ItemTracker.IsWeaponUnlocked(PlayerWeaponType.Bow);
        }

        private static bool HasNonPazWeapon()
        {
            return Randomizer.ItemTracker.IsWeaponUnlocked(PlayerWeaponType.Falx)
                || Randomizer.ItemTracker.IsWeaponUnlocked(PlayerWeaponType.Shotgun)
                || Randomizer.ItemTracker.IsWeaponUnlocked(PlayerWeaponType.Pistols)
                || Randomizer.ItemTracker.IsWeaponUnlocked(PlayerWeaponType.Vulcan)
                || Randomizer.ItemTracker.IsWeaponUnlocked(PlayerWeaponType.Boomerang)
                || Randomizer.ItemTracker.IsWeaponUnlocked(PlayerWeaponType.AssaultRifle)
                || Randomizer.ItemTracker.IsWeaponUnlocked(PlayerWeaponType.Bow);
        }

        private static bool HasNonPazRangedWeapon()
        {
            return Randomizer.ItemTracker.IsWeaponUnlocked(PlayerWeaponType.Shotgun)
                || Randomizer.ItemTracker.IsWeaponUnlocked(PlayerWeaponType.Pistols)
                || Randomizer.ItemTracker.IsWeaponUnlocked(PlayerWeaponType.Vulcan)
                || Randomizer.ItemTracker.IsWeaponUnlocked(PlayerWeaponType.Boomerang)
                || Randomizer.ItemTracker.IsWeaponUnlocked(PlayerWeaponType.AssaultRifle)
                || Randomizer.ItemTracker.IsWeaponUnlocked(PlayerWeaponType.Bow);
        }

        private static bool HasRangedWeapon()
        {
            return Randomizer.ItemTracker.IsWeaponUnlocked(PlayerWeaponType.RhythmWeapon)
                || Randomizer.ItemTracker.IsWeaponUnlocked(PlayerWeaponType.Shotgun)
                || Randomizer.ItemTracker.IsWeaponUnlocked(PlayerWeaponType.Pistols)
                || Randomizer.ItemTracker.IsWeaponUnlocked(PlayerWeaponType.Vulcan)
                || Randomizer.ItemTracker.IsWeaponUnlocked(PlayerWeaponType.Boomerang)
                || Randomizer.ItemTracker.IsWeaponUnlocked(PlayerWeaponType.AssaultRifle)
                || Randomizer.ItemTracker.IsWeaponUnlocked(PlayerWeaponType.Bow);
        }

        private static bool HasRangedWeaponWithUltimate()
        {
            return (
                    Randomizer.ItemTracker.IsWeaponUnlocked(PlayerWeaponType.Shotgun)
                    && Randomizer.ItemTracker.CanWeaponUltimate(PlayerWeaponType.Shotgun)
                )
                || (
                    Randomizer.ItemTracker.IsWeaponUnlocked(PlayerWeaponType.Pistols)
                    && Randomizer.ItemTracker.CanWeaponUltimate(PlayerWeaponType.Pistols)
                )
                || (
                    Randomizer.ItemTracker.IsWeaponUnlocked(PlayerWeaponType.Vulcan)
                    && Randomizer.ItemTracker.CanWeaponUltimate(PlayerWeaponType.Vulcan)
                )
                || (
                    Randomizer.ItemTracker.IsWeaponUnlocked(PlayerWeaponType.Boomerang)
                    && Randomizer.ItemTracker.CanWeaponUltimate(PlayerWeaponType.Boomerang)
                )
                || (
                    Randomizer.ItemTracker.IsWeaponUnlocked(PlayerWeaponType.AssaultRifle)
                    && Randomizer.ItemTracker.CanWeaponUltimate(PlayerWeaponType.AssaultRifle)
                )
                || (
                    Randomizer.ItemTracker.IsWeaponUnlocked(PlayerWeaponType.Bow)
                    && Randomizer.ItemTracker.CanWeaponUltimate(PlayerWeaponType.Bow)
                );
        }

        private static bool HasBaseMovement()
        {
            return Randomizer.ItemTracker.CanJump() || Randomizer.ItemTracker.CanDash();
        }

        private static bool HasAdvancedMovement()
        {
            return (Randomizer.ItemTracker.CanJump() && Randomizer.ItemTracker.CanDash()) | Randomizer.ItemTracker.CanSoar() | Randomizer.ItemTracker.CanDoubleJump();
        }

        private static bool IsNotGoatOrCanFullyHeal()
        {
            bool isNotGoat =
                Randomizer.ItemTracker.HasDifficultyUnlocked(EDifficulty.Easy);
            bool canHeal = HasAllHeal();
            return isNotGoat || canHeal;
        }

        private static bool HasAllHeal()
        {
            return Randomizer.ItemTracker.IsDestructible("Health")
                && Randomizer.ItemTracker.CanSlaughter();
        }

        private static bool IsNotBeast()
        {
            return 
                Randomizer.ItemTracker.HasDifficultyUnlocked(EDifficulty.Easy)
                || Randomizer.ItemTracker.HasDifficultyUnlocked(EDifficulty.Medium);
        }

        private static bool HasAnyHeal()
        {
            return Randomizer.ItemTracker.IsDestructible("Health")
                || Randomizer.ItemTracker.CanSlaughter();
        }

        private static bool IsNotArchdevilOrCanHeal()
        {
            bool isNotArchdevil = IsNotArchdevil();
            bool canHeal =
                Randomizer.ItemTracker.IsDestructible("Health")
                || Randomizer.ItemTracker.CanSlaughter();
            return isNotArchdevil || canHeal;
        }

        private static bool IsNotArchdevil()
        {
            return Randomizer.ItemTracker.HasDifficultyUnlocked(EDifficulty.Easy)
                || Randomizer.ItemTracker.HasDifficultyUnlocked(EDifficulty.Medium)
                || Randomizer.ItemTracker.HasDifficultyUnlocked(EDifficulty.Hard);
        }

        private static bool CanAccessChallengeArena(EZone zone, EArena arena)
        {
            string levelId = getChallengeFromZoneAndArena(zone, arena);
            return Randomizer.ItemTracker.HasRandomizedLevelUnlocked(levelId);
        }

        private static string getChallengeFromZoneAndArena(EZone zone, EArena arena)
        {
            int arenaNumber = arena switch
            {
                EArena.Torment1 => 1,
                EArena.Torment2 => 2,
                EArena.Torment3 => 3,
                _ => 0,
            };

            string challengeIdBase = Randomizer.ItemTracker.GetChallengeIdFromZone(zone);
            string levelId = $"{challengeIdBase}{arenaNumber}";
            return levelId;
        }

        private static bool CanAccessLocationType(ELocationType type)
        {
            return type switch
            {
                ELocationType.LevelAmmostashCompletion => Randomizer.ItemTracker.IsDestructible("Ammostash"),
                ELocationType.LevelHealthCrystalCompletion => Randomizer.ItemTracker.IsDestructible("Health"),
                ELocationType.LevelChaosCrystalCompletion => Randomizer.ItemTracker.IsDestructible("Chaos"),
                ELocationType.ArenaAmmostashCompletion => Randomizer.ItemTracker.IsDestructible("Ammostash"),
                ELocationType.ArenaHealthCrystalCompletion => Randomizer.ItemTracker.IsDestructible("Health"),
                ELocationType.ArenaChaosCrystalCompletion => Randomizer.ItemTracker.IsDestructible("Chaos"),
                ELocationType.ArenaDestructibleCompletion => CanDestroyAll(),
                ELocationType.LevelSpeed => Randomizer.ItemTracker.CanSoar()
                    && Randomizer.ItemTracker.CanDoubleJump()
                    && Randomizer.ItemTracker.CanQuickReload()
                    && IsNotArchdevil(),
                ELocationType.TormentGold => Randomizer.ItemTracker.CanQuickReload()
                    && HasAdvancedMovement()
                    && HasAnyHeal(),
                _ => true,
            };
        }

        private static bool CanDestroyAll()
        {
            return Randomizer.ItemTracker.IsDestructible("Ammostash")
                && Randomizer.ItemTracker.IsDestructible("Health")
                && Randomizer.ItemTracker.IsDestructible("Chaos");
        }

        private static bool CanDestroyAny()
        {
            return Randomizer.ItemTracker.IsDestructible("Ammostash")
                || Randomizer.ItemTracker.IsDestructible("Health")
                || Randomizer.ItemTracker.IsDestructible("Chaos");
        }

        private static bool CanAccessLocation(
            EZone zone,
            EArena arena,
            ELocationType type,
            string id
        )
        {
            if (!CanAccessLocationType(type))
                return false;

            return zone switch
            {
                EZone.Global => GlobalExceptions(type, id),
                EZone.Tutorial => true,
                EZone.Voke => VokeExceptions(id),
                EZone.Stygia => StygiaExceptions(id),
                EZone.Yhelm => YhelmExceptions(id),
                EZone.Incaustis => IncaustisExceptions(id),
                EZone.Gehenna => GehennaExceptions(id),
                EZone.Nihil => NihilExceptions(id),
                EZone.Acheron => AcheronExceptions(id),
                EZone.Sheol => SheolExceptions(id),

                EZone.KillingWithRhythm => HasRequiredChallengeWeapons(zone, arena),
                EZone.Giantslayer => HasRequiredChallengeWeapons(zone, arena),
                EZone.UltimateMastery => HasRequiredChallengeWeapons(zone, arena)
                    && HasUltimatesForChallenge(zone, arena),
                EZone.SlaughterMastery => HasRequiredChallengeWeapons(zone, arena)
                    && Randomizer.ItemTracker.CanSlaughter(),
                EZone.RelicThief => HasRequiredChallengeWeapons(zone, arena),
                EZone.WeaponTrickery => HasRequiredChallengeWeapons(zone, arena),
                EZone.DeathsEdge => HasRequiredChallengeWeapons(zone, arena),

                EZone.Leviathan => LeviathanExceptions(arena, id),
                _ => false,
            };
        }

        private static bool GlobalExceptions(ELocationType type, string id)
        {
            return type switch {
                ELocationType.Bestiary => BestiaryRequirementsMet(id),
                ELocationType.Codex => CodexRequirementsMet(id),
                ELocationType.SectionClearOutfit => Randomizer.ItemTracker.HasOutfitByLocation(id) && CanAccessAnyBoss(),
                ELocationType.SectionClearMainSong => Randomizer.ItemTracker.HasSongByLocation(id) && CanAccessAnyBoss(),
                ELocationType.SectionClearBossSong => Randomizer.ItemTracker.HasSongByLocation(id) && CanAccessAnyBoss()
                    || (id.Equals("Section Cleared with: No Tomorrow") && CanAccessArena(EZone.Leviathan, EArena.FinalDestination)),
                ELocationType.SectionClearWeapon => Randomizer.ItemTracker.HasWeaponByLocation(id) && CanAccessAnyBoss(),
                ELocationType.FirstMiscellaneous => LocationSpecificMet(id),
                ELocationType.WeaponSkin => CanCollectCoatOfArms() && HasAccessToEnoughCoatOfArms(id),
                _ => true,
            };
        }

        private static bool VokeExceptions(string id)
        {
            return id switch {
                "Voke Coat of Arms Archdevil" => Randomizer.ItemTracker.CanDash(),
                _ => true
            };
        }

        private static bool StygiaExceptions(string id)
        {
            return id switch {
                "Stygia Next Multiplier 2" => HasBaseMovement(),
                "Stygia Secret Max Multiplier" => Randomizer.ItemTracker.CanDash(),
                "Stygia Coat of Arms Beast" => Randomizer.ItemTracker.CanDash(),
                "Stygia Arena 2 Ammostash Destruction" => HasBaseMovement() || HasRangedWeapon(),
                "Stygia Arena 2 Destructible Completion" => HasBaseMovement() || HasRangedWeapon(),
                _ => true
            };
        }

        private static bool YhelmExceptions(string id)
        {
            return id switch {
                "Yhelm Secret Max Multiplier" => Randomizer.ItemTracker.CanDash() || Randomizer.ItemTracker.CanDoubleJump(),
                "Yhelm Coat of Arms Goat" => Randomizer.ItemTracker.CanDash(),
                "Yhelm Coat of Arms Beast" => Randomizer.ItemTracker.CanDash(),
                "Yhelm Coat of Arms Archdevil" => HasBaseMovement(),
                _ => true
            };
        }

        private static bool IncaustisExceptions(string id)
        {
            return id switch {
                "Incaustis Arena 1 Health Crystal Destruction" => HasBaseMovement() || HasRangedWeapon(),
                "Incaustis Arena 1 Destructible Completion" => HasBaseMovement() || HasRangedWeapon(),
                _ => true
            };
        }

        private static bool GehennaExceptions(string id)
        {
            return id switch {
                "Gehenna Secret Max Multiplier" => Randomizer.ItemTracker.CanDash() || Randomizer.ItemTracker.CanDoubleJump(),
                "Gehenna Coat of Arms Goat" => HasBaseMovement(),
                "Gehenna Arena 2 Ammostash Destruction" => HasBaseMovement() || HasRangedWeapon(),
                "Gehenna Arena 2 Destructible Completion" => HasBaseMovement() || HasRangedWeapon(),
                _ => true
            };
        }

        private static bool NihilExceptions(string id)
        {
            return id switch {
                "Nihil Max Multiplier 1" => HasBaseMovement(),
                "Nihil Coat of Arms Goat" => HasBaseMovement(),
                "Nihil Coat of Arms Archdevil" => Randomizer.ItemTracker.CanDash(),
                _ => true
            };
        }

        private static bool AcheronExceptions(string id)
        {
            return id switch {
                "Acheron Secret Max Multiplier" => HasBaseMovement(),
                "Acheron Coat of Arms Archdevil" => HasBaseMovement(),
                "Acheron Coat of Arms Beast" => Randomizer.ItemTracker.CanDash(),
                _ => true
            };
        }

        private static bool SheolExceptions(string id)
        {
            return id switch {
                "Sheol Next Multiplier 4" => HasBaseMovement(),
                "Sheol Next Multiplier 5" => Randomizer.ItemTracker.CanDash() || Randomizer.ItemTracker.CanDoubleJump(),
                "Sheol Secret Max Multiplier" => Randomizer.ItemTracker.CanDash(),
                "Sheol Coat of Arms Lamb" => Randomizer.ItemTracker.CanDash() || Randomizer.ItemTracker.CanDoubleJump(),
                "Sheol Coat of Arms Goat" => Randomizer.ItemTracker.CanDash() || Randomizer.ItemTracker.CanDoubleJump(),
                "Sheol Coat of Arms Beast" => Randomizer.ItemTracker.CanDash(),
                _ => true
            };
        }


        private static bool BestiaryRequirementsMet(string id)
        {
            return id switch {
                "Marionette discovered" => CanAccessZone(EZone.Tutorial, EArena.Tutorial) && CanAccessArena(EZone.Tutorial, EArena.Tutorial),
                "Cambion discovered" => CanAccessZone(EZone.Voke, EArena.Arena1) && CanAccessArena(EZone.Voke, EArena.Arena1),
                "Behemoth discovered" => CanAccessZone(EZone.Voke, EArena.Arena3) && CanAccessArena(EZone.Voke, EArena.Arena3),
                "Stalker discovered" => CanAccessZone(EZone.Stygia, EArena.Arena3) && CanAccessArena(EZone.Stygia, EArena.Arena3),
                "Eyeless discovered" => CanAccessZone(EZone.Yhelm, EArena.Arena2) && CanAccessArena(EZone.Yhelm, EArena.Arena2),
                "Hierophant discovered" => CanAccessZone(EZone.Incaustis, EArena.Arena2) && CanAccessArena(EZone.Incaustis, EArena.Arena2),
                "Lesser Seraph discovered" => CanAccessZone(EZone.Gehenna, EArena.Arena2) && CanAccessArena(EZone.Gehenna, EArena.Arena2),
                "Shield Cambion discovered" => CanAccessZone(EZone.Yhelm, EArena.Arena1) && CanAccessArena(EZone.Yhelm, EArena.Arena1),
                "Siege Behemoth discovered" => CanAccessZone(EZone.Incaustis, EArena.Arena4) && CanAccessArena(EZone.Incaustis, EArena.Arena4),
                "Void Stalker discovered" => CanAccessZone(EZone.Nihil, EArena.Arena3) && CanAccessArena(EZone.Nihil, EArena.Arena3),
                "Annihilator Seraph discovered" => Randomizer.ItemTracker.HasDifficultyUnlocked(EDifficulty.VeryHard) 
                    && CanAccessZone(EZone.Voke, EArena.Arena4)
                    && CanAccessArena(EZone.Voke, EArena.Arena4),
                _ => true,
            };
        }

        private static bool CanCollectCoatOfArms()
        {
            return !Randomizer.Settings.IncludeCoatOfArmsChecks && Randomizer.Settings.RequireCoatOfArmsForSheol;
        }

        private static bool HasAccessToEnoughCoatOfArms(string id)
        {
            int requiredCount = id switch {
                "Paz Weapon Skin Unlock" => 2,
                "Terminus Weapon Skin Unlock" => 8,
                "Persephone Weapon Skin Unlock" => 14,
                "The Hounds Weapon Skin Unlock" => 20,
                "Vulcan Weapon Skin Unlock" => 26,
                "Hellcrow Weapon Skin Unlock" => 32,
                _ => 100,
            };
            int accessibleCount = GetAccessibleCoatOfArms().Count;
            return accessibleCount >= requiredCount;
        }

        private static List<Location> GetAccessibleCoatOfArms()
        {
            var coatOfArmsLocations = Lookup.CoatOfArmsLocationIds
                .Select(id => Locations.LocationDataByName[id])
                .ToList();
            List<Location> accessibleCoatOfArms = new List<Location>();
            foreach (var location in coatOfArmsLocations)
            {
                if(CanReach(location))
                    accessibleCoatOfArms.Add(location);

            }
            return accessibleCoatOfArms;
        }

        private static bool LocationSpecificMet(string id)
        {
            return id switch {
                "First Miscellaneous - Slaughter" => Randomizer.ItemTracker.CanSlaughter(),
                "First Miscellaneous - Jump" => Randomizer.ItemTracker.CanJump(),
                "First Miscellaneous - Double Jump" => Randomizer.ItemTracker.CanDoubleJump(),
                "First Miscellaneous - Infinite Jump" => Randomizer.ItemTracker.CanInfiniteJump(),
                "First Miscellaneous - Quick Reload" => Randomizer.ItemTracker.CanQuickReload(),
                "First Miscellaneous - Dash" => Randomizer.ItemTracker.CanDash(),
                "First Miscellaneous - Soar" => Randomizer.ItemTracker.CanSoar(),
                "First Miscellaneous - Ammostash" => Randomizer.ItemTracker.IsDestructible("Ammostash") && CanAccessAnyHellsOrLeviathan(),
                "First Miscellaneous - Health Crystal" => Randomizer.ItemTracker.IsDestructible("Health") && CanAccessAnyHellsOrLeviathan(),
                "First Miscellaneous - Chaos Crystal" => Randomizer.ItemTracker.IsDestructible("Chaos") && (HasChaosAccess || CanAccessAnyLeviathan()),
                "First Miscellaneous - Enduring Fury" => Randomizer.ItemTracker.HasBoonByBeatSreakEffect(EBeatStreakEffect.SlowerFuryDecay),
                "First Miscellaneous - Faster Ultimate Gain" => Randomizer.ItemTracker.HasBoonByBeatSreakEffect(EBeatStreakEffect.IncreasedUltimateBuildSpeed),
                "First Miscellaneous - Deadlier Dash" => Randomizer.ItemTracker.HasBoonByBeatSreakEffect(EBeatStreakEffect.IncreasedDashDamage),
                "First Miscellaneous - Explosive Slaughter" => Randomizer.ItemTracker.HasBoonByBeatSreakEffect(EBeatStreakEffect.ExplosiveSlaughters),
                "First Miscellaneous - Paz Ultimate" => Randomizer.ItemTracker.CanWeaponUltimate(PlayerWeaponType.RhythmWeapon) && CanAccessAnyHellsOrLeviathan(),
                "First Miscellaneous - Terminus Ultimate" => Randomizer.ItemTracker.CanWeaponUltimate(PlayerWeaponType.Falx) && CanAccessAnyHellsOrLeviathan(),
                "First Miscellaneous - Persephone Ultimate" => Randomizer.ItemTracker.CanWeaponUltimate(PlayerWeaponType.Shotgun) && CanAccessAnyHellsOrLeviathan(),
                "First Miscellaneous - The Hounds Ultimate" => Randomizer.ItemTracker.CanWeaponUltimate(PlayerWeaponType.Pistols) && CanAccessAnyHellsOrLeviathan(),
                "First Miscellaneous - Vulcan Ultimate" => Randomizer.ItemTracker.CanWeaponUltimate(PlayerWeaponType.Vulcan) && CanAccessAnyHellsOrLeviathan(),
                "First Miscellaneous - Hellcrow Ultimate" => Randomizer.ItemTracker.CanWeaponUltimate(PlayerWeaponType.Boomerang) && CanAccessAnyHellsOrLeviathan(),
                "First Miscellaneous - The Red Right Hand Ultimate" => Randomizer.ItemTracker.CanWeaponUltimate(PlayerWeaponType.AssaultRifle) && CanAccessAnyHellsOrLeviathan(),
                "First Miscellaneous - Telos Ultimate" => Randomizer.ItemTracker.CanWeaponUltimate(PlayerWeaponType.Bow) && CanAccessAnyHellsOrLeviathan(),
                _ => true,
            };
        }

        private static bool CodexRequirementsMet(string id)
        {
            return CanAccessAnyHellsOrLeviathan() && id switch
            {
                "Styx Reload discovered" => Randomizer.ItemTracker.CanQuickReload()
                    && Randomizer.ItemTracker.IsDestructible("Health"),
                "Hells's Heartbeat discovered" => Randomizer.ItemTracker.CanQuickReload(),
                "Basilisk Mode discovered" => Randomizer.ItemTracker.CanSoar(),
                "Double Hit and Run discovered" => (Randomizer.ItemTracker.IsDestructible("Ammostash") || Randomizer.ItemTracker.IsDestructible("Health"))
                    && Randomizer.ItemTracker.CanDash(),
                "Shatter Two discovered" => Randomizer.ItemTracker.IsDestructible("Ammostash") || Randomizer.ItemTracker.IsDestructible("Health"),
                "Devil's Flight discovered" => Randomizer.ItemTracker.CanDash()
                    && Randomizer.ItemTracker.CanSoar()
                    && Randomizer.ItemTracker.CanJump(),
                "Double Slaughter discovered" => Randomizer.ItemTracker.CanSlaughter(),
                "Chaos and Slaughter discovered" => Randomizer.ItemTracker.IsDestructible("Chaos") 
                    && HasChaosAccess
                    && Randomizer.ItemTracker.CanSlaughter(),
                "Unholy Mess discovered" => Randomizer.ItemTracker.CanSlaughter(),
                "Five Endings discovered" => ((Randomizer.ItemTracker.IsDestructible("Chaos") && HasChaosAccess)
                    || (CanAccessAnyHells() && Randomizer.ItemTracker.Has("Paz")) && HasAmountOfWeapons(3)),
                "Slaughter and Kill discovered" => Randomizer.ItemTracker.CanSlaughter(),
                "Chaos Flight discovered" => Randomizer.ItemTracker.CanSoar()
                    && Randomizer.ItemTracker.CanJump()
                    && Randomizer.ItemTracker.IsDestructible("Chaos")
                    && HasChaosAccess,
                "Death from Above discovered" => Randomizer.ItemTracker.CanSoar()
                    && Randomizer.ItemTracker.CanSlaughter(),
                "Lethal Cycle discovered" => HasAmountOfWeapons(3),
                "Kill Trio discovered" => true,
                "Triple Dash discovered" => Randomizer.ItemTracker.CanDash(),
                _ => false,
            };
        }

        private static bool HasChaosAccess =>
            (CanAccessZone(EZone.Voke, EArena.Arena2) && CanAccessArena(EZone.Voke, EArena.Arena2))
            || ( CanAccessZone(EZone.Stygia, EArena.Arena2) && CanAccessArena(EZone.Stygia, EArena.Arena2))
            || ( CanAccessZone(EZone.Yhelm, EArena.Arena2) && CanAccessArena(EZone.Yhelm, EArena.Arena2))
            || ( CanAccessZone(EZone.Incaustis, EArena.Arena1) && CanAccessArena(EZone.Incaustis, EArena.Arena1))
            || ( CanAccessZone(EZone.Gehenna, EArena.Arena1) && CanAccessArena(EZone.Gehenna, EArena.Arena1))
            || ( CanAccessZone(EZone.Nihil, EArena.Arena1) && CanAccessArena(EZone.Nihil, EArena.Arena1))
            || ( CanAccessZone(EZone.Acheron, EArena.Arena1) && CanAccessArena(EZone.Acheron, EArena.Arena1))
            || ( CanAccessZone(EZone.Sheol, EArena.Arena1) && CanAccessArena(EZone.Sheol, EArena.Arena1));

        private static bool CanAccessAnyHellsOrLeviathan()
        {
            return CanAccessAnyHells() || CanAccessAnyLeviathan();
        }

        private static bool CanAccessAnyLeviathan()
        {
            return CanAccessZone(EZone.Leviathan, EArena.Global);
        }


        private static bool CanAccessAnyHells()
        {
            return CanAccessZone(EZone.Voke, EArena.Arena1)
                || CanAccessZone(EZone.Stygia, EArena.Arena1)
                || CanAccessZone(EZone.Yhelm, EArena.Arena1)
                || CanAccessZone(EZone.Incaustis, EArena.Arena1)
                || CanAccessZone(EZone.Gehenna, EArena.Arena1)
                || CanAccessZone(EZone.Nihil, EArena.Arena1)
                || CanAccessZone(EZone.Acheron, EArena.Arena1)
                || CanAccessZone(EZone.Sheol, EArena.Arena1);
        }

        private static bool CanAccessAnyArena4()
        {
            return CanAccessZone(EZone.Voke, EArena.Arena4)
                || CanAccessZone(EZone.Stygia, EArena.Arena4)
                || CanAccessZone(EZone.Yhelm, EArena.Arena4)
                || CanAccessZone(EZone.Incaustis, EArena.Arena4)
                || CanAccessZone(EZone.Gehenna, EArena.Arena4)
                || CanAccessZone(EZone.Nihil, EArena.Arena4)
                || CanAccessZone(EZone.Acheron, EArena.Arena4)
                || CanAccessZone(EZone.Sheol, EArena.Arena4);
        }

        private static bool CanAccessAnyBoss()
        {
            return (CanAccessZone(EZone.Voke, EArena.Boss) && CanAccessArena(EZone.Voke, EArena.Boss))
                || (CanAccessZone(EZone.Stygia, EArena.Boss) && CanAccessArena(EZone.Stygia, EArena.Boss))
                || (CanAccessZone(EZone.Yhelm, EArena.Boss) && CanAccessArena(EZone.Yhelm, EArena.Boss))
                || (CanAccessZone(EZone.Incaustis, EArena.Boss) && CanAccessArena(EZone.Incaustis, EArena.Boss))
                || (CanAccessZone(EZone.Gehenna, EArena.Boss) && CanAccessArena(EZone.Gehenna, EArena.Boss))
                || (CanAccessZone(EZone.Nihil, EArena.Boss) && CanAccessArena(EZone.Nihil, EArena.Boss))
                || (CanAccessZone(EZone.Acheron, EArena.Boss) && CanAccessArena(EZone.Acheron, EArena.Boss))
                || (CanAccessZone(EZone.Sheol, EArena.Boss) && CanAccessArena(EZone.Sheol, EArena.Boss));
        }

        private static bool HasAmountOfWeapons(int amount)
        {
            int count = 0;
            foreach (var weapon in Lookup.ExtendedWeaponNameToType.Keys)
            {
                if (Randomizer.ItemTracker.Has(weapon))
                    count++;
                if(count == amount)
                    return true;
            }
            return false;
        }

        private static bool HasRequiredChallengeWeapons(EZone zone, EArena arena)
        {
            string levelId = getChallengeFromZoneAndArena(zone, arena);
            return Randomizer.ItemTracker.HasLevelUnlocked(levelId);
        }

        private static bool HasUltimatesForChallenge(EZone zone, EArena arena)
        {
            string levelId = getChallengeFromZoneAndArena(zone, arena);
            var requiredWeapons = Lookup.RequiredWeaponsForLevel[levelId];
            int count = 0;
            foreach (var weapon in requiredWeapons)
            {
                if(Randomizer.ItemTracker.CanWeaponUltimate(weapon))
                    count++;
            }
            return count >= requiredWeapons.Count;
        }

        // TODO: Leviathan integration
        private static bool LeviathanExceptions(EArena arena, string id)
        {
            return arena switch {
                _ => true,
            };
        }

        // TODO: Leviathan integration
        private static bool CanAccessEndOfLeviathanStage()
        {
            return false;
        }


        // TODO: Leviathan integration
        private static bool CanAccessLeviathanArenas(EArena arena)
        {
            return true;
        }

        internal static bool CanAccessRegion(EZone hells)
        {
            var ZoneArenaTuple = Lookup.EZoneToIndividualLevels[hells];
            foreach (var ZoneArena in ZoneArenaTuple)
            {
                if(LocationAccessibility.CanAccessZone(ZoneArena.Item1, ZoneArena.Item2))
                    return true;
            }
            return false;
        }
    }
}

