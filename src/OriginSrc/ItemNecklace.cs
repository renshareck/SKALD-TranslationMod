using System;
using System.Runtime.Serialization;

// Token: 0x020000D0 RID: 208
[Serializable]
public class ItemNecklace : ItemJewelry, ISerializable
{
	// Token: 0x06000CC5 RID: 3269 RVA: 0x0003B03A File Offset: 0x0003923A
	public ItemNecklace(SKALDProjectData.ItemDataContainers.JewelryContainer.Jewelry rawData) : base(rawData)
	{
	}

	// Token: 0x06000CC6 RID: 3270 RVA: 0x0003B044 File Offset: 0x00039244
	public ItemNecklace(SerializationInfo info, StreamingContext context)
	{
		this.dynamicData = (Item.ItemSaveData)info.GetValue("saveData", typeof(Item.ItemSaveData));
		if (this.dynamicData == null)
		{
			MainControl.logError("Necklace could not load dynamic data!");
		}
		this.applySaveData(this.dynamicData);
	}

	// Token: 0x06000CC7 RID: 3271 RVA: 0x0003B095 File Offset: 0x00039295
	public new void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("saveData", this.dynamicData, typeof(Item.ItemSaveData));
	}

	// Token: 0x06000CC8 RID: 3272 RVA: 0x0003B0B2 File Offset: 0x000392B2
	protected override string getUseSound()
	{
		return "ItemJewelry1";
	}

	// Token: 0x06000CC9 RID: 3273 RVA: 0x0003B0B9 File Offset: 0x000392B9
	public override Item.ItemTypes getType()
	{
		return Item.ItemTypes.Necklace;
	}
}
