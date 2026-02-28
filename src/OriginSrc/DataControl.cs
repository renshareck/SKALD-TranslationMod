using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Token: 0x02000034 RID: 52
public class DataControl
{
	// Token: 0x06000550 RID: 1360 RVA: 0x00019383 File Offset: 0x00017583
	public DataControl()
	{
		TextParser.setTieIn(this);
	}

	// Token: 0x06000551 RID: 1361 RVA: 0x000193A8 File Offset: 0x000175A8
	public void initialize()
	{
		MainControl.log("Initializing Data Control");
		FactionControl.populateFactionList();
		this.questControl = new QuestControl();
		this.sideBench = new SideBench();
		this.achievementControl = new AchievementControl();
		Calendar.setCalendar();
		this.party = new Party();
		this.variableContainer = new GameVariableContainer();
		this.flagContainer = new SkaldFlagContainer();
		this.technicalDataRecord = new SkaldTechnicalDataRecord();
		this.journal = new Journal();
		this.addPremadeCharacters();
		GameData.populateMapList();
		this.eventManager = new MasterEventManager();
		this.craftingControl = new CraftingControl();
		this.settingsLoad();
		MainControl.log("Completed Data Control");
	}

	// Token: 0x06000552 RID: 1362 RVA: 0x00019452 File Offset: 0x00017652
	public void setStartingData()
	{
		this.gameStarted = true;
		this.mountMap(GameData.getStartingMapId());
		this.moveOverland(0, 0, true);
		this.processString(GameData.getStartingTrigger());
		this.setDescription("The adventure begins!");
	}

	// Token: 0x06000553 RID: 1363 RVA: 0x0001948C File Offset: 0x0001768C
	public bool mountModule(string moduleID)
	{
		MainControl.log("Attempting to mount module from folder: " + moduleID);
		try
		{
			SkaldModControl.setCurrentModProjectFolder(moduleID);
			GameData.loadData();
			this.initialize();
			this.addPremadeCharacters();
			this.setStartingData();
		}
		catch (Exception obj)
		{
			PopUpControl.addPopUpOK("ERROR: Module " + moduleID + " could not be loaded.");
			MainControl.logError(obj);
			return false;
		}
		return true;
	}

	// Token: 0x06000554 RID: 1364 RVA: 0x000194FC File Offset: 0x000176FC
	public void addPremadeCharacters()
	{
		MainControl.log("Initializing Party and adding premade characters");
		this.party = new Party();
		Character character = GameData.instantiateCharacter("CHA_Main");
		character.setMainCharacter();
		character.setPC(true);
		this.party.addAndSetAsMainParty(character);
		this.party.mergeInventory();
		this.party.setCurrentPC(character);
		MainControl.log("Completed Party and adding premade characters");
	}

	// Token: 0x06000555 RID: 1365 RVA: 0x00019566 File Offset: 0x00017766
	public string debug()
	{
		MainControl.activateAllDebugMessages();
		this.revealMap();
		return "Debugging Activated!";
	}

	// Token: 0x06000556 RID: 1366 RVA: 0x00019578 File Offset: 0x00017778
	public string testToHit()
	{
		MainControl.debugFunctions = false;
		if (!this.isCombatActive())
		{
			MainControl.logError("Combat is not active");
			return "Combat is not active";
		}
		Character currentCharacter = this.getCombatEncounter().getCurrentCharacter();
		if (currentCharacter == null)
		{
			MainControl.logError("No Attacker");
			return "No Attacker";
		}
		return currentCharacter.debugTestToHit();
	}

	// Token: 0x06000557 RID: 1367 RVA: 0x000195C8 File Offset: 0x000177C8
	public void setTacticalHoverText(string message)
	{
		if (this.isCombatActive())
		{
			this.getCombatEncounter().getCurrentCharacter().setTacticalHoverText(message);
		}
		this.getCurrentPC().setTacticalHoverText(message);
	}

	// Token: 0x06000558 RID: 1368 RVA: 0x000195EF File Offset: 0x000177EF
	public SkaldTechnicalDataRecord getTechnicalDataRecord()
	{
		if (this.technicalDataRecord == null)
		{
			this.technicalDataRecord = new SkaldTechnicalDataRecord();
		}
		return this.technicalDataRecord;
	}

	// Token: 0x06000559 RID: 1369 RVA: 0x0001960A File Offset: 0x0001780A
	public bool isGameStarted()
	{
		return this.gameStarted;
	}

	// Token: 0x0600055A RID: 1370 RVA: 0x00019612 File Offset: 0x00017812
	public PropWorkBench getWorkBench()
	{
		return this.workbench;
	}

	// Token: 0x0600055B RID: 1371 RVA: 0x0001961A File Offset: 0x0001781A
	public Journal getJournal()
	{
		return this.journal;
	}

	// Token: 0x0600055C RID: 1372 RVA: 0x00019622 File Offset: 0x00017822
	public void clearWorkbench()
	{
		this.workbench = null;
	}

	// Token: 0x0600055D RID: 1373 RVA: 0x0001962B File Offset: 0x0001782B
	public void mountWorkBench(PropWorkBench prop)
	{
		this.workbench = prop;
	}

	// Token: 0x0600055E RID: 1374 RVA: 0x00019634 File Offset: 0x00017834
	public string resetTouchTrigger(string characterId)
	{
		List<SkaldWorldObject> charactersById = GameData.getCharactersById(characterId);
		foreach (SkaldWorldObject skaldWorldObject in charactersById)
		{
			((Character)skaldWorldObject).resetTouchTrigger();
		}
		return "Reset touch trigger for " + charactersById.Count.ToString() + " Characters.";
	}

	// Token: 0x0600055F RID: 1375 RVA: 0x000196A8 File Offset: 0x000178A8
	public string printFunctions()
	{
		FunctionTools.saveFunctionList();
		return "Saving funtion list";
	}

	// Token: 0x06000560 RID: 1376 RVA: 0x000196B4 File Offset: 0x000178B4
	public string getRandomListEntry(string id)
	{
		return this.processString(GameData.getRandomStringListEntry(id));
	}

	// Token: 0x06000561 RID: 1377 RVA: 0x000196C4 File Offset: 0x000178C4
	public string getSessionId()
	{
		if (this.sessionID == null || this.sessionID == "")
		{
			try
			{
				string[] array = new string[]
				{
					"Red",
					"Green",
					"Blue",
					"Grim",
					"Slow",
					"Cruel",
					"Good",
					"Fine",
					"Just",
					"Hidden",
					"Grand",
					"Ancient",
					"Evil",
					"Raw",
					"Massive",
					"Hard",
					"Gentle",
					"Rapid",
					"Great",
					"Minor",
					"Strong",
					"Weak",
					"Void",
					"Bleak",
					"Radiant"
				};
				string[] array2 = new string[]
				{
					"Sword",
					"Shield",
					"Hand",
					"Mail",
					"Mutton",
					"Gate",
					"Claw",
					"Frost",
					"Skald",
					"Dagger",
					"Reaper",
					"Cult",
					"Robin",
					"Tides",
					"Star",
					"Wind",
					"Fire",
					"Clock",
					"Fey",
					"Wilds",
					"Ball",
					"Ray",
					"Flow",
					"Might",
					"Force",
					"Skill",
					"Riot",
					"Wall",
					"Row",
					"Army",
					"Spear",
					"Axe",
					"Flail",
					"Mace",
					"Sling",
					"Bow",
					"Arc",
					"Face",
					"Helm",
					"Fist",
					"Maw",
					"Time",
					"Life",
					"Hell",
					"Clan",
					"Plate",
					"Ivy",
					"Herb",
					"Belt",
					"Spell",
					"Raven",
					"Crow",
					"Tower",
					"Hole"
				};
				string[] array3 = new string[]
				{
					"OfDoom",
					"OfGlory",
					"OfDread",
					"OfJustice",
					"OfMadness",
					"OfGlory",
					"OfLife",
					"OfDarkness",
					"OfJoy",
					"OfGluttony",
					"OfGreed",
					"OfRage",
					"OfJudgement"
				};
				string[] array4 = array;
				object[] array5 = array;
				string str = array4[SkaldRandom.range(array5)];
				string[] array6 = array2;
				array5 = array2;
				string str2 = array6[SkaldRandom.range(array5)];
				array5 = array3;
				string str3 = array3[SkaldRandom.range(array5)];
				this.setSessionId(str + str2 + str3 + SkaldRandom.range(10000, 99999).ToString());
			}
			catch (Exception obj)
			{
				this.setSessionId("defaultSessionId");
				MainControl.logError(obj);
			}
		}
		return this.sessionID;
	}

	// Token: 0x06000562 RID: 1378 RVA: 0x00019AAC File Offset: 0x00017CAC
	public void setSessionId(string id)
	{
		if (id != "" && id != null)
		{
			this.sessionID = id;
		}
	}

	// Token: 0x06000563 RID: 1379 RVA: 0x00019AC5 File Offset: 0x00017CC5
	public void addCutSceneAdvanceChapter()
	{
		this.getJournal().advanceChapter();
		CutSceneControl.addTextCard(this.getJournal().printCurrentChapterString());
	}

	// Token: 0x06000564 RID: 1380 RVA: 0x00019AE2 File Offset: 0x00017CE2
	public void addCutSceneTextCard(string text)
	{
		CutSceneControl.addTextCard(text);
	}

	// Token: 0x06000565 RID: 1381 RVA: 0x00019AEA File Offset: 0x00017CEA
	public void addCutSceneAnimated(string cutsceneId)
	{
		CutSceneControl.addAnimatedCutScene(cutsceneId);
	}

	// Token: 0x06000566 RID: 1382 RVA: 0x00019AF2 File Offset: 0x00017CF2
	public void succeed()
	{
		PopUpControl.succeed();
	}

	// Token: 0x06000567 RID: 1383 RVA: 0x00019AF9 File Offset: 0x00017CF9
	public void showControlOverlayToolTip()
	{
		if (SkaldIO.isControllerConnected())
		{
			this.addHoverImage("CONTROLLER_Prompt");
			return;
		}
		this.addHoverImage("WASD_Prompt");
		this.addHoverImage("SHIFT_Prompt");
	}

	// Token: 0x06000568 RID: 1384 RVA: 0x00019B24 File Offset: 0x00017D24
	public void fail()
	{
		PopUpControl.fail();
	}

	// Token: 0x06000569 RID: 1385 RVA: 0x00019B2B File Offset: 0x00017D2B
	public void openSteam()
	{
		Application.OpenURL("https://store.steampowered.com/app/1069160/SKALD_Against_the_Black_Priory/");
	}

	// Token: 0x0600056A RID: 1386 RVA: 0x00019B37 File Offset: 0x00017D37
	public void openGOG()
	{
		Application.OpenURL("https://www.gog.com/game/skald_against_the_black_priory");
	}

	// Token: 0x0600056B RID: 1387 RVA: 0x00019B43 File Offset: 0x00017D43
	public void openDiscord()
	{
		Application.OpenURL("https://discord.gg/6wrFFdW");
	}

	// Token: 0x0600056C RID: 1388 RVA: 0x00019B4F File Offset: 0x00017D4F
	public void openModIO()
	{
		Application.OpenURL("https://mod.io/g/skald-against-the-bl");
	}

	// Token: 0x0600056D RID: 1389 RVA: 0x00019B5B File Offset: 0x00017D5B
	public void ascend()
	{
		MainControl.log("Ascending!");
		if (GlobalSettings.getGamePlaySettings().getAutoSaveOnEnterMap())
		{
			this.autoSave();
		}
		this.transposeToMap(this.currentMap.mapAboveId);
	}

	// Token: 0x0600056E RID: 1390 RVA: 0x00019B8A File Offset: 0x00017D8A
	public void descend()
	{
		if (GlobalSettings.getGamePlaySettings().getAutoSaveOnEnterMap())
		{
			this.autoSave();
		}
		this.transposeToMap(this.currentMap.mapBelowId);
	}

	// Token: 0x0600056F RID: 1391 RVA: 0x00019BAF File Offset: 0x00017DAF
	public void testSnippet()
	{
		MainControl.log(GameData.getTestSnippet());
		this.processString(GameData.getTestSnippet());
	}

	// Token: 0x06000570 RID: 1392 RVA: 0x00019BC8 File Offset: 0x00017DC8
	public void transposeToMap(string mapId)
	{
		int xpos = this.currentMap.getXPos();
		int ypos = this.currentMap.getYPos();
		this.mountMap(mapId);
		this.currentMap.setPositionTeleport(xpos, ypos);
	}

	// Token: 0x06000571 RID: 1393 RVA: 0x00019C04 File Offset: 0x00017E04
	public void printModelStrip(string frame)
	{
		int length = int.Parse(frame);
		this.party.getCurrentCharacter().printModelStrip(length);
	}

	// Token: 0x06000572 RID: 1394 RVA: 0x00019C29 File Offset: 0x00017E29
	public void revealMap()
	{
		this.currentMap.revealMap();
	}

	// Token: 0x06000573 RID: 1395 RVA: 0x00019C36 File Offset: 0x00017E36
	public void clearSceneImage()
	{
		if (!this.isSceneMounted())
		{
			return;
		}
		this.getCurrentScene().toggleNoImage();
	}

	// Token: 0x06000574 RID: 1396 RVA: 0x00019C4C File Offset: 0x00017E4C
	public string rain(string rounds)
	{
		this.currentMap.setRain(int.Parse(rounds));
		return "It begins to rain.";
	}

	// Token: 0x06000575 RID: 1397 RVA: 0x00019C64 File Offset: 0x00017E64
	public string fog(string rounds)
	{
		this.currentMap.setFog(int.Parse(rounds));
		return "Fog rolls in.";
	}

	// Token: 0x06000576 RID: 1398 RVA: 0x00019C7C File Offset: 0x00017E7C
	public void toggleLight()
	{
		this.getParty().toggleLightOnOff();
		this.currentMap.updateDrawLogic();
	}

	// Token: 0x06000577 RID: 1399 RVA: 0x00019C94 File Offset: 0x00017E94
	public void hide()
	{
		this.getCurrentPC().toggleHidden();
		this.passRound();
	}

	// Token: 0x06000578 RID: 1400 RVA: 0x00019CA7 File Offset: 0x00017EA7
	public void clearFogEffect()
	{
		this.currentMap.clearFogEffect();
	}

	// Token: 0x06000579 RID: 1401 RVA: 0x00019CB4 File Offset: 0x00017EB4
	public void clearRainEffect()
	{
		this.currentMap.clearRainEffect();
	}

	// Token: 0x0600057A RID: 1402 RVA: 0x00019CC1 File Offset: 0x00017EC1
	public Inventory getInventory()
	{
		return this.getParty().getInventory();
	}

	// Token: 0x0600057B RID: 1403 RVA: 0x00019CCE File Offset: 0x00017ECE
	public bool isArmed()
	{
		return this.getCurrentPC().isArmed();
	}

	// Token: 0x0600057C RID: 1404 RVA: 0x00019CE0 File Offset: 0x00017EE0
	public bool isUnarmed()
	{
		return !this.isArmed();
	}

	// Token: 0x0600057D RID: 1405 RVA: 0x00019CEB File Offset: 0x00017EEB
	public bool hasWeapon()
	{
		return this.getInventory().hasItemOfType("MeleeWeapon") || this.getInventory().hasItemOfType("RangedWeapon");
	}

	// Token: 0x0600057E RID: 1406 RVA: 0x00019D11 File Offset: 0x00017F11
	public void curtain()
	{
		if (this.currentMap == null)
		{
			return;
		}
		this.currentMap.setCurtain();
	}

	// Token: 0x0600057F RID: 1407 RVA: 0x00019D28 File Offset: 0x00017F28
	public void fireEffect(string effectId)
	{
		Effect effect = GameData.getEffect(effectId);
		if (effect != null)
		{
			effect.fireEffect(this.getCurrentPC(), this.getCurrentPC());
		}
	}

	// Token: 0x06000580 RID: 1408 RVA: 0x00019D51 File Offset: 0x00017F51
	public void recruitDirect(string npcId)
	{
		this.recruit(GameData.instantiateCharacter(npcId), true);
	}

	// Token: 0x06000581 RID: 1409 RVA: 0x00019D60 File Offset: 0x00017F60
	public void recruit(Character c, bool levelToCharacter = true)
	{
		if (c == null)
		{
			return;
		}
		if (c.isUnique() && this.party.containsObject(c.getId()))
		{
			MainControl.log("Could not add NPC: Already in party.");
			return;
		}
		Character currentCharacter = this.party.getCurrentCharacter();
		MapTile mapTile = c.getMapTile();
		if (mapTile != null)
		{
			mapTile.clearParty();
		}
		if (c.getLevel() < this.getMainCharacter().getLevel() && levelToCharacter)
		{
			c.addLevelDontLevelUp(this.getMainCharacter().getLevel() - c.getLevel());
		}
		if (!this.getParty().canPlayerPartyFitMoreMembers())
		{
			MainControl.logError("Trying to add character to party but party is full!");
			PopUpControl.addPopUpOK("There cannot be more than 6 characters in the active party.\n\nThis character is sent to the camp.");
			this.sideBench.add(c);
			return;
		}
		this.party.addAndSetAsMainParty(c);
		if (currentCharacter != null)
		{
			this.party.setCurrentPC(currentCharacter);
		}
		this.party.mergeInventory();
		this.setDescription(c.setPC(true));
		this.playSound("Fanfare7");
	}

	// Token: 0x06000582 RID: 1410 RVA: 0x00019E52 File Offset: 0x00018052
	public int getLevel()
	{
		return this.getMainCharacter().getLevel();
	}

	// Token: 0x06000583 RID: 1411 RVA: 0x00019E5F File Offset: 0x0001805F
	public void endGame()
	{
		MainControl.gotoDemoEnd();
	}

	// Token: 0x06000584 RID: 1412 RVA: 0x00019E66 File Offset: 0x00018066
	public void restartGame()
	{
		MainControl.restartGame();
	}

	// Token: 0x06000585 RID: 1413 RVA: 0x00019E6D File Offset: 0x0001806D
	public string makeFactionHostile(string factionId)
	{
		string result = FactionControl.makeFactionHostile(factionId);
		this.removeHostilePartyMembers();
		return result;
	}

	// Token: 0x06000586 RID: 1414 RVA: 0x00019E7B File Offset: 0x0001807B
	public string makeFactionFriendly(string factionId)
	{
		return FactionControl.makeFactionFriendly(factionId);
	}

	// Token: 0x06000587 RID: 1415 RVA: 0x00019E84 File Offset: 0x00018084
	public string makeFactionHostileIfSpotted(string factionId)
	{
		foreach (Party party in this.currentMap.nearByFriendlies)
		{
			if (!party.isEmpty() && party.getCurrentCharacter().isAlert() && party.getCurrentCharacter().isFactionMember(factionId))
			{
				return this.makeFactionHostile(factionId);
			}
		}
		return "";
	}

	// Token: 0x06000588 RID: 1416 RVA: 0x00019F0C File Offset: 0x0001810C
	public int getPartyCount()
	{
		return this.party.getCount();
	}

	// Token: 0x06000589 RID: 1417 RVA: 0x00019F19 File Offset: 0x00018119
	public bool isFactionHostile(string factionId)
	{
		return FactionControl.isFactionHostile(factionId);
	}

	// Token: 0x0600058A RID: 1418 RVA: 0x00019F24 File Offset: 0x00018124
	public void removeHostilePartyMembers()
	{
		Party party = this.getParty().removeHostileCharacters();
		if (party != null)
		{
			this.mountParty(party);
		}
	}

	// Token: 0x0600058B RID: 1419 RVA: 0x00019F47 File Offset: 0x00018147
	public void clearInteractParty()
	{
		this.getParty().clearInteractParty();
	}

	// Token: 0x0600058C RID: 1420 RVA: 0x00019F54 File Offset: 0x00018154
	public void clearCurrentParty()
	{
		this.clearInteractParty();
		this.getTargetTile().deleteParty();
	}

	// Token: 0x0600058D RID: 1421 RVA: 0x00019F67 File Offset: 0x00018167
	public void warpCurrentPartyToProp(string propId)
	{
		this.currentMap.warpCurrentPartyToProp(propId);
	}

	// Token: 0x0600058E RID: 1422 RVA: 0x00019F78 File Offset: 0x00018178
	public void warpToProp(string propId)
	{
		Prop firstPropByIdOnMap = GameData.getFirstPropByIdOnMap(this.currentMap.getId(), propId);
		if (firstPropByIdOnMap == null)
		{
			MainControl.logError("Did not find prop " + propId + " on map " + this.currentMap.getId());
			return;
		}
		this.gotoPoint(firstPropByIdOnMap.getTileX().ToString(), firstPropByIdOnMap.getTileY().ToString());
	}

	// Token: 0x0600058F RID: 1423 RVA: 0x00019FE0 File Offset: 0x000181E0
	public void warpToPropOnMap(string propId, string mapId)
	{
		this.mountMap(mapId);
		Prop firstPropByIdOnMap = GameData.getFirstPropByIdOnMap(this.currentMap.getId(), propId);
		if (firstPropByIdOnMap == null)
		{
			MainControl.logError("Did not find prop " + propId + " on map " + this.currentMap.getId());
			return;
		}
		this.gotoPoint(firstPropByIdOnMap.getTileX().ToString(), firstPropByIdOnMap.getTileY().ToString());
	}

	// Token: 0x06000590 RID: 1424 RVA: 0x0001A050 File Offset: 0x00018250
	public void warpCurrentPartyToPropOnMap(string propId, string mapId)
	{
		if (this.getTargetTile() == null || this.getTargetTile().getParty() == null)
		{
			MainControl.logError("No target party for prop " + propId + " on map " + mapId);
			return;
		}
		Party party = this.getTargetTile().getParty();
		Prop firstPropByIdOnMap = GameData.getFirstPropByIdOnMap(mapId, propId);
		if (firstPropByIdOnMap == null)
		{
			MainControl.logError("Did not find prop " + propId + " on map " + mapId);
			return;
		}
		this.getTargetTile().clearParty();
		if (mapId == this.currentMap.getId())
		{
			this.currentMap.attemptToPlaceCharacterCloseToPoint(firstPropByIdOnMap.getTileX(), firstPropByIdOnMap.getTileY(), party.getCurrentCharacter(), null);
			return;
		}
		party.setTilePosition(firstPropByIdOnMap.getTileX(), firstPropByIdOnMap.getTileY(), mapId);
	}

