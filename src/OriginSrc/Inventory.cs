using System;
using System.Collections.Generic;

// Token: 0x020000BA RID: 186
[Serializable]
public class Inventory : SkaldWorldObjectList
{
	// Token: 0x06000B98 RID: 2968 RVA: 0x00036E8B File Offset: 0x0003508B
	public Inventory()
	{
		this.setName("Inventory");
	}

	// Token: 0x06000B99 RID: 2969 RVA: 0x00036E9F File Offset: 0x0003509F
	public new Item getCurrentObject()
	{
		if (base.isEmpty())
		{
			this.currentObject = null;
		}
		if (base.getCurrentObject() == null)
		{
			return null;
		}
		return base.getCurrentObject() as Item;
	}

	// Token: 0x06000B9A RID: 2970 RVA: 0x00036EC8 File Offset: 0x000350C8
	public void autoEquip(Character user)
	{
		this.autoEquipArmor(user);
		this.autoEquipMeleeWeapon(user);
		this.autoEquipRangedWeapon(user);
		this.autoEquipClothing(user);
		this.autoEquipHeadware(user);
		this.autoEquipLight(user);
		this.autoEquipShield(user);
		this.autoEquipAmmo(user);
		this.autoEquipRing(user);
		this.autoEquipNecklace(user);
		this.autoEquipIdleItem(user);
	}

	// Token: 0x06000B9B RID: 2971 RVA: 0x00036F22 File Offset: 0x00035122
	public void activateNewAdditionTagging()
	{
		this.tagNewAdditions = true;
	}

	// Token: 0x06000B9C RID: 2972 RVA: 0x00036F2B File Offset: 0x0003512B
	private void autoEquipIdleItem(Character user)
	{
		this.autoEquipItem(Item.ItemTypes.Idle, user);
	}

	// Token: 0x06000B9D RID: 2973 RVA: 0x00036F36 File Offset: 0x00035136
	private void autoEquipMeleeWeapon(Character user)
	{
		this.autoEquipItem(Item.ItemTypes.MeleeWeapon, user);
	}

	// Token: 0x06000B9E RID: 2974 RVA: 0x00036F40 File Offset: 0x00035140
	private void autoEquipAmmo(Character user)
	{
		this.autoEquipItem(Item.ItemTypes.Ammo, user);
	}

	// Token: 0x06000B9F RID: 2975 RVA: 0x00036F4A File Offset: 0x0003514A
	private void autoEquipRing(Character user)
	{
		this.autoEquipItem(Item.ItemTypes.Ring, user);
	}

	// Token: 0x06000BA0 RID: 2976 RVA: 0x00036F55 File Offset: 0x00035155
	private void autoEquipNecklace(Character user)
	{
		this.autoEquipItem(Item.ItemTypes.Necklace, user);
	}

	// Token: 0x06000BA1 RID: 2977 RVA: 0x00036F60 File Offset: 0x00035160
	private void autoEquipRangedWeapon(Character user)
	{
		this.autoEquipItem(Item.ItemTypes.RangedWeapon, user);
	}

	// Token: 0x06000BA2 RID: 2978 RVA: 0x00036F6A File Offset: 0x0003516A
	private void autoEquipClothing(Character user)
	{
		this.autoEquipItem(Item.ItemTypes.Clothing, user);
	}

	// Token: 0x06000BA3 RID: 2979 RVA: 0x00036F74 File Offset: 0x00035174
	private void autoEquipArmor(Character user)
	{
		this.autoEquipItem(Item.ItemTypes.Armor, user);
	}

	// Token: 0x06000BA4 RID: 2980 RVA: 0x00036F7E File Offset: 0x0003517E
	private void autoEquipHeadware(Character user)
	{
		this.autoEquipItem(Item.ItemTypes.Headwear, user);
	}

	// Token: 0x06000BA5 RID: 2981 RVA: 0x00036F88 File Offset: 0x00035188
	private void autoEquipLight(Character user)
	{
		this.autoEquipItem(Item.ItemTypes.Light, user);
	}

	// Token: 0x06000BA6 RID: 2982 RVA: 0x00036F93 File Offset: 0x00035193
	public void unequipLight(Character user)
	{
		this.unequipItem(Item.ItemTypes.Light, user);
	}

