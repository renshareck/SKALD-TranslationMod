using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// Token: 0x02000191 RID: 401
public static class TextTools
{
	// Token: 0x060014BB RID: 5307 RVA: 0x0005C214 File Offset: 0x0005A414
	public static string formatFullDesription(string header, string body)
	{
		string text = "";
		if (header != "")
		{
			text = string.Concat(new string[]
			{
				text,
				C64Color.HEADER_TAG,
				header.ToUpper(),
				C64Color.HEADER_CLOSING_TAG,
				"\n\n"
			});
		}
		return text + body;
	}

	// Token: 0x060014BC RID: 5308 RVA: 0x0005C26E File Offset: 0x0005A46E
	public static string formateNameValuePairSoft(string name, int value)
	{
		return TextTools.formateNameValuePairSoft(name, value.ToString());
	}

	// Token: 0x060014BD RID: 5309 RVA: 0x0005C27D File Offset: 0x0005A47D
	public static string formateNameValuePairSoft(string name, string value)
	{
		return TextTools.addColor(name, C64Color.GRAY_LIGHT_TAG) + C64Color.ATTRIBUTE_VALUE_TAG + value + "</color>";
	}

	// Token: 0x060014BE RID: 5310 RVA: 0x0005C29A File Offset: 0x0005A49A
	public static string formateNameValuePairYellow(string name, int value)
	{
		return TextTools.formateNameValuePairYellow(name, value.ToString());
	}

	// Token: 0x060014BF RID: 5311 RVA: 0x0005C2A9 File Offset: 0x0005A4A9
	public static string formateNameValuePairYellow(string name, string value)
	{
		return TextTools.addColor(name, C64Color.YELLOW_TAG) + C64Color.ATTRIBUTE_VALUE_TAG + value + "</color>";
	}

	// Token: 0x060014C0 RID: 5312 RVA: 0x0005C2C6 File Offset: 0x0005A4C6
	public static string formateNameValuePair(string name, string value)
	{
		return TextTools.addColor(name, C64Color.ATTRIBUTE_NAME_TAG) + C64Color.ATTRIBUTE_VALUE_TAG + value + "</color>";
	}

	// Token: 0x060014C1 RID: 5313 RVA: 0x0005C2E3 File Offset: 0x0005A4E3
	public static string formateNameValuePair(string name, int value)
	{
		return TextTools.formateNameValuePair(name, value.ToString());
	}

	// Token: 0x060014C2 RID: 5314 RVA: 0x0005C2F2 File Offset: 0x0005A4F2
	public static string formateNameValuePairPlusMinus(string name, int value)
	{
		return TextTools.formateNameValuePair(name, TextTools.formatePlusMinus(value));
	}

	// Token: 0x060014C3 RID: 5315 RVA: 0x0005C300 File Offset: 0x0005A500
	public static string formatePlusMinus(float value)
	{
		string text = value.ToString();
		if (value >= 0f)
		{
			text = "+" + text;
		}
		return text;
	}

	// Token: 0x060014C4 RID: 5316 RVA: 0x0005C32C File Offset: 0x0005A52C
	public static string formatePlusMinus(int value)
	{
		string text = value.ToString();
		if (value >= 0)
		{
			text = "+" + text;
		}
		return text;
	}

	// Token: 0x060014C5 RID: 5317 RVA: 0x0005C352 File Offset: 0x0005A552
	public static string addColor(string s, string colorTag)
	{
		return colorTag + s + "</color>\t";
	}

	// Token: 0x060014C6 RID: 5318 RVA: 0x0005C360 File Offset: 0x0005A560
	public static string adjustStringLength(string s, int maxLenght)
	{
		if (s.Length <= maxLenght)
		{
			return s;
		}
		s = s.Substring(0, maxLenght);
		return s + ".";
	}

	// Token: 0x060014C7 RID: 5319 RVA: 0x0005C384 File Offset: 0x0005A584
	public static string purgeMarkup(string input)
	{
		if (input == "")
		{
			return input;
		}
		string[] array = input.Split(new char[]
		{
			'<'
		});
		if (array.Length == 1)
		{
			return array[0];
		}
		string text = "";
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			string[] array3 = array2[i].Split(new char[]
			{
				'>'
			});
			if (array3.Length == 1)
			{
				text += array3[0];
			}
			else
			{
				text += array3[1];
			}
		}
		return text;
	}

	// Token: 0x060014C8 RID: 5320 RVA: 0x0005C408 File Offset: 0x0005A608
	public static List<string> splitStringIntoList(string input, char sepparator = ',')
	{
		List<string> list = new List<string>();
		foreach (string item in input.Split(new char[]
		{
			sepparator
		}))
		{
			list.Add(item);
		}
		return list;
	}

	// Token: 0x060014C9 RID: 5321 RVA: 0x0005C446 File Offset: 0x0005A646
	public static string upperCaseEachWord(string s)
	{
		return Regex.Replace(s, "(^\\w)|(\\s\\w)", (Match m) => m.Value.ToUpper());
	}

	// Token: 0x060014CA RID: 5322 RVA: 0x0005C474 File Offset: 0x0005A674
	public static string printList(List<string> list)
	{
		string text = "";
		foreach (string str in list)
		{
			text = text + str + "\n";
		}
		return text;
	}

	// Token: 0x060014CB RID: 5323 RVA: 0x0005C4D0 File Offset: 0x0005A6D0
	public static string printListLine(List<string> list, string prefix = "")
	{
		string text = "";
		foreach (string str in list)
		{
			text = text + prefix + str + ", ";
		}
		text = TextTools.removeTrailingComma(text);
		return text;
	}

	// Token: 0x060014CC RID: 5324 RVA: 0x0005C534 File Offset: 0x0005A734
	public static string printListLineWithAnd(List<string> list)
	{
		if (list.Count < 2)
		{
			return TextTools.printListLine(list, "");
		}
		string text = "";
		for (int i = 0; i < list.Count; i++)
		{
			text += list[i];
			if (i < list.Count - 2)
			{
				text += ", ";
			}
			else if (i == list.Count - 2)
			{
				text += " and ";
			}
		}
		return text;
	}

	// Token: 0x060014CD RID: 5325 RVA: 0x0005C5AC File Offset: 0x0005A7AC
	public static string removeTrailingComma(string result)
	{
		if (result == "")
		{
			return result;
		}
		result = TextTools.removeTrailingWhiteSpace(result);
		if (result.Substring(result.Length - 1) == ",")
		{
			result = result.Substring(0, result.Length - 1);
		}
		return result;
	}

	// Token: 0x060014CE RID: 5326 RVA: 0x0005C5FC File Offset: 0x0005A7FC
	public static string removeTrailingWhiteSpace(string result)
	{
		if (result == "")
		{
			return result;
		}
		while (result.Substring(result.Length - 1) == " ")
		{
			result = result.Substring(0, result.Length - 1);
		}
		return result;
	}
}
