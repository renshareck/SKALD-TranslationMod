using System;
using System.Collections.Generic;

// Token: 0x02000135 RID: 309
public class UICombatAbilitySelectorGrid : UIAbilitySelectorGrid
{
	// Token: 0x0600121A RID: 4634 RVA: 0x00050324 File Offset: 0x0004E524
	public UICombatAbilitySelectorGrid(List<UIButtonControlBase.ButtonData> buttonData, int x) : base(buttonData, x + 20, 19, 3)
	{
		this.arrow = new UIImage("Images/GUIIcons/MiscCombatUI/AbilityMenuArrow");
		this.arrow.stretchVertical = true;
		this.textBlock = new UITextBlock(0, 0, 0, 8, C64Color.White);
		this.textBlock.padding.left = 1;
		this.textBlock.stretchHorizontal = true;
		this.textBlock.stretchVertical = true;
		if (buttonData.Count == 0)
		{
			this.setTextBlock("-Empty-");
		}
		else
		{
			this.setTextBlock("Select Ability!");
		}
		this.setButtons(buttonData);
	}

	// Token: 0x0600121B RID: 4635 RVA: 0x000503BF File Offset: 0x0004E5BF
	protected override void setButtons(List<UIButtonControlBase.ButtonData> buttonData)
	{
		this.clearElements();
		this.add(this.textBlock);
		base.addMainGrid(buttonData);
		this.add(this.arrow);
		this.alignElements();
	}

	// Token: 0x0600121C RID: 4636 RVA: 0x000503EC File Offset: 0x0004E5EC
	public void setTextBlock(string input)
	{
		this.textBlock.setContent(input);
		this.textBlock.setLetterShadowColor(C64Color.Black);
	}

	// Token: 0x04000450 RID: 1104
	private UIImage arrow;

	// Token: 0x04000451 RID: 1105
	private UITextBlock textBlock;
}
