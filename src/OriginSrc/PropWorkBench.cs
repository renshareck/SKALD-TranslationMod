using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

// Token: 0x02000104 RID: 260
[Serializable]
public class PropWorkBench : PropActivatable, ISerializable
{
	// Token: 0x06001076 RID: 4214 RVA: 0x0004B1EF File Offset: 0x000493EF
	public PropWorkBench(SKALDProjectData.PropContainers.WorkBenchContainer.WorkBench rawData) : base(rawData)
	{
	}

	// Token: 0x06001077 RID: 4215 RVA: 0x0004B1F8 File Offset: 0x000493F8
	public PropWorkBench(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}

	// Token: 0x06001078 RID: 4216 RVA: 0x0004B202 File Offset: 0x00049402
	public override string getInspectDescription()
	{
		return base.getInspectDescription() + "\n\n[Workbench]";
	}

	// Token: 0x06001079 RID: 4217 RVA: 0x0004B214 File Offset: 0x00049414
	public string getCraftingType()
	{
		return this.getRawData().craftingType;
	}

	// Token: 0x0600107A RID: 4218 RVA: 0x0004B224 File Offset: 0x00049424
	public List<Item.ItemTypes> getType()
	{
		string a = this.getCraftingType().ToUpper();
		if (a == "KITCHEN")
		{
			return new List<Item.ItemTypes>
			{
				Item.ItemTypes.Food
			};
		}
		if (a == "ALCHEMIST")
		{
			return new List<Item.ItemTypes>
			{
				Item.ItemTypes.Reagent,
				Item.ItemTypes.Consumable
			};
		}
		return new List<Item.ItemTypes>
		{
			Item.ItemTypes.Food
		};
	}

	// Token: 0x0600107B RID: 4219 RVA: 0x0004B288 File Offset: 0x00049488
	private new SKALDProjectData.PropContainers.WorkBenchContainer.WorkBench getRawData()
	{
		SKALDProjectData.PropContainers.Prop rawData = base.getRawData();
		if (rawData is SKALDProjectData.PropContainers.WorkBenchContainer.WorkBench)
		{
			return rawData as SKALDProjectData.PropContainers.WorkBenchContainer.WorkBench;
		}
		return null;
	}

	// Token: 0x0600107C RID: 4220 RVA: 0x0004B2AC File Offset: 0x000494AC
	public override string interactWithProp()
	{
		MainControl.getDataControl().mountWorkBench(this);
		this.activate();
		return base.interactWithProp();
	}
}
