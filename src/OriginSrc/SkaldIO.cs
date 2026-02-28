using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

// Token: 0x02000068 RID: 104
public static class SkaldIO
{
	// Token: 0x060008EF RID: 2287 RVA: 0x0002B434 File Offset: 0x00029634
	public static void updateMousePosition()
	{
		Vector2 rightJoystickPosition = SkaldIO.ControllerInputControl.getRightJoystickPosition();
		float controllerSensitivity = GlobalSettings.getGamePlaySettings().getControllerSensitivity();
		SkaldIO.virtualCursorHorizontalOffset += rightJoystickPosition.x * controllerSensitivity * ScreenControl.getCurrentScreenScale();
		SkaldIO.virtualCursorVerticalOffset += rightJoystickPosition.y * controllerSensitivity * ScreenControl.getCurrentScreenScale();
		Vector2 vector = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		if (vector.x != SkaldIO.lastGlobalSystemMousePosition.x || vector.y != SkaldIO.lastGlobalSystemMousePosition.y)
		{
			SkaldIO.virtualCursorHorizontalOffset = 0f;
			SkaldIO.virtualCursorVerticalOffset = 0f;
		}
		if (vector.x + SkaldIO.virtualCursorHorizontalOffset < 0f)
		{
			SkaldIO.virtualCursorHorizontalOffset = 0f - vector.x;
		}
		else if (vector.x + SkaldIO.virtualCursorHorizontalOffset > (float)ScreenControl.getCurrentGameWidth())
		{
			SkaldIO.virtualCursorHorizontalOffset = 0f - (vector.x - (float)ScreenControl.getCurrentGameWidth());
		}
		if (vector.y + SkaldIO.virtualCursorVerticalOffset < 0f)
		{
			SkaldIO.virtualCursorVerticalOffset = 0f - vector.y;
		}
		else if (vector.y + SkaldIO.virtualCursorVerticalOffset > (float)ScreenControl.getCurrentGameHeight())
		{
			SkaldIO.virtualCursorVerticalOffset = 0f - (vector.y - (float)ScreenControl.getCurrentGameHeight());
		}
		SkaldIO.lastGlobalSystemMousePosition = vector;
	}

	// Token: 0x060008F0 RID: 2288 RVA: 0x0002B584 File Offset: 0x00029784
	public static void setVirtualMousePosition(int x, int y)
	{
		SkaldIO.virtualCursorHorizontalOffset = 0f;
		SkaldIO.virtualCursorVerticalOffset = 0f;
		Vector2 globalMousePosition = SkaldIO.getGlobalMousePosition();
		SkaldIO.virtualCursorHorizontalOffset = ((float)x - globalMousePosition.x) / 480f * (float)Screen.width;
		SkaldIO.virtualCursorVerticalOffset = ((float)y - globalMousePosition.y) / 270f * (float)Screen.height;
	}

	// Token: 0x060008F1 RID: 2289 RVA: 0x0002B5E4 File Offset: 0x000297E4
	public static void clear()
	{
		SkaldIO.ControllerInputControl.clear();
		SkaldIO.keyPressed.Clear();
		SkaldIO.keyUp.Clear();
		SkaldIO.keyHeldDown.Clear();
		SkaldIO.mousePressed.Clear();
		SkaldIO.mouseHeldDown.Clear();
		SkaldIO.mouseWheelScroll = 0f;
		SkaldIO.mouseUp.Clear();
		SkaldIO.inputString = "";
	}

	// Token: 0x060008F2 RID: 2290 RVA: 0x0002B648 File Offset: 0x00029848
	public static void update()
	{
		SkaldIO.ControllerInputControl.update();
		foreach (KeyCode keyCode in SkaldIO.keyCodes)
		{
			if (Input.GetKeyDown(keyCode))
			{
				SkaldIO.lastKeyPressed = keyCode;
				SkaldIO.keyPressed.Add(keyCode);
			}
			if (Input.GetKey(keyCode))
			{
				SkaldIO.keyHeldDown.Add(keyCode);
			}
			if (Input.GetKeyUp(keyCode))
			{
				SkaldIO.keyUp.Add(keyCode);
			}
		}
		for (int j = 0; j < 2; j++)
		{
			if (Input.GetMouseButton(j))
			{
				SkaldIO.mouseHeldDown.Add(j);
			}
			if (Input.GetMouseButtonDown(j))
			{
				SkaldIO.mousePressed.Add(j);
			}
			if (Input.GetMouseButtonUp(j))
			{
				SkaldIO.mouseUp.Add(j);
			}
		}
		if (!SkaldIO.mouseUp.Contains(0) && SkaldIO.ControllerInputControl.leftTriggerUp())
		{
			SkaldIO.mouseUp.Add(0);
		}
		if (!SkaldIO.mousePressed.Contains(0) && SkaldIO.ControllerInputControl.leftTriggerPressed())
		{
			SkaldIO.mousePressed.Add(0);
		}
		if (!SkaldIO.mouseUp.Contains(1) && SkaldIO.ControllerInputControl.rightTriggerUp())
		{
			SkaldIO.mouseUp.Add(1);
		}
		if (!SkaldIO.mousePressed.Contains(1) && SkaldIO.ControllerInputControl.rightTriggerPressed())
		{
			SkaldIO.mousePressed.Add(1);
		}
		if (!SkaldIO.mouseHeldDown.Contains(0) && SkaldIO.ControllerInputControl.leftTriggerHeld())
		{
			SkaldIO.mouseHeldDown.Add(0);
		}
		if (!SkaldIO.mouseHeldDown.Contains(1) && SkaldIO.ControllerInputControl.rightTriggerHeld())
		{
			SkaldIO.mouseHeldDown.Add(1);
		}
		SkaldIO.inputString += Input.inputString;
		if (Input.mouseScrollDelta.y != 0f)
		{
			SkaldIO.mouseWheelScroll = Input.mouseScrollDelta.y;
		}
	}

