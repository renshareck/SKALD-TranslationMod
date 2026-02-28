using System;
using System.Collections.Generic;
using SkaldEnums;

// Token: 0x020000A8 RID: 168
public class FeatBuyState : CharacterInfoStates
{
	// Token: 0x06000AB9 RID: 2745 RVA: 0x00033BF8 File Offset: 0x00031DF8
	public FeatBuyState(DataControl dataControl) : base(dataControl)
	{
		this.currentCharacter = dataControl.getCurrentPC();
		this.featTree = new UIFeatTree(this.currentCharacter.getFeatContainer());
		this.guiControl = new GUIControlFeatBuy(this.featTree);
		this.guiControl.setNumericButtonsAsABXY();
		this.stateId = SkaldStates.Feats;
		this.currentCharacter.updateFeatLegality();
		this.setMainTextBuffer("These are the Feats derived from your class. Place development points in them (gained as you level up) to gain benefits and new Abilities.");
		this.setGUIData();
		if (this.currentCharacter.canLevelUp())
		{
			PopUpControl.addPopUpLevelUp(this.currentCharacter);
		}
	}

	// Token: 0x06000ABA RID: 2746 RVA: 0x00033C88 File Offset: 0x00031E88
	public override void update()
	{
		if (this.testExit())
		{
			return;
		}
		if (this.terminate)
		{
			this.setTargetState(SkaldStates.Overland);
			return;
		}
		this.currentCharacter.updateFeatLegality();
		if (this.currentCharacter != this.dataControl.getCurrentPC())
		{
			this.reset();
			this.currentCharacter = this.dataControl.getCurrentPC();
			this.featTree.setData(this.currentCharacter.getFeatContainer());
			if (this.currentCharacter.canLevelUp())
			{
				PopUpControl.addPopUpLevelUp(this.currentCharacter);
			}
			this.setGUIData();
		}
		this.featTree.update(this.currentCharacter);
		if (UIFeatTree.getCurrentFeat() != null)
		{
			this.setMainTextBuffer(UIFeatTree.getCurrentFeat().getFullDescriptionAndHeader());
		}
		base.update();
		if (this.numericInputIndex != -1)
		{
			if (this.numericInputIndex == 0)
			{
				this.currentCharacter.finalizeFeatPurchases();
				if (NewSpellsToLearnControl.hasSpellsToLearn())
				{
					NewSpellsToLearnControl.finalize();
					this.terminate = true;
				}
				else
				{
					this.setTargetState(SkaldStates.Overland);
				}
			}
			else if (this.numericInputIndex == 1)
			{
				this.reset();
				this.clearAndGoToOverland();
			}
			else if (this.numericInputIndex == 2)
			{
				this.reset();
			}
		}
		else if (this.UIInputIndex != -1 && this.hasUnsavedChanges())
		{
			this.addExitPopup();
		}
		this.numericInputIndex = -1;
		this.setGUIData();
	}

	// Token: 0x06000ABB RID: 2747 RVA: 0x00033DCB File Offset: 0x00031FCB
	protected override bool isCharacterSwapAllowed()
	{
		if (this.hasUnsavedChanges())
		{
			this.addExitPopup();
			return false;
		}
		return base.isCharacterSwapAllowed();
	}

	// Token: 0x06000ABC RID: 2748 RVA: 0x00033DE4 File Offset: 0x00031FE4
	private void reset()
	{
		int i = this.currentCharacter.getFeatContainer().resetRanks();
		this.currentCharacter.addDevelopmentPoints(i);
	}

	// Token: 0x06000ABD RID: 2749 RVA: 0x00033E0E File Offset: 0x0003200E
	protected override void updateSheetQuickButtons()
	{
		if (this.hasUnsavedChanges())
		{
			return;
		}
		base.updateSheetQuickButtons();
	}

	// Token: 0x06000ABE RID: 2750 RVA: 0x00033E1F File Offset: 0x0003201F
	protected override void setTargetState(SkaldStates state)
	{
		if (this.hasUnsavedChanges())
		{
			return;
		}
		base.setTargetState(state);
	}

	// Token: 0x06000ABF RID: 2751 RVA: 0x00033E31 File Offset: 0x00032031
	private void addExitPopup()
	{
		PopUpControl.addPopUpOK(string.Concat(new string[]
		{
			"You cannot Exit this screen while you have unsaved Development Points placed in Feats.\n\nClick ",
			C64Color.CYAN_TAG,
			"SAVE AND EXIT</color> or ",
			C64Color.CYAN_TAG,
			"RESET</color>"
		}));
	}

	// Token: 0x06000AC0 RID: 2752 RVA: 0x00033E6B File Offset: 0x0003206B
	protected override bool testExit()
	{
		if (SkaldIO.getPressedEscapeKey())
		{
			if (this.hasUnsavedChanges())
			{
				this.reset();
			}
			this.clearAndGoToOverland();
			return true;
		}
		return false;
	}

	// Token: 0x06000AC1 RID: 2753 RVA: 0x00033E8B File Offset: 0x0003208B
	protected override void clearAndGoToOverland()
	{
		if (this.hasUnsavedChanges())
		{
			return;
		}
		base.clearAndGoToOverland();
	}

	// Token: 0x06000AC2 RID: 2754 RVA: 0x00033E9C File Offset: 0x0003209C
	private bool hasUnsavedChanges()
	{
		return this.currentCharacter.getFeatContainer().hasPossibleRanks();
	}

	// Token: 0x06000AC3 RID: 2755 RVA: 0x00033EB0 File Offset: 0x000320B0
	protected override void setGUIData()
	{
		base.setGUIData();
		if (this.hasUnsavedChanges())
		{
			base.setButtons(new List<string>
			{
				"Abort",
				"Reset"
			}, "Save and Exit");
		}
		else
		{
			base.setButtons(new List<string>
			{
				"Exit",
				"..."
			}, "...");
		}
		this.featTree.setPointsText(this.currentCharacter.printDevelopmentPoints());
		string sheetHeader = this.currentCharacter.getName() + ": " + this.currentCharacter.getClassName() + " Feats";
		this.guiControl.setSheetHeader(sheetHeader);
		this.guiControl.setSheetDescription(this.getMainTextBuffer());
		this.guiControl.revealAll();
	}

	// Token: 0x040002DA RID: 730
	private Character currentCharacter;

	// Token: 0x040002DB RID: 731
	private UIFeatTree featTree;

	// Token: 0x040002DC RID: 732
	private bool terminate;
}
