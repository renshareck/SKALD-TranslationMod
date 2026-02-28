using System;
using SkaldEnums;

// Token: 0x0200009C RID: 156
public class SettingsDifficulty : SettingsBaseListState
{
	// Token: 0x06000A9C RID: 2716 RVA: 0x0003336F File Offset: 0x0003156F
	public SettingsDifficulty(DataControl dataControl) : base(dataControl, GlobalSettings.getDifficultySettings())
	{
		this.stateId = SkaldStates.SettingsDifficulty;
	}
}
