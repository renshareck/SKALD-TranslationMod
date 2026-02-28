using System;
using SkaldEnums;

// Token: 0x020000AA RID: 170
public class JournalState : JournalInfoState
{
	// Token: 0x06000AC8 RID: 2760 RVA: 0x0003405A File Offset: 0x0003225A
	public JournalState(DataControl dataControl) : base(dataControl)
	{
		this.stateId = SkaldStates.Journal;
		this.list = dataControl.getJournal().getJournalDataList();
		this.setGUIData();
	}
}
