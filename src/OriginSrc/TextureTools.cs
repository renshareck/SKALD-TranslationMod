using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x02000192 RID: 402
public static class TextureTools
{
	// Token: 0x060014CF RID: 5327 RVA: 0x0005C639 File Offset: 0x0005A839
	public static void clearBuffer()
	{
		TextureTools.imageBuffer.clearBuffer();
		TextureTools.characterBuffer.clearBuffer();
		TextureTools.propBuffer.clearBuffer();
		TextureTools.fullImageBuffer.clearBuffer();
	}

	// Token: 0x060014D0 RID: 5328 RVA: 0x0005C664 File Offset: 0x0005A864
	public static void applyOverlay(TextureTools.TextureData target, TextureTools.TextureData overlay, int anchorX, int anchorY)
	{
		if (target == null || target.isEmpty() || overlay == null || overlay.isEmpty())
		{
			return;
		}
		if (target.width == overlay.width && target.height == overlay.height && anchorX == 0 && anchorY == 0)
		{
			overlay.applyOverlay(target);
			return;
		}
		overlay.applyOverlay(anchorX, anchorY, target);
	}

	// Token: 0x060014D1 RID: 5329 RVA: 0x0005C6BB File Offset: 0x0005A8BB
	public static void applyOverlayAddative(TextureTools.TextureData target, TextureTools.TextureData overlay, int anchorX, int anchorY)
	{
		if (target == null || target.isEmpty() || overlay == null || overlay.isEmpty())
		{
			return;
		}
		overlay.applyOverlayAddative(anchorX, anchorY, target);
	}

	// Token: 0x060014D2 RID: 5330 RVA: 0x0005C6E0 File Offset: 0x0005A8E0
	public static void applyUnderlay(TextureTools.TextureData target, TextureTools.TextureData overlay, int anchorX, int anchorY)
	{
		if (target == null || target.isEmpty() || overlay == null || overlay.isEmpty())
		{
			return;
		}
		if (target.width == overlay.width && target.height == overlay.height && anchorX == 0 && anchorY == 0)
		{
			overlay.applyUnderlay(target);
			return;
		}
		overlay.applyUnderlay(anchorX, anchorY, target);
	}

	// Token: 0x060014D3 RID: 5331 RVA: 0x0005C737 File Offset: 0x0005A937
	public static void applyOverlay(TextureTools.TextureData target, TextureTools.TextureData overlay)
	{
		TextureTools.applyOverlay(target, overlay, 0, 0);
	}

	// Token: 0x060014D4 RID: 5332 RVA: 0x0005C744 File Offset: 0x0005A944
	public static void applyOverlay(TextureTools.TextureData target, List<TextureTools.Sprite> spriteList)
	{
		if (spriteList.Count == 0)
		{
			return;
		}
		foreach (TextureTools.Sprite sprite in spriteList)
		{
			TextureTools.applyOverlay(target, sprite.texture, sprite.x, sprite.y);
		}
	}

	// Token: 0x060014D5 RID: 5333 RVA: 0x0005C7AC File Offset: 0x0005A9AC
	public static void applyNegative(TextureTools.TextureData textureData)
	{
		for (int i = 0; i < textureData.colors.Length; i++)
		{
			if (textureData.notBlackOrTransparent(i))
			{
				textureData.colors[i] = C64Color.Black;
			}
			else
			{
				textureData.colors[i] = C64Color.White;
			}
		}
	}

	// Token: 0x060014D6 RID: 5334 RVA: 0x0005C7FC File Offset: 0x0005A9FC
	public static void pullCurtain(float degree, TextureTools.TextureData textureData)
	{
		if (degree == 0f)
		{
			return;
		}
		textureData.clearCompression();
		if (degree > 1f)
		{
			degree = 1f;
		}
		int num = textureData.width / 2;
		for (int i = 0; i < textureData.colors.Length; i += textureData.width)
		{
			int num2 = 0;
			while ((float)num2 < (float)num * degree)
			{
				textureData.colors[i + num2] = C64Color.Black;
				textureData.colors[i + textureData.width - num2 - 1] = C64Color.Black;
				num2++;
			}
		}
	}

	// Token: 0x060014D7 RID: 5335 RVA: 0x0005C888 File Offset: 0x0005AA88
	public static void shakeScreen(TextureTools.TextureData textureData)
	{
		int num = 4;
		int num2 = num * 2;
		int x = Random.Range(0, num2);
		int y = Random.Range(0, num);
		Color32[] pixels = textureData.GetPixels(num, num, textureData.width - num2, textureData.height - num2);
		textureData.SetPixels(x, y, textureData.width - num2, textureData.height - num2, pixels);
	}

	// Token: 0x060014D8 RID: 5336 RVA: 0x0005C8DF File Offset: 0x0005AADF
	public static void applyOverlayColor(TextureTools.TextureData target, TextureTools.TextureData overlay, int anchorX, int anchorY, Color32 color)
	{
		overlay.overlayColor = color;
		TextureTools.applyOverlay(target, overlay, anchorX, anchorY);
		overlay.clearOverlayColor();
	}

	// Token: 0x060014D9 RID: 5337 RVA: 0x0005C8F8 File Offset: 0x0005AAF8
	public static void applyNightOverlay(TextureTools.TextureData target, TextureTools.TextureData overlay)
	{
		if (target == null || overlay == null)
		{
			return;
		}
		overlay.applyNightOverlay(target);
	}

	// Token: 0x060014DA RID: 5338 RVA: 0x0005C908 File Offset: 0x0005AB08
	public static void applyNegativeOverlayDamage(TextureTools.TextureData target, TextureTools.TextureData overlay, int anchorX, int anchorY)
	{
		if (Random.Range(0, 100) < 50)
		{
			TextureTools.applyOverlayRandomColor(target, overlay, anchorX, anchorY, new Color32[]
			{
				C64Color.Red,
				C64Color.RedLight
			});
			return;
		}
		TextureTools.applyOverlay(target, overlay, anchorX, anchorY);
	}

	// Token: 0x060014DB RID: 5339 RVA: 0x0005C947 File Offset: 0x0005AB47
	public static void applyNegativeOverlaySpell(TextureTools.TextureData target, TextureTools.TextureData overlay, int anchorX, int anchorY)
	{
		TextureTools.applyOverlayRandomColor(target, overlay, anchorX, anchorY, ParticleSystem.negativeColors);
	}

	// Token: 0x060014DC RID: 5340 RVA: 0x0005C957 File Offset: 0x0005AB57
	public static void applyPositiveOverlaySpell(TextureTools.TextureData target, TextureTools.TextureData overlay, int anchorX, int anchorY)
	{
		TextureTools.applyOverlayRandomColor(target, overlay, anchorX, anchorY, ParticleSystem.positiveColors);
	}

	// Token: 0x060014DD RID: 5341 RVA: 0x0005C968 File Offset: 0x0005AB68
	public static void applyOverlayRandomColor(TextureTools.TextureData target, TextureTools.TextureData overlay, int anchorX, int anchorY, Color32[] colorArray)
	{
		Color32 color = colorArray[Random.Range(0, colorArray.Length)];
		TextureTools.applyOverlayColor(target, overlay, anchorX, anchorY, color);
	}

