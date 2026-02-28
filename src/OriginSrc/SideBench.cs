using System;

// Token: 0x02000108 RID: 264
[Serializable]
public class SideBench : SkaldObjectList
{
	// Token: 0x060010BA RID: 4282 RVA: 0x0004C310 File Offset: 0x0004A510
	public SkaldBaseObject add(Character character)
	{
		if (character == null)
		{
			return null;
		}
		MainControl.log("Adding to side-bench: " + character.getId());
		character.clearTilePosition();
		return base.add(character);
	}

	// Token: 0x060010BB RID: 4283 RVA: 0x0004C339 File Offset: 0x0004A539
	public Character removeCharacter(string characterId)
	{
		base.setCurrentObject(base.getObject(characterId));
		return this.removeCurrentObject() as Character;
	}
}
