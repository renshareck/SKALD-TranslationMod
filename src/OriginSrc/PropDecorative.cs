using System;
using System.Runtime.Serialization;

// Token: 0x020000F8 RID: 248
[Serializable]
public class PropDecorative : Prop, ISerializable
{
	// Token: 0x06001014 RID: 4116 RVA: 0x0004A430 File Offset: 0x00048630
	public PropDecorative(SKALDProjectData.PropContainers.DecorativeContainer.Decorative rawData) : base(rawData)
	{
	}

	// Token: 0x06001015 RID: 4117 RVA: 0x0004A439 File Offset: 0x00048639
	public PropDecorative(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}

	// Token: 0x06001016 RID: 4118 RVA: 0x0004A444 File Offset: 0x00048644
	private new SKALDProjectData.PropContainers.DecorativeContainer.Decorative getRawData()
	{
		SKALDProjectData.PropContainers.Prop rawData = base.getRawData();
		if (rawData is SKALDProjectData.PropContainers.DecorativeContainer.Decorative)
		{
			return rawData as SKALDProjectData.PropContainers.DecorativeContainer.Decorative;
		}
		return null;
	}
}
