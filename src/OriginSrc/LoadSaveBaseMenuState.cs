using System;
using System.Collections.Generic;

// Token: 0x02000089 RID: 137
public abstract class LoadSaveBaseMenuState : NestedMenuState
{
	// Token: 0x06000A34 RID: 2612 RVA: 0x00030F7A File Offset: 0x0002F17A
	protected LoadSaveBaseMenuState(DataControl dataControl) : base(dataControl)
	{
		this.createGUI();
		this.guiControl.setNumericButtonsAsABXY();
		base.disableCharacterSwap();
		this.setGUIData();
		this.setList();
	}

	// Token: 0x06000A35 RID: 2613 RVA: 0x00030FB9 File Offset: 0x0002F1B9
	public void setList()
	{
		this.list = SkaldSaveControl.getSavePathContent();
		this.currentObject = this.list.getCurrentObject();
		if (this.currentObject != null)
		{
			this.setMainTextBuffer(this.currentObject.getFullDescriptionAndHeader());
		}
	}

	// Token: 0x06000A36 RID: 2614 RVA: 0x00030FF0 File Offset: 0x0002F1F0
	public SkaldObjectList getList()
	{
		return this.list;
	}

	// Token: 0x06000A37 RID: 2615 RVA: 0x00030FF8 File Offset: 0x0002F1F8
	public override void update()
	{
		base.update();
		if (this.numericInputIndex != -1)
		{
			if (this.numericInputIndex == 1)
			{
				base.exit();
				return;
			}
			if (this.numericInputIndex == 2)
			{
				if (this.currentObject != null)
				{
					SkaldSaveControl.copySave(this.currentObject.getId());
					this.setList();
					return;
				}
			}
			else if (this.numericInputIndex == 3)
			{
				if (this.currentObject != null)
				{
					PopUpControl.addPopUpSaveRename(this);
					return;
				}
			}
			else if (this.numericInputIndex == 4 && this.currentObject != null)
			{
				PopUpControl.addPopUpSaveDelete(this);
			}
		}
	}

	// Token: 0x06000A38 RID: 2616 RVA: 0x0003107B File Offset: 0x0002F27B
	protected override void createGUI()
	{
		this.guiControl = new GUIControlSheet();
	}

	// Token: 0x06000A39 RID: 2617 RVA: 0x00031088 File Offset: 0x0002F288
	public void overwrite()
	{
		this.dataControl.gameSave(this.currentObject.getId());
		base.exit();
	}

	// Token: 0x06000A3A RID: 2618 RVA: 0x000310A6 File Offset: 0x0002F2A6
	public void delete()
	{
		SkaldSaveControl.deleteSave(this.currentObject.getId());
		this.setList();
	}

	// Token: 0x06000A3B RID: 2619 RVA: 0x000310C0 File Offset: 0x0002F2C0
	protected List<string> buildOptionList()
	{
		List<string> list = new List<string>();
		if (this.list == null)
		{
			goto IL_62;
		}
		int num = 0;
		using (List<string>.Enumerator enumerator = this.list.getScrolledStringList().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				string item = enumerator.Current;
				if (num == 20)
				{
					break;
				}
				list.Add(item);
				num++;
			}
			goto IL_62;
		}
		IL_56:
		list.Add(this.emptyTag);
		IL_62:
		if (list.Count >= this.slots)
		{
			return list;
		}
		goto IL_56;
	}

	// Token: 0x06000A3C RID: 2620 RVA: 0x00031150 File Offset: 0x0002F350
	protected SkaldBaseObject getCurrentObj()
	{
		return this.currentObject;
	}

	// Token: 0x06000A3D RID: 2621 RVA: 0x00031158 File Offset: 0x0002F358
	protected void setCurrentObj(SkaldBaseObject newObj)
	{
		this.currentObject = newObj;
		if (this.currentObject != null)
		{
			this.setMainTextBuffer(this.currentObject.getFullDescriptionAndHeader());
		}
	}

	// Token: 0x06000A3E RID: 2622 RVA: 0x0003117A File Offset: 0x0002F37A
	protected virtual void addNumericButtons()
	{
		this.guiControl.setNumericButtons(new List<string>
		{
			"Back"
		});
	}

	// Token: 0x06000A3F RID: 2623 RVA: 0x00031197 File Offset: 0x0002F397
	protected override void setGUIData()
	{
		base.setGUIData();
		this.guiControl.setSheetDescription(this.getMainTextBuffer());
		this.addNumericButtons();
		this.guiControl.setListButtons(this.buildOptionList());
		this.guiControl.revealAll();
	}

	// Token: 0x040002AB RID: 683
	protected string emptyTag = "--empty--";

	// Token: 0x040002AC RID: 684
	protected int slots = 20;

	// Token: 0x040002AD RID: 685
	private SkaldBaseObject currentObject;

	// Token: 0x040002AE RID: 686
	protected SkaldObjectList list;
}
