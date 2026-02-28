using System;
using System.Runtime.Serialization;

// Token: 0x020000CD RID: 205
[Serializable]
public class ItemLight : ItemEquipable, ISerializable
{
	// Token: 0x06000CB5 RID: 3253 RVA: 0x0003AE9A File Offset: 0x0003909A
	public ItemLight(SKALDProjectData.ItemDataContainers.AdventuringContainer.AdventuringItem rawData) : base(rawData)
	{
	}

	// Token: 0x06000CB6 RID: 3254 RVA: 0x0003AEA4 File Offset: 0x000390A4
	public ItemLight(SerializationInfo info, StreamingContext context)
	{
		this.dynamicData = (Item.ItemSaveData)info.GetValue("saveData", typeof(Item.ItemSaveData));
		if (this.dynamicData == null)
		{
			MainControl.logError("Light could not load dynamic data!");
		}
		this.applySaveData(this.dynamicData);
	}

	// Token: 0x06000CB7 RID: 3255 RVA: 0x0003AEF5 File Offset: 0x000390F5
	public new void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("saveData", this.dynamicData, typeof(Item.ItemSaveData));
	}

	// Token: 0x06000CB8 RID: 3256 RVA: 0x0003AF12 File Offset: 0x00039112
	public override Item.ItemTypes getType()
	{
		return Item.ItemTypes.Light;
	}
}
