using System;
using System.Collections.Generic;

// Token: 0x0200005D RID: 93
public abstract class PopUpTestBase : PopUpBase
{
	// Token: 0x06000898 RID: 2200 RVA: 0x00029FC9 File Offset: 0x000281C9
	protected PopUpTestBase(string description, List<string> options) : base(description, options)
	{
		this.allowCharacterSwap = true;
	}

	// Token: 0x06000899 RID: 2201 RVA: 0x00029FF3 File Offset: 0x000281F3
	protected virtual Character getCharacter()
	{
		return MainControl.getDataControl().getCurrentPC();
	}

	// Token: 0x0600089A RID: 2202 RVA: 0x00029FFF File Offset: 0x000281FF
	protected string getLoadString()
	{
		if (this.loadStringCountdown.isTimerZero())
		{
			this.loadString += "*";
		}
		this.loadStringCountdown.tick();
		return this.loadString;
	}

	// Token: 0x0600089B RID: 2203 RVA: 0x0002A035 File Offset: 0x00028235
	protected void resetLoadString()
	{
		this.loadString = "";
	}

	// Token: 0x0600089C RID: 2204 RVA: 0x0002A042 File Offset: 0x00028242
	protected override void setPopUpUI(string description, List<string> buttonList)
	{
		this.uiElements = new PopUpBase.PopUpUITest(description, buttonList);
	}

	// Token: 0x0600089D RID: 2205 RVA: 0x0002A051 File Offset: 0x00028251
	public void roll(SkaldTestBase test)
	{
		this.getPopUpUITest().roll(test);
	}

	// Token: 0x0600089E RID: 2206 RVA: 0x0002A05F File Offset: 0x0002825F
	public bool isRolling()
	{
		return this.getPopUpUITest() != null && this.getPopUpUITest().isRolling();
	}

	// Token: 0x0600089F RID: 2207 RVA: 0x0002A076 File Offset: 0x00028276
	protected PopUpBase.PopUpUITest getPopUpUITest()
	{
		return this.uiElements as PopUpBase.PopUpUITest;
	}

	// Token: 0x060008A0 RID: 2208 RVA: 0x0002A083 File Offset: 0x00028283
	public virtual void succeed()
	{
		base.handle(true);
	}

	// Token: 0x060008A1 RID: 2209 RVA: 0x0002A08C File Offset: 0x0002828C
	public virtual void fail()
	{
		base.handle(true);
	}

	// Token: 0x060008A2 RID: 2210 RVA: 0x0002A095 File Offset: 0x00028295
	protected override void handle(bool result)
	{
		if (this.test != null)
		{
			if (this.test.wasSuccess())
			{
				this.succeed();
			}
			else
			{
				this.fail();
			}
		}
		base.handle(result);
	}

	// Token: 0x040001EE RID: 494
	protected SkaldTestBase test;

	// Token: 0x040001EF RID: 495
	private CountDownClock loadStringCountdown = new CountDownClock(15, true);

	// Token: 0x040001F0 RID: 496
	private string loadString = "";
}
