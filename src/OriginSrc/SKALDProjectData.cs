using System;
using System.Collections.Generic;

// Token: 0x02000063 RID: 99
[Serializable]
public class SKALDProjectData : BaseDataObject
{
	// Token: 0x060008CC RID: 2252 RVA: 0x0002A93C File Offset: 0x00028B3C
	public List<BaseDataObject> getBaseList()
	{
		return new List<BaseDataObject>
		{
			this.data,
			this.itemContainer,
			this.loadoutContainers,
			this.cutsceneContainers,
			this.recipeContainers,
			this.propContainer,
			this.terrainContainers,
			this.backgroundContainers,
			this.raceContainers,
			this.classContainers,
			this.featContainers,
			this.abilityContainers,
			this.achievementContainers,
			this.conditionContainers,
			this.effectContainers,
			this.journalContainers,
			this.characterContainer,
			this.vehicleContainer,
			this.animationContainer,
			this.questContainers,
			this.uiContainer,
			this.encylopediaContainer,
			this.mapDataContainer,
			this.enchantmentContainers
		};
	}

	// Token: 0x0400020A RID: 522
	public SKALDProjectData.MetaData metaData = new SKALDProjectData.MetaData();

	// Token: 0x0400020B RID: 523
	public SKALDProjectData.Objects data = new SKALDProjectData.Objects();

	// Token: 0x0400020C RID: 524
	public SKALDProjectData.ItemDataContainers itemContainer = new SKALDProjectData.ItemDataContainers();

	// Token: 0x0400020D RID: 525
	public SKALDProjectData.LoadoutDataContainers loadoutContainers = new SKALDProjectData.LoadoutDataContainers();

	// Token: 0x0400020E RID: 526
	public SKALDProjectData.CutSceneDataContainers cutsceneContainers = new SKALDProjectData.CutSceneDataContainers();

	// Token: 0x0400020F RID: 527
	public SKALDProjectData.PropContainers propContainer = new SKALDProjectData.PropContainers();

	// Token: 0x04000210 RID: 528
	public SKALDProjectData.TerrainContainers terrainContainers = new SKALDProjectData.TerrainContainers();

	// Token: 0x04000211 RID: 529
	public SKALDProjectData.RaceContainers raceContainers = new SKALDProjectData.RaceContainers();

	// Token: 0x04000212 RID: 530
	public SKALDProjectData.ClassContainers classContainers = new SKALDProjectData.ClassContainers();

	// Token: 0x04000213 RID: 531
	public SKALDProjectData.BackgroundContainers backgroundContainers = new SKALDProjectData.BackgroundContainers();

	// Token: 0x04000214 RID: 532
	public SKALDProjectData.FeatContainers featContainers = new SKALDProjectData.FeatContainers();

	// Token: 0x04000215 RID: 533
	public SKALDProjectData.AbilityContainers abilityContainers = new SKALDProjectData.AbilityContainers();

	// Token: 0x04000216 RID: 534
	public SKALDProjectData.AchievementContainers achievementContainers = new SKALDProjectData.AchievementContainers();

	// Token: 0x04000217 RID: 535
	public SKALDProjectData.ApperanceElementContainers apperanceElementContainers = new SKALDProjectData.ApperanceElementContainers();

	// Token: 0x04000218 RID: 536
	public SKALDProjectData.ConditionContainers conditionContainers = new SKALDProjectData.ConditionContainers();

	// Token: 0x04000219 RID: 537
	public SKALDProjectData.EffectContainers effectContainers = new SKALDProjectData.EffectContainers();

	// Token: 0x0400021A RID: 538
	public SKALDProjectData.JournalContainers journalContainers = new SKALDProjectData.JournalContainers();

	// Token: 0x0400021B RID: 539
	public SKALDProjectData.VehicleContainers vehicleContainer = new SKALDProjectData.VehicleContainers();

	// Token: 0x0400021C RID: 540
	public SKALDProjectData.RecipeContainers recipeContainers = new SKALDProjectData.RecipeContainers();

	// Token: 0x0400021D RID: 541
	public SKALDProjectData.CharacterContainers characterContainer = new SKALDProjectData.CharacterContainers();

	// Token: 0x0400021E RID: 542
	public SKALDProjectData.AnimationContainers animationContainer = new SKALDProjectData.AnimationContainers();

	// Token: 0x0400021F RID: 543
	public SKALDProjectData.QuestContainers questContainers = new SKALDProjectData.QuestContainers();

	// Token: 0x04000220 RID: 544
	public SKALDProjectData.UIContainers uiContainer = new SKALDProjectData.UIContainers();

	// Token: 0x04000221 RID: 545
	public SKALDProjectData.EncylopediaContainer encylopediaContainer = new SKALDProjectData.EncylopediaContainer();

	// Token: 0x04000222 RID: 546
	public SKALDProjectData.MapDataContainer mapDataContainer = new SKALDProjectData.MapDataContainer();

	// Token: 0x04000223 RID: 547
	public SKALDProjectData.EnchantmentContainers enchantmentContainers = new SKALDProjectData.EnchantmentContainers();

	// Token: 0x0200020E RID: 526
	[Serializable]
	public class MetaData : SKALDProjectData.GameDataObject
	{
		// Token: 0x040007EF RID: 2031
		public bool startAtCharacterCreation;

		// Token: 0x040007F0 RID: 2032
		public bool standalone;

		// Token: 0x040007F1 RID: 2033
		public int maxLevel;

		// Token: 0x040007F2 RID: 2034
		public int startingDP;

		// Token: 0x040007F3 RID: 2035
		public int levelUpDP;

		// Token: 0x040007F4 RID: 2036
		public string startingMap;

		// Token: 0x040007F5 RID: 2037
		public string startingTrigger;

		// Token: 0x040007F6 RID: 2038
		public string version;

		// Token: 0x040007F7 RID: 2039
		public string testSnippet;
	}

	// Token: 0x0200020F RID: 527
	[Serializable]
	public class Objects : SKALDProjectData.BaseContainerObject
	{
		// Token: 0x06001852 RID: 6226 RVA: 0x0006B408 File Offset: 0x00069608
		public Objects()
		{
			this.sceneData = new SKALDProjectData.Objects.SceneData();
			this.factionData = new SKALDProjectData.Objects.FactionDataContainer();
			this.mapMetaData = new SKALDProjectData.Objects.MapMetaDataContainer();
			this.storeData = new SKALDProjectData.Objects.StoreDataContainer();
			this.apperanceData = new SKALDProjectData.Objects.ApperanceDataContainer();
			this.difficultyData = new SKALDProjectData.Objects.DifficultyContainer();
			this.stringListData = new SKALDProjectData.Objects.StringListData();
			this.attributeData = new SKALDProjectData.Objects.AttributeData();
			this.variableContainer = new SKALDProjectData.Objects.VariableContainer();
			this.scriptContainer = new SKALDProjectData.Objects.ScriptContainer();
			this.eventContainer = new SKALDProjectData.Objects.EventContainer();
			this.encounterContainer = new SKALDProjectData.Objects.EncounterContainer();
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x0006B4A0 File Offset: 0x000696A0
		public override List<BaseDataObject> getBaseList()
		{
			List<BaseDataObject> baseList = base.getBaseList();
			baseList.Add(this.storeData);
			baseList.Add(this.mapMetaData);
			baseList.Add(this.factionData);
			baseList.Add(this.sceneData);
			baseList.Add(this.attributeData);
			baseList.Add(this.stringListData);
			baseList.Add(this.variableContainer);
			baseList.Add(this.scriptContainer);
			baseList.Add(this.eventContainer);
			baseList.Add(this.encounterContainer);
			baseList.Add(this.difficultyData);
			baseList.Add(this.apperanceData);
			return baseList;
		}

		// Token: 0x040007F8 RID: 2040
		public SKALDProjectData.Objects.SceneData sceneData;

		// Token: 0x040007F9 RID: 2041
		public SKALDProjectData.Objects.FactionDataContainer factionData;

		// Token: 0x040007FA RID: 2042
		public SKALDProjectData.Objects.MapMetaDataContainer mapMetaData;

		// Token: 0x040007FB RID: 2043
		public SKALDProjectData.Objects.StoreDataContainer storeData;

		// Token: 0x040007FC RID: 2044
		public SKALDProjectData.Objects.ApperanceDataContainer apperanceData;

		// Token: 0x040007FD RID: 2045
		public SKALDProjectData.Objects.DifficultyContainer difficultyData;

		// Token: 0x040007FE RID: 2046
		public SKALDProjectData.Objects.AttributeData attributeData;

		// Token: 0x040007FF RID: 2047
		public SKALDProjectData.Objects.StringListData stringListData;

		// Token: 0x04000800 RID: 2048
		public SKALDProjectData.Objects.VariableContainer variableContainer;

		// Token: 0x04000801 RID: 2049
		public SKALDProjectData.Objects.ScriptContainer scriptContainer;

		// Token: 0x04000802 RID: 2050
		public SKALDProjectData.Objects.EventContainer eventContainer;

		// Token: 0x04000803 RID: 2051
		public SKALDProjectData.Objects.EncounterContainer encounterContainer;

		// Token: 0x02000330 RID: 816
		[Serializable]
		public class SceneData : SKALDProjectData.DeepContainerObject<SKALDProjectData.Objects.SceneData.SceneContainer>
		{
			// Token: 0x020003F4 RID: 1012
			[Serializable]
			public class SceneContainer : SKALDProjectData.DeepContainerObject<SKALDProjectData.Objects.SceneData.SceneContainer.SceneNodeContainer>
			{
				// Token: 0x02000436 RID: 1078
				[Serializable]
				public class SceneNodeContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.Objects.SceneData.SceneContainer.SceneNodeContainer.SceneNodeData>
				{
					// Token: 0x0200043C RID: 1084
					[Serializable]
					public class SceneNodeData : SKALDProjectData.SpecificContainerObject<SKALDProjectData.Objects.SceneData.SceneContainer.SceneNodeContainer.SceneNodeData.Exit>
					{
						// Token: 0x04000DD3 RID: 3539
						public bool setMainCharacter;

						// Token: 0x04000DD4 RID: 3540
						public bool root;

						// Token: 0x04000DD5 RID: 3541
						public bool clearIfDead;

						// Token: 0x04000DD6 RID: 3542
						public string title;

						// Token: 0x04000DD7 RID: 3543
						public string description;

						// Token: 0x04000DD8 RID: 3544
						public string imagePath;

						// Token: 0x04000DD9 RID: 3545
						public string launchTrigger;

						// Token: 0x0200043D RID: 1085
						[Serializable]
						public class Exit : BaseDataObject
						{
							// Token: 0x04000DDA RID: 3546
							public static int exitNumber = 1;

							// Token: 0x04000DDB RID: 3547
							public bool randomRoll;

							// Token: 0x04000DDC RID: 3548
							public int difficulty;

							// Token: 0x04000DDD RID: 3549
							public string option;

							// Token: 0x04000DDE RID: 3550
							public string target;

							// Token: 0x04000DDF RID: 3551
							public string alternateTarget;

							// Token: 0x04000DE0 RID: 3552
							public string key;

							// Token: 0x04000DE1 RID: 3553
							public string exitTrigger;

							// Token: 0x04000DE2 RID: 3554
							public string testAttribute;
						}
					}
				}
			}
		}

		// Token: 0x02000331 RID: 817
		[Serializable]
		public class AttributeData : SKALDProjectData.DeepContainerObject<SKALDProjectData.Objects.AttributeData.AttributeContainer>
		{
			// Token: 0x020003F5 RID: 1013
			[Serializable]
			public class AttributeContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.Objects.AttributeData.AttributeContainer.Attribute>
			{
				// Token: 0x02000437 RID: 1079
				[Serializable]
				public class Attribute : SKALDProjectData.GameDataObject
				{
					// Token: 0x04000DA6 RID: 3494
					public int startingValue;

					// Token: 0x04000DA7 RID: 3495
					public int ranks;

					// Token: 0x04000DA8 RID: 3496
					public int levelFactor;

					// Token: 0x04000DA9 RID: 3497
					public int minValue;

					// Token: 0x04000DAA RID: 3498
					public int maxValue;

					// Token: 0x04000DAB RID: 3499
					public int rootFactorPercentage;

					// Token: 0x04000DAC RID: 3500
					public string rootAbility;

					// Token: 0x04000DAD RID: 3501
					public string suffixCharacter;

					// Token: 0x04000DAE RID: 3502
					public string attributeType;

					// Token: 0x04000DAF RID: 3503
					public string abbreviation;

					// Token: 0x04000DB0 RID: 3504
					public bool countArmorEncumberance;

					// Token: 0x04000DB1 RID: 3505
					public bool countGloveEncumberance;

					// Token: 0x04000DB2 RID: 3506
					public bool countHelmetEncumberance;

					// Token: 0x04000DB3 RID: 3507
					public bool countShoeEncumberance;

					// Token: 0x04000DB4 RID: 3508
					public bool countOutfitReaction;

					// Token: 0x04000DB5 RID: 3509
					public bool usesPrimaryAttributeAsRoot;
				}
			}
		}

		// Token: 0x02000332 RID: 818
		[Serializable]
		public class StringListData : SKALDProjectData.SpecificContainerObject<SKALDProjectData.Objects.StringListData.StringListContainer>
		{
			// Token: 0x020003F6 RID: 1014
			[Serializable]
			public class StringListContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.Objects.StringListData.StringListContainer.StringData>
			{
				// Token: 0x02000438 RID: 1080
				[Serializable]
				public class StringData : SKALDProjectData.GameDataObject
				{
				}
			}
		}

		// Token: 0x02000333 RID: 819
		[Serializable]
		public class FactionDataContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.Objects.FactionDataContainer.FactionData>
		{
			// Token: 0x020003F7 RID: 1015
			[Serializable]
			public class FactionData : SKALDProjectData.GameDataObject
			{
				// Token: 0x04000C9F RID: 3231
				public bool willGoHostile;

				// Token: 0x04000CA0 RID: 3232
				public bool willGoFriendly;

				// Token: 0x04000CA1 RID: 3233
				public int playerRelation;

				// Token: 0x04000CA2 RID: 3234
				public string parent;

				// Token: 0x04000CA3 RID: 3235
				public string playerStance;

				// Token: 0x04000CA4 RID: 3236
				public string enemyFaction;

				// Token: 0x04000CA5 RID: 3237
				public string allyFaction;

				// Token: 0x04000CA6 RID: 3238
				public string apperancePack;

				// Token: 0x04000CA7 RID: 3239
				public string loadout;

				// Token: 0x04000CA8 RID: 3240
				public string hostileTrigger;

				// Token: 0x04000CA9 RID: 3241
				public string friendlyTrigger;
			}
		}

		// Token: 0x02000334 RID: 820
		[Serializable]
		public class ApperanceDataContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.Objects.ApperanceDataContainer.ApperanceData>
		{
			// Token: 0x020003F8 RID: 1016
			[Serializable]
			public class ApperanceData : SKALDProjectData.GameDataObject
			{
				// Token: 0x04000CAA RID: 3242
				public string portraitPath;

				// Token: 0x04000CAB RID: 3243
				public string femalePortraitPath;

				// Token: 0x04000CAC RID: 3244
				public List<string> mainColors;

				// Token: 0x04000CAD RID: 3245
				public List<string> secColors;

				// Token: 0x04000CAE RID: 3246
				public List<string> tertiaryColors;

				// Token: 0x04000CAF RID: 3247
				public List<string> skinColors;

				// Token: 0x04000CB0 RID: 3248
				public List<string> hairColors;

				// Token: 0x04000CB1 RID: 3249
				public List<string> hairStyles;

				// Token: 0x04000CB2 RID: 3250
				public List<string> beardStyles;

				// Token: 0x04000CB3 RID: 3251
				public List<string> femaleHairStyles;

				// Token: 0x04000CB4 RID: 3252
				public string idleAnim;

				// Token: 0x04000CB5 RID: 3253
				public string femaleIdleAnim;
			}
		}

		// Token: 0x02000335 RID: 821
		[Serializable]
		public class MapMetaDataContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.Objects.MapMetaDataContainer.MapMetaData>
		{
			// Token: 0x020003F9 RID: 1017
			[Serializable]
			public class MapMetaData : SKALDProjectData.GameDataObject
			{
				// Token: 0x04000CB6 RID: 3254
				public bool enterPrompt;

				// Token: 0x04000CB7 RID: 3255
				public bool directionalExit;

				// Token: 0x04000CB8 RID: 3256
				public bool overland;

				// Token: 0x04000CB9 RID: 3257
				public bool wilderness;

				// Token: 0x04000CBA RID: 3258
				public bool indoors;

				// Token: 0x04000CBB RID: 3259
				public bool city;

				// Token: 0x04000CBC RID: 3260
				public bool dynamicEnc;

				// Token: 0x04000CBD RID: 3261
				public bool drawAsCube;

				// Token: 0x04000CBE RID: 3262
				public bool canRestHere;

				// Token: 0x04000CBF RID: 3263
				public bool canRestInBedHere;

				// Token: 0x04000CC0 RID: 3264
				public bool canFightHere;

				// Token: 0x04000CC1 RID: 3265
				public bool published;

				// Token: 0x04000CC2 RID: 3266
				public bool deleteAllOnLeave;

				// Token: 0x04000CC3 RID: 3267
				public bool fogRegrows;

				// Token: 0x04000CC4 RID: 3268
				public bool dayNightCycleLight;

				// Token: 0x04000CC5 RID: 3269
				public bool dynamicWeather;

				// Token: 0x04000CC6 RID: 3270
				public bool whiteGround;

				// Token: 0x04000CC7 RID: 3271
				public int startX;

				// Token: 0x04000CC8 RID: 3272
				public int startY;

				// Token: 0x04000CC9 RID: 3273
				public int width;

				// Token: 0x04000CCA RID: 3274
				public int height;

				// Token: 0x04000CCB RID: 3275
				public int travelTime;

				// Token: 0x04000CCC RID: 3276
				public float visionRange;

				// Token: 0x04000CCD RID: 3277
				public float maxLightLevel;

				// Token: 0x04000CCE RID: 3278
				public string musicPath;

				// Token: 0x04000CCF RID: 3279
				public string combatMusicPath;

				// Token: 0x04000CD0 RID: 3280
				public string economy;

				// Token: 0x04000CD1 RID: 3281
				public string mapAboveId;

				// Token: 0x04000CD2 RID: 3282
				public string mapBelowId;

				// Token: 0x04000CD3 RID: 3283
				public string containerMapId;

				// Token: 0x04000CD4 RID: 3284
				public string northernEdgeMapId;

				// Token: 0x04000CD5 RID: 3285
				public string easternEdgeMapId;

				// Token: 0x04000CD6 RID: 3286
				public string southernEdgeMapId;

				// Token: 0x04000CD7 RID: 3287
				public string westernEdgeMapId;

				// Token: 0x04000CD8 RID: 3288
				public string faction;

				// Token: 0x04000CD9 RID: 3289
				public string enterTrigger;

				// Token: 0x04000CDA RID: 3290
				public string firstTimeEnterTrigger;

				// Token: 0x04000CDB RID: 3291
				public string leaveTrigger;

				// Token: 0x04000CDC RID: 3292
				public string firstTimeLeaveTrigger;
			}
		}

		// Token: 0x02000336 RID: 822
		[Serializable]
		public class DifficultyContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.Objects.DifficultyContainer.DifficultyData>
		{
			// Token: 0x020003FA RID: 1018
			[Serializable]
			public class DifficultyData : SKALDProjectData.GameDataObject
			{
				// Token: 0x04000CDD RID: 3293
				public bool xpForDowned;

				// Token: 0x04000CDE RID: 3294
				public bool healFullyAfterCombat;

				// Token: 0x04000CDF RID: 3295
				public bool ignoreEncumberance;

				// Token: 0x04000CE0 RID: 3296
				public bool cannotDie;

				// Token: 0x04000CE1 RID: 3297
				public bool ignoreFood;

				// Token: 0x04000CE2 RID: 3298
				public bool ignoreTrashMobs;

				// Token: 0x04000CE3 RID: 3299
				public int degree;

				// Token: 0x04000CE4 RID: 3300
				public int playerRerolls;

				// Token: 0x04000CE5 RID: 3301
				public int playerAttributeTestRerolls;

				// Token: 0x04000CE6 RID: 3302
				public int enemyRerolls;

				// Token: 0x04000CE7 RID: 3303
				public int playerDamageRerolls;

				// Token: 0x04000CE8 RID: 3304
				public int enemyDamageRerolls;

				// Token: 0x04000CE9 RID: 3305
				public int maxMissSmoothing;

				// Token: 0x04000CEA RID: 3306
				public float depletionRate;
			}
		}

		// Token: 0x02000337 RID: 823
		[Serializable]
		public class StoreDataContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.Objects.StoreDataContainer.StoreData>
		{
			// Token: 0x020003FB RID: 1019
			[Serializable]
			public class StoreData : SKALDProjectData.GameDataObject
			{
				// Token: 0x04000CEB RID: 3307
				public bool fence;

				// Token: 0x04000CEC RID: 3308
				public bool fullStock;

				// Token: 0x04000CED RID: 3309
				public bool serviceHeal;

				// Token: 0x04000CEE RID: 3310
				public bool serviceIdentify;

				// Token: 0x04000CEF RID: 3311
				public bool serviceRest;

				// Token: 0x04000CF0 RID: 3312
				public bool serviceRepair;

				// Token: 0x04000CF1 RID: 3313
				public bool serviceCarouse;

				// Token: 0x04000CF2 RID: 3314
				public int minRarity;

				// Token: 0x04000CF3 RID: 3315
				public int maxRarity;

				// Token: 0x04000CF4 RID: 3316
				public int minMagic;

				// Token: 0x04000CF5 RID: 3317
				public int maxMagic;

				// Token: 0x04000CF6 RID: 3318
				public int restockRate;

				// Token: 0x04000CF7 RID: 3319
				public int slotsPerType;

				// Token: 0x04000CF8 RID: 3320
				public int startingGold;

				// Token: 0x04000CF9 RID: 3321
				public float stackMultiplier;

				// Token: 0x04000CFA RID: 3322
				public float valueMultiplier;

				// Token: 0x04000CFB RID: 3323
				public float buyMultiplier;

				// Token: 0x04000CFC RID: 3324
				public List<string> itemTypes;

				// Token: 0x04000CFD RID: 3325
				public List<string> loadouts;

				// Token: 0x04000CFE RID: 3326
				public string restockTrigger;

				// Token: 0x04000CFF RID: 3327
				public string mountTrigger;

				// Token: 0x04000D00 RID: 3328
				public string rumorList;
			}
		}

		// Token: 0x02000338 RID: 824
		[Serializable]
		public class VariableContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.Objects.VariableContainer.Variable>
		{
			// Token: 0x020003FC RID: 1020
			[Serializable]
			public class Variable : SKALDProjectData.GameDataObject
			{
			}
		}

		// Token: 0x02000339 RID: 825
		[Serializable]
		public class ScriptContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.Objects.ScriptContainer.Script>
		{
			// Token: 0x020003FD RID: 1021
			[Serializable]
			public class Script : SKALDProjectData.GameDataObject
			{
			}
		}

		// Token: 0x0200033A RID: 826
		public abstract class BaseEvent : SKALDProjectData.GameDataObject
		{
			// Token: 0x04000AC0 RID: 2752
			public bool campEvent;

			// Token: 0x04000AC1 RID: 2753
			public bool seaEvent;

			// Token: 0x04000AC2 RID: 2754
			public bool overlandEvent;

			// Token: 0x04000AC3 RID: 2755
			public bool trashMob;

			// Token: 0x04000AC4 RID: 2756
			public bool active;

			// Token: 0x04000AC5 RID: 2757
			public bool essential;

			// Token: 0x04000AC6 RID: 2758
			public bool repeatable;

			// Token: 0x04000AC7 RID: 2759
			public int weight;

			// Token: 0x04000AC8 RID: 2760
			public int priority;

			// Token: 0x04000AC9 RID: 2761
			public int minLevel;

			// Token: 0x04000ACA RID: 2762
			public int maxLevel;

			// Token: 0x04000ACB RID: 2763
			public string condition;

			// Token: 0x04000ACC RID: 2764
			public string trigger;

			// Token: 0x04000ACD RID: 2765
			public List<string> requiredCharacters;

			// Token: 0x04000ACE RID: 2766
			public List<string> requiredMaps;

			// Token: 0x04000ACF RID: 2767
			public List<string> requiredTiles;
		}

		// Token: 0x0200033B RID: 827
		[Serializable]
		public class EventContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.Objects.EventContainer.Event>
		{
			// Token: 0x020003FE RID: 1022
			[Serializable]
			public class Event : SKALDProjectData.Objects.BaseEvent
			{
			}
		}

		// Token: 0x0200033C RID: 828
		[Serializable]
		public class EncounterContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.Objects.EncounterContainer.Encounter>
		{
			// Token: 0x020003FF RID: 1023
			[Serializable]
			public class Encounter : SKALDProjectData.Objects.BaseEvent
			{
				// Token: 0x04000D01 RID: 3329
				public bool stealth;

				// Token: 0x04000D02 RID: 3330
				public bool flee;

				// Token: 0x04000D03 RID: 3331
				public bool diplomacy;

				// Token: 0x04000D04 RID: 3332
				public bool bribes;

				// Token: 0x04000D05 RID: 3333
				public float minFactor;

				// Token: 0x04000D06 RID: 3334
				public float maxFactor;

				// Token: 0x04000D07 RID: 3335
				public int maxCreatureNumber;

				// Token: 0x04000D08 RID: 3336
				public int propChance;

				// Token: 0x04000D09 RID: 3337
				public List<string> maps;

				// Token: 0x04000D0A RID: 3338
				public List<string> enemiesList;

				// Token: 0x04000D0B RID: 3339
				public List<string> setEnemiesList;

				// Token: 0x04000D0C RID: 3340
				public List<string> props;

				// Token: 0x04000D0D RID: 3341
				public List<string> loadouts;
			}
		}
	}

