using System;

// Token: 0x0200018D RID: 397
public class SkaldTestGreaterThan : SkaldTestBase
{
	// Token: 0x060014A8 RID: 5288 RVA: 0x0005BAA2 File Offset: 0x00059CA2
	public SkaldTestGreaterThan(Character character, AttributesControl.CoreAttributes attribute, int difficulty) : base(character.getCurrentAttributeValueStatic(attribute), GameData.getAttributeName(attribute), difficulty)
	{
		this.testResult();
	}

	// Token: 0x060014A9 RID: 5289 RVA: 0x0005BAC0 File Offset: 0x00059CC0
	protected override bool testResult()
	{
		this.isSuccess = (this.ability >= this.difficulty);
		this.degreeOfResult = this.ability - this.difficulty;
		if (this.isSuccess && this.degreeOfResult <= 0)
		{
			this.degreeOfResult = 1;
		}
		string text = string.Concat(new string[]
		{
			this.attributeString,
			" (",
			C64Color.ATTRIBUTE_VALUE_TAG,
			(this.ability - 7).ToString(),
			"</color>) + Static Bonus (",
			C64Color.ATTRIBUTE_VALUE_TAG,
			"7</color>) = ",
			C64Color.ATTRIBUTE_VALUE_TAG,
			this.ability.ToString(),
			"</color>"
		});
		if (this.isSuccess)
		{
			this.returnString = string.Concat(new string[]
			{
				text,
				" beats ",
				this.targetString,
				" (",
				C64Color.ATTRIBUTE_VALUE_TAG,
				this.difficulty.ToString(),
				"</color>)"
			});
		}
		else
		{
			this.returnString = string.Concat(new string[]
			{
				text,
				" does not beat ",
				this.targetString,
				" (",
				C64Color.ATTRIBUTE_VALUE_TAG,
				this.difficulty.ToString(),
				"</color>)!"
			});
		}
		if (MainControl.debugFunctions)
		{
			MainControl.log(this.getReturnString());
		}
		return this.isSuccess;
	}
}
