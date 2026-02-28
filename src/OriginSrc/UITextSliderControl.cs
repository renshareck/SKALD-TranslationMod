using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200016A RID: 362
public class UITextSliderControl : UICanvasVertical
{
	// Token: 0x060013BA RID: 5050 RVA: 0x0005734B File Offset: 0x0005554B
	public UITextSliderControl()
	{
		this.setWidth(118);
		this.alignments = new UIElement.Alignments(UIElement.Alignments.VerticalAlignments.Top, UIElement.Alignments.HorizontalAlignments.Center);
		this.padding.top = 0;
	}

	// Token: 0x060013BB RID: 5051 RVA: 0x00057374 File Offset: 0x00055574
	public void update()
	{
		this.hoverButton = null;
		foreach (UIElement uielement in base.getElements())
		{
			UITextSliderControl.UITextSliderButton uitextSliderButton = (UITextSliderControl.UITextSliderButton)uielement;
			uitextSliderButton.update();
			if (uitextSliderButton.getHover())
			{
				this.hoverButton = uitextSliderButton;
			}
		}
	}

	// Token: 0x060013BC RID: 5052 RVA: 0x000573E4 File Offset: 0x000555E4
	public override void controllerScrollSidewaysRight()
	{
		foreach (UIElement uielement in base.getElements())
		{
			((UITextSliderControl.UITextSliderButton)uielement).controllerScrollSidewaysRight();
		}
	}

	// Token: 0x060013BD RID: 5053 RVA: 0x0005743C File Offset: 0x0005563C
	public override void controllerScrollSidewaysLeft()
	{
		foreach (UIElement uielement in base.getElements())
		{
			((UITextSliderControl.UITextSliderButton)uielement).controllerScrollSidewaysLeft();
		}
	}

	// Token: 0x060013BE RID: 5054 RVA: 0x00057494 File Offset: 0x00055694
	public override List<UIElement> getScrollableElements()
	{
		List<UIElement> list = new List<UIElement>();
		foreach (UIElement uielement in base.getScrollableElements())
		{
			if (uielement is UITextSliderControl.UITextSliderButton)
			{
				UITextSliderControl.UITextSliderButton uitextSliderButton = uielement as UITextSliderControl.UITextSliderButton;
				list.Add(uitextSliderButton.getControllerSelectedButton());
			}
			else
			{
				list.Add(uielement);
			}
		}
		return list;
	}

	// Token: 0x060013BF RID: 5055 RVA: 0x0005750C File Offset: 0x0005570C
	public override bool canControllerScrollLeft()
	{
		if (base.getElements().Count == 0)
		{
			return false;
		}
		UIElement uielement = base.getElements()[0];
		return uielement is UITextSliderControl.UITextSliderButton && (uielement as UITextSliderControl.UITextSliderButton).canControllerScrollLeft();
	}

	// Token: 0x060013C0 RID: 5056 RVA: 0x0005754C File Offset: 0x0005574C
	public override bool canControllerScrollRight()
	{
		if (base.getElements().Count == 0)
		{
			return false;
		}
		UIElement uielement = base.getElements()[0];
		return uielement is UITextSliderControl.UITextSliderButton && (uielement as UITextSliderControl.UITextSliderButton).canControllerScrollRight();
	}

	// Token: 0x060013C1 RID: 5057 RVA: 0x0005758A File Offset: 0x0005578A
	public string getHoverButtonDescription()
	{
		if (this.hoverButton == null)
		{
			return "";
		}
		return this.hoverButton.getDescription();
	}

	// Token: 0x060013C2 RID: 5058 RVA: 0x000575A8 File Offset: 0x000557A8
	public void randomize()
	{
		foreach (UIElement uielement in base.getElements())
		{
			((UITextSliderControl.UITextSliderButton)uielement).randomize();
		}
	}

