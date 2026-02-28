using System;
using System.Collections.Generic;
using System.Drawing;
using SkaldEnums;
using UnityEngine;

// Token: 0x02000091 RID: 145
public class OverlandState : OverlandBaseState
{
	// Token: 0x06000A56 RID: 2646 RVA: 0x00031731 File Offset: 0x0002F931
	public OverlandState(DataControl dataControl) : base(dataControl)
	{
		this.guiControl.setMainMenuButton();
	}

	// Token: 0x06000A57 RID: 2647 RVA: 0x00031770 File Offset: 0x0002F970
	public override StateBase activateState()
	{
		this.currentCharacter = this.dataControl.getCurrentPC();
		string text = this.dataControl.getMusicPath();
		if (text == "")
		{
			text = "campfire";
		}
		AudioControl.playMusic(text);
		this.setGUIData();
		return this;
	}

	// Token: 0x06000A58 RID: 2648 RVA: 0x000317BA File Offset: 0x0002F9BA
	private bool isStateReady()
	{
		return this.dataControl.currentMap.isMapReady() && this.currentCharacter.basePhysicMovementComplete() && this.clock.isTimerZero();
	}

	// Token: 0x06000A59 RID: 2649 RVA: 0x000317EC File Offset: 0x0002F9EC
	public override void update()
	{
		if (this.currentCharacter != this.dataControl.getCurrentPC())
		{
			this.currentCharacter = this.dataControl.getCurrentPC();
			this.clearTooltipsAndSelectorGrids();
		}
		base.update();
		this.clock.tick();
		this.setGUIData();
		if (this.currentCharacter.hasDynamicAnimation())
		{
			return;
		}
		this.updateQuickButtons();
		this.setMouseInput();
		if (this.dataControl.shouldILaunchCombat())
		{
			this.dataControl.launchCombat();
			return;
		}
		if (this.dataControl.currentMap.areOpponentsNearBy())
		{
			this.spottedOpponents = true;
		}
		else
		{
			this.spottedOpponents = false;
		}
		if (this.guiControl.contextualButtonWasPressed())
		{
			this.dataControl.verbTrigger();
		}
		if (this.isStateReady())
		{
			if (this.edgeExitbuffer != "" && this.dataControl.currentMap.isPartyOnEdgeTile())
			{
				this.dataControl.mountMapEdgePrompt(this.edgeExitbuffer);
				this.edgeExitbuffer = "";
				return;
			}
			if (SkaldIO.anyKeyDown())
			{
				if (SkaldIO.getDownUpKey())
				{
					this.move(0, 1);
				}
				else if (SkaldIO.getDownLeftKey())
				{
					this.move(-1, 0);
				}
				else if (SkaldIO.getDownDownKey())
				{
					this.move(0, -1);
				}
				else if (SkaldIO.getDownRightKey())
				{
					this.move(1, 0);
				}
			}
			else
			{
				this.resolveAutoMove();
			}
			this.UIInputIndex = -1;
		}
	}

	// Token: 0x06000A5A RID: 2650 RVA: 0x0003194C File Offset: 0x0002FB4C
	private void move(int x, int y)
	{
		this.clearTooltipsAndSelectorGrids();
		if (this.dataControl.getParty().navigationCourseHasNodes())
		{
			this.dataControl.getParty().clearNavigationCourse();
		}
		else
		{
			this.dataControl.moveOverland(x, y, true);
		}
		this.clock.reset();
	}

	// Token: 0x06000A5B RID: 2651 RVA: 0x000319A0 File Offset: 0x0002FBA0
	private void resolveAutoMove()
	{
		Party party = this.dataControl.getParty();
		if (party.navigationCourseHasNodes())
		{
			Point point = party.popNavigationNode();
			this.dataControl.moveOverland(point.X - party.getTileX(), point.Y - party.getTileY(), false);
			this.clock.reset();
			if (this.dataControl.currentMap.areOpponentsNearBy() && !this.spottedOpponents)
			{
				party.clearNavigationCourse();
			}
		}
	}

	// Token: 0x06000A5C RID: 2652 RVA: 0x00031A1C File Offset: 0x0002FC1C
	protected void updateQuickButtons()
	{
		this.updateSheetQuickButtons();
		this.updateOverlandActionButtons();
		this.updateSystemButton();
	}

