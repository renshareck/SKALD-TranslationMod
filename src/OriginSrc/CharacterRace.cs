using System;

// Token: 0x0200001E RID: 30
[Serializable]
public class CharacterRace : CharacterFeature
{
	// Token: 0x060001C3 RID: 451 RVA: 0x0000A3AC File Offset: 0x000085AC
	public CharacterRace(SKALDProjectData.RaceContainers.RaceData rawData) : base(rawData)
	{
		this.startingWounds = rawData.startingWounds;
	}

	// Token: 0x060001C4 RID: 452 RVA: 0x0000A3C1 File Offset: 0x000085C1
	public CharacterRace()
	{
	}

	// Token: 0x060001C5 RID: 453 RVA: 0x0000A3C9 File Offset: 0x000085C9
	public override void applyData(Character character)
	{
		base.applyData(character);
		character.addToAttributeRank(AttributesControl.CoreAttributes.ATT_Wounds, this.startingWounds);
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x0000A3E0 File Offset: 0x000085E0
	public string getDeathSound()
	{
		SKALDProjectData.RaceContainers.RaceData raceRawData = GameData.getRaceRawData(this.getId());
		if (raceRawData != null)
		{
			return raceRawData.deathSound;
		}
		return "";
	}

	// Token: 0x0400004A RID: 74
	private int startingWounds;
}
