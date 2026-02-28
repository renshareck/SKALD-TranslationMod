using System;
using System.Runtime.Serialization;

// Token: 0x020000D6 RID: 214
[Serializable]
public class ItemTrinket : Item, ISerializable
{
	// Token: 0x06000D00 RID: 3328 RVA: 0x0003BAF6 File Offset: 0x00039CF6
	public ItemTrinket(SKALDProjectData.ItemDataContainers.TrinketContainer.Trinket rawData) : base(rawData)
	{
	}

	// Token: 0x06000D01 RID: 3329 RVA: 0x0003BAFF File Offset: 0x00039CFF
	public ItemTrinket(SKALDProjectData.ItemDataContainers.GemContainer.Gem rawData) : base(rawData)
	{
	}

	// Token: 0x06000D02 RID: 3330 RVA: 0x0003BB08 File Offset: 0x00039D08
	public ItemTrinket(SerializationInfo info, StreamingContext context)
	{
		this.dynamicData = (Item.ItemSaveData)info.GetValue("saveData", typeof(Item.ItemSaveData));
		if (this.dynamicData == null)
		{
			MainControl.logError("Trinket could not load dynamic data!");
		}
		this.applySaveData(this.dynamicData);
	}

	// Token: 0x06000D03 RID: 3331 RVA: 0x0003BB59 File Offset: 0x00039D59
	public new void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("saveData", this.dynamicData, typeof(Item.ItemSaveData));
	}

	// Token: 0x06000D04 RID: 3332 RVA: 0x0003BB76 File Offset: 0x00039D76
	public override Item.ItemTypes getType()
	{
		return Item.ItemTypes.Trinket;
	}
}
