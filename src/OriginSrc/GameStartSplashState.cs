using System;
using System.Collections.Generic;
using SkaldEnums;

// Token: 0x020000AF RID: 175
public class GameStartSplashState : SplashState
{
	// Token: 0x06000AD6 RID: 2774 RVA: 0x0003431B File Offset: 0x0003251B
	public GameStartSplashState(DataControl dataControl) : base(dataControl, "", new List<string>
	{
		"IntroSlide1",
		"IntroSlide2"
	}, 120)
	{
		this.exitState = SkaldStates.IntroSplash;
	}

	// Token: 0x06000AD7 RID: 2775 RVA: 0x0003434D File Offset: 0x0003254D
	protected override void exit()
	{
		ScreenControl.enforceResolution();
		base.exit();
	}
}
