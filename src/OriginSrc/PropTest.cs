using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

// Token: 0x02000100 RID: 256
[Serializable]
public class PropTest : PropActivatable, ISerializable
{
	// Token: 0x06001054 RID: 4180 RVA: 0x0004ADA4 File Offset: 0x00048FA4
	public PropTest(SKALDProjectData.PropContainers.TestPropContainer.TestProp rawData) : base(rawData)
	{
	}

	// Token: 0x06001055 RID: 4181 RVA: 0x0004ADAD File Offset: 0x00048FAD
	public PropTest(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}

	// Token: 0x06001056 RID: 4182 RVA: 0x0004ADB8 File Offset: 0x00048FB8
	private new SKALDProjectData.PropContainers.TestPropContainer.TestProp getRawData()
	{
		SKALDProjectData.PropContainers.Prop rawData = base.getRawData();
		if (rawData is SKALDProjectData.PropContainers.TestPropContainer.TestProp)
		{
			return rawData as SKALDProjectData.PropContainers.TestPropContainer.TestProp;
		}
		return null;
	}

	// Token: 0x06001057 RID: 4183 RVA: 0x0004ADDC File Offset: 0x00048FDC
	public int getDifficulty()
	{
		return this.getRawData().difficulty;
	}

	// Token: 0x06001058 RID: 4184 RVA: 0x0004ADE9 File Offset: 0x00048FE9
	public string getBonusItemID()
	{
		return this.getRawData().bonusItem;
	}

	// Token: 0x06001059 RID: 4185 RVA: 0x0004ADF8 File Offset: 0x00048FF8
	public bool canBonusItemBeUsed()
	{
		string bonusItemID = this.getBonusItemID();
		return !(bonusItemID == "") && MainControl.getDataControl().getItemCount(bonusItemID) > 0;
	}

	// Token: 0x0600105A RID: 4186 RVA: 0x0004AE2C File Offset: 0x0004902C
	public List<string> getOptionList()
	{
		SKALDProjectData.PropContainers.TestPropContainer.TestProp rawData = this.getRawData();
		if (!this.canBonusItemBeUsed())
		{
			return new List<string>
			{
				this.getVerb(),
				"Leave"
			};
		}
		string text = rawData.bonusItemVerb;
		if (text == "")
		{
			text = GameData.getItemName(rawData.bonusItem);
		}
		return new List<string>
		{
			this.getVerb(),
			"Leave",
			text
		};
	}

	// Token: 0x0600105B RID: 4187 RVA: 0x0004AEA8 File Offset: 0x000490A8
	public string getSuccessDescription()
	{
		return this.getRawData().successDescription;
	}

	// Token: 0x0600105C RID: 4188 RVA: 0x0004AEB5 File Offset: 0x000490B5
	public string getFailureDescription()
	{
		return this.getRawData().failureDescription;
	}

	// Token: 0x0600105D RID: 4189 RVA: 0x0004AEC2 File Offset: 0x000490C2
	public string getTestAttributeId()
	{
		return this.getRawData().testAttribute;
	}

	// Token: 0x0600105E RID: 4190 RVA: 0x0004AECF File Offset: 0x000490CF
	public string processBonusItemTrigger()
	{
		string result = base.processString(this.getRawData().bonusItemTrigger, null);
		if (this.getRawData().consumeBonusItem)
		{
			MainControl.getDataControl().getInventory().deleteItem(this.getBonusItemID());
		}
		return result;
	}

	// Token: 0x0600105F RID: 4191 RVA: 0x0004AF06 File Offset: 0x00049106
	public string getBonusItemDescription()
	{
		return this.getRawData().bonusItemDescription;
	}

	// Token: 0x06001060 RID: 4192 RVA: 0x0004AF13 File Offset: 0x00049113
	public string processSuccessTrigger()
	{
		return base.processString(this.getRawData().successTrigger, null);
	}

	// Token: 0x06001061 RID: 4193 RVA: 0x0004AF27 File Offset: 0x00049127
	public string processFailureTrigger()
	{
		return base.processString(this.getRawData().failureTrigger, null);
	}

	// Token: 0x06001062 RID: 4194 RVA: 0x0004AF3B File Offset: 0x0004913B
	private bool isRandom()
	{
		return this.getRawData().testRandom;
	}

	// Token: 0x06001063 RID: 4195 RVA: 0x0004AF48 File Offset: 0x00049148
	public override string interactWithProp()
	{
		if (this.isRandom())
		{
			PopUpControl.addPropRandomTestPopUp(this);
		}
		return base.interactWithProp();
	}
}