	// Token: 0x02000210 RID: 528
	[Serializable]
	public class ItemDataContainers : SKALDProjectData.BaseContainerObject
	{
		// Token: 0x06001854 RID: 6228 RVA: 0x0006B544 File Offset: 0x00069744
		public ItemDataContainers()
		{
			this.id = "Items";
			this.meleeWeapons = new SKALDProjectData.ItemDataContainers.MeleeWeaponContainer();
			this.rangedWeapons = new SKALDProjectData.ItemDataContainers.RangedWeaponContainer();
			this.ammoContainer = new SKALDProjectData.ItemDataContainers.AmmoContainer();
			this.armor = new SKALDProjectData.ItemDataContainers.ArmorContainer();
			this.accessories = new SKALDProjectData.ItemDataContainers.AccessoryContainer();
			this.shields = new SKALDProjectData.ItemDataContainers.ShieldContainer();
			this.clothing = new SKALDProjectData.ItemDataContainers.ClothingContainer();
			this.miscItems = new SKALDProjectData.ItemDataContainers.ItemContainer();
			this.tomes = new SKALDProjectData.ItemDataContainers.TomeContainer();
			this.reagents = new SKALDProjectData.ItemDataContainers.ReagentContainer();
			this.foods = new SKALDProjectData.ItemDataContainers.FoodContainer();
			this.consumeables = new SKALDProjectData.ItemDataContainers.ConsumeableContainer();
			this.adventuringItems = new SKALDProjectData.ItemDataContainers.AdventuringContainer();
			this.trinkets = new SKALDProjectData.ItemDataContainers.TrinketContainer();
			this.jewelry = new SKALDProjectData.ItemDataContainers.JewelryContainer();
			this.gems = new SKALDProjectData.ItemDataContainers.GemContainer();
			this.books = new SKALDProjectData.ItemDataContainers.BookContainer();
			this.keys = new SKALDProjectData.ItemDataContainers.KeyContainer();
			this.idleItems = new SKALDProjectData.ItemDataContainers.IdleItemContainer();
			this.recipes = new SKALDProjectData.ItemDataContainers.RecipeContainer();
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x0006B640 File Offset: 0x00069840
		public override List<BaseDataObject> getBaseList()
		{
			List<BaseDataObject> baseList = base.getBaseList();
			baseList.Add(this.miscItems);
			baseList.Add(this.meleeWeapons);
			baseList.Add(this.rangedWeapons);
			baseList.Add(this.ammoContainer);
			baseList.Add(this.armor);
			baseList.Add(this.accessories);
			baseList.Add(this.shields);
			baseList.Add(this.clothing);
			baseList.Add(this.reagents);
			baseList.Add(this.tomes);
			baseList.Add(this.foods);
			baseList.Add(this.consumeables);
			baseList.Add(this.adventuringItems);
			baseList.Add(this.trinkets);
			baseList.Add(this.jewelry);
			baseList.Add(this.gems);
			baseList.Add(this.books);
			baseList.Add(this.keys);
			baseList.Add(this.idleItems);
			baseList.Add(this.recipes);
			return baseList;
		}

		// Token: 0x04000804 RID: 2052
		public SKALDProjectData.ItemDataContainers.MeleeWeaponContainer meleeWeapons;

		// Token: 0x04000805 RID: 2053
		public SKALDProjectData.ItemDataContainers.RangedWeaponContainer rangedWeapons;

		// Token: 0x04000806 RID: 2054
		public SKALDProjectData.ItemDataContainers.AmmoContainer ammoContainer;

		// Token: 0x04000807 RID: 2055
		public SKALDProjectData.ItemDataContainers.ArmorContainer armor;

		// Token: 0x04000808 RID: 2056
		public SKALDProjectData.ItemDataContainers.AccessoryContainer accessories;

		// Token: 0x04000809 RID: 2057
		public SKALDProjectData.ItemDataContainers.ShieldContainer shields;

		// Token: 0x0400080A RID: 2058
		public SKALDProjectData.ItemDataContainers.ClothingContainer clothing;

		// Token: 0x0400080B RID: 2059
		public SKALDProjectData.ItemDataContainers.TomeContainer tomes;

		// Token: 0x0400080C RID: 2060
		public SKALDProjectData.ItemDataContainers.ItemContainer miscItems;

		// Token: 0x0400080D RID: 2061
		public SKALDProjectData.ItemDataContainers.ReagentContainer reagents;

		// Token: 0x0400080E RID: 2062
		public SKALDProjectData.ItemDataContainers.FoodContainer foods;

		// Token: 0x0400080F RID: 2063
		public SKALDProjectData.ItemDataContainers.ConsumeableContainer consumeables;

		// Token: 0x04000810 RID: 2064
		public SKALDProjectData.ItemDataContainers.AdventuringContainer adventuringItems;

		// Token: 0x04000811 RID: 2065
		public SKALDProjectData.ItemDataContainers.TrinketContainer trinkets;

		// Token: 0x04000812 RID: 2066
		public SKALDProjectData.ItemDataContainers.JewelryContainer jewelry;

		// Token: 0x04000813 RID: 2067
		public SKALDProjectData.ItemDataContainers.GemContainer gems;

		// Token: 0x04000814 RID: 2068
		public SKALDProjectData.ItemDataContainers.BookContainer books;

		// Token: 0x04000815 RID: 2069
		public SKALDProjectData.ItemDataContainers.KeyContainer keys;

		// Token: 0x04000816 RID: 2070
		public SKALDProjectData.ItemDataContainers.IdleItemContainer idleItems;

		// Token: 0x04000817 RID: 2071
		public SKALDProjectData.ItemDataContainers.RecipeContainer recipes;

		// Token: 0x0200033D RID: 829
		[Serializable]
		public class MeleeWeaponContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.ItemDataContainers.MeleeWeaponContainer.MeleeWeaponData>
		{
			// Token: 0x02000400 RID: 1024
			[Serializable]
			public class MeleeWeaponData : SKALDProjectData.ItemDataContainers.WeaponData
			{
			}
		}

		// Token: 0x0200033E RID: 830
		[Serializable]
		public class RangedWeaponContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.ItemDataContainers.RangedWeaponContainer.RangedWeaponData>
		{
			// Token: 0x02000401 RID: 1025
			[Serializable]
			public class RangedWeaponData : SKALDProjectData.ItemDataContainers.WeaponData
			{
				// Token: 0x04000D0E RID: 3342
				public bool useInMelee;

				// Token: 0x04000D0F RID: 3343
				public bool destroyOnUse;

				// Token: 0x04000D10 RID: 3344
				public int range;

				// Token: 0x04000D11 RID: 3345
				public string ammo;
			}
		}

		// Token: 0x0200033F RID: 831
		[Serializable]
		public class AmmoContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.ItemDataContainers.AmmoContainer.AmmoData>
		{
			// Token: 0x02000402 RID: 1026
			[Serializable]
			public class AmmoData : SKALDProjectData.ItemDataContainers.DamagingItem
			{
				// Token: 0x04000D12 RID: 3346
				public string ammoType;

				// Token: 0x04000D13 RID: 3347
				public bool recoverable;
			}
		}

		// Token: 0x02000340 RID: 832
		[Serializable]
		public class TomeContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.ItemDataContainers.TomeContainer.TomeData>
		{
			// Token: 0x02000403 RID: 1027
			[Serializable]
			public class TomeData : SKALDProjectData.ItemDataContainers.ItemData
			{
				// Token: 0x04000D14 RID: 3348
				public string spellLearned;
			}
		}

		// Token: 0x02000341 RID: 833
		[Serializable]
		public class IdleItemContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.ItemDataContainers.IdleItemContainer.IdleItemData>
		{
			// Token: 0x02000404 RID: 1028
			[Serializable]
			public class IdleItemData : SKALDProjectData.ItemDataContainers.ItemData
			{
				// Token: 0x04000D15 RID: 3349
				public string idleItemAnimation;
			}
		}

		// Token: 0x02000342 RID: 834
		[Serializable]
		public class ArmorContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.ItemDataContainers.ArmorContainer.Armor>
		{
			// Token: 0x02000405 RID: 1029
			[Serializable]
			public class Armor : SKALDProjectData.ItemDataContainers.ClothingWearableData
			{
			}
		}

		// Token: 0x02000343 RID: 835
		[Serializable]
		public class AccessoryContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.ItemDataContainers.AccessoryContainer.Accessory>
		{
			// Token: 0x02000406 RID: 1030
			[Serializable]
			public class Accessory : SKALDProjectData.ItemDataContainers.ClothingWearableData
			{
				// Token: 0x04000D16 RID: 3350
				public bool hideBeard;

				// Token: 0x04000D17 RID: 3351
				public bool hideHair;
			}
		}

		// Token: 0x02000344 RID: 836
		[Serializable]
		public class ShieldContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.ItemDataContainers.ShieldContainer.Shield>
		{
			// Token: 0x02000407 RID: 1031
			[Serializable]
			public class Shield : SKALDProjectData.ItemDataContainers.ClothingWearableData
			{
				// Token: 0x04000D18 RID: 3352
				public int backstrapXOffset;

				// Token: 0x04000D19 RID: 3353
				public int backstrapYOffset;
			}
		}

		// Token: 0x02000345 RID: 837
		[Serializable]
		public class ClothingContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.ItemDataContainers.ClothingContainer.Clothing>
		{
			// Token: 0x02000408 RID: 1032
			[Serializable]
			public class Clothing : SKALDProjectData.ItemDataContainers.ClothingWearableData
			{
				// Token: 0x04000D1A RID: 3354
				public bool hidesArmor;
			}
		}

		// Token: 0x02000346 RID: 838
		[Serializable]
		public class GemContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.ItemDataContainers.GemContainer.Gem>
		{
			// Token: 0x02000409 RID: 1033
			[Serializable]
			public class Gem : SKALDProjectData.ItemDataContainers.ItemData
			{
			}
		}

		// Token: 0x02000347 RID: 839
		[Serializable]
		public class ReagentContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.ItemDataContainers.ReagentContainer.Reagent>
		{
			// Token: 0x0200040A RID: 1034
			[Serializable]
			public class Reagent : SKALDProjectData.ItemDataContainers.ItemData
			{
				// Token: 0x04000D1B RID: 3355
				public bool medium;

				// Token: 0x04000D1C RID: 3356
				public bool catalyst;

				// Token: 0x04000D1D RID: 3357
				public int power;

				// Token: 0x04000D1E RID: 3358
				public string primaryEffect;

				// Token: 0x04000D1F RID: 3359
				public string secondaryEffect;

				// Token: 0x04000D20 RID: 3360
				public string tertiaryEffect;

				// Token: 0x04000D21 RID: 3361
				public string eatTrigger;
			}
		}

		// Token: 0x02000348 RID: 840
		[Serializable]
		public class FoodContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.ItemDataContainers.FoodContainer.Food>
		{
			// Token: 0x0200040B RID: 1035
			[Serializable]
			public class Food : SKALDProjectData.ItemDataContainers.ItemData
			{
				// Token: 0x04000D22 RID: 3362
				public int foodValue;
			}
		}

		// Token: 0x02000349 RID: 841
		[Serializable]
		public class ConsumeableContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.ItemDataContainers.ConsumeableContainer.Consumeable>
		{
			// Token: 0x0200040C RID: 1036
			[Serializable]
			public class Consumeable : SKALDProjectData.ItemDataContainers.ItemData
			{
			}
		}

		// Token: 0x0200034A RID: 842
		[Serializable]
		public class AdventuringContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.ItemDataContainers.AdventuringContainer.AdventuringItem>
		{
			// Token: 0x0200040D RID: 1037
			[Serializable]
			public class AdventuringItem : SKALDProjectData.ItemDataContainers.ItemData
			{
				// Token: 0x04000D23 RID: 3363
				public int light;
			}
		}

		// Token: 0x0200034B RID: 843
		[Serializable]
		public class TrinketContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.ItemDataContainers.TrinketContainer.Trinket>
		{
			// Token: 0x0200040E RID: 1038
			[Serializable]
			public class Trinket : SKALDProjectData.ItemDataContainers.ItemData
			{
			}
		}

		// Token: 0x0200034C RID: 844
		[Serializable]
		public class RecipeContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.ItemDataContainers.RecipeContainer.Recipe>
		{
			// Token: 0x0200040F RID: 1039
			[Serializable]
			public class Recipe : SKALDProjectData.ItemDataContainers.BookContainer.Book
			{
				// Token: 0x04000D24 RID: 3364
				public string recipe;
			}
		}

		// Token: 0x0200034D RID: 845
		[Serializable]
		public class BookContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.ItemDataContainers.BookContainer.Book>
		{
			// Token: 0x02000410 RID: 1040
			[Serializable]
			public class Book : SKALDProjectData.ItemDataContainers.ItemData
			{
				// Token: 0x04000D25 RID: 3365
				public string content;
			}
		}

		// Token: 0x0200034E RID: 846
		[Serializable]
		public class KeyContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.ItemDataContainers.KeyContainer.Key>
		{
			// Token: 0x02000411 RID: 1041
			[Serializable]
			public class Key : SKALDProjectData.ItemDataContainers.ItemData
			{
			}
		}

		// Token: 0x0200034F RID: 847
		[Serializable]
		public class JewelryContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.ItemDataContainers.JewelryContainer.Jewelry>
		{
			// Token: 0x02000412 RID: 1042
			[Serializable]
			public class Jewelry : SKALDProjectData.ItemDataContainers.EquippableData
			{
				// Token: 0x04000D26 RID: 3366
				public string slotJewelry;
			}
		}

		// Token: 0x02000350 RID: 848
		[Serializable]
		public class ItemContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.ItemDataContainers.ItemData>
		{
		}

		// Token: 0x02000351 RID: 849
		[Serializable]
		public abstract class WeaponData : SKALDProjectData.ItemDataContainers.DamagingItem
		{
			// Token: 0x04000AD0 RID: 2768
			public string attackAnimation;

			// Token: 0x04000AD1 RID: 2769
			public string aimAnimation;

			// Token: 0x04000AD2 RID: 2770
			public string blockAnimation;

			// Token: 0x04000AD3 RID: 2771
			public string idleAnimation;

			// Token: 0x04000AD4 RID: 2772
			public string weightCategory;

			// Token: 0x04000AD5 RID: 2773
			public string weaponType;

			// Token: 0x04000AD6 RID: 2774
			public bool twoHanded;
		}

		// Token: 0x02000352 RID: 850
		public abstract class DamagingItem : SKALDProjectData.ItemDataContainers.ItemData
		{
			// Token: 0x04000AD7 RID: 2775
			public int maxDamage;

			// Token: 0x04000AD8 RID: 2776
			public int minDamage;

			// Token: 0x04000AD9 RID: 2777
			public int hitBonus;

			// Token: 0x04000ADA RID: 2778
			public float crit;

			// Token: 0x04000ADB RID: 2779
			public string hitTrigger;

			// Token: 0x04000ADC RID: 2780
			public string critTrigger;

			// Token: 0x04000ADD RID: 2781
			public List<string> damageType;

			// Token: 0x04000ADE RID: 2782
			public List<string> hitEffect;

			// Token: 0x04000ADF RID: 2783
			public List<string> critEffect;
		}

		// Token: 0x02000353 RID: 851
		[Serializable]
		public abstract class ClothingWearableData : SKALDProjectData.ItemDataContainers.EquippableData
		{
			// Token: 0x04000AE0 RID: 2784
			public int soak;

			// Token: 0x04000AE1 RID: 2785
			public int encumberance;

			// Token: 0x04000AE2 RID: 2786
			public int reactionBonus;

			// Token: 0x04000AE3 RID: 2787
			public string slot;

			// Token: 0x04000AE4 RID: 2788
			public string torsoModel;

			// Token: 0x04000AE5 RID: 2789
			public string armsModel;

			// Token: 0x04000AE6 RID: 2790
			public string legsModel;

			// Token: 0x04000AE7 RID: 2791
			public string headModel;

			// Token: 0x04000AE8 RID: 2792
			public string cloakModel;
		}

		// Token: 0x02000354 RID: 852
		[Serializable]
		public abstract class EquippableData : SKALDProjectData.ItemDataContainers.ItemData
		{
			// Token: 0x04000AE9 RID: 2793
			public bool equipable;

			// Token: 0x04000AEA RID: 2794
			public string equipTrigger;

			// Token: 0x04000AEB RID: 2795
			public string firstEquipTrigger;

			// Token: 0x04000AEC RID: 2796
			public string faction;

			// Token: 0x04000AED RID: 2797
			public string weightCategory;
		}

		// Token: 0x02000355 RID: 853
		[Serializable]
		public class ItemData : SKALDProjectData.GameDataObject
		{
			// Token: 0x04000AEE RID: 2798
			public bool stackable;

			// Token: 0x04000AEF RID: 2799
			public bool unique;

			// Token: 0x04000AF0 RID: 2800
			public bool sellable;

			// Token: 0x04000AF1 RID: 2801
			public bool questItem;

			// Token: 0x04000AF2 RID: 2802
			public bool stolen;

			// Token: 0x04000AF3 RID: 2803
			public bool lootable;

			// Token: 0x04000AF4 RID: 2804
			public int value;

			// Token: 0x04000AF5 RID: 2805
			public int storeStack;

			// Token: 0x04000AF6 RID: 2806
			public int rarity;

			// Token: 0x04000AF7 RID: 2807
			public float weight;

			// Token: 0x04000AF8 RID: 2808
			public string use;

			// Token: 0x04000AF9 RID: 2809
			public string parent;

			// Token: 0x04000AFA RID: 2810
			public string useVerb;

			// Token: 0x04000AFB RID: 2811
			public string primaryColor;

			// Token: 0x04000AFC RID: 2812
			public string secondaryColor;

			// Token: 0x04000AFD RID: 2813
			public int powerLevel;

			// Token: 0x04000AFE RID: 2814
			public int magicLevel;

			// Token: 0x04000AFF RID: 2815
			public List<string> conferredAbilities;

			// Token: 0x04000B00 RID: 2816
			public List<string> conferredConditions;

			// Token: 0x04000B01 RID: 2817
			public List<string> conferredSpells;

			// Token: 0x04000B02 RID: 2818
			public List<string> useEffect;

			// Token: 0x04000B03 RID: 2819
			public string enchantment;
		}
	}

