using System;
using System.Collections.Generic;

// Token: 0x0200011F RID: 287
public class GUIControlExtraRowList : SheetClass
{
	// Token: 0x060011F2 RID: 4594 RVA: 0x0004FF32 File Offset: 0x0004E132
	public GUIControlExtraRowList() : base(new GUIControl.ExtraButtonRowSheetComplex())
	{
	}

	// Token: 0x060011F3 RID: 4595 RVA: 0x0004FF3F File Offset: 0x0004E13F
	public void setHorizontalMenuButtons(List<string> options, int currentIndex)
	{
		(this.sheetComplex as GUIControl.ExtraButtonRowSheetComplex).setHorizontalMenuButtons(options, currentIndex);
	}

	// Token: 0x060011F4 RID: 4596 RVA: 0x0004FF53 File Offset: 0x0004E153
	public int getHorizontalMenuButtonsIndex()
	{
		return (this.sheetComplex as GUIControl.ExtraButtonRowSheetComplex).getHorizontalMenuButtonsIndex();
	}

	// Token: 0x060011F5 RID: 4597 RVA: 0x0004FF65 File Offset: 0x0004E165
	protected override void setMouseToSelectedOption()
	{
		if (!this.snapToOptions)
		{
			return;
		}
		base.setMouseToUIElement(this.getControllerScrollableList(), 25, -8);
	}

	// Token: 0x060011F6 RID: 4598 RVA: 0x0004FF80 File Offset: 0x0004E180
	public override void controllerScrollSidewaysRight()
	{
		this.setMouseToClosestOptionBelow();
	}

	// Token: 0x060011F7 RID: 4599 RVA: 0x0004FF88 File Offset: 0x0004E188
	public override void controllerScrollSidewaysLeft()
	{
		this.setMouseToClosestOptionAbove();
	}
}
