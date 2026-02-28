using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

// Token: 0x02000030 RID: 48
public class CombatEncounter
{
	// Token: 0x060004EA RID: 1258 RVA: 0x00017250 File Offset: 0x00015450
	public CombatEncounter(DataControl dc, Map _combatMap)
	{
		CombatLog.clearLog();
		this.combatMap = _combatMap;
		this.combatMap.revealNearbyNPCs();
		this.combatMap.fetchOpponentsJustOutsideCombat();
		this.dataControl = dc;
		this.playerParty = this.combatMap.playerParty;
		this.dataControl.deployParty();
		this.opponentParty = new Party();
		this.allyParty = new Party();
		this.playerParty.startOfCombatUpkeep(this.opponentParty);
		foreach (Party party in this.combatMap.nearByEnemies)
		{
			foreach (SkaldBaseObject skaldBaseObject in party.getObjectList())
			{
				Character character = (Character)skaldBaseObject;
				this.opponentParty.addWithoutSettingPosition(character);
			}
		}
		this.opponentParty.startOfCombatUpkeep(this.playerParty);
		foreach (Party party2 in this.combatMap.nearByFriendlies)
		{
			foreach (SkaldBaseObject skaldBaseObject2 in party2.getObjectList())
			{
				Character character2 = (Character)skaldBaseObject2;
				if (character2.canAttack())
				{
					this.allyParty.addWithoutSettingPosition(character2);
				}
			}
		}
		this.allyParty.startOfCombatUpkeep(this.opponentParty);
		this.combatMap.clearNPCLists();
		this.initiativeList = new InitiativeList(this.playerParty, this.opponentParty, this.allyParty);
		this.updatePositionalInfoAll();
		this.logRound();
	}

	// Token: 0x060004EB RID: 1259 RVA: 0x00017480 File Offset: 0x00015680
	internal bool shouldIDrawUI()
	{
		return (this.isStateIntro() || this.isStatePlanningPlayer()) && !this.getCurrentCharacter().hasUpcomingAttack();
	}

	// Token: 0x060004EC RID: 1260 RVA: 0x000174A2 File Offset: 0x000156A2
	public Party getPlayerParty()
	{
		return this.playerParty;
	}

	// Token: 0x060004ED RID: 1261 RVA: 0x000174AA File Offset: 0x000156AA
	public Party getOpponentParty()
	{
		return this.opponentParty;
	}

	// Token: 0x060004EE RID: 1262 RVA: 0x000174B2 File Offset: 0x000156B2
	public Party getAllyParty()
	{
		return this.allyParty;
	}

	// Token: 0x060004EF RID: 1263 RVA: 0x000174BA File Offset: 0x000156BA
	public void useCombatManeuver()
	{
		this.getCurrentCharacter().useAbility();
		this.gotoNextState();
	}

	// Token: 0x060004F0 RID: 1264 RVA: 0x000174CD File Offset: 0x000156CD
	public void castSpell()
	{
		this.getCurrentCharacter().castSpell();
		this.gotoNextState();
	}

	// Token: 0x060004F1 RID: 1265 RVA: 0x000174E4 File Offset: 0x000156E4
	public void holdCurrentCharacterAction()
	{
		if (this.getCurrentCharacter() == null)
		{
			return;
		}
		SkaldActionResult skaldActionResult = this.initiativeList.holdCurrentCharactersAction();
		if (!skaldActionResult.wasSuccess())
		{
			PopUpControl.addPopUpOK(skaldActionResult.getResultString());
		}
		this.stateControl.setResult();
		this.clearDescription();
		this.addToDesc(skaldActionResult.getResultString());
	}

	// Token: 0x060004F2 RID: 1266 RVA: 0x00017538 File Offset: 0x00015738
	public void updateMousePosition(Vector2 point)
	{
		MapTile mouseTile = this.combatMap.getMouseTile();
		this.combatMap.setMouseTile(point);
		this.combatMap.setClosestTileToMouseTile(this.getCurrentCharacter());
		if (mouseTile != this.combatMap.getMouseTile() && this.getCurrentCharacter() != null && this.getCurrentCharacter().isPC() && this.getCurrentCharacter().canCharacterCombatMove() && !this.getCurrentCharacter().moveAlongCombatPath && !this.getCurrentCharacter().isPanicked() && this.canMouseMove() && this.combatMap.getMouseTile() != null)
		{
			this.combatMap.findCombatPath(this.getCurrentCharacter().getTileParty(), this.combatMap.getMouseTile().getTileX(), this.combatMap.getMouseTile().getTileY());
		}
	}

	// Token: 0x060004F3 RID: 1267 RVA: 0x00017604 File Offset: 0x00015804
	public void placeCharacterAtMouseTile(Character character)
	{
		MapTile mapTile = character.getMapTile();
		MapTile mouseTile = this.combatMap.getMouseTile();
		if (mouseTile == null)
		{
			return;
		}
		if (mouseTile.getParty() != null)
		{
			if (mouseTile.getParty().isPC())
			{
				this.dataControl.getParty().setCurrentPC(mouseTile.getParty().getCurrentCharacter());
			}
			return;
		}
		if (!this.combatMap.getPreCombatPlacementTiles().Contains(mouseTile))
		{
			return;
		}
		mapTile.clearParty();
		mouseTile.addCharacter(character);
		character.snapToGrid();
	}

