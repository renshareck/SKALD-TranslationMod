// Patches/UITextBlockSetContentPatch.cs
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;

using TranslationMod.Configuration;

namespace TranslationMod.Patches
{
    /// <summary>
    /// Harmony patch intercepting UITextBlock.setContent(string)
    /// and inserting translated text.
    /// </summary>
    [HarmonyPatch(typeof(UITextBlock), nameof(UITextBlock.setContent), new[] { typeof(string) })]
    public static class UITextBlockSetContentPatch
    {
        /* ----------------------------------------------------------------- */
        /* 1.  Lazy-инициализация переводчика                               */
        /* ----------------------------------------------------------------- */

        private static readonly Lazy<TranslationService> _translator =
            new(() => new TranslationService());

        /* ----------------------------------------------------------------- */
        /* 1.0  Буфер для tooltip ключей: переведенный ключ -> оригинальный ключ */
        /* ----------------------------------------------------------------- */
        
        /// <summary>
        /// Буфер tooltip ключей: переведенный ключ -> оригинальный ключ
        /// </summary>
        public static readonly Dictionary<string, string> TooltipKeyBuffer = new();
        
        /// <summary>
        /// Regex для извлечения tooltip ключей из тегов <tag>tooltipKey</tag>
        /// </summary>
        private static readonly Regex TooltipTagRegex = new(@"<tag>([^<]+)</tag>", RegexOptions.Compiled);
        private static readonly Regex HashPatternRegex = new Regex(@"#[A-Za-z]{2,}", RegexOptions.Compiled);
        /// <summary>
        /// Объект для синхронизации доступа к буферу
        /// </summary>
        private static readonly object _tooltipBufferLock = new();

        /* ----------------------------------------------------------------- */
        /* 1.1  Рефлексия для доступа к приватным полям и методам            */
        /* ----------------------------------------------------------------- */
        
        private static FieldInfo _illuminatedImageField;
        private static FieldInfo _fontField;
        private static FieldInfo _contentField;
        private static FieldInfo _tooltipField;
        
        private static MethodInfo _preProcessStringMethod;
        private static MethodInfo _identifyTooltipKeywordsMethod;
        private static MethodInfo _splitIntoParagraphMethod;
        private static MethodInfo _parseParagraphMethod;
        private static MethodInfo _pruneLengthMethod;
        
        /// <summary>Ленивая инициализация FieldInfo для illuminatedImage</summary>
        private static FieldInfo IlluminatedImageField
        {
            get
            {
                if (_illuminatedImageField == null)
                    _illuminatedImageField = typeof(UITextBlock).GetField("illuminatedImage", BindingFlags.NonPublic | BindingFlags.Instance);
                return _illuminatedImageField;
            }
        }
        
        /// <summary>Ленивая инициализация FieldInfo для font</summary>
        private static FieldInfo FontField
        {
            get
            {
                if (_fontField == null)
                    _fontField = typeof(UITextBlock).GetField("font", BindingFlags.NonPublic | BindingFlags.Instance);
                return _fontField;
            }
        }
        
        /// <summary>Ленивая инициализация FieldInfo для content</summary>
        private static FieldInfo ContentField
        {
            get
            {
                if (_contentField == null)
                    _contentField = typeof(UITextBlock).GetField("content", BindingFlags.NonPublic | BindingFlags.Instance);
                return _contentField;
            }
        }

        /// <summary>Ленивая инициализация FieldInfo для content</summary>
        private static FieldInfo ToolTipField
        {
            get
            {
                if (_tooltipField == null)
                    _tooltipField = typeof(UITextBlock).GetField("toolTips", BindingFlags.NonPublic | BindingFlags.Instance);
                return _tooltipField;
            }
        }
        
        /// <summary>Ленивая инициализация MethodInfo для preProcessString</summary>
        private static MethodInfo PreProcessStringMethod
        {
            get
            {
                if (_preProcessStringMethod == null)
                    _preProcessStringMethod = typeof(UITextBlock).GetMethod("preProcessString", BindingFlags.NonPublic | BindingFlags.Instance);
                return _preProcessStringMethod;
            }
        }
        
