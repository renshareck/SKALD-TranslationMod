using System;
using System.Collections.Generic;

// Token: 0x02000044 RID: 68
public class PopUpCreateSave : PopUpBase
{
	// Token: 0x06000842 RID: 2114 RVA: 0x000282CC File Offset: 0x000264CC
	public PopUpCreateSave(SaveMenuState callingState, string fileName) : base("", new List<string>
	{
		"Save",
		"Cancel"
	})
	{
		this.callingState = callingState;
		this.oldName = fileName;
		this.consoleInput = this.oldName;
		this.caution = string.Concat(new string[]
		{
			"\n\nName must be between ",
			this.minLength.ToString(),
			" and ",
			this.maxLength.ToString(),
			" characters long and must be a valid file-name."
		});
		base.setMainTextContent(this.prompt + this.caution);
		SkaldIO.openVirtualKeyboard(this.prompt, this.consoleInput);
	}

	// Token: 0x06000843 RID: 2115 RVA: 0x000283D2 File Offset: 0x000265D2
	private bool isNameValid()
	{
		return this.consoleInput.Length <= this.maxLength && this.consoleInput.Length >= this.minLength && SkaldSaveControl.isNameAValidFileName(this.consoleInput);
	}

	// Token: 0x06000844 RID: 2116 RVA: 0x0002840E File Offset: 0x0002660E
	private void updateCursor()
	{
		this.cursorBlinker.tick();
		if (this.cursorBlinker.isTimerZero())
		{
			this.cursorVisible = !this.cursorVisible;
		}
	}

	// Token: 0x06000845 RID: 2117 RVA: 0x00028438 File Offset: 0x00026638
	public override void handle()
	{
		if (base.isHandled())
		{
			return;
		}
		base.handle();
		this.updateCursor();
		if (this.cursorVisible)
		{
			base.setTertiaryTextContent(C64Color.CYAN_TAG + this.consoleInput + "_</color>");
		}
		else
		{
			base.setTertiaryTextContent(C64Color.CYAN_TAG + this.consoleInput + "</color>");
		}
		if (SkaldIO.getPressedEnterKey() || base.getButtonPressIndex() == 0)
		{
			if (this.isNameValid())
			{
				this.handle(true);
				return;
			}
			PopUpControl.addPopUpOK(this.caution);
			return;
		}
		else
		{
			if (SkaldIO.getPressedEscapeKey() || base.getButtonPressIndex() == 1)
			{
				this.handle(false);
				return;
			}
			if (SkaldIO.getInputString() == "\b")
			{
				if (this.consoleInput.Length != 0)
				{
					if (this.eraseAll)
					{
						this.eraseAll = false;
						this.consoleInput = "";
						return;
					}
					this.consoleInput = this.consoleInput.Substring(0, this.consoleInput.Length - 1);
					return;
				}
			}
			else if (this.consoleInput.Length < this.maxLength)
			{
				this.consoleInput += SkaldIO.getInputString();
			}
			return;
		}
	}

	// Token: 0x06000846 RID: 2118 RVA: 0x0002855F File Offset: 0x0002675F
	protected override void handle(bool result)
	{
		if (result)
		{
			this.callingState.createSave(this.consoleInput);
		}
		base.handle(true);
	}

	// Token: 0x040001B6 RID: 438
	private SaveMenuState callingState;

	// Token: 0x040001B7 RID: 439
	private int minLength = 3;

	// Token: 0x040001B8 RID: 440
	private int maxLength = 60;

	// Token: 0x040001B9 RID: 441
	private string caution;

	// Token: 0x040001BA RID: 442
	private string prompt = "Type a name and press 'Save'! ";

	// Token: 0x040001BB RID: 443
	private string consoleInput = "";

	// Token: 0x040001BC RID: 444
	private string oldName = "";

	// Token: 0x040001BD RID: 445
	private CountDownClock cursorBlinker = new CountDownClock(30, true);

	// Token: 0x040001BE RID: 446
	private bool cursorVisible = true;

	// Token: 0x040001BF RID: 447
	private bool eraseAll = true;
}
