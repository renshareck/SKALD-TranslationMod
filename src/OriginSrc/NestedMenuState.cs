using System;
using SkaldEnums;

// Token: 0x0200008B RID: 139
public class NestedMenuState : BaseMenuState
{
	// Token: 0x06000A43 RID: 2627 RVA: 0x00031311 File Offset: 0x0002F511
	public NestedMenuState(DataControl dataControl) : base(dataControl)
	{
	}

	// Token: 0x06000A44 RID: 2628 RVA: 0x00031321 File Offset: 0x0002F521
	protected virtual SkaldStates getExitState()
	{
		return this.exitTarget;
	}

	// Token: 0x06000A45 RID: 2629 RVA: 0x00031329 File Offset: 0x0002F529
	public override void update()
	{
		base.update();
		if (SkaldIO.getPressedEscapeKey())
		{
			this.exit();
		}
	}

	// Token: 0x06000A46 RID: 2630 RVA: 0x0003133E File Offset: 0x0002F53E
	protected void exit()
	{
		this.setTargetState(this.getExitState());
	}

	// Token: 0x040002AF RID: 687
	protected SkaldStates exitTarget = SkaldStates.Overland;
}