	// Token: 0x060004F4 RID: 1268 RVA: 0x00017680 File Offset: 0x00015880
	public void cycleTargetForPlayerCharacter()
	{
		if (this.getCurrentCharacter() == null || !this.isCurrentCharacterPlayer())
		{
			return;
		}
		Character nextLiveCharacterButDontSet = this.opponentParty.getNextLiveCharacterButDontSet(this.getCurrentCharacter().getTargetOpponent());
		this.getCurrentCharacter().setTargetOpponent(nextLiveCharacterButDontSet);
	}

	// Token: 0x060004F5 RID: 1269 RVA: 0x000176C4 File Offset: 0x000158C4
	public void placeCharacterAtAdjacentTile(Character character, int xOffset, int yOffset)
	{
		MapTile mapTile = character.getMapTile();
		MapTile tile = this.combatMap.getTile(character.getTileX() + xOffset, character.getTileY() + yOffset);
		Character character2 = null;
		if (tile == null)
		{
			return;
		}
		if (!this.combatMap.getPreCombatPlacementTiles().Contains(tile))
		{
			return;
		}
		if (tile.getParty() != null)
		{
			if (!tile.getParty().isPC())
			{
				return;
			}
			character2 = tile.getParty().getCurrentCharacter();
			tile.clearParty();
		}
		mapTile.clearParty();
		tile.addCharacter(character);
		character.snapToGrid();
		if (character2 != null)
		{
			mapTile.addCharacter(character2);
			character2.snapToGrid();
		}
	}

	// Token: 0x060004F6 RID: 1270 RVA: 0x0001775C File Offset: 0x0001595C
	public void summonCreature(MapTile targetTile, string creatureId, bool hostile)
	{
		if (creatureId == "")
		{
			return;
		}
		if (targetTile == null)
		{
			return;
		}
		if (!targetTile.isTileOpenAndPassable() || !this.combatMap.isTilePassableInCombat(targetTile) || !targetTile.isSpotted())
		{
			return;
		}
		Character character = GameData.instantiateCharacter(creatureId);
		if (character == null)
		{
			return;
		}
		targetTile.addCharacter(character);
		if (hostile)
		{
			this.opponentParty.addWithoutSettingPosition(character);
		}
		else
		{
			this.allyParty.addWithoutSettingPosition(character);
		}
		character.setHostile(hostile);
		character.setSummoned();
		this.initiativeList.addIntoInitiativeList(character);
		this.updatePositionalInfoAll();
	}

	// Token: 0x060004F7 RID: 1271 RVA: 0x000177E9 File Offset: 0x000159E9
	public MapTile getTileFromMouse(Vector2 point)
	{
		return this.combatMap.getTileAtRelativeLocalPos(point);
	}

	// Token: 0x060004F8 RID: 1272 RVA: 0x000177F7 File Offset: 0x000159F7
	public bool canMouseMove()
	{
		return this.isStatePlanningPlayer() && this.combatMap.getNextMoveTile() != null;
	}

	// Token: 0x060004F9 RID: 1273 RVA: 0x00017811 File Offset: 0x00015A11
	public bool isStatePlanningPlayer()
	{
		return this.stateControl.isStatePlanningPlayer();
	}

	// Token: 0x060004FA RID: 1274 RVA: 0x00017820 File Offset: 0x00015A20
	public void moveToMouse()
	{
		if (!this.isStatePlanningPlayer())
		{
			return;
		}
		if (this.getCurrentCharacter() == null || !this.getCurrentCharacter().canCharacterCombatMove() || !this.canMouseMove())
		{
			return;
		}
		MapTile mouseTile = this.combatMap.getMouseTile();
		if (mouseTile == null)
		{
			return;
		}
		int tileX = mouseTile.getTileX();
		int tileY = mouseTile.getTileY();
		if (mouseTile.getLiveCharacter() != null && this.getCurrentCharacter().isNPCHostile(mouseTile.getCharacter()))
		{
			if (this.getCurrentCharacter().getTargetOpponent() != mouseTile.getCharacter())
			{
				this.getCurrentCharacter().setTargetOpponent(mouseTile.getCharacter());
				if (!this.getCurrentCharacter().isTargetInRange())
				{
					this.combatMap.findCombatPath(this.getCurrentCharacter().getTileParty(), tileX, tileY);
				}
				return;
			}
			if (this.getCurrentCharacter().getTargetOpponent() == mouseTile.getCharacter() && this.getCurrentCharacter().isWeaponRanged() && this.getCurrentCharacter().isTargetInRange())
			{
				this.gotoNextState();
				return;
			}
		}
		if (this.getCurrentCharacter().moveAlongCombatPath)
		{
			this.getCurrentCharacter().moveAlongCombatPath = false;
		}
		else if (this.getCurrentCharacter().navigationCourseHasNodes() && this.getCurrentCharacter().GetNavigationCourse().getDestination().X == tileX && this.getCurrentCharacter().GetNavigationCourse().getDestination().Y == tileY)
		{
			this.getCurrentCharacter().moveAlongCombatPath = true;
			this.moveCurrentCharacterAlongPath();
		}
		else
		{
			this.combatMap.findCombatPath(this.getCurrentCharacter().getTileParty(), tileX, tileY);
			Character targetOpponent = this.getCurrentCharacter().getTargetOpponent();
			Character character = mouseTile.getCharacter();
			if (targetOpponent != null && character != null && character == targetOpponent)
			{
				this.getCurrentCharacter().moveAlongCombatPath = true;
				this.moveCurrentCharacterAlongPath();
			}
		}
		this.getCurrentCharacter().setWaitInCombat();
		if (!this.isStateResult())
		{
			this.gotoNextState();
		}
	}

