using System;

// Token: 0x0200006B RID: 107
public static class SkaldOptionGlyphPrinter
{
	// Token: 0x06000942 RID: 2370 RVA: 0x0002C240 File Offset: 0x0002A440
	private static string printOptionGlyphNumeric(int indexFromZero)
	{
		return (indexFromZero + 1).ToString() + ") ";
	}

	// Token: 0x06000943 RID: 2371 RVA: 0x0002C262 File Offset: 0x0002A462
	public static string printOptionNoGlyphs(int indexFromZero)
	{
		if (!SkaldIO.isControllerConnected())
		{
			return SkaldOptionGlyphPrinter.printOptionGlyphNumeric(indexFromZero);
		}
		return "";
	}

	// Token: 0x06000944 RID: 2372 RVA: 0x0002C277 File Offset: 0x0002A477
	public static string printOptionGlyphBAXY(int indexFromZero)
	{
		return SkaldOptionGlyphPrinter.printOptionsGlyph(indexFromZero, SkaldOptionGlyphPrinter.BAXYLetters);
	}

	// Token: 0x06000945 RID: 2373 RVA: 0x0002C284 File Offset: 0x0002A484
	public static string printOptionGlyphAXBY(int indexFromZero)
	{
		return SkaldOptionGlyphPrinter.printOptionsGlyph(indexFromZero, SkaldOptionGlyphPrinter.AXBYLetters);
	}

	// Token: 0x06000946 RID: 2374 RVA: 0x0002C291 File Offset: 0x0002A491
	public static string printOptionGlyphAXBYButNoNumbers(int indexFromZero)
	{
		return SkaldOptionGlyphPrinter.printOptionsGlyphButNoNumbers(indexFromZero, SkaldOptionGlyphPrinter.AXBYLetters);
	}

	// Token: 0x06000947 RID: 2375 RVA: 0x0002C29E File Offset: 0x0002A49E
	public static string printOptionGlyphABXY(int indexFromZero)
	{
		return SkaldOptionGlyphPrinter.printOptionsGlyph(indexFromZero, SkaldOptionGlyphPrinter.ABXYLetters);
	}

	// Token: 0x06000948 RID: 2376 RVA: 0x0002C2AB File Offset: 0x0002A4AB
	private static string printOptionsGlyph(int indexFromZero, string[] options)
	{
		if (!SkaldIO.isControllerConnected())
		{
			return SkaldOptionGlyphPrinter.printOptionGlyphNumeric(indexFromZero);
		}
		if (indexFromZero < options.Length)
		{
			return options[indexFromZero] + ") ";
		}
		return "";
	}

	// Token: 0x06000949 RID: 2377 RVA: 0x0002C2D4 File Offset: 0x0002A4D4
	private static string printOptionsGlyphButNoNumbers(int indexFromZero, string[] options)
	{
		if (!SkaldIO.isControllerConnected())
		{
			return "";
		}
		if (indexFromZero < options.Length)
		{
			return options[indexFromZero] + ") ";
		}
		return "";
	}

	// Token: 0x04000252 RID: 594
	private static string[] ABXYLetters = new string[]
	{
		C64Color.GREEN_TAG + "A</color>",
		C64Color.RED_TAG + "B</color>",
		C64Color.BLUE_LIGHT_TAG + "X</color>",
		C64Color.YELLOW_TAG + "Y</color>"
	};

	// Token: 0x04000253 RID: 595
	private static string[] BAXYLetters = new string[]
	{
		C64Color.RED_TAG + "B</color>",
		C64Color.GREEN_TAG + "A</color>",
		C64Color.BLUE_LIGHT_TAG + "X</color>",
		C64Color.YELLOW_TAG + "Y</color>"
	};

	// Token: 0x04000254 RID: 596
	private static string[] AXBYLetters = new string[]
	{
		C64Color.GREEN_TAG + "A</color>",
		C64Color.BLUE_LIGHT_TAG + "X</color>",
		C64Color.RED_TAG + "B</color>",
		C64Color.YELLOW_TAG + "Y</color>"
	};
}
