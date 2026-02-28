using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

// Token: 0x020000EB RID: 235
public class MapIllustrator
{
	// Token: 0x06000E07 RID: 3591 RVA: 0x00040F9C File Offset: 0x0003F19C
	public MapIllustrator(Map map, MapTileGrid tileGrid)
	{
		this.fogOfWarManager = new MapIllustrator.FogOfWarManager(MapIllustrator.ScreenDimensions.getIllustratedMapWidthAndRim(), MapIllustrator.ScreenDimensions.getIllustratedMapHeightAndRim(), map, tileGrid);
		this.foregroundManager = new MapIllustrator.ForegroundManager(MapIllustrator.ScreenDimensions.getIllustratedMapWidthAndRim(), MapIllustrator.ScreenDimensions.getIllustratedMapHeightAndRim());
		int width = MapIllustrator.ScreenDimensions.getIllustratedMapWidthAndRim() * 16;
		int height = MapIllustrator.ScreenDimensions.getIllustratedMapHeightAndRim() * 16;
		this.illustratedMapTerrain = new TextureTools.TextureData(width, height);
		this.illustratedMapTerrainOverlay = new TextureTools.TextureData(width, height);
		this.illustratedMapTerrainForeground = new TextureTools.TextureData(width, height);
		this.illustratedMapAnimated = new TextureTools.TextureData(width, height);
		this.illustratedMapAnimatedForeground = new TextureTools.TextureData(width, height);
		this.weatherEffectControl = new WeatherEffectsControl();
		this.particleSystem = new ParticleSystem();
		this.genericEffectsControl = new GenericEffectsControl();
		this.setIcons();
		this.scrollControl = new MapIllustrator.ScrollControl();
		this.tileGrid = tileGrid;
		this.map = map;
	}

	// Token: 0x06000E08 RID: 3592 RVA: 0x00041100 File Offset: 0x0003F300
	private void setIcons()
	{
		this.lifeBox = TextureTools.loadTextureData("Images/GUIIcons/LifeBox");
		this.pathPipGreen = TextureTools.loadTextureData("Images/GUIIcons/PathPipGreen");
		this.pathPipRed = TextureTools.loadTextureData("Images/GUIIcons/PathPipRed");
		this.potentialMoveShadow1 = TextureTools.loadTextureData("Images/GUIIcons/potentialMoveShadow1");
		this.targetShadow1 = TextureTools.loadTextureData("Images/GUIIcons/targetShadow1");
		this.targetShadow2 = TextureTools.loadTextureData("Images/GUIIcons/targetShadow2");
		this.targetShadowSwap1 = TextureTools.loadTextureData("Images/GUIIcons/targetShadowSwap1");
		this.targetShadowSwap2 = TextureTools.loadTextureData("Images/GUIIcons/targetShadowSwap2");
		this.targetShadowSpells1 = TextureTools.loadTextureData("Images/GUIIcons/targetShadowSpells1");
		this.targetShadowSpells2 = TextureTools.loadTextureData("Images/GUIIcons/targetShadowSpells2");
		this.targetShadowBlocked1 = TextureTools.loadTextureData("Images/GUIIcons/targetShadowBlocked1");
		this.targetShadowBlocked2 = TextureTools.loadTextureData("Images/GUIIcons/targetShadowBlocked2");
		this.targetShadowOpponent1 = TextureTools.loadTextureData("Images/GUIIcons/targetShadowOpponent1");
		this.targetShadowOpponent2 = TextureTools.loadTextureData("Images/GUIIcons/targetShadowOpponent2");
		this.targetShadowInteractable1 = TextureTools.loadTextureData("Images/GUIIcons/targetShadowInteractable1");
		this.targetShadowInteractable2 = TextureTools.loadTextureData("Images/GUIIcons/targetShadowInteractable2");
		this.lightBox = TextureTools.loadTextureData("Images/Tiles/EditorBoxGreen");
		this.momentumIcon = TextureTools.loadTextureData("Images/GUIIcons/MomentumIcon");
		this.flankedIcon = TextureTools.loadTextureData("Images/GUIIcons/FlankedIcon");
		this.tacticalGrid = TextureTools.loadTextureData("Images/GUIIcons/tacticalGrid");
		this.tacticalGridNonGray = TextureTools.loadTextureData("Images/GUIIcons/tacticalGridNonGray");
	}

	// Token: 0x06000E09 RID: 3593 RVA: 0x0004125D File Offset: 0x0003F45D
	public void resetTextureBuffer()
	{
		this.tileGrid.resetTextureBuffers();
	}

	// Token: 0x06000E0A RID: 3594 RVA: 0x0004126A File Offset: 0x0003F46A
	public void stopScroll()
	{
		this.scrollControl.stopScroll();
	}

	// Token: 0x06000E0B RID: 3595 RVA: 0x00041277 File Offset: 0x0003F477
	public bool isScrollReady()
	{
		return this.scrollControl.isScrollReady();
	}

	// Token: 0x06000E0C RID: 3596 RVA: 0x00041284 File Offset: 0x0003F484
	public void setScroll(int x, int y)
	{
		this.scrollControl.setScroll(x, y, this.tileGrid);
	}

	// Token: 0x06000E0D RID: 3597 RVA: 0x00041299 File Offset: 0x0003F499
	public GenericEffectsControl getGenericEffectsControl()
	{
		return this.genericEffectsControl;
	}

	// Token: 0x06000E0E RID: 3598 RVA: 0x000412A1 File Offset: 0x0003F4A1
	public WeatherEffectsControl getWeatherEffectsControl()
	{
		return this.weatherEffectControl;
	}

