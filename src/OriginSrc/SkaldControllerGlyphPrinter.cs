using System;
using System.Collections.Generic;

// Token: 0x02000066 RID: 102
public static class SkaldControllerGlyphPrinter
{
	// Token: 0x060008E4 RID: 2276 RVA: 0x0002AF28 File Offset: 0x00029128
	public static void displayOverlandGlyphs()
	{
		SkaldControllerGlyphPrinter.UIGlyphList uiglyphList = new SkaldControllerGlyphPrinter.UIGlyphList("---PAGE 1/2---");
		SkaldControllerGlyphPrinter.glyphListsToDraw.Add(uiglyphList);
		uiglyphList.addEntry(SkaldControllerGlyphPrinter.glyphStickLeft, "Move Character / Scroll");
		uiglyphList.addEntry(SkaldControllerGlyphPrinter.glyphStickRight, "Move Cursor");
		uiglyphList.addLineBreak();
		uiglyphList.addEntry(SkaldControllerGlyphPrinter.glyphTriggerRight, "Select / Move to Cursor");
		uiglyphList.addEntry(SkaldControllerGlyphPrinter.glyphTriggerLeft, "Inspect / Highlight Interactables");
		uiglyphList.addLineBreak();
		uiglyphList.addEntry(SkaldControllerGlyphPrinter.glyphButtonA, "Interact / Wait");
		uiglyphList.addEntry(SkaldControllerGlyphPrinter.glyphButtonB, "Menu / Back");
		uiglyphList.addEntry(SkaldControllerGlyphPrinter.glyphButtonX, "Inventory");
		uiglyphList.addEntry(SkaldControllerGlyphPrinter.glyphButtonY, "Character Sheet");
		SkaldControllerGlyphPrinter.UIGlyphList uiglyphList2 = new SkaldControllerGlyphPrinter.UIGlyphList("---PAGE 2/2---");
		SkaldControllerGlyphPrinter.glyphListsToDraw.Add(uiglyphList2);
		uiglyphList2.addEntry(SkaldControllerGlyphPrinter.glyphBumperRight, "Swap Character");
		uiglyphList2.addEntry(SkaldControllerGlyphPrinter.glyphBumperLeft, "Cycle Sheets");
		uiglyphList2.addLineBreak();
		uiglyphList2.addEntry(SkaldControllerGlyphPrinter.glyphDPadLeftRight, "Select Action Buttons");
		uiglyphList2.addEntry(SkaldControllerGlyphPrinter.glyphDPadDown, "Console Commands");
		uiglyphList2.addEntry(SkaldControllerGlyphPrinter.glyphDPadUp, "Show Controller Layout");
		uiglyphList2.addLineBreak();
		uiglyphList2.addEntry(SkaldControllerGlyphPrinter.glyphStickPushLeft, "Hide");
		uiglyphList2.addEntry(SkaldControllerGlyphPrinter.glyphStickPushRight, "Toggle Lightsource");
		uiglyphList2.addLineBreak();
		uiglyphList2.addEntry(SkaldControllerGlyphPrinter.glyphButtonMenu, "Journal");
		uiglyphList2.addEntry(SkaldControllerGlyphPrinter.glyphButtonView, "Quick Save");
	}

	// Token: 0x060008E5 RID: 2277 RVA: 0x0002B090 File Offset: 0x00029290
	public static void draw(TextureTools.TextureData targetTexture)
	{
		if (!SkaldIO.isControllerConnected())
		{
			SkaldControllerGlyphPrinter.glyphListsToDraw.Clear();
			return;
		}
		if (SkaldIO.pressedLayoutKey())
		{
			if (SkaldControllerGlyphPrinter.glyphListsToDraw.Count == 0)
			{
				SkaldControllerGlyphPrinter.displayOverlandGlyphs();
			}
			else
			{
				SkaldControllerGlyphPrinter.glyphListsToDraw.RemoveAt(0);
			}
		}
		if (SkaldControllerGlyphPrinter.glyphListsToDraw.Count == 0)
		{
			return;
		}
		SkaldControllerGlyphPrinter.glyphListsToDraw[0].draw(targetTexture);
		if (SkaldControllerGlyphPrinter.glyphListsToDraw[0].isDead())
		{
			SkaldControllerGlyphPrinter.glyphListsToDraw.RemoveAt(0);
		}
	}

	// Token: 0x04000229 RID: 553
	private static string basePath = "Images/GUIIcons/ControllerGlyphs/";

	// Token: 0x0400022A RID: 554
	private static TextureTools.TextureData glyphButtonA = TextureTools.loadTextureData(SkaldControllerGlyphPrinter.basePath + "GlyphButtonA");

	// Token: 0x0400022B RID: 555
	private static TextureTools.TextureData glyphButtonB = TextureTools.loadTextureData(SkaldControllerGlyphPrinter.basePath + "GlyphButtonB");

	// Token: 0x0400022C RID: 556
	private static TextureTools.TextureData glyphButtonX = TextureTools.loadTextureData(SkaldControllerGlyphPrinter.basePath + "GlyphButtonX");

	// Token: 0x0400022D RID: 557
	private static TextureTools.TextureData glyphButtonY = TextureTools.loadTextureData(SkaldControllerGlyphPrinter.basePath + "GlyphButtonY");

	// Token: 0x0400022E RID: 558
	private static TextureTools.TextureData glyphButtonMenu = TextureTools.loadTextureData(SkaldControllerGlyphPrinter.basePath + "GlyphButtonMenu");

