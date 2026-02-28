using System;
using System.Text;

// Token: 0x02000175 RID: 373
public static class LogicParser
{
	// Token: 0x06001410 RID: 5136 RVA: 0x00058A78 File Offset: 0x00056C78
	private static void debugError(string message)
	{
		if (LogicParser.debug)
		{
			MainControl.logError("LOGIC PARSER " + message);
		}
	}

	// Token: 0x06001411 RID: 5137 RVA: 0x00058A91 File Offset: 0x00056C91
	private static void debugMessage(string message)
	{
		if (LogicParser.debug)
		{
			MainControl.log("Logic parser message: " + message);
		}
	}

	// Token: 0x06001412 RID: 5138 RVA: 0x00058AAC File Offset: 0x00056CAC
	public static string parseCode(string input, SkaldBaseObject caller = null)
	{
		if (input == "")
		{
			return "";
		}
		if (input.IndexOf('#') == -1)
		{
			return input;
		}
		StringBuilder stringBuilder = new StringBuilder("");
		string[] array = input.Split(new string[]
		{
			LogicParser.ifToken
		}, StringSplitOptions.None);
		stringBuilder.Append(array[0]);
		for (int i = 1; i < array.Length; i++)
		{
			stringBuilder.Append(LogicParser.parseExpressions(LogicParser.ifToken + array[i], caller));
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06001413 RID: 5139 RVA: 0x00058B34 File Offset: 0x00056D34
	public static string parseExpressions(string input, SkaldBaseObject caller)
	{
		if (!input.Contains(LogicParser.ifToken))
		{
			return input;
		}
		string text = "";
		string text2 = "";
		string str = input.Split(new string[]
		{
			LogicParser.endToken
		}, StringSplitOptions.None)[1];
		string text3 = input.Split(new string[]
		{
			LogicParser.ifToken
		}, StringSplitOptions.None)[1];
		text3 = text3.Split(new char[]
		{
			'#'
		})[0];
		LogicParser.debugMessage("IF statement: " + text3);
		if (input.Contains(LogicParser.thenToken))
		{
			text = input.Split(new string[]
			{
				LogicParser.thenToken
			}, StringSplitOptions.None)[1];
			text = text.Split(new char[]
			{
				'#'
			})[0];
			LogicParser.debugMessage("THEN statement: " + text);
		}
		if (input.Contains(LogicParser.elseToken))
		{
			text2 = input.Split(new string[]
			{
				LogicParser.elseToken
			}, StringSplitOptions.None)[1];
			text2 = text2.Split(new char[]
			{
				'#'
			})[0];
			LogicParser.debugMessage("ELSE statement: " + text2);
		}
		string text4;
		if (text == "" && text2 == "")
		{
			if (LogicParser.evaluateExpression(text3, caller))
			{
				text4 = "True";
			}
			else
			{
				text4 = "False";
			}
		}
		else if (LogicParser.evaluateExpression(text3, caller))
		{
			if (text != "")
			{
				text4 = TextParser.processString(text, caller);
			}
			else
			{
				text4 = "";
			}
		}
		else if (text2 != "")
		{
			text4 = TextParser.processString(text2, caller);
		}
		else
		{
			text4 = "";
		}
		text4 += str;
		LogicParser.debugMessage("Output: " + text4);
		return text4;
	}

	// Token: 0x06001414 RID: 5140 RVA: 0x00058CD8 File Offset: 0x00056ED8
	private static bool evaluateExpression(string expression, SkaldBaseObject caller)
	{
		if (LogicParser.isTrue(expression))
		{
			return true;
		}
		LogicParser.debugMessage("Evaluating expression: " + expression);
		expression = LogicParser.resolveParenthesis(expression, caller);
		LogicParser.debugMessage("Expression after eval: " + expression);
		if (LogicParser.isTrue(expression))
		{
			return true;
		}
		if (expression.Contains(LogicParser.andOperator))
		{
			string[] array = expression.Split(new string[]
			{
				LogicParser.andOperator
			}, StringSplitOptions.None);
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].Trim();
			}
			array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				if (!LogicParser.isTrue(array2[i]))
				{
					return false;
				}
			}
			return true;
		}
		if (expression.Contains(LogicParser.orOperator))
		{
			string[] array3 = expression.Split(new string[]
			{
				LogicParser.orOperator
			}, StringSplitOptions.None);
			string[] array2 = array3;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].Trim();
			}
			array2 = array3;
			for (int i = 0; i < array2.Length; i++)
			{
				if (LogicParser.isTrue(array2[i]))
				{
					return true;
				}
			}
			return false;
		}
		if (expression.Contains(LogicParser.equalOperator))
		{
			string[] array4 = expression.Split(new string[]
			{
				LogicParser.equalOperator
			}, StringSplitOptions.None);
			string[] array2 = array4;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].Trim();
			}
			if (array4.Length < 2)
			{
				LogicParser.debugError("Malformed EQUAL operator for expression: " + expression);
			}
			return array4[0] == array4[1];
		}
		if (expression.Contains(LogicParser.notEqualOperator))
		{
			string[] array5 = expression.Split(new string[]
			{
				LogicParser.notEqualOperator
			}, StringSplitOptions.None);
			string[] array2 = array5;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].Trim();
			}
			if (array5.Length < 2)
			{
				LogicParser.debugError("Malformed NOTEQUAL operator for expression: " + expression);
			}
			return array5[0] != array5[1];
		}
		if (expression.Contains(LogicParser.greaterThanRightOperator))
		{
			string[] array6 = expression.Split(new string[]
			{
				LogicParser.greaterThanRightOperator
			}, StringSplitOptions.None);
			string[] array2 = array6;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].Trim();
			}
			if (array6.Length < 2)
			{
				LogicParser.debugError("Malformed GREATER THAN operator for expression: " + expression);
			}
			return LogicParser.testGreaterThan(array6[0], array6[1]);
		}
		if (expression.Contains(LogicParser.greaterThanLeftOperator))
		{
			string[] array7 = expression.Split(new string[]
			{
				LogicParser.greaterThanLeftOperator
			}, StringSplitOptions.None);
			string[] array2 = array7;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].Trim();
			}
			if (array7.Length < 2)
			{
				LogicParser.debugError("Malformed GREATER THAN operator for expression: " + expression);
			}
			return LogicParser.testGreaterThan(array7[1], array7[0]);
		}
		return false;
	}

	// Token: 0x06001415 RID: 5141 RVA: 0x00058F77 File Offset: 0x00057177
	private static bool isTrue(string term)
	{
		return term.ToUpper() == "TRUE";
	}

	// Token: 0x06001416 RID: 5142 RVA: 0x00058F8C File Offset: 0x0005718C
	private static bool testGreaterThan(string s1, string s2)
	{
		try
		{
			int num = int.Parse(s1);
			int num2 = int.Parse(s2);
			if (num > num2)
			{
				return true;
			}
		}
		catch
		{
			LogicParser.debugError(string.Concat(new string[]
			{
				"Malformed GREATER THAN operator for expression: ",
				s1,
				" > ",
				s2,
				". Non-number is present."
			}));
		}
		return false;
	}

	// Token: 0x06001417 RID: 5143 RVA: 0x00058FF8 File Offset: 0x000571F8
	private static string resolveParenthesis(string input, SkaldBaseObject caller)
	{
		if (input == "")
		{
			return "";
		}
		if (input.IndexOf(')') == -1)
		{
			return input;
		}
		int num = 1;
		StringBuilder stringBuilder = new StringBuilder("", input.Length);
		while (input[num] != ')')
		{
			if (input[num] == '(')
			{
				string str = LogicParser.resolveParenthesis(input.Substring(num), caller);
				input = input.Substring(0, num) + str;
			}
			else
			{
				stringBuilder.Append(input[num]);
				num++;
			}
		}
		string text = TextParser.resolveCurlyBraces(stringBuilder.ToString(), caller);
		text = TextParser.parseVariables(text);
		text = LogicParser.evaluateExpression(text, caller).ToString();
		stringBuilder.Clear();
		stringBuilder.Append(text);
		stringBuilder.Append(input.Substring(num + 1));
		return stringBuilder.ToString();
	}

	// Token: 0x04000510 RID: 1296
	private static bool debug = false;

	// Token: 0x04000511 RID: 1297
	private static string ifToken = "#IF";

	// Token: 0x04000512 RID: 1298
	private static string thenToken = "#THEN";

	// Token: 0x04000513 RID: 1299
	private static string elseToken = "#ELSE";

	// Token: 0x04000514 RID: 1300
	private static string endToken = "#END";

	// Token: 0x04000515 RID: 1301
	private static string equalOperator = "==";

	// Token: 0x04000516 RID: 1302
	private static string notEqualOperator = "!=";

	// Token: 0x04000517 RID: 1303
	private static string greaterThanRightOperator = ">";

	// Token: 0x04000518 RID: 1304
	private static string greaterThanLeftOperator = "<";

	// Token: 0x04000519 RID: 1305
	private static string andOperator = "&&";

	// Token: 0x0400051A RID: 1306
	private static string orOperator = "||";
}
