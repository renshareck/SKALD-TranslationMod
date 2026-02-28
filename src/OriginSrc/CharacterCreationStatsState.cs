using System;
using System.Collections.Generic;
using SkaldEnums;

// Token: 0x02000072 RID: 114
public class CharacterCreationStatsState : CharacterBuilderBaseState
{
	// Token: 0x0600097D RID: 2429 RVA: 0x0002D744 File Offset: 0x0002B944
	public CharacterCreationStatsState(DataControl dataControl) : base(dataControl)
	{
		this.resetCharacter();
		this.createGUI();
		this.guiControl.setNumericButtonsAsABXY();
		this.stateId = SkaldStates.StatsEditor;
		this.setGUIData();
		base.addIntroPopUp("Distribute points to your PRIMARY ATTRIBUTES and SKILLS. These go a long way towards defining your character mechanically.");
	}

	// Token: 0x0600097E RID: 2430 RVA: 0x0002D7A0 File Offset: 0x0002B9A0
	protected override void createGUI()
	{
		this.sheet = new UIAttributeEditorSheet();
		string backgroundPath = "SheetAttributeEditorBackground";
		this.guiControl = new GUIControlCharacterSheet(this.sheet, backgroundPath);
	}

	// Token: 0x0600097F RID: 2431 RVA: 0x0002D7D0 File Offset: 0x0002B9D0
	public override void update()
	{
		base.update();
		this.updateAttributePointButtons();
		this.updateSkillPointButtons();
		if (this.numericInputIndex == 0)
		{
			if (this.isDone())
			{
				base.setNextState(1);
			}
			else
			{
				PopUpControl.addPopUpOK("You must destribute all your attribute points before you can proceed!");
			}
		}
		this.numericInputIndex = -1;
		this.setGUIData();
	}

	// Token: 0x06000980 RID: 2432 RVA: 0x0002D820 File Offset: 0x0002BA20
	private void updateAttributePointButtons()
	{
		SkaldBaseObject attributePlusObject = this.sheet.getAttributePlusObject();
		if (attributePlusObject != null && this.attributePoints > 0 && base.getCharacter().getAttributeRank(attributePlusObject.getId()) < this.maxRanks)
		{
			base.getCharacter().addToAttributeRank(attributePlusObject.getId(), 1);
			this.attributePoints--;
		}
		SkaldBaseObject attributeMinusObject = this.sheet.getAttributeMinusObject();
		if (attributeMinusObject != null && base.getCharacter().getAttributeRank(attributeMinusObject.getId()) > 1)
		{
			base.getCharacter().addToAttributeRank(attributeMinusObject.getId(), -1);
			this.attributePoints++;
		}
	}

	// Token: 0x06000981 RID: 2433 RVA: 0x0002D8C4 File Offset: 0x0002BAC4
	private void updateSkillPointButtons()
	{
		SkaldBaseObject skillPlusObject = this.sheet.getSkillPlusObject();
		if (skillPlusObject != null && this.skillPoints > 0 && base.getCharacter().getAttributeRank(skillPlusObject.getId()) < this.maxRanks)
		{
			base.getCharacter().addToAttributeRank(skillPlusObject.getId(), 1);
			this.skillPoints--;
		}
		SkaldBaseObject skillMinusObject = this.sheet.getSkillMinusObject();
		if (skillMinusObject != null && base.getCharacter().getAttributeRank(skillMinusObject.getId()) > 0)
		{
			base.getCharacter().addToAttributeRank(skillMinusObject.getId(), -1);
			this.skillPoints++;
		}
	}

	// Token: 0x06000982 RID: 2434 RVA: 0x0002D965 File Offset: 0x0002BB65
	private void resetCharacter()
	{
		base.getCharacter().clearAttributes(1, 1, 1, 1, 1);
		base.getCharacter().resetMovementStat();
		this.attributePoints = this.maxAttributePoints;
		this.skillPoints = this.maxSkillPoints;
	}

	// Token: 0x06000983 RID: 2435 RVA: 0x0002D99B File Offset: 0x0002BB9B
	private bool isDone()
	{
		return this.attributePoints == 0 && this.skillPoints == 0;
	}

	// Token: 0x06000984 RID: 2436 RVA: 0x0002D9B0 File Offset: 0x0002BBB0
	private SkaldDataList decorateDataList(SkaldDataList list, int rankLimit)
	{
		foreach (SkaldBaseObject skaldBaseObject in list.getObjectList())
		{
			SkaldDataList.SkaldListDataObject skaldListDataObject = (SkaldDataList.SkaldListDataObject)skaldBaseObject;
			if (!(skaldListDataObject.getId() == "HEADER"))
			{
				if (skaldListDataObject.getId() == base.getCharacter().getCoreAttributeId())
				{
					skaldListDataObject.setName(C64Color.GREEN_LIGHT_TAG + skaldListDataObject.getName() + "</color>");
					skaldListDataObject.setDescription(skaldListDataObject.getDescription() + "\n\n" + C64Color.GREEN_LIGHT_TAG + "-Main Attribute-</color>");
				}
				int attributeRank = base.getCharacter().getAttributeRank(skaldListDataObject.getId());
				if (attributeRank > rankLimit)
				{
					if (attributeRank < this.maxRanks)
					{
						skaldListDataObject.setValue(skaldListDataObject.getValue() + C64Color.GRAY_LIGHT_TAG + "+</color>");
					}
					else
					{
						skaldListDataObject.setValue(skaldListDataObject.getValue() + C64Color.GRAY_LIGHT_TAG + "*</color>");
					}
				}
			}
		}
		return list;
	}

	// Token: 0x06000985 RID: 2437 RVA: 0x0002DACC File Offset: 0x0002BCCC
	protected override void setGUIData()
	{
		base.setGUIData();
		base.setButtons(new List<string>
		{
			"Continue"
		});
		SkaldDataList data = this.decorateDataList(base.getCharacter().getListOfPrimaryAttributes(), 1);
		SkaldDataList data2 = this.decorateDataList(base.getCharacter().getListOfSkills(), 0);
		this.sheet.updateEntry1(data);
		this.sheet.updateEntry2(data2);
		this.sheet.updateEntry3(base.getCharacter().getListOfSecondaryAttributes());
		this.sheet.updateEntry4(base.getCharacter().getListOfCombatStats());
		this.sheet.updateEntry5(base.getCharacter().getListOfDefences());
		this.sheet.setAttributePoints(this.attributePoints);
		this.sheet.setSkillPoints(this.skillPoints);
		this.guiControl.setSheetDescription(this.sheet.getCurrentObjectFullDescriptionAndHeader());
		this.guiControl.setSheetHeader("Primary Attributes and Skills");
		this.guiControl.revealAll();
	}

	// Token: 0x0400026E RID: 622
	private int attributePoints;

	// Token: 0x0400026F RID: 623
	private int maxAttributePoints = 5;

	// Token: 0x04000270 RID: 624
	private int skillPoints;

	// Token: 0x04000271 RID: 625
	private int maxSkillPoints = 6;

	// Token: 0x04000272 RID: 626
	private int maxRanks = 4;

	// Token: 0x04000273 RID: 627
	private UIAttributeEditorSheet sheet;
}
