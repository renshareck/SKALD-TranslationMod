using System;
using System.Collections.Generic;

// Token: 0x02000014 RID: 20
public class Condition : BaseCharacterComponent
{
	// Token: 0x06000113 RID: 275 RVA: 0x00006B22 File Offset: 0x00004D22
	public Condition(SKALDProjectData.ConditionContainers.ConditionData rawData) : base(rawData)
	{
		this.setFunctionalConditions();
	}

	// Token: 0x06000114 RID: 276 RVA: 0x00006B31 File Offset: 0x00004D31
	public SKALDProjectData.ConditionContainers.ConditionData getRawData()
	{
		return GameData.getConditionData(this.getId());
	}

	// Token: 0x06000115 RID: 277 RVA: 0x00006B40 File Offset: 0x00004D40
	public bool causesBaseCondition(string baseConditionId)
	{
		SKALDProjectData.ConditionContainers.ConditionData rawData = this.getRawData();
		return rawData != null && rawData.baseConditionsCaused.Contains(baseConditionId);
	}

	// Token: 0x06000116 RID: 278 RVA: 0x00006B65 File Offset: 0x00004D65
	protected override string printComponentType()
	{
		if (this.isAdvantage())
		{
			return "Positive Condition";
		}
		return "Negative Condition";
	}

	// Token: 0x06000117 RID: 279 RVA: 0x00006B7A File Offset: 0x00004D7A
	public override string getFullDescription()
	{
		return "" + this.printComponentTypeFormated() + "\n\n" + this.getDescription();
	}

	// Token: 0x06000118 RID: 280 RVA: 0x00006B9C File Offset: 0x00004D9C
	public void setFunctionalConditions()
	{
		SKALDProjectData.ConditionContainers.ConditionData rawData = this.getRawData();
		if (rawData == null)
		{
			return;
		}
		foreach (string text in rawData.baseConditionsCaused)
		{
			if (text == "Blindness")
			{
				this.blind = true;
			}
			else if (text == "Invisible")
			{
				this.invisible = true;
			}
			else if (text == "Marked")
			{
				this.marked = true;
			}
			else if (text == "Deafness")
			{
				this.deaf = true;
			}
			else if (text == "Confusion")
			{
				this.confused = true;
			}
			else if (text == "Disease")
			{
				this.diseased = true;
			}
			else if (text == "Poison")
			{
				this.poisoned = true;
			}
			else if (text == "Stun")
			{
				this.stunned = true;
			}
			else if (text == "Panic")
			{
				this.panicked = true;
			}
			else if (text == "Paralysis")
			{
				this.paralyzed = true;
			}
			else if (text == "Insanity")
			{
				this.insane = true;
			}
			else if (text == "Immobile")
			{
				this.immobile = true;
			}
			else if (text == "Flat Footed")
			{
				this.flatFooted = true;
			}
			else if (text == "Defenceless")
			{
				this.defenceless = true;
			}
			else if (text == "Asleep")
			{
				this.asleep = true;
			}
			else if (text == "Occupied")
			{
				this.occupied = true;
			}
			else if (text == "Intoxicated")
			{
				this.intoxicated = true;
			}
			else if (text == "Charmed")
			{
				this.charmed = true;
			}
			else if (text == "Afraid")
			{
				this.afraid = true;
			}
			else if (text == "Silenced")
			{
				this.silenced = true;
			}
			else if (text == "Bleeding")
			{
				this.bleeding = true;
			}
			else if (text == "Defending")
			{
				this.defending = true;
			}
			else
			{
				MainControl.logError("Faulty functional condition " + text + " found in Condition " + this.getId());
			}
		}
	}

	// Token: 0x06000119 RID: 281 RVA: 0x00006E38 File Offset: 0x00005038
	public bool clearAtEndOfCombat(Character character)
	{
		SKALDProjectData.ConditionContainers.ConditionData rawData = this.getRawData();
		return rawData != null && (rawData.autoClearAtEndOfCombat || this.clearAtEndOfTurn(character) || this.clearAtStartOfTurn());
	}

	// Token: 0x0600011A RID: 282 RVA: 0x00006E6C File Offset: 0x0000506C
	public bool clearAtRest(Character character)
	{
		SKALDProjectData.ConditionContainers.ConditionData rawData = this.getRawData();
		return rawData != null && (rawData.restClears || this.clearAtEndOfCombat(character));
	}

