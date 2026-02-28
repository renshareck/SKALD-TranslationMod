using System;

// Token: 0x02000097 RID: 151
public class SceneBaseState : StateBase
{
	// Token: 0x06000A85 RID: 2693 RVA: 0x00032CBB File Offset: 0x00030EBB
	public SceneBaseState(DataControl dataControll) : base(dataControll)
	{
		this.guiControl = new GUIControlScene();
		this.guiControl.setMainImage("BlackScreen");
		this.guiControl.toggleAdjustVertical();
		this.guiControl.resetCurrentSelectOption();
	}

	// Token: 0x06000A86 RID: 2694 RVA: 0x00032CF5 File Offset: 0x00030EF5
	protected void setNoImage()
	{
		this.noImage = true;
		this.guiControl.setMainImage("");
		this.guiControl.clearMainImage();
	}

	// Token: 0x06000A87 RID: 2695 RVA: 0x00032D19 File Offset: 0x00030F19
	protected void clearNoImage()
	{
		this.noImage = false;
	}

	// Token: 0x06000A88 RID: 2696 RVA: 0x00032D22 File Offset: 0x00030F22
	protected bool drawNoImage()
	{
		return this.noImage;
	}

	// Token: 0x06000A89 RID: 2697 RVA: 0x00032D2A File Offset: 0x00030F2A
	public override void update()
	{
		base.update();
		if (SkaldIO.getOptionSelectionButtonDown())
		{
			this.guiControl.setMouseToClosestOptionBelow();
			return;
		}
		if (SkaldIO.getOptionSelectionButtonUp())
		{
			this.guiControl.setMouseToClosestOptionAbove();
		}
	}

	// Token: 0x06000A8A RID: 2698 RVA: 0x00032D57 File Offset: 0x00030F57
	protected override void setGUIData()
	{
		base.setGUIData();
		this.drawPortraits();
		this.guiControl.setSecondaryDescription(this.dataControl.getDescription());
	}

	// Token: 0x040002CB RID: 715
	private bool noImage;
}
