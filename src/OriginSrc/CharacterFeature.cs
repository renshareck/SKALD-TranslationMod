using System;
using System.Collections.Generic;

// Token: 0x0200001C RID: 28
[Serializable]
public abstract class CharacterFeature : SkaldInstanceObject
{
	// Token: 0x060001A9 RID: 425 RVA: 0x00009B5C File Offset: 0x00007D5C
	protected CharacterFeature(SKALDProjectData.CharacterFeature rawData) : base(rawData)
	{
		if (rawData != null)
		{
			this.selectable = rawData.selectable;
			this.hidden = rawData.hidden;
			this.feats = rawData.featsList;
			this.spells = rawData.spellsList;
			this.loadoutIdList = rawData.loadoutList;
			this.apperanceIdList = rawData.apperancePackList;
			this.abilityList = rawData.abilityList;
			this.deluxe = rawData.deluxeEdition;
			this.updateAllowedItems(rawData);
			this.bonusDP = rawData.bonusDP;
		}
	}

	// Token: 0x060001AA RID: 426 RVA: 0x00009C48 File Offset: 0x00007E48
	protected void updateAllowedItems(SKALDProjectData.CharacterFeature rawData)
	{
		foreach (string item in rawData.allowedArmors)
		{
			if (!this.allowedArmorWeights.Contains(item))
			{
				this.allowedArmorWeights.Add(item);
			}
		}
		foreach (string item2 in rawData.allowedWeaponWeights)
		{
			if (!this.allowedWeaponWeights.Contains(item2))
			{
				this.allowedWeaponWeights.Add(item2);
			}
		}
		foreach (string item3 in rawData.allowedWeaponTypes)
		{
			if (!this.allowedWeaponTypes.Contains(item3))
			{
				this.allowedWeaponTypes.Add(item3);
			}
		}
		if (rawData.allowedArmors.Contains("Light"))
		{
			this.allowLightArmor = true;
		}
		if (rawData.allowedArmors.Contains("Medium"))
		{
			this.allowMediumArmor = true;
		}
		if (rawData.allowedArmors.Contains("Heavy"))
		{
			this.allowHeavyArmor = true;
		}
		if (rawData.allowedWeaponWeights.Contains("Light"))
		{
			this.allowLightWeapons = true;
		}
		if (rawData.allowedWeaponWeights.Contains("Medium"))
		{
			this.allowMediumWeapons = true;
		}
		if (rawData.allowedWeaponWeights.Contains("Heavy"))
		{
			this.allowHeavyWeapons = true;
		}
		if (rawData.allowedWeaponTypes.Contains("Blade"))
		{
			this.allowBlades = true;
		}
		if (rawData.allowedWeaponTypes.Contains("Club"))
		{
			this.allowClubs = true;
		}
		if (rawData.allowedWeaponTypes.Contains("Bow"))
		{
			this.allowBows = true;
		}
		if (rawData.allowedWeaponTypes.Contains("Axe"))
		{
			this.allowAxes = true;
		}
	}

	// Token: 0x060001AB RID: 427 RVA: 0x00009E54 File Offset: 0x00008054
	protected CharacterFeature()
	{
		this.setId("Unknown");
		this.setName("Unknown");
	}

	// Token: 0x060001AC RID: 428 RVA: 0x00009EDE File Offset: 0x000080DE
	public bool isHidden()
	{
		return this.hidden;
	}

	// Token: 0x060001AD RID: 429 RVA: 0x00009EE6 File Offset: 0x000080E6
	public bool isDeluxeEdition()
	{
		return this.deluxe;
	}

	// Token: 0x060001AE RID: 430 RVA: 0x00009EEE File Offset: 0x000080EE
	public bool isSelectable()
	{
		return this.selectable;
	}