	// Token: 0x02000211 RID: 529
	[Serializable]
	public class RecipeContainers : SKALDProjectData.BaseContainerObject
	{
		// Token: 0x06001856 RID: 6230 RVA: 0x0006B743 File Offset: 0x00069943
		public RecipeContainers()
		{
			this.foodRecipes = new SKALDProjectData.RecipeContainers.FoodRecipeContainer();
			this.alchemyRecipes = new SKALDProjectData.RecipeContainers.AlchemyRecipeContainer();
			this.smithyRecipes = new SKALDProjectData.RecipeContainers.SmithyRecipeContainer();
			this.workbenchRecepies = new SKALDProjectData.RecipeContainers.WorkbenchRecipeContainer();
			this.miscRecipes = new SKALDProjectData.RecipeContainers.MiscRecipeContainer();
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x0006B784 File Offset: 0x00069984
		public override List<BaseDataObject> getBaseList()
		{
			List<BaseDataObject> baseList = base.getBaseList();
			baseList.Add(this.foodRecipes);
			baseList.Add(this.alchemyRecipes);
			baseList.Add(this.smithyRecipes);
			baseList.Add(this.workbenchRecepies);
			baseList.Add(this.miscRecipes);
			return baseList;
		}

		// Token: 0x04000818 RID: 2072
		public SKALDProjectData.RecipeContainers.FoodRecipeContainer foodRecipes;

		// Token: 0x04000819 RID: 2073
		public SKALDProjectData.RecipeContainers.AlchemyRecipeContainer alchemyRecipes;

		// Token: 0x0400081A RID: 2074
		public SKALDProjectData.RecipeContainers.SmithyRecipeContainer smithyRecipes;

		// Token: 0x0400081B RID: 2075
		public SKALDProjectData.RecipeContainers.WorkbenchRecipeContainer workbenchRecepies;

		// Token: 0x0400081C RID: 2076
		public SKALDProjectData.RecipeContainers.MiscRecipeContainer miscRecipes;

		// Token: 0x02000356 RID: 854
		[Serializable]
		public class MiscRecipeContainer : SKALDProjectData.RecipeContainers.RecipeDataContainer
		{
		}

		// Token: 0x02000357 RID: 855
		[Serializable]
		public class WorkbenchRecipeContainer : SKALDProjectData.RecipeContainers.RecipeDataContainer
		{
		}

		// Token: 0x02000358 RID: 856
		[Serializable]
		public class SmithyRecipeContainer : SKALDProjectData.RecipeContainers.RecipeDataContainer
		{
		}

		// Token: 0x02000359 RID: 857
		[Serializable]
		public class AlchemyRecipeContainer : SKALDProjectData.RecipeContainers.RecipeDataContainer
		{
		}

		// Token: 0x0200035A RID: 858
		[Serializable]
		public class FoodRecipeContainer : SKALDProjectData.RecipeContainers.RecipeDataContainer
		{
		}

		// Token: 0x0200035B RID: 859
		[Serializable]
		public abstract class RecipeDataContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.RecipeContainers.Recipe>
		{
		}

		// Token: 0x0200035C RID: 860
		[Serializable]
		public class Recipe : SKALDProjectData.GameDataObject
		{
			// Token: 0x04000B04 RID: 2820
			public List<string> craftingType;

			// Token: 0x04000B05 RID: 2821
			public string skill;

			// Token: 0x04000B06 RID: 2822
			public List<string> componentList;

			// Token: 0x04000B07 RID: 2823
			public List<string> multiComponentList;

			// Token: 0x04000B08 RID: 2824
			public List<string> yields;

			// Token: 0x04000B09 RID: 2825
			public List<string> failYield;

			// Token: 0x04000B0A RID: 2826
			public string successTrigger;

			// Token: 0x04000B0B RID: 2827
			public string failTrigger;

			// Token: 0x04000B0C RID: 2828
			public int multiNumber;

			// Token: 0x04000B0D RID: 2829
			public int levelRequirement;

			// Token: 0x04000B0E RID: 2830
			public int numberYielded;

			// Token: 0x04000B0F RID: 2831
			public int discoveryXP;

			// Token: 0x04000B10 RID: 2832
			public int difficulty;

			// Token: 0x04000B11 RID: 2833
			public bool requireRecipe;

			// Token: 0x04000B12 RID: 2834
			public bool alwaysAvailable;
		}
	}

	// Token: 0x02000212 RID: 530
	[Serializable]
	public class VehicleContainers : SKALDProjectData.BaseContainerObject
	{
		// Token: 0x06001858 RID: 6232 RVA: 0x0006B7D3 File Offset: 0x000699D3
		public VehicleContainers()
		{
			this.vehicleData = new SKALDProjectData.VehicleContainers.ShipContainer();
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x0006B7E6 File Offset: 0x000699E6
		public override List<BaseDataObject> getBaseList()
		{
			List<BaseDataObject> baseList = base.getBaseList();
			baseList.Add(this.vehicleData);
			return baseList;
		}

		// Token: 0x0400081D RID: 2077
		public SKALDProjectData.VehicleContainers.ShipContainer vehicleData;

		// Token: 0x0200035D RID: 861
		[Serializable]
		public class ShipContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.VehicleContainers.Vehicle>
		{
		}

		// Token: 0x0200035E RID: 862
		[Serializable]
		public class Vehicle : SKALDProjectData.GameDataObject
		{
			// Token: 0x04000B13 RID: 2835
			public string nestedMap;
		}
	}

	// Token: 0x02000213 RID: 531
	[Serializable]
	public class AbilityContainers : SKALDProjectData.BaseContainerObject
	{
		// Token: 0x0600185A RID: 6234 RVA: 0x0006B7FA File Offset: 0x000699FA
		public AbilityContainers()
		{
			this.id = "Ability Data";
			this.additionAbilityContainer = new SKALDProjectData.AbilityContainers.PassiveAbilityContainer();
			this.combatManeuverContainer = new SKALDProjectData.AbilityContainers.CombatManeuverContainer();
			this.spellContainer = new SKALDProjectData.AbilityContainers.SpellContainer();
			this.triggeredAbilityContainer = new SKALDProjectData.AbilityContainers.TriggeredAbilityContainer();
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x0006B839 File Offset: 0x00069A39
		public override List<BaseDataObject> getBaseList()
		{
			List<BaseDataObject> baseList = base.getBaseList();
			baseList.Add(this.additionAbilityContainer);
			baseList.Add(this.combatManeuverContainer);
			baseList.Add(this.spellContainer);
			baseList.Add(this.triggeredAbilityContainer);
			return baseList;
		}

		// Token: 0x0400081E RID: 2078
		public SKALDProjectData.AbilityContainers.PassiveAbilityContainer additionAbilityContainer;

		// Token: 0x0400081F RID: 2079
		public SKALDProjectData.AbilityContainers.CombatManeuverContainer combatManeuverContainer;

		// Token: 0x04000820 RID: 2080
		public SKALDProjectData.AbilityContainers.SpellContainer spellContainer;

		// Token: 0x04000821 RID: 2081
		public SKALDProjectData.AbilityContainers.TriggeredAbilityContainer triggeredAbilityContainer;

		// Token: 0x0200035F RID: 863
		[Serializable]
		public class SpellContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.AbilityContainers.SpellContainer.SpellAbility>
		{
			// Token: 0x02000413 RID: 1043
			[Serializable]
			public class SpellAbility : SKALDProjectData.AbilityContainers.UseableAbility
			{
				// Token: 0x04000D27 RID: 3367
				public List<string> school;

				// Token: 0x04000D28 RID: 3368
				public int tier;

				// Token: 0x04000D29 RID: 3369
				public int cascadeDCMod;

				// Token: 0x04000D2A RID: 3370
				public bool useInCombat;

				// Token: 0x04000D2B RID: 3371
				public bool useOutsideCombat;
			}
		}

		// Token: 0x02000360 RID: 864
		[Serializable]
		public class TriggeredAbilityContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.AbilityContainers.TriggeredAbilityContainer.TriggeredAbility>
		{
			// Token: 0x02000414 RID: 1044
			[Serializable]
			public class TriggeredAbility : SKALDProjectData.AbilityContainers.ActiveAbility
			{
				// Token: 0x04000D2C RID: 3372
				public bool triggerOnMeleeHit;

				// Token: 0x04000D2D RID: 3373
				public bool triggerOnRangedHit;

				// Token: 0x04000D2E RID: 3374
				public bool triggerOnUnarmedHit;

				// Token: 0x04000D2F RID: 3375
				public bool triggerOnManeuverHit;

				// Token: 0x04000D30 RID: 3376
				public bool triggerOnBackstabHit;

				// Token: 0x04000D31 RID: 3377
				public bool triggerOnCriticalHit;

				// Token: 0x04000D32 RID: 3378
				public bool triggerOnAnyDamage;

				// Token: 0x04000D33 RID: 3379
				public bool triggerOnWoundDamage;

				// Token: 0x04000D34 RID: 3380
				public bool triggerOnDead;

				// Token: 0x04000D35 RID: 3381
				public bool triggerOnDefending;

				// Token: 0x04000D36 RID: 3382
				public bool triggerOnCombatStart;

				// Token: 0x04000D37 RID: 3383
				public bool triggerOnCombatEnd;

				// Token: 0x04000D38 RID: 3384
				public bool triggerOnKilledTarget;

				// Token: 0x04000D39 RID: 3385
				public bool triggerOnKilledMarkedTarget;

				// Token: 0x04000D3A RID: 3386
				public bool triggerOnAllyDead;

				// Token: 0x04000D3B RID: 3387
				public bool triggerOnMiss;

				// Token: 0x04000D3C RID: 3388
				public bool triggerOnSpellcasting;

				// Token: 0x04000D3D RID: 3389
				public bool triggerOnDodge;

				// Token: 0x04000D3E RID: 3390
				public bool triggerOnDodgeMelee;

				// Token: 0x04000D3F RID: 3391
				public bool triggerOnStartOfTurn;

				// Token: 0x04000D40 RID: 3392
				public bool triggerOnChargeHit;

				// Token: 0x04000D41 RID: 3393
				public int triggerChance;
			}
		}

		// Token: 0x02000361 RID: 865
		[Serializable]
		public class CombatManeuverContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.AbilityContainers.CombatManeuverContainer.CombatManueverAbility>
		{
			// Token: 0x06001CDA RID: 7386 RVA: 0x0007B48E File Offset: 0x0007968E
			public CombatManeuverContainer()
			{
				this.id = "Combat Manuevers";
			}

			// Token: 0x02000415 RID: 1045
			[Serializable]
			public class CombatManueverAbility : SKALDProjectData.AbilityContainers.UseableAbility
			{
				// Token: 0x04000D42 RID: 3394
				public bool attackBased;

				// Token: 0x04000D43 RID: 3395
				public bool addManeuverBonus;

				// Token: 0x04000D44 RID: 3396
				public bool armorPiercing;

				// Token: 0x04000D45 RID: 3397
				public bool addCombatDamageOnHit;

				// Token: 0x04000D46 RID: 3398
				public bool allowCrit;

				// Token: 0x04000D47 RID: 3399
				public bool allowBackstab;

				// Token: 0x04000D48 RID: 3400
				public bool autoCrit;

				// Token: 0x04000D49 RID: 3401
				public int coolDownCost;

				// Token: 0x04000D4A RID: 3402
				public int toHitBonus;

				// Token: 0x04000D4B RID: 3403
				public int damageBonus;
			}
		}

		// Token: 0x02000362 RID: 866
		[Serializable]
		public class UseableAbility : SKALDProjectData.AbilityContainers.ActiveAbility
		{
			// Token: 0x04000B14 RID: 2836
			public List<string> requiredItems;

			// Token: 0x04000B15 RID: 2837
			public string resourceType;

			// Token: 0x04000B16 RID: 2838
			public string timeCost;

			// Token: 0x04000B17 RID: 2839
			public int useCost;

			// Token: 0x04000B18 RID: 2840
			public int itemUseCost;

			// Token: 0x04000B19 RID: 2841
			public int AIUseChance;

			// Token: 0x04000B1A RID: 2842
			public bool AIUseable;
		}

		// Token: 0x02000363 RID: 867
		[Serializable]
		public class ActiveAbility : SKALDProjectData.AbilityContainers.AbilityData
		{
			// Token: 0x04000B1B RID: 2843
			public int radius;

			// Token: 0x04000B1C RID: 2844
			public string effectPattern;

			// Token: 0x04000B1D RID: 2845
			public string soundEffect;

			// Token: 0x04000B1E RID: 2846
			public string useAnimation;

			// Token: 0x04000B1F RID: 2847
			public string userAttribute;

			// Token: 0x04000B20 RID: 2848
			public string targetAttribute;

			// Token: 0x04000B21 RID: 2849
			public List<string> tileParticleEffect;

			// Token: 0x04000B22 RID: 2850
			public List<string> creatureSummoned;

			// Token: 0x04000B23 RID: 2851
			public List<string> userParticleEffect;

			// Token: 0x04000B24 RID: 2852
			public string useTrigger;

			// Token: 0x04000B25 RID: 2853
			public string successTrigger;

			// Token: 0x04000B26 RID: 2854
			public string failureTrigger;

			// Token: 0x04000B27 RID: 2855
			public List<string> useEffect;

			// Token: 0x04000B28 RID: 2856
			public List<string> successEffect;

			// Token: 0x04000B29 RID: 2857
			public List<string> failureEffect;

			// Token: 0x04000B2A RID: 2858
			public bool targetAllies;

			// Token: 0x04000B2B RID: 2859
			public bool targetEnemies;

			// Token: 0x04000B2C RID: 2860
			public bool userMustSee;

			// Token: 0x04000B2D RID: 2861
			public bool userMustSpeak;

			// Token: 0x04000B2E RID: 2862
			public bool userMustMove;

			// Token: 0x04000B2F RID: 2863
			public bool targetMustSee;

			// Token: 0x04000B30 RID: 2864
			public bool targetMustHear;

			// Token: 0x04000B31 RID: 2865
			public bool armedMelee;

			// Token: 0x04000B32 RID: 2866
			public bool ranged;

			// Token: 0x04000B33 RID: 2867
			public bool unarmed;

			// Token: 0x04000B34 RID: 2868
			public bool requireShield;

			// Token: 0x04000B35 RID: 2869
			public bool requireBow;

			// Token: 0x04000B36 RID: 2870
			public bool requireSword;

			// Token: 0x04000B37 RID: 2871
			public bool requireAxe;

			// Token: 0x04000B38 RID: 2872
			public bool requireClub;

			// Token: 0x04000B39 RID: 2873
			public bool requirePolearm;

			// Token: 0x04000B3A RID: 2874
			public bool requireLightWeapon;

			// Token: 0x04000B3B RID: 2875
			public bool requireHeavyWeapon;

			// Token: 0x04000B3C RID: 2876
			public bool requireBlunt;

			// Token: 0x04000B3D RID: 2877
			public bool requirePiercing;

			// Token: 0x04000B3E RID: 2878
			public bool requireSlashing;

			// Token: 0x04000B3F RID: 2879
			public bool requireLight;

			// Token: 0x04000B40 RID: 2880
			public bool requireMedium;

			// Token: 0x04000B41 RID: 2881
			public bool requireHeavy;

			// Token: 0x04000B42 RID: 2882
			public bool requireOutOfMelee;
		}

		// Token: 0x02000364 RID: 868
		[Serializable]
		public class PassiveAbilityContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.AbilityContainers.PassiveAbilityContainer.PassiveAbility>
		{
			// Token: 0x02000416 RID: 1046
			[Serializable]
			public class PassiveAbility : SKALDProjectData.AbilityContainers.AbilityData
			{
				// Token: 0x04000D4C RID: 3404
				public int bonusMagnitude;

				// Token: 0x04000D4D RID: 3405
				public List<string> bonusAttributes;

				// Token: 0x04000D4E RID: 3406
				public int penaltyMagnitude;

				// Token: 0x04000D4F RID: 3407
				public List<string> penaltyAttributes;

				// Token: 0x04000D50 RID: 3408
				public List<string> staticCondition;
			}
		}

		// Token: 0x02000365 RID: 869
		[Serializable]
		public class AbilityData : SKALDProjectData.GameDataObject
		{
			// Token: 0x04000B43 RID: 2883
			public string getTrigger;

			// Token: 0x04000B44 RID: 2884
			public bool isPositiveForTarget;
		}
	}

