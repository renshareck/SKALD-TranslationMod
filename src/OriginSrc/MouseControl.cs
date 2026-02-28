using System;
using UnityEngine;

// Token: 0x0200008E RID: 142
public static class MouseControl
{
	// Token: 0x06000A4F RID: 2639 RVA: 0x00031574 File Offset: 0x0002F774
	public static void drawMouse(TextureTools.TextureData texture)
	{
		TextureTools.TextureData textureData;
		if (SkaldIO.getMouseHeldDown(0) || SkaldIO.getMouseHeldDown(1))
		{
			textureData = MouseControl.mousePointer2;
		}
		else
		{
			textureData = MouseControl.mousePointer;
		}
		Vector2 globalMousePosition = SkaldIO.getGlobalMousePosition();
		int num = (int)globalMousePosition.x - 1;
		int num2 = (int)globalMousePosition.y - textureData.height + 2;
		if (num < 0)
		{
			num = 0;
		}
		else if (num + textureData.width > 480)
		{
			num = 480 - textureData.width;
		}
		if (num2 + textureData.height > 270)
		{
			num2 = 270 - textureData.height;
		}
		TextureTools.applyOverlay(texture, textureData, num, num2);
	}

	// Token: 0x040002B1 RID: 689
	private static TextureTools.TextureData mousePointer = TextureTools.loadTextureData("Images/GUIIcons/MouseNew");

	// Token: 0x040002B2 RID: 690
	private static TextureTools.TextureData mousePointer2 = TextureTools.loadTextureData("Images/GUIIcons/MouseNew2");
}
