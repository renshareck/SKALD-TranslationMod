using System;
using SkaldEnums;

// Token: 0x020000A7 RID: 167
public class FactionsState : JournalInfoState
{
	// Token: 0x06000AB8 RID: 2744 RVA: 0x00033BD3 File Offset: 0x00031DD3
	public FactionsState(DataControl dataControl) : base(dataControl)
	{
		this.stateId = SkaldStates.Factions;
		this.list = FactionControl.getFactionList();
		this.setGUIData();
	}
}
