using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000B8 RID: 184
public class Damage
{
	// Token: 0x06000B77 RID: 2935 RVA: 0x00036540 File Offset: 0x00034740
	public Damage(int amount)
	{
		this.amount = amount;
		this.addVerboseResultString("Incoming damage: " + amount.ToString());
	}

	// Token: 0x06000B78 RID: 2936 RVA: 0x00036599 File Offset: 0x00034799
	public Damage(int amount, List<string> damageType) : this(amount)
	{
		this.addDamageType(damageType);
	}

	// Token: 0x06000B79 RID: 2937 RVA: 0x000365AC File Offset: 0x000347AC
	public Damage(int amount, List<string> damageType, Character target, Character source)
	{
		this.addDamageType(damageType);
		this.amount = amount;
		if (target != null && source != null)
		{
			this.addVerboseResultString(string.Concat(new string[]
			{
				target.getNameColored(),
				" takes takes damage from ",
				source.getNameColored(),
				". Incoming amount: ",
				amount.ToString()
			}));
			return;
		}
		if (target != null)
		{
			this.addVerboseResultString(target.getNameColored() + " has incoming Damage: " + amount.ToString());
			return;
		}
		this.addVerboseResultString("Incoming damage: " + amount.ToString());
	}

	// Token: 0x06000B7A RID: 2938 RVA: 0x00036674 File Offset: 0x00034874
	private static AttributesControl.CoreAttributes getResistance(string damageType)
	{
		if (damageType == "")
		{
			return AttributesControl.CoreAttributes.Null;
		}
		if (Damage.resistanceDictionary == null)
		{
			Damage.resistanceDictionary = new Dictionary<string, AttributesControl.CoreAttributes>();
			Damage.resistanceDictionary.Add("Electric", AttributesControl.CoreAttributes.ATT_ResEnergyElectric);
			Damage.resistanceDictionary.Add("Kinetic", AttributesControl.CoreAttributes.ATT_ResEnergyKinetic);
			Damage.resistanceDictionary.Add("Sublime", AttributesControl.CoreAttributes.ATT_ResEnergySublime);
			Damage.resistanceDictionary.Add("Corruption", AttributesControl.CoreAttributes.ATT_ResEnergyCorruption);
			Damage.resistanceDictionary.Add("Acid", AttributesControl.CoreAttributes.ATT_ResEnergyAcid);
			Damage.resistanceDictionary.Add("Cold", AttributesControl.CoreAttributes.ATT_ResEnergyCold);
			Damage.resistanceDictionary.Add("Magical", AttributesControl.CoreAttributes.ATT_ResAnyMagic);
			Damage.resistanceDictionary.Add("Fire", AttributesControl.CoreAttributes.ATT_ResEnergyFire);
			Damage.resistanceDictionary.Add("Energy", AttributesControl.CoreAttributes.ATT_ResAnyEnergy);
			Damage.resistanceDictionary.Add("Psychic", AttributesControl.CoreAttributes.ATT_ResEnergyPsychic);
			Damage.resistanceDictionary.Add("Blunt", AttributesControl.CoreAttributes.ATT_ResPhysicalBlunt);
			Damage.resistanceDictionary.Add("Slashing", AttributesControl.CoreAttributes.ATT_ResPhysicalSlashing);
			Damage.resistanceDictionary.Add("Piercing", AttributesControl.CoreAttributes.ATT_ResPhysicalPiercing);
		}
		if (Damage.resistanceDictionary.ContainsKey(damageType))
		{
			return Damage.resistanceDictionary[damageType];
		}
		MainControl.logError("Did not find damagetype " + damageType + " in resistance dictionary.");
		return AttributesControl.CoreAttributes.Null;
	}

	// Token: 0x06000B7B RID: 2939 RVA: 0x000367B2 File Offset: 0x000349B2
	public bool isSlashing()
	{
		return this.damageTypes.Contains("Slashing");
	}

	// Token: 0x06000B7C RID: 2940 RVA: 0x000367C4 File Offset: 0x000349C4
	private void addDamageType(List<string> damageType)
	{
		if (damageType == null)
		{
			return;
		}
		foreach (string damageType2 in damageType)
		{
			this.addDamageType(damageType2);
		}
	}

	// Token: 0x06000B7D RID: 2941 RVA: 0x00036818 File Offset: 0x00034A18
	private void addDamageType(string damageType)
	{
		if (damageType == "Magical")
		{
			this.magical = true;
			return;
		}
		if (damageType == "Energy")
		{
			this.physical = false;
			return;
		}
		if (!this.damageTypes.Contains(damageType))
		{
			this.damageTypes.Add(damageType);
		}
	}

	// Token: 0x06000B7E RID: 2942 RVA: 0x00036869 File Offset: 0x00034A69
	private void addResultString(string s)
	{
		if (!this.resultStrings.Contains(s))
		{
			this.resultStrings.Add(s);
		}
	}

	// Token: 0x06000B7F RID: 2943 RVA: 0x00036885 File Offset: 0x00034A85
	public void addVerboseResultString(string s)
	{
		this.verboseResultString = this.verboseResultString + s + "\n\n";
	}

	// Token: 0x06000B80 RID: 2944 RVA: 0x000368A0 File Offset: 0x00034AA0
	public string getVerboseResultString()
	{
		return string.Concat(new string[]
		{
			this.verboseResultString,
			"Damage Taken: ",
			this.getAmount().ToString(),
			" ",
			this.printDamageTypes()
		});
	}

