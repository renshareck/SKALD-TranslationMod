using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000041 RID: 65
public abstract class PopUpBase
{
	// Token: 0x06000804 RID: 2052 RVA: 0x00027CF0 File Offset: 0x00025EF0
	protected PopUpBase(string description, List<string> buttonList)
	{
		this.setPopUpUI(description, buttonList);
	}

	// Token: 0x06000805 RID: 2053 RVA: 0x00027D07 File Offset: 0x00025F07
	protected PopUpBase()
	{
	}

	// Token: 0x06000806 RID: 2054 RVA: 0x00027D16 File Offset: 0x00025F16
	public virtual bool allowTooltips()
	{
		return false;
	}

	// Token: 0x06000807 RID: 2055 RVA: 0x00027D19 File Offset: 0x00025F19
	public bool allowsCharacterSwap()
	{
		return this.allowCharacterSwap;
	}

	// Token: 0x06000808 RID: 2056 RVA: 0x00027D21 File Offset: 0x00025F21
	protected virtual void setPopUpUI(string description, List<string> buttonList)
	{
		this.uiElements = new PopUpBase.PopUpUISimple(description, buttonList);
	}

	// Token: 0x06000809 RID: 2057 RVA: 0x00027D30 File Offset: 0x00025F30
	public void draw(TextureTools.TextureData target)
	{
		if (this.uiElements == null)
		{
			return;
		}
		this.uiElements.draw(target);
	}

	// Token: 0x0600080A RID: 2058 RVA: 0x00027D47 File Offset: 0x00025F47
	public void setMainTextContent(string text)
	{
		this.uiElements.setMainTextContent(text);
	}

	// Token: 0x0600080B RID: 2059 RVA: 0x00027D55 File Offset: 0x00025F55
	public void setSecondaryTextContent(string text)
	{
		this.uiElements.setSecondaryTextContent(text);
	}

	// Token: 0x0600080C RID: 2060 RVA: 0x00027D63 File Offset: 0x00025F63
	public void setTertiaryTextContent(string text)
	{
		this.uiElements.setTertiaryTextContent(text);
	}

	// Token: 0x0600080D RID: 2061 RVA: 0x00027D71 File Offset: 0x00025F71
	public void setButtonsText(List<string> buttonText)
	{
		this.uiElements.setButtonsText(buttonText);
	}

	// Token: 0x0600080E RID: 2062 RVA: 0x00027D7F File Offset: 0x00025F7F
	public virtual void handle()
	{
		if (this.handled)
		{
			return;
		}
		this.updateControllerScrolling();
		this.updateButtons();
	}

	// Token: 0x0600080F RID: 2063 RVA: 0x00027D98 File Offset: 0x00025F98
	protected virtual void updateControllerScrolling()
	{
		if (SkaldIO.getOptionSelectionButtonUp())
		{
			this.uiElements.setMouseToClosestButtonAbove();
			return;
		}
		if (SkaldIO.getOptionSelectionButtonDown())
		{
			this.uiElements.setMouseToClosestButtonBelow();
			return;
		}
		if (SkaldIO.getOptionSelectionButtonLeft())
		{
			this.uiElements.controllerScrollSidewaysLeft();
			return;
		}
		if (SkaldIO.getOptionSelectionButtonRight())
		{
			this.uiElements.controllerScrollSidewaysRight();
		}
	}

	// Token: 0x06000810 RID: 2064 RVA: 0x00027DF0 File Offset: 0x00025FF0
	protected virtual void handle(bool result)
	{
		if (this.handled)
		{
			return;
		}
		this.handled = true;
		this.result = result;
	}

	// Token: 0x06000811 RID: 2065 RVA: 0x00027E09 File Offset: 0x00026009
	public bool isHandled()
	{
		return this.handled;
	}

	// Token: 0x06000812 RID: 2066 RVA: 0x00027E11 File Offset: 0x00026011
	public bool resultWasPositive()
	{
		return this.result;
	}

