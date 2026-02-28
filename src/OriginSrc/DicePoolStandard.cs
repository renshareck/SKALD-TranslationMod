using System;

// Token: 0x02000183 RID: 387
public class DicePoolStandard : DicePoolBase
{
	// Token: 0x06001468 RID: 5224 RVA: 0x0005B05E File Offset: 0x0005925E
	public DicePoolStandard(string modifierName, int modifier) : this(modifierName, 2, 0, modifier)
	{
	}

	// Token: 0x06001469 RID: 5225 RVA: 0x0005B06A File Offset: 0x0005926A
	public DicePoolStandard(string modifierName, int rerolls, int modifier) : this(modifierName, 2, rerolls, modifier)
	{
	}

	// Token: 0x0600146A RID: 5226 RVA: 0x0005B076 File Offset: 0x00059276
	public DicePoolStandard(string modifierName, int poolSize, int rerolls, int modifier) : base(poolSize, rerolls, modifier)
	{
		this.name = poolSize.ToString() + "D6";
		this.modifierName = modifierName;
		base.roll();
	}

	// Token: 0x0600146B RID: 5227 RVA: 0x0005B0A6 File Offset: 0x000592A6
	public override DicePoolBase.DiceBase createDice()
	{
		return new DicePoolStandard.DiceSixSided();
	}

	// Token: 0x020002BB RID: 699
	protected class DiceSixSided : DicePoolBase.DiceBase
	{
		// Token: 0x06001B50 RID: 6992 RVA: 0x00075D5B File Offset: 0x00073F5B
		public DiceSixSided()
		{
			base.roll();
		}
	}
}
