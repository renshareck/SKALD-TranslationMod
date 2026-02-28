using System;
using System.Collections.Generic;

// Token: 0x02000033 RID: 51
public static class CutSceneControl
{
	// Token: 0x06000549 RID: 1353 RVA: 0x000192A0 File Offset: 0x000174A0
	public static void draw(TextureTools.TextureData target)
	{
		if (!CutSceneControl.hasCutScene())
		{
			return;
		}
		CutSceneControl.CutScene cutScene = CutSceneControl.cutScenes[CutSceneControl.cutScenes.Count - 1];
		cutScene.draw(target);
		if (cutScene.isDead())
		{
			TextParser.processString(cutScene.getExitTrigger(), null);
			CutSceneControl.addAnimatedCutScene(cutScene.getNextScene());
			CutSceneControl.cutScenes.Remove(cutScene);
		}
	}

	// Token: 0x0600054A RID: 1354 RVA: 0x000192FF File Offset: 0x000174FF
	public static void addTextCard(string text)
	{
		CutSceneControl.cutScenes.Add(new CutSceneControl.CutSceneTextCard(text));
	}

	// Token: 0x0600054B RID: 1355 RVA: 0x00019311 File Offset: 0x00017511
	public static void addGameWinScreen()
	{
		CutSceneControl.cutScenes.Add(new CutSceneControl.CutSceneGameWin());
	}

	// Token: 0x0600054C RID: 1356 RVA: 0x00019322 File Offset: 0x00017522
	public static void clearCutScenes()
	{
		CutSceneControl.cutScenes.Clear();
	}

	// Token: 0x0600054D RID: 1357 RVA: 0x00019330 File Offset: 0x00017530
	public static void addAnimatedCutScene(string id)
	{
		if (id == null || id == "")
		{
			return;
		}
		SKALDProjectData.CutSceneDataContainers.CutScenesBase.CutScenesData cutSceneData = GameData.getCutSceneData(id);
		if (cutSceneData != null)
		{
			CutSceneControl.cutScenes.Add(new CutSceneControl.CutSceneAnimated(cutSceneData));
		}
	}

	// Token: 0x0600054E RID: 1358 RVA: 0x00019368 File Offset: 0x00017568
	public static bool hasCutScene()
	{
		return CutSceneControl.cutScenes.Count > 0;
	}

	// Token: 0x04000149 RID: 329
	private static List<CutSceneControl.CutScene> cutScenes = new List<CutSceneControl.CutScene>();

	// Token: 0x020001E0 RID: 480
	private abstract class CutScene
	{
		// Token: 0x0600171F RID: 5919
		public abstract void draw(TextureTools.TextureData target);

