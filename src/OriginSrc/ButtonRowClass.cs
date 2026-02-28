using System;
using System.Collections.Generic;

// Token: 0x02000119 RID: 281
public abstract class ButtonRowClass : GUIControl
{
	// Token: 0x060011DF RID: 4575 RVA: 0x0004FCFD File Offset: 0x0004DEFD
	public override void setCombatOrderButtonRow(List<UIButtonControlBase.ButtonData> options)
	{
		if (this.combatButtonRow == null)
		{
			this.combatButtonRow = new UIImageCombatButtonControl(107, 200, 16, 9);
		}
		this.combatButtonRow.setButtons(options);
	}

	// Token: 0x060011E0 RID: 4576 RVA: 0x0004FD29 File Offset: 0x0004DF29
	public override void setActionCounter(Character character, AbilityUseable ability)
	{
		if (this.actionCounter == null)
		{
			this.actionCounter = new UIActionCounter(364, 14);
		}
		this.actionCounter.update(character, ability);
	}

	// Token: 0x060011E1 RID: 4577 RVA: 0x0004FD52 File Offset: 0x0004DF52
	public override void setInitiativeCounter(UIInitiativeList initiativeList)
	{
		this.initiativeList = initiativeList;
	}

	// Token: 0x060011E2 RID: 4578 RVA: 0x0004FD5B File Offset: 0x0004DF5B
	public override void setAbilitySelectorGrid(UIAbilitySelectorGrid grid)
	{
		this.abilitySelectorGrid = grid;
	}

	// Token: 0x060011E3 RID: 4579 RVA: 0x0004FD64 File Offset: 0x0004DF64
	public override void setAndUpdateWeaponSwapControl(Character character)
	{
		if (this.weaponSwapControl == null)
		{
			this.weaponSwapControl = new UIWeaponSwapControl(60, 19, 40, 16);
		}
		this.weaponSwapControl.setButtonsAndUpdate(character);
	}
}