	// Token: 0x02000214 RID: 532
	[Serializable]
	public class FeatContainers : SKALDProjectData.BaseContainerObject
	{
		// Token: 0x0600185C RID: 6236 RVA: 0x0006B874 File Offset: 0x00069A74
		public FeatContainers()
		{
			this.id = "Feat Data";
			this.genericFeatsContainer = new SKALDProjectData.FeatContainers.GenericFeatsContainer();
			this.warriorFeatsContainer = new SKALDProjectData.FeatContainers.WarriorFeatsContainer();
			this.rogueFeatsContainer = new SKALDProjectData.FeatContainers.RogueFeatsContainer();
			this.magosFeatsContainer = new SKALDProjectData.FeatContainers.MagosFeatsContainer();
			this.wandererFeatsContainer = new SKALDProjectData.FeatContainers.WandererFeatsContainer();
			this.clericFeatsContainer = new SKALDProjectData.FeatContainers.ClericFeatsContainer();
			this.NPCFeats = new SKALDProjectData.FeatContainers.NPCFeatsContainer();
			this.backgroundFeatsContainer = new SKALDProjectData.FeatContainers.BackgroundFeatsContainer();
			this.raceFeatsContainer = new SKALDProjectData.FeatContainers.RaceFeatsContainer();
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x0006B8F8 File Offset: 0x00069AF8
		public override List<BaseDataObject> getBaseList()
		{
			List<BaseDataObject> baseList = base.getBaseList();
			baseList.Add(this.genericFeatsContainer);
			baseList.Add(this.warriorFeatsContainer);
			baseList.Add(this.rogueFeatsContainer);
			baseList.Add(this.clericFeatsContainer);
			baseList.Add(this.magosFeatsContainer);
			baseList.Add(this.wandererFeatsContainer);
			baseList.Add(this.NPCFeats);
			baseList.Add(this.backgroundFeatsContainer);
			baseList.Add(this.raceFeatsContainer);
			return baseList;
		}

		// Token: 0x04000822 RID: 2082
		public SKALDProjectData.FeatContainers.GenericFeatsContainer genericFeatsContainer;

		// Token: 0x04000823 RID: 2083
		public SKALDProjectData.FeatContainers.WarriorFeatsContainer warriorFeatsContainer;

		// Token: 0x04000824 RID: 2084
		public SKALDProjectData.FeatContainers.RogueFeatsContainer rogueFeatsContainer;

		// Token: 0x04000825 RID: 2085
		public SKALDProjectData.FeatContainers.ClericFeatsContainer clericFeatsContainer;

		// Token: 0x04000826 RID: 2086
		public SKALDProjectData.FeatContainers.MagosFeatsContainer magosFeatsContainer;

		// Token: 0x04000827 RID: 2087
		public SKALDProjectData.FeatContainers.WandererFeatsContainer wandererFeatsContainer;

		// Token: 0x04000828 RID: 2088
		public SKALDProjectData.FeatContainers.NPCFeatsContainer NPCFeats;

		// Token: 0x04000829 RID: 2089
		public SKALDProjectData.FeatContainers.BackgroundFeatsContainer backgroundFeatsContainer;

		// Token: 0x0400082A RID: 2090
		public SKALDProjectData.FeatContainers.RaceFeatsContainer raceFeatsContainer;

		// Token: 0x02000366 RID: 870
		[Serializable]
		public class GenericFeatsContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.FeatContainers.FeatData>
		{
			// Token: 0x06001CDF RID: 7391 RVA: 0x0007B4C4 File Offset: 0x000796C4
			public override List<string> getFlatIdList(List<string> resultList)
			{
				foreach (BaseDataObject baseDataObject in this.baseDataObjectList)
				{
					resultList.Add(baseDataObject.id);
				}
				return resultList;
			}
		}

		// Token: 0x02000367 RID: 871
		[Serializable]
		public class BackgroundFeatsContainer : SKALDProjectData.FeatContainers.GenericFeatsContainer
		{
		}

		// Token: 0x02000368 RID: 872
		[Serializable]
		public class RaceFeatsContainer : SKALDProjectData.FeatContainers.GenericFeatsContainer
		{
		}

		// Token: 0x02000369 RID: 873
		[Serializable]
		public class WarriorFeatsContainer : SKALDProjectData.FeatContainers.GenericFeatsContainer
		{
		}

		// Token: 0x0200036A RID: 874
		[Serializable]
		public class RogueFeatsContainer : SKALDProjectData.FeatContainers.GenericFeatsContainer
		{
		}

		// Token: 0x0200036B RID: 875
		[Serializable]
		public class ClericFeatsContainer : SKALDProjectData.FeatContainers.GenericFeatsContainer
		{
		}

		// Token: 0x0200036C RID: 876
		[Serializable]
		public class MagosFeatsContainer : SKALDProjectData.FeatContainers.GenericFeatsContainer
		{
		}

		// Token: 0x0200036D RID: 877
		[Serializable]
		public class WandererFeatsContainer : SKALDProjectData.FeatContainers.GenericFeatsContainer
		{
		}

		// Token: 0x0200036E RID: 878
		[Serializable]
		public class NPCFeatsContainer : SKALDProjectData.FeatContainers.GenericFeatsContainer
		{
		}

		// Token: 0x0200036F RID: 879
		[Serializable]
		public class FeatData : SKALDProjectData.SpecificContainerObject<SKALDProjectData.FeatContainers.FeatData.FeatTierData>
		{
			// Token: 0x04000B45 RID: 2885
			public bool recommended;

			// Token: 0x04000B46 RID: 2886
			public int startingRank;

			// Token: 0x04000B47 RID: 2887
			public int prereqLevel;

			// Token: 0x04000B48 RID: 2888
			public int tier;

			// Token: 0x04000B49 RID: 2889
			public string prereqFeat;

			// Token: 0x04000B4A RID: 2890
			public string type;

			// Token: 0x04000B4B RID: 2891
			public string title;

			// Token: 0x04000B4C RID: 2892
			public string description;

			// Token: 0x04000B4D RID: 2893
			public string imagePath;

			// Token: 0x02000417 RID: 1047
			[Serializable]
			public class FeatTierData : SKALDProjectData.GameDataObject
			{
				// Token: 0x04000D51 RID: 3409
				public int rank;

				// Token: 0x04000D52 RID: 3410
				public List<string> abilities;

				// Token: 0x04000D53 RID: 3411
				public List<string> spells;

				// Token: 0x04000D54 RID: 3412
				public string spellSchool;

				// Token: 0x04000D55 RID: 3413
				public int spellsAdded;
			}
		}
	}

	// Token: 0x02000215 RID: 533
	[Serializable]
	public class ConditionContainers : SKALDProjectData.BaseContainerObject
	{
		// Token: 0x0600185E RID: 6238 RVA: 0x0006B977 File Offset: 0x00069B77
		public ConditionContainers()
		{
			this.naturalConditions = new SKALDProjectData.ConditionContainers.NaturalConditionsContainer();
			this.tacticalConditions = new SKALDProjectData.ConditionContainers.TacticalConditionsContainer();
			this.magicalConditions = new SKALDProjectData.ConditionContainers.MagicalConditionsContainer();
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x0006B9A0 File Offset: 0x00069BA0
		public override List<BaseDataObject> getBaseList()
		{
			List<BaseDataObject> baseList = base.getBaseList();
			baseList.Add(this.naturalConditions);
			baseList.Add(this.tacticalConditions);
			baseList.Add(this.magicalConditions);
			return baseList;
		}

		// Token: 0x0400082B RID: 2091
		public SKALDProjectData.ConditionContainers.NaturalConditionsContainer naturalConditions;

		// Token: 0x0400082C RID: 2092
		public SKALDProjectData.ConditionContainers.TacticalConditionsContainer tacticalConditions;

		// Token: 0x0400082D RID: 2093
		public SKALDProjectData.ConditionContainers.MagicalConditionsContainer magicalConditions;

		// Token: 0x02000370 RID: 880
		[Serializable]
		public class NaturalConditionsContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.ConditionContainers.ConditionData>
		{
		}

		// Token: 0x02000371 RID: 881
		[Serializable]
		public class TacticalConditionsContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.ConditionContainers.ConditionData>
		{
		}

		// Token: 0x02000372 RID: 882
		[Serializable]
		public class MagicalConditionsContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.ConditionContainers.ConditionData>
		{
		}

		// Token: 0x02000373 RID: 883
		[Serializable]
		public class ConditionData : SKALDProjectData.GameDataObject
		{
			// Token: 0x04000B4E RID: 2894
			public bool isMagical;

			// Token: 0x04000B4F RID: 2895
			public bool isAdvantage;

			// Token: 0x04000B50 RID: 2896
			public bool autoClearAtEndOfCombat;

			// Token: 0x04000B51 RID: 2897
			public bool restClears;

			// Token: 0x04000B52 RID: 2898
			public bool magicClears;

			// Token: 0x04000B53 RID: 2899
			public bool singleTurnEndOfTurn;

			// Token: 0x04000B54 RID: 2900
			public bool singleTurnStartOfTurn;

			// Token: 0x04000B55 RID: 2901
			public bool chanceToSaveEachTurn;

			// Token: 0x04000B56 RID: 2902
			public bool firstAidResolves;

			// Token: 0x04000B57 RID: 2903
			public string saveAttribute;

			// Token: 0x04000B58 RID: 2904
			public string resistanceAttribute;

			// Token: 0x04000B59 RID: 2905
			public string targetAnimation;

			// Token: 0x04000B5A RID: 2906
			public int primaryMagnitude;

			// Token: 0x04000B5B RID: 2907
			public int secondaryMagnitude;

			// Token: 0x04000B5C RID: 2908
			public int saveDifficulty;

			// Token: 0x04000B5D RID: 2909
			public float illuminationDegree;

			// Token: 0x04000B5E RID: 2910
			public int illuminationRange;

			// Token: 0x04000B5F RID: 2911
			public string illuminationImage;

			// Token: 0x04000B60 RID: 2912
			public List<string> baseConditionsCaused;

			// Token: 0x04000B61 RID: 2913
			public List<string> abilitiesConferred;

			// Token: 0x04000B62 RID: 2914
			public List<string> primaryAffectedAttributes;

			// Token: 0x04000B63 RID: 2915
			public List<string> secondaryAffectedAttributes;

			// Token: 0x04000B64 RID: 2916
			public List<string> targetParticleEffects;
		}
	}

	// Token: 0x02000216 RID: 534
	[Serializable]
	public class ApperanceElementContainers : SKALDProjectData.BaseContainerObject
	{
		// Token: 0x06001860 RID: 6240 RVA: 0x0006B9CC File Offset: 0x00069BCC
		public ApperanceElementContainers()
		{
			this.hairData = new SKALDProjectData.ApperanceElementContainers.HairContainer();
			this.beardData = new SKALDProjectData.ApperanceElementContainers.BeardContainer();
			this.portraitData = new SKALDProjectData.ApperanceElementContainers.PortraitContainer();
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x0006B9F5 File Offset: 0x00069BF5
		public override List<BaseDataObject> getBaseList()
		{
			List<BaseDataObject> baseList = base.getBaseList();
			baseList.Add(this.beardData);
			baseList.Add(this.hairData);
			baseList.Add(this.portraitData);
			return baseList;
		}

		// Token: 0x0400082E RID: 2094
		public SKALDProjectData.ApperanceElementContainers.HairContainer hairData;

		// Token: 0x0400082F RID: 2095
		public SKALDProjectData.ApperanceElementContainers.BeardContainer beardData;

		// Token: 0x04000830 RID: 2096
		public SKALDProjectData.ApperanceElementContainers.PortraitContainer portraitData;

		// Token: 0x02000374 RID: 884
		[Serializable]
		public class HairContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.ApperanceElementContainers.ApperanceElement>
		{
		}

		// Token: 0x02000375 RID: 885
		[Serializable]
		public class BeardContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.ApperanceElementContainers.ApperanceElement>
		{
		}

		// Token: 0x02000376 RID: 886
		[Serializable]
		public class PortraitContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.ApperanceElementContainers.ApperanceElement>
		{
		}

		// Token: 0x02000377 RID: 887
		[Serializable]
		public class ApperanceElement : SKALDProjectData.GameDataObject
		{
			// Token: 0x04000B65 RID: 2917
			public bool playerSelectable;

			// Token: 0x04000B66 RID: 2918
			public bool maleSelectable;

			// Token: 0x04000B67 RID: 2919
			public bool femaleSelectable;

			// Token: 0x04000B68 RID: 2920
			public bool deluxeEdition;
		}
	}

	// Token: 0x02000217 RID: 535
	[Serializable]
	public class EffectContainers : SKALDProjectData.BaseContainerObject
	{
		// Token: 0x06001862 RID: 6242 RVA: 0x0006BA21 File Offset: 0x00069C21
		public EffectContainers()
		{
			this.generalEffectContainer = new SKALDProjectData.EffectContainers.GeneralEffectContainer();
			this.damageEffectContainer = new SKALDProjectData.EffectContainers.DamageEffectContainer();
			this.healingEffectContainer = new SKALDProjectData.EffectContainers.BlessingEffectContainer();
			this.curseEffectContainer = new SKALDProjectData.EffectContainers.CurseEffectContainer();
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x0006BA55 File Offset: 0x00069C55
		public override List<BaseDataObject> getBaseList()
		{
			List<BaseDataObject> baseList = base.getBaseList();
			baseList.Add(this.generalEffectContainer);
			baseList.Add(this.healingEffectContainer);
			baseList.Add(this.damageEffectContainer);
			baseList.Add(this.curseEffectContainer);
			return baseList;
		}

		// Token: 0x04000831 RID: 2097
		public SKALDProjectData.EffectContainers.GeneralEffectContainer generalEffectContainer;

		// Token: 0x04000832 RID: 2098
		public SKALDProjectData.EffectContainers.DamageEffectContainer damageEffectContainer;

		// Token: 0x04000833 RID: 2099
		public SKALDProjectData.EffectContainers.BlessingEffectContainer healingEffectContainer;

		// Token: 0x04000834 RID: 2100
		public SKALDProjectData.EffectContainers.CurseEffectContainer curseEffectContainer;

		// Token: 0x02000378 RID: 888
		[Serializable]
		public class GeneralEffectContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.EffectContainers.EffectData>
		{
		}

		// Token: 0x02000379 RID: 889
		[Serializable]
		public class BlessingEffectContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.EffectContainers.EffectData>
		{
		}

		// Token: 0x0200037A RID: 890
		[Serializable]
		public class CurseEffectContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.EffectContainers.EffectData>
		{
		}

		// Token: 0x0200037B RID: 891
		[Serializable]
		public class DamageEffectContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.EffectContainers.EffectData>
		{
		}

		// Token: 0x0200037C RID: 892
		[Serializable]
		public class EffectData : SKALDProjectData.GameDataObject
		{
			// Token: 0x04000B69 RID: 2921
			public List<string> addedConditions;

			// Token: 0x04000B6A RID: 2922
			public List<string> removedConditions;

			// Token: 0x04000B6B RID: 2923
			public List<string> removedBaseConditions;

			// Token: 0x04000B6C RID: 2924
			public List<string> damageTypes;

			// Token: 0x04000B6D RID: 2925
			public List<string> userParticleEffects;

			// Token: 0x04000B6E RID: 2926
			public List<string> targetParticleEffects;

			// Token: 0x04000B6F RID: 2927
			public List<string> tileParticleEffects;

			// Token: 0x04000B70 RID: 2928
			public string userAttribute;

			// Token: 0x04000B71 RID: 2929
			public string targetAttribute;

			// Token: 0x04000B72 RID: 2930
			public string triggerOnTarget;

			// Token: 0x04000B73 RID: 2931
			public string soundEffects;

			// Token: 0x04000B74 RID: 2932
			public int minHealing;

			// Token: 0x04000B75 RID: 2933
			public int maxHealing;

			// Token: 0x04000B76 RID: 2934
			public int minAttunement;

			// Token: 0x04000B77 RID: 2935
			public int maxAttunement;

			// Token: 0x04000B78 RID: 2936
			public int minDamage;

			// Token: 0x04000B79 RID: 2937
			public int maxDamage;

			// Token: 0x04000B7A RID: 2938
			public int knockback;

			// Token: 0x04000B7B RID: 2939
			public bool isMagic;

			// Token: 0x04000B7C RID: 2940
			public bool saveForHalf;

			// Token: 0x04000B7D RID: 2941
			public bool randomizeCondition;

			// Token: 0x04000B7E RID: 2942
			public bool removeAllFirstAidConditions;

			// Token: 0x04000B7F RID: 2943
			public bool removeAllMagicalConditions;

			// Token: 0x04000B80 RID: 2944
			public bool removeAllRestClearsConditions;

			// Token: 0x04000B81 RID: 2945
			public bool removeAllConditions;

			// Token: 0x04000B82 RID: 2946
			public bool removeAllNegativeConditions;

			// Token: 0x04000B83 RID: 2947
			public bool removeAllPositiveConditions;

			// Token: 0x04000B84 RID: 2948
			public bool addNumericalBonus;
		}
	}

