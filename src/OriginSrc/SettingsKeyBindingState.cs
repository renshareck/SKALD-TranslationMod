using System;
using System.Collections.Generic;
using SkaldEnums;
using UnityEngine;

// Token: 0x0200009F RID: 159
public class SettingsKeyBindingState : SettingsBaseState
{
	// Token: 0x06000AA0 RID: 2720 RVA: 0x000333C9 File Offset: 0x000315C9
	public SettingsKeyBindingState(DataControl dataControl) : base(dataControl)
	{
		this.list = SkaldIO.keyBindings;
		this.list.setMaxPageSize(30);
		this.setMainTextBuffer("\n\n\nTo reassign a key, select the binding, press ENTER, type in the new key and press ENTER again.\n\n");
		this.stateId = SkaldStates.SettingsKeyBindings;
	}

	// Token: 0x06000AA1 RID: 2721 RVA: 0x00033400 File Offset: 0x00031600
	public override void update()
	{
		if (SkaldIO.getPressedEscapeKey())
		{
			if (!this.assigning)
			{
				this.clearAndGoToOverland();
				return;
			}
			this.assigning = false;
			this.setMainTextBuffer("Aborted updating key.");
		}
		if (this.assigning)
		{
			if (SkaldIO.getLastKeyPressed() != KeyCode.Return && SkaldIO.getLastKeyPressed() != KeyCode.Mouse0 && SkaldIO.getLastKeyPressed() != KeyCode.Alpha3)
			{
				this.currentKey = SkaldIO.getLastKeyPressed();
			}
			this.setMainTextBuffer("Assigning key. Press ENTER or \"Re-Assign\" when done!\n\nEnter new key: " + C64Color.GREEN_LIGHT_TAG + this.currentKey.ToString() + "</color>");
		}
		else
		{
			base.update();
			if (this.numericInputIndex == 1)
			{
				SkaldIO.keyBindings.resetAll();
				this.setMainTextBuffer("All keys have been reset!");
			}
		}
		if (SkaldIO.getPressedEnterKey() || this.numericInputIndex == 2)
		{
			if (this.assigning)
			{
				this.setMainTextBuffer(SkaldIO.keyBindings.updateKey(this.currentKey));
			}
			this.assigning = !this.assigning;
		}
		this.numericInputIndex = -1;
		this.setGUIData();
	}

	// Token: 0x06000AA2 RID: 2722 RVA: 0x00033504 File Offset: 0x00031704
	protected override void setGUIData()
	{
		base.setGUIData();
		List<string> numericButtons = new List<string>
		{
			"Exit",
			"Reset",
			"Re-Assign"
		};
		this.guiControl.setNumericButtons(numericButtons);
		this.guiControl.setSheetHeader("Key Bindings");
		this.guiControl.revealAll();
	}

	// Token: 0x040002D2 RID: 722
	public bool assigning;

	// Token: 0x040002D3 RID: 723
	public KeyCode currentKey;
}
