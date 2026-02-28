using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000113 RID: 275
[Serializable]
public class Store : SkaldInstanceObject
{
	// Token: 0x0600115D RID: 4445 RVA: 0x0004DBD8 File Offset: 0x0004BDD8
	public Store(Character owner)
	{
		if (owner != null)
		{
			this.inventory = owner.getInventory();
			this.owner = owner;
		}
	}

	// Token: 0x0600115E RID: 4446 RVA: 0x0004DC48 File Offset: 0x0004BE48
	public Store(SKALDProjectData.Objects.StoreDataContainer.StoreData data, Character owner) : base(data)
	{
		this.owner = owner;
		this.itemTypes = data.itemTypes;
		this.loadoutIdList = data.loadouts;
		this.restockTrigger = data.restockTrigger;
		this.mountTrigger = data.mountTrigger;
		this.minRarity = data.minRarity;
		this.maxRarity = data.maxRarity;
		this.minMagic = data.minMagic;
		this.maxMagic = data.maxMagic;
		this.startingGold = data.startingGold;
		this.fence = data.fence;
		this.fullStock = data.fullStock;
		this.restockRate = data.restockRate;
		this.slotsPerTypes = data.slotsPerType;
		this.stockStore();
	}

	// Token: 0x0600115F RID: 4447 RVA: 0x0004DD4A File Offset: 0x0004BF4A
	private Character getOwner()
	{
		return this.owner;
	}

	// Token: 0x06001160 RID: 4448 RVA: 0x0004DD52 File Offset: 0x0004BF52
	public bool isStealLocked()
	{
		return this.getOwner() != null && this.getOwner().isStealLocked();
	}

	// Token: 0x06001161 RID: 4449 RVA: 0x0004DD69 File Offset: 0x0004BF69
	public void activateStealLocked()
	{
		if (this.getOwner() == null)
		{
			return;
		}
		this.getOwner().setStealLock(true);
	}

	// Token: 0x06001162 RID: 4450 RVA: 0x0004DD80 File Offset: 0x0004BF80
	private void clearStealLocked()
	{
		if (this.getOwner() == null)
		{
			return;
		}
		this.getOwner().setStealLock(false);
	}

	// Token: 0x06001163 RID: 4451 RVA: 0x0004DD97 File Offset: 0x0004BF97
	private SKALDProjectData.Objects.StoreDataContainer.StoreData getRawData()
	{
		if (this.getId() == "")
		{
			return null;
		}
		return GameData.getStoreRawData(this.getId());
	}

	// Token: 0x06001164 RID: 4452 RVA: 0x0004DDB8 File Offset: 0x0004BFB8
	public override string getColorTag()
	{
		return C64Color.YELLOW_TAG;
	}

	// Token: 0x06001165 RID: 4453 RVA: 0x0004DDBF File Offset: 0x0004BFBF
	public void setOwner(Character owner)
	{
		this.owner = owner;
	}

	// Token: 0x06001166 RID: 4454 RVA: 0x0004DDC8 File Offset: 0x0004BFC8
	public override string getName()
	{
		if (this.getOwner() == null)
		{
			return "Trading";
		}
		if (this.getOwner().getName() != "")
		{
			return this.getOwner().getName() + " (Vendor has " + this.inventory.getMoney().ToString() + " Gold)";
		}
		return "Trading (Vendor has " + this.inventory.getMoney().ToString() + " Gold)";
	}

	// Token: 0x06001167 RID: 4455 RVA: 0x0004DE4A File Offset: 0x0004C04A
	public void clearStoreUpkeep()
	{
		this.customers = null;
		this.customerInventory = null;
	}

	// Token: 0x06001168 RID: 4456 RVA: 0x0004DE5C File Offset: 0x0004C05C
	public string printInfoString()
	{
		string text = "";
		text = text + TextTools.formateNameValuePairSoft("Gold", this.inventory.getMoney()) + "\n";
		text = text + TextTools.formateNameValuePairSoft("Sell mult.", this.getBuyMultiplier().ToString("0.00")) + "\n";
		text = text + TextTools.formateNameValuePairSoft("Buy mult.", this.getSalesMultiplier().ToString("0.00")) + "\n";
		if (this.owner != null)
		{
			text = text + TextTools.formateNameValuePairSoft("Suspicion", this.owner.getThieverySuspicion()) + "\n";
		}
		if (this.doesNotRestock())
		{
			text += "\nThis merchant does not restock.";
		}
		else
		{
			text = text + "\nRestocks in " + (this.restockDay - Calendar.getDayTotal()).ToString() + " days.";
		}
		return text;
	}

