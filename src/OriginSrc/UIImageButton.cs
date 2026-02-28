using System;

// Token: 0x02000159 RID: 345
public class UIImageButton : UIImage
{
	// Token: 0x0600132F RID: 4911 RVA: 0x00054C8C File Offset: 0x00052E8C
	public UIImageButton(string path)
	{
		this.imagePath += path;
		this.hoverImagePath = this.imagePath + "Hover";
		this.pressedImagePath = this.imagePath + "Pressed";
		this.backgroundTexture = TextureTools.loadTextureData(this.imagePath);
		base.setDimensions(0, 0, this.backgroundTexture.width + 1, this.backgroundTexture.height);
		this.backgroundTextureHover = TextureTools.loadTextureData(this.hoverImagePath);
		this.backgroundTexturePressed = TextureTools.loadTextureData(this.pressedImagePath);
	}

	// Token: 0x040004AE RID: 1198
	private string imagePath = "Images/GUIIcons/";

	// Token: 0x040004AF RID: 1199
	private string hoverImagePath = "";

	// Token: 0x040004B0 RID: 1200
	private string pressedImagePath = "";
}
