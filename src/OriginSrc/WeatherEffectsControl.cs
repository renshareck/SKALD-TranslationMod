using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000197 RID: 407
public class WeatherEffectsControl
{
	// Token: 0x06001535 RID: 5429 RVA: 0x0005DEC8 File Offset: 0x0005C0C8
	public void applyEffect(TextureTools.TextureData input, int xScrollOffset, int yScrollOffset, TextureTools.TextureData punchOut = null)
	{
		this.applyFog(input, xScrollOffset, yScrollOffset, this.xScroll, this.yScroll, punchOut);
		this.applyFogRow(input, xScrollOffset, yScrollOffset, this.xScroll, this.yScroll, punchOut);
		this.applyRain(input, punchOut);
		this.xScroll = 0;
		this.yScroll = 0;
	}

	// Token: 0x06001536 RID: 5430 RVA: 0x0005DF1A File Offset: 0x0005C11A
	public void setXScroll(int i)
	{
		this.xScroll = i;
	}

	// Token: 0x06001537 RID: 5431 RVA: 0x0005DF23 File Offset: 0x0005C123
	public void setYScroll(int i)
	{
		this.yScroll = i;
	}

	// Token: 0x06001538 RID: 5432 RVA: 0x0005DF2C File Offset: 0x0005C12C
	public string printDescription()
	{
		if (this.isItFoggy() && this.isItRainy())
		{
			return "Fog and Rain";
		}
		if (this.isItFoggy())
		{
			return "Foggy";
		}
		if (this.isItRainy())
		{
			return "Raining";
		}
		return "Overcast";
	}

	// Token: 0x06001539 RID: 5433 RVA: 0x0005DF65 File Offset: 0x0005C165
	public bool isItFoggy()
	{
		return this.fogMaker != null;
	}

	// Token: 0x0600153A RID: 5434 RVA: 0x0005DF70 File Offset: 0x0005C170
	public bool isItRainy()
	{
		return this.rainMaker != null && this.rainMaker.isItRaining();
	}

	// Token: 0x0600153B RID: 5435 RVA: 0x0005DF87 File Offset: 0x0005C187
	private void applyRain(TextureTools.TextureData input, TextureTools.TextureData punchOut)
	{
		if (this.rainMaker != null)
		{
			this.rainMaker.applyRain(input, punchOut);
		}
	}

	// Token: 0x0600153C RID: 5436 RVA: 0x0005DF9E File Offset: 0x0005C19E
	private void applyFog(TextureTools.TextureData input, int xScrollOffset, int yScrollOffset, int _xScroll, int _yScroll, TextureTools.TextureData punchOut)
	{
		if (this.fogMaker == null)
		{
			return;
		}
		this.fogMaker.update(input, xScrollOffset, yScrollOffset, _xScroll, _yScroll, punchOut);
	}

	// Token: 0x0600153D RID: 5437 RVA: 0x0005DFBD File Offset: 0x0005C1BD
	private void applyFogRow(TextureTools.TextureData input, int xScrollOffset, int yScrollOffset, int _xScroll, int _yScroll, TextureTools.TextureData punchOut)
	{
		if (this.fogRow == null)
		{
			return;
		}
		this.fogRow.update(input, xScrollOffset, yScrollOffset, _xScroll, _yScroll, punchOut);
	}

	// Token: 0x0600153E RID: 5438 RVA: 0x0005DFDC File Offset: 0x0005C1DC
	public void setRain(int duration)
	{
		if (duration <= 0)
		{
			this.clearRain();
		}
		if (this.rainMaker == null)
		{
			this.rainMaker = new WeatherEffectsControl.RainMaker();
		}
		this.rainMaker.setDuration(duration);
	}

	// Token: 0x0600153F RID: 5439 RVA: 0x0005E007 File Offset: 0x0005C207
	public void clearFog()
	{
		this.fogMaker = null;
		this.fogRow = null;
	}

	// Token: 0x06001540 RID: 5440 RVA: 0x0005E017 File Offset: 0x0005C217
	public void clearRain()
	{
		this.rainMaker = null;
	}

	// Token: 0x06001541 RID: 5441 RVA: 0x0005E020 File Offset: 0x0005C220
	public void setFog(int i)
	{
		if (this.fogMaker == null)
		{
			this.fogMaker = new WeatherEffectsControl.FogMaker();
		}
	}

