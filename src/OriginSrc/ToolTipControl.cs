using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000B3 RID: 179
public static class ToolTipControl
{
	// Token: 0x06000AFA RID: 2810 RVA: 0x00034960 File Offset: 0x00032B60
	public static ToolTipControl.ToolTipCategory getLoreToolTips()
	{
		if (ToolTipControl.loreTooltips == null)
		{
			ToolTipControl.loreTooltips = new ToolTipControl.ToolTipsLore();
		}
		return ToolTipControl.loreTooltips;
	}

	// Token: 0x06000AFB RID: 2811 RVA: 0x00034978 File Offset: 0x00032B78
	public static ToolTipControl.ToolTipCategory getRulesToolTips()
	{
		if (ToolTipControl.rulesTooltips == null)
		{
			ToolTipControl.rulesTooltips = new ToolTipControl.ToolTipsRules();
		}
		return ToolTipControl.rulesTooltips;
	}

	// Token: 0x040002F3 RID: 755
	private static ToolTipControl.ToolTipCategory loreTooltips;

	// Token: 0x040002F4 RID: 756
	private static ToolTipControl.ToolTipCategory rulesTooltips;

	// Token: 0x02000239 RID: 569
	public class ToolTipsRules : ToolTipControl.ToolTipCategory
	{
		// Token: 0x060018F4 RID: 6388 RVA: 0x0006D134 File Offset: 0x0006B334
		public ToolTipsRules() : base(GameData.getTooltipsByCategories("Rules"))
		{
			foreach (string id in GameData.getAbilityRawDataIdList())
			{
				Ability ability = GameData.getAbility(id);
				if (ability == null)
				{
					ability = GameData.getSpell(id);
				}
				if (ability != null)
				{
					base.addToolTip(new ToolTipControl.ToolTipCategory.ToolTip(ability.getId(), ability.getName(), ability.getFullDescription()));
				}
			}
			foreach (string id2 in GameData.getConditionDataIdList())
			{
				Condition condition = GameData.getCondition(id2);
				if (condition != null)
				{
					base.addToolTip(new ToolTipControl.ToolTipCategory.ToolTip(condition.getId(), condition.getName(), condition.getFullDescription()));
				}
			}
			foreach (string id3 in GameData.getAttributeDataIdList())
			{
				SKALDProjectData.Objects.AttributeData.AttributeContainer.Attribute attributeRawData = GameData.getAttributeRawData(id3);
				if (attributeRawData != null)
				{
					string text = "Attribute";
					if (attributeRawData.attributeType != "")
					{
						text = text + " - " + attributeRawData.attributeType;
					}
					base.addToolTip(new ToolTipControl.ToolTipCategory.ToolTip(attributeRawData.id, attributeRawData.title, attributeRawData.description, text));
				}
			}
			foreach (string id4 in GameData.getFoodIdForTooltips())
			{
				ItemFood itemFood = GameData.instantiateItem(id4) as ItemFood;
				if (itemFood != null)
				{
					base.addToolTip(new ToolTipControl.ToolTipCategory.ToolTipNoHeader(itemFood.getId(), itemFood.getName(), itemFood.printComparativeStats(null)));
					itemFood.setToBeRemoved();
				}
			}
			foreach (string id5 in GameData.getConsumableIdsForTooltips())
			{
				ItemConsumable itemConsumable = GameData.instantiateItem(id5) as ItemConsumable;
				if (itemConsumable != null)
				{
					base.addToolTip(new ToolTipControl.ToolTipCategory.ToolTipNoHeader(itemConsumable.getId(), itemConsumable.getName(), itemConsumable.printComparativeStats(null)));
					itemConsumable.setToBeRemoved();
				}
			}
			foreach (SkaldBaseObject skaldBaseObject in GameData.getClassList().getObjectList())
			{
				if (skaldBaseObject != null)
				{
					base.addToolTip(new ToolTipControl.ToolTipCategory.ToolTip(skaldBaseObject.getId(), skaldBaseObject.getName(), skaldBaseObject.getFullDescription()));
				}
			}
			foreach (SkaldBaseObject skaldBaseObject2 in GameData.getBackgroundList().getObjectList())
			{
				if (skaldBaseObject2 != null)
				{
					base.addToolTip(new ToolTipControl.ToolTipCategory.ToolTip(skaldBaseObject2.getId(), skaldBaseObject2.getName(), skaldBaseObject2.getFullDescription(), "Background"));
				}
			}
			foreach (SkaldBaseObject skaldBaseObject3 in GameData.getClassArchetypeList().getObjectList())
			{
				if (skaldBaseObject3 != null)
				{
					base.addToolTip(new ToolTipControl.ToolTipCategory.ToolTip(skaldBaseObject3.getId(), skaldBaseObject3.getName(), skaldBaseObject3.getFullDescription(), "Archetype"));
				}
			}
		}
	}

	// Token: 0x0200023A RID: 570
	public class ToolTipsLore : ToolTipControl.ToolTipCategory
	{
		// Token: 0x060018F5 RID: 6389 RVA: 0x0006D4D0 File Offset: 0x0006B6D0
		public ToolTipsLore() : base(GameData.getTooltipsByCategories("Lore"))
		{
		}
	}

