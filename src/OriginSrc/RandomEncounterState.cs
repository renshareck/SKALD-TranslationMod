using System;
using System.Collections.Generic;
using SkaldEnums;

// Token: 0x02000096 RID: 150
public class RandomEncounterState : SceneBaseState
{
	// Token: 0x06000A78 RID: 2680 RVA: 0x000327DC File Offset: 0x000309DC
	public RandomEncounterState(DataControl dataControl) : base(dataControl)
	{
		this.encounter = dataControl.getRandomEncounter();
		this.NPCs = this.encounter.getListOfOpponents(dataControl.getParty().getPowerLevel());
		dataControl.clearRandomEncounter();
		this.setMainTextBuffer(this.encounter.getDescription());
		this.setGUIData();
		if (this.encounter.getImagePath() == "")
		{
			base.setNoImage();
		}
	}

	// Token: 0x06000A79 RID: 2681 RVA: 0x00032854 File Offset: 0x00030A54
	public override void update()
	{
		base.update();
		if (this.numericInputIndex != -1)
		{
			if (this.terminate)
			{
				if (this.numericInputIndex == 0)
				{
					this.leave();
				}
			}
			else if (this.isEncounterHostile())
			{
				if (this.numericInputIndex == 0)
				{
					this.mountEncounter();
				}
				else if (this.numericInputIndex == 1)
				{
					this.flee();
				}
				else if (this.numericInputIndex == 2)
				{
					this.stealth();
				}
				else if (this.numericInputIndex == 3)
				{
					this.diplomacy();
				}
			}
			else if (this.numericInputIndex == 0)
			{
				this.mountEncounter();
			}
			else if (this.numericInputIndex == 1)
			{
				this.setSuccess("You avoid the encounter...");
			}
			this.setGUIData();
			this.numericInputIndex = -1;
			return;
		}
		if (base.drawNoImage())
		{
			this.guiControl.setMap(this.dataControl.currentMap.getIllustratedOutputMap());
		}
	}

	// Token: 0x06000A7A RID: 2682 RVA: 0x0003292B File Offset: 0x00030B2B
	private void addPopUpOK(string text)
	{
		PopUpControl.addPopUpOK(text);
	}

	// Token: 0x06000A7B RID: 2683 RVA: 0x00032933 File Offset: 0x00030B33
	private void bribe()
	{
		if (!this.encounter.bribeLegal())
		{
			this.addPopUpOK("You cannot bribe this foe.");
			return;
		}
	}

	// Token: 0x06000A7C RID: 2684 RVA: 0x0003294E File Offset: 0x00030B4E
	private void diplomacy()
	{
		if (!this.encounter.diplomacyLegal())
		{
			this.addPopUpOK("You cannot parlay with this foe.");
			return;
		}
		PopUpControl.addRandomEncounterTestPopUp(this, "You attempt to parlay with your opponents.", "You successfully parlay your way out of trouble!", "You fail to resolve the situation peacefully. Combat erupts!", this.NPCs.getHighestAttributeStatic(AttributesControl.CoreAttributes.ATT_Will) + 4, AttributesControl.CoreAttributes.ATT_Diplomacy);
	}

	// Token: 0x06000A7D RID: 2685 RVA: 0x0003298E File Offset: 0x00030B8E
	public void setSuccess(string input)
	{
		this.setMainTextBuffer(input);
		this.terminate = true;
		this.setGUIData();
	}

	// Token: 0x06000A7E RID: 2686 RVA: 0x000329A4 File Offset: 0x00030BA4
	public void setFailure(string input)
	{
		this.mountEncounter();
		this.setMainTextBuffer(input);
		this.terminate = true;
		this.setGUIData();
	}

	// Token: 0x06000A7F RID: 2687 RVA: 0x000329C0 File Offset: 0x00030BC0
	private void stealth()
	{
		if (!this.encounter.stealthLegal())
		{
			this.addPopUpOK("You cannot sneak away from this foe.");
			return;
		}
		PopUpControl.addRandomEncounterTestPopUp(this, "You attempt to evade your opponents.", "You managed to sneak away from your opponents!", "You failed to sneak away from your opponents. Combat erupts!", this.NPCs.getHighestAttributeStatic(AttributesControl.CoreAttributes.ATT_Awareness) + 4, AttributesControl.CoreAttributes.ATT_Stealth);
	}

