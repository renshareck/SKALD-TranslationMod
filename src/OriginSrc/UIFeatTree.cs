using System;
using System.Collections.Generic;

// Token: 0x02000154 RID: 340
public class UIFeatTree : UICanvasVertical
{
	// Token: 0x0600130C RID: 4876 RVA: 0x0005446D File Offset: 0x0005266D
	public UIFeatTree(FeatContainer featContainer)
	{
		this.setData(featContainer);
	}

	// Token: 0x0600130D RID: 4877 RVA: 0x0005447C File Offset: 0x0005267C
	public override List<UIElement> getScrollableElements()
	{
		return this.treeCollection.getScrollableElements();
	}

	// Token: 0x0600130E RID: 4878 RVA: 0x00054489 File Offset: 0x00052689
	public override void controllerScrollSidewaysRight()
	{
		this.treeCollection.controllerScrollSidewaysRight();
		base.clearCurrentSelectedButton();
	}

	// Token: 0x0600130F RID: 4879 RVA: 0x0005449C File Offset: 0x0005269C
	public override void controllerScrollSidewaysLeft()
	{
		this.treeCollection.controllerScrollSidewaysLeft();
		base.clearCurrentSelectedButton();
	}

	// Token: 0x06001310 RID: 4880 RVA: 0x000544B0 File Offset: 0x000526B0
	public void setData(FeatContainer featContainer)
	{
		UIFeatTree.pressedLeftFeat = null;
		UIFeatTree.pressedRightFeat = null;
		UIFeatTree.currentFeat = null;
		this.clearElements();
		this.treeCollection = new UIFeatTree.FeatTreeCollection(featContainer);
		this.add(this.treeCollection);
		this.pointsText = new UITextBlock(0, 0, 0, 30, C64Color.White, FontContainer.getMediumFont());
		this.pointsText.alignments = new UIElement.Alignments(UIElement.Alignments.VerticalAlignments.Top, UIElement.Alignments.HorizontalAlignments.Center);
		this.add(this.pointsText);
		this.alignments = new UIElement.Alignments(UIElement.Alignments.VerticalAlignments.Top, UIElement.Alignments.HorizontalAlignments.Center);
	}

	// Token: 0x06001311 RID: 4881 RVA: 0x00054532 File Offset: 0x00052732
	public static FeatContainer.Feat getCurrentFeat()
	{
		return UIFeatTree.currentFeat;
	}

	// Token: 0x06001312 RID: 4882 RVA: 0x00054539 File Offset: 0x00052739
	private static void setPressedLeftFeat(FeatContainer.Feat feat)
	{
		UIFeatTree.pressedLeftFeat = feat;
	}

	// Token: 0x06001313 RID: 4883 RVA: 0x00054541 File Offset: 0x00052741
	private static void setPressedRightFeat(FeatContainer.Feat feat)
	{
		UIFeatTree.pressedRightFeat = feat;
	}

	// Token: 0x06001314 RID: 4884 RVA: 0x00054549 File Offset: 0x00052749
	public void update(Character character)
	{
		UIFeatTree.setPressedLeftFeat(null);
		UIFeatTree.setPressedRightFeat(null);
		this.updateMouseInteraction();
		this.updatePressedLeftFeat(character);
		this.updatePressedRightFeat(character);
	}

	// Token: 0x06001315 RID: 4885 RVA: 0x0005456C File Offset: 0x0005276C
	private void updatePressedLeftFeat(Character character)
	{
		if (UIFeatTree.pressedLeftFeat != null)
		{
			if (UIFeatTree.currentFeat != UIFeatTree.pressedLeftFeat)
			{
				UIFeatTree.currentFeat = UIFeatTree.pressedLeftFeat;
				return;
			}
			if (character.getDevelopmentPoints() > 0)
			{
				int num = UIFeatTree.pressedLeftFeat.addPossibleRank();
				if (num != 0)
				{
					character.addDevelopmentPoints(-num);
					return;
				}
				PopUpControl.addPopUpOK(UIFeatTree.pressedLeftFeat.getLegalityMessage());
				return;
			}
			else
			{
				PopUpControl.addPopUpOK("You don't have any ranks to distribute. Level up to gain ranks!");
			}
		}
	}

