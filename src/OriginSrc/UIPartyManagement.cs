using System;
using System.Collections.Generic;

// Token: 0x02000163 RID: 355
public class UIPartyManagement : UICanvasVertical
{
	// Token: 0x06001374 RID: 4980 RVA: 0x00055C00 File Offset: 0x00053E00
	public UIPartyManagement()
	{
		base.setDimensions(0, 0, 350, 0);
		this.alignments.horizontalAlignment = UIElement.Alignments.HorizontalAlignments.Center;
		this.stretchVertical = true;
		this.header1 = new UIPartyManagement.MediumHeader();
		this.header1.setContent("Main Party");
		this.add(this.header1);
		this.partyBlock = new UIPartyManagement.UIPortraitBlockParty();
		this.add(this.partyBlock);
		this.header2 = new UIPartyManagement.MediumHeader();
		this.header2.setContent("Camp Followers");
		this.add(this.header2);
		this.sideBenchBlock = new UIPartyManagement.UIPortraitBlockSideBench();
		this.add(this.sideBenchBlock);
	}

	// Token: 0x06001375 RID: 4981 RVA: 0x00055CB0 File Offset: 0x00053EB0
	public Character updatePartyList(List<SkaldBaseObject> characters)
	{
		return this.partyBlock.updatePartyList(characters);
	}

	// Token: 0x06001376 RID: 4982 RVA: 0x00055CBE File Offset: 0x00053EBE
	public Character updateSideBenchBlock(List<SkaldBaseObject> characters)
	{
		return this.sideBenchBlock.updatePartyList(characters);
	}

	// Token: 0x040004CC RID: 1228
	private UIPartyManagement.UIPortraitBlock partyBlock;

	// Token: 0x040004CD RID: 1229
	private UIPartyManagement.UIPortraitBlock sideBenchBlock;

	// Token: 0x040004CE RID: 1230
	private UIPartyManagement.MediumHeader header1;

	// Token: 0x040004CF RID: 1231
	private UIPartyManagement.MediumHeader header2;

	// Token: 0x020002A4 RID: 676
	private class UIPortraitBlockSideBench : UIPartyManagement.UIPortraitBlock
	{
		// Token: 0x06001AF9 RID: 6905 RVA: 0x00074AE0 File Offset: 0x00072CE0
		public UIPortraitBlockSideBench() : base(2)
		{
		}
	}

	// Token: 0x020002A5 RID: 677
	private class UIPortraitBlockParty : UIPartyManagement.UIPortraitBlock
	{
		// Token: 0x06001AFA RID: 6906 RVA: 0x00074AE9 File Offset: 0x00072CE9
		public UIPortraitBlockParty() : base(1)
		{
		}
	}

	// Token: 0x020002A6 RID: 678
	private abstract class UIPortraitBlock : UICanvasVertical
	{
		// Token: 0x06001AFB RID: 6907 RVA: 0x00074AF4 File Offset: 0x00072CF4
		protected UIPortraitBlock(int height)
		{
			this.stretchVertical = true;
			this.alignments.horizontalAlignment = UIElement.Alignments.HorizontalAlignments.Center;
			for (int i = 0; i < height; i++)
			{
				this.add(new UIPartyManagement.UIPortraitBlock.UIPortraitRow(6));
			}
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x00074B34 File Offset: 0x00072D34
		public Character updatePartyList(List<SkaldBaseObject> characters)
		{
			int num = 0;
			foreach (UIElement uielement in base.getElements())
			{
				UIPartyManagement.UIPortraitBlock.UIPortraitRow uiportraitRow = (UIPartyManagement.UIPortraitBlock.UIPortraitRow)uielement;
				List<Character> list = new List<Character>();
				for (int i = 0; i < 6; i++)
				{
					if (num < characters.Count)
					{
						list.Add(characters[num] as Character);
					}
					num++;
				}
				Character character = uiportraitRow.updatePartyList(list);
				if (character != null)
				{
					return character;
				}
			}
			return null;
		}

		// Token: 0x040009D8 RID: 2520
		private const int width = 6;

		// Token: 0x020003DF RID: 991
		private class UIPortraitRow : UICanvasHorizontal
		{
			// Token: 0x06001D97 RID: 7575 RVA: 0x0007CCC0 File Offset: 0x0007AEC0
			public UIPortraitRow(int width)
			{
				this.stretchVertical = true;
				this.alignments.horizontalAlignment = UIElement.Alignments.HorizontalAlignments.Center;
				for (int i = 0; i < width; i++)
				{
					this.add(new UIPartyManagement.UIPortraitBlock.UIPortraitRow.UIPortrait());
				}
			}

			// Token: 0x06001D98 RID: 7576 RVA: 0x0007CD00 File Offset: 0x0007AF00
			public Character updatePartyList(List<Character> characters)
			{
				int num = 0;
				foreach (UIElement uielement in base.getElements())
				{
					UIPartyManagement.UIPortraitBlock.UIPortraitRow.UIPortrait uiportrait = (UIPartyManagement.UIPortraitBlock.UIPortraitRow.UIPortrait)uielement;
					if (num < characters.Count)
					{
						Character character = uiportrait.update(characters[num]);
						if (character != null)
						{
							return character;
						}
					}
					else
					{
						uiportrait.update(null);
					}
					num++;
				}
				return null;
			}

			// Token: 0x02000433 RID: 1075
			private class UIPortrait : UIElement
			{
				// Token: 0x06001E05 RID: 7685 RVA: 0x0007DEB8 File Offset: 0x0007C0B8
				public UIPortrait()
				{
					base.setDimensions(0, 0, 41, 40);
					this.padding.right = 5;
					this.setPaddingTop(5);
					this.backgroundTexture = this.background;
					this.allowDoubleClick = false;
				}

				// Token: 0x06001E06 RID: 7686 RVA: 0x0007DF20 File Offset: 0x0007C120
				public Character update(Character character)
				{
					this.updateMouseInteraction();
					if (character != null)
					{
						this.backgroundTexture = character.getPortrait();
					}
					else
					{
						this.backgroundTexture = this.background;
					}
					if (base.getHover())
					{
						this.foregroundTexture = this.frame;
					}
					else
					{
						this.foregroundTexture = null;
					}
					if (character != null && base.getRightUp())
					{
						ToolTipPrinter.setToolTipWithRules(character.getInspectDescription());
					}
					if (base.getLeftUp())
					{
						return character;
					}
					return null;
				}

				// Token: 0x04000D9C RID: 3484
				private TextureTools.TextureData background = TextureTools.loadTextureData("Images/Portraits/SelectorBackground");

				// Token: 0x04000D9D RID: 3485
				private TextureTools.TextureData frame = TextureTools.loadTextureData("Images/Portraits/SelectorFull");
			}
		}
	}

	// Token: 0x020002A7 RID: 679
	private class MediumHeader : UITextBlock
	{
		// Token: 0x06001AFD RID: 6909 RVA: 0x00074BD4 File Offset: 0x00072DD4
		public MediumHeader() : base(0, 0, 300, 10, C64Color.White, FontContainer.getMediumFont())
		{
			this.alignments.horizontalAlignment = UIElement.Alignments.HorizontalAlignments.Center;
			this.foregroundColors.shadowMainColor = C64Color.Black;
			this.padding.top = 8;
			this.padding.bottom = 2;
		}
	}
}
