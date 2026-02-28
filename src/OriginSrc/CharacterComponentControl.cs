using System;
using System.Collections.Generic;

// Token: 0x02000013 RID: 19
public class CharacterComponentControl
{
	// Token: 0x0600010A RID: 266 RVA: 0x00006A7E File Offset: 0x00004C7E
	public Effect getEffect(string id)
	{
		return this.effectControl.getFeature(id);
	}

	// Token: 0x0600010B RID: 267 RVA: 0x00006A8C File Offset: 0x00004C8C
	public void addEffect(Effect effect)
	{
		this.effectControl.addFeature(effect);
	}

	// Token: 0x0600010C RID: 268 RVA: 0x00006A9A File Offset: 0x00004C9A
	public AbilitySpell getSpell(string id)
	{
		return this.spellControl.getFeature(id);
	}

	// Token: 0x0600010D RID: 269 RVA: 0x00006AA8 File Offset: 0x00004CA8
	public void addSpell(AbilitySpell spell)
	{
		this.spellControl.addFeature(spell);
	}

	// Token: 0x0600010E RID: 270 RVA: 0x00006AB6 File Offset: 0x00004CB6
	public Ability getAbility(string id)
	{
		return this.abilityControl.getFeature(id);
	}

	// Token: 0x0600010F RID: 271 RVA: 0x00006AC4 File Offset: 0x00004CC4
	public void addAbility(Ability ability)
	{
		this.abilityControl.addFeature(ability);
	}

	// Token: 0x06000110 RID: 272 RVA: 0x00006AD2 File Offset: 0x00004CD2
	public void addCondition(Condition condition)
	{
		this.conditionControl.addFeature(condition);
	}

	// Token: 0x06000111 RID: 273 RVA: 0x00006AE0 File Offset: 0x00004CE0
	public Condition getCondition(string id)
	{
		return this.conditionControl.getFeature(id);
	}

	// Token: 0x04000010 RID: 16
	private CharacterComponentControl.CharacterComponenetControl<Ability> abilityControl = new CharacterComponentControl.CharacterComponenetControl<Ability>();

	// Token: 0x04000011 RID: 17
	private CharacterComponentControl.CharacterComponenetControl<AbilitySpell> spellControl = new CharacterComponentControl.CharacterComponenetControl<AbilitySpell>();

	// Token: 0x04000012 RID: 18
	private CharacterComponentControl.CharacterComponenetControl<Condition> conditionControl = new CharacterComponentControl.CharacterComponenetControl<Condition>();

	// Token: 0x04000013 RID: 19
	private CharacterComponentControl.CharacterComponenetControl<Effect> effectControl = new CharacterComponentControl.CharacterComponenetControl<Effect>();

	// Token: 0x020001BC RID: 444
	private class CharacterComponenetControl<T> where T : SkaldBaseObject
	{
		// Token: 0x0600162B RID: 5675 RVA: 0x00062F84 File Offset: 0x00061184
		public void addFeature(T feature)
		{
			if (!this.features.ContainsKey(feature.getId()))
			{
				this.features.Add(feature.getId(), feature);
				return;
			}
			MainControl.logWarning("Trying to add existing component into componenet dictionary: " + feature.getId());
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x00062FDC File Offset: 0x000611DC
		public T getFeature(string id)
		{
			if (this.features.ContainsKey(id))
			{
				return this.features[id];
			}
			return default(T);
		}

		// Token: 0x04000692 RID: 1682
		private Dictionary<string, T> features = new Dictionary<string, T>();
	}
}
