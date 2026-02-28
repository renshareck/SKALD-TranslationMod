using System;
using System.Collections.Generic;
using SkaldEnums;

// Token: 0x020000A9 RID: 169
public abstract class JournalInfoState : ListSheetBaseState
{
	// Token: 0x06000AC4 RID: 2756 RVA: 0x00033F7D File Offset: 0x0003217D
	protected JournalInfoState(DataControl dataControl) : base(dataControl)
	{
	}

	// Token: 0x06000AC5 RID: 2757 RVA: 0x00033F86 File Offset: 0x00032186
	protected override void setStateSelector()
	{
		base.setStateSelector();
		this.stateSelector.add(SkaldStates.Journal, "Journal");
		this.stateSelector.add(SkaldStates.Quests, "Quests");
		this.stateSelector.add(SkaldStates.Factions, "Factions");
	}

	// Token: 0x06000AC6 RID: 2758 RVA: 0x00033FC4 File Offset: 0x000321C4
	public override void update()
	{
		if (this.testExit())
		{
			return;
		}
		base.update();
		this.setGUIData();
	}

	// Token: 0x06000AC7 RID: 2759 RVA: 0x00033FDC File Offset: 0x000321DC
	protected override void setGUIData()
	{
		base.setGUIData();
		base.setButtons(new List<string>(), "Exit");
		this.guiControl.setSheetHeader(this.list.getName());
		if (this.list.isEmpty())
		{
			this.guiControl.setSheetDescription("No entries have been added.");
		}
		else
		{
			this.guiControl.setSheetDescription(this.list.getCurrentObject().getFullDescriptionAndHeader());
		}
		this.guiControl.revealAll();
	}
}
