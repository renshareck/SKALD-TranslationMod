using System;
using System.Runtime.Serialization;

// Token: 0x020000C0 RID: 192
[Serializable]
public abstract class ItemArmorBase : ItemEquipable, ISerializable
{
	// Token: 0x06000C4C RID: 3148 RVA: 0x00039B74 File Offset: 0x00037D74
	protected ItemArmorBase(SKALDProjectData.ItemDataContainers.ClothingWearableData rawData) : base(rawData)
	{
	}

	// Token: 0x06000C4D RID: 3149 RVA: 0x00039B7D File Offset: 0x00037D7D
	protected ItemArmorBase()
	{
	}

	// Token: 0x06000C4E RID: 3150 RVA: 0x00039B88 File Offset: 0x00037D88
	public new SKALDProjectData.ItemDataContainers.ClothingWearableData getRawData()
	{
		SKALDProjectData.ItemDataContainers.ItemData rawData = base.getRawData();
		if (rawData is SKALDProjectData.ItemDataContainers.ClothingWearableData)
		{
			return rawData as SKALDProjectData.ItemDataContainers.ClothingWearableData;
		}
		return null;
	}

	// Token: 0x06000C4F RID: 3151 RVA: 0x00039BAC File Offset: 0x00037DAC
	public override string getWeightCategory()
	{
		SKALDProjectData.ItemDataContainers.ClothingWearableData rawData = this.getRawData();
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
			SKALDProjectData.ItemDataContainers.ClothingWearableData clothingWearableData = GameData.getItemRawData(rawData.parent) as SKALDProjectData.ItemDataContainers.ClothingWearableData;
			if (clothingWearableData != null)
			{
				return clothingWearableData.weightCategory;
			}
		}
		return "";
	}

	// Token: 0x06000C50 RID: 3152 RVA: 0x00039C1C File Offset: 0x00037E1C
	protected override string getUseSound()
	{
		if (base.isLight())
		{
			return "ItemArmorLight1";
		}
		return "ItemArmorMedium1";
	}

	// Token: 0x06000C51 RID: 3153 RVA: 0x00039C34 File Offset: 0x00037E34
	public int getEncumbrance()
	{
		SKALDProjectData.ItemDataContainers.ClothingWearableData rawData = this.getRawData();
		if (rawData == null)
		{
			return 0;
		}
		int num = rawData.encumberance;
		if (rawData.parent != null && rawData.parent != "")
		{
			SKALDProjectData.ItemDataContainers.ClothingWearableData clothingWearableData = GameData.getItemRawData(rawData.parent) as SKALDProjectData.ItemDataContainers.ClothingWearableData;
			if (clothingWearableData != null)
			{
				num += clothingWearableData.encumberance;
			}
		}
		SKALDProjectData.EnchantmentContainers.Enchantment enchantment = base.getEnchantment();
		if (enchantment != null && enchantment.conferredAbilities != null)
		{
			num += enchantment.encumberance;
		}
		if (num < 0)
		{
			num = 0;
		}
		return num;
	}

	// Token: 0x06000C52 RID: 3154 RVA: 0x00039CB0 File Offset: 0x00037EB0
	public int getSoak()
	{
		SKALDProjectData.ItemDataContainers.ClothingWearableData rawData = this.getRawData();
		if (rawData == null)
		{
			return 0;
		}
		int num = rawData.soak;
		if (rawData.parent != null && rawData.parent != "")
		{
			SKALDProjectData.ItemDataContainers.ClothingWearableData clothingWearableData = GameData.getItemRawData(rawData.parent) as SKALDProjectData.ItemDataContainers.ClothingWearableData;
			if (clothingWearableData != null)
			{
				num += clothingWearableData.soak;
			}
		}
		SKALDProjectData.EnchantmentContainers.Enchantment enchantment = base.getEnchantment();
		if (enchantment != null && enchantment.conferredAbilities != null)
		{
			num += enchantment.soak;
		}
		if (num < 0)
		{
			num = 0;
		}
		return num;
	}

	// Token: 0x06000C53 RID: 3155 RVA: 0x00039D2C File Offset: 0x00037F2C
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
			return C64Color.ColorIds.COL_Brown.ToString();
		}
		if (powerLevel == 2)
		{
			return C64Color.ColorIds.COL_BrownLight.ToString();
		}
		if (powerLevel == 3)
		{
			return C64Color.ColorIds.COL_Red.ToString();
		}
		return C64Color.ColorIds.COL_White.ToString();
	}

	// Token: 0x06000C54 RID: 3156 RVA: 0x00039DA4 File Offset: 0x00037FA4
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
			return C64Color.ColorIds.COL_Brown.ToString();
		}
		if (powerLevel == 2)
		{
			return C64Color.ColorIds.COL_Gray.ToString();
		}
		if (powerLevel == 3)
		{
			return C64Color.ColorIds.COL_Yellow.ToString();
		}
		return C64Color.ColorIds.COL_Cyan.ToString();
	}

	// Token: 0x06000C55 RID: 3157 RVA: 0x00039E1C File Offset: 0x0003801C
	protected virtual string getArmorComparativeStat(ItemArmorBase compareItem)
	{
		string str = this.printStatsHeader();
		string value;
		if (compareItem == null)
		{
			value = this.getSoak().ToString() + "\n";
		}
		else
		{
			value = base.makeComparativeColorTag((float)this.getSoak(), (float)compareItem.getSoak()) + "\n";
		}
		string str2 = str + TextTools.formateNameValuePair("Soak", value);
		string value2;
		if (compareItem == null)
		{
			value2 = this.getEncumbrance().ToString() + "\n";
		}
		else
		{
			value2 = base.makeComparativeColorTagReversed((float)this.getEncumbrance(), (float)compareItem.getEncumbrance(), "-", "") + "\n";
		}
		return str2 + TextTools.formateNameValuePair("Enc.", value2) + this.printStatsTail();
	}
}
