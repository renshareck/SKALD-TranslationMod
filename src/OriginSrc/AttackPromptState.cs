using System;
using System.Collections.Generic;
using SkaldEnums;

// Token: 0x02000093 RID: 147
public class AttackPromptState : SceneBaseState
{
	// Token: 0x06000A6E RID: 2670 RVA: 0x000322AB File Offset: 0x000304AB
	public AttackPromptState(DataControl dataControll) : base(dataControll)
	{
		this.setMainTextBuffer("You are about to attack a non-hostile party. Are you sure?");
		this.setGUIData();
		this.activateState();
		if (dataControll.getSceneImagePath() == "")
		{
			base.setNoImage();
		}
	}

	// Token: 0x06000A6F RID: 2671 RVA: 0x000322E4 File Offset: 0x000304E4
	public override void update()
	{
		base.update();
		if (SkaldIO.getPressedEscapeKey())
		{
			this.setTargetState(SkaldStates.Interact);
			return;
		}
		if (this.numericInputIndex != -1)
		{
			if (this.numericInputIndex == 0)
			{
				this.setTargetState(SkaldStates.Interact);
			}
			else if (this.numericInputIndex == 1)
			{
				this.dataControl.getInteractParty().setHostile(true);
				this.dataControl.clearInteractParty();
				this.dataControl.launchCombat();
				return;
			}
			this.numericInputIndex = -1;
			return;
		}
		if (base.drawNoImage())
		{
			this.guiControl.setMap(this.dataControl.currentMap.getIllustratedOutputMap());
		}
	}

	// Token: 0x06000A70 RID: 2672 RVA: 0x00032380 File Offset: 0x00030580
	protected override void setGUIData()
	{
		base.setGUIData();
		if (!base.drawNoImage())
		{
			this.guiControl.setMainImage(this.dataControl.getSceneImagePath());
		}
		List<string> numericButtons = new List<string>
		{
			"Cancel",
			"Attack"
		};
		this.guiControl.setNumericButtons(numericButtons);
		this.guiControl.setSheetHeader("");
		this.guiControl.setPrimaryHeader(this.dataControl.getInteractParty().getCurrentCharacter().getName());
		this.guiControl.setSceneDescription(this.getMainTextBuffer());
	}
}