	// Token: 0x060013C3 RID: 5059 RVA: 0x00057600 File Offset: 0x00055800
	public UITextSliderControl.UITextSliderButton createAndReturnSliderButton(string title, List<string> values, string prefix, int startingIndex)
	{
		UITextSliderControl.UITextSliderButton uitextSliderButton = new UITextSliderControl.UITextCustomSliderButton(title, values, this.getBaseWidth(), prefix, startingIndex);
		this.add(uitextSliderButton);
		return uitextSliderButton;
	}

	// Token: 0x060013C4 RID: 5060 RVA: 0x00057628 File Offset: 0x00055828
	public UITextSliderControl.UITextSliderButton createAndReturnSliderButton(GlobalSettings.SettingsCollection.Setting setting)
	{
		UITextSliderControl.UITextSliderButton uitextSliderButton = new UITextSliderControl.UITextSliderSettingsButton(setting, this.getBaseWidth());
		this.add(uitextSliderButton);
		return uitextSliderButton;
	}

	// Token: 0x060013C5 RID: 5061 RVA: 0x0005764C File Offset: 0x0005584C
	public UITextSliderControl.UITextSliderButton createCampActivitySliderButton(Character character)
	{
		UITextSliderControl.UITextSliderButton uitextSliderButton = new UITextSliderControl.UITextSliderCampActivityButton(character, this.getBaseWidth());
		this.add(uitextSliderButton);
		return uitextSliderButton;
	}

	// Token: 0x060013C6 RID: 5062 RVA: 0x00057670 File Offset: 0x00055870
	public UITextSliderControl.UITextSliderButton createAndReturnSliderButton(string title, List<SKALDProjectData.ApperanceElementContainers.ApperanceElement> objects, string prefix, int startingIndex)
	{
		List<string> list = new List<string>();
		foreach (SKALDProjectData.ApperanceElementContainers.ApperanceElement apperanceElement in objects)
		{
			list.Add(apperanceElement.title);
		}
		UITextSliderControl.UITextSliderButton uitextSliderButton = new UITextSliderControl.UITextCustomSliderButton(title, list, this.getBaseWidth(), prefix, startingIndex);
		this.add(uitextSliderButton);
		return uitextSliderButton;
	}

	// Token: 0x040004EA RID: 1258
	private UITextSliderControl.UITextSliderButton hoverButton;

	// Token: 0x020002AA RID: 682
	public abstract class UITextSliderButton : UICanvasVertical
	{
		// Token: 0x06001B03 RID: 6915 RVA: 0x00074D58 File Offset: 0x00072F58
		public UITextSliderButton(string title, int width)
		{
			this.stretchVertical = true;
			this.setWidth(width);
			this.padding.top = 1;
			this.headerTextBlock = new UITextBlock(0, 0, 10, 8, C64Color.HeaderColor, FontContainer.getTinyCapitalizedFont());
			this.headerTextBlock.stretchHorizontal = true;
			this.headerTextBlock.padding.bottom = 1;
			this.headerTextBlock.padding.left = 3;
			this.headerTextBlock.setLetterShadowColor(C64Color.SmallTextShadowColor);
			this.headerTextBlock.setContent(title);
			this.add(this.headerTextBlock);
			this.buttonRow = new UICanvasHorizontal();
			this.buttonRow.stretchHorizontal = true;
			this.buttonRow.stretchVertical = true;
			this.add(this.buttonRow);
			this.currentValueTextBlock = new UITextBlock(0, 0, 60, 8, C64Color.GrayLight);
			this.currentValueTextBlock.stretchHorizontal = true;
			this.currentValueTextBlock.foregroundColors.hoverColor = C64Color.White;
			this.currentValueTextBlock.setLetterShadowColor(C64Color.SmallTextShadowColor);
			this.currentValueTextBlock.padding.left = 10;
			this.currentValueTextBlock.padding.top = 0;
			this.buttonRow.add(this.currentValueTextBlock);
			this.minusButton = new UIImageButton("OptionsArrowLeft");
			this.minusButton.padding.right = 10;
			this.minusButton.setAllowDoubleClick(false);
			this.buttonRow.add(this.minusButton);
			this.plusButton = new UIImageButton("OptionsArrowRight");
			this.plusButton.padding.left = 10;
			this.plusButton.setAllowDoubleClick(false);
			this.buttonRow.add(this.plusButton);
			UIImage uiimage = new UIImage(0, 0, width, 1);
			uiimage.backgroundColors.mainColor = C64Color.Brown;
			this.add(uiimage);
			this.update();
		}

