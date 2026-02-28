using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

// Token: 0x020000D8 RID: 216
[Serializable]
public abstract class ItemWeapon : ItemDamaging, ISerializable
{
	// Token: 0x06000D0E RID: 3342 RVA: 0x0003BDF8 File Offset: 0x00039FF8
	public ItemWeapon(SKALDProjectData.ItemDataContainers.WeaponData rawData) : base(rawData)
	{
	}

	// Token: 0x06000D0F RID: 3343 RVA: 0x0003BE01 File Offset: 0x0003A001
	public ItemWeapon()
	{
	}

	// Token: 0x06000D10 RID: 3344 RVA: 0x0003BE0C File Offset: 0x0003A00C
	public new SKALDProjectData.ItemDataContainers.WeaponData getRawData()
	{
		SKALDProjectData.ItemDataContainers.ItemData rawData = base.getRawData();
		if (rawData is SKALDProjectData.ItemDataContainers.WeaponData)
		{
			return rawData as SKALDProjectData.ItemDataContainers.WeaponData;
		}
		return null;
	}

	// Token: 0x06000D11 RID: 3345 RVA: 0x0003BE30 File Offset: 0x0003A030
	public string getWeaponType()
	{
		SKALDProjectData.ItemDataContainers.WeaponData rawData = this.getRawData();
		if (rawData == null)
		{
			return "";
		}
		if (rawData.weaponType != "")
		{
			return rawData.weaponType;
		}
		if (rawData.parent != null && rawData.parent != "")
		{
			SKALDProjectData.ItemDataContainers.WeaponData weaponData = GameData.getItemRawData(rawData.parent) as SKALDProjectData.ItemDataContainers.WeaponData;
			if (weaponData != null)
			{
				return weaponData.weaponType;
			}
		}
		return "";
	}

	// Token: 0x06000D12 RID: 3346 RVA: 0x0003BEA0 File Offset: 0x0003A0A0
	public virtual List<string> getAttackSound()
	{
		return new List<string>
		{
			"WeaponSwing1",
			"WeaponSwing2",
			"WeaponSwing3",
			"WeaponSwing4"
		};
	}

	// Token: 0x06000D13 RID: 3347 RVA: 0x0003BED3 File Offset: 0x0003A0D3
	protected override string getUseSound()
	{
		return "ItemWeapon1";
	}

	// Token: 0x06000D14 RID: 3348 RVA: 0x0003BEDC File Offset: 0x0003A0DC
	public override string getPrimaryColor()
	{
		string primaryColor = base.getPrimaryColor();
		if (primaryColor != "")
		{
			return primaryColor;
		}
		int powerLevel = base.getPowerLevel();
		if (powerLevel == 1)
		{
			return C64Color.ColorIds.COL_BrownLight.ToString();
		}
		if (powerLevel == 2)
		{
			return C64Color.ColorIds.COL_GrayLight.ToString();
		}
		if (powerLevel == 3)
		{
			return C64Color.ColorIds.COL_Yellow.ToString();
		}
		return C64Color.ColorIds.COL_Cyan.ToString();
	}

	// Token: 0x06000D15 RID: 3349 RVA: 0x0003BF54 File Offset: 0x0003A154
	public override string getSecondaryColor()
	{
		string secondaryColor = base.getSecondaryColor();
		if (secondaryColor != "")
		{
			return secondaryColor;
		}
		int powerLevel = base.getPowerLevel();
		if (powerLevel == 1)
		{
			return C64Color.ColorIds.COL_GrayDark.ToString();
		}
		if (powerLevel == 2)
		{
			return C64Color.ColorIds.COL_Gray.ToString();
		}
		if (powerLevel == 3)
		{
			return C64Color.ColorIds.COL_GrayLight.ToString();
		}
		return C64Color.ColorIds.COL_White.ToString();
	}

	// Token: 0x06000D16 RID: 3350 RVA: 0x0003BFCC File Offset: 0x0003A1CC
	public bool isTwoHanded()
	{
		SKALDProjectData.ItemDataContainers.WeaponData rawData = this.getRawData();
		return rawData != null && rawData.twoHanded;
	}

