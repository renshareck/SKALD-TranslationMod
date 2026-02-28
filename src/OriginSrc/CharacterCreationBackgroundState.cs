using System;
using System.Collections.Generic;
using SkaldEnums;

// Token: 0x0200006F RID: 111
public class CharacterCreationBackgroundState : CharacterBuilderBaseState
{
	// Token: 0x06000969 RID: 2409 RVA: 0x0002D0B8 File Offset: 0x0002B2B8
	public CharacterCreationBackgroundState(DataControl dataControl) : base(dataControl)
	{
		this.stateId = SkaldStates.BackgroundEditor;
		this.list = GameData.getBackgroundList();
		this.list.setFirstSelectableObjectAsCurrentObject();
		this.setMainTextBuffer(this.list.getCurrentObjectFullDescriptionAndHeader());
		this.setGUIData();
		base.addIntroPopUp("Select a BACKGROUND for your character. BACKGROUNDS add points in skills, influence starting conditions and narrative content.");
	}

	// Token: 0x0600096A RID: 2410 RVA: 0x0002D10C File Offset: 0x0002B30C
	public override void update()
	{
		base.update();
		if (this.guiControl.getListButtonPressIndex() != -1)
		{
			this.list.getObjectByPageIndex(this.guiControl.getListButtonPressIndex());
			this.setMainTextBuffer(this.list.getCurrentObjectFullDescriptionAndHeader());
		}
		if (this.numericInputIndex == 0)
		{
			if ((this.list.getCurrentObject() as CharacterBackground).isSelectable())
			{
				base.getCharacter().addCharacterBackground(this.list.getCurrentObjectId());
				base.setNextState(1);
			}
			else
			{
				PopUpControl.addPopUpOK("This background cannot be selected.");
			}
		}
		this.numericInputIndex = -1;
		this.setGUIData();
	}

	// Token: 0x0600096B RID: 2411 RVA: 0x0002D1AA File Offset: 0x0002B3AA
	protected override string getSheetName()
	{
		return "Select a Background";
	}

	// Token: 0x0600096C RID: 2412 RVA: 0x0002D1B4 File Offset: 0x0002B3B4
	protected override void setGUIData()
	{
		base.setGUIData();
		base.setButtons(new List<string>
		{
			"Continue"
		});
		this.guiControl.setSheetDescription(this.getMainTextBuffer());
		if (this.list != null)
		{
			List<string> scrolledStringList = this.list.getScrolledStringList();
			this.guiControl.setListButtons(scrolledStringList);
		}
		this.guiControl.revealAll();
	}
}
