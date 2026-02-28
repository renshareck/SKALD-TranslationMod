using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200015B RID: 347
public abstract class UIInventorySheetBase : UICanvasHorizontal
{
	// Token: 0x06001335 RID: 4917 RVA: 0x00054EBC File Offset: 0x000530BC
	public UIInventorySheetBase()
	{
		this.stretchHorizontal = true;
		this.mainRow = new UICanvasVertical();
		this.add(this.mainRow);
		this.initialize();
	}

	// Token: 0x06001336 RID: 4918
	protected abstract void initialize();

	// Token: 0x06001337 RID: 4919 RVA: 0x00054EE8 File Offset: 0x000530E8
	public virtual string getBackgroundPath()
	{
		if (GlobalSettings.getFontSettings().getTextBackgroundColor() == 2)
		{
			return "Images/GUIIcons/SheetInventoryBackgroundBlack";
		}
		if (GlobalSettings.getFontSettings().getTextBackgroundColor() == 1)
		{
			return "Images/GUIIcons/SheetInventoryBackgroundBrown";
		}
		return "Images/GUIIcons/SheetInventoryBackground";
	}

	// Token: 0x06001338 RID: 4920 RVA: 0x00054F18 File Offset: 0x00053118
	public virtual void update(SkaldInstanceObject obj, Inventory mainInv, Inventory secondaryInventory, Character currentCharacter)
	{
		if (this.filterButtons != null)
		{
			this.filterButtons.update();
		}
		if (this.itemInteractionGrid != null)
		{
			this.itemInteractionGrid.update(obj);
		}
		if (this.mainInventoryGrid != null)
		{
			this.mainInventoryGrid.update(mainInv, this.getTypeFilter(), currentCharacter);
		}
		if (obj is Character)
		{
			Party mainParty = (obj as Character).getMainParty();
			if (mainParty != null && this.goldWeightBlock != null)
			{
				this.goldWeightBlock.setContent(mainInv.printGold() + "    " + mainParty.printWeight());
			}
		}
		if (this.secondaryInventoryGrid != null)
		{
			this.secondaryInventoryGrid.update(secondaryInventory, this.getTypeFilter(), currentCharacter);
		}
	}

	// Token: 0x06001339 RID: 4921 RVA: 0x00054FC4 File Offset: 0x000531C4
	private UICanvas getControllerFocusSurface()
	{
		if (this.currentControllerSurface == null)
		{
			this.currentControllerSurface = this.mainInventoryGrid;
		}
		return this.currentControllerSurface;
	}

	// Token: 0x0600133A RID: 4922 RVA: 0x00054FE0 File Offset: 0x000531E0
	public override List<UIElement> getScrollableElements()
	{
		return this.getControllerFocusSurface().getScrollableElements();
	}

	// Token: 0x0600133B RID: 4923 RVA: 0x00054FED File Offset: 0x000531ED
	public override void incrementCurrentSelectedButton()
	{
		this.getControllerFocusSurface().canControllerScrollRight();
		base.incrementCurrentSelectedButton();
	}

	// Token: 0x0600133C RID: 4924 RVA: 0x00055001 File Offset: 0x00053201
	public override void decrementCurrentSelectedButton()
	{
		this.getControllerFocusSurface().canControllerScrollRight();
		base.decrementCurrentSelectedButton();
	}

	// Token: 0x0600133D RID: 4925 RVA: 0x00055015 File Offset: 0x00053215
	public override void controllerScrollSidewaysRight()
	{
		if (this.getControllerFocusSurface().canControllerScrollRight())
		{
			base.setCurrentSelectedButtonIndexToHoveredElement();
			this.getControllerFocusSurface().controllerScrollSidewaysRight();
			return;
		}
		this.scrollRightToControllerSurface();
	}