	// Token: 0x060004FB RID: 1275 RVA: 0x000179EC File Offset: 0x00015BEC
	public void moveCurrentCharacterAlongPath()
	{
		if (this.getCurrentCharacter().navigationCourseHasNodes() && this.getCurrentCharacter().hasRemainingCombatMovesOrAttacks())
		{
			Point point = this.getCurrentCharacter().popNavigationNode();
			Point point2 = new Point(point.X - this.getCurrentCharacter().getTileParty().getTileX(), point.Y - this.getCurrentCharacter().getTileParty().getTileY());
			this.moveCharacter(point2.X, point2.Y);
		}
		else
		{
			this.getCurrentCharacter().moveAlongCombatPath = false;
			this.getCurrentCharacter().setWaitInCombat();
		}
		this.gotoNextState();
	}

	// Token: 0x060004FC RID: 1276 RVA: 0x00017A8C File Offset: 0x00015C8C
	private bool setNextCharacter()
	{
		this.initiativeList.sortInitiativeList();
		Character currentCharacter = this.getCurrentCharacter();
		this.initiativeList.setNextCharacter();
		if (currentCharacter != null && this.getCurrentCharacter() != currentCharacter)
		{
			currentCharacter.endOfTurnUpkeep();
		}
		if (this.getCurrentCharacter() == null)
		{
			return false;
		}
		this.updateHidden(this.getCurrentCharacter());
		this.selectTargetsForCurrentCharacter();
		return true;
	}

	// Token: 0x060004FD RID: 1277 RVA: 0x00017AE5 File Offset: 0x00015CE5
	public bool isCurrentCharacterPlayer()
	{
		return this.getCurrentCharacter() != null && this.getCurrentCharacter().isPC();
	}

	// Token: 0x060004FE RID: 1278 RVA: 0x00017B01 File Offset: 0x00015D01
	public bool lootBufferHasLoot()
	{
		return this.lootbuffer.hasLoot();
	}

	// Token: 0x060004FF RID: 1279 RVA: 0x00017B0E File Offset: 0x00015D0E
	public List<Inventory> getLootBufferContent()
	{
		return this.lootbuffer.getLootBufferContent();
	}

	// Token: 0x06000500 RID: 1280 RVA: 0x00017B1B File Offset: 0x00015D1B
	public void addItemToLootBuffer(Item item)
	{
		this.lootbuffer.addItemToInventory(item);
	}

	// Token: 0x06000501 RID: 1281 RVA: 0x00017B29 File Offset: 0x00015D29
	public bool isCurrentCharacterReadyForResults()
	{
		return this.getCurrentCharacter() != null && this.getCurrentCharacter().physicMovementComplete();
	}

	// Token: 0x06000502 RID: 1282 RVA: 0x00017B44 File Offset: 0x00015D44
	public void gotoNextState()
	{
		if (this.isStateIntro())
		{
			this.startOfCombatUpkeep();
		}
		else if (this.isStatePlanningPlayer())
		{
			if (!this.resolveAction().wasPerformed())
			{
				this.stateControl.setFailedAction();
			}
			else if (!this.getCurrentCharacter().hasRemainingCombatMovesOrAttacks() || this.getCurrentCharacter().hasUpcomingAttack())
			{
				this.stateControl.setResolving();
			}
		}
		else if (this.isStateFailedAction())
		{
			if (!this.getCurrentCharacter().hasRemainingCombatMovesOrAttacks())
			{
				this.stateControl.setResult();
			}
			else if (this.getCurrentCharacter().isPC())
			{
				this.stateControl.setPlanningPlayer();
			}
			else
			{
				MainControl.logError("NPC failed a combat action!");
				this.stateControl.setResult();
			}
		}
		else if (this.isStateResult())
		{
			this.forceNextCharacter();
		}
		else if (this.isStateResolving())
		{
			this.stateControl.setResult();
		}
		this.updatePositionalInfoAll();
	}

	// Token: 0x06000503 RID: 1283 RVA: 0x00017C2F File Offset: 0x00015E2F
	public bool isStateResolving()
	{
		return this.stateControl.isStateResolving();
	}

	// Token: 0x06000504 RID: 1284 RVA: 0x00017C3C File Offset: 0x00015E3C
	public void forceNextCharacter()
	{
		if (!this.setNextCharacter())
		{
			this.endOfRoundUpkeep();
		}
		this.resolveActionAndSetState();
	}

	// Token: 0x06000505 RID: 1285 RVA: 0x00017C52 File Offset: 0x00015E52
	public bool isStateFailedAction()
	{
		return this.stateControl.isStateFailedAction();
	}

	// Token: 0x06000506 RID: 1286 RVA: 0x00017C5F File Offset: 0x00015E5F
	public bool isStateAContinueState()
	{
		return this.isStateResult() || this.isStateRound() || this.isStateFailedAction() || this.isStateIntro();
	}

	// Token: 0x06000507 RID: 1287 RVA: 0x00017C81 File Offset: 0x00015E81
	public bool shouldIAutoResolve()
	{
		return this.autoResolve || (this.getCurrentCharacter() != null && (this.getCurrentCharacter().shouldYouAutoResolveMyActions() || (this.getCurrentCharacter().isPC() && this.isStateResult())));
	}

	// Token: 0x06000508 RID: 1288 RVA: 0x00017CBE File Offset: 0x00015EBE
	public bool isStateResult()
	{
		return this.stateControl.isStateResult();
	}

