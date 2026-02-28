using System;
using System.Collections.Generic;

// Token: 0x0200013D RID: 317
public class UIAttributeEditorSheet : UIBaseCharacterSheet
{
	// Token: 0x0600124C RID: 4684 RVA: 0x0005135C File Offset: 0x0004F55C
	protected override void addEntries()
	{
		this.entry1 = new UIBaseCharacterSheet.EditorSheetEntry();
		this.leftColumn.add(this.entry1);
		this.entry2 = new UIBaseCharacterSheet.EditorSheetEntry();
		this.leftColumn.add(this.entry2);
		this.entry3 = new UIBaseCharacterSheet.SheetEntry(95);
		this.rightColumn.add(this.entry3);
		this.entry4 = new UIBaseCharacterSheet.SheetEntry(95);
		this.rightColumn.add(this.entry4);
		this.entry5 = new UIBaseCharacterSheet.SheetEntry(95);
		this.rightColumn.add(this.entry5);
	}

	// Token: 0x0600124D RID: 4685 RVA: 0x000513FB File Offset: 0x0004F5FB
	public override void controllerScrollSidewaysLeft()
	{
		this.entry1.controllerScrollSidewaysLeft();
		this.entry2.controllerScrollSidewaysLeft();
	}

	// Token: 0x0600124E RID: 4686 RVA: 0x00051413 File Offset: 0x0004F613
	public override void controllerScrollSidewaysRight()
	{
		this.entry1.controllerScrollSidewaysRight();
		this.entry2.controllerScrollSidewaysRight();
	}

	// Token: 0x0600124F RID: 4687 RVA: 0x0005142C File Offset: 0x0004F62C
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
		return list;
	}

	// Token: 0x06001250 RID: 4688 RVA: 0x000514E4 File Offset: 0x0004F6E4
	public override void updateEntry1(SkaldDataList data)
	{
		base.updateEntry1(data);
		this.updateCurrentObject(data);
	}

	// Token: 0x06001251 RID: 4689 RVA: 0x000514F4 File Offset: 0x0004F6F4
	public override void updateEntry2(SkaldDataList data)
	{
		base.updateEntry2(data);
		this.updateCurrentObject(data);
	}

	// Token: 0x06001252 RID: 4690 RVA: 0x00051504 File Offset: 0x0004F704
	private void updateCurrentObject(SkaldDataList data)
	{
		if (this.currentObject == null)
		{
			return;
		}
		SkaldBaseObject @object = data.getObject(this.currentObject.getId());
		if (@object != null)
		{
			this.currentObject = @object;
		}
	}

	// Token: 0x06001253 RID: 4691 RVA: 0x00051536 File Offset: 0x0004F736
	public SkaldBaseObject getAttributePlusObject()
	{
		return (this.entry1 as UIBaseCharacterSheet.EditorSheetEntry).getPlusObject();
	}

	// Token: 0x06001254 RID: 4692 RVA: 0x00051548 File Offset: 0x0004F748
	public SkaldBaseObject getAttributeMinusObject()
	{
		return (this.entry1 as UIBaseCharacterSheet.EditorSheetEntry).getMinusObject();
	}

	// Token: 0x06001255 RID: 4693 RVA: 0x0005155A File Offset: 0x0004F75A
	public SkaldBaseObject getSkillPlusObject()
	{
		return (this.entry2 as UIBaseCharacterSheet.EditorSheetEntry).getPlusObject();
	}

	// Token: 0x06001256 RID: 4694 RVA: 0x0005156C File Offset: 0x0004F76C
	public SkaldBaseObject getSkillMinusObject()
	{
		return (this.entry2 as UIBaseCharacterSheet.EditorSheetEntry).getMinusObject();
	}

	// Token: 0x06001257 RID: 4695 RVA: 0x0005157E File Offset: 0x0004F77E
	public void setAttributePoints(int value)
	{
		(this.entry1 as UIBaseCharacterSheet.EditorSheetEntry).setPointValue(value);
	}

	// Token: 0x06001258 RID: 4696 RVA: 0x00051591 File Offset: 0x0004F791
	public void setSkillPoints(int value)
	{
		(this.entry2 as UIBaseCharacterSheet.EditorSheetEntry).setPointValue(value);
	}

	// Token: 0x04000470 RID: 1136
	protected UICanvasVertical rightButtonColumn;

	// Token: 0x04000471 RID: 1137
	protected UICanvasVertical leftButtonColumn;
}
