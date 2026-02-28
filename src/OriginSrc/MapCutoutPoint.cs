using System;
using System.Collections.Generic;

// Token: 0x020000E2 RID: 226
public class MapCutoutPoint : MapCutoutTemplate
{
	// Token: 0x06000DEB RID: 3563 RVA: 0x000404F7 File Offset: 0x0003E6F7
	public MapCutoutPoint(int xTarget, int yTarget, Character user) : base(xTarget, yTarget, 0, user)
	{
	}

	// Token: 0x06000DEC RID: 3564 RVA: 0x00040503 File Offset: 0x0003E703
	public MapCutoutPoint(Character character) : base(character.getTileX(), character.getTileY(), 0, character)
	{
	}

	// Token: 0x06000DED RID: 3565 RVA: 0x0004051C File Offset: 0x0003E71C
	public override List<MapTile> getCutout()
	{
		Map currentMap = MainControl.getDataControl().currentMap;
		return new List<MapTile>
		{
			currentMap.getTile(this.x, this.y)
		};
	}
}