	// Token: 0x06000509 RID: 1289 RVA: 0x00017CCB File Offset: 0x00015ECB
	private void endOfRoundUpkeep()
	{
		this.turns += 1;
		this.startOfRoundUpkeep();
		this.logRound();
	}

	// Token: 0x0600050A RID: 1290 RVA: 0x00017CE8 File Offset: 0x00015EE8
	private void logRound()
	{
		CombatLog.addHeader("ROUND " + this.turns.ToString(), "Round " + this.turns.ToString() + "\n\n" + this.initiativeList.printInitiativeOrderWithRoll());
	}

	// Token: 0x0600050B RID: 1291 RVA: 0x00017D34 File Offset: 0x00015F34
	private void startOfCombatUpkeep()
	{
		this.startOfRoundUpkeep();
		this.resolveActionAndSetState();
	}

	// Token: 0x0600050C RID: 1292 RVA: 0x00017D42 File Offset: 0x00015F42
	private void startOfRoundUpkeep()
	{
		this.clearDescription();
		this.initiativeList.rollInitiative();
		this.makeCharactersReady();
		this.addToDesc(this.printInitiativeOrder());
		this.setNextCharacter();
	}

	// Token: 0x0600050D RID: 1293 RVA: 0x00017D6E File Offset: 0x00015F6E
	private void resolveActionAndSetState()
	{
		if (this.isCurrentCharacterPlayer())
		{
			this.stateControl.setPlanningPlayer();
			return;
		}
		this.resolveAction();
		this.stateControl.setResolving();
	}

	// Token: 0x0600050E RID: 1294 RVA: 0x00017D96 File Offset: 0x00015F96
	public void defend()
	{
		if (this.getCurrentCharacter() != null)
		{
			this.getCurrentCharacter().defend();
		}
		this.gotoNextState();
	}

	// Token: 0x0600050F RID: 1295 RVA: 0x00017DB1 File Offset: 0x00015FB1
	public string printInitiativeOrder()
	{
		return this.initiativeList.printInitiativeOrder();
	}

	// Token: 0x06000510 RID: 1296 RVA: 0x00017DBE File Offset: 0x00015FBE
	private Character getCurrentPC()
	{
		return this.playerParty.getCurrentCharacter();
	}

	// Token: 0x06000511 RID: 1297 RVA: 0x00017DCB File Offset: 0x00015FCB
	public string getTitle()
	{
		if (this.isStateVictory())
		{
			return "Victory";
		}
		if (this.isStateIntro())
		{
			return "Combat!";
		}
		return "Round " + this.turns.ToString();
	}

	// Token: 0x06000512 RID: 1298 RVA: 0x00017DFE File Offset: 0x00015FFE
	private bool isStateVictory()
	{
		return this.stateControl.isStateVictory();
	}

	// Token: 0x06000513 RID: 1299 RVA: 0x00017E0B File Offset: 0x0001600B
	private void makeCharactersReady()
	{
		this.playerParty.startOfRoundUpkeep();
		this.opponentParty.startOfRoundUpkeep();
		this.allyParty.startOfRoundUpkeep();
	}

	// Token: 0x06000514 RID: 1300 RVA: 0x00017E30 File Offset: 0x00016030
	private void selectTargetsForCurrentCharacter()
	{
		if (this.getCurrentCharacter() == null)
		{
			return;
		}
		if (this.playerParty.getPartyList().Contains(this.getCurrentCharacter()))
		{
			this.selectTarget(this.getCurrentCharacter(), new Party[]
			{
				this.opponentParty
			});
			return;
		}
		if (this.allyParty.getPartyList().Contains(this.getCurrentCharacter()))
		{
			this.selectTarget(this.getCurrentCharacter(), new Party[]
			{
				this.opponentParty
			});
			return;
		}
		if (this.opponentParty.getPartyList().Contains(this.getCurrentCharacter()))
		{
			this.selectTarget(this.getCurrentCharacter(), new Party[]
			{
				this.playerParty,
				this.allyParty
			});
		}
	}

	// Token: 0x06000515 RID: 1301 RVA: 0x00017EEC File Offset: 0x000160EC
	private void selectTarget(Character c, Party[] opponents)
	{
		c.turnToTarget();
		if (c.hasValidTarget())
		{
			return;
		}
		if (c.isDead())
		{
			return;
		}
		List<Character> list = new List<Character>();
		foreach (Party party in opponents)
		{
			list.AddRange(party.getLiveCharacters());
		}
		if (list.Count == 0)
		{
			return;
		}
		Character character = this.combatMap.getBestCombatTarget(c, list);
		if (character == null)
		{
			character = list[Random.Range(0, list.Count)];
		}
		if (character.isHidden())
		{
			character = null;
		}
		c.setTargetOpponent(character);
		if (c.getTargetOpponent() == null && !c.isPC())
		{
			c.defend();
			return;
		}
		c.turnToTarget();
	}

	// Token: 0x06000516 RID: 1302 RVA: 0x00017F94 File Offset: 0x00016194
	public string getDescription()
	{
		if (this.isPartyVictorious())
		{
			return "Victory!";
		}
		if (this.isBattlefieldEmpty())
		{
			return "There are no enemies. Press ENTER to leave combat!";
		}
		if (this.autoResolve)
		{
			return "Auto-resolving round!\n\nPress any key to stop!";
		}
		if (this.isStateIntro())
		{
			return this.getIntroDescription();
		}
		if (this.isStateRound())
		{
			return "The combatants stare at each other from across the battlefield...";
		}
		if (this.isStatePlanningPlayer())
		{
			return this.getCurrentCharacter().getCombatDescription();
		}
		return this.combatDescription;
	}

