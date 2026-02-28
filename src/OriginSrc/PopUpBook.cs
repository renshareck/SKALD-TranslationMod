using System;
using System.Collections.Generic;

// Token: 0x02000042 RID: 66
public class PopUpBook : PopUpBase
{
	// Token: 0x06000815 RID: 2069 RVA: 0x00027E34 File Offset: 0x00026034
	public PopUpBook(ItemBook book) : base("", new List<string>
	{
		"1) ...",
		"2) ...",
		"3) ..."
	})
	{
		this.book = book;
		this.entries = book.getContent();
		while (this.entries.Count < 2)
		{
			this.entries.Add("");
		}
	}

	// Token: 0x06000816 RID: 2070 RVA: 0x00027EA5 File Offset: 0x000260A5
	protected override void setPopUpUI(string description, List<string> buttonList)
	{
		this.uiElements = new PopUpBase.PopUpUIBook(description, buttonList);
	}

	// Token: 0x06000817 RID: 2071 RVA: 0x00027EB4 File Offset: 0x000260B4
	public override void handle()
	{
		if (base.isHandled())
		{
			return;
		}
		if (this.entries == null)
		{
			return;
		}
		base.handle();
		base.setMainTextContent(this.entries[this.page]);
		base.setSecondaryTextContent(this.entries[this.page + 1]);
		base.setButtonsText(new List<string>
		{
			"1) " + (this.pagesRemainBehind() ? "Prev." : "..."),
			"2) Leave",
			"3) " + (this.pagesRemainAhead() ? "Next" : "...")
		});
		if (SkaldIO.getPressedNumericButton1() || base.getButtonPressIndex() == 0)
		{
			this.flipPageBack();
			return;
		}
		if (SkaldIO.getPressedEscapeKey() || SkaldIO.getPressedNumericButton2() || base.getButtonPressIndex() == 1)
		{
			this.handle(true);
			return;
		}
		if (SkaldIO.getPressedMainInteractKey() || SkaldIO.getPressedNumericButton3() || base.getButtonPressIndex() == 2)
		{
			this.flipPage();
		}
	}

	// Token: 0x06000818 RID: 2072 RVA: 0x00027FB9 File Offset: 0x000261B9
	private void flipPage()
	{
		if (!this.pagesRemainAhead())
		{
			return;
		}
		AudioControl.playPaperSound();
		this.page += 2;
	}

	// Token: 0x06000819 RID: 2073 RVA: 0x00027FD7 File Offset: 0x000261D7
	private void flipPageBack()
	{
		if (!this.pagesRemainBehind())
		{
			return;
		}
		AudioControl.playPaperSound();
		this.page -= 2;
	}

	// Token: 0x0600081A RID: 2074 RVA: 0x00027FF5 File Offset: 0x000261F5
	private bool pagesRemainAhead()
	{
		return this.page < this.entries.Count - 2;
	}

	// Token: 0x0600081B RID: 2075 RVA: 0x0002800C File Offset: 0x0002620C
	private bool pagesRemainBehind()
	{
		return this.page > 1;
	}

	// Token: 0x040001B2 RID: 434
	private ItemBook book;

	// Token: 0x040001B3 RID: 435
	private List<string> entries;

	// Token: 0x040001B4 RID: 436
	private int page;
}
