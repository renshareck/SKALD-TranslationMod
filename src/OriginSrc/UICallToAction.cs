using System;

// Token: 0x0200014F RID: 335
public class UICallToAction : UICanvasVertical
{
	// Token: 0x060012B2 RID: 4786 RVA: 0x00052C08 File Offset: 0x00050E08
	public UICallToAction()
	{
		int num = 110;
		int y = 70;
		this.setPosition(480 + num, y);
		base.setTargetDimensions(480 - num, y);
		if (!MainControl.runningOnSteamDeck())
		{
			this.discordButton = new UICallToAction.DiscordButton();
			this.add(this.discordButton);
			this.moreContentButton = new UICallToAction.MoreContentButton();
			this.add(this.moreContentButton);
		}
		this.stretchVertical = true;
	}

	// Token: 0x060012B3 RID: 4787 RVA: 0x00052C7C File Offset: 0x00050E7C
	public void update()
	{
		foreach (UIElement uielement in base.getElements())
		{
			((UICallToAction.CallToArmsButton)uielement).update();
		}
	}

	// Token: 0x04000486 RID: 1158
	private UICallToAction.SteamButton steamButton;

	// Token: 0x04000487 RID: 1159
	private UICallToAction.DiscordButton discordButton;

	// Token: 0x04000488 RID: 1160
	private UICallToAction.MoreContentButton moreContentButton;

	// Token: 0x0200028F RID: 655
	private abstract class CallToArmsButton : UIImage
	{
		// Token: 0x06001AB0 RID: 6832 RVA: 0x00073691 File Offset: 0x00071891
		protected CallToArmsButton(string imagePath) : base(imagePath)
		{
			this.backgroundColors.mainColor = C64Color.Black;
			this.foregroundColors.hoverColor = C64Color.Yellow;
			this.foregroundColors.leftClickedColor = C64Color.GrayLight;
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x000736CA File Offset: 0x000718CA
		public virtual void update()
		{
			this.updateMouseInteraction();
		}
	}

	// Token: 0x02000290 RID: 656
	private class SteamButton : UICallToAction.CallToArmsButton
	{
		// Token: 0x06001AB2 RID: 6834 RVA: 0x000736D2 File Offset: 0x000718D2
		public SteamButton() : base("Images/GUIIcons/WishlistOnSteam")
		{
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x000736DF File Offset: 0x000718DF
		public override void update()
		{
			base.update();
			if (this.leftUp)
			{
				MainControl.getDataControl().openSteam();
			}
		}
	}

	// Token: 0x02000291 RID: 657
	private class DiscordButton : UICallToAction.CallToArmsButton
	{
		// Token: 0x06001AB4 RID: 6836 RVA: 0x000736F9 File Offset: 0x000718F9
		public DiscordButton() : base("Images/GUIIcons/JoinUsOnDiscord")
		{
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x00073706 File Offset: 0x00071906
		public override void update()
		{
			base.update();
			if (this.leftUp)
			{
				MainControl.getDataControl().openDiscord();
			}
		}
	}

	// Token: 0x02000292 RID: 658
	private class MoreContentButton : UICallToAction.CallToArmsButton
	{
		// Token: 0x06001AB6 RID: 6838 RVA: 0x00073720 File Offset: 0x00071920
		public MoreContentButton() : base("Images/GUIIcons/MoreContent")
		{
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x0007372D File Offset: 0x0007192D
		public override void update()
		{
			base.update();
			if (this.leftUp)
			{
				MainControl.getDataControl().openModIO();
			}
		}
	}
}
