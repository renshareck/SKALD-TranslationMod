using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using UnityEngine;

// Token: 0x0200003C RID: 60
public static class GlobalSettings
{
	// Token: 0x060007D3 RID: 2003 RVA: 0x000276CD File Offset: 0x000258CD
	public static GlobalSettings.GamePlaySettings getGamePlaySettings()
	{
		if (GlobalSettings.gamePlaySettings == null)
		{
			GlobalSettings.gamePlaySettings = new GlobalSettings.GamePlaySettings();
		}
		return GlobalSettings.gamePlaySettings;
	}

	// Token: 0x060007D4 RID: 2004 RVA: 0x000276E5 File Offset: 0x000258E5
	public static GlobalSettings.DisplaySettings getDisplaySettings()
	{
		if (GlobalSettings.displaySettings == null)
		{
			GlobalSettings.displaySettings = new GlobalSettings.DisplaySettings();
		}
		return GlobalSettings.displaySettings;
	}

	// Token: 0x060007D5 RID: 2005 RVA: 0x000276FD File Offset: 0x000258FD
	public static GlobalSettings.FontSettings getFontSettings()
	{
		if (GlobalSettings.fontSettings == null)
		{
			GlobalSettings.fontSettings = new GlobalSettings.FontSettings();
		}
		return GlobalSettings.fontSettings;
	}

	// Token: 0x060007D6 RID: 2006 RVA: 0x00027715 File Offset: 0x00025915
	public static GlobalSettings.AudioSettings getAudioSettings()
	{
		if (GlobalSettings.audioSettings == null)
		{
			GlobalSettings.audioSettings = new GlobalSettings.AudioSettings();
		}
		return GlobalSettings.audioSettings;
	}

	// Token: 0x060007D7 RID: 2007 RVA: 0x0002772D File Offset: 0x0002592D
	public static GlobalSettings.DifficultySettings getDifficultySettings()
	{
		if (GlobalSettings.difficultySettings == null)
		{
			GlobalSettings.difficultySettings = new GlobalSettings.DifficultySettings();
		}
		return GlobalSettings.difficultySettings;
	}

	// Token: 0x060007D8 RID: 2008 RVA: 0x00027745 File Offset: 0x00025945
	public static void setDifficultySettings(GlobalSettings.DifficultySettings settings)
	{
		if (settings != null)
		{
			GlobalSettings.difficultySettings = settings;
		}
	}

	// Token: 0x060007D9 RID: 2009 RVA: 0x00027750 File Offset: 0x00025950
	public static void setGamePlaySettings(GlobalSettings.GamePlaySettings settings)
	{
		if (settings != null)
		{
			GlobalSettings.gamePlaySettings = settings;
		}
	}

	// Token: 0x060007DA RID: 2010 RVA: 0x0002775B File Offset: 0x0002595B
	public static void setDisplaySettings(GlobalSettings.DisplaySettings settings)
	{
		if (settings != null)
		{
			GlobalSettings.displaySettings = settings;
		}
	}

	// Token: 0x060007DB RID: 2011 RVA: 0x00027766 File Offset: 0x00025966
	public static void setAudioSettings(GlobalSettings.AudioSettings settings)
	{
		if (settings != null)
		{
			GlobalSettings.audioSettings = settings;
		}
	}

	// Token: 0x060007DC RID: 2012 RVA: 0x00027771 File Offset: 0x00025971
	public static void setFontSettings(GlobalSettings.FontSettings settings)
	{
		if (settings != null)
		{
			GlobalSettings.fontSettings = settings;
		}
	}

	// Token: 0x0400018D RID: 397
	private static GlobalSettings.GamePlaySettings gamePlaySettings;

	// Token: 0x0400018E RID: 398
	private static GlobalSettings.DifficultySettings difficultySettings;

	// Token: 0x0400018F RID: 399
	private static GlobalSettings.DisplaySettings displaySettings;

	// Token: 0x04000190 RID: 400
	private static GlobalSettings.AudioSettings audioSettings;

	// Token: 0x04000191 RID: 401
	private static GlobalSettings.FontSettings fontSettings;