	// Token: 0x06001169 RID: 4457 RVA: 0x0004DF47 File Offset: 0x0004C147
	private bool doesNotRestock()
	{
		return this.restockRate <= 0;
	}

	// Token: 0x0600116A RID: 4458 RVA: 0x0004DF55 File Offset: 0x0004C155
	public void testRestock()
	{
		if (!this.doesNotRestock() && Calendar.getDayTotal() >= this.restockDay)
		{
			this.stockStore();
		}
	}

	// Token: 0x0600116B RID: 4459 RVA: 0x0004DF72 File Offset: 0x0004C172
	public void mountUpkeep(Party customers)
	{
		this.setCustomers(customers);
		base.processString(this.mountTrigger, null);
		this.testRestock();
	}

	// Token: 0x0600116C RID: 4460 RVA: 0x0004DF90 File Offset: 0x0004C190
	public void stockStore()
	{
		this.clearStealLocked();
		base.processString(this.restockTrigger, null);
		this.inventory.deleteAllItems();
		foreach (string loadoutId in this.loadoutIdList)
		{
			GameData.applyLoadoutData(loadoutId, this.inventory);
		}
		foreach (string type in this.itemTypes)
		{
			if (this.fullStock)
			{
				this.inventory.addItem(GameData.makeStoreInventory(type, this.minRarity, this.maxRarity, this.minMagic, this.maxMagic));
			}
			else
			{
				foreach (string id in GameData.getRandomItemIdList(type, this.minRarity, this.maxRarity, this.minMagic, this.maxMagic, this.slotsPerTypes))
				{
					this.inventory.addItem(id);
				}
			}
		}
		this.inventory.setAllItemsToStoreStack();
		if (!this.doesNotRestock())
		{
			while (this.restockDay <= Calendar.getDayTotal())
			{
				this.restockDay += (ulong)((long)this.restockRate);
			}
		}
		this.inventory.addMoney(this.startingGold);
	}

	// Token: 0x0600116D RID: 4461 RVA: 0x0004E12C File Offset: 0x0004C32C
	public void setCustomers(Party party)
	{
		this.customers = party;
		this.customerInventory = this.customers.getInventory();
	}

	// Token: 0x0600116E RID: 4462 RVA: 0x0004E146 File Offset: 0x0004C346
	public Inventory getInventory()
	{
		return this.inventory;
	}

	// Token: 0x0600116F RID: 4463 RVA: 0x0004E150 File Offset: 0x0004C350
	private float getBuyMultiplier()
	{
		SKALDProjectData.Objects.StoreDataContainer.StoreData rawData = this.getRawData();
		float num = Store.priceMedian;
		if (rawData == null)
		{
			num += 1f;
		}
		else
		{
			num += rawData.valueMultiplier / 2f;
		}
		num -= this.getCustomerReactionScore();
		if (num < Store.priceMedian)
		{
			num = Store.priceMedian;
		}
		return num;
	}

	// Token: 0x06001170 RID: 4464 RVA: 0x0004E1A0 File Offset: 0x0004C3A0
	private float getSalesMultiplier()
	{
		SKALDProjectData.Objects.StoreDataContainer.StoreData rawData = this.getRawData();
		float num = Store.priceMedian;
		if (rawData != null)
		{
			num *= rawData.buyMultiplier + this.getCustomerReactionScore();
		}
		if (num > Store.priceMedian)
		{
			num = Store.priceMedian;
		}
		return num;
	}

	// Token: 0x06001171 RID: 4465 RVA: 0x0004E1DC File Offset: 0x0004C3DC
	private float getCustomerReactionScore()
	{
		if (this.customers == null)
		{
			return 0f;
		}
		int num = this.customers.getReactionScore();
		if (this.owner != null)
		{
			num -= this.owner.getThieverySuspicion();
		}
		if (num <= 0)
		{
			return 0f;
		}
		return (float)(num * 4) / 100f;
	}

	// Token: 0x06001172 RID: 4466 RVA: 0x0004E230 File Offset: 0x0004C430
	private int getBuyPrice(Item item)
	{
		float num = (float)item.getValue() * this.getBuyMultiplier();
		if (num < 1f)
		{
			num = 1f;
		}
		return Mathf.RoundToInt(num);
	}

