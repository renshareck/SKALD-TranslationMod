using System;
using System.Collections.Generic;
using SkaldEnums;
using UnityEngine;

// Token: 0x02000079 RID: 121
public class CombatPlanningState : CombatBaseState
{
	// Token: 0x060009B1 RID: 2481 RVA: 0x0002E774 File Offset: 0x0002C974
	public CombatPlanningState(DataControl dataControl) : base(dataControl)
	{
		this.stateId = SkaldStates.CombatPlanning;
		this.guiControl.setMainMenuButton();
	}

	// Token: 0x060009B2 RID: 2482 RVA: 0x0002E7D0 File Offset: 0x0002C9D0
	public override void update()
	{
		if (this.testNewState())
		{
			return;
		}
		base.update();
		this.setGUIData();
		if (SkaldIO.getPressedEscapeKey())
		{
			if (this.areAnyAbilitySelectorsActive())
			{
				this.clearAllSelectorGrids();
			}
			else
			{
				this.setTargetState(SkaldStates.Menu);
			}
		}
		else if (this.guiControl.mainMenuButtonWasPressed())
		{
			this.setTargetState(SkaldStates.Menu);
		}
		if (this.dataControl.newSceneMounted)
		{
			this.guiControl = new GUIControlCombat();
			this.setGUIData();
			this.dataControl.newSceneMounted = false;
		}
		if (base.getCurrentCharacter() != null && base.getCurrentCharacter().isPassing())
		{
			this.combatEncounter.gotoNextState();
			this.buttonRowInputIndex = -1;
			this.clearTooltipsAndSelectorGrids();
			return;
		}
		this.testMoveAlongPath();
		base.testAutoResolve();
		this.updateSheetQuickButtons();
		if (SkaldIO.getPressedToggleKey())
		{
			base.getCurrentCharacter().toggleWeaponPreference();
		}
		if (this.guiControl.contextualButtonWasPressed())
		{
			if (SkaldIO.isControllerConnected())
			{
				this.attackAndGotoNextState();
			}
			else
			{
				this.testRepeatAction();
			}
		}
		else if (SkaldIO.getPressedEnterKey())
		{
			if (this.combatEncounter.isBattlefieldEmpty())
			{
				this.dataControl.clearCombat();
			}
		}
		else if (SkaldIO.getAbilityButtonShiftLeftButtonPressed())
		{
			this.guiControl.setMouseToClosestAbilityButtonBelow();
		}
		else if (SkaldIO.getAbilityButtonShiftRightButtonPressed())
		{
			this.guiControl.setMouseToClosestAbilityButtonAbove();
		}
		else if (SkaldIO.getPressedNextCharacterKey())
		{
			this.combatEncounter.cycleTargetForPlayerCharacter();
		}
		else if (SkaldIO.getPressedUpKey())
		{
			this.moveCharacter(0, 1);
		}
		else if (SkaldIO.getPressedLeftKey())
		{
			this.moveCharacter(-1, 0);
		}
		else if (SkaldIO.getPressedRightKey())
		{
			this.moveCharacter(1, 0);
		}
		else if (SkaldIO.getPressedDownKey())
		{
			this.moveCharacter(0, -1);
		}
		else if (SkaldIO.getPressedEscapeKey())
		{
			this.clearTooltipsAndSelectorGrids();
		}
		else if (this.buttonRowInputIndex == -1)
		{
			return;
		}
		if (this.buttonRowInputIndex == 0)
		{
			this.attackAndGotoNextState();
		}
		else if (this.buttonRowInputIndex == 1)
		{
			this.setSpellSelectorGrid();
		}
		else if (this.buttonRowInputIndex == 2)
		{
			this.setAbilitySelectorGrid();
		}
		else if (this.buttonRowInputIndex == 3)
		{
			this.setConsumableSelectorGrid();
		}
		else if (this.buttonRowInputIndex == 4)
		{
			if (!this.combatEncounter.isBattlefieldEmpty())
			{
				this.combatEncounter.defend();
			}
			this.clearTooltipsAndSelectorGrids();
		}
		else if (this.buttonRowInputIndex == 5)
		{
			this.setTargetState(SkaldStates.Inventory);
			this.clearTooltipsAndSelectorGrids();
		}
		else if (this.buttonRowInputIndex == 6)
		{
			this.combatEncounter.holdCurrentCharacterAction();
			this.clearTooltipsAndSelectorGrids();
		}
		else if (this.buttonRowInputIndex == 7)
		{
			this.setTargetState(SkaldStates.CombatLog);
			this.clearTooltipsAndSelectorGrids();
		}
		else if (this.buttonRowInputIndex == 8)
		{
			this.testRepeatAction();
			this.clearTooltipsAndSelectorGrids();
		}
		this.buttonRowInputIndex = -1;
	}

