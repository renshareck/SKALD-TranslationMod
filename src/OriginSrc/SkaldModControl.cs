using System;
using System.IO;
using UnityEngine;

// Token: 0x02000069 RID: 105
public static class SkaldModControl
{
	// Token: 0x06000934 RID: 2356 RVA: 0x0002BEFC File Offset: 0x0002A0FC
	public static SkaldObjectList getModFolderContent()
	{
		string savePath = SkaldSaveControl.getSavePath(SkaldModControl.modFilesFolder);
		SkaldObjectList skaldObjectList = new SkaldObjectList();
		try
		{
			foreach (string text in Directory.GetDirectories(savePath))
			{
				string text2 = Path.Combine(text, "Data");
				if (Directory.Exists(text2))
				{
					string path = Path.Combine(text2, "Meta Data.json");
					if (File.Exists(path))
					{
						SkaldBaseObject skaldBaseObject = SkaldModControl.parseMetaData(text.Replace(savePath, ""), path);
						if (skaldBaseObject != null)
						{
							skaldObjectList.add(skaldBaseObject);
						}
					}
				}
			}
		}
		catch (Exception obj)
		{
			MainControl.logError(obj);
		}
		return skaldObjectList;
	}

	// Token: 0x06000935 RID: 2357 RVA: 0x0002BFA0 File Offset: 0x0002A1A0
	private static SkaldBaseObject parseMetaData(string id, string path)
	{
		if (!File.Exists(path))
		{
			Debug.LogError("Trying to Parse non Existant meta-data for Mod: " + path);
			return null;
		}
		SkaldBaseObject result;
		try
		{
			SKALDProjectData.MetaData metaData = JsonUtility.FromJson<SKALDProjectData.MetaData>(File.ReadAllText(path));
			result = new SkaldBaseObject(id, metaData.title, metaData.description);
		}
		catch (Exception obj)
		{
			MainControl.logError(obj);
			result = null;
		}
		return result;
	}

	// Token: 0x06000936 RID: 2358 RVA: 0x0002C004 File Offset: 0x0002A204
	public static string getCurrentModProjectPath()
	{
		string text = Application.persistentDataPath + "/Custom Projects/";
		Directory.CreateDirectory(text);
		return text + SkaldModControl.getCurrentModProjectFolder() + "/Data/";
	}

	// Token: 0x06000937 RID: 2359 RVA: 0x0002C02B File Offset: 0x0002A22B
	public static void debugLogProjectPath()
	{
		MainControl.log("Current mod project loaded from folder:" + SkaldModControl.getCurrentModProjectFolder());
	}

	// Token: 0x06000938 RID: 2360 RVA: 0x0002C041 File Offset: 0x0002A241
	public static string getCurrentModProjectFolder()
	{
		if (SkaldModControl.currentModProjectFolder != "")
		{
			return SkaldModControl.currentModProjectFolder;
		}
		return SkaldModControl.defaultModProjectFolder;
	}

	// Token: 0x06000939 RID: 2361 RVA: 0x0002C05F File Offset: 0x0002A25F
	public static void setCurrentModProjectFolder(string folderName)
	{
		SkaldModControl.currentModProjectFolder = folderName;
		MainControl.log("Setting new mod project folder name: " + folderName);
	}

	// Token: 0x0600093A RID: 2362 RVA: 0x0002C077 File Offset: 0x0002A277
	public static bool isFolderNameTheCurrentFolderName(string folderName)
	{
		return (folderName == "" && SkaldModControl.getCurrentModProjectFolder() == SkaldModControl.defaultModProjectFolder) || folderName == SkaldModControl.getCurrentModProjectFolder();
	}

	// Token: 0x0400024E RID: 590
	private static string modFilesFolder = "Custom Projects";

	// Token: 0x0400024F RID: 591
	private static string defaultModProjectFolder = "DevelopmentBuild";

	// Token: 0x04000250 RID: 592
	private static string currentModProjectFolder = "";
}
