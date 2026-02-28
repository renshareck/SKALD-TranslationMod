using System;

// Token: 0x02000115 RID: 277
public class Font
{
	// Token: 0x0600118E RID: 4494 RVA: 0x0004EB74 File Offset: 0x0004CD74
	public Font(SKALDProjectData.UIContainers.FontContainer.Font fontData)
	{
		this.fontPath = fontData.modelPath;
		this.letterSpacing = fontData.letterSpacing;
		this.wordSpacing = fontData.wordSpacing;
		this.wordHeight = fontData.wordHeight;
	}

	// Token: 0x0600118F RID: 4495 RVA: 0x0004EBD7 File Offset: 0x0004CDD7
	public string getModelPath()
	{
		return this.fontBasePath + this.fontPath;
	}

	// Token: 0x0400042B RID: 1067
	public string fontBasePath = "Images/CustomFonts/";

	// Token: 0x0400042C RID: 1068
	public string fontPath;

	// Token: 0x0400042D RID: 1069
	public int letterSpacing = 1;

	// Token: 0x0400042E RID: 1070
	public int wordSpacing = 3;

	// Token: 0x0400042F RID: 1071
	public int wordHeight = 7;
}
