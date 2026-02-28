using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using UnityEngine;

// Token: 0x0200017C RID: 380
public static class SkaldSaveControl
{
	// Token: 0x06001436 RID: 5174 RVA: 0x00059D1C File Offset: 0x00057F1C
	public static SkaldObjectList getSavePathContent()
	{
		string savePath = SkaldSaveControl.getSavePath(SkaldSaveControl.saveFilesFolder);
		SkaldObjectList skaldObjectList = new SkaldObjectList();
		try
		{
			string[] files = Directory.GetFiles(savePath);
			for (int i = 0; i < files.Length; i++)
			{
				string text = files[i].Replace(savePath, "");
				text = text.Replace(Path.DirectorySeparatorChar.ToString(), "");
				if (text.Contains(SkaldSaveControl.savesFileType))
				{
					text = text.Replace(SkaldSaveControl.savesFileType, "");
					string text2 = SkaldSaveControl.viewFileHeader(text);
					if (text2 != "")
					{
						skaldObjectList.add(new SkaldBaseObject(text, text, text2));
					}
					else
					{
						skaldObjectList.add(new SkaldBaseObject(text, text, "A save file!"));
					}
				}
			}
		}
		catch (Exception obj)
		{
			MainControl.logError(obj);
		}
		string lastSavedFile = SkaldSaveControl.getLastSavedFile();
		if (lastSavedFile != null)
		{
			skaldObjectList.setCurrentObject(skaldObjectList.getObject(lastSavedFile));
		}
		return skaldObjectList;
	}

	// Token: 0x06001437 RID: 5175 RVA: 0x00059E18 File Offset: 0x00058018
	public static bool isNameAValidFileName(string fileName)
	{
		return fileName.IndexOfAny(Path.GetInvalidFileNameChars()) < 0;
	}

	// Token: 0x06001438 RID: 5176 RVA: 0x00059E28 File Offset: 0x00058028
	public static void renameSave(string fileName, string newName)
	{
		try
		{
			if (fileName != newName)
			{
				newName = SkaldSaveControl.createSafeSaveName(newName);
			}
			fileName = Path.Combine(SkaldSaveControl.getSavePath(SkaldSaveControl.saveFilesFolder), fileName + SkaldSaveControl.savesFileType);
			newName = Path.Combine(SkaldSaveControl.getSavePath(SkaldSaveControl.saveFilesFolder), newName + SkaldSaveControl.savesFileType);
			File.Move(fileName, newName);
		}
		catch (Exception obj)
		{
			MainControl.logError(obj);
		}
	}

	// Token: 0x06001439 RID: 5177 RVA: 0x00059EA0 File Offset: 0x000580A0
	public static void deleteSave(string fileName)
	{
		try
		{
			fileName = Path.Combine(SkaldSaveControl.getSavePath(SkaldSaveControl.saveFilesFolder), fileName + SkaldSaveControl.savesFileType);
			File.Delete(fileName);
		}
		catch (Exception obj)
		{
			MainControl.logError(obj);
		}
	}

	// Token: 0x0600143A RID: 5178 RVA: 0x00059EE8 File Offset: 0x000580E8
	public static void copySave(string fileName)
	{
		try
		{
			string savePath = SkaldSaveControl.getSavePath(SkaldSaveControl.saveFilesFolder);
			string sourceFileName = Path.Combine(savePath, fileName + SkaldSaveControl.savesFileType);
			string str = SkaldSaveControl.createSafeSaveName(fileName);
			string destFileName = Path.Combine(savePath, str + SkaldSaveControl.savesFileType);
			File.Copy(sourceFileName, destFileName);
		}
		catch (Exception obj)
		{
			MainControl.logError(obj);
		}
	}

	// Token: 0x0600143B RID: 5179 RVA: 0x00059F4C File Offset: 0x0005814C
	private static string createSafeSaveName(string fileName)
	{
		foreach (string path in Directory.GetFiles(SkaldSaveControl.getSavePath(SkaldSaveControl.saveFilesFolder)))
		{
			if (fileName + SkaldSaveControl.savesFileType == Path.GetFileName(path))
			{
				fileName += "_copy";
				return SkaldSaveControl.createSafeSaveName(fileName);
			}
		}
		return fileName;
	}

	// Token: 0x0600143C RID: 5180 RVA: 0x00059FA8 File Offset: 0x000581A8
	public static bool settingsSave(SkaldSaveControl.SaveDataContainer saveDataObject)
	{
		return SkaldSaveControl.saveToFile(SkaldSaveControl.getSavePath(SkaldSaveControl.settingsFolder), SkaldSaveControl.settingsId, SkaldSaveControl.serialize(saveDataObject), "");
	}

