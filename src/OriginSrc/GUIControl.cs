using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000116 RID: 278
public class GUIControl
{
	// Token: 0x06001190 RID: 4496 RVA: 0x0004EBEC File Offset: 0x0004CDEC
	public GUIControl()
	{
		this.spriteRenderer = GameObject.Find("GameControl").GetComponent<SpriteRenderer>();
		if (this.spriteRenderer == null)
		{
			MainControl.logError("Could not find Draw Target!");
		}
		this.outputImage = new TextureTools.TextureData(480, 270);
	}

	// Token: 0x06001191 RID: 4497 RVA: 0x0004EC44 File Offset: 0x0004CE44
	protected Texture2D getOutputTexture2D()
	{
		if (GUIControl.outputTexture2D == null)
		{
			try
			{
				GUIControl.outputTexture2D = new Texture2D(480, 270);
				GUIControl.outputTexture2D.filterMode = FilterMode.Point;
			}
			catch (Exception obj)
			{
				PopUpControl.addPopUpOK("ERROR: A Create Texture2D error occured! Terminate the game and send the developer the Player.txt file located in the local game folder.");
				MainControl.logError(obj);
			}
		}
		return GUIControl.outputTexture2D;
	}

	// Token: 0x06001192 RID: 4498 RVA: 0x0004ECA8 File Offset: 0x0004CEA8
	public void setBackground(string path)
	{
		if (this.backgroundImage == null)
		{
			this.backgroundImage = new GUIControl.BackgroundImage();
		}
		this.backgroundImage.setImage(path);
	}

	// Token: 0x06001193 RID: 4499 RVA: 0x0004ECC9 File Offset: 0x0004CEC9
	public void setBackground(TextureTools.TextureData texture)
	{
		if (this.backgroundImage == null)
		{
			this.backgroundImage = new GUIControl.BackgroundImage();
		}
		this.backgroundImage.setImage(texture);
	}

	// Token: 0x06001194 RID: 4500 RVA: 0x0004ECEA File Offset: 0x0004CEEA
	public int getListButtonPressIndex()
	{
		if (this.listButtons == null)
		{
			return -1;
		}
		return this.listButtons.getButtonPressIndexLeft();
	}

	// Token: 0x06001195 RID: 4501 RVA: 0x0004ED01 File Offset: 0x0004CF01
	public UIElement getHandoffElement()
	{
		if (this.sceneDescription != null)
		{
			return this.sceneDescription;
		}
		if (this.sheetComplex != null)
		{
			return this.sheetComplex;
		}
		return null;
	}

	// Token: 0x06001196 RID: 4502 RVA: 0x0004ED22 File Offset: 0x0004CF22
	private void setHandoffElement(UIElement element)
	{
		if (element is GUIControl.SheetComplex && this.sheetComplex != null)
		{
			return;
		}
		if (element is GUIControl.SceneDescription && this.sceneDescription != null)
		{
			return;
		}
		this.handoffElement = element;
		if (this.handoffElement != null)
		{
			this.handoffElement.setTargetDimensionsY(0);
		}
	}

	// Token: 0x06001197 RID: 4503 RVA: 0x0004ED61 File Offset: 0x0004CF61
	public void addInfoBark(string input, Item i)
	{
		this.addInfoBark(input, i.getInventoryGridX(), i.getInventoryGridY());
	}

	// Token: 0x06001198 RID: 4504 RVA: 0x0004ED76 File Offset: 0x0004CF76
	public void addInfoBark(string input, int x, int y)
	{
		if (this.barkControl == null)
		{
			this.barkControl = new BarkControl(0, 0);
		}
		this.barkControl.addInventoryBark(input, 0, x, y);
	}

	// Token: 0x06001199 RID: 4505 RVA: 0x0004ED9C File Offset: 0x0004CF9C
	public SkaldPoint2D getSheetPosition()
	{
		if (this.sheetComplex == null)
		{
			return null;
		}
		return this.sheetComplex.getPosition();
	}

	// Token: 0x0600119A RID: 4506 RVA: 0x0004EDB3 File Offset: 0x0004CFB3
	public SkaldPoint2D getCallToActionPosition()
	{
		if (this.callToAction == null)
		{
			return null;
		}
		return this.callToAction.getPosition();
	}

	// Token: 0x0600119B RID: 4507 RVA: 0x0004EDCC File Offset: 0x0004CFCC
	public void recieveOldGuiData(GUIControl oldGui)
	{
		if (oldGui == null)
		{
			return;
		}
		if (this.sheetComplex != null && oldGui.getSheetPosition() != null)
		{
			this.sheetComplex.setPosition(oldGui.getSheetPosition());
		}
		if (this.callToAction != null && oldGui.getCallToActionPosition() != null)
		{
			this.callToAction.setPosition(oldGui.getCallToActionPosition());
		}
		this.setHandoffElement(oldGui.getHandoffElement());
	}

	// Token: 0x0600119C RID: 4508 RVA: 0x0004EE2B File Offset: 0x0004D02B
	public int getPortraitPressIndex()
	{
		if (this.portraitContainer == null)
		{
			return -1;
		}
		return this.portraitContainer.getPortraitPressIndex();
	}

	// Token: 0x0600119D RID: 4509 RVA: 0x0004EE42 File Offset: 0x0004D042
	public int getPortraitPressIndexRight()
	{
		if (this.portraitContainer == null)
		{
			return -1;
		}
		return this.portraitContainer.getPortraitPressIndexRight();
	}

	// Token: 0x0600119E RID: 4510 RVA: 0x0004EE59 File Offset: 0x0004D059
	public int getUIButtonPressIndex()
	{
		if (this.sheetComplex == null)
		{
			return -1;
		}
		return this.sheetComplex.getTabRowButtonPressIndex();
	}

	// Token: 0x0600119F RID: 4511 RVA: 0x0004EE70 File Offset: 0x0004D070
	public virtual int updateLeftScrollBarAndReturnIndex(int itemCount)
	{
		if (this.sheetComplex == null)
		{
			return -1;
		}
		return this.sheetComplex.updateLeftScrollbarAndReturnIndex(itemCount);
	}

	// Token: 0x060011A0 RID: 4512 RVA: 0x0004EE88 File Offset: 0x0004D088
	public void updateRightScrollBar()
	{
		if (this.sheetComplex == null)
		{
			return;
		}
		this.sheetComplex.updateRightScrollbar();
	}

	// Token: 0x060011A1 RID: 4513 RVA: 0x0004EE9E File Offset: 0x0004D09E
	public int getNumericButtonPressIndex()
	{
		if (this.numericButtons == null)
		{
			return -1;
		}
		return this.numericButtons.getButtonPressIndexLeft();
	}

	// Token: 0x060011A2 RID: 4514 RVA: 0x0004EEB5 File Offset: 0x0004D0B5
	public int getButtonRowPressIndex()
	{
		if (this.combatButtonRow == null)
		{
			return -1;
		}
		return this.combatButtonRow.getButtonPressIndexLeft();
	}

	// Token: 0x060011A3 RID: 4515 RVA: 0x0004EECC File Offset: 0x0004D0CC
	public bool hoveringOverCombatTabs()
	{
		return (this.combatButtonRow != null && this.combatButtonRow.hoveringOverButtons()) || (this.weaponSwapControl != null && this.weaponSwapControl.hoveringOverButtons()) || (this.abilitySelectorGrid != null && this.abilitySelectorGrid.hoveringOverButtons());
	}

	// Token: 0x060011A4 RID: 4516 RVA: 0x0004EF1F File Offset: 0x0004D11F
	public void toggleAdjustVertical()
	{
		this.adjustVertical = true;
	}

	// Token: 0x060011A5 RID: 4517 RVA: 0x0004EF28 File Offset: 0x0004D128
	public string getButtonRowHovertext()
	{
		if (this.combatButtonRow == null)
		{
			return "";
		}
		return this.combatButtonRow.getHoverText();
	}

	// Token: 0x060011A6 RID: 4518 RVA: 0x0004EF43 File Offset: 0x0004D143
	public virtual void setAbilitySelectorGrid(UIAbilitySelectorGrid grid)
	{
	}

	// Token: 0x060011A7 RID: 4519 RVA: 0x0004EF45 File Offset: 0x0004D145
	public virtual void setNumericButtons(List<string> options)
	{
		if (this.numericButtons == null)
		{
			return;
		}
		this.numericButtons.setButtons(options);
	}

	// Token: 0x060011A8 RID: 4520 RVA: 0x0004EF5C File Offset: 0x0004D15C
	public virtual void setCombatOrderButtonRow(List<UIButtonControlBase.ButtonData> options)
	{
		if (this.combatButtonRow == null)
		{
			return;
		}
		this.combatButtonRow.setButtons(options);
	}

	// Token: 0x060011A9 RID: 4521 RVA: 0x0004EF73 File Offset: 0x0004D173
	public void clearSheetComplexAndButtons()
	{
		this.sheetComplex = null;
		this.numericButtons = null;
		this.listButtons = null;
	}

	// Token: 0x060011AA RID: 4522 RVA: 0x0004EF8A File Offset: 0x0004D18A
	public virtual void setActionCounter(Character character, AbilityUseable ability)
	{
	}

	// Token: 0x060011AB RID: 4523 RVA: 0x0004EF8C File Offset: 0x0004D18C
	public virtual void setInitiativeCounter(UIInitiativeList list)
	{
	}

	// Token: 0x060011AC RID: 4524 RVA: 0x0004EF8E File Offset: 0x0004D18E
	public virtual void setAndUpdateWeaponSwapControl(Character character)
	{
		if (this.weaponSwapControl == null)
		{
			return;
		}
		this.weaponSwapControl.setButtonsAndUpdate(character);
	}

