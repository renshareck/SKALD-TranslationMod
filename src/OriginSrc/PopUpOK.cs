using System;
using System.Collections.Generic;

// Token: 0x0200004C RID: 76
public class PopUpOK : PopUpBase
{
	// Token: 0x0600085D RID: 2141 RVA: 0x00028D95 File Offset: 0x00026F95
	public PopUpOK(string description) : base(description, new List<string>
	{
		"Continue"
	})
	{
	}

	// Token: 0x0600085E RID: 2142 RVA: 0x00028DAE File Offset: 0x00026FAE
	public override void handle()
	{
		if (base.isHandled())
		{
			return;
		}
		base.handle();
		if (SkaldIO.getPressedMainInteractKey() || SkaldIO.getPressedNumericButton1() || base.getButtonPressIndex() == 0)
		{
			this.handle(true);
		}
	}
}
