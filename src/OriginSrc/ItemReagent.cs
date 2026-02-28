using System;
using System.Runtime.Serialization;

// Token: 0x020000D1 RID: 209
[Serializable]
public class ItemReagent : Item, ISerializable
{
	// Token: 0x06000CCA RID: 3274 RVA: 0x0003B0BD File Offset: 0x000392BD
	public ItemReagent(SKALDProjectData.ItemDataContainers.ReagentContainer.Reagent rawData) : base(rawData)
	{
	}

	// Token: 0x06000CCB RID: 3275 RVA: 0x0003B0C8 File Offset: 0x000392C8
	public ItemReagent(SerializationInfo info, StreamingContext context)
	{
		this.dynamicData = (Item.ItemSaveData)info.GetValue("saveData", typeof(Item.ItemSaveData));
		if (this.dynamicData == null)
		{
			MainControl.logError("Reagent could not load dynamic data!");
		}
		this.applySaveData(this.dynamicData);
	}

	// Token: 0x06000CCC RID: 3276 RVA: 0x0003B11C File Offset: 0x0003931C
	public override string getDescription()
	{
		string text = MainControl.getDataControl().getCraftingControl().getRecipesContainingIngredient(this.getId());
		if (text != "")
		{
			text += "\n\n";
		}
		return text + base.getDescription();
	}

	// Token: 0x06000CCD RID: 3277 RVA: 0x0003B166 File Offset: 0x00039366
	public new void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("saveData", this.dynamicData, typeof(Item.ItemSaveData));
	}

	// Token: 0x06000CCE RID: 3278 RVA: 0x0003B183 File Offset: 0x00039383
	public override Item.ItemTypes getType()
	{
		return Item.ItemTypes.Reagent;
	}
}
