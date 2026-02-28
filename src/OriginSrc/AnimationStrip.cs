using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000024 RID: 36
[Serializable]
public class AnimationStrip
{
	// Token: 0x0600040F RID: 1039 RVA: 0x00012EF3 File Offset: 0x000110F3
	public AnimationStrip(int[] _frames, float _fps, bool randomStartingFrame) : this(_frames, _fps, randomStartingFrame, false, true, false)
	{
	}

	// Token: 0x06000410 RID: 1040 RVA: 0x00012F04 File Offset: 0x00011104
	public AnimationStrip(int[] _frames, float _fps, bool randomStartingFrame, bool _nonLinear, bool _looping, bool _randomFrameLength)
	{
		this.frames = new int[1];
		this.fps = 6f;
		this.looping = true;
		this.endOfLoopTrigger = "";
		this.endOfLoopTarget = "";
		base..ctor();
		this.frames = _frames;
		this.nonLinear = _nonLinear;
		this.looping = _looping;
		this.randomFrameLength = _randomFrameLength;
		this.fps = _fps;
		this.setStartingConditions(randomStartingFrame);
	}

	// Token: 0x06000411 RID: 1041 RVA: 0x00012F78 File Offset: 0x00011178
	public AnimationStrip(SKALDProjectData.AnimationContainers.AnimationContainer.AnimationData animationData)
	{
		this.frames = new int[1];
		this.fps = 6f;
		this.looping = true;
		this.endOfLoopTrigger = "";
		this.endOfLoopTarget = "";
		base..ctor();
		if (animationData != null)
		{
			this.frames = this.parseFrames(animationData.frames);
			this.nonLinear = animationData.nonLinear;
			this.looping = animationData.looping;
			this.randomFrameLength = animationData.randomFrameLength;
			this.fps = animationData.FPS;
			this.endOfLoopTrigger = animationData.endOfAnimationTrigger;
			this.endOfLoopTarget = animationData.exitTarget;
			this.setStartingConditions(animationData.randomStartingFrame);
		}
	}

	// Token: 0x06000412 RID: 1042 RVA: 0x0001302C File Offset: 0x0001122C
	private void setStartingConditions(bool randomStartingFrame)
	{
		if (this.fps == 0f)
		{
			this.fps = 1f;
		}
		this.maxTick = 60f / this.fps;
		if (this.frames == null || this.frames.Length == 0)
		{
			this.frames = new int[1];
		}
		if (randomStartingFrame)
		{
			this.setRandomFrame();
		}
	}

	// Token: 0x06000413 RID: 1043 RVA: 0x0001308C File Offset: 0x0001128C
	private int[] parseFrames(string input)
	{
		if (input == null || input == "")
		{
			return new int[1];
		}
		string[] array = TextParser.getArray(input);
		List<int> list = new List<int>();
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			int item;
			if (int.TryParse(array2[i], out item))
			{
				list.Add(item);
			}
		}
		int[] array3 = new int[list.Count];
		for (int j = 0; j < list.Count; j++)
		{
			array3[j] = list[j];
		}
		return array3;
	}

	// Token: 0x06000414 RID: 1044 RVA: 0x0001310D File Offset: 0x0001130D
	internal bool willAnimationTerminate()
	{
		return !this.looping && !this.nonLinear && this.fps > 0f;
	}

	// Token: 0x06000415 RID: 1045 RVA: 0x00013130 File Offset: 0x00011330
	public int getCurrentFrame()
	{
		if (this.isDead() && !this.looping)
		{
			return this.frames[this.frames.Length - 1];
		}
		this.tick();
		int result = 0;
		try
		{
			result = this.frames[this.currentFrame];
		}
		catch
		{
		}
		return result;
	}

	// Token: 0x06000416 RID: 1046 RVA: 0x0001318C File Offset: 0x0001138C
	private void setRandomFrame()
	{
		this.currentFrame = Random.Range(0, this.frames.Length);
	}

	// Token: 0x06000417 RID: 1047 RVA: 0x000131A2 File Offset: 0x000113A2
	public bool isDead()
	{
		return this.dead;
	}

	// Token: 0x06000418 RID: 1048 RVA: 0x000131AA File Offset: 0x000113AA
	public bool isAnimationUpdated()
	{
		return this.animationUpdated;
	}

	// Token: 0x06000419 RID: 1049 RVA: 0x000131B4 File Offset: 0x000113B4
	private void tick()
	{
		if (this.isDead())
		{
			return;
		}
		this.currentTick += 1f;
		this.animationUpdated = false;
		if (this.currentTick > this.maxTick)
		{
			this.currentTick = 0f;
			if (this.randomFrameLength)
			{
				this.maxTick = 60f / this.fps * Random.Range(0.5f, 1.5f);
			}
			this.nextFrame();
		}
	}

	// Token: 0x0600041A RID: 1050 RVA: 0x0001322C File Offset: 0x0001142C
	private void nextFrame()
	{
		this.currentFrame++;
		if (this.currentFrame >= this.frames.Length && !this.looping)
		{
			this.dead = true;
			this.currentFrame = this.frames.Length - 1;
			TextParser.processString(this.endOfLoopTrigger, null);
			return;
		}
		if (this.nonLinear)
		{
			this.setRandomFrame();
		}
		else if (this.currentFrame < 0 || this.currentFrame >= this.frames.Length)
		{
			this.currentFrame = 0;
			TextParser.processString(this.endOfLoopTrigger, null);
		}
		this.animationUpdated = true;
	}

	// Token: 0x040000A9 RID: 169
	private int[] frames;

	// Token: 0x040000AA RID: 170
	private int currentFrame;

	// Token: 0x040000AB RID: 171
	private float fps;

	// Token: 0x040000AC RID: 172
	private float currentTick;

	// Token: 0x040000AD RID: 173
	private float maxTick;

	// Token: 0x040000AE RID: 174
	private bool dead;

	// Token: 0x040000AF RID: 175
	private bool animationUpdated;

	// Token: 0x040000B0 RID: 176
	private bool randomFrameLength;

	// Token: 0x040000B1 RID: 177
	private bool nonLinear;

	// Token: 0x040000B2 RID: 178
	private bool looping;

	// Token: 0x040000B3 RID: 179
	private string endOfLoopTrigger;

	// Token: 0x040000B4 RID: 180
	public string endOfLoopTarget;
}