	// Token: 0x06000517 RID: 1303 RVA: 0x00018002 File Offset: 0x00016202
	public int getXpBuffer()
	{
		return this.xpBuffer;
	}

	// Token: 0x06000518 RID: 1304 RVA: 0x0001800A File Offset: 0x0001620A
	public bool isStateRound()
	{
		return this.stateControl.isStateRound();
	}

	// Token: 0x06000519 RID: 1305 RVA: 0x00018017 File Offset: 0x00016217
	public bool isStateIntro()
	{
		return this.stateControl.isStateIntro();
	}

	// Token: 0x0600051A RID: 1306 RVA: 0x00018024 File Offset: 0x00016224
	public void winCombat()
	{
		this.opponentParty.killAll();
	}

	// Token: 0x0600051B RID: 1307 RVA: 0x00018031 File Offset: 0x00016231
	private string getIntroDescription()
	{
		return "The party has been attacked! Prepare for combat!\n\nREMEMBER: You can press RETURN on your turn to auto-resolve combat!";
	}

	// Token: 0x0600051C RID: 1308 RVA: 0x00018038 File Offset: 0x00016238
	private void addToDesc(string s)
	{
		this.combatDescription = this.combatDescription + s + "\n\n";
	}

	// Token: 0x0600051D RID: 1309 RVA: 0x00018051 File Offset: 0x00016251
	private void clearDescription()
	{
		this.combatDescription = "";
	}

	// Token: 0x0600051E RID: 1310 RVA: 0x00018060 File Offset: 0x00016260
	private SkaldActionResult resolveAction()
	{
		this.clearDescription();
		SkaldActionResult skaldActionResult;
		if (this.isCurrentCharacterPlayer() && !this.autoResolve && !this.getCurrentCharacter().shouldYouAutoResolveMyActions())
		{
			skaldActionResult = this.getCurrentCharacter().resolveAction();
		}
		else
		{
			if (this.getCurrentCharacter().shouldIMove())
			{
				this.moveNPC();
			}
			this.getCurrentCharacter().toggleOptimalWeapon();
			skaldActionResult = this.getCurrentCharacter().resolveActionAutomated();
		}
		if (skaldActionResult != null)
		{
			this.addToDesc(skaldActionResult.getResultString());
			CombatLog.addEntry(this.getCurrentCharacter().getName(), skaldActionResult);
			this.updatePositionalInfoAll();
			this.combatMap.updateDrawLogic();
			if (this.getCurrentCharacter().hasRemainingCombatMovesOrAttacks())
			{
				this.selectTargetsForCurrentCharacter();
			}
			return skaldActionResult;
		}
		return new SkaldActionResult(false, false, "", true);
	}

	// Token: 0x0600051F RID: 1311 RVA: 0x0001811D File Offset: 0x0001631D
	public bool moveNPCIfNecessary()
	{
		if (this.isCurrentCharacterPlayer())
		{
			return false;
		}
		if (this.getCurrentCharacter().shouldIMove())
		{
			this.resolveAction();
			return true;
		}
		return false;
	}

	// Token: 0x06000520 RID: 1312 RVA: 0x00018140 File Offset: 0x00016340
	private void updatePositionalInfoAll()
	{
		foreach (Character c in this.playerParty.getLiveCharacters())
		{
			this.updatePositionalInfo(c);
		}
		foreach (Character c2 in this.allyParty.getLiveCharacters())
		{
			this.updatePositionalInfo(c2);
		}
		foreach (Character c3 in this.opponentParty.getLiveCharacters())
		{
			this.updatePositionalInfo(c3);
		}
	}

	// Token: 0x06000521 RID: 1313 RVA: 0x00018228 File Offset: 0x00016428
	private void updatePositionalInfo(Character c)
	{
		this.combatMap.getFlanking(c);
		this.combatMap.getNearbyAllyCount(c);
		this.combatMap.getInMelee(c);
	}

	// Token: 0x06000522 RID: 1314 RVA: 0x00018250 File Offset: 0x00016450
	private void moveNPC()
	{
		if (this.getCurrentCharacter() == null)
		{
			return;
		}
		if (this.getCurrentCharacter().isPanicked())
		{
			if (this.getCurrentCharacter().isInMelee())
			{
				List<MapTile> tilesToFleeTo = this.combatMap.getTilesToFleeTo(this.getCurrentCharacter());
				if (tilesToFleeTo.Count == 0)
				{
					this.getCurrentCharacter().defend();
					return;
				}
				MapTile mapTile = tilesToFleeTo[Random.Range(0, tilesToFleeTo.Count)];
				this.getPathAndMove(mapTile.getTileX(), mapTile.getTileY());
				return;
			}
		}
		else
		{
			Character targetOpponent = this.getCurrentCharacter().getTargetOpponent();
			if (targetOpponent == null)
			{
				this.getCurrentCharacter().defend();
				return;
			}
			this.getPathAndMove(targetOpponent.getTileX(), targetOpponent.getTileY());
		}
	}

	// Token: 0x06000523 RID: 1315 RVA: 0x000182FC File Offset: 0x000164FC
	private void getPathAndMove(int x, int y)
	{
		Point pathToTarget = this.getPathToTarget(this.getCurrentCharacter().getTileParty(), x, y);
		if (pathToTarget.X != 0 || pathToTarget.Y != 0)
		{
			this.moveCharacter(pathToTarget.X, pathToTarget.Y);
			return;
		}
		this.getCurrentCharacter().defend();
	}