	// Token: 0x06000E0F RID: 3599 RVA: 0x000412AC File Offset: 0x0003F4AC
	public UIMap applyMapOverlay(Character currentCharacter, bool combatHighlights, bool overlandHighlights, bool highlightAllCharacers)
	{
		this.illustratedMapTerrain.copyToTarget(this.illustratedMapAnimated);
		this.illustratedMapAnimatedForeground.clear();
		MapIllustrator.CURRENT_FRAME = this.globalAnimationControl.getCurrentFrame();
		MapIllustrator.CURRENT_WATER_FRAME = this.globalWaterAnimationControl.getCurrentFrame();
		this.scrollControl.scroll(this.weatherEffectControl, false);
		this.tilesWithCharacters.Clear();
		this.tilesWithCorpses.Clear();
		this.tilesWithMapObjects.Clear();
		this.UISprites.Clear();
		this.UISpritesTopLayer.Clear();
		int num = (this.tileGrid.getViewportX() - MapIllustrator.ScreenDimensions.getIllustratedMapHalfWidth() - 1) * 16;
		int num2 = (this.tileGrid.getViewportY() - MapIllustrator.ScreenDimensions.getIllustratedMapHalfHeight() - 1) * 16;
		int num3 = this.tileGrid.getViewportX() - 1 - MapIllustrator.ScreenDimensions.getIllustratedMapHalfWidth();
		int num4 = 0;
		for (int i = 0; i < MapIllustrator.ScreenDimensions.getIllustratedMapWidthAndRim(); i++)
		{
			int num5 = this.tileGrid.getViewportY() + 1 + MapIllustrator.ScreenDimensions.getIllustratedMapHalfHeight();
			int num6 = (MapIllustrator.ScreenDimensions.getIllustratedMapHeight() + 1) * 16;
			for (int j = MapIllustrator.ScreenDimensions.getIllustratedMapHeight() + 1; j >= 0; j--)
			{
				if (this.tileGrid.isTileValid(num3, num5))
				{
					this.currentOverlayTile = this.tileGrid.getTile(num3, num5);
					this.map.updateTileLightValue(num3, num5);
					if (this.currentOverlayTile.getParty() != null)
					{
						this.tilesWithCharacters.Add(this.currentOverlayTile);
					}
					if (this.currentOverlayTile.getDeadParty() != null)
					{
						this.tilesWithCorpses.Add(this.currentOverlayTile);
					}
					if (MainControl.debugLight && this.currentOverlayTile.isIlluminated())
					{
						TextureTools.applyOverlay(this.illustratedMapAnimated, this.lightBox, num4, num6);
					}
					if (this.currentOverlayTile.shouldTileBeAnimated())
					{
						int subImage = MapIllustrator.CURRENT_FRAME;
						if (this.currentOverlayTile.shouldAnimateAsWater())
						{
							subImage = (num3 + MapIllustrator.CURRENT_WATER_FRAME) % 4;
						}
						if (this.currentOverlayTile.shouldDrawBehind())
						{
							TextureTools.loadTextureDataAndApplyUnderlay(this.currentOverlayTile.getAnimationPath(), subImage, num4, num6, this.illustratedMapAnimated);
						}
						else
						{
							TextureTools.loadTextureDataAndApplyOverlay(this.currentOverlayTile.getAnimationPath(), subImage, num4, num6, this.illustratedMapAnimated);
						}
					}
					if (this.drawTacticalSymbols() && this.currentOverlayTile.canDrawTacticalGrid() && GlobalSettings.getGamePlaySettings().showTacticalGrid())
					{
						if (this.map.isGroundWhite())
						{
							TextureTools.applyOverlay(this.illustratedMapAnimated, this.tacticalGridNonGray, num4, num6);
						}
						else
						{
							TextureTools.applyOverlay(this.illustratedMapAnimated, this.tacticalGrid, num4, num6);
						}
					}
					if (!this.currentOverlayTile.getInventory().isEmpty() && this.currentOverlayTile.isPassable() && (this.currentOverlayTile.getProp() == null || !this.currentOverlayTile.getProp().isContainer()))
					{
						string tileImagePath = this.currentOverlayTile.getInventory().getTileImagePath();
						TextureTools.loadTextureDataAndApplyOverlay(tileImagePath, 0, num4, num6, this.illustratedMapAnimated);
						if (overlandHighlights)
						{
							TextureTools.TextureData subImageTextureData = TextureTools.getSubImageTextureData(0, tileImagePath);
							if (subImageTextureData != null)
							{
								this.UISprites.Add(this.outlineDrawer.getOutline(num4 - 1, num6 - 1, C64Color.Yellow, subImageTextureData));
							}
						}
					}
					if (this.currentOverlayTile.hasVehicle())
					{
						TextureTools.applyOverlay(this.illustratedMapAnimated, this.currentOverlayTile.getVehicle().getIdleModelTexture(), num4 - 2, num6);
						if (overlandHighlights)
						{
							TextureTools.TextureData idleModelTexture = this.currentOverlayTile.getVehicle().getIdleModelTexture();
							if (idleModelTexture != null)
							{
								this.UISprites.Add(this.outlineDrawer.getOutline(num4 - 3, num6 - 1, C64Color.Yellow, idleModelTexture));
							}
						}
					}
					if (combatHighlights && this.map.getPreCombatPlacementTiles() != null && this.map.getPreCombatPlacementTiles().Contains(this.currentOverlayTile) && this.currentOverlayTile.getParty() == null)
					{
						this.UISprites.Add(new TextureTools.Sprite(num4, num6, this.potentialMoveShadow1));
					}
					int x = num4;
					int y = num6;
					if (combatHighlights && currentCharacter != null && currentCharacter.isPC() && currentCharacter.areaEffectSelectionContains(this.currentOverlayTile))
					{
						if (MapIllustrator.CURRENT_FRAME % 2 == 0)
						{
							this.UISpritesTopLayer.Add(new TextureTools.Sprite(x, y, this.targetShadowSpells1));
						}
						else
						{
							this.UISpritesTopLayer.Add(new TextureTools.Sprite(x, y, this.targetShadowSpells2));
						}
					}
					else if (currentCharacter != null && currentCharacter.isPC() && !currentCharacter.hasAreaEffectSelectionSet() && this.currentOverlayTile.isSpotted() && this.map.getMouseTile() == this.currentOverlayTile)
					{
						if (this.currentOverlayTile.getLiveCharacter() != null && this.currentOverlayTile.getLiveCharacter().isHostile())
						{
							if (MapIllustrator.CURRENT_FRAME % 2 == 0)
							{
								this.UISpritesTopLayer.Add(new TextureTools.Sprite(x, y, this.targetShadowOpponent1));
							}
							else
							{
								this.UISpritesTopLayer.Add(new TextureTools.Sprite(x, y, this.targetShadowOpponent2));
							}
						}
						if (this.currentOverlayTile.getLiveCharacter() != null && !this.currentOverlayTile.getLiveCharacter().isHostile() && combatHighlights && this.currentOverlayTile.getLiveCharacter() != currentCharacter)
						{
							if (MapIllustrator.CURRENT_FRAME % 2 == 0)
							{
								this.UISpritesTopLayer.Add(new TextureTools.Sprite(x, y, this.targetShadowSwap1));
							}
							else
							{
								this.UISpritesTopLayer.Add(new TextureTools.Sprite(x, y, this.targetShadowSwap2));
							}
						}
						else if (this.currentOverlayTile.getLiveCharacter() == null && (combatHighlights || this.currentOverlayTile.getPropOrGuestProp() == null || !this.currentOverlayTile.getPropOrGuestProp().shouldBeHighlighted()))
						{
							if (!this.currentOverlayTile.isPassable())
							{
								if (MapIllustrator.CURRENT_FRAME % 2 == 0)
								{
									this.UISpritesTopLayer.Add(new TextureTools.Sprite(x, y, this.targetShadowBlocked1));
								}
								else
								{
									this.UISpritesTopLayer.Add(new TextureTools.Sprite(x, y, this.targetShadowBlocked2));
								}
							}
							else if (MapIllustrator.CURRENT_FRAME % 2 == 0)
							{
								this.UISpritesTopLayer.Add(new TextureTools.Sprite(x, y, this.targetShadow1));
							}
							else
							{
								this.UISpritesTopLayer.Add(new TextureTools.Sprite(x, y, this.targetShadow2));
							}
						}
					}
					this.currentOverlayTile.clearLightEffectsLevel();
					if (this.currentOverlayTile.hasMapObject())
					{
						this.tilesWithMapObjects.Add(this.currentOverlayTile);
					}
				}
				num5--;
				num6 -= 16;
			}
			num3++;
			num4 += 16;
		}
		this.drawProps(num, num2, overlandHighlights, this.UISprites);
		foreach (MapTile mapTile in this.tilesWithMapObjects)
		{
			mapTile.updateMapObject(num, num2, this.illustratedMapAnimated);
			this.map.applyEffectLight(mapTile.getMapObject());
			if ((double)mapTile.getMapObject().getLightEffectStrength() > 0.1)
			{
				this.drawLight(num, num2, mapTile.getMapObject().getVisualEffects().getLightModelPath());
			}
			mapTile.clearMapObjectIfDead();
		}
		foreach (MapTile mapTile2 in this.tilesWithCorpses)
		{
			this.drawParty(mapTile2.getDeadParty(), mapTile2, currentCharacter, num, num2, combatHighlights, overlandHighlights || highlightAllCharacers, this.UISprites);
		}
		foreach (MapTile mapTile3 in this.tilesWithCharacters)
		{
			this.drawParty(mapTile3.getParty(), mapTile3, currentCharacter, num, num2, combatHighlights, overlandHighlights || highlightAllCharacers, this.UISprites);
		}
		foreach (MapTile mapTile4 in this.tilesWithCorpses)
		{
			this.drawPartyFX(mapTile4.getDeadParty(), num, num2, mapTile4);
		}
		foreach (MapTile mapTile5 in this.tilesWithCharacters)
		{
			this.drawPartyFX(mapTile5.getParty(), num, num2, mapTile5);
		}
		this.drawLightProps(num, num2);
		if (GlobalSettings.getDisplaySettings().getWeatherEffects())
		{
			if (!this.map.shouldIDrawOverlay())
			{
				this.weatherEffectControl.applyEffect(this.illustratedMapAnimatedForeground, this.scrollControl.scrollX, this.scrollControl.scrollY, this.illustratedMapTerrainForeground);
			}
			else
			{
				this.weatherEffectControl.applyEffect(this.illustratedMapAnimatedForeground, this.scrollControl.scrollX, this.scrollControl.scrollY, null);
			}
		}
		TextureTools.applyOverlay(this.illustratedMapAnimated, this.illustratedMapTerrainOverlay);
		this.foregroundManager.applyEffect(this.illustratedMapAnimated);
		TextureTools.applyOverlay(this.illustratedMapAnimated, this.illustratedMapAnimatedForeground);
		this.fogOfWarManager.applyEffect(this.illustratedMapAnimated);
		MapIllustrator.DarknessControl.applyDarkness(this.illustratedMapAnimated, this.map);
		TextureTools.applyOverlay(this.illustratedMapAnimated, this.UISprites);
		TextureTools.applyOverlay(this.illustratedMapAnimated, this.UISpritesTopLayer);
		this.genericEffectsControl.updateGlobalEffects(this.illustratedMapAnimated);
		if (this.mapCanvas == null)
		{
			this.mapCanvas = new UIMap();
		}
		this.mapCanvas.updateTexture(12 - this.scrollControl.scrollX, 0 - this.scrollControl.scrollY, this.illustratedMapAnimated);
		return this.mapCanvas;
	}

