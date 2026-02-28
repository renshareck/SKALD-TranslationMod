using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

// Token: 0x02000009 RID: 9
[Serializable]
public abstract class AbilityContainer : CharacterComponentContainer, ISerializable
{
	// Token: 0x0600006A RID: 106 RVA: 0x000044BE File Offset: 0x000026BE
	public AbilityContainer(Character owner) : base(owner)
	{
	}

	// Token: 0x0600006B RID: 107 RVA: 0x000044C7 File Offset: 0x000026C7
	public AbilityContainer(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}

	// Token: 0x0600006C RID: 108 RVA: 0x000044D1 File Offset: 0x000026D1
	public new void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		base.GetObjectData(info, context);
	}

	// Token: 0x0600006D RID: 109 RVA: 0x000044DC File Offset: 0x000026DC
	protected List<string> getItemComponentIdListGeneric<T>()
	{
		if (this.itemComponentIdList == null)
		{
			this.itemComponentIdList = new List<string>();
		}
		else
		{
			this.itemComponentIdList.Clear();
		}
		foreach (string text in this.owner.getConferredAbilitiesFromItems())
		{
			Ability ability = GameData.getAbility(text);
			if (ability != null && ability is T)
			{
				this.itemComponentIdList.Add(text);
			}
		}
		return this.itemComponentIdList;
	}

	// Token: 0x0600006E RID: 110 RVA: 0x00004574 File Offset: 0x00002774
	protected void addAbility(Ability ability, Character character)
	{
		if (ability == null)
		{
			return;
		}
		if (!ability.stackable() && base.hasComponentNatively(ability.getId()))
		{
			return;
		}
		base.addComponent(ability);
		ability.applyAbility(character);
	}

	// Token: 0x0600006F RID: 111 RVA: 0x000045A0 File Offset: 0x000027A0
	public bool hasLegalCombatActivatedComponent()
	{
		foreach (string id in base.getComponentIdList())
		{
			AbilityActive abilityActive = GameData.getAbility(id) as AbilityActive;
			if (abilityActive != null && abilityActive.isCombatActivated() && abilityActive.canUserUseAbility(this.owner).wasSuccess())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000070 RID: 112 RVA: 0x0000461C File Offset: 0x0000281C
	public override List<BaseCharacterComponent> getCombatActivatedComponentList()
	{
		List<BaseCharacterComponent> list = new List<BaseCharacterComponent>();
		foreach (string id in base.getComponentIdList())
		{
			Ability ability = GameData.getAbility(id);
			if (ability != null && ability.isCombatActivated())
			{
				list.Add(ability);
			}
		}
		return list;
	}

	// Token: 0x06000071 RID: 113 RVA: 0x00004688 File Offset: 0x00002888
	public override List<BaseCharacterComponent> getNonCombatActivatedComponentList()
	{
		List<BaseCharacterComponent> list = new List<BaseCharacterComponent>();
		foreach (string id in base.getComponentIdList())
		{
			Ability ability = GameData.getAbility(id);
			if (ability != null && ability.isNonCombatActivated())
			{
				list.Add(ability);
			}
		}
		return list;
	}

	// Token: 0x06000072 RID: 114 RVA: 0x000046F4 File Offset: 0x000028F4
	public override SkaldActionResult useCurrentComponent(Character user)
	{
		if (base.getCurrentComponent() == null)
		{
			return new SkaldActionResult(false, false, "No currentComponent set!", true);
		}
		if (!(base.getCurrentComponent() is AbilityActive))
		{
			return new SkaldActionResult(false, false, "Current component is not an activated ability!", true);
		}
		AbilityActive abilityActive = base.getCurrentComponent() as AbilityActive;
		SkaldActionResult skaldActionResult = abilityActive.canUserUseAbility(user);
		if (!skaldActionResult.wasSuccess())
		{
			return skaldActionResult;
		}
		if (abilityActive.isAttackBased())
		{
			user.beginAttack(this);
			Debug.Log(string.Concat(new string[]
			{
				abilityActive.getId(),
				": ",
				user.getId(),
				" ",
				user.getTileX().ToString(),
				"/",
				user.getTileY().ToString(),
				" VS ",
				user.getTargetOpponent().getId(),
				" ",
				user.getTargetOpponent().getTileX().ToString(),
				"/",
				user.getTargetOpponent().getTileY().ToString(),
				" Legal: ",
				user.hasValidTarget().ToString(),
				" ",
				user.getRange().ToString()
			}));
			return new SkaldActionResult(true, true, this.owner.getName() + " begun: " + abilityActive.getName(), false);
		}
		return base.fireAbility(user, abilityActive);
	}

	// Token: 0x06000073 RID: 115 RVA: 0x00004878 File Offset: 0x00002A78
	public override List<BaseCharacterComponent> getComponentList()
	{
		List<BaseCharacterComponent> list = new List<BaseCharacterComponent>();
		foreach (string id in base.getComponentIdList())
		{
			list.Add(GameData.getAbility(id));
		}
		return list;
	}

	// Token: 0x06000074 RID: 116 RVA: 0x000048D8 File Offset: 0x00002AD8
	public override BaseCharacterComponent forcedGetComponentFromIdWithoutCheckingIfIHaveIt(string id)
	{
		return GameData.getAbility(id);
	}

	// Token: 0x06000075 RID: 117 RVA: 0x000048E0 File Offset: 0x00002AE0
	public override MapCutoutTemplate getTargetZoneCutout(Character user, int x, int y)
	{
		if (!(base.getCurrentComponent() is AbilityActive))
		{
			return null;
		}
		AbilityActive abilityActive = base.getCurrentComponent() as AbilityActive;
		if (abilityActive == null)
		{
			return null;
		}
		return abilityActive.getTargetZoneCutout(user, x, y);
	}

	// Token: 0x04000007 RID: 7
	private List<string> itemComponentIdList;
}