	// Token: 0x02000218 RID: 536
	[Serializable]
	public class CharacterContainers : SKALDProjectData.BaseContainerObject
	{
		// Token: 0x06001864 RID: 6244 RVA: 0x0006BA8D File Offset: 0x00069C8D
		public CharacterContainers()
		{
			this.uniqueHumanoids = new SKALDProjectData.CharacterContainers.UniqueHumanoidContainer();
			this.commonHumanoids = new SKALDProjectData.CharacterContainers.CommonHumanoidContainer();
			this.animals = new SKALDProjectData.CharacterContainers.AnimalContainer();
			this.monsters = new SKALDProjectData.CharacterContainers.MonsterContainer();
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x0006BAC1 File Offset: 0x00069CC1
		public override List<BaseDataObject> getBaseList()
		{
			List<BaseDataObject> baseList = base.getBaseList();
			baseList.Add(this.uniqueHumanoids);
			baseList.Add(this.commonHumanoids);
			baseList.Add(this.animals);
			baseList.Add(this.monsters);
			return baseList;
		}

		// Token: 0x04000835 RID: 2101
		public SKALDProjectData.CharacterContainers.UniqueHumanoidContainer uniqueHumanoids;

		// Token: 0x04000836 RID: 2102
		public SKALDProjectData.CharacterContainers.CommonHumanoidContainer commonHumanoids;

		// Token: 0x04000837 RID: 2103
		public SKALDProjectData.CharacterContainers.AnimalContainer animals;

		// Token: 0x04000838 RID: 2104
		public SKALDProjectData.CharacterContainers.MonsterContainer monsters;

		// Token: 0x0200037D RID: 893
		[Serializable]
		public class UniqueHumanoidContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.CharacterContainers.UniqueHumanoidContainer.UniqueHumanoid>
		{
			// Token: 0x02000418 RID: 1048
			[Serializable]
			public class UniqueHumanoid : SKALDProjectData.CharacterContainers.Humanoid
			{
			}
		}

		// Token: 0x0200037E RID: 894
		[Serializable]
		public class CommonHumanoidContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.CharacterContainers.CommonHumanoidContainer.CommonHumanoid>
		{
			// Token: 0x02000419 RID: 1049
			[Serializable]
			public class CommonHumanoid : SKALDProjectData.CharacterContainers.Humanoid
			{
			}
		}

		// Token: 0x0200037F RID: 895
		[Serializable]
		public class AnimalContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.CharacterContainers.AnimalContainer.Animal>
		{
			// Token: 0x0200041A RID: 1050
			[Serializable]
			public class Animal : SKALDProjectData.CharacterContainers.Beast
			{
			}
		}

		// Token: 0x02000380 RID: 896
		[Serializable]
		public class MonsterContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.CharacterContainers.MonsterContainer.Monster>
		{
			// Token: 0x0200041B RID: 1051
			[Serializable]
			public class Monster : SKALDProjectData.CharacterContainers.Beast
			{
			}
		}

		// Token: 0x02000381 RID: 897
		[Serializable]
		public abstract class Humanoid : SKALDProjectData.CharacterContainers.Character
		{
		}

		// Token: 0x02000382 RID: 898
		[Serializable]
		public abstract class Beast : SKALDProjectData.CharacterContainers.Character
		{
		}

		// Token: 0x02000383 RID: 899
		[Serializable]
		public abstract class Character : SKALDProjectData.GameDataObject
		{
			// Token: 0x04000B85 RID: 2949
			public bool persistent;

			// Token: 0x04000B86 RID: 2950
			public bool unique;

			// Token: 0x04000B87 RID: 2951
			public bool recruitable;

			// Token: 0x04000B88 RID: 2952
			public bool hostile;

			// Token: 0x04000B89 RID: 2953
			public bool paperDoll;

			// Token: 0x04000B8A RID: 2954
			public bool drawClothing;

			// Token: 0x04000B8B RID: 2955
			public bool paletteSwap;

			// Token: 0x04000B8C RID: 2956
			public bool fireable;

			// Token: 0x04000B8D RID: 2957
			public bool alert;

			// Token: 0x04000B8E RID: 2958
			public bool isMale;

			// Token: 0x04000B8F RID: 2959
			public bool afraid;

			// Token: 0x04000B90 RID: 2960
			public bool asleep;

			// Token: 0x04000B91 RID: 2961
			public bool deserts;

			// Token: 0x04000B92 RID: 2962
			public bool busy;

			// Token: 0x04000B93 RID: 2963
			public bool willTrade;

			// Token: 0x04000B94 RID: 2964
			public bool canAttack;

			// Token: 0x04000B95 RID: 2965
			public bool interactable;

			// Token: 0x04000B96 RID: 2966
			public bool deluxeEdition;

			// Token: 0x04000B97 RID: 2967
			public bool mercenary;

			// Token: 0x04000B98 RID: 2968
			public float mercenaryPriceModifier;

			// Token: 0x04000B99 RID: 2969
			public int tileWidth;

			// Token: 0x04000B9A RID: 2970
			public int tileHeight;

			// Token: 0x04000B9B RID: 2971
			public int level;

			// Token: 0x04000B9C RID: 2972
			public int levelOffset;

			// Token: 0x04000B9D RID: 2973
			public int maxLevel;

			// Token: 0x04000B9E RID: 2974
			public int strength;

			// Token: 0x04000B9F RID: 2975
			public int agility;

			// Token: 0x04000BA0 RID: 2976
			public int fortitude;

			// Token: 0x04000BA1 RID: 2977
			public int intellect;

			// Token: 0x04000BA2 RID: 2978
			public int presence;

			// Token: 0x04000BA3 RID: 2979
			public int baseReaction;

			// Token: 0x04000BA4 RID: 2980
			public int relationshipRank;

			// Token: 0x04000BA5 RID: 2981
			public int combatMoves;

			// Token: 0x04000BA6 RID: 2982
			public int modelXOffset;

			// Token: 0x04000BA7 RID: 2983
			public int modelYOffset;

			// Token: 0x04000BA8 RID: 2984
			public string race;

			// Token: 0x04000BA9 RID: 2985
			public string classKit;

			// Token: 0x04000BAA RID: 2986
			public string background;

			// Token: 0x04000BAB RID: 2987
			public List<string> factionList;

			// Token: 0x04000BAC RID: 2988
			public List<string> featsList;

			// Token: 0x04000BAD RID: 2989
			public List<string> abilityList;

			// Token: 0x04000BAE RID: 2990
			public List<string> apperancePackList;

			// Token: 0x04000BAF RID: 2991
			public List<string> loadoutList;

			// Token: 0x04000BB0 RID: 2992
			public List<string> loadoutLootList;

			// Token: 0x04000BB1 RID: 2993
			public List<string> spellsList;

			// Token: 0x04000BB2 RID: 2994
			public List<string> unarmedDamageType;

			// Token: 0x04000BB3 RID: 2995
			public string moveMode;

			// Token: 0x04000BB4 RID: 2996
			public string store;

			// Token: 0x04000BB5 RID: 2997
			public string becomeFriendlyTrigger;

			// Token: 0x04000BB6 RID: 2998
			public string becomeUnfriendlyTrigger;

			// Token: 0x04000BB7 RID: 2999
			public string deathTrigger;

			// Token: 0x04000BB8 RID: 3000
			public string victoryTrigger;

			// Token: 0x04000BB9 RID: 3001
			public string alertTrigger;

			// Token: 0x04000BBA RID: 3002
			public string clearAlertTrigger;

			// Token: 0x04000BBB RID: 3003
			public string spottedTrigger;

			// Token: 0x04000BBC RID: 3004
			public string talkTrigger;

			// Token: 0x04000BBD RID: 3005
			public string firstTimeTalkTrigger;

			// Token: 0x04000BBE RID: 3006
			public string isHitTrigger;

			// Token: 0x04000BBF RID: 3007
			public string halfLifeTrigger;

			// Token: 0x04000BC0 RID: 3008
			public string partyTalkTrigger;

			// Token: 0x04000BC1 RID: 3009
			public string contactTrigger;

			// Token: 0x04000BC2 RID: 3010
			public string approachTrigger;

			// Token: 0x04000BC3 RID: 3011
			public string firstTimeContactTrigger;

			// Token: 0x04000BC4 RID: 3012
			public string deathSound;

			// Token: 0x04000BC5 RID: 3013
			public string attackSound;
		}
	}

	// Token: 0x02000219 RID: 537
	[Serializable]
	public class AchievementContainers : SKALDProjectData.BaseContainerObject
	{
		// Token: 0x06001866 RID: 6246 RVA: 0x0006BAF9 File Offset: 0x00069CF9
		public AchievementContainers()
		{
			this.id = "Achievement Data";
			this.steamAchievementData = new SKALDProjectData.AchievementContainers.SteamContainer();
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x0006BB17 File Offset: 0x00069D17
		public override List<BaseDataObject> getBaseList()
		{
			List<BaseDataObject> baseList = base.getBaseList();
			baseList.Add(this.steamAchievementData);
			return baseList;
		}

		// Token: 0x04000839 RID: 2105
		public SKALDProjectData.AchievementContainers.SteamContainer steamAchievementData;

		// Token: 0x02000384 RID: 900
		[Serializable]
		public class SteamContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.AchievementContainers.Achievement>
		{
		}

		// Token: 0x02000385 RID: 901
		[Serializable]
		public class Achievement : SKALDProjectData.GameDataObject
		{
			// Token: 0x04000BC6 RID: 3014
			public int requiredAmount;

			// Token: 0x04000BC7 RID: 3015
			public string addedTrigger;

			// Token: 0x04000BC8 RID: 3016
			public string iconAchieved;

			// Token: 0x04000BC9 RID: 3017
			public string iconUnachieved;

			// Token: 0x04000BCA RID: 3018
			public List<string> conferredAbilitites;

			// Token: 0x04000BCB RID: 3019
			public List<string> countCreature;

			// Token: 0x04000BCC RID: 3020
			public List<string> countRace;

			// Token: 0x04000BCD RID: 3021
			public string statName;
		}
	}

	// Token: 0x0200021A RID: 538
	[Serializable]
	public class PropContainers : SKALDProjectData.BaseContainerObject
	{
		// Token: 0x06001868 RID: 6248 RVA: 0x0006BB2C File Offset: 0x00069D2C
		public PropContainers()
		{
			this.doors = new SKALDProjectData.PropContainers.DoorContainer();
			this.beds = new SKALDProjectData.PropContainers.BedContainer();
			this.warps = new SKALDProjectData.PropContainers.WarpContainer();
			this.containers = new SKALDProjectData.PropContainers.ContainerContainer();
			this.triggers = new SKALDProjectData.PropContainers.TriggerContainer();
			this.spawners = new SKALDProjectData.PropContainers.SpawnerContainer();
			this.lightSources = new SKALDProjectData.PropContainers.LightSourceContainer();
			this.interactives = new SKALDProjectData.PropContainers.InteractableContainer();
			this.testProps = new SKALDProjectData.PropContainers.TestPropContainer();
			this.beacons = new SKALDProjectData.PropContainers.BeaconContainer();
			this.decoratives = new SKALDProjectData.PropContainers.DecorativeContainer();
			this.traps = new SKALDProjectData.PropContainers.TrapContainer();
			this.inspectables = new SKALDProjectData.PropContainers.InspectableContainer();
			this.workBenches = new SKALDProjectData.PropContainers.WorkBenchContainer();
			this.pickups = new SKALDProjectData.PropContainers.PickupContainer();
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x0006BBE4 File Offset: 0x00069DE4
		public override List<BaseDataObject> getBaseList()
		{
			List<BaseDataObject> baseList = base.getBaseList();
			baseList.Add(this.doors);
			baseList.Add(this.beds);
			baseList.Add(this.warps);
			baseList.Add(this.containers);
			baseList.Add(this.triggers);
			baseList.Add(this.spawners);
			baseList.Add(this.lightSources);
			baseList.Add(this.interactives);
			baseList.Add(this.testProps);
			baseList.Add(this.beacons);
			baseList.Add(this.decoratives);
			baseList.Add(this.traps);
			baseList.Add(this.inspectables);
			baseList.Add(this.workBenches);
			baseList.Add(this.pickups);
			return baseList;
		}

		// Token: 0x0400083A RID: 2106
		public SKALDProjectData.PropContainers.DoorContainer doors;

		// Token: 0x0400083B RID: 2107
		public SKALDProjectData.PropContainers.BedContainer beds;

		// Token: 0x0400083C RID: 2108
		public SKALDProjectData.PropContainers.WarpContainer warps;

		// Token: 0x0400083D RID: 2109
		public SKALDProjectData.PropContainers.ContainerContainer containers;

		// Token: 0x0400083E RID: 2110
		public SKALDProjectData.PropContainers.TriggerContainer triggers;

		// Token: 0x0400083F RID: 2111
		public SKALDProjectData.PropContainers.SpawnerContainer spawners;

		// Token: 0x04000840 RID: 2112
		public SKALDProjectData.PropContainers.LightSourceContainer lightSources;

		// Token: 0x04000841 RID: 2113
		public SKALDProjectData.PropContainers.InteractableContainer interactives;

		// Token: 0x04000842 RID: 2114
		public SKALDProjectData.PropContainers.TestPropContainer testProps;

		// Token: 0x04000843 RID: 2115
		public SKALDProjectData.PropContainers.BeaconContainer beacons;

		// Token: 0x04000844 RID: 2116
		public SKALDProjectData.PropContainers.DecorativeContainer decoratives;

		// Token: 0x04000845 RID: 2117
		public SKALDProjectData.PropContainers.TrapContainer traps;

		// Token: 0x04000846 RID: 2118
		public SKALDProjectData.PropContainers.PickupContainer pickups;

		// Token: 0x04000847 RID: 2119
		public SKALDProjectData.PropContainers.InspectableContainer inspectables;

		// Token: 0x04000848 RID: 2120
		public SKALDProjectData.PropContainers.WorkBenchContainer workBenches;

		// Token: 0x02000386 RID: 902
		[Serializable]
		public class DoorContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.PropContainers.DoorContainer.Door>
		{
			// Token: 0x0200041C RID: 1052
			[Serializable]
			public class Door : SKALDProjectData.PropContainers.LockableProp
			{
			}
		}

		// Token: 0x02000387 RID: 903
		[Serializable]
		public class PickupContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.PropContainers.PickupContainer.Pickup>
		{
			// Token: 0x0200041D RID: 1053
			[Serializable]
			public class Pickup : SKALDProjectData.PropContainers.ActivatableProp
			{
				// Token: 0x04000D56 RID: 3414
				public List<string> pickupLoadout;
			}
		}

		// Token: 0x02000388 RID: 904
		[Serializable]
		public class BedContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.PropContainers.BedContainer.Bed>
		{
			// Token: 0x0200041E RID: 1054
			[Serializable]
			public class Bed : SKALDProjectData.PropContainers.ActivatableProp
			{
			}
		}

		// Token: 0x02000389 RID: 905
		[Serializable]
		public class WarpContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.PropContainers.WarpContainer.Warp>
		{
			// Token: 0x0200041F RID: 1055
			[Serializable]
			public class Warp : SKALDProjectData.PropContainers.LockableProp
			{
				// Token: 0x04000D57 RID: 3415
				public bool ascend;

				// Token: 0x04000D58 RID: 3416
				public bool descend;

				// Token: 0x04000D59 RID: 3417
				public bool toOverland;

				// Token: 0x04000D5A RID: 3418
				public bool toMap;

				// Token: 0x04000D5B RID: 3419
				public string map;
			}
		}

		// Token: 0x0200038A RID: 906
		[Serializable]
		public class WorkBenchContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.PropContainers.WorkBenchContainer.WorkBench>
		{
			// Token: 0x02000420 RID: 1056
			[Serializable]
			public class WorkBench : SKALDProjectData.PropContainers.ActivatableProp
			{
				// Token: 0x04000D5C RID: 3420
				public string craftingType;
			}
		}

		// Token: 0x0200038B RID: 907
		[Serializable]
		public class ContainerContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.PropContainers.ContainerContainer.Container>
		{
			// Token: 0x02000421 RID: 1057
			[Serializable]
			public class Container : SKALDProjectData.PropContainers.LockableProp
			{
				// Token: 0x04000D5D RID: 3421
				public string loadout;
			}
		}

		// Token: 0x0200038C RID: 908
		[Serializable]
		public class BeaconContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.PropContainers.BeaconContainer.Beacon>
		{
			// Token: 0x02000422 RID: 1058
			[Serializable]
			public class Beacon : SKALDProjectData.PropContainers.Prop
			{
			}
		}

		// Token: 0x0200038D RID: 909
		[Serializable]
		public class TriggerContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.PropContainers.TriggerContainer.Trigger>
		{
			// Token: 0x02000423 RID: 1059
			[Serializable]
			public class Trigger : SKALDProjectData.PropContainers.Prop
			{
				// Token: 0x04000D5E RID: 3422
				public string firstTimeTrigger;

				// Token: 0x04000D5F RID: 3423
				public string anyFirstTimeTrigger;

				// Token: 0x04000D60 RID: 3424
				public string tryEnterTrigger;

				// Token: 0x04000D61 RID: 3425
				public string enterTrigger;

				// Token: 0x04000D62 RID: 3426
				public string anyEnterTrigger;

				// Token: 0x04000D63 RID: 3427
				public string leaveTrigger;

				// Token: 0x04000D64 RID: 3428
				public string anyLeaveTrigger;

				// Token: 0x04000D65 RID: 3429
				public string digTrigger;

				// Token: 0x04000D66 RID: 3430
				public string combatLaunchTrigger;
			}
		}

		// Token: 0x0200038E RID: 910
		[Serializable]
		public class SpawnerContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.PropContainers.SpawnerContainer.Spawner>
		{
			// Token: 0x02000424 RID: 1060
			[Serializable]
			public class Spawner : SKALDProjectData.PropContainers.Prop
			{
			}
		}

		// Token: 0x0200038F RID: 911
		[Serializable]
		public class LightSourceContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.PropContainers.LightSourceContainer.LightSource>
		{
			// Token: 0x02000425 RID: 1061
			[Serializable]
			public class LightSource : SKALDProjectData.PropContainers.ActivatableProp
			{
				// Token: 0x04000D67 RID: 3431
				public int emitterX;

				// Token: 0x04000D68 RID: 3432
				public int emitterY;

				// Token: 0x04000D69 RID: 3433
				public string particleEffect;

				// Token: 0x04000D6A RID: 3434
				public string deactivateEffect;
			}
		}

		// Token: 0x02000390 RID: 912
		[Serializable]
		public class InteractableContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.PropContainers.InteractableContainer.Interactable>
		{
			// Token: 0x02000426 RID: 1062
			[Serializable]
			public class Interactable : SKALDProjectData.PropContainers.ActivatableProp
			{
			}
		}

		// Token: 0x02000391 RID: 913
		[Serializable]
		public class TestPropContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.PropContainers.TestPropContainer.TestProp>
		{
			// Token: 0x02000427 RID: 1063
			[Serializable]
			public class TestProp : SKALDProjectData.PropContainers.ActivatableProp
			{
				// Token: 0x04000D6B RID: 3435
				public int difficulty;

				// Token: 0x04000D6C RID: 3436
				public int bonusItemBonus;

				// Token: 0x04000D6D RID: 3437
				public bool testRandom;

				// Token: 0x04000D6E RID: 3438
				public bool repeatable;

				// Token: 0x04000D6F RID: 3439
				public bool consumeBonusItem;

				// Token: 0x04000D70 RID: 3440
				public string testAttribute;

				// Token: 0x04000D71 RID: 3441
				public string bonusItem;

				// Token: 0x04000D72 RID: 3442
				public string successTrigger;

				// Token: 0x04000D73 RID: 3443
				public string failureTrigger;

				// Token: 0x04000D74 RID: 3444
				public string successDescription;

				// Token: 0x04000D75 RID: 3445
				public string failureDescription;

				// Token: 0x04000D76 RID: 3446
				public string bonusItemTrigger;

				// Token: 0x04000D77 RID: 3447
				public string bonusItemVerb;

				// Token: 0x04000D78 RID: 3448
				public string bonusItemDescription;
			}
		}

		// Token: 0x02000392 RID: 914
		[Serializable]
		public class DecorativeContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.PropContainers.DecorativeContainer.Decorative>
		{
			// Token: 0x02000428 RID: 1064
			[Serializable]
			public class Decorative : SKALDProjectData.PropContainers.Prop
			{
			}
		}

		// Token: 0x02000393 RID: 915
		[Serializable]
		public class TrapContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.PropContainers.TrapContainer.Trap>
		{
			// Token: 0x02000429 RID: 1065
			[Serializable]
			public class Trap : SKALDProjectData.PropContainers.ActivatableProp
			{
			}
		}

		// Token: 0x02000394 RID: 916
		[Serializable]
		public class InspectableContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.PropContainers.InspectableContainer.Inspectable>
		{
			// Token: 0x0200042A RID: 1066
			[Serializable]
			public class Inspectable : SKALDProjectData.PropContainers.ActivatableProp
			{
			}
		}

		// Token: 0x02000395 RID: 917
		[Serializable]
		public abstract class LockableProp : SKALDProjectData.PropContainers.ActivatableProp
		{
			// Token: 0x04000BCE RID: 3022
			public bool locked;

			// Token: 0x04000BCF RID: 3023
			public bool forceable;

			// Token: 0x04000BD0 RID: 3024
			public bool pickable;

			// Token: 0x04000BD1 RID: 3025
			public int pickDifficulty;

			// Token: 0x04000BD2 RID: 3026
			public int forceDifficulty;

			// Token: 0x04000BD3 RID: 3027
			public string key;
		}

		// Token: 0x02000396 RID: 918
		[Serializable]
		public abstract class ActivatableProp : SKALDProjectData.PropContainers.Prop
		{
			// Token: 0x04000BD4 RID: 3028
			public bool playerInteractive;

			// Token: 0x04000BD5 RID: 3029
			public bool canDeactivate;

			// Token: 0x04000BD6 RID: 3030
			public bool canActivate;

			// Token: 0x04000BD7 RID: 3031
			public bool active;

			// Token: 0x04000BD8 RID: 3032
			public bool autoToggle;

			// Token: 0x04000BD9 RID: 3033
			public string verbTrigger;

			// Token: 0x04000BDA RID: 3034
			public string activeVerbTrigger;

			// Token: 0x04000BDB RID: 3035
			public string deactivatedVerbTrigger;

			// Token: 0x04000BDC RID: 3036
			public string activationTrigger;

			// Token: 0x04000BDD RID: 3037
			public string deactivationTrigger;

			// Token: 0x04000BDE RID: 3038
			public string verb;

			// Token: 0x04000BDF RID: 3039
			public string trap;

			// Token: 0x04000BE0 RID: 3040
			public string deactivatedImage;

			// Token: 0x04000BE1 RID: 3041
			public string deactivateSound;

			// Token: 0x04000BE2 RID: 3042
			public string activateSound;

			// Token: 0x04000BE3 RID: 3043
			public string deactivatedAnimation;
		}

		// Token: 0x02000397 RID: 919
		[Serializable]
		public class Prop : SKALDProjectData.GameDataObject
		{
			// Token: 0x04000BE4 RID: 3044
			public bool fullModel;

			// Token: 0x04000BE5 RID: 3045
			public bool loopingAnimation;

			// Token: 0x04000BE6 RID: 3046
			public bool impassable;

			// Token: 0x04000BE7 RID: 3047
			public bool visibility;

			// Token: 0x04000BE8 RID: 3048
			public bool unique;

			// Token: 0x04000BE9 RID: 3049
			public bool forceEditorIcon;

			// Token: 0x04000BEA RID: 3050
			public bool highlight;

			// Token: 0x04000BEB RID: 3051
			public bool preferForeground;

			// Token: 0x04000BEC RID: 3052
			public bool hidden;

			// Token: 0x04000BED RID: 3053
			public bool colorSwap;

			// Token: 0x04000BEE RID: 3054
			public bool illegal;

			// Token: 0x04000BEF RID: 3055
			public bool deluxeEdition;

			// Token: 0x04000BF0 RID: 3056
			public float lightStrength;

			// Token: 0x04000BF1 RID: 3057
			public int modelFrame;

			// Token: 0x04000BF2 RID: 3058
			public int light;

			// Token: 0x04000BF3 RID: 3059
			public int width;

			// Token: 0x04000BF4 RID: 3060
			public int height;

			// Token: 0x04000BF5 RID: 3061
			public int spotDC;

			// Token: 0x04000BF6 RID: 3062
			public int pixelXOffest;

			// Token: 0x04000BF7 RID: 3063
			public int pixelYOffest;

			// Token: 0x04000BF8 RID: 3064
			public string animation;

			// Token: 0x04000BF9 RID: 3065
			public string lightModel;

			// Token: 0x04000BFA RID: 3066
			public string faction;

			// Token: 0x04000BFB RID: 3067
			public string spottedTrigger;

			// Token: 0x04000BFC RID: 3068
			public string endOfAnimationTrigger;

			// Token: 0x04000BFD RID: 3069
			public string apperance;

			// Token: 0x04000BFE RID: 3070
			public string animationStrip;
		}
	}

