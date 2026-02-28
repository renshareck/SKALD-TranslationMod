using System;
using System.Collections.Generic;
using SkaldEnums;

// Token: 0x0200006C RID: 108
public class CampActivityState : InventoryBaseState
{
	// Token: 0x0600094B RID: 2379 RVA: 0x0002C404 File Offset: 0x0002A604
	public CampActivityState(DataControl dataControl) : base(dataControl)
	{
		this.stateId = SkaldStates.CampActivities;
		this.allowQuickButtons = false;
		dataControl.playMusic("Campfire");
		base.disableClickablePortraits();
		if (!dataControl.getTechnicalDataRecord().hasRested())
		{
			dataControl.getTechnicalDataRecord().setHasRested();
			PopUpControl.addTutorialPopUp("TUT_Resting1");
		}
		this.header = this.getCampingOrder().getDescription();
		if (GlobalSettings.getDifficultySettings().ignoreFood())
		{
			this.setMainTextBuffer("Select activities for your party-members to perform whilst resting.\n\nFood is disabled for this difficuly level.\n\nClick \"Begin Rest\" when you are ready. Resting will pass 8 hours.");
			return;
		}
		this.setMainTextBuffer("Select activities for your party-members to perform whilst resting.\n\nAdd Food for the party to eat. You must add " + C64Color.CYAN_TAG + this.getRequiredFood().ToString() + "</color> points of food if they are to recover fully.\n\nClick \"Begin Rest\" when you are ready. Resting will pass 8 hours.");
	}

	// Token: 0x0600094C RID: 2380 RVA: 0x0002C4C2 File Offset: 0x0002A6C2
	protected override void createGUI()
	{
		this.gridUIAsCampingFoodUI = new UIInventorySheetCampingFood(this.dataControl.getParty());
		this.gridUI = this.gridUIAsCampingFoodUI;
		this.guiControl = new GUIControlCamping(this.gridUI as UIInventorySheetCampingFood);
	}

	// Token: 0x0600094D RID: 2381 RVA: 0x0002C4FC File Offset: 0x0002A6FC
	protected override string getMainTextBuffer()
	{
		if (this.gridUIAsCampingFoodUI.getActivityDescription() != "")
		{
			return this.gridUIAsCampingFoodUI.getActivityDescription();
		}
		return base.getMainTextBuffer();
	}

	// Token: 0x0600094E RID: 2382 RVA: 0x0002C528 File Offset: 0x0002A728
	public override void update()
	{
		base.update();
		if (this.testExit())
		{
			return;
		}
		if (this.getTechnicalButtonsIndex() == 0)
		{
			this.clearCookpot();
		}
		else if (this.numericInputIndex == 1)
		{
			this.beginRest();
		}
		else if (this.numericInputIndex == 2)
		{
			if (this.getCampingOrder().allowPartyManagement())
			{
				this.clearCookpot();
				this.setTargetState(SkaldStates.PartyManagement);
			}
			else
			{
				PopUpControl.addPopUpOK("You can only manage your party if resting in a comfortable camp or lodgings.\n\nFind bed or an inn!");
			}
		}
		this.numericInputIndex = -1;
		this.setGUIData();
	}

	// Token: 0x0600094F RID: 2383 RVA: 0x0002C5A2 File Offset: 0x0002A7A2
	private CampActivityState.CampingOrder getCampingOrder()
	{
		return this.dataControl.getCampingOrder();
	}

	// Token: 0x06000950 RID: 2384 RVA: 0x0002C5AF File Offset: 0x0002A7AF
	protected override void interactWithCurrentItemFromMainInventory()
	{
		this.getSecondaryInventory().addItem(this.getMainInventory().removeCurrentItem());
		AudioControl.playDefaultInventorySound();
		this.selectCurrentItemFromSecondary();
	}

	// Token: 0x06000951 RID: 2385 RVA: 0x0002C5D3 File Offset: 0x0002A7D3
	protected override void interactWithCurrentItemFromSecondaryInventory()
	{
		this.getMainInventory().addItem(this.getSecondaryInventory().removeCurrentItem());
		AudioControl.playDefaultInventorySound();
		this.selectCurrentItemFromMain();
	}

