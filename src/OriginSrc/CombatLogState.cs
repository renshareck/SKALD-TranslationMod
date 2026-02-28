using System;
using System.Collections.Generic;
using SkaldEnums;

// Token: 0x020000A6 RID: 166
public class CombatLogState : ListSheetBaseState
{
	// Token: 0x06000AB5 RID: 2741 RVA: 0x00033AEC File Offset: 0x00031CEC
	public CombatLogState(DataControl dataControl) : base(dataControl)
	{
		this.guiControl = new GUIControlLog();
		this.stateId = SkaldStates.Factions;
		this.list = CombatLog.getLog();
		this.list.setLastObjectAsCurrentObject();
		if (dataControl.isCombatActive())
		{
			this.allowQuickButtons = false;
		}
	}

	// Token: 0x06000AB6 RID: 2742 RVA: 0x00033B38 File Offset: 0x00031D38
	public override void update()
	{
		if (this.testExit())
		{
			this.setTargetState(SkaldStates.CombatPlanning);
			return;
		}
		if (!this.dataControl.isCombatActive())
		{
			return;
		}
		if (this.numericInputIndex == 1)
		{
			CombatLog.clearLog();
		}
		base.update();
		this.setGUIData();
	}

	// Token: 0x06000AB7 RID: 2743 RVA: 0x00033B74 File Offset: 0x00031D74
	protected override void setGUIData()
	{
		base.setGUIData();
		base.setButtons(new List<string>
		{
			"Clear Log"
		}, "Exit");
		this.guiControl.setSheetDescription(this.list.getCurrentObjectDescription());
		this.guiControl.setSheetHeader("Combat Log");
		this.guiControl.revealAll();
	}
}
