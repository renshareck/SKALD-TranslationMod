using System;
using System.Collections.Generic;

// Token: 0x0200003F RID: 63
[Serializable]
public class MapSaveDataContainer
{
	// Token: 0x060007FE RID: 2046 RVA: 0x00027C08 File Offset: 0x00025E08
	public MapSaveDataContainer(int width, int height, string id)
	{
		this.width = width;
		this.height = height;
		this.id = id;
		this.characterLayer = new MapSaveDataContainer.CharacterLayer(width, height);
		this.propLayer = new MapSaveDataContainer.PropLayer(width, height);
		this.itemLayer = new MapSaveDataContainer.ItemLayer(width, height);
	}

	// Token: 0x060007FF RID: 2047 RVA: 0x00027C64 File Offset: 0x00025E64
	public MapSaveDataContainer.TerrainLayer createAndGetTerrainLayer(string id)
	{
		MapSaveDataContainer.TerrainLayer terrainLayer = new MapSaveDataContainer.TerrainLayer(id, this.width, this.height);
		this.terrainLayers.Add(terrainLayer);
		return terrainLayer;
	}

	// Token: 0x040001A6 RID: 422
	public List<MapSaveDataContainer.TerrainLayer> terrainLayers = new List<MapSaveDataContainer.TerrainLayer>();

	// Token: 0x040001A7 RID: 423
	public MapSaveDataContainer.CharacterLayer characterLayer;

	// Token: 0x040001A8 RID: 424
	public MapSaveDataContainer.PropLayer propLayer;

	// Token: 0x040001A9 RID: 425
	public MapSaveDataContainer.ItemLayer itemLayer;

	// Token: 0x040001AA RID: 426
	public int width;

	// Token: 0x040001AB RID: 427
	public int height;

	// Token: 0x040001AC RID: 428
	public string id;

