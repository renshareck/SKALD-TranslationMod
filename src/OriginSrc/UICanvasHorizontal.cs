using System;
using System.Collections.Generic;

// Token: 0x02000152 RID: 338
public class UICanvasHorizontal : UICanvas
{
	// Token: 0x060012D7 RID: 4823 RVA: 0x00053574 File Offset: 0x00051774
	public UICanvasHorizontal(int x, int y, int width, int height) : base(x, y, width, height)
	{
	}

	// Token: 0x060012D8 RID: 4824 RVA: 0x00053581 File Offset: 0x00051781
	public UICanvasHorizontal()
	{
	}

	// Token: 0x060012D9 RID: 4825 RVA: 0x0005358C File Offset: 0x0005178C
	public override int getWidth()
	{
		if (!this.stretchHorizontal)
		{
			return this.getBaseWidth();
		}
		int num = 0;
		foreach (UIElement uielement in base.getElements())
		{
			num += uielement.getWidth();
		}
		num += base.getHorizontalPadding();
		if (num > this.getBaseWidth())
		{
			return num;
		}
		return this.getBaseWidth();
	}

	// Token: 0x060012DA RID: 4826 RVA: 0x0005360C File Offset: 0x0005180C
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
		int num = this.getBaseHeight();
		foreach (UIElement uielement in base.getElements())
		{
			if (uielement.getHeight() > num)
			{
				num = uielement.getHeight() + base.getVerticalPadding();
			}
		}
		return num;
	}

	// Token: 0x060012DB RID: 4827 RVA: 0x00053690 File Offset: 0x00051890
	public override void alignElements()
	{
		if (this.alignments.horizontalAlignment == UIElement.Alignments.HorizontalAlignments.Center)
		{
			int num = 0;
			foreach (UIElement uielement in base.getElements())
			{
				if (!uielement.fixedPosition)
				{
					num += uielement.getWidth();
				}
			}
			int num2 = 0;
			using (List<UIElement>.Enumerator enumerator = base.getElements().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					UIElement uielement2 = enumerator.Current;
					if (!uielement2.fixedPosition)
					{
						uielement2.setX(base.getDrawXAnchor() - num / 2 + num2);
						uielement2.setY(this.getY() + this.padding.top);
						num2 += uielement2.getWidth();
					}
					uielement2.alignElements();
				}
				return;
			}
		}
		int num3 = this.getX() + this.padding.left;
		foreach (UIElement uielement3 in base.getElements())
		{
			if (!uielement3.fixedPosition)
			{
				uielement3.setX(num3);
				uielement3.setY(this.getY() + this.padding.top);
				num3 += uielement3.getWidth();
			}
			uielement3.alignElements();
		}
	}
}
