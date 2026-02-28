using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000173 RID: 371
public static class FeedbackTool
{
	// Token: 0x06001407 RID: 5127 RVA: 0x000585F5 File Offset: 0x000567F5
	public static IEnumerator sendFeedback(string userName, string message)
	{
		yield return new WaitForEndOfFrame();
		if (FeedbackTool.busy)
		{
			FeedbackTool.addToFeedbackBuffer("Busy!");
		}
		else
		{
			FeedbackTool.busy = true;
			FeedbackTool.addToFeedbackBuffer("Sending message. Wait for it!");
			byte[] file_bytes = ScreenshotTool.forceTakeScreenShot();
			DiscordPoster.sendMessage(userName, message + "`\n\n**Comment: **" + FeedbackTool.consoleInput, file_bytes);
			FeedbackTool.consoleInput = "";
		}
		yield break;
	}

	// Token: 0x06001408 RID: 5128 RVA: 0x0005860C File Offset: 0x0005680C
	public static void update()
	{
		if (FeedbackTool.takeInput)
		{
			if (Input.inputString == "\b")
			{
				if (FeedbackTool.consoleInput.Length != 0)
				{
					FeedbackTool.consoleInput = FeedbackTool.consoleInput.Substring(0, FeedbackTool.consoleInput.Length - 1);
					return;
				}
			}
			else if (FeedbackTool.consoleInput.Length < FeedbackTool.maxCommentLength)
			{
				FeedbackTool.consoleInput += Input.inputString;
			}
		}
	}

	// Token: 0x06001409 RID: 5129 RVA: 0x00058680 File Offset: 0x00056880
	private static string getPrompt()
	{
		string text = FeedbackTool.prompt + "\n";
		foreach (string str in FeedbackTool.feedbackBuffer)
		{
			text = text + "\n\t" + str;
		}
		text = text + "\n\nComment> " + FeedbackTool.consoleInput;
		text = string.Concat(new string[]
		{
			text,
			"\n",
			FeedbackTool.consoleInput.Length.ToString(),
			" / ",
			FeedbackTool.maxCommentLength.ToString(),
			" char."
		});
		return text;
	}

	// Token: 0x0600140A RID: 5130 RVA: 0x00058744 File Offset: 0x00056944
	public static void printFeedbackTool()
	{
		if (FeedbackTool.takeInput)
		{
			GUIStyle guistyle = new GUIStyle();
			guistyle.fontSize = 12;
			guistyle.normal.textColor = Color.black;
			GUI.Label(new Rect(19f, 39f, 300f, 30f), FeedbackTool.getPrompt(), guistyle);
			guistyle.normal.textColor = Color.green;
			GUI.Label(new Rect(20f, 40f, 300f, 30f), FeedbackTool.getPrompt(), guistyle);
		}
	}

	// Token: 0x0600140B RID: 5131 RVA: 0x000587CE File Offset: 0x000569CE
	public static void addToFeedbackBuffer(string input)
	{
		FeedbackTool.feedbackBuffer.Add(input);
		if (FeedbackTool.feedbackBuffer.Count > 5)
		{
			FeedbackTool.feedbackBuffer.RemoveAt(0);
		}
	}

	// Token: 0x0400050A RID: 1290
	public static bool takeInput = false;

	// Token: 0x0400050B RID: 1291
	private static string consoleInput = "";

	// Token: 0x0400050C RID: 1292
	private static string prompt = "You may auto-post a typo or minor bug report to the Skald Discord server using this tool.\nEnter a comment (optional), hover the mouse over the bug or typo you want to point out and press ENTER to send.\nPress ESCAPE to cancel.\n\nNOTE: If you wish to file a more complex report this should be done manually through the Steam or GOG forum or the game's Discord page.";

	// Token: 0x0400050D RID: 1293
	public static bool busy = false;

	// Token: 0x0400050E RID: 1294
	private static List<string> feedbackBuffer = new List<string>();

	// Token: 0x0400050F RID: 1295
	private static int maxCommentLength = 500;
}
