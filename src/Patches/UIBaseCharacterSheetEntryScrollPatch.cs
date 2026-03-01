using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace TranslationMod.Patches
{
    // 为 UIBaseCharacterSheet 的 entry 增加分页与滚动条。
    // 仅在 CharacterCreationStatsState.setGUIData 调用期间生效。
    [HarmonyPatch]
    public static class UIBaseCharacterSheetEntryScrollPatch
    {
        private sealed class EntryScrollState
        {
            public UIScrollbarStandard Scrollbar;
            public int ScrollStart;
            public bool IsEditorEntry;
        }

        private static readonly Dictionary<object, EntryScrollState> _entryStates = new();
        private static readonly HashSet<SkaldDataList> _pagedLists = new();
        private static readonly object _lock = new();

        // 与系统其他位置使用的滚动条宽度保持一致。
        private const int ScrollbarWidth = 13;
        private const int ScrollbarGap = 2;

        private static readonly Type SheetEntryType = AccessTools.Inner(typeof(UIBaseCharacterSheet), "SheetEntry");
        private static readonly Type EditorSheetEntryType = AccessTools.Inner(typeof(UIBaseCharacterSheet), "EditorSheetEntry");

        private static readonly FieldInfo ButtonsField = SheetEntryType != null
            ? AccessTools.Field(SheetEntryType, "buttons")
            : null;
        private static readonly FieldInfo ColumnsField = EditorSheetEntryType != null
            ? AccessTools.Field(EditorSheetEntryType, "columns")
            : null;
        private static readonly FieldInfo ButtonColumnField = EditorSheetEntryType != null
            ? AccessTools.Field(EditorSheetEntryType, "buttonColumn")
            : null;
        private static readonly FieldInfo MinusColumnField = EditorSheetEntryType != null
            ? AccessTools.Field(EditorSheetEntryType, "minusColumn")
            : null;
        private static readonly FieldInfo PlusColumnField = EditorSheetEntryType != null
            ? AccessTools.Field(EditorSheetEntryType, "plusColumn")
            : null;
        private static readonly FieldInfo PointBlockField = EditorSheetEntryType != null
            ? AccessTools.Field(EditorSheetEntryType, "pointBlock")
            : null;

        [HarmonyPatch]
        private static class SheetEntryUpdatePatch
        {
            [HarmonyTargetMethod]
            private static MethodBase TargetMethod()
            {
                return SheetEntryType == null ? null : AccessTools.Method(SheetEntryType, "update", new[] { typeof(SkaldDataList) });
            }

            [HarmonyPrefix]
            private static void Prefix(object __instance, ref SkaldDataList data)
            {
                if (!CharacterCreationStatsStatePatch.IsSetGuiDataRunning)
                {
                    return;
                }

                data = BuildPagedDataAndUpdateScrollbar(__instance, data);
            }

            [HarmonyPostfix]
            private static void Postfix(object __instance)
            {
                if (!CharacterCreationStatsStatePatch.IsSetGuiDataRunning)
                {
                    return;
                }

                RefreshScrollbarLayout(__instance);
            }
        }

        [HarmonyPatch]
        private static class EditorSheetEntryUpdatePatch
        {
            [HarmonyTargetMethod]
            private static MethodBase TargetMethod()
            {
                return EditorSheetEntryType == null ? null : AccessTools.Method(EditorSheetEntryType, "update", new[] { typeof(SkaldDataList) });
            }

            [HarmonyPrefix]
            private static void Prefix(object __instance, ref SkaldDataList data)
            {
                if (!CharacterCreationStatsStatePatch.IsSetGuiDataRunning)
                {
                    return;
                }

                data = BuildPagedDataAndUpdateScrollbar(__instance, data);
            }

            [HarmonyPostfix]
            private static void Postfix(object __instance)
            {
                if (!CharacterCreationStatsStatePatch.IsSetGuiDataRunning)
                {
                    return;
                }

                RefreshScrollbarLayout(__instance);
                SyncPointBlockHeightToTinyFont(__instance);
            }
        }

        private static SkaldDataList BuildPagedDataAndUpdateScrollbar(object entryObject, SkaldDataList fullData)
        {
            if (entryObject == null || fullData == null || IsPagedList(fullData))
            {
                return fullData;
            }

            var buttons = ButtonsField?.GetValue(entryObject) as ListButtonControl;
            if (buttons == null)
            {
                return fullData;
            }

            var entryState = GetOrCreateState(entryObject, buttons);
            RefreshScrollbarLayout(entryObject);

            var fullObjects = fullData.getObjectList();
            int count = fullObjects?.Count ?? 0;
            if (count == 0)
            {
                entryState.ScrollStart = 0;
                return fullData;
            }

            int originalPageSize = fullData.getMaxPageSize();

            int pageSize = Math.Max(1, originalPageSize * 9 / 12);
            int maxStart = Math.Max(0, count - pageSize);

            entryState.Scrollbar.updateMouseInteraction();
            entryState.Scrollbar.setIncrement(Math.Max(1, maxStart + 1));
            entryState.ScrollStart = Math.Max(0, Math.Min(maxStart,
                (int)Math.Round(maxStart * entryState.Scrollbar.getScrollDegree())));

            int take = Math.Min(pageSize, count - entryState.ScrollStart);
            if (take <= 0)
            {
                return fullData;
            }

            var pagedData = new SkaldDataList();
            for (int i = 0; i < take; i++)
            {
                pagedData.add(fullObjects[entryState.ScrollStart + i]);
            }

            lock (_lock)
            {
                _pagedLists.Add(pagedData);
            }

            return pagedData;
        }

        private static void RefreshScrollbarLayout(object entryObject)
        {
            var buttons = ButtonsField?.GetValue(entryObject) as ListButtonControl;
            var state = GetState(entryObject);
            if (buttons == null || state?.Scrollbar == null)
            {
                return;
            }

            int height = Math.Max(24, buttons.getHeight());
            state.Scrollbar.setHeight(height);

            if (!state.IsEditorEntry)
            {
                int x = buttons.getX() + buttons.getWidth() + ScrollbarGap;
                int y = buttons.getY();
                state.Scrollbar.fixedPosition = true;
                state.Scrollbar.setPosition(x, y);
            }

            if (state.IsEditorEntry)
            {
                EnforceEditorControlSizes(entryObject);
                var columns = ColumnsField?.GetValue(entryObject) as UICanvasHorizontal;
                columns?.alignElements();
            }
        }

        private static void SyncPointBlockHeightToTinyFont(object entryObject)
        {
            if (EditorSheetEntryType == null || !EditorSheetEntryType.IsInstanceOfType(entryObject))
            {
                return;
            }

            var pointBlock = PointBlockField?.GetValue(entryObject) as UITextBlock;
            if (pointBlock == null)
            {
                return;
            }

            var tiny = FontContainer.getTinyFont();
            if (tiny == null)
            {
                return;
            }

            int tinyHeight = Math.Max(1, tiny.wordHeight);
            if (pointBlock.getBaseHeight() != tinyHeight)
            {
                pointBlock.setHeight(tinyHeight);
            }
        }

        private static void EnforceEditorControlSizes(object entryObject)
        {
            var buttons = ButtonsField?.GetValue(entryObject) as ListButtonControl;
            if (buttons != null && buttons.getBaseWidth() != 70)
            {
                // 将 EditorSheetEntry 的文本按钮列宽从 80 调整为 70。
                buttons.setWidth(70);
            }

            var plusColumn = PlusColumnField?.GetValue(entryObject) as UICanvasVertical;
            var tiny = FontContainer.getTinyFont();
            if (plusColumn != null && tiny != null)
            {
                int targetTop = 4 + Math.Max(1, tiny.wordHeight);
                if (plusColumn.padding.top != targetTop)
                {
                    var padding = plusColumn.padding;
                    padding.top = targetTop;
                    plusColumn.padding = padding;
                }
            }
        }

        private static EntryScrollState GetOrCreateState(object entryObject, ListButtonControl buttons)
        {
            lock (_lock)
            {
                if (_entryStates.TryGetValue(entryObject, out var existing))
                {
                    return existing;
                }

                bool isEditor = EditorSheetEntryType != null && EditorSheetEntryType.IsInstanceOfType(entryObject);
                var scrollBar = new UIScrollbarStandard(ScrollbarWidth, Math.Max(24, buttons.getHeight()), buttons);
                scrollBar.padding.top = -1;

                if (isEditor)
                {
                    var columns = ColumnsField?.GetValue(entryObject) as UICanvasHorizontal;
                    if (columns != null)
                    {
                        scrollBar.fixedPosition = false;
                        scrollBar.padding.left = ScrollbarGap;
                        columns.add(scrollBar);
                    }
                }
                else
                {
                    var entry = entryObject as UICanvasVertical;
                    if (entry != null)
                    {
                        scrollBar.fixedPosition = true;
                        entry.add(scrollBar);
                    }
                }

                var state = new EntryScrollState
                {
                    Scrollbar = scrollBar,
                    ScrollStart = 0,
                    IsEditorEntry = isEditor
                };
                _entryStates[entryObject] = state;
                return state;
            }
        }

        private static EntryScrollState GetState(object entryObject)
        {
            lock (_lock)
            {
                _entryStates.TryGetValue(entryObject, out var state);
                return state;
            }
        }

        private static bool IsPagedList(SkaldDataList data)
        {
            lock (_lock)
            {
                return _pagedLists.Contains(data);
            }
        }
    }
}
