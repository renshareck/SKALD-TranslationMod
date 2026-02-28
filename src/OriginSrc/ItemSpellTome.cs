using System;
using System.Runtime.Serialization;
using UnityEngine;

// Token: 0x020000D5 RID: 213
[Serializable]
public class ItemSpellTome : ItemUseable, ISerializable
{
	// Token: 0x06000CEC RID: 3308 RVA: 0x0003B763 File Offset: 0x00039963
	public ItemSpellTome(SKALDProjectData.ItemDataContainers.TomeContainer.TomeData rawData) : base(rawData)
	{
	}

	// Token: 0x06000CED RID: 3309 RVA: 0x0003B76C File Offset: 0x0003996C
	public ItemSpellTome(SerializationInfo info, StreamingContext context)
	{
		this.dynamicData = (Item.ItemSaveData)info.GetValue("saveData", typeof(Item.ItemSaveData));
		if (this.dynamicData == null)
		{
			MainControl.logError("Tome could not load dynamic data!");
		}
		this.applySaveData(this.dynamicData);
	}

	// Token: 0x06000CEE RID: 3310 RVA: 0x0003B7BD File Offset: 0x000399BD
	public new void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("saveData", this.dynamicData, typeof(Item.ItemSaveData));
	}

	// Token: 0x06000CEF RID: 3311 RVA: 0x0003B7DA File Offset: 0x000399DA
	public override Item.ItemTypes getType()
	{
		return Item.ItemTypes.Tome;
	}

	// Token: 0x06000CF0 RID: 3312 RVA: 0x0003B7DE File Offset: 0x000399DE
	public override string getUseVerb(Character character)
	{
		return "Learn";
	}

	// Token: 0x06000CF1 RID: 3313 RVA: 0x0003B7E5 File Offset: 0x000399E5
	public override bool isStackable()
	{
		return true;
	}

	// Token: 0x06000CF2 RID: 3314 RVA: 0x0003B7E8 File Offset: 0x000399E8
	public override bool isLootable()
	{
		return true;
	}

	// Token: 0x06000CF3 RID: 3315 RVA: 0x0003B7EB File Offset: 0x000399EB
	public override bool isSellable()
	{
		return true;
	}

	// Token: 0x06000CF4 RID: 3316 RVA: 0x0003B7F0 File Offset: 0x000399F0
	public override SkaldActionResult useItem(Character user)
	{
		AbilitySpell spell = this.getSpell();
		if (spell == null)
		{
			return new SkaldActionResult(true, false, "No spell set for this tome.", true);
		}
		if (user.getSpellContainer().hasComponent(spell.getId()))
		{
			SkaldActionResult skaldActionResult = new SkaldActionResult(true, false, user.getName() + " already  knows " + spell.getName(), true);
			PopUpControl.addPopUpOK(skaldActionResult.getResultString());
			return skaldActionResult;
		}
		SkaldActionResult skaldActionResult2 = spell.canUserCastThisSpellTier(user, true);
		if (!skaldActionResult2.wasSuccess())
		{
			PopUpControl.addPopUpOK(skaldActionResult2.getResultString());
			return new SkaldActionResult(true, false, "Cannot learn spell.", true);
		}
		user.addSpell(spell.getId());
		MainControl.getDataControl().deleteCurrentItem();
		base.playUseSound();
		return new SkaldActionResult(true, true, user.getName() + " learned " + spell.getName() + ".", true);
	}

	// Token: 0x06000CF5 RID: 3317 RVA: 0x0003B8BC File Offset: 0x00039ABC
	protected override string getUseSound()
	{
		return "ItemPaper1";
	}

	// Token: 0x06000CF6 RID: 3318 RVA: 0x0003B8C4 File Offset: 0x00039AC4
	public override int getValue()
	{
		AbilitySpell spell = this.getSpell();
		if (spell == null)
		{
			return 100;
		}
		int tier = spell.getTier();
		if (tier == 1)
		{
			return 200;
		}
		if (tier == 2)
		{
			return 400;
		}
		if (tier == 3)
		{
			return 800;
		}
		if (tier == 4)
		{
			return 1600;
		}
		return Mathf.RoundToInt(Mathf.Pow((float)spell.getTier(), 2f) * 100f) + 400;
	}

	// Token: 0x06000CF7 RID: 3319 RVA: 0x0003B930 File Offset: 0x00039B30
	public override string getName()
	{
		SKALDProjectData.AbilityContainers.SpellContainer.SpellAbility spellData = this.getSpellData();
		if (spellData == null)
		{
			return "Spell Tome";
		}
		return "Tome: " + spellData.title;
	}

	// Token: 0x06000CF8 RID: 3320 RVA: 0x0003B960 File Offset: 0x00039B60
	public override string getDescription()
	{
		AbilitySpell spell = this.getSpell();
		if (spell == null)
		{
			return "A spell tome";
		}
		string text = "Read to learn the spell: " + spell.getName();
		text = text + "\n\nRequires ranks in one of the following schools: \n" + C64Color.GRAY_LIGHT_TAG;
		foreach (string id in spell.getSchoolList())
		{
			text = string.Concat(new string[]
			{
				text,
				"Tier ",
				spell.getTier().ToString(),
				" ",
				GameData.getAttributeName(id),
				"\n"
			});
		}
		text += "</color>\n";
		return text;
	}

	// Token: 0x06000CF9 RID: 3321 RVA: 0x0003BA30 File Offset: 0x00039C30
	public override float getWeight()
	{
		return 1f;
	}

	// Token: 0x06000CFA RID: 3322 RVA: 0x0003BA37 File Offset: 0x00039C37
	public override bool destroyOnUse()
	{
		return true;
	}

	// Token: 0x06000CFB RID: 3323 RVA: 0x0003BA3A File Offset: 0x00039C3A
	public override int getStoreStack()
	{
		return 1;
	}

	// Token: 0x06000CFC RID: 3324 RVA: 0x0003BA40 File Offset: 0x00039C40
	public override string getImagePath()
	{
		AbilitySpell spell = this.getSpell();
		if (spell == null)
		{
			return "BookSpellTomeArcane1";
		}
		return spell.getSpelltomeIconPath();
	}

	// Token: 0x06000CFD RID: 3325 RVA: 0x0003BA64 File Offset: 0x00039C64
	public new SKALDProjectData.ItemDataContainers.TomeContainer.TomeData getRawData()
	{
		SKALDProjectData.ItemDataContainers.ItemData rawData = base.getRawData();
		if (rawData is SKALDProjectData.ItemDataContainers.TomeContainer.TomeData)
		{
			return rawData as SKALDProjectData.ItemDataContainers.TomeContainer.TomeData;
		}
		return null;
	}

	// Token: 0x06000CFE RID: 3326 RVA: 0x0003BA88 File Offset: 0x00039C88
	public SKALDProjectData.AbilityContainers.SpellContainer.SpellAbility getSpellData()
	{
		SKALDProjectData.ItemDataContainers.TomeContainer.TomeData rawData = this.getRawData();
		if (rawData == null || rawData.spellLearned == "")
		{
			return null;
		}
		return GameData.getSpellRawData(rawData.spellLearned);
	}

	// Token: 0x06000CFF RID: 3327 RVA: 0x0003BAC0 File Offset: 0x00039CC0
	public AbilitySpell getSpell()
	{
		SKALDProjectData.ItemDataContainers.TomeContainer.TomeData rawData = this.getRawData();
		if (rawData == null && rawData.spellLearned == "")
		{
			return null;
		}
		return GameData.getSpell(rawData.spellLearned);
	}
}
