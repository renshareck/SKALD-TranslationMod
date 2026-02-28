using System;
using System.Runtime.Serialization;

// Token: 0x0200016B RID: 363
[Serializable]
public class Ship : Vehicle, ISerializable
{
	// Token: 0x060013C7 RID: 5063 RVA: 0x000576E4 File Offset: 0x000558E4
	public Ship(SKALDProjectData.VehicleContainers.Vehicle rawData) : base(rawData)
	{
	}

	// Token: 0x060013C8 RID: 5064 RVA: 0x000576F0 File Offset: 0x000558F0
	public Ship(SerializationInfo info, StreamingContext context)
	{
		this.dynamicData = (Vehicle.VehicleSaveData)info.GetValue("saveData", typeof(Vehicle.VehicleSaveData));
		if (this.dynamicData == null)
		{
			MainControl.logError("Vehicle could not load dynamic data!");
		}
		this.applySaveData(this.dynamicData);
	}
}
