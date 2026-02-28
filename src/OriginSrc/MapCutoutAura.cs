using System;
using System.Collections.Generic;

// Token: 0x020000E1 RID: 225
public class MapCutoutAura : MapCutoutSquare
{
	// Token: 0x06000DE8 RID: 3560 RVA: 0x00040413 File Offset: 0x0003E613
	public MapCutoutAura(int radius, Character user, bool includeCaster) : base(user.getTileX(), user.getTileY(), radius, user)
	{
		this.includeCaster = includeCaster;
	}

	// Token: 0x06000DE9 RID: 3561 RVA: 0x00040430 File Offset: 0x0003E630
	protected override int getRadius()
	{
		int num = base.getRadius();
		if (base.getUser() != null)
		{
			num += base.getUser().getRadiusBonusAura();
		}
		return num;
	}

	// Token: 0x06000DEA RID: 3562 RVA: 0x0004045C File Offset: 0x0003E65C
	public override List<MapTile> getCutout()
	{
		List<MapTile> list = new List<MapTile>();
		Map currentMap = MainControl.getDataControl().currentMap;
		for (int i = this.x - this.getRadius(); i < this.x + this.getRadius() + 1; i++)
		{
			for (int j = this.y - this.getRadius(); j < this.y + this.getRadius() + 1; j++)
			{
				if (this.includeCaster || i != this.x || j != this.y)
				{
					MapTile tile = currentMap.getTile(i, j);
					if (tile != null)
					{
						list.Add(tile);
					}
				}
			}
		}
		return list;
	}

	// Token: 0x0400035C RID: 860
	private bool includeCaster;
}
