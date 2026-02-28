using System;

// Token: 0x02000084 RID: 132
public class BaseMenuState : StateBase
{
	// Token: 0x06000A19 RID: 2585 RVA: 0x000308AB File Offset: 0x0002EAAB
	protected BaseMenuState(DataControl dataControl) : base(dataControl)
	{
		AudioControl.playMusic("intro");
		IntroBackgroundMaker.togglePan();
		this.setGUIData();
		this.guiControl.clearCurrentSelectedOptionAndSnap();
	}

	// Token: 0x06000A1A RID: 2586 RVA: 0x000308D4 File Offset: 0x0002EAD4
	protected override void createGUI()
	{
		this.guiControl = new GUIControlMenu();
	}

	// Token: 0x06000A1B RID: 2587 RVA: 0x000308E1 File Offset: 0x0002EAE1
	protected override void setBackground()
	{
		this.guiControl.setBackground(IntroBackgroundMaker.drawAndGetOutput());
	}

	// Token: 0x06000A1C RID: 2588 RVA: 0x000308F4 File Offset: 0x0002EAF4
	public override void update()
	{
		if (SkaldIO.getOptionSelectionButtonDown())
		{
			this.guiControl.setMouseToClosestOptionBelow();
		}
		else if (SkaldIO.getOptionSelectionButtonUp())
		{
			this.guiControl.setMouseToClosestOptionAbove();
		}
		else if (SkaldIO.getOptionSelectionButtonRight())
		{
			this.guiControl.controllerScrollSidewaysRight();
		}
		else if (SkaldIO.getOptionSelectionButtonLeft())
		{
			this.guiControl.controllerScrollSidewaysLeft();
		}
		base.update();
	}

	// Token: 0x06000A1D RID: 2589 RVA: 0x00030955 File Offset: 0x0002EB55
	protected override void setGUIData()
	{
		this.setBackground();
	}
}
