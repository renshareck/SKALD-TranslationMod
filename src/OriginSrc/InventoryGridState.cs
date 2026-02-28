using System;
using System.Collections.Generic;
using SkaldEnums;

// Token: 0x02000082 RID: 130
public class InventoryGridState : InventoryBaseState
{
	// Token: 0x06000A06 RID: 2566 RVA: 0x000303C1 File Offset: 0x0002E5C1
	public InventoryGridState(DataControl dataControl) : base(dataControl)
	{
		this.stateId = SkaldStates.Inventory;
		this.getMainInventory().sortList();
	}

	// Token: 0x06000A07 RID: 2567 RVA: 0x000303E0 File Offset: 0x0002E5E0
	public override void update()
	{
		if (this.testExit())
		{
			return;
		}
		base.update();
		if (this.numericInputIndex != -1)
		{
			if (this.numericInputIndex == 1)
			{
				this.interactWithCurrentItemFromMainInventory();
			}
			else if (this.numericInputIndex == 2)
			{
				this.setTargetState(SkaldStates.Container);
			}
		}
		this.numericInputIndex = -1;
		this.setGUIData();
	}

	// Token: 0x06000A08 RID: 2568 RVA: 0x00030434 File Offset: 0x0002E634
	protected override void interactWithCurrentItemFromMainInventory()
	{
		if (this.getMainInventory().getCurrentObject().isUseable())
		{
			Item currentObject = this.getMainInventory().getCurrentObject();
			SkaldActionResult skaldActionResult = this.dataControl.useCurrentItem();
			this.secondaryTextBuffer = skaldActionResult.getShortAndVerboseResultString();
			this.selectCurrentItemFromMain();
			if (this.dataControl.isCombatActive())
			{
				this.dataControl.getCombatEncounter().getCurrentCharacter().useItemInCombat();
				this.clearAndGoToOverland();
			}
			this.guiControl.addInfoBark(skaldActionResult.getResultString(), currentObject);
			this.guiControl.addInfoBark(skaldActionResult.getVerboseResultString(), currentObject);
			return;
		}
		this.secondaryTextBuffer = "This item cannot be used like this.";
	}

	// Token: 0x06000A09 RID: 2569 RVA: 0x000304D5 File Offset: 0x0002E6D5
	protected override void interactWithCurrentItemFromSecondaryInventory()
	{
		this.dataControl.getItemOnGround();
	}

	// Token: 0x06000A0A RID: 2570 RVA: 0x000304E2 File Offset: 0x0002E6E2
	protected override void clearAndGoToOverland()
	{
		this.getMainInventory().clearNewAdditions();
		this.getSecondaryInventory().clearNewAdditions();
		base.clearAndGoToOverland();
	}

	// Token: 0x06000A0B RID: 2571 RVA: 0x00030500 File Offset: 0x0002E700
	protected override void setGUIData()
	{
		string useVerb = this.getMainInventory().getUseVerb(this.dataControl.getCurrentPC());
		this.gridUI.setInfoBlock(this.dataControl.getCurrentPC().printInventorySummary());
		base.setGUIData(new List<string>
		{
			useVerb,
			"Drop / Pick Up"
		}, this.dataControl.getCurrentPC());
		this.guiControl.setSheetHeader(this.dataControl.getCurrentPC().getName() + ": " + this.getMainInventory().getName());
	}
}
