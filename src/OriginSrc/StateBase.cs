using System;
using System.Collections.Generic;
using SkaldEnums;

// Token: 0x020000B2 RID: 178
public class StateBase
{
	// Token: 0x06000AE1 RID: 2785 RVA: 0x000344B4 File Offset: 0x000326B4
	protected StateBase(DataControl dataControl)
	{
		this.dataControl = dataControl;
		ToolTipPrinter.clearToolTip();
		this.createGUI();
	}

	// Token: 0x06000AE2 RID: 2786 RVA: 0x00034519 File Offset: 0x00032719
	protected virtual void createGUI()
	{
		this.guiControl = new GUIControlOverland();
	}

	// Token: 0x06000AE3 RID: 2787 RVA: 0x00034526 File Offset: 0x00032726
	protected virtual void setStateSelector()
	{
		this.stateSelector = new StateBase.StateCarousel();
	}

	// Token: 0x06000AE4 RID: 2788 RVA: 0x00034533 File Offset: 0x00032733
	protected virtual string getSheetName()
	{
		return "";
	}

	// Token: 0x06000AE5 RID: 2789 RVA: 0x0003453A File Offset: 0x0003273A
	protected virtual string getMainTextBuffer()
	{
		return this.mainTextBuffer;
	}

	// Token: 0x06000AE6 RID: 2790 RVA: 0x00034542 File Offset: 0x00032742
	protected virtual void setMainTextBuffer(string text)
	{
		this.mainTextBuffer = text;
	}

	// Token: 0x06000AE7 RID: 2791 RVA: 0x0003454B File Offset: 0x0003274B
	public virtual bool allowAudio()
	{
		return true;
	}

	// Token: 0x06000AE8 RID: 2792 RVA: 0x0003454E File Offset: 0x0003274E
	public virtual void update()
	{
		this.numericInputIndex = this.guiControl.getNumericButtonPressIndex();
		this.buttonRowInputIndex = this.guiControl.getButtonRowPressIndex();
		this.UIInputIndex = this.guiControl.getUIButtonPressIndex();
		this.setTargetState(SkaldStates.Null);
	}

	// Token: 0x06000AE9 RID: 2793 RVA: 0x0003458A File Offset: 0x0003278A
	protected virtual void setSecondaryDescription()
	{
		this.guiControl.setSecondaryDescription(this.dataControl.getBuffer());
	}

	// Token: 0x06000AEA RID: 2794 RVA: 0x000345A4 File Offset: 0x000327A4
	protected virtual void setGUIData()
	{
		this.guiControl.setSheetHeader("");
		this.guiControl.setSheetDescription("");
		this.setSecondaryDescription();
		this.setBackground();
		if (this.dataControl.currentMap != null)
		{
			this.guiControl.setPrimaryHeader(this.dataControl.currentMap.getName());
			if (this.drawHighlights)
			{
				this.guiControl.setMap(this.dataControl.currentMap.getIllustratedOutputMap(null, false, true, false));
				return;
			}
			this.guiControl.setMap(this.dataControl.currentMap.getIllustratedOutputMap());
		}
	}

	// Token: 0x06000AEB RID: 2795 RVA: 0x00034648 File Offset: 0x00032848
	protected virtual void setBackground()
	{
		this.guiControl.setBackground(GlobalSettings.getDisplaySettings().getWindowFramePath());
	}

	// Token: 0x06000AEC RID: 2796 RVA: 0x0003465F File Offset: 0x0003285F
	public void handOverDataToNewState(StateBase newState)
	{
		if (newState == null)
		{
			return;
		}
		newState.recieveGUIHandoverData(this.guiControl);
	}

	// Token: 0x06000AED RID: 2797 RVA: 0x00034671 File Offset: 0x00032871
	public void recieveGUIHandoverData(GUIControl oldGui)
	{
		if (oldGui == null)
		{
			return;
		}
		this.guiControl.recieveOldGuiData(oldGui);
	}

	// Token: 0x06000AEE RID: 2798 RVA: 0x00034683 File Offset: 0x00032883
	public virtual StateBase activateState()
	{
		return this;
	}