	// Token: 0x06000591 RID: 1425 RVA: 0x0001A108 File Offset: 0x00018308
	public void applyLoadoutToProps(string loadoutId, string propId)
	{
		this.applyLoadoutToPropsOnMap(loadoutId, propId, this.currentMap.getId());
	}

	// Token: 0x06000592 RID: 1426 RVA: 0x0001A120 File Offset: 0x00018320
	public void applyLoadoutToPropsOnMap(string loadoutId, string propId, string mapId)
	{
		foreach (SkaldWorldObject skaldWorldObject in GameData.getPropsByMap(mapId))
		{
			Prop prop = (Prop)skaldWorldObject;
			if (prop.isId(propId))
			{
				MapTile tile = this.currentMap.getTile(prop.getTileX(), prop.getTileY());
				if (tile != null)
				{
					GameData.applyLoadoutData(loadoutId, tile.getInventory());
				}
			}
		}
	}

	// Token: 0x06000593 RID: 1427 RVA: 0x0001A1A4 File Offset: 0x000183A4
	public void applyPlayerLoadout(string propId)
	{
		foreach (string loadoutId in this.getMainCharacter().getClass().getPlayerLoadoutListId())
		{
			this.applyLoadoutToProps(loadoutId, propId);
		}
	}

	// Token: 0x06000594 RID: 1428 RVA: 0x0001A204 File Offset: 0x00018404
	public string runScript(string scriptId)
	{
		string script = GameData.getScript(scriptId);
		if (script == "")
		{
			return "No script of empty script with ID " + scriptId;
		}
		return this.processString(script);
	}

	// Token: 0x06000595 RID: 1429 RVA: 0x0001A238 File Offset: 0x00018438
	private void mountInteractParty(Party p)
	{
		p.processContactTrigger();
		if (p.isInteractable() && !this.isSceneMounted() && !this.isCombatActive() && !this.isStoreMounted())
		{
			this.getParty().mountInteractParty(p);
		}
	}

	// Token: 0x06000596 RID: 1430 RVA: 0x0001A26D File Offset: 0x0001846D
	public bool isInteractPartyMounted()
	{
		return this.getParty().isInteractPartyMounted();
	}

	// Token: 0x06000597 RID: 1431 RVA: 0x0001A27A File Offset: 0x0001847A
	public Party getInteractParty()
	{
		return this.getParty().getInteractParty();
	}

	// Token: 0x06000598 RID: 1432 RVA: 0x0001A287 File Offset: 0x00018487
	public string getHeShe(string upperCase)
	{
		return this.getParty().getHeShe(upperCase);
	}

	// Token: 0x06000599 RID: 1433 RVA: 0x0001A295 File Offset: 0x00018495
	public string getHimHer(string upperCase)
	{
		return this.getParty().getHimHer(upperCase);
	}

	// Token: 0x0600059A RID: 1434 RVA: 0x0001A2A3 File Offset: 0x000184A3
	public string getNameParty(string upperCase)
	{
		return this.getParty().getNameParty(upperCase);
	}

	// Token: 0x0600059B RID: 1435 RVA: 0x0001A2B1 File Offset: 0x000184B1
	public bool testFlag(string flag)
	{
		return this.flagContainer.testFlag(flag);
	}

	// Token: 0x0600059C RID: 1436 RVA: 0x0001A2BF File Offset: 0x000184BF
	public string setFlag(string flag)
	{
		return this.flagContainer.setFlag(flag);
	}

	// Token: 0x0600059D RID: 1437 RVA: 0x0001A2CD File Offset: 0x000184CD
	public string clearFlag(string flag)
	{
		return this.flagContainer.clearFlag(flag);
	}

	// Token: 0x0600059E RID: 1438 RVA: 0x0001A2DB File Offset: 0x000184DB
	public string closeFlag(string flag)
	{
		return this.flagContainer.closeFlag(flag);
	}

	// Token: 0x0600059F RID: 1439 RVA: 0x0001A2E9 File Offset: 0x000184E9
	public string printFlags()
	{
		return this.flagContainer.printFlagList();
	}

	// Token: 0x060005A0 RID: 1440 RVA: 0x0001A2F8 File Offset: 0x000184F8
	public bool testAbilityRandom(string attributeId, string difficulty)
	{
		int difficulty2 = int.Parse(difficulty);
		return new SkaldTestRandomVsStatic(this.getCurrentPC(), attributeId, difficulty2, 1).wasSuccess();
	}

	// Token: 0x060005A1 RID: 1441 RVA: 0x0001A320 File Offset: 0x00018520
	public bool testAbilityGT(string attributeId, string difficulty)
	{
		int difficulty2 = int.Parse(difficulty);
		AttributesControl.CoreAttributes enumFromString = AttributesControl.getEnumFromString(attributeId);
		return enumFromString != AttributesControl.CoreAttributes.Null && new SkaldTestGreaterThan(this.getCurrentPC(), enumFromString, difficulty2).wasSuccess();
	}

	// Token: 0x060005A2 RID: 1442 RVA: 0x0001A354 File Offset: 0x00018554
	public bool hasItemOfType(string type)
	{
		return this.getInventory().hasItemOfType(type);
	}

	// Token: 0x060005A3 RID: 1443 RVA: 0x0001A362 File Offset: 0x00018562
	public bool isQuestStateOpen(string questId)
	{
		return this.questControl.isQuestStateOpen(questId);
	}

	// Token: 0x060005A4 RID: 1444 RVA: 0x0001A370 File Offset: 0x00018570
	public bool isQuestStateBegun(string questId)
	{
		return this.questControl.isQuestStateBegun(questId);
	}

	// Token: 0x060005A5 RID: 1445 RVA: 0x0001A37E File Offset: 0x0001857E
	public bool isQuestStateCompleted(string questId)
	{
		return this.questControl.isQuestStateCompleted(questId);
	}

	// Token: 0x060005A6 RID: 1446 RVA: 0x0001A38C File Offset: 0x0001858C
	public bool isQuestStateFailed(string questId)
	{
		return this.questControl.isQuestStateFailed(questId);
	}

	// Token: 0x060005A7 RID: 1447 RVA: 0x0001A39A File Offset: 0x0001859A
	public bool isQuestStateRewarded(string questId)
	{
		return this.questControl.isQuestStateRewarded(questId);
	}

	// Token: 0x060005A8 RID: 1448 RVA: 0x0001A3A8 File Offset: 0x000185A8
	public bool isQuestActive(string questId)
	{
		return this.questControl.isQuestActive(questId);
	}

	// Token: 0x060005A9 RID: 1449 RVA: 0x0001A3B6 File Offset: 0x000185B6
	public bool isQuestOpen(string questId)
	{
		return this.questControl.isQuestOpen(questId);
	}

	// Token: 0x060005AA RID: 1450 RVA: 0x0001A3C4 File Offset: 0x000185C4
	public bool isQuestNotBegun(string questId)
	{
		return !this.isQuestBegun(questId);
	}

	// Token: 0x060005AB RID: 1451 RVA: 0x0001A3D0 File Offset: 0x000185D0
	public bool isQuestBegun(string questId)
	{
		return this.questControl.isQuestBegun(questId);
	}

	// Token: 0x060005AC RID: 1452 RVA: 0x0001A3DE File Offset: 0x000185DE
	public bool isQuestCompleted(string questId)
	{
		return this.questControl.isQuestCompleted(questId);
	}

	// Token: 0x060005AD RID: 1453 RVA: 0x0001A3EC File Offset: 0x000185EC
	public bool isQuestNotCompleted(string questId)
	{
		return !this.questControl.isQuestCompleted(questId);
	}

	// Token: 0x060005AE RID: 1454 RVA: 0x0001A3FD File Offset: 0x000185FD
	public bool isQuestFailed(string questId)
	{
		return this.questControl.isQuestFailed(questId);
	}

	// Token: 0x060005AF RID: 1455 RVA: 0x0001A40B File Offset: 0x0001860B
	public bool isQuestReadyToStart(string questId)
	{
		return this.isQuestOpen(questId) && this.isQuestNotBegun(questId);
	}

	// Token: 0x060005B0 RID: 1456 RVA: 0x0001A41F File Offset: 0x0001861F
	public string getQuestAboutDescription(string questId)
	{
		return this.questControl.getAboutDescription(questId);
	}

	// Token: 0x060005B1 RID: 1457 RVA: 0x0001A42D File Offset: 0x0001862D
	public bool evaluateQuestSuccess(string questId)
	{
		return this.questControl.evaluateQuestSuccess(questId);
	}

	// Token: 0x060005B2 RID: 1458 RVA: 0x0001A43B File Offset: 0x0001863B
	public string lordLady()
	{
		if (this.getCurrentPC().isCharacterMale())
		{
			return "Lord";
		}
		return "Lady";
	}

	// Token: 0x060005B3 RID: 1459 RVA: 0x0001A455 File Offset: 0x00018655
	public bool isQuestRewarded(string questId)
	{
		return this.questControl.isQuestRewarded(questId);
	}

	// Token: 0x060005B4 RID: 1460 RVA: 0x0001A463 File Offset: 0x00018663
	public string openQuest(string questId)
	{
		return this.questControl.openQuest(questId);
	}

	// Token: 0x060005B5 RID: 1461 RVA: 0x0001A471 File Offset: 0x00018671
	public string beginQuest(string questId)
	{
		return this.questControl.beginQuest(questId);
	}

	// Token: 0x060005B6 RID: 1462 RVA: 0x0001A47F File Offset: 0x0001867F
	public string failQuest(string questId)
	{
		return this.questControl.failQuest(questId);
	}

	// Token: 0x060005B7 RID: 1463 RVA: 0x0001A48D File Offset: 0x0001868D
	public string completeQuest(string questId)
	{
		return this.questControl.completeQuest(questId);
	}

	// Token: 0x060005B8 RID: 1464 RVA: 0x0001A49B File Offset: 0x0001869B
	public string sealQuest(string questId)
	{
		return this.questControl.sealQuest(questId);
	}

	// Token: 0x060005B9 RID: 1465 RVA: 0x0001A4AC File Offset: 0x000186AC
	public string completeAndRewardQuestIfSuccessful(string questId)
	{
		if (!this.evaluateQuestSuccess(questId))
		{
			return "";
		}
		string text = this.completeQuest(questId);
		string text2 = this.rewardQuest(questId);
		if (text != "" && text2 != "")
		{
			return text + "\n\n" + text2;
		}
		if (text != "")
		{
			return text;
		}
		return text2;
	}

	// Token: 0x060005BA RID: 1466 RVA: 0x0001A510 File Offset: 0x00018710
	public string completeAndRewardQuest(string questId)
	{
		string text = this.completeQuest(questId);
		string text2 = this.rewardQuest(questId);
		if (text != "" && text2 != "")
		{
			return text + "\n\n" + text2;
		}
		if (text != "")
		{
			return text;
		}
		return text2;
	}

	// Token: 0x060005BB RID: 1467 RVA: 0x0001A564 File Offset: 0x00018764
	public string completeAndRewardOrFailQuest(string questId)
	{
		if (this.questControl.evaluateQuestSuccess(questId))
		{
			return this.completeAndRewardQuest(questId);
		}
		return this.failQuest(questId);
	}

	// Token: 0x060005BC RID: 1468 RVA: 0x0001A583 File Offset: 0x00018783
	public string rewardQuest(string questId)
	{
		return this.questControl.rewardQuest(questId, this.party);
	}

	// Token: 0x060005BD RID: 1469 RVA: 0x0001A597 File Offset: 0x00018797
	public void beginAllQuests()
	{
		this.questControl.beginAllQuests();
	}

	// Token: 0x060005BE RID: 1470 RVA: 0x0001A5A4 File Offset: 0x000187A4
	public void addAllJournalEntries()
	{
		this.journal.addAllJournalEntries();
	}

	// Token: 0x060005BF RID: 1471 RVA: 0x0001A5B1 File Offset: 0x000187B1
	public string conditionallyAddAbilityToParty(string abilityId)
	{
		return this.party.conditionallyAddAbilityToParty(abilityId);
	}

	// Token: 0x060005C0 RID: 1472 RVA: 0x0001A5BF File Offset: 0x000187BF
	public bool isCharacterNotAvailable(string npcId)
	{
		return !this.isCharacterAvailable(npcId);
	}

	// Token: 0x060005C1 RID: 1473 RVA: 0x0001A5CB File Offset: 0x000187CB
	public bool isCharacterAvailable(string npcId)
	{
		if (this.getParty().containsObject(npcId))
		{
			return true;
		}
		if (!npcId.Contains("CHA_"))
		{
			MainControl.logError("Suspected malformed Character ID with ID: " + npcId);
		}
		return false;
	}

	// Token: 0x060005C2 RID: 1474 RVA: 0x0001A5FB File Offset: 0x000187FB
	public bool isCharacterOnSideBench(string npcId)
	{
		return this.sideBench.containsObject(npcId);
	}

	// Token: 0x060005C3 RID: 1475 RVA: 0x0001A609 File Offset: 0x00018809
	public bool isCharacterAvailableOrOnSideBench(string npcId)
	{
		return this.isCharacterAvailable(npcId) || this.isCharacterOnSideBench(npcId);
	}

	// Token: 0x060005C4 RID: 1476 RVA: 0x0001A61D File Offset: 0x0001881D
	public bool isCharacterNotAvailableOrOnSideBench(string npcId)
	{
		return !this.isCharacterAvailableOrOnSideBench(npcId);
	}

	// Token: 0x060005C5 RID: 1477 RVA: 0x0001A629 File Offset: 0x00018829
	public bool isNotAlone()
	{
		return this.getParty().isNotAlone();
	}

	// Token: 0x060005C6 RID: 1478 RVA: 0x0001A636 File Offset: 0x00018836
	public string getQuestTitle()
	{
		return this.questControl.getQuestTitle();
	}

	// Token: 0x060005C7 RID: 1479 RVA: 0x0001A643 File Offset: 0x00018843
	public string printQuestList()
	{
		return this.questControl.printQuestList();
	}

	// Token: 0x060005C8 RID: 1480 RVA: 0x0001A650 File Offset: 0x00018850
	public string printQuestListStatus()
	{
		return this.questControl.printQuestListStatus();
	}

	// Token: 0x060005C9 RID: 1481 RVA: 0x0001A65D File Offset: 0x0001885D
	public string printVariables()
	{
		return this.variableContainer.printVariables();
	}

	// Token: 0x060005CA RID: 1482 RVA: 0x0001A66C File Offset: 0x0001886C
	public string printSceneData()
	{
		string text = "";
		if (this.currentMap == null)
		{
			text = "No map mounted\n";
		}
		else
		{
			text = text + "Map: " + this.currentMap.getId() + "\n";
			text = string.Concat(new string[]
			{
				text,
				"X Y: ",
				this.currentMap.getXPos().ToString(),
				"-",
				this.currentMap.getYPos().ToString(),
				"\n"
			});
			text = text + "Time: " + Calendar.getSaveDateStamp() + "\n\n";
		}
		text = text + "SceneSource: " + this.sceneSource + "\n";
		if (this.isSceneMounted())
		{
			text = text + "Scene: " + this.currentScene.getSceneId() + "\n";
		}
		else
		{
			text += "Scene: no scene mounted!\n";
		}
		return text;
	}

	// Token: 0x060005CB RID: 1483 RVA: 0x0001A764 File Offset: 0x00018964
	public string printFeedbackData()
	{
		string text = ">>> __**------Feedback------**__\n\n";
		if (this.party != null)
		{
			if (this.getMainCharacter() != null)
			{
				text = text + "**Player PC:** " + this.getMainCharacter().printFeedbackData() + "\n";
			}
			text = text + "**Party:** " + this.party.printCountList() + "\n";
			text = text + "**Current Character:** " + this.getCurrentPC().getName() + "\n";
			text = text + "**Difficulty: **" + GlobalSettings.getDifficultySettings().getCurrentDifficultyName() + "\n\n";
		}
		else
		{
			text += "*No party mounted*\n\n";
		}
		if (this.currentMap == null)
		{
			text += "*No map mounted*\n";
		}
		else
		{
			text = text + "**Map:** " + this.currentMap.getId() + "\n";
			text = string.Concat(new string[]
			{
				text,
				"**X\\Y**: ",
				this.currentMap.getXPos().ToString(),
				"-",
				this.currentMap.getYPos().ToString(),
				"\n"
			});
			text = text + "**Time**: " + Calendar.getSaveDateStamp() + "\n\n";
		}
		if (this.isSceneMounted())
		{
			text = text + "**SceneSource**: " + this.sceneSource + "\n";
			text = text + "**Scene**: " + this.currentScene.getSceneId() + "\n";
		}
		else
		{
			text += "*No scene mounted*\n";
		}
		text = text + "**Tags**: #" + this.getSessionId();
		if (this.isSceneMounted())
		{
			text += " #Scene";
		}
		if (this.isCombatActive())
		{
			text += " #Combat";
		}
		if (this.isStoreMounted())
		{
			text += " #Store";
		}
		if (this.isContainerMounted())
		{
			text += " #Container";
		}
		if (this.isInteractPartyMounted())
		{
			text += " #Interact";
		}
		return text;
	}

	// Token: 0x060005CC RID: 1484 RVA: 0x0001A964 File Offset: 0x00018B64
	public string printAllActiveComponentsId()
	{
		string text = "";
		if (this.isSceneMounted())
		{
			text = text + "SceneSource:" + this.sceneSource + "\n";
			text = text + "Scene: " + this.currentScene.getSceneId() + "\n";
		}
		if (this.isStoreMounted())
		{
			text = text + "Store:" + this.currentStore.getId() + "\n";
		}
		if (this.getTargetTile() != null && this.getTargetTile().getPropOrGuestProp() != null)
		{
			text = text + "Target Tile Prop: " + this.getTargetTile().getPropOrGuestProp().getId() + "\n";
		}
		if (this.getCurrentTile() != null && this.getCurrentTile().getPropOrGuestProp() != null)
		{
			text = text + "Current Tile Prop: " + this.getCurrentTile().getPropOrGuestProp().getId() + "\n";
		}
		if (this.isInteractPartyMounted())
		{
			text = text + "Interact Party\n" + this.getInteractParty().printList() + "\n";
		}
		return text;
	}

	// Token: 0x060005CD RID: 1485 RVA: 0x0001AA67 File Offset: 0x00018C67
	public string printInstanceList()
	{
		string text = GameData.printInstanceList();
		MainControl.log(text);
		return text;
	}

	// Token: 0x060005CE RID: 1486 RVA: 0x0001AA74 File Offset: 0x00018C74
	public string printFactions()
	{
		return FactionControl.printFactions();
	}

	// Token: 0x060005CF RID: 1487 RVA: 0x0001AA7B File Offset: 0x00018C7B
	public string addFaction(string factionId)
	{
		return this.getParty().addFaction(factionId);
	}

	// Token: 0x060005D0 RID: 1488 RVA: 0x0001AA89 File Offset: 0x00018C89
	public string printPCAbilityData()
	{
		return this.getCurrentPC().printAbilityData();
	}

	// Token: 0x060005D1 RID: 1489 RVA: 0x0001AA96 File Offset: 0x00018C96
	public int getGoldDropBonus()
	{
		return this.getMainCharacter().getGoldDropBonus();
	}

	// Token: 0x060005D2 RID: 1490 RVA: 0x0001AAA3 File Offset: 0x00018CA3
	public string getCurrentWeaponId()
	{
		if (this.getCurrentPC().getCurrentWeapon() == null)
		{
			return "";
		}
		return this.getCurrentPC().getCurrentWeapon().getId();
	}

	// Token: 0x060005D3 RID: 1491 RVA: 0x0001AAC8 File Offset: 0x00018CC8
	public string getRandomItemId(string type, string minRarity, string maxRarity, string minMagic, string maxMagic)
	{
		return GameData.getRandomItemIdList(type, int.Parse(minRarity), int.Parse(maxRarity), int.Parse(minMagic), int.Parse(maxMagic), 1)[0];
	}

	// Token: 0x060005D4 RID: 1492 RVA: 0x0001AAF4 File Offset: 0x00018CF4
	public string addItem(string itemId, string amount)
	{
		if (itemId == "" || amount == "0" || amount == "")
		{
			return "";
		}
		int amount2 = int.Parse(amount);
		this.getCurrentPC().getInventory().addItem(itemId, amount2);
		this.setDescription("Added some items to inventory!");
		AudioControl.playDefaultInventorySound();
		return itemId;
	}

	// Token: 0x060005D5 RID: 1493 RVA: 0x0001AB5A File Offset: 0x00018D5A
	public Party getParty()
	{
		return this.party;
	}

	// Token: 0x060005D6 RID: 1494 RVA: 0x0001AB62 File Offset: 0x00018D62
	public void removeItem()
	{
		this.getCurrentPC().getInventory().removeCurrentItem();
	}

	// Token: 0x060005D7 RID: 1495 RVA: 0x0001AB75 File Offset: 0x00018D75
	public string deleteItem(string itemId)
	{
		return this.getCurrentPC().getInventory().deleteItem(itemId);
	}

	// Token: 0x060005D8 RID: 1496 RVA: 0x0001AB88 File Offset: 0x00018D88
	public string deleteCurrentItem()
	{
		return this.getCurrentPC().getInventory().deleteCurrentItem();
	}

	// Token: 0x060005D9 RID: 1497 RVA: 0x0001AB9A File Offset: 0x00018D9A
	public string addVariable(string variable, string value)
	{
		return this.variableContainer.addVariable(variable, value);
	}

	// Token: 0x060005DA RID: 1498 RVA: 0x0001ABA9 File Offset: 0x00018DA9
	public string addVariableOnce(string variable, string value)
	{
		return this.variableContainer.addVariableOnce(variable, value);
	}

