using System;

// Token: 0x02000129 RID: 297
public class GUIControlContainer : GUIControlInventoryBase
{
	// Token: 0x06001204 RID: 4612 RVA: 0x00050057 File Offset: 0x0004E257
	public GUIControlContainer(UIInventorySheetBase gridInventory) : base(new GUIControl.SheetComplexContainer(gridInventory))
	{
	}
}