	// Token: 0x06000952 RID: 2386 RVA: 0x0002C5F7 File Offset: 0x0002A7F7
	protected override Inventory getSecondaryInventory()
	{
		return this.cookingPot;
	}

	// Token: 0x06000953 RID: 2387 RVA: 0x0002C5FF File Offset: 0x0002A7FF
	private void clearCookpot()
	{
		AudioControl.playDefaultInventorySound();
		this.getMainInventory().transferInventory(this.cookingPot);
	}

	// Token: 0x06000954 RID: 2388 RVA: 0x0002C618 File Offset: 0x0002A818
	private void beginRest()
	{
		float num = 0f;
		if (this.getRequiredFood() == 0)
		{
			num = 1f;
		}
		else if (this.getCurrentFood() > 0)
		{
			num = (float)this.getCurrentFood() / (float)this.getRequiredFood();
		}
		this.cookingPot.deleteAllItems();
		this.restSummary.Add("Camp activities begin.");
		foreach (Character character in this.dataControl.getParty().getLiveCharacters())
		{
			character.getConditionContainer().deleteComponent("CON_Motivated");
		}
		foreach (Character character2 in this.dataControl.getParty().getLiveCharacters())
		{
			if (character2.isBloodied())
			{
				this.restSummary.Add(character2.getName() + " is too heavily wounded to work.");
			}
			else if (num >= 0.5f)
			{
				this.restSummary.Add(character2.performPreferredCampActivity().getResultString());
			}
			else
			{
				this.restSummary.Add(character2.getName() + " is too hungry to work.");
			}
		}
		this.restSummary.Add("Z");
		this.restSummary.Add("Z z");
		this.restSummary.Add("Z z Z");
		if (num >= 1f)
		{
			this.restSummary.Add("The party was well-fed and rested 8 hours, recovering fully.");
			this.restSummary.Add("The party was well-fed and rested 8 hours, recovering fully.");
			this.dataControl.restFull();
		}
		else
		{
			this.restSummary.Add("The party rested 8 hours and recovered partially.");
			this.restSummary.Add("The party rested 8 hours and recovered partially.");
			this.dataControl.restPartial(num);
		}
		this.terminating = true;
		PopUpControl.addPopUpRest(this);
	}

	// Token: 0x06000955 RID: 2389 RVA: 0x0002C80C File Offset: 0x0002AA0C
	public string getRestingPopUpImagePath()
	{
		return this.getCampingOrder().getRestingPopUpImagePath();
	}

	// Token: 0x06000956 RID: 2390 RVA: 0x0002C819 File Offset: 0x0002AA19
	protected override void clearAndGoToOverland()
	{
		this.clearCookpot();
		base.clearAndGoToOverland();
	}

	// Token: 0x06000957 RID: 2391 RVA: 0x0002C827 File Offset: 0x0002AA27
	public List<string> getRestSummary()
	{
		return this.restSummary;
	}

	// Token: 0x06000958 RID: 2392 RVA: 0x0002C82F File Offset: 0x0002AA2F
	public void exit()
	{
		this.clearAndGoToOverland();
	}

	// Token: 0x06000959 RID: 2393 RVA: 0x0002C837 File Offset: 0x0002AA37
	private int getCurrentFood()
	{
		return this.cookingPot.getFoodCount();
	}

	// Token: 0x0600095A RID: 2394 RVA: 0x0002C844 File Offset: 0x0002AA44
	private int getRequiredFood()
	{
		if (this.getCampingOrder() != null && !this.getCampingOrder().requireFood())
		{
			return 0;
		}
		return this.dataControl.getParty().getFoodRequiement();
	}

	// Token: 0x0600095B RID: 2395 RVA: 0x0002C86D File Offset: 0x0002AA6D
	private int getTechnicalButtonsIndex()
	{
		return this.gridUIAsCampingFoodUI.getTechnicalButtonsIndex();
	}