		// Token: 0x06001B04 RID: 6916
		public abstract int getPointer();

		// Token: 0x06001B05 RID: 6917 RVA: 0x00074F4A File Offset: 0x0007314A
		public virtual void update()
		{
			this.updateMouseInteraction();
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x00074F52 File Offset: 0x00073152
		public UIElement getControllerSelectedButton()
		{
			if (this.controllerSelectPlusButton)
			{
				return this.plusButton;
			}
			return this.minusButton;
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x00074F69 File Offset: 0x00073169
		public override void controllerScrollSidewaysRight()
		{
			this.controllerSelectPlusButton = true;
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x00074F72 File Offset: 0x00073172
		public override void controllerScrollSidewaysLeft()
		{
			this.controllerSelectPlusButton = false;
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x00074F7B File Offset: 0x0007317B
		public override bool canControllerScrollLeft()
		{
			return this.controllerSelectPlusButton;
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x00074F83 File Offset: 0x00073183
		public override bool canControllerScrollRight()
		{
			return !this.controllerSelectPlusButton;
		}

		// Token: 0x06001B0B RID: 6923
		public abstract string getValue();

		// Token: 0x06001B0C RID: 6924
		public abstract string getDescription();

		// Token: 0x06001B0D RID: 6925
		public abstract void randomize();

		// Token: 0x040009DD RID: 2525
		protected UITextBlock headerTextBlock;

		// Token: 0x040009DE RID: 2526
		protected UICanvasHorizontal buttonRow;

		// Token: 0x040009DF RID: 2527
		protected UIImageButton minusButton;

		// Token: 0x040009E0 RID: 2528
		protected UIImageButton plusButton;

		// Token: 0x040009E1 RID: 2529
		protected UITextBlock currentValueTextBlock;

		// Token: 0x040009E2 RID: 2530
		protected int pointer;

		// Token: 0x040009E3 RID: 2531
		private bool controllerSelectPlusButton = true;
	}

	// Token: 0x020002AB RID: 683
	public class UITextCustomSliderButton : UITextSliderControl.UITextSliderButton
	{
		// Token: 0x06001B0E RID: 6926 RVA: 0x00074F8E File Offset: 0x0007318E
		public UITextCustomSliderButton(string title, List<string> values, int width, string prefix, int startingIndex) : base(title, width)
		{
			this.values = values;
			this.prefix = prefix;
			this.pointer = startingIndex;
			this.boundPointer();
			this.update();
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x00074FBC File Offset: 0x000731BC
		public override void update()
		{
			base.update();
			this.minusButton.updateMouseInteraction();
			this.plusButton.updateMouseInteraction();
			if (this.plusButton.getLeftUp())
			{
				this.pointer++;
			}
			if (this.minusButton.getLeftUp())
			{
				this.pointer--;
			}
			this.boundPointer();
			if (this.values == null || this.values.Count == 0)
			{
				this.currentValueTextBlock.setContent("--Empty--");
			}
			else if (this.prefix == "")
			{
				this.currentValueTextBlock.setContent(this.values[this.pointer]);
			}
			else
			{
				this.currentValueTextBlock.setContent(this.prefix + " " + (this.pointer + 1).ToString());
			}
			this.alignElements();
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x000750A9 File Offset: 0x000732A9
		public override string getDescription()
		{
			return "";
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x000750B0 File Offset: 0x000732B0
		public override string getValue()
		{
			if (this.values == null || this.values.Count == 0)
			{
				return "";
			}
			this.boundPointer();
			return this.values[this.pointer];
		}

		// Token: 0x06001B12 RID: 6930 RVA: 0x000750E4 File Offset: 0x000732E4
		public override int getPointer()
		{
			this.boundPointer();
			return this.pointer;
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x000750F4 File Offset: 0x000732F4
		private void boundPointer()
		{
			if (this.values == null || this.values.Count == 0)
			{
				return;
			}
			if (this.pointer >= this.values.Count)
			{
				this.pointer = 0;
				return;
			}
			if (this.pointer < 0)
			{
				this.pointer = this.values.Count - 1;
			}
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x0007514E File Offset: 0x0007334E
		public override void randomize()
		{
			if (this.values == null || this.values.Count <= 1)
			{
				return;
			}
			this.pointer = Random.Range(0, this.values.Count);
			this.boundPointer();
		}

		// Token: 0x040009E4 RID: 2532
		private string prefix;

		// Token: 0x040009E5 RID: 2533
		private List<string> values;
	}

	// Token: 0x020002AC RID: 684
	public class UITextSliderSettingsButton : UITextSliderControl.UITextSliderButton
	{
		// Token: 0x06001B15 RID: 6933 RVA: 0x00075184 File Offset: 0x00073384
		public UITextSliderSettingsButton(GlobalSettings.SettingsCollection.Setting setting, int width) : base(setting.getName(), width)
		{
			this.setting = setting;
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x0007519C File Offset: 0x0007339C
		public override void update()
		{
			if (this.setting == null)
			{
				return;
			}
			base.update();
			this.minusButton.updateMouseInteraction();
			this.plusButton.updateMouseInteraction();
			if (this.plusButton.getLeftUp())
			{
				this.setting.incrementState(1);
			}
			if (this.minusButton.getLeftUp())
			{
				this.setting.incrementState(-1);
			}
			this.currentValueTextBlock.setContent(this.setting.printState());
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x00075216 File Offset: 0x00073416
		public override string getDescription()
		{
			return this.setting.getFullDescriptionAndHeader();
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x00075223 File Offset: 0x00073423
		public override string getValue()
		{
			return this.setting.printState();
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x00075230 File Offset: 0x00073430
		public override void randomize()
		{
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x00075232 File Offset: 0x00073432
		public override int getPointer()
		{
			return 0;
		}

		// Token: 0x040009E6 RID: 2534
		private GlobalSettings.SettingsCollection.Setting setting;
	}

	// Token: 0x020002AD RID: 685
	public class UITextSliderCampActivityButton : UITextSliderControl.UITextSliderButton
	{
		// Token: 0x06001B1B RID: 6939 RVA: 0x00075235 File Offset: 0x00073435
		public UITextSliderCampActivityButton(Character c, int width) : base(c.getName(), width)
		{
			this.character = c;
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x0007524C File Offset: 0x0007344C
		public override void update()
		{
			if (this.character == null)
			{
				return;
			}
			base.update();
			this.minusButton.updateMouseInteraction();
			this.plusButton.updateMouseInteraction();
			if (this.plusButton.getLeftUp())
			{
				this.character.cyclePreferredCampActivity(1);
			}
			if (this.minusButton.getLeftUp())
			{
				this.character.cyclePreferredCampActivity(-1);
			}
			this.currentValueTextBlock.setContent(this.character.getPreferredCampActivityName());
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x000752C6 File Offset: 0x000734C6
		public override string getDescription()
		{
			return this.character.getPreferredCampActivityDescription();
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x000752D3 File Offset: 0x000734D3
		public override string getValue()
		{
			return this.character.getPreferredCampActivityName();
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x000752E0 File Offset: 0x000734E0
		public override void randomize()
		{
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x000752E2 File Offset: 0x000734E2
		public override int getPointer()
		{
			return 0;
		}

		// Token: 0x040009E7 RID: 2535
		private Character character;
	}
}
