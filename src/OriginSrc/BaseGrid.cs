using System;
using UnityEngine;

// Token: 0x02000114 RID: 276
public abstract class BaseGrid
{
	// Token: 0x06001187 RID: 4487 RVA: 0x0004EA38 File Offset: 0x0004CC38
	protected BaseGrid(int gridWidth, int gridHeight, int drawHeight, int rimPadding, string basePath, string backgroundPath)
	{
		this.gridWidth = gridWidth;
		this.gridHeight = gridHeight;
		this.gridDrawHeight = drawHeight;
		this.rimPadding = rimPadding;
		this.initializeOutputTexture();
		this.basePath = basePath;
	}

	// Token: 0x06001188 RID: 4488 RVA: 0x0004EA94 File Offset: 0x0004CC94
	protected BaseGrid.PointInt convertFromDecimalToIntPosition(Vector2 pos)
	{
		return new BaseGrid.PointInt(-1, -1)
		{
			x = Mathf.FloorToInt((float)this.gridWidth * pos.x),
			y = Mathf.FloorToInt((float)this.gridHeight * pos.y)
		};
	}

	// Token: 0x06001189 RID: 4489 RVA: 0x0004EADF File Offset: 0x0004CCDF
	protected int getPageOffset()
	{
		return (this.page - 1) * this.getMaxPageSize();
	}

	// Token: 0x0600118A RID: 4490 RVA: 0x0004EAF0 File Offset: 0x0004CCF0
	protected void initializeOutputTexture()
	{
		this.textureWorkspace = new TextureTools.TextureData(this.gridWidth * 16 + this.rimPadding * 2, this.gridHeight * 16 + this.rimPadding * 2);
	}

	// Token: 0x0600118B RID: 4491 RVA: 0x0004EB21 File Offset: 0x0004CD21
	protected void clearTextureWorkspace()
	{
		this.textureWorkspace.SetPixels(1, 1, this.background.width, this.background.height, this.background.colors);
	}

	// Token: 0x0600118C RID: 4492 RVA: 0x0004EB51 File Offset: 0x0004CD51
	protected int getMaxPageSize()
	{
		return this.gridDrawHeight * this.gridWidth;
	}

	// Token: 0x0600118D RID: 4493 RVA: 0x0004EB60 File Offset: 0x0004CD60
	protected int getIndexFromPosition(int x, int y)
	{
		return y * this.gridWidth + x + this.getPageOffset();
	}

	// Token: 0x0400041F RID: 1055
	protected int gridWidth;

	// Token: 0x04000420 RID: 1056
	protected int gridHeight;

	// Token: 0x04000421 RID: 1057
	protected int gridDrawHeight;

	// Token: 0x04000422 RID: 1058
	protected int page = 1;

	// Token: 0x04000423 RID: 1059
	protected int examineX;

	// Token: 0x04000424 RID: 1060
	protected int examineY;

	// Token: 0x04000425 RID: 1061
	protected int rimPadding;

	// Token: 0x04000426 RID: 1062
	protected TextureTools.TextureData textureWorkspace;

	// Token: 0x04000427 RID: 1063
	protected TextureTools.TextureData background;

	// Token: 0x04000428 RID: 1064
	protected Color32 menuSelectionColor = C64Color.Yellow;

	// Token: 0x04000429 RID: 1065
	protected Color32 menuBaseColor = C64Color.BrownLight;

	// Token: 0x0400042A RID: 1066
	protected string basePath;

	// Token: 0x02000261 RID: 609
	protected struct PointInt
	{
		// Token: 0x060019F7 RID: 6647 RVA: 0x00071632 File Offset: 0x0006F832
		public PointInt(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x0400096C RID: 2412
		public int x;

		// Token: 0x0400096D RID: 2413
		public int y;
	}
}
