using HarmonyLib;
using System;
using System.Reflection;

namespace TranslationMod.Patches
{
    [HarmonyPatch]
    public static class UIBaseCharacterSheetEntryPatch
    {
        [HarmonyTargetMethod]
        private static MethodBase TargetMethod()
        {
            var editorSheetEntryType = AccessTools.Inner(typeof(UIBaseCharacterSheet), "EditorSheetEntry");
            if (editorSheetEntryType == null)
            {
                TranslationMod.Logger?.LogError("[UIBaseCharacterSheetEntryPatch] Cannot find UIBaseCharacterSheet.EditorSheetEntry type");
                return null;
            }

            var ctor = AccessTools.Constructor(editorSheetEntryType, Type.EmptyTypes);
            if (ctor == null)
            {
                TranslationMod.Logger?.LogError("[UIBaseCharacterSheetEntryPatch] Cannot find EditorSheetEntry constructor");
                return null;
            }

            return ctor;
        }

        [HarmonyPostfix]
        private static void Postfix(object __instance)
        {
            try
            {
                if (__instance == null)
                {
                    return;
                }

                var tiny = FontContainer.getTinyFont();
                if (tiny == null)
                {
                    return;
                }

                int pointBlockHeight = tiny.wordHeight;
                int plusTopPadding = 11 - 7 + tiny.wordHeight;

                var instanceType = __instance.GetType();

                var pointBlockField = AccessTools.Field(instanceType, "pointBlock");
                var pointBlock = pointBlockField?.GetValue(__instance) as UITextBlock;
                if (pointBlock != null)
                {
                    pointBlock.setHeight(pointBlockHeight);
                }

                var plusColumnField = AccessTools.Field(instanceType, "plusColumn");
                var plusColumn = plusColumnField?.GetValue(__instance) as UICanvasVertical;
                if (plusColumn != null)
                {
                    var padding = plusColumn.padding;
                    padding.top = plusTopPadding;
                    plusColumn.padding = padding;
                }
            }
            catch (Exception ex)
            {
                TranslationMod.Logger?.LogError($"[UIBaseCharacterSheetEntryPatch] Failed to patch EditorSheetEntry: {ex.Message}");
            }
        }
    }
}
