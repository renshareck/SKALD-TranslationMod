using System;
using System.Collections.Generic;

// Token: 0x02000133 RID: 307
public class UISpellbookSelectorGrid : UIAbilitySelectorGrid
{
	// Token: 0x06001215 RID: 4629 RVA: 0x00050296 File Offset: 0x0004E496
	public UISpellbookSelectorGrid(List<UIButtonControlBase.ButtonData> buttonData, int height) : base(buttonData, 0, 0, 11)
	{
		this.padding.left = 3;
		this.setHeight(height + 3);
	}

	// Token: 0x06001216 RID: 4630 RVA: 0x000502B8 File Offset: 0x0004E4B8
	protected override void setButtons(List<UIButtonControlBase.ButtonData> buttonData)
	{
		this.stretchVertical = false;
		this.alignments.verticalAlignment = UIElement.Alignments.VerticalAlignments.Top;
		base.setButtons(buttonData);
	}
}
