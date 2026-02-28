using System;
using System.Collections.Generic;

// Token: 0x02000179 RID: 377
public static class PortraitTools
{
	// Token: 0x0600142E RID: 5166 RVA: 0x00059A70 File Offset: 0x00057C70
	private static List<TextureTools.TextureData> getPortraitList()
	{
		if (PortraitTools.portraitList.Count == 0)
		{
			for (int i = 0; i < 6; i++)
			{
				TextureTools.TextureData item = new TextureTools.TextureData(PortraitTools.background.width, PortraitTools.background.height);
				PortraitTools.portraitList.Add(item);
			}
		}
		return PortraitTools.portraitList;
	}

	// Token: 0x0600142F RID: 5167 RVA: 0x00059ABF File Offset: 0x00057CBF
	public static List<TextureTools.TextureData> makePortraitList(Party party)
	{
		PortraitTools.makePortraitList(party.getPartyList(), party.getCurrentCharacter());
		return PortraitTools.getPortraitList();
	}

	// Token: 0x06001430 RID: 5168 RVA: 0x00059AD8 File Offset: 0x00057CD8
	public static List<TextureTools.TextureData> makeEmptyPortraitList()
	{
		for (int i = 0; i < PortraitTools.getPortraitList().Count; i++)
		{
			PortraitTools.appylBlankBackgroundToWorkspace(i);
			PortraitTools.backgroundWorkSpace.applyOverlay(PortraitTools.getPortraitList()[i]);
		}
		return PortraitTools.getPortraitList();
	}

	// Token: 0x06001431 RID: 5169 RVA: 0x00059B1C File Offset: 0x00057D1C
	public static List<TextureTools.TextureData> makePortraitList(List<Character> characters, Character highlightCharacter)
	{
		for (int i = 0; i < PortraitTools.getPortraitList().Count; i++)
		{
			if (i < characters.Count && characters[i] != null)
			{
				TextureTools.TextureData portrait = characters[i].getPortrait();
				PortraitTools.background2.copyToTarget(PortraitTools.backgroundWorkSpace);
				TextureTools.applyOverlay(PortraitTools.backgroundWorkSpace, portrait, 0, 7);
				TextureTools.drawStatusBars(characters[i], 0, 5, PortraitTools.backgroundWorkSpace);
				if (characters[i] == highlightCharacter && characters.Count > 1)
				{
					TextureTools.applyOverlay(PortraitTools.backgroundWorkSpace, PortraitTools.selector, 0, 7);
				}
			}
			else
			{
				PortraitTools.appylBlankBackgroundToWorkspace(i);
			}
			PortraitTools.backgroundWorkSpace.applyOverlay(PortraitTools.getPortraitList()[i]);
		}
		return PortraitTools.getPortraitList();
	}

	// Token: 0x06001432 RID: 5170 RVA: 0x00059BD8 File Offset: 0x00057DD8
	private static void appylBlankBackgroundToWorkspace(int i)
	{
		if (GlobalSettings.getGamePlaySettings().isNostalgiaModeOn())
		{
			if (i < 3)
			{
				PortraitTools.backgroundNostalgicFlipped.copyToTarget(PortraitTools.backgroundWorkSpace);
				return;
			}
			PortraitTools.backgroundNostalgic.copyToTarget(PortraitTools.backgroundWorkSpace);
			return;
		}
		else
		{
			if (i < 3)
			{
				PortraitTools.backgroundFlipped.copyToTarget(PortraitTools.backgroundWorkSpace);
				return;
			}
			PortraitTools.background.copyToTarget(PortraitTools.backgroundWorkSpace);
			return;
		}
	}

	// Token: 0x0400051D RID: 1309
	private static List<TextureTools.TextureData> portraitList = new List<TextureTools.TextureData>(6);

	// Token: 0x0400051E RID: 1310
	private static TextureTools.TextureData selector = TextureTools.loadTextureData("Images/Portraits/Selector");

	// Token: 0x0400051F RID: 1311
	private static TextureTools.TextureData background = TextureTools.loadTextureData("Images/Portraits/Background");

	// Token: 0x04000520 RID: 1312
	private static TextureTools.TextureData backgroundFlipped = TextureTools.loadTextureData("Images/Portraits/BackgroundFlipped");

	// Token: 0x04000521 RID: 1313
	private static TextureTools.TextureData backgroundNostalgic = TextureTools.loadTextureData("Images/Portraits/BackgroundNostalgic");

	// Token: 0x04000522 RID: 1314
	private static TextureTools.TextureData backgroundNostalgicFlipped = TextureTools.loadTextureData("Images/Portraits/BackgroundNostalgicFlipped");

	// Token: 0x04000523 RID: 1315
	private static TextureTools.TextureData background2 = TextureTools.loadTextureData("Images/Portraits/Background2");

	// Token: 0x04000524 RID: 1316
	private static TextureTools.TextureData backgroundWorkSpace = TextureTools.loadTextureData("Images/Portraits/Background");
}
