using System;
using SkaldEnums;
using Steamworks;
using UnityEngine;

// Token: 0x0200003E RID: 62
public class MainControl : MonoBehaviour
{
	// Token: 0x060007E5 RID: 2021 RVA: 0x00027855 File Offset: 0x00025A55
	private void Start()
	{
		this.initSteps = MainControl.InitializationSteps.Begin;
		this.runInitialization();
	}

	// Token: 0x060007E6 RID: 2022 RVA: 0x00027864 File Offset: 0x00025A64
	private void runInitialization()
	{
		switch (this.initSteps)
		{
		case MainControl.InitializationSteps.Begin:
			MainControl.log("Initializing Application!");
			MainControl.credits = this.creditsPublic;
			Cursor.visible = false;
			ScreenControl.enforceResolution();
			ScreenControl.setFullScreen();
			this.initSteps++;
			return;
		case MainControl.InitializationSteps.WaitForPlatformSDKInit:
			if (SteamManager.Initialized)
			{
				this.initSteps++;
				return;
			}
			break;
		case MainControl.InitializationSteps.Finish:
			this.setDlcValues();
			MainControl.gameControl = new MainControl.StateControl();
			MainControl.log("Application Ready!");
			this.isReady = true;
			this.initSteps++;
			break;
		case MainControl.InitializationSteps.Done:
			break;
		default:
			return;
		}
	}

	// Token: 0x060007E7 RID: 2023 RVA: 0x00027908 File Offset: 0x00025B08
	private void setDlcValues()
	{
		MainControl.IS_DELUXE_EDITION = SteamApps.BIsDlcInstalled(new AppId_t(MainControl.DELUXE_EDITION_DLC_STEAM_ID));
	}

	// Token: 0x060007E8 RID: 2024 RVA: 0x0002791E File Offset: 0x00025B1E
	public static bool isDeluxeEdition()
	{
		return MainControl.IS_DELUXE_EDITION;
	}

	// Token: 0x060007E9 RID: 2025 RVA: 0x00027925 File Offset: 0x00025B25
	public static void gotoDemoEnd()
	{
		MainControl.END_DEMO = true;
	}

	// Token: 0x060007EA RID: 2026 RVA: 0x0002792D File Offset: 0x00025B2D
	public static void log(int message)
	{
		MainControl.log(message.ToString());
	}

	// Token: 0x060007EB RID: 2027 RVA: 0x0002793B File Offset: 0x00025B3B
	public static void winGame(string cutsceneId)
	{
		MainControl.gameControl.winGame(cutsceneId);
	}

	// Token: 0x060007EC RID: 2028 RVA: 0x00027948 File Offset: 0x00025B48
	public static void log(string message)
	{
		if (MainControl.logMessages)
		{
			Debug.Log("LOG: " + message);
		}
	}

	// Token: 0x060007ED RID: 2029 RVA: 0x00027961 File Offset: 0x00025B61
	public static bool allowAudio()
	{
		return MainControl.gameControl != null && MainControl.gameControl.allowAudio();
	}

	// Token: 0x060007EE RID: 2030 RVA: 0x00027978 File Offset: 0x00025B78
	public static void logError(string message)
	{
		if (MainControl.logErrorMessages)
		{
			Debug.LogError("ERROR: " + message + "\n\n");
			if (MainControl.getDataControl() != null && MainControl.getDataControl().printAllActiveComponentsId() != "")
			{
				Debug.LogError("ERROR DATA: \n" + MainControl.getDataControl().printAllActiveComponentsId() + "\n\n");
			}
		}
	}

	// Token: 0x060007EF RID: 2031 RVA: 0x000279DC File Offset: 0x00025BDC
	public static bool runningOnSteamDeck()
	{
		return SteamUtils.IsSteamRunningOnSteamDeck();
	}

	// Token: 0x060007F0 RID: 2032 RVA: 0x000279E3 File Offset: 0x00025BE3
	public static void logError(object obj)
	{
		if (obj != null)
		{
			MainControl.logError(obj.ToString());
		}
	}

	// Token: 0x060007F1 RID: 2033 RVA: 0x000279F3 File Offset: 0x00025BF3
	public static void logWarning(string message)
	{
		if (MainControl.logWarningMessages)
		{
			Debug.LogWarning("WARNING: " + message);
		}
	}

