using System;
using System.Text.RegularExpressions;
using SkaldEnums;

// Token: 0x02000085 RID: 133
public class CreditsState : BaseMenuState
{
	// Token: 0x06000A1E RID: 2590 RVA: 0x0003095D File Offset: 0x0002EB5D
	public CreditsState(DataControl dataControl) : base(dataControl)
	{
		this.instantiateCreditsList();
	}

	// Token: 0x06000A1F RID: 2591 RVA: 0x0003096C File Offset: 0x0002EB6C
	protected override void createGUI()
	{
		this.guiControl = new GUIControlCredits();
	}

	// Token: 0x06000A20 RID: 2592 RVA: 0x0003097C File Offset: 0x0002EB7C
	private void instantiateCreditsList()
	{
		this.creditsList = new SkaldDataList();
		if (MainControl.credits == null)
		{
			return;
		}
		foreach (string text in Regex.Split(MainControl.credits.text, "\n"))
		{
			this.creditsList.addEntry(text, text, text);
		}
	}

	// Token: 0x06000A21 RID: 2593 RVA: 0x000309D8 File Offset: 0x0002EBD8
	public override void update()
	{
		base.update();
		if (SkaldIO.getPressedEscapeKey())
		{
			this.setTargetState(SkaldStates.IntroMenu);
		}
		int num = this.guiControl.updateLeftScrollBarAndReturnIndex(this.creditsList.getCount());
		if (num != -1)
		{
			this.creditsList.setScrollIndex(num);
		}
		this.guiControl.setListButtons(this.creditsList.getScrolledStringList());
		this.setGUIData();
	}

	// Token: 0x040002A6 RID: 678
	private SkaldDataList creditsList;
}
