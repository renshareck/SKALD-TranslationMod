using System;
using System.Collections.Generic;
using SkaldEnums;

// Token: 0x020000A5 RID: 165
public class CharacterState : CharacterInfoStates
{
	// Token: 0x06000AB1 RID: 2737 RVA: 0x00033953 File Offset: 0x00031B53
	public CharacterState(DataControl dataControl) : base(dataControl)
	{
		this.stateId = SkaldStates.Character;
		this.character = dataControl.getCurrentPC();
		this.setGUIData();
	}

	// Token: 0x06000AB2 RID: 2738 RVA: 0x00033978 File Offset: 0x00031B78
	protected override void createGUI()
	{
		this.sheet = new UICharacterSheet();
		string backgroundPath = "SheetCharacterBackground";
		this.guiControl = new GUIControlCharacterSheet(this.sheet, backgroundPath);
	}

	// Token: 0x06000AB3 RID: 2739 RVA: 0x000339A8 File Offset: 0x00031BA8
	public override void update()
	{
		if (this.testExit())
		{
			return;
		}
		base.update();
		if (this.dataControl.getCurrentPC() != this.character)
		{
			this.character = this.dataControl.getCurrentPC();
			this.sheet.clearCurrentObject();
		}
		this.UIInputIndex = this.guiControl.getUIButtonPressIndex();
		this.numericInputIndex = this.guiControl.getNumericButtonPressIndex();
		if (this.numericInputIndex != -1)
		{
			if (this.numericInputIndex == 1)
			{
				this.dataControl.moveMember();
			}
			this.numericInputIndex = -1;
		}
		this.setGUIData();
	}

	// Token: 0x06000AB4 RID: 2740 RVA: 0x00033A40 File Offset: 0x00031C40
	protected override void setGUIData()
	{
		base.setGUIData();
		base.setButtons(new List<string>
		{
			"Move Character"
		}, "Exit");
		this.sheet.setPortrait(this.character.getPortrait());
		this.sheet.updateEntry1(this.character.getInfoListOfDescriptors());
		this.sheet.updateEntry2(this.character.getListOfConditions());
		this.guiControl.setSheetDescription(this.sheet.getCurrentObjectFullDescriptionAndHeader());
		this.guiControl.setSheetHeader(this.dataControl.getCurrentPC().printNameLevelClass());
		this.guiControl.revealAll();
	}

	// Token: 0x040002D8 RID: 728
	private UICharacterSheet sheet;

	// Token: 0x040002D9 RID: 729
	private Character character;
}