	// Token: 0x0200021B RID: 539
	[Serializable]
	public class TerrainContainers : SKALDProjectData.BaseContainerObject
	{
		// Token: 0x0600186A RID: 6250 RVA: 0x0006BCAB File Offset: 0x00069EAB
		public TerrainContainers()
		{
			this.terrainTiles = new SKALDProjectData.TerrainContainers.TerrainTileContainer();
			this.technicalTiles = new SKALDProjectData.TerrainContainers.TechnicalTileContainer();
			this.infoTiles = new SKALDProjectData.TerrainContainers.InfoTileContainer();
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x0006BCD4 File Offset: 0x00069ED4
		public override List<BaseDataObject> getBaseList()
		{
			List<BaseDataObject> baseList = base.getBaseList();
			baseList.Add(this.terrainTiles);
			baseList.Add(this.technicalTiles);
			baseList.Add(this.infoTiles);
			return baseList;
		}

		// Token: 0x04000849 RID: 2121
		public SKALDProjectData.TerrainContainers.TerrainTileContainer terrainTiles;

		// Token: 0x0400084A RID: 2122
		public SKALDProjectData.TerrainContainers.TechnicalTileContainer technicalTiles;

		// Token: 0x0400084B RID: 2123
		public SKALDProjectData.TerrainContainers.InfoTileContainer infoTiles;

		// Token: 0x02000398 RID: 920
		[Serializable]
		public class TerrainTileContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.TerrainContainers.TerrainTile>
		{
		}

		// Token: 0x02000399 RID: 921
		[Serializable]
		public class TechnicalTileContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.TerrainContainers.TerrainTile>
		{
		}

		// Token: 0x0200039A RID: 922
		[Serializable]
		public class InfoTileContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.TerrainContainers.TerrainTile>
		{
		}

		// Token: 0x0200039B RID: 923
		[Serializable]
		public class TerrainTile : SKALDProjectData.GameDataObject
		{
			// Token: 0x04000BFF RID: 3071
			public bool visibility;

			// Token: 0x04000C00 RID: 3072
			public bool technical;

			// Token: 0x04000C01 RID: 3073
			public bool infoTile;

			// Token: 0x04000C02 RID: 3074
			public bool impassable;

			// Token: 0x04000C03 RID: 3075
			public bool concealed;

			// Token: 0x04000C04 RID: 3076
			public bool forceEditorIcon;

			// Token: 0x04000C05 RID: 3077
			public bool water;

			// Token: 0x04000C06 RID: 3078
			public bool forceOutside;

			// Token: 0x04000C07 RID: 3079
			public bool overlay;

			// Token: 0x04000C08 RID: 3080
			public bool npcBlocked;

			// Token: 0x04000C09 RID: 3081
			public bool animateAsWater;

			// Token: 0x04000C0A RID: 3082
			public bool drawBehind;

			// Token: 0x04000C0B RID: 3083
			public int modelFrame;

			// Token: 0x04000C0C RID: 3084
			public int encounterChance;

			// Token: 0x04000C0D RID: 3085
			public int sheetWidth;

			// Token: 0x04000C0E RID: 3086
			public int sheetHeight;

			// Token: 0x04000C0F RID: 3087
			public int sheetPixelWidth;

			// Token: 0x04000C10 RID: 3088
			public int sheetPadding;

			// Token: 0x04000C11 RID: 3089
			public int light;

			// Token: 0x04000C12 RID: 3090
			public string animation;

			// Token: 0x04000C13 RID: 3091
			public List<string> encounterMaps;
		}
	}

	// Token: 0x0200021C RID: 540
	[Serializable]
	public class CutSceneDataContainers : SKALDProjectData.BaseContainerObject
	{
		// Token: 0x0600186C RID: 6252 RVA: 0x0006BD00 File Offset: 0x00069F00
		public CutSceneDataContainers()
		{
			this.id = "Cutscene Data";
			this.cutScenes = new SKALDProjectData.CutSceneDataContainers.CutScenesGeneric();
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x0006BD1E File Offset: 0x00069F1E
		public override List<BaseDataObject> getBaseList()
		{
			List<BaseDataObject> baseList = base.getBaseList();
			baseList.Add(this.cutScenes);
			return baseList;
		}

		// Token: 0x0400084C RID: 2124
		public SKALDProjectData.CutSceneDataContainers.CutScenesGeneric cutScenes;

		// Token: 0x0200039C RID: 924
		[Serializable]
		public class CutScenesGeneric : SKALDProjectData.CutSceneDataContainers.CutScenesBase
		{
			// Token: 0x06001D16 RID: 7446 RVA: 0x0007B6D0 File Offset: 0x000798D0
			public CutScenesGeneric()
			{
				this.id = "Generic Cutscenes";
			}
		}

		// Token: 0x0200039D RID: 925
		[Serializable]
		public abstract class CutScenesBase : SKALDProjectData.SpecificContainerObject<SKALDProjectData.CutSceneDataContainers.CutScenesBase.CutScenesData>
		{
			// Token: 0x06001D17 RID: 7447 RVA: 0x0007B6E3 File Offset: 0x000798E3
			public CutScenesBase()
			{
				this.id = "Cutscenes";
			}

			// Token: 0x06001D18 RID: 7448 RVA: 0x0007B6F8 File Offset: 0x000798F8
			public override List<string> getFlatIdList(List<string> resultList)
			{
				foreach (BaseDataObject baseDataObject in this.baseDataObjectList)
				{
					resultList.Add(baseDataObject.id);
				}
				return resultList;
			}

			// Token: 0x0200042B RID: 1067
			[Serializable]
			public class CutScenesData : SKALDProjectData.SpecificContainerObject<SKALDProjectData.CutSceneDataContainers.CutScenesBase.CutScenesData.ParallaxLayer>
			{
				// Token: 0x04000D79 RID: 3449
				public int duration;

				// Token: 0x04000D7A RID: 3450
				public int delay;

				// Token: 0x04000D7B RID: 3451
				public int parSpeedRatio;

				// Token: 0x04000D7C RID: 3452
				public string folderPath;

				// Token: 0x04000D7D RID: 3453
				public string descriptionLine;

				// Token: 0x04000D7E RID: 3454
				public string exitTrigger;

				// Token: 0x04000D7F RID: 3455
				public string nextCutScene;

				// Token: 0x04000D80 RID: 3456
				public string musicPath;

				// Token: 0x04000D81 RID: 3457
				public bool arrivalScroll;

				// Token: 0x02000439 RID: 1081
				[Serializable]
				public class ParallaxLayer : BaseDataObject
				{
					// Token: 0x04000DB6 RID: 3510
					public float homingSpeed;

					// Token: 0x04000DB7 RID: 3511
					public int xBasePosition;

					// Token: 0x04000DB8 RID: 3512
					public int yBasePosition;

					// Token: 0x04000DB9 RID: 3513
					public int xTargetPosition;

					// Token: 0x04000DBA RID: 3514
					public int yTargetPosition;

					// Token: 0x04000DBB RID: 3515
					public int xDegree;

					// Token: 0x04000DBC RID: 3516
					public int yDegree;

					// Token: 0x04000DBD RID: 3517
					public string imagePath;
				}
			}
		}
	}

	// Token: 0x0200021D RID: 541
	[Serializable]
	public class LoadoutDataContainers : SKALDProjectData.BaseContainerObject
	{
		// Token: 0x0600186E RID: 6254 RVA: 0x0006BD32 File Offset: 0x00069F32
		public LoadoutDataContainers()
		{
			this.characterLoadouts = new SKALDProjectData.LoadoutDataContainers.CharacterLoadout();
			this.containerLoadouts = new SKALDProjectData.LoadoutDataContainers.ContainerLoadout();
			this.storeLoadouts = new SKALDProjectData.LoadoutDataContainers.StoreLoadout();
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x0006BD5B File Offset: 0x00069F5B
		public override List<BaseDataObject> getBaseList()
		{
			List<BaseDataObject> baseList = base.getBaseList();
			baseList.Add(this.characterLoadouts);
			baseList.Add(this.containerLoadouts);
			baseList.Add(this.storeLoadouts);
			return baseList;
		}

		// Token: 0x0400084D RID: 2125
		public SKALDProjectData.LoadoutDataContainers.CharacterLoadout characterLoadouts;

		// Token: 0x0400084E RID: 2126
		public SKALDProjectData.LoadoutDataContainers.ContainerLoadout containerLoadouts;

		// Token: 0x0400084F RID: 2127
		public SKALDProjectData.LoadoutDataContainers.StoreLoadout storeLoadouts;

		// Token: 0x0200039E RID: 926
		[Serializable]
		public class CharacterLoadout : SKALDProjectData.LoadoutDataContainers.LoadoutDataContainer
		{
		}

		// Token: 0x0200039F RID: 927
		[Serializable]
		public class ContainerLoadout : SKALDProjectData.LoadoutDataContainers.LoadoutDataContainer
		{
		}

		// Token: 0x020003A0 RID: 928
		[Serializable]
		public class StoreLoadout : SKALDProjectData.LoadoutDataContainers.LoadoutDataContainer
		{
		}

		// Token: 0x020003A1 RID: 929
		[Serializable]
		public abstract class LoadoutDataContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.LoadoutDataContainers.LoadoutDataContainer.LoadoutData>
		{
			// Token: 0x06001D1C RID: 7452 RVA: 0x0007B76C File Offset: 0x0007996C
			public override List<string> getFlatIdList(List<string> resultList)
			{
				foreach (BaseDataObject baseDataObject in this.baseDataObjectList)
				{
					resultList.Add(baseDataObject.id);
				}
				return resultList;
			}

			// Token: 0x0200042C RID: 1068
			[Serializable]
			public class LoadoutData : SKALDProjectData.SpecificContainerObject<SKALDProjectData.LoadoutDataContainers.LoadoutDataContainer.LoadoutData.LoadoutEntry>
			{
				// Token: 0x04000D82 RID: 3458
				public int minGold;

				// Token: 0x04000D83 RID: 3459
				public int maxGold;

				// Token: 0x0200043A RID: 1082
				[Serializable]
				public class LoadoutEntry : BaseDataObject
				{
					// Token: 0x04000DBE RID: 3518
					public int maxRarity;

					// Token: 0x04000DBF RID: 3519
					public int minRarity;

					// Token: 0x04000DC0 RID: 3520
					public int maxMagic;

					// Token: 0x04000DC1 RID: 3521
					public int minMagic;

					// Token: 0x04000DC2 RID: 3522
					public int dynamicChance;

					// Token: 0x04000DC3 RID: 3523
					public int dynamicMinNumber;

					// Token: 0x04000DC4 RID: 3524
					public int dynamicMaxNumber;

					// Token: 0x04000DC5 RID: 3525
					public int setChance;

					// Token: 0x04000DC6 RID: 3526
					public int setMinNumber;

					// Token: 0x04000DC7 RID: 3527
					public int setMaxNumber;

					// Token: 0x04000DC8 RID: 3528
					public bool randomizeSetItems;

					// Token: 0x04000DC9 RID: 3529
					public bool randomizeDynamicItems;

					// Token: 0x04000DCA RID: 3530
					public bool deluxeEdition;

					// Token: 0x04000DCB RID: 3531
					public List<string> itemTypesList;

					// Token: 0x04000DCC RID: 3532
					public List<string> setItemsList;

					// Token: 0x04000DCD RID: 3533
					public List<string> altSetItemsList;
				}
			}
		}
	}

