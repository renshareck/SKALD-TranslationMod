using System;

// Token: 0x02000036 RID: 54
[Serializable]
public class DynamicEventControl : BaseEventControl
{
	// Token: 0x0600072A RID: 1834 RVA: 0x0001F978 File Offset: 0x0001DB78
	public DynamicEventControl() : base(10, "Events")
	{
		foreach (SKALDProjectData.Objects.EventContainer.Event rawData in GameData.getEventList())
		{
			DynamicEventControl.SkaldEvent skaldEvent = new DynamicEventControl.SkaldEvent(rawData);
			if (skaldEvent.isEssential())
			{
				this.essentialEvents.add(skaldEvent);
			}
			else
			{
				this.optionalEvents.add(skaldEvent);
			}
		}
	}

	// Token: 0x0600072B RID: 1835 RVA: 0x0001F9F8 File Offset: 0x0001DBF8
	public DynamicEventControl.SkaldEvent triggerEvent(MapTile mapTile)
	{
		BaseEventControl.BaseEvent baseEvent = base.triggerEvent();
		if (baseEvent != null)
		{
			return baseEvent as DynamicEventControl.SkaldEvent;
		}
		return null;
	}

	// Token: 0x0600072C RID: 1836 RVA: 0x0001FA18 File Offset: 0x0001DC18
	public new DynamicEventControl.SkaldEvent getEvent(string id)
	{
		BaseEventControl.BaseEvent @event = base.getEvent(id);
		if (@event != null)
		{
			return @event as DynamicEventControl.SkaldEvent;
		}
		MainControl.logError("Did not find Event with ID: " + id);
		return null;
	}

	// Token: 0x020001E9 RID: 489
	[Serializable]
	public class SkaldEvent : BaseEventControl.BaseEvent
	{
		// Token: 0x06001750 RID: 5968 RVA: 0x000679DA File Offset: 0x00065BDA
		public SkaldEvent(SKALDProjectData.Objects.EventContainer.Event rawData) : base(rawData)
		{
			this.setImagePath(rawData.imagePath);
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x000679EF File Offset: 0x00065BEF
		protected override SKALDProjectData.Objects.BaseEvent getRawData()
		{
			return GameData.getEventRawData(this.getId());
		}
	}
}
