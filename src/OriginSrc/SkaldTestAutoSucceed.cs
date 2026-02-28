using System;

// Token: 0x0200018F RID: 399
public class SkaldTestAutoSucceed : SkaldTestBase
{
	// Token: 0x060014AC RID: 5292 RVA: 0x0005BC46 File Offset: 0x00059E46
	public SkaldTestAutoSucceed(string resultString) : base(resultString, true)
	{
	}

	// Token: 0x060014AD RID: 5293 RVA: 0x0005BC50 File Offset: 0x00059E50
	protected override bool testResult()
	{
		return true;
	}
}
