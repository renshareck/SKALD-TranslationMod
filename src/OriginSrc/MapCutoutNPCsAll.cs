using System;
using System.Collections.Generic;

// Token: 0x020000E6 RID: 230
public class MapCutoutNPCsAll : MapCutoutNPCs
{
	// Token: 0x06000DF4 RID: 3572 RVA: 0x0004063A File Offset: 0x0003E83A
	public MapCutoutNPCsAll(Character character) : base(character)
	{
	}

	// Token: 0x06000DF5 RID: 3573 RVA: 0x00040644 File Offset: 0x0003E844
	public override List<MapTile> getCutout()
	{
		List<MapTile> list = new List<MapTile>();
		base.addTilesFromParty(list, base.getUser().getCombatAllyParty());
		base.addTilesFromParty(list, base.getUser().getOpponentParty());
		return list;
	}
}
