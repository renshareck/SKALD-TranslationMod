using System;
using System.Collections.Generic;

// Token: 0x0200006A RID: 106
public class SkaldNumericContainer
{
	// Token: 0x0600093C RID: 2364 RVA: 0x0002C0C4 File Offset: 0x0002A2C4
	public void transferToTargetContainer(SkaldNumericContainer targetContainer)
	{
		foreach (SkaldNumericContainer.Entry entry in this.entries)
		{
			targetContainer.addEntryButIgnoreZero(entry.getName(), entry.getValue());
		}
	}

	// Token: 0x0600093D RID: 2365 RVA: 0x0002C124 File Offset: 0x0002A324
	public void addEntryButIgnoreZero(Character c, AttributesControl.CoreAttributes attribute)
	{
		this.addEntryButIgnoreZero(GameData.getAttributeName(attribute), c.getCurrentAttributeValue(attribute));
	}

	// Token: 0x0600093E RID: 2366 RVA: 0x0002C139 File Offset: 0x0002A339
	public void addEntryButIgnoreZero(string name, int value)
	{
		if (value != 0)
		{
			this.entries.Add(new SkaldNumericContainer.Entry(name, value));
		}
	}

	// Token: 0x0600093F RID: 2367 RVA: 0x0002C150 File Offset: 0x0002A350
	public int getTotalValue()
	{
		int num = 0;
		foreach (SkaldNumericContainer.Entry entry in this.entries)
		{
			num += entry.getValue();
		}
		return num;
	}

	// Token: 0x06000940 RID: 2368 RVA: 0x0002C1AC File Offset: 0x0002A3AC
	public string printEntries()
	{
		string text = "";
		int num = 0;
		foreach (SkaldNumericContainer.Entry entry in this.entries)
		{
			text += entry.printNameValue();
			num += entry.getValue();
		}
		text += TextTools.formateNameValuePair("TOTAL", num);
		return text;
	}

	// Token: 0x04000251 RID: 593
	private List<SkaldNumericContainer.Entry> entries = new List<SkaldNumericContainer.Entry>();

	// Token: 0x02000230 RID: 560
	private struct Entry
	{
		// Token: 0x060018C9 RID: 6345 RVA: 0x0006CE5F File Offset: 0x0006B05F
		public Entry(string name, int value)
		{
			this.name = name;
			this.value = value;
			this.textLine = TextTools.formateNameValuePair(name, value) + "\n";
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x0006CE86 File Offset: 0x0006B086
		public string getName()
		{
			return this.name;
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x0006CE8E File Offset: 0x0006B08E
		public int getValue()
		{
			return this.value;
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x0006CE96 File Offset: 0x0006B096
		public string printNameValue()
		{
			return this.textLine;
		}

		// Token: 0x040008A8 RID: 2216
		private string name;

		// Token: 0x040008A9 RID: 2217
		private string textLine;

		// Token: 0x040008AA RID: 2218
		private int value;
	}
}
