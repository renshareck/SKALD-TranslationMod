using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

// Token: 0x020000B7 RID: 183
[Serializable]
public class CraftingControl
{
	// Token: 0x06000B69 RID: 2921 RVA: 0x000360C0 File Offset: 0x000342C0
	public CraftingControl()
	{
		MainControl.log("Initilaizing Crafting Control!");
		foreach (SKALDProjectData.RecipeContainers.Recipe recipeData in GameData.getAllRecipes())
		{
			this.recipes.add(new CraftingControl.Recipe(recipeData));
		}
		MainControl.log("Finished Crafting Control!");
	}

	// Token: 0x06000B6A RID: 2922 RVA: 0x00036150 File Offset: 0x00034350
	public void setType(string type)
	{
		this.currentType = type;
	}

	// Token: 0x06000B6B RID: 2923 RVA: 0x0003615C File Offset: 0x0003435C
	public List<Item> craft(Inventory workbenchInventory, Character currentCharacter)
	{
		foreach (SkaldBaseObject skaldBaseObject in this.recipes.getObjectList())
		{
			CraftingControl.Recipe recipe = (CraftingControl.Recipe)skaldBaseObject;
			if (recipe.canThisBeCrafted(workbenchInventory, currentCharacter))
			{
				return recipe.createYield(workbenchInventory, currentCharacter);
			}
		}
		return null;
	}

	// Token: 0x06000B6C RID: 2924 RVA: 0x000361CC File Offset: 0x000343CC
	public List<string> getRecipeList(Inventory workbenchInventory, Character currentCharacter)
	{
		List<string> list = new List<string>();
		SkaldBaseObject currentObject = this.recipes.getCurrentObject();
		foreach (CraftingControl.Recipe recipe in this.getAvailableRecipes())
		{
			string text = recipe.getListName();
			if (currentObject == recipe)
			{
				text = C64Color.YELLOW_TAG + text + "</color>";
			}
			else if (recipe.couldThisPotentiallyBeCrafted(workbenchInventory, currentCharacter))
			{
				text = C64Color.GREEN_LIGHT_TAG + text + "</color>";
			}
			list.Add(text);
		}
		return list;
	}

	// Token: 0x06000B6D RID: 2925 RVA: 0x00036274 File Offset: 0x00034474
	public bool couldThisPotentiallyBeCrafted(Inventory workbenchInventory, Character currentCharacter)
	{
		return (this.recipes.getCurrentObject() as CraftingControl.Recipe).couldThisPotentiallyBeCrafted(workbenchInventory, currentCharacter);
	}

	// Token: 0x06000B6E RID: 2926 RVA: 0x00036290 File Offset: 0x00034490
	private List<CraftingControl.Recipe> getAvailableRecipes()
	{
		if (this.currentType == "")
		{
			MainControl.logError("Type not set for crafting control!");
		}
		List<CraftingControl.Recipe> list = new List<CraftingControl.Recipe>();
		foreach (SkaldBaseObject skaldBaseObject in this.recipes.getObjectList())
		{
			CraftingControl.Recipe recipe = (CraftingControl.Recipe)skaldBaseObject;
			if (recipe.isKnown() && recipe.isTypeLegal(this.currentType))
			{
				list.Add(recipe);
			}
		}
		return list;
	}

	// Token: 0x06000B6F RID: 2927 RVA: 0x00036328 File Offset: 0x00034528
	public string getRecipesContainingIngredient(string ingredientID)
	{
		List<string> list = new List<string>();
		foreach (SkaldBaseObject skaldBaseObject in this.recipes.getObjectList())
		{
			CraftingControl.Recipe recipe = (CraftingControl.Recipe)skaldBaseObject;
			if (recipe.isIngredientUsedForThis(ingredientID))
			{
				list.Add(recipe.getName());
			}
		}
		if (list.Count == 0)
		{
			return "";
		}
		string text = "";
		foreach (string str in list)
		{
			text = text + str + ", ";
		}
		text = TextTools.removeTrailingComma(text);
		return "Used in: " + C64Color.GRAY_TAG + text + "</color>";
	}

