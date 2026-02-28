using System;
using System.Collections.Generic;

// Token: 0x02000141 RID: 321
public class UISheetTabButtonControl : UITextButtonControlHorizontalBase
{
	// Token: 0x06001280 RID: 4736 RVA: 0x00051B1A File Offset: 0x0004FD1A
	public UISheetTabButtonControl(int x, int y, int width, int height) : base(x, y, width, height, 0)
	{
	}

	// Token: 0x06001281 RID: 4737 RVA: 0x00051B28 File Offset: 0x0004FD28
	protected override UIButtonControlBase.UITextButton createButton()
	{
		UIButtonControlBase.UITextButton uitextButton = new UIButtonControlBase.UITextButton(0, 0);
		uitextButton.foregroundColors.hoverColor = C64Color.Yellow;
		uitextButton.foregroundColors.leftClickedColor = C64Color.Gray;
		uitextButton.foregroundColors.shadowMainColor = C64Color.Black;
		uitextButton.alignments.horizontalAlignment = UIElement.Alignments.HorizontalAlignments.Center;
		uitextButton.padding.top = 4;
		uitextButton.padding.left = 2;
		uitextButton.padding.right = 2;
		uitextButton.backgroundTexture = UISheetTabButtonControl.tabDark;
		uitextButton.setWidth(uitextButton.backgroundTexture.width);
		return uitextButton;
	}

	// Token: 0x06001282 RID: 4738 RVA: 0x00051BB8 File Offset: 0x0004FDB8
	public void setButtons(List<string> stringList, int currentIndex)
	{
		while (base.getButtonsList().Count < stringList.Count)
		{
			UIButtonControlBase.UITextButton element = this.createButton();
			base.getNestedCanvas().add(element);
		}
		List<UIElement> buttonsList = base.getButtonsList();
		for (int i = 0; i < buttonsList.Count; i++)
		{
			UIButtonControlBase.UITextButton uitextButton = buttonsList[i] as UIButtonControlBase.UITextButton;
			if (i == currentIndex)
			{
				uitextButton.backgroundTexture = UISheetTabButtonControl.tabLight;
				uitextButton.foregroundColors.mainColor = C64Color.White;
			}
			else
			{
				uitextButton.backgroundTexture = UISheetTabButtonControl.tabDark;
				uitextButton.foregroundColors.mainColor = C64Color.GrayLight;
			}
			if (i < stringList.Count && stringList[i] != "")
			{
				uitextButton.setContent(stringList[i]);
			}
			else
			{
				uitextButton.setContent();
			}
		}
	}

	// Token: 0x04000479 RID: 1145
	private static TextureTools.TextureData tabDark = TextureTools.loadTextureData("Images/GUIIcons/SheetTabDark");

	// Token: 0x0400047A RID: 1146
	private static TextureTools.TextureData tabLight = TextureTools.loadTextureData("Images/GUIIcons/SheetTabLight");
}
