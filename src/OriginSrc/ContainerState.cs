using System;
using System.Collections.Generic;

// Token: 0x0200007F RID: 127
public class ContainerState : InventoryBaseState
{
	// Token: 0x060009E4 RID: 2532 RVA: 0x0002FA18 File Offset: 0x0002DC18
	public ContainerState(DataControl dataControl) : base(dataControl)
	{
		if (this.getSecondaryInventory() != null)
		{
			this.getSecondaryInventory().checkIntegrity();
		}
	}

	// Token: 0x060009E5 RID: 2533 RVA: 0x0002FA34 File Offset: 0x0002DC34
	protected override void createGUI()
	{
		this.gridUI = new UIInventorySheetContainer();
		this.guiControl = new GUIControlContainer(this.gridUI);
	}

	// Token: 0x060009E6 RID: 2534 RVA: 0x0002FA54 File Offset: 0x0002DC54
	public override void update()
	{
		base.update();
		if (this.testExit())
		{
			return;
		}
		if (this.numericInputIndex != -1)
		{
			if (this.numericInputIndex == 1)
			{
				this.dataControl.getAllItemsInTargetTile();
				this.clearAndGoToOverland();
			}
			else if (this.numericInputIndex == 2)
			{
				if (base.getCurrentFocusIsMainInventory())
				{
					this.dataControl.dropItemStack();
				}
				else
				{
					this.dataControl.getItemStackInTargetTile();
				}
			}
			else if (this.numericInputIndex == 3)
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
		}
		this.numericInputIndex = -1;
		this.setGUIData();
	}

	// Token: 0x060009E7 RID: 2535 RVA: 0x0002FAED File Offset: 0x0002DCED
	protected override void interactWithCurrentItemFromMainInventory()
	{
		this.dataControl.dropItem();
		this.selectCurrentItemFromSecondary();
	}

	// Token: 0x060009E8 RID: 2536 RVA: 0x0002FB01 File Offset: 0x0002DD01
	protected override void interactWithCurrentItemFromSecondaryInventory()
	{
		this.dataControl.getItemInTargetTile();
		this.selectCurrentItemFromMain();
	}

	// Token: 0x060009E9 RID: 2537 RVA: 0x0002FB14 File Offset: 0x0002DD14
	protected override Inventory getSecondaryInventory()
	{
		return this.dataControl.getContainerInventory();
	}

	// Token: 0x060009EA RID: 2538 RVA: 0x0002FB24 File Offset: 0x0002DD24
	protected override void setGUIData()
	{
		if (this.getMainInventory() == null)
		{
			return;
		}
		base.setGUIData(new List<string>
		{
			"Get All",
			"Transfer Stack",
			"Transfer Item"
		}, this.dataControl.getCurrentPC());
		if (this.dataControl.getCurrentProp() != null && this.dataControl.isContainerMounted())
		{
			this.guiControl.setSheetHeader(this.dataControl.getCurrentProp().getName());
			return;
		}
		this.guiControl.setSheetHeader("Ground");
	}
}
