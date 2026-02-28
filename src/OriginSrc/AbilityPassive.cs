using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200000D RID: 13
public class AbilityPassive : Ability
{
	// Token: 0x06000088 RID: 136 RVA: 0x00004A9C File Offset: 0x00002C9C
	public AbilityPassive(SKALDProjectData.AbilityContainers.PassiveAbilityContainer.PassiveAbility rawData) : base(rawData)
	{
	}

	// Token: 0x06000089 RID: 137 RVA: 0x00004AA5 File Offset: 0x00002CA5
	private SKALDProjectData.AbilityContainers.PassiveAbilityContainer.PassiveAbility getRawData()
	{
		return GameData.getAbilityRawData(this.getId()) as SKALDProjectData.AbilityContainers.PassiveAbilityContainer.PassiveAbility;
	}

	// Token: 0x0600008A RID: 138 RVA: 0x00004AB7 File Offset: 0x00002CB7
	protected override string printComponentType()
	{
		return "Passive Ability";
	}

	// Token: 0x0600008B RID: 139 RVA: 0x00004AC0 File Offset: 0x00002CC0
	public override string getDescription()
	{
		string text = "";
		SKALDProjectData.AbilityContainers.PassiveAbilityContainer.PassiveAbility rawData = this.getRawData();
		if (rawData == null)
		{
			return "";
		}
		if (rawData.bonusAttributes.Count != 0)
		{
			if (text != "")
			{
				text += "\n";
			}
			foreach (string id in rawData.bonusAttributes)
			{
				text = string.Concat(new string[]
				{
					text,
					TextTools.formatePlusMinus(rawData.bonusMagnitude),
					GameData.getAttributeSuffix(id),
					" ",
					GameData.getAttributeName(id),
					"\n"
				});
			}
		}
		if (rawData.penaltyAttributes.Count != 0)
		{
			if (text != "")
			{
				text += "\n";
			}
			foreach (string id2 in rawData.penaltyAttributes)
			{
				text = string.Concat(new string[]
				{
					text,
					TextTools.formatePlusMinus(rawData.penaltyMagnitude),
					GameData.getAttributeSuffix(id2),
					" ",
					GameData.getAttributeName(id2),
					"\n"
				});
			}
		}
		if (rawData.staticCondition.Count != 0)
		{
			if (text != "")
			{
				text += "\n";
			}
			text += "Confers Conditions: ";
			foreach (string id3 in rawData.staticCondition)
			{
				text = text + GameData.getAttributeName(id3) + ", ";
			}
			text = TextTools.removeTrailingComma(text);
		}
		if (base.getDescription() != "")
		{
			if (text != "")
			{
				text += "\n";
			}
			text += base.getDescription();
		}
		return text;
	}

	// Token: 0x0600008C RID: 140 RVA: 0x00004CF4 File Offset: 0x00002EF4
	protected override List<Color32> gridIconBaseColor()
	{
		return new List<Color32>
		{
			C64Color.Cyan,
			C64Color.BlueLight
		};
	}

	// Token: 0x0600008D RID: 141 RVA: 0x00004D14 File Offset: 0x00002F14
	protected override List<string> getAffectedAttributes()
	{
		List<string> list = new List<string>();
		SKALDProjectData.AbilityContainers.PassiveAbilityContainer.PassiveAbility rawData = this.getRawData();
		if (rawData == null)
		{
			return null;
		}
		foreach (string item in rawData.bonusAttributes)
		{
			list.Add(item);
		}
		foreach (string item2 in rawData.penaltyAttributes)
		{
			list.Add(item2);
		}
		return list;
	}

	// Token: 0x0600008E RID: 142 RVA: 0x00004DC0 File Offset: 0x00002FC0
	public override int getModifierToAttribute(string attributeId)
	{
		if (this.modifierBuffer == null)
		{
			this.modifierBuffer = new Dictionary<string, int>();
		}
		if (this.modifierBuffer.ContainsKey(attributeId))
		{
			return this.modifierBuffer[attributeId];
		}
		int num = 0;
		SKALDProjectData.AbilityContainers.PassiveAbilityContainer.PassiveAbility rawData = this.getRawData();
		if (rawData == null)
		{
			return 0;
		}
		using (List<string>.Enumerator enumerator = rawData.bonusAttributes.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current == attributeId)
				{
					num += rawData.bonusMagnitude;
					break;
				}
			}
		}
		using (List<string>.Enumerator enumerator = rawData.penaltyAttributes.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current == attributeId)
				{
					num -= rawData.penaltyMagnitude;
					break;
				}
			}
		}
		this.modifierBuffer.Add(attributeId, num);
		return num;
	}

	// Token: 0x0600008F RID: 143 RVA: 0x00004EB8 File Offset: 0x000030B8
	public override bool stackable()
	{
		return true;
	}

	// Token: 0x04000008 RID: 8
	private Dictionary<string, int> modifierBuffer;
}
