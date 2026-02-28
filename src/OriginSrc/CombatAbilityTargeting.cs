using System;
using SkaldEnums;

// Token: 0x02000074 RID: 116
public class CombatAbilityTargeting : CombatTargetingBase
{
	// Token: 0x0600098D RID: 2445 RVA: 0x0002DE2B File Offset: 0x0002C02B
	public CombatAbilityTargeting(DataControl dataControl) : base(dataControl)
	{
		this.descText = "Select a target for your Maneuver!";
		this.hoverText = "Execute Manuever.";
	}

	// Token: 0x0600098E RID: 2446 RVA: 0x0002DE4A File Offset: 0x0002C04A
	protected override void setBigHeader()
	{
		this.guiControl.setBigHeader(this.combatEncounter.getCurrentCharacter().getAbilityManueverContainer().getTargetingString());
	}

	// Token: 0x0600098F RID: 2447 RVA: 0x0002DE6C File Offset: 0x0002C06C
	protected override void useAbility()
	{
		this.combatEncounter.useCombatManeuver();
		base.exit(SkaldStates.CombatResolve);
	}

	// Token: 0x06000990 RID: 2448 RVA: 0x0002DE81 File Offset: 0x0002C081
	protected override void printActionCounter()
	{
		this.guiControl.setActionCounter(base.getCurrentCharacter(), base.getCurrentCharacter().getAbilityManueverContainer().getCurrentComponent() as AbilityUseable);
	}

	// Token: 0x06000991 RID: 2449 RVA: 0x0002DEA9 File Offset: 0x0002C0A9
	protected override void setAreaEffectSelection(int x, int y)
	{
		this.combatEncounter.getCurrentCharacter().setAbilityTarget(x, y);
	}

	// Token: 0x06000992 RID: 2450 RVA: 0x0002DEBD File Offset: 0x0002C0BD
	protected override bool isCurrentAreaEffectLegal()
	{
		return this.combatEncounter.getCurrentCharacter().isAbilityAreaEffectLegal();
	}
}
