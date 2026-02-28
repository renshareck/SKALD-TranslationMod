using System;
using System.Collections.Generic;
using SkaldEnums;

// Token: 0x02000076 RID: 118
public class CombatContinue : CombatBaseState
{
	// Token: 0x0600099E RID: 2462 RVA: 0x0002E269 File Offset: 0x0002C469
	public CombatContinue(DataControl dataControl) : base(dataControl)
	{
		this.stateId = SkaldStates.CombatContinue;
	}

	// Token: 0x0600099F RID: 2463 RVA: 0x0002E27A File Offset: 0x0002C47A
	protected override bool testNewState()
	{
		if (base.testNewState())
		{
			return true;
		}
		if (this.combatEncounter.isStatePlanningPlayer())
		{
			this.setTargetState(SkaldStates.CombatPlanning);
			return true;
		}
		return false;
	}

	// Token: 0x060009A0 RID: 2464 RVA: 0x0002E2A0 File Offset: 0x0002C4A0
	public override void update()
	{
		if (this.testNewState())
		{
			return;
		}
		base.update();
		this.setGUIData();
		if (this.combatEncounter.isStateFailedAction())
		{
			this.combatEncounter.gotoNextState();
			this.buttonRowInputIndex = -1;
			return;
		}
		base.testAutoResolve();
		if (this.combatEncounter.shouldIAutoResolve())
		{
			return;
		}
		if (this.buttonRowInputIndex == -1 && (SkaldIO.getMouseUp(0) || this.guiControl.contextualButtonWasPressed()))
		{
			this.dataControl.nextCombatState();
			this.buttonRowInputIndex = -1;
			return;
		}
		if (this.buttonRowInputIndex == 0 && !this.combatEncounter.isBattlefieldEmpty())
		{
			this.combatEncounter.gotoNextState();
		}
	}

	// Token: 0x060009A1 RID: 2465 RVA: 0x0002E348 File Offset: 0x0002C548
	protected override void setButtonData()
	{
		if (this.combatEncounter.shouldIAutoResolve())
		{
			return;
		}
		List<UIButtonControlBase.ButtonData> combatOrderButtonRow = new List<UIButtonControlBase.ButtonData>
		{
			new UIButtonControlBase.ButtonData(CombatContinue.textureContinue, "Continue")
		};
		this.guiControl.setCombatOrderButtonRow(combatOrderButtonRow);
	}

	// Token: 0x04000277 RID: 631
	private static TextureTools.TextureData textureContinue = TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/MenuBarBoxContinue");
}
