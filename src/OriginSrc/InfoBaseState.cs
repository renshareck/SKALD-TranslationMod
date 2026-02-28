using System;
using System.Collections.Generic;

// Token: 0x0200007E RID: 126
public class InfoBaseState : StateBase
{
	// Token: 0x060009DF RID: 2527 RVA: 0x0002F80E File Offset: 0x0002DA0E
	protected InfoBaseState(DataControl dataControl) : base(dataControl)
	{
		this.setStateSelector();
	}

	// Token: 0x060009E0 RID: 2528 RVA: 0x0002F83E File Offset: 0x0002DA3E
	protected virtual bool testExit()
	{
		if (SkaldIO.getPressedEscapeKey())
		{
			this.clearAndGoToOverland();
			return true;
		}
		return false;
	}

	// Token: 0x060009E1 RID: 2529 RVA: 0x0002F850 File Offset: 0x0002DA50
	public override void update()
	{
		base.update();
		this.updateSheetQuickButtons();
		if (this.numericInputIndex == 0)
		{
			this.clearAndGoToOverland();
			return;
		}
		if (this.UIInputIndex != -1)
		{
			this.setTargetState(this.stateSelector.getStateFromIndex(this.UIInputIndex));
			return;
		}
		if (SkaldIO.getStateCarouselRightButtonPressed())
		{
			base.setNextState(1);
			return;
		}
		if (SkaldIO.getStateCarouselLeftButtonPressed())
		{
			base.setNextState(-1);
			return;
		}
		if (SkaldIO.getOptionSelectionButtonDown())
		{
			this.guiControl.setMouseToClosestOptionBelow();
			return;
		}
		if (SkaldIO.getOptionSelectionButtonUp())
		{
			this.guiControl.setMouseToClosestOptionAbove();
			return;
		}
		if (SkaldIO.getOptionSelectionButtonRight())
		{
			this.guiControl.controllerScrollSidewaysRight();
			return;
		}
		if (SkaldIO.getOptionSelectionButtonLeft())
		{
			this.guiControl.controllerScrollSidewaysLeft();
		}
	}

	// Token: 0x060009E2 RID: 2530 RVA: 0x0002F904 File Offset: 0x0002DB04
	protected override void setGUIData()
	{
		this.guiControl.setSheetHeader("");
		this.guiControl.setSheetDescription("");
		this.setSecondaryDescription();
		this.setBackground();
		if (this.dataControl.currentMap != null)
		{
			this.guiControl.setPrimaryHeader(this.dataControl.currentMap.getName());
			if (!this.guiControl.hasMap())
			{
				this.guiControl.setMap(this.dataControl.currentMap.getIllustratedOutputMap());
			}
		}
		this.drawPortraits();
	}

	// Token: 0x060009E3 RID: 2531 RVA: 0x0002F994 File Offset: 0x0002DB94
	protected void setButtons(List<string> options, string exitPrompt = "Exit")
	{
		List<string> list = new List<string>();
		list.Add(exitPrompt);
		for (int i = 0; i < 8; i++)
		{
			if (i < options.Count)
			{
				list.Add(options[i]);
			}
			else
			{
				list.Add("");
			}
		}
		this.guiControl.setNumericButtons(list);
		if (this.stateSelector != null)
		{
			this.guiControl.setTabRowButtons(this.stateSelector.getStringList(), this.stateSelector.getCurrentIndex(this.stateId));
		}
	}

	// Token: 0x0400029B RID: 667
	protected string title = "";

	// Token: 0x0400029C RID: 668
	protected string desc = "";

	// Token: 0x0400029D RID: 669
	protected string imagePath = "";
}
