using System;
using System.Collections.Generic;
using SkaldEnums;

// Token: 0x0200006E RID: 110
public class CharacterCreationApperanceState : CharacterBuilderBaseState
{
	// Token: 0x06000962 RID: 2402 RVA: 0x0002CC04 File Offset: 0x0002AE04
	public CharacterCreationApperanceState(DataControl dataControl) : base(dataControl)
	{
		this.setMainTextBuffer("You can use this screen to customize your characters physical appearance.\n\nNote that changes made here are purely cosmetic.");
		this.stateId = SkaldStates.ApperanceEditor;
		this.guiControl.setNumericButtonsAsAXBY();
		this.hairStyles = GameData.getPlayerHairStyles();
		this.beardStyles = GameData.getPlayerBeardStyles();
		this.characterModels = new List<string>
		{
			"MinimalistFighterColorSwap",
			"MinimalistClericColorSwap",
			"MinimalistThiefDaggersColorSwap",
			"MinimalistMageColorSwap"
		};
		this.portraits = GameData.getPlayerPortraits();
		bool flag = dataControl.getMainCharacter().isPaperDoll();
		this.genderButton = this.textSliderControl.createAndReturnSliderButton("Gender", new List<string>
		{
			"Male",
			"Female"
		}, "", 0);
		this.portraitButton = this.textSliderControl.createAndReturnSliderButton("Portrait", this.portraits, "Portrait", 0);
		if (flag)
		{
			this.hairStyleButton = this.textSliderControl.createAndReturnSliderButton("Hair Style", this.hairStyles, "Style", 5);
			this.beardStyleButton = this.textSliderControl.createAndReturnSliderButton("Facial Hair", this.beardStyles, "Style", 5);
		}
		else
		{
			this.characterModelButton = this.textSliderControl.createAndReturnSliderButton("Model", this.characterModels, "Model", 0);
		}
		this.hairColorButton = this.textSliderControl.createAndReturnSliderButton("Hair Color", C64Color.getHairColors(), "Color", 7);
		this.skinColorButton = this.textSliderControl.createAndReturnSliderButton("Skin Tone", C64Color.getSkinColors(), "Color", 0);
		this.mainColorButton = this.textSliderControl.createAndReturnSliderButton("Main Color", C64Color.getClothingColors(), "Color", 10);
		this.secondaryColorButton = this.textSliderControl.createAndReturnSliderButton("Secondary Color", C64Color.getClothingColors(), "Color", 13);
		this.tertiaryColorButton = this.textSliderControl.createAndReturnSliderButton("Tertiary Color", C64Color.getClothingColors(), "Color", 8);
		this.applyApperanceData();
		this.setGUIData();
		base.addIntroPopUp("Finalize your character by editing their appearance.");
	}

	// Token: 0x06000963 RID: 2403 RVA: 0x0002CE25 File Offset: 0x0002B025
	protected override void createGUI()
	{
		this.guiControl = new GUIControlApperanceEditorSheet(this.textSliderControl);
	}

	// Token: 0x06000964 RID: 2404 RVA: 0x0002CE38 File Offset: 0x0002B038
	public override void update()
	{
		if (this.done)
		{
			this.venturForth();
			return;
		}
		base.update();
		this.textSliderControl.update();
		this.applyApperanceData();
		this.setGUIData();
	}

	// Token: 0x06000965 RID: 2405 RVA: 0x0002CE68 File Offset: 0x0002B068
	protected override void updateButtonPressIndex()
	{
		if (this.numericInputIndex == 0)
		{
			PopUpControl.addPopUpName(base.getCharacter());
			this.done = true;
		}
		else if (this.numericInputIndex == 1)
		{
			this.textSliderControl.randomize();
		}
		else if (this.numericInputIndex == 2)
		{
			base.exit();
		}
		this.numericInputIndex = -1;
	}

