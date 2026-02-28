using System;
using System.Collections.Generic;

// Token: 0x02000054 RID: 84
public class PopUpSaveRename : PopUpBase
{
	// Token: 0x0600086F RID: 2159 RVA: 0x000290BC File Offset: 0x000272BC
	public PopUpSaveRename(LoadSaveBaseMenuState callingState) : base("", new List<string>
	{
		"OK"
	})
	{
		this.callingState = callingState;
		this.oldName = callingState.getList().getCurrentObjectId();
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

	// Token: 0x06000870 RID: 2160 RVA: 0x000291BA File Offset: 0x000273BA
	private bool isNameValid()
	{
		return this.consoleInput.Length <= this.maxLength && this.consoleInput.Length >= this.minLength && SkaldSaveControl.isNameAValidFileName(this.consoleInput);
	}

	// Token: 0x06000871 RID: 2161 RVA: 0x000291F6 File Offset: 0x000273F6
	private void updateCursor()
	{
		this.cursorBlinker.tick();
		if (this.cursorBlinker.isTimerZero())
		{
			this.cursorVisible = !this.cursorVisible;
		}
	}

	// Token: 0x06000872 RID: 2162 RVA: 0x00029220 File Offset: 0x00027420
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
		if (!SkaldIO.getPressedEnterKey() && base.getButtonPressIndex() != 0)
		{
			if (SkaldIO.getInputString() == "\b")
			{
				if (this.consoleInput.Length != 0)
				{
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
		if (this.isNameValid())
		{
			SkaldSaveControl.renameSave(this.oldName, this.consoleInput);
			this.callingState.setList();
			this.handle(true);
			return;
		}
		PopUpControl.addPopUpOK(this.caution);
	}

	// Token: 0x040001D9 RID: 473
	private LoadSaveBaseMenuState callingState;

	// Token: 0x040001DA RID: 474
	private int minLength = 3;

	// Token: 0x040001DB RID: 475
	private int maxLength = 60;

	// Token: 0x040001DC RID: 476
	private string caution;

	// Token: 0x040001DD RID: 477
	private string prompt = "Type a name and press 'OK'! ";

	// Token: 0x040001DE RID: 478
	private string consoleInput = "";

	// Token: 0x040001DF RID: 479
	private string oldName = "";

	// Token: 0x040001E0 RID: 480
	private CountDownClock cursorBlinker = new CountDownClock(30, true);

	// Token: 0x040001E1 RID: 481
	private bool cursorVisible = true;
}
