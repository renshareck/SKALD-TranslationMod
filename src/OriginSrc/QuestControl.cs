using System;
using System.Collections.Generic;

// Token: 0x02000105 RID: 261
[Serializable]
public class QuestControl
{
	// Token: 0x0600107D RID: 4221 RVA: 0x0004B2C5 File Offset: 0x000494C5
	public QuestControl()
	{
		MainControl.log("Initializing Quest Control");
		this.begunQuestsList = new SkaldObjectList();
		this.begunQuestsList.deactivateSorting();
		this.populateQuestList();
		MainControl.log("Completed Quest Control");
	}

	// Token: 0x0600107E RID: 4222 RVA: 0x0004B300 File Offset: 0x00049500
	private void populateQuestList()
	{
		List<SKALDProjectData.QuestContainers.QuestData> list = GameData.getQuestList();
		this.questList = new Dictionary<string, QuestControl.Quest>();
		foreach (SKALDProjectData.QuestContainers.QuestData questData in list)
		{
			if (!this.questList.ContainsKey(questData.id.ToUpper()))
			{
				this.questList.Add(questData.id.ToUpper(), new QuestControl.Quest(questData));
			}
		}
		foreach (SKALDProjectData.QuestContainers.QuestData questData2 in list)
		{
			foreach (BaseDataObject baseDataObject in questData2.getBaseList())
			{
				SKALDProjectData.QuestContainers.QuestData questData3 = (SKALDProjectData.QuestContainers.QuestData)baseDataObject;
				this.getQuest(questData3.id).setParent(questData2.id);
			}
		}
		foreach (KeyValuePair<string, QuestControl.Quest> keyValuePair in this.questList)
		{
			keyValuePair.Value.validateParent();
		}
	}

	// Token: 0x0600107F RID: 4223 RVA: 0x0004B468 File Offset: 0x00049668
	public SkaldObjectList getQuestList()
	{
		SkaldDataList skaldDataList = new SkaldDataList();
		skaldDataList.setName("Quests");
		List<SkaldBaseObject> list = new List<SkaldBaseObject>();
		List<SkaldBaseObject> list2 = new List<SkaldBaseObject>();
		List<SkaldBaseObject> list3 = new List<SkaldBaseObject>();
		List<SkaldBaseObject> list4 = new List<SkaldBaseObject>();
		List<SkaldBaseObject> list5 = new List<SkaldBaseObject>();
		List<SkaldBaseObject> list6 = new List<SkaldBaseObject>();
		foreach (SkaldBaseObject skaldBaseObject in this.begunQuestsList.getObjectList())
		{
			QuestControl.Quest quest = (QuestControl.Quest)skaldBaseObject;
			if (quest.isSubquest())
			{
				list2.Add(quest);
			}
			else
			{
				list.Add(quest);
			}
		}
		list.Reverse();
		foreach (SkaldBaseObject skaldBaseObject2 in list)
		{
			QuestControl.Quest quest2 = (QuestControl.Quest)skaldBaseObject2;
			if (quest2.isQuestActive())
			{
				if (quest2.isMain())
				{
					this.appendQuestAndSubQuests(list2, list3, quest2);
				}
				else
				{
					this.appendQuestAndSubQuests(list2, list4, quest2);
				}
			}
			else if (quest2.isQuestFinished())
			{
				if (quest2.isQuestFailed())
				{
					this.appendQuestAndSubQuests(list2, list6, quest2);
				}
				else
				{
					this.appendQuestAndSubQuests(list2, list5, quest2);
				}
			}
		}
		if (list3.Count > 0)
		{
			skaldDataList.addHeaderListAndSpace("MAIN QUESTS", "Main quests that have not yet been completed.", list3);
		}
		if (list4.Count > 0)
		{
			skaldDataList.addHeaderListAndSpace("SIDE QUESTS", "Side-quests that have not yet been completed.", list4);
		}
		skaldDataList.deactivateSaveLastRealEntry();
		if (list5.Count > 0)
		{
			skaldDataList.addHeaderListAndSpace("COMPLETED", "Quests that have been completed.", list5);
		}
		if (list6.Count > 0)
		{
			skaldDataList.addHeaderListAndSpace("FAILED", "Quests that have failed.", list6);
		}
		skaldDataList.setLastObjectAsCurrentObject();
		return skaldDataList;
	}

