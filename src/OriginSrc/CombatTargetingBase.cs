using System;
using SkaldEnums;
using UnityEngine;

// Token: 0x0200007D RID: 125
public abstract class CombatTargetingBase : CombatBaseState
{
	// Token: 0x060009D4 RID: 2516 RVA: 0x0002F680 File Offset: 0x0002D880
	public CombatTargetingBase(DataControl dataControl) : base(dataControl)
	{
		this.guiControl = new GUIControlCombatEffectTargeting();
		this.activateState();
		this.setBigHeader();
	}

	// Token: 0x060009D5 RID: 2517
	protected abstract void setBigHeader();

	// Token: 0x060009D6 RID: 2518 RVA: 0x0002F6B8 File Offset: 0x0002D8B8
	public override void update()
	{
		base.update();
		this.setGUIData();
		this.setMousePosition();
		if (SkaldIO.getPressedEscapeKey())
		{
			this.exit(SkaldStates.CombatPlanning);
		}
		if (this.buttonRowInputIndex != -1 && this.buttonRowInputIndex == 0)
		{
			this.exit(SkaldStates.CombatPlanning);
		}
		this.buttonRowInputIndex = -1;
	}

	// Token: 0x060009D7 RID: 2519 RVA: 0x0002F708 File Offset: 0x0002D908
	private void setMousePosition()
	{
		if (this.guiControl.hoveringOverCombatTabs())
		{
			return;
		}
		Vector2 mouseRelativeToMap = this.guiControl.getMouseRelativeToMap();
		MapTile tileFromMouse = this.combatEncounter.getTileFromMouse(mouseRelativeToMap);
		if (tileFromMouse == null)
		{
			return;
		}
		this.setAreaEffectSelection(tileFromMouse.getTileX(), tileFromMouse.getTileY());
		if (SkaldIO.getMouseUp(0) || SkaldIO.getPressedMainInteractKey())
		{
			if (tileFromMouse != null && tileFromMouse.getCharacter() != null)
			{
				this.combatEncounter.getCurrentCharacter().setTargetOpponent(tileFromMouse.getCharacter());
			}
			if (this.isCurrentAreaEffectLegal())
			{
				this.useAbility();
				return;
			}
			PopUpControl.addPopUpOK("No legal target is selected.");
		}
	}

	// Token: 0x060009D8 RID: 2520 RVA: 0x0002F79B File Offset: 0x0002D99B
	protected void exit(SkaldStates state)
	{
		this.setTargetState(state);
		this.combatEncounter.getCurrentCharacter().clearAreaEffectSelection();
	}

	// Token: 0x060009D9 RID: 2521 RVA: 0x0002F7B4 File Offset: 0x0002D9B4
	protected override void setCombatDescription()
	{
		string buttonRowHovertext = this.guiControl.getButtonRowHovertext();
		if (buttonRowHovertext == "")
		{
			buttonRowHovertext = this.descText;
		}
		this.guiControl.setSecondaryDescription(buttonRowHovertext);
	}

	// Token: 0x060009DA RID: 2522 RVA: 0x0002F7F0 File Offset: 0x0002D9F0
	protected override void setButtonData()
	{
	}

	// Token: 0x060009DB RID: 2523
	protected abstract void useAbility();

	// Token: 0x060009DC RID: 2524
	protected abstract void setAreaEffectSelection(int x, int y);

	// Token: 0x060009DD RID: 2525
	protected abstract bool isCurrentAreaEffectLegal();

	// Token: 0x04000298 RID: 664
	private static TextureTools.TextureData textureBack = TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/MenuBarBoxBack");

	// Token: 0x04000299 RID: 665
	protected string descText = "Select a target for your Ability!";

	// Token: 0x0400029A RID: 666
	protected string hoverText = "Execute Ability.";
}
