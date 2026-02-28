using System;
using System.Collections.Generic;

// Token: 0x02000007 RID: 7
public abstract class AbilityActive : Ability
{
	// Token: 0x0600002C RID: 44 RVA: 0x000028D3 File Offset: 0x00000AD3
	protected AbilityActive(SKALDProjectData.AbilityContainers.ActiveAbility rawData) : base(rawData)
	{
	}

	// Token: 0x0600002D RID: 45 RVA: 0x000028DC File Offset: 0x00000ADC
	private SKALDProjectData.AbilityContainers.ActiveAbility getRawData()
	{
		return GameData.getAbilityRawData(this.getId()) as SKALDProjectData.AbilityContainers.ActiveAbility;
	}

	// Token: 0x0600002E RID: 46 RVA: 0x000028F0 File Offset: 0x00000AF0
	public virtual SkaldActionResult canUserUseAbility(Character user)
	{
		SkaldActionResult skaldActionResult = this.isUserEligibleToUseAbility(user);
		if (!skaldActionResult.wasSuccess())
		{
			return skaldActionResult;
		}
		skaldActionResult = this.canUserAffordAbility(user);
		if (!skaldActionResult.wasSuccess())
		{
			return skaldActionResult;
		}
		return new SkaldActionResult(true, true, "Can use " + this.getName(), true);
	}

