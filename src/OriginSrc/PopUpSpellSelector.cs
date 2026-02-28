using System;
using System.Collections.Generic;

// Token: 0x02000059 RID: 89
public class PopUpSpellSelector : PopUpBase
{
	// Token: 0x06000885 RID: 2181 RVA: 0x00029924 File Offset: 0x00027B24
	public PopUpSpellSelector(string school, int tier, int number, Character character)
	{
		this.school = school;
		this.tierOfSpellsToSelect = tier;
		this.numberOfSpellsToSelect = number;
		this.character = character;
	}

	// Token: 0x06000886 RID: 2182 RVA: 0x00029960 File Offset: 0x00027B60
	private void initialize()
	{
		int count = this.getLegalSpells().Count;
		if (this.numberOfSpellsToSelect > count)
		{
			this.numberOfSpellsToSelect = count;
		}
		this.setPopUpUI("", new List<string>
		{
			"Confirm"
		});
		this.updateText();
	}

	// Token: 0x06000887 RID: 2183 RVA: 0x000299AA File Offset: 0x00027BAA
	public override bool allowTooltips()
	{
		return true;
	}

	// Token: 0x06000888 RID: 2184 RVA: 0x000299B0 File Offset: 0x00027BB0
	private void updateText()
	{
		int num = this.numberOfSpellsToSelect - this.spellsSelected.Count;
		if (this.numberOfSpellsToSelect == 0)
		{
			base.setMainTextContent(C64Color.GREEN_LIGHT_TAG + "There are no more spells left to select for this tier.</color>");
			return;
		}
		if (num == 0)
		{
			base.setMainTextContent(C64Color.GREEN_LIGHT_TAG + "You have selected the required spells and can proceed!</color>");
			return;
		}
		if (num == 1)
		{
			base.setMainTextContent(string.Concat(new string[]
			{
				"Pick 1 new ",
				GameData.getAttributeName(this.school),
				" ",
				this.tierOfSpellsToSelect.ToString(),
				" spell to learn."
			}));
			return;
		}
		base.setMainTextContent(string.Concat(new string[]
		{
			"Pick ",
			num.ToString(),
			" new ",
			GameData.getAttributeName(this.school),
			" ",
			this.tierOfSpellsToSelect.ToString(),
			" spells to learn."
		}));
	}

	// Token: 0x06000889 RID: 2185 RVA: 0x00029AA8 File Offset: 0x00027CA8
	private void addSpellIdToSelectedList(string spellId)
	{
		AbilitySpell spell = GameData.getSpell(spellId);
		if (spell.getTier() > this.tierOfSpellsToSelect)
		{
			PopUpControl.addPopUpOK("This spell has too high Tier (" + spell.getTier().ToString() + ") for you to learn it yet.");
			return;
		}
		if (this.spellsSelected.Contains(spellId))
		{
			this.spellsSelected.Remove(spellId);
			return;
		}
		if (this.spellsSelected.Count >= this.numberOfSpellsToSelect)
		{
			return;
		}
		this.spellsSelected.Add(spellId);
	}

	// Token: 0x0600088A RID: 2186 RVA: 0x00029B2C File Offset: 0x00027D2C
	public override void handle()
	{
		if (this.uiElements == null)
		{
			this.initialize();
		}
		if (base.isHandled())
		{
			return;
		}
		this.updateControllerScrolling();
		PopUpBase.PopUpUISpellSelector popUpUISpellSelector = this.uiElements as PopUpBase.PopUpUISpellSelector;
		List<AbilitySpell> legalSpells = this.getLegalSpells();
		popUpUISpellSelector.update(this.createButtonDataList(legalSpells));
		int gridHoverIndex = popUpUISpellSelector.getGridHoverIndex();
		int gridButtonPressIndexRight = popUpUISpellSelector.getGridButtonPressIndexRight();
		int gridButtonPressIndexLeft = popUpUISpellSelector.getGridButtonPressIndexLeft();
		if (gridHoverIndex != -1 && gridHoverIndex < legalSpells.Count)
		{
			base.setTertiaryTextContent(legalSpells[gridHoverIndex].getName());
		}
		if (gridButtonPressIndexRight != -1 && gridButtonPressIndexRight < legalSpells.Count)
		{
			ToolTipPrinter.setToolTipWithRules(legalSpells[gridButtonPressIndexRight].getFullDescriptionAndHeader());
		}
		if (gridButtonPressIndexLeft != -1 && gridButtonPressIndexLeft < legalSpells.Count)
		{
			this.addSpellIdToSelectedList(legalSpells[gridButtonPressIndexLeft].getId());
		}
		this.updateText();
		if (SkaldIO.getPressedMainInteractKey() || SkaldIO.getPressedNumericButton1() || (base.getButtonPressIndex() == 0 && !ToolTipPrinter.isMouseOverTooltip()))
		{
			if (this.spellsSelected.Count == this.numberOfSpellsToSelect)
			{
				this.character.addSpell(this.spellsSelected);
				this.handle(true);
				return;
			}
			if (this.numberOfSpellsToSelect == 1)
			{
				PopUpControl.addPopUpOK("You must select 1 spell before proceeding!\n\nLeft-click to select spells\nRight-click to examine them.");
				return;
			}
			PopUpControl.addPopUpOK("You must select " + this.numberOfSpellsToSelect.ToString() + " spells before proceeding!\n\nLeft-click to select spells\nRight-click to examine them.");
		}
	}

