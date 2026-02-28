using System;
using SkaldEnums;

// Token: 0x02000099 RID: 153
public class SettingsAudioState : SettingsBaseListState
{
	// Token: 0x06000A8F RID: 2703 RVA: 0x00032FD7 File Offset: 0x000311D7
	public SettingsAudioState(DataControl dataControl) : base(dataControl, GlobalSettings.getAudioSettings())
	{
		this.stateId = SkaldStates.SettingsAudio;
	}
}
