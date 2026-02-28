using System;
using SkaldEnums;

// Token: 0x020000A3 RID: 163
public abstract class CharacterInfoStates : InfoBaseState
{
	// Token: 0x06000AAD RID: 2733 RVA: 0x00033842 File Offset: 0x00031A42
	protected CharacterInfoStates(DataControl dataControl) : base(dataControl)
	{
	}

	// Token: 0x06000AAE RID: 2734 RVA: 0x0003384C File Offset: 0x00031A4C
	protected override void setStateSelector()
	{
		base.setStateSelector();
		this.stateSelector.add(SkaldStates.Character, "Character");
		this.stateSelector.add(SkaldStates.Attributes, "Attributes");
		this.stateSelector.add(SkaldStates.Inventory, "Inventory");
		this.stateSelector.add(SkaldStates.Feats, "Feats");
		this.stateSelector.add(SkaldStates.Spells, "Spells");
		this.stateSelector.add(SkaldStates.Abilities, "Abilities");
	}
}
