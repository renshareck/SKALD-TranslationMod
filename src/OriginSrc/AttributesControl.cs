using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x02000002 RID: 2
[Serializable]
public class AttributesControl
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	protected void checkCompleteness()
	{
		foreach (object obj in Enum.GetValues(typeof(AttributesControl.CoreAttributes)))
		{
			AttributesControl.CoreAttributes coreAttributes = (AttributesControl.CoreAttributes)obj;
			if (coreAttributes != AttributesControl.CoreAttributes.Null)
			{
				string text = coreAttributes.ToString();
				if (this.searchForAttribute(text) == null)
				{
					MainControl.logError("A critical attribute has not been set: " + text);
				}
			}
		}
	}

	// Token: 0x06000002 RID: 2 RVA: 0x000020D8 File Offset: 0x000002D8
	public int getPowerLevel()
	{
		return 0 + this.getMaxAttributeValue(AttributesControl.CoreAttributes.ATT_Strength) + this.getMaxAttributeValue(AttributesControl.CoreAttributes.ATT_Agility) + this.getMaxAttributeValue(AttributesControl.CoreAttributes.ATT_Fortitude) + this.getMaxAttributeValue(AttributesControl.CoreAttributes.ATT_Intellect) + this.getMaxAttributeValue(AttributesControl.CoreAttributes.ATT_Presence) + this.getMaxAttributeValue(AttributesControl.CoreAttributes.ATT_Attunement) + this.getMaxAttributeValue(AttributesControl.CoreAttributes.ATT_Dodge) + this.getMaxAttributeValue(AttributesControl.CoreAttributes.ATT_Will) + this.getMaxAttributeValue(AttributesControl.CoreAttributes.ATT_Toughness) + this.getMaxAttributeValue(AttributesControl.CoreAttributes.ATT_Initiative) + this.getMaxAttributeValue(AttributesControl.CoreAttributes.ATT_Melee) + this.getMaxAttributeValue(AttributesControl.CoreAttributes.ATT_Ranged) + this.getMaxAttributeValue(AttributesControl.CoreAttributes.ATT_DamageBonus) * 2 + this.getMaxAttributeValue(AttributesControl.CoreAttributes.ATT_Soak) + this.getMaxAttributeValue(AttributesControl.CoreAttributes.ATT_Wounds) * 2;
	}

	// Token: 0x06000003 RID: 3 RVA: 0x0000216C File Offset: 0x0000036C
	protected AttributesControl.Attribute createAttributeFromRawData(string id)
	{
		SKALDProjectData.Objects.AttributeData.AttributeContainer.Attribute attributeRawData = GameData.getAttributeRawData(id);
		AttributesControl.Attribute result;
		if (attributeRawData != null)
		{
			result = new AttributesControl.Attribute(attributeRawData);
		}
		else
		{
			MainControl.logError("Missing attribute data for: " + id);
			result = new AttributesControl.Attribute(id);
		}
		return result;
	}

	// Token: 0x06000004 RID: 4 RVA: 0x000021A4 File Offset: 0x000003A4
	public static AttributesControl.CoreAttributes getEnumFromString(string id)
	{
		AttributesControl.CoreAttributes result;
		try
		{
			result = (AttributesControl.CoreAttributes)Enum.Parse(typeof(AttributesControl.CoreAttributes), id);
		}
		catch (Exception obj)
		{
			MainControl.logError(obj);
			result = AttributesControl.CoreAttributes.Null;
		}
		return result;
	}

	// Token: 0x06000005 RID: 5 RVA: 0x000021E4 File Offset: 0x000003E4
	public int getCurrentAttributeValue(AttributesControl.CoreAttributes attribute)
	{
		string name = attribute.ToString();
		return this.getCurrentAttributeValue(name);
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00002208 File Offset: 0x00000408
	public int getCurrentAttributeValue(string name)
	{
		AttributesControl.Attribute attribute = this.searchForAttribute(name);
		if (attribute != null)
		{
			return attribute.getCurrentValue();
		}
		return 0;
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00002228 File Offset: 0x00000428
	public string getAttributeName(AttributesControl.CoreAttributes attribute)
	{
		string name = attribute.ToString();
		return this.getAttributeName(name);
	}

	// Token: 0x06000008 RID: 8 RVA: 0x0000224C File Offset: 0x0000044C
	public string getAttributeName(string name)
	{
		AttributesControl.Attribute attribute = this.searchForAttribute(name);
		if (attribute != null)
		{
			return attribute.getName();
		}
		return "";
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00002270 File Offset: 0x00000470
	public int getMaxAttributeValue(AttributesControl.CoreAttributes attribute)
	{
		string name = attribute.ToString();
		return this.getMaxAttributeValue(name);
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002294 File Offset: 0x00000494
	public int getMaxAttributeValue(string name)
	{
		AttributesControl.Attribute attribute = this.searchForAttribute(name);
		if (attribute != null)
		{
			return attribute.getMaxValue();
		}
		MainControl.logError("Tried to GET non-existant attribute: " + name);
		return 0;
	}

	// Token: 0x0600000B RID: 11 RVA: 0x000022C4 File Offset: 0x000004C4
	public int getAttributeRank(AttributesControl.CoreAttributes attribute)
	{
		string name = attribute.ToString();
		return this.getAttributeRank(name);
	}

	// Token: 0x0600000C RID: 12 RVA: 0x000022E8 File Offset: 0x000004E8
	public int getAttributeRank(string name)
	{
		AttributesControl.Attribute attribute = this.searchForAttribute(name);
		if (attribute != null)
		{
			return attribute.getRank();
		}
		MainControl.logError("Tried to GET non-existant attribute: " + name);
		return 0;
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00002318 File Offset: 0x00000518
	public void setAttributeCurrentValue(AttributesControl.CoreAttributes name, int value)
	{
		this.setAttributeCurrentValue(name.ToString(), value);
	}

	// Token: 0x0600000E RID: 14 RVA: 0x00002330 File Offset: 0x00000530
	public void setAttributeCurrentValue(string name, int value)
	{
		AttributesControl.Attribute attribute = this.searchForAttribute(name);
		if (attribute != null)
		{
			attribute.setCurrentValue(value);
			return;
		}
		MainControl.logError("Tried to SET non-existant attribute current: " + name);
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00002360 File Offset: 0x00000560
	public void setAttributeRank(AttributesControl.CoreAttributes name, int value)
	{
		this.setAttributeRank(name.ToString(), value);
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00002378 File Offset: 0x00000578
	public void setAttributeRank(string name, int value)
	{
		AttributesControl.Attribute attribute = this.searchForAttribute(name);
		if (attribute != null)
		{
			attribute.setRank(value);
			return;
		}
		MainControl.logError("Tried to SET non-existant attribute current: " + name);
	}

	// Token: 0x06000011 RID: 17 RVA: 0x000023A8 File Offset: 0x000005A8
	public void addToAttributeRank(AttributesControl.CoreAttributes name, int value)
	{
		this.addToAttributeRank(name.ToString(), value);
	}

	// Token: 0x06000012 RID: 18 RVA: 0x000023C0 File Offset: 0x000005C0
	public void addToAttributeRank(string name, int value)
	{
		AttributesControl.Attribute attribute = this.searchForAttribute(name);
		if (attribute != null)
		{
			attribute.addToRank(value);
			return;
		}
		MainControl.logError("Tried to SET non-existant attribute current: " + name);
	}

	// Token: 0x06000013 RID: 19 RVA: 0x000023F0 File Offset: 0x000005F0
	public bool attributeExists(string id)
	{
		return id != null && !(id == "") && this.searchForAttribute(id) != null;
	}

	// Token: 0x06000014 RID: 20 RVA: 0x00002410 File Offset: 0x00000610
	public void resetAttributeToAbsoluteMax(AttributesControl.CoreAttributes name)
	{
		AttributesControl.Attribute attribute = this.searchForAttribute(name.ToString());
		if (attribute != null)
		{
			attribute.resetToAbsoluteMax();
			return;
		}
		MainControl.logError("Tried to RESET non-existant attribute to absolute max: " + name.ToString());
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00002458 File Offset: 0x00000658
	public void resetAllAttributesToAbsoluteMax()
	{
		foreach (AttributesControl.AttributeList attributeList in this.metaList)
		{
			attributeList.resetAllAttributesToAbsoluteMax();
		}
	}

	// Token: 0x06000016 RID: 22 RVA: 0x000024A8 File Offset: 0x000006A8
	public void restoreAllAttributesPartially(float degree)
	{
		foreach (AttributesControl.AttributeList attributeList in this.metaList)
		{
			attributeList.restoreAllAttributePartially(degree);
		}
	}

	// Token: 0x06000017 RID: 23 RVA: 0x000024FC File Offset: 0x000006FC
	public void addToAttributeCurrentValue(AttributesControl.CoreAttributes name, int value)
	{
		this.addToAttributeCurrentValue(name.ToString(), value);
	}

	// Token: 0x06000018 RID: 24 RVA: 0x00002514 File Offset: 0x00000714
	public void addToAttributeCurrentValue(string name, int value)
	{
		AttributesControl.Attribute attribute = this.searchForAttribute(name);
		if (attribute != null)
		{
			attribute.addToCurrent(value);
			return;
		}
		MainControl.logError("Tried to add to non-existant attribute: " + name);
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00002544 File Offset: 0x00000744
	public void setLevelFactor(AttributesControl.CoreAttributes name, float value)
	{
		AttributesControl.Attribute attribute = this.searchForAttribute(name.ToString());
		if (attribute != null)
		{
			attribute.setLevelFactor(value);
			return;
		}
		MainControl.logError("Tried to add to set non-existant attribute: " + name.ToString());
	}

	// Token: 0x0600001A RID: 26 RVA: 0x0000258C File Offset: 0x0000078C
	protected AttributesControl.Attribute searchForAttribute(string id)
	{
		if (id == "")
		{
			return null;
		}
		if (this.attributeCache.ContainsKey(id))
		{
			return this.attributeCache[id];
		}
		foreach (AttributesControl.AttributeList attributeList in this.metaList)
		{
			AttributesControl.Attribute attribute = attributeList.getAttribute(id);
			if (attribute != null)
			{
				this.attributeCache.Add(id, attribute);
				return attribute;
			}
		}
		MainControl.logError("Could not find Attribute with id: " + id);
		return null;
	}

	// Token: 0x04000001 RID: 1
	protected List<AttributesControl.AttributeList> metaList;

	// Token: 0x04000002 RID: 2
	protected Dictionary<string, AttributesControl.Attribute> attributeCache = new Dictionary<string, AttributesControl.Attribute>();

	// Token: 0x020001A0 RID: 416
	public enum CoreAttributes
	{
		// Token: 0x040005F7 RID: 1527
		ATT_Strength,
		// Token: 0x040005F8 RID: 1528
		ATT_Agility,
		// Token: 0x040005F9 RID: 1529
		ATT_Fortitude,
		// Token: 0x040005FA RID: 1530
		ATT_Intellect,
		// Token: 0x040005FB RID: 1531
		ATT_Presence,
		// Token: 0x040005FC RID: 1532
		ATT_Attunement,
		// Token: 0x040005FD RID: 1533
		ATT_Vitality,
		// Token: 0x040005FE RID: 1534
		ATT_Wounds,
		// Token: 0x040005FF RID: 1535
		ATT_Diplomacy,
		// Token: 0x04000600 RID: 1536
		ATT_Dodge,
		// Token: 0x04000601 RID: 1537
		ATT_Will,
		// Token: 0x04000602 RID: 1538
		ATT_Toughness,
		// Token: 0x04000603 RID: 1539
		ATT_Soak,
		// Token: 0x04000604 RID: 1540
		ATT_Melee,
		// Token: 0x04000605 RID: 1541
		ATT_Ranged,
		// Token: 0x04000606 RID: 1542
		ATT_Initiative,
		// Token: 0x04000607 RID: 1543
		ATT_Critical,
		// Token: 0x04000608 RID: 1544
		ATT_Movement,
		// Token: 0x04000609 RID: 1545
		ATT_Stealth,
		// Token: 0x0400060A RID: 1546
		ATT_Thievery,
		// Token: 0x0400060B RID: 1547
		ATT_Athletics,
		// Token: 0x0400060C RID: 1548
		ATT_Backstab,
		// Token: 0x0400060D RID: 1549
		ATT_Awareness,
		// Token: 0x0400060E RID: 1550
		ATT_Lore,
		// Token: 0x0400060F RID: 1551
		ATT_Attacks,
		// Token: 0x04000610 RID: 1552
		ATT_ChargeBonus,
		// Token: 0x04000611 RID: 1553
		ATT_FlankingBonus,
		// Token: 0x04000612 RID: 1554
		ATT_Survival,
		// Token: 0x04000613 RID: 1555
		ATT_Crafting,
		// Token: 0x04000614 RID: 1556
		ATT_Healing,
		// Token: 0x04000615 RID: 1557
		ATT_DamageBonus,
		// Token: 0x04000616 RID: 1558
		Null,
		// Token: 0x04000617 RID: 1559
		ATT_Retribution,
		// Token: 0x04000618 RID: 1560
		ATT_ResEnergyCorruption,
		// Token: 0x04000619 RID: 1561
		ATT_ResEnergyElectric,
		// Token: 0x0400061A RID: 1562
		ATT_ResEnergyFire,
		// Token: 0x0400061B RID: 1563
		ATT_ResEnergyKinetic,
		// Token: 0x0400061C RID: 1564
		ATT_ResEnergySublime,
		// Token: 0x0400061D RID: 1565
		ATT_ResAnyMagic,
		// Token: 0x0400061E RID: 1566
		ATT_ResPhysicalBlunt,
		// Token: 0x0400061F RID: 1567
		ATT_ResPhysicalPiercing,
		// Token: 0x04000620 RID: 1568
		ATT_ResPhysicalSlashing,
		// Token: 0x04000621 RID: 1569
		ATT_ResEnergyAcid,
		// Token: 0x04000622 RID: 1570
		ATT_ResEnergyCold,
		// Token: 0x04000623 RID: 1571
		ATT_ResAnyNonMagical,
		// Token: 0x04000624 RID: 1572
		ATT_ResConditionCriticalHits,
		// Token: 0x04000625 RID: 1573
		ATT_Regeneration,
		// Token: 0x04000626 RID: 1574
		ATT_ResConditionBlindness,
		// Token: 0x04000627 RID: 1575
		ATT_ResConditionConfusion,
		// Token: 0x04000628 RID: 1576
		ATT_ResConditionDisease,
		// Token: 0x04000629 RID: 1577
		ATT_ResConditionPoison,
		// Token: 0x0400062A RID: 1578
		ATT_ResConditionPanic,
		// Token: 0x0400062B RID: 1579
		ATT_ResConditionDeafness,
		// Token: 0x0400062C RID: 1580
		ATT_ResConditionParalysis,
		// Token: 0x0400062D RID: 1581
		ATT_ResConditionInsanity,
		// Token: 0x0400062E RID: 1582
		ATT_DmgWeaponPolearms,
		// Token: 0x0400062F RID: 1583
		ATT_DmgWeaponSwords,
		// Token: 0x04000630 RID: 1584
		ATT_DmgWeaponMedium,
		// Token: 0x04000631 RID: 1585
		ATT_DmgWeaponLight,
		// Token: 0x04000632 RID: 1586
		ATT_DmgWeaponHeavy,
		// Token: 0x04000633 RID: 1587
		ATT_DmgWeaponClubs,
		// Token: 0x04000634 RID: 1588
		ATT_DmgWeaponBows,
		// Token: 0x04000635 RID: 1589
		ATT_DmgWeaponAxes,
		// Token: 0x04000636 RID: 1590
		ATT_DmgTypeSublime,
		// Token: 0x04000637 RID: 1591
		ATT_DmgTypeSlashing,
		// Token: 0x04000638 RID: 1592
		ATT_DmgTypePiercing,
		// Token: 0x04000639 RID: 1593
		ATT_DmgTypeKinetic,
		// Token: 0x0400063A RID: 1594
		ATT_DmgTypeFire,
		// Token: 0x0400063B RID: 1595
		ATT_DmgTypeCold,
		// Token: 0x0400063C RID: 1596
		ATT_DmgTypeAcid,
		// Token: 0x0400063D RID: 1597
		ATT_DmgGeneralUnarmed,
		// Token: 0x0400063E RID: 1598
		ATT_DmgTypeCorruption,
		// Token: 0x0400063F RID: 1599
		ATT_DmgGeneralRanged,
		// Token: 0x04000640 RID: 1600
		ATT_DmgGeneralMelee,
		// Token: 0x04000641 RID: 1601
		ATT_DmgGeneralMagic,
		// Token: 0x04000642 RID: 1602
		ATT_DmgCreaturesVermin,
		// Token: 0x04000643 RID: 1603
		ATT_DmgCreaturesUndead,
		// Token: 0x04000644 RID: 1604
		ATT_DmgCreaturesAnimals,
		// Token: 0x04000645 RID: 1605
		ATT_DmgCreaturesAbominations,
		// Token: 0x04000646 RID: 1606
		ATT_HitCombatManeuvers,
		// Token: 0x04000647 RID: 1607
		ATT_HitCreaturesAbominations,
		// Token: 0x04000648 RID: 1608
		ATT_HitCreaturesAnimals,
		// Token: 0x04000649 RID: 1609
		ATT_HitCreaturesUndead,
		// Token: 0x0400064A RID: 1610
		ATT_HitCreaturesVermin,
		// Token: 0x0400064B RID: 1611
		ATT_HitWeaponAxes,
		// Token: 0x0400064C RID: 1612
		ATT_HitWeaponBows,
		// Token: 0x0400064D RID: 1613
		ATT_HitWeaponClubs,
		// Token: 0x0400064E RID: 1614
		ATT_HitWeaponHeavy,
		// Token: 0x0400064F RID: 1615
		ATT_HitWeaponLight,
		// Token: 0x04000650 RID: 1616
		ATT_HitWeaponMedium,
		// Token: 0x04000651 RID: 1617
		ATT_HitWeaponUnarmed,
		// Token: 0x04000652 RID: 1618
		ATT_HitWeaponPolearms,
		// Token: 0x04000653 RID: 1619
		ATT_HitWeaponSwords,
		// Token: 0x04000654 RID: 1620
		ATT_ArmEncHeavy,
		// Token: 0x04000655 RID: 1621
		ATT_ArmEncLight,
		// Token: 0x04000656 RID: 1622
		ATT_ArmEncMedium,
		// Token: 0x04000657 RID: 1623
		ATT_ArmDodgeUnarmored,
		// Token: 0x04000658 RID: 1624
		ATT_ArmDodgeShield,
		// Token: 0x04000659 RID: 1625
		ATT_ArmEncMage,
		// Token: 0x0400065A RID: 1626
		ATT_ResConditionSleep,
		// Token: 0x0400065B RID: 1627
		ATT_ResConditionCharmed,
		// Token: 0x0400065C RID: 1628
		ATT_ResConditionSilenced,
		// Token: 0x0400065D RID: 1629
		ATT_DefendBonus,
		// Token: 0x0400065E RID: 1630
		ATT_ResConditionImmobilized,
		// Token: 0x0400065F RID: 1631
		ATT_ResAnyPhysical,
		// Token: 0x04000660 RID: 1632
		ATT_ResAnyEnergy,
		// Token: 0x04000661 RID: 1633
		ATT_DmgTypeBlunt,
		// Token: 0x04000662 RID: 1634
		ATT_PhalanxBonus,
		// Token: 0x04000663 RID: 1635
		ATT_CriticalBonusAxes,
		// Token: 0x04000664 RID: 1636
		ATT_CriticalBonusSwords,
		// Token: 0x04000665 RID: 1637
		ATT_CriticalBonusClubs,
		// Token: 0x04000666 RID: 1638
		ATT_CriticalBonusBows,
		// Token: 0x04000667 RID: 1639
		ATT_CriticalBonusUnarmed,
		// Token: 0x04000668 RID: 1640
		ATT_ArrowRecoveryChance,
		// Token: 0x04000669 RID: 1641
		ATT_CriticalBonusLight,
		// Token: 0x0400066A RID: 1642
		ATT_SpellRadiusSphere,
		// Token: 0x0400066B RID: 1643
		ATT_SpellRadiusLine,
		// Token: 0x0400066C RID: 1644
		ATT_SpellRadiusAura,
		// Token: 0x0400066D RID: 1645
		ATT_ResEnergyPsychic,
		// Token: 0x0400066E RID: 1646
		ATT_SpellListAir,
		// Token: 0x0400066F RID: 1647
		ATT_SpellListBody,
		// Token: 0x04000670 RID: 1648
		ATT_SpellListEarth,
		// Token: 0x04000671 RID: 1649
		ATT_SpellListFire,
		// Token: 0x04000672 RID: 1650
		ATT_SpellListMind,
		// Token: 0x04000673 RID: 1651
		ATT_SpellListNature,
		// Token: 0x04000674 RID: 1652
		ATT_SpellListSpirit,
		// Token: 0x04000675 RID: 1653
		ATT_SpellAptitude,
		// Token: 0x04000676 RID: 1654
		ATT_CarryWeightBonus,
		// Token: 0x04000677 RID: 1655
		ATT_XPBonus,
		// Token: 0x04000678 RID: 1656
		ATT_GoldDropBonus,
		// Token: 0x04000679 RID: 1657
		ATT_SpellMaxCascade,
		// Token: 0x0400067A RID: 1658
		ATT_SpellListBardic
	}

	// Token: 0x020001A1 RID: 417
	[Serializable]
	protected class AttributesListRest : AttributesControl.AttributeList
	{
		// Token: 0x06001583 RID: 5507 RVA: 0x00060C74 File Offset: 0x0005EE74
		public override string getName()
		{
			return "Rest Attributes";
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x00060C7B File Offset: 0x0005EE7B
		public override string getDescription()
		{
			return "Attributes restored by rest.";
		}
	}

	// Token: 0x020001A2 RID: 418
	[Serializable]
	protected class AttributesListMagicalAttributes : AttributesControl.AttributeList
	{
		// Token: 0x06001586 RID: 5510 RVA: 0x00060C8A File Offset: 0x0005EE8A
		public override string getName()
		{
			return "Magical Attributes";
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x00060C91 File Offset: 0x0005EE91
		public override string getDescription()
		{
			return "Attributes involved in spell-casting.";
		}
	}

	// Token: 0x020001A3 RID: 419
	[Serializable]
	protected class AttributesListMagicSchools : AttributesControl.AttributeList
	{
		// Token: 0x06001589 RID: 5513 RVA: 0x00060CA0 File Offset: 0x0005EEA0
		public override string getName()
		{
			return "Magic Schools";
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x00060CA7 File Offset: 0x0005EEA7
		public override string getDescription()
		{
			return "The Schools of Magic. The value of this attribute indicates the maximum tier of spell from that school the character may cast.";
		}
	}

	// Token: 0x020001A4 RID: 420
	[Serializable]
	protected class AttributesListSecondaryCombatAttributes : AttributesControl.AttributeList
	{
		// Token: 0x0600158C RID: 5516 RVA: 0x00060CB6 File Offset: 0x0005EEB6
		public override string getName()
		{
			return "Secondary Combat Stats";
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x00060CBD File Offset: 0x0005EEBD
		public override string getDescription()
		{
			return "Secondary combat stats are not derived from the primary attributes. Instead they are typically modified by feats.";
		}
	}

	// Token: 0x020001A5 RID: 421
	[Serializable]
	protected class AttributesListCombatAttributes : AttributesControl.AttributeList
	{
		// Token: 0x0600158F RID: 5519 RVA: 0x00060CCC File Offset: 0x0005EECC
		public override string getName()
		{
			return "Combat Stats";
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x00060CD3 File Offset: 0x0005EED3
		public override string getDescription()
		{
			return "Derived attributes that are used during combat.";
		}
	}

	// Token: 0x020001A6 RID: 422
	[Serializable]
	protected class AttributesListSecondaryAttributes : AttributesControl.AttributeList
	{
		// Token: 0x06001592 RID: 5522 RVA: 0x00060CE2 File Offset: 0x0005EEE2
		public override string getName()
		{
			return "Secondary Stats";
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x00060CE9 File Offset: 0x0005EEE9
		public override string getDescription()
		{
			return "Secondary attributes that tend to vary during play.";
		}
	}

	// Token: 0x020001A7 RID: 423
	[Serializable]
	protected class AttributesListSkills : AttributesControl.AttributeList
	{
		// Token: 0x06001595 RID: 5525 RVA: 0x00060CF8 File Offset: 0x0005EEF8
		public override string getName()
		{
			return "Skills";
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x00060CFF File Offset: 0x0005EEFF
		public override string getDescription()
		{
			return "Skills are derived from primary attributes and are used to perform most non-combat tasks in the game.";
		}
	}

	// Token: 0x020001A8 RID: 424
	[Serializable]
	protected class AttributesListDefences : AttributesControl.AttributeList
	{
		// Token: 0x06001598 RID: 5528 RVA: 0x00060D0E File Offset: 0x0005EF0E
		public override string getName()
		{
			return "Defences";
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x00060D15 File Offset: 0x0005EF15
		public override string getDescription()
		{
			return "Defences are used to defend against attacks and negative effects.";
		}
	}

	// Token: 0x020001A9 RID: 425
	[Serializable]
	protected class AttributesListDamageResistance : AttributesControl.AttributeList
	{
		// Token: 0x0600159B RID: 5531 RVA: 0x00060D24 File Offset: 0x0005EF24
		public override string getName()
		{
			return "Damage Resistances";
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x00060D2B File Offset: 0x0005EF2B
		public override string getDescription()
		{
			return "Resistances to specific damage-types.";
		}
	}

	// Token: 0x020001AA RID: 426
	[Serializable]
	protected class AttributesListDamageBonus : AttributesControl.AttributeList
	{
		// Token: 0x0600159E RID: 5534 RVA: 0x00060D3A File Offset: 0x0005EF3A
		public override string getName()
		{
			return "Damage Bonuses";
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x00060D41 File Offset: 0x0005EF41
		public override string getDescription()
		{
			return "Bonuses to specific damage-types.";
		}
	}

	// Token: 0x020001AB RID: 427
	[Serializable]
	protected class AttributesListHitBonus : AttributesControl.AttributeList
	{
		// Token: 0x060015A1 RID: 5537 RVA: 0x00060D50 File Offset: 0x0005EF50
		public override string getName()
		{
			return "To Hit Bonuses";
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x00060D57 File Offset: 0x0005EF57
		public override string getDescription()
		{
			return "Bonuses to hit using specic weapons and against specific targets.";
		}
	}

	// Token: 0x020001AC RID: 428
	[Serializable]
	protected class AttributesListArmorBonus : AttributesControl.AttributeList
	{
		// Token: 0x060015A4 RID: 5540 RVA: 0x00060D66 File Offset: 0x0005EF66
		public override string getName()
		{
			return "Armor Bonuses";
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x00060D6D File Offset: 0x0005EF6D
		public override string getDescription()
		{
			return "Bonuses when wearing different types of armor.";
		}
	}

	// Token: 0x020001AD RID: 429
	[Serializable]
	protected class AttributesListConditionResistance : AttributesControl.AttributeList
	{
		// Token: 0x060015A7 RID: 5543 RVA: 0x00060D7C File Offset: 0x0005EF7C
		public override string getName()
		{
			return "Condition Resistances";
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x00060D83 File Offset: 0x0005EF83
		public override string getDescription()
		{
			return "Resistances to specific conditions.";
		}
	}

	// Token: 0x020001AE RID: 430
	[Serializable]
	protected class AttributesListPrimaryStats : AttributesControl.AttributeList
	{
		// Token: 0x060015AA RID: 5546 RVA: 0x00060D92 File Offset: 0x0005EF92
		public override string getName()
		{
			return "Primary Stats";
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x00060D99 File Offset: 0x0005EF99
		public override string getDescription()
		{
			return "The major, defining attributes for the character. These influence most other attributes in the game.";
		}
	}

	// Token: 0x020001AF RID: 431
	protected abstract class AttributeList : SkaldObjectList
	{
		// Token: 0x060015AD RID: 5549 RVA: 0x00060DA8 File Offset: 0x0005EFA8
		public AttributeList()
		{
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x00060DBC File Offset: 0x0005EFBC
		public List<AttributesControl.Attribute.AttributeSaveData> getSaveDataList()
		{
			List<AttributesControl.Attribute.AttributeSaveData> list = new List<AttributesControl.Attribute.AttributeSaveData>();
			foreach (SkaldBaseObject skaldBaseObject in this.objectList)
			{
				AttributesControl.Attribute attribute = (AttributesControl.Attribute)skaldBaseObject;
				if (attribute.shouldBeSaved())
				{
					list.Add(attribute.getSaveData());
				}
			}
			return list;
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x00060E28 File Offset: 0x0005F028
		public void setSaveData(List<AttributesControl.Attribute.AttributeSaveData> saveDataList)
		{
			if (saveDataList == null || saveDataList.Count == 0)
			{
				return;
			}
			foreach (AttributesControl.Attribute.AttributeSaveData attributeSaveData in saveDataList)
			{
				AttributesControl.Attribute attribute = this.getAttribute(attributeSaveData.id);
				if (attribute != null)
				{
					attribute.setSaveData(attributeSaveData);
				}
			}
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x00060E94 File Offset: 0x0005F094
		public void setExternalAttributeData(AttributesControl.AttributeList primaryAbilities, Character character)
		{
			foreach (SkaldBaseObject skaldBaseObject in this.objectList)
			{
				((AttributesControl.Attribute)skaldBaseObject).setExternalAttributeData(primaryAbilities, character);
			}
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x00060EEC File Offset: 0x0005F0EC
		public void addAttribute(AttributesControl.Attribute attribute)
		{
			if (this.attributeCache.ContainsKey(attribute.getId()))
			{
				MainControl.logError("Attribute " + attribute.getId() + " is added multiple times into list " + this.getName());
				return;
			}
			this.add(attribute);
			this.attributeCache.Add(attribute.getId(), attribute);
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x00060F48 File Offset: 0x0005F148
		public AttributesControl.Attribute getAttribute(string id)
		{
			if (id == "")
			{
				return null;
			}
			if (this.attributeCache.ContainsKey(id))
			{
				return this.attributeCache[id];
			}
			AttributesControl.Attribute attribute = base.getObject(id) as AttributesControl.Attribute;
			Debug.LogError("Cache Miss!");
			if (attribute == null)
			{
				return null;
			}
			this.attributeCache.Add(attribute.getId(), attribute);
			return attribute;
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x00060FB0 File Offset: 0x0005F1B0
		public void resetAllAttributesToAbsoluteMax()
		{
			foreach (SkaldBaseObject skaldBaseObject in this.objectList)
			{
				((AttributesControl.Attribute)skaldBaseObject).resetToAbsoluteMax();
			}
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x00061008 File Offset: 0x0005F208
		public void clearAllRanks()
		{
			foreach (SkaldBaseObject skaldBaseObject in this.objectList)
			{
				((AttributesControl.Attribute)skaldBaseObject).clearRank();
			}
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x00061060 File Offset: 0x0005F260
		public void resetAllAttributesToCurrentMax()
		{
			foreach (SkaldBaseObject skaldBaseObject in this.objectList)
			{
				((AttributesControl.Attribute)skaldBaseObject).resetToMaxValue();
			}
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x000610B8 File Offset: 0x0005F2B8
		public void restoreAllAttributePartially(float degree)
		{
			foreach (SkaldBaseObject skaldBaseObject in this.objectList)
			{
				((AttributesControl.Attribute)skaldBaseObject).restoreAttributePartially(degree);
			}
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x00061110 File Offset: 0x0005F310
		public SkaldDataList getListOfInfoObjects()
		{
			SkaldDataList skaldDataList = new SkaldDataList(this.getName(), this.getDescription());
			foreach (SkaldBaseObject skaldBaseObject in this.objectList)
			{
				AttributesControl.Attribute attribute = (AttributesControl.Attribute)skaldBaseObject;
				skaldDataList.addEntry(attribute.getId(), attribute.getName(), attribute.printValue(), attribute.getDescription());
			}
			return skaldDataList;
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x00061194 File Offset: 0x0005F394
		public SkaldDataList appendObjectToList(SkaldDataList list, string objectId)
		{
			SkaldBaseObject @object = base.getObject(objectId);
			if (@object == null)
			{
				return list;
			}
			AttributesControl.Attribute attribute = @object as AttributesControl.Attribute;
			list.addEntry(attribute.getId(), attribute.getName(), attribute.printValue(), attribute.getDescription());
			return list;
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x000611D4 File Offset: 0x0005F3D4
		public SkaldDataList getListOfInfoObjectsIfNonZero()
		{
			SkaldDataList skaldDataList = new SkaldDataList(this.getName(), this.getDescription());
			bool flag = false;
			foreach (SkaldBaseObject skaldBaseObject in this.objectList)
			{
				AttributesControl.Attribute attribute = (AttributesControl.Attribute)skaldBaseObject;
				if (attribute.getCurrentValue() != 0)
				{
					skaldDataList.addEntry(attribute.getId(), attribute.getName(), attribute.printValue(), attribute.getDescription());
					flag = true;
				}
			}
			if (!flag)
			{
				skaldDataList.addEntry("Empty", "--Empty--", "", "This list will only show entries with non-zero values.");
			}
			return skaldDataList;
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x00061280 File Offset: 0x0005F480
		public bool areAttributesAtMax()
		{
			using (List<SkaldBaseObject>.Enumerator enumerator = this.objectList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!((AttributesControl.Attribute)enumerator.Current).isAtMax())
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x000612E0 File Offset: 0x0005F4E0
		public string printAllVulnerabiltites()
		{
			string text = "";
			foreach (SkaldBaseObject skaldBaseObject in this.objectList)
			{
				AttributesControl.Attribute attribute = (AttributesControl.Attribute)skaldBaseObject;
				if (attribute.getCurrentValue() < 0)
				{
					text = text + attribute.getAbbreviation() + ", ";
				}
			}
			text = TextTools.removeTrailingComma(text);
			return text;
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x0006135C File Offset: 0x0005F55C
		public string printAllResistances()
		{
			string text = "";
			foreach (SkaldBaseObject skaldBaseObject in this.objectList)
			{
				AttributesControl.Attribute attribute = (AttributesControl.Attribute)skaldBaseObject;
				if (attribute.getCurrentValue() > 0 && attribute.getCurrentValue() < 100)
				{
					text = text + attribute.getAbbreviation() + ", ";
				}
			}
			text = TextTools.removeTrailingComma(text);
			return text;
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x000613E0 File Offset: 0x0005F5E0
		public string printAllImmunities()
		{
			string text = "";
			foreach (SkaldBaseObject skaldBaseObject in this.objectList)
			{
				AttributesControl.Attribute attribute = (AttributesControl.Attribute)skaldBaseObject;
				if (attribute.getCurrentValue() >= 100)
				{
					text = text + attribute.getAbbreviation() + ", ";
				}
			}
			text = TextTools.removeTrailingComma(text);
			return text;
		}

		// Token: 0x0400067B RID: 1659
		private Dictionary<string, AttributesControl.Attribute> attributeCache = new Dictionary<string, AttributesControl.Attribute>();
	}

	// Token: 0x020001B0 RID: 432
	protected class Attribute : SkaldBaseObject
	{
		// Token: 0x060015BE RID: 5566 RVA: 0x0006145C File Offset: 0x0005F65C
		public Attribute(string id) : base(id)
		{
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x0006147B File Offset: 0x0005F67B
		public Attribute(SKALDProjectData.Objects.AttributeData.AttributeContainer.Attribute rawData) : this(rawData.id)
		{
			this.ranks = (short)rawData.ranks;
			this.setLevelFactor((float)rawData.levelFactor);
			this.resetToAbsoluteMax();
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x000614A9 File Offset: 0x0005F6A9
		public bool shouldBeSaved()
		{
			return this.ranks != 0 || this.drain != 0 || this.levelFactor != 0f;
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x000614CD File Offset: 0x0005F6CD
		public override string getColorTag()
		{
			return C64Color.YELLOW_TAG;
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x000614D4 File Offset: 0x0005F6D4
		private void addInfluence(AttributesControl.Attribute attribute)
		{
			if (!this.influences.Contains(attribute))
			{
				this.influences.Add(attribute);
			}
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x000614F0 File Offset: 0x0005F6F0
		public void setLevelFactor(float factor)
		{
			this.levelFactor = factor;
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x000614F9 File Offset: 0x0005F6F9
		public void setRank(int rank)
		{
			this.ranks = (short)rank;
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x00061503 File Offset: 0x0005F703
		public void addRank(int amount)
		{
			this.ranks += (short)amount;
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x00061515 File Offset: 0x0005F715
		public void clearRank()
		{
			this.ranks = 0;
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x0006151E File Offset: 0x0005F71E
		public int getRank()
		{
			return (int)this.ranks;
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x00061526 File Offset: 0x0005F726
		private int getAbilityBonus()
		{
			if (this.attributeOwner == null || this.attributeOwner.getAbilityManueverContainer() == null)
			{
				return 0;
			}
			return this.attributeOwner.getAbilityPassiveContainer().getModifierToAttributeFromComponents(this.getId());
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x00061555 File Offset: 0x0005F755
		private int getConditionBonus()
		{
			if (this.attributeOwner == null || this.attributeOwner.getConditionContainer() == null)
			{
				return 0;
			}
			return this.attributeOwner.getConditionContainer().getModifierToAttributeFromComponents(this.getId());
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x00061584 File Offset: 0x0005F784
		public void addToRank(int rank)
		{
			this.ranks += (short)rank;
			this.resetToMaxValue();
			foreach (AttributesControl.Attribute attribute in this.influences)
			{
				attribute.resetToMaxValue();
			}
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x000615EC File Offset: 0x0005F7EC
		public override string getListName()
		{
			return TextTools.formateNameValuePair(base.getName(), this.printValue());
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x00061600 File Offset: 0x0005F800
		public string printValue()
		{
			int currentValue = this.getCurrentValue();
			int maxValue = this.getMaxValue();
			int staticMaxValue = this.getStaticMaxValue();
			string text = currentValue.ToString();
			if (!this.isAtMax() && currentValue <= maxValue)
			{
				text = text + "/" + maxValue.ToString();
			}
			if (maxValue > staticMaxValue)
			{
				text += " (+)";
			}
			else if (maxValue < staticMaxValue)
			{
				text += " (-)";
			}
			return text;
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x0006166C File Offset: 0x0005F86C
		public bool isAtMax()
		{
			return this.getCurrentValue() == this.getMaxValue();
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x0006167C File Offset: 0x0005F87C
		public void setExternalAttributeData(AttributesControl.AttributeList primaryAttributeList, Character character)
		{
			this.attributeOwner = character;
			this.setRootAbilities(primaryAttributeList);
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x0006168C File Offset: 0x0005F88C
		private void setRootAbilities(AttributesControl.AttributeList primaryAttributeList)
		{
			SKALDProjectData.Objects.AttributeData.AttributeContainer.Attribute rawData = this.getRawData();
			if (rawData == null)
			{
				return;
			}
			AttributesControl.Attribute attribute = primaryAttributeList.getAttribute(rawData.rootAbility);
			if (attribute == null)
			{
				return;
			}
			this.rootAbilities.Add(attribute);
			attribute.addInfluence(this);
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x000616C8 File Offset: 0x0005F8C8
		private SKALDProjectData.Objects.AttributeData.AttributeContainer.Attribute getRawData()
		{
			return GameData.getAttributeRawData(this.getId());
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x000616D8 File Offset: 0x0005F8D8
		public override string getName()
		{
			SKALDProjectData.Objects.AttributeData.AttributeContainer.Attribute rawData = this.getRawData();
			if (rawData == null)
			{
				return this.getId();
			}
			return rawData.title;
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x000616FC File Offset: 0x0005F8FC
		public string getAbbreviation()
		{
			SKALDProjectData.Objects.AttributeData.AttributeContainer.Attribute rawData = this.getRawData();
			if (rawData == null || rawData.abbreviation == "")
			{
				return this.getName();
			}
			return rawData.abbreviation;
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x00061734 File Offset: 0x0005F934
		public float getRootFactor()
		{
			SKALDProjectData.Objects.AttributeData.AttributeContainer.Attribute rawData = this.getRawData();
			if (rawData == null)
			{
				return 1f;
			}
			return (float)rawData.rootFactorPercentage / 100f;
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x0006175E File Offset: 0x0005F95E
		private int getBaseLevel()
		{
			if (this.attributeOwner == null)
			{
				return 1;
			}
			return this.attributeOwner.getLevel();
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x00061778 File Offset: 0x0005F978
		public override string getDescription()
		{
			AttributesControl.Attribute.stringBuilder.Clear();
			SKALDProjectData.Objects.AttributeData.AttributeContainer.Attribute rawData = this.getRawData();
			if (rawData != null)
			{
				AttributesControl.Attribute.stringBuilder.Append(rawData.description);
			}
			AttributesControl.Attribute.stringBuilder.Append("\n\n");
			if (rawData.countArmorEncumberance || rawData.countHelmetEncumberance || rawData.countGloveEncumberance || rawData.countShoeEncumberance)
			{
				List<string> list = new List<string>();
				if (rawData.countArmorEncumberance)
				{
					list.Add("Armor");
				}
				if (rawData.countHelmetEncumberance)
				{
					list.Add("Headwear");
				}
				if (rawData.countGloveEncumberance)
				{
					list.Add("Gloves");
				}
				if (rawData.countShoeEncumberance)
				{
					list.Add("Footwear");
				}
				string value = "Counts encumberance from " + TextTools.printListLineWithAnd(list) + ".\n\n";
				AttributesControl.Attribute.stringBuilder.AppendLine(value);
			}
			if (this.ranks != 0)
			{
				AttributesControl.Attribute.stringBuilder.AppendLine(TextTools.formateNameValuePair("Ranks", (int)this.ranks));
			}
			if (this.drain != 0)
			{
				AttributesControl.Attribute.stringBuilder.AppendLine(TextTools.formateNameValuePairPlusMinus("Drain", (int)this.drain));
			}
			int abilityBonus = this.getAbilityBonus();
			if (abilityBonus != 0)
			{
				AttributesControl.Attribute.stringBuilder.AppendLine(TextTools.formateNameValuePairPlusMinus("Abilities", abilityBonus));
			}
			int conditionBonus = this.getConditionBonus();
			if (conditionBonus != 0)
			{
				AttributesControl.Attribute.stringBuilder.AppendLine(TextTools.formateNameValuePairPlusMinus("Conditions", conditionBonus));
			}
			int armorEncumberance = this.getArmorEncumberance();
			if (armorEncumberance != 0)
			{
				AttributesControl.Attribute.stringBuilder.AppendLine(TextTools.formateNameValuePairPlusMinus("Armor Enc.", armorEncumberance));
			}
			int helmetEncumberance = this.getHelmetEncumberance();
			if (helmetEncumberance != 0)
			{
				AttributesControl.Attribute.stringBuilder.AppendLine(TextTools.formateNameValuePairPlusMinus("Helmet Enc.", helmetEncumberance));
			}
			int gloveEncumberance = this.getGloveEncumberance();
			if (gloveEncumberance != 0)
			{
				AttributesControl.Attribute.stringBuilder.AppendLine(TextTools.formateNameValuePairPlusMinus("Glove Enc.", gloveEncumberance));
			}
			int shoeEncumberance = this.getShoeEncumberance();
			if (shoeEncumberance != 0)
			{
				AttributesControl.Attribute.stringBuilder.AppendLine(TextTools.formateNameValuePairPlusMinus("Shoe Enc.", shoeEncumberance));
			}
			int outfitModifier = this.getOutfitModifier();
			if (outfitModifier != 0)
			{
				AttributesControl.Attribute.stringBuilder.AppendLine(TextTools.formateNameValuePairPlusMinus("Outfit Mod.", outfitModifier));
			}
			if (this.usePrimaryAttributeAsRoot())
			{
				AttributesControl.Attribute.stringBuilder.AppendLine(TextTools.formateNameValuePairPlusMinus(GameData.getAttributeName(this.attributeOwner.getCoreAttributeId()), this.getRootAbilitySum()));
			}
			else
			{
				foreach (AttributesControl.Attribute attribute in this.rootAbilities)
				{
					AttributesControl.Attribute.stringBuilder.AppendLine(TextTools.formateNameValuePairPlusMinus(attribute.getName(), this.getRootAbilitySum()));
				}
			}
			if (this.levelFactor != 0f)
			{
				AttributesControl.Attribute.stringBuilder.AppendLine(TextTools.formateNameValuePairPlusMinus("Level", this.getLevelBonus()));
			}
			int currentValueUncapped = this.getCurrentValueUncapped();
			int minimumLegalValue = (int)this.getMinimumLegalValue();
			int maxValue = this.getMaxValue();
			if (this.isAtMax() || currentValueUncapped < minimumLegalValue || minimumLegalValue > maxValue)
			{
				AttributesControl.Attribute.stringBuilder.AppendLine(TextTools.formateNameValuePair("TOTAL", " " + currentValueUncapped.ToString()));
			}
			else
			{
				AttributesControl.Attribute.stringBuilder.AppendLine(TextTools.formateNameValuePair("TOTAL", " " + currentValueUncapped.ToString() + " / " + maxValue.ToString()));
			}
			if (currentValueUncapped < minimumLegalValue)
			{
				AttributesControl.Attribute.stringBuilder.AppendLine(TextTools.formateNameValuePair("MIN. LEGAL", " " + minimumLegalValue.ToString()));
			}
			if (this.influences.Count != 0)
			{
				AttributesControl.Attribute.stringBuilder.Append("\n\nThis attribute influences: ");
				string text = "";
				foreach (AttributesControl.Attribute attribute2 in this.influences)
				{
					text = text + C64Color.YELLOW_TAG + attribute2.getName() + "</color>, ";
				}
				AttributesControl.Attribute.stringBuilder.Append(TextTools.removeTrailingComma(text));
			}
			return AttributesControl.Attribute.stringBuilder.ToString();
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x00061B88 File Offset: 0x0005FD88
		private bool usePrimaryAttributeAsRoot()
		{
			SKALDProjectData.Objects.AttributeData.AttributeContainer.Attribute rawData = this.getRawData();
			return rawData != null && rawData.usesPrimaryAttributeAsRoot;
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x00061BA8 File Offset: 0x0005FDA8
		private int getRootAbilitySum()
		{
			float num = 0f;
			if (this.usePrimaryAttributeAsRoot())
			{
				num = (float)this.attributeOwner.getCoreAttributeValue() * this.getRootFactor();
			}
			else
			{
				if (this.rootAbilities.Count == 0)
				{
					return 0;
				}
				foreach (AttributesControl.Attribute attribute in this.rootAbilities)
				{
					num += (float)attribute.getCurrentValue() * this.getRootFactor();
				}
				num /= (float)this.rootAbilities.Count;
			}
			int num2 = Mathf.RoundToInt(num);
			if (num2 < 0)
			{
				num2 = 0;
			}
			return num2;
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x00061C58 File Offset: 0x0005FE58
		private int getLevelBonus()
		{
			return (int)((float)this.getBaseLevel() * this.levelFactor);
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x00061C6C File Offset: 0x0005FE6C
		private int getArmorEncumberance()
		{
			SKALDProjectData.Objects.AttributeData.AttributeContainer.Attribute rawData = this.getRawData();
			if (rawData == null || !rawData.countArmorEncumberance)
			{
				return 0;
			}
			return 0 - this.attributeOwner.getArmorEncumberance();
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x00061C9C File Offset: 0x0005FE9C
		private int getShoeEncumberance()
		{
			SKALDProjectData.Objects.AttributeData.AttributeContainer.Attribute rawData = this.getRawData();
			if (rawData == null || !rawData.countShoeEncumberance)
			{
				return 0;
			}
			return 0 - this.attributeOwner.getShoeEncumberance();
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x00061CCC File Offset: 0x0005FECC
		private int getGloveEncumberance()
		{
			SKALDProjectData.Objects.AttributeData.AttributeContainer.Attribute rawData = this.getRawData();
			if (rawData == null || !rawData.countGloveEncumberance)
			{
				return 0;
			}
			return 0 - this.attributeOwner.getGloveEncumberance();
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x00061CFC File Offset: 0x0005FEFC
		private int getHelmetEncumberance()
		{
			SKALDProjectData.Objects.AttributeData.AttributeContainer.Attribute rawData = this.getRawData();
			if (rawData == null || !rawData.countHelmetEncumberance)
			{
				return 0;
			}
			return 0 - this.attributeOwner.getHelmetEncumberance();
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x00061D2C File Offset: 0x0005FF2C
		private int getOutfitModifier()
		{
			SKALDProjectData.Objects.AttributeData.AttributeContainer.Attribute rawData = this.getRawData();
			if (rawData == null || !rawData.countOutfitReaction)
			{
				return 0;
			}
			return this.attributeOwner.getClothingReactionModifier();
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x00061D58 File Offset: 0x0005FF58
		private int getStaticMaxValue()
		{
			return (int)this.ranks + this.getRootAbilitySum() + this.getLevelBonus() + this.getAbilityBonus() + this.getArmorEncumberance() + this.getShoeEncumberance() + this.getGloveEncumberance() + this.getHelmetEncumberance() + this.getOutfitModifier();
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x00061D98 File Offset: 0x0005FF98
		public int getMaxValue()
		{
			return this.getStaticMaxValue() + this.getConditionBonus();
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x00061DA7 File Offset: 0x0005FFA7
		public void resetToAbsoluteMax()
		{
			this.resetToMaxValue();
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x00061DB0 File Offset: 0x0005FFB0
		public void restoreAttributePartially(float degree)
		{
			if (degree > 1f)
			{
				degree = 1f;
			}
			if (degree < 0f)
			{
				degree = 0f;
			}
			int num = (int)((short)Mathf.RoundToInt((float)this.getMaxValue() * degree));
			if (num > this.getCurrentValue())
			{
				this.setCurrentValue(num);
			}
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x00061DFB File Offset: 0x0005FFFB
		public void resetToMaxValue()
		{
			this.drain = 0;
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x00061E04 File Offset: 0x00060004
		public int getCurrentValue()
		{
			int num = this.getMaxValue() + (int)this.drain;
			int minimumLegalValue = (int)this.getMinimumLegalValue();
			if (num < minimumLegalValue)
			{
				return minimumLegalValue;
			}
			return num;
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x00061E2D File Offset: 0x0006002D
		private int getCurrentValueUncapped()
		{
			return this.getMaxValue() + (int)this.drain;
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x00061E3C File Offset: 0x0006003C
		public void setCurrentValue(int val)
		{
			int maxValue = this.getMaxValue();
			int num = (int)((short)(val - this.getMaxValue()));
			short minimumLegalValue = this.getMinimumLegalValue();
			if (maxValue + num < (int)minimumLegalValue)
			{
				num = (int)minimumLegalValue - this.getMaxValue();
			}
			if (num > 0)
			{
				num = 0;
			}
			this.drain = (short)num;
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x00061E7C File Offset: 0x0006007C
		private short getMinimumLegalValue()
		{
			SKALDProjectData.Objects.AttributeData.AttributeContainer.Attribute rawData = this.getRawData();
			if (rawData == null)
			{
				return 0;
			}
			return (short)rawData.minValue;
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x00061E9C File Offset: 0x0006009C
		public void addToCurrent(int val)
		{
			int currentValue = this.getCurrentValue();
			this.setCurrentValue(currentValue + val);
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x00061EB9 File Offset: 0x000600B9
		public AttributesControl.Attribute.AttributeSaveData getSaveData()
		{
			return new AttributesControl.Attribute.AttributeSaveData(this.getId(), this.ranks, this.drain, this.levelFactor);
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x00061ED8 File Offset: 0x000600D8
		public void setSaveData(AttributesControl.Attribute.AttributeSaveData newSaveData)
		{
			this.ranks = newSaveData.ranks;
			this.drain = newSaveData.drain;
			this.levelFactor = newSaveData.levelFactor;
		}

		// Token: 0x0400067C RID: 1660
		private List<AttributesControl.Attribute> influences = new List<AttributesControl.Attribute>();

		// Token: 0x0400067D RID: 1661
		private List<AttributesControl.Attribute> rootAbilities = new List<AttributesControl.Attribute>();

		// Token: 0x0400067E RID: 1662
		private Character attributeOwner;

		// Token: 0x0400067F RID: 1663
		private static StringBuilder stringBuilder = new StringBuilder();

		// Token: 0x04000680 RID: 1664
		public short ranks;

		// Token: 0x04000681 RID: 1665
		public short drain;

		// Token: 0x04000682 RID: 1666
		public float levelFactor;

		// Token: 0x020002F6 RID: 758
		[Serializable]
		public struct AttributeSaveData
		{
			// Token: 0x06001C0F RID: 7183 RVA: 0x00079728 File Offset: 0x00077928
			public AttributeSaveData(string id, short ranks, short drain, float levelFactor)
			{
				this.id = id;
				this.ranks = ranks;
				this.drain = drain;
				this.levelFactor = levelFactor;
			}

			// Token: 0x04000A81 RID: 2689
			public string id;

			// Token: 0x04000A82 RID: 2690
			public short ranks;

			// Token: 0x04000A83 RID: 2691
			public short drain;

			// Token: 0x04000A84 RID: 2692
			public float levelFactor;
		}
	}
}
