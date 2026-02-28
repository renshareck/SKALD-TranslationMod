using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// Token: 0x0200004B RID: 75
public class PopUpName : PopUpBase
{
	// Token: 0x06000859 RID: 2137 RVA: 0x00028B44 File Offset: 0x00026D44
	public PopUpName(Character character) : base("", new List<string>
	{
		"Continue"
	})
	{
		this.character = character;
		this.caution = string.Concat(new string[]
		{
			"Name must be between ",
			this.minLength.ToString(),
			" and ",
			this.maxLength.ToString(),
			" characters long and may only contain standard English letters (A-Z)."
		});
		base.setMainTextContent(this.prompt + this.caution);
		SkaldIO.openVirtualKeyboard(this.prompt, this.consoleInput);
	}

	// Token: 0x0600085A RID: 2138 RVA: 0x00028C1C File Offset: 0x00026E1C
	private bool isNameValid()
	{
		return this.consoleInput.Length <= this.maxLength && this.consoleInput.Length >= this.minLength && Regex.IsMatch(this.consoleInput, "^[a-zA-Z]+$");
	}

	// Token: 0x0600085B RID: 2139 RVA: 0x00028C68 File Offset: 0x00026E68
	private void updateCursor()
	{
		this.cursorBlinker.tick();
		if (this.cursorBlinker.isTimerZero())
		{
			this.cursorVisible = !this.cursorVisible;
		}
	}

	// Token: 0x0600085C RID: 2140 RVA: 0x00028C94 File Offset: 0x00026E94
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
			base.setTertiaryTextContent("            Name: " + this.consoleInput + "_");
		}
		else
		{
			base.setTertiaryTextContent("            Name: " + this.consoleInput);
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
			this.character.setName(this.consoleInput);
			this.handle(true);
			return;
		}
		PopUpControl.addPopUpOK(this.caution);
	}

	// Token: 0x040001C6 RID: 454
	private Character character;

	// Token: 0x040001C7 RID: 455
	private int minLength = 3;

	// Token: 0x040001C8 RID: 456
	private int maxLength = 10;

	// Token: 0x040001C9 RID: 457
	private string caution;

	// Token: 0x040001CA RID: 458
	private string prompt = "Type a name and press 'Continue'! ";

	// Token: 0x040001CB RID: 459
	private string consoleInput = "";

	// Token: 0x040001CC RID: 460
	private CountDownClock cursorBlinker = new CountDownClock(30, true);

	// Token: 0x040001CD RID: 461
	private bool cursorVisible = true;
}