	// Token: 0x06000813 RID: 2067 RVA: 0x00027E19 File Offset: 0x00026019
	protected int getButtonPressIndex()
	{
		return this.uiElements.getButtonPressIndex();
	}

	// Token: 0x06000814 RID: 2068 RVA: 0x00027E26 File Offset: 0x00026026
	protected void updateButtons()
	{
		this.uiElements.update();
	}

	// Token: 0x040001AE RID: 430
	private bool handled;

	// Token: 0x040001AF RID: 431
	private bool result = true;

	// Token: 0x040001B0 RID: 432
	protected bool allowCharacterSwap;

	// Token: 0x040001B1 RID: 433
	protected PopUpBase.PopUpUIBase uiElements;

	// Token: 0x02000201 RID: 513
	protected abstract class PopUpUIBase : UICanvasVertical
	{
		// Token: 0x060017E3 RID: 6115 RVA: 0x00069FB0 File Offset: 0x000681B0
		protected PopUpUIBase(string description, List<string> buttonList)
		{
			this.createUI(description, buttonList);
			this.buttons.clearCurrentSelectedButton();
			this.setMouseToSelectedButton();
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x00069FE8 File Offset: 0x000681E8
		protected PopUpUIBase()
		{
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x0006A008 File Offset: 0x00068208
		protected void setMouseToSelectedButton()
		{
			if (this.getControllerScrollableUICanvas() == null)
			{
				return;
			}
			if (!SkaldIO.isControllerConnected())
			{
				return;
			}
			UIElement currentControllerSelectedElement = this.getControllerScrollableUICanvas().getCurrentControllerSelectedElement();
			if (currentControllerSelectedElement == null)
			{
				return;
			}
			SkaldPoint2D position = currentControllerSelectedElement.getPosition();
			SkaldIO.setVirtualMousePosition(position.X + 8, position.Y - 8);
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x0006A052 File Offset: 0x00068252
		public virtual void setMouseToClosestButtonAbove()
		{
			if (this.getControllerScrollableUICanvas() == null)
			{
				return;
			}
			this.getControllerScrollableUICanvas().decrementCurrentSelectedButton();
			this.setMouseToSelectedButton();
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x0006A06E File Offset: 0x0006826E
		public virtual void setMouseToClosestButtonBelow()
		{
			if (this.getControllerScrollableUICanvas() == null)
			{
				return;
			}
			this.getControllerScrollableUICanvas().incrementCurrentSelectedButton();
			this.setMouseToSelectedButton();
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x0006A08A File Offset: 0x0006828A
		public override void controllerScrollSidewaysLeft()
		{
			this.setMouseToClosestButtonAbove();
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x0006A092 File Offset: 0x00068292
		public override void controllerScrollSidewaysRight()
		{
			this.setMouseToClosestButtonBelow();
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x0006A09A File Offset: 0x0006829A
		protected virtual UICanvas getControllerScrollableUICanvas()
		{
			if (this.currentControllerScrollableUICanvas == null)
			{
				this.currentControllerScrollableUICanvas = this.buttons;
			}
			return this.currentControllerScrollableUICanvas;
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x0006A0B6 File Offset: 0x000682B6
		protected void setControllerScrollableUICanvas(UICanvas c)
		{
			this.currentControllerScrollableUICanvas = c;
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x0006A0BF File Offset: 0x000682BF
		public virtual void update()
		{
			if (this.buttons != null)
			{
				this.buttons.update();
			}
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x0006A0D4 File Offset: 0x000682D4
		public virtual int getButtonPressIndex()
		{
			return this.buttons.getButtonPressIndexLeft();
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x0006A0E1 File Offset: 0x000682E1
		protected virtual void createUI(string description, List<string> buttonList)
		{
			this.setPosition();
			this.setBackgroundAndSize();
			this.setMainDescription(description);
			this.setSecondaryDescription("");
			this.setTertiaryDescription("");
			this.setButtons(buttonList);
			this.alignElements();
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x0006A11C File Offset: 0x0006831C
		protected virtual void setBackgroundAndSize()
		{
			this.backgroundTexture = TextureTools.loadTextureData(this.getMainBackgroundPath());
			this.setWidth(this.backgroundTexture.width - this.sidePadding * 2);
			this.setHeight(this.backgroundTexture.height);
			this.padding = new UIElement.Padding(20, this.sidePadding, 10, this.sidePadding);
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x0006A180 File Offset: 0x00068380
		protected virtual void setPosition()
		{
			if (MainControl.runningOnSteamDeck())
			{
				this.setPosition(90, 205);
				return;
			}
			this.setPosition(90, 175);
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x0006A1A4 File Offset: 0x000683A4
		protected virtual int getButtonOffset()
		{
			return 32;
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x0006A1A8 File Offset: 0x000683A8
		public virtual void setMainTextContent(string text)
		{
			this.mainDescription.setContent(text);
			this.alignElements();
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x0006A1BC File Offset: 0x000683BC
		protected virtual string getMainBackgroundPath()
		{
			return "Images/GUIIcons/TextBoxPopup";
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x0006A1C4 File Offset: 0x000683C4
		public virtual void setSecondaryTextContent(string text)
		{
			if (this.secondaryDescription == null)
			{
				return;
			}
			this.secondaryDescription.setContent(text);
			if (text != "")
			{
				this.secondaryDescription.backgroundTexture = TextureTools.loadTextureData("Images/GUIIcons/PopUpTextBackground");
				this.secondaryDescription.padding.top = 5;
			}
			this.alignElements();
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x0006A21F File Offset: 0x0006841F
		public virtual void setTertiaryTextContent(string text)
		{
			if (this.tertiaryDescription == null)
			{
				return;
			}
			this.tertiaryDescription.setContent(text);
			this.alignElements();
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x0006A23C File Offset: 0x0006843C
		protected virtual void setMainDescription(string description)
		{
			this.mainDescription = new UITextBlock(0, 0, this.getBaseWidth() - 2, 0, C64Color.White);
			this.mainDescription.alignments = new UIElement.Alignments(UIElement.Alignments.VerticalAlignments.Top, UIElement.Alignments.HorizontalAlignments.Center);
			this.mainDescription.foregroundColors.shadowMainColor = C64Color.Black;
			this.mainDescription.padding = this.textBoxPadding;
			this.mainDescription.setContent(description);
			this.add(this.mainDescription);
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x0006A2B4 File Offset: 0x000684B4
		protected virtual void setSecondaryDescription(string description)
		{
			this.secondaryDescription = new UITextBlock(0, 0, this.getBaseWidth() - 2, 0, C64Color.White);
			this.secondaryDescription.foregroundColors.shadowMainColor = C64Color.Black;
			this.secondaryDescription.padding = this.textBoxPadding;
			this.secondaryDescription.setContent(description);
			this.add(this.secondaryDescription);
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x0006A31C File Offset: 0x0006851C
		protected virtual void setTertiaryDescription(string description)
		{
			this.tertiaryDescription = new UITextBlock(0, 0, this.getBaseWidth() - 2, 0, C64Color.GrayLight);
			this.tertiaryDescription.foregroundColors.shadowMainColor = C64Color.Black;
			this.tertiaryDescription.setContent(description);
			this.tertiaryDescription.padding = this.textBoxPadding;
			this.add(this.tertiaryDescription);
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x0006A384 File Offset: 0x00068584
		public virtual void setButtons(List<string> buttonList)
		{
			this.buttons = new UIPopUpButtonControl(this.getX() + this.padding.left + 1, this.getY() - this.getHeight() + this.getButtonOffset(), this.getBaseWidth(), 32, buttonList.Count);
			this.setButtonsText(buttonList);
			this.buttons.fixedPosition = true;
			this.add(this.buttons);
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x0006A3F1 File Offset: 0x000685F1
		public virtual void setButtonsText(List<string> buttonList)
		{
			if (this.buttons != null)
			{
				this.buttons.setButtonText(buttonList);
			}
		}

		// Token: 0x040007DD RID: 2013
		protected UITextBlock mainDescription;

		// Token: 0x040007DE RID: 2014
		protected UITextBlock secondaryDescription;

		// Token: 0x040007DF RID: 2015
		protected UITextBlock tertiaryDescription;

		// Token: 0x040007E0 RID: 2016
		protected UIPopUpButtonControl buttons;

		// Token: 0x040007E1 RID: 2017
		private UICanvas currentControllerScrollableUICanvas;

		// Token: 0x040007E2 RID: 2018
		protected int sidePadding = 12;

		// Token: 0x040007E3 RID: 2019
		protected UIElement.Padding textBoxPadding = new UIElement.Padding(3, 0, 3, 3);
	}

	// Token: 0x02000202 RID: 514
	protected class PopUpUIBook : PopUpBase.PopUpUIBase
	{
		// Token: 0x060017FB RID: 6139 RVA: 0x0006A407 File Offset: 0x00068607
		public PopUpUIBook(string description, List<string> buttonList) : base(description, buttonList)
		{
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x0006A411 File Offset: 0x00068611
		protected override string getMainBackgroundPath()
		{
			return "Images/GUIIcons/PopUpBook";
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x0006A418 File Offset: 0x00068618
		protected override void setPosition()
		{
			this.setPosition(67, 225);
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x0006A427 File Offset: 0x00068627
		protected override void createUI(string description, List<string> buttonList)
		{
			this.pages = new UICanvasHorizontal(0, 0, this.getWidth(), this.getHeight());
			this.add(this.pages);
			base.createUI(description, buttonList);
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x0006A456 File Offset: 0x00068656
		protected override void setMainDescription(string description)
		{
			this.mainDescription = this.createPage();
			this.mainDescription.setContent(description);
			this.pages.add(this.mainDescription);
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x0006A481 File Offset: 0x00068681
		protected override void setSecondaryDescription(string description)
		{
			this.secondaryDescription = this.createPage();
			this.secondaryDescription.setContent(description);
			this.pages.add(this.secondaryDescription);
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x0006A4AC File Offset: 0x000686AC
		protected override int getButtonOffset()
		{
			return 32;
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x0006A4B0 File Offset: 0x000686B0
		protected override void setTertiaryDescription(string description)
		{
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x0006A4B2 File Offset: 0x000686B2
		public override void setSecondaryTextContent(string text)
		{
			if (this.secondaryDescription == null)
			{
				return;
			}
			this.secondaryDescription.setContent(text);
			this.alignElements();
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x0006A4D0 File Offset: 0x000686D0
		private UITextBlock createPage()
		{
			UITextBlock uitextBlock = new UITextBlock(0, 0, this.getBaseWidth() / 2 - 3, this.getBaseHeight(), C64Color.GrayLight);
			uitextBlock.foregroundColors.shadowMainColor = C64Color.SmallTextShadowColor;
			uitextBlock.padding.left = 5;
			uitextBlock.padding.right = 3;
			return uitextBlock;
		}

		// Token: 0x040007E4 RID: 2020
		private UICanvasHorizontal pages;
	}

	// Token: 0x02000203 RID: 515
	protected class PopUpUISimple : PopUpBase.PopUpUIBase
	{
		// Token: 0x06001805 RID: 6149 RVA: 0x0006A521 File Offset: 0x00068721
		public PopUpUISimple(string description, List<string> buttonList) : base(description, buttonList)
		{
		}
	}

	// Token: 0x02000204 RID: 516
	protected class PopUpUITestLockPick : PopUpBase.PopUpUITest
	{
		// Token: 0x06001806 RID: 6150 RVA: 0x0006A52B File Offset: 0x0006872B
		public PopUpUITestLockPick(string description, List<string> buttonList) : base(description, buttonList)
		{
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x0006A535 File Offset: 0x00068735
		protected override void setMainDescription(string description)
		{
			base.setMainDescription(description);
			this.mainDescription.padding.bottom = 6;
			this.addHealthBar();
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x0006A558 File Offset: 0x00068758
		private void addHealthBar()
		{
			this.healthBar = new UICanvasHorizontal(0, 0, this.getBaseWidth(), 3);
			this.healthBar.padding.left = this.healthBar.getWidth() / 3;
			this.healthBar.padding.bottom = 5;
			this.healthBar.padding.top = 2;
			TextureTools.TextureData foregroundTexture = new TextureTools.TextureData(this.healthBar.getWidth() / 3, this.healthBar.getHeight());
			this.healthBar.foregroundTexture = foregroundTexture;
			this.add(this.healthBar);
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x0006A5F0 File Offset: 0x000687F0
		public void adjustHealthBar(PropLockable prop)
		{
			if (prop == null)
			{
				return;
			}
			int currentHP = prop.getCurrentHP();
			int maxHP = prop.getMaxHP();
			this.healthBar.foregroundTexture.clearToBlack();
			TextureTools.drawSingleStatusBar(0, 1, this.healthBar.foregroundTexture, C64Color.Green, C64Color.Red, currentHP, maxHP);
		}

		// Token: 0x040007E5 RID: 2021
		private UICanvasHorizontal healthBar;
	}

	// Token: 0x02000205 RID: 517
	protected class PopUpUITest : PopUpBase.PopUpUISimple
	{
		// Token: 0x0600180A RID: 6154 RVA: 0x0006A640 File Offset: 0x00068840
		public PopUpUITest(string description, List<string> buttonList) : base(description, buttonList)
		{
			this.diceImages = new PopUpBase.PopUpUITest.DiceRoller(this.getX() + this.padding.left + 2, this.getY() - this.getHeight() + 19, this.getBaseWidth(), 20);
			this.add(this.diceImages);
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x0006A698 File Offset: 0x00068898
		public override void update()
		{
			base.update();
			this.diceImages.update();
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x0006A6AB File Offset: 0x000688AB
		protected override string getMainBackgroundPath()
		{
			return "Images/GUIIcons/TextBoxPopupTest";
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x0006A6B2 File Offset: 0x000688B2
		public void roll(SkaldTestBase test)
		{
			this.diceImages.roll(test);
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x0006A6C0 File Offset: 0x000688C0
		public bool isRolling()
		{
			return this.diceImages.isRolling();
		}

		// Token: 0x040007E6 RID: 2022
		private PopUpBase.PopUpUITest.DiceRoller diceImages;

		// Token: 0x0200032F RID: 815
		private class DiceRoller : UICanvasHorizontal
		{
			// Token: 0x06001CA5 RID: 7333 RVA: 0x0007B228 File Offset: 0x00079428
			public DiceRoller(int x, int y, int width, int height) : base(x, y, width, height)
			{
				this.fixedPosition = true;
				this.alignments = new UIElement.Alignments(UIElement.Alignments.VerticalAlignments.Top, UIElement.Alignments.HorizontalAlignments.Center);
				this.dice1 = new PopUpBase.PopUpUITest.DiceRoller.Dice();
				this.dice2 = new PopUpBase.PopUpUITest.DiceRoller.Dice();
				this.add(this.dice1);
				this.add(this.dice2);
			}

			// Token: 0x06001CA6 RID: 7334 RVA: 0x0007B282 File Offset: 0x00079482
			public void update()
			{
				this.dice1.update();
				this.dice2.update();
			}

			// Token: 0x06001CA7 RID: 7335 RVA: 0x0007B29C File Offset: 0x0007949C
			public void roll(SkaldTestBase test)
			{
				bool flash = test.wasSuccess();
				List<int> rawAbilityDiceRollList = test.getRawAbilityDiceRollList();
				if (rawAbilityDiceRollList == null || rawAbilityDiceRollList.Count < 2)
				{
					return;
				}
				this.dice1.roll(rawAbilityDiceRollList[0], flash);
				this.dice2.roll(rawAbilityDiceRollList[1], flash);
			}

			// Token: 0x06001CA8 RID: 7336 RVA: 0x0007B2EA File Offset: 0x000794EA
			public bool isRolling()
			{
				return this.dice1.isRolling() || this.dice2.isRolling();
			}

			// Token: 0x04000ABE RID: 2750
			private PopUpBase.PopUpUITest.DiceRoller.Dice dice1;

			// Token: 0x04000ABF RID: 2751
			private PopUpBase.PopUpUITest.DiceRoller.Dice dice2;

			// Token: 0x020003F3 RID: 1011
			private class Dice : UIElement
			{
				// Token: 0x06001DBC RID: 7612 RVA: 0x0007DA78 File Offset: 0x0007BC78
				public Dice()
				{
					this.stretchHorizontal = true;
					this.padding.right = 1;
					this.padding.left = 1;
					this.physicsSystem = new PhysicsSystem();
					this.animationControl = new AnimationStrip(new int[]
					{
						6,
						7,
						8,
						9
					}, 16f, true);
					this.flashFrameControl = new AnimationStrip(new int[]
					{
						0,
						1
					}, 8f, false);
					this.setTexture();
				}

				// Token: 0x06001DBD RID: 7613 RVA: 0x0007DB10 File Offset: 0x0007BD10
				public void update()
				{
					if (this.rollCountDown != null)
					{
						this.rollCountDown.tick();
					}
					if (this.flashCountDown != null && !this.isRolling())
					{
						this.flashCountDown.tick();
					}
					this.physicsSystem.update();
					this.setTexture();
				}

				// Token: 0x06001DBE RID: 7614 RVA: 0x0007DB5C File Offset: 0x0007BD5C
				public override void draw(TextureTools.TextureData targetTexture)
				{
					if (this.backgroundTexture != null)
					{
						TextureTools.applyOverlay(targetTexture, this.backgroundTexture, this.getX(), this.getY() - this.backgroundTexture.height + this.physicsSystem.getYPos());
					}
				}

				// Token: 0x06001DBF RID: 7615 RVA: 0x0007DB96 File Offset: 0x0007BD96
				public void roll(int target, bool flash)
				{
					this.diceTarget = target;
					this.startCountDown();
					this.physicsSystem.jump();
					AudioControl.playSound("Dice3");
					if (flash)
					{
						this.setFlash();
					}
				}

				// Token: 0x06001DC0 RID: 7616 RVA: 0x0007DBC3 File Offset: 0x0007BDC3
				public void setFlash()
				{
					this.flashCountDown = new CountDownClock(45, false);
				}

				// Token: 0x06001DC1 RID: 7617 RVA: 0x0007DBD4 File Offset: 0x0007BDD4
				public void setTexture()
				{
					int num = this.diceTarget - 1;
					string text = GlobalSettings.getGamePlaySettings().getDicePath();
					if (this.isRolling())
					{
						num = this.animationControl.getCurrentFrame();
						if (num == 9 || num == 13)
						{
							num = Random.Range(0, 6);
						}
					}
					else if (this.flashCountDown != null && !this.flashCountDown.isTimerZero() && this.flashFrameControl.getCurrentFrame() % 2 == 0)
					{
						text += "Flashing";
					}
					this.backgroundTexture = TextureTools.getSubImageTextureData(num, text, 4, 1);
				}

				// Token: 0x06001DC2 RID: 7618 RVA: 0x0007DC5C File Offset: 0x0007BE5C
				public bool isRolling()
				{
					return this.rollCountDown != null && !this.rollCountDown.isTimerZero();
				}

				// Token: 0x06001DC3 RID: 7619 RVA: 0x0007DC76 File Offset: 0x0007BE76
				private void startCountDown()
				{
					this.rollCountDown = new CountDownClock(Random.Range(this.minRollLength, this.maxRollLength), false);
				}

				// Token: 0x04000C97 RID: 3223
				private CountDownClock rollCountDown;

				// Token: 0x04000C98 RID: 3224
				private CountDownClock flashCountDown;

				// Token: 0x04000C99 RID: 3225
				private AnimationStrip animationControl;

				// Token: 0x04000C9A RID: 3226
				private AnimationStrip flashFrameControl;

				// Token: 0x04000C9B RID: 3227
				private int diceTarget = 6;

				// Token: 0x04000C9C RID: 3228
				private int minRollLength = 45;

				// Token: 0x04000C9D RID: 3229
				private int maxRollLength = 60;

				// Token: 0x04000C9E RID: 3230
				private PhysicsSystem physicsSystem;
			}
		}
	}

	// Token: 0x02000206 RID: 518
	protected class PopUpUISystemMenu : PopUpBase.PopUpUIBase
	{
		// Token: 0x0600180F RID: 6159 RVA: 0x0006A6CD File Offset: 0x000688CD
		public PopUpUISystemMenu(string description, List<string> buttonList) : base(description, buttonList)
		{
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x0006A6D7 File Offset: 0x000688D7
		protected override void setPosition()
		{
			this.setPosition(132, 175);
		}
	}

	// Token: 0x02000207 RID: 519
	protected class PopUpUISystemInventory : PopUpBase.PopUpUIBase
	{
		// Token: 0x06001811 RID: 6161 RVA: 0x0006A6E9 File Offset: 0x000688E9
		public PopUpUISystemInventory(string description, List<string> buttonList) : base(description, buttonList)
		{
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x0006A6F4 File Offset: 0x000688F4
		protected override void createUI(string description, List<string> buttonList)
		{
			this.setPosition();
			this.setBackgroundAndSize();
			this.padding.top = 15;
			this.setMainDescription(description);
			this.grid = new UIGridInventory(10, 2);
			this.grid.setBackgroundPaths("Images/GUIIcons/InventoryUI/MenuBarBoxGray", "Images/GUIIcons/InventoryUI/MenuBarBoxHoverGray", "Images/GUIIcons/InventoryUI/MenuBarBoxRightClickGray");
			this.grid.padding.left = 1;
			this.grid.padding.top = 5;
			this.grid.setMagicFlash(20);
			this.add(this.grid);
			this.setTertiaryDescription("...");
			this.setButtons(buttonList);
			this.alignElements();
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x0006A79C File Offset: 0x0006899C
		public int getGridButtonPressIndexLeft()
		{
			return this.grid.getButtonPressIndexLeft();
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x0006A7A9 File Offset: 0x000689A9
		public int getGridHoverIndex()
		{
			return this.grid.getHoverIndex();
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x0006A7B6 File Offset: 0x000689B6
		public int getGridButtonPressIndexRight()
		{
			return this.grid.getButtonPressIndexRight();
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x0006A7C3 File Offset: 0x000689C3
		public void update(Inventory inventory)
		{
			this.grid.update(inventory, MainControl.getDataControl().getMainCharacter());
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x0006A7DB File Offset: 0x000689DB
		protected override string getMainBackgroundPath()
		{
			return "Images/GUIIcons/TextBoxPopupLoot";
		}

		// Token: 0x040007E7 RID: 2023
		private UIGridInventory grid;
	}

	// Token: 0x02000208 RID: 520
	protected class PopUpUISpellSelector : PopUpBase.PopUpUIBase
	{
		// Token: 0x06001818 RID: 6168 RVA: 0x0006A7E2 File Offset: 0x000689E2
		public PopUpUISpellSelector(string description, List<string> buttonList, List<UIButtonControlBase.ButtonData> spellButtonData)
		{
			this.createUI(description, buttonList, spellButtonData);
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x0006A7F4 File Offset: 0x000689F4
		protected void createUI(string description, List<string> buttonList, List<UIButtonControlBase.ButtonData> spellButtonData)
		{
			this.alignments.horizontalAlignment = UIElement.Alignments.HorizontalAlignments.Center;
			this.setPosition();
			this.setBackgroundAndSize();
			this.padding.top = 12;
			this.setMainDescription(description);
			this.grid = new UINewSpellSelectorGrid(spellButtonData);
			this.grid.padding.left = 1;
			this.grid.padding.top = 4;
			this.add(this.grid);
			this.setTertiaryDescription("...");
			this.setButtons(buttonList);
			this.alignElements();
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x0006A880 File Offset: 0x00068A80
		protected override void setMainDescription(string description)
		{
			this.mainDescription = new UITextBlock(0, 0, this.getBaseWidth() - 2, 16, C64Color.White);
			this.mainDescription.stretchVertical = false;
			this.mainDescription.alignments = new UIElement.Alignments(UIElement.Alignments.VerticalAlignments.Top, UIElement.Alignments.HorizontalAlignments.Center);
			this.mainDescription.foregroundColors.shadowMainColor = C64Color.Black;
			this.mainDescription.padding = this.textBoxPadding;
			this.mainDescription.setContent(description);
			this.add(this.mainDescription);
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x0006A905 File Offset: 0x00068B05
		public int getGridButtonPressIndexLeft()
		{
			return this.grid.getLeftClickIndex();
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x0006A912 File Offset: 0x00068B12
		public int getGridHoverIndex()
		{
			return this.grid.getHoverIndex();
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x0006A91F File Offset: 0x00068B1F
		public int getGridButtonPressIndexRight()
		{
			return this.grid.getRightClickIndex();
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x0006A92C File Offset: 0x00068B2C
		public override void setMouseToClosestButtonAbove()
		{
			if (this.getControllerScrollableUICanvas().canControllerScrollUp())
			{
				this.getControllerScrollableUICanvas().decrementCurrentSelectedButton();
			}
			else
			{
				base.setControllerScrollableUICanvas(this.grid);
			}
			base.setMouseToSelectedButton();
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x0006A95A File Offset: 0x00068B5A
		public override void setMouseToClosestButtonBelow()
		{
			if (this.getControllerScrollableUICanvas().canControllerScrollDown())
			{
				this.getControllerScrollableUICanvas().incrementCurrentSelectedButton();
			}
			else
			{
				base.setControllerScrollableUICanvas(this.buttons);
			}
			base.setMouseToSelectedButton();
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x0006A988 File Offset: 0x00068B88
		public override void controllerScrollSidewaysLeft()
		{
			base.setControllerScrollableUICanvas(this.grid);
			this.getControllerScrollableUICanvas().controllerScrollSidewaysLeft();
			base.setMouseToSelectedButton();
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x0006A9A7 File Offset: 0x00068BA7
		public override void controllerScrollSidewaysRight()
		{
			base.setControllerScrollableUICanvas(this.grid);
			this.getControllerScrollableUICanvas().controllerScrollSidewaysRight();
			base.setMouseToSelectedButton();
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x0006A9C6 File Offset: 0x00068BC6
		public void update(List<UIButtonControlBase.ButtonData> buttonData)
		{
			base.update();
			this.grid.update(buttonData);
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x0006A9DA File Offset: 0x00068BDA
		protected override string getMainBackgroundPath()
		{
			return "Images/GUIIcons/TextBoxPopup";
		}

		// Token: 0x040007E8 RID: 2024
		private UINewSpellSelectorGrid grid;
	}
}
