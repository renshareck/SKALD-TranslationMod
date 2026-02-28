using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200000F RID: 15
public class AbilityTriggered : AbilityActive
{
	// Token: 0x060000A3 RID: 163 RVA: 0x00005518 File Offset: 0x00003718
	public AbilityTriggered(SKALDProjectData.AbilityContainers.TriggeredAbilityContainer.TriggeredAbility rawData) : base(rawData)
	{
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x00005521 File Offset: 0x00003721
	private SKALDProjectData.AbilityContainers.TriggeredAbilityContainer.TriggeredAbility getRawData()
	{
		return GameData.getAbilityRawData(this.getId()) as SKALDProjectData.AbilityContainers.TriggeredAbilityContainer.TriggeredAbility;
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x00005533 File Offset: 0x00003733
	protected override string printComponentType()
	{
		return "Triggered Ability";
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x0000553A File Offset: 0x0000373A
	protected override string printCost()
	{
		return "\n";
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x00005544 File Offset: 0x00003744
	public override bool isTargetLegal(Character user, Character target)
	{
		int triggerChance = this.getTriggerChance();
		if (triggerChance < 100)
		{
			DicePoolPercentile dicePoolPercentile = new DicePoolPercentile("Trigger Roll");
			if (dicePoolPercentile.getResult() > triggerChance)
			{
				if (MainControl.debugFunctions)
				{
					MainControl.log(string.Concat(new string[]
					{
						"Failed Ability ",
						C64Color.WHITE_TAG,
						this.getId(),
						" </color> from user due to triggerChance: Rolled ",
						dicePoolPercentile.getResult().ToString(),
						" vs chance ",
						triggerChance.ToString()
					}));
				}
				return false;
			}
		}
		return base.isTargetLegal(user, target);
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x000055D8 File Offset: 0x000037D8
	private int getTriggerChance()
	{
		SKALDProjectData.AbilityContainers.TriggeredAbilityContainer.TriggeredAbility rawData = this.getRawData();
		if (rawData == null)
		{
			return 100;
		}
		return rawData.triggerChance;
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x000055F8 File Offset: 0x000037F8
	protected override List<Color32> gridIconBaseColor()
	{
		return new List<Color32>
		{
			C64Color.Yellow,
			C64Color.GrayLight
		};
	}

	// Token: 0x060000AA RID: 170 RVA: 0x00005618 File Offset: 0x00003818
	public bool triggeredOnMeleeHit()
	{
		SKALDProjectData.AbilityContainers.TriggeredAbilityContainer.TriggeredAbility rawData = this.getRawData();
		return rawData != null && rawData.triggerOnMeleeHit;
	}

	// Token: 0x060000AB RID: 171 RVA: 0x00005638 File Offset: 0x00003838
	public bool triggeredOnRangedHit()
	{
		SKALDProjectData.AbilityContainers.TriggeredAbilityContainer.TriggeredAbility rawData = this.getRawData();
		return rawData != null && rawData.triggerOnRangedHit;
	}

	// Token: 0x060000AC RID: 172 RVA: 0x00005658 File Offset: 0x00003858
	public bool triggeredOnChargeHit()
	{
		SKALDProjectData.AbilityContainers.TriggeredAbilityContainer.TriggeredAbility rawData = this.getRawData();
		return rawData != null && rawData.triggerOnChargeHit;
	}

	// Token: 0x060000AD RID: 173 RVA: 0x00005678 File Offset: 0x00003878
	public bool triggeredOnUnarmedHit()
	{
		SKALDProjectData.AbilityContainers.TriggeredAbilityContainer.TriggeredAbility rawData = this.getRawData();
		return rawData != null && rawData.triggerOnUnarmedHit;
	}

	// Token: 0x060000AE RID: 174 RVA: 0x00005698 File Offset: 0x00003898
	public bool triggeredOnCriticalHit()
	{
		SKALDProjectData.AbilityContainers.TriggeredAbilityContainer.TriggeredAbility rawData = this.getRawData();
		return rawData != null && rawData.triggerOnCriticalHit;
	}

	// Token: 0x060000AF RID: 175 RVA: 0x000056B8 File Offset: 0x000038B8
	public bool triggeredOnBackstabHit()
	{
		SKALDProjectData.AbilityContainers.TriggeredAbilityContainer.TriggeredAbility rawData = this.getRawData();
		return rawData != null && rawData.triggerOnBackstabHit;
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x000056D8 File Offset: 0x000038D8
	public bool triggeredOnMiss()
	{
		SKALDProjectData.AbilityContainers.TriggeredAbilityContainer.TriggeredAbility rawData = this.getRawData();
		return rawData != null && rawData.triggerOnMiss;
	}

	// Token: 0x060000B1 RID: 177 RVA: 0x000056F8 File Offset: 0x000038F8
	public bool triggeredOnManueverHit()
	{
		SKALDProjectData.AbilityContainers.TriggeredAbilityContainer.TriggeredAbility rawData = this.getRawData();
		return rawData != null && rawData.triggerOnManeuverHit;
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x00005718 File Offset: 0x00003918
	public bool triggeredOnCombatStart()
	{
		SKALDProjectData.AbilityContainers.TriggeredAbilityContainer.TriggeredAbility rawData = this.getRawData();
		return rawData != null && rawData.triggerOnCombatStart;
	}

	// Token: 0x060000B3 RID: 179 RVA: 0x00005738 File Offset: 0x00003938
	public bool triggeredOnCombatEnd()
	{
		SKALDProjectData.AbilityContainers.TriggeredAbilityContainer.TriggeredAbility rawData = this.getRawData();
		return rawData != null && rawData.triggerOnCombatEnd;
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x00005758 File Offset: 0x00003958
	public bool triggeredOnDead()
	{
		SKALDProjectData.AbilityContainers.TriggeredAbilityContainer.TriggeredAbility rawData = this.getRawData();
		return rawData != null && rawData.triggerOnDead;
	}

	// Token: 0x060000B5 RID: 181 RVA: 0x00005778 File Offset: 0x00003978
	public bool triggerOnWoundDamage()
	{
		SKALDProjectData.AbilityContainers.TriggeredAbilityContainer.TriggeredAbility rawData = this.getRawData();
		return rawData != null && rawData.triggerOnWoundDamage;
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x00005798 File Offset: 0x00003998
	public bool triggerOnAnyDamage()
	{
		SKALDProjectData.AbilityContainers.TriggeredAbilityContainer.TriggeredAbility rawData = this.getRawData();
		return rawData != null && rawData.triggerOnAnyDamage;
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x000057B8 File Offset: 0x000039B8
	public bool triggerOnDefending()
	{
		SKALDProjectData.AbilityContainers.TriggeredAbilityContainer.TriggeredAbility rawData = this.getRawData();
		return rawData != null && rawData.triggerOnDefending;
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x000057D8 File Offset: 0x000039D8
	public bool triggerOnKilledTarget()
	{
		SKALDProjectData.AbilityContainers.TriggeredAbilityContainer.TriggeredAbility rawData = this.getRawData();
		return rawData != null && rawData.triggerOnKilledTarget;
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x000057F8 File Offset: 0x000039F8
	public bool triggerOnKilledMarkedTarget()
	{
		SKALDProjectData.AbilityContainers.TriggeredAbilityContainer.TriggeredAbility rawData = this.getRawData();
		return rawData != null && rawData.triggerOnKilledMarkedTarget;
	}

	// Token: 0x060000BA RID: 186 RVA: 0x00005818 File Offset: 0x00003A18
	public bool triggerOnAllyDead()
	{
		SKALDProjectData.AbilityContainers.TriggeredAbilityContainer.TriggeredAbility rawData = this.getRawData();
		return rawData != null && rawData.triggerOnAllyDead;
	}

	// Token: 0x060000BB RID: 187 RVA: 0x00005838 File Offset: 0x00003A38
	public bool triggerOnDodge()
	{
		SKALDProjectData.AbilityContainers.TriggeredAbilityContainer.TriggeredAbility rawData = this.getRawData();
		return rawData != null && rawData.triggerOnDodge;
	}

	// Token: 0x060000BC RID: 188 RVA: 0x00005858 File Offset: 0x00003A58
	public bool triggerOnDodgeMelee()
	{
		SKALDProjectData.AbilityContainers.TriggeredAbilityContainer.TriggeredAbility rawData = this.getRawData();
		return rawData != null && rawData.triggerOnDodgeMelee;
	}

	// Token: 0x060000BD RID: 189 RVA: 0x00005878 File Offset: 0x00003A78
	public bool triggerOnSpellcasting()
	{
		SKALDProjectData.AbilityContainers.TriggeredAbilityContainer.TriggeredAbility rawData = this.getRawData();
		return rawData != null && rawData.triggerOnSpellcasting;
	}

	// Token: 0x060000BE RID: 190 RVA: 0x00005898 File Offset: 0x00003A98
	public bool triggerOnStartOfTurn()
	{
		SKALDProjectData.AbilityContainers.TriggeredAbilityContainer.TriggeredAbility rawData = this.getRawData();
		return rawData != null && rawData.triggerOnStartOfTurn;
	}
}
