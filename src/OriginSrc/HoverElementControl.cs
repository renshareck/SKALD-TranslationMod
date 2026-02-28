using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200003D RID: 61
public static class HoverElementControl
{
	// Token: 0x060007DD RID: 2013 RVA: 0x0002777C File Offset: 0x0002597C
	public static void addHoverText(string header, string description)
	{
		HoverElementControl.primaryTextList.Add(new HoverElementControl.HoverText(header, description));
	}

	// Token: 0x060007DE RID: 2014 RVA: 0x0002778F File Offset: 0x0002598F
	public static void addHoverImage(string path)
	{
		HoverElementControl.secondaryList.Add(new HoverElementControl.HoverImage(path));
	}

	// Token: 0x060007DF RID: 2015 RVA: 0x000277A1 File Offset: 0x000259A1
	public static void addHoverTextRed(string header, string description)
	{
		HoverElementControl.primaryTextList.Add(new HoverElementControl.HoverTextRed(header, description));
	}

	// Token: 0x060007E0 RID: 2016 RVA: 0x000277B4 File Offset: 0x000259B4
	public static void addTacticalHoverTextFlashing(string header, int x, int y)
	{
		HoverElementControl.tacticalTextList.Add(new HoverElementControl.TacticalHoverTextFlashing(header, "", x, y));
	}

	// Token: 0x060007E1 RID: 2017 RVA: 0x000277CD File Offset: 0x000259CD
	public static bool hasTacticalText()
	{
		return HoverElementControl.tacticalTextList.Count > 0;
	}

	// Token: 0x060007E2 RID: 2018 RVA: 0x000277DC File Offset: 0x000259DC
	public static void draw(TextureTools.TextureData targetTexture)
	{
		HoverElementControl.drawList(targetTexture, HoverElementControl.primaryTextList);
		HoverElementControl.drawList(targetTexture, HoverElementControl.tacticalTextList);
		HoverElementControl.drawList(targetTexture, HoverElementControl.secondaryList);
	}

	// Token: 0x060007E3 RID: 2019 RVA: 0x000277FF File Offset: 0x000259FF
	private static void drawList(TextureTools.TextureData targetTexture, List<HoverElementControl.HoverElement> list)
	{
		if (list.Count == 0)
		{
			return;
		}
		if (list[0] != null)
		{
			list[0].draw(targetTexture);
		}
		if (list[0].isDead())
		{
			list.RemoveAt(0);
		}
	}

	// Token: 0x04000192 RID: 402
	private static List<HoverElementControl.HoverElement> primaryTextList = new List<HoverElementControl.HoverElement>();

	// Token: 0x04000193 RID: 403
	private static List<HoverElementControl.HoverElement> secondaryList = new List<HoverElementControl.HoverElement>();

	// Token: 0x04000194 RID: 404
	private static List<HoverElementControl.HoverElement> tacticalTextList = new List<HoverElementControl.HoverElement>();

