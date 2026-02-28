using System;

// Token: 0x02000019 RID: 25
public class CharacterBackground : CharacterFeature
{
	// Token: 0x06000192 RID: 402 RVA: 0x00009650 File Offset: 0x00007850
	public CharacterBackground(SKALDProjectData.BackgroundContainers.BackgroundData rawData) : base(rawData)
	{
	}

	// Token: 0x06000193 RID: 403 RVA: 0x00009659 File Offset: 0x00007859
	public CharacterBackground()
	{
	}

	// Token: 0x06000194 RID: 404 RVA: 0x00009664 File Offset: 0x00007864
	public override string getDescription()
	{
		string text = base.getDescription() + "\n";
		foreach (string id in this.abilityList)
		{
			Ability ability = GameData.getAbility(id);
			if (ability != null)
			{
				text = text + "\n" + ability.getDescription();
			}
		}
		return text;
	}
}
