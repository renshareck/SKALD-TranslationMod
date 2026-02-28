using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Token: 0x02000176 RID: 374
public static class MagicItemMaker
{
	// Token: 0x06001419 RID: 5145 RVA: 0x00059144 File Offset: 0x00057344
	public static void createMagicItems(List<string> enchantmentId, SKALDProjectData project)
	{
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		int num = 0;
		List<SKALDProjectData.ItemDataContainers.MeleeWeaponContainer.MeleeWeaponData> list = new List<SKALDProjectData.ItemDataContainers.MeleeWeaponContainer.MeleeWeaponData>();
		List<SKALDProjectData.ItemDataContainers.RangedWeaponContainer.RangedWeaponData> list2 = new List<SKALDProjectData.ItemDataContainers.RangedWeaponContainer.RangedWeaponData>();
		List<SKALDProjectData.ItemDataContainers.ShieldContainer.Shield> list3 = new List<SKALDProjectData.ItemDataContainers.ShieldContainer.Shield>();
		List<SKALDProjectData.ItemDataContainers.ArmorContainer.Armor> list4 = new List<SKALDProjectData.ItemDataContainers.ArmorContainer.Armor>();
		List<SKALDProjectData.ItemDataContainers.AccessoryContainer.Accessory> list5 = new List<SKALDProjectData.ItemDataContainers.AccessoryContainer.Accessory>();
		List<SKALDProjectData.ItemDataContainers.JewelryContainer.Jewelry> list6 = new List<SKALDProjectData.ItemDataContainers.JewelryContainer.Jewelry>();
		foreach (string id in enchantmentId)
		{
			SKALDProjectData.EnchantmentContainers.Enchantment enchantmentRawData = GameData.getEnchantmentRawData(id);
			if (enchantmentRawData != null)
			{
				foreach (string id2 in enchantmentRawData.applicableItems)
				{
					SKALDProjectData.ItemDataContainers.ItemData itemRawData = GameData.getItemRawData(id2);
					if (itemRawData != null)
					{
						num++;
						if (itemRawData is SKALDProjectData.ItemDataContainers.MeleeWeaponContainer.MeleeWeaponData)
						{
							MagicItemMaker.copyItemRawData<SKALDProjectData.ItemDataContainers.MeleeWeaponContainer.MeleeWeaponData>(list, itemRawData, enchantmentRawData);
						}
						else if (itemRawData is SKALDProjectData.ItemDataContainers.RangedWeaponContainer.RangedWeaponData)
						{
							MagicItemMaker.copyItemRawData<SKALDProjectData.ItemDataContainers.RangedWeaponContainer.RangedWeaponData>(list2, itemRawData, enchantmentRawData);
						}
						else if (itemRawData is SKALDProjectData.ItemDataContainers.ShieldContainer.Shield)
						{
							MagicItemMaker.copyItemRawData<SKALDProjectData.ItemDataContainers.ShieldContainer.Shield>(list3, itemRawData, enchantmentRawData);
						}
						else if (itemRawData is SKALDProjectData.ItemDataContainers.ArmorContainer.Armor)
						{
							MagicItemMaker.copyItemRawData<SKALDProjectData.ItemDataContainers.ArmorContainer.Armor>(list4, itemRawData, enchantmentRawData);
						}
						else if (itemRawData is SKALDProjectData.ItemDataContainers.AccessoryContainer.Accessory)
						{
							MagicItemMaker.copyItemRawData<SKALDProjectData.ItemDataContainers.AccessoryContainer.Accessory>(list5, itemRawData, enchantmentRawData);
						}
						else if (itemRawData is SKALDProjectData.ItemDataContainers.JewelryContainer.Jewelry)
						{
							MagicItemMaker.copyItemRawData<SKALDProjectData.ItemDataContainers.JewelryContainer.Jewelry>(list6, itemRawData, enchantmentRawData);
						}
						else
						{
							MainControl.logError("Trying to enchant illegal item " + itemRawData.id + " with " + enchantmentRawData.id);
						}
					}
				}
			}
		}
		foreach (SKALDProjectData.ItemDataContainers.MeleeWeaponContainer.MeleeWeaponData item in list)
		{
			project.itemContainer.meleeWeapons.list.Add(item);
		}
		foreach (SKALDProjectData.ItemDataContainers.RangedWeaponContainer.RangedWeaponData item2 in list2)
		{
			project.itemContainer.rangedWeapons.list.Add(item2);
		}
		foreach (SKALDProjectData.ItemDataContainers.ShieldContainer.Shield item3 in list3)
		{
			project.itemContainer.shields.list.Add(item3);
		}
		foreach (SKALDProjectData.ItemDataContainers.ArmorContainer.Armor item4 in list4)
		{
			project.itemContainer.armor.list.Add(item4);
		}
		foreach (SKALDProjectData.ItemDataContainers.AccessoryContainer.Accessory item5 in list5)
		{
			project.itemContainer.accessories.list.Add(item5);
		}
		foreach (SKALDProjectData.ItemDataContainers.JewelryContainer.Jewelry item6 in list6)
		{
			project.itemContainer.jewelry.list.Add(item6);
		}
		stopwatch.Stop();
		MainControl.log(string.Concat(new string[]
		{
			"Added ",
			num.ToString(),
			" new magical items in ",
			stopwatch.Elapsed.ToString(),
			" seconds."
		}));
	}

	// Token: 0x0600141A RID: 5146 RVA: 0x00059560 File Offset: 0x00057760
	private static void copyItemRawData<T>(List<T> targetList, SKALDProjectData.ItemDataContainers.ItemData baseItemData, SKALDProjectData.EnchantmentContainers.Enchantment enchantment) where T : SKALDProjectData.ItemDataContainers.ItemData
	{
		T t = JsonUtility.FromJson<T>(MagicItemMaker.serializeObject(baseItemData));
		T t2 = t;
		t2.id += enchantment.id.Remove(0, 4);
		t.enchantment = enchantment.id;
		targetList.Add(t);
	}

	// Token: 0x0600141B RID: 5147 RVA: 0x000595B4 File Offset: 0x000577B4
	private static string serializeObject(SKALDProjectData.ItemDataContainers.ItemData baseItemData)
	{
		if (MagicItemMaker.serializationCache.ContainsKey(baseItemData.id))
		{
			return MagicItemMaker.serializationCache[baseItemData.id];
		}
		string text = JsonUtility.ToJson(baseItemData);
		MagicItemMaker.serializationCache.Add(baseItemData.id, text);
		return text;
	}

	// Token: 0x0400051B RID: 1307
	private static Dictionary<string, string> serializationCache = new Dictionary<string, string>();
}