	// Token: 0x06001542 RID: 5442 RVA: 0x0005E035 File Offset: 0x0005C235
	public void setRainSplashOnColor()
	{
		if (this.rainMaker == null)
		{
			return;
		}
		this.rainMaker.setSplashOnColor();
	}

	// Token: 0x06001543 RID: 5443 RVA: 0x0005E04B File Offset: 0x0005C24B
	public void setFogRow(int yPosition)
	{
		if (this.fogRow == null)
		{
			this.fogRow = new WeatherEffectsControl.FogRow(yPosition);
		}
	}

	// Token: 0x06001544 RID: 5444 RVA: 0x0005E061 File Offset: 0x0005C261
	public void applySaveData(WeatherEffectsControl.WeatherSaveData data)
	{
		if (data == null)
		{
			return;
		}
		if (data.fog)
		{
			this.setFog(1);
		}
		this.setRain(data.rain);
	}

	// Token: 0x06001545 RID: 5445 RVA: 0x0005E084 File Offset: 0x0005C284
	public WeatherEffectsControl.WeatherSaveData getSaveData()
	{
		WeatherEffectsControl.WeatherSaveData weatherSaveData = new WeatherEffectsControl.WeatherSaveData();
		weatherSaveData.fog = (this.fogMaker != null);
		if (this.rainMaker != null)
		{
			weatherSaveData.rain = this.rainMaker.getDuration();
		}
		return weatherSaveData;
	}

	// Token: 0x04000575 RID: 1397
	private WeatherEffectsControl.RainMaker rainMaker;

	// Token: 0x04000576 RID: 1398
	private WeatherEffectsControl.FogMaker fogMaker;

	// Token: 0x04000577 RID: 1399
	private WeatherEffectsControl.FogRow fogRow;

	// Token: 0x04000578 RID: 1400
	private int xScroll;

	// Token: 0x04000579 RID: 1401
	private int yScroll;

	// Token: 0x020002E7 RID: 743
	[Serializable]
	public class WeatherSaveData
	{
		// Token: 0x04000A4E RID: 2638
		public bool fog;

		// Token: 0x04000A4F RID: 2639
		public int rain;
	}

	// Token: 0x020002E8 RID: 744
	protected class FogRow : WeatherEffectsControl.FogMaker
	{
		// Token: 0x06001BFB RID: 7163 RVA: 0x00078FD8 File Offset: 0x000771D8
		public FogRow(int yPosition)
		{
			this.yPosition = yPosition;
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x00078FE8 File Offset: 0x000771E8
		protected override void populate(TextureTools.TextureData targetTexture)
		{
			float xSpeed = 0f;
			float num = (float)Random.Range(-100, MapIllustrator.ScreenDimensions.getIllustratedMapHalfWidth() * 16);
			this.fogList.Add(new WeatherEffectsControl.FogMaker.ProceduralFog(num, (float)this.yPosition, xSpeed));
			this.fogList.Add(new WeatherEffectsControl.FogMaker.ProceduralFog(num + 150f, (float)this.yPosition, xSpeed));
			this.fogList.Add(new WeatherEffectsControl.FogMaker.ProceduralFog(num - 150f, (float)this.yPosition, xSpeed));
		}

		// Token: 0x04000A50 RID: 2640
		private int yPosition;
	}

