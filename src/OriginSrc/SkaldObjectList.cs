using System;
using System.Collections.Generic;

// Token: 0x0200010C RID: 268
[Serializable]
public class SkaldObjectList : SkaldBaseList
{
	// Token: 0x060010F3 RID: 4339 RVA: 0x0004CBFB File Offset: 0x0004ADFB
	public SkaldObjectList()
	{
		this.setName("Components");
	}

	// Token: 0x060010F4 RID: 4340 RVA: 0x0004CC17 File Offset: 0x0004AE17
	public SkaldObjectList(string title)
	{
		this.setName(title);
	}

	// Token: 0x060010F5 RID: 4341 RVA: 0x0004CC2F File Offset: 0x0004AE2F
	public SkaldBaseObject getCurrentObject()
	{
		if (this.currentObject != null)
		{
			return this.currentObject;
		}
		if (base.isEmpty())
		{
			return null;
		}
		this.currentObject = this.objectList[0];
		return this.currentObject;
	}

	// Token: 0x060010F6 RID: 4342 RVA: 0x0004CC62 File Offset: 0x0004AE62
	public override void purgeList()
	{
		base.purgeList();
		this.currentObject = null;
	}

	// Token: 0x060010F7 RID: 4343 RVA: 0x0004CC71 File Offset: 0x0004AE71
	public void setCurrentObject(SkaldBaseObject obj)
	{
		if (this.objectList.Contains(obj))
		{
			this.currentObject = obj;
		}
	}

	// Token: 0x060010F8 RID: 4344 RVA: 0x0004CC88 File Offset: 0x0004AE88
	public override SkaldBaseObject getObjectByIndex(int i)
	{
		SkaldBaseObject objectByIndex = base.getObjectByIndex(i);
		if (objectByIndex != null)
		{
			this.currentObject = objectByIndex;
		}
		return this.currentObject;
	}

	// Token: 0x060010F9 RID: 4345 RVA: 0x0004CCAD File Offset: 0x0004AEAD
	public virtual void setLastObjectAsCurrentObject()
	{
		if (base.isEmpty())
		{
			return;
		}
		this.currentObject = this.objectList[base.getCount() - 1];
		this.setScrollIndex(base.getCount() - this.getMaxPageSize() / 2);
	}

	// Token: 0x060010FA RID: 4346 RVA: 0x0004CCE6 File Offset: 0x0004AEE6
	public void setFirstObjectAsCurrentObject()
	{
		if (base.isEmpty())
		{
			return;
		}
		this.currentObject = this.objectList[0];
		this.setScrollIndex(0);
	}

	// Token: 0x060010FB RID: 4347 RVA: 0x0004CD0A File Offset: 0x0004AF0A
	public void setScrollIndex(int index)
	{
		this.scrollIndex = index;
		if (this.scrollIndex < 0)
		{
			this.scrollIndex = 0;
			return;
		}
		if (this.scrollIndex >= base.getCount())
		{
			this.scrollIndex = base.getCount() - 1;
		}
	}