	// Token: 0x06001173 RID: 4467 RVA: 0x0004E260 File Offset: 0x0004C460
	private int getSalePrice(Item item)
	{
		float num = (float)item.getValue();
		if (num != 0f)
		{
			num *= this.getSalesMultiplier();
			if (num < 1f)
			{
				num = 1f;
			}
		}
		return Mathf.RoundToInt(num);
	}

	// Token: 0x06001174 RID: 4468 RVA: 0x0004E29C File Offset: 0x0004C49C
	public string getSalesOffer()
	{
		string result;
		try
		{
			if (this.customerInventory.getCurrentObject().isStolen() && !this.fence)
			{
				result = "This merchant will not buy stolen goods.";
			}
			else if (!this.customerInventory.getCurrentObject().canBeTraded())
			{
				result = "This item can't be traded.";
			}
			else
			{
				result = "Vendor will pay you " + this.getColorTag() + this.getSalePrice(this.customerInventory.getCurrentObject()).ToString() + " GP</color> for this.";
			}
		}
		catch (Exception obj)
		{
			MainControl.logError(obj);
			result = "";
		}
		return result;
	}

	// Token: 0x06001175 RID: 4469 RVA: 0x0004E334 File Offset: 0x0004C534
	public string getBuyOffer()
	{
		string result;
		try
		{
			if (!this.inventory.getCurrentObject().canBeTraded())
			{
				result = "This item is not for sale. Gold, equipped items and quest items cannot be traded.";
			}
			else
			{
				result = "Vendor wants " + this.getColorTag() + this.getBuyPrice(this.inventory.getCurrentObject()).ToString() + " GP</color> for this.";
			}
		}
		catch (Exception obj)
		{
			MainControl.logError(obj);
			result = "";
		}
		return result;
	}

	// Token: 0x06001176 RID: 4470 RVA: 0x0004E3AC File Offset: 0x0004C5AC
	public string getCurrentItemDesc()
	{
		if (this.inventory.isEmpty())
		{
			return "";
		}
		if (this.customers == null)
		{
			return "";
		}
		if (this.customers.getCurrentCharacter() == null)
		{
			return "";
		}
		return this.inventory.printComparativeItemStats(this.customers.getCurrentCharacter()) + "\n\n" + this.getBuyOffer();
	}

	// Token: 0x06001177 RID: 4471 RVA: 0x0004E414 File Offset: 0x0004C614
	public SkaldActionResult buyItem()
	{
		Item currentObject = this.inventory.getCurrentObject();
		if (currentObject == null)
		{
			return new SkaldActionResult(false, false, "", "No items in inventory.", true);
		}
		if (!currentObject.canBeTraded())
		{
			return this.getCannotSellPrompt();
		}
		int buyPrice = this.getBuyPrice(currentObject);
		if (this.customerInventory.testMoneyGT(buyPrice - 1))
		{
			this.customerInventory.addItem(this.inventory.removeCurrentItem());
			this.customerInventory.addMoney(0 - buyPrice);
			this.inventory.addMoney(buyPrice);
			this.playBuyAudio();
			return new SkaldActionResult(true, true, "Purchased " + currentObject.getName(), string.Concat(new string[]
			{
				"Purchased ",
				currentObject.getName().ToUpper(),
				" for ",
				buyPrice.ToString(),
				" gold!"
			}), true);
		}
		return this.getCannotAffordPrompt();
	}

	// Token: 0x06001178 RID: 4472 RVA: 0x0004E500 File Offset: 0x0004C700
	public int getCurrentItemStealDC()
	{
		int num = 7;
		if (this.getOwner() != null)
		{
			num = this.getOwner().getStaticThieveryAwareness();
		}
		Item currentObject = this.inventory.getCurrentObject();
		if (currentObject != null)
		{
			num += currentObject.getStealDC();
		}
		return num;
	}