	// Token: 0x060001AF RID: 431 RVA: 0x00009EF8 File Offset: 0x000080F8
	public SkaldActionResult isWeaponAllowed(ItemWeapon weapon)
	{
		if (weapon.isHeavy() && !this.allowHeavyWeapons)
		{
			return new SkaldActionResult(true, false, "Heavy Weapons can't be equipped by this Class.", true);
		}
		if (weapon.isMedium() && !this.allowMediumWeapons)
		{
			return new SkaldActionResult(true, false, "Medium Weapons can't be equipped by this Class.", true);
		}
		if (weapon.isLight() && !this.allowLightWeapons)
		{
			return new SkaldActionResult(true, false, "Light Weapons can't be equipped by this Class.", true);
		}
		if (weapon.isSword() && !this.allowBlades)
		{
			return new SkaldActionResult(true, false, "Blades can't be equipped by this Class.", true);
		}
		if (weapon.isAxe() && !this.allowAxes)
		{
			return new SkaldActionResult(true, false, "Axes Weapons can't be equipped by this Class.", true);
		}
		if (weapon.isBow() && !this.allowBows)
		{
			return new SkaldActionResult(true, false, "Bows can't be equipped by this Class.", true);
		}
		if (weapon.isClub() && !this.allowClubs)
		{
			return new SkaldActionResult(true, false, "Clubs Weapons can't be equipped by this Class.", true);
		}
		return new SkaldActionResult(true, true, "", true);
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x00009FE4 File Offset: 0x000081E4
	public SkaldActionResult isArmorAllowed(ItemArmorBase armor)
	{
		if (armor.isHeavy() && !this.allowHeavyArmor)
		{
			return new SkaldActionResult(true, false, "Heavy Armor can't be equipped by this Class.", true);
		}
		if (armor.isMedium() && !this.allowMediumArmor)
		{
			return new SkaldActionResult(true, false, "Medium Armor can't be equipped by this Class.", true);
		}
		if (armor.isLight() && !this.allowLightArmor)
		{
			return new SkaldActionResult(true, false, "Light Armor can't be equipped by this Class.", true);
		}
		return new SkaldActionResult(true, true, "", true);
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x0000A058 File Offset: 0x00008258
	protected string printAllowedArmor()
	{
		if (this.allowedArmorWeights.Count == 0)
		{
			return "May not wear armor.";
		}
		if (this.allowedArmorWeights.Count == 3)
		{
			return "May use any armor.";
		}
		return "May wear " + TextTools.printListLineWithAnd(this.allowedArmorWeights) + " armor.";
	}

	// Token: 0x060001B2 RID: 434 RVA: 0x0000A0A6 File Offset: 0x000082A6
	protected int getStartingDevelopmentPoints()
	{
		return this.bonusDP + GameData.getStartingDP();
	}

	// Token: 0x060001B3 RID: 435 RVA: 0x0000A0B4 File Offset: 0x000082B4
	protected string printAllowedWeapons()
	{
		if (this.allowedWeaponTypes.Count == 0 || this.allowedWeaponWeights.Count == 0)
		{
			return "May not use weapons.";
		}
		if (this.allowedWeaponTypes.Count == 0 && this.allowedWeaponTypes.Count <= 4)
		{
			return "May use all weapons.";
		}
		string text;
		if (this.allowedWeaponWeights.Count == 3)
		{
			text = "all";
		}
		else
		{
			text = TextTools.printListLineWithAnd(this.allowedWeaponWeights);
		}
		return string.Concat(new string[]
		{
			"May use ",
			text,
			" weapons of type ",
			TextTools.printListLineWithAnd(this.allowedWeaponTypes),
			"."
		});
	}

	// Token: 0x060001B4 RID: 436 RVA: 0x0000A160 File Offset: 0x00008360
	public override string getListName()
	{
		string text = this.getName();
		if (!this.isSelectable())
		{
			text += " [Locked]";
		}
		return text;
	}

	// Token: 0x060001B5 RID: 437 RVA: 0x0000A189 File Offset: 0x00008389
	public override string getDescription()
	{
		if (!this.isSelectable())
		{
			return "[This feature cannot be selected for now.]\n\n";
		}
		return base.getDescription();
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x0000A1A0 File Offset: 0x000083A0
	public virtual void applyData(Character character)
	{
		if (this.loadoutIdList != null)
		{
			foreach (string loadoutId in this.loadoutIdList)
			{
				character.applyLoadout(loadoutId);
			}
		}
		if (this.apperanceIdList != null)
		{
			foreach (string appreancePackId in this.apperanceIdList)
			{
				character.applyApperancePack(appreancePackId);
			}
		}
		character.addFeat(this.feats);
		character.addSpell(this.spells);
		character.addAbilities(this.abilityList);
		character.addDevelopmentPoints(this.bonusDP);
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x0000A278 File Offset: 0x00008478
	public virtual string getCoreAttributeId()
	{
		return "";
	}

	// Token: 0x060001B8 RID: 440 RVA: 0x0000A27F File Offset: 0x0000847F
	public virtual string printCoreAttributes()
	{
		return "The Main Attribute for this Class is " + GameData.getAttributeName(this.getCoreAttributeId()) + ".";
	}

	// Token: 0x060001B9 RID: 441 RVA: 0x0000A29C File Offset: 0x0000849C
	public virtual string printFeats()
	{
		string text = "";
		foreach (string id in this.feats)
		{
			text = text + "- " + GameData.getFeatName(id) + "\n";
		}
		return text;
	}

	// Token: 0x04000030 RID: 48
	protected List<string> feats = new List<string>();

	// Token: 0x04000031 RID: 49
	protected List<string> spells = new List<string>();

	// Token: 0x04000032 RID: 50
	protected bool hidden;

	// Token: 0x04000033 RID: 51
	protected bool selectable = true;

	// Token: 0x04000034 RID: 52
	protected List<string> loadoutIdList = new List<string>();

	// Token: 0x04000035 RID: 53
	protected List<string> apperanceIdList = new List<string>();

	// Token: 0x04000036 RID: 54
	protected List<string> abilityList = new List<string>();

	// Token: 0x04000037 RID: 55
	private List<string> allowedArmorWeights = new List<string>();

	// Token: 0x04000038 RID: 56
	private List<string> allowedWeaponWeights = new List<string>();

	// Token: 0x04000039 RID: 57
	private List<string> allowedWeaponTypes = new List<string>();

	// Token: 0x0400003A RID: 58
	private bool allowLightArmor;

	// Token: 0x0400003B RID: 59
	private bool allowMediumArmor;

	// Token: 0x0400003C RID: 60
	private bool allowHeavyArmor;

	// Token: 0x0400003D RID: 61
	private bool allowLightWeapons;

	// Token: 0x0400003E RID: 62
	private bool allowMediumWeapons;

	// Token: 0x0400003F RID: 63
	private bool allowHeavyWeapons;

	// Token: 0x04000040 RID: 64
	private bool allowBlades;

	// Token: 0x04000041 RID: 65
	private bool allowClubs;

	// Token: 0x04000042 RID: 66
	private bool allowBows;

	// Token: 0x04000043 RID: 67
	private bool allowAxes;

	// Token: 0x04000044 RID: 68
	private bool deluxe;

	// Token: 0x04000045 RID: 69
	private int bonusDP;
}
