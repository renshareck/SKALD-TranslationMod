using System;

// Token: 0x02000006 RID: 6
public class Ability : BaseCharacterComponent
{
	// Token: 0x06000027 RID: 39 RVA: 0x00002848 File Offset: 0x00000A48
	public Ability(SKALDProjectData.AbilityContainers.AbilityData rawData) : base(rawData)
	{
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00002851 File Offset: 0x00000A51
	private SKALDProjectData.AbilityContainers.AbilityData getRawData()
	{
		return GameData.getAbilityRawData(this.getId());
	}

	// Token: 0x06000029 RID: 41 RVA: 0x0000285E File Offset: 0x00000A5E
	public virtual bool stackable()
	{
		return false;
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00002861 File Offset: 0x00000A61
	protected override string printComponentType()
	{
		return "Ability";
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00002868 File Offset: 0x00000A68
	public virtual void applyAbility(Character character)
	{
		SKALDProjectData.AbilityContainers.AbilityData rawData = this.getRawData();
		if (rawData == null)
		{
			return;
		}
		base.processString(rawData.getTrigger, character);
		if (MainControl.debugFunctions)
		{
			MainControl.log("Added ability to " + character.getId() + ": " + this.getId());
		}
		if (character.isPC())
		{
			character.addPositiveBark("Added Ability: " + this.getName());
		}
	}
}
