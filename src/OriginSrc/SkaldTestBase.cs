using System;
using System.Collections.Generic;

// Token: 0x02000188 RID: 392
public abstract class SkaldTestBase
{
	// Token: 0x06001485 RID: 5253 RVA: 0x0005B5EC File Offset: 0x000597EC
	protected SkaldTestBase(string returnString, bool result)
	{
		this.returnString = returnString;
		this.isSuccess = result;
	}

	// Token: 0x06001486 RID: 5254 RVA: 0x0005B644 File Offset: 0x00059844
	protected SkaldTestBase(int ability, int difficulty) : this(ability, "Attribute", difficulty, "Target")
	{
	}

	// Token: 0x06001487 RID: 5255 RVA: 0x0005B658 File Offset: 0x00059858
	protected SkaldTestBase(int ability, string attributeString, int difficulty, string targetString)
	{
		this.attributeString = attributeString;
		this.targetString = targetString;
		this.ability = ability;
		this.difficulty = difficulty;
	}

	// Token: 0x06001488 RID: 5256 RVA: 0x0005B6BF File Offset: 0x000598BF
	protected SkaldTestBase(int ability, string attributeString, int difficulty) : this(ability, attributeString, difficulty, "Difficulty")
	{
	}

	// Token: 0x06001489 RID: 5257
	protected abstract bool testResult();

	// Token: 0x0600148A RID: 5258 RVA: 0x0005B6CF File Offset: 0x000598CF
	public virtual string getReturnString()
	{
		return this.getPrefix() + ": " + this.returnString;
	}

	// Token: 0x0600148B RID: 5259 RVA: 0x0005B6E7 File Offset: 0x000598E7
	protected string getSuccessPrefix()
	{
		return this.successColorTag + "SUCCESS</color>";
	}

	// Token: 0x0600148C RID: 5260 RVA: 0x0005B6F9 File Offset: 0x000598F9
	protected string getPrefix()
	{
		if (this.isSuccess)
		{
			return this.getSuccessPrefix();
		}
		return this.getFailurePrefix();
	}

	// Token: 0x0600148D RID: 5261 RVA: 0x0005B710 File Offset: 0x00059910
	protected string getFailurePrefix()
	{
		return this.failureColorTag + "FAILURE</color>";
	}

	// Token: 0x0600148E RID: 5262 RVA: 0x0005B722 File Offset: 0x00059922
	public void appendToReturnString(string input)
	{
		this.returnString += input;
	}

	// Token: 0x0600148F RID: 5263 RVA: 0x0005B736 File Offset: 0x00059936
	public bool wasSuccess()
	{
		return this.isSuccess;
	}

	// Token: 0x06001490 RID: 5264 RVA: 0x0005B73E File Offset: 0x0005993E
	public int getDegreeOfResult()
	{
		return this.degreeOfResult;
	}

	// Token: 0x06001491 RID: 5265 RVA: 0x0005B746 File Offset: 0x00059946
	public virtual List<int> getRawAbilityDiceRollList()
	{
		return null;
	}

	// Token: 0x06001492 RID: 5266 RVA: 0x0005B749 File Offset: 0x00059949
	public static int getPercentileChance(int skill, int difficulty)
	{
		return 0;
	}

	// Token: 0x04000542 RID: 1346
	protected bool isSuccess;

	// Token: 0x04000543 RID: 1347
	protected string returnString = "No test performed.";

	// Token: 0x04000544 RID: 1348
	protected int ability;

	// Token: 0x04000545 RID: 1349
	protected int difficulty;

	// Token: 0x04000546 RID: 1350
	protected int degreeOfResult;

	// Token: 0x04000547 RID: 1351
	protected string successColorTag = C64Color.GREEN_LIGHT_TAG;

	// Token: 0x04000548 RID: 1352
	protected string failureColorTag = C64Color.RED_LIGHT_TAG;

	// Token: 0x04000549 RID: 1353
	public string header = "";

	// Token: 0x0400054A RID: 1354
	public string tail = "";

	// Token: 0x0400054B RID: 1355
	protected string attributeString;

	// Token: 0x0400054C RID: 1356
	protected string targetString;
}