	// Token: 0x0200021E RID: 542
	[Serializable]
	public class QuestContainers : SKALDProjectData.BaseContainerObject
	{
		// Token: 0x06001870 RID: 6256 RVA: 0x0006BD87 File Offset: 0x00069F87
		public QuestContainers()
		{
			this.sideQuests = new SKALDProjectData.QuestContainers.SideQuests();
			this.mainQuests = new SKALDProjectData.QuestContainers.MainQuests();
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x0006BDA5 File Offset: 0x00069FA5
		public override List<BaseDataObject> getBaseList()
		{
			List<BaseDataObject> baseList = base.getBaseList();
			baseList.Add(this.sideQuests);
			baseList.Add(this.mainQuests);
			return baseList;
		}

		// Token: 0x04000850 RID: 2128
		public SKALDProjectData.QuestContainers.SideQuests sideQuests;

		// Token: 0x04000851 RID: 2129
		public SKALDProjectData.QuestContainers.MainQuests mainQuests;

		// Token: 0x020003A2 RID: 930
		[Serializable]
		public class SideQuests : SKALDProjectData.SpecificContainerObject<SKALDProjectData.QuestContainers.QuestData>
		{
		}

		// Token: 0x020003A3 RID: 931
		[Serializable]
		public class MainQuests : SKALDProjectData.SpecificContainerObject<SKALDProjectData.QuestContainers.QuestData>
		{
		}

		// Token: 0x020003A4 RID: 932
		[Serializable]
		public class QuestData : SKALDProjectData.SpecificContainerObject<SKALDProjectData.QuestContainers.QuestData>
		{
			// Token: 0x06001D20 RID: 7456 RVA: 0x0007B7E0 File Offset: 0x000799E0
			public override List<BaseDataObject> getBaseList()
			{
				List<BaseDataObject> baseList = base.getBaseList();
				List<BaseDataObject> list = new List<BaseDataObject>();
				baseList.Add(this);
				foreach (BaseDataObject baseDataObject in base.getBaseList())
				{
					foreach (BaseDataObject baseDataObject2 in ((SKALDProjectData.SpecificContainerObject<SKALDProjectData.QuestContainers.QuestData>)baseDataObject).getBaseList())
					{
						SKALDProjectData.SpecificContainerObject<SKALDProjectData.QuestContainers.QuestData> item = (SKALDProjectData.SpecificContainerObject<SKALDProjectData.QuestContainers.QuestData>)baseDataObject2;
						list.Add(item);
					}
				}
				foreach (BaseDataObject baseDataObject3 in list)
				{
					SKALDProjectData.SpecificContainerObject<SKALDProjectData.QuestContainers.QuestData> item2 = (SKALDProjectData.SpecificContainerObject<SKALDProjectData.QuestContainers.QuestData>)baseDataObject3;
					baseList.Add(item2);
				}
				return baseList;
			}

			// Token: 0x06001D21 RID: 7457 RVA: 0x0007B8D4 File Offset: 0x00079AD4
			public override List<string> getFlatIdList(List<string> resultList)
			{
				resultList.Add(this.id);
				foreach (BaseDataObject baseDataObject in this.baseDataObjectList)
				{
					if (baseDataObject is SKALDProjectData.BaseContainerObject)
					{
						(baseDataObject as SKALDProjectData.BaseContainerObject).getFlatIdList(resultList);
					}
					else
					{
						resultList.Add(baseDataObject.id);
					}
				}
				return resultList;
			}

			// Token: 0x04000C14 RID: 3092
			public bool open;

			// Token: 0x04000C15 RID: 3093
			public bool main;

			// Token: 0x04000C16 RID: 3094
			public bool completesParent;

			// Token: 0x04000C17 RID: 3095
			public bool optional;

			// Token: 0x04000C18 RID: 3096
			public int rewardXP;

			// Token: 0x04000C19 RID: 3097
			public int rewardXPPercentage;

			// Token: 0x04000C1A RID: 3098
			public int rewardGold;

			// Token: 0x04000C1B RID: 3099
			public string begunDescription;

			// Token: 0x04000C1C RID: 3100
			public string title;

			// Token: 0x04000C1D RID: 3101
			public string completedDescription;

			// Token: 0x04000C1E RID: 3102
			public string failedDescription;

			// Token: 0x04000C1F RID: 3103
			public string rewardDescription;

			// Token: 0x04000C20 RID: 3104
			public string aboutDescription;

			// Token: 0x04000C21 RID: 3105
			public string begunLogString;

			// Token: 0x04000C22 RID: 3106
			public string aboutLogString;

			// Token: 0x04000C23 RID: 3107
			public string completedLogString;

			// Token: 0x04000C24 RID: 3108
			public string failedLogString;

			// Token: 0x04000C25 RID: 3109
			public string rewardLogString;

			// Token: 0x04000C26 RID: 3110
			public string openTrigger;

			// Token: 0x04000C27 RID: 3111
			public string begunTrigger;

			// Token: 0x04000C28 RID: 3112
			public string completedTrigger;

			// Token: 0x04000C29 RID: 3113
			public string failedTrigger;

			// Token: 0x04000C2A RID: 3114
			public string rewardTrigger;

			// Token: 0x04000C2B RID: 3115
			public string parentQuest;

			// Token: 0x04000C2C RID: 3116
			public List<string> startsSubQuestsList;

			// Token: 0x04000C2D RID: 3117
			public List<string> prerequisiteQuestsList;

			// Token: 0x04000C2E RID: 3118
			public List<string> nextQuestList;

			// Token: 0x04000C2F RID: 3119
			public string successCriteria;

			// Token: 0x04000C30 RID: 3120
			public string rewardLoadout;
		}
	}

	// Token: 0x0200021F RID: 543
	[Serializable]
	public class EnchantmentContainers : SKALDProjectData.BaseContainerObject
	{
		// Token: 0x06001872 RID: 6258 RVA: 0x0006BDC8 File Offset: 0x00069FC8
		public EnchantmentContainers()
		{
			this.weaponEnchantmentContainer = new SKALDProjectData.EnchantmentContainers.WeaponEnchantmentContainer();
			this.armorEnchantmentContainer = new SKALDProjectData.EnchantmentContainers.ArmorEnchantmentContainer();
			this.shieldEnchantmentContainer = new SKALDProjectData.EnchantmentContainers.ShieldEnchantmentContainer();
			this.accessoryEnchantmentContainer = new SKALDProjectData.EnchantmentContainers.AccessoryEnchantmentContainer();
			this.jewelryEnchantmentContainer = new SKALDProjectData.EnchantmentContainers.JewelryEnchantmentContainer();
			this.generalEnchantmentContainer = new SKALDProjectData.EnchantmentContainers.GeneralEnchantmentContainer();
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x0006BE20 File Offset: 0x0006A020
		public override List<BaseDataObject> getBaseList()
		{
			List<BaseDataObject> baseList = base.getBaseList();
			baseList.Add(this.weaponEnchantmentContainer);
			baseList.Add(this.armorEnchantmentContainer);
			baseList.Add(this.shieldEnchantmentContainer);
			baseList.Add(this.accessoryEnchantmentContainer);
			baseList.Add(this.jewelryEnchantmentContainer);
			baseList.Add(this.generalEnchantmentContainer);
			return baseList;
		}

		// Token: 0x04000852 RID: 2130
		public SKALDProjectData.EnchantmentContainers.WeaponEnchantmentContainer weaponEnchantmentContainer;

		// Token: 0x04000853 RID: 2131
		public SKALDProjectData.EnchantmentContainers.ArmorEnchantmentContainer armorEnchantmentContainer;

		// Token: 0x04000854 RID: 2132
		public SKALDProjectData.EnchantmentContainers.ShieldEnchantmentContainer shieldEnchantmentContainer;

		// Token: 0x04000855 RID: 2133
		public SKALDProjectData.EnchantmentContainers.AccessoryEnchantmentContainer accessoryEnchantmentContainer;

		// Token: 0x04000856 RID: 2134
		public SKALDProjectData.EnchantmentContainers.JewelryEnchantmentContainer jewelryEnchantmentContainer;

		// Token: 0x04000857 RID: 2135
		public SKALDProjectData.EnchantmentContainers.GeneralEnchantmentContainer generalEnchantmentContainer;

		// Token: 0x020003A5 RID: 933
		[Serializable]
		public class WeaponEnchantmentContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.EnchantmentContainers.Enchantment>
		{
		}

		// Token: 0x020003A6 RID: 934
		[Serializable]
		public class ArmorEnchantmentContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.EnchantmentContainers.Enchantment>
		{
		}

		// Token: 0x020003A7 RID: 935
		[Serializable]
		public class GeneralEnchantmentContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.EnchantmentContainers.Enchantment>
		{
		}

		// Token: 0x020003A8 RID: 936
		[Serializable]
		public class ShieldEnchantmentContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.EnchantmentContainers.Enchantment>
		{
		}

		// Token: 0x020003A9 RID: 937
		[Serializable]
		public class AccessoryEnchantmentContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.EnchantmentContainers.Enchantment>
		{
		}

		// Token: 0x020003AA RID: 938
		[Serializable]
		public class JewelryEnchantmentContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.EnchantmentContainers.Enchantment>
		{
		}

		// Token: 0x020003AB RID: 939
		[Serializable]
		public class Enchantment : SKALDProjectData.GameDataObject
		{
			// Token: 0x04000C31 RID: 3121
			public List<string> applicableItems;

			// Token: 0x04000C32 RID: 3122
			public int magicLevel;

			// Token: 0x04000C33 RID: 3123
			public int basePrice;

			// Token: 0x04000C34 RID: 3124
			public float valueMultiplier;

			// Token: 0x04000C35 RID: 3125
			public int soak;

			// Token: 0x04000C36 RID: 3126
			public int encumberance;

			// Token: 0x04000C37 RID: 3127
			public int maxDamage;

			// Token: 0x04000C38 RID: 3128
			public int minDamage;

			// Token: 0x04000C39 RID: 3129
			public int hitBonus;

			// Token: 0x04000C3A RID: 3130
			public float crit;

			// Token: 0x04000C3B RID: 3131
			public string hitTrigger;

			// Token: 0x04000C3C RID: 3132
			public string critTrigger;

			// Token: 0x04000C3D RID: 3133
			public List<string> hitEffect;

			// Token: 0x04000C3E RID: 3134
			public List<string> critEffect;

			// Token: 0x04000C3F RID: 3135
			public List<string> damageType;

			// Token: 0x04000C40 RID: 3136
			public List<string> conferredAbilities;

			// Token: 0x04000C41 RID: 3137
			public List<string> conferredConditions;

			// Token: 0x04000C42 RID: 3138
			public List<string> conferredSpells;

			// Token: 0x04000C43 RID: 3139
			public List<string> useEffect;

			// Token: 0x04000C44 RID: 3140
			public string primaryColor;

			// Token: 0x04000C45 RID: 3141
			public string secondaryColor;

			// Token: 0x04000C46 RID: 3142
			public string prefix;

			// Token: 0x04000C47 RID: 3143
			public string suffix;
		}
	}

	// Token: 0x02000220 RID: 544
	[Serializable]
	public class JournalContainers : SKALDProjectData.BaseContainerObject
	{
		// Token: 0x06001874 RID: 6260 RVA: 0x0006BE7C File Offset: 0x0006A07C
		public JournalContainers()
		{
			this.chapter0Container = new SKALDProjectData.JournalContainers.Chapter0Container();
			this.chapter1Container = new SKALDProjectData.JournalContainers.Chapter1Container();
			this.chapter2Container = new SKALDProjectData.JournalContainers.Chapter2Container();
			this.chapter3Container = new SKALDProjectData.JournalContainers.Chapter3Container();
			this.chapter4Container = new SKALDProjectData.JournalContainers.Chapter4Container();
			this.chapter5Container = new SKALDProjectData.JournalContainers.Chapter5Container();
			this.chapter6Container = new SKALDProjectData.JournalContainers.Chapter6Container();
			this.chapter7Container = new SKALDProjectData.JournalContainers.Chapter7Container();
			this.chapter8Container = new SKALDProjectData.JournalContainers.Chapter8Container();
			this.chapter9Container = new SKALDProjectData.JournalContainers.Chapter9Container();
			this.charactersContainer = new SKALDProjectData.JournalContainers.CharactersContainer();
			this.miscContainer = new SKALDProjectData.JournalContainers.MiscContainer();
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x0006BF14 File Offset: 0x0006A114
		public override List<BaseDataObject> getBaseList()
		{
			List<BaseDataObject> baseList = base.getBaseList();
			baseList.Add(this.chapter0Container);
			baseList.Add(this.chapter1Container);
			baseList.Add(this.chapter2Container);
			baseList.Add(this.chapter3Container);
			baseList.Add(this.chapter4Container);
			baseList.Add(this.chapter5Container);
			baseList.Add(this.chapter6Container);
			baseList.Add(this.chapter7Container);
			baseList.Add(this.chapter8Container);
			baseList.Add(this.chapter9Container);
			baseList.Add(this.charactersContainer);
			baseList.Add(this.miscContainer);
			return baseList;
		}

		// Token: 0x04000858 RID: 2136
		public SKALDProjectData.JournalContainers.Chapter0Container chapter0Container;

		// Token: 0x04000859 RID: 2137
		public SKALDProjectData.JournalContainers.Chapter1Container chapter1Container;

		// Token: 0x0400085A RID: 2138
		public SKALDProjectData.JournalContainers.Chapter2Container chapter2Container;

		// Token: 0x0400085B RID: 2139
		public SKALDProjectData.JournalContainers.Chapter3Container chapter3Container;

		// Token: 0x0400085C RID: 2140
		public SKALDProjectData.JournalContainers.Chapter4Container chapter4Container;

		// Token: 0x0400085D RID: 2141
		public SKALDProjectData.JournalContainers.Chapter5Container chapter5Container;

		// Token: 0x0400085E RID: 2142
		public SKALDProjectData.JournalContainers.Chapter6Container chapter6Container;

		// Token: 0x0400085F RID: 2143
		public SKALDProjectData.JournalContainers.Chapter7Container chapter7Container;

		// Token: 0x04000860 RID: 2144
		public SKALDProjectData.JournalContainers.Chapter8Container chapter8Container;

		// Token: 0x04000861 RID: 2145
		public SKALDProjectData.JournalContainers.Chapter9Container chapter9Container;

		// Token: 0x04000862 RID: 2146
		public SKALDProjectData.JournalContainers.CharactersContainer charactersContainer;

		// Token: 0x04000863 RID: 2147
		public SKALDProjectData.JournalContainers.MiscContainer miscContainer;

		// Token: 0x020003AC RID: 940
		[Serializable]
		public class Chapter0Container : SKALDProjectData.SpecificContainerObject<SKALDProjectData.JournalContainers.JournalEntry>
		{
		}

		// Token: 0x020003AD RID: 941
		[Serializable]
		public class Chapter1Container : SKALDProjectData.SpecificContainerObject<SKALDProjectData.JournalContainers.JournalEntry>
		{
		}

		// Token: 0x020003AE RID: 942
		[Serializable]
		public class Chapter2Container : SKALDProjectData.SpecificContainerObject<SKALDProjectData.JournalContainers.JournalEntry>
		{
		}

		// Token: 0x020003AF RID: 943
		[Serializable]
		public class Chapter3Container : SKALDProjectData.SpecificContainerObject<SKALDProjectData.JournalContainers.JournalEntry>
		{
		}

		// Token: 0x020003B0 RID: 944
		[Serializable]
		public class Chapter4Container : SKALDProjectData.SpecificContainerObject<SKALDProjectData.JournalContainers.JournalEntry>
		{
		}

		// Token: 0x020003B1 RID: 945
		[Serializable]
		public class Chapter5Container : SKALDProjectData.SpecificContainerObject<SKALDProjectData.JournalContainers.JournalEntry>
		{
		}

		// Token: 0x020003B2 RID: 946
		[Serializable]
		public class Chapter6Container : SKALDProjectData.SpecificContainerObject<SKALDProjectData.JournalContainers.JournalEntry>
		{
		}

		// Token: 0x020003B3 RID: 947
		[Serializable]
		public class Chapter7Container : SKALDProjectData.SpecificContainerObject<SKALDProjectData.JournalContainers.JournalEntry>
		{
		}

		// Token: 0x020003B4 RID: 948
		[Serializable]
		public class Chapter8Container : SKALDProjectData.SpecificContainerObject<SKALDProjectData.JournalContainers.JournalEntry>
		{
		}

		// Token: 0x020003B5 RID: 949
		[Serializable]
		public class Chapter9Container : SKALDProjectData.SpecificContainerObject<SKALDProjectData.JournalContainers.JournalEntry>
		{
		}

		// Token: 0x020003B6 RID: 950
		[Serializable]
		public class CharactersContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.JournalContainers.JournalEntry>
		{
		}

		// Token: 0x020003B7 RID: 951
		[Serializable]
		public class MiscContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.JournalContainers.JournalEntry>
		{
		}

		// Token: 0x020003B8 RID: 952
		[Serializable]
		public class JournalEntry : SKALDProjectData.GameDataObject
		{
			// Token: 0x04000C48 RID: 3144
			public string addTrigger;
		}
	}

	// Token: 0x02000221 RID: 545
	[Serializable]
	public class BackgroundContainers : SKALDProjectData.BaseContainerObject
	{
		// Token: 0x06001876 RID: 6262 RVA: 0x0006BFB7 File Offset: 0x0006A1B7
		public BackgroundContainers()
		{
			this.npcBackgroundContainer = new SKALDProjectData.BackgroundContainers.NPCBackgroundContainer();
			this.backgrounContainer = new SKALDProjectData.BackgroundContainers.BackgroundContainer();
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x0006BFD5 File Offset: 0x0006A1D5
		public override List<BaseDataObject> getBaseList()
		{
			List<BaseDataObject> baseList = base.getBaseList();
			baseList.Add(this.npcBackgroundContainer);
			baseList.Add(this.backgrounContainer);
			return baseList;
		}

		// Token: 0x04000864 RID: 2148
		public SKALDProjectData.BackgroundContainers.NPCBackgroundContainer npcBackgroundContainer;

		// Token: 0x04000865 RID: 2149
		public SKALDProjectData.BackgroundContainers.BackgroundContainer backgrounContainer;

		// Token: 0x020003B9 RID: 953
		[Serializable]
		public class NPCBackgroundContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.BackgroundContainers.BackgroundData>
		{
		}

		// Token: 0x020003BA RID: 954
		[Serializable]
		public class BackgroundContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.BackgroundContainers.BackgroundData>
		{
		}

		// Token: 0x020003BB RID: 955
		[Serializable]
		public class BackgroundData : SKALDProjectData.CharacterFeature
		{
		}
	}

