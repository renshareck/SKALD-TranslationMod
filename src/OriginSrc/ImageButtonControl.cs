using System;
using System.Collections.Generic;

// Token: 0x0200014C RID: 332
public class ImageButtonControl : UIButtonControlVerticalText
{
	// Token: 0x060012AA RID: 4778 RVA: 0x0005298C File Offset: 0x00050B8C
	public ImageButtonControl(int x, int y, int width, int height, string imagePath) : base(x, y, width, height, 0)
	{
		this.imagePath += imagePath;
		this.hoverImagePath = this.imagePath + "Hover";
		this.pressedImagePath = this.imagePath + "Pressed";
	}

	// Token: 0x060012AB RID: 4779 RVA: 0x00052A08 File Offset: 0x00050C08
	protected override UIButtonControlBase.UITextButton createButton()
	{
		UIButtonControlBase.UITextButton uitextButton = new UIButtonControlBase.UITextButton(0, 0, 9, 9, C64Color.GrayLight);
		uitextButton.padding.bottom = 2;
		uitextButton.backgroundTexture = TextureTools.loadTextureData(this.imagePath);
		uitextButton.backgroundTextureHover = TextureTools.loadTextureData(this.hoverImagePath);
		uitextButton.backgroundTexturePressed = TextureTools.loadTextureData(this.pressedImagePath);
		uitextButton.setAllowDoubleClick(false);
		return uitextButton;
	}

	// Token: 0x060012AC RID: 4780 RVA: 0x00052A6C File Offset: 0x00050C6C
	public virtual void setButtons(SkaldDataList dataList)
	{
		while (base.getButtonsList().Count < dataList.getCount() - 1)
		{
			UIButtonControlBase.UITextButton element = this.createButton();
			base.getNestedCanvas().add(element);
		}
	}

	// Token: 0x060012AD RID: 4781 RVA: 0x00052AA4 File Offset: 0x00050CA4
	public override List<UIElement> getScrollableElements()
	{
		List<UIElement> list = new List<UIElement>();
		foreach (UIElement item in base.getNestedCanvas().getElements())
		{
			list.Add(item);
		}
		return list;
	}

	// Token: 0x04000483 RID: 1155
	private string imagePath = "Images/GUIIcons/";

	// Token: 0x04000484 RID: 1156
	private string hoverImagePath = "";

	// Token: 0x04000485 RID: 1157
	private string pressedImagePath = "";
}
