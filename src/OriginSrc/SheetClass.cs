using System;

// Token: 0x0200011C RID: 284
public abstract class SheetClass : NumericListClass
{
	// Token: 0x060011EB RID: 4587 RVA: 0x0004FE78 File Offset: 0x0004E078
	protected SheetClass(GUIControl.SheetComplex sheet)
	{
		this.sheetComplex = sheet;
		this.numericButtons = this.sheetComplex.getNumericButtons();
		this.menuTab = this.sheetComplex.getTabRow();
		this.listButtons = this.sheetComplex.getListButtons();
		this.snapToOptions = true;
	}

	// Token: 0x060011EC RID: 4588 RVA: 0x0004FECC File Offset: 0x0004E0CC
	protected override UICanvas getControllerScrollableList()
	{
		return this.sheetComplex.getControllerScrollableList();
	}
}
