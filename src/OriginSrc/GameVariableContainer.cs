using System;
using System.Collections.Generic;

// Token: 0x0200003B RID: 59
[Serializable]
public class GameVariableContainer
{
	// Token: 0x060007CB RID: 1995 RVA: 0x00027474 File Offset: 0x00025674
	public GameVariableContainer()
	{
		MainControl.log("Initializing Variable Container");
		List<SKALDProjectData.Objects.VariableContainer.Variable> variableList = GameData.getVariableList();
		foreach (SKALDProjectData.Objects.VariableContainer.Variable variable in variableList)
		{
			this.addVariable(variable.id, variable.description);
		}
		MainControl.log("Completed Variable Container with " + variableList.Count.ToString() + " variables added.");
	}

	// Token: 0x060007CC RID: 1996 RVA: 0x00027514 File Offset: 0x00025714
	public string printVariables()
	{
		string text = "VARIABLES:\n";
		foreach (KeyValuePair<string, string> keyValuePair in this.variables)
		{
			text = string.Concat(new string[]
			{
				text,
				keyValuePair.Key,
				": ",
				keyValuePair.Value,
				"\n"
			});
		}
		return text;
	}

	// Token: 0x060007CD RID: 1997 RVA: 0x0002759C File Offset: 0x0002579C
	public string addVariable(string key, string value)
	{
		if (this.variables.ContainsKey(key))
		{
			this.variables[key] = value;
		}
		else
		{
			this.variables.Add(key, value);
		}
		return value;
	}

	// Token: 0x060007CE RID: 1998 RVA: 0x000275CC File Offset: 0x000257CC
	public string addVariableOnce(string key, string value)
	{
		if (this.variables.ContainsKey(key))
		{
			MainControl.logError(string.Concat(new string[]
			{
				"Attempted to inserted value ",
				value,
				" to ",
				key,
				". Value already exists."
			}));
			return this.getVariable(key);
		}
		return this.addVariable(key, value);
	}

	// Token: 0x060007CF RID: 1999 RVA: 0x00027627 File Offset: 0x00025827
	public string getVariable(string key)
	{
		if (this.variables.ContainsKey(key))
		{
			return this.variables[key];
		}
		return "";
	}

	// Token: 0x060007D0 RID: 2000 RVA: 0x0002764C File Offset: 0x0002584C
	public string variableAddition(string key, string ammount)
	{
		int num = int.Parse(this.getVariable(key));
		int num2 = int.Parse(ammount);
		string text = Convert.ToString(num + num2);
		this.addVariable(key, text);
		MainControl.log(string.Concat(new string[]
		{
			"Added ",
			ammount,
			" to ",
			key,
			". New value is ",
			text
		}));
		return ammount;
	}

	// Token: 0x060007D1 RID: 2001 RVA: 0x000276B2 File Offset: 0x000258B2
	public GameVariableContainer.GameVariableSaveData getSaveData()
	{
		return new GameVariableContainer.GameVariableSaveData(this.variables);
	}

	// Token: 0x060007D2 RID: 2002 RVA: 0x000276BF File Offset: 0x000258BF
	public void applySaveData(GameVariableContainer.GameVariableSaveData data)
	{
		this.variables = data.getData();
	}

	// Token: 0x0400018C RID: 396
	private Dictionary<string, string> variables = new Dictionary<string, string>();

	// Token: 0x020001ED RID: 493
	[Serializable]
	public class GameVariableSaveData
	{
		// Token: 0x06001765 RID: 5989 RVA: 0x00067F10 File Offset: 0x00066110
		public GameVariableSaveData(Dictionary<string, string> data)
		{
			this.variables = new Dictionary<string, string>();
			foreach (KeyValuePair<string, string> keyValuePair in data)
			{
				this.variables.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x00067F84 File Offset: 0x00066184
		public Dictionary<string, string> getData()
		{
			return this.variables;
		}

		// Token: 0x0400079A RID: 1946
		private Dictionary<string, string> variables;
	}
}
