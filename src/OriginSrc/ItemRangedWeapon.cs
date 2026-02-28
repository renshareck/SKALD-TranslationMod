using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

// Token: 0x020000DA RID: 218
[Serializable]
public class ItemRangedWeapon : ItemWeapon, ISerializable
{
	// Token: 0x06000D2E RID: 3374 RVA: 0x0003C550 File Offset: 0x0003A750
	public ItemRangedWeapon(SKALDProjectData.ItemDataContainers.RangedWeaponContainer.RangedWeaponData rawData) : base(rawData)
	{
	}

	// Token: 0x06000D2F RID: 3375 RVA: 0x0003C55C File Offset: 0x0003A75C
	public ItemRangedWeapon(SerializationInfo info, StreamingContext context)
	{
		this.dynamicData = (Item.ItemSaveData)info.GetValue("saveData", typeof(Item.ItemSaveData));
		if (this.dynamicData == null)
		{
			MainControl.logError("Ranged Weapon could not load dynamic data!");
		}
		this.applySaveData(this.dynamicData);
	}

	// Token: 0x06000D30 RID: 3376 RVA: 0x0003C5AD File Offset: 0x0003A7AD
	public new void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("saveData", this.dynamicData, typeof(Item.ItemSaveData));
	}

	// Token: 0x06000D31 RID: 3377 RVA: 0x0003C5CA File Offset: 0x0003A7CA
	public override Item.ItemTypes getType()
	{
		return Item.ItemTypes.RangedWeapon;
	}

	// Token: 0x06000D32 RID: 3378 RVA: 0x0003C5CD File Offset: 0x0003A7CD
	protected override string getTypeString()
	{
		return "Ranged, " + base.getTypeString();
	}

	// Token: 0x06000D33 RID: 3379 RVA: 0x0003C5DF File Offset: 0x0003A7DF
	public override List<string> getAttackSound()
	{
		return new List<string>
		{
			"WeaponBow1",
			"WeaponBow2",
			"WeaponBow3"
		};
	}

	// Token: 0x06000D34 RID: 3380 RVA: 0x0003C607 File Offset: 0x0003A807
	protected override string getUseSound()
	{
		return "ItemBow1";
	}

	// Token: 0x06000D35 RID: 3381 RVA: 0x0003C610 File Offset: 0x0003A810
	public string getAmmoType()
	{
		SKALDProjectData.ItemDataContainers.RangedWeaponContainer.RangedWeaponData rawData = this.getRawData();
		if (rawData == null)
		{
			return "";
		}
		if (rawData.ammo != "")
		{
			return rawData.ammo;
		}
		if (rawData.parent != null && rawData.parent != "")
		{
			SKALDProjectData.ItemDataContainers.RangedWeaponContainer.RangedWeaponData rangedWeaponData = GameData.getItemRawData(rawData.parent) as SKALDProjectData.ItemDataContainers.RangedWeaponContainer.RangedWeaponData;
			if (rangedWeaponData != null)
			{
				return rangedWeaponData.ammo;
			}
		}
		return "";
	}

	// Token: 0x06000D36 RID: 3382 RVA: 0x0003C680 File Offset: 0x0003A880
	public new SKALDProjectData.ItemDataContainers.RangedWeaponContainer.RangedWeaponData getRawData()
	{
		SKALDProjectData.ItemDataContainers.ItemData rawData = base.getRawData();
		if (rawData is SKALDProjectData.ItemDataContainers.RangedWeaponContainer.RangedWeaponData)
		{
			return rawData as SKALDProjectData.ItemDataContainers.RangedWeaponContainer.RangedWeaponData;
		}
		return null;
	}

	// Token: 0x06000D37 RID: 3383 RVA: 0x0003C6A4 File Offset: 0x0003A8A4
	public override int getRange()
	{
		SKALDProjectData.ItemDataContainers.RangedWeaponContainer.RangedWeaponData rawData = this.getRawData();
		int num = 1;
		if (rawData != null && rawData.range > num)
		{
			num = rawData.range;
		}
		if (rawData.parent != null && rawData.parent != "")
		{
			SKALDProjectData.ItemDataContainers.RangedWeaponContainer.RangedWeaponData rangedWeaponData = GameData.getItemRawData(rawData.parent) as SKALDProjectData.ItemDataContainers.RangedWeaponContainer.RangedWeaponData;
			if (rangedWeaponData != null && rangedWeaponData.range > num)
			{
				num = rangedWeaponData.range;
			}
		}
		return num;
	}

	// Token: 0x06000D38 RID: 3384 RVA: 0x0003C70C File Offset: 0x0003A90C
	public override bool isRanged()
	{
		return true;
	}

	// Token: 0x06000D39 RID: 3385 RVA: 0x0003C70F File Offset: 0x0003A90F
	public override void setUser(Character c)
	{
		base.setUser(c);
		c.toggleRangedWeapon();
	}
}
