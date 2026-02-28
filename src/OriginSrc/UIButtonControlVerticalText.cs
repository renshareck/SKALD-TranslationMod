using System;
using System.Collections.Generic;

// Token: 0x02000147 RID: 327
public abstract class UIButtonControlVerticalText : UIButtonControlBase
{
	// Token: 0x06001296 RID: 4758 RVA: 0x000521D4 File Offset: 0x000503D4
	public UIButtonControlVerticalText(int x, int y, int width, int height, int buttonNumber) : base(x, y, width, height, buttonNumber)
	{
		this.add(new UICanvasVertical(x, y, width, height));
		this.populateButtons();
	}

	// Token: 0x06001297 RID: 4759 RVA: 0x000521FC File Offset: 0x000503FC
	protected override void populateButtons()
	{
		for (int i = 0; i < this.buttonNumber; i++)
		{
			UIButtonControlBase.UITextButton element = this.createButton();
			base.getNestedCanvas().add(element);
		}
	}

	// Token: 0x06001298 RID: 4760
	protected abstract UIButtonControlBase.UITextButton createButton();

	// Token: 0x06001299 RID: 4761 RVA: 0x00052230 File Offset: 0x00050430
	public virtual void setButtons(List<string> buttonTextList)
	{
		int num = 0;
		foreach (UIElement uielement in base.getButtonsList())
		{
			UIButtonControlBase.UITextButton uitextButton = (UIButtonControlBase.UITextButton)uielement;
			if (num < buttonTextList.Count)
			{
				uitextButton.setContent(buttonTextList[num]);
			}
			else
			{
				uitextButton.setContent();
			}
			num++;
		}
		this.alignElements();
	}
}
