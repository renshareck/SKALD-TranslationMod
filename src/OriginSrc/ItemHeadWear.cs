using System;
using System.Runtime.Serialization;

// Token: 0x020000C9 RID: 201
[Serializable]
public class ItemHeadWear : ItemAccessory, ISerializable
{
	// Token: 0x06000C9F RID: 3231 RVA: 0x0003ABEE File Offset: 0x00038DEE
	public ItemHeadWear(SKALDProjectData.ItemDataContainers.AccessoryContainer.Accessory rawData) : base(rawData)
	{
	}

	// Token: 0x06000CA0 RID: 3232 RVA: 0x0003ABF8 File Offset: 0x00038DF8
	public ItemHeadWear(SerializationInfo info, StreamingContext context)
	{
		this.dynamicData = (Item.ItemSaveData)info.GetValue("saveData", typeof(Item.ItemSaveData));
		if (this.dynamicData == null)
		{
			MainControl.logError("Headware could not load dynamic data!");
		}
		this.applySaveData(this.dynamicData);
	}

	// Token: 0x06000CA1 RID: 3233 RVA: 0x0003AC49 File Offset: 0x00038E49
	public new void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("saveData", this.dynamicData, typeof(Item.ItemSaveData));
	}

	// Token: 0x06000CA2 RID: 3234 RVA: 0x0003AC66 File Offset: 0x00038E66
	public override Item.ItemTypes getType()
	{
		return Item.ItemTypes.Headwear;
	}

	// Token: 0x06000CA3 RID: 3235 RVA: 0x0003AC69 File Offset: 0x00038E69
	protected override string getModelBasePath()
	{
		return "Images/Models/Headwear/";
	}

	// Token: 0x06000CA4 RID: 3236 RVA: 0x0003AC70 File Offset: 0x00038E70
	public bool hidesHair()
	{
		SKALDProjectData.ItemDataContainers.AccessoryContainer.Accessory accessory = GameData.getItemRawData(this.getId()) as SKALDProjectData.ItemDataContainers.AccessoryContainer.Accessory;
		return accessory == null || accessory.hideHair;
	}

	// Token: 0x06000CA5 RID: 3237 RVA: 0x0003AC9C File Offset: 0x00038E9C
	public bool hidesBeard()
	{
		SKALDProjectData.ItemDataContainers.AccessoryContainer.Accessory accessory = GameData.getItemRawData(this.getId()) as SKALDProjectData.ItemDataContainers.AccessoryContainer.Accessory;
		return accessory == null || accessory.hideBeard;
	}

	// Token: 0x06000CA6 RID: 3238 RVA: 0x0003ACC8 File Offset: 0x00038EC8
	public override string printComparativeStats(Character compareCharacter)
	{
		ItemAccessory itemAccessory = null;
		if (compareCharacter != null)
		{
			itemAccessory = compareCharacter.getCurrentHeadwear();
		}
		if (itemAccessory == this)
		{
			itemAccessory = null;
		}
		return this.getArmorComparativeStat(itemAccessory);
	}
}
