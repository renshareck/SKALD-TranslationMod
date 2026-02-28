using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

// Token: 0x0200000A RID: 10
[Serializable]
public class AbilityContainerManeuver : AbilityContainer, ISerializable
{
	// Token: 0x06000076 RID: 118 RVA: 0x00004916 File Offset: 0x00002B16
	public AbilityContainerManeuver(Character owner) : base(owner)
	{
	}

	// Token: 0x06000077 RID: 119 RVA: 0x0000491F File Offset: 0x00002B1F
	public AbilityContainerManeuver(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}

	// Token: 0x06000078 RID: 120 RVA: 0x00004929 File Offset: 0x00002B29
	public new void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		base.GetObjectData(info, context);
	}

	// Token: 0x06000079 RID: 121 RVA: 0x00004933 File Offset: 0x00002B33
	protected override List<string> getItemComponentIdList()
	{
		return base.getItemComponentIdListGeneric<AbilityCombatManeuver>();
	}

	// Token: 0x0600007A RID: 122 RVA: 0x0000493B File Offset: 0x00002B3B
	public void addAbility(AbilityCombatManeuver ability, Character character)
	{
		base.addAbility(ability, character);
	}
}