	// Token: 0x020002E9 RID: 745
	protected class FogMaker
	{
		// Token: 0x06001BFD RID: 7165 RVA: 0x00079064 File Offset: 0x00077264
		protected virtual void populate(TextureTools.TextureData targetTexture)
		{
			float num = 0f;
			bool flag = true;
			while (num < (float)targetTexture.height)
			{
				num += (float)Random.Range(24, 50);
				float num2 = 0f;
				float num3 = (float)Random.Range(-100, MapIllustrator.ScreenDimensions.getIllustratedMapHalfWidth() * 16);
				if (flag)
				{
					flag = false;
					while ((double)num2 > -0.05)
					{
						num2 = Random.Range(-0.1f, 0f);
					}
				}
				else
				{
					flag = true;
					while ((double)num2 < 0.05)
					{
						num2 = Random.Range(0f, 0.1f);
					}
				}
				this.fogList.Add(new WeatherEffectsControl.FogMaker.ProceduralFog(num3, num, num2));
				this.fogList.Add(new WeatherEffectsControl.FogMaker.ProceduralFog(num3 + 45f, num, num2));
				this.fogList.Add(new WeatherEffectsControl.FogMaker.ProceduralFog(num3 - 45f, num, num2));
			}
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x0007913C File Offset: 0x0007733C
		public void update(TextureTools.TextureData targetTexture, int xScrollOffset, int yScrollOffset, int xScroll, int yScroll, TextureTools.TextureData punchOut)
		{
			if (this.fogList.Count == 0)
			{
				this.populate(targetTexture);
			}
			foreach (WeatherEffectsControl.FogMaker.ProceduralFog proceduralFog in this.fogList)
			{
				proceduralFog.x += proceduralFog.getXSpeed();
				proceduralFog.x += (float)xScroll;
				proceduralFog.y += (float)yScroll;
				if (proceduralFog.y > (float)targetTexture.height)
				{
					proceduralFog.y = (float)(-(float)proceduralFog.getHeight());
				}
				else if (proceduralFog.y < (float)(-(float)proceduralFog.getHeight()))
				{
					proceduralFog.y = (float)targetTexture.height;
				}
				proceduralFog.update();
				proceduralFog.draw(targetTexture, (int)proceduralFog.x + xScrollOffset, (int)proceduralFog.y + yScrollOffset, punchOut);
			}
		}

		// Token: 0x04000A51 RID: 2641
		protected List<WeatherEffectsControl.FogMaker.ProceduralFog> fogList = new List<WeatherEffectsControl.FogMaker.ProceduralFog>();

		// Token: 0x020003EE RID: 1006
		protected class Fog
		{
			// Token: 0x06001DA8 RID: 7592 RVA: 0x0007D5D7 File Offset: 0x0007B7D7
			public Fog(float _x, float _y, float _xSpeed)
			{
				this.x = _x;
				this.y = _y;
				this.xSpeed = _xSpeed;
			}

			// Token: 0x06001DA9 RID: 7593 RVA: 0x0007D5F4 File Offset: 0x0007B7F4
			public virtual int getHeight()
			{
				return 0;
			}

			// Token: 0x06001DAA RID: 7594 RVA: 0x0007D5F7 File Offset: 0x0007B7F7
			public virtual float getXSpeed()
			{
				return this.xSpeed;
			}

			// Token: 0x04000C80 RID: 3200
			public float x;

			// Token: 0x04000C81 RID: 3201
			public float y;

			// Token: 0x04000C82 RID: 3202
			private float xSpeed;
		}

		// Token: 0x020003EF RID: 1007
		protected class ProceduralFog : WeatherEffectsControl.FogMaker.Fog
		{
			// Token: 0x06001DAB RID: 7595 RVA: 0x0007D600 File Offset: 0x0007B800
			public ProceduralFog(float _x, float _y, float _xSpeed) : base(_x, _y, _xSpeed)
			{
				this.heightGlobal = (int)this.maxMagnitude * 3;
				this.width = 220;
				this.growthRate = Random.Range(0.0005f, 0.0015f);
				this.samples = new int[this.getFogLength()];
				this.carrierWaves = new WeatherEffectsControl.FogMaker.ProceduralFog.Wave[1];
				this.carrierWaves[0] = new WeatherEffectsControl.FogMaker.ProceduralFog.Wave(0f, 0.5f, 1f);
				this.noiseWaves = new WeatherEffectsControl.FogMaker.ProceduralFog.Wave[this.noise];
				for (int i = 0; i < this.noiseWaves.Length; i++)
				{
					this.noiseWaves[i] = new WeatherEffectsControl.FogMaker.ProceduralFog.Wave();
				}
			}

			// Token: 0x06001DAC RID: 7596 RVA: 0x0007D6E9 File Offset: 0x0007B8E9
			public override int getHeight()
			{
				return this.heightGlobal;
			}

			// Token: 0x06001DAD RID: 7597 RVA: 0x0007D6F4 File Offset: 0x0007B8F4
			public void update()
			{
				if (this.updateCounter > 0)
				{
					this.updateCounter--;
					return;
				}
				this.updateCounter = 5;
				this.grow();
				for (int i = 0; i < this.noiseWaves.Length; i++)
				{
					this.noiseWaves[i].update();
				}
				int num = 0;
				for (float num2 = 0f; num2 < (float)this.samples.Length; num2 += 1f)
				{
					this.samples[num] = this.getHeightAtPoint(num2 / (float)this.samples.Length);
					num++;
				}
			}

			// Token: 0x06001DAE RID: 7598 RVA: 0x0007D781 File Offset: 0x0007B981
			public override float getXSpeed()
			{
				return base.getXSpeed();
			}

			// Token: 0x06001DAF RID: 7599 RVA: 0x0007D78C File Offset: 0x0007B98C
			private void grow()
			{
				if (this.expand && this.globalMagnitude < 1f)
				{
					this.globalMagnitude += this.growthRate;
					if (this.globalMagnitude >= 1f)
					{
						this.expand = false;
						return;
					}
				}
				else if (!this.expand && (double)this.globalMagnitude > 0.75)
				{
					this.globalMagnitude -= this.growthRate;
					if ((double)this.globalMagnitude <= 0.75)
					{
						this.expand = true;
					}
				}
			}

			// Token: 0x06001DB0 RID: 7600 RVA: 0x0007D81C File Offset: 0x0007BA1C
			public void draw(TextureTools.TextureData target, int drawX, int drawY, TextureTools.TextureData punchOut)
			{
				if (drawY > target.height)
				{
					return;
				}
				int fogLength = this.getFogLength();
				int num = drawX + this.width / 2 - fogLength / 2;
				int num2 = target.width * 2;
				int num3 = 0;
				if (drawY < 0)
				{
					num3 += drawY;
					drawY = 0;
				}
				for (int i = 0; i < fogLength; i++)
				{
					int num4 = this.samples[i] + num3;
					int num5 = drawY;
					if (num5 < 0)
					{
						num4 += num5;
						num5 = 0;
					}
					if (num % 2 != 0)
					{
						num5++;
					}
					num++;
					int num6 = num;
					if (num6 > target.width)
					{
						num6 %= target.width;
					}
					else if (num6 < 0)
					{
						num6 = target.width - Mathf.Abs(num6) % target.width;
					}
					int num7 = num5 * target.width + num6;
					int num8 = num5 + num4;
					int num9 = num5;
					while (num9 < num8 && num7 < target.colors.Length)
					{
						if (punchOut == null || punchOut.isPixelTransparent(num7))
						{
							target.colors[num7] = this.color;
						}
						num7 += num2;
						num9 += 2;
					}
				}
			}

			// Token: 0x06001DB1 RID: 7601 RVA: 0x0007D934 File Offset: 0x0007BB34
			private float getMaxMagnitude()
			{
				return this.maxMagnitude * this.globalMagnitude;
			}

			// Token: 0x06001DB2 RID: 7602 RVA: 0x0007D943 File Offset: 0x0007BB43
			private int getFogLength()
			{
				return this.width;
			}

			// Token: 0x06001DB3 RID: 7603 RVA: 0x0007D94C File Offset: 0x0007BB4C
			private int getHeightAtPoint(float point)
			{
				float carrierWaveAtPoint = this.getCarrierWaveAtPoint(point);
				if ((double)carrierWaveAtPoint < 0.1)
				{
					return 0;
				}
				float num = 1f;
				for (int i = 0; i < this.noiseWaves.Length; i++)
				{
					num *= this.noiseWaves[i].getValue(point);
				}
				num = carrierWaveAtPoint * (this.getMaxMagnitude() + this.getMaxMagnitude() * num);
				return Mathf.RoundToInt(num);
			}

			// Token: 0x06001DB4 RID: 7604 RVA: 0x0007D9B4 File Offset: 0x0007BBB4
			private float getCarrierWaveAtPoint(float point)
			{
				float num = 1f;
				for (int i = 0; i < this.carrierWaves.Length; i++)
				{
					num *= this.carrierWaves[i].getValue(point);
				}
				return Mathf.Pow(num, 3f);
			}

			// Token: 0x04000C83 RID: 3203
			private float globalMagnitude = 0.5f;

			// Token: 0x04000C84 RID: 3204
			private float maxMagnitude = 24f;

			// Token: 0x04000C85 RID: 3205
			private float growthRate = 0.05f;

			// Token: 0x04000C86 RID: 3206
			private int heightGlobal;

			// Token: 0x04000C87 RID: 3207
			private int width;

			// Token: 0x04000C88 RID: 3208
			private bool expand = true;

			// Token: 0x04000C89 RID: 3209
			private int noise = 6;

			// Token: 0x04000C8A RID: 3210
			protected Color32 color = C64Color.Blue;

			// Token: 0x04000C8B RID: 3211
			private WeatherEffectsControl.FogMaker.ProceduralFog.Wave[] noiseWaves;

			// Token: 0x04000C8C RID: 3212
			private WeatherEffectsControl.FogMaker.ProceduralFog.Wave[] carrierWaves;

			// Token: 0x04000C8D RID: 3213
			private int[] samples;

			// Token: 0x04000C8E RID: 3214
			private int updateCounter;

			// Token: 0x02000435 RID: 1077
			private class Wave
			{
				// Token: 0x06001E08 RID: 7688 RVA: 0x0007DFB4 File Offset: 0x0007C1B4
				public Wave()
				{
					this.phase = Random.Range(0f, 6.2831855f);
					this.frequency = Random.Range(2f, 6f);
					this.weight = 1f;
					this.delta = Random.Range(-0.015f, 0.015f);
					this.TWO_PI_TIMES_FREQUENCY = 6.2831855f * this.frequency;
				}

				// Token: 0x06001E09 RID: 7689 RVA: 0x0007E030 File Offset: 0x0007C230
				public Wave(float _phase, float _frequency, float _weight)
				{
					this.delta = 0f;
					this.phase = _phase;
					this.frequency = _frequency;
					this.weight = _weight;
					this.TWO_PI_TIMES_FREQUENCY = 6.2831855f * this.frequency;
				}

				// Token: 0x06001E0A RID: 7690 RVA: 0x0007E080 File Offset: 0x0007C280
				public void update()
				{
					this.phase += this.delta;
				}

				// Token: 0x06001E0B RID: 7691 RVA: 0x0007E095 File Offset: 0x0007C295
				public float getValue(float atPoint)
				{
					return this.weight * Mathf.Sin(this.phase + this.TWO_PI_TIMES_FREQUENCY * atPoint);
				}

				// Token: 0x04000DA0 RID: 3488
				private float phase;

				// Token: 0x04000DA1 RID: 3489
				private float frequency;

				// Token: 0x04000DA2 RID: 3490
				private float delta;

				// Token: 0x04000DA3 RID: 3491
				private float weight = 1f;

				// Token: 0x04000DA4 RID: 3492
				private const float TWO_PI = 6.2831855f;

				// Token: 0x04000DA5 RID: 3493
				private float TWO_PI_TIMES_FREQUENCY;
			}
		}
	}

