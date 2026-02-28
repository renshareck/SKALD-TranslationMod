using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

// Token: 0x020000BB RID: 187
[Serializable]
public class Item : SkaldWorldObject, ISerializable
{
	// Token: 0x06000BE0 RID: 3040 RVA: 0x00038448 File Offset: 0x00036648
	public Item()
	{
		this.dynamicData = new Item.ItemSaveData(this.worldPosition, this.coreData, this.instanceData);
	}

	// Token: 0x06000BE1 RID: 3041 RVA: 0x00038478 File Offset: 0x00036678
	public Item(SKALDProjectData.ItemDataContainers.ItemData rawData) : base(rawData)
	{
		this.rawData = rawData;
		this.dynamicData = new Item.ItemSaveData(this.worldPosition, this.coreData, this.instanceData);
		this.setCount(1);
		this.setStolen(rawData.stolen);
	}

	// Token: 0x06000BE2 RID: 3042 RVA: 0x000384CF File Offset: 0x000366CF
	public void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("saveData", this.dynamicData, typeof(Item.ItemSaveData));
	}

	// Token: 0x06000BE3 RID: 3043 RVA: 0x000384EC File Offset: 0x000366EC
	public override string getColorTag()
	{
		return C64Color.YELLOW_TAG;
	}

	// Token: 0x06000BE4 RID: 3044 RVA: 0x000384F3 File Offset: 0x000366F3
	public void playUseSound()
	{
		AudioControl.playSound(this.getUseSound());
	}

	// Token: 0x06000BE5 RID: 3045 RVA: 0x00038500 File Offset: 0x00036700
	protected virtual string getUseSound()
	{
		return "Inventory1";
	}

	// Token: 0x06000BE6 RID: 3046 RVA: 0x00038507 File Offset: 0x00036707
	public void markAsNewAddition()
	{
		this.newAddition = true;
	}

	// Token: 0x06000BE7 RID: 3047 RVA: 0x00038510 File Offset: 0x00036710
	public void clearNewAddition()
	{
		this.newAddition = false;
	}

	// Token: 0x06000BE8 RID: 3048 RVA: 0x00038519 File Offset: 0x00036719
	public bool isNewAddition()
	{
		return this.newAddition;
	}

	// Token: 0x06000BE9 RID: 3049 RVA: 0x00038524 File Offset: 0x00036724
	private static int getTypeSortOrder(Item.ItemTypes type)
	{
		if (Item.typeSortOrder == null)
		{
			Item.typeSortOrder = new Dictionary<Item.ItemTypes, int>();
			int num = 0;
			foreach (object obj in Enum.GetValues(typeof(Item.ItemTypes)))
			{
				Item.ItemTypes key = (Item.ItemTypes)obj;
				Item.typeSortOrder.Add(key, num);
				num++;
			}
		}
		if (Item.typeSortOrder.ContainsKey(type))
		{
			return Item.typeSortOrder[type];
		}
		MainControl.logError("Sort order error.");
		return -1;
	}

	// Token: 0x06000BEA RID: 3050 RVA: 0x000385C8 File Offset: 0x000367C8
	public int getTypeSortOrder()
	{
		return Item.getTypeSortOrder(this.getType());
	}

	// Token: 0x06000BEB RID: 3051 RVA: 0x000385D5 File Offset: 0x000367D5
	public void setInventoryPosition(int x, int y)
	{
		this.inventoryGridX = x + 10;
		this.inventoryGridY = y - 10;
	}

	// Token: 0x06000BEC RID: 3052 RVA: 0x000385EB File Offset: 0x000367EB
	public int getInventoryGridX()
	{
		return this.inventoryGridX;
	}

	// Token: 0x06000BED RID: 3053 RVA: 0x000385F3 File Offset: 0x000367F3
	public int getInventoryGridY()
	{
		return this.inventoryGridY;
	}

	// Token: 0x06000BEE RID: 3054 RVA: 0x000385FC File Offset: 0x000367FC
	public Item(SerializationInfo info, StreamingContext context)
	{
		this.dynamicData = (Item.ItemSaveData)info.GetValue("saveData", typeof(Item.ItemSaveData));
		if (this.dynamicData == null)
		{
			MainControl.logError("Item could not load dynamic data!");
		}
		this.applySaveData(this.dynamicData);
	}

	// Token: 0x06000BEF RID: 3055 RVA: 0x00038658 File Offset: 0x00036858
	public virtual SKALDProjectData.ItemDataContainers.ItemData getRawData()
	{
		if (this.rawData == null)
		{
			this.rawData = GameData.getItemRawData(this.getId());
		}
		return this.rawData;
	}

	// Token: 0x06000BF0 RID: 3056 RVA: 0x00038679 File Offset: 0x00036879
	public virtual void setUser(Character c)
	{
		this.dynamicData.user = c;
		c.updateItemSlot();
	}

	// Token: 0x06000BF1 RID: 3057 RVA: 0x00038690 File Offset: 0x00036890
	public override string getImagePath()
	{
		SKALDProjectData.ItemDataContainers.ItemData itemData = this.getRawData();
		if (itemData == null)
		{
			return "Generic";
		}
		string text = itemData.imagePath;
		if (text == "" && itemData.parent != null && itemData.parent != "")
		{
			SKALDProjectData.ItemDataContainers.ItemData itemRawData = GameData.getItemRawData(itemData.parent);
			if (itemRawData != null)
			{
				text = itemRawData.imagePath;
			}
		}
		if (text == null || text == "")
		{
			text = "Generic";
		}
		return text;
	}

	// Token: 0x06000BF2 RID: 3058 RVA: 0x00038708 File Offset: 0x00036908
	private string getIconPath()
	{
		if (this.iconPath == "")
		{
			this.iconPath = "Images/InventoryIcons/" + this.getImagePath();
		}
		return this.iconPath;
	}

	// Token: 0x06000BF3 RID: 3059 RVA: 0x00038738 File Offset: 0x00036938
	public TextureTools.TextureData getBaseIcon()
	{
		TextureTools.TextureData textureData = TextureTools.loadTextureData(this.getIconPath());
		if (textureData == null)
		{
			textureData = TextureTools.loadTextureData("Images/InventoryIcons/Generic");
		}
		return textureData;
	}

	// Token: 0x06000BF4 RID: 3060 RVA: 0x00038760 File Offset: 0x00036960
	public override TextureTools.TextureData getGridIcon()
	{
		this.gridIcon = this.getBaseIcon();
		TextureTools.applyOverlay(this.gridIcon, this.getMagicIconOutline(), -1, -1);
		if (this.getCount() > 1)
		{
			if (this.getCount() < 10)
			{
				TextureTools.TextureData textureData = TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/Digits/" + this.getCount().ToString());
				if (textureData != null)
				{
					textureData.applyOverlay(10, 0, this.gridIcon);
				}
			}
			else
			{
				Item.plussIcon.applyOverlay(11, 0, this.gridIcon);
			}
		}
		if (this.isNewAddition())
		{
			Item.newIcon.applyOverlay(0, 0, this.gridIcon);
		}
		return this.gridIcon;
	}

	// Token: 0x06000BF5 RID: 3061 RVA: 0x00038804 File Offset: 0x00036A04
	public TextureTools.TextureData getGridIconOutline()
	{
		return Item.outlineIcon;
	}

	// Token: 0x06000BF6 RID: 3062 RVA: 0x0003880C File Offset: 0x00036A0C
	private TextureTools.TextureData getMagicIconOutline()
	{
		if (this.magicOutline != null)
		{
			return this.magicOutline;
		}
		if (this.getMagicLevel() == 0)
		{
			return null;
		}
		if (this.getMagicLevel() == 1)
		{
			this.magicOutline = this.getBaseIcon().getOutline(C64Color.BlueLight);
		}
		else if (this.getMagicLevel() == 2)
		{
			this.magicOutline = this.getBaseIcon().getOutline(C64Color.GreenLight);
		}
		else
		{
			this.magicOutline = this.getBaseIcon().getOutline(C64Color.Yellow);
		}
		return this.magicOutline;
	}

	// Token: 0x06000BF7 RID: 3063 RVA: 0x00038890 File Offset: 0x00036A90
	public string getTileImagePath()
	{
		return "Images/Props/Bag";
	}

	// Token: 0x06000BF8 RID: 3064 RVA: 0x00038898 File Offset: 0x00036A98
	public override string getModelPath()
	{
		SKALDProjectData.ItemDataContainers.ItemData itemData = this.getRawData();
		if (itemData == null)
		{
			return this.getModelBasePath();
		}
		string text = itemData.modelPath;
		if (text == "" && itemData.parent != null && itemData.parent != "")
		{
			SKALDProjectData.ItemDataContainers.ItemData itemRawData = GameData.getItemRawData(itemData.parent);
			if (itemRawData != null)
			{
				text = itemRawData.modelPath;
			}
		}
		if (text == "")
		{
			text = this.getImagePath();
		}
		return this.getModelBasePath() + text;
	}

	// Token: 0x06000BF9 RID: 3065 RVA: 0x0003891A File Offset: 0x00036B1A
	protected virtual string getModelBasePath()
	{
		return "Images/Models/Items/";
	}

	// Token: 0x06000BFA RID: 3066 RVA: 0x00038921 File Offset: 0x00036B21
	public override void setToBeRemoved()
	{
		this.clearUser();
		base.setToBeRemoved();
	}

	// Token: 0x06000BFB RID: 3067 RVA: 0x0003892F File Offset: 0x00036B2F
	public virtual Item.ItemTypes getType()
	{
		return Item.ItemTypes.Misc;
	}

	// Token: 0x06000BFC RID: 3068 RVA: 0x00038933 File Offset: 0x00036B33
	protected Character getUser()
	{
		return this.dynamicData.user;
	}

	// Token: 0x06000BFD RID: 3069 RVA: 0x00038940 File Offset: 0x00036B40
	public virtual string getUseVerb(Character character)
	{
		return "...";
	}

	// Token: 0x06000BFE RID: 3070 RVA: 0x00038947 File Offset: 0x00036B47
	public bool testUser(Character c)
	{
		return this.getUser() != null && this.getUser() == c;
	}

	// Token: 0x06000BFF RID: 3071 RVA: 0x0003895C File Offset: 0x00036B5C
	public void setCountToStoreStack()
	{
		if (this.getCount() < this.getStoreStack())
		{
			this.setCount(this.getStoreStack());
		}
	}

	// Token: 0x06000C00 RID: 3072 RVA: 0x0003897C File Offset: 0x00036B7C
	public List<string> getConferredAbilities()
	{
		if (this.conferredAbilities != null)
		{
			return this.conferredAbilities;
		}
		this.conferredAbilities = new List<string>();
		SKALDProjectData.ItemDataContainers.ItemData itemData = this.getRawData();
		if (itemData != null)
		{
			foreach (string item in itemData.conferredAbilities)
			{
				this.conferredAbilities.Add(item);
			}
		}
		SKALDProjectData.EnchantmentContainers.Enchantment enchantment = this.getEnchantment();
		if (enchantment != null && enchantment.conferredAbilities != null)
		{
			foreach (string item2 in enchantment.conferredAbilities)
			{
				this.conferredAbilities.Add(item2);
			}
		}
		return this.conferredAbilities;
	}

	// Token: 0x06000C01 RID: 3073 RVA: 0x00038A5C File Offset: 0x00036C5C
	public int getPowerLevel()
	{
		SKALDProjectData.ItemDataContainers.ItemData itemData = this.getRawData();
		if (itemData != null)
		{
			return itemData.powerLevel;
		}
		return 1;
	}

	// Token: 0x06000C02 RID: 3074 RVA: 0x00038A7C File Offset: 0x00036C7C
	public List<string> getConferredConditions()
	{
		if (this.conferredConditions != null)
		{
			return this.conferredConditions;
		}
		this.conferredConditions = new List<string>();
		SKALDProjectData.ItemDataContainers.ItemData itemData = this.getRawData();
		if (itemData == null)
		{
			return this.conferredConditions;
		}
		foreach (string item in itemData.conferredConditions)
		{
			this.conferredConditions.Add(item);
		}
		if (itemData.parent != null && itemData.parent != "")
		{
			SKALDProjectData.ItemDataContainers.ItemData itemRawData = GameData.getItemRawData(itemData.parent);
			if (itemRawData != null)
			{
				foreach (string item2 in itemRawData.conferredConditions)
				{
					this.conferredConditions.Add(item2);
				}
			}
		}
		SKALDProjectData.EnchantmentContainers.Enchantment enchantment = this.getEnchantment();
		if (enchantment != null && enchantment.conferredConditions != null)
		{
			foreach (string item3 in enchantment.conferredConditions)
			{
				this.conferredConditions.Add(item3);
			}
		}
		return this.conferredConditions;
	}

	// Token: 0x06000C03 RID: 3075 RVA: 0x00038BD8 File Offset: 0x00036DD8
	internal bool isMagical()
	{
		return this.getMagicLevel() > 0;
	}

	// Token: 0x06000C04 RID: 3076 RVA: 0x00038BE4 File Offset: 0x00036DE4
	internal int getMagicLevel()
	{
		SKALDProjectData.ItemDataContainers.ItemData itemData = this.getRawData();
		if (itemData == null)
		{
			return 0;
		}
		int num = itemData.magicLevel;
		if (itemData.parent != null && itemData.parent != "")
		{
			SKALDProjectData.ItemDataContainers.ItemData itemRawData = GameData.getItemRawData(itemData.parent);
			if (itemRawData != null)
			{
				num += itemRawData.magicLevel;
			}
		}
		SKALDProjectData.EnchantmentContainers.Enchantment enchantment = this.getEnchantment();
		if (enchantment != null)
		{
			num += enchantment.magicLevel;
		}
		return num;
	}

	// Token: 0x06000C05 RID: 3077 RVA: 0x00038C4C File Offset: 0x00036E4C
	public SKALDProjectData.EnchantmentContainers.Enchantment getEnchantment()
	{
		SKALDProjectData.ItemDataContainers.ItemData itemData = this.getRawData();
		if (itemData == null)
		{
			return null;
		}
		if (itemData.enchantment == null || itemData.enchantment == "")
		{
			return null;
		}
		return GameData.getEnchantmentRawData(itemData.enchantment);
	}

	// Token: 0x06000C06 RID: 3078 RVA: 0x00038C8C File Offset: 0x00036E8C
	public List<string> getConferredSpells()
	{
		List<string> list = new List<string>();
		SKALDProjectData.ItemDataContainers.ItemData itemData = this.getRawData();
		if (itemData == null)
		{
			return list;
		}
		foreach (string item in itemData.conferredSpells)
		{
			list.Add(item);
		}
		if (itemData.parent != null && itemData.parent != "")
		{
			SKALDProjectData.ItemDataContainers.ItemData itemRawData = GameData.getItemRawData(itemData.parent);
			if (itemRawData != null)
			{
				foreach (string item2 in itemRawData.conferredSpells)
				{
					list.Add(item2);
				}
			}
		}
		SKALDProjectData.EnchantmentContainers.Enchantment enchantment = this.getEnchantment();
		if (enchantment != null && enchantment.conferredSpells != null)
		{
			foreach (string item3 in enchantment.conferredSpells)
			{
				list.Add(item3);
			}
		}
		return list;
	}

	// Token: 0x06000C07 RID: 3079 RVA: 0x00038DBC File Offset: 0x00036FBC
	public void clearUser()
	{
		Character user = this.dynamicData.user;
		if (user != null)
		{
			user.updateItemSlot();
		}
		this.dynamicData.user = null;
	}

	// Token: 0x06000C08 RID: 3080 RVA: 0x00038DEC File Offset: 0x00036FEC
	internal UIButtonControlBase.ButtonData getButtonData()
	{
		return new UIButtonControlBase.ButtonData(this.getBaseIcon(), this.getName())
		{
			count = this.getCount()
		};
	}

	// Token: 0x06000C09 RID: 3081 RVA: 0x00038E1A File Offset: 0x0003701A
	public bool hasUser()
	{
		return this.getUser() != null;
	}

	// Token: 0x06000C0A RID: 3082 RVA: 0x00038E25 File Offset: 0x00037025
	public bool isStolen()
	{
		return this.dynamicData.stolen;
	}

	// Token: 0x06000C0B RID: 3083 RVA: 0x00038E34 File Offset: 0x00037034
	public virtual bool isQuestItem()
	{
		SKALDProjectData.ItemDataContainers.ItemData itemData = this.getRawData();
		return itemData == null || itemData.questItem;
	}

	// Token: 0x06000C0C RID: 3084 RVA: 0x00038E54 File Offset: 0x00037054
	public virtual bool isStackable()
	{
		SKALDProjectData.ItemDataContainers.ItemData itemData = this.getRawData();
		return itemData != null && itemData.stackable;
	}

	// Token: 0x06000C0D RID: 3085 RVA: 0x00038E73 File Offset: 0x00037073
	public void setStolen(bool b)
	{
		this.dynamicData.stolen = b;
	}

	// Token: 0x06000C0E RID: 3086 RVA: 0x00038E81 File Offset: 0x00037081
	protected virtual string getUseActionResultString()
	{
		return this.getName() + " cannot be used like this.";
	}

	// Token: 0x06000C0F RID: 3087 RVA: 0x00038E93 File Offset: 0x00037093
	public virtual SkaldActionResult useItem(Character user)
	{
		return new SkaldActionResult(false, false, this.getUseActionResultString(), true);
	}

	// Token: 0x06000C10 RID: 3088 RVA: 0x00038EA3 File Offset: 0x000370A3
	public virtual bool isUseable()
	{
		return false;
	}

	// Token: 0x06000C11 RID: 3089 RVA: 0x00038EA6 File Offset: 0x000370A6
	public virtual bool isEquipable()
	{
		return false;
	}

	// Token: 0x06000C12 RID: 3090 RVA: 0x00038EAC File Offset: 0x000370AC
	public virtual bool isLootable()
	{
		SKALDProjectData.ItemDataContainers.ItemData itemData = this.getRawData();
		return itemData != null && itemData.lootable;
	}

	// Token: 0x06000C13 RID: 3091 RVA: 0x00038ECC File Offset: 0x000370CC
	public virtual bool TestLootChance()
	{
		if (this.isUnique())
		{
			return true;
		}
		if (this.isQuestItem())
		{
			return true;
		}
		if (this.getType() == Item.ItemTypes.Key || this.getType() == Item.ItemTypes.Misc || this.getType() == Item.ItemTypes.Book || this.getType() == Item.ItemTypes.Ammo)
		{
			return true;
		}
		if (this.getMagicLevel() >= 2)
		{
			return true;
		}
		int num = (this.getPowerLevel() + this.getMagicLevel()) * 10;
		return SkaldRandom.range(0, 100) < num;
	}

	// Token: 0x06000C14 RID: 3092 RVA: 0x00038F40 File Offset: 0x00037140
	internal int getGoldLootEquivalent()
	{
		float num = (float)(this.getValue() * this.getCount());
		if (num == 0f)
		{
			return 0;
		}
		float minInclusive = num / 10f;
		float num2 = num / 5f;
		if (num2 < 1f)
		{
			num2 = 1f;
		}
		float num3 = Random.Range(minInclusive, num2);
		float num4 = 1f + (float)MainControl.getDataControl().getGoldDropBonus() / 100f;
		return Mathf.RoundToInt(num3 * num4);
	}

	// Token: 0x06000C15 RID: 3093 RVA: 0x00038FAC File Offset: 0x000371AC
	internal int getGoldLootEquivalent2()
	{
		int num = Mathf.RoundToInt((float)(this.getValue() * this.getCount()) / 20f);
		float num2 = 1f + (float)MainControl.getDataControl().getGoldDropBonus() / 100f;
		return Mathf.RoundToInt((float)(SkaldRandom.range(0, this.getPowerLevel() * 4) + num) * num2);
	}

	// Token: 0x06000C16 RID: 3094 RVA: 0x00039004 File Offset: 0x00037204
	public bool isType(string type)
	{
		type = type.ToUpper();
		return this.getType().ToString().ToUpper() == type;
	}

	// Token: 0x06000C17 RID: 3095 RVA: 0x00039038 File Offset: 0x00037238
	public bool isType(Item.ItemTypes type)
	{
		return this.getType() == type;
	}

	// Token: 0x06000C18 RID: 3096 RVA: 0x00039044 File Offset: 0x00037244
	protected virtual string getTypeString()
	{
		return this.getType().ToString();
	}

	// Token: 0x06000C19 RID: 3097 RVA: 0x00039065 File Offset: 0x00037265
	public int getStealDC()
	{
		return this.getValue() / 1000;
	}

	// Token: 0x06000C1A RID: 3098 RVA: 0x00039073 File Offset: 0x00037273
	public int getSuspicionIncreaseIfStolen()
	{
		return 1 + this.getStealDC();
	}

	// Token: 0x06000C1B RID: 3099 RVA: 0x00039080 File Offset: 0x00037280
	public virtual float getWeight()
	{
		SKALDProjectData.ItemDataContainers.ItemData itemData = this.getRawData();
		if (itemData == null)
		{
			return 0f;
		}
		float num = itemData.weight;
		if (itemData.parent != null && itemData.parent != "")
		{
			SKALDProjectData.ItemDataContainers.ItemData itemRawData = GameData.getItemRawData(itemData.parent);
			if (itemRawData != null)
			{
				num += itemRawData.weight;
			}
		}
		return num * (float)this.getCount();
	}

	// Token: 0x06000C1C RID: 3100 RVA: 0x000390E0 File Offset: 0x000372E0
	public virtual int getValue()
	{
		SKALDProjectData.ItemDataContainers.ItemData itemData = this.getRawData();
		if (itemData == null)
		{
			return 0;
		}
		int num = itemData.value;
		SKALDProjectData.EnchantmentContainers.Enchantment enchantment = this.getEnchantment();
		if (enchantment != null)
		{
			num = Mathf.RoundToInt((float)num * enchantment.valueMultiplier + (float)enchantment.basePrice);
		}
		return num;
	}

	// Token: 0x06000C1D RID: 3101 RVA: 0x00039124 File Offset: 0x00037324
	public virtual bool isSellable()
	{
		SKALDProjectData.ItemDataContainers.ItemData itemData = this.getRawData();
		return itemData != null && itemData.sellable;
	}

	// Token: 0x06000C1E RID: 3102 RVA: 0x00039143 File Offset: 0x00037343
	public bool canBeTraded()
	{
		return this.isSellable() && !this.isQuestItem() && this.getUser() == null;
	}

	// Token: 0x06000C1F RID: 3103 RVA: 0x00039164 File Offset: 0x00037364
	public override string getDescription()
	{
		string text = base.getDescription();
		if (text == "")
		{
			SKALDProjectData.ItemDataContainers.ItemData itemRawData = GameData.getItemRawData(this.getRawData().parent);
			if (itemRawData != null)
			{
				text += itemRawData.description;
			}
		}
		SKALDProjectData.EnchantmentContainers.Enchantment enchantment = this.getEnchantment();
		if (enchantment != null)
		{
			text = text + "\n\n" + enchantment.description;
		}
		return text;
	}

	// Token: 0x06000C20 RID: 3104 RVA: 0x000391C4 File Offset: 0x000373C4
	protected virtual string printStatsHeader()
	{
		string str = this.getNameFullColored();
		str = str + "\n" + C64Color.GRAY_LIGHT_TAG;
		str = str + "[" + this.getTypeString() + "]";
		if (this.isMagical())
		{
			str += " [Enchanted]";
		}
		return str + "</color>\n\n";
	}

	// Token: 0x06000C21 RID: 3105 RVA: 0x00039224 File Offset: 0x00037424
	protected virtual string printStatsTail()
	{
		string str = "" + TextTools.formateNameValuePair("Value", this.getValue().ToString() + " GP") + "\n";
		string str2 = (this.getWeight() > 1f) ? " lbs\n" : " lb\n";
		return (str + TextTools.formateNameValuePair("Weight", this.getWeight().ToString("0.00") + str2) + "\n\n" + this.getDescription()).TrimEnd(new char[]
		{
			'\n'
		}).TrimEnd(new char[]
		{
			'\n'
		});
	}

	// Token: 0x06000C22 RID: 3106 RVA: 0x000392D4 File Offset: 0x000374D4
	public virtual int getStoreStack()
	{
		if (!this.isStackable())
		{
			return 1;
		}
		SKALDProjectData.ItemDataContainers.ItemData itemData = this.getRawData();
		if (itemData != null)
		{
			return itemData.storeStack;
		}
		return 1;
	}

	// Token: 0x06000C23 RID: 3107 RVA: 0x000392FD File Offset: 0x000374FD
	public bool doesItemCompeteForSlot(Item item)
	{
		return (this.isOffhand() && item.isOffhand()) || item.isType(this.getType());
	}

	// Token: 0x06000C24 RID: 3108 RVA: 0x00039322 File Offset: 0x00037522
	public bool isOffhand()
	{
		return this.isType(Item.ItemTypes.Shield);
	}

	// Token: 0x06000C25 RID: 3109 RVA: 0x00039330 File Offset: 0x00037530
	protected string makeComparativeColorTag(float i1, float i2)
	{
		return this.makeComparativeColorTag(i1, i2, "", "");
	}

	// Token: 0x06000C26 RID: 3110 RVA: 0x00039344 File Offset: 0x00037544
	protected string makeComparativeColorTag(float i1, float i2, string prefix, string suffix)
	{
		string text = C64Color.WHITE_TAG;
		if (i1 == i2)
		{
			return string.Concat(new string[]
			{
				text,
				prefix,
				i1.ToString(),
				suffix,
				"</color>"
			});
		}
		if (i1 > i2)
		{
			text = C64Color.GREEN_LIGHT_TAG;
		}
		else if (i1 < i2)
		{
			text = C64Color.RED_LIGHT_TAG;
		}
		return string.Concat(new string[]
		{
			text,
			prefix,
			i1.ToString(),
			suffix,
			"</color> (Vs: ",
			prefix,
			i2.ToString(),
			suffix,
			")"
		});
	}

	// Token: 0x06000C27 RID: 3111 RVA: 0x000393E4 File Offset: 0x000375E4
	protected string makeComparativeColorTagPlusMinus(float i1, float i2, string prefix, string suffix)
	{
		string text = C64Color.WHITE_TAG;
		if (i1 == i2)
		{
			return string.Concat(new string[]
			{
				text,
				prefix,
				TextTools.formatePlusMinus(i1),
				suffix,
				"</color>"
			});
		}
		if (i1 > i2)
		{
			text = C64Color.GREEN_LIGHT_TAG;
		}
		else if (i1 < i2)
		{
			text = C64Color.RED_LIGHT_TAG;
		}
		return string.Concat(new string[]
		{
			text,
			prefix,
			TextTools.formatePlusMinus(i1),
			suffix,
			"</color> (Vs: ",
			prefix,
			TextTools.formatePlusMinus(i2),
			suffix,
			")"
		});
	}

	// Token: 0x06000C28 RID: 3112 RVA: 0x00039480 File Offset: 0x00037680
	protected string makeComparativeColorTagReversed(float i1, float i2, string prefix, string suffix)
	{
		string text = C64Color.WHITE_TAG;
		if (i1 == i2)
		{
			return text + i1.ToString() + "</color>";
		}
		if (i1 < i2)
		{
			text = C64Color.GREEN_LIGHT_TAG;
		}
		else if (i1 > i2)
		{
			text = C64Color.RED_LIGHT_TAG;
		}
		return string.Concat(new string[]
		{
			text,
			prefix,
			i1.ToString(),
			suffix,
			"</color> (Vs: ",
			prefix,
			i2.ToString(),
			suffix,
			")"
		});
	}

	// Token: 0x06000C29 RID: 3113 RVA: 0x00039508 File Offset: 0x00037708
	public override string getName()
	{
		SKALDProjectData.EnchantmentContainers.Enchantment enchantment = this.getEnchantment();
		if (enchantment == null)
		{
			return base.getName();
		}
		string text = base.getName();
		if (enchantment.prefix != "")
		{
			text = enchantment.prefix + " " + text;
		}
		if (enchantment.suffix != "")
		{
			text = text + " " + enchantment.suffix;
		}
		return text;
	}

	// Token: 0x06000C2A RID: 3114 RVA: 0x00039578 File Offset: 0x00037778
	public string getNameAndAmount()
	{
		string text = this.getName();
		if (this.getCount() > 1)
		{
			text = text + " (" + this.getCount().ToString() + ")";
		}
		return text;
	}

	// Token: 0x06000C2B RID: 3115 RVA: 0x000395B5 File Offset: 0x000377B5
	public virtual string getNameFullColored()
	{
		return C64Color.HEADER_TAG + this.getNameAndAmount().ToUpper() + C64Color.HEADER_CLOSING_TAG;
	}

	// Token: 0x06000C2C RID: 3116 RVA: 0x000395D4 File Offset: 0x000377D4
	protected string getNameInfo()
	{
		string text = "";
		if (this.getCount() > 1)
		{
			text = text + " (" + this.getCount().ToString() + ")";
		}
		if (this.hasUser())
		{
			text = text + "\n\n[" + this.getUser().getName() + "]";
		}
		return text;
	}

	// Token: 0x06000C2D RID: 3117 RVA: 0x00039634 File Offset: 0x00037834
	public virtual string printComparativeStats(Character c)
	{
		return this.printStatsHeader() + this.printStatsTail();
	}

	// Token: 0x06000C2E RID: 3118 RVA: 0x00039648 File Offset: 0x00037848
	public int addCount(int amount)
	{
		if (!this.isStackable())
		{
			MainControl.logError("Trying to stack non-stackable object!");
			return 0;
		}
		this.dynamicData.count += amount;
		if (this.getCount() < 0)
		{
			this.dynamicData.count = 0;
		}
		return this.getCount();
	}

	// Token: 0x06000C2F RID: 3119 RVA: 0x00039698 File Offset: 0x00037898
	public int setCount(int amount)
	{
		if (!this.isStackable() && amount > 1)
		{
			MainControl.logError("Trying to stack non-stackable object with id " + this.getId());
			return 0;
		}
		this.dynamicData.count = amount;
		if (this.getCount() < 0)
		{
			this.dynamicData.count = 0;
		}
		return this.getCount();
	}

	// Token: 0x06000C30 RID: 3120 RVA: 0x000396EF File Offset: 0x000378EF
	public int getCount()
	{
		return this.dynamicData.count;
	}

	// Token: 0x04000316 RID: 790
	protected Item.ItemSaveData dynamicData;

	// Token: 0x04000317 RID: 791
	protected SKALDProjectData.ItemDataContainers.ItemData rawData;

	// Token: 0x04000318 RID: 792
	public static TextureTools.TextureData plussIcon = TextureTools.loadTextureData("Images/GUIIcons/InventoryUI/Pluss");

	// Token: 0x04000319 RID: 793
	protected static TextureTools.TextureData newIcon = TextureTools.loadTextureData("Images/GUIIcons/InventoryUI/NewIcon");

	// Token: 0x0400031A RID: 794
	protected static TextureTools.TextureData outlineIcon = TextureTools.loadTextureData("Images/GUIIcons/InventoryUI/OutlineBox");

	// Token: 0x0400031B RID: 795
	private TextureTools.TextureData gridIcon;

	// Token: 0x0400031C RID: 796
	private TextureTools.TextureData magicOutline;

	// Token: 0x0400031D RID: 797
	private List<string> conferredAbilities;

	// Token: 0x0400031E RID: 798
	private List<string> conferredConditions;

	// Token: 0x0400031F RID: 799
	private int inventoryGridX;

	// Token: 0x04000320 RID: 800
	private int inventoryGridY;

	// Token: 0x04000321 RID: 801
	private bool newAddition;

	// Token: 0x04000322 RID: 802
	private string iconPath = "";

	// Token: 0x04000323 RID: 803
	private static Dictionary<Item.ItemTypes, int> typeSortOrder = null;

	// Token: 0x02000243 RID: 579
	public enum ItemTypes
	{
		// Token: 0x040008BD RID: 2237
		MeleeWeapon,
		// Token: 0x040008BE RID: 2238
		RangedWeapon,
		// Token: 0x040008BF RID: 2239
		Ammo,
		// Token: 0x040008C0 RID: 2240
		Armor,
		// Token: 0x040008C1 RID: 2241
		Shield,
		// Token: 0x040008C2 RID: 2242
		Glove,
		// Token: 0x040008C3 RID: 2243
		Footwear,
		// Token: 0x040008C4 RID: 2244
		Headwear,
		// Token: 0x040008C5 RID: 2245
		Clothing,
		// Token: 0x040008C6 RID: 2246
		Jewelry,
		// Token: 0x040008C7 RID: 2247
		Ring,
		// Token: 0x040008C8 RID: 2248
		Necklace,
		// Token: 0x040008C9 RID: 2249
		Adventuring,
		// Token: 0x040008CA RID: 2250
		Light,
		// Token: 0x040008CB RID: 2251
		Consumable,
		// Token: 0x040008CC RID: 2252
		Food,
		// Token: 0x040008CD RID: 2253
		Book,
		// Token: 0x040008CE RID: 2254
		Tome,
		// Token: 0x040008CF RID: 2255
		Misc,
		// Token: 0x040008D0 RID: 2256
		Key,
		// Token: 0x040008D1 RID: 2257
		Reagent,
		// Token: 0x040008D2 RID: 2258
		Gems,
		// Token: 0x040008D3 RID: 2259
		Trinket,
		// Token: 0x040008D4 RID: 2260
		Idle
	}

	// Token: 0x02000244 RID: 580
	[Serializable]
	protected class ItemSaveData : BaseSaveData
	{
		// Token: 0x06001931 RID: 6449 RVA: 0x0006E415 File Offset: 0x0006C615
		public ItemSaveData(SkaldWorldObject.WorldPosition position, SkaldBaseObject.CoreData coreData, SkaldInstanceObject.InstanceData instanceData) : base(position, coreData, instanceData)
		{
		}

		// Token: 0x040008D5 RID: 2261
		public int count;

		// Token: 0x040008D6 RID: 2262
		public Character user;

		// Token: 0x040008D7 RID: 2263
		public bool stolen;
	}
}
