using System;
using System.Collections.Generic;
using SkaldEnums;

// Token: 0x02000045 RID: 69
public class PopUpEncumberance : PopUpYesNo
{
	// Token: 0x06000847 RID: 2119 RVA: 0x0002857C File Offset: 0x0002677C
	public PopUpEncumberance() : base("You're Encumbered and cannot move! This means you're carrying more weight than your current Strength score allows.\n\nYou need to drop some items or find a way to raise your Strength.", new List<string>
	{
		"Inventory",
		"Cancel"
	})
	{
	}

	// Token: 0x06000848 RID: 2120 RVA: 0x000285A4 File Offset: 0x000267A4
	protected override void handle(bool result)
	{
		if (result)
		{
			MainControl.jumpToState(SkaldStates.Inventory);
		}
		base.handle(true);
	}
}
