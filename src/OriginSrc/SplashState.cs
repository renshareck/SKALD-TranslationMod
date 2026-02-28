using System;
using System.Collections.Generic;
using SkaldEnums;

// Token: 0x020000B1 RID: 177
public class SplashState : StateBase
{
	// Token: 0x06000ADA RID: 2778 RVA: 0x00034384 File Offset: 0x00032584
	public SplashState(DataControl dataControl, string musicPath, List<string> backgroundPaths, int timerLength) : base(dataControl)
	{
		this.guiControl = new GUIControl();
		dataControl.clearScene();
		this.timerCount = timerLength;
		this.resetTimer();
		AudioControl.playMusic(musicPath);
		this.backgroundPaths = backgroundPaths;
		this.exitState = SkaldStates.Overland;
	}

	// Token: 0x06000ADB RID: 2779 RVA: 0x000343DB File Offset: 0x000325DB
	private void resetTimer()
	{
		this.countDown = new CountDownClock(this.timerCount, false);
	}

	// Token: 0x06000ADC RID: 2780 RVA: 0x000343EF File Offset: 0x000325EF
	protected override void setBackground()
	{
		if (this.backgroundPaths != null && this.areThereStillBackgroundsLeft())
		{
			this.guiControl.setBackground(this.backgroundPaths[this.currentIndex]);
		}
	}

	// Token: 0x06000ADD RID: 2781 RVA: 0x00034420 File Offset: 0x00032620
	public override void update()
	{
		this.countDown.tick();
		this.setTargetState(SkaldStates.Null);
		this.setGUIData();
		if (SkaldIO.anyKeyPressed() || this.countDown.isTimerZero())
		{
			if (this.areThereStillBackgroundsLeft())
			{
				this.currentIndex++;
				this.resetTimer();
				this.setBackground();
			}
			else
			{
				this.exit();
			}
		}
		this.setGUIData();
	}

	// Token: 0x06000ADE RID: 2782 RVA: 0x00034489 File Offset: 0x00032689
	protected virtual void exit()
	{
		this.setTargetState(this.exitState);
	}

	// Token: 0x06000ADF RID: 2783 RVA: 0x00034497 File Offset: 0x00032697
	protected override void setGUIData()
	{
		this.setBackground();
	}

	// Token: 0x06000AE0 RID: 2784 RVA: 0x0003449F File Offset: 0x0003269F
	private bool areThereStillBackgroundsLeft()
	{
		return this.currentIndex < this.backgroundPaths.Count;
	}

	// Token: 0x040002E0 RID: 736
	protected SkaldStates exitState = SkaldStates.IntroMenu;

	// Token: 0x040002E1 RID: 737
	protected CountDownClock countDown;

	// Token: 0x040002E2 RID: 738
	private List<string> backgroundPaths;

	// Token: 0x040002E3 RID: 739
	private int currentIndex;

	// Token: 0x040002E4 RID: 740
	private int timerCount = 60;
}
