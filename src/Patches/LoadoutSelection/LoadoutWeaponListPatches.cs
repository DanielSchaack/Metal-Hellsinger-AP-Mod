using HarmonyLib;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Outsiders.GUI;

namespace Randomizer
{
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