	// Token: 0x020001EE RID: 494
	[Serializable]
	public class DifficultySettings : GlobalSettings.SettingsCollection, ISerializable
	{
		// Token: 0x06001767 RID: 5991 RVA: 0x00067F8C File Offset: 0x0006618C
		public DifficultySettings() : base("Difficulty Settings")
		{
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x00067F99 File Offset: 0x00066199
		public DifficultySettings(SerializationInfo info, StreamingContext context)
		{
			base.load(info, context);
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x00067FAC File Offset: 0x000661AC
		public override void initialize()
		{
			this.objectList.Clear();
			this.difficultySelection = new GlobalSettings.DifficultySettings.DifficultySelection();
			this.add(this.difficultySelection);
			this.playerAttributeCheckRerolls = new GlobalSettings.DifficultySettings.PlayerAttributeRerollSetting();
			this.add(this.playerAttributeCheckRerolls);
			this.playerHitRerolls = new GlobalSettings.DifficultySettings.PlayerHitRerollSetting();
			this.add(this.playerHitRerolls);
			this.playerDamageRerollSetting = new GlobalSettings.DifficultySettings.PlayerDamageRerollSetting();
			this.add(this.playerDamageRerollSetting);
			this.enemyHitRerolls = new GlobalSettings.DifficultySettings.EnemyHitRerollSetting();
			this.add(this.enemyHitRerolls);
			this.enemyDamageRerollSetting = new GlobalSettings.DifficultySettings.EnemyDamageRerollSetting();
			this.add(this.enemyDamageRerollSetting);
			this.missStreakRerollSetting = new GlobalSettings.DifficultySettings.MissStreakRerollSetting();
			this.add(this.missStreakRerollSetting);
			this.healAfterCombatSetting = new GlobalSettings.DifficultySettings.HealAfterCombatSetting();
			this.add(this.healAfterCombatSetting);
			this.ignoreEncumberanceSetting = new GlobalSettings.DifficultySettings.IgnoreEncumberanceSetting();
			this.add(this.ignoreEncumberanceSetting);
			this.cannotDieSetting = new GlobalSettings.DifficultySettings.CannotDieSetting();
			this.add(this.cannotDieSetting);
			this.xpForDowned = new GlobalSettings.DifficultySettings.XpForDownedCharacters();
			this.add(this.xpForDowned);
			this.ignoreFoodSetting = new GlobalSettings.DifficultySettings.IgnoreFoodSetting();
			this.add(this.ignoreFoodSetting);
			this.ignoreTrashMobsSetting = new GlobalSettings.DifficultySettings.IgnoreTrashMobsSetting();
			this.add(this.ignoreTrashMobsSetting);
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x000680FC File Offset: 0x000662FC
		public override string getListName()
		{
			return base.getName() + ": " + this.getCurrentDifficultyName();
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x00068114 File Offset: 0x00066314
		public static List<string> getDifficultySettingsIdList()
		{
			List<string> difficultyDataList = GameData.getDifficultyDataList();
			difficultyDataList.Insert(0, GlobalSettings.DifficultySettings.CUSTOM_TAG);
			return difficultyDataList;
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x00068128 File Offset: 0x00066328
		public string getCurrentDifficultyDescription()
		{
			SKALDProjectData.Objects.DifficultyContainer.DifficultyData difficultySettingsRawData = this.getDifficultySettingsRawData();
			if (difficultySettingsRawData != null)
			{
				return difficultySettingsRawData.description;
			}
			return "Custom difficulty.";
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x0006814C File Offset: 0x0006634C
		public string getCurrentDifficultyName()
		{
			SKALDProjectData.Objects.DifficultyContainer.DifficultyData difficultySettingsRawData = this.getDifficultySettingsRawData();
			if (difficultySettingsRawData != null)
			{
				return difficultySettingsRawData.title;
			}
			return "CUSTOM";
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x0006816F File Offset: 0x0006636F
		public bool getXpForDowned()
		{
			return this.xpForDowned.getState();
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x0006817C File Offset: 0x0006637C
		public int getDifficultyLevel()
		{
			return this.difficultySelection.getState();
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x00068189 File Offset: 0x00066389
		public void setDifficultyLevel(int target)
		{
			this.difficultySelection.forceSetDifficultyLevel(target);
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x00068198 File Offset: 0x00066398
		public SKALDProjectData.Objects.DifficultyContainer.DifficultyData getDifficultySettingsRawData()
		{
			string currentDifficultyId = this.difficultySelection.getCurrentDifficultyId();
			if (currentDifficultyId == GlobalSettings.DifficultySettings.CUSTOM_TAG)
			{
				return null;
			}
			return GameData.getDifficultyData(currentDifficultyId);
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x000681C6 File Offset: 0x000663C6
		public bool ignoreTrashMobs()
		{
			return this.ignoreTrashMobsSetting != null && this.ignoreTrashMobsSetting.getState();
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x000681DD File Offset: 0x000663DD
		public void forceCustomDifficulty()
		{
			if (this.difficultySelection == null)
			{
				return;
			}
			this.difficultySelection.forceCustomDifficulty();
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x000681F3 File Offset: 0x000663F3
		public bool isCurrentDifficultyCustom()
		{
			return this.difficultySelection != null && this.difficultySelection.isCurrentDifficultyCustom();
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x0006820A File Offset: 0x0006640A
		public bool ignoreFood()
		{
			return this.ignoreFoodSetting != null && this.ignoreFoodSetting.getState();
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x00068221 File Offset: 0x00066421
		public bool cannotDie()
		{
			return this.cannotDieSetting != null && this.cannotDieSetting.getState();
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x00068238 File Offset: 0x00066438
		public bool ignoreEncumberance()
		{
			return this.ignoreEncumberanceSetting != null && this.ignoreEncumberanceSetting.getState();
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x0006824F File Offset: 0x0006644F
		public bool healFullyAfterCombat()
		{
			return this.healAfterCombatSetting != null && this.healAfterCombatSetting.getState();
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x00068266 File Offset: 0x00066466
		public int getPlayerToHitRerolls()
		{
			if (this.playerHitRerolls == null)
			{
				return 1;
			}
			return this.playerHitRerolls.getState();
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x0006827D File Offset: 0x0006647D
		public int getPlayerAttributeRerolls()
		{
			if (this.playerAttributeCheckRerolls == null)
			{
				return 1;
			}
			return this.playerAttributeCheckRerolls.getState();
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x00068294 File Offset: 0x00066494
		public int getEnemyToHitRerolls()
		{
			if (this.enemyHitRerolls == null)
			{
				return 1;
			}
			return this.enemyHitRerolls.getState();
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x000682AB File Offset: 0x000664AB
		public int getPlayerDamageRerolls()
		{
			if (this.playerDamageRerollSetting == null)
			{
				return 1;
			}
			return this.playerDamageRerollSetting.getState();
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x000682C2 File Offset: 0x000664C2
		public int getEnemyDamageRerolls()
		{
			if (this.enemyDamageRerollSetting == null)
			{
				return 1;
			}
			return this.enemyDamageRerollSetting.getState();
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x000682D9 File Offset: 0x000664D9
		public int getMissSmoothening()
		{
			if (this.missStreakRerollSetting == null)
			{
				return 1;
			}
			return this.missStreakRerollSetting.getState();
		}

		// Token: 0x0400079B RID: 1947
		public static string CUSTOM_TAG = "CUSTOM";

		// Token: 0x0400079C RID: 1948
		private GlobalSettings.DifficultySettings.DifficultySelection difficultySelection;

		// Token: 0x0400079D RID: 1949
		private GlobalSettings.DifficultySettings.PlayerAttributeRerollSetting playerAttributeCheckRerolls;

		// Token: 0x0400079E RID: 1950
		private GlobalSettings.DifficultySettings.PlayerHitRerollSetting playerHitRerolls;

		// Token: 0x0400079F RID: 1951
		private GlobalSettings.DifficultySettings.EnemyHitRerollSetting enemyHitRerolls;

		// Token: 0x040007A0 RID: 1952
		private GlobalSettings.DifficultySettings.PlayerDamageRerollSetting playerDamageRerollSetting;

		// Token: 0x040007A1 RID: 1953
		private GlobalSettings.DifficultySettings.EnemyDamageRerollSetting enemyDamageRerollSetting;

		// Token: 0x040007A2 RID: 1954
		private GlobalSettings.DifficultySettings.MissStreakRerollSetting missStreakRerollSetting;

		// Token: 0x040007A3 RID: 1955
		private GlobalSettings.DifficultySettings.HealAfterCombatSetting healAfterCombatSetting;

		// Token: 0x040007A4 RID: 1956
		private GlobalSettings.DifficultySettings.IgnoreEncumberanceSetting ignoreEncumberanceSetting;

		// Token: 0x040007A5 RID: 1957
		private GlobalSettings.DifficultySettings.CannotDieSetting cannotDieSetting;

		// Token: 0x040007A6 RID: 1958
		private GlobalSettings.DifficultySettings.IgnoreFoodSetting ignoreFoodSetting;

		// Token: 0x040007A7 RID: 1959
		private GlobalSettings.DifficultySettings.IgnoreTrashMobsSetting ignoreTrashMobsSetting;

		// Token: 0x040007A8 RID: 1960
		private GlobalSettings.DifficultySettings.XpForDownedCharacters xpForDowned;

		// Token: 0x02000305 RID: 773
		private class DifficultySelection : GlobalSettings.SettingsCollection.CarouselSetting
		{
			// Token: 0x06001C29 RID: 7209 RVA: 0x00079F2C File Offset: 0x0007812C
			public DifficultySelection() : base("difficultySelection", "Difficulty", "The current difficulty of the game.", GlobalSettings.DifficultySettings.getDifficultySettingsIdList(), 3)
			{
			}

			// Token: 0x06001C2A RID: 7210 RVA: 0x00079F49 File Offset: 0x00078149
			public string getCurrentDifficultyId()
			{
				base.boundState();
				if (this.alternatives == null)
				{
					return GlobalSettings.DifficultySettings.CUSTOM_TAG;
				}
				return this.alternatives[this.getState()];
			}

			// Token: 0x06001C2B RID: 7211 RVA: 0x00079F70 File Offset: 0x00078170
			public void forceSetDifficultyLevel(int target)
			{
				this.state = target;
				base.boundState();
			}

			// Token: 0x06001C2C RID: 7212 RVA: 0x00079F7F File Offset: 0x0007817F
			public void forceCustomDifficulty()
			{
				if (this.isCurrentDifficultyCustom())
				{
					return;
				}
				PopUpControl.addPopUpOK("You're trying to edit the difficulty so the difficulty options are now being set to the ‘CUSTOM’ values.\n\nThe custom difficulty will be saved and you can toggle back and forth between ‘CUSTOM’ difficulty and the pre-set difficulties any time. ");
				this.forceSetDifficultyLevel(0);
			}

			// Token: 0x06001C2D RID: 7213 RVA: 0x00079F9B File Offset: 0x0007819B
			public bool isCurrentDifficultyCustom()
			{
				return this.getState() == 0;
			}

			// Token: 0x06001C2E RID: 7214 RVA: 0x00079FA6 File Offset: 0x000781A6
			public override string printState()
			{
				return GlobalSettings.getDifficultySettings().getCurrentDifficultyName();
			}

			// Token: 0x06001C2F RID: 7215 RVA: 0x00079FB2 File Offset: 0x000781B2
			public override string getDescription()
			{
				return base.getDescription() + "\n" + GlobalSettings.getDifficultySettings().getCurrentDifficultyDescription();
			}

			// Token: 0x04000AA4 RID: 2724
			private const int CUSTOM_INDEX = 0;
		}

		// Token: 0x02000306 RID: 774
		protected abstract class DifficultyNumericCarouselSetting : GlobalSettings.SettingsCollection.NumericCarouselSetting
		{
			// Token: 0x06001C30 RID: 7216 RVA: 0x00079FCE File Offset: 0x000781CE
			public DifficultyNumericCarouselSetting(string id, string name, string description, int max, int min, int startingState) : base(id, name, description, max, min, startingState)
			{
			}

			// Token: 0x06001C31 RID: 7217 RVA: 0x00079FDF File Offset: 0x000781DF
			public override void incrementState(int index)
			{
				GlobalSettings.getDifficultySettings().forceCustomDifficulty();
				base.incrementState(index);
			}
		}

		// Token: 0x02000307 RID: 775
		protected abstract class DifficultyBoolSetting : GlobalSettings.SettingsCollection.BoolSetting
		{
			// Token: 0x06001C32 RID: 7218 RVA: 0x00079FF2 File Offset: 0x000781F2
			public DifficultyBoolSetting(string id, string name, string description, bool startingState) : base(id, name, description, startingState)
			{
			}

			// Token: 0x06001C33 RID: 7219 RVA: 0x00079FFF File Offset: 0x000781FF
			public override void incrementState(int index)
			{
				GlobalSettings.getDifficultySettings().forceCustomDifficulty();
				base.incrementState(index);
			}
		}

		// Token: 0x02000308 RID: 776
		protected class PlayerAttributeRerollSetting : GlobalSettings.DifficultySettings.DifficultyNumericCarouselSetting
		{
			// Token: 0x06001C34 RID: 7220 RVA: 0x0007A012 File Offset: 0x00078212
			public PlayerAttributeRerollSetting() : base("PLAYER_ATTRIBUTE_REROLLS", "Attribute Rerolls", "If this number is greater than zero, the system will reroll any of the player's Attribute Check (Skill Checks for instance) dice rolls that many times and pick the most favorable one to the player.", 10, -10, 1)
			{
			}

			// Token: 0x06001C35 RID: 7221 RVA: 0x0007A030 File Offset: 0x00078230
			public override int getState()
			{
				if (!GlobalSettings.getDifficultySettings().isCurrentDifficultyCustom())
				{
					SKALDProjectData.Objects.DifficultyContainer.DifficultyData difficultySettingsRawData = GlobalSettings.getDifficultySettings().getDifficultySettingsRawData();
					if (difficultySettingsRawData != null)
					{
						return difficultySettingsRawData.playerAttributeTestRerolls;
					}
				}
				return base.getState();
			}
		}

		// Token: 0x02000309 RID: 777
		protected class PlayerHitRerollSetting : GlobalSettings.DifficultySettings.DifficultyNumericCarouselSetting
		{
			// Token: 0x06001C36 RID: 7222 RVA: 0x0007A064 File Offset: 0x00078264
			public PlayerHitRerollSetting() : base("PLAYER_REROLLS", "Player Rerolls", "If this number is greater than zero, the system will reroll any of the player's Combat dice rolls (such as Attack Rolls or Defence Rolls) that many times and pick the most favorable one to the player.", 10, -10, 1)
			{
			}

			// Token: 0x06001C37 RID: 7223 RVA: 0x0007A080 File Offset: 0x00078280
			public override int getState()
			{
				if (!GlobalSettings.getDifficultySettings().isCurrentDifficultyCustom())
				{
					SKALDProjectData.Objects.DifficultyContainer.DifficultyData difficultySettingsRawData = GlobalSettings.getDifficultySettings().getDifficultySettingsRawData();
					if (difficultySettingsRawData != null)
					{
						return difficultySettingsRawData.playerRerolls;
					}
				}
				return base.getState();
			}
		}

		// Token: 0x0200030A RID: 778
		protected class EnemyHitRerollSetting : GlobalSettings.DifficultySettings.DifficultyNumericCarouselSetting
		{
			// Token: 0x06001C38 RID: 7224 RVA: 0x0007A0B4 File Offset: 0x000782B4
			public EnemyHitRerollSetting() : base("ENEMY_REROLLS", "Enemy Rerolls", "If this number is greater than zero, the system will reroll any of the enemy's Combat dice rolls (such as Attack Rolls or Defence Rolls) that many times and pick the most favorable one to the enemy.", 10, -10, 1)
			{
			}

			// Token: 0x06001C39 RID: 7225 RVA: 0x0007A0D0 File Offset: 0x000782D0
			public override int getState()
			{
				if (!GlobalSettings.getDifficultySettings().isCurrentDifficultyCustom())
				{
					SKALDProjectData.Objects.DifficultyContainer.DifficultyData difficultySettingsRawData = GlobalSettings.getDifficultySettings().getDifficultySettingsRawData();
					if (difficultySettingsRawData != null)
					{
						return difficultySettingsRawData.enemyRerolls;
					}
				}
				return base.getState();
			}
		}

		// Token: 0x0200030B RID: 779
		protected class PlayerDamageRerollSetting : GlobalSettings.DifficultySettings.DifficultyNumericCarouselSetting
		{
			// Token: 0x06001C3A RID: 7226 RVA: 0x0007A104 File Offset: 0x00078304
			public PlayerDamageRerollSetting() : base("PLAYER_DAMAGE_REROLLS", "P. Dmg. Rerolls", "If this number is greater than zero, the system will reroll any of the player's damage dice rolls that many times and pick the most favorable one to the player.", 10, -10, 0)
			{
			}

			// Token: 0x06001C3B RID: 7227 RVA: 0x0007A120 File Offset: 0x00078320
			public override int getState()
			{
				if (!GlobalSettings.getDifficultySettings().isCurrentDifficultyCustom())
				{
					SKALDProjectData.Objects.DifficultyContainer.DifficultyData difficultySettingsRawData = GlobalSettings.getDifficultySettings().getDifficultySettingsRawData();
					if (difficultySettingsRawData != null)
					{
						return difficultySettingsRawData.playerDamageRerolls;
					}
				}
				return base.getState();
			}
		}

		// Token: 0x0200030C RID: 780
		protected class EnemyDamageRerollSetting : GlobalSettings.DifficultySettings.DifficultyNumericCarouselSetting
		{
			// Token: 0x06001C3C RID: 7228 RVA: 0x0007A154 File Offset: 0x00078354
			public EnemyDamageRerollSetting() : base("ENEMY_DAMAGE_REROLLS", "E. Dmg. Rerolls", "If this number is greater than zero, the system will reroll any of the enemy's damage dice rolls that many times and pick the most favorable one to the enemy.", 10, -10, 0)
			{
			}

			// Token: 0x06001C3D RID: 7229 RVA: 0x0007A170 File Offset: 0x00078370
			public override int getState()
			{
				if (!GlobalSettings.getDifficultySettings().isCurrentDifficultyCustom())
				{
					SKALDProjectData.Objects.DifficultyContainer.DifficultyData difficultySettingsRawData = GlobalSettings.getDifficultySettings().getDifficultySettingsRawData();
					if (difficultySettingsRawData != null)
					{
						return difficultySettingsRawData.enemyDamageRerolls;
					}
				}
				return base.getState();
			}
		}

		// Token: 0x0200030D RID: 781
		protected class MissStreakRerollSetting : GlobalSettings.DifficultySettings.DifficultyNumericCarouselSetting
		{
			// Token: 0x06001C3E RID: 7230 RVA: 0x0007A1A4 File Offset: 0x000783A4
			public MissStreakRerollSetting() : base("MISS_SMOOTHENINGING", "Miss Smoothening", "The degree to which the engine compensates player miss streaks by increasing To-Hit chances.", 10, -10, 3)
			{
			}

			// Token: 0x06001C3F RID: 7231 RVA: 0x0007A1C0 File Offset: 0x000783C0
			public override int getState()
			{
				if (!GlobalSettings.getDifficultySettings().isCurrentDifficultyCustom())
				{
					SKALDProjectData.Objects.DifficultyContainer.DifficultyData difficultySettingsRawData = GlobalSettings.getDifficultySettings().getDifficultySettingsRawData();
					if (difficultySettingsRawData != null)
					{
						return difficultySettingsRawData.maxMissSmoothing;
					}
				}
				return base.getState();
			}
		}

		// Token: 0x0200030E RID: 782
		protected class XpForDownedCharacters : GlobalSettings.DifficultySettings.DifficultyBoolSetting
		{
			// Token: 0x06001C40 RID: 7232 RVA: 0x0007A1F4 File Offset: 0x000783F4
			public XpForDownedCharacters() : base("XP_FOR_DOWNED", "XP For KO Char.", "If Enabled, XP will also be given to downed characters after combat.", false)
			{
			}

			// Token: 0x06001C41 RID: 7233 RVA: 0x0007A20C File Offset: 0x0007840C
			public override bool getState()
			{
				if (!GlobalSettings.getDifficultySettings().isCurrentDifficultyCustom())
				{
					SKALDProjectData.Objects.DifficultyContainer.DifficultyData difficultySettingsRawData = GlobalSettings.getDifficultySettings().getDifficultySettingsRawData();
					if (difficultySettingsRawData != null)
					{
						return difficultySettingsRawData.xpForDowned;
					}
				}
				return base.getState();
			}
		}

		// Token: 0x0200030F RID: 783
		protected class HealAfterCombatSetting : GlobalSettings.DifficultySettings.DifficultyBoolSetting
		{
			// Token: 0x06001C42 RID: 7234 RVA: 0x0007A240 File Offset: 0x00078440
			public HealAfterCombatSetting() : base("HEAL_AFTER_COMBAT", "Combat Heal", "If Enabled, the party will heal fully after each combat.", false)
			{
			}

			// Token: 0x06001C43 RID: 7235 RVA: 0x0007A258 File Offset: 0x00078458
			public override bool getState()
			{
				if (!GlobalSettings.getDifficultySettings().isCurrentDifficultyCustom())
				{
					SKALDProjectData.Objects.DifficultyContainer.DifficultyData difficultySettingsRawData = GlobalSettings.getDifficultySettings().getDifficultySettingsRawData();
					if (difficultySettingsRawData != null)
					{
						return difficultySettingsRawData.healFullyAfterCombat;
					}
				}
				return base.getState();
			}
		}

		// Token: 0x02000310 RID: 784
		protected class IgnoreEncumberanceSetting : GlobalSettings.DifficultySettings.DifficultyBoolSetting
		{
			// Token: 0x06001C44 RID: 7236 RVA: 0x0007A28C File Offset: 0x0007848C
			public IgnoreEncumberanceSetting() : base("IGNORE_ENCUMBERANCE", "No Encumb.", "If Enabled, the party has no limit to carrying capacity.", false)
			{
			}

			// Token: 0x06001C45 RID: 7237 RVA: 0x0007A2A4 File Offset: 0x000784A4
			public override bool getState()
			{
				if (!GlobalSettings.getDifficultySettings().isCurrentDifficultyCustom())
				{
					SKALDProjectData.Objects.DifficultyContainer.DifficultyData difficultySettingsRawData = GlobalSettings.getDifficultySettings().getDifficultySettingsRawData();
					if (difficultySettingsRawData != null)
					{
						return difficultySettingsRawData.ignoreEncumberance;
					}
				}
				return base.getState();
			}
		}

		// Token: 0x02000311 RID: 785
		protected class CannotDieSetting : GlobalSettings.DifficultySettings.DifficultyBoolSetting
		{
			// Token: 0x06001C46 RID: 7238 RVA: 0x0007A2D8 File Offset: 0x000784D8
			public CannotDieSetting() : base("CANNOT_DIE", "Cannot Die", "If Enabled, the main PC can never die in combat.", false)
			{
			}

			// Token: 0x06001C47 RID: 7239 RVA: 0x0007A2F0 File Offset: 0x000784F0
			public override bool getState()
			{
				if (!GlobalSettings.getDifficultySettings().isCurrentDifficultyCustom())
				{
					SKALDProjectData.Objects.DifficultyContainer.DifficultyData difficultySettingsRawData = GlobalSettings.getDifficultySettings().getDifficultySettingsRawData();
					if (difficultySettingsRawData != null)
					{
						return difficultySettingsRawData.cannotDie;
					}
				}
				return base.getState();
			}
		}

		// Token: 0x02000312 RID: 786
		protected class IgnoreFoodSetting : GlobalSettings.DifficultySettings.DifficultyBoolSetting
		{
			// Token: 0x06001C48 RID: 7240 RVA: 0x0007A324 File Offset: 0x00078524
			public IgnoreFoodSetting() : base("IGNORE_FOOD", "Ignore Food", "If Enabled, the party does not need to consume food.", false)
			{
			}

			// Token: 0x06001C49 RID: 7241 RVA: 0x0007A33C File Offset: 0x0007853C
			public override bool getState()
			{
				if (!GlobalSettings.getDifficultySettings().isCurrentDifficultyCustom())
				{
					SKALDProjectData.Objects.DifficultyContainer.DifficultyData difficultySettingsRawData = GlobalSettings.getDifficultySettings().getDifficultySettingsRawData();
					if (difficultySettingsRawData != null)
					{
						return difficultySettingsRawData.ignoreFood;
					}
				}
				return base.getState();
			}
		}

		// Token: 0x02000313 RID: 787
		protected class IgnoreTrashMobsSetting : GlobalSettings.DifficultySettings.DifficultyBoolSetting
		{
			// Token: 0x06001C4A RID: 7242 RVA: 0x0007A370 File Offset: 0x00078570
			public IgnoreTrashMobsSetting() : base("IGNORE_TRASH_MOB", "No Trash Mobs", "If Enabled, the party will not encounter 'trash mob' encounters on the overland map.", false)
			{
			}

			// Token: 0x06001C4B RID: 7243 RVA: 0x0007A388 File Offset: 0x00078588
			public override bool getState()
			{
				if (!GlobalSettings.getDifficultySettings().isCurrentDifficultyCustom())
				{
					SKALDProjectData.Objects.DifficultyContainer.DifficultyData difficultySettingsRawData = GlobalSettings.getDifficultySettings().getDifficultySettingsRawData();
					if (difficultySettingsRawData != null)
					{
						return difficultySettingsRawData.ignoreTrashMobs;
					}
				}
				return base.getState();
			}
		}
	}

	// Token: 0x020001EF RID: 495
	[Serializable]
	public class AudioSettings : GlobalSettings.SettingsCollection, ISerializable
	{
		// Token: 0x06001780 RID: 6016 RVA: 0x000682FC File Offset: 0x000664FC
		public AudioSettings() : base("Audio Settings")
		{
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x00068309 File Offset: 0x00066509
		public AudioSettings(SerializationInfo info, StreamingContext context)
		{
			base.load(info, context);
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x00068319 File Offset: 0x00066519
		public override void initialize()
		{
			this.objectList.Clear();
			this.musicSettings = new GlobalSettings.AudioSettings.MusicVolumeSettings();
			this.add(this.musicSettings);
			this.soundSettings = new GlobalSettings.AudioSettings.SoundFXVolumeSettings();
			this.add(this.soundSettings);
		}

		// Token: 0x040007A9 RID: 1961
		private GlobalSettings.AudioSettings.MusicVolumeSettings musicSettings;

		// Token: 0x040007AA RID: 1962
		private GlobalSettings.AudioSettings.SoundFXVolumeSettings soundSettings;

		// Token: 0x02000314 RID: 788
		private class MusicVolumeSettings : GlobalSettings.SettingsCollection.Setting
		{
			// Token: 0x06001C4C RID: 7244 RVA: 0x0007A3BC File Offset: 0x000785BC
			public MusicVolumeSettings() : base("MusicVolumeSettings", "Music Volume", "The volume of the soundtrack.")
			{
				AudioControl.setMusicVolume(1f);
			}

			// Token: 0x06001C4D RID: 7245 RVA: 0x0007A3E0 File Offset: 0x000785E0
			public override GlobalSettings.SettingsCollection.SettingSaveData getSettingSaveData()
			{
				return new GlobalSettings.SettingsCollection.SettingSaveData(this.getId(), AudioControl.getBaseMusicVolume().ToString());
			}

			// Token: 0x06001C4E RID: 7246 RVA: 0x0007A408 File Offset: 0x00078608
			public override void applySettingSaveData(GlobalSettings.SettingsCollection.SettingSaveData data)
			{
				try
				{
					AudioControl.setMusicVolume(float.Parse(data.value, CultureInfo.InvariantCulture));
				}
				catch (Exception obj)
				{
					MainControl.logError(obj);
				}
			}

			// Token: 0x06001C4F RID: 7247 RVA: 0x0007A444 File Offset: 0x00078644
			public override void incrementState(int i)
			{
				AudioControl.adjustMusicVolume(i);
			}

			// Token: 0x06001C50 RID: 7248 RVA: 0x0007A44C File Offset: 0x0007864C
			public override string printState()
			{
				return AudioControl.printMusicVolume();
			}
		}

		// Token: 0x02000315 RID: 789
		private class SoundFXVolumeSettings : GlobalSettings.SettingsCollection.Setting
		{
			// Token: 0x06001C51 RID: 7249 RVA: 0x0007A453 File Offset: 0x00078653
			public SoundFXVolumeSettings() : base("SoundFXVolumeSettings", "Sound FX Volume", "The volume of the sound effects.")
			{
				AudioControl.setSoundVolume(1f);
			}

			// Token: 0x06001C52 RID: 7250 RVA: 0x0007A474 File Offset: 0x00078674
			public override GlobalSettings.SettingsCollection.SettingSaveData getSettingSaveData()
			{
				return new GlobalSettings.SettingsCollection.SettingSaveData(this.getId(), AudioControl.getBaseSoundVolume().ToString());
			}

			// Token: 0x06001C53 RID: 7251 RVA: 0x0007A49C File Offset: 0x0007869C
			public override void applySettingSaveData(GlobalSettings.SettingsCollection.SettingSaveData data)
			{
				try
				{
					AudioControl.setSoundVolume(float.Parse(data.value, CultureInfo.InvariantCulture));
				}
				catch (Exception obj)
				{
					MainControl.logError(obj);
				}
			}

			// Token: 0x06001C54 RID: 7252 RVA: 0x0007A4D8 File Offset: 0x000786D8
			public override void incrementState(int i)
			{
				AudioControl.adjustSoundVolume(i);
			}

			// Token: 0x06001C55 RID: 7253 RVA: 0x0007A4E0 File Offset: 0x000786E0
			public override string printState()
			{
				return AudioControl.printSoundVolume();
			}
		}
	}

	// Token: 0x020001F0 RID: 496
	[Serializable]
	public class FontSettings : GlobalSettings.SettingsCollection, ISerializable
	{
		// Token: 0x06001783 RID: 6019 RVA: 0x00068356 File Offset: 0x00066556
		public FontSettings() : base("Font Settings")
		{
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x00068363 File Offset: 0x00066563
		public FontSettings(SerializationInfo info, StreamingContext context)
		{
			base.load(info, context);
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x00068374 File Offset: 0x00066574
		public override void initialize()
		{
			this.objectList.Clear();
			this.smallDescriptionFont = new GlobalSettings.FontSettings.FontTypeSetting("smallDescriptionFont", "Description Font", "The font used for most descriptions.\n\n”This is what a quote looks like.”", new List<string>
			{
				"FON_TinyFont",
				"FON_TinyFontTall",
				"FON_TinyFontCapitalized",
				"FON_TinyFontTallCapitalized"
			}, 1);
			this.add(this.smallDescriptionFont);
			this.descriptionColor = new GlobalSettings.SettingsCollection.ColorSetting("descriptionColor", "Description Color", "The color of the main descriptive text.\n\n”This is what a quote looks like.”", C64Color.getFontColors(), 3);
			this.add(this.descriptionColor);
			this.quoteColor = new GlobalSettings.SettingsCollection.ColorSetting("quoteColor", "Quote Color", "The color of the main descriptive text that is displayed in quotation marks.\n\n”This is what a quote looks like.”", C64Color.getFontColors(), 1);
			this.add(this.quoteColor);
			this.descriptionShadowColor = new GlobalSettings.SettingsCollection.ColorSetting("descriptionShadowColor", "Description Shadow", "The color of the main descriptive text's drop-down shadow.\n\n”This is what a quote looks like.”", C64Color.getFontShadowColors(), 1);
			this.add(this.descriptionShadowColor);
			this.outlineShadow = new GlobalSettings.SettingsCollection.BoolSetting("outlineShadow", "Draw Outline", "Draws an outline around the letters instead of a drop-down shadow.\n\n”This is what a quote looks like.”", false);
			this.add(this.outlineShadow);
			this.textBackgroundColor = new GlobalSettings.SettingsCollection.CarouselSetting("textBackground", "Text Background Color", "The color of certain UI-elements behind text. Adjust this to increase the contrast between text and background.\n\n”This is what a quote looks like.”", new List<string>
			{
				"Classic",
				"Dark",
				"Black"
			}, 0);
			this.add(this.textBackgroundColor);
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x000684E4 File Offset: 0x000666E4
		public Font getSmallDescriptionFont()
		{
			if (this.smallDescriptionFont == null)
			{
				this.initialize();
			}
			Font font = this.smallDescriptionFont.getFont();
			if (font == null)
			{
				font = GameData.getFont("FON_TinyFont");
			}
			return font;
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x0006851A File Offset: 0x0006671A
		public Font getSmallTechnicalFont()
		{
			return GameData.getFont("FON_TinyFont");
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x00068526 File Offset: 0x00066726
		public bool getOutlineShadow()
		{
			return this.outlineShadow != null && this.outlineShadow.getState();
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x0006853D File Offset: 0x0006673D
		public int getTextBackgroundColor()
		{
			if (this.textBackgroundColor == null)
			{
				return 0;
			}
			return this.textBackgroundColor.getState();
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x00068554 File Offset: 0x00066754
		public GlobalSettings.SettingsCollection.Setting getTextBackgroundColorSetting()
		{
			return this.textBackgroundColor;
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x0006855C File Offset: 0x0006675C
		public GlobalSettings.SettingsCollection.Setting getDescriptiveFontSetting()
		{
			return this.smallDescriptionFont;
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x00068564 File Offset: 0x00066764
		public GlobalSettings.SettingsCollection.Setting getDescriptiveFontColorSetting()
		{
			return this.descriptionColor;
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x0006856C File Offset: 0x0006676C
		public string getSmallDescriptionFontColor()
		{
			if (this.descriptionColor == null)
			{
				this.initialize();
			}
			return this.descriptionColor.getColor();
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x00068587 File Offset: 0x00066787
		public string getSmallDescriptionQuoteFontColor()
		{
			if (this.quoteColor == null)
			{
				this.initialize();
			}
			return this.quoteColor.getColor();
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x000685A2 File Offset: 0x000667A2
		public string getSmallDescriptionFontShadowColor()
		{
			if (this.descriptionShadowColor == null)
			{
				this.initialize();
			}
			return this.descriptionShadowColor.getColor();
		}

		// Token: 0x040007AB RID: 1963
		private GlobalSettings.FontSettings.FontTypeSetting smallDescriptionFont;

		// Token: 0x040007AC RID: 1964
		private GlobalSettings.SettingsCollection.ColorSetting descriptionColor;

		// Token: 0x040007AD RID: 1965
		private GlobalSettings.SettingsCollection.ColorSetting quoteColor;

		// Token: 0x040007AE RID: 1966
		private GlobalSettings.SettingsCollection.ColorSetting descriptionShadowColor;

		// Token: 0x040007AF RID: 1967
		private GlobalSettings.SettingsCollection.CarouselSetting textBackgroundColor;

		// Token: 0x040007B0 RID: 1968
		private GlobalSettings.SettingsCollection.BoolSetting outlineShadow;

		// Token: 0x02000316 RID: 790
		protected class FontTypeSetting : GlobalSettings.SettingsCollection.CarouselSetting
		{
			// Token: 0x06001C56 RID: 7254 RVA: 0x0007A4E7 File Offset: 0x000786E7
			public FontTypeSetting(string id, string name, string description, List<string> alternatives, int startingState) : base(id, name, description, alternatives, startingState)
			{
			}

			// Token: 0x06001C57 RID: 7255 RVA: 0x0007A4F8 File Offset: 0x000786F8
			public Font getFont()
			{
				Font font = null;
				try
				{
					if (this.alternatives != null && this.getState() < this.alternatives.Count)
					{
						font = GameData.getFont(this.alternatives[this.getState()]);
					}
				}
				catch
				{
				}
				if (font == null)
				{
					GameData.getFont("FON_TinyFont");
				}
				return font;
			}

			// Token: 0x06001C58 RID: 7256 RVA: 0x0007A560 File Offset: 0x00078760
			public override string printState()
			{
				return "Font " + (this.getState() + 1).ToString();
			}
		}
	}

	// Token: 0x020001F1 RID: 497
	[Serializable]
	public class DisplaySettings : GlobalSettings.SettingsCollection, ISerializable
	{
		// Token: 0x06001790 RID: 6032 RVA: 0x000685BD File Offset: 0x000667BD
		public DisplaySettings() : base("Display Settings")
		{
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x000685CA File Offset: 0x000667CA
		public DisplaySettings(SerializationInfo info, StreamingContext context)
		{
			base.load(info, context);
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x000685DC File Offset: 0x000667DC
		public override void initialize()
		{
			this.objectList.Clear();
			this.CRT = new GlobalSettings.DisplaySettings.CRTSettings();
			this.add(this.CRT);
			this.ScreenMode = new GlobalSettings.DisplaySettings.ScreenModeSettings();
			this.add(this.ScreenMode);
			this.windowScale = new GlobalSettings.DisplaySettings.WindowScaleSetting();
			this.add(this.windowScale);
			this.VSync = new GlobalSettings.DisplaySettings.VSyncSettings();
			this.add(this.VSync);
			this.classicConsole = new GlobalSettings.SettingsCollection.BoolSetting("classicConsole", "Classic Console", "The console uses the classic C64 color theme.", true);
			this.add(this.classicConsole);
			this.fancyLighting = new GlobalSettings.SettingsCollection.BoolSetting("fancyLighting", "Fancy Lighting", "If enabled it will use the game’s blended lighting system. This is the intended experience.\n\nThis feature should only be disabled if you experience performance issues.", true);
			this.add(this.fancyLighting);
			this.weatherEffects = new GlobalSettings.SettingsCollection.BoolSetting("weatherEffects", "Weather Effects", "If enabled the game will render weather effects like fog and rain.\n\nDisabling this option may increase performance.", true);
			this.add(this.weatherEffects);
			this.allowScreenShaking = new GlobalSettings.SettingsCollection.BoolSetting("screenShake", "Screen Shaking", "Whether or not to allow the screen-shake visual effect.", true);
			this.add(this.allowScreenShaking);
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x000686F4 File Offset: 0x000668F4
		public bool getFancyLightModels()
		{
			return true;
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x000686F7 File Offset: 0x000668F7
		public bool getFancyLighting()
		{
			return this.fancyLighting.getState();
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x00068704 File Offset: 0x00066904
		public bool allowScreenshake()
		{
			return this.allowScreenShaking.getState();
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x00068711 File Offset: 0x00066911
		public bool getWeatherEffects()
		{
			return this.weatherEffects.getState();
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x0006871E File Offset: 0x0006691E
		public int getWindowScale()
		{
			return this.windowScale.getState();
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x0006872B File Offset: 0x0006692B
		public bool getClassicConsole()
		{
			return this.classicConsole == null || this.classicConsole.getState();
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x00068742 File Offset: 0x00066942
		public GlobalSettings.SettingsCollection.Setting getCRTSetting()
		{
			return this.CRT;
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x0006874A File Offset: 0x0006694A
		private string getBaseFramePath()
		{
			return "GUI10";
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x00068754 File Offset: 0x00066954
		public string getWindowFramePath()
		{
			if (GlobalSettings.getGamePlaySettings().isNostalgiaModeOn())
			{
				return "NostaligFrame";
			}
			string text = this.getBaseFramePath();
			if (GlobalSettings.getFontSettings().getTextBackgroundColor() == 2)
			{
				text += "Black";
			}
			else if (GlobalSettings.getFontSettings().getTextBackgroundColor() == 1)
			{
				text += "Brown";
			}
			return text;
		}

		// Token: 0x040007B1 RID: 1969
		private GlobalSettings.SettingsCollection.BoolSetting classicConsole;

		// Token: 0x040007B2 RID: 1970
		private GlobalSettings.DisplaySettings.CRTSettings CRT;

		// Token: 0x040007B3 RID: 1971
		private GlobalSettings.DisplaySettings.VSyncSettings VSync;

		// Token: 0x040007B4 RID: 1972
		private GlobalSettings.DisplaySettings.ScreenModeSettings ScreenMode;

		// Token: 0x040007B5 RID: 1973
		private GlobalSettings.DisplaySettings.WindowScaleSetting windowScale;

		// Token: 0x040007B6 RID: 1974
		private GlobalSettings.SettingsCollection.BoolSetting fancyLighting;

		// Token: 0x040007B7 RID: 1975
		private GlobalSettings.SettingsCollection.BoolSetting weatherEffects;

		// Token: 0x040007B8 RID: 1976
		private GlobalSettings.SettingsCollection.BoolSetting allowScreenShaking;

		// Token: 0x02000317 RID: 791
		protected class WindowScaleSetting : GlobalSettings.SettingsCollection.NumericCarouselSetting
		{
			// Token: 0x06001C59 RID: 7257 RVA: 0x0007A587 File Offset: 0x00078787
			public WindowScaleSetting() : base("windowScale", "Windowed Mode Scale", "The scale of the window when run in Window mode.", 4, 1, 3)
			{
			}

			// Token: 0x06001C5A RID: 7258 RVA: 0x0007A5A1 File Offset: 0x000787A1
			public override void incrementState(int i)
			{
				base.incrementState(i);
				ScreenControl.enforceResolution();
			}
		}

		// Token: 0x02000318 RID: 792
		private class CRTSettings : GlobalSettings.SettingsCollection.CarouselSetting
		{
			// Token: 0x06001C5B RID: 7259 RVA: 0x0007A5AF File Offset: 0x000787AF
			public CRTSettings() : base("CRTSetting", "CRT Filter", "Activating this filter gives the game a fuzzy retro look.", GlobalSettings.DisplaySettings.CRTSettings.getCRTFilterList(), 0)
			{
				this.updateCRTState();
			}

			// Token: 0x06001C5C RID: 7260 RVA: 0x0007A5D4 File Offset: 0x000787D4
			private static List<string> getCRTFilterList()
			{
				List<string> list = new List<string>();
				string text = GlobalSettings.DisplaySettings.CRTSettings.noneID;
				list.Add(text);
				text = "Soft";
				list.Add(text);
				if (!GlobalSettings.DisplaySettings.CRTSettings.IDtoCRTPresetList.ContainsKey(text))
				{
					GlobalSettings.DisplaySettings.CRTSettings.IDtoCRTPresetList.Add(text, BaseCRTEffect.Preset.Soft);
				}
				text = "Classic";
				list.Add(text);
				if (!GlobalSettings.DisplaySettings.CRTSettings.IDtoCRTPresetList.ContainsKey(text))
				{
					GlobalSettings.DisplaySettings.CRTSettings.IDtoCRTPresetList.Add(text, BaseCRTEffect.Preset.Default);
				}
				text = "High-End";
				list.Add(text);
				if (!GlobalSettings.DisplaySettings.CRTSettings.IDtoCRTPresetList.ContainsKey(text))
				{
					GlobalSettings.DisplaySettings.CRTSettings.IDtoCRTPresetList.Add(text, BaseCRTEffect.Preset.HighEndMonitor);
				}
				text = "Arcade";
				list.Add(text);
				if (!GlobalSettings.DisplaySettings.CRTSettings.IDtoCRTPresetList.ContainsKey(text))
				{
					GlobalSettings.DisplaySettings.CRTSettings.IDtoCRTPresetList.Add(text, BaseCRTEffect.Preset.ArcadeDisplay);
				}
				text = "Kitchen TV";
				list.Add(text);
				if (!GlobalSettings.DisplaySettings.CRTSettings.IDtoCRTPresetList.ContainsKey(text))
				{
					GlobalSettings.DisplaySettings.CRTSettings.IDtoCRTPresetList.Add(text, BaseCRTEffect.Preset.KitchenTV);
				}
				text = "Mini CRT";
				list.Add(text);
				if (!GlobalSettings.DisplaySettings.CRTSettings.IDtoCRTPresetList.ContainsKey(text))
				{
					GlobalSettings.DisplaySettings.CRTSettings.IDtoCRTPresetList.Add(text, BaseCRTEffect.Preset.MiniCRT);
				}
				text = "Old TV";
				list.Add(text);
				if (!GlobalSettings.DisplaySettings.CRTSettings.IDtoCRTPresetList.ContainsKey(text))
				{
					GlobalSettings.DisplaySettings.CRTSettings.IDtoCRTPresetList.Add(text, BaseCRTEffect.Preset.OldTV);
				}
				text = "Broken B/W";
				list.Add(text);
				if (!GlobalSettings.DisplaySettings.CRTSettings.IDtoCRTPresetList.ContainsKey(text))
				{
					GlobalSettings.DisplaySettings.CRTSettings.IDtoCRTPresetList.Add(text, BaseCRTEffect.Preset.BrokenBlackAndWhite);
				}
				return list;
			}

			// Token: 0x06001C5D RID: 7261 RVA: 0x0007A725 File Offset: 0x00078925
			public override void incrementState(int i)
			{
				base.incrementState(i);
				this.updateCRTState();
			}

			// Token: 0x06001C5E RID: 7262 RVA: 0x0007A734 File Offset: 0x00078934
			private void updateCRTState()
			{
				BaseCRTEffect crtcontrol = GlobalSettings.DisplaySettings.CRTSettings.getCRTControl();
				if (crtcontrol == null)
				{
					MainControl.logError("No CRT Detected");
					return;
				}
				string text = this.alternatives[this.getState()];
				if (!(text == GlobalSettings.DisplaySettings.CRTSettings.noneID))
				{
					crtcontrol.enabled = true;
					if (GlobalSettings.DisplaySettings.CRTSettings.IDtoCRTPresetList.ContainsKey(text))
					{
						crtcontrol.predefinedModel = GlobalSettings.DisplaySettings.CRTSettings.IDtoCRTPresetList[text];
					}
					return;
				}
				if (MainControl.runningOnSteamDeck())
				{
					crtcontrol.enabled = true;
					crtcontrol.setPresetToStretch();
					return;
				}
				crtcontrol.enabled = false;
			}

			// Token: 0x06001C5F RID: 7263 RVA: 0x0007A7BC File Offset: 0x000789BC
			private static BaseCRTEffect getCRTControl()
			{
				Component[] components = Camera.main.GetComponents(typeof(BaseCRTEffect));
				if (components.Length == 0)
				{
					return null;
				}
				return components[0] as BaseCRTEffect;
			}

			// Token: 0x06001C60 RID: 7264 RVA: 0x0007A7EC File Offset: 0x000789EC
			public override void applySettingSaveData(GlobalSettings.SettingsCollection.SettingSaveData data)
			{
				base.applySettingSaveData(data);
				this.updateCRTState();
			}

			// Token: 0x04000AA5 RID: 2725
			private static Dictionary<string, BaseCRTEffect.Preset> IDtoCRTPresetList = new Dictionary<string, BaseCRTEffect.Preset>();

			// Token: 0x04000AA6 RID: 2726
			private static string noneID = "None";
		}

		// Token: 0x02000319 RID: 793
		private class VSyncSettings : GlobalSettings.SettingsCollection.Setting
		{
			// Token: 0x06001C62 RID: 7266 RVA: 0x0007A811 File Offset: 0x00078A11
			public VSyncSettings() : base("VsyncSettings", "VSync", "VSync prevents artifacts such as screen tearing.")
			{
				ScreenControl.vsyncOn();
			}

			// Token: 0x06001C63 RID: 7267 RVA: 0x0007A82D File Offset: 0x00078A2D
			public override void incrementState(int i = 0)
			{
				ScreenControl.toggleVsync();
			}

			// Token: 0x06001C64 RID: 7268 RVA: 0x0007A834 File Offset: 0x00078A34
			public override string printState()
			{
				return ScreenControl.printVsyncOnOff();
			}

			// Token: 0x06001C65 RID: 7269 RVA: 0x0007A83C File Offset: 0x00078A3C
			public override GlobalSettings.SettingsCollection.SettingSaveData getSettingSaveData()
			{
				return new GlobalSettings.SettingsCollection.SettingSaveData(this.getId(), ScreenControl.isVsyncOn().ToString());
			}

			// Token: 0x06001C66 RID: 7270 RVA: 0x0007A864 File Offset: 0x00078A64
			public override void applySettingSaveData(GlobalSettings.SettingsCollection.SettingSaveData data)
			{
				if (data == null)
				{
					MainControl.logError("Corrupt or NULL save data container for setting: " + this.getId());
					return;
				}
				if (data.value == false.ToString())
				{
					ScreenControl.vsyncOff();
					return;
				}
				if (data.value == true.ToString())
				{
					ScreenControl.vsyncOn();
					return;
				}
				MainControl.logError("Corrupt save data value: " + data.value + " for setting: " + this.getId());
			}
		}

		// Token: 0x0200031A RID: 794
		private class ScreenModeSettings : GlobalSettings.SettingsCollection.Setting
		{
			// Token: 0x06001C67 RID: 7271 RVA: 0x0007A8E2 File Offset: 0x00078AE2
			public ScreenModeSettings() : base(" ScreenModeSettings", "Screen Mode", "Allows the game to be played in full screen or windowed mode.")
			{
				ScreenControl.setFullScreen();
			}

			// Token: 0x06001C68 RID: 7272 RVA: 0x0007A8FE File Offset: 0x00078AFE
			public override void incrementState(int i = 0)
			{
				if (MainControl.runningOnSteamDeck())
				{
					ScreenControl.setFullScreen();
					return;
				}
				ScreenControl.swapScreenMode();
			}

			// Token: 0x06001C69 RID: 7273 RVA: 0x0007A912 File Offset: 0x00078B12
			public override string printState()
			{
				if (Screen.fullScreen)
				{
					return "Full";
				}
				return "Windowed";
			}

			// Token: 0x06001C6A RID: 7274 RVA: 0x0007A928 File Offset: 0x00078B28
			public override GlobalSettings.SettingsCollection.SettingSaveData getSettingSaveData()
			{
				return new GlobalSettings.SettingsCollection.SettingSaveData(this.getId(), Screen.fullScreen.ToString());
			}

			// Token: 0x06001C6B RID: 7275 RVA: 0x0007A950 File Offset: 0x00078B50
			public override void applySettingSaveData(GlobalSettings.SettingsCollection.SettingSaveData data)
			{
				if (MainControl.runningOnSteamDeck())
				{
					ScreenControl.setFullScreen();
					return;
				}
				if (data == null)
				{
					MainControl.logError("Corrupt or NULL save data container for setting: " + this.getId());
					return;
				}
				if (data.value == false.ToString())
				{
					ScreenControl.setWindowed();
					return;
				}
				if (data.value == true.ToString())
				{
					ScreenControl.setFullScreen();
					return;
				}
				MainControl.logError("Corrupt save data value: " + data.value + " for setting: " + this.getId());
			}
		}
	}

	// Token: 0x020001F2 RID: 498
	[Serializable]
	public class GamePlaySettings : GlobalSettings.SettingsCollection, ISerializable
	{
		// Token: 0x0600179C RID: 6044 RVA: 0x000687AF File Offset: 0x000669AF
		public GamePlaySettings() : base("Gameplay Settings")
		{
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x000687BC File Offset: 0x000669BC
		public GamePlaySettings(SerializationInfo info, StreamingContext context)
		{
			base.load(info, context);
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x000687CC File Offset: 0x000669CC
		public override void initialize()
		{
			this.objectList.Clear();
			this.autoSaveOnEnterMap = new GlobalSettings.SettingsCollection.BoolSetting("autoSaveOnEnterMap", "Enter Map Auto Save", "The game will auto save whenever you enter a new map.", true);
			this.add(this.autoSaveOnEnterMap);
			this.autoSaveOnRest = new GlobalSettings.SettingsCollection.BoolSetting("autoSaveOnRest", "Rest Auto Save", "The game will auto save whenever you make camp.", true);
			this.add(this.autoSaveOnRest);
			this.tacticalIcons = new GlobalSettings.SettingsCollection.BoolSetting("tacticalInfo", "Tactical Icons Always On", "The degree to which the game shows tactical symbols and a grid during combat.\n\nIf set to true, the game will automatically show tactical icons during the planning state.\n\nIf set to false, the tactical icons will only show when holding down SHIFT.", false);
			this.add(this.tacticalIcons);
			this.tacticalGrid = new GlobalSettings.SettingsCollection.BoolSetting("tacticalGrid", "Tactical Grid", "Whether or not the game shows a grid along with the tactical icons when activated during combat.", true);
			this.add(this.tacticalGrid);
			this.stealthInfo = new GlobalSettings.SettingsCollection.BoolSetting("stealthInfo", "Show Stealth Info", "Setting this to Enabled will show a tooltip box containing info on light levels and stealth modifiers for tiles when you right click them.", false);
			this.add(this.stealthInfo);
			this.combatPopUpLifeLength = new GlobalSettings.SettingsCollection.CarouselSetting("combatPopUp", "Combat Pop-Up Length", "This adjusts how long the on-screen combat pop-up text lingers.\n\nNote that a turn does not progress until all the character’s pop-up text is gone, so adjusting this number will also adjust how quickly combat turns pass.", new List<string>
			{
				"Fast",
				"Normal",
				"Slow",
				"Very Slow"
			}, 1);
			this.add(this.combatPopUpLifeLength);
			this.tutorials = new GlobalSettings.SettingsCollection.CarouselSetting("tutorials", "Tutorials", "The degree to which you want the game to offer gameplay tutorials during play.", new List<string>
			{
				"None",
				"Some",
				"Extensive"
			}, 2);
			this.add(this.tutorials);
			this.controllerSensitivity = new GlobalSettings.SettingsCollection.NumericCarouselSetting("controllerSensitivity", "Controller Sensitivity", "The sensitivity of the joystick used for moving the cursor when using a controller.", 4, 1, 2);
			this.add(this.controllerSensitivity);
			this.nostalgiaMode = new GlobalSettings.SettingsCollection.BoolSetting("nostalgiaMode", "DELUXE: Nostalgia Mode", "Toggles the special ‘nostalgic UI’ that may remind you of classic RPG series we all know and love.", false);
			this.nostalgiaMode.setDeluxeOnly(true);
			this.add(this.nostalgiaMode);
			this.diceAnimation = new GlobalSettings.SettingsCollection.CarouselSetting("diceAnimation", "DELUXE: Dice Animation", "The skin of the dice being rolled during skill-tests.", new List<string>
			{
				"White - Pips",
				"White - Roman",
				"Blue - Pips",
				"Blue - Roman",
				"Red - Pips",
				"Red - Roman"
			}, 0);
			this.diceAnimation.setDeluxeOnly(true);
			this.add(this.diceAnimation);
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x00068A2C File Offset: 0x00066C2C
		public bool getAutoSaveOnEnterMap()
		{
			return this.autoSaveOnEnterMap.getState();
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x00068A39 File Offset: 0x00066C39
		public bool getAutoSaveOnRest()
		{
			return this.autoSaveOnRest.getState();
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x00068A46 File Offset: 0x00066C46
		public bool showTacticalSymbols()
		{
			return this.tacticalIcons.getState();
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x00068A53 File Offset: 0x00066C53
		public float getControllerSensitivity()
		{
			if (this.controllerSensitivity == null)
			{
				return 3f;
			}
			return (float)(this.controllerSensitivity.getState() + 1);
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x00068A71 File Offset: 0x00066C71
		public bool showStealthInfo()
		{
			return this.stealthInfo.getState();
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x00068A7E File Offset: 0x00066C7E
		public bool showTacticalGrid()
		{
			return this.tacticalGrid.getState();
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x00068A8C File Offset: 0x00066C8C
		public string getDicePath()
		{
			string result = "Images/GUIIcons/DiceD6WhitePips";
			if (!MainControl.isDeluxeEdition())
			{
				return result;
			}
			if (this.diceAnimation.getState() == 0)
			{
				return result;
			}
			if (this.diceAnimation.getState() == 1)
			{
				return "Images/GUIIcons/DiceD6WhiteRoman";
			}
			if (this.diceAnimation.getState() == 2)
			{
				return "Images/GUIIcons/DiceD6BluePips";
			}
			if (this.diceAnimation.getState() == 3)
			{
				return "Images/GUIIcons/DiceD6BlueRoman";
			}
			if (this.diceAnimation.getState() == 4)
			{
				return "Images/GUIIcons/DiceD6RedPips";
			}
			if (this.diceAnimation.getState() == 5)
			{
				return "Images/GUIIcons/DiceD6RedRoman";
			}
			return result;
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x00068B1C File Offset: 0x00066D1C
		public bool isNostalgiaModeOn()
		{
			if (!MainControl.isDeluxeEdition())
			{
				this.nostalgiaMode.forceBoolState(false);
				return false;
			}
			return this.nostalgiaMode.getState();
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x00068B3E File Offset: 0x00066D3E
		public int getTutorialDegree()
		{
			return this.tutorials.getState();
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x00068B4C File Offset: 0x00066D4C
		public float getPopUpLifeModifer()
		{
			if (this.combatPopUpLifeLength.getState() == 0)
			{
				return 0.5f;
			}
			if (this.combatPopUpLifeLength.getState() == 1)
			{
				return 1f;
			}
			if (this.combatPopUpLifeLength.getState() == 2)
			{
				return 1.5f;
			}
			if (this.combatPopUpLifeLength.getState() == 3)
			{
				return 2f;
			}
			return 1f;
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x00068BAD File Offset: 0x00066DAD
		public void setToNoTutorials()
		{
			this.tutorials.setStateTo(0);
		}

		// Token: 0x040007B9 RID: 1977
		private GlobalSettings.SettingsCollection.BoolSetting autoSaveOnEnterMap;

		// Token: 0x040007BA RID: 1978
		private GlobalSettings.SettingsCollection.BoolSetting autoSaveOnRest;

		// Token: 0x040007BB RID: 1979
		private GlobalSettings.SettingsCollection.CarouselSetting tutorials;

		// Token: 0x040007BC RID: 1980
		private GlobalSettings.SettingsCollection.BoolSetting tacticalIcons;

		// Token: 0x040007BD RID: 1981
		private GlobalSettings.SettingsCollection.BoolSetting tacticalGrid;

		// Token: 0x040007BE RID: 1982
		private GlobalSettings.SettingsCollection.BoolSetting stealthInfo;

		// Token: 0x040007BF RID: 1983
		private GlobalSettings.SettingsCollection.CarouselSetting combatPopUpLifeLength;

		// Token: 0x040007C0 RID: 1984
		private GlobalSettings.SettingsCollection.BoolSetting nostalgiaMode;

		// Token: 0x040007C1 RID: 1985
		private GlobalSettings.SettingsCollection.CarouselSetting diceAnimation;

		// Token: 0x040007C2 RID: 1986
		private GlobalSettings.SettingsCollection.NumericCarouselSetting controllerSensitivity;
	}

	// Token: 0x020001F3 RID: 499
	[Serializable]
	public abstract class SettingsCollection : SkaldObjectList, ISerializable
	{
		// Token: 0x060017AA RID: 6058 RVA: 0x00068BBB File Offset: 0x00066DBB
		public SettingsCollection(string name) : base(name)
		{
			base.deactivateSorting();
			this.initialize();
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x00068BD0 File Offset: 0x00066DD0
		public SettingsCollection()
		{
			base.deactivateSorting();
			this.initialize();
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x00068BE4 File Offset: 0x00066DE4
		public SettingsCollection(SerializationInfo info, StreamingContext context)
		{
			this.load(info, context);
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x00068BF4 File Offset: 0x00066DF4
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			this.save(info, context);
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x00068C00 File Offset: 0x00066E00
		protected void load(SerializationInfo info, StreamingContext context)
		{
			base.deactivateSorting();
			this.initialize();
			this.setName((string)info.GetValue("name", typeof(string)));
			try
			{
				foreach (GlobalSettings.SettingsCollection.SettingSaveData settingSaveData in ((List<GlobalSettings.SettingsCollection.SettingSaveData>)info.GetValue("saveData", typeof(List<GlobalSettings.SettingsCollection.SettingSaveData>))))
				{
					GlobalSettings.SettingsCollection.Setting setting = base.getObject(settingSaveData.id) as GlobalSettings.SettingsCollection.Setting;
					if (setting != null)
					{
						setting.applySettingSaveData(settingSaveData);
					}
				}
			}
			catch (Exception obj)
			{
				MainControl.logError(obj);
			}
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x00068CC0 File Offset: 0x00066EC0
		protected void save(SerializationInfo info, StreamingContext context)
		{
			List<GlobalSettings.SettingsCollection.SettingSaveData> list = new List<GlobalSettings.SettingsCollection.SettingSaveData>();
			foreach (SkaldBaseObject skaldBaseObject in this.objectList)
			{
				GlobalSettings.SettingsCollection.Setting setting = (GlobalSettings.SettingsCollection.Setting)skaldBaseObject;
				list.Add(setting.getSettingSaveData());
			}
			info.AddValue("saveData", list, typeof(List<GlobalSettings.SettingsCollection.SettingSaveData>));
			info.AddValue("name", this.getName(), typeof(string));
		}

		// Token: 0x060017B0 RID: 6064
		public abstract void initialize();

		// Token: 0x060017B1 RID: 6065 RVA: 0x00068D54 File Offset: 0x00066F54
		public UITextSliderControl createAndPopulateSliderControls(UITextSliderControl result)
		{
			if (result == null)
			{
				result = new UITextSliderControl();
			}
			else
			{
				result.clearElements();
			}
			try
			{
				int num = base.getScrollIndex();
				while (num < base.getIndexAtEndOfPage() && num < base.getCount())
				{
					GlobalSettings.SettingsCollection.Setting setting = this.objectList[num] as GlobalSettings.SettingsCollection.Setting;
					if (MainControl.isDeluxeEdition() || (!MainControl.isDeluxeEdition() && !setting.isDeluxeOnly()))
					{
						result.createAndReturnSliderButton(setting);
					}
					num++;
				}
			}
			catch
			{
			}
			return result;
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x00068DDC File Offset: 0x00066FDC
		public void reset()
		{
			this.initialize();
		}

		// Token: 0x0200031B RID: 795
		[Serializable]
		public class SettingSaveData
		{
			// Token: 0x06001C6C RID: 7276 RVA: 0x0007A9DB File Offset: 0x00078BDB
			public SettingSaveData(string id, string value)
			{
				this.id = id;
				this.value = value;
			}

			// Token: 0x04000AA7 RID: 2727
			public string id = "";

			// Token: 0x04000AA8 RID: 2728
			public string value = "";
		}

		// Token: 0x0200031C RID: 796
		public abstract class Setting : SkaldBaseObject
		{
			// Token: 0x06001C6D RID: 7277 RVA: 0x0007AA07 File Offset: 0x00078C07
			protected Setting(string id, string name, string description) : base(id, name, description)
			{
			}

			// Token: 0x06001C6E RID: 7278 RVA: 0x0007AA19 File Offset: 0x00078C19
			protected Setting()
			{
			}

			// Token: 0x06001C6F RID: 7279
			public abstract string printState();

			// Token: 0x06001C70 RID: 7280
			public abstract void incrementState(int index);

			// Token: 0x06001C71 RID: 7281
			public abstract GlobalSettings.SettingsCollection.SettingSaveData getSettingSaveData();

			// Token: 0x06001C72 RID: 7282
			public abstract void applySettingSaveData(GlobalSettings.SettingsCollection.SettingSaveData data);

			// Token: 0x06001C73 RID: 7283 RVA: 0x0007AA28 File Offset: 0x00078C28
			public bool isDeluxeOnly()
			{
				return this.deluxeOnly;
			}

			// Token: 0x06001C74 RID: 7284 RVA: 0x0007AA30 File Offset: 0x00078C30
			public void setDeluxeOnly(bool value)
			{
				this.deluxeOnly = value;
			}

			// Token: 0x06001C75 RID: 7285 RVA: 0x0007AA39 File Offset: 0x00078C39
			public virtual void setState()
			{
				this.incrementState(1);
			}

			// Token: 0x06001C76 RID: 7286 RVA: 0x0007AA42 File Offset: 0x00078C42
			public override string getListName()
			{
				return string.Concat(new string[]
				{
					base.getName(),
					":\t",
					C64Color.WHITE_TAG,
					this.printState(),
					"</color>"
				});
			}

			// Token: 0x04000AA9 RID: 2729
			protected bool canBeSet = true;

			// Token: 0x04000AAA RID: 2730
			protected bool deluxeOnly;
		}

		// Token: 0x0200031D RID: 797
		protected class BoolSetting : GlobalSettings.SettingsCollection.Setting
		{
			// Token: 0x06001C77 RID: 7287 RVA: 0x0007AA79 File Offset: 0x00078C79
			public BoolSetting(string id, string name, string description, bool state) : base(id, name, description)
			{
				this.state = state;
			}

			// Token: 0x06001C78 RID: 7288 RVA: 0x0007AA8C File Offset: 0x00078C8C
			public override void incrementState(int i)
			{
				if (this.canBeSet)
				{
					this.state = !this.state;
				}
			}

			// Token: 0x06001C79 RID: 7289 RVA: 0x0007AAA5 File Offset: 0x00078CA5
			public void forceBoolState(bool value)
			{
				this.state = value;
			}

			// Token: 0x06001C7A RID: 7290 RVA: 0x0007AAAE File Offset: 0x00078CAE
			public virtual bool getState()
			{
				return this.state;
			}

			// Token: 0x06001C7B RID: 7291 RVA: 0x0007AAB6 File Offset: 0x00078CB6
			public override string printState()
			{
				if (this.getState())
				{
					return "Enabled";
				}
				return "Disabled";
			}

			// Token: 0x06001C7C RID: 7292 RVA: 0x0007AACC File Offset: 0x00078CCC
			public override GlobalSettings.SettingsCollection.SettingSaveData getSettingSaveData()
			{
				return new GlobalSettings.SettingsCollection.SettingSaveData(this.getId(), this.getState().ToString());
			}

			// Token: 0x06001C7D RID: 7293 RVA: 0x0007AAF4 File Offset: 0x00078CF4
			public override void applySettingSaveData(GlobalSettings.SettingsCollection.SettingSaveData data)
			{
				if (data == null)
				{
					MainControl.logError("Corrupt or NULL save data container for setting: " + this.getId());
					return;
				}
				if (data.value == false.ToString())
				{
					this.state = false;
					return;
				}
				if (data.value == true.ToString())
				{
					this.state = true;
					return;
				}
				MainControl.logError("Corrupt save data value: " + data.value + " for setting: " + this.getId());
			}

			// Token: 0x04000AAB RID: 2731
			private bool state;
		}

		// Token: 0x0200031E RID: 798
		protected class CarouselSetting : GlobalSettings.SettingsCollection.Setting
		{
			// Token: 0x06001C7E RID: 7294 RVA: 0x0007AB76 File Offset: 0x00078D76
			public CarouselSetting(string id, string name, string description, List<string> alternatives, int startingState) : base(id, name, description)
			{
				this.alternatives = alternatives;
				this.state = startingState;
				this.boundState();
			}

			// Token: 0x06001C7F RID: 7295 RVA: 0x0007AB97 File Offset: 0x00078D97
			public void setStateTo(int value)
			{
				this.state = value;
				this.boundState();
			}

			// Token: 0x06001C80 RID: 7296 RVA: 0x0007ABA6 File Offset: 0x00078DA6
			public override void incrementState(int i)
			{
				if (!this.canBeSet)
				{
					return;
				}
				this.state += i;
				this.boundState();
			}

			// Token: 0x06001C81 RID: 7297 RVA: 0x0007ABC5 File Offset: 0x00078DC5
			protected void boundState()
			{
				if (this.state >= this.alternatives.Count)
				{
					this.state = 0;
					return;
				}
				if (this.state < 0)
				{
					this.state = this.alternatives.Count - 1;
				}
			}

			// Token: 0x06001C82 RID: 7298 RVA: 0x0007ABFE File Offset: 0x00078DFE
			public virtual int getState()
			{
				return this.state;
			}

			// Token: 0x06001C83 RID: 7299 RVA: 0x0007AC06 File Offset: 0x00078E06
			public override string printState()
			{
				if (this.alternatives == null || this.getState() >= this.alternatives.Count)
				{
					return "";
				}
				return this.alternatives[this.getState()];
			}

			// Token: 0x06001C84 RID: 7300 RVA: 0x0007AC3C File Offset: 0x00078E3C
			public override GlobalSettings.SettingsCollection.SettingSaveData getSettingSaveData()
			{
				return new GlobalSettings.SettingsCollection.SettingSaveData(this.getId(), this.getState().ToString());
			}

			// Token: 0x06001C85 RID: 7301 RVA: 0x0007AC64 File Offset: 0x00078E64
			public override void applySettingSaveData(GlobalSettings.SettingsCollection.SettingSaveData data)
			{
				if (data == null)
				{
					MainControl.logError("Corrupt or NULL save data container for setting: " + this.getId());
					return;
				}
				try
				{
					int num = 0;
					if (int.TryParse(data.value, out num))
					{
						this.state = num;
					}
					this.boundState();
				}
				catch (Exception obj)
				{
					MainControl.logError(obj);
				}
			}

			// Token: 0x04000AAC RID: 2732
			protected int state;

			// Token: 0x04000AAD RID: 2733
			protected List<string> alternatives;
		}

		// Token: 0x0200031F RID: 799
		protected class NumericCarouselSetting : GlobalSettings.SettingsCollection.Setting
		{
			// Token: 0x06001C86 RID: 7302 RVA: 0x0007ACC4 File Offset: 0x00078EC4
			public NumericCarouselSetting(string id, string name, string description, int max, int min, int startingState) : base(id, name, description)
			{
				this.state = startingState;
				this.max = max;
				this.min = min;
				this.boundState();
			}

			// Token: 0x06001C87 RID: 7303 RVA: 0x0007ACED File Offset: 0x00078EED
			public override void incrementState(int i)
			{
				if (!this.canBeSet)
				{
					return;
				}
				this.state += i;
				this.boundState();
			}

			// Token: 0x06001C88 RID: 7304 RVA: 0x0007AD0C File Offset: 0x00078F0C
			private void boundState()
			{
				if (this.state > this.max)
				{
					this.state = this.min;
					return;
				}
				if (this.state < this.min)
				{
					this.state = this.max;
				}
			}

			// Token: 0x06001C89 RID: 7305 RVA: 0x0007AD43 File Offset: 0x00078F43
			public virtual int getState()
			{
				return this.state;
			}

			// Token: 0x06001C8A RID: 7306 RVA: 0x0007AD4C File Offset: 0x00078F4C
			public override string printState()
			{
				return this.getState().ToString();
			}

			// Token: 0x06001C8B RID: 7307 RVA: 0x0007AD68 File Offset: 0x00078F68
			public override GlobalSettings.SettingsCollection.SettingSaveData getSettingSaveData()
			{
				return new GlobalSettings.SettingsCollection.SettingSaveData(this.getId(), this.getState().ToString());
			}

			// Token: 0x06001C8C RID: 7308 RVA: 0x0007AD90 File Offset: 0x00078F90
			public override void applySettingSaveData(GlobalSettings.SettingsCollection.SettingSaveData data)
			{
				if (data == null)
				{
					MainControl.logError("Corrupt or NULL save data container for setting: " + this.getId());
					return;
				}
				try
				{
					int num = 0;
					if (int.TryParse(data.value, out num))
					{
						this.state = num;
					}
					this.boundState();
				}
				catch (Exception obj)
				{
					MainControl.logError(obj);
				}
			}

			// Token: 0x04000AAE RID: 2734
			private int state;

			// Token: 0x04000AAF RID: 2735
			private int max;

			// Token: 0x04000AB0 RID: 2736
			private int min;
		}

		// Token: 0x02000320 RID: 800
		protected class ColorSetting : GlobalSettings.SettingsCollection.CarouselSetting
		{
			// Token: 0x06001C8D RID: 7309 RVA: 0x0007ADF0 File Offset: 0x00078FF0
			public ColorSetting(string id, string name, string description, List<string> alternatives, int startingState) : base(id, name, description, alternatives, startingState)
			{
			}

			// Token: 0x06001C8E RID: 7310 RVA: 0x0007ADFF File Offset: 0x00078FFF
			public string getColor()
			{
				if (this.alternatives == null || this.getState() >= this.alternatives.Count)
				{
					return "";
				}
				return this.alternatives[this.getState()];
			}

			// Token: 0x06001C8F RID: 7311 RVA: 0x0007AE34 File Offset: 0x00079034
			public override string printState()
			{
				return "Color " + (this.getState() + 1).ToString();
			}
		}
	}
}
