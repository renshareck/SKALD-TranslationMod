using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x02000021 RID: 33
[Serializable]
public class CharacterLooksControl
{
	// Token: 0x060003EA RID: 1002 RVA: 0x000129C4 File Offset: 0x00010BC4
	public CharacterLooksControl()
	{
		this.skinColor = C64Color.getSkinColors()[0];
		this.hairColor = C64Color.getHairColors()[0];
		this.mainColor = C64Color.getClothingColors()[0];
		this.secondaryColor = C64Color.getClothingColors()[0];
		this.tertiaryColor = C64Color.getClothingColors()[0];
		this.setColorBufferString();
	}

	// Token: 0x060003EB RID: 1003 RVA: 0x00012A98 File Offset: 0x00010C98
	private string testIfListContains(string value, List<string> list)
	{
		if (list.Count == 0)
		{
			return "";
		}
		using (List<string>.Enumerator enumerator = list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.ToUpper() == value.ToUpper())
				{
					return value;
				}
			}
		}
		return list[0];
	}

	// Token: 0x060003EC RID: 1004 RVA: 0x00012B0C File Offset: 0x00010D0C
	public Dictionary<Color32, Color32> getSwapDictionary(string weaponDetail, string weaponMetal, string armorDetail, string armorColor)
	{
		return C64Color.getSwapDictionary(this.getSkinColor(), this.getHairColor(), this.getMainColor(), this.getSecondaryColor(), this.getTertiaryColor(), weaponDetail, weaponMetal, armorDetail, armorColor);
	}

	// Token: 0x060003ED RID: 1005 RVA: 0x00012B44 File Offset: 0x00010D44
	public string getHairStylePath()
	{
		SKALDProjectData.ApperanceElementContainers.ApperanceElement hairData = GameData.getHairData(this.hairStyleID);
		if (hairData == null)
		{
			return "";
		}
		return hairData.modelPath;
	}

	// Token: 0x060003EE RID: 1006 RVA: 0x00012B6C File Offset: 0x00010D6C
	public void setHairStyleId(string value)
	{
		this.hairStyleID = value;
	}

	// Token: 0x060003EF RID: 1007 RVA: 0x00012B78 File Offset: 0x00010D78
	public string getBeardStylePath()
	{
		SKALDProjectData.ApperanceElementContainers.ApperanceElement beardData = GameData.getBeardData(this.beardStyleID);
		if (beardData == null)
		{
			return "";
		}
		return beardData.modelPath;
	}

	// Token: 0x060003F0 RID: 1008 RVA: 0x00012BA0 File Offset: 0x00010DA0
	public void setBeardStyleId(string value)
	{
		this.beardStyleID = value;
	}

	// Token: 0x060003F1 RID: 1009 RVA: 0x00012BAC File Offset: 0x00010DAC
	public string getPortraitPath()
	{
		SKALDProjectData.ApperanceElementContainers.ApperanceElement portraitData = GameData.getPortraitData(this.portraitID);
		if (portraitData == null)
		{
			return "";
		}
		return portraitData.modelPath;
	}

	// Token: 0x060003F2 RID: 1010 RVA: 0x00012BD4 File Offset: 0x00010DD4
	public void setPortraitId(string value)
	{
		this.portraitID = value;
	}

	// Token: 0x060003F3 RID: 1011 RVA: 0x00012BDD File Offset: 0x00010DDD
	public string getHairColor()
	{
		return this.hairColor;
	}

	// Token: 0x060003F4 RID: 1012 RVA: 0x00012BE8 File Offset: 0x00010DE8
	private void setColorBufferString()
	{
		StringBuilder stringBuilder = new StringBuilder(128);
		stringBuilder.Append(this.getSkinColor());
		stringBuilder.Append(this.getHairColor());
		stringBuilder.Append(this.getMainColor());
		stringBuilder.Append(this.getSecondaryColor());
		stringBuilder.Append(this.getTertiaryColor());
		this.colorBufferString = stringBuilder.ToString();
	}

	// Token: 0x060003F5 RID: 1013 RVA: 0x00012C4D File Offset: 0x00010E4D
	public string getColorBufferString()
	{
		return this.colorBufferString;
	}

	// Token: 0x060003F6 RID: 1014 RVA: 0x00012C55 File Offset: 0x00010E55
	public void setHairColor(string value)
	{
		this.hairColor = this.testIfListContains(value, C64Color.getHairColors());
		this.setColorBufferString();
	}

	// Token: 0x060003F7 RID: 1015 RVA: 0x00012C6F File Offset: 0x00010E6F
	public string getSkinColor()
	{
		return this.skinColor;
	}

	// Token: 0x060003F8 RID: 1016 RVA: 0x00012C77 File Offset: 0x00010E77
	public void setSkinColor(string value)
	{
		this.skinColor = this.testIfListContains(value, C64Color.getSkinColorsForAll());
		this.setColorBufferString();
	}

	// Token: 0x060003F9 RID: 1017 RVA: 0x00012C91 File Offset: 0x00010E91
	public string getMainColor()
	{
		return this.mainColor;
	}

	// Token: 0x060003FA RID: 1018 RVA: 0x00012C99 File Offset: 0x00010E99
	public void setMainColor(string value)
	{
		this.mainColor = this.testIfListContains(value, C64Color.getClothingColors());
		this.setColorBufferString();
	}

	// Token: 0x060003FB RID: 1019 RVA: 0x00012CB3 File Offset: 0x00010EB3
	public string getSecondaryColor()
	{
		return this.secondaryColor;
	}

	// Token: 0x060003FC RID: 1020 RVA: 0x00012CBB File Offset: 0x00010EBB
	public void setSecondaryColor(string value)
	{
		this.secondaryColor = this.testIfListContains(value, C64Color.getClothingColors());
		this.setColorBufferString();
	}

	// Token: 0x060003FD RID: 1021 RVA: 0x00012CD5 File Offset: 0x00010ED5
	public string getTertiaryColor()
	{
		return this.tertiaryColor;
	}

	// Token: 0x060003FE RID: 1022 RVA: 0x00012CDD File Offset: 0x00010EDD
	public void setTertiaryColor(string value)
	{
		this.tertiaryColor = this.testIfListContains(value, C64Color.getClothingColors());
		this.setColorBufferString();
	}

	// Token: 0x04000099 RID: 153
	private string skinColor = "";

	// Token: 0x0400009A RID: 154
	private string hairColor = "";

	// Token: 0x0400009B RID: 155
	private string beardStyleID = "";

	// Token: 0x0400009C RID: 156
	private string hairStyleID = "";

	// Token: 0x0400009D RID: 157
	private string mainColor = "";

	// Token: 0x0400009E RID: 158
	private string secondaryColor = "";

	// Token: 0x0400009F RID: 159
	private string tertiaryColor = "";

	// Token: 0x040000A0 RID: 160
	private string portraitID = "";

	// Token: 0x040000A1 RID: 161
	private string colorBufferString = "";
}
