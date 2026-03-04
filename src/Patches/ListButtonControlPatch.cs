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
            if (!LanguageManager.NoLetterLanguage())
            {
                return;
            }

            if (__result is UIElement button)
            {
                button.padding.bottom = 0;
            }
        }
    }
}