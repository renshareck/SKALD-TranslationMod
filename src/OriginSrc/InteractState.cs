using System;
using System.Collections.Generic;
using SkaldEnums;

// Token: 0x02000095 RID: 149
public class InteractState : SceneBaseState
{
	// Token: 0x06000A74 RID: 2676 RVA: 0x00032558 File Offset: 0x00030758
	public InteractState(DataControl dataControll) : base(dataControll)
	{
		this.activateState();
		this.setMainTextBuffer(this.party.getCurrentCharacter().getDescription());
		this.setGUIData();
		if (dataControll.getSceneImagePath() == "")
		{
			base.setNoImage();
		}
	}

	// Token: 0x06000A75 RID: 2677 RVA: 0x000325A7 File Offset: 0x000307A7
	public override StateBase activateState()
	{
		base.activateState();
		this.party = this.dataControl.getInteractParty();
		return this;
	}

	// Token: 0x06000A76 RID: 2678 RVA: 0x000325C4 File Offset: 0x000307C4
	public override void update()
	{
		base.update();
		if (SkaldIO.getPressedEscapeKey())
		{
			this.dataControl.clearInteractParty();
			this.setTargetState(SkaldStates.Overland);
			return;
		}
		if (this.numericInputIndex != -1)
		{
			if (this.numericInputIndex == 0)
			{
				if (this.party.hasTalkTrigger())
				{
					this.setTargetState(SkaldStates.Scene);
					this.party.processTalkTrigger();
					this.dataControl.clearInteractParty();
				}
				else
				{
					this.setMainTextBuffer(this.party.getName() + " has nothing to talk about.");
				}
				this.setGUIData();
			}
			else if (this.numericInputIndex == 1)
			{
				if (this.party.willTrade())
				{
					this.setTargetState(SkaldStates.Trade);
					this.dataControl.mountStoreFromCharacter();
				}
				else
				{
					this.setMainTextBuffer(this.party.getName() + " does not want to trade.");
					this.setGUIData();
				}
			}
			else if (this.numericInputIndex == 2)
			{
				if (this.party.canBeAttacked())
				{
					this.setTargetState(SkaldStates.AttackPrompt);
				}
				else
				{
					this.setMainTextBuffer(this.party.getName() + " does not want to fight.");
					this.setGUIData();
				}
			}
			else if (this.numericInputIndex == 3)
			{
				this.setTargetState(SkaldStates.Overland);
				this.dataControl.clearInteractParty();
			}
			this.numericInputIndex = -1;
			return;
		}
		if (base.drawNoImage())
		{
			this.guiControl.setMap(this.dataControl.currentMap.getIllustratedOutputMap());
		}
	}

	// Token: 0x06000A77 RID: 2679 RVA: 0x00032734 File Offset: 0x00030934
	protected override void setGUIData()
	{
		base.setGUIData();
		if (!base.drawNoImage())
		{
			this.guiControl.setMainImage(this.dataControl.getSceneImagePath());
		}
		List<string> numericButtons = new List<string>
		{
			"Talk",
			"Trade / Steal",
			"Attack",
			"Leave"
		};
		this.guiControl.setNumericButtons(numericButtons);
		this.guiControl.setSheetHeader("");
		this.guiControl.setPrimaryHeader(this.party.getName());
		this.guiControl.setSceneDescription(this.getMainTextBuffer());
	}

	// Token: 0x040002C7 RID: 711
	private Party party;
}
