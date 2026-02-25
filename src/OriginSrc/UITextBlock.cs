using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

// Token: 0x02000169 RID: 361
public class UITextBlock : UICanvasVertical
{
	// Token: 0x0600138F RID: 5007 RVA: 0x000561CC File Offset: 0x000543CC
	public UITextBlock(int x, int y, int width, int height, Color32 color, Font font) : base(x, y, width, height)
	{
		this.foregroundColors.mainColor = color;
		this.font = font;
		this.stretchVertical = true;
	}

	// Token: 0x06001390 RID: 5008 RVA: 0x0005623B File Offset: 0x0005443B
	public UITextBlock(int x, int y, int width, int height, Color32 color) : this(x, y, width, height, color, FontContainer.getTinyFont())
	{
	}

	// Token: 0x06001391 RID: 5009 RVA: 0x0005624F File Offset: 0x0005444F
	public UITextBlock(int x, int y, int width, int height) : this(x, y, width, height, C64Color.SmallTextColor, FontContainer.getTinyFont())
	{
	}

	// Token: 0x06001392 RID: 5010 RVA: 0x00056266 File Offset: 0x00054466
	public UITextBlock(int width, int height) : this(0, 0, width, height, C64Color.SmallTextColor, FontContainer.getTinyFont())
	{
	}

	// Token: 0x06001393 RID: 5011 RVA: 0x0005627C File Offset: 0x0005447C
	public int getMaxLines()
	{
		return this.maxLines;
	}

	// Token: 0x06001394 RID: 5012 RVA: 0x00056284 File Offset: 0x00054484
	private void setMaxLines(int index)
	{
		this.maxLines = index;
		if (index > 0)
		{
			index--;
		}
	}

	// Token: 0x06001395 RID: 5013 RVA: 0x00056298 File Offset: 0x00054498
	public void setScrollIndex(int index)
	{
		if (this.scrollIndex == index)
		{
			return;
		}
		this.scrollIndex = index;
		if (this.scrollIndex < 0)
		{
			this.scrollIndex = 0;
		}
		else if (this.scrollIndex > this.getMaxLines())
		{
			this.scrollIndex = this.getMaxLines();
		}
		this.forceSetContent();
	}

	// Token: 0x06001396 RID: 5014 RVA: 0x000562E8 File Offset: 0x000544E8
	public override bool useableAsScrollableElement()
	{
		return !this.isEmpty();
	}

	// Token: 0x06001397 RID: 5015 RVA: 0x000562F3 File Offset: 0x000544F3
	public void setTabWidth(int newTabWidth)
	{
		this.tabWidth = newTabWidth;
	}

	// Token: 0x06001398 RID: 5016 RVA: 0x000562FC File Offset: 0x000544FC
	public void setEnforceHeight()
	{
		this.enforceHeight = true;
	}

	// Token: 0x06001399 RID: 5017 RVA: 0x00056305 File Offset: 0x00054505
	public void setSingleLine()
	{
		this.multiLine = false;
	}

	// Token: 0x0600139A RID: 5018 RVA: 0x0005630E File Offset: 0x0005450E
	public override void clearElements()
	{
		base.clearElements();
		this.toolTipWords.Clear();
		this.drawColors = null;
		this.currentTooltipWord = "";
		this.illuminatedImage = null;
		this.drawingQuote = false;
	}

	// Token: 0x0600139B RID: 5019 RVA: 0x00056341 File Offset: 0x00054541
	public void setFont(Font font)
	{
		this.font = font;
		this.forceSetContent();
	}

	// Token: 0x0600139C RID: 5020 RVA: 0x00056350 File Offset: 0x00054550
	public void setLetterMainColor(Color32 color)
	{
		this.foregroundColors.mainColor = color;
		foreach (UIElement uielement in base.getElements())
		{
			foreach (UIElement uielement2 in ((UICanvas)uielement).getElements())
			{
				foreach (UIElement uielement3 in ((UITextBlock.Word)uielement2).getElements())
				{
					((UITextBlock.Letter)uielement3).foregroundColors.mainColor = color;
				}
			}
		}
	}