	// Token: 0x06000D17 RID: 3351 RVA: 0x0003BFEC File Offset: 0x0003A1EC
	public override string getWeightCategory()
	{
		SKALDProjectData.ItemDataContainers.WeaponData rawData = this.getRawData();
		if (rawData == null)
		{
			return "";
		}
		if (rawData.weightCategory != "")
		{
			return rawData.weightCategory;
		}
		if (rawData.parent != null && rawData.parent != "")
		{
			SKALDProjectData.ItemDataContainers.WeaponData weaponData = GameData.getItemRawData(rawData.parent) as SKALDProjectData.ItemDataContainers.WeaponData;
			if (weaponData != null)
			{
				return weaponData.weightCategory;
			}
		}
		return "";
	}

	// Token: 0x06000D18 RID: 3352 RVA: 0x0003C05C File Offset: 0x0003A25C
	public bool isSword()
	{
		return this.getWeaponType() == "Blade";
	}

	// Token: 0x06000D19 RID: 3353 RVA: 0x0003C06E File Offset: 0x0003A26E
	public bool isClub()
	{
		return this.getWeaponType() == "Club";
	}

	// Token: 0x06000D1A RID: 3354 RVA: 0x0003C080 File Offset: 0x0003A280
	public bool isAxe()
	{
		return this.getWeaponType() == "Axe";
	}

	// Token: 0x06000D1B RID: 3355 RVA: 0x0003C092 File Offset: 0x0003A292
	public bool isBow()
	{
		return this.getWeaponType() == "Bow";
	}

	// Token: 0x06000D1C RID: 3356 RVA: 0x0003C0A4 File Offset: 0x0003A2A4
	public bool isPolearm()
	{
		return this.getWeaponType() == "Polearm";
	}

	// Token: 0x06000D1D RID: 3357 RVA: 0x0003C0B6 File Offset: 0x0003A2B6
	public bool isCrossbow()
	{
		return this.getWeaponType() == "Crossbow";
	}

	// Token: 0x06000D1E RID: 3358 RVA: 0x0003C0C8 File Offset: 0x0003A2C8
	public string getAttackAnimation()
	{
		string text = "";
		SKALDProjectData.ItemDataContainers.WeaponData rawData = this.getRawData();
		if (rawData != null)
		{
			text = rawData.attackAnimation;
		}
		if (text != "")
		{
			return text;
		}
		if (rawData.parent != null && rawData.parent != "")
		{
			SKALDProjectData.ItemDataContainers.WeaponData weaponData = GameData.getItemRawData(rawData.parent) as SKALDProjectData.ItemDataContainers.WeaponData;
			if (weaponData != null)
			{
				text = weaponData.attackAnimation;
			}
		}
		if (text == "")
		{
			text = "ANI_AttackBase";
		}
		return text;
	}

	// Token: 0x06000D1F RID: 3359 RVA: 0x0003C144 File Offset: 0x0003A344
	public string getIdleAnimation()
	{
		SKALDProjectData.ItemDataContainers.WeaponData rawData = this.getRawData();
		string text = "";
		if (rawData != null)
		{
			text = rawData.idleAnimation;
		}
		if (text != "")
		{
			return text;
		}
		if (rawData.parent != null && rawData.parent != "")
		{
			SKALDProjectData.ItemDataContainers.WeaponData weaponData = GameData.getItemRawData(rawData.parent) as SKALDProjectData.ItemDataContainers.WeaponData;
			if (weaponData != null)
			{
				text = weaponData.idleAnimation;
			}
		}
		return text;
	}

	// Token: 0x06000D20 RID: 3360 RVA: 0x0003C1AD File Offset: 0x0003A3AD
	public virtual int getRange()
	{
		return 1;
	}