	// Token: 0x06001316 RID: 4886 RVA: 0x000545D1 File Offset: 0x000527D1
	private void updatePressedRightFeat(Character character)
	{
		if (UIFeatTree.pressedRightFeat != null)
		{
			if (UIFeatTree.currentFeat != UIFeatTree.pressedRightFeat)
			{
				UIFeatTree.currentFeat = UIFeatTree.pressedRightFeat;
				return;
			}
			character.addDevelopmentPoints(UIFeatTree.pressedRightFeat.subtractPossibleRank());
		}
	}

	// Token: 0x06001317 RID: 4887 RVA: 0x00054601 File Offset: 0x00052801
	public void setPointsText(string text)
	{
		this.pointsText.setWidth(this.getWidth());
		this.pointsText.setContent(text);
		this.pointsText.setLetterShadowColor(C64Color.Black);
		this.alignElements();
	}

	// Token: 0x06001318 RID: 4888 RVA: 0x00054636 File Offset: 0x00052836
	public override void updateMouseInteraction()
	{
		this.treeCollection.updateMouseInteraction();
	}

	// Token: 0x040004A7 RID: 1191
	private UIFeatTree.FeatTreeCollection treeCollection;

	// Token: 0x040004A8 RID: 1192
	private UITextBlock pointsText;

	// Token: 0x040004A9 RID: 1193
	private static FeatContainer.Feat currentFeat;

	// Token: 0x040004AA RID: 1194
	private static FeatContainer.Feat pressedLeftFeat;

	// Token: 0x040004AB RID: 1195
	private static FeatContainer.Feat pressedRightFeat;