	// Token: 0x06000524 RID: 1316 RVA: 0x00018350 File Offset: 0x00016550
	private Point getPathToTarget(Party party, int targetX, int targetY)
	{
		if (!party.navigationCourseHasNodes())
		{
			this.combatMap.findCombatPath(party, targetX, targetY);
		}
		Point result;
		if (party.navigationCourseHasNodes())
		{
			Point point = this.getCurrentCharacter().getTileParty().popNavigationNode();
			result = new Point(point.X - this.getCurrentCharacter().getTileParty().getTileX(), point.Y - this.getCurrentCharacter().getTileParty().getTileY());
		}
		else
		{
			result = this.combatMap.stepTowardsTarget(this.getCurrentCharacter().getTileX(), this.getCurrentCharacter().getTileY(), targetX, targetY);
		}
		return result;
	}

	// Token: 0x06000525 RID: 1317 RVA: 0x000183EA File Offset: 0x000165EA
	public Character getCurrentCharacter()
	{
		return this.initiativeList.getCurrentCharacter();
	}

	// Token: 0x06000526 RID: 1318 RVA: 0x000183F7 File Offset: 0x000165F7
	public UIInitiativeList getUIInitiativeList()
	{
		if (this.initiativeUIList == null)
		{
			this.initiativeUIList = new UIInitiativeList();
		}
		this.initiativeUIList.setButtons(this.initiativeList.getInitiativeList(), this.getCurrentCharacter());
		return this.initiativeUIList;
	}

	// Token: 0x06000527 RID: 1319 RVA: 0x0001842E File Offset: 0x0001662E
	public bool isBattlefieldEmpty()
	{
		return this.opponentParty.isEmpty();
	}

	// Token: 0x06000528 RID: 1320 RVA: 0x0001843B File Offset: 0x0001663B
	public bool isPartyVictorious()
	{
		return this.opponentParty.isPartyDead();
	}

	// Token: 0x06000529 RID: 1321 RVA: 0x00018450 File Offset: 0x00016650
	public void loot()
	{
		this.calculateXP();
		foreach (Character character in this.opponentParty.getPartyList())
		{
			MapTile tile = this.combatMap.getTile(character.getTileX(), character.getTileY());
			character.dropLoot(tile.getInventory());
			this.lootbuffer.addLootTile(tile);
		}
	}

	// Token: 0x0600052A RID: 1322 RVA: 0x000184D8 File Offset: 0x000166D8
	public void clearCombatUpkeep()
	{
		this.playerParty.postCombatUpkeep();
		this.allyParty.postCombatUpkeep();
		this.opponentParty.postCombatUpkeep();
		if (this.opponentParty.isPartyDead())
		{
			this.playerParty.resolveVictoryTriggers();
			this.allyParty.resolveVictoryTriggers();
		}
		if (this.playerParty.isPartyDead())
		{
			this.opponentParty.resolveVictoryTriggers();
		}
	}

	// Token: 0x0600052B RID: 1323 RVA: 0x00018544 File Offset: 0x00016744
	public bool isPartyDefeated()
	{
		return this.partyDefeated;
	}

	// Token: 0x0600052C RID: 1324 RVA: 0x0001854C File Offset: 0x0001674C
	private void calculateXP()
	{
		if (this.xpBuffer == 0)
		{
			this.xpBuffer = this.playerParty.getXPFromOpponentParty(this.opponentParty, this.allyParty);
		}
	}

	// Token: 0x0600052D RID: 1325 RVA: 0x00018574 File Offset: 0x00016774
	private void updateHidden(Character character)
	{
		if (character == null)
		{
			return;
		}
		if (!character.isHidden())
		{
			return;
		}
		if (!character.isHostile())
		{
			character.attemptToSneak(this.opponentParty.getPartyList());
			return;
		}
		character.attemptToSneak(this.playerParty.getPartyList());
		character.attemptToSneak(this.allyParty.getPartyList());
	}

	// Token: 0x0600052E RID: 1326 RVA: 0x000185CC File Offset: 0x000167CC
	public void moveCharacter(int x, int y)
	{
		Character currentCharacter = this.getCurrentCharacter();
		if (currentCharacter == null)
		{
			return;
		}
		if (!currentCharacter.canCharacterCombatMove())
		{
			return;
		}
		int tileX = currentCharacter.getTileX();
		int tileY = currentCharacter.getTileY();
		int x2 = tileX + x;
		int y2 = tileY + y;
		currentCharacter.turnToPoint(x2, y2);
		if (!this.combatMap.isTilePassableInCombat(x2, y2))
		{
			if (!this.isCurrentCharacterPlayer() || this.autoResolve)
			{
				currentCharacter.combatMove();
				return;
			}
			currentCharacter.setWaitInCombat();
			return;
		}
		else
		{
			MapTile tile = this.combatMap.getTile(x2, y2);
			MapTile tile2 = this.combatMap.getTile(tileX, tileY);
			if (!tile.isTileOpen())
			{
				Character character = tile.getCharacter();
				if (character != null)
				{
					if (currentCharacter.getTargetOpponent() == character)
					{
						return;
					}
					if (!character.isDead() && currentCharacter.isNPCHostile(character))
					{
						currentCharacter.setTargetOpponent(character);
						currentCharacter.setWaitInCombat();
						return;
					}
					if (!currentCharacter.isNPCHostile(character))
					{
						tile2.clearParty();
						tile.clearParty();
						tile.addCharacter(currentCharacter);
						tile.playMoveSound();
						tile2.addCharacter(character);
						currentCharacter.combatSwap();
						return;
					}
				}
				else
				{
					currentCharacter.setWaitInCombat();
				}
				return;
			}
			tile2.clearParty();
			tile.addCharacter(this.getCurrentCharacter());
			tile.playMoveSound();
			currentCharacter.combatMove();
			this.updateHidden(currentCharacter);
			return;
		}
	}

