using System;
using System.Collections.Generic;

// Token: 0x020000AE RID: 174
public class DemoOverSplashState : SplashState
{
	// Token: 0x06000AD4 RID: 2772 RVA: 0x000342E9 File Offset: 0x000324E9
	public DemoOverSplashState(DataControl dataControl) : base(dataControl, "cutscene", new List<string>
	{
		"demoOverBlurb"
	}, 60)
	{
	}

	// Token: 0x06000AD5 RID: 2773 RVA: 0x00034309 File Offset: 0x00032509
	protected override void exit()
	{
		this.dataControl.openSteam();
		MainControl.restartGame();
	}
}