	// Token: 0x06000B70 RID: 2928 RVA: 0x00036410 File Offset: 0x00034610
	public void setFirstKnownRecipe()
	{
		List<CraftingControl.Recipe> availableRecipes = this.getAvailableRecipes();
		if (availableRecipes.Count == 0)
		{
			return;
		}
		this.recipes.setCurrentObject(availableRecipes[0]);
	}

	// Token: 0x06000B71 RID: 2929 RVA: 0x0003643F File Offset: 0x0003463F
	public bool areAnyRecipesKnown()
	{
		return this.getAvailableRecipes().Count > 0;
	}

	// Token: 0x06000B72 RID: 2930 RVA: 0x0003644F File Offset: 0x0003464F
	public bool isAKnownRecipeAvailable()
	{
		return this.recipes.getCount() != 0 && this.getAvailableRecipes().Contains(this.recipes.getCurrentObject() as CraftingControl.Recipe);
	}

	// Token: 0x06000B73 RID: 2931 RVA: 0x0003647B File Offset: 0x0003467B
	public string getCurrentRecipeFullDescription(Inventory workbenchInventory, Character currentCharacter)
	{
		if (this.recipes.getCurrentObject() != null)
		{
			return (this.recipes.getCurrentObject() as CraftingControl.Recipe).printRecipe(workbenchInventory, currentCharacter);
		}
		return "";
	}

	// Token: 0x06000B74 RID: 2932 RVA: 0x000364A8 File Offset: 0x000346A8
	public void learnRecipe(string id)
	{
		try
		{
			(this.recipes.getObject(id) as CraftingControl.Recipe).makeKnown();
		}
		catch (Exception obj)
		{
			MainControl.logError(obj);
		}
	}

	// Token: 0x06000B75 RID: 2933 RVA: 0x000364E4 File Offset: 0x000346E4
	public void setEntryByIndex(int index)
	{
		try
		{
			this.recipes.setCurrentObject(this.getAvailableRecipes()[index]);
		}
		catch (Exception obj)
		{
			MainControl.logError(obj);
		}
	}

	// Token: 0x06000B76 RID: 2934 RVA: 0x00036524 File Offset: 0x00034724
	public void transferItemsFromPartyToWorkbench(Inventory workbenchInventory, Character currentCharacter)
	{
		(this.recipes.getCurrentObject() as CraftingControl.Recipe).transferItemsFromPartyToWorkbench(workbenchInventory, currentCharacter);
	}

	// Token: 0x0400030A RID: 778
	private SkaldObjectList recipes = new SkaldObjectList();

	// Token: 0x0400030B RID: 779
	private string currentType = "";