	// Token: 0x06000E10 RID: 3600 RVA: 0x00041C54 File Offset: 0x0003FE54
	private void drawProps(int xOffset, int yOffset, bool highlightProps, List<TextureTools.Sprite> UISprites)
	{
		foreach (Prop prop in this.tilesWithProps)
		{
			if (!prop.shouldBeRemovedFromGame() && this.map.isId(prop.getContainerMapId()))
			{
				prop.updatePhysics();
				if (!prop.shouldNotBeDrawn())
				{
					TextureTools.TextureData modelTexture = prop.getModelTexture();
					TextureTools.TextureData[,] tileMatrix = prop.getTileMatrix();
					if (modelTexture == null)
					{
						break;
					}
					if (tileMatrix == null)
					{
						break;
					}
					bool flag = false;
					int tileX = prop.getTileX();
					int tileY = prop.getTileY();
					int num = tileX * 16 - xOffset;
					int num2 = tileY * 16 - yOffset;
					for (int i = 0; i < tileMatrix.GetLength(0); i++)
					{
						for (int j = 0; j < tileMatrix.GetLength(1); j++)
						{
							int x = tileX + i;
							int y = tileY + j;
							MapTile tile = this.tileGrid.getTile(x, y);
							if (tileMatrix[i, j] != null && tile != null)
							{
								if (tile.isSpotted())
								{
									flag = true;
								}
								int anchorX = num + i * 16 + prop.getPixelXOffest();
								int anchorY = num2 + j * 16 + prop.getPixelYOffest();
								if (tile.isOverlay() && prop.preferForeground())
								{
									TextureTools.applyOverlay(this.illustratedMapAnimatedForeground, tileMatrix[i, j], anchorX, anchorY);
								}
								else
								{
									TextureTools.applyOverlay(this.illustratedMapAnimated, tileMatrix[i, j], anchorX, anchorY);
								}
							}
						}
					}
					if (modelTexture != null && flag && prop.shouldBeHighlighted() && (highlightProps || prop.getVisualEffects().getForceHighlight() || (this.tileGrid.getMouseTile() != null && this.tileGrid.getMouseTile().getPropOrGuestProp() == prop) || (this.tileGrid.getExamineTile() != null && this.tileGrid.getExamineTile().getPropOrGuestProp() == prop)))
					{
						Color32 color = prop.getHighlightedColor();
						if (prop.getVisualEffects().getForceHighlight() && MapIllustrator.CURRENT_FRAME % 2 == 0)
						{
							color = C64Color.GreenLight;
						}
						UISprites.Add(prop.getOutline(num - 1 + prop.getPixelXOffest(), num2 - 1 + prop.getPixelYOffest(), color, modelTexture));
					}
				}
				prop.updateParticleEffects(xOffset, yOffset, this.illustratedMapAnimated);
			}
		}
	}

	// Token: 0x06000E11 RID: 3601 RVA: 0x00041EC4 File Offset: 0x000400C4
	private void drawLightProps(int xOffset, int yOffset)
	{
		foreach (Prop prop in this.tilesWithLightProps)
		{
			int tileX = prop.getTileX();
			int tileY = prop.getTileY();
			int pixelX = tileX * 16 - xOffset;
			int pixelY = tileY * 16 - yOffset;
			this.drawLight(pixelX, pixelY, prop.getLightModel());
		}
	}

	// Token: 0x06000E12 RID: 3602 RVA: 0x00041F3C File Offset: 0x0004013C
	public void clearTilesWithLightProps()
	{
		this.tilesWithLightProps.Clear();
	}

	// Token: 0x06000E13 RID: 3603 RVA: 0x00041F49 File Offset: 0x00040149
	public void addLightProp(Prop p)
	{
		this.tilesWithLightProps.Add(p);
	}

	// Token: 0x06000E14 RID: 3604 RVA: 0x00041F57 File Offset: 0x00040157
	private bool isCombatActive()
	{
		return MainControl.getDataControl().isCombatActive();
	}

	// Token: 0x06000E15 RID: 3605 RVA: 0x00041F63 File Offset: 0x00040163
	private bool drawTacticalSymbols()
	{
		if (!this.isCombatActive())
		{
			return false;
		}
		if (GlobalSettings.getGamePlaySettings().showTacticalSymbols() && MainControl.getDataControl().getCombatEncounter().isStatePlanningPlayer())
		{
			return !SkaldIO.getHighlightKeyDown();
		}
		return SkaldIO.getHighlightKeyDown();
	}

	// Token: 0x06000E16 RID: 3606 RVA: 0x00041FA4 File Offset: 0x000401A4
	private void drawParty(Party party, MapTile currentTile, Character currentCharacter, int xOffset, int yOffset, bool combatHighlights, bool overlandHighlights, List<TextureTools.Sprite> UISprites)
	{
		if (currentTile == null || party == null || party.isEmpty())
		{
			return;
		}
		bool flag = currentTile.isIlluminated();
		party.setTargetHeight(currentTile.getHeightOffset());
		party.setConcealmentOffset(currentTile.getConcealmentOffset());
		party.setWading(currentTile.wading());
		party.updatePhysics();
		int num = party.getPixelX() - xOffset;
		int num2 = party.getPixelY() - yOffset;
		int x = currentTile.getPixelX() - xOffset;
		int num3 = currentTile.getPixelY() - yOffset;
		Character currentCharacter2 = party.getCurrentCharacter();
		TextureTools.TextureData target = this.illustratedMapAnimated;
		if (this.drawTacticalSymbols() && currentTile.isSpottedOnce() && currentCharacter2.isSpotted() && !party.isPartyDead())
		{
			UISprites.Add(currentCharacter2.getTacticalIcon(x, num3, currentCharacter));
		}
		if (!currentCharacter2.isDead() && this.isCombatActive())
		{
			SkaldPoint2D pixelOffsetForNearbyCharacters = this.map.getPixelOffsetForNearbyCharacters(currentTile.getTileX(), currentTile.getTileY());
			num += pixelOffsetForNearbyCharacters.X;
			num2 += pixelOffsetForNearbyCharacters.Y;
		}
		int num4 = 0 - currentCharacter2.getXOffset();
		int num5 = currentCharacter2.getYOffset();
		int num6 = 2;
		if (currentTile.getTileY() == this.tileGrid.getMapTileHeight() - 1)
		{
			num5 = 0;
		}
		if (currentTile.getTileY() == this.tileGrid.getMapTileHeight() - 1)
		{
			num6 = -3;
		}
		Map mapAboveForDrawing = this.map.getMapAboveForDrawing();
		if (mapAboveForDrawing != null && !currentTile.isOverlay() && mapAboveForDrawing.isVoidTile(currentTile.getTileX(), currentTile.getTileY()) && !party.isPartyDead())
		{
			target = this.illustratedMapAnimatedForeground;
		}
		try
		{
			if (currentCharacter2.getConcealmentOffset() == 0 && !party.isPartyDead() && !party.hasVehicle())
			{
				TextureTools.applyOverlay(target, currentCharacter2.getShadowImage(), num, num2 + num6);
			}
			num2 += party.getBounceOffset();
			if (flag || party.isSpotted() || party.isPC())
			{
				TextureTools.TextureData textureData = party.getModelTexture();
				if (currentTile.isSpottedOnce() && currentCharacter2.isSpotted() && textureData != null && !currentCharacter2.isDead())
				{
					int x2 = num - 1 + num4;
					int y = num2 - 1 + num5;
					bool flag2 = false;
					if (currentCharacter2.getVisualEffects().getForceHighlight())
					{
						flag2 = true;
					}
					else if (overlandHighlights)
					{
						flag2 = true;
					}
					else if (UIInitiativeList.getCurrentCharacter() == currentCharacter2)
					{
						flag2 = true;
					}
					else if (currentCharacter2.getVisualEffects().getForceHighlight())
					{
						flag2 = true;
					}
					else if (currentTile.isOverlay() || this.map.isTileAboveForceOutside(currentTile.getTileX(), currentTile.getTileY()))
					{
						flag2 = true;
					}
					else if (combatHighlights && currentCharacter2 == currentCharacter.getTargetOpponent() && (!currentCharacter.isPC() || !currentCharacter.hasAreaEffectSelectionSet()))
					{
						flag2 = true;
					}
					else if (combatHighlights && currentCharacter2 == currentCharacter)
					{
						flag2 = true;
					}
					else if (currentCharacter2.isHidden())
					{
						flag2 = true;
					}
					else if (currentTile == this.map.getMouseTile() && !SkaldIO.anyKeyDown())
					{
						flag2 = true;
					}
					if (flag2 && !this.drawTacticalSymbols())
					{
						this.drawCharacterOutline(x2, y, currentCharacter2, currentCharacter, textureData);
					}
					int num7 = 0;
					if (combatHighlights && currentCharacter2 == currentCharacter.getTargetOpponent())
					{
						if (this.drawTacticalSymbols())
						{
							UISprites.Add(new TextureTools.Sprite(x, num3 + 14, TextureTools.createShortHealthBar(currentCharacter2, this.lifeBox, 15)));
						}
						else
						{
							UISprites.Add(new TextureTools.Sprite(num, num2 + num5 + 21, TextureTools.createShortHealthBar(currentCharacter2, this.lifeBox, 15)));
						}
						num7 = 4;
					}
					int y2 = num2 + num5 + 23 + num7;
					if (currentCharacter2.isFlanked())
					{
						UISprites.Add(new TextureTools.Sprite(num + 3, y2, this.flankedIcon));
					}
					if (currentCharacter2 == currentCharacter && currentCharacter2.isPC() && currentCharacter2.getChargeCounterValue() > 0)
					{
						for (int i = 0; i < currentCharacter2.getChargeCounterValue(); i++)
						{
							UISprites.Add(new TextureTools.Sprite(num + 4 + 3 * i, y2, this.momentumIcon));
						}
					}
				}
				if (currentCharacter2.getVisualEffects().getShaken())
				{
					this.genericEffectsControl.setScreenShake(10);
				}
				if (!currentCharacter2.isHidden())
				{
					if (currentTile.isConcealment())
					{
						int num8 = currentCharacter2.getConcealmentOffset();
						if (currentTile.wading())
						{
							int num9 = (currentTile.getTileX() + MapIllustrator.CURRENT_WATER_FRAME) % 4;
							if (num9 == 1 || num9 == 3)
							{
								num8++;
							}
							else if (num9 == 2)
							{
								num8 += 2;
							}
							textureData.drawWaterEdge(num8);
						}
						else
						{
							textureData.clearPartially(num8);
						}
					}
					if (currentCharacter2.getVisualEffects().getNegativeFlashCounter())
					{
						TextureTools.applyNegativeOverlaySpell(target, textureData, num + num4, num2 + num5);
					}
					else if (currentCharacter2.getVisualEffects().getPositiveFlashCounter())
					{
						TextureTools.applyPositiveOverlaySpell(target, textureData, num + num4, num2 + num5);
					}
					else if (currentCharacter2.getVisualEffects().getDamageFlashCounter())
					{
						TextureTools.applyNegativeOverlayDamage(target, textureData, num + num4, num2 + num5);
					}
					else
					{
						TextureTools.applyOverlay(target, textureData, num + num4, num2 + num5);
					}
				}
			}
			else if (!currentTile.isConcealment() && currentTile.isSpotted())
			{
				string path = "Images/Models/unseenIcon";
				TextureTools.TextureData textureData = TextureTools.getSubImageTextureData(MapIllustrator.CURRENT_FRAME, path);
				UISprites.Add(new TextureTools.Sprite(num, num2 + num5, textureData));
			}
		}
		catch (Exception ex)
		{
			MainControl.logError(((ex != null) ? ex.ToString() : null) + "\n" + party.printPositions());
		}
		try
		{
			if (currentCharacter2.isPC() && currentCharacter2 == currentCharacter && currentCharacter2.isHidden())
			{
				Color32 textColor = C64Color.GreenLight;
				if (currentCharacter2.isBeingObserved())
				{
					textColor = C64Color.Yellow;
				}
				TextureTools.TextureData texture = StringPrinter.bakeFancyString(currentCharacter2.getHiddenDegree().ToString() + "% Stealth", textColor, C64Color.BrownLight);
				UISprites.Add(new TextureTools.Sprite(num - 16, num2 + num5 + 21, texture));
			}
			if (currentCharacter.isCharacterTarget(currentCharacter2) && currentCharacter.isPC() && this.getCrosshair(currentCharacter) != "" && !currentCharacter.hasAreaEffectSelectionSet())
			{
				this.crossHair = TextureTools.getSubImageTextureData(MapIllustrator.CURRENT_FRAME, this.getCrosshair(currentCharacter));
				if (this.drawTacticalSymbols() && !party.isPartyDead())
				{
					UISprites.Add(new TextureTools.Sprite(x, num3, this.crossHair));
				}
				else
				{
					UISprites.Add(new TextureTools.Sprite(num, num2 + num5, this.crossHair));
				}
			}
			if (currentCharacter2 == currentCharacter)
			{
				this.drawCharacterPath(party, UISprites, combatHighlights);
			}
			string andClearTacticalHoverText = currentCharacter2.getAndClearTacticalHoverText();
			if (andClearTacticalHoverText != "")
			{
				HoverElementControl.addTacticalHoverTextFlashing(andClearTacticalHoverText, num + 8, num2 + 40);
			}
			if (!HoverElementControl.hasTacticalText())
			{
				foreach (TextureTools.Sprite sprite in currentCharacter2.getBarkSprites())
				{
					sprite.x += num;
					sprite.y += num2;
					if (currentTile.isSpottedOnce() && currentCharacter2.isSpotted())
					{
						UISprites.Add(sprite);
					}
				}
			}
		}
		catch (Exception ex2)
		{
			MainControl.logError(((ex2 != null) ? ex2.ToString() : null) + "\n" + party.printPositions());
		}
		if (currentCharacter2.getVisualEffects().effectsAreIlluminatingArea())
		{
			this.drawLight(num, num2, currentCharacter2.getVisualEffects().getLightModelPath());
			this.map.applyEffectLight(currentCharacter2);
		}
		if (currentCharacter2.getLightStrength() > 0f)
		{
			this.drawLight(num, num2, currentCharacter2.getLightImage());
		}
	}