	// Token: 0x060007F2 RID: 2034 RVA: 0x00027A0C File Offset: 0x00025C0C
	public static void jumpToState(SkaldStates state)
	{
		MainControl.gameControl.setState(state);
	}

	// Token: 0x060007F3 RID: 2035 RVA: 0x00027A19 File Offset: 0x00025C19
	public static void activateAllDebugMessages()
	{
		MainControl.debugFunctions = true;
		MainControl.logMessages = true;
		MainControl.logErrorMessages = true;
	}

	// Token: 0x060007F4 RID: 2036 RVA: 0x00027A2D File Offset: 0x00025C2D
	public static void restartGame()
	{
		MainControl.RESTART = true;
	}

	// Token: 0x060007F5 RID: 2037 RVA: 0x00027A35 File Offset: 0x00025C35
	public static DataControl getDataControl()
	{
		if (MainControl.gameControl == null)
		{
			return null;
		}
		return MainControl.gameControl.getDataControl();
	}

	// Token: 0x060007F6 RID: 2038 RVA: 0x00027A4C File Offset: 0x00025C4C
	private void Update()
	{
		this.runInitialization();
		if (!this.isReady)
		{
			return;
		}
		if (MainControl.END_DEMO)
		{
			MainControl.END_DEMO = false;
			MainControl.gameControl.setState(SkaldStates.DemoOverSplash);
		}
		if (MainControl.RESTART)
		{
			MainControl.RESTART = false;
			MainControl.gameControl = new MainControl.StateControl();
			MainControl.gameControl.setState(SkaldStates.IntroSplash);
		}
		SkaldIO.update();
		ConsoleControl.update();
		FeedbackTool.update();
		ScreenControl.updateResolution();
	}

	// Token: 0x060007F7 RID: 2039 RVA: 0x00027AB6 File Offset: 0x00025CB6
	private void FixedUpdate()
	{
		if (!this.isReady)
		{
			return;
		}
		this.updateGameLogic();
	}

	// Token: 0x060007F8 RID: 2040 RVA: 0x00027AC7 File Offset: 0x00025CC7
	private void FPSDepentantUpdate()
	{
		this.frameTimer += Time.deltaTime;
		if (this.frameTimer >= this.frameLength)
		{
			this.frameTimer = 0f;
			this.updateGameLogic();
		}
	}

	// Token: 0x060007F9 RID: 2041 RVA: 0x00027AFC File Offset: 0x00025CFC
	private void updateGameLogic()
	{
		SkaldIO.updateMousePosition();
		if (MainControl.gameControl != null && !ConsoleControl.console)
		{
			if (SkaldIO.getFeedbackKey())
			{
				if (FeedbackTool.takeInput)
				{
					FeedbackTool.takeInput = false;
				}
				else
				{
					FeedbackTool.takeInput = true;
				}
			}
			if (FeedbackTool.takeInput)
			{
				if (SkaldIO.getPressedEscapeKey())
				{
					FeedbackTool.takeInput = false;
				}
				else if (SkaldIO.getPressedEnterKey())
				{
					base.StartCoroutine(FeedbackTool.sendFeedback(MainControl.getDataControl().getSessionId(), MainControl.getDataControl().printFeedbackData()));
				}
			}
			else
			{
				this.updateDebugFunctions();
				MainControl.gameControl.update();
			}
		}
		SkaldIO.clear();
	}

	// Token: 0x060007FA RID: 2042 RVA: 0x00027B8C File Offset: 0x00025D8C
	private void updateDebugFunctions()
	{
	}

	// Token: 0x060007FB RID: 2043 RVA: 0x00027B8E File Offset: 0x00025D8E
	private void OnGUI()
	{
		if (MainControl.gameControl != null)
		{
			MainControl.gameControl.updateGUI();
		}
	}

	// Token: 0x04000195 RID: 405
	private static MainControl.StateControl gameControl;

	// Token: 0x04000196 RID: 406
	private static uint DELUXE_EDITION_DLC_STEAM_ID = 2902190U;

	// Token: 0x04000197 RID: 407
	private static ulong DELUXE_EDITION_DLC_GOG_ID = 1214557486UL;