	// Token: 0x0600002F RID: 47 RVA: 0x0000293C File Offset: 0x00000B3C
	public override UIButtonControlBase.ButtonData getButtonData(Character owner)
	{
		if (owner == null)
		{
			return base.getButtonData(owner);
		}
		SkaldActionResult skaldActionResult = this.canUserUseAbility(owner);
		if (skaldActionResult.wasSuccess())
		{
			return base.getButtonData(owner);
		}
		return new UIButtonControlBase.ButtonData(AbilityActive.lockedIcon, this.getName() + skaldActionResult.getResultString());
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00002987 File Offset: 0x00000B87
	public virtual SkaldActionResult canUserAffordAbility(Character user)
	{
		if (user == null)
		{
			new SkaldActionResult(true, true, "Can afford ability: " + this.getName(), true);
		}
		return new SkaldActionResult(true, true, user.getName() + " can afford ability: " + this.getName(), true);
	}

	// Token: 0x06000031 RID: 49 RVA: 0x000029C4 File Offset: 0x00000BC4
	public bool isTargetCauseForAbortingUse(Character user, Character target)
	{
		SKALDProjectData.AbilityContainers.ActiveAbility rawData = this.getRawData();
		return rawData != null && (!rawData.isPositiveForTarget && rawData.targetAllies && user.isNPCAlly(target));
	}

	// Token: 0x06000032 RID: 50 RVA: 0x000029FC File Offset: 0x00000BFC
	public bool isTargetAGoodCaseForUse(Character user, Character target)
	{
		SKALDProjectData.AbilityContainers.ActiveAbility rawData = this.getRawData();
		return rawData == null || (rawData.isPositiveForTarget && rawData.targetAllies && user.isNPCAlly(target)) || (!rawData.isPositiveForTarget && rawData.targetEnemies && user.isNPCHostile(target));
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00002A4C File Offset: 0x00000C4C
	public SkaldActionResult isUserEligibleToUseAbility(Character user)
	{
		if (user == null)
		{
			new SkaldActionResult(true, true, "Can use " + this.getName(), true);
		}
		if (!user.isPC() && !this.testAIUseability())
		{
			return new SkaldActionResult(false, false, "Can't use " + this.getName().ToUpper() + ". Ability is not useable by AI.", true);
		}
		SKALDProjectData.AbilityContainers.ActiveAbility rawData = this.getRawData();
		if (rawData.userMustMove && user.isImmobilized())
		{
			return new SkaldActionResult(false, false, string.Concat(new string[]
			{
				"Can't use ",
				this.getName().ToUpper(),
				". ",
				user.getName(),
				" is IMMOBILIZED."
			}), true);
		}
		if (rawData.userMustSee && user.isBlind())
		{
			return new SkaldActionResult(false, false, string.Concat(new string[]
			{
				"Can't use ",
				this.getName().ToUpper(),
				". ",
				user.getName(),
				" is BLIND."
			}), true);
		}
		if (rawData.userMustSpeak && user.isSilenced())
		{
			return new SkaldActionResult(false, false, string.Concat(new string[]
			{
				"Can't use ",
				this.getName().ToUpper(),
				". ",
				user.getName(),
				" is SILENCED."
			}), true);
		}
		if (user.isArmed() && !user.isWeaponRanged() && !rawData.armedMelee)
		{
			return new SkaldActionResult(false, false, "Can't use " + this.getName().ToUpper() + ". This ability cannot be used with MELEE WEAPONS.", true);
		}
		if (!user.isArmed() && !rawData.unarmed)
		{
			return new SkaldActionResult(false, false, "Can't use " + this.getName().ToUpper() + ". This ability cannot be used with UNARMED.", true);
		}
		if (user.isWeaponRanged() && !rawData.ranged)
		{
			return new SkaldActionResult(false, false, "Can't use " + this.getName().ToUpper() + ". This ability cannot be used with RANGED WEAPONS.", true);
		}
		if (rawData.requirePiercing && !user.isCurrentWeaponPiercing())
		{
			return new SkaldActionResult(false, false, string.Concat(new string[]
			{
				"Can't use ",
				this.getName().ToUpper(),
				". ",
				user.getName(),
				" must used a PIERCING weapon."
			}), true);
		}
		if (rawData.requireBlunt && !user.isCurrentWeaponBlunt())
		{
			return new SkaldActionResult(false, false, string.Concat(new string[]
			{
				"Can't use ",
				this.getName().ToUpper(),
				". ",
				user.getName(),
				" must used a BLUNT weapon."
			}), true);
		}
		if (rawData.requireSlashing && !user.isCurrentWeaponSlashing())
		{
			return new SkaldActionResult(false, false, string.Concat(new string[]
			{
				"Can't use ",
				this.getName().ToUpper(),
				". ",
				user.getName(),
				" must used a SLASHING weapon."
			}), true);
		}
		if (rawData.requireShield && user.getCurrentShieldIfInHand() == null)
		{
			return new SkaldActionResult(false, false, string.Concat(new string[]
			{
				"Can't use ",
				this.getName().ToUpper(),
				". ",
				user.getName(),
				" must be using a SHIELD."
			}), true);
		}
		if (rawData.requirePolearm && !user.isCurrentWeaponPolearm())
		{
			return new SkaldActionResult(false, false, string.Concat(new string[]
			{
				"Can't use ",
				this.getName().ToUpper(),
				". ",
				user.getName(),
				" must be wielding a POLEARM."
			}), true);
		}
		if (rawData.requireSword && !user.isCurrentWeaponSword())
		{
			return new SkaldActionResult(false, false, string.Concat(new string[]
			{
				"Can't use ",
				this.getName().ToUpper(),
				". ",
				user.getName(),
				" must be wielding a BLADE."
			}), true);
		}
		if (rawData.requireClub && !user.isCurrentWeaponClub())
		{
			return new SkaldActionResult(false, false, string.Concat(new string[]
			{
				"Can't use ",
				this.getName().ToUpper(),
				". ",
				user.getName(),
				" must be wielding a CLUB."
			}), true);
		}
		if (rawData.requireAxe && !user.isCurrentWeaponAxe())
		{
			return new SkaldActionResult(false, false, string.Concat(new string[]
			{
				"Can't use ",
				this.getName().ToUpper(),
				". ",
				user.getName(),
				" must be wielding an AXE."
			}), true);
		}
		if (rawData.requireBow && !user.isCurrentWeaponBow())
		{
			return new SkaldActionResult(false, false, string.Concat(new string[]
			{
				"Can't use ",
				this.getName().ToUpper(),
				". ",
				user.getName(),
				" must be wielding a BOW."
			}), true);
		}
		if (rawData.requireLight && !user.isCurrentWeaponLight())
		{
			return new SkaldActionResult(false, false, string.Concat(new string[]
			{
				"Can't use ",
				this.getName().ToUpper(),
				". ",
				user.getName(),
				" must be wielding a LIGHT weapon."
			}), true);
		}
		if (rawData.requireMedium && !user.isCurrentWeaponMedium())
		{
			return new SkaldActionResult(false, false, string.Concat(new string[]
			{
				"Can't use ",
				this.getName().ToUpper(),
				". ",
				user.getName(),
				" must be wielding a MEDIUM weapon."
			}), true);
		}
		if (rawData.requireHeavy && !user.isCurrentWeaponHeavy())
		{
			return new SkaldActionResult(false, false, string.Concat(new string[]
			{
				"Can't use ",
				this.getName().ToUpper(),
				". ",
				user.getName(),
				" must be wielding a HEAVY weapon."
			}), true);
		}
		if (rawData.requireOutOfMelee && user.isInMelee())
		{
			return new SkaldActionResult(false, false, string.Concat(new string[]
			{
				"Can't use ",
				this.getName().ToUpper(),
				". ",
				user.getName(),
				" cannot use this ability whilst in MELEE."
			}), true);
		}
		return new SkaldActionResult(true, true, "Can use " + this.getName(), true);
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00003098 File Offset: 0x00001298
	public virtual void payForUse(Character user)
	{
	}

	// Token: 0x06000035 RID: 53 RVA: 0x0000309C File Offset: 0x0000129C
	protected int getRadius()
	{
		SKALDProjectData.AbilityContainers.ActiveAbility rawData = this.getRawData();
		if (rawData == null)
		{
			return 0;
		}
		return rawData.radius;
	}

	// Token: 0x06000036 RID: 54 RVA: 0x000030BB File Offset: 0x000012BB
	public virtual bool testAIUseability()
	{
		return true;
	}

	// Token: 0x06000037 RID: 55 RVA: 0x000030BE File Offset: 0x000012BE
	protected virtual string printCost()
	{
		return "";
	}

	// Token: 0x06000038 RID: 56 RVA: 0x000030C5 File Offset: 0x000012C5
	protected virtual string printTimeCost()
	{
		return "";
	}

	// Token: 0x06000039 RID: 57 RVA: 0x000030CC File Offset: 0x000012CC
	public override string getFullDescription()
	{
		SKALDProjectData.AbilityContainers.ActiveAbility rawData = this.getRawData();
		string text = this.printComponentTypeFormated() + "\n";
		text += this.printCost();
		text += this.printTimeCost();
		if (this.getEffectPattern() == "Self")
		{
			text += TextTools.formateNameValuePair("\nTargets", "Self");
		}
		else
		{
			if (rawData.targetAllies && rawData.targetEnemies)
			{
				text += TextTools.formateNameValuePair("\nTargets", "Anyone");
			}
			else if (rawData.targetAllies)
			{
				text += TextTools.formateNameValuePair("\nTargets", "Allies");
			}
			else if (rawData.targetEnemies)
			{
				text += TextTools.formateNameValuePair("\nTargets", "Enemies");
			}
			text += TextTools.formateNameValuePair("\nAoE.", this.getEffectPattern());
		}
		if (base.getDescription() != "")
		{
			text = text + "\n\n" + base.getDescription();
		}
		else
		{
			text = string.Concat(new string[]
			{
				text,
				"\n\n",
				this.printEffectsHeader(rawData),
				"\n\n",
				this.printUseEffects(rawData)
			});
		}
		return text;
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00003210 File Offset: 0x00001410
	protected virtual string printEffectsHeader(SKALDProjectData.AbilityContainers.ActiveAbility rawData)
	{
		string text = "Targets ";
		if (this.getEffectPattern() == "Self")
		{
			text += "the character themself.";
		}
		else if (this.getEffectPattern() == "Point")
		{
			if (rawData.targetAllies && rawData.targetEnemies)
			{
				text += "any opponent or ally";
			}
			else if (rawData.targetAllies)
			{
				text += "any ally";
			}
			else if (rawData.targetEnemies)
			{
				text += "any opponent";
			}
			else
			{
				text += "any tile";
			}
			text += " anywhere on the battlefield.";
		}
		else if (this.getEffectPattern() == "Touch" || this.getEffectPattern() == "Melee")
		{
			if (rawData.targetAllies && rawData.targetEnemies)
			{
				text += "any opponent or ally";
			}
			else if (rawData.targetAllies)
			{
				text += "any ally";
			}
			else if (rawData.targetEnemies)
			{
				text += "any opponent";
			}
			else
			{
				text += "any tile";
			}
			text += " adjacent to the caster.";
		}
		else if (this.getEffectPattern() == "All")
		{
			text += "all opponents and allies on the battlefield.";
		}
		else if (this.getEffectPattern() == "AllEnemies")
		{
			text += "all opponents on the battlefield.";
		}
		else if (this.getEffectPattern() == "AllAllies")
		{
			text += "all allies on the battlefield.";
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
			else
			{
				text += "any tile";
			}
			text = text + " in the " + this.getEffectPattern() + " target area.";
		}
		return text;
	}

	// Token: 0x0600003B RID: 59 RVA: 0x00003420 File Offset: 0x00001620
	protected string printUseEffects(SKALDProjectData.AbilityContainers.ActiveAbility rawData)
	{
		Effect.EffectDescription effectDescription = new Effect.EffectDescription();
		foreach (string id in this.getUseEffectList())
		{
			effectDescription.mergeInEffectDescription(GameData.getEffect(id));
		}
		string text = effectDescription.printDescription();
		if (rawData.creatureSummoned.Count != 0)
		{
			if (text != "")
			{
				text += "\n\n";
			}
			text += "Summons one of the following creatures in each target area tile: ";
			foreach (string id2 in rawData.creatureSummoned)
			{
				text = text + GameData.getCharacterRawData(id2).title + ", ";
			}
			text = TextTools.removeTrailingComma(text);
		}
		return text;
	}

	// Token: 0x0600003C RID: 60 RVA: 0x00003514 File Offset: 0x00001714
	protected string printSuccessEffects(SKALDProjectData.AbilityContainers.ActiveAbility rawData)
	{
		Effect.EffectDescription effectDescription = new Effect.EffectDescription();
		foreach (string id in this.getSuccessEffectList())
		{
			effectDescription.mergeInEffectDescription(GameData.getEffect(id));
		}
		string text = effectDescription.printDescription();
		if (rawData.creatureSummoned.Count != 0)
		{
			if (text != "")
			{
				text += "\n\n";
			}
			text += "Summons one of the following creatures in each target area tile: ";
			foreach (string id2 in rawData.creatureSummoned)
			{
				text = text + GameData.getCharacterRawData(id2).title + ", ";
			}
			text = TextTools.removeTrailingComma(text);
		}
		return text;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00003608 File Offset: 0x00001808
	public override string getTargetingString()
	{
		string a = this.getEffectPattern().ToUpper();
		if (a == "SELF")
		{
			return "Targets user. Click anywhere to use ability!";
		}
		if (a == "AURA")
		{
			return "Aura effect. Click anywhere to use ability!";
		}
		if (a == "ALL")
		{
			return "Targets everyone. Click anywhere to use ability!";
		}
		if (a == "ALLALLIES")
		{
			return "Targets all allies. Click anywhere to use ability!";
		}
		if (a == "ALLENEMIES")
		{
			return "Targets all enemies. Click anywhere to use ability!";
		}
		return "Select a target to use ability!";
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00003688 File Offset: 0x00001888
	public override MapCutoutTemplate getTargetZoneCutout(Character user, int x, int y)
	{
		string text = this.getEffectPattern().ToUpper();
		if (text == "SELF")
		{
			return new MapCutoutPoint(user);
		}
		if (text == "AURA")
		{
			return new MapCutoutAura(this.getRadius(), user, this.isPositiveForTarget());
		}
		if (text == "SPHERE")
		{
			return new MapCutoutSphere(x, y, this.getRadius(), user);
		}
		if (text == "POINT")
		{
			return new MapCutoutPoint(x, y, user);
		}
		if (text == "MELEE")
		{
			return new MapCutoutLine(x, y, 1, user);
		}
		if (text == "TOUCH")
		{
			return new MapCutoutTouch(x, y, user);
		}
		if (text == "LINE")
		{
			return new MapCutoutLine(x, y, this.getRadius(), user);
		}
		if (text == "CONE")
		{
			return new MapCutoutCone(x, y, this.getRadius(), user);
		}
		if (text == "ALL")
		{
			return new MapCutoutNPCsAll(user);
		}
		if (text == "ALLALLIES")
		{
			return new MapCutoutNPCsAllies(user);
		}
		if (text == "ALLENEMIES")
		{
			return new MapCutoutNPCsEnemies(user);
		}
		if (text != "")
		{
			MainControl.logError("Malformed effect pattern " + text + " for ability " + this.getId());
		}
		return new MapCutoutPoint(x, y, user);
	}

	// Token: 0x0600003F RID: 63 RVA: 0x000037D8 File Offset: 0x000019D8
	public string getEffectPattern()
	{
		SKALDProjectData.AbilityContainers.ActiveAbility rawData = this.getRawData();
		if (rawData == null)
		{
			return "";
		}
		return rawData.effectPattern;
	}

	// Token: 0x06000040 RID: 64 RVA: 0x000037FC File Offset: 0x000019FC
	public bool isPositiveForTarget()
	{
		SKALDProjectData.AbilityContainers.ActiveAbility rawData = this.getRawData();
		return rawData != null && rawData.isPositiveForTarget;
	}

	// Token: 0x06000041 RID: 65 RVA: 0x0000381C File Offset: 0x00001A1C
	public virtual string getSoundEffect()
	{
		SKALDProjectData.AbilityContainers.ActiveAbility rawData = this.getRawData();
		if (rawData == null)
		{
			return "";
		}
		return rawData.soundEffect;
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00003840 File Offset: 0x00001A40
	public virtual string getUserAnimation()
	{
		SKALDProjectData.AbilityContainers.ActiveAbility rawData = this.getRawData();
		if (rawData == null)
		{
			return "";
		}
		return rawData.useAnimation;
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00003864 File Offset: 0x00001A64
	public virtual List<string> getUserParticleEffect()
	{
		SKALDProjectData.AbilityContainers.ActiveAbility rawData = this.getRawData();
		if (rawData == null)
		{
			return null;
		}
		return rawData.userParticleEffect;
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00003884 File Offset: 0x00001A84
	protected string getUserAttribute()
	{
		SKALDProjectData.AbilityContainers.ActiveAbility rawData = this.getRawData();
		if (rawData == null)
		{
			return "";
		}
		return rawData.userAttribute;
	}

	// Token: 0x06000045 RID: 69 RVA: 0x000038A8 File Offset: 0x00001AA8
	protected string getTargetAttribute()
	{
		SKALDProjectData.AbilityContainers.ActiveAbility rawData = this.getRawData();
		if (rawData == null)
		{
			return "";
		}
		return rawData.targetAttribute;
	}

	// Token: 0x06000046 RID: 70 RVA: 0x000038CB File Offset: 0x00001ACB
	protected void fireUseTriggers(Character user, Character target)
	{
		this.fireEffects(this.getUseEffectList(), user, target);
		this.fireTrigger(this.getUseTrigger(), user);
	}

	// Token: 0x06000047 RID: 71 RVA: 0x000038E8 File Offset: 0x00001AE8
	protected void fireSuccessTriggers(Character user, Character target)
	{
		this.fireEffects(this.getSuccessEffectList(), user, target);
		this.fireTrigger(this.getSuccessTrigger(), user);
	}

	// Token: 0x06000048 RID: 72 RVA: 0x00003905 File Offset: 0x00001B05
	protected void fireFailureTriggers(Character user, Character target)
	{
		this.fireEffects(this.getFailureEffectList(), user, target);
		this.fireTrigger(this.getFailureTrigger(), user);
	}

	// Token: 0x06000049 RID: 73 RVA: 0x00003924 File Offset: 0x00001B24
	private List<string> getUseEffectList()
	{
		SKALDProjectData.AbilityContainers.ActiveAbility rawData = this.getRawData();
		if (rawData == null)
		{
			return null;
		}
		return rawData.useEffect;
	}

	// Token: 0x0600004A RID: 74 RVA: 0x00003944 File Offset: 0x00001B44
	private List<string> getSuccessEffectList()
	{
		SKALDProjectData.AbilityContainers.ActiveAbility rawData = this.getRawData();
		if (rawData == null)
		{
			return null;
		}
		return rawData.successEffect;
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00003964 File Offset: 0x00001B64
	private List<string> getFailureEffectList()
	{
		SKALDProjectData.AbilityContainers.ActiveAbility rawData = this.getRawData();
		if (rawData == null)
		{
			return null;
		}
		return rawData.failureEffect;
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00003984 File Offset: 0x00001B84
	private string getUseTrigger()
	{
		SKALDProjectData.AbilityContainers.ActiveAbility rawData = this.getRawData();
		if (rawData == null)
		{
			return null;
		}
		return rawData.useTrigger;
	}

	// Token: 0x0600004D RID: 77 RVA: 0x000039A3 File Offset: 0x00001BA3
	public virtual bool isAttackBased()
	{
		return false;
	}

	// Token: 0x0600004E RID: 78 RVA: 0x000039A8 File Offset: 0x00001BA8
	private string getSuccessTrigger()
	{
		SKALDProjectData.AbilityContainers.ActiveAbility rawData = this.getRawData();
		if (rawData == null)
		{
			return null;
		}
		return rawData.successTrigger;
	}

	// Token: 0x0600004F RID: 79 RVA: 0x000039C8 File Offset: 0x00001BC8
	private string getFailureTrigger()
	{
		SKALDProjectData.AbilityContainers.ActiveAbility rawData = this.getRawData();
		if (rawData == null)
		{
			return null;
		}
		return rawData.failureTrigger;
	}

	// Token: 0x06000050 RID: 80 RVA: 0x000039E7 File Offset: 0x00001BE7
	private void fireTrigger(string trigger, Character target)
	{
		base.processString(trigger, target);
	}

	// Token: 0x06000051 RID: 81 RVA: 0x000039F4 File Offset: 0x00001BF4
	private void fireEffects(List<string> effectIdList, Character user, Character target)
	{
		if (effectIdList == null)
		{
			return;
		}
		foreach (string id in effectIdList)
		{
			Effect effect = GameData.getEffect(id);
			if (effect != null)
			{
				effect.fireEffect(user, target);
			}
		}
	}

	// Token: 0x06000052 RID: 82 RVA: 0x00003A50 File Offset: 0x00001C50
	public SkaldActionResult fireTrigger(Character user, CharacterComponentContainer.EffectSelection areaEffected)
	{
		if (MainControl.debugFunctions)
		{
			MainControl.log(string.Concat(new string[]
			{
				"Trying to fire Triggered Ability ",
				C64Color.WHITE_TAG,
				this.getName(),
				"</color> by ",
				user.getId()
			}));
		}
		if (user == null)
		{
			MainControl.logError("User was null for " + C64Color.WHITE_TAG + this.getId() + " </color>!");
			return new SkaldActionResult(false, false, " Could not perform ABILITY " + this.getName(), true);
		}
		if (areaEffected == null)
		{
			MainControl.logError("Area Effect was null for" + C64Color.WHITE_TAG + this.getId() + " </color>!");
			return new SkaldActionResult(false, false, user.getName() + " could not perform ABILITY " + this.getName(), true);
		}
		SkaldActionResult skaldActionResult = this.canUserUseAbility(user);
		if (!skaldActionResult.wasSuccess())
		{
			if (MainControl.debugFunctions)
			{
				MainControl.log(C64Color.WHITE_TAG + this.getId() + " </color>: " + skaldActionResult.getResultString());
			}
			return skaldActionResult;
		}
		this.payForUse(user);
		bool wasSuccess = false;
		List<Character> allCharactersInSelection = areaEffected.getAllCharactersInSelection();
		string text = string.Concat(new string[]
		{
			user.getName(),
			" used ",
			this.printComponentType(),
			": ",
			this.getName()
		});
		if (allCharactersInSelection.Count != 0)
		{
			text += "\n\nTargets: ";
			foreach (Character character in allCharactersInSelection)
			{
				text = text + character.getNameColored() + ", ";
			}
			text = TextTools.removeTrailingComma(text);
		}
		CombatLog.addEntry(user.getNameColored() + ": " + this.getName(), text);
		foreach (Character target in allCharactersInSelection)
		{
			if (this.isTargetLegal(user, target))
			{
				this.fireUseTriggers(user, target);
				if (this.targetSucceededSave(user, target))
				{
					this.fireFailureTriggers(user, target);
				}
				else
				{
					this.fireSuccessTriggers(user, target);
					wasSuccess = true;
				}
			}
		}
		this.summonCreatures(user, areaEffected);
		this.applyVisualEffects(user, areaEffected);
		return new SkaldActionResult(true, wasSuccess, user.getName() + ": " + this.getName(), text, false);
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00003CBC File Offset: 0x00001EBC
	private void summonCreatures(Character user, CharacterComponentContainer.EffectSelection areaEffected)
	{
		SKALDProjectData.AbilityContainers.ActiveAbility rawData = this.getRawData();
		if (rawData == null)
		{
			return;
		}
		if (rawData.creatureSummoned.Count == 0)
		{
			return;
		}
		MainControl.getDataControl().summonCreature(areaEffected.getMapTiles(), rawData.creatureSummoned, user.isHostile());
	}

	// Token: 0x06000054 RID: 84 RVA: 0x00003D00 File Offset: 0x00001F00
	private void applyVisualEffects(Character user, CharacterComponentContainer.EffectSelection areaEffected)
	{
		user.addPositiveBark(this.getName());
		SKALDProjectData.AbilityContainers.ActiveAbility rawData = this.getRawData();
		if (rawData == null)
		{
			return;
		}
		foreach (MapTile mapTile in areaEffected.getMapTiles())
		{
			mapTile.setMapObject(rawData.tileParticleEffect);
		}
		user.getVisualEffects().setCombatEffectFromString(this.getUserParticleEffect(), user.getTargetOpponent());
		user.setAbilityUseAnimation(this.getUserAnimation());
		AudioControl.playSound(this.getSoundEffect());
	}

	// Token: 0x06000055 RID: 85 RVA: 0x00003D9C File Offset: 0x00001F9C
	protected bool targetSucceededSave(Character user, Character target)
	{
		string userAttribute = this.getUserAttribute();
		string targetAttribute = this.getTargetAttribute();
		if (userAttribute == "" || targetAttribute == "")
		{
			return false;
		}
		SkaldTestRandomVsRandom skaldTestRandomVsRandom = new SkaldTestRandomVsRandom(target.getCurrentAttributeValue(targetAttribute), targetAttribute, user.getCurrentAttributeValue(userAttribute), userAttribute, 1);
		if (MainControl.debugFunctions)
		{
			MainControl.log(string.Concat(new string[]
			{
				"Rolled save for Ability ",
				C64Color.WHITE_TAG,
				this.getId(),
				" </ color >: ",
				skaldTestRandomVsRandom.getReturnString()
			}));
		}
		return skaldTestRandomVsRandom.wasSuccess();
	}

	// Token: 0x06000056 RID: 86 RVA: 0x00003E34 File Offset: 0x00002034
	public virtual bool isTargetLegal(Character user, Character target)
	{
		SKALDProjectData.AbilityContainers.ActiveAbility rawData = this.getRawData();
		if (rawData == null)
		{
			return true;
		}
		if ((!rawData.targetEnemies || !user.isNPCHostile(target)) && (!rawData.targetAllies || user.isNPCHostile(target)))
		{
			if (MainControl.debugFunctions)
			{
				MainControl.log(string.Concat(new string[]
				{
					"Failed Ability ",
					C64Color.WHITE_TAG,
					this.getId(),
					" </color>. Target ",
					target.getId(),
					" is not eligble"
				}));
			}
			return false;
		}
		if (rawData.targetMustHear && target.isDeaf())
		{
			if (MainControl.debugFunctions)
			{
				MainControl.log(string.Concat(new string[]
				{
					"Failed Ability ",
					C64Color.WHITE_TAG,
					this.getId(),
					" </color>. User ",
					target.getId(),
					" is deaf"
				}));
			}
			return false;
		}
		if (rawData.targetMustSee && target.isBlind())
		{
			if (MainControl.debugFunctions)
			{
				MainControl.log(string.Concat(new string[]
				{
					"Failed Ability ",
					C64Color.WHITE_TAG,
					this.getId(),
					" </color>. Target ",
					target.getId(),
					" is blind"
				}));
			}
			return false;
		}
		return true;
	}

	// Token: 0x04000006 RID: 6
	protected static TextureTools.TextureData lockedIcon = TextureTools.loadTextureData("Images/GUIIcons/AbilityIcons/AbilityLocked");
}
