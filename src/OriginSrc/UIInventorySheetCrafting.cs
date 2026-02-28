using System;
using System.Collections.Generic;

// Token: 0x0200015D RID: 349
public class UIInventorySheetCrafting : UIInventorySheetBase
{
	// Token: 0x06001350 RID: 4944 RVA: 0x000552A4 File Offset: 0x000534A4
	protected override void initialize()
	{
		this.secondRow = new UICanvasVertical();
		this.secondRow.padding.left = 1;
		this.add(this.secondRow);
		this.mainRow.setWidth(108);
		this.mainRow.padding.top = 2;
		this.mainRow.padding.left = 5;
		UIInventorySheetBase.TextLable textLable = new UIInventorySheetBase.TextLable("Recipes");
		textLable.padding.left = 0;
		textLable.padding.bottom = 4;
		this.mainRow.add(textLable);
		this.listButtons = new ListButtonControl(0, 0, 100, 188, 20);
		this.mainRow.add(this.listButtons);
		UIInventorySheetBase.TextLable textLable2 = new UIInventorySheetBase.TextLable("Workstation");
		textLable2.padding.top = 1;
		textLable2.padding.bottom = 1;
		this.secondRow.add(textLable2);
		this.secondaryInventoryGrid = new UIInventorySheetBase.UIGridCharacterInventorySegment(3, 2);
		this.itemInteractionGrid = new UIInventorySheetCrafting.IngredientsUI(this.secondaryInventoryGrid);
		this.itemInteractionGrid.padding.bottom = 2;
		this.secondRow.add(this.itemInteractionGrid);
		this.buttons = new UITechnicalButtonsHorizontal(0, 0, this.getBaseWidth(), 16, 2);
		this.buttons.toggleAXBYButNoNumbersControllerPressControl();
		this.buttons.setButtonText(new List<string>
		{
			"Craft",
			"Clear"
		});
		this.secondRow.add(this.buttons);
		UIInventorySheetBase.TextLable textLable3 = new UIInventorySheetBase.TextLable("Party Inventory");
		textLable3.padding.top = 5;
		this.secondRow.add(textLable3);
		this.mainInventoryGrid = new UIInventorySheetBase.UIGridCharacterInventorySegment(5, 6);
		this.secondRow.add(this.mainInventoryGrid);
	}

	// Token: 0x06001351 RID: 4945 RVA: 0x00055466 File Offset: 0x00053666
	public override void update(SkaldInstanceObject obj, Inventory mainInv, Inventory secondaryInventory, Character currentCharacter)
	{
		base.update(obj, mainInv, secondaryInventory, currentCharacter);
		this.buttons.update();
	}

	// Token: 0x06001352 RID: 4946 RVA: 0x0005547E File Offset: 0x0005367E
	public override void scrollRightToControllerSurface()
	{
		this.currentControllerSurface = this.mainInventoryGrid;
	}

	// Token: 0x06001353 RID: 4947 RVA: 0x0005548C File Offset: 0x0005368C
	public override void scrollLeftToControllerSurface()
	{
		this.currentControllerSurface = this.listButtons;
	}

	// Token: 0x06001354 RID: 4948 RVA: 0x0005549A File Offset: 0x0005369A
	public int getTechnicalButtonsIndex()
	{
		return this.buttons.getButtonPressIndexLeft();
	}

	// Token: 0x06001355 RID: 4949 RVA: 0x000554A7 File Offset: 0x000536A7
	public override List<Item.ItemTypes> getTypeFilter()
	{
		if (this.workBench == null)
		{
			this.workBench = MainControl.getDataControl().getWorkBench();
		}
		if (this.workBench == null)
		{
			return new List<Item.ItemTypes>
			{
				Item.ItemTypes.Food
			};
		}
		return this.workBench.getType();
	}

	// Token: 0x06001356 RID: 4950 RVA: 0x000554E2 File Offset: 0x000536E2
	public override string getBackgroundPath()
	{
		if (GlobalSettings.getFontSettings().getTextBackgroundColor() == 2)
		{
			return "Images/GUIIcons/SheetCraftingBackgroundBlack";
		}
		if (GlobalSettings.getFontSettings().getTextBackgroundColor() == 1)
		{
			return "Images/GUIIcons/SheetCraftingBackgroundBrown";
		}
		return "Images/GUIIcons/SheetCraftingBackground";
	}

	// Token: 0x06001357 RID: 4951 RVA: 0x0005550F File Offset: 0x0005370F
	public ListButtonControl getListButtons()
	{
		return this.listButtons;
	}

	// Token: 0x040004BA RID: 1210
	protected ListButtonControl listButtons;

	// Token: 0x040004BB RID: 1211
	protected UICanvasVertical secondRow;

	// Token: 0x040004BC RID: 1212
	protected UITechnicalButtonsHorizontal buttons;

	// Token: 0x040004BD RID: 1213
	private PropWorkBench workBench;

	// Token: 0x020002A0 RID: 672
	protected class IngredientsUI : UIInventorySheetBase.ItemInteractionUI
	{
		// Token: 0x06001AED RID: 6893 RVA: 0x00074763 File Offset: 0x00072963
		public IngredientsUI(UIInventorySheetBase.UIGridCharacterInventorySegment grid)
		{
			this.grid = grid;
			this.add(grid);
			this.stretchVertical = true;
			this.padding.bottom = 3;
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x0007478C File Offset: 0x0007298C
		public override void update(SkaldInstanceObject obj)
		{
			base.clearInteractItems();
			if (obj != null)
			{
				this.image.setImage(obj.getModelTexture());
				return;
			}
			this.image.setImage(null);
		}

		// Token: 0x040009D5 RID: 2517
		private UIInventorySheetBase.UIGridCharacterInventorySegment grid;
	}
}
