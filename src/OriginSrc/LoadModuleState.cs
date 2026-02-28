using System;
using System.Collections.Generic;
using SkaldEnums;

// Token: 0x02000088 RID: 136
public class LoadModuleState : NestedMenuState
{
	// Token: 0x06000A2C RID: 2604 RVA: 0x00030CE4 File Offset: 0x0002EEE4
	public LoadModuleState(DataControl dataControl) : base(dataControl)
	{
		this.createGUI();
		this.guiControl.setNumericButtonsAsABXY();
		base.disableCharacterSwap();
		this.setGUIData();
		this.setList();
	}

	// Token: 0x06000A2D RID: 2605 RVA: 0x00030D23 File Offset: 0x0002EF23
	public void setList()
	{
		this.list = SkaldModControl.getModFolderContent();
		this.setCurrentObj(this.list.getCurrentObject());
	}

	// Token: 0x06000A2E RID: 2606 RVA: 0x00030D41 File Offset: 0x0002EF41
	protected void setCurrentObj(SkaldBaseObject newObj)
	{
		this.currentObject = newObj;
		if (this.currentObject != null)
		{
			this.setMainTextBuffer(this.currentObject.getFullDescriptionAndHeader());
		}
	}

	// Token: 0x06000A2F RID: 2607 RVA: 0x00030D64 File Offset: 0x0002EF64
	public override void update()
	{
		base.update();
		this.setGUIData();
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
				this.setCurrentObj(objectByPageIndex);
			}
			else
			{
				this.setMainTextBuffer("An empty save-slot.");
			}
		}
		else if (this.numericInputIndex != -1)
		{
			if (this.numericInputIndex == 0)
			{
				SkaldBaseObject skaldBaseObject = this.list.getCurrentObject();
				if (skaldBaseObject != null && this.dataControl.mountModule(skaldBaseObject.getId()))
				{
					if (GameData.shouldStartWithCharacterCreation())
					{
						this.dataControl.editCurrentCharacterAsMain();
						this.setTargetState(SkaldStates.DifficultySelector);
					}
					else
					{
						this.setTargetState(SkaldStates.Overland);
					}
				}
			}
			else if (this.numericInputIndex == 1)
			{
				this.dataControl.openModIO();
			}
			else if (this.numericInputIndex == 2)
			{
				base.exit();
			}
		}
		this.numericInputIndex = 0;
	}

	// Token: 0x06000A30 RID: 2608 RVA: 0x00030E4F File Offset: 0x0002F04F
	protected override void createGUI()
	{
		this.guiControl = new GUIControlSheet();
	}

	// Token: 0x06000A31 RID: 2609 RVA: 0x00030E5C File Offset: 0x0002F05C
	protected List<string> buildOptionList()
	{
		List<string> list = new List<string>();
		if (this.list == null)
		{
			goto IL_66;
		}
		int num = 0;
		using (List<string>.Enumerator enumerator = this.list.getScrolledStringList().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				string item = enumerator.Current;
				if (num == this.slots)
				{
					break;
				}
				list.Add(item);
				num++;
			}
			goto IL_66;
		}
		IL_5A:
		list.Add(this.emptyTag);
		IL_66:
		if (list.Count >= this.slots)
		{
			return list;
		}
		goto IL_5A;
	}

	// Token: 0x06000A32 RID: 2610 RVA: 0x00030EF0 File Offset: 0x0002F0F0
	protected virtual void addNumericButtons()
	{
		this.guiControl.setNumericButtons(new List<string>
		{
			"Launch",
			"Get More Content",
			"Back"
		});
	}

	// Token: 0x06000A33 RID: 2611 RVA: 0x00030F24 File Offset: 0x0002F124
	protected override void setGUIData()
	{
		base.setGUIData();
		this.guiControl.setSheetDescription(this.getMainTextBuffer());
		this.addNumericButtons();
		this.guiControl.setListButtons(this.buildOptionList());
		this.guiControl.revealAll();
		this.guiControl.setSheetHeader("Load Module");
	}

	// Token: 0x040002A7 RID: 679
	protected string emptyTag = "--empty--";

	// Token: 0x040002A8 RID: 680
	protected int slots = 20;

	// Token: 0x040002A9 RID: 681
	private SkaldBaseObject currentObject;

	// Token: 0x040002AA RID: 682
	protected SkaldObjectList list;
}
