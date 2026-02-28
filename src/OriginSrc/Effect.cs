using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000016 RID: 22
public class Effect : SkaldBaseObject
{
	// Token: 0x0600016D RID: 365 RVA: 0x00008924 File Offset: 0x00006B24
	public Effect(SKALDProjectData.EffectContainers.EffectData rawData) : base(rawData)
	{
	}

	// Token: 0x0600016E RID: 366 RVA: 0x00008930 File Offset: 0x00006B30
	public void fireEffect(Character user, Character target)
	{
		if (MainControl.debugFunctions)
		{
			Debug.Log(string.Concat(new string[]
			{
				"Fired effect ",
				C64Color.WHITE_TAG,
				this.getName(),
				"</color> by ",
				user.getId(),
				" on ",
				target.getId()
			}));
		}
		SKALDProjectData.EffectContainers.EffectData rawData = this.getRawData();
		if (rawData == null)
		{
			return;
		}
		base.processString(rawData.triggerOnTarget, target);
		if (target == null)
		{
			return;
		}
		if (this.testSave(user, target))
		{
			target.addPositiveBark("Saved: " + this.getName());
			return;
		}
		if (rawData.randomizeCondition && rawData.addedConditions.Count != 0)
		{
			target.addConditionToCharacter(rawData.addedConditions[Random.Range(0, rawData.addedConditions.Count)]);
		}
		else
		{
			target.addConditionToCharacterFromList(rawData.addedConditions);
		}
		target.removeCondition(rawData.removedConditions);
		target.removeBaseConditions(rawData.removedBaseConditions);
		if (user != null)
		{
			MapTile mapTile = user.getMapTile();
			if (mapTile != null)
			{
				mapTile.setMapObject(rawData.tileParticleEffects);
			}
			user.getVisualEffects().setCombatEffectFromString(rawData.userParticleEffects, target);
		}
		target.getVisualEffects().setCombatEffectFromString(rawData.targetParticleEffects, null);
		int num = 0;
		if (user != null && rawData.addNumericalBonus && rawData.userAttribute != "")
		{
			num = user.getCurrentAttributeValue(rawData.userAttribute);
		}
		if (rawData.soundEffects != "")
		{
			AudioControl.playSound(rawData.soundEffects);
		}
		if (rawData.maxDamage != 0)
		{
			int num2 = rawData.minDamage + num;
			int num3 = rawData.maxDamage + num;
			if (num3 < num2)
			{
				num3 = num2;
			}
			Damage damageObject = new Damage(new DicePoolVariable("Effect - Damage", num2, num3).getResult(), rawData.damageTypes, target, user);
			target.takeDamage(damageObject, true);
		}
		if (rawData.maxHealing != 0)
		{
			int num4 = rawData.minHealing + num;
			int num5 = rawData.maxHealing + num;
			if (num5 < num4)
			{
				num5 = num4;
			}
			DicePoolVariable dicePoolVariable = new DicePoolVariable("Effect - Healing", num4, num5);
			target.addVitality(dicePoolVariable.getResult());
		}
		if (rawData.maxAttunement != 0)
		{
			int num6 = rawData.minAttunement + num;
			int num7 = rawData.maxAttunement + num;
			if (num7 < num6)
			{
				num7 = num6;
			}
			DicePoolVariable dicePoolVariable2 = new DicePoolVariable("Effect - Attunement", num6, num7);
			target.addAttunement(dicePoolVariable2.getResult());
		}
	}

	// Token: 0x0600016F RID: 367 RVA: 0x00008B84 File Offset: 0x00006D84
	public Effect.EffectDescription getEffectDescription()
	{
		return new Effect.EffectDescription(this.getRawData());
	}

	// Token: 0x06000170 RID: 368 RVA: 0x00008B91 File Offset: 0x00006D91
	public SKALDProjectData.EffectContainers.EffectData getRawData()
	{
		return GameData.getEffectData(this.getId());
	}

	// Token: 0x06000171 RID: 369 RVA: 0x00008BA0 File Offset: 0x00006DA0
	public bool testSave(Character user, Character target)
	{
		SKALDProjectData.EffectContainers.EffectData rawData = this.getRawData();
		if (user == null || target == null)
		{
			return false;
		}
		if (rawData == null)
		{
			return false;
		}
		if (rawData.targetAttribute == "" || rawData.userAttribute == "")
		{
			return false;
		}
		SkaldTestRandomVsRandom skaldTestRandomVsRandom = new SkaldTestRandomVsRandom(target.getCurrentAttributeValue(rawData.targetAttribute), rawData.targetAttribute, user.getCurrentAttributeValue(rawData.userAttribute), rawData.userAttribute, 1);
		if (MainControl.debugFunctions)
		{
			MainControl.log(string.Concat(new string[]
			{
				"Rolled save for Effect ",
				C64Color.WHITE_TAG,
				this.getId(),
				" </color>: ",
				skaldTestRandomVsRandom.getReturnString()
			}));
		}
		return skaldTestRandomVsRandom.wasSuccess();
	}