	// Token: 0x0400022F RID: 559
	private static TextureTools.TextureData glyphButtonView = TextureTools.loadTextureData(SkaldControllerGlyphPrinter.basePath + "GlyphButtonView");

	// Token: 0x04000230 RID: 560
	private static TextureTools.TextureData glyphDPadUp = TextureTools.loadTextureData(SkaldControllerGlyphPrinter.basePath + "GlyphDPadUp");

	// Token: 0x04000231 RID: 561
	private static TextureTools.TextureData glyphDPadRight = TextureTools.loadTextureData(SkaldControllerGlyphPrinter.basePath + "GlyphDPadRight");

	// Token: 0x04000232 RID: 562
	private static TextureTools.TextureData glyphDPadDown = TextureTools.loadTextureData(SkaldControllerGlyphPrinter.basePath + "GlyphDPadDown");

	// Token: 0x04000233 RID: 563
	private static TextureTools.TextureData glyphDPadLeft = TextureTools.loadTextureData(SkaldControllerGlyphPrinter.basePath + "GlyphDPadLeft");

	// Token: 0x04000234 RID: 564
	private static TextureTools.TextureData glyphDPadLeftRight = TextureTools.loadTextureData(SkaldControllerGlyphPrinter.basePath + "GlyphDPadLeftRight");

	// Token: 0x04000235 RID: 565
	private static TextureTools.TextureData glyphBumperLeft = TextureTools.loadTextureData(SkaldControllerGlyphPrinter.basePath + "GlyphBumperLeft");

	// Token: 0x04000236 RID: 566
	private static TextureTools.TextureData glyphBumperRight = TextureTools.loadTextureData(SkaldControllerGlyphPrinter.basePath + "GlyphBumperRight");

	// Token: 0x04000237 RID: 567
	private static TextureTools.TextureData glyphTriggerLeft = TextureTools.loadTextureData(SkaldControllerGlyphPrinter.basePath + "GlyphTriggerLeft");

	// Token: 0x04000238 RID: 568
	private static TextureTools.TextureData glyphTriggerRight = TextureTools.loadTextureData(SkaldControllerGlyphPrinter.basePath + "GlyphTriggerRight");

	// Token: 0x04000239 RID: 569
	private static TextureTools.TextureData glyphStickLeft = TextureTools.loadTextureData(SkaldControllerGlyphPrinter.basePath + "GlyphStickLeft");

	// Token: 0x0400023A RID: 570
	private static TextureTools.TextureData glyphStickRight = TextureTools.loadTextureData(SkaldControllerGlyphPrinter.basePath + "GlyphStickRight");

	// Token: 0x0400023B RID: 571
	private static TextureTools.TextureData glyphStickPushLeft = TextureTools.loadTextureData(SkaldControllerGlyphPrinter.basePath + "GlyphStickPushLeft");

	// Token: 0x0400023C RID: 572
	private static TextureTools.TextureData glyphStickPushRight = TextureTools.loadTextureData(SkaldControllerGlyphPrinter.basePath + "GlyphStickPushRight");

	// Token: 0x0400023D RID: 573
	private static List<SkaldControllerGlyphPrinter.UIGlyphList> glyphListsToDraw = new List<SkaldControllerGlyphPrinter.UIGlyphList>();

	// Token: 0x0200022D RID: 557
	private class UIGlyphList : UICanvasVertical
	{
		// Token: 0x06001898 RID: 6296 RVA: 0x0006C6E8 File Offset: 0x0006A8E8
		public UIGlyphList(string title) : base(10, 165, 0, 0)
		{
			UITextBlock uitextBlock = new UITextBlock(0, 0, 215, 0, C64Color.White);
			uitextBlock.setLetterShadowColor(C64Color.Black);
			uitextBlock.setContent(title);
			this.add(uitextBlock);
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x0006C73B File Offset: 0x0006A93B
		public void addEntry(TextureTools.TextureData glyphTexture, string descriptor)
		{
			this.add(new SkaldControllerGlyphPrinter.UIGlyphList.UIGlyphTextCombo(glyphTexture, descriptor));
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x0006C74A File Offset: 0x0006A94A
		public void addLineBreak()
		{
			this.add(new UIImage(0, 0, 1, 4));
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x0006C75B File Offset: 0x0006A95B
		public override void draw(TextureTools.TextureData targetTexture)
		{
			this.life--;
			base.draw(targetTexture);
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x0006C772 File Offset: 0x0006A972
		public bool isDead()
		{
			return this.life <= 0;
		}

		// Token: 0x0400088D RID: 2189
		private int life = 400;

		// Token: 0x020003C9 RID: 969
		private class UIGlyphTextCombo : UICanvasHorizontal
		{
			// Token: 0x06001D47 RID: 7495 RVA: 0x0007BA84 File Offset: 0x00079C84
			public UIGlyphTextCombo(TextureTools.TextureData glyphTexture, string descriptor)
			{
				this.stretchVertical = true;
				UIImage uiimage = new UIImage(glyphTexture);
				uiimage.padding.right = 2;
				this.add(uiimage);
				UITextBlock uitextBlock = new UITextBlock(0, 0, 200, 0, C64Color.White);
				uitextBlock.setLetterShadowColor(C64Color.Black);
				uitextBlock.setContent(descriptor);
				uitextBlock.padding.top = 6;
				this.add(uitextBlock);
			}
		}
	}
}
