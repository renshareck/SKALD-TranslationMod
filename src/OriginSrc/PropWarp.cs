using System;
using System.Runtime.Serialization;

// Token: 0x02000103 RID: 259
[Serializable]
public class PropWarp : PropLockable, ISerializable
{
	// Token: 0x06001070 RID: 4208 RVA: 0x0004B0A7 File Offset: 0x000492A7
	public PropWarp(SKALDProjectData.PropContainers.WarpContainer.Warp rawData) : base(rawData)
	{
	}

	// Token: 0x06001071 RID: 4209 RVA: 0x0004B0B0 File Offset: 0x000492B0
	public PropWarp(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}

	// Token: 0x06001072 RID: 4210 RVA: 0x0004B0BC File Offset: 0x000492BC
	private new SKALDProjectData.PropContainers.WarpContainer.Warp getRawData()
	{
		SKALDProjectData.PropContainers.Prop rawData = base.getRawData();
		if (rawData is SKALDProjectData.PropContainers.WarpContainer.Warp)
		{
			return rawData as SKALDProjectData.PropContainers.WarpContainer.Warp;
		}
		return null;
	}

	// Token: 0x06001073 RID: 4211 RVA: 0x0004B0E0 File Offset: 0x000492E0
	internal override string getNestedMapId()
	{
		SKALDProjectData.PropContainers.WarpContainer.Warp rawData = this.getRawData();
		if (rawData != null)
		{
			return rawData.map;
		}
		return "";
	}

	// Token: 0x06001074 RID: 4212 RVA: 0x0004B104 File Offset: 0x00049304
	public override string interactWithProp()
	{
		if (base.isHidden())
		{
			return "";
		}
		SKALDProjectData.PropContainers.WarpContainer.Warp rawData = this.getRawData();
		if (rawData != null)
		{
			if (rawData.ascend)
			{
				MainControl.getDataControl().ascend();
			}
			else if (rawData.descend)
			{
				MainControl.getDataControl().descend();
			}
			else if (rawData.toOverland)
			{
				MainControl.getDataControl().mountMap(MainControl.getDataControl().currentMap.getContainerMapId());
			}
		}
		return base.interactWithProp();
	}

	// Token: 0x06001075 RID: 4213 RVA: 0x0004B17C File Offset: 0x0004937C
	public override string getVerb()
	{
		SKALDProjectData.PropContainers.WarpContainer.Warp rawData = this.getRawData();
		if (rawData == null)
		{
			return "Use";
		}
		if (rawData.verb != "")
		{
			return rawData.verb;
		}
		if (rawData.ascend)
		{
			return "Ascend";
		}
		if (rawData.descend)
		{
			return "Descend";
		}
		if (rawData.toOverland)
		{
			return "Exit";
		}
		if (rawData.toMap)
		{
			return "Enter";
		}
		return "Use";
	}
}