	// Token: 0x06000E17 RID: 3607 RVA: 0x00042718 File Offset: 0x00040918
	private void drawPartyFX(Party party, int xOffset, int yOffset, MapTile currentTile)
	{
		if (party == null || party.isEmpty())
		{
			return;
		}
		TextureTools.TextureData targetTexture = this.illustratedMapAnimated;
		Character currentCharacter = party.getCurrentCharacter();
		Map mapAboveForDrawing = this.map.getMapAboveForDrawing();
		if (mapAboveForDrawing != null && !currentTile.isOverlay() && mapAboveForDrawing.isVoidTile(currentTile.getTileX(), currentTile.getTileY()))
		{
			targetTexture = this.illustratedMapAnimatedForeground;
		}
		try
		{
			currentCharacter.updateParticleEffects(xOffset, yOffset, targetTexture);
		}
		catch (Exception ex)
		{
			MainControl.logError(((ex != null) ? ex.ToString() : null) + "\n" + party.printPositions());
		}
	}

	// Token: 0x06000E18 RID: 3608 RVA: 0x000427B4 File Offset: 0x000409B4
	private void drawCharacterOutline(int x, int y, Character character, Character currentCharacter, TextureTools.TextureData partyImg)
	{
		this.UISprites.Add(character.getOutline(x, y, currentCharacter, partyImg));
	}

	// Token: 0x06000E19 RID: 3609 RVA: 0x000427D0 File Offset: 0x000409D0
	private void drawCharacterPath(Party party, List<TextureTools.Sprite> UISprites, bool drawUI)
	{
		if (!party.navigationCourseHasNodes())
		{
			return;
		}
		if (!party.isPC())
		{
			return;
		}
		NavigationCourse navigationCourse = party.GetNavigationCourse();
		if (!party.getCurrentCharacter().hasRemainingCombatMovesOrAttacks())
		{
			return;
		}
		int num = (this.tileGrid.getViewportX() - MapIllustrator.ScreenDimensions.getIllustratedMapHalfWidth() - 1) * 16;
		int num2 = (this.tileGrid.getViewportY() - MapIllustrator.ScreenDimensions.getIllustratedMapHalfHeight() - 1) * 16;
		for (int i = 0; i < navigationCourse.getLength(); i++)
		{
			Point point = navigationCourse.course[i];
			if (this.map.isPointInView(point.X, point.Y, 0))
			{
				MapTile tile = this.tileGrid.getTile(point.X, point.Y);
				if (i == navigationCourse.getLength() - 1 && (tile.getParty() != null || (tile.getPropOrGuestProp() != null && tile.getPropOrGuestProp().shouldBeHighlighted())))
				{
					return;
				}
				TextureTools.TextureData texture;
				if (i >= party.getCurrentCharacter().getExactRemainingCombatMovesIncludingAttacks() && drawUI)
				{
					texture = this.pathPipRed;
				}
				else
				{
					texture = this.pathPipGreen;
				}
				UISprites.Add(new TextureTools.Sprite(tile.getPixelX() - num + 5, tile.getPixelY() - num2 + 5, texture));
			}
		}
	}

	// Token: 0x06000E1A RID: 3610 RVA: 0x00042908 File Offset: 0x00040B08
	private void drawLight(int pixelX, int pixelY, string template)
	{
		if (template == "")
		{
			return;
		}
		try
		{
			int x = pixelX + 8;
			int y = pixelY + 8;
			if (GlobalSettings.getDisplaySettings().getFancyLightModels())
			{
				TextureTools.loadTextureDataAndApplyOverlayAddativeCentered("Images/Light/" + template, x, y, this.illustratedMapAnimated);
			}
		}
		catch (Exception obj)
		{
			MainControl.logError(obj);
		}
	}

	// Token: 0x06000E1B RID: 3611 RVA: 0x0004296C File Offset: 0x00040B6C
	private string getCrosshair(Character currentCharacter)
	{
		string result = "Images/GUIIcons/CrosshairSword";
		if (currentCharacter != null)
		{
			if (currentCharacter.getCrosshairPath() == "")
			{
				return "";
			}
			result = "Images/GUIIcons/" + currentCharacter.getCrosshairPath();
		}
		return result;
	}

