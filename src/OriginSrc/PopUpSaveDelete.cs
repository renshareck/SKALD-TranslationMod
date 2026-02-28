using System;

// Token: 0x02000052 RID: 82
public class PopUpSaveDelete : PopUpSaveBase
{
	// Token: 0x0600086B RID: 2155 RVA: 0x0002903D File Offset: 0x0002723D
	public PopUpSaveDelete(LoadSaveBaseMenuState callingState) : base("Are you sure you want to delete the following save:\n\n" + C64Color.CYAN_TAG + callingState.getList().getCurrentObjectTitle() + " </ color > ", callingState)
	{
	}

	// Token: 0x0600086C RID: 2156 RVA: 0x00029065 File Offset: 0x00027265
	protected override void handle(bool result)
	{
		if (result)
		{
			this.callingState.delete();
		}
		base.handle(true);
	}
}