	// Token: 0x02000240 RID: 576
	[Serializable]
	private class Recipe : SkaldBaseObject, ISerializable
	{
		// Token: 0x06001908 RID: 6408 RVA: 0x0006D7E3 File Offset: 0x0006B9E3
		public Recipe(SKALDProjectData.RecipeContainers.Recipe recipeData)
		{
			this.saveData.id = recipeData.id;
			this.saveData.known = recipeData.alwaysAvailable;
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x0006D818 File Offset: 0x0006BA18
		public Recipe(SerializationInfo info, StreamingContext context)
		{
			this.saveData = (CraftingControl.Recipe.SaveData)info.GetValue("saveData", typeof(CraftingControl.Recipe.SaveData));
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x0006D84B File Offset: 0x0006BA4B
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("saveData", this.saveData, typeof(CraftingControl.Recipe.SaveData));
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x0006D868 File Offset: 0x0006BA68
		private SKALDProjectData.RecipeContainers.Recipe getRawData()
		{
			return GameData.getRecipeRawData(this.getId());
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x0006D878 File Offset: 0x0006BA78
		private SKALDProjectData.ItemDataContainers.ItemData getYieldItemRawData()
		{
			List<string> yields = this.getRawData().yields;
			if (yields != null && yields.Count > 0)
			{
				return GameData.getItemRawData(yields[0]);
			}
			return null;
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x0006D8AB File Offset: 0x0006BAAB
		public override string getName()
		{
			if (this.getRawData().title != "")
			{
				return this.getRawData().title;
			}
			if (this.getYieldItemRawData() != null)
			{
				return this.getYieldItemRawData().title;
			}
			return "A Recipe";
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x0006D8EC File Offset: 0x0006BAEC
		public void transferItemsFromPartyToWorkbench(Inventory workbenchInventory, Character currentCharacter)
		{
			Inventory inventory = currentCharacter.getInventory();
			List<string> componentList = this.getRawData().componentList;
			List<string> multiComponentList = this.getRawData().multiComponentList;
			foreach (string id in componentList)
			{
				if (inventory.containsObject(id))
				{
					inventory.setCurrentObject(inventory.getObject(id));
					workbenchInventory.addItem(inventory.removeCurrentItemStack());
				}
			}
			foreach (string id2 in multiComponentList)
			{
				if (inventory.containsObject(id2))
				{
					inventory.setCurrentObject(inventory.getObject(id2));
					workbenchInventory.addItem(inventory.removeCurrentItemStack());
				}
			}
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x0006D9D0 File Offset: 0x0006BBD0
		public bool couldThisPotentiallyBeCrafted(Inventory workbenchInventory, Character currentCharacter)
		{
			foreach (string id in this.getRawData().componentList)
			{
				if (!workbenchInventory.containsObject(id) && !currentCharacter.getInventory().containsObject(id))
				{
					return false;
				}
			}
			List<string> multiComponentList = this.getRawData().multiComponentList;
			int multiNumber = this.getRawData().multiNumber;
			foreach (string item in multiComponentList)
			{
				if (workbenchInventory.getItemCount(item) < multiNumber && currentCharacter.getInventory().getItemCount(item) < multiNumber)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x0006DAAC File Offset: 0x0006BCAC
		public bool canThisBeCrafted(Inventory workbenchInventory, Character currentCharacter)
		{
			List<string> componentList = this.getRawData().componentList;
			foreach (string id in componentList)
			{
				if (!workbenchInventory.containsObject(id))
				{
					return false;
				}
			}
			List<string> multiComponentList = this.getRawData().multiComponentList;
			int multiNumber = this.getRawData().multiNumber;
			foreach (string item in multiComponentList)
			{
				if (workbenchInventory.getItemCount(item) < multiNumber)
				{
					return false;
				}
			}
			foreach (SkaldBaseObject skaldBaseObject in workbenchInventory.getObjectList())
			{
				if (!componentList.Contains(skaldBaseObject.getId()) && !multiComponentList.Contains(skaldBaseObject.getId()))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x0006DBD8 File Offset: 0x0006BDD8
		public List<Item> createYield(Inventory workbenchInventory, Character creator)
		{
			this.makeKnown();
			List<Item> list = new List<Item>();
			if (this.getYieldItemRawData() != null)
			{
				foreach (string id in this.getFlatIngredientsList())
				{
					workbenchInventory.deleteItem(id);
				}
				SkaldTestBase skaldTestBase = new SkaldTestRandomVsStatic(creator, AttributesControl.CoreAttributes.ATT_Crafting, this.getRawData().difficulty, 1);
				int num = this.getRawData().numberYielded;
				if (skaldTestBase.wasSuccess())
				{
					num++;
				}
				Item item = GameData.instantiateItem(this.getYieldItemRawData().id);
				if (item.isStackable())
				{
					item.setCount(num);
					list.Add(item);
				}
				else
				{
					list.Add(item);
					for (int i = 0; i < num; i++)
					{
						list.Add(GameData.instantiateItem(this.getYieldItemRawData().id));
					}
				}
			}
			return list;
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x0006DCC8 File Offset: 0x0006BEC8
		public void makeKnown()
		{
			if (this.isKnown())
			{
				return;
			}
			this.saveData.known = true;
			HoverElementControl.addHoverText("New Recipe", this.getName());
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x0006DCEF File Offset: 0x0006BEEF
		public bool isKnown()
		{
			return this.saveData.known;
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x0006DCFC File Offset: 0x0006BEFC
		public bool isTypeLegal(string type)
		{
			SKALDProjectData.RecipeContainers.Recipe rawData = this.getRawData();
			return rawData != null && rawData.craftingType.Contains(type);
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x0006DD24 File Offset: 0x0006BF24
		private List<string> getFlatIngredientsList()
		{
			List<string> list = new List<string>();
			foreach (string item in this.getRawData().componentList)
			{
				list.Add(item);
			}
			List<string> multiComponentList = this.getRawData().multiComponentList;
			int multiNumber = this.getRawData().multiNumber;
			foreach (string item2 in multiComponentList)
			{
				for (int i = 0; i < multiNumber; i++)
				{
					list.Add(item2);
				}
			}
			return list;
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x0006DDE8 File Offset: 0x0006BFE8
		public override string getId()
		{
			return this.saveData.id;
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x0006DDF5 File Offset: 0x0006BFF5
		public string printRecipe(Inventory workbenchInventory, Character currentCharacter)
		{
			return TextTools.formatFullDesription(this.getName() + " Recipe", this.getDescription(workbenchInventory, currentCharacter));
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x0006DE14 File Offset: 0x0006C014
		public string getDescription(Inventory workbenchInventory, Character currentCharacter)
		{
			if (!this.saveData.known)
			{
				return "An unknown recipe.";
			}
			string text;
			if (this.getYieldItemRawData() != null)
			{
				text = "A recipe for crafting " + this.getYieldItemRawData().title + ".";
			}
			else
			{
				text = "A recipe.";
			}
			text = text + "\n\n" + this.printIngredients(workbenchInventory, currentCharacter);
			return string.Concat(new string[]
			{
				text,
				"\n\n",
				currentCharacter.getName(),
				" has a Crafting skill of ",
				C64Color.CYAN_TAG,
				"2d6 + ",
				currentCharacter.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_Crafting).ToString(),
				"</color> versus Difficulty ",
				C64Color.WHITE_TAG,
				this.getRawData().difficulty.ToString(),
				"</color> to produce a larger amount."
			});
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x0006DEF4 File Offset: 0x0006C0F4
		public bool isIngredientUsedForThis(string ingredientID)
		{
			return this.getRawData() != null && (this.getRawData().componentList.Contains(ingredientID) || this.getRawData().multiComponentList.Contains(ingredientID));
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x0006DF2C File Offset: 0x0006C12C
		public string printIngredients(Inventory workbenchInventory, Character currentCharacter)
		{
			string text = "Ingredients:\n\n";
			foreach (string id in this.getRawData().componentList)
			{
				text = text + this.getComponentNameAndCountFromId(id, 1, currentCharacter, workbenchInventory) + "\n";
			}
			List<string> multiComponentList = this.getRawData().multiComponentList;
			int multiNumber = this.getRawData().multiNumber;
			foreach (string id2 in multiComponentList)
			{
				text = text + this.getComponentNameAndCountFromId(id2, multiNumber, currentCharacter, workbenchInventory) + "\n";
			}
			return text;
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x0006E000 File Offset: 0x0006C200
		private string getComponentNameAndCountFromId(string id, int count, Character currentCharacter, Inventory workbench)
		{
			string text = count.ToString() + " x " + GameData.getItemRawData(id).title;
			if (currentCharacter.getInventory().getItemCount(id) >= count || workbench.getItemCount(id) >= count)
			{
				text = C64Color.GREEN_LIGHT_TAG + text + "</color>";
			}
			return text;
		}

		// Token: 0x040008B7 RID: 2231
		private CraftingControl.Recipe.SaveData saveData = new CraftingControl.Recipe.SaveData();

		// Token: 0x020003CF RID: 975
		[Serializable]
		private class SaveData
		{
			// Token: 0x04000C57 RID: 3159
			public string id;

			// Token: 0x04000C58 RID: 3160
			public bool known;
		}
	}
}
