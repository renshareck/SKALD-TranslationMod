using System;
using Galaxy.Api;
using UnityEngine;

namespace Gog
{
	// Token: 0x0200019D RID: 413
	public class StatsAndAchievements : MonoBehaviour
	{
		// Token: 0x06001575 RID: 5493 RVA: 0x000607BE File Offset: 0x0005E9BE
		private void OnEnable()
		{
			this.ListenersInit();
			this.RequestUserStatsAndAchievements();
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x000607CC File Offset: 0x0005E9CC
		private void OnDisable()
		{
			this.ListenersDispose();
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x000607D4 File Offset: 0x0005E9D4
		private void ListenersInit()
		{
			Listener.Create<StatsAndAchievements.UserStatsAndAchievementsRetrieveListener>(ref this.achievementRetrieveListener);
			Listener.Create<StatsAndAchievements.AchievementChangeListener>(ref this.achievementChangeListener);
			Listener.Create<StatsAndAchievements.StatsAndAchievementsStoreListener>(ref this.achievementStoreListener);
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x000607F7 File Offset: 0x0005E9F7
		private void ListenersDispose()
		{
			Listener.Dispose<StatsAndAchievements.StatsAndAchievementsStoreListener>(ref this.achievementStoreListener);
			Listener.Dispose<StatsAndAchievements.UserStatsAndAchievementsRetrieveListener>(ref this.achievementRetrieveListener);
			Listener.Dispose<StatsAndAchievements.AchievementChangeListener>(ref this.achievementChangeListener);
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x0006081C File Offset: 0x0005EA1C
		public void RequestUserStatsAndAchievements()
		{
			Debug.Log("Requesting Stats and Achievements");
			try
			{
				GalaxyInstance.Stats().RequestUserStatsAndAchievements();
			}
			catch (GalaxyInstance.Error error)
			{
				string str = "Achievements definitions could not be retrived for reason: ";
				GalaxyInstance.Error error2 = error;
				Debug.LogWarning(str + ((error2 != null) ? error2.ToString() : null));
			}
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x00060870 File Offset: 0x0005EA70
		public void SetAchievement(string apiKey)
		{
			Debug.Log("Trying to unlock achievement " + apiKey);
			try
			{
				GalaxyInstance.Stats().SetAchievement(apiKey);
				GalaxyInstance.Stats().StoreStatsAndAchievements();
			}
			catch (GalaxyInstance.Error error)
			{
				string str = "Achievement ";
				string str2 = " could not be unlocked for reason: ";
				GalaxyInstance.Error error2 = error;
				Debug.LogWarning(str + apiKey + str2 + ((error2 != null) ? error2.ToString() : null));
			}
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x000608DC File Offset: 0x0005EADC
		public bool GetAchievement(string apiKey)
		{
			Debug.Log("Trying to get achievement status for " + apiKey);
			bool result = false;
			try
			{
				uint num = 0U;
				GalaxyInstance.Stats().GetAchievement(apiKey, ref result, ref num);
				Debug.Log(string.Concat(new string[]
				{
					"Achievement: \"",
					apiKey,
					"\" unlocked: ",
					result.ToString(),
					" unlock time: ",
					num.ToString()
				}));
			}
			catch (GalaxyInstance.Error error)
			{
				string str = "Could not get status of achievement ";
				string str2 = " for reason: ";
				GalaxyInstance.Error error2 = error;
				Debug.LogWarning(str + apiKey + str2 + ((error2 != null) ? error2.ToString() : null));
			}
			return result;
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x00060984 File Offset: 0x0005EB84
		public string GetAchievementName(string apiKey)
		{
			Debug.Log("Trying to get achievement name " + apiKey);
			string text = "";
			try
			{
				text = GalaxyInstance.Stats().GetAchievementDisplayName(apiKey);
				Debug.Log("Achievement display name: " + text);
			}
			catch (GalaxyInstance.Error error)
			{
				string str = "Could not get name of achievement ";
				string str2 = " for reason: ";
				GalaxyInstance.Error error2 = error;
				Debug.LogWarning(str + apiKey + str2 + ((error2 != null) ? error2.ToString() : null));
			}
			return text;
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x000609FC File Offset: 0x0005EBFC
		public void SetStatFloat(string apiKey, float statValue)
		{
			Debug.Log("Setting stat " + apiKey);
			try
			{
				GalaxyInstance.Stats().SetStatFloat(apiKey, statValue);
				GalaxyInstance.Stats().StoreStatsAndAchievements();
				Debug.Log("Stat " + apiKey + " set to " + statValue.ToString());
			}
			catch (GalaxyInstance.Error error)
			{
				string str = "Could not set value of statistic ";
				string str2 = " for reason: ";
				GalaxyInstance.Error error2 = error;
				Debug.LogWarning(str + apiKey + str2 + ((error2 != null) ? error2.ToString() : null));
			}
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x00060A84 File Offset: 0x0005EC84
		public void SetStatInt(string apiKey, int statValue)
		{
			Debug.Log("Setting stat " + apiKey);
			try
			{
				GalaxyInstance.Stats().SetStatInt(apiKey, statValue);
				GalaxyInstance.Stats().StoreStatsAndAchievements();
				Debug.Log("Stat " + apiKey + " set to " + statValue.ToString());
			}
			catch (GalaxyInstance.Error error)
			{
				string str = "Could not set value of statistic ";
				string str2 = " for reason: ";
				GalaxyInstance.Error error2 = error;
				Debug.LogWarning(str + apiKey + str2 + ((error2 != null) ? error2.ToString() : null));
			}
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x00060B0C File Offset: 0x0005ED0C
		public float GetStatFloat(string apiKey)
		{
			Debug.Log("Getting stat " + apiKey);
			float result = 0f;
			try
			{
				result = GalaxyInstance.Stats().GetStatFloat(apiKey);
				Debug.Log("Stat with key " + apiKey + " has value " + result.ToString());
			}
			catch (GalaxyInstance.Error error)
			{
				string str = "Could not get value of statistic ";
				string str2 = " for reason: ";
				GalaxyInstance.Error error2 = error;
				Debug.LogWarning(str + apiKey + str2 + ((error2 != null) ? error2.ToString() : null));
			}
			return result;
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x00060B90 File Offset: 0x0005ED90
		public int GetStatInt(string apiKey)
		{
			Debug.Log("Getting stat " + apiKey);
			int result = 0;
			try
			{
				result = GalaxyInstance.Stats().GetStatInt(apiKey);
				Debug.Log("Stat with key " + apiKey + " has value " + result.ToString());
			}
			catch (GalaxyInstance.Error error)
			{
				string str = "Could not get value of statistic ";
				string str2 = " for reason: ";
				GalaxyInstance.Error error2 = error;
				Debug.LogWarning(str + apiKey + str2 + ((error2 != null) ? error2.ToString() : null));
			}
			return result;
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x00060C10 File Offset: 0x0005EE10
		public void ResetStatsAndAchievements()
		{
			Debug.Log("Trying to reset user stats and achievements");
			try
			{
				GalaxyInstance.Stats().ResetStatsAndAchievements();
				Debug.Log("User stats and achievements reset");
			}
			catch (GalaxyInstance.Error error)
			{
				string str = "Could not get reset user stats and achievements for reason: ";
				GalaxyInstance.Error error2 = error;
				Debug.LogWarning(str + ((error2 != null) ? error2.ToString() : null));
			}
		}

		// Token: 0x040005B8 RID: 1464
		private StatsAndAchievements.UserStatsAndAchievementsRetrieveListener achievementRetrieveListener;

		// Token: 0x040005B9 RID: 1465
		private StatsAndAchievements.AchievementChangeListener achievementChangeListener;

		// Token: 0x040005BA RID: 1466
		private StatsAndAchievements.StatsAndAchievementsStoreListener achievementStoreListener;

		// Token: 0x020002F0 RID: 752
		private class UserStatsAndAchievementsRetrieveListener : GlobalUserStatsAndAchievementsRetrieveListener
		{
			// Token: 0x06001C07 RID: 7175 RVA: 0x0007965A File Offset: 0x0007785A
			public override void OnUserStatsAndAchievementsRetrieveSuccess(GalaxyID userID)
			{
				this.retrieved = true;
				GogManager.Instance.StatsAndAchievements.SetAchievement("launchTheGame");
				Debug.Log("User " + ((userID != null) ? userID.ToString() : null) + " stats and achievements retrieved");
			}

			// Token: 0x06001C08 RID: 7176 RVA: 0x00079698 File Offset: 0x00077898
			public override void OnUserStatsAndAchievementsRetrieveFailure(GalaxyID userID, IUserStatsAndAchievementsRetrieveListener.FailureReason failureReason)
			{
				this.retrieved = false;
				Debug.LogWarning("User " + ((userID != null) ? userID.ToString() : null) + " stats and achievements could not be retrieved, for reason " + failureReason.ToString());
			}

			// Token: 0x04000A80 RID: 2688
			public bool retrieved;
		}

		// Token: 0x020002F1 RID: 753
		private class StatsAndAchievementsStoreListener : GlobalStatsAndAchievementsStoreListener
		{
			// Token: 0x06001C0A RID: 7178 RVA: 0x000796D7 File Offset: 0x000778D7
			public override void OnUserStatsAndAchievementsStoreFailure(IStatsAndAchievementsStoreListener.FailureReason failureReason)
			{
				Debug.LogWarning("OnUserStatsAndAchievementsStoreFailure: " + failureReason.ToString());
			}

			// Token: 0x06001C0B RID: 7179 RVA: 0x000796F5 File Offset: 0x000778F5
			public override void OnUserStatsAndAchievementsStoreSuccess()
			{
				Debug.Log("OnUserStatsAndAchievementsStoreSuccess");
			}
		}

		// Token: 0x020002F2 RID: 754
		private class AchievementChangeListener : GlobalAchievementChangeListener
		{
			// Token: 0x06001C0D RID: 7181 RVA: 0x00079709 File Offset: 0x00077909
			public override void OnAchievementUnlocked(string name)
			{
				Debug.Log("Achievement \"" + name + "\" unlocked");
			}
		}
	}
}
