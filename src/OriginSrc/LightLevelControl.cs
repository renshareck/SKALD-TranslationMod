using System;
using UnityEngine;

// Token: 0x020000F0 RID: 240
public class LightLevelControl
{
	// Token: 0x06000EF4 RID: 3828 RVA: 0x00046BB5 File Offset: 0x00044DB5
	public LightLevelControl()
	{
	}

	// Token: 0x06000EF5 RID: 3829 RVA: 0x00046BD3 File Offset: 0x00044DD3
	public LightLevelControl(float fadeFrames)
	{
		this.fadeFrames = fadeFrames;
	}

	// Token: 0x06000EF6 RID: 3830 RVA: 0x00046BF8 File Offset: 0x00044DF8
	public bool isIlluminated()
	{
		return (double)this.getLightLevel() > 0.5;
	}

	// Token: 0x06000EF7 RID: 3831 RVA: 0x00046C0C File Offset: 0x00044E0C
	public float getLightLevel()
	{
		return Mathf.Clamp(this.lightLevel + this.lightEffects, 0f, 1f);
	}

	// Token: 0x06000EF8 RID: 3832 RVA: 0x00046C2A File Offset: 0x00044E2A
	public float getTargetLightLevel()
	{
		return this.targetLightLevel;
	}

	// Token: 0x06000EF9 RID: 3833 RVA: 0x00046C32 File Offset: 0x00044E32
	public void setLightEffectsLevel(float newLevel)
	{
		newLevel = Mathf.Clamp(newLevel, 0f, 1f);
		if (newLevel > this.lightEffects)
		{
			this.lightEffects = newLevel;
		}
	}

	// Token: 0x06000EFA RID: 3834 RVA: 0x00046C56 File Offset: 0x00044E56
	public void clearLightEffects()
	{
		this.lightEffects = 0f;
	}

	// Token: 0x06000EFB RID: 3835 RVA: 0x00046C63 File Offset: 0x00044E63
	public void setLightLevel(float newLevel)
	{
		if (newLevel <= this.targetLightLevel)
		{
			return;
		}
		this.targetLightLevel = newLevel;
		this.targetLightLevel = Mathf.Clamp(this.targetLightLevel, 0f, 1f);
		this.calculateFadeDelta();
	}

	// Token: 0x06000EFC RID: 3836 RVA: 0x00046C97 File Offset: 0x00044E97
	public void clearIlluminated()
	{
		this.targetLightLevel = 0f;
		if (this.lightLevel != 0f)
		{
			this.calculateFadeDelta();
			return;
		}
		this.fadeDelta = 0f;
	}

	// Token: 0x06000EFD RID: 3837 RVA: 0x00046CC3 File Offset: 0x00044EC3
	private void calculateFadeDelta()
	{
		this.fadeDelta = (this.getTotalTargetLightLevel() - this.lightLevel) / this.fadeFrames;
	}

	// Token: 0x06000EFE RID: 3838 RVA: 0x00046CE0 File Offset: 0x00044EE0
	public void updateLightLevel()
	{
		if (this.getTotalTargetLightLevel() == this.lightLevel)
		{
			return;
		}
		this.lightLevel += this.fadeDelta;
		float num = Mathf.Abs(this.fadeDelta);
		if (this.lightLevel < this.getTotalTargetLightLevel() + num && this.lightLevel > this.getTotalTargetLightLevel() - num)
		{
			this.arrive();
		}
		this.lightLevel = Mathf.Clamp(this.lightLevel, 0f, 1f);
	}

	// Token: 0x06000EFF RID: 3839 RVA: 0x00046D5C File Offset: 0x00044F5C
	public float getTotalTargetLightLevel()
	{
		return Mathf.Clamp(this.targetLightLevel, 0f, 1f);
	}

	// Token: 0x06000F00 RID: 3840 RVA: 0x00046D73 File Offset: 0x00044F73
	private void arrive()
	{
		this.lightLevel = this.getTotalTargetLightLevel();
		this.fadeDelta = 0f;
	}

	// Token: 0x06000F01 RID: 3841 RVA: 0x00046D8C File Offset: 0x00044F8C
	internal string printDescription()
	{
		return string.Concat(new string[]
		{
			"LL: ",
			this.lightLevel.ToString(),
			"\nEL: ",
			this.lightEffects.ToString(),
			"\nDark: ",
			(!this.isIlluminated()).ToString()
		});
	}

	// Token: 0x040003DE RID: 990
	private float lightLevel;

	// Token: 0x040003DF RID: 991
	private float targetLightLevel;

	// Token: 0x040003E0 RID: 992
	private float lightEffects;

	// Token: 0x040003E1 RID: 993
	private float fadeDelta = 1f;

	// Token: 0x040003E2 RID: 994
	private float fadeFrames = 15f;
}