	// Token: 0x06000A80 RID: 2688 RVA: 0x00032A0C File Offset: 0x00030C0C
	private void flee()
	{
		if (!this.encounter.fleeLegal())
		{
			this.addPopUpOK("You cannot flee from this foe.");
			return;
		}
		PopUpControl.addRandomEncounterTestPopUp(this, "You attempt to flee your opponents.", "You manage to flee your opponents!", "You fail to flee. Combat erupts!", this.NPCs.getHighestAttributeStatic(AttributesControl.CoreAttributes.ATT_Athletics) + 4, AttributesControl.CoreAttributes.ATT_Athletics);
	}

	// Token: 0x06000A81 RID: 2689 RVA: 0x00032A58 File Offset: 0x00030C58
	private bool isEncounterHostile()
	{
		return this.NPCs.isHostile();
	}

	// Token: 0x06000A82 RID: 2690 RVA: 0x00032A68 File Offset: 0x00030C68
	private void mountEncounter()
	{
		Map map = this.encounter.getMap();
		if (map == null)
		{
			map = this.dataControl.getCurrentTile().getEncounterMap();
		}
		if (map == null)
		{
			return;
		}
		this.dataControl.mountMapDirect(map, true);
		this.dataControl.setOverland(5, 3, true);
		Prop prop = this.encounter.getProp();
		if (prop != null)
		{
			MapTile tile = map.getTile(SkaldRandom.range(4, 7), SkaldRandom.range(8, 10));
			if (tile != null)
			{
				map.setProp(prop, tile);
				string loadoutId = this.encounter.getLoadoutId();
				if (loadoutId != "" && prop.isContainer())
				{
					GameData.applyLoadoutData(loadoutId, tile.getInventory());
				}
			}
		}
		foreach (Character character in this.NPCs.getLiveCharacters())
		{
			character.setAlert();
			map.attemptToPlaceCharacterCloseToPoint(SkaldRandom.range(3, 9), SkaldRandom.range(5, 8), character, null);
		}
		if (this.isEncounterHostile())
		{
			this.dataControl.passRound();
			this.dataControl.launchCombat();
			this.setMainTextBuffer("You attack your foes!");
		}
		map.updateDrawLogic();
		this.dataControl.processString(this.encounter.getTrigger());
		this.terminate = true;
	}

	// Token: 0x06000A83 RID: 2691 RVA: 0x00032BC8 File Offset: 0x00030DC8
	private void leave()
	{
		this.setTargetState(SkaldStates.Overland);
	}

	// Token: 0x06000A84 RID: 2692 RVA: 0x00032BD4 File Offset: 0x00030DD4
	protected override void setGUIData()
	{
		base.setGUIData();
		if (!base.drawNoImage())
		{
			this.guiControl.setMainImage(this.encounter.getImagePath());
		}
		List<string> numericButtons;
		if (this.terminate)
		{
			numericButtons = new List<string>
			{
				"Continue"
			};
		}
		else if (this.isEncounterHostile())
		{
			numericButtons = new List<string>
			{
				"Attack",
				"Flee",
				"Sneak Away",
				"Parlay"
			};
		}
		else
		{
			numericButtons = new List<string>
			{
				"Approach",
				"Avoid"
			};
		}
		this.guiControl.setNumericButtons(numericButtons);
		this.guiControl.setSheetHeader("");
		this.guiControl.setPrimaryHeader(this.encounter.getNameSimple());
		this.guiControl.setSceneDescription(this.getMainTextBuffer());
	}

	// Token: 0x040002C8 RID: 712
	private EncounterControl.Encounter encounter;

	// Token: 0x040002C9 RID: 713
	private Party NPCs;

	// Token: 0x040002CA RID: 714
	private bool terminate;
}
