using System;
using System.Runtime.Serialization;

// Token: 0x02000102 RID: 258
[Serializable]
public class PropTrigger : Prop, ISerializable
{
	// Token: 0x06001067 RID: 4199 RVA: 0x0004AF98 File Offset: 0x00049198
	public PropTrigger(SKALDProjectData.PropContainers.TriggerContainer.Trigger rawData) : base(rawData)
	{
	}

	// Token: 0x06001068 RID: 4200 RVA: 0x0004AFA1 File Offset: 0x000491A1
	public PropTrigger(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}

	// Token: 0x06001069 RID: 4201 RVA: 0x0004AFAC File Offset: 0x000491AC
	private new SKALDProjectData.PropContainers.TriggerContainer.Trigger getRawData()
	{
		SKALDProjectData.PropContainers.Prop rawData = base.getRawData();
		if (rawData is SKALDProjectData.PropContainers.TriggerContainer.Trigger)
		{
			return rawData as SKALDProjectData.PropContainers.TriggerContainer.Trigger;
		}
		return null;
	}

	// Token: 0x0600106A RID: 4202 RVA: 0x0004AFD0 File Offset: 0x000491D0
	protected override string getEnterTrigger()
	{
		SKALDProjectData.PropContainers.TriggerContainer.Trigger rawData = this.getRawData();
		if (rawData != null)
		{
			return rawData.enterTrigger;
		}
		return "";
	}

	// Token: 0x0600106B RID: 4203 RVA: 0x0004AFF4 File Offset: 0x000491F4
	protected override string getLeaveTrigger()
	{
		SKALDProjectData.PropContainers.TriggerContainer.Trigger rawData = this.getRawData();
		if (rawData != null)
		{
			return rawData.leaveTrigger;
		}
		return "";
	}

	// Token: 0x0600106C RID: 4204 RVA: 0x0004B018 File Offset: 0x00049218
	protected override string getDigTrigger()
	{
		SKALDProjectData.PropContainers.TriggerContainer.Trigger rawData = this.getRawData();
		if (rawData != null)
		{
			return rawData.digTrigger;
		}
		return "";
	}

	// Token: 0x0600106D RID: 4205 RVA: 0x0004B03C File Offset: 0x0004923C
	protected override string getCombatLaunchTrigger()
	{
		SKALDProjectData.PropContainers.TriggerContainer.Trigger rawData = this.getRawData();
		if (rawData != null)
		{
			return rawData.combatLaunchTrigger;
		}
		return "";
	}

	// Token: 0x0600106E RID: 4206 RVA: 0x0004B060 File Offset: 0x00049260
	protected override string getTryEnterTrigger()
	{
		SKALDProjectData.PropContainers.TriggerContainer.Trigger rawData = this.getRawData();
		if (rawData != null)
		{
			return rawData.tryEnterTrigger;
		}
		return "";
	}

	// Token: 0x0600106F RID: 4207 RVA: 0x0004B084 File Offset: 0x00049284
	protected override string getFirstTimeTrigger()
	{
		SKALDProjectData.PropContainers.TriggerContainer.Trigger rawData = this.getRawData();
		if (rawData != null)
		{
			return rawData.firstTimeTrigger;
		}
		return "";
	}
}
