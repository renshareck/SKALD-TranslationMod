using System;
using System.Collections.Generic;

// Token: 0x02000038 RID: 56
[Serializable]
public class MasterEventManager
{
	// Token: 0x0600072F RID: 1839 RVA: 0x0001FAF4 File Offset: 0x0001DCF4
	public MasterEventManager()
	{
		MainControl.log("Initializing Event Manager!");
		this.metaList = new List<BaseEventControl>();
		this.encounterControl = new EncounterControl();
		this.metaList.Add(this.encounterControl);
		this.eventControl = new DynamicEventControl();
		this.metaList.Add(this.eventControl);
		MainControl.log("Completed Event Manager!");
	}

	// Token: 0x06000730 RID: 1840 RVA: 0x0001FB60 File Offset: 0x0001DD60
	public string printEvents()
	{
		string text = "";
		foreach (BaseEventControl baseEventControl in this.metaList)
		{
			text = text + baseEventControl.printEvents() + "\n\n";
		}
		return text;
	}

	// Token: 0x06000731 RID: 1841 RVA: 0x0001FBC8 File Offset: 0x0001DDC8
	public void partiallyResetCounters()
	{
		foreach (BaseEventControl baseEventControl in this.metaList)
		{
			baseEventControl.partiallyResetCounter();
		}
	}

	// Token: 0x06000732 RID: 1842 RVA: 0x0001FC18 File Offset: 0x0001DE18
	public string deleteEvent(string eventId)
	{
		return this.eventControl.deleteEvent(eventId);
	}

	// Token: 0x06000733 RID: 1843 RVA: 0x0001FC26 File Offset: 0x0001DE26
	public string activateEvent(string eventId)
	{
		return this.eventControl.activateEvent(eventId);
	}

	// Token: 0x06000734 RID: 1844 RVA: 0x0001FC34 File Offset: 0x0001DE34
	public string deactivateEvent(string eventId)
	{
		return this.eventControl.deactivateEvent(eventId);
	}

	// Token: 0x06000735 RID: 1845 RVA: 0x0001FC42 File Offset: 0x0001DE42
	public DynamicEventControl.SkaldEvent triggerEvent(MapTile mapTile)
	{
		return this.eventControl.triggerEvent(mapTile);
	}

	// Token: 0x06000736 RID: 1846 RVA: 0x0001FC50 File Offset: 0x0001DE50
	public DynamicEventControl.SkaldEvent getEvent(string eventId)
	{
		return this.eventControl.getEvent(eventId);
	}

	// Token: 0x06000737 RID: 1847 RVA: 0x0001FC5E File Offset: 0x0001DE5E
	public EncounterControl.Encounter triggerEncounter(MapTile tile)
	{
		return this.encounterControl.triggerEncounter(tile);
	}

	// Token: 0x06000738 RID: 1848 RVA: 0x0001FC6C File Offset: 0x0001DE6C
	public string activateEncounter(string encounterId)
	{
		return this.encounterControl.activateEvent(encounterId);
	}

	// Token: 0x04000166 RID: 358
	private EncounterControl encounterControl;

	// Token: 0x04000167 RID: 359
	private DynamicEventControl eventControl;

	// Token: 0x04000168 RID: 360
	private List<BaseEventControl> metaList;
}