	// Token: 0x060008F3 RID: 2291 RVA: 0x0002B7E4 File Offset: 0x000299E4
	public static bool getKeyPressed(KeyCode key)
	{
		return SkaldIO.keyPressed.Contains(key);
	}

	// Token: 0x060008F4 RID: 2292 RVA: 0x0002B7F6 File Offset: 0x000299F6
	private static float getMouseScrollDelta()
	{
		return SkaldIO.mouseWheelScroll;
	}

	// Token: 0x060008F5 RID: 2293 RVA: 0x0002B7FD File Offset: 0x000299FD
	public static bool pressedLayoutKey()
	{
		return SkaldIO.ControllerInputControl.isDpadUpPressed();
	}

	// Token: 0x060008F6 RID: 2294 RVA: 0x0002B804 File Offset: 0x00029A04
	public static bool getMouseWheelScrollUp()
	{
		return SkaldIO.getMouseScrollDelta() > 0f;
	}

	// Token: 0x060008F7 RID: 2295 RVA: 0x0002B812 File Offset: 0x00029A12
	public static bool getMouseWheelScrollDown()
	{
		return SkaldIO.getMouseScrollDelta() < 0f;
	}

	// Token: 0x060008F8 RID: 2296 RVA: 0x0002B820 File Offset: 0x00029A20
	public static bool isLeftStickHeldRight()
	{
		return SkaldIO.ControllerInputControl.isLeftStickRightHeld();
	}

	// Token: 0x060008F9 RID: 2297 RVA: 0x0002B827 File Offset: 0x00029A27
	public static bool isLeftStickHeldLeft()
	{
		return SkaldIO.ControllerInputControl.isLeftStickLeftHeld();
	}

	// Token: 0x060008FA RID: 2298 RVA: 0x0002B82E File Offset: 0x00029A2E
	public static KeyCode getLastKeyPressed()
	{
		return SkaldIO.lastKeyPressed;
	}

	// Token: 0x060008FB RID: 2299 RVA: 0x0002B835 File Offset: 0x00029A35
	public static bool getKeyHeldDown(KeyCode key)
	{
		return SkaldIO.keyHeldDown.Contains(key);
	}

	// Token: 0x060008FC RID: 2300 RVA: 0x0002B847 File Offset: 0x00029A47
	public static bool getKeyUp(KeyCode key)
	{
		return SkaldIO.keyUp.Contains(key);
	}

	// Token: 0x060008FD RID: 2301 RVA: 0x0002B859 File Offset: 0x00029A59
	public static string getInputString()
	{
		return SkaldIO.inputString;
	}

	// Token: 0x060008FE RID: 2302 RVA: 0x0002B860 File Offset: 0x00029A60
	public static bool getMousePressed(int index)
	{
		return SkaldIO.mousePressed.Contains(index);
	}

	// Token: 0x060008FF RID: 2303 RVA: 0x0002B872 File Offset: 0x00029A72
	public static bool getMouseUp(int index)
	{
		return SkaldIO.mouseUp.Contains(index);
	}

	// Token: 0x06000900 RID: 2304 RVA: 0x0002B884 File Offset: 0x00029A84
	public static bool getMouseHeldDown(int index)
	{
		return SkaldIO.mouseHeldDown.Contains(index);
	}

	// Token: 0x06000901 RID: 2305 RVA: 0x0002B898 File Offset: 0x00029A98
	public static Vector2 getMousePosition()
	{
		Vector2 globalMousePosition = SkaldIO.getGlobalMousePosition();
		if (globalMousePosition.x < 0f || globalMousePosition.x >= 480f)
		{
			return new Vector2(-1f, -1f);
		}
		if (globalMousePosition.y < 0f || globalMousePosition.y >= 270f)
		{
			return new Vector2(-1f, -1f);
		}
		return globalMousePosition;
	}

	// Token: 0x06000902 RID: 2306 RVA: 0x0002B900 File Offset: 0x00029B00
	public static Vector2 getGlobalMousePosition()
	{
		float num = (Input.mousePosition.x + SkaldIO.virtualCursorHorizontalOffset) / (float)Screen.width;
		float num2 = (Input.mousePosition.y + SkaldIO.virtualCursorVerticalOffset) / (float)Screen.height;
		return new Vector2(num * 480f, num2 * 270f);
	}

	// Token: 0x06000903 RID: 2307 RVA: 0x0002B94E File Offset: 0x00029B4E
	public static bool anyKeyDown()
	{
		return SkaldIO.ControllerInputControl.isAnyControllerButtonPressed() || SkaldIO.keyHeldDown.Count > 0;
	}

	// Token: 0x06000904 RID: 2308 RVA: 0x0002B969 File Offset: 0x00029B69
	public static void openVirtualKeyboard(string description, string startingText)
	{
		if (MainControl.runningOnSteamDeck())
		{
			SteamUtils.ShowFloatingGamepadTextInput(0, 0, 0, 100, 100);
		}
	}

	// Token: 0x06000905 RID: 2309 RVA: 0x0002B97F File Offset: 0x00029B7F
	public static bool isControllerConnected()
	{
		return MainControl.runningOnSteamDeck() || SkaldIO.ControllerInputControl.isControllerConnected();
	}

	// Token: 0x06000906 RID: 2310 RVA: 0x0002B98F File Offset: 0x00029B8F
	public static bool anyKeyPressed()
	{
		return SkaldIO.keyPressed.Count > 0 || SkaldIO.mousePressed.Count > 0;
	}

	// Token: 0x06000907 RID: 2311 RVA: 0x0002B9AE File Offset: 0x00029BAE
	public static bool anyKeyUp()
	{
		return SkaldIO.keyUp.Count > 0 || SkaldIO.mouseUp.Count > 0;
	}