	// Token: 0x020001BD RID: 445
	public class EffectDescription
	{
		// Token: 0x0600162E RID: 5678 RVA: 0x00063020 File Offset: 0x00061220
		public EffectDescription(SKALDProjectData.EffectContainers.EffectData rawData)
		{
			this.clearAndApplyRawData(rawData);
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x00063088 File Offset: 0x00061288
		public EffectDescription()
		{
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x000630E8 File Offset: 0x000612E8
		private void clearAndApplyRawData(SKALDProjectData.EffectContainers.EffectData rawData)
		{
			if (rawData == null)
			{
				return;
			}
			this.removeAllFirstAidConditions = rawData.removeAllFirstAidConditions;
			this.removeAllMagicalConditions = rawData.removeAllMagicalConditions;
			this.removeAllRestClearsConditions = rawData.removeAllRestClearsConditions;
			this.removeAllPositiveConditions = rawData.removeAllPositiveConditions;
			this.removeAllNegativeConditions = rawData.removeAllNegativeConditions;
			this.removeAllConditions = rawData.removeAllConditions;
			if (rawData.randomizeCondition)
			{
				this.appendList(this.addedConditionsRandom, rawData.addedConditions);
			}
			else
			{
				this.appendList(this.addedConditions, rawData.addedConditions);
			}
			this.appendList(this.removedConditions, rawData.removedConditions);
			this.appendList(this.removedBaseConditions, rawData.removedBaseConditions);
			string text = "";
			if (rawData.addNumericalBonus && rawData.userAttribute != "")
			{
				text = " (+" + GameData.getAttributeName(rawData.userAttribute) + ")";
			}
			if (rawData.maxDamage > 0)
			{
				string text2 = string.Concat(new string[]
				{
					"Deals ",
					rawData.minDamage.ToString(),
					"-",
					rawData.maxDamage.ToString(),
					text,
					" Damage"
				});
				if (rawData.damageTypes.Count != 0)
				{
					text2 = text2 + " [" + TextTools.printListLine(rawData.damageTypes, "") + "]";
				}
				this.damageStrings.Add(text2);
			}
			if (rawData.maxHealing > 0)
			{
				string item = string.Concat(new string[]
				{
					"Heals ",
					rawData.minHealing.ToString(),
					"-",
					rawData.maxHealing.ToString(),
					text,
					" Vitality\n\n"
				});
				this.healingStrings.Add(item);
			}
			if (rawData.maxAttunement > 0)
			{
				string item2 = string.Concat(new string[]
				{
					"Restores ",
					rawData.minAttunement.ToString(),
					"-",
					rawData.maxAttunement.ToString(),
					text,
					" Attunement\n\n"
				});
				this.attunementStrings.Add(item2);
			}
			this.knockback += rawData.knockback;
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x0006331C File Offset: 0x0006151C
		public void mergeInEffectDescription(Effect effect)
		{
			if (effect == null)
			{
				return;
			}
			Effect.EffectDescription effectDescription = effect.getEffectDescription();
			if (effectDescription.removeAllFirstAidConditions)
			{
				this.removeAllFirstAidConditions = effectDescription.removeAllFirstAidConditions;
			}
			if (effectDescription.removeAllMagicalConditions)
			{
				this.removeAllMagicalConditions = effectDescription.removeAllMagicalConditions;
			}
			if (effectDescription.removeAllRestClearsConditions)
			{
				this.removeAllRestClearsConditions = effectDescription.removeAllRestClearsConditions;
			}
			if (effectDescription.removeAllPositiveConditions)
			{
				this.removeAllPositiveConditions = effectDescription.removeAllPositiveConditions;
			}
			if (effectDescription.removeAllNegativeConditions)
			{
				this.removeAllNegativeConditions = effectDescription.removeAllNegativeConditions;
			}
			if (effectDescription.removeAllConditions)
			{
				this.removeAllConditions = effectDescription.removeAllConditions;
			}
			this.appendList(this.addedConditionsRandom, effectDescription.addedConditionsRandom);
			this.appendList(this.addedConditions, effectDescription.addedConditions);
			this.appendList(this.removedConditions, effectDescription.removedConditions);
			this.appendList(this.removedBaseConditions, effectDescription.removedBaseConditions);
			this.appendList(this.damageStrings, effectDescription.damageStrings);
			this.appendList(this.healingStrings, effectDescription.healingStrings);
			this.appendList(this.attunementStrings, effectDescription.attunementStrings);
			this.knockback += effectDescription.knockback;
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x00063440 File Offset: 0x00061640
		public string printDescription()
		{
			string text = "";
			if (this.removeAllFirstAidConditions)
			{
				text += "Removes all First-Aid dependent Conditions.\n\n";
			}
			if (this.removeAllMagicalConditions)
			{
				text += "Removes all magical Conditions.\n\n";
			}
			if (this.removeAllRestClearsConditions)
			{
				text += "Removes all Rest dependent Conditions.\n\n";
			}
			if (this.removeAllPositiveConditions)
			{
				text += "Removes all Positive Conditions.\n\n";
			}
			if (this.removeAllNegativeConditions)
			{
				text += "Removes all Negative Conditions.\n\n";
			}
			if (this.removeAllConditions)
			{
				text += "Removes all Conditions.\n\n";
			}
			if (this.addedConditions.Count != 0)
			{
				if (this.addedConditions.Count == 1)
				{
					text += "Adds Condition: ";
				}
				else
				{
					text += "Adds Conditions: ";
				}
				text = text + this.printConditionList(this.addedConditions) + "\n\n";
			}
			if (this.addedConditionsRandom.Count != 0)
			{
				if (this.addedConditionsRandom.Count == 1)
				{
					text += "Randomly Adds Condition: ";
				}
				else
				{
					text += "Randomly Adds Conditions: ";
				}
				text = text + this.printConditionList(this.addedConditionsRandom) + "\n\n";
			}
			if (this.removedConditions.Count != 0)
			{
				if (this.removedConditions.Count == 1)
				{
					text += "Removes Condition: ";
				}
				else
				{
					text += "Removes Conditions: ";
				}
				text = text + this.printConditionList(this.removedConditions) + "\n\n";
			}
			if (this.removedBaseConditions.Count != 0)
			{
				if (this.removedBaseConditions.Count == 1)
				{
					text += "Removes Conditions with Tag: ";
				}
				else
				{
					text += "Removes Conditions with Tags: ";
				}
				text = text + TextTools.printListLine(this.removedBaseConditions, "#") + "\n\n";
			}
			if (this.damageStrings.Count != 0)
			{
				foreach (string str in this.damageStrings)
				{
					text = text + str + "\n\n";
				}
			}
			if (this.healingStrings.Count != 0)
			{
				foreach (string str2 in this.healingStrings)
				{
					text = text + str2 + "\n\n";
				}
			}
			if (this.attunementStrings.Count != 0)
			{
				foreach (string str3 in this.attunementStrings)
				{
					text = text + str3 + "\n\n";
				}
			}
			if (this.knockback != 0)
			{
				text = text + "Knocks opponent back " + this.knockback.ToString() + " squares.\n\n";
			}
			text = text.TrimEnd(new char[]
			{
				'\n'
			});
			text = text.TrimEnd(new char[]
			{
				'\n'
			});
			text = text.TrimEnd(new char[]
			{
				'\n'
			});
			return text;
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x00063764 File Offset: 0x00061964
		private void appendList(List<string> target, List<string> newList)
		{
			foreach (string item in newList)
			{
				target.Add(item);
			}
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x000637B4 File Offset: 0x000619B4
		private string printConditionList(List<string> conditionIdList)
		{
			List<string> list = new List<string>();
			foreach (string conditionId in conditionIdList)
			{
				list.Add(GameData.getConditionName(conditionId));
			}
			return TextTools.printListLine(list, "");
		}

		// Token: 0x04000693 RID: 1683
		private List<string> addedConditions = new List<string>();

		// Token: 0x04000694 RID: 1684
		private List<string> addedConditionsRandom = new List<string>();

		// Token: 0x04000695 RID: 1685
		private List<string> removedConditions = new List<string>();

		// Token: 0x04000696 RID: 1686
		private List<string> removedBaseConditions = new List<string>();

		// Token: 0x04000697 RID: 1687
		private List<string> damageStrings = new List<string>();

		// Token: 0x04000698 RID: 1688
		private List<string> attunementStrings = new List<string>();

		// Token: 0x04000699 RID: 1689
		private List<string> healingStrings = new List<string>();

		// Token: 0x0400069A RID: 1690
		private bool removeAllFirstAidConditions;

		// Token: 0x0400069B RID: 1691
		private bool removeAllMagicalConditions;

		// Token: 0x0400069C RID: 1692
		private bool removeAllRestClearsConditions;

		// Token: 0x0400069D RID: 1693
		private bool removeAllPositiveConditions;

		// Token: 0x0400069E RID: 1694
		private bool removeAllNegativeConditions;

		// Token: 0x0400069F RID: 1695
		private bool removeAllConditions;

		// Token: 0x040006A0 RID: 1696
		private int knockback;
	}
}
