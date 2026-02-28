using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x02000031 RID: 49
public static class CombatLog
{
	// Token: 0x06000531 RID: 1329 RVA: 0x00018A82 File Offset: 0x00016C82
	public static List<SkaldBaseObject> getEntries()
	{
		return CombatLog.getLog().getObjectList();
	}

	// Token: 0x06000532 RID: 1330 RVA: 0x00018A8E File Offset: 0x00016C8E
	public static SkaldObjectList getLog()
	{
		if (CombatLog.log == null)
		{
			CombatLog.log = new SkaldObjectList();
			CombatLog.log.deactivateSorting();
		}
		return CombatLog.log;
	}

	// Token: 0x06000533 RID: 1331 RVA: 0x00018AB0 File Offset: 0x00016CB0
	public static void clearLog()
	{
		CombatLog.getLog().purgeList();
		File.Delete(CombatLog.logFilePath);
	}

	// Token: 0x06000534 RID: 1332 RVA: 0x00018AC6 File Offset: 0x00016CC6
	public static void addHeader(string name, string content)
	{
		CombatLog.addEntry(C64Color.CYAN_TAG + name.ToUpper() + "</color>", content);
	}

	// Token: 0x06000535 RID: 1333 RVA: 0x00018AE4 File Offset: 0x00016CE4
	public static void addEntry(string name, SkaldActionResult result)
	{
		if (!result.wasPerformed())
		{
			return;
		}
		if (!result.shouldBePostedInCombatLog())
		{
			return;
		}
		string text = result.getResultString();
		string text2 = result.getVerboseResultString();
		if (text == "" && text2 == "")
		{
			return;
		}
		if (text == "")
		{
			text = name + " acts";
		}
		if (text2 == "")
		{
			text2 = text;
		}
		CombatLog.addEntry(text, text2);
	}

	// Token: 0x06000536 RID: 1334 RVA: 0x00018B5B File Offset: 0x00016D5B
	public static void addEntry(string name)
	{
		CombatLog.addEntry(name, name);
	}

	// Token: 0x06000537 RID: 1335 RVA: 0x00018B64 File Offset: 0x00016D64
	public static void addEntry(string name, string content)
	{
		CombatLog.getLog().add(new CombatLog.Entry((CombatLog.getLog().getCount() + 1).ToString(), name, content));
		CombatLog.addToCommandBuffer(content);
	}

	// Token: 0x06000538 RID: 1336 RVA: 0x00018BA0 File Offset: 0x00016DA0
	private static void addToCommandBuffer(string s)
	{
		if (s == "")
		{
			return;
		}
		s = TextTools.purgeMarkup(s);
		try
		{
			File.AppendAllLines(CombatLog.logFilePath, new List<string>
			{
				s + "\n"
			});
		}
		catch (Exception ex)
		{
			MainControl.logError(ex.Message);
		}
	}

	// Token: 0x04000142 RID: 322
	private static SkaldObjectList log = null;

	// Token: 0x04000143 RID: 323
	private static string logFilePath = Application.persistentDataPath + "/CombatLog.txt";

	// Token: 0x020001DF RID: 479
	private class Entry : SkaldBaseObject
	{
		// Token: 0x0600171E RID: 5918 RVA: 0x00066E99 File Offset: 0x00065099
		public Entry(string id, string name, string content) : base(id, name, content)
		{
		}
	}
}
