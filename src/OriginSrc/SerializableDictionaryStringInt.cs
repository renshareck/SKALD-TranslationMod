using System;
using System.Collections.Generic;

// Token: 0x0200017E RID: 382
[Serializable]
public class SerializableDictionaryStringInt
{
	// Token: 0x0600144F RID: 5199 RVA: 0x0005A6D5 File Offset: 0x000588D5
	public SerializableDictionaryStringInt()
	{
	}

	// Token: 0x06001450 RID: 5200 RVA: 0x0005A6E8 File Offset: 0x000588E8
	public SerializableDictionaryStringInt(Dictionary<string, int> dictionary)
	{
		this.buildPairList(dictionary);
	}

	// Token: 0x06001451 RID: 5201 RVA: 0x0005A704 File Offset: 0x00058904
	public void buildPairList(Dictionary<string, int> dictionary)
	{
		this.pairList = new List<SerializableDictionaryStringInt.Pair>();
		foreach (KeyValuePair<string, int> keyValuePair in dictionary)
		{
			this.addToDictionary(keyValuePair.Key, keyValuePair.Value);
		}
	}

	// Token: 0x06001452 RID: 5202 RVA: 0x0005A76C File Offset: 0x0005896C
	public void addToDictionary(string key, int value)
	{
		this.pairList.Add(new SerializableDictionaryStringInt.Pair(key, value));
	}

	// Token: 0x06001453 RID: 5203 RVA: 0x0005A780 File Offset: 0x00058980
	public Dictionary<string, int> getDictionary()
	{
		Dictionary<string, int> dictionary = new Dictionary<string, int>();
		foreach (SerializableDictionaryStringInt.Pair pair in this.pairList)
		{
			if (dictionary.ContainsKey(pair.getKey()))
			{
				dictionary[pair.getKey()] = pair.getValue();
			}
			else
			{
				dictionary.Add(pair.getKey(), pair.getValue());
			}
		}
		return dictionary;
	}

	// Token: 0x04000530 RID: 1328
	private List<SerializableDictionaryStringInt.Pair> pairList = new List<SerializableDictionaryStringInt.Pair>();

	// Token: 0x020002BA RID: 698
	[Serializable]
	private class Pair
	{
		// Token: 0x06001B4D RID: 6989 RVA: 0x00075D35 File Offset: 0x00073F35
		public Pair(string key, int value)
		{
			this.key = key;
			this.value = value;
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x00075D4B File Offset: 0x00073F4B
		public string getKey()
		{
			return this.key;
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x00075D53 File Offset: 0x00073F53
		public int getValue()
		{
			return this.value;
		}

		// Token: 0x04000A0D RID: 2573
		private string key;

		// Token: 0x04000A0E RID: 2574
		private int value;
	}
}
