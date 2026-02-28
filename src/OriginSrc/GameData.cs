using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x0200003A RID: 58
[Serializable]
public static class GameData
{
	// Token: 0x06000748 RID: 1864 RVA: 0x0001FD79 File Offset: 0x0001DF79
	public static void loadData()
	{
		GameData.loadData("SkaldProject");
	}

	// Token: 0x06000749 RID: 1865 RVA: 0x0001FD85 File Offset: 0x0001DF85
	public static void loadDataAndMaps()
	{
		GameData.loadDataAndMaps("SkaldProject");
	}

	// Token: 0x0600074A RID: 1866 RVA: 0x0001FD91 File Offset: 0x0001DF91
	public static void loadDataAndMaps(string projecDataName)
	{
		GameData.loadData(projecDataName);
		GameData.populateMapList();
	}

	// Token: 0x0600074B RID: 1867 RVA: 0x0001FDA0 File Offset: 0x0001DFA0
	public static void validateProjectVersion(string version)
	{
		MainControl.log("VALIDATING VERSION: " + version);
		string text = "SkaldProject";
		if (version == null || version == "")
		{
			MainControl.logError("Version is blank. Setting default safe project and reloading data: " + text);
			GameData.loadDataAndMaps(text);
			return;
		}
		if (version == GameData.getVersionNumber())
		{
			MainControl.log("Version is in sync. Proceeding with latest data.");
			return;
		}
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary.Add("Pre-Launch 1.0.0", "SkaldProject");
		if (dictionary.ContainsKey(version) && dictionary[version] != GameData.getVersionNumber())
		{
			MainControl.log("Found version in dictionary. Loading path: " + dictionary[version]);
			GameData.loadDataAndMaps(dictionary[version]);
			return;
		}
		MainControl.logError("Version is not blank or in dictionary. Proceeding with latest data.");
	}

	// Token: 0x0600074C RID: 1868 RVA: 0x0001FE64 File Offset: 0x0001E064
	private static void loadData(string projecDataName)
	{
		MainControl.log("Starting to Load Game Data");
		GameData.projectStack = new List<SKALDProjectData>();
		GameData.setCustomProjectData();
		GameData.setBundledProjectData(projecDataName);
		C64Color.loadColors();
		GameData.propRawDataCache = new GameData.RawDataCache<SKALDProjectData.PropContainers.Prop>();
		GameData.itemRawDataCache = new GameData.RawDataCache<SKALDProjectData.ItemDataContainers.ItemData>();
		GameData.characterRawDataCache = new GameData.RawDataCache<SKALDProjectData.CharacterContainers.Character>();
		GameData.terrainRawDataCache = new GameData.RawDataCache<SKALDProjectData.TerrainContainers.TerrainTile>();
		GameData.classRawDataCache = new GameData.RawDataCache<SKALDProjectData.ClassContainers.ClassData>();
		GameData.raceRawDataCache = new GameData.RawDataCache<SKALDProjectData.RaceContainers.RaceData>();
		GameData.backgroundRawDataCache = new GameData.RawDataCache<SKALDProjectData.BackgroundContainers.BackgroundData>();
		GameData.recipeRawDataCache = new GameData.RawDataCache<SKALDProjectData.RecipeContainers.Recipe>();
		GameData.journalRawDataCache = new GameData.RawDataCache<SKALDProjectData.JournalContainers.JournalEntry>();
		GameData.attributeCache = new GameData.RawDataCache<SKALDProjectData.Objects.AttributeData.AttributeContainer.Attribute>();
		GameData.factionRawDataCache = new GameData.RawDataCache<SKALDProjectData.Objects.FactionDataContainer.FactionData>();
		GameData.effectCache = new GameData.RawDataCache<SKALDProjectData.EffectContainers.EffectData>();
		GameData.conditionCache = new GameData.RawDataCache<SKALDProjectData.ConditionContainers.ConditionData>();
		GameData.featCache = new GameData.RawDataCache<SKALDProjectData.FeatContainers.FeatData>();
		GameData.encounterCache = new GameData.RawDataCache<SKALDProjectData.Objects.EncounterContainer.Encounter>();
		GameData.eventCache = new GameData.RawDataCache<SKALDProjectData.Objects.EventContainer.Event>();
		GameData.hairCache = new GameData.RawDataCache<SKALDProjectData.ApperanceElementContainers.ApperanceElement>();
		GameData.beardCache = new GameData.RawDataCache<SKALDProjectData.ApperanceElementContainers.ApperanceElement>();
		GameData.portraitCache = new GameData.RawDataCache<SKALDProjectData.ApperanceElementContainers.ApperanceElement>();
		GameData.enchantmentCache = new GameData.RawDataCache<SKALDProjectData.EnchantmentContainers.Enchantment>();
		GameData.vehicleRawDataCache = new GameData.RawDataCache<SKALDProjectData.VehicleContainers.Vehicle>();
		GameData.setCharacterComponentControl();
		GameData.characterFeatureControl = new CharacterFeatureControl();
		GameData.initilizeInstanceControl();
		GameData.populateTerrainDictionary();
		GameData.createSpellTomes();
		GameData.createMagicItems();
		GameData.createRecipes();
		MainControl.log("Finished Loading Game Data");
	}

	// Token: 0x0600074D RID: 1869 RVA: 0x0001FF99 File Offset: 0x0001E199
	public static void initilizeInstanceControl()
	{
		GameData.instanceControl = new GameData.SkaldInstanceControl();
	}

	// Token: 0x0600074E RID: 1870 RVA: 0x0001FFA8 File Offset: 0x0001E1A8
	public static void setBundledProjectData(string projectDataName)
	{
		MainControl.log("Setting Bundled project: " + projectDataName);
		TextAsset textAsset = SkaldFileIO.Load<TextAsset>("Data/BuildData/" + projectDataName);
		try
		{
			GameData.coreProjectFile = JsonUtility.FromJson<SKALDProjectData>(textAsset.text);
			GameData.projectStack.Add(GameData.coreProjectFile);
		}
		catch (Exception obj)
		{
			MainControl.logError(obj);
			if (projectDataName != "SkaldProject")
			{
				GameData.loadData("SkaldProject");
			}
		}
	}

	// Token: 0x0600074F RID: 1871 RVA: 0x00020028 File Offset: 0x0001E228
	private static SKALDProjectData getFirstProjectFile()
	{
		return GameData.projectStack[0];
	}

	// Token: 0x06000750 RID: 1872 RVA: 0x00020035 File Offset: 0x0001E235
	private static SKALDProjectData getCoreProjectFile()
	{
		return GameData.coreProjectFile;
	}

	// Token: 0x06000751 RID: 1873 RVA: 0x0002003C File Offset: 0x0001E23C
	private static void setCharacterComponentControl()
	{
		GameData.characterComponentControl = new CharacterComponentControl();
		List<string> list = GameData.getAbilityRawDataIdList();
		foreach (string id in list)
		{
			GameData.getAbility(id);
		}
		list.Clear();
		list = GameData.getConditionDataIdList();
		foreach (string id2 in list)
		{
			GameData.getCondition(id2);
		}
	}

	// Token: 0x06000752 RID: 1874 RVA: 0x000200E0 File Offset: 0x0001E2E0
	private static GameData.SkaldInstanceControl getInstanceControl()
	{
		if (GameData.instanceControl == null)
		{
			GameData.instanceControl = new GameData.SkaldInstanceControl();
		}
		return GameData.instanceControl;
	}

	// Token: 0x06000753 RID: 1875 RVA: 0x000200F8 File Offset: 0x0001E2F8
	public static string printInstanceList()
	{
		return GameData.getInstanceControl().print();
	}

	// Token: 0x06000754 RID: 1876 RVA: 0x00020104 File Offset: 0x0001E304
	public static void purgeInstancesByMapId(string mapId)
	{
		GameData.getInstanceControl().purgeInstancesByMapID(mapId);
	}

	// Token: 0x06000755 RID: 1877 RVA: 0x00020111 File Offset: 0x0001E311
	public static List<SkaldWorldObject> getCharacterByMap(string mapId)
	{
		return GameData.getInstanceControl().characterList.getMemberByMap(mapId);
	}

	// Token: 0x06000756 RID: 1878 RVA: 0x00020123 File Offset: 0x0001E323
	public static List<SkaldWorldObject> getItemByMap(string mapId)
	{
		return GameData.getInstanceControl().itemList.getMemberByMap(mapId);
	}

	// Token: 0x06000757 RID: 1879 RVA: 0x00020135 File Offset: 0x0001E335
	public static List<SkaldWorldObject> getPropsByMap(string mapId)
	{
		return GameData.getInstanceControl().propList.getMemberByMap(mapId);
	}

	// Token: 0x06000758 RID: 1880 RVA: 0x00020147 File Offset: 0x0001E347
	public static List<SkaldWorldObject> getVehiclesByMap(string mapId)
	{
		if (GameData.getInstanceControl().vehicleList == null)
		{
			GameData.getInstanceControl().vehicleList = new GameData.SkaldInstanceControl.VehicleList();
		}
		return GameData.getInstanceControl().vehicleList.getMemberByMap(mapId);
	}

	// Token: 0x06000759 RID: 1881 RVA: 0x00020174 File Offset: 0x0001E374
	public static List<SkaldWorldObject> getCharactersById(string npcId)
	{
		return GameData.getInstanceControl().characterList.getMembersById(npcId);
	}

	// Token: 0x0600075A RID: 1882 RVA: 0x00020188 File Offset: 0x0001E388
	public static Prop getFirstPropByIdOnMap(string mapId, string propId)
	{
		foreach (SkaldWorldObject skaldWorldObject in GameData.getPropsByMap(mapId))
		{
			Prop prop = (Prop)skaldWorldObject;
			if (prop.isId(propId))
			{
				return prop;
			}
		}
		return null;
	}

	// Token: 0x0600075B RID: 1883 RVA: 0x000201EC File Offset: 0x0001E3EC
	public static GameData.SkaldInstanceControl.InstanceSaveData getInstanceSaveData()
	{
		return GameData.getInstanceControl().getInstanceSaveData();
	}

	// Token: 0x0600075C RID: 1884 RVA: 0x000201F8 File Offset: 0x0001E3F8
	public static void applyInstanceSaveData(GameData.SkaldInstanceControl.InstanceSaveData data)
	{
		GameData.getInstanceControl().applyInstanceSaveData(data);
	}