	// Token: 0x0600143D RID: 5181 RVA: 0x00059FCC File Offset: 0x000581CC
	public static bool gameSave(string fileName, SkaldSaveControl.SaveDataContainer saveDataObject, string header)
	{
		SkaldSaveControl.saveLastSavedFile(fileName);
		fileName += SkaldSaveControl.savesFileType;
		if (SkaldSaveControl.saveThread != null && SkaldSaveControl.saveThread.IsAlive)
		{
			MainControl.logError("Save thread is busy!");
			return false;
		}
		string path = SkaldSaveControl.getSavePath(SkaldSaveControl.saveFilesFolder);
		byte[] data = SkaldSaveControl.serialize(saveDataObject);
		SkaldSaveControl.saveThread = new Thread(delegate()
		{
			SkaldSaveControl.saveToFile(path, fileName, data, header);
		});
		SkaldSaveControl.saveThread.Start();
		return true;
	}

	// Token: 0x0600143E RID: 5182 RVA: 0x0005A06A File Offset: 0x0005826A
	private static void saveLastSavedFile(string fileName)
	{
		File.WriteAllText(Path.Combine(SkaldSaveControl.getSavePath(SkaldSaveControl.settingsFolder), SkaldSaveControl.lastSaveId), fileName);
	}

	// Token: 0x0600143F RID: 5183 RVA: 0x0005A088 File Offset: 0x00058288
	public static string getLastSavedFile()
	{
		string result;
		try
		{
			string[] array = File.ReadAllLines(Path.Combine(SkaldSaveControl.getSavePath(SkaldSaveControl.settingsFolder), SkaldSaveControl.lastSaveId));
			if (array == null || array.Length == 0)
			{
				result = null;
			}
			else
			{
				result = array[0];
			}
		}
		catch
		{
			result = "";
		}
		return result;
	}

	// Token: 0x06001440 RID: 5184 RVA: 0x0005A0DC File Offset: 0x000582DC
	public static bool isLastSaveCached()
	{
		string lastSavedFile = SkaldSaveControl.getLastSavedFile();
		if (lastSavedFile == "")
		{
			return false;
		}
		try
		{
			if (File.Exists(Path.Combine(SkaldSaveControl.getSavePath(SkaldSaveControl.saveFilesFolder), lastSavedFile + SkaldSaveControl.savesFileType)))
			{
				return true;
			}
		}
		catch (Exception obj)
		{
			MainControl.logError(obj);
		}
		return false;
	}

	// Token: 0x06001441 RID: 5185 RVA: 0x0005A140 File Offset: 0x00058340
	private static byte[] serialize(SkaldSaveControl.SaveDataContainer saveDataObject)
	{
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		MemoryStream memoryStream = new MemoryStream();
		binaryFormatter.Serialize(memoryStream, saveDataObject);
		memoryStream.Seek(0L, SeekOrigin.Begin);
		memoryStream.Close();
		stopwatch.Stop();
		return memoryStream.ToArray();
	}

	// Token: 0x06001442 RID: 5186 RVA: 0x0005A188 File Offset: 0x00058388
	private static bool saveToFile(string path, string fileName, byte[] bytesToEncode, string header)
	{
		path = Path.Combine(path, fileName);
		bool result;
		try
		{
			string text = "";
			Stopwatch stopwatch = new Stopwatch();
			Stopwatch stopwatch2 = new Stopwatch();
			stopwatch.Start();
			stopwatch2.Start();
			string text2 = SkaldSaveControl.encodeData(bytesToEncode);
			text = text + "Encode to string: " + stopwatch.ElapsedMilliseconds.ToString() + "\n";
			stopwatch.Restart();
			if (header != null && header != "")
			{
				File.WriteAllLines(path, new List<string>
				{
					header + SkaldSaveControl.HEADER_TAIL,
					text2
				});
			}
			else
			{
				File.WriteAllText(path, text2);
			}
			stopwatch.Stop();
			text = text + "Write to HD: " + stopwatch.ElapsedMilliseconds.ToString() + "\n";
			stopwatch2.Stop();
			text = text + "TOTAL TIME: " + stopwatch2.ElapsedMilliseconds.ToString() + "\n";
			text = text + "Bytes: " + bytesToEncode.Length.ToString() + "\n";
			if (MainControl.debugFunctions)
			{
				MainControl.log("Save complete!\n" + text);
			}
			result = true;
		}
		catch (Exception ex)
		{
			MainControl.logError("Save failed: " + ex.Message);
			result = false;
		}
		return result;
	}

