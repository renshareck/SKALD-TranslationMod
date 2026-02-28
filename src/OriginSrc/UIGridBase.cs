using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000155 RID: 341
public abstract class UIGridBase : UICanvasVertical
{
	// Token: 0x0600131A RID: 4890 RVA: 0x00054648 File Offset: 0x00052848
	protected UIGridBase(int width, int height)
	{
		this.width = width;
		this.height = height;
		this.stretchHorizontal = true;
		this.stretchVertical = true;
		for (int i = 0; i < height; i++)
		{
			this.add(new UIGridBase.UIGridRow(width));
		}
	}

	// Token: 0x0600131B RID: 4891 RVA: 0x00054690 File Offset: 0x00052890
	public int getButtonPressIndexDoubleClicked()
	{
		int num = 0;
		foreach (UIElement uielement in base.getElements())
		{
			UIGridBase.UIGridRow uigridRow = (UIGridBase.UIGridRow)uielement;
			if (uigridRow.getButtonPressIndexDoubleClicked() != -1)
			{
				return uigridRow.getButtonPressIndexDoubleClicked() + num * this.width;
			}
			num++;
		}
		return -1;
	}

	// Token: 0x0600131C RID: 4892 RVA: 0x00054708 File Offset: 0x00052908
	public void setMagicFlash(short flash)
	{
		foreach (UIElement uielement in base.getElements())
		{
			((UIGridBase.UIGridRow)uielement).setMagicFlash(flash);
		}
	}

	// Token: 0x0600131D RID: 4893 RVA: 0x00054760 File Offset: 0x00052960
	public int getButtonPressIndexLeft()
	{
		int num = 0;
		foreach (UIElement uielement in base.getElements())
		{
			UIGridBase.UIGridRow uigridRow = (UIGridBase.UIGridRow)uielement;
			if (uigridRow.getButtonPressIndexLeft() != -1)
			{
				return uigridRow.getButtonPressIndexLeft() + num * this.width;
			}
			num++;
		}
		return -1;
	}

	// Token: 0x0600131E RID: 4894 RVA: 0x000547D8 File Offset: 0x000529D8
	public int getHoverIndex()
	{
		int num = 0;
		foreach (UIElement uielement in base.getElements())
		{
			UIGridBase.UIGridRow uigridRow = (UIGridBase.UIGridRow)uielement;
			if (uigridRow.getHoverIndex() != -1)
			{
				return uigridRow.getHoverIndex() + num * this.width;
			}
			num++;
		}
		return -1;
	}

	// Token: 0x0600131F RID: 4895 RVA: 0x00054850 File Offset: 0x00052A50
	public void setBackgroundPaths(string ordinary, string hover, string clicked)
	{
		foreach (UIElement uielement in base.getElements())
		{
			((UIGridBase.UIGridRow)uielement).setBackgroundPaths(ordinary, hover, clicked);
		}
	}

	// Token: 0x06001320 RID: 4896 RVA: 0x000548A8 File Offset: 0x00052AA8
	public int getButtonPressIndexRight()
	{
		int num = 0;
		foreach (UIElement uielement in base.getElements())
		{
			UIGridBase.UIGridRow uigridRow = (UIGridBase.UIGridRow)uielement;
			if (uigridRow.getButtonPressIndexRight() != -1)
			{
				return uigridRow.getButtonPressIndexRight() + num * this.width;
			}
			num++;
		}
		return -1;
	}

	// Token: 0x06001321 RID: 4897 RVA: 0x00054920 File Offset: 0x00052B20
	private void resetAllButtonImages()
	{
		foreach (UIElement uielement in base.getElements())
		{
			((UIGridBase.UIGridRow)uielement).resetButtonImages();
		}
	}

	// Token: 0x06001322 RID: 4898 RVA: 0x00054978 File Offset: 0x00052B78
	public virtual void update()
	{
		foreach (UIElement uielement in base.getElements())
		{
			((UIGridBase.UIGridRow)uielement).update();
		}
		this.resetAllButtonImages();
	}

	// Token: 0x06001323 RID: 4899 RVA: 0x000549D4 File Offset: 0x00052BD4
	public List<UIElement> getScrollableElements(int column)
	{
		List<UIElement> list = new List<UIElement>();
		foreach (UIElement uielement in base.getElements())
		{
			UIGridBase.UIGridRow uigridRow = (UIGridBase.UIGridRow)uielement;
			list.Add(uigridRow.getScrollableElements()[column]);
		}
		return list;
	}

