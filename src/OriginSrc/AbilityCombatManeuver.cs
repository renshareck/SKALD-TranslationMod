using System;
using System.Collections.Generic;

// Token: 0x02000008 RID: 8
public class AbilityCombatManeuver : AbilityUseable
{
	// Token: 0x06000058 RID: 88 RVA: 0x00003F82 File Offset: 0x00002182
	public AbilityCombatManeuver(SKALDProjectData.AbilityContainers.CombatManeuverContainer.CombatManueverAbility rawData) : base(rawData)
	{
	}

	// Token: 0x06000059 RID: 89 RVA: 0x00003F8B File Offset: 0x0000218B
	public override bool isCombatActivated()
	{
		return true;
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00003F8E File Offset: 0x0000218E
	protected override string printComponentType()
	{
		return "Maneuver";
	}

	// Token: 0x0600005B RID: 91 RVA: 0x00003F98 File Offset: 0x00002198
	protected override string printEffectsHeader(SKALDProjectData.AbilityContainers.ActiveAbility rawData)
	{
		SKALDProjectData.AbilityContainers.CombatManeuverContainer.CombatManueverAbility combatManueverAbility = rawData as SKALDProjectData.AbilityContainers.CombatManeuverContainer.CombatManueverAbility;
		if (!combatManueverAbility.attackBased)
		{
			return base.printEffectsHeader(rawData);
		}
		string text = "Executes an Attack ";
		List<string> list = new List<string>();
		if (combatManueverAbility.toHitBonus != 0)
		{
			list.Add("with " + TextTools.formatePlusMinus(combatManueverAbility.toHitBonus) + " Accuracy");
		}
		if (combatManueverAbility.damageBonus != 0)
		{
			list.Add("with " + TextTools.formatePlusMinus(combatManueverAbility.damageBonus) + " bonus to Damage");
		}
		if (combatManueverAbility.armorPiercing)
		{
			list.Add("that is Armor Piercing");
		}
		if (combatManueverAbility.autoCrit)
		{
			list.Add("that automatically scores a Critical Hit if successful");
		}
		text += TextTools.printListLineWithAnd(list);
		text = TextTools.removeTrailingWhiteSpace(text);
		text += ".";
		if (rawData.successEffect.Count == 0)
		{
			return text;
		}
		if (base.getEffectPattern() == "Melee")
		{
			text += " If the Attack is successful the following effects target the opponent:";
		}
		else
		{
			text += " If the Attack is successful the following effects trigger and target ";
			if (base.getEffectPattern() == "Self")
			{
				text += "the character themself.";
			}
			else if (base.getEffectPattern() == "Point")
			{
				if (rawData.targetAllies && rawData.targetEnemies)
				{
					text += "an opponent or ally";
				}
				else if (rawData.targetAllies)
				{
					text += "an ally";
				}
				else if (rawData.targetEnemies)
				{
					text += "an opponent";
				}
				text += " anywhere on the battlefield.";
			}
			else if (base.getEffectPattern() == "Touch")
			{
				if (rawData.targetAllies && rawData.targetEnemies)
				{
					text += "an opponent or ally";
				}
				else if (rawData.targetAllies)
				{
					text += "an ally";
				}
				else if (rawData.targetEnemies)
				{
					text += "an opponent";
				}
				text += " adjacent to the caster.";
			}
			else if (base.getEffectPattern() == "All")
			{
				text += " all opponents and allies on the battlefield.";
			}
			else if (base.getEffectPattern() == "AllEnemies")
			{
				text += " all opponents on the battlefield.";
			}
			else if (base.getEffectPattern() == "AllAllies")
			{
				text += " all allies on the battlefield.";
			}
			else
			{
				if (rawData.targetAllies && rawData.targetEnemies)
				{
					text += "all opponents and allies";
				}
				else if (rawData.targetAllies)
				{
					text += "all allies";
				}
				else if (rawData.targetEnemies)
				{
					text += "all opponents";
				}
				text = text + " in the " + base.getEffectPattern() + " target area.";
			}
		}
		return text + "\n\n" + base.printSuccessEffects(rawData);
	}

	// Token: 0x0600005C RID: 92 RVA: 0x00004269 File Offset: 0x00002469
	public new SKALDProjectData.AbilityContainers.CombatManeuverContainer.CombatManueverAbility getRawData()
	{
		return GameData.getAbilityRawData(this.getId()) as SKALDProjectData.AbilityContainers.CombatManeuverContainer.CombatManueverAbility;
	}

	// Token: 0x0600005D RID: 93 RVA: 0x0000427C File Offset: 0x0000247C
	protected override string printCost()
	{
		SKALDProjectData.AbilityContainers.CombatManeuverContainer.CombatManueverAbility rawData = this.getRawData();
		if (rawData == null)
		{
			return "";
		}
		return "\n" + TextTools.formateNameValuePair("Cooldown", rawData.coolDownCost.ToString() + " turns");
	}

	// Token: 0x0600005E RID: 94 RVA: 0x000042C4 File Offset: 0x000024C4
	public override SkaldActionResult canUserAffordAbility(Character user)
	{
		int cooldown = user.getCooldown();
		if (cooldown > 0)
		{
			return new SkaldActionResult(false, false, user.getName() + " cannot use combat maneuvers again for " + cooldown.ToString() + " turns due to cooldown!", true);
		}
		return base.canUserAffordAbility(user);
	}

	// Token: 0x0600005F RID: 95 RVA: 0x00004308 File Offset: 0x00002508
	public override SkaldActionResult canUserUseAbility(Character user)
	{
		if (this.isAttackBased() && !user.hasValidTarget())
		{
			return new SkaldActionResult(false, false, string.Concat(new string[]
			{
				"Can't use ",
				this.getName().ToUpper(),
				". ",
				user.getName(),
				" must be within reach of a valid target."
			}), true);
		}
		return base.canUserUseAbility(user);
	}

	// Token: 0x06000060 RID: 96 RVA: 0x00004370 File Offset: 0x00002570
	public override void payForUse(Character user)
	{
		base.payForUse(user);
		SKALDProjectData.AbilityContainers.CombatManeuverContainer.CombatManueverAbility rawData = this.getRawData();
		if (rawData == null)
		{
			return;
		}
		user.setCooldown(rawData.coolDownCost);
	}

	// Token: 0x06000061 RID: 97 RVA: 0x0000439C File Offset: 0x0000259C
	public override bool isAttackBased()
	{
		SKALDProjectData.AbilityContainers.CombatManeuverContainer.CombatManueverAbility rawData = this.getRawData();
		return rawData != null && rawData.attackBased;
	}

	// Token: 0x06000062 RID: 98 RVA: 0x000043BC File Offset: 0x000025BC
	public bool addManeuverBonus()
	{
		SKALDProjectData.AbilityContainers.CombatManeuverContainer.CombatManueverAbility rawData = this.getRawData();
		return rawData != null && rawData.addManeuverBonus;
	}

	// Token: 0x06000063 RID: 99 RVA: 0x000043DC File Offset: 0x000025DC
	public bool dealsCombatDamageOnHit()
	{
		SKALDProjectData.AbilityContainers.CombatManeuverContainer.CombatManueverAbility rawData = this.getRawData();
		return rawData != null && rawData.addCombatDamageOnHit;
	}

	// Token: 0x06000064 RID: 100 RVA: 0x000043FC File Offset: 0x000025FC
	public bool allowsCriticalHits()
	{
		SKALDProjectData.AbilityContainers.CombatManeuverContainer.CombatManueverAbility rawData = this.getRawData();
		return rawData != null && rawData.allowCrit;
	}

	// Token: 0x06000065 RID: 101 RVA: 0x0000441C File Offset: 0x0000261C
	public bool allowsBackstab()
	{
		SKALDProjectData.AbilityContainers.CombatManeuverContainer.CombatManueverAbility rawData = this.getRawData();
		return rawData != null && rawData.allowBackstab;
	}

	// Token: 0x06000066 RID: 102 RVA: 0x0000443C File Offset: 0x0000263C
	public int getHitModifier()
	{
		SKALDProjectData.AbilityContainers.CombatManeuverContainer.CombatManueverAbility rawData = this.getRawData();
		if (rawData == null)
		{
			return 0;
		}
		return rawData.toHitBonus;
	}

	// Token: 0x06000067 RID: 103 RVA: 0x0000445C File Offset: 0x0000265C
	public int getDamageBonus()
	{
		SKALDProjectData.AbilityContainers.CombatManeuverContainer.CombatManueverAbility rawData = this.getRawData();
		if (rawData == null)
		{
			return 0;
		}
		return rawData.damageBonus;
	}

	// Token: 0x06000068 RID: 104 RVA: 0x0000447C File Offset: 0x0000267C
	public bool autoCritOnHit()
	{
		SKALDProjectData.AbilityContainers.CombatManeuverContainer.CombatManueverAbility rawData = this.getRawData();
		return rawData != null && rawData.autoCrit;
	}

	// Token: 0x06000069 RID: 105 RVA: 0x0000449C File Offset: 0x0000269C
	public bool applyArmorSoak()
	{
		SKALDProjectData.AbilityContainers.CombatManeuverContainer.CombatManueverAbility rawData = this.getRawData();
		return rawData != null && !rawData.armorPiercing;
	}
}
