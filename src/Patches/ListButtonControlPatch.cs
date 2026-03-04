using HarmonyLib;
using TranslationMod.Configuration;

namespace TranslationMod.Patches
{
    [HarmonyPatch(typeof(ListButtonControl), "createButton")]
    public static class ListButtonControlPatch
    {
        [HarmonyPostfix]
        private static void Postfix(object __result)
        {
            if (__result is UIElement button)
            {
                if (LanguageManager.NoLetterLanguage())
                {
                    var padding = button.padding;
                    padding.bottom = 1;
                    button.padding = padding;
                }
            }
        }
    }
}
