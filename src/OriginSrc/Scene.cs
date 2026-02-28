using System;
using System.Collections.Generic;

// Token: 0x02000106 RID: 262
public class Scene
{
	// Token: 0x0600109A RID: 4250 RVA: 0x0004BCC6 File Offset: 0x00049EC6
	public Scene(SceneNode startingNode)
	{
		this.setSceneNode(startingNode);
		this.setMainCharacter = this.sceneNode.setMainCharacter;
		this.noImage = this.sceneNode.noImage;
	}

	// Token: 0x0600109B RID: 4251 RVA: 0x0004BCF8 File Offset: 0x00049EF8
	public void setSceneNode(SceneNode node)
	{
		this.sceneNode = node;
		if (this.sceneNode.getImagePath() != "")
		{
			this.imagePath = this.sceneNode.getImagePath();
		}
		if (!this.clearImage)
		{
			this.clearImage = this.sceneNode.clearImage;
		}
		if (!this.setMainCharacter)
		{
			this.setMainCharacter = this.sceneNode.setMainCharacter;
		}
	}

	// Token: 0x0600109C RID: 4252 RVA: 0x0004BD66 File Offset: 0x00049F66
	public bool getSetMainCharacter()
	{
		return this.setMainCharacter;
	}

	// Token: 0x0600109D RID: 4253 RVA: 0x0004BD6E File Offset: 0x00049F6E
	public string getImagePath()
	{
		return this.imagePath;
	}

	// Token: 0x0600109E RID: 4254 RVA: 0x0004BD76 File Offset: 0x00049F76
	public string getName()
	{
		if (this.sceneNode == null)
		{
			return "";
		}
		return this.sceneNode.getName();
	}

	// Token: 0x0600109F RID: 4255 RVA: 0x0004BD91 File Offset: 0x00049F91
	public void toggleNoImage()
	{
		this.noImage = true;
		this.clearImage = true;
	}

	// Token: 0x060010A0 RID: 4256 RVA: 0x0004BDA1 File Offset: 0x00049FA1
	public bool shouldImageBeCleared()
	{
		return this.clearImage;
	}

	// Token: 0x060010A1 RID: 4257 RVA: 0x0004BDA9 File Offset: 0x00049FA9
	public bool showImage()
	{
		return !this.noImage;
	}

	// Token: 0x060010A2 RID: 4258 RVA: 0x0004BDB4 File Offset: 0x00049FB4
	public void setSceneOptions(SKALDProjectData.Objects.SceneData.SceneContainer.SceneNodeContainer.SceneNodeData rawData)
	{
		if (this.sceneNode == null)
		{
			return;
		}
		this.sceneNode.setSceneOptions(rawData);
	}

	// Token: 0x060010A3 RID: 4259 RVA: 0x0004BDCB File Offset: 0x00049FCB
	public List<string> getSceneOptions()
	{
		if (this.sceneNode == null)
		{
			return new List<string>();
		}
		return this.sceneNode.getSceneOptions();
	}

	// Token: 0x060010A4 RID: 4260 RVA: 0x0004BDE6 File Offset: 0x00049FE6
	public string getSceneTargets(int i)
	{
		if (this.sceneNode == null)
		{
			return "";
		}
		return this.sceneNode.getSceneTargets(i);
	}

	// Token: 0x060010A5 RID: 4261 RVA: 0x0004BE02 File Offset: 0x0004A002
	public string getAlternateSceneTargets(int i)
	{
		if (this.sceneNode == null)
		{
			return "";
		}
		return this.sceneNode.getAlternateSceneTargets(i);
	}

	// Token: 0x060010A6 RID: 4262 RVA: 0x0004BE1E File Offset: 0x0004A01E
	public string getExitTrigger(int i)
	{
		if (this.sceneNode == null)
		{
			return "";
		}
		return this.sceneNode.getExitTrigger(i);
	}

	// Token: 0x060010A7 RID: 4263 RVA: 0x0004BE3A File Offset: 0x0004A03A
	public bool isExitBranching(int i)
	{
		return this.sceneNode != null && this.sceneNode.isExitBranching(i);
	}

	// Token: 0x060010A8 RID: 4264 RVA: 0x0004BE52 File Offset: 0x0004A052
	public bool isExitBranchRandom(int i)
	{
		return this.sceneNode != null && this.sceneNode.isExitBranchRandom(i);
	}

	// Token: 0x060010A9 RID: 4265 RVA: 0x0004BE6A File Offset: 0x0004A06A
	public string getExitBranchAttribute(int i)
	{
		if (this.sceneNode == null)
		{
			return "";
		}
		return this.sceneNode.getExitBranchAttribute(i);
	}

	// Token: 0x060010AA RID: 4266 RVA: 0x0004BE86 File Offset: 0x0004A086
	public string getDescription()
	{
		if (this.sceneNode == null)
		{
			return "";
		}
		return this.sceneNode.getDescription();
	}

	// Token: 0x060010AB RID: 4267 RVA: 0x0004BEA1 File Offset: 0x0004A0A1
	public int getExitBranchDifficulty(int i)
	{
		if (this.sceneNode == null)
		{
			return 0;
		}
		return this.sceneNode.getExitBranchDifficulty(i);
	}

	// Token: 0x060010AC RID: 4268 RVA: 0x0004BEB9 File Offset: 0x0004A0B9
	public string getSceneId()
	{
		if (this.sceneNode == null)
		{
			return "";
		}
		return this.sceneNode.getId();
	}

	// Token: 0x040003F4 RID: 1012
	private SceneNode sceneNode;

	// Token: 0x040003F5 RID: 1013
	private string imagePath;

	// Token: 0x040003F6 RID: 1014
	private bool noImage;

	// Token: 0x040003F7 RID: 1015
	private bool clearImage;

	// Token: 0x040003F8 RID: 1016
	private bool setMainCharacter;
}