	// Token: 0x060005DB RID: 1499 RVA: 0x0001ABB8 File Offset: 0x00018DB8
	public string getVariable(string variable)
	{
		return this.variableContainer.getVariable(variable);
	}

	// Token: 0x060005DC RID: 1500 RVA: 0x0001ABC6 File Offset: 0x00018DC6
	public string addVitality(string input)
	{
		return this.getCurrentPC().addVitality(int.Parse(input));
	}

	// Token: 0x060005DD RID: 1501 RVA: 0x0001ABD9 File Offset: 0x00018DD9
	public string addAttunement(string input)
	{
		this.getCurrentPC().addAttunement(int.Parse(input));
		return input;
	}

	// Token: 0x060005DE RID: 1502 RVA: 0x0001ABF0 File Offset: 0x00018DF0
	public string addAbility(string abilityId)
	{
		Ability ability = GameData.getAbility(abilityId);
		if (ability == null)
		{
			return "No Ability found with ID" + abilityId;
		}
		this.getCurrentPC().addAbility(ability);
		return "Added Ability: " + abilityId;
	}

	// Token: 0x060005DF RID: 1503 RVA: 0x0001AC2A File Offset: 0x00018E2A
	public void setAttribute(string attributeId, string input)
	{
		this.getCurrentPC().setAttribute(attributeId, int.Parse(input));
	}

	// Token: 0x060005E0 RID: 1504 RVA: 0x0001AC3E File Offset: 0x00018E3E
	public CraftingControl getCraftingControl()
	{
		return this.craftingControl;
	}

	// Token: 0x060005E1 RID: 1505 RVA: 0x0001AC46 File Offset: 0x00018E46
	public SkaldActionResult pushOpponent()
	{
		if (!this.isCombatActive())
		{
			return new SkaldActionResult(false, false, "This only works in combat!", true);
		}
		return this.getCombatEncounter().pushTarget();
	}

	// Token: 0x060005E2 RID: 1506 RVA: 0x0001AC69 File Offset: 0x00018E69
	public SkaldActionResult overrunOpponent()
	{
		if (!this.isCombatActive())
		{
			return new SkaldActionResult(false, false, "This only works in combat!", true);
		}
		return this.getCombatEncounter().overrunTarget();
	}

	// Token: 0x060005E3 RID: 1507 RVA: 0x0001AC8C File Offset: 0x00018E8C
	public string addVitalityAll(string input)
	{
		return this.party.addVitalityAll(int.Parse(input));
	}

	// Token: 0x060005E4 RID: 1508 RVA: 0x0001AC9F File Offset: 0x00018E9F
	public string addAttunementAll(string input)
	{
		this.party.addAttunementAll(int.Parse(input));
		return input;
	}

	// Token: 0x060005E5 RID: 1509 RVA: 0x0001ACB3 File Offset: 0x00018EB3
	public string setBreath()
	{
		return this.party.setBreath();
	}

	// Token: 0x060005E6 RID: 1510 RVA: 0x0001ACC0 File Offset: 0x00018EC0
	public string updateBreath()
	{
		return this.party.updateBreath();
	}

	// Token: 0x060005E7 RID: 1511 RVA: 0x0001ACCD File Offset: 0x00018ECD
	public void healFull()
	{
		this.getCurrentPC().restoreAllFull();
		this.getCurrentPC().clearAllConditions();
	}

	// Token: 0x060005E8 RID: 1512 RVA: 0x0001ACE5 File Offset: 0x00018EE5
	public string healFullAll()
	{
		this.party.healFullAll();
		return "Healed full all.";
	}

	// Token: 0x060005E9 RID: 1513 RVA: 0x0001ACF7 File Offset: 0x00018EF7
	public string clearNegativeConditionsAll()
	{
		this.party.clearNegativeConditionsAll();
		return "Cleared all negative Conditions.";
	}

	// Token: 0x060005EA RID: 1514 RVA: 0x0001AD09 File Offset: 0x00018F09
	public string restShortAll()
	{
		this.party.restShortAll();
		return "";
	}

	// Token: 0x060005EB RID: 1515 RVA: 0x0001AD1B File Offset: 0x00018F1B
	public string resurrect()
	{
		this.getCurrentPC().resurrect();
		return "Ressurected party.";
	}

	// Token: 0x060005EC RID: 1516 RVA: 0x0001AD2D File Offset: 0x00018F2D
	public string resurrectAll()
	{
		this.party.resurrectAll();
		return "";
	}

	// Token: 0x060005ED RID: 1517 RVA: 0x0001AD40 File Offset: 0x00018F40
	public void takeDamage(string input)
	{
		int amount = int.Parse(input);
		this.getCurrentPC().takeDamage(new Damage(amount), true);
	}

	// Token: 0x060005EE RID: 1518 RVA: 0x0001AD66 File Offset: 0x00018F66
	public void takeDamageAllSilent(string input)
	{
		this.party.takeDamageAllSilent(int.Parse(input));
	}

	// Token: 0x060005EF RID: 1519 RVA: 0x0001AD79 File Offset: 0x00018F79
	public void takeDamageAll(string input)
	{
		this.party.takeDamageAll(int.Parse(input));
	}

	// Token: 0x060005F0 RID: 1520 RVA: 0x0001AD8C File Offset: 0x00018F8C
	public void takeDamageType(string input, string damageType)
	{
		this.getCurrentPC().takeDamage(int.Parse(input), new List<string>
		{
			damageType
		});
	}

	// Token: 0x060005F1 RID: 1521 RVA: 0x0001ADAB File Offset: 0x00018FAB
	public string takeDamageAllType(string input, string damageType)
	{
		this.party.takeDamageAll(int.Parse(input), new List<string>
		{
			damageType
		});
		return input;
	}

	// Token: 0x060005F2 RID: 1522 RVA: 0x0001ADCB File Offset: 0x00018FCB
	public string addXp(string gainedXP)
	{
		this.party.addXp(int.Parse(gainedXP));
		return gainedXP;
	}

	// Token: 0x060005F3 RID: 1523 RVA: 0x0001ADDF File Offset: 0x00018FDF
	public void addXpInt(int gainedXP)
	{
		this.party.addXp(gainedXP);
	}

	// Token: 0x060005F4 RID: 1524 RVA: 0x0001ADED File Offset: 0x00018FED
	public void levelUp()
	{
		if (this.getCurrentPC().canLevelUp())
		{
			PopUpControl.addPopUpLevelUp(this.getCurrentPC());
			return;
		}
		MainControl.logError("Current Character can't level up");
	}

	// Token: 0x060005F5 RID: 1525 RVA: 0x0001AE12 File Offset: 0x00019012
	public bool hasCondition(string conditionId)
	{
		return this.getCurrentPC().hasCondtion(conditionId);
	}

	// Token: 0x060005F6 RID: 1526 RVA: 0x0001AE20 File Offset: 0x00019020
	public string addLevel(string levels)
	{
		int levels2 = int.Parse(levels);
		this.getCurrentPC().addLevelDontLevelUp(levels2);
		return "";
	}

	// Token: 0x060005F7 RID: 1527 RVA: 0x0001AE45 File Offset: 0x00019045
	public void addConditionToAll(string conditionId)
	{
		this.getParty().addConditionToAll(conditionId);
	}

	// Token: 0x060005F8 RID: 1528 RVA: 0x0001AE53 File Offset: 0x00019053
	public void addCondition(string conditionId)
	{
		this.getParty().getCurrentCharacter().addConditionToCharacter(conditionId);
	}

	// Token: 0x060005F9 RID: 1529 RVA: 0x0001AE66 File Offset: 0x00019066
	public void purgeNPCs()
	{
		this.getParty().purgeNPCs();
	}

	// Token: 0x060005FA RID: 1530 RVA: 0x0001AE73 File Offset: 0x00019073
	public string playSound(string path)
	{
		AudioControl.playSound(path);
		return "";
	}

	// Token: 0x060005FB RID: 1531 RVA: 0x0001AE80 File Offset: 0x00019080
	public void stopMusic()
	{
		AudioControl.stopMusic();
	}

	// Token: 0x060005FC RID: 1532 RVA: 0x0001AE87 File Offset: 0x00019087
	public string playMusic(string path)
	{
		AudioControl.playMusic(path);
		return "";
	}

	// Token: 0x060005FD RID: 1533 RVA: 0x0001AE94 File Offset: 0x00019094
	public string setPortraitPath(string portraitPath)
	{
		if (this.getCurrentPC() != null)
		{
			this.getCurrentPC().setPortraitPath(portraitPath);
		}
		return portraitPath;
	}

	// Token: 0x060005FE RID: 1534 RVA: 0x0001AEAB File Offset: 0x000190AB
	public void deleteProp()
	{
		this.getTargetTile().deleteProp();
		this.passRound();
	}

	// Token: 0x060005FF RID: 1535 RVA: 0x0001AEC0 File Offset: 0x000190C0
	public string setProp(string propId)
	{
		Prop prop = GameData.instantiateProp(propId);
		if (prop != null)
		{
			this.currentMap.setProp(prop, this.getTargetTile());
		}
		return propId;
	}

	// Token: 0x06000600 RID: 1536 RVA: 0x0001AEEA File Offset: 0x000190EA
	public Prop getCurrentProp()
	{
		return this.getTargetTile().getPropOrGuestProp();
	}

	// Token: 0x06000601 RID: 1537 RVA: 0x0001AEF7 File Offset: 0x000190F7
	public void deleteAllPropsById(string propId)
	{
		if (this.currentMap != null)
		{
			this.currentMap.deleteAllPropsById(propId);
		}
	}

	// Token: 0x06000602 RID: 1538 RVA: 0x0001AF10 File Offset: 0x00019110
	public void deleteAllPropsByIdOnMap(string propId, string mapId)
	{
		Map map = GameData.getMap(mapId, "DataControl");
		if (map != null)
		{
			map.deleteAllPropsById(propId);
		}
	}

	// Token: 0x06000603 RID: 1539 RVA: 0x0001AF33 File Offset: 0x00019133
	public void replaceAllPropsById(string propId, string newProp)
	{
		if (this.currentMap != null)
		{
			this.currentMap.replaceAllPropsById(propId, newProp);
		}
	}

	// Token: 0x06000604 RID: 1540 RVA: 0x0001AF4C File Offset: 0x0001914C
	public void fixRopeDown()
	{
		MapTile tile = this.currentMap.getTile(this.party.getTileX(), this.party.getTileY() - 1);
		MapTile tile2 = this.currentMap.getTile(this.party.getTileX(), this.party.getTileY() - 4);
		if (tile == null || tile2 == null)
		{
			return;
		}
		this.currentMap.setProp(GameData.instantiateProp("PRO_RopeGenericDown"), tile);
		this.currentMap.setProp(GameData.instantiateProp("PRO_RopeGenericUp"), tile2);
		this.wait();
	}

	// Token: 0x06000605 RID: 1541 RVA: 0x0001AFDC File Offset: 0x000191DC
	public void fixRopeUp()
	{
		MapTile tile = this.currentMap.getTile(this.party.getTileX(), this.party.getTileY() + 1);
		MapTile tile2 = this.currentMap.getTile(this.party.getTileX(), this.party.getTileY() + 4);
		if (tile == null || tile2 == null)
		{
			return;
		}
		this.currentMap.setProp(GameData.instantiateProp("PRO_RopeGenericUp"), tile);
		this.currentMap.setProp(GameData.instantiateProp("PRO_RopeGenericDown"), tile2);
		this.wait();
	}

	// Token: 0x06000606 RID: 1542 RVA: 0x0001B06C File Offset: 0x0001926C
	public void replaceAllPropsByIdOnMap(string propId, string newProp, string mapId)
	{
		Map map = GameData.getMap(mapId, "DataControl");
		if (map != null)
		{
			map.replaceAllPropsById(propId, newProp);
		}
	}

	// Token: 0x06000607 RID: 1543 RVA: 0x0001B090 File Offset: 0x00019290
	public void createPropAt(string propId, string x, string y)
	{
		Prop prop = GameData.instantiateProp(propId);
		int x2 = int.Parse(x);
		int y2 = int.Parse(y);
		this.currentMap.setProp(prop, this.currentMap.getTile(x2, y2));
	}

	// Token: 0x06000608 RID: 1544 RVA: 0x0001B0CB File Offset: 0x000192CB
	public void activateAllPropsById(string propId)
	{
		if (this.currentMap != null)
		{
			this.currentMap.activateAllPropsById(propId);
		}
	}

	// Token: 0x06000609 RID: 1545 RVA: 0x0001B0E1 File Offset: 0x000192E1
	public void deactivateAllPropsById(string propId)
	{
		if (this.currentMap != null)
		{
			this.currentMap.deactivateAllPropsById(propId);
		}
	}

	// Token: 0x0600060A RID: 1546 RVA: 0x0001B0F7 File Offset: 0x000192F7
	public void unlockAllPropsById(string propId)
	{
		if (this.currentMap != null)
		{
			this.currentMap.unlockAllPropsById(propId);
		}
	}

	// Token: 0x0600060B RID: 1547 RVA: 0x0001B110 File Offset: 0x00019310
	public void deactivateAllPropsByIdOnMap(string propId, string mapId)
	{
		Map map = GameData.getMap(mapId, "DataControl");
		if (map != null)
		{
			map.deactivateAllPropsById(propId);
		}
	}

	// Token: 0x0600060C RID: 1548 RVA: 0x0001B134 File Offset: 0x00019334
	public void activateAllPropsByIdOnMap(string propId, string mapId)
	{
		Map map = GameData.getMap(mapId, "DataControl");
		if (map != null)
		{
			map.activateAllPropsById(propId);
		}
	}

	// Token: 0x0600060D RID: 1549 RVA: 0x0001B157 File Offset: 0x00019357
	public void toggleAllPropsById(string propId)
	{
		if (this.currentMap != null)
		{
			this.currentMap.toggleAllPropsById(propId);
		}
	}

	// Token: 0x0600060E RID: 1550 RVA: 0x0001B170 File Offset: 0x00019370
	public string killCharacter(string npcId)
	{
		List<SkaldWorldObject> charactersById = GameData.getCharactersById(npcId);
		foreach (SkaldWorldObject skaldWorldObject in charactersById)
		{
			Character character = (Character)skaldWorldObject;
			MapTile mapTile = character.getMapTile();
			if (mapTile != null)
			{
				character.dropLoot(mapTile.getInventory());
			}
			character.kill();
		}
		return "Killed " + charactersById.Count.ToString() + " characters.";
	}

	// Token: 0x0600060F RID: 1551 RVA: 0x0001B200 File Offset: 0x00019400
	public string addConditionToAllById(string conditionId, string npcId)
	{
		List<SkaldWorldObject> characterByMap = GameData.getCharacterByMap(this.currentMap.getId());
		int num = 0;
		foreach (SkaldWorldObject skaldWorldObject in characterByMap)
		{
			Character character = (Character)skaldWorldObject;
			if (character.isId(npcId))
			{
				character.addConditionToCharacter(conditionId);
				num++;
			}
		}
		return "Added condition to " + num.ToString() + " characters.";
	}

	// Token: 0x06000610 RID: 1552 RVA: 0x0001B288 File Offset: 0x00019488
	public void killTarget()
	{
		if (this.getCurrentPC().getTargetOpponent() != null)
		{
			this.getCurrentPC().getTargetOpponent().kill();
			return;
		}
		if (!this.isCombatActive() && this.getTargetTile().getCharacter() != null)
		{
			this.getTargetTile().getCharacter().kill();
		}
	}

	// Token: 0x06000611 RID: 1553 RVA: 0x0001B2D8 File Offset: 0x000194D8
	public bool isCharacterDead(string npcId)
	{
		List<SkaldWorldObject> charactersById = GameData.getCharactersById(npcId);
		if (charactersById.Count == 0)
		{
			return false;
		}
		using (List<SkaldWorldObject>.Enumerator enumerator = charactersById.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!((Character)enumerator.Current).isDead())
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x06000612 RID: 1554 RVA: 0x0001B344 File Offset: 0x00019544
	public bool isCharacterAlive(string npcId)
	{
		return !this.isCharacterDead(npcId);
	}

	// Token: 0x06000613 RID: 1555 RVA: 0x0001B350 File Offset: 0x00019550
	public bool testHorrynOccupation()
	{
		return this.hasJournalEntry("JOU_HorrynChief1Dead") && this.hasJournalEntry("JOU_HorrynChief2Dead") && this.hasJournalEntry("JOU_HorrynChief3Dead");
	}

	// Token: 0x06000614 RID: 1556 RVA: 0x0001B37C File Offset: 0x0001957C
	public void getChamberlainScene()
	{
		if (this.isQuestCompleted("QUE_StormingTheKeep"))
		{
			this.gotoScene("ChamberlainCastleTaken", "Intro");
			return;
		}
		if (this.isQuestCompleted("QUE_RetakingHorryn"))
		{
			this.gotoScene("HorrynOuterRetakenChamberlain", "Return");
			return;
		}
		if (this.evaluateQuestSuccess("QUE_Chapter1Refugees"))
		{
			this.gotoScene("Chapter1Iben", "Intro");
			return;
		}
		this.gotoScene("Chapter1Chamberlain", "Return");
	}

	// Token: 0x06000615 RID: 1557 RVA: 0x0001B3F8 File Offset: 0x000195F8
	public void addFullParty()
	{
		this.resetPlayer();
		string[] array = new string[]
		{
			"ITE_WeaponScimitarMagical",
			"ITE_WeaponHandaxeFine",
			"ITE_WeaponWarhammer",
			"ITE_WeaponBastardswordMagical",
			"ITE_WeaponWaraxeFine"
		};
		this.addItem(array[SkaldRandom.range(0, array.Length)], "1");
		this.equip();
		string[] array2 = new string[]
		{
			"ITE_ArmorBreastplate",
			"ITE_ArmorChainmail",
			"ITE_ArmorLeather",
			"ITE_ArmorStuddedLeather"
		};
		this.addItem(array2[SkaldRandom.range(0, array2.Length)], "1");
		this.equip();
		string[] array3 = new string[]
		{
			"ITE_ShieldBuckler",
			"ITE_ShieldLarge",
			"ITE_ShieldLargeSquare",
			"ITE_ShieldMedium",
			"ITE_ShieldMediumRound"
		};
		this.addItem(array3[SkaldRandom.range(0, array3.Length)], "1");
		this.equip();
		string[] array4 = new string[]
		{
			"ITE_OutfitBravosOutfit",
			"ITE_OutfitCheckeredTunic",
			"ITE_OutfitDoublet",
			"ITE_OutfitNobleOutfit",
			"ITE_OutfitTabbard",
			"ITE_OutfitRangersOutfit"
		};
		this.addItem(array4[SkaldRandom.range(0, array4.Length)], "1");
		this.equip();
		string[] array5 = new string[]
		{
			"ITE_AccessoryHelmetCoif",
			"ITE_AccessoryHelmetCoifAndHelmet",
			"ITE_AccessoryHelmetCoifAndWideHelmet",
			"ITE_AccessoryHelmetFull",
			"ITE_AccessoryHelmetFull2",
			"ITE_AccessoryHelmetMediumSteel",
			"ITE_AccessoryHelmetPlumed",
			"ITE_AccessoryHelmetRound"
		};
		this.addItem(array5[SkaldRandom.range(0, array5.Length)], "1");
		this.equip();
		this.addItem("ITE_PotionOfSpeed", "3");
		this.addItem("ITE_PotionOfStrength", "3");
		this.addItem("ITE_PotionHealingGreater", "3");
		this.addItem("ITE_MiscLantern", "1");
		this.addLevel("9");
		this.getCurrentPC().levelUp();
		this.addAllFeats();
		this.addAllLegalSpells();
		this.recruitDirect("CHA_Roland");
		this.party.setNextObject(1);
		this.getCurrentPC().levelUp();
		this.addAllFeats();
		array = new string[]
		{
			"ITE_WeaponGreathammer",
			"ITE_WeaponGreataxe",
			"ITE_WeaponTwoHander",
			"ITE_WeaponGreathammerFine",
			"ITE_WeaponGreataxeFine",
			"ITE_WeaponTwoHanderFine",
			"ITE_WeaponGreathammerMagical",
			"ITE_WeaponGreataxeMagical",
			"ITE_WeaponTwoHanderMagical"
		};
		this.addItem(array[SkaldRandom.range(0, array.Length)], "1");
		this.equip();
		this.addItem(array2[SkaldRandom.range(0, array2.Length)], "1");
		this.equip();
		this.recruitDirect("CHA_Embla");
		this.party.setNextObject(1);
		this.getCurrentPC().levelUp();
		this.addAllFeats();
		this.addAllLegalSpells();
		this.recruitDirect("CHA_Iago");
		this.party.setNextObject(1);
		this.getCurrentPC().levelUp();
		this.addAllFeats();
		this.recruitDirect("CHA_Driina");
		this.party.setNextObject(1);
		this.getCurrentPC().levelUp();
		this.addAllFeats();
		this.addAllLegalSpells();
		this.addItem(array3[SkaldRandom.range(0, array3.Length)], "1");
		this.equip();
		this.addItem(array5[SkaldRandom.range(0, array5.Length)], "1");
		this.equip();
		this.addItem(array2[SkaldRandom.range(0, array2.Length)], "1");
		this.equip();
		this.recruitDirect("CHA_Iben");
		this.party.setNextObject(1);
		this.getCurrentPC().levelUp();
		this.addAllFeats();
		this.addAllLegalSpells();
		this.addItem("ITE_ArrowBodkin", "10");
		this.equip();
		this.addItem(array3[SkaldRandom.range(0, array3.Length)], "1");
		this.equip();
		array = new string[]
		{
			"ITE_WeaponScimitarFine",
			"ITE_WeaponHandaxeMagical",
			"ITE_WeaponWarhammer",
			"ITE_WeaponBastardswordFine",
			"ITE_WeaponWaraxePoor"
		};
		this.addItem(array[SkaldRandom.range(0, array.Length)], "1");
		this.equip();
	}

	// Token: 0x06000616 RID: 1558 RVA: 0x0001B863 File Offset: 0x00019A63
	public void spawnByProps(string propId, string npcId)
	{
		if (this.isCharacterAvailableOrOnSideBench(npcId))
		{
			return;
		}
		if (this.currentMap != null)
		{
			this.currentMap.spawnByProps(propId, npcId);
		}
	}

	// Token: 0x06000617 RID: 1559 RVA: 0x0001B884 File Offset: 0x00019A84
	public string printSpellsByTier(string attributeId)
	{
		List<string>[] array = new List<string>[4];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = new List<string>();
		}
		foreach (SKALDProjectData.AbilityContainers.SpellContainer.SpellAbility spellAbility in GameData.getSpellRawDataList(attributeId))
		{
			if (spellAbility.tier - 1 < array.Length)
			{
				array[spellAbility.tier - 1].Add(spellAbility.title);
			}
		}
		string text = "";
		for (int j = 0; j < array.Length; j++)
		{
			text = string.Concat(new string[]
			{
				text,
				C64Color.WHITE_TAG,
				"TIER ",
				(j + 1).ToString(),
				"</color>\n"
			});
			foreach (string str in array[j])
			{
				text = text + str + "\n";
			}
			text += "\n";
		}
		return text;
	}

