using System;

// Token: 0x0200005E RID: 94
public class PopUpTestPropRandom : PopUpSingleAttributeTestBase
{
	// Token: 0x060008A3 RID: 2211 RVA: 0x0002A0C1 File Offset: 0x000282C1
	public PopUpTestPropRandom(PropTest testProp) : base(testProp.getDescription(), testProp.getOptionList(), testProp.getDifficulty(), testProp.getTestAttributeId())
	{
		this.testProp = testProp;
	}

	// Token: 0x060008A4 RID: 2212 RVA: 0x0002A0E8 File Offset: 0x000282E8
	protected override void printResultDescription()
	{
		if (this.test.wasSuccess())
		{
			base.setMainTextContent(this.testProp.getSuccessDescription());
			return;
		}
		base.setMainTextContent(this.testProp.getFailureDescription());
	}

	// Token: 0x060008A5 RID: 2213 RVA: 0x0002A11A File Offset: 0x0002831A
	public override void succeed()
	{
		this.testProp.processSuccessTrigger();
		base.succeed();
	}

	// Token: 0x060008A6 RID: 2214 RVA: 0x0002A12E File Offset: 0x0002832E
	public override void fail()
	{
		this.testProp.processFailureTrigger();
		base.fail();
	}

	// Token: 0x060008A7 RID: 2215 RVA: 0x0002A144 File Offset: 0x00028344
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
		if (this.testProp.canBonusItemBeUsed() && (SkaldIO.getPressedNumericButton3() || base.getButtonPressIndex() == 2))
		{
			this.testProp.processBonusItemTrigger();
			base.setMainTextContent(this.testProp.getBonusItemDescription());
			base.handle(true);
			return;
		}
		base.handle();
	}

	// Token: 0x040001F1 RID: 497
	private PropTest testProp;
}
