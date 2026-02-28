using System;

// Token: 0x0200015F RID: 351
public class UIInventorySheetContainer : UIInventorySheetBase
{
	// Token: 0x06001364 RID: 4964 RVA: 0x000557EF File Offset: 0x000539EF
	protected virtual string getSecondaryInventoryTag()
	{
		return "Container";
	}

	// Token: 0x06001365 RID: 4965 RVA: 0x000557F8 File Offset: 0x000539F8
	protected override void initialize()
	{
		this.rows = new UICanvasHorizontal();
		this.rows.setHeight(164);
		this.firstRow = new UICanvasVertical();
		this.firstRow.padding.left = 0;
		this.firstRow.add(new UIInventorySheetBase.TextLable("Party Inventory"));
		this.mainInventoryGrid = new UIInventorySheetBase.UIGridCharacterInventorySegment(this.gridWidth, this.gridHeight);
		this.firstRow.add(this.mainInventoryGrid);
		this.firstRow.setWidth(106);
		this.firstRow.padding.right = 0;
		this.rows.add(this.firstRow);
		this.createSecondRow();
		this.mainRow.add(this.rows);
		this.filterButtons = new UIInventorySheetBase.FilterButtons();
		this.mainRow.add(this.filterButtons);
		this.goldWeightBlock = new UITextBlock(0, 0);
		this.goldWeightBlock.stretchHorizontal = true;
		this.goldWeightBlock.padding.left = 5;
		this.goldWeightBlock.padding.top = 6;
		this.mainRow.add(this.goldWeightBlock);
	}

	// Token: 0x06001366 RID: 4966 RVA: 0x00055927 File Offset: 0x00053B27
	public override string getBackgroundPath()
	{
		if (GlobalSettings.getFontSettings().getTextBackgroundColor() == 2)
		{
			return "Images/GUIIcons/SheetContainerBackgroundBlack";
		}
		if (GlobalSettings.getFontSettings().getTextBackgroundColor() == 1)
		{
			return "Images/GUIIcons/SheetContainerBackgroundBrown";
		}
		return "Images/GUIIcons/SheetContainerBackground";
	}

	// Token: 0x06001367 RID: 4967 RVA: 0x00055954 File Offset: 0x00053B54
	protected virtual void createSecondRow()
	{
		this.secondRow = new UICanvasVertical();
		this.secondRow.padding.left = 8;
		this.secondRow.add(new UIInventorySheetBase.TextLable(this.getSecondaryInventoryTag()));
		this.secondaryInventoryGrid = new UIInventorySheetBase.UIGridCharacterInventorySegment(this.gridWidth, this.gridHeight);
		this.secondaryInventoryGrid.padding.bottom = 0;
		this.secondRow.add(this.secondaryInventoryGrid);
		this.rows.add(this.secondRow);
	}

	// Token: 0x040004C4 RID: 1220
	protected UICanvasHorizontal rows;

	// Token: 0x040004C5 RID: 1221
	protected UICanvasVertical firstRow;

	// Token: 0x040004C6 RID: 1222
	protected UICanvasVertical secondRow;

	// Token: 0x040004C7 RID: 1223
	protected int gridWidth = 5;

	// Token: 0x040004C8 RID: 1224
	protected int gridHeight = 8;
}
