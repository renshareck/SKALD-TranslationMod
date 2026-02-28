using System;
using System.Runtime.Serialization;

// Token: 0x020000FE RID: 254
[Serializable]
public class PropPickup : PropActivatable, ISerializable
{
	// Token: 0x0600104B RID: 4171 RVA: 0x0004AC86 File Offset: 0x00048E86
	public PropPickup(SKALDProjectData.PropContainers.PickupContainer.Pickup rawData) : base(rawData)
	{
	}

	// Token: 0x0600104C RID: 4172 RVA: 0x0004AC8F File Offset: 0x00048E8F
	public PropPickup(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}

	// Token: 0x0600104D RID: 4173 RVA: 0x0004AC99 File Offset: 0x00048E99
	protected override void setDeactivateImage()
	{
		this.setModelPath("");
	}

	// Token: 0x0600104E RID: 4174 RVA: 0x0004ACA8 File Offset: 0x00048EA8
	protected new SKALDProjectData.PropContainers.PickupContainer.Pickup getRawData()
	{
		SKALDProjectData.PropContainers.Prop rawData = base.getRawData();
		if (rawData is SKALDProjectData.PropContainers.PickupContainer.Pickup)
		{
			return rawData as SKALDProjectData.PropContainers.PickupContainer.Pickup;
		}
		return null;
	}

	// Token: 0x0600104F RID: 4175 RVA: 0x0004ACCC File Offset: 0x00048ECC
	public override string interactWithProp()
	{
		if (base.isHidden())
		{
			return "";
		}
		this.deactivate();
		return base.processVerbTrigger();
	}

	// Token: 0x06001050 RID: 4176 RVA: 0x0004ACE8 File Offset: 0x00048EE8
	public override void deactivate()
	{
		this.setToBeRemoved();
		Inventory inventory = new Inventory();
		foreach (string loadoutId in this.getRawData().pickupLoadout)
		{
			GameData.applyLoadoutData(loadoutId, inventory);
		}
		MainControl.getDataControl().addPositiveBark(inventory.getPickedUpBark());
		MainControl.getDataControl().getInventory().transferInventoryAndClearUser(inventory);
	}
}