	// Token: 0x06000B81 RID: 2945 RVA: 0x000368EC File Offset: 0x00034AEC
	public List<string> getResultString()
	{
		List<string> list = new List<string>();
		foreach (string item in this.resultStrings)
		{
			list.Add(item);
		}
		if (this.amount <= 0)
		{
			return list;
		}
		string text = this.amount.ToString();
		text = text + " " + this.printDamageTypes();
		text += " Damage";
		list.Add(text);
		return list;
	}

	// Token: 0x06000B82 RID: 2946 RVA: 0x00036984 File Offset: 0x00034B84
	private string printDamageTypes()
	{
		string text = "";
		foreach (string str in this.damageTypes)
		{
			text = text + str + ", ";
		}
		text = TextTools.removeTrailingComma(text);
		return text;
	}

	// Token: 0x06000B83 RID: 2947 RVA: 0x000369EC File Offset: 0x00034BEC
	public void modulateDamageByResistance(Character character)
	{
		if (this.physical)
		{
			this.modulateDamage(character.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_ResAnyPhysical), "Physical");
		}
		else
		{
			this.modulateDamage(character.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_ResAnyEnergy), "Energy");
		}
		if (this.magical)
		{
			this.modulateDamage(character.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_ResAnyMagic), "Magical");
		}
		else
		{
			this.modulateDamage(character.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_ResAnyNonMagical), "Non-Magical");
		}
		if (this.damageTypes == null)
		{
			return;
		}
		foreach (string damageType in this.damageTypes)
		{
			AttributesControl.CoreAttributes resistance = Damage.getResistance(damageType);
			if (resistance != AttributesControl.CoreAttributes.Null)
			{
				this.modulateDamage(character.getCurrentAttributeValue(resistance), damageType);
			}
		}
	}

	// Token: 0x06000B84 RID: 2948 RVA: 0x00036ABC File Offset: 0x00034CBC
	private void modulateDamage(int resistance, string damageType)
	{
		if (this.amount == 0 || resistance == 0)
		{
			return;
		}
		int num = this.amount;
		if (resistance >= 100)
		{
			this.amount = 0;
			this.addResultString("Immune: " + damageType);
			this.addVerboseResultString("Immune to " + damageType + " Damage. Damage reduced to 0.");
			return;
		}
		if (resistance >= 0 && resistance < 100)
		{
			this.amount = Mathf.CeilToInt((float)this.amount * ((float)resistance / 100f));
			this.addResultString("Resistant: " + damageType);
			this.addVerboseResultString(string.Concat(new string[]
			{
				resistance.ToString(),
				"% Resistant to ",
				damageType,
				" Damage. Damage reduced by ",
				(num - this.amount).ToString()
			}));
			return;
		}
		if (resistance < 0)
		{
			resistance = Mathf.Abs(resistance);
			this.amount = Mathf.CeilToInt((float)this.amount * (1f + (float)resistance / 100f));
			this.addResultString("Vulnerable: " + damageType);
			this.addVerboseResultString("Vulnerable to " + damageType + " Damage. Damage increased by " + (this.amount - num).ToString());
		}
	}

	// Token: 0x06000B85 RID: 2949 RVA: 0x00036BEC File Offset: 0x00034DEC
	public void add(int amount, string description)
	{
		this.addVerboseResultString(description + amount.ToString());
		this.amount += amount;
	}

	// Token: 0x06000B86 RID: 2950 RVA: 0x00036C10 File Offset: 0x00034E10
	public void applySoak(int soak)
	{
		if (soak <= 0)
		{
			return;
		}
		this.soaked = true;
		this.addResultString("Soak: -" + soak.ToString());
		this.addVerboseResultString("Armor Soak reduced damage by " + soak.ToString() + ".");
		this.amount -= soak;
		this.bound();
	}

	// Token: 0x06000B87 RID: 2951 RVA: 0x00036C70 File Offset: 0x00034E70
	public bool hasSoaked()
	{
		return this.soaked;
	}

	// Token: 0x06000B88 RID: 2952 RVA: 0x00036C78 File Offset: 0x00034E78
	public void multiply(float scale, string message)
	{
		this.amount = Mathf.RoundToInt((float)this.amount * scale);
		this.addVerboseResultString(message + scale.ToString() + " to " + this.amount.ToString());
	}

	// Token: 0x06000B89 RID: 2953 RVA: 0x00036CB1 File Offset: 0x00034EB1
	public void bound()
	{
		if (this.amount < 1)
		{
			this.amount = 1;
		}
	}

	// Token: 0x06000B8A RID: 2954 RVA: 0x00036CC3 File Offset: 0x00034EC3
	public int getAmount()
	{
		if (this.amount < 0)
		{
			return 0;
		}
		return this.amount;
	}

	// Token: 0x06000B8B RID: 2955 RVA: 0x00036CD6 File Offset: 0x00034ED6
	public override string ToString()
	{
		return this.amount.ToString();
	}

	// Token: 0x0400030C RID: 780
	private int amount;

	// Token: 0x0400030D RID: 781
	private bool soaked;

	// Token: 0x0400030E RID: 782
	private List<string> damageTypes = new List<string>();

	// Token: 0x0400030F RID: 783
	private List<string> resultStrings = new List<string>();

	// Token: 0x04000310 RID: 784
	private string verboseResultString = "";

	// Token: 0x04000311 RID: 785
	private static Dictionary<string, AttributesControl.CoreAttributes> resistanceDictionary;

	// Token: 0x04000312 RID: 786
	private bool magical;

	// Token: 0x04000313 RID: 787
	private bool physical = true;
}
