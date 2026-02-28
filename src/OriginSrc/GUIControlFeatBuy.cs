using System;

// Token: 0x02000126 RID: 294
public class GUIControlFeatBuy : SheetClass
{
	// Token: 0x060011FF RID: 4607 RVA: 0x0004FFFE File Offset: 0x0004E1FE
	public GUIControlFeatBuy(UIFeatTree featTree) : base(new GUIControl.SheetComplexFeatTree(featTree))
	{
	}

	// Token: 0x06001200 RID: 4608 RVA: 0x0005000C File Offset: 0x0004E20C
	protected override void setMouseToSelectedOption()
	{
		if (!this.snapToOptions)
		{
			return;
		}
		base.setMouseToUIElement(this.getControllerScrollableList(), 8, -8);
	}
}
