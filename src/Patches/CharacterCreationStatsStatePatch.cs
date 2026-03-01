using HarmonyLib;

using TranslationMod.Configuration;

namespace TranslationMod.Patches
{
    [HarmonyPatch(typeof(CharacterCreationStatsState), "setGUIData")]
    public static class CharacterCreationStatsStatePatch
    {
        public static bool IsSetGuiDataRunning { get; private set; }

        [HarmonyPrefix]
        private static void Prefix()
        {
            IsSetGuiDataRunning = false;
            if (LanguageManager.NoLetterLanguage())
            {
                IsSetGuiDataRunning = true;
            }
        }

        [HarmonyPostfix]
        private static void Postfix()
        {
            IsSetGuiDataRunning = false;
        }
    }
}
