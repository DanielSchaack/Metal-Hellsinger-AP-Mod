using HarmonyLib;

namespace Randomizer
{
    // Coat of Arms
    [HarmonyPatch(typeof(CollectiblePickupSystem))]
    public class CollectiblePickupSystemPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(CollectiblePickupSystem.Register))]
        static bool RegisterPrefix(
            CollectiblePickupSystem __instance,
            ref CollectiblePickup instance
        )
        {
            Logger.LogDebug(
                $"CollectiblePickupSystem Register Prefix called for {instance.ID} that is enabled for {instance.EnabledOnDifficulty}"
            );
            instance.EnabledOnDifficulty |=
                EDifficultyFlags.Easy
                | EDifficultyFlags.Medium
                | EDifficultyFlags.Hard
                | EDifficultyFlags.VeryHard;
            Logger.LogInfo($"Force-enabled collectible '{instance.ID}' across all difficulties.");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(CollectiblePickupSystem.Register))]
        static void RegisterPostfix(CollectiblePickupSystem __instance, CollectiblePickup instance)
        {
            Logger.LogDebug(
                $"CollectiblePickupSystem Register Postfix called for {instance.ID} that is enabled for {instance.EnabledOnDifficulty}"
            );
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(CollectiblePickupSystem.HandlePickupCollected))]
        static bool HandlePickupCollectedPrefix(
            CollectiblePickupSystem __instance,
            CollectiblePickupType type,
            string id
        )
        {
            Logger.LogDebug(
                $"CollectiblePickupSystem HandlePickupCollected Prefix called for {id}"
            );
            Randomizer.LocationTracker.CheckCoatOfArms(id);
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(CollectiblePickupSystem.HandlePickupCollected))]
        static void HandlePickupCollectedPostfix(
            CollectiblePickupSystem __instance,
            CollectiblePickupType type,
            string id
        )
        {
            Logger.LogDebug(
                $"CollectiblePickupSystem HandlePickupCollected Postfix called for {id}"
            );
        }
    }

    // Weapon pickups events
    [HarmonyPatch(typeof(WeaponGiver))]
    public class WeaponGiverPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(WeaponGiver.GiveWeapon))]
        static bool GiveWeaponPrefix(WeaponGiver __instance)
        {
            Logger.LogDebug($"WeaponGiver GiveWeapon Prefix called");
            return false;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(WeaponGiver.GiveWeapon))]
        static void GiveWeaponPostfix(WeaponGiver __instance)
        {
            Logger.LogDebug($"WeaponGiver GiveWeapon Postfix called");
            Randomizer.LocationTracker.CheckWeaponPickups(__instance.Weapon);
        }
    }

    // Destructible events - health crystal, ammostashes, chaos crystal, anguish gates
    [HarmonyPatch(typeof(DestructibleObjectSystem))]
    public class DestructibleObjectSystemPatches
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(DestructibleObjectSystem.Destruct))]
        static void DestructPostfix(
            DestructibleObjectSystem __instance,
            DestructibleObjectSystem.DestructibleCombatant combatant,
            bool spawnEffect,
            EBeatGrading beatGrade,
            bool onBeat
        )
        {
            Logger.LogDebug(
                $"DestructibleObjectSystem Destruct Postfix called for {combatant.Destructible.name}"
            );
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(DestructibleObjectSystem.OnHit))]
        static bool OnHitPrefix(
            DestructibleObjectSystem __instance,
            DestructibleObjectSystem.DestructibleCombatant combatant,
            AttackID attackId,
            bool onBeat,
            AttackBase attack
        )
        {
            Logger.LogDebug(
                $"DestructibleObjectSystem OnHit Prefix called for {combatant.Destructible.name}"
            );
            return Randomizer.LocationTracker.IsDestructible(combatant.Destructible);
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(DestructibleObjectSystem.OnHit))]
        static void OnHitPostfix(
            DestructibleObjectSystem __instance,
            DestructibleObjectSystem.DestructibleCombatant combatant,
            AttackID attackId,
            bool onBeat,
            AttackBase attack
        )
        {
            Logger.LogDebug(
                $"DestructibleObjectSystem OnHit Postfix called for {combatant.Destructible.name}"
            );
            Randomizer.LocationTracker.CheckDestructible(Randomizer.CurrentLevel, combatant.Destructible);
        }
    }

    // Multiplier pickup events
    [HarmonyPatch(typeof(HUDView))]
    public class HUDViewPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(HUDView.OnMultiplierBoostEvent))]
        static bool OnMultiplierBoostEventPrefix(
            HUDView __instance,
            MultiplierBoostEventType eventType
        )
        {
            Logger.LogDebug($"HUDView OnMultiplierBoostEvent Prefix for {eventType}");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(HUDView.OnMultiplierBoostEvent))]
        static void OnMultiplierBoostEventPostfix(
            HUDView __instance,
            MultiplierBoostEventType eventType
        )
        {
            Logger.LogDebug($"HUDView OnMultiplierBoostEvent Postfix for {eventType}");
            Randomizer.LocationTracker.CheckMultiplierPickups();
        }
    }
}
