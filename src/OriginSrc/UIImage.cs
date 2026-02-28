using System;

// Token: 0x02000158 RID: 344
public class UIImage : UIElement
{
	// Token: 0x0600132B RID: 4907 RVA: 0x00054C06 File Offset: 0x00052E06
	public UIImage(int x, int y, int width, int height) : base(x, y, width, height)
	{
	}

	// Token: 0x0600132C RID: 4908 RVA: 0x00054C13 File Offset: 0x00052E13
	public UIImage()
	{
	}

	// Token: 0x0600132D RID: 4909 RVA: 0x00054C1B File Offset: 0x00052E1B
	public UIImage(string imagePath)
	{
		this.foregroundTexture = TextureTools.loadTextureData(imagePath);
		if (this.foregroundTexture != null)
		{
			base.setDimensions(0, 0, this.foregroundTexture.width, this.foregroundTexture.height);
		}
	}

	// Token: 0x0600132E RID: 4910 RVA: 0x00054C55 File Offset: 0x00052E55
	public UIImage(TextureTools.TextureData texture)
	{
		this.foregroundTexture = texture;
		if (this.foregroundTexture != null)
		{
			base.setDimensions(0, 0, this.foregroundTexture.width, this.foregroundTexture.height);
		}
	}
}
