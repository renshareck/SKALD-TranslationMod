using System;
using System.Collections.Generic;

// Token: 0x02000109 RID: 265
[Serializable]
public class SkaldBaseList : SkaldWorldObject
{
	// Token: 0x060010BD RID: 4285 RVA: 0x0004C35B File Offset: 0x0004A55B
	public bool isEmpty()
	{
		return this.objectList == null || this.objectList.Count == 0;
	}

	// Token: 0x060010BE RID: 4286 RVA: 0x0004C375 File Offset: 0x0004A575
	public virtual void purgeList()
	{
		this.objectList.Clear();
	}

	// Token: 0x060010BF RID: 4287 RVA: 0x0004C382 File Offset: 0x0004A582
	public int getCount()
	{
		return this.objectList.Count;
	}

	// Token: 0x060010C0 RID: 4288 RVA: 0x0004C38F File Offset: 0x0004A58F
	public bool hasObjectOnIndex(int index)
	{
		return index >= 0 && index < this.objectList.Count && this.objectList[index] != null;
	}

	// Token: 0x060010C1 RID: 4289 RVA: 0x0004C3B6 File Offset: 0x0004A5B6
	public virtual SkaldBaseObject getObjectByIndex(int i)
	{
		if (!this.hasObjectOnIndex(i))
		{
			return null;
		}
		return this.objectList[i];
	}

	// Token: 0x060010C2 RID: 4290 RVA: 0x0004C3D0 File Offset: 0x0004A5D0
	public List<SkaldBaseObject> getObjectList()
	{
		List<SkaldBaseObject> list = new List<SkaldBaseObject>();
		foreach (SkaldBaseObject item in this.objectList)
		{
			list.Add(item);
		}
		return list;
	}

	// Token: 0x060010C3 RID: 4291 RVA: 0x0004C42C File Offset: 0x0004A62C
	public void deactivateSorting()
	{
		this.shouldSortList = false;
	}

	// Token: 0x060010C4 RID: 4292 RVA: 0x0004C435 File Offset: 0x0004A635
	protected bool shouldBeSorted()
	{
		return this.shouldSortList;
	}

	// Token: 0x060010C5 RID: 4293 RVA: 0x0004C440 File Offset: 0x0004A640
	public void sortList()
	{
		if (!this.shouldSortList)
		{
			return;
		}
		if (this.isEmpty())
		{
			return;
		}
		List<SkaldBaseObject> list = new List<SkaldBaseObject>();
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			if (list.Count == 0)
			{
				list.Add(skaldBaseObject);
			}
			else
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (this.doesObjectSortBeforeAnotherItem(skaldBaseObject, list[i]))
					{
						list.Insert(i, skaldBaseObject);
						break;
					}
					if (i == list.Count - 1)
					{
						list.Add(skaldBaseObject);
						break;
					}
				}
			}
		}
		this.objectList = list;
	}

	// Token: 0x060010C6 RID: 4294 RVA: 0x0004C4FC File Offset: 0x0004A6FC
	protected virtual bool doesObjectSortBeforeAnotherItem(SkaldBaseObject obj1, SkaldBaseObject obj2)
	{
		return string.Compare(obj1.getNameForSorting(), obj2.getNameForSorting()) < 0;
	}

	// Token: 0x060010C7 RID: 4295 RVA: 0x0004C512 File Offset: 0x0004A712
	public virtual SkaldBaseObject add(SkaldBaseObject component)
	{
		this.objectList.Add(component);
		return component;
	}

	// Token: 0x060010C8 RID: 4296 RVA: 0x0004C524 File Offset: 0x0004A724
	public virtual string printListSimplifiedString()
	{
		string text = this.getName().ToUpper() + "\n";
		if (this.objectList.Count == 0)
		{
			return "Empty";
		}
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			text = text + skaldBaseObject.getName() + "\n";
		}
		return text;
	}

	// Token: 0x060010C9 RID: 4297 RVA: 0x0004C5AC File Offset: 0x0004A7AC
	public virtual string printListSingleLine()
	{
		if (this.objectList.Count == 0)
		{
			return "Empty";
		}
		string text = "";
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			text = text + skaldBaseObject.getName() + ", ";
		}
		text = TextTools.removeTrailingComma(text);
		return text;
	}

	// Token: 0x060010CA RID: 4298 RVA: 0x0004C62C File Offset: 0x0004A82C
	public bool containsObject(string id)
	{
		using (List<SkaldBaseObject>.Enumerator enumerator = this.objectList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isId(id))
				{
					return true;
				}
			}
		}
		if (id.Length < 3 || id[3] != '_')
		{
			MainControl.logError("Id may be malformed: " + id);
		}
		return false;
	}

	// Token: 0x060010CB RID: 4299 RVA: 0x0004C6AC File Offset: 0x0004A8AC
	public SkaldBaseObject getObject(string id)
	{
		if (id == "")
		{
			return null;
		}
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			if (skaldBaseObject.isId(id))
			{
				return skaldBaseObject;
			}
		}
		return null;
	}

	// Token: 0x060010CC RID: 4300 RVA: 0x0004C718 File Offset: 0x0004A918
	public virtual string deleteObject(string id)
	{
		if (this.isEmpty())
		{
			return "";
		}
		SkaldBaseObject @object = this.getObject(id);
		if (@object != null)
		{
			this.objectList.Remove(@object);
			return "Removed " + @object.getName();
		}
		return "";
	}

	// Token: 0x060010CD RID: 4301 RVA: 0x0004C764 File Offset: 0x0004A964
	public virtual string printCountList()
	{
		string text = "";
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			text = text + skaldBaseObject.getName() + ", ";
		}
		if (text.Length > 1)
		{
			text = text.Substring(0, text.Length - 2);
		}
		return text;
	}

	// Token: 0x040003FE RID: 1022
	protected List<SkaldBaseObject> objectList = new List<SkaldBaseObject>();

	// Token: 0x040003FF RID: 1023
	private bool shouldSortList = true;
}
