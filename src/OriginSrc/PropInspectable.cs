using System;
using System.Runtime.Serialization;

// Token: 0x020000FA RID: 250
[Serializable]
public class PropInspectable : PropActivatable, ISerializable
{
	// Token: 0x0600101F RID: 4127 RVA: 0x0004A538 File Offset: 0x00048738
	public PropInspectable(SKALDProjectData.PropContainers.InspectableContainer.Inspectable rawData) : base(rawData)
	{
	}

	// Token: 0x06001020 RID: 4128 RVA: 0x0004A541 File Offset: 0x00048741
	public PropInspectable(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}

	// Token: 0x06001021 RID: 4129 RVA: 0x0004A54C File Offset: 0x0004874C
	private new SKALDProjectData.PropContainers.InspectableContainer.Inspectable getRawData()
	{
		SKALDProjectData.PropContainers.Prop rawData = base.getRawData();
		if (rawData is SKALDProjectData.PropContainers.InspectableContainer.Inspectable)
		{
			return rawData as SKALDProjectData.PropContainers.InspectableContainer.Inspectable;
		}
		return null;
	}

	// Token: 0x06001022 RID: 4130 RVA: 0x0004A570 File Offset: 0x00048770
	public override string interactWithProp()
	{
		if (base.isHidden())
		{
			return "";
		}
		this.deactivate();
		string description = this.getDescription();
		if (description != "")
		{
			MainControl.getDataControl().mountSimpleScene(this.getName(), description);
		}
		return base.processVerbTrigger();
	}
}
