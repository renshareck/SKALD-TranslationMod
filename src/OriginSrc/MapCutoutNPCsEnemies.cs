using System;
using System.Collections.Generic;

// Token: 0x020000E4 RID: 228
public class MapCutoutNPCsEnemies : MapCutoutNPCs
{
	// Token: 0x06000DF0 RID: 3568 RVA: 0x000405D8 File Offset: 0x0003E7D8
	public MapCutoutNPCsEnemies(Character character) : base(character)
	{
	}

	// Token: 0x06000DF1 RID: 3569 RVA: 0x000405E4 File Offset: 0x0003E7E4
	public override List<MapTile> getCutout()
	{
		List<MapTile> list = new List<MapTile>();
		base.addTilesFromParty(list, base.getUser().getOpponentParty());
		return list;
	}
}