	// Token: 0x06001324 RID: 4900 RVA: 0x00054A40 File Offset: 0x00052C40
	public int getScrollableElementColumn()
	{
		foreach (UIElement uielement in base.getElements())
		{
			List<UIElement> scrollableElements = ((UIGridBase.UIGridRow)uielement).getScrollableElements();
			for (int i = 0; i < scrollableElements.Count; i++)
			{
				if (scrollableElements[i].getHover())
				{
					return i;
				}
			}
		}
		return -1;
	}

	// Token: 0x040004AC RID: 1196
	protected int width;

	// Token: 0x040004AD RID: 1197
	protected int height;

	// Token: 0x02000298 RID: 664
	protected class UIGridRow : UIButtonControlHorizontal
	{
		// Token: 0x06001AC3 RID: 6851 RVA: 0x000739D8 File Offset: 0x00071BD8
		public UIGridRow(int buttonNumber) : base(0, 0, 0, 0, buttonNumber)
		{
			base.getNestedCanvas().stretchHorizontal = true;
			base.getNestedCanvas().stretchVertical = true;
			UIGridBase.UIGridRow.backgroundOrdinary = TextureTools.loadTextureData(this.backgroundPathOridinary);
			UIGridBase.UIGridRow.backgroundHover = TextureTools.loadTextureData(this.backgroundPathHover);
			UIGridBase.UIGridRow.backgroundClicked = TextureTools.loadTextureData(this.backgroundPathClicked);
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x00073A5C File Offset: 0x00071C5C
		public void setBackgroundPaths(string ordinary, string hover, string clicked)
		{
			this.backgroundPathOridinary = ordinary;
			this.backgroundPathHover = hover;
			this.backgroundPathClicked = clicked;
			UIGridBase.UIGridRow.backgroundOrdinary = TextureTools.loadTextureData(this.backgroundPathOridinary);
			UIGridBase.UIGridRow.backgroundHover = TextureTools.loadTextureData(this.backgroundPathHover);
			UIGridBase.UIGridRow.backgroundClicked = TextureTools.loadTextureData(this.backgroundPathClicked);
			this.resetButtonImages();
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x00073AB4 File Offset: 0x00071CB4
		protected override void populateButtons()
		{
			for (int i = 0; i < this.buttonNumber; i++)
			{
				UIButtonControlBase.UITextButton element = this.createButton();
				base.getNestedCanvas().add(element);
			}
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x00073AE8 File Offset: 0x00071CE8
		protected UIButtonControlBase.UITextButton createButton()
		{
			UIGridBase.UIGridRow.UIGridButton uigridButton = new UIGridBase.UIGridRow.UIGridButton();
			uigridButton.stretchVertical = false;
			uigridButton.stretchHorizontal = false;
			uigridButton.backgroundTexture = TextureTools.loadTextureData(this.backgroundPathOridinary);
			uigridButton.setWidth(uigridButton.backgroundTexture.width - 1);
			uigridButton.setHeight(uigridButton.backgroundTexture.height - 1);
			return uigridButton;
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x00073B40 File Offset: 0x00071D40
		public void resetButtonImages()
		{
			foreach (UIElement uielement in base.getButtonsList())
			{
				UIButtonControlBase.UITextButton uitextButton = (UIButtonControlBase.UITextButton)uielement;
				if (uitextButton.getRightDown() || uitextButton.getLeftDown())
				{
					TextureTools.applyOverlay(uitextButton.backgroundTexture, UIGridBase.UIGridRow.backgroundClicked);
				}
				else if (uitextButton.getHover())
				{
					TextureTools.applyOverlay(uitextButton.backgroundTexture, UIGridBase.UIGridRow.backgroundHover);
				}
				else
				{
					TextureTools.applyOverlay(uitextButton.backgroundTexture, UIGridBase.UIGridRow.backgroundOrdinary);
				}
			}
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x00073BE0 File Offset: 0x00071DE0
		public void setMagicFlash(short i)
		{
			this.magicFlash = i;
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x00073BEC File Offset: 0x00071DEC
		public void setButton(Character character, Item item, int index, string backupPath)
		{
			List<UIElement> buttonsList = base.getButtonsList();
			if (index >= buttonsList.Count || index < 0)
			{
				return;
			}
			UIElement uielement = buttonsList[index];
			SkaldBaseObject currentObject = character.getInventory().getCurrentObject();
			TextureTools.TextureData textureData;
			if (item == null)
			{
				textureData = TextureTools.loadTextureData(backupPath);
				if (uielement.getHover())
				{
					textureData.clearToColorIfNotBlack(C64Color.Brown);
				}
			}
			else
			{
				textureData = item.getGridIcon();
				Item currentObject2 = character.getInventory().getCurrentObject();
				if (uielement.getLeftUp() && currentObject2 != item && !ToolTipPrinter.isMouseOverTooltip())
				{
					character.getInventory().setCurrentObject(item);
				}
				else if (SkaldIO.isControllerConnected() && uielement.getLeftUp() && currentObject2 == item && !ToolTipPrinter.isMouseOverTooltip())
				{
					character.getInventory().setCurrentObject(item);
					character.useCurrentItem();
				}
				else if (uielement.getDoubleClicked() && currentObject2 == item && !ToolTipPrinter.isMouseOverTooltip())
				{
					character.getInventory().setCurrentObject(item);
					character.useCurrentItem();
				}
				else if (uielement.getRightUp())
				{
					ToolTipPrinter.setToolTipWithRules(item.printComparativeStats(MainControl.getDataControl().getCurrentPC()));
				}
			}
			if (item != null && item == currentObject)
			{
				TextureTools.applyOverlay(uielement.backgroundTexture, item.getGridIconOutline(), 1, 1);
			}
			if (item != null && item.isMagical() && this.magicFlash > 0)
			{
				this.magicFlash -= 1;
				textureData.clearToColorIfNotBlack(ParticleSystem.positiveColors[Random.Range(0, ParticleSystem.positiveColors.Length - 1)]);
			}
			TextureTools.applyOverlay(uielement.backgroundTexture, textureData, 2, 2);
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x00073D60 File Offset: 0x00071F60
		public void setButtons(List<Item> itemList, Item currentObject, Character character)
		{
			List<UIElement> buttonsList = base.getButtonsList();
			int num = 0;
			foreach (SkaldBaseObject skaldBaseObject in itemList)
			{
				if (num < buttonsList.Count)
				{
					Item item = skaldBaseObject as Item;
					item.setInventoryPosition(buttonsList[num].getX(), buttonsList[num].getY());
					if (character != null && !character.isItemLegalToEquip(item, false).wasSuccess())
					{
						TextureTools.applyOverlay(buttonsList[num].backgroundTexture, UIGridBase.UIGridRow.legalityTexture, 2, 2);
					}
					if (skaldBaseObject == currentObject)
					{
						TextureTools.applyOverlay(buttonsList[num].backgroundTexture, item.getGridIconOutline(), 1, 1);
					}
					TextureTools.TextureData gridIcon = skaldBaseObject.getGridIcon();
					if (item != null && item.isMagical() && this.magicFlash > 0)
					{
						this.magicFlash -= 1;
						gridIcon.clearToColorIfNotBlack(ParticleSystem.positiveColors[Random.Range(0, ParticleSystem.positiveColors.Length - 1)]);
					}
					TextureTools.applyOverlay(buttonsList[num].backgroundTexture, gridIcon, 2, 2);
				}
				num++;
			}
		}

		// Token: 0x040009B8 RID: 2488
		private string backgroundPathOridinary = "Images/GUIIcons/InventoryUI/MenuBarBox";

		// Token: 0x040009B9 RID: 2489
		private string backgroundPathHover = "Images/GUIIcons/InventoryUI/MenuBarBoxHover";

		// Token: 0x040009BA RID: 2490
		private string backgroundPathClicked = "Images/GUIIcons/InventoryUI/MenuBarBoxRightClick";

		// Token: 0x040009BB RID: 2491
		private static TextureTools.TextureData backgroundOrdinary = null;

		// Token: 0x040009BC RID: 2492
		private static TextureTools.TextureData backgroundHover = null;

		// Token: 0x040009BD RID: 2493
		private static TextureTools.TextureData backgroundClicked = null;

		// Token: 0x040009BE RID: 2494
		private static TextureTools.TextureData legalityTexture = TextureTools.loadTextureData("Images/GUIIcons/InventoryUI/MenuBarBoxLegality");

		// Token: 0x040009BF RID: 2495
		private short magicFlash;

		// Token: 0x020003DA RID: 986
		protected class UIGridButton : UIButtonControlBase.UITextButton
		{
			// Token: 0x06001D84 RID: 7556 RVA: 0x0007C771 File Offset: 0x0007A971
			public UIGridButton() : base(0, 0, 0, 0, C64Color.Cyan, FontContainer.getTinyFont())
			{
			}

			// Token: 0x06001D85 RID: 7557 RVA: 0x0007C787 File Offset: 0x0007A987
			public override bool useableAsScrollableElement()
			{
				return true;
			}
		}
	}
}
