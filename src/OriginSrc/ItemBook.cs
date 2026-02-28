using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

// Token: 0x020000C1 RID: 193
[Serializable]
public class ItemBook : ItemUseable, ISerializable
{
	// Token: 0x06000C56 RID: 3158 RVA: 0x00039EEA File Offset: 0x000380EA
	public ItemBook(SKALDProjectData.ItemDataContainers.BookContainer.Book rawData) : base(rawData)
	{
	}

	// Token: 0x06000C57 RID: 3159 RVA: 0x00039EF3 File Offset: 0x000380F3
	public ItemBook()
	{
	}

	// Token: 0x06000C58 RID: 3160 RVA: 0x00039EFC File Offset: 0x000380FC
	public ItemBook(SerializationInfo info, StreamingContext context)
	{
		this.dynamicData = (Item.ItemSaveData)info.GetValue("saveData", typeof(Item.ItemSaveData));
		if (this.dynamicData == null)
		{
			MainControl.logError("Book could not load dynamic data!");
		}
		this.applySaveData(this.dynamicData);
	}

	// Token: 0x06000C59 RID: 3161 RVA: 0x00039F4D File Offset: 0x0003814D
	public new void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("saveData", this.dynamicData, typeof(Item.ItemSaveData));
	}

	// Token: 0x06000C5A RID: 3162 RVA: 0x00039F6A File Offset: 0x0003816A
	public override Item.ItemTypes getType()
	{
		return Item.ItemTypes.Book;
	}

	// Token: 0x06000C5B RID: 3163 RVA: 0x00039F6E File Offset: 0x0003816E
	protected override string getUseSound()
	{
		return "ItemPaper1";
	}

	// Token: 0x06000C5C RID: 3164 RVA: 0x00039F75 File Offset: 0x00038175
	public override bool isStackable()
	{
		return true;
	}

	// Token: 0x06000C5D RID: 3165 RVA: 0x00039F78 File Offset: 0x00038178
	public override string getUseVerb(Character character)
	{
		return "Read";
	}

	// Token: 0x06000C5E RID: 3166 RVA: 0x00039F80 File Offset: 0x00038180
	public new SKALDProjectData.ItemDataContainers.BookContainer.Book getRawData()
	{
		SKALDProjectData.ItemDataContainers.ItemData rawData = base.getRawData();
		if (rawData is SKALDProjectData.ItemDataContainers.BookContainer.Book)
		{
			return rawData as SKALDProjectData.ItemDataContainers.BookContainer.Book;
		}
		return null;
	}

	// Token: 0x06000C5F RID: 3167 RVA: 0x00039FA4 File Offset: 0x000381A4
	public virtual List<string> getContent()
	{
		SKALDProjectData.ItemDataContainers.BookContainer.Book rawData = this.getRawData();
		if (rawData == null || rawData.content == null || rawData.content == "")
		{
			return null;
		}
		string[] array = rawData.content.Split(new char[]
		{
			'*'
		});
		List<string> list = new List<string>();
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			string item = array2[i].Trim();
			list.Add(item);
		}
		if (list.Count % 2 != 0)
		{
			list.Add(" ");
		}
		return list;
	}

	// Token: 0x06000C60 RID: 3168 RVA: 0x0003A02A File Offset: 0x0003822A
	public override SkaldActionResult useItem(Character user)
	{
		PopUpControl.addPopUpBook(this);
		return base.useItem(user);
	}
}
