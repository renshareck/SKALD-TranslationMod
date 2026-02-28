using System;

// Token: 0x02000053 RID: 83
public class PopUpSaveOverwrite : PopUpSaveBase
{
	// Token: 0x0600086D RID: 2157 RVA: 0x0002907C File Offset: 0x0002727C
	public PopUpSaveOverwrite(LoadSaveBaseMenuState callingState) : base("Are you sure you want to overwrite the following save:\n\n" + C64Color.CYAN_TAG + callingState.getList().getCurrentObjectTitle() + "</color>", callingState)
	{
	}

	// Token: 0x0600086E RID: 2158 RVA: 0x000290A4 File Offset: 0x000272A4
	protected override void handle(bool result)
	{
		if (result)
		{
			this.callingState.overwrite();
		}
		base.handle(true);
	}
}
