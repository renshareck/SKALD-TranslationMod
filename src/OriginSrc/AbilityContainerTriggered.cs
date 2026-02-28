using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

// Token: 0x0200000C RID: 12
[Serializable]
public class AbilityContainerTriggered : AbilityContainer, ISerializable
{
	// Token: 0x06000082 RID: 130 RVA: 0x000049F2 File Offset: 0x00002BF2
	public AbilityContainerTriggered(Character owner) : base(owner)
	{
	}

	// Token: 0x06000083 RID: 131 RVA: 0x000049FB File Offset: 0x00002BFB
	public AbilityContainerTriggered(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}

	// Token: 0x06000084 RID: 132 RVA: 0x00004A05 File Offset: 0x00002C05
	public new void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		base.GetObjectData(info, context);
	}

	// Token: 0x06000085 RID: 133 RVA: 0x00004A0F File Offset: 0x00002C0F
	protected override List<string> getItemComponentIdList()
	{
		return base.getItemComponentIdListGeneric<AbilityTriggered>();
	}

	// Token: 0x06000086 RID: 134 RVA: 0x00004A17 File Offset: 0x00002C17
	public void addAbility(AbilityTriggered ability, Character character)
	{
		base.addAbility(ability, character);
	}

	// Token: 0x06000087 RID: 135 RVA: 0x00004A24 File Offset: 0x00002C24
	public AbilityContainerTriggered.TriggeredAbilityContainer getTriggeredAbilityList()
	{
		List<AbilityTriggered> list = new List<AbilityTriggered>();
		foreach (string id in base.getComponentIdList())
		{
			Ability ability = GameData.getAbility(id);
			if (ability != null && ability is AbilityTriggered)
			{
				list.Add(ability as AbilityTriggered);
			}
		}
		return new AbilityContainerTriggered.TriggeredAbilityContainer(list, this);
	}

	// Token: 0x020001B7 RID: 439
	public class TriggeredAbilityContainer
	{
		// Token: 0x06001608 RID: 5640 RVA: 0x0006230B File Offset: 0x0006050B
		public TriggeredAbilityContainer(List<AbilityTriggered> triggeredAbilityList, AbilityContainer abilityContainer)
		{
			this.triggeredAbilityList = triggeredAbilityList;
			this.abilityContainer = abilityContainer;
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x00062324 File Offset: 0x00060524
		public void triggerUnarmedHit(Character user)
		{
			if (MainControl.debugFunctions && this.triggeredAbilityList.Count > 0)
			{
				MainControl.log(user.getId() + " is looking for UNARMED triggered abilities!");
			}
			foreach (AbilityTriggered abilityTriggered in this.triggeredAbilityList)
			{
				if (abilityTriggered.triggeredOnUnarmedHit())
				{
					this.abilityContainer.fireAbility(user, abilityTriggered);
				}
			}
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x000623B0 File Offset: 0x000605B0
		public void triggerArmedMeleeHit(Character user)
		{
			if (MainControl.debugFunctions && this.triggeredAbilityList.Count > 0)
			{
				MainControl.log(user.getId() + " is looking for ARMED triggered abilities!");
			}
			foreach (AbilityTriggered abilityTriggered in this.triggeredAbilityList)
			{
				if (abilityTriggered.triggeredOnMeleeHit())
				{
					this.abilityContainer.fireAbility(user, abilityTriggered);
				}
			}
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x0006243C File Offset: 0x0006063C
		public void triggerRangedHit(Character user)
		{
			if (MainControl.debugFunctions && this.triggeredAbilityList.Count > 0)
			{
				MainControl.log(user.getId() + " is looking for RANGED triggered abilities!");
			}
			foreach (AbilityTriggered abilityTriggered in this.triggeredAbilityList)
			{
				if (abilityTriggered.triggeredOnRangedHit())
				{
					this.abilityContainer.fireAbility(user, abilityTriggered);
				}
			}
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x000624C8 File Offset: 0x000606C8
		public void triggerChargeHit(Character user)
		{
			if (MainControl.debugFunctions && this.triggeredAbilityList.Count > 0)
			{
				MainControl.log(user.getId() + " is looking for CHARGE HIT triggered abilities!");
			}
			foreach (AbilityTriggered abilityTriggered in this.triggeredAbilityList)
			{
				if (abilityTriggered.triggeredOnChargeHit())
				{
					this.abilityContainer.fireAbility(user, abilityTriggered);
				}
			}
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x00062554 File Offset: 0x00060754
		public void triggerCriticalHit(Character user)
		{
			if (MainControl.debugFunctions && this.triggeredAbilityList.Count > 0)
			{
				MainControl.log(user.getId() + " is looking for CRITICAL triggered abilities!");
			}
			foreach (AbilityTriggered abilityTriggered in this.triggeredAbilityList)
			{
				if (abilityTriggered.triggeredOnCriticalHit())
				{
					this.abilityContainer.fireAbility(user, abilityTriggered);
				}
			}
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x000625E0 File Offset: 0x000607E0
		public void triggerBackstabHit(Character user)
		{
			if (MainControl.debugFunctions && this.triggeredAbilityList.Count > 0)
			{
				MainControl.log(user.getId() + " is looking for BACKSTAB triggered abilities!");
			}
			foreach (AbilityTriggered abilityTriggered in this.triggeredAbilityList)
			{
				if (abilityTriggered.triggeredOnBackstabHit())
				{
					this.abilityContainer.fireAbility(user, abilityTriggered);
				}
			}
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x0006266C File Offset: 0x0006086C
		public void triggerOnManueverHit(Character user)
		{
			if (MainControl.debugFunctions && this.triggeredAbilityList.Count > 0)
			{
				MainControl.log(user.getId() + " is looking for BACKSTAB triggered abilities!");
			}
			foreach (AbilityTriggered abilityTriggered in this.triggeredAbilityList)
			{
				if (abilityTriggered.triggeredOnManueverHit())
				{
					this.abilityContainer.fireAbility(user, abilityTriggered);
				}
			}
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x000626F8 File Offset: 0x000608F8
		public void triggerMiss(Character user)
		{
			if (MainControl.debugFunctions && this.triggeredAbilityList.Count > 0)
			{
				MainControl.log(user.getId() + " is looking for MISS triggered abilities!");
			}
			foreach (AbilityTriggered abilityTriggered in this.triggeredAbilityList)
			{
				if (abilityTriggered.triggeredOnMiss())
				{
					this.abilityContainer.fireAbility(user, abilityTriggered);
				}
			}
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x00062784 File Offset: 0x00060984
		public void triggerCombatStart(Character user)
		{
			if (MainControl.debugFunctions && this.triggeredAbilityList.Count > 0)
			{
				MainControl.log(user.getId() + " is looking for MISS triggered abilities!");
			}
			foreach (AbilityTriggered abilityTriggered in this.triggeredAbilityList)
			{
				if (abilityTriggered.triggeredOnCombatStart())
				{
					this.abilityContainer.fireAbility(user, abilityTriggered);
				}
			}
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x00062810 File Offset: 0x00060A10
		public void triggerCombatEnd(Character user)
		{
			if (MainControl.debugFunctions && this.triggeredAbilityList.Count > 0)
			{
				MainControl.log(user.getId() + " is looking for MISS triggered abilities!");
			}
			foreach (AbilityTriggered abilityTriggered in this.triggeredAbilityList)
			{
				if (abilityTriggered.triggeredOnCombatEnd())
				{
					this.abilityContainer.fireAbility(user, abilityTriggered);
				}
			}
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x0006289C File Offset: 0x00060A9C
		public void triggerDead(Character user)
		{
			if (MainControl.debugFunctions && this.triggeredAbilityList.Count > 0)
			{
				MainControl.log(user.getId() + " is looking for DEAD triggered abilities!");
			}
			foreach (AbilityTriggered abilityTriggered in this.triggeredAbilityList)
			{
				if (abilityTriggered.triggeredOnDead())
				{
					this.abilityContainer.fireAbility(user, abilityTriggered);
				}
			}
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x00062928 File Offset: 0x00060B28
		public void triggerWoundDamage(Character user)
		{
			if (MainControl.debugFunctions && this.triggeredAbilityList.Count > 0)
			{
				MainControl.log(user.getId() + " is looking for WOUND triggered abilities!");
			}
			foreach (AbilityTriggered abilityTriggered in this.triggeredAbilityList)
			{
				if (abilityTriggered.triggerOnWoundDamage())
				{
					this.abilityContainer.fireAbility(user, abilityTriggered);
				}
			}
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x000629B4 File Offset: 0x00060BB4
		public void triggerAnyDamage(Character user)
		{
			if (MainControl.debugFunctions && this.triggeredAbilityList.Count > 0)
			{
				MainControl.log(user.getId() + " is looking for ANYDAMAGE triggered abilities!");
			}
			foreach (AbilityTriggered abilityTriggered in this.triggeredAbilityList)
			{
				if (abilityTriggered.triggerOnAnyDamage())
				{
					this.abilityContainer.fireAbility(user, abilityTriggered);
				}
			}
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x00062A40 File Offset: 0x00060C40
		public void triggerDefending(Character user)
		{
			if (MainControl.debugFunctions && this.triggeredAbilityList.Count > 0)
			{
				MainControl.log(user.getId() + " is looking for DEFENDING triggered abilities!");
			}
			foreach (AbilityTriggered abilityTriggered in this.triggeredAbilityList)
			{
				if (abilityTriggered.triggerOnDefending())
				{
					this.abilityContainer.fireAbility(user, abilityTriggered);
				}
			}
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x00062ACC File Offset: 0x00060CCC
		public void triggerKilledTarget(Character user)
		{
			if (MainControl.debugFunctions && this.triggeredAbilityList.Count > 0)
			{
				MainControl.log(user.getId() + " is looking for KILLED TARGET triggered abilities!");
			}
			foreach (AbilityTriggered abilityTriggered in this.triggeredAbilityList)
			{
				if (abilityTriggered.triggerOnKilledTarget())
				{
					this.abilityContainer.fireAbility(user, abilityTriggered);
				}
			}
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x00062B58 File Offset: 0x00060D58
		public void triggerKilledMarkedTarget(Character user)
		{
			if (MainControl.debugFunctions && this.triggeredAbilityList.Count > 0)
			{
				MainControl.log(user.getId() + " is looking for KILLED MARKED TARGET triggered abilities!");
			}
			foreach (AbilityTriggered abilityTriggered in this.triggeredAbilityList)
			{
				if (abilityTriggered.triggerOnKilledMarkedTarget())
				{
					this.abilityContainer.fireAbility(user, abilityTriggered);
				}
			}
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x00062BE4 File Offset: 0x00060DE4
		public void triggerAllyDead(Character user)
		{
			if (MainControl.debugFunctions && this.triggeredAbilityList.Count > 0)
			{
				MainControl.log(user.getId() + " is looking for ALLY DEAD triggered abilities!");
			}
			foreach (AbilityTriggered abilityTriggered in this.triggeredAbilityList)
			{
				if (abilityTriggered.triggerOnAllyDead())
				{
					this.abilityContainer.fireAbility(user, abilityTriggered);
				}
			}
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x00062C70 File Offset: 0x00060E70
		public void triggerDodge(Character user)
		{
			if (MainControl.debugFunctions && this.triggeredAbilityList.Count > 0)
			{
				MainControl.log(user.getId() + " is looking for DODGE triggered abilities!");
			}
			foreach (AbilityTriggered abilityTriggered in this.triggeredAbilityList)
			{
				if (abilityTriggered.triggerOnDodge())
				{
					this.abilityContainer.fireAbility(user, abilityTriggered);
				}
			}
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x00062CFC File Offset: 0x00060EFC
		public void triggerDodgeMelee(Character user)
		{
			if (MainControl.debugFunctions && this.triggeredAbilityList.Count > 0)
			{
				MainControl.log(user.getId() + " is looking for DODGE MELEE triggered abilities!");
			}
			foreach (AbilityTriggered abilityTriggered in this.triggeredAbilityList)
			{
				if (abilityTriggered.triggerOnDodgeMelee())
				{
					this.abilityContainer.fireAbility(user, abilityTriggered);
				}
			}
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x00062D88 File Offset: 0x00060F88
		public void triggerSpellcasting(Character user)
		{
			if (MainControl.debugFunctions && this.triggeredAbilityList.Count > 0)
			{
				MainControl.log(user.getId() + " is looking for SPELLCASTING triggered abilities!");
			}
			foreach (AbilityTriggered abilityTriggered in this.triggeredAbilityList)
			{
				if (abilityTriggered.triggerOnSpellcasting())
				{
					this.abilityContainer.fireAbility(user, abilityTriggered);
				}
			}
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x00062E14 File Offset: 0x00061014
		public void triggerStartOfTurn(Character user)
		{
			if (MainControl.debugFunctions && this.triggeredAbilityList.Count > 0)
			{
				MainControl.log(user.getId() + " is looking for STARTOFTURN triggered abilities!");
			}
			foreach (AbilityTriggered abilityTriggered in this.triggeredAbilityList)
			{
				if (abilityTriggered.triggerOnStartOfTurn())
				{
					this.abilityContainer.fireAbility(user, abilityTriggered);
				}
			}
		}

		// Token: 0x04000688 RID: 1672
		private List<AbilityTriggered> triggeredAbilityList;

		// Token: 0x04000689 RID: 1673
		private AbilityContainer abilityContainer;
	}
}
