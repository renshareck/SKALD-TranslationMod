using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000198 RID: 408
[RequireComponent(typeof(SpriteRenderer))]
public class ImageSwitcher : MonoBehaviour
{
	// Token: 0x06001547 RID: 5447 RVA: 0x0005E0C8 File Offset: 0x0005C2C8
	private void Awake()
	{
		BaseCRTEffect.Preset[] array = (BaseCRTEffect.Preset[])Enum.GetValues(typeof(BaseCRTEffect.Preset));
		this.presets = new BaseCRTEffect.Preset[array.Length - 1];
		int num = 0;
		foreach (BaseCRTEffect.Preset preset in array)
		{
			if (preset != BaseCRTEffect.Preset.Custom)
			{
				this.presets[num++] = preset;
			}
		}
		this.textures = SkaldFileIO.LoadAll<Texture2D>("");
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		foreach (Camera camera in Camera.allCameras)
		{
			this.effect = camera.GetComponentInChildren<BaseCRTEffect>();
			if (this.effect != null)
			{
				break;
			}
		}
		foreach (Text text in Object.FindObjectsOfType<Text>())
		{
			if (!(text.name != "Preset Name"))
			{
				this.text = text;
				break;
			}
		}
		this.text.enabled = false;
		this.textureIndex = Array.IndexOf<Texture2D>(this.textures, this.spriteRenderer.sprite.texture);
		this.presetIndex = Array.IndexOf<BaseCRTEffect.Preset>(this.presets, this.effect.predefinedModel);
		if (this.presetIndex < 0)
		{
			this.effect.predefinedModel = this.presets[0];
			this.presetIndex = 0;
		}
		this.ShowPresetName(this.presets[this.presetIndex]);
	}

	// Token: 0x06001548 RID: 5448 RVA: 0x0005E230 File Offset: 0x0005C430
	private void Update()
	{
		if (this.textVisibleDuration != 0f)
		{
			this.textVisibleDuration -= Time.deltaTime;
			if (this.textVisibleDuration <= 0f)
			{
				this.textVisibleDuration = 0f;
				this.text.enabled = false;
			}
		}
		int num = Input.GetKeyDown("a") ? -1 : (Input.GetKeyDown("d") ? 1 : 0);
		int num2 = Input.GetKeyDown("w") ? -1 : (Input.GetKeyDown("s") ? 1 : 0);
		bool keyDown = Input.GetKeyDown(KeyCode.Space);
		if (num == 0 && num2 == 0 && !keyDown)
		{
			return;
		}
		int num3 = (this.textureIndex + num + this.textures.Length) % this.textures.Length;
		int num4 = (this.presetIndex + num2 + this.presets.Length) % this.presets.Length;
		if (this.textureIndex != num3)
		{
			this.spriteRenderer.sprite = Sprite.Create(this.textures[num3], new Rect(0f, 0f, (float)this.textures[num3].width, (float)this.textures[num3].height), new Vector2(0.5f, 0.5f));
			this.textureIndex = num3;
		}
		if (this.presetIndex != num4)
		{
			this.presetIndex = num4;
			this.effect.predefinedModel = this.presets[num4];
			this.ShowPresetName(this.presets[num4]);
		}
		if (keyDown)
		{
			this.effect.enabled = !this.effect.enabled;
			this.ShowOnOff(this.effect.enabled);
		}
	}

	// Token: 0x06001549 RID: 5449 RVA: 0x0005E3D0 File Offset: 0x0005C5D0
	public void OnUpClicked()
	{
		int num = (this.presetIndex - 1 + this.presets.Length) % this.presets.Length;
		this.presetIndex = num;
		this.effect.predefinedModel = this.presets[num];
		this.ShowPresetName(this.presets[num]);
	}

	// Token: 0x0600154A RID: 5450 RVA: 0x0005E420 File Offset: 0x0005C620
	public void OnDownClicked()
	{
		int num = (this.presetIndex + 1 + this.presets.Length) % this.presets.Length;
		this.presetIndex = num;
		this.effect.predefinedModel = this.presets[num];
		this.ShowPresetName(this.presets[num]);
	}

	// Token: 0x0600154B RID: 5451 RVA: 0x0005E470 File Offset: 0x0005C670
	public void OnLeftClicked()
	{
		int num = (this.textureIndex - 1 + this.textures.Length) % this.textures.Length;
		this.spriteRenderer.sprite = Sprite.Create(this.textures[num], new Rect(0f, 0f, (float)this.textures[num].width, (float)this.textures[num].height), new Vector2(0.5f, 0.5f));
		this.textureIndex = num;
	}

	// Token: 0x0600154C RID: 5452 RVA: 0x0005E4F4 File Offset: 0x0005C6F4
	public void OnRightClicked()
	{
		int num = (this.textureIndex + 1 + this.textures.Length) % this.textures.Length;
		this.spriteRenderer.sprite = Sprite.Create(this.textures[num], new Rect(0f, 0f, (float)this.textures[num].width, (float)this.textures[num].height), new Vector2(0.5f, 0.5f));
		this.textureIndex = num;
	}

	// Token: 0x0600154D RID: 5453 RVA: 0x0005E575 File Offset: 0x0005C775
	public void OnCenterClicked()
	{
		this.effect.enabled = !this.effect.enabled;
		this.ShowOnOff(this.effect.enabled);
	}

	// Token: 0x0600154E RID: 5454 RVA: 0x0005E5A1 File Offset: 0x0005C7A1
	private void ShowPresetName(BaseCRTEffect.Preset preset)
	{
		this.text.text = preset.ToString();
		this.text.enabled = true;
		this.textVisibleDuration = 2f;
	}

	// Token: 0x0600154F RID: 5455 RVA: 0x0005E5D2 File Offset: 0x0005C7D2
	private void ShowOnOff(bool on)
	{
		this.text.text = (on ? "[postprocess: on]" : "[postprocess: off]");
		this.text.enabled = true;
		this.textVisibleDuration = 2f;
	}

	// Token: 0x0400057A RID: 1402
	private int textureIndex;

	// Token: 0x0400057B RID: 1403
	private Texture2D[] textures;

	// Token: 0x0400057C RID: 1404
	private BaseCRTEffect.Preset[] presets;

	// Token: 0x0400057D RID: 1405
	private int presetIndex;

	// Token: 0x0400057E RID: 1406
	private SpriteRenderer spriteRenderer;

	// Token: 0x0400057F RID: 1407
	private BaseCRTEffect effect;

	// Token: 0x04000580 RID: 1408
	private Text text;

	// Token: 0x04000581 RID: 1409
	private float textVisibleDuration;
}
