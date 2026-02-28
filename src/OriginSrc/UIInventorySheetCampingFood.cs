using System;
using System.Collections.Generic;

// Token: 0x0200015E RID: 350
public class UIInventorySheetCampingFood : UIInventorySheetBase
{
	// Token: 0x06001358 RID: 4952 RVA: 0x00055517 File Offset: 0x00053717
	public UIInventorySheetCampingFood(Party party)
	{
		this.party = party;
		this.setActivites();
	}

	// Token: 0x06001359 RID: 4953 RVA: 0x0005552C File Offset: 0x0005372C
	protected override void initialize()
	{
		this.secondRow = new UICanvasVertical();
		this.secondRow.padding.left = 1;
		this.add(this.secondRow);
		this.mainRow.setWidth(108);
		this.mainRow.padding.top = 2;
		this.mainRow.padding.left = 5;
		this.mealLabel = new UIInventorySheetBase.TextLable("Tonight's Meal");
		this.mealLabel.padding.top = 1;
		this.mealLabel.padding.bottom = 1;
		this.secondRow.add(this.mealLabel);
		this.secondaryInventoryGrid = new UIInventorySheetBase.UIGridCharacterInventorySegment(5, 2);
		this.itemInteractionGrid = new UIInventorySheetCampingFood.CookingUI(this.secondaryInventoryGrid);
		this.itemInteractionGrid.padding.bottom = 0;
		this.secondRow.add(this.itemInteractionGrid);
		this.buttons = new UITechnicalButtonsHorizontal(0, 0, this.getBaseWidth(), 16, 1);
		this.buttons.setButtonText(new List<string>
		{
			"Clear"
		});
		this.secondRow.add(this.buttons);
		UIInventorySheetBase.TextLable textLable = new UIInventorySheetBase.TextLable("Party's Food");
		textLable.padding.top = 5;
		this.secondRow.add(textLable);
		this.mainInventoryGrid = new UIInventorySheetBase.UIGridCharacterInventorySegment(5, 6);
		this.secondRow.add(this.mainInventoryGrid);
	}

	// Token: 0x0600135A RID: 4954 RVA: 0x00055696 File Offset: 0x00053896
	public override void scrollRightToControllerSurface()
	{
		this.currentControllerSurface = this.mainInventoryGrid;
	}

	// Token: 0x0600135B RID: 4955 RVA: 0x000556A4 File Offset: 0x000538A4
	public override void scrollLeftToControllerSurface()
	{
		this.currentControllerSurface = this.activities;
	}

	// Token: 0x0600135C RID: 4956 RVA: 0x000556B4 File Offset: 0x000538B4
	private void setActivites()
	{
		UIInventorySheetBase.TextLable textLable = new UIInventorySheetBase.TextLable("Activities");
		textLable.padding.left = 0;
		textLable.padding.bottom = 4;
		this.mainRow.add(textLable);
		this.activities = new UITextSliderControl();
		foreach (Character character in this.party.getPartyList())
		{
			this.activities.createCampActivitySliderButton(character);
		}
		this.mainRow.add(this.activities);
	}

	// Token: 0x0600135D RID: 4957 RVA: 0x00055760 File Offset: 0x00053960
	public string getActivityDescription()
	{
		return this.activities.getHoverButtonDescription();
	}

	// Token: 0x0600135E RID: 4958 RVA: 0x0005576D File Offset: 0x0005396D
	public void setMealLabel(string s)
	{
		this.mealLabel.setContent(s);
	}

	// Token: 0x0600135F RID: 4959 RVA: 0x0005577B File Offset: 0x0005397B
	public override void update(SkaldInstanceObject obj, Inventory mainInv, Inventory secondaryInventory, Character currentCharacter)
	{
		base.update(obj, mainInv, secondaryInventory, currentCharacter);
		this.buttons.update();
		this.activities.update();
	}

	// Token: 0x06001360 RID: 4960 RVA: 0x0005579E File Offset: 0x0005399E
	public int getTechnicalButtonsIndex()
	{
		return this.buttons.getButtonPressIndexLeft();
	}

	// Token: 0x06001361 RID: 4961 RVA: 0x000557AB File Offset: 0x000539AB
	public override List<Item.ItemTypes> getTypeFilter()
	{
		return new List<Item.ItemTypes>
		{
			Item.ItemTypes.Food
		};
	}

	// Token: 0x06001362 RID: 4962 RVA: 0x000557BA File Offset: 0x000539BA
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

	// Token: 0x06001363 RID: 4963 RVA: 0x000557E7 File Offset: 0x000539E7
	public ListButtonControl getListButtons()
	{
		return this.listButtons;
	}

	// Token: 0x040004BE RID: 1214
	protected ListButtonControl listButtons;

	// Token: 0x040004BF RID: 1215
	protected UICanvasVertical secondRow;

	// Token: 0x040004C0 RID: 1216
	protected UITechnicalButtonsHorizontal buttons;

	// Token: 0x040004C1 RID: 1217
	private UITextSliderControl activities;

	// Token: 0x040004C2 RID: 1218
	private UIInventorySheetBase.TextLable mealLabel;

	// Token: 0x040004C3 RID: 1219
	private Party party;

	// Token: 0x020002A1 RID: 673
	protected class CookingUI : UIInventorySheetBase.ItemInteractionUI
	{
		// Token: 0x06001AEF RID: 6895 RVA: 0x000747B5 File Offset: 0x000729B5
		public CookingUI(UIInventorySheetBase.UIGridCharacterInventorySegment grid)
		{
			this.grid = grid;
			this.add(grid);
			this.stretchVertical = true;
			this.padding.bottom = 3;
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x000747DE File Offset: 0x000729DE
		protected override void createImage()
		{
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x000747E0 File Offset: 0x000729E0
		public override void update(SkaldInstanceObject obj)
		{
			base.clearInteractItems();
		}

		// Token: 0x040009D6 RID: 2518
		private UIInventorySheetBase.UIGridCharacterInventorySegment grid;
	}
}