	// Token: 0x0600052F RID: 1327 RVA: 0x00018704 File Offset: 0x00016904
	public SkaldActionResult pushTarget()
	{
		Character currentCharacter = this.getCurrentCharacter();
		if (currentCharacter == null)
		{
			return new SkaldActionResult(false, false, "No active character!", true);
		}
		Character targetOpponent = currentCharacter.getTargetOpponent();
		if (targetOpponent == null)
		{
			return new SkaldActionResult(false, false, this.getCurrentCharacter().getName() + " does not have a target", true);
		}
		int x;
		int y;
		if (currentCharacter.getTileX() == targetOpponent.getTileX())
		{
			x = currentCharacter.getTileX();
			if (currentCharacter.getTileY() == targetOpponent.getTileY() - 1)
			{
				y = targetOpponent.getTileY() + 1;
			}
			else
			{
				if (currentCharacter.getTileY() != targetOpponent.getTileY() + 1)
				{
					return new SkaldActionResult(false, false, "Target is too far away!", true);
				}
				y = targetOpponent.getTileY() - 1;
			}
		}
		else
		{
			if (currentCharacter.getTileY() != targetOpponent.getTileY())
			{
				return new SkaldActionResult(false, false, "Target is too far away!", true);
			}
			y = currentCharacter.getTileY();
			if (currentCharacter.getTileX() == targetOpponent.getTileX() - 1)
			{
				x = targetOpponent.getTileX() + 1;
			}
			else
			{
				if (currentCharacter.getTileX() != targetOpponent.getTileX() + 1)
				{
					return new SkaldActionResult(false, false, "Target is too far away!", true);
				}
				x = targetOpponent.getTileX() - 1;
			}
		}
		MapTile tile = this.combatMap.getTile(x, y);
		if (tile == null || !this.combatMap.isTilePassableInCombat(tile) || !tile.isTileOpen())
		{
			return new SkaldActionResult(false, false, "There is no room to push target!", true);
		}
		MapTile tile2 = this.combatMap.getTile(this.getCurrentCharacter().getTileX(), this.getCurrentCharacter().getTileY());
		MapTile tile3 = this.combatMap.getTile(targetOpponent.getTileX(), targetOpponent.getTileY());
		tile3.clearParty();
		tile2.clearParty();
		if (tile != null)
		{
			tile.addCharacter(targetOpponent);
		}
		tile3.addCharacter(currentCharacter);
		targetOpponent.getVisualEffects().setShaken();
		AudioControl.playSound("Bump1");
		return new SkaldActionResult(true, true, this.getCurrentCharacter().getName() + " pushed target", true);
	}

	// Token: 0x06000530 RID: 1328 RVA: 0x000188DC File Offset: 0x00016ADC
	public SkaldActionResult overrunTarget()
	{
		Character currentCharacter = this.getCurrentCharacter();
		if (currentCharacter == null)
		{
			return new SkaldActionResult(false, false, "No active character!", true);
		}
		Character targetOpponent = currentCharacter.getTargetOpponent();
		if (targetOpponent == null)
		{
			return new SkaldActionResult(false, false, this.getCurrentCharacter().getName() + " does not have a target", true);
		}
		if (!targetOpponent.isDead())
		{
			return new SkaldActionResult(false, false, targetOpponent.getName() + " is not dead!", true);
		}
		if (currentCharacter.getTileX() == targetOpponent.getTileX())
		{
			currentCharacter.getTileX();
			if (currentCharacter.getTileY() == targetOpponent.getTileY() - 1)
			{
				targetOpponent.getTileY();
			}
			else
			{
				if (currentCharacter.getTileY() != targetOpponent.getTileY() + 1)
				{
					return new SkaldActionResult(false, false, "Target is too far away!", true);
				}
				targetOpponent.getTileY();
			}
		}
		else
		{
			if (currentCharacter.getTileY() != targetOpponent.getTileY())
			{
				return new SkaldActionResult(false, false, "Target is too far away!", true);
			}
			currentCharacter.getTileY();
			if (currentCharacter.getTileX() == targetOpponent.getTileX() - 1)
			{
				targetOpponent.getTileX();
			}
			else
			{
				if (currentCharacter.getTileX() != targetOpponent.getTileX() + 1)
				{
					return new SkaldActionResult(false, false, "Target is too far away!", true);
				}
				targetOpponent.getTileX();
			}
		}
		MapTile tile = this.combatMap.getTile(this.getCurrentCharacter().getTileX(), this.getCurrentCharacter().getTileY());
		MapTile tile2 = this.combatMap.getTile(targetOpponent.getTileX(), targetOpponent.getTileY());
		tile2.updateDeadPartyStatus();
		tile.clearParty();
		tile2.addCharacter(currentCharacter);
		targetOpponent.getVisualEffects().setShaken();
		AudioControl.playSound("Bump1");
		return new SkaldActionResult(true, true, this.getCurrentCharacter().getName() + " pushed target", true);
	}

