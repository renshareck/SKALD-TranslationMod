using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

// Token: 0x02000018 RID: 24
[Serializable]
public class SpellContainer : CharacterComponentContainer, ISerializable
{
	// Token: 0x06000182 RID: 386 RVA: 0x00009238 File Offset: 0x00007438
	public SpellContainer(Character owner) : base(owner)
	{
	}

	// Token: 0x06000183 RID: 387 RVA: 0x00009241 File Offset: 0x00007441
	public SpellContainer(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}

	// Token: 0x06000184 RID: 388 RVA: 0x0000924C File Offset: 0x0000744C
	public override SkaldActionResult useCurrentComponent(Character user)
	{
		if (base.getCurrentComponent() == null)
		{
			return new SkaldActionResult(false, false, "No currentComponent set!", true);
		}
		if (!(base.getCurrentComponent() is AbilitySpell))
		{
			return new SkaldActionResult(false, false, "Current component is not a SPELL!", true);
		}
		AbilitySpell ability = base.getCurrentComponent() as AbilitySpell;
		return base.fireAbility(user, ability);
	}

	// Token: 0x06000185 RID: 389 RVA: 0x0000929E File Offset: 0x0000749E
	public override BaseCharacterComponent forcedGetComponentFromIdWithoutCheckingIfIHaveIt(string id)
	{
		return GameData.getSpell(id);
	}

	// Token: 0x06000186 RID: 390 RVA: 0x000092A6 File Offset: 0x000074A6
	protected override List<string> getItemComponentIdList()
	{
		return this.owner.getConferredSpellsFromItems();
	}

