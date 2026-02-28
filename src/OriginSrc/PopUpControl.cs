using System;
using System.Collections.Generic;

// Token: 0x02000043 RID: 67
public static class PopUpControl
{
	// Token: 0x0600081C RID: 2076 RVA: 0x00028017 File Offset: 0x00026217
	public static void addPopUpOK(string description)
	{
		PopUpControl.addPopUp(new PopUpOK(description));
	}

	// Token: 0x0600081D RID: 2077 RVA: 0x00028024 File Offset: 0x00026224
	public static void addPopUpName(Character character)
	{
		PopUpControl.addPopUp(new PopUpName(character));
	}

	// Token: 0x0600081E RID: 2078 RVA: 0x00028031 File Offset: 0x00026231
	public static void addPopUpBook(ItemBook book)
	{
		PopUpControl.addPopUp(new PopUpBook(book));
	}

	// Token: 0x0600081F RID: 2079 RVA: 0x0002803E File Offset: 0x0002623E
	public static void addRandomEncounterTestPopUp(RandomEncounterState state, string description, string successString, string failureString, int difficulty, AttributesControl.CoreAttributes attribute)
	{
		PopUpControl.addPopUp(new PopUpRandomEncounterTest(state, description, successString, failureString, difficulty, attribute));
	}

	// Token: 0x06000820 RID: 2080 RVA: 0x00028052 File Offset: 0x00026252
	public static void addPopUpSystemMenu(string description)
	{
		PopUpControl.addPopUp(new PopUpSystemMenu(description));
	}

	// Token: 0x06000821 RID: 2081 RVA: 0x0002805F File Offset: 0x0002625F
	public static void addPopUpLoot(Inventory inventory)
	{
		PopUpControl.addPopUp(new PopUpLoot(inventory));
	}

	// Token: 0x06000822 RID: 2082 RVA: 0x0002806C File Offset: 0x0002626C
	public static bool allowCharaterSwap()
	{
		return PopUpControl.hasPopUp() && PopUpControl.getCurrentPopUp().allowsCharacterSwap();
	}

	// Token: 0x06000823 RID: 2083 RVA: 0x00028081 File Offset: 0x00026281
	public static void addPopUpLock(PropLockable propLockable)
	{
		PopUpControl.addPopUp(new PopUpLock(propLockable));
	}

	// Token: 0x06000824 RID: 2084 RVA: 0x0002808E File Offset: 0x0002628E
	public static void addPopUpLevelUp(Character character)
	{
		PopUpControl.addPopUp(new PopUpLevelUp(character));
	}

	// Token: 0x06000825 RID: 2085 RVA: 0x0002809B File Offset: 0x0002629B
	public static void addPopUpSaveOverwrite(LoadSaveBaseMenuState callingState)
	{
		PopUpControl.addPopUp(new PopUpSaveOverwrite(callingState));
	}

	// Token: 0x06000826 RID: 2086 RVA: 0x000280A8 File Offset: 0x000262A8
	public static void addPopUpCreateSave(SaveMenuState callingState, string saveName)
	{
		PopUpControl.addPopUp(new PopUpCreateSave(callingState, saveName));
	}

	// Token: 0x06000827 RID: 2087 RVA: 0x000280B6 File Offset: 0x000262B6
	public static void addPopUpSaveDelete(LoadSaveBaseMenuState callingState)
	{
		PopUpControl.addPopUp(new PopUpSaveDelete(callingState));
	}

	// Token: 0x06000828 RID: 2088 RVA: 0x000280C3 File Offset: 0x000262C3
	public static void addPopUpVisualStyle(IntroMenuState introMenuState)
	{
		PopUpControl.addPopUp(new PopUpVisualStyle(introMenuState));
	}

	// Token: 0x06000829 RID: 2089 RVA: 0x000280D0 File Offset: 0x000262D0
	public static void addPopUpStealLocked()
	{
		PopUpControl.addPopUp(new PopUpStealLocked());
	}

	// Token: 0x0600082A RID: 2090 RVA: 0x000280DC File Offset: 0x000262DC
	public static void addPopUpSaveRename(LoadSaveBaseMenuState callingState)
	{
		PopUpControl.addPopUp(new PopUpSaveRename(callingState));
	}

	// Token: 0x0600082B RID: 2091 RVA: 0x000280E9 File Offset: 0x000262E9
	public static void addSceneRandomTestPopUp(Scene scene, int exitNumber)
	{
		PopUpControl.addPopUp(new PopUpSceneTestRandom(scene, exitNumber));
	}

	// Token: 0x0600082C RID: 2092 RVA: 0x000280F7 File Offset: 0x000262F7
	public static PopUpYesNo addPopUpYesNo(string message)
	{
		PopUpYesNo popUpYesNo = new PopUpYesNo(message);
		PopUpControl.addPopUp(popUpYesNo);
		return popUpYesNo;
	}

	// Token: 0x0600082D RID: 2093 RVA: 0x00028105 File Offset: 0x00026305
	public static void addPopUpRest(CampActivityState state)
	{
		PopUpControl.addPopUp(new PopUpRest(state));
	}

	// Token: 0x0600082E RID: 2094 RVA: 0x00028112 File Offset: 0x00026312
	public static void addPopUpNewSpells(string school, int tier, int number, Character character)
	{
		PopUpControl.addPopUp(new PopUpSpellSelector(school, tier, number, character));
	}

