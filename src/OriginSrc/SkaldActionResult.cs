using System;

// Token: 0x02000187 RID: 391
public class SkaldActionResult
{
	// Token: 0x0600147C RID: 5244 RVA: 0x0005B4DC File Offset: 0x000596DC
	public SkaldActionResult(bool wasSuccess, bool postToCombatLog = true) : this(true, wasSuccess, "", postToCombatLog)
	{
	}

	// Token: 0x0600147D RID: 5245 RVA: 0x0005B4EC File Offset: 0x000596EC
	public SkaldActionResult(bool wasPerformed, bool wasSuccess, string resultString, bool postToCombatLog = true)
	{
		this.postInCombatLog = postToCombatLog;
		this.performed = wasPerformed;
		this.success = wasSuccess;
		this.resultString = resultString;
	}

	// Token: 0x0600147E RID: 5246 RVA: 0x0005B53C File Offset: 0x0005973C
	public SkaldActionResult(bool wasPerformed, bool wasSuccess, string resultString, string verboseResultString, bool postToCombatLog = true) : this(wasPerformed, wasSuccess, resultString, postToCombatLog)
	{
		this.verboseResultString = verboseResultString;
		if (MainControl.debugFunctions)
		{
			MainControl.log(string.Concat(new string[]
			{
				wasPerformed.ToString(),
				"\n",
				wasSuccess.ToString(),
				"\n",
				this.getShortAndVerboseResultString()
			}));
		}
	}

	// Token: 0x0600147F RID: 5247 RVA: 0x0005B5A0 File Offset: 0x000597A0
	public virtual string getResultString()
	{
		return this.resultString;
	}

	// Token: 0x06001480 RID: 5248 RVA: 0x0005B5A8 File Offset: 0x000597A8
	public bool shouldBePostedInCombatLog()
	{
		return this.postInCombatLog;
	}

	// Token: 0x06001481 RID: 5249 RVA: 0x0005B5B0 File Offset: 0x000597B0
	public virtual string getVerboseResultString()
	{
		return this.verboseResultString;
	}

	// Token: 0x06001482 RID: 5250 RVA: 0x0005B5B8 File Offset: 0x000597B8
	public virtual string getShortAndVerboseResultString()
	{
		return this.resultString + "\n\n" + this.verboseResultString;
	}

	// Token: 0x06001483 RID: 5251 RVA: 0x0005B5D0 File Offset: 0x000597D0
	public bool wasSuccess()
	{
		return this.wasPerformed() && this.success;
	}

	// Token: 0x06001484 RID: 5252 RVA: 0x0005B5E2 File Offset: 0x000597E2
	public bool wasPerformed()
	{
		return this.performed;
	}

	// Token: 0x0400053D RID: 1341
	protected bool performed;

	// Token: 0x0400053E RID: 1342
	protected bool success;

	// Token: 0x0400053F RID: 1343
	protected bool postInCombatLog = true;

	// Token: 0x04000540 RID: 1344
	protected string resultString = "";

	// Token: 0x04000541 RID: 1345
	protected string verboseResultString = "";
}
