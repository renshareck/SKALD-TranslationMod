using System;

// Token: 0x02000171 RID: 369
public class CountDownClock
{
	// Token: 0x06001401 RID: 5121 RVA: 0x00058541 File Offset: 0x00056741
	public CountDownClock(int maxTime, bool loop = true)
	{
		this.maxTimer = maxTime;
		this.timer = maxTime;
		this.looping = loop;
	}

	// Token: 0x06001402 RID: 5122 RVA: 0x0005855E File Offset: 0x0005675E
	public bool isTimerZero()
	{
		return this.timer <= 0;
	}

	// Token: 0x06001403 RID: 5123 RVA: 0x0005856C File Offset: 0x0005676C
	public int getTimer()
	{
		return this.timer;
	}

	// Token: 0x06001404 RID: 5124 RVA: 0x00058574 File Offset: 0x00056774
	public void reset()
	{
		this.timer = this.maxTimer;
	}

	// Token: 0x06001405 RID: 5125 RVA: 0x00058582 File Offset: 0x00056782
	public void tick()
	{
		if (this.timer <= 0)
		{
			if (this.looping)
			{
				this.reset();
				return;
			}
		}
		else
		{
			this.timer--;
		}
	}

	// Token: 0x04000507 RID: 1287
	private int timer;

	// Token: 0x04000508 RID: 1288
	private int maxTimer;

	// Token: 0x04000509 RID: 1289
	private bool looping;
}
