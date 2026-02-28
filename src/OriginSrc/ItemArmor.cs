using System;
using System.Runtime.Serialization;

// Token: 0x020000BF RID: 191
[Serializable]
public class ItemArmor : ItemArmorBase, ISerializable
{
	// Token: 0x06000C45 RID: 3141 RVA: 0x00039AB1 File Offset: 0x00037CB1
	public ItemArmor(SKALDProjectData.ItemDataContainers.ArmorContainer.Armor rawData) : base(rawData)
	{
	}

	// Token: 0x06000C46 RID: 3142 RVA: 0x00039ABC File Offset: 0x00037CBC
	public ItemArmor(SerializationInfo info, StreamingContext context)
	{
		this.dynamicData = (Item.ItemSaveData)info.GetValue("saveData", typeof(Item.ItemSaveData));
		if (this.dynamicData == null)
		{
			MainControl.logError("Armor could not load dynamic data!");
		}
		this.applySaveData(this.dynamicData);
	}

	// Token: 0x06000C47 RID: 3143 RVA: 0x00039B0D File Offset: 0x00037D0D
	public new void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("saveData", this.dynamicData, typeof(Item.ItemSaveData));
	}

	// Token: 0x06000C48 RID: 3144 RVA: 0x00039B2C File Offset: 0x00037D2C
	public override string printComparativeStats(Character compareCharacter)
	{
		ItemArmor itemArmor = null;
		if (compareCharacter != null)
		{
			itemArmor = compareCharacter.getCurrentArmor();
		}
		if (itemArmor == this)
		{
			itemArmor = null;
		}
		return this.getArmorComparativeStat(itemArmor);
	}

	// Token: 0x06000C49 RID: 3145 RVA: 0x00039B52 File Offset: 0x00037D52
	public override Item.ItemTypes getType()
	{
		return Item.ItemTypes.Armor;
	}

	// Token: 0x06000C4A RID: 3146 RVA: 0x00039B55 File Offset: 0x00037D55
	protected override string getTypeString()
	{
		return this.getWeightCategory() + " " + base.getTypeString();
	}

	// Token: 0x06000C4B RID: 3147 RVA: 0x00039B6D File Offset: 0x00037D6D
	protected override string getModelBasePath()
	{
		return "Images/Models/Armor/";
	}
}
