using System;
using System.Collections.Generic;

// Token: 0x020000DF RID: 223
public abstract class MapCutoutSquare : MapCutoutTemplate
{
	// Token: 0x06000DE4 RID: 3556 RVA: 0x0004034A File Offset: 0x0003E54A
	public MapCutoutSquare(int x, int y, int radius, Character owner) : base(x, y, radius, owner)
	{
	}

	// Token: 0x06000DE5 RID: 3557 RVA: 0x00040358 File Offset: 0x0003E558
	public override List<MapTile> getCutout()
	{
		List<MapTile> list = new List<MapTile>();
		Map currentMap = MainControl.getDataControl().currentMap;
		for (int i = this.x - this.getRadius(); i < this.x + this.getRadius() + 1; i++)
		{
			for (int j = this.y - this.getRadius(); j < this.y + this.getRadius() + 1; j++)
			{
				MapTile tile = currentMap.getTile(i, j);
				if (tile != null)
				{
					list.Add(tile);
				}
			}
		}
		return list;
	}
}
