using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

// Token: 0x02000017 RID: 23
[Serializable]
public class FeatContainer : ISerializable
{
	// Token: 0x06000172 RID: 370 RVA: 0x00008C59 File Offset: 0x00006E59
	public FeatContainer()
	{
	}

	// Token: 0x06000173 RID: 371 RVA: 0x00008C6C File Offset: 0x00006E6C
	public FeatContainer(SerializationInfo info, StreamingContext context)
	{
		foreach (KeyValuePair<string, int> keyValuePair in ((SerializableDictionaryStringInt)info.GetValue("saveData", typeof(SerializableDictionaryStringInt))).getDictionary())
		{
			FeatContainer.Feat feat = this.createFeat(keyValuePair.Key);
			if (feat != null)
			{
				feat.setRank(keyValuePair.Value);
			}
		}
	}

	// Token: 0x06000174 RID: 372 RVA: 0x00008D00 File Offset: 0x00006F00
	public void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		SerializableDictionaryStringInt serializableDictionaryStringInt = new SerializableDictionaryStringInt();
		foreach (FeatContainer.Feat feat in this.featList)
		{
			serializableDictionaryStringInt.addToDictionary(feat.getId(), feat.getRank());
		}
		info.AddValue("saveData", serializableDictionaryStringInt, typeof(SerializableDictionaryStringInt));
	}

	// Token: 0x06000175 RID: 373 RVA: 0x00008D7C File Offset: 0x00006F7C
	internal void autoLevel(int DPGained, Character character)
	{
		foreach (FeatContainer.Feat feat in this.featList)
		{
			if (DPGained <= 0)
			{
				break;
			}
			int openRanks = feat.getOpenRanks();
			if (openRanks > 0)
			{
				if (openRanks >= DPGained)
				{
					feat.addAbsoluteRank(DPGained, character);
					break;
				}
				if (DPGained > openRanks)
				{
					feat.addAbsoluteRank(openRanks, character);
					DPGained -= openRanks;
				}
			}
		}
	}

	// Token: 0x06000176 RID: 374 RVA: 0x00008DF8 File Offset: 0x00006FF8
	public void updateFeatsLegality(Character character)
	{
		foreach (FeatContainer.Feat feat in this.featList)
		{
			feat.clearLegality();
			if (!this.isPrerequisiteFeatUnlocked(feat.getPrerequisitFeat()))
			{
				feat.setIllegalPrereqFeat();
				character.addDevelopmentPoints(feat.subtractPossibleRank());
			}
			else if (character.getLevel() < feat.getPrerequisitLevel())
			{
				feat.setIllegalLevel();
				character.addDevelopmentPoints(feat.subtractPossibleRank());
			}
		}
	}

	// Token: 0x06000177 RID: 375 RVA: 0x00008E8C File Offset: 0x0000708C
	public void addFeat(List<string> idList)
	{
		if (idList == null)
		{
			return;
		}
		foreach (string id in idList)
		{
			this.addFeat(id);
		}
	}

	// Token: 0x06000178 RID: 376 RVA: 0x00008EE0 File Offset: 0x000070E0
	public void addFeat(string id)
	{
		this.createFeat(id);
	}

	// Token: 0x06000179 RID: 377 RVA: 0x00008EEC File Offset: 0x000070EC
	public bool containsFeat(string id)
	{
		using (List<FeatContainer.Feat>.Enumerator enumerator = this.featList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isId(id))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600017A RID: 378 RVA: 0x00008F48 File Offset: 0x00007148
	public bool isPrerequisiteFeatUnlocked(string id)
	{
		if (id == "")
		{
			return true;
		}
		foreach (FeatContainer.Feat feat in this.featList)
		{
			if (feat.isId(id) && feat.getCurrentTierLevel() > 0)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600017B RID: 379 RVA: 0x00008FBC File Offset: 0x000071BC
	private FeatContainer.Feat createFeat(string id)
	{
		if (this.containsFeat(id))
		{
			MainControl.logError("Trying to add duplicate feat: " + id);
			return null;
		}
		SKALDProjectData.FeatContainers.FeatData featRawData = GameData.getFeatRawData(id);
		if (featRawData == null)
		{
			return null;
		}
		FeatContainer.Feat feat = new FeatContainer.Feat(featRawData);
		this.featList.Add(feat);
		return feat;
	}

	// Token: 0x0600017C RID: 380 RVA: 0x00009004 File Offset: 0x00007204
	public List<FeatContainer.Feat> getRootFeatList()
	{
		List<FeatContainer.Feat> list = new List<FeatContainer.Feat>();
		foreach (FeatContainer.Feat feat in this.featList)
		{
			if (feat.isRootFeat())
			{
				list.Add(feat);
			}
		}
		return list;
	}

	// Token: 0x0600017D RID: 381 RVA: 0x00009068 File Offset: 0x00007268
	public List<FeatContainer.Feat> getChildFeats(FeatContainer.Feat parentFeat)
	{
		List<FeatContainer.Feat> list = new List<FeatContainer.Feat>();
		if (parentFeat == null)
		{
			return list;
		}
		foreach (FeatContainer.Feat feat in this.featList)
		{
			if (feat.getPrerequisitFeat() == parentFeat.getId())
			{
				list.Add(feat);
			}
		}
		return list;
	}

	// Token: 0x0600017E RID: 382 RVA: 0x000090DC File Offset: 0x000072DC
	public void addAllRanks(Character character)
	{
		foreach (FeatContainer.Feat feat in this.featList)
		{
			feat.addAllRanks(character);
		}
	}

	// Token: 0x0600017F RID: 383 RVA: 0x00009130 File Offset: 0x00007330
	public void finalizeRanks(Character character)
	{
		foreach (FeatContainer.Feat feat in this.featList)
		{
			feat.finalizeRanks(character);
		}
	}

	// Token: 0x06000180 RID: 384 RVA: 0x00009184 File Offset: 0x00007384
	public int resetRanks()
	{
		int num = 0;
		foreach (FeatContainer.Feat feat in this.featList)
		{
			num += feat.clearPossibleRanks();
		}
		return num;
	}

	// Token: 0x06000181 RID: 385 RVA: 0x000091DC File Offset: 0x000073DC
	public bool hasPossibleRanks()
	{
		using (List<FeatContainer.Feat>.Enumerator enumerator = this.featList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.hasPossibleRanks())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0400002D RID: 45
	private List<FeatContainer.Feat> featList = new List<FeatContainer.Feat>();

	// Token: 0x020001BE RID: 446
	public class Feat : SkaldBaseObject
	{
		// Token: 0x06001635 RID: 5685 RVA: 0x00063818 File Offset: 0x00061A18
		public Feat(SKALDProjectData.FeatContainers.FeatData rawData)
		{
			this.rawData = rawData;
			this.coreData.id = rawData.id;
			this.addFeatAttributes(rawData.list);
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x00063868 File Offset: 0x00061A68
		private void addFeatAttributes(List<SKALDProjectData.FeatContainers.FeatData.FeatTierData> dataList)
		{
			foreach (SKALDProjectData.FeatContainers.FeatData.FeatTierData featTierData in dataList)
			{
				if (featTierData.rank > this.maxRank)
				{
					this.maxRank = featTierData.rank;
				}
				this.featTiers.Add(new FeatContainer.Feat.FeatTier(featTierData));
			}
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x000638DC File Offset: 0x00061ADC
		public int addPossibleRank()
		{
			if (this.isLegal() && this.canMoreRanksBePlaced())
			{
				this.possibleRankAddition++;
				return 1;
			}
			return 0;
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x00063900 File Offset: 0x00061B00
		public void addAbsoluteRank(int ranks, Character character)
		{
			for (int i = 0; i < ranks; i++)
			{
				this.rank++;
				this.applyAttributeIfValid(this.rank, character, true);
			}
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x00063935 File Offset: 0x00061B35
		public int subtractPossibleRank()
		{
			if (this.possibleRankAddition == 0)
			{
				return 0;
			}
			this.possibleRankAddition--;
			return 1;
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x00063950 File Offset: 0x00061B50
		public bool isRootFeat()
		{
			return this.getPrerequisitFeat() == "";
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x00063962 File Offset: 0x00061B62
		public string getPrerequisitFeat()
		{
			return this.rawData.prereqFeat;
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x0006396F File Offset: 0x00061B6F
		public int getRank()
		{
			return this.rank + this.possibleRankAddition;
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x0006397E File Offset: 0x00061B7E
		public bool hasPossibleRanks()
		{
			return this.possibleRankAddition > 0;
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x0006398C File Offset: 0x00061B8C
		public void finalizeRanks(Character character)
		{
			if (this.possibleRankAddition < 1)
			{
				return;
			}
			for (int i = 0; i < this.possibleRankAddition; i++)
			{
				this.rank++;
				this.applyAttributeIfValid(this.rank, character, false);
			}
			this.clearPossibleRanks();
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x000639D8 File Offset: 0x00061BD8
		private void applyAttributeIfValid(int newRank, Character character, bool addDefaultSpell)
		{
			foreach (FeatContainer.Feat.FeatTier featTier in this.featTiers)
			{
				if (featTier.getRankCost() == newRank)
				{
					featTier.apply(character, addDefaultSpell);
				}
			}
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x00063A38 File Offset: 0x00061C38
		public int clearPossibleRanks()
		{
			int result = this.possibleRankAddition;
			this.possibleRankAddition = 0;
			return result;
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x00063A47 File Offset: 0x00061C47
		public override string getName()
		{
			return this.rawData.title;
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x00063A54 File Offset: 0x00061C54
		public override string getDescription()
		{
			string text = base.processString(this.rawData.description, this);
			if (text != "")
			{
				text += "\n\n";
			}
			text += TextTools.formateNameValuePair("Ranks:", this.getRank().ToString() + "/" + this.getMaxRankLevel().ToString());
			if (this.featTiers.Count > 3)
			{
				text += "\n\nThis is a Feat for a Non-Player Character.";
			}
			else
			{
				foreach (FeatContainer.Feat.FeatTier featTier in this.featTiers)
				{
					string text2 = featTier.getDescription();
					if (this.getRank() >= featTier.getRankCost())
					{
						text2 = C64Color.GREEN_LIGHT_TAG + text2 + "</color>";
					}
					text = text + "\n\n" + text2;
				}
			}
			return text;
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x00063B58 File Offset: 0x00061D58
		public void addAllRanks(Character character)
		{
			this.rank = this.getMaxRankLevel();
			this.applyAttributeIfValid(this.rank, character, true);
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x00063B74 File Offset: 0x00061D74
		private int getMaxRankLevel()
		{
			return this.maxRank;
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x00063B7C File Offset: 0x00061D7C
		public bool canMoreRanksBePlaced()
		{
			return this.getRank() < this.getMaxRankLevel();
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x00063B8C File Offset: 0x00061D8C
		public void setRank(int rank)
		{
			this.rank = rank;
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x00063B95 File Offset: 0x00061D95
		public int getOpenRanks()
		{
			return this.getMaxRankLevel() - this.getRank();
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x00063BA4 File Offset: 0x00061DA4
		public int[] getFeatTierLevels()
		{
			int[] array = new int[this.featTiers.Count];
			int num = 0;
			for (int i = 0; i < this.featTiers.Count; i++)
			{
				array[i] = this.featTiers[i].getRankCost() - num;
				num += array[i];
			}
			return array;
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x00063BF8 File Offset: 0x00061DF8
		public int getCurrentTierLevel()
		{
			int num = 0;
			foreach (FeatContainer.Feat.FeatTier featTier in this.featTiers)
			{
				if (this.getRank() >= featTier.getRankCost())
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x00063C5C File Offset: 0x00061E5C
		public override string getImagePath()
		{
			string str = "Images/GUIIcons/FeatIcons/";
			if (this.getFeatType() == "Combat")
			{
				return str + "FeatCombat";
			}
			if (this.getFeatType() == "Magic")
			{
				return str + "FeatMagic";
			}
			if (this.getFeatType() == "ClassGeneral")
			{
				return str + "FeatClassGeneral";
			}
			if (this.getFeatType() == "ClassSpecific")
			{
				return str + "FeatClassSpecific";
			}
			if (this.getFeatType() == "Misc")
			{
				return str + "FeatMisc";
			}
			if (this.getFeatType() == "Race")
			{
				return str + "FeatMisc";
			}
			if (this.getFeatType() == "Background")
			{
				return str + "FeatMisc";
			}
			return str + "FeatClassGeneral";
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x00063D4C File Offset: 0x00061F4C
		public override TextureTools.TextureData getGridIcon()
		{
			if (this.icon != null)
			{
				return this.icon;
			}
			TextureTools.TextureData result;
			try
			{
				this.icon = TextureTools.loadTextureData(this.getImagePath());
				StringPrinter.buildGridIcon(this.icon, 4, this.getName(), this.gridIconBaseColor()[0], this.gridIconBaseColor()[1]);
				int featTier = this.getFeatTier();
				if (featTier > 0 && featTier <= 4)
				{
					TextureTools.loadTextureDataAndApplyOverlay("Images/GUIIcons/FeatTree/TierPip" + featTier.ToString(), 2, this.icon.width - 10, 0, this.icon);
				}
				result = this.icon;
			}
			catch (Exception message)
			{
				Debug.LogError(message);
				result = new TextureTools.TextureData(20, 20);
			}
			return result;
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x00063E0C File Offset: 0x0006200C
		private List<Color32> gridIconBaseColor()
		{
			if (this.getFeatType() == "ClassSpecific")
			{
				return new List<Color32>
				{
					C64Color.RedLight,
					C64Color.GrayLight
				};
			}
			if (this.getFeatType() == "ClassGeneral")
			{
				return new List<Color32>
				{
					C64Color.GrayLight,
					C64Color.White
				};
			}
			if (this.getFeatType() == "Magic")
			{
				return new List<Color32>
				{
					C64Color.Cyan,
					C64Color.White
				};
			}
			if (this.getFeatType() == "Combat")
			{
				return new List<Color32>
				{
					C64Color.GrayLight,
					C64Color.White
				};
			}
			if (this.getFeatType() == "Misc")
			{
				return new List<Color32>
				{
					C64Color.GrayLight,
					C64Color.White
				};
			}
			return new List<Color32>
			{
				C64Color.GrayLight,
				C64Color.White
			};
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x00063F1A File Offset: 0x0006211A
		private string getFeatType()
		{
			return this.rawData.type;
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x00063F27 File Offset: 0x00062127
		private int getFeatTier()
		{
			return this.rawData.tier;
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x00063F34 File Offset: 0x00062134
		internal void clearLegality()
		{
			this.legalLevel = true;
			this.legalPrereqFeat = true;
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x00063F44 File Offset: 0x00062144
		public void setIllegalLevel()
		{
			this.legalLevel = false;
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x00063F4D File Offset: 0x0006214D
		public void setIllegalPrereqFeat()
		{
			this.legalPrereqFeat = false;
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x00063F56 File Offset: 0x00062156
		internal bool isLegal()
		{
			return this.legalLevel && this.legalPrereqFeat;
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x00063F68 File Offset: 0x00062168
		internal int getPrerequisitLevel()
		{
			return this.rawData.prereqLevel;
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x00063F78 File Offset: 0x00062178
		internal string getLegalityMessage()
		{
			if (!this.canMoreRanksBePlaced())
			{
				return "Cannot place Rank: The maximum number of ranks have been placed for this Feat!";
			}
			string text = "This Feat cannot be bought yet:";
			if (!this.legalLevel)
			{
				text = text + "\n\n-Character must be at least level " + this.getPrerequisitLevel().ToString();
			}
			if (!this.legalPrereqFeat)
			{
				text = text + "\n\n-Character must be at least tier 1 in the feat " + GameData.getFeatName(this.getPrerequisitFeat()).ToUpper();
			}
			return text;
		}

		// Token: 0x040006A1 RID: 1697
		private int rank;

		// Token: 0x040006A2 RID: 1698
		private int possibleRankAddition;

		// Token: 0x040006A3 RID: 1699
		private int maxRank;

		// Token: 0x040006A4 RID: 1700
		private bool legalLevel = true;

		// Token: 0x040006A5 RID: 1701
		private bool legalPrereqFeat = true;

		// Token: 0x040006A6 RID: 1702
		private SKALDProjectData.FeatContainers.FeatData rawData;

		// Token: 0x040006A7 RID: 1703
		private List<FeatContainer.Feat.FeatTier> featTiers = new List<FeatContainer.Feat.FeatTier>();

		// Token: 0x040006A8 RID: 1704
		private TextureTools.TextureData icon;

		// Token: 0x020002F7 RID: 759
		private class FeatTier : SkaldBaseObject
		{
			// Token: 0x06001C10 RID: 7184 RVA: 0x00079748 File Offset: 0x00077948
			public FeatTier(SKALDProjectData.FeatContainers.FeatData.FeatTierData rawData)
			{
				this.rawData = rawData;
				foreach (string id in rawData.abilities)
				{
					Ability ability = GameData.getAbility(id);
					if (ability != null)
					{
						this.abilitites.Add(ability);
					}
				}
				foreach (string id2 in rawData.spells)
				{
					AbilitySpell spell = GameData.getSpell(id2);
					if (spell != null)
					{
						this.defaultSpells.Add(spell);
					}
				}
				this.spellSchool = rawData.spellSchool;
				this.spellsAdded = rawData.spellsAdded;
			}

			// Token: 0x06001C11 RID: 7185 RVA: 0x00079840 File Offset: 0x00077A40
			public int getRankCost()
			{
				return this.rawData.rank;
			}

			// Token: 0x06001C12 RID: 7186 RVA: 0x00079850 File Offset: 0x00077A50
			public override string getDescription()
			{
				string text = this.getRankCost().ToString() + " RANKS\n";
				foreach (Ability ability in this.abilitites)
				{
					text = text + "~ " + ability.getName() + "\n";
				}
				if (this.spellSchool != "")
				{
					if (this.spellsAdded == 1)
					{
						text = text + "~ Learn 1 new spell from the " + GameData.getAttributeName(this.spellSchool) + ":";
					}
					else if (this.spellsAdded > 1)
					{
						text = string.Concat(new string[]
						{
							text,
							"~ Learn ",
							this.spellsAdded.ToString(),
							" new spells from the ",
							GameData.getAttributeName(this.spellSchool),
							":"
						});
					}
					text = text + "\n\n" + MainControl.getDataControl().printSpellsByTier(this.spellSchool);
				}
				return text;
			}

			// Token: 0x06001C13 RID: 7187 RVA: 0x00079974 File Offset: 0x00077B74
			public void apply(Character character, bool addDefaultSpell)
			{
				character.addAbilities(this.abilitites);
				if (addDefaultSpell)
				{
					character.addSpell(this.defaultSpells);
					return;
				}
				if (this.spellSchool != "" && this.spellsAdded != 0)
				{
					if (character.isPC())
					{
						NewSpellsToLearnControl.addNewSpellsToLearn(this.spellSchool, character.getCurrentAttributeValue(this.spellSchool), this.spellsAdded, character);
						return;
					}
					MainControl.logError("Trying to add spell learning pop-up for non-PC character.");
					character.addSpell(this.defaultSpells);
				}
			}

			// Token: 0x04000A85 RID: 2693
			private SKALDProjectData.FeatContainers.FeatData.FeatTierData rawData;

			// Token: 0x04000A86 RID: 2694
			private List<Ability> abilitites = new List<Ability>();

			// Token: 0x04000A87 RID: 2695
			private List<AbilitySpell> defaultSpells = new List<AbilitySpell>();

			// Token: 0x04000A88 RID: 2696
			private string spellSchool = "";

			// Token: 0x04000A89 RID: 2697
			private int spellsAdded;
		}
	}
}
