using System;
using UnityEngine;

// Token: 0x02000153 RID: 339
public abstract class UIElement
{
	// Token: 0x060012DC RID: 4828 RVA: 0x00053818 File Offset: 0x00051A18
	protected UIElement()
	{
		this.alignments = new UIElement.Alignments(UIElement.Alignments.VerticalAlignments.Top, UIElement.Alignments.HorizontalAlignments.Left);
		this.dimensions = new UIElement.Dimensions(0, 0, 0, 0);
	}

	// Token: 0x060012DD RID: 4829 RVA: 0x00053867 File Offset: 0x00051A67
	protected UIElement(int x, int y, int width, int height) : this()
	{
		this.dimensions = new UIElement.Dimensions(x, y, width, height);
	}

	// Token: 0x060012DE RID: 4830 RVA: 0x00053880 File Offset: 0x00051A80
	public virtual int getWidth()
	{
		int num = 0;
		if (this.stretchHorizontal)
		{
			if (this.backgroundTexture != null)
			{
				num = this.backgroundTexture.width;
			}
			if (this.foregroundTexture != null && this.foregroundTexture.width > num)
			{
				num = this.foregroundTexture.width;
			}
			num += this.getHorizontalPadding();
		}
		if (num > this.dimensions.width)
		{
			return num;
		}
		return this.dimensions.width;
	}

	// Token: 0x060012DF RID: 4831 RVA: 0x000538F1 File Offset: 0x00051AF1
	public virtual void setYieldToTooltips(bool value)
	{
		this.yieldToTooltips = value;
	}

	// Token: 0x060012E0 RID: 4832 RVA: 0x000538FA File Offset: 0x00051AFA
	public virtual bool useableAsScrollableElement()
	{
		return true;
	}

	// Token: 0x060012E1 RID: 4833 RVA: 0x000538FD File Offset: 0x00051AFD
	public bool hasArrived()
	{
		return this.targetDimensions == null || (this.dimensions.x == this.targetDimensions.x && this.dimensions.y == this.targetDimensions.y);
	}

	// Token: 0x060012E2 RID: 4834 RVA: 0x0005393B File Offset: 0x00051B3B
	public virtual string getHoverText()
	{
		if (this.getHover())
		{
			return this.hoverText;
		}
		return "";
	}

	// Token: 0x060012E3 RID: 4835 RVA: 0x00053951 File Offset: 0x00051B51
	public void setTargetDimensions(int x, int y, int width, int height)
	{
		this.targetDimensions = new UIElement.Dimensions(x, y, width, height);
	}

	// Token: 0x060012E4 RID: 4836 RVA: 0x00053963 File Offset: 0x00051B63
	public void setTargetDimensions(int x, int y)
	{
		this.setTargetDimensions(x, y, this.dimensions.width, this.dimensions.height);
	}

	// Token: 0x060012E5 RID: 4837 RVA: 0x00053983 File Offset: 0x00051B83
	public void setTargetDimensionsY(int y)
	{
		this.setTargetDimensions(this.dimensions.x, y, this.dimensions.width, this.dimensions.height);
	}

	// Token: 0x060012E6 RID: 4838 RVA: 0x000539B0 File Offset: 0x00051BB0
	private void arrive()
	{
		if (this.hasArrived())
		{
			return;
		}
		int num = 10;
		int num2 = 0;
		int num3 = 0;
		if (this.dimensions.x < this.targetDimensions.x)
		{
			num2 = num;
		}
		else if (this.dimensions.x > this.targetDimensions.x)
		{
			num2 = -num;
		}
		if (this.dimensions.y < this.targetDimensions.y)
		{
			num3 = num;
		}
		else if (this.dimensions.y > this.targetDimensions.y)
		{
			num3 = -num;
		}
		this.dimensions.x += num2;
		this.dimensions.y += num3;
		if (this.dimensions.x >= this.targetDimensions.x - num && this.dimensions.x <= this.targetDimensions.x + num)
		{
			this.dimensions.x = this.targetDimensions.x;
		}
		if (this.dimensions.y >= this.targetDimensions.y - num && this.dimensions.y <= this.targetDimensions.y + num)
		{
			this.dimensions.y = this.targetDimensions.y;
		}
		this.alignElements();
	}

