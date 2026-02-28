using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000DB RID: 219
public class LoadoutBuilder
{
	// Token: 0x06000D3A RID: 3386 RVA: 0x0003C720 File Offset: 0x0003A920
	public LoadoutBuilder(SKALDProjectData.LoadoutDataContainers.LoadoutDataContainer.LoadoutData data, Inventory inventory)
	{
		if (data == null || inventory == null)
		{
			return;
		}
		this.data = data;
		this.inventory = inventory;
		foreach (BaseDataObject baseDataObject in data.getBaseList())
		{
			SKALDProjectData.LoadoutDataContainers.LoadoutDataContainer.LoadoutData.LoadoutEntry loadoutEntry = (SKALDProjectData.LoadoutDataContainers.LoadoutDataContainer.LoadoutData.LoadoutEntry)baseDataObject;
			if (MainControl.isDeluxeEdition() || !loadoutEntry.deluxeEdition)
			{
				this.dynamicEntries.Add(new LoadoutBuilder.DynamicEntry(loadoutEntry, data.id));
				this.setEntries.Add(new LoadoutBuilder.SetEntry(loadoutEntry, data.id));
			}
		}
		this.addSetItems();
		this.addDynamicItems();
		this.addMoney();
	}

	// Token: 0x06000D3B RID: 3387 RVA: 0x0003C7F4 File Offset: 0x0003A9F4
	private void addSetItems()
	{
		foreach (LoadoutBuilder.SetEntry setEntry in this.setEntries)
		{
			setEntry.addItems(this.inventory);
		}
	}

	// Token: 0x06000D3C RID: 3388 RVA: 0x0003C84C File Offset: 0x0003AA4C
	private void addDynamicItems()
	{
		foreach (LoadoutBuilder.DynamicEntry dynamicEntry in this.dynamicEntries)
		{
			dynamicEntry.addItems(this.inventory);
		}
	}

	// Token: 0x06000D3D RID: 3389 RVA: 0x0003C8A4 File Offset: 0x0003AAA4
	private void addMoney()
	{
		this.inventory.addMoney(Random.Range(this.data.minGold, this.data.maxGold));
	}

	// Token: 0x04000324 RID: 804
	private SKALDProjectData.LoadoutDataContainers.LoadoutDataContainer.LoadoutData data;

	// Token: 0x04000325 RID: 805
	private Inventory inventory;

	// Token: 0x04000326 RID: 806
	private List<LoadoutBuilder.DynamicEntry> dynamicEntries = new List<LoadoutBuilder.DynamicEntry>();

	// Token: 0x04000327 RID: 807
	private List<LoadoutBuilder.SetEntry> setEntries = new List<LoadoutBuilder.SetEntry>();

	// Token: 0x02000245 RID: 581
	private class DynamicEntry : LoadoutBuilder.Entry
	{
		// Token: 0x06001932 RID: 6450 RVA: 0x0006E420 File Offset: 0x0006C620
		public DynamicEntry(SKALDProjectData.LoadoutDataContainers.LoadoutDataContainer.LoadoutData.LoadoutEntry data, string ownerId) : base(data.dynamicChance, data.dynamicMinNumber, data.dynamicMaxNumber, ownerId)
		{
			this.minRarity = data.minRarity;
			this.maxRarity = data.maxRarity;
			this.minMagic = data.minMagic;
			this.maxMagic = data.maxMagic;
			this.types = data.itemTypesList;
			this.randomize = data.randomizeDynamicItems;
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x0006E49C File Offset: 0x0006C69C
		public override void addItems(Inventory targetInventory)
		{
			if (!base.testChance())
			{
				return;
			}
			if (this.types.Count == 0)
			{
				return;
			}
			if (this.randomize)
			{
				for (int i = 0; i < base.getNumberOfIterations(); i++)
				{
					foreach (string itemId in GameData.getRandomItemIdList(this.types[Random.Range(0, this.types.Count)], this.minRarity, this.maxRarity, this.minMagic, this.maxMagic, 1))
					{
						base.addItem(targetInventory, itemId);
					}
				}
				return;
			}
			List<string> randomItemIdList = GameData.getRandomItemIdList(this.types[Random.Range(0, this.types.Count)], this.minRarity, this.maxRarity, this.minMagic, this.maxMagic, 1);
			for (int j = 0; j < base.getNumberOfIterations(); j++)
			{
				foreach (string itemId2 in randomItemIdList)
				{
					base.addItem(targetInventory, itemId2);
				}
			}
		}

		// Token: 0x040008D8 RID: 2264
		private List<string> types;

		// Token: 0x040008D9 RID: 2265
		private int minRarity = 1;

		// Token: 0x040008DA RID: 2266
		private int maxRarity = 1;

		// Token: 0x040008DB RID: 2267
		private int minMagic;

		// Token: 0x040008DC RID: 2268
		private int maxMagic;
	}

