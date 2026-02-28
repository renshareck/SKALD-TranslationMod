using System;
using SkaldEnums;

// Token: 0x02000077 RID: 119
public class CombatOverState : StateBase
{
	// Token: 0x060009A3 RID: 2467 RVA: 0x0002E39B File Offset: 0x0002C59B
	public CombatOverState(DataControl dataControl) : base(dataControl)
	{
		this.guiControl.setBigHeader("Victory!");
		this.victoryCountdown = new CountDownClock(120, false);
		this.activateState();
	}

	// Token: 0x060009A4 RID: 2468 RVA: 0x0002E3C9 File Offset: 0x0002C5C9
	public override StateBase activateState()
	{
		base.disableCharacterSwap();
		AudioControl.playMusic("Combat_victory");
		this.setGUIData();
		return this;
	}

	// Token: 0x060009A5 RID: 2469 RVA: 0x0002E3E4 File Offset: 0x0002C5E4
	public override void update()
	{
		base.update();
		this.victoryCountdown.tick();
		if (this.victoryCountdown.isTimerZero())
		{
			if (this.xpCountdown == null)
			{
				this.dataControl.getCombatEncounter().loot();
				this.guiControl.setBigHeader("Gained " + this.dataControl.combatEncounter.getXpBuffer().ToString() + " XP!");
				this.xpCountdown = new CountDownClock(120, false);
			}
			this.xpCountdown.tick();
			if (this.xpCountdown.isTimerZero())
			{
				if (this.dataControl.combatEncounter.lootBufferHasLoot())
				{
					this.dataControl.getLootBuffer();
				}
				else
				{
					this.leaveCombat();
				}
			}
		}
		this.setGUIData();
	}

	// Token: 0x060009A6 RID: 2470 RVA: 0x0002E4AD File Offset: 0x0002C6AD
	protected void leaveCombat()
	{
		this.dataControl.clearCombat();
		if (this.dataControl.isPartyDead())
		{
			this.setTargetState(SkaldStates.GameOver);
			return;
		}
		this.setTargetState(SkaldStates.Scene);
	}

	// Token: 0x060009A7 RID: 2471 RVA: 0x0002E4D7 File Offset: 0x0002C6D7
	protected override void setGUIData()
	{
		this.drawPortraits();
		base.setGUIData();
	}

	// Token: 0x04000278 RID: 632
	private CountDownClock victoryCountdown;

	// Token: 0x04000279 RID: 633
	private CountDownClock xpCountdown;
}