	// Token: 0x060009B3 RID: 2483 RVA: 0x0002EA71 File Offset: 0x0002CC71
	private void clearAllSelectorGrids()
	{
		this.clearAbilitySelectorGrid();
		this.clearSpellSelectorGrid();
		this.clearConsumableSelectorGrid();
	}

	// Token: 0x060009B4 RID: 2484 RVA: 0x0002EA85 File Offset: 0x0002CC85
	private void moveCharacter(int x, int y)
	{
		this.clearTooltipsAndSelectorGrids();
		this.combatEncounter.moveCharacter(x, y);
		this.combatEncounter.gotoNextState();
	}

	// Token: 0x060009B5 RID: 2485 RVA: 0x0002EAA5 File Offset: 0x0002CCA5
	private void clearTooltipsAndSelectorGrids()
	{
		this.clearAllSelectorGrids();
		ToolTipPrinter.clearToolTip();
	}

	// Token: 0x060009B6 RID: 2486 RVA: 0x0002EAB2 File Offset: 0x0002CCB2
	private void setSpellSelectorGrid()
	{
		if (this.spellSelectorGrid == null)
		{
			this.spellSelectorGrid = new UICombatAbilitySelectorGrid(base.getCurrentCharacter().getCombatActivatedSpellButtonDataList(), 86);
		}
		else
		{
			this.clearSpellSelectorGrid();
		}
		this.clearAbilitySelectorGrid();
		this.clearConsumableSelectorGrid();
	}

	// Token: 0x060009B7 RID: 2487 RVA: 0x0002EAE8 File Offset: 0x0002CCE8
	private void setAbilitySelectorGrid()
	{
		if (this.abilitySelectorGrid == null)
		{
			this.abilitySelectorGrid = new UICombatAbilitySelectorGrid(base.getCurrentCharacter().getCombatActivatedAbilityButtonDataList(), 105);
		}
		else
		{
			this.clearAbilitySelectorGrid();
		}
		this.clearSpellSelectorGrid();
		this.clearConsumableSelectorGrid();
	}

	// Token: 0x060009B8 RID: 2488 RVA: 0x0002EB1E File Offset: 0x0002CD1E
	private void setConsumableSelectorGrid()
	{
		if (this.consumableSelectorGrid == null)
		{
			this.consumableSelectorGrid = new UICombatAbilitySelectorGrid(base.getCurrentCharacter().getInventory().getConsumablesButtonDataList(), 124);
		}
		else
		{
			this.clearConsumableSelectorGrid();
		}
		this.clearSpellSelectorGrid();
		this.clearAbilitySelectorGrid();
	}

	// Token: 0x060009B9 RID: 2489 RVA: 0x0002EB59 File Offset: 0x0002CD59
	private void clearConsumableSelectorGrid()
	{
		this.consumableSelectorGrid = null;
	}

	// Token: 0x060009BA RID: 2490 RVA: 0x0002EB62 File Offset: 0x0002CD62
	private void clearAbilitySelectorGrid()
	{
		this.abilitySelectorGrid = null;
	}

	// Token: 0x060009BB RID: 2491 RVA: 0x0002EB6B File Offset: 0x0002CD6B
	private void clearSpellSelectorGrid()
	{
		this.spellSelectorGrid = null;
	}

	// Token: 0x060009BC RID: 2492 RVA: 0x0002EB74 File Offset: 0x0002CD74
	private bool areAnyAbilitySelectorsActive()
	{
		return this.abilitySelectorGrid != null || this.spellSelectorGrid != null || this.consumableSelectorGrid != null;
	}