	// Token: 0x06001080 RID: 4224 RVA: 0x0004B628 File Offset: 0x00049828
	private void appendQuestAndSubQuests(List<SkaldBaseObject> subquestlist, List<SkaldBaseObject> targetList, QuestControl.Quest currentQuest)
	{
		if (currentQuest.isQuestSealed())
		{
			return;
		}
		targetList.Add(currentQuest);
		foreach (SkaldBaseObject skaldBaseObject in subquestlist)
		{
			QuestControl.Quest quest = (QuestControl.Quest)skaldBaseObject;
			if (quest.getParentQuestID() == currentQuest.getId() && (currentQuest.isQuestActive() || (currentQuest.isQuestFinished() && quest.isQuestFinished())))
			{
				this.appendQuestAndSubQuests(subquestlist, targetList, quest);
			}
		}
	}

	// Token: 0x06001081 RID: 4225 RVA: 0x0004B6B8 File Offset: 0x000498B8
	public void beginAllQuests()
	{
		foreach (KeyValuePair<string, QuestControl.Quest> keyValuePair in this.questList)
		{
			this.beginQuest(keyValuePair.Value.getId());
		}
	}

	// Token: 0x06001082 RID: 4226 RVA: 0x0004B718 File Offset: 0x00049918
	private QuestControl.Quest getQuest(string id)
	{
		if (id == "")
		{
			MainControl.logError("Searching for quest with blank ID.");
			return null;
		}
		id = id.ToUpper();
		if (this.questList.Count == 0)
		{
			MainControl.logError("Empty quest-list");
			return null;
		}
		if (this.questList.ContainsKey(id))
		{
			return this.questList[id];
		}
		MainControl.logError("Could not find Quest with ID: " + id);
		return null;
	}

	// Token: 0x06001083 RID: 4227 RVA: 0x0004B78C File Offset: 0x0004998C
	public bool isQuestStateOpen(string id)
	{
		bool result;
		try
		{
			result = this.getQuest(id).isCurrentQuestStateOpen();
		}
		catch (NullReferenceException obj)
		{
			MainControl.logError(obj);
			result = false;
		}
		return result;
	}

	// Token: 0x06001084 RID: 4228 RVA: 0x0004B7C4 File Offset: 0x000499C4
	public bool isQuestStateBegun(string id)
	{
		bool result;
		try
		{
			result = this.getQuest(id).isCurrentQuestStateBegun();
		}
		catch (NullReferenceException obj)
		{
			MainControl.logError(obj);
			result = false;
		}
		return result;
	}

	// Token: 0x06001085 RID: 4229 RVA: 0x0004B7FC File Offset: 0x000499FC
	public bool isQuestStateCompleted(string id)
	{
		bool result;
		try
		{
			result = this.getQuest(id).isCurrentQuestStateCompleted();
		}
		catch (NullReferenceException obj)
		{
			MainControl.logError(obj);
			result = false;
		}
		return result;
	}

	// Token: 0x06001086 RID: 4230 RVA: 0x0004B834 File Offset: 0x00049A34
	public bool isQuestStateFailed(string id)
	{
		bool result;
		try
		{
			result = this.getQuest(id).isCurrentQuestStateFailed();
		}
		catch (NullReferenceException obj)
		{
			MainControl.logError(obj);
			result = false;
		}
		return result;
	}

	// Token: 0x06001087 RID: 4231 RVA: 0x0004B86C File Offset: 0x00049A6C
	public bool isQuestStateRewarded(string id)
	{
		bool result;
		try
		{
			result = this.getQuest(id).isCurrentQuestStateRewarded();
		}
		catch (NullReferenceException obj)
		{
			MainControl.logError(obj);
			result = false;
		}
		return result;
	}

