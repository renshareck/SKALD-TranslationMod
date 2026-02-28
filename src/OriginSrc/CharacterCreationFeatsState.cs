using System;
using System.Collections.Generic;
using SkaldEnums;

// Token: 0x0200006D RID: 109
public class CharacterCreationFeatsState : CharacterBuilderBaseState
{
	// Token: 0x0600095D RID: 2397 RVA: 0x0002C9A0 File Offset: 0x0002ABA0
	public CharacterCreationFeatsState(DataControl dataControl) : base(dataControl)
	{
		this.featTree = new UIFeatTree(base.getCharacter().getFeatContainer());
		this.guiControl = new GUIControlFeatBuy(this.featTree);
		this.guiControl.setNumericButtonsAsABXY();
		this.stateId = SkaldStates.FeatsCharacterCreation;
		base.getCharacter().updateFeatLegality();
		CharacterClassArchetype archetype = base.getCharacter().getClass().getArchetype();
		string text = "These are the Feats derived from your class. Distribute your Development Points in them to gain benefits and new Abilities.";
		if (archetype != null && (archetype.isId("CLA_Magos") || archetype.isId("CLA_Cleric")))
		{
			text += "\n\nTo gain spellcasting abilities, you must purchase ranks in the relevant (violet) Feat-tree. More spells can be learned by studying spell-tomes that you acquire as you adventure.";
		}
		PopUpControl.addPopUpOK(text);
		this.setMainTextBuffer(text);
		this.setGUIData();
	}

	// Token: 0x0600095E RID: 2398 RVA: 0x0002CA4C File Offset: 0x0002AC4C
	public override void update()
	{
		base.getCharacter().updateFeatLegality();
		this.featTree.update(base.getCharacter());
		if (this.terminate)
		{
			base.setNextState(1);
			return;
		}
		if (UIFeatTree.getCurrentFeat() != null)
		{
			this.setMainTextBuffer(UIFeatTree.getCurrentFeat().getFullDescriptionAndHeader());
		}
		base.update();
		if (this.numericInputIndex != -1 && this.numericInputIndex == 0)
		{
			if (this.readyToProceed())
			{
				base.getCharacter().finalizeFeatPurchases();
				if (NewSpellsToLearnControl.hasSpellsToLearn())
				{
					NewSpellsToLearnControl.finalize();
					this.terminate = true;
				}
				else
				{
					base.setNextState(1);
				}
			}
			else
			{
				PopUpControl.addPopUpOK("You need to distribute all your Development Points before proceeding!\n\n" + base.getCharacter().getDevelopmentPoints().ToString() + " Development Points remaining.");
			}
		}
		this.numericInputIndex = -1;
		this.setGUIData();
	}

	// Token: 0x0600095F RID: 2399 RVA: 0x0002CB18 File Offset: 0x0002AD18
	protected override void updateSheetQuickButtons()
	{
		if (base.getCharacter().getFeatContainer().hasPossibleRanks())
		{
			return;
		}
		base.updateSheetQuickButtons();
	}

	// Token: 0x06000960 RID: 2400 RVA: 0x0002CB33 File Offset: 0x0002AD33
	private bool readyToProceed()
	{
		return base.getCharacter().getDevelopmentPoints() == 0;
	}

	// Token: 0x06000961 RID: 2401 RVA: 0x0002CB44 File Offset: 0x0002AD44
	protected override void setGUIData()
	{
		base.setGUIData();
		if (this.readyToProceed())
		{
			base.setButtons(new List<string>
			{
				"Continue"
			});
		}
		else
		{
			base.setButtons(new List<string>
			{
				"..."
			});
		}
		this.guiControl.setSheetDescription(this.getMainTextBuffer());
		this.featTree.setPointsText(base.getCharacter().printDevelopmentPoints());
		string sheetHeader = base.getCharacter().getName() + ": " + base.getCharacter().getClassName() + " Feats";
		this.guiControl.setSheetHeader(sheetHeader);
		this.guiControl.setSheetDescription(this.getMainTextBuffer());
		this.guiControl.revealAll();
	}

	// Token: 0x0400025A RID: 602
	private UIFeatTree featTree;

	// Token: 0x0400025B RID: 603
	private bool terminate;
}