	// Token: 0x060011AD RID: 4525 RVA: 0x0004EFA5 File Offset: 0x0004D1A5
	public virtual void setContextualButton(string input)
	{
		if (input == "")
		{
			this.contextualButton = null;
			return;
		}
		if (this.contextualButton == null)
		{
			this.contextualButton = new GUIControl.UIContextualButton();
		}
		this.contextualButton.setContent(input);
	}

	// Token: 0x060011AE RID: 4526 RVA: 0x0004EFDB File Offset: 0x0004D1DB
	public virtual void setMainMenuButton()
	{
		if (this.mainMenuButton == null)
		{
			if (GlobalSettings.getGamePlaySettings().isNostalgiaModeOn())
			{
				this.mainMenuButton = new GUIControl.UIMainMenuButtonNostalgic();
				return;
			}
			this.mainMenuButton = new GUIControl.UIMainMenuButtonNormal();
		}
	}

	// Token: 0x060011AF RID: 4527 RVA: 0x0004F008 File Offset: 0x0004D208
	public virtual void setTabRowButtons(List<string> options, int currentIndex)
	{
		if (this.sheetComplex == null)
		{
			return;
		}
		this.sheetComplex.setTabRow(options, currentIndex);
	}

	// Token: 0x060011B0 RID: 4528 RVA: 0x0004F020 File Offset: 0x0004D220
	public virtual void setListButtons(List<string> options)
	{
		if (this.listButtons == null)
		{
			return;
		}
		this.listButtons.setButtons(options);
	}

	// Token: 0x060011B1 RID: 4529 RVA: 0x0004F038 File Offset: 0x0004D238
	public void setPortrait(List<TextureTools.TextureData> portraits)
	{
		if ((GlobalSettings.getGamePlaySettings().isNostalgiaModeOn() && this.portraitContainer is GUIControl.PortraitContainerNormal) || (!GlobalSettings.getGamePlaySettings().isNostalgiaModeOn() && this.portraitContainer is GUIControl.PortraitContainerNostalgic))
		{
			this.portraitContainer = null;
		}
		if (this.portraitContainer == null)
		{
			if (GlobalSettings.getGamePlaySettings().isNostalgiaModeOn())
			{
				this.portraitContainer = new GUIControl.PortraitContainerNostalgic();
			}
			else
			{
				this.portraitContainer = new GUIControl.PortraitContainerNormal();
			}
		}
		this.portraitContainer.setPortraits(portraits);
	}

	// Token: 0x060011B2 RID: 4530 RVA: 0x0004F0B6 File Offset: 0x0004D2B6
	public virtual void setBigHeader(string input)
	{
		this.bigHeader = new GUIControl.LargeHeader();
		this.bigHeader.setContent(input);
	}

	// Token: 0x060011B3 RID: 4531 RVA: 0x0004F0CF File Offset: 0x0004D2CF
	public void setSheetHeader(string input)
	{
		if (this.sheetComplex == null)
		{
			return;
		}
		this.sheetComplex.setHeader(input);
	}

	// Token: 0x060011B4 RID: 4532 RVA: 0x0004F0E8 File Offset: 0x0004D2E8
	public void setPrimaryHeader(string input)
	{
		if (input == null || input == "")
		{
			return;
		}
		if ((GlobalSettings.getGamePlaySettings().isNostalgiaModeOn() && !(this.primaryHeader is GUIControl.PrimaryHeaderNostalgic)) || (!GlobalSettings.getGamePlaySettings().isNostalgiaModeOn() && !(this.primaryHeader is GUIControl.PrimaryHeaderNormal)))
		{
			this.primaryHeader = null;
		}
		if (this.primaryHeader == null)
		{
			if (GlobalSettings.getGamePlaySettings().isNostalgiaModeOn())
			{
				this.primaryHeader = new GUIControl.PrimaryHeaderNostalgic();
			}
			else
			{
				this.primaryHeader = new GUIControl.PrimaryHeaderNormal();
			}
		}
		this.primaryHeader.setContent(input);
	}

	// Token: 0x060011B5 RID: 4533 RVA: 0x0004F177 File Offset: 0x0004D377
	public void setSheetDescription(string input)
	{
		if (this.sheetComplex == null)
		{
			return;
		}
		if (input == "")
		{
			return;
		}
		this.sheetComplex.setDescription(input);
	}

	// Token: 0x060011B6 RID: 4534 RVA: 0x0004F19C File Offset: 0x0004D39C
	public virtual void setSceneDescription(string input)
	{
		if (this.sceneDescription == null)
		{
			return;
		}
		if (GlobalSettings.getFontSettings().getTextBackgroundColor() == 2)
		{
			this.sceneDescription.backgroundTexture = TextureTools.loadTextureData("Images/GUIIcons/TextBoxMainBlack");
		}
		else if (GlobalSettings.getFontSettings().getTextBackgroundColor() == 1)
		{
			this.sceneDescription.backgroundTexture = TextureTools.loadTextureData("Images/GUIIcons/TextBoxMainBrown");
		}
		else
		{
			this.sceneDescription.backgroundTexture = TextureTools.loadTextureData("Images/GUIIcons/TextBoxMain");
		}
		this.sceneDescription.setUpperCaseTooltipsWords(true);
		this.sceneDescription.setHighlightQuotes(true);
		this.sceneDescription.setTooltipHighlightColor(C64Color.Cyan);
		this.sceneDescription.padding = new UIElement.Padding(9, 20, 5, 48);
		this.sceneDescription.foregroundColors.shadowMainColor = C64Color.SmallTextShadowColor;
		this.sceneDescription.setTooltips(ToolTipControl.getLoreToolTips());
		this.sceneDescription.illuminatedFont = FontContainer.getIlluminatedFont();
		if (this.sceneDescription.backgroundTexture != null)
		{
			this.sceneDescription.setWidth(this.sceneDescription.backgroundTexture.width);
		}
		this.sceneDescription.setContent(input);
		this.sceneDescription.setReveal(false);
	}

	// Token: 0x060011B7 RID: 4535 RVA: 0x0004F2C4 File Offset: 0x0004D4C4
	public void setSecondaryDescription(string input)
	{
		if ((GlobalSettings.getGamePlaySettings().isNostalgiaModeOn() && this.secondaryDescription is GUIControl.SecondaryDescription) || (!GlobalSettings.getGamePlaySettings().isNostalgiaModeOn() && this.secondaryDescription is GUIControl.SecondaryDescriptionNostalgic))
		{
			this.secondaryDescription = null;
		}
		if (this.secondaryDescription == null)
		{
			if (GlobalSettings.getGamePlaySettings().isNostalgiaModeOn())
			{
				this.secondaryDescription = new GUIControl.SecondaryDescriptionNostalgic();
			}
			else
			{
				this.secondaryDescription = new GUIControl.SecondaryDescription();
			}
		}
		this.secondaryDescription.setContent(input);
	}

	// Token: 0x060011B8 RID: 4536 RVA: 0x0004F342 File Offset: 0x0004D542
	public void setMap(UIMap mapCanvas)
	{
		this.map = mapCanvas;
	}

	// Token: 0x060011B9 RID: 4537 RVA: 0x0004F34B File Offset: 0x0004D54B
	public bool hasMap()
	{
		return this.map != null && this.map.backgroundTexture != null;
	}

	// Token: 0x060011BA RID: 4538 RVA: 0x0004F365 File Offset: 0x0004D565
	public void setNumericButtonsAsABXY()
	{
		if (this.numericButtons == null)
		{
			return;
		}
		this.numericButtons.toggleABXYControllerPressControl();
	}

	// Token: 0x060011BB RID: 4539 RVA: 0x0004F37B File Offset: 0x0004D57B
	public void setNumericButtonsAsAXBY()
	{
		if (this.numericButtons == null)
		{
			return;
		}
		this.numericButtons.toggleAXBYControllerPressControl();
	}

	// Token: 0x060011BC RID: 4540 RVA: 0x0004F391 File Offset: 0x0004D591
	public void clearMap()
	{
		this.map = null;
	}

	// Token: 0x060011BD RID: 4541 RVA: 0x0004F39C File Offset: 0x0004D59C
	protected virtual void bakeOutputTexture()
	{
		if (this.map != null && (this.mainImage == null || this.mainImage.backgroundTexture == null))
		{
			this.map.draw(this.outputImage);
		}
		if (this.mainImage != null)
		{
			this.mainImage.draw(this.outputImage);
		}
		if (this.handoffElement != null && !this.handoffElement.hasArrived())
		{
			this.handoffElement.draw(this.outputImage);
		}
		if (this.portraitContainer != null)
		{
			this.portraitContainer.draw(this.outputImage);
		}
		if (this.backgroundImage != null && this.backgroundImage.isImageSet())
		{
			this.backgroundImage.draw(this.outputImage);
		}
		if (this.sheetComplex != null)
		{
			this.sheetComplex.draw(this.outputImage);
		}
		if (this.secondaryDescription != null)
		{
			this.secondaryDescription.draw(this.outputImage);
		}
		if (this.sceneDescription != null)
		{
			this.sceneDescription.draw(this.outputImage);
		}
		if (this.primaryHeader != null)
		{
			this.primaryHeader.draw(this.outputImage);
		}
		if (this.bigHeader != null)
		{
			this.bigHeader.draw(this.outputImage);
		}
		if (this.combatButtonRow != null)
		{
			this.combatButtonRow.draw(this.outputImage);
		}
		if (this.actionCounter != null)
		{
			this.actionCounter.draw(this.outputImage);
		}
		if (this.initiativeList != null)
		{
			this.initiativeList.draw(this.outputImage);
		}
		if (this.abilitySelectorGrid != null)
		{
			this.abilitySelectorGrid.draw(this.outputImage);
		}
		if (this.weaponSwapControl != null)
		{
			this.weaponSwapControl.draw(this.outputImage);
		}
		if (this.effectSelector != null)
		{
			this.effectSelector.draw(this.outputImage);
		}
		if (this.contextualButton != null)
		{
			this.contextualButton.draw(this.outputImage);
		}
		if (this.mainMenuButton != null)
		{
			this.mainMenuButton.draw(this.outputImage);
		}
		if (this.numericButtons != null)
		{
			this.numericButtons.draw(this.outputImage);
		}
		if (this.barkControl != null)
		{
			foreach (TextureTools.Sprite sprite in this.barkControl.getBarkSprites())
			{
				sprite.ensureSpritePositionOnScreen();
				sprite.texture.applyOverlay(sprite.x, sprite.y, this.outputImage);
			}
		}
		if (this.callToAction != null)
		{
			this.callToAction.draw(this.outputImage);
		}
		if (this.listButtons != null)
		{
			this.listButtons.draw(this.outputImage);
		}
		PopUpControl.draw(this.outputImage);
		ToolTipPrinter.draw(this.outputImage);
		SkaldControllerGlyphPrinter.draw(this.outputImage);
		if (this.mainImage == null)
		{
			HoverElementControl.draw(this.outputImage);
		}
		MouseControl.drawMouse(this.outputImage);
		this.outputImage.bakeTexture2D(this.getOutputTexture2D());
		this.setSprite();
		this.outputImage.clear();
	}