	// Token: 0x0600139D RID: 5021 RVA: 0x00056438 File Offset: 0x00054638
	public void setHighlightQuotes(bool value)
	{
		this.highlightQuotes = value;
	}

	// Token: 0x0600139E RID: 5022 RVA: 0x00056444 File Offset: 0x00054644
	public void setLetterShadowColor(Color32 color)
	{
		this.foregroundColors.shadowMainColor = color;
		foreach (UIElement uielement in base.getElements())
		{
			foreach (UIElement uielement2 in ((UICanvas)uielement).getElements())
			{
				foreach (UIElement uielement3 in ((UITextBlock.Word)uielement2).getElements())
				{
					((UITextBlock.Letter)uielement3).foregroundColors.shadowMainColor = color;
				}
			}
		}
	}

	// Token: 0x0600139F RID: 5023 RVA: 0x0005652C File Offset: 0x0005472C
	public void setTooltips(ToolTipControl.ToolTipCategory toolTips)
	{
		this.toolTips = toolTips;
	}

	// Token: 0x060013A0 RID: 5024 RVA: 0x00056535 File Offset: 0x00054735
	public void setContent()
	{
		this.setContent(" ");
	}

	// Token: 0x060013A1 RID: 5025 RVA: 0x00056542 File Offset: 0x00054742
	public bool isEmpty()
	{
		return this.isContentEqual(" ") || this.isContentEqual("");
	}

	// Token: 0x060013A2 RID: 5026 RVA: 0x0005655E File Offset: 0x0005475E
	public void forceSetContent()
	{
		this.forceSetContent(this.content);
	}

	// Token: 0x060013A3 RID: 5027 RVA: 0x0005656C File Offset: 0x0005476C
	public void setTooltipHighlightColor(Color32 color)
	{
		this.toolTipHighlightColor = color;
	}

	// Token: 0x060013A4 RID: 5028 RVA: 0x00056575 File Offset: 0x00054775
	public void setUpperCaseTooltipsWords(bool upperCase)
	{
		this.uppercaseTooltipWords = upperCase;
	}

	// Token: 0x060013A5 RID: 5029 RVA: 0x0005657E File Offset: 0x0005477E
	public void forceSetContent(string input)
	{
		this.content = "";
		base.clearElements();
		this.setContent(input);
	}

	// Token: 0x060013A6 RID: 5030 RVA: 0x00056598 File Offset: 0x00054798
	public bool isContentEqual(string input)
	{
		return string.Equals(this.content, input);
	}

	// Token: 0x060013A7 RID: 5031 RVA: 0x000565A8 File Offset: 0x000547A8
	public void setContent(string input)
	{
		if (this.isContentEqual(input))
		{
			return;
		}
		if (input == null || input.Length == 0)
		{
			return;
		}
		this.clearElements();
		this.content = string.Copy(input);
		if (this.font == null)
		{
			MainControl.logError("No font set for text box:" + input);
			return;
		}
		if (this.illuminatedFont != null)
		{
			int subimageForChar = StringPrinter.getSubimageForChar(input[0]);
			if (subimageForChar <= 25)
			{
				this.illuminatedImage = new UICanvasVertical();
				input = input.Substring(1);
				this.illuminatedImage.backgroundTexture = TextureTools.getLetterSubImageTextureData(subimageForChar, this.illuminatedFont.getModelPath());
				this.illuminatedImage.padding.right = this.font.wordSpacing;
				this.add(this.illuminatedImage);
			}
		}
		input = this.preProcessString(input);
		input = this.identifyTooltipKeywords(input);
		foreach (string input2 in this.splitIntoParagraph(input))
		{
			this.parseParagraph(input2);
		}
		this.pruneLength();
		this.alignElements();
	}

	// Token: 0x060013A8 RID: 5032 RVA: 0x000566CC File Offset: 0x000548CC
	private string preProcessString(string input)
	{
		input = input.Replace('“', '"');
		input = input.Replace('”', '"');
		input = input.Replace("…", "...");
		return input;
	}

