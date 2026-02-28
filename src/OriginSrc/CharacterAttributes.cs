using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

// Token: 0x02000020 RID: 32
[Serializable]
public class CharacterAttributes : AttributesControl, ISerializable
{
	// Token: 0x060003C8 RID: 968 RVA: 0x00011F7D File Offset: 0x0001017D
	public CharacterAttributes(Character character)
	{
		this.initialize();
		this.setExternalAttributeData(character);
	}

	// Token: 0x060003C9 RID: 969 RVA: 0x00011F94 File Offset: 0x00010194
	public void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		foreach (AttributesControl.AttributeList attributeList in this.metaList)
		{
			info.AddValue(attributeList.getName(), attributeList.getSaveDataList(), typeof(List<AttributesControl.Attribute.AttributeSaveData>));
		}
	}

	// Token: 0x060003CA RID: 970 RVA: 0x00011FFC File Offset: 0x000101FC
	public CharacterAttributes(SerializationInfo info, StreamingContext context)
	{
		this.initialize();
		foreach (AttributesControl.AttributeList attributeList in this.metaList)
		{
			List<AttributesControl.Attribute.AttributeSaveData> saveData = (List<AttributesControl.Attribute.AttributeSaveData>)info.GetValue(attributeList.getName(), typeof(List<AttributesControl.Attribute.AttributeSaveData>));
			attributeList.setSaveData(saveData);
		}
	}

	// Token: 0x060003CB RID: 971 RVA: 0x00012078 File Offset: 0x00010278
	private void initialize()
	{
		this.restAttributes = new AttributesControl.AttributesListRest();
		this.skillsList = new AttributesControl.AttributesListSkills();
		this.defencesList = new AttributesControl.AttributesListDefences();
		this.damageResistanceList = new AttributesControl.AttributesListDamageResistance();
		this.damageBonusList = new AttributesControl.AttributesListDamageBonus();
		this.hitBonusList = new AttributesControl.AttributesListHitBonus();
		this.armorBonusList = new AttributesControl.AttributesListArmorBonus();
		this.conditionResistanceList = new AttributesControl.AttributesListConditionResistance();
		this.primaryAttributes = new AttributesControl.AttributesListPrimaryStats();
		this.secondaryAttributes = new AttributesControl.AttributesListSecondaryAttributes();
		this.combatAttributes = new AttributesControl.AttributesListCombatAttributes();
		this.secondaryCombatAttributes = new AttributesControl.AttributesListSecondaryCombatAttributes();
		this.spellTiers = new AttributesControl.AttributesListMagicSchools();
		this.magicalAttributes = new AttributesControl.AttributesListMagicalAttributes();
		this.metaList = new List<AttributesControl.AttributeList>
		{
			this.primaryAttributes,
			this.secondaryAttributes,
			this.skillsList,
			this.combatAttributes,
			this.defencesList,
			this.damageBonusList,
			this.armorBonusList,
			this.hitBonusList,
			this.damageResistanceList,
			this.conditionResistanceList,
			this.secondaryCombatAttributes,
			this.spellTiers,
			this.magicalAttributes
		};
		this.addPrimaryAttribute(AttributesControl.CoreAttributes.ATT_Strength);
		this.addPrimaryAttribute(AttributesControl.CoreAttributes.ATT_Agility);
		this.addPrimaryAttribute(AttributesControl.CoreAttributes.ATT_Fortitude);
		this.addPrimaryAttribute(AttributesControl.CoreAttributes.ATT_Intellect);
		this.addPrimaryAttribute(AttributesControl.CoreAttributes.ATT_Presence);
		this.addSkill(AttributesControl.CoreAttributes.ATT_Stealth);
		this.addSkill(AttributesControl.CoreAttributes.ATT_Thievery);
		this.addSkill(AttributesControl.CoreAttributes.ATT_Survival);
		this.addSkill(AttributesControl.CoreAttributes.ATT_Lore);
		this.addSkill(AttributesControl.CoreAttributes.ATT_Diplomacy);
		this.addSkill(AttributesControl.CoreAttributes.ATT_Athletics);
		this.addSkill(AttributesControl.CoreAttributes.ATT_Awareness);
		this.addSkill(AttributesControl.CoreAttributes.ATT_Crafting);
		this.addSkill(AttributesControl.CoreAttributes.ATT_Healing);
		this.addDefence(AttributesControl.CoreAttributes.ATT_Dodge);
		this.addDefence(AttributesControl.CoreAttributes.ATT_Will);
		this.addDefence(AttributesControl.CoreAttributes.ATT_Toughness);
		this.restAttributes.addAttribute(this.addSecondaryAttribute(AttributesControl.CoreAttributes.ATT_Vitality));
		this.restAttributes.addAttribute(this.addSecondaryAttribute(AttributesControl.CoreAttributes.ATT_Wounds));
		this.addCombatAttribute(AttributesControl.CoreAttributes.ATT_Initiative);
		this.addCombatAttribute(AttributesControl.CoreAttributes.ATT_Melee);
		this.addCombatAttribute(AttributesControl.CoreAttributes.ATT_Ranged);
		this.addCombatAttribute(AttributesControl.CoreAttributes.ATT_Critical);
		this.addCombatAttribute(AttributesControl.CoreAttributes.ATT_DamageBonus);
		this.addScondaryCombatStats(AttributesControl.CoreAttributes.ATT_Backstab);
		this.addScondaryCombatStats(AttributesControl.CoreAttributes.ATT_Movement);
		this.addScondaryCombatStats(AttributesControl.CoreAttributes.ATT_Soak);
		this.addScondaryCombatStats(AttributesControl.CoreAttributes.ATT_Attacks);
		this.addScondaryCombatStats(AttributesControl.CoreAttributes.ATT_ChargeBonus);
		this.addScondaryCombatStats(AttributesControl.CoreAttributes.ATT_FlankingBonus);
		this.addScondaryCombatStats(AttributesControl.CoreAttributes.ATT_Retribution);
		this.addScondaryCombatStats(AttributesControl.CoreAttributes.ATT_Regeneration);
		this.addScondaryCombatStats(AttributesControl.CoreAttributes.ATT_DefendBonus);
		this.addScondaryCombatStats(AttributesControl.CoreAttributes.ATT_PhalanxBonus);
		this.addScondaryCombatStats(AttributesControl.CoreAttributes.ATT_ArrowRecoveryChance);
		this.addScondaryCombatStats(AttributesControl.CoreAttributes.ATT_CarryWeightBonus);
		this.addScondaryCombatStats(AttributesControl.CoreAttributes.ATT_XPBonus);
		this.addScondaryCombatStats(AttributesControl.CoreAttributes.ATT_GoldDropBonus);
		this.addDamageResistance(AttributesControl.CoreAttributes.ATT_ResEnergyCorruption);
		this.addDamageResistance(AttributesControl.CoreAttributes.ATT_ResEnergyElectric);
		this.addDamageResistance(AttributesControl.CoreAttributes.ATT_ResEnergyFire);
		this.addDamageResistance(AttributesControl.CoreAttributes.ATT_ResEnergyKinetic);
		this.addDamageResistance(AttributesControl.CoreAttributes.ATT_ResEnergySublime);
		this.addDamageResistance(AttributesControl.CoreAttributes.ATT_ResEnergyAcid);
		this.addDamageResistance(AttributesControl.CoreAttributes.ATT_ResEnergyCold);
		this.addDamageResistance(AttributesControl.CoreAttributes.ATT_ResEnergyPsychic);
		this.addDamageResistance(AttributesControl.CoreAttributes.ATT_ResPhysicalBlunt);
		this.addDamageResistance(AttributesControl.CoreAttributes.ATT_ResPhysicalPiercing);
		this.addDamageResistance(AttributesControl.CoreAttributes.ATT_ResPhysicalSlashing);
		this.addDamageResistance(AttributesControl.CoreAttributes.ATT_ResAnyNonMagical);
		this.addDamageResistance(AttributesControl.CoreAttributes.ATT_ResAnyPhysical);
		this.addDamageResistance(AttributesControl.CoreAttributes.ATT_ResAnyEnergy);
		this.addDamageResistance(AttributesControl.CoreAttributes.ATT_ResAnyMagic);
		this.addConditionResistance(AttributesControl.CoreAttributes.ATT_ResConditionCriticalHits);
		this.addConditionResistance(AttributesControl.CoreAttributes.ATT_ResConditionBlindness);
		this.addConditionResistance(AttributesControl.CoreAttributes.ATT_ResConditionConfusion);
		this.addConditionResistance(AttributesControl.CoreAttributes.ATT_ResConditionDisease);
		this.addConditionResistance(AttributesControl.CoreAttributes.ATT_ResConditionPoison);
		this.addConditionResistance(AttributesControl.CoreAttributes.ATT_ResConditionPanic);
		this.addConditionResistance(AttributesControl.CoreAttributes.ATT_ResConditionSleep);
		this.addConditionResistance(AttributesControl.CoreAttributes.ATT_ResConditionDeafness);
		this.addConditionResistance(AttributesControl.CoreAttributes.ATT_ResConditionParalysis);
		this.addConditionResistance(AttributesControl.CoreAttributes.ATT_ResConditionInsanity);
		this.addConditionResistance(AttributesControl.CoreAttributes.ATT_ResConditionCharmed);
		this.addConditionResistance(AttributesControl.CoreAttributes.ATT_ResConditionSilenced);
		this.addConditionResistance(AttributesControl.CoreAttributes.ATT_ResConditionImmobilized);
		this.addDamageBonus(AttributesControl.CoreAttributes.ATT_DmgCreaturesAbominations);
		this.addDamageBonus(AttributesControl.CoreAttributes.ATT_DmgCreaturesAnimals);
		this.addDamageBonus(AttributesControl.CoreAttributes.ATT_DmgCreaturesUndead);
		this.addDamageBonus(AttributesControl.CoreAttributes.ATT_DmgCreaturesVermin);
		this.addDamageBonus(AttributesControl.CoreAttributes.ATT_DmgGeneralMagic);
		this.addDamageBonus(AttributesControl.CoreAttributes.ATT_DmgGeneralMelee);
		this.addDamageBonus(AttributesControl.CoreAttributes.ATT_DmgGeneralRanged);
		this.addDamageBonus(AttributesControl.CoreAttributes.ATT_DmgGeneralUnarmed);
		this.addDamageBonus(AttributesControl.CoreAttributes.ATT_DmgTypeAcid);
		this.addDamageBonus(AttributesControl.CoreAttributes.ATT_DmgTypeCold);
		this.addDamageBonus(AttributesControl.CoreAttributes.ATT_DmgTypeCorruption);
		this.addDamageBonus(AttributesControl.CoreAttributes.ATT_DmgTypeFire);
		this.addDamageBonus(AttributesControl.CoreAttributes.ATT_DmgTypeKinetic);
		this.addDamageBonus(AttributesControl.CoreAttributes.ATT_DmgTypePiercing);
		this.addDamageBonus(AttributesControl.CoreAttributes.ATT_DmgTypeSlashing);
		this.addDamageBonus(AttributesControl.CoreAttributes.ATT_DmgTypeBlunt);
		this.addDamageBonus(AttributesControl.CoreAttributes.ATT_DmgTypeSublime);
		this.addDamageBonus(AttributesControl.CoreAttributes.ATT_DmgWeaponAxes);
		this.addDamageBonus(AttributesControl.CoreAttributes.ATT_DmgWeaponBows);
		this.addDamageBonus(AttributesControl.CoreAttributes.ATT_DmgWeaponClubs);
		this.addDamageBonus(AttributesControl.CoreAttributes.ATT_DmgWeaponHeavy);
		this.addDamageBonus(AttributesControl.CoreAttributes.ATT_DmgWeaponLight);
		this.addDamageBonus(AttributesControl.CoreAttributes.ATT_DmgWeaponMedium);
		this.addDamageBonus(AttributesControl.CoreAttributes.ATT_DmgWeaponSwords);
		this.addDamageBonus(AttributesControl.CoreAttributes.ATT_DmgWeaponPolearms);
		this.addHitBonus(AttributesControl.CoreAttributes.ATT_HitCombatManeuvers);
		this.addHitBonus(AttributesControl.CoreAttributes.ATT_HitCreaturesAbominations);
		this.addHitBonus(AttributesControl.CoreAttributes.ATT_HitCreaturesAnimals);
		this.addHitBonus(AttributesControl.CoreAttributes.ATT_HitCreaturesUndead);
		this.addHitBonus(AttributesControl.CoreAttributes.ATT_HitCreaturesVermin);
		this.addHitBonus(AttributesControl.CoreAttributes.ATT_HitWeaponAxes);
		this.addHitBonus(AttributesControl.CoreAttributes.ATT_HitWeaponBows);
		this.addHitBonus(AttributesControl.CoreAttributes.ATT_HitWeaponClubs);
		this.addHitBonus(AttributesControl.CoreAttributes.ATT_HitWeaponHeavy);
		this.addHitBonus(AttributesControl.CoreAttributes.ATT_HitWeaponLight);
		this.addHitBonus(AttributesControl.CoreAttributes.ATT_HitWeaponMedium);
		this.addHitBonus(AttributesControl.CoreAttributes.ATT_HitWeaponPolearms);
		this.addHitBonus(AttributesControl.CoreAttributes.ATT_HitWeaponSwords);
		this.addHitBonus(AttributesControl.CoreAttributes.ATT_HitWeaponUnarmed);
		this.addHitBonus(AttributesControl.CoreAttributes.ATT_CriticalBonusAxes);
		this.addHitBonus(AttributesControl.CoreAttributes.ATT_CriticalBonusSwords);
		this.addHitBonus(AttributesControl.CoreAttributes.ATT_CriticalBonusClubs);
		this.addHitBonus(AttributesControl.CoreAttributes.ATT_CriticalBonusBows);
		this.addHitBonus(AttributesControl.CoreAttributes.ATT_CriticalBonusUnarmed);
		this.addHitBonus(AttributesControl.CoreAttributes.ATT_CriticalBonusLight);
		this.addArmorBonus(AttributesControl.CoreAttributes.ATT_ArmEncHeavy);
		this.addArmorBonus(AttributesControl.CoreAttributes.ATT_ArmEncLight);
		this.addArmorBonus(AttributesControl.CoreAttributes.ATT_ArmEncMedium);
		this.addArmorBonus(AttributesControl.CoreAttributes.ATT_ArmDodgeUnarmored);
		this.addArmorBonus(AttributesControl.CoreAttributes.ATT_ArmEncMage);
		this.addArmorBonus(AttributesControl.CoreAttributes.ATT_ArmDodgeShield);
		this.addSpellTier(AttributesControl.CoreAttributes.ATT_SpellListAir);
		this.addSpellTier(AttributesControl.CoreAttributes.ATT_SpellListBody);
		this.addSpellTier(AttributesControl.CoreAttributes.ATT_SpellListEarth);
		this.addSpellTier(AttributesControl.CoreAttributes.ATT_SpellListFire);
		this.addSpellTier(AttributesControl.CoreAttributes.ATT_SpellListMind);
		this.addSpellTier(AttributesControl.CoreAttributes.ATT_SpellListNature);
		this.addSpellTier(AttributesControl.CoreAttributes.ATT_SpellListSpirit);
		this.addSpellTier(AttributesControl.CoreAttributes.ATT_SpellListBardic);
		this.restAttributes.addAttribute(this.addMagicalAttribute(AttributesControl.CoreAttributes.ATT_Attunement));
		this.addMagicalAttribute(AttributesControl.CoreAttributes.ATT_SpellAptitude);
		this.addMagicalAttribute(AttributesControl.CoreAttributes.ATT_SpellRadiusAura);
		this.addMagicalAttribute(AttributesControl.CoreAttributes.ATT_SpellRadiusLine);
		this.addMagicalAttribute(AttributesControl.CoreAttributes.ATT_SpellRadiusSphere);
		this.addMagicalAttribute(AttributesControl.CoreAttributes.ATT_SpellMaxCascade);
		if (MainControl.debugFunctions)
		{
			base.checkCompleteness();
		}
	}

	// Token: 0x060003CC RID: 972 RVA: 0x0001268F File Offset: 0x0001088F
	private static string getIdFromEnum(AttributesControl.CoreAttributes attribute)
	{
		if (!CharacterAttributes.enumToStringDict.ContainsKey(attribute))
		{
			CharacterAttributes.enumToStringDict.Add(attribute, attribute.ToString());
		}
		return CharacterAttributes.enumToStringDict[attribute];
	}

	// Token: 0x060003CD RID: 973 RVA: 0x000126C1 File Offset: 0x000108C1
	protected AttributesControl.Attribute addPrimaryAttribute(AttributesControl.CoreAttributes coreAttribute)
	{
		return this.createAttributeAndInsertIntoList(coreAttribute, this.primaryAttributes);
	}

	// Token: 0x060003CE RID: 974 RVA: 0x000126D0 File Offset: 0x000108D0
	protected AttributesControl.Attribute addSkill(AttributesControl.CoreAttributes coreAttribute)
	{
		return this.createAttributeAndInsertIntoList(coreAttribute, this.skillsList);
	}

	// Token: 0x060003CF RID: 975 RVA: 0x000126DF File Offset: 0x000108DF
	protected AttributesControl.Attribute addDefence(AttributesControl.CoreAttributes coreAttribute)
	{
		return this.createAttributeAndInsertIntoList(coreAttribute, this.defencesList);
	}

	// Token: 0x060003D0 RID: 976 RVA: 0x000126EE File Offset: 0x000108EE
	protected AttributesControl.Attribute addDamageResistance(AttributesControl.CoreAttributes coreAttribute)
	{
		return this.createAttributeAndInsertIntoList(coreAttribute, this.damageResistanceList);
	}

	// Token: 0x060003D1 RID: 977 RVA: 0x000126FD File Offset: 0x000108FD
	protected AttributesControl.Attribute addConditionResistance(AttributesControl.CoreAttributes coreAttribute)
	{
		return this.createAttributeAndInsertIntoList(coreAttribute, this.conditionResistanceList);
	}

	// Token: 0x060003D2 RID: 978 RVA: 0x0001270C File Offset: 0x0001090C
	protected AttributesControl.Attribute addScondaryCombatStats(AttributesControl.CoreAttributes coreAttribute)
	{
		return this.createAttributeAndInsertIntoList(coreAttribute, this.secondaryCombatAttributes);
	}

	// Token: 0x060003D3 RID: 979 RVA: 0x0001271B File Offset: 0x0001091B
	protected AttributesControl.Attribute addSecondaryAttribute(AttributesControl.CoreAttributes coreAttribute)
	{
		return this.createAttributeAndInsertIntoList(coreAttribute, this.secondaryAttributes);
	}

	// Token: 0x060003D4 RID: 980 RVA: 0x0001272A File Offset: 0x0001092A
	protected AttributesControl.Attribute addCombatAttribute(AttributesControl.CoreAttributes coreAttribute)
	{
		return this.createAttributeAndInsertIntoList(coreAttribute, this.combatAttributes);
	}

	// Token: 0x060003D5 RID: 981 RVA: 0x00012739 File Offset: 0x00010939
	protected AttributesControl.Attribute addDamageBonus(AttributesControl.CoreAttributes coreAttribute)
	{
		return this.createAttributeAndInsertIntoList(coreAttribute, this.damageBonusList);
	}

	// Token: 0x060003D6 RID: 982 RVA: 0x00012748 File Offset: 0x00010948
	protected AttributesControl.Attribute addArmorBonus(AttributesControl.CoreAttributes coreAttribute)
	{
		return this.createAttributeAndInsertIntoList(coreAttribute, this.armorBonusList);
	}

	// Token: 0x060003D7 RID: 983 RVA: 0x00012757 File Offset: 0x00010957
	protected AttributesControl.Attribute addSpellTier(AttributesControl.CoreAttributes coreAttribute)
	{
		return this.createAttributeAndInsertIntoList(coreAttribute, this.spellTiers);
	}

	// Token: 0x060003D8 RID: 984 RVA: 0x00012766 File Offset: 0x00010966
	protected AttributesControl.Attribute addMagicalAttribute(AttributesControl.CoreAttributes coreAttribute)
	{
		return this.createAttributeAndInsertIntoList(coreAttribute, this.magicalAttributes);
	}

	// Token: 0x060003D9 RID: 985 RVA: 0x00012775 File Offset: 0x00010975
	protected AttributesControl.Attribute addHitBonus(AttributesControl.CoreAttributes coreAttribute)
	{
		return this.createAttributeAndInsertIntoList(coreAttribute, this.hitBonusList);
	}

	// Token: 0x060003DA RID: 986 RVA: 0x00012784 File Offset: 0x00010984
	private AttributesControl.Attribute createAttributeAndInsertIntoList(AttributesControl.CoreAttributes coreAttribute, AttributesControl.AttributeList attributeList)
	{
		AttributesControl.Attribute attribute = base.createAttributeFromRawData(CharacterAttributes.getIdFromEnum(coreAttribute));
		if (this.attributeCache.ContainsKey(attribute.getId()))
		{
			MainControl.logError("Trying to insert duplicate ability!");
		}
		else
		{
			this.attributeCache.Add(attribute.getId(), attribute);
		}
		attributeList.addAttribute(attribute);
		return attribute;
	}

	// Token: 0x060003DB RID: 987 RVA: 0x000127D8 File Offset: 0x000109D8
	public void setExternalAttributeData(Character character)
	{
		foreach (AttributesControl.AttributeList attributeList in this.metaList)
		{
			attributeList.setExternalAttributeData(this.primaryAttributes, character);
		}
	}

	// Token: 0x060003DC RID: 988 RVA: 0x00012830 File Offset: 0x00010A30
	public void clearAllSkillAndPrimaryRanks()
	{
		this.primaryAttributes.clearAllRanks();
		this.skillsList.clearAllRanks();
	}

	// Token: 0x060003DD RID: 989 RVA: 0x00012848 File Offset: 0x00010A48
	public bool areRestableAttributesMax()
	{
		return this.restAttributes.areAttributesAtMax();
	}

	// Token: 0x060003DE RID: 990 RVA: 0x00012855 File Offset: 0x00010A55
	public void restoreRestAttributesPartially(float degree)
	{
		this.restAttributes.restoreAllAttributePartially(degree);
	}

	// Token: 0x060003DF RID: 991 RVA: 0x00012863 File Offset: 0x00010A63
	public SkaldDataList getInfoListOfPrimaryAttributes()
	{
		return this.primaryAttributes.getListOfInfoObjects();
	}

	// Token: 0x060003E0 RID: 992 RVA: 0x00012870 File Offset: 0x00010A70
	public SkaldDataList getInfoListOfSecondaryAttributes()
	{
		SkaldDataList listOfInfoObjects = this.secondaryAttributes.getListOfInfoObjects();
		this.magicalAttributes.appendObjectToList(listOfInfoObjects, "ATT_Attunement");
		return listOfInfoObjects;
	}

	// Token: 0x060003E1 RID: 993 RVA: 0x0001289C File Offset: 0x00010A9C
	public SkaldDataList getInfoListOfSkills()
	{
		return this.skillsList.getListOfInfoObjects();
	}

	// Token: 0x060003E2 RID: 994 RVA: 0x000128A9 File Offset: 0x00010AA9
	public SkaldDataList getInfoListOfSpellSchools()
	{
		return this.spellTiers.getListOfInfoObjectsIfNonZero();
	}

	// Token: 0x060003E3 RID: 995 RVA: 0x000128B6 File Offset: 0x00010AB6
	public SkaldDataList getInfoListOfMagicAttributes()
	{
		return this.magicalAttributes.getListOfInfoObjectsIfNonZero();
	}

	// Token: 0x060003E4 RID: 996 RVA: 0x000128C3 File Offset: 0x00010AC3
	public SkaldDataList getInfoListOfDefences()
	{
		return this.defencesList.getListOfInfoObjects();
	}

	// Token: 0x060003E5 RID: 997 RVA: 0x000128D0 File Offset: 0x00010AD0
	public SkaldDataList getInfoListOfCombatAttributes()
	{
		return this.combatAttributes.getListOfInfoObjects();
	}

	// Token: 0x060003E6 RID: 998 RVA: 0x000128E0 File Offset: 0x00010AE0
	public string printImmunityList()
	{
		string text = this.conditionResistanceList.printAllImmunities();
		if (text != "")
		{
			text += ", ";
		}
		return text + this.damageResistanceList.printAllImmunities();
	}

	// Token: 0x060003E7 RID: 999 RVA: 0x00012928 File Offset: 0x00010B28
	public string printResistanceList()
	{
		string text = this.conditionResistanceList.printAllResistances();
		if (text != "")
		{
			text += ", ";
		}
		return text + this.damageResistanceList.printAllResistances();
	}

	// Token: 0x060003E8 RID: 1000 RVA: 0x00012970 File Offset: 0x00010B70
	public string printVulnerabilityList()
	{
		string text = this.conditionResistanceList.printAllVulnerabiltites();
		if (text != "")
		{
			text += ", ";
		}
		return text + this.damageResistanceList.printAllVulnerabiltites();
	}

	// Token: 0x0400008A RID: 138
	private static Dictionary<AttributesControl.CoreAttributes, string> enumToStringDict = new Dictionary<AttributesControl.CoreAttributes, string>();

	// Token: 0x0400008B RID: 139
	private AttributesControl.AttributesListRest restAttributes;

	// Token: 0x0400008C RID: 140
	private AttributesControl.AttributesListSkills skillsList;

	// Token: 0x0400008D RID: 141
	private AttributesControl.AttributesListDefences defencesList;

	// Token: 0x0400008E RID: 142
	private AttributesControl.AttributesListDamageResistance damageResistanceList;

	// Token: 0x0400008F RID: 143
	private AttributesControl.AttributesListDamageBonus damageBonusList;

	// Token: 0x04000090 RID: 144
	private AttributesControl.AttributesListHitBonus hitBonusList;

	// Token: 0x04000091 RID: 145
	private AttributesControl.AttributesListArmorBonus armorBonusList;

	// Token: 0x04000092 RID: 146
	private AttributesControl.AttributesListConditionResistance conditionResistanceList;

	// Token: 0x04000093 RID: 147
	private AttributesControl.AttributesListPrimaryStats primaryAttributes;

	// Token: 0x04000094 RID: 148
	private AttributesControl.AttributesListSecondaryAttributes secondaryAttributes;

	// Token: 0x04000095 RID: 149
	private AttributesControl.AttributesListCombatAttributes combatAttributes;

	// Token: 0x04000096 RID: 150
	private AttributesControl.AttributesListSecondaryCombatAttributes secondaryCombatAttributes;

	// Token: 0x04000097 RID: 151
	private AttributesControl.AttributesListMagicSchools spellTiers;

	// Token: 0x04000098 RID: 152
	private AttributesControl.AttributesListMagicalAttributes magicalAttributes;
}
