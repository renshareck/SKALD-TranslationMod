using System;
using System.Collections.Generic;

// Token: 0x02000067 RID: 103
[Serializable]
public class SkaldFlagContainer
{
	// Token: 0x060008E7 RID: 2279 RVA: 0x0002B30C File Offset: 0x0002950C
	public SkaldFlagContainer()
	{
		MainControl.log("Initializing Flag Container");
		MainControl.log("Completed Flag Container");
	}

	// Token: 0x060008E8 RID: 2280 RVA: 0x0002B33E File Offset: 0x0002953E
	public bool testFlag(string flag)
	{
		return this.flags.Contains(flag);
	}

	// Token: 0x060008E9 RID: 2281 RVA: 0x0002B34C File Offset: 0x0002954C
	public string printFlagList()
	{
		return "\n\nFlags:\n" + TextTools.printList(this.flags) + "\n\nClosed Flags:\n" + TextTools.printList(this.closedFlags);
	}

	// Token: 0x060008EA RID: 2282 RVA: 0x0002B378 File Offset: 0x00029578
	public string setFlag(string flag)
	{
		if (this.flags.Contains(flag))
		{
			return "";
		}
		if (this.closedFlags.Contains(flag))
		{
			return "";
		}
		this.flags.Add(flag);
		return flag;
	}

	// Token: 0x060008EB RID: 2283 RVA: 0x0002B3AF File Offset: 0x000295AF
	public string clearFlag(string flag)
	{
		if (this.flags.Contains(flag))
		{
			this.flags.Remove(flag);
		}
		return flag;
	}

	// Token: 0x060008EC RID: 2284 RVA: 0x0002B3CD File Offset: 0x000295CD
	public string closeFlag(string flag)
	{
		if (this.flags.Contains(flag))
		{
			this.flags.Remove(flag);
		}
		if (!this.closedFlags.Contains(flag))
		{
			this.closedFlags.Add(flag);
		}
		return flag;
	}

	// Token: 0x060008ED RID: 2285 RVA: 0x0002B405 File Offset: 0x00029605
	public SkaldFlagContainer.FlagsSaveData getSaveData()
	{
		return new SkaldFlagContainer.FlagsSaveData(this.flags, this.closedFlags);
	}

	// Token: 0x060008EE RID: 2286 RVA: 0x0002B418 File Offset: 0x00029618
	public void applySaveData(SkaldFlagContainer.FlagsSaveData data)
	{
		this.flags = data.flags;
		this.closedFlags = data.closedFlags;
	}

	// Token: 0x0400023E RID: 574
	private List<string> flags = new List<string>();

	// Token: 0x0400023F RID: 575
	private List<string> closedFlags = new List<string>();

	// Token: 0x0200022E RID: 558
	[Serializable]
	public class FlagsSaveData
	{
		// Token: 0x0600189D RID: 6301 RVA: 0x0006C780 File Offset: 0x0006A980
		public FlagsSaveData(List<string> flags, List<string> closedFlags)
		{
			foreach (string item in flags)
			{
				this.flags.Add(item);
			}
			foreach (string item2 in closedFlags)
			{
				this.closedFlags.Add(item2);
			}
		}

		// Token: 0x0400088E RID: 2190
		public List<string> flags = new List<string>();

		// Token: 0x0400088F RID: 2191
		public List<string> closedFlags = new List<string>();
	}
}