	// Token: 0x06001443 RID: 5187 RVA: 0x0005A2E8 File Offset: 0x000584E8
	public static SkaldSaveControl.SaveDataContainer settingsLoad()
	{
		if (SkaldSaveControl.saveThread != null && SkaldSaveControl.saveThread.IsAlive)
		{
			MainControl.logError("Save thread is busy!");
			return null;
		}
		SkaldSaveControl.SaveDataContainer result;
		try
		{
			string path = Path.Combine(SkaldSaveControl.getSavePath(SkaldSaveControl.settingsFolder), SkaldSaveControl.settingsId);
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			MemoryStream memoryStream = new MemoryStream(SkaldSaveControl.decodeData(File.ReadAllText(path)));
			SkaldSaveControl.SaveDataContainer saveDataContainer = (SkaldSaveControl.SaveDataContainer)binaryFormatter.Deserialize(memoryStream);
			memoryStream.Close();
			result = saveDataContainer;
		}
		catch (Exception ex)
		{
			MainControl.logWarning(ex.ToString());
			MainControl.logWarning(ex.Message);
			result = null;
		}
		return result;
	}

	// Token: 0x06001444 RID: 5188 RVA: 0x0005A380 File Offset: 0x00058580
	public static SkaldSaveControl.SaveDataContainer gameLoad(string fileName)
	{
		if (SkaldSaveControl.saveThread != null && SkaldSaveControl.saveThread.IsAlive)
		{
			MainControl.logError("Save thread is busy!");
			return null;
		}
		SkaldSaveControl.SaveDataContainer result;
		try
		{
			fileName += SkaldSaveControl.savesFileType;
			if (Path.GetFileName(fileName) != fileName)
			{
				throw new Exception("'fileName' is invalid!");
			}
			string path = Path.Combine(SkaldSaveControl.getSavePath(SkaldSaveControl.saveFilesFolder), fileName);
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			string[] array = File.ReadAllLines(path);
			MemoryStream memoryStream = new MemoryStream(SkaldSaveControl.decodeData(array[array.Length - 1]));
			SkaldSaveControl.SaveDataContainer saveDataContainer = (SkaldSaveControl.SaveDataContainer)binaryFormatter.Deserialize(memoryStream);
			memoryStream.Close();
			result = saveDataContainer;
		}
		catch (Exception ex)
		{
			MainControl.logError(ex);
			MainControl.logError(ex.Message);
			result = null;
		}
		return result;
	}

	// Token: 0x06001445 RID: 5189 RVA: 0x0005A43C File Offset: 0x0005863C
	public static string viewFileHeader(string fileName)
	{
		if (SkaldSaveControl.saveThread != null && SkaldSaveControl.saveThread.IsAlive)
		{
			MainControl.logError("Save thread is busy!");
			return "Could not load header for " + fileName;
		}
		string result;
		try
		{
			string path = Path.Combine(SkaldSaveControl.getSavePath(SkaldSaveControl.saveFilesFolder), fileName + SkaldSaveControl.savesFileType);
			string text = "";
			using (StreamReader streamReader = new StreamReader(path))
			{
				string text2;
				do
				{
					text2 = streamReader.ReadLine();
					text = text + text2 + "\n";
				}
				while (!text2.Contains(SkaldSaveControl.HEADER_TAIL));
			}
			if (text == "")
			{
				result = "No header found.";
			}
			else
			{
				result = text.Replace(SkaldSaveControl.HEADER_TAIL, "");
			}
		}
		catch (Exception ex)
		{
			MainControl.logError(ex);
			MainControl.logError(ex.Message);
			result = "Could not load header for " + fileName;
		}
		return result;
	}

	// Token: 0x06001446 RID: 5190 RVA: 0x0005A530 File Offset: 0x00058730
	public static string getSavePath(string folderName)
	{
		string text = Path.Combine(Application.persistentDataPath, folderName);
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		return text;
	}

	// Token: 0x06001447 RID: 5191 RVA: 0x0005A559 File Offset: 0x00058759
	private static byte[] decodeData(string input)
	{
		return Convert.FromBase64String(input);
	}

	// Token: 0x06001448 RID: 5192 RVA: 0x0005A561 File Offset: 0x00058761
	private static string encodeData(byte[] input)
	{
		return Convert.ToBase64String(input);
	}

	// Token: 0x04000529 RID: 1321
	private static string lastSaveId = "LastSave.txt";

	// Token: 0x0400052A RID: 1322
	private static string settingsId = "GlobalSettings.dat";

	// Token: 0x0400052B RID: 1323
	private static string savesFileType = ".SKS";

	// Token: 0x0400052C RID: 1324
	private static string saveFilesFolder = "SaveFiles";

	// Token: 0x0400052D RID: 1325
	private static string settingsFolder = "MetaData";

	// Token: 0x0400052E RID: 1326
	private static Thread saveThread;

	// Token: 0x0400052F RID: 1327
	public static string HEADER_TAIL = "<HEADER_END>";

	// Token: 0x020002B7 RID: 695
	[Serializable]
	public abstract class SaveDataContainer
	{
	}
}
