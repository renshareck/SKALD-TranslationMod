using System;
using System.Collections.Generic;
using SkaldEnums;

// Token: 0x020000A1 RID: 161
public class AbilitiesState : CharacterInfoStates
{
	// Token: 0x06000AA5 RID: 2725 RVA: 0x00033593 File Offset: 0x00031793
	public AbilitiesState(DataControl dataControl) : base(dataControl)
	{
		this.stateId = SkaldStates.Abilities;
		this.character = dataControl.getCurrentPC();
		this.setGUIData();
	}

	// Token: 0x06000AA6 RID: 2726 RVA: 0x000335B8 File Offset: 0x000317B8
	protected override void createGUI()
	{
		this.sheet = new UIAbilitySheet(this.character);
		string background = "SheetAbilitiesBackground";
		this.guiControl = new GUIControlAbilitySheet(this.sheet, background);
	}

	// Token: 0x06000AA7 RID: 2727 RVA: 0x000335F0 File Offset: 0x000317F0
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
		int numericInputIndex = this.numericInputIndex;
		this.numericInputIndex = -1;
		this.setGUIData();
	}

	// Token: 0x06000AA8 RID: 2728 RVA: 0x00033654 File Offset: 0x00031854
	protected override void setGUIData()
	{
		base.setGUIData();
		base.setButtons(new List<string>(), "Exit");
		this.sheet.update(this.character);
		this.guiControl.setSheetDescription(this.sheet.getCurrentObjectFullDescriptionAndHeader());
		this.guiControl.setSheetHeader(this.character.getName() + ": Abilities");
		this.guiControl.revealAll();
	}

	// Token: 0x040002D4 RID: 724
	private UIAbilitySheet sheet;

	// Token: 0x040002D5 RID: 725
	private Character character;
}
