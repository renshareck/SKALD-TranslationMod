using System;
using UnityEngine;

// Token: 0x02000193 RID: 403
public class GenericEffectsControl
{
	// Token: 0x060014F8 RID: 5368 RVA: 0x0005D07F File Offset: 0x0005B27F
	public void updateGlobalEffects(TextureTools.TextureData input)
	{
		this.flashScreen(input);
		this.shakeScreen(input);
		this.pullCurtain(input);
	}

	// Token: 0x060014F9 RID: 5369 RVA: 0x0005D096 File Offset: 0x0005B296
	public void setCurtain()
	{
		this.curtain = 1.2f;
	}

	// Token: 0x060014FA RID: 5370 RVA: 0x0005D0A3 File Offset: 0x0005B2A3
	public bool isCurtainReady()
	{
		return this.curtain < 0.6f;
	}

	// Token: 0x060014FB RID: 5371 RVA: 0x0005D0B8 File Offset: 0x0005B2B8
	private void pullCurtain(TextureTools.TextureData input)
	{
		if (this.curtain == 0f)
		{
			return;
		}
		if (this.curtain > 0f)
		{
			this.curtain -= 0.02f;
		}
		if (this.curtain < 0f)
		{
			this.curtain = 0f;
		}
		TextureTools.pullCurtain(this.curtain, input);
	}

	// Token: 0x060014FC RID: 5372 RVA: 0x0005D116 File Offset: 0x0005B316
	private void flashScreen(TextureTools.TextureData output)
	{
		if (this.flash > 0)
		{
			this.flash--;
			if (Random.Range(0, 100) < 60)
			{
				TextureTools.applyNegative(output);
			}
		}
	}

	// Token: 0x060014FD RID: 5373 RVA: 0x0005D141 File Offset: 0x0005B341
	private void shakeScreen(TextureTools.TextureData output)
	{
		if (this.screenShake == 0)
		{
			return;
		}
		this.screenShake--;
		if (GlobalSettings.getDisplaySettings().allowScreenshake())
		{
			TextureTools.shakeScreen(output);
		}
	}

	// Token: 0x060014FE RID: 5374 RVA: 0x0005D16C File Offset: 0x0005B36C
	public void setScreenShake(int i)
	{
		this.screenShake = i;
	}

	// Token: 0x060014FF RID: 5375 RVA: 0x0005D175 File Offset: 0x0005B375
	public void setFlash(int i)
	{
		this.flash = i;
	}

	// Token: 0x0400055B RID: 1371
	private int screenShake;

	// Token: 0x0400055C RID: 1372
	private int flash;

	// Token: 0x0400055D RID: 1373
	private float curtain;

	// Token: 0x0400055E RID: 1374
	private const float maxCurtain = 1.2f;
}
