using System;

// Token: 0x02000139 RID: 313
public class UICharacterSheet : UIBaseCharacterSheet
{
	// Token: 0x0600123C RID: 4668 RVA: 0x00050D4C File Offset: 0x0004EF4C
	protected override void addEntries()
	{
		this.leftColumn.padding.top = -1;
		this.leftColumn.setWidth(90);
		this.alignElements();
		this.portrait = new UIImage();
		this.portrait.padding.top = 0;
		this.portrait.setHeight(48);
		this.portrait.padding.bottom = 0;
		this.portrait.padding.left = 20;
		this.leftColumn.add(this.portrait);
		this.entry1 = new UIBaseCharacterSheet.SheetEntry(78);
		this.entry1.setHeight(126);
		this.entry1.toggleCenterText();
		this.entry1.setTabWidth(58);
		this.entry1.setButtonTextShadowColor(C64Color.SmallTextShadowColorDarkBackground);
		this.entry1.padding.top = 13;
		this.entry1.padding.left = 2;
		this.entry1.stretchVertical = false;
		this.leftColumn.add(this.entry1);
		this.entry2 = new UIBaseCharacterSheet.SheetEntry(120);
		this.rightColumn.add(this.entry2);
	}

	// Token: 0x0600123D RID: 4669 RVA: 0x00050E7A File Offset: 0x0004F07A
	public void setPortrait(TextureTools.TextureData portraitImage)
	{
		this.portrait.foregroundTexture = portraitImage;
	}

	// Token: 0x04000465 RID: 1125
	private UIImage portrait;
}
