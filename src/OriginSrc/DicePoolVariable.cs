using System;

// Token: 0x02000185 RID: 389
public class DicePoolVariable : DicePoolBase
{
	// Token: 0x0600146D RID: 5229 RVA: 0x0005B0BA File Offset: 0x000592BA
	public DicePoolVariable(string name, int min, int max) : this(name, min, max, 0)
	{
	}

	// Token: 0x0600146E RID: 5230 RVA: 0x0005B0C6 File Offset: 0x000592C6
	public DicePoolVariable(int min, int max) : this("", min, max, 0)
	{
	}

	// Token: 0x0600146F RID: 5231 RVA: 0x0005B0D6 File Offset: 0x000592D6
	public DicePoolVariable(string name, int min, int max, int rerolls) : base(1, rerolls, 0)
	{
		if (name != "")
		{
			this.name = name;
		}
		this.min = min;
		this.max = max;
		base.roll();
	}

	// Token: 0x06001470 RID: 5232 RVA: 0x0005B10A File Offset: 0x0005930A
	public override DicePoolBase.DiceBase createDice()
	{
		return new DicePoolVariable.DiceVariable(this.min, this.max);
	}

	// Token: 0x04000535 RID: 1333
	private int min;

	// Token: 0x04000536 RID: 1334
	private int max;

	// Token: 0x020002BC RID: 700
	protected class DiceVariable : DicePoolBase.DiceBase
	{
		// Token: 0x06001B51 RID: 6993 RVA: 0x00075D69 File Offset: 0x00073F69
		public DiceVariable(int min, int max)
		{
			this.min = min;
			this.max = max;
			base.roll();
		}
	}
}
