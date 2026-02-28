using System;
using System.Collections.Generic;

// Token: 0x02000061 RID: 97
public class PopUpYesNo : PopUpBase
{
	// Token: 0x060008AE RID: 2222 RVA: 0x0002A33D File Offset: 0x0002853D
	public PopUpYesNo(string description) : base(description, new List<string>
	{
		"Yes",
		"No"
	})
	{
	}

	// Token: 0x060008AF RID: 2223 RVA: 0x0002A361 File Offset: 0x00028561
	public PopUpYesNo(string description, List<string> options) : base(description, options)
	{
	}

	// Token: 0x060008B0 RID: 2224 RVA: 0x0002A36C File Offset: 0x0002856C
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
			return;
		}
		if (SkaldIO.getPressedEscapeKey() || SkaldIO.getPressedNumericButton2() || base.getButtonPressIndex() == 1)
		{
			this.handle(false);
		}
	}
}
