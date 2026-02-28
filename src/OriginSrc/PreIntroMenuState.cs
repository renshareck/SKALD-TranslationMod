using System;
using SkaldEnums;

// Token: 0x0200008C RID: 140
public class PreIntroMenuState : BaseMenuState
{
	// Token: 0x06000A47 RID: 2631 RVA: 0x0003134C File Offset: 0x0002F54C
	public PreIntroMenuState(DataControl dataControl) : base(dataControl)
	{
		this.guiControl = new GUIControl();
	}

	// Token: 0x06000A48 RID: 2632 RVA: 0x0003136E File Offset: 0x0002F56E
	public override void update()
	{
		this.countDown.tick();
		if (IntroBackgroundMaker.isLogoFinished())
		{
			this.setTargetState(SkaldStates.IntroMenu);
		}
		this.setGUIData();
	}

	// Token: 0x040002B0 RID: 688
	protected CountDownClock countDown = new CountDownClock(120, false);
}
