using System;
using System.Runtime.Serialization;

// Token: 0x020000FB RID: 251
[Serializable]
public class PropInteractable : PropActivatable, ISerializable
{
	// Token: 0x06001023 RID: 4131 RVA: 0x0004A5BC File Offset: 0x000487BC
	public PropInteractable(SKALDProjectData.PropContainers.InteractableContainer.Interactable rawData) : base(rawData)
	{
	}

	// Token: 0x06001024 RID: 4132 RVA: 0x0004A5C5 File Offset: 0x000487C5
	public PropInteractable(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}

	// Token: 0x06001025 RID: 4133 RVA: 0x0004A5D0 File Offset: 0x000487D0
	private new SKALDProjectData.PropContainers.InteractableContainer.Interactable getRawData()
	{
		SKALDProjectData.PropContainers.Prop rawData = base.getRawData();
		if (rawData is SKALDProjectData.PropContainers.InteractableContainer.Interactable)
		{
			return rawData as SKALDProjectData.PropContainers.InteractableContainer.Interactable;
		}
		return null;
	}
}