	// Token: 0x060013A9 RID: 5033 RVA: 0x00056700 File Offset: 0x00054900
	private string identifyTooltipKeywords(string input)
	{
		if (this.toolTips == null)
		{
			return input;
		}
		List<string> list = new List<string>();
		string text = this.identifyHeader(input);
		if (text != "" && this.toolTips.getKeywords().Contains(text))
		{
			list = this.toolTips.getToolTip(text).getKeywords();
		}
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		List<string> keywords = this.toolTips.getKeywords();
		for (int i = 0; i < keywords.Count; i++)
		{
			string text2 = keywords[i];
			if (text2 != null && !(text2 == "") && !(text2 == " "))
			{
				int num = input.IndexOf(text2);
				if (num != -1)
				{
					string text3 = "<tag>" + i.ToString() + "</tag>";
					while (num != -1)
					{
						int num2 = num - 1;
						int num3 = num + text2.Length;
						bool flag = num3 < input.Length && char.IsLetter(input[num3]);
						bool flag2 = num2 > 0 && char.IsLetter(input[num2]);
						if (!flag && !flag2)
						{
							input = input.Remove(num, text2.Length);
							input = input.Insert(num, text3);
							num3 = num + text3.Length;
						}
						num = input.IndexOf(text2, num3);
					}
					if (list.Contains(text2))
					{
						dictionary.Add(text3, text2);
					}
					else
					{
						dictionary.Add(text3, "<tag>" + text2 + "</tag>");
					}
				}
			}
		}
		foreach (KeyValuePair<string, string> keyValuePair in dictionary)
		{
			input = input.Replace(keyValuePair.Key, keyValuePair.Value);
		}
		return input;
	}

	// Token: 0x060013AA RID: 5034 RVA: 0x000568EC File Offset: 0x00054AEC
	private void getAllIndexesOfSubstring(string toolTip, string input, string tag)
	{
		int num = input.IndexOf(toolTip);
		if (num != -1)
		{
			while (num != -1)
			{
				int num2 = num - 1;
				int num3 = num + toolTip.Length;
				if (num3 < input.Length && char.IsLetter(input[num3]))
				{
					Debug.LogError(string.Concat(new string[]
					{
						toolTip,
						" is part of longer word: ",
						input[num3].ToString(),
						" - ",
						num.ToString()
					}));
				}
				else if (num2 > 0 && char.IsLetter(input[num2]))
				{
					Debug.LogError(string.Concat(new string[]
					{
						toolTip,
						" is part of longer word: ",
						input[num2].ToString(),
						" - ",
						num.ToString()
					}));
				}
				else
				{
					Debug.LogError("Found Tooltip: " + toolTip + " - " + num.ToString());
					input = input.Remove(num, toolTip.Length);
					input = input.Insert(num, tag);
					num3 = num + tag.Length;
				}
				num = input.IndexOf(toolTip, num3);
			}
		}
	}

	// Token: 0x060013AB RID: 5035 RVA: 0x00056A18 File Offset: 0x00054C18
	private string identifyHeader(string input)
	{
		int num = input.IndexOf(C64Color.HEADER_TAG_CONTENT);
		if (num == -1)
		{
			return "";
		}
		return input.Substring(num + C64Color.HEADER_TAG.Length - 1).Split(new char[]
		{
			'<'
		})[0];
	}

	// Token: 0x060013AC RID: 5036 RVA: 0x00056A64 File Offset: 0x00054C64
	private List<string> splitIntoParagraph(string input)
	{
		List<string> list = new List<string>();
		input = Regex.Replace(input, "\\r\\n?|\\n", "\n");
		while (input.Length > 0)
		{
			if (input.IndexOf("\n") == -1)
			{
				list.Add(input);
				break;
			}
			if (input.IndexOf("\n") == 0)
			{
				input = input.Remove(0, 1);
				while (input.IndexOf("\n") == 0)
				{
					input = input.Remove(0, 1);
					list.Add(" ");
				}
			}
			else
			{
				int num = input.IndexOf("\n");
				string text = input.Substring(0, num);
				text = text.Trim();
				input = input.Remove(0, num);
				list.Add(text);
			}
		}
		return list;
	}