	// Token: 0x06000618 RID: 1560 RVA: 0x0001B9C0 File Offset: 0x00019BC0
	public void spawnByPropsOnMap(string npcId, string propId, string mapId)
	{
		if (this.getParty().containsObject(npcId))
		{
			return;
		}
		foreach (SkaldWorldObject skaldWorldObject in GameData.getPropsByMap(mapId))
		{
			Prop prop = (Prop)skaldWorldObject;
			if (prop.isId(propId))
			{
				Character character = GameData.instantiateCharacter(npcId);
				if (character != null)
				{
					if (mapId == this.currentMap.getId())
					{
						this.currentMap.attemptToPlaceCharacterCloseToPoint(prop.getTileX(), prop.getTileY(), character, null);
					}
					else
					{
						character.setTilePosition(prop.getTileX(), prop.getTileY(), mapId);
					}
				}
			}
		}
	}

	// Token: 0x06000619 RID: 1561 RVA: 0x0001BA78 File Offset: 0x00019C78
	public void moveNPCToProp(string propId, string npcId)
	{
		if (this.currentMap != null)
		{
			this.currentMap.moveNPCToProp(propId, npcId);
		}
	}

	// Token: 0x0600061A RID: 1562 RVA: 0x0001BA8F File Offset: 0x00019C8F
	private MapTile getTargetTile()
	{
		if (this.currentMap == null)
		{
			return null;
		}
		return this.currentMap.getTargetTile();
	}

	// Token: 0x0600061B RID: 1563 RVA: 0x0001BAA6 File Offset: 0x00019CA6
	public MapTile getCurrentTile()
	{
		if (this.currentMap == null)
		{
			return null;
		}
		return this.currentMap.getCurrentTile();
	}

	// Token: 0x0600061C RID: 1564 RVA: 0x0001BABD File Offset: 0x00019CBD
	public string testItemGT(string itemId, string count, string success, string failure)
	{
		if (this.getInventory().testItemGT(itemId, int.Parse(count)))
		{
			return success;
		}
		return failure;
	}

	// Token: 0x0600061D RID: 1565 RVA: 0x0001BAD7 File Offset: 0x00019CD7
	public int rollDice(string min, string max)
	{
		return new DicePoolVariable(int.Parse(min), int.Parse(max)).getResult();
	}

	// Token: 0x0600061E RID: 1566 RVA: 0x0001BAEF File Offset: 0x00019CEF
	public string variableAddition(string variable, string ammount)
	{
		return this.variableContainer.variableAddition(variable, ammount);
	}

	// Token: 0x0600061F RID: 1567 RVA: 0x0001BAFE File Offset: 0x00019CFE
	public string equip()
	{
		return this.getCurrentPC().equip();
	}

	// Token: 0x06000620 RID: 1568 RVA: 0x0001BB0C File Offset: 0x00019D0C
	public void createVehicleAt(string x, string y, string vehicleId)
	{
		Ship ship = GameData.instantiateShip(vehicleId);
		if (ship != null)
		{
			this.currentMap.getTile(int.Parse(x), int.Parse(y)).setVehicle(ship);
		}
	}

	// Token: 0x06000621 RID: 1569 RVA: 0x0001BB40 File Offset: 0x00019D40
	public void createVehicleAtProp(string vehicleId, string propId)
	{
		foreach (SkaldWorldObject skaldWorldObject in GameData.getPropsByMap(this.currentMap.getId()))
		{
			Prop prop = (Prop)skaldWorldObject;
			if (prop.isId(propId))
			{
				MapTile mapTile = prop.getMapTile();
				if (mapTile != null)
				{
					Ship ship = GameData.instantiateShip(vehicleId);
					if (ship != null)
					{
						mapTile.setVehicle(ship);
					}
				}
			}
		}
	}

	// Token: 0x06000622 RID: 1570 RVA: 0x0001BBC0 File Offset: 0x00019DC0
	public CombatEncounter getCombatEncounter()
	{
		return this.combatEncounter;
	}

	// Token: 0x06000623 RID: 1571 RVA: 0x0001BBC8 File Offset: 0x00019DC8
	public void changePC(int index)
	{
		this.party.changePC(index);
		if (index != 0)
		{
			this.setDescription(this.getCurrentPC().getFullNameUpper() + " is now leading the party.");
		}
	}

	// Token: 0x06000624 RID: 1572 RVA: 0x0001BBF6 File Offset: 0x00019DF6
	public string processString(string input)
	{
		if (input == null)
		{
			return "";
		}
		return TextParser.processString(input, null);
	}

	// Token: 0x06000625 RID: 1573 RVA: 0x0001BC08 File Offset: 0x00019E08
	public Inventory getContainerInventory()
	{
		if (!this.isContainerMounted())
		{
			return this.getCurrentTile().getInventory();
		}
		return this.containerInventory;
	}

	// Token: 0x06000626 RID: 1574 RVA: 0x0001BC24 File Offset: 0x00019E24
	public string printLog()
	{
		if (this.getCombatEncounter() == null)
		{
			return "No log mounted.";
		}
		string text = "";
		foreach (SkaldBaseObject skaldBaseObject in CombatLog.getEntries())
		{
			string fullDescriptionAndHeader = skaldBaseObject.getFullDescriptionAndHeader();
			MainControl.log(fullDescriptionAndHeader);
			text = text + fullDescriptionAndHeader + "\n\n";
		}
		return text;
	}

	// Token: 0x06000627 RID: 1575 RVA: 0x0001BC9C File Offset: 0x00019E9C
	public string printLogLast()
	{
		if (this.getCombatEncounter() == null)
		{
			return "No log mounted.";
		}
		string result = "";
		List<SkaldBaseObject> entries = CombatLog.getEntries();
		if (entries != null && entries.Count != 0)
		{
			string fullDescriptionAndHeader = entries[entries.Count - 1].getFullDescriptionAndHeader();
			MainControl.log(fullDescriptionAndHeader);
			result = fullDescriptionAndHeader;
		}
		return result;
	}

	// Token: 0x06000628 RID: 1576 RVA: 0x0001BCE9 File Offset: 0x00019EE9
	public string addPopUpOK(string input)
	{
		PopUpControl.addPopUpOK(input);
		return input;
	}

	// Token: 0x06000629 RID: 1577 RVA: 0x0001BCF2 File Offset: 0x00019EF2
	public void addPopUpUnimplemented()
	{
		PopUpControl.addPopUpSystemMenu("This feature is not implemented yet! It will be soon though, so stay posted by following the project on Steam!");
	}

	// Token: 0x0600062A RID: 1578 RVA: 0x0001BCFE File Offset: 0x00019EFE
	public void clearContainer()
	{
		this.containerInventory = null;
	}

	// Token: 0x0600062B RID: 1579 RVA: 0x0001BD07 File Offset: 0x00019F07
	public void gotoContainerMap()
	{
		if (this.currentMap.containerMapId != "")
		{
			this.mountMap(this.currentMap.containerMapId);
		}
	}

	// Token: 0x0600062C RID: 1580 RVA: 0x0001BD32 File Offset: 0x00019F32
	public string mountMap(string mapId)
	{
		return this.mountMap(mapId, true);
	}

	// Token: 0x0600062D RID: 1581 RVA: 0x0001BD3C File Offset: 0x00019F3C
	private string mountMap(string mapId, bool fireTriggers)
	{
		this.getParty().clearNavigationCourse();
		Map map = GameData.getMap(mapId, "DataControl");
		return this.mountMapDirect(map, fireTriggers);
	}

	// Token: 0x0600062E RID: 1582 RVA: 0x0001BD68 File Offset: 0x00019F68
	public string activateEncounter(string encounterId)
	{
		return this.eventManager.activateEncounter(encounterId);
	}

	// Token: 0x0600062F RID: 1583 RVA: 0x0001BD76 File Offset: 0x00019F76
	public void clearMovePath()
	{
		this.getParty().clearNavigationCourse();
	}

	// Token: 0x06000630 RID: 1584 RVA: 0x0001BD83 File Offset: 0x00019F83
	private void prepareToSnapMove()
	{
		if (this.currentMap != null)
		{
			this.currentMap.stopScroll();
		}
		this.clearMovePath();
		this.party.snapToGrid();
	}

	// Token: 0x06000631 RID: 1585 RVA: 0x0001BDAC File Offset: 0x00019FAC
	public string mountMapDirect(Map newMap, bool fireTriggers)
	{
		this.clearMovePath();
		if (newMap == null)
		{
			MainControl.logError("Trying to load non-existant map.");
			return "";
		}
		if (this.currentMap != null)
		{
			this.currentMap.clearMapUpkeep(fireTriggers);
		}
		this.currentMap = newMap;
		this.currentMap.mountMapUpkeep(this.party, fireTriggers);
		this.changePC(0);
		this.eventManager.partiallyResetCounters();
		string musicPath = this.currentMap.getMusicPath();
		if (this.getCurrentTile().hasVehicle())
		{
			this.getParty().setVehicle(this.getCurrentTile().transferVehicle());
		}
		if (musicPath != null)
		{
			this.playMusic(musicPath);
		}
		else
		{
			this.playMusic("campfire");
		}
		return newMap.getId();
	}

	// Token: 0x06000632 RID: 1586 RVA: 0x0001BE5F File Offset: 0x0001A05F
	public string applyLoadout(string loadoutId)
	{
		this.getCurrentPC().applyLoadout(loadoutId);
		return "Applying loadout!";
	}

	// Token: 0x06000633 RID: 1587 RVA: 0x0001BE74 File Offset: 0x0001A074
	private void mountMapFromTile()
	{
		if (this.getTargetTile().getNestedMapId() != "")
		{
			if (GlobalSettings.getGamePlaySettings().getAutoSaveOnEnterMap())
			{
				this.autoSave();
			}
			Map map = GameData.getMap(this.getTargetTile().getNestedMapId(), this.currentMap.getId());
			if (map.enterPrompt)
			{
				string[] options = new string[]
				{
					"Enter",
					"Leave"
				};
				string[] targets = new string[]
				{
					"{clearScene}",
					"{clearScene}"
				};
				string[] exitTriggers = new string[]
				{
					string.Concat(new string[]
					{
						"{gotoMap|",
						map.getId(),
						";",
						map.startX.ToString(),
						";",
						map.startY.ToString(),
						"}"
					}),
					""
				};
				SceneNode scene = new SceneNode(map.getName(), map.getDescription(), map.getImagePath(), options, targets, exitTriggers);
				this.setScene(scene);
				return;
			}
			this.gotoMap(map.getId(), map.startX.ToString(), map.startY.ToString());
		}
	}

	// Token: 0x06000634 RID: 1588 RVA: 0x0001BFAC File Offset: 0x0001A1AC
	public string gotoMap(string mapId, string x, string y)
	{
		this.mountMap(mapId);
		this.gotoPoint(x, y);
		return mapId;
	}

	// Token: 0x06000635 RID: 1589 RVA: 0x0001BFC0 File Offset: 0x0001A1C0
	public string addMin(string minutes)
	{
		return Calendar.addMin(minutes);
	}

	// Token: 0x06000636 RID: 1590 RVA: 0x0001BFC8 File Offset: 0x0001A1C8
	public void updateMapTime()
	{
		Calendar.addSec(this.currentMap.getTravelTimeInSeconds());
	}

	// Token: 0x06000637 RID: 1591 RVA: 0x0001BFDB File Offset: 0x0001A1DB
	public void makeCampAtInn()
	{
		if (GlobalSettings.getGamePlaySettings().getAutoSaveOnRest())
		{
			this.autoSave();
		}
		this.campingOrder = new CampActivityState.CampingOrderInn();
	}

	// Token: 0x06000638 RID: 1592 RVA: 0x0001BFFA File Offset: 0x0001A1FA
	public void makeCampWithBed()
	{
		if (!this.currentMap.canSleepInBedOnMap())
		{
			PopUpControl.addPopUpOK("You cannot rest anywhere in this area. You must look for a suitable camp-site elsewhere.");
			return;
		}
		if (GlobalSettings.getGamePlaySettings().getAutoSaveOnRest())
		{
			this.autoSave();
		}
		this.campingOrder = new CampActivityState.CampingOrderBed();
	}

	// Token: 0x06000639 RID: 1593 RVA: 0x0001C034 File Offset: 0x0001A234
	public void makeCampRough()
	{
		if (!this.currentMap.canMakeCampOnThisMap() && !this.currentMap.canSleepInBedOnMap())
		{
			PopUpControl.addPopUpOK("You cannot rest anywhere in this area. You must look for a suitable camp-site elsewhere.");
			return;
		}
		if (!this.currentMap.canMakeCampOnThisMap())
		{
			PopUpControl.addPopUpOK("You cannot make camp in this area. However, you can sleep in a bed if you can find one!");
			return;
		}
		if (GlobalSettings.getGamePlaySettings().getAutoSaveOnRest())
		{
			this.autoSave();
		}
		this.campingOrder = new CampActivityState.CampingOrderRoughCamp();
	}

	// Token: 0x0600063A RID: 1594 RVA: 0x0001C09B File Offset: 0x0001A29B
	public void restFull()
	{
		this.healFullAll();
		this.restUpkeep();
	}

	// Token: 0x0600063B RID: 1595 RVA: 0x0001C0AA File Offset: 0x0001A2AA
	public void restPartial(float degree)
	{
		this.getParty().healPartialAll(degree);
		this.restUpkeep();
	}

	// Token: 0x0600063C RID: 1596 RVA: 0x0001C0BE File Offset: 0x0001A2BE
	private void restUpkeep()
	{
		this.addHours("8");
		this.wait();
		this.setOverland(-1, -1, true);
		this.setDescription("You rest for 8 hours.");
		this.curtain();
	}

	// Token: 0x0600063D RID: 1597 RVA: 0x0001C0EE File Offset: 0x0001A2EE
	public bool isCampActivityMounted()
	{
		return this.campingOrder != null;
	}

	// Token: 0x0600063E RID: 1598 RVA: 0x0001C0F9 File Offset: 0x0001A2F9
	public void clearCamp()
	{
		this.campingOrder = null;
	}

	// Token: 0x0600063F RID: 1599 RVA: 0x0001C102 File Offset: 0x0001A302
	public CampActivityState.CampingOrder getCampingOrder()
	{
		return this.campingOrder;
	}

	// Token: 0x06000640 RID: 1600 RVA: 0x0001C10C File Offset: 0x0001A30C
	public string interactWithVehicle()
	{
		if (!this.getParty().hasVehicle())
		{
			MainControl.logError("No vehicle is set!");
			return "No vehicle is set!";
		}
		string vehicleNestedMap = this.getParty().getVehicleNestedMap();
		if (vehicleNestedMap != "")
		{
			this.getCurrentTile().setVehicle(this.getParty().transferVehicle());
			this.mountMap(vehicleNestedMap);
		}
		return "Mounting Map Id: " + vehicleNestedMap;
	}

	// Token: 0x06000641 RID: 1601 RVA: 0x0001C178 File Offset: 0x0001A378
	public string getDayTime()
	{
		return Calendar.getDayTimeString();
	}

	// Token: 0x06000642 RID: 1602 RVA: 0x0001C17F File Offset: 0x0001A37F
	public string addHours(string hours)
	{
		return Calendar.addHours(hours);
	}

	// Token: 0x06000643 RID: 1603 RVA: 0x0001C187 File Offset: 0x0001A387
	public void setHour(string hour)
	{
		Calendar.setHour(hour);
	}

	// Token: 0x06000644 RID: 1604 RVA: 0x0001C18F File Offset: 0x0001A38F
	public string addDays(string days)
	{
		return Calendar.addDays(days);
	}

	// Token: 0x06000645 RID: 1605 RVA: 0x0001C197 File Offset: 0x0001A397
	public string addYears(string years)
	{
		return Calendar.addYears(years);
	}

	// Token: 0x06000646 RID: 1606 RVA: 0x0001C19F File Offset: 0x0001A39F
	public void resetHiddenDegree()
	{
		this.getParty().getCurrentCharacter().resetHiddenDegree();
	}

	// Token: 0x06000647 RID: 1607 RVA: 0x0001C1B1 File Offset: 0x0001A3B1
	public string addVocalBark(string bark)
	{
		this.getCurrentPC().addVocalBarkLocal(bark);
		return bark;
	}

	// Token: 0x06000648 RID: 1608 RVA: 0x0001C1C0 File Offset: 0x0001A3C0
	public string addPositiveBark(string bark)
	{
		this.getCurrentPC().addPositiveBark(bark);
		return bark;
	}

	// Token: 0x06000649 RID: 1609 RVA: 0x0001C1CF File Offset: 0x0001A3CF
	public void addJournalEntry(string journalId)
	{
		this.journal.addEntry(journalId);
	}

	// Token: 0x0600064A RID: 1610 RVA: 0x0001C1DD File Offset: 0x0001A3DD
	public bool notHasJournalEntry(string journalId)
	{
		return !this.journal.containsObject(journalId);
	}

	// Token: 0x0600064B RID: 1611 RVA: 0x0001C1EE File Offset: 0x0001A3EE
	public bool hasJournalEntry(string journalId)
	{
		return this.journal.containsObject(journalId);
	}

	// Token: 0x0600064C RID: 1612 RVA: 0x0001C1FC File Offset: 0x0001A3FC
	public string getDay()
	{
		return Calendar.getDay();
	}

	// Token: 0x0600064D RID: 1613 RVA: 0x0001C204 File Offset: 0x0001A404
	public string unlock()
	{
		Prop currentProp = this.getCurrentProp();
		if (currentProp != null && currentProp is PropLockable)
		{
			(currentProp as PropLockable).unlock();
			return "Click";
		}
		return "No Prop";
	}

	// Token: 0x0600064E RID: 1614 RVA: 0x0001C239 File Offset: 0x0001A439
	public string getHour()
	{
		return Calendar.getHour();
	}

	// Token: 0x0600064F RID: 1615 RVA: 0x0001C240 File Offset: 0x0001A440
	public bool isPartyDead()
	{
		return this.getCurrentPC() == null || this.party.isPartyDead();
	}

	// Token: 0x06000650 RID: 1616 RVA: 0x0001C257 File Offset: 0x0001A457
	public void setSceneSource(string path)
	{
		this.sceneSource = path;
	}

	// Token: 0x06000651 RID: 1617 RVA: 0x0001C260 File Offset: 0x0001A460
	public string gotoScene(string sceneId, string nodeId)
	{
		this.setSceneSource(sceneId);
		this.mountScene(nodeId);
		return nodeId;
	}

	// Token: 0x06000652 RID: 1618 RVA: 0x0001C274 File Offset: 0x0001A474
	public string mountScene(string nodeId)
	{
		if (nodeId == "")
		{
			return "";
		}
		if (GameData.getSceneSetMainCharacter(nodeId, this.sceneSource))
		{
			this.setMainCharacter();
		}
		SceneNode scene = GameData.getScene(nodeId, this.sceneSource);
		if (scene.clearIfDead && this.isPartyDead())
		{
			this.clearScene();
		}
		else
		{
			this.setScene(scene);
		}
		return nodeId;
	}

	// Token: 0x06000653 RID: 1619 RVA: 0x0001C2D5 File Offset: 0x0001A4D5
	public void mountSimpleScene(string title, string description)
	{
		this.setScene(new SceneNode(title, description, ""));
	}

	// Token: 0x06000654 RID: 1620 RVA: 0x0001C2EC File Offset: 0x0001A4EC
	private void setScene(SceneNode s)
	{
		if (this.isSceneMounted())
		{
			this.currentScene.setSceneNode(s);
		}
		else
		{
			this.currentScene = new Scene(s);
		}
		this.newSceneMounted = true;
		if (this.currentScene.getSetMainCharacter())
		{
			this.setMainCharacter();
		}
		this.party.getCurrentCharacter().clearHidden();
	}

	// Token: 0x06000655 RID: 1621 RVA: 0x0001C348 File Offset: 0x0001A548
	public string getBuffer()
	{
		if (this.currentMap == null)
		{
			return "";
		}
		return "" + "\n" + Calendar.printDayTimeFormatted() + "\n" + this.getPositionString() + "\n" + "\n " + this.currentMap.printWeatherDescription() + "\n " + Calendar.getDayTimeString();
	}

	// Token: 0x06000656 RID: 1622 RVA: 0x0001C3B8 File Offset: 0x0001A5B8
	public string getPositionString()
	{
		string text = "";
		if (this.currentMap != null)
		{
			text = text + TextTools.formateNameValuePairYellow("X Pos.:", this.currentMap.getXPos()) + "\n";
			text += TextTools.formateNameValuePairYellow("Y Pos.:", this.currentMap.getYPos());
		}
		return text;
	}

	// Token: 0x06000657 RID: 1623 RVA: 0x0001C411 File Offset: 0x0001A611
	public string shakeScreen(string frames)
	{
		this.currentMap.setScreenShake(int.Parse(frames));
		return frames;
	}

	// Token: 0x06000658 RID: 1624 RVA: 0x0001C425 File Offset: 0x0001A625
	public string flashScreen(string frames)
	{
		this.currentMap.setFlash(int.Parse(frames));
		return frames;
	}