	// Token: 0x0600011B RID: 283 RVA: 0x00006E98 File Offset: 0x00005098
	public bool clearAtEndOfTurn(Character character)
	{
		SKALDProjectData.ConditionContainers.ConditionData rawData = this.getRawData();
		return rawData != null && (rawData.singleTurnEndOfTurn || (rawData.chanceToSaveEachTurn && rawData.saveAttribute != "" && new SkaldTestRandomVsStatic(character, rawData.saveAttribute, rawData.saveDifficulty, 1).wasSuccess()));
	}

	// Token: 0x0600011C RID: 284 RVA: 0x00006EF0 File Offset: 0x000050F0
	public bool clearAtStartOfTurn()
	{
		SKALDProjectData.ConditionContainers.ConditionData rawData = this.getRawData();
		return rawData != null && rawData.singleTurnStartOfTurn;
	}

	// Token: 0x0600011D RID: 285 RVA: 0x00006F14 File Offset: 0x00005114
	public bool isAdvantage()
	{
		SKALDProjectData.ConditionContainers.ConditionData rawData = this.getRawData();
		return rawData != null && rawData.isAdvantage;
	}

	// Token: 0x0600011E RID: 286 RVA: 0x00006F34 File Offset: 0x00005134
	public bool testResistance(Character character)
	{
		if (this.getResistanceAttribute() == "")
		{
			return false;
		}
		int currentAttributeValue = character.getCurrentAttributeValue(this.getResistanceAttribute());
		if (currentAttributeValue == 0)
		{
			return false;
		}
		if (currentAttributeValue >= 100)
		{
			character.addPositiveBark("Immune: " + this.getName());
			CombatLog.addEntry(character.getName() + " immune to: " + this.getName(), string.Concat(new string[]
			{
				character.getName(),
				" immune to the condition: ",
				this.getName(),
				". Characters resistance is ",
				currentAttributeValue.ToString(),
				"%."
			}));
			return true;
		}
		DicePoolPercentile dicePoolPercentile = new DicePoolPercentile("Resistance Roll");
		dicePoolPercentile.roll();
		if (dicePoolPercentile.getResult() < currentAttributeValue)
		{
			character.addPositiveBark("Resisted: " + this.getName());
			CombatLog.addEntry(character.getName() + " resisted: " + this.getName(), string.Concat(new string[]
			{
				character.getName(),
				" resisted the condition: ",
				this.getName(),
				". Characters resistance is ",
				currentAttributeValue.ToString(),
				"%."
			}));
			return true;
		}
		return false;
	}

	// Token: 0x0600011F RID: 287 RVA: 0x0000706C File Offset: 0x0000526C
	public string getResistanceAttribute()
	{
		SKALDProjectData.ConditionContainers.ConditionData rawData = this.getRawData();
		if (rawData == null)
		{
			return "";
		}
		return rawData.resistanceAttribute;
	}

	// Token: 0x06000120 RID: 288 RVA: 0x00007090 File Offset: 0x00005290
	public virtual void applyCondition(Character character)
	{
		SKALDProjectData.ConditionContainers.ConditionData rawData = this.getRawData();
		if (rawData == null)
		{
			return;
		}
		if (MainControl.debugFunctions)
		{
			MainControl.log(string.Concat(new string[]
			{
				"Added condition ",
				C64Color.WHITE_TAG,
				this.getName(),
				"</color> to ",
				character.getName()
			}));
		}
		if (this.isAdvantage())
		{
			character.addPositiveBark(this.getName().ToUpper());
		}
		else
		{
			character.addNegativeBark(this.getName().ToUpper());
		}
		CombatLog.addEntry(character.getNameColored() + ": " + this.getName(), character.getNameColored() + " gained the condition: " + this.getName());
		if (rawData.targetParticleEffects.Count != 0)
		{
			foreach (string effect in rawData.targetParticleEffects)
			{
				character.getVisualEffects().setCombatEffectFromString(effect, character.getTargetOpponent());
			}
			return;
		}
		if (rawData.isAdvantage)
		{
			character.getVisualEffects().setCombatEffectFromString("PositiveFlash", character.getTargetOpponent());
			return;
		}
		character.getVisualEffects().setCombatEffectFromString("NegativeFlash", character.getTargetOpponent());
	}

