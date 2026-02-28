using System;
using System.Runtime.Serialization;

// Token: 0x0200016C RID: 364
[Serializable]
public abstract class Vehicle : SkaldWorldObject, ISerializable
{
	// Token: 0x060013C9 RID: 5065 RVA: 0x00057741 File Offset: 0x00055941
	public Vehicle(SKALDProjectData.VehicleContainers.Vehicle rawData) : base(rawData)
	{
		this.dynamicData = new Vehicle.VehicleSaveData(this.worldPosition, this.coreData, this.instanceData);
	}

	// Token: 0x060013CA RID: 5066 RVA: 0x00057767 File Offset: 0x00055967
	public Vehicle()
	{
		this.dynamicData = new Vehicle.VehicleSaveData(this.worldPosition, this.coreData, this.instanceData);
	}

	// Token: 0x060013CB RID: 5067 RVA: 0x0005778C File Offset: 0x0005598C
	public Vehicle(SerializationInfo info, StreamingContext context)
	{
		this.dynamicData = (Vehicle.VehicleSaveData)info.GetValue("saveData", typeof(Vehicle.VehicleSaveData));
		if (this.dynamicData == null)
		{
			MainControl.logError("Vehicle could not load dynamic data!");
		}
		this.applySaveData(this.dynamicData);
	}

	// Token: 0x060013CC RID: 5068 RVA: 0x000577DD File Offset: 0x000559DD
	public void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("saveData", this.dynamicData, typeof(Vehicle.VehicleSaveData));
	}

	// Token: 0x060013CD RID: 5069 RVA: 0x000577FA File Offset: 0x000559FA
	public override TextureTools.TextureData getModelTexture()
	{
		return TextureTools.getSubImageTextureData(MapIllustrator.CURRENT_WATER_FRAME, "Images/" + this.getModelPath());
	}

	// Token: 0x060013CE RID: 5070 RVA: 0x00057816 File Offset: 0x00055A16
	public TextureTools.TextureData getIdleModelTexture()
	{
		return TextureTools.getSubImageTextureData(MapIllustrator.CURRENT_WATER_FRAME + 4, "Images/" + this.getModelPath());
	}

	// Token: 0x060013CF RID: 5071 RVA: 0x00057834 File Offset: 0x00055A34
	protected SKALDProjectData.VehicleContainers.Vehicle getRawData()
	{
		return GameData.getVehicleRawData(this.getId());
	}

	// Token: 0x060013D0 RID: 5072 RVA: 0x00057844 File Offset: 0x00055A44
	public override string getDescription()
	{
		SKALDProjectData.VehicleContainers.Vehicle rawData = this.getRawData();
		if (rawData != null)
		{
			return rawData.description;
		}
		return "A Ship";
	}

	// Token: 0x060013D1 RID: 5073 RVA: 0x00057868 File Offset: 0x00055A68
	public string getNestedMapId()
	{
		SKALDProjectData.VehicleContainers.Vehicle rawData = this.getRawData();
		if (rawData != null)
		{
			return rawData.nestedMap;
		}
		return "";
	}

	// Token: 0x060013D2 RID: 5074 RVA: 0x0005788C File Offset: 0x00055A8C
	public override string getName()
	{
		SKALDProjectData.VehicleContainers.Vehicle rawData = this.getRawData();
		if (rawData != null)
		{
			return rawData.title;
		}
		return "A Ship";
	}

	// Token: 0x060013D3 RID: 5075 RVA: 0x000578B0 File Offset: 0x00055AB0
	public override string getModelPath()
	{
		SKALDProjectData.VehicleContainers.Vehicle rawData = this.getRawData();
		if (rawData != null && rawData.modelPath != "")
		{
			return "Models/" + rawData.modelPath;
		}
		return "Models/Caravel";
	}

	// Token: 0x040004EB RID: 1259
	protected Vehicle.VehicleSaveData dynamicData;

	// Token: 0x020002AE RID: 686
	[Serializable]
	protected class VehicleSaveData : BaseSaveData
	{
		// Token: 0x06001B21 RID: 6945 RVA: 0x000752E5 File Offset: 0x000734E5
		public VehicleSaveData(SkaldWorldObject.WorldPosition position, SkaldBaseObject.CoreData coreData, SkaldInstanceObject.InstanceData instanceData) : base(position, coreData, instanceData)
		{
		}
	}
}
