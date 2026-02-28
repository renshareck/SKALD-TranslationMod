using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000186 RID: 390
public abstract class DicePoolBase
{
	// Token: 0x06001471 RID: 5233 RVA: 0x0005B11D File Offset: 0x0005931D
	protected DicePoolBase(int poolSize, int reRolls, int modifier) : this(modifier)
	{
		this.reRolls = reRolls;
		this.poolSize = poolSize;
	}

	// Token: 0x06001472 RID: 5234 RVA: 0x0005B134 File Offset: 0x00059334
	protected DicePoolBase(int modifier)
	{
		this.poolSize = 1;
		this.modifierName = "Modifier";
		this.name = "Roll";
		base..ctor();
		this.modifier = modifier;
	}

	// Token: 0x06001473 RID: 5235 RVA: 0x0005B160 File Offset: 0x00059360
	protected DicePoolBase()
	{
		this.poolSize = 1;
		this.modifierName = "Modifier";
		this.name = "Roll";
		base..ctor();
	}

	// Token: 0x06001474 RID: 5236
	public abstract DicePoolBase.DiceBase createDice();

	// Token: 0x06001475 RID: 5237 RVA: 0x0005B188 File Offset: 0x00059388
	public void roll()
	{
		List<DicePoolBase.DiceBase> list = new List<DicePoolBase.DiceBase>();
		this.resultDice = new List<DicePoolBase.DiceBase>();
		int num = Mathf.Abs(this.reRolls);
		for (int i = 0; i < this.poolSize + num; i++)
		{
			list.Add(this.createDice());
		}
		if (this.reRolls < 0)
		{
			for (int j = 0; j < num; j++)
			{
				this.removeHighestDiceFromList(list);
			}
		}
		else if (this.reRolls > 0)
		{
			for (int k = 0; k < num; k++)
			{
				this.removeLowestDiceFromList(list);
			}
		}
		foreach (DicePoolBase.DiceBase item in list)
		{
			this.resultDice.Add(item);
		}
		if (MainControl.debugFunctions)
		{
			MainControl.log(string.Concat(new string[]
			{
				C64Color.WHITE_TAG,
				this.name,
				" + ",
				this.modifierName,
				" (",
				this.modifier.ToString(),
				")</color>: ",
				this.getResultString()
			}));
		}
	}

	// Token: 0x06001476 RID: 5238 RVA: 0x0005B2B8 File Offset: 0x000594B8
	private void removeHighestDiceFromList(List<DicePoolBase.DiceBase> list)
	{
		DicePoolBase.DiceBase diceBase = null;
		foreach (DicePoolBase.DiceBase diceBase2 in list)
		{
			if (diceBase == null || diceBase2.getResult() > diceBase.getResult())
			{
				diceBase = diceBase2;
			}
		}
		list.Remove(diceBase);
	}

	// Token: 0x06001477 RID: 5239 RVA: 0x0005B31C File Offset: 0x0005951C
	private void removeLowestDiceFromList(List<DicePoolBase.DiceBase> list)
	{
		DicePoolBase.DiceBase diceBase = null;
		foreach (DicePoolBase.DiceBase diceBase2 in list)
		{
			if (diceBase == null || diceBase2.getResult() < diceBase.getResult())
			{
				diceBase = diceBase2;
			}
		}
		list.Remove(diceBase);
	}

	// Token: 0x06001478 RID: 5240 RVA: 0x0005B380 File Offset: 0x00059580
	public int getResult()
	{
		return this.getResultUnmodified() + this.modifier;
	}

	// Token: 0x06001479 RID: 5241 RVA: 0x0005B390 File Offset: 0x00059590
	private int getResultUnmodified()
	{
		if (this.resultDice == null)
		{
			return 0;
		}
		int num = 0;
		foreach (DicePoolBase.DiceBase diceBase in this.resultDice)
		{
			num += diceBase.getResult();
		}
		return num;
	}

	// Token: 0x0600147A RID: 5242 RVA: 0x0005B3F4 File Offset: 0x000595F4
	public string getResultString()
	{
		if (this.resultDice == null)
		{
			return "No roll was made!";
		}
		return string.Concat(new string[]
		{
			"(",
			this.modifier.ToString(),
			") + 2d6 (",
			this.getResultUnmodified().ToString(),
			") = ",
			C64Color.WHITE_TAG,
			this.getResult().ToString(),
			"</color>"
		});
	}

	// Token: 0x0600147B RID: 5243 RVA: 0x0005B474 File Offset: 0x00059674
	public List<int> getRawDiceRollList()
	{
		List<int> list = new List<int>();
		if (this.resultDice != null)
		{
			foreach (DicePoolBase.DiceBase diceBase in this.resultDice)
			{
				list.Add(diceBase.getResult());
			}
		}
		return list;
	}

	// Token: 0x04000537 RID: 1335
	private int reRolls;

	// Token: 0x04000538 RID: 1336
	private int poolSize;

	// Token: 0x04000539 RID: 1337
	private int modifier;

	// Token: 0x0400053A RID: 1338
	protected string modifierName;

	// Token: 0x0400053B RID: 1339
	protected string name;

	// Token: 0x0400053C RID: 1340
	private List<DicePoolBase.DiceBase> resultDice;

	// Token: 0x020002BD RID: 701
	public abstract class DiceBase
	{
		// Token: 0x06001B52 RID: 6994 RVA: 0x00075D85 File Offset: 0x00073F85
		public void roll()
		{
			this.result = SkaldRandom.range(this.min, this.max + 1);
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x00075DA0 File Offset: 0x00073FA0
		public int getResult()
		{
			return this.result;
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x00075DA8 File Offset: 0x00073FA8
		public string getResultString()
		{
			return this.result.ToString();
		}

		// Token: 0x04000A0F RID: 2575
		protected int min = 1;

		// Token: 0x04000A10 RID: 2576
		protected int max = 6;

		// Token: 0x04000A11 RID: 2577
		protected int result;
	}
}