	// Token: 0x0200023B RID: 571
	public abstract class ToolTipCategory
	{
		// Token: 0x060018F6 RID: 6390 RVA: 0x0006D4E2 File Offset: 0x0006B6E2
		protected ToolTipCategory(List<SKALDProjectData.EncylopediaContainer.Entry> rawTooltips)
		{
			this.toolTips = new Dictionary<string, ToolTipControl.ToolTipCategory.ToolTip>();
			this.keywords = new List<string>();
			this.addToolTips(rawTooltips);
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x0006D508 File Offset: 0x0006B708
		protected void addToolTips(List<SKALDProjectData.EncylopediaContainer.Entry> rawTooltips)
		{
			if (rawTooltips == null || rawTooltips.Count == 0)
			{
				return;
			}
			foreach (SKALDProjectData.EncylopediaContainer.Entry data in rawTooltips)
			{
				this.addToolTip(new ToolTipControl.ToolTipCategory.ToolTip(data));
			}
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x0006D568 File Offset: 0x0006B768
		protected void addToolTip(ToolTipControl.ToolTipCategory.ToolTip tip)
		{
			foreach (string text in tip.getKeywords())
			{
				if (!this.toolTips.ContainsKey(text))
				{
					this.toolTips.Add(text, tip);
					this.insertIntoKeywordList(text);
				}
				else
				{
					Debug.LogError("Duplicate Keyword: " + text + " from object " + tip.getId());
				}
			}
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x0006D5F4 File Offset: 0x0006B7F4
		private void insertIntoKeywordList(string keyword)
		{
			if (this.keywords.Count == 0)
			{
				this.keywords.Add(keyword);
			}
			for (int i = 0; i < this.keywords.Count; i++)
			{
				if (keyword.Length > this.keywords[i].Length)
				{
					this.keywords.Insert(i, keyword);
					return;
				}
			}
			this.keywords.Add(keyword);
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x0006D663 File Offset: 0x0006B863
		public ToolTipControl.ToolTipCategory.ToolTip getToolTip(string keyword)
		{
			if (this.toolTips.ContainsKey(keyword))
			{
				return this.toolTips[keyword];
			}
			MainControl.logError("No tooltip for id: " + keyword);
			return null;
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x0006D691 File Offset: 0x0006B891
		public List<string> getKeywords()
		{
			return this.keywords;
		}

		// Token: 0x040008AE RID: 2222
		private Dictionary<string, ToolTipControl.ToolTipCategory.ToolTip> toolTips;

		// Token: 0x040008AF RID: 2223
		private List<string> keywords;

		// Token: 0x020003CD RID: 973
		public class ToolTipNoHeader : ToolTipControl.ToolTipCategory.ToolTip
		{
			// Token: 0x06001D51 RID: 7505 RVA: 0x0007BB84 File Offset: 0x00079D84
			public ToolTipNoHeader(string id, string name, string description) : base(id, name, description)
			{
			}

			// Token: 0x06001D52 RID: 7506 RVA: 0x0007BB8F File Offset: 0x00079D8F
			public override string getFullDescription()
			{
				return this.getDescription();
			}
		}

		// Token: 0x020003CE RID: 974
		public class ToolTip : SkaldBaseObject
		{
			// Token: 0x06001D53 RID: 7507 RVA: 0x0007BB98 File Offset: 0x00079D98
			public ToolTip(SKALDProjectData.EncylopediaContainer.Entry data) : base(data)
			{
				this.setDescription(C64Color.GRAY_LIGHT_TAG + "[Concept]</color>\n\n" + this.getDescription());
				if (data.keywords != null && data.keywords != "")
				{
					using (List<string>.Enumerator enumerator = TextTools.splitStringIntoList(data.keywords, ',').GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string keyword = enumerator.Current;
							this.addKeyword(keyword);
						}
						return;
					}
				}
				this.addKeyword(this.getName());
			}

			// Token: 0x06001D54 RID: 7508 RVA: 0x0007BC48 File Offset: 0x00079E48
			public ToolTip(string id, string name, string description) : base(id, name, description)
			{
				if (string.IsNullOrEmpty(this.getName()))
				{
					Debug.LogError("Missing name for " + id);
					this.setName(id);
				}
				if (string.IsNullOrEmpty(this.getDescription()))
				{
					Debug.LogError("Missing desc for " + id);
					this.setDescription("");
				}
				this.addKeyword(this.getName());
			}

			// Token: 0x06001D55 RID: 7509 RVA: 0x0007BCC3 File Offset: 0x00079EC3
			public ToolTip(string id, string name, string description, string tag) : this(id, name, string.Concat(new string[]
			{
				C64Color.GRAY_LIGHT_TAG,
				"[",
				tag,
				"]</color>\n\n",
				description
			}))
			{
			}

			// Token: 0x06001D56 RID: 7510 RVA: 0x0007BCF9 File Offset: 0x00079EF9
			public List<string> getKeywords()
			{
				return this.keywords;
			}

			// Token: 0x06001D57 RID: 7511 RVA: 0x0007BD01 File Offset: 0x00079F01
			public override string getFullDescription()
			{
				return string.Concat(new string[]
				{
					C64Color.HEADER_TAG,
					this.getName().ToUpper(),
					C64Color.HEADER_CLOSING_TAG,
					"\n",
					this.getDescription()
				});
			}

			// Token: 0x06001D58 RID: 7512 RVA: 0x0007BD40 File Offset: 0x00079F40
			private void addKeyword(string keyword)
			{
				this.keywords.Add(keyword);
				keyword = keyword.ToLower();
				if (!this.keywords.Contains(keyword))
				{
					this.keywords.Add(keyword);
				}
				if (char.IsLetter(keyword[0]))
				{
					keyword = TextTools.upperCaseEachWord(keyword);
					if (!this.keywords.Contains(keyword))
					{
						this.keywords.Add(keyword);
					}
				}
				keyword = keyword.ToUpper();
				if (!this.keywords.Contains(keyword))
				{
					this.keywords.Add(keyword);
				}
			}

			// Token: 0x04000C56 RID: 3158
			private List<string> keywords = new List<string>();
		}
	}
}
