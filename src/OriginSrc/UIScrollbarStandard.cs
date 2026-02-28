using System;

// Token: 0x02000165 RID: 357
public class UIScrollbarStandard : UIScrollbar
{
	// Token: 0x06001379 RID: 4985 RVA: 0x00055CDD File Offset: 0x00053EDD
	public UIScrollbarStandard(int width, int height, UICanvas targetCanvas) : base(0, 0, width, height, targetCanvas)
	{
	}

	// Token: 0x0600137A RID: 4986 RVA: 0x00055CEC File Offset: 0x00053EEC
	protected override bool canMouseWheelScroll()
	{
		if (this.testCanvas != null)
		{
			this.testCanvas.setDimensions(this.targetCanvas.getX(), this.targetCanvas.getY(), this.targetCanvas.getWidth(), this.targetCanvas.getHeight());
			this.testCanvas.updateMouseInteraction();
			if (this.testCanvas.getHover())
			{
				return true;
			}
		}
		else if (this.targetCanvas != null)
		{
			this.testCanvas = new UIImage(this.targetCanvas.getX(), this.targetCanvas.getY(), this.targetCanvas.getWidth(), this.targetCanvas.getHeight());
		}
		return base.canMouseWheelScroll();
	}
}