	// Token: 0x06001088 RID: 4232 RVA: 0x0004B8A4 File Offset: 0x00049AA4
	public bool isQuestActive(string id)
	{
		bool result;
		try
		{
			result = this.getQuest(id).isQuestActive();
		}
		catch (NullReferenceException obj)
		{
			MainControl.logError(obj);
			result = false;
		}
		return result;
	}

	// Token: 0x06001089 RID: 4233 RVA: 0x0004B8DC File Offset: 0x00049ADC
	public bool isQuestOpen(string id)
	{
		bool result;
		try
		{
			result = this.getQuest(id).isQuestOpen();
		}
		catch (NullReferenceException obj)
		{
			MainControl.logError(obj);
			result = false;
		}
		return result;
	}

	// Token: 0x0600108A RID: 4234 RVA: 0x0004B914 File Offset: 0x00049B14
	public bool isQuestBegun(string id)
	{
		bool result;
		try
		{
			result = this.getQuest(id).isQuestBegun();
		}
		catch (NullReferenceException obj)
		{
			MainControl.logError(obj);
			result = false;
		}
		return result;
	}

	// Token: 0x0600108B RID: 4235 RVA: 0x0004B94C File Offset: 0x00049B4C
	public bool isQuestCompleted(string id)
	{
		bool result;
		try
		{
			result = this.getQuest(id).isQuestCompleted();
		}
		catch (NullReferenceException obj)
		{
			MainControl.logError(obj);
			result = false;
		}
		return result;
	}

	// Token: 0x0600108C RID: 4236 RVA: 0x0004B984 File Offset: 0x00049B84
	public bool isQuestFailed(string id)
	{
		bool result;
		try
		{
			result = this.getQuest(id).isQuestFailed();
		}
		catch (NullReferenceException obj)
		{
			MainControl.logError(obj);
			result = false;
		}
		return result;
	}

	// Token: 0x0600108D RID: 4237 RVA: 0x0004B9BC File Offset: 0x00049BBC
	public bool isQuestRewarded(string id)
	{
		bool result;
		try
		{
			result = this.getQuest(id).isQuestRewarded();
		}
		catch (NullReferenceException obj)
		{
			MainControl.logError(obj);
			result = false;
		}
		return result;
	}

	// Token: 0x0600108E RID: 4238 RVA: 0x0004B9F4 File Offset: 0x00049BF4
	public bool evaluateQuestSuccess(string id)
	{
		bool result;
		try
		{
			result = this.getQuest(id).evaluateQuestSuccess();
		}
		catch (NullReferenceException obj)
		{
			MainControl.logError(obj);
			result = false;
		}
		return result;
	}

	// Token: 0x0600108F RID: 4239 RVA: 0x0004BA2C File Offset: 0x00049C2C
	public string openQuest(string id)
	{
		string result;
		try
		{
			result = this.getQuest(id).openQuest();
		}
		catch (NullReferenceException obj)
		{
			MainControl.logError(obj);
			result = "";
		}
		return result;
	}

	// Token: 0x06001090 RID: 4240 RVA: 0x0004BA68 File Offset: 0x00049C68
	public string beginQuest(string id)
	{
		string result;
		try
		{
			QuestControl.Quest quest = this.getQuest(id);
			if (!quest.isQuestBegun())
			{
				this.begunQuestsList.add(quest);
			}
			result = this.getQuest(id).beginQuest();
		}
		catch (NullReferenceException obj)
		{
			MainControl.logError(obj);
			result = "";
		}
		return result;
	}

	// Token: 0x06001091 RID: 4241 RVA: 0x0004BAC0 File Offset: 0x00049CC0
	public string failQuest(string id)
	{
		string result;
		try
		{
			result = this.getQuest(id).failQuest();
		}
		catch (NullReferenceException obj)
		{
			MainControl.logError(obj);
			result = "";
		}
		return result;
	}

	// Token: 0x06001092 RID: 4242 RVA: 0x0004BAFC File Offset: 0x00049CFC
	public string completeQuest(string id)
	{
		string result;
		try
		{
			result = this.getQuest(id).completeQuest();
		}
		catch (NullReferenceException obj)
		{
			MainControl.logError(obj);
			result = "";
		}
		return result;
	}

