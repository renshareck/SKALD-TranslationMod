using System;
using System.Collections.Generic;
using SkaldEnums;

// Token: 0x02000083 RID: 131
public class TradeState : InventoryBaseState
{
	// Token: 0x06000A0C RID: 2572 RVA: 0x00030597 File Offset: 0x0002E797
	public TradeState(DataControl dataControl) : base(dataControl)
	{
		this.currentCharacter = dataControl.getCurrentPC();
	}

	// Token: 0x06000A0D RID: 2573 RVA: 0x000305AC File Offset: 0x0002E7AC
	protected override void createGUI()
	{
		this.gridUI = new UIInventorySheetMerchant();
		this.guiControl = new GUIControlContainer(this.gridUI);
	}

	// Token: 0x06000A0E RID: 2574 RVA: 0x000305CC File Offset: 0x0002E7CC
	public override void update()
	{
		base.update();
		if (this.testExit())
		{
			return;
		}
		UIInventorySheetMerchant uiinventorySheetMerchant = this.gridUI as UIInventorySheetMerchant;
		uiinventorySheetMerchant.updateServiceButtons(this.dataControl.currentStore.getServiceList());
		if (uiinventorySheetMerchant.getServiceButtonPressIndex() != -1)
		{
			this.dataControl.currentStore.applyService(uiinventorySheetMerchant.getServiceButtonPressIndex());
		}
		if (this.numericInputIndex != -1)
		{
			if (this.numericInputIndex == 1)
			{
				if (base.getCurrentFocusIsMainInventory())
				{
					this.interactWithCurrentItemFromMainInventory();
				}
				else
				{
					this.interactWithCurrentItemFromSecondaryInventory();
				}
			}
			else if (this.numericInputIndex == 2)
			{
				if (base.getCurrentFocusIsMainInventory())
				{
					this.printResultBark(this.getMainInventory().getCurrentObject(), this.dataControl.currentStore.sellStack());
				}
				else
				{
					PopUpControl.addStealingPopUp(this.dataControl.currentStore);
				}
			}
		}
		this.numericInputIndex = -1;
		this.setGUIData();
	}

	// Token: 0x06000A0F RID: 2575 RVA: 0x000306A4 File Offset: 0x0002E8A4
	protected override bool testExit()
	{
		if (base.testExit())
		{
			return true;
		}
		if (!this.isStoreMounted())
		{
			this.clearAndGoToOverland();
			return true;
		}
		if (this.dataControl.currentStore.isStealLocked())
		{
			PopUpControl.addPopUpStealLocked();
			return true;
		}
		return false;
	}

	// Token: 0x06000A10 RID: 2576 RVA: 0x000306DA File Offset: 0x0002E8DA
	protected override void clearAndGoToOverland()
	{
		this.setTargetState(SkaldStates.Overland);
		this.dataControl.clearStore();
	}

	// Token: 0x06000A11 RID: 2577 RVA: 0x000306EE File Offset: 0x0002E8EE
	protected override void interactWithCurrentItemFromMainInventory()
	{
		this.printResultBark(this.getMainInventory().getCurrentObject(), this.dataControl.currentStore.sellItem());
	}

	// Token: 0x06000A12 RID: 2578 RVA: 0x00030711 File Offset: 0x0002E911
	protected override void interactWithCurrentItemFromSecondaryInventory()
	{
		this.printResultBark(this.getSecondaryInventory().getCurrentObject(), this.dataControl.currentStore.buyItem());
	}

	// Token: 0x06000A13 RID: 2579 RVA: 0x00030734 File Offset: 0x0002E934
	private void printResultBark(Item item, SkaldActionResult result)
	{
		if (item == null)
		{
			return;
		}
		if (result.wasPerformed())
		{
			this.guiControl.addInfoBark(result.getResultString(), item);
		}
		this.setMainTextBuffer("\n\n\n" + result.getVerboseResultString());
	}

	// Token: 0x06000A14 RID: 2580 RVA: 0x0003076A File Offset: 0x0002E96A
	protected override void selectCurrentItemFromSecondary()
	{
		this.setMainTextBuffer(this.dataControl.currentStore.getCurrentItemDesc());
	}

	// Token: 0x06000A15 RID: 2581 RVA: 0x00030782 File Offset: 0x0002E982
	protected override void selectCurrentItemFromMain()
	{
		this.setMainTextBuffer(this.getMainInventory().printComparativeItemStats(this.dataControl.getCurrentPC()) + "\n\n" + this.dataControl.currentStore.getSalesOffer());
	}

	// Token: 0x06000A16 RID: 2582 RVA: 0x000307BA File Offset: 0x0002E9BA
	protected override Inventory getSecondaryInventory()
	{
		if (this.isStoreMounted())
		{
			return this.dataControl.currentStore.getInventory();
		}
		return null;
	}

	// Token: 0x06000A17 RID: 2583 RVA: 0x000307D6 File Offset: 0x0002E9D6
	private bool isStoreMounted()
	{
		return this.dataControl.isStoreMounted();
	}

	// Token: 0x06000A18 RID: 2584 RVA: 0x000307E4 File Offset: 0x0002E9E4
	protected override void setGUIData()
	{
		if (this.getMainInventory() == null)
		{
			return;
		}
		if (base.getCurrentFocusIsMainInventory())
		{
			base.setGUIData(new List<string>
			{
				"Sell",
				"Sell Stack"
			}, this.dataControl.getCurrentPC());
		}
		else
		{
			base.setGUIData(new List<string>
			{
				"Buy",
				"Steal"
			}, this.dataControl.getCurrentPC());
		}
		if (this.isStoreMounted())
		{
			this.guiControl.setSheetHeader(this.dataControl.currentStore.getName());
			this.guiControl.setSecondaryDescription(this.dataControl.currentStore.printInfoString());
			return;
		}
		this.guiControl.setSheetHeader("Trading");
	}

	// Token: 0x040002A5 RID: 677
	private Character currentCharacter;
}
