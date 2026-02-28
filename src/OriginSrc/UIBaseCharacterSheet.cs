using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000138 RID: 312
public abstract class UIBaseCharacterSheet : UICanvasHorizontal
{
	// Token: 0x06001230 RID: 4656 RVA: 0x0005098D File Offset: 0x0004EB8D
	protected UIBaseCharacterSheet()
	{
		this.initialize();
	}

	// Token: 0x06001231 RID: 4657 RVA: 0x0005099C File Offset: 0x0004EB9C
	public override List<UIElement> getScrollableElements()
	{
		List<UIElement> list = new List<UIElement>();
		if (this.entry1 != null)
		{
			foreach (UIElement item in this.entry1.getScrollableElements())
			{
				list.Add(item);
			}
		}
		if (this.entry2 != null)
		{
			foreach (UIElement item2 in this.entry2.getScrollableElements())
			{
				list.Add(item2);
			}
		}
		if (this.entry3 != null)
		{
			foreach (UIElement item3 in this.entry3.getScrollableElements())
			{
				list.Add(item3);
			}
		}
		if (this.entry4 != null)
		{
			foreach (UIElement item4 in this.entry4.getScrollableElements())
			{
				list.Add(item4);
			}
		}
		if (this.entry5 != null)
		{
			foreach (UIElement item5 in this.entry5.getScrollableElements())
			{
				list.Add(item5);
			}
		}
		return list;
	}

	// Token: 0x06001232 RID: 4658 RVA: 0x00050B48 File Offset: 0x0004ED48
	protected virtual void addColumns()
	{
		this.leftColumn = new UICanvasVertical();
		this.leftColumn.setWidth(116);
		this.leftColumn.padding.left = 4;
		this.add(this.leftColumn);
		this.rightColumn = new UICanvasVertical();
		this.add(this.rightColumn);
	}

	// Token: 0x06001233 RID: 4659 RVA: 0x00050BA4 File Offset: 0x0004EDA4
	protected virtual void addEntries()
	{
		this.entry1 = new UIBaseCharacterSheet.SheetEntry();
		this.leftColumn.add(this.entry1);
		this.entry2 = new UIBaseCharacterSheet.SheetEntry();
		this.leftColumn.add(this.entry2);
		this.entry3 = new UIBaseCharacterSheet.SheetEntry();
		this.rightColumn.add(this.entry3);
		this.entry4 = new UIBaseCharacterSheet.SheetEntry();
		this.rightColumn.add(this.entry4);
		this.entry5 = new UIBaseCharacterSheet.SheetEntry();
		this.rightColumn.add(this.entry5);
	}

	// Token: 0x06001234 RID: 4660 RVA: 0x00050C3D File Offset: 0x0004EE3D
	public void initialize()
	{
		this.addColumns();
		this.addEntries();
	}

	// Token: 0x06001235 RID: 4661 RVA: 0x00050C4B File Offset: 0x0004EE4B
	public void clearCurrentObject()
	{
		this.currentObject = null;
	}

	// Token: 0x06001236 RID: 4662 RVA: 0x00050C54 File Offset: 0x0004EE54
	public string getCurrentObjectFullDescriptionAndHeader()
	{
		if (this.currentObject != null)
		{
			return this.currentObject.getFullDescriptionAndHeader();
		}
		return "\n\n\nSelect an entry!";
	}

	// Token: 0x06001237 RID: 4663 RVA: 0x00050C6F File Offset: 0x0004EE6F
	public virtual void updateEntry1(SkaldDataList data)
	{
		this.entry1.update(data);
		if (this.entry1.getCurrentObject() != null)
		{
			this.currentObject = this.entry1.getCurrentObject();
		}
	}

	// Token: 0x06001238 RID: 4664 RVA: 0x00050C9B File Offset: 0x0004EE9B
	public virtual void updateEntry2(SkaldDataList data)
	{
		this.entry2.update(data);
		if (this.entry2.getCurrentObject() != null)
		{
			this.currentObject = this.entry2.getCurrentObject();
		}
	}

	// Token: 0x06001239 RID: 4665 RVA: 0x00050CC7 File Offset: 0x0004EEC7
	public void updateEntry3(SkaldDataList data)
	{
		this.entry3.update(data);
		if (this.entry3.getCurrentObject() != null)
		{
			this.currentObject = this.entry3.getCurrentObject();
		}
	}