		// Token: 0x06001720 RID: 5920 RVA: 0x00066EA4 File Offset: 0x000650A4
		protected void update()
		{
			if (this.delay())
			{
				this.updateDelayTimer();
				return;
			}
			this.updateHangTimer();
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x00066EBB File Offset: 0x000650BB
		private void updateDelayTimer()
		{
			this.delayTimer.tick();
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x00066EC8 File Offset: 0x000650C8
		protected virtual void updateHangTimer()
		{
			this.hangTimeTimer.tick();
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x00066ED5 File Offset: 0x000650D5
		public bool isDead()
		{
			return this.hangTimeTimer.isTimerZero();
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x00066EE2 File Offset: 0x000650E2
		protected bool delay()
		{
			return !this.delayTimer.isTimerZero();
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x00066EF2 File Offset: 0x000650F2
		public virtual string getExitTrigger()
		{
			return "";
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x00066EF9 File Offset: 0x000650F9
		public virtual string getNextScene()
		{
			return "";
		}

		// Token: 0x0400076B RID: 1899
		protected CountDownClock hangTimeTimer = new CountDownClock(180, false);

		// Token: 0x0400076C RID: 1900
		protected CountDownClock delayTimer = new CountDownClock(30, false);

		// Token: 0x020002FC RID: 764
		protected abstract class CutsceneTextHeader : UITextBlock
		{
			// Token: 0x06001C1B RID: 7195 RVA: 0x00079A72 File Offset: 0x00077C72
			public CutsceneTextHeader(string text, int x, int y) : base(x, y, 480 - (x + 20), 200, C64Color.White, FontContainer.getMediumFont())
			{
				base.setContent(text);
				base.setLetterShadowColor(C64Color.Black);
				this.setReveal(false);
			}
		}
	}

	// Token: 0x020001E1 RID: 481
	private class CutSceneAnimated : CutSceneControl.CutScene
	{
		// Token: 0x06001728 RID: 5928 RVA: 0x00066F28 File Offset: 0x00065128
		public CutSceneAnimated(SKALDProjectData.CutSceneDataContainers.CutScenesBase.CutScenesData rawData)
		{
			MainControl.log("Created cutscene: " + rawData.id);
			this.delayLength = rawData.delay;
			this.duration = rawData.duration;
			this.parallaxControl.setBasePath(rawData.folderPath);
			this.hangTimeTimer = new CountDownClock(this.duration, false);
			this.delayTimer = new CountDownClock(this.delayLength, false);
			this.nextScene = rawData.nextCutScene;
			this.exitTrigger = rawData.exitTrigger;
			if (rawData.descriptionLine != "")
			{
				this.stringArray = rawData.descriptionLine.Split(new char[]
				{
					'*'
				});
				this.setHeader();
			}
			if (rawData.musicPath != "")
			{
				AudioControl.playMusic(rawData.musicPath);
			}
			foreach (BaseDataObject baseDataObject in rawData.getBaseList())
			{
				SKALDProjectData.CutSceneDataContainers.CutScenesBase.CutScenesData.ParallaxLayer rawData2 = (SKALDProjectData.CutSceneDataContainers.CutScenesBase.CutScenesData.ParallaxLayer)baseDataObject;
				this.parallaxControl.addLayer(rawData2);
			}
			this.parallaxControl.setParallaxSpeedRatio(rawData.parSpeedRatio);
			if (rawData.arrivalScroll)
			{
				this.parallaxControl.toggleArrivalScroll();
			}
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x0006709C File Offset: 0x0006529C
		private void setHeader()
		{
			if (!this.areThereMoreStringsToDraw())
			{
				return;
			}
			this.header = new CutSceneControl.CutSceneAnimated.Header(TextParser.processString(this.stringArray[this.stringIndex], null));
			this.stringIndex++;
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x000670D3 File Offset: 0x000652D3
		private bool areThereMoreStringsToDraw()
		{
			return this.stringArray != null && this.stringArray.Length != 0 && this.stringIndex < this.stringArray.Length;
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x000670F8 File Offset: 0x000652F8
		public override void draw(TextureTools.TextureData target)
		{
			base.update();
			if (base.delay())
			{
				return;
			}
			this.parallaxControl.drawAndGetOutput().applyOverlay(target);
			if (this.header == null)
			{
				return;
			}
			this.header.reveal();
			this.header.draw(target);
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x00067148 File Offset: 0x00065348
		protected override void updateHangTimer()
		{
			base.updateHangTimer();
			if (this.hangTimeTimer.isTimerZero() && this.areThereMoreStringsToDraw())
			{
				if (this.header != null)
				{
					this.hangTimeTimer = new CountDownClock(this.delayLength, true);
					this.header = null;
					return;
				}
				this.hangTimeTimer = new CountDownClock(this.duration, true);
				this.setHeader();
			}
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x000671AA File Offset: 0x000653AA
		public override string getExitTrigger()
		{
			return this.exitTrigger;
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x000671B2 File Offset: 0x000653B2
		public override string getNextScene()
		{
			return this.nextScene;
		}

		// Token: 0x0400076D RID: 1901
		private ParallaxControl parallaxControl = new ParallaxControl();

		// Token: 0x0400076E RID: 1902
		private CutSceneControl.CutSceneAnimated.Header header;

		// Token: 0x0400076F RID: 1903
		private string nextScene = "";

		// Token: 0x04000770 RID: 1904
		private string exitTrigger = "";

		// Token: 0x04000771 RID: 1905
		private string[] stringArray;

		// Token: 0x04000772 RID: 1906
		private int stringIndex;

		// Token: 0x04000773 RID: 1907
		private int delayLength;

		// Token: 0x04000774 RID: 1908
		private int duration;

		// Token: 0x020002FD RID: 765
		private class Header : CutSceneControl.CutScene.CutsceneTextHeader
		{
			// Token: 0x06001C1C RID: 7196 RVA: 0x00079AAE File Offset: 0x00077CAE
			public Header(string text) : base(text, 120, 40)
			{
			}
		}
	}

	// Token: 0x020001E2 RID: 482
	private class CutSceneGameWin : CutSceneControl.CutScene
	{
		// Token: 0x0600172F RID: 5935 RVA: 0x000671BC File Offset: 0x000653BC
		public CutSceneGameWin()
		{
			this.background = new UIImage("Images/Backgrounds/GameWin");
			this.background.setY(270);
			this.textBlock = new UITextBlock(32, 220, 416, 300, C64Color.White, FontContainer.getMediumFont());
			this.textBlock.setLetterShadowColor(C64Color.Black);
			this.textBlock.alignments.horizontalAlignment = UIElement.Alignments.HorizontalAlignments.Center;
			ScreenControl.setWindowed();
			Character mainCharacter = MainControl.getDataControl().getMainCharacter();
			string text = "Let it be known that after much toil and hardship, " + mainCharacter.getName() + " joins the illustrious ranks of the most holy order of the Persevering Blade.";
			text += "\n\nTo be one of us is to have spat in the face of Death and Despair. It is to be one who forges ahead where lesser souls stand trembling. Now, onward to glory!";
			text += "\n\n---";
			text = string.Concat(new string[]
			{
				text,
				"\n\nYou completed the game as a Level ",
				mainCharacter.getLevel().ToString(),
				" ",
				mainCharacter.getClassName(),
				" in ",
				MainControl.getDataControl().getDaysSinceStart(),
				" days."
			});
			text += "\n\nReport thine feat on Twitter to (a)SkaldRPG!\n\nAL";
			this.textBlock.setContent(text);
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x000672E3 File Offset: 0x000654E3
		public override void draw(TextureTools.TextureData target)
		{
			this.background.draw(target);
			this.textBlock.draw(target);
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x000672FD File Offset: 0x000654FD
		protected override void updateHangTimer()
		{
		}

		// Token: 0x04000775 RID: 1909
		private UITextBlock textBlock;

		// Token: 0x04000776 RID: 1910
		private UIImage background;
	}

	// Token: 0x020001E3 RID: 483
	private class CutSceneTextCard : CutSceneControl.CutScene
	{
		// Token: 0x06001732 RID: 5938 RVA: 0x000672FF File Offset: 0x000654FF
		public CutSceneTextCard(string text)
		{
			this.header = new CutSceneControl.CutSceneTextCard.Header(text);
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x00067313 File Offset: 0x00065513
		public override void draw(TextureTools.TextureData target)
		{
			base.update();
			if (base.delay())
			{
				return;
			}
			this.header.reveal();
			this.header.draw(target);
		}

		// Token: 0x04000777 RID: 1911
		private CutSceneControl.CutSceneTextCard.Header header;

		// Token: 0x020002FE RID: 766
		private class Header : CutSceneControl.CutScene.CutsceneTextHeader
		{
			// Token: 0x06001C1D RID: 7197 RVA: 0x00079ABB File Offset: 0x00077CBB
			public Header(string text) : base(text, 160, 135)
			{
			}
		}
	}
}
