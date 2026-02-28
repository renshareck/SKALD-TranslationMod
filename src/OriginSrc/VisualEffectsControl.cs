using System;
using System.Collections.Generic;

// Token: 0x020000B5 RID: 181
[Serializable]
public class VisualEffectsControl
{
	// Token: 0x06000B04 RID: 2820 RVA: 0x00034DF2 File Offset: 0x00032FF2
	public VisualEffectsControl(SkaldPhysicalObject owner)
	{
		this.resetEffects();
		this.owner = owner;
	}

	// Token: 0x06000B05 RID: 2821 RVA: 0x00034E14 File Offset: 0x00033014
	public void updateVisualEffects(int xOffset, int yOffset, TextureTools.TextureData targetTexture)
	{
		this.updateLocalVFX();
		if (this.particleSystem != null)
		{
			this.particleSystem.update(xOffset, yOffset, targetTexture);
		}
		if (this.lightLevelControl == null)
		{
			return;
		}
		if (!this.areAnyEffectsActive())
		{
			this.lightLevelControl.clearIlluminated();
		}
		this.lightLevelControl.updateLightLevel();
	}

	// Token: 0x06000B06 RID: 2822 RVA: 0x00034E64 File Offset: 0x00033064
	public float getLightStrength()
	{
		if (this.lightLevelControl == null)
		{
			return 0f;
		}
		return this.lightLevelControl.getLightLevel();
	}

	// Token: 0x06000B07 RID: 2823 RVA: 0x00034E7F File Offset: 0x0003307F
	public bool effectsAreIlluminatingArea()
	{
		return (double)this.getLightStrength() > 0.01;
	}

	// Token: 0x06000B08 RID: 2824 RVA: 0x00034E93 File Offset: 0x00033093
	public void setEffectsLighting(float value)
	{
		this.getAndSetLightLevelControl().setLightLevel(value);
	}

	// Token: 0x06000B09 RID: 2825 RVA: 0x00034EA1 File Offset: 0x000330A1
	public string getLightModelPath()
	{
		return "Round2Green";
	}

	// Token: 0x06000B0A RID: 2826 RVA: 0x00034EA8 File Offset: 0x000330A8
	public float getLightDistance()
	{
		return 3f;
	}

	// Token: 0x06000B0B RID: 2827 RVA: 0x00034EAF File Offset: 0x000330AF
	public bool shouldYouWaitForParticlesToFinish()
	{
		return this.particleSystem != null && this.particleSystem.shouldYouWaitForParticlesToFinish();
	}

	// Token: 0x06000B0C RID: 2828 RVA: 0x00034EC6 File Offset: 0x000330C6
	public bool areAnyPerpetualEffectsActive()
	{
		return this.particleSystem != null && this.particleSystem.areAnyPerpetualEffectsActive();
	}