	// Token: 0x060009BD RID: 2493 RVA: 0x0002EB91 File Offset: 0x0002CD91
	private void testRepeatAction()
	{
		if (base.getCurrentCharacter() == null)
		{
			return;
		}
		if (base.getCurrentCharacter().hasRepeatableActions())
		{
			base.getCurrentCharacter().repeatAction();
			this.combatEncounter.gotoNextState();
			return;
		}
		this.attackAndGotoNextState();
	}

	// Token: 0x060009BE RID: 2494 RVA: 0x0002EBC6 File Offset: 0x0002CDC6
	private void attackAndGotoNextState()
	{
		if (!this.combatEncounter.isBattlefieldEmpty())
		{
			this.combatEncounter.gotoNextState();
		}
		this.clearTooltipsAndSelectorGrids();
	}

	// Token: 0x060009BF RID: 2495 RVA: 0x0002EBE8 File Offset: 0x0002CDE8
	private void testMoveAlongPath()
	{
		if (this.combatEncounter.isCurrentCharacterPlayer() && base.getCurrentCharacter().moveAlongCombatPath)
		{
			if (this.moveDelay.isTimerZero() && base.getCurrentCharacter().physicMovementComplete())
			{
				this.combatEncounter.moveCurrentCharacterAlongPath();
				this.moveDelay.reset();
				return;
			}
			this.moveDelay.tick();
		}
	}

	// Token: 0x060009C0 RID: 2496 RVA: 0x0002EC4C File Offset: 0x0002CE4C
	protected override void setCombatDescription()
	{
		string text = "";
		if (this.descBuffer != "")
		{
			text = this.descBuffer;
			this.descBuffer = "";
		}
		if (text == "")
		{
			base.setCombatDescription();
			return;
		}
		this.guiControl.setSecondaryDescription(text);
	}

	// Token: 0x060009C1 RID: 2497 RVA: 0x0002ECA4 File Offset: 0x0002CEA4
	private void setMousePosition()
	{
		if (this.guiControl.hoveringOverCombatTabs() || this.combatEncounter == null || ToolTipPrinter.isMouseOverTooltip())
		{
			return;
		}
		if (this.areAnyAbilitySelectorsActive())
		{
			if (SkaldIO.getMouseUp(0) || SkaldIO.getMouseUp(1))
			{
				this.clearTooltipsAndSelectorGrids();
			}
			return;
		}
		Vector2 mouseRelativeToMap = this.guiControl.getMouseRelativeToMap();
		this.combatEncounter.updateMousePosition(mouseRelativeToMap);
		if (SkaldIO.getMousePressed(0))
		{
			this.mouseClickedFlag = true;
		}
		if (this.mouseClickedFlag && SkaldIO.getMouseUp(0))
		{
			this.mouseClickedFlag = false;
			this.combatEncounter.moveToMouse();
			return;
		}
		if (SkaldIO.getMouseUp(1))
		{
			this.dataControl.currentMap.getMouseTileDescriptionToolTip();
		}
	}