	// Token: 0x060014DE RID: 5342 RVA: 0x0005C991 File Offset: 0x0005AB91
	public static void tryToGetCharacterBufferImage(string path, TextureTools.TextureData copyTarget)
	{
		TextureTools.characterBuffer.getTexture(path, copyTarget);
	}

	// Token: 0x060014DF RID: 5343 RVA: 0x0005C99F File Offset: 0x0005AB9F
	public static bool characterBufferContainsImage(string path)
	{
		return TextureTools.characterBuffer.containsTexture(path);
	}

	// Token: 0x060014E0 RID: 5344 RVA: 0x0005C9AC File Offset: 0x0005ABAC
	public static void addToCharacterBuffer(string key, TextureTools.TextureData texture)
	{
		TextureTools.characterBuffer.addTexture(key, texture);
	}

	// Token: 0x060014E1 RID: 5345 RVA: 0x0005C9BA File Offset: 0x0005ABBA
	public static bool propBufferContainsImage(string path)
	{
		return TextureTools.propBuffer.containsTexture(path);
	}

	// Token: 0x060014E2 RID: 5346 RVA: 0x0005C9C7 File Offset: 0x0005ABC7
	public static void tryToGetPropBufferImage(string path, TextureTools.TextureData copyTarget)
	{
		TextureTools.propBuffer.getTexture(path, copyTarget);
	}

	// Token: 0x060014E3 RID: 5347 RVA: 0x0005C9D5 File Offset: 0x0005ABD5
	public static void addToPropBuffer(string key, TextureTools.TextureData texture)
	{
		TextureTools.propBuffer.addTexture(key, texture);
	}

	// Token: 0x060014E4 RID: 5348 RVA: 0x0005C9E4 File Offset: 0x0005ABE4
	public static TextureTools.TextureData loadTextureDataUnbuffered(string path)
	{
		if (path == "")
		{
			return null;
		}
		TextureTools.workspace = SkaldFileIO.Load<Texture2D>(path);
		if (TextureTools.workspace == null)
		{
			MainControl.logError("Could not load texture from " + path);
			return null;
		}
		TextureTools.TextureData textureData = new TextureTools.TextureData(TextureTools.workspace);
		if (textureData == null || textureData.isEmpty())
		{
			return null;
		}
		return textureData;
	}

	// Token: 0x060014E5 RID: 5349 RVA: 0x0005CA44 File Offset: 0x0005AC44
	public static TextureTools.TextureData loadTextureData(string path)
	{
		if (path == "")
		{
			return null;
		}
		if (TextureTools.fullImageBuffer.containsTexture(path))
		{
			return TextureTools.fullImageBuffer.getTexture(path);
		}
		TextureTools.TextureData textureData = TextureTools.loadTextureDataUnbuffered(path);
		if (textureData == null || textureData.isEmpty())
		{
			return null;
		}
		TextureTools.fullImageBuffer.addTexture(path, textureData);
		return textureData;
	}

	// Token: 0x060014E6 RID: 5350 RVA: 0x0005CA9A File Offset: 0x0005AC9A
	public static void loadTextureDataAndApplyUnderlay(string path, int subImage, int x, int y, TextureTools.TextureData target)
	{
		TextureTools.loadTextureDataAndApplyUnderlay(path, subImage, x, y, target, 4, 1);
	}

	// Token: 0x060014E7 RID: 5351 RVA: 0x0005CAA9 File Offset: 0x0005ACA9
	public static void loadTextureDataAndApplyOverlay(string path, int subImage, int x, int y, TextureTools.TextureData target)
	{
		TextureTools.loadTextureDataAndApplyOverlay(path, subImage, x, y, target, 4, 1);
	}

	// Token: 0x060014E8 RID: 5352 RVA: 0x0005CAB8 File Offset: 0x0005ACB8
	public static void loadTextureDataAndApplyOverlay(string path, int subImage, int x, int y, TextureTools.TextureData target, int tileWidth, int padding)
	{
		if (path == "")
		{
			return;
		}
		if (!TextureTools.imageBuffer.containsTexture(path, subImage))
		{
			TextureTools.getSubImageTextureData(subImage, path, tileWidth, padding);
		}
		TextureTools.imageBuffer.applyOverlay(path, subImage, x, y, target);
	}

	// Token: 0x060014E9 RID: 5353 RVA: 0x0005CAF2 File Offset: 0x0005ACF2
	public static void loadTextureDataAndApplyUnderlay(string path, int subImage, int x, int y, TextureTools.TextureData target, int tileWidth, int padding)
	{
		if (path == "")
		{
			return;
		}
		if (!TextureTools.imageBuffer.containsTexture(path, subImage))
		{
			TextureTools.getSubImageTextureData(subImage, path, tileWidth, padding);
		}
		TextureTools.imageBuffer.applyUnderlay(path, subImage, x, y, target);
	}

	// Token: 0x060014EA RID: 5354 RVA: 0x0005CB2C File Offset: 0x0005AD2C
	public static void loadTextureDataAndApplyOverlayAddativeCentered(string path, int x, int y, TextureTools.TextureData target)
	{
		if (path == "")
		{
			return;
		}
		if (!TextureTools.fullImageBuffer.containsTexture(path))
		{
			TextureTools.loadTextureData(path);
		}
		TextureTools.fullImageBuffer.applyOverlayAddativeCentred(path, x, y, target);
	}

	// Token: 0x060014EB RID: 5355 RVA: 0x0005CB5E File Offset: 0x0005AD5E
	public static TextureTools.TextureData getSubImageTextureData(int subImage, string path)
	{
		return TextureTools.getSubImageTextureData(subImage, path, 4, 1);
	}

	// Token: 0x060014EC RID: 5356 RVA: 0x0005CB6C File Offset: 0x0005AD6C
	public static TextureTools.TextureData getSubImageTextureData(int subImage, string path, int tileWidth, int padding)
	{
		if (path == "")
		{
			return null;
		}
		if (TextureTools.imageBuffer.containsTexture(path, subImage))
		{
			return TextureTools.imageBuffer.getTexture(path, subImage);
		}
		TextureTools.TextureData subImage2 = TextureTools.fullImageBuffer.getSubImage(subImage, path, tileWidth, padding);
		if (subImage2 == null || subImage2.isEmpty())
		{
			return null;
		}
		TextureTools.imageBuffer.addTexture(path, subImage, subImage2);
		return subImage2;
	}

	// Token: 0x060014ED RID: 5357 RVA: 0x0005CBD0 File Offset: 0x0005ADD0
	public static TextureTools.TextureData getLetterSubImageTextureData(int letter, string path)
	{
		if (path == "")
		{
			return null;
		}
		if (TextureTools.imageBuffer.containsTexture(path, letter))
		{
			return TextureTools.imageBuffer.getTexture(path, letter);
		}
		TextureTools.TextureData subImage = TextureTools.fullImageBuffer.getSubImage(letter, path, 9, 1);
		if (subImage == null || subImage.isEmpty())
		{
			return null;
		}
		TextureTools.TextureData textureData = TextureTools.trimLetter(subImage);
		TextureTools.imageBuffer.addTexture(path, letter, textureData);
		return textureData;
	}

