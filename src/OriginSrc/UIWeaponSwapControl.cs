using System;
using System.Collections.Generic;

// Token: 0x02000146 RID: 326
public class UIWeaponSwapControl : UIButtonControlHorizontal
{
	// Token: 0x06001292 RID: 4754 RVA: 0x00052080 File Offset: 0x00050280
	public UIWeaponSwapControl(int x, int y, int width, int height) : base(x, y, width, height, 2)
	{
	}

	// Token: 0x06001293 RID: 4755 RVA: 0x00052090 File Offset: 0x00050290
	protected override void populateButtons()
	{
		for (int i = 0; i < this.buttonNumber; i++)
		{
			UIWeaponSwapControl.UIImageButton element = this.createButton();
			base.getNestedCanvas().add(element);
		}
	}

	// Token: 0x06001294 RID: 4756 RVA: 0x000520C4 File Offset: 0x000502C4
	private UIWeaponSwapControl.UIImageButton createButton()
	{
		UIWeaponSwapControl.UIImageButton uiimageButton = new UIWeaponSwapControl.UIImageButton(100, 250, 19, 0);
		uiimageButton.padding.left = 2;
		uiimageButton.padding.top = 8;
		uiimageButton.alignments.horizontalAlignment = UIElement.Alignments.HorizontalAlignments.Center;
		uiimageButton.alignments.verticalAlignment = UIElement.Alignments.VerticalAlignments.Center;
		uiimageButton.setData();
		return uiimageButton;
	}

	// Token: 0x06001295 RID: 4757 RVA: 0x00052118 File Offset: 0x00050318
	public void setButtonsAndUpdate(Character character)
	{
		if (character == null)
		{
			return;
		}
		this.update();
		if (this.buttonPressIndexLeft == 0)
		{
			character.toggleMeleeWeapon();
		}
		else if (this.buttonPressIndexLeft == 1)
		{
			character.toggleRangedWeapon();
		}
		List<UIElement> buttonsList = base.getButtonsList();
		UIWeaponSwapControl.UIImageButton uiimageButton = buttonsList[0] as UIWeaponSwapControl.UIImageButton;
		UIWeaponSwapControl.UIImageButton uiimageButton2 = buttonsList[1] as UIWeaponSwapControl.UIImageButton;
		if (character.getCurrentRangedWeapon() != null)
		{
			uiimageButton.setData(character.getCurrentMeleeWeaponBaseIcon(), character.getCurrentWeapon() == null || (character.getCurrentWeapon() != null && !character.getCurrentWeapon().isRanged()));
			uiimageButton2.setData(character.getCurrentRangedWeaponBaseIcon(), character.getCurrentWeapon() != null && character.getCurrentWeapon().isRanged());
			return;
		}
		uiimageButton.setData();
		uiimageButton2.setData();
	}

	// Token: 0x0200028E RID: 654
	protected class UIImageButton : UIImage
	{
		// Token: 0x06001AAD RID: 6829 RVA: 0x00073612 File Offset: 0x00071812
		public UIImageButton(int x, int y, int width, int height) : base(x, y, width, height)
		{
			this.hoverText = "An option!";
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x0007362A File Offset: 0x0007182A
		public void setData()
		{
			this.setData(null, false);
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x00073634 File Offset: 0x00071834
		public void setData(TextureTools.TextureData icon, bool current)
		{
			string str = "Images/GUIIcons/CombatMenuButtons/";
			if (icon != null)
			{
				this.foregroundTexture = TextureTools.loadTextureData(str + "ItemButton");
				icon.applyOverlay(3, 3, this.foregroundTexture);
				if (current)
				{
					icon.getOutline(C64Color.Yellow).applyOverlay(2, 2, this.foregroundTexture);
					return;
				}
			}
			else
			{
				this.foregroundTexture = null;
			}
		}
	}
}
