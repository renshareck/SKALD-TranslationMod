using System;
using System.Runtime.Serialization;

// Token: 0x020000C2 RID: 194
[Serializable]
public class ItemClothing : ItemEquipable, ISerializable
{
	// Token: 0x06000C61 RID: 3169 RVA: 0x0003A039 File Offset: 0x00038239
	public ItemClothing(SKALDProjectData.ItemDataContainers.ClothingContainer.Clothing rawData) : base(rawData)
	{
	}

	// Token: 0x06000C62 RID: 3170 RVA: 0x0003A044 File Offset: 0x00038244
	public ItemClothing(SerializationInfo info, StreamingContext context)
	{
		this.dynamicData = (Item.ItemSaveData)info.GetValue("saveData", typeof(Item.ItemSaveData));
		if (this.dynamicData == null)
		{
			MainControl.logError("Clothing could not load dynamic data!");
		}
		this.applySaveData(this.dynamicData);
	}

	// Token: 0x06000C63 RID: 3171 RVA: 0x0003A095 File Offset: 0x00038295
	public new void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("saveData", this.dynamicData, typeof(Item.ItemSaveData));
	}

	// Token: 0x06000C64 RID: 3172 RVA: 0x0003A0B2 File Offset: 0x000382B2
	public override Item.ItemTypes getType()
	{
		return Item.ItemTypes.Clothing;
	}

	// Token: 0x06000C65 RID: 3173 RVA: 0x0003A0B5 File Offset: 0x000382B5
	protected override string getModelBasePath()
	{
		return "Images/Models/Bodies/";
	}

	// Token: 0x06000C66 RID: 3174 RVA: 0x0003A0BC File Offset: 0x000382BC
	protected override string getUseSound()
	{
		return "ItemArmorLight1";
	}

	// Token: 0x06000C67 RID: 3175 RVA: 0x0003A0C4 File Offset: 0x000382C4
	public string getTorsoPath()
	{
		SKALDProjectData.ItemDataContainers.ClothingContainer.Clothing clothing = this.getRawData() as SKALDProjectData.ItemDataContainers.ClothingContainer.Clothing;
		if (clothing == null || clothing.torsoModel == "")
		{
			return "";
		}
		return this.getModelBasePath() + "Torsos/" + clothing.torsoModel;
	}

	// Token: 0x06000C68 RID: 3176 RVA: 0x0003A110 File Offset: 0x00038310
	public string getLegsPath()
	{
		SKALDProjectData.ItemDataContainers.ClothingContainer.Clothing clothing = this.getRawData() as SKALDProjectData.ItemDataContainers.ClothingContainer.Clothing;
		if (clothing == null || clothing.legsModel == "")
		{
			return "";
		}
		return this.getModelBasePath() + "Legs/" + clothing.legsModel;
	}

	// Token: 0x06000C69 RID: 3177 RVA: 0x0003A15C File Offset: 0x0003835C
	public string getCloakPath()
	{
		SKALDProjectData.ItemDataContainers.ClothingContainer.Clothing clothing = this.getRawData() as SKALDProjectData.ItemDataContainers.ClothingContainer.Clothing;
		if (clothing == null || clothing.cloakModel == "")
		{
			return "";
		}
		return this.getModelBasePath() + "Cloaks/" + clothing.cloakModel;
	}

	// Token: 0x06000C6A RID: 3178 RVA: 0x0003A1A8 File Offset: 0x000383A8
	public string getArmsPath()
	{
		SKALDProjectData.ItemDataContainers.ClothingContainer.Clothing clothing = this.getRawData() as SKALDProjectData.ItemDataContainers.ClothingContainer.Clothing;
		if (clothing == null || clothing.armsModel == "")
		{
			return "";
		}
		return this.getModelBasePath() + "Arms/" + clothing.armsModel;
	}

	// Token: 0x06000C6B RID: 3179 RVA: 0x0003A1F4 File Offset: 0x000383F4
	public int getReactionBonus()
	{
		SKALDProjectData.ItemDataContainers.ClothingContainer.Clothing clothing = this.getRawData() as SKALDProjectData.ItemDataContainers.ClothingContainer.Clothing;
		if (clothing == null)
		{
			return 0;
		}
		return clothing.reactionBonus;
	}
}
