using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000EC RID: 236
[Serializable]
public class MapTile : SkaldBaseObject
{
	// Token: 0x06000E25 RID: 3621 RVA: 0x00042D4C File Offset: 0x00040F4C
	public MapTile(int _x, int _y, string mapId)
	{
		this.worldPosition.setTilePosition(_x, _y, mapId);
	}

	// Token: 0x06000E26 RID: 3622 RVA: 0x00042DB8 File Offset: 0x00040FB8
	public MapTile(int _x, int _y, Map map, List<MapSaveDataContainer.TerrainLayer.TerrainLoadData> terrainDataGround, List<MapSaveDataContainer.TerrainLayer.TerrainLoadData> terrainDataWall) : this(_x, _y, map.getId())
	{
		this.voidTile = true;
		int num = 0;
		int wallLayers = 0;
		if (terrainDataGround != null)
		{
			num = terrainDataGround.Count;
		}
		if (terrainDataWall != null)
		{
			wallLayers = terrainDataWall.Count;
		}
		this.textureBuffer.setTileAndSubImageList(num, wallLayers);
		this.applyTerrainLayer(terrainDataGround, map.getId(), 0);
		this.applyTerrainLayer(terrainDataWall, map.getId(), num);
	}

	// Token: 0x06000E27 RID: 3623 RVA: 0x00042E24 File Offset: 0x00041024
	private void applyTerrainLayer(List<MapSaveDataContainer.TerrainLayer.TerrainLoadData> list, string mapId, int layerOffset)
	{
		if (list == null || list.Count == 0)
		{
			return;
		}
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i] != null)
			{
				SKALDProjectData.TerrainContainers.TerrainTile terrainById = GameData.getTerrainById(list[i].id, mapId);
				this.applyTerrain(terrainById, i + layerOffset);
				this.setSubImage(i + layerOffset, (short)list[i].subImage);
				this.voidTile = false;
			}
		}
	}

	// Token: 0x06000E28 RID: 3624 RVA: 0x00042E91 File Offset: 0x00041091
	public virtual int getPixelX()
	{
		return this.worldPosition.getPixelX();
	}

	// Token: 0x06000E29 RID: 3625 RVA: 0x00042E9E File Offset: 0x0004109E
	public virtual int getPixelY()
	{
		return this.worldPosition.getPixelY();
	}

	// Token: 0x06000E2A RID: 3626 RVA: 0x00042EAB File Offset: 0x000410AB
	public virtual int getTileX()
	{
		return this.worldPosition.getTileX();
	}

	// Token: 0x06000E2B RID: 3627 RVA: 0x00042EB8 File Offset: 0x000410B8
	public virtual int getTileY()
	{
		return this.worldPosition.getTileY();
	}

	// Token: 0x06000E2C RID: 3628 RVA: 0x00042EC5 File Offset: 0x000410C5
	public void resetTextureBuffer()
	{
		this.textureBuffer.resetTextures();
	}

	// Token: 0x06000E2D RID: 3629 RVA: 0x00042ED4 File Offset: 0x000410D4
	public void setMapObject(List<string> idList)
	{
		foreach (string text in idList)
		{
			this.setMapObject(text);
		}
	}

	// Token: 0x06000E2E RID: 3630 RVA: 0x00042F24 File Offset: 0x00041124
	public bool hasMapObject()
	{
		return this.mapObject != null;
	}

	// Token: 0x06000E2F RID: 3631 RVA: 0x00042F30 File Offset: 0x00041130
	public void setMapObject(string id)
	{
		if (id == null || id == "")
		{
			return;
		}
		id = id.ToUpper();
		if (id == "FIREMEDIUM")
		{
			this.mapObject = new MapObjectFire(this);
			return;
		}
		MainControl.logError("Malformed MAP OBJECT ID: " + id);
	}

	// Token: 0x06000E30 RID: 3632 RVA: 0x00042F80 File Offset: 0x00041180
	public BaseTemporaryMapObjects getMapObject()
	{
		return this.mapObject;
	}

	// Token: 0x06000E31 RID: 3633 RVA: 0x00042F88 File Offset: 0x00041188
	public void updateMapObject(int xOffset, int yOffset, TextureTools.TextureData targetTexture)
	{
		if (this.mapObject == null)
		{
			return;
		}
		this.mapObject.updateParticleEffects(xOffset, yOffset, targetTexture);
	}

	// Token: 0x06000E32 RID: 3634 RVA: 0x00042FA1 File Offset: 0x000411A1
	public void clearMapObjectIfDead()
	{
		if (this.mapObject == null)
		{
			return;
		}
		if (this.mapObject.isDead())
		{
			this.clearMapObjects();
		}
	}

	// Token: 0x06000E33 RID: 3635 RVA: 0x00042FBF File Offset: 0x000411BF
	public void clearMapObjects()
	{
		this.mapObject = null;
	}

	// Token: 0x06000E34 RID: 3636 RVA: 0x00042FC8 File Offset: 0x000411C8
	private Prop getGuestProp()
	{
		if (this.guestPropContainer != null)
		{
			return this.guestPropContainer.getGuestProp();
		}
		return null;
	}

	// Token: 0x06000E35 RID: 3637 RVA: 0x00042FDF File Offset: 0x000411DF
	public Prop getPropOrGuestProp()
	{
		if (this.getProp() != null)
		{
			return this.getProp();
		}
		return this.getGuestProp();
	}

	// Token: 0x06000E36 RID: 3638 RVA: 0x00042FF6 File Offset: 0x000411F6
	public void setGuestProp(Prop guestProp)
	{
		if (guestProp == null || this.getProp() == guestProp)
		{
			return;
		}
		this.guestPropContainer = new MapTile.GuestPropContainer(guestProp);
	}

	// Token: 0x06000E37 RID: 3639 RVA: 0x00043011 File Offset: 0x00041211
	public string getContainerMapId()
	{
		return this.worldPosition.getMapId();
	}

	// Token: 0x06000E38 RID: 3640 RVA: 0x00043020 File Offset: 0x00041220
	public void clearInstancesAndDelete()
	{
		if (this.getParty() != null)
		{
			this.getParty().setToBeRemoved();
		}
		if (this.getProp() != null)
		{
			this.getProp().setToBeRemoved();
		}
		if (this.inventory != null)
		{
			this.inventory.setToBeRemoved();
		}
		if (this.hasVehicle())
		{
			this.vehicle.setToBeRemoved();
		}
		this.clearInstances();
	}

	// Token: 0x06000E39 RID: 3641 RVA: 0x0004307F File Offset: 0x0004127F
	public void clearInstances()
	{
		this.clearParty();
		this.deleteDeadParty();
		this.clearProp();
		this.clearVehicle();
		this.guestPropContainer = null;
		this.inventory = null;
	}

	// Token: 0x06000E3A RID: 3642 RVA: 0x000430A7 File Offset: 0x000412A7
	public bool isSpotted()
	{
		return this.spotted;
	}

	// Token: 0x06000E3B RID: 3643 RVA: 0x000430AF File Offset: 0x000412AF
	public bool isForceOutside()
	{
		return this.forceOutside;
	}

	// Token: 0x06000E3C RID: 3644 RVA: 0x000430B7 File Offset: 0x000412B7
	public bool isSpottedOnce()
	{
		return this.spottedOnce;
	}

	// Token: 0x06000E3D RID: 3645 RVA: 0x000430BF File Offset: 0x000412BF
	private void setSpottedOnce()
	{
		if (this.spottedOnce)
		{
			return;
		}
		this.processSpottedTrigger();
		this.spottedOnce = true;
	}

	// Token: 0x06000E3E RID: 3646 RVA: 0x000430D8 File Offset: 0x000412D8
	public bool isVisited()
	{
		return this.visited;
	}

	// Token: 0x06000E3F RID: 3647 RVA: 0x000430E0 File Offset: 0x000412E0
	public void setVisited()
	{
		if (this.visited)
		{
			this.processEnterTrigger();
			return;
		}
		this.processFirstTimeTrigger();
		this.processEnterTrigger();
		this.visited = true;
	}

	// Token: 0x06000E40 RID: 3648 RVA: 0x00043108 File Offset: 0x00041308
	public void setSpotted()
	{
		this.spotted = true;
		this.clearCount = 10;
		Party party = this.getParty();
		if (party != null && !this.isIlluminated() && !party.isPC() && !party.isSpotted() && (MainControl.getDataControl().getCurrentPC().testAwarenessStatic(party.getCurrentCharacter().getCurrentAttributeValueStatic(AttributesControl.CoreAttributes.ATT_Stealth) + this.getStealthBonus() + 3).wasSuccess() || (party.isUnique() && !party.isHostile())))
		{
			party.setSpotted();
		}
		this.setSpottedOnce();
	}

	// Token: 0x06000E41 RID: 3649 RVA: 0x0004318E File Offset: 0x0004138E
	public void setSaveData(bool spotted, bool spottedOnce, bool visited)
	{
		this.spotted = spotted;
		this.spottedOnce = spottedOnce;
		this.visited = visited;
	}

	// Token: 0x06000E42 RID: 3650 RVA: 0x000431A5 File Offset: 0x000413A5
	public void clearSpotted()
	{
		this.spotted = false;
	}

	// Token: 0x06000E43 RID: 3651 RVA: 0x000431AE File Offset: 0x000413AE
	public void clearSpottedWhenReady()
	{
		if (this.clearCount == 0)
		{
			this.clearSpotted();
			return;
		}
		this.clearCount--;
	}

	// Token: 0x06000E44 RID: 3652 RVA: 0x000431CD File Offset: 0x000413CD
	public bool isVoidTile()
	{
		return this.voidTile;
	}

	// Token: 0x06000E45 RID: 3653 RVA: 0x000431D8 File Offset: 0x000413D8
	public int getStealthBonus()
	{
		int num = 0;
		num -= Mathf.Clamp(Mathf.RoundToInt(this.lightLevelControl.getLightLevel() * 7f) - 1, 0, 7);
		if (this.concealment)
		{
			num += 3;
		}
		return num;
	}

	// Token: 0x06000E46 RID: 3654 RVA: 0x00043218 File Offset: 0x00041418
	public Character getCharacter()
	{
		if (this.party == null || this.party.isEmpty())
		{
			return null;
		}
		Character currentCharacter = this.party.getCurrentCharacter();
		if (!currentCharacter.checkIfOnPosition(this.worldPosition.getMapId(), this.getTileX(), this.getTileY()))
		{
			MainControl.logError(string.Concat(new string[]
			{
				"Removing marooned Character ",
				currentCharacter.getId(),
				" from tile ",
				this.getTileX().ToString(),
				"/",
				this.getTileY().ToString()
			}));
			this.party.removeCurrentObject();
			if (this.party.isEmpty())
			{
				this.clearParty();
			}
			return null;
		}
		return this.party.getCurrentCharacter();
	}

	// Token: 0x06000E47 RID: 3655 RVA: 0x000432E6 File Offset: 0x000414E6
	public bool isTileOpen()
	{
		return this.getLiveCharacter() == null;
	}

	// Token: 0x06000E48 RID: 3656 RVA: 0x000432F3 File Offset: 0x000414F3
	public bool isTaggedAsCombatAccessible()
	{
		return this.combatAccessible;
	}

	// Token: 0x06000E49 RID: 3657 RVA: 0x000432FB File Offset: 0x000414FB
	public bool canDrawTacticalGrid()
	{
		return !this.isWater() && this.isPassable() && this.isTaggedAsCombatAccessible();
	}

	// Token: 0x06000E4A RID: 3658 RVA: 0x00043315 File Offset: 0x00041515
	public void tagAsCombatAccessible()
	{
		this.combatAccessible = true;
	}

	// Token: 0x06000E4B RID: 3659 RVA: 0x0004331E File Offset: 0x0004151E
	public void clearCombatAccessible()
	{
		this.combatAccessible = false;
	}

	// Token: 0x06000E4C RID: 3660 RVA: 0x00043328 File Offset: 0x00041528
	public Character getLiveCharacter()
	{
		Character character = this.getCharacter();
		if (character != null && !character.isDead())
		{
			return character;
		}
		return null;
	}

	// Token: 0x06000E4D RID: 3661 RVA: 0x0004334A File Offset: 0x0004154A
	public void clearParty()
	{
		this.party = null;
	}

	// Token: 0x06000E4E RID: 3662 RVA: 0x00043353 File Offset: 0x00041553
	public void deleteParty()
	{
		if (this.party != null)
		{
			this.party.clearTilePosition();
			this.clearParty();
		}
	}

	// Token: 0x06000E4F RID: 3663 RVA: 0x0004336E File Offset: 0x0004156E
	public void clearProp()
	{
		this.prop = null;
	}

	// Token: 0x06000E50 RID: 3664 RVA: 0x00043377 File Offset: 0x00041577
	public void deleteDeadParty()
	{
		if (this.deadParty != null)
		{
			this.deadParty.clearTilePosition();
			this.deadParty.setToBeRemoved();
			this.deadParty = null;
		}
	}

	// Token: 0x06000E51 RID: 3665 RVA: 0x0004339E File Offset: 0x0004159E
	public void updateDeadPartyStatus()
	{
		if (this.party != null && this.party.isPartyDead())
		{
			this.deadParty = this.party;
			this.clearParty();
		}
	}

	// Token: 0x06000E52 RID: 3666 RVA: 0x000433C7 File Offset: 0x000415C7
	public Party getDeadParty()
	{
		return this.deadParty;
	}

	// Token: 0x06000E53 RID: 3667 RVA: 0x000433D0 File Offset: 0x000415D0
	public void applyTerrain(SKALDProjectData.TerrainContainers.TerrainTile t, int i)
	{
		if (t == null)
		{
			return;
		}
		if (t.id != "" && t.infoTile)
		{
			this.setId(t.id);
		}
		if (t.title != "" && t.infoTile)
		{
			this.setName(base.processString(t.title, null));
		}
		if (t.description != "" && t.infoTile)
		{
			this.setDescription(base.processString(t.description, null));
		}
		if (t.overlay)
		{
			this.overlay = t.overlay;
		}
		if (t.encounterChance != 0 && t.infoTile)
		{
			this.encounterChance = (short)t.encounterChance;
		}
		if (t.encounterMaps != null && t.encounterMaps.Count != 0 && t.infoTile)
		{
			this.randomEncounterMapIdList = t.encounterMaps;
		}
		if (t.imagePath != "")
		{
			this.setImagePath(t.imagePath);
		}
		if (t.animation != "")
		{
			this.animationPath = base.processString(t.animation, null);
		}
		if (t.forceOutside)
		{
			this.forceOutside = t.forceOutside;
		}
		if (t.animateAsWater)
		{
			this.animateAsWater = true;
		}
		if (t.drawBehind)
		{
			this.drawBehind = true;
		}
		if (!t.technical && !t.infoTile)
		{
			this.textureBuffer.addTileImage(i, t.modelPath, t.sheetWidth, t.sheetPadding);
		}
		if (!this.noVisibility)
		{
			this.noVisibility = t.visibility;
		}
		if (!this.impassable)
		{
			this.impassable = t.impassable;
		}
		if (t.npcBlocked)
		{
			this.NPCBlocked = t.npcBlocked;
		}
		if (!this.water)
		{
			this.water = t.water;
		}
		if (t.concealed)
		{
			this.concealment = t.concealed;
		}
		if (t.light != 0)
		{
			this.light = (short)t.light;
		}
	}

	// Token: 0x06000E54 RID: 3668 RVA: 0x000435DA File Offset: 0x000417DA
	public bool testRandomEncounter()
	{
		return Random.Range(0, 100) < (int)this.encounterChance;
	}

	// Token: 0x06000E55 RID: 3669 RVA: 0x000435EC File Offset: 0x000417EC
	public Map getEncounterMap()
	{
		if (this.randomEncounterMapIdList.Count == 0)
		{
			return null;
		}
		return GameData.getMap(this.randomEncounterMapIdList[Random.Range(0, this.randomEncounterMapIdList.Count)], this.getId());
	}

	// Token: 0x06000E56 RID: 3670 RVA: 0x00043624 File Offset: 0x00041824
	public MapTile getPropRootTile()
	{
		if (this.getGuestProp() != null)
		{
			return this.guestPropContainer.getRootTile();
		}
		if (this.getProp() != null)
		{
			return this;
		}
		return null;
	}

	// Token: 0x06000E57 RID: 3671 RVA: 0x00043648 File Offset: 0x00041848
	public void deleteProp()
	{
		if (this.getGuestProp() != null)
		{
			this.guestPropContainer.getRootTile().deleteProp();
			this.guestPropContainer = null;
		}
		if (this.getProp() == null)
		{
			return;
		}
		this.getProp().clearTilePosition();
		this.getProp().setToBeRemoved();
		this.clearProp();
	}

	// Token: 0x06000E58 RID: 3672 RVA: 0x00043699 File Offset: 0x00041899
	public bool isWater()
	{
		return this.water;
	}

	// Token: 0x06000E59 RID: 3673 RVA: 0x000436A1 File Offset: 0x000418A1
	public bool shouldDrawBehind()
	{
		return this.drawBehind;
	}

	// Token: 0x06000E5A RID: 3674 RVA: 0x000436A9 File Offset: 0x000418A9
	public float getTravelTime()
	{
		return this.travelTime;
	}

	// Token: 0x06000E5B RID: 3675 RVA: 0x000436B1 File Offset: 0x000418B1
	public string getNestedMapId()
	{
		if (this.getPropOrGuestProp() != null)
		{
			return this.getPropOrGuestProp().getNestedMapId();
		}
		return "";
	}

	// Token: 0x06000E5C RID: 3676 RVA: 0x000436CC File Offset: 0x000418CC
	public bool isConcealment()
	{
		return this.concealment;
	}

	// Token: 0x06000E5D RID: 3677 RVA: 0x000436D4 File Offset: 0x000418D4
	public Party getParty()
	{
		this.updateDeadPartyStatus();
		this.getCharacter();
		return this.party;
	}

	// Token: 0x06000E5E RID: 3678 RVA: 0x000436E9 File Offset: 0x000418E9
	public void setParty(Party p)
	{
		this.party = p;
		if (this.party != null)
		{
			this.party.setTilePosition(this.getTileX(), this.getTileY(), this.getContainerMapId());
		}
	}

	// Token: 0x06000E5F RID: 3679 RVA: 0x00043717 File Offset: 0x00041917
	public string getTileImageByIndex(int index)
	{
		return this.textureBuffer.getTileImageByIndex(index);
	}

	// Token: 0x06000E60 RID: 3680 RVA: 0x00043725 File Offset: 0x00041925
	public int getTileImageListLength()
	{
		return this.textureBuffer.getTileImageListLength();
	}

	// Token: 0x06000E61 RID: 3681 RVA: 0x00043734 File Offset: 0x00041934
	public void setProp(Prop p)
	{
		if (p == null)
		{
			return;
		}
		if (this.getProp() != null && this.getProp() != p)
		{
			this.deleteProp();
		}
		this.prop = p;
		this.prop.applyLoadoutToTile(this);
		this.getProp().setTilePosition(this.getTileX(), this.getTileY(), this.getContainerMapId());
	}

	// Token: 0x06000E62 RID: 3682 RVA: 0x0004378C File Offset: 0x0004198C
	public string getAnimationPath()
	{
		return "Images/Animations/" + this.animationPath;
	}

	// Token: 0x06000E63 RID: 3683 RVA: 0x0004379E File Offset: 0x0004199E
	public bool shouldTileBeAnimated()
	{
		return this.animationPath != "";
	}

	// Token: 0x06000E64 RID: 3684 RVA: 0x000437B0 File Offset: 0x000419B0
	public void setFullModelBufferImage(TextureTools.TextureData image, int index)
	{
		this.textureBuffer.setFullModelImage(image, index);
		this.voidTile = false;
	}

	// Token: 0x06000E65 RID: 3685 RVA: 0x000437C6 File Offset: 0x000419C6
	public TextureTools.TextureData getBakedImageStack()
	{
		return this.textureBuffer.getBakedImageStack(this.isOverlay());
	}

	// Token: 0x06000E66 RID: 3686 RVA: 0x000437D9 File Offset: 0x000419D9
	public Prop getProp()
	{
		return this.prop;
	}

	// Token: 0x06000E67 RID: 3687 RVA: 0x000437E4 File Offset: 0x000419E4
	public Inventory getInventory()
	{
		if (this.getGuestProp() != null && this.getGuestProp().isContainer())
		{
			return this.guestPropContainer.getRootTile().getInventory();
		}
		if (this.inventory == null)
		{
			this.inventory = new Inventory();
			this.inventory.setTilePosition(this.getTileX(), this.getTileY(), this.getContainerMapId());
		}
		return this.inventory;
	}

	// Token: 0x06000E68 RID: 3688 RVA: 0x0004384D File Offset: 0x00041A4D
	public void setVehicle(Vehicle c)
	{
		this.vehicle = c;
		if (this.vehicle != null)
		{
			this.vehicle.setTilePosition(this.getTileX(), this.getTileY(), this.getContainerMapId());
		}
	}

	// Token: 0x06000E69 RID: 3689 RVA: 0x0004387B File Offset: 0x00041A7B
	public Vehicle getVehicle()
	{
		return this.vehicle;
	}

	// Token: 0x06000E6A RID: 3690 RVA: 0x00043884 File Offset: 0x00041A84
	public Vehicle transferVehicle()
	{
		Vehicle vehicle = this.vehicle;
		this.clearVehicle();
		if (vehicle != null)
		{
			vehicle.clearTilePosition();
		}
		return vehicle;
	}

	// Token: 0x06000E6B RID: 3691 RVA: 0x000438A8 File Offset: 0x00041AA8
	public bool hasVehicle()
	{
		return this.vehicle != null;
	}

	// Token: 0x06000E6C RID: 3692 RVA: 0x000438B3 File Offset: 0x00041AB3
	private void clearVehicle()
	{
		this.vehicle = null;
	}

	// Token: 0x06000E6D RID: 3693 RVA: 0x000438BC File Offset: 0x00041ABC
	public void addCharacter(Character c)
	{
		if (this.party == null)
		{
			this.setParty(new Party());
		}
		else if (!this.party.isEmpty() && !this.party.isPC())
		{
			MainControl.logError(string.Concat(new string[]
			{
				"Multiple NPCs occupy the same tile: ",
				this.party.printList(),
				"\nTile position: ",
				this.getTileX().ToString(),
				" / ",
				this.getTileY().ToString()
			}));
		}
		this.party.addAndSetAsTileParty(c);
	}

	// Token: 0x06000E6E RID: 3694 RVA: 0x00043960 File Offset: 0x00041B60
	public int getLightRadius()
	{
		int num = 0;
		if (this.getPropOrGuestProp() != null)
		{
			num = this.getPropOrGuestProp().getLight();
		}
		if ((int)this.light > num)
		{
			num = (int)this.light;
		}
		if (this.getParty() != null && this.getParty().getLightDistance() > num)
		{
			num = this.getParty().getLightDistance();
		}
		return num;
	}

	// Token: 0x06000E6F RID: 3695 RVA: 0x000439B8 File Offset: 0x00041BB8
	public float getLightEmitterStrength()
	{
		float num = 0f;
		if (this.getPropOrGuestProp() != null)
		{
			num = this.getPropOrGuestProp().getLightStrength();
		}
		if (this.getParty() != null && this.getParty().getLightStrength() > num)
		{
			num = this.getParty().getLightStrength();
		}
		return num;
	}

	// Token: 0x06000E70 RID: 3696 RVA: 0x00043A02 File Offset: 0x00041C02
	public override string getDescription()
	{
		if (this.getPropOrGuestProp() != null && this.getPropOrGuestProp().getDescription() != "")
		{
			return this.getPropOrGuestProp().getDescription();
		}
		return base.getDescription();
	}

	// Token: 0x06000E71 RID: 3697 RVA: 0x00043A35 File Offset: 0x00041C35
	public bool isIlluminated()
	{
		return this.lightLevelControl.isIlluminated();
	}

	// Token: 0x06000E72 RID: 3698 RVA: 0x00043A42 File Offset: 0x00041C42
	public float getLightLevel()
	{
		return this.lightLevelControl.getLightLevel();
	}

	// Token: 0x06000E73 RID: 3699 RVA: 0x00043A4F File Offset: 0x00041C4F
	public float getTargetLightLevel()
	{
		return this.lightLevelControl.getTargetLightLevel();
	}

	// Token: 0x06000E74 RID: 3700 RVA: 0x00043A5C File Offset: 0x00041C5C
	public void updateLightLevel()
	{
		if (this.isVoidTile())
		{
			return;
		}
		this.lightLevelControl.updateLightLevel();
	}

	// Token: 0x06000E75 RID: 3701 RVA: 0x00043A72 File Offset: 0x00041C72
	public bool isTilePassableToNPC(Party p)
	{
		return this.isPassable() && !this.NPCBlocked && (this.party == null || this.party == p) && !this.isWater();
	}

	// Token: 0x06000E76 RID: 3702 RVA: 0x00043AA0 File Offset: 0x00041CA0
	public bool shouldAnimateAsWater()
	{
		return this.animateAsWater;
	}

	// Token: 0x06000E77 RID: 3703 RVA: 0x00043AA8 File Offset: 0x00041CA8
	public bool wading()
	{
		return this.isConcealment() && this.shouldAnimateAsWater();
	}

	// Token: 0x06000E78 RID: 3704 RVA: 0x00043ABA File Offset: 0x00041CBA
	public int getHeightOffset()
	{
		if (this.wading())
		{
			return -8;
		}
		if (this.isConcealment())
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x06000E79 RID: 3705 RVA: 0x00043AD2 File Offset: 0x00041CD2
	public int getConcealmentOffset()
	{
		if (this.wading())
		{
			return Mathf.Abs(this.getHeightOffset());
		}
		if (this.isConcealment())
		{
			return 8;
		}
		return 0;
	}

	// Token: 0x06000E7A RID: 3706 RVA: 0x00043AF3 File Offset: 0x00041CF3
	public TextureTools.TextureData getMainTileImage()
	{
		return this.textureBuffer.getMainTileImage(this.isOverlay());
	}

	// Token: 0x06000E7B RID: 3707 RVA: 0x00043B06 File Offset: 0x00041D06
	public TextureTools.TextureData getOverlayTileImage()
	{
		return this.textureBuffer.getOverlayImage(this.isOverlay());
	}

	// Token: 0x06000E7C RID: 3708 RVA: 0x00043B19 File Offset: 0x00041D19
	public string getVerb()
	{
		if (this.getPropOrGuestProp() != null)
		{
			return this.getPropOrGuestProp().getVerb();
		}
		return "";
	}

	// Token: 0x06000E7D RID: 3709 RVA: 0x00043B34 File Offset: 0x00041D34
	private string processEnterTrigger()
	{
		if (this.getPropOrGuestProp() != null)
		{
			return this.getPropOrGuestProp().processEnterTrigger();
		}
		return "";
	}

	// Token: 0x06000E7E RID: 3710 RVA: 0x00043B4F File Offset: 0x00041D4F
	private string processFirstTimeTrigger()
	{
		if (this.getPropOrGuestProp() != null)
		{
			return this.getPropOrGuestProp().processFirstTimeTrigger();
		}
		return "";
	}

	// Token: 0x06000E7F RID: 3711 RVA: 0x00043B6A File Offset: 0x00041D6A
	private string processSpottedTrigger()
	{
		if (this.getPropOrGuestProp() != null)
		{
			return this.getPropOrGuestProp().processSpottedTrigger();
		}
		return "";
	}

	// Token: 0x06000E80 RID: 3712 RVA: 0x00043B85 File Offset: 0x00041D85
	public void clearLightEffectsLevel()
	{
		this.lightLevelControl.clearLightEffects();
	}

	// Token: 0x06000E81 RID: 3713 RVA: 0x00043B92 File Offset: 0x00041D92
	public void setLightEffectsLevel(float value)
	{
		this.lightLevelControl.setLightEffectsLevel(value);
	}

	// Token: 0x06000E82 RID: 3714 RVA: 0x00043BA0 File Offset: 0x00041DA0
	public void setLightLevel(float value)
	{
		this.lightLevelControl.setLightLevel(value);
		if (!this.isIlluminated())
		{
			return;
		}
		if (this.getParty() != null && this.isSpotted())
		{
			this.getParty().setSpotted();
		}
		Prop propOrGuestProp = this.getPropOrGuestProp();
		if (this.isSpotted() && propOrGuestProp != null && propOrGuestProp.isHidden() && MainControl.getDataControl().getCurrentPC().testAwarenessStatic(propOrGuestProp.getSpotDC()).wasSuccess())
		{
			propOrGuestProp.clearHidden();
		}
	}

	// Token: 0x06000E83 RID: 3715 RVA: 0x00043C19 File Offset: 0x00041E19
	public void clearIlluminated()
	{
		this.lightLevelControl.clearIlluminated();
	}

	// Token: 0x06000E84 RID: 3716 RVA: 0x00043C26 File Offset: 0x00041E26
	public string processTryEnterTrigger()
	{
		if (this.getPropOrGuestProp() != null)
		{
			return this.getPropOrGuestProp().processTryEnterTrigger();
		}
		return "";
	}

	// Token: 0x06000E85 RID: 3717 RVA: 0x00043C41 File Offset: 0x00041E41
	public string processCombatLaunchTrigger()
	{
		if (this.getPropOrGuestProp() != null)
		{
			return this.getPropOrGuestProp().processCombatLaunchTrigger();
		}
		return "";
	}

	// Token: 0x06000E86 RID: 3718 RVA: 0x00043C5C File Offset: 0x00041E5C
	public void dig()
	{
		if (this.getPropOrGuestProp() != null && this.getPropOrGuestProp().canDig())
		{
			this.getPropOrGuestProp().fireDigTrigger();
			PopUpControl.addPopUpOK("You use the shovel to dig in the ground a bit and you find somehing!");
			return;
		}
		PopUpControl.addPopUpOK("You use the shovel to dig in the ground a bit but you don't find anything!");
	}

	// Token: 0x06000E87 RID: 3719 RVA: 0x00043C93 File Offset: 0x00041E93
	public string processLeaveTrigger()
	{
		if (this.getPropOrGuestProp() != null)
		{
			return this.getPropOrGuestProp().processLeaveTrigger();
		}
		return "";
	}

	// Token: 0x06000E88 RID: 3720 RVA: 0x00043CAE File Offset: 0x00041EAE
	public string processVerbTrigger()
	{
		if (this.getPropOrGuestProp() != null && !this.getPropOrGuestProp().shouldBeRemovedFromGame())
		{
			return this.getPropOrGuestProp().interactWithProp();
		}
		return "";
	}

	// Token: 0x06000E89 RID: 3721 RVA: 0x00043CD6 File Offset: 0x00041ED6
	public void setSubImage(int index, short subImage)
	{
		this.textureBuffer.setSubImageIndex(index, subImage);
	}

	// Token: 0x06000E8A RID: 3722 RVA: 0x00043CE5 File Offset: 0x00041EE5
	public bool isContainer()
	{
		return this.getPropOrGuestProp() != null && this.getPropOrGuestProp().isContainer();
	}

	// Token: 0x06000E8B RID: 3723 RVA: 0x00043CFC File Offset: 0x00041EFC
	public bool isInteractive()
	{
		return this.getPropOrGuestProp() != null && this.getPropOrGuestProp().isInteractive();
	}

	// Token: 0x06000E8C RID: 3724 RVA: 0x00043D16 File Offset: 0x00041F16
	public bool isPassable()
	{
		return !this.voidTile && !this.impassable && (this.getPropOrGuestProp() == null || !this.getPropOrGuestProp().impassable());
	}

	// Token: 0x06000E8D RID: 3725 RVA: 0x00043D46 File Offset: 0x00041F46
	public bool isOverlay()
	{
		return this.overlay;
	}

	// Token: 0x06000E8E RID: 3726 RVA: 0x00043D4E File Offset: 0x00041F4E
	public bool isTileOpenAndPassable()
	{
		return this.isPassable() && this.isTileOpen();
	}

	// Token: 0x06000E8F RID: 3727 RVA: 0x00043D60 File Offset: 0x00041F60
	public string getShortInspectDescription()
	{
		if (this.getPropOrGuestProp() != null)
		{
			string text = this.getPropOrGuestProp().getName();
			if (text != "")
			{
				text = "You see: " + text;
			}
			return text;
		}
		return "";
	}

	// Token: 0x06000E90 RID: 3728 RVA: 0x00043DA4 File Offset: 0x00041FA4
	public string getInspectDescription()
	{
		if (!this.isSpotted())
		{
			return "";
		}
		if (this.party != null)
		{
			return this.party.getInspectDescription();
		}
		string text = "";
		if (this.hasVehicle())
		{
			text = this.vehicle.getName();
		}
		else if (this.getPropOrGuestProp() != null && this.getPropOrGuestProp().getInspectDescription() != "")
		{
			text = this.getPropOrGuestProp().getInspectDescription();
		}
		else if (this.getName() != "")
		{
			text = this.getName();
		}
		string text2 = "";
		if (text != "")
		{
			text2 = "You see: " + text + ".";
		}
		if (!GlobalSettings.getGamePlaySettings().showStealthInfo())
		{
			return text2;
		}
		if (text2 != "")
		{
			text2 += "\n\n";
		}
		string text3 = this.getLightLevel().ToString();
		if (text3.Length > 4)
		{
			text3 = text3.Substring(0, 4);
		}
		text2 = text2 + TextTools.formateNameValuePair("Position", this.getTileX().ToString() + "/" + this.getTileY().ToString()) + "\n";
		text2 = text2 + TextTools.formateNameValuePair("Light Lvl.", text3) + "\n";
		return text2 + TextTools.formateNameValuePairPlusMinus("Stealth", this.getStealthBonus());
	}

	// Token: 0x06000E91 RID: 3729 RVA: 0x00043F0E File Offset: 0x0004210E
	public bool getSeeThrough()
	{
		return (this.getPropOrGuestProp() == null || !this.getPropOrGuestProp().noVisibility()) && !this.noVisibility;
	}

	// Token: 0x06000E92 RID: 3730 RVA: 0x00043F34 File Offset: 0x00042134
	internal void playMoveSound()
	{
		if (this.isConcealment() && this.shouldAnimateAsWater())
		{
			AudioControl.playMoveSoundWater();
			return;
		}
		if (this.isConcealment())
		{
			AudioControl.playMoveSoundVegetation();
			return;
		}
		if (this.getCharacter() != null && this.getCharacter().isHidden())
		{
			AudioControl.playMoveSoundStealth();
			return;
		}
		AudioControl.playMoveSound();
	}

	// Token: 0x0400039C RID: 924
	private bool spotted;

	// Token: 0x0400039D RID: 925
	private bool visited;

	// Token: 0x0400039E RID: 926
	private bool spottedOnce;

	// Token: 0x0400039F RID: 927
	private bool forceOutside;

	// Token: 0x040003A0 RID: 928
	private bool animateAsWater;

	// Token: 0x040003A1 RID: 929
	private bool drawBehind;

	// Token: 0x040003A2 RID: 930
	private bool combatAccessible;

	// Token: 0x040003A3 RID: 931
	public short fogSubImage = 6;

	// Token: 0x040003A4 RID: 932
	private int clearCount;

	// Token: 0x040003A5 RID: 933
	private short encounterChance;

	// Token: 0x040003A6 RID: 934
	private List<string> randomEncounterMapIdList = new List<string>();

	// Token: 0x040003A7 RID: 935
	private bool impassable;

	// Token: 0x040003A8 RID: 936
	private bool concealment;

	// Token: 0x040003A9 RID: 937
	private bool overlay;

	// Token: 0x040003AA RID: 938
	private bool NPCBlocked;

	// Token: 0x040003AB RID: 939
	private string animationPath = "";

	// Token: 0x040003AC RID: 940
	private bool noVisibility;

	// Token: 0x040003AD RID: 941
	private bool water;

	// Token: 0x040003AE RID: 942
	private float travelTime = 1f;

	// Token: 0x040003AF RID: 943
	private Inventory inventory;

	// Token: 0x040003B0 RID: 944
	public Vehicle vehicle;

	// Token: 0x040003B1 RID: 945
	private SkaldWorldObject.WorldPosition worldPosition = new SkaldWorldObject.WorldPosition();

	// Token: 0x040003B2 RID: 946
	private Party party;

	// Token: 0x040003B3 RID: 947
	private Party deadParty;

	// Token: 0x040003B4 RID: 948
	private Prop prop;

	// Token: 0x040003B5 RID: 949
	private short light;

	// Token: 0x040003B6 RID: 950
	private bool voidTile;

	// Token: 0x040003B7 RID: 951
	private MapTile.TextureBuffer textureBuffer = new MapTile.TextureBuffer();

	// Token: 0x040003B8 RID: 952
	private MapTile.GuestPropContainer guestPropContainer;

	// Token: 0x040003B9 RID: 953
	private LightLevelControl lightLevelControl = new LightLevelControl();

	// Token: 0x040003BA RID: 954
	private BaseTemporaryMapObjects mapObject;

	// Token: 0x02000253 RID: 595
	private class GuestPropContainer
	{
		// Token: 0x06001975 RID: 6517 RVA: 0x0006FB80 File Offset: 0x0006DD80
		public GuestPropContainer(Prop prop)
		{
			this.prop = prop;
			this.rootX = prop.getTileX();
			this.rootY = prop.getTileY();
			this.rootMap = prop.getContainerMapId();
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x0006FBB3 File Offset: 0x0006DDB3
		private bool isPropPositionValid()
		{
			return this.rootX == this.prop.getTileX() && this.rootY == this.prop.getTileY() && this.rootMap == this.prop.getContainerMapId();
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x0006FBF3 File Offset: 0x0006DDF3
		public Prop getGuestProp()
		{
			if (this.isPropPositionValid())
			{
				return this.prop;
			}
			return null;
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x0006FC05 File Offset: 0x0006DE05
		public MapTile getRootTile()
		{
			if (this.prop == null)
			{
				return null;
			}
			return this.prop.getMapTile();
		}

		// Token: 0x04000916 RID: 2326
		private Prop prop;

		// Token: 0x04000917 RID: 2327
		private int rootX;

		// Token: 0x04000918 RID: 2328
		private int rootY;

		// Token: 0x04000919 RID: 2329
		private string rootMap;
	}

	// Token: 0x02000254 RID: 596
	private class TextureBuffer
	{
		// Token: 0x06001979 RID: 6521 RVA: 0x0006FC1C File Offset: 0x0006DE1C
		public void setTileAndSubImageList(int groundLayers, int wallLayers)
		{
			this.imageLayers = new MapTile.TextureBuffer.ImageLayer[groundLayers + wallLayers];
			this.numberOfGroundLayers = groundLayers;
			if (this.numberOfGroundLayers == 0)
			{
				this.numberOfGroundLayers = 1;
			}
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x0006FC42 File Offset: 0x0006DE42
		public void resetTextures()
		{
			this.mainTileImage = null;
			this.overlayTileImage = null;
			this.fullModelImage = null;
			this.bakedImage = null;
			this.fullModelLayerIndex = 0;
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x0006FC67 File Offset: 0x0006DE67
		public string getTileImageByIndex(int index)
		{
			if (index < 0 || index >= this.getTileImageListLength())
			{
				return "";
			}
			return this.imageLayers[index].path;
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x0006FC8D File Offset: 0x0006DE8D
		public int getTileImageListLength()
		{
			if (this.imageLayers == null)
			{
				return 0;
			}
			return this.imageLayers.Length;
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x0006FCA4 File Offset: 0x0006DEA4
		public TextureTools.TextureData getBakedImageStack(bool isOverlay)
		{
			if (!isOverlay)
			{
				return this.getMainTileImage(isOverlay);
			}
			if (this.bakedImage == null)
			{
				this.bakedImage = new TextureTools.TextureData(16, 16);
				TextureTools.applyOverlay(this.bakedImage, this.getMainTileImage(isOverlay));
				TextureTools.applyOverlay(this.bakedImage, this.getOverlayImage(isOverlay));
			}
			this.bakedImage.compress();
			return this.bakedImage;
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x0006FD08 File Offset: 0x0006DF08
		public TextureTools.TextureData getMainTileImage(bool isOverlay)
		{
			if (this.mainTileImage != null)
			{
				return this.mainTileImage;
			}
			this.mainTileImage = new TextureTools.TextureData(16, 16);
			int tileImageListLength = this.getTileImageListLength();
			if (isOverlay)
			{
				tileImageListLength = this.numberOfGroundLayers;
			}
			for (int i = 0; i < tileImageListLength; i++)
			{
				if (this.imageLayers[i].path != null && this.imageLayers[i].path != "")
				{
					this.drawLayer(this.imageLayers[i], this.mainTileImage);
				}
				if (i == this.fullModelLayerIndex && this.fullModelImage != null)
				{
					TextureTools.applyOverlay(this.mainTileImage, this.fullModelImage);
				}
			}
			this.mainTileImage.compress();
			return this.mainTileImage;
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x0006FDCC File Offset: 0x0006DFCC
		public TextureTools.TextureData getOverlayImage(bool isOverlay)
		{
			if (!isOverlay)
			{
				return null;
			}
			if (this.overlayTileImage != null)
			{
				return this.overlayTileImage;
			}
			this.overlayTileImage = new TextureTools.TextureData(16, 16);
			int i = 0;
			if (this.getTileImageListLength() > 1)
			{
				i = this.numberOfGroundLayers;
			}
			while (i < this.getTileImageListLength())
			{
				if (this.imageLayers[i].path != null && this.imageLayers[i].path != "")
				{
					this.drawLayer(this.imageLayers[i], this.overlayTileImage);
				}
				if (i == this.fullModelLayerIndex && this.fullModelImage != null)
				{
					TextureTools.applyOverlay(this.overlayTileImage, this.fullModelImage);
				}
				i++;
			}
			this.overlayTileImage.compress();
			return this.overlayTileImage;
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x0006FE98 File Offset: 0x0006E098
		private void drawLayer(MapTile.TextureBuffer.ImageLayer imageLayer, TextureTools.TextureData target)
		{
			TextureTools.loadTextureDataAndApplyOverlay("Images/Tiles/" + imageLayer.path, (int)imageLayer.subImage, 0, 0, target, (int)imageLayer.sheetWidth, (int)imageLayer.sheetPadding);
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x0006FEC4 File Offset: 0x0006E0C4
		public void addTileImage(int index, string path, int sheetWidth, int sheetPadding)
		{
			if (path == null)
			{
				path = "";
			}
			if (path != "")
			{
				this.castsShadow = true;
			}
			this.imageLayers[index] = new MapTile.TextureBuffer.ImageLayer(path, 0, (short)sheetWidth, (short)sheetPadding);
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x0006FEFC File Offset: 0x0006E0FC
		public bool castShadow()
		{
			return this.castsShadow;
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x0006FF04 File Offset: 0x0006E104
		public void setFullModelImage(TextureTools.TextureData image, int index)
		{
			this.fullModelImage = image;
			this.fullModelLayerIndex = index;
			this.castsShadow = true;
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x0006FF1C File Offset: 0x0006E11C
		public void setSubImageIndex(int index, short subImage)
		{
			try
			{
				this.imageLayers[index].subImage = subImage;
			}
			catch (Exception obj)
			{
				MainControl.logError(obj);
			}
		}

		// Token: 0x0400091A RID: 2330
		private TextureTools.TextureData mainTileImage;

		// Token: 0x0400091B RID: 2331
		private TextureTools.TextureData overlayTileImage;

		// Token: 0x0400091C RID: 2332
		private TextureTools.TextureData fullModelImage;

		// Token: 0x0400091D RID: 2333
		private TextureTools.TextureData bakedImage;

		// Token: 0x0400091E RID: 2334
		private MapTile.TextureBuffer.ImageLayer[] imageLayers;

		// Token: 0x0400091F RID: 2335
		private bool castsShadow;

		// Token: 0x04000920 RID: 2336
		private int fullModelLayerIndex;

		// Token: 0x04000921 RID: 2337
		private int numberOfGroundLayers = 1;

		// Token: 0x020003D0 RID: 976
		private struct ImageLayer
		{
			// Token: 0x06001D5A RID: 7514 RVA: 0x0007BDD5 File Offset: 0x00079FD5
			public ImageLayer(string path, short subImage, short sheetWidth, short sheetPadding)
			{
				this.path = path;
				this.subImage = subImage;
				this.sheetWidth = sheetWidth;
				this.sheetPadding = sheetPadding;
			}

			// Token: 0x04000C59 RID: 3161
			public string path;

			// Token: 0x04000C5A RID: 3162
			public short subImage;

			// Token: 0x04000C5B RID: 3163
			public short sheetWidth;

			// Token: 0x04000C5C RID: 3164
			public short sheetPadding;
		}
	}
}