	// Token: 0x0600133E RID: 4926 RVA: 0x0005503C File Offset: 0x0005323C
	public override void controllerScrollSidewaysLeft()
	{
		if (this.getControllerFocusSurface().canControllerScrollLeft())
		{
			base.setCurrentSelectedButtonIndexToHoveredElement();
			this.getControllerFocusSurface().controllerScrollSidewaysLeft();
			return;
		}
		this.scrollLeftToControllerSurface();
	}

	// Token: 0x0600133F RID: 4927 RVA: 0x00055063 File Offset: 0x00053263
	public virtual void scrollRightToControllerSurface()
	{
		if (this.getControllerFocusSurface() == this.mainInventoryGrid && this.secondaryInventoryGrid != null)
		{
			this.currentControllerSurface = this.secondaryInventoryGrid;
		}
	}

	// Token: 0x06001340 RID: 4928 RVA: 0x00055087 File Offset: 0x00053287
	public virtual void scrollLeftToControllerSurface()
	{
		this.currentControllerSurface = this.mainInventoryGrid;
	}

	// Token: 0x06001341 RID: 4929 RVA: 0x00055095 File Offset: 0x00053295
	public UIScrollbar getControllerSurfaceScrollbar()
	{
		if (this.getControllerFocusSurface() is UIInventorySheetBase.UIGridCharacterInventorySegment)
		{
			return (this.getControllerFocusSurface() as UIInventorySheetBase.UIGridCharacterInventorySegment).getScrollbar();
		}
		return null;
	}

	// Token: 0x06001342 RID: 4930 RVA: 0x000550B6 File Offset: 0x000532B6
	public void shiftFilterIndex(int i)
	{
		if (this.filterButtons != null)
		{
			this.filterButtons.shiftFilterIndex(i);
		}
	}

	// Token: 0x06001343 RID: 4931 RVA: 0x000550CC File Offset: 0x000532CC
	public virtual List<Item.ItemTypes> getTypeFilter()
	{
		return this.filterButtons.getTypeFilter();
	}

	// Token: 0x06001344 RID: 4932 RVA: 0x000550D9 File Offset: 0x000532D9
	public Item getMainLeftClickedInteractItem()
	{
		if (this.mainInventoryGrid != null)
		{
			return this.mainInventoryGrid.getLeftClickedInteractItem();
		}
		return null;
	}

	// Token: 0x06001345 RID: 4933 RVA: 0x000550F0 File Offset: 0x000532F0
	public Item getMainDoubleClickedInteractItem()
	{
		if (this.mainInventoryGrid != null)
		{
			return this.mainInventoryGrid.getDoubleClickedInteractItem();
		}
		return null;
	}

	// Token: 0x06001346 RID: 4934 RVA: 0x00055107 File Offset: 0x00053307
	public Item getMainRightClickedInteractItem()
	{
		if (this.mainInventoryGrid != null)
		{
			return this.mainInventoryGrid.getRightClickedInteractItem();
		}
		return null;
	}

	// Token: 0x06001347 RID: 4935 RVA: 0x0005511E File Offset: 0x0005331E
	public Item getSecondaryLeftClickedInteractItem()
	{
		if (this.secondaryInventoryGrid != null)
		{
			return this.secondaryInventoryGrid.getLeftClickedInteractItem();
		}
		return null;
	}

	// Token: 0x06001348 RID: 4936 RVA: 0x00055135 File Offset: 0x00053335
	public Item getSecondaryDoubleClickedInteractItem()
	{
		if (this.secondaryInventoryGrid != null)
		{
			return this.secondaryInventoryGrid.getDoubleClickedInteractItem();
		}
		return null;
	}

	// Token: 0x06001349 RID: 4937 RVA: 0x0005514C File Offset: 0x0005334C
	public Item getSecondaryRightClickedInteractItem()
	{
		if (this.secondaryInventoryGrid != null)
		{
			return this.secondaryInventoryGrid.getRightClickedInteractItem();
		}
		return null;
	}

	// Token: 0x0600134A RID: 4938 RVA: 0x00055163 File Offset: 0x00053363
	public Item getWornLeftClickedInteractItem()
	{
		if (this.itemInteractionGrid != null)
		{
			return this.itemInteractionGrid.getLeftClickedInteractItem();
		}
		return null;
	}

