using System;
using UnityEngine;

// Token: 0x02000065 RID: 101
public static class ScreenControl
{
	// Token: 0x060008D0 RID: 2256 RVA: 0x0002ABC6 File Offset: 0x00028DC6
	public static void enforceResolution()
	{
		if (ScreenControl.fullScreen)
		{
			ScreenControl.setFullScreen();
			return;
		}
		ScreenControl.setWindowed();
	}

	// Token: 0x060008D1 RID: 2257 RVA: 0x0002ABDA File Offset: 0x00028DDA
	public static void swapScreenMode()
	{
		if (ScreenControl.fullScreen)
		{
			ScreenControl.setWindowed();
			return;
		}
		ScreenControl.setFullScreen();
	}

	// Token: 0x060008D2 RID: 2258 RVA: 0x0002ABF0 File Offset: 0x00028DF0
	public static void setFullScreen()
	{
		ScreenControl.fullScreen = true;
		Screen.SetResolution(ScreenControl.getTargetFullWidth(), ScreenControl.getTargeFullHeight(), true);
		if (MainControl.debugFunctions)
		{
			MainControl.log(string.Concat(new string[]
			{
				"Enforced Resolution - FS: ",
				ScreenControl.getTargetFullWidth().ToString(),
				" / ",
				ScreenControl.getTargeFullHeight().ToString(),
				"\nCurrent: ",
				Screen.width.ToString(),
				" / ",
				Screen.height.ToString()
			}));
		}
	}

	// Token: 0x060008D3 RID: 2259 RVA: 0x0002AC8C File Offset: 0x00028E8C
	public static void setWindowed()
	{
		ScreenControl.fullScreen = false;
		Screen.SetResolution(ScreenControl.getTargetWindowWidth(), ScreenControl.getTargetWindowHeight(), false);
		if (MainControl.debugFunctions)
		{
			MainControl.log(string.Concat(new string[]
			{
				"Enforced Resolution - Window: ",
				ScreenControl.getTargetFullWidth().ToString(),
				" / ",
				ScreenControl.getTargeFullHeight().ToString(),
				"\nCurrent: ",
				Screen.width.ToString(),
				" / ",
				Screen.height.ToString()
			}));
		}
	}

	// Token: 0x060008D4 RID: 2260 RVA: 0x0002AD28 File Offset: 0x00028F28
	public static void updateResolution()
	{
		if (SkaldIO.getPressedEscapeKey() || SkaldIO.getKeyPressed(KeyCode.LeftAlt))
		{
			ScreenControl.enforceResolution();
			return;
		}
		if (!ScreenControl.fullScreen)
		{
			if (Screen.width != ScreenControl.getTargetWindowWidth() || Screen.height != ScreenControl.getTargetWindowHeight())
			{
				ScreenControl.enforceResolution();
				return;
			}
		}
		else if (Screen.width != ScreenControl.getTargetFullWidth() || Screen.height != ScreenControl.getTargeFullHeight())
		{
			ScreenControl.enforceResolution();
		}
	}

	// Token: 0x060008D5 RID: 2261 RVA: 0x0002AD90 File Offset: 0x00028F90
	public static float getCurrentScreenScale()
	{
		return (float)(Screen.height / 270);
	}

	// Token: 0x060008D6 RID: 2262 RVA: 0x0002AD9E File Offset: 0x00028F9E
	public static int getCurrentGameWidth()
	{
		if (ScreenControl.fullScreen)
		{
			return ScreenControl.getTargetFullWidth();
		}
		return ScreenControl.getTargetWindowWidth();
	}

	// Token: 0x060008D7 RID: 2263 RVA: 0x0002ADB2 File Offset: 0x00028FB2
	public static int getCurrentGameHeight()
	{
		if (ScreenControl.fullScreen)
		{
			return ScreenControl.getTargeFullHeight();
		}
		return ScreenControl.getTargetWindowHeight();
	}

	// Token: 0x060008D8 RID: 2264 RVA: 0x0002ADC6 File Offset: 0x00028FC6
	private static int getTargetWindowWidth()
	{
		return 480 * GlobalSettings.getDisplaySettings().getWindowScale();
	}

	// Token: 0x060008D9 RID: 2265 RVA: 0x0002ADD8 File Offset: 0x00028FD8
	private static int getTargetWindowHeight()
	{
		return 270 * GlobalSettings.getDisplaySettings().getWindowScale();
	}

	// Token: 0x060008DA RID: 2266 RVA: 0x0002ADEA File Offset: 0x00028FEA
	private static int getTargetFullWidth()
	{
		return Display.main.systemWidth;
	}

	// Token: 0x060008DB RID: 2267 RVA: 0x0002ADF6 File Offset: 0x00028FF6
	private static int getTargeFullHeight()
	{
		return Display.main.systemHeight;
	}

	// Token: 0x060008DC RID: 2268 RVA: 0x0002AE04 File Offset: 0x00029004
	public static string printResolutions()
	{
		string text = "--- Screen Resolutions---\nWidth - Height - Refresh\n";
		foreach (Resolution resolution in Screen.resolutions)
		{
			text = string.Concat(new string[]
			{
				text,
				resolution.width.ToString(),
				" x ",
				resolution.height.ToString(),
				": ",
				resolution.refreshRate.ToString(),
				"\n"
			});
		}
		return text;
	}

	// Token: 0x060008DD RID: 2269 RVA: 0x0002AE95 File Offset: 0x00029095
	public static void toggleVsync()
	{
		if (QualitySettings.vSyncCount == 1)
		{
			ScreenControl.vsyncOff();
			return;
		}
		ScreenControl.vsyncOn();
	}

	// Token: 0x060008DE RID: 2270 RVA: 0x0002AEAA File Offset: 0x000290AA
	public static void vsyncOff()
	{
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;
	}

	// Token: 0x060008DF RID: 2271 RVA: 0x0002AEB9 File Offset: 0x000290B9
	public static void vsyncOn()
	{
		QualitySettings.vSyncCount = 1;
	}

	// Token: 0x060008E0 RID: 2272 RVA: 0x0002AEC1 File Offset: 0x000290C1
	public static bool isVsyncOn()
	{
		return QualitySettings.vSyncCount > 0;
	}

	// Token: 0x060008E1 RID: 2273 RVA: 0x0002AECB File Offset: 0x000290CB
	public static string printVsyncOnOff()
	{
		if (ScreenControl.isVsyncOn())
		{
			return "On";
		}
		return "Off";
	}

	// Token: 0x060008E2 RID: 2274 RVA: 0x0002AEE0 File Offset: 0x000290E0
	public static string printVsyncValues()
	{
		return "" + "\nVsync Count: " + QualitySettings.vSyncCount.ToString() + "\nTarget FR: " + Application.targetFrameRate.ToString();
	}

	// Token: 0x04000226 RID: 550
	public const int GAME_WIDTH = 480;

	// Token: 0x04000227 RID: 551
	public const int GAME_HEIGHT = 270;

	// Token: 0x04000228 RID: 552
	private static bool fullScreen = true;
}
