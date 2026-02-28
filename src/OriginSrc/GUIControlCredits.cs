using System;

// Token: 0x02000131 RID: 305
public class GUIControlCredits : GUIControl
{
	// Token: 0x06001210 RID: 4624 RVA: 0x000501D8 File Offset: 0x0004E3D8
	public GUIControlCredits()
	{
		this.sheetComplex = new GUIControlCredits.CreditsSheetComplex();
		this.listButtons = this.sheetComplex.getListButtons();
	}

	// Token: 0x06001211 RID: 4625 RVA: 0x000501FC File Offset: 0x0004E3FC
	public override void setMouseToClosestOptionAbove()
	{
		this.sheetComplex.scrollLeftBarDown();
		this.sheetComplex.scrollLeftBarDown();
		this.sheetComplex.scrollLeftBarDown();
		this.sheetComplex.scrollLeftBarDown();
	}

	// Token: 0x06001212 RID: 4626 RVA: 0x0005022A File Offset: 0x0004E42A
	public override void setMouseToClosestOptionBelow()
	{
		this.sheetComplex.scrollLeftBarUp();
		this.sheetComplex.scrollLeftBarUp();
		this.sheetComplex.scrollLeftBarUp();
		this.sheetComplex.scrollLeftBarUp();
	}

	// Token: 0x02000280 RID: 640
	protected class CreditsSheetComplex : GUIControl.SheetComplex
	{
		// Token: 0x06001A78 RID: 6776 RVA: 0x00072BB2 File Offset: 0x00070DB2
		public CreditsSheetComplex()
		{
			this.initialize();
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x00072BE0 File Offset: 0x00070DE0
		protected override void initialize()
		{
			this.listButtons = new CreditsButtons(this.x, this.y - 1, this.buttonWidth, 100, 14);
			this.listButtons.setPaddingTop(10);
			this.listButtons.setTabWidth(90);
			this.createLeftScrollBar();
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x00072C30 File Offset: 0x00070E30
		protected override void createLeftScrollBar()
		{
			this.leftScrollBar = new UIScrollbarCredits(this.x + this.buttonWidth + 4, this.y - 12, 16, 150);
			this.leftScrollBar.padding.top = -1;
			this.leftScrollBar.fixedPosition = true;
			this.add(this.leftScrollBar);
		}

		// Token: 0x0400098B RID: 2443
		private int x = 62;

		// Token: 0x0400098C RID: 2444
		private int y = 178;

		// Token: 0x0400098D RID: 2445
		private int buttonWidth = 170;
	}
}