	// Token: 0x0600134B RID: 4939 RVA: 0x0005517A File Offset: 0x0005337A
	public Item getWornRightClickedInteractItem()
	{
		if (this.itemInteractionGrid != null)
		{
			return this.itemInteractionGrid.getRightClickedInteractItem();
		}
		return null;
	}

	// Token: 0x0600134C RID: 4940 RVA: 0x00055191 File Offset: 0x00053391
	public void setInfoBlock(string content)
	{
		if (this.itemInteractionGrid != null)
		{
			this.itemInteractionGrid.setInfoBlock(content);
		}
	}

	// Token: 0x040004B3 RID: 1203
	protected UIInventorySheetBase.FilterButtons filterButtons;

	// Token: 0x040004B4 RID: 1204
	protected UICanvasVertical mainRow;

	// Token: 0x040004B5 RID: 1205
	protected UIInventorySheetBase.ItemInteractionUI itemInteractionGrid;

	// Token: 0x040004B6 RID: 1206
	protected UIInventorySheetBase.UIGridCharacterInventorySegment mainInventoryGrid;

	// Token: 0x040004B7 RID: 1207
	protected UIInventorySheetBase.UIGridCharacterInventorySegment secondaryInventoryGrid;

	// Token: 0x040004B8 RID: 1208
	protected UICanvas currentControllerSurface;

	// Token: 0x040004B9 RID: 1209
	protected UITextBlock goldWeightBlock;

