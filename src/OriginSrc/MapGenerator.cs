using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000EA RID: 234
public class MapGenerator
{
	// Token: 0x06000DFF RID: 3583 RVA: 0x00040A04 File Offset: 0x0003EC04
	public MapGenerator(int width, int height)
	{
		ColorUtility.TryParseHtmlString("#CB6CEA", out this.floorColor);
		ColorUtility.TryParseHtmlString("#AAAAAA", out this.roofColor);
		ColorUtility.TryParseHtmlString("#4E4A36", out this.wallColor);
		ColorUtility.TryParseHtmlString("#7C70DA", out this.stairDownColor);
		ColorUtility.TryParseHtmlString("#ABABAB", out this.corridorColor);
		ColorUtility.TryParseHtmlString("#555555", out this.voidColor);
		ColorUtility.TryParseHtmlString("#DDE20E", out this.treasureColor);
		ColorUtility.TryParseHtmlString("#7C7000", out this.stairUpColor);
		this.mapTexture = new TextureTools.TextureData(width, height);
		this.mapTexture.clearToColor(this.voidColor);
		new MapGenerator.Room(width / 2, height / 2, this.mapTexture, this.floorColor);
		this.createCorridors(this.mapTexture, width / 2, height / 2, 10, 10, 50, this.corridorColor);
		this.cellularGrowth(this.mapTexture, 1, 100, this.voidColor, this.floorColor, this.roofColor);
		this.cellularGrowth(this.mapTexture, 1, 100, this.voidColor, this.roofColor, this.roofColor);
		this.insertWalls(this.mapTexture, this.roofColor, this.floorColor, this.wallColor);
		this.insertWalls(this.mapTexture, this.floorColor, this.wallColor, this.roofColor);
		this.cellularGrowth(this.mapTexture, 1, 100, this.voidColor, this.corridorColor, this.roofColor);
		this.insertWalls(this.mapTexture, this.roofColor, this.corridorColor, this.wallColor);
		this.insertWalls(this.mapTexture, this.corridorColor, this.wallColor, this.roofColor);
		this.seedRandom(this.mapTexture, this.floorColor, this.stairDownColor);
		for (int i = 0; i < 10; i++)
		{
			this.seedRandom(this.mapTexture, this.floorColor, this.treasureColor);
		}
		this.mapTexture.SetPixel(width / 2, height / 2, this.stairUpColor);
	}

	// Token: 0x06000E00 RID: 3584 RVA: 0x00040C34 File Offset: 0x0003EE34
	private TextureTools.TextureData createCorridors(TextureTools.TextureData map, int x, int y, int minLength, int maxLength, int nodes, Color corridorValue)
	{
		int num = x;
		int num2 = y;
		int num3 = num;
		int num4 = num2;
		for (int i = 0; i < nodes; i++)
		{
			do
			{
				int num5 = Random.Range(1, 5);
				if (num5 == 1)
				{
					num3 = num + Random.Range(minLength, maxLength);
					num4 = num2;
				}
				else if (num5 == 2)
				{
					num3 = num - Random.Range(minLength, maxLength);
					num4 = num2;
				}
				else if (num5 == 3)
				{
					num3 = num;
					num4 = num2 + Random.Range(minLength, maxLength);
				}
				else if (num5 == 4)
				{
					num3 = num;
					num4 = num2 - Random.Range(minLength, maxLength);
				}
			}
			while (!this.isPointLegal(map, num3, num4));
			while (num != num3 || num2 != num4)
			{
				map.SetPixel(num, num2, corridorValue);
				if (num < num3)
				{
					num++;
				}
				if (num > num3)
				{
					num--;
				}
				if (num2 < num4)
				{
					num2++;
				}
				if (num2 > num4)
				{
					num2--;
				}
			}
			if (Random.Range(1, 100) < 40)
			{
				this.insertBlock(map, num, num2, Random.Range(3, 7), Random.Range(3, 7), this.floorColor);
			}
		}
		return map;
	}

	// Token: 0x06000E01 RID: 3585 RVA: 0x00040D2C File Offset: 0x0003EF2C
	private void seedRandom(TextureTools.TextureData map, Color testValue, Color insertValue)
	{
		for (int i = 0; i < 100000; i++)
		{
			int x = Random.Range(0, map.width);
			int y = Random.Range(0, map.height);
			if (map.GetPixel(x, y) == testValue)
			{
				map.SetPixel(x, y, insertValue);
				return;
			}
		}
	}

