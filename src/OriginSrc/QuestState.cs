using System;
using SkaldEnums;

// Token: 0x020000AC RID: 172
public class QuestState : JournalInfoState
{
	// Token: 0x06000ACF RID: 2767 RVA: 0x0003418F File Offset: 0x0003238F
	public QuestState(DataControl dataControl) : base(dataControl)
	{
		this.stateId = SkaldStates.Quests;
		this.list = dataControl.getQuestControl().getQuestList();
		this.setGUIData();
	}
}
