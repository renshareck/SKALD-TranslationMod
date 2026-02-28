using System;

// Token: 0x02000118 RID: 280
public class ConsoleGUIControl : GUIControl
{
	// Token: 0x060011DC RID: 4572 RVA: 0x0004FC96 File Offset: 0x0004DE96
	public override void update()
	{
		this.bakeOutputTexture();
	}

	// Token: 0x060011DD RID: 4573 RVA: 0x0004FCA0 File Offset: 0x0004DEA0
	protected override void bakeOutputTexture()
	{
		if (this.backgroundImage != null && this.backgroundImage.isImageSet())
		{
			this.backgroundImage.draw(this.outputImage);
		}
		this.outputImage.bakeTexture2D(base.getOutputTexture2D());
		base.setSprite();
		this.outputImage.clear();
	}
}
