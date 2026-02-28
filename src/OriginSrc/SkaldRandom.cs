using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000181 RID: 385
public static class SkaldRandom
{
	// Token: 0x0600145D RID: 5213 RVA: 0x0005A878 File Offset: 0x00058A78
	public static int range(int minInclusive, int maxExclusive)
	{
		return Random.Range(minInclusive, maxExclusive);
	}

	// Token: 0x0600145E RID: 5214 RVA: 0x0005A881 File Offset: 0x00058A81
	public static int range(List<object> list)
	{
		return Random.Range(0, list.Count);
	}

	// Token: 0x0600145F RID: 5215 RVA: 0x0005A88F File Offset: 0x00058A8F
	public static int range(object[] array)
	{
		return Random.Range(0, array.Length);
	}
}
