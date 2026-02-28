using System;

// Token: 0x02000026 RID: 38
[Serializable]
public class CharacterAnimationContainer : BaseAnimationContainer
{
	// Token: 0x06000426 RID: 1062 RVA: 0x000133B8 File Offset: 0x000115B8
	public CharacterAnimationContainer()
	{
		this.setCombatAnimation("ANI_BaseCombat");
		this.setCloakAnimation("ANI_BaseCombat");
		this.setFallingAnimation("ANI_BaseFalling");
		this.setDeathAnimation("ANI_BaseDeath");
		this.setStunnedAnimation("ANI_BaseStunned");
		this.setPanicAnimation("ANI_PanicBase");
		this.setAimingAnimation("ANI_BaseAiming");
		this.setAlertAnimation("ANI_BaseAlert");
		this.setBloodiedAnimation("ANI_BaseAlert");
		this.setHiddenAnimation("ANI_IdleHidden");
		this.setExploreAnimation("ANI_Explore");
	}

	// Token: 0x06000427 RID: 1063 RVA: 0x00013444 File Offset: 0x00011644
	public FrameOffsetControl.OffsetData getHeadOffset(int i)
	{
		return FrameOffsetControl.getHeadOffset(i);
	}

	// Token: 0x06000428 RID: 1064 RVA: 0x0001344C File Offset: 0x0001164C
	public FrameOffsetControl.OffsetData getLegsOffset(int i)
	{
		return FrameOffsetControl.getLegsOffset(i);
	}

	// Token: 0x06000429 RID: 1065 RVA: 0x00013454 File Offset: 0x00011654
	public FrameOffsetControl.OffsetData getTorsoOffset(int i)
	{
		return FrameOffsetControl.getTorsoOffset(i);
	}

	// Token: 0x0600042A RID: 1066 RVA: 0x0001345C File Offset: 0x0001165C
	public FrameOffsetControl.OffsetData getCloakOffset(int i)
	{
		return FrameOffsetControl.getCloakOffset(i);
	}

	// Token: 0x0600042B RID: 1067 RVA: 0x00013464 File Offset: 0x00011664
	public FrameOffsetControl.OffsetData getArmsOffset(int i)
	{
		return FrameOffsetControl.getArmsOffset(i);
	}

	// Token: 0x0600042C RID: 1068 RVA: 0x0001346C File Offset: 0x0001166C
	public FrameOffsetControl.OffsetData getShieldOffset(int i)
	{
		return FrameOffsetControl.getShieldOffset(i);
	}

	// Token: 0x0600042D RID: 1069 RVA: 0x00013474 File Offset: 0x00011674
	public FrameOffsetControl.OffsetData getWeaponOffset(int i)
	{
		return FrameOffsetControl.getWeaponOffset(i);
	}

	// Token: 0x0600042E RID: 1070 RVA: 0x0001347C File Offset: 0x0001167C
	public FrameOffsetControl.OffsetData getBannerOffset(int i)
	{
		return FrameOffsetControl.getBannerOffset(i);
	}

	// Token: 0x0600042F RID: 1071 RVA: 0x00013484 File Offset: 0x00011684
	public FrameOffsetControl.OffsetData getGlobalOffset(int i)
	{
		return FrameOffsetControl.getGlobalOffset(i);
	}

	// Token: 0x06000430 RID: 1072 RVA: 0x0001348C File Offset: 0x0001168C
	public FrameOffsetControl.OffsetData getSwooshOffset(int i)
	{
		return FrameOffsetControl.getSwooshOffset(i);
	}

	// Token: 0x06000431 RID: 1073 RVA: 0x00013494 File Offset: 0x00011694
	public FrameOffsetControl.OffsetData getLightOffset(int i)
	{
		return FrameOffsetControl.getLightOffset(i);
	}

	// Token: 0x06000432 RID: 1074 RVA: 0x0001349C File Offset: 0x0001169C
	public void setCombatAnimation(string animationId)
	{
		base.setStripControl(ref this.combatAnimation, animationId);
	}

	// Token: 0x06000433 RID: 1075 RVA: 0x000134AB File Offset: 0x000116AB
	public int getCombatAnimation()
	{
		return this.combatAnimation.getFrame();
	}

	// Token: 0x06000434 RID: 1076 RVA: 0x000134B8 File Offset: 0x000116B8
	public int getCloakAnimation()
	{
		return this.cloakAnimation.getFrame();
	}

	// Token: 0x06000435 RID: 1077 RVA: 0x000134C5 File Offset: 0x000116C5
	public int getExploreAnimation()
	{
		return this.exploreAnimation.getFrame();
	}

	// Token: 0x06000436 RID: 1078 RVA: 0x000134D2 File Offset: 0x000116D2
	public void setExploreAnimation(string animationId)
	{
		base.setStripControl(ref this.exploreAnimation, animationId);
	}

	// Token: 0x06000437 RID: 1079 RVA: 0x000134E1 File Offset: 0x000116E1
	public void setOverlandAnimation(string animationId)
	{
		base.setStripControl(ref this.overlandAnimation, animationId);
	}

	// Token: 0x06000438 RID: 1080 RVA: 0x000134F0 File Offset: 0x000116F0
	public void setCloakAnimation(string animationId)
	{
		base.setStripControl(ref this.cloakAnimation, animationId);
	}

	// Token: 0x06000439 RID: 1081 RVA: 0x000134FF File Offset: 0x000116FF
	public int getOverlandAnimation()
	{
		if (this.overlandAnimation == null)
		{
			this.setOverlandAnimation("ANI_IdleOverland");
		}
		return this.overlandAnimation.getFrame();
	}

