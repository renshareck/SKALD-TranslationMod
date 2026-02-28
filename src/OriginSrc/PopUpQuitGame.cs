using System;
using UnityEngine;

// Token: 0x0200004D RID: 77
public class PopUpQuitGame : PopUpYesNo
{
	// Token: 0x0600085F RID: 2143 RVA: 0x00028DDC File Offset: 0x00026FDC
	public PopUpQuitGame() : base("Do you really want to quit the game?\n\nUnsaved progress will be lost.")
	{
	}

	// Token: 0x06000860 RID: 2144 RVA: 0x00028DE9 File Offset: 0x00026FE9
	protected override void handle(bool result)
	{
		if (result)
		{
			Application.Quit();
		}
		base.handle(true);
	}
}
