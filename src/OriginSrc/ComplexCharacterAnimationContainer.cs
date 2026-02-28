using System;

// Token: 0x02000027 RID: 39
[Serializable]
public class ComplexCharacterAnimationContainer : CharacterAnimationContainer
{
	// Token: 0x0600044C RID: 1100 RVA: 0x0001362E File Offset: 0x0001182E
	public ComplexCharacterAnimationContainer()
	{
		base.setDeathAnimation("ANI_DeadComplex");
		base.setPanicAnimation("ANI_PanicComplex");
		base.setBloodiedAnimation("ANI_IdleSad");
	}
}
