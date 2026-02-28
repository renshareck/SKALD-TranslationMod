using System;
using UnityEngine;

// Token: 0x0200019A RID: 410
[AddComponentMenu("Image Effects/CRT/Ultimate CRT")]
public class CRTEffect : BaseCRTEffect
{
	// Token: 0x0600156C RID: 5484 RVA: 0x0006071C File Offset: 0x0005E91C
	private void OnPreCull()
	{
		base.InternalPreRender();
	}

	// Token: 0x0600156D RID: 5485 RVA: 0x00060724 File Offset: 0x0005E924
	private void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		base.ProcessEffect(src, dest);
	}
}
