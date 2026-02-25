using HarmonyLib;
using System.Text;

namespace TranslationMod.Patches
{
    /// <summary>
    /// Prefix patch for UITextBlock.parseParagraph.
    /// [FIX] Pre-processes paragraph text so non-ASCII text (e.g. Chinese) has explicit break points.
    /// This keeps original parseParagraph logic intact while improving word splitting.
    /// </summary>
    [HarmonyPatch(typeof(UITextBlock), "parseParagraph", new[] { typeof(string) })]
    internal static class ParseParagraphPrefixPatch
    {
        [HarmonyPrefix]
        private static void Prefix(ref string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return;
            }

            // [FIX] Only preprocess plain text regions; keep tag body/content unchanged.
            input = InsertBreakHintsOutsideTags(input);
        }

        private static string InsertBreakHintsOutsideTags(string input)
        {
            var sb = new StringBuilder(input.Length * 2);
            bool inTagMarkup = false;

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];

                if (c == '<')
                {
                    inTagMarkup = true;
                    sb.Append(c);
                    continue;
                }

                if (inTagMarkup)
                {
                    sb.Append(c);
                    if (c == '>')
                    {
                        inTagMarkup = false;
                    }
                    continue;
                }

                sb.Append(c);

                // [FIX] After non-ASCII characters, inject a space as an explicit word boundary.
                // Original parseParagraph already treats space as a word flush condition.
                if (ShouldInjectBreakAfter(c)
                    && i + 1 < input.Length
                    && !char.IsWhiteSpace(input[i + 1])
                    && input[i + 1] != '<')
                {
                    sb.Append(' ');
                }
            }

            return sb.ToString();
        }

        private static bool ShouldInjectBreakAfter(char c)
        {
            // Keep ASCII letters/digits/symbols behavior unchanged.
            if (c <= sbyte.MaxValue)
            {
                return false;
            }

            // Skip whitespace.
            if (char.IsWhiteSpace(c))
            {
                return false;
            }

            return true;
        }
    }
}
