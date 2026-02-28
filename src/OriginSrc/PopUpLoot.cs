using System;
using System.Collections.Generic;

// Token: 0x0200004A RID: 74
public class PopUpLoot : PopUpBase
{
	// Token: 0x06000855 RID: 2133 RVA: 0x00028958 File Offset: 0x00026B58
	public PopUpLoot(Inventory inventory) : base(inventory.getName(), new List<string>
	{
		"1) Loot",
		"2) Inv.",
		"3) Leave"
	})
	{
		this.inventory = inventory;
		inventory.checkIntegrity();
		Prop currentProp = MainControl.getDataControl().getCurrentProp();
		if (currentProp != null)
		{
			base.setMainTextContent(currentProp.getName());
		}
	}

	// Token: 0x06000856 RID: 2134 RVA: 0x000289BE File Offset: 0x00026BBE
	public override bool allowTooltips()
	{
		return true;
	}

	// Token: 0x06000857 RID: 2135 RVA: 0x000289C4 File Offset: 0x00026BC4
	public override void handle()
	{
		if (base.isHandled())
		{
			return;
		}
		PopUpBase.PopUpUISystemInventory popUpUISystemInventory = this.uiElements as PopUpBase.PopUpUISystemInventory;
		popUpUISystemInventory.update(this.inventory);
		base.handle();
		int gridHoverIndex = popUpUISystemInventory.getGridHoverIndex();
		if (gridHoverIndex != -1)
		{
			this.inventory.getObjectByIndex(gridHoverIndex);
		}
		base.setTertiaryTextContent(this.inventory.getCurrentItemNameAndAmount());
		int gridButtonPressIndexLeft = popUpUISystemInventory.getGridButtonPressIndexLeft();
		if (gridButtonPressIndexLeft != -1)
		{
			if (gridButtonPressIndexLeft < this.inventory.getCount())
			{
				MainControl.getDataControl().getCurrentPC().getInventory().addItem(this.inventory.removeCurrentItemStack());
			}
			if (this.inventory.isEmpty())
			{
				this.handle(true);
			}
		}
		int gridButtonPressIndexRight = popUpUISystemInventory.getGridButtonPressIndexRight();
		if (gridButtonPressIndexRight != -1 && gridButtonPressIndexRight < this.inventory.getCount())
		{
			ToolTipPrinter.setToolTipWithRules(this.inventory.getCurrentObject().printComparativeStats(MainControl.getDataControl().getCurrentPC()));
		}
		if (SkaldIO.getPressedMainInteractKey() || SkaldIO.getPressedNumericButton1() || (base.getButtonPressIndex() == 0 && !ToolTipPrinter.isMouseOverTooltip()))
		{
			if (!this.inventory.isEmpty())
			{
				MainControl.getDataControl().getAllItemsInTargetTile();
			}
			this.handle(true);
			return;
		}
		if (SkaldIO.getPressedNumericButton2() || (base.getButtonPressIndex() == 1 && !ToolTipPrinter.isMouseOverTooltip()))
		{
			MainControl.getDataControl().gotoContainerState(this.inventory);
			this.handle(true);
			return;
		}
		if (SkaldIO.getPressedEscapeKey() || SkaldIO.getPressedNumericButton3() || (base.getButtonPressIndex() == 2 && !ToolTipPrinter.isMouseOverTooltip()))
		{
			this.handle(true);
		}
	}

	// Token: 0x06000858 RID: 2136 RVA: 0x00028B34 File Offset: 0x00026D34
	protected override void setPopUpUI(string description, List<string> buttonList)
	{
		this.uiElements = new PopUpBase.PopUpUISystemInventory(description, buttonList);
	}

	// Token: 0x040001C5 RID: 453
	private Inventory inventory;
}
