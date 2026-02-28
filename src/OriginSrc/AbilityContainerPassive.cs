using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

// Token: 0x0200000B RID: 11
[Serializable]
public class AbilityContainerPassive : AbilityContainer, ISerializable
{
	// Token: 0x0600007B RID: 123 RVA: 0x00004945 File Offset: 0x00002B45
	public AbilityContainerPassive(Character owner) : base(owner)
	{
	}

	// Token: 0x0600007C RID: 124 RVA: 0x0000494E File Offset: 0x00002B4E
	public AbilityContainerPassive(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00004958 File Offset: 0x00002B58
	public new void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		base.GetObjectData(info, context);
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00004962 File Offset: 0x00002B62
	protected override List<string> getItemComponentIdList()
	{
		return base.getItemComponentIdListGeneric<AbilityPassive>();
	}

	// Token: 0x0600007F RID: 127 RVA: 0x0000496A File Offset: 0x00002B6A
	protected override bool allowDuplicatedItemComponents()
	{
		return true;
	}

	// Token: 0x06000080 RID: 128 RVA: 0x00004970 File Offset: 0x00002B70
	public int getModifierToAttributeFromComponents(string attributeId)
	{
		int num = 0;
		foreach (string id in base.getComponentIdList())
		{
			List<string> affectedComponents = BaseCharacterComponent.GetAffectedComponents(id);
			if (affectedComponents != null && affectedComponents.Contains(attributeId))
			{
				BaseCharacterComponent baseCharacterComponent = this.forcedGetComponentFromIdWithoutCheckingIfIHaveIt(id);
				num += baseCharacterComponent.getModifierToAttribute(attributeId);
			}
		}
		return num;
	}

	// Token: 0x06000081 RID: 129 RVA: 0x000049E8 File Offset: 0x00002BE8
	public void addAbility(AbilityPassive ability, Character character)
	{
		base.addAbility(ability, character);
	}
}
