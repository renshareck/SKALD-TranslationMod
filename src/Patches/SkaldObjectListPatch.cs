using HarmonyLib;
using System.Reflection;

namespace TranslationMod.Patches
{
    [HarmonyPatch]
    public static class SkaldObjectListPatch
    {
        private static readonly FieldInfo MaxPageSizeField =
            AccessTools.Field(typeof(SkaldObjectList), "maxPageSize");

        [HarmonyPatch(typeof(SkaldObjectList), MethodType.Constructor, new System.Type[] { })]
        [HarmonyPostfix]
        private static void CtorPostfix_NoArgs(SkaldObjectList __instance)
        {
            SetDefaultMaxPageSize(__instance);
        }

        [HarmonyPatch(typeof(SkaldObjectList), MethodType.Constructor, new[] { typeof(string) })]
        [HarmonyPostfix]
        private static void CtorPostfix_WithTitle(SkaldObjectList __instance)
        {
            SetDefaultMaxPageSize(__instance);
        }

        [HarmonyPatch(typeof(SkaldObjectList), "setMaxPageSize")]
        [HarmonyPrefix]
        private static void SetMaxPageSizePrefix(ref int newSize)
        {
            newSize = newSize * 9 / 12;
        }

        private static void SetDefaultMaxPageSize(SkaldObjectList instance)
        {
            if (instance == null || MaxPageSizeField == null)
            {
                return;
            }

            MaxPageSizeField.SetValue(instance, 13);
        }
    }
}
