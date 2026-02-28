using System;
using SkaldEnums;
using UnityEngine;

// Token: 0x02000078 RID: 120
public class CombatPlacementState : StateBase
{
	// Token: 0x060009A8 RID: 2472 RVA: 0x0002E4E8 File Offset: 0x0002C6E8
	public CombatPlacementState(DataControl dataControl) : base(dataControl)
	{
		this.activateState();
		string bigHeader = "Deploy Your Party!";
		this.guiControl.setBigHeader(bigHeader);
		this.countDown = new CountDownClock(90, false);
		base.disableClickablePortraits();
	}

	// Token: 0x060009A9 RID: 2473 RVA: 0x0002E529 File Offset: 0x0002C729
	public override StateBase activateState()
	{
		this.combatEncounter = this.dataControl.getCombatEncounter();
		AudioControl.playCombatMusic();
		this.setGUIData();
		return this;
	}

	// Token: 0x060009AA RID: 2474 RVA: 0x0002E548 File Offset: 0x0002C748
	protected override void createGUI()
	{
		this.guiControl = new GUIControlCombat();
	}

	// Token: 0x060009AB RID: 2475 RVA: 0x0002E555 File Offset: 0x0002C755
	protected Character getCurrentCharacter()
	{
		return this.dataControl.getCurrentPC();
	}

	// Token: 0x060009AC RID: 2476 RVA: 0x0002E564 File Offset: 0x0002C764
	public override void update()
	{
		base.update();
		this.setMousePosition();
		this.updatePlacementButtons();
		this.countDown.tick();
		if (this.countDown.isTimerZero())
		{
			this.guiControl.setBigHeader("");
		}
		if (SkaldIO.getPressedNextCharacterKey())
		{
			this.dataControl.changePC(1);
		}
		if (this.guiControl.contextualButtonWasPressed())
		{
			this.setTargetState(SkaldStates.CombatPlanning);
			this.dataControl.currentMap.clearPreCombatPlacementTiles();
			this.dataControl.nextCombatState();
		}
		this.setGUIData();
	}

	// Token: 0x060009AD RID: 2477 RVA: 0x0002E5F4 File Offset: 0x0002C7F4
	protected override bool isCharacterSwapAllowed()
	{
		return true;
	}

	// Token: 0x060009AE RID: 2478 RVA: 0x0002E5F8 File Offset: 0x0002C7F8
	private void setMousePosition()
	{
		Vector2 mouseRelativeToMap = this.guiControl.getMouseRelativeToMap();
		this.combatEncounter.updateMousePosition(mouseRelativeToMap);
		if (SkaldIO.getMouseUp(0) && !this.guiControl.contextualButtonHover())
		{
			this.combatEncounter.placeCharacterAtMouseTile(this.getCurrentCharacter());
			return;
		}
		if (SkaldIO.getMouseUp(1))
		{
			this.dataControl.currentMap.getMouseTileDescriptionToolTip();
		}
	}

	// Token: 0x060009AF RID: 2479 RVA: 0x0002E65C File Offset: 0x0002C85C
	private void updatePlacementButtons()
	{
		if (SkaldIO.getPressedLeftKey())
		{
			this.combatEncounter.placeCharacterAtAdjacentTile(this.getCurrentCharacter(), -1, 0);
			return;
		}
		if (SkaldIO.getPressedRightKey())
		{
			this.combatEncounter.placeCharacterAtAdjacentTile(this.getCurrentCharacter(), 1, 0);
			return;
		}
		if (SkaldIO.getPressedUpKey())
		{
			this.combatEncounter.placeCharacterAtAdjacentTile(this.getCurrentCharacter(), 0, 1);
			return;
		}
		if (SkaldIO.getPressedDownKey())
		{
			this.combatEncounter.placeCharacterAtAdjacentTile(this.getCurrentCharacter(), 0, -1);
		}
	}

	// Token: 0x060009B0 RID: 2480 RVA: 0x0002E6D4 File Offset: 0x0002C8D4
	protected override void setGUIData()
	{
		this.guiControl.setPrimaryHeader("Deploy Party");
		string str = "Begin Combat!";
		this.guiControl.setContextualButton("Begin Combat!");
		this.guiControl.setSecondaryDescription("Deploy your party and press \"" + str + "\"");
		this.guiControl.setInitiativeCounter(this.combatEncounter.getUIInitiativeList());
		this.guiControl.setMap(this.dataControl.currentMap.getIllustratedOutputMap(this.getCurrentCharacter(), true, false, true));
		this.drawPortraits();
		this.setBackground();
		this.guiControl.revealAll();
	}

	// Token: 0x0400027A RID: 634
	private CountDownClock countDown;

	// Token: 0x0400027B RID: 635
	private CombatEncounter combatEncounter;
}
