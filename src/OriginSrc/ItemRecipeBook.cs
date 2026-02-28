using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

// Token: 0x020000D2 RID: 210
[Serializable]
public class ItemRecipeBook : ItemBook, ISerializable
{
	// Token: 0x06000CCF RID: 3279 RVA: 0x0003B187 File Offset: 0x00039387
	public ItemRecipeBook(SKALDProjectData.ItemDataContainers.RecipeContainer.Recipe rawData) : base(rawData)
	{
	}

	// Token: 0x06000CD0 RID: 3280 RVA: 0x0003B190 File Offset: 0x00039390
	public ItemRecipeBook(SerializationInfo info, StreamingContext context)
	{
		this.dynamicData = (Item.ItemSaveData)info.GetValue("saveData", typeof(Item.ItemSaveData));
		if (this.dynamicData == null)
		{
			MainControl.logError("Book could not load dynamic data!");
		}
		this.applySaveData(this.dynamicData);
	}

	// Token: 0x06000CD1 RID: 3281 RVA: 0x0003B1E1 File Offset: 0x000393E1
	public new void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("saveData", this.dynamicData, typeof(Item.ItemSaveData));
	}

	// Token: 0x06000CD2 RID: 3282 RVA: 0x0003B200 File Offset: 0x00039400
	public SKALDProjectData.RecipeContainers.Recipe getRecipeRawData()
	{
		SKALDProjectData.ItemDataContainers.RecipeContainer.Recipe rawData = this.getRawData();
		if (rawData == null)
		{
			return null;
		}
		return GameData.getRecipeRawData(rawData.recipe);
	}

	// Token: 0x06000CD3 RID: 3283 RVA: 0x0003B224 File Offset: 0x00039424
	public new SKALDProjectData.ItemDataContainers.RecipeContainer.Recipe getRawData()
	{
		SKALDProjectData.ItemDataContainers.ItemData rawData = base.getRawData();
		if (rawData is SKALDProjectData.ItemDataContainers.RecipeContainer.Recipe)
		{
			return rawData as SKALDProjectData.ItemDataContainers.RecipeContainer.Recipe;
		}
		return null;
	}

	// Token: 0x06000CD4 RID: 3284 RVA: 0x0003B248 File Offset: 0x00039448
	public override bool isSellable()
	{
		return true;
	}

	// Token: 0x06000CD5 RID: 3285 RVA: 0x0003B24C File Offset: 0x0003944C
	public override string getName()
	{
		SKALDProjectData.ItemDataContainers.ItemData yieldItemRawData = this.getYieldItemRawData();
		if (yieldItemRawData == null)
		{
			return "Recipe";
		}
		return "Recipe: " + yieldItemRawData.title;
	}

	// Token: 0x06000CD6 RID: 3286 RVA: 0x0003B27C File Offset: 0x0003947C
	public override string getDescription()
	{
		SKALDProjectData.ItemDataContainers.RecipeContainer.Recipe rawData = this.getRawData();
		if (rawData != null && rawData.description != "")
		{
			return rawData.description;
		}
		SKALDProjectData.ItemDataContainers.ItemData yieldItemRawData = this.getYieldItemRawData();
		if (yieldItemRawData == null)
		{
			return "A recipe.";
		}
		return "A recipe for " + yieldItemRawData.title;
	}

	// Token: 0x06000CD7 RID: 3287 RVA: 0x0003B2CC File Offset: 0x000394CC
	public override List<string> getContent()
	{
		bool rawData = this.getRawData() != null;
		SKALDProjectData.RecipeContainers.Recipe recipeRawData = this.getRecipeRawData();
		SKALDProjectData.ItemDataContainers.ItemData yieldItemRawData = this.getYieldItemRawData();
		if (!rawData || recipeRawData == null || yieldItemRawData == null)
		{
			return new List<string>
			{
				""
			};
		}
		string text = yieldItemRawData.title.ToUpper() + "\n\nThis recipe requires the following ingredients:\n\n";
		foreach (string id in recipeRawData.componentList)
		{
			text = text + this.getComponentNameAndCountFromId(id, 1) + "\n";
		}
		List<string> multiComponentList = recipeRawData.multiComponentList;
		int multiNumber = recipeRawData.multiNumber;
		foreach (string id2 in multiComponentList)
		{
			text = text + this.getComponentNameAndCountFromId(id2, multiNumber) + "\n";
		}
		text += "\n\n[You have now learned how to make this recipe.]";
		return new List<string>
		{
			text
		};
	}

	// Token: 0x06000CD8 RID: 3288 RVA: 0x0003B3E8 File Offset: 0x000395E8
	private string getComponentNameAndCountFromId(string id, int count)
	{
		return count.ToString() + " x " + GameData.getItemRawData(id).title;
	}

	// Token: 0x06000CD9 RID: 3289 RVA: 0x0003B408 File Offset: 0x00039608
	public override string getImagePath()
	{
		SKALDProjectData.RecipeContainers.Recipe recipeRawData = this.getRecipeRawData();
		if (recipeRawData == null || recipeRawData.craftingType.Count == 0)
		{
			return "BookRecipe1";
		}
		if (recipeRawData.craftingType[0] == "Kitchen")
		{
			return "BookRecipe1";
		}
		if (recipeRawData.craftingType[0] == "Alchemist")
		{
			return "BookRecipe2";
		}
		return "BookRecipe1";
	}

	// Token: 0x06000CDA RID: 3290 RVA: 0x0003B473 File Offset: 0x00039673
	public override float getWeight()
	{
		return 1f;
	}

	// Token: 0x06000CDB RID: 3291 RVA: 0x0003B47A File Offset: 0x0003967A
	public override int getStoreStack()
	{
		return 1;
	}

	// Token: 0x06000CDC RID: 3292 RVA: 0x0003B480 File Offset: 0x00039680
	private SKALDProjectData.ItemDataContainers.ItemData getYieldItemRawData()
	{
		SKALDProjectData.RecipeContainers.Recipe recipeRawData = this.getRecipeRawData();
		if (recipeRawData == null)
		{
			return null;
		}
		List<string> yields = recipeRawData.yields;
		if (yields != null && yields.Count > 0)
		{
			return GameData.getItemRawData(yields[0]);
		}
		return null;
	}

	// Token: 0x06000CDD RID: 3293 RVA: 0x0003B4BC File Offset: 0x000396BC
	public override SkaldActionResult useItem(Character user)
	{
		SKALDProjectData.ItemDataContainers.RecipeContainer.Recipe rawData = this.getRawData();
		if (rawData != null && rawData.recipe != "")
		{
			MainControl.getDataControl().learnRecipe(rawData.recipe);
		}
		return base.useItem(user);
	}
}
