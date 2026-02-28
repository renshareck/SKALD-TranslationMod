using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E8 RID: 232
public class MapCutoutLine : MapCutoutTemplate
{
	// Token: 0x06000DF8 RID: 3576 RVA: 0x000406E8 File Offset: 0x0003E8E8
	public MapCutoutLine(int xTarget, int yTarget, int radius, Character user) : base(xTarget, yTarget, radius, user)
	{
		this.setIncrements();
	}

	// Token: 0x06000DF9 RID: 3577 RVA: 0x000406FC File Offset: 0x0003E8FC
	protected override int getRadius()
	{
		int num = base.getRadius();
		if (base.getUser() != null)
		{
			num += base.getUser().getRadiusBonusLine();
		}
		return num;
	}

	// Token: 0x06000DFA RID: 3578 RVA: 0x00040728 File Offset: 0x0003E928
	protected virtual void setIncrements()
	{
		if (base.getUser() == null)
		{
			return;
		}
		int num = this.x - base.getUser().getTileX();
		int num2 = this.y - base.getUser().getTileY();
		int num3 = Mathf.Abs(num);
		int num4 = Mathf.Abs(num2);
		if (num3 > num4)
		{
			this.xIncrement = (int)Mathf.Sign((float)num);
			return;
		}
		if (num3 < num4)
		{
			this.yIncrement = (int)Mathf.Sign((float)num2);
			return;
		}
		if (num3 == num4)
		{
			this.xIncrement = (int)Mathf.Sign((float)num);
		}
	}

	// Token: 0x06000DFB RID: 3579 RVA: 0x000407AC File Offset: 0x0003E9AC
	public override List<MapTile> getCutout()
	{
		List<MapTile> list = new List<MapTile>();
		Map currentMap = MainControl.getDataControl().currentMap;
		for (int i = 1; i < this.getRadius() + 1; i++)
		{
			int x = base.getUser().getTileX() + i * this.xIncrement;
			int y = base.getUser().getTileY() + i * this.yIncrement;
			MapTile tile = currentMap.getTile(x, y);
			if (tile != null)
			{
				list.Add(tile);
			}
			if (i == this.getRadius() - 1)
			{
				base.setBaseTile(tile);
			}
		}
		return list;
	}

	// Token: 0x0400035D RID: 861
	protected int xIncrement;

	// Token: 0x0400035E RID: 862
	protected int yIncrement;
}