	// Token: 0x060011BE RID: 4542 RVA: 0x0004F6B8 File Offset: 0x0004D8B8
	protected void bakeOutputTextureCutScene()
	{
		CutSceneControl.draw(this.outputImage);
		this.outputImage.bakeTexture2D(this.getOutputTexture2D());
		this.setSprite();
		this.outputImage.clear();
	}

	// Token: 0x060011BF RID: 4543 RVA: 0x0004F6E7 File Offset: 0x0004D8E7
	public bool isControllABXYPressActivated()
	{
		return this.numericButtons != null && this.numericButtons.isControllPressActivated();
	}

	// Token: 0x060011C0 RID: 4544 RVA: 0x0004F700 File Offset: 0x0004D900
	protected void setSprite()
	{
		if (this.spriteRenderer == null)
		{
			return;
		}
		Object.Destroy(this.spriteRenderer.sprite);
		if (this.getOutputTexture2D() != null)
		{
			this.spriteRenderer.sprite = Sprite.Create(this.getOutputTexture2D(), new Rect(0f, 0f, (float)this.getOutputTexture2D().width, (float)this.getOutputTexture2D().height), new Vector2(0.5f, 0.5f), 100f, 0U, 0);
		}
	}

	// Token: 0x060011C1 RID: 4545 RVA: 0x0004F790 File Offset: 0x0004D990
	private void adjustElementsVertically()
	{
		if (this.sceneDescription == null || this.numericButtons == null)
		{
			return;
		}
		this.sceneDescription.setTargetPosition(this.numericButtons.getHeight());
		int y = this.sceneDescription.getY() - this.sceneDescription.getHeight();
		int x = this.sceneDescription.getX() + this.sceneDescription.padding.left + 6;
		this.numericButtons.setPosition(x, y);
		this.numericButtons.alignElements();
	}

	// Token: 0x060011C2 RID: 4546 RVA: 0x0004F813 File Offset: 0x0004DA13
	public void setMouseToClosestAbilityButtonAbove()
	{
		if (this.combatButtonRow == null)
		{
			return;
		}
		this.combatButtonRow.decrementCurrentSelectedButton();
		this.setMouseToSelectedAbilityButton();
	}

	// Token: 0x060011C3 RID: 4547 RVA: 0x0004F82F File Offset: 0x0004DA2F
	public void setMouseToClosestAbilityButtonBelow()
	{
		if (this.combatButtonRow == null)
		{
			return;
		}
		this.combatButtonRow.incrementCurrentSelectedButton();
		this.setMouseToSelectedAbilityButton();
	}

	// Token: 0x060011C4 RID: 4548 RVA: 0x0004F84B File Offset: 0x0004DA4B
	private void setMouseToSelectedAbilityButton()
	{
		this.setMouseToUIElement(this.combatButtonRow, 8, -8);
	}

	// Token: 0x060011C5 RID: 4549 RVA: 0x0004F85C File Offset: 0x0004DA5C
	public virtual void setMouseToClosestOptionAbove()
	{
		if (this.getControllerScrollableList() == null)
		{
			return;
		}
		if (!SkaldIO.isControllerConnected())
		{
			return;
		}
		if (this.getControllerScrollableList().canControllerScrollDown())
		{
			this.getControllerScrollableList().decrementCurrentSelectedButton();
		}
		else if (this.sheetComplex != null)
		{
			this.sheetComplex.scrollLeftBarDown();
		}
		this.setMouseToSelectedOption();
	}

	// Token: 0x060011C6 RID: 4550 RVA: 0x0004F8B0 File Offset: 0x0004DAB0
	public virtual void setMouseToClosestOptionBelow()
	{
		if (this.getControllerScrollableList() == null)
		{
			return;
		}
		if (!SkaldIO.isControllerConnected())
		{
			return;
		}
		if (this.getControllerScrollableList().canControllerScrollUp())
		{
			this.getControllerScrollableList().incrementCurrentSelectedButton();
		}
		else if (this.sheetComplex != null)
		{
			this.sheetComplex.scrollLeftBarUp();
		}
		this.setMouseToSelectedOption();
	}

	// Token: 0x060011C7 RID: 4551 RVA: 0x0004F901 File Offset: 0x0004DB01
	public virtual void controllerScrollSidewaysRight()
	{
		if (this.sheetComplex == null)
		{
			return;
		}
		this.sheetComplex.controllerScrollSidewaysRight();
		this.setMouseToSelectedOption();
	}

	// Token: 0x060011C8 RID: 4552 RVA: 0x0004F91D File Offset: 0x0004DB1D
	public virtual void controllerScrollSidewaysLeft()
	{
		if (this.sheetComplex == null)
		{
			return;
		}
		this.sheetComplex.controllerScrollSidewaysLeft();
		this.setMouseToSelectedOption();
	}

	// Token: 0x060011C9 RID: 4553 RVA: 0x0004F939 File Offset: 0x0004DB39
	protected virtual void setMouseToSelectedOption()
	{
		if (!this.snapToOptions)
		{
			return;
		}
		this.setMouseToUIElement(this.getControllerScrollableList(), 2, -6);
	}

	// Token: 0x060011CA RID: 4554 RVA: 0x0004F954 File Offset: 0x0004DB54
	protected void setMouseToUIElement(UICanvas buttonControl, int xOffset, int yOffset)
	{
		if (buttonControl == null)
		{
			return;
		}
		if (!SkaldIO.isControllerConnected())
		{
			return;
		}
		UIElement currentControllerSelectedElement = buttonControl.getCurrentControllerSelectedElement();
		if (currentControllerSelectedElement == null)
		{
			return;
		}
		SkaldPoint2D position = currentControllerSelectedElement.getPosition();
		SkaldIO.setVirtualMousePosition(position.X + xOffset, position.Y + yOffset);
	}

	// Token: 0x060011CB RID: 4555 RVA: 0x0004F994 File Offset: 0x0004DB94
	public void resetCurrentSelectOption()
	{
		if (this.getControllerScrollableList() == null)
		{
			return;
		}
		this.getControllerScrollableList().clearCurrentSelectedButton();
	}

	// Token: 0x060011CC RID: 4556 RVA: 0x0004F9AA File Offset: 0x0004DBAA
	public void clearCurrentSelectedOptionAndSnap()
	{
		this.resetCurrentSelectOption();
		this.setMouseToSelectedOption();
	}

	// Token: 0x060011CD RID: 4557 RVA: 0x0004F9B8 File Offset: 0x0004DBB8
	protected virtual UICanvas getControllerScrollableList()
	{
		return this.numericButtons;
	}

	// Token: 0x060011CE RID: 4558 RVA: 0x0004F9C0 File Offset: 0x0004DBC0
	public virtual void update()
	{
		if (CutSceneControl.hasCutScene())
		{
			this.bakeOutputTextureCutScene();
			this.handoffElement = null;
			return;
		}
		if (this.adjustVertical)
		{
			this.adjustElementsVertically();
		}
		ToolTipPrinter.updateTooltips();
		this.contextualButtonPressed = false;
		if (this.contextualButton != null)
		{
			this.contextualButton.update();
		}
		if (SkaldIO.getPressedMainInteractKey() || (this.contextualButton != null && this.contextualButton.getLeftUp()))
		{
			this.contextualButtonPressed = true;
		}
		if (this.mainMenuButton != null)
		{
			this.mainMenuButton.update();
		}
		if (this.portraitContainer != null)
		{
			this.portraitContainer.update();
		}
		if (this.numericButtons != null)
		{
			this.numericButtons.update();
		}
		if (this.listButtons != null)
		{
			this.listButtons.update();
		}
		this.updateRightScrollBar();
		if (this.combatButtonRow != null)
		{
			this.combatButtonRow.update();
		}
		if (this.abilitySelectorGrid != null)
		{
			this.abilitySelectorGrid.update();
		}
		if (this.effectSelector != null)
		{
			this.effectSelector.update();
		}
		if (this.callToAction != null)
		{
			this.callToAction.update();
		}
		if (this.menuTab != null)
		{
			this.menuTab.update();
		}
		this.graduallyRevealText();
		this.bakeOutputTexture();
	}

	// Token: 0x060011CF RID: 4559 RVA: 0x0004FAEF File Offset: 0x0004DCEF
	protected virtual void graduallyRevealText()
	{
		if (this.bigHeader != null)
		{
			this.bigHeader.reveal();
		}
	}