	// Token: 0x06000BA7 RID: 2983 RVA: 0x00036FA0 File Offset: 0x000351A0
	public void toggleLight(Character user)
	{
		bool flag = user.getCurrentLight() != null;
		if (this.getEquippedItem(Item.ItemTypes.Light, user) != null)
		{
			this.unequipLight(user);
		}
		else
		{
			this.autoEquipLight(user);
		}
		bool flag2 = user.getCurrentLight() != null;
		if (flag && !flag2)
		{
			AudioControl.playSound("FireOff1");
			return;
		}
		if (!flag && flag2)
		{
			AudioControl.playSound("FireOn2");
			return;
		}
		if (!flag && !flag2 && user.isPC())
		{
			PopUpControl.addPopUpOK("The party does not have a lightsource to equip. Find a lantern!");
		}
	}

	// Token: 0x06000BA8 RID: 2984 RVA: 0x00037018 File Offset: 0x00035218
	public void autoEquipShield(Character user)
	{
		this.autoEquipItem(Item.ItemTypes.Shield, user);
	}

	// Token: 0x06000BA9 RID: 2985 RVA: 0x00037024 File Offset: 0x00035224
	protected override bool doesObjectSortBeforeAnotherItem(SkaldBaseObject obj1, SkaldBaseObject obj2)
	{
		Item item = obj1 as Item;
		Item item2 = obj2 as Item;
		if (item.isNewAddition() && !item2.isNewAddition())
		{
			return true;
		}
		if (!item.isNewAddition() && item2.isNewAddition())
		{
			return false;
		}
		int typeSortOrder = item.getTypeSortOrder();
		int typeSortOrder2 = item2.getTypeSortOrder();
		return typeSortOrder < typeSortOrder2 || (typeSortOrder <= typeSortOrder2 && base.doesObjectSortBeforeAnotherItem(item, item2));
	}