	// Token: 0x060013AD RID: 5037 RVA: 0x00056B1C File Offset: 0x00054D1C
	private void pruneLength()
	{
		if (!this.multiLine)
		{
			while (base.getElements().Count > 1)
			{
				base.removeLastElement();
			}
		}
		this.setMaxLines(base.getElements().Count);
		if (this.scrollIndex > 0)
		{
			for (int i = 0; i < this.scrollIndex; i++)
			{
				if (base.getElements().Count > 0)
				{
					this.purgeTooltipsFromRemovedElement(base.getElements()[0]);
					base.getElements().RemoveAt(0);
				}
			}
		}
		while (base.getElements().Count > 0 && this.isTooLong())
		{
			base.getElements().RemoveAt(base.getElements().Count - 1);
		}
	}

	// Token: 0x060013AE RID: 5038 RVA: 0x00056BD0 File Offset: 0x00054DD0
	private void purgeTooltipsFromRemovedElement(UIElement elemenent)
	{
		if (!(elemenent is UICanvas))
		{
			return;
		}
		UICanvas uicanvas = elemenent as UICanvas;
		List<UITextBlock.Word> list = new List<UITextBlock.Word>();
		foreach (UITextBlock.Word item in this.toolTipWords)
		{
			if (uicanvas.getElements().Contains(item))
			{
				list.Add(item);
			}
		}
		foreach (UITextBlock.Word item2 in list)
		{
			this.toolTipWords.Remove(item2);
		}
	}

	// Token: 0x060013AF RID: 5039 RVA: 0x00056C8C File Offset: 0x00054E8C
	private bool isTooLong()
	{
		return this.enforceHeight && this.getHeight() > this.getBaseHeight();
	}