	// Token: 0x02000246 RID: 582
	private class SetEntry : LoadoutBuilder.Entry
	{
		// Token: 0x06001934 RID: 6452 RVA: 0x0006E5E8 File Offset: 0x0006C7E8
		public SetEntry(SKALDProjectData.LoadoutDataContainers.LoadoutDataContainer.LoadoutData.LoadoutEntry data, string ownerId) : base(data.setChance, data.setMinNumber, data.setMaxNumber, ownerId)
		{
			this.itemIds = data.setItemsList;
			this.altItemIds = data.altSetItemsList;
			this.randomize = data.randomizeSetItems;
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x0006E628 File Offset: 0x0006C828
		public override void addItems(Inventory targetInventory)
		{
			List<string> list = this.itemIds;
			if (!base.testChance())
			{
				list = this.altItemIds;
			}
			if (list.Count == 0)
			{
				return;
			}
			if (this.randomize)
			{
				string itemId = list[Random.Range(0, list.Count)];
				int numberOfIterations = base.getNumberOfIterations();
				for (int i = 0; i < numberOfIterations; i++)
				{
					base.addItem(targetInventory, itemId);
				}
				return;
			}
			foreach (string itemId2 in list)
			{
				int numberOfIterations2 = base.getNumberOfIterations();
				for (int j = 0; j < numberOfIterations2; j++)
				{
					base.addItem(targetInventory, itemId2);
				}
			}
		}

		// Token: 0x040008DD RID: 2269
		private List<string> itemIds;

		// Token: 0x040008DE RID: 2270
		private List<string> altItemIds;
	}

	// Token: 0x02000247 RID: 583
	private abstract class Entry
	{
		// Token: 0x06001936 RID: 6454 RVA: 0x0006E6EC File Offset: 0x0006C8EC
		protected Entry(int chance, int minNumber, int maxNumber, string ownerId)
		{
			this.chance = chance;
			this.minNumberOfIterations = minNumber;
			this.maxNumberOfIterations = maxNumber;
			this.ownerId = ownerId;
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x0006E72A File Offset: 0x0006C92A
		protected bool testChance()
		{
			return Random.Range(0, 100) < this.chance;
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x0006E73C File Offset: 0x0006C93C
		protected int getNumberOfIterations()
		{
			return Random.Range(this.minNumberOfIterations, this.maxNumberOfIterations + 1);
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x0006E754 File Offset: 0x0006C954
		protected void addItem(Inventory targetInventory, string itemId)
		{
			Item item = GameData.instantiateItem(itemId);
			if (item != null)
			{
				targetInventory.addItem(item);
				return;
			}
			MainControl.logError("Malformed item ID " + itemId + " with loadout " + this.ownerId);
		}

		// Token: 0x0600193A RID: 6458
		public abstract void addItems(Inventory targetInventory);

		// Token: 0x040008DF RID: 2271
		protected int chance;

		// Token: 0x040008E0 RID: 2272
		protected int minNumberOfIterations = 1;

		// Token: 0x040008E1 RID: 2273
		protected int maxNumberOfIterations = 1;

		// Token: 0x040008E2 RID: 2274
		protected string ownerId = "";

		// Token: 0x040008E3 RID: 2275
		protected bool randomize;
	}
}
