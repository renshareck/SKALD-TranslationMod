using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200013E RID: 318
public abstract class UIButtonControlBase : UICanvas
{
	// Token: 0x0600125A RID: 4698 RVA: 0x000515AC File Offset: 0x0004F7AC
	public UIButtonControlBase(int x, int y, int width, int height, int buttonNumber) : base(x, y, width, height)
	{
		this.buttonNumber = buttonNumber;
	}

	// Token: 0x0600125B RID: 4699
	protected abstract void populateButtons();

	// Token: 0x0600125C RID: 4700 RVA: 0x000515E0 File Offset: 0x0004F7E0
	public virtual void update()
	{
		this.clearButtonPressIndex();
		if (!this.isRevealed())
		{
			return;
		}
		int num = 0;
		foreach (UIElement uielement in this.getButtonsList())
		{
			uielement.updateMouseInteraction();
			if (uielement.getDoubleClicked())
			{
				this.buttonPressIndexDoubleClicked = num;
			}
			if (uielement.getHover())
			{
				this.hoverIndex = num;
			}
			if (uielement.getLeftUp() || this.testKeyboardPressIndex(num) || this.testControllerPressIndex(num))
			{
				this.buttonPressIndexLeft = num;
			}
			else if (uielement.getRightUp())
			{
				this.buttonPressIndexRight = num;
			}
			num++;
		}
	}

	// Token: 0x0600125D RID: 4701 RVA: 0x00051698 File Offset: 0x0004F898
	public override void setAlignment(UIElement.Alignments alignments)
	{
		this.getNestedCanvas().alignments = alignments;
	}

	// Token: 0x0600125E RID: 4702 RVA: 0x000516A8 File Offset: 0x0004F8A8
	public override void setPaddingLeft(int value)
	{
		UICanvas nestedCanvas = this.getNestedCanvas();
		if (nestedCanvas != null)
		{
			nestedCanvas.setPaddingLeft(value);
		}
	}

	// Token: 0x0600125F RID: 4703 RVA: 0x000516C8 File Offset: 0x0004F8C8
	public override void setPaddingTop(int value)
	{
		UICanvas nestedCanvas = this.getNestedCanvas();
		if (nestedCanvas != null)
		{
			nestedCanvas.setPaddingTop(value);
		}
	}

	// Token: 0x06001260 RID: 4704 RVA: 0x000516E8 File Offset: 0x0004F8E8
	protected List<UIElement> getButtonsList()
	{
		if (base.getElements().Count == 0)
		{
			return null;
		}
		List<UIElement> result;
		try
		{
			result = this.getNestedCanvas().getElements();
		}
		catch (Exception obj)
		{
			MainControl.logError(obj);
			result = null;
		}
		return result;
	}

	// Token: 0x06001261 RID: 4705 RVA: 0x00051730 File Offset: 0x0004F930
	public override string getHoverText()
	{
		foreach (UIElement uielement in this.getButtonsList())
		{
			if (uielement.getHoverText() != "")
			{
				return uielement.getHoverText();
			}
		}
		return "";
	}

	// Token: 0x06001262 RID: 4706 RVA: 0x000517A0 File Offset: 0x0004F9A0
	public override void setPosition(int x, int y)
	{
		base.setPosition(x, y);
		UICanvas nestedCanvas = this.getNestedCanvas();
		if (nestedCanvas != null)
		{
			nestedCanvas.setPosition(x, y);
		}
	}

	// Token: 0x06001263 RID: 4707 RVA: 0x000517C8 File Offset: 0x0004F9C8
	public override void setX(int x)
	{
		base.setX(x);
		UICanvas nestedCanvas = this.getNestedCanvas();
		if (nestedCanvas != null)
		{
			nestedCanvas.setX(x);
		}
	}

	// Token: 0x06001264 RID: 4708 RVA: 0x000517F0 File Offset: 0x0004F9F0
	public override List<UIElement> getScrollableElements()
	{
		UICanvas nestedCanvas = this.getNestedCanvas();
		if (nestedCanvas == null)
		{
			return new List<UIElement>();
		}
		return nestedCanvas.getScrollableElements();
	}

	// Token: 0x06001265 RID: 4709 RVA: 0x00051814 File Offset: 0x0004FA14
	public override void setY(int y)
	{
		base.setY(y);
		UICanvas nestedCanvas = this.getNestedCanvas();
		if (nestedCanvas != null)
		{
			nestedCanvas.setY(y);
		}
	}

