using HarmonyLib;
using System;
using System.Reflection;

namespace TranslationMod.Patches
{
    [HarmonyPatch]
    public static class FontPatch
    {
        [HarmonyTargetMethod]
        private static MethodBase TargetMethod()
        {
            var fontContainerType = AccessTools.TypeByName("FontContainer");
            if (fontContainerType == null)
            {
                TranslationMod.Logger?.LogError("[FontPatch] Cannot find FontContainer type");
                return null;
            }

            var method = AccessTools.Method(fontContainerType, "getTinyFont");
            if (method == null)
            {
                TranslationMod.Logger?.LogError("[FontPatch] Cannot find FontContainer.getTinyFont method");
                return null;
            }

            return method;
        }

        [HarmonyPostfix]
        private static void Postfix(object __result)
        {
            try
            {
                if (__result == null)
                {
                    return;
                }

                var resultType = __result.GetType();

                var wordHeightField = AccessTools.Field(resultType, "wordHeight");
                if (wordHeightField != null && wordHeightField.FieldType == typeof(int))
                {
                    wordHeightField.SetValue(__result, 10);
                    return;
                }

                // 备用设置方案
                var wordHeightProperty = AccessTools.Property(resultType, "wordHeight");
                if (wordHeightProperty != null && wordHeightProperty.CanWrite && wordHeightProperty.PropertyType == typeof(int))
                {
                    wordHeightProperty.SetValue(__result, 10, null);
                }
            }
            catch (Exception ex)
            {
                TranslationMod.Logger?.LogError($"[FontPatch] Failed to set wordHeight: {ex.Message}");
            }
        }
    }
}
