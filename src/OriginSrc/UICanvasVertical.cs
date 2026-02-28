using System;

// Token: 0x02000151 RID: 337
public class UICanvasVertical : UICanvas
{
	// Token: 0x060012D2 RID: 4818 RVA: 0x00053328 File Offset: 0x00051528
	public UICanvasVertical(int x, int y, int width, int height) : base(x, y, width, height)
	{
	}

	// Token: 0x060012D3 RID: 4819 RVA: 0x00053335 File Offset: 0x00051535
	public UICanvasVertical()
	{
	}

	// Token: 0x060012D4 RID: 4820 RVA: 0x00053340 File Offset: 0x00051540
	public override int getWidth()
	{
		if (!this.stretchHorizontal)
		{
			return this.getBaseWidth() + base.getHorizontalPadding();
		}
		int num = this.getBaseWidth() + base.getHorizontalPadding();
		foreach (UIElement uielement in base.getElements())
		{
			if (uielement.getWidth() + base.getHorizontalPadding() > num)
			{
				num = uielement.getWidth() + base.getHorizontalPadding();
			}
		}
		return num;
	}

	// Token: 0x060012D5 RID: 4821 RVA: 0x000533D0 File Offset: 0x000515D0
	public override int getHeight()
	{
		if (this.hidden)
		{
			return 0;
		}
		if (!this.stretchVertical)
		{
			return this.getBaseHeight();
		}
		int num = 0;
		foreach (UIElement uielement in base.getElements())
		{
			num += uielement.getHeight();
		}
		num += base.getVerticalPadding();
		if (num > this.getBaseHeight())
		{
			return num;
		}
		return this.getBaseHeight();
	}

	// Token: 0x060012D6 RID: 4822 RVA: 0x0005345C File Offset: 0x0005165C
	public override void alignElements()
	{
		int num = this.getY() - this.padding.top;
		int num2 = 0;
		if (this.alignments.verticalAlignment == UIElement.Alignments.VerticalAlignments.Bottom)
		{
			foreach (UIElement uielement in base.getElements())
			{
				num2 += uielement.getHeight();
			}
		}
		foreach (UIElement uielement2 in base.getElements())
		{
			if (!uielement2.fixedPosition)
			{
				if (this.alignments.horizontalAlignment == UIElement.Alignments.HorizontalAlignments.Center)
				{
					uielement2.setX(base.getDrawXAnchor() - uielement2.getWidth() / 2);
				}
				else
				{
					uielement2.setX(this.getX() + this.padding.left);
				}
				uielement2.setY(num + num2);
				num -= uielement2.getHeight();
			}
			uielement2.alignElements();
		}
	}
}
