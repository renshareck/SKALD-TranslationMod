using System;
using System.Collections.Generic;

// Token: 0x0200015A RID: 346
public class UIInitiativeList : UICanvasVertical
{
	// Token: 0x06001330 RID: 4912 RVA: 0x00054D51 File Offset: 0x00052F51
	public UIInitiativeList() : base(UIInitiativeList.restingX, 100, 0, 0)
	{
		this.stretchHorizontal = true;
		this.stretchVertical = true;
	}

	// Token: 0x06001331 RID: 4913 RVA: 0x00054D70 File Offset: 0x00052F70
	public void setButtons(List<Character> characters, Character currentCharacter)
	{
		this.clearElements();
		if (characters == null || characters.Count == 0)
		{
			return;
		}
		foreach (Character character in characters)
		{
			this.add(new UIInitiativeList.InitiativeButton(character, character == currentCharacter));
			this.setY(135 + this.getHeight() / 2);
			this.alignElements();
		}
		this.setHover();
	}

	// Token: 0x06001332 RID: 4914 RVA: 0x00054DFC File Offset: 0x00052FFC
	private void setHover()
	{
		bool flag = false;
		UIInitiativeList.currentCharacter = null;
		foreach (UIElement uielement in base.getElements())
		{
			UIInitiativeList.InitiativeButton initiativeButton = (UIInitiativeList.InitiativeButton)uielement;
			initiativeButton.updateMouseInteraction();
			if (initiativeButton.getHover())
			{
				flag = true;
				UIInitiativeList.currentCharacter = initiativeButton.getCharacter();
				break;
			}
		}
		if (flag)
		{
			base.setTargetDimensions(0, this.getY());
			return;
		}
		base.setTargetDimensions(UIInitiativeList.restingX, this.getY());
	}

	// Token: 0x06001333 RID: 4915 RVA: 0x00054E94 File Offset: 0x00053094
	public static Character getCurrentCharacter()
	{
		if (!MainControl.getDataControl().isCombatActive())
		{
			UIInitiativeList.currentCharacter = null;
		}
		return UIInitiativeList.currentCharacter;
	}

	// Token: 0x040004B1 RID: 1201
	private static int restingX = -41;

	// Token: 0x040004B2 RID: 1202
	private static Character currentCharacter = null;

	// Token: 0x02000299 RID: 665
	private class InitiativeButton : UITextBlock
	{
		// Token: 0x06001ACC RID: 6860 RVA: 0x00073EBC File Offset: 0x000720BC
		public InitiativeButton(Character character, bool isCurrent) : base(0, 0, 60, 12, C64Color.GrayLight)
		{
			this.character = character;
			if (character.isHostile() && isCurrent)
			{
				this.backgroundTexture = UIInitiativeList.InitiativeButton.textureEnemySelected;
			}
			else if (character.isHostile() && !isCurrent)
			{
				this.backgroundTexture = UIInitiativeList.InitiativeButton.textureEnemy;
			}
			else if (!character.isHostile() && character.isPC() && isCurrent)
			{
				this.backgroundTexture = UIInitiativeList.InitiativeButton.texturePCSelected;
			}
			else if (!character.isHostile() && character.isPC() && !isCurrent)
			{
				this.backgroundTexture = UIInitiativeList.InitiativeButton.texturePC;
			}
			else if (!character.isHostile() && isCurrent)
			{
				this.backgroundTexture = UIInitiativeList.InitiativeButton.textureAllySelected;
			}
			else if (!character.isHostile() && !isCurrent)
			{
				this.backgroundTexture = UIInitiativeList.InitiativeButton.textureAlly;
			}
			if (character.isHostile())
			{
				this.foregroundColors.hoverColor = C64Color.RedLight;
			}
			else if (character.isPC())
			{
				this.foregroundColors.hoverColor = C64Color.GreenLight;
			}
			else
			{
				this.foregroundColors.hoverColor = C64Color.Cyan;
			}
			this.padding.top = 3;
			this.padding.left = 2;
			base.setContent(TextTools.adjustStringLength(character.getName(), 9));
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x00073FF4 File Offset: 0x000721F4
		public Character getCharacter()
		{
			return this.character;
		}

		// Token: 0x040009C0 RID: 2496
		private Character character;

		// Token: 0x040009C1 RID: 2497
		private static TextureTools.TextureData textureEnemySelected = TextureTools.loadTextureData("Images/GUIIcons/MiscCombatUI/InitiativeEnemySelected");

		// Token: 0x040009C2 RID: 2498
		private static TextureTools.TextureData textureEnemy = TextureTools.loadTextureData("Images/GUIIcons/MiscCombatUI/InitiativeEnemy");

		// Token: 0x040009C3 RID: 2499
		private static TextureTools.TextureData texturePCSelected = TextureTools.loadTextureData("Images/GUIIcons/MiscCombatUI/InitiativePCSelected");

		// Token: 0x040009C4 RID: 2500
		private static TextureTools.TextureData texturePC = TextureTools.loadTextureData("Images/GUIIcons/MiscCombatUI/InitiativePC");

		// Token: 0x040009C5 RID: 2501
		private static TextureTools.TextureData textureAllySelected = TextureTools.loadTextureData("Images/GUIIcons/MiscCombatUI/InitiativeAllySelected");

		// Token: 0x040009C6 RID: 2502
		private static TextureTools.TextureData textureAlly = TextureTools.loadTextureData("Images/GUIIcons/MiscCombatUI/InitiativeAlly");
	}
}