	// Token: 0x060011D0 RID: 4560 RVA: 0x0004FB04 File Offset: 0x0004DD04
	public void revealAll()
	{
		if (this.sceneDescription != null)
		{
			this.sceneDescription.setReveal(true);
		}
		this.revealNumericButtons();
	}

	// Token: 0x060011D1 RID: 4561 RVA: 0x0004FB20 File Offset: 0x0004DD20
	protected void revealNumericButtons()
	{
		if (this.numericButtons == null)
		{
			return;
		}
		if (this.numericButtons.isRevealed())
		{
			return;
		}
		this.numericButtons.setReveal(true);
	}

	// Token: 0x060011D2 RID: 4562 RVA: 0x0004FB48 File Offset: 0x0004DD48
	public bool mainMenuButtonWasPressed()
	{
		if (this.mainMenuButton == null)
		{
			return false;
		}
		if ((GlobalSettings.getGamePlaySettings().isNostalgiaModeOn() && this.mainMenuButton is GUIControl.UIMainMenuButtonNormal) || (!GlobalSettings.getGamePlaySettings().isNostalgiaModeOn() && this.mainMenuButton is GUIControl.UIMainMenuButtonNostalgic))
		{
			this.mainMenuButton = null;
		}
		this.setMainMenuButton();
		return this.mainMenuButton.getLeftUp();
	}

	// Token: 0x060011D3 RID: 4563 RVA: 0x0004FBA9 File Offset: 0x0004DDA9
	public bool contextualButtonWasPressed()
	{
		return this.contextualButtonPressed;
	}

	// Token: 0x060011D4 RID: 4564 RVA: 0x0004FBB1 File Offset: 0x0004DDB1
	public bool contextualButtonHover()
	{
		return this.contextualButton != null && this.contextualButton.getHover();
	}

	// Token: 0x060011D5 RID: 4565 RVA: 0x0004FBC8 File Offset: 0x0004DDC8
	public Vector2 getMouseRelativeToMap()
	{
		if (this.map == null)
		{
			return new Vector2(-1f, -1f);
		}
		return this.map.getRelativeMousePosition();
	}

	// Token: 0x060011D6 RID: 4566 RVA: 0x0004FBF0 File Offset: 0x0004DDF0
	public void setMainImage(string path)
	{
		if (path == "")
		{
			return;
		}
		if (this.mainImage == null)
		{
			this.mainImage = new GUIControl.MainImage();
		}
		if (path.ToUpper() == "CLEAR")
		{
			this.clearMainImage();
			return;
		}
		this.mainImage.setImage("SceneImages/" + path);
	}

	// Token: 0x060011D7 RID: 4567 RVA: 0x0004FC4D File Offset: 0x0004DE4D
	public void clearMainImage()
	{
		this.mainImage = null;
	}

	// Token: 0x060011D8 RID: 4568 RVA: 0x0004FC56 File Offset: 0x0004DE56
	public bool mainImageIsSet()
	{
		return this.mainImage != null;
	}

	// Token: 0x04000430 RID: 1072
	private SpriteRenderer spriteRenderer;

	// Token: 0x04000431 RID: 1073
	protected TextureTools.TextureData outputImage;

	// Token: 0x04000432 RID: 1074
	protected bool snapToOptions;

	// Token: 0x04000433 RID: 1075
	private static Texture2D outputTexture2D;

	// Token: 0x04000434 RID: 1076
	private UIElement handoffElement;

	// Token: 0x04000435 RID: 1077
	protected GUIControl.BackgroundImage backgroundImage;

	// Token: 0x04000436 RID: 1078
	protected GUIControl.MainImage mainImage;

	// Token: 0x04000437 RID: 1079
	protected UICallToAction callToAction;

	// Token: 0x04000438 RID: 1080
	protected ListButtonControl listButtons;

	// Token: 0x04000439 RID: 1081
	protected UIButtonControlVerticalText numericButtons;

	// Token: 0x0400043A RID: 1082
	protected UIImageCombatButtonControl combatButtonRow;

	// Token: 0x0400043B RID: 1083
	protected UIActionCounter actionCounter;

	// Token: 0x0400043C RID: 1084
	protected UIInitiativeList initiativeList;

	// Token: 0x0400043D RID: 1085
	protected UIAbilitySelectorGrid abilitySelectorGrid;

	// Token: 0x0400043E RID: 1086
	protected UIWeaponSwapControl weaponSwapControl;

	// Token: 0x0400043F RID: 1087
	protected UISheetTabButtonControl menuTab;

	// Token: 0x04000440 RID: 1088
	protected GUIControl.UIContextualButton contextualButton;

	// Token: 0x04000441 RID: 1089
	protected GUIControl.UIMainMenuButton mainMenuButton;

	// Token: 0x04000442 RID: 1090
	protected GUIControl.SceneDescription sceneDescription;

	// Token: 0x04000443 RID: 1091
	protected UITextBlock secondaryDescription;

	// Token: 0x04000444 RID: 1092
	protected UIPartyEffectSelector effectSelector;

	// Token: 0x04000445 RID: 1093
	protected GUIControl.PortraitContainer portraitContainer;

	// Token: 0x04000446 RID: 1094
	protected GUIControl.MediumHeader primaryHeader;

	// Token: 0x04000447 RID: 1095
	protected GUIControl.MediumHeader secondaryHeader;

	// Token: 0x04000448 RID: 1096
	protected UITextBlock bigHeader;

	// Token: 0x04000449 RID: 1097
	protected GUIControl.SheetComplex sheetComplex;

	// Token: 0x0400044A RID: 1098
	protected UIMap map;

	// Token: 0x0400044B RID: 1099
	protected BarkControl barkControl;

	// Token: 0x0400044C RID: 1100
	protected bool contextualButtonPressed;

	// Token: 0x0400044D RID: 1101
	private bool adjustVertical;

	// Token: 0x0400044E RID: 1102
	public const int MAP_X_ANCHOR = 12;

	// Token: 0x0400044F RID: 1103
	public const int MAP_Y_ANCHOR = 0;

	// Token: 0x02000262 RID: 610
	protected class SecondaryDescription : UITextBlock
	{
		// Token: 0x060019F8 RID: 6648 RVA: 0x00071642 File Offset: 0x0006F842
		public SecondaryDescription() : base(388, 84, 82, 73, C64Color.GrayLight)
		{
			this.foregroundColors.shadowMainColor = C64Color.SmallTextShadowColor;
			base.setEnforceHeight();
		}
	}

	// Token: 0x02000263 RID: 611
	protected class SecondaryDescriptionNostalgic : UITextBlock
	{
		// Token: 0x060019F9 RID: 6649 RVA: 0x00071670 File Offset: 0x0006F870
		public SecondaryDescriptionNostalgic() : base(392, 100, 82, 84, C64Color.GrayLight)
		{
			this.foregroundColors.shadowMainColor = C64Color.SmallTextShadowColor;
			base.setEnforceHeight();
		}
	}

	// Token: 0x02000264 RID: 612
	protected class PortraitContainerNostalgic : GUIControl.PortraitContainer
	{
		// Token: 0x060019FA RID: 6650 RVA: 0x000716A0 File Offset: 0x0006F8A0
		public PortraitContainerNostalgic()
		{
			this.setPosition(390, 264);
			this.column1.padding.right = 1;
			this.column2.padding.right = 1;
			this.column1.setPortraitsBottomPadding(-1);
			this.column2.setPortraitsBottomPadding(-1);
			this.alignElements();
		}
	}

	// Token: 0x02000265 RID: 613
	protected class PortraitContainerNormal : GUIControl.PortraitContainer
	{
	}

