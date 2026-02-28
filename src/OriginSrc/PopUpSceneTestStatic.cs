using System;
using System.Collections.Generic;

// Token: 0x02000057 RID: 87
public class PopUpSceneTestStatic : PopUpSceneTestBase
{
	// Token: 0x06000877 RID: 2167 RVA: 0x000293C5 File Offset: 0x000275C5
	public PopUpSceneTestStatic(Scene scene, int exitNumber) : base(scene, exitNumber, "", new List<string>
	{
		"Continue"
	})
	{
	}

	// Token: 0x06000878 RID: 2168 RVA: 0x000293E4 File Offset: 0x000275E4
	protected override void setPopUpUI(string description, List<string> buttonList)
	{
		this.uiElements = new PopUpBase.PopUpUISimple(description, buttonList);
	}

	// Token: 0x06000879 RID: 2169 RVA: 0x000293F3 File Offset: 0x000275F3
	protected override int getSkillValue()
	{
		return this.getCharacter().getCurrentAttributeValueStatic(this.attributeEnum);
	}

	// Token: 0x0600087A RID: 2170 RVA: 0x00029408 File Offset: 0x00027608
	protected override void printPreRollAssesment()
	{
		string secondaryTextContent = string.Concat(new string[]
		{
			"A ",
			base.getAttributeName(),
			" score of ",
			(this.difficulty - 7).ToString(),
			" or higher is required to succeed."
		});
		base.setSecondaryTextContent(secondaryTextContent);
	}

	// Token: 0x0600087B RID: 2171 RVA: 0x0002945C File Offset: 0x0002765C
	public override void handle()
	{
		if (base.isHandled())
		{
			return;
		}
		this.test = new SkaldTestGreaterThan(this.getCharacter(), this.attributeEnum, this.difficulty);
		base.handle();
		this.printPreRollAssesment();
		base.setMainTextContent(C64Color.YELLOW_TAG + "STATIC TEST</color>");
		if (this.test.wasSuccess())
		{
			base.setTertiaryTextContent(string.Concat(new string[]
			{
				C64Color.GREEN_LIGHT_TAG,
				"SUCCESS:</color> ",
				this.getCharacter().getName(),
				" has a ",
				base.getAttributeName(),
				" score of ",
				(this.getSkillValue() - 7).ToString()
			}));
		}
		else
		{
			base.setTertiaryTextContent(string.Concat(new string[]
			{
				C64Color.RED_LIGHT_TAG,
				"FAILURE:</color> ",
				this.getCharacter().getName(),
				" only has a ",
				base.getAttributeName(),
				" score of ",
				(this.getSkillValue() - 7).ToString()
			}));
		}
		if (SkaldIO.getPressedMainInteractKey() || SkaldIO.getPressedNumericButton1() || base.getButtonPressIndex() == 0)
		{
			this.handle(this.test.wasSuccess());
		}
	}
}
