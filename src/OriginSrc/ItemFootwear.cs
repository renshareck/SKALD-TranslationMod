using System;
using System.Runtime.Serialization;

// Token: 0x020000C7 RID: 199
[Serializable]
public class ItemFootwear : ItemAccessory, ISerializable
{
	// Token: 0x06000C95 RID: 3221 RVA: 0x0003AAA6 File Offset: 0x00038CA6
	public ItemFootwear(SKALDProjectData.ItemDataContainers.AccessoryContainer.Accessory rawData) : base(rawData)
	{
	}

	// Token: 0x06000C96 RID: 3222 RVA: 0x0003AAB0 File Offset: 0x00038CB0
	public ItemFootwear(SerializationInfo info, StreamingContext context)
	{
		this.dynamicData = (Item.ItemSaveData)info.GetValue("saveData", typeof(Item.ItemSaveData));
		if (this.dynamicData == null)
		{
			MainControl.logError("Glove could not load dynamic data!");
		}
		this.applySaveData(this.dynamicData);
	}

	// Token: 0x06000C97 RID: 3223 RVA: 0x0003AB01 File Offset: 0x00038D01
	public new void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("saveData", this.dynamicData, typeof(Item.ItemSaveData));
	}

	// Token: 0x06000C98 RID: 3224 RVA: 0x0003AB1E File Offset: 0x00038D1E
	public override Item.ItemTypes getType()
	{
		return Item.ItemTypes.Footwear;
	}

	// Token: 0x06000C99 RID: 3225 RVA: 0x0003AB24 File Offset: 0x00038D24
	public override string printComparativeStats(Character compareCharacter)
	{
		ItemAccessory itemAccessory = null;
		if (compareCharacter != null)
		{
			itemAccessory = compareCharacter.getCurrentFootwear();
		}
		if (itemAccessory == this)
		{
			itemAccessory = null;
		}
		return this.getArmorComparativeStat(itemAccessory);
	}
}
