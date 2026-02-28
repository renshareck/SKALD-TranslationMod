using System;
using UnityEngine;

// Token: 0x02000199 RID: 409
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class BaseCRTEffect : MonoBehaviour
{
	// Token: 0x06001551 RID: 5457 RVA: 0x0005E610 File Offset: 0x0005C810
	public static void SetupDefaultPreset(BaseCRTEffect effect)
	{
		effect.blurSize = 0.7f;
		effect.blurStrength = 0.6f;
		effect.bleedingSize = 0.75f;
		effect.bleedingStrength = 0.5f;
		effect.chromaticAberrationOffset = 1.25f;
		effect.RGBMaskIntensivity = 0.6f;
		effect.RGBMaskStrength = 0.6f;
		effect.RGBMaskBleeding = 0.1f;
		effect.colorNoiseMode = BaseCRTEffect.NoiseMode.Add;
		effect.colorNoiseStrength = 0.15f;
		effect.whiteNoiseMode = BaseCRTEffect.NoiseMode.Lighten;
		effect.whiteNoiseStrength = 0.25f;
		effect.darkestLevel = Color.black;
		effect.brightestLevel = Color.Lerp(Color.black, Color.white, 0.92156863f);
		effect.darkestColor = Color.Lerp(Color.black, Color.white, 0.15686275f);
		effect.brightestColor = Color.white;
		effect.brightness = 0.2f;
		effect.contrast = 0.1f;
		effect.saturation = -0.05f;
		effect.interferenceWidth = 25f;
		effect.interferenceSpeed = 3f;
		effect.interferenceStrength = 0f;
		effect.interferenceSplit = 0.25f;
		effect.maskMode = BaseCRTEffect.MaskMode.Dense;
		effect.maskStrength = 0.35f;
		effect.curvatureX = 0.6f;
		effect.curvatureY = 0.6f;
		effect.overscan = 0f;
		effect.vignetteSize = 0.35f;
		effect.vignetteStrength = 0.1f;
		effect.textureScaling = BaseCRTEffect.TextureScalingMode.AdjustForHeight;
		effect.scalingPolicy = BaseCRTEffect.TextureScalingPolicy.DownscaleOnly;
		effect.textureSize = 768;
	}

	// Token: 0x06001552 RID: 5458 RVA: 0x0005E794 File Offset: 0x0005C994
	public static void SetupStretchPreset(BaseCRTEffect effect)
	{
		effect.blurSize = 0f;
		effect.blurStrength = 0f;
		effect.bleedingSize = 0f;
		effect.bleedingStrength = 0f;
		effect.chromaticAberrationOffset = 0f;
		effect.RGBMaskIntensivity = 0f;
		effect.RGBMaskStrength = 0f;
		effect.RGBMaskBleeding = 0f;
		effect.colorNoiseMode = BaseCRTEffect.NoiseMode.Add;
		effect.colorNoiseStrength = 0f;
		effect.whiteNoiseMode = BaseCRTEffect.NoiseMode.Lighten;
		effect.whiteNoiseStrength = 0f;
		effect.darkestLevel = Color.black;
		effect.brightestLevel = Color.white;
		effect.darkestColor = Color.black;
		effect.brightestColor = Color.white;
		effect.brightness = 0f;
		effect.contrast = 0f;
		effect.saturation = --0f;
		effect.interferenceWidth = 25f;
		effect.interferenceSpeed = 3f;
		effect.interferenceStrength = 0f;
		effect.interferenceSplit = 0.25f;
		effect.maskMode = BaseCRTEffect.MaskMode.Dense;
		effect.maskStrength = 0f;
		effect.curvatureX = 0f;
		effect.curvatureY = 0f;
		effect.overscan = 0f;
		effect.vignetteSize = 0f;
		effect.vignetteStrength = 0f;
		effect.textureScaling = BaseCRTEffect.TextureScalingMode.AdjustForHeight;
		effect.scalingPolicy = BaseCRTEffect.TextureScalingPolicy.DownscaleOnly;
		effect.textureSize = 768;
	}

	// Token: 0x06001553 RID: 5459 RVA: 0x0005E8F8 File Offset: 0x0005CAF8
	public static void SetupSoftPreset(BaseCRTEffect effect)
	{
		effect.blurSize = 0.1f;
		effect.blurStrength = 0.5f;
		effect.bleedingSize = 0.5f;
		effect.bleedingStrength = 0.8f;
		effect.chromaticAberrationOffset = 0.5f;
		effect.RGBMaskIntensivity = 0.4f;
		effect.RGBMaskStrength = 0.4f;
		effect.RGBMaskBleeding = 0.1f;
		effect.colorNoiseMode = BaseCRTEffect.NoiseMode.Lighten;
		effect.colorNoiseStrength = 0.15f;
		effect.whiteNoiseMode = BaseCRTEffect.NoiseMode.Lighten;
		effect.whiteNoiseStrength = 0.1f;
		effect.darkestLevel = Color.black;
		effect.brightestLevel = Color.white;
		effect.darkestColor = Color.black;
		effect.brightestColor = Color.white;
		effect.brightness = -0.05f;
		effect.contrast = 0f;
		effect.saturation = 0.05f;
		effect.interferenceWidth = 25f;
		effect.interferenceSpeed = 3f;
		effect.interferenceStrength = 0f;
		effect.interferenceSplit = 0.25f;
		effect.maskMode = BaseCRTEffect.MaskMode.Thin;
		effect.maskStrength = 0.3f;
		effect.curvatureX = 0.3f;
		effect.curvatureY = 0.3f;
		effect.overscan = 0f;
		effect.vignetteSize = 0f;
		effect.vignetteStrength = 0f;
		effect.textureScaling = BaseCRTEffect.TextureScalingMode.AdjustForHeight;
		effect.scalingPolicy = BaseCRTEffect.TextureScalingPolicy.DownscaleOnly;
		effect.textureSize = 768;
	}

	// Token: 0x06001554 RID: 5460 RVA: 0x0005EA5C File Offset: 0x0005CC5C
	public static void SetupKitchenTVPreset(BaseCRTEffect effect)
	{
		effect.blurSize = 0.7f;
		effect.blurStrength = 0.6f;
		effect.bleedingSize = 0.75f;
		effect.bleedingStrength = 0.5f;
		effect.chromaticAberrationOffset = 1.25f;
		effect.RGBMaskIntensivity = 0.4f;
		effect.RGBMaskStrength = 0.4f;
		effect.RGBMaskBleeding = 0.1f;
		effect.colorNoiseMode = BaseCRTEffect.NoiseMode.Add;
		effect.colorNoiseStrength = 0.15f;
		effect.whiteNoiseMode = BaseCRTEffect.NoiseMode.Lighten;
		effect.whiteNoiseStrength = 0.15f;
		effect.darkestLevel = Color.black;
		effect.brightestLevel = Color.Lerp(Color.black, Color.white, 0.92156863f);
		effect.darkestColor = Color.Lerp(Color.black, Color.white, 0.09803922f);
		effect.brightestColor = Color.white;
		effect.brightness = 0.1f;
		effect.contrast = 0.1f;
		effect.saturation = -0.05f;
		effect.interferenceWidth = 25f;
		effect.interferenceSpeed = 3f;
		effect.interferenceStrength = 0f;
		effect.interferenceSplit = 0.25f;
		effect.maskMode = BaseCRTEffect.MaskMode.Dense;
		effect.maskStrength = 0.25f;
		effect.curvatureX = 0.3f;
		effect.curvatureY = 0.3f;
		effect.overscan = 0f;
		effect.vignetteSize = 0.35f;
		effect.vignetteStrength = 0.1f;
		effect.textureScaling = BaseCRTEffect.TextureScalingMode.AdjustForHeight;
		effect.scalingPolicy = BaseCRTEffect.TextureScalingPolicy.DownscaleOnly;
		effect.textureSize = 768;
	}

	// Token: 0x06001555 RID: 5461 RVA: 0x0005EBE0 File Offset: 0x0005CDE0
	public static void SetupMiniCRTPreset(BaseCRTEffect effect)
	{
		effect.blurSize = 0.8f;
		effect.blurStrength = 0.8f;
		effect.bleedingSize = 0.5f;
		effect.bleedingStrength = 1f;
		effect.chromaticAberrationOffset = 2.5f;
		effect.RGBMaskIntensivity = 0.8f;
		effect.RGBMaskStrength = 0.8f;
		effect.RGBMaskBleeding = 0.3f;
		effect.colorNoiseMode = BaseCRTEffect.NoiseMode.Add;
		effect.colorNoiseStrength = 0.25f;
		effect.whiteNoiseMode = BaseCRTEffect.NoiseMode.Lighten;
		effect.whiteNoiseStrength = 0.25f;
		effect.darkestLevel = Color.black;
		effect.brightestLevel = Color.Lerp(Color.black, Color.white, 0.88235295f);
		effect.darkestColor = Color.Lerp(Color.black, Color.white, 0.13725491f);
		effect.brightestColor = Color.white;
		effect.brightness = 0.3f;
		effect.contrast = 0.3f;
		effect.saturation = -0.1f;
		effect.interferenceWidth = 25f;
		effect.interferenceSpeed = 3f;
		effect.interferenceStrength = 0f;
		effect.interferenceSplit = 0.25f;
		effect.maskMode = BaseCRTEffect.MaskMode.Denser;
		effect.maskStrength = 1f;
		effect.curvatureX = 0.7f;
		effect.curvatureY = 0.7f;
		effect.overscan = 0f;
		effect.vignetteSize = 0.5f;
		effect.vignetteStrength = 0.425f;
		effect.textureScaling = BaseCRTEffect.TextureScalingMode.AdjustForHeight;
		effect.scalingPolicy = BaseCRTEffect.TextureScalingPolicy.DownscaleOnly;
		effect.textureSize = 768;
	}

	// Token: 0x06001556 RID: 5462 RVA: 0x0005ED64 File Offset: 0x0005CF64
	public static void SetupColorTVPreset(BaseCRTEffect effect)
	{
		effect.blurSize = 0.9f;
		effect.blurStrength = 0.6f;
		effect.bleedingSize = 0.85f;
		effect.bleedingStrength = 0.75f;
		effect.chromaticAberrationOffset = 1.75f;
		effect.RGBMaskIntensivity = 0.4f;
		effect.RGBMaskStrength = 0.4f;
		effect.RGBMaskBleeding = 0.1f;
		effect.colorNoiseMode = BaseCRTEffect.NoiseMode.Add;
		effect.colorNoiseStrength = 0.3f;
		effect.whiteNoiseMode = BaseCRTEffect.NoiseMode.Lighten;
		effect.whiteNoiseStrength = 0.2f;
		effect.darkestLevel = Color.black;
		effect.brightestLevel = Color.Lerp(Color.black, Color.white, 0.92156863f);
		effect.darkestColor = Color.Lerp(Color.black, Color.white, 0.13725491f);
		effect.brightestColor = new Color(0.9607843f, 1f, 1f);
		effect.brightness = 0f;
		effect.contrast = 0.2f;
		effect.saturation = 0.1f;
		effect.interferenceWidth = 25f;
		effect.interferenceSpeed = 3f;
		effect.interferenceStrength = 0f;
		effect.interferenceSplit = 0.25f;
		effect.maskMode = BaseCRTEffect.MaskMode.Denser;
		effect.maskStrength = 0.2f;
		effect.curvatureX = 0.5f;
		effect.curvatureY = 0.5f;
		effect.overscan = 0.1f;
		effect.vignetteSize = 0.4f;
		effect.vignetteStrength = 0.5f;
		effect.textureScaling = BaseCRTEffect.TextureScalingMode.AdjustForHeight;
		effect.scalingPolicy = BaseCRTEffect.TextureScalingPolicy.DownscaleOnly;
		effect.textureSize = 768;
	}

	// Token: 0x06001557 RID: 5463 RVA: 0x0005EEF8 File Offset: 0x0005D0F8
	public static void SetupOldTVPreset(BaseCRTEffect effect)
	{
		effect.blurSize = 0.9f;
		effect.blurStrength = 0.8f;
		effect.bleedingSize = 0.95f;
		effect.bleedingStrength = 0.95f;
		effect.chromaticAberrationOffset = 1.9f;
		effect.RGBMaskIntensivity = 0.7f;
		effect.RGBMaskStrength = 0.7f;
		effect.RGBMaskBleeding = 0.3f;
		effect.colorNoiseMode = BaseCRTEffect.NoiseMode.Add;
		effect.colorNoiseStrength = 0.5f;
		effect.whiteNoiseMode = BaseCRTEffect.NoiseMode.Darken;
		effect.whiteNoiseStrength = 0.55f;
		effect.darkestLevel = Color.black;
		effect.brightestLevel = Color.Lerp(Color.black, Color.white, 0.92156863f);
		effect.darkestColor = Color.Lerp(Color.black, Color.white, 0.13725491f);
		effect.brightestColor = new Color(0.9607843f, 1f, 1f);
		effect.brightness = 0f;
		effect.contrast = -0.1f;
		effect.saturation = -0.05f;
		effect.interferenceWidth = 35f;
		effect.interferenceSpeed = 2f;
		effect.interferenceStrength = 0.075f;
		effect.interferenceSplit = 0.25f;
		effect.maskMode = BaseCRTEffect.MaskMode.Thin;
		effect.maskStrength = 0.75f;
		effect.curvatureX = 0.625f;
		effect.curvatureY = 0.625f;
		effect.overscan = 0.1f;
		effect.vignetteSize = 0.4f;
		effect.vignetteStrength = 0.5f;
		effect.textureScaling = BaseCRTEffect.TextureScalingMode.AdjustForHeight;
		effect.scalingPolicy = BaseCRTEffect.TextureScalingPolicy.DownscaleOnly;
		effect.textureSize = 768;
	}

	// Token: 0x06001558 RID: 5464 RVA: 0x0005F08C File Offset: 0x0005D28C
	public static void SetupHighEndMonitorPreset(BaseCRTEffect effect)
	{
		effect.blurSize = 0.35f;
		effect.blurStrength = 0.5f;
		effect.bleedingSize = 0.5f;
		effect.bleedingStrength = 0.8f;
		effect.chromaticAberrationOffset = 0.5f;
		effect.RGBMaskIntensivity = 0.4f;
		effect.RGBMaskStrength = 0.4f;
		effect.RGBMaskBleeding = 0.1f;
		effect.colorNoiseMode = BaseCRTEffect.NoiseMode.Lighten;
		effect.colorNoiseStrength = 0.15f;
		effect.whiteNoiseMode = BaseCRTEffect.NoiseMode.Lighten;
		effect.whiteNoiseStrength = 0.1f;
		effect.darkestLevel = Color.black;
		effect.brightestLevel = Color.white;
		effect.darkestColor = Color.black;
		effect.brightestColor = Color.white;
		effect.brightness = 0.1f;
		effect.contrast = 0f;
		effect.saturation = 0.05f;
		effect.interferenceWidth = 25f;
		effect.interferenceSpeed = 3f;
		effect.interferenceStrength = 0f;
		effect.interferenceSplit = 0.25f;
		effect.maskMode = BaseCRTEffect.MaskMode.Thin;
		effect.maskStrength = 0.3f;
		effect.curvatureX = 0f;
		effect.curvatureY = 0f;
		effect.overscan = 0f;
		effect.vignetteSize = 0f;
		effect.vignetteStrength = 0f;
		effect.textureScaling = BaseCRTEffect.TextureScalingMode.AdjustForHeight;
		effect.scalingPolicy = BaseCRTEffect.TextureScalingPolicy.DownscaleOnly;
		effect.textureSize = 768;
	}

	// Token: 0x06001559 RID: 5465 RVA: 0x0005F1F0 File Offset: 0x0005D3F0
	public static void SetupArcadeDisplayPreset(BaseCRTEffect effect)
	{
		effect.blurSize = 0.5f;
		effect.blurStrength = 0.7f;
		effect.bleedingSize = 0.65f;
		effect.bleedingStrength = 0.8f;
		effect.chromaticAberrationOffset = 0.9f;
		effect.RGBMaskIntensivity = 0.4f;
		effect.RGBMaskStrength = 0.4f;
		effect.RGBMaskBleeding = 0.2f;
		effect.colorNoiseMode = BaseCRTEffect.NoiseMode.Lighten;
		effect.colorNoiseStrength = 0.15f;
		effect.whiteNoiseMode = BaseCRTEffect.NoiseMode.Lighten;
		effect.whiteNoiseStrength = 0.1f;
		effect.darkestLevel = Color.black;
		effect.brightestLevel = Color.white;
		effect.darkestColor = Color.black;
		effect.brightestColor = Color.white;
		effect.brightness = 0.1f;
		effect.contrast = 0.1f;
		effect.saturation = 0.1f;
		effect.interferenceWidth = 25f;
		effect.interferenceSpeed = 3f;
		effect.interferenceStrength = 0f;
		effect.interferenceSplit = 0.25f;
		effect.maskMode = BaseCRTEffect.MaskMode.Scanline;
		effect.maskStrength = 0.75f;
		effect.curvatureX = 0f;
		effect.curvatureY = 0f;
		effect.overscan = 0f;
		effect.vignetteSize = 0.3f;
		effect.vignetteStrength = 0.2f;
		effect.textureScaling = BaseCRTEffect.TextureScalingMode.AdjustForHeight;
		effect.scalingPolicy = BaseCRTEffect.TextureScalingPolicy.DownscaleOnly;
		effect.textureSize = 768;
	}

	// Token: 0x0600155A RID: 5466 RVA: 0x0005F354 File Offset: 0x0005D554
	public static void SetupBrokenBlackAndWhitePreset(BaseCRTEffect effect)
	{
		effect.blurSize = 0.9f;
		effect.blurStrength = 1f;
		effect.bleedingSize = 0.75f;
		effect.bleedingStrength = 0.9f;
		effect.chromaticAberrationOffset = 2.5f;
		effect.RGBMaskIntensivity = 0.6f;
		effect.RGBMaskStrength = 0.6f;
		effect.RGBMaskBleeding = 0.1f;
		effect.colorNoiseMode = BaseCRTEffect.NoiseMode.Add;
		effect.colorNoiseStrength = 0.75f;
		effect.whiteNoiseMode = BaseCRTEffect.NoiseMode.Lighten;
		effect.whiteNoiseStrength = 0.5f;
		effect.darkestLevel = Color.Lerp(Color.black, Color.white, 0.05882353f);
		effect.brightestLevel = Color.Lerp(Color.black, Color.white, 0.88235295f);
		effect.darkestColor = Color.Lerp(Color.black, Color.white, 0.23529412f);
		effect.brightestColor = Color.white;
		effect.brightness = 0f;
		effect.contrast = -0.2f;
		effect.saturation = -1f;
		effect.interferenceWidth = 85f;
		effect.interferenceSpeed = 2.5f;
		effect.interferenceStrength = 0.05f;
		effect.interferenceSplit = 0f;
		effect.maskMode = BaseCRTEffect.MaskMode.Denser;
		effect.maskStrength = 0.15f;
		effect.curvatureX = 0.6f;
		effect.curvatureY = 0.6f;
		effect.overscan = 0.4f;
		effect.vignetteSize = 0.75f;
		effect.vignetteStrength = 0.5f;
		effect.textureScaling = BaseCRTEffect.TextureScalingMode.AdjustForHeight;
		effect.scalingPolicy = BaseCRTEffect.TextureScalingPolicy.DownscaleOnly;
		effect.textureSize = 768;
	}

	// Token: 0x0600155B RID: 5467 RVA: 0x0005F4E8 File Offset: 0x0005D6E8
	public static void SetupGreenTerminalPreset(BaseCRTEffect effect)
	{
		effect.blurSize = 0.9f;
		effect.blurStrength = 1f;
		effect.bleedingSize = 0.8f;
		effect.bleedingStrength = 0.65f;
		effect.chromaticAberrationOffset = 0f;
		effect.RGBMaskIntensivity = 0.7f;
		effect.RGBMaskStrength = 0.7f;
		effect.RGBMaskBleeding = 0.2f;
		effect.colorNoiseMode = BaseCRTEffect.NoiseMode.Add;
		effect.colorNoiseStrength = 0f;
		effect.whiteNoiseMode = BaseCRTEffect.NoiseMode.Lighten;
		effect.whiteNoiseStrength = 0f;
		effect.darkestLevel = Color.Lerp(Color.black, Color.white, 0.039215688f);
		effect.brightestLevel = Color.Lerp(Color.black, Color.white, 0.8039216f);
		effect.darkestColor = new Color(0f, 0.11764706f, 0f);
		effect.brightestColor = new Color(0.09803922f, 1f, 0.09803922f);
		effect.brightness = 0.4f;
		effect.contrast = -0.1f;
		effect.saturation = -0.8f;
		effect.interferenceWidth = 300f;
		effect.interferenceSpeed = 25f;
		effect.interferenceStrength = 0.0035f;
		effect.interferenceSplit = 0f;
		effect.maskMode = BaseCRTEffect.MaskMode.DenseScanline;
		effect.maskStrength = 0.25f;
		effect.curvatureX = 0.55f;
		effect.curvatureY = 0.55f;
		effect.overscan = 0f;
		effect.vignetteSize = 0.35f;
		effect.vignetteStrength = 0.35f;
		effect.textureScaling = BaseCRTEffect.TextureScalingMode.AdjustForHeight;
		effect.scalingPolicy = BaseCRTEffect.TextureScalingPolicy.DownscaleOnly;
		effect.textureSize = 768;
	}

	// Token: 0x0600155C RID: 5468 RVA: 0x0005F688 File Offset: 0x0005D888
	public static void SetupYellowMonitorPreset(BaseCRTEffect effect)
	{
		effect.blurSize = 0.9f;
		effect.blurStrength = 0.6f;
		effect.bleedingSize = 0.85f;
		effect.bleedingStrength = 0.75f;
		effect.chromaticAberrationOffset = 1.75f;
		effect.RGBMaskIntensivity = 0.4f;
		effect.RGBMaskStrength = 0.4f;
		effect.RGBMaskBleeding = 0.1f;
		effect.colorNoiseMode = BaseCRTEffect.NoiseMode.Multiply;
		effect.colorNoiseStrength = 0.4f;
		effect.whiteNoiseMode = BaseCRTEffect.NoiseMode.Darken;
		effect.whiteNoiseStrength = 0.2f;
		effect.darkestLevel = Color.Lerp(Color.black, Color.white, 0.039215688f);
		effect.brightestLevel = Color.Lerp(Color.black, Color.white, 0.8039216f);
		effect.darkestColor = new Color(0.11764706f, 0.11764706f, 0f);
		effect.brightestColor = new Color(1f, 1f, 0.09803922f);
		effect.brightness = 0.5f;
		effect.contrast = -0.1f;
		effect.saturation = -1f;
		effect.interferenceWidth = 300f;
		effect.interferenceSpeed = 25f;
		effect.interferenceStrength = 0.0035f;
		effect.interferenceSplit = 0f;
		effect.maskMode = BaseCRTEffect.MaskMode.DenseScanline;
		effect.maskStrength = 0.25f;
		effect.curvatureX = 0.4f;
		effect.curvatureY = 0.4f;
		effect.overscan = 0f;
		effect.vignetteSize = 0.35f;
		effect.vignetteStrength = 0.35f;
		effect.textureScaling = BaseCRTEffect.TextureScalingMode.AdjustForHeight;
		effect.scalingPolicy = BaseCRTEffect.TextureScalingPolicy.DownscaleOnly;
		effect.textureSize = 768;
	}

	// Token: 0x0600155D RID: 5469 RVA: 0x0005F828 File Offset: 0x0005DA28
	protected void InternalPreRender()
	{
		RenderTexture renderTexture = this.CreateCameraTexture(this.cameraTarget);
		this.oldCameraTarget = this.mainCamera.targetTexture;
		this.mainCamera.targetTexture = renderTexture;
		if (renderTexture != this.cameraTarget)
		{
			this.cameraTarget = renderTexture;
			if (this.otherCameras != null && this.otherCameras.Length != 0)
			{
				foreach (Camera camera in this.otherCameras)
				{
					if (!(camera == null))
					{
						camera.targetTexture = this.cameraTarget;
						camera.Render();
					}
				}
			}
			if (this.oldCameraTarget != null)
			{
				this.oldCameraTarget.Release();
			}
		}
		this.OnCameraPreRender();
	}

	// Token: 0x0600155E RID: 5470 RVA: 0x0005F8D8 File Offset: 0x0005DAD8
	private void OnPostRender()
	{
		if (this.mainCamera.targetTexture != this.oldCameraTarget)
		{
			RenderTexture targetTexture = this.mainCamera.targetTexture;
			this.mainCamera.targetTexture = this.oldCameraTarget;
			this.OnCameraPostRender(targetTexture);
			targetTexture.DiscardContents();
		}
	}

	// Token: 0x0600155F RID: 5471 RVA: 0x0005F928 File Offset: 0x0005DB28
	private void Awake()
	{
		this.mainCamera = base.GetComponent<Camera>();
		this.blurMaterial = new Material(Shader.Find("CRT/Blur"));
		this.postProMaterial = new Material(Shader.Find("CRT/Postprocess"));
		this.finalPostProMaterial = new Material(Shader.Find("CRT/FinalPostprocess"));
	}

	// Token: 0x06001560 RID: 5472 RVA: 0x0005F980 File Offset: 0x0005DB80
	private void Update()
	{
		this.seconds += Time.deltaTime;
		if (this.predefinedModel != this.preset)
		{
			this.preset = this.predefinedModel;
			switch (this.preset)
			{
			case BaseCRTEffect.Preset.Stretch:
				BaseCRTEffect.SetupStretchPreset(this);
				return;
			case BaseCRTEffect.Preset.Custom:
				break;
			case BaseCRTEffect.Preset.Default:
				BaseCRTEffect.SetupDefaultPreset(this);
				return;
			case BaseCRTEffect.Preset.KitchenTV:
				BaseCRTEffect.SetupKitchenTVPreset(this);
				return;
			case BaseCRTEffect.Preset.MiniCRT:
				BaseCRTEffect.SetupMiniCRTPreset(this);
				return;
			case BaseCRTEffect.Preset.ColorTV:
				BaseCRTEffect.SetupColorTVPreset(this);
				return;
			case BaseCRTEffect.Preset.OldTV:
				BaseCRTEffect.SetupOldTVPreset(this);
				return;
			case BaseCRTEffect.Preset.HighEndMonitor:
				BaseCRTEffect.SetupHighEndMonitorPreset(this);
				return;
			case BaseCRTEffect.Preset.ArcadeDisplay:
				BaseCRTEffect.SetupArcadeDisplayPreset(this);
				return;
			case BaseCRTEffect.Preset.BrokenBlackAndWhite:
				BaseCRTEffect.SetupBrokenBlackAndWhitePreset(this);
				return;
			case BaseCRTEffect.Preset.GreenTerminal:
				BaseCRTEffect.SetupGreenTerminalPreset(this);
				return;
			case BaseCRTEffect.Preset.YellowMonitor:
				BaseCRTEffect.SetupYellowMonitorPreset(this);
				break;
			case BaseCRTEffect.Preset.Soft:
				BaseCRTEffect.SetupSoftPreset(this);
				return;
			default:
				return;
			}
		}
	}

	// Token: 0x06001561 RID: 5473 RVA: 0x0005FA54 File Offset: 0x0005DC54
	private void OnDisable()
	{
		this.mainCamera.targetTexture = this.oldCameraTarget;
		if (this.otherCameras != null && this.otherCameras.Length != 0)
		{
			foreach (Camera camera in this.otherCameras)
			{
				if (!(camera == null))
				{
					camera.targetTexture = null;
				}
			}
		}
		if (this.cameraTarget != null)
		{
			this.cameraTarget.Release();
		}
		this.cameraTarget = null;
	}

	// Token: 0x06001562 RID: 5474 RVA: 0x0005FACC File Offset: 0x0005DCCC
	protected virtual RenderTexture CreateCameraTexture(RenderTexture currentCameraTexture)
	{
		if (this.textureScaling == BaseCRTEffect.TextureScalingMode.Off || this.textureSize == 0)
		{
			return null;
		}
		float num = (float)Screen.width;
		float num2 = (float)Screen.height;
		int num3;
		int num4;
		if (this.textureScaling == BaseCRTEffect.TextureScalingMode.AdjustForHeight)
		{
			num3 = this.textureSize;
			num4 = Mathf.RoundToInt(num * (float)this.textureSize / num2);
		}
		else
		{
			num4 = this.textureSize;
			num3 = Mathf.RoundToInt(num2 * (float)this.textureSize / num);
		}
		if (this.scalingPolicy != BaseCRTEffect.TextureScalingPolicy.Always && (this.scalingPolicy != BaseCRTEffect.TextureScalingPolicy.DownscaleOnly || (num <= (float)num4 && num2 <= (float)num3)) && (this.scalingPolicy != BaseCRTEffect.TextureScalingPolicy.UpscaleOnly || (num >= (float)num4 && num2 >= (float)num3)))
		{
			return null;
		}
		if (currentCameraTexture == null || currentCameraTexture.width != num4 || currentCameraTexture.height != num3)
		{
			return new RenderTexture(num4, num3, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);
		}
		return currentCameraTexture;
	}

	// Token: 0x06001563 RID: 5475 RVA: 0x0005FB8B File Offset: 0x0005DD8B
	protected virtual void OnCameraPreRender()
	{
	}

	// Token: 0x06001564 RID: 5476 RVA: 0x0005FB8D File Offset: 0x0005DD8D
	protected virtual void OnCameraPostRender(Texture texture)
	{
	}

	// Token: 0x06001565 RID: 5477 RVA: 0x0005FB8F File Offset: 0x0005DD8F
	public void setPresetToStretch()
	{
		this.predefinedModel = BaseCRTEffect.Preset.Stretch;
	}

	// Token: 0x06001566 RID: 5478 RVA: 0x0005FB98 File Offset: 0x0005DD98
	protected void ProcessEffect(Texture src, RenderTexture dest)
	{
		RenderTexture temporary = RenderTexture.GetTemporary(src.width, src.height, 0);
		RenderTexture temporary2 = RenderTexture.GetTemporary(src.width, src.height, 0);
		float num = Mathf.Lerp(1E-05f, 1.99999f, this.blurSize);
		this.UpdateBlurKernel(num);
		float num2 = Mathf.Lerp(0.8f, 1.2f, (this.brightness + 1f) / 2f);
		float c = Mathf.Lerp(0.5f, 1.5f, (this.contrast + 1f) / 2f);
		float s = Mathf.Lerp(0f, 2f, (this.saturation + 1f) / 2f);
		this.UpdateColorMatrices(num2 - 1.5f, c, s);
		this.blurMaterial.SetFloat("pixelSizeX", (float)(1.0 / (double)src.width));
		this.blurMaterial.SetFloat("pixelSizeY", (float)(1.0 / (double)src.height));
		this.blurMaterial.SetFloat("blurSigma", num);
		this.blurMaterial.SetVector("blurKernel", new Vector4(this.blurKernel[0], this.blurKernel[1]));
		this.blurMaterial.SetFloat("blurZ", this.blurZ);
		this.postProMaterial.SetTexture("_BlurTex", temporary);
		this.postProMaterial.SetFloat("pixelSizeX", (float)(1.0 / (double)src.width));
		this.postProMaterial.SetFloat("pixelSizeY", (float)(1.0 / (double)src.height));
		this.postProMaterial.SetFloat("seconds", this.seconds);
		this.postProMaterial.SetFloat("blurStr", 1f - this.blurStrength);
		this.postProMaterial.SetFloat("bleedDist", this.bleedingSize);
		this.postProMaterial.SetFloat("bleedStr", this.bleedingStrength);
		this.postProMaterial.SetFloat("rgbMaskStr", Mathf.Lerp(0f, 0.3f, this.RGBMaskStrength));
		this.postProMaterial.SetFloat("rgbMaskSub", this.RGBMaskIntensivity);
		this.postProMaterial.SetFloat("rgbMaskSep", 1f - this.RGBMaskBleeding);
		this.postProMaterial.SetFloat("colorNoiseStr", Mathf.Lerp(0f, 0.4f, this.colorNoiseStrength));
		this.postProMaterial.SetInt("colorNoiseMode", (int)this.colorNoiseMode);
		this.postProMaterial.SetFloat("monoNoiseStr", Mathf.Lerp(0f, 0.4f, this.whiteNoiseStrength));
		this.postProMaterial.SetInt("monoNoiseMode", (int)this.whiteNoiseMode);
		this.postProMaterial.SetMatrix("colorMat", this.colorMat);
		this.postProMaterial.SetColor("minLevels", this.darkestLevel);
		this.postProMaterial.SetColor("maxLevels", this.brightestLevel);
		this.postProMaterial.SetColor("blackPoint", this.darkestColor);
		this.postProMaterial.SetColor("whitePoint", this.brightestColor);
		this.postProMaterial.SetFloat("interWidth", this.interferenceWidth);
		this.postProMaterial.SetFloat("interSpeed", this.interferenceSpeed);
		this.postProMaterial.SetFloat("interStr", this.interferenceStrength);
		this.postProMaterial.SetFloat("interSplit", this.interferenceSplit);
		this.postProMaterial.SetFloat("aberStr", -this.chromaticAberrationOffset);
		float num3 = Mathf.Lerp(0.25f, 0.45f, this.curvatureX);
		float num4 = Mathf.Lerp(0.25f, 0.45f, this.curvatureY);
		this.finalPostProMaterial.SetFloat("pixelSizeX", (float)(1.0 / (double)src.width));
		this.finalPostProMaterial.SetFloat("pixelSizeY", (float)(1.0 / (double)src.height));
		this.finalPostProMaterial.SetFloat("vignetteStr", this.vignetteStrength);
		this.finalPostProMaterial.SetFloat("vignetteSize", 1f - this.vignetteSize);
		this.finalPostProMaterial.SetFloat("maskStr", this.maskStrength / 10f);
		this.finalPostProMaterial.SetInt("maskMode", (int)this.maskMode);
		this.finalPostProMaterial.SetFloat("crtBendX", Mathf.Lerp(1f, 100f, (1f - num3) / Mathf.Exp(10f * num3)));
		this.finalPostProMaterial.SetFloat("crtBendY", Mathf.Lerp(1f, 100f, (1f - num4) / Mathf.Exp(10f * num4)));
		this.finalPostProMaterial.SetFloat("crtOverscan", Mathf.Lerp(0.1f, 0.25f, this.overscan));
		this.finalPostProMaterial.SetInt("flipV", (src.texelSize.y < 0f && this.cameraTarget != null) ? 1 : 0);
		Graphics.Blit(src, temporary, this.blurMaterial);
		Graphics.Blit(src, temporary2, this.postProMaterial);
		if (dest == null)
		{
			this.DrawFullScreenQuad(temporary2, this.finalPostProMaterial);
		}
		else
		{
			Graphics.Blit(temporary2, dest, this.finalPostProMaterial);
		}
		temporary.DiscardContents();
		temporary2.DiscardContents();
		RenderTexture.ReleaseTemporary(temporary);
		RenderTexture.ReleaseTemporary(temporary2);
	}

	// Token: 0x06001567 RID: 5479 RVA: 0x00060140 File Offset: 0x0005E340
	protected void DrawFullScreenQuad(Texture src, Material material)
	{
		RenderTexture.active = null;
		GL.PushMatrix();
		GL.LoadOrtho();
		GL.Viewport(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height));
		material.mainTexture = src;
		for (int i = 0; i < material.passCount; i++)
		{
			material.SetPass(i);
			GL.Begin(7);
			GL.Color(Color.white);
			GL.TexCoord2(0f, 0f);
			GL.Vertex3(0f, 0f, 0.1f);
			GL.TexCoord2(1f, 0f);
			GL.Vertex3(1f, 0f, 0.1f);
			GL.TexCoord2(1f, 1f);
			GL.Vertex3(1f, 1f, 0.1f);
			GL.TexCoord2(0f, 1f);
			GL.Vertex3(0f, 1f, 0.1f);
			GL.End();
		}
		GL.PopMatrix();
		material.mainTexture = null;
	}

	// Token: 0x06001568 RID: 5480 RVA: 0x00060250 File Offset: 0x0005E450
	protected float CalculateBlurWeight(float x, float sigma)
	{
		return 0.39894f * Mathf.Exp(-0.5f * x * x / (sigma * sigma)) / sigma;
	}

	// Token: 0x06001569 RID: 5481 RVA: 0x0006026C File Offset: 0x0005E46C
	protected void UpdateBlurKernel(float sigma)
	{
		if (sigma == this.blurSigma)
		{
			return;
		}
		this.blurSigma = sigma;
		this.blurZ = 0f;
		for (int i = 0; i <= 1; i++)
		{
			float num = this.CalculateBlurWeight((float)i, sigma);
			this.blurKernel[1 - i] = num;
			if (i > 0)
			{
				this.blurZ += 2f * num;
			}
			else
			{
				this.blurZ += num;
			}
		}
		this.blurZ *= this.blurZ;
	}

	// Token: 0x0600156A RID: 5482 RVA: 0x000602F4 File Offset: 0x0005E4F4
	protected void UpdateColorMatrices(float b, float c, float s)
	{
		bool flag = false;
		if (b != this.currentBrightness)
		{
			flag = true;
			this.currentBrightness = b;
			this.brightnessMat.SetColumn(0, new Vector4(1f, 0f, 0f, 0f));
			this.brightnessMat.SetColumn(1, new Vector4(0f, 1f, 0f, 0f));
			this.brightnessMat.SetColumn(2, new Vector4(0f, 0f, 1f, 0f));
			this.brightnessMat.SetColumn(3, new Vector4(b, b, b, 1f));
		}
		if (c != this.currentContrast)
		{
			flag = true;
			this.currentContrast = c;
			float num = (1f - this.contrast) / 2f;
			this.contrastMat.SetColumn(0, new Vector4(c, 0f, 0f, 0f));
			this.contrastMat.SetColumn(1, new Vector4(0f, c, 0f, 0f));
			this.contrastMat.SetColumn(2, new Vector4(0f, 0f, c, 0f));
			this.contrastMat.SetColumn(3, new Vector4(num, num, num, 1f));
		}
		if (s != this.currentSaturation)
		{
			flag = true;
			this.currentSaturation = s;
			Vector3 vector = new Vector3(0.3086f, 0.6094f, 0.082f);
			float num2 = 1f - s;
			Vector4 vector2 = new Vector4(vector.x * num2 + s, vector.x * num2, vector.x * num2, 0f);
			Vector4 vector3 = new Vector4(vector.y * num2, vector.y * num2 + s, vector.y * num2, 0f);
			Vector4 vector4 = new Vector4(vector.z * num2, vector.z * num2, vector.z * num2 + s, 0f);
			this.saturationMat.SetColumn(0, vector2);
			this.saturationMat.SetColumn(1, vector3);
			this.saturationMat.SetColumn(2, vector4);
			this.saturationMat.SetColumn(3, new Vector4(0f, 0f, 0f, 1f));
		}
		if (flag)
		{
			this.colorMat = this.brightnessMat * this.contrastMat * this.saturationMat;
		}
	}

	// Token: 0x04000582 RID: 1410
	public BaseCRTEffect.Preset predefinedModel = BaseCRTEffect.Preset.Custom;

	// Token: 0x04000583 RID: 1411
	protected BaseCRTEffect.Preset preset = BaseCRTEffect.Preset.Custom;

	// Token: 0x04000584 RID: 1412
	[Header("Blur")]
	[Range(0f, 1f)]
	[Tooltip("Blur ammount. How blurry the blurred layer is.")]
	public float blurSize = 0.7f;

	// Token: 0x04000585 RID: 1413
	[Range(0f, 1f)]
	[Tooltip("How much of the blurred image is mixed with the base image.")]
	public float blurStrength = 0.6f;

	// Token: 0x04000586 RID: 1414
	[Header("Luminosity bleeding")]
	[Range(0f, 2f)]
	[Tooltip("How many adjacent pixels is going to be overlaped by a given brighter pixel.")]
	public float bleedingSize = 0.75f;

	// Token: 0x04000587 RID: 1415
	[Range(0f, 1f)]
	[Tooltip("How much of the bleeded image is mixed with the base image.")]
	public float bleedingStrength = 0.5f;

	// Token: 0x04000588 RID: 1416
	[Header("Chromatic aberration")]
	[Range(-2.5f, 2.5f)]
	[Tooltip("How many pixels the blurred layer is shifted for Red and Blue channels.")]
	public float chromaticAberrationOffset = 1.25f;

	// Token: 0x04000589 RID: 1417
	[Header("RGB Mask")]
	[Range(0f, 1f)]
	[Tooltip("How much each channel blocks other channels for a given pixel. E.g. if set to 1.0 all red channel pixels will show no green or blue values.")]
	public float RGBMaskIntensivity = 0.6f;

	// Token: 0x0400058A RID: 1418
	[Range(0f, 1f)]
	[Tooltip("How much of the masked image is mixed with the base image.")]
	public float RGBMaskStrength = 0.6f;

	// Token: 0x0400058B RID: 1419
	[Range(0f, 1f)]
	[Tooltip("How much each channel passes through other channels for a given pixel. Basically opposite of RGB Mask Intensitity.")]
	public float RGBMaskBleeding = 0.1f;

	// Token: 0x0400058C RID: 1420
	[Header("Noise")]
	[Tooltip("What blending mode is used when mixing base image pixels with color noise pixels.")]
	public BaseCRTEffect.NoiseMode colorNoiseMode;

	// Token: 0x0400058D RID: 1421
	[Range(0f, 1f)]
	[Tooltip("How much of the generated color noise image is mixed with the base image.")]
	public float colorNoiseStrength = 0.15f;

	// Token: 0x0400058E RID: 1422
	[Space(4f)]
	[Tooltip("What blending mode is used when mixing base image pixels with white noise pixels.")]
	public BaseCRTEffect.NoiseMode whiteNoiseMode = BaseCRTEffect.NoiseMode.Lighten;

	// Token: 0x0400058F RID: 1423
	[Range(0f, 1f)]
	[Tooltip("How much of the generated white noise image is mixed with the base image.")]
	public float whiteNoiseStrength = 0.25f;

	// Token: 0x04000590 RID: 1424
	[Header("Color adjustments")]
	[Tooltip("This color becomes the new darkest color (works similarly to Photoshop 'Levels' adjustment).")]
	public Color darkestLevel = Color.black;

	// Token: 0x04000591 RID: 1425
	[Tooltip("This color becomes the new brightest color (works similarly to Photoshop 'Levels' adjustment).")]
	public Color brightestLevel = Color.Lerp(Color.black, Color.white, 0.92156863f);

	// Token: 0x04000592 RID: 1426
	[Space(4f)]
	[Tooltip("Darkest color of the output image, makes image brigther and contrast lower, if brighter than black.")]
	public Color darkestColor = Color.Lerp(Color.black, Color.white, 0.15686275f);

	// Token: 0x04000593 RID: 1427
	[Tooltip("Brightest color of the output image, makes image darker and contrast lower if darker than white.")]
	public Color brightestColor = Color.white;

	// Token: 0x04000594 RID: 1428
	[Space(4f)]
	[Range(-1f, 1f)]
	[Tooltip("Brightness adjustment.")]
	public float brightness = 0.2f;

	// Token: 0x04000595 RID: 1429
	[Range(-1f, 1f)]
	[Tooltip("Contrast adjustment.")]
	public float contrast = 0.1f;

	// Token: 0x04000596 RID: 1430
	[Range(-1f, 1f)]
	[Tooltip("Saturation adjustment.")]
	public float saturation = -0.05f;

	// Token: 0x04000597 RID: 1431
	[Header("Horizontal interference")]
	[Range(0f, 1000f)]
	[Tooltip("How wide (in pixels) is the horizontal interference bar.")]
	public float interferenceWidth = 25f;

	// Token: 0x04000598 RID: 1432
	[Range(-25f, 25f)]
	[Tooltip("How fast does the horizontal interference bar move (in pixels per secons).")]
	public float interferenceSpeed = 3f;

	// Token: 0x04000599 RID: 1433
	[Range(0f, 1f)]
	[Tooltip("How much of the interference is mixed with the base image.")]
	public float interferenceStrength;

	// Token: 0x0400059A RID: 1434
	[Range(0f, 1f)]
	[Tooltip("How much the RGB channels of the interference are appart (in percent of interference bar width, it's basically chromatic aberration just for the interference).")]
	public float interferenceSplit = 0.25f;

	// Token: 0x0400059B RID: 1435
	[Header("CRT")]
	[Tooltip("CRT mask type. Makes some pixels darker than the adjacent ones to simulate CRT display mask.")]
	public BaseCRTEffect.MaskMode maskMode = BaseCRTEffect.MaskMode.Dense;

	// Token: 0x0400059C RID: 1436
	[Range(0f, 1f)]
	[Tooltip("How much of the mask is mixed with the base image.")]
	public float maskStrength = 0.35f;

	// Token: 0x0400059D RID: 1437
	[Space(4f)]
	[Range(0f, 1f)]
	[Tooltip("How curvy the display is, on X-asis.")]
	public float curvatureX = 0.6f;

	// Token: 0x0400059E RID: 1438
	[Range(0f, 1f)]
	[Tooltip("How curvy the display is, on Y-asis.")]
	public float curvatureY = 0.6f;

	// Token: 0x0400059F RID: 1439
	[Range(0f, 1f)]
	[Tooltip("Enlarges the image.")]
	public float overscan;

	// Token: 0x040005A0 RID: 1440
	[Space(4f)]
	[Range(0f, 1f)]
	[Tooltip("How much of the center part is covered by vignette.")]
	public float vignetteSize = 0.35f;

	// Token: 0x040005A1 RID: 1441
	[Range(0f, 1f)]
	[Tooltip("How much of the vignette is mixed with the base image.")]
	public float vignetteStrength = 0.1f;

	// Token: 0x040005A2 RID: 1442
	[Header("Camera's texture")]
	[Tooltip("Internal texture (if used) will be created proportionally to camera's display size.")]
	public BaseCRTEffect.TextureScalingMode textureScaling = BaseCRTEffect.TextureScalingMode.AdjustForHeight;

	// Token: 0x040005A3 RID: 1443
	[Tooltip("Internal texture will be created only when the condition is met (e.g., for 'Downscale Only' it will be created only, if camera's display is bigger than the texture size).")]
	public BaseCRTEffect.TextureScalingPolicy scalingPolicy = BaseCRTEffect.TextureScalingPolicy.DownscaleOnly;

	// Token: 0x040005A4 RID: 1444
	[Range(0f, 4096f)]
	[Tooltip("How wide or high the internally used texture will be.")]
	public int textureSize = 768;

	// Token: 0x040005A5 RID: 1445
	[Header("Multi-camera setup workaround")]
	[Tooltip("If you're using more than one camera to render the scene (like with Pro Camera 2D parallax setup), drag your cameras onto this property and add the effect to the last camera on your rendering path.")]
	public Camera[] otherCameras;

	// Token: 0x040005A6 RID: 1446
	protected Camera mainCamera;

	// Token: 0x040005A7 RID: 1447
	protected RenderTexture cameraTarget;

	// Token: 0x040005A8 RID: 1448
	protected RenderTexture oldCameraTarget;

	// Token: 0x040005A9 RID: 1449
	protected Material blurMaterial;

	// Token: 0x040005AA RID: 1450
	protected Material postProMaterial;

	// Token: 0x040005AB RID: 1451
	protected Material finalPostProMaterial;

	// Token: 0x040005AC RID: 1452
	protected float seconds;

	// Token: 0x040005AD RID: 1453
	protected float blurSigma = float.NaN;

	// Token: 0x040005AE RID: 1454
	protected float[] blurKernel = new float[2];

	// Token: 0x040005AF RID: 1455
	protected float blurZ = float.NaN;

	// Token: 0x040005B0 RID: 1456
	protected float currentBrightness = float.NaN;

	// Token: 0x040005B1 RID: 1457
	protected Matrix4x4 brightnessMat;

	// Token: 0x040005B2 RID: 1458
	protected float currentContrast = float.NaN;

	// Token: 0x040005B3 RID: 1459
	protected Matrix4x4 contrastMat;

	// Token: 0x040005B4 RID: 1460
	protected float currentSaturation = float.NaN;

	// Token: 0x040005B5 RID: 1461
	protected Matrix4x4 saturationMat;

	// Token: 0x040005B6 RID: 1462
	protected Matrix4x4 colorMat;

	// Token: 0x040005B7 RID: 1463
	protected Vector4 colorTransform;

	// Token: 0x020002EB RID: 747
	public enum Preset
	{
		// Token: 0x04000A5D RID: 2653
		Stretch,
		// Token: 0x04000A5E RID: 2654
		Custom,
		// Token: 0x04000A5F RID: 2655
		Default,
		// Token: 0x04000A60 RID: 2656
		KitchenTV,
		// Token: 0x04000A61 RID: 2657
		MiniCRT,
		// Token: 0x04000A62 RID: 2658
		ColorTV,
		// Token: 0x04000A63 RID: 2659
		OldTV,
		// Token: 0x04000A64 RID: 2660
		HighEndMonitor,
		// Token: 0x04000A65 RID: 2661
		ArcadeDisplay,
		// Token: 0x04000A66 RID: 2662
		BrokenBlackAndWhite,
		// Token: 0x04000A67 RID: 2663
		GreenTerminal,
		// Token: 0x04000A68 RID: 2664
		YellowMonitor,
		// Token: 0x04000A69 RID: 2665
		Soft
	}

	// Token: 0x020002EC RID: 748
	public enum NoiseMode
	{
		// Token: 0x04000A6B RID: 2667
		Add,
		// Token: 0x04000A6C RID: 2668
		Subtract,
		// Token: 0x04000A6D RID: 2669
		Multiply,
		// Token: 0x04000A6E RID: 2670
		Divide,
		// Token: 0x04000A6F RID: 2671
		Lighten,
		// Token: 0x04000A70 RID: 2672
		Darken
	}

	// Token: 0x020002ED RID: 749
	public enum MaskMode
	{
		// Token: 0x04000A72 RID: 2674
		Thin,
		// Token: 0x04000A73 RID: 2675
		Dense,
		// Token: 0x04000A74 RID: 2676
		Denser,
		// Token: 0x04000A75 RID: 2677
		ThinScanline,
		// Token: 0x04000A76 RID: 2678
		Scanline,
		// Token: 0x04000A77 RID: 2679
		DenseScanline
	}

	// Token: 0x020002EE RID: 750
	public enum TextureScalingMode
	{
		// Token: 0x04000A79 RID: 2681
		Off,
		// Token: 0x04000A7A RID: 2682
		AdjustForWidth,
		// Token: 0x04000A7B RID: 2683
		AdjustForHeight
	}

	// Token: 0x020002EF RID: 751
	public enum TextureScalingPolicy
	{
		// Token: 0x04000A7D RID: 2685
		Always,
		// Token: 0x04000A7E RID: 2686
		UpscaleOnly,
		// Token: 0x04000A7F RID: 2687
		DownscaleOnly
	}
}
