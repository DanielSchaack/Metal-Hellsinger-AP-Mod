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
            return false;
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
            bool isDestructible = Randomizer.LocationTracker.IsDestructible(combatant.Destructible);
            if (isDestructible)
                Randomizer.LocationTracker.CheckDestructible(
                    Randomizer.CurrentLevel,
                    combatant.Destructible
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

    [HarmonyPatch(typeof(MultiplierBoostPickup))]
    public class MultiplierBoostPickupPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(MultiplierBoostPickup.OnTriggerEnter))]
        static bool OnTriggerEnterPrefix(MultiplierBoostPickup __instance)
        {
            Logger.LogDebug(
                $"MultiplierBoostPickup OnTriggerEnter Prefix for {__instance.BoostType}, from gameObject: {__instance.gameObject.name}"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(MultiplierBoostPickup.OnTriggerEnter))]
        static void OnTriggerEnterPostfix(MultiplierBoostPickup __instance)
        {
            // Logger.LogDebug(
            //     $"MultiplierBoostPickup OnTriggerEnter Postfix for {__instance.BoostType}"
            // );
        }
    }

    [HarmonyPatch(typeof(MultiplierBoostSystem))]
    public class MultiplierBoostSystemPatches
    {
        // Used in Hells
        [HarmonyPrefix]
        [HarmonyPatch(nameof(MultiplierBoostSystem.SetupMultiplierBoost))]
        static bool SetupMultiplierBoostPrefix(
            MultiplierBoostSystem __instance,
            MultiplierBoostPickup multiplierBoostPickup,
            ref Il2CppSystem.Action multiplierPickedupCallback
        )
        {
            Logger.LogDebug(
                $"MultiplierBoostSystem SetupMultiplierBoost Prefix for {multiplierBoostPickup.BoostType}"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(MultiplierBoostSystem.SetupMultiplierBoost))]
        static void SetupMultiplierBoostPostfix(
            MultiplierBoostSystem __instance,
            MultiplierBoostPickup multiplierBoostPickup
        )
        {
            Logger.LogDebug(
                $"MultiplierBoostSystem SetupMultiplierBoost Postfix for {multiplierBoostPickup.BoostType}"
            );
        }

        // Used in Leviathan
        [HarmonyPrefix]
        [HarmonyPatch(nameof(MultiplierBoostSystem.SpawnMultiplierBoost))]
        static bool SpawnMultiplierBoostPrefix(
            MultiplierBoostSystem __instance,
            MultiplierBoostType boostType,
            UnityEngine.Vector3 position,
            UnityEngine.Quaternion rotation,
            ref Il2CppSystem.Action multiplierPickedupCallback
        )
        {
            Logger.LogDebug($"MultiplierBoostSystem SpawnMultiplierBoost Prefix for {boostType}");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(MultiplierBoostSystem.SpawnMultiplierBoost))]
        static void SpawnMultiplierBoostPostfix(
            MultiplierBoostSystem __instance,
            MultiplierBoostType boostType,
            UnityEngine.Vector3 position,
            UnityEngine.Quaternion rotation,
            Il2CppSystem.Action multiplierPickedupCallback
        )
        {
            Logger.LogDebug($"MultiplierBoostSystem SpawnMultiplierBoost Postfix for {boostType}");
        }
    }
}