	// Token: 0x06001179 RID: 4473 RVA: 0x0004E53C File Offset: 0x0004C73C
	public SkaldActionResult stealItem()
	{
		Item currentObject = this.inventory.getCurrentObject();
		if (currentObject == null)
		{
			return new SkaldActionResult(false, false, "", "No items in inventory.", true);
		}
		Item item = this.inventory.removeCurrentItem();
		item.setStolen(true);
		this.customerInventory.addItem(item);
		int suspicionIncreaseIfStolen = currentObject.getSuspicionIncreaseIfStolen();
		if (this.getOwner() != null)
		{
			this.getOwner().increaseThieverySuspicion(suspicionIncreaseIfStolen);
		}
		this.playBuyAudio();
		string text = string.Concat(new string[]
		{
			this.customers.getCurrentCharacter().getName(),
			" stole ",
			currentObject.getName(),
			". Vendor's Suspicion increased by ",
			suspicionIncreaseIfStolen.ToString(),
			"."
		});
		return new SkaldActionResult(true, true, text, text, true);
	}

	// Token: 0x0600117A RID: 4474 RVA: 0x0004E600 File Offset: 0x0004C800
	public string getCurrentItemName()
	{
		Item currentObject = this.inventory.getCurrentObject();
		if (currentObject == null)
		{
			return "Nothing";
		}
		return currentObject.getName();
	}

	// Token: 0x0600117B RID: 4475 RVA: 0x0004E628 File Offset: 0x0004C828
	public SkaldActionResult sellStack()
	{
		Item currentObject = this.customerInventory.getCurrentObject();
		if (!currentObject.canBeTraded())
		{
			return this.getCannotSellPrompt();
		}
		if (currentObject.isStolen() && !this.fence)
		{
			return this.getNoFencePrompt();
		}
		if (currentObject.getCount() <= 1)
		{
			return this.sellItem();
		}
		int num = this.getSalePrice(currentObject) * currentObject.getCount();
		if (this.inventory.getMoney() < num)
		{
			return this.getMerchantCannotAffordPrompt(num);
		}
		string name = currentObject.getName();
		string verboseResultString = string.Concat(new string[]
		{
			"Sold ",
			name,
			"(",
			currentObject.getCount().ToString(),
			") for ",
			num.ToString(),
			" Gold!"
		});
		this.inventory.addItem(this.customerInventory.removeCurrentItemStack());
		this.customerInventory.addMoney(num);
		this.inventory.addMoney(0 - num);
		this.playSellAudio();
		return new SkaldActionResult(true, true, "Sold for " + num.ToString(), verboseResultString, true);
	}

	// Token: 0x0600117C RID: 4476 RVA: 0x0004E744 File Offset: 0x0004C944
	public SkaldActionResult sellItem()
	{
		Item currentObject = this.customerInventory.getCurrentObject();
		if (!currentObject.canBeTraded())
		{
			return this.getCannotSellPrompt();
		}
		if (currentObject.isStolen() && !this.fence)
		{
			return this.getNoFencePrompt();
		}
		int salePrice = this.getSalePrice(currentObject);
		if (this.inventory.getMoney() < salePrice)
		{
			return this.getMerchantCannotAffordPrompt(salePrice);
		}
		string name = currentObject.getName();
		string text = string.Concat(new string[]
		{
			"Sold ",
			name,
			" for ",
			salePrice.ToString(),
			" Gold!"
		});
		if (currentObject.getCount() > 1)
		{
			text = string.Concat(new string[]
			{
				text,
				" ",
				(currentObject.getCount() - 1).ToString(),
				" ",
				name,
				" remaining."
			});
		}
		this.inventory.addItem(this.customerInventory.removeCurrentItem());
		this.customerInventory.addMoney(salePrice);
		this.inventory.addMoney(0 - salePrice);
		this.playSellAudio();
		return new SkaldActionResult(true, true, "Sold for " + salePrice.ToString(), text, true);
	}

	// Token: 0x0600117D RID: 4477 RVA: 0x0004E876 File Offset: 0x0004CA76
	private void playSellAudio()
	{
		AudioControl.playCoinSound();
	}

	// Token: 0x0600117E RID: 4478 RVA: 0x0004E87D File Offset: 0x0004CA7D
	private void playBuyAudio()
	{
		AudioControl.playCoinsOutSound();
	}

	// Token: 0x0600117F RID: 4479 RVA: 0x0004E884 File Offset: 0x0004CA84
	private SkaldActionResult getCannotSellPrompt()
	{
		return this.createPrompt("This item cannot be sold. Equipped items and quest items cannot be traded.");
	}

	// Token: 0x06001180 RID: 4480 RVA: 0x0004E891 File Offset: 0x0004CA91
	private SkaldActionResult getNoFencePrompt()
	{
		return this.createPrompt("This merchant will not buy stolen goods.");
	}

