using System;
using System.Collections.Generic;

// Token: 0x0200008D RID: 141
public class SaveMenuState : LoadSaveBaseMenuState
{
	// Token: 0x06000A49 RID: 2633 RVA: 0x00031390 File Offset: 0x0002F590
	public SaveMenuState(DataControl dataControl) : base(dataControl)
	{
	}

	// Token: 0x06000A4A RID: 2634 RVA: 0x0003139C File Offset: 0x0002F59C
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
			if (listButtonPressIndex >= this.list.getCount())
			{
				this.save(this.emptyTag);
			}
			else
			{
				SkaldBaseObject objectByPageIndex = this.list.getObjectByPageIndex(listButtonPressIndex);
				if (objectByPageIndex == base.getCurrentObj())
				{
					this.save(objectByPageIndex.getName());
				}
				else
				{
					base.setCurrentObj(objectByPageIndex);
				}
			}
		}
		else if (this.numericInputIndex != -1 && this.numericInputIndex == 0)
		{
			if (base.getCurrentObj() != null)
			{
				this.save(base.getCurrentObj().getId());
			}
			else
			{
				this.save(this.emptyTag);
			}
		}
		this.numericInputIndex = -1;
		this.setGUIData();
	}

	// Token: 0x06000A4B RID: 2635 RVA: 0x00031458 File Offset: 0x0002F658
	private void save(string fileName)
	{
		if (fileName == this.emptyTag)
		{
			int saveNumber = 1;
			if (this.list != null)
			{
				saveNumber = this.list.getCount() + 1;
			}
			PopUpControl.addPopUpCreateSave(this, this.dataControl.printSaveName(saveNumber));
			return;
		}
		PopUpControl.addPopUpSaveOverwrite(this);
	}

	// Token: 0x06000A4C RID: 2636 RVA: 0x000314A4 File Offset: 0x0002F6A4
	public void createSave(string saveName)
	{
		this.dataControl.gameSave(saveName);
		base.exit();
	}

	// Token: 0x06000A4D RID: 2637 RVA: 0x000314B8 File Offset: 0x0002F6B8
	protected override void addNumericButtons()
	{
		if (base.getCurrentObj() != null)
		{
			this.guiControl.setNumericButtons(new List<string>
			{
				"OVERWRITE",
				"Back",
				"Copy",
				"Rename",
				"Delete"
			});
			return;
		}
		this.guiControl.setNumericButtons(new List<string>
		{
			"SAVE",
			"Back",
			"Copy",
			"Rename",
			"Delete"
		});
	}

	// Token: 0x06000A4E RID: 2638 RVA: 0x0003155C File Offset: 0x0002F75C
	protected override void setGUIData()
	{
		base.setGUIData();
		this.guiControl.setSheetHeader("Save Game");
	}
}
