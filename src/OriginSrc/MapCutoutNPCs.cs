using System;
using System.Collections.Generic;

// Token: 0x020000E3 RID: 227
public abstract class MapCutoutNPCs : MapCutoutTemplate
{
	// Token: 0x06000DEE RID: 3566 RVA: 0x00040551 File Offset: 0x0003E751
	public MapCutoutNPCs(Character character) : base(character.getTileX(), character.getTileY(), 0, character)
	{
	}

	// Token: 0x06000DEF RID: 3567 RVA: 0x00040568 File Offset: 0x0003E768
	protected void addTilesFromParty(List<MapTile> resultList, Party party)
	{
		if (party == null || resultList == null)
		{
			return;
		}
		foreach (SkaldBaseObject skaldBaseObject in party.getObjectList())
		{
			MapTile mapTile = ((Character)skaldBaseObject).getMapTile();
			if (mapTile != null && !resultList.Contains(mapTile))
			{
				resultList.Add(mapTile);
			}
		}
	}
}
