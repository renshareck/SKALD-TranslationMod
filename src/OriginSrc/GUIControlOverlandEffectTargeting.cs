using System;

// Token: 0x0200012F RID: 303
public class GUIControlOverlandEffectTargeting : GUIControl
{
	// Token: 0x0600120C RID: 4620 RVA: 0x00050122 File Offset: 0x0004E322
	public GUIControlOverlandEffectTargeting(UIPartyEffectSelector effectSelector)
	{
		this.effectSelector = effectSelector;
	}

	// Token: 0x0600120D RID: 4621 RVA: 0x00050134 File Offset: 0x0004E334
	public override void setContextualButton(string input)
	{
		if (input == "")
		{
			this.contextualButton = null;
			return;
		}
		if (this.contextualButton == null)
		{
			this.contextualButton = new GUIControl.UIContextualButton();
			this.contextualButton.setY(108);
		}
		this.contextualButton.setContent(input);
	}
}
