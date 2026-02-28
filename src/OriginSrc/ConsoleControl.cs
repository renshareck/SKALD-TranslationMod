using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

// Token: 0x02000170 RID: 368
public static class ConsoleControl
{
	// Token: 0x060013F9 RID: 5113 RVA: 0x000580B0 File Offset: 0x000562B0
	public static void update()
	{
		if (ConsoleControl.commandBuffer == null)
		{
			ConsoleControl.loadCommandBuffer();
		}
		ConsoleControl.draw();
		if (SkaldIO.getPressedConsoleKey())
		{
			ConsoleControl.toggleOnOff();
		}
		if (ConsoleControl.console)
		{
			if (Input.GetKeyDown(KeyCode.Return) && ConsoleControl.consoleInput != "")
			{
				ConsoleControl.addToCommandBuffer(ConsoleControl.consoleInput);
				ConsoleControl.bufferIndex = -1;
				string text = TextParser.processString(ConsoleControl.consoleInput, null);
				ConsoleControl.addToOutputBuffer(text);
				if (text != "")
				{
					MainControl.log(text);
				}
				ConsoleControl.consoleInput = "";
				return;
			}
			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				ConsoleControl.bufferIndex++;
				if (ConsoleControl.bufferIndex >= ConsoleControl.commandBuffer.Count)
				{
					ConsoleControl.bufferIndex = -1;
				}
				if (ConsoleControl.bufferIndex == -1)
				{
					ConsoleControl.consoleInput = "";
					return;
				}
				if (ConsoleControl.commandBuffer.Count != 0)
				{
					ConsoleControl.consoleInput = ConsoleControl.commandBuffer[ConsoleControl.bufferIndex];
					return;
				}
			}
			else if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				ConsoleControl.bufferIndex--;
				if (ConsoleControl.bufferIndex == -1)
				{
					ConsoleControl.consoleInput = "";
					return;
				}
				if (ConsoleControl.bufferIndex < -1)
				{
					ConsoleControl.bufferIndex = ConsoleControl.commandBuffer.Count - 1;
					ConsoleControl.consoleInput = ConsoleControl.commandBuffer[ConsoleControl.bufferIndex];
					return;
				}
				ConsoleControl.consoleInput = ConsoleControl.commandBuffer[ConsoleControl.bufferIndex];
				return;
			}
			else
			{
				if (Input.GetKeyDown(KeyCode.Escape))
				{
					ConsoleControl.outputBuffer = "";
					ConsoleControl.introText = "";
					return;
				}
				if (Input.inputString == "\b")
				{
					if (ConsoleControl.consoleInput.Length != 0)
					{
						ConsoleControl.consoleInput = ConsoleControl.consoleInput.Substring(0, ConsoleControl.consoleInput.Length - 1);
						return;
					}
				}
				else if (!Input.GetKey(KeyCode.LeftControl) || !Input.GetKeyDown(KeyCode.V))
				{
					ConsoleControl.consoleInput += Input.inputString;
				}
			}
		}
	}

	// Token: 0x060013FA RID: 5114 RVA: 0x00058293 File Offset: 0x00056493
	private static void toggleOnOff()
	{
		ConsoleControl.setBackgroundTheme();
		if (ConsoleControl.console)
		{
			ConsoleControl.console = false;
			return;
		}
		ConsoleControl.console = true;
		SkaldIO.openVirtualKeyboard("Enter a console command.", "");
	}

	// Token: 0x060013FB RID: 5115 RVA: 0x000582C0 File Offset: 0x000564C0
	private static void loadCommandBuffer()
	{
		try
		{
			ConsoleControl.commandBuffer = File.ReadAllLines(ConsoleControl.commandBufferPath).ToList<string>();
		}
		catch
		{
			ConsoleControl.commandBuffer = new List<string>();
		}
	}

	// Token: 0x060013FC RID: 5116 RVA: 0x00058300 File Offset: 0x00056500
	private static void addToCommandBuffer(string s)
	{
		if (ConsoleControl.commandBuffer == null)
		{
			ConsoleControl.loadCommandBuffer();
		}
		if (ConsoleControl.commandBuffer.Count > 0 && ConsoleControl.commandBuffer[0] == s)
		{
			return;
		}
		ConsoleControl.commandBuffer.Insert(0, ConsoleControl.consoleInput);
		try
		{
			File.Delete(ConsoleControl.commandBufferPath);
			File.AppendAllLines(ConsoleControl.commandBufferPath, ConsoleControl.commandBuffer);
		}
		catch (Exception ex)
		{
			MainControl.logError(ex.Message);
		}
	}

	// Token: 0x060013FD RID: 5117 RVA: 0x00058384 File Offset: 0x00056584
	private static void addToOutputBuffer(string s)
	{
		ConsoleControl.outputBuffer = ConsoleControl.outputBuffer + s + "\n";
	}

	// Token: 0x060013FE RID: 5118 RVA: 0x0005839C File Offset: 0x0005659C
	private static void draw()
	{
		if (!ConsoleControl.console)
		{
			return;
		}
		if (ConsoleControl.GUIBackground == null)
		{
			ConsoleControl.setBackgroundTheme();
		}
		ConsoleControl.GUIBackground.copyToTarget(ConsoleControl.outputTexture);
		ConsoleControl.outputText.setContent(ConsoleControl.introText + ConsoleControl.outputBuffer + ">" + ConsoleControl.consoleInput);
		ConsoleControl.outputText.draw(ConsoleControl.outputTexture);
		ConsoleControl.guiControl.setBackground(ConsoleControl.outputTexture);
		ConsoleControl.guiControl.update();
	}

	// Token: 0x060013FF RID: 5119 RVA: 0x00058418 File Offset: 0x00056618
	public static void setBackgroundTheme()
	{
		string path = "Images/backgrounds/Console";
		Color32 color = C64Color.BlueLight;
		if (!GlobalSettings.getDisplaySettings().getClassicConsole())
		{
			path = "Images/backgrounds/Loading";
			color = C64Color.GrayLight;
		}
		ConsoleControl.GUIBackground = TextureTools.loadTextureData(path);
		if (ConsoleControl.GUIBackground != null)
		{
			ConsoleControl.outputTexture = new TextureTools.TextureData(ConsoleControl.GUIBackground.width, ConsoleControl.GUIBackground.height);
		}
		ConsoleControl.outputText = new UITextBlock(ConsoleControl.framePadding, ConsoleControl.GUIBackground.height - ConsoleControl.framePadding, ConsoleControl.GUIBackground.width - ConsoleControl.framePadding * 2, ConsoleControl.GUIBackground.height - ConsoleControl.framePadding * 2, color, FontContainer.getConsoleFont());
	}

	// Token: 0x040004FB RID: 1275
	private static string consoleInput = "";

	// Token: 0x040004FC RID: 1276
	private static List<string> commandBuffer;

	// Token: 0x040004FD RID: 1277
	private static int bufferIndex = -1;

	// Token: 0x040004FE RID: 1278
	private static string introText = "*** SKALD " + GameData.getVersionNumberAndEdition() + " ***\n\n Press TAB to exit or ESC to clear console.\nSome helpful commands:\n\n{healFullAll} - heals the party fully\n{addMoney|1000} - adds 1000 gold.\n{winCombat} - Wins any combat.\n\n";

	// Token: 0x040004FF RID: 1279
	private static string outputBuffer = "";

	// Token: 0x04000500 RID: 1280
	public static bool console = false;

	// Token: 0x04000501 RID: 1281
	private static string commandBufferPath = Application.persistentDataPath + "/commandBuffer.txt";

	// Token: 0x04000502 RID: 1282
	private static int framePadding = 36;

	// Token: 0x04000503 RID: 1283
	private static TextureTools.TextureData GUIBackground = null;

	// Token: 0x04000504 RID: 1284
	private static TextureTools.TextureData outputTexture = null;

	// Token: 0x04000505 RID: 1285
	private static UITextBlock outputText = null;

	// Token: 0x04000506 RID: 1286
	private static ConsoleGUIControl guiControl = new ConsoleGUIControl();
}
