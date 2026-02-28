using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000182 RID: 386
public static class StringPrinter
{
	// Token: 0x06001460 RID: 5216 RVA: 0x0005A89C File Offset: 0x00058A9C
	private static Dictionary<char, int> getCharacterChart()
	{
		if (StringPrinter.characterDictionary == null)
		{
			StringPrinter.characterDictionary = new Dictionary<char, int>();
			StringPrinter.characterDictionary.Add('A', 0);
			StringPrinter.characterDictionary.Add('B', 1);
			StringPrinter.characterDictionary.Add('C', 2);
			StringPrinter.characterDictionary.Add('D', 3);
			StringPrinter.characterDictionary.Add('E', 4);
			StringPrinter.characterDictionary.Add('F', 5);
			StringPrinter.characterDictionary.Add('G', 6);
			StringPrinter.characterDictionary.Add('H', 7);
			StringPrinter.characterDictionary.Add('I', 8);
			StringPrinter.characterDictionary.Add('J', 9);
			StringPrinter.characterDictionary.Add('K', 10);
			StringPrinter.characterDictionary.Add('L', 11);
			StringPrinter.characterDictionary.Add('M', 12);
			StringPrinter.characterDictionary.Add('N', 13);
			StringPrinter.characterDictionary.Add('O', 14);
			StringPrinter.characterDictionary.Add('P', 15);
			StringPrinter.characterDictionary.Add('Q', 16);
			StringPrinter.characterDictionary.Add('R', 17);
			StringPrinter.characterDictionary.Add('S', 18);
			StringPrinter.characterDictionary.Add('T', 19);
			StringPrinter.characterDictionary.Add('U', 20);
			StringPrinter.characterDictionary.Add('V', 21);
			StringPrinter.characterDictionary.Add('W', 22);
			StringPrinter.characterDictionary.Add('X', 23);
			StringPrinter.characterDictionary.Add('Y', 24);
			StringPrinter.characterDictionary.Add('Z', 25);
			StringPrinter.characterDictionary.Add(' ', 26);
			StringPrinter.characterDictionary.Add('a', 27);
			StringPrinter.characterDictionary.Add('b', 28);
			StringPrinter.characterDictionary.Add('c', 29);
			StringPrinter.characterDictionary.Add('d', 30);
			StringPrinter.characterDictionary.Add('e', 31);
			StringPrinter.characterDictionary.Add('f', 32);
			StringPrinter.characterDictionary.Add('g', 33);
			StringPrinter.characterDictionary.Add('h', 34);
			StringPrinter.characterDictionary.Add('i', 35);
			StringPrinter.characterDictionary.Add('j', 36);
			StringPrinter.characterDictionary.Add('k', 37);
			StringPrinter.characterDictionary.Add('l', 38);
			StringPrinter.characterDictionary.Add('m', 39);
			StringPrinter.characterDictionary.Add('n', 40);
			StringPrinter.characterDictionary.Add('o', 41);
			StringPrinter.characterDictionary.Add('p', 42);
			StringPrinter.characterDictionary.Add('q', 43);
			StringPrinter.characterDictionary.Add('r', 44);
			StringPrinter.characterDictionary.Add('s', 45);
			StringPrinter.characterDictionary.Add('t', 46);
			StringPrinter.characterDictionary.Add('u', 47);
			StringPrinter.characterDictionary.Add('v', 48);
			StringPrinter.characterDictionary.Add('w', 49);
			StringPrinter.characterDictionary.Add('x', 50);
			StringPrinter.characterDictionary.Add('y', 51);
			StringPrinter.characterDictionary.Add('z', 52);
			StringPrinter.characterDictionary.Add('!', 53);
			StringPrinter.characterDictionary.Add('0', 54);
			StringPrinter.characterDictionary.Add('1', 55);
			StringPrinter.characterDictionary.Add('2', 56);
			StringPrinter.characterDictionary.Add('3', 57);
			StringPrinter.characterDictionary.Add('4', 58);
			StringPrinter.characterDictionary.Add('5', 59);
			StringPrinter.characterDictionary.Add('6', 60);
			StringPrinter.characterDictionary.Add('7', 61);
			StringPrinter.characterDictionary.Add('8', 62);
			StringPrinter.characterDictionary.Add('9', 63);
			StringPrinter.characterDictionary.Add('.', 64);
			StringPrinter.characterDictionary.Add(',', 65);
			StringPrinter.characterDictionary.Add(':', 66);
			StringPrinter.characterDictionary.Add('/', 67);
			StringPrinter.characterDictionary.Add('%', 68);
			StringPrinter.characterDictionary.Add('-', 69);
			StringPrinter.characterDictionary.Add('+', 70);
			StringPrinter.characterDictionary.Add('?', 71);
			StringPrinter.characterDictionary.Add('\'', 72);
			StringPrinter.characterDictionary.Add('’', 72);
			StringPrinter.characterDictionary.Add('‘', 72);
			StringPrinter.characterDictionary.Add('"', 73);
			StringPrinter.characterDictionary.Add('(', 74);
			StringPrinter.characterDictionary.Add(')', 75);
			StringPrinter.characterDictionary.Add('=', 76);
			StringPrinter.characterDictionary.Add('*', 77);
			StringPrinter.characterDictionary.Add('_', 78);
			StringPrinter.characterDictionary.Add('[', 79);
			StringPrinter.characterDictionary.Add(']', 80);
			StringPrinter.characterDictionary.Add('#', 81);
			StringPrinter.characterDictionary.Add('<', 82);
			StringPrinter.characterDictionary.Add('>', 83);
			StringPrinter.characterDictionary.Add(';', 84);
			StringPrinter.characterDictionary.Add('{', 85);
			StringPrinter.characterDictionary.Add('}', 86);
			StringPrinter.characterDictionary.Add('|', 87);
			StringPrinter.characterDictionary.Add('~', 88);
		}
		return StringPrinter.characterDictionary;
	}

