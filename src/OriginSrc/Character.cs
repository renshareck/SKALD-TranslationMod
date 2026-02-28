using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using UnityEngine;

// Token: 0x0200001F RID: 31
[Serializable]
public class Character : SkaldPhysicalObject, ISerializable
{
	// Token: 0x060001C7 RID: 455 RVA: 0x0000A408 File Offset: 0x00008608
	public Character(SKALDProjectData.CharacterContainers.Character npcRawData) : base(npcRawData)
	{
		this.setId(npcRawData.id);
		this.setName(base.processString(npcRawData.title, null));
		this.attributes = new CharacterAttributes(this);
		this.abilityContainerManuver = new AbilityContainerManeuver(this);
		this.abilityContainerPassive = new AbilityContainerPassive(this);
		this.abilityContainerTriggered = new AbilityContainerTriggered(this);
		this.spellContainer = new SpellContainer(this);
		this.temporaryConditionContainer = new ConditionContainer(this);
		this.rawData = npcRawData;
		this.dynamicData = new Character.CharacterSaveData(this.worldPosition, this.coreData, this.instanceData);
		this.dynamicData.isMale = npcRawData.isMale;
		if (this.drawClothing())
		{
			this.dynamicData.characterAnimationControl = new ComplexCharacterAnimationContainer();
		}
		else
		{
			this.dynamicData.characterAnimationControl = new CharacterAnimationContainer();
		}
		this.attributes.setAttributeRank(AttributesControl.CoreAttributes.ATT_Strength, npcRawData.strength);
		this.attributes.setAttributeRank(AttributesControl.CoreAttributes.ATT_Agility, npcRawData.agility);
		this.attributes.setAttributeRank(AttributesControl.CoreAttributes.ATT_Fortitude, npcRawData.fortitude);
		this.attributes.setAttributeRank(AttributesControl.CoreAttributes.ATT_Intellect, npcRawData.intellect);
		this.attributes.setAttributeRank(AttributesControl.CoreAttributes.ATT_Presence, npcRawData.presence);
		this.attributes.setAttributeRank(AttributesControl.CoreAttributes.ATT_Movement, npcRawData.combatMoves);
		this.restoreAllFull();
		this.addCharacterRace(npcRawData.race);
		this.addCharacterClass(npcRawData.classKit);
		this.addCharacterBackground(npcRawData.background);
		this.setImagePath(npcRawData.imagePath);
		this.setModelPath("Models/" + base.processString(npcRawData.modelPath, null));
		this.dynamicData.baseReaction = npcRawData.baseReaction;
		this.dynamicData.relationshipRank = npcRawData.relationshipRank;
		base.setPersistent(npcRawData.persistent);
		this.dynamicData.hostile = npcRawData.hostile;
		this.dynamicData.afraid = npcRawData.afraid;
		this.dynamicData.alert = npcRawData.alert;
		this.setAsleep(npcRawData.asleep);
		this.setMoveMode(npcRawData.moveMode);
		base.setUnique(npcRawData.unique);
		foreach (string factionId in this.rawData.factionList)
		{
			this.addFactionMembership(factionId);
		}
		foreach (string appreancePackId in this.rawData.apperancePackList)
		{
			this.applyApperancePack(appreancePackId);
		}
		this.addFeat(this.rawData.featsList);
		this.addAbilities(this.rawData.abilityList);
		foreach (string id in this.rawData.spellsList)
		{
			this.addSpell(id);
		}
		foreach (string loadoutId in npcRawData.loadoutList)
		{
			GameData.applyLoadoutData(loadoutId, this.getInventory());
		}
		this.getInventory().autoEquip(this);
		if (this.getIdleItem() != null)
		{
			string idleItemAnimation = this.getIdleItem().getIdleItemAnimation();
			if (idleItemAnimation != "")
			{
				this.dynamicData.characterAnimationControl.setIdleItemAnimation(idleItemAnimation);
			}
		}
		if (npcRawData.level > 1)
		{
			this.addLevel(npcRawData.level - 1);
		}
		else
		{
			this.autoDistributeFeatsIfApplicable();
		}
		this.restoreAllFull();
		if (Random.Range(0, 100) < 50)
		{
			this.setFacing(3);
		}
	}

