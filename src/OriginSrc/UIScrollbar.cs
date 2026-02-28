using System;
using UnityEngine;

// Token: 0x02000167 RID: 359
public abstract class UIScrollbar : UIElement
{
	// Token: 0x0600137D RID: 4989 RVA: 0x00055DC4 File Offset: 0x00053FC4
	protected UIScrollbar(int x, int y, int width, int height, UICanvas targetCanvas) : base(x, y, width, height)
	{
		this.targetCanvas = targetCanvas;
		this.padding.left = 1;
		this.padding.top = 1;
		this.backgroundColors.mainColor = Color.red;
		base.setAllowDoubleClick(false);
		this.bar = new TextureTools.TextureData(8, height);
		this.bar.clearToColor(C64Color.Gray);
		this.scrollUpButton = new TextureTools.TextureData(6, 8);
		this.scrollUpButton.clearToColor(C64Color.GrayDark);
		this.scrollDownButton = new TextureTools.TextureData(6, 8);
		this.scrollDownButton.clearToColor(C64Color.GrayDark);
		this.setHead();
	}

	// Token: 0x0600137E RID: 4990 RVA: 0x00055E7D File Offset: 0x0005407D
	public UIScrollbar(int width, int height, UICanvas targetCanvas) : this(0, 0, width, height, targetCanvas)
	{
	}

	// Token: 0x0600137F RID: 4991 RVA: 0x00055E8A File Offset: 0x0005408A
	public float getScrollDegree()
	{
		return this.degree;
	}

	// Token: 0x06001380 RID: 4992 RVA: 0x00055E92 File Offset: 0x00054092
	public void resetScrollBar()
	{
		this.degree = 0f;
	}

	// Token: 0x06001381 RID: 4993 RVA: 0x00055E9F File Offset: 0x0005409F
	public void setIncrement(int increment)
	{
		if (increment == this.increment)
		{
			return;
		}
		this.increment = increment;
		if (this.increment == 0)
		{
			this.increment = 1;
		}
		this.setHead();
	}

	// Token: 0x06001382 RID: 4994 RVA: 0x00055EC8 File Offset: 0x000540C8
	private void setHead()
	{
		int scrollableHeight = this.getScrollableHeight();
		int width = 6;
		int height = Mathf.Clamp(scrollableHeight / this.increment - 2, 2, scrollableHeight);
		this.head = new TextureTools.TextureData(width, height);
		this.head.clearToColor(C64Color.GrayLight);
		this.headLight = new TextureTools.TextureData(width, height);
		this.headLight.clearToColor(C64Color.White);
	}

	// Token: 0x06001383 RID: 4995 RVA: 0x00055F2C File Offset: 0x0005412C
	private void scrollByIncrement(bool scrollUp)
	{
		if (this.increment <= 1)
		{
			return;
		}
		float num = 1f / (float)this.increment;
		if (scrollUp)
		{
			this.degree += num;
		}
		else
		{
			this.degree -= num;
		}
		this.degree = Mathf.Clamp(this.degree, 0f, 1f);
	}

	// Token: 0x06001384 RID: 4996 RVA: 0x00055F8D File Offset: 0x0005418D
	public void scrollUpByIncrement()
	{
		this.scrollByIncrement(true);
	}

	// Token: 0x06001385 RID: 4997 RVA: 0x00055F96 File Offset: 0x00054196
	public void scrollDownByIncrement()
	{
		this.scrollByIncrement(false);
	}

	// Token: 0x06001386 RID: 4998 RVA: 0x00055F9F File Offset: 0x0005419F
	protected virtual bool canMouseWheelScroll()
	{
		return base.getHover();
	}

	// Token: 0x06001387 RID: 4999 RVA: 0x00055FAC File Offset: 0x000541AC
	public override void updateMouseInteraction()
	{
		base.updateMouseInteraction();
		float y = SkaldIO.getGlobalMousePosition().y;
		int scrollDownButtonYPos = this.getScrollDownButtonYPos();
		int scrollableHeight = this.getScrollableHeight();
		bool flag = this.canMouseWheelScroll();
		if (flag && (SkaldIO.getMouseWheelScrollUp() || SkaldIO.getButtonScrollUp()))
		{
			this.scrollDownByIncrement();
			return;
		}
		if (flag && (SkaldIO.getMouseWheelScrollDown() || SkaldIO.getButtonScrollDown()))
		{
			this.scrollUpByIncrement();
			return;
		}
		if (y > (float)scrollDownButtonYPos)
		{
			if (this.leftUp)
			{
				this.scrollDownByIncrement();
				return;
			}
		}
		else if (y < (float)(scrollDownButtonYPos - scrollableHeight))
		{
			if (this.leftUp)
			{
				this.scrollUpByIncrement();
				return;
			}
		}
		else if (base.getHover() && this.leftDown)
		{
			this.degree = Mathf.Abs((y - (float)scrollDownButtonYPos) / (float)scrollableHeight);
		}
	}

	// Token: 0x06001388 RID: 5000 RVA: 0x0005605C File Offset: 0x0005425C
	public override void draw(TextureTools.TextureData targetTexture)
	{
		TextureTools.applyOverlay(targetTexture, this.bar, this.padding.left + this.getX(), this.getY() - this.padding.top - this.bar.height);
		TextureTools.TextureData textureData = this.head;
		if (this.canMouseWheelScroll())
		{
			textureData = this.headLight;
		}
		TextureTools.applyOverlay(targetTexture, textureData, this.padding.left + this.getX() + 1, this.getScrollDownButtonYPos() - (this.padding.top + textureData.height + Mathf.RoundToInt(((float)this.getScrollableHeight() - (float)(textureData.height + 2)) * this.degree)));
		TextureTools.applyOverlay(targetTexture, this.scrollDownButton, this.padding.left + this.getX() + 1, this.getScrollDownButtonYPos());
		TextureTools.applyOverlay(targetTexture, this.scrollUpButton, this.padding.left + this.getX() + 1, this.getScrollUpButtonYPos());
	}

	// Token: 0x06001389 RID: 5001 RVA: 0x00056159 File Offset: 0x00054359
	private int getScrollDownButtonYPos()
	{
		return this.getY() - (1 + this.padding.top + this.scrollDownButton.height);
	}

	// Token: 0x0600138A RID: 5002 RVA: 0x0005617B File Offset: 0x0005437B
	private int getScrollUpButtonYPos()
	{
		return this.getY() - (this.bar.height + this.padding.top - 1);
	}

	// Token: 0x0600138B RID: 5003 RVA: 0x0005619D File Offset: 0x0005439D
	private int getScrollableHeight()
	{
		return this.getHeight() - (this.scrollDownButton.height + this.scrollUpButton.height + 2);
	}

	// Token: 0x040004D0 RID: 1232
	private float degree;

	// Token: 0x040004D1 RID: 1233
	private int increment = 1;

	// Token: 0x040004D2 RID: 1234
	private TextureTools.TextureData head;

	// Token: 0x040004D3 RID: 1235
	private TextureTools.TextureData headLight;

	// Token: 0x040004D4 RID: 1236
	private TextureTools.TextureData scrollUpButton;

	// Token: 0x040004D5 RID: 1237
	private TextureTools.TextureData scrollDownButton;

	// Token: 0x040004D6 RID: 1238
	private TextureTools.TextureData bar;

	// Token: 0x040004D7 RID: 1239
	protected UIElement targetCanvas;

	// Token: 0x040004D8 RID: 1240
	protected UIElement testCanvas;
}
