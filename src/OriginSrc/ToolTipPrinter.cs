using System;
using UnityEngine;

// Token: 0x020000B4 RID: 180
public static class ToolTipPrinter
{
	// Token: 0x06000AFC RID: 2812 RVA: 0x00034990 File Offset: 0x00032B90
	public static void draw(TextureTools.TextureData outputImage)
	{
		if (ToolTipPrinter.toolTip != null)
		{
			ToolTipPrinter.toolTip.draw(outputImage);
		}
	}

	// Token: 0x06000AFD RID: 2813 RVA: 0x000349A4 File Offset: 0x00032BA4
	public static void updateTooltips()
	{
		if (ToolTipPrinter.hasToolTip() && !ToolTipPrinter.clickBuffer)
		{
			ToolTipPrinter.toolTip.update();
			if (!ToolTipPrinter.toolTip.getHover() || SkaldIO.getMousePressed(1))
			{
				ToolTipPrinter.clearToolTip();
			}
		}
		ToolTipPrinter.clickBuffer = false;
	}

	// Token: 0x06000AFE RID: 2814 RVA: 0x000349DD File Offset: 0x00032BDD
	public static void clearToolTip()
	{
		ToolTipPrinter.toolTip = null;
	}

	// Token: 0x06000AFF RID: 2815 RVA: 0x000349E5 File Offset: 0x00032BE5
	public static bool hasToolTip()
	{
		return ToolTipPrinter.toolTip != null;
	}

	// Token: 0x06000B00 RID: 2816 RVA: 0x000349EF File Offset: 0x00032BEF
	public static bool isMouseOverTooltip()
	{
		return ToolTipPrinter.hasToolTip() && ToolTipPrinter.toolTip.getHover();
	}

	// Token: 0x06000B01 RID: 2817 RVA: 0x00034A04 File Offset: 0x00032C04
	public static void setToolTipWithRules(string message)
	{
		ToolTipPrinter.setToolTip(message, ToolTipControl.getRulesToolTips());
	}

