using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000195 RID: 405
public class ParticleSystem
{
	// Token: 0x06001503 RID: 5379 RVA: 0x0005D330 File Offset: 0x0005B530
	public ParticleSystem()
	{
		this.emitterList = new List<ParticleSystem.Emitter>();
	}

	// Token: 0x06001504 RID: 5380 RVA: 0x0005D344 File Offset: 0x0005B544
	public void setLife(int life)
	{
		foreach (ParticleSystem.Emitter emitter in this.emitterList)
		{
			emitter.setLife(life);
		}
	}

	// Token: 0x06001505 RID: 5381 RVA: 0x0005D398 File Offset: 0x0005B598
	public bool areAnyPerpetualEffectsActive()
	{
		using (List<ParticleSystem.Emitter>.Enumerator enumerator = this.emitterList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isPerpetual())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001506 RID: 5382 RVA: 0x0005D3F4 File Offset: 0x0005B5F4
	public void update(int xOffset, int yOffset, TextureTools.TextureData textureData)
	{
		List<ParticleSystem.Emitter> list = new List<ParticleSystem.Emitter>();
		foreach (ParticleSystem.Emitter emitter in this.emitterList)
		{
			if (emitter.isDead())
			{
				list.Add(emitter);
			}
			else
			{
				emitter.update(xOffset, yOffset, textureData);
			}
		}
		foreach (ParticleSystem.Emitter item in list)
		{
			this.emitterList.Remove(item);
		}
	}

	// Token: 0x06001507 RID: 5383 RVA: 0x0005D4A4 File Offset: 0x0005B6A4
	public float getLightDistance()
	{
		float num = 0f;
		foreach (ParticleSystem.Emitter emitter in this.emitterList)
		{
			if (!emitter.isDead() && emitter.getLightDistance() > num)
			{
				num = emitter.getLightDistance();
			}
		}
		return num;
	}

	// Token: 0x06001508 RID: 5384 RVA: 0x0005D510 File Offset: 0x0005B710
	public float getLightStrength()
	{
		float num = 0f;
		foreach (ParticleSystem.Emitter emitter in this.emitterList)
		{
			if (!emitter.isDead() && emitter.getLightStrength() > num)
			{
				num = emitter.getLightStrength();
			}
		}
		return num;
	}

	// Token: 0x06001509 RID: 5385 RVA: 0x0005D57C File Offset: 0x0005B77C
	public bool shouldYouWaitForParticlesToFinish()
	{
		using (List<ParticleSystem.Emitter>.Enumerator enumerator = this.emitterList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.shouldYouWaitForMeToFinish())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600150A RID: 5386 RVA: 0x0005D5D8 File Offset: 0x0005B7D8
	public bool areAnyEffectsActive()
	{
		using (List<ParticleSystem.Emitter>.Enumerator enumerator = this.emitterList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.isDead())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600150B RID: 5387 RVA: 0x0005D634 File Offset: 0x0005B834
	public void addParticles(int size, Color32 col, float x, float y, float xSpeed, float ySpeed, float _xGravity, float _yGravity, int _life, float _ground)
	{
		ParticleSystem.ParticleEmitter item = new ParticleSystem.ParticleEmitter(size, col, x, y, xSpeed, ySpeed, _xGravity, _yGravity, _life, _ground);
		this.emitterList.Add(item);
	}

	// Token: 0x0600150C RID: 5388 RVA: 0x0005D664 File Offset: 0x0005B864
	public void addParticlesSparks(float x, float y)
	{
		this.addParticles(5, C64Color.Yellow, x, y, 0.75f, 0.75f, 0f, -0.02f, 30, y - 10f);
		this.addParticles(5, C64Color.Cyan, x, y, 0.75f, 0.75f, 0f, -0.02f, 30, y - 10f);
		this.addParticles(5, C64Color.White, x, y, 0.75f, 0.75f, 0f, -0.02f, 30, y - 10f);
	}

	// Token: 0x0600150D RID: 5389 RVA: 0x0005D6F4 File Offset: 0x0005B8F4
	public void addSweat(float x, float y)
	{
		this.addParticles(2, C64Color.Blue, x, y, 0.75f, 0.75f, 0f, -0.05f, 20, y - 10f);
		this.addParticles(2, C64Color.Cyan, x, y, 0.75f, 0.75f, 0f, -0.05f, 20, y - 10f);
		this.addParticles(2, C64Color.White, x, y, 0.75f, 0.75f, 0f, -0.05f, 20, y - 10f);
	}

	// Token: 0x0600150E RID: 5390 RVA: 0x0005D784 File Offset: 0x0005B984
	public void addParticlesBlood(float x, float y)
	{
		this.addParticles(10, C64Color.Red, x, y, 0.75f, 0.75f, 0f, -0.05f, 35, y - 5f);
		this.addParticles(4, C64Color.RedLight, x, y, 0.75f, 0.75f, 0f, -0.05f, 35, y - 5f);
	}

	// Token: 0x0600150F RID: 5391 RVA: 0x0005D7E8 File Offset: 0x0005B9E8
	public void addLightning(int xStart, int yStart, int xTarget, int yTarget)
	{
		this.addParticlesSparks((float)xTarget, (float)yTarget);
		ParticleSystem.LightningEmitter item = new ParticleSystem.LightningEmitter(xStart, yStart, xTarget, yTarget);
		this.emitterList.Add(item);
		item = new ParticleSystem.LightningEmitter(xStart, yStart, xTarget, yTarget);
		this.emitterList.Add(item);
		item = new ParticleSystem.LightningEmitter(xStart, yStart, xTarget, yTarget);
		this.emitterList.Add(item);
	}

	// Token: 0x06001510 RID: 5392 RVA: 0x0005D848 File Offset: 0x0005BA48
	public void addArrow(int xStart, int yStart, int xTarget, int yTarget)
	{
		ParticleSystem.Arrow item = new ParticleSystem.Arrow(xStart, yStart, xTarget, yTarget);
		this.emitterList.Add(item);
	}

	// Token: 0x06001511 RID: 5393 RVA: 0x0005D86C File Offset: 0x0005BA6C
	public void addRay(int xStart, int yStart, int xTarget, int yTarget)
	{
		ParticleSystem.Ray item = new ParticleSystem.Ray(xStart, yStart, xTarget, yTarget);
		this.addParticlesSparks((float)xTarget, (float)yTarget);
		this.emitterList.Add(item);
	}

	// Token: 0x06001512 RID: 5394 RVA: 0x0005D89B File Offset: 0x0005BA9B
	public void addSmokeMedium(int xStart, int yStart, int life)
	{
		this.emitterList.Add(new ParticleSystem.SmokeControl(life, 2, xStart, yStart));
	}

	// Token: 0x06001513 RID: 5395 RVA: 0x0005D8B1 File Offset: 0x0005BAB1
	public void addFireFlashMedium(int xStart, int yStart)
	{
		this.emitterList.Add(new ParticleSystem.FlameFlashMediumControl(xStart, yStart));
	}

	// Token: 0x06001514 RID: 5396 RVA: 0x0005D8C5 File Offset: 0x0005BAC5
	public void addFireFlashTiny(int xStart, int yStart)
	{
		this.emitterList.Add(new ParticleSystem.TinyExplosionControl(xStart, yStart));
	}

	// Token: 0x06001515 RID: 5397 RVA: 0x0005D8D9 File Offset: 0x0005BAD9
	public void addFireball(int xStart, int yStart)
	{
		this.emitterList.Add(new ParticleSystem.FireballControl(xStart, yStart));
	}

	// Token: 0x06001516 RID: 5398 RVA: 0x0005D8ED File Offset: 0x0005BAED
	public void addSprayFireLine(int xStart, int yStart, int xTarget, int yTarget)
	{
		this.emitterList.Add(new ParticleSystem.SprayFireLineControl(xStart, yStart, xTarget, yTarget));
	}

	// Token: 0x06001517 RID: 5399 RVA: 0x0005D904 File Offset: 0x0005BB04
	public void addSprayAcidLine(int xStart, int yStart, int xTarget, int yTarget)
	{
		this.emitterList.Add(new ParticleSystem.SprayColorLineControl(xStart, yStart, xTarget, yTarget, ParticleSystem.acidColors));
	}

	// Token: 0x06001518 RID: 5400 RVA: 0x0005D920 File Offset: 0x0005BB20
	public void addSprayIceLine(int xStart, int yStart, int xTarget, int yTarget)
	{
		this.emitterList.Add(new ParticleSystem.SprayColorLineControl(xStart, yStart, xTarget, yTarget, ParticleSystem.waterColors));
	}

	// Token: 0x06001519 RID: 5401 RVA: 0x0005D93C File Offset: 0x0005BB3C
	public void addSprayAcidCone(int xStart, int yStart, int xTarget, int yTarget)
	{
		this.emitterList.Add(new ParticleSystem.SprayColorConeControl(xStart, yStart, xTarget, yTarget, ParticleSystem.acidColors));
	}

	// Token: 0x0600151A RID: 5402 RVA: 0x0005D958 File Offset: 0x0005BB58
	public void addSprayIceCone(int xStart, int yStart, int xTarget, int yTarget)
	{
		this.emitterList.Add(new ParticleSystem.SprayColorConeControl(xStart, yStart, xTarget, yTarget, ParticleSystem.waterColors));
	}

	// Token: 0x0600151B RID: 5403 RVA: 0x0005D974 File Offset: 0x0005BB74
	public void addSprayFireCone(int xStart, int yStart, int xTarget, int yTarget)
	{
		this.emitterList.Add(new ParticleSystem.SprayFireConeControl(xStart, yStart, xTarget, yTarget));
	}

	// Token: 0x0600151C RID: 5404 RVA: 0x0005D98B File Offset: 0x0005BB8B
	public void addAuraGreen(int xStart, int yStart)
	{
		this.emitterList.Add(new ParticleSystem.AuraControl(xStart, yStart, ParticleSystem.positiveColors));
	}

	// Token: 0x0600151D RID: 5405 RVA: 0x0005D9A4 File Offset: 0x0005BBA4
	public void addAuraRed(int xStart, int yStart)
	{
		this.emitterList.Add(new ParticleSystem.AuraControl(xStart, yStart, ParticleSystem.negativeColors));
	}

	// Token: 0x0600151E RID: 5406 RVA: 0x0005D9BD File Offset: 0x0005BBBD
	public void addSplashAcid(int xStart, int yStart)
	{
		this.emitterList.Add(new ParticleSystem.SmallColoredSplashControl(xStart, yStart, 30, ParticleSystem.acidColors));
	}

	// Token: 0x0600151F RID: 5407 RVA: 0x0005D9D8 File Offset: 0x0005BBD8
	public void addAuraIce(int xStart, int yStart)
	{
		this.emitterList.Add(new ParticleSystem.AuraControl(xStart, yStart, ParticleSystem.waterColors));
	}

	// Token: 0x06001520 RID: 5408 RVA: 0x0005D9F1 File Offset: 0x0005BBF1
	public void addSplashPoison(int xStart, int yStart)
	{
		this.emitterList.Add(new ParticleSystem.SmallColoredSplashControl(xStart, yStart, 30, ParticleSystem.poisonColors));
	}

	// Token: 0x06001521 RID: 5409 RVA: 0x0005DA0C File Offset: 0x0005BC0C
	public void addFlameContinualLarge(int xStart, int yStart)
	{
		this.emitterList.Add(new ParticleSystem.FlameContinualLargeControl(xStart, yStart));
	}

	// Token: 0x06001522 RID: 5410 RVA: 0x0005DA20 File Offset: 0x0005BC20
	public void addFlameContinualMedium(int xStart, int yStart)
	{
		this.emitterList.Add(new ParticleSystem.FlameContinualMediumControl(xStart, yStart));
	}

	// Token: 0x06001523 RID: 5411 RVA: 0x0005DA34 File Offset: 0x0005BC34
	public void addFlameContinualMediumGreen(int xStart, int yStart)
	{
		this.emitterList.Add(new ParticleSystem.FlameContinualMediumControlGreen(xStart, yStart));
	}

	// Token: 0x06001524 RID: 5412 RVA: 0x0005DA48 File Offset: 0x0005BC48
	public void addFlameContinualMediumPurple(int xStart, int yStart)
	{
		this.emitterList.Add(new ParticleSystem.FlameContinualMediumControlPurple(xStart, yStart));
	}

	// Token: 0x06001525 RID: 5413 RVA: 0x0005DA5C File Offset: 0x0005BC5C
	public void addFountainMedium(int xStart, int yStart)
	{
		this.emitterList.Add(new ParticleSystem.FountainMediumControl(xStart, yStart));
	}

	// Token: 0x06001526 RID: 5414 RVA: 0x0005DA70 File Offset: 0x0005BC70
	public void addFlameContinualSmall(int xStart, int yStart)
	{
		this.emitterList.Add(new ParticleSystem.FlameContinualSmallControl(xStart, yStart));
	}

	// Token: 0x06001527 RID: 5415 RVA: 0x0005DA84 File Offset: 0x0005BC84
	public void addFlameContinualTiny(int xStart, int yStart)
	{
		this.emitterList.Add(new ParticleSystem.FlameContinualTinyControl(xStart, yStart));
	}

	// Token: 0x06001528 RID: 5416 RVA: 0x0005DA98 File Offset: 0x0005BC98
	public void addHomingParticle(int xStart, int yStart, int xTarget, int yTarget)
	{
		ParticleSystem.HomingParticleEmitter item = new ParticleSystem.HomingParticleEmitter(xStart, yStart, xTarget, yTarget);
		this.emitterList.Add(item);
	}

	// Token: 0x06001529 RID: 5417 RVA: 0x0005DABC File Offset: 0x0005BCBC
	public void addFireflyParticle(int xStart, int yStart)
	{
		ParticleSystem.FireFlyEmitter item = new ParticleSystem.FireFlyEmitter(xStart, yStart, xStart, yStart);
		this.emitterList.Add(item);
	}

	// Token: 0x0600152A RID: 5418 RVA: 0x0005DAE0 File Offset: 0x0005BCE0
	public void addCorpseflyParticle(int xStart, int yStart)
	{
		ParticleSystem.CorpseFlyEmitter item = new ParticleSystem.CorpseFlyEmitter(xStart, yStart, xStart, yStart);
		this.emitterList.Add(item);
	}

	// Token: 0x04000562 RID: 1378
	private List<ParticleSystem.Emitter> emitterList;

	// Token: 0x04000563 RID: 1379
	private static Color32[] acidColors = new Color32[]
	{
		C64Color.Green,
		C64Color.GreenLight,
		C64Color.Yellow,
		C64Color.Cyan
	};

	// Token: 0x04000564 RID: 1380
	private static Color32[] poisonColors = new Color32[]
	{
		C64Color.Green,
		C64Color.Yellow,
		C64Color.Brown,
		C64Color.BrownLight
	};

	// Token: 0x04000565 RID: 1381
	private static Color32[] fireColors = new Color32[]
	{
		C64Color.White,
		C64Color.Yellow,
		C64Color.Yellow,
		C64Color.Yellow,
		C64Color.RedLight,
		C64Color.Red
	};

	// Token: 0x04000566 RID: 1382
	private static Color32[] fireGreenColors = new Color32[]
	{
		C64Color.White,
		C64Color.Yellow,
		C64Color.GreenLight,
		C64Color.GreenLight,
		C64Color.Green,
		C64Color.BlueLight
	};

	// Token: 0x04000567 RID: 1383
	private static Color32[] firePurpleColors = new Color32[]
	{
		C64Color.White,
		C64Color.RedLight,
		C64Color.RedLight,
		C64Color.Violet,
		C64Color.Violet,
		C64Color.Red
	};

	// Token: 0x04000568 RID: 1384
	private static Color32[] lightningColors = new Color32[]
	{
		C64Color.White,
		C64Color.Blue,
		C64Color.Cyan,
		C64Color.Yellow
	};

	// Token: 0x04000569 RID: 1385
	private static Color32[] smokeColors = new Color32[]
	{
		C64Color.Black,
		C64Color.GrayDark,
		C64Color.Gray,
		C64Color.GrayLight,
		C64Color.White
	};

	// Token: 0x0400056A RID: 1386
	public static Color32[] negativeColors = new Color32[]
	{
		C64Color.White,
		C64Color.Violet,
		C64Color.Blue,
		C64Color.Red,
		C64Color.RedLight
	};

	// Token: 0x0400056B RID: 1387
	public static Color32[] positiveColors = new Color32[]
	{
		C64Color.White,
		C64Color.BlueLight,
		C64Color.Cyan,
		C64Color.GreenLight
	};

	// Token: 0x0400056C RID: 1388
	private static Color32[] waterColors = new Color32[]
	{
		C64Color.Cyan,
		C64Color.BlueLight,
		C64Color.GrayLight,
		C64Color.White
	};

	// Token: 0x020002C3 RID: 707
	private class ParticleEmitter : ParticleSystem.BaseParticleEmitter
	{
		// Token: 0x06001B9F RID: 7071 RVA: 0x000779B8 File Offset: 0x00075BB8
		public ParticleEmitter(int size, Color32 col, float x, float y, float xSpeed, float ySpeed, float _xGravity, float _yGravity, int _life, float _ground)
		{
			this.particleArray = new float[size * 4];
			this.color = col;
			this.xGravity = _xGravity;
			this.yGravity = _yGravity;
			base.setLife(_life);
			this.ground = _ground;
			for (int i = 0; i < this.particleArray.Length - 4; i += 4)
			{
				base.setParticleData(i, Random.Range(0f - xSpeed, xSpeed), Random.Range(0f, ySpeed), x, y);
			}
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x00077A3C File Offset: 0x00075C3C
		public override void update(int xOffset, int yOffset, TextureTools.TextureData textureData)
		{
			this.testLife();
			for (int i = 0; i < this.particleArray.Length - 4; i += 4)
			{
				float num = this.particleArray[i];
				float num2 = this.particleArray[i + 1];
				float num3 = this.particleArray[i + 2];
				float num4 = this.particleArray[i + 3];
				num += this.xGravity;
				num3 += num;
				if (num4 > this.ground)
				{
					num2 += this.yGravity;
					num4 += num2;
				}
				textureData.SetPixel(Mathf.RoundToInt(num3) - xOffset, Mathf.RoundToInt(num4) - yOffset, this.color);
				base.setParticleData(i, num, num2, num3, num4);
			}
		}
	}

	// Token: 0x020002C4 RID: 708
	private abstract class EmitterControl : ParticleSystem.Emitter
	{
		// Token: 0x06001BA1 RID: 7073 RVA: 0x00077AE1 File Offset: 0x00075CE1
		protected EmitterControl(int life, int density, int xStart, int yStart)
		{
			base.setLife(life);
			this.density = density;
			this.x = xStart;
			this.y = yStart;
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x00077B14 File Offset: 0x00075D14
		public override bool isDead()
		{
			if (!base.isDead())
			{
				return false;
			}
			using (List<ParticleSystem.Emitter>.Enumerator enumerator = this.emitters.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.isDead())
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x00077B78 File Offset: 0x00075D78
		public override void update(int xOffset, int yOffset, TextureTools.TextureData textureData)
		{
			this.testLife();
			List<ParticleSystem.Emitter> list = new List<ParticleSystem.Emitter>();
			this.updateEmitterNumber();
			foreach (ParticleSystem.Emitter emitter in this.emitters)
			{
				if (emitter.isDead())
				{
					list.Add(emitter);
				}
				else
				{
					emitter.update(xOffset, yOffset, textureData);
				}
			}
			foreach (ParticleSystem.Emitter item in list)
			{
				this.emitters.Remove(item);
			}
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x00077C34 File Offset: 0x00075E34
		protected virtual void updateEmitterNumber()
		{
			if (!base.isDead())
			{
				for (int i = 0; i < this.density; i++)
				{
					this.addEmitter();
				}
			}
		}

		// Token: 0x06001BA5 RID: 7077
		protected abstract void addEmitter();

		// Token: 0x04000A1E RID: 2590
		protected List<ParticleSystem.Emitter> emitters = new List<ParticleSystem.Emitter>();

		// Token: 0x04000A1F RID: 2591
		protected int density;
	}

	// Token: 0x020002C5 RID: 709
	private class SmokeControl : ParticleSystem.EmitterControl
	{
		// Token: 0x06001BA6 RID: 7078 RVA: 0x00077C60 File Offset: 0x00075E60
		public SmokeControl(int life, int density, int xStart, int yStart) : base(life, density, xStart, yStart)
		{
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x00077C6D File Offset: 0x00075E6D
		protected override void addEmitter()
		{
			if (!base.isDead())
			{
				this.emitters.Add(new ParticleSystem.SmokeControl.SmokeEmitter((float)this.x, (float)this.y));
			}
		}

		// Token: 0x020003E1 RID: 993
		private class SmokeEmitter : ParticleSystem.RisingParticleEmitter
		{
			// Token: 0x06001D9A RID: 7578 RVA: 0x0007CDF0 File Offset: 0x0007AFF0
			public SmokeEmitter(float x, float y)
			{
				this.particleArray = new float[8];
				this.colorList = ParticleSystem.smokeColors;
				this.rotation = Random.Range(0f, 6.2831855f);
				base.setLife(Random.Range(15, 60));
				this.cycleColor();
				for (int i = 0; i < this.particleArray.Length - 4; i += 4)
				{
					this.particleArray[i] = Random.Range(-0.5f, 0.8f);
					this.particleArray[i + 1] = Random.Range(0.1f, 0.6f);
					this.particleArray[i + 2] = x + (float)Random.Range(-2, 2);
					this.particleArray[i + 3] = y;
				}
			}

			// Token: 0x06001D9B RID: 7579 RVA: 0x0007CEAC File Offset: 0x0007B0AC
			public override void update(int xOffset, int yOffset, TextureTools.TextureData textureData)
			{
				this.testLife();
				this.cycleColor();
				this.rotation = (this.rotation + 0.1f) % 6.2831855f;
				for (int i = 0; i < this.particleArray.Length - 4; i += 4)
				{
					float num = this.particleArray[i];
					float num2 = this.particleArray[i + 1];
					float num3 = this.particleArray[i + 2];
					float num4 = this.particleArray[i + 3];
					num3 += num + 0.5f * Mathf.Sin(this.rotation);
					num4 += num2;
					if ((double)num > 0.2)
					{
						num -= 0.05f;
					}
					if ((double)num < 0.2)
					{
						num += 0.05f;
					}
					textureData.SetPixel(Mathf.RoundToInt(num3) - xOffset, Mathf.RoundToInt(num4) - yOffset, this.color);
					this.particleArray[i] = num;
					this.particleArray[i + 1] = num2;
					this.particleArray[i + 2] = num3;
					this.particleArray[i + 3] = num4;
				}
			}
		}
	}

	// Token: 0x020002C6 RID: 710
	private abstract class FireControl : ParticleSystem.EmitterControl
	{
		// Token: 0x06001BA8 RID: 7080 RVA: 0x00077C95 File Offset: 0x00075E95
		public FireControl(int life, int density, int xStart, int yStart) : base(life, density, xStart, yStart)
		{
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x00077CA2 File Offset: 0x00075EA2
		protected override void addEmitter()
		{
			this.emitters.Add(new ParticleSystem.FireControl.FireEmitter(this.x, this.y));
		}

		// Token: 0x020003E2 RID: 994
		protected class FireEmitterGreen : ParticleSystem.FireControl.FireEmitter
		{
			// Token: 0x06001D9C RID: 7580 RVA: 0x0007CFB1 File Offset: 0x0007B1B1
			public FireEmitterGreen(int x, int y) : base(x, y)
			{
				this.colorList = ParticleSystem.fireGreenColors;
			}
		}

		// Token: 0x020003E3 RID: 995
		protected class FireEmitterPurple : ParticleSystem.FireControl.FireEmitter
		{
			// Token: 0x06001D9D RID: 7581 RVA: 0x0007CFC6 File Offset: 0x0007B1C6
			public FireEmitterPurple(int x, int y) : base(x, y)
			{
				this.colorList = ParticleSystem.firePurpleColors;
			}
		}

		// Token: 0x020003E4 RID: 996
		protected class FireEmitter : ParticleSystem.RisingParticleEmitter
		{
			// Token: 0x06001D9E RID: 7582 RVA: 0x0007CFDC File Offset: 0x0007B1DC
			public FireEmitter(int x, int y)
			{
				this.x = x;
				this.y = y + Random.Range(2, -3);
				this.updateFrequency = 1;
				this.colorList = ParticleSystem.fireColors;
				this.rotation = Random.Range(0f, 6.2831855f);
				this.rotationSpeed = 0.2f;
				this.rotationAmplitude = 0.05f;
				this.drag = 0.1f;
				base.setLife(Random.Range(20, 55));
				this.xSpeedVariance = 0.5f;
				this.arraySize = 5;
				this.riseSpeed = 0.2f;
				this.baseWidth = 3f;
				this.cycleColor();
				base.populateArray();
			}
		}
	}

	// Token: 0x020002C7 RID: 711
	private class FlameFlashMediumControl : ParticleSystem.FireControl
	{
		// Token: 0x06001BAA RID: 7082 RVA: 0x00077CC0 File Offset: 0x00075EC0
		public FlameFlashMediumControl(int xStart, int yStart) : base(60, 3, xStart, yStart)
		{
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x00077CCD File Offset: 0x00075ECD
		protected override void addEmitter()
		{
			this.emitters.Add(new ParticleSystem.FireControl.FireEmitter(this.x, this.y));
		}
	}

	// Token: 0x020002C8 RID: 712
	private class MediumExplosionControl : ParticleSystem.FireControl
	{
		// Token: 0x06001BAC RID: 7084 RVA: 0x00077CEB File Offset: 0x00075EEB
		public MediumExplosionControl(int xStart, int yStart) : base(20, 3, xStart, yStart)
		{
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x00077CF8 File Offset: 0x00075EF8
		protected override void addEmitter()
		{
			this.emitters.Add(new ParticleSystem.MediumExplosionControl.MediumExplosion(this.x, this.y));
		}

		// Token: 0x020003E5 RID: 997
		protected class MediumExplosion : ParticleSystem.FireControl.FireEmitter
		{
			// Token: 0x06001D9F RID: 7583 RVA: 0x0007D094 File Offset: 0x0007B294
			public MediumExplosion(int x, int y) : base(x, y)
			{
				this.colorList = ParticleSystem.fireColors;
				this.updateFrequency = 0;
				this.riseSpeed = 0.6f;
				this.xSpeedVariance = 0.4f;
				this.drag = 0f;
				base.setLife(Random.Range(5, 30));
				this.yGravity = -0.02f;
				this.arraySize = 5;
				this.cycleColor();
				base.populateArray();
			}
		}
	}

	// Token: 0x020002C9 RID: 713
	private class TinyExplosionControl : ParticleSystem.FireControl
	{
		// Token: 0x06001BAE RID: 7086 RVA: 0x00077D16 File Offset: 0x00075F16
		public TinyExplosionControl(int xStart, int yStart) : base(5, 1, xStart, yStart)
		{
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x00077D22 File Offset: 0x00075F22
		protected override void addEmitter()
		{
			this.emitters.Add(new ParticleSystem.TinyExplosionControl.SmallExplosion(this.x, this.y));
		}

		// Token: 0x020003E6 RID: 998
		protected class SmallExplosion : ParticleSystem.FireControl.FireEmitter
		{
			// Token: 0x06001DA0 RID: 7584 RVA: 0x0007D108 File Offset: 0x0007B308
			public SmallExplosion(int x, int y) : base(x, y)
			{
				this.x = x;
				this.y = y;
				this.rotation = Random.Range(0f, 6.2831855f);
				this.rotationSpeed = 0.2f;
				this.rotationAmplitude = 0.05f;
				this.drag = 0.1f;
				base.setLife(Random.Range(5, 15));
				this.xSpeedVariance = 0.05f;
				this.arraySize = 3;
				this.riseSpeed = 0.05f;
				this.baseWidth = 1f;
				this.cycleColor();
				base.populateArray();
			}
		}
	}

	// Token: 0x020002CA RID: 714
	private class SmallColoredSplashControl : ParticleSystem.FireControl
	{
		// Token: 0x06001BB0 RID: 7088 RVA: 0x00077D40 File Offset: 0x00075F40
		public SmallColoredSplashControl(int xStart, int yStart, int duration, Color32[] colorList) : base(duration, 3, xStart, yStart)
		{
			this.colorList = colorList;
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x00077D54 File Offset: 0x00075F54
		public SmallColoredSplashControl(int xStart, int yStart, Color32[] colorList) : base(10, 3, xStart, yStart)
		{
			this.colorList = colorList;
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x00077D68 File Offset: 0x00075F68
		protected override void addEmitter()
		{
			this.emitters.Add(new ParticleSystem.SmallColoredSplashControl.SmallColoredSplash(this.x, this.y, this.colorList));
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x00077D8C File Offset: 0x00075F8C
		protected override void cycleColor()
		{
			this.color = this.colorList[Random.Range(0, this.colorList.Length)];
		}

		// Token: 0x020003E7 RID: 999
		protected class SmallColoredSplash : ParticleSystem.FireControl.FireEmitter
		{
			// Token: 0x06001DA1 RID: 7585 RVA: 0x0007D1A4 File Offset: 0x0007B3A4
			public SmallColoredSplash(int x, int y, Color32[] colorList) : base(x, y)
			{
				this.colorList = colorList;
				this.updateFrequency = 0;
				this.riseSpeed = 0.4f;
				this.xSpeedVariance = 0.1f;
				this.drag = 0f;
				base.setLife(Random.Range(5, 15));
				this.arraySize = 3;
				this.cycleColor();
				base.populateArray();
			}
		}
	}

	// Token: 0x020002CB RID: 715
	private class ColoredSpellParticlesControl : ParticleSystem.FireControl
	{
		// Token: 0x06001BB4 RID: 7092 RVA: 0x00077DAD File Offset: 0x00075FAD
		public ColoredSpellParticlesControl(int xStart, int yStart, Color32[] colorList) : base(10, 3, xStart, yStart)
		{
			this.colorList = colorList;
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x00077DC1 File Offset: 0x00075FC1
		protected override void addEmitter()
		{
			this.emitters.Add(new ParticleSystem.ColoredSpellParticlesControl.ColoredSpellParticles(this.x, this.y, this.colorList));
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x00077DE5 File Offset: 0x00075FE5
		protected override void cycleColor()
		{
			this.color = this.colorList[Random.Range(0, this.colorList.Length)];
		}

		// Token: 0x020003E8 RID: 1000
		protected class ColoredSpellParticles : ParticleSystem.FireControl.FireEmitter
		{
			// Token: 0x06001DA2 RID: 7586 RVA: 0x0007D20C File Offset: 0x0007B40C
			public ColoredSpellParticles(int x, int y, Color32[] colorList) : base(x, y)
			{
				this.colorList = colorList;
				this.updateFrequency = 0;
				this.riseSpeed = 0.2f;
				this.xSpeedVariance = 0.1f;
				this.drag = 0f;
				base.setLife(Random.Range(5, 20));
				this.arraySize = 2;
				this.cycleColor();
				base.populateArray();
			}
		}
	}

	// Token: 0x020002CC RID: 716
	private class SmallExplosionControl : ParticleSystem.FireControl
	{
		// Token: 0x06001BB7 RID: 7095 RVA: 0x00077E06 File Offset: 0x00076006
		public SmallExplosionControl(int xStart, int yStart) : base(15, 3, xStart, yStart)
		{
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x00077E13 File Offset: 0x00076013
		protected override void addEmitter()
		{
			this.emitters.Add(new ParticleSystem.SmallExplosionControl.SmallExplosion(this.x, this.y));
		}

		// Token: 0x020003E9 RID: 1001
		protected class SmallExplosion : ParticleSystem.FireControl.FireEmitter
		{
			// Token: 0x06001DA3 RID: 7587 RVA: 0x0007D274 File Offset: 0x0007B474
			public SmallExplosion(int x, int y) : base(x, y)
			{
				this.colorList = ParticleSystem.fireColors;
				this.updateFrequency = 0;
				this.riseSpeed = 0.6f;
				this.xSpeedVariance = 0.2f;
				this.drag = 0f;
				base.setLife(Random.Range(5, 30));
				this.arraySize = 5;
				this.cycleColor();
				base.populateArray();
			}
		}
	}

	// Token: 0x020002CD RID: 717
	private class AuraControl : ParticleSystem.FireControl
	{
		// Token: 0x06001BB9 RID: 7097 RVA: 0x00077E31 File Offset: 0x00076031
		public AuraControl(int xStart, int yStart, Color32[] colorList) : base(45, 3, xStart, yStart)
		{
			this.colorList = colorList;
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x00077E4C File Offset: 0x0007604C
		public override void update(int xOffset, int yOffset, TextureTools.TextureData textureData)
		{
			if (this.radius < 30)
			{
				this.radius += 4;
			}
			base.update(xOffset, yOffset, textureData);
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x00077E70 File Offset: 0x00076070
		protected override void addEmitter()
		{
			float f = Random.Range(0f, 6.2831855f);
			int num = Mathf.RoundToInt((float)this.radius * Mathf.Sin(f));
			int num2 = Mathf.RoundToInt((float)this.radius * Mathf.Cos(f));
			this.emitters.Add(new ParticleSystem.ColoredSpellParticlesControl(this.x + num, this.y + num2, this.colorList));
		}

		// Token: 0x04000A20 RID: 2592
		protected int radius = 8;
	}

	// Token: 0x020002CE RID: 718
	private class SprayFireConeControl : ParticleSystem.SprayControl
	{
		// Token: 0x06001BBC RID: 7100 RVA: 0x00077EDB File Offset: 0x000760DB
		public SprayFireConeControl(int xStart, int yStart, int xTarget, int yTarget) : base(xStart, yStart, xTarget, yTarget)
		{
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x00077EE8 File Offset: 0x000760E8
		protected override void addEmitter()
		{
			float f = this.facing + Random.Range(-0.7853982f, 0.7853982f);
			int num = Mathf.RoundToInt((float)this.radius * Mathf.Sin(f));
			int num2 = Mathf.RoundToInt((float)this.radius * Mathf.Cos(f));
			this.emitters.Add(new ParticleSystem.SmallExplosionControl(this.x + num, this.y + num2));
		}
	}

	// Token: 0x020002CF RID: 719
	private class SprayFireLineControl : ParticleSystem.SprayControl
	{
		// Token: 0x06001BBE RID: 7102 RVA: 0x00077F54 File Offset: 0x00076154
		public SprayFireLineControl(int xStart, int yStart, int xTarget, int yTarget) : base(xStart, yStart, xTarget, yTarget)
		{
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x00077F64 File Offset: 0x00076164
		protected override void addEmitter()
		{
			float facing = this.facing;
			int num = Mathf.RoundToInt((float)this.radius * Mathf.Sin(facing));
			int num2 = Mathf.RoundToInt((float)this.radius * Mathf.Cos(facing));
			this.emitters.Add(new ParticleSystem.SmallExplosionControl(this.x + num, this.y + num2));
		}
	}

	// Token: 0x020002D0 RID: 720
	private class SprayColorLineControl : ParticleSystem.SprayControl
	{
		// Token: 0x06001BC0 RID: 7104 RVA: 0x00077FC0 File Offset: 0x000761C0
		public SprayColorLineControl(int xStart, int yStart, int xTarget, int yTarget, Color32[] colorList) : base(xStart, yStart, xTarget, yTarget)
		{
			this.colorList = colorList;
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x00077FD8 File Offset: 0x000761D8
		protected override void addEmitter()
		{
			float facing = this.facing;
			int num = Mathf.RoundToInt((float)this.radius * Mathf.Sin(facing));
			int num2 = Mathf.RoundToInt((float)this.radius * Mathf.Cos(facing));
			this.emitters.Add(new ParticleSystem.SmallColoredSplashControl(this.x + num, this.y + num2, this.colorList));
		}
	}

	// Token: 0x020002D1 RID: 721
	private class SprayColorConeControl : ParticleSystem.SprayControl
	{
		// Token: 0x06001BC2 RID: 7106 RVA: 0x0007803A File Offset: 0x0007623A
		public SprayColorConeControl(int xStart, int yStart, int xTarget, int yTarget, Color32[] colorList) : base(xStart, yStart, xTarget, yTarget)
		{
			this.colorList = colorList;
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x00078050 File Offset: 0x00076250
		protected override void addEmitter()
		{
			float f = this.facing + Random.Range(-0.7853982f, 0.7853982f);
			int num = Mathf.RoundToInt((float)this.radius * Mathf.Sin(f));
			int num2 = Mathf.RoundToInt((float)this.radius * Mathf.Cos(f));
			this.emitters.Add(new ParticleSystem.SmallColoredSplashControl(this.x + num, this.y + num2, this.colorList));
		}
	}

	// Token: 0x020002D2 RID: 722
	private abstract class SprayControl : ParticleSystem.FireControl
	{
		// Token: 0x06001BC4 RID: 7108 RVA: 0x000780C2 File Offset: 0x000762C2
		public SprayControl(int xStart, int yStart, int xTarget, int yTarget) : this(xStart, yStart, xTarget, yTarget, 25, 8)
		{
		}

		// Token: 0x06001BC5 RID: 7109 RVA: 0x000780D4 File Offset: 0x000762D4
		public SprayControl(int xStart, int yStart, int xTarget, int yTarget, int life, int density) : base(life, density, xStart, yStart)
		{
			int num = xTarget - xStart;
			int num2 = yTarget - yStart;
			int num3 = Mathf.Abs(num);
			int num4 = Mathf.Abs(num2);
			if (num3 <= num4)
			{
				if (num3 < num4)
				{
					if (num2 > 0)
					{
						this.facing = 0f;
						return;
					}
					this.facing = -3.1415927f;
				}
				return;
			}
			if (num > 0)
			{
				this.facing = 1.5707964f;
				return;
			}
			this.facing = -1.5707964f;
		}

		// Token: 0x06001BC6 RID: 7110 RVA: 0x0007814A File Offset: 0x0007634A
		public override void update(int xOffset, int yOffset, TextureTools.TextureData textureData)
		{
			if (this.radius < 45)
			{
				this.radius += 3;
			}
			base.update(xOffset, yOffset, textureData);
		}

		// Token: 0x04000A21 RID: 2593
		protected int radius = 6;

		// Token: 0x04000A22 RID: 2594
		protected float facing;
	}

	// Token: 0x020002D3 RID: 723
	private class FireballControl : ParticleSystem.FireControl
	{
		// Token: 0x06001BC7 RID: 7111 RVA: 0x0007816D File Offset: 0x0007636D
		public FireballControl(int xStart, int yStart) : base(15, 8, xStart, yStart)
		{
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x0007817C File Offset: 0x0007637C
		protected override void addEmitter()
		{
			float f = Random.Range(0f, 6.2831855f);
			int num = Mathf.RoundToInt((float)this.radius * Mathf.Sin(f));
			int num2 = Mathf.RoundToInt((float)this.radius * Mathf.Cos(f));
			this.emitters.Add(new ParticleSystem.SmallExplosionControl(this.x + num, this.y + num2));
		}

		// Token: 0x06001BC9 RID: 7113 RVA: 0x000781E1 File Offset: 0x000763E1
		public override void update(int xOffset, int yOffset, TextureTools.TextureData textureData)
		{
			if (this.radius < 30)
			{
				this.radius += 3;
			}
			base.update(xOffset, yOffset, textureData);
		}

		// Token: 0x04000A23 RID: 2595
		private int radius;
	}

	// Token: 0x020002D4 RID: 724
	private class FlameContinualLargeControl : ParticleSystem.FireControl
	{
		// Token: 0x06001BCA RID: 7114 RVA: 0x00078204 File Offset: 0x00076404
		public FlameContinualLargeControl(int xStart, int yStart) : base(-1, 2, xStart, yStart)
		{
		}

		// Token: 0x06001BCB RID: 7115 RVA: 0x00078210 File Offset: 0x00076410
		protected override void addEmitter()
		{
			this.emitters.Add(new ParticleSystem.FlameContinualLargeControl.FlameLarge(this.x, this.y));
		}

		// Token: 0x020003EA RID: 1002
		protected class FlameLarge : ParticleSystem.FireControl.FireEmitter
		{
			// Token: 0x06001DA4 RID: 7588 RVA: 0x0007D2E0 File Offset: 0x0007B4E0
			public FlameLarge(int x, int y) : base(x, y)
			{
				this.x = x;
				this.y = y + Random.Range(2, -3);
				this.updateFrequency = 1;
				this.colorList = ParticleSystem.fireColors;
				this.rotation = Random.Range(0f, 6.2831855f);
				this.rotationSpeed = 0.2f;
				this.rotationAmplitude = 0.05f;
				this.drag = 0.1f;
				base.setLife(Random.Range(30, 65));
				this.xSpeedVariance = 0.5f;
				this.arraySize = 5;
				this.riseSpeed = 0.3f;
				this.baseWidth = 4f;
				this.cycleColor();
				base.populateArray();
			}
		}
	}

	// Token: 0x020002D5 RID: 725
	private class FlameContinualMediumControl : ParticleSystem.FireControl
	{
		// Token: 0x06001BCC RID: 7116 RVA: 0x0007822E File Offset: 0x0007642E
		public FlameContinualMediumControl(int xStart, int yStart) : base(-1, 2, xStart, yStart)
		{
		}

		// Token: 0x06001BCD RID: 7117 RVA: 0x0007823A File Offset: 0x0007643A
		protected override void addEmitter()
		{
			this.emitters.Add(new ParticleSystem.FireControl.FireEmitter(this.x, this.y));
		}
	}

	// Token: 0x020002D6 RID: 726
	private class FlameContinualMediumControlGreen : ParticleSystem.FireControl
	{
		// Token: 0x06001BCE RID: 7118 RVA: 0x00078258 File Offset: 0x00076458
		public FlameContinualMediumControlGreen(int xStart, int yStart) : base(-1, 2, xStart, yStart)
		{
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x00078264 File Offset: 0x00076464
		protected override void addEmitter()
		{
			this.emitters.Add(new ParticleSystem.FireControl.FireEmitterGreen(this.x, this.y));
		}
	}

	// Token: 0x020002D7 RID: 727
	private class FlameContinualMediumControlPurple : ParticleSystem.FireControl
	{
		// Token: 0x06001BD0 RID: 7120 RVA: 0x00078282 File Offset: 0x00076482
		public FlameContinualMediumControlPurple(int xStart, int yStart) : base(-1, 2, xStart, yStart)
		{
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x0007828E File Offset: 0x0007648E
		protected override void addEmitter()
		{
			this.emitters.Add(new ParticleSystem.FireControl.FireEmitterPurple(this.x, this.y));
		}
	}

	// Token: 0x020002D8 RID: 728
	private class FountainMediumControl : ParticleSystem.FireControl
	{
		// Token: 0x06001BD2 RID: 7122 RVA: 0x000782AC File Offset: 0x000764AC
		public FountainMediumControl(int xStart, int yStart) : base(-1, 2, xStart, yStart)
		{
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x000782B8 File Offset: 0x000764B8
		protected override void addEmitter()
		{
			this.emitters.Add(new ParticleSystem.FountainMediumControl.WaterSpurt(this.x, this.y));
		}

		// Token: 0x020003EB RID: 1003
		protected class WaterSpurt : ParticleSystem.FireControl.FireEmitter
		{
			// Token: 0x06001DA5 RID: 7589 RVA: 0x0007D398 File Offset: 0x0007B598
			public WaterSpurt(int x, int y) : base(x, y)
			{
				this.updateFrequency = 0;
				this.colorList = ParticleSystem.waterColors;
				this.rotation = 0f;
				this.rotationSpeed = 0f;
				this.rotationAmplitude = 0f;
				this.drag = 0.001f;
				base.setLife(Random.Range(20, 30));
				this.xSpeedVariance = 0.15f;
				this.arraySize = 5;
				this.riseSpeed = 0.8f;
				this.baseWidth = 1f;
				this.yGravity = -0.02f;
				this.cycleColor();
				base.populateArray();
			}
		}
	}

	// Token: 0x020002D9 RID: 729
	private class FlameContinualSmallControl : ParticleSystem.FireControl
	{
		// Token: 0x06001BD4 RID: 7124 RVA: 0x000782D6 File Offset: 0x000764D6
		public FlameContinualSmallControl(int xStart, int yStart) : base(-1, 2, xStart, yStart)
		{
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x000782E2 File Offset: 0x000764E2
		protected override void addEmitter()
		{
			this.emitters.Add(new ParticleSystem.FlameContinualSmallControl.FlameSmall(this.x, this.y));
		}

		// Token: 0x020003EC RID: 1004
		protected class FlameSmall : ParticleSystem.FireControl.FireEmitter
		{
			// Token: 0x06001DA6 RID: 7590 RVA: 0x0007D43C File Offset: 0x0007B63C
			public FlameSmall(int x, int y) : base(x, y)
			{
				this.x = x;
				this.y = y + Random.Range(1, -2);
				this.rotation = Random.Range(0f, 6.2831855f);
				this.rotationSpeed = 0.2f;
				this.rotationAmplitude = 0.05f;
				this.drag = 0.1f;
				base.setLife(Random.Range(10, 40));
				this.xSpeedVariance = 0.1f;
				this.arraySize = 3;
				this.riseSpeed = 0.15f;
				this.baseWidth = 1f;
				this.cycleColor();
				base.populateArray();
			}
		}
	}

	// Token: 0x020002DA RID: 730
	private class FlameContinualTinyControl : ParticleSystem.FireControl
	{
		// Token: 0x06001BD6 RID: 7126 RVA: 0x00078300 File Offset: 0x00076500
		public FlameContinualTinyControl(int xStart, int yStart) : base(-1, 1, xStart, yStart)
		{
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x0007830C File Offset: 0x0007650C
		protected override void addEmitter()
		{
			this.emitters.Add(new ParticleSystem.FlameContinualTinyControl.FlameTiny(this.x, this.y));
		}

		// Token: 0x020003ED RID: 1005
		protected class FlameTiny : ParticleSystem.FireControl.FireEmitter
		{
			// Token: 0x06001DA7 RID: 7591 RVA: 0x0007D4E4 File Offset: 0x0007B6E4
			public FlameTiny(int x, int y) : base(x, y)
			{
				this.x = x + Random.Range(-1, 2);
				this.y = y + Random.Range(1, -2);
				this.rotation = Random.Range(0f, 6.2831855f);
				this.colorList = new Color32[]
				{
					C64Color.White,
					C64Color.Yellow,
					C64Color.Yellow,
					C64Color.Yellow,
					C64Color.RedLight
				};
				this.rotationSpeed = 0.2f;
				this.rotationAmplitude = 0.05f;
				this.drag = 0.1f;
				base.setLife(Random.Range(5, 1));
				this.xSpeedVariance = 0.1f;
				this.arraySize = 3;
				this.riseSpeed = 0.15f;
				this.baseWidth = 0f;
				this.cycleColor();
				base.populateArray();
			}
		}
	}

	// Token: 0x020002DB RID: 731
	protected abstract class BaseParticleEmitter : ParticleSystem.Emitter
	{
		// Token: 0x06001BD8 RID: 7128 RVA: 0x0007832A File Offset: 0x0007652A
		protected CountDownClock getCountDownClock()
		{
			if (this.updateCountDown == null)
			{
				this.updateCountDown = new CountDownClock(this.updateFrequency, true);
			}
			return this.updateCountDown;
		}

		// Token: 0x06001BD9 RID: 7129 RVA: 0x0007834C File Offset: 0x0007654C
		protected void setParticleData(int i, float xSpeed, float ySpeed, float x, float y)
		{
			this.particleArray[i] = xSpeed;
			this.particleArray[i + 1] = ySpeed;
			this.particleArray[i + 2] = x;
			this.particleArray[i + 3] = y;
		}

		// Token: 0x04000A24 RID: 2596
		protected float[] particleArray;

		// Token: 0x04000A25 RID: 2597
		protected int updateFrequency = 1;

		// Token: 0x04000A26 RID: 2598
		private CountDownClock updateCountDown;

		// Token: 0x04000A27 RID: 2599
		protected float xGravity;

		// Token: 0x04000A28 RID: 2600
		protected float yGravity;

		// Token: 0x04000A29 RID: 2601
		protected float ground;
	}

	// Token: 0x020002DC RID: 732
	protected abstract class RisingParticleEmitter : ParticleSystem.BaseParticleEmitter
	{
		// Token: 0x06001BDB RID: 7131 RVA: 0x0007838C File Offset: 0x0007658C
		protected void populateArray()
		{
			this.particleArray = new float[this.arraySize * 4];
			for (int i = 0; i < this.particleArray.Length - 4; i += 4)
			{
				base.setParticleData(i, Random.Range(-this.xSpeedVariance, this.xSpeedVariance), Random.Range(0.1f, this.riseSpeed), (float)this.x + Random.Range(-this.baseWidth, this.baseWidth), (float)this.y);
			}
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x0007840C File Offset: 0x0007660C
		public override void update(int xOffset, int yOffset, TextureTools.TextureData textureData)
		{
			base.getCountDownClock().tick();
			if (base.getCountDownClock().isTimerZero())
			{
				this.testLife();
				this.cycleColor();
				this.rotation = (this.rotation + this.rotationSpeed) % 6.2831855f;
			}
			for (int i = 0; i < this.particleArray.Length - 4; i += 4)
			{
				float num = this.particleArray[i];
				float num2 = this.particleArray[i + 1];
				float num3 = this.particleArray[i + 2];
				float num4 = this.particleArray[i + 3];
				if (base.getCountDownClock().isTimerZero())
				{
					num2 += this.yGravity;
					num3 += num + this.rotationAmplitude * Mathf.Sin(this.rotation);
					num4 += num2;
					if (num3 > (float)this.x)
					{
						num -= this.drag;
					}
					if (num3 < (float)this.x)
					{
						num += this.drag;
					}
				}
				textureData.SetPixel(Mathf.RoundToInt(num3) - xOffset, Mathf.RoundToInt(num4) - yOffset, this.color);
				base.setParticleData(i, num, num2, num3, num4);
			}
		}

		// Token: 0x04000A2A RID: 2602
		protected float rotation;

		// Token: 0x04000A2B RID: 2603
		protected float rotationAmplitude;

		// Token: 0x04000A2C RID: 2604
		protected float rotationSpeed;

		// Token: 0x04000A2D RID: 2605
		protected float drag;

		// Token: 0x04000A2E RID: 2606
		protected float xSpeedVariance;

		// Token: 0x04000A2F RID: 2607
		protected float riseSpeed;

		// Token: 0x04000A30 RID: 2608
		protected float baseWidth;

		// Token: 0x04000A31 RID: 2609
		protected int arraySize;
	}

	// Token: 0x020002DD RID: 733
	private class FlyEmitter : ParticleSystem.DirectionalEffect
	{
		// Token: 0x06001BDE RID: 7134 RVA: 0x00078528 File Offset: 0x00076728
		public FlyEmitter(int _xStart, int _yStart, int _xTarget, int _yTarget) : base(_xStart, _yStart, _xTarget, _yTarget)
		{
			this.particleArray = new float[40];
			this.colorList = new Color32[]
			{
				C64Color.Gray
			};
			for (int i = 0; i < this.particleArray.Length - 4; i += 4)
			{
				float f = Random.Range(0f, 6.2831855f);
				this.particleArray[i] = this.maxSpeed * Mathf.Sin(f);
				this.particleArray[i + 1] = this.maxSpeed * Mathf.Cos(f);
				this.particleArray[i + 2] = (float)_xStart;
				this.particleArray[i + 3] = (float)_yStart;
			}
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x000785E4 File Offset: 0x000767E4
		public override void update(int xOffset, int yOffset, TextureTools.TextureData textureData)
		{
			for (int i = 0; i < this.particleArray.Length - 4; i += 4)
			{
				float num = this.particleArray[i];
				float num2 = this.particleArray[i + 1];
				float num3 = this.particleArray[i + 2];
				float num4 = this.particleArray[i + 3];
				num3 += num;
				num4 += num2;
				if (num3 < (float)this.xTarget && num < this.maxSpeed)
				{
					num += this.correction + Random.Range(-0.005f, 0.005f);
				}
				if (num3 > (float)this.xTarget && num > -this.maxSpeed)
				{
					num -= this.correction + Random.Range(-0.005f, 0.005f);
				}
				if (num4 < (float)this.yTarget && num2 < this.maxSpeed)
				{
					num2 += this.correction + Random.Range(-0.005f, 0.005f);
				}
				if (num4 > (float)this.yTarget && num2 > -this.maxSpeed)
				{
					num2 -= this.correction + Random.Range(-0.005f, 0.005f);
				}
				base.setColor();
				int num5 = Mathf.RoundToInt(num3) - xOffset;
				int num6 = Mathf.RoundToInt(num4) - yOffset;
				if (num5 >= 0 && num5 < textureData.width && num6 >= 0 && num6 < textureData.height)
				{
					textureData.SetPixel(num5, num6, this.color);
				}
				this.particleArray[i] = num;
				this.particleArray[i + 1] = num2;
				this.particleArray[i + 2] = num3;
				this.particleArray[i + 3] = num4;
			}
		}

		// Token: 0x04000A32 RID: 2610
		protected float[] particleArray;

		// Token: 0x04000A33 RID: 2611
		protected float maxSpeed = 0.5f;

		// Token: 0x04000A34 RID: 2612
		protected float correction = 0.005f;
	}

	// Token: 0x020002DE RID: 734
	private class FireFlyEmitter : ParticleSystem.FlyEmitter
	{
		// Token: 0x06001BE0 RID: 7136 RVA: 0x00078775 File Offset: 0x00076975
		public FireFlyEmitter(int _xStart, int _yStart, int _xTarget, int _yTarget) : base(_xStart, _yStart, _xTarget, _yTarget)
		{
			this.colorList = new Color32[]
			{
				C64Color.Yellow
			};
		}
	}

	// Token: 0x020002DF RID: 735
	private class CorpseFlyEmitter : ParticleSystem.FlyEmitter
	{
		// Token: 0x06001BE1 RID: 7137 RVA: 0x0007879C File Offset: 0x0007699C
		public CorpseFlyEmitter(int _xStart, int _yStart, int _xTarget, int _yTarget) : base(_xStart, _yStart, _xTarget, _yTarget)
		{
			this.maxSpeed = 0.65f;
			this.correction = 0.015f;
			this.colorList = new Color32[]
			{
				C64Color.GrayDark,
				C64Color.GrayLight
			};
		}
	}

	// Token: 0x020002E0 RID: 736
	private class HomingParticleEmitter : ParticleSystem.DirectionalEffect
	{
		// Token: 0x06001BE2 RID: 7138 RVA: 0x000787F0 File Offset: 0x000769F0
		public HomingParticleEmitter(int _xStart, int _yStart, int _xTarget, int _yTarget) : base(_xStart, _yStart, _xTarget, _yTarget)
		{
			this.particleArray = new float[120];
			this.colorList = new Color32[]
			{
				C64Color.White,
				C64Color.Yellow
			};
			base.setLife(60);
			for (int i = 0; i < this.particleArray.Length - 4; i += 4)
			{
				float f = Random.Range(0f, 6.2831855f);
				this.particleArray[i] = Mathf.Sin(f) * 2f;
				this.particleArray[i + 1] = Mathf.Cos(f) * 2f;
				this.particleArray[i + 2] = (float)_xStart;
				this.particleArray[i + 3] = (float)_yStart;
			}
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x000788C0 File Offset: 0x00076AC0
		public override void update(int xOffset, int yOffset, TextureTools.TextureData textureData)
		{
			this.testLife();
			for (int i = 0; i < this.particleArray.Length - 4; i += 4)
			{
				float num = this.particleArray[i];
				float num2 = this.particleArray[i + 1];
				float num3 = this.particleArray[i + 2];
				float num4 = this.particleArray[i + 3];
				num3 += num;
				num4 += num2;
				if (num3 < (float)this.xTarget && num < this.maxSpeed)
				{
					num += this.correction;
				}
				if (num3 > (float)this.xTarget && num > -this.maxSpeed)
				{
					num -= this.correction;
				}
				if (num4 < (float)this.yTarget && num2 < this.maxSpeed)
				{
					num2 += this.correction;
				}
				if (num4 > (float)this.yTarget && num2 > -this.maxSpeed)
				{
					num2 -= this.correction;
				}
				base.setColor();
				int num5 = Mathf.RoundToInt(num3) - xOffset;
				int num6 = Mathf.RoundToInt(num4) - yOffset;
				if (num5 >= 0 && num5 < textureData.width && num6 >= 0 && num6 < textureData.height)
				{
					textureData.SetPixel(num5, num6, this.color);
				}
				this.particleArray[i] = num;
				this.particleArray[i + 1] = num2;
				this.particleArray[i + 2] = num3;
				this.particleArray[i + 3] = num4;
			}
		}

		// Token: 0x04000A35 RID: 2613
		private float[] particleArray;

		// Token: 0x04000A36 RID: 2614
		private float maxSpeed = 3f;

		// Token: 0x04000A37 RID: 2615
		private float correction = 0.2f;
	}

	// Token: 0x020002E1 RID: 737
	private class LightningEmitter : ParticleSystem.DirectionalEffect
	{
		// Token: 0x06001BE4 RID: 7140 RVA: 0x00078A17 File Offset: 0x00076C17
		public LightningEmitter(int _xStart, int _yStart, int _xTarget, int _yTarget) : base(_xStart, _yStart, _xTarget, _yTarget)
		{
			this.colorList = ParticleSystem.lightningColors;
			base.setColor();
			base.setLife(30);
			this.createBendArray();
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x00078A4C File Offset: 0x00076C4C
		private void createBendArray()
		{
			this.bendArray = new int[this.bends * 2 + 4];
			this.bendArray[0] = this.xStart;
			this.bendArray[1] = this.yStart;
			this.bendArray[this.bendArray.Length - 2] = this.xTarget;
			this.bendArray[this.bendArray.Length - 1] = this.yTarget;
			int num = (this.xTarget - this.xStart) / this.bends;
			int num2 = (this.yTarget - this.yStart) / this.bends;
			int num3 = 8;
			for (int i = 2; i < this.bendArray.Length - 2; i += 2)
			{
				this.bendArray[i] = this.bendArray[i - 2] + num + Random.Range(-num3, num3);
				this.bendArray[i + 1] = this.bendArray[i - 1] + num2 + Random.Range(-num3, num3);
			}
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x00078B38 File Offset: 0x00076D38
		public override void update(int xOffset, int yOffset, TextureTools.TextureData textureData)
		{
			this.testLife();
			base.setColor();
			this.createBendArray();
			for (int i = 0; i < this.bendArray.Length - 2; i += 2)
			{
				int x = this.bendArray[i] - xOffset;
				int y = this.bendArray[i + 1] - yOffset;
				int x2 = this.bendArray[i + 2] - xOffset;
				int y2 = this.bendArray[i + 3] - yOffset;
				TextureTools.drawRay(x, y, x2, y2, textureData, this.color);
			}
		}

		// Token: 0x04000A38 RID: 2616
		private int[] bendArray;

		// Token: 0x04000A39 RID: 2617
		private int bends = 3;
	}

	// Token: 0x020002E2 RID: 738
	private class Ray : ParticleSystem.DirectionalEffect
	{
		// Token: 0x06001BE7 RID: 7143 RVA: 0x00078BAC File Offset: 0x00076DAC
		public Ray(int _xStart, int _yStart, int _xTarget, int _yTarget) : base(_xStart, _yStart, _xTarget, _yTarget)
		{
			this.colorList = new Color32[]
			{
				C64Color.White,
				C64Color.White,
				C64Color.Red,
				C64Color.Yellow,
				C64Color.RedLight
			};
			base.setColor();
			base.setLife(30);
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x00078C1A File Offset: 0x00076E1A
		public override void update(int xOffset, int yOffset, TextureTools.TextureData textureData)
		{
			this.testLife();
			base.setColor();
			TextureTools.drawRay(this.xStart - xOffset, this.yStart - yOffset, this.xTarget - xOffset, this.yTarget - yOffset, textureData, this.color);
		}
	}

	// Token: 0x020002E3 RID: 739
	private class Arrow : ParticleSystem.Projectile
	{
		// Token: 0x06001BE9 RID: 7145 RVA: 0x00078C54 File Offset: 0x00076E54
		public Arrow(int _xStart, int _yStart, int _xTarget, int _yTarget) : base(_xStart, _yStart, _xTarget, _yTarget)
		{
			this.length = 8f;
		}
	}

	// Token: 0x020002E4 RID: 740
	protected class Projectile : ParticleSystem.DirectionalEffect
	{
		// Token: 0x06001BEA RID: 7146 RVA: 0x00078C6C File Offset: 0x00076E6C
		protected Projectile(int _xStart, int _yStart, int _xTarget, int _yTarget) : base(_xStart, _yStart, _xTarget, _yTarget)
		{
			this.color = C64Color.White;
			float f = (float)(this.xTarget - this.xStart);
			float f2 = (float)(this.yTarget - this.yStart);
			this.currentX = (float)this.xStart;
			this.currentY = (float)this.yStart;
			float num = Mathf.Abs(f);
			float num2 = Mathf.Abs(f2);
			int num3 = Mathf.RoundToInt(Mathf.Sign(f2));
			int num4 = Mathf.RoundToInt(Mathf.Sign(f));
			this.yStep = (float)num3;
			this.xStep = (float)num4;
			if (num > num2)
			{
				this.greatestDistance = num;
				this.yStep = (float)num3 * (num2 / num);
			}
			else if (num < num2)
			{
				this.greatestDistance = num2;
				this.xStep = (float)num4 * (num / num2);
			}
			this.waitToFinish = true;
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x00078D4C File Offset: 0x00076F4C
		public override void update(int xOffset, int yOffset, TextureTools.TextureData textureData)
		{
			this.currentDistance += this.speed;
			this.testLife();
			this.currentX += this.xStep * this.speed;
			this.currentY += this.yStep * this.speed;
			int num = Mathf.RoundToInt(this.currentX) - xOffset;
			int num2 = Mathf.RoundToInt(this.currentY) - yOffset;
			int x = Mathf.RoundToInt((float)num + this.xStep * this.length);
			int y = Mathf.RoundToInt((float)num2 + this.yStep * this.length);
			TextureTools.drawRay(num, num2, x, y, textureData, this.color);
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x00078DFC File Offset: 0x00076FFC
		protected override void testLife()
		{
			if (this.currentDistance >= this.greatestDistance)
			{
				this.dead = true;
			}
		}

		// Token: 0x04000A3A RID: 2618
		private float yStep;

		// Token: 0x04000A3B RID: 2619
		private float xStep;

		// Token: 0x04000A3C RID: 2620
		private float currentX;

		// Token: 0x04000A3D RID: 2621
		private float currentY;

		// Token: 0x04000A3E RID: 2622
		protected float speed = 8f;

		// Token: 0x04000A3F RID: 2623
		protected float length = 1f;

		// Token: 0x04000A40 RID: 2624
		private float currentDistance;

		// Token: 0x04000A41 RID: 2625
		private float greatestDistance;
	}

	// Token: 0x020002E5 RID: 741
	protected class DirectionalEffect : ParticleSystem.Emitter
	{
		// Token: 0x06001BED RID: 7149 RVA: 0x00078E13 File Offset: 0x00077013
		protected DirectionalEffect(int _xStart, int _yStart, int _xTarget, int _yTarget)
		{
			this.xStart = _xStart;
			this.yStart = _yStart;
			this.xTarget = _xTarget;
			this.yTarget = _yTarget;
		}

		// Token: 0x06001BEE RID: 7150 RVA: 0x00078E38 File Offset: 0x00077038
		public override void update(int xOffset, int yOffset, TextureTools.TextureData textureData)
		{
		}

		// Token: 0x04000A42 RID: 2626
		protected int xStart;

		// Token: 0x04000A43 RID: 2627
		protected int yStart;

		// Token: 0x04000A44 RID: 2628
		protected int xTarget;

		// Token: 0x04000A45 RID: 2629
		protected int yTarget;
	}

	// Token: 0x020002E6 RID: 742
	protected abstract class Emitter
	{
		// Token: 0x06001BF0 RID: 7152
		public abstract void update(int xOffset, int yOffset, TextureTools.TextureData textureData);

		// Token: 0x06001BF1 RID: 7153 RVA: 0x00078E4D File Offset: 0x0007704D
		public virtual bool isDead()
		{
			return this.dead;
		}

		// Token: 0x06001BF2 RID: 7154 RVA: 0x00078E55 File Offset: 0x00077055
		public virtual float getLightDistance()
		{
			return 3f;
		}

		// Token: 0x06001BF3 RID: 7155 RVA: 0x00078E5C File Offset: 0x0007705C
		public virtual float getLightStrength()
		{
			return 1f;
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x00078E63 File Offset: 0x00077063
		public bool shouldYouWaitForMeToFinish()
		{
			return this.waitToFinish && !this.isDead();
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x00078E78 File Offset: 0x00077078
		protected void setColor()
		{
			if (this.colorList == null)
			{
				this.color = C64Color.White;
				return;
			}
			this.color = this.colorList[Random.Range(0, this.colorList.Length)];
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x00078EB0 File Offset: 0x000770B0
		protected virtual void cycleColor()
		{
			int num = this.colorList.Length - 1 - Mathf.RoundToInt(this.life / this.maxLife * (float)(this.colorList.Length - 1));
			if (num < 0 || num >= this.colorList.Length)
			{
				MainControl.logError(string.Concat(new string[]
				{
					num.ToString(),
					" / ",
					this.colorList.Length.ToString(),
					" (",
					this.life.ToString(),
					"/",
					this.maxLife.ToString(),
					")"
				}));
			}
			this.color = this.colorList[num];
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x00078F71 File Offset: 0x00077171
		protected virtual void testLife()
		{
			if (this.life < 0f)
			{
				return;
			}
			this.life -= 1f;
			if (this.life == 0f)
			{
				this.dead = true;
			}
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x00078FA7 File Offset: 0x000771A7
		public void setLife(int life)
		{
			if (!this.isDead())
			{
				this.life = (float)life;
				this.maxLife = (float)life;
			}
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x00078FC1 File Offset: 0x000771C1
		public bool isPerpetual()
		{
			return this.life < 0f;
		}

		// Token: 0x04000A46 RID: 2630
		protected bool dead;

		// Token: 0x04000A47 RID: 2631
		private float life;

		// Token: 0x04000A48 RID: 2632
		private float maxLife = -1f;

		// Token: 0x04000A49 RID: 2633
		protected Color32 color;

		// Token: 0x04000A4A RID: 2634
		protected Color32[] colorList;

		// Token: 0x04000A4B RID: 2635
		protected bool waitToFinish;

		// Token: 0x04000A4C RID: 2636
		protected int x;

		// Token: 0x04000A4D RID: 2637
		protected int y;
	}
}
