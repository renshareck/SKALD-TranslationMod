using System;

// Token: 0x02000137 RID: 311
public class UIActionCounter : UICanvasVertical
{
	// Token: 0x0600122E RID: 4654 RVA: 0x000507E0 File Offset: 0x0004E9E0
	public UIActionCounter(int x, int y) : base(x, y, 11, 0)
	{
		this.stretchVertical = true;
		this.initialYPosition = y;
		this.actionPointAttackSpent = TextureTools.loadTextureData("Images/GUIIcons/MiscCombatUI/ActionPointAttackSpent");
		this.actionPointAttack = TextureTools.loadTextureData("Images/GUIIcons/MiscCombatUI/ActionPointAttack");
		this.actionPointMovementSpent = TextureTools.loadTextureData("Images/GUIIcons/MiscCombatUI/ActionPointMovementSpent");
		this.actionPointMovemet = TextureTools.loadTextureData("Images/GUIIcons/MiscCombatUI/ActionPointMovement");
	}

	// Token: 0x0600122F RID: 4655 RVA: 0x00050854 File Offset: 0x0004EA54
	public void update(Character character, AbilityUseable ability)
	{
		if (character == null)
		{
			return;
		}
		this.blinkTimer.tick();
		bool flag = this.blinkTimer.getTimer() < 30;
		AbilityUseable.TimeCost timeCost = AbilityUseable.TimeCost.Free;
		if (ability != null)
		{
			timeCost = ability.getTimeCost();
		}
		this.clearElements();
		for (int i = 0; i < character.getMaxAttacks() - character.getRemainingAttacks(); i++)
		{
			this.add(new UIImage(this.actionPointAttackSpent));
		}
		for (int j = 0; j < character.getRemainingAttacks(); j++)
		{
			if (flag && (timeCost == AbilityUseable.TimeCost.FullRound || (timeCost == AbilityUseable.TimeCost.MoveEquivalent && character.getRemainingCombatMoves() == 0)))
			{
				this.add(new UIImage(this.actionPointAttackSpent));
			}
			else
			{
				this.add(new UIImage(this.actionPointAttack));
			}
		}
		for (int k = 0; k < character.getRemainingCombatMoves(); k++)
		{
			if (flag && (timeCost == AbilityUseable.TimeCost.FullRound || timeCost == AbilityUseable.TimeCost.MoveEquivalent))
			{
				this.add(new UIImage(this.actionPointAttackSpent));
			}
			else
			{
				this.add(new UIImage(this.actionPointMovemet));
			}
		}
		for (int l = 0; l < character.getMaxMoves() - character.getRemainingCombatMoves(); l++)
		{
			this.add(new UIImage(this.actionPointMovementSpent));
		}
		this.setY(this.initialYPosition + this.getHeight());
		this.alignElements();
	}

	// Token: 0x04000457 RID: 1111
	private int initialYPosition;

	// Token: 0x04000458 RID: 1112
	private CountDownClock blinkTimer = new CountDownClock(60, true);

	// Token: 0x04000459 RID: 1113
	private TextureTools.TextureData actionPointAttackSpent;

	// Token: 0x0400045A RID: 1114
	private TextureTools.TextureData actionPointAttack;

	// Token: 0x0400045B RID: 1115
	private TextureTools.TextureData actionPointMovementSpent;

	// Token: 0x0400045C RID: 1116
	private TextureTools.TextureData actionPointMovemet;
}
