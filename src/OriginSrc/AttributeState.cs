using System;
using System.Collections.Generic;
using SkaldEnums;

// Token: 0x020000A2 RID: 162
public class AttributeState : CharacterInfoStates
{
	// Token: 0x06000AA9 RID: 2729 RVA: 0x000336C9 File Offset: 0x000318C9
	public AttributeState(DataControl dataControl) : base(dataControl)
	{
		this.stateId = SkaldStates.Attributes;
		this.character = dataControl.getCurrentPC();
		this.setGUIData();
	}

	// Token: 0x06000AAA RID: 2730 RVA: 0x000336EC File Offset: 0x000318EC
	protected override void createGUI()
	{
		this.sheet = new UIAttributeSheet();
		string backgroundPath = "SheetAttributeBackground";
		this.guiControl = new GUIControlCharacterSheet(this.sheet, backgroundPath);
	}

	// Token: 0x06000AAB RID: 2731 RVA: 0x0003371C File Offset: 0x0003191C
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
		this.setGUIData();
	}

	// Token: 0x06000AAC RID: 2732 RVA: 0x00033770 File Offset: 0x00031970
	protected override void setGUIData()
	{
		base.setGUIData();
		base.setButtons(new List<string>(), "Exit");
		this.sheet.updateEntry1(this.character.getListOfPrimaryAttributes());
		this.sheet.updateEntry2(this.character.getListOfSkills());
		this.sheet.updateEntry3(this.character.getListOfSecondaryAttributes());
		this.sheet.updateEntry4(this.character.getListOfCombatStats());
		this.sheet.updateEntry5(this.character.getListOfDefences());
		this.guiControl.setSheetDescription(this.sheet.getCurrentObjectFullDescriptionAndHeader());
		this.guiControl.setSheetHeader(this.character.getName() + ": Attributes");
		this.guiControl.revealAll();
	}

	// Token: 0x040002D6 RID: 726
	private UIAttributeSheet sheet;

	// Token: 0x040002D7 RID: 727
	private Character character;
}