        /// <summary>Ленивая инициализация MethodInfo для identifyTooltipKeywords</summary>
        private static MethodInfo IdentifyTooltipKeywordsMethod
        {
            get
            {
                if (_identifyTooltipKeywordsMethod == null)
                    _identifyTooltipKeywordsMethod = typeof(UITextBlock).GetMethod("identifyTooltipKeywords", BindingFlags.NonPublic | BindingFlags.Instance);
                return _identifyTooltipKeywordsMethod;
            }
        }
        
        /// <summary>Ленивая инициализация MethodInfo для splitIntoParagraph</summary>
        private static MethodInfo SplitIntoParagraphMethod
        {
            get
            {
                if (_splitIntoParagraphMethod == null)
                    _splitIntoParagraphMethod = typeof(UITextBlock).GetMethod("splitIntoParagraph", BindingFlags.NonPublic | BindingFlags.Instance);
                return _splitIntoParagraphMethod;
            }
        }
        
        /// <summary>Ленивая инициализация MethodInfo для parseParagraph</summary>
        private static MethodInfo ParseParagraphMethod
        {
            get
            {
                if (_parseParagraphMethod == null)
                    _parseParagraphMethod = typeof(UITextBlock).GetMethod("parseParagraph", BindingFlags.NonPublic | BindingFlags.Instance);
                return _parseParagraphMethod;
            }
        }
        
        /// <summary>Ленивая инициализация MethodInfo для pruneLength</summary>
        private static MethodInfo PruneLengthMethod
        {
            get
            {
                if (_pruneLengthMethod == null)
                    _pruneLengthMethod = typeof(UITextBlock).GetMethod("pruneLength", BindingFlags.NonPublic | BindingFlags.Instance);
                return _pruneLengthMethod;
            }
        }

        /* ----------------------------------------------------------------- */
        /* 2.  Prefix: меняем аргумент метода и полная реимплементация       */
        /* ----------------------------------------------------------------- */

        private static bool Prefix(UITextBlock __instance, string __0)
        {
            try
            {
                var currentLanguagePack = LanguageManager.GetCurrentLanguagePack();
                if (currentLanguagePack == null || currentLanguagePack.Name.Equals("English", StringComparison.OrdinalIgnoreCase))
                {
                    return true; // Выполняем оригинальный метод
                }
                string processedText = StripHashTags(__0);
                string translatedText = _translator.Value.Process(processedText);
                // Затем полная реимплементация setContent с переведенным текстом
                SetContentComplete(__instance, __0, translatedText);
                
                // Возвращаем false, чтобы пропустить оригинальный метод
                return false;
            }
            catch (Exception ex)
            {
#if DEBUG
                TranslationMod.Logger?.LogError($"Error in custom setContent implementation: {ex.Message}");
#endif
                // В случае ошибки позволяем выполниться оригинальному методу
                return true;
            }
        }
        
