using System;

// Token: 0x0200014E RID: 334
public class SingleButtonControl : UIButtonControlVerticalText
{
	// Token: 0x060012B0 RID: 4784 RVA: 0x00052B86 File Offset: 0x00050D86
	public SingleButtonControl() : base(0, 0, 100, 23, 1)
	{
		this.setPaddingTop(3);
		this.setPaddingLeft(5);
	}

	// Token: 0x060012B1 RID: 4785 RVA: 0x00052BA4 File Offset: 0x00050DA4
	protected override UIButtonControlBase.UITextButton createButton()
	{
		UIButtonControlBase.UITextButton uitextButton = new UIButtonControlBase.UITextButton(100, 100, this.getBaseWidth(), 12, C64Color.Yellow, FontContainer.getMediumFont());
		uitextButton.foregroundColors.hoverColor = C64Color.White;
		uitextButton.foregroundColors.leftClickedColor = C64Color.Gray;
		uitextButton.foregroundColors.shadowMainColor = C64Color.Black;
		uitextButton.alignments.horizontalAlignment = UIElement.Alignments.HorizontalAlignments.Center;
		return uitextButton;
	}
}
