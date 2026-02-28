using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

// Token: 0x02000190 RID: 400
public static class TextParser
{
	// Token: 0x060014AE RID: 5294 RVA: 0x0005BC53 File Offset: 0x00059E53
	public static void setTieIn(DataControl tieIn)
	{
		TextParser.tieInClass = tieIn;
	}

	// Token: 0x060014AF RID: 5295 RVA: 0x0005BC5C File Offset: 0x00059E5C
	public static string[] getArray(string input)
	{
		string[] array = input.Split(new char[]
		{
			'&'
		});
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = TextParser.processString(array[i], null);
		}
		return array;
	}

	// Token: 0x060014B0 RID: 5296 RVA: 0x0005BC96 File Offset: 0x00059E96
	public static string processStringFromOrList(List<string> input, SkaldBaseObject caller = null)
	{
		if (input == null || input.Count == 0)
		{
			return "";
		}
		return TextParser.processString(input[Random.Range(0, input.Count)], caller);
	}

	// Token: 0x060014B1 RID: 5297 RVA: 0x0005BCC4 File Offset: 0x00059EC4
	public static string processString(string input, SkaldBaseObject caller = null)
	{
		if (string.IsNullOrEmpty(input))
		{
			return "";
		}
		input = LogicParser.parseCode(input, caller);
		input = TextParser.resolveCurlyBraces(input, caller);
		if (input.IndexOf('(') != -1 || input.IndexOf('/') != -1)
		{
			input = "(" + input + ")";
			input = TextParser.resolveParenthesis(input);
		}
		input = TextParser.parseVariables(input);
		input = TextParser.cleanString(input);
		return input;
	}

	// Token: 0x060014B2 RID: 5298 RVA: 0x0005BD34 File Offset: 0x00059F34
	private static string cleanString(string input)
	{
		input = input.Replace("\t", string.Empty);
		while (input.IndexOf("  ") > 0)
		{
			input = input.Replace("  ", " ");
		}
		input.Trim();
		while (input.IndexOf("\n") == input.Length - 3 && input.Length > 2)
		{
			input = input.Substring(0, input.Length - 3);
		}
		return input;
	}

	// Token: 0x060014B3 RID: 5299 RVA: 0x0005BDB0 File Offset: 0x00059FB0
	public static string resolveCurlyBraces(string input, SkaldBaseObject caller = null)
	{
		string[] array = input.Split(new char[]
		{
			'{'
		});
		if (array.Length == 1)
		{
			return input;
		}
		string text = "";
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].IndexOf('}') == -1)
			{
				text += array[i];
			}
			else
			{
				string[] array2 = array[i].Split(new char[]
				{
					'}'
				});
				array2[0] = TextParser.parseVariables(array2[0]);
				string function = array2[0];
				text += TextParser.callFunctionFromString(function, caller).getResultString();
				if (array2.Length > 1)
				{
					text += array2[1];
				}
			}
		}
		return text;
	}

	// Token: 0x060014B4 RID: 5300 RVA: 0x0005BE50 File Offset: 0x0005A050
	public static string parseVariables(string input)
	{
		if (!input.Contains("$"))
		{
			return input;
		}
		string[] array = input.Split(new char[]
		{
			'$'
		});
		StringBuilder stringBuilder = new StringBuilder();
		int i = 0;
		if (input[0] != '$')
		{
			stringBuilder.Append(array[0]);
			i = 1;
		}
		while (i < array.Length)
		{
			stringBuilder.Append(TextParser.parseVariable(array[i]));
			i++;
		}
		return stringBuilder.ToString();
	}

	// Token: 0x060014B5 RID: 5301 RVA: 0x0005BEC0 File Offset: 0x0005A0C0
	private static string parseVariable(string input)
	{
		if (input == "")
		{
			return "";
		}
		MainControl.log("Looking for variable in string: " + input);
		for (int i = 0; i < input.Length; i++)
		{
			if (!char.IsLetterOrDigit(input[i]))
			{
				string str = input.Substring(i);
				string text = input.Substring(0, i);
				MainControl.log("Found variable: " + text);
				text = TextParser.tieInClass.getVariable(text);
				return text + str;
			}
		}
		return TextParser.tieInClass.getVariable(input);
	}

	// Token: 0x060014B6 RID: 5302 RVA: 0x0005BF50 File Offset: 0x0005A150
	private static SkaldActionResult callFunctionFromString(string function, SkaldBaseObject caller)
	{
		Type type = TextParser.tieInClass.GetType();
		string[] array = TextParser.parseParameters(function);
		string name = TextParser.parseFunctionName(function);
		MethodInfo method = type.GetMethod(name);
		try
		{
			MethodBase methodBase = method;
			object obj = TextParser.tieInClass;
			object[] parameters = array;
			object obj2 = methodBase.Invoke(obj, parameters);
			if (obj2 == null)
			{
				return new SkaldActionResult(true, false, "", true);
			}
			if (obj2 is SkaldActionResult)
			{
				return obj2 as SkaldActionResult;
			}
			return new SkaldActionResult(true, false, obj2.ToString(), true);
		}
		catch (Exception ex)
		{
			if (!(ex is NullReferenceException))
			{
				string str = "Error on invoking ";
				string str2 = ": ";
				Exception ex2 = ex;
				MainControl.logError(str + function + str2 + ((ex2 != null) ? ex2.ToString() : null));
			}
		}
		if (caller == null)
		{
			return new SkaldActionResult(false, false, "", true);
		}
		method = caller.GetType().GetMethod(name);
		try
		{
			MethodBase methodBase2 = method;
			object[] parameters = array;
			object obj3 = methodBase2.Invoke(caller, parameters);
			if (obj3 == null)
			{
				return new SkaldActionResult(true, false, "", true);
			}
			if (obj3 is SkaldActionResult)
			{
				return obj3 as SkaldActionResult;
			}
			return new SkaldActionResult(true, false, obj3.ToString(), true);
		}
		catch (Exception ex)
		{
		}
		Exception ex;
		if (ex != null)
		{
			string str3 = "Error on invoking ";
			string str4 = ": ";
			Exception ex3 = ex;
			MainControl.log(str3 + function + str4 + ((ex3 != null) ? ex3.ToString() : null));
		}
		return new SkaldActionResult(false, false, "", true);
	}

	// Token: 0x060014B7 RID: 5303 RVA: 0x0005C0BC File Offset: 0x0005A2BC
	private static string[] parseParameters(string input)
	{
		if (input.IndexOf('|') == -1)
		{
			return null;
		}
		string[] array = input.Split(new char[]
		{
			'|'
		})[1].Split(new char[]
		{
			';'
		});
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = TextParser.processString(array[i], null);
		}
		return array;
	}

	// Token: 0x060014B8 RID: 5304 RVA: 0x0005C115 File Offset: 0x0005A315
	private static string parseFunctionName(string input)
	{
		if (input.IndexOf('|') == -1)
		{
			return input;
		}
		input = input.Split(new char[]
		{
			'|'
		})[0];
		return input;
	}

	// Token: 0x060014B9 RID: 5305 RVA: 0x0005C13C File Offset: 0x0005A33C
	private static string resolveParenthesis(string input)
	{
		if (input == "")
		{
			return "";
		}
		if (input.IndexOf(')') == -1 && input.IndexOf('/') == -1)
		{
			return input;
		}
		int num = 1;
		StringBuilder stringBuilder = new StringBuilder("", input.Length);
		while (input[num] != ')')
		{
			if (input[num] == '(')
			{
				string str = TextParser.resolveParenthesis(input.Substring(num));
				input = input.Substring(0, num) + str;
			}
			else
			{
				stringBuilder.Append(input[num]);
				num++;
			}
		}
		string value = TextParser.splitRandom(stringBuilder.ToString());
		stringBuilder.Clear();
		stringBuilder.Append(value);
		stringBuilder.Append(input.Substring(num + 1));
		return stringBuilder.ToString();
	}

	// Token: 0x060014BA RID: 5306 RVA: 0x0005C204 File Offset: 0x0005A404
	private static string splitRandom(string input)
	{
		return input;
	}

	// Token: 0x04000550 RID: 1360
	private static DataControl tieInClass;
}