	// Token: 0x06000659 RID: 1625 RVA: 0x0001C439 File Offset: 0x0001A639
	public string thunder()
	{
		this.currentMap.setFlash(10);
		this.currentMap.setScreenShake(25);
		return "";
	}

	// Token: 0x0600065A RID: 1626 RVA: 0x0001C45A File Offset: 0x0001A65A
	public void clearScene()
	{
		if (this.currentScene == null)
		{
			return;
		}
		if (!this.newSceneMounted)
		{
			this.currentScene = null;
		}
	}

	// Token: 0x0600065B RID: 1627 RVA: 0x0001C474 File Offset: 0x0001A674
	public bool isSceneMounted()
	{
		return this.currentScene != null;
	}

	// Token: 0x0600065C RID: 1628 RVA: 0x0001C47F File Offset: 0x0001A67F
	public bool isRandomEncounterMounted()
	{
		return this.randomEncounter != null;
	}

	// Token: 0x0600065D RID: 1629 RVA: 0x0001C48A File Offset: 0x0001A68A
	public void clearRandomEncounter()
	{
		this.randomEncounter = null;
	}

	// Token: 0x0600065E RID: 1630 RVA: 0x0001C493 File Offset: 0x0001A693
	public EncounterControl.Encounter getRandomEncounter()
	{
		return this.randomEncounter;
	}

	// Token: 0x0600065F RID: 1631 RVA: 0x0001C49C File Offset: 0x0001A69C
	public bool moveOverland(int xDist, int yDist, bool bumpTile = true)
	{
		if (yDist > 0 && xDist == 0)
		{
			this.party.setFacing(0);
		}
		else if (yDist < 0 && xDist == 0)
		{
			this.party.setFacing(2);
		}
		else if (xDist > 0 && yDist == 0)
		{
			this.party.setFacing(1);
		}
		else if (xDist < 0 && yDist == 0)
		{
			this.party.setFacing(3);
		}
		int targetX = xDist + this.currentMap.getXPos();
		int targetY = yDist + this.currentMap.getYPos();
		this.setOverland(targetX, targetY, bumpTile);
		return true;
	}

	// Token: 0x06000660 RID: 1632 RVA: 0x0001C524 File Offset: 0x0001A724
	public void skip(string x, string y)
	{
		this.prepareToSnapMove();
		int xDist = int.Parse(x);
		int yDist = int.Parse(y);
		this.moveOverland(xDist, yDist, true);
		this.curtain();
	}

	// Token: 0x06000661 RID: 1633 RVA: 0x0001C558 File Offset: 0x0001A758
	public string getTileVerb()
	{
		if (this.getTargetTile() == null)
		{
			return "";
		}
		string text = this.getTargetTile().getVerb();
		if (this.itemsOnGround())
		{
			text = "Get Items";
		}
		if (text == "")
		{
			text = "";
		}
		if (text != "")
		{
			text = text.ToUpper();
		}
		return text;
	}

	// Token: 0x06000662 RID: 1634 RVA: 0x0001C5B5 File Offset: 0x0001A7B5
	public void resetPlayer()
	{
		this.purgeNPCs();
		this.healFull();
		this.purgeInventory();
	}

	// Token: 0x06000663 RID: 1635 RVA: 0x0001C5C9 File Offset: 0x0001A7C9
	public SideBench getSideBench()
	{
		return this.sideBench;
	}

	// Token: 0x06000664 RID: 1636 RVA: 0x0001C5D1 File Offset: 0x0001A7D1
	public string setToMale()
	{
		return this.getCurrentPC().setIsCharacterMale(true);
	}

	// Token: 0x06000665 RID: 1637 RVA: 0x0001C5DF File Offset: 0x0001A7DF
	public string setToFemale()
	{
		return this.getCurrentPC().setIsCharacterMale(true);
	}

	// Token: 0x06000666 RID: 1638 RVA: 0x0001C5ED File Offset: 0x0001A7ED
	public void purgeInventory()
	{
		this.getParty().getInventory().deleteAllItems();
	}

	// Token: 0x06000667 RID: 1639 RVA: 0x0001C5FF File Offset: 0x0001A7FF
	public void clearAllNPCs()
	{
		if (this.currentMap != null)
		{
			this.currentMap.clearAllNPCs();
		}
	}

	// Token: 0x06000668 RID: 1640 RVA: 0x0001C614 File Offset: 0x0001A814
	public string setCurrentPropModelPath(string path)
	{
		Prop currentProp = this.getCurrentProp();
		if (currentProp != null)
		{
			currentProp.setModelPath(path);
			return "Setting path " + path + " for porp " + currentProp.getId();
		}
		return "No prop found!";
	}

	// Token: 0x06000669 RID: 1641 RVA: 0x0001C650 File Offset: 0x0001A850
	public string getCurrentPropModelPath()
	{
		Prop currentProp = this.getCurrentProp();
		if (currentProp != null)
		{
			return currentProp.getModelPath();
		}
		return "";
	}

	// Token: 0x0600066A RID: 1642 RVA: 0x0001C673 File Offset: 0x0001A873
	public void centerViewport()
	{
		if (this.currentMap != null)
		{
			this.currentMap.centerViewPort();
		}
	}

	// Token: 0x0600066B RID: 1643 RVA: 0x0001C688 File Offset: 0x0001A888
	public bool isPrioryLevel1Open()
	{
		return this.getVariable("VAR_PrioryStatue1") == "1" && this.getVariable("VAR_PrioryStatue2") == "2" && this.getVariable("VAR_PrioryStatue3") == "3" && this.getVariable("VAR_PrioryStatue0") == "0";
	}

	// Token: 0x0600066C RID: 1644 RVA: 0x0001C6F1 File Offset: 0x0001A8F1
	public string getDaysSinceStart()
	{
		return Calendar.getDaysSinceStart();
	}

	// Token: 0x0600066D RID: 1645 RVA: 0x0001C6F8 File Offset: 0x0001A8F8
	public string printNPCLocation(string npcId)
	{
		List<SkaldWorldObject> charactersById = GameData.getCharactersById(npcId);
		string text = "";
		foreach (SkaldWorldObject skaldWorldObject in charactersById)
		{
			Character character = (Character)skaldWorldObject;
			text = string.Concat(new string[]
			{
				text,
				"MAP: ",
				character.getContainerMapId(),
				" | X:",
				character.getTileX().ToString(),
				" / Y:",
				character.getTileY().ToString(),
				" | Status:",
				character.printInitiativeStatus(),
				"\n"
			});
		}
		MainControl.log(text);
		return text;
	}

	// Token: 0x0600066E RID: 1646 RVA: 0x0001C7C8 File Offset: 0x0001A9C8
	public void clearAllNPCsByFaction(string factionId)
	{
		if (this.currentMap != null)
		{
			this.currentMap.clearAllNPCsByFaction(factionId);
		}
	}

	// Token: 0x0600066F RID: 1647 RVA: 0x0001C7E0 File Offset: 0x0001A9E0
	public void clearAllNPCsByFactionOnMap(string factionId, string mapId)
	{
		Map map = GameData.getMap(mapId, "DataControl");
		if (map == null)
		{
			return;
		}
		map.clearAllNPCsByFaction(factionId);
	}

	// Token: 0x06000670 RID: 1648 RVA: 0x0001C804 File Offset: 0x0001AA04
	public void clearAllNPCsByIdOnMap(string npcId, string mapId)
	{
		Map map = GameData.getMap(mapId, "DataControl");
		if (map == null)
		{
			return;
		}
		map.clearAllNPCsById(npcId);
	}

	// Token: 0x06000671 RID: 1649 RVA: 0x0001C828 File Offset: 0x0001AA28
	public string testGoldLootEquivalent()
	{
		Item currentObject = this.getInventory().getCurrentObject();
		if (currentObject == null)
		{
			return "No Item";
		}
		return "Loot Value: " + currentObject.getGoldLootEquivalent().ToString() + "/" + currentObject.getGoldLootEquivalent2().ToString();
	}

	// Token: 0x06000672 RID: 1650 RVA: 0x0001C875 File Offset: 0x0001AA75
	public void clearAllNPCsById(string npcId)
	{
		this.currentMap.clearAllNPCsById(npcId);
	}

	// Token: 0x06000673 RID: 1651 RVA: 0x0001C883 File Offset: 0x0001AA83
	public void clearNearbyNPCs()
	{
		if (this.currentMap != null)
		{
			this.currentMap.clearNearbyNPCs();
		}
	}

	// Token: 0x06000674 RID: 1652 RVA: 0x0001C898 File Offset: 0x0001AA98
	public string enforceResolution()
	{
		ScreenControl.enforceResolution();
		return "Enforcing resolution!";
	}

	// Token: 0x06000675 RID: 1653 RVA: 0x0001C8A4 File Offset: 0x0001AAA4
	public string swapScreenMode()
	{
		ScreenControl.swapScreenMode();
		return "Swapping screen mode!";
	}

	// Token: 0x06000676 RID: 1654 RVA: 0x0001C8B0 File Offset: 0x0001AAB0
	public void clearNearbyEnemies()
	{
		if (this.currentMap != null)
		{
			this.currentMap.clearNearbyEnemies();
		}
	}

	// Token: 0x06000677 RID: 1655 RVA: 0x0001C8C5 File Offset: 0x0001AAC5
	public string setNPCMoveMode(string npcId, string moveMode)
	{
		return this.currentMap.setNPCMoveMode(npcId, moveMode);
	}

	// Token: 0x06000678 RID: 1656 RVA: 0x0001C8D4 File Offset: 0x0001AAD4
	public void purgeInstancesByMapId(string mapId)
	{
		GameData.purgeInstancesByMapId(mapId);
	}

	// Token: 0x06000679 RID: 1657 RVA: 0x0001C8DC File Offset: 0x0001AADC
	public string benchmarkDrawPipeline()
	{
		MainControl.log(BenchMarkTools.benchmarkDrawPipeline(1000000));
		return "Benchmark completed!";
	}

	// Token: 0x0600067A RID: 1658 RVA: 0x0001C8F2 File Offset: 0x0001AAF2
	public string gotoPoint(string targetX, string targetY)
	{
		this.prepareToSnapMove();
		this.setOverland(int.Parse(targetX), int.Parse(targetY), true);
		this.prepareToSnapMove();
		this.currentMap.updateDrawLogic();
		return "Went to " + targetX + "/" + targetY;
	}

	// Token: 0x0600067B RID: 1659 RVA: 0x0001C930 File Offset: 0x0001AB30
	public void mountMapEdgePrompt(string id)
	{
		this.mountMap(id);
	}

	// Token: 0x0600067C RID: 1660 RVA: 0x0001C93A File Offset: 0x0001AB3A
	public void mountMapEdge(string id)
	{
		this.mountMap(id);
		this.currentMap.setPosition();
	}

	// Token: 0x0600067D RID: 1661 RVA: 0x0001C94F File Offset: 0x0001AB4F
	public void winGame(string cutsceneId)
	{
		this.setAchievement("ACH_AllIsDarkness");
		MainControl.winGame(cutsceneId);
	}

	// Token: 0x0600067E RID: 1662 RVA: 0x0001C964 File Offset: 0x0001AB64
	public string getYear()
	{
		return Calendar.getYear().ToString();
	}

	// Token: 0x0600067F RID: 1663 RVA: 0x0001C97E File Offset: 0x0001AB7E
	public CharacterBuilderBaseState.CharacterCreatorUseCase getCharacterCreatorUseCase()
	{
		return this.characterCreatorUseCase;
	}

	// Token: 0x06000680 RID: 1664 RVA: 0x0001C986 File Offset: 0x0001AB86
	public void clearCharacterCreatorUseCase()
	{
		this.characterCreatorUseCase = null;
	}

	// Token: 0x06000681 RID: 1665 RVA: 0x0001C98F File Offset: 0x0001AB8F
	public void editCharacterAsMerc(Character character)
	{
		this.characterCreatorUseCase = new CharacterBuilderBaseState.CharacterCreatorUseCaseMercenary(character);
	}

	// Token: 0x06000682 RID: 1666 RVA: 0x0001C99D File Offset: 0x0001AB9D
	public void editCurrentCharacterAsMain()
	{
		this.characterCreatorUseCase = new CharacterBuilderBaseState.CharacterCreatorUseCaseMain(this.getParty().getCurrentCharacter());
	}

	// Token: 0x06000683 RID: 1667 RVA: 0x0001C9B5 File Offset: 0x0001ABB5
	public bool isDeluxeEdition()
	{
		return MainControl.isDeluxeEdition();
	}

	// Token: 0x06000684 RID: 1668 RVA: 0x0001C9BC File Offset: 0x0001ABBC
	public void makeCurrentPartyHostile()
	{
		if (this.isInteractPartyMounted())
		{
			this.getInteractParty().setHostile(true);
			this.clearInteractParty();
			return;
		}
		if (this.getTargetTile() != null && this.getTargetTile().getParty() != null && !this.getTargetTile().getParty().isPC())
		{
			this.getTargetTile().getParty().setHostile(true);
		}
	}

	// Token: 0x06000685 RID: 1669 RVA: 0x0001CA20 File Offset: 0x0001AC20
	public string printSaveName(int saveNumber)
	{
		return string.Concat(new string[]
		{
			"Save",
			saveNumber.ToString(),
			" ",
			this.getMainCharacter().getName(),
			" ",
			DateTime.Now.ToString("MM.dd.yy"),
			" ",
			DateTime.Now.ToString("HH.mm.ss")
		});
	}

	// Token: 0x06000686 RID: 1670 RVA: 0x0001CA9C File Offset: 0x0001AC9C
	public bool setOverland(int targetX, int targetY, bool shouldITileBump = true)
	{
		if (!this.getParty().navigationCourseHasNodes())
		{
			this.currentMap.setExamineTile(-1, -1);
		}
		if (!this.currentMap.isScrollReady())
		{
			return false;
		}
		if (this.getCurrentPC().isDead())
		{
			this.getParty().getCurrentCharacter().addNegativeBark("Unconscious");
			this.setDescription(this.getCurrentPC().getNameColored() + " is not conscious.\nYou must select a conscious character to act!");
			this.clearMovePath();
			return false;
		}
		if (targetX == -1 && targetY == -1)
		{
			if (!this.isCombatActive())
			{
				this.updateMapTime();
			}
			this.currentMap.setPosition();
			this.clearMovePath();
			if (this.getCurrentTile().getParty() != null && !this.getCurrentTile().getParty().isPC())
			{
				if (!this.getCurrentTile().getParty().isHostile())
				{
					this.mountInteractParty(this.getCurrentTile().getParty());
				}
				return false;
			}
			return false;
		}
		else
		{
			this.setDescription("");
			if (!this.currentMap.isTileValid(targetX, targetY))
			{
				this.clearMovePath();
				if (this.currentMap.testExitByEdge(targetX, targetY) != "")
				{
					this.mountMapEdgePrompt(this.currentMap.testExitByEdge(targetX, targetY));
					return true;
				}
				this.appendDesc("You can't go that way!");
				return false;
			}
			else
			{
				bool flag = false;
				if (shouldITileBump)
				{
					if (this.getTargetTile() == this.currentMap.getTile(targetX, targetY))
					{
						flag = true;
					}
					else
					{
						this.currentMap.setTargetTile(targetX, targetY);
					}
				}
				else
				{
					flag = true;
					this.currentMap.setTargetTile(targetX, targetY);
				}
				this.getTargetTile().processTryEnterTrigger();
				if (!this.getTargetTile().isPassable())
				{
					this.clearMovePath();
					this.currentMap.setExamineTile(targetX, targetY);
					string a = "";
					if (!flag)
					{
						a = this.getTargetTile().getShortInspectDescription();
					}
					if (a != "")
					{
						this.appendDesc(a);
					}
					this.currentMap.setPosition();
					if (this.getCurrentTile().isWater())
					{
						return false;
					}
					if (flag)
					{
						this.bumpToTile();
					}
					this.updateMapTime();
					if (this.getCurrentTile().getParty() != null && !this.getCurrentTile().getParty().isPC())
					{
						if (!this.getCurrentTile().getParty().isHostile())
						{
							this.mountInteractParty(this.getCurrentTile().getParty());
						}
						return false;
					}
					if (this.getTargetTile().getNestedMapId() != "")
					{
						this.mountMapFromTile();
						return false;
					}
					if (flag && this.getTargetTile().isInteractive())
					{
						this.verbTrigger();
					}
					return false;
				}
				else
				{
					if (this.getTargetTile().getParty() != null && !this.getTargetTile().getParty().isPC())
					{
						this.clearMovePath();
						if (this.getTargetTile().getParty().isHostile())
						{
							this.launchCombat();
						}
						else
						{
							this.mountInteractParty(this.getTargetTile().getParty());
							this.setDescription(this.getTerrainDesc());
						}
						return false;
					}
					if (this.getTargetTile().isWater())
					{
						if (this.getTargetTile().hasVehicle())
						{
							if (this.getParty().hasVehicle())
							{
								this.clearMovePath();
								this.setDescription("You encounter another ship!");
								return false;
							}
							this.getParty().setVehicle(this.getTargetTile().transferVehicle());
							this.setDescription("You board a ship!");
						}
						else if (!this.getParty().hasVehicle())
						{
							this.setDescription("Water blocks your path!");
							this.clearMovePath();
							if (flag)
							{
								this.bumpToTile();
							}
							return false;
						}
					}
					else if (this.getParty().hasVehicle())
					{
						this.getCurrentTile().setVehicle(this.getParty().transferVehicle());
						this.setDescription("You make landfall!");
					}
					if (this.getParty().isEncumbered())
					{
						this.setDescription("The party is encumbered and cannot move!");
						PopUpControl.addPopUpEncumberance();
						this.clearMovePath();
						return false;
					}
					this.currentMap.setPosition(targetX, targetY);
					this.currentMap.getCurrentTile().setVisited();
					this.updateMapTime();
					this.currentMap.getCurrentTile().playMoveSound();
					this.setDescription(this.getTerrainDesc());
					if (this.getTargetTile().getParty() != null && !this.getTargetTile().getParty().isPC())
					{
						this.clearMovePath();
						if (!this.getTargetTile().getParty().isHostile())
						{
							this.mountInteractParty(this.getTargetTile().getParty());
							this.setDescription(this.getTerrainDesc());
							return false;
						}
					}
					this.triggerEvent();
					return true;
				}
			}
		}
	}

	// Token: 0x06000687 RID: 1671 RVA: 0x0001CEF7 File Offset: 0x0001B0F7
	public void setMainCharacter()
	{
		this.getParty().setMainCharacter();
	}

	// Token: 0x06000688 RID: 1672 RVA: 0x0001CF05 File Offset: 0x0001B105
	public string getModFolderContent()
	{
		return SkaldModControl.getModFolderContent().printList();
	}

	// Token: 0x06000689 RID: 1673 RVA: 0x0001CF11 File Offset: 0x0001B111
	public string printResolutions()
	{
		string text = ScreenControl.printResolutions();
		MainControl.log(text);
		return text;
	}

	// Token: 0x0600068A RID: 1674 RVA: 0x0001CF1E File Offset: 0x0001B11E
	public string debugLight()
	{
		MainControl.debugLight = !MainControl.debugLight;
		return "Toggle Debugging Light!";
	}

	// Token: 0x0600068B RID: 1675 RVA: 0x0001CF32 File Offset: 0x0001B132
	public string printVsync()
	{
		string text = ScreenControl.printVsyncValues();
		MainControl.log(text);
		return text;
	}

	// Token: 0x0600068C RID: 1676 RVA: 0x0001CF40 File Offset: 0x0001B140
	public string addAllItems(string category)
	{
		List<string> allItems = GameData.getAllItems(category);
		foreach (string id in allItems)
		{
			this.getInventory().addItem(GameData.instantiateItem(id));
		}
		return "Added " + allItems.Count.ToString() + " items. Try keywords: CONSUMABLES, AMMO, ACCESSORIES, SHIELD, FOODS, REAGENTS, BOOKS, GEMS, TRINKETS, JEWELRY, ADVENTURING, MELEE, RANGED, ARMOR, MISC, TOMES, RECIPE, OUTFITS";
	}

	// Token: 0x0600068D RID: 1677 RVA: 0x0001CFC0 File Offset: 0x0001B1C0
	public string addRandomItems(string category, string count)
	{
		List<string> allItems = GameData.getAllItems(category);
		int num = 0;
		if (!int.TryParse(count, out num))
		{
			return "Malformed count.";
		}
		int i = 0;
		if (allItems.Count > 0)
		{
			while (i < num)
			{
				this.getInventory().addItem(GameData.instantiateItem(allItems[SkaldRandom.range(0, allItems.Count - 1)]));
				i++;
			}
		}
		return "Added " + i.ToString() + " items. Try keywords: CONSUMABLES, AMMO, ACCESSORIES, SHIELD, FOODS, REAGENTS, BOOKS, GEMS, TRINKETS, JEWELRY, ADVENTURING, MELEE, RANGED, ARMOR, MISC, TOMES, RECIPE, OUTFITS";
	}

	// Token: 0x0600068E RID: 1678 RVA: 0x0001D036 File Offset: 0x0001B236
	public void setCharacterName(string name)
	{
		this.getParty().getCurrentCharacter().setName(name);
	}

	// Token: 0x0600068F RID: 1679 RVA: 0x0001D04A File Offset: 0x0001B24A
	public void activateCurrentProp()
	{
		if (this.getTargetTile() != null && this.getTargetTile().getPropOrGuestProp() != null)
		{
			this.getTargetTile().getPropOrGuestProp().activate();
		}
	}

	// Token: 0x06000690 RID: 1680 RVA: 0x0001D071 File Offset: 0x0001B271
	private void bumpToTile()
	{
		this.getParty().getCurrentCharacter().setPixelTargetInterpolated(this.getTargetTile().getPixelX(), this.getTargetTile().getPixelY(), 2f, 0.25f);
		AudioControl.playBumpSound();
	}

	// Token: 0x06000691 RID: 1681 RVA: 0x0001D0A8 File Offset: 0x0001B2A8
	public void deactivateCurrentProp()
	{
		if (this.getTargetTile() != null && this.getTargetTile().getPropOrGuestProp() != null)
		{
			this.getTargetTile().getPropOrGuestProp().deactivate();
		}
	}

