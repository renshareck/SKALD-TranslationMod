using System;

// Token: 0x02000164 RID: 356
public class UIScrollbarCredits : UIScrollbar
{
	// Token: 0x06001377 RID: 4983 RVA: 0x00055CCC File Offset: 0x00053ECC
	public UIScrollbarCredits(int x, int y, int width, int height) : base(x, y, width, height, null)
	{
	}

	// Token: 0x06001378 RID: 4984 RVA: 0x00055CDA File Offset: 0x00053EDA
	protected override bool canMouseWheelScroll()
	{
		return true;
	}
}
