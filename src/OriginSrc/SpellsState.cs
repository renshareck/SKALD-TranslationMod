using System;
using System.Collections.Generic;
using SkaldEnums;

// Token: 0x020000AD RID: 173
public class SpellsState : CharacterInfoStates
{
	// Token: 0x06000AD0 RID: 2768 RVA: 0x000341B7 File Offset: 0x000323B7
	public SpellsState(DataControl dataControl) : base(dataControl)
	{
		this.stateId = SkaldStates.Spells;
		this.character = dataControl.getCurrentPC();
		this.setGUIData();
	}

	// Token: 0x06000AD1 RID: 2769 RVA: 0x000341DA File Offset: 0x000323DA
	protected override void createGUI()
	{
		this.sheet = new UISpellBookSheet(this.dataControl.getCurrentPC().getSpellContainer().getSortedSpellList());
		this.guiControl = new GUIControlSpellBookSheet(this.sheet);
	}

	// Token: 0x06000AD2 RID: 2770 RVA: 0x00034210 File Offset: 0x00032410
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

	// Token: 0x06000AD3 RID: 2771 RVA: 0x00034274 File Offset: 0x00032474
	protected override void setGUIData()
	{
		base.setGUIData();
		base.setButtons(new List<string>(), "Exit");
		this.sheet.update(this.character);
		this.guiControl.setSheetDescription(this.sheet.getCurrentObjectFullDescriptionAndHeader());
		this.guiControl.setSheetHeader(this.character.getName() + ": Spell Grimoire");
		this.guiControl.revealAll();
	}

	// Token: 0x040002DE RID: 734
	private UISpellBookSheet sheet;

	// Token: 0x040002DF RID: 735
	private Character character;
}
