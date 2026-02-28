using System;
using SkaldEnums;

// Token: 0x0200007B RID: 123
public class CombatSpellTargeting : CombatTargetingBase
{
	// Token: 0x060009CA RID: 2506 RVA: 0x0002F50A File Offset: 0x0002D70A
	public CombatSpellTargeting(DataControl dataControl) : base(dataControl)
	{
		this.descText = "Select a target for your SPELL!";
		this.hoverText = "Cast SPELL.";
	}

	// Token: 0x060009CB RID: 2507 RVA: 0x0002F529 File Offset: 0x0002D729
	protected override void setBigHeader()
	{
		this.guiControl.setBigHeader(this.combatEncounter.getCurrentCharacter().getSpellContainer().getTargetingString());
	}

	// Token: 0x060009CC RID: 2508 RVA: 0x0002F54B File Offset: 0x0002D74B
	protected override void useAbility()
	{
		this.combatEncounter.castSpell();
		base.exit(SkaldStates.CombatResolve);
	}

	// Token: 0x060009CD RID: 2509 RVA: 0x0002F560 File Offset: 0x0002D760
	protected override void printActionCounter()
	{
		this.guiControl.setActionCounter(base.getCurrentCharacter(), base.getCurrentCharacter().getSpellContainer().getCurrentComponent() as AbilityUseable);
	}

	// Token: 0x060009CE RID: 2510 RVA: 0x0002F588 File Offset: 0x0002D788
	protected override void setAreaEffectSelection(int x, int y)
	{
		this.combatEncounter.getCurrentCharacter().setCombatSpellTarget(x, y);
	}

	// Token: 0x060009CF RID: 2511 RVA: 0x0002F59C File Offset: 0x0002D79C
	protected override bool isCurrentAreaEffectLegal()
	{
		return this.combatEncounter.getCurrentCharacter().isSpellAreaEffectLegal();
	}
}
