using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200005F RID: 95
public class PopUpTutorial : PopUpBase
{
	// Token: 0x060008A8 RID: 2216 RVA: 0x0002A1C1 File Offset: 0x000283C1
	public PopUpTutorial(SKALDProjectData.EncylopediaContainer.TutorialContainer.TutorialCategory.Tutorial rawData)
	{
		this.rawData = rawData;
		this.uiElements = new PopUpTutorial.PopUpUITutorial(rawData);
	}

	// Token: 0x060008A9 RID: 2217 RVA: 0x0002A1DC File Offset: 0x000283DC
	public override void handle()
	{
		if (base.isHandled())
		{
			return;
		}
		base.handle();
		if (base.getButtonPressIndex() == 0)
		{
			this.handle(true);
			if (this.rawData.nextTutorial != "")
			{
				PopUpControl.addTutorialPopUp(this.rawData.nextTutorial);
				return;
			}
		}
		else
		{
			if (base.getButtonPressIndex() == 1)
			{
				this.handle(true);
				return;
			}
			if (base.getButtonPressIndex() == 2)
			{
				this.handle(true);
				GlobalSettings.getGamePlaySettings().setToNoTutorials();
				PopUpControl.addPopUpOK("Tutorials have been disabled. They can be turned back on in the Setting -> Gameplay menu.");
			}
		}
	}

	// Token: 0x040001F2 RID: 498
	private SKALDProjectData.EncylopediaContainer.TutorialContainer.TutorialCategory.Tutorial rawData;

	// Token: 0x0200020B RID: 523
	protected class PopUpUITutorial : PopUpBase.PopUpUIBase
	{
		// Token: 0x06001834 RID: 6196 RVA: 0x0006ADA4 File Offset: 0x00068FA4
		public PopUpUITutorial(SKALDProjectData.EncylopediaContainer.TutorialContainer.TutorialCategory.Tutorial rawData)
		{
			this.setPosition();
			this.setBackgroundAndSize();
			this.setHeader(rawData.title);
			if (SkaldIO.isControllerConnected())
			{
				if (rawData.primaryDescriptionController != "")
				{
					this.setMainDescription(rawData.primaryDescriptionController);
				}
				else
				{
					this.setMainDescription(rawData.description);
				}
			}
			else
			{
				this.setMainDescription(rawData.description);
			}
			this.setSecondaryDescription(rawData.imagePath);
			if (SkaldIO.isControllerConnected())
			{
				if (rawData.secondaryDescriptionController != "")
				{
					this.setTertiaryDescription(rawData.secondaryDescriptionController);
				}
				else
				{
					this.setTertiaryDescription(rawData.secondaryDescription);
				}
			}
			else
			{
				this.setTertiaryDescription(rawData.secondaryDescription);
			}
			this.tertiaryDescription.alignments = new UIElement.Alignments(UIElement.Alignments.VerticalAlignments.Top, UIElement.Alignments.HorizontalAlignments.Center);
			this.setButtons(new List<string>
			{
				"Continue",
				"Skip",
				"Disable"
			});
			this.buttons.clearCurrentSelectedButton();
			base.setMouseToSelectedButton();
			this.alignElements();
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x0006AEB4 File Offset: 0x000690B4
		protected virtual void setHeader(string description)
		{
			this.header = new UITextBlock(0, 0, this.getBaseWidth(), 15, C64Color.HeaderColor, FontContainer.getMediumFont());
			this.header.alignments = new UIElement.Alignments(UIElement.Alignments.VerticalAlignments.Top, UIElement.Alignments.HorizontalAlignments.Center);
			this.header.foregroundColors.shadowMainColor = C64Color.Black;
			this.header.padding.top = 4;
			this.header.setContent(description);
			this.add(this.header);
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x0006AF30 File Offset: 0x00069130
		protected override void setMainDescription(string description)
		{
			this.mainDescription = new UITextBlock(0, 0, this.getBaseWidth() - 2, 0, Color.clear, FontContainer.getSmallDescriptionFont());
			this.mainDescription.alignments = new UIElement.Alignments(UIElement.Alignments.VerticalAlignments.Top, UIElement.Alignments.HorizontalAlignments.Center);
			this.mainDescription.foregroundColors.shadowMainColor = C64Color.Black;
			this.mainDescription.padding = this.textBoxPadding;
			this.mainDescription.setContent(description);
			this.add(this.mainDescription);
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x0006AFB2 File Offset: 0x000691B2
		protected override string getMainBackgroundPath()
		{
			return "Images/GUIIcons/TextBoxPopUpTutorial";
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x0006AFB9 File Offset: 0x000691B9
		protected override void setPosition()
		{
			this.setPosition(45, 254);
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x0006AFC8 File Offset: 0x000691C8
		protected override void setSecondaryDescription(string path)
		{
			if (path == "")
			{
				return;
			}
			UICanvasHorizontal uicanvasHorizontal = new UICanvasHorizontal(0, 0, this.getBaseWidth(), 0);
			uicanvasHorizontal.stretchVertical = true;
			uicanvasHorizontal.alignments = new UIElement.Alignments(UIElement.Alignments.VerticalAlignments.Top, UIElement.Alignments.HorizontalAlignments.Center);
			uicanvasHorizontal.add(new UIImage("Images/Tutorials/" + path));
			this.add(uicanvasHorizontal);
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x0006B023 File Offset: 0x00069223
		protected override int getButtonOffset()
		{
			return 32;
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x0006B027 File Offset: 0x00069227
		public override void setSecondaryTextContent(string text)
		{
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x0006B02C File Offset: 0x0006922C
		protected override void setTertiaryDescription(string description)
		{
			this.tertiaryDescription = new UITextBlock(0, 0, this.getBaseWidth() - 2, 0, C64Color.White);
			this.tertiaryDescription.foregroundColors.shadowMainColor = C64Color.Black;
			this.tertiaryDescription.setContent(description);
			this.tertiaryDescription.padding = this.textBoxPadding;
			this.add(this.tertiaryDescription);
		}

		// Token: 0x040007EA RID: 2026
		private UITextBlock header;
	}
}