	// Token: 0x060013B0 RID: 5040 RVA: 0x00056CA8 File Offset: 0x00054EA8
	private void parseParagraph(string input)
	{   
        // 创建行排版对象，createLine()时会根据首字符装饰对象的宽和右badding改变自己的leftbdiing
		UICanvasHorizontal uicanvasHorizontal = this.createLine();

        // TODO: [DEBUG]检查下font里面是什么
        /*
        this.fontPath = fontData.modelPath;
		this.letterSpacing = fontData.letterSpacing;
		this.wordSpacing = fontData.wordSpacing;
		this.wordHeight = fontData.wordHeight;
        */
        // 创建词排版对象
		UITextBlock.Word word = new UITextBlock.Word(this.font);
		bool flag = false;
		int i = 0;
		while (i < input.Length)
		{   
            // 处理 < 标签符号
            // 标签内文本还是按照正常字符处理，只不过此时color已为push状态或是tag:flag
			if (input[i] == '<')
			{
				string text = input.Substring(i);
				int num = text.IndexOf(">") + 1;
				if (num == -1)
				{
					goto IL_202;    // 直接将当前字符不当作标签处理，直接作为字符显示
				}
				if (text.IndexOf(C64Color.HEADER_TAG_CONTENT) == 1) // <Header></Header>标签
				{
					this.pushColor(C64Color.HeaderColor);
					flag = true;
				}
				else if (text.IndexOf("/" + C64Color.HEADER_TAG_CONTENT) == 1)
				{
					this.popColor();
					flag = false;
				}
				else if (text.IndexOf("tag") == 1 && !flag)         // <tag></tag>标签
				{
					this.currentTooltipWord = text.Substring(num).Split(new char[]    // 设置currentTooltipWord，即 <tag>xxxx</tag> 中 xxxx 内容，这里虽然xxxx被存入currentTooltipWord，但是其还是要后面继续走一般字符处理流程（只在发现<tag>时设置currentTooltipWord）
					{
						'<'
					})[0];
					this.pushColor(this.toolTipHighlightColor);
				}
				else if (text.IndexOf("/tag") == 1 && !flag && this.currentTooltipWord != "")  // currentTooltipWord同时做TooltipWord_flag作用
				{
					this.currentTooltipWord = "";
					this.popColor();
				}
				else
				{
					string text2 = this.trimBrace(text.Substring(0, num));
					if (text2.IndexOf("color=".ToUpper()) == 1)             //<color=#xxxxxx></color>标签
					{
						int startIndex = text2.IndexOf("#".ToUpper());
						Color color;
						if (ColorUtility.TryParseHtmlString(text2.Substring(startIndex, 7), ref color))
						{
							this.pushColor(color);
						}
					}
					else if (text2.IndexOf("/color".ToUpper()) == 1)
					{
						this.popColor();
					}
				}
				if (num > 1)
				{
					i += num - 1;
				}
				if (i >= input.Length - 1)
				{
					if (this.shouldWeCreateNewLine(uicanvasHorizontal, word))
					{
						uicanvasHorizontal = this.createLine();
					}
					uicanvasHorizontal.add(word);
					break;
				}
			}
			else
			{
				if (input.IndexOf("\t") != i)                                   // 如果不为tab，则当作一般字符处理
				{
					goto IL_202;
				}
				uicanvasHorizontal.add(word);                                   // 将当前累计的词添加到行
				UITextBlock.Word word2 = new UITextBlock.Word(this.font);       // 创建一个tab空格占位词
				word2.setWidth(this.getPixelsToNextTab(uicanvasHorizontal));
				uicanvasHorizontal.add(word2);                                  // 将tab空格占位词添加到行
				word = new UITextBlock.Word(this.font);                         // 重置词排版对象
			}
			IL_458: // 处理下个字符
			i++;
			continue;
			IL_202: // 一般字符处理流程
			if (input[i] != ' ')
			{
				bool flag2 = false;     // 若发现引号时则高亮表示引用直至发现下一个引号
				if (input[i] == '"' && this.highlightQuotes)
				{
					if (this.drawingQuote)
					{
						flag2 = true;
					}
					this.drawingQuote = !this.drawingQuote;
				}
				UITextBlock.Letter letter;
                // 若为currentTooltipWord，则将当前字符转为大写 
				if (this.currentTooltipWord != "")
				{
					if (this.uppercaseTooltipWords)
					{
						letter = new UITextBlock.Letter(char.ToUpper(input[i]), this.getDrawColor(), this.foregroundColors.shadowMainColor, this.font);
					}
					else
					{
						letter = new UITextBlock.Letter(input[i], this.getDrawColor(), this.foregroundColors.shadowMainColor, this.font);
					}
				}
                // 若为引号，则对字符进行引用着色
				else if (this.drawingQuote || flag2)
				{
					letter = new UITextBlock.Letter(input[i], C64Color.SmallTextQuoteColor, this.foregroundColors.shadowMainColor, this.font);
				}
				else
				{
					letter = new UITextBlock.Letter(input[i], this.getDrawColor(), this.foregroundColors.shadowMainColor, this.font);
				}
                // 设置字符各项颜色
				letter.foregroundColors.hoverColor = this.foregroundColors.hoverColor;
				letter.foregroundColors.leftClickedColor = this.foregroundColors.leftClickedColor;
				letter.foregroundColors.outlineMainColor = this.foregroundColors.outlineMainColor;
				// 若 不允许多行 或 允许多行但词宽+行宽小于控件总宽，则当前词添加此字符
                // getWidthInLine()为当前词宽减去一个字母间距再减去一个词间距
                if (this.multiLine || (!this.multiLine && uicanvasHorizontal.getWidth() + word.getWidthInLine() <= this.getBaseWidth()))
				{
					word.add(letter);
				}

                //若当前字符还在<tag>...</tag>内部
                // 这部分代码写得比较乱
				if (this.currentTooltipWord != "")
				{
					word.highlightWord = this.currentTooltipWord;   // 当前词可能不完整，后续会依照highlightWord进行判定
					if (!this.toolTipWords.Contains(word))          // 在toolTipWords集合中添加当前toolTip词（关键是其中highlightWord属性）（不重复添加）
					{
						this.toolTipWords.Add(word);
					}
				}
			}
            // 若当前字符为空 或 当前字符为最后一个 或 当前词宽已大于控件宽，则需要判断是否结算换行（可认为这为词排版对象结束条件）
			if (input[i] == ' ' || i >= input.Length - 1 || (word.getWidthInLine() >= this.getBaseWidth() && !this.stretchHorizontal))
			{
				bool flag3 = this.shouldWeCreateNewLine(uicanvasHorizontal, word); // 加上此词后是否超过控件宽flag
				if (input[i] == ' ') // 若当前字符为空，则正常将字符添加词排版对象后
				{
					word.add(new UITextBlock.Letter(input[i], this.getDrawColor(), this.foregroundColors.shadowMainColor, this.font));
				}
				if (flag3) // 若超宽，则添加新行后添加此词
				{
					uicanvasHorizontal = this.createLine();
					uicanvasHorizontal.add(word);
				}
				else if (i == input.Length - 1) //若为最后一个词，则添加此词后创建新行
				{
					uicanvasHorizontal.add(word);
					uicanvasHorizontal = this.createLine();
				}
				else
				{   // 否则添加当前词至行（基本不会走到这里）
					uicanvasHorizontal.add(word);
				}
				word = new UITextBlock.Word(this.font); //清空词排版对象
				goto IL_458;
			}
			goto IL_458;
		}
		if (uicanvasHorizontal != null && uicanvasHorizontal.getElements().Count == 0)
		{
			base.getElements().Remove(uicanvasHorizontal);
		}
	}

