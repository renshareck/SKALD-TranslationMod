using System;
using System.Collections.Generic;

// Token: 0x02000049 RID: 73
public class PopUpLock : PopUpTestBase
{
	// Token: 0x06000850 RID: 2128 RVA: 0x00028794 File Offset: 0x00026994
	public PopUpLock(PropLockable propLockable) : base(propLockable.getDescription(), new List<string>
	{
		"1) Pick",
		"2) Force",
		"3) Leave"
	})
	{
		this.propLockable = propLockable;
		base.setTertiaryTextContent("Pick or force the lock until its HP reaches zero." + this.getLockpickCount());
	}

	// Token: 0x06000851 RID: 2129 RVA: 0x000287F0 File Offset: 0x000269F0
	private string getLockpickCount()
	{
		int itemCount = this.getCharacter().getInventory().getItemCount("ITE_MiscLockpick");
		return "\nParty has " + C64Color.ATTRIBUTE_VALUE_TAG + itemCount.ToString() + "</color> Thieves' Tools";
	}

	// Token: 0x06000852 RID: 2130 RVA: 0x0002882E File Offset: 0x00026A2E
	protected override void setPopUpUI(string description, List<string> buttonList)
	{
		this.uiElements = new PopUpBase.PopUpUITestLockPick(description, buttonList);
	}

	// Token: 0x06000853 RID: 2131 RVA: 0x00028840 File Offset: 0x00026A40
	public override void handle()
	{
		if (base.isHandled())
		{
			return;
		}
		base.handle();
		if (base.isRolling())
		{
			base.setTertiaryTextContent(base.getLoadString());
			return;
		}
		base.setMainTextContent(this.propLockable.getDescription());
		base.setSecondaryTextContent(this.propLockable.getLockDescription());
		(this.uiElements as PopUpBase.PopUpUITestLockPick).adjustHealthBar(this.propLockable);
		if (this.test != null)
		{
			base.setTertiaryTextContent(this.test.getReturnString() + this.getLockpickCount());
		}
		if (!this.propLockable.isLocked())
		{
			this.handle(true);
		}
		if (SkaldIO.getPressedNumericButton1() || base.getButtonPressIndex() == 0)
		{
			this.handle(this.propLockable.pick());
			return;
		}
		if (SkaldIO.getPressedNumericButton2() || base.getButtonPressIndex() == 1)
		{
			this.handle(this.propLockable.force());
			return;
		}
		if (SkaldIO.getPressedEscapeKey() || SkaldIO.getPressedNumericButton3() || base.getButtonPressIndex() == 2)
		{
			this.handle(false);
		}
	}

	// Token: 0x06000854 RID: 2132 RVA: 0x00028942 File Offset: 0x00026B42
	private void handle(SkaldTestBase test)
	{
		this.test = test;
		base.resetLoadString();
		base.roll(test);
	}

	// Token: 0x040001C4 RID: 452
	private PropLockable propLockable;
}
