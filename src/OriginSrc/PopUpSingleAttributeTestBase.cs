using System;
using System.Collections.Generic;

// Token: 0x02000058 RID: 88
public abstract class PopUpSingleAttributeTestBase : PopUpTestBase
{
	// Token: 0x0600087C RID: 2172 RVA: 0x0002959C File Offset: 0x0002779C
	protected PopUpSingleAttributeTestBase(string description, List<string> options, int difficulty, string attributeId) : this(description, options, difficulty, AttributesControl.getEnumFromString(attributeId))
	{
	}

	// Token: 0x0600087D RID: 2173 RVA: 0x000295AE File Offset: 0x000277AE
	protected PopUpSingleAttributeTestBase(string description, List<string> options, int difficulty, AttributesControl.CoreAttributes attribute) : base(description, options)
	{
		this.difficulty = difficulty;
		this.attributeEnum = attribute;
	}

	// Token: 0x0600087E RID: 2174 RVA: 0x000295C7 File Offset: 0x000277C7
	protected virtual int getSkillValue()
	{
		return this.getCharacter().getCurrentAttributeValue(this.attributeEnum);
	}

	// Token: 0x0600087F RID: 2175 RVA: 0x000295DA File Offset: 0x000277DA
	protected string getAttributeName()
	{
		return GameData.getAttributeName(this.attributeEnum).ToUpper();
	}

	// Token: 0x06000880 RID: 2176 RVA: 0x000295EC File Offset: 0x000277EC
	protected void generateTest()
	{
		this.test = new SkaldTestRandomVsStatic(this.getCharacter(), this.attributeEnum, this.difficulty, this.getCharacter().getAttributeCheckRerolls());
		base.roll(this.test);
	}

	// Token: 0x06000881 RID: 2177 RVA: 0x00029624 File Offset: 0x00027824
	protected void printRollAssesment()
	{
		int num = this.difficulty - this.getSkillValue();
		string tertiaryTextContent;
		if (num <= 2)
		{
			tertiaryTextContent = string.Concat(new string[]
			{
				C64Color.CYAN_TAG,
				this.getCharacter().getName().ToUpper(),
				"</color> will automatically ",
				C64Color.ATTRIBUTE_VALUE_TAG,
				"SUCCEED</color> this skill check due to having high Skill Score versus the Difficulty."
			});
		}
		else if (num == 12)
		{
			tertiaryTextContent = string.Concat(new string[]
			{
				C64Color.CYAN_TAG,
				this.getCharacter().getName().ToUpper(),
				"</color> must roll ",
				C64Color.ATTRIBUTE_VALUE_TAG,
				num.ToString(),
				"</color> on 2d6 to succeed on this ",
				C64Color.CYAN_TAG,
				this.getAttributeName(),
				"</color> skill check!"
			});
		}
		else if (num > 12)
		{
			tertiaryTextContent = string.Concat(new string[]
			{
				C64Color.CYAN_TAG,
				this.getCharacter().getName().ToUpper(),
				"</color> will automatically ",
				C64Color.ATTRIBUTE_VALUE_TAG,
				"FAIL</color> this skill check due to having low Skill Score versus the Difficulty."
			});
		}
		else
		{
			tertiaryTextContent = string.Concat(new string[]
			{
				C64Color.CYAN_TAG,
				this.getCharacter().getName().ToUpper(),
				"</color> must roll ",
				C64Color.ATTRIBUTE_VALUE_TAG,
				num.ToString(),
				"</color> or above on 2d6 to succeed on this ",
				C64Color.CYAN_TAG,
				this.getAttributeName(),
				"</color> skill check!"
			});
		}
		base.setTertiaryTextContent(tertiaryTextContent);
	}

	// Token: 0x06000882 RID: 2178 RVA: 0x000297A4 File Offset: 0x000279A4
	protected virtual void printPreRollAssesment()
	{
		string secondaryTextContent = string.Concat(new string[]
		{
			C64Color.ATTRIBUTE_NAME_TAG,
			this.getAttributeName(),
			":</color> ",
			C64Color.ATTRIBUTE_VALUE_TAG,
			this.getSkillValue().ToString(),
			" + 2d6</color>\n",
			C64Color.ATTRIBUTE_NAME_TAG,
			"DIFFICULTY:</color> ",
			C64Color.ATTRIBUTE_VALUE_TAG,
			this.difficulty.ToString(),
			"</color>"
		});
		base.setSecondaryTextContent(secondaryTextContent);
	}

	// Token: 0x06000883 RID: 2179 RVA: 0x0002982F File Offset: 0x00027A2F
	protected virtual void printResultDescription()
	{
		if (this.test.wasSuccess())
		{
			base.setMainTextContent("You bring glory upon thine Ancestors!");
			return;
		}
		base.setMainTextContent("You have been found wanting...");
	}

	// Token: 0x06000884 RID: 2180 RVA: 0x00029858 File Offset: 0x00027A58
	public override void handle()
	{
		if (base.isHandled())
		{
			return;
		}
		base.handle();
		this.printPreRollAssesment();
		if (base.isRolling())
		{
			base.setTertiaryTextContent(base.getLoadString());
			base.setButtonsText(new List<string>
			{
				"..."
			});
			return;
		}
		if (this.test != null)
		{
			base.setTertiaryTextContent(this.test.getReturnString());
			base.setButtonsText(new List<string>
			{
				"Continue"
			});
			this.printResultDescription();
			if (SkaldIO.getPressedMainInteractKey() || SkaldIO.getPressedNumericButton1() || base.getButtonPressIndex() == 0)
			{
				this.handle(this.test.wasSuccess());
				return;
			}
		}
		else
		{
			this.printRollAssesment();
			if (SkaldIO.getPressedMainInteractKey() || SkaldIO.getPressedNumericButton1() || base.getButtonPressIndex() == 0)
			{
				this.generateTest();
			}
		}
	}

	// Token: 0x040001E4 RID: 484
	protected int difficulty;

	// Token: 0x040001E5 RID: 485
	protected AttributesControl.CoreAttributes attributeEnum;
}