	// Token: 0x04000133 RID: 307
	private Party playerParty;

	// Token: 0x04000134 RID: 308
	private Party allyParty;

	// Token: 0x04000135 RID: 309
	private Party opponentParty = new Party();

	// Token: 0x04000136 RID: 310
	private int xpBuffer;

	// Token: 0x04000137 RID: 311
	private DataControl dataControl;

	// Token: 0x04000138 RID: 312
	public bool chooseTarget = true;

	// Token: 0x04000139 RID: 313
	private string combatDescription = "";

	// Token: 0x0400013A RID: 314
	private short turns = 1;

	// Token: 0x0400013B RID: 315
	private bool partyDefeated;

	// Token: 0x0400013C RID: 316
	public bool autoResolve;

	// Token: 0x0400013D RID: 317
	private InitiativeList initiativeList;

	// Token: 0x0400013E RID: 318
	private Map combatMap;

	// Token: 0x0400013F RID: 319
	private CombatEncounter.LootBuffer lootbuffer = new CombatEncounter.LootBuffer();

	// Token: 0x04000140 RID: 320
	private CombatEncounter.StateControl stateControl = new CombatEncounter.StateControl();

	// Token: 0x04000141 RID: 321
	private UIInitiativeList initiativeUIList;

	// Token: 0x020001DD RID: 477
	private class LootBuffer
	{
		// Token: 0x0600170D RID: 5901 RVA: 0x00066D27 File Offset: 0x00064F27
		public void addLootTile(MapTile tile)
		{
			if (tile.getInventory() == null || tile.getInventory().isEmpty())
			{
				return;
			}
			if (!this.lootTileList.Contains(tile))
			{
				this.lootTileList.Add(tile);
			}
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x00066D59 File Offset: 0x00064F59
		public void addItemToInventory(Item item)
		{
			this.bufferInventory.addItem(item);
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x00066D68 File Offset: 0x00064F68
		public bool hasLoot()
		{
			return this.lootTileList.Count > 0 || !this.bufferInventory.isEmpty();
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x00066D88 File Offset: 0x00064F88
		public List<Inventory> getLootBufferContent()
		{
			List<Inventory> list = new List<Inventory>
			{
				this.bufferInventory
			};
			foreach (MapTile mapTile in this.lootTileList)
			{
				list.Add(mapTile.getInventory());
			}
			this.lootTileList.Clear();
			return list;
		}

		// Token: 0x04000768 RID: 1896
		private List<MapTile> lootTileList = new List<MapTile>();

		// Token: 0x04000769 RID: 1897
		private Inventory bufferInventory = new Inventory();
	}

	// Token: 0x020001DE RID: 478
	private class StateControl
	{
		// Token: 0x06001712 RID: 5906 RVA: 0x00066E1E File Offset: 0x0006501E
		internal bool isStatePlanningPlayer()
		{
			return this.currentState == CombatEncounter.StateControl.states.PlanningPlayer;
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x00066E29 File Offset: 0x00065029
		internal void setPlanningPlayer()
		{
			this.currentState = CombatEncounter.StateControl.states.PlanningPlayer;
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x00066E32 File Offset: 0x00065032
		internal bool isStateRound()
		{
			return this.currentState == CombatEncounter.StateControl.states.Round;
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x00066E3D File Offset: 0x0006503D
		internal bool isStateIntro()
		{
			return this.currentState == CombatEncounter.StateControl.states.Intro;
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x00066E48 File Offset: 0x00065048
		internal bool isStateVictory()
		{
			return this.currentState == CombatEncounter.StateControl.states.Victory;
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x00066E53 File Offset: 0x00065053
		internal void setResult()
		{
			this.currentState = CombatEncounter.StateControl.states.Result;
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x00066E5C File Offset: 0x0006505C
		internal bool isStateResult()
		{
			return this.currentState == CombatEncounter.StateControl.states.Result;
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x00066E67 File Offset: 0x00065067
		internal bool isStateFailedAction()
		{
			return this.currentState == CombatEncounter.StateControl.states.FailedAction;
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x00066E72 File Offset: 0x00065072
		internal void setFailedAction()
		{
			this.currentState = CombatEncounter.StateControl.states.FailedAction;
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x00066E7B File Offset: 0x0006507B
		internal bool isStateResolving()
		{
			return this.currentState == CombatEncounter.StateControl.states.Resolving;
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x00066E87 File Offset: 0x00065087
		internal void setResolving()
		{
			this.currentState = CombatEncounter.StateControl.states.Resolving;
		}

		// Token: 0x0400076A RID: 1898
		private CombatEncounter.StateControl.states currentState;

		// Token: 0x020002FB RID: 763
		private enum states
		{
			// Token: 0x04000A96 RID: 2710
			Intro,
			// Token: 0x04000A97 RID: 2711
			Round,
			// Token: 0x04000A98 RID: 2712
			PlanningPlayer,
			// Token: 0x04000A99 RID: 2713
			PlanningNPC,
			// Token: 0x04000A9A RID: 2714
			Result,
			// Token: 0x04000A9B RID: 2715
			Victory,
			// Token: 0x04000A9C RID: 2716
			Defeat,
			// Token: 0x04000A9D RID: 2717
			FailedAction,
			// Token: 0x04000A9E RID: 2718
			Execution,
			// Token: 0x04000A9F RID: 2719
			Resolving
		}
	}
}
