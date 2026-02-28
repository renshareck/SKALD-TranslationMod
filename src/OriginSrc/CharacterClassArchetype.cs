using System;
using System.Collections.Generic;

// Token: 0x0200001B RID: 27
public class CharacterClassArchetype : CharacterFeature
{
	// Token: 0x060001A0 RID: 416 RVA: 0x000099B9 File Offset: 0x00007BB9
	public CharacterClassArchetype(SKALDProjectData.ClassContainers.ClassData rawData) : base(rawData)
	{
		this.setClassArchetype(this.getId());
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x000099D5 File Offset: 0x00007BD5
	public CharacterClassArchetype()
	{
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x000099E4 File Offset: 0x00007BE4
	private SKALDProjectData.ClassContainers.ClassData getRawData()
	{
		return GameData.getClassRawData(this.getId());
	}

	// Token: 0x060001A3 RID: 419 RVA: 0x000099F4 File Offset: 0x00007BF4
	public int getLevelUpVitality()
	{
		SKALDProjectData.ClassContainers.ClassData rawData = this.getRawData();
		if (rawData != null)
		{
			return rawData.levelUpHP;
		}
		return 0;
	}

	// Token: 0x060001A4 RID: 420 RVA: 0x00009A14 File Offset: 0x00007C14
	public override string getCoreAttributeId()
	{
		SKALDProjectData.ClassContainers.ClassData rawData = this.getRawData();
		if (rawData == null)
		{
			return "";
		}
		return rawData.mainAttribute;
	}

	// Token: 0x060001A5 RID: 421 RVA: 0x00009A38 File Offset: 0x00007C38
	public List<string> getPlayerLoadoutListId()
	{
		SKALDProjectData.ClassContainers.ClassData rawData = this.getRawData();
		if (rawData != null && rawData.playerLoadout.Count != 0)
		{
			return rawData.playerLoadout;
		}
		return new List<string>();
	}

	// Token: 0x060001A6 RID: 422 RVA: 0x00009A68 File Offset: 0x00007C68
	public int getLevelUpAttunement()
	{
		SKALDProjectData.ClassContainers.ClassData rawData = this.getRawData();
		int result = 0;
		if (rawData != null)
		{
			result = rawData.levelUpSP;
		}
		return result;
	}

	// Token: 0x060001A7 RID: 423 RVA: 0x00009A8C File Offset: 0x00007C8C
	private void setClassArchetype(string archetypeId)
	{
		foreach (object obj in Enum.GetValues(typeof(CharacterClassArchetype.ClassArchetypes)))
		{
			CharacterClassArchetype.ClassArchetypes classArchetypes = (CharacterClassArchetype.ClassArchetypes)obj;
			if (archetypeId.ToUpper().Contains(classArchetypes.ToString().ToUpper()))
			{
				this.archetype = classArchetypes;
				return;
			}
		}
		if (archetypeId != "")
		{
			MainControl.logError("Missing archetype for class id: " + this.getId() + ". Archetype id: " + archetypeId);
			return;
		}
		MainControl.logWarning("Missing archetype for class id: " + this.getId() + ". Archetype id: " + archetypeId);
	}

	// Token: 0x060001A8 RID: 424 RVA: 0x00009B54 File Offset: 0x00007D54
	public CharacterClassArchetype.ClassArchetypes getArchetypeEnum()
	{
		return this.archetype;
	}

	// Token: 0x0400002F RID: 47
	private CharacterClassArchetype.ClassArchetypes archetype = CharacterClassArchetype.ClassArchetypes.None;

	// Token: 0x020001C0 RID: 448
	public enum ClassArchetypes
	{
		// Token: 0x040006AC RID: 1708
		Warrior,
		// Token: 0x040006AD RID: 1709
		Rogue,
		// Token: 0x040006AE RID: 1710
		Cleric,
		// Token: 0x040006AF RID: 1711
		Wanderer,
		// Token: 0x040006B0 RID: 1712
		Magos,
		// Token: 0x040006B1 RID: 1713
		None
	}
}
