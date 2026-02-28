using System;
using System.Collections.Generic;

// Token: 0x02000148 RID: 328
public class NumericButtonControl : UIButtonControlVerticalText
{
	// Token: 0x0600129A RID: 4762 RVA: 0x000522AC File Offset: 0x000504AC
	public NumericButtonControl(int x, int y, int width, int height) : base(x, y, width, height, 8)
	{
		base.toggleKeyboardPressControl();
		this.setAlignment(new UIElement.Alignments(UIElement.Alignments.VerticalAlignments.Top, UIElement.Alignments.HorizontalAlignments.Center));
		base.setStretchVertical(true);
	}

	// Token: 0x0600129B RID: 4763 RVA: 0x000522D4 File Offset: 0x000504D4
	protected override UIButtonControlBase.UITextButton createButton()
	{
		UIButtonControlBase.UITextButton uitextButton = new UIButtonControlBase.UITextButton(20, 100, this.getBaseWidth(), 0, C64Color.Cyan, FontContainer.getSmallDescriptionFont());
		uitextButton.foregroundColors.hoverColor = C64Color.GreenLight;
		uitextButton.foregroundColors.leftClickedColor = C64Color.Gray;
		uitextButton.foregroundColors.shadowMainColor = C64Color.HighlightedSmallTextShadowColor;
		uitextButton.padding.top = 2;
		return uitextButton;
	}

	// Token: 0x0600129C RID: 4764 RVA: 0x00052338 File Offset: 0x00050538
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
				uitextButton.setContent(C64Color.YELLOW_TAG + (num + 1).ToString() + ". </color>" + buttonTextList[num]);
				uitextButton.hidden = false;
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
}
