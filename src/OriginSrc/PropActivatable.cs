using System;
using System.Runtime.Serialization;
using UnityEngine;

// Token: 0x020000F4 RID: 244
[Serializable]
public abstract class PropActivatable : Prop, ISerializable
{
	// Token: 0x06000FEC RID: 4076 RVA: 0x0004A00D File Offset: 0x0004820D
	public PropActivatable(SKALDProjectData.PropContainers.ActivatableProp rawData) : base(rawData)
	{
		this.dynamicData.activated = rawData.active;
		this.dynamicData.trap = rawData.trap;
		this.dynamicData.verb = rawData.verb;
	}

	// Token: 0x06000FED RID: 4077 RVA: 0x0004A049 File Offset: 0x00048249
	public PropActivatable()
	{
	}

	// Token: 0x06000FEE RID: 4078 RVA: 0x0004A051 File Offset: 0x00048251
	public PropActivatable(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}

	// Token: 0x06000FEF RID: 4079 RVA: 0x0004A05C File Offset: 0x0004825C
	protected override string getVerbTrigger()
	{
		SKALDProjectData.PropContainers.ActivatableProp rawData = this.getRawData();
		if (rawData != null)
		{
			return rawData.verbTrigger;
		}
		return "";
	}

	// Token: 0x06000FF0 RID: 4080 RVA: 0x0004A080 File Offset: 0x00048280
	protected override string getDeactivatedVerbTrigger()
	{
		SKALDProjectData.PropContainers.ActivatableProp rawData = this.getRawData();
		if (rawData != null)
		{
			return rawData.deactivatedVerbTrigger;
		}
		return "";
	}

	// Token: 0x06000FF1 RID: 4081 RVA: 0x0004A0A4 File Offset: 0x000482A4
	protected override string getActiveVerbTrigger()
	{
		SKALDProjectData.PropContainers.ActivatableProp rawData = this.getRawData();
		if (rawData != null)
		{
			return rawData.activeVerbTrigger;
		}
		return "";
	}

	// Token: 0x06000FF2 RID: 4082 RVA: 0x0004A0C7 File Offset: 0x000482C7
	public bool hasTrap()
	{
		return this.dynamicData.trap != null && this.dynamicData.trap != "";
	}

	// Token: 0x06000FF3 RID: 4083 RVA: 0x0004A0ED File Offset: 0x000482ED
	public void clearTrap()
	{
		this.dynamicData.trap = "";
	}

	// Token: 0x06000FF4 RID: 4084 RVA: 0x0004A0FF File Offset: 0x000482FF
	public void triggerTrap()
	{
		MainControl.getDataControl().takeDamage("5");
		MainControl.getDataControl().shakeScreen("30");
		this.clearTrap();
	}

	// Token: 0x06000FF5 RID: 4085 RVA: 0x0004A128 File Offset: 0x00048328
	protected override string getActivatedTrigger()
	{
		SKALDProjectData.PropContainers.ActivatableProp rawData = this.getRawData();
		if (rawData != null)
		{
			return rawData.activationTrigger;
		}
		return "";
	}

	// Token: 0x06000FF6 RID: 4086 RVA: 0x0004A14C File Offset: 0x0004834C
	public override bool isPlayerInteractive()
	{
		SKALDProjectData.PropContainers.ActivatableProp rawData = this.getRawData();
		if (rawData != null)
		{
			return rawData.playerInteractive;
		}
		return base.isPlayerInteractive();
	}

	// Token: 0x06000FF7 RID: 4087 RVA: 0x0004A170 File Offset: 0x00048370
	protected override bool canActivate()
	{
		if (base.isActive())
		{
			return false;
		}
		SKALDProjectData.PropContainers.ActivatableProp rawData = this.getRawData();
		if (rawData != null)
		{
			return rawData.canActivate;
		}
		return base.canActivate();
	}

	// Token: 0x06000FF8 RID: 4088 RVA: 0x0004A19E File Offset: 0x0004839E
	public override bool shouldBeAnimated()
	{
		return base.isActive() && base.shouldBeAnimated();
	}