	// Token: 0x0600095C RID: 2396 RVA: 0x0002C87C File Offset: 0x0002AA7C
	protected override void setGUIData()
	{
		if (!this.terminating)
		{
			base.setGUIData(new List<string>
			{
				"Begin Rest",
				"Manage Party"
			}, this.cookingPot);
			this.guiControl.setSheetHeader(this.header);
			this.guiControl.setSecondaryDescription(this.dataControl.getDescription());
			if (this.gridUIAsCampingFoodUI != null)
			{
				if (GlobalSettings.getDifficultySettings().ignoreFood())
				{
					this.gridUIAsCampingFoodUI.setMealLabel(string.Concat(new string[]
					{
						"Food: ",
						this.getCurrentFood().ToString(),
						"/",
						this.getRequiredFood().ToString(),
						" [Disabled]"
					}));
				}
				else
				{
					this.gridUIAsCampingFoodUI.setMealLabel("Food: " + this.getCurrentFood().ToString() + "/" + this.getRequiredFood().ToString());
				}
			}
		}
		else
		{
			base.setGUIData();
			this.guiControl.clearSheetComplexAndButtons();
		}
		this.guiControl.clearMap();
	}

	// Token: 0x04000255 RID: 597
	private Inventory cookingPot = new Inventory();

	// Token: 0x04000256 RID: 598
	private UIInventorySheetCampingFood gridUIAsCampingFoodUI;

	// Token: 0x04000257 RID: 599
	private bool terminating;

	// Token: 0x04000258 RID: 600
	private List<string> restSummary = new List<string>();

	// Token: 0x04000259 RID: 601
	private string header;

	// Token: 0x02000231 RID: 561
	public class CampingOrderInn : CampActivityState.CampingOrder
	{
		// Token: 0x060018CD RID: 6349 RVA: 0x0006CE9E File Offset: 0x0006B09E
		public override bool allowPartyManagement()
		{
			return true;
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x0006CEA1 File Offset: 0x0006B0A1
		public override string getDescription()
		{
			return "A Comfortable Room";
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x0006CEA8 File Offset: 0x0006B0A8
		public override bool requireFood()
		{
			return false;
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x0006CEAB File Offset: 0x0006B0AB
		public override string getRestingPopUpImagePath()
		{
			return "CampInn";
		}
	}

	// Token: 0x02000232 RID: 562
	public class CampingOrderRoughCamp : CampActivityState.CampingOrder
	{
		// Token: 0x060018D2 RID: 6354 RVA: 0x0006CEBA File Offset: 0x0006B0BA
		public override string getDescription()
		{
			return "A Rough Camp";
		}
	}

	// Token: 0x02000233 RID: 563
	public class CampingOrderBed : CampActivityState.CampingOrder
	{
		// Token: 0x060018D4 RID: 6356 RVA: 0x0006CEC9 File Offset: 0x0006B0C9
		public override bool allowPartyManagement()
		{
			return true;
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x0006CECC File Offset: 0x0006B0CC
		public override string getDescription()
		{
			return "A Comfortable Camp";
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x0006CED3 File Offset: 0x0006B0D3
		public override string getRestingPopUpImagePath()
		{
			return "CampBed";
		}
	}

	// Token: 0x02000234 RID: 564
	public abstract class CampingOrder
	{
		// Token: 0x060018D8 RID: 6360 RVA: 0x0006CEE2 File Offset: 0x0006B0E2
		public virtual bool allowPartyManagement()
		{
			return false;
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x0006CEE5 File Offset: 0x0006B0E5
		public virtual bool requireFood()
		{
			return true;
		}

		// Token: 0x060018DA RID: 6362
		public abstract string getDescription();

		// Token: 0x060018DB RID: 6363 RVA: 0x0006CEE8 File Offset: 0x0006B0E8
		public virtual string getRestingPopUpImagePath()
		{
			return "Campfire1";
		}
	}
}