	// Token: 0x06000A5D RID: 2653 RVA: 0x00031A30 File Offset: 0x0002FC30
	private void updateOverlandActionButtons()
	{
		if (this.buttonRowInputIndex == 0)
		{
			if (this.dataControl.currentMap.areOpponentsNearBy())
			{
				this.dataControl.launchCombat();
			}
			else
			{
				this.dataControl.addVocalBark("I don't see any enemies!");
				this.dataControl.setDescription("You can't enter combat without enemies within combat range.");
			}
			this.clearTooltipsAndSelectorGrids();
		}
		else if (this.buttonRowInputIndex == 1)
		{
			this.setSpellSelectorGrid();
		}
		else if (this.buttonRowInputIndex == 2 || SkaldIO.getPressedHideKey())
		{
			this.dataControl.hide();
			this.clearTooltipsAndSelectorGrids();
		}
		else if (this.buttonRowInputIndex == 3)
		{
			this.dataControl.wait();
			this.clearTooltipsAndSelectorGrids();
		}
		else if (this.buttonRowInputIndex == 4)
		{
			this.setTargetState(SkaldStates.Quests);
			this.clearTooltipsAndSelectorGrids();
		}
		else if (this.buttonRowInputIndex == 5)
		{
			this.setTargetState(SkaldStates.Inventory);
			this.clearTooltipsAndSelectorGrids();
		}
		else if (this.buttonRowInputIndex == 6)
		{
			if (this.dataControl.getParty().hasVehicle())
			{
				this.dataControl.interactWithVehicle();
			}
			else
			{
				this.dataControl.makeCampRough();
			}
			this.clearTooltipsAndSelectorGrids();
		}
		else if (SkaldIO.getPressedToggleKey())
		{
			this.dataControl.toggleLight();
			this.clearTooltipsAndSelectorGrids();
		}
		else if (SkaldIO.getAbilityButtonShiftLeftButtonPressed())
		{
			this.guiControl.setMouseToClosestAbilityButtonBelow();
		}
		else if (SkaldIO.getAbilityButtonShiftRightButtonPressed())
		{
			this.guiControl.setMouseToClosestAbilityButtonAbove();
		}
		else if (SkaldIO.getStateCarouselRightButtonPressed())
		{
			this.setTargetState(SkaldStates.Character);
		}
		this.buttonRowInputIndex = 0;
	}

	// Token: 0x06000A5E RID: 2654 RVA: 0x00031BB8 File Offset: 0x0002FDB8
	private void updateSystemButton()
	{
		if (SkaldIO.getPressedEscapeKey())
		{
			if (this.areAnyAbilitySelectorsActive())
			{
				this.clearTooltipsAndSelectorGrids();
				return;
			}
			this.setTargetState(SkaldStates.Menu);
			return;
		}
		else
		{
			if (this.guiControl.mainMenuButtonWasPressed())
			{
				this.setTargetState(SkaldStates.Menu);
				return;
			}
			if (SkaldIO.getPressedQuickSaveKey())
			{
				this.dataControl.gameSave();
				return;
			}
			if (SkaldIO.getPressedQuickLoadKey())
			{
				this.dataControl.gameLoad();
			}
			return;
		}
	}

	// Token: 0x06000A5F RID: 2655 RVA: 0x00031C1D File Offset: 0x0002FE1D
	private void setSpellSelectorGrid()
	{
		if (this.spellSelectorGrid == null)
		{
			this.spellSelectorGrid = new UICombatAbilitySelectorGrid(this.currentCharacter.getNonCombatActivatedSpellButtonDataList(), 108);
			return;
		}
		this.clearSpellSelectorGrid();
	}

	// Token: 0x06000A60 RID: 2656 RVA: 0x00031C46 File Offset: 0x0002FE46
	private void clearAllSelectorGrids()
	{
		this.clearSpellSelectorGrid();
	}

	// Token: 0x06000A61 RID: 2657 RVA: 0x00031C4E File Offset: 0x0002FE4E
	private void clearSpellSelectorGrid()
	{
		this.spellSelectorGrid = null;
	}

	// Token: 0x06000A62 RID: 2658 RVA: 0x00031C57 File Offset: 0x0002FE57
	private bool areAnyAbilitySelectorsActive()
	{
		return this.spellSelectorGrid != null;
	}

