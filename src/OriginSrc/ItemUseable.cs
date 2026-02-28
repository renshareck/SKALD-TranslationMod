using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

// Token: 0x020000D7 RID: 215
[Serializable]
public abstract class ItemUseable : Item, ISerializable
{
	// Token: 0x06000D05 RID: 3333 RVA: 0x0003BB7A File Offset: 0x00039D7A
	protected ItemUseable(SKALDProjectData.ItemDataContainers.ItemData rawData) : base(rawData)
	{
	}

	// Token: 0x06000D06 RID: 3334 RVA: 0x0003BB83 File Offset: 0x00039D83
	protected ItemUseable()
	{
	}

	// Token: 0x06000D07 RID: 3335 RVA: 0x0003BB8B File Offset: 0x00039D8B
	public override bool isUseable()
	{
		return true;
	}

	// Token: 0x06000D08 RID: 3336 RVA: 0x0003BB8E File Offset: 0x00039D8E
	public override string getUseVerb(Character character)
	{
		return "Use";
	}

	// Token: 0x06000D09 RID: 3337 RVA: 0x0003BB98 File Offset: 0x00039D98
	protected virtual string getUseTrigger()
	{
		SKALDProjectData.ItemDataContainers.ItemData rawData = this.getRawData();
		if (rawData == null)
		{
			return "";
		}
		string text = rawData.use;
		if (rawData.parent == null || rawData.parent == "")
		{
			return text;
		}
		SKALDProjectData.ItemDataContainers.ItemData itemRawData = GameData.getItemRawData(rawData.parent);
		if (itemRawData != null)
		{
			text += itemRawData.use;
		}
		return text;
	}

	// Token: 0x06000D0A RID: 3338 RVA: 0x0003BBF5 File Offset: 0x00039DF5
	public virtual bool destroyOnUse()
	{
		return false;
	}

	// Token: 0x06000D0B RID: 3339 RVA: 0x0003BBF8 File Offset: 0x00039DF8
	protected override string getUseActionResultString()
	{
		if (this.destroyOnUse())
		{
			return "Used 1 " + this.getName();
		}
		return "Used " + this.getName();
	}

	// Token: 0x06000D0C RID: 3340 RVA: 0x0003BC24 File Offset: 0x00039E24
	protected List<string> getUseEffects()
	{
		SKALDProjectData.ItemDataContainers.ItemData rawData = this.getRawData();
		if (rawData == null)
		{
			return new List<string>();
		}
		List<string> list = new List<string>();
		if (rawData.useEffect != null)
		{
			foreach (string item in rawData.useEffect)
			{
				list.Add(item);
			}
		}
		if (rawData.parent != null && rawData.parent != "")
		{
			SKALDProjectData.ItemDataContainers.ItemData itemRawData = GameData.getItemRawData(rawData.parent);
			if (itemRawData != null)
			{
				foreach (string item2 in itemRawData.useEffect)
				{
					list.Add(item2);
				}
			}
		}
		SKALDProjectData.EnchantmentContainers.Enchantment enchantment = base.getEnchantment();
		if (enchantment != null && enchantment.useEffect != null)
		{
			foreach (string item3 in enchantment.useEffect)
			{
				list.Add(item3);
			}
		}
		return list;
	}

	// Token: 0x06000D0D RID: 3341 RVA: 0x0003BD60 File Offset: 0x00039F60
	public override SkaldActionResult useItem(Character user)
	{
		if (this.destroyOnUse())
		{
			MainControl.getDataControl().deleteCurrentItem();
		}
		base.playUseSound();
		string verboseResultString = "";
		base.processString(this.getUseTrigger(), null);
		foreach (string id in this.getUseEffects())
		{
			Effect effect = GameData.getEffect(id);
			if (effect != null)
			{
				effect.fireEffect(user, user);
			}
		}
		return new SkaldActionResult(true, true, this.getUseActionResultString(), verboseResultString, true);
	}
}
