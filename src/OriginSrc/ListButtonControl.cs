using System;
using UnityEngine;

// Token: 0x0200014A RID: 330
public class ListButtonControl : UIButtonControlVerticalText
{
	// Token: 0x060012A1 RID: 4769 RVA: 0x00052617 File Offset: 0x00050817
	public ListButtonControl(int x, int y, int width, int height, int buttonNumber) : base(x, y, width, height, buttonNumber)
	{
	}

	// Token: 0x060012A2 RID: 4770 RVA: 0x00052639 File Offset: 0x00050839
	public ListButtonControl(int x, int y, int width, int height) : base(x, y, width, height, 0)
	{
	}

	// Token: 0x060012A3 RID: 4771 RVA: 0x0005265C File Offset: 0x0005085C
	protected override UIButtonControlBase.UITextButton createButton()
	{
		UIButtonControlBase.UITextButton uitextButton = new UIButtonControlBase.UITextButton(0, 0, this.getBaseWidth(), 0, C64Color.GrayLight);
		if (this.centerButtonText)
		{
			uitextButton.setAlignment(new UIElement.Alignments(UIElement.Alignments.VerticalAlignments.Top, UIElement.Alignments.HorizontalAlignments.Center));
			uitextButton.stretchHorizontal = true;
		}
		uitextButton.setSingleLine();
		uitextButton.stretchHorizontal = true;
		uitextButton.foregroundColors.hoverColor = C64Color.White;
		uitextButton.foregroundColors.leftClickedColor = C64Color.Gray;
		uitextButton.foregroundColors.shadowMainColor = this.shadowColor;
		uitextButton.padding.bottom = 2;
		uitextButton.setTabWidth(this.tabWidth);
		return uitextButton;
	}

	// Token: 0x060012A4 RID: 4772 RVA: 0x000526F0 File Offset: 0x000508F0
	public void setButtonTextShadowColor(Color32 color)
	{
		this.shadowColor = color;
	}

	// Token: 0x060012A5 RID: 4773 RVA: 0x000526F9 File Offset: 0x000508F9
	public void toggleCenterText()
	{
		this.centerButtonText = true;
	}

	// Token: 0x060012A6 RID: 4774 RVA: 0x00052704 File Offset: 0x00050904
	public void setTabWidth(int width)
	{
		this.tabWidth = width;
		foreach (UIElement uielement in base.getButtonsList())
		{
			((UIButtonControlBase.UITextButton)uielement).setTabWidth(this.tabWidth);
		}
	}

	// Token: 0x060012A7 RID: 4775 RVA: 0x00052768 File Offset: 0x00050968
	public virtual void setButtons(SkaldDataList dataList)
	{
		int num = 0;
		while (base.getButtonsList().Count < dataList.getCount())
		{
			UIButtonControlBase.UITextButton element = this.createButton();
			base.getNestedCanvas().add(element);
		}
		foreach (UIElement uielement in base.getButtonsList())
		{
			UIButtonControlBase.UITextButton uitextButton = (UIButtonControlBase.UITextButton)uielement;
			if (num < dataList.getObjectList().Count)
			{
				uitextButton.setContent(dataList.getObjectList()[num].getListName());
				if (uitextButton.getElements().Count > 0 && dataList.getObjectList()[num].getImagePath() != "")
				{
					uitextButton.getElements()[0].backgroundTexture = TextureTools.loadTextureData(dataList.getObjectList()[num].getImagePath());
					if (uitextButton.getElements()[0].backgroundTexture != null)
					{
						uitextButton.getElements()[0].padding.left = 10;
					}
				}
				if (num != 0)
				{
					uitextButton.backgroundColors.hoverColor = C64Color.Red;
				}
			}
			else
			{
				uitextButton.setContent();
				uitextButton.backgroundColors.hoverColor = Color.clear;
			}
			num++;
		}
		this.alignElements();
	}

	// Token: 0x0400047D RID: 1149
	private int tabWidth = 68;

	// Token: 0x0400047E RID: 1150
	private Color32 shadowColor = C64Color.HighlightedSmallTextShadowColor;

	// Token: 0x0400047F RID: 1151
	private bool centerButtonText;
}
