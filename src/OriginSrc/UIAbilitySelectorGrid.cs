using System;
using System.Collections.Generic;

// Token: 0x02000136 RID: 310
public abstract class UIAbilitySelectorGrid : UICanvasVertical
{
	// Token: 0x0600121D RID: 4637 RVA: 0x0005040C File Offset: 0x0004E60C
	public UIAbilitySelectorGrid(List<UIButtonControlBase.ButtonData> buttonData, int x, int y, int width) : base(x, y, 0, 0)
	{
		this.rowWidth = width;
		this.stretchHorizontal = true;
		this.stretchVertical = true;
		this.alignments.verticalAlignment = UIElement.Alignments.VerticalAlignments.Bottom;
		this.setButtons(buttonData);
	}

	// Token: 0x0600121E RID: 4638 RVA: 0x00050470 File Offset: 0x0004E670
	public override List<UIElement> getScrollableElements()
	{
		List<UIElement> list = new List<UIElement>();
		foreach (UIElement uielement in base.getElements())
		{
			if (uielement is UIAbilitySelectorGrid.UIAbilityGridRow)
			{
				foreach (UIElement item in (uielement as UIAbilitySelectorGrid.UIAbilityGridRow).getElements())
				{
					list.Add(item);
				}
			}
		}
		return list;
	}

	// Token: 0x0600121F RID: 4639 RVA: 0x00050514 File Offset: 0x0004E714
	public override void incrementCurrentSelectedButton()
	{
		base.setCurrentSelectedButton(base.getCurrentSelectedButtonIndex() + this.rowWidth);
	}

	// Token: 0x06001220 RID: 4640 RVA: 0x00050529 File Offset: 0x0004E729
	public override void decrementCurrentSelectedButton()
	{
		base.setCurrentSelectedButton(base.getCurrentSelectedButtonIndex() - this.rowWidth);
	}

	// Token: 0x06001221 RID: 4641 RVA: 0x0005053E File Offset: 0x0004E73E
	public override void controllerScrollSidewaysLeft()
	{
		base.decrementCurrentSelectedButton();
	}

	// Token: 0x06001222 RID: 4642 RVA: 0x00050546 File Offset: 0x0004E746
	public override void controllerScrollSidewaysRight()
	{
		base.incrementCurrentSelectedButton();
	}

	// Token: 0x06001223 RID: 4643 RVA: 0x00050550 File Offset: 0x0004E750
	public override bool canControllerScrollDown()
	{
		int count = this.getScrollableElements().Count;
		return base.getCurrentSelectedButtonIndex() + count % this.rowWidth < count;
	}

	// Token: 0x06001224 RID: 4644 RVA: 0x0005057B File Offset: 0x0004E77B
	public override bool canControllerScrollUp()
	{
		return base.getCurrentSelectedButtonIndex() >= this.rowWidth;
	}

	// Token: 0x06001225 RID: 4645 RVA: 0x0005058E File Offset: 0x0004E78E
	protected virtual void setButtons(List<UIButtonControlBase.ButtonData> buttonData)
	{
		this.clearElements();
		this.addMainGrid(buttonData);
		this.alignElements();
	}

	// Token: 0x06001226 RID: 4646 RVA: 0x000505A4 File Offset: 0x0004E7A4
	protected void addMainGrid(List<UIButtonControlBase.ButtonData> buttonData)
	{
		int num = 0;
		int num2 = 0;
		UIAbilitySelectorGrid.UIAbilityGridRow uiabilityGridRow = new UIAbilitySelectorGrid.UIAbilityGridRow();
		this.add(uiabilityGridRow);
		foreach (UIButtonControlBase.ButtonData buttonData2 in buttonData)
		{
			uiabilityGridRow.add(buttonData2, num2);
			num++;
			num2++;
			if (num >= this.rowWidth)
			{
				uiabilityGridRow = new UIAbilitySelectorGrid.UIAbilityGridRow();
				this.add(uiabilityGridRow);
				num = 0;
			}
		}
	}

