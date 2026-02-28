using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000035 RID: 53
[Serializable]
public abstract class BaseEventControl
{
	// Token: 0x06000721 RID: 1825 RVA: 0x0001F6A0 File Offset: 0x0001D8A0
	protected BaseEventControl(int maxTimer, string objectsContainedId)
	{
		this.eventTimer = new BaseEventControl.EventTimer(maxTimer);
		this.essentialEvents = new BaseEventControl.SkaldEventContainer("Essential " + objectsContainedId);
		this.optionalEvents = new BaseEventControl.SkaldEventContainer("Optional " + objectsContainedId);
		this.containerList = new List<BaseEventControl.SkaldEventContainer>();
		this.containerList.Add(this.essentialEvents);
		this.containerList.Add(this.optionalEvents);
	}

	// Token: 0x06000722 RID: 1826 RVA: 0x0001F718 File Offset: 0x0001D918
	private bool testCounter()
	{
		return this.eventTimer.testCounter();
	}

	// Token: 0x06000723 RID: 1827 RVA: 0x0001F725 File Offset: 0x0001D925
	public void partiallyResetCounter()
	{
		this.eventTimer.partialReset();
	}

	// Token: 0x06000724 RID: 1828 RVA: 0x0001F734 File Offset: 0x0001D934
	public string printEvents()
	{
		string text = "--Current Counter: " + this.eventTimer.printCounter() + "\n";
		foreach (BaseEventControl.SkaldEventContainer skaldEventContainer in this.containerList)
		{
			text = text + skaldEventContainer.printListSimplifiedString() + "\n";
		}
		return text;
	}

	// Token: 0x06000725 RID: 1829 RVA: 0x0001F7B0 File Offset: 0x0001D9B0
	public string deleteEvent(string id)
	{
		foreach (BaseEventControl.SkaldEventContainer skaldEventContainer in this.containerList)
		{
			skaldEventContainer.deleteObject(id);
		}
		return "Deleted event: " + id;
	}

	// Token: 0x06000726 RID: 1830 RVA: 0x0001F810 File Offset: 0x0001DA10
	public string activateEvent(string id)
	{
		foreach (BaseEventControl.SkaldEventContainer skaldEventContainer in this.containerList)
		{
			skaldEventContainer.activateEvent(id);
		}
		return "Event activated: " + id;
	}

	// Token: 0x06000727 RID: 1831 RVA: 0x0001F870 File Offset: 0x0001DA70
	public string deactivateEvent(string id)
	{
		foreach (BaseEventControl.SkaldEventContainer skaldEventContainer in this.containerList)
		{
			skaldEventContainer.deactivateEvent(id);
		}
		return "Event deactivated: " + id;
	}

	// Token: 0x06000728 RID: 1832 RVA: 0x0001F8D0 File Offset: 0x0001DAD0
	public BaseEventControl.BaseEvent triggerEvent()
	{
		BaseEventControl.BaseEvent @event = this.essentialEvents.getEvent();
		if (@event == null && this.testCounter())
		{
			@event = this.optionalEvents.getEvent();
		}
		return @event;
	}

	// Token: 0x06000729 RID: 1833 RVA: 0x0001F904 File Offset: 0x0001DB04
	public BaseEventControl.BaseEvent getEvent(string id)
	{
		foreach (BaseEventControl.SkaldEventContainer skaldEventContainer in this.containerList)
		{
			BaseEventControl.BaseEvent baseEvent = skaldEventContainer.getObject(id) as BaseEventControl.BaseEvent;
			if (baseEvent != null)
			{
				return baseEvent;
			}
		}
		MainControl.logError("Did not find Event with ID: " + id);
		return null;
	}

	// Token: 0x04000162 RID: 354
	private BaseEventControl.EventTimer eventTimer;

	// Token: 0x04000163 RID: 355
	protected BaseEventControl.SkaldEventContainer essentialEvents;

	// Token: 0x04000164 RID: 356
	protected BaseEventControl.SkaldEventContainer optionalEvents;

