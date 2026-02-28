using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

// Token: 0x020000DD RID: 221
public class Map : SkaldBaseObject
{
	// Token: 0x06000D45 RID: 3397 RVA: 0x0003CB08 File Offset: 0x0003AD08
	public Map(SKALDProjectData.Objects.MapMetaDataContainer.MapMetaData rawData)
	{
		this.setName(rawData.title);
		this.setId(rawData.id);
		this.setDescription(rawData.description);
		this.setImagePath(rawData.imagePath);
		this.startX = rawData.startX;
		this.startY = rawData.startY;
		this.fogRegrows = rawData.fogRegrows;
		this.mapAboveId = rawData.mapAboveId;
		this.mapBelowId = rawData.mapBelowId;
		this.overland = rawData.overland;
		this.wilderness = rawData.wilderness;
		this.indoors = rawData.indoors;
		this.city = rawData.city;
		this.dynamicEnc = rawData.dynamicEnc;
		this.containerMapId = rawData.containerMapId;
		this.northernEdgeMapId = rawData.northernEdgeMapId;
		this.easternEdgeMapId = rawData.easternEdgeMapId;
		this.westernEdgeMapId = rawData.westernEdgeMapId;
		this.southernEdgeMapId = rawData.southernEdgeMapId;
		this.enterTrigger = rawData.enterTrigger;
		this.firstTimeEnterTrigger = rawData.firstTimeEnterTrigger;
		this.enterPrompt = rawData.enterPrompt;
		this.leaveTrigger = rawData.leaveTrigger;
		this.firstTimeLeaveTrigger = rawData.firstTimeLeaveTrigger;
		this.drawAsCube = rawData.drawAsCube;
		this.canMakeCampHere = rawData.canRestHere;
		this.canSleepInBedHere = rawData.canRestInBedHere;
		this.canFightHere = rawData.canFightHere;
		this.deleteAllOnLeave = rawData.deleteAllOnLeave;
		this.baseAmbientLightLevel = rawData.maxLightLevel;
		this.dayNightCycleLight = rawData.dayNightCycleLight;
		this.groundIsWhite = rawData.whiteGround;
		if (rawData.combatMusicPath != "")
		{
			this.combatMusicPath = rawData.combatMusicPath;
		}
		this.faction = "";
		this.economy = "";
		MapSaveDataContainer mapSaveDataContainer = GameData.getMapData(rawData.id);
		if (mapSaveDataContainer == null)
		{
			mapSaveDataContainer = new MapSaveDataContainer(rawData.width, rawData.height, rawData.id);
		}
		this.tileGrid = new MapTileGrid(mapSaveDataContainer, this);
		this.tileGrid.setTilePosition(this.startX, this.startY);
		this.tileGrid.centerViewPort();
		this.mapIllustrator = new MapIllustrator(this, this.tileGrid);
		this.musicPath = rawData.musicPath;
		this.setPositionDirect(this.startX, this.startY);
		this.tileGrid.clearInstances();
	}

	// Token: 0x06000D46 RID: 3398 RVA: 0x0003CE44 File Offset: 0x0003B044
	public void initializeFinal()
	{
		this.mapIllustrator.makeTerrainMap();
	}