	// Token: 0x02000297 RID: 663
	private class FeatTreeCollection : UICanvasHorizontal
	{
		// Token: 0x06001ABC RID: 6844 RVA: 0x00073835 File Offset: 0x00071A35
		public FeatTreeCollection(FeatContainer featContainer)
		{
			this.stretchHorizontal = true;
			base.setDimensions(0, 0, 0, 180);
			this.buildTree(featContainer);
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x0007385C File Offset: 0x00071A5C
		public override void updateMouseInteraction()
		{
			foreach (UIElement uielement in base.getElements())
			{
				((UICanvas)uielement).updateMouseInteraction();
			}
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x000738B4 File Offset: 0x00071AB4
		private void buildTree(FeatContainer featContainer)
		{
			if (featContainer == null)
			{
				return;
			}
			foreach (FeatContainer.Feat feat in featContainer.getRootFeatList())
			{
				UIFeatTree.FeatTreeCollection.FeatTree element = new UIFeatTree.FeatTreeCollection.FeatTree(featContainer, feat);
				this.add(element);
			}
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x00073914 File Offset: 0x00071B14
		public override List<UIElement> getScrollableElements()
		{
			return this.getCurrentControllerSelectedFeatTree().getScrollableElements();
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x00073924 File Offset: 0x00071B24
		public override void controllerScrollSidewaysRight()
		{
			UIFeatTree.FeatTreeCollection.FeatTree currentControllerSelectedFeatTree = this.getCurrentControllerSelectedFeatTree();
			if (currentControllerSelectedFeatTree.canControllerScrollRight())
			{
				currentControllerSelectedFeatTree.controllerScrollSidewaysRight();
				return;
			}
			this.controllerScrollIndex++;
			if (this.controllerScrollIndex >= base.getElements().Count)
			{
				this.controllerScrollIndex = 0;
			}
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x00073970 File Offset: 0x00071B70
		public override void controllerScrollSidewaysLeft()
		{
			UIFeatTree.FeatTreeCollection.FeatTree currentControllerSelectedFeatTree = this.getCurrentControllerSelectedFeatTree();
			if (currentControllerSelectedFeatTree.canControllerScrollLeft())
			{
				currentControllerSelectedFeatTree.controllerScrollSidewaysLeft();
				return;
			}
			this.controllerScrollIndex--;
			if (this.controllerScrollIndex < 0)
			{
				this.controllerScrollIndex = base.getElements().Count - 1;
			}
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x000739BD File Offset: 0x00071BBD
		private UIFeatTree.FeatTreeCollection.FeatTree getCurrentControllerSelectedFeatTree()
		{
			return base.getElements()[this.controllerScrollIndex] as UIFeatTree.FeatTreeCollection.FeatTree;
		}

		// Token: 0x040009B7 RID: 2487
		private int controllerScrollIndex;

		// Token: 0x020003D6 RID: 982
		private class FeatTree : UICanvas
		{
			// Token: 0x06001D62 RID: 7522 RVA: 0x0007BF48 File Offset: 0x0007A148
			public FeatTree(FeatContainer featContainer, FeatContainer.Feat feat)
			{
				this.padding.left = 3;
				this.padding.right = 3;
				this.padding.top = 10;
				this.stretchHorizontal = true;
				UIFeatTree.FeatTreeCollection.Node node = new UIFeatTree.FeatTreeCollection.Node(feat);
				this.addNode(node);
				this.findAndAppendChildren(featContainer, feat);
				this.addArrows();
			}

			// Token: 0x06001D63 RID: 7523 RVA: 0x0007BFB0 File Offset: 0x0007A1B0
			public override List<UIElement> getScrollableElements()
			{
				List<UIElement> list = new List<UIElement>();
				foreach (UIElement uielement in base.getElements())
				{
					UIFeatTree.FeatTreeCollection.GridElement gridElement = (UIFeatTree.FeatTreeCollection.GridElement)uielement;
					if (gridElement.useableAsScrollableElement() && gridElement.getGridXPos() == this.currentColumnForControllers)
					{
						list.Add(gridElement);
					}
				}
				return list;
			}

			// Token: 0x06001D64 RID: 7524 RVA: 0x0007C028 File Offset: 0x0007A228
			public override bool canControllerScrollLeft()
			{
				return this.currentColumnForControllers > 0;
			}

			// Token: 0x06001D65 RID: 7525 RVA: 0x0007C034 File Offset: 0x0007A234
			public override bool canControllerScrollRight()
			{
				int num = 0;
				foreach (UIElement uielement in base.getElements())
				{
					UIFeatTree.FeatTreeCollection.GridElement gridElement = (UIFeatTree.FeatTreeCollection.GridElement)uielement;
					if (gridElement.getGridXPos() > num)
					{
						num = gridElement.getGridXPos();
					}
				}
				return this.currentColumnForControllers < num;
			}

			// Token: 0x06001D66 RID: 7526 RVA: 0x0007C0A0 File Offset: 0x0007A2A0
			public override void controllerScrollSidewaysRight()
			{
				if (this.canControllerScrollRight())
				{
					this.currentColumnForControllers++;
				}
			}

			// Token: 0x06001D67 RID: 7527 RVA: 0x0007C0B8 File Offset: 0x0007A2B8
			public override void controllerScrollSidewaysLeft()
			{
				if (this.canControllerScrollLeft())
				{
					this.currentColumnForControllers--;
				}
			}

			// Token: 0x06001D68 RID: 7528 RVA: 0x0007C0D0 File Offset: 0x0007A2D0
			private void findAndAppendChildren(FeatContainer featContainer, FeatContainer.Feat feat)
			{
				foreach (FeatContainer.Feat feat2 in featContainer.getChildFeats(feat))
				{
					this.appendNode(new UIFeatTree.FeatTreeCollection.Node(feat2), feat.getId());
					this.findAndAppendChildren(featContainer, feat2);
				}
			}

			// Token: 0x06001D69 RID: 7529 RVA: 0x0007C138 File Offset: 0x0007A338
			private void addNode(UIFeatTree.FeatTreeCollection.Node node)
			{
				if (this.nodeDictionary.ContainsKey(node.getId()))
				{
					return;
				}
				this.nodeDictionary.Add(node.getId(), node);
				this.add(node);
				this.alignElements();
			}

			// Token: 0x06001D6A RID: 7530 RVA: 0x0007C170 File Offset: 0x0007A370
			public override void updateMouseInteraction()
			{
				foreach (UIElement uielement in base.getElements())
				{
					uielement.updateMouseInteraction();
				}
			}

			// Token: 0x06001D6B RID: 7531 RVA: 0x0007C1C0 File Offset: 0x0007A3C0
			public void appendNode(UIFeatTree.FeatTreeCollection.Node child, string parentId)
			{
				if (this.nodeDictionary.ContainsKey(child.getId()))
				{
					return;
				}
				if (!this.nodeDictionary.ContainsKey(parentId))
				{
					return;
				}
				this.nodeDictionary[parentId].addChild(child);
				this.addNode(child);
			}

			// Token: 0x06001D6C RID: 7532 RVA: 0x0007C200 File Offset: 0x0007A400
			public void addArrows()
			{
				List<UIFeatTree.FeatTreeCollection.GridArrow> list = new List<UIFeatTree.FeatTreeCollection.GridArrow>();
				foreach (UIElement uielement in base.getElements())
				{
					UIFeatTree.FeatTreeCollection.GridArrow arrow = ((UIFeatTree.FeatTreeCollection.Node)uielement).getArrow();
					if (arrow != null)
					{
						list.Add(arrow);
					}
				}
				foreach (UIFeatTree.FeatTreeCollection.GridArrow item in list)
				{
					base.getElements().Add(item);
				}
			}

			// Token: 0x06001D6D RID: 7533 RVA: 0x0007C2AC File Offset: 0x0007A4AC
			public override void alignElements()
			{
				foreach (UIElement uielement in base.getElements())
				{
					UIFeatTree.FeatTreeCollection.GridElement gridElement = (UIFeatTree.FeatTreeCollection.GridElement)uielement;
					gridElement.setX(this.getX() + this.padding.left + gridElement.getGridPixelXPos());
					gridElement.setY(this.getY() - (this.padding.top + gridElement.getGridPixelYPos()));
				}
			}

			// Token: 0x06001D6E RID: 7534 RVA: 0x0007C33C File Offset: 0x0007A53C
			public override int getWidth()
			{
				int num = 0;
				foreach (UIElement uielement in base.getElements())
				{
					UIFeatTree.FeatTreeCollection.GridElement gridElement = (UIFeatTree.FeatTreeCollection.GridElement)uielement;
					if (gridElement.getGridXPos() + 1 > num)
					{
						num = gridElement.getGridXPos() + 1;
					}
				}
				return num * 25 + base.getHorizontalPadding();
			}

			// Token: 0x04000C68 RID: 3176
			private Dictionary<string, UIFeatTree.FeatTreeCollection.Node> nodeDictionary = new Dictionary<string, UIFeatTree.FeatTreeCollection.Node>();

			// Token: 0x04000C69 RID: 3177
			private int currentColumnForControllers;
		}

		// Token: 0x020003D7 RID: 983
		private abstract class GridElement : UIImage
		{
			// Token: 0x06001D6F RID: 7535 RVA: 0x0007C3B0 File Offset: 0x0007A5B0
			public virtual int getGridPixelXPos()
			{
				return this.xPos * 25;
			}

			// Token: 0x06001D70 RID: 7536 RVA: 0x0007C3BB File Offset: 0x0007A5BB
			public virtual int getGridPixelYPos()
			{
				return this.yPos * 34;
			}

			// Token: 0x06001D71 RID: 7537 RVA: 0x0007C3C6 File Offset: 0x0007A5C6
			public int getGridXPos()
			{
				return this.xPos;
			}

			// Token: 0x06001D72 RID: 7538 RVA: 0x0007C3CE File Offset: 0x0007A5CE
			public int getGridYPos()
			{
				return this.yPos;
			}

			// Token: 0x06001D73 RID: 7539 RVA: 0x0007C3D6 File Offset: 0x0007A5D6
			protected void setGridPosition(int xPos, int yPos)
			{
				this.xPos = xPos;
				this.yPos = yPos;
			}

			// Token: 0x04000C6A RID: 3178
			public const int GRID_WIDTH = 25;

			// Token: 0x04000C6B RID: 3179
			public const int GRID_HEIGHT = 34;

			// Token: 0x04000C6C RID: 3180
			private int xPos;

			// Token: 0x04000C6D RID: 3181
			private int yPos;
		}

		// Token: 0x020003D8 RID: 984
		private class GridArrow : UIFeatTree.FeatTreeCollection.GridElement
		{
			// Token: 0x06001D75 RID: 7541 RVA: 0x0007C3F0 File Offset: 0x0007A5F0
			public GridArrow(int arrows, int xPos, int yPos)
			{
				base.setGridPosition(xPos, yPos);
				string path;
				if (arrows == 1)
				{
					path = "Images/GUIIcons/FeatTree/Arrow1";
				}
				else if (arrows == 2)
				{
					path = "Images/GUIIcons/FeatTree/Arrow2";
				}
				else if (arrows == 3)
				{
					path = "Images/GUIIcons/FeatTree/Arrow3";
				}
				else if (arrows == 4)
				{
					path = "Images/GUIIcons/FeatTree/Arrow4";
				}
				else if (arrows == 5)
				{
					path = "Images/GUIIcons/FeatTree/Arrow5";
				}
				else
				{
					path = "Images/GUIIcons/FeatTree/Arrow6";
				}
				this.foregroundTexture = TextureTools.loadTextureData(path);
			}

			// Token: 0x06001D76 RID: 7542 RVA: 0x0007C45F File Offset: 0x0007A65F
			public override bool useableAsScrollableElement()
			{
				return false;
			}

			// Token: 0x06001D77 RID: 7543 RVA: 0x0007C462 File Offset: 0x0007A662
			public override int getGridPixelXPos()
			{
				return base.getGridPixelXPos() + 3;
			}

			// Token: 0x06001D78 RID: 7544 RVA: 0x0007C46C File Offset: 0x0007A66C
			public override int getGridPixelYPos()
			{
				return base.getGridPixelYPos() + 22;
			}
		}

		// Token: 0x020003D9 RID: 985
		private class Node : UIFeatTree.FeatTreeCollection.GridElement
		{
			// Token: 0x06001D79 RID: 7545 RVA: 0x0007C478 File Offset: 0x0007A678
			public Node(FeatContainer.Feat feat)
			{
				this.feat = feat;
				this.foregroundTexture = feat.getGridIcon();
				if (this.foregroundTexture == null)
				{
					this.foregroundTexture = TextureTools.loadTextureData("Images/GUIIcons/FeatIcons/FeatDefault");
				}
				base.setDimensions(0, 0, this.foregroundTexture.width, this.foregroundTexture.height);
				this.backgroundTexturePressed = UIFeatTree.FeatTreeCollection.Node.featBorderPressed;
				base.setAllowDoubleClick(false);
			}

			// Token: 0x06001D7A RID: 7546 RVA: 0x0007C4F1 File Offset: 0x0007A6F1
			public string getId()
			{
				return this.feat.getId();
			}

			// Token: 0x06001D7B RID: 7547 RVA: 0x0007C4FE File Offset: 0x0007A6FE
			public void addChild(UIFeatTree.FeatTreeCollection.Node child)
			{
				this.children.Add(child);
				child.setGridPosition(base.getGridXPos() + (this.children.Count - 1), base.getGridYPos() + 1);
			}

			// Token: 0x06001D7C RID: 7548 RVA: 0x0007C52E File Offset: 0x0007A72E
			public UIFeatTree.FeatTreeCollection.GridArrow getArrow()
			{
				if (this.children.Count == 0)
				{
					return null;
				}
				return new UIFeatTree.FeatTreeCollection.GridArrow(this.children.Count, base.getGridXPos(), base.getGridYPos());
			}

			// Token: 0x06001D7D RID: 7549 RVA: 0x0007C55B File Offset: 0x0007A75B
			public List<UIFeatTree.FeatTreeCollection.Node> getChildList()
			{
				return this.children;
			}

			// Token: 0x06001D7E RID: 7550 RVA: 0x0007C564 File Offset: 0x0007A764
			public override void draw(TextureTools.TextureData targetTexture)
			{
				if (UIFeatTree.getCurrentFeat() == this.feat)
				{
					this.backgroundTexture = UIFeatTree.FeatTreeCollection.Node.featBorderSelected;
					this.backgroundTextureHover = null;
				}
				else
				{
					this.backgroundTexture = UIFeatTree.FeatTreeCollection.Node.featBorder;
					this.backgroundTextureHover = UIFeatTree.FeatTreeCollection.Node.featBorderHighlight;
				}
				base.draw(targetTexture);
				this.drawTierPips(targetTexture);
				this.drawPadlock(targetTexture);
			}

			// Token: 0x06001D7F RID: 7551 RVA: 0x0007C5C0 File Offset: 0x0007A7C0
			private void drawTierPips(TextureTools.TextureData targetTexture)
			{
				int rank = this.feat.getRank();
				int num = 0;
				int num2 = 0;
				int[] featTierLevels = this.feat.getFeatTierLevels();
				int anchorX = this.getX() - 1;
				int num3 = this.getY() - 6;
				for (int i = 0; i < featTierLevels.Length; i++)
				{
					if (i >= 3)
					{
						return;
					}
					num += featTierLevels[i];
					for (int j = 0; j < featTierLevels[i]; j++)
					{
						if (num2 >= rank)
						{
							TextureTools.applyOverlay(targetTexture, UIFeatTree.FeatTreeCollection.Node.pipEmpty, anchorX, num3);
						}
						else if (rank < num)
						{
							TextureTools.applyOverlay(targetTexture, UIFeatTree.FeatTreeCollection.Node.pipBegun, anchorX, num3);
						}
						else
						{
							TextureTools.applyOverlay(targetTexture, UIFeatTree.FeatTreeCollection.Node.pipFull, anchorX, num3);
						}
						num3 -= 2;
						num2++;
					}
					num3 -= 2;
				}
			}

			// Token: 0x06001D80 RID: 7552 RVA: 0x0007C678 File Offset: 0x0007A878
			public bool isLegal()
			{
				return this.feat.isLegal();
			}

			// Token: 0x06001D81 RID: 7553 RVA: 0x0007C688 File Offset: 0x0007A888
			private void drawPadlock(TextureTools.TextureData targetTexture)
			{
				if (this.isLegal())
				{
					return;
				}
				int x = this.getX();
				int anchorY = this.getY() - 23;
				TextureTools.applyOverlay(targetTexture, UIFeatTree.FeatTreeCollection.Node.padLock, x, anchorY);
			}

			// Token: 0x06001D82 RID: 7554 RVA: 0x0007C6BC File Offset: 0x0007A8BC
			public override void updateMouseInteraction()
			{
				base.updateMouseInteraction();
				if (this.leftUp)
				{
					UIFeatTree.setPressedLeftFeat(this.feat);
					return;
				}
				if (this.rightUp)
				{
					UIFeatTree.setPressedRightFeat(this.feat);
				}
			}

			// Token: 0x04000C6E RID: 3182
			private List<UIFeatTree.FeatTreeCollection.Node> children = new List<UIFeatTree.FeatTreeCollection.Node>();

			// Token: 0x04000C6F RID: 3183
			private FeatContainer.Feat feat;

			// Token: 0x04000C70 RID: 3184
			private static TextureTools.TextureData pipEmpty = TextureTools.loadTextureData("Images/GUIIcons/FeatTree/PipEmpty");

			// Token: 0x04000C71 RID: 3185
			private static TextureTools.TextureData pipBegun = TextureTools.loadTextureData("Images/GUIIcons/FeatTree/PipBegun");

			// Token: 0x04000C72 RID: 3186
			private static TextureTools.TextureData pipFull = TextureTools.loadTextureData("Images/GUIIcons/FeatTree/PipFull");

			// Token: 0x04000C73 RID: 3187
			private static TextureTools.TextureData padLock = TextureTools.loadTextureData("Images/GUIIcons/FeatIcons/FeatLocked");

			// Token: 0x04000C74 RID: 3188
			private static TextureTools.TextureData featBorder = TextureTools.loadTextureData("Images/GUIIcons/FeatIcons/FeatBorder");

			// Token: 0x04000C75 RID: 3189
			private static TextureTools.TextureData featBorderSelected = TextureTools.loadTextureData("Images/GUIIcons/FeatIcons/FeatBorderSelected");

			// Token: 0x04000C76 RID: 3190
			private static TextureTools.TextureData featBorderHighlight = TextureTools.loadTextureData("Images/GUIIcons/FeatIcons/FeatBorderHighlight");

			// Token: 0x04000C77 RID: 3191
			private static TextureTools.TextureData featBorderPressed = TextureTools.loadTextureData("Images/GUIIcons/FeatIcons/FeatBorderPressed");
		}
	}
}
