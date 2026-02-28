using System;
using UnityEngine;

// Token: 0x0200014B RID: 331
public class CreditsButtons : ListButtonControl
{
	// Token: 0x060012A8 RID: 4776 RVA: 0x000528C8 File Offset: 0x00050AC8
	public CreditsButtons(int x, int y, int width, int height, int buttonNumber) : base(x, y, width, height, buttonNumber)
	{
	}

	// Token: 0x060012A9 RID: 4777 RVA: 0x000528EC File Offset: 0x00050AEC
	protected override UIButtonControlBase.UITextButton createButton()
	{
		UIButtonControlBase.UITextButton uitextButton = new UIButtonControlBase.UITextButton(0, 0, this.getBaseWidth(), 9, C64Color.Yellow, GameData.getFont("FON_TinyFontTall"));
		if (this.centerButtonText)
		{
			uitextButton.setAlignment(new UIElement.Alignments(UIElement.Alignments.VerticalAlignments.Top, UIElement.Alignments.HorizontalAlignments.Center));
			uitextButton.stretchHorizontal = true;
		}
		uitextButton.setSingleLine();
		uitextButton.stretchHorizontal = true;
		uitextButton.foregroundColors.hoverColor = C64Color.Yellow;
		uitextButton.foregroundColors.leftClickedColor = C64Color.Yellow;
		uitextButton.foregroundColors.shadowMainColor = this.shadowColor;
		uitextButton.padding.bottom = 2;
		uitextButton.setTabWidth(this.tabWidth);
		return uitextButton;
	}

	// Token: 0x04000480 RID: 1152
	private int tabWidth = 68;

	// Token: 0x04000481 RID: 1153
	private Color32 shadowColor = C64Color.HighlightedSmallTextShadowColor;

	// Token: 0x04000482 RID: 1154
	private bool centerButtonText;
}
