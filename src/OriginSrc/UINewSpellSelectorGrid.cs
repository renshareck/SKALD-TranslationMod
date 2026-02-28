using System;
using System.Collections.Generic;

// Token: 0x02000134 RID: 308
public class UINewSpellSelectorGrid : UIAbilitySelectorGrid
{
	// Token: 0x06001217 RID: 4631 RVA: 0x000502D4 File Offset: 0x0004E4D4
	public UINewSpellSelectorGrid(List<UIButtonControlBase.ButtonData> buttonData) : base(buttonData, 0, 0, 10)
	{
		this.padding.left = 3;
		this.padding.bottom = 4;
	}

	// Token: 0x06001218 RID: 4632 RVA: 0x000502F9 File Offset: 0x0004E4F9
	public void update(List<UIButtonControlBase.ButtonData> buttonData)
	{
		base.updateMainGridImage(buttonData);
		base.update();
	}

	// Token: 0x06001219 RID: 4633 RVA: 0x00050308 File Offset: 0x0004E508
	protected override void setButtons(List<UIButtonControlBase.ButtonData> buttonData)
	{
		this.stretchVertical = true;
		this.alignments.verticalAlignment = UIElement.Alignments.VerticalAlignments.Top;
		base.setButtons(buttonData);
	}
}