	// Token: 0x06001266 RID: 4710 RVA: 0x0005183C File Offset: 0x0004FA3C
	public override void setWidth(int width)
	{
		base.setWidth(width);
		UICanvas nestedCanvas = this.getNestedCanvas();
		if (nestedCanvas != null)
		{
			nestedCanvas.setWidth(width);
		}
	}

	// Token: 0x06001267 RID: 4711 RVA: 0x00051864 File Offset: 0x0004FA64
	public override void setHeight(int height)
	{
		base.setHeight(height);
		UICanvas nestedCanvas = this.getNestedCanvas();
		if (nestedCanvas != null)
		{
			nestedCanvas.setHeight(height);
		}
	}

	// Token: 0x06001268 RID: 4712 RVA: 0x0005188C File Offset: 0x0004FA8C
	public override int getWidth()
	{
		UICanvas nestedCanvas = this.getNestedCanvas();
		if (nestedCanvas != null)
		{
			return nestedCanvas.getWidth();
		}
		return 0;
	}

	// Token: 0x06001269 RID: 4713 RVA: 0x000518AC File Offset: 0x0004FAAC
	public override int getHeight()
	{
		UICanvas nestedCanvas = this.getNestedCanvas();
		if (nestedCanvas != null)
		{
			return nestedCanvas.getHeight();
		}
		return 0;
	}

	// Token: 0x0600126A RID: 4714 RVA: 0x000518CC File Offset: 0x0004FACC
	public void setPosition()
	{
		UICanvas nestedCanvas = this.getNestedCanvas();
		if (nestedCanvas != null)
		{
			nestedCanvas.setPosition(this.getX(), this.getY());
		}
	}

	// Token: 0x0600126B RID: 4715 RVA: 0x000518F8 File Offset: 0x0004FAF8
	protected UICanvas getNestedCanvas()
	{
		UICanvas result;
		try
		{
			result = (base.getElements()[0] as UICanvas);
		}
		catch (Exception obj)
		{
			MainControl.logError(obj);
			result = null;
		}
		return result;
	}

	// Token: 0x0600126C RID: 4716 RVA: 0x00051934 File Offset: 0x0004FB34
	public void setStretchHorizontal(bool value)
	{
		UICanvas nestedCanvas = this.getNestedCanvas();
		if (nestedCanvas != null)
		{
			nestedCanvas.stretchHorizontal = value;
		}
	}

	// Token: 0x0600126D RID: 4717 RVA: 0x00051954 File Offset: 0x0004FB54
	public void setStretchVertical(bool value)
	{
		UICanvas nestedCanvas = this.getNestedCanvas();
		if (nestedCanvas != null)
		{
			nestedCanvas.stretchVertical = value;
		}
	}