	// Token: 0x060012E7 RID: 4839 RVA: 0x00053AFB File Offset: 0x00051CFB
	public virtual void reveal()
	{
		this.revealed = true;
	}

	// Token: 0x060012E8 RID: 4840 RVA: 0x00053B04 File Offset: 0x00051D04
	public void setDimensions(int x, int y, int width, int height)
	{
		this.dimensions = new UIElement.Dimensions(x, y, width, height);
	}

	// Token: 0x060012E9 RID: 4841 RVA: 0x00053B16 File Offset: 0x00051D16
	public virtual void setPosition(int x, int y)
	{
		this.setX(x);
		this.setY(y);
	}

	// Token: 0x060012EA RID: 4842 RVA: 0x00053B26 File Offset: 0x00051D26
	public virtual void setX(int x)
	{
		this.dimensions.x = x;
	}

	// Token: 0x060012EB RID: 4843 RVA: 0x00053B34 File Offset: 0x00051D34
	public virtual void setY(int y)
	{
		this.dimensions.y = y;
	}

	// Token: 0x060012EC RID: 4844 RVA: 0x00053B42 File Offset: 0x00051D42
	public SkaldPoint2D getPosition()
	{
		return new SkaldPoint2D(this.getX(), this.getY());
	}

	// Token: 0x060012ED RID: 4845 RVA: 0x00053B55 File Offset: 0x00051D55
	public virtual void setPosition(SkaldPoint2D position)
	{
		if (position == null)
		{
			return;
		}
		this.setX(position.X);
		this.setY(position.Y);
	}

	// Token: 0x060012EE RID: 4846 RVA: 0x00053B73 File Offset: 0x00051D73
	public virtual int getX()
	{
		return this.dimensions.x;
	}

	// Token: 0x060012EF RID: 4847 RVA: 0x00053B80 File Offset: 0x00051D80
	public virtual int getY()
	{
		return this.dimensions.y;
	}

	// Token: 0x060012F0 RID: 4848 RVA: 0x00053B8D File Offset: 0x00051D8D
	public virtual void setWidth(int width)
	{
		this.dimensions.width = width;
	}

	// Token: 0x060012F1 RID: 4849 RVA: 0x00053B9B File Offset: 0x00051D9B
	public virtual void setHeight(int height)
	{
		this.dimensions.height = height;
	}

	// Token: 0x060012F2 RID: 4850 RVA: 0x00053BA9 File Offset: 0x00051DA9
	public virtual int getBaseWidth()
	{
		return this.dimensions.width;
	}

	// Token: 0x060012F3 RID: 4851 RVA: 0x00053BB6 File Offset: 0x00051DB6
	public virtual int getBaseHeight()
	{
		return this.dimensions.height;
	}

	// Token: 0x060012F4 RID: 4852 RVA: 0x00053BC3 File Offset: 0x00051DC3
	public virtual void setReveal(bool reveal)
	{
		this.revealed = reveal;
	}

	// Token: 0x060012F5 RID: 4853 RVA: 0x00053BCC File Offset: 0x00051DCC
	public virtual bool isRevealed()
	{
		return this.revealed;
	}

	// Token: 0x060012F6 RID: 4854 RVA: 0x00053BD4 File Offset: 0x00051DD4
	public virtual int getHeight()
	{
		if (this.hidden)
		{
			return 0;
		}
		int num = 0;
		if (this.backgroundTexture != null)
		{
			num = this.backgroundTexture.height;
		}
		if (this.foregroundTexture != null && this.foregroundTexture.height > num)
		{
			num = this.foregroundTexture.height;
		}
		num += this.getVerticalPadding();
		if (num > this.dimensions.height)
		{
			return num;
		}
		return this.dimensions.height;
	}

	// Token: 0x060012F7 RID: 4855 RVA: 0x00053C47 File Offset: 0x00051E47
	public virtual void setAlignment(UIElement.Alignments alignments)
	{
		this.alignments = alignments;
	}

