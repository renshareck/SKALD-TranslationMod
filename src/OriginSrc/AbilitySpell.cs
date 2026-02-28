using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200000E RID: 14
public class AbilitySpell : AbilityUseable
{
	// Token: 0x06000090 RID: 144 RVA: 0x00004EBB File Offset: 0x000030BB
	public AbilitySpell(SKALDProjectData.AbilityContainers.SpellContainer.SpellAbility rawData) : base(rawData)
	{
	}

	// Token: 0x06000091 RID: 145 RVA: 0x00004EC4 File Offset: 0x000030C4
	protected override string printComponentType()
	{
		return "Spell";
	}

	// Token: 0x06000092 RID: 146 RVA: 0x00004ECB File Offset: 0x000030CB
	protected new SKALDProjectData.AbilityContainers.SpellContainer.SpellAbility getRawData()
	{
		return GameData.getSpellRawData(this.getId());
	}

	// Token: 0x06000093 RID: 147 RVA: 0x00004ED8 File Offset: 0x000030D8
	public override AbilityUseable.TimeCost getTimeCost()
	{
		return AbilityUseable.TimeCost.FullRound;
	}

	// Token: 0x06000094 RID: 148 RVA: 0x00004EDC File Offset: 0x000030DC
	public override void payForUse(Character user)
	{
		if (user == null)
		{
			return;
		}
		base.payForUse(user);
		user.incrementCascade();
		if (!MainControl.getDataControl().isCombatActive() || !user.hasCascadesRemaining())
		{
			return;
		}
		if (new SkaldTestRandomVsStatic(user, AttributesControl.CoreAttributes.ATT_SpellAptitude, this.getCascadeDC(), 0).wasSuccess())
		{
			user.restoreOneAttack();
			user.setTacticalHoverText("Cascade");
		}
	}

	// Token: 0x06000095 RID: 149 RVA: 0x00004F38 File Offset: 0x00003138
	protected override List<Color32> gridIconBaseColor()
	{
		foreach (string a in this.getSchoolList())
		{
			if (a == "ATT_SpellListAir")
			{
				return new List<Color32>
				{
					C64Color.Cyan,
					C64Color.White
				};
			}
			if (a == "ATT_SpellListFire")
			{
				return new List<Color32>
				{
					C64Color.Yellow,
					C64Color.RedLight
				};
			}
			if (a == "ATT_SpellListEarth")
			{
				return new List<Color32>
				{
					C64Color.BrownLight,
					C64Color.GrayLight
				};
			}
			if (a == "ATT_SpellListWater")
			{
				return new List<Color32>
				{
					C64Color.Blue,
					C64Color.BlueLight
				};
			}
			if (a == "ATT_SpellListNature")
			{
				return new List<Color32>
				{
					C64Color.Green,
					C64Color.GreenLight
				};
			}
			if (a == "ATT_SpellListBardic")
			{
				return new List<Color32>
				{
					C64Color.Violet,
					C64Color.BlueLight
				};
			}
			if (a == "ATT_SpellListMind")
			{
				return new List<Color32>
				{
					C64Color.GreenLight,
					C64Color.Cyan
				};
			}
			if (a == "ATT_SpellListBody")
			{
				return new List<Color32>
				{
					C64Color.RedLight,
					C64Color.Violet
				};
			}
			if (a == "ATT_SpellListSpirit")
			{
				return new List<Color32>
				{
					C64Color.Yellow,
					C64Color.White
				};
			}
		}
		return new List<Color32>
		{
			C64Color.Green,
			C64Color.GreenLight
		};
	}

	// Token: 0x06000096 RID: 150 RVA: 0x0000514C File Offset: 0x0000334C
	public string getSpelltomeIconPath()
	{
		string str = "BookSpellTomeArcane";
		List<string> schoolList = this.getSchoolList();
		if (schoolList.Count > 0)
		{
			string a = schoolList[0];
			if (a == "ATT_SpellListAir")
			{
				str = "BookSpellTomeArcane";
			}
			else if (a == "ATT_SpellListFire")
			{
				str = "BookSpellTomeArcane";
			}
			else if (a == "ATT_SpellListEarth")
			{
				str = "BookSpellTomeArcane";
			}
			else if (a == "ATT_SpellListWater")
			{
				str = "BookSpellTomeArcane";
			}
			else if (a == "ATT_SpellListNature")
			{
				str = "BookSpellTomeDivine";
			}
			else if (a == "ATT_SpellListMind")
			{
				str = "BookSpellTomeDivine";
			}
			else if (a == "ATT_SpellListBody")
			{
				str = "BookSpellTomeDivine";
			}
			else if (a == "ATT_SpellListSpirit")
			{
				str = "BookSpellTomeDivine";
			}
		}
		return str + Mathf.Clamp(this.getTier(), 1, 4).ToString();
	}

