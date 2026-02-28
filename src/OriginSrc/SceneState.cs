using System;
using System.Collections.Generic;
using SkaldEnums;

// Token: 0x02000098 RID: 152
public class SceneState : SceneBaseState
{
	// Token: 0x06000A8B RID: 2699 RVA: 0x00032D7C File Offset: 0x00030F7C
	public SceneState(DataControl dataControl) : base(dataControl)
	{
		this.activateState();
		this.setGUIData();
		if (!this.scene.showImage())
		{
			base.setNoImage();
		}
		AudioControl.playQuillSound();
		this.guiControl.setMainMenuButton();
	}

	// Token: 0x06000A8C RID: 2700 RVA: 0x00032DCB File Offset: 0x00030FCB
	public override StateBase activateState()
	{
		this.scene = this.dataControl.getCurrentScene();
		this.dataControl.setDescription("");
		return this;
	}

	// Token: 0x06000A8D RID: 2701 RVA: 0x00032DF0 File Offset: 0x00030FF0
	public override void update()
	{
		this.scene = this.dataControl.getCurrentScene();
		if (this.scene.shouldImageBeCleared())
		{
			if (!base.drawNoImage())
			{
				base.setNoImage();
			}
		}
		else
		{
			base.clearNoImage();
		}
		base.update();
		if (SkaldIO.getPressedEscapeKey() || this.guiControl.mainMenuButtonWasPressed())
		{
			this.setTargetState(SkaldStates.Menu);
		}
		if (this.dataControl.newSceneMounted)
		{
			this.setGUIData();
		}
		if (this.dataControl.newSceneMounted)
		{
			this.dataControl.newSceneMounted = false;
		}
		if (this.numericInputIndex != -1 && this.numericInputIndex < this.scene.getSceneOptions().Count)
		{
			this.dataControl.setSceneInput(this.numericInputIndex);
			this.numericInputIndex = -1;
			return;
		}
		if (!this.guiControl.mainImageIsSet())
		{
			this.guiControl.setMap(this.dataControl.currentMap.getIllustratedOutputMap());
		}
	}

	// Token: 0x06000A8E RID: 2702 RVA: 0x00032EE0 File Offset: 0x000310E0
	protected override void setGUIData()
	{
		this.scene = this.dataControl.getCurrentScene();
		if (this.scene == null)
		{
			return;
		}
		base.setGUIData();
		List<string> sceneOptions = this.scene.getSceneOptions();
		this.guiControl.setNumericButtons(sceneOptions);
		if (!base.drawNoImage())
		{
			this.guiControl.setMainImage(this.dataControl.getSceneImagePath());
		}
		this.guiControl.setSceneDescription(this.scene.getDescription());
		if (this.title != this.scene.getName() && this.scene.getName() != "")
		{
			this.title = this.scene.getName();
		}
		if (this.title != "")
		{
			this.guiControl.setPrimaryHeader(this.title);
		}
		this.guiControl.setSheetHeader("");
		this.guiControl.resetCurrentSelectOption();
	}

	// Token: 0x040002CC RID: 716
	private string title = "";

	// Token: 0x040002CD RID: 717
	private Scene scene;
}
