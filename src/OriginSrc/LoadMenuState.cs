using System;
using System.Collections.Generic;
using SkaldEnums;

// Token: 0x02000087 RID: 135
public class LoadMenuState : LoadSaveBaseMenuState
{
	// Token: 0x06000A26 RID: 2598 RVA: 0x00030BAC File Offset: 0x0002EDAC
	public LoadMenuState(DataControl dataControl) : base(dataControl)
	{
	}

	// Token: 0x06000A27 RID: 2599 RVA: 0x00030BB5 File Offset: 0x0002EDB5
	public LoadMenuState(DataControl dataControl, SkaldStates _exitTarget) : base(dataControl)
	{
	}

	// Token: 0x06000A28 RID: 2600 RVA: 0x00030BC0 File Offset: 0x0002EDC0
	public override void update()
	{
		base.update();
		int listButtonPressIndex = this.guiControl.getListButtonPressIndex();
		if (listButtonPressIndex != -1)
		{
			if (listButtonPressIndex >= this.slots)
			{
				return;
			}
			if (listButtonPressIndex < this.list.getCount())
			{
				SkaldBaseObject objectByPageIndex = this.list.getObjectByPageIndex(listButtonPressIndex);
				if (objectByPageIndex != null && objectByPageIndex == base.getCurrentObj())
				{
					this.load();
				}
				else
				{
					base.setCurrentObj(objectByPageIndex);
				}
			}
			else
			{
				this.setMainTextBuffer("An empty save-slot.");
			}
		}
		else if (this.numericInputIndex != -1 && this.numericInputIndex == 0)
		{
			this.load();
		}
		this.numericInputIndex = -1;
		this.setGUIData();
	}

	// Token: 0x06000A29 RID: 2601 RVA: 0x00030C56 File Offset: 0x0002EE56
	private void load()
	{
		if (this.dataControl.gameLoad(base.getCurrentObj().getId()))
		{
			this.setTargetState(SkaldStates.Overland);
		}
	}

	// Token: 0x06000A2A RID: 2602 RVA: 0x00030C78 File Offset: 0x0002EE78
	protected override void addNumericButtons()
	{
		this.guiControl.setNumericButtons(new List<string>
		{
			"LOAD",
			"Back",
			"Copy",
			"Rename",
			"Delete"
		});
	}

	// Token: 0x06000A2B RID: 2603 RVA: 0x00030CCC File Offset: 0x0002EECC
	protected override void setGUIData()
	{
		base.setGUIData();
		this.guiControl.setSheetHeader("Load Game");
	}
}
