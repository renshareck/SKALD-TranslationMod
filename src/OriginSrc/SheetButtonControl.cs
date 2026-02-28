using System;
using System.Collections.Generic;

// Token: 0x02000149 RID: 329
public class SheetButtonControl : UIButtonControlVerticalText
{
	// Token: 0x0600129D RID: 4765 RVA: 0x00052400 File Offset: 0x00050600
	public SheetButtonControl(int x, int y, int width, int height) : base(x, y, width, height, 6)
	{
		base.toggleKeyboardPressControl();
		base.toggleBAXYControllerPressControl();
		this.setAlignment(new UIElement.Alignments(UIElement.Alignments.VerticalAlignments.Top, UIElement.Alignments.HorizontalAlignments.Center));
	}

	// Token: 0x0600129E RID: 4766 RVA: 0x00052454 File Offset: 0x00050654
	protected override UIButtonControlBase.UITextButton createButton()
	{
		UIButtonControlBase.UITextButton uitextButton = new UIButtonControlBase.UITextButton(20, 100, this.getBaseWidth(), 0, C64Color.GrayLight);
		uitextButton.foregroundColors.hoverColor = C64Color.Yellow;
		uitextButton.foregroundColors.leftClickedColor = C64Color.White;
		uitextButton.foregroundColors.shadowMainColor = C64Color.Black;
		this.toggleButtonBackground(uitextButton);
		uitextButton.setAlignment(new UIElement.Alignments(UIElement.Alignments.VerticalAlignments.Top, UIElement.Alignments.HorizontalAlignments.Center));
		return uitextButton;
	}

	// Token: 0x0600129F RID: 4767 RVA: 0x000524BC File Offset: 0x000506BC
	public override void setButtons(List<string> buttonTextList)
	{
		this.setReveal(false);
		base.setPosition();
		int num = 0;
		foreach (UIElement uielement in base.getButtonsList())
		{
			UIButtonControlBase.UITextButton uitextButton = (UIButtonControlBase.UITextButton)uielement;
			if (num < buttonTextList.Count && buttonTextList[num] != "")
			{
				if (this.controllerPressControl != null)
				{
					uitextButton.setContent(this.controllerPressControl.getButtonPrefix(num) + buttonTextList[num]);
				}
				else
				{
					uitextButton.setContent(SkaldOptionGlyphPrinter.printOptionNoGlyphs(num) + buttonTextList[num]);
				}
				uitextButton.hidden = false;
				this.toggleButtonBackground(uitextButton);
			}
			else
			{
				uitextButton.setContent();
				uitextButton.hidden = true;
			}
			num++;
		}
		base.getNestedCanvas().alignElements();
	}

	// Token: 0x060012A0 RID: 4768 RVA: 0x000525AC File Offset: 0x000507AC
	private void toggleButtonBackground(UIButtonControlBase.UITextButton button)
	{
		if (button.getLeftDown())
		{
			button.padding.top = 2;
			button.backgroundTexture = this.pressedBackground;
		}
		else
		{
			button.padding.top = 1;
			button.backgroundTexture = this.normalBackground;
		}
		button.setWidth(button.backgroundTexture.width);
		button.setHeight(button.backgroundTexture.height + 1);
	}

	// Token: 0x0400047B RID: 1147
	private TextureTools.TextureData normalBackground = TextureTools.loadTextureData("Images/GUIIcons/SheetButton");

	// Token: 0x0400047C RID: 1148
	private TextureTools.TextureData pressedBackground = TextureTools.loadTextureData("Images/GUIIcons/SheetButtonDown");
}
