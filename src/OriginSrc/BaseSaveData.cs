using System;

// Token: 0x0200017A RID: 378
[Serializable]
public class BaseSaveData
{
	// Token: 0x06001434 RID: 5172 RVA: 0x00059CBC File Offset: 0x00057EBC
	public BaseSaveData(SkaldWorldObject.WorldPosition position, SkaldBaseObject.CoreData coreData, SkaldInstanceObject.InstanceData instanceData)
	{
		if (position == null)
		{
			MainControl.logError("Failed to save position data!");
		}
		if (coreData == null)
		{
			MainControl.logError("Failed to save core data!");
		}
		if (instanceData == null)
		{
			MainControl.logError("Failed to save instance data!");
		}
		this.position = position;
		this.coreData = coreData;
		this.instanceData = instanceData;
	}

	// Token: 0x04000525 RID: 1317
	public SkaldWorldObject.WorldPosition position;

	// Token: 0x04000526 RID: 1318
	public SkaldBaseObject.CoreData coreData;

	// Token: 0x04000527 RID: 1319
	public SkaldInstanceObject.InstanceData instanceData;
}