	// Token: 0x060009C2 RID: 2498 RVA: 0x0002ED50 File Offset: 0x0002CF50
	protected override void setButtonData()
	{
		this.buttonOptions.Clear();
		if (!this.combatEncounter.autoResolve && this.combatEncounter.isStatePlanningPlayer())
		{
			this.flashDelay.tick();
			if (this.flashDelay.isTimerZero())
			{
				this.flashDelay.reset();
			}
			this.buttonOptions.Add(CombatPlanningState.buttonAttack);
			if (!base.getCurrentCharacter().getSpellContainer().hasLegalCombatActivatedComponent())
			{
				this.buttonOptions.Add(CombatPlanningState.buttonNoSpell);
			}
			else if (this.flashDelay.getTimer() > 10 && this.flashDelay.getTimer() < 25)
			{
				this.buttonOptions.Add(CombatPlanningState.buttonSpellFlash);
			}
			else
			{
				this.buttonOptions.Add(CombatPlanningState.buttonSpell);
			}
			if (base.getCurrentCharacter().getCooldown() > 0)
			{
				if (base.getCurrentCharacter().getCooldown() >= 4)
				{
					this.buttonOptions.Add(CombatPlanningState.buttonNoManeuverCooldown4);
				}
				else if (base.getCurrentCharacter().getCooldown() == 3)
				{
					this.buttonOptions.Add(CombatPlanningState.buttonNoManeuverCooldown3);
				}
				else if (base.getCurrentCharacter().getCooldown() == 2)
				{
					this.buttonOptions.Add(CombatPlanningState.buttonNoManeuverCooldown2);
				}
				else if (base.getCurrentCharacter().getCooldown() == 1)
				{
					this.buttonOptions.Add(CombatPlanningState.buttonNoManeuverCooldown1);
				}
			}
			else if (!base.getCurrentCharacter().getAbilityManueverContainer().hasLegalCombatActivatedComponent())
			{
				this.buttonOptions.Add(CombatPlanningState.buttonNoManeuver);
			}
			else if (this.flashDelay.getTimer() > 5 && this.flashDelay.getTimer() < 20)
			{
				this.buttonOptions.Add(CombatPlanningState.buttonManeuverFlash);
			}
			else
			{
				this.buttonOptions.Add(CombatPlanningState.buttonManeuver);
			}
			if (base.getCurrentCharacter().getInventory().getConsumablesButtonDataList().Count == 0)
			{
				this.buttonOptions.Add(CombatPlanningState.buttonNoItem);
			}
			else if (this.flashDelay.getTimer() < 15)
			{
				this.buttonOptions.Add(CombatPlanningState.buttonItemFlash);
			}
			else
			{
				this.buttonOptions.Add(CombatPlanningState.buttonItem);
			}
			this.buttonOptions.Add(CombatPlanningState.buttonDefend);
			this.buttonOptions.Add(CombatPlanningState.buttonInventory);
			this.buttonOptions.Add(CombatPlanningState.buttonHold);
			this.buttonOptions.Add(CombatPlanningState.buttonLog);
			this.buttonOptions.Add(CombatPlanningState.buttonRepeat);
		}
		this.guiControl.setCombatOrderButtonRow(this.buttonOptions);
	}

	// Token: 0x060009C3 RID: 2499 RVA: 0x0002EFD8 File Offset: 0x0002D1D8
	private void updateAbilitySelectorGrid()
	{
		if (this.abilitySelectorGrid != null)
		{
			this.updateSelectorGridInteractions(this.abilitySelectorGrid, base.getCurrentCharacter().getAbilityManueverContainer(), SkaldStates.CombatAbilityTargeting);
			return;
		}
		if (this.spellSelectorGrid != null)
		{
			this.updateSelectorGridInteractions(this.spellSelectorGrid, base.getCurrentCharacter().getSpellContainer(), SkaldStates.CombatSpellTargeting);
			return;
		}
		if (this.consumableSelectorGrid != null)
		{
			this.guiControl.setAbilitySelectorGrid(this.consumableSelectorGrid);
			if (this.consumableSelectorGrid.getHoverIndex() != -1)
			{
				base.getCurrentCharacter().getInventory().setCurrentConsumableItemByIndex(this.consumableSelectorGrid.getHoverIndex());
				if (base.getCurrentCharacter().getInventory().getCurrentObject() != null)
				{
					this.descBuffer = base.getCurrentCharacter().getInventory().getCurrentObject().getName();
					this.consumableSelectorGrid.setTextBlock(base.getCurrentCharacter().getInventory().getCurrentObject().getName());
				}
			}
			if (this.consumableSelectorGrid.getLeftClickIndex() != -1)
			{
				if (!ToolTipPrinter.hasToolTip())
				{
					base.getCurrentCharacter().getInventory().setCurrentConsumableItemByIndex(this.consumableSelectorGrid.getHoverIndex());
					if (base.getCurrentCharacter().getInventory().getCurrentObject() != null)
					{
						base.getCurrentCharacter().getInventory().useCurrentItem(base.getCurrentCharacter());
						base.getCurrentCharacter().useItemInCombat();
					}
					this.clearAllSelectorGrids();
					return;
				}
			}
			else if (this.consumableSelectorGrid.getRightClickIndex() != -1 && !ToolTipPrinter.hasToolTip())
			{
				base.getCurrentCharacter().getInventory().setCurrentConsumableItemByIndex(this.consumableSelectorGrid.getHoverIndex());
				if (base.getCurrentCharacter().getInventory().getCurrentObject() != null)
				{
					ToolTipPrinter.setToolTipWithRules(base.getCurrentCharacter().getInventory().getCurrentObject().getFullDescriptionAndHeader());
					return;
				}
			}
		}
		else
		{
			this.guiControl.setAbilitySelectorGrid(null);
		}
	}