	// Token: 0x06000121 RID: 289 RVA: 0x000071DC File Offset: 0x000053DC
	protected override List<string> getAffectedAttributes()
	{
		List<string> list = new List<string>();
		SKALDProjectData.ConditionContainers.ConditionData rawData = this.getRawData();
		if (rawData == null)
		{
			return null;
		}
		foreach (string item in rawData.primaryAffectedAttributes)
		{
			list.Add(item);
		}
		foreach (string item2 in rawData.secondaryAffectedAttributes)
		{
			list.Add(item2);
		}
		return list;
	}

	// Token: 0x06000122 RID: 290 RVA: 0x00007288 File Offset: 0x00005488
	public int getLightRadius()
	{
		SKALDProjectData.ConditionContainers.ConditionData rawData = this.getRawData();
		if (rawData == null)
		{
			return 0;
		}
		return rawData.illuminationRange;
	}

	// Token: 0x06000123 RID: 291 RVA: 0x000072A8 File Offset: 0x000054A8
	public float getLightDegree()
	{
		SKALDProjectData.ConditionContainers.ConditionData rawData = this.getRawData();
		if (rawData == null)
		{
			return 0f;
		}
		return rawData.illuminationDegree;
	}

	// Token: 0x06000124 RID: 292 RVA: 0x000072CC File Offset: 0x000054CC
	public string getLightImage()
	{
		SKALDProjectData.ConditionContainers.ConditionData rawData = this.getRawData();
		if (rawData == null)
		{
			return "";
		}
		return rawData.illuminationImage;
	}

