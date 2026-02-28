using System;
using System.Collections.Generic;

// Token: 0x02000110 RID: 272
public class SkaldDataList : SkaldObjectList
{
	// Token: 0x06001137 RID: 4407 RVA: 0x0004D6BA File Offset: 0x0004B8BA
	public SkaldDataList() : this("", "")
	{
	}

	// Token: 0x06001138 RID: 4408 RVA: 0x0004D6CC File Offset: 0x0004B8CC
	public SkaldDataList(string name, string description) : base(name)
	{
		if (name != "")
		{
			this.addHeader(name, description);
		}
		base.deactivateSorting();
	}

	// Token: 0x06001139 RID: 4409 RVA: 0x0004D6F8 File Offset: 0x0004B8F8
	public void addHeaderListAndSpace(string headerName, string headerDesc, List<SkaldBaseObject> objects)
	{
		this.addHeader(headerName, headerDesc);
		foreach (SkaldBaseObject obj in objects)
		{
			this.addEntry(obj);
		}
		this.addSpace();
	}

	// Token: 0x0600113A RID: 4410 RVA: 0x0004D754 File Offset: 0x0004B954
	public void addEntry(SkaldBaseObject obj)
	{
		this.addEntry(obj.getId(), obj.getName(), "", obj.getFullDescription(), obj.getImagePath(), obj.getListName());
	}

	// Token: 0x0600113B RID: 4411 RVA: 0x0004D77F File Offset: 0x0004B97F
	public void addEntry(string id, string name, string description)
	{
		this.addEntry(id, name, "", description);
	}

	// Token: 0x0600113C RID: 4412 RVA: 0x0004D78F File Offset: 0x0004B98F
	public void addEntry(string id, string name, string value, string description)
	{
		this.addEntry(id, name, value, description, "", "");
	}

	// Token: 0x0600113D RID: 4413 RVA: 0x0004D7A6 File Offset: 0x0004B9A6
	public void addEntry(string id, string name, string value, string description, string iconPath, string listName)
	{
		this.setLastRealEntry(this.add(new SkaldDataList.SkaldListDataObject(id, name, value, description, iconPath, listName)));
	}

	// Token: 0x0600113E RID: 4414 RVA: 0x0004D7C2 File Offset: 0x0004B9C2
	public void addHeader(string name, string description)
	{
		this.add(new SkaldDataList.SkaldListHeaderDataObject(name, description));
	}

	// Token: 0x0600113F RID: 4415 RVA: 0x0004D7D2 File Offset: 0x0004B9D2
	public void addSpace()
	{
		this.add(new SkaldDataList.SkaldEmptyListLineObject());
	}

	// Token: 0x06001140 RID: 4416 RVA: 0x0004D7E0 File Offset: 0x0004B9E0
	public override SkaldBaseObject add(SkaldBaseObject obj)
	{
		base.add(obj);
		base.setFirstObjectAsCurrentObject();
		return obj;
	}

	// Token: 0x06001141 RID: 4417 RVA: 0x0004D7F1 File Offset: 0x0004B9F1
	public void deactivateSaveLastRealEntry()
	{
		this.saveLastRealEntry = false;
	}

	// Token: 0x06001142 RID: 4418 RVA: 0x0004D7FA File Offset: 0x0004B9FA
	private void setLastRealEntry(SkaldBaseObject obj)
	{
		if (this.saveLastRealEntry)
		{
			this.lastRealEntry = obj;
		}
	}

	// Token: 0x06001143 RID: 4419 RVA: 0x0004D80B File Offset: 0x0004BA0B
	public override void setLastObjectAsCurrentObject()
	{
		base.setCurrentObject(this.lastRealEntry);
	}

	// Token: 0x06001144 RID: 4420 RVA: 0x0004D81C File Offset: 0x0004BA1C
	public void setFirstObjectWithADescription()
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			if (skaldBaseObject.getDescription() != "")
			{
				base.setCurrentObject(skaldBaseObject);
				break;
			}
		}
	}

	// Token: 0x06001145 RID: 4421 RVA: 0x0004D884 File Offset: 0x0004BA84
	public void mergeListFormatted(SkaldDataList newList)
	{
		this.addSpace();
		foreach (SkaldBaseObject component in newList.getObjectList())
		{
			this.add(component);
		}
		base.setFirstObjectAsCurrentObject();
	}

	// Token: 0x04000409 RID: 1033
	private SkaldBaseObject lastRealEntry;

	// Token: 0x0400040A RID: 1034
	private bool saveLastRealEntry = true;

	// Token: 0x0200025C RID: 604
	public class SkaldListDataObject : SkaldBaseObject
	{
		// Token: 0x060019E4 RID: 6628 RVA: 0x00071485 File Offset: 0x0006F685
		public SkaldListDataObject(string id, string name, string value, string description) : base(id, name, description)
		{
			this.value = value;
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x000714AE File Offset: 0x0006F6AE
		public SkaldListDataObject(string id, string name, string description) : base(id, name, description)
		{
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x000714CF File Offset: 0x0006F6CF
		public SkaldListDataObject(string id, string name, string value, string description, string imagePath, string listName) : this(id, name, value, description)
		{
			this.setImagePath(imagePath);
			this.listName = listName;
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x000714EC File Offset: 0x0006F6EC
		public override string getListName()
		{
			if (this.listName != "")
			{
				return this.listName;
			}
			if (this.value == "")
			{
				return this.getName();
			}
			return TextTools.formateNameValuePair(this.getName(), this.value);
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x0007153C File Offset: 0x0006F73C
		public void setValue(string value)
		{
			this.value = value;
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x00071545 File Offset: 0x0006F745
		public string getValue()
		{
			return this.value;
		}

		// Token: 0x0400095F RID: 2399
		private string value = "";

		// Token: 0x04000960 RID: 2400
		private string listName = "";
	}

	// Token: 0x0200025D RID: 605
	private class SkaldEmptyListLineObject : SkaldBaseObject
	{
		// Token: 0x060019EA RID: 6634 RVA: 0x0007154D File Offset: 0x0006F74D
		public SkaldEmptyListLineObject() : base("EMPTY", " ", " ")
		{
		}
	}

	// Token: 0x0200025E RID: 606
	private class SkaldListHeaderDataObject : SkaldDataList.SkaldListDataObject
	{
		// Token: 0x060019EB RID: 6635 RVA: 0x00071564 File Offset: 0x0006F764
		public SkaldListHeaderDataObject(string name, string description) : base("HEADER", name, "", description)
		{
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x00071578 File Offset: 0x0006F778
		public override string getListName()
		{
			return C64Color.HEADER_TAG + this.getName() + C64Color.HEADER_CLOSING_TAG;
		}
	}
}