	// Token: 0x02000266 RID: 614
	protected abstract class PortraitContainer : UICanvasHorizontal
	{
		// Token: 0x060019FC RID: 6652 RVA: 0x0007170C File Offset: 0x0006F90C
		public PortraitContainer()
		{
			this.setPosition(385, 258);
			this.stretchHorizontal = true;
			this.stretchVertical = true;
			this.column1 = new GUIControl.PortraitContainer.PortraitColumn();
			this.column2 = new GUIControl.PortraitContainer.PortraitColumn();
			this.add(this.column1);
			this.add(this.column2);
			foreach (UIElement uielement in base.getElements())
			{
				GUIControl.PortraitContainer.PortraitColumn portraitColumn = (GUIControl.PortraitContainer.PortraitColumn)uielement;
				for (int i = 0; i < this.columnHight; i++)
				{
					portraitColumn.add(new GUIControl.PortraitContainer.Portrait());
				}
			}
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x000717E0 File Offset: 0x0006F9E0
		public void clearPortraitPressIndex()
		{
			this.portraitPressIndex = -1;
			this.portraitPressIndexRight = -1;
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x000717F0 File Offset: 0x0006F9F0
		public int getPortraitPressIndex()
		{
			return this.portraitPressIndex;
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x000717F8 File Offset: 0x0006F9F8
		public int getPortraitPressIndexRight()
		{
			return this.portraitPressIndexRight;
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x00071800 File Offset: 0x0006FA00
		public virtual void setPortraits(List<TextureTools.TextureData> textureList)
		{
			int num = 0;
			foreach (UIElement uielement in base.getElements())
			{
				foreach (UIElement uielement2 in ((GUIControl.PortraitContainer.PortraitColumn)uielement).getElements())
				{
					GUIControl.PortraitContainer.Portrait portrait = (GUIControl.PortraitContainer.Portrait)uielement2;
					if (num < textureList.Count)
					{
						portrait.backgroundTexture = textureList[num];
					}
					num++;
				}
			}
			this.alignElements();
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x000718B4 File Offset: 0x0006FAB4
		public void update()
		{
			this.clearPortraitPressIndex();
			int num = 0;
			foreach (UIElement uielement in base.getElements())
			{
				foreach (UIElement uielement2 in ((GUIControl.PortraitContainer.PortraitColumn)uielement).getElements())
				{
					GUIControl.PortraitContainer.Portrait portrait = (GUIControl.PortraitContainer.Portrait)uielement2;
					portrait.updateMouseInteraction();
					if (portrait.getLeftUp())
					{
						this.portraitPressIndex = num;
					}
					if (portrait.getRightUp())
					{
						this.portraitPressIndexRight = num;
					}
					num++;
				}
			}
		}

		// Token: 0x0400096E RID: 2414
		private int portraitPressIndex = -1;

		// Token: 0x0400096F RID: 2415
		private int portraitPressIndexRight = -1;

		// Token: 0x04000970 RID: 2416
		private int columnHight = 3;

		// Token: 0x04000971 RID: 2417
		protected GUIControl.PortraitContainer.PortraitColumn column1;

		// Token: 0x04000972 RID: 2418
		protected GUIControl.PortraitContainer.PortraitColumn column2;

		// Token: 0x020003D1 RID: 977
		private class Portrait : UIElement
		{
			// Token: 0x06001D5B RID: 7515 RVA: 0x0007BDF4 File Offset: 0x00079FF4
			public Portrait()
			{
				this.stretchHorizontal = true;
				this.stretchVertical = true;
				this.padding.bottom = 6;
				base.setAllowDoubleClick(false);
			}
		}

		// Token: 0x020003D2 RID: 978
		protected class PortraitColumn : UICanvasVertical
		{
			// Token: 0x06001D5C RID: 7516 RVA: 0x0007BE1D File Offset: 0x0007A01D
			public PortraitColumn()
			{
				this.stretchHorizontal = true;
				this.stretchVertical = true;
				this.padding.right = 4;
			}

			// Token: 0x06001D5D RID: 7517 RVA: 0x0007BE40 File Offset: 0x0007A040
			public void setPortraitsBottomPadding(int padding)
			{
				foreach (UIElement uielement in base.getElements())
				{
					((GUIControl.PortraitContainer.Portrait)uielement).padding.bottom = padding;
				}
			}
		}
	}

	// Token: 0x02000267 RID: 615
	protected abstract class SheetComplex : UICanvasVertical
	{
		// Token: 0x06001A03 RID: 6659 RVA: 0x0007197C File Offset: 0x0006FB7C
		protected virtual void initialize()
		{
			this.createBackground();
			this.setPosition();
			this.setToScrollIn();
			this.createTabRow();
			this.createHeader();
			this.createMainRow();
			this.createLeftColumn();
			this.createLeftScrollBar();
			this.createRightColumn();
			this.createRightScrollbar();
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x000719BC File Offset: 0x0006FBBC
		protected virtual void createHeader()
		{
			this.header = new GUIControl.MediumHeader(0, 0, this.getBaseWidth(), 21, C64Color.Yellow);
			this.header.alignments.horizontalAlignment = UIElement.Alignments.HorizontalAlignments.Left;
			this.header.padding.top = 3;
			this.header.padding.bottom = 2;
			this.header.padding.left = 8;
			this.add(this.header);
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x00071A33 File Offset: 0x0006FC33
		protected virtual void createTabRow()
		{
			this.tabRow = new UISheetTabButtonControl(0, 0, this.getBaseWidth(), 16);
			this.add(this.tabRow);
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x00071A58 File Offset: 0x0006FC58
		protected virtual void createMainRow()
		{
			this.mainRow = new UICanvasHorizontal(0, 0, this.getBaseWidth(), 0);
			this.mainRow.padding.left = 3;
			this.mainRow.backgroundColors.mainColor = C64Color.Violet;
			this.add(this.mainRow);
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x00071AAB File Offset: 0x0006FCAB
		public int updateLeftScrollbarAndReturnIndex(int listCount)
		{
			if (this.leftScrollBar == null)
			{
				return -1;
			}
			this.leftScrollBar.updateMouseInteraction();
			this.leftScrollBar.setIncrement(Mathf.RoundToInt((float)listCount));
			return Mathf.RoundToInt((float)listCount * this.leftScrollBar.getScrollDegree());
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x00071AE7 File Offset: 0x0006FCE7
		public virtual void scrollLeftBarDown()
		{
			if (this.leftScrollBar == null)
			{
				return;
			}
			this.leftScrollBar.scrollDownByIncrement();
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x00071AFD File Offset: 0x0006FCFD
		public virtual void scrollLeftBarUp()
		{
			if (this.leftScrollBar == null)
			{
				return;
			}
			this.leftScrollBar.scrollUpByIncrement();
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x00071B14 File Offset: 0x0006FD14
		public void updateRightScrollbar()
		{
			if (this.rightScrollBar == null)
			{
				return;
			}
			this.rightScrollBar.updateMouseInteraction();
			if (this.mainDescription == null)
			{
				return;
			}
			this.rightScrollBar.setIncrement(Mathf.RoundToInt((float)this.mainDescription.getMaxLines()));
			int scrollIndex = Mathf.RoundToInt((float)this.mainDescription.getMaxLines() * this.rightScrollBar.getScrollDegree());
			this.mainDescription.setScrollIndex(scrollIndex);
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x00071B84 File Offset: 0x0006FD84
		protected virtual void createBackground()
		{
			if (GlobalSettings.getFontSettings().getTextBackgroundColor() == 2)
			{
				this.backgroundTexture = TextureTools.loadTextureData("Images/GUIIcons/SheetListBackgroundBlack");
			}
			else if (GlobalSettings.getFontSettings().getTextBackgroundColor() == 1)
			{
				this.backgroundTexture = TextureTools.loadTextureData("Images/GUIIcons/SheetListBackgroundBrown");
			}
			else
			{
				this.backgroundTexture = TextureTools.loadTextureData("Images/GUIIcons/SheetListBackground");
			}
			this.setWidth(this.backgroundTexture.width);
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x00071BF0 File Offset: 0x0006FDF0
		protected virtual void createLeftScrollBar()
		{
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x00071BF2 File Offset: 0x0006FDF2
		public override void controllerScrollSidewaysLeft()
		{
			this.getControllerScrollableList().controllerScrollSidewaysLeft();
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x00071BFF File Offset: 0x0006FDFF
		public override void controllerScrollSidewaysRight()
		{
			this.getControllerScrollableList().controllerScrollSidewaysRight();
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x00071C0C File Offset: 0x0006FE0C
		public virtual UICanvas getControllerScrollableList()
		{
			return this.getListButtons();
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x00071C14 File Offset: 0x0006FE14
		protected virtual void createRightScrollbar()
		{
			this.rightScrollBar = new UIScrollbarStandard(13, this.getMainDescriptionHeight(), this.mainDescription);
			this.rightScrollBar.padding.top = -1;
			this.mainRow.add(this.rightScrollBar);
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x00071C51 File Offset: 0x0006FE51
		protected virtual int getTabWidth()
		{
			return 48;
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x00071C58 File Offset: 0x0006FE58
		protected virtual void createLeftColumn()
		{
			this.leftColumn = new UICanvasVertical(0, 0, this.getLeftColumnWidth(), this.mainRow.getHeight());
			this.leftColumn.padding.left = this.getLeftColumnPadding();
			this.mainRow.add(this.leftColumn);
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x00071CAC File Offset: 0x0006FEAC
		protected virtual void createRightColumn()
		{
			this.rightColumn = new UICanvasVertical(0, 0, this.getRightColumnWidth(), 0);
			this.rightColumn.padding.right = this.getRightColumnPadding() - 1;
			this.mainRow.add(this.rightColumn);
			this.mainDescription = new UITextBlock(0, 0, this.getRightColumnWidth(), this.getMainDescriptionHeight(), C64Color.SmallTextColor, FontContainer.getSmallDescriptionFont());
			this.mainDescription.foregroundColors.shadowMainColor = C64Color.SmallTextShadowColorDarkBackground;
			this.mainDescription.setEnforceHeight();
			this.mainDescription.setTabWidth(this.getTabWidth());
			this.mainDescription.padding.left = this.getRightColumnPadding();
			this.mainDescription.setTooltips(ToolTipControl.getRulesToolTips());
			this.mainDescription.setHighlightQuotes(true);
			this.mainDescription.padding.top = 3;
			this.rightColumn.add(this.mainDescription);
			this.numericButtons = new SheetButtonControl(0, 0, this.getRightColumnWidth() + 12, 35);
			this.numericButtons.setPaddingTop(35);
			this.numericButtons.setAlignment(new UIElement.Alignments(UIElement.Alignments.VerticalAlignments.Bottom, UIElement.Alignments.HorizontalAlignments.Center));
			this.rightColumn.add(this.numericButtons);
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x00071DE6 File Offset: 0x0006FFE6
		protected virtual int getMainDescriptionHeight()
		{
			return 154;
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x00071DED File Offset: 0x0006FFED
		protected virtual int getLeftColumnWidth()
		{
			return 125 - this.getLeftColumnPadding();
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x00071DF8 File Offset: 0x0006FFF8
		protected virtual int getRightColumnWidth()
		{
			return 218 - this.getRightColumnPadding();
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x00071E06 File Offset: 0x00070006
		protected virtual int getMainDescriptionWidth()
		{
			return this.getRightColumnWidth();
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x00071E0E File Offset: 0x0007000E
		protected virtual int getLeftColumnPadding()
		{
			return 5;
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x00071E11 File Offset: 0x00070011
		protected virtual int getRightColumnPadding()
		{
			return 6;
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x00071E14 File Offset: 0x00070014
		public void setHeader(string text)
		{
			this.header.setContent(text);
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x00071E22 File Offset: 0x00070022
		protected virtual void setToScrollIn()
		{
			base.setTargetDimensions(this.getX(), 15 + this.backgroundTexture.height, 0, 0);
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x00071E40 File Offset: 0x00070040
		protected virtual void setPosition()
		{
			this.setPosition(15, 0);
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x00071E4B File Offset: 0x0007004B
		protected virtual int getListButtonHeight()
		{
			return 188;
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x00071E52 File Offset: 0x00070052
		public void setDescription(string input)
		{
			if (!this.mainDescription.isContentEqual(input) && this.rightScrollBar != null)
			{
				this.rightScrollBar.resetScrollBar();
			}
			this.mainDescription.setContent(input);
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x00071E81 File Offset: 0x00070081
		public void setTabRow(List<string> textList, int currentIndex)
		{
			if (this.tabRow != null)
			{
				this.tabRow.setButtons(textList, currentIndex);
			}
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x00071E98 File Offset: 0x00070098
		public int getTabRowButtonPressIndex()
		{
			if (this.tabRow == null)
			{
				return -1;
			}
			return this.tabRow.getButtonPressIndexLeft();
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x00071EB0 File Offset: 0x000700B0
		public virtual ListButtonControl getListButtons()
		{
			if (this.listButtons == null)
			{
				this.listButtons = new ListButtonControl(0, 0, this.getLeftColumnWidth() - 6, this.getListButtonHeight(), 20);
				this.listButtons.setPaddingTop(10);
				this.listButtons.setTabWidth(80);
				this.leftColumn.add(this.listButtons);
			}
			return this.listButtons;
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x00071F13 File Offset: 0x00070113
		public UIButtonControlVerticalText getNumericButtons()
		{
			return this.numericButtons;
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x00071F1B File Offset: 0x0007011B
		public UISheetTabButtonControl getTabRow()
		{
			return this.tabRow;
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x00071F23 File Offset: 0x00070123
		public UICanvas getImageCanvas()
		{
			return this.image;
		}

		// Token: 0x04000973 RID: 2419
		protected GUIControl.MediumHeader header;

		// Token: 0x04000974 RID: 2420
		protected UISheetTabButtonControl tabRow;

		// Token: 0x04000975 RID: 2421
		protected UICanvasHorizontal mainRow;

		// Token: 0x04000976 RID: 2422
		protected UICanvasVertical leftColumn;

		// Token: 0x04000977 RID: 2423
		protected UICanvasVertical rightColumn;

		// Token: 0x04000978 RID: 2424
		protected UITextBlock mainDescription;

		// Token: 0x04000979 RID: 2425
		protected ListButtonControl listButtons;

		// Token: 0x0400097A RID: 2426
		protected SheetButtonControl numericButtons;

		// Token: 0x0400097B RID: 2427
		protected UICanvasVertical image;

		// Token: 0x0400097C RID: 2428
		protected UIScrollbar leftScrollBar;

		// Token: 0x0400097D RID: 2429
		protected UIScrollbar rightScrollBar;
	}

	// Token: 0x02000268 RID: 616
	protected class SheetComplexList : GUIControl.SheetComplex
	{
		// Token: 0x06001A25 RID: 6693 RVA: 0x00071F2B File Offset: 0x0007012B
		public SheetComplexList()
		{
			this.initialize();
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x00071F39 File Offset: 0x00070139
		protected override int getTabWidth()
		{
			return 60;
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x00071F3D File Offset: 0x0007013D
		protected override void createLeftScrollBar()
		{
			this.leftScrollBar = new UIScrollbarStandard(13, this.getListButtonHeight() + 5, this.getListButtons());
			this.leftScrollBar.padding.top = -1;
			this.mainRow.add(this.leftScrollBar);
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x00071F7C File Offset: 0x0007017C
		protected override int getRightColumnWidth()
		{
			return base.getRightColumnWidth() - 11;
		}
	}

	// Token: 0x02000269 RID: 617
	protected class SheetComplexSettings : GUIControl.SheetComplex
	{
		// Token: 0x06001A29 RID: 6697 RVA: 0x00071F87 File Offset: 0x00070187
		public SheetComplexSettings()
		{
			this.initialize();
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x00071F95 File Offset: 0x00070195
		protected override void createLeftScrollBar()
		{
			this.leftScrollBar = new UIScrollbarStandard(13, this.getListButtonHeight() + 5, this.leftColumn);
			this.leftScrollBar.padding.top = -1;
			this.mainRow.add(this.leftScrollBar);
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x00071FD4 File Offset: 0x000701D4
		protected override void setPosition()
		{
			this.setPosition(43, 0);
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x00071FDF File Offset: 0x000701DF
		protected override void setToScrollIn()
		{
			base.setTargetDimensions(this.getX(), 4 + this.backgroundTexture.height, 0, 0);
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x00071FFC File Offset: 0x000701FC
		public void setSliderControls(UITextSliderControl control)
		{
			this.leftColumn.clearElements();
			this.leftColumn.setHeight(this.getListButtonHeight());
			this.leftColumn.add(control);
			this.sliderControl = control;
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x0007202D File Offset: 0x0007022D
		public override UICanvas getControllerScrollableList()
		{
			return this.sliderControl;
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x00072035 File Offset: 0x00070235
		public void updateMainDescriptionFontData()
		{
			this.mainDescription.setLetterMainColor(C64Color.SmallTextColor);
			this.mainDescription.setLetterShadowColor(C64Color.SmallTextShadowColor);
			this.mainDescription.setFont(FontContainer.getSmallDescriptionFont());
			this.createBackground();
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x00072070 File Offset: 0x00070270
		protected override void createBackground()
		{
			if (GlobalSettings.getFontSettings().getTextBackgroundColor() == 2)
			{
				this.backgroundTexture = TextureTools.loadTextureData("Images/GUIIcons/SettingsBackgroundBlack");
			}
			else if (GlobalSettings.getFontSettings().getTextBackgroundColor() == 1)
			{
				this.backgroundTexture = TextureTools.loadTextureData("Images/GUIIcons/SettingsBackgroundBrown");
			}
			else
			{
				this.backgroundTexture = TextureTools.loadTextureData("Images/GUIIcons/SettingsBackgroundLight");
			}
			this.setWidth(this.backgroundTexture.width);
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x000720DC File Offset: 0x000702DC
		protected override int getLeftColumnWidth()
		{
			return 149 - this.getLeftColumnPadding();
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x000720EA File Offset: 0x000702EA
		protected override int getListButtonHeight()
		{
			return 215;
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x000720F4 File Offset: 0x000702F4
		public override ListButtonControl getListButtons()
		{
			if (this.listButtons == null)
			{
				this.listButtons = new ListButtonControl(0, 0, this.getLeftColumnWidth(), this.getListButtonHeight(), 25);
				this.listButtons.setPaddingTop(10);
				this.listButtons.setTabWidth(90);
				this.leftColumn.add(this.listButtons);
			}
			return this.listButtons;
		}

		// Token: 0x0400097E RID: 2430
		private UITextSliderControl sliderControl;
	}

	// Token: 0x0200026A RID: 618
	protected class ExtraButtonRowSheetComplex : GUIControl.SheetComplexList
	{
		// Token: 0x06001A34 RID: 6708 RVA: 0x00072158 File Offset: 0x00070358
		protected override void createHeader()
		{
			this.header = new GUIControl.MediumHeader(0, 0, this.getBaseWidth(), 21, C64Color.Yellow);
			this.header.alignments.horizontalAlignment = UIElement.Alignments.HorizontalAlignments.Left;
			this.header.padding.top = 3;
			this.header.padding.bottom = 2;
			this.header.padding.left = 8;
			this.add(this.header);
			this.horizontalMenuButtons = new UIHorizontalMenuButtons(this.getBaseWidth(), 21);
			this.add(this.horizontalMenuButtons);
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x000721EE File Offset: 0x000703EE
		public void setHorizontalMenuButtons(List<string> options, int currentIndex)
		{
			this.horizontalMenuButtons.setButtonText(options, currentIndex);
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x000721FD File Offset: 0x000703FD
		protected override int getListButtonHeight()
		{
			return base.getListButtonHeight() - 21;
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x00072208 File Offset: 0x00070408
		protected override void createBackground()
		{
			if (GlobalSettings.getFontSettings().getTextBackgroundColor() == 2)
			{
				this.backgroundTexture = TextureTools.loadTextureData("Images/GUIIcons/SheetListExtraRowBackgroundBlack");
			}
			else if (GlobalSettings.getFontSettings().getTextBackgroundColor() == 1)
			{
				this.backgroundTexture = TextureTools.loadTextureData("Images/GUIIcons/SheetListExtraRowBackgroundBrown");
			}
			else
			{
				this.backgroundTexture = TextureTools.loadTextureData("Images/GUIIcons/SheetListExtraRowBackground");
			}
			this.setWidth(this.backgroundTexture.width);
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x00072274 File Offset: 0x00070474
		protected override int getMainDescriptionHeight()
		{
			return 130;
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x0007227B File Offset: 0x0007047B
		internal int getHorizontalMenuButtonsIndex()
		{
			this.horizontalMenuButtons.update();
			return this.horizontalMenuButtons.getButtonPressIndexLeft();
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x00072293 File Offset: 0x00070493
		public override UICanvas getControllerScrollableList()
		{
			return this.horizontalMenuButtons;
		}

		// Token: 0x0400097F RID: 2431
		private UIHorizontalMenuButtons horizontalMenuButtons;
	}

	// Token: 0x0200026B RID: 619
	protected class ApperanceEditorSheetComplex : GUIControl.SheetComplexList
	{
		// Token: 0x06001A3C RID: 6716 RVA: 0x000722A3 File Offset: 0x000704A3
		public ApperanceEditorSheetComplex(UITextSliderControl textSliderControl)
		{
			this.textSliderControl = textSliderControl;
			this.leftColumn.add(textSliderControl);
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x000722C0 File Offset: 0x000704C0
		protected override void createBackground()
		{
			if (GlobalSettings.getFontSettings().getTextBackgroundColor() == 2)
			{
				this.backgroundTexture = TextureTools.loadTextureData("Images/GUIIcons/SheetApperanceEditorBackgroundBlack");
			}
			else if (GlobalSettings.getFontSettings().getTextBackgroundColor() == 1)
			{
				this.backgroundTexture = TextureTools.loadTextureData("Images/GUIIcons/SheetApperanceEditorBackgroundBrown");
			}
			else
			{
				this.backgroundTexture = TextureTools.loadTextureData("Images/GUIIcons/SheetApperanceEditorBackground");
			}
			this.setWidth(this.backgroundTexture.width);
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x0007232C File Offset: 0x0007052C
		protected override void createRightScrollbar()
		{
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x0007232E File Offset: 0x0007052E
		public override UICanvas getControllerScrollableList()
		{
			return this.textSliderControl;
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x00072338 File Offset: 0x00070538
		protected override void createRightColumn()
		{
			this.rightColumn = new UICanvasVertical(0, 0, this.getRightColumnWidth() - 20, 0);
			this.mainRow.add(this.rightColumn);
			this.apperancePortraits = new UICanvasHorizontal(0, 0, this.getRightColumnWidth() - 20, 66);
			this.apperancePortraits.alignments.horizontalAlignment = UIElement.Alignments.HorizontalAlignments.Center;
			this.rightColumn.add(this.apperancePortraits);
			this.modelPortrait = new UIImage(0, 0, 40, 40);
			this.modelPortrait.padding.top = 0;
			this.modelPortrait.padding.bottom = -5;
			this.modelPortrait.padding.right = 0;
			this.modelPortrait.padding.left = 5;
			this.apperancePortraits.add(this.modelPortrait);
			this.characterPortrait = new UIImage(0, 0, 40, 40);
			this.characterPortrait.padding.top = 15;
			this.characterPortrait.padding.left = 30;
			this.characterPortrait.padding.bottom = 8;
			this.apperancePortraits.add(this.characterPortrait);
			this.mainDescription = new UITextBlock(0, 0, this.getMainDescriptionWidth(), this.getMainDescriptionHeight(), C64Color.SmallTextColor, FontContainer.getSmallDescriptionFont());
			this.mainDescription.foregroundColors.shadowMainColor = C64Color.SmallTextShadowColor;
			this.mainDescription.setTabWidth(this.getTabWidth());
			this.mainDescription.padding.left = this.getRightColumnPadding();
			this.mainDescription.setHighlightQuotes(true);
			this.rightColumn.add(this.mainDescription);
			this.numericButtons = new SheetButtonControl(0, 0, this.getRightColumnWidth() + 12, 50);
			this.rightColumn.add(this.numericButtons);
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x0007250A File Offset: 0x0007070A
		protected override int getMainDescriptionHeight()
		{
			return 85;
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x0007250E File Offset: 0x0007070E
		public void setModelPortrait(TextureTools.TextureData texture)
		{
			this.modelPortrait.foregroundTexture = texture;
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x0007251C File Offset: 0x0007071C
		public void setCharacterPortrait(TextureTools.TextureData texture)
		{
			this.characterPortrait.foregroundTexture = texture;
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x0007252A File Offset: 0x0007072A
		protected override void createLeftScrollBar()
		{
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x0007252C File Offset: 0x0007072C
		protected override int getLeftColumnWidth()
		{
			return 138 - this.getLeftColumnPadding();
		}

		// Token: 0x04000980 RID: 2432
		private UITextSliderControl textSliderControl;

		// Token: 0x04000981 RID: 2433
		private UICanvasHorizontal apperancePortraits;

		// Token: 0x04000982 RID: 2434
		private UIImage modelPortrait;

		// Token: 0x04000983 RID: 2435
		private UIImage characterPortrait;
	}

	// Token: 0x0200026C RID: 620
	protected abstract class SheetComplexLeftHeavy : GUIControl.SheetComplex
	{
		// Token: 0x06001A46 RID: 6726 RVA: 0x0007253A File Offset: 0x0007073A
		protected override int getLeftColumnWidth()
		{
			return this.getWidth() / 3 * 2 - this.getLeftColumnPadding() - 17;
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x00072550 File Offset: 0x00070750
		protected override int getRightColumnWidth()
		{
			return this.getWidth() / 3 - this.getRightColumnPadding() + 5;
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x00072563 File Offset: 0x00070763
		protected override int getLeftColumnPadding()
		{
			return 0;
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x00072566 File Offset: 0x00070766
		protected override int getMainDescriptionWidth()
		{
			return this.getRightColumnWidth() + 7;
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x00072570 File Offset: 0x00070770
		protected override int getRightColumnPadding()
		{
			return 5;
		}
	}

	// Token: 0x0200026D RID: 621
	protected class SheetComplexInventory : GUIControl.SheetComplexLeftHeavy
	{
		// Token: 0x06001A4C RID: 6732 RVA: 0x0007257B File Offset: 0x0007077B
		public SheetComplexInventory(UIInventorySheetBase gridInventory)
		{
			this.grid = gridInventory;
			this.initialize();
			this.leftColumn.add(this.grid);
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x000725A1 File Offset: 0x000707A1
		protected override void createBackground()
		{
			this.backgroundTexture = TextureTools.loadTextureData(this.grid.getBackgroundPath());
			this.setWidth(this.backgroundTexture.width);
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x000725CA File Offset: 0x000707CA
		public override ListButtonControl getListButtons()
		{
			return null;
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x000725CD File Offset: 0x000707CD
		public override UICanvas getControllerScrollableList()
		{
			return this.grid;
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x000725D8 File Offset: 0x000707D8
		public override void scrollLeftBarDown()
		{
			UIScrollbar controllerSurfaceScrollbar = this.grid.getControllerSurfaceScrollbar();
			if (controllerSurfaceScrollbar == null)
			{
				return;
			}
			controllerSurfaceScrollbar.scrollDownByIncrement();
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x000725FC File Offset: 0x000707FC
		public override void scrollLeftBarUp()
		{
			UIScrollbar controllerSurfaceScrollbar = this.grid.getControllerSurfaceScrollbar();
			if (controllerSurfaceScrollbar == null)
			{
				return;
			}
			controllerSurfaceScrollbar.scrollUpByIncrement();
		}

		// Token: 0x04000984 RID: 2436
		protected UIInventorySheetBase grid;
	}

	// Token: 0x0200026E RID: 622
	protected class SheetComplexPartyManagement : GUIControl.SheetComplexLeftHeavy
	{
		// Token: 0x06001A52 RID: 6738 RVA: 0x0007261F File Offset: 0x0007081F
		public SheetComplexPartyManagement(UIPartyManagement partyManagement)
		{
			this.initialize();
			this.leftColumn.add(partyManagement);
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x00072639 File Offset: 0x00070839
		protected override void createBackground()
		{
			this.backgroundTexture = TextureTools.loadTextureData("Images/GUIIcons/SheetPartyManagement");
			this.setWidth(this.backgroundTexture.width);
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x0007265C File Offset: 0x0007085C
		protected override void createRightScrollbar()
		{
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x00072660 File Offset: 0x00070860
		protected override void createLeftColumn()
		{
			this.leftColumn = new UICanvasVertical(0, 0, this.getWidth(), this.mainRow.getHeight());
			this.leftColumn.padding.left = this.getLeftColumnPadding();
			this.mainRow.add(this.leftColumn);
			this.numericButtons = new SheetButtonControl(0, 0, this.getWidth(), 0);
			this.numericButtons.setPaddingTop(180);
			this.leftColumn.add(this.numericButtons);
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x000726E7 File Offset: 0x000708E7
		protected override void createRightColumn()
		{
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x000726E9 File Offset: 0x000708E9
		public bool isControllPressActivated()
		{
			return this.numericButtons.isControllPressActivated();
		}
	}

	// Token: 0x0200026F RID: 623
	protected class SheetComplexContainer : GUIControl.SheetComplexInventory
	{
		// Token: 0x06001A58 RID: 6744 RVA: 0x000726F6 File Offset: 0x000708F6
		public SheetComplexContainer(UIInventorySheetBase gridInventory) : base(gridInventory)
		{
		}
	}

	// Token: 0x02000270 RID: 624
	protected class SheetComplexCrafting : GUIControl.SheetComplexInventory
	{
		// Token: 0x06001A59 RID: 6745 RVA: 0x000726FF File Offset: 0x000708FF
		public SheetComplexCrafting(UIInventorySheetCrafting gridInventory) : base(gridInventory)
		{
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x00072708 File Offset: 0x00070908
		protected override void createTabRow()
		{
			this.add(new UIImage(0, 0, this.getBaseWidth(), 16));
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x0007271F File Offset: 0x0007091F
		public override ListButtonControl getListButtons()
		{
			return (this.grid as UIInventorySheetCrafting).getListButtons();
		}
	}

	// Token: 0x02000271 RID: 625
	protected class SheetComplexCamping : GUIControl.SheetComplexInventory
	{
		// Token: 0x06001A5C RID: 6748 RVA: 0x00072731 File Offset: 0x00070931
		public SheetComplexCamping(UIInventorySheetCampingFood gridInventory) : base(gridInventory)
		{
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x0007273A File Offset: 0x0007093A
		protected override void createTabRow()
		{
			this.add(new UIImage(0, 0, this.getBaseWidth(), 16));
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x00072751 File Offset: 0x00070951
		public override ListButtonControl getListButtons()
		{
			return (this.grid as UIInventorySheetCampingFood).getListButtons();
		}
	}

	// Token: 0x02000272 RID: 626
	protected class SheetComplexDoubleColumn : GUIControl.SheetComplexLeftHeavy
	{
		// Token: 0x06001A5F RID: 6751 RVA: 0x00072763 File Offset: 0x00070963
		public SheetComplexDoubleColumn(UIBaseCharacterSheet sheet, string backgroundPath)
		{
			this.sheet = sheet;
			this.backgroundPath = backgroundPath;
			this.initialize();
			this.leftColumn.add(sheet);
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x0007278C File Offset: 0x0007098C
		protected override void createBackground()
		{
			if (GlobalSettings.getFontSettings().getTextBackgroundColor() == 2)
			{
				this.backgroundPath += "Black";
			}
			else if (GlobalSettings.getFontSettings().getTextBackgroundColor() == 1)
			{
				this.backgroundPath += "Brown";
			}
			this.backgroundTexture = TextureTools.loadTextureData("Images/GUIIcons/" + this.backgroundPath);
			this.setWidth(this.backgroundTexture.width);
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x0007280D File Offset: 0x00070A0D
		public override UICanvas getControllerScrollableList()
		{
			return this.sheet;
		}

		// Token: 0x04000985 RID: 2437
		private UIBaseCharacterSheet sheet;

		// Token: 0x04000986 RID: 2438
		private string backgroundPath;
	}

	// Token: 0x02000273 RID: 627
	protected class SheetComplexFeatTree : GUIControl.SheetComplexLeftHeavy
	{
		// Token: 0x06001A62 RID: 6754 RVA: 0x00072815 File Offset: 0x00070A15
		public SheetComplexFeatTree(UIFeatTree featTree)
		{
			this.featTree = featTree;
			this.initialize();
			featTree.setWidth(this.leftColumn.getBaseWidth());
			featTree.alignElements();
			this.leftColumn.add(featTree);
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x00072850 File Offset: 0x00070A50
		protected override void createBackground()
		{
			if (GlobalSettings.getFontSettings().getTextBackgroundColor() == 2)
			{
				this.backgroundTexture = TextureTools.loadTextureData("Images/GUIIcons/SheetBigLeftColumnBlack");
			}
			else if (GlobalSettings.getFontSettings().getTextBackgroundColor() == 1)
			{
				this.backgroundTexture = TextureTools.loadTextureData("Images/GUIIcons/SheetBigLeftColumnBrown");
			}
			else
			{
				this.backgroundTexture = TextureTools.loadTextureData("Images/GUIIcons/SheetBigLeftColumn");
			}
			this.setWidth(this.backgroundTexture.width);
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x000728BC File Offset: 0x00070ABC
		public override UICanvas getControllerScrollableList()
		{
			return this.featTree;
		}

		// Token: 0x04000987 RID: 2439
		private UIFeatTree featTree;
	}

	// Token: 0x02000274 RID: 628
	protected class ImageContainer : UIElement
	{
		// Token: 0x06001A65 RID: 6757 RVA: 0x000728C4 File Offset: 0x00070AC4
		public void setImage(string path)
		{
			if (path == "")
			{
				return;
			}
			string text = this.basePath + path;
			if (text == this.currentPath)
			{
				return;
			}
			this.currentPath = text;
			this.backgroundTexture = TextureTools.loadTextureData(text);
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x0007290E File Offset: 0x00070B0E
		public void setImage(TextureTools.TextureData texture)
		{
			this.currentPath = "";
			this.backgroundTexture = texture;
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x00072922 File Offset: 0x00070B22
		public bool isImageSet()
		{
			return this.backgroundTexture != null;
		}

		// Token: 0x04000988 RID: 2440
		private string currentPath;

		// Token: 0x04000989 RID: 2441
		protected string basePath = "Images/";
	}

	// Token: 0x02000275 RID: 629
	protected class MainImage : GUIControl.ImageContainer
	{
		// Token: 0x06001A69 RID: 6761 RVA: 0x00072940 File Offset: 0x00070B40
		public MainImage()
		{
			this.setPosition(74, 248);
		}
	}

	// Token: 0x02000276 RID: 630
	protected class BackgroundImage : GUIControl.ImageContainer
	{
		// Token: 0x06001A6A RID: 6762 RVA: 0x00072955 File Offset: 0x00070B55
		public BackgroundImage()
		{
			this.basePath = "Images/Backgrounds/";
			this.setPosition(0, 270);
		}
	}

	// Token: 0x02000277 RID: 631
	protected class PrimaryHeaderNormal : GUIControl.MediumHeader
	{
		// Token: 0x06001A6B RID: 6763 RVA: 0x00072974 File Offset: 0x00070B74
		public PrimaryHeaderNormal() : base(122, 272, 145, 10, Color.clear, FontContainer.getMediumFontBlue())
		{
		}
	}

	// Token: 0x02000278 RID: 632
	protected class PrimaryHeaderNostalgic : GUIControl.MediumHeader
	{
		// Token: 0x06001A6C RID: 6764 RVA: 0x00072999 File Offset: 0x00070B99
		public PrimaryHeaderNostalgic() : base(122, 272, 145, 10, C64Color.White, FontContainer.getMediumFontBlue())
		{
		}
	}

	// Token: 0x02000279 RID: 633
	protected class MediumHeader : UITextBlock
	{
		// Token: 0x06001A6D RID: 6765 RVA: 0x000729B9 File Offset: 0x00070BB9
		public MediumHeader(int x, int y, int width, int height, Color32 color) : this(x, y, width, height, color, FontContainer.getMediumFont())
		{
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x000729CD File Offset: 0x00070BCD
		public MediumHeader(int x, int y, int width, int height, Color32 color, Font font) : base(x, y, width, height, color, font)
		{
			this.alignments.horizontalAlignment = UIElement.Alignments.HorizontalAlignments.Center;
			this.foregroundColors.shadowMainColor = C64Color.Black;
			this.padding.top = 3;
		}
	}

	// Token: 0x0200027A RID: 634
	protected class LargeHeader : UITextBlock
	{
		// Token: 0x06001A6F RID: 6767 RVA: 0x00072A08 File Offset: 0x00070C08
		public LargeHeader() : base(62, 141, 300, 10, Color.clear, FontContainer.getBigFont())
		{
			this.alignments.horizontalAlignment = UIElement.Alignments.HorizontalAlignments.Center;
			this.foregroundColors.outlineMainColor = C64Color.Black;
			this.foregroundColors.shadowMainColor = C64Color.Black;
			this.setReveal(false);
		}
	}

	// Token: 0x0200027B RID: 635
	protected class SceneDescription : UITextBlock
	{
		// Token: 0x06001A70 RID: 6768 RVA: 0x00072A6B File Offset: 0x00070C6B
		public SceneDescription() : base(15, 0, 100, 0, C64Color.SmallTextColor, FontContainer.getSmallDescriptionFont())
		{
			this.setTargetPosition(0);
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x00072A8C File Offset: 0x00070C8C
		public void setTargetPosition(int optionsHeight)
		{
			int num = optionsHeight + this.getHeight() + 20;
			if (num > 80)
			{
				base.setTargetDimensions(15, num, 100, 0);
				return;
			}
			base.setTargetDimensions(15, 80, 100, 0);
		}

		// Token: 0x0400098A RID: 2442
		private const int SCENE_DESCRIPTION_TARGET_Y = 80;
	}

	// Token: 0x0200027C RID: 636
	protected class UIContextualButton : UITextBlock
	{
		// Token: 0x06001A72 RID: 6770 RVA: 0x00072AC4 File Offset: 0x00070CC4
		public UIContextualButton() : base(133, 20, 100, 0, C64Color.Yellow, FontContainer.getMediumFont())
		{
			this.backgroundTexture = TextureTools.loadTextureData("Images/GUIIcons/ContextualButton");
			this.foregroundColors.hoverColor = C64Color.White;
			this.foregroundColors.leftClickedColor = C64Color.Cyan;
			this.foregroundColors.shadowMainColor = C64Color.Black;
			this.setWidth(this.backgroundTexture.width);
			this.padding.top = 2;
			this.setAlignment(new UIElement.Alignments(UIElement.Alignments.VerticalAlignments.Top, UIElement.Alignments.HorizontalAlignments.Center));
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x00072B55 File Offset: 0x00070D55
		public void update()
		{
			this.updateMouseInteraction();
		}
	}

	// Token: 0x0200027D RID: 637
	protected abstract class UIMainMenuButton : UIImage
	{
		// Token: 0x06001A74 RID: 6772 RVA: 0x00072B5D File Offset: 0x00070D5D
		public UIMainMenuButton(string path) : base(path)
		{
			this.foregroundColors.outlineHoverColor = C64Color.White;
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x00072B76 File Offset: 0x00070D76
		public void update()
		{
			this.updateMouseInteraction();
		}
	}

	// Token: 0x0200027E RID: 638
	protected class UIMainMenuButtonNormal : GUIControl.UIMainMenuButton
	{
		// Token: 0x06001A76 RID: 6774 RVA: 0x00072B7E File Offset: 0x00070D7E
		public UIMainMenuButtonNormal() : base("Images/GUIIcons/MainMenuButton")
		{
			this.setPosition(383, 9);
		}
	}

	// Token: 0x0200027F RID: 639
	protected class UIMainMenuButtonNostalgic : GUIControl.UIMainMenuButton
	{
		// Token: 0x06001A77 RID: 6775 RVA: 0x00072B98 File Offset: 0x00070D98
		public UIMainMenuButtonNostalgic() : base("Images/GUIIcons/MainMenuButtonNostalgia")
		{
			this.setPosition(389, 9);
		}
	}
}
