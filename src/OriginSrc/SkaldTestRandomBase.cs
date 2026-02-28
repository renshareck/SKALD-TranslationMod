using System;
using System.Collections.Generic;

// Token: 0x02000189 RID: 393
public abstract class SkaldTestRandomBase : SkaldTestBase
{
	// Token: 0x06001493 RID: 5267 RVA: 0x0005B74C File Offset: 0x0005994C
	protected SkaldTestRandomBase(int ability, string attributeString, int difficulty, string targetString, int rerolls = 1) : base(ability, attributeString, difficulty, targetString)
	{
		this.dieRoll = new DicePoolStandard(attributeString, rerolls, ability);
	}

	// Token: 0x06001494 RID: 5268 RVA: 0x0005B768 File Offset: 0x00059968
	protected SkaldTestRandomBase(int ability, string attributeString, int difficulty, int rerolls = 1) : base(ability, attributeString, difficulty)
	{
		this.dieRoll = new DicePoolStandard(attributeString, rerolls, ability);
	}

	// Token: 0x06001495 RID: 5269 RVA: 0x0005B784 File Offset: 0x00059984
	protected override bool testResult()
	{
		this.isSuccess = (this.getActiveRoll() >= this.getTargetNumber());
		this.degreeOfResult = this.getActiveRoll() - this.getTargetNumber();
		if (this.isSuccess && this.degreeOfResult <= 0)
		{
			this.degreeOfResult = 1;
		}
		if (MainControl.debugFunctions)
		{
			MainControl.log(this.getReturnString());
		}
		return this.isSuccess;
	}

	// Token: 0x06001496 RID: 5270 RVA: 0x0005B7EB File Offset: 0x000599EB
	protected virtual int getActiveRoll()
	{
		return this.dieRoll.getResult();
	}

	// Token: 0x06001497 RID: 5271 RVA: 0x0005B7F8 File Offset: 0x000599F8
	public override List<int> getRawAbilityDiceRollList()
	{
		return this.dieRoll.getRawDiceRollList();
	}

	// Token: 0x06001498 RID: 5272 RVA: 0x0005B805 File Offset: 0x00059A05
	protected virtual string getActiveRollResultString()
	{
		return this.dieRoll.getResultString();
	}

	// Token: 0x06001499 RID: 5273 RVA: 0x0005B814 File Offset: 0x00059A14
	public override string getReturnString()
	{
		string text = "";
		if (this.header != "")
		{
			text = text + this.header + "\n\n";
		}
		if (this.isSuccess)
		{
			text = text + base.getSuccessPrefix() + ": ";
		}
		else
		{
			text = text + base.getFailurePrefix() + ": ";
		}
		if (this.attributeString != "")
		{
			text = text + this.attributeString + " ";
		}
		string text2 = "Difficulty";
		if (this.targetString != "")
		{
			text2 = this.targetString;
		}
		if (this.isSuccess)
		{
			text = string.Concat(new string[]
			{
				text,
				this.getActiveRollResultString(),
				" beats ",
				text2,
				" ",
				this.getTargetNumberResultString()
			});
		}
		else
		{
			text = string.Concat(new string[]
			{
				text,
				this.getActiveRollResultString(),
				" does not beat ",
				text2,
				" ",
				this.getTargetNumberResultString()
			});
		}
		if (this.tail != "")
		{
			text = text + "\n\n" + this.tail;
		}
		return text;
	}

	// Token: 0x0600149A RID: 5274
	protected abstract int getTargetNumber();

	// Token: 0x0600149B RID: 5275
	protected abstract string getTargetNumberResultString();

	// Token: 0x0400054D RID: 1357
	private DicePoolStandard dieRoll;
}
