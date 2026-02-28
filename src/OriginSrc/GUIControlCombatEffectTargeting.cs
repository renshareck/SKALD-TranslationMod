using System;

// Token: 0x0200012E RID: 302
public class GUIControlCombatEffectTargeting : ButtonRowClass
{
	// Token: 0x0600120A RID: 4618 RVA: 0x000500B4 File Offset: 0x0004E2B4
	public override void setBigHeader(string input)
	{
		this.bigHeader = new UITextBlock(188, 40, 0, 50, C64Color.White, FontContainer.getMediumFont());
		this.bigHeader.stretchHorizontal = true;
		this.bigHeader.setAlignment(new UIElement.Alignments(UIElement.Alignments.VerticalAlignments.Top, UIElement.Alignments.HorizontalAlignments.Center));
		this.bigHeader.setLetterShadowColor(C64Color.Black);
		this.bigHeader.setContent(input);
	}
}
