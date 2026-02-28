using System;
using System.Collections.Generic;

// Token: 0x02000130 RID: 304
public class GUIControlMenu : GUIControl
{
	// Token: 0x0600120E RID: 4622 RVA: 0x00050182 File Offset: 0x0004E382
	public GUIControlMenu()
	{
		this.snapToOptions = true;
		this.callToAction = new UICallToAction();
	}

	// Token: 0x0600120F RID: 4623 RVA: 0x0005019C File Offset: 0x0004E39C
	public override void setNumericButtons(List<string> options)
	{
		int width = 200;
		if (this.numericButtons == null)
		{
			this.numericButtons = new MenuButtonControl(60, 150, width, 100);
		}
		this.numericButtons.setButtons(options);
	}
}
