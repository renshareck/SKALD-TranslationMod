using System;
using System.Collections.Generic;

// Token: 0x02000140 RID: 320
public class UIPopUpButtonControl : UITextButtonControlHorizontalBase
{
	// Token: 0x0600127E RID: 4734 RVA: 0x00051AAF File Offset: 0x0004FCAF
	public UIPopUpButtonControl(int x, int y, int width, int height, int buttonNumber) : base(x, y, width, height, buttonNumber)
	{
	}

	// Token: 0x0600127F RID: 4735 RVA: 0x00051AC0 File Offset: 0x0004FCC0
	public override void setButtonText(List<string> stringList)
	{
		if (stringList.Count < base.getButtonsList().Count)
		{
			base.getNestedCanvas().clearElements();
		}
		while (base.getButtonsList().Count < stringList.Count)
		{
			UIButtonControlBase.UITextButton element = this.createButton();
			base.getNestedCanvas().add(element);
		}
		base.setButtonText(stringList);
	}
}
