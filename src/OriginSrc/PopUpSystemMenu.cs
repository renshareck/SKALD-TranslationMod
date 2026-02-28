using System;
using System.Collections.Generic;

// Token: 0x0200005C RID: 92
public class PopUpSystemMenu : PopUpOK
{
	// Token: 0x06000896 RID: 2198 RVA: 0x00029FB1 File Offset: 0x000281B1
	public PopUpSystemMenu(string description) : base(description)
	{
	}

	// Token: 0x06000897 RID: 2199 RVA: 0x00029FBA File Offset: 0x000281BA
	protected override void setPopUpUI(string description, List<string> buttonList)
	{
		this.uiElements = new PopUpBase.PopUpUISystemMenu(description, buttonList);
	}
}
