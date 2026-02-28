using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000050 RID: 80
public class PopUpRest : PopUpBase
{
	// Token: 0x06000867 RID: 2151 RVA: 0x00028F18 File Offset: 0x00027118
	public PopUpRest(CampActivityState state) : base("", new List<string>())
	{
		base.setSecondaryTextContent("");
		this.state = state;
		this.uiElements = new PopUpRest.PopUpUIResting("You begin resting", "z", state.getRestingPopUpImagePath(), "");
		this.restSummary = state.getRestSummary();
	}

	// Token: 0x06000868 RID: 2152 RVA: 0x00028F84 File Offset: 0x00027184
	public override void handle()
	{
		if (base.isHandled())
		{
			return;
		}
		base.handle();
		this.countDown.tick();
		if (this.countDown.isTimerZero())
		{
			this.seconds++;
			if (this.seconds == this.restSummary.Count)
			{
				this.handle(true);
			}
		}
		if (this.seconds < this.restSummary.Count)
		{
			base.setTertiaryTextContent(this.restSummary[this.seconds]);
		}
	}

	// Token: 0x06000869 RID: 2153 RVA: 0x0002900A File Offset: 0x0002720A
	protected override void handle(bool result)
	{
		if (result)
		{
			this.state.exit();
		}
		base.handle(true);
	}

	// Token: 0x040001D3 RID: 467
	private CountDownClock countDown = new CountDownClock(75, true);

	// Token: 0x040001D4 RID: 468
	private int seconds;

	// Token: 0x040001D5 RID: 469
	private CampActivityState state;

	// Token: 0x040001D6 RID: 470
	private List<string> restSummary;

	// Token: 0x0200020A RID: 522
	protected class PopUpUIResting : PopUpBase.PopUpUIBase
	{
		// Token: 0x0600182B RID: 6187 RVA: 0x0006AB54 File Offset: 0x00068D54
		public PopUpUIResting(string title, string description, string imagePath, string secondaryDescription)
		{
			this.setPosition();
			this.setBackgroundAndSize();
			this.setSecondaryDescription(imagePath);
			this.setTertiaryDescription(secondaryDescription);
			this.tertiaryDescription.alignments = new UIElement.Alignments(UIElement.Alignments.VerticalAlignments.Top, UIElement.Alignments.HorizontalAlignments.Center);
			this.alignElements();
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x0006AB90 File Offset: 0x00068D90
		protected virtual void setHeader(string description)
		{
			this.header = new UITextBlock(0, 0, this.getBaseWidth(), 15, C64Color.HeaderColor, FontContainer.getMediumFont());
			this.header.alignments = new UIElement.Alignments(UIElement.Alignments.VerticalAlignments.Top, UIElement.Alignments.HorizontalAlignments.Center);
			this.header.foregroundColors.shadowMainColor = C64Color.Black;
			this.header.padding.top = 2;
			this.header.padding.bottom = 2;
			this.header.setContent(description);
			this.add(this.header);
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x0006AC20 File Offset: 0x00068E20
		protected override void setMainDescription(string description)
		{
			this.mainDescription = new UITextBlock(0, 0, this.getBaseWidth() - 2, 0, Color.clear, FontContainer.getSmallDescriptionFont());
			this.mainDescription.alignments = new UIElement.Alignments(UIElement.Alignments.VerticalAlignments.Top, UIElement.Alignments.HorizontalAlignments.Center);
			this.mainDescription.foregroundColors.shadowMainColor = C64Color.Black;
			this.mainDescription.padding = this.textBoxPadding;
			this.mainDescription.padding.bottom = 6;
			this.mainDescription.setContent(description);
			this.add(this.mainDescription);
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x0006ACB3 File Offset: 0x00068EB3
		protected override string getMainBackgroundPath()
		{
			return "Images/GUIIcons/TextBoxPopUpRest";
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x0006ACBA File Offset: 0x00068EBA
		protected override void setPosition()
		{
			this.setPosition(45, 226);
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x0006ACC9 File Offset: 0x00068EC9
		protected override int getButtonOffset()
		{
			return 32;
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x0006ACCD File Offset: 0x00068ECD
		public override void setSecondaryTextContent(string text)
		{
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x0006ACD0 File Offset: 0x00068ED0
		protected override void setSecondaryDescription(string path)
		{
			if (path == "")
			{
				return;
			}
			UICanvasHorizontal uicanvasHorizontal = new UICanvasHorizontal(0, 0, this.getBaseWidth(), 0);
			uicanvasHorizontal.stretchVertical = true;
			uicanvasHorizontal.alignments = new UIElement.Alignments(UIElement.Alignments.VerticalAlignments.Top, UIElement.Alignments.HorizontalAlignments.Center);
			uicanvasHorizontal.add(new UIImage("Images/SceneImages/" + path));
			this.add(uicanvasHorizontal);
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x0006AD2C File Offset: 0x00068F2C
		protected override void setTertiaryDescription(string description)
		{
			this.tertiaryDescription = new UITextBlock(0, 0, this.getBaseWidth() - 2, 0, C64Color.GrayLight);
			this.tertiaryDescription.foregroundColors.shadowMainColor = C64Color.Black;
			this.tertiaryDescription.setContent(description);
			this.tertiaryDescription.padding = this.textBoxPadding;
			this.tertiaryDescription.padding.top = 8;
			this.add(this.tertiaryDescription);
		}

		// Token: 0x040007E9 RID: 2025
		private UITextBlock header;
	}
}
