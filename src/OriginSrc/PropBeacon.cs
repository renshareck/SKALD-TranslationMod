using System;
using System.Runtime.Serialization;

// Token: 0x020000F5 RID: 245
[Serializable]
public class PropBeacon : Prop, ISerializable
{
	// Token: 0x06001007 RID: 4103 RVA: 0x0004A34B File Offset: 0x0004854B
	public PropBeacon(SKALDProjectData.PropContainers.BeaconContainer.Beacon rawData) : base(rawData)
	{
	}

	// Token: 0x06001008 RID: 4104 RVA: 0x0004A354 File Offset: 0x00048554
	public PropBeacon(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}
}
