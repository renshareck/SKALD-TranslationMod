using System;
using System.Collections.Generic;

// Token: 0x0200010A RID: 266
[Serializable]
public class SkaldBaseObject
{
	// Token: 0x060010CF RID: 4303 RVA: 0x0004C7FE File Offset: 0x0004A9FE
	protected SkaldBaseObject()
	{
	}

	// Token: 0x060010D0 RID: 4304 RVA: 0x0004C814 File Offset: 0x0004AA14
	protected SkaldBaseObject(SKALDProjectData.GameDataObject rawData)
	{
		try
		{
			this.setId(rawData.id);
			this.coreData.name = this.processString(rawData.title, null);
			this.coreData.description = rawData.description;
			this.coreData.imagePath = this.processString(rawData.imagePath, null);
		}
		catch (Exception obj)
		{
			MainControl.logError(obj);
		}
	}

	// Token: 0x060010D1 RID: 4305 RVA: 0x0004C89C File Offset: 0x0004AA9C
	public SkaldBaseObject(string id, string name, string description)
	{
		this.setId(id);
		this.coreData.name = name;
		this.coreData.description = description;
	}

	// Token: 0x060010D2 RID: 4306 RVA: 0x0004C8CF File Offset: 0x0004AACF
	public SkaldBaseObject(string id)
	{
		this.setId(id);
	}

	// Token: 0x060010D3 RID: 4307 RVA: 0x0004C8EA File Offset: 0x0004AAEA
	public virtual TextureTools.TextureData getGridIcon()
	{
		return null;
	}

	// Token: 0x060010D4 RID: 4308 RVA: 0x0004C8ED File Offset: 0x0004AAED
	public bool isId(string id)
	{
		return this.getId().Equals(id, StringComparison.CurrentCultureIgnoreCase);
	}

	// Token: 0x060010D5 RID: 4309 RVA: 0x0004C8FC File Offset: 0x0004AAFC
	public virtual string getId()
	{
		if (this.coreData == null)
		{
			MainControl.logError("Missing core data!");
			return "";
		}
		return this.coreData.id;
	}

	// Token: 0x060010D6 RID: 4310 RVA: 0x0004C921 File Offset: 0x0004AB21
	public virtual string getColorTag()
	{
		return C64Color.BROWN_LIGHT_TAG;
	}

	// Token: 0x060010D7 RID: 4311 RVA: 0x0004C928 File Offset: 0x0004AB28
	public virtual string getName()
	{
		if (this.coreData == null)
		{
			MainControl.logError("Missing core data!");
			return "";
		}
		return this.coreData.name;
	}

	// Token: 0x060010D8 RID: 4312 RVA: 0x0004C950 File Offset: 0x0004AB50
	public virtual string getNameForSorting()
	{
		string text = this.getName();
		if (text == "" || text == null)
		{
			text = this.getId();
		}
		if (text == "" || text == null)
		{
			text = "0";
		}
		return text;
	}

	// Token: 0x060010D9 RID: 4313 RVA: 0x0004C992 File Offset: 0x0004AB92
	public virtual string getDescription()
	{
		if (this.coreData == null)
		{
			MainControl.logError("Missing core data!");
			return "";
		}
		return this.coreData.description;
	}

	// Token: 0x060010DA RID: 4314 RVA: 0x0004C9B7 File Offset: 0x0004ABB7
	public virtual string getListName()
	{
		return this.getName();
	}

	// Token: 0x060010DB RID: 4315 RVA: 0x0004C9BF File Offset: 0x0004ABBF
	public virtual string getFullDescription()
	{
		return "" + this.getDescription();
	}

	// Token: 0x060010DC RID: 4316 RVA: 0x0004C9D1 File Offset: 0x0004ABD1
	public virtual string getFullDescriptionAndHeader()
	{
		return TextTools.formatFullDesription(this.getName(), this.getFullDescription());
	}

	// Token: 0x060010DD RID: 4317 RVA: 0x0004C9E4 File Offset: 0x0004ABE4
	public virtual string setName(string s)
	{
		this.coreData.name = s;
		return this.coreData.name;
	}

	// Token: 0x060010DE RID: 4318 RVA: 0x0004C9FD File Offset: 0x0004ABFD
	public virtual string setId(string s)
	{
		this.coreData.id = s;
		return this.coreData.id;
	}

	// Token: 0x060010DF RID: 4319 RVA: 0x0004CA16 File Offset: 0x0004AC16
	public virtual string setDescription(string s)
	{
		this.coreData.description = s;
		return this.coreData.description;
	}

	// Token: 0x060010E0 RID: 4320 RVA: 0x0004CA2F File Offset: 0x0004AC2F
	public static string processStringFromOrList(List<string> input, SkaldBaseObject caller = null)
	{
		return TextParser.processStringFromOrList(input, caller);
	}

	// Token: 0x060010E1 RID: 4321 RVA: 0x0004CA38 File Offset: 0x0004AC38
	protected string processString(string s, SkaldBaseObject caller = null)
	{
		if (string.IsNullOrEmpty(s))
		{
			return "";
		}
		return TextParser.processString(s, caller);
	}

	// Token: 0x060010E2 RID: 4322 RVA: 0x0004CA4F File Offset: 0x0004AC4F
	public virtual void applySaveData(BaseSaveData baseSaveData)
	{
		this.coreData = baseSaveData.coreData;
		if (this.coreData == null)
		{
			MainControl.logError("Missing Core Data!");
		}
	}

	// Token: 0x060010E3 RID: 4323 RVA: 0x0004CA6F File Offset: 0x0004AC6F
	public virtual string getImagePath()
	{
		return this.coreData.imagePath;
	}

	// Token: 0x060010E4 RID: 4324 RVA: 0x0004CA7C File Offset: 0x0004AC7C
	public virtual void setImagePath(string s)
	{
		this.coreData.imagePath = s;
	}

	// Token: 0x04000400 RID: 1024
	public SkaldBaseObject.CoreData coreData = new SkaldBaseObject.CoreData();

	// Token: 0x02000259 RID: 601
	[Serializable]
	public class CoreData
	{
		// Token: 0x04000946 RID: 2374
		public string id = "";

		// Token: 0x04000947 RID: 2375
		public string description = "";

		// Token: 0x04000948 RID: 2376
		public string name = "";

		// Token: 0x04000949 RID: 2377
		public string imagePath = "";
	}
}