	// Token: 0x0600082F RID: 2095 RVA: 0x00028122 File Offset: 0x00026322
	public static void addPopUpEncumberance()
	{
		PopUpControl.addPopUp(new PopUpEncumberance());
	}

	// Token: 0x06000830 RID: 2096 RVA: 0x0002812E File Offset: 0x0002632E
	public static void addPopUpQuitGame()
	{
		PopUpControl.addPopUp(new PopUpQuitGame());
	}

	// Token: 0x06000831 RID: 2097 RVA: 0x0002813A File Offset: 0x0002633A
	public static void addPopUpRentRoom(float priceMod)
	{
		PopUpControl.addPopUp(new PopUpRentRoom(priceMod));
	}

	// Token: 0x06000832 RID: 2098 RVA: 0x00028147 File Offset: 0x00026347
	public static void addPopUpHeal(float priceMod)
	{
		PopUpControl.addPopUp(new PopUpHeal(priceMod));
	}

	// Token: 0x06000833 RID: 2099 RVA: 0x00028154 File Offset: 0x00026354
	public static void addSceneStaticTestPopUp(Scene scene, int exitNumber)
	{
		PopUpControl.addPopUp(new PopUpSceneTestStatic(scene, exitNumber));
	}

	// Token: 0x06000834 RID: 2100 RVA: 0x00028162 File Offset: 0x00026362
	public static void addStealingPopUp(Store store)
	{
		PopUpControl.addPopUp(new PopUpStealing(store));
	}

	// Token: 0x06000835 RID: 2101 RVA: 0x0002816F File Offset: 0x0002636F
	public static void addPropRandomTestPopUp(PropTest prop)
	{
		PopUpControl.addPopUp(new PopUpTestPropRandom(prop));
	}

	// Token: 0x06000836 RID: 2102 RVA: 0x0002817C File Offset: 0x0002637C
	public static void addHireMercPopUp(Character character)
	{
		PopUpControl.addPopUp(new PopUpHireMerc(character));
	}

	// Token: 0x06000837 RID: 2103 RVA: 0x0002818C File Offset: 0x0002638C
	public static void addTutorialPopUp(string tutorialId)
	{
		SKALDProjectData.EncylopediaContainer.TutorialContainer.TutorialCategory.Tutorial tutorial = GameData.getTutorial(tutorialId);
		if (tutorial == null)
		{
			return;
		}
		if (tutorial.priority > GlobalSettings.getGamePlaySettings().getTutorialDegree())
		{
			return;
		}
		PopUpControl.addPopUp(new PopUpTutorial(tutorial));
	}

	// Token: 0x06000838 RID: 2104 RVA: 0x000281C2 File Offset: 0x000263C2
	public static bool hasPopUp()
	{
		return PopUpControl.popUps.Count > 0;
	}

	// Token: 0x06000839 RID: 2105 RVA: 0x000281D1 File Offset: 0x000263D1
	public static bool allowTooltips()
	{
		return !PopUpControl.hasPopUp() || PopUpControl.getCurrentPopUp().allowTooltips();
	}

	// Token: 0x0600083A RID: 2106 RVA: 0x000281E6 File Offset: 0x000263E6
	public static void draw(TextureTools.TextureData target)
	{
		if (!PopUpControl.hasPopUp())
		{
			return;
		}
		PopUpControl.getCurrentPopUp().draw(target);
	}

	// Token: 0x0600083B RID: 2107 RVA: 0x000281FB File Offset: 0x000263FB
	public static void succeed()
	{
		if (PopUpControl.getCurrentPopUp() == null || !(PopUpControl.getCurrentPopUp() is PopUpTestBase))
		{
			return;
		}
		(PopUpControl.getCurrentPopUp() as PopUpTestBase).succeed();
	}

	// Token: 0x0600083C RID: 2108 RVA: 0x00028220 File Offset: 0x00026420
	public static void fail()
	{
		if (PopUpControl.getCurrentPopUp() == null || !(PopUpControl.getCurrentPopUp() is PopUpTestBase))
		{
			return;
		}
		(PopUpControl.getCurrentPopUp() as PopUpTestBase).fail();
	}

	// Token: 0x0600083D RID: 2109 RVA: 0x00028245 File Offset: 0x00026445
	public static void testHandlePopUp()
	{
		if (!PopUpControl.hasPopUp())
		{
			return;
		}
		PopUpControl.getCurrentPopUp().handle();
		PopUpControl.popCurrentPopUp();
	}

	// Token: 0x0600083E RID: 2110 RVA: 0x0002825E File Offset: 0x0002645E
	private static PopUpBase getCurrentPopUp()
	{
		if (!PopUpControl.hasPopUp())
		{
			return null;
		}
		return PopUpControl.popUps[PopUpControl.popUps.Count - 1];
	}

	// Token: 0x0600083F RID: 2111 RVA: 0x0002827F File Offset: 0x0002647F
	private static void addPopUp(PopUpBase popUp)
	{
		PopUpControl.popCurrentPopUp();
		PopUpControl.popUps.Add(popUp);
	}

	// Token: 0x06000840 RID: 2112 RVA: 0x00028291 File Offset: 0x00026491
	private static void popCurrentPopUp()
	{
		if (!PopUpControl.hasPopUp())
		{
			return;
		}
		if (PopUpControl.getCurrentPopUp().isHandled())
		{
			PopUpControl.popUps.RemoveAt(PopUpControl.popUps.Count - 1);
		}
	}

	// Token: 0x040001B5 RID: 437
	private static List<PopUpBase> popUps = new List<PopUpBase>();
}
