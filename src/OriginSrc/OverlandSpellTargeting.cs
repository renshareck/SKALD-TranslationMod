using System;
using System.Collections.Generic;
using SkaldEnums;

// Token: 0x02000090 RID: 144
public class OverlandSpellTargeting : OverlandBaseState
{
	// Token: 0x06000A52 RID: 2642 RVA: 0x00031637 File Offset: 0x0002F837
	public OverlandSpellTargeting(DataControl dataControl) : base(dataControl)
	{
		this.currentCharacter = dataControl.getCurrentPC();
		base.disableCharacterSwap();
		this.setGUIData();
	}

	// Token: 0x06000A53 RID: 2643 RVA: 0x00031658 File Offset: 0x0002F858
	protected override void createGUI()
	{
		this.effectSelector = this.dataControl.getCurrentPC().getOutOfCombatSpellTargetSelector();
		this.guiControl = new GUIControlOverlandEffectTargeting(this.effectSelector);
	}

	// Token: 0x06000A54 RID: 2644 RVA: 0x00031684 File Offset: 0x0002F884
	public override void update()
	{
		if (SkaldIO.getPressedEscapeKey())
		{
			this.setTargetState(SkaldStates.Overland);
		}
		else if (this.guiControl.contextualButtonWasPressed())
		{
			List<Character> selectedCharacters = this.effectSelector.getSelectedCharacters();
			if (selectedCharacters.Count == 0)
			{
				PopUpControl.addPopUpOK("You must select at least 1 target to cast spell!");
			}
			else
			{
				this.currentCharacter.setOutOfCombatSpellTarget(selectedCharacters);
				this.currentCharacter.castSpell();
				this.setTargetState(SkaldStates.Overland);
			}
		}
		this.setGUIData();
	}

	// Token: 0x06000A55 RID: 2645 RVA: 0x000316F3 File Offset: 0x0002F8F3
	protected override void setGUIData()
	{
		this.guiControl.setContextualButton("Cast Spell");
		base.setGUIData();
		this.drawPortraits();
		this.guiControl.setSceneDescription("");
		this.guiControl.setMainImage("");
	}

	// Token: 0x040002B3 RID: 691
	private Character currentCharacter;

	// Token: 0x040002B4 RID: 692
	private UIPartyEffectSelector effectSelector;
}
