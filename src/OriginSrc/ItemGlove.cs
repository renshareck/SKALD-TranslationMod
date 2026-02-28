using System;
using System.Runtime.Serialization;

// Token: 0x020000C8 RID: 200
[Serializable]
public class ItemGlove : ItemAccessory, ISerializable
{
	// Token: 0x06000C9A RID: 3226 RVA: 0x0003AB4A File Offset: 0x00038D4A
	public ItemGlove(SKALDProjectData.ItemDataContainers.AccessoryContainer.Accessory rawData) : base(rawData)
	{
	}

	// Token: 0x06000C9B RID: 3227 RVA: 0x0003AB54 File Offset: 0x00038D54
	public ItemGlove(SerializationInfo info, StreamingContext context)
	{
		this.dynamicData = (Item.ItemSaveData)info.GetValue("saveData", typeof(Item.ItemSaveData));
		if (this.dynamicData == null)
		{
			MainControl.logError("Glove could not load dynamic data!");
		}
		this.applySaveData(this.dynamicData);
	}

	// Token: 0x06000C9C RID: 3228 RVA: 0x0003ABA5 File Offset: 0x00038DA5
	public new void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("saveData", this.dynamicData, typeof(Item.ItemSaveData));
	}

	// Token: 0x06000C9D RID: 3229 RVA: 0x0003ABC2 File Offset: 0x00038DC2
	public override Item.ItemTypes getType()
	{
		return Item.ItemTypes.Glove;
	}

	// Token: 0x06000C9E RID: 3230 RVA: 0x0003ABC8 File Offset: 0x00038DC8
	public override string printComparativeStats(Character compareCharacter)
	{
		ItemAccessory itemAccessory = null;
		if (compareCharacter != null)
		{
			itemAccessory = compareCharacter.getCurrentGloves();
		}
		if (itemAccessory == this)
		{
			itemAccessory = null;
		}
		return this.getArmorComparativeStat(itemAccessory);
	}
}