	// Token: 0x060013B1 RID: 5041 RVA: 0x0005713A File Offset: 0x0005533A
	private int getPixelsToNextTab(UICanvasHorizontal line)
	{
		return this.tabWidth - line.getWidth() % this.tabWidth;
	}

	// Token: 0x060013B2 RID: 5042 RVA: 0x00057150 File Offset: 0x00055350
	private bool shouldWeCreateNewLine(UICanvasHorizontal line, UITextBlock.Word word)
	{
		return !this.stretchHorizontal && line.getWidth() + word.getWidthInLine() >= this.getBaseWidth() - base.getHorizontalPadding();
	}

	// Token: 0x060013B3 RID: 5043 RVA: 0x0005717B File Offset: 0x0005537B
	public override void draw(TextureTools.TextureData targetTexture)
	{
		this.getTooltipText();
		base.draw(targetTexture);
	}

	// Token: 0x060013B4 RID: 5044 RVA: 0x0005718C File Offset: 0x0005538C
	private void getTooltipText()
	{
		if (this.toolTips != null)
		{
			foreach (UITextBlock.Word word in this.toolTipWords)
			{
				word.updateMouseInteraction();
				if (word.getLeftUp())
				{
					ToolTipControl.ToolTipCategory.ToolTip toolTip = this.toolTips.getToolTip(word.highlightWord);
					if (toolTip != null)
					{
						ToolTipPrinter.setToolTip(toolTip.getFullDescription(), this.toolTips);
						break;
					}
				}
			}
		}
	}

	// Token: 0x060013B5 RID: 5045 RVA: 0x00057218 File Offset: 0x00055418
	private UICanvasHorizontal createLine()
	{
		UICanvasHorizontal uicanvasHorizontal = new UICanvasHorizontal();
		uicanvasHorizontal.stretchVertical = true;
		uicanvasHorizontal.stretchHorizontal = true;
		if (this.illuminatedImage != null && this.getHeight() - this.illuminatedImage.backgroundTexture.height < this.illuminatedImage.backgroundTexture.height)
		{
			UICanvasHorizontal uicanvasHorizontal2 = uicanvasHorizontal;
			uicanvasHorizontal2.padding.left = uicanvasHorizontal2.padding.left + (this.illuminatedImage.backgroundTexture.width + this.illuminatedImage.padding.right);
		}
		this.add(uicanvasHorizontal);
		return uicanvasHorizontal;
	}

	// Token: 0x060013B6 RID: 5046 RVA: 0x000572A2 File Offset: 0x000554A2
	private string trimBrace(string input)
	{
		input = input.Replace(" ", "");
		input = input.ToUpper();
		return input;
	}

	// Token: 0x060013B7 RID: 5047 RVA: 0x000572BF File Offset: 0x000554BF
	private Color32 getDrawColor()
	{
		if (this.drawColors == null || this.drawColors.Count == 0)
		{
			return this.foregroundColors.mainColor;
		}
		return this.drawColors[this.drawColors.Count - 1];
	}

	// Token: 0x060013B8 RID: 5048 RVA: 0x000572FA File Offset: 0x000554FA
	private void pushColor(Color32 color)
	{
		if (this.drawColors == null)
		{
			this.drawColors = new List<Color32>();
		}
		this.drawColors.Add(color);
	}