	// Token: 0x06001461 RID: 5217 RVA: 0x0005ADB9 File Offset: 0x00058FB9
	public static int getSubimageForChar(char c)
	{
		if (StringPrinter.getCharacterChart().ContainsKey(c))
		{
			return StringPrinter.getCharacterChart()[c];
		}
		return 26;
	}

	// Token: 0x06001462 RID: 5218 RVA: 0x0005ADD8 File Offset: 0x00058FD8
	public static TextureTools.TextureData bakeFancyString(string s, Color32 textColor, Color32 shadowColor)
	{
		Font tinyFont = FontContainer.getTinyFont();
		TextureTools.TextureData textureData = StringPrinter.bakeString(s, textColor, tinyFont);
		Color32 black = C64Color.Black;
		TextureTools.TextureData textureData2 = new TextureTools.TextureData(textureData.width + 2, textureData.height + 2);
		textureData2.SetPixels(1, 1, textureData.width, textureData.height, textureData.colors);
		TextureTools.applyOverlay(textureData2, textureData.getOutline(black));
		return textureData2;
	}

	// Token: 0x06001463 RID: 5219 RVA: 0x0005AE38 File Offset: 0x00059038
	public static void buildGridIcon(TextureTools.TextureData target, int yOffset, string input, Color32 textColor, Color32 highlightColor)
	{
		string[] array = input.Split(new char[]
		{
			' '
		});
		if (array.Length > 1)
		{
			StringPrinter.buildIcon(target, yOffset + 7, array[0], textColor, highlightColor);
			StringPrinter.buildIcon(target, yOffset, array[array.Length - 1], textColor, highlightColor);
			return;
		}
		StringPrinter.buildIcon(target, yOffset + 3, array[0], textColor, highlightColor);
	}

	// Token: 0x06001464 RID: 5220 RVA: 0x0005AE90 File Offset: 0x00059090
	private static void buildIcon(TextureTools.TextureData target, int y, string input, Color32 textColor, Color32 highlightColor)
	{
		if (input == null || input == "")
		{
			return;
		}
		string text = input[0].ToString().ToUpper();
		if (input.Length > 1)
		{
			text += input[1].ToString();
		}
		TextureTools.TextureData textureData = StringPrinter.bakeFancyString(text, textColor, C64Color.GrayDark);
		TextureTools.TextureData textureData2 = new TextureTools.TextureData(textureData.width, 4, textureData.GetPixels(0, 0, textureData.width, 4));
		textureData2.clearToColorIfNotBlack(highlightColor);
		TextureTools.applyOverlay(textureData, textureData2, 0, 0);
		int anchorX = 2;
		if (textureData.width < target.width)
		{
			anchorX = Mathf.RoundToInt((float)(target.width - textureData.width) / 2f);
		}
		TextureTools.applyOverlay(target, textureData, anchorX, y);
	}

	// Token: 0x06001465 RID: 5221 RVA: 0x0005AF54 File Offset: 0x00059154
	public static TextureTools.TextureData bakeString(string s, Color32 textColor, Font font)
	{
		List<TextureTools.TextureData> list = new List<TextureTools.TextureData>();
		int num = 0;
		for (int i = 0; i < s.Length; i++)
		{
			TextureTools.TextureData letterSubImageTextureData = TextureTools.getLetterSubImageTextureData(StringPrinter.getSubimageForChar(s[i]), font.getModelPath());
			if (letterSubImageTextureData != null)
			{
				list.Add(letterSubImageTextureData);
				num += letterSubImageTextureData.width + StringPrinter.spacing;
			}
		}
		TextureTools.TextureData textureData = new TextureTools.TextureData(num, font.wordHeight);
		int num2 = 0;
		foreach (TextureTools.TextureData textureData2 in list)
		{
			TextureTools.applyOverlayColor(textureData, textureData2, num2, 0, textColor);
			num2 += textureData2.width + StringPrinter.spacing;
		}
		return textureData;
	}

	// Token: 0x06001466 RID: 5222 RVA: 0x0005B01C File Offset: 0x0005921C
	public static TextureTools.TextureData applyShadow(TextureTools.TextureData input, Color32 color)
	{
		TextureTools.TextureData textureData = input.createCopy();
		textureData.clearToColorIfNotBlack(color);
		input.SetPixels(0, 0, input.width, 1, textureData.GetPixels(0, 0, input.width, 1));
		return input;
	}

	// Token: 0x04000533 RID: 1331
	private static int spacing = 1;

	// Token: 0x04000534 RID: 1332
	private static Dictionary<char, int> characterDictionary;
}