	// Token: 0x06000AEF RID: 2799 RVA: 0x00034688 File Offset: 0x00032888
	protected virtual void updateSheetQuickButtons()
	{
		if (!this.allowQuickButtons)
		{
			return;
		}
		if (SkaldIO.getPressedSpellBookKey())
		{
			this.setStateFromQuickButton(SkaldStates.Spells);
		}
		else if (SkaldIO.getPressedFeatsKey())
		{
			this.setStateFromQuickButton(SkaldStates.Feats);
		}
		else if (SkaldIO.getPressedJournalKey())
		{
			this.setStateFromQuickButton(SkaldStates.Quests);
		}
		else if (SkaldIO.getPressedLevelUpKey())
		{
			this.setStateFromQuickButton(SkaldStates.Feats);
		}
		else if (SkaldIO.getPressedNextCharacterKey())
		{
			if (this.isCharacterSwapAllowed())
			{
				this.dataControl.changePC(1);
			}
		}
		else if (SkaldIO.getPressedCharacterSheetKey(!this.guiControl.isControllABXYPressActivated()))
		{
			this.setStateFromQuickButton(SkaldStates.Character);
		}
		else if (SkaldIO.getPressedInventoryKey(!this.guiControl.isControllABXYPressActivated()))
		{
			this.setStateFromQuickButton(SkaldStates.Inventory);
		}
		if (SkaldIO.getHighlightKeyDown() || SkaldIO.getMouseHeldDown(1))
		{
			this.drawHighlights = true;
			return;
		}
		this.drawHighlights = false;
	}

	// Token: 0x06000AF0 RID: 2800 RVA: 0x0003475C File Offset: 0x0003295C
	private void setStateFromQuickButton(SkaldStates state)
	{
		if (this.stateId == state)
		{
			this.clearAndGoToOverland();
			return;
		}
		this.setTargetState(state);
	}

	// Token: 0x06000AF1 RID: 2801 RVA: 0x00034775 File Offset: 0x00032975
	protected virtual bool isCharacterSwapAllowed()
	{
		return this.allowCharacterSwap && !this.dataControl.isCombatActive();
	}

	// Token: 0x06000AF2 RID: 2802 RVA: 0x0003478F File Offset: 0x0003298F
	protected void disableCharacterSwap()
	{
		this.allowCharacterSwap = false;
	}

	// Token: 0x06000AF3 RID: 2803 RVA: 0x00034798 File Offset: 0x00032998
	protected void disableClickablePortraits()
	{
		this.clickablePortraits = false;
	}

	// Token: 0x06000AF4 RID: 2804 RVA: 0x000347A4 File Offset: 0x000329A4
	public virtual void drawPortraits()
	{
		this.guiControl.setPortrait(PortraitTools.makePortraitList(this.dataControl.getParty()));
		if (this.guiControl.getPortraitPressIndexRight() != -1)
		{
			string inspectDescriptionFromIndex = this.dataControl.getParty().getInspectDescriptionFromIndex(this.guiControl.getPortraitPressIndexRight());
			if (inspectDescriptionFromIndex != "")
			{
				ToolTipPrinter.setToolTipWithRules(inspectDescriptionFromIndex);
			}
		}
		if (this.guiControl.getPortraitPressIndex() != -1 && this.isCharacterSwapAllowed() && !ToolTipPrinter.isMouseOverTooltip())
		{
			Character currentPC = this.dataControl.getCurrentPC();
			this.dataControl.getParty().setCurrentPC(this.guiControl.getPortraitPressIndex());
			if (this.dataControl.getCurrentPC() == currentPC && this.clickablePortraits && !PopUpControl.hasPopUp())
			{
				this.setTargetState(SkaldStates.Character);
				return;
			}
			this.dataControl.setDescription(this.dataControl.getCurrentPC().getFullNameUpper() + " is now leading the party!");
		}
	}

	// Token: 0x06000AF5 RID: 2805 RVA: 0x0003489C File Offset: 0x00032A9C
	public SkaldStates getTargetState()
	{
		return this.targetState;
	}

	// Token: 0x06000AF6 RID: 2806 RVA: 0x000348A4 File Offset: 0x00032AA4
	protected virtual void clearAndGoToOverland()
	{
		if (this.dataControl.isGameStarted())
		{
			this.setTargetState(SkaldStates.Overland);
		}
		else
		{
			this.setTargetState(SkaldStates.IntroMenu);
		}
		this.dataControl.clearContainer();
		this.dataControl.clearStore();
		this.dataControl.clearCamp();
	}

