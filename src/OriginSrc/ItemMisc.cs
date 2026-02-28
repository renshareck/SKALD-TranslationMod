using System;
using System.Runtime.Serialization;

// Token: 0x020000CE RID: 206
[Serializable]
public class ItemMisc : Item, ISerializable
{
	// Token: 0x06000CB9 RID: 3257 RVA: 0x0003AF16 File Offset: 0x00039116
	public ItemMisc(SKALDProjectData.ItemDataContainers.ItemData rawData) : base(rawData)
	{
	}

	// Token: 0x06000CBA RID: 3258 RVA: 0x0003AF20 File Offset: 0x00039120
	public ItemMisc(SerializationInfo info, StreamingContext context)
	{
		this.dynamicData = (Item.ItemSaveData)info.GetValue("saveData", typeof(Item.ItemSaveData));
		if (this.dynamicData == null)
		{
			MainControl.logError("MiscItem could not load dynamic data!");
		}
		this.applySaveData(this.dynamicData);
	}

	// Token: 0x06000CBB RID: 3259 RVA: 0x0003AF71 File Offset: 0x00039171
	public new void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("saveData", this.dynamicData, typeof(Item.ItemSaveData));
	}
}
