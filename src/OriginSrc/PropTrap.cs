using System;
using System.Runtime.Serialization;

// Token: 0x02000101 RID: 257
[Serializable]
public class PropTrap : PropActivatable, ISerializable
{
	// Token: 0x06001064 RID: 4196 RVA: 0x0004AF5E File Offset: 0x0004915E
	public PropTrap(SKALDProjectData.PropContainers.TrapContainer.Trap rawData) : base(rawData)
	{
	}

	// Token: 0x06001065 RID: 4197 RVA: 0x0004AF67 File Offset: 0x00049167
	public PropTrap(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}

	// Token: 0x06001066 RID: 4198 RVA: 0x0004AF74 File Offset: 0x00049174
	private new SKALDProjectData.PropContainers.TrapContainer.Trap getRawData()
	{
		SKALDProjectData.PropContainers.Prop rawData = base.getRawData();
		if (rawData is SKALDProjectData.PropContainers.TrapContainer.Trap)
		{
			return rawData as SKALDProjectData.PropContainers.TrapContainer.Trap;
		}
		return null;
	}
}