	// Token: 0x060012F8 RID: 4856 RVA: 0x00053C50 File Offset: 0x00051E50
	public virtual void draw(TextureTools.TextureData targetTexture)
	{
		if (this.hidden)
		{
			return;
		}
		if (!this.hasArrived())
		{
			this.arrive();
		}
		if (this.leftDown && this.backgroundColors.leftClickedColor != Color.clear)
		{
			this.drawColorBackground(this.backgroundColors.leftClickedColor, targetTexture);
		}
		else if (this.hover && this.backgroundColors.hoverColor != Color.clear)
		{
			this.drawColorBackground(this.backgroundColors.hoverColor, targetTexture);
		}
		else if (this.backgroundColors.mainColor != Color.clear)
		{
			this.drawColorBackground(this.backgroundColors.mainColor, targetTexture);
		}
		if (this.leftDown && this.backgroundTexturePressed != null)
		{
			TextureTools.applyOverlay(targetTexture, this.backgroundTexturePressed, this.dimensions.x, this.dimensions.y - this.backgroundTexture.height);
		}
		else if (this.hover && this.backgroundTextureHover != null)
		{
			TextureTools.applyOverlay(targetTexture, this.backgroundTextureHover, this.dimensions.x, this.dimensions.y - this.backgroundTexture.height);
		}
		else if (this.backgroundTexture != null)
		{
			TextureTools.applyOverlay(targetTexture, this.backgroundTexture, this.dimensions.x, this.dimensions.y - this.backgroundTexture.height);
		}
		if (!this.isRevealed())
		{
			return;
		}
		if (this.foregroundTexture != null)
		{
			int num = this.dimensions.x + this.padding.left;
			int num2 = this.dimensions.y - this.getHeight() + this.padding.top;
			this.drawForegroundTextureShadow(targetTexture, num, num2);
			if (this.leftDown && this.foregroundColors.leftClickedColor != Color.clear)
			{
				TextureTools.applyOverlayColor(targetTexture, this.foregroundTexture, num, num2, this.foregroundColors.leftClickedColor);
			}
			else if (this.hover && this.foregroundColors.hoverColor != Color.clear)
			{
				TextureTools.applyOverlayColor(targetTexture, this.foregroundTexture, num, num2, this.foregroundColors.hoverColor);
			}
			else if (this.foregroundColors.mainColor != Color.clear)
			{
				TextureTools.applyOverlayColor(targetTexture, this.foregroundTexture, num, num2, this.foregroundColors.mainColor);
			}
			else
			{
				TextureTools.applyOverlay(targetTexture, this.foregroundTexture, num, num2);
			}
			if (this.leftDown && this.foregroundColors.outlineClickedColor != Color.clear)
			{
				TextureTools.applyOverlay(targetTexture, this.foregroundTexture.getOutline(this.foregroundColors.outlineClickedColor), num - 1, num2 - 1);
				return;
			}
			if (this.hover && this.foregroundColors.outlineHoverColor != Color.clear)
			{
				TextureTools.applyOverlay(targetTexture, this.foregroundTexture.getOutline(this.foregroundColors.outlineHoverColor), num - 1, num2 - 1);
				return;
			}
			if (this.foregroundColors.outlineMainColor != Color.clear)
			{
				TextureTools.applyOverlay(targetTexture, this.foregroundTexture.getOutline(this.foregroundColors.outlineMainColor), num - 1, num2 - 1);
			}
		}
	}

	// Token: 0x060012F9 RID: 4857 RVA: 0x00053FC0 File Offset: 0x000521C0
	private void drawColorBackground(Color color, TextureTools.TextureData targetTexture)
	{
		if (this.colorBackground == null)
		{
			this.colorBackground = new TextureTools.TextureData(this.getWidth(), this.getHeight());
		}
		this.colorBackground.clearToColor(color);
		TextureTools.applyOverlay(targetTexture, this.colorBackground, this.dimensions.x, this.dimensions.y - this.colorBackground.height);
	}

	// Token: 0x060012FA RID: 4858 RVA: 0x0005402B File Offset: 0x0005222B
	public virtual void alignElements()
	{
	}

	// Token: 0x060012FB RID: 4859 RVA: 0x0005402D File Offset: 0x0005222D
	protected virtual void drawForegroundTextureShadow(TextureTools.TextureData targetTexture, int x, int y)
	{
		if (this.foregroundColors.shadowMainColor != Color.clear)
		{
			TextureTools.applyOverlayColor(targetTexture, this.foregroundTexture, x - 1, y - 1, this.foregroundColors.shadowMainColor);
		}
	}