	// Token: 0x06001093 RID: 4243 RVA: 0x0004BB38 File Offset: 0x00049D38
	public string sealQuest(string id)
	{
		string result;
		try
		{
			result = this.getQuest(id).sealQuest();
		}
		catch (NullReferenceException obj)
		{
			MainControl.logError(obj);
			result = "";
		}
		return result;
	}

	// Token: 0x06001094 RID: 4244 RVA: 0x0004BB74 File Offset: 0x00049D74
	public string rewardQuest(string id, Party party)
	{
		string result;
		try
		{
			result = this.getQuest(id).rewardQuest(party);
		}
		catch (NullReferenceException obj)
		{
			MainControl.logError(obj);
			result = "";
		}
		return result;
	}

	// Token: 0x06001095 RID: 4245 RVA: 0x0004BBB0 File Offset: 0x00049DB0
	public string getAboutDescription(string id)
	{
		string result;
		try
		{
			result = this.getQuest(id).getAboutDescriptionString();
		}
		catch (NullReferenceException obj)
		{
			MainControl.logError(obj);
			result = "";
		}
		return result;
	}

	// Token: 0x06001096 RID: 4246 RVA: 0x0004BBEC File Offset: 0x00049DEC
	public string getQuestTitle()
	{
		string result;
		try
		{
			result = this.getCurrentQuest().getName();
		}
		catch (NullReferenceException obj)
		{
			MainControl.logError(obj);
			result = "";
		}
		return result;
	}

	// Token: 0x06001097 RID: 4247 RVA: 0x0004BC28 File Offset: 0x00049E28
	public string printQuestList()
	{
		if (this.begunQuestsList.isEmpty())
		{
			return "You have no quests.";
		}
		return this.begunQuestsList.printList();
	}

	// Token: 0x06001098 RID: 4248 RVA: 0x0004BC48 File Offset: 0x00049E48
	public string printQuestListStatus()
	{
		string text = "QUEST STATES:\n";
		foreach (KeyValuePair<string, QuestControl.Quest> keyValuePair in this.questList)
		{
			text = text + keyValuePair.Value.printState() + "\n";
		}
		return text;
	}

	// Token: 0x06001099 RID: 4249 RVA: 0x0004BCB4 File Offset: 0x00049EB4
	private QuestControl.Quest getCurrentQuest()
	{
		return (QuestControl.Quest)this.begunQuestsList.getCurrentObject();
	}

	// Token: 0x040003F2 RID: 1010
	private Dictionary<string, QuestControl.Quest> questList;

	// Token: 0x040003F3 RID: 1011
	private SkaldObjectList begunQuestsList;

