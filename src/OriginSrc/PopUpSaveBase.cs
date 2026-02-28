using System;

// Token: 0x02000051 RID: 81
public abstract class PopUpSaveBase : PopUpYesNo
{
	// Token: 0x0600086A RID: 2154 RVA: 0x00029021 File Offset: 0x00027221
	protected PopUpSaveBase(string description, LoadSaveBaseMenuState callingState) : base(description)
	{
		this.callingState = callingState;
		this.list = callingState.getList();
	}

	// Token: 0x040001D7 RID: 471
	protected SkaldObjectList list;

	// Token: 0x040001D8 RID: 472
	protected LoadSaveBaseMenuState callingState;
}
