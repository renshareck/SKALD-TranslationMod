using System;

// Token: 0x02000120 RID: 288
public class GUIControlApperanceEditorSheet : SheetClass
{
	// Token: 0x060011F8 RID: 4600 RVA: 0x0004FF90 File Offset: 0x0004E190
	public GUIControlApperanceEditorSheet(UITextSliderControl textSliderControl) : base(new GUIControl.ApperanceEditorSheetComplex(textSliderControl))
	{
	}

	// Token: 0x060011F9 RID: 4601 RVA: 0x0004FF9E File Offset: 0x0004E19E
	public void setModelAndCharacterPortrait(TextureTools.TextureData modelTexture, TextureTools.TextureData characterTexture)
	{
		GUIControl.ApperanceEditorSheetComplex apperanceEditorSheetComplex = this.sheetComplex as GUIControl.ApperanceEditorSheetComplex;
		apperanceEditorSheetComplex.setModelPortrait(modelTexture);
		apperanceEditorSheetComplex.setCharacterPortrait(characterTexture);
	}
}
