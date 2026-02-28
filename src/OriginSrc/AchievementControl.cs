using System;
using Steamworks;
using UnityEngine;

// Token: 0x02000023 RID: 35
[Serializable]
public class AchievementControl
{
	// Token: 0x06000409 RID: 1033 RVA: 0x00012E1C File Offset: 0x0001101C
	public void setAchievement(string achievementId)
	{
		AchievementControl.Achievement achievement = this.tryToGetAchievement(achievementId);
		try
		{
			if (achievement != null)
			{
				achievement.updateProgress();
			}
		}
		catch (Exception obj)
		{
			MainControl.logError(obj);
		}
	}

	// Token: 0x0600040A RID: 1034 RVA: 0x00012E54 File Offset: 0x00011054
	public void lockAchievement(string achievementId)
	{
		AchievementControl.Achievement achievement = this.tryToGetAchievement(achievementId);
		if (achievement != null)
		{
			achievement.lockAchievement();
		}
	}

	// Token: 0x0600040B RID: 1035 RVA: 0x00012E74 File Offset: 0x00011074
	private AchievementControl.Achievement tryToGetAchievement(string id)
	{
		Debug.Log(id);
		AchievementControl.Achievement achievement;
		if (this.getAchievementsList().containsObject(id))
		{
			achievement = (this.getAchievementsList().getObject(id) as AchievementControl.Achievement);
		}
		else
		{
			achievement = AchievementControl.Achievement.instantiate(id);
			if (achievement != null)
			{
				this.getAchievementsList().add(achievement);
			}
		}
		return achievement;
	}

	// Token: 0x0600040C RID: 1036 RVA: 0x00012EC3 File Offset: 0x000110C3
	public string printLocalAchievements()
	{
		return this.getAchievementsList().printListSimplifiedString();
	}

	// Token: 0x0600040D RID: 1037 RVA: 0x00012ED0 File Offset: 0x000110D0
	private SkaldBaseList getAchievementsList()
	{
		if (this.achievements == null)
		{
			this.achievements = new SkaldBaseList();
		}
		return this.achievements;
	}

	// Token: 0x040000A8 RID: 168
	private SkaldBaseList achievements;

	// Token: 0x020001CA RID: 458
	[Serializable]
	private class Achievement : SkaldBaseObject
	{
		// Token: 0x06001694 RID: 5780 RVA: 0x00065570 File Offset: 0x00063770
		private Achievement(SKALDProjectData.AchievementContainers.Achievement rawData) : base(rawData)
		{
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x0006557C File Offset: 0x0006377C
		public static AchievementControl.Achievement instantiate(string id)
		{
			SKALDProjectData.AchievementContainers.Achievement achievementRawData = GameData.getAchievementRawData(id);
			if (achievementRawData == null)
			{
				return null;
			}
			return new AchievementControl.Achievement(achievementRawData);
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x0006559B File Offset: 0x0006379B
		private SKALDProjectData.AchievementContainers.Achievement getRawData()
		{
			return GameData.getAchievementRawData(this.getId());
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x000655A8 File Offset: 0x000637A8
		public override string getName()
		{
			SKALDProjectData.AchievementContainers.Achievement rawData = this.getRawData();
			string text = rawData.title;
			if (rawData.requiredAmount > 0)
			{
				text = string.Concat(new string[]
				{
					text,
					" (",
					this.currentProgress.ToString(),
					" / ",
					rawData.requiredAmount.ToString(),
					")"
				});
			}
			if (this.locked)
			{
				text += " [Locked]";
			}
			else if (this.isCompleted())
			{
				text += " [Completed]";
			}
			else
			{
				text += " [Incomplete]";
			}
			return text;
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x0006564C File Offset: 0x0006384C
		public override string getDescription()
		{
			SKALDProjectData.AchievementContainers.Achievement rawData = this.getRawData();
			if (rawData == null)
			{
				return "";
			}
			return rawData.description;
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x00065670 File Offset: 0x00063870
		public bool isCompleted()
		{
			SKALDProjectData.AchievementContainers.Achievement rawData = this.getRawData();
			int num = 1;
			if (rawData != null)
			{
				num = rawData.requiredAmount;
			}
			return this.currentProgress >= num;
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x0006569C File Offset: 0x0006389C
		public void updateProgress()
		{
			if (this.locked)
			{
				return;
			}
			SKALDProjectData.AchievementContainers.Achievement rawData = this.getRawData();
			if (rawData == null)
			{
				return;
			}
			if (!this.isCompleted())
			{
				this.currentProgress++;
			}
			this.updateBackend(rawData);
			if (this.isCompleted())
			{
				this.fireCompleteTrigger(rawData);
			}
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x000656E9 File Offset: 0x000638E9
		public void lockAchievement()
		{
			this.locked = true;
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x000656F4 File Offset: 0x000638F4
		private void fireCompleteTrigger(SKALDProjectData.AchievementContainers.Achievement rawData)
		{
			if (this.firedCompleteTrigger)
			{
				return;
			}
			this.firedCompleteTrigger = true;
			HoverElementControl.addHoverText("Achievement Unlocked", rawData.title);
			base.processString(rawData.addedTrigger, null);
			MainControl.getDataControl().getMainCharacter().addAbilities(rawData.conferredAbilitites);
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x00065744 File Offset: 0x00063944
		private void updateBackend(SKALDProjectData.AchievementContainers.Achievement rawData)
		{
			if (!SteamManager.Initialized)
			{
				return;
			}
			if (!string.IsNullOrEmpty(rawData.statName))
			{
				SteamUserStats.SetStat(rawData.statName, this.currentProgress);
			}
			if (this.isCompleted())
			{
				SteamUserStats.SetAchievement(rawData.id);
			}
			SteamUserStats.StoreStats();
		}

		// Token: 0x0400070E RID: 1806
		private int currentProgress;

		// Token: 0x0400070F RID: 1807
		private bool locked;

		// Token: 0x04000710 RID: 1808
		private bool firedCompleteTrigger;
	}
}
