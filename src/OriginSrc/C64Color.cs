using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200002F RID: 47
public static class C64Color
{
	// Token: 0x060004AD RID: 1197 RVA: 0x000168EC File Offset: 0x00014AEC
	public static void loadColors()
	{
		C64Color.skinColorIds = new List<string>();
		C64Color.skinColorForNPCIds = new List<string>();
		C64Color.hairColorIds = new List<string>();
		C64Color.clothingColorIds = new List<string>();
		C64Color.fontColorIds = new List<string>();
		C64Color.fontShadowColorIds = new List<string>();
		C64Color.colorDictionary = new Dictionary<string, C64Color.ColorData>();
		C64Color.fontColorIds.Add("");
		C64Color.fontShadowColorIds.Add("");
		C64Color.BLACK = C64Color.loadColor(C64Color.ColorIds.COL_Black, "000000");
		C64Color.WHITE = C64Color.loadColor(C64Color.ColorIds.COL_White, "ffffff");
		C64Color.GRAY_DARK = C64Color.loadColor(C64Color.ColorIds.COL_GrayDark, "4A4A4A");
		C64Color.GRAY = C64Color.loadColor(C64Color.ColorIds.COL_Gray, "7B7B7B");
		C64Color.GRAY_LIGHT = C64Color.loadColor(C64Color.ColorIds.COL_GrayLight, "B2B2B2");
		C64Color.RED = C64Color.loadColor(C64Color.ColorIds.COL_Red, "813338");
		C64Color.RED_LIGHT = C64Color.loadColor(C64Color.ColorIds.COL_RedLight, "BB776D");
		C64Color.BROWN = C64Color.loadColor(C64Color.ColorIds.COL_Brown, "5C4700");
		C64Color.BROWN_LIGHT = C64Color.loadColor(C64Color.ColorIds.COL_BrownLight, "905F25");
		C64Color.YELLOW = C64Color.loadColor(C64Color.ColorIds.COL_Yellow, "EDF171");
		C64Color.GREEN = C64Color.loadColor(C64Color.ColorIds.COL_Green, "56AC4D");
		C64Color.GREEN_LIGHT = C64Color.loadColor(C64Color.ColorIds.COL_GreenLight, "A9FF9F");
		C64Color.CYAN = C64Color.loadColor(C64Color.ColorIds.COL_Cyan, "75CEC8");
		C64Color.BLUE = C64Color.loadColor(C64Color.ColorIds.COL_Blue, "2E2C9B");
		C64Color.BLUE_LIGHT = C64Color.loadColor(C64Color.ColorIds.COL_BlueLight, "706DEB");
		C64Color.VIOLET = C64Color.loadColor(C64Color.ColorIds.COL_Violet, "8E3C97");
		C64Color.colorList = new Color32[]
		{
			C64Color.Black,
			C64Color.White,
			C64Color.GrayDark,
			C64Color.Gray,
			C64Color.GrayLight,
			C64Color.Red,
			C64Color.RedLight,
			C64Color.Brown,
			C64Color.BrownLight,
			C64Color.Yellow,
			C64Color.Green,
			C64Color.GreenLight,
			C64Color.Cyan,
			C64Color.Blue,
			C64Color.BlueLight,
			C64Color.Violet
		};
	}

	// Token: 0x060004AE RID: 1198 RVA: 0x00016B38 File Offset: 0x00014D38
	private static C64Color.ColorData loadColor(C64Color.ColorIds colorId, string backupHex)
	{
		string text = colorId.ToString();
		C64Color.ColorData colorData = new C64Color.ColorData(colorId, backupHex);
		C64Color.colorDictionary.Add(text.ToUpper(), colorData);
		if (colorData.skinColor)
		{
			C64Color.skinColorIds.Add(text);
		}
		if (colorData.skinColor || colorData.skinColorForNpc)
		{
			C64Color.skinColorForNPCIds.Add(text);
		}
		if (colorData.hairColor)
		{
			C64Color.hairColorIds.Add(text);
		}
		if (colorData.clothingColor)
		{
			C64Color.clothingColorIds.Add(text);
		}
		if (colorData.fontColor)
		{
			C64Color.fontColorIds.Add(text);
		}
		if (colorData.fontShadowColor)
		{
			C64Color.fontShadowColorIds.Add(text);
		}
		return colorData;
	}