	// Token: 0x060014EE RID: 5358 RVA: 0x0005CC3C File Offset: 0x0005AE3C
	public static TextureTools.TextureData loadPortrait(int subImage, string path)
	{
		if (path == "")
		{
			return null;
		}
		if (TextureTools.imageBuffer.containsTexture(path, subImage))
		{
			return TextureTools.imageBuffer.getTexture(path, subImage);
		}
		TextureTools.TextureData textureData = TextureTools.loadTextureDataUnbuffered(path);
		if (textureData == null)
		{
			return null;
		}
		int num = textureData.width / 40;
		if (subImage > num)
		{
			subImage = 0;
		}
		TextureTools.TextureData subImage2 = TextureTools.getSubImage(subImage, textureData, num, 0);
		if (subImage2 == null || subImage2.isEmpty())
		{
			return null;
		}
		TextureTools.imageBuffer.addTexture(path, subImage, subImage2);
		return subImage2;
	}

	// Token: 0x060014EF RID: 5359 RVA: 0x0005CCB8 File Offset: 0x0005AEB8
	private static TextureTools.TextureData trimLetter(TextureTools.TextureData input)
	{
		int num = -1;
		int num2 = -1;
		int num3 = -1;
		for (int i = 0; i < input.width; i++)
		{
			for (int j = 0; j < input.height; j++)
			{
				if (!input.isPixelTransparent(i, j))
				{
					if (num == -1)
					{
						num = i;
					}
					if (j > num2)
					{
						num2 = j;
					}
					if (i > num3)
					{
						num3 = i;
					}
				}
			}
		}
		int width = num3 - num + 1;
		int height = num2 + 1;
		return new TextureTools.TextureData(width, height, input.GetPixels(num, 0, width, height));
	}

	// Token: 0x060014F0 RID: 5360 RVA: 0x0005CD38 File Offset: 0x0005AF38
	public static TextureTools.TextureData getSubImage(int subImage, TextureTools.TextureData source, int mosaicSize, int padding)
	{
		if (source == null || source.isEmpty())
		{
			return null;
		}
		int x = 0;
		int y = 0;
		int num = 16;
		if (source.width > 67)
		{
			num = (source.width - (mosaicSize - 1)) / mosaicSize;
		}
		else if (source.width < 16)
		{
			num = source.width;
		}
		if (subImage != 0 && source.width > 16)
		{
			int num2 = subImage / mosaicSize;
			int num3 = subImage - num2 * mosaicSize;
			y = num2 * num + num2;
			x = num3 * num + num3;
		}
		TextureTools.TextureData textureData = new TextureTools.TextureData(num, num);
		source.GetAndApplyPixels(x, y, num, num, textureData.colors);
		return textureData;
	}

	// Token: 0x060014F1 RID: 5361 RVA: 0x0005CDC8 File Offset: 0x0005AFC8
	public static TextureTools.TextureData createShortHealthBar(Character character, TextureTools.TextureData box, int length)
	{
		TextureTools.TextureData textureData = box.createCopy();
		Color32 green = C64Color.Green;
		Color32 violet = C64Color.Violet;
		TextureTools.drawBar(0, 2, textureData, green, length, (float)character.getVitality() / (float)character.getMaxVitality(), false);
		TextureTools.drawBar(0, 1, textureData, violet, length, (float)character.getWounds() / (float)character.getMaxWounds(), false);
		return textureData;
	}

	// Token: 0x060014F2 RID: 5362 RVA: 0x0005CE20 File Offset: 0x0005B020
	public static void drawStatusBars(Character character, int x, int y, TextureTools.TextureData targetTexture)
	{
		Color32 green = C64Color.Green;
		Color32 blue = C64Color.Blue;
		Color32 violet = C64Color.Violet;
		Color32 redLight = C64Color.RedLight;
		TextureTools.drawSingleStatusBar(x, y, targetTexture, green, redLight, character.getVitality(), character.getMaxVitality());
		TextureTools.drawSingleStatusBar(x, y - 2, targetTexture, violet, redLight, character.getWounds(), character.getMaxWounds());
		TextureTools.drawSingleStatusBar(x, y - 4, targetTexture, blue, redLight, character.getAttunement(), character.getMaxAttunement());
	}

	// Token: 0x060014F3 RID: 5363 RVA: 0x0005CE8C File Offset: 0x0005B08C
	public static void burnAwayPixels(TextureTools.TextureData targetTexture, float degree)
	{
		if (degree < 0f || degree > 1f)
		{
			return;
		}
		if (targetTexture == null || targetTexture.colors == null || targetTexture.colors.Length == 0)
		{
			return;
		}
		targetTexture.clearCompression();
		int maxExclusive = Mathf.RoundToInt(degree * (float)targetTexture.colors.Length);
		for (int i = Random.Range(1, maxExclusive); i < targetTexture.colors.Length; i += Random.Range(1, maxExclusive))
		{
			targetTexture.colors[i].a = 0;
		}
	}

	// Token: 0x060014F4 RID: 5364 RVA: 0x0005CF08 File Offset: 0x0005B108
	public static void drawSingleStatusBar(int x, int y, TextureTools.TextureData targetTexture, Color32 mainColor, Color32 damageColor, int currentValue, int currentMax)
	{
		int width = targetTexture.width;
		if (currentMax == 0)
		{
			TextureTools.drawBar(x, y, targetTexture, mainColor, width, 1f, true);
			return;
		}
		TextureTools.drawBar(x, y, targetTexture, mainColor, width, (float)currentValue / (float)currentMax, true);
	}

	// Token: 0x060014F5 RID: 5365 RVA: 0x0005CF44 File Offset: 0x0005B144
	private static void drawBar(int x, int y, TextureTools.TextureData targetTexture, Color32 color, int length, float fraction, bool drawEndPixel)
	{
		int num = x + 1;
		float num2 = (float)x + (float)length * fraction;
		targetTexture.clearCompression();
		if (drawEndPixel && fraction == 0f)
		{
			num2 = (float)(num + 1);
		}
		while ((float)num < num2)
		{
			targetTexture.SetPixel(num, y, color);
			num++;
		}
		if (drawEndPixel)
		{
			targetTexture.SetPixel(num - 1, y, C64Color.Yellow);
		}
	}

