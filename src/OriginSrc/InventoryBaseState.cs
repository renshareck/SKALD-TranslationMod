using System;
using System.Collections.Generic;
using SkaldEnums;

// Token: 0x02000081 RID: 129
public abstract class InventoryBaseState : CharacterInfoStates
{
	// Token: 0x060009F7 RID: 2551 RVA: 0x0003008C File Offset: 0x0002E28C
	protected InventoryBaseState(DataControl dataControl) : base(dataControl)
	{
		this.createGUI();
		this.selectCurrentItemFromMain();
		this.stateId = SkaldStates.Inventory;
		this.setGUIData();
		if (this.getMainInventory() != null)
		{
			this.getMainInventory().checkIntegrity();
		}
		if (this.getSecondaryInventory() != null)
		{
			this.getSecondaryInventory().checkIntegrity();
		}
	}

	// Token: 0x060009F8 RID: 2552 RVA: 0x000300E7 File Offset: 0x0002E2E7
	protected override void createGUI()
	{
		this.gridUI = new UIInventorySheetCharacter();
		this.guiControl = new GUIControlInventory(this.gridUI);
	}

	// Token: 0x060009F9 RID: 2553 RVA: 0x00030105 File Offset: 0x0002E305
	public override void update()
	{
		base.update();
		this.updatedCurrentCharacter();
		this.updateInteractItems();
		if (SkaldIO.getAbilityButtonShiftRightButtonPressed())
		{
			this.gridUI.shiftFilterIndex(-1);
			return;
		}
		if (SkaldIO.getAbilityButtonShiftLeftButtonPressed())
		{
			this.gridUI.shiftFilterIndex(1);
		}
	}

	// Token: 0x060009FA RID: 2554 RVA: 0x00030140 File Offset: 0x0002E340
	protected virtual void updatedCurrentCharacter()
	{
		if (this.dataControl.getCurrentPC() != this.currentCharacter)
		{
			this.currentCharacter = this.dataControl.getCurrentPC();
			if (this.getCurrentFocusIsMainInventory())
			{
				this.selectCurrentItemFromMain();
				return;
			}
			this.selectCurrentItemFromSecondary();
		}
	}

	// Token: 0x060009FB RID: 2555 RVA: 0x0003017C File Offset: 0x0002E37C
	private void updateInteractItems()
	{
		if (ToolTipPrinter.isMouseOverTooltip())
		{
			return;
		}
		if (this.gridUI.getMainLeftClickedInteractItem() != null)
		{
			this.setCurrentFocusIsMainInventory();
			this.selectCurrentItemFromMain();
		}
		if (this.gridUI.getMainDoubleClickedInteractItem() != null)
		{
			this.setCurrentFocusIsMainInventory();
			this.selectCurrentItemFromMain();
			this.interactWithCurrentItemFromMainInventory();
		}
		else if (this.gridUI.getMainRightClickedInteractItem() != null)
		{
			this.setCurrentFocusIsMainInventory();
			this.selectCurrentItemFromMain();
			if (this.getMainInventory().getCurrentObject() != null)
			{
				ToolTipPrinter.setToolTipWithRules(this.getMainInventory().getCurrentObject().printComparativeStats(this.dataControl.getCurrentPC()));
			}
		}
		else if (this.gridUI.getWornLeftClickedInteractItem() != null)
		{
			this.setCurrentFocusIsMainInventory();
			this.selectCurrentItemFromMain();
		}
		else if (this.gridUI.getWornRightClickedInteractItem() != null)
		{
			this.setCurrentFocusIsMainInventory();
			this.selectCurrentItemFromMain();
		}
		else if (this.gridUI.getSecondaryLeftClickedInteractItem() != null)
		{
			this.setCurrentFocusIsSecondaryInventory();
			this.selectCurrentItemFromSecondary();
		}
		if (this.gridUI.getSecondaryDoubleClickedInteractItem() != null)
		{
			this.setCurrentFocusIsSecondaryInventory();
			this.selectCurrentItemFromSecondary();
			this.interactWithCurrentItemFromSecondaryInventory();
			return;
		}
		if (this.gridUI.getSecondaryRightClickedInteractItem() != null)
		{
			this.setCurrentFocusIsSecondaryInventory();
			this.selectCurrentItemFromSecondary();
			if (this.getMainInventory().getCurrentObject() != null)
			{
				ToolTipPrinter.setToolTipWithRules(this.getSecondaryInventory().getCurrentObject().printComparativeStats(this.dataControl.getCurrentPC()));
			}
		}
	}

	// Token: 0x060009FC RID: 2556 RVA: 0x000302CB File Offset: 0x0002E4CB
	protected bool getCurrentFocusIsMainInventory()
	{
		return this.currentFocusIsMainInventory;
	}

	// Token: 0x060009FD RID: 2557 RVA: 0x000302D3 File Offset: 0x0002E4D3
	protected void setCurrentFocusIsMainInventory()
	{
		this.currentFocusIsMainInventory = true;
	}

	// Token: 0x060009FE RID: 2558 RVA: 0x000302DC File Offset: 0x0002E4DC
	protected void setCurrentFocusIsSecondaryInventory()
	{
		this.currentFocusIsMainInventory = false;
	}

	// Token: 0x060009FF RID: 2559
	protected abstract void interactWithCurrentItemFromMainInventory();

	// Token: 0x06000A00 RID: 2560
	protected abstract void interactWithCurrentItemFromSecondaryInventory();

	// Token: 0x06000A01 RID: 2561 RVA: 0x000302E5 File Offset: 0x0002E4E5
	protected virtual void selectCurrentItemFromMain()
	{
		this.setMainTextBuffer(this.getMainInventory().printComparativeItemStats(this.dataControl.getCurrentPC()));
	}

	// Token: 0x06000A02 RID: 2562 RVA: 0x00030303 File Offset: 0x0002E503
	protected virtual void selectCurrentItemFromSecondary()
	{
		this.setMainTextBuffer(this.getSecondaryInventory().printComparativeItemStats(this.dataControl.getCurrentPC()));
	}

	// Token: 0x06000A03 RID: 2563 RVA: 0x00030321 File Offset: 0x0002E521
	protected virtual Inventory getMainInventory()
	{
		return this.dataControl.getInventory();
	}

	// Token: 0x06000A04 RID: 2564 RVA: 0x0003032E File Offset: 0x0002E52E
	protected virtual Inventory getSecondaryInventory()
	{
		return this.dataControl.getTerrainInventory();
	}

	// Token: 0x06000A05 RID: 2565 RVA: 0x0003033C File Offset: 0x0002E53C
	protected virtual void setGUIData(List<string> buttonList, SkaldInstanceObject focusPC)
	{
		base.setGUIData();
		this.gridUI.update(focusPC, this.getMainInventory(), this.getSecondaryInventory(), this.dataControl.getCurrentPC());
		this.guiControl.setSecondaryDescription(this.secondaryTextBuffer);
		base.setButtons(buttonList, "Exit");
		this.guiControl.setSheetDescription(this.getMainTextBuffer());
		this.guiControl.setSheetHeader(this.getMainInventory().getName());
		this.guiControl.revealAll();
	}

	// Token: 0x040002A2 RID: 674
	protected UIInventorySheetBase gridUI;

	// Token: 0x040002A3 RID: 675
	private bool currentFocusIsMainInventory = true;

	// Token: 0x040002A4 RID: 676
	private Character currentCharacter;
}