	// Token: 0x06000908 RID: 2312 RVA: 0x0002B9CD File Offset: 0x00029BCD
	public static bool getPressedSpellBookKey()
	{
		return SkaldIO.keyBindings != null && SkaldIO.getKeyPressed(SkaldIO.keyBindings.getSpellBookKey());
	}

	// Token: 0x06000909 RID: 2313 RVA: 0x0002B9E7 File Offset: 0x00029BE7
	public static bool getPressedConsoleKey()
	{
		return SkaldIO.keyBindings != null && (Input.GetKeyDown(SkaldIO.keyBindings.getConsoleKey()) || SkaldIO.ControllerInputControl.openConsoleCommands());
	}

	// Token: 0x0600090A RID: 2314 RVA: 0x0002BA0A File Offset: 0x00029C0A
	public static bool getPressedFeatsKey()
	{
		return SkaldIO.keyBindings != null && SkaldIO.getKeyPressed(SkaldIO.keyBindings.getFeatsKey());
	}

	// Token: 0x0600090B RID: 2315 RVA: 0x0002BA24 File Offset: 0x00029C24
	public static bool getPressedJournalKey()
	{
		return SkaldIO.keyBindings != null && (SkaldIO.getKeyPressed(SkaldIO.keyBindings.getQuestLogKey()) || SkaldIO.ControllerInputControl.menuButtonPressed());
	}

	// Token: 0x0600090C RID: 2316 RVA: 0x0002BA47 File Offset: 0x00029C47
	public static bool getPressedNextCharacterKey()
	{
		return SkaldIO.keyBindings != null && (SkaldIO.getKeyPressed(SkaldIO.keyBindings.getNextCharacterKey()) || SkaldIO.ControllerInputControl.leftBumperPressed());
	}

	// Token: 0x0600090D RID: 2317 RVA: 0x0002BA6A File Offset: 0x00029C6A
	public static bool getPressedCharacterSheetKey(bool interrogateController)
	{
		return SkaldIO.keyBindings != null && (SkaldIO.getKeyPressed(SkaldIO.keyBindings.getCharacterSheetKey()) || (interrogateController && SkaldIO.getControllerButtonYPressed()));
	}

	// Token: 0x0600090E RID: 2318 RVA: 0x0002BA95 File Offset: 0x00029C95
	public static bool getPressedInventoryKey(bool interrogateController)
	{
		return SkaldIO.keyBindings != null && (SkaldIO.getKeyPressed(SkaldIO.keyBindings.getInventoryKey()) || (interrogateController && SkaldIO.getControllerButtonXPressed()));
	}

	// Token: 0x0600090F RID: 2319 RVA: 0x0002BAC0 File Offset: 0x00029CC0
	public static bool getControllerButtonXPressed()
	{
		return SkaldIO.ControllerInputControl.buttonXPressed();
	}

	// Token: 0x06000910 RID: 2320 RVA: 0x0002BAC7 File Offset: 0x00029CC7
	public static bool getControllerButtonYPressed()
	{
		return SkaldIO.ControllerInputControl.buttonYPressed();
	}

	// Token: 0x06000911 RID: 2321 RVA: 0x0002BACE File Offset: 0x00029CCE
	public static bool getControllerButtonAPressed()
	{
		return SkaldIO.ControllerInputControl.buttonAPressed();
	}

	// Token: 0x06000912 RID: 2322 RVA: 0x0002BAD5 File Offset: 0x00029CD5
	public static bool getControllerButtonBPressed()
	{
		return SkaldIO.ControllerInputControl.buttonBPressed();
	}

	// Token: 0x06000913 RID: 2323 RVA: 0x0002BADC File Offset: 0x00029CDC
	public static bool getPressedHideKey()
	{
		return SkaldIO.keyBindings != null && (SkaldIO.getKeyPressed(SkaldIO.keyBindings.getHideKey()) || SkaldIO.ControllerInputControl.leftStickPressed());
	}

	// Token: 0x06000914 RID: 2324 RVA: 0x0002BAFF File Offset: 0x00029CFF
	public static bool getPressedLevelUpKey()
	{
		return SkaldIO.keyBindings != null && SkaldIO.getKeyPressed(SkaldIO.keyBindings.getLevelUpKey());
	}

	// Token: 0x06000915 RID: 2325 RVA: 0x0002BB19 File Offset: 0x00029D19
	public static bool getPressedToggleKey()
	{
		return SkaldIO.keyBindings != null && (SkaldIO.getKeyPressed(SkaldIO.keyBindings.getToggleKey()) || SkaldIO.ControllerInputControl.rightStickPressed());
	}

	// Token: 0x06000916 RID: 2326 RVA: 0x0002BB3C File Offset: 0x00029D3C
	public static bool getFeedbackKey()
	{
		return SkaldIO.keyBindings != null && SkaldIO.getKeyPressed(SkaldIO.keyBindings.getFeedbackKey());
	}

	// Token: 0x06000917 RID: 2327 RVA: 0x0002BB56 File Offset: 0x00029D56
	public static bool getPressedQuickLoadKey()
	{
		return SkaldIO.keyBindings != null && SkaldIO.getKeyPressed(SkaldIO.keyBindings.getQuickLoadKey());
	}

	// Token: 0x06000918 RID: 2328 RVA: 0x0002BB70 File Offset: 0x00029D70
	public static bool getHighlightKeyDown()
	{
		return SkaldIO.keyBindings != null && (SkaldIO.getKeyHeldDown(SkaldIO.keyBindings.getHighlightKey()) || SkaldIO.ControllerInputControl.rightTriggerHeld());
	}

