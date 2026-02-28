using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

// Token: 0x020000C4 RID: 196
[Serializable]
public abstract class ItemDamaging : ItemEquipable, ISerializable
{
	// Token: 0x06000C75 RID: 3189 RVA: 0x0003A356 File Offset: 0x00038556
	protected ItemDamaging(SKALDProjectData.ItemDataContainers.DamagingItem rawData) : base(rawData)
	{
	}

	// Token: 0x06000C76 RID: 3190 RVA: 0x0003A35F File Offset: 0x0003855F
	protected ItemDamaging()
	{
	}

	// Token: 0x06000C77 RID: 3191 RVA: 0x0003A367 File Offset: 0x00038567
	private new SKALDProjectData.ItemDataContainers.DamagingItem getRawData()
	{
		return GameData.getItemRawData(this.getId()) as SKALDProjectData.ItemDataContainers.DamagingItem;
	}

	// Token: 0x06000C78 RID: 3192 RVA: 0x0003A37C File Offset: 0x0003857C
	public List<string> getDamageTypes()
	{
		SKALDProjectData.ItemDataContainers.DamagingItem rawData = this.getRawData();
		if (rawData == null)
		{
			return null;
		}
		List<string> list = new List<string>();
		foreach (string item in rawData.damageType)
		{
			list.Add(item);
		}
		SKALDProjectData.ItemDataContainers.DamagingItem damagingItem = GameData.getItemRawData(rawData.parent) as SKALDProjectData.ItemDataContainers.DamagingItem;
		if (damagingItem != null)
		{
			foreach (string item2 in damagingItem.damageType)
			{
				if (!list.Contains(item2))
				{
					list.Add(item2);
				}
			}
		}
		SKALDProjectData.EnchantmentContainers.Enchantment enchantment = base.getEnchantment();
		if (enchantment != null)
		{
			foreach (string item3 in enchantment.damageType)
			{
				if (!list.Contains(item3))
				{
					list.Add(item3);
				}
			}
		}
		if (base.isMagical())
		{
			list.Add("Magical");
		}
		return list;
	}

	// Token: 0x06000C79 RID: 3193 RVA: 0x0003A4B4 File Offset: 0x000386B4
	protected virtual string printDamageString()
	{
		int minDamage = this.getMinDamage();
		int maxDamage = this.getMaxDamage();
		if (minDamage == maxDamage)
		{
			return minDamage.ToString();
		}
		return minDamage.ToString() + "-" + maxDamage.ToString();
	}

	// Token: 0x06000C7A RID: 3194 RVA: 0x0003A4F4 File Offset: 0x000386F4
	protected string printDamageTypeString()
	{
		string text = "";
		foreach (string str in this.getDamageTypes())
		{
			text = text + str + ", ";
		}
		text = TextTools.removeTrailingComma(text);
		return text;
	}

	// Token: 0x06000C7B RID: 3195 RVA: 0x0003A55C File Offset: 0x0003875C
	public void fireHitTriggers(Character user, Character target)
	{
		SKALDProjectData.ItemDataContainers.DamagingItem rawData = this.getRawData();
		if (rawData == null)
		{
			return;
		}
		base.processString(rawData.hitTrigger, user);
		foreach (string id in rawData.hitEffect)
		{
			Effect effect = GameData.getEffect(id);
			if (effect != null)
			{
				effect.fireEffect(user, target);
			}
		}
		SKALDProjectData.ItemDataContainers.DamagingItem damagingItem = GameData.getItemRawData(rawData.parent) as SKALDProjectData.ItemDataContainers.DamagingItem;
		if (damagingItem != null)
		{
			base.processString(damagingItem.hitTrigger, user);
			foreach (string id2 in damagingItem.hitEffect)
			{
				Effect effect2 = GameData.getEffect(id2);
				if (effect2 != null)
				{
					effect2.fireEffect(user, target);
				}
			}
		}
		SKALDProjectData.EnchantmentContainers.Enchantment enchantment = base.getEnchantment();
		if (enchantment != null)
		{
			base.processString(enchantment.hitTrigger, user);
			foreach (string id3 in enchantment.hitEffect)
			{
				Effect effect3 = GameData.getEffect(id3);
				if (effect3 != null)
				{
					effect3.fireEffect(user, target);
				}
			}
		}
	}

