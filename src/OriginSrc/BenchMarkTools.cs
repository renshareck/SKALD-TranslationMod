using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Token: 0x0200002E RID: 46
public static class BenchMarkTools
{
	// Token: 0x060004A7 RID: 1191 RVA: 0x00016534 File Offset: 0x00014734
	private static string startTimer(string message, int iterations)
	{
		BenchMarkTools.watch = Stopwatch.StartNew();
		string text = message + ": " + iterations.ToString() + " iterations.\n";
		MainControl.log(text);
		return text;
	}

	// Token: 0x060004A8 RID: 1192 RVA: 0x00016560 File Offset: 0x00014760
	private static string stopTimer(string message, int iterations)
	{
		if (BenchMarkTools.watch == null)
		{
			return "Error - watch not set.\n";
		}
		BenchMarkTools.watch.Stop();
		string text = message + ": \n" + "\tTotal MS elapsed: " + BenchMarkTools.watch.ElapsedMilliseconds.ToString() + "\n" + "\tMS per iteration: " + (BenchMarkTools.watch.ElapsedMilliseconds / (long)iterations).ToString() + "\n\n";
		MainControl.log(text);
		return text;
	}

	// Token: 0x060004A9 RID: 1193 RVA: 0x000165DA File Offset: 0x000147DA
	public static string benchmarkDrawPipeline(int iterations)
	{
		return BenchMarkTools.performIntBenchmark();
	}

	// Token: 0x060004AA RID: 1194 RVA: 0x000165E4 File Offset: 0x000147E4
	private static string performIntBenchmark()
	{
		int num = 1000000;
		int[] array = new int[]
		{
			1,
			2,
			3,
			4,
			5,
			1,
			2,
			3,
			4,
			5,
			1,
			2,
			3,
			4,
			5,
			1,
			2,
			3,
			4,
			5
		};
		string str = BenchMarkTools.startTimer("Starting int Benchmark 1", num);
		for (int i = 0; i < num; i++)
		{
			int[] array2 = new int[array.Length];
			for (int j = 0; j < array.Length; j++)
			{
				array2[j] = array[j];
			}
		}
		str += BenchMarkTools.stopTimer("Finished int Benchmark 1", num);
		Color[] array3 = new Color[]
		{
			Color.white,
			Color.black,
			Color.red,
			Color.blue,
			Color.yellow,
			Color.green,
			Color.cyan,
			Color.magenta,
			Color.clear,
			Color.gray,
			Color.white,
			Color.black,
			Color.red,
			Color.blue,
			Color.yellow,
			Color.green,
			Color.cyan,
			Color.magenta,
			Color.clear,
			Color.gray
		};
		Dictionary<int, Color> dictionary = new Dictionary<int, Color>();
		dictionary.Add(1, Color.white);
		dictionary.Add(2, Color.black);
		dictionary.Add(3, Color.red);
		dictionary.Add(4, Color.blue);
		dictionary.Add(5, Color.cyan);
		str += BenchMarkTools.startTimer("Starting int Benchmark 3", num);
		for (int k = 0; k < num; k++)
		{
			Color[] array4 = new Color[array3.Length];
			for (int l = 0; l < array.Length; l++)
			{
				array4[l] = dictionary[array[l]];
			}
		}
		str += BenchMarkTools.stopTimer("Finished int Benchmark 3", num);
		str += BenchMarkTools.startTimer("Starting int Benchmark 2", num);
		for (int m = 0; m < num; m++)
		{
			Color[] array5 = new Color[array3.Length];
			for (int n = 0; n < array3.Length; n++)
			{
				array5[n] = array3[n];
			}
		}
		return str + BenchMarkTools.stopTimer("Finished int Benchmark 2", num);
	}

	// Token: 0x060004AB RID: 1195 RVA: 0x00016870 File Offset: 0x00014A70
	private static string performBenchmarkLoop(string imagePath, int iterations, bool compress)
	{
		TextureTools.TextureData target = new TextureTools.TextureData(16, 16);
		TextureTools.TextureData subImageTextureData = TextureTools.getSubImageTextureData(0, imagePath);
		if (compress)
		{
			subImageTextureData.compress();
		}
		string str = BenchMarkTools.startTimer("Starting Graphics Benchmark - " + imagePath, iterations);
		for (int i = 0; i < iterations; i++)
		{
			if (subImageTextureData != null)
			{
				TextureTools.applyOverlay(target, subImageTextureData, 0, 0);
			}
			else
			{
				MainControl.logError("Error: Texture not found!");
			}
		}
		return str + BenchMarkTools.stopTimer("Finished Graphics Benchmark - " + imagePath, iterations);
	}

	// Token: 0x04000106 RID: 262
	private static Stopwatch watch;
}