	// Token: 0x0600126E RID: 4718 RVA: 0x00051974 File Offset: 0x0004FB74
	public bool hoveringOverButtons()
	{
		using (List<UIElement>.Enumerator enumerator = this.getButtonsList().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.getHover())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600126F RID: 4719 RVA: 0x000519D0 File Offset: 0x0004FBD0
	public void clearButtonPressIndex()
	{
		this.buttonPressIndexLeft = -1;
		this.buttonPressIndexRight = -1;
		this.hoverIndex = -1;
		this.buttonPressIndexDoubleClicked = -1;
	}

	// Token: 0x06001270 RID: 4720 RVA: 0x000519EE File Offset: 0x0004FBEE
	public int getHoverIndex()
	{
		return this.hoverIndex;
	}

	// Token: 0x06001271 RID: 4721 RVA: 0x000519F6 File Offset: 0x0004FBF6
	public int getButtonPressIndexLeft()
	{
		return this.buttonPressIndexLeft;
	}

	// Token: 0x06001272 RID: 4722 RVA: 0x000519FE File Offset: 0x0004FBFE
	public int getButtonPressIndexDoubleClicked()
	{
		return this.buttonPressIndexDoubleClicked;
	}

	// Token: 0x06001273 RID: 4723 RVA: 0x00051A06 File Offset: 0x0004FC06
	public int getButtonPressIndexRight()
	{
		return this.buttonPressIndexRight;
	}

	// Token: 0x06001274 RID: 4724 RVA: 0x00051A0E File Offset: 0x0004FC0E
	private bool testKeyboardPressIndex(int i)
	{
		return this.keyboardPressControl != null && this.keyboardPressControl.getButtonPressIndex(i);
	}

	// Token: 0x06001275 RID: 4725 RVA: 0x00051A26 File Offset: 0x0004FC26
	private bool testControllerPressIndex(int i)
	{
		return this.controllerPressControl != null && this.controllerPressControl.getButtonPressIndex(i);
	}

	// Token: 0x06001276 RID: 4726 RVA: 0x00051A3E File Offset: 0x0004FC3E
	protected void toggleKeyboardPressControl()
	{
		this.keyboardPressControl = new UIButtonControlBase.KeyboardPressControl();
	}

	// Token: 0x06001277 RID: 4727 RVA: 0x00051A4B File Offset: 0x0004FC4B
	public void toggleBAXYControllerPressControl()
	{
		this.controllerPressControl = new UIButtonControlBase.ControllerInputControlForButtonsBAXY();
	}

	// Token: 0x06001278 RID: 4728 RVA: 0x00051A58 File Offset: 0x0004FC58
	public void toggleABXYControllerPressControl()
	{
		this.controllerPressControl = new UIButtonControlBase.ControllerInputControlForButtonsABXY();
	}

	// Token: 0x06001279 RID: 4729 RVA: 0x00051A65 File Offset: 0x0004FC65
	public void toggleAXBYControllerPressControl()
	{
		this.controllerPressControl = new UIButtonControlBase.ControllerInputControlForButtonsAXBY();
	}

	// Token: 0x0600127A RID: 4730 RVA: 0x00051A72 File Offset: 0x0004FC72
	public void toggleAXBYButNoNumbersControllerPressControl()
	{
		this.controllerPressControl = new UIButtonControlBase.ControllerInputControlForButtonsAXBYButNoNumbers();
	}

	// Token: 0x0600127B RID: 4731 RVA: 0x00051A7F File Offset: 0x0004FC7F
	public bool isControllPressActivated()
	{
		return this.controllerPressControl != null;
	}

	// Token: 0x04000472 RID: 1138
	protected int buttonPressIndexLeft = -1;

	// Token: 0x04000473 RID: 1139
	protected int hoverIndex = -1;

	// Token: 0x04000474 RID: 1140
	protected int buttonPressIndexRight = -1;

	// Token: 0x04000475 RID: 1141
	protected int buttonPressIndexDoubleClicked = -1;

	// Token: 0x04000476 RID: 1142
	protected int buttonNumber;

	// Token: 0x04000477 RID: 1143
	protected UIButtonControlBase.KeyboardPressControl keyboardPressControl;

	// Token: 0x04000478 RID: 1144
	protected UIButtonControlBase.ControllerInputControlForButtons controllerPressControl;

	// Token: 0x02000285 RID: 645
	protected class KeyboardPressControl
	{
		// Token: 0x06001A94 RID: 6804 RVA: 0x00073361 File Offset: 0x00071561
		public bool getButtonPressIndex(int i)
		{
			return (this.holdDown && SkaldIO.getKeyHeldDown(this.keyCodes[i]) && (i == 1 || i == 3 || i == 4 || i == 5)) || SkaldIO.getKeyPressed(this.keyCodes[i]);
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x0007339C File Offset: 0x0007159C
		public KeyboardPressControl()
		{
			KeyCode[] array = new KeyCode[9];
			RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.F002D88D1605774CD2608C1D941E46CAD47C23D0F8E6530B87794DC9244A45AB).FieldHandle);
			this.keyCodes = array;
			base..ctor();
		}

		// Token: 0x0400099D RID: 2461
		private KeyCode[] keyCodes;

		// Token: 0x0400099E RID: 2462
		public bool holdDown;
	}

	// Token: 0x02000286 RID: 646
	protected abstract class ControllerInputControlForButtons
	{
		// Token: 0x06001A96 RID: 6806
		public abstract bool getButtonPressIndex(int i);

		// Token: 0x06001A97 RID: 6807
		public abstract string getButtonPrefix(int i);
	}