	// Token: 0x06000B0D RID: 2829 RVA: 0x00034EE0 File Offset: 0x000330E0
	public bool areAnyEffectsActive()
	{
		if (this.particleSystem != null && this.particleSystem.areAnyEffectsActive())
		{
			return true;
		}
		using (List<VisualEffectsControl.Effect>.Enumerator enumerator = this.effects.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isActive())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000B0E RID: 2830 RVA: 0x00034F50 File Offset: 0x00033150
	public bool areAnyEffectsOrLightActive()
	{
		return this.areAnyEffectsActive() || this.effectsAreIlluminatingArea();
	}

	// Token: 0x06000B0F RID: 2831 RVA: 0x00034F62 File Offset: 0x00033162
	public void setParticleLife(int life)
	{
		if (this.particleSystem == null)
		{
			return;
		}
		this.particleSystem.setLife(life);
	}

	// Token: 0x06000B10 RID: 2832 RVA: 0x00034F79 File Offset: 0x00033179
	private ParticleSystem getAndSetParticleSystem()
	{
		if (this.particleSystem == null)
		{
			this.particleSystem = new ParticleSystem();
		}
		return this.particleSystem;
	}

	// Token: 0x06000B11 RID: 2833 RVA: 0x00034F94 File Offset: 0x00033194
	private void updateLocalVFX()
	{
		if (this.effects == null)
		{
			return;
		}
		foreach (VisualEffectsControl.Effect effect in this.effects)
		{
			effect.countDown();
		}
	}

	// Token: 0x06000B12 RID: 2834 RVA: 0x00034FF0 File Offset: 0x000331F0
	public void resetEffects()
	{
		this.lightLevelControl = null;
		this.particleSystem = null;
		this.effects.Clear();
		this.negativeFlashCounter = new VisualEffectsControl.Effect();
		this.effects.Add(this.negativeFlashCounter);
		this.damageFlashCounter = new VisualEffectsControl.Effect();
		this.effects.Add(this.damageFlashCounter);
		this.positiveFlashCounter = new VisualEffectsControl.Effect();
		this.effects.Add(this.positiveFlashCounter);
		this.forceHighlight = new VisualEffectsControl.Effect();
		this.effects.Add(this.forceHighlight);
		this.shaken = new VisualEffectsControl.EffectBool();
		this.effects.Add(this.shaken);
	}

	// Token: 0x06000B13 RID: 2835 RVA: 0x000350A4 File Offset: 0x000332A4
	public void setContinualEffectFromString(string effect)
	{
		if (effect == null || effect == "")
		{
			return;
		}
		effect = effect.ToUpper();
		if (effect == "FLAMECONTINUALMEDIUMGREEN")
		{
			this.setFlameContinualMediumGreen();
			return;
		}
		if (effect == "FLAMECONTINUALMEDIUMPURPLE")
		{
			this.setFlameContinualMediumPurple();
			return;
		}
		if (effect == "FLAMECONTINUALLARGE")
		{
			this.setFlameContinualLarge();
			return;
		}
		if (effect == "FLAMECONTINUALMEDIUM")
		{
			this.setFlameContinualMedium();
			return;
		}
		if (effect == "FLAMECONTINUALSMALL")
		{
			this.setFlameContinualSmall();
			return;
		}
		if (effect == "FLAMECONTINUALTINY")
		{
			this.setFlameContinualTiny();
			return;
		}
		if (effect == "SMOKEMEDIUM")
		{
			this.setSmokeMedium();
			return;
		}
		if (effect == "FOUNTAINMEDIUM")
		{
			this.setFountainMedium();
			return;
		}
		if (effect == "FIREFLY")
		{
			this.setFireFlyEffect();
			return;
		}
		if (effect == "CORPSEFLY")
		{
			this.setCorpseFlyEffect();
			return;
		}
		MainControl.logError("Malformed visual effect id: " + effect + " for object " + this.owner.getId());
	}

	// Token: 0x06000B14 RID: 2836 RVA: 0x000351B4 File Offset: 0x000333B4
	public void setCombatEffectFromString(List<string> effects, SkaldPhysicalObject target)
	{
		foreach (string effect in effects)
		{
			if (target == null)
			{
				this.setCombatEffectFromString(effect, this.owner);
			}
			else
			{
				this.setCombatEffectFromString(effect, target);
			}
		}
	}

	// Token: 0x06000B15 RID: 2837 RVA: 0x00035218 File Offset: 0x00033418
	public void setCombatEffectFromString(string effect, SkaldPhysicalObject target)
	{
		if (effect == null || effect == "")
		{
			return;
		}
		if (target == null)
		{
			target = this.owner;
		}
		effect = effect.ToUpper();
		if (effect == "POSITIVEFLASH")
		{
			this.setPositiveFlashCounter();
			return;
		}
		if (effect == "NEGATIVEFLASH")
		{
			this.setNegativeFlashCounter();
			return;
		}
		if (effect == "DAMAGEFLASH")
		{
			this.setDamageFlashCounter();
			return;
		}
		if (effect == "RAY")
		{
			this.setRayEffect(target);
			return;
		}
		if (effect == "LIGHTNING")
		{
			this.setLightningEffect(target);
			return;
		}
		if (effect == "BLOOD")
		{
			this.setBlood();
			return;
		}
		if (effect == "SPARKS")
		{
			this.setSweating();
			return;
		}
		if (effect == "ACID")
		{
			this.setSweating();
			return;
		}
		if (effect == "FIREFLASHMEDIUM")
		{
			this.setFireFlashMedium(target);
			return;
		}
		if (effect == "HOMING")
		{
			this.setHomingParticleEffect(target);
			return;
		}
		if (effect == "SHAKESCREEN")
		{
			this.setShaken();
			return;
		}
		if (!(effect == "SHIELDGREEN"))
		{
			if (effect == "FIREBALL")
			{
				this.setFireball();
				return;
			}
			if (effect == "AURAGREEN")
			{
				this.setAuraGreen();
				return;
			}
			if (effect == "AURARED")
			{
				this.setAuraRed();
				return;
			}
			if (effect == "AURAFIRE")
			{
				this.setAuraFire();
				return;
			}
			if (effect == "AURAICE")
			{
				this.setAuraIce();
				return;
			}
			if (effect == "SPRAYFIRELINE")
			{
				this.setSprayFireLine();
				return;
			}
			if (effect == "SPRAYFIRECONE")
			{
				this.setSprayFireCone();
				return;
			}
			if (effect == "SPRAYICECONE")
			{
				this.setSprayIceCone();
				return;
			}
			if (effect == "SPRAYACIDLINE")
			{
				this.setSprayAcidLine();
				return;
			}
			if (effect == "SPRAYICELINE")
			{
				this.setSprayIceLine();
				return;
			}
			if (effect == "SPRAYACIDCONE")
			{
				this.setSprayAcidCone();
				return;
			}
			if (effect == "SPLASHACIC")
			{
				this.setSplashAcid();
				return;
			}
			if (effect == "SPLASHPOISON")
			{
				this.setSplashPoison();
				return;
			}
			MainControl.logError("Malformed visual effect id: " + effect + " for object " + this.owner.getId());
		}
	}

	// Token: 0x06000B16 RID: 2838 RVA: 0x0003545D File Offset: 0x0003365D
	public void setNegativeFlashCounter()
	{
		this.setEffectsLighting(1f);
		this.negativeFlashCounter.setEffect(30);
	}

	// Token: 0x06000B17 RID: 2839 RVA: 0x00035477 File Offset: 0x00033677
	public bool getNegativeFlashCounter()
	{
		return this.negativeFlashCounter.getEffect();
	}

	// Token: 0x06000B18 RID: 2840 RVA: 0x00035484 File Offset: 0x00033684
	public void setDamageFlashCounter()
	{
		this.damageFlashCounter.setEffect(10);
	}

	// Token: 0x06000B19 RID: 2841 RVA: 0x00035493 File Offset: 0x00033693
	public bool getDamageFlashCounter()
	{
		return this.damageFlashCounter.getEffect();
	}

	// Token: 0x06000B1A RID: 2842 RVA: 0x000354A0 File Offset: 0x000336A0
	public void setPositiveFlashCounter()
	{
		this.setEffectsLighting(1f);
		this.positiveFlashCounter.setEffect(30);
	}

	// Token: 0x06000B1B RID: 2843 RVA: 0x000354BA File Offset: 0x000336BA
	public bool getPositiveFlashCounter()
	{
		return this.positiveFlashCounter.getEffect();
	}

	// Token: 0x06000B1C RID: 2844 RVA: 0x000354C7 File Offset: 0x000336C7
	public void setSummoned()
	{
		this.setPositiveFlashCounter();
		this.setSmokeMedium();
	}

	// Token: 0x06000B1D RID: 2845 RVA: 0x000354D5 File Offset: 0x000336D5
	public void setForceHighlight(int i)
	{
		this.forceHighlight.setEffect(i);
	}

	// Token: 0x06000B1E RID: 2846 RVA: 0x000354E3 File Offset: 0x000336E3
	public bool getForceHighlight()
	{
		return this.forceHighlight.getEffect();
	}

	// Token: 0x06000B1F RID: 2847 RVA: 0x000354F0 File Offset: 0x000336F0
	public bool isForceHighlightActive()
	{
		return this.forceHighlight.getEffect();
	}

	// Token: 0x06000B20 RID: 2848 RVA: 0x000354FD File Offset: 0x000336FD
	private LightLevelControl getAndSetLightLevelControl()
	{
		if (this.lightLevelControl == null)
		{
			this.lightLevelControl = new LightLevelControl(8f);
			this.lightLevelControl.clearLightEffects();
		}
		return this.lightLevelControl;
	}

	// Token: 0x06000B21 RID: 2849 RVA: 0x00035528 File Offset: 0x00033728
	private void setLightningEffect(SkaldPhysicalObject target)
	{
		int emitterX = this.owner.getEmitterX();
		int emitterY = this.owner.getEmitterY();
		int emitterX2 = target.getEmitterX();
		int emitterY2 = target.getEmitterY();
		this.setEffectsLighting(1f);
		this.getAndSetParticleSystem().addLightning(emitterX, emitterY, emitterX2, emitterY2);
	}

	// Token: 0x06000B22 RID: 2850 RVA: 0x00035578 File Offset: 0x00033778
	public void setArrowEffect(SkaldPhysicalObject target)
	{
		if (target == null)
		{
			return;
		}
		int emitterX = this.owner.getEmitterX();
		int emitterY = this.owner.getEmitterY();
		int emitterX2 = target.getEmitterX();
		int emitterY2 = target.getEmitterY();
		this.getAndSetParticleSystem().addArrow(emitterX, emitterY, emitterX2, emitterY2);
	}

	// Token: 0x06000B23 RID: 2851 RVA: 0x000355C0 File Offset: 0x000337C0
	private void setHomingParticleEffect(SkaldPhysicalObject target)
	{
		int emitterX = this.owner.getEmitterX();
		int emitterY = this.owner.getEmitterY();
		int emitterX2 = target.getEmitterX();
		int emitterY2 = target.getEmitterY();
		this.getAndSetParticleSystem().addHomingParticle(emitterX, emitterY, emitterX2, emitterY2);
	}

	// Token: 0x06000B24 RID: 2852 RVA: 0x00035604 File Offset: 0x00033804
	private void setFireFlyEffect()
	{
		int emitterX = this.owner.getEmitterX();
		int emitterY = this.owner.getEmitterY();
		this.getAndSetParticleSystem().addFireflyParticle(emitterX, emitterY);
	}

	// Token: 0x06000B25 RID: 2853 RVA: 0x00035638 File Offset: 0x00033838
	private void setCorpseFlyEffect()
	{
		int emitterX = this.owner.getEmitterX();
		int emitterY = this.owner.getEmitterY();
		this.getAndSetParticleSystem().addCorpseflyParticle(emitterX, emitterY);
	}

	// Token: 0x06000B26 RID: 2854 RVA: 0x0003566C File Offset: 0x0003386C
	public void setFireball()
	{
		int xStart = this.owner.getEmitterX();
		int yStart = this.owner.getEmitterY();
		if (this.owner is Character)
		{
			Character character = this.owner as Character;
			MapTile areaOfEffectBaseTile = character.getAreaOfEffectBaseTile();
			if (areaOfEffectBaseTile != null)
			{
				xStart = areaOfEffectBaseTile.getPixelX();
				yStart = areaOfEffectBaseTile.getPixelY();
			}
			else if (character.getTargetOpponent() != null)
			{
				xStart = character.getTargetOpponent().getPixelX();
				yStart = character.getTargetOpponent().getPixelY();
			}
		}
		this.getAndSetParticleSystem().addFireball(xStart, yStart);
	}

	// Token: 0x06000B27 RID: 2855 RVA: 0x000356F4 File Offset: 0x000338F4
	public void setAuraFire()
	{
		int emitterX = this.owner.getEmitterX();
		int emitterY = this.owner.getEmitterY();
		this.getAndSetParticleSystem().addFireball(emitterX, emitterY);
	}

	// Token: 0x06000B28 RID: 2856 RVA: 0x00035728 File Offset: 0x00033928
	public void setAuraIce()
	{
		int emitterX = this.owner.getEmitterX();
		int emitterY = this.owner.getEmitterY();
		this.getAndSetParticleSystem().addAuraIce(emitterX, emitterY);
	}

	// Token: 0x06000B29 RID: 2857 RVA: 0x0003575C File Offset: 0x0003395C
	public void setSprayFireLine()
	{
		if (!(this.owner is Character))
		{
			return;
		}
		MapTile areaOfEffectBaseTile = (this.owner as Character).getAreaOfEffectBaseTile();
		if (areaOfEffectBaseTile == null)
		{
			return;
		}
		int emitterX = this.owner.getEmitterX();
		int emitterY = this.owner.getEmitterY();
		int pixelX = areaOfEffectBaseTile.getPixelX();
		int pixelY = areaOfEffectBaseTile.getPixelY();
		this.getAndSetParticleSystem().addSprayFireLine(emitterX, emitterY, pixelX, pixelY);
	}

	// Token: 0x06000B2A RID: 2858 RVA: 0x000357C4 File Offset: 0x000339C4
	public void setSprayAcidLine()
	{
		if (!(this.owner is Character))
		{
			return;
		}
		MapTile areaOfEffectBaseTile = (this.owner as Character).getAreaOfEffectBaseTile();
		if (areaOfEffectBaseTile == null)
		{
			return;
		}
		int emitterX = this.owner.getEmitterX();
		int emitterY = this.owner.getEmitterY();
		int pixelX = areaOfEffectBaseTile.getPixelX();
		int pixelY = areaOfEffectBaseTile.getPixelY();
		this.getAndSetParticleSystem().addSprayAcidLine(emitterX, emitterY, pixelX, pixelY);
	}

	// Token: 0x06000B2B RID: 2859 RVA: 0x0003582C File Offset: 0x00033A2C
	public void setSprayIceLine()
	{
		if (!(this.owner is Character))
		{
			return;
		}
		MapTile areaOfEffectBaseTile = (this.owner as Character).getAreaOfEffectBaseTile();
		if (areaOfEffectBaseTile == null)
		{
			return;
		}
		int emitterX = this.owner.getEmitterX();
		int emitterY = this.owner.getEmitterY();
		int pixelX = areaOfEffectBaseTile.getPixelX();
		int pixelY = areaOfEffectBaseTile.getPixelY();
		this.getAndSetParticleSystem().addSprayIceLine(emitterX, emitterY, pixelX, pixelY);
	}

	// Token: 0x06000B2C RID: 2860 RVA: 0x00035894 File Offset: 0x00033A94
	public void setSprayAcidCone()
	{
		if (!(this.owner is Character))
		{
			return;
		}
		MapTile areaOfEffectBaseTile = (this.owner as Character).getAreaOfEffectBaseTile();
		if (areaOfEffectBaseTile == null)
		{
			return;
		}
		int emitterX = this.owner.getEmitterX();
		int emitterY = this.owner.getEmitterY();
		int pixelX = areaOfEffectBaseTile.getPixelX();
		int pixelY = areaOfEffectBaseTile.getPixelY();
		this.getAndSetParticleSystem().addSprayAcidCone(emitterX, emitterY, pixelX, pixelY);
	}

	// Token: 0x06000B2D RID: 2861 RVA: 0x000358FC File Offset: 0x00033AFC
	public void setSprayFireCone()
	{
		if (!(this.owner is Character))
		{
			return;
		}
		MapTile areaOfEffectBaseTile = (this.owner as Character).getAreaOfEffectBaseTile();
		if (areaOfEffectBaseTile == null)
		{
			return;
		}
		int emitterX = this.owner.getEmitterX();
		int emitterY = this.owner.getEmitterY();
		int pixelX = areaOfEffectBaseTile.getPixelX();
		int pixelY = areaOfEffectBaseTile.getPixelY();
		this.getAndSetParticleSystem().addSprayFireCone(emitterX, emitterY, pixelX, pixelY);
	}

	// Token: 0x06000B2E RID: 2862 RVA: 0x00035964 File Offset: 0x00033B64
	public void setSprayIceCone()
	{
		if (!(this.owner is Character))
		{
			return;
		}
		MapTile areaOfEffectBaseTile = (this.owner as Character).getAreaOfEffectBaseTile();
		if (areaOfEffectBaseTile == null)
		{
			return;
		}
		int emitterX = this.owner.getEmitterX();
		int emitterY = this.owner.getEmitterY();
		int pixelX = areaOfEffectBaseTile.getPixelX();
		int pixelY = areaOfEffectBaseTile.getPixelY();
		this.getAndSetParticleSystem().addSprayIceCone(emitterX, emitterY, pixelX, pixelY);
	}

	// Token: 0x06000B2F RID: 2863 RVA: 0x000359CC File Offset: 0x00033BCC
	public void setSplashAcid()
	{
		int emitterX = this.owner.getEmitterX();
		int emitterY = this.owner.getEmitterY();
		this.getAndSetParticleSystem().addSplashAcid(emitterX, emitterY);
	}

	// Token: 0x06000B30 RID: 2864 RVA: 0x00035A00 File Offset: 0x00033C00
	public void setSplashPoison()
	{
		int emitterX = this.owner.getEmitterX();
		int emitterY = this.owner.getEmitterY();
		this.getAndSetParticleSystem().addSplashPoison(emitterX, emitterY);
	}

	// Token: 0x06000B31 RID: 2865 RVA: 0x00035A34 File Offset: 0x00033C34
	public void setAuraGreen()
	{
		int emitterX = this.owner.getEmitterX();
		int emitterY = this.owner.getEmitterY();
		this.getAndSetParticleSystem().addAuraGreen(emitterX, emitterY);
	}

	// Token: 0x06000B32 RID: 2866 RVA: 0x00035A68 File Offset: 0x00033C68
	public void setAuraRed()
	{
		int emitterX = this.owner.getEmitterX();
		int emitterY = this.owner.getEmitterY();
		this.getAndSetParticleSystem().addAuraRed(emitterX, emitterY);
	}

	// Token: 0x06000B33 RID: 2867 RVA: 0x00035A9C File Offset: 0x00033C9C
	private void setRayEffect(SkaldPhysicalObject target)
	{
		int emitterX = this.owner.getEmitterX();
		int emitterY = this.owner.getEmitterY();
		int emitterX2 = target.getEmitterX();
		int emitterY2 = target.getEmitterY();
		this.getAndSetParticleSystem().addRay(emitterX, emitterY, emitterX2, emitterY2);
	}

	// Token: 0x06000B34 RID: 2868 RVA: 0x00035ADE File Offset: 0x00033CDE
	public void setShaken()
	{
		this.shaken.setEffect(16);
	}

	// Token: 0x06000B35 RID: 2869 RVA: 0x00035AED File Offset: 0x00033CED
	public bool getShaken()
	{
		return this.shaken.getEffect();
	}

	// Token: 0x06000B36 RID: 2870 RVA: 0x00035AFA File Offset: 0x00033CFA
	public void setBlood()
	{
		this.getAndSetParticleSystem().addParticlesBlood((float)this.owner.getEmitterX(), (float)this.owner.getEmitterY());
	}

	// Token: 0x06000B37 RID: 2871 RVA: 0x00035B1F File Offset: 0x00033D1F
	public void setSmokeMedium()
	{
		this.getAndSetParticleSystem().addSmokeMedium(this.owner.getEmitterX(), this.owner.getPixelY() + 4, 75);
	}

	// Token: 0x06000B38 RID: 2872 RVA: 0x00035B46 File Offset: 0x00033D46
	public void setFountainMedium()
	{
		this.getAndSetParticleSystem().addFountainMedium(this.owner.getEmitterX(), this.owner.getPixelY() + 21);
	}

	// Token: 0x06000B39 RID: 2873 RVA: 0x00035B6C File Offset: 0x00033D6C
	public void setFireFlashMedium(SkaldPhysicalObject target)
	{
		int emitterX = target.getEmitterX();
		int emitterY = target.getEmitterY();
		this.getAndSetParticleSystem().addFireFlashMedium(emitterX, emitterY);
		this.setEffectsLighting(1f);
	}

	// Token: 0x06000B3A RID: 2874 RVA: 0x00035B9F File Offset: 0x00033D9F
	public void setFlameContinualLarge()
	{
		this.getAndSetParticleSystem().addFlameContinualLarge(this.owner.getEmitterX(), this.owner.getEmitterY());
		this.setEffectsLighting(1f);
	}

	// Token: 0x06000B3B RID: 2875 RVA: 0x00035BCD File Offset: 0x00033DCD
	public void setFlameContinualMedium()
	{
		this.getAndSetParticleSystem().addFlameContinualMedium(this.owner.getEmitterX(), this.owner.getEmitterY());
		this.setEffectsLighting(1f);
	}

	// Token: 0x06000B3C RID: 2876 RVA: 0x00035BFB File Offset: 0x00033DFB
	public void setFlameContinualMediumGreen()
	{
		this.getAndSetParticleSystem().addFlameContinualMediumGreen(this.owner.getEmitterX(), this.owner.getEmitterY());
		this.setEffectsLighting(1f);
	}

	// Token: 0x06000B3D RID: 2877 RVA: 0x00035C29 File Offset: 0x00033E29
	public void setFlameContinualMediumPurple()
	{
		this.getAndSetParticleSystem().addFlameContinualMediumPurple(this.owner.getEmitterX(), this.owner.getEmitterY());
		this.setEffectsLighting(1f);
	}

	// Token: 0x06000B3E RID: 2878 RVA: 0x00035C57 File Offset: 0x00033E57
	public void setFlameContinualSmall()
	{
		this.getAndSetParticleSystem().addFlameContinualSmall(this.owner.getEmitterX(), this.owner.getEmitterY());
		this.setEffectsLighting(1f);
	}

	// Token: 0x06000B3F RID: 2879 RVA: 0x00035C85 File Offset: 0x00033E85
	public void setFlameContinualTiny()
	{
		this.getAndSetParticleSystem().addFlameContinualTiny(this.owner.getEmitterX(), this.owner.getEmitterY());
		this.setEffectsLighting(1f);
	}

	// Token: 0x06000B40 RID: 2880 RVA: 0x00035CB3 File Offset: 0x00033EB3
	public void setSweating()
	{
		this.getAndSetParticleSystem().addSweat((float)this.owner.getEmitterX(), (float)(this.owner.getEmitterY() + 4));
	}

	// Token: 0x040002F7 RID: 759
	private ParticleSystem particleSystem;

	// Token: 0x040002F8 RID: 760
	private LightLevelControl lightLevelControl;

	// Token: 0x040002F9 RID: 761
	private VisualEffectsControl.Effect negativeFlashCounter;

	// Token: 0x040002FA RID: 762
	private VisualEffectsControl.Effect positiveFlashCounter;

	// Token: 0x040002FB RID: 763
	private VisualEffectsControl.Effect damageFlashCounter;

	// Token: 0x040002FC RID: 764
	private VisualEffectsControl.Effect forceHighlight;

	// Token: 0x040002FD RID: 765
	private VisualEffectsControl.Effect shaken;

	// Token: 0x040002FE RID: 766
	private List<VisualEffectsControl.Effect> effects = new List<VisualEffectsControl.Effect>();

	// Token: 0x040002FF RID: 767
	private SkaldPhysicalObject owner;

	// Token: 0x0200023D RID: 573
	private class Effect
	{
		// Token: 0x06001901 RID: 6401 RVA: 0x0006D792 File Offset: 0x0006B992
		public virtual void setEffect(int amount)
		{
			this.effect = amount;
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x0006D79B File Offset: 0x0006B99B
		public bool getEffect()
		{
			return this.effect > 0;
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x0006D7A6 File Offset: 0x0006B9A6
		public void countDown()
		{
			if (this.effect > 0)
			{
				this.effect--;
			}
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x0006D7BF File Offset: 0x0006B9BF
		public bool isActive()
		{
			return this.effect > 0;
		}

		// Token: 0x040008B1 RID: 2225
		private int effect;
	}

	// Token: 0x0200023E RID: 574
	private class EffectBool : VisualEffectsControl.Effect
	{
		// Token: 0x06001906 RID: 6406 RVA: 0x0006D7D2 File Offset: 0x0006B9D2
		public void setEffect()
		{
			base.setEffect(1);
		}
	}
}