	// Token: 0x06000097 RID: 151 RVA: 0x00005240 File Offset: 0x00003440
	public override SkaldActionResult canUserUseAbility(Character user)
	{
		SkaldActionResult skaldActionResult = this.canUserCastThisSpellTier(user, true);
		if (!skaldActionResult.wasSuccess())
		{
			return skaldActionResult;
		}
		return base.canUserUseAbility(user);
	}

	// Token: 0x06000098 RID: 152 RVA: 0x00005268 File Offset: 0x00003468
	public int getCascadeDC()
	{
		int num = 10 + this.getTier() * 2;
		SKALDProjectData.AbilityContainers.SpellContainer.SpellAbility rawData = this.getRawData();
		if (rawData != null)
		{
			num += rawData.cascadeDCMod;
		}
		return num;
	}

	// Token: 0x06000099 RID: 153 RVA: 0x00005295 File Offset: 0x00003495
	protected override string printTimeCost()
	{
		return "\n" + TextTools.formateNameValuePair("Cascade", this.getCascadeDC());
	}

	// Token: 0x0600009A RID: 154 RVA: 0x000052B4 File Offset: 0x000034B4
	public override string getSoundEffect()
	{
		string soundEffect = base.getSoundEffect();
		if (soundEffect != "")
		{
			return soundEffect;
		}
		SKALDProjectData.AbilityContainers.SpellContainer.SpellAbility rawData = this.getRawData();
		if (rawData != null && rawData.soundEffect != "")
		{
			return rawData.soundEffect;
		}
		if (rawData.creatureSummoned.Count != 0)
		{
			return "SpellStrange";
		}
		if (rawData.targetEnemies && rawData.targetAllies)
		{
			return "SpellGeneric1";
		}
		if (!rawData.isPositiveForTarget)
		{
			return "SpellNegative1";
		}
		return "SpellPositive1";
	}

	// Token: 0x0600009B RID: 155 RVA: 0x00005338 File Offset: 0x00003538
	public override string getUserAnimation()
	{
		string text = base.getUserAnimation();
		if (text == "")
		{
			text = "ANI_SpellCastingComplex";
		}
		return text;
	}

	// Token: 0x0600009C RID: 156 RVA: 0x00005360 File Offset: 0x00003560
	public override List<string> getUserParticleEffect()
	{
		List<string> list = base.getUserParticleEffect();
		if (list == null || list.Count == 0)
		{
			list = new List<string>
			{
				"PositiveFlash"
			};
		}
		return list;
	}

	// Token: 0x0600009D RID: 157 RVA: 0x00005394 File Offset: 0x00003594
	public SkaldActionResult canUserCastThisSpellTier(Character user, bool verboseResult = true)
	{
		if (!user.isPC())
		{
			return new SkaldActionResult(true, true);
		}
		List<string> schoolList = this.getSchoolList();
		if (schoolList.Count == 0)
		{
			MainControl.logError(this.getId() + " does not have a school set.");
			return new SkaldActionResult(true, true);
		}
		int tier = this.getTier();
		foreach (string name in schoolList)
		{
			if (user.getCurrentAttributeValue(name) >= tier)
			{
				return new SkaldActionResult(true, true);
			}
		}
		if (verboseResult)
		{
			List<string> list = new List<string>();
			foreach (string id in schoolList)
			{
				list.Add(GameData.getAttributeName(id));
			}
			string str = TextTools.printListLine(list, "");
			return new SkaldActionResult(true, false, "Requires at least " + this.getTier().ToString() + " rank in one of the following Attribute(s): " + str, true);
		}
		return new SkaldActionResult(false, true);
	}

	// Token: 0x0600009E RID: 158 RVA: 0x000054C8 File Offset: 0x000036C8
	public override bool isCombatActivated()
	{
		return this.getRawData().useInCombat;
	}

	// Token: 0x0600009F RID: 159 RVA: 0x000054D5 File Offset: 0x000036D5
	public override bool isNonCombatActivated()
	{
		return this.getRawData().useOutsideCombat;
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x000054E2 File Offset: 0x000036E2
	public int getTier()
	{
		return this.getRawData().tier;
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x000054EF File Offset: 0x000036EF
	public List<string> getSchoolList()
	{
		return this.getRawData().school;
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x000054FC File Offset: 0x000036FC
	protected override string getResourceType()
	{
		return AttributesControl.CoreAttributes.ATT_Attunement.ToString();
	}
}
