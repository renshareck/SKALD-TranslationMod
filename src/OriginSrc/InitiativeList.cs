using System;
using System.Collections.Generic;

// Token: 0x02000032 RID: 50
public class InitiativeList
{
	// Token: 0x0600053A RID: 1338 RVA: 0x00018C20 File Offset: 0x00016E20
	public InitiativeList(Party _playerParty, Party _opponentParty, Party _allyParty)
	{
		this.playerParty = _playerParty;
		this.opponentParty = _opponentParty;
		this.allyParty = _allyParty;
		this.rollInitiative();
	}

	// Token: 0x0600053B RID: 1339 RVA: 0x00018C4E File Offset: 0x00016E4E
	public Character getCurrentCharacter()
	{
		return this.currentCharacter;
	}

	// Token: 0x0600053C RID: 1340 RVA: 0x00018C56 File Offset: 0x00016E56
	public List<Character> getInitiativeList()
	{
		return this.initiativeList;
	}

	// Token: 0x0600053D RID: 1341 RVA: 0x00018C60 File Offset: 0x00016E60
	private void setLowestInitiativeMinusOne(Character character)
	{
		if (this.initiativeList.Count == 0)
		{
			return;
		}
		int currentInitiative = this.initiativeList[0].getCurrentInitiative();
		foreach (Character character2 in this.initiativeList)
		{
			if (character2.getCurrentInitiative() < currentInitiative)
			{
				currentInitiative = character2.getCurrentInitiative();
			}
		}
		character.setCurrentInitiative(currentInitiative - 1);
	}

	// Token: 0x0600053E RID: 1342 RVA: 0x00018CE8 File Offset: 0x00016EE8
	public SkaldActionResult holdCurrentCharactersAction()
	{
		MainControl.log("BEGIN HOLDING!");
		if (this.isCurrentCharacterLast())
		{
			this.getCurrentCharacter().setWaitInCombat();
			return new SkaldActionResult(false, false, this.getCurrentCharacter().getName() + " is already last in initiative order.", true);
		}
		if (!this.getCurrentCharacter().canHoldAction())
		{
			this.getCurrentCharacter().setWaitInCombat();
			return new SkaldActionResult(false, false, "Characters cannot hold actions once they have begun acting.", true);
		}
		MainControl.log(this.getCurrentCharacter().getName() + " Holding!");
		Character character = this.getCurrentCharacter();
		character.holdAction();
		this.setLowestInitiativeMinusOne(character);
		this.setNextCharacter();
		this.sortInitiativeList();
		return new SkaldActionResult(true, true, character.getName() + " holds " + character.getHisHer("") + " action.", true);
	}

	// Token: 0x0600053F RID: 1343 RVA: 0x00018DB8 File Offset: 0x00016FB8
	private bool isCurrentCharacterLast()
	{
		return this.initiativeList.Count != 0 && this.initiativeList[this.initiativeList.Count - 1] == this.currentCharacter;
	}

	// Token: 0x06000540 RID: 1344 RVA: 0x00018DEC File Offset: 0x00016FEC
	public void rollInitiative()
	{
		foreach (Character c in this.playerParty.getPartyList())
		{
			this.addIntoInitiativeList(c);
		}
		foreach (Character c2 in this.opponentParty.getPartyList())
		{
			this.addIntoInitiativeList(c2);
		}
		foreach (Character c3 in this.allyParty.getPartyList())
		{
			this.addIntoInitiativeList(c3);
		}
		this.sortInitiativeList();
	}

	// Token: 0x06000541 RID: 1345 RVA: 0x00018ED8 File Offset: 0x000170D8
	public void addIntoInitiativeList(Character c)
	{
		if (this.initiativeList.Contains(c))
		{
			return;
		}
		c.rollInitiative();
		this.initiativeList.Add(c);
	}

	// Token: 0x06000542 RID: 1346 RVA: 0x00018EFC File Offset: 0x000170FC
	public void setNextCharacter()
	{
		if (this.initiativeList.Count == 0)
		{
			return;
		}
		if (this.getCurrentCharacter() == null)
		{
			this.setCurrentCharacter(this.initiativeList[0]);
		}
		for (int i = 0; i < this.initiativeList.Count; i++)
		{
			if (this.initiativeList[i] == this.getCurrentCharacter())
			{
				if (this.getCurrentCharacter().hasRemainingCombatMovesOrAttacks() && !this.getCurrentCharacter().isHoldingAction())
				{
					return;
				}
				if (i == this.initiativeList.Count - 1)
				{
					this.clearCurrentCharacter();
					return;
				}
				this.setCurrentCharacter(this.initiativeList[i + 1]);
			}
		}
		this.clearCurrentCharacter();
	}