	// Token: 0x06000692 RID: 1682 RVA: 0x0001D0CF File Offset: 0x0001B2CF
	public string printEvents()
	{
		return this.eventManager.printEvents();
	}

	// Token: 0x06000693 RID: 1683 RVA: 0x0001D0DC File Offset: 0x0001B2DC
	public bool isMapOverland()
	{
		return this.currentMap.overland;
	}

	// Token: 0x06000694 RID: 1684 RVA: 0x0001D0EE File Offset: 0x0001B2EE
	public bool isMapWilderness()
	{
		return this.currentMap.wilderness;
	}

	// Token: 0x06000695 RID: 1685 RVA: 0x0001D100 File Offset: 0x0001B300
	public bool isMapCity()
	{
		return this.currentMap.city;
	}

	// Token: 0x06000696 RID: 1686 RVA: 0x0001D112 File Offset: 0x0001B312
	public bool isMapIndoors()
	{
		return this.currentMap.indoors;
	}

	// Token: 0x06000697 RID: 1687 RVA: 0x0001D124 File Offset: 0x0001B324
	public string triggerEventDirect(string eventId)
	{
		DynamicEventControl.SkaldEvent @event = this.eventManager.getEvent(eventId);
		if (@event == null)
		{
			return "No event triggered!";
		}
		if (@event.testCondition())
		{
			return this.executeEvent(@event);
		}
		return "Event condition not met for: " + @event.getId();
	}

	// Token: 0x06000698 RID: 1688 RVA: 0x0001D168 File Offset: 0x0001B368
	public string triggerEvent()
	{
		if (!this.currentMap.dynamicEnc)
		{
			return this.currentMap.getId() + " does not allow dynamic events!";
		}
		DynamicEventControl.SkaldEvent skaldEvent = this.eventManager.triggerEvent(this.getCurrentTile());
		if (skaldEvent != null)
		{
			return this.executeEvent(skaldEvent);
		}
		if (this.currentMap.overland)
		{
			return this.launchEncounter();
		}
		return "No event triggered.";
	}

	// Token: 0x06000699 RID: 1689 RVA: 0x0001D1D0 File Offset: 0x0001B3D0
	private string executeEvent(DynamicEventControl.SkaldEvent e)
	{
		if (e.getDescription() != "")
		{
			if (e.getImagePath() != "")
			{
				string[] options = new string[]
				{
					"Continue"
				};
				string[] targets = new string[]
				{
					"{clearScene}"
				};
				string[] exitTriggers = new string[]
				{
					e.getTrigger()
				};
				SceneNode scene = new SceneNode(e.getName(), e.getDescription(), e.getImagePath(), options, targets, exitTriggers);
				this.setScene(scene);
			}
			else
			{
				this.mountSimpleScene(e.getName(), e.getDescription());
			}
		}
		else
		{
			this.processString(e.getTrigger());
		}
		if (MainControl.debugFunctions)
		{
			MainControl.log("Firing event:" + e.getId());
		}
		if (!e.isRepeatable())
		{
			this.deleteEvent(e.getId());
		}
		return "Triggered event: " + e.getId();
	}

	// Token: 0x0600069A RID: 1690 RVA: 0x0001D2B5 File Offset: 0x0001B4B5
	public void addGameWinScreen()
	{
		CutSceneControl.addGameWinScreen();
	}

	// Token: 0x0600069B RID: 1691 RVA: 0x0001D2BC File Offset: 0x0001B4BC
	public string launchEncounter()
	{
		EncounterControl.Encounter encounter = this.eventManager.triggerEncounter(this.getCurrentTile());
		if (encounter == null)
		{
			return "No encounter found!";
		}
		this.randomEncounter = encounter;
		if (MainControl.debugFunctions)
		{
			MainControl.log("Firing encounter:" + encounter.getId());
		}
		return "Mounted encounter: " + encounter.getId();
	}

	// Token: 0x0600069C RID: 1692 RVA: 0x0001D317 File Offset: 0x0001B517
	public string deleteEvent(string eventId)
	{
		return this.eventManager.deleteEvent(eventId);
	}

	// Token: 0x0600069D RID: 1693 RVA: 0x0001D325 File Offset: 0x0001B525
	public string activateEvent(string eventId)
	{
		return this.eventManager.activateEvent(eventId);
	}

	// Token: 0x0600069E RID: 1694 RVA: 0x0001D333 File Offset: 0x0001B533
	public string deactivateEvent(string eventId)
	{
		return this.eventManager.deactivateEvent(eventId);
	}

	// Token: 0x0600069F RID: 1695 RVA: 0x0001D341 File Offset: 0x0001B541
	public bool shouldILaunchCombat()
	{
		return this.currentMap.isMapReady() && this.currentMap.areOpponentsAlert();
	}

	// Token: 0x060006A0 RID: 1696 RVA: 0x0001D362 File Offset: 0x0001B562
	public bool isCharacterMale()
	{
		return this.party.getCurrentCharacter().isCharacterMale();
	}

	// Token: 0x060006A1 RID: 1697 RVA: 0x0001D374 File Offset: 0x0001B574
	public string getMapId()
	{
		return this.currentMap.getId();
	}

	// Token: 0x060006A2 RID: 1698 RVA: 0x0001D381 File Offset: 0x0001B581
	public bool isMapId(string mapId)
	{
		return this.currentMap.getId() == mapId;
	}

	// Token: 0x060006A3 RID: 1699 RVA: 0x0001D394 File Offset: 0x0001B594
	public void verbTrigger()
	{
		if (this.itemsOnGround() || this.getTargetTile().isContainer())
		{
			this.mountContainer();
		}
		if (!this.getTargetTile().isInteractive())
		{
			if (!this.isContainerMounted())
			{
				this.wait();
				return;
			}
		}
		else
		{
			this.getTargetTile().processVerbTrigger();
			this.setOverland(-1, -1, true);
		}
	}

	// Token: 0x060006A4 RID: 1700 RVA: 0x0001D3EE File Offset: 0x0001B5EE
	public void passRound()
	{
		this.setOverland(-1, -1, true);
		this.setDescription("You wait a short while.\n");
	}

	// Token: 0x060006A5 RID: 1701 RVA: 0x0001D406 File Offset: 0x0001B606
	public void wait()
	{
		this.passRound();
		this.addVocalBark("...");
	}

	// Token: 0x060006A6 RID: 1702 RVA: 0x0001D41A File Offset: 0x0001B61A
	public string pingSteam()
	{
		if (!SteamManager.Initialized)
		{
			return "ERROR: SteamManager not initialized";
		}
		return "SteamManager is Initialized!";
	}

	// Token: 0x060006A7 RID: 1703 RVA: 0x0001D42E File Offset: 0x0001B62E
	public void setAchievement(string achievementId)
	{
		this.achievementControl.setAchievement(achievementId);
	}

	// Token: 0x060006A8 RID: 1704 RVA: 0x0001D43C File Offset: 0x0001B63C
	public void setPropAnimation(string propId, string animationId)
	{
		foreach (SkaldWorldObject skaldWorldObject in GameData.getPropsByMap(this.currentMap.getId()))
		{
			Prop prop = (Prop)skaldWorldObject;
			if (prop.isId(propId))
			{
				prop.setDynamicAnimation(animationId);
			}
		}
	}

	// Token: 0x060006A9 RID: 1705 RVA: 0x0001D4A8 File Offset: 0x0001B6A8
	public string testAch()
	{
		this.setAchievement("ACH_RatKiller");
		this.setAchievement("ACH_ShoresOfIdra");
		return "-Testing Achievements-\n" + this.printLocalAchievements();
	}

	// Token: 0x060006AA RID: 1706 RVA: 0x0001D4D0 File Offset: 0x0001B6D0
	public string printLocalAchievements()
	{
		string text = this.achievementControl.printLocalAchievements();
		MainControl.log(text);
		return text;
	}

	// Token: 0x060006AB RID: 1707 RVA: 0x0001D4E3 File Offset: 0x0001B6E3
	public void lockAchievement(string achievmentId)
	{
		this.achievementControl.lockAchievement(achievmentId);
	}

	// Token: 0x060006AC RID: 1708 RVA: 0x0001D4F1 File Offset: 0x0001B6F1
	public bool areOpponentsNearBy()
	{
		return this.currentMap.areOpponentsNearBy();
	}

	// Token: 0x060006AD RID: 1709 RVA: 0x0001D4FE File Offset: 0x0001B6FE
	public bool isItNight()
	{
		return Calendar.isItNight();
	}

	// Token: 0x060006AE RID: 1710 RVA: 0x0001D505 File Offset: 0x0001B705
	public bool isItDay()
	{
		return Calendar.isItDay();
	}

	// Token: 0x060006AF RID: 1711 RVA: 0x0001D50C File Offset: 0x0001B70C
	public string mountNPC(string npcId)
	{
		if (npcId == "")
		{
			MainControl.logError("Attempting to mount blank NPC");
			return npcId;
		}
		Character character = GameData.instantiateCharacter(npcId);
		if (character != null)
		{
			this.mountNPCDirect(character);
		}
		return npcId;
	}

	// Token: 0x060006B0 RID: 1712 RVA: 0x0001D544 File Offset: 0x0001B744
	public string alertNearbyEnemies()
	{
		this.currentMap.alertNearbyEnemies();
		return "Nearby enemies alerted!";
	}

	// Token: 0x060006B1 RID: 1713 RVA: 0x0001D556 File Offset: 0x0001B756
	public string alertNearbyFriendlies()
	{
		this.currentMap.alertNearbyFriendlies();
		return "Nearby enemies alerted!";
	}

	// Token: 0x060006B2 RID: 1714 RVA: 0x0001D568 File Offset: 0x0001B768
	public string alertNearbyAll()
	{
		this.currentMap.alertNearbyAll();
		return "All nearby alerted!";
	}

	// Token: 0x060006B3 RID: 1715 RVA: 0x0001D57A File Offset: 0x0001B77A
	public void makeAllHostile()
	{
		this.currentMap.makeNearbyNPCsHostile();
	}

	// Token: 0x060006B4 RID: 1716 RVA: 0x0001D588 File Offset: 0x0001B788
	public void makeNPCHostile(string npcId)
	{
		foreach (SkaldWorldObject skaldWorldObject in GameData.getCharactersById(npcId))
		{
			((Character)skaldWorldObject).setHostile(true);
		}
	}

	// Token: 0x060006B5 RID: 1717 RVA: 0x0001D5E0 File Offset: 0x0001B7E0
	public void facePlayerAll()
	{
		this.currentMap.facePointAll(this.getCurrentPC().getTileX());
	}

	// Token: 0x060006B6 RID: 1718 RVA: 0x0001D5F8 File Offset: 0x0001B7F8
	public void spotAllNearby()
	{
		this.currentMap.spotAll();
	}

	// Token: 0x060006B7 RID: 1719 RVA: 0x0001D605 File Offset: 0x0001B805
	public void deployParty()
	{
		this.currentMap.deployParty();
	}

	// Token: 0x060006B8 RID: 1720 RVA: 0x0001D612 File Offset: 0x0001B812
	public void gatherParty()
	{
		if (this.currentMap == null)
		{
			return;
		}
		this.currentMap.gatherParty();
	}

	// Token: 0x060006B9 RID: 1721 RVA: 0x0001D628 File Offset: 0x0001B828
	public void mountNPCDirect(Character npc)
	{
		if (npc == null)
		{
			MainControl.logError("Attempting to mount blank NPC");
			return;
		}
		if (!this.currentMap.attemptToPlaceCharacterCloseToParty(npc))
		{
			return;
		}
		if (npc.isHostile())
		{
			this.launchCombat();
			return;
		}
		if (!this.isInteractPartyMounted())
		{
			Party party = new Party();
			party.add(npc);
			this.mountInteractParty(party);
		}
	}

	// Token: 0x060006BA RID: 1722 RVA: 0x0001D680 File Offset: 0x0001B880
	public void launchCombat()
	{
		if (!this.currentMap.canLaunchCombatHere())
		{
			this.setDescription("You can't enter combat now");
			return;
		}
		this.clearContainer();
		this.clearStore();
		this.clearScene();
		if (!this.isCombatActive())
		{
			this.clearMovePath();
			AudioControl.playStartOfCombatSound();
			this.curtain();
			this.currentMap.centerMapOnCombat();
			this.currentMap.centerMapOnCombat();
			this.combatEncounter = new CombatEncounter(this, this.currentMap);
			this.currentMap.setTargetTile(this.getCurrentTile());
			this.currentMap.revealNearbyNPCs();
			this.currentMap.clearNPCLists();
			this.currentMap.fireLaunchCombatTriggers();
		}
	}

	// Token: 0x060006BB RID: 1723 RVA: 0x0001D72C File Offset: 0x0001B92C
	private void mountParty(Party p)
	{
		foreach (Character npc in p.getPartyList())
		{
			this.mountNPCDirect(npc);
		}
	}

	// Token: 0x060006BC RID: 1724 RVA: 0x0001D780 File Offset: 0x0001B980
	public void dig()
	{
		this.getCurrentTile().dig();
	}

	// Token: 0x060006BD RID: 1725 RVA: 0x0001D78D File Offset: 0x0001B98D
	public void haltAndCatchFire()
	{
		MainControl.log("Crashing!");
		goto IL_0A;
		for (;;)
		{
			IL_0A:
			goto IL_0A;
		}
	}

	// Token: 0x060006BE RID: 1726 RVA: 0x0001D79C File Offset: 0x0001B99C
	public string mountNPCAt(string npcId, string x, string y)
	{
		if (npcId == "")
		{
			MainControl.logError("Attempting to mount blank NPC");
			return npcId;
		}
		Character character = GameData.instantiateCharacter(npcId);
		if (character != null)
		{
			this.currentMap.attemptToPlaceCharacterCloseToPoint(int.Parse(x), int.Parse(y), character, null);
		}
		return npcId;
	}

	// Token: 0x060006BF RID: 1727 RVA: 0x0001D7E8 File Offset: 0x0001B9E8
	public string removeAndPlaceCharacterById(string npcId)
	{
		Character character = this.party.removeCharacterById(npcId);
		if (character == null)
		{
			character = this.getSideBench().removeCharacter(npcId);
		}
		if (character == null)
		{
			return "Could not find character to remove: " + npcId;
		}
		if (character.isRecruitable())
		{
			character.getInventory().transferAllLootableItems(this.getInventory());
		}
		this.currentMap.attemptToPlaceCharacterCloseToParty(character);
		return "Removed " + npcId;
	}

	// Token: 0x060006C0 RID: 1728 RVA: 0x0001D854 File Offset: 0x0001BA54
	public string deleteCharacterById(string npcId)
	{
		Character character = this.party.removeCharacterById(npcId);
		if (character == null)
		{
			character = this.getSideBench().removeCharacter(npcId);
		}
		if (character == null)
		{
			return "Could not find character to remove: " + npcId;
		}
		if (character.isRecruitable())
		{
			character.getInventory().transferAllLootableItems(this.getInventory());
		}
		character.restoreAllAttributes();
		character.clearAllConditions();
		return "Removed " + npcId;
	}

	// Token: 0x060006C1 RID: 1729 RVA: 0x0001D8C0 File Offset: 0x0001BAC0
	public void sendCharacterToBench(string npcId)
	{
		Character character = this.getParty().getObject(npcId) as Character;
		if (character == null)
		{
			return;
		}
		if (character.isMainCharacter())
		{
			PopUpControl.addPopUpOK("The main character can never be removed from the Main Party.");
			return;
		}
		if (this.getParty().getCount() == 1)
		{
			PopUpControl.addPopUpOK("There must always be at least 1 character remaining in the Main Party.");
			return;
		}
		character = this.getParty().removeCharacter(character);
		if (character == null)
		{
			return;
		}
		this.getSideBench().add(character);
	}

	// Token: 0x060006C2 RID: 1730 RVA: 0x0001D930 File Offset: 0x0001BB30
	public void getCharacterFromBench(string npcId)
	{
		if (!this.getParty().canPlayerPartyFitMoreMembers())
		{
			MainControl.logError("Trying to add character to party but party is full!");
			PopUpControl.addPopUpOK("There cannot be more than 6 characters in the active party.\n\nThis character is sent to the camp.");
			return;
		}
		Character character = this.sideBench.removeCharacter(npcId);
		if (character == null)
		{
			return;
		}
		this.recruit(character, false);
	}

	// Token: 0x060006C3 RID: 1731 RVA: 0x0001D978 File Offset: 0x0001BB78
	public void clearCombat()
	{
		if (this.combatEncounter != null)
		{
			this.combatEncounter.clearCombatUpkeep();
		}
		this.combatEncounter = null;
		this.curtain();
		this.gatherParty();
		this.centerViewport();
	}

	// Token: 0x060006C4 RID: 1732 RVA: 0x0001D9A8 File Offset: 0x0001BBA8
	public string addPercentageXPToNextLevel(string percentage)
	{
		int percentage2 = 0;
		if (int.TryParse(percentage, out percentage2))
		{
			return "Added " + this.getCurrentPC().addPercentageXPToNextLevel(percentage2).ToString() + " XP";
		}
		return "Added 0 XP. Input faulty.";
	}

	// Token: 0x060006C5 RID: 1733 RVA: 0x0001D9EC File Offset: 0x0001BBEC
	public void addPercentageXPToNextLevelAll(string percentage)
	{
		int percentage2 = 0;
		if (int.TryParse(percentage, out percentage2))
		{
			this.party.addPercentageXPToNextLevelAll(percentage2);
		}
	}

	// Token: 0x060006C6 RID: 1734 RVA: 0x0001DA11 File Offset: 0x0001BC11
	public void winCombat()
	{
		if (this.combatEncounter != null)
		{
			this.combatEncounter.winCombat();
		}
	}

	// Token: 0x060006C7 RID: 1735 RVA: 0x0001DA26 File Offset: 0x0001BC26
	public void lose()
	{
		this.getParty().killAll();
	}

	// Token: 0x060006C8 RID: 1736 RVA: 0x0001DA33 File Offset: 0x0001BC33
	public void revealNearbyNPCs()
	{
		this.currentMap.revealNearbyNPCs();
	}

	// Token: 0x060006C9 RID: 1737 RVA: 0x0001DA40 File Offset: 0x0001BC40
	public void revealPropsById(string propId)
	{
		this.currentMap.revealPropsById(propId);
	}

	// Token: 0x060006CA RID: 1738 RVA: 0x0001DA4E File Offset: 0x0001BC4E
	public bool isCharacterNearby(string npcId)
	{
		return this.currentMap.isCharacterNearby(npcId);
	}

	// Token: 0x060006CB RID: 1739 RVA: 0x0001DA5C File Offset: 0x0001BC5C
	public string getMusicPath()
	{
		if (this.currentMap == null)
		{
			return "";
		}
		return this.currentMap.getMusicPath();
	}

	// Token: 0x060006CC RID: 1740 RVA: 0x0001DA77 File Offset: 0x0001BC77
	public bool isCombatActive()
	{
		return this.combatEncounter != null;
	}

	// Token: 0x060006CD RID: 1741 RVA: 0x0001DA82 File Offset: 0x0001BC82
	public void toggleRanged()
	{
		MainControl.log("Toggling ranged weapon!");
		this.getCurrentPC().toggleRangedWeapon();
	}

	// Token: 0x060006CE RID: 1742 RVA: 0x0001DA99 File Offset: 0x0001BC99
	public void toggleMelee()
	{
		MainControl.log("Toggling melee weapon!");
		this.getCurrentPC().toggleMeleeWeapon();
	}

	// Token: 0x060006CF RID: 1743 RVA: 0x0001DAB0 File Offset: 0x0001BCB0
	public void nextCombatState()
	{
		if (this.isCombatActive())
		{
			this.combatEncounter.gotoNextState();
		}
	}

	// Token: 0x060006D0 RID: 1744 RVA: 0x0001DAC8 File Offset: 0x0001BCC8
	public string createItemAtTile(string id)
	{
		if (id == "")
		{
			return "";
		}
		Item item = GameData.instantiateItem(id);
		this.getTerrainInventory().addItem(item);
		return item.getName();
	}

	// Token: 0x060006D1 RID: 1745 RVA: 0x0001DB04 File Offset: 0x0001BD04
	public SkaldActionResult useCurrentItem()
	{
		if (!this.isCombatActive())
		{
			return this.getCurrentPC().useCurrentItem();
		}
		if (this.combatEncounter.getCurrentCharacter() != null)
		{
			return this.combatEncounter.getCurrentCharacter().useCurrentItem();
		}
		return new SkaldActionResult(false, false, "No character active!", true);
	}

	// Token: 0x060006D2 RID: 1746 RVA: 0x0001DB50 File Offset: 0x0001BD50
	public void learnRecipe(string recipeId)
	{
		this.getCraftingControl().learnRecipe(recipeId);
	}

	// Token: 0x060006D3 RID: 1747 RVA: 0x0001DB60 File Offset: 0x0001BD60
	public string addMoney(string amount)
	{
		int num = int.Parse(amount);
		if (num > 0)
		{
			AudioControl.playCoinSound();
		}
		this.getInventory().addMoney(num);
		return amount;
	}

	// Token: 0x060006D4 RID: 1748 RVA: 0x0001DB8B File Offset: 0x0001BD8B
	public void addTutorialPopUp(string tutorialId)
	{
		PopUpControl.addTutorialPopUp(tutorialId);
	}

	// Token: 0x060006D5 RID: 1749 RVA: 0x0001DB93 File Offset: 0x0001BD93
	public void addHoverText(string header, string description)
	{
		HoverElementControl.addHoverText(header, description);
	}

	// Token: 0x060006D6 RID: 1750 RVA: 0x0001DB9C File Offset: 0x0001BD9C
	public void addHoverImage(string path)
	{
		HoverElementControl.addHoverImage(path);
	}

	// Token: 0x060006D7 RID: 1751 RVA: 0x0001DBA4 File Offset: 0x0001BDA4
	public bool isContainerMounted()
	{
		return this.containerInventory != null;
	}

