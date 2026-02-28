using System;
using System.Collections.Generic;
using SkaldEnums;

// Token: 0x02000075 RID: 117
public abstract class CombatBaseState : StateBase
{
	// Token: 0x06000993 RID: 2451 RVA: 0x0002DECF File Offset: 0x0002C0CF
	public CombatBaseState(DataControl dataControl) : base(dataControl)
	{
		this.guiControl = new GUIControlCombat();
		this.activateState();
	}

	// Token: 0x06000994 RID: 2452 RVA: 0x0002DF09 File Offset: 0x0002C109
	public override StateBase activateState()
	{
		this.combatEncounter = this.dataControl.getCombatEncounter();
		base.disableCharacterSwap();
		AudioControl.playCombatMusic();
		this.setGUIData();
		return this;
	}

	// Token: 0x06000995 RID: 2453 RVA: 0x0002DF30 File Offset: 0x0002C130
	protected bool updateEmergencyEscapeClock()
	{
		this.emergencyEscapeClock.tick();
		if (this.emergencyEscapeClock.isTimerZero())
		{
			MainControl.logError("Combat resolution appears hung in state: " + this.stateId.ToString() + ".");
			PopUpControl.addPopUpOK("An error was logged: Combat resolution appears hung in state: " + this.stateId.ToString() + ". Report this message to the dev!");
			this.combatEncounter.getCurrentCharacter().pass();
			this.combatEncounter.getCurrentCharacter().clearNavigationCourse();
			this.combatEncounter.forceNextCharacter();
			return true;
		}
		return false;
	}

	// Token: 0x06000996 RID: 2454 RVA: 0x0002DFD0 File Offset: 0x0002C1D0
	protected virtual bool testNewState()
	{
		if (!this.dataControl.isCombatActive())
		{
			this.setTargetState(SkaldStates.Overland);
			return true;
		}
		if (this.combatEncounter.isPartyVictorious() && !this.combatEncounter.isStateResolving())
		{
			this.setTargetState(SkaldStates.CombatOver);
			return true;
		}
		if (this.combatEncounter.isStateAContinueState() && this.stateId != SkaldStates.CombatContinue)
		{
			this.setTargetState(SkaldStates.CombatContinue);
			return true;
		}
		if (this.combatEncounter.isStatePlanningPlayer() && this.stateId != SkaldStates.CombatPlanning)
		{
			this.setTargetState(SkaldStates.CombatPlanning);
			return true;
		}
		if (this.combatEncounter.isStateResolving() && this.stateId != SkaldStates.CombatResolve)
		{
			this.setTargetState(SkaldStates.CombatResolve);
			return true;
		}
		return false;
	}

	// Token: 0x06000997 RID: 2455 RVA: 0x0002E07C File Offset: 0x0002C27C
	protected void testAutoResolve()
	{
		if (this.combatEncounter.isPartyVictorious())
		{
			this.combatEncounter.autoResolve = false;
			return;
		}
		if (this.combatEncounter.shouldIAutoResolve())
		{
			if (SkaldIO.anyKeyPressed())
			{
				this.combatEncounter.autoResolve = false;
			}
			else if (this.autoResolveDelay.isTimerZero() && this.getCurrentCharacter() != null && this.getCurrentCharacter().physicMovementComplete())
			{
				this.combatEncounter.gotoNextState();
				this.autoResolveDelay.reset();
			}
			else
			{
				this.autoResolveDelay.tick();
			}
			this.buttonRowInputIndex = -1;
		}
	}

	// Token: 0x06000998 RID: 2456 RVA: 0x0002E114 File Offset: 0x0002C314
	protected virtual void setCombatDescription()
	{
		string text = this.guiControl.getButtonRowHovertext();
		if (text == "")
		{
			text = this.combatEncounter.getDescription() + "\n\n";
		}
		this.guiControl.setSecondaryDescription(text);
	}

	// Token: 0x06000999 RID: 2457 RVA: 0x0002E15C File Offset: 0x0002C35C
	protected virtual void setButtonData()
	{
		List<UIButtonControlBase.ButtonData> combatOrderButtonRow = new List<UIButtonControlBase.ButtonData>();
		this.guiControl.setCombatOrderButtonRow(combatOrderButtonRow);
	}

	// Token: 0x0600099A RID: 2458 RVA: 0x0002E17B File Offset: 0x0002C37B
	protected Character getCurrentCharacter()
	{
		return this.combatEncounter.getCurrentCharacter();
	}

	// Token: 0x0600099B RID: 2459 RVA: 0x0002E188 File Offset: 0x0002C388
	protected bool shouldIDrawUI()
	{
		return this.combatEncounter.shouldIDrawUI();
	}

	// Token: 0x0600099C RID: 2460 RVA: 0x0002E195 File Offset: 0x0002C395
	protected virtual void printActionCounter()
	{
		this.guiControl.setActionCounter(this.getCurrentCharacter(), null);
	}

	// Token: 0x0600099D RID: 2461 RVA: 0x0002E1AC File Offset: 0x0002C3AC
	protected override void setGUIData()
	{
		this.setCombatDescription();
		this.guiControl.setPrimaryHeader(this.combatEncounter.getTitle());
		this.setButtonData();
		this.guiControl.setInitiativeCounter(this.combatEncounter.getUIInitiativeList());
		if (this.getCurrentCharacter() == null)
		{
			this.guiControl.setMap(this.dataControl.currentMap.getIllustratedOutputMap());
		}
		else
		{
			if (this.getCurrentCharacter().isPC())
			{
				this.printActionCounter();
			}
			this.guiControl.setMap(this.dataControl.currentMap.getIllustratedOutputMap(this.getCurrentCharacter(), this.shouldIDrawUI(), false, false));
		}
		this.drawPortraits();
		this.setBackground();
		this.guiControl.revealAll();
	}

	// Token: 0x04000274 RID: 628
	protected CombatEncounter combatEncounter;

	// Token: 0x04000275 RID: 629
	protected CountDownClock emergencyEscapeClock = new CountDownClock(300, false);

	// Token: 0x04000276 RID: 630
	private CountDownClock autoResolveDelay = new CountDownClock(10, false);
}