        /// <summary>Полная реимплементация UITextBlock.setContent</summary>
        private static void SetContentComplete(UITextBlock instance, string input, string translated)
        {
            try
            {                
                if (instance.isContentEqual(input) || input == null || input.Length == 0)
                {
                    return;
                }
                
                if (PreProcessStringMethod == null)
                {
#if DEBUG
                    TranslationMod.Logger?.LogError("PreProcessStringMethod is null");
#endif
                    return;
                }
                
                instance.clearElements();
                
                if (ContentField == null)
                {
#if DEBUG
                    TranslationMod.Logger?.LogError("ContentField is null");
#endif
                    return;
                }

                ContentField.SetValue(instance, input);
                
                if (FontField == null)
                {
#if DEBUG
                    TranslationMod.Logger?.LogError("FontField is null");
#endif
                    return;
                }
                
                var font = FontField.GetValue(instance) as Font;
                if (font == null)
                {
                    TranslationMod.Logger?.LogError("No font set for text box:" + input);
                    MainControl.logError("No font set for text box:" + input);
                    return;
                }
                
                // 首字符装饰初始化
                if (instance.illuminatedFont != null)
                {   
                    // 获取首字符在字图集里的子图编号
                    int subimageForChar = StringPrinter.getSubimageForChar(translated[0]);
                    
                    // Расширенная проверка для кириллицы (ваш комментарий: переделать на проверку взятых кодов из кодировки)
                    // 0-25、 90-122 字符需要进行装饰
                    if (subimageForChar <= 25 || (subimageForChar >= 90 && subimageForChar <= 122))
                    {                        
                        if (IlluminatedImageField == null)
                        {
                            TranslationMod.Logger?.LogError("IlluminatedImageField is null");
                            return;
                        }
                        
                        var illuminatedImage = new UICanvasVertical();
                        // 将首字母从待翻译内容中去掉
                        translated = translated.Substring(1);       
                        //设置贴图
                        illuminatedImage.backgroundTexture = TextureTools.getLetterSubImageTextureData(subimageForChar, instance.illuminatedFont.getModelPath());
                        //设置右间距
                        illuminatedImage.padding.right = font.wordSpacing;
                        IlluminatedImageField.SetValue(instance, illuminatedImage);     // 设置 instance.illuminatedImage为illuminatedImage
                        instance.add(illuminatedImage);                                 // 将此装饰首字符对象添加至控件元素树
                    }
                    else
                    {
#if DEBUG
                        TranslationMod.Logger?.LogInfo($"Character '{translated[0]}' with subimage {subimageForChar} doesn't match illuminated criteria");
#endif
                    }
                }
                translated = (string)PreProcessStringMethod.Invoke(instance, new object[] { translated });
                
                string taggedInput = identifyTooltipKeywords(instance, input);
                if(taggedInput != input)
                {
#if DEBUG
                    TranslationMod.Logger?.LogInfo($"Tagged input: {taggedInput}");
#endif
                    
                    // Извлекаем tooltip ключи и добавляем их в буфер
                    //提取 tooltip 的key，并将它们添加到缓冲区
                    var keys = ExtractAndBufferTooltipKeys(taggedInput);
                    
                    // Оборачиваем переведенные ключи в теги <tag></tag>
                    //将已翻译的key用 <tag></tag> 标签包裹起来
                    translated = TagKeys(translated, keys);
                    
#if DEBUG
                    TranslationMod.Logger?.LogInfo($"Translated tagged input: {translated}");
#endif
                }

                if (SplitIntoParagraphMethod == null)
                {
                    TranslationMod.Logger?.LogError("SplitIntoParagraphMethod is null");
                    return;
                }
                // 按照待翻译文本中的换行\r\n、\r、\n将文本分割为多行文本list
                var paragraphs = (List<string>)SplitIntoParagraphMethod.Invoke(instance, new object[] { translated });
                if (paragraphs != null)
                {
                    foreach (string paragraph in paragraphs)
                    {   
                        ParseParagraphComplete(instance, paragraph);
                    }
                }
                
                if (PruneLengthMethod == null)
                {
                    TranslationMod.Logger?.LogError("PruneLengthMethod is null");
                    return;
                }
                
                PruneLengthMethod.Invoke(instance, null);
                
                instance.alignElements();                
            }
            catch (Exception ex)
            {
                TranslationMod.Logger?.LogError($"Exception in SetContentComplete: {ex.Message}");
                TranslationMod.Logger?.LogError($"Stack trace: {ex.StackTrace}");
                throw; // Перебрасываем исключение для обработки в Prefix
            }
        }