	// Token: 0x060006D8 RID: 1752 RVA: 0x0001DBB4 File Offset: 0x0001BDB4
	public void mountContainer()
	{
		if (this.getTargetTile().getPropOrGuestProp() != null && this.getTargetTile().getPropOrGuestProp() is PropLockable && !(this.getTargetTile().getPropOrGuestProp() as PropLockable).readyToMount())
		{
			return;
		}
		PopUpControl.addPopUpLoot(this.getTargetTileInventory());
	}

	// Token: 0x060006D9 RID: 1753 RVA: 0x0001DC03 File Offset: 0x0001BE03
	public void gotoContainerState(Inventory inventory)
	{
		this.containerInventory = inventory;
	}

	// Token: 0x060006DA RID: 1754 RVA: 0x0001DC0C File Offset: 0x0001BE0C
	public Character getCurrentPC()
	{
		return this.party.getCurrentCharacter();
	}

	// Token: 0x060006DB RID: 1755 RVA: 0x0001DC1C File Offset: 0x0001BE1C
	public void addDevelopmentPoints(string points)
	{
		int i = int.Parse(points);
		this.getCurrentPC().addDevelopmentPoints(i);
	}

	// Token: 0x060006DC RID: 1756 RVA: 0x0001DC3C File Offset: 0x0001BE3C
	public QuestControl getQuestControl()
	{
		return this.questControl;
	}

	// Token: 0x060006DD RID: 1757 RVA: 0x0001DC44 File Offset: 0x0001BE44
	public string getSceneImagePath()
	{
		if (this.isInteractPartyMounted())
		{
			return this.getInteractParty().getCurrentImage();
		}
		if (this.isSceneMounted())
		{
			return this.processString(this.currentScene.getImagePath());
		}
		return "";
	}

	// Token: 0x060006DE RID: 1758 RVA: 0x0001DC79 File Offset: 0x0001BE79
	public string printRelationshipRanks()
	{
		string text = this.getParty().printRelationshipRanks();
		MainControl.log(text);
		return text;
	}

	// Token: 0x060006DF RID: 1759 RVA: 0x0001DC8C File Offset: 0x0001BE8C
	public void addToRelationship(string npcId, string value)
	{
		List<SkaldWorldObject> charactersById = GameData.getCharactersById(npcId);
		if (charactersById.Count == 0)
		{
			return;
		}
		int amount = 0;
		int.TryParse(value, out amount);
		foreach (SkaldWorldObject skaldWorldObject in charactersById)
		{
			Character character = (Character)skaldWorldObject;
			character.addToRelationshipRank(amount);
			MainControl.log(character.printRelationshipRank());
			if (!character.isUnique())
			{
				MainControl.logError("Setting relationship rank on non-unique character");
			}
		}
	}

	// Token: 0x060006E0 RID: 1760 RVA: 0x0001DD18 File Offset: 0x0001BF18
	public void clearCharacterAlert(string npcId)
	{
		List<SkaldWorldObject> charactersById = GameData.getCharactersById(npcId);
		if (charactersById.Count == 0)
		{
			return;
		}
		foreach (SkaldWorldObject skaldWorldObject in charactersById)
		{
			((Character)skaldWorldObject).clearAlert();
		}
	}

