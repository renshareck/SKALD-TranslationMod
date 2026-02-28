using System;
using SkaldEnums;

// Token: 0x020000A4 RID: 164
public class CharacterListBaseState : ListSheetBaseState
{
	// Token: 0x06000AAF RID: 2735 RVA: 0x000338CB File Offset: 0x00031ACB
	protected CharacterListBaseState(DataControl dataControl) : base(dataControl)
	{
	}

	// Token: 0x06000AB0 RID: 2736 RVA: 0x000338D4 File Offset: 0x00031AD4
	protected override void setStateSelector()
	{
		base.setStateSelector();
		this.stateSelector.add(SkaldStates.Character, "Character");
		this.stateSelector.add(SkaldStates.Attributes, "Attributes");
		this.stateSelector.add(SkaldStates.Inventory, "Inventory");
		this.stateSelector.add(SkaldStates.Feats, "Feats");
		this.stateSelector.add(SkaldStates.Spells, "Spells");
		this.stateSelector.add(SkaldStates.Journal, "Journal");
	}
}
