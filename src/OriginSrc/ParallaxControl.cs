using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200002B RID: 43
public class ParallaxControl
{
	// Token: 0x0600046A RID: 1130 RVA: 0x00015B9C File Offset: 0x00013D9C
	public ParallaxControl()
	{
		this.layers = new List<ParallaxControl.ParallaxLayer>();
	}

	// Token: 0x0600046B RID: 1131 RVA: 0x00015BD0 File Offset: 0x00013DD0
	public TextureTools.TextureData drawAndGetOutput()
	{
		float horizontalDegree = this.getHorizontalDegree();
		float verticalDegree = this.getVerticalDegree();
		foreach (ParallaxControl.ParallaxLayer parallaxLayer in this.layers)
		{
			parallaxLayer.update(horizontalDegree, verticalDegree);
		}
		foreach (ParallaxControl.ParallaxLayer parallaxLayer2 in this.layers)
		{
			parallaxLayer2.draw(this.outputTexture);
		}
		return this.outputTexture;
	}

	// Token: 0x0600046C RID: 1132 RVA: 0x00015C7C File Offset: 0x00013E7C
	public void setBasePath(string folder)
	{
		this.basePath = "Images/Cutscenes/" + folder + "/";
	}

	// Token: 0x0600046D RID: 1133 RVA: 0x00015C94 File Offset: 0x00013E94
	protected string getBasePath()
	{
		return this.basePath;
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x00015C9C File Offset: 0x00013E9C
	public void addLayer(SKALDProjectData.CutSceneDataContainers.CutScenesBase.CutScenesData.ParallaxLayer rawData)
	{
		string imagePath = rawData.imagePath;
		if (imagePath == "")
		{
			return;
		}
		TextureTools.TextureData textureData = TextureTools.loadTextureData(this.getBasePath() + imagePath);
		if (textureData != null)
		{
			this.layers.Add(new ParallaxControl.ParallaxLayer(textureData, rawData));
		}
	}

	// Token: 0x0600046F RID: 1135 RVA: 0x00015CE8 File Offset: 0x00013EE8
	public void addLayer(string layerName, int xDegree, int yDegree)
	{
		TextureTools.TextureData textureData = TextureTools.loadTextureData(this.getBasePath() + layerName);
		if (textureData != null)
		{
			this.layers.Add(new ParallaxControl.ParallaxLayer(textureData, (float)xDegree, (float)yDegree));
		}
	}

	// Token: 0x06000470 RID: 1136 RVA: 0x00015D20 File Offset: 0x00013F20
	public void addRainLayer(string layerName, int xDegree, int yDegree)
	{
		TextureTools.TextureData textureData = TextureTools.loadTextureData(this.getBasePath() + layerName);
		if (textureData != null)
		{
			this.layers.Add(new ParallaxControl.RainParallaxLayer(textureData, (float)xDegree, (float)yDegree));
		}
	}

	// Token: 0x06000471 RID: 1137 RVA: 0x00015D57 File Offset: 0x00013F57
	public void addFogLayer(int xDegree, int yDegree, int height)
	{
		this.layers.Add(new ParallaxControl.FogParallaxLayer((float)xDegree, (float)yDegree, height));
	}

	// Token: 0x06000472 RID: 1138 RVA: 0x00015D70 File Offset: 0x00013F70
	public void setParallaxSpeedRatio(int speedRatio)
	{
		foreach (ParallaxControl.ParallaxLayer parallaxLayer in this.layers)
		{
			parallaxLayer.setParallaxSpeedRatio(speedRatio);
		}
	}

	// Token: 0x06000473 RID: 1139 RVA: 0x00015DC4 File Offset: 0x00013FC4
	public void setRaining()
	{
		foreach (ParallaxControl.ParallaxLayer parallaxLayer in this.layers)
		{
			if (parallaxLayer is ParallaxControl.RainParallaxLayer)
			{
				(parallaxLayer as ParallaxControl.RainParallaxLayer).setRaining();
			}
		}
	}

	// Token: 0x06000474 RID: 1140 RVA: 0x00015E24 File Offset: 0x00014024
	public void toggleArrivalScroll()
	{
		foreach (ParallaxControl.ParallaxLayer parallaxLayer in this.layers)
		{
			parallaxLayer.toggleArrivalScroll();
		}
	}

	// Token: 0x06000475 RID: 1141 RVA: 0x00015E74 File Offset: 0x00014074
	private float getHorizontalDegree()
	{
		if (this.panLeft)
		{
			return 0f;
		}
		return 1f;
	}

	// Token: 0x06000476 RID: 1142 RVA: 0x00015E89 File Offset: 0x00014089
	private float getVerticalDegree()
	{
		if (this.panLeft)
		{
			return 0f;
		}
		return 1f;
	}

	// Token: 0x06000477 RID: 1143 RVA: 0x00015E9E File Offset: 0x0001409E
	public void togglePan()
	{
		this.panLeft = !this.panLeft;
	}

	// Token: 0x040000F2 RID: 242
	private List<ParallaxControl.ParallaxLayer> layers;

	// Token: 0x040000F3 RID: 243
	protected TextureTools.TextureData outputTexture = new TextureTools.TextureData(480, 270);

	// Token: 0x040000F4 RID: 244
	private string basePath = "Images/Backgrounds/";

	// Token: 0x040000F5 RID: 245
	protected bool panLeft;

	// Token: 0x020001D0 RID: 464
	protected class ParallaxLayer
	{
		// Token: 0x060016BA RID: 5818 RVA: 0x00065E6C File Offset: 0x0006406C
		public ParallaxLayer(TextureTools.TextureData texture, SKALDProjectData.CutSceneDataContainers.CutScenesBase.CutScenesData.ParallaxLayer rawData) : this(texture, (float)rawData.xDegree, (float)rawData.yDegree)
		{
			this.xBaseOffset = (float)rawData.xBasePosition;
			this.yBaseOffset = (float)rawData.yBasePosition;
			this.xTargetOffset = (float)rawData.xTargetPosition;
			this.yTargetOffset = (float)rawData.yTargetPosition;
			this.homingSpeed = rawData.homingSpeed;
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x00065ECE File Offset: 0x000640CE
		public ParallaxLayer(TextureTools.TextureData texture, float horizontalAmplitude, float verticalAmplitude) : this(horizontalAmplitude, verticalAmplitude)
		{
			this.texture = texture;
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x00065EDF File Offset: 0x000640DF
		protected ParallaxLayer(float horizontalAmplitude, float verticalAmplitude)
		{
			this.maxXOffset = horizontalAmplitude;
			this.maxYOffset = verticalAmplitude;
			this.xScrollOffset = -this.maxXOffset;
			this.yScrollOffset = -this.maxYOffset;
			this.setParallaxSpeedRatio(15);
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x00065F17 File Offset: 0x00064117
		public void setParallaxSpeedRatio(int ratio)
		{
			this.speedRatio = (float)ratio;
			this.updateScrollSpeed(1f, 1f);
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x00065F31 File Offset: 0x00064131
		public void toggleArrivalScroll()
		{
			this.arrivalScroll = true;
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x00065F3A File Offset: 0x0006413A
		public virtual void draw(TextureTools.TextureData targetTexture)
		{
			if (this.texture == null || targetTexture == null)
			{
				return;
			}
			this.texture.applyOverlay((int)this.xBaseOffset + (int)this.xScrollOffset, (int)this.yBaseOffset + (int)this.yScrollOffset, targetTexture);
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x00065F72 File Offset: 0x00064172
		public void update(float horizontalDegree, float verticalDegree)
		{
			this.updateParallax(horizontalDegree, verticalDegree);
			this.updateGeneralMovement();
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x00065F84 File Offset: 0x00064184
		private void updateGeneralMovement()
		{
			if (this.xTargetOffset > this.xBaseOffset)
			{
				if (this.xScrollSpeed >= 0f)
				{
					this.xBaseOffset += this.homingSpeed;
				}
			}
			else if (this.xTargetOffset < this.xBaseOffset && this.xScrollSpeed <= 0f)
			{
				this.xBaseOffset -= this.homingSpeed;
			}
			if (this.yTargetOffset > this.yBaseOffset)
			{
				if (this.yScrollSpeed >= 0f)
				{
					this.yBaseOffset += this.homingSpeed;
					return;
				}
			}
			else if (this.yTargetOffset < this.yBaseOffset && this.yScrollSpeed <= 0f)
			{
				this.yBaseOffset -= this.homingSpeed;
			}
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x0006604C File Offset: 0x0006424C
		private void updateParallax(float horizontalDegree, float verticalDegree)
		{
			if (this.arrivalScroll)
			{
				this.updateScrollSpeed(horizontalDegree, verticalDegree);
			}
			this.xScrollOffset += this.xScrollSpeed;
			this.yScrollOffset += this.yScrollSpeed;
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x00066084 File Offset: 0x00064284
		private void updateScrollSpeed(float horizontalDegree, float verticalDegree)
		{
			this.xScrollTargetOffset = 0f - this.maxXOffset + this.maxXOffset * 2f * horizontalDegree;
			this.yScrollTargetOffset = 0f - this.maxYOffset + this.maxYOffset * 2f * verticalDegree;
			float num = this.xScrollTargetOffset - this.xScrollOffset;
			float num2 = this.yScrollTargetOffset - this.yScrollOffset;
			if (Mathf.Abs(num) <= 1f)
			{
				this.xScrollSpeed = 0f;
			}
			else
			{
				this.xScrollSpeed = num / this.speedRatio;
			}
			if (Mathf.Abs(num2) <= 1f)
			{
				this.yScrollSpeed = 0f;
				return;
			}
			this.yScrollSpeed = num2 / this.speedRatio;
		}

		// Token: 0x04000727 RID: 1831
		protected TextureTools.TextureData texture;

		// Token: 0x04000728 RID: 1832
		protected float xScrollOffset;

		// Token: 0x04000729 RID: 1833
		protected float yScrollOffset;

		// Token: 0x0400072A RID: 1834
		protected float xBaseOffset;

		// Token: 0x0400072B RID: 1835
		protected float yBaseOffset;

		// Token: 0x0400072C RID: 1836
		protected float xTargetOffset;

		// Token: 0x0400072D RID: 1837
		protected float yTargetOffset;

		// Token: 0x0400072E RID: 1838
		protected float homingSpeed;

		// Token: 0x0400072F RID: 1839
		private float xScrollTargetOffset;

		// Token: 0x04000730 RID: 1840
		private float yScrollTargetOffset;

		// Token: 0x04000731 RID: 1841
		private float maxXOffset;

		// Token: 0x04000732 RID: 1842
		private float maxYOffset;

		// Token: 0x04000733 RID: 1843
		private float xScrollSpeed;

		// Token: 0x04000734 RID: 1844
		private float yScrollSpeed;

		// Token: 0x04000735 RID: 1845
		private float speedRatio;

		// Token: 0x04000736 RID: 1846
		private bool arrivalScroll;
	}

	// Token: 0x020001D1 RID: 465
	protected class FogParallaxLayer : ParallaxControl.ParallaxLayer
	{
		// Token: 0x060016C4 RID: 5828 RVA: 0x0006613E File Offset: 0x0006433E
		public FogParallaxLayer(float horizontalAmplitude, float verticalAmplitude, int height) : base(horizontalAmplitude, verticalAmplitude)
		{
			this.texture = new TextureTools.TextureData(480, 270);
			this.fogControl.setFogRow(height);
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x00066174 File Offset: 0x00064374
		public override void draw(TextureTools.TextureData targetTexture)
		{
			if (targetTexture == null)
			{
				return;
			}
			this.texture.clear();
			this.fogControl.applyEffect(this.texture, 0, 0, null);
			this.texture.applyOverlay((int)this.xScrollOffset, (int)this.yScrollOffset, targetTexture);
		}

		// Token: 0x04000737 RID: 1847
		private WeatherEffectsControl fogControl = new WeatherEffectsControl();
	}

	// Token: 0x020001D2 RID: 466
	protected class RainParallaxLayer : ParallaxControl.ParallaxLayer
	{
		// Token: 0x060016C6 RID: 5830 RVA: 0x000661B3 File Offset: 0x000643B3
		public RainParallaxLayer(TextureTools.TextureData texture, float horizontalAmplitude, float verticalAmplitude) : base(texture, horizontalAmplitude, verticalAmplitude)
		{
			this.outputTexture = new TextureTools.TextureData(texture.width, texture.height);
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x000661E0 File Offset: 0x000643E0
		public void setRaining()
		{
			this.raining = true;
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x000661EC File Offset: 0x000643EC
		public override void draw(TextureTools.TextureData targetTexture)
		{
			if (targetTexture == null)
			{
				return;
			}
			this.outputTexture.clear();
			this.texture.applyOverlay(this.outputTexture);
			if (this.raining)
			{
				this.weatherControl.setRain(100);
				this.weatherControl.setRainSplashOnColor();
				this.weatherControl.applyEffect(this.outputTexture, 0, 0, null);
			}
			this.outputTexture.applyOverlay((int)this.xScrollOffset, (int)this.yScrollOffset, targetTexture);
		}

		// Token: 0x04000738 RID: 1848
		private WeatherEffectsControl weatherControl = new WeatherEffectsControl();

		// Token: 0x04000739 RID: 1849
		private TextureTools.TextureData outputTexture;

		// Token: 0x0400073A RID: 1850
		private bool raining;
	}
}
