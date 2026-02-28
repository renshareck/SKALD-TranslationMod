using System;
using SkaldEnums;

// Token: 0x0200007C RID: 124
public class CombatStartState : StateBase
{
	// Token: 0x060009D0 RID: 2512 RVA: 0x0002F5B0 File Offset: 0x0002D7B0
	public CombatStartState(DataControl dataControl) : base(dataControl)
	{
		this.activateState();
		string text = this.dataControl.getRandomListEntry("SystemPreCombat");
		if (text == "")
		{
			text = "Steel Thyself!";
		}
		this.guiControl.setBigHeader(text);
		this.countDown = new CountDownClock(120, false);
		this.dataControl.shakeScreen("30");
	}

	// Token: 0x060009D1 RID: 2513 RVA: 0x0002F61A File Offset: 0x0002D81A
	public override StateBase activateState()
	{
		base.disableCharacterSwap();
		AudioControl.playCombatMusic();
		this.setGUIData();
		return this;
	}

	// Token: 0x060009D2 RID: 2514 RVA: 0x0002F62E File Offset: 0x0002D82E
	public override void update()
	{
		base.update();
		this.countDown.tick();
		if (this.countDown.isTimerZero())
		{
			this.setTargetState(SkaldStates.CombatPlacement);
		}
		this.setGUIData();
	}

	// Token: 0x060009D3 RID: 2515 RVA: 0x0002F65C File Offset: 0x0002D85C
	protected override void setGUIData()
	{
		this.drawPortraits();
		this.guiControl.setSecondaryDescription(this.dataControl.getDescription());
		base.setGUIData();
	}

	// Token: 0x04000297 RID: 663
	private CountDownClock countDown;
}
