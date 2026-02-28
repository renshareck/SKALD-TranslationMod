using System;
using System.Collections.Generic;

// Token: 0x02000107 RID: 263
public class SceneNode : SkaldBaseObject
{
	// Token: 0x060010AD RID: 4269 RVA: 0x0004BED4 File Offset: 0x0004A0D4
	public SceneNode(SKALDProjectData.Objects.SceneData.SceneContainer.SceneNodeContainer.SceneNodeData rawData)
	{
		this.setMainCharacter = rawData.setMainCharacter;
		base.processString(rawData.launchTrigger, null);
		this.clearIfDead = rawData.clearIfDead;
		this.setId(rawData.id);
		this.setName(base.processString(rawData.title, null));
		this.setDescription(base.processString(rawData.description, null));
		this.setImagePath(rawData.imagePath);
		if (this.getImagePath().Contains("CLEAR"))
		{
			this.clearImage = true;
			this.noImage = true;
		}
		else if (this.getImagePath() == "")
		{
			this.noImage = true;
		}
		this.setSceneOptions(rawData);
	}

	// Token: 0x060010AE RID: 4270 RVA: 0x0004BF9C File Offset: 0x0004A19C
	public SceneNode(string title, string _description, string image, string[] options, string[] targets, string[] exitTriggers) : base("simpleScene", title, _description)
	{
		if (image == "")
		{
			this.noImage = true;
		}
		else
		{
			this.setImagePath(image);
		}
		for (int i = 0; i < options.Length; i++)
		{
			this.exitList.Add(new SceneNode.Exit(options[i], targets[i], exitTriggers[i]));
		}
	}

	// Token: 0x060010AF RID: 4271 RVA: 0x0004C00C File Offset: 0x0004A20C
	public SceneNode(string title, string _description, string image = "") : base("simpleScene", title, _description)
	{
		if (image == "")
		{
			this.noImage = true;
		}
		else
		{
			this.setImagePath(image);
		}
		this.exitList.Add(new SceneNode.Exit());
	}

	// Token: 0x060010B0 RID: 4272 RVA: 0x0004C060 File Offset: 0x0004A260
	public void setSceneOptions(SKALDProjectData.Objects.SceneData.SceneContainer.SceneNodeContainer.SceneNodeData rawData)
	{
		foreach (SKALDProjectData.Objects.SceneData.SceneContainer.SceneNodeContainer.SceneNodeData.Exit exit in rawData.list)
		{
			string text = base.processString(exit.key, null);
			if (!(exit.key != "") || !(text.ToUpper() != "TRUE"))
			{
				this.exitList.Add(new SceneNode.Exit(exit));
			}
		}
	}

	// Token: 0x060010B1 RID: 4273 RVA: 0x0004C0F0 File Offset: 0x0004A2F0
	public List<string> getSceneOptions()
	{
		List<string> list = new List<string>();
		foreach (SceneNode.Exit exit in this.exitList)
		{
			list.Add(exit.getOption());
		}
		return list;
	}

	// Token: 0x060010B2 RID: 4274 RVA: 0x0004C150 File Offset: 0x0004A350
	public string getSceneTargets(int i)
	{
		try
		{
			return base.processString(this.exitList[i].target, null);
		}
		catch
		{
		}
		return "";
	}

	// Token: 0x060010B3 RID: 4275 RVA: 0x0004C194 File Offset: 0x0004A394
	public string getAlternateSceneTargets(int i)
	{
		try
		{
			return base.processString(this.exitList[i].alternateTarget, null);
		}
		catch
		{
		}
		return "";
	}

	// Token: 0x060010B4 RID: 4276 RVA: 0x0004C1D8 File Offset: 0x0004A3D8
	public string getExitTrigger(int i)
	{
		try
		{
			return this.exitList[i].exitTrigger;
		}
		catch
		{
		}
		return "";
	}

	// Token: 0x060010B5 RID: 4277 RVA: 0x0004C214 File Offset: 0x0004A414
	public bool isExitBranching(int i)
	{
		try
		{
			return this.exitList[i].isExitBranching();
		}
		catch
		{
		}
		return false;
	}

