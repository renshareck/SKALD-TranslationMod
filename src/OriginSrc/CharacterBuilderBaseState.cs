using System;
using System.Collections.Generic;
using SkaldEnums;

// Token: 0x02000070 RID: 112
public class CharacterBuilderBaseState : StateBase
{
	// Token: 0x0600096D RID: 2413 RVA: 0x0002D219 File Offset: 0x0002B419
	protected CharacterBuilderBaseState(DataControl dataControl) : base(dataControl)
	{
		this.createGUI();
		this.guiControl.setNumericButtonsAsABXY();
		AudioControl.playMusic("Merchant");
		this.setStateSelector();
		base.disableCharacterSwap();
		base.disableClickablePortraits();
	}

	// Token: 0x0600096E RID: 2414 RVA: 0x0002D24F File Offset: 0x0002B44F
	protected override void createGUI()
	{
		this.guiControl = new GUIControlSheet();
	}

	// Token: 0x0600096F RID: 2415 RVA: 0x0002D25C File Offset: 0x0002B45C
	protected Character getCharacter()
	{
		return this.getUseCase().getCharacter();
	}

	// Token: 0x06000970 RID: 2416 RVA: 0x0002D26C File Offset: 0x0002B46C
	protected override void setStateSelector()
	{
		base.setStateSelector();
		if (this.getUseCase().startWithDifficultySelector())
		{
			this.stateSelector.add(SkaldStates.DifficultySelector, "Difficulty");
		}
		this.stateSelector.add(SkaldStates.ClassEditor, "Class");
		this.stateSelector.add(SkaldStates.BackgroundEditor, "Background");
		this.stateSelector.add(SkaldStates.StatsEditor, "Attributes");
		this.stateSelector.add(SkaldStates.FeatsCharacterCreation, "Feats");
		this.stateSelector.add(SkaldStates.ApperanceEditor, "Appearance");
	}

	// Token: 0x06000971 RID: 2417 RVA: 0x0002D2F8 File Offset: 0x0002B4F8
	protected virtual void venturForth()
	{
		this.getUseCase().ventureForth(this.dataControl);
		this.setTargetState(SkaldStates.Overland);
		this.dataControl.clearCharacterCreatorUseCase();
	}

	// Token: 0x06000972 RID: 2418 RVA: 0x0002D320 File Offset: 0x0002B520
	public override void update()
	{
		base.update();
		if (this.popUpYesNo != null && this.popUpYesNo.resultWasPositive())
		{
			this.setTargetState(this.getUseCase().getExitState());
			this.getUseCase().abortCreationTrigger(this.dataControl);
			this.dataControl.clearCharacterCreatorUseCase();
			this.popUpYesNo = null;
			return;
		}
		if (SkaldIO.getOptionSelectionButtonDown())
		{
			this.guiControl.setMouseToClosestOptionBelow();
		}
		else if (SkaldIO.getOptionSelectionButtonUp())
		{
			this.guiControl.setMouseToClosestOptionAbove();
		}
		else if (SkaldIO.getOptionSelectionButtonRight())
		{
			this.guiControl.controllerScrollSidewaysRight();
		}
		else if (SkaldIO.getOptionSelectionButtonLeft())
		{
			this.guiControl.controllerScrollSidewaysLeft();
		}
		this.updateButtonPressIndex();
		if (this.list == null)
		{
			return;
		}
		if (this.guiControl.getListButtonPressIndex() != -1)
		{
			this.list.getObjectByPageIndex(this.guiControl.getListButtonPressIndex());
			this.setMainTextBuffer(this.list.getCurrentObjectFullDescriptionAndHeader());
		}
	}

	// Token: 0x06000973 RID: 2419 RVA: 0x0002D410 File Offset: 0x0002B610
	protected override void setGUIData()
	{
		this.guiControl.setSheetHeader("");
		this.guiControl.setSheetDescription("");
		this.setSecondaryDescription();
		this.setBackground();
		if (this.dataControl.currentMap != null)
		{
			this.guiControl.setPrimaryHeader(this.dataControl.currentMap.getName());
			if (!this.guiControl.hasMap())
			{
				this.guiControl.setMap(this.dataControl.currentMap.getIllustratedOutputMap());
			}
		}
		this.drawPortraits();
	}

	// Token: 0x06000974 RID: 2420 RVA: 0x0002D49F File Offset: 0x0002B69F
	protected virtual void updateButtonPressIndex()
	{
		if (this.numericInputIndex == 1)
		{
			this.exit();
		}
	}

	// Token: 0x06000975 RID: 2421 RVA: 0x0002D4B0 File Offset: 0x0002B6B0
	protected void addIntroPopUp(string message)
	{
		this.guiControl.setSecondaryDescription(message);
		if (!this.getUseCase().allowPopUp())
		{
			return;
		}
		PopUpControl.addPopUpOK(message);
	}