	// Token: 0x06000919 RID: 2329 RVA: 0x0002BB93 File Offset: 0x00029D93
	public static bool getPressedEscapeKey()
	{
		return SkaldIO.keyPressed.Contains(KeyCode.Escape) || SkaldIO.getControllerButtonBPressed();
	}

	// Token: 0x0600091A RID: 2330 RVA: 0x0002BBAA File Offset: 0x00029DAA
	public static bool getPressedQuickSaveKey()
	{
		return SkaldIO.keyBindings != null && (SkaldIO.getKeyPressed(SkaldIO.keyBindings.getQuickSaveKey()) || SkaldIO.ControllerInputControl.backButtonPressed());
	}

	// Token: 0x0600091B RID: 2331 RVA: 0x0002BBCD File Offset: 0x00029DCD
	public static bool getPressedUpKey()
	{
		return SkaldIO.keyBindings != null && (SkaldIO.getKeyPressed(SkaldIO.keyBindings.getUpKey()) || SkaldIO.getKeyPressed(SkaldIO.keyBindings.getUpAltKey()) || SkaldIO.ControllerInputControl.isLeftStickUpPressed());
	}

	// Token: 0x0600091C RID: 2332 RVA: 0x0002BC01 File Offset: 0x00029E01
	public static bool getPressedDownKey()
	{
		return SkaldIO.keyBindings != null && (SkaldIO.getKeyPressed(SkaldIO.keyBindings.getDownKey()) || SkaldIO.getKeyPressed(SkaldIO.keyBindings.getDownAltKey()) || SkaldIO.ControllerInputControl.isLeftStickDownPressed());
	}

	// Token: 0x0600091D RID: 2333 RVA: 0x0002BC35 File Offset: 0x00029E35
	public static bool getPressedLeftKey()
	{
		return SkaldIO.keyBindings != null && (SkaldIO.getKeyPressed(SkaldIO.keyBindings.getLeftKey()) || SkaldIO.getKeyPressed(SkaldIO.keyBindings.getLeftAltKey()) || SkaldIO.ControllerInputControl.isLeftStickLeftPressed());
	}

	// Token: 0x0600091E RID: 2334 RVA: 0x0002BC69 File Offset: 0x00029E69
	public static bool getPressedRightKey()
	{
		return SkaldIO.keyBindings != null && (SkaldIO.getKeyPressed(SkaldIO.keyBindings.getRightKey()) || SkaldIO.getKeyPressed(SkaldIO.keyBindings.getRightAltKey()) || SkaldIO.ControllerInputControl.isLeftStickRightPressed());
	}

	// Token: 0x0600091F RID: 2335 RVA: 0x0002BC9D File Offset: 0x00029E9D
	public static bool getPressedMainInteractKey()
	{
		return SkaldIO.getKeyPressed(KeyCode.Space) || SkaldIO.getControllerButtonAPressed();
	}

	// Token: 0x06000920 RID: 2336 RVA: 0x0002BCAF File Offset: 0x00029EAF
	public static bool getPressedNumericButton1()
	{
		return SkaldIO.getKeyPressed(KeyCode.Alpha1);
	}

	// Token: 0x06000921 RID: 2337 RVA: 0x0002BCB8 File Offset: 0x00029EB8
	public static bool getPressedNumericButton2()
	{
		return SkaldIO.getKeyPressed(KeyCode.Alpha2);
	}

	// Token: 0x06000922 RID: 2338 RVA: 0x0002BCC1 File Offset: 0x00029EC1
	public static bool getPressedNumericButton3()
	{
		return SkaldIO.getKeyPressed(KeyCode.Alpha3);
	}

	// Token: 0x06000923 RID: 2339 RVA: 0x0002BCCA File Offset: 0x00029ECA
	public static bool getAbilityButtonShiftRightButtonPressed()
	{
		return SkaldIO.getKeyPressed(KeyCode.LeftControl) || SkaldIO.ControllerInputControl.isDpadLeftPressed();
	}

	// Token: 0x06000924 RID: 2340 RVA: 0x0002BCDF File Offset: 0x00029EDF
	public static bool getAbilityButtonShiftLeftButtonPressed()
	{
		return SkaldIO.getKeyPressed(KeyCode.RightControl) || SkaldIO.ControllerInputControl.isDpadRightPressed();
	}

	// Token: 0x06000925 RID: 2341 RVA: 0x0002BCF4 File Offset: 0x00029EF4
	public static bool getStateCarouselRightButtonPressed()
	{
		return SkaldIO.getKeyPressed(KeyCode.RightAlt) || SkaldIO.ControllerInputControl.rightBumperPressed();
	}

	// Token: 0x06000926 RID: 2342 RVA: 0x0002BD09 File Offset: 0x00029F09
	public static bool getStateCarouselLeftButtonPressed()
	{
		return SkaldIO.getKeyPressed(KeyCode.LeftAlt);
	}

	// Token: 0x06000927 RID: 2343 RVA: 0x0002BD15 File Offset: 0x00029F15
	public static bool getPressedEnterKey()
	{
		return SkaldIO.getKeyPressed(KeyCode.Return) || SkaldIO.ControllerInputControl.buttonAPressed();
	}

	// Token: 0x06000928 RID: 2344 RVA: 0x0002BD27 File Offset: 0x00029F27
	public static bool pasteTextPressed()
	{
		return SkaldIO.getKeyHeldDown(KeyCode.LeftControl) && SkaldIO.getKeyPressed(KeyCode.V);
	}

	// Token: 0x06000929 RID: 2345 RVA: 0x0002BD3E File Offset: 0x00029F3E
	public static bool getDownUpKey()
	{
		return SkaldIO.keyBindings != null && (SkaldIO.getKeyHeldDown(SkaldIO.keyBindings.getUpKey()) || SkaldIO.getKeyHeldDown(SkaldIO.keyBindings.getUpAltKey()) || SkaldIO.ControllerInputControl.isLeftStickUpHeld());
	}

