using System;

// Token: 0x02000121 RID: 289
public class GUIControlCharacterSheet : SheetClass
{
	// Token: 0x060011FA RID: 4602 RVA: 0x0004FFB8 File Offset: 0x0004E1B8
	public GUIControlCharacterSheet(UIBaseCharacterSheet sheet, string backgroundPath) : base(new GUIControl.SheetComplexDoubleColumn(sheet, backgroundPath))
	{
	}
}
