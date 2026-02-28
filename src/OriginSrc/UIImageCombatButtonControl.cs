using System;
using System.Collections.Generic;

// Token: 0x02000145 RID: 325
public class UIImageCombatButtonControl : UIButtonControlHorizontal
{
	// Token: 0x0600128E RID: 4750 RVA: 0x00051F90 File Offset: 0x00050190
	public UIImageCombatButtonControl(int x, int width, int height, int buttonNumber) : base(x, 19, width, height, buttonNumber)
	{
		base.toggleKeyboardPressControl();
	}

	// Token: 0x0600128F RID: 4751 RVA: 0x00051FA8 File Offset: 0x000501A8
	protected override void populateButtons()
	{
		for (int i = 0; i < this.buttonNumber; i++)
		{
			UIImageCombatButtonControl.UIImageButton element = this.createButton(i + 1);
			base.getNestedCanvas().add(element);
		}
	}

	// Token: 0x06001290 RID: 4752 RVA: 0x00051FDC File Offset: 0x000501DC
	private UIImageCombatButtonControl.UIImageButton createButton(int number)
	{
		UIImageCombatButtonControl.UIImageButton uiimageButton = new UIImageCombatButtonControl.UIImageButton(100, 250, 19, 0, number);
		uiimageButton.padding.left = 0;
		uiimageButton.padding.top = 8;
		uiimageButton.alignments.horizontalAlignment = UIElement.Alignments.HorizontalAlignments.Center;
		uiimageButton.alignments.verticalAlignment = UIElement.Alignments.VerticalAlignments.Center;
		uiimageButton.setData();
		return uiimageButton;
	}

	// Token: 0x06001291 RID: 4753 RVA: 0x00052030 File Offset: 0x00050230
	public virtual void setButtons(List<UIButtonControlBase.ButtonData> buttonTextList)
	{
		List<UIElement> buttonsList = base.getButtonsList();
		for (int i = 0; i < buttonsList.Count; i++)
		{
			UIImageCombatButtonControl.UIImageButton uiimageButton = buttonsList[i] as UIImageCombatButtonControl.UIImageButton;
			if (i < buttonTextList.Count)
			{
				uiimageButton.setData(buttonTextList[i]);
			}
			else
			{
				uiimageButton.setData();
			}
		}
	}

	// Token: 0x0200028D RID: 653
	protected class UIImageButton : UIImage
	{
		// Token: 0x06001AA9 RID: 6825 RVA: 0x00073520 File Offset: 0x00071720
		public UIImageButton(int x, int y, int width, int height, int number) : base(x, y, width, height)
		{
			this.number = number;
			this.hoverText = "An option!";
			this.backgroundTexture = TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/MenuBarBox");
			this.backgroundTextureHover = TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/MenuBarBoxHover");
			this.foregroundTexture = TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/MenuBarBackground");
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x0007357B File Offset: 0x0007177B
		public void setData()
		{
			this.setData(null);
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x00073584 File Offset: 0x00071784
		public void setData(UIButtonControlBase.ButtonData buttonData)
		{
			this.setData(buttonData.texture);
			this.hoverText = buttonData.hoverText;
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x000735A0 File Offset: 0x000717A0
		private void setData(TextureTools.TextureData t)
		{
			TextureTools.applyOverlay(this.foregroundTexture, t, 2, 2);
			if (this.number > 0 && this.number < 10 && this.numberTexture == null)
			{
				this.numberTexture = TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/Digits/" + this.number.ToString());
			}
			if (this.numberTexture != null)
			{
				this.numberTexture.applyOverlay(2, 11, this.foregroundTexture);
			}
		}

		// Token: 0x040009A2 RID: 2466
		private int number;

		// Token: 0x040009A3 RID: 2467
		private TextureTools.TextureData numberTexture;
	}
}