	// Token: 0x0600092A RID: 2346 RVA: 0x0002BD72 File Offset: 0x00029F72
	public static bool getDownDownKey()
	{
		return SkaldIO.keyBindings != null && (SkaldIO.getKeyHeldDown(SkaldIO.keyBindings.getDownKey()) || SkaldIO.getKeyHeldDown(SkaldIO.keyBindings.getDownAltKey()) || SkaldIO.ControllerInputControl.isLeftStickDownHeld());
	}

	// Token: 0x0600092B RID: 2347 RVA: 0x0002BDA6 File Offset: 0x00029FA6
	public static bool getDownLeftKey()
	{
		return SkaldIO.keyBindings != null && (SkaldIO.getKeyHeldDown(SkaldIO.keyBindings.getLeftKey()) || SkaldIO.getKeyHeldDown(SkaldIO.keyBindings.getLeftAltKey()) || SkaldIO.ControllerInputControl.isLeftStickLeftHeld());
	}

	// Token: 0x0600092C RID: 2348 RVA: 0x0002BDDA File Offset: 0x00029FDA
	public static bool getDownRightKey()
	{
		return SkaldIO.keyBindings != null && (SkaldIO.getKeyHeldDown(SkaldIO.keyBindings.getRightKey()) || SkaldIO.getKeyHeldDown(SkaldIO.keyBindings.getRightAltKey()) || SkaldIO.ControllerInputControl.isLeftStickRightHeld());
	}

	// Token: 0x0600092D RID: 2349 RVA: 0x0002BE0E File Offset: 0x0002A00E
	public static bool getButtonScrollUp()
	{
		return SkaldIO.getKeyHeldDown(KeyCode.UpArrow);
	}

	// Token: 0x0600092E RID: 2350 RVA: 0x0002BE1A File Offset: 0x0002A01A
	public static bool getButtonScrollDown()
	{
		return SkaldIO.getKeyHeldDown(KeyCode.DownArrow);
	}

	// Token: 0x0600092F RID: 2351 RVA: 0x0002BE26 File Offset: 0x0002A026
	public static bool getOptionSelectionButtonDown()
	{
		return SkaldIO.ControllerInputControl.isLeftStickDownPressed();
	}

	// Token: 0x06000930 RID: 2352 RVA: 0x0002BE2D File Offset: 0x0002A02D
	public static bool getOptionSelectionButtonUp()
	{
		return SkaldIO.ControllerInputControl.isLeftStickUpPressed();
	}

	// Token: 0x06000931 RID: 2353 RVA: 0x0002BE34 File Offset: 0x0002A034
	public static bool getOptionSelectionButtonRight()
	{
		return SkaldIO.ControllerInputControl.isLeftStickRightPressed();
	}

	// Token: 0x06000932 RID: 2354 RVA: 0x0002BE3B File Offset: 0x0002A03B
	public static bool getOptionSelectionButtonLeft()
	{
		return SkaldIO.ControllerInputControl.isLeftStickLeftPressed();
	}

	// Token: 0x04000240 RID: 576
	private static KeyCode lastKeyPressed = KeyCode.None;

	// Token: 0x04000241 RID: 577
	private static List<KeyCode> keyPressed = new List<KeyCode>();

	// Token: 0x04000242 RID: 578
	private static List<KeyCode> keyHeldDown = new List<KeyCode>();

	// Token: 0x04000243 RID: 579
	private static List<KeyCode> keyUp = new List<KeyCode>();

	// Token: 0x04000244 RID: 580
	private static List<int> mousePressed = new List<int>();

	// Token: 0x04000245 RID: 581
	private static List<int> mouseHeldDown = new List<int>();

	// Token: 0x04000246 RID: 582
	private static List<int> mouseUp = new List<int>();

	// Token: 0x04000247 RID: 583
	private static string inputString = "";

	// Token: 0x04000248 RID: 584
	public static SKALDKeyBindings keyBindings = new SKALDKeyBindings();

	// Token: 0x04000249 RID: 585
	private static KeyCode[] keyCodes = (KeyCode[])Enum.GetValues(typeof(KeyCode));

	// Token: 0x0400024A RID: 586
	private static float mouseWheelScroll = 0f;

	// Token: 0x0400024B RID: 587
	private static float virtualCursorVerticalOffset = 0f;

	// Token: 0x0400024C RID: 588
	private static float virtualCursorHorizontalOffset = 0f;

	// Token: 0x0400024D RID: 589
	private static Vector2 lastGlobalSystemMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

