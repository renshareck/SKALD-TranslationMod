using System;
using System.Collections.Generic;

// Token: 0x020000E9 RID: 233
public class MapCutoutCone : MapCutoutLine
{
	// Token: 0x06000DFC RID: 3580 RVA: 0x00040833 File Offset: 0x0003EA33
	public MapCutoutCone(int xTarget, int yTarget, int radius, Character user) : base(xTarget, yTarget, radius, user)
	{
	}

	// Token: 0x06000DFD RID: 3581 RVA: 0x00040840 File Offset: 0x0003EA40
	protected override void setIncrements()
	{
		if (base.getUser() == null)
		{
			return;
		}
		base.setIncrements();
		if (this.xIncrement != 0)
		{
			this.yFlare = 1;
			return;
		}
		if (this.yIncrement != 0)
		{
			this.xFlare = 1;
		}
	}

	// Token: 0x06000DFE RID: 3582 RVA: 0x00040870 File Offset: 0x0003EA70
	public override List<MapTile> getCutout()
	{
		List<MapTile> list = new List<MapTile>();
		Map currentMap = MainControl.getDataControl().currentMap;
		for (int i = 1; i < this.getRadius() + 1; i++)
		{
			int num = base.getUser().getTileX() + i * this.xIncrement;
			int num2 = base.getUser().getTileY() + i * this.yIncrement;
			MapTile tile = currentMap.getTile(num, num2);
			if (tile != null)
			{
				list.Add(tile);
			}
			if (i == this.getRadius() - 1)
			{
				base.setBaseTile(tile);
			}
			if (this.xFlare > 0)
			{
				for (int j = 0 - this.xFlare * (i - 1); j < 0; j++)
				{
					MapTile tile2 = currentMap.getTile(num + j, num2);
					if (tile2 != null && !list.Contains(tile2))
					{
						list.Add(tile2);
					}
				}
				for (int k = 0; k < this.xFlare * i; k++)
				{
					MapTile tile3 = currentMap.getTile(num + k, num2);
					if (tile3 != null && !list.Contains(tile3))
					{
						list.Add(tile3);
					}
				}
			}
			if (this.yFlare > 0)
			{
				for (int l = 0 - this.yFlare * (i - 1); l < 0; l++)
				{
					MapTile tile4 = currentMap.getTile(num, num2 + l);
					if (tile4 != null && !list.Contains(tile4))
					{
						list.Add(tile4);
					}
				}
				for (int m = 0; m < this.yFlare * i; m++)
				{
					MapTile tile5 = currentMap.getTile(num, num2 + m);
					if (tile5 != null && !list.Contains(tile5))
					{
						list.Add(tile5);
					}
				}
			}
		}
		return list;
	}

	// Token: 0x0400035F RID: 863
	private int xFlare;

	// Token: 0x04000360 RID: 864
	private int yFlare;
}