	// Token: 0x06001181 RID: 4481 RVA: 0x0004E89E File Offset: 0x0004CA9E
	private SkaldActionResult getCannotAffordPrompt()
	{
		return this.createPrompt("You cannot afford this item.");
	}

	// Token: 0x06001182 RID: 4482 RVA: 0x0004E8AC File Offset: 0x0004CAAC
	private SkaldActionResult getMerchantCannotAffordPrompt(int itemWorth)
	{
		return this.createPrompt(string.Concat(new string[]
		{
			"Vendor cannot afford this item.\n\n",
			this.inventory.getMoney().ToString(),
			" Gold remaining - Item is worth ",
			itemWorth.ToString(),
			"."
		}));
	}

	// Token: 0x06001183 RID: 4483 RVA: 0x0004E902 File Offset: 0x0004CB02
	private SkaldActionResult createPrompt(string input)
	{
		PopUpControl.addPopUpOK(input);
		return new SkaldActionResult(false, false, "", input, true);
	}

	// Token: 0x06001184 RID: 4484 RVA: 0x0004E918 File Offset: 0x0004CB18
	internal List<string> getServiceList()
	{
		List<string> list = new List<string>();
		SKALDProjectData.Objects.StoreDataContainer.StoreData rawData = this.getRawData();
		if (rawData == null)
		{
			return list;
		}
		if (rawData.serviceHeal)
		{
			list.Add(Store.services.Heal.ToString());
		}
		if (rawData.serviceIdentify)
		{
			list.Add(Store.services.Identify.ToString());
		}
		if (rawData.serviceRest)
		{
			list.Add(Store.services.Rest.ToString());
		}
		if (rawData.serviceCarouse)
		{
			list.Add(Store.services.Carouse.ToString());
		}
		if (rawData.serviceRepair)
		{
			list.Add(Store.services.Repair.ToString());
		}
		return list;
	}

	// Token: 0x06001185 RID: 4485 RVA: 0x0004E9CC File Offset: 0x0004CBCC
	public void applyService(int serviceIndex)
	{
		string a = this.getServiceList()[serviceIndex];
		if (a == Store.services.Heal.ToString())
		{
			PopUpControl.addPopUpHeal(this.getBuyMultiplier());
			return;
		}
		if (a == Store.services.Rest.ToString())
		{
			PopUpControl.addPopUpRentRoom(this.getBuyMultiplier());
		}
	}

	// Token: 0x0400040C RID: 1036
	private Inventory inventory = new Inventory();

	// Token: 0x0400040D RID: 1037
	private Inventory customerInventory;

	// Token: 0x0400040E RID: 1038
	private Party customers;

	// Token: 0x0400040F RID: 1039
	private List<string> itemTypes;

	// Token: 0x04000410 RID: 1040
	private string restockTrigger = "";

	// Token: 0x04000411 RID: 1041
	private string mountTrigger = "";

	// Token: 0x04000412 RID: 1042
	private List<string> loadoutIdList = new List<string>();

	// Token: 0x04000413 RID: 1043
	private int minRarity;

	// Token: 0x04000414 RID: 1044
	private int maxRarity = 3;

	// Token: 0x04000415 RID: 1045
	private int minMagic;

	// Token: 0x04000416 RID: 1046
	private int maxMagic;

	// Token: 0x04000417 RID: 1047
	private ulong restockDay;

	// Token: 0x04000418 RID: 1048
	private int startingGold = 1000;

	// Token: 0x04000419 RID: 1049
	private bool fence;

	// Token: 0x0400041A RID: 1050
	private bool fullStock;

	// Token: 0x0400041B RID: 1051
	private int slotsPerTypes = 5;

	// Token: 0x0400041C RID: 1052
	private int restockRate;

	// Token: 0x0400041D RID: 1053
	private Character owner;

	// Token: 0x0400041E RID: 1054
	private static float priceMedian = 0.5f;

	// Token: 0x02000260 RID: 608
	private enum services
	{
		// Token: 0x04000967 RID: 2407
		Heal,
		// Token: 0x04000968 RID: 2408
		Rest,
		// Token: 0x04000969 RID: 2409
		Repair,
		// Token: 0x0400096A RID: 2410
		Carouse,
		// Token: 0x0400096B RID: 2411
		Identify
	}
}
