using System;
using System.Runtime.Serialization;

// Token: 0x020000BD RID: 189
[Serializable]
public class ItemAdventuring : ItemUseable, ISerializable
{
	// Token: 0x06000C34 RID: 3124 RVA: 0x00039742 File Offset: 0x00037942
	public ItemAdventuring(SKALDProjectData.ItemDataContainers.AdventuringContainer.AdventuringItem rawData) : base(rawData)
	{
	}

	// Token: 0x06000C35 RID: 3125 RVA: 0x0003974C File Offset: 0x0003794C
	public ItemAdventuring(SerializationInfo info, StreamingContext context)
	{
		this.dynamicData = (Item.ItemSaveData)info.GetValue("saveData", typeof(Item.ItemSaveData));
		if (this.dynamicData == null)
		{
			MainControl.logError("Adventuring Item could not load dynamic data!");
		}
		this.applySaveData(this.dynamicData);
	}

	// Token: 0x06000C36 RID: 3126 RVA: 0x0003979D File Offset: 0x0003799D
	public new void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("saveData", this.dynamicData, typeof(Item.ItemSaveData));
	}

	// Token: 0x06000C37 RID: 3127 RVA: 0x000397BA File Offset: 0x000379BA
	public override Item.ItemTypes getType()
	{
		return Item.ItemTypes.Adventuring;
	}
}