        /// <summary>
        /// Извлекает tooltip ключи из тегов <tag>tooltipKey</tag>, переводит их и добавляет в буфер
        /// </summary>
        /// <param name="input">Исходный текст с тегами</param>
        private static Dictionary<string, string> ExtractAndBufferTooltipKeys(string input)
        {
            Dictionary<string, string> keys = new Dictionary<string, string>();
            try
            {
                var matches = TooltipTagRegex.Matches(input);
                if (matches.Count == 0)
                {
#if DEBUG
                    TranslationMod.Logger?.LogDebug($"[TooltipBuffer] No tooltip tags found in: {input}");
#endif
                    return keys;
                }
#if DEBUG
                TranslationMod.Logger?.LogDebug($"[TooltipBuffer] Match Count: {matches.Count}");
#endif 
                lock (_tooltipBufferLock)
                {
                    foreach (Match match in matches)
                    {
#if DEBUG
                        TranslationMod.Logger?.LogDebug($"[TooltipBuffer] Match Count: {match.Groups.Count}");
#endif      
                        if (match.Groups.Count > 1)
                        {          
                            string originalKey = match.Groups[1].Value;
                            if (string.IsNullOrWhiteSpace(originalKey))
                                continue;

                            // Переводим ключ
                            string translatedKey;
                            if(originalKey.Contains("#"))
                            {
                                translatedKey = _translator.Value.Process(originalKey.Replace("#", ""));                                translatedKey = translatedKey.Replace("#", "");
                            }
                            else 
                            {
                                translatedKey = _translator.Value.Process(originalKey);
                            }
                            if(!keys.ContainsKey(translatedKey))
                            {
                                keys.Add(translatedKey, originalKey);
                            }
                        }
                    }
                }
                return keys;
            }
            catch (Exception ex)
            {
                TranslationMod.Logger?.LogError($"[TooltipBuffer] Error extracting tooltip keys: {ex.Message}");
                return keys;
            }
        }

        private static string identifyTooltipKeywords(UITextBlock instance, string input)
        {
            if (IdentifyTooltipKeywordsMethod == null)
            {
                TranslationMod.Logger?.LogError("IdentifyTooltipKeywordsMethod is null");
                return input;
            }
            return (string)IdentifyTooltipKeywordsMethod.Invoke(instance, new object[] { input });
        }

        public static string StripHashTags(string input)
        {
            // (?<![=:])   — перед # нет = или :  → значит это "живой" тег, а не цвет
            // ([\p{L}\d_-]+) — буквы любых алфавитов, цифры, _ и -
            var regex = new Regex(@"(?<![=:])#([\p{L}\d_-]+)",
                                RegexOptions.Compiled | RegexOptions.Multiline);

            var found = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            // MatchEvaluator сразу и собирает тег, и отдаёт замену без #
            string result = regex.Replace(input, m =>
            {
                string tag = m.Groups[1].Value;   // уже без «#»
                found.Add(tag);
                return tag;                       // замена в тексте
            });
            return result;
        }