	// Token: 0x04000165 RID: 357
	protected List<BaseEventControl.SkaldEventContainer> containerList;

	// Token: 0x020001E6 RID: 486
	[Serializable]
	public abstract class BaseEvent : SkaldInstanceObject
	{
		// Token: 0x06001738 RID: 5944 RVA: 0x00067448 File Offset: 0x00065648
		public BaseEvent(SKALDProjectData.Objects.BaseEvent rawData)
		{
			this.setId(rawData.id);
			this.active = rawData.active;
		}

		// Token: 0x06001739 RID: 5945
		protected abstract SKALDProjectData.Objects.BaseEvent getRawData();

		// Token: 0x0600173A RID: 5946 RVA: 0x00067469 File Offset: 0x00065669
		public bool isReady()
		{
			return this.active && this.testCondition();
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x00067480 File Offset: 0x00065680
		public bool testCondition()
		{
			SKALDProjectData.Objects.BaseEvent rawData = this.getRawData();
			DataControl dataControl = MainControl.getDataControl();
			if (rawData == null)
			{
				return false;
			}
			if (GlobalSettings.getDifficultySettings().ignoreTrashMobs() && rawData.trashMob)
			{
				return false;
			}
			if (rawData.seaEvent && !dataControl.getCurrentTile().isWater())
			{
				return false;
			}
			if (!rawData.seaEvent && dataControl.getCurrentTile().isWater())
			{
				return false;
			}
			if (rawData.overlandEvent && !dataControl.isMapOverland())
			{
				return false;
			}
			int level = dataControl.getMainCharacter().getLevel();
			if (level < rawData.minLevel || level > rawData.maxLevel)
			{
				return false;
			}
			if (rawData.condition != "" && base.processString(rawData.condition, null).Contains("False"))
			{
				return false;
			}
			if (rawData.requiredCharacters.Count > 0)
			{
				foreach (string npcId in rawData.requiredCharacters)
				{
					if (!dataControl.isCharacterAvailable(npcId))
					{
						return false;
					}
				}
			}
			if (rawData.requiredMaps.Count > 0)
			{
				bool flag = false;
				string id = dataControl.currentMap.getId();
				foreach (string b in rawData.requiredMaps)
				{
					if (id == b)
					{
						flag = true;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			if (rawData.requiredTiles.Count > 0)
			{
				bool flag2 = false;
				string id2 = dataControl.getCurrentTile().getId();
				foreach (string b2 in rawData.requiredTiles)
				{
					if (id2 == b2)
					{
						flag2 = true;
					}
				}
				if (!flag2)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x00067684 File Offset: 0x00065884
		public bool isRepeatable()
		{
			SKALDProjectData.Objects.BaseEvent rawData = this.getRawData();
			return rawData != null && rawData.repeatable;
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x000676A4 File Offset: 0x000658A4
		public string getTrigger()
		{
			SKALDProjectData.Objects.BaseEvent rawData = this.getRawData();
			if (rawData == null)
			{
				return "";
			}
			return rawData.trigger;
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x000676C8 File Offset: 0x000658C8
		public bool isEssential()
		{
			SKALDProjectData.Objects.BaseEvent rawData = this.getRawData();
			return rawData != null && rawData.essential;
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x000676E7 File Offset: 0x000658E7
		public void activate()
		{
			this.active = true;
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x000676F0 File Offset: 0x000658F0
		public int getWeight()
		{
			SKALDProjectData.Objects.BaseEvent rawData = this.getRawData();
			if (rawData == null)
			{
				return 0;
			}
			return rawData.weight;
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x0006770F File Offset: 0x0006590F
		public void deactivate()
		{
			this.active = false;
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x00067718 File Offset: 0x00065918
		public override string getName()
		{
			SKALDProjectData.Objects.BaseEvent rawData = this.getRawData();
			if (rawData == null)
			{
				return "";
			}
			if (this.active)
			{
				return string.Concat(new string[]
				{
					this.getId(),
					" - Active - ",
					rawData.weight.ToString(),
					" - ",
					this.testCondition().ToString()
				});
			}
			return string.Concat(new string[]
			{
				this.getId(),
				" - Inactive - ",
				rawData.weight.ToString(),
				" - ",
				this.testCondition().ToString()
			});
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x000677C4 File Offset: 0x000659C4
		public string getNameSimple()
		{
			return this.getRawData().title;
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x000677D1 File Offset: 0x000659D1
		public override string getDescription()
		{
			return this.getRawData().description;
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x000677DE File Offset: 0x000659DE
		public override string getImagePath()
		{
			return this.getRawData().imagePath;
		}

		// Token: 0x04000791 RID: 1937
		protected bool active;
	}

	// Token: 0x020001E7 RID: 487
	[Serializable]
	protected class SkaldEventContainer : SkaldBaseList
	{
		// Token: 0x06001746 RID: 5958 RVA: 0x000677EB File Offset: 0x000659EB
		public SkaldEventContainer(string name)
		{
			this.setName(name);
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x000677FB File Offset: 0x000659FB
		public void add(BaseEventControl.BaseEvent _event)
		{
			if (base.containsObject(_event.getId()))
			{
				return;
			}
			base.add(_event);
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x00067814 File Offset: 0x00065A14
		public bool activateEvent(string id)
		{
			BaseEventControl.BaseEvent baseEvent = base.getObject(id) as BaseEventControl.BaseEvent;
			if (baseEvent == null)
			{
				return false;
			}
			baseEvent.activate();
			return true;
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x0006783C File Offset: 0x00065A3C
		public bool deactivateEvent(string id)
		{
			BaseEventControl.BaseEvent baseEvent = base.getObject(id) as BaseEventControl.BaseEvent;
			if (baseEvent == null)
			{
				return false;
			}
			baseEvent.deactivate();
			return true;
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x00067864 File Offset: 0x00065A64
		public BaseEventControl.BaseEvent getEvent()
		{
			List<BaseEventControl.BaseEvent> list = new List<BaseEventControl.BaseEvent>();
			int num = 0;
			foreach (SkaldBaseObject skaldBaseObject in this.objectList)
			{
				BaseEventControl.BaseEvent baseEvent = (BaseEventControl.BaseEvent)skaldBaseObject;
				if (baseEvent.isReady())
				{
					list.Add(baseEvent);
					num += baseEvent.getWeight();
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			int num2 = Random.Range(0, num);
			int num3 = 0;
			if (list.Count == 1)
			{
				return list[0];
			}
			foreach (BaseEventControl.BaseEvent baseEvent2 in list)
			{
				num3 += baseEvent2.getWeight();
				if (num3 > num2)
				{
					return baseEvent2;
				}
			}
			return null;
		}
	}

	// Token: 0x020001E8 RID: 488
	[Serializable]
	private class EventTimer
	{
		// Token: 0x0600174B RID: 5963 RVA: 0x00067950 File Offset: 0x00065B50
		public EventTimer(int maxCounter)
		{
			this.maxCounter = maxCounter;
			this.resetCurrentCounter();
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x00067965 File Offset: 0x00065B65
		public string printCounter()
		{
			return this.currentCounter.ToString();
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x00067972 File Offset: 0x00065B72
		public bool testCounter()
		{
			if (this.currentCounter > 0)
			{
				this.currentCounter--;
				return false;
			}
			this.resetCurrentCounter();
			return true;
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x00067994 File Offset: 0x00065B94
		private void resetCurrentCounter()
		{
			this.currentCounter = Random.Range(this.maxCounter, this.maxCounter * 2);
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x000679B0 File Offset: 0x00065BB0
		public void partialReset()
		{
			int num = Random.Range(1, this.maxCounter);
			if (num > this.currentCounter)
			{
				this.currentCounter = num;
			}
		}

		// Token: 0x04000792 RID: 1938
		private int maxCounter;

		// Token: 0x04000793 RID: 1939
		private int currentCounter;
	}
}
