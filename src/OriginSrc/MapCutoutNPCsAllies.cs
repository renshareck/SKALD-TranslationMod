using System;
using System.Collections.Generic;

// Token: 0x020000E5 RID: 229
public class MapCutoutNPCsAllies : MapCutoutNPCs
{
	// Token: 0x06000DF2 RID: 3570 RVA: 0x0004060A File Offset: 0x0003E80A
	public MapCutoutNPCsAllies(Character character) : base(character)
	{
	}

	// Token: 0x06000DF3 RID: 3571 RVA: 0x00040614 File Offset: 0x0003E814
	public override List<MapTile> getCutout()
	{
		List<MapTile> list = new List<MapTile>();
		base.addTilesFromParty(list, base.getUser().getCombatAllyParty());
		return list;
	}
}
