using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

// Token: 0x020000ED RID: 237
public class MapTileGrid
{
	// Token: 0x06000E93 RID: 3731 RVA: 0x00043F88 File Offset: 0x00042188
	public MapTileGrid(MapSaveDataContainer mapData, Map map)
	{
		this.mapTileWidth = mapData.width;
		this.mapTileHeight = mapData.height;
		this.map = map;
		this.clearTileMap();
		this.setNeighboursDict();
		if (mapData == null)
		{
			this.populateTileMap();
			return;
		}
		this.populateTileMap(mapData);
	}

	// Token: 0x06000E94 RID: 3732 RVA: 0x00043FF0 File Offset: 0x000421F0
	private void populateTileMap()
	{
		for (int i = 0; i < this.getMapTileWidth(); i++)
		{
			for (int j = 0; j < this.getMapTileHeight(); j++)
			{
				this.setTile(i, j, new MapTile(i, j, this.map.getId()));
			}
		}
	}

	// Token: 0x06000E95 RID: 3733 RVA: 0x0004403C File Offset: 0x0004223C
	private void populateTileMap(MapSaveDataContainer mapData)
	{
		List<MapSaveDataContainer.TerrainLayer.TerrainLoadData>[,] array = new List<MapSaveDataContainer.TerrainLayer.TerrainLoadData>[this.mapTileWidth, this.mapTileHeight];
		List<MapSaveDataContainer.TerrainLayer.TerrainLoadData>[,] array2 = new List<MapSaveDataContainer.TerrainLayer.TerrainLoadData>[this.mapTileWidth, this.mapTileHeight];
		MapSaveDataContainer.CharacterLayer.CharacterLoadData[,] array3 = new MapSaveDataContainer.CharacterLayer.CharacterLoadData[this.mapTileWidth, this.mapTileHeight];
		MapSaveDataContainer.PropLayer.PropLoadData[,] array4 = new MapSaveDataContainer.PropLayer.PropLoadData[this.mapTileWidth, this.mapTileHeight];
		MapSaveDataContainer.ItemLayer.ItemLoadData[,] array5 = new MapSaveDataContainer.ItemLayer.ItemLoadData[this.mapTileWidth, this.mapTileHeight];
		foreach (MapSaveDataContainer.TerrainLayer terrainLayer in mapData.terrainLayers)
		{
			List<MapSaveDataContainer.TerrainLayer.TerrainLoadData>[,] array6;
			if (terrainLayer.id.Contains("Ground"))
			{
				array6 = array;
			}
			else
			{
				array6 = array2;
			}
			foreach (MapSaveDataContainer.TerrainLayer.TerrainLoadData terrainLoadData in terrainLayer.getLoadData())
			{
				if (array6[terrainLoadData.x, terrainLoadData.y] == null)
				{
					array6[terrainLoadData.x, terrainLoadData.y] = new List<MapSaveDataContainer.TerrainLayer.TerrainLoadData>();
				}
				array6[terrainLoadData.x, terrainLoadData.y].Add(terrainLoadData);
			}
		}
		foreach (MapSaveDataContainer.CharacterLayer.CharacterLoadData characterLoadData in mapData.characterLayer.getLoadData())
		{
			array3[characterLoadData.x, characterLoadData.y] = characterLoadData;
		}
		foreach (MapSaveDataContainer.PropLayer.PropLoadData propLoadData in mapData.propLayer.getLoadData())
		{
			array4[propLoadData.x, propLoadData.y] = propLoadData;
		}
		foreach (MapSaveDataContainer.ItemLayer.ItemLoadData itemLoadData in mapData.itemLayer.getLoadData())
		{
			array5[itemLoadData.x, itemLoadData.y] = itemLoadData;
		}
		for (int i = 0; i < this.getMapTileWidth(); i++)
		{
			for (int j = 0; j < this.getMapTileHeight(); j++)
			{
				MapTile mapTile = new MapTile(i, j, this.map, array[i, j], array2[i, j]);
				this.setTile(i, j, mapTile);
				if (array3[i, j] != null)
				{
					Character character = GameData.instantiateCharacter(array3[i, j].id);
					if (character != null)
					{
						mapTile.clearParty();
						mapTile.addCharacter(character);
					}
					else
					{
						MainControl.logError(string.Concat(new string[]
						{
							"Missing Instance: ",
							array3[i, j].id,
							" on Map ",
							this.map.getId(),
							" at ",
							i.ToString(),
							"/",
							j.ToString()
						}));
					}
				}
				if (array4[i, j] != null)
				{
					Prop prop = GameData.instantiateProp(array4[i, j].id);
					if (prop != null)
					{
						mapTile.clearProp();
						this.setProp(mapTile, prop);
					}
					else
					{
						MainControl.logError(string.Concat(new string[]
						{
							"Missing Instance: ",
							array4[i, j].id,
							" on Map ",
							this.map.getId(),
							" at ",
							i.ToString(),
							"/",
							j.ToString()
						}));
					}
				}
				if (array5[i, j] != null)
				{
					bool flag;
					if (mapTile.getParty() != null)
					{
						flag = GameData.applyLoadoutData(array5[i, j].id, mapTile.getParty().getInventory());
					}
					else
					{
						flag = GameData.applyLoadoutData(array5[i, j].id, mapTile.getInventory());
					}
					if (!flag)
					{
						MainControl.logError(string.Concat(new string[]
						{
							"Faulty loadout on ",
							this.map.getId(),
							" at position",
							mapTile.getTileX().ToString(),
							"/",
							mapTile.getTileY().ToString()
						}));
					}
				}
			}
		}
	}

	// Token: 0x06000E96 RID: 3734 RVA: 0x000444FC File Offset: 0x000426FC
	public void setProp(MapTile tile, Prop prop)
	{
		if (prop == null)
		{
			return;
		}
		if (tile == null)
		{
			return;
		}
		tile.setProp(prop);
		if (prop.getTileHeight() > 1 || prop.getTileWidth() > 1)
		{
			int tileX = tile.getTileX();
			int tileY = tile.getTileY();
			for (int i = 0; i < prop.getTileWidth(); i++)
			{
				for (int j = 0; j < prop.getTileHeight(); j++)
				{
					MapTile tile2 = this.getTile(tileX + i, tileY + j);
					if (tile2 != null)
					{
						tile2.setGuestProp(prop);
					}
				}
			}
		}
	}

	// Token: 0x06000E97 RID: 3735 RVA: 0x00044574 File Offset: 0x00042774
	public List<MapTile> getTilesToFleeTo(Character character)
	{
		List<MapTile> manhattanNeighbours = this.getManhattanNeighbours(character.getTileX(), character.getTileY());
		List<MapTile> list = new List<MapTile>();
		foreach (MapTile mapTile in manhattanNeighbours)
		{
			if (mapTile.isTileOpenAndPassable())
			{
				bool flag = true;
				foreach (MapTile mapTile2 in this.getManhattanNeighbours(mapTile.getTileX(), mapTile.getTileY()))
				{
					if (mapTile2.getParty() != null && character.isNPCHostile(mapTile2.getParty().getCurrentCharacter()))
					{
						flag = false;
					}
				}
				if (flag)
				{
					list.Add(mapTile);
				}
			}
		}
		return list;
	}