	// Token: 0x06000187 RID: 391 RVA: 0x000092B3 File Offset: 0x000074B3
	public new void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		base.GetObjectData(info, context);
	}

	// Token: 0x06000188 RID: 392 RVA: 0x000092BD File Offset: 0x000074BD
	public void addSpell(string id)
	{
		if (base.hasComponentNatively(id))
		{
			return;
		}
		base.addComponent(GameData.getSpell(id));
	}

	// Token: 0x06000189 RID: 393 RVA: 0x000092D8 File Offset: 0x000074D8
	public void addSpell(List<string> idList)
	{
		if (idList == null || idList.Count == 0)
		{
			return;
		}
		foreach (string id in idList)
		{
			this.addSpell(id);
		}
	}

	// Token: 0x0600018A RID: 394 RVA: 0x00009334 File Offset: 0x00007534
	public void addSpell(List<AbilitySpell> spell)
	{
		foreach (AbilitySpell spell2 in spell)
		{
			this.addSpell(spell2);
		}
	}

	// Token: 0x0600018B RID: 395 RVA: 0x00009384 File Offset: 0x00007584
	public void addSpell(AbilitySpell spell)
	{
		if (base.hasComponentNatively(spell.getId()))
		{
			return;
		}
		base.addComponent(spell);
	}

	// Token: 0x0600018C RID: 396 RVA: 0x0000939C File Offset: 0x0000759C
	public override List<BaseCharacterComponent> getNonCombatActivatedComponentList()
	{
		List<BaseCharacterComponent> list = new List<BaseCharacterComponent>();
		foreach (string id in base.getComponentIdList())
		{
			AbilitySpell spell = GameData.getSpell(id);
			if (spell != null && spell.isNonCombatActivated())
			{
				list.Add(spell);
			}
		}
		return list;
	}

	// Token: 0x0600018D RID: 397 RVA: 0x00009408 File Offset: 0x00007608
	public UIPartyEffectSelector getOutOfCombatSpellTargetSelector()
	{
		if (base.getCurrentComponent() == null)
		{
			return null;
		}
		List<Character> partyList = this.owner.getMainParty().getPartyList();
		string effectPattern = (base.getCurrentComponent() as AbilitySpell).getEffectPattern();
		if (effectPattern == "Self")
		{
			return new UIPartyEffectSelector(new List<Character>
			{
				this.owner
			}, false);
		}
		if (effectPattern == "Melee" || effectPattern == "Touch" || effectPattern == "Point")
		{
			return new UIPartyEffectSelector(partyList, false);
		}
		return new UIPartyEffectSelector(partyList, true);
	}

	// Token: 0x0600018E RID: 398 RVA: 0x0000949C File Offset: 0x0000769C
	public override List<BaseCharacterComponent> getCombatActivatedComponentList()
	{
		List<BaseCharacterComponent> list = new List<BaseCharacterComponent>();
		foreach (string id in base.getComponentIdList())
		{
			AbilitySpell spell = GameData.getSpell(id);
			if (spell != null && spell.isCombatActivated())
			{
				list.Add(spell);
			}
		}
		return list;
	}

	// Token: 0x0600018F RID: 399 RVA: 0x00009508 File Offset: 0x00007708
	public bool hasLegalCombatActivatedComponent()
	{
		foreach (string id in base.getComponentIdList())
		{
			AbilitySpell spell = GameData.getSpell(id);
			if (spell != null && spell.isCombatActivated() && spell.canUserUseAbility(this.owner).wasSuccess())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000190 RID: 400 RVA: 0x00009580 File Offset: 0x00007780
	public override List<BaseCharacterComponent> getComponentList()
	{
		List<BaseCharacterComponent> list = new List<BaseCharacterComponent>();
		foreach (string id in base.getComponentIdList())
		{
			list.Add(GameData.getSpell(id));
		}
		return list;
	}

	// Token: 0x06000191 RID: 401 RVA: 0x000095E0 File Offset: 0x000077E0
	public SpellContainer.SpellList getSortedSpellList()
	{
		SpellContainer.SpellList spellList = new SpellContainer.SpellList(this.owner);
		foreach (BaseCharacterComponent baseCharacterComponent in this.getComponentList())
		{
			AbilitySpell item = (AbilitySpell)baseCharacterComponent;
			spellList.spells.Add(item);
		}
		spellList.sortList();
		return spellList;
	}

	// Token: 0x020001BF RID: 447
	public class SpellList
	{
		// Token: 0x06001655 RID: 5717 RVA: 0x00063FE0 File Offset: 0x000621E0
		public SpellList(Character character)
		{
			this.owner = character;
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x00063FFA File Offset: 0x000621FA
		public List<UIButtonControlBase.ButtonData> getButtonDataList()
		{
			return this.getButtonDataList(this.spells);
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x00064008 File Offset: 0x00062208
		private List<UIButtonControlBase.ButtonData> getButtonDataList(List<AbilitySpell> list)
		{
			List<UIButtonControlBase.ButtonData> list2 = new List<UIButtonControlBase.ButtonData>();
			foreach (BaseCharacterComponent baseCharacterComponent in list)
			{
				list2.Add(baseCharacterComponent.getButtonData(this.owner));
			}
			return list2;
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x00064068 File Offset: 0x00062268
		public void sortList()
		{
			List<AbilitySpell> list = new List<AbilitySpell>();
			Dictionary<string, List<AbilitySpell>> dictionary = new Dictionary<string, List<AbilitySpell>>();
			foreach (AbilitySpell abilitySpell in this.spells)
			{
				List<string> schoolList = abilitySpell.getSchoolList();
				if (schoolList.Count != 0)
				{
					string key = schoolList[0];
					if (!dictionary.ContainsKey(key))
					{
						dictionary.Add(key, new List<AbilitySpell>());
					}
					dictionary[key].Add(abilitySpell);
				}
			}
			foreach (KeyValuePair<string, List<AbilitySpell>> keyValuePair in dictionary)
			{
				foreach (AbilitySpell item in this.sortListAlphabetically(keyValuePair.Value))
				{
					list.Add(item);
				}
			}
			this.spells = list;
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x0006418C File Offset: 0x0006238C
		public List<AbilitySpell> sortListAlphabetically(List<AbilitySpell> input)
		{
			List<AbilitySpell> list = new List<AbilitySpell>();
			foreach (AbilitySpell abilitySpell in input)
			{
				if (list.Count == 0)
				{
					list.Add(abilitySpell);
				}
				else
				{
					for (int i = 0; i < list.Count; i++)
					{
						if (abilitySpell.getNameForSorting()[0] < list[i].getNameForSorting()[0])
						{
							list.Insert(i, abilitySpell);
							break;
						}
						if (i == list.Count - 1)
						{
							list.Insert(i + 1, abilitySpell);
							break;
						}
					}
				}
			}
			return list;
		}

		// Token: 0x040006A9 RID: 1705
		public List<AbilitySpell> spells = new List<AbilitySpell>();

		// Token: 0x040006AA RID: 1706
		private Character owner;
	}
}