	// Token: 0x02000287 RID: 647
	protected class ControllerInputControlForButtonsBAXY : UIButtonControlBase.ControllerInputControlForButtons
	{
		// Token: 0x06001A99 RID: 6809 RVA: 0x000733C4 File Offset: 0x000715C4
		public override bool getButtonPressIndex(int i)
		{
			return (i == 0 && SkaldIO.getControllerButtonBPressed()) || (i == 1 && SkaldIO.getControllerButtonAPressed()) || (i == 2 && SkaldIO.getControllerButtonXPressed()) || (i == 3 && SkaldIO.getControllerButtonYPressed());
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x000733FA File Offset: 0x000715FA
		public override string getButtonPrefix(int i)
		{
			return SkaldOptionGlyphPrinter.printOptionGlyphBAXY(i);
		}
	}

	// Token: 0x02000288 RID: 648
	protected class ControllerInputControlForButtonsABXY : UIButtonControlBase.ControllerInputControlForButtons
	{
		// Token: 0x06001A9C RID: 6812 RVA: 0x0007340A File Offset: 0x0007160A
		public override bool getButtonPressIndex(int i)
		{
			return (i == 0 && SkaldIO.getControllerButtonAPressed()) || (i == 1 && SkaldIO.getControllerButtonBPressed()) || (i == 2 && SkaldIO.getControllerButtonXPressed()) || (i == 3 && SkaldIO.getControllerButtonYPressed());
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x00073440 File Offset: 0x00071640
		public override string getButtonPrefix(int i)
		{
			return SkaldOptionGlyphPrinter.printOptionGlyphABXY(i);
		}
	}

	// Token: 0x02000289 RID: 649
	protected class ControllerInputControlForButtonsAXBY : UIButtonControlBase.ControllerInputControlForButtons
	{
		// Token: 0x06001A9F RID: 6815 RVA: 0x00073450 File Offset: 0x00071650
		public override bool getButtonPressIndex(int i)
		{
			return (i == 0 && SkaldIO.getControllerButtonAPressed()) || (i == 1 && SkaldIO.getControllerButtonXPressed()) || (i == 2 && SkaldIO.getControllerButtonBPressed()) || (i == 3 && SkaldIO.getControllerButtonYPressed());
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x00073486 File Offset: 0x00071686
		public override string getButtonPrefix(int i)
		{
			return SkaldOptionGlyphPrinter.printOptionGlyphAXBY(i);
		}
	}

	// Token: 0x0200028A RID: 650
	protected class ControllerInputControlForButtonsAXBYButNoNumbers : UIButtonControlBase.ControllerInputControlForButtons
	{
		// Token: 0x06001AA2 RID: 6818 RVA: 0x00073496 File Offset: 0x00071696
		public override bool getButtonPressIndex(int i)
		{
			return (i == 0 && SkaldIO.getControllerButtonAPressed()) || (i == 1 && SkaldIO.getControllerButtonXPressed()) || (i == 2 && SkaldIO.getControllerButtonBPressed()) || (i == 3 && SkaldIO.getControllerButtonYPressed());
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x000734CC File Offset: 0x000716CC
		public override string getButtonPrefix(int i)
		{
			return SkaldOptionGlyphPrinter.printOptionGlyphAXBYButNoNumbers(i);
		}
	}

	// Token: 0x0200028B RID: 651
	protected class UITextButton : UITextBlock
	{
		// Token: 0x06001AA5 RID: 6821 RVA: 0x000734DC File Offset: 0x000716DC
		public UITextButton(int x, int y, int width, int height, Color32 color, Font font) : base(x, y, width, height, color, font)
		{
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x000734ED File Offset: 0x000716ED
		public UITextButton(int x, int y, int width, int height, Color32 color) : base(x, y, width, height, color)
		{
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x000734FC File Offset: 0x000716FC
		public UITextButton(int width, int height) : base(width, height)
		{
		}
	}

	// Token: 0x0200028C RID: 652
	public struct ButtonData
	{
		// Token: 0x06001AA8 RID: 6824 RVA: 0x00073506 File Offset: 0x00071706
		public ButtonData(TextureTools.TextureData texture, string hoverText)
		{
			this.texture = texture;
			this.hoverText = hoverText;
			this.count = 0;
		}

		// Token: 0x0400099F RID: 2463
		public TextureTools.TextureData texture;

		// Token: 0x040009A0 RID: 2464
		public string hoverText;

		// Token: 0x040009A1 RID: 2465
		public int count;
	}
}
