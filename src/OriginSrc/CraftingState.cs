using System;
using System.Collections.Generic;
using SkaldEnums;

// Token: 0x02000080 RID: 128
public class CraftingState : InventoryBaseState
{
	// Token: 0x060009EB RID: 2539 RVA: 0x0002FBB8 File Offset: 0x0002DDB8
	public CraftingState(DataControl dataControl) : base(dataControl)
	{
		this.stateId = SkaldStates.Crafting;
		base.disableClickablePortraits();
		this.allowQuickButtons = false;
		this.workbench = dataControl.getWorkBench();
		this.craftingControl = dataControl.getCraftingControl();
		this.craftingControl.setType(this.workbench.getCraftingType());
		this.craftingControl.setFirstKnownRecipe();
		this.workbenchInventory = dataControl.getTargetTileInventory();
		this.setMainTextBuffer("Double click ingredients to add them to the workbench.\n\nPress \"Craft\" to create a new item.\n\nClick recipes in the left column to view ingredients.");
		dataControl.clearWorkbench();
		if (!dataControl.getTechnicalDataRecord().hasCrafted())
		{
			dataControl.getTechnicalDataRecord().setHasCrafted();
			PopUpControl.addTutorialPopUp("TUT_Crafting1");
		}
	}

	// Token: 0x060009EC RID: 2540 RVA: 0x0002FC64 File Offset: 0x0002DE64
	protected override void createGUI()
	{
		this.gridUI = new UIInventorySheetCrafting();
		this.guiControl = new GUIControlCrafting(this.gridUI as UIInventorySheetCrafting);
	}

	// Token: 0x060009ED RID: 2541 RVA: 0x0002FC88 File Offset: 0x0002DE88
	public override void update()
	{
		base.update();
		if (this.testExit())
		{
			return;
		}
		if (this.resultString != "")
		{
			Item currentObject = this.getMainInventory().getCurrentObject();
			currentObject.markAsNewAddition();
			this.getMainInventory().sortList();
			this.guiControl.addInfoBark(this.resultString, currentObject);
			this.resultString = "";
		}
		if (this.guiControl.getListButtonPressIndex() != -1)
		{
			this.craftingControl.setEntryByIndex(this.guiControl.getListButtonPressIndex());
			this.addRecipeToWorkbench();
			this.setMainTextBuffer(this.craftingControl.getCurrentRecipeFullDescription(this.workbenchInventory, this.dataControl.getCurrentPC()));
		}
		if (this.getTechnicalButtonsIndex() == 0)
		{
			this.craft();
		}
		else if (this.getTechnicalButtonsIndex() == 1)
		{
			this.clearWorkbench();
		}
		this.numericInputIndex = -1;
		this.setGUIData();
	}

	// Token: 0x060009EE RID: 2542 RVA: 0x0002FD68 File Offset: 0x0002DF68
	private void clearWorkbench()
	{
		this.getMainInventory().transferInventory(this.workbenchInventory);
	}

	// Token: 0x060009EF RID: 2543 RVA: 0x0002FD7C File Offset: 0x0002DF7C
	private void addRecipeToWorkbench()
	{
		this.clearWorkbench();
		if (!this.craftingControl.areAnyRecipesKnown())
		{
			PopUpControl.addPopUpOK("You currently don't have any recepies to add to the crafting table. Try experimenting with ingredients or look for recipes in the world.");
			return;
		}
		if (!this.craftingControl.isAKnownRecipeAvailable())
		{
			PopUpControl.addPopUpOK("Select a recipe from the list to the left. Available recipes are shown in green.");
			return;
		}
		if (!this.craftingControl.couldThisPotentiallyBeCrafted(this.workbenchInventory, this.dataControl.getCurrentPC()))
		{
			return;
		}
		AudioControl.playDefaultInventorySound();
		this.craftingControl.transferItemsFromPartyToWorkbench(this.workbenchInventory, this.dataControl.getCurrentPC());
	}