	// Token: 0x06000A63 RID: 2659 RVA: 0x00031C64 File Offset: 0x0002FE64
	private void updateSelectorGridInteractions()
	{
		UICombatAbilitySelectorGrid uicombatAbilitySelectorGrid = this.spellSelectorGrid;
		CharacterComponentContainer spellContainer = this.currentCharacter.getSpellContainer();
		SkaldStates targetState = SkaldStates.OverlandSpellTargeting;
		if (uicombatAbilitySelectorGrid == null || spellContainer == null)
		{
			this.guiControl.setAbilitySelectorGrid(null);
			return;
		}
		this.guiControl.setAbilitySelectorGrid(uicombatAbilitySelectorGrid);
		if (uicombatAbilitySelectorGrid.getHoverIndex() != -1)
		{
			spellContainer.setNonCombatActivatedComponentByIndex(uicombatAbilitySelectorGrid.getHoverIndex());
			if (spellContainer.getCurrentComponent() != null)
			{
				this.descBuffer = spellContainer.getCurrentComponent().getName();
				uicombatAbilitySelectorGrid.setTextBlock(spellContainer.getCurrentComponent().getName());
			}
		}
		if (uicombatAbilitySelectorGrid.getLeftClickIndex() != -1)
		{
			if (!ToolTipPrinter.hasToolTip())
			{
				spellContainer.setNonCombatActivatedComponentByIndex(uicombatAbilitySelectorGrid.getLeftClickIndex());
				if (spellContainer.getCurrentComponent() != null && spellContainer.getCurrentComponent() is AbilityActive)
				{
					SkaldActionResult skaldActionResult = (spellContainer.getCurrentComponent() as AbilityActive).canUserUseAbility(this.currentCharacter);
					if (skaldActionResult.wasSuccess())
					{
						this.setTargetState(targetState);
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
		else if (uicombatAbilitySelectorGrid.getRightClickIndex() != -1 && !ToolTipPrinter.hasToolTip())
		{
			spellContainer.setNonCombatActivatedComponentByIndex(uicombatAbilitySelectorGrid.getRightClickIndex());
			if (spellContainer.getCurrentComponent() != null)
			{
				ToolTipPrinter.setToolTipWithRules(spellContainer.getCurrentComponent().getFullDescriptionAndHeader());
			}
		}
	}

	// Token: 0x06000A64 RID: 2660 RVA: 0x00031D8A File Offset: 0x0002FF8A
	private void clearTooltipsAndSelectorGrids()
	{
		this.clearAllSelectorGrids();
		ToolTipPrinter.clearToolTip();
	}

	// Token: 0x06000A65 RID: 2661 RVA: 0x00031D98 File Offset: 0x0002FF98
	private void setMouseInput()
	{
		if (this.guiControl.hoveringOverCombatTabs())
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
		this.dataControl.currentMap.setMouseTile(mouseRelativeToMap);
		if (SkaldIO.getMousePressed(1) && !ToolTipPrinter.isMouseOverTooltip())
		{
			this.dataControl.currentMap.getMouseTileDescriptionToolTip();
			this.dataControl.getParty().clearNavigationCourse();
		}
		if (SkaldIO.getMousePressed(0) && !ToolTipPrinter.isMouseOverTooltip())
		{
			if (this.dataControl.getParty().navigationCourseHasNodes())
			{
				this.dataControl.getParty().clearNavigationCourse();
				this.edgeExitbuffer = "";
				return;
			}
			this.dataControl.currentMap.findPathToMouseTile();
			this.edgeExitbuffer = this.dataControl.currentMap.testMouseExitId(mouseRelativeToMap);
		}
	}

	// Token: 0x06000A66 RID: 2662 RVA: 0x00031E84 File Offset: 0x00030084
	private void setButtonData()
	{
		if (this.buttonListOption == null)
		{
			this.buttonListOption = new List<UIButtonControlBase.ButtonData>();
		}
		this.buttonListOption.Clear();
		if (this.dataControl.currentMap != null && this.dataControl.currentMap.areOpponentsNearBy())
		{
			this.buttonListOption.Add(new UIButtonControlBase.ButtonData(OverlandState.textureOverlandAttack, "Draw your weapons and go to combat."));
		}
		else
		{
			this.buttonListOption.Add(new UIButtonControlBase.ButtonData(OverlandState.textureOverlandAttackNegative, "There are no weapons nearby to attack."));
		}
		this.buttonListOption.Add(new UIButtonControlBase.ButtonData(OverlandState.textureOverlandSpell, "Cast a spell."));
		this.buttonListOption.Add(new UIButtonControlBase.ButtonData(OverlandState.textureOverlandHide, "Hide."));
		this.buttonListOption.Add(new UIButtonControlBase.ButtonData(OverlandState.textureOverlandPass, "Pass a turn."));
		this.buttonListOption.Add(new UIButtonControlBase.ButtonData(OverlandState.textureOverlandJournal, "View the Journal."));
		this.buttonListOption.Add(new UIButtonControlBase.ButtonData(OverlandState.textureOverlandInventory, "View Inventory."));
		if (this.dataControl.getParty().hasVehicle())
		{
			this.buttonListOption.Add(new UIButtonControlBase.ButtonData(OverlandState.textureOverlandShip, "Interact with ship!"));
		}
		else if (this.dataControl.currentMap != null && !this.dataControl.currentMap.canMakeCampOnThisMap())
		{
			this.buttonListOption.Add(new UIButtonControlBase.ButtonData(OverlandState.textureOverlandCampNegative, "You can't make camp on this map."));
		}
		else
		{
			this.buttonListOption.Add(new UIButtonControlBase.ButtonData(OverlandState.textureOverlandCamp, "Make Camp."));
		}
		this.guiControl.setCombatOrderButtonRow(this.buttonListOption);
	}

	// Token: 0x06000A67 RID: 2663 RVA: 0x0003201C File Offset: 0x0003021C
	protected override void setSecondaryDescription()
	{
		string text = this.descBuffer;
		this.descBuffer = "";
		if (text == "")
		{
			text = this.guiControl.getButtonRowHovertext();
		}
		if (text == "")
		{
			text = this.dataControl.getDescription();
		}
		this.guiControl.setSecondaryDescription(text);
	}

	// Token: 0x06000A68 RID: 2664 RVA: 0x0003207C File Offset: 0x0003027C
	protected override void setGUIData()
	{
		base.setGUIData();
		this.updateSelectorGridInteractions();
		this.setButtonData();
		this.drawPortraits();
		this.guiControl.setSceneDescription("");
		this.guiControl.setMainImage("");
		this.guiControl.revealAll();
	}

	// Token: 0x040002B5 RID: 693
	private bool spottedOpponents;

	// Token: 0x040002B6 RID: 694
	private CountDownClock clock = new CountDownClock(16 / MapIllustrator.SCROLL_SPEED, false);

	// Token: 0x040002B7 RID: 695
	private string descBuffer = "";

	// Token: 0x040002B8 RID: 696
	private UICombatAbilitySelectorGrid spellSelectorGrid;

	// Token: 0x040002B9 RID: 697
	private Character currentCharacter;

	// Token: 0x040002BA RID: 698
	private string edgeExitbuffer = "";

	// Token: 0x040002BB RID: 699
	private static TextureTools.TextureData textureOverlandAttack = TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/MenuBarBoxAttackOverland");

	// Token: 0x040002BC RID: 700
	private static TextureTools.TextureData textureOverlandAttackNegative = TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/MenuBarBoxAttackOverlandNegative");

	// Token: 0x040002BD RID: 701
	private static TextureTools.TextureData textureOverlandSpell = TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/MenuBarBoxSpellsOverland");

	// Token: 0x040002BE RID: 702
	private static TextureTools.TextureData textureOverlandHide = TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/MenuBarBoxHide");

	// Token: 0x040002BF RID: 703
	private static TextureTools.TextureData textureOverlandPass = TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/MenuBarBoxHold");

	// Token: 0x040002C0 RID: 704
	private static TextureTools.TextureData textureOverlandInventory = TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/MenuBarBoxItemsOverland");

	// Token: 0x040002C1 RID: 705
	private static TextureTools.TextureData textureOverlandJournal = TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/MenuBarBoxInfo");

	// Token: 0x040002C2 RID: 706
	private static TextureTools.TextureData textureOverlandCamp = TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/MenuBarBoxCamp");

	// Token: 0x040002C3 RID: 707
	private static TextureTools.TextureData textureOverlandCampNegative = TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/MenuBarBoxCampNegative");

	// Token: 0x040002C4 RID: 708
	private static TextureTools.TextureData textureOverlandShip = TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/OverlandShip");

	// Token: 0x040002C5 RID: 709
	private List<UIButtonControlBase.ButtonData> buttonListOption;
}
