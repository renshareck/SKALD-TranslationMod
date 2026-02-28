using System;
using System.Collections.Generic;
using SkaldEnums;

// Token: 0x02000094 RID: 148
public class GameOverState : SceneBaseState
{
	// Token: 0x06000A71 RID: 2673 RVA: 0x0003241A File Offset: 0x0003061A
	public GameOverState(DataControl dataControl) : base(dataControl)
	{
		this.guiControl.setMainImage("BlackScreen");
		AudioControl.playMusic("graveyard");
		this.setGUIData();
		this.activateState();
	}

	// Token: 0x06000A72 RID: 2674 RVA: 0x0003244C File Offset: 0x0003064C
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
				else
				{
					this.setTargetState(SkaldStates.LoadMenu);
				}
			}
			else if (this.numericInputIndex == 1)
			{
				this.setTargetState(SkaldStates.LoadMenu);
			}
			else if (this.numericInputIndex == 2)
			{
				MainControl.log("RESTARTING");
				MainControl.restartGame();
				return;
			}
			this.numericInputIndex = -1;
		}
	}

	// Token: 0x06000A73 RID: 2675 RVA: 0x000324C4 File Offset: 0x000306C4
	protected override void setGUIData()
	{
		base.setGUIData();
		string sceneDescription = "You have died - The Dragon awakens.\n\nReload a previous save or restart the game!\n\nGame too difficult? You can set the Difficulty in Settings -> Difficulty";
		this.guiControl.setSceneDescription(sceneDescription);
		this.guiControl.setMainImage("TheDragon");
		List<string> numericButtons = new List<string>
		{
			"Load Last Save",
			"Load Other Save",
			"Restart Game"
		};
		this.guiControl.setNumericButtons(numericButtons);
		this.guiControl.setSheetHeader("");
		this.guiControl.setPrimaryHeader("Game Over!");
		this.guiControl.revealAll();
	}
}