	// Token: 0x020001F4 RID: 500
	private abstract class HoverElement : UICanvasVertical
	{
		// Token: 0x060017B3 RID: 6067 RVA: 0x00068DE4 File Offset: 0x00066FE4
		public override void draw(TextureTools.TextureData targetTexture)
		{
			this.reveal();
			this.updateHangtime();
			if (this.hangtime < this.fadeThreshold)
			{
				return;
			}
			base.draw(targetTexture);
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x00068E08 File Offset: 0x00067008
		private void updateHangtime()
		{
			if (this.hangtime > 0)
			{
				this.hangtime--;
			}
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x00068E21 File Offset: 0x00067021
		public bool isDead()
		{
			return this.hangtime < 1;
		}

		// Token: 0x040007C3 RID: 1987
		protected int hangtime = 180;

		// Token: 0x040007C4 RID: 1988
		protected int fadeThreshold;
	}

	// Token: 0x020001F5 RID: 501
	private class HoverImage : HoverElementControl.HoverElement
	{
		// Token: 0x060017B7 RID: 6071 RVA: 0x00068E40 File Offset: 0x00067040
		public HoverImage(string path)
		{
			this.hangtime = 240;
			this.fadeThreshold = 60;
			this.backgroundTexture = TextureTools.loadTextureData("Images/GUIIcons/" + path);
			if (this.backgroundTexture != null)
			{
				this.setPosition(16, 16 + this.backgroundTexture.height);
			}
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x00068EA8 File Offset: 0x000670A8
		public override void draw(TextureTools.TextureData targetTexture)
		{
			this.flashTimer.tick();
			if (this.flashTimer.isTimerZero())
			{
				this.colorIndex++;
				if (this.colorIndex >= HoverElementControl.HoverImage.colorArray.Length)
				{
					this.colorIndex = 0;
				}
				this.backgroundTexture.clearToColorIfNotBlack(HoverElementControl.HoverImage.colorArray[this.colorIndex]);
			}
			base.draw(targetTexture);
		}

		// Token: 0x040007C5 RID: 1989
		private static Color32[] colorArray = new Color32[]
		{
			C64Color.White,
			C64Color.GrayLight
		};

		// Token: 0x040007C6 RID: 1990
		private CountDownClock flashTimer = new CountDownClock(30, true);

		// Token: 0x040007C7 RID: 1991
		private int colorIndex;
	}

	// Token: 0x020001F6 RID: 502
	private class HoverText : HoverElementControl.HoverElement
	{
		// Token: 0x060017BA RID: 6074 RVA: 0x00068F38 File Offset: 0x00067138
		public HoverText(string header, string description)
		{
			this.setPosition(16, 250);
			this.addHoverTextElements(header, description);
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x00068F55 File Offset: 0x00067155
		public HoverText(string header, string description, int x, int y)
		{
			this.setPosition(x, y);
			this.addHoverTextElements(header, description);
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x00068F6E File Offset: 0x0006716E
		protected virtual void addHoverTextElements(string header, string description)
		{
			this.add(new HoverElementControl.HoverText.HoverTextElement(header, FontContainer.getMediumFont()));
			this.add(new HoverElementControl.HoverText.HoverTextElement(description, FontContainer.getTinyFont()));
		}

		// Token: 0x02000321 RID: 801
		protected class HoverTextElement : UITextBlock
		{
			// Token: 0x06001C90 RID: 7312 RVA: 0x0007AE5B File Offset: 0x0007905B
			public HoverTextElement(string content, Font font, Color32 color) : base(0, 0, 0, 16, color, font)
			{
				this.stretchHorizontal = true;
				this.setReveal(false);
				this.foregroundColors.shadowMainColor = C64Color.GrayDark;
				base.setContent(content);
			}

			// Token: 0x06001C91 RID: 7313 RVA: 0x0007AE8F File Offset: 0x0007908F
			public HoverTextElement(string content, Font font) : this(content, font, C64Color.White)
			{
			}
		}
	}

	// Token: 0x020001F7 RID: 503
	private class TacticalHoverTextFlashing : HoverElementControl.HoverTextRed
	{
		// Token: 0x060017BD RID: 6077 RVA: 0x00068F92 File Offset: 0x00067192
		public TacticalHoverTextFlashing(string header, string description, int x, int y) : base(header, description)
		{
			this.hangtime = 60;
			this.setPosition(x, y);
			this.alignElements();
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x00068FB4 File Offset: 0x000671B4
		protected override void addHoverTextElements(string header, string description)
		{
			HoverElementControl.TacticalHoverTextFlashing.HoverTextElementFlashing hoverTextElementFlashing = new HoverElementControl.TacticalHoverTextFlashing.HoverTextElementFlashing(header, FontContainer.getMediumFont());
			hoverTextElementFlashing.setAlignment(new UIElement.Alignments(UIElement.Alignments.VerticalAlignments.Top, UIElement.Alignments.HorizontalAlignments.Center));
			HoverElementControl.TacticalHoverTextFlashing.HoverTextElementFlashing element = new HoverElementControl.TacticalHoverTextFlashing.HoverTextElementFlashing(description, FontContainer.getTinyFont());
			hoverTextElementFlashing.setAlignment(new UIElement.Alignments(UIElement.Alignments.VerticalAlignments.Top, UIElement.Alignments.HorizontalAlignments.Center));
			this.add(hoverTextElementFlashing);
			this.add(element);
		}

		// Token: 0x02000322 RID: 802
		protected class HoverTextElementFlashing : HoverElementControl.HoverText.HoverTextElement
		{
			// Token: 0x06001C92 RID: 7314 RVA: 0x0007AE9E File Offset: 0x0007909E
			public HoverTextElementFlashing(string content, Font font) : base(content, font, HoverElementControl.TacticalHoverTextFlashing.HoverTextElementFlashing.colorArray[0])
			{
				base.setLetterShadowColor(C64Color.Black);
			}

			// Token: 0x06001C93 RID: 7315 RVA: 0x0007AECC File Offset: 0x000790CC
			public override void draw(TextureTools.TextureData targetTexture)
			{
				this.flashTimer.tick();
				if (this.flashTimer.isTimerZero())
				{
					this.colorIndex++;
					if (this.colorIndex >= HoverElementControl.TacticalHoverTextFlashing.HoverTextElementFlashing.colorArray.Length)
					{
						this.colorIndex = 0;
					}
					base.setLetterMainColor(HoverElementControl.TacticalHoverTextFlashing.HoverTextElementFlashing.colorArray[this.colorIndex]);
				}
				base.draw(targetTexture);
			}

			// Token: 0x04000AB1 RID: 2737
			private static Color32[] colorArray = new Color32[]
			{
				C64Color.White,
				C64Color.Cyan,
				C64Color.Yellow,
				C64Color.GreenLight
			};

			// Token: 0x04000AB2 RID: 2738
			private CountDownClock flashTimer = new CountDownClock(3, true);

			// Token: 0x04000AB3 RID: 2739
			private int colorIndex;
		}
	}

	// Token: 0x020001F8 RID: 504
	private class HoverTextRed : HoverElementControl.HoverText
	{
		// Token: 0x060017BF RID: 6079 RVA: 0x00069001 File Offset: 0x00067201
		public HoverTextRed(string header, string description) : base(header, description)
		{
			this.hangtime = 240;
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x00069016 File Offset: 0x00067216
		protected override void addHoverTextElements(string header, string description)
		{
			this.add(new HoverElementControl.HoverTextRed.HoverTextElementRed(header, FontContainer.getMediumFont()));
			this.add(new HoverElementControl.HoverTextRed.HoverTextElementRed(description, FontContainer.getTinyFont()));
		}

		// Token: 0x02000323 RID: 803
		protected class HoverTextElementRed : HoverElementControl.HoverText.HoverTextElement
		{
			// Token: 0x06001C95 RID: 7317 RVA: 0x0007AF6F File Offset: 0x0007916F
			public HoverTextElementRed(string content, Font font) : base(content, font, C64Color.RedLight)
			{
				base.setLetterShadowColor(C64Color.Black);
			}
		}
	}
}
