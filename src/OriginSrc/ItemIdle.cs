using System;
using System.Runtime.Serialization;

// Token: 0x020000CA RID: 202
[Serializable]
public class ItemIdle : Item, ISerializable
{
	// Token: 0x06000CA7 RID: 3239 RVA: 0x0003ACEE File Offset: 0x00038EEE
	public ItemIdle(SKALDProjectData.ItemDataContainers.IdleItemContainer.IdleItemData rawData) : base(rawData)
	{
	}

	// Token: 0x06000CA8 RID: 3240 RVA: 0x0003ACF8 File Offset: 0x00038EF8
	public ItemIdle(SerializationInfo info, StreamingContext context)
	{
		this.dynamicData = (Item.ItemSaveData)info.GetValue("saveData", typeof(Item.ItemSaveData));
		if (this.dynamicData == null)
		{
			MainControl.logError("Idle Item could not load dynamic data!");
		}
		this.applySaveData(this.dynamicData);
	}

	// Token: 0x06000CA9 RID: 3241 RVA: 0x0003AD49 File Offset: 0x00038F49
	public new void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("saveData", this.dynamicData, typeof(Item.ItemSaveData));
	}

	// Token: 0x06000CAA RID: 3242 RVA: 0x0003AD66 File Offset: 0x00038F66
	public override Item.ItemTypes getType()
	{
		return Item.ItemTypes.Idle;
	}

	// Token: 0x06000CAB RID: 3243 RVA: 0x0003AD6C File Offset: 0x00038F6C
	public string getIdleItemAnimation()
	{
		SKALDProjectData.ItemDataContainers.IdleItemContainer.IdleItemData idleItemData = GameData.getItemRawData(this.getId()) as SKALDProjectData.ItemDataContainers.IdleItemContainer.IdleItemData;
		if (idleItemData == null)
		{
			return "";
		}
		return idleItemData.idleItemAnimation;
	}
}