	// Token: 0x060013B9 RID: 5049 RVA: 0x0005731B File Offset: 0x0005551B
	private void popColor()
	{
		if (this.drawColors == null || this.drawColors.Count == 0)
		{
			return;
		}
		this.drawColors.RemoveAt(this.drawColors.Count - 1);
	}

	// Token: 0x040004D9 RID: 1241
	private List<Color32> drawColors;

	// Token: 0x040004DA RID: 1242
	private Font font;

	// Token: 0x040004DB RID: 1243
	public Font illuminatedFont;

	// Token: 0x040004DC RID: 1244
	private UICanvasVertical illuminatedImage;

	// Token: 0x040004DD RID: 1245
	private ToolTipControl.ToolTipCategory toolTips;

	// Token: 0x040004DE RID: 1246
	private List<UITextBlock.Word> toolTipWords = new List<UITextBlock.Word>();

	// Token: 0x040004DF RID: 1247
	private string currentTooltipWord = "";

	// Token: 0x040004E0 RID: 1248
	private string content = "";

	// Token: 0x040004E1 RID: 1249
	private int tabWidth = 48;

	// Token: 0x040004E2 RID: 1250
	private int maxLines;

	// Token: 0x040004E3 RID: 1251
	private int scrollIndex;

	// Token: 0x040004E4 RID: 1252
	private bool enforceHeight;

	// Token: 0x040004E5 RID: 1253
	private bool multiLine = true;

	// Token: 0x040004E6 RID: 1254
	private bool uppercaseTooltipWords;

	// Token: 0x040004E7 RID: 1255
	private bool highlightQuotes;

	// Token: 0x040004E8 RID: 1256
	private bool drawingQuote;

	// Token: 0x040004E9 RID: 1257
	private Color32 toolTipHighlightColor = C64Color.Green;

	// Token: 0x020002A8 RID: 680
	private class Letter : UIElement
	{
		// Token: 0x06001AFE RID: 6910 RVA: 0x00074C2E File Offset: 0x00072E2E
		public Letter(char character, Color32 color, Color32 shadowColor, Font font)
		{
			this.charIndex = StringPrinter.getSubimageForChar(character);
			this.setFont(font);
			this.stretchHorizontal = true;
			this.foregroundColors.mainColor = color;
			this.foregroundColors.shadowMainColor = shadowColor;
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x00074C69 File Offset: 0x00072E69
		public void setFont(Font font)
		{
			this.foregroundTexture = TextureTools.getLetterSubImageTextureData(this.charIndex, font.getModelPath());
			this.padding.right = font.letterSpacing;
			this.setHeight(font.wordHeight);
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x00074CA0 File Offset: 0x00072EA0
		protected override void drawForegroundTextureShadow(TextureTools.TextureData targetTexture, int x, int y)
		{
			if (this.foregroundColors.shadowMainColor != Color.clear)
			{
				if (this.dropDownShadow == null)
				{
					this.dropDownShadow = this.foregroundTexture.getDropDownShadow(this.foregroundColors.shadowMainColor);
				}
				TextureTools.applyOverlay(targetTexture, this.dropDownShadow, x - 1, y - 1);
			}
		}

		// Token: 0x040009D9 RID: 2521
		private TextureTools.TextureData dropDownShadow;

		// Token: 0x040009DA RID: 2522
		private int charIndex;
	}

	// Token: 0x020002A9 RID: 681
	private class Word : UICanvasHorizontal
	{
		// Token: 0x06001B01 RID: 6913 RVA: 0x00074CFF File Offset: 0x00072EFF
		public Word(Font font)
		{
			this.font = font;
			this.padding.right = font.wordSpacing;
			this.stretchHorizontal = true;
			this.stretchVertical = true;
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x00074D38 File Offset: 0x00072F38
		public int getWidthInLine()
		{
			return this.getWidth() - (this.font.letterSpacing + this.font.wordSpacing);
		}

		// Token: 0x040009DB RID: 2523
		public string highlightWord = "";

		// Token: 0x040009DC RID: 2524
		private Font font;
	}
}
