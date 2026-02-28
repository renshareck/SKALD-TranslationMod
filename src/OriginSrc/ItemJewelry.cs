using System;
using System.Runtime.Serialization;

// Token: 0x020000CB RID: 203
[Serializable]
public class ItemJewelry : ItemEquipable, ISerializable
{
	// Token: 0x06000CAC RID: 3244 RVA: 0x0003AD99 File Offset: 0x00038F99
	public ItemJewelry(SKALDProjectData.ItemDataContainers.JewelryContainer.Jewelry rawData) : base(rawData)
	{
	}

	// Token: 0x06000CAD RID: 3245 RVA: 0x0003ADA2 File Offset: 0x00038FA2
	public ItemJewelry()
	{
	}

	// Token: 0x06000CAE RID: 3246 RVA: 0x0003ADAC File Offset: 0x00038FAC
	public ItemJewelry(SerializationInfo info, StreamingContext context)
	{
		this.dynamicData = (Item.ItemSaveData)info.GetValue("saveData", typeof(Item.ItemSaveData));
		if (this.dynamicData == null)
		{
			MainControl.logError("Jewelry could not load dynamic data!");
		}
		this.applySaveData(this.dynamicData);
	}

	// Token: 0x06000CAF RID: 3247 RVA: 0x0003ADFD File Offset: 0x00038FFD
	public new void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("saveData", this.dynamicData, typeof(Item.ItemSaveData));
	}

	// Token: 0x06000CB0 RID: 3248 RVA: 0x0003AE1A File Offset: 0x0003901A
	public override Item.ItemTypes getType()
	{
		return Item.ItemTypes.Jewelry;
	}
}
