using System;
using System.Collections.Generic;

// Token: 0x0200013A RID: 314
public class UISpellBookSheet : UIBaseCharacterSheet
{
	// Token: 0x0600123F RID: 4671 RVA: 0x00050E90 File Offset: 0x0004F090
	public UISpellBookSheet(SpellContainer.SpellList sortedSpellList)
	{
		this.updateSortedList(sortedSpellList);
	}

	// Token: 0x06001240 RID: 4672 RVA: 0x00050EA0 File Offset: 0x0004F0A0
	protected override void addEntries()
	{
		if (this.spellList == null)
		{
			return;
		}
		this.leftColumn.clearElements();
		this.rightColumn.clearElements();
		this.entry1 = new UIBaseCharacterSheet.SheetEntry(107);
		this.entry1.stretchVertical = false;
		this.entry1.setHeight(51);
		this.entry1.setTabWidth(78);
		this.leftColumn.add(this.entry1);
		this.entry2 = new UIBaseCharacterSheet.SheetEntry(96);
		this.entry2.setTabWidth(88);
		this.rightColumn.add(this.entry2);
		this.grid = new UISpellbookSelectorGrid(this.spellList.getButtonDataList(), 40);
		this.leftColumn.add(this.grid);
	}

	// Token: 0x06001241 RID: 4673 RVA: 0x00050F64 File Offset: 0x0004F164
	private void updateSortedList(SpellContainer.SpellList spellList)
	{
		this.spellList = spellList;
		this.addEntries();
	}

	// Token: 0x06001242 RID: 4674 RVA: 0x00050F74 File Offset: 0x0004F174
	public void update(Character character)
	{
		if (character != this.currentCharacter)
		{
			this.currentCharacter = character;
			this.updateSortedList(character.getSpellContainer().getSortedSpellList());
		}
		this.updateEntry1(character.getListOfMagicAttributes());
		this.updateEntry2(character.getListOfSpellSchools());
		this.updateTier(this.grid, this.spellList.spells);
	}

	// Token: 0x06001243 RID: 4675 RVA: 0x00050FD1 File Offset: 0x0004F1D1
	private void updateTier(UISpellbookSelectorGrid grid, List<AbilitySpell> list)
	{
		grid.update();
		if (grid.getLeftClickIndex() != -1)
		{
			this.currentObject = list[grid.getLeftClickIndex()];
		}
	}

	// Token: 0x04000466 RID: 1126
	private SpellContainer.SpellList spellList;

	// Token: 0x04000467 RID: 1127
	private UISpellbookSelectorGrid grid;

	// Token: 0x04000468 RID: 1128
	private Character currentCharacter;
}
