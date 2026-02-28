using System;

// Token: 0x02000166 RID: 358
public class UIScrollbarThatDoesNOTUpdateTargetCanvas : UIScrollbar
{
	// Token: 0x0600137B RID: 4987 RVA: 0x00055D97 File Offset: 0x00053F97
	public UIScrollbarThatDoesNOTUpdateTargetCanvas(int width, int height, UICanvas targetCanvas) : base(0, 0, width, height, targetCanvas)
	{
	}

	// Token: 0x0600137C RID: 4988 RVA: 0x00055DA4 File Offset: 0x00053FA4
	protected override bool canMouseWheelScroll()
	{
		return (this.targetCanvas != null && this.targetCanvas.getHover()) || base.canMouseWheelScroll();
	}
}