	// Token: 0x04000198 RID: 408
	private static bool IS_DELUXE_EDITION = false;

	// Token: 0x04000199 RID: 409
	public static bool debugFunctions = false;

	// Token: 0x0400019A RID: 410
	public static bool debugLight;

	// Token: 0x0400019B RID: 411
	public static bool logMessages = false;

	// Token: 0x0400019C RID: 412
	public static bool logErrorMessages = true;

	// Token: 0x0400019D RID: 413
	public static bool logWarningMessages = true;

	// Token: 0x0400019E RID: 414
	public TextAsset creditsPublic;

	// Token: 0x0400019F RID: 415
	public static TextAsset credits = null;

	// Token: 0x040001A0 RID: 416
	private static bool RESTART = false;

	// Token: 0x040001A1 RID: 417
	private static bool END_DEMO = false;

	// Token: 0x040001A2 RID: 418
	private float frameTimer;

	// Token: 0x040001A3 RID: 419
	private float frameLength = 0.0125f;

	// Token: 0x040001A4 RID: 420
	private MainControl.InitializationSteps initSteps;

	// Token: 0x040001A5 RID: 421
	private bool isReady;

	// Token: 0x020001F9 RID: 505
	private enum InitializationSteps
	{
		// Token: 0x040007C9 RID: 1993
		Begin,
		// Token: 0x040007CA RID: 1994
		WaitForPlatformSDKInit,
		// Token: 0x040007CB RID: 1995
		Finish,
		// Token: 0x040007CC RID: 1996
		Done
	}

