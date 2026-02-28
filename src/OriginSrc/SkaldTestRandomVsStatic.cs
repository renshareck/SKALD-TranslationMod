using System;

// Token: 0x0200018B RID: 395
public class SkaldTestRandomVsStatic : SkaldTestRandomBase
{
	// Token: 0x060014A0 RID: 5280 RVA: 0x0005B9BB File Offset: 0x00059BBB
	public SkaldTestRandomVsStatic(Character character, string attributeId, int difficulty, int rerolls = 1) : base(character.getCurrentAttributeValue(attributeId), GameData.getAttributeName(attributeId), difficulty, rerolls)
	{
		this.testResult();
	}

	// Token: 0x060014A1 RID: 5281 RVA: 0x0005B9DA File Offset: 0x00059BDA
	public SkaldTestRandomVsStatic(Character character, AttributesControl.CoreAttributes attribute, int difficulty, int rerolls = 1) : base(character.getCurrentAttributeValue(attribute), GameData.getAttributeName(attribute), difficulty, rerolls)
	{
		this.testResult();
	}

	// Token: 0x060014A2 RID: 5282 RVA: 0x0005B9F9 File Offset: 0x00059BF9
	protected override int getTargetNumber()
	{
		return this.difficulty;
	}

	// Token: 0x060014A3 RID: 5283 RVA: 0x0005BA01 File Offset: 0x00059C01
	protected override string getTargetNumberResultString()
	{
		return C64Color.ATTRIBUTE_VALUE_TAG + this.difficulty.ToString() + "</color>";
	}
}
