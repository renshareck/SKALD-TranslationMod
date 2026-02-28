using System;

// Token: 0x0200011B RID: 283
public class GUIControlScene : NumericListClass
{
	// Token: 0x060011E8 RID: 4584 RVA: 0x0004FDD9 File Offset: 0x0004DFD9
	public GUIControlScene()
	{
		this.snapToOptions = true;
		this.sceneDescription = new GUIControl.SceneDescription();
	}

	// Token: 0x060011E9 RID: 4585 RVA: 0x0004FDF4 File Offset: 0x0004DFF4
	protected override void graduallyRevealText()
	{
		base.graduallyRevealText();
		if (PopUpControl.hasPopUp())
		{
			return;
		}
		if (SkaldIO.anyKeyPressed())
		{
			base.revealAll();
			return;
		}
		if (this.sceneDescription != null)
		{
			this.sceneDescription.reveal();
			if (this.sceneDescription.isRevealed() && this.numericButtons != null && !this.numericButtons.isRevealed())
			{
				base.revealNumericButtons();
				this.setMouseToSelectedOption();
			}
		}
	}

	// Token: 0x060011EA RID: 4586 RVA: 0x0004FE5E File Offset: 0x0004E05E
	protected override void setMouseToSelectedOption()
	{
		if (!this.snapToOptions)
		{
			return;
		}
		base.setMouseToUIElement(this.getControllerScrollableList(), 1, -8);
	}
}
