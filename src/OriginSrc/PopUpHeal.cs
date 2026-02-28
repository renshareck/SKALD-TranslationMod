using System;
using UnityEngine;

// Token: 0x02000046 RID: 70
public class PopUpHeal : PopUpYesNo
{
	// Token: 0x06000849 RID: 2121 RVA: 0x000285B8 File Offset: 0x000267B8
	public PopUpHeal(float priceMultiplier) : base("Do you wish to be healed for " + Mathf.RoundToInt(PopUpHeal.baseCost * priceMultiplier).ToString() + " Gold?\n\nHealing will restore health and cure negative conditions.")
	{
		this.cost = Mathf.RoundToInt(PopUpHeal.baseCost * priceMultiplier);
	}

	// Token: 0x0600084A RID: 2122 RVA: 0x00028600 File Offset: 0x00026800
	protected override void handle(bool result)
	{
		if (result)
		{
			if (MainControl.getDataControl().getMoney() < this.cost)
			{
				PopUpControl.addPopUpOK("You cannot afford healing!");
			}
			else
			{
				MainControl.getDataControl().clearNegativeConditionsAll();
				MainControl.getDataControl().healFullAll();
				MainControl.getDataControl().getInventory().addMoney(0 - this.cost);
			}
		}
		base.handle(true);
	}

	// Token: 0x040001C0 RID: 448
	private static float baseCost = 200f;

	// Token: 0x040001C1 RID: 449
	private int cost;
}