	// Token: 0x060006E1 RID: 1761 RVA: 0x0001DD78 File Offset: 0x0001BF78
	public bool isCharacterInLove(string npcId)
	{
		List<SkaldWorldObject> charactersById = GameData.getCharactersById(npcId);
		if (charactersById.Count == 0)
		{
			return false;
		}
		foreach (SkaldWorldObject skaldWorldObject in charactersById)
		{
			Character character = (Character)skaldWorldObject;
			if (!character.isUnique())
			{
				MainControl.logError("Reading relationship rank on non-unique character");
			}
			if (character.isInLove())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060006E2 RID: 1762 RVA: 0x0001DDF4 File Offset: 0x0001BFF4
	public string getNameOf(string npcId)
	{
		List<SkaldWorldObject> charactersById = GameData.getCharactersById(npcId);
		if (charactersById.Count == 0)
		{
			return npcId;
		}
		return charactersById[0].getName();
	}

	// Token: 0x060006E3 RID: 1763 RVA: 0x0001DE1E File Offset: 0x0001C01E
	public string getDescription()
	{
		if (this.description == "")
		{
			this.description = this.getBuffer();
		}
		return this.description;
	}

	// Token: 0x060006E4 RID: 1764 RVA: 0x0001DE44 File Offset: 0x0001C044
	public string setDescription(string description)
	{
		this.description = description;
		return description;
	}

	// Token: 0x060006E5 RID: 1765 RVA: 0x0001DE4E File Offset: 0x0001C04E
	public string appendDesc(string description)
	{
		this.description += description;
		return this.description;
	}

	// Token: 0x060006E6 RID: 1766 RVA: 0x0001DE68 File Offset: 0x0001C068
	public string getTerrainTitle()
	{
		return this.processString(this.getTargetTile().getName());
	}

	// Token: 0x060006E7 RID: 1767 RVA: 0x0001DE7B File Offset: 0x0001C07B
	public string getTerrainId()
	{
		return this.getTargetTile().getId();
	}

	// Token: 0x060006E8 RID: 1768 RVA: 0x0001DE88 File Offset: 0x0001C088
	public Inventory getTerrainInventory()
	{
		return this.getCurrentTile().getInventory();
	}

	// Token: 0x060006E9 RID: 1769 RVA: 0x0001DE98 File Offset: 0x0001C098
	public string getTerrainImage()
	{
		string text = this.processString(this.getCurrentTile().getImagePath());
		if (text == "")
		{
			text = "BlackScreen";
		}
		return text;
	}

	// Token: 0x060006EA RID: 1770 RVA: 0x0001DECB File Offset: 0x0001C0CB
	public string restoreAttunement()
	{
		return this.getCurrentPC().restoreAttunementFully();
	}

	// Token: 0x060006EB RID: 1771 RVA: 0x0001DED8 File Offset: 0x0001C0D8
	public string getTerrainDesc()
	{
		string text = this.getTargetTile().getDescription();
		if (this.itemsOnGround())
		{
			text = "Items in tile: \n" + this.getTerrainInventory().printCountList();
		}
		if (text == "")
		{
			if (this.getCurrentPC().isHidden())
			{
				text = C64Color.HEADER_TAG + "SNEAKING" + C64Color.HEADER_CLOSING_TAG;
				if (this.getCurrentPC().isBeingObserved())
				{
					text = text + "\n" + TextTools.formateNameValuePairSoft("Status:", "Unsafe");
				}
				else
				{
					text = text + "\n" + TextTools.formateNameValuePairSoft("Status:", "Safe");
				}
				text = text + "\n" + TextTools.formateNameValuePairSoft("Stealth:", this.getCurrentPC().getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_Stealth));
				text = text + "\n" + TextTools.formateNameValuePairSoft("Tile Mod:", this.currentMap.getCurrentTileStealthModifier());
			}
			else
			{
				text = this.getBuffer();
			}
		}
		return text;
	}

	// Token: 0x060006EC RID: 1772 RVA: 0x0001DFD7 File Offset: 0x0001C1D7
	public void setParty(Party _party)
	{
		this.party = _party;
	}

	// Token: 0x060006ED RID: 1773 RVA: 0x0001DFE0 File Offset: 0x0001C1E0
	public string moveMember()
	{
		this.party.moveMember();
		return "";
	}

	// Token: 0x060006EE RID: 1774 RVA: 0x0001DFF2 File Offset: 0x0001C1F2
	public bool itemsOnGround()
	{
		return this.getCurrentTile() != null && !this.getTerrainInventory().isEmpty() && this.getCurrentTile() == this.getTargetTile();
	}

	// Token: 0x060006EF RID: 1775 RVA: 0x0001E01C File Offset: 0x0001C21C
	public void getLootBuffer()
	{
		if (!this.isCombatActive())
		{
			return;
		}
		foreach (Inventory inventory in this.combatEncounter.getLootBufferContent())
		{
			this.dropInventory(inventory);
		}
		if (this.getTargetTileInventory() != null)
		{
			this.getTargetTileInventory().setName("Loot the corpses of your enemies!");
		}
		PopUpControl.addPopUpLoot(this.getTargetTileInventory());
	}

	// Token: 0x060006F0 RID: 1776 RVA: 0x0001E0A4 File Offset: 0x0001C2A4
	public void addItemToLootBuffer(Item item)
	{
		this.combatEncounter.addItemToLootBuffer(item);
	}

	// Token: 0x060006F1 RID: 1777 RVA: 0x0001E0B2 File Offset: 0x0001C2B2
	public Inventory getTargetTileInventory()
	{
		return this.getTargetTile().getInventory();
	}

	// Token: 0x060006F2 RID: 1778 RVA: 0x0001E0C0 File Offset: 0x0001C2C0
	public void getAllItemsInTargetTile()
	{
		if (this.getTargetTileInventory() == null || this.getTargetTileInventory().isEmpty())
		{
			this.setDescription("There is nothing here.");
			return;
		}
		this.setDescription("Picked up:\n" + this.getTargetTileInventory().printCountList());
		this.addPositiveBark(this.getTargetTileInventory().getPickedUpBark());
		this.getCurrentPC().getInventory().transferInventory(this.getTargetTileInventory());
		AudioControl.playDefaultInventorySound();
	}

	// Token: 0x060006F3 RID: 1779 RVA: 0x0001E138 File Offset: 0x0001C338
	public void getItemStackInTargetTile()
	{
		if (this.getTargetTileInventory() == null || this.getTargetTileInventory().isEmpty())
		{
			return;
		}
		this.setDescription("Picked up: " + this.getTargetTileInventory().getCurrentItemNameAndAmount());
		this.getCurrentPC().getInventory().addItem(this.getTargetTileInventory().removeCurrentItemStack());
		AudioControl.playDefaultInventorySound();
	}

	// Token: 0x060006F4 RID: 1780 RVA: 0x0001E198 File Offset: 0x0001C398
	public void getItemInTargetTile()
	{
		if (this.getTargetTileInventory() == null || this.getTargetTileInventory().isEmpty())
		{
			return;
		}
		this.setDescription("Picked up: " + this.getTargetTileInventory().getCurrentItemNameAndAmount());
		this.getCurrentPC().getInventory().addItem(this.getTargetTileInventory().removeCurrentItem());
		AudioControl.playDefaultInventorySound();
	}

	// Token: 0x060006F5 RID: 1781 RVA: 0x0001E1F8 File Offset: 0x0001C3F8
	public void getItemOnGround()
	{
		if (this.getTerrainInventory() == null || this.getTerrainInventory().isEmpty())
		{
			return;
		}
		this.setDescription("Picked up: " + this.getTerrainInventory().getCurrentItemNameAndAmount());
		this.getCurrentPC().getInventory().addItem(this.getTerrainInventory().removeCurrentItem());
		AudioControl.playDefaultInventorySound();
	}

	// Token: 0x060006F6 RID: 1782 RVA: 0x0001E258 File Offset: 0x0001C458
	public void getItemStackOnGround()
	{
		if (this.getTerrainInventory() == null || this.getTerrainInventory().isEmpty())
		{
			return;
		}
		this.setDescription("Picked up: " + this.getTerrainInventory().getCurrentItemNameAndAmount());
		this.getCurrentPC().getInventory().addItem(this.getTerrainInventory().removeCurrentItemStack());
		AudioControl.playDefaultInventorySound();
	}

	// Token: 0x060006F7 RID: 1783 RVA: 0x0001E2B8 File Offset: 0x0001C4B8
	public int getItemCount(string itemId)
	{
		return this.getParty().getInventory().getItemCount(itemId);
	}

	// Token: 0x060006F8 RID: 1784 RVA: 0x0001E2CC File Offset: 0x0001C4CC
	public bool itemCountGT(string itemId, string count)
	{
		bool result;
		try
		{
			int num = int.Parse(count);
			result = (this.getItemCount(itemId) > num);
		}
		catch
		{
			result = false;
		}
		return result;
	}

	// Token: 0x060006F9 RID: 1785 RVA: 0x0001E304 File Offset: 0x0001C504
	public bool itemCountLT(string itemId, string count)
	{
		bool result;
		try
		{
			int num = int.Parse(count);
			result = (this.getItemCount(itemId) < num);
		}
		catch
		{
			result = false;
		}
		return result;
	}

	// Token: 0x060006FA RID: 1786 RVA: 0x0001E33C File Offset: 0x0001C53C
	public string dropItem()
	{
		if (this.getInventory().isEmpty())
		{
			return "";
		}
		Item item = this.getInventory().removeCurrentItem();
		if (item == null)
		{
			return "";
		}
		string name = item.getName();
		if (this.isContainerMounted())
		{
			this.containerInventory.addItem(item);
			return name;
		}
		this.getTerrainInventory().addItem(item);
		return name;
	}

	// Token: 0x060006FB RID: 1787 RVA: 0x0001E39C File Offset: 0x0001C59C
	public string dropItemStack()
	{
		if (this.getInventory().isEmpty())
		{
			return "";
		}
		Item item = this.getInventory().removeCurrentItemStack();
		if (item == null)
		{
			return "";
		}
		string name = item.getName();
		if (this.isContainerMounted())
		{
			this.containerInventory.addItem(item);
			return name;
		}
		this.getTerrainInventory().addItem(item);
		return name;
	}

	// Token: 0x060006FC RID: 1788 RVA: 0x0001E3FC File Offset: 0x0001C5FC
	public void finalBossSummon()
	{
		if (!this.isCombatActive())
		{
			return;
		}
		if (this.getCombatEncounter().getOpponentParty().getLiveCharacters().Count > 5)
		{
			return;
		}
		List<SkaldWorldObject> propsByMap = GameData.getPropsByMap(this.currentMap.getId());
		List<MapTile> list = new List<MapTile>();
		foreach (SkaldWorldObject skaldWorldObject in propsByMap)
		{
			if (skaldWorldObject.isId("PRO_Beacon1"))
			{
				list.Add(skaldWorldObject.getMapTile());
			}
		}
		this.summonCreature(list, new List<string>
		{
			"CHA_SpawnFlyingPolyp",
			"CHA_SpawnFleshstrider",
			"CHA_SpawnNewborne",
			"CHA_SpawnNewborne"
		}, true);
	}

	// Token: 0x060006FD RID: 1789 RVA: 0x0001E4CC File Offset: 0x0001C6CC
	public void summonCreature(List<MapTile> tiles, List<string> creatureIDList, bool hostile)
	{
		if (creatureIDList == null || creatureIDList.Count == 0)
		{
			return;
		}
		if (tiles == null || tiles.Count == 0)
		{
			return;
		}
		List<MapTile> accessibleTilesFromParty = this.currentMap.getAccessibleTilesFromParty();
		foreach (MapTile mapTile in tiles)
		{
			if (accessibleTilesFromParty.Contains(mapTile) && this.isCombatActive())
			{
				this.combatEncounter.summonCreature(mapTile, creatureIDList[Random.Range(0, creatureIDList.Count)], hostile);
			}
		}
	}

	// Token: 0x060006FE RID: 1790 RVA: 0x0001E568 File Offset: 0x0001C768
	public string dropInventory(Inventory inventory)
	{
		string result = "";
		if (inventory.isEmpty())
		{
			return result;
		}
		result = "Items dropped:\n\n" + inventory.printList();
		if (this.isContainerMounted())
		{
			this.containerInventory.transferInventoryAndClearUser(inventory);
		}
		else
		{
			this.getTerrainInventory().transferInventoryAndClearUser(inventory);
		}
		return result;
	}

	// Token: 0x060006FF RID: 1791 RVA: 0x0001E5BC File Offset: 0x0001C7BC
	public string testXPCalc()
	{
		string text = "";
		text += this.getParty().printPowerLevel();
		if (this.isCombatActive())
		{
			text = text + "\n\n" + this.combatEncounter.getOpponentParty().printPowerLevel();
			text = text + "\n\n" + this.combatEncounter.getAllyParty().printPowerLevel();
			text = text + "\n\n" + this.getParty().calculateOpponentXPValue(this.combatEncounter.getOpponentParty(), this.combatEncounter.getAllyParty()).ToString();
		}
		else
		{
			text = text + "\n\n" + this.getParty().calculateOpponentXPValue(this.getParty(), null).ToString();
		}
		MainControl.log(text);
		return text;
	}

	// Token: 0x06000700 RID: 1792 RVA: 0x0001E685 File Offset: 0x0001C885
	public Scene getCurrentScene()
	{
		return this.currentScene;
	}

	// Token: 0x06000701 RID: 1793 RVA: 0x0001E68D File Offset: 0x0001C88D
	public string getName()
	{
		return this.getMainCharacter().getName();
	}

	// Token: 0x06000702 RID: 1794 RVA: 0x0001E69A File Offset: 0x0001C89A
	public void addFeat(string featId)
	{
		this.getCurrentPC().addFeat(featId);
	}

	// Token: 0x06000703 RID: 1795 RVA: 0x0001E6A8 File Offset: 0x0001C8A8
	public void addSpell(string spellId)
	{
		this.getCurrentPC().addSpell(spellId);
	}

	// Token: 0x06000704 RID: 1796 RVA: 0x0001E6B6 File Offset: 0x0001C8B6
	public int getMoney()
	{
		return this.getInventory().getMoney();
	}

	// Token: 0x06000705 RID: 1797 RVA: 0x0001E6C4 File Offset: 0x0001C8C4
	public bool isMoneyGT(string amount)
	{
		int num = 0;
		int.TryParse(amount, out num);
		return this.getInventory().getMoney() > num;
	}

	// Token: 0x06000706 RID: 1798 RVA: 0x0001E6EA File Offset: 0x0001C8EA
	public void restockStore()
	{
		if (this.isStoreMounted())
		{
			this.currentStore.stockStore();
		}
	}

	// Token: 0x06000707 RID: 1799 RVA: 0x0001E6FF File Offset: 0x0001C8FF
	public string mountStore(string storeId)
	{
		this.setStore(GameData.getStore(storeId, null));
		return storeId;
	}

	// Token: 0x06000708 RID: 1800 RVA: 0x0001E70F File Offset: 0x0001C90F
	private void setStore(Store store)
	{
		this.currentStore = store;
		if (this.currentStore != null)
		{
			this.currentStore.mountUpkeep(this.getParty());
		}
	}

	// Token: 0x06000709 RID: 1801 RVA: 0x0001E734 File Offset: 0x0001C934
	public void setCharacterAnimation(string npcId, string animationId)
	{
		foreach (SkaldWorldObject skaldWorldObject in GameData.getCharactersById(npcId))
		{
			((Character)skaldWorldObject).setDynamicAnimation(animationId);
		}
	}

	// Token: 0x0600070A RID: 1802 RVA: 0x0001E78C File Offset: 0x0001C98C
	public void makeCharacterHostileById(string npcId)
	{
		foreach (SkaldWorldObject skaldWorldObject in GameData.getCharactersById(npcId))
		{
			((Character)skaldWorldObject).setHostile(true);
		}
	}

	// Token: 0x0600070B RID: 1803 RVA: 0x0001E7E4 File Offset: 0x0001C9E4
	public void setPCAnimation(string animationId)
	{
		this.getMainCharacter().setDynamicAnimation(animationId);
	}

	// Token: 0x0600070C RID: 1804 RVA: 0x0001E7F2 File Offset: 0x0001C9F2
	public void clearPCAnimation()
	{
		this.getMainCharacter().clearDynamicAnimation();
	}

	// Token: 0x0600070D RID: 1805 RVA: 0x0001E7FF File Offset: 0x0001C9FF
	public Character getMainCharacter()
	{
		return this.party.getMainCharacter();
	}

	// Token: 0x0600070E RID: 1806 RVA: 0x0001E80C File Offset: 0x0001CA0C
	public string mountStoreFromCharacter()
	{
		this.clearScene();
		if (this.getInteractParty() != null)
		{
			this.setStore(this.getInteractParty().getCurrentCharacter().getStore());
		}
		else if (this.getTargetTile().getParty().getCurrentCharacter() != null)
		{
			this.setStore(this.getTargetTile().getParty().getCurrentCharacter().getStore());
		}
		return "Trying to mount store";
	}

	// Token: 0x0600070F RID: 1807 RVA: 0x0001E871 File Offset: 0x0001CA71
	public void clearStore()
	{
		if (this.currentStore != null)
		{
			this.currentStore.clearStoreUpkeep();
		}
		this.currentStore = null;
	}

	// Token: 0x06000710 RID: 1808 RVA: 0x0001E88D File Offset: 0x0001CA8D
	public Inventory getStoreInventory()
	{
		if (this.currentStore != null)
		{
			return this.currentStore.getInventory();
		}
		return null;
	}

	// Token: 0x06000711 RID: 1809 RVA: 0x0001E8A4 File Offset: 0x0001CAA4
	public bool isStoreMounted()
	{
		return this.currentStore != null;
	}

	// Token: 0x06000712 RID: 1810 RVA: 0x0001E8AF File Offset: 0x0001CAAF
	public string addAllLegalSpells()
	{
		return this.getCurrentPC().addAllLegalSpells();
	}

	// Token: 0x06000713 RID: 1811 RVA: 0x0001E8BC File Offset: 0x0001CABC
	public string printAllSpells()
	{
		List<AbilitySpell> allSpells = GameData.getAllSpells();
		List<string> allSpellSchools = GameData.getAllSpellSchools();
		string text = "---SPELLS (" + allSpells.Count.ToString() + ")---\n";
		foreach (string text2 in allSpellSchools)
		{
			List<AbilitySpell> list = new List<AbilitySpell>();
			List<AbilitySpell> list2 = new List<AbilitySpell>();
			List<AbilitySpell> list3 = new List<AbilitySpell>();
			List<AbilitySpell> list4 = new List<AbilitySpell>();
			int num = 0;
			foreach (AbilitySpell abilitySpell in allSpells)
			{
				if (abilitySpell.getSchoolList().Count == 0)
				{
					MainControl.logError("Spell is missing school: " + abilitySpell.getId());
				}
				else if (abilitySpell.getSchoolList().Contains(text2))
				{
					num++;
					if (abilitySpell.getTier() == 1)
					{
						list.Add(abilitySpell);
					}
					else if (abilitySpell.getTier() == 2)
					{
						list2.Add(abilitySpell);
					}
					else if (abilitySpell.getTier() == 3)
					{
						list3.Add(abilitySpell);
					}
					else if (abilitySpell.getTier() == 4)
					{
						list4.Add(abilitySpell);
					}
				}
			}
			text = string.Concat(new string[]
			{
				text,
				"\n\n",
				text2,
				" (",
				num.ToString(),
				")"
			});
			text = text + "  \n\n-Tier 1: " + list.Count.ToString() + "\n";
			foreach (AbilitySpell abilitySpell2 in list)
			{
				text = text + "    " + abilitySpell2.printDebugString() + "\n";
			}
			text = text + "  \n\n-Tier 2: " + list2.Count.ToString() + "\n";
			foreach (AbilitySpell abilitySpell3 in list2)
			{
				text = text + "    " + abilitySpell3.printDebugString() + "\n";
			}
			text = text + "  \n\n-Tier 3: " + list3.Count.ToString() + "\n";
			foreach (AbilitySpell abilitySpell4 in list3)
			{
				text = text + "    " + abilitySpell4.printDebugString() + "\n";
			}
			text = text + "  \n\n-Tier 4: " + list4.Count.ToString() + "\n";
			foreach (AbilitySpell abilitySpell5 in list4)
			{
				text = text + "    " + abilitySpell5.printDebugString() + "\n";
			}
		}
		text = TextTools.removeTrailingComma(text);
		return text;
	}

	// Token: 0x06000714 RID: 1812 RVA: 0x0001EC68 File Offset: 0x0001CE68
	public string addAllFeats()
	{
		this.getCurrentPC().addAllFeats();
		return "Added all available class Feats to " + this.getCurrentPC().getName();
	}

	// Token: 0x06000715 RID: 1813 RVA: 0x0001EC8C File Offset: 0x0001CE8C
	public void setSceneInput(int i)
	{
		if (i != -1)
		{
			try
			{
				Scene scene = this.currentScene;
				this.processString(scene.getExitTrigger(i));
				if (scene.isExitBranching(i))
				{
					if (scene.isExitBranchRandom(i))
					{
						PopUpControl.addSceneRandomTestPopUp(scene, i);
					}
					else
					{
						PopUpControl.addSceneStaticTestPopUp(scene, i);
					}
				}
				else
				{
					this.mountScene(scene.getSceneTargets(i));
				}
			}
			catch (Exception obj)
			{
				this.clearScene();
				MainControl.logError(obj);
			}
		}
	}

	// Token: 0x06000716 RID: 1814 RVA: 0x0001ED04 File Offset: 0x0001CF04
	public void makeBloody()
	{
		this.getCurrentPC().makeBloody();
	}

	// Token: 0x06000717 RID: 1815 RVA: 0x0001ED11 File Offset: 0x0001CF11
	public void makeBloodyAll()
	{
		this.getParty().makeBloodyAll();
	}

	// Token: 0x06000718 RID: 1816 RVA: 0x0001ED1E File Offset: 0x0001CF1E
	public void autoSave()
	{
		this.gameSave("Auto_Save");
	}

	// Token: 0x06000719 RID: 1817 RVA: 0x0001ED2B File Offset: 0x0001CF2B
	public void gameSave()
	{
		this.gameSave("Quick_Save");
	}

	// Token: 0x0600071A RID: 1818 RVA: 0x0001ED38 File Offset: 0x0001CF38
	public void gameSave(string fileName)
	{
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		DataControl.LocalSaveDataContainer localSaveDataContainer = new DataControl.LocalSaveDataContainer();
		localSaveDataContainer.currentMapId = this.currentMap.getId();
		localSaveDataContainer.variableSaveData = this.variableContainer.getSaveData();
		localSaveDataContainer.flagSaveData = this.flagContainer.getSaveData();
		localSaveDataContainer.technicalDataRecord = this.technicalDataRecord;
		localSaveDataContainer.party = this.getParty();
		localSaveDataContainer.sideBench = this.getSideBench();
		localSaveDataContainer.questControl = this.questControl;
		localSaveDataContainer.achievementControl = this.achievementControl;
		localSaveDataContainer.journal = this.journal;
		localSaveDataContainer.seconds = Calendar.getSecond();
		localSaveDataContainer.years = Calendar.getYear();
		localSaveDataContainer.factionList = FactionControl.getFactionList();
		localSaveDataContainer.eventManager = this.eventManager;
		localSaveDataContainer.instanceSaveData = GameData.getInstanceSaveData();
		localSaveDataContainer.addMapData(GameData.getMapList());
		localSaveDataContainer.sessionID = this.getSessionId();
		localSaveDataContainer.craftingControl = this.craftingControl;
		localSaveDataContainer.projectVersion = GameData.getVersionNumberAndEdition();
		localSaveDataContainer.modFileProjectFolder = SkaldModControl.getCurrentModProjectFolder();
		string text = "";
		text = text + TextTools.formateNameValuePair("Save Date", DateTime.Now.ToString("MM/dd/yy")) + "\n";
		text = text + TextTools.formateNameValuePair("Save Time", DateTime.Now.ToString("hh:mm:ss")) + "\n";
		text += "\n";
		text = text + TextTools.formateNameValuePair("Main Char.", this.getMainCharacter().printNameLevelClass()) + "\n";
		text = text + TextTools.formateNameValuePair("Map", this.currentMap.getName()) + "\n";
		text = text + TextTools.formateNameValuePair("Game Date", Calendar.printDayTime()) + "\n";
		text += "\n";
		text = text + TextTools.formateNameValuePair("Version", GameData.getVersionNumberAndEdition()) + "\n";
		text = text + TextTools.formateNameValuePair("Module", GameData.getCurrentModuleTitle()) + "\n";
		stopwatch.Stop();
		if (MainControl.debugFunctions)
		{
			MainControl.log("PACKING DATA FOR SAVE: " + stopwatch.ElapsedMilliseconds.ToString());
		}
		if (SkaldSaveControl.gameSave(fileName, localSaveDataContainer, text))
		{
			HoverElementControl.addHoverText("Game Saved", "");
			return;
		}
		HoverElementControl.addHoverText("Error:", "Failed to save game!");
	}

	// Token: 0x0600071B RID: 1819 RVA: 0x0001EF98 File Offset: 0x0001D198
	public void settingsSave()
	{
		try
		{
			SkaldSaveControl.settingsSave(new DataControl.SettingsSaveDataContainer
			{
				gamePlaySettings = GlobalSettings.getGamePlaySettings(),
				audioSettings = GlobalSettings.getAudioSettings(),
				videoSettings = GlobalSettings.getDisplaySettings(),
				fontSettings = GlobalSettings.getFontSettings(),
				difficultysettings = GlobalSettings.getDifficultySettings(),
				keyBindings = SkaldIO.keyBindings
			});
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
	}

	// Token: 0x0600071C RID: 1820 RVA: 0x0001F00C File Offset: 0x0001D20C
	public void settingsLoad()
	{
		try
		{
			DataControl.SettingsSaveDataContainer settingsSaveDataContainer = (DataControl.SettingsSaveDataContainer)SkaldSaveControl.settingsLoad();
			if (settingsSaveDataContainer == null)
			{
				MainControl.logError("Could not load settings!");
			}
			else
			{
				GlobalSettings.setAudioSettings(settingsSaveDataContainer.audioSettings);
				GlobalSettings.setDisplaySettings(settingsSaveDataContainer.videoSettings);
				GlobalSettings.setGamePlaySettings(settingsSaveDataContainer.gamePlaySettings);
				GlobalSettings.setFontSettings(settingsSaveDataContainer.fontSettings);
				GlobalSettings.setDifficultySettings(settingsSaveDataContainer.difficultysettings);
				SkaldIO.keyBindings = settingsSaveDataContainer.keyBindings;
			}
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
	}

	// Token: 0x0600071D RID: 1821 RVA: 0x0001F090 File Offset: 0x0001D290
	public void gameLoad()
	{
		this.gameLoad("Quick_Save");
	}

	// Token: 0x0600071E RID: 1822 RVA: 0x0001F0A0 File Offset: 0x0001D2A0
	public bool loadLast()
	{
		string lastSavedFile = SkaldSaveControl.getLastSavedFile();
		Debug.Log(lastSavedFile);
		return this.gameLoad(lastSavedFile);
	}

	// Token: 0x0600071F RID: 1823 RVA: 0x0001F0C0 File Offset: 0x0001D2C0
	public void loadAutoSave()
	{
		this.gameLoad("Auto_Save");
	}

	// Token: 0x06000720 RID: 1824 RVA: 0x0001F0D0 File Offset: 0x0001D2D0
	public bool gameLoad(string fileName)
	{
		DataControl.LocalSaveDataContainer localSaveDataContainer = (DataControl.LocalSaveDataContainer)SkaldSaveControl.gameLoad(fileName);
		if (localSaveDataContainer == null)
		{
			MainControl.logError("Could not load: " + fileName);
			this.setDescription("Could not load: " + fileName);
			return false;
		}
		QuestControl questControl = null;
		AchievementControl achievementControl = null;
		Journal journal = null;
		GameVariableContainer.GameVariableSaveData data = null;
		SkaldFlagContainer.FlagsSaveData data2 = null;
		SkaldTechnicalDataRecord skaldTechnicalDataRecord = null;
		Party party = null;
		SideBench sideBench = null;
		MasterEventManager masterEventManager = null;
		GameData.SkaldInstanceControl.InstanceSaveData data3 = null;
		CraftingControl craftingControl = null;
		int years = 0;
		ulong seconds = 0UL;
		string text = "";
		try
		{
			sideBench = localSaveDataContainer.sideBench;
		}
		catch (Exception ex)
		{
			sideBench = new SideBench();
			string str = "Side Bench may be corrupt!\n\n";
			Exception ex2 = ex;
			MainControl.logError(str + ((ex2 != null) ? ex2.ToString() : null));
			this.setDescription("Game loaded but an outdated save may cause errors!");
			return false;
		}
		try
		{
			craftingControl = localSaveDataContainer.craftingControl;
		}
		catch (Exception ex3)
		{
			craftingControl = new CraftingControl();
			string str2 = "Crafting Control may be corrupt!\n\n";
			Exception ex4 = ex3;
			MainControl.logError(str2 + ((ex4 != null) ? ex4.ToString() : null));
			this.setDescription("Game loaded but an outdated save may cause errors!");
			return false;
		}
		try
		{
			data3 = localSaveDataContainer.instanceSaveData;
		}
		catch (Exception ex5)
		{
			string str3 = "CRITICAL SAVE Instance Control is corrupt!\n\n";
			Exception ex6 = ex5;
			MainControl.logError(str3 + ((ex6 != null) ? ex6.ToString() : null));
			this.setDescription("Game not loaded: Savefile was corrupt!");
			return false;
		}
		try
		{
			party = localSaveDataContainer.party;
		}
		catch (Exception ex7)
		{
			string str4 = "CRITICAL SAVE Party is corrupt!\n\n";
			Exception ex8 = ex7;
			MainControl.logError(str4 + ((ex8 != null) ? ex8.ToString() : null));
			this.setDescription("Game not loaded: Savefile was corrupt!");
			return false;
		}
		this.gameStarted = true;
		try
		{
			FactionControl.setFactionList(localSaveDataContainer.factionList);
		}
		catch (Exception ex9)
		{
			string str5 = "Faction Control may be corrupt!\n\n";
			Exception ex10 = ex9;
			MainControl.logError(str5 + ((ex10 != null) ? ex10.ToString() : null));
			this.setDescription("Game loaded but an outdated save may cause errors!");
		}
		try
		{
			questControl = localSaveDataContainer.questControl;
		}
		catch (Exception ex11)
		{
			questControl = new QuestControl();
			string str6 = "Quest Control may be corrupt!\n\n";
			Exception ex12 = ex11;
			MainControl.logError(str6 + ((ex12 != null) ? ex12.ToString() : null));
			this.setDescription("Game loaded but an outdated save may cause errors!");
		}
		try
		{
			achievementControl = localSaveDataContainer.achievementControl;
		}
		catch (Exception ex13)
		{
			achievementControl = new AchievementControl();
			string str7 = "Acheivement Control may be corrupt!\n\n";
			Exception ex14 = ex13;
			MainControl.logError(str7 + ((ex14 != null) ? ex14.ToString() : null));
			this.setDescription("Game loaded but an outdated save may cause errors!");
		}
		try
		{
			skaldTechnicalDataRecord = localSaveDataContainer.technicalDataRecord;
		}
		catch (Exception ex15)
		{
			skaldTechnicalDataRecord = new SkaldTechnicalDataRecord();
			string str8 = "Skald Technical Data Record may be corrupt!\n\n";
			Exception ex16 = ex15;
			MainControl.logError(str8 + ((ex16 != null) ? ex16.ToString() : null));
			this.setDescription("Game loaded but an outdated save may cause errors!");
		}
		try
		{
			journal = localSaveDataContainer.journal;
		}
		catch (Exception ex17)
		{
			journal = new Journal();
			string str9 = "Journal may be corrupt!\n\n";
			Exception ex18 = ex17;
			MainControl.logError(str9 + ((ex18 != null) ? ex18.ToString() : null));
			this.setDescription("Game loaded but an outdated save may cause errors!");
		}
		try
		{
			data = localSaveDataContainer.variableSaveData;
		}
		catch (Exception ex19)
		{
			data = this.variableContainer.getSaveData();
			string str10 = "Game Variable Container may be corrupt!\n\n";
			Exception ex20 = ex19;
			MainControl.logError(str10 + ((ex20 != null) ? ex20.ToString() : null));
			this.setDescription("Game loaded but an outdated save may cause errors!");
		}
		try
		{
			data2 = localSaveDataContainer.flagSaveData;
		}
		catch (Exception ex21)
		{
			data2 = this.flagContainer.getSaveData();
			string str11 = "Flag Container may be corrupt!\n\n";
			Exception ex22 = ex21;
			MainControl.logError(str11 + ((ex22 != null) ? ex22.ToString() : null));
			this.setDescription("Game loaded but an outdated save may cause errors!");
		}
		try
		{
			masterEventManager = localSaveDataContainer.eventManager;
		}
		catch (Exception ex23)
		{
			masterEventManager = new MasterEventManager();
			string str12 = "Event manager is corrupt!\n\n";
			Exception ex24 = ex23;
			MainControl.logError(str12 + ((ex24 != null) ? ex24.ToString() : null));
			this.setDescription("Game loaded but an outdated save may cause errors!");
		}
		try
		{
			years = localSaveDataContainer.years;
		}
		catch (Exception ex25)
		{
			years = Calendar.getYear();
			string str13 = "Year field is corrupt!\n\n";
			Exception ex26 = ex25;
			MainControl.logError(str13 + ((ex26 != null) ? ex26.ToString() : null));
			this.setDescription("Game loaded but an outdated save may cause errors!");
		}
		try
		{
			seconds = localSaveDataContainer.seconds;
		}
		catch (Exception ex27)
		{
			seconds = Calendar.getSecond();
			string str14 = "Seconds field is corrupt!\n\n";
			Exception ex28 = ex27;
			MainControl.logError(str14 + ((ex28 != null) ? ex28.ToString() : null));
			this.setDescription("Game loaded but an outdated save may cause errors!");
		}
		try
		{
			text = localSaveDataContainer.modFileProjectFolder;
		}
		catch (Exception ex29)
		{
			string str15 = "Mod project name missing!\n\n";
			Exception ex30 = ex29;
			MainControl.logError(str15 + ((ex30 != null) ? ex30.ToString() : null));
		}
		if (!SkaldModControl.isFolderNameTheCurrentFolderName(text))
		{
			SkaldModControl.setCurrentModProjectFolder(text);
			GameData.loadDataAndMaps();
		}
		GameData.applyInstanceSaveData(data3);
		this.questControl = questControl;
		this.achievementControl = achievementControl;
		this.craftingControl = craftingControl;
		this.variableContainer.applySaveData(data);
		this.flagContainer.applySaveData(data2);
		this.technicalDataRecord = skaldTechnicalDataRecord;
		this.party = party;
		this.sideBench = sideBench;
		this.party.getInventory().activateNewAdditionTagging();
		this.journal = journal;
		this.eventManager = masterEventManager;
		Calendar.setCalendar(years, seconds);
		this.setSessionId(localSaveDataContainer.sessionID);
		this.clearCombat();
		this.clearContainer();
		this.clearInteractParty();
		this.clearScene();
		this.clearStore();
		this.clearWorkbench();
		this.clearRandomEncounter();
		this.clearCamp();
		localSaveDataContainer.loadMaps(GameData.getMapList());
		this.mountMap(localSaveDataContainer.currentMapId, false);
		this.moveOverland(0, 0, true);
		this.setDescription("Game Loaded!");
		return true;
	}

	// Token: 0x0400014A RID: 330
	private GameVariableContainer variableContainer;

	// Token: 0x0400014B RID: 331
	private SkaldFlagContainer flagContainer;

	// Token: 0x0400014C RID: 332
	private Party party;

	// Token: 0x0400014D RID: 333
	private QuestControl questControl;

	// Token: 0x0400014E RID: 334
	private AchievementControl achievementControl;

	// Token: 0x0400014F RID: 335
	private Journal journal;

	// Token: 0x04000150 RID: 336
	private Scene currentScene;

	// Token: 0x04000151 RID: 337
	private EncounterControl.Encounter randomEncounter;

	// Token: 0x04000152 RID: 338
	private MasterEventManager eventManager;

	// Token: 0x04000153 RID: 339
	private CraftingControl craftingControl;

	// Token: 0x04000154 RID: 340
	private string sceneSource = "";

	// Token: 0x04000155 RID: 341
	public bool newSceneMounted;

	// Token: 0x04000156 RID: 342
	private Inventory containerInventory;

	// Token: 0x04000157 RID: 343
	public Store currentStore;

	// Token: 0x04000158 RID: 344
	private PropWorkBench workbench;

	// Token: 0x04000159 RID: 345
	public Map currentMap;

	// Token: 0x0400015A RID: 346
	public CombatEncounter combatEncounter;

	// Token: 0x0400015B RID: 347
	private SkaldTechnicalDataRecord technicalDataRecord;

	// Token: 0x0400015C RID: 348
	private string description;

	// Token: 0x0400015D RID: 349
	private bool gameStarted;

	// Token: 0x0400015E RID: 350
	private CampActivityState.CampingOrder campingOrder;

	// Token: 0x0400015F RID: 351
	private string sessionID = "";

	// Token: 0x04000160 RID: 352
	private SideBench sideBench;

	// Token: 0x04000161 RID: 353
	private CharacterBuilderBaseState.CharacterCreatorUseCase characterCreatorUseCase;

	// Token: 0x020001E4 RID: 484
	[Serializable]
	public class LocalSaveDataContainer : SkaldSaveControl.SaveDataContainer
	{
		// Token: 0x06001734 RID: 5940 RVA: 0x0006733B File Offset: 0x0006553B
		public LocalSaveDataContainer()
		{
			this.mapList = new Dictionary<string, Map.MapSaveData>();
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x00067370 File Offset: 0x00065570
		public void addMapData(List<Map> maps)
		{
			foreach (Map map in maps)
			{
				this.mapList.Add(map.getId(), map.getSaveData());
			}
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x000673D0 File Offset: 0x000655D0
		public void loadMaps(List<Map> maps)
		{
			foreach (Map map in maps)
			{
				string id = map.getId();
				if (this.mapList.ContainsKey(id))
				{
					map.loadSaveData(this.mapList[id]);
				}
			}
		}

		// Token: 0x04000778 RID: 1912
		public GameVariableContainer.GameVariableSaveData variableSaveData;

		// Token: 0x04000779 RID: 1913
		private Dictionary<string, Map.MapSaveData> mapList;

		// Token: 0x0400077A RID: 1914
		public Party party;

		// Token: 0x0400077B RID: 1915
		public QuestControl questControl;

		// Token: 0x0400077C RID: 1916
		public AchievementControl achievementControl;

		// Token: 0x0400077D RID: 1917
		public Journal journal;

		// Token: 0x0400077E RID: 1918
		public CraftingControl craftingControl;

		// Token: 0x0400077F RID: 1919
		public GameData.SkaldInstanceControl.InstanceSaveData instanceSaveData;

		// Token: 0x04000780 RID: 1920
		public SideBench sideBench;

		// Token: 0x04000781 RID: 1921
		public ulong seconds;

		// Token: 0x04000782 RID: 1922
		public int years;

		// Token: 0x04000783 RID: 1923
		public SkaldObjectList factionList;

		// Token: 0x04000784 RID: 1924
		public SkaldFlagContainer.FlagsSaveData flagSaveData;

		// Token: 0x04000785 RID: 1925
		public SkaldTechnicalDataRecord technicalDataRecord;

		// Token: 0x04000786 RID: 1926
		public MasterEventManager eventManager;

		// Token: 0x04000787 RID: 1927
		public string currentMapId;

		// Token: 0x04000788 RID: 1928
		public string sessionID = "";

		// Token: 0x04000789 RID: 1929
		public string projectVersion = "";

		// Token: 0x0400078A RID: 1930
		public string modFileProjectFolder = "";
	}

	// Token: 0x020001E5 RID: 485
	[Serializable]
	private class SettingsSaveDataContainer : SkaldSaveControl.SaveDataContainer
	{
		// Token: 0x0400078B RID: 1931
		public SKALDKeyBindings keyBindings;

		// Token: 0x0400078C RID: 1932
		public GlobalSettings.GamePlaySettings gamePlaySettings;

		// Token: 0x0400078D RID: 1933
		public GlobalSettings.FontSettings fontSettings;

		// Token: 0x0400078E RID: 1934
		public GlobalSettings.AudioSettings audioSettings;

		// Token: 0x0400078F RID: 1935
		public GlobalSettings.DisplaySettings videoSettings;

		// Token: 0x04000790 RID: 1936
		public GlobalSettings.DifficultySettings difficultysettings;
	}
}
