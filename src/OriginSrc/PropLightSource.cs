using System;
using System.Runtime.Serialization;
using UnityEngine;

// Token: 0x020000FC RID: 252
[Serializable]
public class PropLightSource : PropActivatable, ISerializable
{
	// Token: 0x06001026 RID: 4134 RVA: 0x0004A5F4 File Offset: 0x000487F4
	public PropLightSource(SKALDProjectData.PropContainers.LightSourceContainer.LightSource rawData) : base(rawData)
	{
	}

	// Token: 0x06001027 RID: 4135 RVA: 0x0004A5FD File Offset: 0x000487FD
	public PropLightSource(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}

	// Token: 0x06001028 RID: 4136 RVA: 0x0004A608 File Offset: 0x00048808
	private new SKALDProjectData.PropContainers.LightSourceContainer.LightSource getRawData()
	{
		SKALDProjectData.PropContainers.Prop rawData = base.getRawData();
		if (rawData is SKALDProjectData.PropContainers.LightSourceContainer.LightSource)
		{
			return rawData as SKALDProjectData.PropContainers.LightSourceContainer.LightSource;
		}
		return null;
	}

	// Token: 0x06001029 RID: 4137 RVA: 0x0004A62C File Offset: 0x0004882C
	public override int getEmitterX()
	{
		SKALDProjectData.PropContainers.LightSourceContainer.LightSource rawData = this.getRawData();
		int num = 0;
		if (rawData != null)
		{
			num += rawData.emitterX;
		}
		return base.getEmitterX() + num;
	}

	// Token: 0x0600102A RID: 4138 RVA: 0x0004A658 File Offset: 0x00048858
	public override int getEmitterY()
	{
		SKALDProjectData.PropContainers.LightSourceContainer.LightSource rawData = this.getRawData();
		int num = 0;
		if (rawData != null)
		{
			num += rawData.emitterY;
		}
		return base.getEmitterY() + num;
	}

	// Token: 0x0600102B RID: 4139 RVA: 0x0004A684 File Offset: 0x00048884
	private string getParticleEffect()
	{
		SKALDProjectData.PropContainers.LightSourceContainer.LightSource rawData = this.getRawData();
		if (rawData != null)
		{
			return rawData.particleEffect;
		}
		return "";
	}

	// Token: 0x0600102C RID: 4140 RVA: 0x0004A6A8 File Offset: 0x000488A8
	private string getDeactivateParticleEffect()
	{
		SKALDProjectData.PropContainers.LightSourceContainer.LightSource rawData = this.getRawData();
		if (rawData != null)
		{
			return rawData.deactivateEffect;
		}
		return "";
	}

	// Token: 0x0600102D RID: 4141 RVA: 0x0004A6CB File Offset: 0x000488CB
	public override Color32 getHighlightedColor()
	{
		return C64Color.GrayLight;
	}

	// Token: 0x0600102E RID: 4142 RVA: 0x0004A6D4 File Offset: 0x000488D4
	public override void updateParticleEffects(int xOffset, int yOffset, TextureTools.TextureData targetTexture)
	{
		if (base.isActive() && !base.getVisualEffects().areAnyEffectsActive())
		{
			base.getVisualEffects().setContinualEffectFromString(this.getParticleEffect());
		}
		else if (!base.isActive() && base.getVisualEffects().areAnyPerpetualEffectsActive())
		{
			base.getVisualEffects().setParticleLife(15);
			base.getVisualEffects().setContinualEffectFromString(this.getDeactivateParticleEffect());
		}
		base.updateParticleEffects(xOffset, yOffset, targetTexture);
	}

	// Token: 0x0600102F RID: 4143 RVA: 0x0004A745 File Offset: 0x00048945
	public override string getVerb()
	{
		if (base.isActive())
		{
			return "Douse";
		}
		return "Light";
	}

	// Token: 0x06001030 RID: 4144 RVA: 0x0004A75A File Offset: 0x0004895A
	protected override string getActivateSoundPath()
	{
		return "FireOn1";
	}

	// Token: 0x06001031 RID: 4145 RVA: 0x0004A761 File Offset: 0x00048961
	protected override string getDeactivateSoundPath()
	{
		return "FireOff1";
	}

	// Token: 0x06001032 RID: 4146 RVA: 0x0004A768 File Offset: 0x00048968
	public override int getLight()
	{
		if (base.isActive() && !this.shouldBeRemovedFromGame())
		{
			return this.dynamicData.light;
		}
		return 0;
	}
}
