using HarmonyLib;

namespace TranslationMod.Patches
{
    [HarmonyPatch(typeof(CharacterBuilderBaseState))]
    public static class CharacterBuilderBaseStatePatch
    {
        [HarmonyPatch("setGUIData")]
        [HarmonyPostfix]
        private static void SetGuiDataPostfix(object __instance)
        {
            try
            {
                var stateType = __instance.GetType();
                var listField = AccessTools.Field(stateType, "list");
                var guiControlField = AccessTools.Field(stateType, "guiControl") ??
                                      AccessTools.Field(typeof(StateBase), "guiControl");

                var list = listField?.GetValue(__instance) as SkaldObjectList;
                var guiControl = guiControlField?.GetValue(__instance) as GUIControl;

                if (list == null || guiControl == null)
                {
                    return;
                }

                // Keep left list scrollbar in sync with full list count and apply resulting page index.
                int scrollIndex = guiControl.updateLeftScrollBarAndReturnIndex(list.getCount());
                if (scrollIndex != -1)
                {
                    list.setScrollIndex(scrollIndex);
                }

                // Rebuild visible page after scroll change.
                guiControl.setListButtons(list.getScrolledStringList());

                // Keep description scrollbar reactive on GUI refresh.
                guiControl.updateRightScrollBar();
            }
            catch (System.Exception ex)
            {
                TranslationMod.Logger?.LogError($"[CharacterBuilderBaseStatePatch] setGUIData patch failed: {ex.Message}");
            }
        }
    }
}
