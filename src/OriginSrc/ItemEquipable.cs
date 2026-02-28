using System;
using System.Runtime.Serialization;

// Token: 0x020000C5 RID: 197
[Serializable]
public abstract class ItemEquipable : ItemUseable, ISerializable
{
	// Token: 0x06000C83 RID: 3203 RVA: 0x0003A8A9 File Offset: 0x00038AA9
	protected ItemEquipable(SKALDProjectData.ItemDataContainers.ItemData rawData) : base(rawData)
	{
	}

	// Token: 0x06000C84 RID: 3204 RVA: 0x0003A8B2 File Offset: 0x00038AB2
	protected ItemEquipable()
	{
	}

	// Token: 0x06000C85 RID: 3205 RVA: 0x0003A8BA File Offset: 0x00038ABA
	public override bool isEquipable()
	{
		return true;
	}

	// Token: 0x06000C86 RID: 3206 RVA: 0x0003A8BD File Offset: 0x00038ABD
	public override bool isStackable()
	{
		return false;
	}

	// Token: 0x06000C87 RID: 3207 RVA: 0x0003A8C0 File Offset: 0x00038AC0
	public virtual string getWeightCategory()
	{
		return "";
	}

	// Token: 0x06000C88 RID: 3208 RVA: 0x0003A8C7 File Offset: 0x00038AC7
	public bool isLight()
	{
		return this.getWeightCategory() == "Light";
	}

	// Token: 0x06000C89 RID: 3209 RVA: 0x0003A8D9 File Offset: 0x00038AD9
	public bool isHeavy()
	{
		return this.getWeightCategory() == "Heavy";
	}

	// Token: 0x06000C8A RID: 3210 RVA: 0x0003A8EC File Offset: 0x00038AEC
	public virtual string getPrimaryColor()
	{
		SKALDProjectData.ItemDataContainers.ItemData rawData = this.getRawData();
		if (rawData == null)
		{
			return "";
		}
		SKALDProjectData.EnchantmentContainers.Enchantment enchantment = base.getEnchantment();
		if (enchantment != null && enchantment.primaryColor != null)
		{
			return enchantment.primaryColor;
		}
		return rawData.primaryColor;
	}

	// Token: 0x06000C8B RID: 3211 RVA: 0x0003A928 File Offset: 0x00038B28
	public virtual string getSecondaryColor()
	{
		SKALDProjectData.ItemDataContainers.ItemData rawData = this.getRawData();
		if (rawData == null)
		{
			return "";
		}
		SKALDProjectData.EnchantmentContainers.Enchantment enchantment = base.getEnchantment();
		if (enchantment != null && enchantment.secondaryColor != null)
		{
			return enchantment.secondaryColor;
		}
		return rawData.secondaryColor;
	}

	// Token: 0x06000C8C RID: 3212 RVA: 0x0003A964 File Offset: 0x00038B64
	public bool isMedium()
	{
		return this.getWeightCategory() == "Medium";
	}

	// Token: 0x06000C8D RID: 3213 RVA: 0x0003A976 File Offset: 0x00038B76
	public override string getUseVerb(Character character)
	{
		if (base.testUser(character))
		{
			return "Unequip";
		}
		return "Equip";
	}
}
