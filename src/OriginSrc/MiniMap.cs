using System;
using UnityEngine;

// Token: 0x020000EE RID: 238
public class MiniMap
{
	// Token: 0x06000EEA RID: 3818 RVA: 0x00046850 File Offset: 0x00044A50
	public MiniMap(int width, int height)
	{
		this.blinkTimer = new CountDownClock(40, true);
		this.miniMapWidth = width;
		this.miniMapHeight = height;
		this.halfMinimapWidth = width / 2;
		this.halfMinimapHeight = height / 2;
		this.miniMap = new TextureTools.TextureData(this.miniMapWidth, this.miniMapHeight);
	}

	// Token: 0x06000EEB RID: 3819 RVA: 0x00046914 File Offset: 0x00044B14
	public UIMap makeMiniMap(MapTileGrid sourceMap)
	{
		int num = this.zoomLevel * 2;
		int xpos = sourceMap.getXPos();
		int ypos = sourceMap.getYPos();
		if (num == 0)
		{
			num = 1;
		}
		int num2 = xpos + this.xScroll - this.halfMinimapWidth / num;
		for (int i = 0; i < this.miniMapWidth; i += num)
		{
			int num3 = ypos + this.yScroll - this.halfMinimapHeight / num;
			for (int j = 0; j < this.miniMapHeight; j += num)
			{
				Color32 markerColor = this.backgroundColor;
				if (num2 >= 0 && num2 < sourceMap.getMapTileWidth() && num3 >= 0 && num3 < sourceMap.getMapTileHeight())
				{
					MapTile tile = sourceMap.getTile(num2, num3);
					if (tile != null && tile.isSpottedOnce())
					{
						if (num2 == xpos && num3 == ypos)
						{
							markerColor = this.getMarkerColor();
						}
						else if (tile.getNestedMapId() != "" || tile.getProp() != null || tile.getParty() != null)
						{
							markerColor = this.pointOfInterestColor;
						}
						else if (tile.isWater())
						{
							markerColor = this.waterColor;
						}
						else if (!tile.isPassable())
						{
							if (tile.getSeeThrough())
							{
								markerColor = this.blockedFullyColor;
							}
							else
							{
								markerColor = this.justImpassableColor;
							}
						}
						else if (tile.isConcealment())
						{
							markerColor = this.roughTerrainColor;
						}
						else
						{
							markerColor = this.justFloorColor;
						}
					}
				}
				for (int k = i; k < i + num; k++)
				{
					for (int l = j; l < j + num; l++)
					{
						this.miniMap.SetPixel(k, l, markerColor);
					}
				}
				num3++;
			}
			num2++;
		}
		UIMap uimap = new UIMap();
		uimap.updateTexture(12, 0, this.miniMap);
		return uimap;
	}

	// Token: 0x06000EEC RID: 3820 RVA: 0x00046AD6 File Offset: 0x00044CD6
	private Color32 getMarkerColor()
	{
		this.blinkTimer.tick();
		if (this.blinkTimer.getTimer() < 5)
		{
			return this.markerColor1;
		}
		return this.markerColor2;
	}

	// Token: 0x06000EED RID: 3821 RVA: 0x00046AFE File Offset: 0x00044CFE
	public void scroll(int x, int y)
	{
		this.xScroll += x * (6 - this.zoomLevel);
		this.yScroll += y * (6 - this.zoomLevel);
	}

	// Token: 0x06000EEE RID: 3822 RVA: 0x00046B2E File Offset: 0x00044D2E
	public void zoom(bool zoomIn)
	{
		if (zoomIn && this.zoomLevel < 4)
		{
			this.zoomLevel++;
			return;
		}
		if (!zoomIn && this.zoomLevel > 0)
		{
			this.zoomLevel--;
		}
	}

	// Token: 0x040003CC RID: 972
	private TextureTools.TextureData miniMap;

	// Token: 0x040003CD RID: 973
	private int miniMapWidth;

	// Token: 0x040003CE RID: 974
	private int miniMapHeight;

	// Token: 0x040003CF RID: 975
	private int halfMinimapWidth;

	// Token: 0x040003D0 RID: 976
	private int halfMinimapHeight;

	// Token: 0x040003D1 RID: 977
	private int zoomLevel = 1;

	// Token: 0x040003D2 RID: 978
	private int xScroll;

	// Token: 0x040003D3 RID: 979
	private int yScroll;

	// Token: 0x040003D4 RID: 980
	private Color32 markerColor1 = C64Color.White;

	// Token: 0x040003D5 RID: 981
	private Color32 markerColor2 = C64Color.Red;

	// Token: 0x040003D6 RID: 982
	private Color32 backgroundColor = C64Color.BrownLight;

	// Token: 0x040003D7 RID: 983
	private Color32 blockedFullyColor = C64Color.GrayLight;

	// Token: 0x040003D8 RID: 984
	private Color32 justImpassableColor = C64Color.Gray;

	// Token: 0x040003D9 RID: 985
	private Color32 roughTerrainColor = C64Color.GrayDark;

	// Token: 0x040003DA RID: 986
	private Color32 justFloorColor = C64Color.Brown;

	// Token: 0x040003DB RID: 987
	private Color32 waterColor = C64Color.Blue;

	// Token: 0x040003DC RID: 988
	private Color32 pointOfInterestColor = C64Color.Yellow;

	// Token: 0x040003DD RID: 989
	private CountDownClock blinkTimer;
}
