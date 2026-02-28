using System;
using System.Collections.Generic;

// Token: 0x020000DE RID: 222
public abstract class MapCutoutTemplate
{
	// Token: 0x06000DDE RID: 3550 RVA: 0x000402D0 File Offset: 0x0003E4D0
	protected MapCutoutTemplate(int x, int y, int radius, Character user)
	{
		this.x = x;
		this.y = y;
		this.radius = radius;
		if (this.radius < 1)
		{
			this.radius = 1;
		}
		this.user = user;
		Map currentMap = MainControl.getDataControl().currentMap;
		this.baseTile = currentMap.getTile(x, y);
	}

	// Token: 0x06000DDF RID: 3551 RVA: 0x00040329 File Offset: 0x0003E529
	protected virtual int getRadius()
	{
		return this.radius;
	}

	// Token: 0x06000DE0 RID: 3552 RVA: 0x00040331 File Offset: 0x0003E531
	protected Character getUser()
	{
		return this.user;
	}

	// Token: 0x06000DE1 RID: 3553 RVA: 0x00040339 File Offset: 0x0003E539
	protected void setBaseTile(MapTile tile)
	{
		this.baseTile = tile;
	}

	// Token: 0x06000DE2 RID: 3554 RVA: 0x00040342 File Offset: 0x0003E542
	public MapTile getBaseTile()
	{
		return this.baseTile;
	}

	// Token: 0x06000DE3 RID: 3555
	public abstract List<MapTile> getCutout();

	// Token: 0x04000357 RID: 855
	protected int x;

	// Token: 0x04000358 RID: 856
	protected int y;

	// Token: 0x04000359 RID: 857
	private int radius;

	// Token: 0x0400035A RID: 858
	private Character user;

	// Token: 0x0400035B RID: 859
	private MapTile baseTile;
}
