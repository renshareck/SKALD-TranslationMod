using System;

// Token: 0x02000064 RID: 100
[Serializable]
public class BaseDataObject
{
	// Token: 0x060008CE RID: 2254 RVA: 0x0002ABA1 File Offset: 0x00028DA1
	public bool testId(string testId)
	{
		return this.id.ToUpper() == testId.ToUpper();
	}

	// Token: 0x04000224 RID: 548
	public string systemId;

	// Token: 0x04000225 RID: 549
	public string id;
}