	// Token: 0x060010B6 RID: 4278 RVA: 0x0004C24C File Offset: 0x0004A44C
	public bool isExitBranchRandom(int i)
	{
		try
		{
			return this.exitList[i].randomRoll;
		}
		catch
		{
		}
		return false;
	}

	// Token: 0x060010B7 RID: 4279 RVA: 0x0004C284 File Offset: 0x0004A484
	public string getExitBranchAttribute(int i)
	{
		try
		{
			return this.exitList[i].getTestAttributeId();
		}
		catch (Exception obj)
		{
			MainControl.logError(obj);
		}
		return "";
	}

	// Token: 0x060010B8 RID: 4280 RVA: 0x0004C2C4 File Offset: 0x0004A4C4
	public override string getDescription()
	{
		return base.processString(base.getDescription(), null);
	}

	// Token: 0x060010B9 RID: 4281 RVA: 0x0004C2D4 File Offset: 0x0004A4D4
	public int getExitBranchDifficulty(int i)
	{
		try
		{
			return this.exitList[i].difficulty;
		}
		catch (Exception obj)
		{
			MainControl.logError(obj);
		}
		return 0;
	}

	// Token: 0x040003F9 RID: 1017
	private List<SceneNode.Exit> exitList = new List<SceneNode.Exit>();

	// Token: 0x040003FA RID: 1018
	public bool noImage;

	// Token: 0x040003FB RID: 1019
	public bool clearImage;

	// Token: 0x040003FC RID: 1020
	public bool setMainCharacter;

	// Token: 0x040003FD RID: 1021
	public bool clearIfDead;

	// Token: 0x02000258 RID: 600
	private class Exit
	{
		// Token: 0x060019C3 RID: 6595 RVA: 0x00070D67 File Offset: 0x0006EF67
		public Exit() : this("Leave", "{clearScene}", "")
		{
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x00070D80 File Offset: 0x0006EF80
		public Exit(string option, string target, string trigger)
		{
			this.option = option;
			this.target = target;
			this.exitTrigger = trigger;
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x00070DD4 File Offset: 0x0006EFD4
		public Exit(SKALDProjectData.Objects.SceneData.SceneContainer.SceneNodeContainer.SceneNodeData.Exit exit) : this(exit.option, exit.target, exit.exitTrigger)
		{
			this.alternateTarget = exit.alternateTarget;
			this.attribute = GameData.getAttributeRawData(exit.testAttribute);
			this.difficulty = exit.difficulty;
			this.randomRoll = exit.randomRoll;
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x00070E30 File Offset: 0x0006F030
		public string getOption()
		{
			string str = "";
			if (this.isExitBranching())
			{
				str = str + this.attribute.title.ToUpper() + " ";
				if (this.randomRoll)
				{
					str = str + "vs " + this.difficulty.ToString();
				}
				else if (!this.randomRoll)
				{
					str = str + (this.difficulty - 7).ToString() + "+";
				}
				str += ": ";
			}
			return str + this.option;
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x00070EC6 File Offset: 0x0006F0C6
		public string getTestAttributeId()
		{
			if (this.attribute == null)
			{
				return "";
			}
			return this.attribute.id;
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x00070EE1 File Offset: 0x0006F0E1
		public bool isExitBranching()
		{
			return this.target != "" && this.alternateTarget != "" && this.attribute != null;
		}

		// Token: 0x0400093F RID: 2367
		private string option = "An exit.";

		// Token: 0x04000940 RID: 2368
		public string target = "";

		// Token: 0x04000941 RID: 2369
		public string exitTrigger = "";

		// Token: 0x04000942 RID: 2370
		public string alternateTarget = "";

		// Token: 0x04000943 RID: 2371
		public int difficulty;

		// Token: 0x04000944 RID: 2372
		public bool randomRoll;

		// Token: 0x04000945 RID: 2373
		public SKALDProjectData.Objects.AttributeData.AttributeContainer.Attribute attribute;
	}
}