	// Token: 0x0600123A RID: 4666 RVA: 0x00050CF3 File Offset: 0x0004EEF3
	public void updateEntry4(SkaldDataList data)
	{
		this.entry4.update(data);
		if (this.entry4.getCurrentObject() != null)
		{
			this.currentObject = this.entry4.getCurrentObject();
		}
	}

	// Token: 0x0600123B RID: 4667 RVA: 0x00050D1F File Offset: 0x0004EF1F
	public void updateEntry5(SkaldDataList data)
	{
		this.entry5.update(data);
		if (this.entry5.getCurrentObject() != null)
		{
			this.currentObject = this.entry5.getCurrentObject();
		}
	}

	// Token: 0x0400045D RID: 1117
	protected UICanvasVertical rightColumn;

	// Token: 0x0400045E RID: 1118
	protected UICanvasVertical leftColumn;

	// Token: 0x0400045F RID: 1119
	protected UIBaseCharacterSheet.SheetEntry entry1;

	// Token: 0x04000460 RID: 1120
	protected UIBaseCharacterSheet.SheetEntry entry2;

	// Token: 0x04000461 RID: 1121
	protected UIBaseCharacterSheet.SheetEntry entry3;

	// Token: 0x04000462 RID: 1122
	protected UIBaseCharacterSheet.SheetEntry entry4;

	// Token: 0x04000463 RID: 1123
	protected UIBaseCharacterSheet.SheetEntry entry5;

	// Token: 0x04000464 RID: 1124
	protected SkaldBaseObject currentObject;

