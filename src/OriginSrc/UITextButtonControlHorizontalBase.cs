using System;
using System.Collections.Generic;

// Token: 0x02000142 RID: 322
public class UITextButtonControlHorizontalBase : UIButtonControlHorizontal
{
	// Token: 0x06001284 RID: 4740 RVA: 0x00051CA2 File Offset: 0x0004FEA2
	public UITextButtonControlHorizontalBase(int x, int y, int width, int height, int buttonNumber) : base(x, y, width, height, buttonNumber)
	{
		this.setAlignment(new UIElement.Alignments(UIElement.Alignments.VerticalAlignments.Top, UIElement.Alignments.HorizontalAlignments.Center));
	}

	// Token: 0x06001285 RID: 4741 RVA: 0x00051CC0 File Offset: 0x0004FEC0
	protected override void populateButtons()
	{
		for (int i = 0; i < this.buttonNumber; i++)
		{
			UIButtonControlBase.UITextButton element = this.createButton();
			base.getNestedCanvas().add(element);
		}
	}

	// Token: 0x06001286 RID: 4742 RVA: 0x00051CF4 File Offset: 0x0004FEF4
	public virtual void setButtonText(List<string> stringList)
	{
		List<UIElement> buttonsList = base.getButtonsList();
		for (int i = 0; i < buttonsList.Count; i++)
		{
			UIButtonControlBase.UITextButton uitextButton = buttonsList[i] as UIButtonControlBase.UITextButton;
			if (i < stringList.Count && stringList[i] != "")
			{
				if (this.controllerPressControl != null)
				{
					uitextButton.setContent(this.controllerPressControl.getButtonPrefix(i) + stringList[i]);
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

	// Token: 0x06001287 RID: 4743 RVA: 0x00051D7F File Offset: 0x0004FF7F
	protected virtual string getButtonBackgroundPath()
	{
		return "Images/GUIIcons/TechnicalButtonMedium";
	}

	// Token: 0x06001288 RID: 4744 RVA: 0x00051D88 File Offset: 0x0004FF88
	protected virtual UIButtonControlBase.UITextButton createButton()
	{
		UIButtonControlBase.UITextButton uitextButton = new UIButtonControlBase.UITextButton(0, 0, 0, 0, C64Color.Cyan, FontContainer.getTinyFont());
		uitextButton.foregroundColors.hoverColor = C64Color.Yellow;
		uitextButton.foregroundColors.leftClickedColor = C64Color.Gray;
		uitextButton.foregroundColors.shadowMainColor = C64Color.Black;
		uitextButton.backgroundTexture = TextureTools.loadTextureData(this.getButtonBackgroundPath());
		uitextButton.setWidth(uitextButton.backgroundTexture.width);
		uitextButton.padding.top = 3;
		uitextButton.padding.left = 1;
		uitextButton.padding.right = 0;
		uitextButton.alignments = new UIElement.Alignments(UIElement.Alignments.VerticalAlignments.Center, UIElement.Alignments.HorizontalAlignments.Center);
		uitextButton.setAllowDoubleClick(false);
		return uitextButton;
	}
}