	// Token: 0x060010FC RID: 4348 RVA: 0x0004CD40 File Offset: 0x0004AF40
	public void setFirstSelectableObjectAsCurrentObject()
	{
		if (base.isEmpty())
		{
			return;
		}
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			CharacterFeature characterFeature = (CharacterFeature)skaldBaseObject;
			if (characterFeature.isSelectable())
			{
				this.currentObject = characterFeature;
				break;
			}
		}
	}

	// Token: 0x060010FD RID: 4349 RVA: 0x0004CDAC File Offset: 0x0004AFAC
	public SkaldBaseObject getObjectByPageIndex(int i)
	{
		i = this.getScrollIndex() + i;
		return this.getObjectByIndex(i);
	}

	// Token: 0x060010FE RID: 4350 RVA: 0x0004CDBF File Offset: 0x0004AFBF
	protected int getScrollIndex()
	{
		return this.scrollIndex;
	}

	// Token: 0x060010FF RID: 4351 RVA: 0x0004CDC7 File Offset: 0x0004AFC7
	protected int getIndexAtEndOfPage()
	{
		return this.getScrollIndex() + this.getMaxPageSize();
	}

	// Token: 0x06001100 RID: 4352 RVA: 0x0004CDD6 File Offset: 0x0004AFD6
	public void setMaxPageSize(int newSize)
	{
		this.maxPageSize = newSize;
	}

	// Token: 0x06001101 RID: 4353 RVA: 0x0004CDDF File Offset: 0x0004AFDF
	public int getMaxPageSize()
	{
		if (this.maxPageSize == 0)
		{
			this.maxPageSize = 20;
		}
		return this.maxPageSize;
	}

	// Token: 0x06001102 RID: 4354 RVA: 0x0004CDF8 File Offset: 0x0004AFF8
	public virtual List<string> getScrolledStringList()
	{
		List<string> list = new List<string>();
		try
		{
			int num = this.getScrollIndex();
			while (num < this.getIndexAtEndOfPage() && num < base.getCount())
			{
				SkaldBaseObject skaldBaseObject = this.objectList[num];
				if (skaldBaseObject == this.currentObject)
				{
					list.Add(C64Color.YELLOW_TAG + skaldBaseObject.getListName() + "</color>");
				}
				else
				{
					list.Add(skaldBaseObject.getListName());
				}
				num++;
			}
		}
		catch
		{
		}
		return list;
	}

	// Token: 0x06001103 RID: 4355 RVA: 0x0004CE80 File Offset: 0x0004B080
	public void setNextObject(int index)
	{
		if (base.isEmpty())
		{
			this.currentObject = null;
			return;
		}
		int count = this.objectList.Count;
		int num = 0;
		while (num < count && this.objectList[num] != this.currentObject)
		{
			num++;
		}
		num += index;
		if (num < 0)
		{
			num = count - 1;
		}
		if (num >= count)
		{
			num = 0;
		}
		this.currentObject = this.objectList[num];
	}

	// Token: 0x06001104 RID: 4356 RVA: 0x0004CEF0 File Offset: 0x0004B0F0
	public string getCurrentObjectTitle()
	{
		SkaldBaseObject skaldBaseObject = this.getCurrentObject();
		if (skaldBaseObject != null)
		{
			return skaldBaseObject.getName();
		}
		return "";
	}

	// Token: 0x06001105 RID: 4357 RVA: 0x0004CF14 File Offset: 0x0004B114
	public string getCurrentObjectId()
	{
		SkaldBaseObject skaldBaseObject = this.getCurrentObject();
		if (skaldBaseObject != null)
		{
			return skaldBaseObject.getId();
		}
		return "";
	}

	// Token: 0x06001106 RID: 4358 RVA: 0x0004CF38 File Offset: 0x0004B138
	public string getCurrentObjectDescription()
	{
		SkaldBaseObject skaldBaseObject = this.getCurrentObject();
		if (skaldBaseObject != null)
		{
			return skaldBaseObject.getDescription();
		}
		return "";
	}

	// Token: 0x06001107 RID: 4359 RVA: 0x0004CF5B File Offset: 0x0004B15B
	public virtual string getCurrentObjectFullDescriptionAndHeader()
	{
		if (this.getCurrentObject() != null)
		{
			return this.getCurrentObject().getFullDescriptionAndHeader();
		}
		return "";
	}

	// Token: 0x06001108 RID: 4360 RVA: 0x0004CF76 File Offset: 0x0004B176
	public override SkaldBaseObject add(SkaldBaseObject component)
	{
		base.add(component);
		this.currentObject = component;
		base.sortList();
		return component;
	}

	// Token: 0x06001109 RID: 4361 RVA: 0x0004CF90 File Offset: 0x0004B190
	public virtual SkaldBaseObject removeCurrentObject()
	{
		if (this.getCurrentObject() == null)
		{
			return null;
		}
		SkaldBaseObject skaldBaseObject = this.getCurrentObject();
		this.objectList.Remove(skaldBaseObject);
		this.setNextObject(1);
		return skaldBaseObject;
	}

	// Token: 0x0600110A RID: 4362 RVA: 0x0004CFC3 File Offset: 0x0004B1C3
	public override string deleteObject(string id)
	{
		if (base.isEmpty())
		{
			return "";
		}
		this.currentObject = base.getObject(id);
		if (this.currentObject != null)
		{
			return "Removed " + this.removeCurrentObject().getName();
		}
		return "";
	}

	// Token: 0x0600110B RID: 4363 RVA: 0x0004D004 File Offset: 0x0004B204
	public virtual string printList()
	{
		if (this.objectList.Count == 0)
		{
			return C64Color.GRAY_LIGHT_TAG + " Empty</color>";
		}
		string text = "";
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			if (skaldBaseObject == this.currentObject)
			{
				text = text + C64Color.YELLOW_TAG + skaldBaseObject.getName() + "</color>\n";
			}
			else
			{
				text = text + C64Color.GRAY_LIGHT_TAG + skaldBaseObject.getName() + "</color>\n";
			}
		}
		return text;
	}

	// Token: 0x0600110C RID: 4364 RVA: 0x0004D0B0 File Offset: 0x0004B2B0
	public virtual string printListSimplifiedColor()
	{
		string text = this.getName() + "\n";
		if (this.objectList.Count == 0)
		{
			return C64Color.GRAY_LIGHT_TAG + " Empty</color>";
		}
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			text = text + C64Color.GRAY_LIGHT_TAG + skaldBaseObject.getName() + "</color>\n";
		}
		return text;
	}

	// Token: 0x04000402 RID: 1026
	protected SkaldBaseObject currentObject;

	// Token: 0x04000403 RID: 1027
	private int scrollIndex;

	// Token: 0x04000404 RID: 1028
	private int maxPageSize = 20;
}
