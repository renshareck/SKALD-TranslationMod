using System;

// Token: 0x0200015C RID: 348
public class UIInventorySheetCharacter : UIInventorySheetBase
{
	// Token: 0x0600134D RID: 4941 RVA: 0x000551A8 File Offset: 0x000533A8
	protected override void initialize()
	{
		int num = 0;
		this.mainRow.add(new UIInventorySheetBase.TextLable("Items Worn"));
		this.itemInteractionGrid = new UIInventorySheetBase.ItemsWornUI();
		this.mainRow.add(this.itemInteractionGrid);
		UIInventorySheetBase.TextLable textLable = new UIInventorySheetBase.TextLable("Party Inventory");
		textLable.setPaddingLeft(num + 5);
		this.mainRow.add(textLable);
		this.mainInventoryGrid = new UIInventorySheetBase.UIGridCharacterInventorySegment(11, 6);
		this.mainInventoryGrid.setPaddingLeft(num);
		this.mainInventoryGrid.padding.bottom = 1;
		this.mainRow.add(this.mainInventoryGrid);
		this.filterButtons = new UIInventorySheetBase.FilterButtons();
		this.mainRow.add(this.filterButtons);
		this.goldWeightBlock = new UITextBlock(300, 0);
		this.goldWeightBlock.padding.left = 5;
		this.mainRow.add(this.goldWeightBlock);
	}
}