	// Token: 0x06000E98 RID: 3736 RVA: 0x00044654 File Offset: 0x00042854
	public int getMapTileWidth()
	{
		return this.mapTileWidth;
	}

	// Token: 0x06000E99 RID: 3737 RVA: 0x0004465C File Offset: 0x0004285C
	public int getMapTileHeight()
	{
		return this.mapTileHeight;
	}

	// Token: 0x06000E9A RID: 3738 RVA: 0x00044664 File Offset: 0x00042864
	public void findCombatPath(Party party, int targetX, int targetY)
	{
		NavigationTools.setPath(party, targetX, targetY, false, true, this.getVisibleTilesArray(), false);
	}

	// Token: 0x06000E9B RID: 3739 RVA: 0x00044678 File Offset: 0x00042878
	public bool findPathToMouseTile(Party party)
	{
		if (this.mouseTile == null)
		{
			return false;
		}
		bool traverseLand = !this.mouseTile.isWater() || this.mouseTile.hasVehicle();
		NavigationTools.setPath(party, this.mouseTile.getTileX(), this.mouseTile.getTileY(), party.canTraverseWater(), traverseLand, this.tileMap, false);
		this.examineTile = this.mouseTile;
		if (!party.navigationCourseHasNodes())
		{
			MapTile propRootTile = this.mouseTile.getPropRootTile();
			if (propRootTile != null && propRootTile != this.mouseTile)
			{
				NavigationTools.setPath(party, propRootTile.getTileX(), propRootTile.getTileY(), party.canTraverseWater(), traverseLand, this.tileMap, false);
				this.examineTile = propRootTile;
			}
		}
		return true;
	}

	// Token: 0x06000E9C RID: 3740 RVA: 0x00044729 File Offset: 0x00042929
	public MapTile getTile(int x, int y)
	{
		if (!this.isTileValid(x, y))
		{
			return null;
		}
		return this.tileMap[x, y];
	}

	// Token: 0x06000E9D RID: 3741 RVA: 0x00044744 File Offset: 0x00042944
	public void resetTextureBuffers()
	{
		MapTile[,] array = this.tileMap;
		int upperBound = array.GetUpperBound(0);
		int upperBound2 = array.GetUpperBound(1);
		for (int i = array.GetLowerBound(0); i <= upperBound; i++)
		{
			for (int j = array.GetLowerBound(1); j <= upperBound2; j++)
			{
				array[i, j].resetTextureBuffer();
			}
		}
	}

	// Token: 0x06000E9E RID: 3742 RVA: 0x000447A0 File Offset: 0x000429A0
	public void clearTileMap()
	{
		this.tileMap = new MapTile[this.mapTileWidth, this.mapTileHeight];
		for (int i = 0; i < this.mapTileWidth; i++)
		{
			for (int j = 0; j < this.mapTileHeight; j++)
			{
				this.tileMap[i, j] = null;
			}
		}
	}

	// Token: 0x06000E9F RID: 3743 RVA: 0x000447F4 File Offset: 0x000429F4
	public void setTile(int x, int y, MapTile tile)
	{
		if (!this.isTileValid(x, y))
		{
			return;
		}
		this.tileMap[x, y] = tile;
	}

	// Token: 0x06000EA0 RID: 3744 RVA: 0x0004480F File Offset: 0x00042A0F
	public bool isTileValid(int x, int y)
	{
		return x >= 0 && x < this.mapTileWidth && y >= 0 && y < this.mapTileHeight;
	}

	// Token: 0x06000EA1 RID: 3745 RVA: 0x00044830 File Offset: 0x00042A30
	protected List<MapTile> getVerticalNeighbours(int x, int y)
	{
		this.tiles2.Clear();
		if (this.isTileValid(x, y + 1))
		{
			this.tiles2.Add(this.getTile(x, y + 1));
		}
		if (this.isTileValid(x, y - 1))
		{
			this.tiles2.Add(this.getTile(x, y - 1));
		}
		return this.tiles2;
	}

	// Token: 0x06000EA2 RID: 3746 RVA: 0x00044890 File Offset: 0x00042A90
	protected List<MapTile> getHorizontalNeighbours(int x, int y)
	{
		this.tiles2.Clear();
		if (this.isTileValid(x + 1, y))
		{
			this.tiles2.Add(this.getTile(x + 1, y));
		}
		if (this.isTileValid(x - 1, y))
		{
			this.tiles2.Add(this.getTile(x - 1, y));
		}
		return this.tiles2;
	}

	// Token: 0x06000EA3 RID: 3747 RVA: 0x000448F0 File Offset: 0x00042AF0
	public List<MapTile> getManhattanNeighbours(int x, int y)
	{
		List<MapTile> list = new List<MapTile>();
		foreach (MapTile item in this.getVerticalNeighbours(x, y))
		{
			list.Add(item);
		}
		foreach (MapTile item2 in this.getHorizontalNeighbours(x, y))
		{
			list.Add(item2);
		}
		return list;
	}

