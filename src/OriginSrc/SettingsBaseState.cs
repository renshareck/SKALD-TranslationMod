using System;
using SkaldEnums;

// Token: 0x0200009B RID: 155
public class SettingsBaseState : BaseMenuState
{
	// Token: 0x06000A94 RID: 2708 RVA: 0x00033115 File Offset: 0x00031315
	public SettingsBaseState(DataControl dataControl) : base(dataControl)
	{
		this.createGUI();
		base.disableCharacterSwap();
		this.setStateSelector();
		this.setGUIData();
	}

	// Token: 0x06000A95 RID: 2709 RVA: 0x0003313D File Offset: 0x0003133D
	protected virtual void setList()
	{
	}

	// Token: 0x06000A96 RID: 2710 RVA: 0x00033140 File Offset: 0x00031340
	protected override void setStateSelector()
	{
		base.setStateSelector();
		this.stateSelector.add(SkaldStates.SettingsGameplay, "Gameplay");
		this.stateSelector.add(SkaldStates.SettingsDifficulty, "Difficulty");
		this.stateSelector.add(SkaldStates.SettingsVideo, "Display");
		this.stateSelector.add(SkaldStates.SettingsAudio, "Audio");
		this.stateSelector.add(SkaldStates.SettingsKeyBindings, "Controls");
		this.stateSelector.add(SkaldStates.SettingsFonts, "Fonts");
	}

	// Token: 0x06000A97 RID: 2711 RVA: 0x000331BF File Offset: 0x000313BF
	protected override void createGUI()
	{
		this.guiControl = new GUIControlSettings();
	}

	// Token: 0x06000A98 RID: 2712 RVA: 0x000331CC File Offset: 0x000313CC
	public override void update()
	{
		base.update();
		if (this.numericInputIndex == 0)
		{
			this.clearAndGoToOverland();
		}
		else if (this.UIInputIndex != -1)
		{
			this.setTargetState(this.stateSelector.getStateFromIndex(this.UIInputIndex));
		}
		else if (SkaldIO.getStateCarouselRightButtonPressed())
		{
			base.setNextState(1);
		}
		else if (SkaldIO.getStateCarouselLeftButtonPressed())
		{
			base.setNextState(-1);
		}
		if (this.list == null)
		{
			return;
		}
		this.updateScrollBar();
		if (this.guiControl.getListButtonPressIndex() != -1)
		{
			this.list.getObjectByPageIndex(this.guiControl.getListButtonPressIndex());
		}
		this.setMainTextBuffer(this.list.getCurrentObjectFullDescriptionAndHeader());
	}

	// Token: 0x06000A99 RID: 2713 RVA: 0x00033274 File Offset: 0x00031474
	protected override void clearAndGoToOverland()
	{
		this.dataControl.settingsSave();
		base.clearAndGoToOverland();
	}

	// Token: 0x06000A9A RID: 2714 RVA: 0x00033288 File Offset: 0x00031488
	protected void updateScrollBar()
	{
		if (this.list == null)
		{
			return;
		}
		int num = this.guiControl.updateLeftScrollBarAndReturnIndex(this.list.getCount());
		if (this.leftScrollIndex != num)
		{
			this.leftScrollIndex = num;
			if (this.leftScrollIndex != -1)
			{
				this.list.setScrollIndex(this.leftScrollIndex);
			}
			this.setList();
		}
	}

	// Token: 0x06000A9B RID: 2715 RVA: 0x000332E8 File Offset: 0x000314E8
	protected override void setGUIData()
	{
		base.setGUIData();
		this.guiControl.setSheetDescription(this.getMainTextBuffer());
		if (this.list != null)
		{
			this.guiControl.setListButtons(this.list.getScrolledStringList());
			this.guiControl.setSheetHeader(this.list.getListName());
		}
		if (this.stateSelector != null)
		{
			this.guiControl.setTabRowButtons(this.stateSelector.getStringList(), this.stateSelector.getCurrentIndex(this.stateId));
		}
	}

	// Token: 0x040002D0 RID: 720
	protected SkaldObjectList list;

	// Token: 0x040002D1 RID: 721
	private int leftScrollIndex = -1;
}
