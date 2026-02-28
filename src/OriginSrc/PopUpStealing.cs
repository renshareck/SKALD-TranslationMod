using System;
using System.Collections.Generic;

// Token: 0x0200005B RID: 91
public class PopUpStealing : PopUpSingleAttributeTestBase
{
	// Token: 0x06000891 RID: 2193 RVA: 0x00029EC0 File Offset: 0x000280C0
	public PopUpStealing(Store store) : base("Trying to steal " + store.getCurrentItemName() + " from merchant", new List<string>
	{
		"Steal",
		"Leave"
	}, store.getCurrentItemStealDC(), AttributesControl.CoreAttributes.ATT_Thievery)
	{
	}

	// Token: 0x06000892 RID: 2194 RVA: 0x00029F16 File Offset: 0x00028116
	public override void succeed()
	{
		this.stealResult = MainControl.getDataControl().currentStore.stealItem().getVerboseResultString();
		PopUpControl.addPopUpOK(this.stealResult);
		base.succeed();
	}

	// Token: 0x06000893 RID: 2195 RVA: 0x00029F43 File Offset: 0x00028143
	public override void fail()
	{
		MainControl.getDataControl().currentStore.activateStealLocked();
		base.fail();
	}

	// Token: 0x06000894 RID: 2196 RVA: 0x00029F5A File Offset: 0x0002815A
	protected override void printResultDescription()
	{
		if (this.test.wasSuccess())
		{
			base.setMainTextContent(this.stealResult);
			return;
		}
		base.setMainTextContent("Failed to steal item!");
	}

	// Token: 0x06000895 RID: 2197 RVA: 0x00029F81 File Offset: 0x00028181
	public override void handle()
	{
		if (base.isHandled())
		{
			return;
		}
		if (SkaldIO.getPressedEscapeKey() || SkaldIO.getPressedNumericButton2() || base.getButtonPressIndex() == 1)
		{
			this.handle(false);
			return;
		}
		base.handle();
	}

	// Token: 0x040001ED RID: 493
	private string stealResult = "";
}
