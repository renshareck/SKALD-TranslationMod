using System;
using System.Collections.Generic;

// Token: 0x02000048 RID: 72
public class PopUpLevelUp : PopUpOK
{
	// Token: 0x0600084E RID: 2126 RVA: 0x0002873C File Offset: 0x0002693C
	public PopUpLevelUp(Character character) : base(character.levelUp())
	{
		this.character = character;
		this.uiElements.setMainTextContent(character.getName() + " reaches Level " + character.getLevel().ToString());
	}

	// Token: 0x0600084F RID: 2127 RVA: 0x00028785 File Offset: 0x00026985
	protected override void setPopUpUI(string description, List<string> buttonList)
	{
		this.uiElements = new PopUpLevelUp.PopupUILevelUp(description, buttonList);
	}

	// Token: 0x040001C3 RID: 451
	private Character character;

	// Token: 0x02000209 RID: 521
	protected class PopupUILevelUp : PopUpBase.PopUpUISimple
	{
		// Token: 0x06001824 RID: 6180 RVA: 0x0006A9E1 File Offset: 0x00068BE1
		public PopupUILevelUp(string description, List<string> buttonList) : base("", buttonList)
		{
			this.setSecondaryTextContent(description);
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x0006A9F6 File Offset: 0x00068BF6
		protected override void setPosition()
		{
			this.setPosition(90, 210);
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x0006AA05 File Offset: 0x00068C05
		protected override void setBackgroundAndSize()
		{
			base.setBackgroundAndSize();
			this.padding = new UIElement.Padding(15, this.sidePadding, 10, this.sidePadding);
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x0006AA28 File Offset: 0x00068C28
		protected override void setMainDescription(string description)
		{
			this.mainDescription = new UITextBlock(0, 0, this.getBaseWidth() - 2, 0, C64Color.Yellow, FontContainer.getMediumFont());
			this.mainDescription.alignments = new UIElement.Alignments(UIElement.Alignments.VerticalAlignments.Top, UIElement.Alignments.HorizontalAlignments.Center);
			this.mainDescription.foregroundColors.shadowMainColor = C64Color.Black;
			this.mainDescription.padding = this.textBoxPadding;
			this.mainDescription.padding.top = 0;
			this.mainDescription.setContent(description);
			this.add(this.mainDescription);
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x0006AAB8 File Offset: 0x00068CB8
		protected override void setSecondaryDescription(string description)
		{
			this.secondaryDescription = new UITextBlock(0, 0, this.getBaseWidth() - 2, 0, C64Color.White);
			this.secondaryDescription.foregroundColors.shadowMainColor = C64Color.Black;
			this.secondaryDescription.alignments = new UIElement.Alignments(UIElement.Alignments.VerticalAlignments.Top, UIElement.Alignments.HorizontalAlignments.Center);
			this.secondaryDescription.padding = this.textBoxPadding;
			this.secondaryDescription.setContent(description);
			this.add(this.secondaryDescription);
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x0006AB30 File Offset: 0x00068D30
		public override void setSecondaryTextContent(string text)
		{
			if (this.secondaryDescription == null)
			{
				return;
			}
			this.secondaryDescription.setContent(text);
			this.alignElements();
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x0006AB4D File Offset: 0x00068D4D
		protected override string getMainBackgroundPath()
		{
			return "Images/GUIIcons/TextBoxPopupLevelUp";
		}
	}
}
