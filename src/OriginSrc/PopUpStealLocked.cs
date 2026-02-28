using System;

// Token: 0x0200005A RID: 90
public class PopUpStealLocked : PopUpOK
{
	// Token: 0x0600088F RID: 2191 RVA: 0x00029E97 File Offset: 0x00028097
	public PopUpStealLocked() : base("This merchant no longer trusts you and refuses to trade. Try again later.")
	{
	}

	// Token: 0x06000890 RID: 2192 RVA: 0x00029EA4 File Offset: 0x000280A4
	public override void handle()
	{
		if (base.isHandled())
		{
			return;
		}
		MainControl.getDataControl().clearStore();
		base.handle();
	}
}
