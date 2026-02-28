using System;
using System.Collections.Generic;
using SkaldEnums;

// Token: 0x02000073 RID: 115
public class DifficultySelectorState : CharacterBuilderBaseState
{
	// Token: 0x06000986 RID: 2438 RVA: 0x0002DBC8 File Offset: 0x0002BDC8
	public DifficultySelectorState(DataControl dataControl) : base(dataControl)
	{
		this.stateId = SkaldStates.DifficultySelector;
		GlobalSettings.getDifficultySettings().setDifficultyLevel(2);
		this.setData();
		this.setGUIData();
		base.addIntroPopUp("Welcome to character creation! Start by picking a DIFFICULTY for your playthrough. Difficulty can be changed during play.");
	}

	// Token: 0x06000987 RID: 2439 RVA: 0x0002DBFB File Offset: 0x0002BDFB
	protected override string getSheetName()
	{
		return "Pick a Difficulty";
	}

	// Token: 0x06000988 RID: 2440 RVA: 0x0002DC04 File Offset: 0x0002BE04
	private void setData()
	{
		this.setMainTextBuffer(GlobalSettings.getDifficultySettings().getCurrentDifficultyDescription());
		SkaldObjectList skaldObjectList = new SkaldObjectList();
		skaldObjectList.deactivateSorting();
		foreach (SkaldBaseObject skaldBaseObject in GlobalSettings.getDifficultySettings().getObjectList())
		{
			skaldObjectList.add(new SkaldBaseObject(skaldBaseObject.getId(), skaldBaseObject.getListName(), skaldBaseObject.getDescription()));
		}
		skaldObjectList.setFirstObjectAsCurrentObject();
		skaldObjectList.removeCurrentObject();
		this.list = skaldObjectList;
	}

	// Token: 0x06000989 RID: 2441 RVA: 0x0002DCA4 File Offset: 0x0002BEA4
	protected override void createGUI()
	{
		this.guiControl = new GUIControlExtraRowList();
	}

	// Token: 0x0600098A RID: 2442 RVA: 0x0002DCB4 File Offset: 0x0002BEB4
	public override void update()
	{
		base.update();
		int horizontalMenuButtonsIndex = (this.guiControl as GUIControlExtraRowList).getHorizontalMenuButtonsIndex();
		if (horizontalMenuButtonsIndex != -1)
		{
			GlobalSettings.getDifficultySettings().setDifficultyLevel(horizontalMenuButtonsIndex + 1);
			this.setData();
		}
		if (this.numericInputIndex == 0)
		{
			this.dataControl.settingsSave();
			base.setNextState(1);
		}
		this.numericInputIndex = -1;
		this.setGUIData();
	}

	// Token: 0x0600098B RID: 2443 RVA: 0x0002DD18 File Offset: 0x0002BF18
	public List<string> getDifficultySettingsNameList()
	{
		List<string> difficultyDataList = GameData.getDifficultyDataList();
		List<string> list = new List<string>();
		foreach (string id in difficultyDataList)
		{
			SKALDProjectData.Objects.DifficultyContainer.DifficultyData difficultyData = GameData.getDifficultyData(id);
			if (difficultyData != null)
			{
				list.Add(difficultyData.title);
			}
			else
			{
				list.Add("Unnamed");
			}
		}
		return list;
	}

	// Token: 0x0600098C RID: 2444 RVA: 0x0002DD8C File Offset: 0x0002BF8C
	protected override void setGUIData()
	{
		base.setGUIData();
		GUIControlExtraRowList guicontrolExtraRowList = this.guiControl as GUIControlExtraRowList;
		int num = GlobalSettings.getDifficultySettings().getDifficultyLevel();
		if (num > 0)
		{
			num--;
		}
		guicontrolExtraRowList.setHorizontalMenuButtons(this.getDifficultySettingsNameList(), num);
		base.setButtons(new List<string>
		{
			"Select"
		});
		this.guiControl.setSheetDescription(this.getMainTextBuffer());
		this.guiControl.setSecondaryDescription("");
		if (this.list != null)
		{
			List<string> scrolledStringList = this.list.getScrolledStringList();
			this.guiControl.setListButtons(scrolledStringList);
		}
		this.guiControl.revealAll();
	}
}
