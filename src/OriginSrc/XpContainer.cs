using System;

// Token: 0x02000022 RID: 34
[Serializable]
public class XpContainer
{
	// Token: 0x06000400 RID: 1024 RVA: 0x00012D14 File Offset: 0x00010F14
	public int addXp(int xp, Character target)
	{
		if (target.hasReachedLevelCap())
		{
			return 0;
		}
		this.currentXp += xp;
		this.totalXp += xp;
		while (this.currentXp >= this.nextLevel)
		{
			this.currentXp -= this.nextLevel;
			this.addLevel(1, target);
		}
		return xp;
	}

	// Token: 0x06000401 RID: 1025 RVA: 0x00012D74 File Offset: 0x00010F74
	public void addLevel(int levels, Character target)
	{
		for (int i = 0; i < levels; i++)
		{
			if (target.hasReachedLevelCap())
			{
				return;
			}
			this.nextLevel += XpContainer.LEVEL_INCREMENTOR;
			this.level++;
			this.levelUps++;
		}
	}

	// Token: 0x06000402 RID: 1026 RVA: 0x00012DC4 File Offset: 0x00010FC4
	public int getLevel()
	{
		return this.level;
	}

	// Token: 0x06000403 RID: 1027 RVA: 0x00012DCC File Offset: 0x00010FCC
	public int getLevelUps()
	{
		return this.levelUps;
	}

	// Token: 0x06000404 RID: 1028 RVA: 0x00012DD4 File Offset: 0x00010FD4
	public int getXp()
	{
		return this.currentXp;
	}

	// Token: 0x06000405 RID: 1029 RVA: 0x00012DDC File Offset: 0x00010FDC
	public int getNextLevel()
	{
		return this.nextLevel;
	}

	// Token: 0x06000406 RID: 1030 RVA: 0x00012DE4 File Offset: 0x00010FE4
	public int getXPToGo()
	{
		return this.getNextLevel() - this.getXp();
	}

	// Token: 0x06000407 RID: 1031 RVA: 0x00012DF3 File Offset: 0x00010FF3
	public int levelUp()
	{
		if (this.levelUps <= 0)
		{
			return 0;
		}
		int result = this.levelUps;
		this.levelUps = 0;
		return result;
	}

	// Token: 0x040000A2 RID: 162
	private int currentXp;

	// Token: 0x040000A3 RID: 163
	private int totalXp;

	// Token: 0x040000A4 RID: 164
	private int level = 1;

	// Token: 0x040000A5 RID: 165
	private int nextLevel = 1000;

	// Token: 0x040000A6 RID: 166
	private int levelUps;

	// Token: 0x040000A7 RID: 167
	private static int LEVEL_INCREMENTOR = 500;
}
