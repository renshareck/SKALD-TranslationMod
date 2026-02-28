using System;
using System.Collections.Generic;

// Token: 0x02000143 RID: 323
public class UIHorizontalMenuButtons : UITextButtonControlHorizontalBase
{
	// Token: 0x06001289 RID: 4745 RVA: 0x00051E32 File Offset: 0x00050032
	public UIHorizontalMenuButtons(int width, int height) : base(0, 0, width, height, 0)
	{
	}

	// Token: 0x0600128A RID: 4746 RVA: 0x00051E40 File Offset: 0x00050040
	protected override UIButtonControlBase.UITextButton createButton()
	{
		UIButtonControlBase.UITextButton uitextButton = new UIButtonControlBase.UITextButton(0, 0, 0, 0, C64Color.Cyan, FontContainer.getMediumFont());
		uitextButton.foregroundColors.hoverColor = C64Color.Yellow;
		uitextButton.foregroundColors.leftClickedColor = C64Color.Gray;
		uitextButton.foregroundColors.shadowMainColor = C64Color.Black;
		uitextButton.padding.top = 3;
		uitextButton.padding.left = 1;
		uitextButton.padding.right = 0;
		uitextButton.setWidth(60);
		uitextButton.alignments = new UIElement.Alignments(UIElement.Alignments.VerticalAlignments.Center, UIElement.Alignments.HorizontalAlignments.Center);
		return uitextButton;
	}

	// Token: 0x0600128B RID: 4747 RVA: 0x00051ECC File Offset: 0x000500CC
	public void setButtonText(List<string> stringList, int currentIndex)
	{
		List<UIElement> buttonsList = base.getButtonsList();
		while (buttonsList.Count < stringList.Count)
		{
			buttonsList.Add(this.createButton());
		}
		for (int i = 0; i < buttonsList.Count; i++)
		{
			UIButtonControlBase.UITextButton uitextButton = buttonsList[i] as UIButtonControlBase.UITextButton;
			if (i < stringList.Count && stringList[i] != "")
			{
				if (currentIndex == i)
				{
					uitextButton.setContent(C64Color.WHITE_TAG + stringList[i] + "</color>");
				}
				else
				{
					uitextButton.setContent(stringList[i]);
				}
			}
			else
			{
				uitextButton.setContent();
			}
		}
	}
}