	// Token: 0x06000543 RID: 1347 RVA: 0x00018FA8 File Offset: 0x000171A8
	private void setCurrentCharacter(Character newCharacter)
	{
		if (this.isCharPlayer(newCharacter))
		{
			this.playerParty.setCurrentPC(newCharacter);
		}
		else if (newCharacter.isHostile() && newCharacter.getTargetOpponent() != null)
		{
			this.playerParty.setCurrentPC(newCharacter.getTargetOpponent());
		}
		newCharacter.startOfTurnUpkeep();
		this.currentCharacter = newCharacter;
	}

	// Token: 0x06000544 RID: 1348 RVA: 0x00018FFA File Offset: 0x000171FA
	private void clearCurrentCharacter()
	{
		this.currentCharacter = null;
	}

	// Token: 0x06000545 RID: 1349 RVA: 0x00019003 File Offset: 0x00017203
	public bool isCharPlayer(Character character)
	{
		return character != null && character.isPC();
	}

	// Token: 0x06000546 RID: 1350 RVA: 0x00019018 File Offset: 0x00017218
	public void sortInitiativeList()
	{
		List<Character> list = new List<Character>();
		foreach (Character character in this.initiativeList)
		{
			if (!character.isDead())
			{
				if (list.Count == 0)
				{
					list.Add(character);
				}
				else
				{
					for (int i = 0; i < list.Count; i++)
					{
						if (character.getCurrentInitiative() > list[i].getCurrentInitiative())
						{
							list.Insert(i, character);
							break;
						}
						if (i == list.Count - 1)
						{
							list.Insert(i + 1, character);
							break;
						}
					}
				}
			}
		}
		this.initiativeList = list;
	}

	// Token: 0x06000547 RID: 1351 RVA: 0x000190D0 File Offset: 0x000172D0
	public string printInitiativeOrder()
	{
		string text = "";
		for (int i = 0; i < this.initiativeList.Count; i++)
		{
			string nameColored = this.initiativeList[i].getNameColored();
			if (this.initiativeList[i] == this.getCurrentCharacter())
			{
				text = string.Concat(new string[]
				{
					text,
					nameColored.ToUpper(),
					" ",
					this.initiativeList[i].printInitiativeStatus(),
					"\n"
				});
			}
			else if (this.getCurrentCharacter() != null && this.getCurrentCharacter().getTargetOpponent() != null && this.initiativeList[i] == this.getCurrentCharacter().getTargetOpponent())
			{
				text = string.Concat(new string[]
				{
					text,
					nameColored,
					"* ",
					this.initiativeList[i].printInitiativeStatus(),
					"\n"
				});
			}
			else
			{
				text = string.Concat(new string[]
				{
					text,
					nameColored,
					" ",
					this.initiativeList[i].printInitiativeStatus(),
					"\n"
				});
			}
		}
		return text;
	}

	// Token: 0x06000548 RID: 1352 RVA: 0x00019208 File Offset: 0x00017408
	public string printInitiativeOrderWithRoll()
	{
		string text = "";
		for (int i = 0; i < this.initiativeList.Count; i++)
		{
			string nameColored = this.initiativeList[i].getNameColored();
			text = string.Concat(new string[]
			{
				text,
				nameColored,
				" (",
				this.initiativeList[i].printInitiativeStatus(),
				") Initiative: ",
				this.initiativeList[i].getCurrentInitiative().ToString(),
				"\n"
			});
		}
		return text;
	}

	// Token: 0x04000144 RID: 324
	private Party playerParty;

	// Token: 0x04000145 RID: 325
	private Party opponentParty;

	// Token: 0x04000146 RID: 326
	private Party allyParty;

	// Token: 0x04000147 RID: 327
	private List<Character> initiativeList = new List<Character>();

	// Token: 0x04000148 RID: 328
	private Character currentCharacter;
}
