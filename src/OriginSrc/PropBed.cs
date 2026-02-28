using System;
using System.Runtime.Serialization;

// Token: 0x020000F6 RID: 246
[Serializable]
public class PropBed : PropActivatable, ISerializable
{
	// Token: 0x06001009 RID: 4105 RVA: 0x0004A35E File Offset: 0x0004855E
	public PropBed(SKALDProjectData.PropContainers.BedContainer.Bed rawData) : base(rawData)
	{
	}

	// Token: 0x0600100A RID: 4106 RVA: 0x0004A367 File Offset: 0x00048567
	public PropBed(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}

	// Token: 0x0600100B RID: 4107 RVA: 0x0004A374 File Offset: 0x00048574
	private new SKALDProjectData.PropContainers.BedContainer.Bed getRawData()
	{
		SKALDProjectData.PropContainers.Prop rawData = base.getRawData();
		if (rawData is SKALDProjectData.PropContainers.BedContainer.Bed)
		{
			return rawData as SKALDProjectData.PropContainers.BedContainer.Bed;
		}
		return null;
	}

	// Token: 0x0600100C RID: 4108 RVA: 0x0004A398 File Offset: 0x00048598
	public override string getVerb()
	{
		return "Rest";
	}

	// Token: 0x0600100D RID: 4109 RVA: 0x0004A39F File Offset: 0x0004859F
	public override string interactWithProp()
	{
		if (base.isHidden())
		{
			return "";
		}
		MainControl.getDataControl().makeCampWithBed();
		return base.processVerbTrigger();
	}
}
