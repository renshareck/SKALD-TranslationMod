using System;
using System.Collections.Generic;

// Token: 0x02000060 RID: 96
public class PopUpVisualStyle : PopUpYesNo
{
	// Token: 0x060008AA RID: 2218 RVA: 0x0002A264 File Offset: 0x00028464
	public PopUpVisualStyle(IntroMenuState introMenuState) : base("Choose a visual style. DEFAULT is recommended. This can also be customized in the SETTINGS menu.\n", new List<string>
	{
		"Default",
		"Continue",
		"Cancel"
	})
	{
		this.introMenuState = introMenuState;
	}

	// Token: 0x060008AB RID: 2219 RVA: 0x0002A29E File Offset: 0x0002849E
	protected override void handle(bool result)
	{
		if (result)
		{
			this.introMenuState.beginCharacterCreations();
		}
		base.handle(result);
	}

	// Token: 0x060008AC RID: 2220 RVA: 0x0002A2B5 File Offset: 0x000284B5
	protected override void setPopUpUI(string description, List<string> buttonList)
	{
		this.uiElements = new PopUpVisualStyle.PopUpUIVisualStyle(description, buttonList);
	}

	// Token: 0x060008AD RID: 2221 RVA: 0x0002A2C4 File Offset: 0x000284C4
	public override void handle()
	{
		if (base.isHandled())
		{
			return;
		}
		base.handle();
		if (SkaldIO.getPressedMainInteractKey() || SkaldIO.getPressedNumericButton1() || base.getButtonPressIndex() == 0)
		{
			GlobalSettings.getFontSettings().reset();
			this.handle(true);
		}
		if (SkaldIO.getPressedNumericButton2() || base.getButtonPressIndex() == 1)
		{
			this.handle(true);
			return;
		}
		if (SkaldIO.getPressedEscapeKey() || SkaldIO.getPressedNumericButton3() || base.getButtonPressIndex() == 2)
		{
			this.handle(false);
		}
	}

	// Token: 0x040001F3 RID: 499
	private IntroMenuState introMenuState;

	// Token: 0x0200020C RID: 524
	protected class PopUpUIVisualStyle : PopUpBase.PopUpUISimple
	{
		// Token: 0x0600183D RID: 6205 RVA: 0x0006B094 File Offset: 0x00069294
		public PopUpUIVisualStyle(string description, List<string> buttonList) : base("", buttonList)
		{
			this.setSecondaryTextContent(description);
			this.secondaryDescription.alignments = new UIElement.Alignments(UIElement.Alignments.VerticalAlignments.Top, UIElement.Alignments.HorizontalAlignments.Center);
			this.secondaryDescription.padding.bottom = 16;
			this.alignElements();
			base.setControllerScrollableUICanvas(this.textSliderControl);
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x0006B0F5 File Offset: 0x000692F5
		protected override void setPosition()
		{
			this.setPosition(90, 210);
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x0006B104 File Offset: 0x00069304
		protected override void setBackgroundAndSize()
		{
			base.setBackgroundAndSize();
			this.padding = new UIElement.Padding(15, this.sidePadding, 10, this.sidePadding);
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x0006B127 File Offset: 0x00069327
		public override void setMouseToClosestButtonAbove()
		{
			if (!this.getControllerScrollableUICanvas().canControllerScrollDown())
			{
				base.setControllerScrollableUICanvas(this.textSliderControl);
			}
			else
			{
				this.getControllerScrollableUICanvas().decrementCurrentSelectedButton();
			}
			base.setMouseToSelectedButton();
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x0006B155 File Offset: 0x00069355
		public override void setMouseToClosestButtonBelow()
		{
			if (!this.getControllerScrollableUICanvas().canControllerScrollUp())
			{
				base.setControllerScrollableUICanvas(this.buttons);
			}
			else
			{
				this.getControllerScrollableUICanvas().incrementCurrentSelectedButton();
			}
			base.setMouseToSelectedButton();
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x0006B183 File Offset: 0x00069383
		public override void controllerScrollSidewaysLeft()
		{
			if (this.getControllerScrollableUICanvas() == this.textSliderControl)
			{
				this.getControllerScrollableUICanvas().controllerScrollSidewaysLeft();
			}
			base.setMouseToClosestButtonAbove();
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x0006B1A4 File Offset: 0x000693A4
		public override void controllerScrollSidewaysRight()
		{
			if (this.getControllerScrollableUICanvas() == this.textSliderControl)
			{
				this.getControllerScrollableUICanvas().controllerScrollSidewaysRight();
			}
			base.setMouseToClosestButtonBelow();
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x0006B1C5 File Offset: 0x000693C5
		public override void setSecondaryTextContent(string text)
		{
			if (this.secondaryDescription == null)
			{
				return;
			}
			this.secondaryDescription.setContent(text);
			this.alignElements();
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x0006B1E4 File Offset: 0x000693E4
		protected override void setTertiaryDescription(string description)
		{
			this.textSliderControl.createAndReturnSliderButton(GlobalSettings.getFontSettings().getDescriptiveFontSetting());
			this.textSliderControl.createAndReturnSliderButton(GlobalSettings.getFontSettings().getDescriptiveFontColorSetting());
			this.textSliderControl.createAndReturnSliderButton(GlobalSettings.getFontSettings().getTextBackgroundColorSetting());
			this.textSliderControl.createAndReturnSliderButton(GlobalSettings.getDisplaySettings().getCRTSetting());
			this.textSliderControl.padding.left = 16;
			this.add(this.textSliderControl);
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x0006B267 File Offset: 0x00069467
		protected override string getMainBackgroundPath()
		{
			return "Images/GUIIcons/TextBoxPopupOptions";
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x0006B270 File Offset: 0x00069470
		public override void update()
		{
			this.textSliderControl.update();
			this.secondaryDescription.setLetterMainColor(C64Color.SmallTextColor);
			this.secondaryDescription.setLetterShadowColor(C64Color.SmallTextShadowColor);
			this.secondaryDescription.setFont(FontContainer.getSmallDescriptionFont());
			this.secondaryDescription.forceSetContent();
			if (GlobalSettings.getFontSettings().getTextBackgroundColor() == 2)
			{
				this.backgroundTexture = TextureTools.loadTextureData(this.getMainBackgroundPath() + "Black");
			}
			else if (GlobalSettings.getFontSettings().getTextBackgroundColor() == 1)
			{
				this.backgroundTexture = TextureTools.loadTextureData(this.getMainBackgroundPath() + "Brown");
			}
			else
			{
				this.backgroundTexture = TextureTools.loadTextureData(this.getMainBackgroundPath());
			}
			base.update();
		}

		// Token: 0x040007EB RID: 2027
		private UITextSliderControl textSliderControl = new UITextSliderControl();
	}
}
