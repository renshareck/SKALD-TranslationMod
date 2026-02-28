using System;
using UnityEngine;

// Token: 0x02000194 RID: 404
public class ImageOutlineDrawer
{
	// Token: 0x06001501 RID: 5377 RVA: 0x0005D188 File Offset: 0x0005B388
	public TextureTools.Sprite getOutline(int x, int y, Color32 color, TextureTools.TextureData input)
	{
		int num = input.width + 2;
		int num2 = input.height + 2;
		if (this.workspace == null)
		{
			this.workspace = new TextureTools.TextureData(num, num2);
		}
		else
		{
			this.workspace.ensureSize(num, num2);
			this.workspace.clear();
		}
		if (this.result == null)
		{
			this.result = new TextureTools.TextureData(num, num2);
		}
		else
		{
			this.result.ensureSize(num, num2);
			this.result.clear();
		}
		if (this.sprite == null)
		{
			this.sprite = new TextureTools.Sprite(x, y, this.result);
		}
		else
		{
			this.sprite.x = x;
			this.sprite.y = y;
		}
		input.applyOverlay(1, 1, this.workspace);
		for (int i = 0; i < this.workspace.colors.Length; i++)
		{
			if (this.workspace.isPixelTransparent(i) && ((i != 0 && !this.workspace.isPixelTransparent(i - 1)) || (i != this.workspace.colors.Length - 1 && !this.workspace.isPixelTransparent(i + 1)) || (i < this.workspace.colors.Length - this.workspace.width && !this.workspace.isPixelTransparent(i + this.workspace.width)) || (i > this.workspace.width && !this.workspace.isPixelTransparent(i - this.workspace.width))))
			{
				this.result.colors[i] = color;
			}
		}
		return this.sprite;
	}

	// Token: 0x0400055F RID: 1375
	private TextureTools.TextureData workspace;

	// Token: 0x04000560 RID: 1376
	private TextureTools.TextureData result;

	// Token: 0x04000561 RID: 1377
	private TextureTools.Sprite sprite;
}