	// Token: 0x020001FA RID: 506
	private class StateControl
	{
		// Token: 0x060017C1 RID: 6081 RVA: 0x0006903A File Offset: 0x0006723A
		public StateControl()
		{
			this.start();
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x00069058 File Offset: 0x00067258
		public void start()
		{
			MainControl.log("Initializing State Control");
			GameData.loadData();
			this.dataControl = new DataControl();
			this.dataControl.initialize();
			MainControl.log("Creating Current State");
			this.overlandState = new OverlandState(this.dataControl);
			this.currentState = new GameStartSplashState(this.dataControl);
			MainControl.log("Completed State Control");
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x000690C0 File Offset: 0x000672C0
		public bool allowAudio()
		{
			return this.currentState != null && this.currentState.allowAudio();
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x000690D7 File Offset: 0x000672D7
		public DataControl getDataControl()
		{
			return this.dataControl;
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x000690E0 File Offset: 0x000672E0
		public void runTestSnippet()
		{
			MainControl.debugFunctions = true;
			if (this.currentState is IntroMenuState || this.currentState is GameStartSplashState || this.currentState is PreIntroMenuState)
			{
				this.dataControl.addPremadeCharacters();
				this.dataControl.setStartingData();
				this.dataControl.setHour("8");
				this.setState(SkaldStates.Overland);
			}
			this.dataControl.clearScene();
			CutSceneControl.clearCutScenes();
			this.getDataControl().testSnippet();
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x00069162 File Offset: 0x00067362
		public void debugQuickLoad()
		{
			this.getDataControl().gameLoad();
			MainControl.debugFunctions = true;
			this.setState(SkaldStates.Overland);
			this.dataControl.clearScene();
			CutSceneControl.clearCutScenes();
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x0006918C File Offset: 0x0006738C
		public void restartAtPosition()
		{
			int xpos = this.dataControl.currentMap.getXPos();
			int ypos = this.dataControl.currentMap.getYPos();
			string id = this.dataControl.currentMap.getId();
			Party party = this.dataControl.getParty();
			TextureTools.clearBuffer();
			Resources.UnloadUnusedAssets();
			this.start();
			this.dataControl.setParty(party);
			this.dataControl.mountMap(id);
			this.dataControl.setOverland(xpos, ypos, true);
			this.currentState = this.overlandState;
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x0006921D File Offset: 0x0006741D
		public void unloadAssets()
		{
			if (this.assetReloadCounter == 0)
			{
				this.assetReloadCounter = this.assetReloadFrequency;
				return;
			}
			this.assetReloadCounter--;
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x00069244 File Offset: 0x00067444
		public void update()
		{
			this.unloadAssets();
			AudioControl.update();
			this.setState(this.currentState.getTargetState());
			this.currentState.updateGUI();
			if (PopUpControl.hasPopUp())
			{
				if (PopUpControl.allowCharaterSwap())
				{
					if (SkaldIO.getPressedNextCharacterKey())
					{
						this.dataControl.changePC(1);
					}
					this.currentState.drawPortraits();
				}
				PopUpControl.testHandlePopUp();
			}
			else if (!CutSceneControl.hasCutScene())
			{
				this.currentState.update();
			}
			TextureTools.drawCount = 0UL;
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x000692C4 File Offset: 0x000674C4
		private StateBase getCombatState()
		{
			if (this.combatState == null)
			{
				this.combatState = new CombatPlanningState(this.dataControl);
			}
			return this.combatState;
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x000692E8 File Offset: 0x000674E8
		private SkaldStates adjustTargetState(SkaldStates targetState)
		{
			if (this.dataControl.isCampActivityMounted() && this.currentState == this.overlandState)
			{
				targetState = SkaldStates.CampActivities;
			}
			if (this.dataControl.getCharacterCreatorUseCase() != null && this.currentState == this.overlandState)
			{
				targetState = SkaldStates.ClassEditor;
			}
			if (this.dataControl.getWorkBench() != null)
			{
				targetState = SkaldStates.Crafting;
			}
			if (this.dataControl.isContainerMounted() && this.currentState == this.overlandState)
			{
				targetState = SkaldStates.Container;
			}
			if (this.dataControl.isRandomEncounterMounted())
			{
				targetState = SkaldStates.RandomEncounterState;
			}
			if (this.dataControl.isCombatActive() && this.currentState is TradeState)
			{
				targetState = SkaldStates.CombatStart;
			}
			if (this.dataControl.isCombatActive() && this.currentState is AttackPromptState)
			{
				targetState = SkaldStates.CombatStart;
			}
			if (this.dataControl.isCombatActive() && this.currentState is RandomEncounterState)
			{
				targetState = SkaldStates.CombatStart;
			}
			if (this.dataControl.isCombatActive() && this.currentState == this.sceneState && targetState == SkaldStates.Null)
			{
				targetState = SkaldStates.CombatStart;
			}
			if (this.dataControl.isCombatActive() && this.currentState == this.overlandState && targetState == SkaldStates.Null)
			{
				targetState = SkaldStates.CombatStart;
			}
			if (this.dataControl.isCombatActive() && targetState == SkaldStates.Overland)
			{
				targetState = SkaldStates.CombatPlanning;
			}
			if (this.dataControl.isSceneMounted() && (this.currentState == this.overlandState || this.currentState is InventoryBaseState || this.currentState is CampActivityState))
			{
				targetState = SkaldStates.Scene;
			}
			if (this.dataControl.isInteractPartyMounted() && this.currentState == this.overlandState)
			{
				targetState = SkaldStates.Interact;
			}
			if (!this.dataControl.isSceneMounted() && this.currentState == this.sceneState)
			{
				targetState = SkaldStates.Overland;
			}
			if ((this.currentState == this.sceneState || this.currentState == this.overlandState || this.currentState is InteractState) && this.dataControl.isStoreMounted())
			{
				targetState = SkaldStates.Trade;
			}
			return targetState;
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x000694D8 File Offset: 0x000676D8
		public void setState(SkaldStates targetState)
		{
			StateBase stateBase = null;
			if (targetState == SkaldStates.DemoOverSplash)
			{
				this.currentState = new DemoOverSplashState(this.dataControl);
				return;
			}
			if (targetState == SkaldStates.Overland && !this.dataControl.isGameStarted())
			{
				targetState = SkaldStates.IntroMenu;
			}
			if (this.dataControl.isPartyDead() && (this.currentState is CombatContinue || this.currentState is OverlandState))
			{
				if (targetState == SkaldStates.LoadMenu)
				{
					this.currentState = new LoadMenuState(this.dataControl, SkaldStates.GameOver);
					return;
				}
				if (!(this.currentState is GameOverState) && !(this.currentState is LoadMenuState))
				{
					this.currentState = new GameOverState(this.dataControl);
				}
				return;
			}
			else
			{
				targetState = this.adjustTargetState(targetState);
				if (targetState == SkaldStates.Null)
				{
					return;
				}
				if (targetState == SkaldStates.Scene)
				{
					if (!this.dataControl.isSceneMounted())
					{
						targetState = SkaldStates.Overland;
					}
					else
					{
						this.sceneState = new SceneState(this.dataControl);
						stateBase = this.sceneState;
					}
				}
				if (targetState == SkaldStates.IntroSplash)
				{
					stateBase = new PreIntroMenuState(this.dataControl);
				}
				else if (targetState == SkaldStates.LoadModule)
				{
					stateBase = new LoadModuleState(this.dataControl);
				}
				else if (targetState == SkaldStates.GameStartSplash)
				{
					stateBase = new GameStartSplashState(this.dataControl);
				}
				else if (targetState == SkaldStates.Container)
				{
					stateBase = new ContainerState(this.dataControl);
				}
				else if (targetState == SkaldStates.AttackPrompt)
				{
					stateBase = new AttackPromptState(this.dataControl);
				}
				else if (targetState == SkaldStates.Overland)
				{
					stateBase = this.overlandState.activateState();
				}
				else if (targetState == SkaldStates.OverlandSpellTargeting)
				{
					stateBase = new OverlandSpellTargeting(this.dataControl);
				}
				else if (targetState == SkaldStates.Menu)
				{
					stateBase = new MenuState(this.dataControl);
				}
				else if (targetState == SkaldStates.SaveMenu)
				{
					stateBase = new SaveMenuState(this.dataControl);
				}
				else if (targetState == SkaldStates.PartyManagement)
				{
					stateBase = new PartyManagementState(this.dataControl);
				}
				else if (targetState == SkaldStates.LoadMenu)
				{
					if (stateBase is IntroMenuState || stateBase is GameOverState)
					{
						stateBase = new LoadMenuState(this.dataControl, SkaldStates.IntroMenu);
					}
					else
					{
						stateBase = new LoadMenuState(this.dataControl);
					}
				}
				else if (targetState == SkaldStates.CampActivities)
				{
					stateBase = new CampActivityState(this.dataControl);
				}
				else if (targetState == SkaldStates.SettingsKeyBindings)
				{
					stateBase = new SettingsKeyBindingState(this.dataControl);
				}
				else if (targetState == SkaldStates.SettingsFonts)
				{
					stateBase = new SettingsFontSelectionState(this.dataControl);
				}
				else if (targetState == SkaldStates.SettingsVideo)
				{
					stateBase = new SettingsVideo(this.dataControl);
				}
				else if (targetState == SkaldStates.SettingsAudio)
				{
					stateBase = new SettingsAudioState(this.dataControl);
				}
				else if (targetState == SkaldStates.SettingsDifficulty)
				{
					stateBase = new SettingsDifficulty(this.dataControl);
				}
				else if (targetState == SkaldStates.SettingsGameplay)
				{
					stateBase = new SettingsGameplayState(this.dataControl);
				}
				else if (targetState == SkaldStates.Feats)
				{
					stateBase = new FeatBuyState(this.dataControl);
				}
				else if (targetState == SkaldStates.Journal)
				{
					stateBase = new JournalState(this.dataControl);
				}
				else if (targetState == SkaldStates.Attributes)
				{
					stateBase = new AttributeState(this.dataControl);
				}
				else if (targetState == SkaldStates.Trade)
				{
					stateBase = new TradeState(this.dataControl);
				}
				else if (targetState == SkaldStates.Inventory)
				{
					stateBase = new InventoryGridState(this.dataControl);
				}
				else if (targetState == SkaldStates.Character)
				{
					if (this.dataControl.getCurrentPC().canLevelUp())
					{
						stateBase = new FeatBuyState(this.dataControl);
					}
					else
					{
						stateBase = new CharacterState(this.dataControl);
					}
				}
				else if (targetState == SkaldStates.CombatPlanning)
				{
					stateBase = this.getCombatState().activateState();
				}
				else if (targetState == SkaldStates.CombatPlacement)
				{
					stateBase = new CombatPlacementState(this.dataControl);
				}
				else if (targetState == SkaldStates.Abilities)
				{
					stateBase = new AbilitiesState(this.dataControl);
				}
				else if (targetState == SkaldStates.CombatOver)
				{
					stateBase = new CombatOverState(this.dataControl);
				}
				else if (targetState == SkaldStates.CombatContinue)
				{
					stateBase = new CombatContinue(this.dataControl);
				}
				else if (targetState == SkaldStates.CombatResolve)
				{
					stateBase = new CombatResolveState(this.dataControl);
				}
				else if (targetState == SkaldStates.CombatAbilityTargeting)
				{
					stateBase = new CombatAbilityTargeting(this.dataControl);
				}
				else if (targetState == SkaldStates.CombatSpellTargeting)
				{
					stateBase = new CombatSpellTargeting(this.dataControl);
				}
				else if (targetState == SkaldStates.CombatLog)
				{
					stateBase = new CombatLogState(this.dataControl);
				}
				else if (targetState == SkaldStates.CombatStart)
				{
					stateBase = new CombatStartState(this.dataControl);
				}
				else if (targetState == SkaldStates.Interact)
				{
					stateBase = new InteractState(this.dataControl);
				}
				else if (targetState == SkaldStates.Quests)
				{
					stateBase = new QuestState(this.dataControl);
				}
				else if (targetState == SkaldStates.Factions)
				{
					stateBase = new FactionsState(this.dataControl);
				}
				else if (targetState == SkaldStates.Feats)
				{
					stateBase = new FeatBuyState(this.dataControl);
				}
				else if (targetState == SkaldStates.GameOver)
				{
					stateBase = new GameOverState(this.dataControl);
				}
				else if (targetState == SkaldStates.Spells)
				{
					stateBase = new SpellsState(this.dataControl);
				}
				else if (targetState == SkaldStates.Crafting)
				{
					stateBase = new CraftingState(this.dataControl);
				}
				else if (targetState == SkaldStates.ClassEditor)
				{
					stateBase = new CharacterCreationClassState(this.dataControl);
				}
				else if (targetState == SkaldStates.BackgroundEditor)
				{
					stateBase = new CharacterCreationBackgroundState(this.dataControl);
				}
				else if (targetState == SkaldStates.RandomEncounterState)
				{
					stateBase = new RandomEncounterState(this.dataControl);
				}
				else if (targetState == SkaldStates.StatsEditor)
				{
					stateBase = new CharacterCreationStatsState(this.dataControl);
				}
				else if (targetState == SkaldStates.DifficultySelector)
				{
					stateBase = new DifficultySelectorState(this.dataControl);
				}
				else if (targetState == SkaldStates.FeatsCharacterCreation)
				{
					stateBase = new CharacterCreationFeatsState(this.dataControl);
				}
				else if (targetState == SkaldStates.ApperanceEditor)
				{
					stateBase = new CharacterCreationApperanceState(this.dataControl);
				}
				else if (targetState == SkaldStates.IntroMenu)
				{
					stateBase = new IntroMenuState(this.dataControl);
				}
				else if (targetState == SkaldStates.Credits)
				{
					stateBase = new CreditsState(this.dataControl);
				}
				else if (targetState != SkaldStates.Null && targetState != SkaldStates.Scene)
				{
					MainControl.logError("Tried to set non-existant state: " + targetState.ToString());
				}
				if (stateBase != null)
				{
					if (this.currentState != null)
					{
						this.currentState.handOverDataToNewState(stateBase);
					}
					this.currentState = stateBase;
				}
				return;
			}
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x00069A77 File Offset: 0x00067C77
		public void winGame(string cutsceneId)
		{
			this.currentState = new GameWinState(MainControl.getDataControl(), cutsceneId);
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x00069A8A File Offset: 0x00067C8A
		public void updateGUI()
		{
			FeedbackTool.printFeedbackTool();
		}

		// Token: 0x040007CD RID: 1997
		private DataControl dataControl;

		// Token: 0x040007CE RID: 1998
		private StateBase currentState;

		// Token: 0x040007CF RID: 1999
		private StateBase sceneState;

		// Token: 0x040007D0 RID: 2000
		private StateBase combatState;

		// Token: 0x040007D1 RID: 2001
		private StateBase overlandState;

		// Token: 0x040007D2 RID: 2002
		private int assetReloadCounter;

		// Token: 0x040007D3 RID: 2003
		private int assetReloadFrequency = Application.targetFrameRate * 30;
	}
}
