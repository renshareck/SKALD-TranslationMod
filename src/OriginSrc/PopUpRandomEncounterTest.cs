using System;
using System.Collections.Generic;

// Token: 0x0200004E RID: 78
public class PopUpRandomEncounterTest : PopUpSingleAttributeTestBase
{
	// Token: 0x06000861 RID: 2145 RVA: 0x00028DFA File Offset: 0x00026FFA
	public PopUpRandomEncounterTest(RandomEncounterState state, string description, string successString, string failureString, int difficulty, AttributesControl.CoreAttributes attribute) : base(description, new List<string>
	{
		"Continue"
	}, difficulty, attribute)
	{
		this.state = state;
		this.successString = successString;
		this.failureString = failureString;
	}

	// Token: 0x06000862 RID: 2146 RVA: 0x00028E2D File Offset: 0x0002702D
	public override void succeed()
	{
		this.state.setSuccess(this.successString);
		base.succeed();
	}

	// Token: 0x06000863 RID: 2147 RVA: 0x00028E46 File Offset: 0x00027046
	public override void fail()
	{
		this.state.setFailure(this.failureString);
		base.fail();
	}

	// Token: 0x040001CE RID: 462
	private RandomEncounterState state;

	// Token: 0x040001CF RID: 463
	private string successString;

	// Token: 0x040001D0 RID: 464
	private string failureString;
}
