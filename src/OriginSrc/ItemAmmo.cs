using System;
using System.Runtime.Serialization;

// Token: 0x020000BE RID: 190
[Serializable]
public class ItemAmmo : ItemDamaging, ISerializable
{
	// Token: 0x06000C38 RID: 3128 RVA: 0x000397BE File Offset: 0x000379BE
	public ItemAmmo(SKALDProjectData.ItemDataContainers.AmmoContainer.AmmoData rawData) : base(rawData)
	{
	}

	// Token: 0x06000C39 RID: 3129 RVA: 0x000397C7 File Offset: 0x000379C7
	public ItemAmmo()
	{
	}

	// Token: 0x06000C3A RID: 3130 RVA: 0x000397D0 File Offset: 0x000379D0
	public ItemAmmo(SerializationInfo info, StreamingContext context)
	{
		this.dynamicData = (Item.ItemSaveData)info.GetValue("saveData", typeof(Item.ItemSaveData));
		if (this.dynamicData == null)
		{
			MainControl.logError("Ammo could not load dynamic data!");
		}
		this.applySaveData(this.dynamicData);
	}

	// Token: 0x06000C3B RID: 3131 RVA: 0x00039821 File Offset: 0x00037A21
	public new void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("saveData", this.dynamicData, typeof(Item.ItemSaveData));
	}

	// Token: 0x06000C3C RID: 3132 RVA: 0x0003983E File Offset: 0x00037A3E
	public override Item.ItemTypes getType()
	{
		return Item.ItemTypes.Ammo;
	}

	// Token: 0x06000C3D RID: 3133 RVA: 0x00039841 File Offset: 0x00037A41
	public override bool isStackable()
	{
		return true;
	}

	// Token: 0x06000C3E RID: 3134 RVA: 0x00039844 File Offset: 0x00037A44
	private new SKALDProjectData.ItemDataContainers.AmmoContainer.AmmoData getRawData()
	{
		return GameData.getItemRawData(this.getId()) as SKALDProjectData.ItemDataContainers.AmmoContainer.AmmoData;
	}

	// Token: 0x06000C3F RID: 3135 RVA: 0x00039858 File Offset: 0x00037A58
	public string getAmmoType()
	{
		SKALDProjectData.ItemDataContainers.AmmoContainer.AmmoData rawData = this.getRawData();
		if (rawData == null)
		{
			return "";
		}
		return rawData.ammoType;
	}

	// Token: 0x06000C40 RID: 3136 RVA: 0x0003987C File Offset: 0x00037A7C
	public bool isRecoverable()
	{
		SKALDProjectData.ItemDataContainers.AmmoContainer.AmmoData rawData = this.getRawData();
		return rawData != null && rawData.recoverable;
	}

	// Token: 0x06000C41 RID: 3137 RVA: 0x0003989B File Offset: 0x00037A9B
	public override void setUser(Character c)
	{
		c.setPreferredAmmo(this.getId());
		c.updateItemSlot();
	}

	// Token: 0x06000C42 RID: 3138 RVA: 0x000398AF File Offset: 0x00037AAF
	protected override string printDamageString()
	{
		return "+" + base.printDamageString();
	}

	// Token: 0x06000C43 RID: 3139 RVA: 0x000398C4 File Offset: 0x00037AC4
	public override string printComparativeStats(Character compareCharacter)
	{
		string str = this.printStatsHeader();
		ItemAmmo itemAmmo = null;
		if (compareCharacter != null)
		{
			itemAmmo = compareCharacter.getCurrentAmmo();
		}
		if (itemAmmo == this)
		{
			itemAmmo = null;
		}
		string value;
		if (itemAmmo == null)
		{
			value = TextTools.formatePlusMinus(base.getHitBonus()) + "\n";
		}
		else
		{
			value = base.makeComparativeColorTag((float)base.getHitBonus(), (float)itemAmmo.getHitBonus(), "", "") + "\n";
		}
		str += TextTools.formateNameValuePair("Accuracy", value);
		string value2;
		if (itemAmmo == null)
		{
			value2 = this.printDamageString();
		}
		else if (base.getMaxDamage() == itemAmmo.getMaxDamage())
		{
			value2 = this.printDamageString();
		}
		else if (base.getMaxDamage() > itemAmmo.getMaxDamage())
		{
			value2 = string.Concat(new string[]
			{
				C64Color.GREEN_LIGHT_TAG,
				this.printDamageString(),
				"</color> (Vs ",
				itemAmmo.printDamageString(),
				")"
			});
		}
		else
		{
			value2 = string.Concat(new string[]
			{
				C64Color.RED_LIGHT_TAG,
				this.printDamageString(),
				"</color> (Vs ",
				itemAmmo.printDamageString(),
				")"
			});
		}
		str = str + TextTools.formateNameValuePair("Damage", value2) + "\n";
		string value3;
		if (itemAmmo == null)
		{
			value3 = base.getCritMultiplier().ToString("0.0") + "\n";
		}
		else
		{
			value3 = base.makeComparativeColorTag(base.getCritMultiplier(), itemAmmo.getCritMultiplier(), "x", "") + "\n";
		}
		str += TextTools.formateNameValuePair("Crit.", value3);
		return str + this.printStatsTail();
	}

	// Token: 0x06000C44 RID: 3140 RVA: 0x00039A7C File Offset: 0x00037C7C
	protected override string getTypeString()
	{
		string text = "Ammo";
		string text2 = base.printDamageTypeString();
		if (text2 != "")
		{
			text = text + ", " + text2;
		}
		return text;
	}
}