	// Token: 0x0600088B RID: 2187 RVA: 0x00029C68 File Offset: 0x00027E68
	private List<AbilitySpell> getLegalSpells()
	{
		List<AbilitySpell> list = new List<AbilitySpell>();
		List<AbilitySpell>[] array = new List<AbilitySpell>[4];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = new List<AbilitySpell>();
		}
		foreach (AbilitySpell abilitySpell in GameData.getAllSpells())
		{
			if (abilitySpell.getSchoolList().Contains(this.school) && !this.character.getSpellContainer().hasComponent(abilitySpell.getId()))
			{
				int num = abilitySpell.getTier() - 1;
				if (num < array.Length)
				{
					array[num].Add(abilitySpell);
				}
			}
		}
		List<AbilitySpell>[] array2 = array;
		for (int j = 0; j < array2.Length; j++)
		{
			foreach (AbilitySpell abilitySpell2 in array2[j])
			{
				if (abilitySpell2.getSchoolList().Contains(this.school) && !this.character.getSpellContainer().hasComponent(abilitySpell2.getId()))
				{
					list.Add(abilitySpell2);
				}
			}
		}
		return list;
	}

	// Token: 0x0600088C RID: 2188 RVA: 0x00029DA8 File Offset: 0x00027FA8
	private List<UIButtonControlBase.ButtonData> createButtonDataList(List<AbilitySpell> legalSpells)
	{
		List<UIButtonControlBase.ButtonData> list = new List<UIButtonControlBase.ButtonData>();
		foreach (AbilitySpell abilitySpell in legalSpells)
		{
			UIButtonControlBase.ButtonData buttonData = abilitySpell.getButtonData(this.character);
			if (abilitySpell.getTier() > this.tierOfSpellsToSelect)
			{
				buttonData.texture = PopUpSpellSelector.lockedIcon;
			}
			else if (this.spellsSelected.Contains(abilitySpell.getId()))
			{
				buttonData.texture = buttonData.texture.createCopy();
				TextureTools.applyOverlay(buttonData.texture, PopUpSpellSelector.selectedIcon);
			}
			list.Add(buttonData);
		}
		return list;
	}

	// Token: 0x0600088D RID: 2189 RVA: 0x00029E5C File Offset: 0x0002805C
	protected override void setPopUpUI(string description, List<string> buttonList)
	{
		this.uiElements = new PopUpBase.PopUpUISpellSelector(description, buttonList, this.createButtonDataList(this.getLegalSpells()));
	}

	// Token: 0x040001E6 RID: 486
	private string school = "";

	// Token: 0x040001E7 RID: 487
	private int tierOfSpellsToSelect;

	// Token: 0x040001E8 RID: 488
	private int numberOfSpellsToSelect;

	// Token: 0x040001E9 RID: 489
	private Character character;

	// Token: 0x040001EA RID: 490
	private List<string> spellsSelected = new List<string>();

	// Token: 0x040001EB RID: 491
	private static TextureTools.TextureData selectedIcon = TextureTools.loadTextureData("Images/GUIIcons/CombatMenuButtons/SelectedSpell");

	// Token: 0x040001EC RID: 492
	private static TextureTools.TextureData lockedIcon = TextureTools.loadTextureData("Images/GUIIcons/AbilityIcons/AbilityLocked");
}