	// Token: 0x06000E1C RID: 3612 RVA: 0x000429AC File Offset: 0x00040BAC
	public bool isFieldInView(int x, int y, int width, int height, int padding)
	{
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				if (this.map.isPointInView(x + i, y + j, padding))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000E1D RID: 3613 RVA: 0x000429EC File Offset: 0x00040BEC
	public void makeTerrainMap()
	{
		this.illustratedMapTerrainForeground.clear();
		this.illustratedMapTerrain.clear();
		this.illustratedMapTerrainOverlay.clear();
		Map mapAboveForDrawing = this.map.getMapAboveForDrawing();
		this.findAllTilesWithProps();
		int num = this.tileGrid.getViewportX() - (MapIllustrator.ScreenDimensions.getIllustratedMapHalfWidth() + 1);
		for (int i = 0; i < MapIllustrator.ScreenDimensions.getIllustratedMapWidthAndRim(); i++)
		{
			int num2 = this.tileGrid.getViewportY() - (MapIllustrator.ScreenDimensions.getIllustratedMapHalfHeight() + 1);
			int anchorX = i * 16;
			for (int j = 0; j < MapIllustrator.ScreenDimensions.getIllustratedMapHeightAndRim(); j++)
			{
				int anchorY = j * 16;
				TextureTools.TextureData terrainTileImage = this.getTerrainTileImage(num, num2);
				if (terrainTileImage != null)
				{
					TextureTools.applyOverlay(this.illustratedMapTerrain, terrainTileImage, anchorX, anchorY);
				}
				TextureTools.TextureData terrainTileOverlayImage = this.getTerrainTileOverlayImage(num, num2);
				if (terrainTileOverlayImage != null)
				{
					TextureTools.applyOverlay(this.illustratedMapTerrainOverlay, terrainTileOverlayImage, anchorX, anchorY);
				}
				if (this.map.drawAsCube && mapAboveForDrawing != null && mapAboveForDrawing.getTile(num, num2) != null && !mapAboveForDrawing.isVoidTile(num, num2))
				{
					TextureTools.TextureData terrainTileImage2 = mapAboveForDrawing.getTerrainTileImage(num, num2);
					if (terrainTileImage2 != null)
					{
						TextureTools.applyOverlay(this.illustratedMapTerrainForeground, terrainTileImage2, anchorX, anchorY);
					}
				}
				num2++;
			}
			num++;
		}
		this.illustratedMapTerrainOverlay.compress();
		this.illustratedMapTerrain.compress();
		this.illustratedMapTerrainForeground.compress();
		if (!this.map.shouldIDrawOverlay())
		{
			this.foregroundManager.setFadeOutImage(this.illustratedMapTerrainForeground);
			return;
		}
		this.foregroundManager.setFadeInImage(this.illustratedMapTerrainForeground);
	}

	// Token: 0x06000E1E RID: 3614 RVA: 0x00042B78 File Offset: 0x00040D78
	private void findAllTilesWithProps()
	{
		this.tilesWithProps = new List<Prop>();
		List<Prop> list = new List<Prop>();
		using (List<SkaldWorldObject>.Enumerator enumerator = GameData.getPropsByMap(this.map.getId()).GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				SkaldWorldObject skaldWorldObject = enumerator.Current;
				Prop prop = (Prop)skaldWorldObject;
				if (this.isFieldInView(prop.getTileX(), prop.getTileY(), prop.getTileDrawWidth(), prop.getTileDrawHeight(), 2))
				{
					list.Add(prop);
				}
			}
			goto IL_D1;
		}
		IL_77:
		Prop prop2 = null;
		foreach (Prop prop3 in list)
		{
			if (prop2 == null || prop3.getTileY() >= prop2.getTileY())
			{
				prop2 = prop3;
			}
		}
		if (prop2 != null)
		{
			this.tilesWithProps.Add(prop2);
			list.Remove(prop2);
		}
		IL_D1:
		if (list.Count <= 0)
		{
			return;
		}
		goto IL_77;
	}

	// Token: 0x06000E1F RID: 3615 RVA: 0x00042C7C File Offset: 0x00040E7C
	private TextureTools.TextureData getTerrainTileOverlayImage(int x, int y)
	{
		if (!this.tileGrid.isTileValid(x, y) || !this.tileGrid.getTile(x, y).isOverlay())
		{
			return null;
		}
		return this.tileGrid.getTile(x, y).getOverlayTileImage();
	}

	// Token: 0x06000E20 RID: 3616 RVA: 0x00042CB8 File Offset: 0x00040EB8
	public TextureTools.TextureData getTerrainTileImage(int x, int y)
	{
		if (!this.tileGrid.isTileValid(x, y))
		{
			return null;
		}
		MapTile tile = this.tileGrid.getTile(x, y);
		if (tile.isVoidTile())
		{
			Map mapBelow = this.map.getMapBelow();
			if (mapBelow != null)
			{
				tile = mapBelow.getTile(x, y);
			}
			return tile.getBakedImageStack();
		}
		return tile.getMainTileImage();
	}

	// Token: 0x06000E21 RID: 3617 RVA: 0x00042D11 File Offset: 0x00040F11
	public void cacheFogOfWarOverlay()
	{
		this.fogOfWarManager.drawOldImage();
	}

	// Token: 0x06000E22 RID: 3618 RVA: 0x00042D1E File Offset: 0x00040F1E
	public void makeFogOfWarOverlay()
	{
		this.fogOfWarManager.setImages();
	}

	// Token: 0x06000E23 RID: 3619 RVA: 0x00042D2B File Offset: 0x00040F2B
	public void clearOverlays()
	{
		this.fogOfWarManager.clearImage();
	}

	// Token: 0x0400036A RID: 874
	private MapIllustrator.FogOfWarManager fogOfWarManager;

	// Token: 0x0400036B RID: 875
	private MapIllustrator.ForegroundManager foregroundManager;

	// Token: 0x0400036C RID: 876
	private TextureTools.TextureData illustratedMapTerrain;

	// Token: 0x0400036D RID: 877
	private TextureTools.TextureData illustratedMapTerrainOverlay;

	// Token: 0x0400036E RID: 878
	private TextureTools.TextureData illustratedMapAnimated;

	// Token: 0x0400036F RID: 879
	private TextureTools.TextureData illustratedMapAnimatedForeground;

	// Token: 0x04000370 RID: 880
	private TextureTools.TextureData illustratedMapTerrainForeground;

	// Token: 0x04000371 RID: 881
	private TextureTools.TextureData lifeBox;

	// Token: 0x04000372 RID: 882
	private TextureTools.TextureData crossHair;

	// Token: 0x04000373 RID: 883
	private TextureTools.TextureData pathPipGreen;

	// Token: 0x04000374 RID: 884
	private TextureTools.TextureData pathPipRed;

	// Token: 0x04000375 RID: 885
	private TextureTools.TextureData potentialMoveShadow1;

	// Token: 0x04000376 RID: 886
	private TextureTools.TextureData targetShadow1;

	// Token: 0x04000377 RID: 887
	private TextureTools.TextureData targetShadow2;

	// Token: 0x04000378 RID: 888
	private TextureTools.TextureData targetShadowSwap1;

	// Token: 0x04000379 RID: 889
	private TextureTools.TextureData targetShadowSwap2;

	// Token: 0x0400037A RID: 890
	private TextureTools.TextureData targetShadowBlocked1;

	// Token: 0x0400037B RID: 891
	private TextureTools.TextureData targetShadowBlocked2;

	// Token: 0x0400037C RID: 892
	private TextureTools.TextureData targetShadowInteractable1;

	// Token: 0x0400037D RID: 893
	private TextureTools.TextureData targetShadowInteractable2;

	// Token: 0x0400037E RID: 894
	private TextureTools.TextureData targetShadowOpponent1;

	// Token: 0x0400037F RID: 895
	private TextureTools.TextureData targetShadowOpponent2;

	// Token: 0x04000380 RID: 896
	private TextureTools.TextureData targetShadowSpells1;

	// Token: 0x04000381 RID: 897
	private TextureTools.TextureData targetShadowSpells2;

	// Token: 0x04000382 RID: 898
	private TextureTools.TextureData tacticalGrid;

	// Token: 0x04000383 RID: 899
	private TextureTools.TextureData tacticalGridNonGray;

	// Token: 0x04000384 RID: 900
	private TextureTools.TextureData lightBox;

	// Token: 0x04000385 RID: 901
	private TextureTools.TextureData momentumIcon;

	// Token: 0x04000386 RID: 902
	private TextureTools.TextureData flankedIcon;

	// Token: 0x04000387 RID: 903
	private ParticleSystem particleSystem;

	// Token: 0x04000388 RID: 904
	private GenericEffectsControl genericEffectsControl;

	// Token: 0x04000389 RID: 905
	private WeatherEffectsControl weatherEffectControl;