	// Token: 0x060004AF RID: 1199 RVA: 0x00016BE7 File Offset: 0x00014DE7
	public static List<string> getSkinColors()
	{
		return C64Color.skinColorIds;
	}

	// Token: 0x060004B0 RID: 1200 RVA: 0x00016BEE File Offset: 0x00014DEE
	public static List<string> getSkinColorsForAll()
	{
		return C64Color.skinColorForNPCIds;
	}

	// Token: 0x060004B1 RID: 1201 RVA: 0x00016BF5 File Offset: 0x00014DF5
	public static List<string> getHairColors()
	{
		return C64Color.hairColorIds;
	}

	// Token: 0x060004B2 RID: 1202 RVA: 0x00016BFC File Offset: 0x00014DFC
	public static List<string> getClothingColors()
	{
		return C64Color.clothingColorIds;
	}

	// Token: 0x060004B3 RID: 1203 RVA: 0x00016C03 File Offset: 0x00014E03
	public static List<string> getFontColors()
	{
		return C64Color.fontColorIds;
	}

	// Token: 0x060004B4 RID: 1204 RVA: 0x00016C0A File Offset: 0x00014E0A
	public static List<string> getFontShadowColors()
	{
		return C64Color.fontShadowColorIds;
	}

	// Token: 0x060004B5 RID: 1205 RVA: 0x00016C14 File Offset: 0x00014E14
	private static C64Color.ColorData getColorFromDictionary(string id)
	{
		if (id == null || id == "")
		{
			return null;
		}
		id = id.ToUpper();
		if (C64Color.colorDictionary.ContainsKey(id))
		{
			return C64Color.colorDictionary[id];
		}
		MainControl.logError("Could not find color with ID: " + id);
		return null;
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00016C68 File Offset: 0x00014E68
	public static Color32 SmallTextColor
	{
		get
		{
			string smallDescriptionFontColor = GlobalSettings.getFontSettings().getSmallDescriptionFontColor();
			if (smallDescriptionFontColor == "")
			{
				return C64Color.Transparent;
			}
			return C64Color.getColorFromDictionary(smallDescriptionFontColor).getColor();
		}
	}

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x060004B7 RID: 1207 RVA: 0x00016CA0 File Offset: 0x00014EA0
	public static Color32 SmallTextQuoteColor
	{
		get
		{
			string smallDescriptionQuoteFontColor = GlobalSettings.getFontSettings().getSmallDescriptionQuoteFontColor();
			if (smallDescriptionQuoteFontColor == "")
			{
				return C64Color.Transparent;
			}
			return C64Color.getColorFromDictionary(smallDescriptionQuoteFontColor).getColor();
		}
	}

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x060004B8 RID: 1208 RVA: 0x00016CD8 File Offset: 0x00014ED8
	public static Color32 SmallTextShadowColor
	{
		get
		{
			string smallDescriptionFontShadowColor = GlobalSettings.getFontSettings().getSmallDescriptionFontShadowColor();
			if (smallDescriptionFontShadowColor == "")
			{
				return C64Color.Transparent;
			}
			return C64Color.getColorFromDictionary(smallDescriptionFontShadowColor).getColor();
		}
	}

	// Token: 0x17000007 RID: 7
	// (get) Token: 0x060004B9 RID: 1209 RVA: 0x00016D0E File Offset: 0x00014F0E
	public static Color32 SmallTextShadowColorDarkBackground
	{
		get
		{
			return C64Color.Black;
		}
	}

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x060004BA RID: 1210 RVA: 0x00016D15 File Offset: 0x00014F15
	public static Color32 HighlightedSmallTextShadowColor
	{
		get
		{
			return C64Color.Black;
		}
	}

	// Token: 0x17000009 RID: 9
	// (get) Token: 0x060004BB RID: 1211 RVA: 0x00016D1C File Offset: 0x00014F1C
	public static Color32 HeaderColor
	{
		get
		{
			return C64Color.Cyan;
		}
	}

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x060004BC RID: 1212 RVA: 0x00016D23 File Offset: 0x00014F23
	public static Color32 Transparent
	{
		get
		{
			return C64Color.transparent;
		}
	}

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x060004BD RID: 1213 RVA: 0x00016D2A File Offset: 0x00014F2A
	public static Color32 Black
	{
		get
		{
			return C64Color.BLACK.getColor();
		}
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x060004BE RID: 1214 RVA: 0x00016D36 File Offset: 0x00014F36
	public static string BLACK_TAG
	{
		get
		{
			return C64Color.BLACK.getColorTag();
		}
	}

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x060004BF RID: 1215 RVA: 0x00016D42 File Offset: 0x00014F42
	public static Color32 White
	{
		get
		{
			return C64Color.WHITE.getColor();
		}
	}

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x060004C0 RID: 1216 RVA: 0x00016D4E File Offset: 0x00014F4E
	public static string WHITE_TAG
	{
		get
		{
			return C64Color.WHITE.getColorTag();
		}
	}

	// Token: 0x1700000F RID: 15
	// (get) Token: 0x060004C1 RID: 1217 RVA: 0x00016D5A File Offset: 0x00014F5A
	public static Color32 GrayDark
	{
		get
		{
			return C64Color.GRAY_DARK.getColor();
		}
	}

	// Token: 0x17000010 RID: 16
	// (get) Token: 0x060004C2 RID: 1218 RVA: 0x00016D66 File Offset: 0x00014F66
	public static string GRAY_DARK_TAG
	{
		get
		{
			return C64Color.GRAY_DARK.getColorTag();
		}
	}

	// Token: 0x17000011 RID: 17
	// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00016D72 File Offset: 0x00014F72
	public static Color32 Gray
	{
		get
		{
			return C64Color.GRAY.getColor();
		}
	}

	// Token: 0x17000012 RID: 18
	// (get) Token: 0x060004C4 RID: 1220 RVA: 0x00016D7E File Offset: 0x00014F7E
	public static string GRAY_TAG
	{
		get
		{
			return C64Color.GRAY.getColorTag();
		}
	}

	// Token: 0x17000013 RID: 19
	// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00016D8A File Offset: 0x00014F8A
	public static Color32 GrayLight
	{
		get
		{
			return C64Color.GRAY_LIGHT.getColor();
		}
	}

	// Token: 0x17000014 RID: 20
	// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00016D96 File Offset: 0x00014F96
	public static string GRAY_LIGHT_TAG
	{
		get
		{
			return C64Color.GRAY_LIGHT.getColorTag();
		}
	}

	// Token: 0x17000015 RID: 21
	// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00016DA2 File Offset: 0x00014FA2
	public static Color32 Red
	{
		get
		{
			return C64Color.RED.getColor();
		}
	}

	// Token: 0x17000016 RID: 22
	// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00016DAE File Offset: 0x00014FAE
	public static string RED_TAG
	{
		get
		{
			return C64Color.RED.getColorTag();
		}
	}

	// Token: 0x17000017 RID: 23
	// (get) Token: 0x060004C9 RID: 1225 RVA: 0x00016DBA File Offset: 0x00014FBA
	public static Color32 RedLight
	{
		get
		{
			return C64Color.RED_LIGHT.getColor();
		}
	}

	// Token: 0x17000018 RID: 24
	// (get) Token: 0x060004CA RID: 1226 RVA: 0x00016DC6 File Offset: 0x00014FC6
	public static string RED_LIGHT_TAG
	{
		get
		{
			return C64Color.RED_LIGHT.getColorTag();
		}
	}

	// Token: 0x17000019 RID: 25
	// (get) Token: 0x060004CB RID: 1227 RVA: 0x00016DD2 File Offset: 0x00014FD2
	public static Color32 Brown
	{
		get
		{
			return C64Color.BROWN.getColor();
		}
	}

	// Token: 0x1700001A RID: 26
	// (get) Token: 0x060004CC RID: 1228 RVA: 0x00016DDE File Offset: 0x00014FDE
	public static string BROWN_TAG
	{
		get
		{
			return C64Color.BROWN.getColorTag();
		}
	}

	// Token: 0x1700001B RID: 27
	// (get) Token: 0x060004CD RID: 1229 RVA: 0x00016DEA File Offset: 0x00014FEA
	public static Color32 BrownLight
	{
		get
		{
			return C64Color.BROWN_LIGHT.getColor();
		}
	}

	// Token: 0x1700001C RID: 28
	// (get) Token: 0x060004CE RID: 1230 RVA: 0x00016DF6 File Offset: 0x00014FF6
	public static string BROWN_LIGHT_TAG
	{
		get
		{
			return C64Color.BROWN_LIGHT.getColorTag();
		}
	}

	// Token: 0x1700001D RID: 29
	// (get) Token: 0x060004CF RID: 1231 RVA: 0x00016E02 File Offset: 0x00015002
	public static Color32 Yellow
	{
		get
		{
			return C64Color.YELLOW.getColor();
		}
	}

	// Token: 0x1700001E RID: 30
	// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00016E0E File Offset: 0x0001500E
	public static string YELLOW_TAG
	{
		get
		{
			return C64Color.YELLOW.getColorTag();
		}
	}

	// Token: 0x1700001F RID: 31
	// (get) Token: 0x060004D1 RID: 1233 RVA: 0x00016E1A File Offset: 0x0001501A
	public static Color32 Green
	{
		get
		{
			return C64Color.GREEN.getColor();
		}
	}

	// Token: 0x17000020 RID: 32
	// (get) Token: 0x060004D2 RID: 1234 RVA: 0x00016E26 File Offset: 0x00015026
	public static string GREEN_TAG
	{
		get
		{
			return C64Color.GREEN.getColorTag();
		}
	}

	// Token: 0x17000021 RID: 33
	// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00016E32 File Offset: 0x00015032
	public static Color32 GreenLight
	{
		get
		{
			return C64Color.GREEN_LIGHT.getColor();
		}
	}

	// Token: 0x17000022 RID: 34
	// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00016E3E File Offset: 0x0001503E
	public static string GREEN_LIGHT_TAG
	{
		get
		{
			return C64Color.GREEN_LIGHT.getColorTag();
		}
	}

	// Token: 0x17000023 RID: 35
	// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00016E4A File Offset: 0x0001504A
	public static Color32 Cyan
	{
		get
		{
			return C64Color.CYAN.getColor();
		}
	}

	// Token: 0x17000024 RID: 36
	// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00016E56 File Offset: 0x00015056
	public static string CYAN_TAG
	{
		get
		{
			return C64Color.CYAN.getColorTag();
		}
	}

	// Token: 0x17000025 RID: 37
	// (get) Token: 0x060004D7 RID: 1239 RVA: 0x00016E62 File Offset: 0x00015062
	public static Color32 BlueLight
	{
		get
		{
			return C64Color.BLUE_LIGHT.getColor();
		}
	}

	// Token: 0x17000026 RID: 38
	// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00016E6E File Offset: 0x0001506E
	public static string BLUE_LIGHT_TAG
	{
		get
		{
			return C64Color.BLUE_LIGHT.getColorTag();
		}
	}

	// Token: 0x17000027 RID: 39
	// (get) Token: 0x060004D9 RID: 1241 RVA: 0x00016E7A File Offset: 0x0001507A
	public static Color32 Blue
	{
		get
		{
			return C64Color.BLUE.getColor();
		}
	}

	// Token: 0x17000028 RID: 40
	// (get) Token: 0x060004DA RID: 1242 RVA: 0x00016E86 File Offset: 0x00015086
	public static string BlueTag
	{
		get
		{
			return C64Color.BLUE.getColorTag();
		}
	}

	// Token: 0x17000029 RID: 41
	// (get) Token: 0x060004DB RID: 1243 RVA: 0x00016E92 File Offset: 0x00015092
	public static Color32 Violet
	{
		get
		{
			return C64Color.VIOLET.getColor();
		}
	}

	// Token: 0x1700002A RID: 42
	// (get) Token: 0x060004DC RID: 1244 RVA: 0x00016E9E File Offset: 0x0001509E
	public static string VIOLET_TAG
	{
		get
		{
			return C64Color.VIOLET.getColorTag();
		}
	}

	// Token: 0x1700002B RID: 43
	// (get) Token: 0x060004DD RID: 1245 RVA: 0x00016EAA File Offset: 0x000150AA
	public static string SHEET_TAG
	{
		get
		{
			return C64Color.GRAY_LIGHT_TAG;
		}
	}

	// Token: 0x1700002C RID: 44
	// (get) Token: 0x060004DE RID: 1246 RVA: 0x00016EB1 File Offset: 0x000150B1
	public static string HEADER_TAG
	{
		get
		{
			return "<" + C64Color.HEADER_TAG_CONTENT + ">";
		}
	}

	// Token: 0x1700002D RID: 45
	// (get) Token: 0x060004DF RID: 1247 RVA: 0x00016EC7 File Offset: 0x000150C7
	public static string HEADER_CLOSING_TAG
	{
		get
		{
			return "</" + C64Color.HEADER_TAG_CONTENT + ">";
		}
	}

	// Token: 0x1700002E RID: 46
	// (get) Token: 0x060004E0 RID: 1248 RVA: 0x00016EDD File Offset: 0x000150DD
	public static string HEADER_TAG_CONTENT
	{
		get
		{
			return "Header";
		}
	}

	// Token: 0x1700002F RID: 47
	// (get) Token: 0x060004E1 RID: 1249 RVA: 0x00016EE4 File Offset: 0x000150E4
	public static string PC_TAG
	{
		get
		{
			return C64Color.YELLOW_TAG;
		}
	}

	// Token: 0x17000030 RID: 48
	// (get) Token: 0x060004E2 RID: 1250 RVA: 0x00016EEB File Offset: 0x000150EB
	public static string ENEMY_TAG
	{
		get
		{
			return C64Color.RED_LIGHT_TAG;
		}
	}

	// Token: 0x17000031 RID: 49
	// (get) Token: 0x060004E3 RID: 1251 RVA: 0x00016EF2 File Offset: 0x000150F2
	public static string ATTRIBUTE_VALUE_TAG
	{
		get
		{
			return C64Color.WHITE_TAG;
		}
	}

	// Token: 0x17000032 RID: 50
	// (get) Token: 0x060004E4 RID: 1252 RVA: 0x00016EF9 File Offset: 0x000150F9
	public static string ATTRIBUTE_NAME_TAG
	{
		get
		{
			return C64Color.GRAY_LIGHT_TAG;
		}
	}

	// Token: 0x060004E5 RID: 1253 RVA: 0x00016F00 File Offset: 0x00015100
	public static Color32[] getColorArray()
	{
		return C64Color.colorList;
	}

	// Token: 0x060004E6 RID: 1254 RVA: 0x00016F07 File Offset: 0x00015107
	public static List<Color32> getMarkerColors()
	{
		return C64Color.markerColors;
	}

	// Token: 0x060004E7 RID: 1255 RVA: 0x00016F10 File Offset: 0x00015110
	public static Dictionary<Color32, Color32> getSwapDictionary(string skinColorId, string hairColorId, string mainColorId, string secondaryColorId, string tertiaryColorId, string weaponDetailColorId, string weaponMetalColorId, string armorDetailColorId, string armorColorColorId)
	{
		Dictionary<Color32, Color32> dictionary = new Dictionary<Color32, Color32>();
		C64Color.addToSwapDictionary(dictionary, C64Color.SWAP_SKIN.getColor(), C64Color.SWAP_SKIN_SHADE.getColor(), skinColorId);
		C64Color.addToSwapDictionary(dictionary, C64Color.SWAP_HAIR.getColor(), C64Color.SWAP_HAIR_SHADE.getColor(), hairColorId);
		C64Color.addToSwapDictionary(dictionary, C64Color.SWAP_MAIN.getColor(), C64Color.SWAP_MAIN_SHADE.getColor(), mainColorId);
		C64Color.addToSwapDictionary(dictionary, C64Color.SWAP_SECONDARY.getColor(), C64Color.SWAP_SECONDARY_SHADE.getColor(), secondaryColorId);
		C64Color.addToSwapDictionary(dictionary, C64Color.SWAP_TERTIARY.getColor(), C64Color.SWAP_TERTIARY_SHADE.getColor(), tertiaryColorId);
		C64Color.addToSwapDictionary(dictionary, C64Color.SWAP_WEAPON_DETAIL.getColor(), C64Color.SWAP_WEAPON_DETAIL_SHADE.getColor(), weaponDetailColorId);
		C64Color.addToSwapDictionary(dictionary, C64Color.SWAP_WEAPON_COLOR.getColor(), C64Color.SWAP_WEAPON_COLOR_SHADE.getColor(), weaponMetalColorId);
		C64Color.addToSwapDictionary(dictionary, C64Color.SWAP_ARMOR_DETAIL.getColor(), C64Color.SWAP_ARMOR_DETAIL_SHADE.getColor(), armorDetailColorId);
		C64Color.addToSwapDictionary(dictionary, C64Color.SWAP_ARMOR_COLOR.getColor(), C64Color.SWAP_ARMOR_COLOR_SHADE.getColor(), armorColorColorId);
		return dictionary;
	}

	// Token: 0x060004E8 RID: 1256 RVA: 0x0001701C File Offset: 0x0001521C
	private static void addToSwapDictionary(Dictionary<Color32, Color32> swapDictionary, Color32 swapColor, Color32 swapColorShade, string colorId)
	{
		if (colorId == "")
		{
			return;
		}
		C64Color.ColorData colorFromDictionary = C64Color.getColorFromDictionary(colorId);
		if (colorFromDictionary == null)
		{
			return;
		}
		C64Color.ColorData colorFromDictionary2 = C64Color.getColorFromDictionary(colorFromDictionary.getShadeId());
		swapDictionary.Add(swapColor, colorFromDictionary.getColor());
		if (colorFromDictionary2 != null)
		{
			swapDictionary.Add(swapColorShade, colorFromDictionary2.getColor());
			return;
		}
		swapDictionary.Add(swapColorShade, colorFromDictionary.getColor());
	}

	// Token: 0x04000107 RID: 263
	private static C64Color.ColorData BLACK;

	// Token: 0x04000108 RID: 264
	private static C64Color.ColorData WHITE;

	// Token: 0x04000109 RID: 265
	private static C64Color.ColorData GRAY_DARK;

	// Token: 0x0400010A RID: 266
	private static C64Color.ColorData GRAY;

	// Token: 0x0400010B RID: 267
	private static C64Color.ColorData GRAY_LIGHT;

	// Token: 0x0400010C RID: 268
	private static C64Color.ColorData RED;

	// Token: 0x0400010D RID: 269
	private static C64Color.ColorData RED_LIGHT;

	// Token: 0x0400010E RID: 270
	private static C64Color.ColorData BROWN;

	// Token: 0x0400010F RID: 271
	private static C64Color.ColorData BROWN_LIGHT;

	// Token: 0x04000110 RID: 272
	private static C64Color.ColorData YELLOW;

	// Token: 0x04000111 RID: 273
	private static C64Color.ColorData GREEN;

	// Token: 0x04000112 RID: 274
	private static C64Color.ColorData GREEN_LIGHT;

	// Token: 0x04000113 RID: 275
	private static C64Color.ColorData CYAN;

	// Token: 0x04000114 RID: 276
	private static C64Color.ColorData BLUE;

	// Token: 0x04000115 RID: 277
	private static C64Color.ColorData BLUE_LIGHT;

	// Token: 0x04000116 RID: 278
	private static C64Color.ColorData VIOLET;

	// Token: 0x04000117 RID: 279
	private static C64Color.ColorData SWAP_SKIN = new C64Color.ColorData("ff0000");

	// Token: 0x04000118 RID: 280
	private static C64Color.ColorData SWAP_SKIN_SHADE = new C64Color.ColorData("ff00ff");

	// Token: 0x04000119 RID: 281
	private static C64Color.ColorData SWAP_HAIR = new C64Color.ColorData("ffff00");

	// Token: 0x0400011A RID: 282
	private static C64Color.ColorData SWAP_HAIR_SHADE = new C64Color.ColorData("AAAA00");

	// Token: 0x0400011B RID: 283
	private static C64Color.ColorData SWAP_MAIN = new C64Color.ColorData("00ffff");

	// Token: 0x0400011C RID: 284
	private static C64Color.ColorData SWAP_MAIN_SHADE = new C64Color.ColorData("0000ff");

	// Token: 0x0400011D RID: 285
	private static C64Color.ColorData SWAP_SECONDARY = new C64Color.ColorData("00ff00");

	// Token: 0x0400011E RID: 286
	private static C64Color.ColorData SWAP_SECONDARY_SHADE = new C64Color.ColorData("00aaaa");

	// Token: 0x0400011F RID: 287
	private static C64Color.ColorData SWAP_TERTIARY = new C64Color.ColorData("AA00AA");

	// Token: 0x04000120 RID: 288
	private static C64Color.ColorData SWAP_TERTIARY_SHADE = new C64Color.ColorData("FFAA00");

	// Token: 0x04000121 RID: 289
	private static C64Color.ColorData SWAP_WEAPON_DETAIL = new C64Color.ColorData("FF8C8C");

	// Token: 0x04000122 RID: 290
	private static C64Color.ColorData SWAP_WEAPON_DETAIL_SHADE = new C64Color.ColorData("E25A5A");

	// Token: 0x04000123 RID: 291
	private static C64Color.ColorData SWAP_WEAPON_COLOR = new C64Color.ColorData("9EFFED");

	// Token: 0x04000124 RID: 292
	private static C64Color.ColorData SWAP_WEAPON_COLOR_SHADE = new C64Color.ColorData("5AB5A4");

	// Token: 0x04000125 RID: 293
	private static C64Color.ColorData SWAP_ARMOR_DETAIL = new C64Color.ColorData("B2AE7C");

	// Token: 0x04000126 RID: 294
	private static C64Color.ColorData SWAP_ARMOR_DETAIL_SHADE = new C64Color.ColorData("71773B");

	// Token: 0x04000127 RID: 295
	private static C64Color.ColorData SWAP_ARMOR_COLOR = new C64Color.ColorData("713E3B");

	// Token: 0x04000128 RID: 296
	private static C64Color.ColorData SWAP_ARMOR_COLOR_SHADE = new C64Color.ColorData("542E2C");

	// Token: 0x04000129 RID: 297
	private static List<Color32> markerColors = new List<Color32>
	{
		C64Color.SWAP_SKIN.getColor(),
		C64Color.SWAP_SKIN_SHADE.getColor(),
		C64Color.SWAP_HAIR.getColor(),
		C64Color.SWAP_HAIR_SHADE.getColor(),
		C64Color.SWAP_MAIN.getColor(),
		C64Color.SWAP_MAIN_SHADE.getColor(),
		C64Color.SWAP_SECONDARY.getColor(),
		C64Color.SWAP_SECONDARY_SHADE.getColor(),
		C64Color.SWAP_TERTIARY.getColor(),
		C64Color.SWAP_TERTIARY_SHADE.getColor()
	};

	// Token: 0x0400012A RID: 298
	private static Color32[] colorList;

	// Token: 0x0400012B RID: 299
	private static List<string> skinColorIds;

	// Token: 0x0400012C RID: 300
	private static List<string> skinColorForNPCIds;

	// Token: 0x0400012D RID: 301
	private static List<string> hairColorIds;

	// Token: 0x0400012E RID: 302
	private static List<string> clothingColorIds;

	// Token: 0x0400012F RID: 303
	private static List<string> fontColorIds;

	// Token: 0x04000130 RID: 304
	private static List<string> fontShadowColorIds;

	// Token: 0x04000131 RID: 305
	private static Dictionary<string, C64Color.ColorData> colorDictionary;

	// Token: 0x04000132 RID: 306
	private static Color32 transparent = Color.clear;

	// Token: 0x020001DB RID: 475
	public enum ColorIds
	{
		// Token: 0x0400074B RID: 1867
		NULL,
		// Token: 0x0400074C RID: 1868
		COL_Black,
		// Token: 0x0400074D RID: 1869
		COL_White,
		// Token: 0x0400074E RID: 1870
		COL_GrayDark,
		// Token: 0x0400074F RID: 1871
		COL_Gray,
		// Token: 0x04000750 RID: 1872
		COL_GrayLight,
		// Token: 0x04000751 RID: 1873
		COL_Red,
		// Token: 0x04000752 RID: 1874
		COL_RedLight,
		// Token: 0x04000753 RID: 1875
		COL_Blue,
		// Token: 0x04000754 RID: 1876
		COL_BlueLight,
		// Token: 0x04000755 RID: 1877
		COL_Cyan,
		// Token: 0x04000756 RID: 1878
		COL_Yellow,
		// Token: 0x04000757 RID: 1879
		COL_Green,
		// Token: 0x04000758 RID: 1880
		COL_GreenLight,
		// Token: 0x04000759 RID: 1881
		COL_Violet,
		// Token: 0x0400075A RID: 1882
		COL_Brown,
		// Token: 0x0400075B RID: 1883
		COL_BrownLight
	}

	// Token: 0x020001DC RID: 476
	private class ColorData
	{
		// Token: 0x06001705 RID: 5893 RVA: 0x00066AE8 File Offset: 0x00064CE8
		public ColorData(C64Color.ColorIds colorId, string backupHex)
		{
			SKALDProjectData.UIContainers.ColorContainer.Color colorData = GameData.getColorData(colorId);
			if (colorData == null)
			{
				MainControl.logError("Missing object for color: " + backupHex);
				this.setHexCode(backupHex);
				return;
			}
			if (colorData.hexCode == "" || colorData.id == "")
			{
				MainControl.logError("MIssing ID or Hexcode for color: " + colorData.id);
				this.setHexCode(backupHex);
				return;
			}
			if (colorData.hexCode != backupHex)
			{
				MainControl.logWarning(string.Concat(new string[]
				{
					"Potential mismatched hexcode for color: ",
					colorData.id,
					" - ",
					colorData.hexCode,
					" vs ",
					backupHex
				}));
			}
			this.id = colorData.id;
			this.shadeId = colorData.shade;
			this.highlightId = colorData.highlight;
			this.skinColor = colorData.skin;
			this.skinColorForNpc = colorData.skinNotPC;
			this.hairColor = colorData.hair;
			this.clothingColor = colorData.clothing;
			this.fontColor = colorData.font;
			this.fontShadowColor = colorData.fontShadow;
			this.setHexCode(colorData.hexCode);
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x00066C38 File Offset: 0x00064E38
		public ColorData(string hexCode)
		{
			this.setHexCode(hexCode);
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x00066C60 File Offset: 0x00064E60
		private void setHexCode(string hexCode)
		{
			if (!hexCode.Contains("#"))
			{
				hexCode = "#" + hexCode;
			}
			this.colorTag = "<color=" + hexCode + ">";
			this.color = C64Color.ColorData.getColorFromHexCode(hexCode);
			MainControl.log("Set color: " + this.id + " - " + hexCode);
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x00066CC4 File Offset: 0x00064EC4
		public string getShadeId()
		{
			return this.shadeId;
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x00066CCC File Offset: 0x00064ECC
		public string getHighlightId()
		{
			return this.highlightId;
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x00066CD4 File Offset: 0x00064ED4
		public Color32 getColor()
		{
			return this.color;
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x00066CDC File Offset: 0x00064EDC
		public string getColorTag()
		{
			return this.colorTag;
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x00066CE4 File Offset: 0x00064EE4
		private static Color32 getColorFromHexCode(string hexCode)
		{
			Color c;
			if (ColorUtility.TryParseHtmlString(hexCode, out c))
			{
				return c;
			}
			MainControl.logError("ERROR! Invalid hex color code: " + hexCode);
			return new Color32(0, byte.MaxValue, byte.MaxValue, byte.MaxValue);
		}

		// Token: 0x0400075C RID: 1884
		public bool skinColor;

		// Token: 0x0400075D RID: 1885
		public bool skinColorForNpc;

		// Token: 0x0400075E RID: 1886
		public bool hairColor;

		// Token: 0x0400075F RID: 1887
		public bool clothingColor;

		// Token: 0x04000760 RID: 1888
		public bool fontColor;

		// Token: 0x04000761 RID: 1889
		public bool fontShadowColor;

		// Token: 0x04000762 RID: 1890
		private string id;

		// Token: 0x04000763 RID: 1891
		private string hexCode;

		// Token: 0x04000764 RID: 1892
		private string colorTag;

		// Token: 0x04000765 RID: 1893
		private string shadeId = "";

		// Token: 0x04000766 RID: 1894
		private string highlightId = "";

		// Token: 0x04000767 RID: 1895
		private Color32 color;
	}
}
