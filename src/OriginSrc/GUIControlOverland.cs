using System;
using System.Collections.Generic;

// Token: 0x0200011A RID: 282
public class GUIControlOverland : NumericListClass
{
	// Token: 0x060011E5 RID: 4581 RVA: 0x0004FD95 File Offset: 0x0004DF95
	public override void setCombatOrderButtonRow(List<UIButtonControlBase.ButtonData> options)
	{
		if (this.combatButtonRow == null)
		{
			this.combatButtonRow = new UIImageCombatButtonControl(129, 200, 16, options.Count);
		}
		this.combatButtonRow.setButtons(options);
	}

	// Token: 0x060011E6 RID: 4582 RVA: 0x0004FDC8 File Offset: 0x0004DFC8
	public override void setAbilitySelectorGrid(UIAbilitySelectorGrid grid)
	{
		this.abilitySelectorGrid = grid;
	}
}