	// Token: 0x06000AF7 RID: 2807 RVA: 0x000348F0 File Offset: 0x00032AF0
	protected virtual void setTargetState(SkaldStates state)
	{
		if (state != SkaldStates.Null && state == this.stateId)
		{
			MainControl.logError("Attempting to set target state as state ID: " + state.ToString());
		}
		this.targetState = state;
	}

	// Token: 0x06000AF8 RID: 2808 RVA: 0x00034921 File Offset: 0x00032B21
	public void updateGUI()
	{
		this.guiControl.update();
	}

	// Token: 0x06000AF9 RID: 2809 RVA: 0x00034930 File Offset: 0x00032B30
	protected void setNextState(int index)
	{
		if (this.stateSelector == null)
		{
			return;
		}
		SkaldStates nextState = this.stateSelector.getNextState(index, this.stateId);
		this.setTargetState(nextState);
	}

	// Token: 0x040002E5 RID: 741
	private string mainTextBuffer = "";

	// Token: 0x040002E6 RID: 742
	protected string secondaryTextBuffer = "";

	// Token: 0x040002E7 RID: 743
	protected DataControl dataControl;

	// Token: 0x040002E8 RID: 744
	protected GUIControl guiControl;

	// Token: 0x040002E9 RID: 745
	protected int numericInputIndex = -1;

	// Token: 0x040002EA RID: 746
	protected int buttonRowInputIndex = -1;

	// Token: 0x040002EB RID: 747
	protected int UIInputIndex = -1;

	// Token: 0x040002EC RID: 748
	private bool allowCharacterSwap = true;

	// Token: 0x040002ED RID: 749
	private SkaldStates targetState;

	// Token: 0x040002EE RID: 750
	protected SkaldStates stateId;

	// Token: 0x040002EF RID: 751
	protected StateBase.StateCarousel stateSelector;

	// Token: 0x040002F0 RID: 752
	protected bool allowQuickButtons = true;

	// Token: 0x040002F1 RID: 753
	private bool clickablePortraits = true;

	// Token: 0x040002F2 RID: 754
	protected bool drawHighlights;

	// Token: 0x02000238 RID: 568
	protected class StateCarousel
	{
		// Token: 0x060018ED RID: 6381 RVA: 0x0006CF82 File Offset: 0x0006B182
		public StateCarousel()
		{
			this.dictionary = new Dictionary<SkaldStates, string>();
			this.states = new List<SkaldStates>();
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x0006CFA0 File Offset: 0x0006B1A0
		public void add(SkaldStates state, string name)
		{
			if (this.dictionary.ContainsKey(state))
			{
				return;
			}
			this.states.Add(state);
			this.dictionary.Add(state, name);
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x0006CFCC File Offset: 0x0006B1CC
		public SkaldStates getNextState(int index, SkaldStates currentState)
		{
			if (currentState == SkaldStates.Null)
			{
				return currentState;
			}
			int i = 0;
			while (i < this.states.Count)
			{
				if (currentState == this.states[i])
				{
					if (i + index >= this.states.Count)
					{
						return this.states[0];
					}
					if (i + index < 0)
					{
						return this.states[this.states.Count - 1];
					}
					return this.states[i + index];
				}
				else
				{
					i++;
				}
			}
			return SkaldStates.Null;
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x0006D050 File Offset: 0x0006B250
		public int getCurrentIndex(SkaldStates currentState)
		{
			for (int i = 0; i < this.states.Count; i++)
			{
				if (currentState == this.states[i])
				{
					return i;
				}
			}
			return 0;
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x0006D088 File Offset: 0x0006B288
		public List<string> getStringList()
		{
			List<string> list = new List<string>();
			foreach (SkaldStates entry in this.states)
			{
				list.Add(this.getName(entry));
			}
			return list;
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x0006D0E8 File Offset: 0x0006B2E8
		private string getName(SkaldStates entry)
		{
			if (this.dictionary.ContainsKey(entry))
			{
				return this.dictionary[entry];
			}
			return entry.ToString();
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x0006D112 File Offset: 0x0006B312
		public SkaldStates getStateFromIndex(int i)
		{
			if (i >= 0 && i < this.states.Count)
			{
				return this.states[i];
			}
			return SkaldStates.Null;
		}

		// Token: 0x040008AC RID: 2220
		private Dictionary<SkaldStates, string> dictionary;

		// Token: 0x040008AD RID: 2221
		private List<SkaldStates> states;
	}
}
