using System;

// Token: 0x0200018A RID: 394
public class SkaldTestRandomVsRandom : SkaldTestRandomBase
{
	// Token: 0x0600149C RID: 5276 RVA: 0x0005B957 File Offset: 0x00059B57
	public SkaldTestRandomVsRandom(int ability, string attributeString, int difficulty, string targetString, int rerolls = 1) : base(ability, attributeString, difficulty, targetString, rerolls)
	{
		this.targetRoll = new DicePoolStandard(targetString, difficulty);
		this.testResult();
	}

	// Token: 0x0600149D RID: 5277 RVA: 0x0005B97B File Offset: 0x00059B7B
	public SkaldTestRandomVsRandom(Character character, AttributesControl.CoreAttributes attribute, Character targetCharacter, AttributesControl.CoreAttributes targetAttribute, int rerolls = 1) : this(character.getCurrentAttributeValue(attribute), GameData.getAttributeName(attribute), targetCharacter.getCurrentAttributeValue(targetAttribute), GameData.getAttributeName(targetAttribute), rerolls)
	{
	}

	// Token: 0x0600149E RID: 5278 RVA: 0x0005B9A1 File Offset: 0x00059BA1
	protected override int getTargetNumber()
	{
		return this.targetRoll.getResult();
	}

	// Token: 0x0600149F RID: 5279 RVA: 0x0005B9AE File Offset: 0x00059BAE
	protected override string getTargetNumberResultString()
	{
		return this.targetRoll.getResultString();
	}

	// Token: 0x0400054E RID: 1358
	private DicePoolStandard targetRoll;
}
