using System;
using System.Collections.Generic;

// Token: 0x02000162 RID: 354
public class UIPartyEffectSelector : UICanvasVertical
{
	// Token: 0x06001370 RID: 4976 RVA: 0x00055AF4 File Offset: 0x00053CF4
	public UIPartyEffectSelector(List<Character> characterList, bool allowFullParty)
	{
		this.backgroundTexture = TextureTools.loadTextureData("Images/GUIIcons/EffectSelector");
		base.setDimensions(33, 200, this.backgroundTexture.width, this.backgroundTexture.height);
		this.header = new UITextBlock(0, 0, this.getWidth(), 16, C64Color.White, FontContainer.getMediumFont());
		this.header.padding.top = 8;
		this.header.setLetterShadowColor(C64Color.Black);
		this.header.setAlignment(new UIElement.Alignments(UIElement.Alignments.VerticalAlignments.Top, UIElement.Alignments.HorizontalAlignments.Center));
		this.header.setContent("Select a target!");
		this.header.padding.bottom = 8;
		this.add(this.header);
		this.portraitRow = new UIPartyEffectSelector.PortraitRow(this.getWidth(), characterList, allowFullParty);
		this.add(this.portraitRow);
	}

	// Token: 0x06001371 RID: 4977 RVA: 0x00055BD8 File Offset: 0x00053DD8
	public void setHeader(string message)
	{
		this.header.setContent(message);
	}

	// Token: 0x06001372 RID: 4978 RVA: 0x00055BE6 File Offset: 0x00053DE6
	public void update()
	{
		this.portraitRow.update();
	}

	// Token: 0x06001373 RID: 4979 RVA: 0x00055BF3 File Offset: 0x00053DF3
	public List<Character> getSelectedCharacters()
	{
		return this.portraitRow.getSelectedCharacters();
	}

	// Token: 0x040004CA RID: 1226
	private UITextBlock header;

	// Token: 0x040004CB RID: 1227
	private UIPartyEffectSelector.PortraitRow portraitRow;

	// Token: 0x020002A3 RID: 675
	private class PortraitRow : UICanvasHorizontal
	{
		// Token: 0x06001AF6 RID: 6902 RVA: 0x0007491C File Offset: 0x00072B1C
		public PortraitRow(int width, List<Character> characterList, bool selectAll)
		{
			this.setWidth(width);
			this.setAlignment(new UIElement.Alignments(UIElement.Alignments.VerticalAlignments.Top, UIElement.Alignments.HorizontalAlignments.Center));
			this.allowFullParty = selectAll;
			for (int i = 0; i < characterList.Count; i++)
			{
				this.add(new UIPartyEffectSelector.PortraitRow.Portrait(characterList[i], i == 0 || selectAll));
			}
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x00074974 File Offset: 0x00072B74
		public void update()
		{
			foreach (UIElement uielement in base.getElements())
			{
				((UIPartyEffectSelector.PortraitRow.Portrait)uielement).update();
			}
			foreach (UIElement uielement2 in base.getElements())
			{
				UIPartyEffectSelector.PortraitRow.Portrait portrait = (UIPartyEffectSelector.PortraitRow.Portrait)uielement2;
				if (portrait.getLeftUp())
				{
					portrait.setToggled();
				}
				if (portrait.isToggled() && !this.allowFullParty)
				{
					foreach (UIElement uielement3 in base.getElements())
					{
						UIPartyEffectSelector.PortraitRow.Portrait portrait2 = (UIPartyEffectSelector.PortraitRow.Portrait)uielement3;
						if (portrait2 != portrait)
						{
							portrait2.clearToggled();
						}
					}
				}
			}
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x00074A74 File Offset: 0x00072C74
		public List<Character> getSelectedCharacters()
		{
			List<Character> list = new List<Character>();
			foreach (UIElement uielement in base.getElements())
			{
				UIPartyEffectSelector.PortraitRow.Portrait portrait = (UIPartyEffectSelector.PortraitRow.Portrait)uielement;
				if (portrait.isToggled())
				{
					list.Add(portrait.getCharacter());
				}
			}
			return list;
		}

		// Token: 0x040009D7 RID: 2519
		private bool allowFullParty;

		// Token: 0x020003DE RID: 990
		private class Portrait : UIElement
		{
			// Token: 0x06001D90 RID: 7568 RVA: 0x0007CBB0 File Offset: 0x0007ADB0
			public Portrait(Character character, bool selected)
			{
				this.character = character;
				this.toggled = selected;
				this.padding.left = 4;
				this.padding.right = 4;
				TextureTools.TextureData overlay = TextureTools.loadTextureData("Images/Portraits/SelectorFull");
				TextureTools.applyOverlay(this.selectedTexture, character.getPortrait(), 0, 0);
				TextureTools.applyOverlay(this.selectedTexture, overlay, 0, 0);
				TextureTools.applyOverlay(this.notSelectedTexture, character.getPortrait(), 0, 0);
				this.backgroundTexture = this.selectedTexture;
				this.stretchHorizontal = true;
				this.stretchVertical = true;
			}

			// Token: 0x06001D91 RID: 7569 RVA: 0x0007CC60 File Offset: 0x0007AE60
			public bool isToggled()
			{
				return this.toggled;
			}

			// Token: 0x06001D92 RID: 7570 RVA: 0x0007CC68 File Offset: 0x0007AE68
			public Character getCharacter()
			{
				return this.character;
			}

			// Token: 0x06001D93 RID: 7571 RVA: 0x0007CC70 File Offset: 0x0007AE70
			public void setToggled()
			{
				this.toggled = !this.toggled;
			}

			// Token: 0x06001D94 RID: 7572 RVA: 0x0007CC81 File Offset: 0x0007AE81
			public void clearToggled()
			{
				this.toggled = false;
			}

			// Token: 0x06001D95 RID: 7573 RVA: 0x0007CC8A File Offset: 0x0007AE8A
			internal void update()
			{
				this.updateMouseInteraction();
			}

			// Token: 0x06001D96 RID: 7574 RVA: 0x0007CC92 File Offset: 0x0007AE92
			public override void draw(TextureTools.TextureData targetTexture)
			{
				if (this.toggled)
				{
					this.backgroundTexture = this.selectedTexture;
				}
				else
				{
					this.backgroundTexture = this.notSelectedTexture;
				}
				base.draw(targetTexture);
			}

			// Token: 0x04000C7A RID: 3194
			private Character character;

			// Token: 0x04000C7B RID: 3195
			private TextureTools.TextureData selectedTexture = new TextureTools.TextureData(40, 40);

			// Token: 0x04000C7C RID: 3196
			private TextureTools.TextureData notSelectedTexture = new TextureTools.TextureData(40, 40);

			// Token: 0x04000C7D RID: 3197
			private bool toggled;
		}
	}
}
