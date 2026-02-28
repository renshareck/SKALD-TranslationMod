using System;

// Token: 0x0200018E RID: 398
public class SkaldTestAutoFail : SkaldTestBase
{
	// Token: 0x060014AA RID: 5290 RVA: 0x0005BC39 File Offset: 0x00059E39
	public SkaldTestAutoFail(string resultString) : base(resultString, false)
	{
	}

	// Token: 0x060014AB RID: 5291 RVA: 0x0005BC43 File Offset: 0x00059E43
	protected override bool testResult()
	{
		return false;
	}
}