	// Token: 0x060012FC RID: 4860 RVA: 0x00054068 File Offset: 0x00052268
	public virtual void setPaddingLeft(int value)
	{
		this.padding.left = value;
	}

	// Token: 0x060012FD RID: 4861 RVA: 0x00054076 File Offset: 0x00052276
	public virtual void setPaddingTop(int value)
	{
		this.padding.top = value;
	}

	// Token: 0x060012FE RID: 4862 RVA: 0x00054084 File Offset: 0x00052284
	public void clearOutlineColor()
	{
		this.foregroundColors.outlineMainColor = Color.clear;
	}

	// Token: 0x060012FF RID: 4863 RVA: 0x0005409B File Offset: 0x0005229B
	public int getHorizontalPadding()
	{
		return this.padding.right + this.padding.left;
	}

	// Token: 0x06001300 RID: 4864 RVA: 0x000540B4 File Offset: 0x000522B4
	public int getVerticalPadding()
	{
		return this.padding.top + this.padding.bottom;
	}

	// Token: 0x06001301 RID: 4865 RVA: 0x000540D0 File Offset: 0x000522D0
	public void ensureOnScreen()
	{
		if (this.dimensions.x < 0)
		{
			this.dimensions.x = 0;
		}
		else if (this.dimensions.x + this.getWidth() > 480 && this.getWidth() < 480)
		{
			this.dimensions.x = 480 - this.getWidth();
		}
		if (this.dimensions.y > 270)
		{
			this.dimensions.y = 270;
			return;
		}
		if (this.dimensions.y - this.getHeight() < 0)
		{
			if (this.getHeight() > 270)
			{
				this.dimensions.y = 270;
				return;
			}
			this.dimensions.y = this.getHeight();
		}
	}

	// Token: 0x06001302 RID: 4866 RVA: 0x000541A0 File Offset: 0x000523A0
	public Vector2 getRelativeMousePosition()
	{
		Vector2 mousePosition = SkaldIO.getMousePosition();
		float num = (mousePosition.x - (float)this.dimensions.x) / (float)this.getWidth();
		float num2 = (mousePosition.y - (float)(this.dimensions.y - this.getHeight())) / (float)this.getHeight();
		if (num < 0f || num > 1f)
		{
			return new Vector2(-1f, -1f);
		}
		if (num2 < 0f || num2 > 1f)
		{
			return new Vector2(-1f, -1f);
		}
		return new Vector2(num, num2);
	}

	// Token: 0x06001303 RID: 4867 RVA: 0x00054237 File Offset: 0x00052437
	public void setAllowDoubleClick(bool value)
	{
		this.allowDoubleClick = value;
	}

	// Token: 0x06001304 RID: 4868 RVA: 0x00054240 File Offset: 0x00052440
	public virtual void updateMouseInteraction()
	{
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		bool flag5 = false;
		bool flag6 = false;
		Vector2 mousePosition = SkaldIO.getMousePosition();
		if (this.mouseTimer != null)
		{
			if (this.mouseTimer.isTimerZero())
			{
				this.mouseTimer = null;
			}
			else
			{
				this.mouseTimer.tick();
			}
		}
		bool flag7 = this.yieldToTooltips && ToolTipPrinter.isMouseOverTooltip();
		if (this.isRevealed() && !flag7 && (float)this.dimensions.x <= mousePosition.x && (float)(this.dimensions.x + this.getWidth()) > mousePosition.x && (float)this.dimensions.y >= mousePosition.y && (float)(this.dimensions.y - this.getHeight()) < mousePosition.y)
		{
			flag = true;
			if (SkaldIO.getMouseUp(0) && this.focus)
			{
				if (this.allowDoubleClick && this.doubleClickCooldown)
				{
					this.doubleClickCooldown = false;
				}
				else
				{
					this.focus = true;
					flag4 = true;
				}
			}
			else if (SkaldIO.getMouseUp(1) && this.focus)
			{
				flag5 = true;
			}
			else if (SkaldIO.getMousePressed(0) || SkaldIO.getMousePressed(1))
			{
				this.focus = true;
				AudioControl.playButtonClick();
				if (SkaldIO.getMousePressed(0) && this.allowDoubleClick)
				{
					if (this.mouseTimer == null)
					{
						this.mouseTimer = new CountDownClock(30, false);
						this.doubleClickCooldown = false;
					}
					else if (!this.mouseTimer.isTimerZero())
					{
						flag6 = true;
						this.doubleClickCooldown = true;
						this.mouseTimer = null;
					}
				}
			}
			else if (SkaldIO.getMouseHeldDown(0))
			{
				flag2 = true;
			}
			else if (SkaldIO.getMouseHeldDown(1))
			{
				flag3 = true;
			}
		}
		if (SkaldIO.anyKeyUp())
		{
			this.focus = false;
		}
		this.setMouseInteraction(flag, flag2, flag3, flag4, flag5, flag6);
	}

