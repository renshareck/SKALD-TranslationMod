using System;
using SkaldEnums;

// Token: 0x0200007A RID: 122
public class CombatResolveState : CombatBaseState
{
	// Token: 0x060009C7 RID: 2503 RVA: 0x0002F4A4 File Offset: 0x0002D6A4
	public CombatResolveState(DataControl dataControl) : base(dataControl)
	{
		this.stateId = SkaldStates.CombatResolve;
	}

	// Token: 0x060009C8 RID: 2504 RVA: 0x0002F4B8 File Offset: 0x0002D6B8
	public override void update()
	{
		if (this.testNewState())
		{
			return;
		}
		if (base.updateEmergencyEscapeClock())
		{
			return;
		}
		base.update();
		this.setGUIData();
		if (this.combatEncounter.isCurrentCharacterReadyForResults() && !this.combatEncounter.moveNPCIfNecessary())
		{
			this.combatEncounter.gotoNextState();
		}
	}

	// Token: 0x060009C9 RID: 2505 RVA: 0x0002F508 File Offset: 0x0002D708
	protected override void setButtonData()
	{
	}
}
