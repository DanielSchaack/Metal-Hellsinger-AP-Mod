using HarmonyLib;
using Outsiders.GUI;

namespace Randomizer
{
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
            // data.Level = sigilLevel;
            // data.Unlocked = sigilLevel > 0;

            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(LoadoutSigilItem.SetData))]
        static void SetDataPostfix(LoadoutSigilItem __instance, LoadoutSigilData data, int index)
        {
            Logger.LogInfo(
                $"Loadout index {index}, sigil: {data.SigilType}, unlocked: {data.Unlocked}, level: {data.Level}"
            );
            // __instance.m_lockIcon.gameObject.SetActive(!hasWeapon);
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
}