	// Token: 0x06000E02 RID: 3586 RVA: 0x00040D88 File Offset: 0x0003EF88
	private int countNeighbours(TextureTools.TextureData map, int x, int y, Color value)
	{
		int num = 0;
		for (int i = x - 1; i < x + 2; i++)
		{
			for (int j = y - 1; j < y + 2; j++)
			{
				if (this.isPointLegal(map, i, j) && i != 0 && j != 0 && map.GetPixel(i, j) == value)
				{
					num++;
				}
			}
		}
		return num;
	}

	// Token: 0x06000E03 RID: 3587 RVA: 0x00040DE4 File Offset: 0x0003EFE4
	private TextureTools.TextureData cellularGrowth(TextureTools.TextureData map, int generations, int replaceChance, Color targetValue, Color testValue, Color replaceValue)
	{
		TextureTools.TextureData textureData = map;
		for (int i = 0; i < generations; i++)
		{
			for (int j = 0; j < map.width; j++)
			{
				for (int k = 0; k < map.height; k++)
				{
					if (!(map.GetPixel(j, k) != targetValue))
					{
						int num = this.countNeighbours(map, j, k, testValue);
						if ((num > 1 && Random.Range(1, 100) < replaceChance) || num == 8)
						{
							textureData.SetPixel(j, k, replaceValue);
						}
						else
						{
							textureData.SetPixel(j, k, map.GetPixel(j, k));
						}
					}
				}
			}
		}
		map = textureData;
		return map;
	}

	// Token: 0x06000E04 RID: 3588 RVA: 0x00040E84 File Offset: 0x0003F084
	private TextureTools.TextureData insertWalls(TextureTools.TextureData map, Color targetValue, Color testValue, Color replaceValue)
	{
		TextureTools.TextureData textureData = map;
		for (int i = 0; i < map.width; i++)
		{
			for (int j = 0; j < map.height; j++)
			{
				if (!(map.GetPixel(i, j) != targetValue))
				{
					bool flag = false;
					if (this.isPointLegal(map, i, j - 1) && map.GetPixel(i, j - 1) == testValue)
					{
						flag = true;
					}
					if (flag)
					{
						textureData.SetPixel(i, j, replaceValue);
					}
					else
					{
						textureData.SetPixel(i, j, map.GetPixel(i, j));
					}
				}
			}
		}
		map = textureData;
		return map;
	}

	// Token: 0x06000E05 RID: 3589 RVA: 0x00040F1C File Offset: 0x0003F11C
	private TextureTools.TextureData insertBlock(TextureTools.TextureData map, int x, int y, int w, int h, Color value)
	{
		for (int i = 0; i < w; i++)
		{
			for (int j = 0; j < h; j++)
			{
				int x2 = x + (i - Mathf.CeilToInt((float)(w / 2)));
				int y2 = y + (j - Mathf.CeilToInt((float)(h / 2)));
				if (this.isPointLegal(map, x2, y2))
				{
					map.SetPixel(x2, y2, value);
				}
			}
		}
		return map;
	}

	// Token: 0x06000E06 RID: 3590 RVA: 0x00040F7C File Offset: 0x0003F17C
	private bool isPointLegal(TextureTools.TextureData map, int x, int y)
	{
		return x >= 0 && x < map.width && y >= 0 && y < map.height;
	}

	// Token: 0x04000361 RID: 865
	private TextureTools.TextureData mapTexture;

	// Token: 0x04000362 RID: 866
	private Color floorColor;

	// Token: 0x04000363 RID: 867
	private Color voidColor;

	// Token: 0x04000364 RID: 868
	private Color roofColor;

	// Token: 0x04000365 RID: 869
	private Color wallColor;

	// Token: 0x04000366 RID: 870
	private Color stairDownColor;

	// Token: 0x04000367 RID: 871
	private Color stairUpColor;

	// Token: 0x04000368 RID: 872
	private Color corridorColor;

	// Token: 0x04000369 RID: 873
	private Color treasureColor;

