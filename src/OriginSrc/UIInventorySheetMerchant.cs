using System;
using System.Collections.Generic;

// Token: 0x02000160 RID: 352
public class UIInventorySheetMerchant : UIInventorySheetContainer
{
	// Token: 0x06001369 RID: 4969 RVA: 0x000559F4 File Offset: 0x00053BF4
	protected override void createSecondRow()
	{
		this.secondRow = new UICanvasVertical();
		this.secondRow.padding.left = 8;
		this.secondRow.add(new UIInventorySheetBase.TextLable(this.getSecondaryInventoryTag()));
		this.secondaryInventoryGrid = new UIInventorySheetBase.UIGridCharacterInventorySegment(this.gridWidth, this.gridHeight - 1);
		this.secondaryInventoryGrid.padding.bottom = 5;
		this.secondRow.add(this.secondaryInventoryGrid);
		this.serviceButtons = new UIInventorySheetMerchant.ServiceButtons();
		this.secondRow.add(this.serviceButtons);
		this.rows.add(this.secondRow);
	}

	// Token: 0x0600136A RID: 4970 RVA: 0x00055A9B File Offset: 0x00053C9B
	protected override string getSecondaryInventoryTag()
	{
		return "Merchant";
	}

	// Token: 0x0600136B RID: 4971 RVA: 0x00055AA2 File Offset: 0x00053CA2
	public void updateServiceButtons(List<string> services)
	{
		this.serviceButtons.update(services);
	}

	// Token: 0x0600136C RID: 4972 RVA: 0x00055AB0 File Offset: 0x00053CB0
	public int getServiceButtonPressIndex()
	{
		return this.serviceButtons.getButtonPressIndexLeft();
	}

	// Token: 0x040004C9 RID: 1225
	private UIInventorySheetMerchant.ServiceButtons serviceButtons;

	// Token: 0x020002A2 RID: 674
	private class ServiceButtons : UIButtonControlHorizontal
	{
		// Token: 0x06001AF2 RID: 6898 RVA: 0x000747E8 File Offset: 0x000729E8
		public ServiceButtons() : base(0, 0, 100, 20, 0)
		{
			base.getNestedCanvas().setAlignment(new UIElement.Alignments(UIElement.Alignments.VerticalAlignments.Top, UIElement.Alignments.HorizontalAlignments.Center));
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x00074809 File Offset: 0x00072A09
		protected override void populateButtons()
		{
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x0007480C File Offset: 0x00072A0C
		protected UIButtonControlBase.UITextButton createButton()
		{
			UIButtonControlBase.UITextButton uitextButton = new UIButtonControlBase.UITextButton(0, 0, 0, 0, C64Color.Yellow, FontContainer.getMediumFont());
			uitextButton.setHeight(20);
			uitextButton.stretchHorizontal = true;
			uitextButton.foregroundColors.hoverColor = C64Color.White;
			uitextButton.foregroundColors.leftClickedColor = C64Color.GrayLight;
			uitextButton.setContent("");
			uitextButton.padding.left = 2;
			uitextButton.padding.right = 2;
			return uitextButton;
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x00074880 File Offset: 0x00072A80
		public void update(List<string> services)
		{
			while (base.getButtonsList().Count < services.Count)
			{
				base.getButtonsList().Add(this.createButton());
			}
			int num = 0;
			foreach (string content in services)
			{
				UIButtonControlBase.UITextButton uitextButton = base.getButtonsList()[num] as UIButtonControlBase.UITextButton;
				uitextButton.setContent(content);
				uitextButton.setLetterShadowColor(C64Color.Black);
				num++;
			}
			this.update();
		}
	}
}