	// Token: 0x060014F6 RID: 5366 RVA: 0x0005CFA0 File Offset: 0x0005B1A0
	public static void drawRay(int x1, int y1, int x2, int y2, TextureTools.TextureData textureData, Color32 color)
	{
		float f = (float)(x2 - x1);
		float f2 = (float)(y2 - y1);
		float num = Mathf.Abs(f);
		float num2 = Mathf.Abs(f2);
		float num3 = num;
		int num4 = Mathf.RoundToInt(Mathf.Sign(f2));
		int num5 = Mathf.RoundToInt(Mathf.Sign(f));
		float num6 = (float)num4;
		float num7 = (float)num5;
		float num8 = (float)x1;
		float num9 = (float)y1;
		if (num > num2)
		{
			num3 = num;
			num6 = (float)num4 * (num2 / num);
		}
		else if (num < num2)
		{
			num3 = num2;
			num7 = (float)num5 * (num / num2);
		}
		textureData.clearCompression();
		int num10 = 1;
		while ((float)num10 < num3)
		{
			num8 += num7;
			num9 += num6;
			textureData.SetPixel(Mathf.RoundToInt(num8), Mathf.RoundToInt(num9), color);
			num10++;
		}
	}

	// Token: 0x04000551 RID: 1361
	public const int TILE_SIZE = 16;

	// Token: 0x04000552 RID: 1362
	public const int DEFAULT_TILE_SIZE = 4;

	// Token: 0x04000553 RID: 1363
	public const int DEFAULT_SHEET_PADDING = 1;

	// Token: 0x04000554 RID: 1364
	public const int HALF_TILE_SIZE = 8;

	// Token: 0x04000555 RID: 1365
	public static Texture2D workspace;

	// Token: 0x04000556 RID: 1366
	private static TextureTools.BufferSubImage imageBuffer = new TextureTools.BufferSubImage();

	// Token: 0x04000557 RID: 1367
	private static TextureTools.BufferFullImage characterBuffer = new TextureTools.BufferFullImage();

	// Token: 0x04000558 RID: 1368
	private static TextureTools.BufferFullImage fullImageBuffer = new TextureTools.BufferFullImage();

	// Token: 0x04000559 RID: 1369
	private static TextureTools.BufferFullImage propBuffer = new TextureTools.BufferFullImage();

	// Token: 0x0400055A RID: 1370
	public static ulong drawCount = 0UL;

	// Token: 0x020002BF RID: 703
	public class Sprite
	{
		// Token: 0x06001B59 RID: 7001 RVA: 0x00075DEC File Offset: 0x00073FEC
		public Sprite(int x, int y, TextureTools.TextureData texture)
		{
			this.x = x;
			this.y = y;
			this.texture = texture;
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x00075E0C File Offset: 0x0007400C
		public void ensureSpritePositionOnScreen()
		{
			if (this.x < 0)
			{
				this.x = 0;
			}
			else if (this.x + this.texture.width > 480)
			{
				this.x = 480 - this.texture.width;
			}
			if (this.y < 0)
			{
				this.y = 0;
				return;
			}
			if (this.y + this.texture.height > 270)
			{
				this.y = 270 - this.texture.height;
			}
		}

		// Token: 0x04000A14 RID: 2580
		public int x;

		// Token: 0x04000A15 RID: 2581
		public int y;

		// Token: 0x04000A16 RID: 2582
		public TextureTools.TextureData texture;
	}

	// Token: 0x020002C0 RID: 704
	public class TextureData
	{
		// Token: 0x06001B5B RID: 7003 RVA: 0x00075E9C File Offset: 0x0007409C
		public TextureData(int _width, int _height, Color32[] _colors)
		{
			this.width = _width;
			this.height = _height;
			this.SetPixels(_colors);
		}