	// Token: 0x060001C8 RID: 456 RVA: 0x0000A818 File Offset: 0x00008A18
	public void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("attributeData", this.attributes, typeof(CharacterAttributes));
		info.AddValue("featData", this.featContainer, typeof(FeatContainer));
		info.AddValue("conditionData", this.temporaryConditionContainer, typeof(ConditionContainer));
		info.AddValue("abilityManeuverData", this.abilityContainerManuver, typeof(AbilityContainerManeuver));
		info.AddValue("abilityTriggeredData", this.abilityContainerTriggered, typeof(AbilityContainerTriggered));
		info.AddValue("abilityPassiveData", this.abilityContainerPassive, typeof(AbilityContainerPassive));
		info.AddValue("spellData", this.spellContainer, typeof(SpellContainer));
		info.AddValue("saveData", this.dynamicData, typeof(Character.CharacterSaveData));
	}

	// Token: 0x060001C9 RID: 457 RVA: 0x0000A900 File Offset: 0x00008B00
	public Character(SerializationInfo info, StreamingContext context)
	{
		this.attributes = (CharacterAttributes)info.GetValue("attributeData", typeof(CharacterAttributes));
		this.attributes.setExternalAttributeData(this);
		this.dynamicData = (Character.CharacterSaveData)info.GetValue("saveData", typeof(Character.CharacterSaveData));
		this.featContainer = (FeatContainer)info.GetValue("featData", typeof(FeatContainer));
		this.temporaryConditionContainer = (ConditionContainer)info.GetValue("conditionData", typeof(ConditionContainer));
		this.temporaryConditionContainer.setOwner(this);
		this.abilityContainerManuver = (AbilityContainerManeuver)info.GetValue("abilityManeuverData", typeof(AbilityContainerManeuver));
		this.abilityContainerManuver.setOwner(this);
		this.abilityContainerTriggered = (AbilityContainerTriggered)info.GetValue("abilityTriggeredData", typeof(AbilityContainerTriggered));
		this.abilityContainerTriggered.setOwner(this);
		this.abilityContainerPassive = (AbilityContainerPassive)info.GetValue("abilityPassiveData", typeof(AbilityContainerPassive));
		this.abilityContainerPassive.setOwner(this);
		this.spellContainer = (SpellContainer)info.GetValue("spellData", typeof(SpellContainer));
		this.spellContainer.setOwner(this);
		this.applySaveData(this.dynamicData);
	}

	// Token: 0x060001CA RID: 458 RVA: 0x0000AA98 File Offset: 0x00008C98
	private bool drawClothing()
	{
		SKALDProjectData.CharacterContainers.Character character = this.getRawData();
		return character == null || character.drawClothing;
	}

	// Token: 0x060001CB RID: 459 RVA: 0x0000AAB7 File Offset: 0x00008CB7
	public void clearNegativeConditions()
	{
		this.getConditionContainer().clearNegativeConditions();
	}

	// Token: 0x060001CC RID: 460 RVA: 0x0000AAC4 File Offset: 0x00008CC4
	public void clearAllCharacterData()
	{
		this.attributes = new CharacterAttributes(this);
		this.spellContainer = new SpellContainer(this);
		this.abilityContainerManuver = new AbilityContainerManeuver(this);
		this.abilityContainerTriggered = new AbilityContainerTriggered(this);
		this.abilityContainerPassive = new AbilityContainerPassive(this);
		this.featContainer = new FeatContainer();
		this.temporaryConditionContainer = new ConditionContainer(this);
		this.dynamicData.combatAbilityFlags = new Character.CombatAbilityFlags();
		this.dynamicData.developmentPoints = GameData.getStartingDP();
		this.dynamicData.characterClass = "";
		this.addCharacterRace("RAC_Human");
		this.dynamicData.background = "";
	}

	// Token: 0x060001CD RID: 461 RVA: 0x0000AB70 File Offset: 0x00008D70
	public void applyApperancePack(string appreancePackId)
	{
		SKALDProjectData.Objects.ApperanceDataContainer.ApperanceData apperanceData = GameData.getApperanceData(appreancePackId);
		if (apperanceData == null)
		{
			return;
		}
		if (this.isCharacterMale())
		{
			if (apperanceData.idleAnim != "")
			{
				this.setBaseAnimation(base.processString(apperanceData.idleAnim, null));
			}
		}
		else if (apperanceData.femaleIdleAnim != "")
		{
			this.setBaseAnimation(base.processString(apperanceData.femaleIdleAnim, null));
		}
		else if (apperanceData.idleAnim != "")
		{
			this.setBaseAnimation(base.processString(apperanceData.idleAnim, null));
		}
		if (this.drawClothing())
		{
			if (this.isCharacterMale())
			{
				string text = SkaldBaseObject.processStringFromOrList(apperanceData.hairStyles, null);
				if (text != "")
				{
					this.getLooksControl().setHairStyleId(text);
				}
				string text2 = SkaldBaseObject.processStringFromOrList(apperanceData.beardStyles, null);
				if (text2 != "")
				{
					this.getLooksControl().setBeardStyleId(text2);
				}
			}
			else
			{
				string text3 = SkaldBaseObject.processStringFromOrList(apperanceData.femaleHairStyles, null);
				if (text3 != "")
				{
					this.getLooksControl().setHairStyleId(text3);
				}
				else
				{
					text3 = SkaldBaseObject.processStringFromOrList(apperanceData.hairStyles, null);
					if (text3 != "")
					{
						this.getLooksControl().setHairStyleId(text3);
					}
				}
			}
		}
		string portraitPath = apperanceData.portraitPath;
		if (portraitPath != "")
		{
			this.getLooksControl().setPortraitId(portraitPath);
		}
		if (this.paletteSwap())
		{
			string text4 = SkaldBaseObject.processStringFromOrList(apperanceData.mainColors, null);
			if (text4 != "")
			{
				this.getLooksControl().setMainColor(text4);
			}
			string text5 = SkaldBaseObject.processStringFromOrList(apperanceData.secColors, null);
			if (text5 != "")
			{
				this.getLooksControl().setSecondaryColor(text5);
			}
			string text6 = SkaldBaseObject.processStringFromOrList(apperanceData.tertiaryColors, null);
			if (text6 != "")
			{
				this.getLooksControl().setTertiaryColor(text6);
			}
			string text7 = SkaldBaseObject.processStringFromOrList(apperanceData.skinColors, null);
			if (text7 != "")
			{
				this.getLooksControl().setSkinColor(text7);
			}
			string text8 = SkaldBaseObject.processStringFromOrList(apperanceData.hairColors, null);
			if (text8 != "")
			{
				this.getLooksControl().setHairColor(text8);
			}
		}
	}

	// Token: 0x060001CE RID: 462 RVA: 0x0000ADB0 File Offset: 0x00008FB0
	public bool isSummoned()
	{
		return this.summoned;
	}

	// Token: 0x060001CF RID: 463 RVA: 0x0000ADB8 File Offset: 0x00008FB8
	public void setSummoned()
	{
		this.summoned = true;
		base.getVisualEffects().setSummoned();
		this.setAlert();
		this.setSpotted();
	}

	// Token: 0x060001D0 RID: 464 RVA: 0x0000ADD8 File Offset: 0x00008FD8
	public void addToRelationshipRank(int amount)
	{
		this.dynamicData.relationshipRank += amount;
	}

	// Token: 0x060001D1 RID: 465 RVA: 0x0000ADF0 File Offset: 0x00008FF0
	public string printRelationshipRank()
	{
		string text = this.getName() + " Relationship Rank:\t" + this.dynamicData.relationshipRank.ToString();
		if (this.isInLove())
		{
			text += "[In Love]";
		}
		return text;
	}

	// Token: 0x060001D2 RID: 466 RVA: 0x0000AE33 File Offset: 0x00009033
	public bool isInLove()
	{
		return this.dynamicData.relationshipRank >= 10;
	}

	// Token: 0x060001D3 RID: 467 RVA: 0x0000AE48 File Offset: 0x00009048
	public void levelScale()
	{
		if (this.isPC())
		{
			return;
		}
		SKALDProjectData.CharacterContainers.Character character = this.getRawData();
		if (character == null)
		{
			return;
		}
		int num = MainControl.getDataControl().getMainCharacter().getLevel() + character.levelOffset;
		if (num > character.maxLevel)
		{
			num = character.maxLevel;
		}
		if (this.getLevel() < num)
		{
			if (MainControl.debugFunctions)
			{
				MainControl.log(string.Concat(new string[]
				{
					"Level Scaling ",
					this.getId(),
					" from ",
					this.getLevel().ToString(),
					" to ",
					num.ToString(),
					". Offset: ",
					character.levelOffset.ToString(),
					", Max Level: ",
					character.maxLevel.ToString()
				}));
			}
			this.addLevel(num - this.getLevel());
		}
	}

	// Token: 0x060001D4 RID: 468 RVA: 0x0000AF2B File Offset: 0x0000912B
	public void setBaseAnimation(string animationId)
	{
		if (animationId == "")
		{
			animationId = "ANI_BaseAlert";
		}
		this.dynamicData.characterAnimationControl.setBaseAnimation(animationId);
		this.dynamicData.characterAnimationControl.setBaseNotAlertAnimation(animationId);
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x0000AF63 File Offset: 0x00009163
	public void increaseThieverySuspicion(int amount)
	{
		this.dynamicData.thieverySuspicion += amount;
	}

	// Token: 0x060001D6 RID: 470 RVA: 0x0000AF78 File Offset: 0x00009178
	public int getThieverySuspicion()
	{
		return this.dynamicData.thieverySuspicion;
	}

	// Token: 0x060001D7 RID: 471 RVA: 0x0000AF85 File Offset: 0x00009185
	public int getStaticThieveryAwareness()
	{
		return this.getCurrentAttributeValueStatic(AttributesControl.CoreAttributes.ATT_Awareness) + this.getThieverySuspicion();
	}

	// Token: 0x060001D8 RID: 472 RVA: 0x0000AF98 File Offset: 0x00009198
	public string printFeedbackData()
	{
		string text = this.getName();
		if (this.dynamicData.race != null)
		{
			text = string.Concat(new string[]
			{
				text,
				" / ",
				this.printGender(),
				" ",
				this.getRace().getName()
			});
		}
		if (this.dynamicData.characterClass != null)
		{
			text = string.Concat(new string[]
			{
				text,
				" / Level ",
				this.getLevel().ToString(),
				" ",
				this.getClassName()
			});
		}
		return text;
	}

	// Token: 0x060001D9 RID: 473 RVA: 0x0000B038 File Offset: 0x00009238
	public void setWading(bool isWading)
	{
		this.wading = isWading;
	}

	// Token: 0x060001DA RID: 474 RVA: 0x0000B044 File Offset: 0x00009244
	public void addAbilities(List<string> abilities)
	{
		if (abilities == null)
		{
			return;
		}
		foreach (string id in abilities)
		{
			this.addAbility(GameData.getAbility(id));
		}
	}

	// Token: 0x060001DB RID: 475 RVA: 0x0000B09C File Offset: 0x0000929C
	public int getGoldDropBonus()
	{
		return this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_GoldDropBonus);
	}

	// Token: 0x060001DC RID: 476 RVA: 0x0000B0AC File Offset: 0x000092AC
	public void addAbilities(List<Ability> abilities)
	{
		if (abilities == null)
		{
			return;
		}
		foreach (Ability ability in abilities)
		{
			this.addAbility(ability);
		}
	}

	// Token: 0x060001DD RID: 477 RVA: 0x0000B100 File Offset: 0x00009300
	public void addAbility(Ability ability)
	{
		if (ability == null)
		{
			return;
		}
		if (ability is AbilityCombatManeuver)
		{
			this.getAbilityManueverContainer().addAbility(ability as AbilityCombatManeuver, this);
			return;
		}
		if (ability is AbilityPassive)
		{
			this.getAbilityPassiveContainer().addAbility(ability as AbilityPassive, this);
			return;
		}
		if (ability is AbilityTriggered)
		{
			this.getAbilityTriggeredContainer().addAbility(ability as AbilityTriggered, this);
			return;
		}
		if (ability is AbilitySpell)
		{
			this.getSpellContainer().addSpell(ability as AbilitySpell);
		}
	}

	// Token: 0x060001DE RID: 478 RVA: 0x0000B17C File Offset: 0x0000937C
	public string printAbilityData()
	{
		return "" + "Conditions: " + this.getConditionContainer().printListSingleLine() + "\nSpells: " + this.getSpellContainer().printListSingleLine() + "\nManeuvers: " + this.getAbilityManueverContainer().printListSingleLine() + "\nPassive: " + this.getAbilityPassiveContainer().printListSingleLine() + "\nTriggered: " + this.getAbilityTriggeredContainer().printListSingleLine();
	}

	// Token: 0x060001DF RID: 479 RVA: 0x0000B1F7 File Offset: 0x000093F7
	public bool isHidden()
	{
		return this.getConditionContainer().isInvisible() || (this.dynamicData.hidden && this.dynamicData.hiddenDegree > 0);
	}

	// Token: 0x060001E0 RID: 480 RVA: 0x0000B225 File Offset: 0x00009425
	public int getRadiusBonusAura()
	{
		return this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_SpellRadiusAura);
	}

	// Token: 0x060001E1 RID: 481 RVA: 0x0000B22F File Offset: 0x0000942F
	public int getRadiusBonusLine()
	{
		return this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_SpellRadiusLine);
	}

	// Token: 0x060001E2 RID: 482 RVA: 0x0000B239 File Offset: 0x00009439
	public int getRadiusBonusSphere()
	{
		return this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_SpellRadiusSphere);
	}

	// Token: 0x060001E3 RID: 483 RVA: 0x0000B243 File Offset: 0x00009443
	public void toggleHidden()
	{
		if (this.dynamicData.hidden)
		{
			this.clearHidden();
			return;
		}
		this.hideCharacter();
	}

	// Token: 0x060001E4 RID: 484 RVA: 0x0000B25F File Offset: 0x0000945F
	public void hideCharacter()
	{
		if (this.dynamicData.hidden)
		{
			return;
		}
		AudioControl.playSound("StealthActivate1");
		this.dynamicData.hidden = true;
	}

	// Token: 0x060001E5 RID: 485 RVA: 0x0000B285 File Offset: 0x00009485
	public void hideInCombat()
	{
		if (this.isHidden())
		{
			return;
		}
		this.resetHiddenDegree();
		this.hideCharacter();
	}

	// Token: 0x060001E6 RID: 486 RVA: 0x0000B29C File Offset: 0x0000949C
	public void clearHidden()
	{
		this.dynamicData.hidden = false;
		this.getConditionContainer().purgeInvisibility();
	}

	// Token: 0x060001E7 RID: 487 RVA: 0x0000B2B5 File Offset: 0x000094B5
	public int getHiddenDegree()
	{
		return this.dynamicData.hiddenDegree;
	}

	// Token: 0x060001E8 RID: 488 RVA: 0x0000B2C4 File Offset: 0x000094C4
	private void adjustHiddenDegree(int ammount)
	{
		this.dynamicData.hiddenDegree = Mathf.Clamp(this.dynamicData.hiddenDegree += ammount, 0, 100);
		if (this.getConditionContainer().isInvisible())
		{
			this.resetHiddenDegree();
			return;
		}
		if (this.getHiddenDegree() == 0)
		{
			this.clearHidden();
		}
	}

	// Token: 0x060001E9 RID: 489 RVA: 0x0000B31C File Offset: 0x0000951C
	public void resetHiddenDegree()
	{
		this.dynamicData.hiddenDegree = 100;
	}

	// Token: 0x060001EA RID: 490 RVA: 0x0000B32B File Offset: 0x0000952B
	public void recoverHiddenDegree()
	{
		if (this.isBeingObserved())
		{
			return;
		}
		this.adjustHiddenDegree(5 + this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_Stealth) * 2);
	}

	// Token: 0x060001EB RID: 491 RVA: 0x0000B348 File Offset: 0x00009548
	public bool isBusy()
	{
		return this.getRawData().busy;
	}

	// Token: 0x060001EC RID: 492 RVA: 0x0000B355 File Offset: 0x00009555
	public void fireApproachTrigger()
	{
		if (!this.dynamicData.approached)
		{
			base.processString(this.getRawData().approachTrigger, this);
			this.dynamicData.approached = true;
		}
	}

	// Token: 0x060001ED RID: 493 RVA: 0x0000B383 File Offset: 0x00009583
	public void setHiddenDegree(int ammount)
	{
		this.dynamicData.hiddenDegree = Mathf.Clamp(ammount, 0, 100);
		if (this.getHiddenDegree() == 0)
		{
			this.clearHidden();
		}
	}

	// Token: 0x060001EE RID: 494 RVA: 0x0000B3A8 File Offset: 0x000095A8
	public void attemptToSneak(List<Character> observers)
	{
		foreach (Character observer in observers)
		{
			this.attemptToSneak(observer);
		}
	}

	// Token: 0x060001EF RID: 495 RVA: 0x0000B3F8 File Offset: 0x000095F8
	public void attemptToSneak(Character observer)
	{
		if (!observer.getMapTile().isSpotted())
		{
			return;
		}
		int stealthBonus = base.getMapTile().getStealthBonus();
		int num = 0;
		float linearDistance = NavigationTools.getLinearDistance(this, observer);
		if (linearDistance == 1f)
		{
			num = -6;
		}
		else if (linearDistance < 2f)
		{
			num = -3;
		}
		else if (linearDistance > 5f)
		{
			num = 2;
		}
		SkaldTestBase skaldTestBase = observer.testAwarenessStatic(this.getStealthModified() + stealthBonus + num);
		int num2 = 0;
		if (!observer.isAsleep())
		{
			num2 = new DicePoolVariable(1, 3).getResult();
		}
		if (skaldTestBase.getDegreeOfResult() > 0)
		{
			num2 += skaldTestBase.getDegreeOfResult() * 4;
		}
		if (MainControl.debugFunctions)
		{
			MainControl.log(string.Concat(new string[]
			{
				this.getId(),
				" SNEAKING \n\nTile mod: ",
				stealthBonus.ToString(),
				"\nDist. mod: ",
				num.ToString(),
				"\nResult: -",
				num2.ToString()
			}));
		}
		this.adjustHiddenDegree(-num2);
	}

	// Token: 0x060001F0 RID: 496 RVA: 0x0000B4EC File Offset: 0x000096EC
	public CharacterClass getClass()
	{
		return GameData.getClass(this.dynamicData.characterClass);
	}

	// Token: 0x060001F1 RID: 497 RVA: 0x0000B4FE File Offset: 0x000096FE
	public string getClassName()
	{
		return GameData.getClass(this.dynamicData.characterClass).getName();
	}

	// Token: 0x060001F2 RID: 498 RVA: 0x0000B515 File Offset: 0x00009715
	private CharacterRace getRace()
	{
		return GameData.getRace(this.dynamicData.race);
	}

	// Token: 0x060001F3 RID: 499 RVA: 0x0000B527 File Offset: 0x00009727
	public CharacterBackground getBackground()
	{
		if (this.dynamicData.background == "")
		{
			return null;
		}
		return GameData.getBackground(this.dynamicData.background);
	}

	// Token: 0x060001F4 RID: 500 RVA: 0x0000B552 File Offset: 0x00009752
	private Character.ItemSlots getItemSlots()
	{
		if (this.itemSlots == null)
		{
			this.itemSlots = new Character.ItemSlots(this.getInventory(), this);
		}
		return this.itemSlots;
	}

	// Token: 0x060001F5 RID: 501 RVA: 0x0000B574 File Offset: 0x00009774
	private BarkControl getBarkControl()
	{
		if (this.barkControl == null)
		{
			this.barkControl = new BarkControl(8, 20);
		}
		return this.barkControl;
	}

	// Token: 0x060001F6 RID: 502 RVA: 0x0000B592 File Offset: 0x00009792
	public void setTacticalHoverText(string message)
	{
		if (this.isPC())
		{
			this.tacticalHoverTextBuffer = message;
		}
	}

	// Token: 0x060001F7 RID: 503 RVA: 0x0000B5A3 File Offset: 0x000097A3
	public string getAndClearTacticalHoverText()
	{
		string result = this.tacticalHoverTextBuffer;
		this.tacticalHoverTextBuffer = "";
		return result;
	}

	// Token: 0x060001F8 RID: 504 RVA: 0x0000B5B6 File Offset: 0x000097B6
	public void addVocalBarkDelayed(string message, int delay)
	{
		this.getBarkControl().addVocalBark(message, delay);
	}

	// Token: 0x060001F9 RID: 505 RVA: 0x0000B5C5 File Offset: 0x000097C5
	public void addVocalBarkLocal(string message)
	{
		this.addVocalBarkDelayed(message, 0);
	}

	// Token: 0x060001FA RID: 506 RVA: 0x0000B5D0 File Offset: 0x000097D0
	public void barkListEntryLocal(string listId)
	{
		string randomStringListEntry = GameData.getRandomStringListEntry(listId);
		if (randomStringListEntry != "")
		{
			this.addVocalBarkLocal(base.processString(randomStringListEntry, null));
		}
	}

	// Token: 0x060001FB RID: 507 RVA: 0x0000B5FF File Offset: 0x000097FF
	public void addPositiveBark(string message, int delay)
	{
		this.getBarkControl().addPositiveBark(message, delay);
	}

	// Token: 0x060001FC RID: 508 RVA: 0x0000B60E File Offset: 0x0000980E
	public void addPositiveBark(string message)
	{
		this.addPositiveBark(message, 0);
	}

	// Token: 0x060001FD RID: 509 RVA: 0x0000B618 File Offset: 0x00009818
	public void addNegativeBark(string message)
	{
		this.addNegativeBark(message, 0);
	}

	// Token: 0x060001FE RID: 510 RVA: 0x0000B622 File Offset: 0x00009822
	public void addInfoBark(string message)
	{
		this.addInfoBark(message, 0);
	}

	// Token: 0x060001FF RID: 511 RVA: 0x0000B62C File Offset: 0x0000982C
	public void addNegativeBark(string message, int delay)
	{
		this.getBarkControl().addNegativeBark(message, delay);
	}

	// Token: 0x06000200 RID: 512 RVA: 0x0000B63B File Offset: 0x0000983B
	public void addInfoBark(string message, int delay)
	{
		this.getBarkControl().addInfoBark(message, delay);
	}

	// Token: 0x06000201 RID: 513 RVA: 0x0000B64A File Offset: 0x0000984A
	public List<TextureTools.Sprite> getBarkSprites()
	{
		return this.getBarkControl().getBarkSprites();
	}

	// Token: 0x06000202 RID: 514 RVA: 0x0000B657 File Offset: 0x00009857
	public string getHalfLifeTrigger()
	{
		return base.processString(this.getRawData().halfLifeTrigger, this);
	}

	// Token: 0x06000203 RID: 515 RVA: 0x0000B66B File Offset: 0x0000986B
	public string getIsHitTrigger()
	{
		return base.processString(this.getRawData().isHitTrigger, this);
	}

	// Token: 0x06000204 RID: 516 RVA: 0x0000B67F File Offset: 0x0000987F
	public string getBecomeFriendlyTrigger()
	{
		return base.processString(this.getRawData().becomeFriendlyTrigger, this);
	}

	// Token: 0x06000205 RID: 517 RVA: 0x0000B693 File Offset: 0x00009893
	public string getBecomeUnfriendlyTrigger()
	{
		return base.processString(this.getRawData().becomeUnfriendlyTrigger, this);
	}

	// Token: 0x06000206 RID: 518 RVA: 0x0000B6A8 File Offset: 0x000098A8
	public bool isPaperDoll()
	{
		SKALDProjectData.CharacterContainers.Character character = this.getRawData();
		return character == null || character.paperDoll;
	}

	// Token: 0x06000207 RID: 519 RVA: 0x0000B6C7 File Offset: 0x000098C7
	public bool isSpotted()
	{
		if (this.isPC())
		{
			this.dynamicData.isSpotted = true;
		}
		return this.dynamicData.isSpotted;
	}

	// Token: 0x06000208 RID: 520 RVA: 0x0000B6E8 File Offset: 0x000098E8
	public void setSpotted()
	{
		if (this.dynamicData.isSpotted)
		{
			return;
		}
		this.dynamicData.isSpotted = true;
		base.processString(this.getRawData().spottedTrigger, this);
	}

	// Token: 0x06000209 RID: 521 RVA: 0x0000B718 File Offset: 0x00009918
	public bool isRecruitable()
	{
		SKALDProjectData.CharacterContainers.Character character = this.getRawData();
		return character != null && character.recruitable;
	}

	// Token: 0x0600020A RID: 522 RVA: 0x0000B738 File Offset: 0x00009938
	public bool canAttack()
	{
		SKALDProjectData.CharacterContainers.Character character = this.getRawData();
		return character == null || character.canAttack;
	}

	// Token: 0x0600020B RID: 523 RVA: 0x0000B758 File Offset: 0x00009958
	public bool willTrade()
	{
		SKALDProjectData.CharacterContainers.Character character = this.getRawData();
		return character != null && character.willTrade;
	}

	// Token: 0x0600020C RID: 524 RVA: 0x0000B778 File Offset: 0x00009978
	public bool isFirable()
	{
		SKALDProjectData.CharacterContainers.Character character = this.getRawData();
		return character == null || character.fireable;
	}

	// Token: 0x0600020D RID: 525 RVA: 0x0000B797 File Offset: 0x00009997
	public FactionControl.FactionRelationships getFactionRelationships()
	{
		if (this.dynamicData.factionRelationships == null)
		{
			this.dynamicData.factionRelationships = new FactionControl.FactionRelationships(this);
		}
		return this.dynamicData.factionRelationships;
	}

	// Token: 0x0600020E RID: 526 RVA: 0x0000B7C4 File Offset: 0x000099C4
	private bool paletteSwap()
	{
		SKALDProjectData.CharacterContainers.Character character = this.getRawData();
		return character == null || character.paletteSwap;
	}

	// Token: 0x0600020F RID: 527 RVA: 0x0000B7E4 File Offset: 0x000099E4
	public int getTileWidth()
	{
		SKALDProjectData.CharacterContainers.Character character = this.getRawData();
		int num = 1;
		if (character != null)
		{
			num = character.tileWidth;
		}
		if (num < 1)
		{
			num = 1;
		}
		return num;
	}

	// Token: 0x06000210 RID: 528 RVA: 0x0000B80C File Offset: 0x00009A0C
	public int getTileHeight()
	{
		SKALDProjectData.CharacterContainers.Character character = this.getRawData();
		int num = 1;
		if (character != null)
		{
			num = character.tileHeight;
		}
		if (num < 1)
		{
			num = 1;
		}
		return num;
	}

	// Token: 0x06000211 RID: 529 RVA: 0x0000B833 File Offset: 0x00009A33
	public NavigationCourse GetNavigationCourse()
	{
		return this.course;
	}

	// Token: 0x06000212 RID: 530 RVA: 0x0000B83B File Offset: 0x00009A3B
	public void clearNavigationCourse()
	{
		if (this.course != null)
		{
			this.course.clearCourse();
		}
	}

	// Token: 0x06000213 RID: 531 RVA: 0x0000B850 File Offset: 0x00009A50
	public void holdAction()
	{
		this.holdingAction = true;
		this.hasHeldActionThisTurn = true;
	}

	// Token: 0x06000214 RID: 532 RVA: 0x0000B860 File Offset: 0x00009A60
	public bool isHoldingAction()
	{
		return this.holdingAction;
	}

	// Token: 0x06000215 RID: 533 RVA: 0x0000B868 File Offset: 0x00009A68
	public void clearBeingObserved()
	{
		this.dynamicData.isBeingObserved = false;
	}

	// Token: 0x06000216 RID: 534 RVA: 0x0000B876 File Offset: 0x00009A76
	public void setBeingObserved()
	{
		this.dynamicData.isBeingObserved = true;
	}

	// Token: 0x06000217 RID: 535 RVA: 0x0000B884 File Offset: 0x00009A84
	public bool isBeingObserved()
	{
		return this.dynamicData.isBeingObserved;
	}

	// Token: 0x06000218 RID: 536 RVA: 0x0000B891 File Offset: 0x00009A91
	public bool navigationCourseHasNodes()
	{
		return this.course != null && this.course.hasNodes();
	}

	// Token: 0x06000219 RID: 537 RVA: 0x0000B8A8 File Offset: 0x00009AA8
	public int getStealthModified()
	{
		return this.getCurrentAttributeValueStatic(AttributesControl.CoreAttributes.ATT_Stealth);
	}

	// Token: 0x0600021A RID: 538 RVA: 0x0000B8B2 File Offset: 0x00009AB2
	public SkaldTestRandomVsStatic testAwarenessStatic(int DC)
	{
		return new SkaldTestRandomVsStatic(this, AttributesControl.CoreAttributes.ATT_Awareness, DC, 1);
	}

	// Token: 0x0600021B RID: 539 RVA: 0x0000B8BE File Offset: 0x00009ABE
	public void setNavigationCourse(NavigationCourse c)
	{
		this.course = c;
	}

	// Token: 0x0600021C RID: 540 RVA: 0x0000B8C7 File Offset: 0x00009AC7
	public Point popNavigationNode()
	{
		if (this.course == null)
		{
			return new Point(this.getTileX(), this.getTileY());
		}
		return this.course.popFirstNode();
	}

	// Token: 0x0600021D RID: 541 RVA: 0x0000B8EE File Offset: 0x00009AEE
	public void applyLoadout(string loadoutId)
	{
		GameData.applyLoadoutData(loadoutId, this.getInventory());
	}

	// Token: 0x0600021E RID: 542 RVA: 0x0000B900 File Offset: 0x00009B00
	public Store getStore()
	{
		if (this.dynamicData.store == null)
		{
			string text = base.processString(this.getRawData().store, null);
			if (text != null && text != "")
			{
				this.dynamicData.store = GameData.getStore(base.processString(text, null), this);
			}
			if (this.dynamicData.store == null)
			{
				this.dynamicData.store = new Store(this);
			}
		}
		this.dynamicData.store.setOwner(this);
		return this.dynamicData.store;
	}

	// Token: 0x0600021F RID: 543 RVA: 0x0000B990 File Offset: 0x00009B90
	public void setFacing(int _facing)
	{
		if (this.isDead())
		{
			return;
		}
		this.dynamicData.facing = _facing;
		if (this.dynamicData.facing < 0 || this.dynamicData.facing > 3)
		{
			this.dynamicData.facing = 0;
		}
		if (this.dynamicData.facing == 1)
		{
			this.dynamicData.spriteFacesRight = true;
		}
		if (this.dynamicData.facing == 3)
		{
			this.dynamicData.spriteFacesRight = false;
		}
	}

	// Token: 0x06000220 RID: 544 RVA: 0x0000BA10 File Offset: 0x00009C10
	public bool shouldTurnToFacePC()
	{
		return !this.isBusy() && this.isAlert() && this.dynamicData.moveMode != Character.MoveMode.PatrolCCW && this.dynamicData.moveMode != Character.MoveMode.PatrolCW && this.dynamicData.moveMode != Character.MoveMode.PatrolNS && this.dynamicData.moveMode != Character.MoveMode.PatrolEW;
	}

	// Token: 0x06000221 RID: 545 RVA: 0x0000BA6D File Offset: 0x00009C6D
	public int getFacing()
	{
		return this.dynamicData.facing;
	}

	// Token: 0x06000222 RID: 546 RVA: 0x0000BA7A File Offset: 0x00009C7A
	public void setAsleep(bool asleep)
	{
		if (asleep)
		{
			this.addConditionToCharacter("CON_Asleep");
			return;
		}
		this.removeCondition(new List<string>
		{
			"CON_Asleep"
		});
	}

	// Token: 0x06000223 RID: 547 RVA: 0x0000BAA1 File Offset: 0x00009CA1
	public bool isAsleep()
	{
		return this.getConditionContainer().isAsleep();
	}

	// Token: 0x06000224 RID: 548 RVA: 0x0000BAAE File Offset: 0x00009CAE
	public bool isAlert()
	{
		return !this.isAsleep() && (this.isPC() || this.dynamicData.alert);
	}

	// Token: 0x06000225 RID: 549 RVA: 0x0000BACF File Offset: 0x00009CCF
	public bool isAfraid()
	{
		return this.dynamicData.afraid;
	}

	// Token: 0x06000226 RID: 550 RVA: 0x0000BADC File Offset: 0x00009CDC
	public bool isArmed()
	{
		return this.getCurrentWeapon() != null;
	}

	// Token: 0x06000227 RID: 551 RVA: 0x0000BAE9 File Offset: 0x00009CE9
	public void setAlert()
	{
		if (this.isAsleep())
		{
			this.setAsleep(false);
		}
		if (!this.isAlert())
		{
			if (this.isHostile())
			{
				this.addInfoBark("Alerted");
			}
			this.dynamicData.alert = true;
			this.processAlertTrigger();
		}
	}

	// Token: 0x06000228 RID: 552 RVA: 0x0000BB28 File Offset: 0x00009D28
	public void setGradualAlert()
	{
		if (this.isAlert())
		{
			return;
		}
		if (this.dynamicData.alertGrace > 0)
		{
			Character.CharacterSaveData characterSaveData = this.dynamicData;
			characterSaveData.alertGrace -= 1;
			if (this.isHostile())
			{
				this.addInfoBark("?");
			}
			return;
		}
		if (this.isAsleep())
		{
			this.setAsleep(false);
			return;
		}
		this.setAlert();
	}

	// Token: 0x06000229 RID: 553 RVA: 0x0000BB8A File Offset: 0x00009D8A
	public void clearAlert()
	{
		if (this.isAlert())
		{
			this.dynamicData.alertGrace = 2;
			this.dynamicData.alert = false;
			this.processClearAlertTrigger();
		}
	}

	// Token: 0x0600022A RID: 554 RVA: 0x0000BBB3 File Offset: 0x00009DB3
	public bool isStealLocked()
	{
		return this.dynamicData.stealLocked;
	}

	// Token: 0x0600022B RID: 555 RVA: 0x0000BBC0 File Offset: 0x00009DC0
	public void setStealLock(bool value)
	{
		this.dynamicData.stealLocked = value;
	}

	// Token: 0x0600022C RID: 556 RVA: 0x0000BBCE File Offset: 0x00009DCE
	public void setFlanked(bool f)
	{
		this.flanked = f;
	}

	// Token: 0x0600022D RID: 557 RVA: 0x0000BBD8 File Offset: 0x00009DD8
	public void setNearbyAllyCount(int nearbyAllies)
	{
		int phalanxDodgeBonus = this.getPhalanxDodgeBonus();
		this.nearbyAllies = nearbyAllies;
		if (this.isPC() && this.getPhalanxDodgeBonus() > 0 && this.getPhalanxDodgeBonus() != phalanxDodgeBonus)
		{
			this.addPositiveBark("Phalanx: " + this.getPhalanxDodgeBonus().ToString() + " Dodge");
		}
	}

	// Token: 0x0600022E RID: 558 RVA: 0x0000BC30 File Offset: 0x00009E30
	public int getPhalanxDodgeBonus()
	{
		if (this.getCurrentShieldIfInHand() == null)
		{
			return 0;
		}
		int currentAttributeValue = this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_PhalanxBonus);
		int result = this.nearbyAllies;
		if (this.nearbyAllies > currentAttributeValue)
		{
			result = currentAttributeValue;
		}
		return result;
	}

	// Token: 0x0600022F RID: 559 RVA: 0x0000BC64 File Offset: 0x00009E64
	public bool isDeserter()
	{
		SKALDProjectData.CharacterContainers.Character character = this.getRawData();
		return character == null || character.deserts;
	}

	// Token: 0x06000230 RID: 560 RVA: 0x0000BC84 File Offset: 0x00009E84
	public string processTalkTrigger()
	{
		string talkTrigger = this.getRawData().talkTrigger;
		string firstTimeTalkTrigger = this.getRawData().firstTimeTalkTrigger;
		if (!this.dynamicData.hasTalkedToPlayer)
		{
			this.dynamicData.hasTalkedToPlayer = true;
			if (firstTimeTalkTrigger != "")
			{
				return base.processString(firstTimeTalkTrigger, this);
			}
		}
		return base.processString(talkTrigger, this);
	}

	// Token: 0x06000231 RID: 561 RVA: 0x0000BCE0 File Offset: 0x00009EE0
	public bool hasTalkTrigger()
	{
		string talkTrigger = this.getRawData().talkTrigger;
		string firstTimeTalkTrigger = this.getRawData().firstTimeTalkTrigger;
		return (!this.dynamicData.hasTalkedToPlayer && firstTimeTalkTrigger != "") || talkTrigger != "";
	}

	// Token: 0x06000232 RID: 562 RVA: 0x0000BD31 File Offset: 0x00009F31
	public bool isFlanked()
	{
		return this.flanked;
	}

	// Token: 0x06000233 RID: 563 RVA: 0x0000BD39 File Offset: 0x00009F39
	public bool isPassing()
	{
		return this.passing;
	}

	// Token: 0x06000234 RID: 564 RVA: 0x0000BD41 File Offset: 0x00009F41
	public void addConditionToCharacter(string conditionId)
	{
		this.getConditionContainer().addCondition(conditionId, this);
	}

	// Token: 0x06000235 RID: 565 RVA: 0x0000BD50 File Offset: 0x00009F50
	public void addConditionToCharacterFromList(List<string> conditionIdList)
	{
		foreach (string conditionId in conditionIdList)
		{
			this.getConditionContainer().addCondition(conditionId, this);
		}
	}

	// Token: 0x06000236 RID: 566 RVA: 0x0000BDA4 File Offset: 0x00009FA4
	public void removeCondition(List<string> conditionIdList)
	{
		foreach (string id in conditionIdList)
		{
			this.getConditionContainer().deleteComponent(id);
		}
	}

	// Token: 0x06000237 RID: 567 RVA: 0x0000BDF8 File Offset: 0x00009FF8
	public void removeBaseConditions(List<string> conditionIdList)
	{
		foreach (string baseCondition in conditionIdList)
		{
			this.getConditionContainer().deleteByBaseConditionCaused(baseCondition);
		}
	}

	// Token: 0x06000238 RID: 568 RVA: 0x0000BE4C File Offset: 0x0000A04C
	public float getLightStrength()
	{
		float num = 0f;
		if (this.getCurrentLight() != null && !this.isHidden())
		{
			num = 1f;
		}
		if (num != 1f)
		{
			num = this.getConditionContainer().getLightDegree();
		}
		return num;
	}

	// Token: 0x06000239 RID: 569 RVA: 0x0000BE8C File Offset: 0x0000A08C
	public int getLightDistance()
	{
		int num = this.getConditionContainer().getLightRadius();
		if (this.getCurrentLight() != null && num < 3)
		{
			num = 3;
		}
		return num;
	}

	// Token: 0x0600023A RID: 570 RVA: 0x0000BEB4 File Offset: 0x0000A0B4
	public string getLightImage()
	{
		string text = this.getConditionContainer().getLightImage();
		if (text == "")
		{
			text = "Round2Yellow";
		}
		return text;
	}

	// Token: 0x0600023B RID: 571 RVA: 0x0000BEE1 File Offset: 0x0000A0E1
	public string getHeShe(string upperCase)
	{
		if (this.dynamicData.isMale)
		{
			if (upperCase == "")
			{
				return "he";
			}
			return "He";
		}
		else
		{
			if (upperCase == "")
			{
				return "she";
			}
			return "She";
		}
	}

	// Token: 0x0600023C RID: 572 RVA: 0x0000BF21 File Offset: 0x0000A121
	public string getHimHer(string upperCase)
	{
		if (this.dynamicData.isMale)
		{
			if (upperCase == "")
			{
				return "him";
			}
			return "Him";
		}
		else
		{
			if (upperCase == "")
			{
				return "her";
			}
			return "Her";
		}
	}

	// Token: 0x0600023D RID: 573 RVA: 0x0000BF61 File Offset: 0x0000A161
	public string getHisHer(string upperCase)
	{
		if (this.dynamicData.isMale)
		{
			if (upperCase == "")
			{
				return "his";
			}
			return "His";
		}
		else
		{
			if (upperCase == "")
			{
				return "her";
			}
			return "Her";
		}
	}

	// Token: 0x0600023E RID: 574 RVA: 0x0000BFA1 File Offset: 0x0000A1A1
	public bool isCharacterMale()
	{
		return this.dynamicData.isMale;
	}

	// Token: 0x0600023F RID: 575 RVA: 0x0000BFAE File Offset: 0x0000A1AE
	public bool isHostile()
	{
		if (this.isPC())
		{
			return false;
		}
		if (this.getFactionRelationships().areAnyFactionsHostile())
		{
			this.dynamicData.hostile = true;
		}
		return this.dynamicData.hostile;
	}

	// Token: 0x06000240 RID: 576 RVA: 0x0000BFDE File Offset: 0x0000A1DE
	public bool setHostile(bool makeHostile)
	{
		if (makeHostile)
		{
			this.getFactionRelationships().makeAllFactionsHostile();
		}
		this.dynamicData.hostile = makeHostile;
		return makeHostile;
	}

	// Token: 0x06000241 RID: 577 RVA: 0x0000BFFB File Offset: 0x0000A1FB
	public SpellContainer getSpellContainer()
	{
		return this.spellContainer;
	}

	// Token: 0x06000242 RID: 578 RVA: 0x0000C003 File Offset: 0x0000A203
	public void setPortraitPath(string s)
	{
		this.portrait = null;
		this.portraitBloodied = null;
		this.getLooksControl().setPortraitId(s);
	}

	// Token: 0x06000243 RID: 579 RVA: 0x0000C020 File Offset: 0x0000A220
	private static TextureTools.TextureData getLevelUpIcon(bool levelUp)
	{
		if (Character.levelUpIcon == null || Character.levelUpIcon2 == null)
		{
			Character.levelUpIcon = TextureTools.loadTextureData("Images/GUIIcons/PortraitIcons/LevelUp");
			Character.levelUpIcon2 = TextureTools.loadTextureData("Images/GUIIcons/PortraitIcons/LevelUp2");
		}
		if (!levelUp)
		{
			return Character.levelUpIcon;
		}
		if (MapIllustrator.CURRENT_FRAME <= 1)
		{
			return Character.levelUpIcon;
		}
		return Character.levelUpIcon2;
	}

	// Token: 0x06000244 RID: 580 RVA: 0x0000C078 File Offset: 0x0000A278
	public TextureTools.TextureData getPortrait()
	{
		this.setPortraits();
		if (this.isDead())
		{
			this.portraitKOBloody.copyToTarget(this.portraitWorkspace);
		}
		else if (this.isBloodied())
		{
			this.portraitBloodied.copyToTarget(this.portraitWorkspace);
		}
		else if (this.getConditionContainer().isAsleep())
		{
			this.portraitKO.copyToTarget(this.portraitWorkspace);
		}
		else if (this.getConditionContainer().hasNegativeCondition())
		{
			this.portraitCondition.copyToTarget(this.portraitWorkspace);
		}
		else
		{
			this.portrait.copyToTarget(this.portraitWorkspace);
		}
		List<TextureTools.TextureData> list = new List<TextureTools.TextureData>();
		if (this.canLevelUp() || this.getDevelopmentPoints() != 0)
		{
			list.Add(Character.getLevelUpIcon(this.canLevelUp()));
		}
		this.getConditionContainer().getConditionIcons(list);
		if (this.coolDown > 0)
		{
			list.Add(this.getCooldownIcon());
		}
		this.decoratePortrait(list, this.portraitWorkspace);
		return this.portraitWorkspace;
	}

	// Token: 0x06000245 RID: 581 RVA: 0x0000C170 File Offset: 0x0000A370
	public TextureTools.TextureData getCooldownIcon()
	{
		string text = "Images/GUIIcons/PortraitIcons/Cooldown";
		if (this.coolDown > 4)
		{
			text += "4";
		}
		else
		{
			text += this.getCooldown().ToString();
		}
		return TextureTools.loadTextureData(text);
	}

	// Token: 0x06000246 RID: 582 RVA: 0x0000C1B5 File Offset: 0x0000A3B5
	public bool hasCascadesRemaining()
	{
		return this.currentCascadeCount < this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_SpellMaxCascade);
	}

	// Token: 0x06000247 RID: 583 RVA: 0x0000C1CA File Offset: 0x0000A3CA
	public void incrementCascade()
	{
		this.currentCascadeCount++;
	}

	// Token: 0x06000248 RID: 584 RVA: 0x0000C1DA File Offset: 0x0000A3DA
	public void clearCascadeCounter()
	{
		this.currentCascadeCount = 0;
	}

	// Token: 0x06000249 RID: 585 RVA: 0x0000C1E3 File Offset: 0x0000A3E3
	public bool isSilenced()
	{
		return this.getConditionContainer().isSilenced();
	}

	// Token: 0x0600024A RID: 586 RVA: 0x0000C1F0 File Offset: 0x0000A3F0
	public bool isBlind()
	{
		return this.getConditionContainer().isBlind();
	}

	// Token: 0x0600024B RID: 587 RVA: 0x0000C1FD File Offset: 0x0000A3FD
	public bool isDefending()
	{
		return this.getConditionContainer().isDefending();
	}

	// Token: 0x0600024C RID: 588 RVA: 0x0000C20A File Offset: 0x0000A40A
	public bool isImmobilized()
	{
		return this.getConditionContainer().isImmobilized();
	}

	// Token: 0x0600024D RID: 589 RVA: 0x0000C217 File Offset: 0x0000A417
	public bool isDeaf()
	{
		return this.getConditionContainer().isDeaf();
	}

	// Token: 0x0600024E RID: 590 RVA: 0x0000C224 File Offset: 0x0000A424
	private void decoratePortrait(List<TextureTools.TextureData> inputList, TextureTools.TextureData target)
	{
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < inputList.Count; i++)
		{
			TextureTools.applyOverlay(target, inputList[i], num * 8, num2 * 8);
			num++;
			if (num == 5)
			{
				num = 0;
				num2++;
			}
		}
	}

	// Token: 0x0600024F RID: 591 RVA: 0x0000C268 File Offset: 0x0000A468
	private void setPortraits()
	{
		if (this.portrait == null)
		{
			string text = "Images/Portraits/";
			if (this.getLooksControl().getPortraitPath() == "")
			{
				text += "Black";
			}
			else
			{
				text += this.getLooksControl().getPortraitPath();
			}
			this.portrait = TextureTools.loadPortrait(0, text);
			this.portraitCondition = TextureTools.loadPortrait(3, text);
			this.portraitKO = TextureTools.loadPortrait(2, text);
			this.portraitBloodied = TextureTools.loadPortrait(1, text);
			this.portraitKOBloody = TextureTools.loadPortrait(4, text);
			this.portraitWorkspace = new TextureTools.TextureData(this.portrait.width, this.portrait.height);
		}
	}

	// Token: 0x06000250 RID: 592 RVA: 0x0000C31F File Offset: 0x0000A51F
	public bool isBloodied()
	{
		return this.getWounds() < this.getMaxWounds();
	}

	// Token: 0x06000251 RID: 593 RVA: 0x0000C32F File Offset: 0x0000A52F
	public string setIsCharacterMale(bool male)
	{
		this.dynamicData.isMale = male;
		return this.getName() + " is " + this.printGender();
	}

	// Token: 0x06000252 RID: 594 RVA: 0x0000C353 File Offset: 0x0000A553
	public void useAbility()
	{
		this.combatOrders.useAbility();
	}

	// Token: 0x06000253 RID: 595 RVA: 0x0000C360 File Offset: 0x0000A560
	public string setPC(bool pc)
	{
		if (pc)
		{
			this.dynamicData.PC = true;
			this.dynamicData.hostile = false;
			this.getInventory().activateNewAdditionTagging();
			return "You add " + this.getFullNameUpper() + " to your party.";
		}
		this.dynamicData.PC = false;
		return "";
	}

	// Token: 0x06000254 RID: 596 RVA: 0x0000C3BA File Offset: 0x0000A5BA
	public override string getColorTag()
	{
		if (this.isPC())
		{
			return C64Color.YELLOW_TAG;
		}
		return C64Color.RED_LIGHT_TAG;
	}

	// Token: 0x06000255 RID: 597 RVA: 0x0000C3CF File Offset: 0x0000A5CF
	public bool isPC()
	{
		return this.dynamicData.PC;
	}

	// Token: 0x06000256 RID: 598 RVA: 0x0000C3DC File Offset: 0x0000A5DC
	public int getCooldown()
	{
		return this.coolDown;
	}

	// Token: 0x06000257 RID: 599 RVA: 0x0000C3E4 File Offset: 0x0000A5E4
	public void setCooldown(int amount)
	{
		if (amount < 0)
		{
			this.coolDown = 0;
			return;
		}
		this.coolDown = amount;
	}

	// Token: 0x06000258 RID: 600 RVA: 0x0000C3F9 File Offset: 0x0000A5F9
	public void decrementCooldown()
	{
		if (this.coolDown >= 0)
		{
			this.coolDown--;
		}
	}

	// Token: 0x06000259 RID: 601 RVA: 0x0000C412 File Offset: 0x0000A612
	private void clearCoolDown()
	{
		this.coolDown = 0;
	}

	// Token: 0x0600025A RID: 602 RVA: 0x0000C41C File Offset: 0x0000A61C
	public int getCarryingCapacity()
	{
		int num = 20 + this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_CarryWeightBonus);
		int currentAttributeValue = this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_Strength);
		if (currentAttributeValue <= 0)
		{
			return num;
		}
		return num + currentAttributeValue * 30;
	}

	// Token: 0x0600025B RID: 603 RVA: 0x0000C449 File Offset: 0x0000A649
	public void makeArcaneCaster()
	{
		this.dynamicData.combatAbilityFlags.arcaneSpellcaster = true;
	}

	// Token: 0x0600025C RID: 604 RVA: 0x0000C45C File Offset: 0x0000A65C
	public void makeDivineCaster()
	{
		this.dynamicData.combatAbilityFlags.divineSpellcaster = true;
	}

	// Token: 0x0600025D RID: 605 RVA: 0x0000C470 File Offset: 0x0000A670
	public void addCharacterClass(string id)
	{
		CharacterClass @class = GameData.getClass(id);
		@class.applyData(this);
		this.dynamicData.characterClass = @class.getId();
		this.adjustLevelFactorForHPAndATT();
		this.restoreAllFull();
	}

	// Token: 0x0600025E RID: 606 RVA: 0x0000C4A8 File Offset: 0x0000A6A8
	private void adjustLevelFactorForHPAndATT()
	{
		CharacterClass @class = this.getClass();
		this.attributes.setLevelFactor(AttributesControl.CoreAttributes.ATT_Vitality, (float)@class.getLevelUpVitality());
		this.attributes.setLevelFactor(AttributesControl.CoreAttributes.ATT_Attunement, (float)@class.getLevelUpAttunement());
	}

	// Token: 0x0600025F RID: 607 RVA: 0x0000C4E4 File Offset: 0x0000A6E4
	public void addCharacterRace(string id)
	{
		CharacterRace race = GameData.getRace(id);
		this.dynamicData.race = race.getId();
		race.applyData(this);
	}

	// Token: 0x06000260 RID: 608 RVA: 0x0000C510 File Offset: 0x0000A710
	public string addFactionMembership(string[] factionIds)
	{
		foreach (string factionId in factionIds)
		{
			this.addFactionMembership(factionId);
		}
		return "";
	}

	// Token: 0x06000261 RID: 609 RVA: 0x0000C540 File Offset: 0x0000A740
	public bool isInteractable()
	{
		SKALDProjectData.CharacterContainers.Character character = this.getRawData();
		return character != null && character.interactable;
	}

	// Token: 0x06000262 RID: 610 RVA: 0x0000C55F File Offset: 0x0000A75F
	public string addFactionMembership(string factionId)
	{
		return this.getFactionRelationships().addFactionMembership(factionId);
	}

	// Token: 0x06000263 RID: 611 RVA: 0x0000C56D File Offset: 0x0000A76D
	public bool isFactionMember(string factionId)
	{
		return this.getFactionRelationships().isFactionMember(factionId);
	}

	// Token: 0x06000264 RID: 612 RVA: 0x0000C57C File Offset: 0x0000A77C
	public void addCharacterBackground(string id)
	{
		CharacterBackground background = GameData.getBackground(id);
		this.dynamicData.background = background.getId();
		background.applyData(this);
	}

	// Token: 0x06000265 RID: 613 RVA: 0x0000C5A8 File Offset: 0x0000A7A8
	public bool isNPCHostile(Character testCharacter)
	{
		return testCharacter != null && (!this.isPC() || !testCharacter.isPC()) && ((this.isPC() && testCharacter.isHostile()) || this.isHostile() != testCharacter.isHostile());
	}

	// Token: 0x06000266 RID: 614 RVA: 0x0000C5E4 File Offset: 0x0000A7E4
	public bool isNPCAlly(Character testCharacter)
	{
		return !this.isNPCHostile(testCharacter);
	}

	// Token: 0x06000267 RID: 615 RVA: 0x0000C5F0 File Offset: 0x0000A7F0
	public bool isCurrentWeaponSlashing()
	{
		if (this.getCurrentWeapon() != null)
		{
			return this.getCurrentWeapon().isSlashing();
		}
		return this.getDamageTypes().Contains("Slashing");
	}

	// Token: 0x06000268 RID: 616 RVA: 0x0000C616 File Offset: 0x0000A816
	public bool isCurrentWeaponPiercing()
	{
		if (this.getCurrentWeapon() != null)
		{
			return this.getCurrentWeapon().isPiercing();
		}
		return this.getDamageTypes().Contains("Piercing");
	}

	// Token: 0x06000269 RID: 617 RVA: 0x0000C63C File Offset: 0x0000A83C
	public bool isCurrentWeaponBlunt()
	{
		if (this.getCurrentWeapon() != null)
		{
			return this.getCurrentWeapon().isBlunt();
		}
		return this.getDamageTypes().Contains("Blunt");
	}

	// Token: 0x0600026A RID: 618 RVA: 0x0000C664 File Offset: 0x0000A864
	public int getCoreAttributeValue()
	{
		string coreAttributeId = this.getClass().getCoreAttributeId();
		if (coreAttributeId == "")
		{
			return 0;
		}
		return this.getCurrentAttributeValue(coreAttributeId);
	}

	// Token: 0x0600026B RID: 619 RVA: 0x0000C693 File Offset: 0x0000A893
	public string getCoreAttributeId()
	{
		return this.getClass().getCoreAttributeId();
	}

	// Token: 0x0600026C RID: 620 RVA: 0x0000C6A0 File Offset: 0x0000A8A0
	public bool isCurrentWeaponSword()
	{
		return this.getCurrentWeapon() != null && this.getCurrentWeapon().isSword();
	}

	// Token: 0x0600026D RID: 621 RVA: 0x0000C6B7 File Offset: 0x0000A8B7
	public bool isCurrentWeaponAxe()
	{
		return this.getCurrentWeapon() != null && this.getCurrentWeapon().isAxe();
	}

	// Token: 0x0600026E RID: 622 RVA: 0x0000C6CE File Offset: 0x0000A8CE
	public bool isCurrentWeaponClub()
	{
		return this.getCurrentWeapon() != null && this.getCurrentWeapon().isClub();
	}

	// Token: 0x0600026F RID: 623 RVA: 0x0000C6E5 File Offset: 0x0000A8E5
	public bool isCurrentWeaponPolearm()
	{
		return this.getCurrentWeapon() != null && this.getCurrentWeapon().isPolearm();
	}

	// Token: 0x06000270 RID: 624 RVA: 0x0000C6FC File Offset: 0x0000A8FC
	public bool isCurrentWeaponBow()
	{
		return this.getCurrentWeapon() != null && this.getCurrentWeapon().isBow();
	}

	// Token: 0x06000271 RID: 625 RVA: 0x0000C713 File Offset: 0x0000A913
	public bool isCurrentWeaponCrossbow()
	{
		return this.getCurrentWeapon() != null && this.getCurrentWeapon().isCrossbow();
	}

	// Token: 0x06000272 RID: 626 RVA: 0x0000C72A File Offset: 0x0000A92A
	public bool isCurrentWeaponLight()
	{
		return this.getCurrentWeapon() == null || this.getCurrentWeapon().isLight();
	}

	// Token: 0x06000273 RID: 627 RVA: 0x0000C741 File Offset: 0x0000A941
	public bool isCurrentWeaponMedium()
	{
		return this.getCurrentWeapon() != null && this.getCurrentWeapon().isMedium();
	}

	// Token: 0x06000274 RID: 628 RVA: 0x0000C758 File Offset: 0x0000A958
	public bool isCurrentWeaponHeavy()
	{
		return this.getCurrentWeapon() != null && this.getCurrentWeapon().isHeavy();
	}

	// Token: 0x06000275 RID: 629 RVA: 0x0000C76F File Offset: 0x0000A96F
	public AbilityContainerManeuver getAbilityManueverContainer()
	{
		return this.abilityContainerManuver;
	}

	// Token: 0x06000276 RID: 630 RVA: 0x0000C777 File Offset: 0x0000A977
	public AbilityContainerTriggered getAbilityTriggeredContainer()
	{
		return this.abilityContainerTriggered;
	}

	// Token: 0x06000277 RID: 631 RVA: 0x0000C77F File Offset: 0x0000A97F
	public AbilityContainerPassive getAbilityPassiveContainer()
	{
		return this.abilityContainerPassive;
	}

	// Token: 0x06000278 RID: 632 RVA: 0x0000C787 File Offset: 0x0000A987
	public int getReactionScore()
	{
		return this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_Diplomacy);
	}

	// Token: 0x06000279 RID: 633 RVA: 0x0000C790 File Offset: 0x0000A990
	public SkaldActionResult castSpell()
	{
		if (this.isInCombat)
		{
			this.combatOrders.castSpell();
		}
		else if (this.getSpellContainer().isCurrentComponentOverlandActivated())
		{
			return this.getSpellContainer().useCurrentComponent(this);
		}
		return new SkaldActionResult(false, true, "This spell cannot be cast now.", true);
	}

	// Token: 0x0600027A RID: 634 RVA: 0x0000C7CE File Offset: 0x0000A9CE
	private Character.ChargeMoveCounter GetChargeMoveCounter()
	{
		if (this.chargeMoveCounter == null)
		{
			this.chargeMoveCounter = new Character.ChargeMoveCounter();
		}
		return this.chargeMoveCounter;
	}

	// Token: 0x0600027B RID: 635 RVA: 0x0000C7E9 File Offset: 0x0000A9E9
	public int getChargeCounterValue()
	{
		return this.GetChargeMoveCounter().getCount();
	}

	// Token: 0x0600027C RID: 636 RVA: 0x0000C7F6 File Offset: 0x0000A9F6
	private void updateChargeMoveCounter()
	{
		this.GetChargeMoveCounter().updateCounter(this.dynamicData.facing);
	}

	// Token: 0x0600027D RID: 637 RVA: 0x0000C80E File Offset: 0x0000AA0E
	private void clearChargeMoveCounter()
	{
		this.GetChargeMoveCounter().clearCounter();
	}

	// Token: 0x0600027E RID: 638 RVA: 0x0000C81B File Offset: 0x0000AA1B
	public void combatMove()
	{
		this.updateChargeMoveCounter();
		this.combatOrders.combatMove();
	}

	// Token: 0x0600027F RID: 639 RVA: 0x0000C82E File Offset: 0x0000AA2E
	public void combatSwap()
	{
		this.combatOrders.combatSwap();
	}

	// Token: 0x06000280 RID: 640 RVA: 0x0000C83B File Offset: 0x0000AA3B
	public bool hasRemainingCombatMovesOrAttacks()
	{
		return this.getExactRemainingCombatMovesIncludingAttacks() > 0;
	}

	// Token: 0x06000281 RID: 641 RVA: 0x0000C849 File Offset: 0x0000AA49
	public void useItemInCombat()
	{
		if (this.getRemainingCombatMoves() > 0)
		{
			this.clearCombatMovesButNotAttacks();
			return;
		}
		this.clearAllCombatMoves();
		this.pass();
	}

	// Token: 0x06000282 RID: 642 RVA: 0x0000C867 File Offset: 0x0000AA67
	public void clearAllCombatMoves()
	{
		this.clearCombatMovesButNotAttacks();
		this.attributes.setAttributeCurrentValue(AttributesControl.CoreAttributes.ATT_Attacks, 0);
	}

	// Token: 0x06000283 RID: 643 RVA: 0x0000C87D File Offset: 0x0000AA7D
	public void clearCombatMovesButNotAttacks()
	{
		this.attributes.setAttributeCurrentValue(AttributesControl.CoreAttributes.ATT_Movement, 0);
	}

	// Token: 0x06000284 RID: 644 RVA: 0x0000C88D File Offset: 0x0000AA8D
	public void clearCombatMovesButNotAttacksIfIHaveMoves()
	{
		if (this.getRemainingCombatMoves() > 0)
		{
			this.attributes.setAttributeCurrentValue(AttributesControl.CoreAttributes.ATT_Movement, 0);
			return;
		}
		this.clearAllCombatMoves();
	}

	// Token: 0x06000285 RID: 645 RVA: 0x0000C8AD File Offset: 0x0000AAAD
	public void restoreOneAttack()
	{
		this.attributes.addToAttributeCurrentValue(AttributesControl.CoreAttributes.ATT_Attacks, 1);
		if (this.hasRemainingCombatMovesOrAttacks())
		{
			this.hasActed = false;
			this.planning = true;
			this.combatOrders.clearOrders();
		}
	}

	// Token: 0x06000286 RID: 646 RVA: 0x0000C8DE File Offset: 0x0000AADE
	public void resetCombatMoves()
	{
		this.attributes.resetAttributeToAbsoluteMax(AttributesControl.CoreAttributes.ATT_Movement);
		this.attributes.resetAttributeToAbsoluteMax(AttributesControl.CoreAttributes.ATT_Attacks);
	}

	// Token: 0x06000287 RID: 647 RVA: 0x0000C8FA File Offset: 0x0000AAFA
	private void decrementCombatMoves()
	{
		if (this.getRemainingCombatMoves() > 0)
		{
			this.attributes.addToAttributeCurrentValue(AttributesControl.CoreAttributes.ATT_Movement, -1);
			if (this.getRemainingAttacks() > 1)
			{
				this.attributes.setAttributeCurrentValue(AttributesControl.CoreAttributes.ATT_Attacks, 1);
				return;
			}
		}
		else
		{
			this.attributes.setAttributeCurrentValue(AttributesControl.CoreAttributes.ATT_Attacks, 0);
		}
	}

	// Token: 0x06000288 RID: 648 RVA: 0x0000C939 File Offset: 0x0000AB39
	private void decrementAttacks()
	{
		this.attributes.addToAttributeCurrentValue(AttributesControl.CoreAttributes.ATT_Attacks, -1);
	}

	// Token: 0x06000289 RID: 649 RVA: 0x0000C94C File Offset: 0x0000AB4C
	public int getExactRemainingCombatMovesIncludingAttacks()
	{
		int num = 0;
		if (this.getRemainingAttacks() > 0)
		{
			num = 1;
		}
		return this.getRemainingCombatMoves() + num;
	}

	// Token: 0x0600028A RID: 650 RVA: 0x0000C96E File Offset: 0x0000AB6E
	public int getRemainingCombatMoves()
	{
		return this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_Movement);
	}

	// Token: 0x0600028B RID: 651 RVA: 0x0000C978 File Offset: 0x0000AB78
	public int getRemainingAttacks()
	{
		return this.attributes.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_Attacks);
	}

	// Token: 0x0600028C RID: 652 RVA: 0x0000C987 File Offset: 0x0000AB87
	public int getMaxAttacks()
	{
		return this.attributes.getMaxAttributeValue(AttributesControl.CoreAttributes.ATT_Attacks);
	}

	// Token: 0x0600028D RID: 653 RVA: 0x0000C996 File Offset: 0x0000AB96
	public int getMaxMoves()
	{
		return this.attributes.getMaxAttributeValue(AttributesControl.CoreAttributes.ATT_Movement);
	}

	// Token: 0x0600028E RID: 654 RVA: 0x0000C9A5 File Offset: 0x0000ABA5
	public bool hasMoved()
	{
		return this.getRemainingCombatMoves() < this.getMaxMoves();
	}

	// Token: 0x0600028F RID: 655 RVA: 0x0000C9B5 File Offset: 0x0000ABB5
	public bool canCharacterCombatMove()
	{
		return !this.isDead() && !this.isImmobilized() && this.isAlert() && this.hasRemainingCombatMovesOrAttacks();
	}

	// Token: 0x06000290 RID: 656 RVA: 0x0000C9E0 File Offset: 0x0000ABE0
	public bool isWeaponRanged()
	{
		return this.getCurrentWeapon() != null && this.getCurrentWeapon().isRanged();
	}

	// Token: 0x06000291 RID: 657 RVA: 0x0000C9F8 File Offset: 0x0000ABF8
	public void dropLoot(Inventory targetInventory)
	{
		targetInventory.transferLootableInventoryAndClearUser(this.getInventory());
		foreach (string loadoutId in this.getRawData().loadoutLootList)
		{
			GameData.applyLoadoutData(loadoutId, targetInventory);
		}
	}

	// Token: 0x06000292 RID: 658 RVA: 0x0000CA5C File Offset: 0x0000AC5C
	public void activatePointBlankShot()
	{
		this.dynamicData.combatAbilityFlags.pointBlankShot = true;
	}

	// Token: 0x06000293 RID: 659 RVA: 0x0000CA70 File Offset: 0x0000AC70
	public string clearParty()
	{
		this.dynamicData.mainParty = null;
		Inventory inventory = this.getInventory().removeOwnedItems(this);
		this.dynamicData.inventory = inventory;
		this.setPC(false);
		return "Cleared party";
	}

	// Token: 0x06000294 RID: 660 RVA: 0x0000CAAF File Offset: 0x0000ACAF
	public string setOpponentParty(Party _party)
	{
		this.opponentParty = _party;
		return "Added oppoentn party";
	}

	// Token: 0x06000295 RID: 661 RVA: 0x0000CABD File Offset: 0x0000ACBD
	public Party getOpponentParty()
	{
		return this.opponentParty;
	}

	// Token: 0x06000296 RID: 662 RVA: 0x0000CAC5 File Offset: 0x0000ACC5
	public string setCombatAllyParty(Party _party)
	{
		this.combatParty = _party;
		return "Added combat party";
	}

	// Token: 0x06000297 RID: 663 RVA: 0x0000CAD3 File Offset: 0x0000ACD3
	public Party getCombatAllyParty()
	{
		return this.combatParty;
	}

	// Token: 0x06000298 RID: 664 RVA: 0x0000CADB File Offset: 0x0000ACDB
	private string clearCombatParties()
	{
		this.opponentParty = null;
		this.combatParty = null;
		return "Cleared combat parties";
	}

	// Token: 0x06000299 RID: 665 RVA: 0x0000CAF0 File Offset: 0x0000ACF0
	public void setMainParty(Party _party)
	{
		this.dynamicData.mainParty = _party;
	}

	// Token: 0x0600029A RID: 666 RVA: 0x0000CAFE File Offset: 0x0000ACFE
	public Party getMainParty()
	{
		return this.dynamicData.mainParty;
	}

	// Token: 0x0600029B RID: 667 RVA: 0x0000CB0B File Offset: 0x0000AD0B
	public void setTileParty(Party _party)
	{
		this.tileParty = _party;
	}

	// Token: 0x0600029C RID: 668 RVA: 0x0000CB14 File Offset: 0x0000AD14
	public Party getTileParty()
	{
		return this.tileParty;
	}

	// Token: 0x0600029D RID: 669 RVA: 0x0000CB1C File Offset: 0x0000AD1C
	public void addAllFeats()
	{
		this.getFeatContainer().addAllRanks(this);
		this.dynamicData.developmentPoints = 0;
	}

	// Token: 0x0600029E RID: 670 RVA: 0x0000CB36 File Offset: 0x0000AD36
	public void addFeat(List<string> idList)
	{
		this.getFeatContainer().addFeat(idList);
	}

	// Token: 0x0600029F RID: 671 RVA: 0x0000CB44 File Offset: 0x0000AD44
	public void addFeat(string id)
	{
		this.getFeatContainer().addFeat(id);
	}

	// Token: 0x060002A0 RID: 672 RVA: 0x0000CB52 File Offset: 0x0000AD52
	public FeatContainer getFeatContainer()
	{
		return this.featContainer;
	}

	// Token: 0x060002A1 RID: 673 RVA: 0x0000CB5A File Offset: 0x0000AD5A
	public ConditionContainer getConditionContainer()
	{
		if (this.temporaryConditionContainer == null)
		{
			this.temporaryConditionContainer = new ConditionContainer(this);
		}
		return this.temporaryConditionContainer;
	}

	// Token: 0x060002A2 RID: 674 RVA: 0x0000CB76 File Offset: 0x0000AD76
	public void finalizeFeatPurchases()
	{
		this.getFeatContainer().finalizeRanks(this);
	}

	// Token: 0x060002A3 RID: 675 RVA: 0x0000CB84 File Offset: 0x0000AD84
	public void activateEvasion()
	{
		this.dynamicData.combatAbilityFlags.evasion = true;
	}

	// Token: 0x060002A4 RID: 676 RVA: 0x0000CB97 File Offset: 0x0000AD97
	public void addSpell(string id)
	{
		this.getSpellContainer().addSpell(id);
	}

	// Token: 0x060002A5 RID: 677 RVA: 0x0000CBA5 File Offset: 0x0000ADA5
	public void addSpell(List<string> idList)
	{
		this.getSpellContainer().addSpell(idList);
	}

	// Token: 0x060002A6 RID: 678 RVA: 0x0000CBB3 File Offset: 0x0000ADB3
	public void addSpell(List<AbilitySpell> idList)
	{
		this.getSpellContainer().addSpell(idList);
	}

	// Token: 0x060002A7 RID: 679 RVA: 0x0000CBC4 File Offset: 0x0000ADC4
	public string getCrosshairPath()
	{
		if (this.isVulnerable() || this.isDead())
		{
			return "";
		}
		if (!this.hasRemainingCombatMovesOrAttacks())
		{
			return "";
		}
		if (this.targetOpponent == null || this.targetOpponent.isDead())
		{
			return "";
		}
		return "CrosshairSword";
	}

	// Token: 0x060002A8 RID: 680 RVA: 0x0000CC15 File Offset: 0x0000AE15
	public bool hasDynamicAnimation()
	{
		return this.dynamicData.characterAnimationControl.hasDynamicAnimation();
	}

	// Token: 0x060002A9 RID: 681 RVA: 0x0000CC27 File Offset: 0x0000AE27
	public void setDynamicAnimation(string id)
	{
		if (this.getMainParty() != null && this.getMainParty().hasVehicle())
		{
			return;
		}
		this.dynamicData.characterAnimationControl.setDynamicAnimation(id);
	}

	// Token: 0x060002AA RID: 682 RVA: 0x0000CC50 File Offset: 0x0000AE50
	public void clearDynamicAnimation()
	{
		this.dynamicData.characterAnimationControl.clearDynamicAnimation();
	}

	// Token: 0x060002AB RID: 683 RVA: 0x0000CC62 File Offset: 0x0000AE62
	private bool hasDynamaicAnimation()
	{
		return this.dynamicData.characterAnimationControl.hasDynamicAnimation();
	}

	// Token: 0x060002AC RID: 684 RVA: 0x0000CC74 File Offset: 0x0000AE74
	private int getCurrentFrame()
	{
		if (this.hasDynamaicAnimation())
		{
			return this.dynamicData.characterAnimationControl.getDynamicAnimationFrame();
		}
		if (this.isFalling)
		{
			return this.dynamicData.characterAnimationControl.getFallingAnimation();
		}
		if (this.isDead())
		{
			return this.dynamicData.characterAnimationControl.getDeathAnimation();
		}
		if (this.drawOverlandItem())
		{
			return this.dynamicData.characterAnimationControl.getOverlandAnimation();
		}
		if (this.drawIdleItem())
		{
			return this.dynamicData.characterAnimationControl.getIdleItemAnimation();
		}
		if (this.isPanicked())
		{
			return this.dynamicData.characterAnimationControl.getPanicAnimation();
		}
		if (this.getConditionContainer().isStunned())
		{
			return this.dynamicData.characterAnimationControl.getStunnedAnimation();
		}
		if (this.planning && this.getTargetOpponent() != null && this.isTargetInRange() && !this.getTargetOpponent().isDead() && this.isPaperDoll() && !this.isWeaponRanged())
		{
			this.updateAimAnimation();
			return this.dynamicData.characterAnimationControl.getAimingAnimation();
		}
		if ((this.isInCombat || this.isHostile()) && this.isAlert())
		{
			this.updateIdleAnimation();
			return this.dynamicData.characterAnimationControl.getCombatAnimation();
		}
		if (this.isHidden())
		{
			return this.dynamicData.characterAnimationControl.getHiddenAnimation();
		}
		if (this.wading)
		{
			return this.dynamicData.characterAnimationControl.getExploreAnimation();
		}
		if (this.getCurrentLight() != null)
		{
			return this.dynamicData.characterAnimationControl.getExploreAnimation();
		}
		if (this.isBloodied())
		{
			return this.dynamicData.characterAnimationControl.getBloodiedAnimation();
		}
		if (!this.isAlert())
		{
			return this.dynamicData.characterAnimationControl.getBaseNotAlertAnimation();
		}
		return this.dynamicData.characterAnimationControl.getBaseAnimation();
	}

	// Token: 0x060002AD RID: 685 RVA: 0x0000CE3D File Offset: 0x0000B03D
	public XpContainer getXpContainer()
	{
		if (this.dynamicData.xpContainer == null)
		{
			this.dynamicData.xpContainer = new XpContainer();
		}
		return this.dynamicData.xpContainer;
	}

	// Token: 0x060002AE RID: 686 RVA: 0x0000CE67 File Offset: 0x0000B067
	public int getLevel()
	{
		return this.getXpContainer().getLevel();
	}

	// Token: 0x060002AF RID: 687 RVA: 0x0000CE74 File Offset: 0x0000B074
	public override string getDescription()
	{
		if (base.getDescription() != "")
		{
			return base.processString(base.getDescription(), this);
		}
		return "You meet " + this.getName() + "!";
	}

	// Token: 0x060002B0 RID: 688 RVA: 0x0000CEAB File Offset: 0x0000B0AB
	public int getLevelUps()
	{
		return this.getXpContainer().getLevelUps();
	}

	// Token: 0x060002B1 RID: 689 RVA: 0x0000CEB8 File Offset: 0x0000B0B8
	public Character.MoveMode getMoveMode()
	{
		return this.dynamicData.moveMode;
	}

	// Token: 0x060002B2 RID: 690 RVA: 0x0000CEC5 File Offset: 0x0000B0C5
	public void updateMoveMode()
	{
		if (this.isHostile() && this.isAlert())
		{
			if (this.isAfraid())
			{
				this.setMoveMode(Character.MoveMode.Flee);
				return;
			}
			this.setMoveMode(Character.MoveMode.Home);
		}
	}

	// Token: 0x060002B3 RID: 691 RVA: 0x0000CEF0 File Offset: 0x0000B0F0
	public void setMoveMode(string mode)
	{
		if (mode == null || mode == "")
		{
			return;
		}
		mode = mode.ToUpper();
		if (mode == "HOME")
		{
			this.setMoveMode(Character.MoveMode.Home);
			return;
		}
		if (mode == "ROAM")
		{
			this.setMoveMode(Character.MoveMode.Roam);
			return;
		}
		if (mode == "FLEE")
		{
			this.setMoveMode(Character.MoveMode.Roam);
			return;
		}
		if (mode == "ROAMIFALERT")
		{
			this.setMoveMode(Character.MoveMode.RoamIfAlert);
			return;
		}
		if (mode == "GRAZE")
		{
			this.setMoveMode(Character.MoveMode.Graze);
			return;
		}
		if (mode == "PATROLNS")
		{
			this.setMoveMode(Character.MoveMode.PatrolNS);
			return;
		}
		if (mode == "PATROLEW")
		{
			this.setMoveMode(Character.MoveMode.PatrolEW);
			return;
		}
		if (mode == "PATROLCW")
		{
			this.setMoveMode(Character.MoveMode.PatrolCW);
			return;
		}
		if (mode == "PATROLCCW")
		{
			this.setMoveMode(Character.MoveMode.PatrolCCW);
			return;
		}
		MainControl.logError("Trying to set illegal move-mode for " + this.getId());
	}

	// Token: 0x060002B4 RID: 692 RVA: 0x0000CFE9 File Offset: 0x0000B1E9
	public void setMoveMode(Character.MoveMode mode)
	{
		this.dynamicData.moveMode = mode;
	}

	// Token: 0x060002B5 RID: 693 RVA: 0x0000CFF8 File Offset: 0x0000B1F8
	public int getClothingReactionModifier()
	{
		ItemClothing currentClothing = this.getCurrentClothing();
		if (currentClothing == null)
		{
			return 0;
		}
		return currentClothing.getReactionBonus();
	}

	// Token: 0x060002B6 RID: 694 RVA: 0x0000D018 File Offset: 0x0000B218
	public int getHelmetEncumberance()
	{
		ItemHeadWear currentHeadwear = this.getCurrentHeadwear();
		if (currentHeadwear == null)
		{
			return 0;
		}
		return currentHeadwear.getEncumbrance();
	}

	// Token: 0x060002B7 RID: 695 RVA: 0x0000D038 File Offset: 0x0000B238
	public int getShoeEncumberance()
	{
		ItemFootwear currentFootwear = this.getCurrentFootwear();
		if (currentFootwear == null)
		{
			return 0;
		}
		return currentFootwear.getEncumbrance();
	}

	// Token: 0x060002B8 RID: 696 RVA: 0x0000D058 File Offset: 0x0000B258
	public int getGloveEncumberance()
	{
		ItemGlove currentGloves = this.getCurrentGloves();
		if (currentGloves == null)
		{
			return 0;
		}
		return currentGloves.getEncumbrance();
	}

	// Token: 0x060002B9 RID: 697 RVA: 0x0000D077 File Offset: 0x0000B277
	public ItemFootwear getCurrentFootwear()
	{
		return this.getItemSlots().getFootwear(this.getInventory());
	}

	// Token: 0x060002BA RID: 698 RVA: 0x0000D08A File Offset: 0x0000B28A
	public ItemGlove getCurrentGloves()
	{
		return this.getItemSlots().getGlove(this.getInventory());
	}

	// Token: 0x060002BB RID: 699 RVA: 0x0000D0A0 File Offset: 0x0000B2A0
	public int getArmorEncumberance()
	{
		ItemArmor currentArmor = this.getCurrentArmor();
		if (currentArmor == null)
		{
			return 0;
		}
		int num = this.getCurrentArmor().getEncumbrance();
		int num2 = 0;
		if (currentArmor.isHeavy())
		{
			num2 = this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_ArmEncHeavy);
		}
		else if (currentArmor.isMedium())
		{
			num2 = this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_ArmEncMedium);
		}
		else if (currentArmor.isLight())
		{
			num2 = this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_ArmEncLight);
		}
		num -= num2;
		if (num < 0)
		{
			num = 0;
		}
		return num;
	}

	// Token: 0x060002BC RID: 700 RVA: 0x0000D10C File Offset: 0x0000B30C
	private SkaldNumericContainer getDodgeSkill()
	{
		SkaldNumericContainer skaldNumericContainer = new SkaldNumericContainer();
		if (this.isVulnerable())
		{
			skaldNumericContainer.addEntryButIgnoreZero("Vulnerable", -2);
		}
		else
		{
			int currentAttributeValue = this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_Dodge);
			if (currentAttributeValue > 0)
			{
				skaldNumericContainer.addEntryButIgnoreZero(GameData.getAttributeName(AttributesControl.CoreAttributes.ATT_Dodge), currentAttributeValue);
			}
			skaldNumericContainer.addEntryButIgnoreZero("Shield", this.getShieldBonus());
			skaldNumericContainer.addEntryButIgnoreZero(GameData.getAttributeName(AttributesControl.CoreAttributes.ATT_PhalanxBonus), this.getPhalanxDodgeBonus());
			if (this.getCurrentArmor() == null)
			{
				skaldNumericContainer.addEntryButIgnoreZero(this, AttributesControl.CoreAttributes.ATT_ArmDodgeUnarmored);
			}
			if (this.isDefending())
			{
				skaldNumericContainer.addEntryButIgnoreZero(GameData.getAttributeName(AttributesControl.CoreAttributes.ATT_DefendBonus), this.getDefendBonus());
			}
		}
		if (MainControl.debugFunctions)
		{
			MainControl.log("DODGE FOR " + this.getId().ToUpper() + "\n" + skaldNumericContainer.printEntries());
		}
		return skaldNumericContainer;
	}

	// Token: 0x060002BD RID: 701 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
	public int getShieldBonus()
	{
		ItemShield currentShieldIfInHand = this.getCurrentShieldIfInHand();
		if (currentShieldIfInHand != null)
		{
			return currentShieldIfInHand.getSoak() + this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_ArmDodgeShield);
		}
		return 0;
	}

	// Token: 0x060002BE RID: 702 RVA: 0x0000D1F8 File Offset: 0x0000B3F8
	public int getSoak()
	{
		int num = 0;
		string text = "SOAK FOR " + this.getId().ToUpper() + "\n";
		if (this.getCurrentArmor() != null)
		{
			int soak = this.getCurrentArmor().getSoak();
			num += soak;
			if (MainControl.debugFunctions)
			{
				text = text + "Armor\t" + soak.ToString() + "\n";
			}
		}
		if (this.getCurrentHeadwear() != null)
		{
			int soak2 = this.getCurrentHeadwear().getSoak();
			num += soak2;
			if (MainControl.debugFunctions)
			{
				text = text + "Helmet\t" + soak2.ToString() + "\n";
			}
		}
		if (this.getCurrentFootwear() != null)
		{
			int soak3 = this.getCurrentFootwear().getSoak();
			num += soak3;
			if (MainControl.debugFunctions)
			{
				text = text + "Footwear\t" + soak3.ToString() + "\n";
			}
		}
		if (this.getCurrentGloves() != null)
		{
			int soak4 = this.getCurrentGloves().getSoak();
			num += soak4;
			if (MainControl.debugFunctions)
			{
				text = text + "Gloves\t" + soak4.ToString() + "\n";
			}
		}
		int currentAttributeValue = this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_Soak);
		num += currentAttributeValue;
		if (MainControl.debugFunctions)
		{
			text = text + "Natural\t" + currentAttributeValue.ToString() + "\n";
		}
		if (MainControl.debugFunctions)
		{
			text = text + "Total\t" + num.ToString();
			MainControl.log(text);
		}
		return num;
	}

	// Token: 0x060002BF RID: 703 RVA: 0x0000D354 File Offset: 0x0000B554
	public int rollSoak()
	{
		int soak = this.getSoak();
		return new DicePoolVariable("Soak", 0, soak).getResult();
	}

	// Token: 0x060002C0 RID: 704 RVA: 0x0000D37C File Offset: 0x0000B57C
	public string getSoakString()
	{
		int soak = this.getSoak();
		if (soak == 0)
		{
			return "0";
		}
		return "0-" + soak.ToString();
	}

	// Token: 0x060002C1 RID: 705 RVA: 0x0000D3AA File Offset: 0x0000B5AA
	public void activateFreeSwap()
	{
		this.dynamicData.combatAbilityFlags.freeSwap = true;
	}

	// Token: 0x060002C2 RID: 706 RVA: 0x0000D3BD File Offset: 0x0000B5BD
	public void setInMelee(bool inMelee)
	{
		this.inMelee = inMelee;
	}

	// Token: 0x060002C3 RID: 707 RVA: 0x0000D3C6 File Offset: 0x0000B5C6
	public bool isInMelee()
	{
		return this.inMelee;
	}

	// Token: 0x060002C4 RID: 708 RVA: 0x0000D3D0 File Offset: 0x0000B5D0
	public string addAllLegalSpells()
	{
		List<AbilitySpell> allSpells = GameData.getAllSpells();
		string text = "Added Spells: ";
		foreach (AbilitySpell abilitySpell in allSpells)
		{
			if (abilitySpell.canUserCastThisSpellTier(this, true).wasSuccess())
			{
				this.addAbility(abilitySpell);
				text = text + abilitySpell.getName() + ", ";
			}
		}
		text = TextTools.removeTrailingComma(text);
		return text;
	}

	// Token: 0x060002C5 RID: 709 RVA: 0x0000D454 File Offset: 0x0000B654
	public void rollMoral()
	{
		if (this.isDead())
		{
			return;
		}
		if (!this.isInCombat)
		{
			return;
		}
		bool flag = this.combatParty != null && this.combatParty.getLiveCharacters().Count <= 1;
		if (flag && this.isPC())
		{
			return;
		}
		string text = "ROLLING MORAL FOR " + this.getId().ToUpper() + "\n";
		int num = 6;
		text = text + "Base Diff.:\t" + num.ToString();
		if (this.isBloodied())
		{
			num++;
			text += "Bloodied:\t+1\n";
		}
		if (flag)
		{
			num++;
			text += "Alone:\t+1\n";
		}
		if (!this.isInMelee())
		{
			num += 3;
			text += "Melee:\t+2\n";
		}
		text = text + "TOTAL:\t" + num.ToString() + "\n";
		if (!new SkaldTestRandomVsStatic(this, AttributesControl.CoreAttributes.ATT_Will, num, 1).wasSuccess())
		{
			this.addConditionToCharacter("CON_Panic");
			text += "FAILED";
		}
		if (MainControl.debugFunctions)
		{
			MainControl.log(text);
		}
	}

	// Token: 0x060002C6 RID: 710 RVA: 0x0000D564 File Offset: 0x0000B764
	public string printDevelopmentPoints()
	{
		return C64Color.GRAY_LIGHT_TAG + "Ranks to Distribute:</color> " + this.getDevelopmentPoints().ToString();
	}

	// Token: 0x060002C7 RID: 711 RVA: 0x0000D58E File Offset: 0x0000B78E
	public void updateFeatLegality()
	{
		this.getFeatContainer().updateFeatsLegality(this);
	}

	// Token: 0x060002C8 RID: 712 RVA: 0x0000D59C File Offset: 0x0000B79C
	public int getDevelopmentPoints()
	{
		return this.dynamicData.developmentPoints;
	}

	// Token: 0x060002C9 RID: 713 RVA: 0x0000D5A9 File Offset: 0x0000B7A9
	public void addDevelopmentPoints(int i)
	{
		this.dynamicData.developmentPoints += i;
	}

	// Token: 0x060002CA RID: 714 RVA: 0x0000D5C0 File Offset: 0x0000B7C0
	public int getNumberOfToHitRerolls()
	{
		int num = 0;
		int num2 = 0;
		if (this.isPC())
		{
			num = this.getMissCounter().getBonusRerolls();
			num2 += GlobalSettings.getDifficultySettings().getPlayerToHitRerolls();
		}
		else
		{
			num2 += GlobalSettings.getDifficultySettings().getEnemyToHitRerolls();
		}
		if (MainControl.debugFunctions)
		{
			MainControl.log(string.Concat(new string[]
			{
				C64Color.YELLOW_TAG,
				this.getName().ToUpper(),
				" To-Hit Re-rolls:</color> ",
				num2.ToString(),
				" (base) / ",
				num.ToString(),
				" (smoothing)"
			}));
		}
		return num2 + num;
	}

	// Token: 0x060002CB RID: 715 RVA: 0x0000D660 File Offset: 0x0000B860
	public int getNumberOfDamageRerolls()
	{
		int num = 0;
		int num2;
		if (this.isPC())
		{
			num = this.getMissCounter().getBonusRerolls();
			num2 = GlobalSettings.getDifficultySettings().getPlayerDamageRerolls();
		}
		else
		{
			num2 = GlobalSettings.getDifficultySettings().getEnemyDamageRerolls();
		}
		if (MainControl.debugFunctions)
		{
			MainControl.log(string.Concat(new string[]
			{
				C64Color.YELLOW_TAG,
				this.getName().ToUpper(),
				" Damage Re-rolls:</color> ",
				num2.ToString(),
				" (base) / ",
				num.ToString(),
				" (smoothing)"
			}));
		}
		return num2 + num;
	}

	// Token: 0x060002CC RID: 716 RVA: 0x0000D6F9 File Offset: 0x0000B8F9
	private Character.MissCounter getMissCounter()
	{
		if (this.missCounter == null)
		{
			this.missCounter = new Character.MissCounter();
		}
		return this.missCounter;
	}

	// Token: 0x060002CD RID: 717 RVA: 0x0000D714 File Offset: 0x0000B914
	public void resetMissCounter()
	{
		this.getMissCounter().reset();
	}

	// Token: 0x060002CE RID: 718 RVA: 0x0000D721 File Offset: 0x0000B921
	public void incrementMissCounter()
	{
		this.getMissCounter().addMiss();
	}

	// Token: 0x060002CF RID: 719 RVA: 0x0000D72E File Offset: 0x0000B92E
	public void cyclePreferredCampActivity(int direction)
	{
		this.dynamicData.preferredCampActivity = CampActivityContainer.cycleCampActivity(this.dynamicData.preferredCampActivity, direction);
	}

	// Token: 0x060002D0 RID: 720 RVA: 0x0000D74C File Offset: 0x0000B94C
	public string getPreferredCampActivityName()
	{
		return CampActivityContainer.getCampActivityName(this.dynamicData.preferredCampActivity);
	}

	// Token: 0x060002D1 RID: 721 RVA: 0x0000D75E File Offset: 0x0000B95E
	public string getPreferredCampActivityDescription()
	{
		return CampActivityContainer.getCampActivityDescription(this.dynamicData.preferredCampActivity, this);
	}

	// Token: 0x060002D2 RID: 722 RVA: 0x0000D771 File Offset: 0x0000B971
	public SkaldActionResult performPreferredCampActivity()
	{
		return CampActivityContainer.performPreferredCampActivity(this.dynamicData.preferredCampActivity, this);
	}

	// Token: 0x060002D3 RID: 723 RVA: 0x0000D784 File Offset: 0x0000B984
	private List<string> getDamageTypes()
	{
		List<string> list = new List<string>();
		if (this.getCurrentWeapon() != null)
		{
			list = this.getCurrentWeapon().getDamageTypes();
		}
		else
		{
			SKALDProjectData.CharacterContainers.Character character = this.getRawData();
			if (character != null)
			{
				list = character.unarmedDamageType;
			}
		}
		if (list.Count == 0)
		{
			list.Add("Blunt");
		}
		return list;
	}

	// Token: 0x060002D4 RID: 724 RVA: 0x0000D7D4 File Offset: 0x0000B9D4
	public Damage rollCombatDamage(AbilityCombatManeuver maneuver)
	{
		int damageBonus = this.calculateWeaponAndUnarmedDamageBonus(maneuver);
		return new Damage(new DicePoolVariable(this.getName() + " Damage", this.calculateMinimalDamage(damageBonus), this.calculateMaxDamage(damageBonus), this.getNumberOfDamageRerolls()).getResult(), this.getDamageTypes(), this.getTargetOpponent(), this);
	}

	// Token: 0x060002D5 RID: 725 RVA: 0x0000D82C File Offset: 0x0000BA2C
	private string printDamageRange()
	{
		int damageBonus = this.calculateWeaponAndUnarmedDamageBonus(null);
		return this.calculateMinimalDamage(damageBonus).ToString() + "-" + this.calculateMaxDamage(damageBonus).ToString();
	}

	// Token: 0x060002D6 RID: 726 RVA: 0x0000D869 File Offset: 0x0000BA69
	public void updateItemSlot()
	{
		this.itemSlots = null;
	}

	// Token: 0x060002D7 RID: 727 RVA: 0x0000D874 File Offset: 0x0000BA74
	private int calculateMaxDamage(int damageBonus)
	{
		if (this.getCurrentWeapon() != null)
		{
			int num = 0;
			if (this.needsAmmo() && this.hasAmmo())
			{
				num = this.getCurrentAmmo().getMaxDamage();
			}
			return this.getCurrentWeapon().getMaxDamage() + damageBonus + num;
		}
		return 1 + damageBonus;
	}

	// Token: 0x060002D8 RID: 728 RVA: 0x0000D8BC File Offset: 0x0000BABC
	private int calculateMinimalDamage(int damageBonus)
	{
		if (this.getCurrentWeapon() != null)
		{
			int num = 0;
			if (this.needsAmmo() && this.hasAmmo())
			{
				num = this.getCurrentAmmo().getMinDamage();
			}
			return this.getCurrentWeapon().getMinDamage() + damageBonus + num;
		}
		return 1;
	}

	// Token: 0x060002D9 RID: 729 RVA: 0x0000D900 File Offset: 0x0000BB00
	private int calculateWeaponAndUnarmedDamageBonus(AbilityCombatManeuver maneuver = null)
	{
		int num = 0;
		string text = "";
		if (MainControl.debugFunctions)
		{
			text = "DAMAGE BONUS CALCULATION FOR " + this.getId().ToUpper() + "\n";
		}
		if (maneuver != null)
		{
			int damageBonus = maneuver.getDamageBonus();
			if (MainControl.debugFunctions)
			{
				text = text + "Maneuver Bonus\t" + damageBonus.ToString() + "\n";
			}
			num += damageBonus;
		}
		if (!this.isWeaponRanged())
		{
			num += this.getMeleeDamageBonus();
			if (MainControl.debugFunctions)
			{
				text = text + "STR Melee Bonus\t" + this.getMeleeDamageBonus().ToString() + "\n";
			}
			int currentAttributeValue = this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_DmgGeneralMelee);
			num += currentAttributeValue;
			if (MainControl.debugFunctions)
			{
				text = text + "General Melee Bonus\t" + currentAttributeValue.ToString() + "\n";
			}
			int chargeBonus = this.getChargeBonus();
			if (MainControl.debugFunctions)
			{
				text = text + "Charge\t" + chargeBonus.ToString() + "\n";
			}
			num += chargeBonus;
		}
		else
		{
			int currentAttributeValue2 = this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_DmgGeneralRanged);
			num += currentAttributeValue2;
			if (MainControl.debugFunctions)
			{
				text = text + "General Ranged Bonus\t" + currentAttributeValue2.ToString() + "\n";
			}
		}
		if (this.getCurrentWeapon() == null)
		{
			int currentAttributeValue3 = this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_DmgGeneralUnarmed);
			num += currentAttributeValue3;
			if (MainControl.debugFunctions)
			{
				text = text + "Unarmed Bonus\t" + currentAttributeValue3.ToString() + "\n";
			}
		}
		else
		{
			if (this.isCurrentWeaponAxe())
			{
				int currentAttributeValue4 = this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_DmgWeaponAxes);
				num += currentAttributeValue4;
				if (MainControl.debugFunctions)
				{
					text = text + "Axe Bonus\t" + currentAttributeValue4.ToString() + "\n";
				}
			}
			if (this.isCurrentWeaponBow())
			{
				int currentAttributeValue5 = this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_DmgWeaponBows);
				num += currentAttributeValue5;
				if (MainControl.debugFunctions)
				{
					text = text + "Bow Bonus\t" + currentAttributeValue5.ToString() + "\n";
				}
			}
			if (this.isCurrentWeaponClub())
			{
				int currentAttributeValue6 = this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_DmgWeaponClubs);
				num += currentAttributeValue6;
				if (MainControl.debugFunctions)
				{
					text = text + "Club Bonus\t" + currentAttributeValue6.ToString() + "\n";
				}
			}
			if (this.isCurrentWeaponPolearm())
			{
				int currentAttributeValue7 = this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_DmgWeaponPolearms);
				num += currentAttributeValue7;
				if (MainControl.debugFunctions)
				{
					text = text + "Polearm Bonus\t" + currentAttributeValue7.ToString() + "\n";
				}
			}
			if (this.isCurrentWeaponSword())
			{
				int currentAttributeValue8 = this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_DmgWeaponSwords);
				num += currentAttributeValue8;
				if (MainControl.debugFunctions)
				{
					text = text + "Sword Bonus\t" + currentAttributeValue8.ToString() + "\n";
				}
			}
			if (this.isCurrentWeaponLight())
			{
				int currentAttributeValue9 = this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_DmgWeaponLight);
				num += currentAttributeValue9;
				if (MainControl.debugFunctions)
				{
					text = text + "Light Bonus\t" + currentAttributeValue9.ToString() + "\n";
				}
			}
			if (this.isCurrentWeaponMedium())
			{
				int currentAttributeValue10 = this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_DmgWeaponMedium);
				num += currentAttributeValue10;
				if (MainControl.debugFunctions)
				{
					text = text + "Medium Bonus\t" + currentAttributeValue10.ToString() + "\n";
				}
			}
			if (this.isCurrentWeaponHeavy())
			{
				int currentAttributeValue11 = this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_DmgWeaponHeavy);
				num += currentAttributeValue11;
				if (MainControl.debugFunctions)
				{
					text = text + "Heavy Bonus\t" + currentAttributeValue11.ToString() + "\n";
				}
			}
			if (this.isCurrentWeaponPiercing())
			{
				int currentAttributeValue12 = this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_DmgTypePiercing);
				num += currentAttributeValue12;
				if (MainControl.debugFunctions)
				{
					text = text + "Piercing Bonus\t" + currentAttributeValue12.ToString() + "\n";
				}
			}
			if (this.isCurrentWeaponBlunt())
			{
				int currentAttributeValue13 = this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_DmgTypeBlunt);
				num += currentAttributeValue13;
				if (MainControl.debugFunctions)
				{
					text = text + "Blunt Bonus\t" + currentAttributeValue13.ToString() + "\n";
				}
			}
			if (this.isCurrentWeaponSlashing())
			{
				int currentAttributeValue14 = this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_DmgTypeSlashing);
				num += currentAttributeValue14;
				if (MainControl.debugFunctions)
				{
					text = text + "Slashing Bonus\t" + currentAttributeValue14.ToString() + "\n";
				}
			}
		}
		if (MainControl.debugFunctions)
		{
			text = text + "Total\t" + num.ToString() + "\n";
			MainControl.log(text);
		}
		return num;
	}

	// Token: 0x060002DA RID: 730 RVA: 0x0000DCE0 File Offset: 0x0000BEE0
	public TextureTools.Sprite getOutline(int x, int y, Character focusCharacter, TextureTools.TextureData input)
	{
		if (this.outlineDrawer == null)
		{
			this.outlineDrawer = new ImageOutlineDrawer();
		}
		Color32 outlineColor = this.getOutlineColor(this == focusCharacter || (focusCharacter.isPC() && this == focusCharacter.getTargetOpponent()));
		return this.outlineDrawer.getOutline(x, y, outlineColor, input);
	}

	// Token: 0x060002DB RID: 731 RVA: 0x0000DD34 File Offset: 0x0000BF34
	public TextureTools.Sprite getTacticalIcon(int x, int y, Character focusCharacter)
	{
		if (this.tacticalCharacterIndicator == null)
		{
			this.tacticalCharacterIndicator = TextureTools.loadTextureData("Images/GUIIcons/tacticalCharacterIndicator");
		}
		Color32 outlineColor = this.getOutlineColor(this == focusCharacter || (focusCharacter.isPC() && this == focusCharacter.getTargetOpponent()));
		this.tacticalCharacterIndicator.clearToColorIfNotBlack(outlineColor);
		return new TextureTools.Sprite(x, y, this.tacticalCharacterIndicator);
	}

	// Token: 0x060002DC RID: 732 RVA: 0x0000DD94 File Offset: 0x0000BF94
	public Color32 getOutlineColor(bool flash)
	{
		bool flag = base.getVisualEffects().isForceHighlightActive() || UIInitiativeList.getCurrentCharacter() == this;
		if (this.isHostile())
		{
			if (!flash && !flag)
			{
				return C64Color.RedLight;
			}
			if (MapIllustrator.CURRENT_FRAME % 2 == 0)
			{
				return C64Color.GrayLight;
			}
			return C64Color.RedLight;
		}
		else if (flash)
		{
			if (MapIllustrator.CURRENT_FRAME % 2 == 0)
			{
				return C64Color.White;
			}
			return C64Color.GreenLight;
		}
		else if (this.shouldBeIndicatedAsPersonOfInterest())
		{
			if (this.isPC())
			{
				if (!flag)
				{
					return C64Color.Green;
				}
				if (MapIllustrator.CURRENT_FRAME % 2 == 0)
				{
					return C64Color.White;
				}
				return C64Color.GreenLight;
			}
			else
			{
				if (!flag)
				{
					return C64Color.Cyan;
				}
				if (MapIllustrator.CURRENT_FRAME % 2 == 0)
				{
					return C64Color.White;
				}
				return C64Color.Cyan;
			}
		}
		else
		{
			if (!flag)
			{
				return C64Color.Gray;
			}
			if (MapIllustrator.CURRENT_FRAME % 2 == 0)
			{
				return C64Color.White;
			}
			return C64Color.GrayLight;
		}
	}

	// Token: 0x060002DD RID: 733 RVA: 0x0000DE63 File Offset: 0x0000C063
	public TextureTools.TextureData getShadowImage()
	{
		if (this.shadowImage == null)
		{
			this.shadowImage = TextureTools.loadTextureData("Images/Models/ShadowSmall");
		}
		return this.shadowImage;
	}

	// Token: 0x060002DE RID: 734 RVA: 0x0000DE84 File Offset: 0x0000C084
	private void autoDistributeFeatsIfApplicable()
	{
		if (this.isRecruitable())
		{
			return;
		}
		if (this.isPC())
		{
			MainControl.logError(this.getId() + " is PC but not marked as recruitable!");
		}
		int developmentPoints = this.getDevelopmentPoints();
		this.addDevelopmentPoints(-developmentPoints);
		this.getFeatContainer().autoLevel(developmentPoints, this);
	}

	// Token: 0x060002DF RID: 735 RVA: 0x0000DED3 File Offset: 0x0000C0D3
	public int getMeleeDamageBonus()
	{
		return this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_DamageBonus);
	}

	// Token: 0x060002E0 RID: 736 RVA: 0x0000DEE0 File Offset: 0x0000C0E0
	public string levelUp()
	{
		int num = this.getXpContainer().levelUp();
		int i = this.getClass().getLevelUpDP() * num;
		this.adjustLevelFactorForHPAndATT();
		int num2 = this.getClass().getLevelUpVitality() * num;
		int num3 = this.getClass().getLevelUpAttunement() * num;
		this.addDevelopmentPoints(i);
		this.autoDistributeFeatsIfApplicable();
		this.restoreAllFull();
		string text = C64Color.CYAN_TAG + "+" + num2.ToString() + " Vitality</color>";
		if (num3 > 0)
		{
			text = string.Concat(new string[]
			{
				text,
				C64Color.CYAN_TAG,
				"\n+",
				num3.ToString(),
				" Attunement</color>"
			});
		}
		string text2 = GameData.getStringListEntry("LevelUpMessage", "STR_Level" + this.getLevel().ToString());
		if (text2 == "")
		{
			text2 = "You live to fight another day!";
		}
		return text + "\n\n" + text2;
	}

	// Token: 0x060002E1 RID: 737 RVA: 0x0000DFDD File Offset: 0x0000C1DD
	public void restoreAllFull()
	{
		if (this.isDead() && !this.isPC())
		{
			return;
		}
		this.restoreAllAttributes();
		if (this.isPC())
		{
			this.getConditionContainer().clearRestConditions();
		}
	}

	// Token: 0x060002E2 RID: 738 RVA: 0x0000E00C File Offset: 0x0000C20C
	public void playAttackSound()
	{
		if (this.getCurrentWeapon() != null)
		{
			AudioControl.playRandomSound(this.getCurrentWeapon().getAttackSound());
			return;
		}
		SKALDProjectData.CharacterContainers.Character character = this.getRawData();
		if (character == null || character.attackSound == "")
		{
			AudioControl.playRandomSound(Character.defaultAttackSounds);
			return;
		}
		AudioControl.playSound(character.attackSound);
	}

	// Token: 0x060002E3 RID: 739 RVA: 0x0000E064 File Offset: 0x0000C264
	public void restoreAllPartially(float degree)
	{
		if (this.isDead() && !this.isPC())
		{
			return;
		}
		this.attributes.restoreRestAttributesPartially(degree);
		if (this.isPC())
		{
			this.getConditionContainer().clearRestConditions();
		}
	}

	// Token: 0x060002E4 RID: 740 RVA: 0x0000E096 File Offset: 0x0000C296
	public void clearAllConditions()
	{
		this.getConditionContainer().clearAllConditions();
	}

	// Token: 0x060002E5 RID: 741 RVA: 0x0000E0A3 File Offset: 0x0000C2A3
	public void restShort()
	{
	}

	// Token: 0x060002E6 RID: 742 RVA: 0x0000E0A5 File Offset: 0x0000C2A5
	public int getNextLevel()
	{
		return this.getXpContainer().getNextLevel();
	}

	// Token: 0x060002E7 RID: 743 RVA: 0x0000E0B2 File Offset: 0x0000C2B2
	public int getXPToGo()
	{
		return this.getXpContainer().getXPToGo();
	}

	// Token: 0x060002E8 RID: 744 RVA: 0x0000E0C0 File Offset: 0x0000C2C0
	public int addPercentageXPToNextLevel(int percentage)
	{
		float num = (float)percentage / 100f;
		int num2 = Mathf.RoundToInt((float)this.getNextLevel() * num);
		this.addXp(num2);
		return num2;
	}

	// Token: 0x060002E9 RID: 745 RVA: 0x0000E0EE File Offset: 0x0000C2EE
	public void pass()
	{
		this.passing = true;
	}

	// Token: 0x060002EA RID: 746 RVA: 0x0000E0F7 File Offset: 0x0000C2F7
	public string addCombatXp(int xp)
	{
		if (this.isDead() && !GlobalSettings.getDifficultySettings().getXpForDowned())
		{
			if (MainControl.debugFunctions)
			{
				MainControl.log("No XP for " + this.getId());
			}
			return "";
		}
		return this.addXp(xp);
	}

	// Token: 0x060002EB RID: 747 RVA: 0x0000E138 File Offset: 0x0000C338
	public string addXp(int xp)
	{
		float num = (float)this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_XPBonus);
		if (num > 0f)
		{
			num /= 100f;
			xp += Mathf.RoundToInt((float)xp * num);
		}
		int num2 = this.getXpContainer().addXp(xp, this);
		this.addPositiveBark("+" + num2.ToString() + "XP!");
		return num2.ToString();
	}

	// Token: 0x060002EC RID: 748 RVA: 0x0000E1A0 File Offset: 0x0000C3A0
	public bool canLevelUp()
	{
		return this.getLevelUps() > 0;
	}

	// Token: 0x060002ED RID: 749 RVA: 0x0000E1AE File Offset: 0x0000C3AE
	private void addLevel(int levels)
	{
		this.getXpContainer().addLevel(levels, this);
		this.levelUp();
	}

	// Token: 0x060002EE RID: 750 RVA: 0x0000E1C4 File Offset: 0x0000C3C4
	public void addLevelDontLevelUp(int levels)
	{
		this.getXpContainer().addLevel(levels, this);
	}

	// Token: 0x060002EF RID: 751 RVA: 0x0000E1D4 File Offset: 0x0000C3D4
	public int rollInitiative()
	{
		int num = new DicePoolStandard(this.getName() + " Initiative", this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_Initiative)).getResult();
		if (this.isVulnerable())
		{
			num -= 7;
		}
		this.currentInitiative = num;
		return this.currentInitiative;
	}

	// Token: 0x060002F0 RID: 752 RVA: 0x0000E21D File Offset: 0x0000C41D
	public int getCurrentInitiative()
	{
		return this.currentInitiative;
	}

	// Token: 0x060002F1 RID: 753 RVA: 0x0000E225 File Offset: 0x0000C425
	public void setCurrentInitiative(int newInitiative)
	{
		this.currentInitiative = newInitiative;
	}

	// Token: 0x060002F2 RID: 754 RVA: 0x0000E22E File Offset: 0x0000C42E
	public List<UIButtonControlBase.ButtonData> getNonCombatActivatedSpellButtonDataList()
	{
		return this.getSpellContainer().getNonCombatActivatedComponentButtonDataList();
	}

	// Token: 0x060002F3 RID: 755 RVA: 0x0000E23B File Offset: 0x0000C43B
	public List<UIButtonControlBase.ButtonData> getCombatActivatedSpellButtonDataList()
	{
		return this.getSpellContainer().getCombatActivatedComponentButtonDataList();
	}

	// Token: 0x060002F4 RID: 756 RVA: 0x0000E248 File Offset: 0x0000C448
	public List<UIButtonControlBase.ButtonData> getCombatActivatedAbilityButtonDataList()
	{
		return this.getAbilityManueverContainer().getCombatActivatedComponentButtonDataList();
	}

	// Token: 0x060002F5 RID: 757 RVA: 0x0000E255 File Offset: 0x0000C455
	public string equip()
	{
		this.getInventory().equip(this);
		return "";
	}

	// Token: 0x060002F6 RID: 758 RVA: 0x0000E26C File Offset: 0x0000C46C
	public SkaldActionResult isItemLegalToEquip(Item item, bool addPopup)
	{
		if (!this.isPC())
		{
			return new SkaldActionResult(true, true);
		}
		Item.ItemTypes type = item.getType();
		if (type == Item.ItemTypes.MeleeWeapon || type == Item.ItemTypes.RangedWeapon)
		{
			SkaldActionResult skaldActionResult = this.getClass().isWeaponAllowed(item as ItemWeapon);
			if (!skaldActionResult.wasSuccess() && addPopup)
			{
				PopUpControl.addPopUpOK(skaldActionResult.getResultString());
			}
			return skaldActionResult;
		}
		if (type == Item.ItemTypes.Armor)
		{
			SkaldActionResult skaldActionResult2 = this.getClass().isArmorAllowed(item as ItemArmor);
			if (!skaldActionResult2.wasSuccess() && addPopup)
			{
				PopUpControl.addPopUpOK(skaldActionResult2.getResultString());
			}
			return skaldActionResult2;
		}
		if (type != Item.ItemTypes.Tome)
		{
			return new SkaldActionResult(true, true);
		}
		AbilitySpell spell = (item as ItemSpellTome).getSpell();
		if (this.getSpellContainer().hasComponent(spell.getId()))
		{
			return new SkaldActionResult(true, false, this.getName() + " already  knows this spell!", true);
		}
		return spell.canUserCastThisSpellTier(this, false);
	}

	// Token: 0x060002F7 RID: 759 RVA: 0x0000E340 File Offset: 0x0000C540
	public void defend()
	{
		this.combatOrders.defend();
	}

	// Token: 0x060002F8 RID: 760 RVA: 0x0000E34D File Offset: 0x0000C54D
	public SkaldActionResult useCurrentItem()
	{
		return this.getInventory().useCurrentItem(this);
	}

	// Token: 0x060002F9 RID: 761 RVA: 0x0000E35C File Offset: 0x0000C55C
	private string setDefendCondition()
	{
		this.addConditionToCharacter("CON_Defending");
		return this.getNameColored() + " fights defensivly and gains +" + this.getDefendBonus().ToString() + " to Dodge until next turn!";
	}

	// Token: 0x060002FA RID: 762 RVA: 0x0000E397 File Offset: 0x0000C597
	private int getDefendBonus()
	{
		return this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_DefendBonus);
	}

	// Token: 0x060002FB RID: 763 RVA: 0x0000E3A4 File Offset: 0x0000C5A4
	public string printNameLevelClass()
	{
		return string.Concat(new string[]
		{
			this.getName(),
			", Level ",
			this.getLevel().ToString(),
			" ",
			this.getClassName()
		});
	}

	// Token: 0x060002FC RID: 764 RVA: 0x0000E3F0 File Offset: 0x0000C5F0
	public string deathTrigger()
	{
		if (!this.deathTriggerFired)
		{
			this.deathTriggerFired = true;
			base.processString(this.getRawData().deathTrigger, this);
		}
		this.getAbilityTriggeredContainer().getTriggeredAbilityList().triggerDead(this);
		string result = this.getName() + " is out of this fight!\n";
		if (this.combatParty != null && !this.combatParty.isEmpty())
		{
			foreach (SkaldBaseObject skaldBaseObject in this.combatParty.getObjectList())
			{
				Character character = (Character)skaldBaseObject;
				if (character != this)
				{
					character.fireAllyDeadTrigger();
				}
			}
		}
		return result;
	}

	// Token: 0x060002FD RID: 765 RVA: 0x0000E4AC File Offset: 0x0000C6AC
	public void fireAllyDeadTrigger()
	{
		this.getAbilityTriggeredContainer().getTriggeredAbilityList().triggerAllyDead(this);
	}

	// Token: 0x060002FE RID: 766 RVA: 0x0000E4BF File Offset: 0x0000C6BF
	public string victoryTrigger()
	{
		return base.processString(this.getRawData().victoryTrigger, this);
	}

	// Token: 0x060002FF RID: 767 RVA: 0x0000E4D4 File Offset: 0x0000C6D4
	private bool shouldBeIndicatedAsPersonOfInterest()
	{
		SKALDProjectData.CharacterContainers.Character character = this.getRawData();
		return this.isMercenary() || this.isPC() || (this.isUnique() && (character.contactTrigger != "" || (!this.dynamicData.hasTouchedPlayer && character.firstTimeContactTrigger != "")));
	}

	// Token: 0x06000300 RID: 768 RVA: 0x0000E53C File Offset: 0x0000C73C
	public bool isMercenary()
	{
		SKALDProjectData.CharacterContainers.Character character = this.getRawData();
		return character != null && character.mercenary;
	}

	// Token: 0x06000301 RID: 769 RVA: 0x0000E55C File Offset: 0x0000C75C
	public int getMercenaryPrice()
	{
		float num = 1f;
		SKALDProjectData.CharacterContainers.Character character = this.getRawData();
		if (character != null)
		{
			num = character.mercenaryPriceModifier;
		}
		return Mathf.RoundToInt(500f * num);
	}

	// Token: 0x06000302 RID: 770 RVA: 0x0000E58C File Offset: 0x0000C78C
	public string processContactTrigger()
	{
		string firstTimeContactTrigger = this.getRawData().firstTimeContactTrigger;
		string contactTrigger = this.getRawData().contactTrigger;
		if (!this.dynamicData.hasTouchedPlayer && firstTimeContactTrigger != "")
		{
			string s = firstTimeContactTrigger;
			this.dynamicData.hasTouchedPlayer = true;
			return base.processString(s, this);
		}
		if (contactTrigger != "")
		{
			return base.processString(contactTrigger, this);
		}
		if (this.isMercenary())
		{
			PopUpControl.addHireMercPopUp(this);
		}
		return "";
	}

	// Token: 0x06000303 RID: 771 RVA: 0x0000E60C File Offset: 0x0000C80C
	public string processAlertTrigger()
	{
		string alertTrigger = this.getRawData().alertTrigger;
		return base.processString(alertTrigger, this);
	}

	// Token: 0x06000304 RID: 772 RVA: 0x0000E630 File Offset: 0x0000C830
	public string processClearAlertTrigger()
	{
		string clearAlertTrigger = this.getRawData().clearAlertTrigger;
		return base.processString(clearAlertTrigger, this);
	}

	// Token: 0x06000305 RID: 773 RVA: 0x0000E654 File Offset: 0x0000C854
	public override TextureTools.TextureData getModelTexture()
	{
		int currentFrame = this.getCurrentFrame();
		return this.getModelTexture(currentFrame, false);
	}

	// Token: 0x06000306 RID: 774 RVA: 0x0000E670 File Offset: 0x0000C870
	public bool isEquippedForMelee()
	{
		return this.getRange() <= 1;
	}

	// Token: 0x06000307 RID: 775 RVA: 0x0000E67E File Offset: 0x0000C87E
	public Inventory getInventory()
	{
		return this.dynamicData.inventory;
	}

	// Token: 0x06000308 RID: 776 RVA: 0x0000E68B File Offset: 0x0000C88B
	public void setInventory(Inventory inventory)
	{
		this.dynamicData.inventory = inventory;
		if (this.isPC() && this.dynamicData.inventory != null)
		{
			this.dynamicData.inventory.activateNewAdditionTagging();
		}
		this.itemSlots = null;
	}

	// Token: 0x06000309 RID: 777 RVA: 0x0000E6C8 File Offset: 0x0000C8C8
	public string debugTestToHit()
	{
		MainControl.debugFunctions = false;
		Character character = this.getTargetOpponent();
		if (character == null)
		{
			MainControl.logError("No target");
			return "No Target";
		}
		int num = 0;
		int numberOfToHitRerolls = this.getNumberOfToHitRerolls();
		for (int i = 0; i < 10000; i++)
		{
			if (new SkaldTestRandomVsRandom(this.getAttackSkillAndModifiers(null).getTotalValue(), "Attack", character.getDodgeSkill().getTotalValue(), "Dodge", numberOfToHitRerolls).wasSuccess())
			{
				num++;
			}
		}
		string text = "10000 - " + numberOfToHitRerolls.ToString() + " Re-Rolls: " + num.ToString();
		MainControl.log(text);
		return text;
	}

	// Token: 0x0600030A RID: 778 RVA: 0x0000E763 File Offset: 0x0000C963
	public TextureTools.TextureData getFixedTexture()
	{
		return this.getModelTexture(0, true);
	}

	// Token: 0x0600030B RID: 779 RVA: 0x0000E76D File Offset: 0x0000C96D
	public CharacterLooksControl getLooksControl()
	{
		return this.dynamicData.characterLooksControl;
	}

	// Token: 0x0600030C RID: 780 RVA: 0x0000E77C File Offset: 0x0000C97C
	private string buildCharacterBufferID(int frame)
	{
		if (this.bufferIdBuilder == null)
		{
			this.bufferIdBuilder = new StringBuilder(256);
		}
		this.bufferIdBuilder.Clear();
		this.bufferIdBuilder.Append(frame);
		this.bufferIdBuilder.Append(this.getModelPath());
		if (this.isCharacterMale())
		{
			this.bufferIdBuilder.Append("IsMale");
		}
		if (this.drawClothing())
		{
			this.bufferIdBuilder.Append(this.getItemSlots().getModelPathString());
			this.bufferIdBuilder.Append(this.getLooksControl().getHairStylePath());
			this.bufferIdBuilder.Append(this.getLooksControl().getBeardStylePath());
		}
		if (this.paletteSwap())
		{
			this.bufferIdBuilder.Append(this.getLooksControl().getColorBufferString());
		}
		if (this.isPaperDoll() && this.getCurrentWeapon() != null)
		{
			this.bufferIdBuilder.Append(this.getCurrentWeapon().getId());
		}
		if (this.isPaperDoll() && this.drawOverlandItem())
		{
			this.bufferIdBuilder.Append("Overland");
		}
		if (this.isPaperDoll() && this.getIdleItem() != null)
		{
			this.bufferIdBuilder.Append(this.getIdleItem().getModelPath());
		}
		if (!this.spriteFacesRight())
		{
			this.bufferIdBuilder.Append("Flip");
		}
		this.bufferIdBuilder.Append(this.isInCombat);
		return this.bufferIdBuilder.ToString();
	}

	// Token: 0x0600030D RID: 781 RVA: 0x0000E8F6 File Offset: 0x0000CAF6
	public void setAbilityUseAnimation(string animationId)
	{
		if (!this.isPaperDoll())
		{
			this.setDynamicAnimation("ANI_AttackBase");
			return;
		}
		this.setDynamicAnimation(animationId);
	}

	// Token: 0x0600030E RID: 782 RVA: 0x0000E914 File Offset: 0x0000CB14
	private void setWeaponAttackAnimation()
	{
		if (!this.isPaperDoll())
		{
			this.setDynamicAnimation("ANI_AttackBase");
			return;
		}
		ItemWeapon currentWeapon = this.getCurrentWeapon();
		if (currentWeapon != null)
		{
			this.setDynamicAnimation(currentWeapon.getAttackAnimation());
			return;
		}
		this.setDynamicAnimation("ANI_AttackPunch");
	}

	// Token: 0x0600030F RID: 783 RVA: 0x0000E958 File Offset: 0x0000CB58
	private void setWeaponAttackFinishAnimation()
	{
		if (!this.isPaperDoll())
		{
			this.setDynamicAnimation("ANI_AttackBaseFinish");
			return;
		}
		ItemWeapon currentWeapon = this.getCurrentWeapon();
		if (currentWeapon != null)
		{
			this.setDynamicAnimation(currentWeapon.getAttackFinishAnimation());
			return;
		}
		this.setDynamicAnimation("ANI_AttackPunchFinish");
	}

	// Token: 0x06000310 RID: 784 RVA: 0x0000E99C File Offset: 0x0000CB9C
	private void updateAimAnimation()
	{
		if (!this.isPaperDoll())
		{
			return;
		}
		ItemWeapon currentWeapon = this.getCurrentWeapon();
		if (currentWeapon != null)
		{
			this.dynamicData.characterAnimationControl.setAimingAnimation(currentWeapon.getAimingAnimation());
			return;
		}
		this.dynamicData.characterAnimationControl.setAimingAnimation("ANI_AimingUnarmed");
	}

	// Token: 0x06000311 RID: 785 RVA: 0x0000E9E8 File Offset: 0x0000CBE8
	private void updateIdleAnimation()
	{
		if (!this.isPaperDoll())
		{
			return;
		}
		ItemWeapon currentWeapon = this.getCurrentWeapon();
		if (currentWeapon != null)
		{
			string text = currentWeapon.getIdleAnimation();
			if (text == "")
			{
				text = "ANI_BaseCombat";
			}
			this.dynamicData.characterAnimationControl.setCombatAnimation(text);
			return;
		}
		this.dynamicData.characterAnimationControl.setCombatAnimation("ANI_IdleCombatUnarmed");
	}

	// Token: 0x06000312 RID: 786 RVA: 0x0000EA49 File Offset: 0x0000CC49
	private ItemIdle getIdleItem()
	{
		return this.getItemSlots().getIdleItem(this.getInventory());
	}

	// Token: 0x06000313 RID: 787 RVA: 0x0000EA5C File Offset: 0x0000CC5C
	private bool drawOverlandItem()
	{
		return this.isPC() && MainControl.getDataControl().currentMap != null && MainControl.getDataControl().currentMap.overland;
	}

	// Token: 0x06000314 RID: 788 RVA: 0x0000EA83 File Offset: 0x0000CC83
	private bool drawIdleItem()
	{
		return !this.drawOverlandItem() && !this.isInCombat && this.getIdleItem() != null;
	}

	// Token: 0x06000315 RID: 789 RVA: 0x0000EAA4 File Offset: 0x0000CCA4
	public int getXOffset()
	{
		SKALDProjectData.CharacterContainers.Character character = this.getRawData();
		int num = 0;
		if (character != null)
		{
			num = character.modelXOffset;
		}
		if (!this.isPaperDoll())
		{
			return Character.PADDING + num;
		}
		if (!this.isDead() || this.hasDynamaicAnimation())
		{
			return this.getXPadding() + num;
		}
		if (this.spriteFacesRight())
		{
			return 30;
		}
		return 0;
	}

	// Token: 0x06000316 RID: 790 RVA: 0x0000EAF9 File Offset: 0x0000CCF9
	private int getXPadding()
	{
		return Mathf.RoundToInt((float)(Character.PADDING * 3 / 2));
	}

	// Token: 0x06000317 RID: 791 RVA: 0x0000EB0C File Offset: 0x0000CD0C
	public int getYOffset()
	{
		if (this.isPaperDoll() && this.isDead() && !this.hasDynamaicAnimation())
		{
			return -8 + base.getCurrentHeight();
		}
		SKALDProjectData.CharacterContainers.Character character = this.getRawData();
		int num = 0;
		if (character != null)
		{
			num = character.modelYOffset;
		}
		return 4 + base.getCurrentHeight() + num;
	}

	// Token: 0x06000318 RID: 792 RVA: 0x0000EB58 File Offset: 0x0000CD58
	public bool basePhysicMovementComplete()
	{
		return base.physicMovementComplete();
	}

	// Token: 0x06000319 RID: 793 RVA: 0x0000EB60 File Offset: 0x0000CD60
	public bool isBackstabber()
	{
		return this.dynamicData.combatAbilityFlags.backStabber;
	}

	// Token: 0x0600031A RID: 794 RVA: 0x0000EB74 File Offset: 0x0000CD74
	public override bool physicMovementComplete()
	{
		return this.basePhysicMovementComplete() && !this.hasUpcomingAttack() && !HoverElementControl.hasTacticalText() && (!base.getMapTile().isSpotted() || (!this.dynamicData.characterAnimationControl.shouldIWaitForAnimation() && (this.barkControl == null || (!this.waitForBarks() && (this.getTargetOpponent() == null || !this.getTargetOpponent().waitForBarks()))) && !base.getVisualEffects().areAnyEffectsActive()));
	}

	// Token: 0x0600031B RID: 795 RVA: 0x0000EBF8 File Offset: 0x0000CDF8
	public bool waitForBarks()
	{
		return base.getMapTile().isSpotted() && this.getBarkControl().waitForBarks();
	}

	// Token: 0x0600031C RID: 796 RVA: 0x0000EC14 File Offset: 0x0000CE14
	private string getDeafultTorsoPath()
	{
		if (this.isCharacterMale())
		{
			return "Torsos/Torso";
		}
		return "Torsos/TorsoFemale";
	}

	// Token: 0x0600031D RID: 797 RVA: 0x0000EC29 File Offset: 0x0000CE29
	public bool spriteFacesRight()
	{
		return this.dynamicData.spriteFacesRight;
	}

	// Token: 0x0600031E RID: 798 RVA: 0x0000EC38 File Offset: 0x0000CE38
	private Dictionary<Color32, Color32> getColorSwapDictionary()
	{
		return this.getLooksControl().getSwapDictionary((this.getCurrentWeapon() != null) ? this.getCurrentWeapon().getPrimaryColor() : "", (this.getCurrentWeapon() != null) ? this.getCurrentWeapon().getSecondaryColor() : "", (this.getCurrentArmor() != null) ? this.getCurrentArmor().getPrimaryColor() : "", (this.getCurrentArmor() != null) ? this.getCurrentArmor().getSecondaryColor() : "");
	}

	// Token: 0x0600031F RID: 799 RVA: 0x0000ECB8 File Offset: 0x0000CEB8
	public string conditionallyAddAbility(string abilityId)
	{
		Ability ability = GameData.getAbility(abilityId);
		if (ability == null)
		{
			return "";
		}
		AbilityContainer abilityContainer = null;
		if (ability is AbilityCombatManeuver)
		{
			abilityContainer = this.getAbilityManueverContainer();
		}
		else if (ability is AbilityPassive)
		{
			abilityContainer = this.getAbilityPassiveContainer();
		}
		else if (ability is AbilityTriggered)
		{
			abilityContainer = this.getAbilityTriggeredContainer();
		}
		if (abilityContainer.hasComponent(abilityId))
		{
			return this.getName() + " already has Ability " + ability.getName();
		}
		this.addAbility(ability);
		return this.getName() + " gained Ability: " + ability.getName();
	}

	// Token: 0x06000320 RID: 800 RVA: 0x0000ED48 File Offset: 0x0000CF48
	public TextureTools.TextureData getModelTexture(int frame, bool forceWeapon = false)
	{
		string text = this.buildCharacterBufferID(frame);
		if (TextureTools.characterBufferContainsImage(text))
		{
			if (this.drawTexture == null)
			{
				this.drawTexture = new TextureTools.TextureData();
			}
			TextureTools.tryToGetCharacterBufferImage(text, this.drawTexture);
			return this.drawTexture;
		}
		TextureTools.TextureData textureData;
		if (!this.isPaperDoll())
		{
			TextureTools.TextureData subImageTextureData = TextureTools.getSubImageTextureData(frame, "Images/" + this.getModelPath());
			textureData = new TextureTools.TextureData(subImageTextureData.width + Character.PADDING * 2, subImageTextureData.height + Character.PADDING * 2);
			textureData.SetPixels(Character.PADDING, 0, subImageTextureData.width, subImageTextureData.height, subImageTextureData.colors);
			if (this.paletteSwap())
			{
				textureData.paletteSwap(this.getColorSwapDictionary());
			}
			if (!this.spriteFacesRight())
			{
				textureData.flipHorizontally();
			}
		}
		else
		{
			FrameOffsetControl.OffsetData globalOffset = this.dynamicData.characterAnimationControl.getGlobalOffset(frame);
			textureData = new TextureTools.TextureData(this.frameSize, this.frameSize);
			string str = "Images/Models/Bodies/";
			int xpadding = this.getXPadding();
			if (this.isPaperDoll() && this.getCurrentWeapon() != null && !this.isInCombat && !forceWeapon && (this.getCurrentWeapon().isTwoHanded() || this.isWeaponRanged()))
			{
				FrameOffsetControl.OffsetData torsoOffset = this.dynamicData.characterAnimationControl.getTorsoOffset(frame);
				FrameOffsetControl.OffsetData offsetData = new FrameOffsetControl.OffsetData(torsoOffset.x - 5, torsoOffset.y - 3, 5, torsoOffset.rotateLeft, torsoOffset.flipped);
				if (this.isWeaponRanged())
				{
					offsetData.x += 2;
					offsetData.y -= 2;
				}
				this.applyModelLayer(textureData, offsetData, this.getCurrentWeapon().getModelPath(), xpadding);
			}
			ItemShield currentShieldIfInHand = this.getCurrentShieldIfInHand();
			if (this.isPaperDoll() && currentShieldIfInHand != null && ((this.getCurrentLight() != null && !this.isInCombat) || this.dynamicData.characterAnimationControl.getShieldOffset(frame) == null))
			{
				FrameOffsetControl.OffsetData torsoOffset2 = this.dynamicData.characterAnimationControl.getTorsoOffset(frame);
				if (torsoOffset2 != null)
				{
					FrameOffsetControl.OffsetData offsetData2 = new FrameOffsetControl.OffsetData(torsoOffset2.x + 1 + currentShieldIfInHand.getBackXOffset(), torsoOffset2.y + currentShieldIfInHand.getBackYOffset(), 5, torsoOffset2.rotateLeft, torsoOffset2.flipped);
					this.applyModelLayer(textureData, offsetData2, currentShieldIfInHand.getModelPath(), xpadding);
				}
			}
			if (this.drawClothing() && this.getCurrentClothing() != null)
			{
				if (this.getCurrentClothing().getCloakPath() != "")
				{
					this.applyModelLayer(textureData, this.dynamicData.characterAnimationControl.getCloakOffset(frame), this.getCurrentClothing().getCloakPath(), xpadding);
				}
				this.applyModelLayer(textureData, this.dynamicData.characterAnimationControl.getLegsOffset(frame), this.getCurrentClothing().getLegsPath(), xpadding, str + "Legs/Legs");
				this.applyModelLayer(textureData, this.dynamicData.characterAnimationControl.getTorsoOffset(frame), this.getCurrentClothing().getTorsoPath(), xpadding, str + this.getDeafultTorsoPath());
			}
			else
			{
				this.applyModelLayer(textureData, this.dynamicData.characterAnimationControl.getLegsOffset(frame), str + "Legs/Legs", xpadding);
				this.applyModelLayer(textureData, this.dynamicData.characterAnimationControl.getTorsoOffset(frame), str + this.getDeafultTorsoPath(), xpadding);
			}
			if (this.isPaperDoll() && this.getCurrentArmor() != null)
			{
				this.applyModelLayer(textureData, this.dynamicData.characterAnimationControl.getTorsoOffset(frame), this.getCurrentArmor().getModelPath(), xpadding);
			}
			this.applyModelLayer(textureData, this.dynamicData.characterAnimationControl.getHeadOffset(frame), str + "Heads/Head", xpadding);
			if (this.isPaperDoll())
			{
				if (this.drawClothing() && this.getCurrentHeadwear() != null)
				{
					this.applyModelLayer(textureData, this.dynamicData.characterAnimationControl.getHeadOffset(frame), this.getCurrentHeadwear().getModelPath(), xpadding);
				}
				if (this.isCharacterMale() && (this.getCurrentHeadwear() == null || !this.getCurrentHeadwear().hidesBeard()) && this.getLooksControl().getBeardStylePath() != "")
				{
					FrameOffsetControl.OffsetData headOffset = this.dynamicData.characterAnimationControl.getHeadOffset(frame);
					if (headOffset != null)
					{
						FrameOffsetControl.OffsetData offsetData3 = new FrameOffsetControl.OffsetData(headOffset.x, headOffset.y - 1, frame, headOffset.rotateLeft, headOffset.flipped);
						this.applyModelLayer(textureData, offsetData3, "Images/Models/Beard/" + this.getLooksControl().getBeardStylePath(), xpadding);
					}
				}
				if ((this.getCurrentHeadwear() == null || !this.getCurrentHeadwear().hidesHair()) && this.getLooksControl().getHairStylePath() != "")
				{
					this.applyModelLayer(textureData, this.dynamicData.characterAnimationControl.getHeadOffset(frame), "Images/Models/Hair/" + this.getLooksControl().getHairStylePath(), xpadding);
				}
			}
			if (this.isPaperDoll() && this.getCurrentWeapon() != null && !this.isInCombat && !forceWeapon && !this.getCurrentWeapon().isTwoHanded())
			{
				FrameOffsetControl.OffsetData torsoOffset3 = this.dynamicData.characterAnimationControl.getTorsoOffset(frame);
				if (torsoOffset3 != null)
				{
					FrameOffsetControl.OffsetData offsetData4 = new FrameOffsetControl.OffsetData(torsoOffset3.x - 5, torsoOffset3.y - 3, 5, torsoOffset3.rotateLeft, torsoOffset3.flipped);
					offsetData4.x -= 2;
					offsetData4.y -= 10;
					this.applyModelLayer(textureData, offsetData4, this.getCurrentWeapon().getModelPath(), xpadding);
				}
			}
			if (this.drawClothing() && this.getCurrentClothing() != null)
			{
				this.applyModelLayer(textureData, this.dynamicData.characterAnimationControl.getArmsOffset(frame), this.getCurrentClothing().getArmsPath(), xpadding, str + "Arms/Arms");
			}
			else
			{
				this.applyModelLayer(textureData, this.dynamicData.characterAnimationControl.getArmsOffset(frame), str + "Arms/Arms", xpadding);
			}
			if (this.isPaperDoll() && this.getCurrentShieldIfInHand() != null && (this.getCurrentLight() == null || this.isInCombat))
			{
				this.applyModelLayer(textureData, this.dynamicData.characterAnimationControl.getShieldOffset(frame), this.getCurrentShieldIfInHand().getModelPath(), xpadding);
			}
			if (this.isPaperDoll() && this.getCurrentLight() != null && !this.isInCombat)
			{
				this.applyModelLayer(textureData, this.dynamicData.characterAnimationControl.getLightOffset(frame), this.getCurrentLight().getModelPath(), xpadding);
			}
			if (this.drawOverlandItem() && !forceWeapon)
			{
				this.applyModelLayer(textureData, this.dynamicData.characterAnimationControl.getBannerOffset(frame), "Images/Models/Items/Banner", xpadding);
				this.applyModelLayer(textureData, this.dynamicData.characterAnimationControl.getWeaponOffset(frame), "Images/Models/Items/Banner", xpadding);
			}
			else if (this.drawIdleItem() && !forceWeapon)
			{
				this.applyModelLayer(textureData, this.dynamicData.characterAnimationControl.getBannerOffset(frame), this.getIdleItem().getModelPath(), xpadding);
				this.applyModelLayer(textureData, this.dynamicData.characterAnimationControl.getWeaponOffset(frame), this.getIdleItem().getModelPath(), xpadding);
			}
			else if (this.isPaperDoll() && this.getCurrentWeapon() != null && (this.isInCombat || forceWeapon))
			{
				this.applyModelLayer(textureData, this.dynamicData.characterAnimationControl.getSwooshOffset(frame), "Images/Models/Items/Swoosh", xpadding);
				this.applyModelLayer(textureData, this.dynamicData.characterAnimationControl.getWeaponOffset(frame), this.getCurrentWeapon().getModelPath(), xpadding);
			}
			if (this.paletteSwap())
			{
				textureData.paletteSwap(this.getColorSwapDictionary());
			}
			if (globalOffset != null && globalOffset.rotateLeft)
			{
				textureData.rotateTextureLeft();
			}
			if (globalOffset != null && globalOffset.flipped)
			{
				textureData.flipHorizontally();
			}
			if (!this.spriteFacesRight())
			{
				textureData.flipHorizontally();
			}
		}
		TextureTools.addToCharacterBuffer(text, textureData);
		return textureData;
	}

	// Token: 0x06000321 RID: 801 RVA: 0x0000F509 File Offset: 0x0000D709
	public override int getEmitterX()
	{
		return base.getEmitterX() + 8;
	}

	// Token: 0x06000322 RID: 802 RVA: 0x0000F513 File Offset: 0x0000D713
	public override int getEmitterY()
	{
		return base.getEmitterY() + 16;
	}

	// Token: 0x06000323 RID: 803 RVA: 0x0000F520 File Offset: 0x0000D720
	private void applyModelLayer(TextureTools.TextureData target, FrameOffsetControl.OffsetData offsetData, string imagePath, int padding, string altPath)
	{
		try
		{
			if (offsetData != null)
			{
				TextureTools.TextureData textureData = null;
				if (imagePath != "")
				{
					textureData = TextureTools.getSubImageTextureData(offsetData.frame, imagePath);
				}
				if (textureData != null)
				{
					TextureTools.applyOverlay(target, textureData, padding + offsetData.x, offsetData.y);
				}
				else if (altPath != "")
				{
					this.applyModelLayer(target, offsetData, altPath, padding);
				}
			}
		}
		catch (Exception ex)
		{
			MainControl.logError("Could not draw to model from path " + imagePath + "\n\n" + ex.ToString());
		}
	}

	// Token: 0x06000324 RID: 804 RVA: 0x0000F5B4 File Offset: 0x0000D7B4
	private void applyModelLayer(TextureTools.TextureData target, FrameOffsetControl.OffsetData offsetData, string imagePath, int padding)
	{
		try
		{
			if (offsetData != null)
			{
				if (offsetData.flipped)
				{
					TextureTools.TextureData subImageTextureData = TextureTools.getSubImageTextureData(offsetData.frame, imagePath);
					if (subImageTextureData != null)
					{
						subImageTextureData.flipHorizontally();
						subImageTextureData.applyOverlay(padding + offsetData.x, offsetData.y, target);
					}
				}
				else
				{
					TextureTools.loadTextureDataAndApplyOverlay(imagePath, offsetData.frame, padding + offsetData.x, offsetData.y, target);
				}
			}
		}
		catch (Exception ex)
		{
			MainControl.logError("Could not draw to model from path " + imagePath + "\n\n" + ex.ToString());
		}
	}

	// Token: 0x06000325 RID: 805 RVA: 0x0000F648 File Offset: 0x0000D848
	public void printModelStrip(int length)
	{
		if (length < 1)
		{
			return;
		}
		List<TextureTools.TextureData> list = new List<TextureTools.TextureData>();
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < length; i++)
		{
			TextureTools.TextureData modelTexture = this.getModelTexture(i, false);
			if (modelTexture != null)
			{
				num += modelTexture.width;
				if (modelTexture.height > num2)
				{
					num2 = modelTexture.height;
				}
				list.Add(modelTexture);
			}
		}
		TextureTools.TextureData textureData = new TextureTools.TextureData(num, num2);
		int num3 = 0;
		for (int j = 0; j < length; j++)
		{
			textureData.SetPixels(num3, 0, list[j].width, list[j].height, list[j].colors);
			num3 += list[j].width;
		}
		Texture2D texture2D = new Texture2D(textureData.width, textureData.height);
		texture2D.filterMode = FilterMode.Point;
		textureData.bakeTexture2D(texture2D);
		try
		{
			byte[] bytes = ImageConversion.EncodeToPNG(texture2D);
			FileInfo fileInfo = new FileInfo(Application.persistentDataPath + "/imageStrip.png");
			fileInfo.Directory.Create();
			File.WriteAllBytes(fileInfo.FullName, bytes);
			MainControl.log("Printed model strip!");
		}
		catch (Exception obj)
		{
			MainControl.logError(obj);
		}
	}

	// Token: 0x06000326 RID: 806 RVA: 0x0000F780 File Offset: 0x0000D980
	public bool isTargetInRange()
	{
		if (this.getTargetOpponent() == null)
		{
			return false;
		}
		int range = this.getRange();
		return this.getRangeToTargetOpponent() <= (float)range;
	}

	// Token: 0x06000327 RID: 807 RVA: 0x0000F7AC File Offset: 0x0000D9AC
	public SkaldTestBase pickLock(int DC)
	{
		if (!this.getInventory().testItemGT("ITE_MiscLockpick", 0))
		{
			return new SkaldTestAutoFail(this.getName() + " does not have Thieves Tools!");
		}
		AudioControl.playLockpickSound();
		this.getInventory().deleteItem("ITE_MiscLockpick");
		SkaldTestRandomVsStatic skaldTestRandomVsStatic = new SkaldTestRandomVsStatic(this, AttributesControl.CoreAttributes.ATT_Thievery, DC, this.getAttributeCheckRerolls());
		skaldTestRandomVsStatic.appendToReturnString("\nSpends 1 Thieves Tools");
		return skaldTestRandomVsStatic;
	}

	// Token: 0x06000328 RID: 808 RVA: 0x0000F812 File Offset: 0x0000DA12
	public int getAttributeCheckRerolls()
	{
		if (!this.isPC())
		{
			return 0;
		}
		return GlobalSettings.getDifficultySettings().getPlayerAttributeRerolls();
	}

	// Token: 0x06000329 RID: 809 RVA: 0x0000F828 File Offset: 0x0000DA28
	public SkaldTestBase forceLock(int DC)
	{
		if (this.getVitality() <= 0)
		{
			return new SkaldTestAutoFail(this.getName() + " is too hurt to force a lock.");
		}
		SkaldTestRandomVsStatic skaldTestRandomVsStatic = new SkaldTestRandomVsStatic(this, AttributesControl.CoreAttributes.ATT_Athletics, DC, this.getAttributeCheckRerolls());
		skaldTestRandomVsStatic.appendToReturnString("\nForcing the lock makes a lot of noise!" + this.getName() + " loses 1 Vitality.");
		this.takeDamage(new Damage(1), true);
		return skaldTestRandomVsStatic;
	}

	// Token: 0x0600032A RID: 810 RVA: 0x0000F88B File Offset: 0x0000DA8B
	public int getRange()
	{
		if (this.getCurrentWeapon() != null)
		{
			return this.getCurrentWeapon().getRange();
		}
		return 1;
	}

	// Token: 0x0600032B RID: 811 RVA: 0x0000F8A2 File Offset: 0x0000DAA2
	public SKALDProjectData.CharacterContainers.Character getRawData()
	{
		if (this.rawData == null)
		{
			this.rawData = GameData.getCharacterRawData(this.getId());
		}
		return this.rawData;
	}

	// Token: 0x0600032C RID: 812 RVA: 0x0000F8C3 File Offset: 0x0000DAC3
	public float getRangeToTargetOpponent()
	{
		if (this.targetOpponent == null)
		{
			return 0f;
		}
		return NavigationTools.getLinearDistance(this.getTileX(), this.getTileY(), this.targetOpponent.getTileX(), this.targetOpponent.getTileY());
	}

	// Token: 0x0600032D RID: 813 RVA: 0x0000F8FA File Offset: 0x0000DAFA
	public bool hasSpellToAutoCast()
	{
		return this.getSpellContainer().tryToSetCombatAutoUseComponent(this);
	}

	// Token: 0x0600032E RID: 814 RVA: 0x0000F908 File Offset: 0x0000DB08
	public bool hasManueverToAutoUse()
	{
		return this.getAbilityManueverContainer().tryToSetCombatAutoUseComponent(this);
	}

	// Token: 0x0600032F RID: 815 RVA: 0x0000F916 File Offset: 0x0000DB16
	public bool shouldIMove()
	{
		return (this.isPanicked() && this.inMelee && this.hasRemainingCombatMovesOrAttacks()) || (this.hasRemainingCombatMovesOrAttacks() && !this.isTargetInRange() && !this.hasSpellToAutoCast() && !this.hasManueverToAutoUse());
	}

	// Token: 0x06000330 RID: 816 RVA: 0x0000F955 File Offset: 0x0000DB55
	public SkaldActionResult resolveActionAutomated()
	{
		if (this.hasSpellToAutoCast())
		{
			this.combatOrders.castSpell();
			return this.resolveAction();
		}
		if (this.hasManueverToAutoUse())
		{
			this.combatOrders.useAbility();
			return this.resolveAction();
		}
		return this.resolveAction();
	}

	// Token: 0x06000331 RID: 817 RVA: 0x0000F991 File Offset: 0x0000DB91
	public void repeatAction()
	{
		this.combatOrders.repeat();
	}

	// Token: 0x06000332 RID: 818 RVA: 0x0000F99E File Offset: 0x0000DB9E
	public bool hasRepeatableActions()
	{
		return this.combatOrders.hasRepeatableAction();
	}

	// Token: 0x06000333 RID: 819 RVA: 0x0000F9AB File Offset: 0x0000DBAB
	public void setWaitInCombat()
	{
		this.waitInCombat = true;
	}

	// Token: 0x06000334 RID: 820 RVA: 0x0000F9B4 File Offset: 0x0000DBB4
	public bool canHoldAction()
	{
		return !this.hasHeldActionThisTurn && !this.hasMoved();
	}

	// Token: 0x06000335 RID: 821 RVA: 0x0000F9CC File Offset: 0x0000DBCC
	public SkaldActionResult resolveAction()
	{
		if (this.waitInCombat)
		{
			this.waitInCombat = false;
			return new SkaldActionResult(true, true, "", true);
		}
		Character.CombatOrders.Orders orders = this.combatOrders.getCurrentOrder();
		if (orders == Character.CombatOrders.Orders.Repeating)
		{
			if (!this.combatOrders.hasRepeatableAction())
			{
				return new SkaldActionResult(false, false, "There is no valid action to repeat!", true);
			}
			orders = this.combatOrders.getLastAction();
		}
		this.hasActed = true;
		this.planning = false;
		if (this.isDead())
		{
			this.clearAllCombatMoves();
			return new SkaldActionResult(true, true, "", true);
		}
		if (this.passing)
		{
			this.clearAllCombatMoves();
			this.passing = false;
			this.autoResolveRemainingTurn = true;
			return new SkaldActionResult(true, true, this.getName() + " passes!", true);
		}
		if (!this.isAlert())
		{
			this.clearAllCombatMoves();
			this.setAlert();
			this.autoResolveRemainingTurn = true;
			return new SkaldActionResult(true, true, this.getName() + " recovers from surprise!", true);
		}
		if (this.getConditionContainer().isOccupied())
		{
			this.clearAllCombatMoves();
			this.autoResolveRemainingTurn = true;
			if (this.isPanicked())
			{
				this.addInfoBark("!");
			}
			else
			{
				this.addInfoBark("?");
			}
			return new SkaldActionResult(true, true, this.getName() + " is occupied!", true);
		}
		if (this.holdingAction)
		{
			this.clearAllCombatMoves();
			this.addInfoBark("Holding");
			return new SkaldActionResult(true, true, this.getName() + " Holds", this.getName() + " holds " + this.getHisHer("") + " action.", true);
		}
		if (orders == Character.CombatOrders.Orders.Defending)
		{
			this.clearAllCombatMoves();
			this.autoResolveRemainingTurn = true;
			this.getAbilityTriggeredContainer().getTriggeredAbilityList().triggerDefending(this);
			return new SkaldActionResult(true, true, this.getName() + " Defends", this.setDefendCondition(), true);
		}
		if (orders == Character.CombatOrders.Orders.Swap)
		{
			if (this.dynamicData.combatAbilityFlags.freeSwap)
			{
				this.decrementCombatMoves();
			}
			else
			{
				this.clearAllCombatMoves();
			}
			if (this.hasRemainingCombatMovesOrAttacks())
			{
				this.hasActed = false;
				this.planning = true;
				this.combatOrders.clearOrders();
				this.clearHidden();
			}
			else
			{
				this.autoResolveRemainingTurn = true;
			}
			this.addInfoBark("Shifting");
			return new SkaldActionResult(true, true, this.getName() + " Shifts ", this.getName() + " forces " + this.getHimHer("") + "self forwards!", true);
		}
		if (orders == Character.CombatOrders.Orders.Moving)
		{
			if (this.inMelee && !this.dynamicData.combatAbilityFlags.evasion)
			{
				this.clearAllCombatMoves();
				this.addInfoBark("Disengages");
				return new SkaldActionResult(true, true, this.getName() + " Disengages", this.getName() + " disengages from melee.", true);
			}
			this.decrementCombatMoves();
			if (this.hasRemainingCombatMovesOrAttacks())
			{
				this.hasActed = false;
				this.planning = true;
				this.combatOrders.clearOrders();
			}
			else
			{
				this.autoResolveRemainingTurn = true;
			}
			return new SkaldActionResult(true, true);
		}
		else
		{
			if (orders == Character.CombatOrders.Orders.UsingAbility)
			{
				SkaldActionResult skaldActionResult = this.getAbilityManueverContainer().useCurrentComponent(this);
				if (MainControl.debugFunctions)
				{
					MainControl.log(skaldActionResult.getResultString());
				}
				if (!skaldActionResult.wasPerformed())
				{
					this.dealWithIllegalOrder(skaldActionResult);
				}
				else if (this.hasRemainingCombatMovesOrAttacks())
				{
					this.hasActed = false;
					this.planning = true;
					this.combatOrders.clearOrders();
				}
				return skaldActionResult;
			}
			if (orders != Character.CombatOrders.Orders.CastingSpell)
			{
				this.combatOrders.attack();
				return this.beginAttack();
			}
			if (this.getSpellContainer().isEmpty())
			{
				SkaldActionResult skaldActionResult2 = new SkaldActionResult(false, true, this.getName() + " knows no spells!", true);
				this.dealWithIllegalOrder(skaldActionResult2);
				return skaldActionResult2;
			}
			if (!this.getSpellContainer().isCurrentComponentCombatActivated())
			{
				SkaldActionResult skaldActionResult3 = new SkaldActionResult(false, true, "This spell cannot be cast now!", true);
				this.dealWithIllegalOrder(skaldActionResult3);
				return skaldActionResult3;
			}
			this.clearHidden();
			SkaldActionResult skaldActionResult4 = this.getSpellContainer().useCurrentComponent(this);
			if (!skaldActionResult4.wasPerformed())
			{
				this.dealWithIllegalOrder(skaldActionResult4);
			}
			else if (this.hasRemainingCombatMovesOrAttacks())
			{
				this.hasActed = false;
				this.planning = true;
				this.combatOrders.clearOrders();
			}
			return skaldActionResult4;
		}
	}

	// Token: 0x06000336 RID: 822 RVA: 0x0000FDE2 File Offset: 0x0000DFE2
	private void dealWithIllegalOrder(SkaldActionResult action)
	{
		if (this.isPC())
		{
			this.combatOrders.clearOrders();
			PopUpControl.addPopUpOK(action.getResultString());
			return;
		}
		this.defend();
		MainControl.logError("AI was issued an illegal order: " + action.getResultString());
	}

	// Token: 0x06000337 RID: 823 RVA: 0x0000FE1E File Offset: 0x0000E01E
	public bool isMarked()
	{
		return this.getConditionContainer().isMarked();
	}

	// Token: 0x06000338 RID: 824 RVA: 0x0000FE2B File Offset: 0x0000E02B
	public void makeBloody()
	{
		if (this.getVitality() > 0 && this.getWounds() > 1)
		{
			this.takeDamage(new Damage(this.getVitality() + 1), true);
		}
	}

	// Token: 0x06000339 RID: 825 RVA: 0x0000FE53 File Offset: 0x0000E053
	public int getRetribution()
	{
		return this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_Retribution);
	}

	// Token: 0x0600033A RID: 826 RVA: 0x0000FE60 File Offset: 0x0000E060
	private string printStatusString()
	{
		string text = "";
		if (this.isDead())
		{
			text += "Knocked Out";
		}
		else
		{
			if (!this.isAlert())
			{
				text += "Not Alert, ";
			}
			if (this.isFlanked())
			{
				text += "Flanked, ";
			}
			if (this.getConditionContainer().isFlatFooted())
			{
				text += "Flat-Footed, ";
			}
			text = TextTools.removeTrailingComma(text);
		}
		if (text == "")
		{
			text = "Ready";
		}
		return text;
	}

	// Token: 0x0600033B RID: 827 RVA: 0x0000FEE5 File Offset: 0x0000E0E5
	public void resetTouchTrigger()
	{
		this.dynamicData.hasTouchedPlayer = false;
	}

	// Token: 0x0600033C RID: 828 RVA: 0x0000FEF4 File Offset: 0x0000E0F4
	public override string getInspectDescription()
	{
		if (!this.isSpotted() && !this.isPC())
		{
			return "It's too dark to make out anything!";
		}
		string text = C64Color.HEADER_TAG + this.getName().ToUpper() + C64Color.HEADER_CLOSING_TAG;
		text = text + "\nLvl. " + this.getLevel().ToString();
		if (this.getClassName() != "" && this.getClassName() != "Unknown")
		{
			text = text + " " + this.getClassName();
		}
		if (this.getRace().getName() != "" && this.getRace().getName() != "Unknown")
		{
			text = text + " (" + this.getRace().getName() + ")";
		}
		text = string.Concat(new string[]
		{
			text,
			"\n ",
			C64Color.GRAY_LIGHT_TAG,
			this.getFactionRelationships().printFactions(),
			"</color>"
		});
		if (this.isHostile())
		{
			text = text + " " + C64Color.RED_LIGHT_TAG + "(Hostile)</color>";
		}
		text = text + C64Color.GRAY_LIGHT_TAG + "\n\nStatus:</color> " + this.printStatusString();
		text = text + "\n\n" + TextTools.formateNameValuePairPlusMinus("To Hit", this.getAttackSkillAndModifiers(null).getTotalValue());
		text = text + "\n" + TextTools.formateNameValuePair("Damage", this.printDamageRange());
		text = text + "\n\n" + TextTools.formateNameValuePair("Vitality", this.getVitality());
		text = text + "\n" + TextTools.formateNameValuePair("Wounds", this.getWounds());
		text = text + "\n" + TextTools.formateNameValuePair("Soak", this.getSoakString());
		text = text + "\n\n" + TextTools.formateNameValuePairPlusMinus("Dodge", this.getDodgeSkill().getTotalValue());
		text = text + "\n" + TextTools.formateNameValuePairPlusMinus("Will.", this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_Will));
		text = text + "\n" + TextTools.formateNameValuePairPlusMinus("Tough.", this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_Toughness));
		text = text + "\n\n" + TextTools.formateNameValuePair("Move/Atc", this.getRemainingCombatMoves().ToString() + "/" + this.getRemainingAttacks().ToString());
		if (this.getCooldown() > 0)
		{
			text = text + "\n" + TextTools.formateNameValuePair("Cooldown", this.getCooldown());
		}
		string text2 = this.getConditionContainer().printListSingleLine();
		if (text2 != "")
		{
			text = string.Concat(new string[]
			{
				text,
				"\n\n",
				C64Color.GRAY_LIGHT_TAG,
				"CONDITIONS</color>\n",
				text2
			});
		}
		string text3 = this.attributes.printImmunityList();
		if (text3 != "")
		{
			text = string.Concat(new string[]
			{
				text,
				"\n\n",
				C64Color.GRAY_LIGHT_TAG,
				"IMMUNITIES</color>\n",
				text3
			});
		}
		string text4 = this.attributes.printResistanceList();
		if (text4 != "")
		{
			text = string.Concat(new string[]
			{
				text,
				"\n\n",
				C64Color.GRAY_LIGHT_TAG,
				"RESISTANCES</color>\n",
				text4
			});
		}
		string text5 = this.attributes.printVulnerabilityList();
		if (text5 != "")
		{
			text = string.Concat(new string[]
			{
				text,
				"\n\n",
				C64Color.GRAY_LIGHT_TAG,
				"VULNERABILITIES</color>\n",
				text5
			});
		}
		string text6 = this.getAbilityManueverContainer().printListSingleLine();
		if (text6 != "")
		{
			text = string.Concat(new string[]
			{
				text,
				"\n\n",
				C64Color.GRAY_LIGHT_TAG,
				"ABILITIES</color>\n",
				text6
			});
		}
		string text7 = this.getSpellContainer().printListSingleLine();
		if (text7 != "")
		{
			text = string.Concat(new string[]
			{
				text,
				"\n\n",
				C64Color.GRAY_LIGHT_TAG,
				"SPELLS</color>\n",
				text7
			});
		}
		return text;
	}

	// Token: 0x0600033D RID: 829 RVA: 0x00010334 File Offset: 0x0000E534
	public string printBasicCombatStats()
	{
		return "" + TextTools.formateNameValuePair("Vitality", this.getVitality()) + "\n" + TextTools.formateNameValuePair("Wounds", this.getWounds()) + "\n" + TextTools.formateNameValuePair("Soak", this.getSoakString()) + "\n" + TextTools.formateNameValuePairPlusMinus("Dodge", this.getDodgeSkill().getTotalValue()) + "\n";
	}

	// Token: 0x0600033E RID: 830 RVA: 0x000103B4 File Offset: 0x0000E5B4
	public string getCombatDescription()
	{
		if (this.getConditionContainer().isDefenceless())
		{
			return this.getFullNameUpper() + " is Defenceless.";
		}
		if (this.getConditionContainer().isFlatFooted())
		{
			return this.getFullNameUpper() + " is Flat-Footed.";
		}
		if (this.getConditionContainer().isOccupied())
		{
			return this.getFullNameUpper() + " is Occupied.";
		}
		if (this.getTargetOpponent() == null)
		{
			return this.getFullNameUpper() + " has no target.";
		}
		if (this.usingRangedWeaponInMelee())
		{
			return "Cannot use a ranged weapon in melee";
		}
		string text = C64Color.HEADER_TAG + this.getTargetOpponent().getName().ToUpper() + C64Color.HEADER_CLOSING_TAG + "\n";
		text += this.getTargetOpponent().printBasicCombatStats();
		if (!this.isTargetInRange())
		{
			text += "\nOut of range";
		}
		if (this.getTargetOpponent().isVulnerable())
		{
			text = text + "\n" + this.getColorTag() + "DEFENCELESS</color>";
		}
		return text;
	}

	// Token: 0x0600033F RID: 831 RVA: 0x000104B4 File Offset: 0x0000E6B4
	private SkaldActionResult getNoTargetResult()
	{
		if (!this.hasValidTarget())
		{
			if (this.getTargetOpponent() == null)
			{
				this.addInfoBark("No target!");
			}
			else if (!this.isTargetInRange())
			{
				this.addInfoBark("Can't reach target!");
			}
			else if (this.usingRangedWeaponInMelee())
			{
				this.addInfoBark("Cannot fire in melee!");
			}
		}
		return new SkaldActionResult(false, false, this.getName() + " does not have a valid target!", true);
	}

	// Token: 0x06000340 RID: 832 RVA: 0x0001051E File Offset: 0x0000E71E
	public List<string> getConferredConditionsFromItems()
	{
		return this.getItemSlots().getConferredConditions(this);
	}

	// Token: 0x06000341 RID: 833 RVA: 0x0001052C File Offset: 0x0000E72C
	public List<string> getConferredAbilitiesFromItems()
	{
		return this.getItemSlots().getConferredAbilities(this);
	}

	// Token: 0x06000342 RID: 834 RVA: 0x0001053A File Offset: 0x0000E73A
	public List<string> getConferredSpellsFromItems()
	{
		return this.getItemSlots().getConferredSpells(this);
	}

	// Token: 0x06000343 RID: 835 RVA: 0x00010548 File Offset: 0x0000E748
	private bool charging()
	{
		return this.getChargeCounterValue() > 0 && !this.isWeaponRanged();
	}

	// Token: 0x06000344 RID: 836 RVA: 0x0001055E File Offset: 0x0000E75E
	private int getChargeBonus()
	{
		if (!this.charging())
		{
			return 0;
		}
		return this.getChargeCounterValue() + this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_ChargeBonus);
	}

	// Token: 0x06000345 RID: 837 RVA: 0x00010579 File Offset: 0x0000E779
	private int getHiddenBonusToHit()
	{
		if (!this.isHidden())
		{
			return 0;
		}
		return 2;
	}

	// Token: 0x06000346 RID: 838 RVA: 0x00010586 File Offset: 0x0000E786
	private bool applyRangedInMeleePenalty()
	{
		return this.isWeaponRanged() && this.inMelee;
	}

	// Token: 0x06000347 RID: 839 RVA: 0x00010598 File Offset: 0x0000E798
	internal SkaldActionResult beginAttack()
	{
		return this.beginAttack(null);
	}

	// Token: 0x06000348 RID: 840 RVA: 0x000105A4 File Offset: 0x0000E7A4
	internal SkaldActionResult beginAttack(CharacterComponentContainer combatManuever)
	{
		if (!this.hasValidTarget())
		{
			return this.getNoTargetResult();
		}
		if (this.needsAmmo() && !this.hasAmmo())
		{
			if (this.isPC())
			{
				this.addInfoBark("Out of ammo!");
			}
			return new SkaldActionResult(false, false, this.getName() + " is out of ammo!", true);
		}
		this.upcomingAttack = new Character.AttackResolution(this, combatManuever);
		this.setWeaponAttackAnimation();
		if (this.hasRemainingCombatMovesOrAttacks())
		{
			this.planning = true;
		}
		return new SkaldActionResult(true, true);
	}

	// Token: 0x06000349 RID: 841 RVA: 0x00010628 File Offset: 0x0000E828
	public ItemAmmo payAmmoForAttack()
	{
		if (!this.needsAmmo())
		{
			return null;
		}
		if (!this.hasAmmo())
		{
			if (!this.isPC())
			{
				MainControl.logError(this.getId() + " is missing ammonition!");
			}
			return null;
		}
		ItemAmmo itemAmmo = this.getInventory().setCurrentItemAndRemove(this.getPreferredAmmoId()) as ItemAmmo;
		if (itemAmmo.isRecoverable() && this.isPC() && new SkaldTestPercentile(this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_ArrowRecoveryChance), "Arrow Recovery Chance", 1).wasSuccess())
		{
			MainControl.getDataControl().addItemToLootBuffer(itemAmmo);
		}
		return itemAmmo;
	}

	// Token: 0x0600034A RID: 842 RVA: 0x000106B3 File Offset: 0x0000E8B3
	public bool hasCondtion(string id)
	{
		return this.getConditionContainer().hasComponent(id);
	}

	// Token: 0x0600034B RID: 843 RVA: 0x000106C1 File Offset: 0x0000E8C1
	private bool needsAmmo()
	{
		return this.getCurrentWeapon() != null && this.isWeaponRanged() && !((this.getCurrentWeapon() as ItemRangedWeapon).getAmmoType() == "");
	}

	// Token: 0x0600034C RID: 844 RVA: 0x000106F8 File Offset: 0x0000E8F8
	private bool hasAmmo()
	{
		ItemRangedWeapon itemRangedWeapon = this.getCurrentWeapon() as ItemRangedWeapon;
		ItemAmmo currentAmmo = this.getCurrentAmmo();
		return currentAmmo != null && itemRangedWeapon != null && !(itemRangedWeapon.getAmmoType() != currentAmmo.getAmmoType()) && this.getInventory().testItemGT(currentAmmo.getId(), 0);
	}

	// Token: 0x0600034D RID: 845 RVA: 0x0001074C File Offset: 0x0000E94C
	public ItemAmmo getCurrentAmmo()
	{
		return this.getItemSlots().getAmmo(this.getInventory());
	}

	// Token: 0x0600034E RID: 846 RVA: 0x00010760 File Offset: 0x0000E960
	public int getCurrentAmmoCount()
	{
		ItemAmmo currentAmmo = this.getCurrentAmmo();
		if (currentAmmo == null)
		{
			return 0;
		}
		if (this.getInventory().getItemCount(currentAmmo.getId()) > 0)
		{
			return currentAmmo.getCount();
		}
		return 0;
	}

	// Token: 0x0600034F RID: 847 RVA: 0x00010795 File Offset: 0x0000E995
	public ItemRing getCurrentRing()
	{
		return this.getItemSlots().getRing(this.getInventory());
	}

	// Token: 0x06000350 RID: 848 RVA: 0x000107A8 File Offset: 0x0000E9A8
	public ItemNecklace getCurrentNecklace()
	{
		return this.getItemSlots().getNecklace(this.getInventory());
	}

	// Token: 0x06000351 RID: 849 RVA: 0x000107BB File Offset: 0x0000E9BB
	public void setPreferredAmmo(string ammoId)
	{
		this.dynamicData.preferredAmmo = ammoId;
	}

	// Token: 0x06000352 RID: 850 RVA: 0x000107C9 File Offset: 0x0000E9C9
	public string getPreferredAmmoId()
	{
		return this.dynamicData.preferredAmmo;
	}

	// Token: 0x06000353 RID: 851 RVA: 0x000107D6 File Offset: 0x0000E9D6
	public bool hasUpcomingAttack()
	{
		return this.upcomingAttack != null;
	}

	// Token: 0x06000354 RID: 852 RVA: 0x000107E1 File Offset: 0x0000E9E1
	public void clearUpcomingAttack()
	{
		this.upcomingAttack = null;
	}

	// Token: 0x06000355 RID: 853 RVA: 0x000107EC File Offset: 0x0000E9EC
	internal SkaldActionResult resolveAttack()
	{
		if (!this.hasValidTarget())
		{
			this.clearUpcomingAttack();
			return new SkaldActionResult(false, false, this.getName() + " does not have a valid target.", "Attack failed: " + this.getName() + " does not have a valid target.", true);
		}
		if (this.upcomingAttack != null)
		{
			SkaldActionResult result = this.upcomingAttack.resolveAttack();
			this.clearUpcomingAttack();
			return result;
		}
		return new SkaldActionResult(false, false, this.getName() + " does not have a valid target.", "Attack failed: " + this.getName() + " does not have a valid target.", true);
	}

	// Token: 0x06000356 RID: 854 RVA: 0x0001087C File Offset: 0x0000EA7C
	public bool isVulnerable()
	{
		return !this.isAlert() || this.getConditionContainer().isFlatFooted();
	}

	// Token: 0x06000357 RID: 855 RVA: 0x00010898 File Offset: 0x0000EA98
	public SkaldNumericContainer getAdjustedAttackSkill()
	{
		SkaldNumericContainer skaldNumericContainer = new SkaldNumericContainer();
		if (this.isWeaponRanged())
		{
			skaldNumericContainer.addEntryButIgnoreZero(this, AttributesControl.CoreAttributes.ATT_Ranged);
		}
		else
		{
			skaldNumericContainer.addEntryButIgnoreZero(this, AttributesControl.CoreAttributes.ATT_Melee);
		}
		if (this.getCurrentWeapon() == null)
		{
			skaldNumericContainer.addEntryButIgnoreZero(this, AttributesControl.CoreAttributes.ATT_HitWeaponUnarmed);
		}
		else
		{
			skaldNumericContainer.addEntryButIgnoreZero("Weapon Accuracy", this.getCurrentWeapon().getHitBonus());
			if (this.needsAmmo() && this.hasAmmo())
			{
				skaldNumericContainer.addEntryButIgnoreZero("Ammo Accuracy", this.getCurrentAmmo().getHitBonus());
			}
			if (this.isCurrentWeaponAxe())
			{
				skaldNumericContainer.addEntryButIgnoreZero(this, AttributesControl.CoreAttributes.ATT_HitWeaponAxes);
			}
			if (this.isCurrentWeaponSword())
			{
				skaldNumericContainer.addEntryButIgnoreZero(this, AttributesControl.CoreAttributes.ATT_HitWeaponSwords);
			}
			if (this.isCurrentWeaponClub())
			{
				skaldNumericContainer.addEntryButIgnoreZero(this, AttributesControl.CoreAttributes.ATT_HitWeaponClubs);
			}
			if (this.isCurrentWeaponBow())
			{
				skaldNumericContainer.addEntryButIgnoreZero(this, AttributesControl.CoreAttributes.ATT_HitWeaponBows);
			}
			if (this.isCurrentWeaponPolearm())
			{
				skaldNumericContainer.addEntryButIgnoreZero(this, AttributesControl.CoreAttributes.ATT_HitWeaponPolearms);
			}
			if (this.isCurrentWeaponLight())
			{
				skaldNumericContainer.addEntryButIgnoreZero(this, AttributesControl.CoreAttributes.ATT_HitWeaponLight);
			}
			if (this.isCurrentWeaponMedium())
			{
				skaldNumericContainer.addEntryButIgnoreZero(this, AttributesControl.CoreAttributes.ATT_HitWeaponMedium);
			}
			if (this.isCurrentWeaponHeavy())
			{
				skaldNumericContainer.addEntryButIgnoreZero(this, AttributesControl.CoreAttributes.ATT_HitWeaponHeavy);
			}
		}
		if (MainControl.debugFunctions)
		{
			MainControl.log("ATTACK SKILL FOR " + this.getName().ToUpper() + "\n" + skaldNumericContainer.printEntries());
		}
		return skaldNumericContainer;
	}

	// Token: 0x06000358 RID: 856 RVA: 0x000109D0 File Offset: 0x0000EBD0
	private SkaldNumericContainer getAttackSkillAndModifiers(AbilityCombatManeuver maneuver = null)
	{
		SkaldNumericContainer skaldNumericContainer = new SkaldNumericContainer();
		this.getAdjustedAttackSkill().transferToTargetContainer(skaldNumericContainer);
		if (this.getTargetOpponent() == null)
		{
			return skaldNumericContainer;
		}
		if (maneuver != null)
		{
			if (maneuver.addManeuverBonus())
			{
				skaldNumericContainer.addEntryButIgnoreZero(this, AttributesControl.CoreAttributes.ATT_HitCombatManeuvers);
			}
			skaldNumericContainer.addEntryButIgnoreZero("Maneuver", maneuver.getHitModifier());
		}
		if (this.getRangeToTargetOpponent() == 1f && this.getTargetOpponent().isFlanked())
		{
			skaldNumericContainer.addEntryButIgnoreZero(this, AttributesControl.CoreAttributes.ATT_FlankingBonus);
		}
		skaldNumericContainer.addEntryButIgnoreZero("Charging", this.getChargeBonus());
		skaldNumericContainer.addEntryButIgnoreZero("Hidden", this.getHiddenBonusToHit());
		if (this.applyRangedInMeleePenalty())
		{
			skaldNumericContainer.addEntryButIgnoreZero("Ranged in Melee", -2);
		}
		if (MainControl.debugFunctions)
		{
			MainControl.log("COMBAT MODIFIERS FOR " + this.getId().ToUpper() + "\n" + skaldNumericContainer.printEntries());
		}
		return skaldNumericContainer;
	}

	// Token: 0x06000359 RID: 857 RVA: 0x00010AA8 File Offset: 0x0000ECA8
	private float getCritMultiplier()
	{
		ItemWeapon currentWeapon = this.getCurrentWeapon();
		if (currentWeapon == null)
		{
			return 2f;
		}
		return currentWeapon.getCritMultiplier();
	}

	// Token: 0x0600035A RID: 858 RVA: 0x00010ACB File Offset: 0x0000ECCB
	public void setTargetOpponent(Character t)
	{
		if (!this.isNPCHostile(t))
		{
			MainControl.logError("Trying to set ally as target!");
			this.targetOpponent = null;
			return;
		}
		this.targetOpponent = t;
		if (this.targetOpponent == null)
		{
			return;
		}
		this.turnToTarget();
	}

	// Token: 0x0600035B RID: 859 RVA: 0x00010AFE File Offset: 0x0000ECFE
	public void turnToTarget()
	{
		if (this.targetOpponent != null)
		{
			this.turnToPoint(this.targetOpponent.getTileX(), this.targetOpponent.getTileY());
		}
	}

	// Token: 0x0600035C RID: 860 RVA: 0x00010B24 File Offset: 0x0000ED24
	public void turnToPoint(int x, int y)
	{
		if (this.getTileX() > x)
		{
			this.setFacing(3);
			return;
		}
		if (this.getTileX() < x)
		{
			this.setFacing(1);
			return;
		}
		if (this.getTileY() > y)
		{
			this.setFacing(2);
			return;
		}
		if (this.getTileY() < y)
		{
			this.setFacing(0);
		}
	}

	// Token: 0x0600035D RID: 861 RVA: 0x00010B74 File Offset: 0x0000ED74
	private int getCriticalChance()
	{
		int num = this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_Critical);
		if (this.getCurrentWeapon() != null)
		{
			num += this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_CriticalBonusUnarmed);
		}
		else if (this.isCurrentWeaponAxe())
		{
			num += this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_CriticalBonusAxes);
		}
		else if (this.isCurrentWeaponSword())
		{
			num += this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_CriticalBonusSwords);
		}
		else if (this.isCurrentWeaponClub())
		{
			num += this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_CriticalBonusClubs);
		}
		else if (this.isCurrentWeaponBow())
		{
			num += this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_CriticalBonusBows);
		}
		else if (this.isCurrentWeaponLight())
		{
			num += this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_CriticalBonusLight);
		}
		return num;
	}

	// Token: 0x0600035E RID: 862 RVA: 0x00010C07 File Offset: 0x0000EE07
	public bool testCritical()
	{
		return new SkaldTestPercentile(this.getCriticalChance(), "Critical Chance", 1).wasSuccess();
	}

	// Token: 0x0600035F RID: 863 RVA: 0x00010C20 File Offset: 0x0000EE20
	public string getCurrentWeaponName()
	{
		Item currentWeapon = this.getCurrentWeapon();
		if (currentWeapon == null)
		{
			return "Unarmed";
		}
		return currentWeapon.getName();
	}

	// Token: 0x06000360 RID: 864 RVA: 0x00010C44 File Offset: 0x0000EE44
	public string getCurrentArmorName()
	{
		Item currentArmor = this.getCurrentArmor();
		if (currentArmor == null)
		{
			return "No armor";
		}
		return currentArmor.getName();
	}

	// Token: 0x06000361 RID: 865 RVA: 0x00010C67 File Offset: 0x0000EE67
	public Character getTargetOpponent()
	{
		return this.targetOpponent;
	}

	// Token: 0x06000362 RID: 866 RVA: 0x00010C6F File Offset: 0x0000EE6F
	public void clearTargetOpponent()
	{
		this.targetOpponent = null;
	}

	// Token: 0x06000363 RID: 867 RVA: 0x00010C78 File Offset: 0x0000EE78
	public bool hasValidTarget()
	{
		return this.targetOpponent != null && !this.targetOpponent.isDead() && this.isTargetInRange() && !this.usingRangedWeaponInMelee() && !this.targetOpponent.isHidden();
	}

	// Token: 0x06000364 RID: 868 RVA: 0x00010CAF File Offset: 0x0000EEAF
	private bool usingRangedWeaponInMelee()
	{
		return this.isWeaponRanged() && this.inMelee && !this.dynamicData.combatAbilityFlags.pointBlankShot;
	}

	// Token: 0x06000365 RID: 869 RVA: 0x00010CD8 File Offset: 0x0000EED8
	public string clearAttributes(int str, int agi, int fort, int inte, int per)
	{
		this.attributes.clearAllSkillAndPrimaryRanks();
		this.attributes.setAttributeRank(AttributesControl.CoreAttributes.ATT_Strength, str);
		this.attributes.setAttributeRank(AttributesControl.CoreAttributes.ATT_Agility, agi);
		this.attributes.setAttributeRank(AttributesControl.CoreAttributes.ATT_Fortitude, fort);
		this.attributes.setAttributeRank(AttributesControl.CoreAttributes.ATT_Intellect, inte);
		this.attributes.setAttributeRank(AttributesControl.CoreAttributes.ATT_Presence, per);
		this.restoreAllAttributes();
		return "";
	}

	// Token: 0x06000366 RID: 870 RVA: 0x00010D3E File Offset: 0x0000EF3E
	public void restoreAllAttributes()
	{
		this.attributes.resetAllAttributesToAbsoluteMax();
	}

	// Token: 0x06000367 RID: 871 RVA: 0x00010D4B File Offset: 0x0000EF4B
	public int getCriticalHitsResistance()
	{
		return this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_ResConditionCriticalHits);
	}

	// Token: 0x06000368 RID: 872 RVA: 0x00010D55 File Offset: 0x0000EF55
	public bool testCriticalHitsResistance()
	{
		return new DicePoolPercentile("Critical Resistance").getResult() < this.getCriticalHitsResistance();
	}

	// Token: 0x06000369 RID: 873 RVA: 0x00010D70 File Offset: 0x0000EF70
	public override void updatePhysics()
	{
		base.updatePhysics();
		base.getVisualEffects().shouldYouWaitForParticlesToFinish();
		if (base.physicMovementComplete() && !base.getVisualEffects().shouldYouWaitForParticlesToFinish())
		{
			if (this.hasUpcomingAttack())
			{
				this.resolveAttack();
				return;
			}
			if (!this.hasDynamaicAnimation())
			{
				base.clearFlags();
			}
		}
	}

	// Token: 0x0600036A RID: 874 RVA: 0x00010DC2 File Offset: 0x0000EFC2
	public int getFoodRequiement()
	{
		if (GlobalSettings.getDifficultySettings().ignoreFood())
		{
			return 0;
		}
		return 10;
	}

	// Token: 0x0600036B RID: 875 RVA: 0x00010DD4 File Offset: 0x0000EFD4
	public bool shouldYouAutoResolveMyActions()
	{
		return !this.isPC() || this.getConditionContainer().isOccupied() || this.autoResolveRemainingTurn;
	}

	// Token: 0x0600036C RID: 876 RVA: 0x00010DF8 File Offset: 0x0000EFF8
	public int getPowerLevel()
	{
		int num = 0;
		num += this.attributes.getPowerLevel();
		num += this.getSpellContainer().getPowerLevel();
		num += this.getAbilityManueverContainer().getPowerLevel();
		if (this.getCurrentWeapon() != null)
		{
			num += this.getCurrentWeapon().getMaxDamage();
		}
		if (this.getCurrentArmor() != null)
		{
			num += this.getCurrentArmor().getSoak();
		}
		return num;
	}

	// Token: 0x0600036D RID: 877 RVA: 0x00010E5E File Offset: 0x0000F05E
	public void toggleMeleeWeapon()
	{
		if (this.dynamicData.preferMeleeWeapon)
		{
			return;
		}
		this.dynamicData.preferMeleeWeapon = true;
		if (this.getCurrentMeleeWeapon() != null)
		{
			this.getCurrentMeleeWeapon().playUseSound();
		}
	}

	// Token: 0x0600036E RID: 878 RVA: 0x00010E8D File Offset: 0x0000F08D
	public void toggleRangedWeapon()
	{
		if (!this.dynamicData.preferMeleeWeapon)
		{
			return;
		}
		this.dynamicData.preferMeleeWeapon = false;
		if (this.getCurrentRangedWeapon() != null)
		{
			this.getCurrentRangedWeapon().playUseSound();
		}
	}

	// Token: 0x0600036F RID: 879 RVA: 0x00010EBC File Offset: 0x0000F0BC
	public void toggleWeaponPreference()
	{
		if (this.dynamicData.preferMeleeWeapon)
		{
			this.toggleRangedWeapon();
			return;
		}
		this.toggleMeleeWeapon();
	}

	// Token: 0x06000370 RID: 880 RVA: 0x00010ED8 File Offset: 0x0000F0D8
	public void toggleOptimalWeapon()
	{
		if (this.inMelee)
		{
			this.toggleMeleeWeapon();
			return;
		}
		if (this.getCurrentRangedWeapon() == null)
		{
			this.toggleMeleeWeapon();
			return;
		}
		if (this.hasAmmo())
		{
			this.toggleRangedWeapon();
			return;
		}
		this.toggleMeleeWeapon();
	}

	// Token: 0x06000371 RID: 881 RVA: 0x00010F10 File Offset: 0x0000F110
	public ItemWeapon getCurrentWeapon()
	{
		if (!this.dynamicData.preferMeleeWeapon)
		{
			ItemWeapon currentRangedWeapon = this.getCurrentRangedWeapon();
			if (currentRangedWeapon != null)
			{
				return currentRangedWeapon;
			}
		}
		return this.getCurrentMeleeWeapon();
	}

	// Token: 0x06000372 RID: 882 RVA: 0x00010F3C File Offset: 0x0000F13C
	public ItemWeapon getCurrentMeleeWeapon()
	{
		return this.getItemSlots().getMeleeWeapon(this.getInventory());
	}

	// Token: 0x06000373 RID: 883 RVA: 0x00010F4F File Offset: 0x0000F14F
	public TextureTools.TextureData getCurrentMeleeWeaponBaseIcon()
	{
		if (this.getCurrentMeleeWeapon() != null)
		{
			return this.getCurrentMeleeWeapon().getBaseIcon();
		}
		return TextureTools.loadTextureData("Images/InventoryIcons/Fist");
	}

	// Token: 0x06000374 RID: 884 RVA: 0x00010F70 File Offset: 0x0000F170
	public TextureTools.TextureData getCurrentRangedWeaponBaseIcon()
	{
		if (this.getCurrentRangedWeapon() != null)
		{
			TextureTools.TextureData baseIcon = this.getCurrentRangedWeapon().getBaseIcon();
			int currentAmmoCount = this.getCurrentAmmoCount();
			if (currentAmmoCount < 10)
			{
				TextureTools.TextureData textureData = TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/Digits/" + currentAmmoCount.ToString());
				if (textureData != null)
				{
					textureData.applyOverlay(10, 0, baseIcon);
				}
			}
			else
			{
				TextureTools.applyOverlay(baseIcon, Item.plussIcon);
			}
			return baseIcon;
		}
		return null;
	}

	// Token: 0x06000375 RID: 885 RVA: 0x00010FD1 File Offset: 0x0000F1D1
	public ItemWeapon getCurrentRangedWeapon()
	{
		return this.getItemSlots().getRangedWeapon(this.getInventory());
	}

	// Token: 0x06000376 RID: 886 RVA: 0x00010FE4 File Offset: 0x0000F1E4
	public ItemClothing getCurrentClothing()
	{
		return this.getItemSlots().getClothing(this.getInventory());
	}

	// Token: 0x06000377 RID: 887 RVA: 0x00010FF7 File Offset: 0x0000F1F7
	public ItemShield getCurrentShieldRegardlessIfWorn()
	{
		return this.getItemSlots().getShield(this.getInventory());
	}

	// Token: 0x06000378 RID: 888 RVA: 0x0001100A File Offset: 0x0000F20A
	public ItemShield getCurrentShieldIfInHand()
	{
		if (this.getCurrentWeapon() != null && this.getCurrentWeapon().isTwoHanded())
		{
			return null;
		}
		return this.getItemSlots().getShield(this.getInventory());
	}

	// Token: 0x06000379 RID: 889 RVA: 0x00011034 File Offset: 0x0000F234
	public ItemHeadWear getCurrentHeadwear()
	{
		return this.getItemSlots().getHeadWear(this.getInventory());
	}

	// Token: 0x0600037A RID: 890 RVA: 0x00011047 File Offset: 0x0000F247
	public ItemLight getCurrentLight()
	{
		return this.getItemSlots().getLight(this.getInventory());
	}

	// Token: 0x0600037B RID: 891 RVA: 0x0001105A File Offset: 0x0000F25A
	public ItemArmor getCurrentArmor()
	{
		return this.getItemSlots().getArmor(this.getInventory());
	}

	// Token: 0x0600037C RID: 892 RVA: 0x0001106D File Offset: 0x0000F26D
	public int getWounds()
	{
		return this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_Wounds);
	}

	// Token: 0x0600037D RID: 893 RVA: 0x00011076 File Offset: 0x0000F276
	public int getVitality()
	{
		return this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_Vitality);
	}

	// Token: 0x0600037E RID: 894 RVA: 0x0001107F File Offset: 0x0000F27F
	public string getCurrentSpellFullDescription()
	{
		return this.getSpellContainer().getCurrentObjectFullDescriptionAndHeader();
	}

	// Token: 0x0600037F RID: 895 RVA: 0x0001108C File Offset: 0x0000F28C
	public void resurrect()
	{
		if (this.isDead())
		{
			this.setAttribute(AttributesControl.CoreAttributes.ATT_Wounds, 1);
		}
	}

	// Token: 0x06000380 RID: 896 RVA: 0x0001109E File Offset: 0x0000F29E
	public int getMaxWounds()
	{
		return this.attributes.getMaxAttributeValue(AttributesControl.CoreAttributes.ATT_Wounds);
	}

	// Token: 0x06000381 RID: 897 RVA: 0x000110AC File Offset: 0x0000F2AC
	public int getMaxVitality()
	{
		return this.attributes.getMaxAttributeValue(AttributesControl.CoreAttributes.ATT_Vitality);
	}

	// Token: 0x06000382 RID: 898 RVA: 0x000110BA File Offset: 0x0000F2BA
	public string printInitiativeStatus()
	{
		if (this.isDead())
		{
			return "KO";
		}
		if (this.hasActed)
		{
			return "Acted";
		}
		return "Ready";
	}

	// Token: 0x06000383 RID: 899 RVA: 0x000110DD File Offset: 0x0000F2DD
	public void kill()
	{
		this.attributes.setAttributeCurrentValue(AttributesControl.CoreAttributes.ATT_Vitality, 0);
		this.attributes.setAttributeCurrentValue(AttributesControl.CoreAttributes.ATT_Wounds, 0);
		this.deathTrigger();
	}

	// Token: 0x06000384 RID: 900 RVA: 0x00011100 File Offset: 0x0000F300
	public bool isMainCharacter()
	{
		return this.dynamicData.isMainCharacter;
	}

	// Token: 0x06000385 RID: 901 RVA: 0x00011110 File Offset: 0x0000F310
	public bool setMainCharacter()
	{
		return this.dynamicData.isMainCharacter = true;
	}

	// Token: 0x06000386 RID: 902 RVA: 0x0001112C File Offset: 0x0000F32C
	public bool isPanicked()
	{
		return this.getConditionContainer().isPanicked();
	}

	// Token: 0x06000387 RID: 903 RVA: 0x00011139 File Offset: 0x0000F339
	public void takeDamage(int amount, List<string> type)
	{
		this.takeDamage(new Damage(amount, type), true);
	}

	// Token: 0x06000388 RID: 904 RVA: 0x0001114C File Offset: 0x0000F34C
	public MapTile getAreaOfEffectBaseTile()
	{
		MapTile areaEffectBaseTile = this.getSpellContainer().getAreaEffectBaseTile();
		if (areaEffectBaseTile == null)
		{
			areaEffectBaseTile = this.getAbilityManueverContainer().getAreaEffectBaseTile();
		}
		if (areaEffectBaseTile == null)
		{
			areaEffectBaseTile = this.getAbilityPassiveContainer().getAreaEffectBaseTile();
		}
		return areaEffectBaseTile;
	}

	// Token: 0x06000389 RID: 905 RVA: 0x00011184 File Offset: 0x0000F384
	public void takeDamage(Damage damageObject, bool addBark = true)
	{
		damageObject.modulateDamageByResistance(this);
		int num = damageObject.getAmount();
		CombatLog.addEntry(this.getNameColored() + " took damage: " + num.ToString(), damageObject.getVerboseResultString());
		if (num <= 0)
		{
			num = 0;
		}
		if (this.isDead())
		{
			num = 0;
		}
		if (addBark)
		{
			foreach (string text in damageObject.getResultString())
			{
				this.addNegativeBark(text);
				if (MainControl.debugFunctions)
				{
					MainControl.log(C64Color.WHITE_TAG + this.getId() + "</color> took DMG: " + text);
				}
			}
		}
		if (num == 0)
		{
			return;
		}
		base.getVisualEffects().setDamageFlashCounter();
		AbilityContainerTriggered.TriggeredAbilityContainer triggeredAbilityList = this.getAbilityTriggeredContainer().getTriggeredAbilityList();
		triggeredAbilityList.triggerAnyDamage(this);
		AttributesControl.CoreAttributes name = AttributesControl.CoreAttributes.ATT_Vitality;
		AttributesControl.CoreAttributes name2 = AttributesControl.CoreAttributes.ATT_Wounds;
		int num2 = 0;
		if (this.getVitality() > num)
		{
			this.attributes.addToAttributeCurrentValue(name, 0 - num);
		}
		else if (this.getVitality() > 0)
		{
			num2 = num - this.getVitality();
			this.attributes.setAttributeCurrentValue(name, 0);
		}
		else
		{
			num2 = num;
		}
		if (num2 > 0)
		{
			if (num2 < this.getWounds() && !this.isBloodied())
			{
				this.fireEffectOnSelf("EFF_CauseInjury");
				this.addNegativeBark("Bloodied!");
				this.rollMoral();
				triggeredAbilityList.triggerWoundDamage(this);
			}
			this.attributes.addToAttributeCurrentValue(name2, 0 - num2);
			base.getVisualEffects().setBlood();
			if (damageObject.isSlashing())
			{
				AudioControl.playRandomSound(Character.defaultSevereSlashingHitSounds);
			}
			else
			{
				AudioControl.playRandomSound(Character.defaultSevereGenericHitSounds);
			}
		}
		else if (this.getCurrentArmor() != null && damageObject.hasSoaked())
		{
			if (this.getCurrentArmor().isHeavy())
			{
				AudioControl.playRandomSound(Character.defaultLightHitSoundsHeavyArmor);
			}
			else
			{
				AudioControl.playRandomSound(Character.defaultLightHitSoundsLightArmor);
			}
		}
		else
		{
			AudioControl.playRandomSound(Character.defaultLightHitSounds);
		}
		if (GlobalSettings.getDifficultySettings().cannotDie() && this.isMainCharacter() && this.getWounds() <= 0)
		{
			this.attributes.setAttributeCurrentValue(name2, 1);
		}
		if (this.isPaperDoll())
		{
			if (this.isInCombat && this.isAlert())
			{
				this.setDynamicAnimation("ANI_WoundedComplex");
			}
			else
			{
				this.setDynamicAnimation("ANI_WoundedComplexUnarmed");
			}
		}
		else
		{
			this.setDynamicAnimation("ANI_BaseWounded");
		}
		if (this.isDead())
		{
			this.playDeathSound();
			base.getVisualEffects().setShaken();
			this.fireEffectOnSelf("EFF_CauseInjury");
			if (this.isPC())
			{
				CombatLog.addEntry(this.getNameColored() + " is Knocked Out");
			}
			else
			{
				CombatLog.addEntry(this.getNameColored() + " is Dead");
			}
			if (this.combatParty != null)
			{
				this.combatParty.rollMoral();
			}
			this.deathTrigger();
		}
	}

	// Token: 0x0600038A RID: 906 RVA: 0x00011438 File Offset: 0x0000F638
	private void playDeathSound()
	{
		string text = "";
		SKALDProjectData.CharacterContainers.Character character = this.getRawData();
		if (character != null && character.deathSound != "")
		{
			text = character.deathSound;
		}
		if (text == "" && this.getRace() != null)
		{
			text = this.getRace().getDeathSound();
		}
		if (text == "")
		{
			AudioControl.playRandomSound(Character.defaultDeathSounds);
			return;
		}
		AudioControl.playSound(text);
	}

	// Token: 0x0600038B RID: 907 RVA: 0x000114AD File Offset: 0x0000F6AD
	public string getNameColored()
	{
		return this.getColorTag() + this.getName() + "</color>";
	}

	// Token: 0x0600038C RID: 908 RVA: 0x000114C8 File Offset: 0x0000F6C8
	public void fireEffectOnSelf(string effectId)
	{
		Effect effect = GameData.getEffect(effectId);
		if (effect != null)
		{
			effect.fireEffect(this, this);
		}
	}

	// Token: 0x0600038D RID: 909 RVA: 0x000114E8 File Offset: 0x0000F6E8
	public string getNameVitality()
	{
		return string.Concat(new string[]
		{
			this.getColorTag(),
			this.getName().ToUpper(),
			" (",
			this.getVitality().ToString(),
			" Wounds)</color>"
		});
	}

	// Token: 0x0600038E RID: 910 RVA: 0x00011538 File Offset: 0x0000F738
	public string getFullNameUpper()
	{
		return this.getColorTag() + this.getName().ToUpper() + "</color>";
	}

	// Token: 0x0600038F RID: 911 RVA: 0x00011555 File Offset: 0x0000F755
	public bool isDead()
	{
		return this.getWounds() <= 0;
	}

	// Token: 0x06000390 RID: 912 RVA: 0x00011563 File Offset: 0x0000F763
	public int getCurrentVitalityDamage()
	{
		return this.attributes.getMaxAttributeValue(AttributesControl.CoreAttributes.ATT_Vitality) - this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_Vitality);
	}

	// Token: 0x06000391 RID: 913 RVA: 0x0001157C File Offset: 0x0000F77C
	public string addVitality(int vitality)
	{
		int currentVitalityDamage = this.getCurrentVitalityDamage();
		this.attributes.addToAttributeCurrentValue(AttributesControl.CoreAttributes.ATT_Vitality, vitality);
		string text;
		if (currentVitalityDamage == 0)
		{
			text = "+0 Vitality.";
		}
		else if (currentVitalityDamage >= vitality)
		{
			text = "+" + vitality.ToString() + " Vitality.";
		}
		else
		{
			text = "+" + currentVitalityDamage.ToString() + " Vitality.";
		}
		this.addPositiveBark(text);
		CombatLog.addEntry(this.getNameColored() + ": " + text, this.getNameColored() + " healed " + text);
		return text;
	}

	// Token: 0x06000392 RID: 914 RVA: 0x0001160B File Offset: 0x0000F80B
	public void addToAttributeCurrentValue(string attributeId, int amount)
	{
		this.attributes.addToAttributeCurrentValue(attributeId, amount);
	}

	// Token: 0x06000393 RID: 915 RVA: 0x0001161A File Offset: 0x0000F81A
	public void addAttunement(int attunement)
	{
		this.attributes.addToAttributeCurrentValue(AttributesControl.CoreAttributes.ATT_Attunement, attunement);
	}

	// Token: 0x06000394 RID: 916 RVA: 0x0001162C File Offset: 0x0000F82C
	public void payAttunement(int attunement)
	{
		if (attunement <= 0)
		{
			return;
		}
		AttributesControl.CoreAttributes name = AttributesControl.CoreAttributes.ATT_Attunement;
		this.attributes.addToAttributeCurrentValue(name, -attunement);
	}

	// Token: 0x06000395 RID: 917 RVA: 0x0001164E File Offset: 0x0000F84E
	public string printGender()
	{
		if (this.dynamicData.isMale)
		{
			return "Male";
		}
		return "Female";
	}

	// Token: 0x06000396 RID: 918 RVA: 0x00011668 File Offset: 0x0000F868
	public SkaldDataList getInfoListOfDescriptors()
	{
		SkaldDataList skaldDataList = new SkaldDataList("", "");
		skaldDataList.addEntry("RACE", string.Concat(new string[]
		{
			C64Color.YELLOW_TAG,
			this.printGender(),
			" ",
			this.getRace().getName(),
			"</color>"
		}), "", this.getRace().getFullDescription());
		skaldDataList.addEntry("CLASS", C64Color.YELLOW_TAG + this.getClassName() + "</color>", "", this.getClass().getFullDescription());
		if (this.getBackground() != null)
		{
			skaldDataList.addEntry("BACKGROUND", C64Color.YELLOW_TAG + this.getBackground().getName() + "</color>", "", this.getBackground().getFullDescription());
		}
		else
		{
			skaldDataList.addEntry("BACKGROUND", "-", "", "No background has been selected.");
		}
		skaldDataList.addEntry("LEVEL", C64Color.YELLOW_TAG + "Level " + this.getLevel().ToString() + "</color>", "", "The current level of this character.");
		if (this.hasReachedLevelCap())
		{
			skaldDataList.addEntry("XP", C64Color.YELLOW_TAG + "Max Level</color>", "", "The character has reached the Level Cap and cannot advance further.");
		}
		else
		{
			skaldDataList.addEntry("XP", C64Color.YELLOW_TAG + this.getXPToGo().ToString() + " Xp To Go</color>", "", "The amount of experience to go until the character levels up.");
		}
		return skaldDataList;
	}

	// Token: 0x06000397 RID: 919 RVA: 0x000117FB File Offset: 0x0000F9FB
	public bool hasReachedLevelCap()
	{
		return (this.isPC() || this.isRecruitable()) && this.getLevel() >= GameData.getLevelCap();
	}

	// Token: 0x06000398 RID: 920 RVA: 0x0001181F File Offset: 0x0000FA1F
	public SkaldDataList getListOfConditions()
	{
		return this.getConditionContainer().getDataList();
	}

	// Token: 0x06000399 RID: 921 RVA: 0x0001182C File Offset: 0x0000FA2C
	public SkaldDataList getListOfPrimaryAttributes()
	{
		return this.attributes.getInfoListOfPrimaryAttributes();
	}

	// Token: 0x0600039A RID: 922 RVA: 0x00011839 File Offset: 0x0000FA39
	public SkaldDataList getListOfSecondaryAttributes()
	{
		return this.attributes.getInfoListOfSecondaryAttributes();
	}

	// Token: 0x0600039B RID: 923 RVA: 0x00011846 File Offset: 0x0000FA46
	public SkaldDataList getListOfCombatStats()
	{
		return this.attributes.getInfoListOfCombatAttributes();
	}

	// Token: 0x0600039C RID: 924 RVA: 0x00011854 File Offset: 0x0000FA54
	public string printInventorySummary()
	{
		string str = "" + TextTools.formateNameValuePairPlusMinus("To Hit", this.getAdjustedAttackSkill().getTotalValue()) + "\n" + TextTools.formateNameValuePairSoft("Damage", this.printDamageRange()) + "\n" + TextTools.formateNameValuePairSoft("Soak", this.getSoakString()) + "\n" + TextTools.formateNameValuePairPlusMinus("Dodge", this.getDodgeSkill().getTotalValue()) + "\n";
		int armorEncumberance = this.getArmorEncumberance();
		string text = armorEncumberance.ToString();
		if (armorEncumberance != 0)
		{
			text = "-" + text;
		}
		return str + TextTools.formateNameValuePair("Encumb.", text) + "\n";
	}

	// Token: 0x0600039D RID: 925 RVA: 0x0001190B File Offset: 0x0000FB0B
	public SkaldDataList getListOfSkills()
	{
		return this.attributes.getInfoListOfSkills();
	}

	// Token: 0x0600039E RID: 926 RVA: 0x00011918 File Offset: 0x0000FB18
	public SkaldDataList getListOfSpellSchools()
	{
		return this.attributes.getInfoListOfSpellSchools();
	}

	// Token: 0x0600039F RID: 927 RVA: 0x00011925 File Offset: 0x0000FB25
	public SkaldDataList getListOfMagicAttributes()
	{
		return this.attributes.getInfoListOfMagicAttributes();
	}

	// Token: 0x060003A0 RID: 928 RVA: 0x00011932 File Offset: 0x0000FB32
	public SkaldDataList getListOfDefences()
	{
		return this.attributes.getInfoListOfDefences();
	}

	// Token: 0x060003A1 RID: 929 RVA: 0x0001193F File Offset: 0x0000FB3F
	public bool isCharacterTarget(Character c)
	{
		return this.getTargetOpponent() != null && this.getTargetOpponent() == c;
	}

	// Token: 0x060003A2 RID: 930 RVA: 0x00011957 File Offset: 0x0000FB57
	public int getCurrentAttributeValue(AttributesControl.CoreAttributes name)
	{
		return this.attributes.getCurrentAttributeValue(name);
	}

	// Token: 0x060003A3 RID: 931 RVA: 0x00011965 File Offset: 0x0000FB65
	public int getCurrentAttributeValueStatic(AttributesControl.CoreAttributes name)
	{
		return this.attributes.getCurrentAttributeValue(name) + 7;
	}

	// Token: 0x060003A4 RID: 932 RVA: 0x00011975 File Offset: 0x0000FB75
	public int getCurrentAttributeValueStatic(string name)
	{
		return this.attributes.getCurrentAttributeValue(name) + 7;
	}

	// Token: 0x060003A5 RID: 933 RVA: 0x00011985 File Offset: 0x0000FB85
	public int getCurrentAttributeValue(string name)
	{
		return this.attributes.getCurrentAttributeValue(name);
	}

	// Token: 0x060003A6 RID: 934 RVA: 0x00011993 File Offset: 0x0000FB93
	public int getAttributeRank(string name)
	{
		return this.attributes.getAttributeRank(name);
	}

	// Token: 0x060003A7 RID: 935 RVA: 0x000119A1 File Offset: 0x0000FBA1
	public void setAttribute(AttributesControl.CoreAttributes name, int value)
	{
		this.attributes.setAttributeCurrentValue(name, value);
	}

	// Token: 0x060003A8 RID: 936 RVA: 0x000119B0 File Offset: 0x0000FBB0
	public void setAttribute(string name, int value)
	{
		this.attributes.setAttributeCurrentValue(name, value);
	}

	// Token: 0x060003A9 RID: 937 RVA: 0x000119BF File Offset: 0x0000FBBF
	public void addToAttribute(AttributesControl.CoreAttributes name, int value)
	{
		this.attributes.addToAttributeCurrentValue(name, value);
	}

	// Token: 0x060003AA RID: 938 RVA: 0x000119CE File Offset: 0x0000FBCE
	public bool isFullyRested()
	{
		return this.attributes.areRestableAttributesMax();
	}

	// Token: 0x060003AB RID: 939 RVA: 0x000119DB File Offset: 0x0000FBDB
	public void addToAttributeRank(AttributesControl.CoreAttributes name, int value)
	{
		this.attributes.addToAttributeRank(name, value);
	}

	// Token: 0x060003AC RID: 940 RVA: 0x000119EA File Offset: 0x0000FBEA
	public void addToAttributeRank(string name, int value)
	{
		this.attributes.addToAttributeRank(name, value);
	}

	// Token: 0x060003AD RID: 941 RVA: 0x000119FC File Offset: 0x0000FBFC
	public void resetMovementStat()
	{
		SKALDProjectData.CharacterContainers.Character character = this.getRawData();
		if (character == null)
		{
			return;
		}
		this.setAttributeRank(AttributesControl.CoreAttributes.ATT_Movement, character.combatMoves);
		this.setAttribute(AttributesControl.CoreAttributes.ATT_Movement, character.combatMoves);
	}

	// Token: 0x060003AE RID: 942 RVA: 0x00011A30 File Offset: 0x0000FC30
	public void setAttributeRank(AttributesControl.CoreAttributes name, int value)
	{
		this.attributes.setAttributeRank(name, value);
	}

	// Token: 0x060003AF RID: 943 RVA: 0x00011A40 File Offset: 0x0000FC40
	private void applyBleeding()
	{
		if (this.getConditionContainer().isBleeding())
		{
			int num = Mathf.CeilToInt((float)(this.getMaxVitality() / 5));
			if (num < 1)
			{
				num = 1;
			}
			Damage damageObject = new Damage(new DicePoolVariable("Bleeding", 1, num).getResult());
			this.addNegativeBark("Bleeding");
			this.takeDamage(damageObject, true);
		}
	}

	// Token: 0x060003B0 RID: 944 RVA: 0x00011A9C File Offset: 0x0000FC9C
	private void applyPoison()
	{
		if (this.getConditionContainer().isPoisoned())
		{
			int num = Mathf.CeilToInt((float)(this.getMaxVitality() / 5));
			if (num < 1)
			{
				num = 1;
			}
			Damage damageObject = new Damage(new DicePoolVariable("Poison", 1, num).getResult());
			this.addNegativeBark("Poison");
			this.takeDamage(damageObject, true);
		}
	}

	// Token: 0x060003B1 RID: 945 RVA: 0x00011AF8 File Offset: 0x0000FCF8
	private void applyRegeneration()
	{
		int currentAttributeValue = this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_Regeneration);
		if (currentAttributeValue <= 0)
		{
			return;
		}
		if (this.getCurrentVitalityDamage() == 0)
		{
			return;
		}
		this.addVitality(currentAttributeValue);
	}

	// Token: 0x060003B2 RID: 946 RVA: 0x00011B24 File Offset: 0x0000FD24
	public void startOfTurnUpkeep()
	{
		this.planning = true;
		if (this.holdingAction)
		{
			this.holdingAction = false;
			this.resetCombatMoves();
			return;
		}
		this.getAbilityTriggeredContainer().getTriggeredAbilityList().triggerStartOfTurn(this);
		this.getConditionContainer().clearStartOfTurnConditions();
		this.applyRegeneration();
		this.applyBleeding();
		this.applyPoison();
		this.decrementCooldown();
	}

	// Token: 0x060003B3 RID: 947 RVA: 0x00011B82 File Offset: 0x0000FD82
	public void endOfTurnUpkeep()
	{
		if (this.holdingAction)
		{
			return;
		}
		this.getConditionContainer().clearEndOfTurnConditions();
		this.clearChargeMoveCounter();
		this.clearAreaEffectSelection();
		this.clearCascadeCounter();
	}

	// Token: 0x060003B4 RID: 948 RVA: 0x00011BAC File Offset: 0x0000FDAC
	public void startOfRoundUpkeep()
	{
		this.hasActed = false;
		this.planning = false;
		this.passing = false;
		this.waitInCombat = false;
		this.holdingAction = false;
		this.combatOrders.clearOrders();
		this.isFalling = false;
		this.autoResolveRemainingTurn = false;
		this.moveAlongCombatPath = false;
		this.hasHeldActionThisTurn = false;
		this.clearNavigationCourse();
		this.resetCombatMoves();
	}

	// Token: 0x060003B5 RID: 949 RVA: 0x00011C10 File Offset: 0x0000FE10
	public void endOfCombatUpkeep()
	{
		this.inMelee = false;
		this.isInCombat = false;
		this.flanked = false;
		this.deathTriggerFired = false;
		this.getAbilityTriggeredContainer().getTriggeredAbilityList().triggerCombatEnd(this);
		this.endOfTurnUpkeep();
		this.startOfRoundUpkeep();
		this.clearCombatParties();
		this.clearCoolDown();
		this.clearTargetOpponent();
		this.clearNavigationCourse();
		this.clearUpcomingAttack();
		this.clearEffectsBarksAndAnimations();
		this.combatOrders = new Character.CombatOrders();
		base.clearPhysicsStates();
		this.getConditionContainer().clearEndOfCombatConditions();
	}

	// Token: 0x060003B6 RID: 950 RVA: 0x00011C97 File Offset: 0x0000FE97
	public void startOfCombatUpkeep()
	{
		this.clearEffectsBarksAndAnimations();
		this.isInCombat = true;
		base.resetPhysics();
		this.getAbilityTriggeredContainer().getTriggeredAbilityList().triggerCombatStart(this);
	}

	// Token: 0x060003B7 RID: 951 RVA: 0x00011CC0 File Offset: 0x0000FEC0
	public void setAsCurrentPC(Character oldCharacter)
	{
		if (oldCharacter != null)
		{
			if (!this.isInCombat && oldCharacter.isHidden())
			{
				oldCharacter.clearHidden();
				this.hideCharacter();
			}
			oldCharacter.clearEffectsBarksAndAnimations();
			oldCharacter.clearPhysicsStates();
			if (!this.isInCombat)
			{
				this.setHiddenDegree(oldCharacter.getHiddenDegree());
			}
		}
		this.clearEffectsBarksAndAnimations();
	}

	// Token: 0x060003B8 RID: 952 RVA: 0x00011D12 File Offset: 0x0000FF12
	public void clearEffectsBarksAndAnimations()
	{
		this.clearDynamicAnimation();
		base.getVisualEffects().resetEffects();
		this.barkControl = null;
		this.tacticalHoverTextBuffer = "";
	}

	// Token: 0x060003B9 RID: 953 RVA: 0x00011D38 File Offset: 0x0000FF38
	public string printAttunement()
	{
		return string.Concat(new string[]
		{
			C64Color.YELLOW_TAG,
			"Attunement:</color> ",
			this.getAttunement().ToString(),
			" / ",
			this.getMaxAttunement().ToString()
		});
	}

	// Token: 0x060003BA RID: 954 RVA: 0x00011D8A File Offset: 0x0000FF8A
	public int getAttunement()
	{
		return this.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_Attunement);
	}

	// Token: 0x060003BB RID: 955 RVA: 0x00011D94 File Offset: 0x0000FF94
	public string restoreAttunementFully()
	{
		this.attributes.resetAttributeToAbsoluteMax(AttributesControl.CoreAttributes.ATT_Attunement);
		return this.getMaxAttunement().ToString();
	}

	// Token: 0x060003BC RID: 956 RVA: 0x00011DBB File Offset: 0x0000FFBB
	public void makeBackstabber()
	{
		this.dynamicData.combatAbilityFlags.backStabber = true;
	}

	// Token: 0x060003BD RID: 957 RVA: 0x00011DCE File Offset: 0x0000FFCE
	public int getMaxAttunement()
	{
		return this.attributes.getMaxAttributeValue(AttributesControl.CoreAttributes.ATT_Attunement);
	}

	// Token: 0x060003BE RID: 958 RVA: 0x00011DDC File Offset: 0x0000FFDC
	public void setAbilityTarget(int x, int y)
	{
		this.getAbilityManueverContainer().setCombatTargetSelection(this, x, y);
	}

	// Token: 0x060003BF RID: 959 RVA: 0x00011DEC File Offset: 0x0000FFEC
	public void setCombatSpellTarget(int x, int y)
	{
		this.getSpellContainer().setCombatTargetSelection(this, x, y);
	}

	// Token: 0x060003C0 RID: 960 RVA: 0x00011DFC File Offset: 0x0000FFFC
	public void setOutOfCombatSpellTarget(List<Character> targets)
	{
		this.getSpellContainer().setOutOfCombatTargetSelection(targets);
	}

	// Token: 0x060003C1 RID: 961 RVA: 0x00011E0A File Offset: 0x0001000A
	public UIPartyEffectSelector getOutOfCombatSpellTargetSelector()
	{
		return this.getSpellContainer().getOutOfCombatSpellTargetSelector();
	}

	// Token: 0x060003C2 RID: 962 RVA: 0x00011E17 File Offset: 0x00010017
	public void clearAreaEffectSelection()
	{
		this.getAbilityManueverContainer().clearAreaEffectSelection();
		this.getSpellContainer().clearAreaEffectSelection();
	}

	// Token: 0x060003C3 RID: 963 RVA: 0x00011E2F File Offset: 0x0001002F
	public bool isSpellAreaEffectLegal()
	{
		return this.getSpellContainer().isAreaEffectSelectionLegal();
	}

	// Token: 0x060003C4 RID: 964 RVA: 0x00011E3C File Offset: 0x0001003C
	public bool isAbilityAreaEffectLegal()
	{
		return this.getAbilityManueverContainer().isAreaEffectSelectionLegal();
	}

	// Token: 0x060003C5 RID: 965 RVA: 0x00011E49 File Offset: 0x00010049
	public bool areaEffectSelectionContains(MapTile tile)
	{
		return this.getAbilityManueverContainer().areaEffectSelectionContains(tile) || this.getSpellContainer().areaEffectSelectionContains(tile);
	}

	// Token: 0x060003C6 RID: 966 RVA: 0x00011E6C File Offset: 0x0001006C
	public bool hasAreaEffectSelectionSet()
	{
		return this.getAbilityManueverContainer().hasAreaEffectSelectionSet() || this.getSpellContainer().hasAreaEffectSelectionSet();
	}

	// Token: 0x0400004B RID: 75
	private Character.CharacterSaveData dynamicData;

	// Token: 0x0400004C RID: 76
	private CharacterAttributes attributes;

	// Token: 0x0400004D RID: 77
	private AbilityContainerManeuver abilityContainerManuver;

	// Token: 0x0400004E RID: 78
	private AbilityContainerTriggered abilityContainerTriggered;

	// Token: 0x0400004F RID: 79
	private AbilityContainerPassive abilityContainerPassive;

	// Token: 0x04000050 RID: 80
	private SpellContainer spellContainer;

	// Token: 0x04000051 RID: 81
	private ConditionContainer temporaryConditionContainer;

	// Token: 0x04000052 RID: 82
	private FeatContainer featContainer = new FeatContainer();

	// Token: 0x04000053 RID: 83
	private Character.AttackResolution upcomingAttack;

	// Token: 0x04000054 RID: 84
	private Character.ChargeMoveCounter chargeMoveCounter;

	// Token: 0x04000055 RID: 85
	private Character.MissCounter missCounter;

	// Token: 0x04000056 RID: 86
	private Character.CombatOrders combatOrders = new Character.CombatOrders();

	// Token: 0x04000057 RID: 87
	private Character.ItemSlots itemSlots;

	// Token: 0x04000058 RID: 88
	protected Character targetOpponent;

	// Token: 0x04000059 RID: 89
	private Party tileParty;

	// Token: 0x0400005A RID: 90
	private Party opponentParty;

	// Token: 0x0400005B RID: 91
	private Party combatParty;

	// Token: 0x0400005C RID: 92
	private NavigationCourse course;

	// Token: 0x0400005D RID: 93
	private BarkControl barkControl;

	// Token: 0x0400005E RID: 94
	private SKALDProjectData.CharacterContainers.Character rawData;

	// Token: 0x0400005F RID: 95
	private int coolDown;

	// Token: 0x04000060 RID: 96
	private int currentInitiative;

	// Token: 0x04000061 RID: 97
	private int nearbyAllies;

	// Token: 0x04000062 RID: 98
	private int currentCascadeCount;

	// Token: 0x04000063 RID: 99
	private bool flanked;

	// Token: 0x04000064 RID: 100
	private const int MODEL_FRAME_SCALE = 3;

	// Token: 0x04000065 RID: 101
	public static int PADDING = 10;

	// Token: 0x04000066 RID: 102
	private int frameSize = 16 + Character.PADDING * 3;

	// Token: 0x04000067 RID: 103
	private bool wading;

	// Token: 0x04000068 RID: 104
	private bool passing;

	// Token: 0x04000069 RID: 105
	private bool autoResolveRemainingTurn;

	// Token: 0x0400006A RID: 106
	private bool waitInCombat;

	// Token: 0x0400006B RID: 107
	private bool holdingAction;

	// Token: 0x0400006C RID: 108
	private bool inMelee;

	// Token: 0x0400006D RID: 109
	private bool hasHeldActionThisTurn;

	// Token: 0x0400006E RID: 110
	private bool isInCombat;

	// Token: 0x0400006F RID: 111
	private bool deathTriggerFired;

	// Token: 0x04000070 RID: 112
	private bool hasActed;

	// Token: 0x04000071 RID: 113
	private bool summoned;

	// Token: 0x04000072 RID: 114
	private bool planning;

	// Token: 0x04000073 RID: 115
	public bool isFalling;

	// Token: 0x04000074 RID: 116
	public bool moveAlongCombatPath;

	// Token: 0x04000075 RID: 117
	private string tacticalHoverTextBuffer = "";

	// Token: 0x04000076 RID: 118
	private TextureTools.TextureData drawTexture;

	// Token: 0x04000077 RID: 119
	private TextureTools.TextureData portrait;

	// Token: 0x04000078 RID: 120
	private TextureTools.TextureData portraitCondition;

	// Token: 0x04000079 RID: 121
	private TextureTools.TextureData portraitBloodied;

	// Token: 0x0400007A RID: 122
	private TextureTools.TextureData portraitKO;

	// Token: 0x0400007B RID: 123
	private TextureTools.TextureData portraitKOBloody;

	// Token: 0x0400007C RID: 124
	private TextureTools.TextureData portraitWorkspace;

	// Token: 0x0400007D RID: 125
	private TextureTools.TextureData tacticalCharacterIndicator;

	// Token: 0x0400007E RID: 126
	private static TextureTools.TextureData levelUpIcon = null;

	// Token: 0x0400007F RID: 127
	private static TextureTools.TextureData levelUpIcon2 = null;

	// Token: 0x04000080 RID: 128
	private TextureTools.TextureData shadowImage;

	// Token: 0x04000081 RID: 129
	private ImageOutlineDrawer outlineDrawer;

	// Token: 0x04000082 RID: 130
	private StringBuilder bufferIdBuilder;

	// Token: 0x04000083 RID: 131
	private static List<string> defaultAttackSounds = new List<string>
	{
		"WeaponUnarmed1"
	};

	// Token: 0x04000084 RID: 132
	private static List<string> defaultLightHitSounds = new List<string>
	{
		"HitLight1",
		"HitLight2",
		"HitLight3"
	};

	// Token: 0x04000085 RID: 133
	private static List<string> defaultLightHitSoundsLightArmor = new List<string>
	{
		"HitArmorLight1",
		"HitArmorLight2"
	};

	// Token: 0x04000086 RID: 134
	private static List<string> defaultLightHitSoundsHeavyArmor = new List<string>
	{
		"HitArmorHeavy1",
		"HitArmorHeavy2"
	};

	// Token: 0x04000087 RID: 135
	private static List<string> defaultSevereSlashingHitSounds = new List<string>
	{
		"HitSevereSlashing1",
		"HitSevereSlashing2"
	};

	// Token: 0x04000088 RID: 136
	private static List<string> defaultSevereGenericHitSounds = new List<string>
	{
		"HitSevereGeneric1",
		"HitSevereBlunt1"
	};

	// Token: 0x04000089 RID: 137
	private static List<string> defaultDeathSounds = new List<string>
	{
		"DeathNormal"
	};

	// Token: 0x020001C2 RID: 450
	public enum MoveMode
	{
		// Token: 0x040006B4 RID: 1716
		None,
		// Token: 0x040006B5 RID: 1717
		Roam,
		// Token: 0x040006B6 RID: 1718
		Home,
		// Token: 0x040006B7 RID: 1719
		Flee,
		// Token: 0x040006B8 RID: 1720
		RoamIfAlert,
		// Token: 0x040006B9 RID: 1721
		Graze,
		// Token: 0x040006BA RID: 1722
		PatrolNS,
		// Token: 0x040006BB RID: 1723
		PatrolEW,
		// Token: 0x040006BC RID: 1724
		PatrolCW,
		// Token: 0x040006BD RID: 1725
		PatrolCCW
	}

	// Token: 0x020001C3 RID: 451
	[Serializable]
	protected class CharacterSaveData : BaseSaveData
	{
		// Token: 0x0600165D RID: 5725 RVA: 0x000642B8 File Offset: 0x000624B8
		public CharacterSaveData(SkaldWorldObject.WorldPosition position, SkaldBaseObject.CoreData coreData, SkaldInstanceObject.InstanceData instanceData) : base(position, coreData, instanceData)
		{
		}

		// Token: 0x040006BE RID: 1726
		public bool spriteFacesRight = true;

		// Token: 0x040006BF RID: 1727
		public bool hasTalkedToPlayer;

		// Token: 0x040006C0 RID: 1728
		public bool hasTouchedPlayer;

		// Token: 0x040006C1 RID: 1729
		public bool hostile = true;

		// Token: 0x040006C2 RID: 1730
		public bool isMale = true;

		// Token: 0x040006C3 RID: 1731
		public bool alert;

		// Token: 0x040006C4 RID: 1732
		public bool stealLocked;

		// Token: 0x040006C5 RID: 1733
		public bool PC;

		// Token: 0x040006C6 RID: 1734
		public bool afraid;

		// Token: 0x040006C7 RID: 1735
		public bool isSpotted;

		// Token: 0x040006C8 RID: 1736
		public bool isMainCharacter;

		// Token: 0x040006C9 RID: 1737
		public bool hidden;

		// Token: 0x040006CA RID: 1738
		public bool isBeingObserved;

		// Token: 0x040006CB RID: 1739
		public bool approached;

		// Token: 0x040006CC RID: 1740
		public bool preferMeleeWeapon = true;

		// Token: 0x040006CD RID: 1741
		public Character.MoveMode moveMode;

		// Token: 0x040006CE RID: 1742
		public CampActivityContainer.CampActivities preferredCampActivity;

		// Token: 0x040006CF RID: 1743
		public int developmentPoints = GameData.getStartingDP();

		// Token: 0x040006D0 RID: 1744
		public int baseReaction = 50;

		// Token: 0x040006D1 RID: 1745
		public int relationshipRank;

		// Token: 0x040006D2 RID: 1746
		public int facing;

		// Token: 0x040006D3 RID: 1747
		public int hiddenDegree = 100;

		// Token: 0x040006D4 RID: 1748
		public int thieverySuspicion;

		// Token: 0x040006D5 RID: 1749
		public short alertGrace = 2;

		// Token: 0x040006D6 RID: 1750
		public Party mainParty;

		// Token: 0x040006D7 RID: 1751
		public FactionControl.FactionRelationships factionRelationships;

		// Token: 0x040006D8 RID: 1752
		public Inventory inventory = new Inventory();

		// Token: 0x040006D9 RID: 1753
		public string characterClass = "";

		// Token: 0x040006DA RID: 1754
		public string race = "";

		// Token: 0x040006DB RID: 1755
		public string background = "";

		// Token: 0x040006DC RID: 1756
		public string preferredAmmo = "";

		// Token: 0x040006DD RID: 1757
		public Store store;

		// Token: 0x040006DE RID: 1758
		public Character.CombatAbilityFlags combatAbilityFlags = new Character.CombatAbilityFlags();

		// Token: 0x040006DF RID: 1759
		public CharacterLooksControl characterLooksControl = new CharacterLooksControl();

		// Token: 0x040006E0 RID: 1760
		public CharacterAnimationContainer characterAnimationControl = new CharacterAnimationContainer();

		// Token: 0x040006E1 RID: 1761
		public XpContainer xpContainer;
	}

	// Token: 0x020001C4 RID: 452
	[Serializable]
	public class CombatAbilityFlags
	{
		// Token: 0x040006E2 RID: 1762
		public bool freeSwap;

		// Token: 0x040006E3 RID: 1763
		public bool backStabber;

		// Token: 0x040006E4 RID: 1764
		public bool evasion;

		// Token: 0x040006E5 RID: 1765
		public bool pointBlankShot;

		// Token: 0x040006E6 RID: 1766
		public bool arcaneSpellcaster;

		// Token: 0x040006E7 RID: 1767
		public bool divineSpellcaster;
	}

	// Token: 0x020001C5 RID: 453
	[Serializable]
	private class CombatOrders
	{
		// Token: 0x0600165F RID: 5727 RVA: 0x0006436C File Offset: 0x0006256C
		public Character.CombatOrders.Orders getCurrentOrder()
		{
			return this.currentOrder;
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x00064374 File Offset: 0x00062574
		public Character.CombatOrders.Orders getLastAction()
		{
			return this.lastAction;
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x0006437C File Offset: 0x0006257C
		public bool hasRepeatableAction()
		{
			return this.lastAction != Character.CombatOrders.Orders.NoOrder;
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x00064389 File Offset: 0x00062589
		public void clearOrders()
		{
			this.currentOrder = Character.CombatOrders.Orders.NoOrder;
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x00064392 File Offset: 0x00062592
		public void repeat()
		{
			this.currentOrder = Character.CombatOrders.Orders.Repeating;
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x0006439B File Offset: 0x0006259B
		public void useAbility()
		{
			this.setAction(Character.CombatOrders.Orders.UsingAbility);
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x000643A4 File Offset: 0x000625A4
		public void castSpell()
		{
			this.setAction(Character.CombatOrders.Orders.CastingSpell);
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x000643AD File Offset: 0x000625AD
		public void defend()
		{
			this.setAction(Character.CombatOrders.Orders.Defending);
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x000643B6 File Offset: 0x000625B6
		public void attack()
		{
			this.setAction(Character.CombatOrders.Orders.Attacking);
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x000643BF File Offset: 0x000625BF
		public void combatMove()
		{
			this.currentOrder = Character.CombatOrders.Orders.Moving;
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x000643C8 File Offset: 0x000625C8
		public void combatSwap()
		{
			this.currentOrder = Character.CombatOrders.Orders.Swap;
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x000643D1 File Offset: 0x000625D1
		private void setAction(Character.CombatOrders.Orders order)
		{
			this.currentOrder = order;
			this.lastAction = order;
		}

		// Token: 0x040006E8 RID: 1768
		private Character.CombatOrders.Orders currentOrder;

		// Token: 0x040006E9 RID: 1769
		private Character.CombatOrders.Orders lastAction;

		// Token: 0x020002F8 RID: 760
		public enum Orders
		{
			// Token: 0x04000A8B RID: 2699
			NoOrder,
			// Token: 0x04000A8C RID: 2700
			Moving,
			// Token: 0x04000A8D RID: 2701
			CastingSpell,
			// Token: 0x04000A8E RID: 2702
			UsingAbility,
			// Token: 0x04000A8F RID: 2703
			Defending,
			// Token: 0x04000A90 RID: 2704
			Attacking,
			// Token: 0x04000A91 RID: 2705
			Repeating,
			// Token: 0x04000A92 RID: 2706
			Swap
		}
	}

	// Token: 0x020001C6 RID: 454
	private class ItemSlots
	{
		// Token: 0x0600166C RID: 5740 RVA: 0x000643E9 File Offset: 0x000625E9
		public ItemSlots(Inventory inventory, Character character)
		{
			this.character = character;
			this.validateInventory(inventory);
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x00064418 File Offset: 0x00062618
		private void populateFromInventory()
		{
			this.meleeWeapon = new Character.ItemSlots.Slot(Item.ItemTypes.MeleeWeapon);
			this.rangedWeapon = new Character.ItemSlots.Slot(Item.ItemTypes.RangedWeapon);
			this.armor = new Character.ItemSlots.Slot(Item.ItemTypes.Armor);
			this.shield = new Character.ItemSlots.Slot(Item.ItemTypes.Shield);
			this.headWear = new Character.ItemSlots.Slot(Item.ItemTypes.Headwear);
			this.light = new Character.ItemSlots.Slot(Item.ItemTypes.Light);
			this.clothing = new Character.ItemSlots.Slot(Item.ItemTypes.Clothing);
			this.boots = new Character.ItemSlots.Slot(Item.ItemTypes.Footwear);
			this.gloves = new Character.ItemSlots.Slot(Item.ItemTypes.Glove);
			this.ammo = new Character.ItemSlots.SlotAmmo(Item.ItemTypes.Ammo);
			this.ring = new Character.ItemSlots.Slot(Item.ItemTypes.Ring);
			this.idleItem = new Character.ItemSlots.Slot(Item.ItemTypes.Idle);
			this.necklace = new Character.ItemSlots.Slot(Item.ItemTypes.Necklace);
			this.slots = new List<Character.ItemSlots.Slot>
			{
				this.meleeWeapon,
				this.rangedWeapon,
				this.armor,
				this.shield,
				this.headWear,
				this.light,
				this.clothing,
				this.ammo,
				this.boots,
				this.gloves,
				this.ring,
				this.necklace,
				this.idleItem
			};
			if (this.character == null || this.inventory == null)
			{
				return;
			}
			foreach (Item item in this.inventory.getListOfOwnedItems(this.character))
			{
				this.addItem(item);
			}
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x000645D0 File Offset: 0x000627D0
		private void validateInventory(Inventory newInventory)
		{
			if (this.inventory == null || this.inventory != newInventory)
			{
				this.inventory = newInventory;
				this.populateFromInventory();
			}
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x000645F0 File Offset: 0x000627F0
		public ItemWeapon getMeleeWeapon(Inventory newInventory)
		{
			this.validateInventory(newInventory);
			return this.meleeWeapon.getItem(this.character) as ItemMeleeWeapon;
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x0006460F File Offset: 0x0006280F
		public ItemWeapon getRangedWeapon(Inventory newInventory)
		{
			this.validateInventory(newInventory);
			return this.rangedWeapon.getItem(this.character) as ItemRangedWeapon;
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x0006462E File Offset: 0x0006282E
		public ItemArmor getArmor(Inventory newInventory)
		{
			this.validateInventory(newInventory);
			return this.armor.getItem(this.character) as ItemArmor;
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x0006464D File Offset: 0x0006284D
		public ItemAmmo getAmmo(Inventory newInventory)
		{
			this.validateInventory(newInventory);
			return this.ammo.getItem(this.character) as ItemAmmo;
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x0006466C File Offset: 0x0006286C
		public ItemRing getRing(Inventory newInventory)
		{
			this.validateInventory(newInventory);
			return this.ring.getItem(this.character) as ItemRing;
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x0006468B File Offset: 0x0006288B
		public ItemNecklace getNecklace(Inventory newInventory)
		{
			this.validateInventory(newInventory);
			return this.necklace.getItem(this.character) as ItemNecklace;
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x000646AA File Offset: 0x000628AA
		public ItemGlove getGlove(Inventory newInventory)
		{
			this.validateInventory(newInventory);
			return this.gloves.getItem(this.character) as ItemGlove;
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x000646C9 File Offset: 0x000628C9
		public ItemFootwear getFootwear(Inventory newInventory)
		{
			this.validateInventory(newInventory);
			return this.boots.getItem(this.character) as ItemFootwear;
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x000646E8 File Offset: 0x000628E8
		public ItemShield getShield(Inventory newInventory)
		{
			this.validateInventory(newInventory);
			return this.shield.getItem(this.character) as ItemShield;
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x00064707 File Offset: 0x00062907
		public ItemIdle getIdleItem(Inventory newInventory)
		{
			this.validateInventory(newInventory);
			return this.idleItem.getItem(this.character) as ItemIdle;
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x00064726 File Offset: 0x00062926
		public ItemHeadWear getHeadWear(Inventory newInventory)
		{
			this.validateInventory(newInventory);
			return this.headWear.getItem(this.character) as ItemHeadWear;
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x00064745 File Offset: 0x00062945
		public ItemLight getLight(Inventory newInventory)
		{
			this.validateInventory(newInventory);
			return this.light.getItem(this.character) as ItemLight;
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x00064764 File Offset: 0x00062964
		public ItemClothing getClothing(Inventory newInventory)
		{
			this.validateInventory(newInventory);
			return this.clothing.getItem(this.character) as ItemClothing;
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x00064784 File Offset: 0x00062984
		public List<string> getConferredAbilities(Character character)
		{
			this.validateInventory(character.getInventory());
			this.conferredIDs.Clear();
			foreach (Character.ItemSlots.Slot slot in this.slots)
			{
				Item item = slot.getItem(character);
				if (item != null)
				{
					foreach (string item2 in item.getConferredAbilities())
					{
						this.conferredIDs.Add(item2);
					}
				}
			}
			return this.conferredIDs;
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x00064840 File Offset: 0x00062A40
		public List<string> getConferredConditions(Character character)
		{
			this.validateInventory(character.getInventory());
			this.conferredIDs.Clear();
			foreach (Character.ItemSlots.Slot slot in this.slots)
			{
				Item item = slot.getItem(character);
				if (item != null)
				{
					foreach (string item2 in item.getConferredConditions())
					{
						this.conferredIDs.Add(item2);
					}
				}
			}
			return this.conferredIDs;
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x000648FC File Offset: 0x00062AFC
		public List<string> getConferredSpells(Character character)
		{
			this.validateInventory(character.getInventory());
			this.conferredIDs.Clear();
			foreach (Character.ItemSlots.Slot slot in this.slots)
			{
				Item item = slot.getItem(character);
				if (item != null)
				{
					foreach (string item2 in item.getConferredSpells())
					{
						this.conferredIDs.Add(item2);
					}
				}
			}
			return this.conferredIDs;
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x000649B8 File Offset: 0x00062BB8
		private void addItem(Item item)
		{
			using (List<Character.ItemSlots.Slot>.Enumerator enumerator = this.slots.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.addIfItemFits(item))
					{
						this.buildModelPathString();
						break;
					}
				}
			}
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x00064A14 File Offset: 0x00062C14
		public string getModelPathString()
		{
			return this.modelPath;
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x00064A1C File Offset: 0x00062C1C
		private void buildModelPathString()
		{
			this.modelPath = "";
			foreach (Character.ItemSlots.Slot slot in this.slots)
			{
				this.modelPath += slot.print();
			}
		}

		// Token: 0x040006EA RID: 1770
		private Character.ItemSlots.Slot meleeWeapon;

		// Token: 0x040006EB RID: 1771
		private Character.ItemSlots.Slot rangedWeapon;

		// Token: 0x040006EC RID: 1772
		private Character.ItemSlots.Slot armor;

		// Token: 0x040006ED RID: 1773
		private Character.ItemSlots.Slot shield;

		// Token: 0x040006EE RID: 1774
		private Character.ItemSlots.Slot headWear;

		// Token: 0x040006EF RID: 1775
		private Character.ItemSlots.Slot light;

		// Token: 0x040006F0 RID: 1776
		private Character.ItemSlots.Slot clothing;

		// Token: 0x040006F1 RID: 1777
		private Character.ItemSlots.Slot gloves;

		// Token: 0x040006F2 RID: 1778
		private Character.ItemSlots.Slot boots;

		// Token: 0x040006F3 RID: 1779
		private Character.ItemSlots.Slot ring;

		// Token: 0x040006F4 RID: 1780
		private Character.ItemSlots.Slot necklace;

		// Token: 0x040006F5 RID: 1781
		private Character.ItemSlots.Slot idleItem;

		// Token: 0x040006F6 RID: 1782
		private Character.ItemSlots.SlotAmmo ammo;

		// Token: 0x040006F7 RID: 1783
		private List<Character.ItemSlots.Slot> slots;

		// Token: 0x040006F8 RID: 1784
		private Inventory inventory;

		// Token: 0x040006F9 RID: 1785
		private Character character;

		// Token: 0x040006FA RID: 1786
		private List<string> conferredIDs = new List<string>();

		// Token: 0x040006FB RID: 1787
		private string modelPath = "";

		// Token: 0x020002F9 RID: 761
		private class Slot
		{
			// Token: 0x06001C14 RID: 7188 RVA: 0x000799F4 File Offset: 0x00077BF4
			public Slot(Item.ItemTypes type)
			{
				this.type = type;
			}

			// Token: 0x06001C15 RID: 7189 RVA: 0x00079A03 File Offset: 0x00077C03
			public Item getItem(Character character)
			{
				if (this.item != null && !this.isUserValid(character))
				{
					this.item = null;
				}
				return this.item;
			}

			// Token: 0x06001C16 RID: 7190 RVA: 0x00079A23 File Offset: 0x00077C23
			protected virtual bool isUserValid(Character character)
			{
				return this.item.testUser(character);
			}

			// Token: 0x06001C17 RID: 7191 RVA: 0x00079A31 File Offset: 0x00077C31
			public bool addIfItemFits(Item i)
			{
				if (i.isType(this.type))
				{
					this.item = i;
					return true;
				}
				return false;
			}

			// Token: 0x06001C18 RID: 7192 RVA: 0x00079A4B File Offset: 0x00077C4B
			public string print()
			{
				if (this.item != null)
				{
					return this.item.getId();
				}
				return "EMPTY";
			}

			// Token: 0x04000A93 RID: 2707
			private Item item;

			// Token: 0x04000A94 RID: 2708
			private Item.ItemTypes type;
		}

		// Token: 0x020002FA RID: 762
		private class SlotAmmo : Character.ItemSlots.Slot
		{
			// Token: 0x06001C19 RID: 7193 RVA: 0x00079A66 File Offset: 0x00077C66
			public SlotAmmo(Item.ItemTypes type) : base(type)
			{
			}

			// Token: 0x06001C1A RID: 7194 RVA: 0x00079A6F File Offset: 0x00077C6F
			protected override bool isUserValid(Character character)
			{
				return true;
			}
		}
	}

	// Token: 0x020001C7 RID: 455
	private class AttackResolution
	{
		// Token: 0x06001682 RID: 5762 RVA: 0x00064A8C File Offset: 0x00062C8C
		public AttackResolution(Character attacker, CharacterComponentContainer combatManeuverContainer)
		{
			this.combatManeuverContainer = combatManeuverContainer;
			if (this.combatManeuverContainer != null && combatManeuverContainer.getCurrentComponent() != null && combatManeuverContainer.getCurrentComponent() is AbilityCombatManeuver)
			{
				this.maneuver = (combatManeuverContainer.getCurrentComponent() as AbilityCombatManeuver);
			}
			this.attacker = attacker;
			if (attacker != null)
			{
				this.target = attacker.getTargetOpponent();
			}
			this.expendedMissile = attacker.payAmmoForAttack();
			this.baseDamage = attacker.rollCombatDamage(this.maneuver);
			this.combatSkill = attacker.getAttackSkillAndModifiers(this.maneuver);
			this.targetSoak = this.target.rollSoak();
			this.targetDodge = this.target.getDodgeSkill();
			this.attackerTriggerContainer = attacker.getAbilityTriggeredContainer().getTriggeredAbilityList();
			this.defenderTriggerContainer = this.target.getAbilityTriggeredContainer().getTriggeredAbilityList();
			this.charge = attacker.charging();
			if (!attacker.isWeaponRanged())
			{
				attacker.setPixelTarget(this.target, 6f);
			}
			if (attacker.isWeaponRanged())
			{
				attacker.getVisualEffects().setArrowEffect(this.target);
			}
			attacker.clearCombatMovesButNotAttacks();
			attacker.decrementAttacks();
			attacker.playAttackSound();
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x00064BBC File Offset: 0x00062DBC
		public SkaldActionResult resolveAttack()
		{
			if (!this.attacker.hasValidTarget())
			{
				return new SkaldActionResult(false, false, this.attacker.getName() + " does not have a valid target.", "Attack failed: " + this.attacker.getName() + " does not have a valid target.", true);
			}
			SkaldTestBase skaldTestBase = this.resolveToHit();
			string text = string.Concat(new string[]
			{
				"\nATTACK ROLL MODIFIERS: \n",
				this.combatSkill.printEntries(),
				"\n\nDEFENCE ROLL MODIFIERS: \n",
				this.targetDodge.printEntries(),
				"\n\n---\n\n",
				skaldTestBase.getReturnString()
			});
			this.attacker.setWeaponAttackFinishAnimation();
			SkaldActionResult result;
			if (skaldTestBase.wasSuccess())
			{
				this.attacker.resetMissCounter();
				this.fireHitTriggers();
				if (this.target.getRetribution() > 0)
				{
					this.attacker.takeDamage(this.target.getRetribution(), new List<string>
					{
						"Piercing"
					});
				}
				string text2 = "";
				SkaldActionResult skaldActionResult = this.resolveCritical();
				if (skaldActionResult.wasSuccess())
				{
					text2 += skaldActionResult.getResultString();
				}
				SkaldActionResult skaldActionResult2 = this.resolveBackstab();
				if (skaldActionResult2.wasSuccess() && text2 == "")
				{
					text2 = skaldActionResult2.getResultString();
				}
				if (this.attacker.isPC())
				{
					if (skaldActionResult.wasSuccess() && skaldActionResult2.wasSuccess())
					{
						this.attacker.setTacticalHoverText("Critical Backstab!!!");
					}
					else if (skaldActionResult.wasSuccess())
					{
						this.attacker.setTacticalHoverText("Critical Hit!");
					}
					else if (skaldActionResult2.wasSuccess())
					{
						this.attacker.setTacticalHoverText("Backstab!");
					}
					else if (this.charge)
					{
						this.attacker.setTacticalHoverText("Charge +" + this.attacker.getChargeBonus().ToString());
					}
				}
				result = new SkaldActionResult(true, skaldTestBase.wasSuccess(), skaldTestBase.header, text + "\n\n" + text2, true);
				CombatLog.addEntry(this.attacker.getName(), result);
				if (this.hasManeuver())
				{
					this.combatManeuverContainer.fireAbility(this.attacker);
				}
				this.resolveDamage();
				if (this.target.isDead())
				{
					this.attackerTriggerContainer.triggerKilledTarget(this.attacker);
					if (this.target.isMarked())
					{
						this.attackerTriggerContainer.triggerKilledMarkedTarget(this.attacker);
					}
					if (!this.attacker.isWeaponRanged())
					{
						this.attacker.applyImpulsePush(this.target);
						MainControl.getDataControl().overrunOpponent();
					}
				}
			}
			else
			{
				this.attacker.incrementMissCounter();
				this.fireMissTriggers();
				result = new SkaldActionResult(true, skaldTestBase.wasSuccess(), skaldTestBase.header, text, true);
				CombatLog.addEntry(this.attacker.getName(), result);
				if (this.attacker.isPC())
				{
					this.attacker.addNegativeBark("Miss", this.barkDelay);
				}
			}
			this.attacker.clearHidden();
			return result;
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x00064EC0 File Offset: 0x000630C0
		private bool hasManeuver()
		{
			return this.maneuver != null;
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x00064ECB File Offset: 0x000630CB
		private bool targetEligibleForBackstab()
		{
			return (this.target.isVulnerable() || this.target.isFlanked()) && this.attacker.isBackstabber() && !this.hasManeuver();
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x00064F00 File Offset: 0x00063100
		private void fireHitTriggers()
		{
			if (this.attacker.getCurrentWeapon() != null)
			{
				this.attacker.getCurrentWeapon().fireHitTriggers(this.attacker, this.target);
			}
			if (this.expendedMissile != null)
			{
				this.expendedMissile.fireHitTriggers(this.attacker, this.target);
			}
			if (this.attacker.isWeaponRanged())
			{
				this.attackerTriggerContainer.triggerRangedHit(this.attacker);
			}
			else if (this.attacker.getCurrentMeleeWeapon() != null)
			{
				this.attackerTriggerContainer.triggerArmedMeleeHit(this.attacker);
			}
			else if (this.attacker.getCurrentMeleeWeapon() == null)
			{
				this.attackerTriggerContainer.triggerUnarmedHit(this.attacker);
			}
			if (this.charge)
			{
				this.attackerTriggerContainer.triggerChargeHit(this.attacker);
			}
			if (this.hasManeuver())
			{
				this.attackerTriggerContainer.triggerOnManueverHit(this.attacker);
			}
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x00064FE8 File Offset: 0x000631E8
		private void fireMissTriggers()
		{
			this.defenderTriggerContainer.triggerDodge(this.target);
			if (this.attacker.getRangeToTargetOpponent() <= 1f)
			{
				this.defenderTriggerContainer.triggerDodgeMelee(this.target);
			}
			this.attackerTriggerContainer.triggerMiss(this.attacker);
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x0006503C File Offset: 0x0006323C
		public void resolveDamage()
		{
			if (this.maneuver == null || this.maneuver.dealsCombatDamageOnHit())
			{
				if (this.maneuver == null || this.maneuver.applyArmorSoak())
				{
					if (MainControl.debugFunctions)
					{
						string white_TAG = C64Color.WHITE_TAG;
						string id = this.attacker.getId();
						string str = "</color> rolls damage: ";
						Damage damage = this.baseDamage;
						MainControl.log(white_TAG + id + str + ((damage != null) ? damage.ToString() : null));
						MainControl.log(C64Color.WHITE_TAG + this.target.getId() + "</color> rolls soak: " + this.targetSoak.ToString());
					}
					this.baseDamage.applySoak(this.targetSoak);
				}
				this.target.takeDamage(this.baseDamage, true);
			}
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x000650FC File Offset: 0x000632FC
		private SkaldTestBase resolveToHit()
		{
			if (this.target.isVulnerable())
			{
				return new SkaldTestAutoSucceed(this.attacker.getFullNameUpper() + " automatically hit " + this.target.getFullNameUpper());
			}
			SkaldTestRandomVsRandom skaldTestRandomVsRandom = new SkaldTestRandomVsRandom(this.combatSkill.getTotalValue(), "Attack Roll", this.targetDodge.getTotalValue(), "Defence Roll", this.attacker.getNumberOfToHitRerolls());
			if (skaldTestRandomVsRandom.wasSuccess())
			{
				skaldTestRandomVsRandom.header = this.attacker.getFullNameUpper() + " hits " + this.target.getFullNameUpper();
			}
			else
			{
				skaldTestRandomVsRandom.header = this.attacker.getFullNameUpper() + " misses " + this.target.getFullNameUpper();
			}
			return skaldTestRandomVsRandom;
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x000651C4 File Offset: 0x000633C4
		private SkaldActionResult resolveCritical()
		{
			bool flag = this.attacker.testCritical();
			if (this.hasManeuver() && this.maneuver.autoCritOnHit())
			{
				flag = true;
			}
			if (flag && (!this.hasManeuver() || this.maneuver.allowsCriticalHits()))
			{
				if (this.target.getCriticalHitsResistance() == 100)
				{
					this.target.addNegativeBark("Immune: Criticals");
				}
				else
				{
					if (!this.target.testCriticalHitsResistance())
					{
						this.attackerTriggerContainer.triggerCriticalHit(this.attacker);
						int amount = this.baseDamage.getAmount();
						this.baseDamage.multiply(this.attacker.getCritMultiplier(), "Critical hit multiplies damage by ");
						string text = C64Color.RED_LIGHT_TAG + "CRITICAL HIT: </color>Target took extra damamge\n\n";
						if (MainControl.debugFunctions)
						{
							MainControl.log(string.Concat(new string[]
							{
								C64Color.WHITE_TAG,
								this.attacker.getId(),
								"</color>: ",
								text,
								" - ",
								(this.baseDamage.getAmount() - amount).ToString()
							}));
						}
						this.target.getVisualEffects().setShaken();
						return new SkaldActionResult(true, true, text, true);
					}
					this.target.addNegativeBark("Resisted Critical");
				}
			}
			return new SkaldActionResult(true, false, "No critical", true);
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x00065328 File Offset: 0x00063528
		private SkaldActionResult resolveBackstab()
		{
			if (!this.attacker.isBackstabber())
			{
				return new SkaldActionResult(true, false, "No backstab", true);
			}
			if ((this.targetEligibleForBackstab() || this.attacker.isHidden()) && (!this.hasManeuver() || this.maneuver.allowsBackstab()))
			{
				if (this.target.getCriticalHitsResistance() == 100)
				{
					this.target.addNegativeBark("Immune: Backstab");
				}
				else
				{
					if (!this.target.testCriticalHitsResistance())
					{
						this.attackerTriggerContainer.triggerBackstabHit(this.attacker);
						int currentAttributeValue = this.attacker.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_Backstab);
						int amount = Mathf.RoundToInt((float)currentAttributeValue * this.attacker.getCritMultiplier() - (float)currentAttributeValue);
						this.baseDamage.add(currentAttributeValue, "Base Backstab Bonus: ");
						this.baseDamage.add(amount, "Weapon Backstab Mod: ");
						string text = C64Color.RED_LIGHT_TAG + "BACKSTABBED: </color>Target took extra damamge\n\n";
						if (MainControl.debugFunctions)
						{
							MainControl.log(string.Concat(new string[]
							{
								C64Color.WHITE_TAG,
								this.attacker.getId(),
								"</color>: ",
								text,
								" - ",
								currentAttributeValue.ToString()
							}));
						}
						this.target.getVisualEffects().setShaken();
						return new SkaldActionResult(true, true, text, true);
					}
					this.target.addNegativeBark("Resisted Backstab");
				}
			}
			return new SkaldActionResult(true, false, "No backstab", true);
		}

		// Token: 0x040006FC RID: 1788
		private Character attacker;

		// Token: 0x040006FD RID: 1789
		private Character target;

		// Token: 0x040006FE RID: 1790
		private CharacterComponentContainer combatManeuverContainer;

		// Token: 0x040006FF RID: 1791
		private AbilityCombatManeuver maneuver;

		// Token: 0x04000700 RID: 1792
		private Damage baseDamage;

		// Token: 0x04000701 RID: 1793
		private ItemAmmo expendedMissile;

		// Token: 0x04000702 RID: 1794
		private SkaldNumericContainer combatSkill;

		// Token: 0x04000703 RID: 1795
		private SkaldNumericContainer targetDodge;

		// Token: 0x04000704 RID: 1796
		private int targetSoak;

		// Token: 0x04000705 RID: 1797
		private int barkDelay = 15;

		// Token: 0x04000706 RID: 1798
		private bool charge;

		// Token: 0x04000707 RID: 1799
		private AbilityContainerTriggered.TriggeredAbilityContainer attackerTriggerContainer;

		// Token: 0x04000708 RID: 1800
		private AbilityContainerTriggered.TriggeredAbilityContainer defenderTriggerContainer;
	}

	// Token: 0x020001C8 RID: 456
	private class ChargeMoveCounter
	{
		// Token: 0x0600168C RID: 5772 RVA: 0x000654A6 File Offset: 0x000636A6
		public void clearCounter()
		{
			this.direction = -1;
			this.count = 0;
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x000654B6 File Offset: 0x000636B6
		public void updateCounter(int currentDirection)
		{
			if (this.direction == currentDirection && this.count < this.maxCount)
			{
				this.count++;
				return;
			}
			this.direction = currentDirection;
			this.count = 0;
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x000654EC File Offset: 0x000636EC
		public int getCount()
		{
			return this.count;
		}

		// Token: 0x04000709 RID: 1801
		private int direction = -1;

		// Token: 0x0400070A RID: 1802
		private int count;

		// Token: 0x0400070B RID: 1803
		private int maxCount = 3;
	}

	// Token: 0x020001C9 RID: 457
	private class MissCounter
	{
		// Token: 0x06001690 RID: 5776 RVA: 0x0006550A File Offset: 0x0006370A
		public void reset()
		{
			this.misses = 0;
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x00065513 File Offset: 0x00063713
		public void addMiss()
		{
			this.misses++;
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x00065524 File Offset: 0x00063724
		public int getBonusRerolls()
		{
			int num = 0;
			if (this.misses > this.interventionCutoff)
			{
				num = this.misses;
			}
			if (num > GlobalSettings.getDifficultySettings().getMissSmoothening())
			{
				num = GlobalSettings.getDifficultySettings().getMissSmoothening();
			}
			return num;
		}

		// Token: 0x0400070C RID: 1804
		private int misses;

		// Token: 0x0400070D RID: 1805
		private int interventionCutoff = 1;
	}
}
