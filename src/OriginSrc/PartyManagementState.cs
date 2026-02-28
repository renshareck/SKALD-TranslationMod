using System;
using System.Collections.Generic;
using SkaldEnums;

// Token: 0x02000092 RID: 146
public class PartyManagementState : StateBase
{
	// Token: 0x06000A6A RID: 2666 RVA: 0x0003216F File Offset: 0x0003036F
	public PartyManagementState(DataControl dataControl) : base(dataControl)
	{
		this.stateId = SkaldStates.PartyManagement;
		dataControl.playMusic("Campfire");
		base.disableCharacterSwap();
		base.disableClickablePortraits();
		this.setGUIData();
	}

	// Token: 0x06000A6B RID: 2667 RVA: 0x0003219E File Offset: 0x0003039E
	protected override void createGUI()
	{
		this.partyManagementUI = new UIPartyManagement();
		this.guiControl = new GUIControlPartyManagement(this.partyManagementUI);
	}

	// Token: 0x06000A6C RID: 2668 RVA: 0x000321BC File Offset: 0x000303BC
	public override void update()
	{
		base.update();
		if (this.numericInputIndex == 0 || SkaldIO.getPressedEscapeKey())
		{
			this.setTargetState(SkaldStates.CampActivities);
		}
		Character character = this.partyManagementUI.updatePartyList(this.dataControl.getParty().getObjectList());
		if (character != null)
		{
			this.dataControl.sendCharacterToBench(character.getId());
		}
		Character character2 = this.partyManagementUI.updateSideBenchBlock(this.dataControl.getSideBench().getObjectList());
		if (character2 != null)
		{
			this.dataControl.getCharacterFromBench(character2.getId());
		}
		this.setGUIData();
	}

	// Token: 0x06000A6D RID: 2669 RVA: 0x0003224C File Offset: 0x0003044C
	protected override void setGUIData()
	{
		base.setGUIData();
		this.guiControl.setNumericButtons(new List<string>
		{
			"Exit"
		});
		this.guiControl.setSheetHeader("Party Management");
		this.guiControl.setSecondaryDescription("Click a party member to move them between your camp and your active party.");
		this.drawPortraits();
		this.guiControl.revealAll();
	}

	// Token: 0x040002C6 RID: 710
	private UIPartyManagement partyManagementUI;
}
