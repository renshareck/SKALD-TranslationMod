using System;
using System.Runtime.Serialization;

// Token: 0x020000D9 RID: 217
[Serializable]
public class ItemMeleeWeapon : ItemWeapon, ISerializable
{
	// Token: 0x06000D27 RID: 3367 RVA: 0x0003C45E File Offset: 0x0003A65E
	public ItemMeleeWeapon(SKALDProjectData.ItemDataContainers.MeleeWeaponContainer.MeleeWeaponData rawData) : base(rawData)
	{
	}

	// Token: 0x06000D28 RID: 3368 RVA: 0x0003C468 File Offset: 0x0003A668
	public ItemMeleeWeapon(SerializationInfo info, StreamingContext context)
	{
		this.dynamicData = (Item.ItemSaveData)info.GetValue("saveData", typeof(Item.ItemSaveData));
		if (this.dynamicData == null)
		{
			MainControl.logError("Melee Weapon could not load dynamic data!");
		}
		this.applySaveData(this.dynamicData);
	}

	// Token: 0x06000D29 RID: 3369 RVA: 0x0003C4B9 File Offset: 0x0003A6B9
	public new void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("saveData", this.dynamicData, typeof(Item.ItemSaveData));
	}

	// Token: 0x06000D2A RID: 3370 RVA: 0x0003C4D6 File Offset: 0x0003A6D6
	public override Item.ItemTypes getType()
	{
		return Item.ItemTypes.MeleeWeapon;
	}

	// Token: 0x06000D2B RID: 3371 RVA: 0x0003C4DC File Offset: 0x0003A6DC
	protected override string getTypeString()
	{
		string text = "Melee, " + base.getTypeString();
		string text2 = base.printDamageTypeString();
		if (text2 != "")
		{
			text = text + ", " + text2;
		}
		return text;
	}

	// Token: 0x06000D2C RID: 3372 RVA: 0x0003C51C File Offset: 0x0003A71C
	public override void setUser(Character c)
	{
		base.setUser(c);
		c.toggleMeleeWeapon();
	}

	// Token: 0x06000D2D RID: 3373 RVA: 0x0003C52C File Offset: 0x0003A72C
	public new SKALDProjectData.ItemDataContainers.MeleeWeaponContainer.MeleeWeaponData getRawData()
	{
		SKALDProjectData.ItemDataContainers.ItemData rawData = base.getRawData();
		if (rawData is SKALDProjectData.ItemDataContainers.MeleeWeaponContainer.MeleeWeaponData)
		{
			return rawData as SKALDProjectData.ItemDataContainers.MeleeWeaponContainer.MeleeWeaponData;
		}
		return null;
	}
}