	// Token: 0x06001305 RID: 4869 RVA: 0x0005440E File Offset: 0x0005260E
	public bool getDoubleClicked()
	{
		return this.doubleClick;
	}

	// Token: 0x06001306 RID: 4870 RVA: 0x00054416 File Offset: 0x00052616
	public bool getLeftUp()
	{
		return this.leftUp;
	}

	// Token: 0x06001307 RID: 4871 RVA: 0x0005441E File Offset: 0x0005261E
	public bool getRightUp()
	{
		return this.rightUp;
	}

	// Token: 0x06001308 RID: 4872 RVA: 0x00054426 File Offset: 0x00052626
	public bool getLeftDown()
	{
		return this.leftDown;
	}

	// Token: 0x06001309 RID: 4873 RVA: 0x0005442E File Offset: 0x0005262E
	public bool getRightDown()
	{
		return this.rightDown;
	}

	// Token: 0x0600130A RID: 4874 RVA: 0x00054436 File Offset: 0x00052636
	public bool getHover()
	{
		return this.hover;
	}

	// Token: 0x0600130B RID: 4875 RVA: 0x0005443E File Offset: 0x0005263E
	public virtual void setMouseInteraction(bool hover, bool leftDown, bool rightDown, bool leftUp, bool rightUp, bool doubleClick)
	{
		this.hover = hover;
		this.leftDown = leftDown;
		this.rightDown = rightDown;
		this.leftUp = leftUp;
		this.rightUp = rightUp;
		this.doubleClick = doubleClick;
	}

	// Token: 0x0400048B RID: 1163
	private UIElement.Dimensions dimensions;

	// Token: 0x0400048C RID: 1164
	private UIElement.Dimensions targetDimensions;

	// Token: 0x0400048D RID: 1165
	public UIElement.Padding padding;

	// Token: 0x0400048E RID: 1166
	public UIElement.Alignments alignments;

	// Token: 0x0400048F RID: 1167
	public string hoverText = "";

	// Token: 0x04000490 RID: 1168
	public bool stretchVertical;

	// Token: 0x04000491 RID: 1169
	public bool stretchHorizontal;

	// Token: 0x04000492 RID: 1170
	public bool fixedPosition;

	// Token: 0x04000493 RID: 1171
	protected bool allowDoubleClick = true;

	// Token: 0x04000494 RID: 1172
	protected bool hover;

	// Token: 0x04000495 RID: 1173
	protected bool focus;

	// Token: 0x04000496 RID: 1174
	protected bool leftDown;

	// Token: 0x04000497 RID: 1175
	protected bool rightDown;

	// Token: 0x04000498 RID: 1176
	protected bool leftUp;

	// Token: 0x04000499 RID: 1177
	protected bool doubleClick;

	// Token: 0x0400049A RID: 1178
	protected bool rightUp;

	// Token: 0x0400049B RID: 1179
	protected bool yieldToTooltips = true;

	// Token: 0x0400049C RID: 1180
	private bool doubleClickCooldown;

	// Token: 0x0400049D RID: 1181
	public UIElement.BehaviourColor backgroundColors;

	// Token: 0x0400049E RID: 1182
	public UIElement.BehaviourColor foregroundColors;

	// Token: 0x0400049F RID: 1183
	public TextureTools.TextureData backgroundTexture;

	// Token: 0x040004A0 RID: 1184
	public TextureTools.TextureData backgroundTextureHover;

	// Token: 0x040004A1 RID: 1185
	public TextureTools.TextureData backgroundTexturePressed;

	// Token: 0x040004A2 RID: 1186
	public TextureTools.TextureData foregroundTexture;