	// Token: 0x0400038A RID: 906
	private List<MapTile> tilesWithCharacters = new List<MapTile>();

	// Token: 0x0400038B RID: 907
	private List<MapTile> tilesWithCorpses = new List<MapTile>();

	// Token: 0x0400038C RID: 908
	private List<MapTile> tilesWithMapObjects = new List<MapTile>();

	// Token: 0x0400038D RID: 909
	private List<Prop> tilesWithProps;

	// Token: 0x0400038E RID: 910
	private List<Prop> tilesWithLightProps = new List<Prop>();

	// Token: 0x0400038F RID: 911
	private List<TextureTools.Sprite> UISprites = new List<TextureTools.Sprite>();

	// Token: 0x04000390 RID: 912
	private List<TextureTools.Sprite> UISpritesTopLayer = new List<TextureTools.Sprite>();

	// Token: 0x04000391 RID: 913
	private MapTile currentOverlayTile;

	// Token: 0x04000392 RID: 914
	public static int CURRENT_WATER_FRAME = 0;

	// Token: 0x04000393 RID: 915
	public static int CURRENT_FRAME = 0;

	// Token: 0x04000394 RID: 916
	public static int SCROLL_SPEED = 2;

	// Token: 0x04000395 RID: 917
	private AnimationStrip globalAnimationControl = new AnimationStrip(new int[]
	{
		0,
		1,
		2,
		3
	}, 4f, true);

	// Token: 0x04000396 RID: 918
	private AnimationStrip globalWaterAnimationControl = new AnimationStrip(new int[]
	{
		0,
		1,
		2,
		3
	}, 6f, true);

	// Token: 0x04000397 RID: 919
	private MapIllustrator.ScrollControl scrollControl;

	// Token: 0x04000398 RID: 920
	private MapTileGrid tileGrid;

	// Token: 0x04000399 RID: 921
	private Map map;

	// Token: 0x0400039A RID: 922
	private UIMap mapCanvas;

	// Token: 0x0400039B RID: 923
	private ImageOutlineDrawer outlineDrawer = new ImageOutlineDrawer();

