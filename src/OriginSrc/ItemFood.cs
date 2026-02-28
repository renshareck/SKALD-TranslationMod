using System;
using System.Runtime.Serialization;

// Token: 0x020000C6 RID: 198
[Serializable]
public class ItemFood : Item, ISerializable
{
	// Token: 0x06000C8E RID: 3214 RVA: 0x0003A98C File Offset: 0x00038B8C
	public ItemFood(SKALDProjectData.ItemDataContainers.FoodContainer.Food rawData) : base(rawData)
	{
	}

	// Token: 0x06000C8F RID: 3215 RVA: 0x0003A998 File Offset: 0x00038B98
	public ItemFood(SerializationInfo info, StreamingContext context)
	{
		this.dynamicData = (Item.ItemSaveData)info.GetValue("saveData", typeof(Item.ItemSaveData));
		if (this.dynamicData == null)
		{
			MainControl.logError("Food could not load dynamic data!");
		}
		this.applySaveData(this.dynamicData);
	}

	// Token: 0x06000C90 RID: 3216 RVA: 0x0003A9E9 File Offset: 0x00038BE9
	public new void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("saveData", this.dynamicData, typeof(Item.ItemSaveData));
	}

	// Token: 0x06000C91 RID: 3217 RVA: 0x0003AA06 File Offset: 0x00038C06
	public override string printComparativeStats(Character c)
	{
		return this.printStatsHeader() + TextTools.formateNameValuePair("Food Val.", this.getFoodValue()) + "\n" + this.printStatsTail();
	}

	// Token: 0x06000C92 RID: 3218 RVA: 0x0003AA33 File Offset: 0x00038C33
	public override Item.ItemTypes getType()
	{
		return Item.ItemTypes.Food;
	}

	// Token: 0x06000C93 RID: 3219 RVA: 0x0003AA38 File Offset: 0x00038C38
	public int getFoodValue()
	{
		SKALDProjectData.ItemDataContainers.FoodContainer.Food food = this.getRawData() as SKALDProjectData.ItemDataContainers.FoodContainer.Food;
		if (food != null)
		{
			return food.foodValue;
		}
		return 1;
	}

	// Token: 0x06000C94 RID: 3220 RVA: 0x0003AA5C File Offset: 0x00038C5C
	public override string getDescription()
	{
		string text = MainControl.getDataControl().getCraftingControl().getRecipesContainingIngredient(this.getId());
		if (text != "")
		{
			text += "\n\n";
		}
		return text + base.getDescription();
	}
}
