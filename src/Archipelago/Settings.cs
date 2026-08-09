using System.Collections.Generic;
using static Randomizer.ItemOrigin;

namespace Randomizer
{
    public class Settings
    {
        private Dictionary<string, object> slotData;

        public enum HellsMode
        {
            Progressive,
            UnlockAsCollectible,
        }

        public enum ChallengeMode
        {
            Progressive,
            UnlockAsCollectible,
        }

        public enum WeaponMode
        {
            WeaponAsOnePackage,
            IndividualAbilities,
        }

        public enum DestructibleMode
        {
            PerEntireArena,
            PerDestructibleType,
        }

        public bool RequireSheolCompletion { get; set; } = true;
        public bool RequireHellsCompletion { get; set; } = false;
        public int RequiredHellsCompletion { get; set; } = 8;
        public bool RequireLeviathanCompletion { get; set; } = false;

        public bool RandomizedHellsEnabled { get; set; } = true;
        public bool RandomizedLevelsEnabled { get; set; } = false;
        public bool RequireAspectForBossArena { get; set; } = false;
        public HellsMode HellsUnlockMode { get; set; } = HellsMode.UnlockAsCollectible;

        public bool RequireNoTomorrowForSheol { get; set; } = true;
        public bool RequireCoatOfArmsForSheol { get; set; } = true;
        public int RequiredCoatOfArmsForSheol { get; set; } = 26;
        public bool RequireNumberOfAspectDefeatedForSheol { get; set; } = true;
        public int RequiredAspectDefeatedForSheol { get; set; } = 7;

        public bool RandomizedChallengesEnabled { get; set; } = true;
        public ChallengeMode ChallengeUnlockMode { get; set; } = ChallengeMode.UnlockAsCollectible;
        public bool RequireStageForChallenges { get; set; } = true;
        public bool RequireWeaponsForChallenges { get; set; } = false;
        public bool ChallengeMedaillonsEnabled { get; set; } = true;
        public bool RequireGoldForChallengeCompletion { get; set; } = true;

        public bool RandomizedSigilsEnabled { get; set; } = true;

        public bool HellsRandomizedWeaponsEnabled { get; set; } = true;
        public WeaponMode WeaponUnlockMode { get; set; } = WeaponMode.WeaponAsOnePackage;

        public bool HellsRandomizedWeaponSkinsEnabled { get; set; } = true;

        public bool HellsNextMultiplierEnabled { get; set; } = true;
        public bool HellsMaxMultiplierEnabled { get; set; } = true;
        public bool HellsSecretMultiplierEnabled { get; set; } = true;
        public bool HellsCoatOfArmsEnabled { get; set; } = true;

        public bool HellsLevelSpeedEnabled { get; set; } = false;
        public bool HellsLevelCompletionEnabled { get; set; } = false;
        public bool HellsLevelDeathlessEnabled { get; set; } = false;
        public bool HellsLevelFuryEnabled { get; set; } = false;

        public bool DestructibleLocationsEnabled { get; set; } = false;
        public bool DestructibleAsUnlocks { get; set; } = false;
        public DestructibleMode DestructibleLocationsMode { get; set; } = DestructibleMode.PerEntireArena;
        public bool RandomizedBoonsEnabled { get; set; } = true;

        public bool RandomizedDashEnabled { get; set; } = true;
        public bool RandomizedJumpEnabled { get; set; } = true;
        public bool RandomizedReloadEnabled { get; set; } = true;
        public bool RandomizedSlaughterEnabled { get; set; } = true;

        public bool RandomizedOutfitsEnabled { get; set; } = true;
        public ItemOrigin RandomizedOutfitDLCs { get; set; } = Base | DreamOfTheBeast | Purgatory;

        public bool RandomizedSongsEnabled { get; set; } = true;
        public ItemOrigin RandomizedSongDLCs { get; set; } = Base | Dusk | DreamOfTheBeast | Purgatory | EssentialHits;

        public int RequiredDestructionCompletions { get; set; } = 5;
        public bool IncludeMiscellaneousChecks { get; set; } = true;

        // TODO: translate into individual settings
        public Settings(Dictionary<string, object> slotData)
        {
            this.slotData = slotData;
        }

        public Settings() { }
    }
}