	// Token: 0x0200024B RID: 587
	private abstract class TransitionManager
	{
		// Token: 0x0600194B RID: 6475 RVA: 0x0006EE12 File Offset: 0x0006D012
		public TransitionManager(int _tileWidth, int _tileHeight)
		{
			this.tileWidth = _tileWidth;
			this.tileHeight = _tileHeight;
			this.pixelWidth = this.tileWidth * 16;
			this.pixelHeight = this.tileHeight * 16;
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x0006EE4E File Offset: 0x0006D04E
		public virtual void setImages(TextureTools.TextureData _currentImage, TextureTools.TextureData _targetImage)
		{
			this.currentImage = _currentImage;
			this.targetImage = _targetImage;
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x0006EE5E File Offset: 0x0006D05E
		public void clearImage()
		{
			if (this.targetImage != null)
			{
				this.currentImage = this.targetImage;
				this.targetImage = null;
			}
		}

		// Token: 0x0600194E RID: 6478
		public abstract void applyEffect(TextureTools.TextureData input);

		// Token: 0x040008F8 RID: 2296
		protected TextureTools.TextureData targetImage;

		// Token: 0x040008F9 RID: 2297
		protected TextureTools.TextureData currentImage;

		// Token: 0x040008FA RID: 2298
		protected int pixelWidth;

		// Token: 0x040008FB RID: 2299
		protected int pixelHeight;

		// Token: 0x040008FC RID: 2300
		protected int tileWidth;

		// Token: 0x040008FD RID: 2301
		protected int tileHeight;

		// Token: 0x040008FE RID: 2302
		protected int timer = -50;
	}

	// Token: 0x0200024C RID: 588
	private class ForegroundManager : MapIllustrator.TransitionManager
	{
		// Token: 0x0600194F RID: 6479 RVA: 0x0006EE7B File Offset: 0x0006D07B
		public ForegroundManager(int _tileWidth, int _tileHeight) : base(_tileWidth, _tileHeight)
		{
			this.blankImage = new TextureTools.TextureData(this.pixelWidth, this.pixelHeight);
			this.filledInImage = new TextureTools.TextureData(this.pixelWidth, this.pixelHeight);
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x0006EEB3 File Offset: 0x0006D0B3
		private TextureTools.TextureData getBlankTexture()
		{
			this.blankImage.clear();
			return this.blankImage;
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x0006EEC8 File Offset: 0x0006D0C8
		public void setFadeInImage(TextureTools.TextureData target)
		{
			target.copyToTarget(this.filledInImage);
			if (!this.fadeIn)
			{
				this.fadeIn = true;
				this.setImages(this.getBlankTexture(), this.filledInImage);
				return;
			}
			this.targetImage = null;
			this.currentImage = this.filledInImage;
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x0006EF18 File Offset: 0x0006D118
		public void setFadeOutImage(TextureTools.TextureData target)
		{
			if (this.fadeIn)
			{
				target.copyToTarget(this.filledInImage);
				this.fadeIn = false;
				this.setImages(this.filledInImage, this.getBlankTexture());
				return;
			}
			this.setImages(this.getBlankTexture(), null);
			this.currentImage.compress();
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x0006EF6C File Offset: 0x0006D16C
		public override void applyEffect(TextureTools.TextureData input)
		{
			if (this.targetImage == null)
			{
				TextureTools.applyOverlay(input, this.currentImage);
				return;
			}
			bool flag = true;
			for (int i = 0; i < this.currentImage.colors.Length; i++)
			{
				if ((this.targetImage.isPixelTransparent(i) && !this.currentImage.isPixelTransparent(i)) || (!this.targetImage.isPixelTransparent(i) && this.currentImage.isPixelTransparent(i)))
				{
					if (Random.Range(0, 100) < 15)
					{
						this.currentImage.colors[i] = this.targetImage.colors[i];
					}
					flag = false;
				}
				if (!this.currentImage.isPixelTransparent(i))
				{
					input.colors[i] = this.currentImage.colors[i];
				}
			}
			if (flag)
			{
				base.clearImage();
				this.currentImage.compress();
			}
		}

		// Token: 0x040008FF RID: 2303
		private bool fadeIn;

		// Token: 0x04000900 RID: 2304
		private TextureTools.TextureData filledInImage;

		// Token: 0x04000901 RID: 2305
		private TextureTools.TextureData blankImage;
	}

	// Token: 0x0200024D RID: 589
	private abstract class CellularOverlayManager : MapIllustrator.TransitionManager
	{
		// Token: 0x06001954 RID: 6484 RVA: 0x0006F058 File Offset: 0x0006D258
		protected CellularOverlayManager(int _tileWidth, int _tileHeight, Map _map, MapTileGrid _tileGrid) : base(_tileWidth, _tileHeight)
		{
			this.map = _map;
			this.tileGrid = _tileGrid;
			this.oldImage = new TextureTools.TextureData(this.pixelWidth, this.pixelHeight);
			this.newImage = new TextureTools.TextureData(this.pixelWidth, this.pixelHeight);
			this.deltaTexture = new TextureTools.TextureData(this.pixelWidth, this.pixelHeight);
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x0006F0C9 File Offset: 0x0006D2C9
		public void drawOldImage()
		{
			this.drawTargetImage(this.oldImage);
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x0006F0D7 File Offset: 0x0006D2D7
		public void drawNewImage()
		{
			this.drawTargetImage(this.newImage);
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x0006F0E5 File Offset: 0x0006D2E5
		public void setImages()
		{
			this.drawNewImage();
			this.setImages(this.oldImage, this.newImage);
		}

		// Token: 0x06001958 RID: 6488
		protected abstract void drawTargetImage(TextureTools.TextureData target);

		// Token: 0x06001959 RID: 6489 RVA: 0x0006F100 File Offset: 0x0006D300
		public override void applyEffect(TextureTools.TextureData input)
		{
			if (this.targetImage == null)
			{
				TextureTools.applyNightOverlay(input, this.currentImage);
				return;
			}
			bool flag = true;
			for (int i = 0; i < this.deltaTexture.compressionOffsetArray.Length; i += 2)
			{
				int num = this.deltaTexture.compressionOffsetArray[i + 1];
				for (int j = this.deltaTexture.compressionOffsetArray[i]; j < num; j++)
				{
					bool flag2 = this.targetImage.isPixelTransparent(j);
					bool flag3 = this.currentImage.isPixelTransparent(j);
					bool flag4 = !flag3;
					if (flag2 && !flag3)
					{
						if (this.testUndrawnNeighbours(this.currentImage, j))
						{
							this.currentImage.colors[j] = this.targetImage.colors[j];
						}
						flag4 = true;
						flag = false;
					}
					else if (!flag2 && flag3)
					{
						if (this.testDrawnNeighbours(this.currentImage, j))
						{
							this.currentImage.colors[j] = this.targetImage.colors[j];
						}
						flag = false;
					}
					if (flag4)
					{
						input.colors[j] = this.currentImage.colors[j];
					}
				}
			}
			if (flag)
			{
				base.clearImage();
				this.currentImage.compress();
			}
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x0006F245 File Offset: 0x0006D445
		public override void setImages(TextureTools.TextureData _currentImage, TextureTools.TextureData _targetImage)
		{
			base.setImages(_currentImage, _targetImage);
			this.currentImage.clearCompression();
			this.setDeltaTexture();
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x0006F260 File Offset: 0x0006D460
		private void setDeltaTexture()
		{
			this.deltaTexture.clear();
			for (int i = 0; i < this.targetImage.colors.Length; i++)
			{
				if (!this.targetImage.isPixelTransparent(i) || !this.currentImage.isPixelTransparent(i))
				{
					this.deltaTexture.colors[i] = C64Color.Black;
				}
			}
			this.deltaTexture.compress();
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x0006F2D0 File Offset: 0x0006D4D0
		private bool testUndrawnNeighbours(TextureTools.TextureData workspace, int i)
		{
			int num = this.baseChance;
			if (workspace.isPixelTransparent(i - 1))
			{
				num += this.neighbourWeight;
			}
			if (workspace.isPixelTransparent(i + 1))
			{
				num += this.neighbourWeight;
			}
			if (workspace.isPixelTransparent(i + workspace.width))
			{
				num += this.neighbourWeight;
			}
			if (workspace.isPixelTransparent(i - workspace.width))
			{
				num += this.neighbourWeight;
			}
			return num != 0 && Random.Range(0, 100) < num;
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x0006F350 File Offset: 0x0006D550
		private bool testDrawnNeighbours(TextureTools.TextureData workspace, int i)
		{
			int num = this.baseChance;
			if (i != 0 && !workspace.isPixelTransparent(i - 1))
			{
				num += this.neighbourWeight;
			}
			if (!workspace.isPixelTransparent(i + 1))
			{
				num += this.neighbourWeight;
			}
			if (!workspace.isPixelTransparent(i + workspace.width))
			{
				num += this.neighbourWeight;
			}
			if (!workspace.isPixelTransparent(i - workspace.width))
			{
				num += this.neighbourWeight;
			}
			return num != 0 && Random.Range(0, 100) < num;
		}

		// Token: 0x04000902 RID: 2306
		protected Map map;

		// Token: 0x04000903 RID: 2307
		protected MapTileGrid tileGrid;

		// Token: 0x04000904 RID: 2308
		protected int neighbourWeight = 25;

		// Token: 0x04000905 RID: 2309
		protected int baseChance;

		// Token: 0x04000906 RID: 2310
		private TextureTools.TextureData deltaTexture;

		// Token: 0x04000907 RID: 2311
		protected TextureTools.TextureData oldImage;

		// Token: 0x04000908 RID: 2312
		protected TextureTools.TextureData newImage;
	}

	// Token: 0x0200024E RID: 590
	private class DarknessManager : MapIllustrator.CellularOverlayManager
	{
		// Token: 0x0600195E RID: 6494 RVA: 0x0006F3D1 File Offset: 0x0006D5D1
		public DarknessManager(int _tileWidth, int _tileHeight, Map _map, MapTileGrid _tileGrid) : base(_tileWidth, _tileHeight, _map, _tileGrid)
		{
			this.neighbourWeight = 10;
			this.baseChance = 5;
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x0006F3F0 File Offset: 0x0006D5F0
		protected override void drawTargetImage(TextureTools.TextureData target)
		{
			int num = this.tileGrid.getViewportX() - (MapIllustrator.ScreenDimensions.getIllustratedMapHalfWidth() + 1);
			target.clear();
			Map mapAboveForDrawing = this.map.getMapAboveForDrawing();
			for (int i = 0; i < MapIllustrator.ScreenDimensions.getIllustratedMapWidthAndRim(); i++)
			{
				int num2 = this.tileGrid.getViewportY() - (MapIllustrator.ScreenDimensions.getIllustratedMapHalfHeight() + 1);
				int x = i * 16;
				for (int j = 0; j < MapIllustrator.ScreenDimensions.getIllustratedMapHeightAndRim(); j++)
				{
					int y = j * 16;
					MapTile tile = this.tileGrid.getTile(num, num2);
					bool flag = false;
					if (!this.map.shouldIDrawOverlay())
					{
						if (tile != null && !tile.isIlluminated())
						{
							flag = true;
						}
					}
					else if (mapAboveForDrawing != null && mapAboveForDrawing.getTile(num, num2) != null && !mapAboveForDrawing.isVoidTile(num, num2))
					{
						if (!mapAboveForDrawing.getTile(num, num2).isIlluminated())
						{
							flag = true;
						}
					}
					else if (tile == null || !tile.isIlluminated())
					{
						flag = true;
					}
					if (flag)
					{
						TextureTools.loadTextureDataAndApplyOverlay("Images/Tiles/DarknessMask", 0, x, y, target);
					}
					else
					{
						int num3 = this.tileGrid.findDarknessSubImage(num, num2);
						if (num3 != 6)
						{
							TextureTools.loadTextureDataAndApplyOverlay("Images/Tiles/DarknessMask", num3, x, y, target);
						}
					}
					num2++;
				}
				num++;
			}
		}

		// Token: 0x04000909 RID: 2313
		private const string DARKNESS_PATH = "Images/Tiles/DarknessMask";
	}

	// Token: 0x0200024F RID: 591
	private class FogOfWarManager : MapIllustrator.CellularOverlayManager
	{
		// Token: 0x06001960 RID: 6496 RVA: 0x0006F523 File Offset: 0x0006D723
		public FogOfWarManager(int _tileWidth, int _tileHeight, Map _map, MapTileGrid _tileGrid) : base(_tileWidth, _tileHeight, _map, _tileGrid)
		{
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x0006F530 File Offset: 0x0006D730
		protected override void drawTargetImage(TextureTools.TextureData target)
		{
			int num = this.tileGrid.getViewportX() - (MapIllustrator.ScreenDimensions.getIllustratedMapHalfWidth() + 1);
			target.clear();
			Map mapAboveForDrawing = this.map.getMapAboveForDrawing();
			for (int i = 0; i < MapIllustrator.ScreenDimensions.getIllustratedMapWidthAndRim(); i++)
			{
				int num2 = this.tileGrid.getViewportY() - (MapIllustrator.ScreenDimensions.getIllustratedMapHalfHeight() + 1);
				int x = i * 16;
				for (int j = 0; j < MapIllustrator.ScreenDimensions.getIllustratedMapHeightAndRim(); j++)
				{
					int y = j * 16;
					MapTile tile = this.tileGrid.getTile(num, num2);
					bool flag = false;
					if (!this.map.shouldIDrawOverlay())
					{
						if (tile == null || !tile.isSpotted())
						{
							flag = true;
						}
					}
					else if (mapAboveForDrawing != null && mapAboveForDrawing.getTile(num, num2) != null && !mapAboveForDrawing.isVoidTile(num, num2))
					{
						if (!mapAboveForDrawing.getTile(num, num2).isSpotted())
						{
							flag = true;
						}
					}
					else if (tile == null || !tile.isSpotted())
					{
						flag = true;
					}
					if (flag)
					{
						TextureTools.loadTextureDataAndApplyOverlay("Images/Tiles/FogOfWar", 0, x, y, target);
					}
					else
					{
						int num3 = this.tileGrid.findFogSubImage(num, num2);
						if (num3 != 6)
						{
							TextureTools.loadTextureDataAndApplyOverlay("Images/Tiles/FogOfWar", num3, x, y, target);
						}
						else
						{
							if (num == 0)
							{
								TextureTools.loadTextureDataAndApplyOverlay("Images/Tiles/EdgeFog", 5, x, y, target);
							}
							else if (num == this.tileGrid.getMapTileWidth() - 1)
							{
								TextureTools.loadTextureDataAndApplyOverlay("Images/Tiles/EdgeFog", 7, x, y, target);
							}
							if (num2 == 0)
							{
								TextureTools.loadTextureDataAndApplyOverlay("Images/Tiles/EdgeFog", 2, x, y, target);
							}
							else if (num2 == this.tileGrid.getMapTileHeight() - 1)
							{
								TextureTools.loadTextureDataAndApplyOverlay("Images/Tiles/EdgeFog", 10, x, y, target);
							}
						}
					}
					num2++;
				}
				num++;
			}
		}

		// Token: 0x0400090A RID: 2314
		private const string FOG_PATH = "Images/Tiles/FogOfWar";

		// Token: 0x0400090B RID: 2315
		private const string EDGE_FOG_PATH = "Images/Tiles/EdgeFog";
	}

	// Token: 0x02000250 RID: 592
	private static class DarknessControl
	{
		// Token: 0x06001962 RID: 6498 RVA: 0x0006F6D4 File Offset: 0x0006D8D4
		public static void applyDarkness(TextureTools.TextureData target, Map map)
		{
			int num = map.getViewportX() - (MapIllustrator.ScreenDimensions.getIllustratedMapHalfWidth() + 1);
			for (int i = 0; i < MapIllustrator.ScreenDimensions.getIllustratedMapWidthAndRim(); i++)
			{
				int num2 = map.getViewportY() - (MapIllustrator.ScreenDimensions.getIllustratedMapHalfHeight() + 1);
				int x = i * 16;
				for (int j = 0; j < MapIllustrator.ScreenDimensions.getIllustratedMapHeightAndRim(); j++)
				{
					int y = j * 16;
					if (GlobalSettings.getDisplaySettings().getFancyLighting())
					{
						float lightLevelForTile = map.getLightLevelForTile(num, num2);
						if (lightLevelForTile < 1f)
						{
							MapIllustrator.DarknessControl.colorInTile(target, x, y, lightLevelForTile);
						}
					}
					num2++;
				}
				num++;
			}
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x0006F764 File Offset: 0x0006D964
		private static void colorInTile(TextureTools.TextureData target, int x, int y, float level)
		{
			float num = MapIllustrator.DarknessControl.baseDarknessScalar + (1f - MapIllustrator.DarknessControl.baseDarknessScalar) * level;
			float num2 = MapIllustrator.DarknessControl.baseDarknessBlueScalar + (1f - MapIllustrator.DarknessControl.baseDarknessBlueScalar) * level;
			int bufferIndex = Mathf.RoundToInt(num * 256f);
			int bufferIndex2 = Mathf.RoundToInt(num2 * 256f) + 260;
			bool flag = false;
			bool flag2 = false;
			int num3 = 18;
			int num4 = num3 - 1;
			int num5 = num3 - 2;
			int num6 = target.colors.Length;
			int num7 = target.getIndexFromXY(x - 2, y - 2) - target.width;
			for (int i = 0; i < num3; i++)
			{
				flag = !flag;
				int num8 = num7 + i;
				for (int j = 0; j < num3; j++)
				{
					flag2 = !flag2;
					num8 += target.width;
					if ((flag || j != 0) && (!flag || j != 1) && (!flag || j != num5) && (flag || j != num4) && (flag2 || i != 0) && (!flag2 || i != 1) && (!flag2 || i != num5) && (flag2 || i != num4))
					{
						if (num8 >= num6)
						{
							return;
						}
						if (num8 >= 0)
						{
							target.colors[num8].r = MapIllustrator.DarknessControl.scaleByte(target.colors[num8].r, num, bufferIndex);
							target.colors[num8].g = MapIllustrator.DarknessControl.scaleByte(target.colors[num8].g, num, bufferIndex);
							target.colors[num8].b = MapIllustrator.DarknessControl.scaleByte(target.colors[num8].b, num2, bufferIndex2);
						}
					}
				}
			}
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x0006F930 File Offset: 0x0006DB30
		private static byte scaleByte(byte target, float scale, int bufferIndex)
		{
			if (target == 0)
			{
				return 0;
			}
			if (MapIllustrator.DarknessControl.colorBuffer[(int)target, bufferIndex] != 0)
			{
				return MapIllustrator.DarknessControl.colorBuffer[(int)target, bufferIndex];
			}
			float num = (float)target;
			num *= scale;
			num = Mathf.Clamp(num, 0f, 255f);
			MapIllustrator.DarknessControl.colorBuffer[(int)target, bufferIndex] = (byte)num;
			return MapIllustrator.DarknessControl.colorBuffer[(int)target, bufferIndex];
		}

		// Token: 0x0400090C RID: 2316
		private static float baseDarknessScalar = 0.1f;

		// Token: 0x0400090D RID: 2317
		private static float baseDarknessBlueScalar = 0.7f;

		// Token: 0x0400090E RID: 2318
		private static byte[,] colorBuffer = new byte[256, 520];
	}

	// Token: 0x02000251 RID: 593
	private class ScrollControl
	{
		// Token: 0x06001966 RID: 6502 RVA: 0x0006F9B9 File Offset: 0x0006DBB9
		public ScrollControl()
		{
			this.scrollX = 16;
			this.scrollY = 16;
			this.allowScroll = true;
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x0006F9D8 File Offset: 0x0006DBD8
		public void setScroll(int x, int y, MapTileGrid tileGrid)
		{
			if (!this.allowScroll)
			{
				return;
			}
			if (x < tileGrid.getViewportX())
			{
				this.scrollX = 32;
			}
			if (x > tileGrid.getViewportX())
			{
				this.scrollX = 0;
			}
			if (y < tileGrid.getViewportY())
			{
				this.scrollY = 32;
			}
			if (y > tileGrid.getViewportY())
			{
				this.scrollY = 0;
			}
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x0006FA30 File Offset: 0x0006DC30
		public bool isScrollReady()
		{
			return !this.allowScroll || (this.scrollX == 16 && this.scrollY == 16);
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x0006FA53 File Offset: 0x0006DC53
		public void stopScroll()
		{
			this.scrollX = 16;
			this.scrollY = 16;
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x0006FA68 File Offset: 0x0006DC68
		public void scroll(WeatherEffectsControl weatherEffectControl, bool falling = false)
		{
			int num = MapIllustrator.SCROLL_SPEED;
			if (falling)
			{
				num *= 2;
			}
			if (this.scrollX > 16)
			{
				this.scrollX -= num;
				weatherEffectControl.setXScroll(num);
			}
			else if (this.scrollX < 16)
			{
				this.scrollX += num;
				weatherEffectControl.setXScroll(-num);
			}
			if (this.scrollY > 16)
			{
				this.scrollY -= num;
				weatherEffectControl.setYScroll(num);
				return;
			}
			if (this.scrollY < 16)
			{
				this.scrollY += num;
				weatherEffectControl.setYScroll(-num);
			}
		}

		// Token: 0x0400090F RID: 2319
		public int scrollX;

		// Token: 0x04000910 RID: 2320
		public int scrollY;

		// Token: 0x04000911 RID: 2321
		public bool allowScroll;
	}

	// Token: 0x02000252 RID: 594
	public static class ScreenDimensions
	{
		// Token: 0x0600196B RID: 6507 RVA: 0x0006FB03 File Offset: 0x0006DD03
		public static int getIllustratedMapWidth()
		{
			return MapIllustrator.ScreenDimensions.ILLUSTRATED_MAP_WIDTH;
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x0006FB0A File Offset: 0x0006DD0A
		public static int getIllustratedMapHeight()
		{
			return MapIllustrator.ScreenDimensions.ILLUSTRATED_MAP_HEIGHT;
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x0006FB11 File Offset: 0x0006DD11
		public static int getIllustratedMapHalfWidth()
		{
			return MapIllustrator.ScreenDimensions.ILLUSTRATED_HALF_WIDTH;
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x0006FB18 File Offset: 0x0006DD18
		public static int getIllustratedMapHalfHeight()
		{
			return MapIllustrator.ScreenDimensions.ILLUSTRATED_HALF_HEIGHT;
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x0006FB1F File Offset: 0x0006DD1F
		public static int getIllustratedMapTileRimWidth()
		{
			return 2;
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x0006FB22 File Offset: 0x0006DD22
		public static int getIllustratedMapTileRimHeight()
		{
			return 2;
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x0006FB25 File Offset: 0x0006DD25
		public static int getIllustratedMapWidthAndRim()
		{
			return MapIllustrator.ScreenDimensions.getIllustratedMapWidth() + MapIllustrator.ScreenDimensions.getIllustratedMapTileRimWidth();
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x0006FB32 File Offset: 0x0006DD32
		public static int getIllustratedMapHeightAndRim()
		{
			return MapIllustrator.ScreenDimensions.getIllustratedMapHeight() + MapIllustrator.ScreenDimensions.getIllustratedMapTileRimHeight();
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x0006FB3F File Offset: 0x0006DD3F
		public static int getLongestTileMapDimension()
		{
			if (MapIllustrator.ScreenDimensions.getIllustratedMapWidth() >= MapIllustrator.ScreenDimensions.getIllustratedMapHeight())
			{
				return MapIllustrator.ScreenDimensions.getIllustratedMapWidth();
			}
			return MapIllustrator.ScreenDimensions.getIllustratedMapHeight();
		}

		// Token: 0x04000912 RID: 2322
		private static int ILLUSTRATED_MAP_WIDTH = 23;

		// Token: 0x04000913 RID: 2323
		private static int ILLUSTRATED_HALF_WIDTH = MapIllustrator.ScreenDimensions.ILLUSTRATED_MAP_WIDTH / 2;

		// Token: 0x04000914 RID: 2324
		private static int ILLUSTRATED_MAP_HEIGHT = 17;

		// Token: 0x04000915 RID: 2325
		private static int ILLUSTRATED_HALF_HEIGHT = MapIllustrator.ScreenDimensions.ILLUSTRATED_MAP_HEIGHT / 2;
	}
}
