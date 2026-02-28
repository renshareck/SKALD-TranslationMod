using System;

// Token: 0x02000025 RID: 37
[Serializable]
public class BaseAnimationContainer
{
	// Token: 0x0600041B RID: 1051 RVA: 0x000132C8 File Offset: 0x000114C8
	public BaseAnimationContainer()
	{
		this.setBaseAnimation("ANI_BaseAlert");
	}

	// Token: 0x0600041C RID: 1052 RVA: 0x000132DB File Offset: 0x000114DB
	public void setBaseAnimation(string animationId)
	{
		this.setStripControl(ref this.baseAnimation, animationId);
	}

	// Token: 0x0600041D RID: 1053 RVA: 0x000132EA File Offset: 0x000114EA
	public void setBaseNotAlertAnimation(string animationId)
	{
		this.setStripControl(ref this.baseNotAlertAnimation, animationId);
	}

	// Token: 0x0600041E RID: 1054 RVA: 0x000132F9 File Offset: 0x000114F9
	protected void setStripControl(ref BaseAnimationContainer.BaseAnimationStripContainer container, string animationId)
	{
		if (container == null)
		{
			container = new BaseAnimationContainer.BaseAnimationStripContainer();
		}
		container.setAnimation(animationId);
	}

	// Token: 0x0600041F RID: 1055 RVA: 0x0001330E File Offset: 0x0001150E
	public int getBaseAnimation()
	{
		return this.baseAnimation.getFrame();
	}

	// Token: 0x06000420 RID: 1056 RVA: 0x0001331B File Offset: 0x0001151B
	public int getBaseNotAlertAnimation()
	{
		if (this.baseNotAlertAnimation == null)
		{
			this.setBaseNotAlertAnimation("ANI_BaseIdle");
		}
		return this.baseNotAlertAnimation.getFrame();
	}

	// Token: 0x06000421 RID: 1057 RVA: 0x0001333B File Offset: 0x0001153B
	public bool hasDynamicAnimation()
	{
		return this.dynamicAnimationContainer != null && this.dynamicAnimationContainer.hasDynamicAnimation();
	}

	// Token: 0x06000422 RID: 1058 RVA: 0x00013352 File Offset: 0x00011552
	public int getDynamicAnimationFrame()
	{
		if (this.dynamicAnimationContainer == null)
		{
			return 0;
		}
		return this.dynamicAnimationContainer.getDynamicAnimationFrame();
	}

	// Token: 0x06000423 RID: 1059 RVA: 0x00013369 File Offset: 0x00011569
	public void setDynamicAnimation(string id)
	{
		if (this.dynamicAnimationContainer == null)
		{
			this.dynamicAnimationContainer = new BaseAnimationContainer.DynamicAnimationContainer();
		}
		this.dynamicAnimationContainer.setAnimation(id);
	}

	// Token: 0x06000424 RID: 1060 RVA: 0x0001338A File Offset: 0x0001158A
	public void clearDynamicAnimation()
	{
		if (this.dynamicAnimationContainer != null)
		{
			this.dynamicAnimationContainer.clearDynamicAnimation();
		}
	}

	// Token: 0x06000425 RID: 1061 RVA: 0x0001339F File Offset: 0x0001159F
	public bool shouldIWaitForAnimation()
	{
		return this.hasDynamicAnimation() && this.dynamicAnimationContainer.willAnimationTerminate();
	}

	// Token: 0x040000B5 RID: 181
	private BaseAnimationContainer.DynamicAnimationContainer dynamicAnimationContainer;

	// Token: 0x040000B6 RID: 182
	protected BaseAnimationContainer.BaseAnimationStripContainer baseAnimation;

	// Token: 0x040000B7 RID: 183
	protected BaseAnimationContainer.BaseAnimationStripContainer baseNotAlertAnimation;

	// Token: 0x020001CB RID: 459
	[Serializable]
	protected class BaseAnimationStripContainer
	{
		// Token: 0x0600169E RID: 5790 RVA: 0x00065794 File Offset: 0x00063994
		public int getFrame()
		{
			if (this.animation == null && this.animationId != null && this.animationId != "")
			{
				this.animation = GameData.getAnimationData(this.animationId);
			}
			if (this.animation == null)
			{
				return 0;
			}
			return this.animation.getCurrentFrame();
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x000657EC File Offset: 0x000639EC
		public void setAnimation(string id)
		{
			if (this.animationId == id)
			{
				return;
			}
			AnimationStrip animationData = GameData.getAnimationData(id);
			if (animationData != null)
			{
				this.animation = animationData;
				this.animationId = id;
			}
		}

		// Token: 0x04000711 RID: 1809
		private AnimationStrip animation;

		// Token: 0x04000712 RID: 1810
		private string animationId;
	}

	// Token: 0x020001CC RID: 460
	[Serializable]
	private class DynamicAnimationContainer
	{
		// Token: 0x060016A1 RID: 5793 RVA: 0x00065828 File Offset: 0x00063A28
		public bool hasDynamicAnimation()
		{
			return this.animation != null;
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x00065834 File Offset: 0x00063A34
		public int getDynamicAnimationFrame()
		{
			if (!this.hasDynamicAnimation())
			{
				return 0;
			}
			int currentFrame = this.animation.getCurrentFrame();
			if (this.animation.isDead())
			{
				string endOfLoopTarget = this.animation.endOfLoopTarget;
				this.clearDynamicAnimation();
				if (endOfLoopTarget != "")
				{
					this.setAnimation(endOfLoopTarget);
				}
			}
			return currentFrame;
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x0006588C File Offset: 0x00063A8C
		public void setAnimation(string id)
		{
			AnimationStrip animationData = GameData.getAnimationData(id);
			if (animationData != null)
			{
				this.animation = animationData;
			}
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x000658AA File Offset: 0x00063AAA
		public void clearDynamicAnimation()
		{
			this.animation = null;
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x000658B3 File Offset: 0x00063AB3
		public bool willAnimationTerminate()
		{
			return this.animation == null || this.animation.willAnimationTerminate();
		}

		// Token: 0x04000713 RID: 1811
		private AnimationStrip animation;
	}
}
