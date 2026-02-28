using System;
using System.Collections.Generic;
using SkaldEnums;

// Token: 0x02000071 RID: 113
public class CharacterCreationClassState : CharacterBuilderBaseState
{
	// Token: 0x06000979 RID: 2425 RVA: 0x0002D5E4 File Offset: 0x0002B7E4
	public CharacterCreationClassState(DataControl dataControl) : base(dataControl)
	{
		this.stateId = SkaldStates.ClassEditor;
		this.list = GameData.getClassList();
		this.list.setFirstSelectableObjectAsCurrentObject();
		this.setMainTextBuffer(this.list.getCurrentObjectFullDescriptionAndHeader());
		this.setGUIData();
		base.addIntroPopUp("Next, select a CLASS for your character! Classes define a character's role in the party by adding certain exclusive abilities.");
	}

	// Token: 0x0600097A RID: 2426 RVA: 0x0002D638 File Offset: 0x0002B838
	protected override string getSheetName()
	{
		return "Select a Class";
	}

	// Token: 0x0600097B RID: 2427 RVA: 0x0002D640 File Offset: 0x0002B840
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
			CharacterClass characterClass = this.list.getCurrentObject() as CharacterClass;
			if (characterClass.isSelectable())
			{
				base.getCharacter().addCharacterClass(characterClass.getId());
				base.setNextState(1);
			}
			else
			{
				PopUpControl.addPopUpOK("This class cannot be selected.");
			}
		}
		this.numericInputIndex = -1;
		this.setGUIData();
	}

	// Token: 0x0600097C RID: 2428 RVA: 0x0002D6DC File Offset: 0x0002B8DC
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
