using System;
using System.Collections.Generic;

// Token: 0x0200009A RID: 154
public class SettingsBaseListState : SettingsBaseState
{
	// Token: 0x06000A90 RID: 2704 RVA: 0x00032FED File Offset: 0x000311ED
	public SettingsBaseListState(DataControl dataControl, GlobalSettings.SettingsCollection settingsList) : base(dataControl)
	{
		this.listAsSettingsList = settingsList;
		this.setList();
	}

	// Token: 0x06000A91 RID: 2705 RVA: 0x00033004 File Offset: 0x00031204
	protected override void setList()
	{
		this.list = this.listAsSettingsList;
		this.list.setMaxPageSize(11);
		this.sliderControl = this.listAsSettingsList.createAndPopulateSliderControls(this.sliderControl);
		(this.guiControl as GUIControlSettings).setSliderControls(this.sliderControl);
	}

	// Token: 0x06000A92 RID: 2706 RVA: 0x00033058 File Offset: 0x00031258
	public override void update()
	{
		if (SkaldIO.getPressedEscapeKey())
		{
			this.clearAndGoToOverland();
			return;
		}
		base.update();
		if (this.numericInputIndex != -1)
		{
			if (this.numericInputIndex == 1)
			{
				this.listAsSettingsList.reset();
				this.setList();
			}
			this.numericInputIndex = -1;
		}
		this.setMainTextBuffer(this.sliderControl.getHoverButtonDescription());
		this.setGUIData();
	}

	// Token: 0x06000A93 RID: 2707 RVA: 0x000330BC File Offset: 0x000312BC
	protected override void setGUIData()
	{
		base.setGUIData();
		if (this.sliderControl != null)
		{
			this.sliderControl.update();
		}
		List<string> numericButtons = new List<string>
		{
			"Exit",
			"Reset"
		};
		this.guiControl.setNumericButtons(numericButtons);
		this.guiControl.revealAll();
	}

	// Token: 0x040002CE RID: 718
	private GlobalSettings.SettingsCollection listAsSettingsList;

	// Token: 0x040002CF RID: 719
	private UITextSliderControl sliderControl;
}