	// Token: 0x0600075D RID: 1885 RVA: 0x00020208 File Offset: 0x0001E408
	private static void createRecipes()
	{
		List<string> list = new List<string>();
		List<SKALDProjectData.RecipeContainers.Recipe> list2 = new List<SKALDProjectData.RecipeContainers.Recipe>();
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.recipeContainers.getBaseList())
			{
				foreach (BaseDataObject baseDataObject2 in ((SKALDProjectData.RecipeContainers.RecipeDataContainer)baseDataObject).getBaseList())
				{
					SKALDProjectData.RecipeContainers.Recipe recipe = (SKALDProjectData.RecipeContainers.Recipe)baseDataObject2;
					if (!list.Contains(recipe.id))
					{
						list.Add(recipe.id);
						list2.Add(recipe);
					}
				}
			}
		}
		GameData.getFirstProjectFile().itemContainer.recipes.list.Clear();
		foreach (SKALDProjectData.RecipeContainers.Recipe recipe2 in list2)
		{
			SKALDProjectData.ItemDataContainers.RecipeContainer.Recipe recipe3 = new SKALDProjectData.ItemDataContainers.RecipeContainer.Recipe();
			recipe3.id = "ITE_Recipe" + recipe2.id.Remove(0, 4);
			recipe3.recipe = recipe2.id;
			recipe3.rarity = 1;
			recipe3.value = 5;
			GameData.getFirstProjectFile().itemContainer.recipes.list.Add(recipe3);
		}
	}

	// Token: 0x0600075E RID: 1886 RVA: 0x000203BC File Offset: 0x0001E5BC
	private static void createSpellTomes()
	{
		List<string> list = new List<string>();
		List<SKALDProjectData.AbilityContainers.SpellContainer.SpellAbility> list2 = new List<SKALDProjectData.AbilityContainers.SpellContainer.SpellAbility>();
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.abilityContainers.spellContainer.getBaseList())
			{
				SKALDProjectData.AbilityContainers.SpellContainer.SpellAbility spellAbility = (SKALDProjectData.AbilityContainers.SpellContainer.SpellAbility)baseDataObject;
				if (!list.Contains(spellAbility.id))
				{
					list.Add(spellAbility.id);
					list2.Add(spellAbility);
				}
			}
		}
		GameData.getFirstProjectFile().itemContainer.tomes.list.Clear();
		foreach (SKALDProjectData.AbilityContainers.SpellContainer.SpellAbility spellAbility2 in list2)
		{
			SKALDProjectData.ItemDataContainers.TomeContainer.TomeData tomeData = new SKALDProjectData.ItemDataContainers.TomeContainer.TomeData();
			tomeData.id = "ITE_Tome" + spellAbility2.id.Remove(0, 4);
			tomeData.spellLearned = spellAbility2.id;
			tomeData.rarity = spellAbility2.tier;
			GameData.getFirstProjectFile().itemContainer.tomes.list.Add(tomeData);
		}
	}

	// Token: 0x0600075F RID: 1887 RVA: 0x00020530 File Offset: 0x0001E730
	private static void createMagicItems()
	{
		List<string> list = new List<string>();
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.enchantmentContainers.getBaseList())
			{
				foreach (BaseDataObject baseDataObject2 in ((SKALDProjectData.BaseContainerObject)baseDataObject).getBaseList())
				{
					SKALDProjectData.EnchantmentContainers.Enchantment enchantment = (SKALDProjectData.EnchantmentContainers.Enchantment)baseDataObject2;
					if (!list.Contains(enchantment.id))
					{
						list.Add(enchantment.id);
					}
				}
			}
		}
		MagicItemMaker.createMagicItems(list, GameData.getFirstProjectFile());
	}

	// Token: 0x06000760 RID: 1888 RVA: 0x00020630 File Offset: 0x0001E830
	public static void setCustomProjectData()
	{
		try
		{
			GameData.customProjectFile = JsonUtility.FromJson<SKALDProjectData>(File.ReadAllText(SkaldModControl.getCurrentModProjectPath() + "SkaldProject.json"));
			SkaldModControl.debugLogProjectPath();
			GameData.projectStack.Add(GameData.customProjectFile);
		}
		catch (Exception ex)
		{
			string str = "WARNING: Could not find custom project: ";
			Exception ex2 = ex;
			MainControl.logWarning(str + ((ex2 != null) ? ex2.ToString() : null));
		}
	}

	// Token: 0x06000761 RID: 1889 RVA: 0x000206A0 File Offset: 0x0001E8A0
	public static List<SKALDProjectData.Objects.VariableContainer.Variable> getVariableList()
	{
		List<SKALDProjectData.Objects.VariableContainer.Variable> list = new List<SKALDProjectData.Objects.VariableContainer.Variable>();
		List<string> list2 = new List<string>();
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (SKALDProjectData.Objects.VariableContainer.Variable variable in skaldprojectData.data.variableContainer.list)
			{
				if (!list2.Contains(variable.id))
				{
					list.Add(variable);
					list2.Add(variable.id);
				}
			}
		}
		return list;
	}

	// Token: 0x06000762 RID: 1890 RVA: 0x00020760 File Offset: 0x0001E960
	public static string getTestSnippet()
	{
		string result = "";
		try
		{
			result = File.ReadAllText(SkaldModControl.getCurrentModProjectPath() + "TestSnippet.txt");
		}
		catch (Exception obj)
		{
			MainControl.logError(obj);
		}
		return result;
	}

	// Token: 0x06000763 RID: 1891 RVA: 0x000207A4 File Offset: 0x0001E9A4
	public static string getVersionNumberAndEdition()
	{
		string text = GameData.getVersionNumber();
		if (MainControl.isDeluxeEdition())
		{
			text += " - DELUXE EDITION";
		}
		return text;
	}

	// Token: 0x06000764 RID: 1892 RVA: 0x000207CC File Offset: 0x0001E9CC
	public static string getVersionNumber()
	{
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			if (skaldprojectData.metaData.version != "")
			{
				return skaldprojectData.metaData.version;
			}
		}
		return "No Version Set";
	}

	// Token: 0x06000765 RID: 1893 RVA: 0x00020844 File Offset: 0x0001EA44
	public static string getStartingMapId()
	{
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			if (skaldprojectData.metaData.startingMap != "")
			{
				return skaldprojectData.metaData.startingMap;
			}
		}
		Debug.LogError("No Starting map is set for this module. See METADATA in editor.");
		return "";
	}

	// Token: 0x06000766 RID: 1894 RVA: 0x000208C8 File Offset: 0x0001EAC8
	public static string getStartingTrigger()
	{
		if (GameData.shouldOverrideCore())
		{
			return GameData.getFirstProjectFile().metaData.startingTrigger;
		}
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			if (skaldprojectData.metaData.startingTrigger != "")
			{
				return skaldprojectData.metaData.startingTrigger;
			}
		}
		return "";
	}

	// Token: 0x06000767 RID: 1895 RVA: 0x00020958 File Offset: 0x0001EB58
	public static bool shouldStartWithCharacterCreation()
	{
		return GameData.getFirstProjectFile().metaData.startAtCharacterCreation;
	}

	// Token: 0x06000768 RID: 1896 RVA: 0x00020969 File Offset: 0x0001EB69
	public static bool shouldOverrideCore()
	{
		return GameData.getFirstProjectFile().metaData.standalone;
	}

	// Token: 0x06000769 RID: 1897 RVA: 0x0002097C File Offset: 0x0001EB7C
	public static string getCurrentModuleTitle()
	{
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			if (skaldprojectData.metaData.title != "")
			{
				return skaldprojectData.metaData.title;
			}
		}
		return "";
	}

	// Token: 0x0600076A RID: 1898 RVA: 0x000209F4 File Offset: 0x0001EBF4
	public static int getLevelUpDP()
	{
		return GameData.getFirstProjectFile().metaData.levelUpDP;
	}

	// Token: 0x0600076B RID: 1899 RVA: 0x00020A05 File Offset: 0x0001EC05
	public static int getStartingDP()
	{
		return GameData.getFirstProjectFile().metaData.startingDP;
	}

	// Token: 0x0600076C RID: 1900 RVA: 0x00020A16 File Offset: 0x0001EC16
	public static int getLevelCap()
	{
		return GameData.getFirstProjectFile().metaData.maxLevel;
	}

	// Token: 0x0600076D RID: 1901 RVA: 0x00020A28 File Offset: 0x0001EC28
	public static List<SKALDProjectData.Objects.EventContainer.Event> getEventList()
	{
		List<SKALDProjectData.Objects.EventContainer.Event> list = new List<SKALDProjectData.Objects.EventContainer.Event>();
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (SKALDProjectData.Objects.EventContainer.Event item in skaldprojectData.data.eventContainer.list)
			{
				list.Add(item);
			}
		}
		return list;
	}

	// Token: 0x0600076E RID: 1902 RVA: 0x00020AC4 File Offset: 0x0001ECC4
	public static List<SKALDProjectData.Objects.EncounterContainer.Encounter> getEncounterList()
	{
		List<SKALDProjectData.Objects.EncounterContainer.Encounter> list = new List<SKALDProjectData.Objects.EncounterContainer.Encounter>();
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (SKALDProjectData.Objects.EncounterContainer.Encounter item in skaldprojectData.data.encounterContainer.list)
			{
				list.Add(item);
			}
		}
		return list;
	}

	// Token: 0x0600076F RID: 1903 RVA: 0x00020B60 File Offset: 0x0001ED60
	public static SKALDProjectData.ApperanceElementContainers.ApperanceElement getHairData(string id)
	{
		if (id == "")
		{
			return null;
		}
		SKALDProjectData.ApperanceElementContainers.ApperanceElement member = GameData.hairCache.getMember(id);
		if (member != null)
		{
			return member;
		}
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.apperanceElementContainers.hairData.getBaseList())
			{
				SKALDProjectData.ApperanceElementContainers.ApperanceElement apperanceElement = (SKALDProjectData.ApperanceElementContainers.ApperanceElement)baseDataObject;
				if (apperanceElement.id == id)
				{
					GameData.hairCache.addMember(apperanceElement, id);
					return apperanceElement;
				}
			}
		}
		Debug.LogError("Missing HAIR with id " + id);
		return null;
	}

	// Token: 0x06000770 RID: 1904 RVA: 0x00020C48 File Offset: 0x0001EE48
	public static SKALDProjectData.ApperanceElementContainers.ApperanceElement getBeardData(string id)
	{
		if (id == "")
		{
			return null;
		}
		SKALDProjectData.ApperanceElementContainers.ApperanceElement member = GameData.beardCache.getMember(id);
		if (member != null)
		{
			return member;
		}
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.apperanceElementContainers.beardData.getBaseList())
			{
				SKALDProjectData.ApperanceElementContainers.ApperanceElement apperanceElement = (SKALDProjectData.ApperanceElementContainers.ApperanceElement)baseDataObject;
				if (apperanceElement.id == id)
				{
					GameData.beardCache.addMember(apperanceElement, id);
					return apperanceElement;
				}
			}
		}
		Debug.LogError("Missing BEARD with id " + id);
		return null;
	}

	// Token: 0x06000771 RID: 1905 RVA: 0x00020D30 File Offset: 0x0001EF30
	public static SKALDProjectData.ApperanceElementContainers.ApperanceElement getPortraitData(string id)
	{
		if (id == "")
		{
			return null;
		}
		SKALDProjectData.ApperanceElementContainers.ApperanceElement member = GameData.portraitCache.getMember(id);
		if (member != null)
		{
			return member;
		}
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.apperanceElementContainers.portraitData.getBaseList())
			{
				SKALDProjectData.ApperanceElementContainers.ApperanceElement apperanceElement = (SKALDProjectData.ApperanceElementContainers.ApperanceElement)baseDataObject;
				if (apperanceElement.id == id)
				{
					GameData.portraitCache.addMember(apperanceElement, id);
					return apperanceElement;
				}
			}
		}
		Debug.LogError("Missing PORTRAIT with id " + id);
		return null;
	}

	// Token: 0x06000772 RID: 1906 RVA: 0x00020E18 File Offset: 0x0001F018
	public static List<SKALDProjectData.ApperanceElementContainers.ApperanceElement> getPlayerHairStyles()
	{
		List<SKALDProjectData.ApperanceElementContainers.ApperanceElement> list = new List<SKALDProjectData.ApperanceElementContainers.ApperanceElement>();
		List<string> list2 = new List<string>();
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.apperanceElementContainers.hairData.getBaseList())
			{
				SKALDProjectData.ApperanceElementContainers.ApperanceElement apperanceElement = (SKALDProjectData.ApperanceElementContainers.ApperanceElement)baseDataObject;
				if (apperanceElement.playerSelectable && (MainControl.isDeluxeEdition() || !apperanceElement.deluxeEdition) && !list2.Contains(apperanceElement.id))
				{
					list2.Add(apperanceElement.id);
					list.Add(apperanceElement);
				}
			}
		}
		return list;
	}

	// Token: 0x06000773 RID: 1907 RVA: 0x00020EFC File Offset: 0x0001F0FC
	public static List<SKALDProjectData.ApperanceElementContainers.ApperanceElement> getPlayerBeardStyles()
	{
		List<SKALDProjectData.ApperanceElementContainers.ApperanceElement> list = new List<SKALDProjectData.ApperanceElementContainers.ApperanceElement>();
		List<string> list2 = new List<string>();
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.apperanceElementContainers.beardData.getBaseList())
			{
				SKALDProjectData.ApperanceElementContainers.ApperanceElement apperanceElement = (SKALDProjectData.ApperanceElementContainers.ApperanceElement)baseDataObject;
				if (apperanceElement.playerSelectable && (MainControl.isDeluxeEdition() || !apperanceElement.deluxeEdition) && !list2.Contains(apperanceElement.id))
				{
					list2.Add(apperanceElement.id);
					list.Add(apperanceElement);
				}
			}
		}
		return list;
	}

	// Token: 0x06000774 RID: 1908 RVA: 0x00020FE0 File Offset: 0x0001F1E0
	public static List<SKALDProjectData.ApperanceElementContainers.ApperanceElement> getPlayerPortraits()
	{
		List<SKALDProjectData.ApperanceElementContainers.ApperanceElement> list = new List<SKALDProjectData.ApperanceElementContainers.ApperanceElement>();
		List<string> list2 = new List<string>();
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.apperanceElementContainers.portraitData.getBaseList())
			{
				SKALDProjectData.ApperanceElementContainers.ApperanceElement apperanceElement = (SKALDProjectData.ApperanceElementContainers.ApperanceElement)baseDataObject;
				if (apperanceElement.playerSelectable && (MainControl.isDeluxeEdition() || !apperanceElement.deluxeEdition) && !list2.Contains(apperanceElement.id))
				{
					list2.Add(apperanceElement.id);
					list.Add(apperanceElement);
				}
			}
		}
		return list;
	}

	// Token: 0x06000775 RID: 1909 RVA: 0x000210C4 File Offset: 0x0001F2C4
	public static SKALDProjectData.Objects.EventContainer.Event getEventRawData(string id)
	{
		SKALDProjectData.Objects.EventContainer.Event member = GameData.eventCache.getMember(id);
		if (member != null)
		{
			return member;
		}
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (SKALDProjectData.Objects.EventContainer.Event @event in skaldprojectData.data.eventContainer.list)
			{
				if (@event.id == id)
				{
					GameData.eventCache.addMember(@event, id);
					return @event;
				}
			}
		}
		return null;
	}

	// Token: 0x06000776 RID: 1910 RVA: 0x00021188 File Offset: 0x0001F388
	public static SKALDProjectData.EnchantmentContainers.Enchantment getEnchantmentRawData(string id)
	{
		SKALDProjectData.EnchantmentContainers.Enchantment member = GameData.enchantmentCache.getMember(id);
		if (member != null)
		{
			return member;
		}
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.enchantmentContainers.getBaseList())
			{
				foreach (BaseDataObject baseDataObject2 in ((SKALDProjectData.BaseContainerObject)baseDataObject).getBaseList())
				{
					SKALDProjectData.EnchantmentContainers.Enchantment enchantment = (SKALDProjectData.EnchantmentContainers.Enchantment)baseDataObject2;
					if (enchantment.id == id)
					{
						GameData.enchantmentCache.addMember(enchantment, id);
						return enchantment;
					}
				}
			}
		}
		return null;
	}

	// Token: 0x06000777 RID: 1911 RVA: 0x00021294 File Offset: 0x0001F494
	public static SKALDProjectData.Objects.EncounterContainer.Encounter getEncounterRawData(string id)
	{
		SKALDProjectData.Objects.EncounterContainer.Encounter member = GameData.encounterCache.getMember(id);
		if (member != null)
		{
			return member;
		}
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (SKALDProjectData.Objects.EncounterContainer.Encounter encounter in skaldprojectData.data.encounterContainer.list)
			{
				if (encounter.id == id)
				{
					GameData.encounterCache.addMember(encounter, id);
					return encounter;
				}
			}
		}
		return null;
	}

	// Token: 0x06000778 RID: 1912 RVA: 0x00021358 File Offset: 0x0001F558
	public static List<SKALDProjectData.QuestContainers.QuestData> getQuestList()
	{
		List<SKALDProjectData.QuestContainers.QuestData> list = new List<SKALDProjectData.QuestContainers.QuestData>();
		List<string> list2 = new List<string>();
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.questContainers.getBaseList())
			{
				foreach (BaseDataObject baseDataObject2 in ((SKALDProjectData.BaseContainerObject)baseDataObject).getBaseList())
				{
					SKALDProjectData.QuestContainers.QuestData questData = (SKALDProjectData.QuestContainers.QuestData)baseDataObject2;
					if (!list2.Contains(questData.id))
					{
						list.Add(questData);
						list2.Add(questData.id);
					}
					foreach (BaseDataObject baseDataObject3 in questData.getBaseList())
					{
						SKALDProjectData.QuestContainers.QuestData questData2 = (SKALDProjectData.QuestContainers.QuestData)baseDataObject3;
						if (!list2.Contains(questData2.id))
						{
							list.Add(questData2);
							list2.Add(questData2.id);
						}
					}
				}
			}
		}
		return list;
	}

	// Token: 0x06000779 RID: 1913 RVA: 0x00021500 File Offset: 0x0001F700
	public static SKALDProjectData.QuestContainers.QuestData getQuestRawData(string questId)
	{
		if (questId == "" || questId == "")
		{
			return null;
		}
		foreach (SKALDProjectData.QuestContainers.QuestData questData in GameData.getQuestList())
		{
			if (questData.id == questId)
			{
				return questData;
			}
		}
		MainControl.logError("Could not find quest with ID: " + questId);
		return null;
	}

	// Token: 0x0600077A RID: 1914 RVA: 0x0002158C File Offset: 0x0001F78C
	public static SKALDProjectData.UIContainers.ColorContainer.Color getColorData(C64Color.ColorIds colorId)
	{
		if (colorId == C64Color.ColorIds.NULL)
		{
			return null;
		}
		string text = colorId.ToString();
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			SKALDProjectData.UIContainers.ColorContainer.Color listMember = skaldprojectData.uiContainer.colorData.getListMember(text);
			if (listMember != null)
			{
				return listMember;
			}
		}
		MainControl.logError("Could not find color with ID: " + text);
		return null;
	}

	// Token: 0x0600077B RID: 1915 RVA: 0x00021614 File Offset: 0x0001F814
	public static SKALDProjectData.CutSceneDataContainers.CutScenesBase.CutScenesData getCutSceneData(string cutSceneId)
	{
		if (cutSceneId == "")
		{
			return null;
		}
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.cutsceneContainers.getBaseList())
			{
				SKALDProjectData.CutSceneDataContainers.CutScenesBase.CutScenesData listMember = ((SKALDProjectData.CutSceneDataContainers.CutScenesBase)baseDataObject).getListMember(cutSceneId);
				if (listMember != null)
				{
					return listMember;
				}
			}
		}
		MainControl.logError("Could not find cutscene with ID: " + cutSceneId);
		return null;
	}

	// Token: 0x0600077C RID: 1916 RVA: 0x000216D4 File Offset: 0x0001F8D4
	public static SKALDProjectData.EffectContainers.EffectData getEffectData(string effectId)
	{
		if (effectId == "")
		{
			return null;
		}
		SKALDProjectData.EffectContainers.EffectData member = GameData.effectCache.getMember(effectId);
		if (member != null)
		{
			return member;
		}
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.effectContainers.getBaseList())
			{
				foreach (BaseDataObject baseDataObject2 in ((SKALDProjectData.BaseContainerObject)baseDataObject).getBaseList())
				{
					SKALDProjectData.EffectContainers.EffectData effectData = (SKALDProjectData.EffectContainers.EffectData)baseDataObject2;
					if (effectData.id == effectId)
					{
						return effectData;
					}
				}
			}
		}
		MainControl.logError("Could not find effect with ID: " + effectId);
		return null;
	}

	// Token: 0x0600077D RID: 1917 RVA: 0x000217F0 File Offset: 0x0001F9F0
	public static Effect getEffect(string id)
	{
		if (id == null || id == "")
		{
			return null;
		}
		Effect effect = GameData.characterComponentControl.getEffect(id);
		if (effect == null)
		{
			SKALDProjectData.EffectContainers.EffectData effectData = GameData.getEffectData(id);
			if (effectData != null)
			{
				effect = new Effect(effectData);
				GameData.characterComponentControl.addEffect(effect);
			}
		}
		if (effect == null)
		{
			MainControl.logError("Could not find effect with ID " + id);
		}
		return effect;
	}

	// Token: 0x0600077E RID: 1918 RVA: 0x00021850 File Offset: 0x0001FA50
	public static SKALDProjectData.AchievementContainers.Achievement getAchievementRawData(string achievementId)
	{
		if (achievementId == null || achievementId == "")
		{
			return null;
		}
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.achievementContainers.getBaseList())
			{
				foreach (BaseDataObject baseDataObject2 in ((SKALDProjectData.BaseContainerObject)baseDataObject).getBaseList())
				{
					SKALDProjectData.AchievementContainers.Achievement achievement = (SKALDProjectData.AchievementContainers.Achievement)baseDataObject2;
					if (achievement.id == achievementId)
					{
						return achievement;
					}
				}
			}
		}
		MainControl.logError("Could not find Achievement with ID " + achievementId);
		return null;
	}

	// Token: 0x0600077F RID: 1919 RVA: 0x0002195C File Offset: 0x0001FB5C
	public static string getConditionName(string conditionId)
	{
		SKALDProjectData.ConditionContainers.ConditionData conditionData = GameData.getConditionData(conditionId);
		if (conditionData != null)
		{
			return conditionData.title;
		}
		return conditionId;
	}

	// Token: 0x06000780 RID: 1920 RVA: 0x0002197C File Offset: 0x0001FB7C
	public static List<string> getConditionDataIdList()
	{
		List<string> list = new List<string>();
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.conditionContainers.getBaseList())
			{
				foreach (BaseDataObject baseDataObject2 in ((SKALDProjectData.BaseContainerObject)baseDataObject).getBaseList())
				{
					SKALDProjectData.ConditionContainers.ConditionData conditionData = (SKALDProjectData.ConditionContainers.ConditionData)baseDataObject2;
					if (!list.Contains(conditionData.id))
					{
						list.Add(conditionData.id);
					}
				}
			}
		}
		return list;
	}

	// Token: 0x06000781 RID: 1921 RVA: 0x00021A74 File Offset: 0x0001FC74
	public static SKALDProjectData.ConditionContainers.ConditionData getConditionData(string conditionId)
	{
		if (conditionId == "")
		{
			return null;
		}
		SKALDProjectData.ConditionContainers.ConditionData member = GameData.conditionCache.getMember(conditionId);
		if (member != null)
		{
			return member;
		}
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.conditionContainers.getBaseList())
			{
				foreach (BaseDataObject baseDataObject2 in ((SKALDProjectData.BaseContainerObject)baseDataObject).getBaseList())
				{
					SKALDProjectData.ConditionContainers.ConditionData conditionData = (SKALDProjectData.ConditionContainers.ConditionData)baseDataObject2;
					if (conditionData.id == conditionId)
					{
						GameData.conditionCache.addMember(conditionData, conditionData.id);
						return conditionData;
					}
				}
			}
		}
		MainControl.logError("Could not find condition with ID: " + conditionId);
		return null;
	}

	// Token: 0x06000782 RID: 1922 RVA: 0x00021BA4 File Offset: 0x0001FDA4
	public static SKALDProjectData.VehicleContainers.Vehicle getVehicleRawData(string id)
	{
		if (id == null || id == "")
		{
			return null;
		}
		SKALDProjectData.VehicleContainers.Vehicle member = GameData.vehicleRawDataCache.getMember(id);
		if (member != null)
		{
			return member;
		}
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.vehicleContainer.getBaseList())
			{
				foreach (BaseDataObject baseDataObject2 in ((SKALDProjectData.BaseContainerObject)baseDataObject).getBaseList())
				{
					SKALDProjectData.VehicleContainers.Vehicle vehicle = (SKALDProjectData.VehicleContainers.Vehicle)baseDataObject2;
					if (vehicle.id == id)
					{
						GameData.vehicleRawDataCache.addMember(vehicle, id);
						return vehicle;
					}
				}
			}
		}
		MainControl.logError("Could not find vehicle with ID: " + id);
		return null;
	}

	// Token: 0x06000783 RID: 1923 RVA: 0x00021CD0 File Offset: 0x0001FED0
	public static Ship instantiateShip(string id)
	{
		if (id == "")
		{
			return null;
		}
		SKALDProjectData.VehicleContainers.Vehicle vehicleRawData = GameData.getVehicleRawData(id);
		if (vehicleRawData == null)
		{
			return null;
		}
		Ship ship = new Ship(vehicleRawData);
		GameData.getInstanceControl().vehicleList.add(ship);
		return ship;
	}

	// Token: 0x06000784 RID: 1924 RVA: 0x00021D14 File Offset: 0x0001FF14
	public static Character instantiateCharacter(string id)
	{
		if (id == "")
		{
			return null;
		}
		SKALDProjectData.CharacterContainers.Character characterRawData = GameData.getCharacterRawData(id);
		if (characterRawData == null)
		{
			return null;
		}
		if (!MainControl.isDeluxeEdition() && characterRawData.deluxeEdition)
		{
			return null;
		}
		if (characterRawData.unique && GameData.getInstanceControl().characterList.containsObject(characterRawData.id))
		{
			return (Character)GameData.getInstanceControl().characterList.getObject(characterRawData.id);
		}
		Character character = new Character(characterRawData);
		GameData.getInstanceControl().characterList.add(character);
		return character;
	}

	// Token: 0x06000785 RID: 1925 RVA: 0x00021DA0 File Offset: 0x0001FFA0
	public static SKALDProjectData.CharacterContainers.Character getCharacterRawData(string id)
	{
		if (id == null || id == "")
		{
			return null;
		}
		SKALDProjectData.CharacterContainers.Character member = GameData.characterRawDataCache.getMember(id);
		if (member != null)
		{
			return member;
		}
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.characterContainer.getBaseList())
			{
				foreach (BaseDataObject baseDataObject2 in ((SKALDProjectData.BaseContainerObject)baseDataObject).getBaseList())
				{
					SKALDProjectData.CharacterContainers.Character character = (SKALDProjectData.CharacterContainers.Character)baseDataObject2;
					if (character.id == id)
					{
						GameData.characterRawDataCache.addMember(character, id);
						return character;
					}
				}
			}
		}
		MainControl.logError("Could not find character with ID: " + id);
		return null;
	}

	// Token: 0x06000786 RID: 1926 RVA: 0x00021ECC File Offset: 0x000200CC
	public static List<SKALDProjectData.Objects.FactionDataContainer.FactionData> getFactionList()
	{
		List<string> list = new List<string>();
		if (GameData.shouldOverrideCore())
		{
			using (List<BaseDataObject>.Enumerator enumerator = GameData.getFirstProjectFile().data.factionData.getBaseList().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					BaseDataObject baseDataObject = enumerator.Current;
					if (!list.Contains(baseDataObject.id))
					{
						list.Add(baseDataObject.id);
					}
				}
				goto IL_EA;
			}
		}
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject2 in skaldprojectData.data.factionData.getBaseList())
			{
				if (!list.Contains(baseDataObject2.id))
				{
					list.Add(baseDataObject2.id);
				}
			}
		}
		IL_EA:
		List<SKALDProjectData.Objects.FactionDataContainer.FactionData> list2 = new List<SKALDProjectData.Objects.FactionDataContainer.FactionData>();
		foreach (string id in list)
		{
			SKALDProjectData.Objects.FactionDataContainer.FactionData factionRawData = GameData.getFactionRawData(id);
			if (factionRawData != null)
			{
				list2.Add(factionRawData);
			}
		}
		return list2;
	}

	// Token: 0x06000787 RID: 1927 RVA: 0x0002203C File Offset: 0x0002023C
	public static SKALDProjectData.Objects.FactionDataContainer.FactionData getFactionRawData(string id)
	{
		if (id == null || id == "")
		{
			return null;
		}
		SKALDProjectData.Objects.FactionDataContainer.FactionData member = GameData.factionRawDataCache.getMember(id);
		if (member != null)
		{
			return member;
		}
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.data.factionData.getBaseList())
			{
				SKALDProjectData.Objects.FactionDataContainer.FactionData factionData = (SKALDProjectData.Objects.FactionDataContainer.FactionData)baseDataObject;
				if (factionData.id == id)
				{
					GameData.factionRawDataCache.addMember(factionData, id);
					return factionData;
				}
			}
		}
		MainControl.logError("Could not find character with ID: " + id);
		return null;
	}

	// Token: 0x06000788 RID: 1928 RVA: 0x00022128 File Offset: 0x00020328
	public static ItemMoney instantiateMoney()
	{
		ItemMoney itemMoney = new ItemMoney();
		GameData.getInstanceControl().itemList.add(itemMoney);
		return itemMoney;
	}

	// Token: 0x06000789 RID: 1929 RVA: 0x00022150 File Offset: 0x00020350
	public static Item instantiateItem(string id)
	{
		if (id == "")
		{
			return null;
		}
		SKALDProjectData.ItemDataContainers.ItemData itemRawData = GameData.getItemRawData(id);
		if (itemRawData == null)
		{
			return null;
		}
		if (itemRawData is SKALDProjectData.ItemDataContainers.MeleeWeaponContainer.MeleeWeaponData)
		{
			if (itemRawData.unique && GameData.getInstanceControl().itemList.containsObject(itemRawData.id))
			{
				MainControl.logError("Trying to instantiate a unique melee weapon that already exits! ID: " + id);
				return (ItemWeapon)GameData.getInstanceControl().itemList.getObject(itemRawData.id);
			}
			ItemMeleeWeapon itemMeleeWeapon = new ItemMeleeWeapon(itemRawData as SKALDProjectData.ItemDataContainers.MeleeWeaponContainer.MeleeWeaponData);
			GameData.getInstanceControl().itemList.add(itemMeleeWeapon);
			return itemMeleeWeapon;
		}
		else if (itemRawData is SKALDProjectData.ItemDataContainers.RangedWeaponContainer.RangedWeaponData)
		{
			if (itemRawData.unique && GameData.getInstanceControl().itemList.containsObject(itemRawData.id))
			{
				MainControl.logError("Trying to instantiate a unique ranged weapon that already exits! ID: " + id);
				return (ItemWeapon)GameData.getInstanceControl().itemList.getObject(itemRawData.id);
			}
			ItemRangedWeapon itemRangedWeapon = new ItemRangedWeapon(itemRawData as SKALDProjectData.ItemDataContainers.RangedWeaponContainer.RangedWeaponData);
			GameData.getInstanceControl().itemList.add(itemRangedWeapon);
			return itemRangedWeapon;
		}
		else if (itemRawData is SKALDProjectData.ItemDataContainers.AmmoContainer.AmmoData)
		{
			if (itemRawData.unique && GameData.getInstanceControl().itemList.containsObject(itemRawData.id))
			{
				MainControl.logError("Trying to instantiate a unique ammo that already exits! ID: " + id);
				return (ItemAmmo)GameData.getInstanceControl().itemList.getObject(itemRawData.id);
			}
			ItemAmmo itemAmmo = new ItemAmmo(itemRawData as SKALDProjectData.ItemDataContainers.AmmoContainer.AmmoData);
			GameData.getInstanceControl().itemList.add(itemAmmo);
			return itemAmmo;
		}
		else if (itemRawData is SKALDProjectData.ItemDataContainers.ArmorContainer.Armor)
		{
			if (itemRawData.unique && GameData.getInstanceControl().itemList.containsObject(itemRawData.id))
			{
				MainControl.logError("Trying to instantiate a unique armor that already exits! ID: " + id);
				return (ItemArmor)GameData.getInstanceControl().itemList.getObject(itemRawData.id);
			}
			ItemArmor itemArmor = new ItemArmor(itemRawData as SKALDProjectData.ItemDataContainers.ArmorContainer.Armor);
			GameData.getInstanceControl().itemList.add(itemArmor);
			return itemArmor;
		}
		else if (itemRawData is SKALDProjectData.ItemDataContainers.AccessoryContainer.Accessory)
		{
			SKALDProjectData.ItemDataContainers.AccessoryContainer.Accessory accessory = itemRawData as SKALDProjectData.ItemDataContainers.AccessoryContainer.Accessory;
			if (accessory.unique && GameData.getInstanceControl().itemList.containsObject(accessory.id))
			{
				MainControl.logError("Trying to instantiate a unique Accessory that already exits! ID: " + id);
				if (accessory.slot == "Head")
				{
					return (ItemHeadWear)GameData.getInstanceControl().itemList.getObject(itemRawData.id);
				}
				if (accessory.slot == "Feet")
				{
					return (ItemFootwear)GameData.getInstanceControl().itemList.getObject(itemRawData.id);
				}
				if (accessory.slot == "Hands")
				{
					return (ItemGlove)GameData.getInstanceControl().itemList.getObject(itemRawData.id);
				}
				return null;
			}
			else
			{
				if (accessory.slot == "Head")
				{
					ItemHeadWear itemHeadWear = new ItemHeadWear(accessory);
					GameData.getInstanceControl().itemList.add(itemHeadWear);
					return itemHeadWear;
				}
				if (accessory.slot == "Feet")
				{
					ItemFootwear itemFootwear = new ItemFootwear(accessory);
					GameData.getInstanceControl().itemList.add(itemFootwear);
					return itemFootwear;
				}
				if (accessory.slot == "Hands")
				{
					ItemGlove itemGlove = new ItemGlove(accessory);
					GameData.getInstanceControl().itemList.add(itemGlove);
					return itemGlove;
				}
				return null;
			}
		}
		else if (itemRawData is SKALDProjectData.ItemDataContainers.ShieldContainer.Shield)
		{
			if (itemRawData.unique && GameData.getInstanceControl().itemList.containsObject(itemRawData.id))
			{
				MainControl.logError("Trying to instantiate a unique shield that already exits! ID: " + id);
				return (ItemShield)GameData.getInstanceControl().itemList.getObject(itemRawData.id);
			}
			ItemShield itemShield = new ItemShield(itemRawData as SKALDProjectData.ItemDataContainers.ShieldContainer.Shield);
			GameData.getInstanceControl().itemList.add(itemShield);
			return itemShield;
		}
		else if (itemRawData is SKALDProjectData.ItemDataContainers.TrinketContainer.Trinket)
		{
			if (itemRawData.unique && GameData.getInstanceControl().itemList.containsObject(itemRawData.id))
			{
				MainControl.logError("Trying to instantiate a unique trinket that already exits! ID: " + id);
				return (ItemTrinket)GameData.getInstanceControl().itemList.getObject(itemRawData.id);
			}
			ItemTrinket itemTrinket = new ItemTrinket(itemRawData as SKALDProjectData.ItemDataContainers.TrinketContainer.Trinket);
			GameData.getInstanceControl().itemList.add(itemTrinket);
			return itemTrinket;
		}
		else if (itemRawData is SKALDProjectData.ItemDataContainers.GemContainer.Gem)
		{
			if (itemRawData.unique && GameData.getInstanceControl().itemList.containsObject(itemRawData.id))
			{
				MainControl.logError("Trying to instantiate a unique gem that already exits! ID: " + id);
				return (ItemTrinket)GameData.getInstanceControl().itemList.getObject(itemRawData.id);
			}
			ItemTrinket itemTrinket2 = new ItemTrinket(itemRawData as SKALDProjectData.ItemDataContainers.GemContainer.Gem);
			GameData.getInstanceControl().itemList.add(itemTrinket2);
			return itemTrinket2;
		}
		else if (itemRawData is SKALDProjectData.ItemDataContainers.ReagentContainer.Reagent)
		{
			if (itemRawData.unique && GameData.getInstanceControl().itemList.containsObject(itemRawData.id))
			{
				MainControl.logError("Trying to instantiate a unique reagent that already exits! ID: " + id);
				return (ItemReagent)GameData.getInstanceControl().itemList.getObject(itemRawData.id);
			}
			ItemReagent itemReagent = new ItemReagent(itemRawData as SKALDProjectData.ItemDataContainers.ReagentContainer.Reagent);
			GameData.getInstanceControl().itemList.add(itemReagent);
			return itemReagent;
		}
		else if (itemRawData is SKALDProjectData.ItemDataContainers.ClothingContainer.Clothing)
		{
			SKALDProjectData.ItemDataContainers.ClothingContainer.Clothing clothing = itemRawData as SKALDProjectData.ItemDataContainers.ClothingContainer.Clothing;
			if (clothing.unique && GameData.getInstanceControl().itemList.containsObject(clothing.id))
			{
				MainControl.logError("Trying to instantiate a unique clothing that already exits! ID: " + id);
				return (ItemClothing)GameData.getInstanceControl().itemList.getObject(clothing.id);
			}
			ItemClothing itemClothing = new ItemClothing(clothing);
			GameData.getInstanceControl().itemList.add(itemClothing);
			return itemClothing;
		}
		else if (itemRawData is SKALDProjectData.ItemDataContainers.AdventuringContainer.AdventuringItem)
		{
			SKALDProjectData.ItemDataContainers.AdventuringContainer.AdventuringItem adventuringItem = itemRawData as SKALDProjectData.ItemDataContainers.AdventuringContainer.AdventuringItem;
			if (adventuringItem.unique && GameData.getInstanceControl().itemList.containsObject(adventuringItem.id))
			{
				MainControl.logError("Trying to instantiate a unique adventuring item that already exits! ID: " + id);
				if (adventuringItem.light > 0)
				{
					return (ItemLight)GameData.getInstanceControl().itemList.getObject(adventuringItem.id);
				}
				return (ItemAdventuring)GameData.getInstanceControl().itemList.getObject(adventuringItem.id);
			}
			else
			{
				if (adventuringItem.light > 0)
				{
					ItemLight itemLight = new ItemLight(adventuringItem);
					GameData.getInstanceControl().itemList.add(itemLight);
					return itemLight;
				}
				ItemAdventuring itemAdventuring = new ItemAdventuring(adventuringItem);
				GameData.getInstanceControl().itemList.add(itemAdventuring);
				return itemAdventuring;
			}
		}
		else if (itemRawData is SKALDProjectData.ItemDataContainers.FoodContainer.Food)
		{
			if (itemRawData.unique && GameData.getInstanceControl().itemList.containsObject(itemRawData.id))
			{
				MainControl.logError("Trying to instantiate a unique food that already exits! ID: " + id);
				return (ItemFood)GameData.getInstanceControl().itemList.getObject(itemRawData.id);
			}
			ItemFood itemFood = new ItemFood(itemRawData as SKALDProjectData.ItemDataContainers.FoodContainer.Food);
			GameData.getInstanceControl().itemList.add(itemFood);
			return itemFood;
		}
		else if (itemRawData is SKALDProjectData.ItemDataContainers.ConsumeableContainer.Consumeable)
		{
			if (itemRawData.unique && GameData.getInstanceControl().itemList.containsObject(itemRawData.id))
			{
				MainControl.logError("Trying to instantiate a unique consumable that already exits! ID: " + id);
				return (ItemConsumable)GameData.getInstanceControl().itemList.getObject(itemRawData.id);
			}
			ItemConsumable itemConsumable = new ItemConsumable(itemRawData as SKALDProjectData.ItemDataContainers.ConsumeableContainer.Consumeable);
			GameData.getInstanceControl().itemList.add(itemConsumable);
			return itemConsumable;
		}
		else if (itemRawData is SKALDProjectData.ItemDataContainers.TomeContainer.TomeData)
		{
			if (itemRawData.unique && GameData.getInstanceControl().itemList.containsObject(itemRawData.id))
			{
				MainControl.logError("Trying to instantiate a unique tome that already exits! ID: " + id);
				return (ItemSpellTome)GameData.getInstanceControl().itemList.getObject(itemRawData.id);
			}
			ItemSpellTome itemSpellTome = new ItemSpellTome(itemRawData as SKALDProjectData.ItemDataContainers.TomeContainer.TomeData);
			GameData.getInstanceControl().itemList.add(itemSpellTome);
			return itemSpellTome;
		}
		else if (itemRawData is SKALDProjectData.ItemDataContainers.RecipeContainer.Recipe)
		{
			if (itemRawData.unique && GameData.getInstanceControl().itemList.containsObject(itemRawData.id))
			{
				MainControl.logError("Trying to instantiate a unique recipe that already exits! ID: " + id);
				return (ItemRecipeBook)GameData.getInstanceControl().itemList.getObject(itemRawData.id);
			}
			ItemRecipeBook itemRecipeBook = new ItemRecipeBook(itemRawData as SKALDProjectData.ItemDataContainers.RecipeContainer.Recipe);
			GameData.getInstanceControl().itemList.add(itemRecipeBook);
			return itemRecipeBook;
		}
		else if (itemRawData is SKALDProjectData.ItemDataContainers.BookContainer.Book)
		{
			if (itemRawData.unique && GameData.getInstanceControl().itemList.containsObject(itemRawData.id))
			{
				MainControl.logError("Trying to instantiate a unique book that already exits! ID: " + id);
				return (ItemBook)GameData.getInstanceControl().itemList.getObject(itemRawData.id);
			}
			ItemBook itemBook = new ItemBook(itemRawData as SKALDProjectData.ItemDataContainers.BookContainer.Book);
			GameData.getInstanceControl().itemList.add(itemBook);
			return itemBook;
		}
		else if (itemRawData is SKALDProjectData.ItemDataContainers.KeyContainer.Key)
		{
			if (itemRawData.unique && GameData.getInstanceControl().itemList.containsObject(itemRawData.id))
			{
				MainControl.logError("Trying to instantiate a unique key that already exits! ID: " + id);
				return (ItemKey)GameData.getInstanceControl().itemList.getObject(itemRawData.id);
			}
			ItemKey itemKey = new ItemKey(itemRawData as SKALDProjectData.ItemDataContainers.KeyContainer.Key);
			GameData.getInstanceControl().itemList.add(itemKey);
			return itemKey;
		}
		else if (itemRawData is SKALDProjectData.ItemDataContainers.JewelryContainer.Jewelry)
		{
			SKALDProjectData.ItemDataContainers.JewelryContainer.Jewelry jewelry = itemRawData as SKALDProjectData.ItemDataContainers.JewelryContainer.Jewelry;
			if (jewelry.unique && GameData.getInstanceControl().itemList.containsObject(jewelry.id))
			{
				MainControl.logError("Trying to instantiate a unique jewelry that already exits! ID: " + id);
				if (jewelry.slotJewelry == "Finger")
				{
					return (ItemRing)GameData.getInstanceControl().itemList.getObject(itemRawData.id);
				}
				if (jewelry.slotJewelry == "Neck")
				{
					return (ItemNecklace)GameData.getInstanceControl().itemList.getObject(itemRawData.id);
				}
				return (ItemJewelry)GameData.getInstanceControl().itemList.getObject(itemRawData.id);
			}
			else
			{
				if (jewelry.slotJewelry == "Finger")
				{
					ItemRing itemRing = new ItemRing(jewelry);
					GameData.getInstanceControl().itemList.add(itemRing);
					return itemRing;
				}
				if (jewelry.slotJewelry == "Neck")
				{
					ItemNecklace itemNecklace = new ItemNecklace(jewelry);
					GameData.getInstanceControl().itemList.add(itemNecklace);
					return itemNecklace;
				}
				ItemJewelry itemJewelry = new ItemJewelry(jewelry);
				GameData.getInstanceControl().itemList.add(itemJewelry);
				return itemJewelry;
			}
		}
		else if (itemRawData is SKALDProjectData.ItemDataContainers.IdleItemContainer.IdleItemData)
		{
			if (itemRawData.unique && GameData.getInstanceControl().itemList.containsObject(itemRawData.id))
			{
				MainControl.logError("Trying to instantiate a unique idle item that already exits! ID: " + id);
				return (ItemIdle)GameData.getInstanceControl().itemList.getObject(itemRawData.id);
			}
			ItemIdle itemIdle = new ItemIdle(itemRawData as SKALDProjectData.ItemDataContainers.IdleItemContainer.IdleItemData);
			GameData.getInstanceControl().itemList.add(itemIdle);
			return itemIdle;
		}
		else
		{
			if (itemRawData.unique && GameData.getInstanceControl().itemList.containsObject(itemRawData.id))
			{
				MainControl.logError("Trying to instantiate a unique misc item that already exits! ID: " + id);
				return (ItemMisc)GameData.getInstanceControl().itemList.getObject(itemRawData.id);
			}
			ItemMisc itemMisc = new ItemMisc(itemRawData);
			GameData.getInstanceControl().itemList.add(itemMisc);
			return itemMisc;
		}
	}

	// Token: 0x0600078A RID: 1930 RVA: 0x00022C94 File Offset: 0x00020E94
	public static string getItemName(string itemId)
	{
		SKALDProjectData.ItemDataContainers.ItemData itemRawData = GameData.getItemRawData(itemId);
		if (itemRawData == null)
		{
			return "";
		}
		return itemRawData.title;
	}

	// Token: 0x0600078B RID: 1931 RVA: 0x00022CB8 File Offset: 0x00020EB8
	public static SKALDProjectData.ItemDataContainers.ItemData getItemRawData(string id)
	{
		if (id == "" || id == null)
		{
			return null;
		}
		SKALDProjectData.ItemDataContainers.ItemData member = GameData.itemRawDataCache.getMember(id);
		if (member != null)
		{
			return member;
		}
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.itemContainer.getBaseList())
			{
				foreach (BaseDataObject baseDataObject2 in ((SKALDProjectData.BaseContainerObject)baseDataObject).getBaseList())
				{
					SKALDProjectData.ItemDataContainers.ItemData itemData = (SKALDProjectData.ItemDataContainers.ItemData)baseDataObject2;
					if (itemData.id == id)
					{
						GameData.itemRawDataCache.addMember(itemData, id);
						return itemData;
					}
				}
			}
		}
		MainControl.logError("Did not find item with ID: " + id);
		return null;
	}

	// Token: 0x0600078C RID: 1932 RVA: 0x00022DE4 File Offset: 0x00020FE4
	public static bool applyLoadoutData(string loadoutId, Inventory inventory)
	{
		if (loadoutId == null || loadoutId == "")
		{
			return false;
		}
		if (inventory == null)
		{
			MainControl.logError("Looking for loadout with NULL inventory: " + loadoutId);
		}
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.loadoutContainers.getBaseList())
			{
				SKALDProjectData.LoadoutDataContainers.LoadoutDataContainer.LoadoutData listMember = ((SKALDProjectData.LoadoutDataContainers.LoadoutDataContainer)baseDataObject).getListMember(loadoutId);
				if (listMember != null)
				{
					new LoadoutBuilder(listMember, inventory);
					return true;
				}
			}
		}
		MainControl.logError("Could not find loadout ID " + loadoutId);
		return false;
	}

	// Token: 0x0600078D RID: 1933 RVA: 0x00022EC0 File Offset: 0x000210C0
	public static List<string> getRandomItemIdList(string type, int minRarity, int maxRarity, int minMagic, int maxMagic, int count)
	{
		List<string> list = new List<string>();
		if (type == "")
		{
			return list;
		}
		List<string> list2 = GameData.searchForItems(type, minRarity, maxRarity, minMagic, maxMagic);
		if (list2 == null || list2.Count == 0)
		{
			return list;
		}
		for (int i = 0; i < count; i++)
		{
			int index = Random.Range(0, list2.Count);
			list.Add(list2[index]);
			if (list2.Count >= count - i)
			{
				list2.RemoveAt(index);
			}
		}
		return list;
	}

	// Token: 0x0600078E RID: 1934 RVA: 0x00022F38 File Offset: 0x00021138
	public static string[] makeStoreInventory(string type, int minRarity, int maxRarity, int minMagic, int maxMagic)
	{
		if (type == "")
		{
			return null;
		}
		List<string> list = GameData.searchForItems(type, minRarity, maxRarity, minMagic, maxMagic);
		if (list == null || list.Count == 0)
		{
			return null;
		}
		string[] array = new string[list.Count];
		for (int i = 0; i < list.Count; i++)
		{
			array[i] = list[i];
		}
		return array;
	}

	// Token: 0x0600078F RID: 1935 RVA: 0x00022F98 File Offset: 0x00021198
	private static List<string> searchForItems(string type, int minRarity, int maxRarity, int minMagic, int maxMagic)
	{
		type = type.ToUpper();
		List<string> list = new List<string>();
		if (type == "MELEEWEAPON")
		{
			foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
			{
				GameData.selectItemsFromList(minRarity, maxRarity, minMagic, maxMagic, skaldprojectData.itemContainer.meleeWeapons.getBaseList(), list);
			}
			return list;
		}
		if (type == "RANGEDWEAPON")
		{
			foreach (SKALDProjectData skaldprojectData2 in GameData.projectStack)
			{
				GameData.selectItemsFromList(minRarity, maxRarity, minMagic, maxMagic, skaldprojectData2.itemContainer.rangedWeapons.getBaseList(), list);
			}
			return list;
		}
		if (type == "AMMO")
		{
			foreach (SKALDProjectData skaldprojectData3 in GameData.projectStack)
			{
				GameData.selectItemsFromList(minRarity, maxRarity, minMagic, maxMagic, skaldprojectData3.itemContainer.ammoContainer.getBaseList(), list);
			}
			return list;
		}
		if (type == "ACCESSORY")
		{
			foreach (SKALDProjectData skaldprojectData4 in GameData.projectStack)
			{
				GameData.selectItemsFromList(minRarity, maxRarity, minMagic, maxMagic, skaldprojectData4.itemContainer.accessories.getBaseList(), list);
			}
			return list;
		}
		if (type == "ARMOR")
		{
			foreach (SKALDProjectData skaldprojectData5 in GameData.projectStack)
			{
				GameData.selectItemsFromList(minRarity, maxRarity, minMagic, maxMagic, skaldprojectData5.itemContainer.armor.getBaseList(), list);
			}
			return list;
		}
		if (type == "ADVENTURING" || type == "LIGHT")
		{
			foreach (SKALDProjectData skaldprojectData6 in GameData.projectStack)
			{
				GameData.selectItemsFromList(minRarity, maxRarity, minMagic, maxMagic, skaldprojectData6.itemContainer.adventuringItems.getBaseList(), list);
			}
			return list;
		}
		if (type == "CLOTHING")
		{
			foreach (SKALDProjectData skaldprojectData7 in GameData.projectStack)
			{
				GameData.selectItemsFromList(minRarity, maxRarity, minMagic, maxMagic, skaldprojectData7.itemContainer.clothing.getBaseList(), list);
			}
			return list;
		}
		if (type == "SHIELD")
		{
			foreach (SKALDProjectData skaldprojectData8 in GameData.projectStack)
			{
				GameData.selectItemsFromList(minRarity, maxRarity, minMagic, maxMagic, skaldprojectData8.itemContainer.shields.getBaseList(), list);
			}
			return list;
		}
		if (type == "CONSUMABLE")
		{
			foreach (SKALDProjectData skaldprojectData9 in GameData.projectStack)
			{
				GameData.selectItemsFromList(minRarity, maxRarity, minMagic, maxMagic, skaldprojectData9.itemContainer.consumeables.getBaseList(), list);
			}
			return list;
		}
		if (type == "MISC")
		{
			foreach (SKALDProjectData skaldprojectData10 in GameData.projectStack)
			{
				GameData.selectItemsFromList(minRarity, maxRarity, minMagic, maxMagic, skaldprojectData10.itemContainer.miscItems.getBaseList(), list);
			}
			return list;
		}
		if (type == "REAGENT")
		{
			foreach (SKALDProjectData skaldprojectData11 in GameData.projectStack)
			{
				GameData.selectItemsFromList(minRarity, maxRarity, minMagic, maxMagic, skaldprojectData11.itemContainer.reagents.getBaseList(), list);
			}
			return list;
		}
		if (type == "TRINKET")
		{
			foreach (SKALDProjectData skaldprojectData12 in GameData.projectStack)
			{
				GameData.selectItemsFromList(minRarity, maxRarity, minMagic, maxMagic, skaldprojectData12.itemContainer.trinkets.getBaseList(), list);
			}
			return list;
		}
		if (type == "GEMS")
		{
			foreach (SKALDProjectData skaldprojectData13 in GameData.projectStack)
			{
				GameData.selectItemsFromList(minRarity, maxRarity, minMagic, maxMagic, skaldprojectData13.itemContainer.gems.getBaseList(), list);
			}
			return list;
		}
		if (type == "FOOD")
		{
			foreach (SKALDProjectData skaldprojectData14 in GameData.projectStack)
			{
				GameData.selectItemsFromList(minRarity, maxRarity, minMagic, maxMagic, skaldprojectData14.itemContainer.foods.getBaseList(), list);
			}
			return list;
		}
		if (type == "BOOK")
		{
			foreach (SKALDProjectData skaldprojectData15 in GameData.projectStack)
			{
				GameData.selectItemsFromList(minRarity, maxRarity, minMagic, maxMagic, skaldprojectData15.itemContainer.books.getBaseList(), list);
			}
			return list;
		}
		if (type == "RECIPE")
		{
			foreach (SKALDProjectData skaldprojectData16 in GameData.projectStack)
			{
				GameData.selectItemsFromList(minRarity, maxRarity, minMagic, maxMagic, skaldprojectData16.itemContainer.recipes.getBaseList(), list);
			}
			return list;
		}
		if (type == "KEY")
		{
			foreach (SKALDProjectData skaldprojectData17 in GameData.projectStack)
			{
				GameData.selectItemsFromList(minRarity, maxRarity, minMagic, maxMagic, skaldprojectData17.itemContainer.keys.getBaseList(), list);
			}
			return list;
		}
		if (type == "JEWELRY")
		{
			foreach (SKALDProjectData skaldprojectData18 in GameData.projectStack)
			{
				GameData.selectItemsFromList(minRarity, maxRarity, minMagic, maxMagic, skaldprojectData18.itemContainer.jewelry.getBaseList(), list);
			}
			return list;
		}
		if (type == "TOMES")
		{
			foreach (SKALDProjectData skaldprojectData19 in GameData.projectStack)
			{
				GameData.selectItemsFromList(minRarity, maxRarity, minMagic, maxMagic, skaldprojectData19.itemContainer.tomes.getBaseList(), list);
			}
			return list;
		}
		if (type == "TOMESFIRE")
		{
			foreach (SKALDProjectData skaldprojectData20 in GameData.projectStack)
			{
				GameData.selectTomeFromList(minRarity, maxRarity, skaldprojectData20.itemContainer.tomes.getBaseList(), "ATT_SpellListFire", list);
			}
			return list;
		}
		if (type == "TOMESAIR")
		{
			foreach (SKALDProjectData skaldprojectData21 in GameData.projectStack)
			{
				GameData.selectTomeFromList(minRarity, maxRarity, skaldprojectData21.itemContainer.tomes.getBaseList(), "ATT_SpellListAir", list);
			}
			return list;
		}
		if (type == "TOMESEARTH")
		{
			foreach (SKALDProjectData skaldprojectData22 in GameData.projectStack)
			{
				GameData.selectTomeFromList(minRarity, maxRarity, skaldprojectData22.itemContainer.tomes.getBaseList(), "ATT_SpellListEarth", list);
			}
			return list;
		}
		if (type == "TOMESWATER")
		{
			foreach (SKALDProjectData skaldprojectData23 in GameData.projectStack)
			{
				GameData.selectTomeFromList(minRarity, maxRarity, skaldprojectData23.itemContainer.tomes.getBaseList(), "ATT_SpellListWater", list);
			}
			return list;
		}
		if (type == "TOMESNATURE")
		{
			foreach (SKALDProjectData skaldprojectData24 in GameData.projectStack)
			{
				GameData.selectTomeFromList(minRarity, maxRarity, skaldprojectData24.itemContainer.tomes.getBaseList(), "ATT_SpellListNature", list);
			}
			return list;
		}
		if (type == "TOMESBARDIC")
		{
			foreach (SKALDProjectData skaldprojectData25 in GameData.projectStack)
			{
				GameData.selectTomeFromList(minRarity, maxRarity, skaldprojectData25.itemContainer.tomes.getBaseList(), "ATT_SpellListBardic", list);
			}
			return list;
		}
		if (type == "TOMESMIND")
		{
			foreach (SKALDProjectData skaldprojectData26 in GameData.projectStack)
			{
				GameData.selectTomeFromList(minRarity, maxRarity, skaldprojectData26.itemContainer.tomes.getBaseList(), "ATT_SpellListMind", list);
			}
			return list;
		}
		if (type == "TOMESBODY")
		{
			foreach (SKALDProjectData skaldprojectData27 in GameData.projectStack)
			{
				GameData.selectTomeFromList(minRarity, maxRarity, skaldprojectData27.itemContainer.tomes.getBaseList(), "ATT_SpellListBody", list);
			}
			return list;
		}
		if (type == "TOMESSPIRIT")
		{
			foreach (SKALDProjectData skaldprojectData28 in GameData.projectStack)
			{
				GameData.selectTomeFromList(minRarity, maxRarity, skaldprojectData28.itemContainer.tomes.getBaseList(), "ATT_SpellListSpirit", list);
			}
			return list;
		}
		MainControl.logError("Could not find item-type: " + type);
		return null;
	}

	// Token: 0x06000790 RID: 1936 RVA: 0x00023C78 File Offset: 0x00021E78
	private static List<string> selectItemsFromList(int minRarity, int maxRarity, int minMagic, int maxMagic, List<BaseDataObject> inputList, List<string> resultList)
	{
		for (int i = 0; i < inputList.Count; i++)
		{
			SKALDProjectData.ItemDataContainers.ItemData itemData = inputList[i] as SKALDProjectData.ItemDataContainers.ItemData;
			int num = itemData.rarity;
			int num2 = itemData.magicLevel;
			bool flag = false;
			bool flag2 = false;
			if (itemData.parent != null && itemData.parent != "")
			{
				SKALDProjectData.ItemDataContainers.ItemData itemRawData = GameData.getItemRawData(itemData.parent);
				if (itemRawData != null)
				{
					num += itemRawData.rarity;
					num2 += itemRawData.magicLevel;
				}
			}
			if (itemData.enchantment != null && itemData.enchantment != "")
			{
				SKALDProjectData.EnchantmentContainers.Enchantment enchantmentRawData = GameData.getEnchantmentRawData(itemData.enchantment);
				if (enchantmentRawData != null)
				{
					num2 += enchantmentRawData.magicLevel;
				}
			}
			if (num < 1)
			{
				num = 1;
			}
			if (num >= minRarity && num <= maxRarity && !itemData.questItem && !itemData.unique)
			{
				flag = true;
			}
			if (num2 >= minMagic && num2 <= maxMagic && !itemData.questItem && !itemData.unique)
			{
				flag2 = true;
			}
			if (flag && flag2 && !resultList.Contains(itemData.id))
			{
				resultList.Add(itemData.id);
			}
		}
		return resultList;
	}

	// Token: 0x06000791 RID: 1937 RVA: 0x00023D94 File Offset: 0x00021F94
	private static List<string> selectTomeFromList(int minRarity, int maxRarity, List<BaseDataObject> inputList, string schoolId, List<string> resultList)
	{
		for (int i = 0; i < inputList.Count; i++)
		{
			SKALDProjectData.ItemDataContainers.TomeContainer.TomeData tomeData = inputList[i] as SKALDProjectData.ItemDataContainers.TomeContainer.TomeData;
			if (!tomeData.questItem && !tomeData.unique)
			{
				SKALDProjectData.AbilityContainers.SpellContainer.SpellAbility spellRawData = GameData.getSpellRawData(tomeData.spellLearned);
				if (spellRawData != null && spellRawData.school.Contains(schoolId) && tomeData.rarity >= minRarity && tomeData.rarity <= maxRarity && !resultList.Contains(tomeData.id))
				{
					resultList.Add(tomeData.id);
				}
			}
		}
		return resultList;
	}

	// Token: 0x06000792 RID: 1938 RVA: 0x00023E1C File Offset: 0x0002201C
	public static AnimationStrip getAnimationData(string animationId)
	{
		if (animationId == "")
		{
			return null;
		}
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			SKALDProjectData.AnimationContainers.AnimationContainer.AnimationData listMember = skaldprojectData.animationContainer.animations.getListMember(animationId);
			if (listMember != null)
			{
				return new AnimationStrip(listMember);
			}
		}
		MainControl.logError("Could not find anmiation ID " + animationId);
		return null;
	}

	// Token: 0x06000793 RID: 1939 RVA: 0x00023EA8 File Offset: 0x000220A8
	public static SKALDProjectData.Objects.ApperanceDataContainer.ApperanceData getApperanceData(string apperanceId)
	{
		if (apperanceId == "")
		{
			return null;
		}
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			SKALDProjectData.Objects.ApperanceDataContainer.ApperanceData listMember = skaldprojectData.data.apperanceData.getListMember(apperanceId);
			if (listMember != null)
			{
				return listMember;
			}
		}
		MainControl.logError("Could not find anmiation ID " + apperanceId);
		return null;
	}

	// Token: 0x06000794 RID: 1940 RVA: 0x00023F2C File Offset: 0x0002212C
	public static Font getFont(string fontId)
	{
		if (fontId == null || fontId == "")
		{
			return null;
		}
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			SKALDProjectData.UIContainers.FontContainer.Font listMember = skaldprojectData.uiContainer.fontData.getListMember(fontId);
			if (listMember != null)
			{
				return new Font(listMember);
			}
		}
		MainControl.logError("Could not find font with ID " + fontId);
		return null;
	}

	// Token: 0x06000795 RID: 1941 RVA: 0x00023FB8 File Offset: 0x000221B8
	public static string getScript(string scriptId)
	{
		if (scriptId == "")
		{
			return null;
		}
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			SKALDProjectData.Objects.ScriptContainer.Script listMember = skaldprojectData.data.scriptContainer.getListMember(scriptId);
			if (listMember != null)
			{
				return listMember.description;
			}
		}
		MainControl.logError("Could not find script with ID " + scriptId);
		return "";
	}

	// Token: 0x06000796 RID: 1942 RVA: 0x00024048 File Offset: 0x00022248
	public static List<string> getAllJournalRawDataId()
	{
		List<string> list = new List<string>();
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.journalContainers.getBaseList())
			{
				foreach (BaseDataObject baseDataObject2 in ((SKALDProjectData.BaseContainerObject)baseDataObject).getBaseList())
				{
					SKALDProjectData.JournalContainers.JournalEntry journalEntry = (SKALDProjectData.JournalContainers.JournalEntry)baseDataObject2;
					if (!list.Contains(journalEntry.id))
					{
						list.Add(journalEntry.id);
					}
				}
			}
		}
		return list;
	}

	// Token: 0x06000797 RID: 1943 RVA: 0x00024140 File Offset: 0x00022340
	public static SKALDProjectData.JournalContainers.JournalEntry getJournalRawData(string id)
	{
		if (id == null || id == "")
		{
			return null;
		}
		SKALDProjectData.JournalContainers.JournalEntry member = GameData.journalRawDataCache.getMember(id);
		if (member != null)
		{
			return member;
		}
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.journalContainers.getBaseList())
			{
				foreach (BaseDataObject baseDataObject2 in ((SKALDProjectData.BaseContainerObject)baseDataObject).getBaseList())
				{
					SKALDProjectData.JournalContainers.JournalEntry journalEntry = (SKALDProjectData.JournalContainers.JournalEntry)baseDataObject2;
					if (journalEntry.id == id)
					{
						GameData.journalRawDataCache.addMember(journalEntry, id);
						return journalEntry;
					}
				}
			}
		}
		MainControl.logError("Could not find journal with ID " + id);
		return null;
	}

	// Token: 0x06000798 RID: 1944 RVA: 0x0002426C File Offset: 0x0002246C
	public static SKALDProjectData.ClassContainers.ClassData getClassRawData(string id)
	{
		if (id == null || id == "" || id == "Unknown")
		{
			return null;
		}
		SKALDProjectData.ClassContainers.ClassData member = GameData.classRawDataCache.getMember(id);
		if (member != null)
		{
			return member;
		}
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.classContainers.getBaseList())
			{
				foreach (BaseDataObject baseDataObject2 in ((SKALDProjectData.BaseContainerObject)baseDataObject).getBaseList())
				{
					SKALDProjectData.ClassContainers.ClassData classData = (SKALDProjectData.ClassContainers.ClassData)baseDataObject2;
					if (classData.id == id)
					{
						GameData.classRawDataCache.addMember(classData, id);
						return classData;
					}
				}
			}
		}
		MainControl.logError("Could not find class with ID " + id);
		return null;
	}

	// Token: 0x06000799 RID: 1945 RVA: 0x000243A8 File Offset: 0x000225A8
	public static CharacterClass getClass(string id)
	{
		CharacterClass characterClass;
		if (id == null || id == "")
		{
			characterClass = new CharacterClass();
			GameData.characterFeatureControl.addClass(characterClass);
			return characterClass;
		}
		characterClass = GameData.characterFeatureControl.getClass(id);
		if (characterClass == null)
		{
			SKALDProjectData.ClassContainers.ClassData classRawData = GameData.getClassRawData(id);
			if (classRawData != null)
			{
				characterClass = new CharacterClass(classRawData);
			}
			else
			{
				characterClass = new CharacterClass();
			}
			GameData.characterFeatureControl.addClass(characterClass);
		}
		return characterClass;
	}

	// Token: 0x0600079A RID: 1946 RVA: 0x0002440C File Offset: 0x0002260C
	public static List<string> getAllSpellSchools()
	{
		List<string> list = new List<string>();
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (SKALDProjectData.Objects.AttributeData.AttributeContainer attributeContainer in skaldprojectData.data.attributeData.list)
			{
				foreach (SKALDProjectData.Objects.AttributeData.AttributeContainer.Attribute attribute in attributeContainer.list)
				{
					if (!list.Contains(attribute.id) && attribute.id.Contains("SpellList"))
					{
						list.Add(attribute.id);
					}
				}
			}
		}
		return list;
	}

	// Token: 0x0600079B RID: 1947 RVA: 0x00024510 File Offset: 0x00022710
	public static List<AbilitySpell> getAllSpells()
	{
		List<AbilitySpell> list = new List<AbilitySpell>();
		List<string> list2 = new List<string>();
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (string item in skaldprojectData.abilityContainers.spellContainer.getFlatIdList(new List<string>()))
			{
				if (!list2.Contains(item))
				{
					list2.Add(item);
				}
			}
		}
		foreach (string text in list2)
		{
			AbilitySpell spell = GameData.getSpell(text);
			if (text != null)
			{
				list.Add(spell);
			}
		}
		return list;
	}

	// Token: 0x0600079C RID: 1948 RVA: 0x0002460C File Offset: 0x0002280C
	public static SKALDProjectData.AbilityContainers.SpellContainer.SpellAbility getSpellRawData(string id)
	{
		if (id == null || id == "" || id == "Unknown")
		{
			return null;
		}
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.abilityContainers.spellContainer.getBaseList())
			{
				SKALDProjectData.AbilityContainers.SpellContainer.SpellAbility spellAbility = (SKALDProjectData.AbilityContainers.SpellContainer.SpellAbility)baseDataObject;
				if (spellAbility.id == id)
				{
					return spellAbility;
				}
			}
		}
		MainControl.logError("Could not find spell with ID " + id);
		return null;
	}

	// Token: 0x0600079D RID: 1949 RVA: 0x000246E4 File Offset: 0x000228E4
	public static AbilitySpell getSpell(string id)
	{
		if (id == null || id == "")
		{
			return null;
		}
		AbilitySpell abilitySpell = GameData.characterComponentControl.getSpell(id);
		if (abilitySpell == null)
		{
			SKALDProjectData.AbilityContainers.SpellContainer.SpellAbility spellRawData = GameData.getSpellRawData(id);
			if (spellRawData != null)
			{
				abilitySpell = new AbilitySpell(spellRawData);
				GameData.characterComponentControl.addSpell(abilitySpell);
			}
		}
		if (abilitySpell == null)
		{
			MainControl.logError("Could not find spell with ID " + id);
		}
		return abilitySpell;
	}

	// Token: 0x0600079E RID: 1950 RVA: 0x00024744 File Offset: 0x00022944
	public static List<string> getAbilityRawDataIdList()
	{
		List<string> list = new List<string>();
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.abilityContainers.getBaseList())
			{
				foreach (BaseDataObject baseDataObject2 in ((SKALDProjectData.BaseContainerObject)baseDataObject).getBaseList())
				{
					SKALDProjectData.AbilityContainers.AbilityData abilityData = (SKALDProjectData.AbilityContainers.AbilityData)baseDataObject2;
					if (!list.Contains(abilityData.id))
					{
						list.Add(abilityData.id);
					}
				}
			}
		}
		return list;
	}

	// Token: 0x0600079F RID: 1951 RVA: 0x0002483C File Offset: 0x00022A3C
	public static List<SKALDProjectData.AbilityContainers.SpellContainer.SpellAbility> getSpellRawDataList(string attributeId)
	{
		List<string> list = new List<string>();
		List<SKALDProjectData.AbilityContainers.SpellContainer.SpellAbility> list2 = new List<SKALDProjectData.AbilityContainers.SpellContainer.SpellAbility>();
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.abilityContainers.spellContainer.getBaseList())
			{
				SKALDProjectData.AbilityContainers.SpellContainer.SpellAbility spellAbility = (SKALDProjectData.AbilityContainers.SpellContainer.SpellAbility)baseDataObject;
				if (spellAbility.school.Contains(attributeId) && !list.Contains(spellAbility.id))
				{
					list.Add(spellAbility.id);
					list2.Add(spellAbility);
				}
			}
		}
		return list2;
	}

	// Token: 0x060007A0 RID: 1952 RVA: 0x00024914 File Offset: 0x00022B14
	public static List<string> getConsumableIdsForTooltips()
	{
		List<string> list = new List<string>();
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.itemContainer.consumeables.getBaseList())
			{
				SKALDProjectData.ItemDataContainers.ItemData itemData = (SKALDProjectData.ItemDataContainers.ItemData)baseDataObject;
				if (!list.Contains(itemData.id))
				{
					list.Add(itemData.id);
				}
			}
		}
		return list;
	}

	// Token: 0x060007A1 RID: 1953 RVA: 0x000249C8 File Offset: 0x00022BC8
	public static List<string> getFoodIdForTooltips()
	{
		List<string> list = new List<string>();
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.itemContainer.foods.getBaseList())
			{
				SKALDProjectData.ItemDataContainers.ItemData itemData = (SKALDProjectData.ItemDataContainers.ItemData)baseDataObject;
				if (!list.Contains(itemData.id))
				{
					list.Add(itemData.id);
				}
			}
		}
		return list;
	}

	// Token: 0x060007A2 RID: 1954 RVA: 0x00024A7C File Offset: 0x00022C7C
	public static SKALDProjectData.AbilityContainers.AbilityData getAbilityRawData(string id)
	{
		if (id == null || id == "" || id == "Unknown")
		{
			return null;
		}
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.abilityContainers.getBaseList())
			{
				foreach (BaseDataObject baseDataObject2 in ((SKALDProjectData.BaseContainerObject)baseDataObject).getBaseList())
				{
					SKALDProjectData.AbilityContainers.AbilityData abilityData = (SKALDProjectData.AbilityContainers.AbilityData)baseDataObject2;
					if (abilityData.id == id)
					{
						return abilityData;
					}
				}
			}
		}
		MainControl.logError("Could not find ability with ID " + id);
		return null;
	}

	// Token: 0x060007A3 RID: 1955 RVA: 0x00024B94 File Offset: 0x00022D94
	public static Ability getAbility(string id)
	{
		if (id == null || id == "")
		{
			return null;
		}
		Ability ability = GameData.characterComponentControl.getAbility(id);
		if (ability == null)
		{
			SKALDProjectData.AbilityContainers.AbilityData abilityRawData = GameData.getAbilityRawData(id);
			if (abilityRawData != null)
			{
				if (abilityRawData is SKALDProjectData.AbilityContainers.TriggeredAbilityContainer.TriggeredAbility)
				{
					ability = new AbilityTriggered(abilityRawData as SKALDProjectData.AbilityContainers.TriggeredAbilityContainer.TriggeredAbility);
				}
				else if (abilityRawData is SKALDProjectData.AbilityContainers.PassiveAbilityContainer.PassiveAbility)
				{
					ability = new AbilityPassive(abilityRawData as SKALDProjectData.AbilityContainers.PassiveAbilityContainer.PassiveAbility);
				}
				else
				{
					if (!(abilityRawData is SKALDProjectData.AbilityContainers.CombatManeuverContainer.CombatManueverAbility))
					{
						return null;
					}
					ability = new AbilityCombatManeuver(abilityRawData as SKALDProjectData.AbilityContainers.CombatManeuverContainer.CombatManueverAbility);
				}
				if (ability != null)
				{
					GameData.characterComponentControl.addAbility(ability);
				}
			}
		}
		if (ability == null)
		{
			MainControl.logError("Could not find ability with ID " + id);
		}
		return ability;
	}

	// Token: 0x060007A4 RID: 1956 RVA: 0x00024C34 File Offset: 0x00022E34
	public static CharacterClassArchetype getClassArchetype(string id)
	{
		if (id == null || id == "")
		{
			return null;
		}
		CharacterClassArchetype characterClassArchetype = GameData.characterFeatureControl.getArchetype(id);
		if (characterClassArchetype == null)
		{
			SKALDProjectData.ClassContainers.ClassData classRawData = GameData.getClassRawData(id);
			if (classRawData != null)
			{
				characterClassArchetype = new CharacterClassArchetype(classRawData);
			}
			else
			{
				characterClassArchetype = new CharacterClassArchetype();
			}
			GameData.characterFeatureControl.addArchetype(characterClassArchetype);
		}
		return characterClassArchetype;
	}

	// Token: 0x060007A5 RID: 1957 RVA: 0x00024C88 File Offset: 0x00022E88
	public static SKALDProjectData.RaceContainers.RaceData getRaceRawData(string id)
	{
		if (id == null || id == "")
		{
			return null;
		}
		SKALDProjectData.RaceContainers.RaceData member = GameData.raceRawDataCache.getMember(id);
		if (member != null)
		{
			return member;
		}
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.raceContainers.getBaseList())
			{
				foreach (BaseDataObject baseDataObject2 in ((SKALDProjectData.BaseContainerObject)baseDataObject).getBaseList())
				{
					SKALDProjectData.RaceContainers.RaceData raceData = (SKALDProjectData.RaceContainers.RaceData)baseDataObject2;
					if (raceData.id == id)
					{
						GameData.raceRawDataCache.addMember(raceData, id);
						return raceData;
					}
				}
			}
		}
		MainControl.logError("Could not find Race with ID " + id);
		return null;
	}

	// Token: 0x060007A6 RID: 1958 RVA: 0x00024DB4 File Offset: 0x00022FB4
	public static CharacterRace getRace(string id)
	{
		CharacterRace characterRace;
		if (id == null || id == "")
		{
			characterRace = new CharacterRace();
			GameData.characterFeatureControl.addRace(characterRace);
			return characterRace;
		}
		characterRace = GameData.characterFeatureControl.getRace(id);
		if (characterRace == null)
		{
			SKALDProjectData.RaceContainers.RaceData raceRawData = GameData.getRaceRawData(id);
			if (raceRawData != null)
			{
				characterRace = new CharacterRace(raceRawData);
				GameData.characterFeatureControl.addRace(characterRace);
			}
			else
			{
				characterRace = new CharacterRace();
			}
		}
		return characterRace;
	}

	// Token: 0x060007A7 RID: 1959 RVA: 0x00024E18 File Offset: 0x00023018
	public static SKALDProjectData.RecipeContainers.Recipe getRecipeRawData(string id)
	{
		if (id == null || id == "")
		{
			return null;
		}
		SKALDProjectData.RecipeContainers.Recipe member = GameData.recipeRawDataCache.getMember(id);
		if (member != null)
		{
			return member;
		}
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.recipeContainers.getBaseList())
			{
				foreach (BaseDataObject baseDataObject2 in ((SKALDProjectData.BaseContainerObject)baseDataObject).getBaseList())
				{
					SKALDProjectData.RecipeContainers.Recipe recipe = (SKALDProjectData.RecipeContainers.Recipe)baseDataObject2;
					if (recipe.id == id)
					{
						GameData.recipeRawDataCache.addMember(recipe, id);
						return recipe;
					}
				}
			}
		}
		MainControl.logError("Could not find Recipe with ID " + id);
		return null;
	}

	// Token: 0x060007A8 RID: 1960 RVA: 0x00024F44 File Offset: 0x00023144
	public static List<SKALDProjectData.RecipeContainers.Recipe> getAllRecipes()
	{
		List<SKALDProjectData.RecipeContainers.Recipe> list = new List<SKALDProjectData.RecipeContainers.Recipe>();
		List<string> list2 = new List<string>();
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.recipeContainers.getBaseList())
			{
				foreach (BaseDataObject baseDataObject2 in ((SKALDProjectData.BaseContainerObject)baseDataObject).getBaseList())
				{
					SKALDProjectData.RecipeContainers.Recipe recipe = (SKALDProjectData.RecipeContainers.Recipe)baseDataObject2;
					if (!list2.Contains(recipe.id))
					{
						list2.Add(recipe.id);
						list.Add(recipe);
					}
				}
			}
		}
		return list;
	}

	// Token: 0x060007A9 RID: 1961 RVA: 0x0002504C File Offset: 0x0002324C
	public static SKALDProjectData.BackgroundContainers.BackgroundData getBackgroundRawData(string id)
	{
		if (id == null || id == "")
		{
			return null;
		}
		SKALDProjectData.BackgroundContainers.BackgroundData member = GameData.backgroundRawDataCache.getMember(id);
		if (member != null)
		{
			return member;
		}
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.backgroundContainers.getBaseList())
			{
				foreach (BaseDataObject baseDataObject2 in ((SKALDProjectData.BaseContainerObject)baseDataObject).getBaseList())
				{
					SKALDProjectData.BackgroundContainers.BackgroundData backgroundData = (SKALDProjectData.BackgroundContainers.BackgroundData)baseDataObject2;
					if (backgroundData.id == id)
					{
						GameData.backgroundRawDataCache.addMember(backgroundData, id);
						return backgroundData;
					}
				}
			}
		}
		MainControl.logError("Could not find Background with ID " + id);
		return null;
	}

	// Token: 0x060007AA RID: 1962 RVA: 0x00025178 File Offset: 0x00023378
	public static CharacterBackground getBackground(string id)
	{
		CharacterBackground characterBackground;
		if (id == null || id == "")
		{
			characterBackground = new CharacterBackground();
			GameData.characterFeatureControl.addBackground(characterBackground);
			return characterBackground;
		}
		characterBackground = GameData.characterFeatureControl.getBackground(id);
		if (characterBackground == null)
		{
			SKALDProjectData.BackgroundContainers.BackgroundData backgroundRawData = GameData.getBackgroundRawData(id);
			if (backgroundRawData != null)
			{
				characterBackground = new CharacterBackground(backgroundRawData);
				GameData.characterFeatureControl.addBackground(characterBackground);
			}
			else
			{
				characterBackground = new CharacterBackground();
			}
		}
		return characterBackground;
	}

	// Token: 0x060007AB RID: 1963 RVA: 0x000251DC File Offset: 0x000233DC
	public static SkaldObjectList getClassArchetypeList()
	{
		SkaldObjectList skaldObjectList = new SkaldObjectList();
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (string id in skaldprojectData.classContainers.archetypeClassContainer.getFlatIdList())
			{
				if (!skaldObjectList.containsObject(id))
				{
					CharacterClassArchetype classArchetype = GameData.getClassArchetype(id);
					if (!classArchetype.isHidden())
					{
						skaldObjectList.add(classArchetype);
					}
				}
			}
		}
		return skaldObjectList;
	}

	// Token: 0x060007AC RID: 1964 RVA: 0x00025294 File Offset: 0x00023494
	public static SkaldObjectList getClassList()
	{
		SkaldObjectList skaldObjectList = new SkaldObjectList();
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (string id in skaldprojectData.classContainers.classContainer.getFlatIdList())
			{
				if (!skaldObjectList.containsObject(id))
				{
					CharacterClass @class = GameData.getClass(id);
					if (!@class.isHidden())
					{
						skaldObjectList.add(@class);
					}
				}
			}
		}
		return skaldObjectList;
	}

	// Token: 0x060007AD RID: 1965 RVA: 0x0002534C File Offset: 0x0002354C
	public static SkaldObjectList getBackgroundList()
	{
		SkaldObjectList skaldObjectList = new SkaldObjectList();
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (string id in skaldprojectData.backgroundContainers.backgrounContainer.getFlatIdList())
			{
				if (!skaldObjectList.containsObject(id))
				{
					CharacterBackground background = GameData.getBackground(id);
					if (!background.isHidden() && (MainControl.isDeluxeEdition() || !background.isDeluxeEdition()))
					{
						skaldObjectList.add(background);
					}
				}
			}
		}
		return skaldObjectList;
	}

	// Token: 0x060007AE RID: 1966 RVA: 0x00025414 File Offset: 0x00023614
	public static Prop instantiateProp(string id)
	{
		if (id == "")
		{
			return null;
		}
		SKALDProjectData.PropContainers.Prop propRawData = GameData.getPropRawData(id);
		if (propRawData == null)
		{
			return null;
		}
		if (!MainControl.isDeluxeEdition() && propRawData.deluxeEdition)
		{
			return null;
		}
		if (propRawData is SKALDProjectData.PropContainers.BedContainer.Bed)
		{
			if (propRawData.unique && GameData.getInstanceControl().propList.containsObject(propRawData.id))
			{
				return (PropBed)GameData.getInstanceControl().propList.getObject(propRawData.id);
			}
			PropBed propBed = new PropBed(propRawData as SKALDProjectData.PropContainers.BedContainer.Bed);
			GameData.getInstanceControl().propList.add(propBed);
			return propBed;
		}
		else if (propRawData is SKALDProjectData.PropContainers.PickupContainer.Pickup)
		{
			if (propRawData.unique && GameData.getInstanceControl().propList.containsObject(propRawData.id))
			{
				return (PropPickup)GameData.getInstanceControl().propList.getObject(propRawData.id);
			}
			PropPickup propPickup = new PropPickup(propRawData as SKALDProjectData.PropContainers.PickupContainer.Pickup);
			GameData.getInstanceControl().propList.add(propPickup);
			return propPickup;
		}
		else if (propRawData is SKALDProjectData.PropContainers.ContainerContainer.Container)
		{
			if (propRawData.unique && GameData.getInstanceControl().propList.containsObject(propRawData.id))
			{
				return (PropCont)GameData.getInstanceControl().propList.getObject(propRawData.id);
			}
			PropCont propCont = new PropCont(propRawData as SKALDProjectData.PropContainers.ContainerContainer.Container);
			GameData.getInstanceControl().propList.add(propCont);
			return propCont;
		}
		else if (propRawData is SKALDProjectData.PropContainers.TestPropContainer.TestProp)
		{
			if (propRawData.unique && GameData.getInstanceControl().propList.containsObject(propRawData.id))
			{
				return (PropTest)GameData.getInstanceControl().propList.getObject(propRawData.id);
			}
			PropTest propTest = new PropTest(propRawData as SKALDProjectData.PropContainers.TestPropContainer.TestProp);
			GameData.getInstanceControl().propList.add(propTest);
			return propTest;
		}
		else if (propRawData is SKALDProjectData.PropContainers.WarpContainer.Warp)
		{
			if (propRawData.unique && GameData.getInstanceControl().propList.containsObject(propRawData.id))
			{
				return (PropWarp)GameData.getInstanceControl().propList.getObject(propRawData.id);
			}
			PropWarp propWarp = new PropWarp(propRawData as SKALDProjectData.PropContainers.WarpContainer.Warp);
			GameData.getInstanceControl().propList.add(propWarp);
			return propWarp;
		}
		else if (propRawData is SKALDProjectData.PropContainers.BeaconContainer.Beacon)
		{
			if (propRawData.unique && GameData.getInstanceControl().propList.containsObject(propRawData.id))
			{
				return (PropWarp)GameData.getInstanceControl().propList.getObject(propRawData.id);
			}
			PropBeacon propBeacon = new PropBeacon(propRawData as SKALDProjectData.PropContainers.BeaconContainer.Beacon);
			GameData.getInstanceControl().propList.add(propBeacon);
			return propBeacon;
		}
		else if (propRawData is SKALDProjectData.PropContainers.DoorContainer.Door)
		{
			if (propRawData.unique && GameData.getInstanceControl().propList.containsObject(propRawData.id))
			{
				return (PropDoor)GameData.getInstanceControl().propList.getObject(propRawData.id);
			}
			PropDoor propDoor = new PropDoor(propRawData as SKALDProjectData.PropContainers.DoorContainer.Door);
			GameData.getInstanceControl().propList.add(propDoor);
			return propDoor;
		}
		else if (propRawData is SKALDProjectData.PropContainers.WorkBenchContainer.WorkBench)
		{
			if (propRawData.unique && GameData.getInstanceControl().propList.containsObject(propRawData.id))
			{
				return (PropDoor)GameData.getInstanceControl().propList.getObject(propRawData.id);
			}
			PropWorkBench propWorkBench = new PropWorkBench(propRawData as SKALDProjectData.PropContainers.WorkBenchContainer.WorkBench);
			GameData.getInstanceControl().propList.add(propWorkBench);
			return propWorkBench;
		}
		else if (propRawData is SKALDProjectData.PropContainers.SpawnerContainer.Spawner)
		{
			if (propRawData.unique && GameData.getInstanceControl().propList.containsObject(propRawData.id))
			{
				return (PropSpawner)GameData.getInstanceControl().propList.getObject(propRawData.id);
			}
			PropSpawner propSpawner = new PropSpawner(propRawData as SKALDProjectData.PropContainers.SpawnerContainer.Spawner);
			GameData.getInstanceControl().propList.add(propSpawner);
			return propSpawner;
		}
		else if (propRawData is SKALDProjectData.PropContainers.DecorativeContainer.Decorative)
		{
			if (propRawData.unique && GameData.getInstanceControl().propList.containsObject(propRawData.id))
			{
				return (PropDecorative)GameData.getInstanceControl().propList.getObject(propRawData.id);
			}
			PropDecorative propDecorative = new PropDecorative(propRawData as SKALDProjectData.PropContainers.DecorativeContainer.Decorative);
			GameData.getInstanceControl().propList.add(propDecorative);
			return propDecorative;
		}
		else if (propRawData is SKALDProjectData.PropContainers.InteractableContainer.Interactable)
		{
			if (propRawData.unique && GameData.getInstanceControl().propList.containsObject(propRawData.id))
			{
				return (PropInteractable)GameData.getInstanceControl().propList.getObject(propRawData.id);
			}
			PropInteractable propInteractable = new PropInteractable(propRawData as SKALDProjectData.PropContainers.InteractableContainer.Interactable);
			GameData.getInstanceControl().propList.add(propInteractable);
			return propInteractable;
		}
		else if (propRawData is SKALDProjectData.PropContainers.LightSourceContainer.LightSource)
		{
			if (propRawData.unique && GameData.getInstanceControl().propList.containsObject(propRawData.id))
			{
				return (PropInteractable)GameData.getInstanceControl().propList.getObject(propRawData.id);
			}
			PropLightSource propLightSource = new PropLightSource(propRawData as SKALDProjectData.PropContainers.LightSourceContainer.LightSource);
			GameData.getInstanceControl().propList.add(propLightSource);
			return propLightSource;
		}
		else if (propRawData is SKALDProjectData.PropContainers.TrapContainer.Trap)
		{
			if (propRawData.unique && GameData.getInstanceControl().propList.containsObject(propRawData.id))
			{
				return (PropInteractable)GameData.getInstanceControl().propList.getObject(propRawData.id);
			}
			PropTrap propTrap = new PropTrap(propRawData as SKALDProjectData.PropContainers.TrapContainer.Trap);
			GameData.getInstanceControl().propList.add(propTrap);
			return propTrap;
		}
		else if (propRawData is SKALDProjectData.PropContainers.TriggerContainer.Trigger)
		{
			if (propRawData.unique && GameData.getInstanceControl().propList.containsObject(propRawData.id))
			{
				return (PropInteractable)GameData.getInstanceControl().propList.getObject(propRawData.id);
			}
			PropTrigger propTrigger = new PropTrigger(propRawData as SKALDProjectData.PropContainers.TriggerContainer.Trigger);
			GameData.getInstanceControl().propList.add(propTrigger);
			return propTrigger;
		}
		else
		{
			if (!(propRawData is SKALDProjectData.PropContainers.InspectableContainer.Inspectable))
			{
				return null;
			}
			if (propRawData.unique && GameData.getInstanceControl().propList.containsObject(propRawData.id))
			{
				return (PropInspectable)GameData.getInstanceControl().propList.getObject(propRawData.id);
			}
			PropInspectable propInspectable = new PropInspectable(propRawData as SKALDProjectData.PropContainers.InspectableContainer.Inspectable);
			GameData.getInstanceControl().propList.add(propInspectable);
			return propInspectable;
		}
	}

	// Token: 0x060007AF RID: 1967 RVA: 0x00025A24 File Offset: 0x00023C24
	public static SKALDProjectData.PropContainers.Prop getPropRawData(string id)
	{
		if (id == "")
		{
			return null;
		}
		SKALDProjectData.PropContainers.Prop member = GameData.propRawDataCache.getMember(id);
		if (member != null)
		{
			return member;
		}
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.propContainer.getBaseList())
			{
				foreach (BaseDataObject baseDataObject2 in ((SKALDProjectData.BaseContainerObject)baseDataObject).getBaseList())
				{
					SKALDProjectData.PropContainers.Prop prop = (SKALDProjectData.PropContainers.Prop)baseDataObject2;
					if (prop.id == id)
					{
						GameData.propRawDataCache.addMember(prop, id);
						return prop;
					}
				}
			}
		}
		MainControl.logError("Did not find Prop with ID: " + id);
		return null;
	}

	// Token: 0x060007B0 RID: 1968 RVA: 0x00025B50 File Offset: 0x00023D50
	public static Store getStore(string id, Character owner)
	{
		if (id == "")
		{
			return null;
		}
		SKALDProjectData.Objects.StoreDataContainer.StoreData storeRawData = GameData.getStoreRawData(id);
		if (storeRawData != null)
		{
			return new Store(storeRawData, owner);
		}
		MainControl.logError("Did not find store with ID: " + id);
		return null;
	}

	// Token: 0x060007B1 RID: 1969 RVA: 0x00025B90 File Offset: 0x00023D90
	public static SKALDProjectData.Objects.StoreDataContainer.StoreData getStoreRawData(string id)
	{
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			SKALDProjectData.Objects.StoreDataContainer.StoreData listMember = skaldprojectData.data.storeData.getListMember(id);
			if (listMember != null)
			{
				return listMember;
			}
		}
		MainControl.logError("Did not find store with ID: " + id);
		return null;
	}

	// Token: 0x060007B2 RID: 1970 RVA: 0x00025C08 File Offset: 0x00023E08
	internal static List<string> getAllItems(string category)
	{
		category = category.ToUpper();
		List<string> list = new List<string>();
		SKALDProjectData skaldprojectData = GameData.getCoreProjectFile();
		if (category == "CONSUMABLES")
		{
			using (List<string>.Enumerator enumerator = skaldprojectData.itemContainer.consumeables.getFlatIdList().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string item = enumerator.Current;
					list.Add(item);
				}
				return list;
			}
		}
		if (category == "AMMO")
		{
			using (List<string>.Enumerator enumerator = skaldprojectData.itemContainer.ammoContainer.getFlatIdList().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string item2 = enumerator.Current;
					list.Add(item2);
				}
				return list;
			}
		}
		if (category == "OUTFITS")
		{
			using (List<string>.Enumerator enumerator = skaldprojectData.itemContainer.clothing.getFlatIdList().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string item3 = enumerator.Current;
					list.Add(item3);
				}
				return list;
			}
		}
		if (category == "ACCESSORIES")
		{
			using (List<string>.Enumerator enumerator = skaldprojectData.itemContainer.accessories.getFlatIdList().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string item4 = enumerator.Current;
					list.Add(item4);
				}
				return list;
			}
		}
		if (category == "FOODS")
		{
			using (List<string>.Enumerator enumerator = skaldprojectData.itemContainer.foods.getFlatIdList().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string item5 = enumerator.Current;
					list.Add(item5);
				}
				return list;
			}
		}
		if (category == "SHIELD")
		{
			using (List<string>.Enumerator enumerator = skaldprojectData.itemContainer.shields.getFlatIdList().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string item6 = enumerator.Current;
					list.Add(item6);
				}
				return list;
			}
		}
		if (category == "REAGENTS")
		{
			using (List<string>.Enumerator enumerator = skaldprojectData.itemContainer.reagents.getFlatIdList().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string item7 = enumerator.Current;
					list.Add(item7);
				}
				return list;
			}
		}
		if (category == "BOOKS")
		{
			using (List<string>.Enumerator enumerator = skaldprojectData.itemContainer.books.getFlatIdList().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string item8 = enumerator.Current;
					list.Add(item8);
				}
				return list;
			}
		}
		if (category == "GEMS")
		{
			using (List<string>.Enumerator enumerator = skaldprojectData.itemContainer.gems.getFlatIdList().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string item9 = enumerator.Current;
					list.Add(item9);
				}
				return list;
			}
		}
		if (category == "TRINKETS")
		{
			using (List<string>.Enumerator enumerator = skaldprojectData.itemContainer.gems.getFlatIdList().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string item10 = enumerator.Current;
					list.Add(item10);
				}
				return list;
			}
		}
		if (category == "JEWELRY")
		{
			using (List<string>.Enumerator enumerator = skaldprojectData.itemContainer.jewelry.getFlatIdList().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string item11 = enumerator.Current;
					list.Add(item11);
				}
				return list;
			}
		}
		if (category == "ADVENTURING")
		{
			using (List<string>.Enumerator enumerator = skaldprojectData.itemContainer.adventuringItems.getFlatIdList().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string item12 = enumerator.Current;
					list.Add(item12);
				}
				return list;
			}
		}
		if (category == "MELEE")
		{
			using (List<string>.Enumerator enumerator = skaldprojectData.itemContainer.meleeWeapons.getFlatIdList().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string item13 = enumerator.Current;
					list.Add(item13);
				}
				return list;
			}
		}
		if (category == "RANGED")
		{
			using (List<string>.Enumerator enumerator = skaldprojectData.itemContainer.rangedWeapons.getFlatIdList().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string item14 = enumerator.Current;
					list.Add(item14);
				}
				return list;
			}
		}
		if (category == "ARMOR")
		{
			using (List<string>.Enumerator enumerator = skaldprojectData.itemContainer.armor.getFlatIdList().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string item15 = enumerator.Current;
					list.Add(item15);
				}
				return list;
			}
		}
		if (category == "MISC")
		{
			using (List<string>.Enumerator enumerator = skaldprojectData.itemContainer.armor.getFlatIdList().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string item16 = enumerator.Current;
					list.Add(item16);
				}
				return list;
			}
		}
		if (category == "TOMES")
		{
			using (List<string>.Enumerator enumerator = skaldprojectData.itemContainer.tomes.getFlatIdList().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string item17 = enumerator.Current;
					list.Add(item17);
				}
				return list;
			}
		}
		if (category == "RECIPE")
		{
			using (List<string>.Enumerator enumerator = GameData.getFirstProjectFile().itemContainer.recipes.getFlatIdList().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string item18 = enumerator.Current;
					list.Add(item18);
				}
				return list;
			}
		}
		if (category == "TOMESFIRE")
		{
			return GameData.getSpellTomesBySchool("ATT_SpellListFire");
		}
		if (category == "TOMESAIR")
		{
			return GameData.getSpellTomesBySchool("ATT_SpellListAir");
		}
		if (category == "TOMESEARTH")
		{
			return GameData.getSpellTomesBySchool("ATT_SpellListEarth");
		}
		if (category == "TOMESWATER")
		{
			return GameData.getSpellTomesBySchool("ATT_SpellListWater");
		}
		if (category == "TOMESMIND")
		{
			return GameData.getSpellTomesBySchool("ATT_SpellListMind");
		}
		if (category == "TOMESSPIRIT")
		{
			return GameData.getSpellTomesBySchool("ATT_SpellListSpirit");
		}
		if (category == "TOMESBODY")
		{
			return GameData.getSpellTomesBySchool("ATT_SpellListBody");
		}
		if (category == "TOMESNATURE")
		{
			return GameData.getSpellTomesBySchool("ATT_SpellListNature");
		}
		if (category == "TOMESBARDIC")
		{
			return GameData.getSpellTomesBySchool("ATT_SpellListBardic");
		}
		return list;
	}

	// Token: 0x060007B3 RID: 1971 RVA: 0x000263A4 File Offset: 0x000245A4
	private static List<string> getSpellTomesBySchool(string schoolId)
	{
		List<string> list = new List<string>();
		List<ItemSpellTome> list2 = new List<ItemSpellTome>();
		foreach (string id in GameData.getFirstProjectFile().itemContainer.tomes.getFlatIdList())
		{
			list2.Add(GameData.instantiateItem(id) as ItemSpellTome);
		}
		foreach (ItemSpellTome itemSpellTome in list2)
		{
			if (itemSpellTome.getSpell().getSchoolList().Contains(schoolId))
			{
				list.Add(itemSpellTome.getId());
			}
			itemSpellTome.setToBeRemoved();
		}
		return list;
	}

	// Token: 0x060007B4 RID: 1972 RVA: 0x0002647C File Offset: 0x0002467C
	public static SKALDProjectData.Objects.DifficultyContainer.DifficultyData getDifficultyData(string id)
	{
		if (id == "")
		{
			return null;
		}
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			SKALDProjectData.Objects.DifficultyContainer.DifficultyData listMember = skaldprojectData.data.difficultyData.getListMember(id);
			if (listMember != null)
			{
				return listMember;
			}
		}
		MainControl.logError("Did not find Difficulty with ID: " + id);
		return null;
	}

	// Token: 0x060007B5 RID: 1973 RVA: 0x00026500 File Offset: 0x00024700
	public static List<string> getDifficultyDataList()
	{
		List<string> list = new List<string>();
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.data.difficultyData.getBaseList())
			{
				if (!list.Contains(baseDataObject.id))
				{
					list.Add(baseDataObject.id);
				}
			}
		}
		return list;
	}

	// Token: 0x060007B6 RID: 1974 RVA: 0x000265B0 File Offset: 0x000247B0
	public static SKALDProjectData.FeatContainers.FeatData getFeatRawData(string id)
	{
		if (id == "")
		{
			return null;
		}
		SKALDProjectData.FeatContainers.FeatData member = GameData.featCache.getMember(id);
		if (member != null)
		{
			return member;
		}
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.featContainers.getBaseList())
			{
				foreach (BaseDataObject baseDataObject2 in ((SKALDProjectData.BaseContainerObject)baseDataObject).getBaseList())
				{
					SKALDProjectData.FeatContainers.FeatData featData = (SKALDProjectData.FeatContainers.FeatData)baseDataObject2;
					if (featData.id == id)
					{
						GameData.featCache.addMember(featData, id);
						return featData;
					}
				}
			}
		}
		MainControl.logError("Did not find Feat with ID: " + id);
		return null;
	}

	// Token: 0x060007B7 RID: 1975 RVA: 0x000266DC File Offset: 0x000248DC
	public static Condition getCondition(string id)
	{
		if (id == null || id == "")
		{
			return null;
		}
		Condition condition = GameData.characterComponentControl.getCondition(id);
		if (condition == null)
		{
			SKALDProjectData.ConditionContainers.ConditionData conditionData = GameData.getConditionData(id);
			if (conditionData != null)
			{
				condition = new Condition(conditionData);
				GameData.characterComponentControl.addCondition(condition);
			}
		}
		if (condition == null)
		{
			MainControl.logError("Could not find Condition with ID " + id);
		}
		return condition;
	}

	// Token: 0x060007B8 RID: 1976 RVA: 0x0002673C File Offset: 0x0002493C
	public static string getFeatName(string id)
	{
		SKALDProjectData.FeatContainers.FeatData featRawData = GameData.getFeatRawData(id);
		if (featRawData == null)
		{
			return id;
		}
		return featRawData.title;
	}

	// Token: 0x060007B9 RID: 1977 RVA: 0x0002675C File Offset: 0x0002495C
	public static MapSaveDataContainer getMapData(string id)
	{
		if (id == "")
		{
			return null;
		}
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			MapSaveDataContainer mapData = skaldprojectData.mapDataContainer.getMapData(id);
			if (mapData != null)
			{
				return mapData;
			}
		}
		MainControl.logWarning("Did not find Map Save Data with ID: " + id);
		return null;
	}

	// Token: 0x060007BA RID: 1978 RVA: 0x000267DC File Offset: 0x000249DC
	public static void populateMapList()
	{
		MainControl.log("Initializing Map List!");
		GameData.mapList = new Dictionary<string, Map>();
		List<Map> list = new List<Map>();
		List<SKALDProjectData> list2 = new List<SKALDProjectData>();
		if (GameData.shouldOverrideCore())
		{
			list2.Add(GameData.getFirstProjectFile());
		}
		else
		{
			list2 = GameData.projectStack;
		}
		foreach (SKALDProjectData skaldprojectData in list2)
		{
			foreach (SKALDProjectData.Objects.MapMetaDataContainer.MapMetaData mapMetaData in skaldprojectData.data.mapMetaData.list)
			{
				if (mapMetaData.published && !GameData.mapList.ContainsKey(mapMetaData.id))
				{
					Map map = new Map(mapMetaData);
					GameData.mapList.Add(mapMetaData.id, map);
					list.Add(map);
				}
			}
		}
		foreach (Map map2 in list)
		{
			map2.initializeFinal();
		}
		MainControl.log("Finished Populating Map List!");
	}

	// Token: 0x060007BB RID: 1979 RVA: 0x0002692C File Offset: 0x00024B2C
	public static SKALDProjectData.Objects.MapMetaDataContainer.MapMetaData getMapRawData(string mapId)
	{
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (SKALDProjectData.Objects.MapMetaDataContainer.MapMetaData mapMetaData in skaldprojectData.data.mapMetaData.list)
			{
				if (mapMetaData.id == mapId)
				{
					return mapMetaData;
				}
			}
		}
		MainControl.logError("Could not find map with id: " + mapId);
		return null;
	}

	// Token: 0x060007BC RID: 1980 RVA: 0x000269E0 File Offset: 0x00024BE0
	public static List<Map> getMapList()
	{
		List<Map> list = new List<Map>();
		foreach (KeyValuePair<string, Map> keyValuePair in GameData.mapList)
		{
			list.Add(keyValuePair.Value);
		}
		return list;
	}

	// Token: 0x060007BD RID: 1981 RVA: 0x00026A40 File Offset: 0x00024C40
	public static SKALDProjectData.EncylopediaContainer.TutorialContainer.TutorialCategory.Tutorial getTutorial(string tutorialId)
	{
		if (tutorialId == null || tutorialId == "")
		{
			return null;
		}
		tutorialId = tutorialId.ToUpper();
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.encylopediaContainer.tutorialContainer.getBaseList())
			{
				foreach (BaseDataObject baseDataObject2 in ((SKALDProjectData.EncylopediaContainer.TutorialContainer.TutorialCategory)baseDataObject).getBaseList())
				{
					SKALDProjectData.EncylopediaContainer.TutorialContainer.TutorialCategory.Tutorial tutorial = (SKALDProjectData.EncylopediaContainer.TutorialContainer.TutorialCategory.Tutorial)baseDataObject2;
					if (tutorial.id.ToUpper() == tutorialId)
					{
						return tutorial;
					}
				}
			}
		}
		MainControl.logError("No Tutorial found with ID " + tutorialId);
		return null;
	}

	// Token: 0x060007BE RID: 1982 RVA: 0x00026B60 File Offset: 0x00024D60
	public static List<SKALDProjectData.EncylopediaContainer.Entry> getTooltipsByCategories(string categoryId)
	{
		List<string> list = new List<string>();
		List<SKALDProjectData.EncylopediaContainer.Entry> list2 = new List<SKALDProjectData.EncylopediaContainer.Entry>();
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.encylopediaContainer.tooltipContainer.getBaseList())
			{
				SKALDProjectData.EncylopediaContainer.TooltipContainer.TooltipCategory tooltipCategory = (SKALDProjectData.EncylopediaContainer.TooltipContainer.TooltipCategory)baseDataObject;
				if (tooltipCategory.id.ToUpper() == categoryId.ToUpper())
				{
					foreach (BaseDataObject baseDataObject2 in tooltipCategory.getBaseList())
					{
						SKALDProjectData.EncylopediaContainer.Entry entry = (SKALDProjectData.EncylopediaContainer.Entry)baseDataObject2;
						if (!list.Contains(entry.id))
						{
							list.Add(entry.id);
							list2.Add(entry);
						}
					}
				}
			}
		}
		return list2;
	}

	// Token: 0x060007BF RID: 1983 RVA: 0x00026C90 File Offset: 0x00024E90
	internal static SKALDProjectData.TerrainContainers.TerrainTile getTerrainById(string id, string caller)
	{
		SKALDProjectData.TerrainContainers.TerrainTile member = GameData.terrainRawDataCache.getMember(id);
		if (member != null)
		{
			return member;
		}
		MainControl.logError("Did not find terrain for ID: " + id + " from caller ID: " + caller);
		return null;
	}

	// Token: 0x060007C0 RID: 1984 RVA: 0x00026CC8 File Offset: 0x00024EC8
	private static void populateTerrainDictionary()
	{
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (BaseDataObject baseDataObject in skaldprojectData.terrainContainers.getBaseList())
			{
				foreach (BaseDataObject baseDataObject2 in ((SKALDProjectData.BaseContainerObject)baseDataObject).getBaseList())
				{
					SKALDProjectData.TerrainContainers.TerrainTile terrainTile = (SKALDProjectData.TerrainContainers.TerrainTile)baseDataObject2;
					GameData.terrainRawDataCache.addMember(terrainTile, terrainTile.id);
				}
			}
		}
	}

	// Token: 0x060007C1 RID: 1985 RVA: 0x00026DAC File Offset: 0x00024FAC
	public static SceneNode getScene(string id, string sceneSource)
	{
		if (id == "" || sceneSource == null)
		{
			return null;
		}
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (SKALDProjectData.Objects.SceneData.SceneContainer sceneContainer in skaldprojectData.data.sceneData.list)
			{
				foreach (SKALDProjectData.Objects.SceneData.SceneContainer.SceneNodeContainer sceneNodeContainer in sceneContainer.list)
				{
					if (sceneNodeContainer.testId(sceneSource))
					{
						using (List<SKALDProjectData.Objects.SceneData.SceneContainer.SceneNodeContainer.SceneNodeData>.Enumerator enumerator4 = sceneNodeContainer.list.GetEnumerator())
						{
							while (enumerator4.MoveNext())
							{
								SKALDProjectData.Objects.SceneData.SceneContainer.SceneNodeContainer.SceneNodeData sceneNodeData = enumerator4.Current;
								if (sceneNodeData.testId(id))
								{
									return new SceneNode(sceneNodeData);
								}
							}
							break;
						}
					}
				}
			}
		}
		MainControl.logError("Did not find Scene with ID: " + id + " in source " + sceneSource);
		return null;
	}

	// Token: 0x060007C2 RID: 1986 RVA: 0x00026F0C File Offset: 0x0002510C
	public static SKALDProjectData.Objects.AttributeData.AttributeContainer.Attribute getAttributeRawData(string id)
	{
		if (id == "")
		{
			return null;
		}
		SKALDProjectData.Objects.AttributeData.AttributeContainer.Attribute member = GameData.attributeCache.getMember(id);
		if (member != null)
		{
			return member;
		}
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (SKALDProjectData.Objects.AttributeData.AttributeContainer attributeContainer in skaldprojectData.data.attributeData.list)
			{
				foreach (SKALDProjectData.Objects.AttributeData.AttributeContainer.Attribute attribute in attributeContainer.list)
				{
					if (attribute.testId(id))
					{
						GameData.attributeCache.addMember(attribute, id);
						return attribute;
					}
				}
			}
		}
		MainControl.logError("Did not find Attribute with ID: " + id);
		return null;
	}

	// Token: 0x060007C3 RID: 1987 RVA: 0x0002702C File Offset: 0x0002522C
	public static List<string> getAttributeDataIdList()
	{
		List<string> list = new List<string>();
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (SKALDProjectData.Objects.AttributeData.AttributeContainer attributeContainer in skaldprojectData.data.attributeData.list)
			{
				foreach (SKALDProjectData.Objects.AttributeData.AttributeContainer.Attribute attribute in attributeContainer.list)
				{
					if (!list.Contains(attribute.id))
					{
						list.Add(attribute.id);
					}
				}
			}
		}
		return list;
	}

	// Token: 0x060007C4 RID: 1988 RVA: 0x00027120 File Offset: 0x00025320
	public static string getAttributeName(string id)
	{
		SKALDProjectData.Objects.AttributeData.AttributeContainer.Attribute attributeRawData = GameData.getAttributeRawData(id);
		if (attributeRawData == null)
		{
			return id;
		}
		if (attributeRawData.title == "")
		{
			return id;
		}
		return attributeRawData.title;
	}

	// Token: 0x060007C5 RID: 1989 RVA: 0x00027154 File Offset: 0x00025354
	public static string getAttributeSuffix(string id)
	{
		SKALDProjectData.Objects.AttributeData.AttributeContainer.Attribute attributeRawData = GameData.getAttributeRawData(id);
		if (attributeRawData == null)
		{
			return "";
		}
		return attributeRawData.suffixCharacter;
	}

	// Token: 0x060007C6 RID: 1990 RVA: 0x00027177 File Offset: 0x00025377
	public static string getAttributeName(AttributesControl.CoreAttributes attribute)
	{
		return GameData.getAttributeName(attribute.ToString());
	}

	// Token: 0x060007C7 RID: 1991 RVA: 0x0002718C File Offset: 0x0002538C
	public static bool getSceneSetMainCharacter(string id, string sceneSource)
	{
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			foreach (SKALDProjectData.Objects.SceneData.SceneContainer sceneContainer in skaldprojectData.data.sceneData.list)
			{
				foreach (SKALDProjectData.Objects.SceneData.SceneContainer.SceneNodeContainer sceneNodeContainer in sceneContainer.list)
				{
					if (sceneNodeContainer.testId(sceneSource))
					{
						using (List<SKALDProjectData.Objects.SceneData.SceneContainer.SceneNodeContainer.SceneNodeData>.Enumerator enumerator4 = sceneNodeContainer.list.GetEnumerator())
						{
							while (enumerator4.MoveNext())
							{
								SKALDProjectData.Objects.SceneData.SceneContainer.SceneNodeContainer.SceneNodeData sceneNodeData = enumerator4.Current;
								if (sceneNodeData.testId(id))
								{
									return sceneNodeData.setMainCharacter;
								}
							}
							break;
						}
					}
				}
			}
		}
		return false;
	}

	// Token: 0x060007C8 RID: 1992 RVA: 0x000272C0 File Offset: 0x000254C0
	public static Map getMap(string id, string callerId)
	{
		if (GameData.mapList == null)
		{
			MainControl.logError("RELOADING DATA DUE TO MISSING MAP LIST!!!");
			GameData.loadData();
		}
		if (id == null || id == "")
		{
			return null;
		}
		if (GameData.mapList.ContainsKey(id))
		{
			return GameData.mapList[id];
		}
		MainControl.logError("Did not find Map with ID: " + id + " Caller is " + callerId);
		return null;
	}

	// Token: 0x060007C9 RID: 1993 RVA: 0x00027328 File Offset: 0x00025528
	public static string getRandomStringListEntry(string listId)
	{
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			SKALDProjectData.Objects.StringListData.StringListContainer listMember = skaldprojectData.data.stringListData.getListMember(listId);
			if (listMember != null)
			{
				int index = Random.Range(0, listMember.list.Count);
				return listMember.list[index].description;
			}
		}
		return "";
	}

	// Token: 0x060007CA RID: 1994 RVA: 0x000273B4 File Offset: 0x000255B4
	public static string getStringListEntry(string listId, string entryId)
	{
		foreach (SKALDProjectData skaldprojectData in GameData.projectStack)
		{
			SKALDProjectData.Objects.StringListData.StringListContainer listMember = skaldprojectData.data.stringListData.getListMember(listId);
			if (listMember != null)
			{
				foreach (BaseDataObject baseDataObject in listMember.getBaseList())
				{
					SKALDProjectData.Objects.StringListData.StringListContainer.StringData stringData = (SKALDProjectData.Objects.StringListData.StringListContainer.StringData)baseDataObject;
					if (stringData.id == entryId)
					{
						return stringData.description;
					}
				}
			}
		}
		return "";
	}

	// Token: 0x0400016F RID: 367
	private const string PROJECT_NAME = "SkaldProject";

	// Token: 0x04000170 RID: 368
	private static List<SKALDProjectData> projectStack;

	// Token: 0x04000171 RID: 369
	private static Dictionary<string, Map> mapList;

	// Token: 0x04000172 RID: 370
	private static SKALDProjectData coreProjectFile;

	// Token: 0x04000173 RID: 371
	private static SKALDProjectData customProjectFile;

	// Token: 0x04000174 RID: 372
	private static CharacterFeatureControl characterFeatureControl;

	// Token: 0x04000175 RID: 373
	private static CharacterComponentControl characterComponentControl;

	// Token: 0x04000176 RID: 374
	private static GameData.SkaldInstanceControl instanceControl;

	// Token: 0x04000177 RID: 375
	private static GameData.RawDataCache<SKALDProjectData.PropContainers.Prop> propRawDataCache;

	// Token: 0x04000178 RID: 376
	private static GameData.RawDataCache<SKALDProjectData.ItemDataContainers.ItemData> itemRawDataCache;

	// Token: 0x04000179 RID: 377
	private static GameData.RawDataCache<SKALDProjectData.CharacterContainers.Character> characterRawDataCache;

	// Token: 0x0400017A RID: 378
	private static GameData.RawDataCache<SKALDProjectData.VehicleContainers.Vehicle> vehicleRawDataCache;

	// Token: 0x0400017B RID: 379
	private static GameData.RawDataCache<SKALDProjectData.TerrainContainers.TerrainTile> terrainRawDataCache;

	// Token: 0x0400017C RID: 380
	private static GameData.RawDataCache<SKALDProjectData.ClassContainers.ClassData> classRawDataCache;

	// Token: 0x0400017D RID: 381
	private static GameData.RawDataCache<SKALDProjectData.RaceContainers.RaceData> raceRawDataCache;

	// Token: 0x0400017E RID: 382
	private static GameData.RawDataCache<SKALDProjectData.BackgroundContainers.BackgroundData> backgroundRawDataCache;

	// Token: 0x0400017F RID: 383
	private static GameData.RawDataCache<SKALDProjectData.RecipeContainers.Recipe> recipeRawDataCache;

	// Token: 0x04000180 RID: 384
	private static GameData.RawDataCache<SKALDProjectData.JournalContainers.JournalEntry> journalRawDataCache;

	// Token: 0x04000181 RID: 385
	private static GameData.RawDataCache<SKALDProjectData.Objects.FactionDataContainer.FactionData> factionRawDataCache;

	// Token: 0x04000182 RID: 386
	private static GameData.RawDataCache<SKALDProjectData.Objects.AttributeData.AttributeContainer.Attribute> attributeCache;

	// Token: 0x04000183 RID: 387
	private static GameData.RawDataCache<SKALDProjectData.ConditionContainers.ConditionData> conditionCache;

	// Token: 0x04000184 RID: 388
	private static GameData.RawDataCache<SKALDProjectData.EffectContainers.EffectData> effectCache;

	// Token: 0x04000185 RID: 389
	private static GameData.RawDataCache<SKALDProjectData.FeatContainers.FeatData> featCache;

	// Token: 0x04000186 RID: 390
	private static GameData.RawDataCache<SKALDProjectData.Objects.EncounterContainer.Encounter> encounterCache;

	// Token: 0x04000187 RID: 391
	private static GameData.RawDataCache<SKALDProjectData.Objects.EventContainer.Event> eventCache;

	// Token: 0x04000188 RID: 392
	private static GameData.RawDataCache<SKALDProjectData.ApperanceElementContainers.ApperanceElement> portraitCache;

	// Token: 0x04000189 RID: 393
	private static GameData.RawDataCache<SKALDProjectData.ApperanceElementContainers.ApperanceElement> hairCache;

	// Token: 0x0400018A RID: 394
	private static GameData.RawDataCache<SKALDProjectData.ApperanceElementContainers.ApperanceElement> beardCache;

	// Token: 0x0400018B RID: 395
	private static GameData.RawDataCache<SKALDProjectData.EnchantmentContainers.Enchantment> enchantmentCache;

	// Token: 0x020001EB RID: 491
	[Serializable]
	public class SkaldInstanceControl
	{
		// Token: 0x0600175D RID: 5981 RVA: 0x00067CEC File Offset: 0x00065EEC
		public SkaldInstanceControl()
		{
			this.characterList = new GameData.SkaldInstanceControl.CharacterList();
			this.propList = new GameData.SkaldInstanceControl.PropList();
			this.itemList = new GameData.SkaldInstanceControl.ItemList();
			this.vehicleList = new GameData.SkaldInstanceControl.VehicleList();
			this.metaList = new List<GameData.SkaldInstanceControl.InstanceList>();
			this.metaList.Add(this.characterList);
			this.metaList.Add(this.propList);
			this.metaList.Add(this.itemList);
			this.metaList.Add(this.vehicleList);
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x00067D7C File Offset: 0x00065F7C
		public string print()
		{
			string text = "";
			int num = 0;
			foreach (GameData.SkaldInstanceControl.InstanceList instanceList in this.metaList)
			{
				text = text + instanceList.printListSimplifiedString() + "\n";
				num += instanceList.getCount();
			}
			return "Total Instance Count: " + num.ToString() + "\n\n" + text;
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x00067E04 File Offset: 0x00066004
		public void purgeInstancesByMapID(string id)
		{
			foreach (GameData.SkaldInstanceControl.InstanceList instanceList in this.metaList)
			{
				if (instanceList != null)
				{
					instanceList.purgeByMapId(id);
				}
			}
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x00067E5C File Offset: 0x0006605C
		public GameData.SkaldInstanceControl.InstanceSaveData getInstanceSaveData()
		{
			return new GameData.SkaldInstanceControl.InstanceSaveData(this.characterList, this.itemList, this.propList, this.vehicleList);
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x00067E7B File Offset: 0x0006607B
		public void applyInstanceSaveData(GameData.SkaldInstanceControl.InstanceSaveData saveData)
		{
			this.propList = saveData.propData;
			this.characterList = saveData.characterData;
			this.itemList = saveData.itemData;
			this.vehicleList = saveData.vehicleList;
		}

		// Token: 0x04000794 RID: 1940
		private List<GameData.SkaldInstanceControl.InstanceList> metaList;

		// Token: 0x04000795 RID: 1941
		public GameData.SkaldInstanceControl.CharacterList characterList;

		// Token: 0x04000796 RID: 1942
		public GameData.SkaldInstanceControl.PropList propList;

		// Token: 0x04000797 RID: 1943
		public GameData.SkaldInstanceControl.ItemList itemList;

		// Token: 0x04000798 RID: 1944
		public GameData.SkaldInstanceControl.VehicleList vehicleList;

		// Token: 0x020002FF RID: 767
		[Serializable]
		public class InstanceSaveData
		{
			// Token: 0x06001C1E RID: 7198 RVA: 0x00079AD0 File Offset: 0x00077CD0
			public InstanceSaveData(GameData.SkaldInstanceControl.CharacterList characterData, GameData.SkaldInstanceControl.ItemList itemData, GameData.SkaldInstanceControl.PropList propData, GameData.SkaldInstanceControl.VehicleList vehicleList)
			{
				this.characterData = characterData;
				this.itemData = itemData;
				this.propData = propData;
				this.vehicleList = vehicleList;
				if (MainControl.debugFunctions)
				{
					MainControl.log(string.Concat(new string[]
					{
						"SAVING INSTANCE LIST\nCharacter Count: ",
						characterData.getCount().ToString(),
						"\nItem Count: ",
						itemData.getCount().ToString(),
						"\nProp Count: ",
						propData.getCount().ToString()
					}));
				}
			}

			// Token: 0x04000AA0 RID: 2720
			public GameData.SkaldInstanceControl.CharacterList characterData;

			// Token: 0x04000AA1 RID: 2721
			public GameData.SkaldInstanceControl.PropList propData;

			// Token: 0x04000AA2 RID: 2722
			public GameData.SkaldInstanceControl.ItemList itemData;

			// Token: 0x04000AA3 RID: 2723
			public GameData.SkaldInstanceControl.VehicleList vehicleList;
		}

		// Token: 0x02000300 RID: 768
		[Serializable]
		public class VehicleList : GameData.SkaldInstanceControl.InstanceList
		{
			// Token: 0x06001C1F RID: 7199 RVA: 0x00079B62 File Offset: 0x00077D62
			public VehicleList()
			{
				this.setName("VEHICLE LIST");
			}
		}

		// Token: 0x02000301 RID: 769
		[Serializable]
		public class PropList : GameData.SkaldInstanceControl.InstanceList
		{
			// Token: 0x06001C20 RID: 7200 RVA: 0x00079B76 File Offset: 0x00077D76
			public PropList()
			{
				this.setName("PROP LIST");
			}
		}

		// Token: 0x02000302 RID: 770
		[Serializable]
		public class ItemList : GameData.SkaldInstanceControl.InstanceList
		{
			// Token: 0x06001C21 RID: 7201 RVA: 0x00079B8A File Offset: 0x00077D8A
			public ItemList()
			{
				this.setName("ITEM LIST");
			}
		}

		// Token: 0x02000303 RID: 771
		[Serializable]
		public class CharacterList : GameData.SkaldInstanceControl.InstanceList
		{
			// Token: 0x06001C22 RID: 7202 RVA: 0x00079B9E File Offset: 0x00077D9E
			public CharacterList()
			{
				this.setName("CHARACTER LIST");
			}
		}

		// Token: 0x02000304 RID: 772
		[Serializable]
		public class InstanceList : SkaldBaseList
		{
			// Token: 0x06001C23 RID: 7203 RVA: 0x00079BB4 File Offset: 0x00077DB4
			public virtual List<SkaldWorldObject> getMemberByMap(string mapId)
			{
				List<SkaldWorldObject> list = new List<SkaldWorldObject>();
				List<SkaldInstanceObject> list2 = new List<SkaldInstanceObject>();
				foreach (SkaldBaseObject skaldBaseObject in this.objectList)
				{
					SkaldWorldObject skaldWorldObject = (SkaldWorldObject)skaldBaseObject;
					try
					{
						if (skaldWorldObject.shouldBeRemovedFromGame())
						{
							list2.Add(skaldWorldObject);
						}
						else if (skaldWorldObject.getContainerMapId() == mapId)
						{
							list.Add(skaldWorldObject);
						}
					}
					catch (Exception ex)
					{
						string[] array = new string[8];
						array[0] = "Found faulty instance in list ";
						array[1] = this.getName();
						array[2] = " on map: ";
						array[3] = mapId;
						array[4] = " at index: ";
						array[5] = this.objectList.IndexOf(skaldWorldObject).ToString();
						array[6] = "\n\n";
						int num = 7;
						Exception ex2 = ex;
						array[num] = ((ex2 != null) ? ex2.ToString() : null);
						MainControl.logError(string.Concat(array));
						list2.Add(skaldWorldObject);
					}
				}
				this.purgeByList(list2);
				return list;
			}

			// Token: 0x06001C24 RID: 7204 RVA: 0x00079CC4 File Offset: 0x00077EC4
			public virtual List<SkaldWorldObject> getMembersById(string id)
			{
				List<SkaldWorldObject> list = new List<SkaldWorldObject>();
				List<SkaldInstanceObject> list2 = new List<SkaldInstanceObject>();
				foreach (SkaldBaseObject skaldBaseObject in this.objectList)
				{
					SkaldWorldObject skaldWorldObject = (SkaldWorldObject)skaldBaseObject;
					try
					{
						if (skaldWorldObject.shouldBeRemovedFromGame())
						{
							list2.Add(skaldWorldObject);
						}
						else if (skaldWorldObject.getId() == id)
						{
							list.Add(skaldWorldObject);
						}
					}
					catch (Exception ex)
					{
						string[] array = new string[8];
						array[0] = "Found faulty instance in list ";
						array[1] = this.getName();
						array[2] = " on map: ";
						array[3] = id;
						array[4] = " at index: ";
						array[5] = this.objectList.IndexOf(skaldWorldObject).ToString();
						array[6] = "\n\n";
						int num = 7;
						Exception ex2 = ex;
						array[num] = ((ex2 != null) ? ex2.ToString() : null);
						MainControl.logError(string.Concat(array));
						list2.Add(skaldWorldObject);
					}
				}
				this.purgeByList(list2);
				return list;
			}

			// Token: 0x06001C25 RID: 7205 RVA: 0x00079DD4 File Offset: 0x00077FD4
			public void purgeByMapId(string id)
			{
				List<SkaldInstanceObject> list = new List<SkaldInstanceObject>();
				foreach (SkaldWorldObject skaldWorldObject in this.getMemberByMap(id))
				{
					skaldWorldObject.setToBeRemoved();
					list.Add(skaldWorldObject);
				}
				this.purgeByList(list);
			}

			// Token: 0x06001C26 RID: 7206 RVA: 0x00079E3C File Offset: 0x0007803C
			public override string printListSimplifiedString()
			{
				string text = this.getName().ToUpper() + "\n";
				if (this.objectList.Count == 0)
				{
					return "Empty";
				}
				for (int i = 0; i < this.objectList.Count; i++)
				{
					SkaldWorldObject skaldWorldObject = this.objectList[i] as SkaldWorldObject;
					text = string.Concat(new string[]
					{
						text,
						i.ToString(),
						") ",
						skaldWorldObject.printStatus(),
						"\n"
					});
				}
				return text;
			}

			// Token: 0x06001C27 RID: 7207 RVA: 0x00079ED0 File Offset: 0x000780D0
			private void purgeByList(List<SkaldInstanceObject> purgeList)
			{
				foreach (SkaldInstanceObject item in purgeList)
				{
					this.objectList.Remove(item);
				}
			}
		}
	}

	// Token: 0x020001EC RID: 492
	private class RawDataCache<T>
	{
		// Token: 0x06001762 RID: 5986 RVA: 0x00067EAD File Offset: 0x000660AD
		public RawDataCache()
		{
			this.dataDictionary = new Dictionary<string, T>();
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x00067EC0 File Offset: 0x000660C0
		public T getMember(string id)
		{
			if (this.dataDictionary.ContainsKey(id))
			{
				return this.dataDictionary[id];
			}
			return default(T);
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x00067EF1 File Offset: 0x000660F1
		public void addMember(T member, string id)
		{
			if (!this.dataDictionary.ContainsKey(id))
			{
				this.dataDictionary.Add(id, member);
			}
		}

		// Token: 0x04000799 RID: 1945
		private Dictionary<string, T> dataDictionary;
	}
}