	// Token: 0x06000976 RID: 2422 RVA: 0x0002D4D2 File Offset: 0x0002B6D2
	protected void exit()
	{
		this.popUpYesNo = PopUpControl.addPopUpYesNo("Are you sure you want to abort? All progress will be lost.");
		this.numericInputIndex = -1;
	}

	// Token: 0x06000977 RID: 2423 RVA: 0x0002D4EB File Offset: 0x0002B6EB
	private CharacterBuilderBaseState.CharacterCreatorUseCase getUseCase()
	{
		return this.dataControl.getCharacterCreatorUseCase();
	}

	// Token: 0x06000978 RID: 2424 RVA: 0x0002D4F8 File Offset: 0x0002B6F8
	protected void setButtons(List<string> options)
	{
		List<string> list = new List<string>();
		foreach (string item in options)
		{
			list.Add(item);
		}
		list.Add("Abort");
		this.guiControl.setNumericButtons(list);
		if (this.list != null)
		{
			List<string> scrolledStringList = this.list.getScrolledStringList();
			this.guiControl.setListButtons(scrolledStringList);
		}
		this.guiControl.setSheetHeader(this.getSheetName());
		this.guiControl.setPrimaryHeader("Character Creation");
		this.guiControl.setSheetDescription(this.getMainTextBuffer());
		if (this.stateSelector != null)
		{
			this.guiControl.setTabRowButtons(this.stateSelector.getStringList(), this.stateSelector.getCurrentIndex(this.stateId));
		}
	}

	// Token: 0x0400026C RID: 620
	protected SkaldObjectList list;

	// Token: 0x0400026D RID: 621
	private PopUpYesNo popUpYesNo;

	// Token: 0x02000235 RID: 565
	public class CharacterCreatorUseCaseMain : CharacterBuilderBaseState.CharacterCreatorUseCase
	{
		// Token: 0x060018DD RID: 6365 RVA: 0x0006CEF7 File Offset: 0x0006B0F7
		public CharacterCreatorUseCaseMain(Character character) : base(character)
		{
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x0006CF00 File Offset: 0x0006B100
		public override void ventureForth(DataControl dataControl)
		{
			dataControl.setStartingData();
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x0006CF08 File Offset: 0x0006B108
		public override SkaldStates getExitState()
		{
			return SkaldStates.IntroMenu;
		}
	}

	// Token: 0x02000236 RID: 566
	public class CharacterCreatorUseCaseMercenary : CharacterBuilderBaseState.CharacterCreatorUseCase
	{
		// Token: 0x060018E0 RID: 6368 RVA: 0x0006CF0C File Offset: 0x0006B10C
		public CharacterCreatorUseCaseMercenary(Character character) : base(character)
		{
			character.setPC(true);
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x0006CF1D File Offset: 0x0006B11D
		public override SkaldStates getExitState()
		{
			return SkaldStates.Overland;
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x0006CF20 File Offset: 0x0006B120
		public override bool startWithDifficultySelector()
		{
			return false;
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x0006CF23 File Offset: 0x0006B123
		public override bool allowPopUp()
		{
			return false;
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x0006CF26 File Offset: 0x0006B126
		public override void abortCreationTrigger(DataControl dataControl)
		{
			base.getCharacter().setPC(false);
			dataControl.getInventory().addMoney(base.getCharacter().getMercenaryPrice());
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x0006CF4C File Offset: 0x0006B14C
		public override void ventureForth(DataControl dataControl)
		{
			dataControl.recruit(base.getCharacter(), true);
		}
	}

	// Token: 0x02000237 RID: 567
	public abstract class CharacterCreatorUseCase
	{
		// Token: 0x060018E6 RID: 6374 RVA: 0x0006CF5B File Offset: 0x0006B15B
		protected CharacterCreatorUseCase(Character character)
		{
			this.character = character;
			character.clearAllCharacterData();
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x0006CF70 File Offset: 0x0006B170
		public Character getCharacter()
		{
			return this.character;
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x0006CF78 File Offset: 0x0006B178
		public virtual bool allowPopUp()
		{
			return true;
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x0006CF7B File Offset: 0x0006B17B
		public virtual void ventureForth(DataControl dataControl)
		{
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x0006CF7D File Offset: 0x0006B17D
		public virtual void abortCreationTrigger(DataControl dataControl)
		{
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x0006CF7F File Offset: 0x0006B17F
		public virtual bool startWithDifficultySelector()
		{
			return true;
		}

		// Token: 0x060018EC RID: 6380
		public abstract SkaldStates getExitState();

		// Token: 0x040008AB RID: 2219
		private Character character;
	}
}