	// Token: 0x06000EA4 RID: 3748 RVA: 0x00044990 File Offset: 0x00042B90
	public bool isPlayerPartyInNeighborhood(int x, int y)
	{
		foreach (MapTile mapTile in this.getManhattanNeighbours(x, y))
		{
			if (mapTile.getParty() != null && mapTile.getParty().isPC())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000EA5 RID: 3749 RVA: 0x000449FC File Offset: 0x00042BFC
	public MapTile getClosestConnnectedOpenTile(int x, int y, List<MapTile> legalTiles)
	{
		if (!this.isTileValid(x, y))
		{
			return null;
		}
		MapTile tile = this.getTile(x, y);
		if (tile.isTileOpenAndPassable() && !tile.isWater())
		{
			return tile;
		}
		List<MapTile> list = new List<MapTile>();
		list.Add(tile);
		List<MapTile> manhattanNeighbours = this.getManhattanNeighbours(x, y);
		while (manhattanNeighbours.Count > 0)
		{
			List<MapTile> list2 = new List<MapTile>();
			List<MapTile> list3 = new List<MapTile>();
			foreach (MapTile mapTile in manhattanNeighbours)
			{
				if (mapTile.isPassable() && !mapTile.isWater() && (legalTiles == null || legalTiles.Contains(mapTile)))
				{
					if (mapTile.isTileOpen())
					{
						return mapTile;
					}
					foreach (MapTile item in this.getManhattanNeighbours(mapTile.getTileX(), mapTile.getTileY()))
					{
						if (!list2.Contains(item) && !manhattanNeighbours.Contains(item) && !list.Contains(item))
						{
							list2.Add(item);
						}
					}
				}
				list3.Add(mapTile);
			}
			foreach (MapTile item2 in list2)
			{
				manhattanNeighbours.Add(item2);
			}
			foreach (MapTile item3 in list3)
			{
				manhattanNeighbours.Remove(item3);
			}
		}
		return null;
	}

	// Token: 0x06000EA6 RID: 3750 RVA: 0x00044BE0 File Offset: 0x00042DE0
	public MapTile getClosestOpenTile(int x, int y)
	{
		if (!this.isTileValid(x, y))
		{
			return null;
		}
		MapTile tile = this.getTile(x, y);
		if (tile.isTileOpenAndPassable())
		{
			return tile;
		}
		List<MapTile> list = new List<MapTile>();
		list.Add(tile);
		List<MapTile> manhattanNeighbours = this.getManhattanNeighbours(x, y);
		List<MapTile> list2 = new List<MapTile>();
		List<MapTile> list3 = new List<MapTile>();
		while (manhattanNeighbours.Count > 0)
		{
			list2.Clear();
			list3.Clear();
			foreach (MapTile mapTile in manhattanNeighbours)
			{
				if (mapTile.isPassable())
				{
					return mapTile;
				}
				foreach (MapTile mapTile2 in this.getManhattanNeighbours(mapTile.getTileX(), mapTile.getTileY()))
				{
					if (!list2.Contains(mapTile2) && !manhattanNeighbours.Contains(mapTile2) && !list.Contains(mapTile2))
					{
						if (mapTile2.isPassable())
						{
							return mapTile2;
						}
						list2.Add(mapTile2);
					}
				}
				list.Add(mapTile);
				list3.Add(mapTile);
			}
			foreach (MapTile item in list2)
			{
				manhattanNeighbours.Add(item);
			}
			foreach (MapTile item2 in list3)
			{
				manhattanNeighbours.Remove(item2);
			}
		}
		return null;
	}

	// Token: 0x06000EA7 RID: 3751 RVA: 0x00044DBC File Offset: 0x00042FBC
	public void revealPoint(int x, int y, int radius)
	{
		for (int i = x - radius; i < x + radius + 1; i++)
		{
			for (int j = y - radius; j < y + radius + 1; j++)
			{
				if (this.isTileValid(i, j))
				{
					this.getTile(i, j).setSpotted();
				}
			}
		}
	}

	// Token: 0x06000EA8 RID: 3752 RVA: 0x00044E04 File Offset: 0x00043004
	public List<MapTile> getConnectedIndoorTiles()
	{
		List<MapTile> list = new List<MapTile>();
		List<MapTile> list2 = new List<MapTile>();
		List<MapTile> list3 = new List<MapTile>();
		list3.Add(this.getTile(this.getXPos(), this.getYPos()));
		while (list3.Count > 0)
		{
			List<MapTile> list4 = new List<MapTile>();
			List<MapTile> list5 = new List<MapTile>();
			foreach (MapTile mapTile in list3)
			{
				if (this.map.isTileCoveredByOverlay(mapTile.getTileX(), mapTile.getTileY()))
				{
					list.Add(mapTile);
					if (mapTile.isPassable())
					{
						foreach (MapTile mapTile2 in this.getManhattanNeighbours(mapTile.getTileX(), mapTile.getTileY()))
						{
							if (!list4.Contains(mapTile2) && !list3.Contains(mapTile2) && !list2.Contains(mapTile2) && this.isPointInView(mapTile2.getTileX(), mapTile2.getTileY(), 3))
							{
								list4.Add(mapTile2);
							}
						}
					}
				}
				list5.Add(mapTile);
				list2.Add(mapTile);
			}
			foreach (MapTile item in list4)
			{
				list3.Add(item);
			}
			foreach (MapTile item2 in list5)
			{
				list3.Remove(item2);
			}
		}
		return list;
	}

	// Token: 0x06000EA9 RID: 3753 RVA: 0x00044FE8 File Offset: 0x000431E8
	public List<MapTile> getAccessibleTilesFromParty()
	{
		return this.getAccessibleTilesFromPoint(this.getXPos(), this.getYPos(), 0);
	}

	// Token: 0x06000EAA RID: 3754 RVA: 0x00045000 File Offset: 0x00043200
	public void fetchCharacterJustOutsideCombat(List<Party> opponents)
	{
		List<MapTile> list = new List<MapTile>();
		List<MapTile> accessibleTilesFromParty = this.getAccessibleTilesFromParty();
		List<Party> list2 = new List<Party>();
		int num = this.getViewportX() - MapIllustrator.ScreenDimensions.getIllustratedMapHalfWidth();
		int num2 = this.getViewportX() + MapIllustrator.ScreenDimensions.getIllustratedMapHalfWidth();
		int num3 = this.getViewportY() - MapIllustrator.ScreenDimensions.getIllustratedMapHalfHeight();
		int num4 = this.getViewportY() + MapIllustrator.ScreenDimensions.getIllustratedMapHalfHeight();
		foreach (Party party in opponents)
		{
			foreach (MapTile mapTile in this.getSquareAroundPoint(party.getTileX(), party.getTileY(), 3))
			{
				if (this.isPointInView(mapTile.getTileX(), mapTile.getTileY(), 0))
				{
					if (mapTile.isTileOpenAndPassable() && (mapTile.getTileX() == num || mapTile.getTileX() == num2 || mapTile.getTileY() == num3 || mapTile.getTileY() == num4) && accessibleTilesFromParty.Contains(mapTile))
					{
						list.Add(mapTile);
					}
				}
				else
				{
					Party party2 = mapTile.getParty();
					if (party2 != null && party2.isHostile() && !list2.Contains(party2))
					{
						list2.Add(party2);
					}
				}
			}
		}
		foreach (Party party3 in list2)
		{
			foreach (MapTile mapTile2 in list)
			{
				if (mapTile2.isTileOpenAndPassable() && this.isTilePassableToNPC(party3, mapTile2.getTileX(), mapTile2.getTileY()))
				{
					party3.getMapTile().clearParty();
					mapTile2.setParty(party3);
					party3.snapToGrid();
					this.revealPoint(mapTile2.getTileX(), mapTile2.getTileY(), 1);
					break;
				}
			}
		}
	}

	// Token: 0x06000EAB RID: 3755 RVA: 0x0004523C File Offset: 0x0004343C
	public List<MapTile> getAccessibleTilesFromPoint(int x, int y, int padding)
	{
		List<MapTile> list = new List<MapTile>();
		List<MapTile> list2 = new List<MapTile>();
		List<MapTile> list3 = new List<MapTile>();
		list3.Add(this.getTile(x, y));
		while (list3.Count > 0)
		{
			List<MapTile> list4 = new List<MapTile>();
			List<MapTile> list5 = new List<MapTile>();
			foreach (MapTile mapTile in list3)
			{
				if (mapTile.isPassable() && !mapTile.isWater())
				{
					list.Add(mapTile);
					foreach (MapTile mapTile2 in this.getManhattanNeighbours(mapTile.getTileX(), mapTile.getTileY()))
					{
						if (!list4.Contains(mapTile2) && !list3.Contains(mapTile2) && !list2.Contains(mapTile2) && this.isPointInView(mapTile2.getTileX(), mapTile2.getTileY(), padding))
						{
							list4.Add(mapTile2);
						}
					}
				}
				list5.Add(mapTile);
				list2.Add(mapTile);
			}
			foreach (MapTile item in list4)
			{
				list3.Add(item);
			}
			foreach (MapTile item2 in list5)
			{
				list3.Remove(item2);
			}
		}
		return list;
	}

	// Token: 0x06000EAC RID: 3756 RVA: 0x00045408 File Offset: 0x00043608
	public void setMouseTile(Vector2 pos)
	{
		this.mouseTile = this.getTileAtRelativeLocalPos(pos);
	}

	// Token: 0x06000EAD RID: 3757 RVA: 0x00045418 File Offset: 0x00043618
	public MapTile getTileAtRelativeLocalPos(Vector2 pos)
	{
		if (pos.x < 0f || (double)pos.x > 0.95 || pos.y < 0f || pos.y > 1f)
		{
			return null;
		}
		int num = this.getViewportX() - (MapIllustrator.ScreenDimensions.getIllustratedMapHalfWidth() + 1);
		int num2 = this.getViewportY() - (MapIllustrator.ScreenDimensions.getIllustratedMapHalfHeight() + 1);
		int num3 = num + Mathf.FloorToInt((float)MapIllustrator.ScreenDimensions.getIllustratedMapWidthAndRim() * pos.x);
		int num4 = num2 + Mathf.FloorToInt((float)MapIllustrator.ScreenDimensions.getIllustratedMapHeightAndRim() * pos.y);
		MapTile result = null;
		if (num3 < 0)
		{
			num3 = 0;
		}
		else if (num3 > this.getMapTileWidth() - 1)
		{
			num3 = this.getMapTileWidth() - 1;
		}
		if (num4 < 0)
		{
			num4 = 0;
		}
		else if (num4 > this.getMapTileHeight() - 1)
		{
			num4 = this.getMapTileHeight() - 1;
		}
		if (pos.x != 0f && pos.y != 0f)
		{
			result = this.getTile(num3, num4);
		}
		return result;
	}

	// Token: 0x06000EAE RID: 3758 RVA: 0x00045504 File Offset: 0x00043704
	public List<Character> getNeighbouringCharacters(int x, int y)
	{
		List<MapTile> manhattanNeighbours = this.getManhattanNeighbours(x, y);
		List<Character> list = new List<Character>(4);
		foreach (MapTile mapTile in manhattanNeighbours)
		{
			if (mapTile.getCharacter() != null)
			{
				list.Add(mapTile.getCharacter());
			}
		}
		return list;
	}

	// Token: 0x06000EAF RID: 3759 RVA: 0x00045570 File Offset: 0x00043770
	public List<Character> getNeighbouringOpponents(Character character, List<Character> opponents)
	{
		List<Character> neighbouringCharacters = this.getNeighbouringCharacters(character.getTileX(), character.getTileY());
		List<Character> list = new List<Character>(4);
		foreach (Character character2 in neighbouringCharacters)
		{
			if (opponents.Contains(character2) && !character2.isDead())
			{
				list.Add(character2);
			}
		}
		return list;
	}

	// Token: 0x06000EB0 RID: 3760 RVA: 0x000455E8 File Offset: 0x000437E8
	public void getNearbyAllyCount(Character character)
	{
		int tileX = character.getTileX();
		int tileY = character.getTileY();
		int num = 0;
		num += this.getAllyCountFromList(character, this.getVerticalNeighbours(tileX, tileY));
		num += this.getAllyCountFromList(character, this.getHorizontalNeighbours(tileX, tileY));
		character.setNearbyAllyCount(num);
	}

	// Token: 0x06000EB1 RID: 3761 RVA: 0x00045630 File Offset: 0x00043830
	public void getFlanking(Character character)
	{
		int tileX = character.getTileX();
		int tileY = character.getTileY();
		bool flanked = false;
		if (this.getEnemyCountFromList(character, this.getVerticalNeighbours(tileX, tileY)) >= 2)
		{
			flanked = true;
		}
		if (this.getEnemyCountFromList(character, this.getHorizontalNeighbours(tileX, tileY)) >= 2)
		{
			flanked = true;
		}
		character.setFlanked(flanked);
	}

	// Token: 0x06000EB2 RID: 3762 RVA: 0x0004567C File Offset: 0x0004387C
	public void getInMelee(Character character)
	{
		int tileX = character.getTileX();
		int tileY = character.getTileY();
		bool inMelee = false;
		if (this.getEnemyCountFromList(character, this.getVerticalNeighbours(tileX, tileY)) > 0)
		{
			inMelee = true;
		}
		else if (this.getEnemyCountFromList(character, this.getHorizontalNeighbours(tileX, tileY)) > 0)
		{
			inMelee = true;
		}
		character.setInMelee(inMelee);
	}

	// Token: 0x06000EB3 RID: 3763 RVA: 0x000456CC File Offset: 0x000438CC
	private int getAllyCountFromList(Character character, List<MapTile> tiles)
	{
		int num = 0;
		foreach (MapTile mapTile in tiles)
		{
			Character character2 = mapTile.getCharacter();
			if (character2 != null && character.isNPCAlly(character2) && !character2.isDead() && !character2.isVulnerable())
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06000EB4 RID: 3764 RVA: 0x0004573C File Offset: 0x0004393C
	private int getEnemyCountFromList(Character character, List<MapTile> tiles)
	{
		int num = 0;
		foreach (MapTile mapTile in tiles)
		{
			Character character2 = mapTile.getCharacter();
			if (character2 != null && character.isNPCHostile(character2) && !character2.isDead() && !character2.isVulnerable() && !character2.isPanicked())
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06000EB5 RID: 3765 RVA: 0x000457B4 File Offset: 0x000439B4
	public Character getBestCombatTarget(Character character, List<Character> opponents)
	{
		Character character2 = this.getBestMeleeTarget(character, opponents);
		if (character2 == null)
		{
			character2 = this.getClosestAvailableTarget(character, opponents);
		}
		return character2;
	}

	// Token: 0x06000EB6 RID: 3766 RVA: 0x000457D8 File Offset: 0x000439D8
	private Character getClosestAvailableTarget(Character character, List<Character> opponentList)
	{
		if (opponentList.Count == 0)
		{
			return null;
		}
		Character character2 = null;
		int num = -1;
		foreach (Character character3 in opponentList)
		{
			if (!character3.isHidden())
			{
				Party tileParty = character.getTileParty();
				NavigationCourse navigationCourse = NavigationTools.findPath(tileParty.getTileX(), tileParty.getTileY(), character3.getTileX(), character3.getTileY(), tileParty.canTraverseWater(), true, this.getVisibleTilesArray(), false);
				if (navigationCourse != null && (character2 == null || navigationCourse.getLength() < num))
				{
					character2 = character3;
					num = navigationCourse.getLength();
				}
			}
		}
		return character2;
	}

	// Token: 0x06000EB7 RID: 3767 RVA: 0x0004588C File Offset: 0x00043A8C
	public List<MapTile> getSquareAroundPoint(int x, int y, int radius)
	{
		List<MapTile> list = new List<MapTile>();
		for (int i = x - radius; i <= x + radius; i++)
		{
			for (int j = y - radius; j <= y + radius; j++)
			{
				if (this.isTileValid(i, j))
				{
					list.Add(this.getTile(i, j));
				}
			}
		}
		return list;
	}

	// Token: 0x06000EB8 RID: 3768 RVA: 0x000458D8 File Offset: 0x00043AD8
	private Character getBestMeleeTarget(Character character, List<Character> opponents)
	{
		List<Character> neighbouringOpponents = this.getNeighbouringOpponents(character, opponents);
		List<Character> list = new List<Character>();
		if (neighbouringOpponents.Count == 0)
		{
			return null;
		}
		foreach (Character character2 in neighbouringOpponents)
		{
			if (character2 == character.getTargetOpponent())
			{
				return character2;
			}
			if (!character2.isHidden())
			{
				list.Add(character2);
			}
		}
		if (list.Count == 0)
		{
			return null;
		}
		return list[Random.Range(0, list.Count)];
	}

	// Token: 0x06000EB9 RID: 3769 RVA: 0x00045974 File Offset: 0x00043B74
	public bool attemptToPlaceCharacterCloseToPoint(int x, int y, Character c, List<MapTile> legalTiles)
	{
		if (c == null)
		{
			return false;
		}
		MapTile closestConnnectedOpenTile = this.getClosestConnnectedOpenTile(x, y, legalTiles);
		if (closestConnnectedOpenTile == null)
		{
			MainControl.logError("Could not find open tile near: " + x.ToString() + " / " + y.ToString());
			return false;
		}
		closestConnnectedOpenTile.addCharacter(c);
		return true;
	}

	// Token: 0x06000EBA RID: 3770 RVA: 0x000459C0 File Offset: 0x00043BC0
	public Point stepTowardsTarget(int x1, int y1, int x2, int y2)
	{
		List<MapTile> manhattanNeighbours = this.getManhattanNeighbours(x1, y1);
		float num = NavigationTools.getLinearDistance(x1, x2, x2, y2);
		MapTile mapTile = null;
		foreach (MapTile mapTile2 in manhattanNeighbours)
		{
			if (mapTile2.isTileOpenAndPassable())
			{
				float linearDistance = NavigationTools.getLinearDistance(mapTile2.getTileX(), mapTile2.getTileY(), x2, y2);
				if (num == 0f || linearDistance < num)
				{
					num = linearDistance;
					mapTile = mapTile2;
				}
			}
		}
		if (mapTile != null)
		{
			return new Point(mapTile.getTileX() - x1, mapTile.getTileY() - y1);
		}
		return new Point(0, 0);
	}

	// Token: 0x06000EBB RID: 3771 RVA: 0x00045A6C File Offset: 0x00043C6C
	public void setClosestTileToMouseTile(Character currentCharacter)
	{
		if (currentCharacter == null)
		{
			this.nextMoveTile = null;
			return;
		}
		if (this.mouseTile != null)
		{
			float num = 10000f;
			MapTile mapTile = null;
			foreach (MapTile mapTile2 in this.getManhattanNeighbours(currentCharacter.getTileX(), currentCharacter.getTileY()))
			{
				if (mapTile2.isPassable() && (mapTile2.getLiveCharacter() == null || currentCharacter.isNPCHostile(mapTile2.getLiveCharacter())))
				{
					float linearDistance = NavigationTools.getLinearDistance(mapTile2.getTileX(), mapTile2.getTileY(), this.mouseTile.getTileX(), this.mouseTile.getTileY());
					if (linearDistance < num)
					{
						num = linearDistance;
						mapTile = mapTile2;
					}
				}
			}
			this.nextMoveTile = mapTile;
			return;
		}
		this.nextMoveTile = null;
	}

	// Token: 0x06000EBC RID: 3772 RVA: 0x00045B44 File Offset: 0x00043D44
	public void setTileMapSubimage()
	{
		for (int i = 0; i < this.getMapTileWidth(); i++)
		{
			for (int j = 0; j < this.getMapTileHeight(); j++)
			{
				this.findSubImage(i, j);
			}
		}
	}

	// Token: 0x06000EBD RID: 3773 RVA: 0x00045B7C File Offset: 0x00043D7C
	private void setNeighboursDict()
	{
		this.neighboursDict.Add(1111, 0);
		this.neighboursDict.Add(2111, 4);
		this.neighboursDict.Add(1121, 12);
		this.neighboursDict.Add(1211, 13);
		this.neighboursDict.Add(1112, 15);
		this.neighboursDict.Add(2121, 8);
		this.neighboursDict.Add(1212, 14);
		this.neighboursDict.Add(2211, 1);
		this.neighboursDict.Add(1221, 9);
		this.neighboursDict.Add(1122, 11);
		this.neighboursDict.Add(2112, 3);
		this.neighboursDict.Add(1222, 10);
		this.neighboursDict.Add(2122, 7);
		this.neighboursDict.Add(2212, 2);
		this.neighboursDict.Add(2221, 5);
		this.neighboursDict.Add(2222, 6);
	}

	// Token: 0x06000EBE RID: 3774 RVA: 0x00045CA0 File Offset: 0x00043EA0
	public List<Party> getAllNPCsByID(string npcId)
	{
		List<Party> list = new List<Party>();
		MapTile[,] array = this.tileMap;
		int upperBound = array.GetUpperBound(0);
		int upperBound2 = array.GetUpperBound(1);
		for (int i = array.GetLowerBound(0); i <= upperBound; i++)
		{
			for (int j = array.GetLowerBound(1); j <= upperBound2; j++)
			{
				Party party = array[i, j].getParty();
				if (party != null && !party.isPC() && !party.isPartyDead() && party.getCurrentCharacter().isId(npcId))
				{
					list.Add(party);
				}
			}
		}
		return list;
	}

	// Token: 0x06000EBF RID: 3775 RVA: 0x00045D38 File Offset: 0x00043F38
	public void clearAllNPCs()
	{
		MapTile[,] array = this.tileMap;
		int upperBound = array.GetUpperBound(0);
		int upperBound2 = array.GetUpperBound(1);
		for (int i = array.GetLowerBound(0); i <= upperBound; i++)
		{
			for (int j = array.GetLowerBound(1); j <= upperBound2; j++)
			{
				MapTile mapTile = array[i, j];
				if (mapTile.getParty() != null && !mapTile.getParty().isPC())
				{
					mapTile.deleteParty();
				}
				if (mapTile.getDeadParty() != null && !mapTile.getDeadParty().isPC())
				{
					mapTile.deleteDeadParty();
				}
			}
		}
	}

	// Token: 0x06000EC0 RID: 3776 RVA: 0x00045DCC File Offset: 0x00043FCC
	public void clearNearbyEnemies()
	{
		foreach (MapTile mapTile in this.getVisibleTiles())
		{
			Party party = mapTile.getParty();
			if (party != null && party.isHostile())
			{
				mapTile.deleteParty();
			}
		}
	}

	// Token: 0x06000EC1 RID: 3777 RVA: 0x00045E30 File Offset: 0x00044030
	public void clearNearbyNPCs()
	{
		foreach (MapTile mapTile in this.getVisibleTiles())
		{
			if (mapTile.getParty() != null && !mapTile.getParty().isPC())
			{
				mapTile.deleteParty();
			}
		}
	}

	// Token: 0x06000EC2 RID: 3778 RVA: 0x00045E98 File Offset: 0x00044098
	public int getViewportLeftEdge()
	{
		return this.getViewportX() - MapIllustrator.ScreenDimensions.getIllustratedMapHalfWidth();
	}

	// Token: 0x06000EC3 RID: 3779 RVA: 0x00045EA6 File Offset: 0x000440A6
	public int getViewportRightEdge()
	{
		return this.getViewportLeftEdge() + MapIllustrator.ScreenDimensions.getIllustratedMapWidth();
	}

	// Token: 0x06000EC4 RID: 3780 RVA: 0x00045EB4 File Offset: 0x000440B4
	public int getViewportBottomEdge()
	{
		return this.getViewportY() - (MapIllustrator.ScreenDimensions.getIllustratedMapHalfHeight() - 1);
	}

	// Token: 0x06000EC5 RID: 3781 RVA: 0x00045EC4 File Offset: 0x000440C4
	public int getViewportTopEdge()
	{
		return this.getViewportBottomEdge() + MapIllustrator.ScreenDimensions.getIllustratedMapHeight() - 2;
	}

	// Token: 0x06000EC6 RID: 3782 RVA: 0x00045ED4 File Offset: 0x000440D4
	public List<MapTile> getVisibleTiles()
	{
		List<MapTile> list = new List<MapTile>();
		for (int i = this.getViewportLeftEdge(); i < this.getViewportRightEdge(); i++)
		{
			for (int j = this.getViewportBottomEdge(); j < this.getViewportTopEdge(); j++)
			{
				if (this.isTileValid(i, j))
				{
					list.Add(this.getTile(i, j));
				}
			}
		}
		return list;
	}

	// Token: 0x06000EC7 RID: 3783 RVA: 0x00045F2C File Offset: 0x0004412C
	private MapTile[,] getVisibleTilesArray()
	{
		MapTile[,] array = new MapTile[MapIllustrator.ScreenDimensions.getIllustratedMapWidth(), MapIllustrator.ScreenDimensions.getIllustratedMapHeight()];
		int num = 0;
		for (int i = this.getViewportLeftEdge(); i < this.getViewportRightEdge(); i++)
		{
			int num2 = 0;
			for (int j = this.getViewportBottomEdge(); j < this.getViewportTopEdge(); j++)
			{
				if (this.isTileValid(i, j))
				{
					array[num, num2] = this.getTile(i, j);
				}
				num2++;
			}
			num++;
		}
		return array;
	}

	// Token: 0x06000EC8 RID: 3784 RVA: 0x00045FA4 File Offset: 0x000441A4
	public bool isPointInView(int x, int y, int padding)
	{
		return x >= this.getViewportLeftEdge() - padding && x < this.getViewportRightEdge() + padding && y >= this.getViewportBottomEdge() - padding && y < this.getViewportTopEdge() + padding;
	}

	// Token: 0x06000EC9 RID: 3785 RVA: 0x00045FD5 File Offset: 0x000441D5
	private int lookupNeighbourDictionary(ushort key)
	{
		if (this.neighboursDict.ContainsKey(key))
		{
			return this.neighboursDict[key];
		}
		MainControl.logError("Missing neighbour key: " + key.ToString());
		return 0;
	}

	// Token: 0x06000ECA RID: 3786 RVA: 0x0004600C File Offset: 0x0004420C
	public void findSubImage(int x, int y)
	{
		MapTile tile = this.getTile(x, y);
		if (tile == null)
		{
			return;
		}
		int tileImageListLength = tile.getTileImageListLength();
		for (int i = 0; i < tileImageListLength; i++)
		{
			string tileImageByIndex = tile.getTileImageByIndex(i);
			if (tileImageByIndex != "" && tileImageByIndex != null)
			{
				ushort num = 1111;
				if (!this.isTileValid(x, y + 1) || this.getTile(x, y + 1).getTileImageByIndex(i) == tileImageByIndex)
				{
					num += 1000;
				}
				if (!this.isTileValid(x + 1, y) || this.getTile(x + 1, y).getTileImageByIndex(i) == tileImageByIndex)
				{
					num += 100;
				}
				if (!this.isTileValid(x, y - 1) || this.getTile(x, y - 1).getTileImageByIndex(i) == tileImageByIndex)
				{
					num += 10;
				}
				if (!this.isTileValid(x - 1, y) || this.getTile(x - 1, y).getTileImageByIndex(i) == tileImageByIndex)
				{
					num += 1;
				}
				tile.setSubImage(i, (short)this.lookupNeighbourDictionary(num));
			}
		}
	}

	// Token: 0x06000ECB RID: 3787 RVA: 0x00046128 File Offset: 0x00044328
	internal void fireLaunchCombatTriggers()
	{
		foreach (MapTile mapTile in this.getVisibleTiles())
		{
			mapTile.processCombatLaunchTrigger();
		}
	}

	// Token: 0x06000ECC RID: 3788 RVA: 0x0004617C File Offset: 0x0004437C
	public int findFogSubImage(int x, int y)
	{
		if (this.getTile(x, y) == null)
		{
			return 6;
		}
		ushort num = 1111;
		MapTile topmostNonVoidMapTile = this.map.getTopmostNonVoidMapTile(x, y + 1);
		if (topmostNonVoidMapTile == null || topmostNonVoidMapTile.isSpotted())
		{
			num += 1000;
		}
		topmostNonVoidMapTile = this.map.getTopmostNonVoidMapTile(x + 1, y);
		if (topmostNonVoidMapTile == null || topmostNonVoidMapTile.isSpotted())
		{
			num += 100;
		}
		topmostNonVoidMapTile = this.map.getTopmostNonVoidMapTile(x, y - 1);
		if (topmostNonVoidMapTile == null || topmostNonVoidMapTile.isSpotted())
		{
			num += 10;
		}
		topmostNonVoidMapTile = this.map.getTopmostNonVoidMapTile(x - 1, y);
		if (topmostNonVoidMapTile == null || topmostNonVoidMapTile.isSpotted())
		{
			num += 1;
		}
		return this.lookupNeighbourDictionary(num);
	}

	// Token: 0x06000ECD RID: 3789 RVA: 0x00046228 File Offset: 0x00044428
	public int findDarknessSubImage(int x, int y)
	{
		if (this.getTile(x, y) == null)
		{
			return 6;
		}
		ushort num = 1111;
		MapTile topmostNonVoidMapTile = this.map.getTopmostNonVoidMapTile(x, y + 1);
		if (topmostNonVoidMapTile == null || topmostNonVoidMapTile.isIlluminated())
		{
			num += 1000;
		}
		topmostNonVoidMapTile = this.map.getTopmostNonVoidMapTile(x + 1, y);
		if (topmostNonVoidMapTile == null || topmostNonVoidMapTile.isIlluminated())
		{
			num += 100;
		}
		topmostNonVoidMapTile = this.map.getTopmostNonVoidMapTile(x, y - 1);
		if (topmostNonVoidMapTile == null || topmostNonVoidMapTile.isIlluminated())
		{
			num += 10;
		}
		topmostNonVoidMapTile = this.map.getTopmostNonVoidMapTile(x - 1, y);
		if (topmostNonVoidMapTile == null || topmostNonVoidMapTile.isIlluminated())
		{
			num += 1;
		}
		return this.lookupNeighbourDictionary(num);
	}

	// Token: 0x06000ECE RID: 3790 RVA: 0x000462D4 File Offset: 0x000444D4
	public bool testLineOfSight(int sourceX, int sourceY, int targetX, int targetY)
	{
		int num = targetX - sourceX;
		int num2 = targetY - sourceY;
		float num3 = (float)Mathf.Abs(num);
		float num4 = (float)Mathf.Abs(num2);
		float num5 = num3;
		int num6 = Mathf.RoundToInt(Mathf.Sign((float)num2));
		int num7 = Mathf.RoundToInt(Mathf.Sign((float)num));
		float num8 = (float)num6;
		float num9 = (float)num7;
		float num10 = (float)sourceX;
		float num11 = (float)sourceY;
		if (num3 > num4)
		{
			num5 = num3;
			num8 = (float)num6 * (num4 / num3);
		}
		else if (num3 < num4)
		{
			num5 = num4;
			num9 = (float)num7 * (num3 / num4);
		}
		int num12 = 1;
		while ((float)num12 < num5)
		{
			num10 += num9;
			num11 += num8;
			int x = Mathf.RoundToInt(num10);
			int y = Mathf.RoundToInt(num11);
			if (!this.getTile(x, y).getSeeThrough())
			{
				return false;
			}
			num12++;
		}
		return true;
	}

	// Token: 0x06000ECF RID: 3791 RVA: 0x00046390 File Offset: 0x00044590
	public bool testLineOfSightLight(int sourceX, int sourceY, int targetX, int targetY)
	{
		int num = targetX - sourceX;
		int num2 = targetY - sourceY;
		float num3 = (float)Mathf.Abs(num);
		float num4 = (float)Mathf.Abs(num2);
		float num5 = num3;
		int num6 = Mathf.RoundToInt(Mathf.Sign((float)num2));
		int num7 = Mathf.RoundToInt(Mathf.Sign((float)num));
		float num8 = (float)num6;
		float num9 = (float)num7;
		float num10 = (float)sourceX;
		float num11 = (float)sourceY;
		bool flag = false;
		if (num3 > num4)
		{
			num5 = num3;
			num8 = (float)num6 * (num4 / num3);
		}
		else if (num3 < num4)
		{
			num5 = num4;
			num9 = (float)num7 * (num3 / num4);
		}
		int num12 = 1;
		while ((float)num12 < num5)
		{
			num10 += num9;
			num11 += num8;
			int x = Mathf.RoundToInt(num10);
			int y = Mathf.RoundToInt(num11);
			if (!this.getTile(x, y).getSeeThrough())
			{
				if (flag)
				{
					flag = true;
				}
			}
			else if (flag)
			{
				return false;
			}
			num12++;
		}
		return true;
	}

	// Token: 0x06000ED0 RID: 3792 RVA: 0x00046459 File Offset: 0x00044659
	public bool isTilePassableToNPC(Party p, int x, int y)
	{
		return this.isTileValid(x, y) && this.getTile(x, y).isTilePassableToNPC(p);
	}

	// Token: 0x06000ED1 RID: 3793 RVA: 0x00046478 File Offset: 0x00044678
	public void clearInstances()
	{
		MapTile[,] array = this.tileMap;
		int upperBound = array.GetUpperBound(0);
		int upperBound2 = array.GetUpperBound(1);
		for (int i = array.GetLowerBound(0); i <= upperBound; i++)
		{
			for (int j = array.GetLowerBound(1); j <= upperBound2; j++)
			{
				array[i, j].clearInstances();
			}
		}
	}

	// Token: 0x06000ED2 RID: 3794 RVA: 0x000464D4 File Offset: 0x000446D4
	public void clearInstancesAndDelete()
	{
		MapTile[,] array = this.tileMap;
		int upperBound = array.GetUpperBound(0);
		int upperBound2 = array.GetUpperBound(1);
		for (int i = array.GetLowerBound(0); i <= upperBound; i++)
		{
			for (int j = array.GetLowerBound(1); j <= upperBound2; j++)
			{
				array[i, j].clearInstancesAndDelete();
			}
		}
	}

	// Token: 0x06000ED3 RID: 3795 RVA: 0x00046530 File Offset: 0x00044730
	public void clearFogAndLight(int x, int y)
	{
		int num = MapIllustrator.ScreenDimensions.getLongestTileMapDimension() / 2 + 5;
		bool fogRegrows = this.map.getFogRegrows();
		bool flag = MainControl.getDataControl().isCombatActive();
		bool flag2 = !this.map.shouldIDrawOverlay();
		for (int i = x - num; i < x + (num + 1); i++)
		{
			for (int j = y - num; j < y + (num + 1); j++)
			{
				if (this.isTileValid(i, j))
				{
					MapTile tile = this.getTile(i, j);
					if (!flag)
					{
						if (fogRegrows || flag2)
						{
							tile.clearSpotted();
						}
						else if (tile.isSpottedOnce())
						{
							tile.setSpotted();
						}
					}
					tile.clearIlluminated();
				}
			}
		}
		if (!fogRegrows && flag2)
		{
			foreach (MapTile mapTile in this.getConnectedIndoorTiles())
			{
				if (mapTile.isSpottedOnce())
				{
					mapTile.setSpotted();
				}
			}
		}
	}

	// Token: 0x06000ED4 RID: 3796 RVA: 0x00046634 File Offset: 0x00044834
	public void revealMap()
	{
		MapTile[,] array = this.tileMap;
		int upperBound = array.GetUpperBound(0);
		int upperBound2 = array.GetUpperBound(1);
		for (int i = array.GetLowerBound(0); i <= upperBound; i++)
		{
			for (int j = array.GetLowerBound(1); j <= upperBound2; j++)
			{
				array[i, j].setSpotted();
			}
		}
	}

	// Token: 0x06000ED5 RID: 3797 RVA: 0x0004668E File Offset: 0x0004488E
	public bool isVoidTile(int x, int y)
	{
		return this.getTile(x, y) == null || this.getTile(x, y).isVoidTile();
	}

	// Token: 0x06000ED6 RID: 3798 RVA: 0x000466AC File Offset: 0x000448AC
	public void setTilePosition(int x, int y)
	{
		if (!this.isTileValid(x, y))
		{
			return;
		}
		int num = x - this.xPos;
		int num2 = y - this.yPos;
		this.xPos = x;
		this.yPos = y;
		this.setCurrentTile(this.getTile(this.xPos, this.yPos));
		if (!this.isPointInsideViewportDeadspace(this.xPos, this.yPos))
		{
			this.setViewportPosition(this.getViewportX() + num, this.getViewportY() + num2);
		}
	}

	// Token: 0x06000ED7 RID: 3799 RVA: 0x00046726 File Offset: 0x00044926
	public void centerViewPort()
	{
		this.setViewportPosition(this.xPos, this.yPos);
		this.map.stopScroll();
	}

	// Token: 0x06000ED8 RID: 3800 RVA: 0x00046745 File Offset: 0x00044945
	public void setViewportPosition(int x, int y)
	{
		this.map.setScroll(x, y);
		this.viewportX = x;
		this.viewportY = y;
	}

	// Token: 0x06000ED9 RID: 3801 RVA: 0x00046762 File Offset: 0x00044962
	public bool isPointInsideViewportDeadspace(int x, int y)
	{
		return x > this.getViewportX() - MapTileGrid.VIEWPORT_HORIZONAL_DEADSPACE && x < this.getViewportX() + MapTileGrid.VIEWPORT_HORIZONAL_DEADSPACE && y > this.getViewportY() - MapTileGrid.VIEWPORT_VERTICAL_DEADSPACE && y < this.getViewportY() + MapTileGrid.VIEWPORT_VERTICAL_DEADSPACE;
	}

	// Token: 0x06000EDA RID: 3802 RVA: 0x000467A2 File Offset: 0x000449A2
	public int getViewportX()
	{
		return this.viewportX;
	}

	// Token: 0x06000EDB RID: 3803 RVA: 0x000467AA File Offset: 0x000449AA
	public int getViewportY()
	{
		return this.viewportY;
	}

	// Token: 0x06000EDC RID: 3804 RVA: 0x000467B2 File Offset: 0x000449B2
	public int getXPos()
	{
		return this.xPos;
	}

	// Token: 0x06000EDD RID: 3805 RVA: 0x000467BA File Offset: 0x000449BA
	public int getYPos()
	{
		return this.yPos;
	}

	// Token: 0x06000EDE RID: 3806 RVA: 0x000467C2 File Offset: 0x000449C2
	public MapTile getTargetTile()
	{
		return this.targetTile;
	}

	// Token: 0x06000EDF RID: 3807 RVA: 0x000467CA File Offset: 0x000449CA
	public MapTile getMouseTile()
	{
		return this.mouseTile;
	}

	// Token: 0x06000EE0 RID: 3808 RVA: 0x000467D2 File Offset: 0x000449D2
	public string getMouseTileDescription()
	{
		if (this.mouseTile != null)
		{
			return this.mouseTile.getInspectDescription();
		}
		return "";
	}

	// Token: 0x06000EE1 RID: 3809 RVA: 0x000467ED File Offset: 0x000449ED
	public MapTile getCurrentTile()
	{
		return this.currentTile;
	}

	// Token: 0x06000EE2 RID: 3810 RVA: 0x000467F5 File Offset: 0x000449F5
	public MapTile getNextMoveTile()
	{
		return this.nextMoveTile;
	}

	// Token: 0x06000EE3 RID: 3811 RVA: 0x000467FD File Offset: 0x000449FD
	public void setTargetTile(MapTile tile)
	{
		this.targetTile = tile;
	}

	// Token: 0x06000EE4 RID: 3812 RVA: 0x00046806 File Offset: 0x00044A06
	public void setCurrentTile(MapTile tile)
	{
		this.currentTile = tile;
	}

	// Token: 0x06000EE5 RID: 3813 RVA: 0x0004680F File Offset: 0x00044A0F
	public void setTargetTile(int x, int y)
	{
		this.setTargetTile(this.getTile(x, y));
	}

	// Token: 0x06000EE6 RID: 3814 RVA: 0x0004681F File Offset: 0x00044A1F
	public void setExamineTile(MapTile tile)
	{
		this.examineTile = tile;
	}

	// Token: 0x06000EE7 RID: 3815 RVA: 0x00046828 File Offset: 0x00044A28
	public void setExamineTile(int x, int y)
	{
		this.setExamineTile(this.getTile(x, y));
	}

	// Token: 0x06000EE8 RID: 3816 RVA: 0x00046838 File Offset: 0x00044A38
	public MapTile getExamineTile()
	{
		return this.examineTile;
	}

	// Token: 0x040003BB RID: 955
	private static int VIEWPORT_VERTICAL_DEADSPACE = 3;

	// Token: 0x040003BC RID: 956
	private static int VIEWPORT_HORIZONAL_DEADSPACE = 5;

	// Token: 0x040003BD RID: 957
	private MapTile currentTile;

	// Token: 0x040003BE RID: 958
	private MapTile targetTile;

	// Token: 0x040003BF RID: 959
	private MapTile examineTile;

	// Token: 0x040003C0 RID: 960
	private MapTile nextMoveTile;

	// Token: 0x040003C1 RID: 961
	private MapTile mouseTile;

	// Token: 0x040003C2 RID: 962
	private Map map;

	// Token: 0x040003C3 RID: 963
	private int xPos;

	// Token: 0x040003C4 RID: 964
	private int yPos;

	// Token: 0x040003C5 RID: 965
	private int viewportX;

	// Token: 0x040003C6 RID: 966
	private int viewportY;

	// Token: 0x040003C7 RID: 967
	private Dictionary<ushort, int> neighboursDict = new Dictionary<ushort, int>();

	// Token: 0x040003C8 RID: 968
	private int mapTileWidth;

	// Token: 0x040003C9 RID: 969
	private int mapTileHeight;

	// Token: 0x040003CA RID: 970
	private MapTile[,] tileMap;

	// Token: 0x040003CB RID: 971
	private List<MapTile> tiles2 = new List<MapTile>(2);
}