		// Token: 0x06001B5C RID: 7004 RVA: 0x00075EB9 File Offset: 0x000740B9
		public TextureData(int _width, int _height)
		{
			this.ensureSize(_width, _height);
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x00075EC9 File Offset: 0x000740C9
		public TextureData() : this(16, 16)
		{
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x00075ED8 File Offset: 0x000740D8
		public TextureData(Texture2D texture)
		{
			if (texture == null)
			{
				MainControl.logError("Trying to create non-existant TextureData!");
				this.ensureSize(16, 16);
				return;
			}
			this.width = texture.width;
			this.height = texture.height;
			this.SetPixels(texture.GetPixels32());
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x00075F30 File Offset: 0x00074130
		public void compress()
		{
			if (this.isEmpty())
			{
				return;
			}
			List<int> list = new List<int>(256);
			bool flag = false;
			int num = 0;
			for (int i = 0; i < this.colors.Length; i++)
			{
				if (this.colors[i].a != 0)
				{
					if (!flag)
					{
						list.Add(i);
						list.Add(i);
						num += 2;
					}
					List<int> list2 = list;
					int index = num - 1;
					list2[index]++;
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			this.compressionOffsetArray = new int[num];
			for (int j = 0; j < num; j++)
			{
				this.compressionOffsetArray[j] = list[j];
			}
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x00075FDF File Offset: 0x000741DF
		public void clearOverlayColor()
		{
			this.overlayColor = C64Color.Transparent;
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x00075FEC File Offset: 0x000741EC
		public bool isCompressed()
		{
			return this.compressionOffsetArray != null;
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x00075FF8 File Offset: 0x000741F8
		public TextureTools.TextureData createCopy()
		{
			TextureTools.TextureData textureData = new TextureTools.TextureData(this.width, this.height);
			this.applyOverlay(textureData);
			textureData.compressionOffsetArray = this.compressionOffsetArray;
			return textureData;
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x0007602B File Offset: 0x0007422B
		public void copyToTarget(TextureTools.TextureData target)
		{
			target.ensureSize(this.width, this.height);
			target.clear();
			this.applyOverlay(target);
			target.compressionOffsetArray = this.compressionOffsetArray;
		}

		// Token: 0x06001B64 RID: 7012 RVA: 0x00076058 File Offset: 0x00074258
		public void ensureSize(int w, int h)
		{
			if (this.width == w && h == this.height)
			{
				return;
			}
			this.width = w;
			this.height = h;
			this.colors = new Color32[this.width * this.height];
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x00076093 File Offset: 0x00074293
		public void clearCompression()
		{
			this.compressionOffsetArray = null;
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x0007609C File Offset: 0x0007429C
		public void applyNightOverlay(TextureTools.TextureData target)
		{
			if (target == null)
			{
				return;
			}
			target.clearCompression();
			if (this.isCompressed())
			{
				for (int i = 0; i < this.compressionOffsetArray.Length; i += 2)
				{
					int num = this.compressionOffsetArray[i + 1];
					for (int j = this.compressionOffsetArray[i]; j < num; j++)
					{
						if (j >= target.colors.Length)
						{
							return;
						}
						if (target.notBlackOrTransparent(j))
						{
							target.colors[j] = this.colors[j];
						}
					}
				}
				return;
			}
			for (int k = 0; k < this.colors.Length; k++)
			{
				if (k >= target.colors.Length)
				{
					return;
				}
				if (!this.isPixelTransparentDangerous(k) && target.notBlackOrTransparent(k))
				{
					target.colors[k] = this.colors[k];
				}
			}
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x00076164 File Offset: 0x00074364
		public void applyOverlay(TextureTools.TextureData target)
		{
			target.clearCompression();
			if (this.isCompressed())
			{
				for (int i = 0; i < this.compressionOffsetArray.Length; i += 2)
				{
					int num = this.compressionOffsetArray[i + 1];
					for (int j = this.compressionOffsetArray[i]; j < num; j++)
					{
						if (j >= target.colors.Length)
						{
							return;
						}
						target.colors[j] = this.colors[j];
					}
				}
				return;
			}
			for (int k = 0; k < this.colors.Length; k++)
			{
				if (k >= target.colors.Length)
				{
					return;
				}
				if (!this.isPixelTransparentDangerous(k))
				{
					target.colors[k] = this.colors[k];
				}
			}
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x00076218 File Offset: 0x00074418
		public void applyUnderlay(int x, int y, TextureTools.TextureData target)
		{
			target.clearCompression();
			bool flag = this.overlayColor.a > 0;
			if (x >= target.width || y >= target.height)
			{
				return;
			}
			if (x + this.width < 0 || y + this.height < 0)
			{
				return;
			}
			int num = this.width;
			if (x + this.width >= target.width)
			{
				num -= x + this.width - target.width;
			}
			if (this.isCompressed())
			{
				int num2 = 0;
				for (int i = 0; i < this.compressionOffsetArray.Length; i += 2)
				{
					num2 += this.compressionOffsetArray[i] - num2;
					int num3 = num2 % this.width;
					int num4 = num2 / this.width;
					int num5 = (y + num4) * target.width + x + num3;
					for (int j = this.compressionOffsetArray[i]; j < this.compressionOffsetArray[i + 1]; j++)
					{
						if (num5 >= target.colors.Length)
						{
							return;
						}
						if (j >= target.colors.Length)
						{
							return;
						}
						if (num5 >= 0 && num3 < num && target.isPixelTransparent(num5))
						{
							if (flag)
							{
								target.colors[num5] = this.overlayColor;
							}
							else
							{
								target.colors[num5] = this.colors[j];
							}
						}
						num3++;
						num2++;
						if (num3 >= this.width)
						{
							num3 = 0;
							num4++;
							num5 = (y + num4) * target.width + x;
						}
						else
						{
							num5++;
						}
					}
				}
				return;
			}
			int num6 = y * target.width + x;
			int num7 = 0;
			int num8 = 0;
			for (int k = 0; k < this.colors.Length; k++)
			{
				if (num6 >= target.colors.Length)
				{
					return;
				}
				if (!this.isPixelTransparent(k) && target.isPixelTransparent(num6) && num6 >= 0 && num7 < num)
				{
					if (flag)
					{
						target.colors[num6] = this.overlayColor;
					}
					else
					{
						target.colors[num6] = this.colors[k];
					}
				}
				num7++;
				if (num7 >= this.width)
				{
					num7 = 0;
					num8++;
					num6 = (y + num8) * target.width + x;
				}
				else
				{
					num6++;
				}
			}
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x00076460 File Offset: 0x00074660
		public void applyUnderlay(TextureTools.TextureData target)
		{
			target.clearCompression();
			if (this.isCompressed())
			{
				for (int i = 0; i < this.compressionOffsetArray.Length; i += 2)
				{
					int num = this.compressionOffsetArray[i + 1];
					for (int j = this.compressionOffsetArray[i]; j < num; j++)
					{
						if (j >= target.colors.Length)
						{
							return;
						}
						if (target.isPixelTransparent(j))
						{
							target.colors[j] = this.colors[j];
						}
					}
				}
				return;
			}
			for (int k = 0; k < this.colors.Length; k++)
			{
				if (k >= target.colors.Length)
				{
					return;
				}
				if (!this.isPixelTransparent(k) && target.isPixelTransparent(k))
				{
					target.colors[k] = this.colors[k];
				}
			}
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x00076524 File Offset: 0x00074724
		public void applyOverlay(int x, int y, TextureTools.TextureData target)
		{
			target.clearCompression();
			bool flag = this.overlayColor.a > 0;
			if (x >= target.width || y >= target.height)
			{
				return;
			}
			if (x + this.width < 0 || y + this.height < 0)
			{
				return;
			}
			int num = this.width;
			if (x + this.width >= target.width)
			{
				num -= x + this.width - target.width;
			}
			if (this.isCompressed())
			{
				int num2 = 0;
				for (int i = 0; i < this.compressionOffsetArray.Length; i += 2)
				{
					num2 += this.compressionOffsetArray[i] - num2;
					int num3 = num2 % this.width;
					int num4 = num2 / this.width;
					int num5 = (y + num4) * target.width + x + num3;
					for (int j = this.compressionOffsetArray[i]; j < this.compressionOffsetArray[i + 1]; j++)
					{
						if (num5 >= target.colors.Length)
						{
							return;
						}
						if (j >= target.colors.Length)
						{
							return;
						}
						if (num5 >= 0 && num3 < num && x + num3 >= 0)
						{
							if (flag)
							{
								target.colors[num5] = this.overlayColor;
							}
							else
							{
								target.colors[num5] = this.colors[j];
							}
						}
						num3++;
						num2++;
						if (num3 >= this.width)
						{
							num3 = 0;
							num4++;
							num5 = (y + num4) * target.width + x;
						}
						else
						{
							num5++;
						}
					}
				}
				return;
			}
			int num6 = y * target.width + x;
			int num7 = 0;
			int num8 = 0;
			for (int k = 0; k < this.colors.Length; k++)
			{
				if (num6 >= target.colors.Length)
				{
					return;
				}
				if (!this.isPixelTransparent(k) && num6 >= 0 && num7 < num && x + num7 >= 0)
				{
					if (flag)
					{
						target.colors[num6] = this.overlayColor;
					}
					else
					{
						target.colors[num6] = this.colors[k];
					}
				}
				num7++;
				if (num7 >= this.width)
				{
					num7 = 0;
					num8++;
					num6 = (y + num8) * target.width + x;
				}
				else
				{
					num6++;
				}
			}
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x00076768 File Offset: 0x00074968
		public void applyOverlayAddative(int x, int y, TextureTools.TextureData target)
		{
			target.clearCompression();
			if (x >= target.width || y >= target.height)
			{
				return;
			}
			if (x + this.width < 0 || y + this.height < 0)
			{
				return;
			}
			int num = this.width;
			if (x + this.width >= target.width)
			{
				num -= x + this.width - target.width;
			}
			if (this.isCompressed())
			{
				int num2 = 0;
				for (int i = 0; i < this.compressionOffsetArray.Length; i += 2)
				{
					num2 += this.compressionOffsetArray[i] - num2;
					int num3 = num2 % this.width;
					int num4 = num2 / this.width;
					int num5 = (y + num4) * target.width + x + num3;
					for (int j = this.compressionOffsetArray[i]; j < this.compressionOffsetArray[i + 1]; j++)
					{
						if (num5 >= target.colors.Length)
						{
							return;
						}
						if (j >= target.colors.Length)
						{
							return;
						}
						if (num5 >= 0 && num3 < num && target.notBlackOrTransparent(num5))
						{
							if (target.colors[num5].r + this.colors[j].r > 255)
							{
								target.colors[num5].r = byte.MaxValue;
							}
							else
							{
								Color32[] array = target.colors;
								int num6 = num5;
								array[num6].r = array[num6].r + this.colors[j].r;
							}
							if (target.colors[num5].g + this.colors[j].g > 255)
							{
								target.colors[num5].g = byte.MaxValue;
							}
							else
							{
								Color32[] array2 = target.colors;
								int num7 = num5;
								array2[num7].g = array2[num7].g + this.colors[j].g;
							}
							if (target.colors[num5].b + this.colors[j].b > 255)
							{
								target.colors[num5].b = byte.MaxValue;
							}
							else
							{
								Color32[] array3 = target.colors;
								int num8 = num5;
								array3[num8].b = array3[num8].b + this.colors[j].b;
							}
						}
						num3++;
						num2++;
						if (num3 >= this.width)
						{
							num3 = 0;
							num4++;
							num5 = (y + num4) * target.width + x;
						}
						else
						{
							num5++;
						}
					}
				}
				return;
			}
			int num9 = y * target.width + x;
			int num10 = 0;
			int num11 = 0;
			for (int k = 0; k < this.colors.Length; k++)
			{
				if (num9 >= target.colors.Length)
				{
					return;
				}
				if (!this.isPixelTransparent(k) && num9 >= 0 && num10 < num && !target.isBlack(num9))
				{
					if (target.colors[num9].r + this.colors[k].r > 255)
					{
						target.colors[num9].r = byte.MaxValue;
					}
					else
					{
						Color32[] array4 = target.colors;
						int num12 = num9;
						array4[num12].r = array4[num12].r + this.colors[k].r;
					}
					if (target.colors[num9].g + this.colors[k].g > 255)
					{
						target.colors[num9].g = byte.MaxValue;
					}
					else
					{
						Color32[] array5 = target.colors;
						int num13 = num9;
						array5[num13].g = array5[num13].g + this.colors[k].g;
					}
					if (target.colors[num9].b + this.colors[k].b > 255)
					{
						target.colors[num9].b = byte.MaxValue;
					}
					else
					{
						Color32[] array6 = target.colors;
						int num14 = num9;
						array6[num14].b = array6[num14].b + this.colors[k].b;
					}
				}
				num10++;
				if (num10 >= this.width)
				{
					num10 = 0;
					num11++;
					num9 = (y + num11) * target.width + x;
				}
				else
				{
					num9++;
				}
			}
		}

		// Token: 0x06001B6C RID: 7020 RVA: 0x00076BE1 File Offset: 0x00074DE1
		public void clear()
		{
			this.clearCompression();
			Array.Clear(this.colors, 0, this.colors.Length);
		}

		// Token: 0x06001B6D RID: 7021 RVA: 0x00076C00 File Offset: 0x00074E00
		public void clearPartially(int height)
		{
			Color32 color = new Color32(0, 0, 0, 0);
			this.clearCompression();
			for (int i = 0; i < height * this.width; i++)
			{
				this.colors[i] = color;
			}
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x00076C40 File Offset: 0x00074E40
		public void drawWaterEdge(int height)
		{
			this.clearPartially(height);
			height++;
			for (int i = 0; i < this.width; i++)
			{
				int indexFromXY = this.getIndexFromXY(i, height);
				if (this.notBlackOrTransparent(indexFromXY))
				{
					this.colors[indexFromXY] = C64Color.BlueLight;
				}
			}
			height--;
			for (int j = 0; j < this.width; j++)
			{
				int indexFromXY2 = this.getIndexFromXY(j, height);
				if (this.notBlackOrTransparent(indexFromXY2))
				{
					this.colors[indexFromXY2] = C64Color.Blue;
				}
			}
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x00076CC8 File Offset: 0x00074EC8
		public bool notBlackOrTransparent(int index)
		{
			return index >= 0 && index < this.colors.Length && this.colors != null && this.colors[index].a != 0 && (this.colors[index].r != 0 || this.colors[index].g != 0 || this.colors[index].b > 0);
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x00076D40 File Offset: 0x00074F40
		public bool isBlack(int index)
		{
			return index < 0 || index >= this.colors.Length || this.colors == null || this.colors[index].a == 0 || (this.colors[index].r == 0 && this.colors[index].g == 0 && this.colors[index].b == 0);
		}

		// Token: 0x06001B71 RID: 7025 RVA: 0x00076DB7 File Offset: 0x00074FB7
		public bool isPixelTransparent(int index)
		{
			return index >= 0 && index < this.colors.Length && this.colors != null && this.colors[index].a == 0;
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x00076DE6 File Offset: 0x00074FE6
		public bool isPixelTransparentDangerous(int index)
		{
			return this.colors[index].a == 0;
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x00076DFC File Offset: 0x00074FFC
		public bool isPixelTransparent(int x, int y)
		{
			int indexFromXY = this.getIndexFromXY(x, y);
			return indexFromXY >= 0 && indexFromXY < this.colors.Length && this.colors != null && this.colors[indexFromXY].a == 0;
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x00076E40 File Offset: 0x00075040
		public void SetPixel(int x, int y, Color32 color)
		{
			if (x < 0 || x >= this.width || y < 0 || y >= this.height)
			{
				return;
			}
			int indexFromXY = this.getIndexFromXY(x, y);
			if (indexFromXY < this.colors.Length)
			{
				this.colors[indexFromXY] = color;
			}
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x00076E8C File Offset: 0x0007508C
		public void flipHorizontally()
		{
			if (this.colors.Length == 0 || this.colors == null)
			{
				return;
			}
			this.clearCompression();
			Color32[] array = new Color32[this.colors.Length];
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < this.height; i++)
			{
				for (int j = num + (this.width - 1); j >= num; j--)
				{
					array[num2] = this.colors[j];
					num2++;
				}
				num += this.width;
			}
			this.colors = array;
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x00076F18 File Offset: 0x00075118
		public void SetPixels(int x, int y, int _width, int _height, Color32[] _colors)
		{
			int num = 0;
			int num2 = this.getIndexFromXY(x, y);
			this.clearCompression();
			for (int i = 0; i < _height; i++)
			{
				for (int j = 0; j < _width; j++)
				{
					if (num2 > 0 && num2 < this.colors.Length && x + j >= 0 && x + j < this.width)
					{
						this.colors[num2] = _colors[num];
					}
					num2++;
					num++;
				}
				num2 += this.width - _width;
			}
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x00076F98 File Offset: 0x00075198
		public void SetPixels(Color32[] _colors)
		{
			if (this.colors == null)
			{
				this.colors = new Color32[_colors.Length];
			}
			this.clearCompression();
			for (int i = 0; i < this.colors.Length; i++)
			{
				this.colors[i] = _colors[i];
			}
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x00076FE8 File Offset: 0x000751E8
		public void cutIntoTiles(TextureTools.TextureData[,] targetMatrix)
		{
			try
			{
				for (int i = 0; i < this.getTileWidth(); i++)
				{
					for (int j = 0; j < this.getTileHeight(); j++)
					{
						if (targetMatrix[i, j] == null)
						{
							targetMatrix[i, j] = new TextureTools.TextureData();
						}
						this.GetAndApplyPixels(i * 16, j * 16, 16, 16, targetMatrix[i, j].colors);
					}
				}
			}
			catch (Exception obj)
			{
				MainControl.logError(obj);
			}
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x00077068 File Offset: 0x00075268
		public void adjustColors(Color32[] palette, List<Color32> ignoreColors = null)
		{
			Dictionary<Color32, Color32> dictionary = new Dictionary<Color32, Color32>();
			for (int i = 0; i < this.colors.Length; i++)
			{
				if (this.shouldColorBeAdjusted(this.colors[i], ignoreColors))
				{
					if (dictionary.ContainsKey(this.colors[i]))
					{
						this.colors[i] = dictionary[this.colors[i]];
					}
					else
					{
						Color32 color = this.aproximateColor(this.colors[i], palette);
						dictionary.Add(this.colors[i], color);
						this.colors[i] = color;
					}
				}
			}
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x00077111 File Offset: 0x00075311
		private bool shouldColorBeAdjusted(Color32 color, List<Color32> ignoreColors)
		{
			return color.a != 0 && (ignoreColors == null || !ignoreColors.Contains(color));
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x00077130 File Offset: 0x00075330
		public void paletteSwap(Dictionary<Color32, Color32> paletteDictionary)
		{
			for (int i = 0; i < this.colors.Length; i++)
			{
				Color32 color;
				if (paletteDictionary.TryGetValue(this.colors[i], out color))
				{
					this.colors[i] = color;
				}
			}
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x00077174 File Offset: 0x00075374
		private Color32 aproximateColor(Color32 testColor, Color32[] palette)
		{
			Color32 result = default(Color32);
			int num = 765;
			foreach (Color32 color in palette)
			{
				int num2 = 0;
				num2 += Mathf.Abs((int)(testColor.r - color.r));
				num2 += Mathf.Abs((int)(testColor.g - color.g));
				num2 += Mathf.Abs((int)(testColor.b - color.b));
				if (num2 < num)
				{
					num = num2;
					result = color;
				}
			}
			return result;
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x00077200 File Offset: 0x00075400
		public Color32[] GetPixels(int x, int y, int _width, int _height)
		{
			Color32[] array = new Color32[_width * _height];
			int num = this.getIndexFromXY(x, y);
			int num2 = 0;
			for (int i = 0; i < _height; i++)
			{
				int num3 = 0;
				while (num3 < _width && num < this.colors.Length && num2 < array.Length)
				{
					if (num3 + x < this.width && num >= 0 && num2 >= 0)
					{
						array[num2] = this.colors[num];
					}
					num2++;
					num++;
					num3++;
				}
				num += this.width - _width;
			}
			return array;
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x0007728C File Offset: 0x0007548C
		public void GetAndApplyPixels(int x, int y, int _width, int _height, Color32[] target)
		{
			int num = this.getIndexFromXY(x, y);
			int num2 = 0;
			for (int i = 0; i < _height; i++)
			{
				int num3 = 0;
				while (num3 < _width && num < this.colors.Length && num2 < target.Length)
				{
					if (num3 + x < this.width && num >= 0 && num2 >= 0)
					{
						target[num2] = this.colors[num];
					}
					num2++;
					num++;
					num3++;
				}
				num += this.width - _width;
			}
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x0007730C File Offset: 0x0007550C
		public void rotateTextureLeft()
		{
			TextureTools.TextureData textureData = new TextureTools.TextureData(this.height, this.width);
			this.clearCompression();
			for (int i = 0; i < this.width; i++)
			{
				for (int j = 0; j < this.height; j++)
				{
					textureData.SetPixel(this.height - 1 - j, i, this.GetPixel(i, j));
				}
			}
			this.colors = textureData.colors;
			this.width = textureData.width;
			this.height = textureData.height;
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x00077390 File Offset: 0x00075590
		public bool isEmpty()
		{
			return this.colors == null || this.colors.Length == 0;
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x000773A6 File Offset: 0x000755A6
		public Color32 GetPixel(int x, int y)
		{
			return this.colors[this.getIndexFromXY(x, y)];
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x000773BB File Offset: 0x000755BB
		public int getIndexFromXY(int x, int y)
		{
			return y * this.width + x;
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x000773C8 File Offset: 0x000755C8
		public void clearToColor(Color32 color)
		{
			for (int i = 0; i < this.colors.Length; i++)
			{
				this.colors[i] = color;
			}
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x000773F8 File Offset: 0x000755F8
		public void clearToBlack()
		{
			for (int i = 0; i < this.colors.Length; i++)
			{
				this.colors[i] = C64Color.Black;
			}
		}

		// Token: 0x06001B85 RID: 7045 RVA: 0x0007742C File Offset: 0x0007562C
		public void clearToColorIfNotBlack(Color32 color)
		{
			for (int i = 0; i < this.colors.Length; i++)
			{
				if (this.notBlackOrTransparent(i))
				{
					this.colors[i] = color;
				}
			}
		}

		// Token: 0x06001B86 RID: 7046 RVA: 0x00077464 File Offset: 0x00075664
		public void bakeTexture2D(Texture2D target)
		{
			if (target == null)
			{
				MainControl.logError("Trying to bake empty texture!");
				return;
			}
			try
			{
				target.SetPixels32(this.colors);
				target.Apply();
			}
			catch (Exception obj)
			{
				PopUpControl.addPopUpOK("ERROR: A Texture2D Set Pixel error occured! Terminate the game and send the developer the Player.txt file located in the local game folder.");
				MainControl.logError(obj);
			}
		}

		// Token: 0x06001B87 RID: 7047 RVA: 0x000774BC File Offset: 0x000756BC
		public void saveToFile(string path)
		{
			try
			{
				Texture2D texture2D = new Texture2D(this.width, this.height);
				this.bakeTexture2D(texture2D);
				path = Application.dataPath + "/Resources/" + path;
				File.Create(path).Close();
				File.WriteAllBytes(path, ImageConversion.EncodeToPNG(texture2D));
			}
			catch (Exception obj)
			{
				MainControl.logError(obj);
			}
		}

		// Token: 0x06001B88 RID: 7048 RVA: 0x00077524 File Offset: 0x00075724
		public int getTileWidth()
		{
			return this.convertDimensionToTileSize(this.width);
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x00077532 File Offset: 0x00075732
		public int getTileHeight()
		{
			return this.convertDimensionToTileSize(this.height);
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x00077540 File Offset: 0x00075740
		private int convertDimensionToTileSize(int dimension)
		{
			int num = dimension % 16;
			int num2 = (dimension - num) / 16;
			if (num > 0)
			{
				num2++;
			}
			return num2;
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x00077564 File Offset: 0x00075764
		public TextureTools.TextureData getOutline(Color32 color)
		{
			TextureTools.TextureData textureData = new TextureTools.TextureData(this.width + 2, this.height + 2);
			this.applyOverlay(1, 1, textureData);
			TextureTools.TextureData textureData2 = new TextureTools.TextureData(this.width + 2, this.height + 2);
			for (int i = 0; i < textureData.colors.Length; i++)
			{
				if (textureData.isPixelTransparent(i) && ((i != 0 && !textureData.isPixelTransparent(i - 1)) || (i != textureData.colors.Length - 1 && !textureData.isPixelTransparent(i + 1)) || (i < textureData.colors.Length - textureData.width && !textureData.isPixelTransparent(i + textureData.width)) || (i > textureData.width && !textureData.isPixelTransparent(i - textureData.width))))
				{
					textureData2.colors[i] = color;
				}
			}
			return textureData2;
		}

		// Token: 0x06001B8C RID: 7052 RVA: 0x00077634 File Offset: 0x00075834
		public TextureTools.TextureData getDropDownShadow(Color32 color)
		{
			TextureTools.TextureData textureData = new TextureTools.TextureData(this.width + 2, this.height + 2);
			if (GlobalSettings.getFontSettings().getOutlineShadow())
			{
				TextureTools.applyOverlayColor(textureData, this.getOutline(color), 0, 0, color);
			}
			else
			{
				TextureTools.applyOverlayColor(textureData, this, 1, 1, color);
				TextureTools.applyOverlayColor(textureData, this, 0, 0, color);
				TextureTools.applyOverlayColor(textureData, this, 1, 0, color);
				textureData.fillHoles(color);
			}
			return textureData;
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x0007769C File Offset: 0x0007589C
		public void fillHoles(Color32 color)
		{
			for (int i = 0; i < this.colors.Length; i++)
			{
				if (this.isPixelTransparent(i) && !this.isPixelTransparent(i + 1) && !this.isPixelTransparent(i - 1) && !this.isPixelTransparent(i + this.width) && !this.isPixelTransparent(i - this.width))
				{
					this.colors[i] = color;
				}
			}
		}

		// Token: 0x04000A17 RID: 2583
		public int width;

		// Token: 0x04000A18 RID: 2584
		public int height;

		// Token: 0x04000A19 RID: 2585
		public Color32[] colors;

		// Token: 0x04000A1A RID: 2586
		public int[] compressionOffsetArray;

		// Token: 0x04000A1B RID: 2587
		public Color32 overlayColor;
	}

	// Token: 0x020002C1 RID: 705
	private class BufferSubImage
	{
		// Token: 0x06001B8E RID: 7054 RVA: 0x00077708 File Offset: 0x00075908
		public void clearBuffer()
		{
			this.imageBuffer = new Dictionary<string, Dictionary<int, TextureTools.TextureData>>();
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x00077715 File Offset: 0x00075915
		public bool containsTexture(string key, int subImage)
		{
			return this.imageBuffer.ContainsKey(key) && this.imageBuffer[key].ContainsKey(subImage);
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x00077739 File Offset: 0x00075939
		public void getTexture(string key, int subImage, TextureTools.TextureData copyTarget)
		{
			if (this.containsTexture(key, subImage))
			{
				this.imageBuffer[key][subImage].copyToTarget(copyTarget);
			}
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x0007775D File Offset: 0x0007595D
		public TextureTools.TextureData getTexture(string key, int subImage)
		{
			if (this.containsTexture(key, subImage))
			{
				return this.imageBuffer[key][subImage].createCopy();
			}
			return null;
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x00077782 File Offset: 0x00075982
		public void applyOverlay(string key, int subImage, int x, int y, TextureTools.TextureData target)
		{
			if (!this.containsTexture(key, subImage))
			{
				return;
			}
			TextureTools.applyOverlay(target, this.imageBuffer[key][subImage], x, y);
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x000777AB File Offset: 0x000759AB
		public void applyUnderlay(string key, int subImage, int x, int y, TextureTools.TextureData target)
		{
			if (!this.containsTexture(key, subImage))
			{
				return;
			}
			TextureTools.applyUnderlay(target, this.imageBuffer[key][subImage], x, y);
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x000777D4 File Offset: 0x000759D4
		public void applyOverlayAddativeCentred(string key, int subImage, int x, int y, TextureTools.TextureData target)
		{
			if (!this.containsTexture(key, subImage))
			{
				return;
			}
			TextureTools.applyOverlayAddative(target, this.imageBuffer[key][subImage], x - this.imageBuffer[key][subImage].width / 2, y - this.imageBuffer[key][subImage].height / 2);
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x0007783C File Offset: 0x00075A3C
		public void addTexture(string key, int subImage, TextureTools.TextureData texture)
		{
			if (!this.containsTexture(key, subImage))
			{
				if (!this.imageBuffer.ContainsKey(key))
				{
					this.imageBuffer.Add(key, new Dictionary<int, TextureTools.TextureData>());
				}
				texture.compress();
				this.imageBuffer[key].Add(subImage, texture.createCopy());
			}
		}

		// Token: 0x04000A1C RID: 2588
		private Dictionary<string, Dictionary<int, TextureTools.TextureData>> imageBuffer = new Dictionary<string, Dictionary<int, TextureTools.TextureData>>();
	}

	// Token: 0x020002C2 RID: 706
	private class BufferFullImage
	{
		// Token: 0x06001B97 RID: 7063 RVA: 0x000778A3 File Offset: 0x00075AA3
		public void clearBuffer()
		{
			this.imageBuffer = new Dictionary<string, TextureTools.TextureData>();
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x000778B0 File Offset: 0x00075AB0
		public bool containsTexture(string key)
		{
			return this.imageBuffer.ContainsKey(key);
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x000778BE File Offset: 0x00075ABE
		public void getTexture(string key, TextureTools.TextureData copyTarget)
		{
			if (this.containsTexture(key))
			{
				this.imageBuffer[key].copyToTarget(copyTarget);
			}
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x000778DB File Offset: 0x00075ADB
		public TextureTools.TextureData getTexture(string key)
		{
			if (this.containsTexture(key))
			{
				return this.imageBuffer[key].createCopy();
			}
			return null;
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x000778F9 File Offset: 0x00075AF9
		public TextureTools.TextureData getSubImage(int subImage, string path, int tileWidth, int padding)
		{
			if (!this.containsTexture(path))
			{
				TextureTools.loadTextureData(path);
			}
			if (!this.containsTexture(path))
			{
				return null;
			}
			return TextureTools.getSubImage(subImage, this.imageBuffer[path], tileWidth, padding);
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x0007792C File Offset: 0x00075B2C
		public void applyOverlayAddativeCentred(string key, int x, int y, TextureTools.TextureData target)
		{
			if (!this.containsTexture(key))
			{
				return;
			}
			TextureTools.applyOverlayAddative(target, this.imageBuffer[key], x - this.imageBuffer[key].width / 2, y - this.imageBuffer[key].height / 2);
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x00077980 File Offset: 0x00075B80
		public void addTexture(string key, TextureTools.TextureData texture)
		{
			if (!this.containsTexture(key))
			{
				texture.compress();
				this.imageBuffer.Add(key, texture.createCopy());
			}
		}

		// Token: 0x04000A1D RID: 2589
		private Dictionary<string, TextureTools.TextureData> imageBuffer = new Dictionary<string, TextureTools.TextureData>();
	}
}
