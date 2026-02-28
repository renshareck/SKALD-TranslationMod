using System;

// Token: 0x02000039 RID: 57
public static class FontContainer
{
	// Token: 0x06000739 RID: 1849 RVA: 0x0001FC7A File Offset: 0x0001DE7A
	private static string getMediumFontID()
	{
		return FontContainer.mediumFontID;
	}

	// Token: 0x0600073A RID: 1850 RVA: 0x0001FC81 File Offset: 0x0001DE81
	private static string getMediumFontBlueID()
	{
		return FontContainer.mediumFontBlueID;
	}

	// Token: 0x0600073B RID: 1851 RVA: 0x0001FC88 File Offset: 0x0001DE88
	private static string getIlluminatedFontID()
	{
		return FontContainer.illuminatedFontID;
	}

	// Token: 0x0600073C RID: 1852 RVA: 0x0001FC8F File Offset: 0x0001DE8F
	private static string getBigFontID()
	{
		return FontContainer.bigFontID;
	}

	// Token: 0x0600073D RID: 1853 RVA: 0x0001FC96 File Offset: 0x0001DE96
	private static string getConsoleFontID()
	{
		return FontContainer.consoleFontID;
	}

	// Token: 0x0600073E RID: 1854 RVA: 0x0001FC9D File Offset: 0x0001DE9D
	private static string getTinyCapitalizedFontID()
	{
		return FontContainer.tinyCapitalizedFontID;
	}

	// Token: 0x0600073F RID: 1855 RVA: 0x0001FCA4 File Offset: 0x0001DEA4
	public static Font getSmallDescriptionFont()
	{
		Font font = GlobalSettings.getFontSettings().getSmallDescriptionFont();
		if (font == null)
		{
			font = GameData.getFont("FON_TinyFont");
		}
		return font;
	}

	// Token: 0x06000740 RID: 1856 RVA: 0x0001FCCC File Offset: 0x0001DECC
	public static Font getTinyFont()
	{
		Font font = GlobalSettings.getFontSettings().getSmallTechnicalFont();
		if (font == null)
		{
			font = GameData.getFont("FON_TinyFont");
		}
		return font;
	}

	// Token: 0x06000741 RID: 1857 RVA: 0x0001FCF3 File Offset: 0x0001DEF3
	public static Font getTinyCapitalizedFont()
	{
		return GameData.getFont(FontContainer.getTinyCapitalizedFontID());
	}

	// Token: 0x06000742 RID: 1858 RVA: 0x0001FCFF File Offset: 0x0001DEFF
	public static Font getMediumFont()
	{
		return GameData.getFont(FontContainer.getMediumFontID());
	}

	// Token: 0x06000743 RID: 1859 RVA: 0x0001FD0B File Offset: 0x0001DF0B
	public static Font getMediumFontBlue()
	{
		return GameData.getFont(FontContainer.getMediumFontBlueID());
	}

	// Token: 0x06000744 RID: 1860 RVA: 0x0001FD17 File Offset: 0x0001DF17
	public static Font getIlluminatedFont()
	{
		return GameData.getFont(FontContainer.getIlluminatedFontID());
	}

	// Token: 0x06000745 RID: 1861 RVA: 0x0001FD23 File Offset: 0x0001DF23
	public static Font getBigFont()
	{
		return GameData.getFont(FontContainer.getBigFontID());
	}

	// Token: 0x06000746 RID: 1862 RVA: 0x0001FD2F File Offset: 0x0001DF2F
	public static Font getConsoleFont()
	{
		return GameData.getFont(FontContainer.getConsoleFontID());
	}

	// Token: 0x04000169 RID: 361
	private static string tinyCapitalizedFontID = "FON_TinyFontCapitalized";

	// Token: 0x0400016A RID: 362
	private static string consoleFontID = "FON_TinyFontFat";

	// Token: 0x0400016B RID: 363
	private static string mediumFontID = "FON_MediumFont";

	// Token: 0x0400016C RID: 364
	private static string mediumFontBlueID = "FON_MediumFontBlue";

	// Token: 0x0400016D RID: 365
	private static string illuminatedFontID = "FON_IlluminatedFont";

	// Token: 0x0400016E RID: 366
	private static string bigFontID = "FON_BigFont";
}
