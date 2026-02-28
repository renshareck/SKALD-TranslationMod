using System;
using System.Runtime.Serialization;

// Token: 0x020000CF RID: 207
[Serializable]
public class ItemMoney : Item, ISerializable
{
	// Token: 0x06000CBC RID: 3260 RVA: 0x0003AF8E File Offset: 0x0003918E
	public ItemMoney()
	{
		this.setId("Money");
		this.setName("Gold");
		this.setDescription("A pile of gold.");
	}

	// Token: 0x06000CBD RID: 3261 RVA: 0x0003AFBA File Offset: 0x000391BA
	public new void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("saveData", this.dynamicData, typeof(Item.ItemSaveData));
	}

	// Token: 0x06000CBE RID: 3262 RVA: 0x0003AFD7 File Offset: 0x000391D7
	public ItemMoney(SerializationInfo info, StreamingContext context)
	{
		this.dynamicData = (Item.ItemSaveData)info.GetValue("saveData", typeof(Item.ItemSaveData));
		this.applySaveData(this.dynamicData);
	}

	// Token: 0x06000CBF RID: 3263 RVA: 0x0003B00B File Offset: 0x0003920B
	public override bool shouldBeRemovedFromGame()
	{
		return this.dynamicData.count <= 0 || base.shouldBeRemovedFromGame();
	}

	// Token: 0x06000CC0 RID: 3264 RVA: 0x0003B023 File Offset: 0x00039223
	public override bool isSellable()
	{
		return false;
	}

	// Token: 0x06000CC1 RID: 3265 RVA: 0x0003B026 File Offset: 0x00039226
	public override bool isStackable()
	{
		return true;
	}

	// Token: 0x06000CC2 RID: 3266 RVA: 0x0003B029 File Offset: 0x00039229
	public override string getImagePath()
	{
		return "Gold";
	}

	// Token: 0x06000CC3 RID: 3267 RVA: 0x0003B030 File Offset: 0x00039230
	public override string getModelPath()
	{
		return "Gold";
	}

	// Token: 0x06000CC4 RID: 3268 RVA: 0x0003B037 File Offset: 0x00039237
	public override SKALDProjectData.ItemDataContainers.ItemData getRawData()
	{
		return null;
	}
}
