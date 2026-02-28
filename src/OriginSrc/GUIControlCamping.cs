using System;

// Token: 0x0200012B RID: 299
public class GUIControlCamping : SheetClass
{
	// Token: 0x06001206 RID: 4614 RVA: 0x00050073 File Offset: 0x0004E273
	public GUIControlCamping(UIInventorySheetCampingFood gridInventory) : base(new GUIControl.SheetComplexCamping(gridInventory))
	{
	}

	// Token: 0x06001207 RID: 4615 RVA: 0x00050081 File Offset: 0x0004E281
	protected override void setMouseToSelectedOption()
	{
		if (!this.snapToOptions)
		{
			return;
		}
		base.setMouseToUIElement(this.getControllerScrollableList(), 4, -8);
	}
}
