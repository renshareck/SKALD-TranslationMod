using System;
using System.Collections.Generic;

// Token: 0x02000132 RID: 306
public class UIAbilitySheetGrid : UIAbilitySelectorGrid
{
	// Token: 0x06001213 RID: 4627 RVA: 0x00050258 File Offset: 0x0004E458
	public UIAbilitySheetGrid(List<UIButtonControlBase.ButtonData> buttonData, int height) : base(buttonData, 0, 0, 11)
	{
		this.padding.left = 3;
		this.setHeight(height + 3);
	}

	// Token: 0x06001214 RID: 4628 RVA: 0x0005027A File Offset: 0x0004E47A
	protected override void setButtons(List<UIButtonControlBase.ButtonData> buttonData)
	{
		this.stretchVertical = false;
		this.alignments.verticalAlignment = UIElement.Alignments.VerticalAlignments.Top;
		base.setButtons(buttonData);
	}
}
