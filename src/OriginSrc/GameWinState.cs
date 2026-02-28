using System;
using System.Collections.Generic;
using SkaldEnums;

// Token: 0x020000B0 RID: 176
public class GameWinState : SplashState
{
	// Token: 0x06000AD8 RID: 2776 RVA: 0x0003435A File Offset: 0x0003255A
	public GameWinState(DataControl dataControl, string cutSceneId) : base(dataControl, "", new List<string>(), 180)
	{
		this.exitState = SkaldStates.Null;
		dataControl.addCutSceneAnimated(cutSceneId);
	}

	// Token: 0x06000AD9 RID: 2777 RVA: 0x00034380 File Offset: 0x00032580
	protected override void exit()
	{
	}
}