	// Token: 0x06000D47 RID: 3399 RVA: 0x0003CE54 File Offset: 0x0003B054
	public bool areOpponentsAlert()
	{
		if (this.potentiallyAlertEnemies == null || this.potentiallyAlertEnemies.Count == 0)
		{
			return false;
		}
		using (List<Party>.Enumerator enumerator = this.potentiallyAlertEnemies.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isAlert())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000D48 RID: 3400 RVA: 0x0003CEC4 File Offset: 0x0003B0C4
	public string getMusicPath()
	{
		return this.musicPath;
	}

	// Token: 0x06000D49 RID: 3401 RVA: 0x0003CECC File Offset: 0x0003B0CC
	public string getCombatMusicPath()
	{
		return this.combatMusicPath;
	}

	// Token: 0x06000D4A RID: 3402 RVA: 0x0003CED4 File Offset: 0x0003B0D4
	public bool canMakeCampOnThisMap()
	{
		return this.canMakeCampHere;
	}

	// Token: 0x06000D4B RID: 3403 RVA: 0x0003CEDC File Offset: 0x0003B0DC
	public bool canSleepInBedOnMap()
	{
		return this.canSleepInBedHere;
	}

	// Token: 0x06000D4C RID: 3404 RVA: 0x0003CEE4 File Offset: 0x0003B0E4
	public bool isGroundWhite()
	{
		return this.groundIsWhite;
	}

	// Token: 0x06000D4D RID: 3405 RVA: 0x0003CEEC File Offset: 0x0003B0EC
	public MapTile getTileAtRelativeLocalPos(Vector2 pos)
	{
		return this.tileGrid.getTileAtRelativeLocalPos(pos);
	}

	// Token: 0x06000D4E RID: 3406 RVA: 0x0003CEFA File Offset: 0x0003B0FA
	public void setMouseTile(Vector2 pos)
	{
		this.tileGrid.setMouseTile(pos);
	}

	// Token: 0x06000D4F RID: 3407 RVA: 0x0003CF08 File Offset: 0x0003B108
	public bool isPartyOnEdgeTile()
	{
		return this.playerParty.getTileX() == 0 || this.playerParty.getTileX() == this.tileGrid.getMapTileWidth() - 1 || this.playerParty.getTileY() == 0 || this.playerParty.getTileY() == this.tileGrid.getMapTileHeight() - 1;
	}

	// Token: 0x06000D50 RID: 3408 RVA: 0x0003CF65 File Offset: 0x0003B165
	public MapTile getMouseTile()
	{
		return this.tileGrid.getMouseTile();
	}

	// Token: 0x06000D51 RID: 3409 RVA: 0x0003CF72 File Offset: 0x0003B172
	public void setClosestTileToMouseTile(Character currentCharacter)
	{
		this.tileGrid.setClosestTileToMouseTile(currentCharacter);
	}

	// Token: 0x06000D52 RID: 3410 RVA: 0x0003CF80 File Offset: 0x0003B180
	public bool isScrollReady()
	{
		return this.mapIllustrator.isScrollReady();
	}

	// Token: 0x06000D53 RID: 3411 RVA: 0x0003CF92 File Offset: 0x0003B192
	public bool isMapReady()
	{
		return this.isScrollReady() && this.mapIllustrator.getGenericEffectsControl().isCurtainReady();
	}

	// Token: 0x06000D54 RID: 3412 RVA: 0x0003CFB1 File Offset: 0x0003B1B1
	public void setPosition()
	{
		this.setPosition(this.tileGrid.getXPos(), this.tileGrid.getYPos());
	}

	// Token: 0x06000D55 RID: 3413 RVA: 0x0003CFD0 File Offset: 0x0003B1D0
	public void setPositionTeleport(int x, int y)
	{
		if (!this.isTileValid(x, y))
		{
			return;
		}
		MapTile mapTile = this.getTile(x, y);
		if (mapTile == null || !mapTile.isPassable())
		{
			mapTile = this.tileGrid.getClosestOpenTile(x, y);
		}
		if (mapTile == null)
		{
			MainControl.logError("Could not find a potential tile at position " + x.ToString() + " / " + y.ToString());
		}
		else
		{
			x = mapTile.getTileX();
			y = mapTile.getTileY();
		}
		this.setPositionDirect(x, y);
		this.tileGrid.centerViewPort();
		if (this.playerParty != null)
		{
			this.playerParty.snapToGrid();
		}
		this.updateDrawLogic();
		this.updateDrawLogic();
	}

	// Token: 0x06000D56 RID: 3414 RVA: 0x0003D074 File Offset: 0x0003B274
	public void updateDrawLogic()
	{
		this.setDrawOverlay();
		this.mapIllustrator.cacheFogOfWarOverlay();
		this.clearFogAndLight(this.getXPos(), this.getYPos());
		this.updateViewshed();
		this.updateLightmap();
		this.mapIllustrator.makeTerrainMap();
		this.mapIllustrator.makeFogOfWarOverlay();
	}

	// Token: 0x06000D57 RID: 3415 RVA: 0x0003D0C6 File Offset: 0x0003B2C6
	public int getViewportX()
	{
		return this.tileGrid.getViewportX();
	}

	// Token: 0x06000D58 RID: 3416 RVA: 0x0003D0D3 File Offset: 0x0003B2D3
	public int getViewportY()
	{
		return this.tileGrid.getViewportY();
	}

	// Token: 0x06000D59 RID: 3417 RVA: 0x0003D0E0 File Offset: 0x0003B2E0
	public bool canLaunchCombatHere()
	{
		return !this.overland && this.canFightHere;
	}

	// Token: 0x06000D5A RID: 3418 RVA: 0x0003D0F4 File Offset: 0x0003B2F4
	public float getAmbientLightLevel()
	{
		if (this.dayNightCycleLight)
		{
			if (Calendar.isItDay())
			{
				return this.baseAmbientLightLevel;
			}
			if (Calendar.isItNight())
			{
				return 0f;
			}
			if (Calendar.isItDawn())
			{
				return this.baseAmbientLightLevel * Calendar.getHourInMinuteProgression();
			}
			if (Calendar.isItDusk())
			{
				return this.baseAmbientLightLevel - this.baseAmbientLightLevel * Calendar.getHourInMinuteProgression();
			}
		}
		return this.baseAmbientLightLevel;
	}

	// Token: 0x06000D5B RID: 3419 RVA: 0x0003D159 File Offset: 0x0003B359
	public void clearFogAndLight(int x, int y)
	{
		this.tileGrid.clearFogAndLight(x, y);
		if (this.getMapAboveForDrawing() != null)
		{
			this.getMapAboveForDrawing().clearFogAndLight(x, y);
		}
	}

	// Token: 0x06000D5C RID: 3420 RVA: 0x0003D17D File Offset: 0x0003B37D
	public bool isPointInView(int x, int y, int padding)
	{
		return this.tileGrid.isPointInView(x, y, padding);
	}

	// Token: 0x06000D5D RID: 3421 RVA: 0x0003D190 File Offset: 0x0003B390
	public List<MapTile> getAccessibleTilesFromParty()
	{
		if (this.cachedAccessibleTiles == null)
		{
			this.cachedAccessibleTiles = new List<MapTile>();
		}
		else
		{
			foreach (MapTile mapTile in this.cachedAccessibleTiles)
			{
				mapTile.clearCombatAccessible();
			}
			this.cachedAccessibleTiles.Clear();
		}
		this.cachedAccessibleTiles = this.tileGrid.getAccessibleTilesFromParty();
		foreach (MapTile mapTile2 in this.cachedAccessibleTiles)
		{
			mapTile2.tagAsCombatAccessible();
		}
		return this.cachedAccessibleTiles;
	}

	// Token: 0x06000D5E RID: 3422 RVA: 0x0003D258 File Offset: 0x0003B458
	public List<MapTile> getAccessibleTilesFromPartyForCombatGrid()
	{
		if (this.cachedAccessibleTiles == null || this.cachedAccessibleTiles.Count == 0)
		{
			this.getAccessibleTilesFromParty();
		}
		return this.cachedAccessibleTiles;
	}

	// Token: 0x06000D5F RID: 3423 RVA: 0x0003D27C File Offset: 0x0003B47C
	public void deployParty()
	{
		if (this.playerParty == null)
		{
			return;
		}
		Party closestEnemy = this.getClosestEnemy();
		if (closestEnemy != null)
		{
			this.playerParty.facePoint(closestEnemy.getTileX(), closestEnemy.getTileY());
		}
		List<Point> list = new List<Point>();
		List<MapTile> accessibleTilesFromParty = this.getAccessibleTilesFromParty();
		for (int i = 0; i < this.playerParty.getCount(); i++)
		{
			list.Add(this.playerParty.getIdealCharacterPlacement(i));
		}
		this.getTile(this.playerParty.getTileX(), this.playerParty.getTileY()).clearParty();
		for (int j = 0; j < this.playerParty.getCount(); j++)
		{
			Point point = list[j];
			Character c = this.playerParty.getObjectByIndex(j) as Character;
			bool flag = this.attemptToPlaceCharacterCloseToPoint(point.X, point.Y, c, accessibleTilesFromParty);
			if (!flag)
			{
				flag = this.attemptToPlaceCharacterCloseToPoint(this.playerParty.getTileX(), this.playerParty.getTileY(), c, accessibleTilesFromParty);
			}
			if (!flag)
			{
				MainControl.logError("Could not deploy party properly");
			}
		}
		this.setPreCombatPlacementTiles(accessibleTilesFromParty);
	}

	// Token: 0x06000D60 RID: 3424 RVA: 0x0003D394 File Offset: 0x0003B594
	public void fetchOpponentsJustOutsideCombat()
	{
		this.tileGrid.fetchCharacterJustOutsideCombat(this.nearByEnemies);
		this.getAllVisibleNPCs(true);
		this.updateDrawLogic();
	}

	// Token: 0x06000D61 RID: 3425 RVA: 0x0003D3B4 File Offset: 0x0003B5B4
	public void setPreCombatPlacementTiles(List<MapTile> legalTiles)
	{
		this.preCombatPlacementTiles = new List<MapTile>();
		foreach (SkaldBaseObject skaldBaseObject in this.playerParty.getObjectList())
		{
			Character character = (Character)skaldBaseObject;
			foreach (MapTile mapTile in this.tileGrid.getSquareAroundPoint(character.getTileX(), character.getTileY(), 2))
			{
				if (this.isTilePassableInCombat(mapTile) && mapTile.isSpotted() && !this.preCombatPlacementTiles.Contains(mapTile) && legalTiles.Contains(mapTile))
				{
					this.preCombatPlacementTiles.Add(mapTile);
				}
			}
		}
		foreach (Party party in this.nearByEnemies)
		{
			Character currentCharacter = party.getCurrentCharacter();
			if (currentCharacter != null)
			{
				foreach (MapTile item in this.tileGrid.getSquareAroundPoint(currentCharacter.getTileX(), currentCharacter.getTileY(), 1))
				{
					if (this.preCombatPlacementTiles.Contains(item))
					{
						this.preCombatPlacementTiles.Remove(item);
					}
				}
			}
		}
		foreach (SkaldBaseObject skaldBaseObject2 in this.playerParty.getObjectList())
		{
			MapTile mapTile2 = ((Character)skaldBaseObject2).getMapTile();
			if (this.isTilePassableInCombat(mapTile2) && !this.preCombatPlacementTiles.Contains(mapTile2) && mapTile2.isSpotted())
			{
				this.preCombatPlacementTiles.Add(mapTile2);
			}
		}
	}

	// Token: 0x06000D62 RID: 3426 RVA: 0x0003D5CC File Offset: 0x0003B7CC
	public void clearPreCombatPlacementTiles()
	{
		this.preCombatPlacementTiles = null;
	}

	// Token: 0x06000D63 RID: 3427 RVA: 0x0003D5D5 File Offset: 0x0003B7D5
	public List<MapTile> getPreCombatPlacementTiles()
	{
		return this.preCombatPlacementTiles;
	}

	// Token: 0x06000D64 RID: 3428 RVA: 0x0003D5E0 File Offset: 0x0003B7E0
	public void gatherParty()
	{
		if (this.playerParty == null)
		{
			return;
		}
		foreach (Character character in this.playerParty.getPartyList())
		{
			MapTile tile = this.getTile(character.getTileX(), character.getTileY());
			if (tile != null)
			{
				if (tile.getParty() != null && tile.getParty().isPC())
				{
					tile.clearParty();
				}
				if (tile.getDeadParty() != null && tile.getDeadParty().isPC())
				{
					tile.deleteDeadParty();
				}
			}
		}
		this.getTile(this.getXPos(), this.getYPos()).setParty(this.playerParty);
		this.playerParty.snapToGrid();
		this.updateDrawLogic();
	}

	// Token: 0x06000D65 RID: 3429 RVA: 0x0003D6B4 File Offset: 0x0003B8B4
	public TextureTools.TextureData getTerrainTileImage(int x, int y)
	{
		return this.mapIllustrator.getTerrainTileImage(x, y);
	}

	// Token: 0x06000D66 RID: 3430 RVA: 0x0003D6C3 File Offset: 0x0003B8C3
	public void setScroll(int x, int y)
	{
		if (this.mapIllustrator != null)
		{
			this.mapIllustrator.setScroll(x, y);
		}
	}

	// Token: 0x06000D67 RID: 3431 RVA: 0x0003D6DA File Offset: 0x0003B8DA
	public void stopScroll()
	{
		if (this.mapIllustrator != null)
		{
			this.mapIllustrator.stopScroll();
		}
	}

	// Token: 0x06000D68 RID: 3432 RVA: 0x0003D6EF File Offset: 0x0003B8EF
	public void setPosition(int x, int y)
	{
		if (!this.isTileValid(x, y))
		{
			return;
		}
		this.setPositionDirect(x, y);
		this.moveNPC();
		this.updateDrawLogic();
		this.getAllVisibleNPCs(true);
	}

	// Token: 0x06000D69 RID: 3433 RVA: 0x0003D718 File Offset: 0x0003B918
	public void centerMapOnCombat()
	{
		this.revealNearbyNPCs();
		int tileX = this.playerParty.getTileX();
		int tileX2 = this.playerParty.getTileX();
		int tileY = this.playerParty.getTileY();
		int tileY2 = this.playerParty.getTileY();
		int viewportX = this.tileGrid.getViewportX();
		int viewportY = this.tileGrid.getViewportY();
		foreach (Party party in this.nearByEnemies)
		{
			if (party.getTileX() < tileX)
			{
				tileX = party.getTileX();
			}
			else if (party.getTileX() > tileX2)
			{
				tileX2 = party.getTileX();
			}
			if (party.getTileY() < tileY)
			{
				tileY = party.getTileY();
			}
			else if (party.getTileY() > tileY2)
			{
				tileY2 = party.getTileY();
			}
		}
		this.tileGrid.setViewportPosition(Mathf.RoundToInt((float)((tileX + tileX2) / 2)), Mathf.RoundToInt((float)((tileY + tileY2) / 2)));
		foreach (MapTile mapTile in this.getAccessibleTilesFromParty())
		{
			Party party2 = mapTile.getParty();
			if (party2 != null && !party2.isEmpty() && party2.isHostile() && !party2.isPartyDead())
			{
				this.stopScroll();
				this.updateDrawLogic();
				this.getAllVisibleNPCs(false);
				return;
			}
		}
		this.tileGrid.setViewportPosition(viewportX, viewportY);
	}

	// Token: 0x06000D6A RID: 3434 RVA: 0x0003D8A8 File Offset: 0x0003BAA8
	private void setPositionDirect(int x, int y)
	{
		if (!this.isTileValid(x, y))
		{
			return;
		}
		this.tileGrid.setTilePosition(x, y);
		if (this.playerParty == null)
		{
			return;
		}
		MapTile tile = this.getTile(this.playerParty.getTileX(), this.playerParty.getTileY());
		if (tile != null)
		{
			tile.clearParty();
		}
		this.getTile(x, y).setParty(this.playerParty);
	}

	// Token: 0x06000D6B RID: 3435 RVA: 0x0003D910 File Offset: 0x0003BB10
	public Map getMapAboveForDrawing()
	{
		if (!this.drawAsCube)
		{
			return null;
		}
		if (this.mapAbove == null)
		{
			if (this.mapAboveId == null || this.mapAboveId == "")
			{
				return null;
			}
			this.mapAbove = GameData.getMap(this.mapAboveId, this.getId());
		}
		return this.mapAbove;
	}

	// Token: 0x06000D6C RID: 3436 RVA: 0x0003D968 File Offset: 0x0003BB68
	public Map getMapBelow()
	{
		if (!this.drawAsCube)
		{
			return null;
		}
		if (this.mapBelow == null)
		{
			if (this.mapBelowId == null || this.mapBelowId == "")
			{
				return null;
			}
			this.mapBelow = GameData.getMap(this.mapBelowId, this.getId());
		}
		return this.mapBelow;
	}

	// Token: 0x06000D6D RID: 3437 RVA: 0x0003D9C0 File Offset: 0x0003BBC0
	public string setNPCMoveMode(string npcId, string moveModeId)
	{
		Character.MoveMode moveMode;
		try
		{
			moveMode = (Character.MoveMode)Enum.Parse(typeof(Character.MoveMode), moveModeId);
		}
		catch (Exception ex)
		{
			MainControl.logError(ex.ToString());
			return "Could not set movemode: " + moveModeId;
		}
		List<Party> allNPCsByID = this.tileGrid.getAllNPCsByID(npcId);
		foreach (Party party in allNPCsByID)
		{
			party.setMoveMode(moveMode);
		}
		return string.Concat(new string[]
		{
			"Set move mode ",
			moveModeId,
			" for ",
			allNPCsByID.Count.ToString(),
			" NPCs"
		});
	}

	// Token: 0x06000D6E RID: 3438 RVA: 0x0003DA90 File Offset: 0x0003BC90
	public void clearAllNPCs()
	{
		this.tileGrid.clearAllNPCs();
		this.clearNPCLists();
	}

	// Token: 0x06000D6F RID: 3439 RVA: 0x0003DAA4 File Offset: 0x0003BCA4
	public void clearAllNPCsByFaction(string factionId)
	{
		foreach (SkaldWorldObject skaldWorldObject in GameData.getCharacterByMap(this.getId()))
		{
			Character character = (Character)skaldWorldObject;
			if (!character.isPC() && character.isFactionMember(factionId))
			{
				MapTile mapTile = character.getMapTile();
				character.clearTilePosition();
				if (mapTile != null)
				{
					if (mapTile.getParty() != null)
					{
						mapTile.deleteParty();
					}
					if (mapTile.getDeadParty() != null)
					{
						mapTile.deleteDeadParty();
					}
				}
			}
		}
	}

	// Token: 0x06000D70 RID: 3440 RVA: 0x0003DB38 File Offset: 0x0003BD38
	public void clearAllNPCsById(string npcId)
	{
		foreach (SkaldWorldObject skaldWorldObject in GameData.getCharacterByMap(this.getId()))
		{
			Character character = (Character)skaldWorldObject;
			if (character.isId(npcId))
			{
				MapTile mapTile = character.getMapTile();
				character.clearTilePosition();
				if (mapTile != null)
				{
					if (mapTile.getParty() != null)
					{
						mapTile.deleteParty();
					}
					if (mapTile.getDeadParty() != null)
					{
						mapTile.deleteDeadParty();
					}
				}
			}
		}
	}

	// Token: 0x06000D71 RID: 3441 RVA: 0x0003DBC4 File Offset: 0x0003BDC4
	public void clearNearbyNPCs()
	{
		this.tileGrid.clearNearbyNPCs();
	}

	// Token: 0x06000D72 RID: 3442 RVA: 0x0003DBD1 File Offset: 0x0003BDD1
	public void clearNearbyEnemies()
	{
		this.tileGrid.clearNearbyEnemies();
	}

	// Token: 0x06000D73 RID: 3443 RVA: 0x0003DBDE File Offset: 0x0003BDDE
	public MapTile getTargetTile()
	{
		return this.tileGrid.getTargetTile();
	}

	// Token: 0x06000D74 RID: 3444 RVA: 0x0003DBEB File Offset: 0x0003BDEB
	public MapTile getCurrentTile()
	{
		return this.tileGrid.getCurrentTile();
	}

	// Token: 0x06000D75 RID: 3445 RVA: 0x0003DBF8 File Offset: 0x0003BDF8
	public MapTile getNextMoveTile()
	{
		return this.tileGrid.getNextMoveTile();
	}

	// Token: 0x06000D76 RID: 3446 RVA: 0x0003DC08 File Offset: 0x0003BE08
	public void getMouseTileDescriptionToolTip()
	{
		string mouseTileDescription = this.tileGrid.getMouseTileDescription();
		if (mouseTileDescription != "")
		{
			ToolTipPrinter.setToolTipWithRules(mouseTileDescription);
		}
	}

	// Token: 0x06000D77 RID: 3447 RVA: 0x0003DC34 File Offset: 0x0003BE34
	public SkaldPoint2D getPixelOffsetForNearbyCharacters(int tileX, int tileY)
	{
		SkaldPoint2D skaldPoint2D = new SkaldPoint2D(0, 0);
		int num = 2;
		if (this.isTileValid(tileX - 1, tileY) && !this.getTile(tileX - 1, tileY).isTileOpenAndPassable())
		{
			skaldPoint2D.X += num;
		}
		if (this.isTileValid(tileX + 1, tileY) && !this.getTile(tileX + 1, tileY).isTileOpenAndPassable())
		{
			skaldPoint2D.X -= num;
		}
		if (this.isTileValid(tileX, tileY + 1) && !this.getTile(tileX, tileY + 1).isTileOpenAndPassable())
		{
			skaldPoint2D.Y -= num;
		}
		if (this.isTileValid(tileX, tileY - 1) && !this.getTile(tileX, tileY - 1).isTileOpenAndPassable())
		{
			skaldPoint2D.Y += num;
		}
		return skaldPoint2D;
	}

	// Token: 0x06000D78 RID: 3448 RVA: 0x0003DCF8 File Offset: 0x0003BEF8
	public void setTargetTile(MapTile tile)
	{
		this.tileGrid.setTargetTile(tile);
	}

	// Token: 0x06000D79 RID: 3449 RVA: 0x0003DD06 File Offset: 0x0003BF06
	public void setTargetTile(int x, int y)
	{
		this.tileGrid.setTargetTile(x, y);
	}

	// Token: 0x06000D7A RID: 3450 RVA: 0x0003DD15 File Offset: 0x0003BF15
	public void setExamineTile(int x, int y)
	{
		this.tileGrid.setExamineTile(x, y);
	}

	// Token: 0x06000D7B RID: 3451 RVA: 0x0003DD24 File Offset: 0x0003BF24
	public MapTile getExamineTile()
	{
		return this.tileGrid.getExamineTile();
	}

	// Token: 0x06000D7C RID: 3452 RVA: 0x0003DD31 File Offset: 0x0003BF31
	public Character getBestCombatTarget(Character character, List<Character> opponents)
	{
		return this.tileGrid.getBestCombatTarget(character, opponents);
	}

	// Token: 0x06000D7D RID: 3453 RVA: 0x0003DD40 File Offset: 0x0003BF40
	public Point stepTowardsTarget(int startX, int startY, int targetX, int targetY)
	{
		return this.tileGrid.stepTowardsTarget(startX, startY, targetX, targetY);
	}

	// Token: 0x06000D7E RID: 3454 RVA: 0x0003DD52 File Offset: 0x0003BF52
	public void getFlanking(Character character)
	{
		this.tileGrid.getFlanking(character);
	}

	// Token: 0x06000D7F RID: 3455 RVA: 0x0003DD60 File Offset: 0x0003BF60
	public void getNearbyAllyCount(Character character)
	{
		this.tileGrid.getNearbyAllyCount(character);
	}

	// Token: 0x06000D80 RID: 3456 RVA: 0x0003DD6E File Offset: 0x0003BF6E
	public void getInMelee(Character character)
	{
		this.tileGrid.getInMelee(character);
	}

	// Token: 0x06000D81 RID: 3457 RVA: 0x0003DD7C File Offset: 0x0003BF7C
	public bool getFogRegrows()
	{
		return this.fogRegrows;
	}

	// Token: 0x06000D82 RID: 3458 RVA: 0x0003DD84 File Offset: 0x0003BF84
	public void revealMap()
	{
		this.fogRegrows = false;
		this.tileGrid.revealMap();
		if (this.getMapAboveForDrawing() != null)
		{
			this.getMapAboveForDrawing().revealMap();
		}
		this.mapIllustrator.resetTextureBuffer();
		this.mapIllustrator.makeTerrainMap();
	}

	// Token: 0x06000D83 RID: 3459 RVA: 0x0003DDC1 File Offset: 0x0003BFC1
	public bool isTileValid(int x, int y)
	{
		return this.tileGrid.isTileValid(x, y);
	}

	// Token: 0x06000D84 RID: 3460 RVA: 0x0003DDD0 File Offset: 0x0003BFD0
	public bool shouldIDrawOverlay()
	{
		return this.drawOverlay;
	}

	// Token: 0x06000D85 RID: 3461 RVA: 0x0003DDD8 File Offset: 0x0003BFD8
	private void setDrawOverlay()
	{
		if (MainControl.getDataControl() != null && MainControl.getDataControl().isCombatActive())
		{
			CombatEncounter combatEncounter = MainControl.getDataControl().getCombatEncounter();
			foreach (Character character in combatEncounter.getPlayerParty().getLiveCharacters())
			{
				if (this.isTileCoveredByOverlay(character.getTileX(), character.getTileY()))
				{
					this.drawOverlay = false;
					return;
				}
			}
			foreach (Character character2 in combatEncounter.getOpponentParty().getLiveCharacters())
			{
				if (this.isTileCoveredByOverlay(character2.getTileX(), character2.getTileY()))
				{
					this.drawOverlay = false;
					return;
				}
			}
		}
		if (!this.isTileCoveredByOverlay(this.getXPos(), this.getYPos()))
		{
			this.drawOverlay = true;
			return;
		}
		if (this.getTileAbove() == null || this.getTileAbove().isForceOutside())
		{
			this.drawOverlay = true;
			return;
		}
		this.drawOverlay = false;
	}

	// Token: 0x06000D86 RID: 3462 RVA: 0x0003DF0C File Offset: 0x0003C10C
	public MapTile getTileAbove()
	{
		return this.getTileAbove(this.getXPos(), this.getYPos());
	}

	// Token: 0x06000D87 RID: 3463 RVA: 0x0003DF20 File Offset: 0x0003C120
	public bool isTileAboveForceOutside(int x, int y)
	{
		MapTile tileAbove = this.getTileAbove(x, y);
		return tileAbove != null && tileAbove.isForceOutside();
	}

	// Token: 0x06000D88 RID: 3464 RVA: 0x0003DF44 File Offset: 0x0003C144
	public MapTile getTopmostNonVoidMapTile(int x, int y)
	{
		MapTile mapTile = null;
		if (!this.shouldIDrawOverlay())
		{
			return this.getTile(x, y);
		}
		if (this.getMapAboveForDrawing() != null)
		{
			mapTile = this.getMapAboveForDrawing().getTopmostNonVoidMapTile(x, y);
		}
		if (mapTile == null)
		{
			mapTile = this.getTile(x, y);
		}
		if (mapTile != null && !mapTile.isVoidTile())
		{
			return mapTile;
		}
		return null;
	}

	// Token: 0x06000D89 RID: 3465 RVA: 0x0003DF94 File Offset: 0x0003C194
	public MapTile getTileAbove(int x, int y)
	{
		if (this.getMapAboveForDrawing() == null)
		{
			return null;
		}
		return this.getMapAboveForDrawing().getTile(x, y);
	}

	// Token: 0x06000D8A RID: 3466 RVA: 0x0003DFAD File Offset: 0x0003C1AD
	public float getLightLevelForTile(int x, int y)
	{
		if (!this.isTileValid(x, y))
		{
			return 1f;
		}
		if (this.shouldIDrawOverlay() && this.isTileCoveredByOverlay(x, y))
		{
			return this.getTileAbove(x, y).getLightLevel();
		}
		return this.getTile(x, y).getLightLevel();
	}

	// Token: 0x06000D8B RID: 3467 RVA: 0x0003DFEC File Offset: 0x0003C1EC
	public bool isTileCoveredByOverlay(int x, int y)
	{
		return this.getMapAboveForDrawing() != null && !this.getMapAboveForDrawing().isVoidTile(x, y);
	}

	// Token: 0x06000D8C RID: 3468 RVA: 0x0003E008 File Offset: 0x0003C208
	public bool isVoidTile(int x, int y)
	{
		return this.tileGrid.isVoidTile(x, y);
	}

	// Token: 0x06000D8D RID: 3469 RVA: 0x0003E017 File Offset: 0x0003C217
	public MapTile getTile(int x, int y)
	{
		return this.tileGrid.getTile(x, y);
	}

	// Token: 0x06000D8E RID: 3470 RVA: 0x0003E026 File Offset: 0x0003C226
	public int getTravelTimeInSeconds()
	{
		if (this.overland)
		{
			return 900;
		}
		return 6;
	}

	// Token: 0x06000D8F RID: 3471 RVA: 0x0003E037 File Offset: 0x0003C237
	public UIMap getIllustratedOutputMap()
	{
		return this.getIllustratedOutputMap(null, false, false, false);
	}

	// Token: 0x06000D90 RID: 3472 RVA: 0x0003E043 File Offset: 0x0003C243
	public UIMap getIllustratedOutputMap(Character currentCharacter, bool combatHighlights, bool overlandHighlights, bool highlightAllCharacters)
	{
		if (currentCharacter == null)
		{
			currentCharacter = this.playerParty.getCurrentCharacter();
		}
		return this.mapIllustrator.applyMapOverlay(currentCharacter, combatHighlights, overlandHighlights, highlightAllCharacters);
	}

	// Token: 0x06000D91 RID: 3473 RVA: 0x0003E068 File Offset: 0x0003C268
	public void alertNearbyEnemies()
	{
		foreach (Party party in this.nearByEnemies)
		{
			if (!party.isAlert())
			{
				party.setAlert();
				if (!this.potentiallyAlertEnemies.Contains(party))
				{
					this.potentiallyAlertEnemies.Add(party);
				}
			}
		}
	}

	// Token: 0x06000D92 RID: 3474 RVA: 0x0003E0DC File Offset: 0x0003C2DC
	public void alertNearbyFriendlies()
	{
		foreach (Party party in this.nearByFriendlies)
		{
			party.setAlert();
		}
	}

	// Token: 0x06000D93 RID: 3475 RVA: 0x0003E12C File Offset: 0x0003C32C
	public void alertNearbyAll()
	{
		this.alertNearbyEnemies();
		this.alertNearbyFriendlies();
	}

	// Token: 0x06000D94 RID: 3476 RVA: 0x0003E13C File Offset: 0x0003C33C
	public bool isTilePassableInCombat(int x, int y)
	{
		MapTile tile = this.getTile(x, y);
		return this.isTilePassableInCombat(tile);
	}

	// Token: 0x06000D95 RID: 3477 RVA: 0x0003E159 File Offset: 0x0003C359
	public bool isTilePassableInCombat(MapTile tile)
	{
		return tile != null && tile.isPassable() && !tile.isWater() && this.isPointInView(tile.getTileX(), tile.getTileY(), 0);
	}

	// Token: 0x06000D96 RID: 3478 RVA: 0x0003E188 File Offset: 0x0003C388
	internal void facePointAll(int x)
	{
		foreach (MapTile mapTile in this.tileGrid.getVisibleTiles())
		{
			if (mapTile.getParty() != null && !mapTile.getParty().isPC())
			{
				mapTile.getParty().turnToPoint(x);
			}
		}
	}

	// Token: 0x06000D97 RID: 3479 RVA: 0x0003E1FC File Offset: 0x0003C3FC
	internal void spotAll()
	{
		foreach (MapTile mapTile in this.tileGrid.getVisibleTiles())
		{
			if (mapTile.getParty() != null && !mapTile.getParty().isPC())
			{
				mapTile.getParty().setSpotted();
			}
		}
	}

	// Token: 0x06000D98 RID: 3480 RVA: 0x0003E270 File Offset: 0x0003C470
	public void makeNearbyNPCsHostile()
	{
		foreach (MapTile mapTile in this.tileGrid.getVisibleTiles())
		{
			if (mapTile.getParty() != null && !mapTile.getParty().isPC() && !mapTile.getParty().isPC())
			{
				mapTile.getParty().setAlert();
				mapTile.getParty().setHostile(true);
			}
		}
	}

	// Token: 0x06000D99 RID: 3481 RVA: 0x0003E2FC File Offset: 0x0003C4FC
	public List<MapTile> getVisibleTiles()
	{
		return this.tileGrid.getVisibleTiles();
	}

	// Token: 0x06000D9A RID: 3482 RVA: 0x0003E30C File Offset: 0x0003C50C
	public bool attemptToPlaceCharacterCloseToParty(Character c)
	{
		int xpos = this.getXPos();
		int ypos = this.getYPos();
		return this.attemptToPlaceCharacterCloseToPoint(xpos, ypos, c, null);
	}

	// Token: 0x06000D9B RID: 3483 RVA: 0x0003E331 File Offset: 0x0003C531
	public bool attemptToPlaceCharacterCloseToPoint(int x, int y, Character c, List<MapTile> legalTiles)
	{
		return this.tileGrid.attemptToPlaceCharacterCloseToPoint(x, y, c, legalTiles);
	}

	// Token: 0x06000D9C RID: 3484 RVA: 0x0003E344 File Offset: 0x0003C544
	public bool isCharacterNearby(string npcId)
	{
		foreach (Party party in this.nearByEnemies)
		{
			if (!party.getCurrentCharacter().isDead() && party.getCurrentCharacter().isId(npcId))
			{
				return true;
			}
		}
		foreach (Party party2 in this.nearByFriendlies)
		{
			if (!party2.getCurrentCharacter().isDead() && party2.getCurrentCharacter().isId(npcId))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000D9D RID: 3485 RVA: 0x0003E40C File Offset: 0x0003C60C
	private void moveNPC()
	{
		int num = this.tileGrid.getXPos() - 1 - MapIllustrator.ScreenDimensions.getIllustratedMapHalfWidth();
		List<Party> list = new List<Party>();
		for (int i = 0; i < MapIllustrator.ScreenDimensions.getIllustratedMapWidthAndRim(); i++)
		{
			int num2 = this.tileGrid.getYPos() - 1 - MapIllustrator.ScreenDimensions.getIllustratedMapHalfHeight();
			int j = 0;
			while (j < MapIllustrator.ScreenDimensions.getIllustratedMapHeightAndRim())
			{
				if (!this.isTileValid(num, num2))
				{
					goto IL_18C;
				}
				MapTile tile = this.getTile(num, num2);
				Party party = tile.getParty();
				if (party == null || party == this.playerParty || list.Contains(party))
				{
					goto IL_18C;
				}
				list.Add(party);
				if (party.isPartyDead() || party.isEmpty())
				{
					tile.clearParty();
				}
				else
				{
					if (NavigationTools.getLinearDistance(this.tileGrid.getXPos(), this.tileGrid.getYPos(), num, num2) < 2f)
					{
						if (party.shouldTurnToFacePC())
						{
							party.turnToPoint(this.tileGrid.getXPos());
						}
						party.fireApproachTrigger();
					}
					party.updateMoveMode();
					Character.MoveMode moveMode = party.getMoveMode();
					if (moveMode == Character.MoveMode.None)
					{
						goto IL_18C;
					}
					if (moveMode == Character.MoveMode.Home && party.isAlert())
					{
						this.moveTowardsPC(party, tile, num, num2);
						goto IL_18C;
					}
					if (moveMode == Character.MoveMode.Roam)
					{
						this.moveRandom(party, tile, num, num2);
						goto IL_18C;
					}
					if (moveMode == Character.MoveMode.PatrolEW)
					{
						this.moveNPCPatrolEW(party, tile, num, num2);
						goto IL_18C;
					}
					if (moveMode == Character.MoveMode.PatrolNS)
					{
						this.moveNPCPatrolNS(party, tile, num, num2);
						goto IL_18C;
					}
					if (moveMode == Character.MoveMode.RoamIfAlert && party.isAlert())
					{
						this.moveRandom(party, tile, num, num2);
						goto IL_18C;
					}
					if (moveMode == Character.MoveMode.Flee && party.isAlert())
					{
						this.moveRandom(party, tile, num, num2);
						goto IL_18C;
					}
					goto IL_18C;
				}
				IL_190:
				j++;
				continue;
				IL_18C:
				num2++;
				goto IL_190;
			}
			num++;
		}
	}

	// Token: 0x06000D9E RID: 3486 RVA: 0x0003E5D0 File Offset: 0x0003C7D0
	private void moveNPCPatrolEW(Party party, MapTile currentTile, int testX, int testY)
	{
		if (party.getFacing() == 0 || party.getFacing() == 2)
		{
			party.setFacing(1);
		}
		if (party.getFacing() == 1 && !this.isLocationUseableForParty(party, testX + 1, testY))
		{
			party.setFacing(3);
		}
		if (party.getFacing() == 3 && !this.isLocationUseableForParty(party, testX - 1, testY))
		{
			party.setFacing(1);
		}
		this.moveNPCTowardsFacing(party, currentTile, testX, testY);
	}

	// Token: 0x06000D9F RID: 3487 RVA: 0x0003E63C File Offset: 0x0003C83C
	private void moveNPCPatrolNS(Party party, MapTile currentTile, int testX, int testY)
	{
		if (party.getFacing() == 1 || party.getFacing() == 3)
		{
			party.setFacing(0);
		}
		if (party.getFacing() == 0 && !this.isLocationUseableForParty(party, testX, testY + 1))
		{
			party.setFacing(2);
		}
		if (party.getFacing() == 2 && !this.isLocationUseableForParty(party, testX, testY - 1))
		{
			party.setFacing(0);
		}
		this.moveNPCTowardsFacing(party, currentTile, testX, testY);
	}

	// Token: 0x06000DA0 RID: 3488 RVA: 0x0003E6A8 File Offset: 0x0003C8A8
	private void moveNPCTowardsFacing(Party party, MapTile currentTile, int testX, int testY)
	{
		if (!this.playerParty.isInteractPartyMounted() && this.tileGrid.isPlayerPartyInNeighborhood(testX, testY))
		{
			party.processContactTrigger();
		}
		int num = 0;
		int num2 = 0;
		if (party.getFacing() == 3 && this.isLocationUseableForParty(party, testX - 1, testY))
		{
			num = -1;
		}
		else if (party.getFacing() == 1 && this.isLocationUseableForParty(party, testX + 1, testY))
		{
			num = 1;
		}
		else if (party.getFacing() == 2 && this.isLocationUseableForParty(party, testX, testY - 1))
		{
			num2 = -1;
		}
		else if (party.getFacing() == 0 && this.isLocationUseableForParty(party, testX, testY + 1))
		{
			num2 = 1;
		}
		if (num != 0 || num2 != 0)
		{
			this.getTile(testX + num, testY + num2).setParty(party);
			currentTile.clearParty();
		}
	}

	// Token: 0x06000DA1 RID: 3489 RVA: 0x0003E768 File Offset: 0x0003C968
	private void moveTowardsPC(Party party, MapTile currentTile, int testX, int testY)
	{
		if (!this.playerParty.isInteractPartyMounted() && this.tileGrid.isPlayerPartyInNeighborhood(testX, testY))
		{
			party.processContactTrigger();
			this.tileGrid.setTargetTile(testX, testY);
			return;
		}
		if (!this.hasLineOfSightToParty(party))
		{
			return;
		}
		if (this.playerParty.getCurrentCharacter().isHidden())
		{
			return;
		}
		int num = 0;
		int num2 = 0;
		if (Random.Range(1, 100) < 50)
		{
			if (testX > this.tileGrid.getXPos() && this.isLocationUseableForParty(party, testX - 1, testY))
			{
				num = -1;
			}
			else if (testX < this.tileGrid.getXPos() && this.isLocationUseableForParty(party, testX + 1, testY))
			{
				num = 1;
			}
			else if (testY > this.tileGrid.getYPos() && this.isLocationUseableForParty(party, testX, testY - 1))
			{
				num2 = -1;
			}
			else if (testY < this.tileGrid.getYPos() && this.isLocationUseableForParty(party, testX, testY + 1))
			{
				num2 = 1;
			}
		}
		else if (testY > this.tileGrid.getYPos() && this.isLocationUseableForParty(party, testX, testY - 1))
		{
			num2 = -1;
		}
		else if (testY < this.tileGrid.getYPos() && this.isLocationUseableForParty(party, testX, testY + 1))
		{
			num2 = 1;
		}
		else if (testX > this.tileGrid.getXPos() && this.isLocationUseableForParty(party, testX - 1, testY))
		{
			num = -1;
		}
		else if (testX < this.tileGrid.getXPos() && this.isLocationUseableForParty(party, testX + 1, testY))
		{
			num = 1;
		}
		if (num != 0 || num2 != 0)
		{
			this.getTile(testX + num, testY + num2).setParty(party);
			currentTile.clearParty();
		}
	}

	// Token: 0x06000DA2 RID: 3490 RVA: 0x0003E90C File Offset: 0x0003CB0C
	private void moveAwayFromPC(Party party, MapTile currentTile, int testX, int testY)
	{
		int num = 0;
		int num2 = 0;
		if (Random.Range(1, 100) < 50)
		{
			if (testX > this.tileGrid.getXPos() && this.isLocationUseableForParty(party, testX + 1, testY))
			{
				num = 1;
			}
			else if (testX < this.tileGrid.getXPos() && this.isLocationUseableForParty(party, testX - 1, testY))
			{
				num = -1;
			}
			else if (testY > this.tileGrid.getYPos() && this.isLocationUseableForParty(party, testX, testY + 1))
			{
				num2 = 1;
			}
			else if (testY < this.tileGrid.getYPos() && this.isLocationUseableForParty(party, testX, testY - 1))
			{
				num2 = -1;
			}
		}
		else if (testY > this.tileGrid.getYPos() && this.isLocationUseableForParty(party, testX, testY + 1))
		{
			num2 = 1;
		}
		else if (testY < this.tileGrid.getYPos() && this.isLocationUseableForParty(party, testX, testY - 1))
		{
			num2 = -1;
		}
		else if (testX > this.tileGrid.getXPos() && this.isLocationUseableForParty(party, testX + 1, testY))
		{
			num = 1;
		}
		else if (testX < this.tileGrid.getXPos() && this.isLocationUseableForParty(party, testX - 1, testY))
		{
			num = -1;
		}
		if (num != 0 || num2 != 0)
		{
			this.getTile(testX + num, testY + num2).setParty(party);
			currentTile.clearParty();
		}
	}

	// Token: 0x06000DA3 RID: 3491 RVA: 0x0003EA60 File Offset: 0x0003CC60
	private void moveRandom(Party party, MapTile currentTile, int testX, int testY)
	{
		int num = 0;
		int num2 = 0;
		int num3 = Random.Range(1, 6);
		if (num3 == 1 && this.isLocationUseableForParty(party, testX - 1, testY))
		{
			num = -1;
		}
		else if (num3 == 2 && this.isLocationUseableForParty(party, testX + 1, testY))
		{
			num = 1;
		}
		else if (num3 == 3 && this.isLocationUseableForParty(party, testX, testY - 1))
		{
			num2 = -1;
		}
		else if (num3 == 4 && this.isLocationUseableForParty(party, testX, testY + 1))
		{
			num2 = 1;
		}
		if (num != 0 || num2 != 0)
		{
			if (testX + num == this.tileGrid.getXPos() && testY + num2 == this.tileGrid.getYPos())
			{
				return;
			}
			this.getTile(testX + num, testY + num2).setParty(party);
			currentTile.clearParty();
		}
	}

	// Token: 0x06000DA4 RID: 3492 RVA: 0x0003EB10 File Offset: 0x0003CD10
	public bool isLocationUseableForParty(Party p, int tileX, int tileY)
	{
		Point tileWidthAndHeight = p.getTileWidthAndHeight();
		for (int i = 0; i < tileWidthAndHeight.X; i++)
		{
			for (int j = 0; j < tileWidthAndHeight.Y; j++)
			{
				if (!this.tileGrid.isTilePassableToNPC(p, tileX + i, tileY + j))
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x06000DA5 RID: 3493 RVA: 0x0003EB60 File Offset: 0x0003CD60
	public void revealNearbyNPCs()
	{
		foreach (MapTile mapTile in this.getAccessibleTilesFromParty())
		{
			Party party = mapTile.getParty();
			if (party != null)
			{
				this.tileGrid.revealPoint(party.getTileX(), party.getTileY(), 1);
			}
		}
		this.getAllVisibleNPCs(false);
		this.updateDrawLogic();
	}

	// Token: 0x06000DA6 RID: 3494 RVA: 0x0003EBDC File Offset: 0x0003CDDC
	public void fireLaunchCombatTriggers()
	{
		this.tileGrid.fireLaunchCombatTriggers();
	}

	// Token: 0x06000DA7 RID: 3495 RVA: 0x0003EBEC File Offset: 0x0003CDEC
	public void clearNPCLists()
	{
		if (this.nearByEnemies == null)
		{
			this.nearByEnemies = new List<Party>();
		}
		else
		{
			this.nearByEnemies.Clear();
		}
		if (this.potentiallyAlertEnemies == null)
		{
			this.potentiallyAlertEnemies = new List<Party>();
		}
		else
		{
			this.potentiallyAlertEnemies.Clear();
		}
		if (this.nearByFriendlies == null)
		{
			this.nearByFriendlies = new List<Party>();
			return;
		}
		this.nearByFriendlies.Clear();
	}

	// Token: 0x06000DA8 RID: 3496 RVA: 0x0003EC58 File Offset: 0x0003CE58
	private void getAllVisibleNPCs(bool updateAlertTriggers)
	{
		this.clearNPCLists();
		this.playerParty.clearBeingObserved();
		List<Party> list = new List<Party>();
		foreach (MapTile mapTile in this.getAccessibleTilesFromParty())
		{
			Party party = mapTile.getParty();
			if (party != null && !party.isEmpty() && !party.isPartyDead() && mapTile.isSpottedOnce())
			{
				party.levelScale();
				if (party.isHostile())
				{
					this.nearByEnemies.Add(party);
				}
				else if (!party.isPC())
				{
					this.nearByFriendlies.Add(party);
				}
				Character currentCharacter = this.playerParty.getCurrentCharacter();
				if (this.hasLineOfSightToParty(party))
				{
					if (!party.isAlert() && this.playerParty != null)
					{
						if (party.getMapTile().isSpotted())
						{
							this.playerParty.setBeingObserved();
						}
						currentCharacter.attemptToSneak(party.getCurrentCharacter());
						if (!currentCharacter.isHidden())
						{
							if (party.isHostile())
							{
								this.potentiallyAlertEnemies.Add(party);
							}
							list.Add(party);
						}
					}
					else if (!currentCharacter.isHidden() && party.isHostile())
					{
						this.potentiallyAlertEnemies.Add(party);
					}
				}
			}
		}
		this.playerParty.getCurrentCharacter().recoverHiddenDegree();
		if (updateAlertTriggers)
		{
			foreach (Party party2 in list)
			{
				party2.setGradualAlert();
			}
		}
	}

	// Token: 0x06000DA9 RID: 3497 RVA: 0x0003EE14 File Offset: 0x0003D014
	public int getCurrentTileStealthModifier()
	{
		return this.getTile(this.tileGrid.getXPos(), this.tileGrid.getYPos()).getStealthBonus();
	}

	// Token: 0x06000DAA RID: 3498 RVA: 0x0003EE37 File Offset: 0x0003D037
	private bool hasLineOfSightToParty(Party observer)
	{
		return this.tileGrid.testLineOfSight(this.playerParty.getTileX(), this.playerParty.getTileY(), observer.getTileX(), observer.getTileY());
	}

	// Token: 0x06000DAB RID: 3499 RVA: 0x0003EE66 File Offset: 0x0003D066
	public bool findPathToMouseTile()
	{
		return this.tileGrid.findPathToMouseTile(this.playerParty);
	}

	// Token: 0x06000DAC RID: 3500 RVA: 0x0003EE79 File Offset: 0x0003D079
	public void findCombatPath(Party party, int targetX, int targetY)
	{
		this.tileGrid.findCombatPath(party, targetX, targetY);
	}

	// Token: 0x06000DAD RID: 3501 RVA: 0x0003EE89 File Offset: 0x0003D089
	public bool areOpponentsNearBy()
	{
		return this.nearByEnemies != null && this.nearByEnemies.Count != 0;
	}

	// Token: 0x06000DAE RID: 3502 RVA: 0x0003EEA3 File Offset: 0x0003D0A3
	public List<MapTile> getTilesToFleeTo(Character character)
	{
		return this.tileGrid.getTilesToFleeTo(character);
	}

	// Token: 0x06000DAF RID: 3503 RVA: 0x0003EEB4 File Offset: 0x0003D0B4
	public void mountMapUpkeep(Party _party, bool fireTriggers = false)
	{
		this.playerParty = _party;
		this.populateMap();
		this.tileGrid.setExamineTile(null);
		this.tileGrid.setTargetTile(this.getXPos(), this.getYPos());
		this.setPositionTeleport(this.getXPos(), this.getYPos());
		this.setCurtain();
		this.setPosition();
		if (fireTriggers)
		{
			this.fireEnterTriggers();
		}
		this.mapIllustrator.clearOverlays();
	}

	// Token: 0x06000DB0 RID: 3504 RVA: 0x0003EF23 File Offset: 0x0003D123
	public string getContainerMapId()
	{
		return this.containerMapId;
	}

	// Token: 0x06000DB1 RID: 3505 RVA: 0x0003EF2B File Offset: 0x0003D12B
	private void fireEnterTriggers()
	{
		if (this.firstTimeEnterTrigger != "")
		{
			base.processString(this.firstTimeEnterTrigger, null);
			this.firstTimeEnterTrigger = "";
			return;
		}
		base.processString(this.enterTrigger, null);
	}

	// Token: 0x06000DB2 RID: 3506 RVA: 0x0003EF67 File Offset: 0x0003D167
	public void clearMapUpkeep(bool fireTriggers = false)
	{
		if (fireTriggers)
		{
			this.fireLeaveTriggers();
		}
		if (this.deleteAllOnLeave)
		{
			this.tileGrid.clearInstancesAndDelete();
			return;
		}
		this.tileGrid.clearInstances();
	}

	// Token: 0x06000DB3 RID: 3507 RVA: 0x0003EF94 File Offset: 0x0003D194
	public Party getClosestEnemy()
	{
		float num = -1f;
		Party result = null;
		foreach (Party party in this.nearByEnemies)
		{
			float linearDistance = NavigationTools.getLinearDistance(this.playerParty.getTileX(), this.playerParty.getTileY(), party.getTileX(), party.getTileY());
			if (num == -1f || linearDistance < num)
			{
				num = linearDistance;
				result = party;
			}
		}
		return result;
	}

	// Token: 0x06000DB4 RID: 3508 RVA: 0x0003F024 File Offset: 0x0003D224
	private void fireLeaveTriggers()
	{
		if (this.firstTimeLeaveTrigger != "")
		{
			base.processString(this.firstTimeLeaveTrigger, null);
			this.firstTimeLeaveTrigger = "";
			return;
		}
		base.processString(this.leaveTrigger, null);
	}

	// Token: 0x06000DB5 RID: 3509 RVA: 0x0003F060 File Offset: 0x0003D260
	public void activateAllPropsById(string propId)
	{
		foreach (SkaldWorldObject skaldWorldObject in GameData.getPropsByMap(this.getId()))
		{
			Prop prop = (Prop)skaldWorldObject;
			if (prop.isId(propId))
			{
				prop.activate();
			}
		}
		this.updateDrawLogic();
	}

	// Token: 0x06000DB6 RID: 3510 RVA: 0x0003F0CC File Offset: 0x0003D2CC
	public void revealPropsById(string propId)
	{
		foreach (SkaldWorldObject skaldWorldObject in GameData.getPropsByMap(this.getId()))
		{
			Prop prop = (Prop)skaldWorldObject;
			if (prop.isId(propId))
			{
				this.tileGrid.revealPoint(prop.getTileX(), prop.getTileY(), 1);
			}
		}
		this.updateDrawLogic();
	}

	// Token: 0x06000DB7 RID: 3511 RVA: 0x0003F14C File Offset: 0x0003D34C
	public void deactivateAllPropsById(string propId)
	{
		foreach (SkaldWorldObject skaldWorldObject in GameData.getPropsByMap(this.getId()))
		{
			Prop prop = (Prop)skaldWorldObject;
			if (prop.isId(propId))
			{
				prop.deactivate();
			}
		}
		this.updateDrawLogic();
	}

	// Token: 0x06000DB8 RID: 3512 RVA: 0x0003F1B8 File Offset: 0x0003D3B8
	public void unlockAllPropsById(string propId)
	{
		foreach (SkaldWorldObject skaldWorldObject in GameData.getPropsByMap(this.getId()))
		{
			Prop prop = (Prop)skaldWorldObject;
			if (prop.isId(propId) && prop != null && prop is PropLockable)
			{
				(prop as PropLockable).unlock();
			}
		}
		this.updateDrawLogic();
	}

	// Token: 0x06000DB9 RID: 3513 RVA: 0x0003F234 File Offset: 0x0003D434
	public void toggleAllPropsById(string propId)
	{
		foreach (SkaldWorldObject skaldWorldObject in GameData.getPropsByMap(this.getId()))
		{
			Prop prop = (Prop)skaldWorldObject;
			if (prop.isId(propId))
			{
				prop.toggle();
			}
		}
		this.updateDrawLogic();
	}

	// Token: 0x06000DBA RID: 3514 RVA: 0x0003F2A0 File Offset: 0x0003D4A0
	public Prop getFirstPropById(string propId)
	{
		return GameData.getFirstPropByIdOnMap(this.getId(), propId);
	}

	// Token: 0x06000DBB RID: 3515 RVA: 0x0003F2B0 File Offset: 0x0003D4B0
	public void warpCurrentPartyToProp(string propId)
	{
		Prop firstPropById = this.getFirstPropById(propId);
		if (firstPropById == null)
		{
			return;
		}
		Party party = this.getTargetTile().getParty();
		this.getTargetTile().clearParty();
		if (party == null)
		{
			return;
		}
		this.attemptToPlaceCharacterCloseToPoint(firstPropById.getTileX(), firstPropById.getTileY(), party.getCurrentCharacter(), null);
	}

	// Token: 0x06000DBC RID: 3516 RVA: 0x0003F300 File Offset: 0x0003D500
	public void deleteAllPropsById(string propId)
	{
		Debug.Log("Removing props with id " + propId + " on map " + this.getId());
		foreach (SkaldWorldObject skaldWorldObject in GameData.getPropsByMap(this.getId()))
		{
			Prop prop = (Prop)skaldWorldObject;
			if (prop.isId(propId))
			{
				MapTile tile = this.getTile(prop.getTileX(), prop.getTileY());
				if (tile != null)
				{
					tile.deleteProp();
				}
				prop.clearTilePosition();
			}
		}
		this.updateDrawLogic();
	}

	// Token: 0x06000DBD RID: 3517 RVA: 0x0003F3A4 File Offset: 0x0003D5A4
	public void replaceAllPropsById(string propId, string newProp)
	{
		foreach (SkaldWorldObject skaldWorldObject in GameData.getPropsByMap(this.getId()))
		{
			Prop prop = (Prop)skaldWorldObject;
			if (prop.isId(propId))
			{
				MapTile tile = this.getTile(prop.getTileX(), prop.getTileY());
				Prop prop2 = GameData.instantiateProp(newProp);
				if (tile != null && tile.getProp() == prop)
				{
					this.setProp(prop2, tile);
				}
				else
				{
					prop2.setTilePosition(prop.getTileX(), prop.getTileY(), this.getId());
					prop.clearTilePosition();
				}
			}
		}
		this.updateDrawLogic();
	}

	// Token: 0x06000DBE RID: 3518 RVA: 0x0003F458 File Offset: 0x0003D658
	public void moveNPCToProp(string propId, string npcId)
	{
		Character character = GameData.instantiateCharacter(npcId);
		List<SkaldWorldObject> propsByMap = GameData.getPropsByMap(this.getId());
		Prop prop = null;
		foreach (SkaldWorldObject skaldWorldObject in propsByMap)
		{
			Prop prop2 = (Prop)skaldWorldObject;
			if (prop2.isId(propId))
			{
				prop = prop2;
				break;
			}
		}
		if (character == null || prop == null)
		{
			MainControl.logError(string.Concat(new string[]
			{
				"Character ",
				npcId,
				" or prop ",
				propId,
				" does not exist."
			}));
			return;
		}
		MapTile mapTile = character.getMapTile();
		MapTile mapTile2 = prop.getMapTile();
		if (mapTile2 == null)
		{
			MainControl.logError("No target tile for prop" + propId);
			return;
		}
		if (mapTile != null)
		{
			mapTile.clearParty();
		}
		this.attemptToPlaceCharacterCloseToPoint(mapTile2.getTileX(), mapTile2.getTileY(), character, null);
		this.updateDrawLogic();
		this.getAllVisibleNPCs(true);
	}

	// Token: 0x06000DBF RID: 3519 RVA: 0x0003F54C File Offset: 0x0003D74C
	public void spawnByProps(string propId, string npcId)
	{
		bool flag = false;
		foreach (SkaldWorldObject skaldWorldObject in GameData.getPropsByMap(this.getId()))
		{
			Prop prop = (Prop)skaldWorldObject;
			if (prop.isId(propId))
			{
				flag = true;
				this.attemptToPlaceCharacterCloseToPoint(prop.getTileX(), prop.getTileY(), GameData.instantiateCharacter(npcId), null);
			}
		}
		if (!flag)
		{
			MainControl.logError("Did not manage to place any NPCs with ID " + npcId + " near props with ID " + propId);
			return;
		}
		this.updateDrawLogic();
		this.getAllVisibleNPCs(true);
	}

	// Token: 0x06000DC0 RID: 3520 RVA: 0x0003F5F0 File Offset: 0x0003D7F0
	private void populateMap()
	{
		foreach (SkaldWorldObject skaldWorldObject in GameData.getCharacterByMap(this.getId()))
		{
			Character character = (Character)skaldWorldObject;
			int tileX = character.getTileX();
			int tileY = character.getTileY();
			if (this.isTileValid(tileX, tileY) && !this.playerParty.containsObject(character.getId()))
			{
				if (this.getTile(tileX, tileY).getParty() != null)
				{
					this.attemptToPlaceCharacterCloseToPoint(tileX, tileY, character, null);
				}
				else
				{
					this.getTile(tileX, tileY).addCharacter(character);
					if (this.getTile(tileX, tileY).getParty() == null)
					{
						MainControl.logError(string.Concat(new string[]
						{
							"Party is null for tile:",
							tileX.ToString(),
							"/",
							tileY.ToString(),
							" on map ",
							this.getId(),
							" with character ",
							character.getId()
						}));
					}
					else
					{
						this.getTile(tileX, tileY).getParty().snapToGrid();
					}
				}
			}
		}
		foreach (SkaldWorldObject skaldWorldObject2 in GameData.getItemByMap(this.getId()))
		{
			Item item = (Item)skaldWorldObject2;
			int tileX2 = item.getTileX();
			int tileY2 = item.getTileY();
			if (this.isTileValid(tileX2, tileY2))
			{
				this.getTile(tileX2, tileY2).getInventory().addItem(item);
			}
		}
		foreach (SkaldWorldObject skaldWorldObject3 in GameData.getPropsByMap(this.getId()))
		{
			Prop prop = (Prop)skaldWorldObject3;
			int tileX3 = prop.getTileX();
			int tileY3 = prop.getTileY();
			if (this.isTileValid(tileX3, tileY3))
			{
				this.setProp(prop, this.getTile(tileX3, tileY3));
			}
		}
		foreach (SkaldWorldObject skaldWorldObject4 in GameData.getVehiclesByMap(this.getId()))
		{
			Vehicle vehicle = (Vehicle)skaldWorldObject4;
			int tileX4 = vehicle.getTileX();
			int tileY4 = vehicle.getTileY();
			if (this.isTileValid(tileX4, tileY4))
			{
				this.getTile(tileX4, tileY4).setVehicle(vehicle);
			}
		}
	}

	// Token: 0x06000DC1 RID: 3521 RVA: 0x0003F88C File Offset: 0x0003DA8C
	public void setProp(Prop prop, MapTile mapTile)
	{
		this.tileGrid.setProp(mapTile, prop);
	}

	// Token: 0x06000DC2 RID: 3522 RVA: 0x0003F89C File Offset: 0x0003DA9C
	public string testMouseExitId(Vector2 position)
	{
		if (position.x < 0f || position.x > 1f || position.y < 0f || position.y > 1f)
		{
			return null;
		}
		int num = this.getViewportX() - MapIllustrator.ScreenDimensions.getIllustratedMapHalfWidth();
		int num2 = this.getViewportY() - MapIllustrator.ScreenDimensions.getIllustratedMapHalfHeight();
		int num3 = num + Mathf.RoundToInt((float)MapIllustrator.ScreenDimensions.getIllustratedMapWidth() * position.x);
		int num4 = num2 + Mathf.RoundToInt((float)MapIllustrator.ScreenDimensions.getIllustratedMapHeight() * position.y);
		if (num3 <= 0)
		{
			if (this.westernEdgeMapId != "")
			{
				return this.westernEdgeMapId;
			}
			if (this.containerMapId != "")
			{
				return this.containerMapId;
			}
		}
		else if (num3 >= this.tileGrid.getMapTileWidth())
		{
			if (this.easternEdgeMapId != "")
			{
				return this.easternEdgeMapId;
			}
			if (this.containerMapId != "")
			{
				return this.containerMapId;
			}
		}
		if (num4 <= 0)
		{
			if (this.southernEdgeMapId != "")
			{
				return this.southernEdgeMapId;
			}
			if (this.containerMapId != "")
			{
				return this.containerMapId;
			}
		}
		else if (num4 >= this.tileGrid.getMapTileHeight())
		{
			if (this.northernEdgeMapId != "")
			{
				return this.northernEdgeMapId;
			}
			if (this.containerMapId != "")
			{
				return this.containerMapId;
			}
		}
		return "";
	}

	// Token: 0x06000DC3 RID: 3523 RVA: 0x0003FA14 File Offset: 0x0003DC14
	public string testExitByEdge(int x, int y)
	{
		if (x < 0 && this.westernEdgeMapId != "")
		{
			return this.westernEdgeMapId;
		}
		if (x >= this.tileGrid.getMapTileWidth() && this.easternEdgeMapId != "")
		{
			return this.easternEdgeMapId;
		}
		if (y < 0 && this.southernEdgeMapId != "")
		{
			return this.southernEdgeMapId;
		}
		if (y >= this.tileGrid.getMapTileHeight() && this.northernEdgeMapId != "")
		{
			return this.northernEdgeMapId;
		}
		if (this.containerMapId != "")
		{
			return this.containerMapId;
		}
		return "";
	}

	// Token: 0x06000DC4 RID: 3524 RVA: 0x0003FAC7 File Offset: 0x0003DCC7
	public void setScreenShake(int i)
	{
		this.mapIllustrator.getGenericEffectsControl().setScreenShake(i);
	}

	// Token: 0x06000DC5 RID: 3525 RVA: 0x0003FADA File Offset: 0x0003DCDA
	public void setFlash(int i)
	{
		this.mapIllustrator.getGenericEffectsControl().setFlash(i);
	}

	// Token: 0x06000DC6 RID: 3526 RVA: 0x0003FAED File Offset: 0x0003DCED
	public void setRain(int i)
	{
		this.mapIllustrator.getWeatherEffectsControl().setRain(i);
	}

	// Token: 0x06000DC7 RID: 3527 RVA: 0x0003FB00 File Offset: 0x0003DD00
	public string printWeatherDescription()
	{
		return this.mapIllustrator.getWeatherEffectsControl().printDescription();
	}

	// Token: 0x06000DC8 RID: 3528 RVA: 0x0003FB12 File Offset: 0x0003DD12
	public void setFog(int i)
	{
		this.mapIllustrator.getWeatherEffectsControl().setFog(i);
	}

	// Token: 0x06000DC9 RID: 3529 RVA: 0x0003FB25 File Offset: 0x0003DD25
	public void clearFogEffect()
	{
		this.mapIllustrator.getWeatherEffectsControl().clearFog();
	}

	// Token: 0x06000DCA RID: 3530 RVA: 0x0003FB37 File Offset: 0x0003DD37
	public void clearRainEffect()
	{
		this.mapIllustrator.getWeatherEffectsControl().clearRain();
	}

	// Token: 0x06000DCB RID: 3531 RVA: 0x0003FB49 File Offset: 0x0003DD49
	public void setCurtain()
	{
		this.mapIllustrator.getGenericEffectsControl().setCurtain();
	}

	// Token: 0x06000DCC RID: 3532 RVA: 0x0003FB5B File Offset: 0x0003DD5B
	private float getViewDistance()
	{
		return this.viewDistance;
	}

	// Token: 0x06000DCD RID: 3533 RVA: 0x0003FB64 File Offset: 0x0003DD64
	private List<MapTile> getEachTileOccupiedByPlayers()
	{
		List<MapTile> list = new List<MapTile>(6);
		if (this.playerParty == null || this.playerParty.isEmpty())
		{
			return list;
		}
		foreach (SkaldBaseObject skaldBaseObject in this.playerParty.getObjectList())
		{
			Character character = (Character)skaldBaseObject;
			if (character.getContainerMapId() == this.getId() && this.isTileValid(character.getTileX(), character.getTileY()))
			{
				MapTile tile = this.getTile(character.getTileX(), character.getTileY());
				if (!list.Contains(tile))
				{
					list.Add(tile);
				}
			}
		}
		return list;
	}

	// Token: 0x06000DCE RID: 3534 RVA: 0x0003FC24 File Offset: 0x0003DE24
	private void updateViewshed()
	{
		List<MapTile> eachTileOccupiedByPlayers = this.getEachTileOccupiedByPlayers();
		if (eachTileOccupiedByPlayers.Count == 0)
		{
			eachTileOccupiedByPlayers.Add(this.getCurrentTile());
		}
		float mofified_view_distance = this.getViewDistance();
		Map mapAboveForDrawing = this.getMapAboveForDrawing();
		foreach (MapTile mapTile in eachTileOccupiedByPlayers)
		{
			int tileX = mapTile.getTileX();
			int tileY = mapTile.getTileY();
			this.getViewshedFromPoint(tileX, tileY, mofified_view_distance);
			if (mapAboveForDrawing != null)
			{
				MapTile tile = mapAboveForDrawing.getTile(tileX, tileY);
				if (mapAboveForDrawing.isVoidTile(tileX, tileY) || (tile != null && tile.isForceOutside()))
				{
					mapAboveForDrawing.getViewshedFromPoint(tileX, tileY, mofified_view_distance);
				}
			}
		}
	}

	// Token: 0x06000DCF RID: 3535 RVA: 0x0003FCE0 File Offset: 0x0003DEE0
	public void getViewshedFromPoint(int x, int y, float mofified_view_distance)
	{
		int longestTileMapDimension = MapIllustrator.ScreenDimensions.getLongestTileMapDimension();
		for (int i = x - longestTileMapDimension - 1; i < x + (longestTileMapDimension + 2); i++)
		{
			for (int j = y - longestTileMapDimension - 1; j < y + (longestTileMapDimension + 2); j++)
			{
				if (this.isTileValid(i, j))
				{
					MapTile tile = this.getTile(i, j);
					float linearDistance = NavigationTools.getLinearDistance(x, y, i, j);
					if (!tile.isSpotted() && linearDistance <= mofified_view_distance && this.tileGrid.testLineOfSight(x, y, i, j))
					{
						tile.setSpotted();
					}
				}
			}
		}
	}

	// Token: 0x06000DD0 RID: 3536 RVA: 0x0003FD60 File Offset: 0x0003DF60
	private void updateLightmap()
	{
		this.updateLightmap(this.getAmbientLightLevel(), this.tileGrid.getXPos(), this.tileGrid.getYPos());
		if (this.getMapAboveForDrawing() != null)
		{
			this.getMapAboveForDrawing().updateLightmap(this.getAmbientLightLevel() * 1.1f, this.tileGrid.getXPos(), this.tileGrid.getYPos());
		}
	}

	// Token: 0x06000DD1 RID: 3537 RVA: 0x0003FDC4 File Offset: 0x0003DFC4
	public void updateTileLightValue(int x, int y)
	{
		MapTile tile = this.getTile(x, y);
		if (tile == null)
		{
			return;
		}
		tile.updateLightLevel();
		if (this.getMapAboveForDrawing() != null)
		{
			this.getMapAboveForDrawing().updateTileLightValue(x, y);
		}
	}

	// Token: 0x06000DD2 RID: 3538 RVA: 0x0003FDFC File Offset: 0x0003DFFC
	public void updateLightmapSecondPass(int x, int y)
	{
		int num = MapIllustrator.ScreenDimensions.getLongestTileMapDimension() / 2 + 5;
		int num2 = x + (num + 1) - (x - num);
		int num3 = y + (num + 1) - (y - num);
		float[,] array = new float[num2, num3];
		int num4 = 0;
		for (int i = x - num; i < x + (num + 1); i++)
		{
			int num5 = 0;
			for (int j = y - num; j < y + (num + 1); j++)
			{
				array[num4, num5] = this.calculateNeighbourLightValue(i, j);
				num5++;
			}
			num4++;
		}
		num4 = 0;
		for (int k = x - num; k < x + (num + 1); k++)
		{
			int num6 = 0;
			for (int l = y - num; l < y + (num + 1); l++)
			{
				if (this.isTileValid(k, l))
				{
					this.getTile(k, l).setLightLevel(array[num4, num6] + 0.1f);
				}
				num6++;
			}
			num4++;
		}
	}

	// Token: 0x06000DD3 RID: 3539 RVA: 0x0003FEE0 File Offset: 0x0003E0E0
	private float calculateNeighbourLightValue(int x, int y)
	{
		if (!this.isTileValid(x, y))
		{
			return 0f;
		}
		MapTile tile = this.getTile(x, y);
		float targetLightLevel = tile.getTargetLightLevel();
		foreach (MapTile mapTile in this.tileGrid.getManhattanNeighbours(x, y))
		{
			if (mapTile.getTargetLightLevel() > targetLightLevel)
			{
				targetLightLevel = mapTile.getTargetLightLevel();
			}
		}
		return (tile.getTargetLightLevel() + targetLightLevel) / 2f;
	}

	// Token: 0x06000DD4 RID: 3540 RVA: 0x0003FF74 File Offset: 0x0003E174
	public void updateLightmap(float baseLightLevel, int x, int y)
	{
		this.mapIllustrator.clearTilesWithLightProps();
		int num = MapIllustrator.ScreenDimensions.getLongestTileMapDimension() / 2 + 5;
		for (int i = x - num; i < x + (num + 1); i++)
		{
			for (int j = y - num; j < y + (num + 1); j++)
			{
				if (this.isTileValid(i, j))
				{
					MapTile tile = this.getTile(i, j);
					tile.setLightLevel(baseLightLevel);
					int lightRadius = tile.getLightRadius();
					float lightEmitterStrength = tile.getLightEmitterStrength();
					if (lightRadius > 0)
					{
						if (tile.getProp() != null && tile.getProp().getLight() > 0)
						{
							this.mapIllustrator.addLightProp(tile.getProp());
						}
						this.applyLight(i, j, lightRadius, lightEmitterStrength);
						MapTile tileAbove = this.getTileAbove(i, j);
						if (tileAbove != null && tileAbove.isVoidTile() && this.getMapAboveForDrawing() != null)
						{
							this.getMapAboveForDrawing().applyLight(i, j, lightRadius, lightEmitterStrength);
						}
					}
				}
			}
		}
		this.updateLightmapSecondPass(x, y);
	}

	// Token: 0x06000DD5 RID: 3541 RVA: 0x00040064 File Offset: 0x0003E264
	public void applyLight(int sourceX, int sourceY, int radius, float lightStrength)
	{
		for (int i = sourceX - radius; i < sourceX + (radius + 1); i++)
		{
			for (int j = sourceY - radius; j < sourceY + (radius + 1); j++)
			{
				if (this.isTileValid(i, j))
				{
					MapTile tile = this.getTile(i, j);
					float linearDistance = NavigationTools.getLinearDistance(sourceX, sourceY, i, j);
					if (linearDistance <= (float)radius && this.tileGrid.testLineOfSightLight(sourceX, sourceY, i, j))
					{
						float lightLevel = lightStrength * (1f - linearDistance / (float)radius);
						tile.setLightLevel(lightLevel);
					}
				}
			}
		}
	}

	// Token: 0x06000DD6 RID: 3542 RVA: 0x000400E0 File Offset: 0x0003E2E0
	public void applyEffectLight(SkaldPhysicalObject emitter)
	{
		if (emitter == null)
		{
			return;
		}
		int lightEffectDistance = emitter.getLightEffectDistance();
		float lightEffectStrength = emitter.getLightEffectStrength();
		int tileX = emitter.getTileX();
		int tileY = emitter.getTileY();
		for (int i = tileX - lightEffectDistance; i < tileX + (lightEffectDistance + 1); i++)
		{
			for (int j = tileY - lightEffectDistance; j < tileY + (lightEffectDistance + 1); j++)
			{
				if (this.isTileValid(i, j))
				{
					MapTile tile = this.getTile(i, j);
					float linearDistance = NavigationTools.getLinearDistance(tileX, tileY, i, j);
					if (linearDistance <= (float)lightEffectDistance)
					{
						float lightEffectsLevel = 0f;
						if (lightEffectStrength != 0f)
						{
							lightEffectsLevel = lightEffectStrength * (1f - linearDistance / (float)lightEffectDistance);
						}
						tile.setLightEffectsLevel(lightEffectsLevel);
					}
				}
			}
		}
	}

	// Token: 0x06000DD7 RID: 3543 RVA: 0x0004018B File Offset: 0x0003E38B
	public WeatherEffectsControl.WeatherSaveData getWeatherSaveData()
	{
		return this.mapIllustrator.getWeatherEffectsControl().getSaveData();
	}

	// Token: 0x06000DD8 RID: 3544 RVA: 0x0004019D File Offset: 0x0003E39D
	private void applyWeatherSaveData(WeatherEffectsControl.WeatherSaveData data)
	{
		this.mapIllustrator.getWeatherEffectsControl().applySaveData(data);
	}

	// Token: 0x06000DD9 RID: 3545 RVA: 0x000401B0 File Offset: 0x0003E3B0
	public int getXPos()
	{
		return this.tileGrid.getXPos();
	}

	// Token: 0x06000DDA RID: 3546 RVA: 0x000401BD File Offset: 0x0003E3BD
	public int getYPos()
	{
		return this.tileGrid.getYPos();
	}

	// Token: 0x06000DDB RID: 3547 RVA: 0x000401CA File Offset: 0x0003E3CA
	public void centerViewPort()
	{
		this.tileGrid.centerViewPort();
		this.updateDrawLogic();
	}

	// Token: 0x06000DDC RID: 3548 RVA: 0x000401E0 File Offset: 0x0003E3E0
	public void loadSaveData(Map.MapSaveData mapSaveData)
	{
		this.tileGrid.setTilePosition(mapSaveData.xPosition, mapSaveData.yPosition);
		this.tileGrid.centerViewPort();
		try
		{
			for (int i = 0; i < this.tileGrid.getMapTileWidth(); i++)
			{
				for (int j = 0; j < this.tileGrid.getMapTileHeight(); j++)
				{
					this.getTile(i, j).setSaveData(mapSaveData.spotted[i, j], mapSaveData.spottedOnce[i, j], mapSaveData.visited[i, j]);
				}
			}
		}
		catch
		{
			MainControl.logError("Trying to load non-existant map: " + this.getId());
		}
		this.firstTimeEnterTrigger = mapSaveData.firstTimeEnterTrigger;
		this.firstTimeLeaveTrigger = mapSaveData.firstTimeLeaveTrigger;
		this.applyWeatherSaveData(mapSaveData.weatherData);
		this.mapIllustrator.resetTextureBuffer();
	}

	// Token: 0x06000DDD RID: 3549 RVA: 0x000402C8 File Offset: 0x0003E4C8
	public Map.MapSaveData getSaveData()
	{
		return new Map.MapSaveData(this);
	}

	// Token: 0x04000329 RID: 809
	private float viewDistance = 5.99f;

	// Token: 0x0400032A RID: 810
	public int startX;

	// Token: 0x0400032B RID: 811
	public int startY;

	// Token: 0x0400032C RID: 812
	private MapTileGrid tileGrid;

	// Token: 0x0400032D RID: 813
	private MapIllustrator mapIllustrator;

	// Token: 0x0400032E RID: 814
	private List<MapTile> cachedAccessibleTiles;

	// Token: 0x0400032F RID: 815
	private Map mapAbove;

	// Token: 0x04000330 RID: 816
	private Map mapBelow;

	// Token: 0x04000331 RID: 817
	private bool drawOverlay = true;

	// Token: 0x04000332 RID: 818
	private bool fogRegrows;

	// Token: 0x04000333 RID: 819
	public bool enterPrompt;

	// Token: 0x04000334 RID: 820
	public bool directionalExit;

	// Token: 0x04000335 RID: 821
	public bool overland;

	// Token: 0x04000336 RID: 822
	public bool wilderness;

	// Token: 0x04000337 RID: 823
	public bool indoors;

	// Token: 0x04000338 RID: 824
	public bool city;

	// Token: 0x04000339 RID: 825
	public bool dynamicEnc = true;

	// Token: 0x0400033A RID: 826
	public bool drawAsCube = true;

	// Token: 0x0400033B RID: 827
	private bool canMakeCampHere = true;

	// Token: 0x0400033C RID: 828
	private bool canSleepInBedHere = true;

	// Token: 0x0400033D RID: 829
	public bool canFightHere = true;

	// Token: 0x0400033E RID: 830
	public bool prefabTarget;

	// Token: 0x0400033F RID: 831
	private bool dayNightCycleLight = true;

	// Token: 0x04000340 RID: 832
	private bool deleteAllOnLeave;

	// Token: 0x04000341 RID: 833
	private bool groundIsWhite;

	// Token: 0x04000342 RID: 834
	public string faction = "";

	// Token: 0x04000343 RID: 835
	public string economy = "";

	// Token: 0x04000344 RID: 836
	private float baseAmbientLightLevel = 1f;

	// Token: 0x04000345 RID: 837
	private string musicPath;

	// Token: 0x04000346 RID: 838
	public List<Party> nearByEnemies;

	// Token: 0x04000347 RID: 839
	public List<Party> nearByFriendlies;

	// Token: 0x04000348 RID: 840
	public List<Party> potentiallyAlertEnemies;

	// Token: 0x04000349 RID: 841
	private List<MapTile> preCombatPlacementTiles;

	// Token: 0x0400034A RID: 842
	public Party playerParty;

	// Token: 0x0400034B RID: 843
	public string mapAboveId = "";

	// Token: 0x0400034C RID: 844
	public string mapBelowId = "";

	// Token: 0x0400034D RID: 845
	public string containerMapId = "";

	// Token: 0x0400034E RID: 846
	private string enterTrigger = "";

	// Token: 0x0400034F RID: 847
	private string firstTimeEnterTrigger = "";

	// Token: 0x04000350 RID: 848
	private string leaveTrigger = "";

	// Token: 0x04000351 RID: 849
	private string firstTimeLeaveTrigger = "";

	// Token: 0x04000352 RID: 850
	public string northernEdgeMapId = "";

	// Token: 0x04000353 RID: 851
	public string easternEdgeMapId = "";

	// Token: 0x04000354 RID: 852
	public string westernEdgeMapId = "";

	// Token: 0x04000355 RID: 853
	public string southernEdgeMapId = "";

	// Token: 0x04000356 RID: 854
	private string combatMusicPath = "Combat_2";

	// Token: 0x02000249 RID: 585
	[Serializable]
	public struct MapSaveData
	{
		// Token: 0x06001940 RID: 6464 RVA: 0x0006E848 File Offset: 0x0006CA48
		public MapSaveData(Map map)
		{
			this.xPosition = map.tileGrid.getXPos();
			this.yPosition = map.tileGrid.getYPos();
			this.firstTimeEnterTrigger = map.firstTimeEnterTrigger;
			this.firstTimeLeaveTrigger = map.firstTimeLeaveTrigger;
			this.id = map.getId();
			this.spotted = new bool[0, 0];
			this.visited = new bool[0, 0];
			this.spottedOnce = new bool[0, 0];
			this.weatherData = map.getWeatherSaveData();
			try
			{
				this.spotted = new bool[map.tileGrid.getMapTileWidth(), map.tileGrid.getMapTileHeight()];
				this.visited = new bool[map.tileGrid.getMapTileWidth(), map.tileGrid.getMapTileHeight()];
				this.spottedOnce = new bool[map.tileGrid.getMapTileWidth(), map.tileGrid.getMapTileHeight()];
				for (int i = 0; i < map.tileGrid.getMapTileWidth(); i++)
				{
					for (int j = 0; j < map.tileGrid.getMapTileHeight(); j++)
					{
						this.spotted[i, j] = map.getTile(i, j).isSpotted();
						this.visited[i, j] = map.getTile(i, j).isVisited();
						this.spottedOnce[i, j] = map.getTile(i, j).isSpottedOnce();
					}
				}
			}
			catch
			{
				MainControl.log("No tilemap for " + this.id);
			}
		}

		// Token: 0x040008E6 RID: 2278
		public int xPosition;

		// Token: 0x040008E7 RID: 2279
		public int yPosition;

		// Token: 0x040008E8 RID: 2280
		public string id;

		// Token: 0x040008E9 RID: 2281
		public string firstTimeEnterTrigger;

		// Token: 0x040008EA RID: 2282
		public string firstTimeLeaveTrigger;

		// Token: 0x040008EB RID: 2283
		public bool[,] spotted;

		// Token: 0x040008EC RID: 2284
		public bool[,] visited;

		// Token: 0x040008ED RID: 2285
		public bool[,] spottedOnce;

		// Token: 0x040008EE RID: 2286
		public WeatherEffectsControl.WeatherSaveData weatherData;
	}
}
