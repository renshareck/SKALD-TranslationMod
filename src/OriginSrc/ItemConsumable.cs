using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

// Token: 0x020000C3 RID: 195
[Serializable]
public class ItemConsumable : ItemUseable, ISerializable
{
	// Token: 0x06000C6C RID: 3180 RVA: 0x0003A218 File Offset: 0x00038418
	public ItemConsumable(SKALDProjectData.ItemDataContainers.ConsumeableContainer.Consumeable rawData) : base(rawData)
	{
	}

	// Token: 0x06000C6D RID: 3181 RVA: 0x0003A224 File Offset: 0x00038424
	public ItemConsumable(SerializationInfo info, StreamingContext context)
	{
		this.dynamicData = (Item.ItemSaveData)info.GetValue("saveData", typeof(Item.ItemSaveData));
		if (this.dynamicData == null)
		{
			MainControl.logError("Consumeable could not load dynamic data!");
		}
		this.applySaveData(this.dynamicData);
	}

	// Token: 0x06000C6E RID: 3182 RVA: 0x0003A275 File Offset: 0x00038475
	public new void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("saveData", this.dynamicData, typeof(Item.ItemSaveData));
	}

	// Token: 0x06000C6F RID: 3183 RVA: 0x0003A292 File Offset: 0x00038492
	public override Item.ItemTypes getType()
	{
		return Item.ItemTypes.Consumable;
	}

	// Token: 0x06000C70 RID: 3184 RVA: 0x0003A296 File Offset: 0x00038496
	protected override string getUseSound()
	{
		return "ItemPotion1";
	}

	// Token: 0x06000C71 RID: 3185 RVA: 0x0003A29D File Offset: 0x0003849D
	public override string getUseVerb(Character character)
	{
		return "Consume";
	}

	// Token: 0x06000C72 RID: 3186 RVA: 0x0003A2A4 File Offset: 0x000384A4
	public override bool destroyOnUse()
	{
		return true;
	}

	// Token: 0x06000C73 RID: 3187 RVA: 0x0003A2A8 File Offset: 0x000384A8
	public override string getDescription()
	{
		string text = base.getDescription();
		List<string> useEffects = base.getUseEffects();
		if (useEffects.Count == 0)
		{
			return text;
		}
		if (text != "")
		{
			text += "\n\n";
		}
		Effect.EffectDescription effectDescription = new Effect.EffectDescription();
		foreach (string id in useEffects)
		{
			effectDescription.mergeInEffectDescription(GameData.getEffect(id));
		}
		text += effectDescription.printDescription();
		return text;
	}

	// Token: 0x06000C74 RID: 3188 RVA: 0x0003A344 File Offset: 0x00038544
	protected override string getUseActionResultString()
	{
		return "Consumed 1 " + this.getName();
	}
}