	// Token: 0x0600043A RID: 1082 RVA: 0x0001351F File Offset: 0x0001171F
	public int getIdleItemAnimation()
	{
		if (this.idleItemAnimation == null)
		{
			this.setIdleItemAnimation("ANI_IdleOverland");
		}
		return this.idleItemAnimation.getFrame();
	}

	// Token: 0x0600043B RID: 1083 RVA: 0x0001353F File Offset: 0x0001173F
	public void setIdleItemAnimation(string animationId)
	{
		base.setStripControl(ref this.idleItemAnimation, animationId);
	}

	// Token: 0x0600043C RID: 1084 RVA: 0x0001354E File Offset: 0x0001174E
	public void setFallingAnimation(string animationId)
	{
		base.setStripControl(ref this.fallingAnimation, animationId);
	}

	// Token: 0x0600043D RID: 1085 RVA: 0x0001355D File Offset: 0x0001175D
	public int getFallingAnimation()
	{
		return this.fallingAnimation.getFrame();
	}

	// Token: 0x0600043E RID: 1086 RVA: 0x0001356A File Offset: 0x0001176A
	public void setDeathAnimation(string animationId)
	{
		base.setStripControl(ref this.deathAnimation, animationId);
	}

	// Token: 0x0600043F RID: 1087 RVA: 0x00013579 File Offset: 0x00011779
	public int getDeathAnimation()
	{
		return this.deathAnimation.getFrame();
	}

	// Token: 0x06000440 RID: 1088 RVA: 0x00013586 File Offset: 0x00011786
	public void setStunnedAnimation(string animationId)
	{
		base.setStripControl(ref this.stunnedAnimation, animationId);
	}

	// Token: 0x06000441 RID: 1089 RVA: 0x00013595 File Offset: 0x00011795
	public int getStunnedAnimation()
	{
		return this.stunnedAnimation.getFrame();
	}

	// Token: 0x06000442 RID: 1090 RVA: 0x000135A2 File Offset: 0x000117A2
	public void setPanicAnimation(string animationId)
	{
		base.setStripControl(ref this.panicAnimation, animationId);
	}

	// Token: 0x06000443 RID: 1091 RVA: 0x000135B1 File Offset: 0x000117B1
	public int getPanicAnimation()
	{
		return this.panicAnimation.getFrame();
	}

	// Token: 0x06000444 RID: 1092 RVA: 0x000135BE File Offset: 0x000117BE
	public void setBloodiedAnimation(string animationId)
	{
		base.setStripControl(ref this.bloodiedAnimation, animationId);
	}

	// Token: 0x06000445 RID: 1093 RVA: 0x000135CD File Offset: 0x000117CD
	public int getBloodiedAnimation()
	{
		return this.bloodiedAnimation.getFrame();
	}

	// Token: 0x06000446 RID: 1094 RVA: 0x000135DA File Offset: 0x000117DA
	public void setHiddenAnimation(string animationId)
	{
		base.setStripControl(ref this.hiddenAnimation, animationId);
	}

	// Token: 0x06000447 RID: 1095 RVA: 0x000135E9 File Offset: 0x000117E9
	public int getHiddenAnimation()
	{
		return this.hiddenAnimation.getFrame();
	}

	// Token: 0x06000448 RID: 1096 RVA: 0x000135F6 File Offset: 0x000117F6
	public void setAimingAnimation(string animationId)
	{
		base.setStripControl(ref this.aimingAnimation, animationId);
	}

	// Token: 0x06000449 RID: 1097 RVA: 0x00013605 File Offset: 0x00011805
	public int getAimingAnimation()
	{
		return this.aimingAnimation.getFrame();
	}

	// Token: 0x0600044A RID: 1098 RVA: 0x00013612 File Offset: 0x00011812
	public void setAlertAnimation(string animationId)
	{
		base.setStripControl(ref this.alertAnimation, animationId);
	}

	// Token: 0x0600044B RID: 1099 RVA: 0x00013621 File Offset: 0x00011821
	public int getAlertAnimation()
	{
		return this.alertAnimation.getFrame();
	}

	// Token: 0x040000B8 RID: 184
	protected BaseAnimationContainer.BaseAnimationStripContainer combatAnimation;

	// Token: 0x040000B9 RID: 185
	protected BaseAnimationContainer.BaseAnimationStripContainer alertAnimation;

	// Token: 0x040000BA RID: 186
	protected BaseAnimationContainer.BaseAnimationStripContainer fallingAnimation;

	// Token: 0x040000BB RID: 187
	protected BaseAnimationContainer.BaseAnimationStripContainer aimingAnimation;

	// Token: 0x040000BC RID: 188
	protected BaseAnimationContainer.BaseAnimationStripContainer deathAnimation;

	// Token: 0x040000BD RID: 189
	protected BaseAnimationContainer.BaseAnimationStripContainer stunnedAnimation;

	// Token: 0x040000BE RID: 190
	protected BaseAnimationContainer.BaseAnimationStripContainer panicAnimation;

	// Token: 0x040000BF RID: 191
	protected BaseAnimationContainer.BaseAnimationStripContainer bloodiedAnimation;

	// Token: 0x040000C0 RID: 192
	protected BaseAnimationContainer.BaseAnimationStripContainer hiddenAnimation;

	// Token: 0x040000C1 RID: 193
	protected BaseAnimationContainer.BaseAnimationStripContainer exploreAnimation;

	// Token: 0x040000C2 RID: 194
	protected BaseAnimationContainer.BaseAnimationStripContainer overlandAnimation;

	// Token: 0x040000C3 RID: 195
	protected BaseAnimationContainer.BaseAnimationStripContainer idleItemAnimation;

	// Token: 0x040000C4 RID: 196
	protected BaseAnimationContainer.BaseAnimationStripContainer cloakAnimation;
}
