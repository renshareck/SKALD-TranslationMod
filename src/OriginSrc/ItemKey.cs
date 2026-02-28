using System;
using System.Runtime.Serialization;

// Token: 0x020000CC RID: 204
[Serializable]
public class ItemKey : Item, ISerializable
{
	// Token: 0x06000CB1 RID: 3249 RVA: 0x0003AE1E File Offset: 0x0003901E
	public ItemKey(SKALDProjectData.ItemDataContainers.KeyContainer.Key rawData) : base(rawData)
	{
	}

	// Token: 0x06000CB2 RID: 3250 RVA: 0x0003AE28 File Offset: 0x00039028
	public ItemKey(SerializationInfo info, StreamingContext context)
	{
		this.dynamicData = (Item.ItemSaveData)info.GetValue("saveData", typeof(Item.ItemSaveData));
		if (this.dynamicData == null)
		{
			MainControl.logError("Key could not load dynamic data!");
		}
		this.applySaveData(this.dynamicData);
	}

	// Token: 0x06000CB3 RID: 3251 RVA: 0x0003AE79 File Offset: 0x00039079
	public new void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("saveData", this.dynamicData, typeof(Item.ItemSaveData));
	}

	// Token: 0x06000CB4 RID: 3252 RVA: 0x0003AE96 File Offset: 0x00039096
	public override Item.ItemTypes getType()
	{
		return Item.ItemTypes.Key;
	}
}
