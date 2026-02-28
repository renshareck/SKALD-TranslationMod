using System;
using SkaldEnums;

// Token: 0x0200009D RID: 157
public class SettingsFontSelectionState : SettingsBaseListState
{
	// Token: 0x06000A9D RID: 2717 RVA: 0x00033385 File Offset: 0x00031585
	public SettingsFontSelectionState(DataControl dataControl) : base(dataControl, GlobalSettings.getFontSettings())
	{
		this.stateId = SkaldStates.SettingsFonts;
	}

	// Token: 0x06000A9E RID: 2718 RVA: 0x0003339B File Offset: 0x0003159B
	public override void update()
	{
		base.update();
		(this.guiControl as GUIControlSettings).updateMainDescriptionFontData();
	}
}