	// Token: 0x060009C4 RID: 2500 RVA: 0x0002F198 File Offset: 0x0002D398
	private void updateSelectorGridInteractions(UICombatAbilitySelectorGrid selectorGrid, CharacterComponentContainer container, SkaldStates exitState)
	{
		this.guiControl.setAbilitySelectorGrid(selectorGrid);
		if (selectorGrid.getHoverIndex() != -1)
		{
			container.setCombatActivatedComponentByIndex(selectorGrid.getHoverIndex());
			if (container.getCurrentComponent() != null)
			{
				this.descBuffer = container.getCurrentComponent().getName();
				selectorGrid.setTextBlock(container.getCurrentComponent().getName());
			}
		}
		if (selectorGrid.getLeftClickIndex() != -1)
		{
			if (!ToolTipPrinter.hasToolTip())
			{
				container.setCombatActivatedComponentByIndex(selectorGrid.getLeftClickIndex());
				if (container.getCurrentComponent() != null && container.getCurrentComponent() is AbilityActive)
				{
					SkaldActionResult skaldActionResult = (container.getCurrentComponent() as AbilityActive).canUserUseAbility(base.getCurrentCharacter());
					if (skaldActionResult.wasSuccess())
					{
						this.setTargetState(exitState);
					}
					else
					{
						PopUpControl.addPopUpOK(skaldActionResult.getResultString());
					}
				}
				this.clearAllSelectorGrids();
				return;
			}
		}
		else if (selectorGrid.getRightClickIndex() != -1 && !ToolTipPrinter.hasToolTip())
		{
			container.setCombatActivatedComponentByIndex(selectorGrid.getRightClickIndex());
			if (container.getCurrentComponent() != null)
			{
				ToolTipPrinter.setToolTipWithRules(container.getCurrentComponent().getFullDescriptionAndHeader());
			}
		}
	}

	// Token: 0x060009C5 RID: 2501 RVA: 0x0002F295 File Offset: 0x0002D495
	protected override void setGUIData()
	{
		this.setMousePosition();
		this.guiControl.setAndUpdateWeaponSwapControl(base.getCurrentCharacter());
		this.updateAbilitySelectorGrid();
		base.setGUIData();
	}

	// Token: 0x0400027C RID: 636
	private CountDownClock moveDelay = new CountDownClock(10, false);

	// Token: 0x0400027D RID: 637
	private CountDownClock flashDelay = new CountDownClock(45, true);

	// Token: 0x0400027E RID: 638
	private UICombatAbilitySelectorGrid abilitySelectorGrid;

	// Token: 0x0400027F RID: 639
	private UICombatAbilitySelectorGrid spellSelectorGrid;

	// Token: 0x04000280 RID: 640
	private UICombatAbilitySelectorGrid consumableSelectorGrid;

	// Token: 0x04000281 RID: 641
	private bool mouseClickedFlag;

	// Token: 0x04000282 RID: 642
	private string descBuffer = "";

