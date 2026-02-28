using System;
using UnityEngine;

// Token: 0x02000029 RID: 41
internal static class IntroBackgroundMaker
{
	// Token: 0x06000464 RID: 1124 RVA: 0x00015A02 File Offset: 0x00013C02
	public static void togglePan()
	{
		IntroBackgroundMaker.parallaxControl.togglePan();
	}

	// Token: 0x06000465 RID: 1125 RVA: 0x00015A0E File Offset: 0x00013C0E
	public static bool isLogoFinished()
	{
		return IntroBackgroundMaker.logo.isFinished();
	}

	// Token: 0x06000466 RID: 1126 RVA: 0x00015A1C File Offset: 0x00013C1C
	private static void drawVersionNumber()
	{
		if (IntroBackgroundMaker.versionNumber == null)
		{
			IntroBackgroundMaker.versionNumber = new UITextBlock(8, 12, 0, 12, C64Color.GrayLight);
			IntroBackgroundMaker.versionNumber.stretchHorizontal = true;
			IntroBackgroundMaker.versionNumber.setContent(GameData.getVersionNumberAndEdition());
		}
		IntroBackgroundMaker.versionNumber.draw(IntroBackgroundMaker.outputTexture);
	}

	// Token: 0x06000467 RID: 1127 RVA: 0x00015A70 File Offset: 0x00013C70
	public static TextureTools.TextureData drawAndGetOutput()
	{
		IntroBackgroundMaker.outputTexture = IntroBackgroundMaker.parallaxControl.drawAndGetOutput();
		IntroBackgroundMaker.logo.draw(IntroBackgroundMaker.outputTexture);
		if (IntroBackgroundMaker.isLogoFinished())
		{
			if (!IntroBackgroundMaker.logoFinishedTriggeresFired)
			{
				IntroBackgroundMaker.logoFinishedTriggeresFired = true;
				IntroBackgroundMaker.parallaxControl.setRaining();
			}
			IntroBackgroundMaker.drawVersionNumber();
		}
		else if (IntroBackgroundMaker.logo.getLife() > 75)
		{
			IntroBackgroundMaker.genericEffectsControl.setScreenShake(5);
		}
		IntroBackgroundMaker.genericEffectsControl.updateGlobalEffects(IntroBackgroundMaker.outputTexture);
		return IntroBackgroundMaker.outputTexture;
	}

	// Token: 0x040000EC RID: 236
	private static IntroBackgroundParallaxControl parallaxControl = new IntroBackgroundParallaxControl();

	// Token: 0x040000ED RID: 237
	private static TextureTools.TextureData outputTexture = new TextureTools.TextureData(480, 270);

	// Token: 0x040000EE RID: 238
	private static IntroBackgroundMaker.Logo logo = new IntroBackgroundMaker.Logo();

	// Token: 0x040000EF RID: 239
	private static bool logoFinishedTriggeresFired = false;

	// Token: 0x040000F0 RID: 240
	private static GenericEffectsControl genericEffectsControl = new GenericEffectsControl();

	// Token: 0x040000F1 RID: 241
	private static UITextBlock versionNumber = null;