	// Token: 0x020002EA RID: 746
	private class RainMaker
	{
		// Token: 0x06001C01 RID: 7169 RVA: 0x000792D8 File Offset: 0x000774D8
		public void setSplashOnColor()
		{
			this.splashOnColor = true;
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x000792E1 File Offset: 0x000774E1
		public void setDuration(int i)
		{
			this.duration = i;
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x000792EA File Offset: 0x000774EA
		public int getDuration()
		{
			return this.duration;
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x000792F2 File Offset: 0x000774F2
		public bool isItRaining()
		{
			return this.duration > 0;
		}

		// Token: 0x06001C05 RID: 7173 RVA: 0x00079300 File Offset: 0x00077500
		public void applyRain(TextureTools.TextureData input, TextureTools.TextureData punchOut)
		{
			if (this.duration == 0 && this.rainList.Count == 0)
			{
				return;
			}
			if (this.currentDrops < (float)this.maxDrops && this.duration != 0)
			{
				this.currentDrops += this.rainIncrement;
			}
			if (this.duration > 0)
			{
				this.duration--;
			}
			else
			{
				this.currentDrops -= this.rainIncrement;
			}
			if ((float)this.rainList.Count < this.currentDrops)
			{
				this.addRaindrop(input.width, input.height);
			}
			List<WeatherEffectsControl.RainMaker.RainDrop> list = new List<WeatherEffectsControl.RainMaker.RainDrop>();
			if (this.rainList.Count != 0)
			{
				foreach (WeatherEffectsControl.RainMaker.RainDrop rainDrop in this.rainList)
				{
					rainDrop.y += this.ySpeed;
					rainDrop.x += this.xSpeed;
					rainDrop.life--;
					if (rainDrop.life <= 0 || rainDrop.y < 0 || rainDrop.y + this.dropSize >= input.height || rainDrop.x - this.dropSize < 0 || rainDrop.x >= input.width)
					{
						list.Add(rainDrop);
						if (rainDrop.life > 0 || (this.splashOnColor && (!this.splashOnColor || input.isPixelTransparent(rainDrop.x, rainDrop.y))))
						{
							continue;
						}
						Color32 white = C64Color.White;
						try
						{
							if (punchOut == null || (punchOut != null && punchOut.isPixelTransparent(rainDrop.x, rainDrop.y)))
							{
								for (int i = 0; i < 6; i++)
								{
									input.SetPixel(rainDrop.x + Random.Range(-4, 4), rainDrop.y + Random.Range(0, 4), white);
								}
							}
							continue;
						}
						catch (Exception obj)
						{
							MainControl.logError(obj);
							continue;
						}
					}
					int num = rainDrop.x + rainDrop.y * input.width;
					for (int j = 0; j < this.dropSize; j++)
					{
						if (this.xSpeed == 0)
						{
							num -= input.width;
						}
						else
						{
							num -= input.width - 1;
						}
						if (num >= 0 && num < input.colors.Length && (punchOut == null || punchOut.isPixelTransparent(num)))
						{
							input.colors[num] = rainDrop.color;
						}
					}
				}
				if (list.Count != 0)
				{
					foreach (WeatherEffectsControl.RainMaker.RainDrop item in list)
					{
						this.rainList.Remove(item);
					}
				}
			}
		}

		// Token: 0x06001C06 RID: 7174 RVA: 0x00079610 File Offset: 0x00077810
		private void addRaindrop(int w, int h)
		{
			int x = Random.Range(0, w);
			int y = Random.Range(0, h);
			Color32 color = this.colorArray[Random.Range(0, this.colorArray.Length)];
			this.rainList.Add(new WeatherEffectsControl.RainMaker.RainDrop(x, y, color));
		}

		// Token: 0x04000A52 RID: 2642
		private int duration;

		// Token: 0x04000A53 RID: 2643
		private int maxDrops = 30;

		// Token: 0x04000A54 RID: 2644
		private float currentDrops;

		// Token: 0x04000A55 RID: 2645
		private float rainIncrement = 0.05f;

		// Token: 0x04000A56 RID: 2646
		private int dropSize = 16;

		// Token: 0x04000A57 RID: 2647
		private int xSpeed = 6;

		// Token: 0x04000A58 RID: 2648
		private int ySpeed = -6;

		// Token: 0x04000A59 RID: 2649
		private bool splashOnColor;

		// Token: 0x04000A5A RID: 2650
		private Color32[] colorArray = new Color32[]
		{
			C64Color.GrayLight,
			C64Color.Gray,
			C64Color.GrayDark,
			C64Color.White,
			C64Color.Cyan
		};

		// Token: 0x04000A5B RID: 2651
		private List<WeatherEffectsControl.RainMaker.RainDrop> rainList = new List<WeatherEffectsControl.RainMaker.RainDrop>();

		// Token: 0x020003F0 RID: 1008
		private class RainDrop
		{
			// Token: 0x06001DB5 RID: 7605 RVA: 0x0007D9F6 File Offset: 0x0007BBF6
			public RainDrop(int _x, int _y, Color32 _color)
			{
				this.x = _x;
				this.y = _y;
				this.color = _color;
				this.life = 8;
			}

			// Token: 0x04000C8F RID: 3215
			public Color32 color;

			// Token: 0x04000C90 RID: 3216
			public int x;

			// Token: 0x04000C91 RID: 3217
			public int y;

			// Token: 0x04000C92 RID: 3218
			public int life;
		}
	}
}