	// Token: 0x06000966 RID: 2406 RVA: 0x0002CEC0 File Offset: 0x0002B0C0
	private void applyApperanceData()
	{
		if (this.genderButton.getValue() == "Male")
		{
			base.getCharacter().setIsCharacterMale(true);
		}
		else
		{
			base.getCharacter().setIsCharacterMale(false);
		}
		if (this.hairStyleButton != null)
		{
			base.getCharacter().getLooksControl().setHairStyleId(this.hairStyles[this.hairStyleButton.getPointer()].id);
		}
		if (this.beardStyleButton != null)
		{
			base.getCharacter().getLooksControl().setBeardStyleId(this.beardStyles[this.beardStyleButton.getPointer()].id);
		}
		if (this.characterModelButton != null)
		{
			base.getCharacter().setModelPath("Models/" + this.characterModels[this.characterModelButton.getPointer()]);
		}
		base.getCharacter().setPortraitPath(this.portraits[this.portraitButton.getPointer()].id);
		base.getCharacter().getLooksControl().setSkinColor(this.skinColorButton.getValue());
		base.getCharacter().getLooksControl().setHairColor(this.hairColorButton.getValue());
		base.getCharacter().getLooksControl().setMainColor(this.mainColorButton.getValue());
		base.getCharacter().getLooksControl().setSecondaryColor(this.secondaryColorButton.getValue());
		base.getCharacter().getLooksControl().setTertiaryColor(this.tertiaryColorButton.getValue());
	}

	// Token: 0x06000967 RID: 2407 RVA: 0x0002D046 File Offset: 0x0002B246
	protected override string getSheetName()
	{
		return "Customize Appearance";
	}

	// Token: 0x06000968 RID: 2408 RVA: 0x0002D050 File Offset: 0x0002B250
	protected override void setGUIData()
	{
		base.setGUIData();
		(this.guiControl as GUIControlApperanceEditorSheet).setModelAndCharacterPortrait(base.getCharacter().getFixedTexture(), base.getCharacter().getPortrait());
		base.setButtons(new List<string>
		{
			"Continue",
			"Randomize"
		});
		this.guiControl.revealAll();
	}

	// Token: 0x0400025C RID: 604
	private UITextSliderControl textSliderControl = new UITextSliderControl();

	// Token: 0x0400025D RID: 605
	private UITextSliderControl.UITextSliderButton genderButton;

	// Token: 0x0400025E RID: 606
	private UITextSliderControl.UITextSliderButton portraitButton;

	// Token: 0x0400025F RID: 607
	private UITextSliderControl.UITextSliderButton hairStyleButton;

	// Token: 0x04000260 RID: 608
	private UITextSliderControl.UITextSliderButton beardStyleButton;

	// Token: 0x04000261 RID: 609
	private UITextSliderControl.UITextSliderButton characterModelButton;

	// Token: 0x04000262 RID: 610
	private UITextSliderControl.UITextSliderButton skinColorButton;

	// Token: 0x04000263 RID: 611
	private UITextSliderControl.UITextSliderButton hairColorButton;

	// Token: 0x04000264 RID: 612
	private UITextSliderControl.UITextSliderButton mainColorButton;

	// Token: 0x04000265 RID: 613
	private UITextSliderControl.UITextSliderButton secondaryColorButton;

	// Token: 0x04000266 RID: 614
	private UITextSliderControl.UITextSliderButton tertiaryColorButton;

	// Token: 0x04000267 RID: 615
	private List<SKALDProjectData.ApperanceElementContainers.ApperanceElement> hairStyles;

	// Token: 0x04000268 RID: 616
	private List<SKALDProjectData.ApperanceElementContainers.ApperanceElement> beardStyles;

	// Token: 0x04000269 RID: 617
	private List<string> characterModels;

	// Token: 0x0400026A RID: 618
	private List<SKALDProjectData.ApperanceElementContainers.ApperanceElement> portraits;

	// Token: 0x0400026B RID: 619
	private bool done;
}
