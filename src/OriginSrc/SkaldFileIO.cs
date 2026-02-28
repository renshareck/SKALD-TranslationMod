using System;
using UnityEngine;

// Token: 0x0200017F RID: 383
public static class SkaldFileIO
{
	// Token: 0x06001454 RID: 5204 RVA: 0x0005A808 File Offset: 0x00058A08
	public static T Load<T>(string path) where T : Object
	{
		return Resources.Load<T>(path);
	}

	// Token: 0x06001455 RID: 5205 RVA: 0x0005A810 File Offset: 0x00058A10
	public static T[] LoadAll<T>(string path) where T : Object
	{
		return Resources.LoadAll<T>(path);
	}
}