	// Token: 0x02000222 RID: 546
	[Serializable]
	public class ClassContainers : SKALDProjectData.BaseContainerObject
	{
		// Token: 0x06001878 RID: 6264 RVA: 0x0006BFF5 File Offset: 0x0006A1F5
		public ClassContainers()
		{
			this.archetypeClassContainer = new SKALDProjectData.ClassContainers.ArchetypeClassContainer();
			this.npcClassContainer = new SKALDProjectData.ClassContainers.NPCClassContainer();
			this.classContainer = new SKALDProjectData.ClassContainers.ClassContainer();
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x0006C01E File Offset: 0x0006A21E
		public override List<BaseDataObject> getBaseList()
		{
			List<BaseDataObject> baseList = base.getBaseList();
			baseList.Add(this.npcClassContainer);
			baseList.Add(this.classContainer);
			baseList.Add(this.archetypeClassContainer);
			return baseList;
		}

		// Token: 0x04000866 RID: 2150
		public SKALDProjectData.ClassContainers.NPCClassContainer npcClassContainer;

		// Token: 0x04000867 RID: 2151
		public SKALDProjectData.ClassContainers.ClassContainer classContainer;

		// Token: 0x04000868 RID: 2152
		public SKALDProjectData.ClassContainers.ArchetypeClassContainer archetypeClassContainer;

		// Token: 0x020003BC RID: 956
		[Serializable]
		public class NPCClassContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.ClassContainers.ClassData>
		{
		}

		// Token: 0x020003BD RID: 957
		[Serializable]
		public class ClassContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.ClassContainers.ClassData>
		{
		}

		// Token: 0x020003BE RID: 958
		[Serializable]
		public class ArchetypeClassContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.ClassContainers.ClassData>
		{
		}

		// Token: 0x020003BF RID: 959
		[Serializable]
		public class ClassData : SKALDProjectData.CharacterFeature
		{
			// Token: 0x04000C49 RID: 3145
			public int levelUpHP;

			// Token: 0x04000C4A RID: 3146
			public int levelUpSP;

			// Token: 0x04000C4B RID: 3147
			public string archetype;

			// Token: 0x04000C4C RID: 3148
			public List<string> playerLoadout;
		}
	}

	// Token: 0x02000223 RID: 547
	[Serializable]
	public class RaceContainers : SKALDProjectData.BaseContainerObject
	{
		// Token: 0x0600187A RID: 6266 RVA: 0x0006C04A File Offset: 0x0006A24A
		public RaceContainers()
		{
			this.npcRaceContainer = new SKALDProjectData.RaceContainers.NPCRaceContainer();
			this.raceContainer = new SKALDProjectData.RaceContainers.RaceContainer();
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x0006C068 File Offset: 0x0006A268
		public override List<BaseDataObject> getBaseList()
		{
			List<BaseDataObject> baseList = base.getBaseList();
			baseList.Add(this.npcRaceContainer);
			baseList.Add(this.raceContainer);
			return baseList;
		}

		// Token: 0x04000869 RID: 2153
		public SKALDProjectData.RaceContainers.NPCRaceContainer npcRaceContainer;

		// Token: 0x0400086A RID: 2154
		public SKALDProjectData.RaceContainers.RaceContainer raceContainer;

		// Token: 0x020003C0 RID: 960
		[Serializable]
		public class NPCRaceContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.RaceContainers.RaceData>
		{
		}

		// Token: 0x020003C1 RID: 961
		[Serializable]
		public class RaceContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.RaceContainers.RaceData>
		{
		}

		// Token: 0x020003C2 RID: 962
		[Serializable]
		public class RaceData : SKALDProjectData.CharacterFeature
		{
			// Token: 0x04000C4D RID: 3149
			public int combatMoves;

			// Token: 0x04000C4E RID: 3150
			public int startingWounds;

			// Token: 0x04000C4F RID: 3151
			public string deathSound;

			// Token: 0x04000C50 RID: 3152
			public string attackSound;
		}
	}

	// Token: 0x02000224 RID: 548
	[Serializable]
	public abstract class CharacterFeature : SKALDProjectData.GameDataObject
	{
		// Token: 0x0400086B RID: 2155
		public bool hidden;

		// Token: 0x0400086C RID: 2156
		public bool selectable;

		// Token: 0x0400086D RID: 2157
		public bool deluxeEdition;

		// Token: 0x0400086E RID: 2158
		public int bonusDP;

		// Token: 0x0400086F RID: 2159
		public int strength;

		// Token: 0x04000870 RID: 2160
		public int agility;

		// Token: 0x04000871 RID: 2161
		public int fortitude;

		// Token: 0x04000872 RID: 2162
		public int intellect;

		// Token: 0x04000873 RID: 2163
		public int presence;

		// Token: 0x04000874 RID: 2164
		public string mainAttribute;

		// Token: 0x04000875 RID: 2165
		public List<string> featsList;

		// Token: 0x04000876 RID: 2166
		public List<string> abilityList;

		// Token: 0x04000877 RID: 2167
		public List<string> spellsList;

		// Token: 0x04000878 RID: 2168
		public List<string> loadoutList;

		// Token: 0x04000879 RID: 2169
		public List<string> apperancePackList;

		// Token: 0x0400087A RID: 2170
		public List<string> preferredFeat;

		// Token: 0x0400087B RID: 2171
		public List<string> preferredAbility;

		// Token: 0x0400087C RID: 2172
		public List<string> preferredSpell;

		// Token: 0x0400087D RID: 2173
		public List<string> allowedArmors;

		// Token: 0x0400087E RID: 2174
		public List<string> allowedWeaponTypes;

		// Token: 0x0400087F RID: 2175
		public List<string> allowedWeaponWeights;
	}

	// Token: 0x02000225 RID: 549
	[Serializable]
	public class AnimationContainers : SKALDProjectData.BaseContainerObject
	{
		// Token: 0x0600187D RID: 6269 RVA: 0x0006C090 File Offset: 0x0006A290
		public AnimationContainers()
		{
			this.animations = new SKALDProjectData.AnimationContainers.AnimationContainer();
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x0006C0A3 File Offset: 0x0006A2A3
		public override List<BaseDataObject> getBaseList()
		{
			List<BaseDataObject> baseList = base.getBaseList();
			baseList.Add(this.animations);
			return baseList;
		}

		// Token: 0x04000880 RID: 2176
		public SKALDProjectData.AnimationContainers.AnimationContainer animations;

		// Token: 0x020003C3 RID: 963
		[Serializable]
		public class AnimationContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.AnimationContainers.AnimationContainer.AnimationData>
		{
			// Token: 0x0200042D RID: 1069
			[Serializable]
			public class AnimationData : SKALDProjectData.GameDataObject
			{
				// Token: 0x04000D84 RID: 3460
				public bool randomStartingFrame;

				// Token: 0x04000D85 RID: 3461
				public bool nonLinear;

				// Token: 0x04000D86 RID: 3462
				public bool looping;

				// Token: 0x04000D87 RID: 3463
				public bool randomFrameLength;

				// Token: 0x04000D88 RID: 3464
				public bool overrideModel;

				// Token: 0x04000D89 RID: 3465
				public bool noOverride;

				// Token: 0x04000D8A RID: 3466
				public float FPS;

				// Token: 0x04000D8B RID: 3467
				public string frames;

				// Token: 0x04000D8C RID: 3468
				public string endOfAnimationTrigger;

				// Token: 0x04000D8D RID: 3469
				public string exitTarget;
			}
		}
	}

	// Token: 0x02000226 RID: 550
	[Serializable]
	public class EncylopediaContainer : SKALDProjectData.BaseContainerObject
	{
		// Token: 0x0600187F RID: 6271 RVA: 0x0006C0B7 File Offset: 0x0006A2B7
		public EncylopediaContainer()
		{
			this.tooltipContainer = new SKALDProjectData.EncylopediaContainer.TooltipContainer();
			this.tutorialContainer = new SKALDProjectData.EncylopediaContainer.TutorialContainer();
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x0006C0D5 File Offset: 0x0006A2D5
		public override List<BaseDataObject> getBaseList()
		{
			List<BaseDataObject> baseList = base.getBaseList();
			baseList.Add(this.tooltipContainer);
			baseList.Add(this.tutorialContainer);
			return baseList;
		}

		// Token: 0x04000881 RID: 2177
		public SKALDProjectData.EncylopediaContainer.TooltipContainer tooltipContainer;

		// Token: 0x04000882 RID: 2178
		public SKALDProjectData.EncylopediaContainer.TutorialContainer tutorialContainer;

		// Token: 0x020003C4 RID: 964
		[Serializable]
		public class TutorialContainer : SKALDProjectData.DeepContainerObject<SKALDProjectData.EncylopediaContainer.TutorialContainer.TutorialCategory>
		{
			// Token: 0x06001D42 RID: 7490 RVA: 0x0007BA50 File Offset: 0x00079C50
			public TutorialContainer()
			{
				this.id = "Tutorials";
			}

			// Token: 0x0200042E RID: 1070
			[Serializable]
			public class TutorialCategory : SKALDProjectData.SpecificContainerObject<SKALDProjectData.EncylopediaContainer.TutorialContainer.TutorialCategory.Tutorial>
			{
				// Token: 0x0200043B RID: 1083
				[Serializable]
				public class Tutorial : SKALDProjectData.GameDataObject
				{
					// Token: 0x04000DCE RID: 3534
					public string secondaryDescription;

					// Token: 0x04000DCF RID: 3535
					public string secondaryDescriptionController;

					// Token: 0x04000DD0 RID: 3536
					public string primaryDescriptionController;

					// Token: 0x04000DD1 RID: 3537
					public string nextTutorial;

					// Token: 0x04000DD2 RID: 3538
					public int priority;
				}
			}
		}

		// Token: 0x020003C5 RID: 965
		[Serializable]
		public class TooltipContainer : SKALDProjectData.DeepContainerObject<SKALDProjectData.EncylopediaContainer.TooltipContainer.TooltipCategory>
		{
			// Token: 0x0200042F RID: 1071
			[Serializable]
			public class TooltipCategory : SKALDProjectData.SpecificContainerObject<SKALDProjectData.EncylopediaContainer.Entry>
			{
			}
		}

		// Token: 0x020003C6 RID: 966
		[Serializable]
		public class Entry : SKALDProjectData.GameDataObject
		{
			// Token: 0x04000C51 RID: 3153
			public string keywords;
		}
	}

	// Token: 0x02000227 RID: 551
	[Serializable]
	public class UIContainers : SKALDProjectData.BaseContainerObject
	{
		// Token: 0x06001881 RID: 6273 RVA: 0x0006C0F5 File Offset: 0x0006A2F5
		public UIContainers()
		{
			this.fontData = new SKALDProjectData.UIContainers.FontContainer();
			this.colorData = new SKALDProjectData.UIContainers.ColorContainer();
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x0006C113 File Offset: 0x0006A313
		public override List<BaseDataObject> getBaseList()
		{
			List<BaseDataObject> baseList = base.getBaseList();
			baseList.Add(this.fontData);
			baseList.Add(this.colorData);
			return baseList;
		}

		// Token: 0x04000883 RID: 2179
		public SKALDProjectData.UIContainers.FontContainer fontData;

		// Token: 0x04000884 RID: 2180
		public SKALDProjectData.UIContainers.ColorContainer colorData;

		// Token: 0x020003C7 RID: 967
		[Serializable]
		public class FontContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.UIContainers.FontContainer.Font>
		{
			// Token: 0x02000430 RID: 1072
			[Serializable]
			public class Font : SKALDProjectData.GameDataObject
			{
				// Token: 0x04000D8E RID: 3470
				public int letterSpacing;

				// Token: 0x04000D8F RID: 3471
				public int wordSpacing;

				// Token: 0x04000D90 RID: 3472
				public int wordHeight;
			}
		}

		// Token: 0x020003C8 RID: 968
		[Serializable]
		public class ColorContainer : SKALDProjectData.SpecificContainerObject<SKALDProjectData.UIContainers.ColorContainer.Color>
		{
			// Token: 0x02000431 RID: 1073
			[Serializable]
			public class Color : SKALDProjectData.GameDataObject
			{
				// Token: 0x04000D91 RID: 3473
				public bool skin;

				// Token: 0x04000D92 RID: 3474
				public bool skinNotPC;

				// Token: 0x04000D93 RID: 3475
				public bool hair;

				// Token: 0x04000D94 RID: 3476
				public bool clothing;

				// Token: 0x04000D95 RID: 3477
				public bool font;

				// Token: 0x04000D96 RID: 3478
				public bool fontShadow;

				// Token: 0x04000D97 RID: 3479
				public string hexCode;

				// Token: 0x04000D98 RID: 3480
				public string shade;

				// Token: 0x04000D99 RID: 3481
				public string highlight;
			}
		}
	}

	// Token: 0x02000228 RID: 552
	[Serializable]
	public class MapDataContainer : SKALDProjectData.BaseContainerObject
	{
		// Token: 0x06001883 RID: 6275 RVA: 0x0006C133 File Offset: 0x0006A333
		public MapDataContainer()
		{
			this.mapSaveDataList = new List<MapSaveDataContainer>();
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x0006C146 File Offset: 0x0006A346
		public void addMapData(MapSaveDataContainer mapData)
		{
			this.removeMapData(mapData.id);
			if (this.getMapData(mapData.id) == null)
			{
				this.mapSaveDataList.Add(mapData);
			}
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x0006C170 File Offset: 0x0006A370
		public MapSaveDataContainer getMapData(string id)
		{
			foreach (MapSaveDataContainer mapSaveDataContainer in this.mapSaveDataList)
			{
				if (mapSaveDataContainer.id == id)
				{
					return mapSaveDataContainer;
				}
			}
			return null;
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x0006C1D4 File Offset: 0x0006A3D4
		public void removeMapData(string id)
		{
			MapSaveDataContainer mapData = this.getMapData(id);
			if (mapData != null)
			{
				this.mapSaveDataList.Remove(mapData);
			}
		}

		// Token: 0x04000885 RID: 2181
		public List<MapSaveDataContainer> mapSaveDataList;
	}

	// Token: 0x02000229 RID: 553
	[Serializable]
	public class GameDataObject : BaseDataObject
	{
		// Token: 0x04000886 RID: 2182
		public string title;

		// Token: 0x04000887 RID: 2183
		public string description;

		// Token: 0x04000888 RID: 2184
		public string imagePath;

		// Token: 0x04000889 RID: 2185
		public string modelPath;
	}

	// Token: 0x0200022A RID: 554
	[Serializable]
	public class DeepContainerObject<T> : SKALDProjectData.SpecificContainerObject<T>
	{
		// Token: 0x06001888 RID: 6280 RVA: 0x0006C204 File Offset: 0x0006A404
		public override List<BaseDataObject> getFlatBaseDataObjectList(List<BaseDataObject> resultList)
		{
			resultList.Add(this);
			foreach (T t in this.list)
			{
				(t as SKALDProjectData.BaseContainerObject).getFlatBaseDataObjectList(resultList);
			}
			return resultList;
		}
	}

	// Token: 0x0200022B RID: 555
	[Serializable]
	public class SpecificContainerObject<T> : SKALDProjectData.BaseContainerObject
	{
		// Token: 0x0600188A RID: 6282 RVA: 0x0006C270 File Offset: 0x0006A470
		public SpecificContainerObject()
		{
			this.list = new List<T>();
			this.dictionary = new Dictionary<string, T>();
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x0006C290 File Offset: 0x0006A490
		public override List<BaseDataObject> getFlatBaseDataObjectList(List<BaseDataObject> resultList)
		{
			resultList.Add(this);
			foreach (T t in this.list)
			{
				if (t is SKALDProjectData.BaseContainerObject)
				{
					(t as SKALDProjectData.BaseContainerObject).getFlatBaseDataObjectList(resultList);
				}
				else
				{
					BaseDataObject item = t as BaseDataObject;
					resultList.Add(item);
				}
			}
			return resultList;
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x0006C318 File Offset: 0x0006A518
		public override List<BaseDataObject> getBaseList()
		{
			return base.getBaseList<T>(this.list);
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x0006C328 File Offset: 0x0006A528
		protected BaseDataObject addIntoList(T obj, string id)
		{
			if (this.list.Contains(obj))
			{
				return null;
			}
			BaseDataObject baseDataObject = obj as BaseDataObject;
			baseDataObject.id = id;
			this.addIntoList(baseDataObject);
			this.list.Add(obj);
			return baseDataObject;
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x0006C370 File Offset: 0x0006A570
		protected BaseDataObject addIntoList(T obj)
		{
			BaseDataObject baseDataObject = obj as BaseDataObject;
			if (this.list.Contains(obj))
			{
				return this.getListMember(baseDataObject.id) as BaseDataObject;
			}
			this.addIntoList(baseDataObject);
			this.list.Add(obj);
			return baseDataObject;
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x0006C3C4 File Offset: 0x0006A5C4
		protected BaseDataObject addIntoList(BaseDataObject o)
		{
			if (!this.isIDLegal(o.id))
			{
				int num = 1;
				while (!this.isIDLegal(o.id + "_" + num.ToString()))
				{
					num++;
				}
				o.id = o.id + "_" + num.ToString();
			}
			return o;
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x0006C424 File Offset: 0x0006A624
		private bool isIDLegal(string id)
		{
			using (List<T>.Enumerator enumerator = this.list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if ((enumerator.Current as BaseDataObject).id == id)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x0006C490 File Offset: 0x0006A690
		public T getListMember(string testId)
		{
			testId = testId.ToUpper();
			if (this.dictionary.ContainsKey(testId))
			{
				return this.dictionary[testId];
			}
			foreach (T t in this.list)
			{
				if (t is BaseDataObject && (t as BaseDataObject).testId(testId))
				{
					this.dictionary.Add(testId, t);
					return t;
				}
			}
			return default(T);
		}

		// Token: 0x0400088A RID: 2186
		public List<T> list;

		// Token: 0x0400088B RID: 2187
		private Dictionary<string, T> dictionary;
	}

	// Token: 0x0200022C RID: 556
	[Serializable]
	public class BaseContainerObject : BaseDataObject
	{
		// Token: 0x06001892 RID: 6290 RVA: 0x0006C53C File Offset: 0x0006A73C
		public BaseContainerObject()
		{
			this.baseDataObjectList = new List<BaseDataObject>();
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x0006C550 File Offset: 0x0006A750
		public virtual List<BaseDataObject> getFlatBaseDataObjectList(List<BaseDataObject> resultList)
		{
			resultList.Add(this);
			foreach (BaseDataObject baseDataObject in this.baseDataObjectList)
			{
				if (baseDataObject is SKALDProjectData.BaseContainerObject)
				{
					(baseDataObject as SKALDProjectData.BaseContainerObject).getFlatBaseDataObjectList(resultList);
				}
				else
				{
					resultList.Add(baseDataObject);
				}
			}
			return resultList;
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x0006C5C4 File Offset: 0x0006A7C4
		public virtual List<string> getFlatIdList(List<string> resultList)
		{
			foreach (BaseDataObject baseDataObject in this.baseDataObjectList)
			{
				if (baseDataObject is SKALDProjectData.BaseContainerObject)
				{
					(baseDataObject as SKALDProjectData.BaseContainerObject).getFlatIdList(resultList);
				}
				else
				{
					resultList.Add(baseDataObject.id);
				}
			}
			return resultList;
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x0006C634 File Offset: 0x0006A834
		public virtual List<string> getFlatIdList()
		{
			List<string> resultList = new List<string>();
			return this.getFlatIdList(resultList);
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x0006C64E File Offset: 0x0006A84E
		public virtual List<BaseDataObject> getBaseList()
		{
			this.baseDataObjectList.Clear();
			return this.baseDataObjectList;
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x0006C664 File Offset: 0x0006A864
		protected virtual List<BaseDataObject> getBaseList<T>(List<T> list)
		{
			this.baseDataObjectList.Clear();
			foreach (T t in list)
			{
				try
				{
					BaseDataObject item = t as BaseDataObject;
					this.baseDataObjectList.Add(item);
				}
				catch
				{
				}
			}
			return this.baseDataObjectList;
		}

		// Token: 0x0400088C RID: 2188
		protected List<BaseDataObject> baseDataObjectList;
	}
}
