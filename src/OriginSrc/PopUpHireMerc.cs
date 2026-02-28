using System;
using System.Collections.Generic;

// Token: 0x02000047 RID: 71
public class PopUpHireMerc : PopUpYesNo
{
	// Token: 0x0600084C RID: 2124 RVA: 0x00028670 File Offset: 0x00026870
	public PopUpHireMerc(Character mercenary) : base("", new List<string>
	{
		"Hire",
		"Cancel"
	})
	{
		base.setMainTextContent("Do you wish to hire this mercenary for " + mercenary.getMercenaryPrice().ToString() + " gold?\n\nA mercenary is a blank character that you get to create from scratch and add to your party.");
		this.mercenary = mercenary;
	}

	// Token: 0x0600084D RID: 2125 RVA: 0x000286D0 File Offset: 0x000268D0
	protected override void handle(bool result)
	{
		if (result && MainControl.getDataControl().getMoney() < this.mercenary.getMercenaryPrice())
		{
			result = false;
			PopUpControl.addPopUpOK("You can’t afford to hire this mercenary at the moment.");
		}
		if (result)
		{
			MainControl.getDataControl().getInventory().addMoney(-this.mercenary.getMercenaryPrice());
			MainControl.getDataControl().editCharacterAsMerc(this.mercenary);
		}
		base.handle(true);
	}

	// Token: 0x040001C2 RID: 450
	private Character mercenary;
}
