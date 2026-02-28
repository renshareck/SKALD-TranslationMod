using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

// Token: 0x02000174 RID: 372
public static class FunctionTools
{
	// Token: 0x0600140D RID: 5133 RVA: 0x0005882C File Offset: 0x00056A2C
	public static List<string> printMethods()
	{
		Type typeFromHandle = typeof(DataControl);
		List<string> list = new List<string>();
		string str = "";
		foreach (MethodInfo methodInfo in typeFromHandle.GetMethods())
		{
			if (methodInfo.IsPublic)
			{
				string text = "{" + methodInfo.Name;
				ParameterInfo[] parameters = methodInfo.GetParameters();
				if (parameters.Length != 0)
				{
					text += "|";
				}
				for (int j = 0; j < parameters.Length; j++)
				{
					text += parameters[j].Name;
					if (j != parameters.Length - 1)
					{
						text += ";";
					}
				}
				text += "}";
				text = text + " [" + methodInfo.ReturnType.Name + "]\n";
				list.Add(text);
			}
		}
		list.Sort();
		foreach (string str2 in list)
		{
			str += str2;
		}
		return list;
	}

	// Token: 0x0600140E RID: 5134 RVA: 0x00058968 File Offset: 0x00056B68
	public static void saveFunctionList()
	{
		FunctionTools.saveFunctionJSONList();
		List<string> list = FunctionTools.printMethods();
		string text = Application.persistentDataPath + "/Custom Projects/Skald Tools";
		Directory.CreateDirectory(text);
		using (TextWriter textWriter = new StreamWriter(text + "/MethodList.txt"))
		{
			foreach (string value in list)
			{
				textWriter.Write(value);
			}
		}
	}

	// Token: 0x0600140F RID: 5135 RVA: 0x00058A00 File Offset: 0x00056C00
	private static void saveFunctionJSONList()
	{
		Type typeFromHandle = typeof(DataControl);
		string text = Application.persistentDataPath + "/Custom Projects/Skald Tools";
		Directory.CreateDirectory(text);
		using (TextWriter textWriter = new StreamWriter(text + "/MethodList.json"))
		{
			textWriter.Write(JsonUtility.ToJson(new FunctionTools.MethodDataContainer(typeFromHandle.GetMethods()), true).ToString());
		}
	}

	// Token: 0x020002B3 RID: 691
	[Serializable]
	public class MethodDataContainer
	{
		// Token: 0x06001B30 RID: 6960 RVA: 0x000756EC File Offset: 0x000738EC
		public MethodDataContainer(MethodInfo[] methodList)
		{
			if (methodList == null || methodList.Length == 0)
			{
				return;
			}
			this.methods = new List<FunctionTools.MethodDataContainer.MethodInformation>();
			foreach (MethodInfo methodInfo in methodList)
			{
				if (methodInfo.IsPublic)
				{
					this.methods.Add(new FunctionTools.MethodDataContainer.MethodInformation(methodInfo));
				}
			}
		}

		// Token: 0x040009F5 RID: 2549
		public string name = "Test";

		// Token: 0x040009F6 RID: 2550
		public List<FunctionTools.MethodDataContainer.MethodInformation> methods;

		// Token: 0x020003E0 RID: 992
		[Serializable]
		public class MethodInformation
		{
			// Token: 0x06001D99 RID: 7577 RVA: 0x0007CD84 File Offset: 0x0007AF84
			public MethodInformation(MethodInfo methodInfo)
			{
				this.name = methodInfo.Name;
				this.parameters = new List<FunctionTools.MethodDataContainer.MethodInformation.ParamterInformation>();
				foreach (ParameterInfo p in methodInfo.GetParameters())
				{
					this.parameters.Add(new FunctionTools.MethodDataContainer.MethodInformation.ParamterInformation(p));
				}
				MainControl.log("Added method: " + JsonUtility.ToJson(this));
			}

			// Token: 0x04000C7E RID: 3198
			public string name;

			// Token: 0x04000C7F RID: 3199
			public List<FunctionTools.MethodDataContainer.MethodInformation.ParamterInformation> parameters;

			// Token: 0x02000434 RID: 1076
			[Serializable]
			public class ParamterInformation
			{
				// Token: 0x06001E07 RID: 7687 RVA: 0x0007DF8E File Offset: 0x0007C18E
				public ParamterInformation(ParameterInfo p)
				{
					this.name = p.Name;
					this.type = p.ParameterType.ToString();
				}

				// Token: 0x04000D9E RID: 3486
				public string name;

				// Token: 0x04000D9F RID: 3487
				public string type;
			}
		}
	}
}
