using System;
using System.Collections.Generic;

// Token: 0x020000E7 RID: 231
public class MapCutoutTouch : MapCutoutLine
{
	// Token: 0x06000DF6 RID: 3574 RVA: 0x0004067C File Offset: 0x0003E87C
	public MapCutoutTouch(int xTarget, int yTarget, Character user) : base(xTarget, yTarget, 1, user)
	{
	}

	// Token: 0x06000DF7 RID: 3575 RVA: 0x00040688 File Offset: 0x0003E888
	public override List<MapTile> getCutout()
	{
		if (this.x == base.getUser().getTileX() && this.y == base.getUser().getTileY())
		{
			return new List<MapTile>
			{
				MainControl.getDataControl().currentMap.getTile(this.x, this.y)
			};
		}
		return base.getCutout();
	}
}
