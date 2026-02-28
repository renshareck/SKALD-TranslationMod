using System;

// Token: 0x0200010F RID: 271
[Serializable]
public class SkaldTechnicalDataRecord
{
	// Token: 0x06001132 RID: 4402 RVA: 0x0004D690 File Offset: 0x0004B890
	public bool hasRested()
	{
		return this.hasRestedOnce;
	}

	// Token: 0x06001133 RID: 4403 RVA: 0x0004D698 File Offset: 0x0004B898
	public void setHasRested()
	{
		this.hasRestedOnce = true;
	}

	// Token: 0x06001134 RID: 4404 RVA: 0x0004D6A1 File Offset: 0x0004B8A1
	public bool hasCrafted()
	{
		return this.hasCraftedOnce;
	}

	// Token: 0x06001135 RID: 4405 RVA: 0x0004D6A9 File Offset: 0x0004B8A9
	public void setHasCrafted()
	{
		this.hasCraftedOnce = true;
	}

	// Token: 0x04000407 RID: 1031
	private bool hasRestedOnce;

	// Token: 0x04000408 RID: 1032
	private bool hasCraftedOnce;
}