	// Token: 0x02000257 RID: 599
	[Serializable]
	protected class Quest : SkaldInstanceObject
	{
		// Token: 0x0600198D RID: 6541 RVA: 0x00070057 File Offset: 0x0006E257
		public Quest(SKALDProjectData.QuestContainers.QuestData rawData)
		{
			this.setId(rawData.id);
			this.opened = rawData.open;
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x00070084 File Offset: 0x0006E284
		public override string getName()
		{
			SKALDProjectData.QuestContainers.QuestData rawData = this.getRawData();
			if (rawData == null || rawData.title == "")
			{
				return this.getId();
			}
			return rawData.title;
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x000700BC File Offset: 0x0006E2BC
		public override string getListName()
		{
			string text = base.getListName();
			if (this.isSubquest())
			{
				int subquestDepth = this.getSubquestDepth();
				if (subquestDepth == 1)
				{
					text = "~ " + text;
				}
				else if (subquestDepth == 2)
				{
					text = "* " + text;
				}
				else
				{
					text = "- " + text;
				}
				for (int i = 0; i < subquestDepth; i++)
				{
					text = " " + text;
				}
				if (this.isQuestFinished())
				{
					if (this.isQuestSealed())
					{
						text += " [C]";
					}
					else if (this.isQuestFailed())
					{
						text += " [F]";
					}
					else
					{
						text += " [S]";
					}
				}
			}
			return text;
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x0007016B File Offset: 0x0006E36B
		public void setParent(string input)
		{
			this.parent = input;
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x00070174 File Offset: 0x0006E374
		public void validateParent()
		{
			string parentQuest = this.getRawData().parentQuest;
			if (parentQuest == "")
			{
				return;
			}
			if (this.parent != parentQuest)
			{
				MainControl.logError(string.Concat(new string[]
				{
					"Faulty parent for ",
					this.getId(),
					". Parent set to ",
					this.parent,
					". Expected is ",
					parentQuest
				}));
			}
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x000701E8 File Offset: 0x0006E3E8
		private int getSubquestDepth()
		{
			if (!this.isSubquest())
			{
				return 0;
			}
			string parentQuestID = this.getParentQuestID();
			int num = 0;
			while (parentQuestID != null && parentQuestID != "" && num < 6)
			{
				num++;
				QuestControl.Quest quest = MainControl.getDataControl().getQuestControl().getQuest(parentQuestID);
				if (quest == null)
				{
					return num;
				}
				parentQuestID = quest.getParentQuestID();
			}
			return num;
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x00070240 File Offset: 0x0006E440
		private SKALDProjectData.QuestContainers.QuestData getRawData()
		{
			return GameData.getQuestRawData(this.getId());
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x00070250 File Offset: 0x0006E450
		public string getBegunDescription()
		{
			SKALDProjectData.QuestContainers.QuestData rawData = this.getRawData();
			if (rawData != null)
			{
				return rawData.begunDescription;
			}
			return "Quest begun.";
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x00070273 File Offset: 0x0006E473
		public bool isSubquest()
		{
			return this.getParentQuestID() != "";
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x00070285 File Offset: 0x0006E485
		public string getParentQuestID()
		{
			return this.parent;
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x00070290 File Offset: 0x0006E490
		public string getFailedDescription()
		{
			SKALDProjectData.QuestContainers.QuestData rawData = this.getRawData();
			if (rawData != null)
			{
				return rawData.failedDescription;
			}
			return "Quest failed.";
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x000702B4 File Offset: 0x0006E4B4
		public string getCompletedDescription()
		{
			SKALDProjectData.QuestContainers.QuestData rawData = this.getRawData();
			if (rawData != null)
			{
				return rawData.completedDescription;
			}
			return "Quest completed.";
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x000702D8 File Offset: 0x0006E4D8
		public string getRewardDescription()
		{
			SKALDProjectData.QuestContainers.QuestData rawData = this.getRawData();
			if (rawData != null)
			{
				return rawData.rewardDescription;
			}
			return "Quest rewarded.";
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x000702FC File Offset: 0x0006E4FC
		public string getAboutDescription()
		{
			SKALDProjectData.QuestContainers.QuestData rawData = this.getRawData();
			if (rawData != null)
			{
				return rawData.aboutDescription;
			}
			return "I have nothing more to tell you.";
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x00070320 File Offset: 0x0006E520
		public string sealQuest()
		{
			if (this.isQuestFinished())
			{
				return "Quest was already finished " + this.getId();
			}
			this.terminated = true;
			SKALDProjectData.QuestContainers.QuestData rawData = this.getRawData();
			if (rawData == null)
			{
				return "Missing Raw Data but sealed Quest " + this.getId();
			}
			List<string> list = new List<string>();
			foreach (BaseDataObject baseDataObject in rawData.getBaseList())
			{
				SKALDProjectData.QuestContainers.QuestData questData = (SKALDProjectData.QuestContainers.QuestData)baseDataObject;
				list.Add(questData.id);
			}
			string text = "Sealed Quest " + this.getId();
			foreach (string questId in list)
			{
				text = text + "\n" + MainControl.getDataControl().sealQuest(questId);
			}
			return text;
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x00070424 File Offset: 0x0006E624
		public string getOpenTrigger()
		{
			SKALDProjectData.QuestContainers.QuestData rawData = this.getRawData();
			if (rawData != null)
			{
				return rawData.openTrigger;
			}
			return "";
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x00070448 File Offset: 0x0006E648
		public string getBegunTrigger()
		{
			SKALDProjectData.QuestContainers.QuestData rawData = this.getRawData();
			if (rawData != null)
			{
				return rawData.begunTrigger;
			}
			return "";
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x0007046C File Offset: 0x0006E66C
		public string getCompletedTrigger()
		{
			SKALDProjectData.QuestContainers.QuestData rawData = this.getRawData();
			if (rawData != null)
			{
				return rawData.completedTrigger;
			}
			return "";
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x00070490 File Offset: 0x0006E690
		public string getFailedTrigger()
		{
			SKALDProjectData.QuestContainers.QuestData rawData = this.getRawData();
			if (rawData != null)
			{
				return rawData.failedTrigger;
			}
			return "";
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x000704B4 File Offset: 0x0006E6B4
		public string getRewardTrigger()
		{
			SKALDProjectData.QuestContainers.QuestData rawData = this.getRawData();
			if (rawData != null)
			{
				return rawData.rewardTrigger;
			}
			return "";
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x000704D8 File Offset: 0x0006E6D8
		public string getSuccessCriteria()
		{
			SKALDProjectData.QuestContainers.QuestData rawData = this.getRawData();
			if (rawData != null)
			{
				return rawData.successCriteria;
			}
			return "";
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x000704FC File Offset: 0x0006E6FC
		public string getRewardLoadout()
		{
			SKALDProjectData.QuestContainers.QuestData rawData = this.getRawData();
			if (rawData != null)
			{
				return rawData.rewardLoadout;
			}
			return "";
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x00070520 File Offset: 0x0006E720
		public List<string> getPrerequisiteQuests()
		{
			SKALDProjectData.QuestContainers.QuestData rawData = this.getRawData();
			if (rawData != null)
			{
				return rawData.prerequisiteQuestsList;
			}
			return new List<string>();
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x00070544 File Offset: 0x0006E744
		public List<string> getStartedSubQuests()
		{
			SKALDProjectData.QuestContainers.QuestData rawData = this.getRawData();
			if (rawData != null)
			{
				return rawData.startsSubQuestsList;
			}
			return new List<string>();
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x00070568 File Offset: 0x0006E768
		public List<string> getNextQuests()
		{
			SKALDProjectData.QuestContainers.QuestData rawData = this.getRawData();
			if (rawData != null)
			{
				return rawData.nextQuestList;
			}
			return new List<string>();
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x0007058C File Offset: 0x0006E78C
		public int getRewardXP()
		{
			SKALDProjectData.QuestContainers.QuestData rawData = this.getRawData();
			if (rawData != null)
			{
				return rawData.rewardXP;
			}
			return 0;
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x000705AC File Offset: 0x0006E7AC
		public int getRewardGold()
		{
			SKALDProjectData.QuestContainers.QuestData rawData = this.getRawData();
			if (rawData != null)
			{
				return rawData.rewardGold;
			}
			return 0;
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x000705CC File Offset: 0x0006E7CC
		public int getRewardXPPercentage()
		{
			SKALDProjectData.QuestContainers.QuestData rawData = this.getRawData();
			if (rawData != null)
			{
				return rawData.rewardXPPercentage;
			}
			return 0;
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x000705EB File Offset: 0x0006E7EB
		public bool isQuestActive()
		{
			return this.isQuestBegun() && !this.isQuestFinished();
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x00070600 File Offset: 0x0006E800
		public bool isQuestFinished()
		{
			return this.isQuestRewarded() || this.isQuestFailed() || this.isQuestSealed();
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x0007061A File Offset: 0x0006E81A
		public bool isCurrentQuestStateOpen()
		{
			return this.isQuestOpen() && !this.isQuestBegun() && !this.isQuestFinished();
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x00070637 File Offset: 0x0006E837
		public bool isCurrentQuestStateBegun()
		{
			return this.isQuestBegun() && !this.isQuestCompleted();
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x0007064C File Offset: 0x0006E84C
		public bool isCurrentQuestStateCompleted()
		{
			return this.isQuestCompleted() && !this.isQuestRewarded();
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x00070661 File Offset: 0x0006E861
		public bool isCurrentQuestStateFailed()
		{
			return this.isQuestFailed();
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x00070669 File Offset: 0x0006E869
		public bool isCurrentQuestStateRewarded()
		{
			return this.isQuestRewarded();
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x00070674 File Offset: 0x0006E874
		public bool isMain()
		{
			SKALDProjectData.QuestContainers.QuestData rawData = this.getRawData();
			return rawData != null && rawData.main;
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x00070694 File Offset: 0x0006E894
		private bool completesParent()
		{
			SKALDProjectData.QuestContainers.QuestData rawData = this.getRawData();
			return rawData != null && rawData.completesParent;
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x000706B4 File Offset: 0x0006E8B4
		private bool isOptional()
		{
			SKALDProjectData.QuestContainers.QuestData rawData = this.getRawData();
			return rawData != null && rawData.optional;
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x000706D3 File Offset: 0x0006E8D3
		public bool isQuestOpen()
		{
			return this.opened;
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x000706DB File Offset: 0x0006E8DB
		public bool isQuestBegun()
		{
			return this.begun;
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x000706E3 File Offset: 0x0006E8E3
		public bool isQuestCompleted()
		{
			return this.completed;
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x000706EB File Offset: 0x0006E8EB
		public bool isQuestFailed()
		{
			return this.failed;
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x000706F3 File Offset: 0x0006E8F3
		public bool isQuestSealed()
		{
			return this.terminated;
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x000706FB File Offset: 0x0006E8FB
		public bool isQuestRewarded()
		{
			return this.rewarded;
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x00070704 File Offset: 0x0006E904
		public string openQuest()
		{
			if (!this.isQuestOpen() && !this.isQuestFinished())
			{
				this.opened = true;
				base.processString(this.getOpenTrigger(), null);
				return "Quest opened: " + this.getId();
			}
			return "Cannot open quest: " + this.getId();
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x00070758 File Offset: 0x0006E958
		public bool evaluateQuestSuccess()
		{
			if (!this.isQuestActive())
			{
				return false;
			}
			foreach (string questId in this.getPrerequisiteQuests())
			{
				if (!MainControl.getDataControl().isQuestRewarded(questId))
				{
					return false;
				}
			}
			return this.getSuccessCriteria() == "" || base.processString(this.getSuccessCriteria(), null).ToUpper().Contains("TRUE");
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x000707F4 File Offset: 0x0006E9F4
		private string getHovertextDescriptor()
		{
			if (this.isSubquest())
			{
				return "Sub-Quest";
			}
			return "Quest";
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x0007080C File Offset: 0x0006EA0C
		public string beginQuest()
		{
			if (this.isQuestOpen() && !this.isQuestBegun() && !this.isQuestFinished())
			{
				this.begun = true;
				base.processString(this.getBegunTrigger(), null);
				HoverElementControl.addHoverText(this.getHovertextDescriptor() + " Started:", this.getName());
				foreach (string questId in this.getStartedSubQuests())
				{
					MainControl.getDataControl().beginQuest(questId);
				}
				return base.processString(this.getBegunDescription(), null);
			}
			return "Cannot begin quest: " + this.getId();
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x000708D0 File Offset: 0x0006EAD0
		public string failQuest()
		{
			if (this.isQuestActive())
			{
				this.failed = true;
				foreach (string questId in this.getPrerequisiteQuests())
				{
					MainControl.getDataControl().failQuest(questId);
				}
				base.processString(this.getFailedTrigger(), null);
				HoverElementControl.addHoverText(this.getHovertextDescriptor() + " Failed:", this.getName());
				return base.processString(this.getFailedDescription(), null);
			}
			return "Cannot fail quest: " + this.getId();
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x00070980 File Offset: 0x0006EB80
		public string getAboutDescriptionString()
		{
			if (!this.about)
			{
				this.about = true;
			}
			return this.getAboutDescription();
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x00070998 File Offset: 0x0006EB98
		public string completeQuest()
		{
			if (this.isQuestActive() && !this.isQuestCompleted())
			{
				this.completed = true;
				base.processString(this.getCompletedTrigger(), null);
				return base.processString(this.getCompletedDescription(), null);
			}
			return "Cannot complete quest: " + this.getId();
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x000709E8 File Offset: 0x0006EBE8
		public string rewardQuest(Party party)
		{
			if (this.isQuestActive() && !this.isQuestRewarded())
			{
				this.rewarded = true;
				HoverElementControl.addHoverText(this.getHovertextDescriptor() + " Completed:", this.getName());
				GameData.applyLoadoutData(this.getRewardLoadout(), party.getInventory());
				if (this.getRewardGold() > 0)
				{
					party.getInventory().addMoney(this.getRewardGold());
					party.getCurrentCharacter().addPositiveBark("+" + this.getRewardGold().ToString() + " Gold");
				}
				if (this.getRewardXP() > 0)
				{
					party.addXp(this.getRewardXP());
				}
				if (this.getRewardXPPercentage() > 0)
				{
					party.addPercentageXPToNextLevelAll(this.getRewardXPPercentage());
				}
				base.processString(this.getRewardTrigger(), null);
				foreach (string questId in this.getNextQuests())
				{
					MainControl.getDataControl().beginQuest(questId);
				}
				if (this.completesParent() && MainControl.getDataControl().evaluateQuestSuccess(this.getParentQuestID()))
				{
					MainControl.getDataControl().completeAndRewardQuest(this.getParentQuestID());
				}
				return base.processString(this.getRewardDescription(), null);
			}
			return "Cannot reward quest: " + this.getId();
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x00070B50 File Offset: 0x0006ED50
		public override string getDescription()
		{
			SKALDProjectData.QuestContainers.QuestData questRawData = GameData.getQuestRawData(this.getId());
			if (questRawData == null)
			{
				return "";
			}
			string text = "";
			if (this.isQuestFailed() && questRawData.failedLogString != "")
			{
				text += questRawData.failedLogString;
			}
			else
			{
				if (this.isQuestBegun())
				{
					if (questRawData.begunLogString == "")
					{
						text += "Quest begun.";
					}
					else
					{
						text += base.processString(questRawData.begunLogString, null);
					}
				}
				if (this.isQuestCompleted())
				{
					text += "\n\n";
					if (questRawData.completedLogString == "")
					{
						text += "Quest Completed.";
					}
					else
					{
						text += base.processString(questRawData.completedLogString, null);
					}
				}
				if (this.isQuestRewarded() && questRawData.rewardLogString != "")
				{
					text = text + "\n\n" + base.processString(questRawData.rewardLogString, null);
				}
			}
			if (this.isQuestFinished())
			{
				if (this.isQuestSealed())
				{
					text = "[Closed]\n\n" + text;
				}
				else if (this.isQuestFailed())
				{
					text = "[Failed]\n\n" + text;
				}
				else
				{
					text = "[Success]\n\n" + text;
				}
			}
			else if (this.isOptional())
			{
				text = "[Optional]\n\n" + text;
			}
			return text;
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x00070CB4 File Offset: 0x0006EEB4
		public string printState()
		{
			if (this.isQuestSealed())
			{
				return this.getName() + ": Sealed";
			}
			if (this.isCurrentQuestStateRewarded())
			{
				return this.getName() + ": Rewarded";
			}
			if (this.isCurrentQuestStateFailed())
			{
				return this.getName() + ": Failed";
			}
			if (this.isCurrentQuestStateCompleted())
			{
				return this.getName() + ": Completed";
			}
			if (this.isCurrentQuestStateBegun())
			{
				return this.getName() + ": Begun";
			}
			if (this.isCurrentQuestStateOpen())
			{
				return this.getName() + ": Open";
			}
			return this.getName() + ": Closed";
		}

		// Token: 0x04000937 RID: 2359
		private bool opened;

		// Token: 0x04000938 RID: 2360
		private bool about;

		// Token: 0x04000939 RID: 2361
		private bool begun;

		// Token: 0x0400093A RID: 2362
		private bool completed;

		// Token: 0x0400093B RID: 2363
		private bool terminated;

		// Token: 0x0400093C RID: 2364
		private bool failed;

		// Token: 0x0400093D RID: 2365
		private bool rewarded;

		// Token: 0x0400093E RID: 2366
		private string parent = "";
	}
}