        public static string HighlightKeysInText(string input, List<string> keys)
        {
            if (string.IsNullOrWhiteSpace(input) || keys == null || keys.Count == 0)
                return input;

            // Сортируем ключи по убыванию длины для приоритета длинных совпадений
            var orderedKeys = keys
                .Where(k => !string.IsNullOrWhiteSpace(k))
                .OrderByDescending(k => k.Length)
                .ToList();

            foreach (var key in orderedKeys)
            {
                // Удаляем лишние пробелы и приводим к нижнему регистру для сопоставления
                string normalizedKey = key.Trim();

                // Пытаемся найти слова, начинающиеся на ключ и допускающие падежные окончания
                string pattern = $@"\b({Regex.Escape(normalizedKey)}\p{{L}}*)\b";

                input = Regex.Replace(
                    input,
                    pattern,
                    match =>
                    {
                        // Уже содержит тег — пропускаем
                        if (match.Value.Contains("<tag>") || match.Value.Contains("</tag>"))
                            return match.Value;

                        // Заворачиваем найденное совпадение
                        return $"<tag>{match.Value}</tag>";
                    },
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
            }

            return input;
        }
        
        private struct PatternInfo
        {
            public string OriginalKey { get; set; }
            public string Key { get; set; }
            public Regex Rx { get; set; }
        }
        /// <summary>
        /// Оборачивает найденные ключи в <tag></tag> с предотвращением дублирования.
        /// </summary>
        public static string TagKeys(string text, Dictionary<string, string> dict)
        {
            if (string.IsNullOrWhiteSpace(text) || dict == null)
                return text;

            try
            {
                // Набор для отслеживания уже обработанных позиций
                var processedRanges = new List<(int start, int end)>();
                
                // 1) строим Regex-паттерны для всех ключей (длинные → первыми)
                var patternInfos = dict
                    .Where(kvp => !string.IsNullOrWhiteSpace(kvp.Key))
                    .OrderByDescending(kvp => kvp.Key.Length)
                    .Select(kvp => 
                    {
                        try
                        {
                            var pattern = BuildPattern(kvp.Key);
#if DEBUG
                            TranslationMod.Logger?.LogInfo($"[TagKeys] Pattern: {pattern}");
#endif
                            if (string.IsNullOrEmpty(pattern))
                                return (PatternInfo?)null;
                            

                            return (PatternInfo?)new PatternInfo
                            {
                                OriginalKey = kvp.Value,
                                Key = kvp.Key,
                                Rx  = new Regex(
                                        pattern,
                                        RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)
                            };
                        }
                        catch (Exception ex)
                        {
                            TranslationMod.Logger?.LogError($"[TagKeys] Error building pattern for key '{kvp.Key}': {ex.Message}");
                            return (PatternInfo?)null;
                        }
                    })
                    .Where(rx => rx.HasValue)
                    .Select(rx => rx.Value)
                    .ToArray();

                // 2) Собираем все совпадения с их позициями
                var allMatches = new List<(Match match, PatternInfo pattern, int priority)>();
                
                for (int i = 0; i < patternInfos.Length; i++)
                {
                    var p = patternInfos[i];
                    try
                    {
                        var matches = p.Rx.Matches(text);
                        foreach (Match match in matches)
                        {
                            // Проверяем, что совпадение не содержит уже тег
                            if (!match.Value.Contains("<tag>") && !match.Value.Contains("</tag>"))
                            {
                                allMatches.Add((match, p, i)); // i как приоритет (меньше = выше приоритет)
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        TranslationMod.Logger?.LogError($"[TagKeys] Error finding matches for pattern: {ex.Message}");
                    }
                }

                // 3) Сортируем по приоритету (длина ключа) и позиции
                allMatches = allMatches
                    .OrderBy(x => x.priority) // Сначала по приоритету (длинные ключи первыми)
                    .ThenBy(x => x.match.Index) // Затем по позиции в тексте
                    .ToList();

                // 4) Отфильтровываем пересекающиеся совпадения
                var finalMatches = new List<(Match match, PatternInfo pattern)>();
                
                foreach (var (match, pattern, _) in allMatches)
                {
                    int start = match.Index;
                    int end = match.Index + match.Length - 1;
                    
                    // Проверяем, не пересекается ли с уже выбранными совпадениями
                    bool overlaps = processedRanges.Any(range => 
                        !(end < range.start || start > range.end));
                    
                    if (!overlaps)
                    {
                        finalMatches.Add((match, pattern));
                        processedRanges.Add((start, end));
#if DEBUG
                        TranslationMod.Logger?.LogDebug($"[TagKeys] Added match: '{match.Value}' at {start}-{end}");
#endif
                    }
                    else
                    {
#if DEBUG
                        TranslationMod.Logger?.LogDebug($"[TagKeys] Skipped overlapping match: '{match.Value}' at {start}-{end}");
#endif
                    }
                }

                // 5) Применяем замены в обратном порядке (от конца к началу), чтобы не сбить позиции
                finalMatches = finalMatches.OrderByDescending(x => x.match.Index).ToList();
                
                foreach (var (match, pattern) in finalMatches)
                {
                    try
                    {
                        string matchValue = match.Value;
                        string replacement = $"<tag>{matchValue}</tag>";
                        
                        // Добавляем в буфер tooltip ключей
                        if (!TooltipKeyBuffer.ContainsKey(matchValue) && matchValue != pattern.OriginalKey)
                        {
                            TooltipKeyBuffer.Add(matchValue, pattern.OriginalKey);
                        }
                        
                        // Заменяем в тексте
                        text = text.Substring(0, match.Index) + replacement + text.Substring(match.Index + match.Length);
                        
#if DEBUG
                        TranslationMod.Logger?.LogDebug($"[TagKeys] Replaced '{matchValue}' with '{replacement}'");
#endif
                    }
                    catch (Exception ex)
                    {
                        TranslationMod.Logger?.LogError($"[TagKeys] Error applying replacement: {ex.Message}");
                    }
                }

                return text;
            }
            catch (Exception ex)
            {
                TranslationMod.Logger?.LogError($"[TagKeys] General error in TagKeys: {ex.Message}");
                return text; // Возвращаем исходный текст в случае ошибки
            }
        }

        /* ── PRIVATE ──────────────────────────────────────────── */

        private static void ParseParagraphComplete(UITextBlock instance, string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return;
            }

            var textBlockType = typeof(UITextBlock);
            var createLineMethod = textBlockType.GetMethod("createLine", BindingFlags.NonPublic | BindingFlags.Instance);
            var shouldCreateNewLineMethod = textBlockType.GetMethod("shouldWeCreateNewLine", BindingFlags.NonPublic | BindingFlags.Instance);
            var getPixelsToNextTabMethod = textBlockType.GetMethod("getPixelsToNextTab", BindingFlags.NonPublic | BindingFlags.Instance);
            var trimBraceMethod = textBlockType.GetMethod("trimBrace", BindingFlags.NonPublic | BindingFlags.Instance);
            var getDrawColorMethod = textBlockType.GetMethod("getDrawColor", BindingFlags.NonPublic | BindingFlags.Instance);
            var pushColorMethod = textBlockType.GetMethod("pushColor", BindingFlags.NonPublic | BindingFlags.Instance);
            var popColorMethod = textBlockType.GetMethod("popColor", BindingFlags.NonPublic | BindingFlags.Instance);

            var currentTooltipWordField = textBlockType.GetField("currentTooltipWord", BindingFlags.NonPublic | BindingFlags.Instance);
            var drawingQuoteField = textBlockType.GetField("drawingQuote", BindingFlags.NonPublic | BindingFlags.Instance);
            var uppercaseTooltipWordsField = textBlockType.GetField("uppercaseTooltipWords", BindingFlags.NonPublic | BindingFlags.Instance);
            var highlightQuotesField = textBlockType.GetField("highlightQuotes", BindingFlags.NonPublic | BindingFlags.Instance);
            var toolTipHighlightColorField = textBlockType.GetField("toolTipHighlightColor", BindingFlags.NonPublic | BindingFlags.Instance);
            var toolTipWordsField = textBlockType.GetField("toolTipWords", BindingFlags.NonPublic | BindingFlags.Instance);
            var multiLineField = textBlockType.GetField("multiLine", BindingFlags.NonPublic | BindingFlags.Instance);

            var wordType = textBlockType.GetNestedType("Word", BindingFlags.NonPublic);
            var letterType = textBlockType.GetNestedType("Letter", BindingFlags.NonPublic);
            var wordCtor = wordType?.GetConstructor(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                null,
                new[] { typeof(Font) },
                null);
            var letterCtor = letterType?.GetConstructor(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                null,
                new[] { typeof(char), typeof(Color32), typeof(Color32), typeof(Font) },
                null);
            var wordGetWidthInLineMethod = wordType?.GetMethod("getWidthInLine", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            var wordHighlightWordField = wordType?.GetField("highlightWord", BindingFlags.Public | BindingFlags.Instance);

            if (createLineMethod == null ||
                shouldCreateNewLineMethod == null ||
                getPixelsToNextTabMethod == null ||
                trimBraceMethod == null ||
                getDrawColorMethod == null ||
                pushColorMethod == null ||
                popColorMethod == null ||
                currentTooltipWordField == null ||
                drawingQuoteField == null ||
                uppercaseTooltipWordsField == null ||
                highlightQuotesField == null ||
                toolTipHighlightColorField == null ||
                toolTipWordsField == null ||
                multiLineField == null ||
                wordCtor == null ||
                letterCtor == null ||
                wordGetWidthInLineMethod == null ||
                wordHighlightWordField == null)
            {
                ParseParagraphMethod?.Invoke(instance, new object[] { input });
                return;
            }

            var font = FontField.GetValue(instance) as Font;
            if (font == null)
            {
                return;
            }

            object CreateWord() => wordCtor.Invoke(new object[] { font });
            UIElement CreateLetter(char c, Color32 main, Color32 shadow) => (UIElement)letterCtor.Invoke(new object[] { c, main, shadow, font });
            UICanvasHorizontal CreateLine() => (UICanvasHorizontal)createLineMethod.Invoke(instance, null);
            bool ShouldCreateNewLine(UICanvasHorizontal line, object word) => (bool)shouldCreateNewLineMethod.Invoke(instance, new[] { line, word });
            int GetPixelsToNextTab(UICanvasHorizontal line) => (int)getPixelsToNextTabMethod.Invoke(instance, new object[] { line });
            int GetWordWidthInLine(object word) => (int)wordGetWidthInLineMethod.Invoke(word, null);
            string TrimBrace(string s) => (string)trimBraceMethod.Invoke(instance, new object[] { s });
            Color32 GetDrawColor() => (Color32)getDrawColorMethod.Invoke(instance, null);
            void PushColor(Color32 color) => pushColorMethod.Invoke(instance, new object[] { color });
            void PopColor() => popColorMethod.Invoke(instance, null);
            string CurrentTooltipWord() => (string)currentTooltipWordField.GetValue(instance);
            void SetCurrentTooltipWord(string s) => currentTooltipWordField.SetValue(instance, s);
            bool DrawingQuote() => (bool)drawingQuoteField.GetValue(instance);
            void SetDrawingQuote(bool value) => drawingQuoteField.SetValue(instance, value);
            bool UppercaseTooltipWords() => (bool)uppercaseTooltipWordsField.GetValue(instance);
            bool HighlightQuotes() => (bool)highlightQuotesField.GetValue(instance);
            Color32 ToolTipHighlightColor() => (Color32)toolTipHighlightColorField.GetValue(instance);
            bool MultiLine() => (bool)multiLineField.GetValue(instance);
            void SetWordHighlightWord(object word, string s) => wordHighlightWordField.SetValue(word, s);

            UICanvasHorizontal lineCanvas = CreateLine();
            object wordObj = CreateWord();
            bool headerFlag = false;

            int i = 0;
            while (i < input.Length)
            {
                if (input[i] == '<')
                {
                    string text = input.Substring(i);
                    int num = text.IndexOf(">") + 1;
                    if (num > 0)
                    {
                        if (text.IndexOf(C64Color.HEADER_TAG_CONTENT) == 1)
                        {
                            PushColor(C64Color.HeaderColor);
                            headerFlag = true;
                        }
                        else if (text.IndexOf("/" + C64Color.HEADER_TAG_CONTENT) == 1)
                        {
                            PopColor();
                            headerFlag = false;
                        }
                        else if (text.IndexOf("tag") == 1 && !headerFlag)
                        {
                            SetCurrentTooltipWord(text.Substring(num).Split(new[] { '<' }, StringSplitOptions.None)[0]);
                            PushColor(ToolTipHighlightColor());
                        }
                        else if (text.IndexOf("/tag") == 1 && !headerFlag && CurrentTooltipWord() != "")
                        {
                            SetCurrentTooltipWord("");
                            PopColor();
                        }
                        else
                        {
                            string text2 = TrimBrace(text.Substring(0, num));
                            if (text2.IndexOf("color=".ToUpper()) == 1)
                            {
                                int startIndex = text2.IndexOf("#".ToUpper());
                                Color color;
                                if (startIndex >= 0 &&
                                    startIndex + 7 <= text2.Length &&
                                    ColorUtility.TryParseHtmlString(text2.Substring(startIndex, 7), out color))
                                {
                                    PushColor(color);
                                }
                            }
                            else if (text2.IndexOf("/color".ToUpper()) == 1)
                            {
                                PopColor();
                            }
                        }

                        if (num > 1)
                        {
                            i += num - 1;
                        }

                        if (i >= input.Length - 1)
                        {
                            if (ShouldCreateNewLine(lineCanvas, wordObj))
                            {
                                lineCanvas = CreateLine();
                            }
                            lineCanvas.add((UIElement)wordObj);
                            break;
                        }

                        i++;
                        continue;
                    }
                }
                else if (input[i] == '\t')
                {
                    lineCanvas.add((UIElement)wordObj);
                    object tabWord = CreateWord();
                    ((UICanvasHorizontal)tabWord).setWidth(GetPixelsToNextTab(lineCanvas));
                    lineCanvas.add((UIElement)tabWord);
                    wordObj = CreateWord();
                    i++;
                    continue;
                }

                if (input[i] != ' ')
                {
                    bool closingQuote = false;
                    if (input[i] == '"' && HighlightQuotes())
                    {
                        if (DrawingQuote())
                        {
                            closingQuote = true;
                        }
                        SetDrawingQuote(!DrawingQuote());
                    }

                    UIElement letter;
                    if (CurrentTooltipWord() != "")
                    {
                        char ch = UppercaseTooltipWords() ? char.ToUpper(input[i]) : input[i];
                        letter = CreateLetter(ch, GetDrawColor(), instance.foregroundColors.shadowMainColor);
                    }
                    else if (DrawingQuote() || closingQuote)
                    {
                        letter = CreateLetter(input[i], C64Color.SmallTextQuoteColor, instance.foregroundColors.shadowMainColor);
                    }
                    else
                    {
                        letter = CreateLetter(input[i], GetDrawColor(), instance.foregroundColors.shadowMainColor);
                    }

                    letter.foregroundColors.hoverColor = instance.foregroundColors.hoverColor;
                    letter.foregroundColors.leftClickedColor = instance.foregroundColors.leftClickedColor;
                    letter.foregroundColors.outlineMainColor = instance.foregroundColors.outlineMainColor;

                    if (MultiLine() || (!MultiLine() && lineCanvas.getWidth() + GetWordWidthInLine(wordObj) <= instance.getBaseWidth()))
                    {
                        ((UICanvasHorizontal)wordObj).add(letter);
                    }

                    if (CurrentTooltipWord() != "")
                    {
                        SetWordHighlightWord(wordObj, CurrentTooltipWord());
                        var toolTipWords = toolTipWordsField.GetValue(instance) as System.Collections.IList;
                        if (toolTipWords != null && !toolTipWords.Contains(wordObj))
                        {
                            toolTipWords.Add(wordObj);
                        }
                    }
                }

                if (input[i] == ' ' ||
                    i >= input.Length - 1 ||
                    (GetWordWidthInLine(wordObj) >= instance.getBaseWidth() && !instance.stretchHorizontal) ||
                    IsNonAlphanumericSymbol(input[i]))
                {
                    bool needNewLine = ShouldCreateNewLine(lineCanvas, wordObj);
                    if (input[i] == ' ')
                    {
                        ((UICanvasHorizontal)wordObj).add(CreateLetter(input[i], GetDrawColor(), instance.foregroundColors.shadowMainColor));
                    }

                    if (needNewLine)
                    {
                        lineCanvas = CreateLine();
                        lineCanvas.add((UIElement)wordObj);
                    }
                    else if (i == input.Length - 1)
                    {
                        lineCanvas.add((UIElement)wordObj);
                        lineCanvas = CreateLine();
                    }
                    else
                    {
                        lineCanvas.add((UIElement)wordObj);
                    }

                    wordObj = CreateWord();
                }

                i++;
            }

            if (lineCanvas != null && lineCanvas.getElements().Count == 0)
            {
                instance.getElements().Remove(lineCanvas);
            }
        }

        private static bool IsNonAlphanumericSymbol(char c)
        {
            if (char.IsWhiteSpace(c))
            {
                return false;
            }

            if (c > sbyte.MaxValue)
            {
                return true;
            }

            return !char.IsLetterOrDigit(c);
        }

        /// строит приближённый паттерн по ключу
        private static string BuildPattern(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return string.Empty;

            // «Очки развития» → «Очк\w* \s+ разви\w*»
            var tokens = key.Split(new[] { ' ', '\t' },
                                StringSplitOptions.RemoveEmptyEntries);

            var parts = tokens.Select(t =>
            {
                if (string.IsNullOrEmpty(t))
                    return string.Empty;

                // берём 85 % слова (но ≥ 3 символов), остальное – любое окончание
                int keep = Math.Max(3, (int)Math.Ceiling(t.Length * 0.65));
                // Убеждаемся, что keep не превышает длину строки
                keep = Math.Min(keep, t.Length);
                
                // Для очень коротких слов (1-2 символа) используем полное слово
                if (t.Length <= 2)
                    keep = t.Length;
                
                string stem = Regex.Escape(t.Substring(0, keep));
                return stem + @"[\w\.-]*";
            }).Where(p => !string.IsNullOrEmpty(p));

            return $@"\b{string.Join(@"\s+", parts)}\b";
        }
    }
}
