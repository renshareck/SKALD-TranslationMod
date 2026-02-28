using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000150 RID: 336
public abstract class UICanvas : UIElement
{
	// Token: 0x060012B4 RID: 4788 RVA: 0x00052CD4 File Offset: 0x00050ED4
	protected UICanvas(int x, int y, int width, int height) : base(x, y, width, height)
	{
	}

	// Token: 0x060012B5 RID: 4789 RVA: 0x00052CF3 File Offset: 0x00050EF3
	protected UICanvas()
	{
	}

	// Token: 0x060012B6 RID: 4790 RVA: 0x00052D0D File Offset: 0x00050F0D
	protected int getCurrentSelectedButtonIndex()
	{
		return this.currentSelectedButton;
	}

	// Token: 0x060012B7 RID: 4791 RVA: 0x00052D18 File Offset: 0x00050F18
	protected void setCurrentSelectedButtonIndexToHoveredElement()
	{
		int num = 0;
		using (List<UIElement>.Enumerator enumerator = this.getScrollableElements().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.getHover())
				{
					this.setCurrentSelectedButton(num);
					this.boundCurrentSelectedButtons();
					break;
				}
				num++;
			}
		}
	}

	// Token: 0x060012B8 RID: 4792 RVA: 0x00052D80 File Offset: 0x00050F80
	public virtual void incrementCurrentSelectedButton()
	{
		int num = 0;
		using (List<UIElement>.Enumerator enumerator = this.getScrollableElements().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.getHover())
				{
					this.setCurrentSelectedButton(num + 1);
					this.boundCurrentSelectedButtons();
					return;
				}
				num++;
			}
		}
		this.boundCurrentSelectedButtons();
	}

	// Token: 0x060012B9 RID: 4793 RVA: 0x00052DF0 File Offset: 0x00050FF0
	public virtual void decrementCurrentSelectedButton()
	{
		int num = 0;
		using (List<UIElement>.Enumerator enumerator = this.getScrollableElements().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.getHover())
				{
					this.setCurrentSelectedButton(num - 1);
					this.boundCurrentSelectedButtons();
					return;
				}
				num++;
			}
		}
		this.boundCurrentSelectedButtons();
	}

	// Token: 0x060012BA RID: 4794 RVA: 0x00052E60 File Offset: 0x00051060
	public virtual bool canControllerScrollDown()
	{
		return this.getCurrentSelectedButtonIndex() > 0;
	}

	// Token: 0x060012BB RID: 4795 RVA: 0x00052E6B File Offset: 0x0005106B
	public virtual bool canControllerScrollUp()
	{
		return this.getCurrentSelectedButtonIndex() < this.getScrollableElements().Count - 1;
	}

	// Token: 0x060012BC RID: 4796 RVA: 0x00052E82 File Offset: 0x00051082
	public virtual bool canControllerScrollRight()
	{
		return false;
	}

	// Token: 0x060012BD RID: 4797 RVA: 0x00052E85 File Offset: 0x00051085
	public virtual bool canControllerScrollLeft()
	{
		return false;
	}

	// Token: 0x060012BE RID: 4798 RVA: 0x00052E88 File Offset: 0x00051088
	protected void setCurrentSelectedButton(int newValue)
	{
		this.currentSelectedButton = newValue;
	}

	// Token: 0x060012BF RID: 4799 RVA: 0x00052E94 File Offset: 0x00051094
	private void boundCurrentSelectedButtons()
	{
		int count = this.getScrollableElements().Count;
		if (this.getCurrentSelectedButtonIndex() < 0)
		{
			this.setCurrentSelectedButton(0);
			return;
		}
		if (this.getCurrentSelectedButtonIndex() >= count)
		{
			this.setCurrentSelectedButton(count - 1);
		}
	}

	// Token: 0x060012C0 RID: 4800 RVA: 0x00052ED0 File Offset: 0x000510D0
	public virtual void controllerScrollSidewaysRight()
	{
	}

	// Token: 0x060012C1 RID: 4801 RVA: 0x00052ED2 File Offset: 0x000510D2
	public virtual void controllerScrollSidewaysLeft()
	{
	}

	// Token: 0x060012C2 RID: 4802 RVA: 0x00052ED4 File Offset: 0x000510D4
	public virtual UIElement getCurrentControllerSelectedElement()
	{
		List<UIElement> scrollableElements = this.getScrollableElements();
		if (scrollableElements.Count == 0)
		{
			return null;
		}
		this.boundCurrentSelectedButtons();
		return scrollableElements[this.currentSelectedButton];
	}

	// Token: 0x060012C3 RID: 4803 RVA: 0x00052F04 File Offset: 0x00051104
	public virtual List<UIElement> getScrollableElements()
	{
		List<UIElement> list = new List<UIElement>();
		foreach (UIElement uielement in this.getElements())
		{
			if (uielement.useableAsScrollableElement())
			{
				list.Add(uielement);
			}
		}
		return list;
	}

	// Token: 0x060012C4 RID: 4804 RVA: 0x00052F68 File Offset: 0x00051168
	public void clearCurrentSelectedButton()
	{
		this.setCurrentSelectedButton(0);
	}

	// Token: 0x060012C5 RID: 4805 RVA: 0x00052F71 File Offset: 0x00051171
	public override void setPosition(SkaldPoint2D position)
	{
		if (position == null)
		{
			return;
		}
		base.setPosition(position);
		this.alignElements();
	}

	// Token: 0x060012C6 RID: 4806 RVA: 0x00052F84 File Offset: 0x00051184
	public List<UIElement> getElements()
	{
		return this.elements;
	}

	// Token: 0x060012C7 RID: 4807 RVA: 0x00052F8C File Offset: 0x0005118C
	public void removeLastElement()
	{
		if (this.getElements() == null || this.getElements().Count == 0)
		{
			return;
		}
		this.getElements().RemoveAt(this.getElements().Count - 1);
		this.alignElements();
	}

	// Token: 0x060012C8 RID: 4808 RVA: 0x00052FC4 File Offset: 0x000511C4
	public virtual void add(UIElement element)
	{
		if (element == null)
		{
			return;
		}
		if (this.getElements().Contains(element))
		{
			Debug.LogError("Adding elements twice!");
		}
		this.getElements().Add(element);
		element.setYieldToTooltips(this.yieldToTooltips);
		element.setReveal(this.isRevealed());
		this.alignElements();
	}

	// Token: 0x060012C9 RID: 4809 RVA: 0x00053018 File Offset: 0x00051218
	public override void setYieldToTooltips(bool value)
	{
		base.setYieldToTooltips(value);
		foreach (UIElement uielement in this.getElements())
		{
			uielement.setYieldToTooltips(value);
		}
	}

	// Token: 0x060012CA RID: 4810 RVA: 0x00053070 File Offset: 0x00051270
	public virtual void clearElements()
	{
		this.getElements().Clear();
	}

	// Token: 0x060012CB RID: 4811 RVA: 0x00053080 File Offset: 0x00051280
	public override void setMouseInteraction(bool hover, bool leftClicked, bool rightClicked, bool leftUp, bool rightUp, bool doubleClick)
	{
		base.setMouseInteraction(hover, leftClicked, rightClicked, leftUp, rightUp, doubleClick);
		foreach (UIElement uielement in this.getElements())
		{
			uielement.setMouseInteraction(hover, leftClicked, rightClicked, leftUp, rightUp, doubleClick);
		}
	}

	// Token: 0x060012CC RID: 4812 RVA: 0x000530E8 File Offset: 0x000512E8
	protected int getDrawXAnchor()
	{
		if (this.alignments.horizontalAlignment == UIElement.Alignments.HorizontalAlignments.Center)
		{
			return this.getX() + this.padding.left + this.getBaseWidth() / 2;
		}
		if (this.alignments.horizontalAlignment == UIElement.Alignments.HorizontalAlignments.Right)
		{
			return this.getX() + this.getWidth();
		}
		return this.getX();
	}

	// Token: 0x060012CD RID: 4813 RVA: 0x00053144 File Offset: 0x00051344
	public override void draw(TextureTools.TextureData targetTexture)
	{
		base.draw(targetTexture);
		if (!this.revealed)
		{
			return;
		}
		foreach (UIElement uielement in this.getElements())
		{
			uielement.draw(targetTexture);
		}
	}

	// Token: 0x060012CE RID: 4814 RVA: 0x000531A8 File Offset: 0x000513A8
	public override void reveal()
	{
		base.reveal();
		int num = 0;
		int num2 = 2;
		foreach (UIElement uielement in this.getElements())
		{
			if (!uielement.isRevealed())
			{
				uielement.reveal();
				num++;
				if (num >= num2)
				{
					break;
				}
			}
		}
	}

	// Token: 0x060012CF RID: 4815 RVA: 0x00053218 File Offset: 0x00051418
	public override void setReveal(bool reveal)
	{
		base.setReveal(reveal);
		foreach (UIElement uielement in this.getElements())
		{
			uielement.setReveal(reveal);
		}
	}

	// Token: 0x060012D0 RID: 4816 RVA: 0x00053270 File Offset: 0x00051470
	public override bool isRevealed()
	{
		if (!base.isRevealed())
		{
			return false;
		}
		using (List<UIElement>.Enumerator enumerator = this.getElements().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.isRevealed())
				{
					return false;
				}
			}
		}
		return this.revealed;
	}

	// Token: 0x060012D1 RID: 4817 RVA: 0x000532D8 File Offset: 0x000514D8
	public override void alignElements()
	{
		foreach (UIElement uielement in this.getElements())
		{
			uielement.alignElements();
		}
	}

	// Token: 0x04000489 RID: 1161
	private List<UIElement> elements = new List<UIElement>();

	// Token: 0x0400048A RID: 1162
	private int currentSelectedButton = -1;
}
