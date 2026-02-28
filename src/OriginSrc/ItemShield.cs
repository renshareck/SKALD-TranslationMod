using System;
using System.Runtime.Serialization;

// Token: 0x020000D4 RID: 212
[Serializable]
public class ItemShield : ItemArmorBase, ISerializable
{
	// Token: 0x06000CE3 RID: 3299 RVA: 0x0003B581 File Offset: 0x00039781
	public ItemShield(SKALDProjectData.ItemDataContainers.ShieldContainer.Shield rawData) : base(rawData)
	{
	}

	// Token: 0x06000CE4 RID: 3300 RVA: 0x0003B58C File Offset: 0x0003978C
	public ItemShield(SerializationInfo info, StreamingContext context)
	{
		this.dynamicData = (Item.ItemSaveData)info.GetValue("saveData", typeof(Item.ItemSaveData));
		if (this.dynamicData == null)
		{
			MainControl.logError("Shield could not load dynamic data!");
		}
		this.applySaveData(this.dynamicData);
	}

	// Token: 0x06000CE5 RID: 3301 RVA: 0x0003B5DD File Offset: 0x000397DD
	public new void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("saveData", this.dynamicData, typeof(Item.ItemSaveData));
	}

	// Token: 0x06000CE6 RID: 3302 RVA: 0x0003B5FC File Offset: 0x000397FC
	public int getBackXOffset()
	{
		SKALDProjectData.ItemDataContainers.ShieldContainer.Shield shield = GameData.getItemRawData(this.getId()) as SKALDProjectData.ItemDataContainers.ShieldContainer.Shield;
		if (shield == null)
		{
			return 0;
		}
		return shield.backstrapXOffset;
	}

	// Token: 0x06000CE7 RID: 3303 RVA: 0x0003B628 File Offset: 0x00039828
	public int getBackYOffset()
	{
		SKALDProjectData.ItemDataContainers.ShieldContainer.Shield shield = GameData.getItemRawData(this.getId()) as SKALDProjectData.ItemDataContainers.ShieldContainer.Shield;
		if (shield == null)
		{
			return 0;
		}
		return shield.backstrapYOffset;
	}

	// Token: 0x06000CE8 RID: 3304 RVA: 0x0003B654 File Offset: 0x00039854
	public override string printComparativeStats(Character compareCharacter)
	{
		ItemShield itemShield = null;
		if (compareCharacter != null)
		{
			itemShield = compareCharacter.getCurrentShieldIfInHand();
		}
		if (itemShield == this)
		{
			itemShield = null;
		}
		return this.getArmorComparativeStat(itemShield);
	}

	// Token: 0x06000CE9 RID: 3305 RVA: 0x0003B67C File Offset: 0x0003987C
	protected override string getArmorComparativeStat(ItemArmorBase compareItem)
	{
		string str = this.printStatsHeader();
		string value;
		if (compareItem == null)
		{
			value = "+" + base.getSoak().ToString() + "\n";
		}
		else
		{
			value = base.makeComparativeColorTag((float)base.getSoak(), (float)compareItem.getSoak(), "+", "") + "\n";
		}
		string str2 = str + TextTools.formateNameValuePair("Dodge", value);
		string value2;
		if (compareItem == null)
		{
			value2 = base.getEncumbrance().ToString() + "\n";
		}
		else
		{
			value2 = base.makeComparativeColorTagReversed((float)base.getEncumbrance(), (float)compareItem.getEncumbrance(), "-", "") + "\n";
		}
		return str2 + TextTools.formateNameValuePair("Enc.", value2) + this.printStatsTail();
	}

	// Token: 0x06000CEA RID: 3306 RVA: 0x0003B759 File Offset: 0x00039959
	public override Item.ItemTypes getType()
	{
		return Item.ItemTypes.Shield;
	}

	// Token: 0x06000CEB RID: 3307 RVA: 0x0003B75C File Offset: 0x0003995C
	protected override string getModelBasePath()
	{
		return "Images/Models/Shields/";
	}
}