	// Token: 0x06001227 RID: 4647 RVA: 0x00050628 File Offset: 0x0004E828
	protected void updateMainGridImage(List<UIButtonControlBase.ButtonData> buttonData)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		List<UIButtonControlBase.ButtonData> list = new List<UIButtonControlBase.ButtonData>();
		UIAbilitySelectorGrid.UIAbilityGridRow uiabilityGridRow = base.getElements()[num3] as UIAbilitySelectorGrid.UIAbilityGridRow;
		foreach (UIButtonControlBase.ButtonData item in buttonData)
		{
			list.Add(item);
			num++;
			num2++;
			if (num >= this.rowWidth)
			{
				uiabilityGridRow = (base.getElements()[num3] as UIAbilitySelectorGrid.UIAbilityGridRow);
				uiabilityGridRow.updateButtonTexture(list);
				list = new List<UIButtonControlBase.ButtonData>();
				num3++;
				num = 0;
			}
		}
		if (list.Count != 0)
		{
			uiabilityGridRow = (base.getElements()[num3] as UIAbilitySelectorGrid.UIAbilityGridRow);
			if (uiabilityGridRow != null)
			{
				uiabilityGridRow.updateButtonTexture(list);
			}
		}
	}

	// Token: 0x06001228 RID: 4648 RVA: 0x000506F8 File Offset: 0x0004E8F8
	public bool hoveringOverButtons()
	{
		return this.hoverIndex != -1;
	}

	// Token: 0x06001229 RID: 4649 RVA: 0x00050706 File Offset: 0x0004E906
	public int getLeftClickIndex()
	{
		return this.leftClickedIndex;
	}

	// Token: 0x0600122A RID: 4650 RVA: 0x0005070E File Offset: 0x0004E90E
	public int getRightClickIndex()
	{
		return this.rightClickedIndex;
	}

	// Token: 0x0600122B RID: 4651 RVA: 0x00050716 File Offset: 0x0004E916
	public int getHoverIndex()
	{
		return this.hoverIndex;
	}

	// Token: 0x0600122C RID: 4652 RVA: 0x0005071E File Offset: 0x0004E91E
	private void clearIndices()
	{
		this.leftClickedIndex = -1;
		this.rightClickedIndex = -1;
		this.hoverIndex = -1;
	}

	// Token: 0x0600122D RID: 4653 RVA: 0x00050738 File Offset: 0x0004E938
	public void update()
	{
		this.clearIndices();
		foreach (UIElement uielement in base.getElements())
		{
			if (uielement is UIAbilitySelectorGrid.UIAbilityGridRow)
			{
				UIAbilitySelectorGrid.UIAbilityGridRow uiabilityGridRow = uielement as UIAbilitySelectorGrid.UIAbilityGridRow;
				uiabilityGridRow.update();
				if (uiabilityGridRow.getLeftClickIndex() != -1)
				{
					this.leftClickedIndex = uiabilityGridRow.getLeftClickIndex();
				}
				if (uiabilityGridRow.getRightClickIndex() != -1)
				{
					this.rightClickedIndex = uiabilityGridRow.getRightClickIndex();
				}
				if (uiabilityGridRow.getHoverIndex() != -1)
				{
					this.hoverIndex = uiabilityGridRow.getHoverIndex();
				}
			}
		}
	}

	// Token: 0x04000452 RID: 1106
	private int rowWidth = 3;

	// Token: 0x04000453 RID: 1107
	private int leftClickedIndex = -1;

	// Token: 0x04000454 RID: 1108
	private int rightClickedIndex = -1;

	// Token: 0x04000455 RID: 1109
	private int hoverIndex = -1;

	// Token: 0x04000456 RID: 1110
	protected int currentSelectedButton = -1;

	// Token: 0x02000281 RID: 641
	private class UIAbilityGridRow : UICanvasHorizontal
	{
		// Token: 0x06001A7B RID: 6779 RVA: 0x00072C90 File Offset: 0x00070E90
		public UIAbilityGridRow()
		{
			this.stretchHorizontal = true;
			this.stretchVertical = true;
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x00072CBB File Offset: 0x00070EBB
		public void add(UIButtonControlBase.ButtonData buttonData, int count)
		{
			this.add(new UIAbilitySelectorGrid.UIAbilityGridRow.UIAbilityButton(buttonData.texture, count));
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x00072CD0 File Offset: 0x00070ED0
		public void updateButtonTexture(List<UIButtonControlBase.ButtonData> buttonData)
		{
			int num = 0;
			List<UIElement> list = new List<UIElement>();
			foreach (UIElement uielement in base.getElements())
			{
				UIAbilitySelectorGrid.UIAbilityGridRow.UIAbilityButton uiabilityButton = (UIAbilitySelectorGrid.UIAbilityGridRow.UIAbilityButton)uielement;
				if (num < buttonData.Count)
				{
					uiabilityButton.setTexture(buttonData[num].texture);
				}
				else
				{
					list.Add(uiabilityButton);
				}
				num++;
			}
			foreach (UIElement item in list)
			{
				base.getElements().Remove(item);
			}
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x00072D98 File Offset: 0x00070F98
		private void clearIndices()
		{
			this.leftClickedIndex = -1;
			this.rightClickedIndex = -1;
			this.hoverIndex = -1;
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x00072DAF File Offset: 0x00070FAF
		public int getLeftClickIndex()
		{
			return this.leftClickedIndex;
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x00072DB7 File Offset: 0x00070FB7
		public int getRightClickIndex()
		{
			return this.rightClickedIndex;
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x00072DBF File Offset: 0x00070FBF
		public int getHoverIndex()
		{
			return this.hoverIndex;
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x00072DC8 File Offset: 0x00070FC8
		public void update()
		{
			this.clearIndices();
			foreach (UIElement uielement in base.getElements())
			{
				UIAbilitySelectorGrid.UIAbilityGridRow.UIAbilityButton uiabilityButton = (UIAbilitySelectorGrid.UIAbilityGridRow.UIAbilityButton)uielement;
				uiabilityButton.updateMouseInteraction();
				if (uiabilityButton.getHover())
				{
					this.hoverIndex = uiabilityButton.getCount();
				}
				if (uiabilityButton.getLeftUp())
				{
					this.leftClickedIndex = uiabilityButton.getCount();
				}
				if (uiabilityButton.getRightUp())
				{
					this.rightClickedIndex = uiabilityButton.getCount();
				}
			}
		}

		// Token: 0x0400098E RID: 2446
		private int leftClickedIndex = -1;

		// Token: 0x0400098F RID: 2447
		private int rightClickedIndex = -1;

		// Token: 0x04000990 RID: 2448
		private int hoverIndex = -1;

		// Token: 0x020003D3 RID: 979
		private class UIAbilityButton : UIElement
		{
			// Token: 0x06001D5E RID: 7518 RVA: 0x0007BE9C File Offset: 0x0007A09C
			public UIAbilityButton(TextureTools.TextureData texture, int count)
			{
				this.count = count;
				this.setTexture(texture);
				this.backgroundTexture = UIAbilitySelectorGrid.UIAbilityGridRow.UIAbilityButton.bgTexture;
				this.backgroundTextureHover = UIAbilitySelectorGrid.UIAbilityGridRow.UIAbilityButton.bgTextureHover;
				base.setDimensions(0, 0, this.backgroundTexture.width - 1, this.backgroundTexture.height - 1);
			}

			// Token: 0x06001D5F RID: 7519 RVA: 0x0007BEF5 File Offset: 0x0007A0F5
			public void setTexture(TextureTools.TextureData texture)
			{
				this.foregroundTexture = TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/MenuBarBackground").createCopy();
				if (texture != null)
				{
					TextureTools.applyOverlay(this.foregroundTexture, texture, 2, 2);
				}
			}

			// Token: 0x06001D60 RID: 7520 RVA: 0x0007BF1D File Offset: 0x0007A11D
			public int getCount()
			{
				return this.count;
			}

			// Token: 0x04000C5D RID: 3165
			private int count;

			// Token: 0x04000C5E RID: 3166
			private static TextureTools.TextureData bgTexture = TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/MenuBarBox");

			// Token: 0x04000C5F RID: 3167
			private static TextureTools.TextureData bgTextureHover = TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/MenuBarBoxHover");
		}
	}
}
