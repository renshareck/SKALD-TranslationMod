using System;
using SkaldEnums;

// Token: 0x0200009E RID: 158
public class SettingsGameplayState : SettingsBaseListState
{
	// Token: 0x06000A9F RID: 2719 RVA: 0x000333B3 File Offset: 0x000315B3
	public SettingsGameplayState(DataControl dataControl) : base(dataControl, GlobalSettings.getGamePlaySettings())
	{
		this.stateId = SkaldStates.SettingsGameplay;
	}
}
