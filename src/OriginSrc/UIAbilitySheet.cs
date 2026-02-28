using System;
using System.Collections.Generic;

// Token: 0x0200013B RID: 315
public class UIAbilitySheet : UIBaseCharacterSheet
{
	// Token: 0x06001244 RID: 4676 RVA: 0x00050FF4 File Offset: 0x0004F1F4
	public UIAbilitySheet(Character character)
	{
		this.update(character);
	}

	// Token: 0x06001245 RID: 4677 RVA: 0x00051004 File Offset: 0x0004F204
	protected override void addEntries()
	{
		if (this.currentCharacter == null)
		{
			return;
		}
		this.leftColumn.clearElements();
		this.rightColumn.clearElements();
		this.setGrid("Maneuver Abilities", ref this.gridManeuvers, this.maneuverList);
		this.setGrid("Triggered Abilities", ref this.gridTriggered, this.triggeredAbilityList);
		this.setGrid("Passive Bonus Abilities", ref this.gridPassive, this.passiveAbilityList);
	}

	// Token: 0x06001246 RID: 4678 RVA: 0x00051078 File Offset: 0x0004F278
	private void setGrid(string name, ref UIAbilitySheetGrid grid, List<Ability> list)
	{
		UIAbilitySheet.Textblock element = new UIAbilitySheet.Textblock(name);
		this.leftColumn.add(element);
		List<UIButtonControlBase.ButtonData> list2 = new List<UIButtonControlBase.ButtonData>();
		foreach (Ability ability in list)
		{
			list2.Add(ability.getButtonData(null));
		}
		grid = new UIAbilitySheetGrid(list2, 42);
		this.leftColumn.add(grid);
	}

	// Token: 0x06001247 RID: 4679 RVA: 0x000510FC File Offset: 0x0004F2FC
	public void update(Character character)
	{
		if (character != this.currentCharacter)
		{
			this.currentCharacter = character;
			this.passiveAbilityList = new List<Ability>();
			foreach (BaseCharacterComponent baseCharacterComponent in this.currentCharacter.getAbilityPassiveContainer().getComponentList())
			{
				Ability item = (Ability)baseCharacterComponent;
				this.passiveAbilityList.Add(item);
			}
			this.maneuverList = new List<Ability>();
			foreach (BaseCharacterComponent baseCharacterComponent2 in this.currentCharacter.getAbilityManueverContainer().getComponentList())
			{
				Ability item2 = (Ability)baseCharacterComponent2;
				this.maneuverList.Add(item2);
			}
			this.triggeredAbilityList = new List<Ability>();
			foreach (BaseCharacterComponent baseCharacterComponent3 in this.currentCharacter.getAbilityTriggeredContainer().getComponentList())
			{
				Ability item3 = (Ability)baseCharacterComponent3;
				this.triggeredAbilityList.Add(item3);
			}
			this.addEntries();
		}
		this.updateGrid();
	}

	// Token: 0x06001248 RID: 4680 RVA: 0x00051250 File Offset: 0x0004F450
	private void updateGrid()
	{
		this.updateGrid(this.gridPassive, this.passiveAbilityList);
		this.updateGrid(this.gridManeuvers, this.maneuverList);
		this.updateGrid(this.gridTriggered, this.triggeredAbilityList);
	}

	// Token: 0x06001249 RID: 4681 RVA: 0x00051288 File Offset: 0x0004F488
	private void updateGrid(UIAbilitySheetGrid grid, List<Ability> list)
	{
		if (grid == null || list == null)
		{
			return;
		}
		grid.update();
		if (grid.getLeftClickIndex() != -1)
		{
			this.currentObject = list[grid.getLeftClickIndex()];
		}
	}

	// Token: 0x04000469 RID: 1129
	private UIAbilitySheetGrid gridPassive;

	// Token: 0x0400046A RID: 1130
	private UIAbilitySheetGrid gridManeuvers;

	// Token: 0x0400046B RID: 1131
	private UIAbilitySheetGrid gridTriggered;

	// Token: 0x0400046C RID: 1132
	private Character currentCharacter;

	// Token: 0x0400046D RID: 1133
	private List<Ability> passiveAbilityList;

	// Token: 0x0400046E RID: 1134
	private List<Ability> maneuverList;

	// Token: 0x0400046F RID: 1135
	private List<Ability> triggeredAbilityList;

	// Token: 0x02000284 RID: 644
	private class Textblock : UITextBlock
	{
		// Token: 0x06001A93 RID: 6803 RVA: 0x00073308 File Offset: 0x00071508
		public Textblock(string content) : base(0, 0, 0, 8, C64Color.HeaderColor)
		{
			this.stretchHorizontal = true;
			base.setContent(content);
			this.padding.top = 2;
			this.padding.bottom = 2;
			base.setLetterShadowColor(C64Color.SmallTextShadowColorDarkBackground);
			this.padding.left = 5;
		}
	}
}