	// Token: 0x020001CF RID: 463
	private class Logo : UIImage
	{
		// Token: 0x060016B2 RID: 5810 RVA: 0x00065A50 File Offset: 0x00063C50
		public Logo()
		{
			this.targetTexture = TextureTools.loadTextureData("Images/Backgrounds/Logo");
			this.foregroundTexture = new TextureTools.TextureData(this.targetTexture.width, this.targetTexture.height);
			this.fadeArray = new bool[this.targetTexture.colors.Length];
			this.setPosition(49, 246);
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x00065B04 File Offset: 0x00063D04
		public override void draw(TextureTools.TextureData targetTexture)
		{
			this.life++;
			this.fadeIn();
			this.fadeIn();
			this.flash();
			base.draw(targetTexture);
			this.particleSystem.update(0, 0, targetTexture);
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x00065B3B File Offset: 0x00063D3B
		public int getLife()
		{
			return this.life;
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x00065B43 File Offset: 0x00063D43
		public bool isFinished()
		{
			return this.cellularGrowthFinished && this.flashFinished;
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x00065B58 File Offset: 0x00063D58
		private void flash()
		{
			if (this.flashFinished)
			{
				return;
			}
			if (this.cellularGrowthFinished)
			{
				this.flashFinished = true;
				this.foregroundColors.mainColor = Color.clear;
				return;
			}
			this.flashChance++;
			if (this.flashChance > 40)
			{
				this.flashChance = 40;
			}
			if (Random.Range(0, 100) < this.flashChance)
			{
				this.foregroundColors.mainColor = this.colorArray[Random.Range(0, this.colorArray.Length)];
				return;
			}
			this.foregroundColors.mainColor = Color.clear;
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x00065C00 File Offset: 0x00063E00
		private void fadeIn()
		{
			if (this.cellularGrowthFinished)
			{
				return;
			}
			this.cellularGrowthFinished = true;
			bool[] array = new bool[this.fadeArray.Length];
			if (this.fireChance < 20)
			{
				this.fireChance++;
			}
			this.seedCount++;
			if (this.seedCount == 0)
			{
				this.setPixel(this.targetTexture.width * (this.targetTexture.height / 2) + (int)((double)this.targetTexture.width * 0.85));
			}
			if (this.seedCount == 10)
			{
				this.setPixel(this.targetTexture.width * (this.targetTexture.height / 2) + (int)((double)this.targetTexture.width * 0.25));
			}
			for (int i = 0; i < this.fadeArray.Length; i++)
			{
				if (this.fadeArray[i])
				{
					array[i] = true;
				}
				else
				{
					this.cellularGrowthFinished = false;
					if (this.shouldWeSetPixel(i))
					{
						array[i] = true;
						if (!this.targetTexture.isPixelTransparent(i) && Random.Range(0, 100) < this.fireChance)
						{
							int xStart = this.getX() + i % this.targetTexture.width;
							int yStart = this.getY() + i / this.targetTexture.width - this.getHeight();
							this.particleSystem.addFireFlashTiny(xStart, yStart);
						}
					}
				}
			}
			for (int j = 0; j < this.fadeArray.Length; j++)
			{
				if (array[j])
				{
					this.setPixel(j);
				}
			}
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x00065D8F File Offset: 0x00063F8F
		private void setPixel(int index)
		{
			this.fadeArray[index] = true;
			this.foregroundTexture.colors[index] = this.targetTexture.colors[index];
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x00065DBC File Offset: 0x00063FBC
		private bool shouldWeSetPixel(int i)
		{
			int num = 0;
			int num2 = 25;
			if (i != 0 && this.fadeArray[i - 1])
			{
				num += num2;
			}
			if (i + 1 < this.fadeArray.Length && this.fadeArray[i + 1])
			{
				num += num2;
			}
			if (i + this.foregroundTexture.width < this.fadeArray.Length && this.fadeArray[i + this.foregroundTexture.width])
			{
				num += num2;
			}
			if (i - this.foregroundTexture.width >= 0 && this.fadeArray[i - this.foregroundTexture.width])
			{
				num += num2;
			}
			return num != 0 && Random.Range(0, 100) < num;
		}

		// Token: 0x0400071D RID: 1821
		private TextureTools.TextureData targetTexture;

		// Token: 0x0400071E RID: 1822
		private bool[] fadeArray;

		// Token: 0x0400071F RID: 1823
		private bool cellularGrowthFinished;

		// Token: 0x04000720 RID: 1824
		private bool flashFinished;

		// Token: 0x04000721 RID: 1825
		private int flashChance = -30;

		// Token: 0x04000722 RID: 1826
		private int fireChance = -30;

		// Token: 0x04000723 RID: 1827
		private int seedCount;

		// Token: 0x04000724 RID: 1828
		private int life;

		// Token: 0x04000725 RID: 1829
		private ParticleSystem particleSystem = new ParticleSystem();

		// Token: 0x04000726 RID: 1830
		private Color32[] colorArray = new Color32[]
		{
			C64Color.White,
			C64Color.Yellow,
			C64Color.RedLight
		};
	}
}
