using System;
using System.Collections.Generic;

// Token: 0x020000DC RID: 220
[Serializable]
public class Journal : SkaldObjectList
{
	// Token: 0x06000D3E RID: 3390 RVA: 0x0003C8CD File Offset: 0x0003AACD
	public Journal()
	{
		base.deactivateSorting();
	}

	// Token: 0x06000D3F RID: 3391 RVA: 0x0003C8E4 File Offset: 0x0003AAE4
	public void addEntry(string id)
	{
		if (base.containsObject(id))
		{
			return;
		}
		SKALDProjectData.JournalContainers.JournalEntry journalRawData = GameData.getJournalRawData(id);
		if (journalRawData != null)
		{
			this.add(new Journal.Entry(journalRawData, this.printCurrentChapterString()));
		}
	}

	// Token: 0x06000D40 RID: 3392 RVA: 0x0003C918 File Offset: 0x0003AB18
	public void addAllJournalEntries()
	{
		foreach (string id in GameData.getAllJournalRawDataId())
		{
			this.addEntry(id);
		}
	}

	// Token: 0x06000D41 RID: 3393 RVA: 0x0003C96C File Offset: 0x0003AB6C
	public SkaldDataList getJournalDataList()
	{
		SkaldDataList skaldDataList = new SkaldDataList(this.getName(), "");
		skaldDataList.purgeList();
		Dictionary<string, List<SkaldBaseObject>> dictionary = new Dictionary<string, List<SkaldBaseObject>>();
		List<SkaldBaseObject> list = new List<SkaldBaseObject>();
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			Journal.Entry item = (Journal.Entry)skaldBaseObject;
			list.Add(item);
		}
		list.Reverse();
		foreach (SkaldBaseObject skaldBaseObject2 in list)
		{
			Journal.Entry entry = (Journal.Entry)skaldBaseObject2;
			if (!dictionary.ContainsKey(entry.getChapter()))
			{
				dictionary.Add(entry.getChapter(), new List<SkaldBaseObject>());
			}
			dictionary[entry.getChapter()].Add(entry);
		}
		foreach (KeyValuePair<string, List<SkaldBaseObject>> keyValuePair in dictionary)
		{
			skaldDataList.addHeaderListAndSpace(keyValuePair.Key.ToUpper(), "", keyValuePair.Value);
		}
		skaldDataList.setFirstObjectWithADescription();
		return skaldDataList;
	}

	// Token: 0x06000D42 RID: 3394 RVA: 0x0003CAC0 File Offset: 0x0003ACC0
	public string printCurrentChapterString()
	{
		if (this.chapter <= 0)
		{
			return "Prologue";
		}
		return "Chapter " + this.chapter.ToString();
	}

	// Token: 0x06000D43 RID: 3395 RVA: 0x0003CAE6 File Offset: 0x0003ACE6
	public override string getName()
	{
		return "Journal: " + this.printCurrentChapterString();
	}

	// Token: 0x06000D44 RID: 3396 RVA: 0x0003CAF8 File Offset: 0x0003ACF8
	public void advanceChapter()
	{
		this.chapter++;
	}

	// Token: 0x04000328 RID: 808
	private int chapter = -1;

	// Token: 0x02000248 RID: 584
	[Serializable]
	private class Entry : SkaldBaseObject
	{
		// Token: 0x0600193B RID: 6459 RVA: 0x0006E78F File Offset: 0x0006C98F
		public Entry(SKALDProjectData.JournalContainers.JournalEntry entryData, string chapter) : base(entryData.id)
		{
			this.date = Calendar.printDayTime();
			this.chapter = chapter;
			HoverElementControl.addHoverText("Updated Journal:", this.getName());
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x0006E7CA File Offset: 0x0006C9CA
		private SKALDProjectData.JournalContainers.JournalEntry getRawData()
		{
			return GameData.getJournalRawData(this.getId());
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x0006E7D8 File Offset: 0x0006C9D8
		public override string getName()
		{
			SKALDProjectData.JournalContainers.JournalEntry rawData = this.getRawData();
			if (rawData != null)
			{
				return rawData.title;
			}
			return "";
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x0006E7FC File Offset: 0x0006C9FC
		public override string getDescription()
		{
			SKALDProjectData.JournalContainers.JournalEntry rawData = this.getRawData();
			string text = this.date + "\n" + this.chapter + "\n\n";
			if (rawData != null)
			{
				text += rawData.description;
			}
			return text;
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x0006E83D File Offset: 0x0006CA3D
		public string getChapter()
		{
			return this.chapter;
		}

		// Token: 0x040008E4 RID: 2276
		private string date = "";

		// Token: 0x040008E5 RID: 2277
		private string chapter;
	}
}