	// Token: 0x06000D21 RID: 3361 RVA: 0x0003C1B0 File Offset: 0x0003A3B0
	public string getAttackFinishAnimation()
	{
		string attackAnimation = this.getAttackAnimation();
		if (attackAnimation != "")
		{
			return attackAnimation + "Finish";
		}
		return "";
	}

	// Token: 0x06000D22 RID: 3362 RVA: 0x0003C1E4 File Offset: 0x0003A3E4
	public string getAimingAnimation()
	{
		string text = "";
		SKALDProjectData.ItemDataContainers.WeaponData rawData = this.getRawData();
		if (rawData != null)
		{
			text = rawData.aimAnimation;
		}
		if (text != "")
		{
			return text;
		}
		if (rawData.parent != null && rawData.parent != "")
		{
			SKALDProjectData.ItemDataContainers.WeaponData weaponData = GameData.getItemRawData(rawData.parent) as SKALDProjectData.ItemDataContainers.WeaponData;
			if (weaponData != null)
			{
				text = weaponData.aimAnimation;
			}
		}
		if (text == "")
		{
			text = "ANI_BaseAiming";
		}
		return text;
	}

	// Token: 0x06000D23 RID: 3363 RVA: 0x0003C260 File Offset: 0x0003A460
	public virtual bool isRanged()
	{
		return false;
	}

	// Token: 0x06000D24 RID: 3364 RVA: 0x0003C263 File Offset: 0x0003A463
	protected override string getTypeString()
	{
		return this.getWeightCategory() + " " + this.getWeaponType();
	}

	// Token: 0x06000D25 RID: 3365 RVA: 0x0003C27C File Offset: 0x0003A47C
	public override string printComparativeStats(Character compareCharacter)
	{
		string str = this.printStatsHeader();
		ItemWeapon itemWeapon = null;
		if (compareCharacter != null)
		{
			if (this.isRanged())
			{
				itemWeapon = compareCharacter.getCurrentRangedWeapon();
			}
			else
			{
				itemWeapon = compareCharacter.getCurrentMeleeWeapon();
			}
		}
		if (itemWeapon == this)
		{
			itemWeapon = null;
		}
		string value;
		if (itemWeapon == null)
		{
			value = TextTools.formatePlusMinus(base.getHitBonus()) + "\n";
		}
		else
		{
			value = base.makeComparativeColorTagPlusMinus((float)base.getHitBonus(), (float)itemWeapon.getHitBonus(), "", "") + "\n";
		}
		str += TextTools.formateNameValuePair("Accuracy", value);
		string value2;
		if (itemWeapon == null)
		{
			value2 = this.printDamageString();
		}
		else
		{
			float num = (float)((base.getMaxDamage() + base.getMinDamage()) / 2);
			float num2 = (float)((itemWeapon.getMaxDamage() + itemWeapon.getMinDamage()) / 2);
			if (num == num2)
			{
				value2 = this.printDamageString();
			}
			else if (num > num2)
			{
				value2 = string.Concat(new string[]
				{
					C64Color.GREEN_LIGHT_TAG,
					this.printDamageString(),
					"</color> (Vs ",
					itemWeapon.printDamageString(),
					")"
				});
			}
			else
			{
				value2 = string.Concat(new string[]
				{
					C64Color.RED_LIGHT_TAG,
					this.printDamageString(),
					"</color> (Vs ",
					itemWeapon.printDamageString(),
					")"
				});
			}
		}
		str = str + TextTools.formateNameValuePair("Damage", value2) + "\n";
		string value3;
		if (itemWeapon == null)
		{
			value3 = base.getCritMultiplier().ToString("0.0") + "\n";
		}
		else
		{
			value3 = base.makeComparativeColorTag(base.getCritMultiplier(), itemWeapon.getCritMultiplier(), "x", "") + "\n";
		}
		str += TextTools.formateNameValuePair("Crit.", value3);
		return str + this.printStatsTail();
	}

	// Token: 0x06000D26 RID: 3366 RVA: 0x0003C457 File Offset: 0x0003A657
	protected override string getModelBasePath()
	{
		return "Images/Models/Weapons/";
	}
}
