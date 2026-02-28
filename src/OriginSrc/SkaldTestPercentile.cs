using System;

// Token: 0x0200018C RID: 396
public class SkaldTestPercentile : SkaldTestRandomBase
{
	// Token: 0x060014A4 RID: 5284 RVA: 0x0005BA1D File Offset: 0x00059C1D
	public SkaldTestPercentile(int target, string name, int rerolls = 1) : base(target, name, 0, rerolls)
	{
		this.targetRollPercentile = new DicePoolPercentile("D100 (" + name + ")");
		this.testResult();
	}

	// Token: 0x060014A5 RID: 5285 RVA: 0x0005BA4B File Offset: 0x00059C4B
	protected override int getTargetNumber()
	{
		return this.ability;
	}

	// Token: 0x060014A6 RID: 5286 RVA: 0x0005BA53 File Offset: 0x00059C53
	protected override bool testResult()
	{
		this.isSuccess = (this.getActiveRoll() > this.targetRollPercentile.getResult());
		if (MainControl.debugFunctions)
		{
			MainControl.log(this.getReturnString());
		}
		return this.isSuccess;
	}

	// Token: 0x060014A7 RID: 5287 RVA: 0x0005BA86 File Offset: 0x00059C86
	protected override string getTargetNumberResultString()
	{
		return C64Color.ATTRIBUTE_VALUE_TAG + this.ability.ToString() + "</color>";
	}

	// Token: 0x0400054F RID: 1359
	private DicePoolPercentile targetRollPercentile;
}
