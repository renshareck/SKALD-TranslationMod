using System;

// Token: 0x020000AB RID: 171
public abstract class ListSheetBaseState : InfoBaseState
{
	// Token: 0x06000AC9 RID: 2761 RVA: 0x00034082 File Offset: 0x00032282
	protected ListSheetBaseState(DataControl dataControl) : base(dataControl)
	{
		this.createGUI();
		if (dataControl.isCombatActive())
		{
			base.disableCharacterSwap();
		}
	}

	// Token: 0x06000ACA RID: 2762 RVA: 0x0003409F File Offset: 0x0003229F
	protected override void createGUI()
	{
		this.guiControl = new GUIControlSheet();
	}

	// Token: 0x06000ACB RID: 2763 RVA: 0x000340AC File Offset: 0x000322AC
	public override void update()
	{
		base.update();
		if (this.list == null)
		{
			return;
		}
		if (this.guiControl.getListButtonPressIndex() != -1)
		{
			this.list.getObjectByPageIndex(this.guiControl.getListButtonPressIndex());
			this.setMainTextBuffer(this.list.getCurrentObjectFullDescriptionAndHeader());
		}
		this.updateScrollBar();
	}

	// Token: 0x06000ACC RID: 2764 RVA: 0x00034104 File Offset: 0x00032304
	protected void updateScrollBar()
	{
		if (this.list == null)
		{
			return;
		}
		int num = this.guiControl.updateLeftScrollBarAndReturnIndex(this.list.getCount());
		if (num != -1)
		{
			this.list.setScrollIndex(num);
		}
	}

	// Token: 0x06000ACD RID: 2765 RVA: 0x00034141 File Offset: 0x00032341
	protected virtual void setNextObject(int index)
	{
		if (this.list == null)
		{
			return;
		}
		this.list.setNextObject(index);
		this.setMainTextBuffer(this.list.getCurrentObjectFullDescriptionAndHeader());
	}

	// Token: 0x06000ACE RID: 2766 RVA: 0x00034169 File Offset: 0x00032369
	protected override void setGUIData()
	{
		base.setGUIData();
		if (this.list != null)
		{
			this.guiControl.setListButtons(this.list.getScrolledStringList());
		}
	}

	// Token: 0x040002DD RID: 733
	protected SkaldObjectList list;
}