	// Token: 0x02000282 RID: 642
	protected class SheetEntry : UICanvasVertical
	{
		// Token: 0x06001A83 RID: 6787 RVA: 0x00072E64 File Offset: 0x00071064
		public SheetEntry(int width)
		{
			this.stretchVertical = true;
			this.buttons = new ListButtonControl(0, 0, width, 0);
			this.buttons.setStretchVertical(true);
			this.buttons.setStretchHorizontal(true);
			this.add(this.buttons);
			this.padding.bottom = 10;
			this.padding.right = 5;
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x00072ECA File Offset: 0x000710CA
		public SheetEntry() : this(107)
		{
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x00072ED4 File Offset: 0x000710D4
		public override List<UIElement> getScrollableElements()
		{
			return this.buttons.getScrollableElements();
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x00072EE1 File Offset: 0x000710E1
		public void setButtonTextShadowColor(Color32 color)
		{
			if (this.buttons != null)
			{
				this.buttons.setButtonTextShadowColor(color);
			}
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x00072EF7 File Offset: 0x000710F7
		public void toggleCenterText()
		{
			if (this.buttons != null)
			{
				this.buttons.toggleCenterText();
			}
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x00072F0C File Offset: 0x0007110C
		public SkaldBaseObject getCurrentObject()
		{
			return this.currentObject;
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x00072F14 File Offset: 0x00071114
		public void setTabWidth(int width)
		{
			this.buttons.setTabWidth(width);
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x00072F24 File Offset: 0x00071124
		public virtual void update(SkaldDataList data)
		{
			this.currentObject = null;
			this.buttons.update();
			this.buttons.setButtons(data);
			int buttonPressIndexLeft = this.buttons.getButtonPressIndexLeft();
			if (buttonPressIndexLeft != -1 && buttonPressIndexLeft < data.getObjectList().Count)
			{
				this.currentObject = data.getObjectList()[buttonPressIndexLeft];
			}
		}

		// Token: 0x04000991 RID: 2449
		protected ListButtonControl buttons;

		// Token: 0x04000992 RID: 2450
		protected SkaldBaseObject currentObject;
	}

	// Token: 0x02000283 RID: 643
	protected class EditorSheetEntry : UIBaseCharacterSheet.SheetEntry
	{
		// Token: 0x06001A8B RID: 6795 RVA: 0x00072F80 File Offset: 0x00071180
		public EditorSheetEntry()
		{
			this.stretchVertical = true;
			this.padding.bottom = 10;
			this.padding.right = 5;
			this.columns = new UICanvasHorizontal();
			this.columns.stretchVertical = true;
			this.add(this.columns);
			this.buttonColumn = new UICanvasVertical();
			this.buttonColumn.stretchVertical = true;
			this.buttonColumn.stretchHorizontal = true;
			this.buttonColumn.padding.right = 4;
			this.columns.add(this.buttonColumn);
			this.minusColumn = new UICanvasVertical();
			this.minusColumn.stretchVertical = true;
			this.minusColumn.stretchHorizontal = true;
			this.minusColumn.padding.top = 0;
			this.minusColumn.padding.right = 3;
			this.columns.add(this.minusColumn);
			this.pointBlock = new UITextBlock(0, 7);
			this.pointBlock.padding.left = 9;
			this.pointBlock.padding.bottom = 3;
			this.minusColumn.add(this.pointBlock);
			this.plusColumn = new UICanvasVertical();
			this.plusColumn.stretchVertical = true;
			this.plusColumn.stretchHorizontal = true;
			this.plusColumn.padding.top = 10;
			this.columns.add(this.plusColumn);
			this.buttons = new ListButtonControl(0, 0, 80, 0);
			this.buttons.setStretchVertical(true);
			this.buttonColumn.add(this.buttons);
			this.minusButtons = new ImageButtonControl(0, 0, 9, 6, "StatArrowLeft");
			this.minusColumn.add(this.minusButtons);
			this.plusButtons = new ImageButtonControl(0, 0, 9, 6, "StatArrowRight");
			this.plusColumn.add(this.plusButtons);
		}

		// Token: 0x06001A8C RID: 6796 RVA: 0x00073178 File Offset: 0x00071378
		public override void update(SkaldDataList data)
		{
			base.update(data);
			this.plusObject = null;
			this.minusObject = null;
			this.minusButtons.update();
			this.plusButtons.update();
			this.minusButtons.setButtons(data);
			this.plusButtons.setButtons(data);
			int hoverIndex = this.plusButtons.getHoverIndex();
			if (hoverIndex != -1 && hoverIndex + 1 < data.getObjectList().Count)
			{
				this.currentObject = data.getObjectList()[hoverIndex + 1];
			}
			int hoverIndex2 = this.minusButtons.getHoverIndex();
			if (hoverIndex2 != -1 && hoverIndex2 + 1 < data.getObjectList().Count)
			{
				this.currentObject = data.getObjectList()[hoverIndex2 + 1];
			}
			int buttonPressIndexLeft = this.minusButtons.getButtonPressIndexLeft();
			if (buttonPressIndexLeft != -1 && buttonPressIndexLeft + 1 < data.getObjectList().Count)
			{
				this.minusObject = data.getObjectList()[buttonPressIndexLeft + 1];
				this.currentObject = this.minusObject;
			}
			int buttonPressIndexLeft2 = this.plusButtons.getButtonPressIndexLeft();
			if (buttonPressIndexLeft2 != -1 && buttonPressIndexLeft2 + 1 < data.getObjectList().Count)
			{
				this.plusObject = data.getObjectList()[buttonPressIndexLeft2 + 1];
				this.currentObject = this.plusObject;
			}
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x000732B0 File Offset: 0x000714B0
		public SkaldBaseObject getMinusObject()
		{
			return this.minusObject;
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x000732B8 File Offset: 0x000714B8
		public SkaldBaseObject getPlusObject()
		{
			return this.plusObject;
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x000732C0 File Offset: 0x000714C0
		public void setPointValue(int value)
		{
			this.pointBlock.setContent(value.ToString());
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x000732D4 File Offset: 0x000714D4
		public override void controllerScrollSidewaysLeft()
		{
			this.controllerScrollToPlusButton = false;
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x000732DD File Offset: 0x000714DD
		public override void controllerScrollSidewaysRight()
		{
			this.controllerScrollToPlusButton = true;
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x000732E6 File Offset: 0x000714E6
		public override List<UIElement> getScrollableElements()
		{
			if (this.controllerScrollToPlusButton)
			{
				return this.plusButtons.getScrollableElements();
			}
			return this.minusButtons.getScrollableElements();
		}

		// Token: 0x04000993 RID: 2451
		private UICanvasHorizontal columns;

		// Token: 0x04000994 RID: 2452
		private UICanvasVertical buttonColumn;

		// Token: 0x04000995 RID: 2453
		private UICanvasVertical minusColumn;

		// Token: 0x04000996 RID: 2454
		private UICanvasVertical plusColumn;

		// Token: 0x04000997 RID: 2455
		private ImageButtonControl minusButtons;

		// Token: 0x04000998 RID: 2456
		private ImageButtonControl plusButtons;

		// Token: 0x04000999 RID: 2457
		private SkaldBaseObject plusObject;

		// Token: 0x0400099A RID: 2458
		private SkaldBaseObject minusObject;

		// Token: 0x0400099B RID: 2459
		private UITextBlock pointBlock;

		// Token: 0x0400099C RID: 2460
		private bool controllerScrollToPlusButton = true;
	}
}
