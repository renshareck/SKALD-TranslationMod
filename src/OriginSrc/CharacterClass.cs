using System;
using System.Collections.Generic;

// Token: 0x0200001A RID: 26
[Serializable]
public class CharacterClass : CharacterFeature
{
	// Token: 0x06000195 RID: 405 RVA: 0x000096DC File Offset: 0x000078DC
	public CharacterClass(SKALDProjectData.ClassContainers.ClassData rawData) : base(rawData)
	{
		this.archetype = GameData.getClassArchetype(rawData.archetype);
		if (this.archetype != null)
		{
			base.updateAllowedItems(GameData.getClassRawData(rawData.archetype));
		}
	}

	// Token: 0x06000196 RID: 406 RVA: 0x0000970F File Offset: 0x0000790F
	public CharacterClass()
	{
		this.hidden = true;
	}

	// Token: 0x06000197 RID: 407 RVA: 0x0000971E File Offset: 0x0000791E
	public CharacterClassArchetype getArchetype()
	{
		return this.archetype;
	}

	// Token: 0x06000198 RID: 408 RVA: 0x00009726 File Offset: 0x00007926
	private SKALDProjectData.ClassContainers.ClassData getRawData()
	{
		return GameData.getClassRawData(this.getId());
	}

	// Token: 0x06000199 RID: 409 RVA: 0x00009734 File Offset: 0x00007934
	public override string getCoreAttributeId()
	{
		SKALDProjectData.ClassContainers.ClassData rawData = this.getRawData();
		if (rawData != null && rawData.mainAttribute != "")
		{
			return rawData.mainAttribute;
		}
		if (this.archetype != null)
		{
			return this.archetype.getCoreAttributeId();
		}
		return "";
	}

	// Token: 0x0600019A RID: 410 RVA: 0x00009780 File Offset: 0x00007980
	public List<string> getPlayerLoadoutListId()
	{
		SKALDProjectData.ClassContainers.ClassData rawData = this.getRawData();
		if (rawData != null && rawData.playerLoadout.Count != 0)
		{
			return rawData.playerLoadout;
		}
		if (this.archetype != null)
		{
			return this.archetype.getPlayerLoadoutListId();
		}
		return new List<string>();
	}

	// Token: 0x0600019B RID: 411 RVA: 0x000097C4 File Offset: 0x000079C4
	public int getLevelUpVitality()
	{
		SKALDProjectData.ClassContainers.ClassData rawData = this.getRawData();
		int num = 0;
		if (rawData != null)
		{
			num = rawData.levelUpHP;
		}
		if (this.archetype != null)
		{
			num += this.archetype.getLevelUpVitality();
		}
		return num;
	}

	// Token: 0x0600019C RID: 412 RVA: 0x000097FC File Offset: 0x000079FC
	public int getLevelUpAttunement()
	{
		SKALDProjectData.ClassContainers.ClassData rawData = this.getRawData();
		int num = 0;
		if (rawData != null)
		{
			num = rawData.levelUpSP;
		}
		if (this.archetype != null)
		{
			num += this.archetype.getLevelUpAttunement();
		}
		return num;
	}

	// Token: 0x0600019D RID: 413 RVA: 0x00009833 File Offset: 0x00007A33
	public int getLevelUpDP()
	{
		return GameData.getLevelUpDP();
	}

	// Token: 0x0600019E RID: 414 RVA: 0x0000983C File Offset: 0x00007A3C
	public override string getDescription()
	{
		string text = "";
		if (!base.isSelectable())
		{
			return text;
		}
		if (this.archetype != null && this.archetype.getArchetypeEnum() != CharacterClassArchetype.ClassArchetypes.None)
		{
			text += TextTools.formateNameValuePair("Archetype", this.archetype.getArchetypeEnum().ToString());
		}
		if (text != "")
		{
			text += "\n\n";
		}
		text += base.getDescription();
		if (text != "")
		{
			text += "\n\n";
		}
		text += this.printCoreAttributes();
		text = text + "\n\n" + TextTools.formateNameValuePair("Start. DP", base.getStartingDevelopmentPoints());
		text = text + "\n" + TextTools.formateNameValuePairPlusMinus("HP/Lvl.", this.getLevelUpVitality());
		text = text + "\n" + TextTools.formateNameValuePairPlusMinus("Magic/Lvl.", this.getLevelUpAttunement());
		text = string.Concat(new string[]
		{
			text,
			"\n\n",
			C64Color.GRAY_LIGHT_TAG,
			base.printAllowedArmor(),
			"</color>"
		});
		return string.Concat(new string[]
		{
			text,
			"\n\n",
			C64Color.GRAY_LIGHT_TAG,
			base.printAllowedWeapons(),
			"</color>"
		});
	}

	// Token: 0x0600019F RID: 415 RVA: 0x0000999C File Offset: 0x00007B9C
	public override void applyData(Character character)
	{
		if (this.archetype != null)
		{
			this.archetype.applyData(character);
		}
		base.applyData(character);
	}

	// Token: 0x0400002E RID: 46
	private CharacterClassArchetype archetype;
}
