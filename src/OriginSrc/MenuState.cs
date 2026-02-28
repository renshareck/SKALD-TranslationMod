using System;
using System.Collections.Generic;
using SkaldEnums;

// Token: 0x0200008A RID: 138
public class MenuState : BaseMenuState
{
	// Token: 0x06000A40 RID: 2624 RVA: 0x000311D2 File Offset: 0x0002F3D2
	public MenuState(DataControl dataControl) : base(dataControl)
	{
	}

	// Token: 0x06000A41 RID: 2625 RVA: 0x000311DC File Offset: 0x0002F3DC
	public override void update()
	{
		base.update();
		if (SkaldIO.getPressedEscapeKey())
		{
			this.setTargetState(SkaldStates.Overland);
			return;
		}
		if (this.numericInputIndex != -1)
		{
			if (this.numericInputIndex == 0)
			{
				this.setTargetState(SkaldStates.Overland);
			}
			if (this.numericInputIndex == 1)
			{
				this.setTargetState(SkaldStates.LoadMenu);
			}
			if (this.numericInputIndex == 2 && !this.dataControl.isCombatActive() && !this.dataControl.isSceneMounted())
			{
				this.setTargetState(SkaldStates.SaveMenu);
			}
			if (this.numericInputIndex == 3)
			{
				this.setTargetState(SkaldStates.SettingsGameplay);
			}
			if (this.numericInputIndex == 4)
			{
				PopUpControl.addPopUpQuitGame();
			}
			this.numericInputIndex = -1;
		}
		this.setGUIData();
	}

	// Token: 0x06000A42 RID: 2626 RVA: 0x00031280 File Offset: 0x0002F480
	protected override void setGUIData()
	{
		base.setGUIData();
		string item;
		if (this.dataControl.isCombatActive() || this.dataControl.isSceneMounted())
		{
			item = "...";
		}
		else
		{
			item = "Save";
		}
		List<string> numericButtons = new List<string>
		{
			"Continue",
			"Load",
			item,
			"Settings",
			"Quit"
		};
		this.guiControl.setNumericButtons(numericButtons);
		this.guiControl.revealAll();
	}
}
