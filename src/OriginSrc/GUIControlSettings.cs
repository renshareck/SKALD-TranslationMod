using System;

// Token: 0x0200011E RID: 286
public class GUIControlSettings : SheetClass
{
	// Token: 0x060011EE RID: 4590 RVA: 0x0004FEE6 File Offset: 0x0004E0E6
	public GUIControlSettings() : base(new GUIControl.SheetComplexSettings())
	{
	}

	// Token: 0x060011EF RID: 4591 RVA: 0x0004FEF3 File Offset: 0x0004E0F3
	public void setSliderControls(UITextSliderControl control)
	{
		(this.sheetComplex as GUIControl.SheetComplexSettings).setSliderControls(control);
	}

	// Token: 0x060011F0 RID: 4592 RVA: 0x0004FF06 File Offset: 0x0004E106
	public void updateMainDescriptionFontData()
	{
		(this.sheetComplex as GUIControl.SheetComplexSettings).updateMainDescriptionFontData();
	}

	// Token: 0x060011F1 RID: 4593 RVA: 0x0004FF18 File Offset: 0x0004E118
	protected override void setMouseToSelectedOption()
	{
		if (!this.snapToOptions)
		{
			return;
		}
		base.setMouseToUIElement(this.getControllerScrollableList(), 4, -4);
	}
}