	// Token: 0x06000BAA RID: 2986 RVA: 0x00037088 File Offset: 0x00035288
	private void autoEquipItem(Item.ItemTypes type, Character user)
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			Item item = (Item)skaldBaseObject;
			if (item.isType(type) && !item.hasUser())
			{
				this.currentObject = item;
				this.equip(user);
				break;
			}
		}
	}

	// Token: 0x06000BAB RID: 2987 RVA: 0x000370FC File Offset: 0x000352FC
	public SkaldActionResult equip(Character user)
	{
		Item currentObject = this.getCurrentObject();
		SkaldActionResult skaldActionResult = user.isItemLegalToEquip(currentObject, true);
		if (!skaldActionResult.wasSuccess())
		{
			return skaldActionResult;
		}
		this.clearAllCompetingItems(user);
		currentObject.setUser(user);
		return new SkaldActionResult(true, true, "Equipped " + currentObject.getName(), true);
	}

	// Token: 0x06000BAC RID: 2988 RVA: 0x0003714C File Offset: 0x0003534C
	public void clearAllCompetingItems(Character user)
	{
		if (base.isEmpty())
		{
			return;
		}
		Item currentObject = this.getCurrentObject();
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			Item item = (Item)skaldBaseObject;
			if (item.testUser(user) && item.doesItemCompeteForSlot(currentObject))
			{
				item.clearUser();
			}
		}
	}

	// Token: 0x06000BAD RID: 2989 RVA: 0x000371C8 File Offset: 0x000353C8
	public int getMoney()
	{
		ItemMoney itemMoney = base.getObject("Money") as ItemMoney;
		if (itemMoney != null)
		{
			return itemMoney.getCount();
		}
		return 0;
	}

	// Token: 0x06000BAE RID: 2990 RVA: 0x000371F4 File Offset: 0x000353F4
	public int addMoney(int amount)
	{
		if (amount == 0)
		{
			return 0;
		}
		ItemMoney itemMoney = base.getObject("Money") as ItemMoney;
		if (itemMoney == null)
		{
			itemMoney = GameData.instantiateMoney();
			this.addItem(itemMoney);
		}
		if (itemMoney != null)
		{
			return itemMoney.addCount(amount);
		}
		return 0;
	}

	// Token: 0x06000BAF RID: 2991 RVA: 0x00037234 File Offset: 0x00035434
	public int getFoodCount()
	{
		int num = 0;
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			Item item = (Item)skaldBaseObject;
			if (item is ItemFood)
			{
				ItemFood itemFood = item as ItemFood;
				num += itemFood.getFoodValue() * itemFood.getCount();
			}
		}
		return num;
	}

	// Token: 0x06000BB0 RID: 2992 RVA: 0x000372A8 File Offset: 0x000354A8
	public Item getEquippedItem(Item.ItemTypes type, Character user)
	{
		if (base.isEmpty())
		{
			return null;
		}
		for (int i = 0; i < this.objectList.Count; i++)
		{
			Item item = this.objectList[i] as Item;
			if (item.isType(type) && item.testUser(user))
			{
				return item;
			}
		}
		return null;
	}

	// Token: 0x06000BB1 RID: 2993 RVA: 0x000372FC File Offset: 0x000354FC
	public Item setCurrentItemAndRemove(string id)
	{
		this.currentObject = base.getObject(id);
		if (this.getCurrentObject() != null && this.getCurrentObject().getId() == id)
		{
			return this.removeCurrentItem();
		}
		return null;
	}

	// Token: 0x06000BB2 RID: 2994 RVA: 0x00037330 File Offset: 0x00035530
	public Item removeCurrentItem()
	{
		Item currentObject = this.getCurrentObject();
		if (currentObject == null)
		{
			return null;
		}
		if (currentObject.getCount() > 1)
		{
			currentObject.addCount(-1);
			return this.copyItem();
		}
		base.setNextObject(1);
		this.objectList.Remove(currentObject);
		currentObject.clearUser();
		return currentObject;
	}

	// Token: 0x06000BB3 RID: 2995 RVA: 0x0003737C File Offset: 0x0003557C
	public Item removeCurrentItemStack()
	{
		Item currentObject = this.getCurrentObject();
		if (currentObject == null)
		{
			return null;
		}
		this.objectList.Remove(currentObject);
		currentObject.clearUser();
		base.setNextObject(1);
		return currentObject;
	}

	// Token: 0x06000BB4 RID: 2996 RVA: 0x000373B0 File Offset: 0x000355B0
	public string deleteItem(string id)
	{
		if (base.isEmpty())
		{
			return "";
		}
		this.currentObject = base.getObject(id);
		if (this.currentObject.getId() == id)
		{
			return this.deleteCurrentItem();
		}
		return "";
	}

	// Token: 0x06000BB5 RID: 2997 RVA: 0x000373EC File Offset: 0x000355EC
	internal List<UIButtonControlBase.ButtonData> getConsumablesButtonDataList()
	{
		List<UIButtonControlBase.ButtonData> list = new List<UIButtonControlBase.ButtonData>();
		foreach (ItemConsumable itemConsumable in this.getConsumableItemList())
		{
			list.Add(itemConsumable.getButtonData());
		}
		return list;
	}

	// Token: 0x06000BB6 RID: 2998 RVA: 0x0003744C File Offset: 0x0003564C
	internal List<ItemConsumable> getConsumableItemList()
	{
		List<ItemConsumable> list = new List<ItemConsumable>();
		foreach (SkaldBaseObject skaldBaseObject in base.getObjectList())
		{
			Item item = (Item)skaldBaseObject;
			if (item is ItemConsumable)
			{
				list.Add(item as ItemConsumable);
			}
		}
		return list;
	}

	// Token: 0x06000BB7 RID: 2999 RVA: 0x000374B8 File Offset: 0x000356B8
	internal bool setCurrentConsumableItemByIndex(int index)
	{
		if (index < 0)
		{
			return false;
		}
		List<ItemConsumable> consumableItemList = this.getConsumableItemList();
		if (index < consumableItemList.Count)
		{
			base.setCurrentObject(consumableItemList[index]);
			if (this.getCurrentObject() != null)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000BB8 RID: 3000 RVA: 0x000374F4 File Offset: 0x000356F4
	public string deleteCurrentItem()
	{
		if (base.isEmpty())
		{
			return "";
		}
		if (this.currentObject != null)
		{
			Item item = this.currentObject as Item;
			if (item != null)
			{
				string result = "Deleted " + this.removeCurrentItem().getName();
				if (!item.isStackable() || item.getCount() == 0)
				{
					item.setToBeRemoved();
				}
				return result;
			}
		}
		return "";
	}

	// Token: 0x06000BB9 RID: 3001 RVA: 0x00037558 File Offset: 0x00035758
	public void deleteAllItems()
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((Item)skaldBaseObject).setToBeRemoved();
		}
		this.purgeList();
	}

	// Token: 0x06000BBA RID: 3002 RVA: 0x000375B4 File Offset: 0x000357B4
	public SkaldActionResult useCurrentItem(Character user)
	{
		Item currentObject = this.getCurrentObject();
		if (currentObject == null)
		{
			return new SkaldActionResult(false, false, "No item available", true);
		}
		SkaldActionResult result = currentObject.useItem(user);
		if (currentObject.isEquipable())
		{
			if (currentObject is ItemAmmo)
			{
				if (user.getPreferredAmmoId() == currentObject.getId())
				{
					user.setPreferredAmmo("");
					user.updateItemSlot();
					result = new SkaldActionResult(true, true, "Unequipped " + currentObject.getName(), true);
				}
				else
				{
					result = this.equip(user);
				}
			}
			else if (currentObject.testUser(user))
			{
				this.unequipCurrentItem();
				result = new SkaldActionResult(true, true, "Unequipped " + currentObject.getName(), true);
			}
			else
			{
				result = this.equip(user);
			}
		}
		return result;
	}

	// Token: 0x06000BBB RID: 3003 RVA: 0x00037670 File Offset: 0x00035870
	public string getPickedUpBark()
	{
		if (base.isEmpty())
		{
			return "Nothing here.";
		}
		if (base.getCount() == 1)
		{
			Item currentObject = this.getCurrentObject();
			string text = "Picked up: " + currentObject.getName();
			if (currentObject.getCount() > 1)
			{
				text = text + " (" + currentObject.getCount().ToString() + ")";
			}
			return text;
		}
		return "Picked up some items.";
	}

	// Token: 0x06000BBC RID: 3004 RVA: 0x000376DC File Offset: 0x000358DC
	public virtual string getUseVerb(Character character)
	{
		Item currentObject = this.getCurrentObject();
		if (currentObject != null)
		{
			return currentObject.getUseVerb(character);
		}
		return "...";
	}

	// Token: 0x06000BBD RID: 3005 RVA: 0x00037700 File Offset: 0x00035900
	public void unequipCurrentItem()
	{
		Item currentObject = this.getCurrentObject();
		if (currentObject == null)
		{
			return;
		}
		currentObject.clearUser();
	}

	// Token: 0x06000BBE RID: 3006 RVA: 0x0003771E File Offset: 0x0003591E
	public void unequipItem(Item.ItemTypes type, Character user)
	{
		while (this.getEquippedItem(type, user) != null)
		{
			this.getEquippedItem(type, user).clearUser();
		}
	}

	// Token: 0x06000BBF RID: 3007 RVA: 0x0003773C File Offset: 0x0003593C
	public void clearNewAdditions()
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((Item)skaldBaseObject).clearNewAddition();
		}
	}

	// Token: 0x06000BC0 RID: 3008 RVA: 0x00037794 File Offset: 0x00035994
	public string addItem(Item item)
	{
		if (item == null)
		{
			return "";
		}
		if (item.isStackable())
		{
			Item item2 = base.getObject(item.getId()) as Item;
			if (item2 != null)
			{
				item.clearTilePosition();
				item2.addCount(item.getCount());
				this.currentObject = item2;
				item.setToBeRemoved();
				return item.getName();
			}
		}
		if (this.tagNewAdditions)
		{
			item.markAsNewAddition();
		}
		else
		{
			item.clearNewAddition();
		}
		item.setTilePosition(this.getTileX(), this.getTileY(), base.getContainerMapId());
		base.add(item);
		return item.getName();
	}

	// Token: 0x06000BC1 RID: 3009 RVA: 0x00037829 File Offset: 0x00035A29
	public string getCurrentItemNameAndAmount()
	{
		if (this.getCurrentObject() == null)
		{
			return "";
		}
		return this.getCurrentObject().getNameAndAmount();
	}

	// Token: 0x06000BC2 RID: 3010 RVA: 0x00037844 File Offset: 0x00035A44
	public override string getDescription()
	{
		if (this.getCurrentObject() == null)
		{
			return "";
		}
		return "\n" + this.getCurrentObject().getDescription();
	}

	// Token: 0x06000BC3 RID: 3011 RVA: 0x00037869 File Offset: 0x00035A69
	public string printComparativeItemStats(Character c)
	{
		if (this.getCurrentObject() == null)
		{
			return "";
		}
		return "\n" + this.getCurrentObject().printComparativeStats(c);
	}

	// Token: 0x06000BC4 RID: 3012 RVA: 0x00037890 File Offset: 0x00035A90
	public string addItem(string id)
	{
		Item item = GameData.instantiateItem(id);
		return this.addItem(item);
	}

	// Token: 0x06000BC5 RID: 3013 RVA: 0x000378AC File Offset: 0x00035AAC
	public string addItem(string id, int amount)
	{
		if (amount <= 0)
		{
			return "";
		}
		string text = "";
		for (int i = 0; i < amount; i++)
		{
			Item item = GameData.instantiateItem(id);
			text = item.getName();
			this.addItem(item);
		}
		return string.Concat(new string[]
		{
			"You get some items: ",
			text,
			"(",
			amount.ToString(),
			")"
		});
	}

	// Token: 0x06000BC6 RID: 3014 RVA: 0x0003791C File Offset: 0x00035B1C
	public Item copyItem()
	{
		if (this.getCurrentObject() == null)
		{
			return null;
		}
		string id = this.getCurrentObject().getId();
		if (id == "Money")
		{
			ItemMoney itemMoney = GameData.instantiateMoney();
			itemMoney.setCount(1);
			return itemMoney;
		}
		return GameData.instantiateItem(id);
	}

	// Token: 0x06000BC7 RID: 3015 RVA: 0x00037960 File Offset: 0x00035B60
	public string addItem(string[] idList)
	{
		string text = "";
		if (idList != null && idList.Length != 0)
		{
			foreach (string id in idList)
			{
				text += this.addItem(id);
			}
		}
		return text;
	}

	// Token: 0x06000BC8 RID: 3016 RVA: 0x000379A0 File Offset: 0x00035BA0
	public void setAllItemsToStoreStack()
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((Item)skaldBaseObject).setCountToStoreStack();
		}
	}

	// Token: 0x06000BC9 RID: 3017 RVA: 0x000379F8 File Offset: 0x00035BF8
	public string printGold()
	{
		return C64Color.GRAY_LIGHT_TAG + "Gold:</color> " + this.getMoney().ToString() + " GP";
	}

	// Token: 0x06000BCA RID: 3018 RVA: 0x00037A28 File Offset: 0x00035C28
	public override string printListSimplifiedColor()
	{
		if (base.isEmpty())
		{
			return "Empty";
		}
		string text = "";
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			Item item = (Item)skaldBaseObject;
			if (item.getCount() > 0 && !(item.getId() == "Money"))
			{
				string text2 = "";
				if (item.getCount() > 1)
				{
					text2 = text2 + " (" + item.getCount().ToString() + ")";
				}
				text = string.Concat(new string[]
				{
					text,
					C64Color.YELLOW_TAG,
					" ",
					item.getName(),
					text2,
					"</color>\n"
				});
			}
		}
		if (this.getMoney() > 0)
		{
			text += this.printGold();
		}
		return text;
	}

	// Token: 0x06000BCB RID: 3019 RVA: 0x00037B2C File Offset: 0x00035D2C
	public Dictionary<string, int> createCountDictionary()
	{
		Dictionary<string, int> result = new Dictionary<string, int>();
		return this.createCountDictionary(result);
	}

	// Token: 0x06000BCC RID: 3020 RVA: 0x00037B48 File Offset: 0x00035D48
	public Dictionary<string, int> createCountDictionary(Dictionary<string, int> result)
	{
		foreach (SkaldBaseObject skaldBaseObject in base.getObjectList())
		{
			Item item = (Item)skaldBaseObject;
			if (item.getCount() > 0 && !(item.getId() == "Money"))
			{
				if (result.ContainsKey(item.getName()))
				{
					string name = item.getName();
					result[name] += item.getCount();
				}
				else
				{
					result.Add(item.getName(), item.getCount());
				}
			}
		}
		return result;
	}

	// Token: 0x06000BCD RID: 3021 RVA: 0x00037BF8 File Offset: 0x00035DF8
	public override string printCountList()
	{
		if (base.isEmpty())
		{
			return "Empty";
		}
		string text = "";
		foreach (KeyValuePair<string, int> keyValuePair in this.createCountDictionary())
		{
			string text2 = "";
			if (keyValuePair.Value > 1)
			{
				text2 = text2 + " (" + keyValuePair.Value.ToString() + ")";
			}
			text = string.Concat(new string[]
			{
				text,
				C64Color.YELLOW_TAG,
				" ",
				keyValuePair.Key,
				text2,
				"</color>\n"
			});
		}
		if (this.getMoney() > 0)
		{
			text += this.printGold();
		}
		return text;
	}

	// Token: 0x06000BCE RID: 3022 RVA: 0x00037CD8 File Offset: 0x00035ED8
	public override string printList()
	{
		string text = "";
		if (this.objectList.Count == 0)
		{
			return "Empty";
		}
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			Item item = (Item)skaldBaseObject;
			string text2 = "";
			if (item.getCount() > 1)
			{
				text2 = text2 + " (" + item.getCount().ToString() + ")";
			}
			if (item == this.getCurrentObject())
			{
				text = string.Concat(new string[]
				{
					text,
					C64Color.YELLOW_TAG,
					item.getName(),
					text2,
					"</color>\n"
				});
			}
			else
			{
				text = string.Concat(new string[]
				{
					text,
					C64Color.WHITE_TAG,
					item.getName(),
					text2,
					"</color>\n"
				});
			}
		}
		return text;
	}

	// Token: 0x06000BCF RID: 3023 RVA: 0x00037DE0 File Offset: 0x00035FE0
	public string getTileImagePath()
	{
		Item currentObject = this.getCurrentObject();
		if (currentObject == null)
		{
			return "Images/Props/Bag";
		}
		return currentObject.getTileImagePath();
	}

	// Token: 0x06000BD0 RID: 3024 RVA: 0x00037E04 File Offset: 0x00036004
	public void checkIntegrity()
	{
		List<Item> list = new List<Item>();
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			Item item = (Item)skaldBaseObject;
			if (!item.isStackable() && item.getCount() > 1)
			{
				MainControl.logError("Stacked non-Stackable items.");
				list.Add(item);
			}
		}
		foreach (Item item2 in list)
		{
			int amount = item2.getCount() - 1;
			item2.setCount(1);
			this.addItem(item2.getId(), amount);
		}
	}

	// Token: 0x06000BD1 RID: 3025 RVA: 0x00037ED8 File Offset: 0x000360D8
	public float getWeight()
	{
		float num = 0f;
		for (int i = 0; i < this.objectList.Count; i++)
		{
			Item item = this.objectList[i] as Item;
			num += item.getWeight();
		}
		return num;
	}

	// Token: 0x06000BD2 RID: 3026 RVA: 0x00037F20 File Offset: 0x00036120
	public void transferInventory(Inventory source)
	{
		if (source == this)
		{
			return;
		}
		foreach (SkaldBaseObject skaldBaseObject in source.objectList)
		{
			Item item = (Item)skaldBaseObject;
			if (item.getCount() > 0)
			{
				this.addItem(item);
			}
		}
		source.purgeList();
	}

	// Token: 0x06000BD3 RID: 3027 RVA: 0x00037F90 File Offset: 0x00036190
	public void transferAllLootableItems(Inventory target)
	{
		List<Item> list = new List<Item>();
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			Item item = (Item)skaldBaseObject;
			if (item.getCount() > 0 && item.isLootable())
			{
				list.Add(item);
			}
		}
		foreach (Item item2 in list)
		{
			this.currentObject = item2;
			this.removeCurrentItemStack();
			target.add(item2);
		}
	}

	// Token: 0x06000BD4 RID: 3028 RVA: 0x00038050 File Offset: 0x00036250
	public void transferLootableInventory(Inventory source)
	{
		if (source == this)
		{
			return;
		}
		source.dropAllLootableItems(this);
	}

	// Token: 0x06000BD5 RID: 3029 RVA: 0x00038060 File Offset: 0x00036260
	public void dropAllLootableItems(Inventory target)
	{
		List<Item> list = new List<Item>();
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			Item item = (Item)skaldBaseObject;
			if (item.getCount() > 0 && item.isLootable())
			{
				if (item.TestLootChance())
				{
					list.Add(item);
				}
				else
				{
					target.addMoney(item.getGoldLootEquivalent());
				}
			}
		}
		foreach (Item item2 in list)
		{
			this.currentObject = item2;
			this.removeCurrentItemStack();
			target.add(item2);
		}
	}

	// Token: 0x06000BD6 RID: 3030 RVA: 0x00038134 File Offset: 0x00036334
	public Inventory removeOwnedItems(Character owner)
	{
		owner.setPreferredAmmo("");
		List<Item> listOfOwnedItems = this.getListOfOwnedItems(owner);
		Inventory inventory = new Inventory();
		foreach (Item item in listOfOwnedItems)
		{
			this.objectList.Remove(item);
			inventory.add(item);
		}
		return inventory;
	}

	// Token: 0x06000BD7 RID: 3031 RVA: 0x000381A8 File Offset: 0x000363A8
	public List<Item> getListOfOwnedItems(Character owner)
	{
		List<Item> list = new List<Item>();
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			Item item = (Item)skaldBaseObject;
			if (item.testUser(owner))
			{
				list.Add(item);
			}
		}
		if (owner.getPreferredAmmoId() != "")
		{
			ItemAmmo itemAmmo = null;
			if (base.containsObject(owner.getPreferredAmmoId()))
			{
				itemAmmo = (base.getObject(owner.getPreferredAmmoId()) as ItemAmmo);
			}
			else
			{
				owner.setPreferredAmmo("");
			}
			if (itemAmmo != null)
			{
				list.Add(itemAmmo);
			}
		}
		return list;
	}

	// Token: 0x06000BD8 RID: 3032 RVA: 0x0003825C File Offset: 0x0003645C
	public bool hasItemOfType(string type)
	{
		using (List<SkaldBaseObject>.Enumerator enumerator = this.objectList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((Item)enumerator.Current).isType(type))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000BD9 RID: 3033 RVA: 0x000382BC File Offset: 0x000364BC
	public List<Item> getListByType(List<Item.ItemTypes> typeList, bool includeEquipped)
	{
		List<Item> list = new List<Item>();
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			Item item = (Item)skaldBaseObject;
			if ((typeList == null || typeList.Count == 0 || typeList.Contains(item.getType())) && (includeEquipped || !item.hasUser()))
			{
				list.Add(item);
			}
		}
		return list;
	}

	// Token: 0x06000BDA RID: 3034 RVA: 0x00038340 File Offset: 0x00036540
	public void transferInventoryAndClearUser(Inventory source)
	{
		if (source == this)
		{
			return;
		}
		source.clearAllUsers();
		this.transferInventory(source);
	}

	// Token: 0x06000BDB RID: 3035 RVA: 0x00038354 File Offset: 0x00036554
	public void transferLootableInventoryAndClearUser(Inventory source)
	{
		if (source == this)
		{
			return;
		}
		this.transferLootableInventory(source);
	}

	// Token: 0x06000BDC RID: 3036 RVA: 0x00038364 File Offset: 0x00036564
	public void clearAllUsers()
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((Item)skaldBaseObject).clearUser();
		}
	}

	// Token: 0x06000BDD RID: 3037 RVA: 0x000383BC File Offset: 0x000365BC
	public bool testMoneyGT(int amount)
	{
		return amount < this.getMoney();
	}

	// Token: 0x06000BDE RID: 3038 RVA: 0x000383CA File Offset: 0x000365CA
	public bool testItemGT(string item, int ammount)
	{
		return this.getItemCount(item) > ammount;
	}

	// Token: 0x06000BDF RID: 3039 RVA: 0x000383DC File Offset: 0x000365DC
	public int getItemCount(string item)
	{
		int num = 0;
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			Item item2 = (Item)skaldBaseObject;
			if (item2.getId() == item)
			{
				num += item2.getCount();
			}
		}
		return num;
	}

	// Token: 0x04000315 RID: 789
	private bool tagNewAdditions;
}