	// Token: 0x0200024A RID: 586
	private struct Room
	{
		// Token: 0x06001941 RID: 6465 RVA: 0x0006E9D8 File Offset: 0x0006CBD8
		public Room(int _x, int _y, TextureTools.TextureData _map, Color _value)
		{
			this.map = _map;
			this.value = _value;
			this.width = Random.Range(5, 10);
			this.height = Random.Range(5, 10);
			this.x = _x - this.width / 2;
			this.y = _y - this.height / 2;
			this.centerX = this.x + this.width / 2;
			this.centerY = this.y + this.height / 2;
			MapGenerator.Room.placedRooms.Clear();
			if (this.isRoomLegal())
			{
				MapGenerator.Room.placedRooms.Add(this);
				MapGenerator.Room.makeNewRoom(this.x, this.y, this.map, _value, this);
				this.insertBlock();
			}
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x0006EAA4 File Offset: 0x0006CCA4
		public Room(int _x, int _y, TextureTools.TextureData _map, Color _value, MapGenerator.Room parent)
		{
			this.map = _map;
			this.value = _value;
			this.width = Random.Range(5, 10);
			this.height = Random.Range(5, 10);
			this.x = _x - this.width / 2;
			this.y = _y - this.height / 2;
			this.centerX = this.x + this.width / 2;
			this.centerY = this.y + this.height / 2;
			if (this.isRoomLegal())
			{
				MapGenerator.Room.placedRooms.Add(this);
				MapGenerator.Room.makeNewRoom(this.centerX, this.centerY, this.map, _value, this);
				this.insertBlock();
				TextureTools.drawRay(this.centerX, this.centerY, parent.centerX, parent.centerY, this.map, this.value);
			}
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x0006EB94 File Offset: 0x0006CD94
		private static void makeNewRoom(int _x, int _y, TextureTools.TextureData _map, Color _value, MapGenerator.Room parent)
		{
			for (int i = 0; i < 20; i++)
			{
				if (MapGenerator.Room.placedRooms.Count < 10)
				{
					int num = _x;
					int num2 = _y;
					int num3 = Random.Range(0, 4);
					if (num3 == 0)
					{
						num += Random.Range(5, 15 + i);
					}
					else if (num3 == 1)
					{
						num -= Random.Range(5, 15 + i);
					}
					else if (num3 == 2)
					{
						num2 += Random.Range(5, 15 + i);
					}
					else if (num3 == 3)
					{
						num2 -= Random.Range(5, 15 + i);
					}
					new MapGenerator.Room(num, num2, _map, _value, parent);
				}
			}
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x0006EC20 File Offset: 0x0006CE20
		private bool isRoomPlacementOnMapLegal()
		{
			return (this.x > 0 & this.x + this.width < this.map.width - 1) && (this.y > 0 & this.y + this.height < this.map.height - 1);
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x0006EC80 File Offset: 0x0006CE80
		private bool isRoomLegal()
		{
			if (!this.isRoomPlacementOnMapLegal())
			{
				return false;
			}
			foreach (MapGenerator.Room targetRoom in MapGenerator.Room.placedRooms)
			{
				if (this.doRoomsOverlap(targetRoom))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x0006ECE8 File Offset: 0x0006CEE8
		private bool doRoomsOverlap(MapGenerator.Room targetRoom)
		{
			return this.doLinesOverlap(this.x, this.x + this.width, targetRoom.x, targetRoom.x + targetRoom.width, 2) && this.doLinesOverlap(this.y, this.y + this.height, targetRoom.y, targetRoom.y + targetRoom.height, 2);
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x0006ED56 File Offset: 0x0006CF56
		private bool doLinesOverlap(int x1Start, int x1Stop, int x2Start, int x2Stop, int padding)
		{
			return (x1Start > x2Start - padding && x1Start < x2Stop + padding) || (x2Start > x1Start - padding && x2Start < x1Stop + padding);
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x0006ED78 File Offset: 0x0006CF78
		private void insertBlock()
		{
			for (int i = this.x; i < this.x + this.width; i++)
			{
				for (int j = this.y; j < this.y + this.height; j++)
				{
					if (this.isPointLegal(this.map, i, j))
					{
						this.map.SetPixel(i, j, this.value);
					}
				}
			}
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x0006EDE7 File Offset: 0x0006CFE7
		private bool isPointLegal(TextureTools.TextureData map, int x, int y)
		{
			return x >= 0 && x < map.width && y >= 0 && y < map.height;
		}

		// Token: 0x040008EF RID: 2287
		private int x;

		// Token: 0x040008F0 RID: 2288
		private int y;

		// Token: 0x040008F1 RID: 2289
		private int width;

		// Token: 0x040008F2 RID: 2290
		private int height;

		// Token: 0x040008F3 RID: 2291
		private int centerX;

		// Token: 0x040008F4 RID: 2292
		private int centerY;

		// Token: 0x040008F5 RID: 2293
		private Color value;

		// Token: 0x040008F6 RID: 2294
		private TextureTools.TextureData map;

		// Token: 0x040008F7 RID: 2295
		private static List<MapGenerator.Room> placedRooms = new List<MapGenerator.Room>();
	}
}
