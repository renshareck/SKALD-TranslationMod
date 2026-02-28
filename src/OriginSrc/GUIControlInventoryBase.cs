using System;

// Token: 0x02000127 RID: 295
public abstract class GUIControlInventoryBase : SheetClass
{
	// Token: 0x06001201 RID: 4609 RVA: 0x00050026 File Offset: 0x0004E226
	protected GUIControlInventoryBase(GUIControl.SheetComplex sheet) : base(sheet)
	{
	}

	// Token: 0x06001202 RID: 4610 RVA: 0x0005002F File Offset: 0x0004E22F
	protected override void setMouseToSelectedOption()
	{
		if (!this.snapToOptions)
		{
			return;
		}
		base.setMouseToUIElement(this.getControllerScrollableList(), 8, -8);
	}
}
