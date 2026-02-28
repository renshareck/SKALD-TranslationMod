using System;
using SkaldEnums;

// Token: 0x020000A0 RID: 160
public class SettingsVideo : SettingsBaseListState
{
	// Token: 0x06000AA3 RID: 2723 RVA: 0x00033565 File Offset: 0x00031765
	public SettingsVideo(DataControl dataControl) : base(dataControl, GlobalSettings.getDisplaySettings())
	{
		this.stateId = SkaldStates.SettingsVideo;
	}

	// Token: 0x06000AA4 RID: 2724 RVA: 0x0003357B File Offset: 0x0003177B
	public override void update()
	{
		base.update();
		(this.guiControl as GUIControlSettings).updateMainDescriptionFontData();
	}
}