	// Token: 0x06000B02 RID: 2818 RVA: 0x00034A14 File Offset: 0x00032C14
	public static void setToolTip(string message, ToolTipControl.ToolTipCategory toolTipCategory)
	{
		if (!PopUpControl.allowTooltips())
		{
			return;
		}
		if (message == "")
		{
			return;
		}
		ToolTipPrinter.clickBuffer = true;
		ToolTipPrinter.UITooltip uitooltip = ToolTipPrinter.toolTip;
		ToolTipPrinter.toolTip = new ToolTipPrinter.UITooltip((int)SkaldIO.getMousePosition().x, (int)SkaldIO.getMousePosition().y, 0, 0, C64Color.SmallTextColor, FontContainer.getSmallDescriptionFont());
		ToolTipPrinter.toolTip.setLetterShadowColor(C64Color.Black);
		ToolTipPrinter.toolTip.backgroundTexture = TextureTools.loadTextureData("Images/GUIIcons/TextBoxToolTip1");
		ToolTipPrinter.toolTip.setYieldToTooltips(false);
		ToolTipPrinter.toolTip.setWidth(ToolTipPrinter.toolTip.backgroundTexture.width);
		ToolTipPrinter.toolTip.padding = new UIElement.Padding(7, 7, 7, 9);
		if (toolTipCategory != null)
		{
			ToolTipPrinter.toolTip.setTooltips(toolTipCategory);
		}
		ToolTipPrinter.toolTip.stretchVertical = true;
		ToolTipPrinter.toolTip.setContent(message);
		if (ToolTipPrinter.toolTip.getHeight() < 30)
		{
			ToolTipPrinter.toolTip.backgroundTexture = TextureTools.loadTextureData("Images/GUIIcons/TextBoxToolTip00");
		}
		else if (ToolTipPrinter.toolTip.getHeight() < 40)
		{
			ToolTipPrinter.toolTip.backgroundTexture = TextureTools.loadTextureData("Images/GUIIcons/TextBoxToolTip0");
		}
		else if (ToolTipPrinter.toolTip.getHeight() < 70)
		{
			ToolTipPrinter.toolTip.backgroundTexture = TextureTools.loadTextureData("Images/GUIIcons/TextBoxToolTip1");
		}
		else if (ToolTipPrinter.toolTip.getHeight() < 90)
		{
			ToolTipPrinter.toolTip.backgroundTexture = TextureTools.loadTextureData("Images/GUIIcons/TextBoxToolTip2");
		}
		else if (ToolTipPrinter.toolTip.getHeight() < 130)
		{
			ToolTipPrinter.toolTip.backgroundTexture = TextureTools.loadTextureData("Images/GUIIcons/TextBoxToolTip3");
		}
		else if (ToolTipPrinter.toolTip.getHeight() < 170)
		{
			ToolTipPrinter.toolTip.backgroundTexture = TextureTools.loadTextureData("Images/GUIIcons/TextBoxToolTip4");
		}
		else if (ToolTipPrinter.toolTip.getHeight() < 210)
		{
			ToolTipPrinter.toolTip.backgroundTexture = TextureTools.loadTextureData("Images/GUIIcons/TextBoxToolTip5");
		}
		else if (ToolTipPrinter.toolTip.getHeight() <= 260)
		{
			ToolTipPrinter.toolTip.backgroundTexture = TextureTools.loadTextureData("Images/GUIIcons/TextBoxToolTip6");
		}
		else if (ToolTipPrinter.toolTip.getHeight() <= 285)
		{
			ToolTipPrinter.toolTip.backgroundTexture = TextureTools.loadTextureData("Images/GUIIcons/TextBoxToolTip7");
			ToolTipPrinter.toolTip.setWidth(ToolTipPrinter.toolTip.backgroundTexture.width);
			ToolTipPrinter.toolTip.forceSetContent(message);
		}
		else
		{
			ToolTipPrinter.toolTip.backgroundTexture = TextureTools.loadTextureData("Images/GUIIcons/TextBoxToolTip8");
			ToolTipPrinter.toolTip.setWidth(ToolTipPrinter.toolTip.backgroundTexture.width - 12);
			ToolTipPrinter.toolTip.forceSetContent(message);
			ToolTipPrinter.toolTip.addScrollbar();
		}
		ToolTipPrinter.toolTip.stretchVertical = false;
		ToolTipPrinter.toolTip.setHeight(ToolTipPrinter.toolTip.backgroundTexture.height);
		if (uitooltip != null)
		{
			ToolTipPrinter.toolTip.setX(uitooltip.getX());
			ToolTipPrinter.toolTip.setY(uitooltip.getY());
			if ((float)(ToolTipPrinter.toolTip.getY() - ToolTipPrinter.toolTip.getHeight()) > SkaldIO.getMousePosition().y)
			{
				ToolTipPrinter.toolTip.setY((int)SkaldIO.getMousePosition().y + ToolTipPrinter.toolTip.getHeight() - 10);
			}
			if ((float)(ToolTipPrinter.toolTip.getX() + ToolTipPrinter.toolTip.getWidth()) < SkaldIO.getMousePosition().x)
			{
				ToolTipPrinter.toolTip.setX((int)SkaldIO.getMousePosition().x - ToolTipPrinter.toolTip.getWidth() / 2);
			}
		}
		else
		{
			ToolTipPrinter.toolTip.setX((int)SkaldIO.getMousePosition().x - ToolTipPrinter.toolTip.getWidth() / 2);
			ToolTipPrinter.toolTip.setY((int)SkaldIO.getMousePosition().y + ToolTipPrinter.toolTip.getHeight() - 10);
		}
		ToolTipPrinter.toolTip.ensureOnScreen();
		ToolTipPrinter.toolTip.alignElements();
	}

	// Token: 0x040002F5 RID: 757
	private static ToolTipPrinter.UITooltip toolTip;

	// Token: 0x040002F6 RID: 758
	private static bool clickBuffer;

	// Token: 0x0200023C RID: 572
	public class UITooltip : UITextBlock
	{
		// Token: 0x060018FC RID: 6396 RVA: 0x0006D699 File Offset: 0x0006B899
		public UITooltip(int x, int y, int width, int height, Color32 color, Font font) : base(x, y, width, height, color, font)
		{
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x0006D6AA File Offset: 0x0006B8AA
		public void addScrollbar()
		{
			this.scrollBar = new UIScrollbarThatDoesNOTUpdateTargetCanvas(11, this.backgroundTexture.height - 8, this);
			this.scrollBar.fixedPosition = true;
			this.add(this.scrollBar);
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x0006D6DF File Offset: 0x0006B8DF
		public void update()
		{
			this.updateMouseInteraction();
			this.updateScrollbar();
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x0006D6ED File Offset: 0x0006B8ED
		public override void draw(TextureTools.TextureData targetTexture)
		{
			base.draw(targetTexture);
			if (this.scrollBar != null)
			{
				this.scrollBar.draw(targetTexture);
			}
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x0006D70C File Offset: 0x0006B90C
		public void updateScrollbar()
		{
			if (this.scrollBar == null)
			{
				return;
			}
			this.scrollBar.setX(this.getX() + (this.getWidth() - 20));
			this.scrollBar.setY(this.getY() - 6);
			this.scrollBar.updateMouseInteraction();
			this.scrollBar.setIncrement(Mathf.RoundToInt((float)base.getMaxLines()));
			int scrollIndex = Mathf.RoundToInt((float)base.getMaxLines() * this.scrollBar.getScrollDegree());
			base.setScrollIndex(scrollIndex);
		}

		// Token: 0x040008B0 RID: 2224
		private UIScrollbar scrollBar;
	}
}
