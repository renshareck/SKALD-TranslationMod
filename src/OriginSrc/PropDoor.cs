using System;
using System.Runtime.Serialization;

// Token: 0x020000F9 RID: 249
[Serializable]
public class PropDoor : PropLockable, ISerializable
{
	// Token: 0x06001017 RID: 4119 RVA: 0x0004A468 File Offset: 0x00048668
	public PropDoor(SKALDProjectData.PropContainers.DoorContainer.Door rawData) : base(rawData)
	{
	}

	// Token: 0x06001018 RID: 4120 RVA: 0x0004A471 File Offset: 0x00048671
	public PropDoor(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}

	// Token: 0x06001019 RID: 4121 RVA: 0x0004A47C File Offset: 0x0004867C
	private new SKALDProjectData.PropContainers.DoorContainer.Door getRawData()
	{
		SKALDProjectData.PropContainers.Prop rawData = base.getRawData();
		if (rawData is SKALDProjectData.PropContainers.DoorContainer.Door)
		{
			return rawData as SKALDProjectData.PropContainers.DoorContainer.Door;
		}
		return null;
	}

	// Token: 0x0600101A RID: 4122 RVA: 0x0004A4A0 File Offset: 0x000486A0
	public override void activate()
	{
		if (!this.canActivate())
		{
			return;
		}
		base.makeImpassable();
		base.setNoVisibility();
		base.activate();
	}

	// Token: 0x0600101B RID: 4123 RVA: 0x0004A4BD File Offset: 0x000486BD
	public override void deactivate()
	{
		if (!this.canDeactivate())
		{
			return;
		}
		if (base.tryLock())
		{
			return;
		}
		base.makePassable();
		base.clearNoVisibility();
		base.deactivate();
	}

	// Token: 0x0600101C RID: 4124 RVA: 0x0004A4E4 File Offset: 0x000486E4
	protected override string getDeactivateSoundPath()
	{
		string text = this.getRawData().deactivateSound;
		if (text == "")
		{
			text = "DoorWood1";
		}
		return text;
	}

	// Token: 0x0600101D RID: 4125 RVA: 0x0004A511 File Offset: 0x00048711
	public override bool shouldBeHighlighted()
	{
		return base.isActive() && base.shouldBeHighlighted();
	}

	// Token: 0x0600101E RID: 4126 RVA: 0x0004A523 File Offset: 0x00048723
	public override string getVerb()
	{
		if (base.isActive())
		{
			return "Open";
		}
		return "Close";
	}
}