	// Token: 0x06000125 RID: 293 RVA: 0x000072F0 File Offset: 0x000054F0
	public override int getModifierToAttribute(string attributeId)
	{
		if (this.modifierBuffer == null)
		{
			this.modifierBuffer = new Dictionary<string, int>();
		}
		if (this.modifierBuffer.ContainsKey(attributeId))
		{
			return this.modifierBuffer[attributeId];
		}
		int num = 0;
		SKALDProjectData.ConditionContainers.ConditionData rawData = this.getRawData();
		if (rawData == null)
		{
			return 0;
		}
		using (List<string>.Enumerator enumerator = rawData.primaryAffectedAttributes.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current == attributeId)
				{
					num += rawData.primaryMagnitude;
					break;
				}
			}
		}
		using (List<string>.Enumerator enumerator = rawData.secondaryAffectedAttributes.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current == attributeId)
				{
					num += rawData.secondaryMagnitude;
					break;
				}
			}
		}
		this.modifierBuffer.Add(attributeId, num);
		return num;
	}

	// Token: 0x06000126 RID: 294 RVA: 0x000073E8 File Offset: 0x000055E8
	public string getModelPath()
	{
		SKALDProjectData.ConditionContainers.ConditionData rawData = this.getRawData();
		if (rawData == null)
		{
			return "";
		}
		if (rawData.modelPath != "")
		{
			return rawData.modelPath;
		}
		if (this.isAdvantage())
		{
			return "GenericPositive";
		}
		return "GenericNegative";
	}

	// Token: 0x06000127 RID: 295 RVA: 0x00007434 File Offset: 0x00005634
	public override string getDescription()
	{
		string text = "";
		SKALDProjectData.ConditionContainers.ConditionData rawData = this.getRawData();
		if (rawData == null)
		{
			return "";
		}
		if (rawData.baseConditionsCaused.Count != 0)
		{
			if (text != "")
			{
				text += "\n";
			}
			text = text + C64Color.GRAY_LIGHT_TAG + "Adds Status Tag:</color> ";
			text = text + TextTools.printListLine(rawData.baseConditionsCaused, "#") + "\n\n";
		}
		if (rawData.primaryAffectedAttributes.Count != 0)
		{
			if (text != "")
			{
				text += "\n";
			}
			foreach (string id in rawData.primaryAffectedAttributes)
			{
				text = string.Concat(new string[]
				{
					text,
					TextTools.formatePlusMinus(rawData.primaryMagnitude),
					" ",
					GameData.getAttributeName(id),
					"\n"
				});
			}
		}
		if (rawData.secondaryAffectedAttributes.Count != 0)
		{
			if (text != "")
			{
				text += "\n";
			}
			foreach (string id2 in rawData.secondaryAffectedAttributes)
			{
				text = string.Concat(new string[]
				{
					text,
					TextTools.formatePlusMinus(rawData.secondaryMagnitude),
					" ",
					GameData.getAttributeName(id2),
					"\n"
				});
			}
		}
		if (text != "")
		{
			text += "\n\n";
		}
		if (rawData.singleTurnEndOfTurn)
		{
			text += "Lasts until end of turn.\n\n";
		}
		else if (rawData.singleTurnStartOfTurn)
		{
			text += "Lasts until start of next turn.\n\n";
		}
		else if (rawData.chanceToSaveEachTurn)
		{
			if (rawData.saveAttribute != "")
			{
				text = string.Concat(new string[]
				{
					text,
					"Roll a successful ",
					GameData.getAttributeName(rawData.saveAttribute),
					" Saving Throw vs DC ",
					rawData.saveDifficulty.ToString(),
					" at end of each turn to remove.\n\n"
				});
			}
			else
			{
				MainControl.logError("Missing save attribute for " + this.getId());
			}
		}
		else if (rawData.autoClearAtEndOfCombat)
		{
			text += "Lasts until end of combat.\n\n";
		}
		else if (rawData.restClears)
		{
			text += "Lasts until next rest.\n\n";
		}
		else if (rawData.restClears)
		{
			text += "Lasts indefinitely.\n\n";
		}
		if (rawData.magicClears)
		{
			text += "Can be removed by magic.\n\n";
		}
		if (base.getDescription() != "" && text != "")
		{
			text += base.getDescription();
		}
		return text;
	}

	// Token: 0x06000128 RID: 296 RVA: 0x00007728 File Offset: 0x00005928
	public TextureTools.TextureData getIcon()
	{
		if (this.icon != null)
		{
			return this.icon;
		}
		string modelPath = this.getModelPath();
		if (modelPath == "")
		{
			return null;
		}
		this.icon = TextureTools.loadTextureData("Images/GUIIcons/PortraitIcons/" + modelPath);
		return this.icon;
	}

	// Token: 0x06000129 RID: 297 RVA: 0x00007776 File Offset: 0x00005976
	public bool isBlind()
	{
		return this.blind;
	}

	// Token: 0x0600012A RID: 298 RVA: 0x0000777E File Offset: 0x0000597E
	public bool isDeaf()
	{
		return this.deaf;
	}

	// Token: 0x0600012B RID: 299 RVA: 0x00007786 File Offset: 0x00005986
	public bool isBleeding()
	{
		return this.bleeding;
	}

	// Token: 0x0600012C RID: 300 RVA: 0x0000778E File Offset: 0x0000598E
	public bool isDefending()
	{
		return this.defending;
	}

	// Token: 0x0600012D RID: 301 RVA: 0x00007796 File Offset: 0x00005996
	public bool isConfused()
	{
		return this.confused;
	}

	// Token: 0x0600012E RID: 302 RVA: 0x0000779E File Offset: 0x0000599E
	public bool isDiseased()
	{
		return this.diseased;
	}

	// Token: 0x0600012F RID: 303 RVA: 0x000077A6 File Offset: 0x000059A6
	public bool isPoisoned()
	{
		return this.poisoned;
	}

	// Token: 0x06000130 RID: 304 RVA: 0x000077AE File Offset: 0x000059AE
	public bool isMarked()
	{
		return this.marked;
	}

	// Token: 0x06000131 RID: 305 RVA: 0x000077B6 File Offset: 0x000059B6
	public bool isStunned()
	{
		return this.stunned;
	}

	// Token: 0x06000132 RID: 306 RVA: 0x000077BE File Offset: 0x000059BE
	public bool isInvisible()
	{
		return this.invisible;
	}

	// Token: 0x06000133 RID: 307 RVA: 0x000077C6 File Offset: 0x000059C6
	public bool isPanicked()
	{
		return this.panicked;
	}

	// Token: 0x06000134 RID: 308 RVA: 0x000077CE File Offset: 0x000059CE
	public bool isParalyzed()
	{
		return this.paralyzed;
	}

	// Token: 0x06000135 RID: 309 RVA: 0x000077D6 File Offset: 0x000059D6
	public bool isInsane()
	{
		return this.insane;
	}

	// Token: 0x06000136 RID: 310 RVA: 0x000077DE File Offset: 0x000059DE
	public bool isImmobilized()
	{
		return this.isStunned() || this.isAsleep() || this.isParalyzed() || this.immobile;
	}

	// Token: 0x06000137 RID: 311 RVA: 0x00007800 File Offset: 0x00005A00
	public bool isFlatFooted()
	{
		return this.isBlind() || this.isImmobilized() || this.isStunned() || this.isDefenceless() || this.isPanicked() || this.flatFooted;
	}

	// Token: 0x06000138 RID: 312 RVA: 0x00007832 File Offset: 0x00005A32
	public bool isDefenceless()
	{
		return this.isAsleep() || this.isParalyzed() || this.defenceless;
	}

	// Token: 0x06000139 RID: 313 RVA: 0x0000784C File Offset: 0x00005A4C
	public bool isOccupied()
	{
		return this.isBlind() || this.isPanicked() || this.isInsane() || this.isImmobilized() || this.isStunned() || this.isAsleep() || this.isParalyzed() || this.isConfused() || this.occupied;
	}

	// Token: 0x0600013A RID: 314 RVA: 0x000078A1 File Offset: 0x00005AA1
	public bool isAsleep()
	{
		return this.asleep;
	}

	// Token: 0x0600013B RID: 315 RVA: 0x000078A9 File Offset: 0x00005AA9
	public bool isIntoxicated()
	{
		return this.intoxicated;
	}

	// Token: 0x0600013C RID: 316 RVA: 0x000078B1 File Offset: 0x00005AB1
	public bool isCharmed()
	{
		return this.charmed;
	}

	// Token: 0x0600013D RID: 317 RVA: 0x000078B9 File Offset: 0x00005AB9
	public bool isAfraid()
	{
		return this.afraid;
	}

	// Token: 0x0600013E RID: 318 RVA: 0x000078C1 File Offset: 0x00005AC1
	public bool isSilenced()
	{
		return this.silenced;
	}

	// Token: 0x04000014 RID: 20
	public static List<string> baseConditions = new List<string>
	{
		"Invisible",
		"Marked",
		"Blindness",
		"Deafness",
		"Bleeding",
		"Confusion",
		"Disease",
		"Poison",
		"Stun",
		"Panic",
		"Berserk",
		"Paralysis",
		"Insanity",
		"Immobile",
		"Flat Footed",
		"Defenceless",
		"Asleep",
		"Occupied",
		"Intoxicated",
		"Charmed",
		"Afraid",
		"Silenced",
		"Defending"
	};

	// Token: 0x04000015 RID: 21
	private TextureTools.TextureData icon;

	// Token: 0x04000016 RID: 22
	private Dictionary<string, int> modifierBuffer;

	// Token: 0x04000017 RID: 23
	private bool blind;

	// Token: 0x04000018 RID: 24
	private bool deaf;

	// Token: 0x04000019 RID: 25
	private bool bleeding;

	// Token: 0x0400001A RID: 26
	private bool confused;

	// Token: 0x0400001B RID: 27
	private bool diseased;

	// Token: 0x0400001C RID: 28
	private bool poisoned;

	// Token: 0x0400001D RID: 29
	private bool stunned;

	// Token: 0x0400001E RID: 30
	private bool panicked;

	// Token: 0x0400001F RID: 31
	private bool paralyzed;

	// Token: 0x04000020 RID: 32
	private bool insane;

	// Token: 0x04000021 RID: 33
	private bool immobile;

	// Token: 0x04000022 RID: 34
	private bool flatFooted;

	// Token: 0x04000023 RID: 35
	private bool defenceless;

	// Token: 0x04000024 RID: 36
	private bool asleep;

	// Token: 0x04000025 RID: 37
	private bool occupied;

	// Token: 0x04000026 RID: 38
	private bool intoxicated;

	// Token: 0x04000027 RID: 39
	private bool charmed;

	// Token: 0x04000028 RID: 40
	private bool afraid;

	// Token: 0x04000029 RID: 41
	private bool silenced;

	// Token: 0x0400002A RID: 42
	private bool defending;

	// Token: 0x0400002B RID: 43
	private bool marked;

	// Token: 0x0400002C RID: 44
	private bool invisible;
}