	// Token: 0x0200022F RID: 559
	private static class ControllerInputControl
	{
		// Token: 0x0600189E RID: 6302 RVA: 0x0006C834 File Offset: 0x0006AA34
		public static bool isControllerConnected()
		{
			foreach (string a in Input.GetJoystickNames())
			{
				if (a != "" && a != "USB Game Controllers")
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x0006C878 File Offset: 0x0006AA78
		public static void update()
		{
			SkaldIO.ControllerInputControl.dpadUp.update(SkaldIO.ControllerInputControl.getDpadPosition().y > 0f);
			SkaldIO.ControllerInputControl.dpadDown.update(SkaldIO.ControllerInputControl.getDpadPosition().y < 0f);
			SkaldIO.ControllerInputControl.dpadRight.update(SkaldIO.ControllerInputControl.getDpadPosition().x > 0f);
			SkaldIO.ControllerInputControl.dpadLeft.update(SkaldIO.ControllerInputControl.getDpadPosition().x < 0f);
			SkaldIO.ControllerInputControl.leftStickUp.update(SkaldIO.ControllerInputControl.getLeftJoystickPosition().y > SkaldIO.ControllerInputControl.leftStickThreshold);
			SkaldIO.ControllerInputControl.leftStickDown.update(SkaldIO.ControllerInputControl.getLeftJoystickPosition().y < 0f - SkaldIO.ControllerInputControl.leftStickThreshold);
			SkaldIO.ControllerInputControl.leftStickRight.update(SkaldIO.ControllerInputControl.getLeftJoystickPosition().x > SkaldIO.ControllerInputControl.leftStickThreshold);
			SkaldIO.ControllerInputControl.leftStickLeft.update(SkaldIO.ControllerInputControl.getLeftJoystickPosition().x < 0f - SkaldIO.ControllerInputControl.leftStickThreshold);
			SkaldIO.ControllerInputControl.leftTrigger.update(Input.GetAxis("TriggerLeft") > 0f);
			SkaldIO.ControllerInputControl.rightTrigger.update(Input.GetAxis("TriggerRight") > 0f);
			foreach (SkaldIO.ControllerInputControl.KeyInputValue keyInputValue in SkaldIO.ControllerInputControl.keyValueList)
			{
				keyInputValue.update();
			}
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x0006C9E4 File Offset: 0x0006ABE4
		public static void clear()
		{
			foreach (SkaldIO.ControllerInputControl.AxisInputValue axisInputValue in SkaldIO.ControllerInputControl.axisValueList)
			{
				axisInputValue.clear();
			}
			foreach (SkaldIO.ControllerInputControl.KeyInputValue keyInputValue in SkaldIO.ControllerInputControl.keyValueList)
			{
				keyInputValue.clear();
			}
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x0006CA74 File Offset: 0x0006AC74
		private static SkaldIO.ControllerInputControl.AxisInputValue createAxisValueContainer()
		{
			SkaldIO.ControllerInputControl.AxisInputValue axisInputValue = new SkaldIO.ControllerInputControl.AxisInputValue();
			SkaldIO.ControllerInputControl.axisValueList.Add(axisInputValue);
			return axisInputValue;
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x0006CA94 File Offset: 0x0006AC94
		private static SkaldIO.ControllerInputControl.KeyInputValue createButtonValueContainer(KeyCode key)
		{
			SkaldIO.ControllerInputControl.KeyInputValue keyInputValue = new SkaldIO.ControllerInputControl.KeyInputValue(key);
			SkaldIO.ControllerInputControl.keyValueList.Add(keyInputValue);
			return keyInputValue;
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x0006CAB4 File Offset: 0x0006ACB4
		public static bool isAnyControllerButtonPressed()
		{
			return SkaldIO.ControllerInputControl.getDpadPosition().x != 0f || SkaldIO.ControllerInputControl.getDpadPosition().y != 0f || SkaldIO.ControllerInputControl.getLeftJoystickPosition().x != 0f || SkaldIO.ControllerInputControl.getLeftJoystickPosition().y != 0f;
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x0006CB0A File Offset: 0x0006AD0A
		public static Vector2 getRightJoystickPosition()
		{
			return new Vector2(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2"));
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x0006CB25 File Offset: 0x0006AD25
		private static Vector2 getLeftJoystickPosition()
		{
			return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x0006CB40 File Offset: 0x0006AD40
		public static bool buttonAPressed()
		{
			return SkaldIO.ControllerInputControl.buttonA.wasPressedThisTurn();
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x0006CB4C File Offset: 0x0006AD4C
		public static bool buttonBPressed()
		{
			return SkaldIO.ControllerInputControl.buttonB.wasPressedThisTurn();
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x0006CB58 File Offset: 0x0006AD58
		public static bool buttonXPressed()
		{
			return SkaldIO.ControllerInputControl.buttonX.wasPressedThisTurn();
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x0006CB64 File Offset: 0x0006AD64
		public static bool buttonYPressed()
		{
			return SkaldIO.ControllerInputControl.buttonY.wasPressedThisTurn();
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x0006CB70 File Offset: 0x0006AD70
		public static bool rightStickPressed()
		{
			return SkaldIO.ControllerInputControl.buttonRightStick.wasPressedThisTurn();
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x0006CB7C File Offset: 0x0006AD7C
		public static bool leftStickPressed()
		{
			return SkaldIO.ControllerInputControl.buttonLeftStick.wasPressedThisTurn();
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x0006CB88 File Offset: 0x0006AD88
		public static bool rightBumperPressed()
		{
			return SkaldIO.ControllerInputControl.buttonRightBumper.wasPressedThisTurn();
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x0006CB94 File Offset: 0x0006AD94
		public static bool leftBumperPressed()
		{
			return SkaldIO.ControllerInputControl.buttonLeftBumper.wasPressedThisTurn();
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x0006CBA0 File Offset: 0x0006ADA0
		public static bool backButtonPressed()
		{
			return SkaldIO.ControllerInputControl.buttonBack.wasPressedThisTurn();
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x0006CBAC File Offset: 0x0006ADAC
		public static bool menuButtonPressed()
		{
			return SkaldIO.ControllerInputControl.buttonMenu.wasPressedThisTurn();
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x0006CBB8 File Offset: 0x0006ADB8
		public static bool leftTriggerPressed()
		{
			return SkaldIO.ControllerInputControl.leftTrigger.wasPressedThisTurn();
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x0006CBC4 File Offset: 0x0006ADC4
		public static bool rightTriggerPressed()
		{
			return SkaldIO.ControllerInputControl.rightTrigger.wasPressedThisTurn();
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x0006CBD0 File Offset: 0x0006ADD0
		public static bool openConsoleCommands()
		{
			if (SkaldIO.ControllerInputControl.isDpadDownPressed())
			{
				SkaldIO.ControllerInputControl.dpadDown.clear();
				return true;
			}
			return false;
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x0006CBE6 File Offset: 0x0006ADE6
		public static bool leftTriggerUp()
		{
			return SkaldIO.ControllerInputControl.leftTrigger.wasReleasedThisTurn();
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x0006CBF2 File Offset: 0x0006ADF2
		public static bool rightTriggerUp()
		{
			return SkaldIO.ControllerInputControl.rightTrigger.wasReleasedThisTurn();
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x0006CBFE File Offset: 0x0006ADFE
		public static bool leftTriggerHeld()
		{
			return SkaldIO.ControllerInputControl.leftTrigger.isDown();
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x0006CC0A File Offset: 0x0006AE0A
		public static bool rightTriggerHeld()
		{
			return SkaldIO.ControllerInputControl.rightTrigger.isDown();
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x0006CC16 File Offset: 0x0006AE16
		public static bool isLeftStickUpHeld()
		{
			return SkaldIO.ControllerInputControl.isControllerConnected() && SkaldIO.ControllerInputControl.leftStickUp.isDown();
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x0006CC2B File Offset: 0x0006AE2B
		public static bool isLeftStickDownHeld()
		{
			return SkaldIO.ControllerInputControl.isControllerConnected() && SkaldIO.ControllerInputControl.leftStickDown.isDown();
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x0006CC40 File Offset: 0x0006AE40
		public static bool isLeftStickRightHeld()
		{
			return SkaldIO.ControllerInputControl.isControllerConnected() && SkaldIO.ControllerInputControl.leftStickRight.isDown();
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x0006CC55 File Offset: 0x0006AE55
		public static bool isLeftStickLeftHeld()
		{
			return SkaldIO.ControllerInputControl.isControllerConnected() && SkaldIO.ControllerInputControl.leftStickLeft.isDown();
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x0006CC6A File Offset: 0x0006AE6A
		public static bool isLeftStickUpPressed()
		{
			return SkaldIO.ControllerInputControl.isControllerConnected() && SkaldIO.ControllerInputControl.leftStickUp.wasPressedThisTurn();
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x0006CC7F File Offset: 0x0006AE7F
		public static bool isLeftStickDownPressed()
		{
			return SkaldIO.ControllerInputControl.isControllerConnected() && SkaldIO.ControllerInputControl.leftStickDown.wasPressedThisTurn();
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x0006CC94 File Offset: 0x0006AE94
		public static bool isLeftStickRightPressed()
		{
			return SkaldIO.ControllerInputControl.isControllerConnected() && SkaldIO.ControllerInputControl.leftStickRight.wasPressedThisTurn();
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x0006CCA9 File Offset: 0x0006AEA9
		public static bool isLeftStickLeftPressed()
		{
			return SkaldIO.ControllerInputControl.leftStickLeft.wasPressedThisTurn();
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x0006CCB5 File Offset: 0x0006AEB5
		public static bool isDpadUpHeld()
		{
			return SkaldIO.ControllerInputControl.dpadUp.isDown();
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x0006CCC1 File Offset: 0x0006AEC1
		public static bool isDpadDownHeld()
		{
			return SkaldIO.ControllerInputControl.dpadDown.isDown();
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x0006CCCD File Offset: 0x0006AECD
		public static bool isDpadRightHeld()
		{
			return SkaldIO.ControllerInputControl.dpadRight.isDown();
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x0006CCD9 File Offset: 0x0006AED9
		public static bool isDpadLeftHeld()
		{
			return SkaldIO.ControllerInputControl.dpadLeft.isDown();
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x0006CCE5 File Offset: 0x0006AEE5
		public static bool isDpadUpPressed()
		{
			return SkaldIO.ControllerInputControl.dpadUp.wasPressedThisTurn();
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x0006CCF1 File Offset: 0x0006AEF1
		public static bool isDpadDownPressed()
		{
			return SkaldIO.ControllerInputControl.dpadDown.wasPressedThisTurn();
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x0006CCFD File Offset: 0x0006AEFD
		public static bool isDpadRightPressed()
		{
			return SkaldIO.ControllerInputControl.dpadRight.wasPressedThisTurn();
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x0006CD09 File Offset: 0x0006AF09
		public static bool isDpadLeftPressed()
		{
			return SkaldIO.ControllerInputControl.dpadLeft.wasPressedThisTurn();
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x0006CD15 File Offset: 0x0006AF15
		private static Vector2 getDpadPosition()
		{
			return new Vector2(Input.GetAxis("Horizontal3"), Input.GetAxis("Vertical3"));
		}

		// Token: 0x04000890 RID: 2192
		private static float leftStickThreshold = 0.5f;

		// Token: 0x04000891 RID: 2193
		private static float rightStickThreshold = 0.5f;

		// Token: 0x04000892 RID: 2194
		private static List<SkaldIO.ControllerInputControl.AxisInputValue> axisValueList = new List<SkaldIO.ControllerInputControl.AxisInputValue>();

		// Token: 0x04000893 RID: 2195
		private static SkaldIO.ControllerInputControl.AxisInputValue dpadUp = SkaldIO.ControllerInputControl.createAxisValueContainer();

		// Token: 0x04000894 RID: 2196
		private static SkaldIO.ControllerInputControl.AxisInputValue dpadDown = SkaldIO.ControllerInputControl.createAxisValueContainer();

		// Token: 0x04000895 RID: 2197
		private static SkaldIO.ControllerInputControl.AxisInputValue dpadRight = SkaldIO.ControllerInputControl.createAxisValueContainer();

		// Token: 0x04000896 RID: 2198
		private static SkaldIO.ControllerInputControl.AxisInputValue dpadLeft = SkaldIO.ControllerInputControl.createAxisValueContainer();

		// Token: 0x04000897 RID: 2199
		private static SkaldIO.ControllerInputControl.AxisInputValue leftStickUp = SkaldIO.ControllerInputControl.createAxisValueContainer();

		// Token: 0x04000898 RID: 2200
		private static SkaldIO.ControllerInputControl.AxisInputValue leftStickDown = SkaldIO.ControllerInputControl.createAxisValueContainer();

		// Token: 0x04000899 RID: 2201
		private static SkaldIO.ControllerInputControl.AxisInputValue leftStickRight = SkaldIO.ControllerInputControl.createAxisValueContainer();

		// Token: 0x0400089A RID: 2202
		private static SkaldIO.ControllerInputControl.AxisInputValue leftStickLeft = SkaldIO.ControllerInputControl.createAxisValueContainer();

		// Token: 0x0400089B RID: 2203
		private static SkaldIO.ControllerInputControl.AxisInputValue leftTrigger = SkaldIO.ControllerInputControl.createAxisValueContainer();

		// Token: 0x0400089C RID: 2204
		private static SkaldIO.ControllerInputControl.AxisInputValue rightTrigger = SkaldIO.ControllerInputControl.createAxisValueContainer();

		// Token: 0x0400089D RID: 2205
		private static List<SkaldIO.ControllerInputControl.KeyInputValue> keyValueList = new List<SkaldIO.ControllerInputControl.KeyInputValue>();

		// Token: 0x0400089E RID: 2206
		private static SkaldIO.ControllerInputControl.KeyInputValue buttonA = SkaldIO.ControllerInputControl.createButtonValueContainer(KeyCode.JoystickButton0);

		// Token: 0x0400089F RID: 2207
		private static SkaldIO.ControllerInputControl.KeyInputValue buttonB = SkaldIO.ControllerInputControl.createButtonValueContainer(KeyCode.JoystickButton1);

		// Token: 0x040008A0 RID: 2208
		private static SkaldIO.ControllerInputControl.KeyInputValue buttonX = SkaldIO.ControllerInputControl.createButtonValueContainer(KeyCode.JoystickButton2);

		// Token: 0x040008A1 RID: 2209
		private static SkaldIO.ControllerInputControl.KeyInputValue buttonY = SkaldIO.ControllerInputControl.createButtonValueContainer(KeyCode.JoystickButton3);

		// Token: 0x040008A2 RID: 2210
		private static SkaldIO.ControllerInputControl.KeyInputValue buttonRightStick = SkaldIO.ControllerInputControl.createButtonValueContainer(KeyCode.JoystickButton9);

		// Token: 0x040008A3 RID: 2211
		private static SkaldIO.ControllerInputControl.KeyInputValue buttonLeftStick = SkaldIO.ControllerInputControl.createButtonValueContainer(KeyCode.JoystickButton8);

		// Token: 0x040008A4 RID: 2212
		private static SkaldIO.ControllerInputControl.KeyInputValue buttonRightBumper = SkaldIO.ControllerInputControl.createButtonValueContainer(KeyCode.JoystickButton4);

		// Token: 0x040008A5 RID: 2213
		private static SkaldIO.ControllerInputControl.KeyInputValue buttonLeftBumper = SkaldIO.ControllerInputControl.createButtonValueContainer(KeyCode.JoystickButton5);

		// Token: 0x040008A6 RID: 2214
		private static SkaldIO.ControllerInputControl.KeyInputValue buttonBack = SkaldIO.ControllerInputControl.createButtonValueContainer(KeyCode.JoystickButton6);

		// Token: 0x040008A7 RID: 2215
		private static SkaldIO.ControllerInputControl.KeyInputValue buttonMenu = SkaldIO.ControllerInputControl.createButtonValueContainer(KeyCode.JoystickButton7);

		// Token: 0x020003CA RID: 970
		private abstract class InputValue
		{
			// Token: 0x06001D48 RID: 7496 RVA: 0x0007BAF0 File Offset: 0x00079CF0
			public void clear()
			{
				this.pressedThisTurn = false;
				this.releasedThisTurn = false;
			}

			// Token: 0x06001D49 RID: 7497 RVA: 0x0007BB00 File Offset: 0x00079D00
			public void update(bool buttonDown)
			{
				if (buttonDown)
				{
					if (!this.heldDown)
					{
						this.pressedThisTurn = true;
						this.heldDown = true;
						return;
					}
				}
				else if (this.heldDown && !this.pressedThisTurn)
				{
					this.heldDown = false;
					this.releasedThisTurn = true;
				}
			}

			// Token: 0x06001D4A RID: 7498 RVA: 0x0007BB3A File Offset: 0x00079D3A
			public bool isDown()
			{
				return this.heldDown;
			}

			// Token: 0x06001D4B RID: 7499 RVA: 0x0007BB42 File Offset: 0x00079D42
			public bool wasPressedThisTurn()
			{
				return this.pressedThisTurn;
			}

			// Token: 0x06001D4C RID: 7500 RVA: 0x0007BB4A File Offset: 0x00079D4A
			public bool wasReleasedThisTurn()
			{
				return this.releasedThisTurn;
			}

			// Token: 0x04000C52 RID: 3154
			protected bool heldDown;

			// Token: 0x04000C53 RID: 3155
			protected bool pressedThisTurn;

			// Token: 0x04000C54 RID: 3156
			protected bool releasedThisTurn;
		}

		// Token: 0x020003CB RID: 971
		private class AxisInputValue : SkaldIO.ControllerInputControl.InputValue
		{
		}

		// Token: 0x020003CC RID: 972
		private class KeyInputValue : SkaldIO.ControllerInputControl.InputValue
		{
			// Token: 0x06001D4F RID: 7503 RVA: 0x0007BB62 File Offset: 0x00079D62
			public KeyInputValue(KeyCode key)
			{
				this.key = key;
			}

			// Token: 0x06001D50 RID: 7504 RVA: 0x0007BB71 File Offset: 0x00079D71
			public void update()
			{
				base.update(Input.GetKey(this.key));
			}

			// Token: 0x04000C55 RID: 3157
			private KeyCode key;
		}
	}
}