	// Token: 0x040004A3 RID: 1187
	private TextureTools.TextureData colorBackground;

	// Token: 0x040004A4 RID: 1188
	public bool revealed = true;

	// Token: 0x040004A5 RID: 1189
	public bool hidden;

	// Token: 0x040004A6 RID: 1190
	private CountDownClock mouseTimer;

	// Token: 0x02000293 RID: 659
	public class Dimensions
	{
		// Token: 0x06001AB8 RID: 6840 RVA: 0x00073747 File Offset: 0x00071947
		public Dimensions(int x, int y, int width, int height)
		{
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
		}

		// Token: 0x040009A4 RID: 2468
		public int x;

		// Token: 0x040009A5 RID: 2469
		public int y;

		// Token: 0x040009A6 RID: 2470
		public int width;

		// Token: 0x040009A7 RID: 2471
		public int height;
	}

	// Token: 0x02000294 RID: 660
	public struct Padding
	{
		// Token: 0x06001AB9 RID: 6841 RVA: 0x0007376C File Offset: 0x0007196C
		public Padding(int top, int right, int bottom, int left)
		{
			this.top = top;
			this.right = right;
			this.bottom = bottom;
			this.left = left;
		}

		// Token: 0x040009A8 RID: 2472
		public int top;

		// Token: 0x040009A9 RID: 2473
		public int right;

		// Token: 0x040009AA RID: 2474
		public int bottom;

		// Token: 0x040009AB RID: 2475
		public int left;
	}

	// Token: 0x02000295 RID: 661
	public struct Alignments
	{
		// Token: 0x06001ABA RID: 6842 RVA: 0x0007378B File Offset: 0x0007198B
		public Alignments(UIElement.Alignments.VerticalAlignments verticalAlignment, UIElement.Alignments.HorizontalAlignments horizontalAlignment)
		{
			this.verticalAlignment = verticalAlignment;
			this.horizontalAlignment = horizontalAlignment;
		}

		// Token: 0x040009AC RID: 2476
		public UIElement.Alignments.HorizontalAlignments horizontalAlignment;

		// Token: 0x040009AD RID: 2477
		public UIElement.Alignments.VerticalAlignments verticalAlignment;

		// Token: 0x020003D4 RID: 980
		public enum VerticalAlignments
		{
			// Token: 0x04000C61 RID: 3169
			Top,
			// Token: 0x04000C62 RID: 3170
			Center,
			// Token: 0x04000C63 RID: 3171
			Bottom
		}

		// Token: 0x020003D5 RID: 981
		public enum HorizontalAlignments
		{
			// Token: 0x04000C65 RID: 3173
			Left,
			// Token: 0x04000C66 RID: 3174
			Center,
			// Token: 0x04000C67 RID: 3175
			Right
		}
	}

	// Token: 0x02000296 RID: 662
	public struct BehaviourColor
	{
		// Token: 0x06001ABB RID: 6843 RVA: 0x0007379C File Offset: 0x0007199C
		public BehaviourColor(Color mainColor)
		{
			this.mainColor = mainColor;
			this.leftClickedColor = Color.clear;
			this.hoverColor = Color.clear;
			this.outlineMainColor = Color.clear;
			this.outlineClickedColor = Color.clear;
			this.outlineHoverColor = Color.clear;
			this.shadowMainColor = Color.clear;
			this.shadowClickedColor = Color.clear;
			this.shadowHoverColor = Color.clear;
		}

		// Token: 0x040009AE RID: 2478
		public Color32 mainColor;

		// Token: 0x040009AF RID: 2479
		public Color32 leftClickedColor;

		// Token: 0x040009B0 RID: 2480
		public Color32 hoverColor;

		// Token: 0x040009B1 RID: 2481
		public Color32 outlineMainColor;

		// Token: 0x040009B2 RID: 2482
		public Color32 outlineClickedColor;

		// Token: 0x040009B3 RID: 2483
		public Color32 outlineHoverColor;

		// Token: 0x040009B4 RID: 2484
		public Color32 shadowMainColor;

		// Token: 0x040009B5 RID: 2485
		public Color32 shadowClickedColor;

		// Token: 0x040009B6 RID: 2486
		public Color32 shadowHoverColor;
	}
}