	// Token: 0x04000283 RID: 643
	private static UIButtonControlBase.ButtonData buttonAttack = new UIButtonControlBase.ButtonData(TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/MenuBarBoxAttack"), "Perform attack.");

	// Token: 0x04000284 RID: 644
	private static UIButtonControlBase.ButtonData buttonDefend = new UIButtonControlBase.ButtonData(TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/MenuBarBoxDefend"), "Pass turn and defend.");

	// Token: 0x04000285 RID: 645
	private static UIButtonControlBase.ButtonData buttonHold = new UIButtonControlBase.ButtonData(TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/MenuBarBoxHold"), "Hold action until end of turn.");

	// Token: 0x04000286 RID: 646
	private static UIButtonControlBase.ButtonData buttonInventory = new UIButtonControlBase.ButtonData(TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/MenuBarBoxItemsOverland"), "Open the current character's Inventory.");

	// Token: 0x04000287 RID: 647
	private static UIButtonControlBase.ButtonData buttonLog = new UIButtonControlBase.ButtonData(TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/MenuBarBoxInfo"), "View combat log.");

	// Token: 0x04000288 RID: 648
	private static UIButtonControlBase.ButtonData buttonRepeat = new UIButtonControlBase.ButtonData(TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/MenuBarBoxRepeat"), "Repeat last action.");

	// Token: 0x04000289 RID: 649
	private static UIButtonControlBase.ButtonData buttonSpell = new UIButtonControlBase.ButtonData(TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/MenuBarBoxSpells"), "Select a Spell to cast.");

	// Token: 0x0400028A RID: 650
	private static UIButtonControlBase.ButtonData buttonSpellFlash = new UIButtonControlBase.ButtonData(TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/MenuBarBoxSpellsFlash"), "Select a Spell to cast.");

	// Token: 0x0400028B RID: 651
	private static UIButtonControlBase.ButtonData buttonNoSpell = new UIButtonControlBase.ButtonData(TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/MenuBarBoxSpellsNegative"), "No spells available.");

	// Token: 0x0400028C RID: 652
	private static UIButtonControlBase.ButtonData buttonManeuver = new UIButtonControlBase.ButtonData(TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/MenuBarBoxFeats"), "Select a Maneuver to perform.");

	// Token: 0x0400028D RID: 653
	private static UIButtonControlBase.ButtonData buttonManeuverFlash = new UIButtonControlBase.ButtonData(TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/MenuBarBoxFeatsFlash"), "Select a Maneuver to perform.");

	// Token: 0x0400028E RID: 654
	private static UIButtonControlBase.ButtonData buttonNoManeuver = new UIButtonControlBase.ButtonData(TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/MenuBarBoxFeatsNegative"), "No Maneuvers to perform.");

	// Token: 0x0400028F RID: 655
	private static UIButtonControlBase.ButtonData buttonNoManeuverCooldown1 = new UIButtonControlBase.ButtonData(TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/MenuBarBoxFeatsCooldown1"), "Cooldown: 1 turn.");

	// Token: 0x04000290 RID: 656
	private static UIButtonControlBase.ButtonData buttonNoManeuverCooldown2 = new UIButtonControlBase.ButtonData(TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/MenuBarBoxFeatsCooldown2"), "Cooldown: 2 turns.");

	// Token: 0x04000291 RID: 657
	private static UIButtonControlBase.ButtonData buttonNoManeuverCooldown3 = new UIButtonControlBase.ButtonData(TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/MenuBarBoxFeatsCooldown3"), "Cooldown: 3 turns.");

	// Token: 0x04000292 RID: 658
	private static UIButtonControlBase.ButtonData buttonNoManeuverCooldown4 = new UIButtonControlBase.ButtonData(TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/MenuBarBoxFeatsCooldown4"), "Cooldown: 4 turns.");

	// Token: 0x04000293 RID: 659
	private static UIButtonControlBase.ButtonData buttonItem = new UIButtonControlBase.ButtonData(TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/MenuBarBoxItems"), "Select an Item to use.");

	// Token: 0x04000294 RID: 660
	private static UIButtonControlBase.ButtonData buttonItemFlash = new UIButtonControlBase.ButtonData(TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/MenuBarBoxItemsFlash"), "Select an Item to use.");

	// Token: 0x04000295 RID: 661
	private static UIButtonControlBase.ButtonData buttonNoItem = new UIButtonControlBase.ButtonData(TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/MenuBarBoxItemsNegative"), "No Items available.");

	// Token: 0x04000296 RID: 662
	private List<UIButtonControlBase.ButtonData> buttonOptions = new List<UIButtonControlBase.ButtonData>();
}
