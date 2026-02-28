using System;
using System.Collections.Generic;

// Token: 0x02000010 RID: 16
public abstract class AbilityUseable : AbilityActive
{
	// Token: 0x060000BF RID: 191 RVA: 0x000058B7 File Offset: 0x00003AB7
	protected AbilityUseable(SKALDProjectData.AbilityContainers.UseableAbility rawData) : base(rawData)
	{
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x000058C0 File Offset: 0x00003AC0
	public SKALDProjectData.AbilityContainers.UseableAbility getRawData()
	{
		return GameData.getAbilityRawData(this.getId()) as SKALDProjectData.AbilityContainers.UseableAbility;
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x000058D4 File Offset: 0x00003AD4
	protected override string printCost()
	{
		SKALDProjectData.AbilityContainers.UseableAbility rawData = this.getRawData();
		if (rawData == null)
		{
			return "";
		}
		string text = "\n";
		text += TextTools.formateNameValuePair("Cost", rawData.useCost);
		if (this.getResourceType() != "")
		{
			text = text + " " + GameData.getAttributeName(this.getResourceType()).Substring(0, 3) + ".";
		}
		return text;
	}

	// Token: 0x060000C2 RID: 194 RVA: 0x00005944 File Offset: 0x00003B44
	protected override string printTimeCost()
	{
		SKALDProjectData.AbilityContainers.UseableAbility rawData = this.getRawData();
		if (rawData == null)
		{
			return "";
		}
		string text = "\n";
		if (rawData.timeCost == "")
		{
			text += TextTools.formateNameValuePair("Time Use", AbilityUseable.TimeCost.FullRound.ToString());
		}
		else
		{
			text += TextTools.formateNameValuePair("Time Use", rawData.timeCost);
		}
		return text;
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x000059B4 File Offset: 0x00003BB4
	public override SkaldActionResult canUserAffordAbility(Character user)
	{
		if (user == null)
		{
			new SkaldActionResult(true, true, "Can afford ability: " + this.getName(), true);
		}
		SKALDProjectData.AbilityContainers.UseableAbility rawData = this.getRawData();
		if (this.getResourceType() != "" || rawData.useCost != 0)
		{
			SkaldActionResult skaldActionResult = this.testUseCostVersusAbility(this.getResourceType(), rawData.useCost, user);
			if (!skaldActionResult.wasSuccess())
			{
				return skaldActionResult;
			}
		}
		if (rawData.requiredItems.Count != 0 || rawData.itemUseCost != 0)
		{
			SkaldActionResult skaldActionResult2 = this.testItemUseCostVersusInventory(rawData.requiredItems, rawData.itemUseCost, user);
			if (!skaldActionResult2.wasSuccess())
			{
				return skaldActionResult2;
			}
		}
		return new SkaldActionResult(true, true, user.getName() + " can afford ability: " + this.getName(), true);
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x00005A70 File Offset: 0x00003C70
	public virtual AbilityUseable.TimeCost getTimeCost()
	{
		SKALDProjectData.AbilityContainers.UseableAbility rawData = this.getRawData();
		if (rawData == null || rawData.timeCost == "")
		{
			return AbilityUseable.TimeCost.FullRound;
		}
		foreach (AbilityUseable.TimeCost result in new List<AbilityUseable.TimeCost>
		{
			AbilityUseable.TimeCost.Free,
			AbilityUseable.TimeCost.FullRound,
			AbilityUseable.TimeCost.MoveEquivalent,
			AbilityUseable.TimeCost.MultiRound
		})
		{
			if (result.ToString() == rawData.timeCost)
			{
				return result;
			}
		}
		MainControl.logError("Malformed timecost: " + rawData.timeCost + " for ability " + this.getId());
		return AbilityUseable.TimeCost.FullRound;
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x00005B38 File Offset: 0x00003D38
	public void payTimeCost(Character user)
	{
		AbilityUseable.TimeCost timeCost = this.getTimeCost();
		if (timeCost == AbilityUseable.TimeCost.Free)
		{
			return;
		}
		if (timeCost == AbilityUseable.TimeCost.MoveEquivalent)
		{
			user.clearCombatMovesButNotAttacksIfIHaveMoves();
			return;
		}
		if (timeCost == AbilityUseable.TimeCost.FullRound)
		{
			user.clearAllCombatMoves();
		}
	}

	// Token: 0x060000C6 RID: 198 RVA: 0x00005B68 File Offset: 0x00003D68
	private SkaldActionResult testUseCostVersusAbility(string ability, int useCost, Character user)
	{
		if (useCost == 0 || ability == "")
		{
			MainControl.logError("Resource type or use cost is not set for ability: " + this.getId());
			return new SkaldActionResult(false, true, user.getName() + " can afford ability: " + this.getName(), true);
		}
		int currentAttributeValue = user.getCurrentAttributeValue(ability);
		if (currentAttributeValue < useCost)
		{
			return new SkaldActionResult(false, false, string.Concat(new string[]
			{
				user.getName(),
				" cannot afford to use: ",
				this.getName().ToUpper(),
				". Requires ",
				useCost.ToString(),
				" ",
				GameData.getAttributeName(ability).ToUpper(),
				" (current is ",
				currentAttributeValue.ToString(),
				")."
			}), true);
		}
		return new SkaldActionResult(true, true, user.getName() + " can afford ability: " + this.getName(), true);
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x00005C5C File Offset: 0x00003E5C
	private SkaldActionResult testItemUseCostVersusInventory(List<string> requiredItems, int itemUseCost, Character user)
	{
		if (requiredItems.Count == 0 || itemUseCost == 0)
		{
			MainControl.logError("Required items or item use cost is not set for ability: " + this.getId());
			return new SkaldActionResult(true, true, user.getName() + " can afford ability: " + this.getName(), true);
		}
		foreach (string text in requiredItems)
		{
			int itemCount = user.getInventory().getItemCount(text);
			if (itemCount < itemUseCost)
			{
				return new SkaldActionResult(false, false, string.Concat(new string[]
				{
					user.getName(),
					" cannot afford: ",
					this.getName().ToUpper(),
					". Requires ",
					itemUseCost.ToString(),
					" ",
					GameData.getItemName(text).ToUpper(),
					" (current is ",
					itemCount.ToString(),
					")."
				}), true);
			}
		}
		return new SkaldActionResult(true, true, user.getName() + " can afford ability: " + this.getName(), true);
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x00005D94 File Offset: 0x00003F94
	public override void payForUse(Character user)
	{
		if (user == null)
		{
			return;
		}
		SKALDProjectData.AbilityContainers.UseableAbility rawData = this.getRawData();
		this.payAbilityCost(this.getResourceType(), rawData.useCost, user);
		this.payItemCost(rawData.requiredItems, rawData.itemUseCost, user);
		this.payTimeCost(user);
	}

	// Token: 0x060000C9 RID: 201 RVA: 0x00005DDC File Offset: 0x00003FDC
	public string printDebugString()
	{
		SKALDProjectData.AbilityContainers.UseableAbility rawData = this.getRawData();
		if (rawData == null)
		{
			return "";
		}
		string text = string.Concat(new string[]
		{
			this.getName(),
			" | Cost: ",
			rawData.useCost.ToString(),
			" | Area: ",
			rawData.effectPattern,
			" | Targets: "
		});
		if (rawData.targetEnemies && rawData.targetAllies)
		{
			text += "Enemies and Allies | ";
		}
		else if (rawData.targetEnemies)
		{
			text += "Enemies | ";
		}
		else if (rawData.targetAllies)
		{
			text += "Allies | ";
		}
		text += "Effects: ";
		foreach (string str in rawData.useEffect)
		{
			text = text + str + ", ";
		}
		text = TextTools.removeTrailingComma(text);
		text += " | ";
		return text;
	}

	// Token: 0x060000CA RID: 202 RVA: 0x00005EF4 File Offset: 0x000040F4
	protected virtual string getResourceType()
	{
		SKALDProjectData.AbilityContainers.UseableAbility rawData = this.getRawData();
		if (rawData == null)
		{
			return "";
		}
		return rawData.resourceType;
	}

	// Token: 0x060000CB RID: 203 RVA: 0x00005F18 File Offset: 0x00004118
	public override bool testAIUseability()
	{
		SKALDProjectData.AbilityContainers.UseableAbility rawData = this.getRawData();
		return rawData != null && rawData.AIUseable && new DicePoolPercentile("AI Legality").getResult() <= rawData.AIUseChance;
	}

	// Token: 0x060000CC RID: 204 RVA: 0x00005F53 File Offset: 0x00004153
	private void payAbilityCost(string ability, int useCost, Character user)
	{
		if (useCost == 0 || ability == "")
		{
			return;
		}
		user.addToAttributeCurrentValue(ability, 0 - useCost);
	}

	// Token: 0x060000CD RID: 205 RVA: 0x00005F70 File Offset: 0x00004170
	private void payItemCost(List<string> requiredItems, int itemUseCost, Character user)
	{
		if (requiredItems.Count == 0 || itemUseCost == 0)
		{
			return;
		}
		foreach (string id in requiredItems)
		{
			for (int i = 0; i < itemUseCost; i++)
			{
				user.getInventory().deleteItem(id);
			}
		}
	}

	// Token: 0x020001B8 RID: 440
	public enum TimeCost
	{
		// Token: 0x0400068B RID: 1675
		Free,
		// Token: 0x0400068C RID: 1676
		MoveEquivalent,
		// Token: 0x0400068D RID: 1677
		FullRound,
		// Token: 0x0400068E RID: 1678
		MultiRound
	}
}
