using System;

// Token: 0x02000004 RID: 4
[Serializable]
public class BaseBehaviourControl
{
	// Token: 0x06000023 RID: 35 RVA: 0x00002813 File Offset: 0x00000A13
	public BaseBehaviourControl(Character _character)
	{
		this.character = _character;
	}

	// Token: 0x06000024 RID: 36 RVA: 0x00002822 File Offset: 0x00000A22
	public void update()
	{
		if (this.currentBehaviour == null)
		{
			return;
		}
		this.currentBehaviour.update(this.character);
	}

	// Token: 0x04000004 RID: 4
	private BaseCharacterBehaviour currentBehaviour;

	// Token: 0x04000005 RID: 5
	private Character character;
}
