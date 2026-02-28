using System;
using System.Collections;
using System.IO;
using UnityEngine;

// Token: 0x0200017D RID: 381
public static class ScreenshotTool
{
	// Token: 0x0600144A RID: 5194 RVA: 0x0005A5A7 File Offset: 0x000587A7
	public static IEnumerator waitAndTakeScreenshot()
	{
		yield return new WaitForEndOfFrame();
		ScreenshotTool.takeScreenshot();
		yield break;
	}

	// Token: 0x0600144B RID: 5195 RVA: 0x0005A5AF File Offset: 0x000587AF
	public static byte[] forceTakeScreenShot()
	{
		return ScreenshotTool.takeScreenshot();
	}

	// Token: 0x0600144C RID: 5196 RVA: 0x0005A5B8 File Offset: 0x000587B8
	private static byte[] takeScreenshot()
	{
		byte[] array = ImageConversion.EncodeToPNG(ScreenshotTool.captureScreen());
		FileInfo fileInfo = new FileInfo(ScreenshotTool.getFullPath());
		try
		{
			fileInfo.Directory.Create();
			File.WriteAllBytes(fileInfo.FullName, array);
		}
		catch (Exception obj)
		{
			MainControl.logError(obj);
		}
		return array;
	}

	// Token: 0x0600144D RID: 5197 RVA: 0x0005A60C File Offset: 0x0005880C
	private static Texture2D captureScreen()
	{
		int width = Screen.width;
		int height = Screen.height;
		Texture2D texture2D = new Texture2D(width, height, TextureFormat.RGB24, false);
		texture2D.ReadPixels(new Rect(0f, 0f, (float)width, (float)height), 0, 0);
		try
		{
			Vector2 mousePosition = SkaldIO.getMousePosition();
			for (int i = -8; i < 8; i++)
			{
				for (int j = -8; j < 8; j++)
				{
					texture2D.SetPixel(Mathf.RoundToInt(mousePosition.x + (float)i), Mathf.RoundToInt(mousePosition.y + (float)j), Color.red);
				}
			}
		}
		catch
		{
			MainControl.logError("Could not mark mouse on screenshot");
		}
		texture2D.Apply();
		return texture2D;
	}

	// Token: 0x0600144E RID: 5198 RVA: 0x0005A6C4 File Offset: 0x000588C4
	private static string getFullPath()
	{
		return Application.persistentDataPath + "/screenShot.png";
	}
}