	// Token: 0x020001FB RID: 507
	[Serializable]
	public class ItemLayer : MapSaveDataContainer.InstanceLayer
	{
		// Token: 0x060017CF RID: 6095 RVA: 0x00069A91 File Offset: 0x00067C91
		public ItemLayer(int width, int height) : base("Props", width, height)
		{
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x00069AAC File Offset: 0x00067CAC
		public MapSaveDataContainer.ItemLayer.ItemSaveData addItemInstance(int x, int y, string id)
		{
			MapSaveDataContainer.ItemLayer.ItemSaveData itemSaveData = new MapSaveDataContainer.ItemLayer.ItemSaveData(base.getIndexFromPosition(x, y), id);
			this.instances.Add(itemSaveData);
			return itemSaveData;
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x00069AD8 File Offset: 0x00067CD8
		public List<MapSaveDataContainer.ItemLayer.ItemLoadData> getLoadData()
		{
			List<MapSaveDataContainer.ItemLayer.ItemLoadData> list = new List<MapSaveDataContainer.ItemLayer.ItemLoadData>();
			foreach (MapSaveDataContainer.ItemLayer.ItemSaveData itemSaveData in this.instances)
			{
				list.Add(new MapSaveDataContainer.ItemLayer.ItemLoadData(base.getXFromIndex(itemSaveData.pos), base.getYFromIndex(itemSaveData.pos), itemSaveData.id));
			}
			return list;
		}

		// Token: 0x040007D4 RID: 2004
		public List<MapSaveDataContainer.ItemLayer.ItemSaveData> instances = new List<MapSaveDataContainer.ItemLayer.ItemSaveData>();

		// Token: 0x02000324 RID: 804
		[Serializable]
		public class ItemSaveData : MapSaveDataContainer.InstanceLayer.InstanceSaveData
		{
			// Token: 0x06001C96 RID: 7318 RVA: 0x0007AF89 File Offset: 0x00079189
			public ItemSaveData(int pos, string id) : base(pos, id)
			{
			}
		}

		// Token: 0x02000325 RID: 805
		public class ItemLoadData : MapSaveDataContainer.InstanceLayer.InstanceLoadData
		{
			// Token: 0x06001C97 RID: 7319 RVA: 0x0007AF93 File Offset: 0x00079193
			public ItemLoadData(int x, int y, string id) : base(x, y, id)
			{
			}
		}
	}

	// Token: 0x020001FC RID: 508
	[Serializable]
	public class PropLayer : MapSaveDataContainer.InstanceLayer
	{
		// Token: 0x060017D2 RID: 6098 RVA: 0x00069B54 File Offset: 0x00067D54
		public PropLayer(int width, int height) : base("Props", width, height)
		{
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x00069B70 File Offset: 0x00067D70
		public MapSaveDataContainer.PropLayer.PropSaveData addPropInstance(int x, int y, string id)
		{
			MapSaveDataContainer.PropLayer.PropSaveData propSaveData = new MapSaveDataContainer.PropLayer.PropSaveData(base.getIndexFromPosition(x, y), id);
			this.instances.Add(propSaveData);
			return propSaveData;
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x00069B9C File Offset: 0x00067D9C
		public List<MapSaveDataContainer.PropLayer.PropLoadData> getLoadData()
		{
			List<MapSaveDataContainer.PropLayer.PropLoadData> list = new List<MapSaveDataContainer.PropLayer.PropLoadData>();
			foreach (MapSaveDataContainer.PropLayer.PropSaveData propSaveData in this.instances)
			{
				list.Add(new MapSaveDataContainer.PropLayer.PropLoadData(base.getXFromIndex(propSaveData.pos), base.getYFromIndex(propSaveData.pos), propSaveData.id));
			}
			return list;
		}

		// Token: 0x040007D5 RID: 2005
		public List<MapSaveDataContainer.PropLayer.PropSaveData> instances = new List<MapSaveDataContainer.PropLayer.PropSaveData>();

		// Token: 0x02000326 RID: 806
		[Serializable]
		public class PropSaveData : MapSaveDataContainer.InstanceLayer.InstanceSaveData
		{
			// Token: 0x06001C98 RID: 7320 RVA: 0x0007AF9E File Offset: 0x0007919E
			public PropSaveData(int pos, string id) : base(pos, id)
			{
			}
		}

		// Token: 0x02000327 RID: 807
		public class PropLoadData : MapSaveDataContainer.InstanceLayer.InstanceLoadData
		{
			// Token: 0x06001C99 RID: 7321 RVA: 0x0007AFA8 File Offset: 0x000791A8
			public PropLoadData(int x, int y, string id) : base(x, y, id)
			{
			}
		}
	}

	// Token: 0x020001FD RID: 509
	[Serializable]
	public class CharacterLayer : MapSaveDataContainer.InstanceLayer
	{
		// Token: 0x060017D5 RID: 6101 RVA: 0x00069C18 File Offset: 0x00067E18
		public CharacterLayer(int width, int height) : base("Characters", width, height)
		{
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x00069C34 File Offset: 0x00067E34
		public MapSaveDataContainer.CharacterLayer.CharacterSaveData addCharacterInstance(int x, int y, string id)
		{
			MapSaveDataContainer.CharacterLayer.CharacterSaveData characterSaveData = new MapSaveDataContainer.CharacterLayer.CharacterSaveData(base.getIndexFromPosition(x, y), id);
			this.instances.Add(characterSaveData);
			return characterSaveData;
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x00069C60 File Offset: 0x00067E60
		public List<MapSaveDataContainer.CharacterLayer.CharacterLoadData> getLoadData()
		{
			List<MapSaveDataContainer.CharacterLayer.CharacterLoadData> list = new List<MapSaveDataContainer.CharacterLayer.CharacterLoadData>();
			foreach (MapSaveDataContainer.CharacterLayer.CharacterSaveData characterSaveData in this.instances)
			{
				list.Add(new MapSaveDataContainer.CharacterLayer.CharacterLoadData(base.getXFromIndex(characterSaveData.pos), base.getYFromIndex(characterSaveData.pos), characterSaveData.id));
			}
			return list;
		}

		// Token: 0x040007D6 RID: 2006
		public List<MapSaveDataContainer.CharacterLayer.CharacterSaveData> instances = new List<MapSaveDataContainer.CharacterLayer.CharacterSaveData>();

		// Token: 0x02000328 RID: 808
		[Serializable]
		public class CharacterSaveData : MapSaveDataContainer.InstanceLayer.InstanceSaveData
		{
			// Token: 0x06001C9A RID: 7322 RVA: 0x0007AFB3 File Offset: 0x000791B3
			public CharacterSaveData(int pos, string id) : base(pos, id)
			{
			}
		}

		// Token: 0x02000329 RID: 809
		public class CharacterLoadData : MapSaveDataContainer.InstanceLayer.InstanceLoadData
		{
			// Token: 0x06001C9B RID: 7323 RVA: 0x0007AFBD File Offset: 0x000791BD
			public CharacterLoadData(int x, int y, string id) : base(x, y, id)
			{
			}
		}
	}

	// Token: 0x020001FE RID: 510
	[Serializable]
	public class TerrainLayer : MapSaveDataContainer.InstanceLayer
	{
		// Token: 0x060017D8 RID: 6104 RVA: 0x00069CDC File Offset: 0x00067EDC
		public TerrainLayer(string id, int width, int height) : base(id, width, height)
		{
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x00069CF4 File Offset: 0x00067EF4
		public void addTerrainInstance(int x, int y, string id, int subImage)
		{
			foreach (MapSaveDataContainer.TerrainLayer.TerrainSaveData terrainSaveData in this.instances)
			{
				if (terrainSaveData.id == id)
				{
					terrainSaveData.addData(base.getIndexFromPosition(x, y), subImage);
					return;
				}
			}
			MapSaveDataContainer.TerrainLayer.TerrainSaveData terrainSaveData2 = new MapSaveDataContainer.TerrainLayer.TerrainSaveData(id);
			terrainSaveData2.addData(base.getIndexFromPosition(x, y), subImage);
			this.instances.Add(terrainSaveData2);
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x00069D84 File Offset: 0x00067F84
		public List<MapSaveDataContainer.TerrainLayer.TerrainLoadData> getLoadData()
		{
			List<MapSaveDataContainer.TerrainLayer.TerrainLoadData> list = new List<MapSaveDataContainer.TerrainLayer.TerrainLoadData>();
			foreach (MapSaveDataContainer.TerrainLayer.TerrainSaveData terrainSaveData in this.instances)
			{
				foreach (MapSaveDataContainer.TerrainLayer.TerrainSaveData.SubImageSaveData subImageSaveData in terrainSaveData.subImages)
				{
					foreach (int i in subImageSaveData.positionIndexList)
					{
						list.Add(new MapSaveDataContainer.TerrainLayer.TerrainLoadData(base.getXFromIndex(i), base.getYFromIndex(i), terrainSaveData.id, subImageSaveData.subImage));
					}
				}
			}
			return list;
		}

		// Token: 0x040007D7 RID: 2007
		public List<MapSaveDataContainer.TerrainLayer.TerrainSaveData> instances = new List<MapSaveDataContainer.TerrainLayer.TerrainSaveData>();

		// Token: 0x0200032A RID: 810
		[Serializable]
		public class TerrainSaveData
		{
			// Token: 0x06001C9C RID: 7324 RVA: 0x0007AFC8 File Offset: 0x000791C8
			public TerrainSaveData(string id)
			{
				this.id = id;
				this.subImages = new List<MapSaveDataContainer.TerrainLayer.TerrainSaveData.SubImageSaveData>();
			}

			// Token: 0x06001C9D RID: 7325 RVA: 0x0007AFE4 File Offset: 0x000791E4
			public void addData(int pos, int subImage)
			{
				foreach (MapSaveDataContainer.TerrainLayer.TerrainSaveData.SubImageSaveData subImageSaveData in this.subImages)
				{
					if (subImageSaveData.subImage == subImage)
					{
						subImageSaveData.positionIndexList.Add(pos);
						return;
					}
				}
				MapSaveDataContainer.TerrainLayer.TerrainSaveData.SubImageSaveData subImageSaveData2 = new MapSaveDataContainer.TerrainLayer.TerrainSaveData.SubImageSaveData(subImage);
				subImageSaveData2.positionIndexList.Add(pos);
				this.subImages.Add(subImageSaveData2);
			}

			// Token: 0x04000AB4 RID: 2740
			public string id;

			// Token: 0x04000AB5 RID: 2741
			public List<MapSaveDataContainer.TerrainLayer.TerrainSaveData.SubImageSaveData> subImages;

			// Token: 0x020003F1 RID: 1009
			[Serializable]
			public class SubImageSaveData
			{
				// Token: 0x06001DB6 RID: 7606 RVA: 0x0007DA1A File Offset: 0x0007BC1A
				public SubImageSaveData(int subImage)
				{
					this.subImage = subImage;
				}

				// Token: 0x04000C93 RID: 3219
				public int subImage;

				// Token: 0x04000C94 RID: 3220
				public List<int> positionIndexList = new List<int>();
			}
		}

		// Token: 0x0200032B RID: 811
		public class TerrainLoadData : MapSaveDataContainer.InstanceLayer.InstanceLoadData
		{
			// Token: 0x06001C9E RID: 7326 RVA: 0x0007B068 File Offset: 0x00079268
			public TerrainLoadData(int x, int y, string id, int subImage) : base(x, y, id)
			{
				this.subImage = subImage;
			}

			// Token: 0x04000AB6 RID: 2742
			public int subImage;
		}
	}

	// Token: 0x020001FF RID: 511
	[Serializable]
	public class InstanceLayer
	{
		// Token: 0x060017DB RID: 6107 RVA: 0x00069E80 File Offset: 0x00068080
		protected InstanceLayer(string id, int width, int height)
		{
			this.id = id;
			this.width = width;
			this.height = height;
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x00069E9D File Offset: 0x0006809D
		public int getIndexFromPosition(int x, int y)
		{
			return y * this.width + x;
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x00069EA9 File Offset: 0x000680A9
		public int getYFromIndex(int i)
		{
			return i / this.width;
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x00069EB3 File Offset: 0x000680B3
		public int getXFromIndex(int i)
		{
			return i % this.width;
		}

		// Token: 0x040007D8 RID: 2008
		public string id;

		// Token: 0x040007D9 RID: 2009
		public int width;

		// Token: 0x040007DA RID: 2010
		public int height;

		// Token: 0x0200032C RID: 812
		[Serializable]
		public class InstanceSaveData
		{
			// Token: 0x06001C9F RID: 7327 RVA: 0x0007B07B File Offset: 0x0007927B
			public InstanceSaveData(int pos, string id)
			{
				this.pos = pos;
				this.id = id;
			}

			// Token: 0x04000AB7 RID: 2743
			public int pos;

			// Token: 0x04000AB8 RID: 2744
			public string id;
		}

		// Token: 0x0200032D RID: 813
		public class InstanceLoadData
		{
			// Token: 0x06001CA0 RID: 7328 RVA: 0x0007B091 File Offset: 0x00079291
			public InstanceLoadData(int x, int y, string id)
			{
				this.x = x;
				this.y = y;
				this.id = id;
			}

			// Token: 0x04000AB9 RID: 2745
			public int x;

			// Token: 0x04000ABA RID: 2746
			public int y;

			// Token: 0x04000ABB RID: 2747
			public string id;
		}
	}
}
