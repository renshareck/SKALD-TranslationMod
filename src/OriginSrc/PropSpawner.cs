using System;
using System.Runtime.Serialization;

// Token: 0x020000FF RID: 255
[Serializable]
public class PropSpawner : Prop, ISerializable
{
	// Token: 0x06001051 RID: 4177 RVA: 0x0004AD6C File Offset: 0x00048F6C
	public PropSpawner(SKALDProjectData.PropContainers.SpawnerContainer.Spawner rawData) : base(rawData)
	{
	}

	// Token: 0x06001052 RID: 4178 RVA: 0x0004AD75 File Offset: 0x00048F75
	public PropSpawner(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}

	// Token: 0x06001053 RID: 4179 RVA: 0x0004AD80 File Offset: 0x00048F80
	private new SKALDProjectData.PropContainers.SpawnerContainer.Spawner getRawData()
	{
		SKALDProjectData.PropContainers.Prop rawData = base.getRawData();
		if (rawData is SKALDProjectData.PropContainers.SpawnerContainer.Spawner)
		{
			return rawData as SKALDProjectData.PropContainers.SpawnerContainer.Spawner;
		}
		return null;
	}
}