	// Token: 0x06000FF9 RID: 4089 RVA: 0x0004A1B0 File Offset: 0x000483B0
	protected override bool canDeactivate()
	{
		if (!base.isActive())
		{
			return false;
		}
		SKALDProjectData.PropContainers.ActivatableProp rawData = this.getRawData();
		if (rawData != null)
		{
			return rawData.canDeactivate;
		}
		return base.canDeactivate();
	}

	// Token: 0x06000FFA RID: 4090 RVA: 0x0004A1DE File Offset: 0x000483DE
	public override Color32 getHighlightedColor()
	{
		if (base.isActive())
		{
			return C64Color.Yellow;
		}
		return C64Color.GrayLight;
	}

	// Token: 0x06000FFB RID: 4091 RVA: 0x0004A1F4 File Offset: 0x000483F4
	protected override string getDeactivatedTrigger()
	{
		SKALDProjectData.PropContainers.ActivatableProp rawData = this.getRawData();
		if (rawData != null)
		{
			return rawData.deactivationTrigger;
		}
		return "";
	}

	// Token: 0x06000FFC RID: 4092 RVA: 0x0004A218 File Offset: 0x00048418
	protected bool shouldAutoToggle()
	{
		SKALDProjectData.PropContainers.ActivatableProp rawData = this.getRawData();
		return rawData == null || rawData.autoToggle;
	}

	// Token: 0x06000FFD RID: 4093 RVA: 0x0004A238 File Offset: 0x00048438
	protected new SKALDProjectData.PropContainers.ActivatableProp getRawData()
	{
		SKALDProjectData.PropContainers.Prop rawData = base.getRawData();
		if (rawData is SKALDProjectData.PropContainers.ActivatableProp)
		{
			return rawData as SKALDProjectData.PropContainers.ActivatableProp;
		}
		return null;
	}

	// Token: 0x06000FFE RID: 4094 RVA: 0x0004A25C File Offset: 0x0004845C
	private void playActivateSound()
	{
		AudioControl.playSound(this.getActivateSoundPath());
	}

	// Token: 0x06000FFF RID: 4095 RVA: 0x0004A269 File Offset: 0x00048469
	protected virtual string getDeactivateSoundPath()
	{
		return this.getRawData().deactivateSound;
	}

	// Token: 0x06001000 RID: 4096 RVA: 0x0004A276 File Offset: 0x00048476
	protected virtual string getActivateSoundPath()
	{
		return this.getRawData().activateSound;
	}

	// Token: 0x06001001 RID: 4097 RVA: 0x0004A283 File Offset: 0x00048483
	private void playDeactivateSound()
	{
		AudioControl.playSound(this.getDeactivateSoundPath());
	}

	// Token: 0x06001002 RID: 4098 RVA: 0x0004A290 File Offset: 0x00048490
	protected virtual void setDeactivateImage()
	{
		string deactivatedImage = this.getRawData().deactivatedImage;
		if (deactivatedImage != "")
		{
			this.setModelPath(deactivatedImage);
		}
	}

	// Token: 0x06001003 RID: 4099 RVA: 0x0004A2C0 File Offset: 0x000484C0
	protected void setActivateImage()
	{
		string modelPath = this.getRawData().modelPath;
		if (modelPath != "")
		{
			this.setModelPath(modelPath);
		}
	}

	// Token: 0x06001004 RID: 4100 RVA: 0x0004A2ED File Offset: 0x000484ED
	public override void activate()
	{
		if (!this.canActivate())
		{
			return;
		}
		this.playActivateSound();
		this.setActivateImage();
		base.activate();
	}

	// Token: 0x06001005 RID: 4101 RVA: 0x0004A30A File Offset: 0x0004850A
	public override void deactivate()
	{
		if (!this.canDeactivate())
		{
			return;
		}
		this.playDeactivateSound();
		this.setDeactivateImage();
		base.deactivate();
	}

	// Token: 0x06001006 RID: 4102 RVA: 0x0004A327 File Offset: 0x00048527
	public override string interactWithProp()
	{
		if (base.isHidden())
		{
			return "";
		}
		string result = base.interactWithProp();
		if (this.shouldAutoToggle())
		{
			base.toggle();
		}
		return result;
	}
}
