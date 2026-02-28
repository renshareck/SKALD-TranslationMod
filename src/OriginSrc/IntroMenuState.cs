using System;
using System.Collections.Generic;
using SkaldEnums;

// Token: 0x02000086 RID: 134
public class IntroMenuState : BaseMenuState
{
	// Token: 0x06000A22 RID: 2594 RVA: 0x00030A3D File Offset: 0x0002EC3D
	public IntroMenuState(DataControl dataControl) : base(dataControl)
	{
	}

	// Token: 0x06000A23 RID: 2595 RVA: 0x00030A48 File Offset: 0x0002EC48
	public override void update()
	{
		base.update();
		if (this.numericInputIndex != -1)
		{
			if (this.numericInputIndex == 0)
			{
				if (this.dataControl.loadLast())
				{
					this.setTargetState(SkaldStates.Overland);
				}
			}
			else if (this.numericInputIndex == 1)
			{
				PopUpControl.addPopUpVisualStyle(this);
			}
			else if (this.numericInputIndex == 2)
			{
				this.setTargetState(SkaldStates.LoadMenu);
			}
			else if (this.numericInputIndex == 3)
			{
				this.setTargetState(SkaldStates.LoadModule);
			}
			else if (this.numericInputIndex == 4)
			{
				this.setTargetState(SkaldStates.SettingsGameplay);
			}
			else if (this.numericInputIndex == 5)
			{
				this.setTargetState(SkaldStates.Credits);
			}
			else if (this.numericInputIndex == 6)
			{
				PopUpControl.addPopUpQuitGame();
			}
			this.numericInputIndex = -1;
		}
		this.setGUIData();
	}

	// Token: 0x06000A24 RID: 2596 RVA: 0x00030AFD File Offset: 0x0002ECFD
	public void beginCharacterCreations()
	{
		this.dataControl.addPremadeCharacters();
		this.dataControl.editCurrentCharacterAsMain();
		this.setTargetState(SkaldStates.DifficultySelector);
	}

	// Token: 0x06000A25 RID: 2597 RVA: 0x00030B20 File Offset: 0x0002ED20
	protected override void setGUIData()
	{
		base.setGUIData();
		string item = "...";
		if (SkaldSaveControl.isLastSaveCached())
		{
			item = "Continue Game";
		}
		List<string> numericButtons = new List<string>
		{
			item,
			"New Game",
			"Load",
			"Start Module",
			"Settings",
			"Credits",
			"Quit"
		};
		this.guiControl.setNumericButtons(numericButtons);
		this.guiControl.revealAll();
	}
}
