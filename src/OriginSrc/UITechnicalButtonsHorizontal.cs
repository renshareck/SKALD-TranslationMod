using System;

// Token: 0x02000144 RID: 324
public class UITechnicalButtonsHorizontal : UITextButtonControlHorizontalBase
{
	// Token: 0x0600128C RID: 4748 RVA: 0x00051F6D File Offset: 0x0005016D
	public UITechnicalButtonsHorizontal(int x, int y, int width, int height, int buttonNumber) : base(x, y, width, height, buttonNumber)
	{
		this.setAlignment(new UIElement.Alignments(UIElement.Alignments.VerticalAlignments.Top, UIElement.Alignments.HorizontalAlignments.Left));
	}

	// Token: 0x0600128D RID: 4749 RVA: 0x00051F89 File Offset: 0x00050189
	protected override string getButtonBackgroundPath()
	{
		return "Images/GUIIcons/TechnicalButtonSmall";
	}
}
