using System;
using System.Runtime.Serialization;

// Token: 0x020000D3 RID: 211
[Serializable]
public class ItemRing : ItemJewelry, ISerializable
{
	// Token: 0x06000CDE RID: 3294 RVA: 0x0003B4FC File Offset: 0x000396FC
	public ItemRing(SKALDProjectData.ItemDataContainers.JewelryContainer.Jewelry rawData) : base(rawData)
	{
	}

	// Token: 0x06000CDF RID: 3295 RVA: 0x0003B508 File Offset: 0x00039708
	public ItemRing(SerializationInfo info, StreamingContext context)
	{
		this.dynamicData = (Item.ItemSaveData)info.GetValue("saveData", typeof(Item.ItemSaveData));
		if (this.dynamicData == null)
		{
			MainControl.logError("Ring could not load dynamic data!");
		}
		this.applySaveData(this.dynamicData);
	}

	// Token: 0x06000CE0 RID: 3296 RVA: 0x0003B559 File Offset: 0x00039759
	public new void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("saveData", this.dynamicData, typeof(Item.ItemSaveData));
	}

	// Token: 0x06000CE1 RID: 3297 RVA: 0x0003B576 File Offset: 0x00039776
	protected override string getUseSound()
	{
		return "ItemRing1";
	}

	// Token: 0x06000CE2 RID: 3298 RVA: 0x0003B57D File Offset: 0x0003977D
	public override Item.ItemTypes getType()
	{
		return Item.ItemTypes.Ring;
	}
}
