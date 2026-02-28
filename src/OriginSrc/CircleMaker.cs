using System;
using System.Collections.Generic;

// Token: 0x0200016F RID: 367
public static class CircleMaker
{
	// Token: 0x060013F4 RID: 5108 RVA: 0x00057F94 File Offset: 0x00056194
	private static void addToCircleDictionary(string id, TextureTools.TextureData texture)
	{
		if (!CircleMaker.circleDictionary.ContainsKey(id))
		{
			CircleMaker.circleDictionary.Add(id, texture);
		}
	}

	// Token: 0x060013F5 RID: 5109 RVA: 0x00057FAF File Offset: 0x000561AF
	private static TextureTools.TextureData tryToGetCircleFromDictionary(string id)
	{
		if (!CircleMaker.circleDictionary.ContainsKey(id))
		{
			return null;
		}
		return CircleMaker.circleDictionary[id];
	}

	// Token: 0x060013F6 RID: 5110 RVA: 0x00057FCC File Offset: 0x000561CC
	public static TextureTools.TextureData makeCircle(int radius)
	{
		string id = CircleMaker.makeId(radius);
		TextureTools.TextureData textureData = CircleMaker.tryToGetCircleFromDictionary(id);
		if (textureData != null)
		{
			return textureData;
		}
		textureData = new TextureTools.TextureData(radius * 2, radius * 2);
		for (int i = 0; i <= radius; i++)
		{
			for (int j = 0; j <= radius; j++)
			{
				float linearDistance = NavigationTools.getLinearDistance(i, j, radius, radius);
				if (linearDistance < (float)radius)
				{
					if (linearDistance > (float)(radius - 4))
					{
						if (i % 2 == 0)
						{
							if (j % 2 == 0)
							{
								goto IL_95;
							}
						}
						else if (j % 2 != 0)
						{
							goto IL_95;
						}
					}
					textureData.SetPixel(i, j, C64Color.Black);
					textureData.SetPixel(radius * 2 - i, j, C64Color.Black);
					textureData.SetPixel(i, radius * 2 - j, C64Color.Black);
					textureData.SetPixel(radius * 2 - i, radius * 2 - j, C64Color.Black);
				}
				IL_95:;
			}
		}
		textureData.compress();
		CircleMaker.addToCircleDictionary(id, textureData);
		return textureData;
	}

	// Token: 0x060013F7 RID: 5111 RVA: 0x0005808F File Offset: 0x0005628F
	private static string makeId(int radius)
	{
		return "Circle_" + radius.ToString();
	}

	// Token: 0x040004FA RID: 1274
	private static Dictionary<string, TextureTools.TextureData> circleDictionary = new Dictionary<string, TextureTools.TextureData>();
}
