using System;
using UnityEngine;

// Token: 0x0200004F RID: 79
public class PopUpRentRoom : PopUpYesNo
{
	// Token: 0x06000864 RID: 2148 RVA: 0x00028E60 File Offset: 0x00027060
	public PopUpRentRoom(float priceMultiplier) : base("Do you wish to rent a room and rest 8 hours for " + Mathf.RoundToInt(PopUpRentRoom.baseCost * priceMultiplier).ToString() + " Gold??\n\nResting will restore lost health.")
	{
		this.cost = Mathf.RoundToInt(PopUpRentRoom.baseCost * priceMultiplier);
	}

	// Token: 0x06000865 RID: 2149 RVA: 0x00028EA8 File Offset: 0x000270A8
	protected override void handle(bool result)
	{
		if (result)
		{
			if (MainControl.getDataControl().getMoney() < this.cost)
			{
				PopUpControl.addPopUpOK("You cannot afford a room here!");
			}
			else
			{
				MainControl.getDataControl().makeCampAtInn();
				MainControl.getDataControl().clearStore();
				MainControl.getDataControl().getInventory().addMoney(0 - this.cost);
			}
		}
		base.handle(true);
	}

	// Token: 0x040001D1 RID: 465
	private static float baseCost = 10f;

	// Token: 0x040001D2 RID: 466
	private int cost;
}
