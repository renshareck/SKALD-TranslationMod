using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000037 RID: 55
[Serializable]
public class EncounterControl : BaseEventControl
{
	// Token: 0x0600072D RID: 1837 RVA: 0x0001FA48 File Offset: 0x0001DC48
	public EncounterControl() : base(11, "Encounters")
	{
		foreach (SKALDProjectData.Objects.EncounterContainer.Encounter rawData in GameData.getEncounterList())
		{
			EncounterControl.Encounter encounter = new EncounterControl.Encounter(rawData);
			if (encounter.isEssential())
			{
				this.essentialEvents.add(encounter);
			}
			else
			{
				this.optionalEvents.add(encounter);
			}
		}
	}

	// Token: 0x0600072E RID: 1838 RVA: 0x0001FAC8 File Offset: 0x0001DCC8
	public EncounterControl.Encounter triggerEncounter(MapTile mapTile)
	{
		if (!mapTile.testRandomEncounter())
		{
			return null;
		}
		BaseEventControl.BaseEvent baseEvent = base.triggerEvent();
		if (baseEvent != null)
		{
			return baseEvent as EncounterControl.Encounter;
		}
		return null;
	}

	// Token: 0x020001EA RID: 490
	[Serializable]
	public class Encounter : BaseEventControl.BaseEvent
	{
		// Token: 0x06001752 RID: 5970 RVA: 0x000679FC File Offset: 0x00065BFC
		public Encounter(SKALDProjectData.Objects.EncounterContainer.Encounter rawData) : base(rawData)
		{
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x00067A05 File Offset: 0x00065C05
		protected override SKALDProjectData.Objects.BaseEvent getRawData()
		{
			return GameData.getEncounterRawData(this.getId());
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x00067A14 File Offset: 0x00065C14
		public bool stealthLegal()
		{
			SKALDProjectData.Objects.EncounterContainer.Encounter encounter = this.getRawData() as SKALDProjectData.Objects.EncounterContainer.Encounter;
			return encounter != null && encounter.stealth;
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x00067A38 File Offset: 0x00065C38
		public bool bribeLegal()
		{
			SKALDProjectData.Objects.EncounterContainer.Encounter encounter = this.getRawData() as SKALDProjectData.Objects.EncounterContainer.Encounter;
			return encounter != null && encounter.bribes;
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x00067A5C File Offset: 0x00065C5C
		public bool fleeLegal()
		{
			SKALDProjectData.Objects.EncounterContainer.Encounter encounter = this.getRawData() as SKALDProjectData.Objects.EncounterContainer.Encounter;
			return encounter != null && encounter.flee;
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x00067A80 File Offset: 0x00065C80
		public bool diplomacyLegal()
		{
			SKALDProjectData.Objects.EncounterContainer.Encounter encounter = this.getRawData() as SKALDProjectData.Objects.EncounterContainer.Encounter;
			return encounter != null && encounter.diplomacy;
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x00067AA4 File Offset: 0x00065CA4
		public Prop getProp()
		{
			SKALDProjectData.Objects.EncounterContainer.Encounter encounter = this.getRawData() as SKALDProjectData.Objects.EncounterContainer.Encounter;
			if (encounter != null && encounter.props.Count > 0)
			{
				if (Random.Range(0, 100) > encounter.propChance)
				{
					return null;
				}
				Prop prop = GameData.instantiateProp(encounter.props[Mathf.FloorToInt((float)Random.Range(0, encounter.props.Count))]);
				if (prop != null)
				{
					return prop;
				}
			}
			return null;
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x00067B10 File Offset: 0x00065D10
		public string getLoadoutId()
		{
			SKALDProjectData.Objects.EncounterContainer.Encounter encounter = this.getRawData() as SKALDProjectData.Objects.EncounterContainer.Encounter;
			if (encounter != null && encounter.loadouts.Count != 0)
			{
				return encounter.loadouts[Mathf.FloorToInt((float)Random.Range(0, encounter.loadouts.Count))];
			}
			return "";
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x00067B64 File Offset: 0x00065D64
		public Party getListOfOpponents(int powerLevel)
		{
			SKALDProjectData.Objects.EncounterContainer.Encounter encounter = this.getRawData() as SKALDProjectData.Objects.EncounterContainer.Encounter;
			if (encounter == null)
			{
				return null;
			}
			Party party = new Party();
			if (encounter.enemiesList.Count == 0)
			{
				return party;
			}
			powerLevel = this.calculatePowerlevel(powerLevel);
			int num = 0;
			using (List<string>.Enumerator enumerator = encounter.setEnemiesList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string id = enumerator.Current;
					Character character = GameData.instantiateCharacter(id);
					if (character != null)
					{
						character.levelScale();
						party.add(character);
					}
				}
				goto IL_BD;
			}
			IL_7A:
			Character character2 = GameData.instantiateCharacter(encounter.enemiesList[Mathf.FloorToInt((float)Random.Range(0, encounter.enemiesList.Count))]);
			if (character2 != null)
			{
				character2.levelScale();
				party.add(character2);
			}
			else
			{
				num++;
			}
			IL_BD:
			if (party.getPowerLevel() >= powerLevel || num >= 20 || party.getCount() > encounter.maxCreatureNumber)
			{
				return party;
			}
			goto IL_7A;
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x00067C5C File Offset: 0x00065E5C
		private int calculatePowerlevel(int basePowerLevel)
		{
			SKALDProjectData.Objects.EncounterContainer.Encounter encounter = this.getRawData() as SKALDProjectData.Objects.EncounterContainer.Encounter;
			if (encounter == null)
			{
				return 0;
			}
			return Mathf.FloorToInt((float)basePowerLevel * Random.Range(encounter.minFactor, encounter.maxFactor));
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x00067C98 File Offset: 0x00065E98
		public Map getMap()
		{
			SKALDProjectData.Objects.EncounterContainer.Encounter encounter = this.getRawData() as SKALDProjectData.Objects.EncounterContainer.Encounter;
			if (encounter == null)
			{
				return null;
			}
			if (encounter.maps.Count == 0)
			{
				return null;
			}
			return GameData.getMap(encounter.maps[Random.Range(0, encounter.maps.Count)], this.getId());
		}
	}
}
