using System;

// Token: 0x0200013F RID: 319
public abstract class UIButtonControlHorizontal : UIButtonControlBase
{
	// Token: 0x0600127C RID: 4732 RVA: 0x00051A8A File Offset: 0x0004FC8A
	public UIButtonControlHorizontal(int x, int y, int width, int height, int buttonNumber) : base(x, y, width, height, buttonNumber)
	{
		this.add(new UICanvasHorizontal(x, y, width, height));
		this.populateButtons();
	}

	// Token: 0x0600127D RID: 4733
	protected abstract override void populateButtons();
}
