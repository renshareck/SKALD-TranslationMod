using System;
using UnityEngine;

// Token: 0x0200019B RID: 411
[AddComponentMenu("Image Effects/CRT/Ultimate CRT (standalone)")]
public class StandaloneCRTEffect : BaseCRTEffect
{
	// Token: 0x0600156F RID: 5487 RVA: 0x00060736 File Offset: 0x0005E936
	private void OnPreCull()
	{
		base.InternalPreRender();
	}

	// Token: 0x06001570 RID: 5488 RVA: 0x00060740 File Offset: 0x0005E940
	protected override RenderTexture CreateCameraTexture(RenderTexture currentCameraTexture)
	{
		RenderTexture renderTexture = base.CreateCameraTexture(currentCameraTexture);
		if (renderTexture != null)
		{
			return renderTexture;
		}
		return new RenderTexture(Screen.width, Screen.height, 0);
	}

	// Token: 0x06001571 RID: 5489 RVA: 0x00060770 File Offset: 0x0005E970
	protected override void OnCameraPostRender(Texture texture)
	{
		base.ProcessEffect(texture, null);
	}
}
