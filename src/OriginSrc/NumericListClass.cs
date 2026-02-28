using System;
using System.Collections.Generic;

// Token: 0x02000117 RID: 279
public abstract class NumericListClass : GUIControl
{
	// Token: 0x060011DA RID: 4570 RVA: 0x0004FC63 File Offset: 0x0004DE63
	public override void setNumericButtons(List<string> options)
	{
		if (this.numericButtons == null)
		{
			this.numericButtons = new NumericButtonControl(50, 100, 300, 0);
		}
		this.numericButtons.setButtons(options);
	}
}
