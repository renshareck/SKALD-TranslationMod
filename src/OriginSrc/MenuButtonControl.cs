using System;

// Token: 0x0200014D RID: 333
public class MenuButtonControl : UIButtonControlVerticalText
{
	// Token: 0x060012AE RID: 4782 RVA: 0x00052B04 File Offset: 0x00050D04
	public MenuButtonControl(int x, int y, int width, int height) : base(x, y, width, height, 8)
	{
		base.toggleKeyboardPressControl();
		base.getNestedCanvas().alignments.horizontalAlignment = UIElement.Alignments.HorizontalAlignments.Center;
	}

	// Token: 0x060012AF RID: 4783 RVA: 0x00052B2C File Offset: 0x00050D2C
	protected override UIButtonControlBase.UITextButton createButton()
	{
		UIButtonControlBase.UITextButton uitextButton = new UIButtonControlBase.UITextButton(100, 100, 0, 12, C64Color.Yellow, FontContainer.getMediumFont());
		uitextButton.foregroundColors.hoverColor = C64Color.White;
		uitextButton.foregroundColors.leftClickedColor = C64Color.Gray;
		uitextButton.foregroundColors.shadowMainColor = C64Color.Black;
		uitextButton.stretchHorizontal = true;
		return uitextButton;
	}
}