	// Token: 0x0200029A RID: 666
	protected abstract class UIInventorySegment : UICanvasHorizontal
	{
		// Token: 0x06001ACF RID: 6863 RVA: 0x00074063 File Offset: 0x00072263
		public Item getLeftClickedInteractItem()
		{
			return this.leftClickedInteractItem;
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x0007406B File Offset: 0x0007226B
		public Item getDoubleClickedInteractItem()
		{
			return this.doubleClickedInteractItem;
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x00074073 File Offset: 0x00072273
		public Item getRightClickedInteractItem()
		{
			return this.rightClickedInteractItem;
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x0007407B File Offset: 0x0007227B
		protected void clearInteractItems()
		{
			this.leftClickedInteractItem = null;
			this.rightClickedInteractItem = null;
			this.doubleClickedInteractItem = null;
		}

		// Token: 0x040009C7 RID: 2503
		protected Item leftClickedInteractItem;

		// Token: 0x040009C8 RID: 2504
		protected Item doubleClickedInteractItem;

		// Token: 0x040009C9 RID: 2505
		protected Item rightClickedInteractItem;
	}

	// Token: 0x0200029B RID: 667
	protected class UIGridCharacterInventorySegment : UIInventorySheetBase.UIInventorySegment
	{
		// Token: 0x06001AD4 RID: 6868 RVA: 0x0007409C File Offset: 0x0007229C
		public UIGridCharacterInventorySegment(int width, int height)
		{
			this.stretchHorizontal = true;
			this.stretchVertical = true;
			this.grid = new UIGridInventory(width, height);
			this.gridWidth = width;
			this.add(this.grid);
			this.scrollBar = new UIScrollbarStandard(13, height * 19 - 1, this.grid);
			this.add(this.scrollBar);
			this.padding.bottom = 3;
			this.padding.right = 2;
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x0007411A File Offset: 0x0007231A
		public override List<UIElement> getScrollableElements()
		{
			return this.grid.getScrollableElements(this.controllerSelectColumn);
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x00074130 File Offset: 0x00072330
		public void setSidewaysScrollingToHoverColumn()
		{
			int scrollableElementColumn = this.grid.getScrollableElementColumn();
			if (scrollableElementColumn != -1)
			{
				this.controllerSelectColumn = scrollableElementColumn;
			}
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x00074154 File Offset: 0x00072354
		public override void controllerScrollSidewaysRight()
		{
			if (this.canControllerScrollRight())
			{
				this.controllerSelectColumn++;
			}
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x0007416C File Offset: 0x0007236C
		public override void controllerScrollSidewaysLeft()
		{
			if (this.canControllerScrollLeft())
			{
				this.controllerSelectColumn--;
			}
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x00074184 File Offset: 0x00072384
		public override bool canControllerScrollLeft()
		{
			this.setSidewaysScrollingToHoverColumn();
			return this.controllerSelectColumn > 0;
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x00074195 File Offset: 0x00072395
		public override bool canControllerScrollRight()
		{
			this.setSidewaysScrollingToHoverColumn();
			return this.controllerSelectColumn < this.gridWidth - 1;
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x000741AD File Offset: 0x000723AD
		public UIScrollbar getScrollbar()
		{
			return this.scrollBar;
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x000741B8 File Offset: 0x000723B8
		public void update(Inventory inv, List<Item.ItemTypes> types, Character currentCharacter)
		{
			this.inventory = inv;
			this.itemTypes = types;
			if (this.inventory == null)
			{
				return;
			}
			this.scrollBar.updateMouseInteraction();
			base.clearInteractItems();
			List<Item> listByType = this.inventory.getListByType(this.itemTypes, false);
			float num = 1f;
			if (listByType.Count > 0)
			{
				num = (float)(listByType.Count / this.gridWidth);
			}
			this.scrollBar.setIncrement(Mathf.RoundToInt(num));
			int num2 = Mathf.RoundToInt(num * this.scrollBar.getScrollDegree());
			this.offsetIndex = this.gridWidth * num2;
			if (this.offsetIndex > listByType.Count)
			{
				this.offsetIndex = listByType.Count;
			}
			listByType.RemoveRange(0, this.offsetIndex);
			this.grid.update(listByType, this.inventory.getCurrentObject(), currentCharacter);
			int buttonPressIndexLeft = this.grid.getButtonPressIndexLeft();
			int buttonPressIndexRight = this.grid.getButtonPressIndexRight();
			int buttonPressIndexDoubleClicked = this.grid.getButtonPressIndexDoubleClicked();
			if (buttonPressIndexLeft != -1 && buttonPressIndexLeft < listByType.Count)
			{
				this.leftClickedInteractItem = listByType[buttonPressIndexLeft];
				if (SkaldIO.isControllerConnected() && this.inventory.getCurrentObject() == listByType[buttonPressIndexLeft])
				{
					this.doubleClickedInteractItem = listByType[buttonPressIndexLeft];
				}
				this.inventory.setCurrentObject(listByType[buttonPressIndexLeft]);
				return;
			}
			if (buttonPressIndexRight != -1 && buttonPressIndexRight < listByType.Count)
			{
				this.inventory.setCurrentObject(listByType[buttonPressIndexRight]);
				this.rightClickedInteractItem = listByType[buttonPressIndexRight];
				return;
			}
			if (buttonPressIndexDoubleClicked != -1 && buttonPressIndexDoubleClicked < listByType.Count)
			{
				this.inventory.setCurrentObject(listByType[buttonPressIndexDoubleClicked]);
				this.doubleClickedInteractItem = listByType[buttonPressIndexDoubleClicked];
			}
		}

		// Token: 0x040009CA RID: 2506
		private UIGridInventory grid;

		// Token: 0x040009CB RID: 2507
		private UIScrollbar scrollBar;

		// Token: 0x040009CC RID: 2508
		private Inventory inventory;

		// Token: 0x040009CD RID: 2509
		private List<Item.ItemTypes> itemTypes;

		// Token: 0x040009CE RID: 2510
		private int gridWidth;

		// Token: 0x040009CF RID: 2511
		private int controllerSelectColumn;

		// Token: 0x040009D0 RID: 2512
		private int offsetIndex;
	}

	// Token: 0x0200029C RID: 668
	protected class TextLable : UITextBlock
	{
		// Token: 0x06001ADD RID: 6877 RVA: 0x0007436B File Offset: 0x0007256B
		public TextLable(string content) : base(0, 0)
		{
			this.stretchHorizontal = true;
			this.foregroundColors.mainColor = C64Color.HeaderColor;
			base.setContent(content);
			base.setLetterShadowColor(C64Color.SmallTextShadowColorDarkBackground);
			this.padding.left = 5;
		}
	}

	// Token: 0x0200029D RID: 669
	protected abstract class ItemInteractionUI : UIInventorySheetBase.UIInventorySegment
	{
		// Token: 0x06001ADE RID: 6878 RVA: 0x000743AA File Offset: 0x000725AA
		protected ItemInteractionUI()
		{
			this.image = new UIInventorySheetBase.ItemInteractionUI.CharacterImage();
			this.add(this.image);
		}

		// Token: 0x06001ADF RID: 6879
		public abstract void update(SkaldInstanceObject obj);

		// Token: 0x06001AE0 RID: 6880 RVA: 0x000743C9 File Offset: 0x000725C9
		public virtual void setInfoBlock(string content)
		{
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x000743CB File Offset: 0x000725CB
		protected virtual void createImage()
		{
			this.image = new UIInventorySheetBase.ItemInteractionUI.CharacterImage();
			this.add(this.image);
		}

		// Token: 0x040009D1 RID: 2513
		public UIInventorySheetBase.ItemInteractionUI.CharacterImage image;

		// Token: 0x020003DB RID: 987
		public class CharacterImage : UIImage
		{
			// Token: 0x06001D86 RID: 7558 RVA: 0x0007C78A File Offset: 0x0007A98A
			public CharacterImage()
			{
				this.stretchVertical = true;
				this.stretchHorizontal = true;
			}

			// Token: 0x06001D87 RID: 7559 RVA: 0x0007C7A0 File Offset: 0x0007A9A0
			public void setImage(TextureTools.TextureData texture)
			{
				this.backgroundTexture = TextureTools.loadTextureData("Images/GUIIcons/InventoryUI/ModelFrame");
				if (texture == null)
				{
					return;
				}
				int anchorX = -4;
				int anchorY = 4;
				if (texture.width < this.backgroundTexture.width)
				{
					anchorX = (this.backgroundTexture.width - texture.width) / 2;
				}
				TextureTools.applyOverlay(this.backgroundTexture, texture, anchorX, anchorY);
			}
		}
	}

	// Token: 0x0200029E RID: 670
	protected class ItemsWornUI : UIInventorySheetBase.ItemInteractionUI
	{
		// Token: 0x06001AE2 RID: 6882 RVA: 0x000743E4 File Offset: 0x000725E4
		public ItemsWornUI()
		{
			this.grid = new UIInventorySheetBase.ItemsWornUI.WornGridUI();
			this.add(this.grid);
			this.infoBlock = new UITextBlock(100, 0);
			this.infoBlock.padding.left = 4;
			this.infoBlock.padding.top = 2;
			this.infoBlock.setTabWidth(40);
			this.add(this.infoBlock);
			this.stretchVertical = true;
			this.padding.bottom = 3;
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x0007446A File Offset: 0x0007266A
		public override void setInfoBlock(string content)
		{
			this.infoBlock.setContent(content);
			this.infoBlock.setLetterShadowColor(C64Color.SmallTextShadowColor);
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x00074488 File Offset: 0x00072688
		public override void update(SkaldInstanceObject obj)
		{
			this.grid.update();
			if (obj == null)
			{
				return;
			}
			base.clearInteractItems();
			if (obj is Character)
			{
				Character character = obj as Character;
				this.image.setImage(character.getFixedTexture());
				this.grid.setButtons(character);
				if (this.grid.getButtonPressIndexLeft() != -1)
				{
					this.leftClickedInteractItem = character.getInventory().getCurrentObject();
					this.toggleWeapon(character, this.leftClickedInteractItem);
					return;
				}
				if (this.grid.getButtonPressIndexRight() != -1)
				{
					this.rightClickedInteractItem = character.getInventory().getCurrentObject();
					this.toggleWeapon(character, this.rightClickedInteractItem);
					return;
				}
			}
			else
			{
				this.image.setImage(obj.getModelTexture());
			}
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x00074541 File Offset: 0x00072741
		private void toggleWeapon(Character character, Item item)
		{
			if (item is ItemMeleeWeapon)
			{
				character.toggleMeleeWeapon();
				return;
			}
			if (item is ItemRangedWeapon)
			{
				character.toggleRangedWeapon();
			}
		}

		// Token: 0x040009D2 RID: 2514
		private UIInventorySheetBase.ItemsWornUI.WornGridUI grid;

		// Token: 0x040009D3 RID: 2515
		private UITextBlock infoBlock;

		// Token: 0x020003DC RID: 988
		private class WornGridUI : UIGridLists
		{
			// Token: 0x06001D88 RID: 7560 RVA: 0x0007C7FC File Offset: 0x0007A9FC
			public WornGridUI() : base(6, 2)
			{
			}

			// Token: 0x06001D89 RID: 7561 RVA: 0x0007C808 File Offset: 0x0007AA08
			public void setButtons(Character currentCharacter)
			{
				base.setButton(currentCharacter, currentCharacter.getCurrentMeleeWeapon(), 0, 0, "Images/GUIIcons/InventoryUI/WornIconMelee");
				base.setButton(currentCharacter, currentCharacter.getCurrentRangedWeapon(), 1, 0, "Images/GUIIcons/InventoryUI/WornIconRanged");
				base.setButton(currentCharacter, currentCharacter.getCurrentArmor(), 2, 0, "Images/GUIIcons/InventoryUI/WornIconArmor");
				base.setButton(currentCharacter, currentCharacter.getCurrentShieldRegardlessIfWorn(), 3, 0, "Images/GUIIcons/InventoryUI/WornIconShield");
				base.setButton(currentCharacter, currentCharacter.getCurrentAmmo(), 4, 0, "Images/GUIIcons/InventoryUI/WornIconAmmo");
				base.setButton(currentCharacter, currentCharacter.getCurrentRing(), 5, 0, "Images/GUIIcons/InventoryUI/WornIconRing");
				base.setButton(currentCharacter, currentCharacter.getCurrentHeadwear(), 0, 1, "Images/GUIIcons/InventoryUI/WornIconHead");
				base.setButton(currentCharacter, currentCharacter.getCurrentClothing(), 1, 1, "Images/GUIIcons/InventoryUI/WornIconClothing");
				base.setButton(currentCharacter, currentCharacter.getCurrentGloves(), 2, 1, "Images/GUIIcons/InventoryUI/WornIconHands");
				base.setButton(currentCharacter, currentCharacter.getCurrentFootwear(), 3, 1, "Images/GUIIcons/InventoryUI/WornIconFeet");
				base.setButton(currentCharacter, currentCharacter.getCurrentLight(), 4, 1, "Images/GUIIcons/InventoryUI/WornIconOffhand");
				base.setButton(currentCharacter, currentCharacter.getCurrentNecklace(), 5, 1, "Images/GUIIcons/InventoryUI/WornIconNecklace");
			}
		}
	}

	// Token: 0x0200029F RID: 671
	protected class FilterButtons : UIButtonControlHorizontal
	{
		// Token: 0x06001AE6 RID: 6886 RVA: 0x00074560 File Offset: 0x00072760
		public FilterButtons() : base(0, 0, 100, 14, 8)
		{
			base.getNestedCanvas().padding.bottom = 4;
			base.getNestedCanvas().padding.right = 0;
			base.getNestedCanvas().padding.left = 5;
			this.filterControl = new UIInventorySheetBase.FilterButtons.FilterControl();
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x000745B8 File Offset: 0x000727B8
		protected override void populateButtons()
		{
			for (int i = 0; i < this.buttonNumber; i++)
			{
				UIButtonControlBase.UITextButton element = this.createButton();
				base.getNestedCanvas().add(element);
			}
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x000745EC File Offset: 0x000727EC
		protected UIButtonControlBase.UITextButton createButton()
		{
			UIButtonControlBase.UITextButton uitextButton = new UIButtonControlBase.UITextButton(0, 0, 0, 0, C64Color.Brown, FontContainer.getMediumFont());
			uitextButton.setHeight(16);
			uitextButton.setWidth(18);
			uitextButton.foregroundColors.mainColor = C64Color.BrownLight;
			uitextButton.foregroundColors.hoverColor = C64Color.Gray;
			uitextButton.foregroundColors.leftClickedColor = C64Color.GrayDark;
			return uitextButton;
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x0007464C File Offset: 0x0007284C
		public override void update()
		{
			this.setButtons();
			base.update();
			if (this.buttonPressIndexLeft != -1)
			{
				this.filterControl.setFilterByIndex(base.getButtonPressIndexLeft());
			}
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x00074674 File Offset: 0x00072874
		public void setButtons()
		{
			base.setPosition();
			int num = 0;
			List<string> iconPaths = this.filterControl.getIconPaths();
			foreach (UIElement uielement in base.getButtonsList())
			{
				UIButtonControlBase.UITextButton uitextButton = (UIButtonControlBase.UITextButton)uielement;
				if (num < iconPaths.Count && iconPaths[num] != "")
				{
					uitextButton.foregroundTexture = TextureTools.loadTextureData(iconPaths[num]);
					if (this.filterControl.getFilterIndex() == num)
					{
						uitextButton.foregroundColors.outlineMainColor = C64Color.GrayLight;
					}
					else
					{
						uitextButton.foregroundColors.outlineMainColor = C64Color.GrayDark;
					}
				}
				num++;
			}
			base.getNestedCanvas().alignElements();
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x00074748 File Offset: 0x00072948
		public void shiftFilterIndex(int i)
		{
			this.filterControl.shiftFilterIndex(i);
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x00074756 File Offset: 0x00072956
		public List<Item.ItemTypes> getTypeFilter()
		{
			return this.filterControl.getTypeFilter();
		}

		// Token: 0x040009D4 RID: 2516
		private UIInventorySheetBase.FilterButtons.FilterControl filterControl;

		// Token: 0x020003DD RID: 989
		private class FilterControl
		{
			// Token: 0x06001D8A RID: 7562 RVA: 0x0007C908 File Offset: 0x0007AB08
			public FilterControl()
			{
				this.filters.Add(new UIInventorySheetBase.FilterButtons.FilterControl.InventoryFilter(null, "FilterAll"));
				this.filters.Add(new UIInventorySheetBase.FilterButtons.FilterControl.InventoryFilter(new List<Item.ItemTypes>
				{
					Item.ItemTypes.MeleeWeapon,
					Item.ItemTypes.RangedWeapon,
					Item.ItemTypes.Ammo
				}, "FilterWeapon"));
				this.filters.Add(new UIInventorySheetBase.FilterButtons.FilterControl.InventoryFilter(new List<Item.ItemTypes>
				{
					Item.ItemTypes.Armor,
					Item.ItemTypes.Clothing,
					Item.ItemTypes.Shield
				}, "FilterArmor"));
				this.filters.Add(new UIInventorySheetBase.FilterButtons.FilterControl.InventoryFilter(new List<Item.ItemTypes>
				{
					Item.ItemTypes.Headwear,
					Item.ItemTypes.Glove,
					Item.ItemTypes.Footwear,
					Item.ItemTypes.Jewelry,
					Item.ItemTypes.Ring,
					Item.ItemTypes.Necklace
				}, "FilterAccessories"));
				this.filters.Add(new UIInventorySheetBase.FilterButtons.FilterControl.InventoryFilter(new List<Item.ItemTypes>
				{
					Item.ItemTypes.Consumable,
					Item.ItemTypes.Tome
				}, "FilterConsumable"));
				this.filters.Add(new UIInventorySheetBase.FilterButtons.FilterControl.InventoryFilter(new List<Item.ItemTypes>
				{
					Item.ItemTypes.Food
				}, "FilterFood"));
				this.filters.Add(new UIInventorySheetBase.FilterButtons.FilterControl.InventoryFilter(new List<Item.ItemTypes>
				{
					Item.ItemTypes.Misc,
					Item.ItemTypes.Reagent,
					Item.ItemTypes.Adventuring,
					Item.ItemTypes.Light,
					Item.ItemTypes.Key,
					Item.ItemTypes.Book
				}, "FilterAdventuring"));
				this.filters.Add(new UIInventorySheetBase.FilterButtons.FilterControl.InventoryFilter(new List<Item.ItemTypes>
				{
					Item.ItemTypes.Trinket,
					Item.ItemTypes.Gems
				}, "FilterMisc"));
			}

			// Token: 0x06001D8B RID: 7563 RVA: 0x0007CAA4 File Offset: 0x0007ACA4
			public void setFilterByIndex(int index)
			{
				if (this.filters.Count == 0)
				{
					this.filterIndex = 0;
					return;
				}
				if (index < 0)
				{
					this.filterIndex = this.filters.Count - 1;
					return;
				}
				if (index >= this.filters.Count)
				{
					this.filterIndex = 0;
					return;
				}
				this.filterIndex = index;
			}

			// Token: 0x06001D8C RID: 7564 RVA: 0x0007CAFB File Offset: 0x0007ACFB
			public int getFilterIndex()
			{
				return this.filterIndex;
			}

			// Token: 0x06001D8D RID: 7565 RVA: 0x0007CB03 File Offset: 0x0007AD03
			public void shiftFilterIndex(int i)
			{
				this.setFilterByIndex(this.getFilterIndex() + i);
			}

			// Token: 0x06001D8E RID: 7566 RVA: 0x0007CB14 File Offset: 0x0007AD14
			public List<string> getIconPaths()
			{
				List<string> list = new List<string>();
				foreach (UIInventorySheetBase.FilterButtons.FilterControl.InventoryFilter inventoryFilter in this.filters)
				{
					list.Add(inventoryFilter.getIconPath());
				}
				return list;
			}

			// Token: 0x06001D8F RID: 7567 RVA: 0x0007CB74 File Offset: 0x0007AD74
			public List<Item.ItemTypes> getTypeFilter()
			{
				List<Item.ItemTypes> result;
				try
				{
					result = this.filters[this.filterIndex].getTypes();
				}
				catch
				{
					result = null;
				}
				return result;
			}

			// Token: 0x04000C78 RID: 3192
			private List<UIInventorySheetBase.FilterButtons.FilterControl.InventoryFilter> filters = new List<UIInventorySheetBase.FilterButtons.FilterControl.InventoryFilter>();

			// Token: 0x04000C79 RID: 3193
			private int filterIndex;

			// Token: 0x02000432 RID: 1074
			private class InventoryFilter
			{
				// Token: 0x06001E02 RID: 7682 RVA: 0x0007DE85 File Offset: 0x0007C085
				public InventoryFilter(List<Item.ItemTypes> types, string iconPath)
				{
					this.types = types;
					this.iconPath = "Images/GUIIcons/InventoryUI/" + iconPath;
				}

				// Token: 0x06001E03 RID: 7683 RVA: 0x0007DEA5 File Offset: 0x0007C0A5
				public List<Item.ItemTypes> getTypes()
				{
					return this.types;
				}

				// Token: 0x06001E04 RID: 7684 RVA: 0x0007DEAD File Offset: 0x0007C0AD
				public string getIconPath()
				{
					return this.iconPath;
				}

				// Token: 0x04000D9A RID: 3482
				private List<Item.ItemTypes> types;

				// Token: 0x04000D9B RID: 3483
				private string iconPath;
			}
		}
	}
}
