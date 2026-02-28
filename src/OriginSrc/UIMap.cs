using System;

// Token: 0x02000161 RID: 353
public class UIMap : UICanvasVertical
{
	// Token: 0x0600136E RID: 4974 RVA: 0x00055AC5 File Offset: 0x00053CC5
	public void updateTexture(int x, int y, TextureTools.TextureData mapTexture)
	{
		base.setDimensions(x, y + mapTexture.height, mapTexture.width, mapTexture.height);
		this.backgroundTexture = mapTexture;
	}
}
