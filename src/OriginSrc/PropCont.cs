using System;
using System.Runtime.Serialization;

// Token: 0x020000F7 RID: 247
[Serializable]
public class PropCont : PropLockable, ISerializable
{
	// Token: 0x0600100E RID: 4110 RVA: 0x0004A3BF File Offset: 0x000485BF
	public PropCont(SKALDProjectData.PropContainers.ContainerContainer.Container rawData) : base(rawData)
	{
	}

	// Token: 0x0600100F RID: 4111 RVA: 0x0004A3C8 File Offset: 0x000485C8
	public PropCont(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}

	// Token: 0x06001010 RID: 4112 RVA: 0x0004A3D4 File Offset: 0x000485D4
	private new SKALDProjectData.PropContainers.ContainerContainer.Container getRawData()
	{
		SKALDProjectData.PropContainers.Prop rawData = base.getRawData();
		if (rawData is SKALDProjectData.PropContainers.ContainerContainer.Container)
		{
			return rawData as SKALDProjectData.PropContainers.ContainerContainer.Container;
		}
		return null;
	}

	// Token: 0x06001011 RID: 4113 RVA: 0x0004A3F8 File Offset: 0x000485F8
	public override bool isContainer()
	{
		return true;
	}

	// Token: 0x06001012 RID: 4114 RVA: 0x0004A3FC File Offset: 0x000485FC
	public override string getLoadOutId()
	{
		SKALDProjectData.PropContainers.ContainerContainer.Container rawData = this.getRawData();
		if (rawData == null)
		{
			return "";
		}
		return rawData.loadout;
	}

	// Token: 0x06001013 RID: 4115 RVA: 0x0004A41F File Offset: 0x0004861F
	public override void deactivate()
	{
		if (base.tryLock())
		{
			return;
		}
		base.deactivate();
	}
}