	// Token: 0x06000C7C RID: 3196 RVA: 0x0003A6AC File Offset: 0x000388AC
	public bool isSlashing()
	{
		return this.getDamageTypes().Contains("Slashing");
	}

	// Token: 0x06000C7D RID: 3197 RVA: 0x0003A6BE File Offset: 0x000388BE
	public bool isBlunt()
	{
		return this.getDamageTypes().Contains("Blunt");
	}

	// Token: 0x06000C7E RID: 3198 RVA: 0x0003A6D0 File Offset: 0x000388D0
	public bool isPiercing()
	{
		return this.getDamageTypes().Contains("Piercing");
	}

	// Token: 0x06000C7F RID: 3199 RVA: 0x0003A6E4 File Offset: 0x000388E4
	public float getCritMultiplier()
	{
		float num = 0f;
		SKALDProjectData.ItemDataContainers.DamagingItem rawData = this.getRawData();
		try
		{
			num = rawData.crit;
		}
		catch (FormatException ex)
		{
			MainControl.log(ex.Message);
			num = 2f;
		}
		SKALDProjectData.ItemDataContainers.DamagingItem damagingItem = GameData.getItemRawData(rawData.parent) as SKALDProjectData.ItemDataContainers.DamagingItem;
		if (damagingItem != null)
		{
			num += damagingItem.crit;
		}
		SKALDProjectData.EnchantmentContainers.Enchantment enchantment = base.getEnchantment();
		if (enchantment != null)
		{
			num += enchantment.crit;
		}
		return num;
	}

	// Token: 0x06000C80 RID: 3200 RVA: 0x0003A75C File Offset: 0x0003895C
	public int getMinDamage()
	{
		SKALDProjectData.ItemDataContainers.DamagingItem rawData = this.getRawData();
		if (rawData == null)
		{
			return 0;
		}
		int num = rawData.minDamage;
		if (rawData.parent == null || rawData.parent == "")
		{
			return num;
		}
		SKALDProjectData.ItemDataContainers.DamagingItem damagingItem = GameData.getItemRawData(rawData.parent) as SKALDProjectData.ItemDataContainers.DamagingItem;
		if (damagingItem != null)
		{
			num += damagingItem.minDamage;
		}
		SKALDProjectData.EnchantmentContainers.Enchantment enchantment = base.getEnchantment();
		if (enchantment != null)
		{
			num += enchantment.minDamage;
		}
		return num;
	}

	// Token: 0x06000C81 RID: 3201 RVA: 0x0003A7CC File Offset: 0x000389CC
	public int getHitBonus()
	{
		SKALDProjectData.ItemDataContainers.DamagingItem rawData = this.getRawData();
		if (rawData == null)
		{
			return 0;
		}
		int num = rawData.hitBonus;
		if (rawData.parent == null || rawData.parent == "")
		{
			return num;
		}
		SKALDProjectData.ItemDataContainers.DamagingItem damagingItem = GameData.getItemRawData(rawData.parent) as SKALDProjectData.ItemDataContainers.DamagingItem;
		if (damagingItem != null)
		{
			num += damagingItem.hitBonus;
		}
		SKALDProjectData.EnchantmentContainers.Enchantment enchantment = base.getEnchantment();
		if (enchantment != null)
		{
			num += enchantment.hitBonus;
		}
		return num;
	}

	// Token: 0x06000C82 RID: 3202 RVA: 0x0003A83C File Offset: 0x00038A3C
	public int getMaxDamage()
	{
		SKALDProjectData.ItemDataContainers.DamagingItem rawData = this.getRawData();
		if (rawData == null)
		{
			return 0;
		}
		int num = rawData.maxDamage;
		if (rawData.parent == null || rawData.parent == "")
		{
			return num;
		}
		SKALDProjectData.ItemDataContainers.DamagingItem damagingItem = GameData.getItemRawData(rawData.parent) as SKALDProjectData.ItemDataContainers.DamagingItem;
		if (damagingItem != null)
		{
			num += damagingItem.maxDamage;
		}
		SKALDProjectData.EnchantmentContainers.Enchantment enchantment = base.getEnchantment();
		if (enchantment != null)
		{
			num += enchantment.maxDamage;
		}
		return num;
	}
}
