using System;

// Token: 0x0200013C RID: 316
public class UIAttributeSheet : UIBaseCharacterSheet
{
	// Token: 0x0600124A RID: 4682 RVA: 0x000512B4 File Offset: 0x0004F4B4
	protected override void addEntries()
	{
		this.entry1 = new UIBaseCharacterSheet.SheetEntry();
		this.leftColumn.add(this.entry1);
		this.entry2 = new UIBaseCharacterSheet.SheetEntry();
		this.leftColumn.add(this.entry2);
		this.entry3 = new UIBaseCharacterSheet.SheetEntry(95);
		this.rightColumn.add(this.entry3);
		this.entry4 = new UIBaseCharacterSheet.SheetEntry(95);
		this.rightColumn.add(this.entry4);
		this.entry5 = new UIBaseCharacterSheet.SheetEntry(95);
		this.rightColumn.add(this.entry5);
	}
}