	// Token: 0x060009F0 RID: 2544 RVA: 0x0002FDFF File Offset: 0x0002DFFF
	protected override void clearAndGoToOverland()
	{
		this.clearWorkbench();
		base.clearAndGoToOverland();
	}

	// Token: 0x060009F1 RID: 2545 RVA: 0x0002FE0D File Offset: 0x0002E00D
	private int getTechnicalButtonsIndex()
	{
		return (this.gridUI as UIInventorySheetCrafting).getTechnicalButtonsIndex();
	}

	// Token: 0x060009F2 RID: 2546 RVA: 0x0002FE1F File Offset: 0x0002E01F
	protected override void interactWithCurrentItemFromMainInventory()
	{
		this.getSecondaryInventory().addItem(this.getMainInventory().removeCurrentItemStack());
		AudioControl.playDefaultInventorySound();
		this.selectCurrentItemFromSecondary();
	}

	// Token: 0x060009F3 RID: 2547 RVA: 0x0002FE43 File Offset: 0x0002E043
	protected override void interactWithCurrentItemFromSecondaryInventory()
	{
		this.getMainInventory().addItem(this.getSecondaryInventory().removeCurrentItemStack());
		AudioControl.playDefaultInventorySound();
		this.selectCurrentItemFromMain();
	}

	// Token: 0x060009F4 RID: 2548 RVA: 0x0002FE67 File Offset: 0x0002E067
	protected override Inventory getSecondaryInventory()
	{
		return this.workbenchInventory;
	}

	// Token: 0x060009F5 RID: 2549 RVA: 0x0002FE70 File Offset: 0x0002E070
	private void craft()
	{
		if (this.workbenchInventory.isEmpty())
		{
			this.secondaryTextBuffer = "No crafting ingredients added!";
			PopUpControl.addPopUpOK("To craft items, you must add ingredients to the crafting grid.\n\nDo this by double-clicking on ingredients from your inventory.\n\nClicking a recipe to view ingredients required. Green recipes are available.");
			return;
		}
		List<Item> list = this.craftingControl.craft(this.workbenchInventory, this.dataControl.getCurrentPC());
		if (list != null && list.Count > 0)
		{
			Item item = list[0];
			foreach (Item item2 in list)
			{
				if (list.Count == 1)
				{
					this.resultString = "Crafted " + item2.getCount().ToString() + " " + item2.getName();
				}
				else
				{
					this.resultString = "Crafted several items!";
				}
				this.getMainInventory().addItem(item2);
			}
			this.secondaryTextBuffer = string.Concat(new string[]
			{
				"You created: ",
				list.Count.ToString(),
				" ",
				C64Color.WHITE_TAG,
				item.getName(),
				"</color>"
			});
			this.selectCurrentItemFromMain();
			AudioControl.playDefaultInventorySound();
			return;
		}
		PopUpControl.addPopUpOK("This is not a valid recipe!");
		this.secondaryTextBuffer = "This is not a valid recipe!";
	}

	// Token: 0x060009F6 RID: 2550 RVA: 0x0002FFCC File Offset: 0x0002E1CC
	protected override void setGUIData()
	{
		base.setGUIData(new List<string>(), this.workbench);
		string secondaryDescription = this.dataControl.getCurrentPC().getNameColored().ToUpper() + "\n" + TextTools.formateNameValuePairPlusMinus("Crafting: ", this.dataControl.getCurrentPC().getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_Crafting));
		this.guiControl.setSecondaryDescription(secondaryDescription);
		if (this.craftingControl != null)
		{
			this.guiControl.setListButtons(this.craftingControl.getRecipeList(this.workbenchInventory, this.dataControl.getCurrentPC()));
		}
		if (this.workbench != null)
		{
			this.guiControl.setSheetHeader("Crafting: " + this.workbench.getName());
		}
	}

	// Token: 0x0400029E RID: 670
	private PropWorkBench workbench;

	// Token: 0x0400029F RID: 671
	private Inventory workbenchInventory;

	// Token: 0x040002A0 RID: 672
	private CraftingControl craftingControl;

	// Token: 0x040002A1 RID: 673
	private string resultString = "";
}
