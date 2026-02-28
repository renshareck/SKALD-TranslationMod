using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using UnityEngine;

// Token: 0x020000F3 RID: 243
[Serializable]
public abstract class Prop : SkaldPhysicalObject, ISerializable
{
	// Token: 0x06000F88 RID: 3976 RVA: 0x000491D0 File Offset: 0x000473D0
	public Prop()
	{
		this.dynamicData = new Prop.PropSaveData(this.worldPosition, this.coreData, this.instanceData);
	}

	// Token: 0x06000F89 RID: 3977 RVA: 0x000491F8 File Offset: 0x000473F8
	public Prop(SKALDProjectData.PropContainers.Prop data) : base(data)
	{
		this.dynamicData = new Prop.PropSaveData(this.worldPosition, this.coreData, this.instanceData);
		this.dynamicData.impassable = data.impassable;
		this.dynamicData.animation = base.processString(data.animation, null);
		this.dynamicData.noVisiblity = data.visibility;
		this.dynamicData.light = data.light;
		this.dynamicData.modelFrame = data.modelFrame;
		this.dynamicData.drawFullModel = data.fullModel;
		this.dynamicData.apperanceId = data.apperance;
		this.dynamicData.hidden = data.hidden;
		this.dynamicData.hitPoints = this.getMaxHP();
		if (data.animationStrip != "")
		{
			this.getAnimationContainer().setBaseAnimation(data.animationStrip);
		}
		this.setFaction(data.faction);
	}

	// Token: 0x06000F8A RID: 3978 RVA: 0x000492F8 File Offset: 0x000474F8
	public void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("saveData", this.dynamicData, typeof(Prop.PropSaveData));
		if (this.colorContainer != null)
		{
			info.AddValue("colorContainer", this.colorContainer, typeof(Prop.ColorContainer));
		}
		if (this.animationContainer != null)
		{
			info.AddValue("animationContainer", this.animationContainer, typeof(BaseAnimationContainer));
		}
	}

	// Token: 0x06000F8B RID: 3979 RVA: 0x00049368 File Offset: 0x00047568
	public Prop(SerializationInfo info, StreamingContext context)
	{
		this.dynamicData = (Prop.PropSaveData)info.GetValue("saveData", typeof(Prop.PropSaveData));
		try
		{
			this.colorContainer = (Prop.ColorContainer)info.GetValue("colorContainer", typeof(Prop.ColorContainer));
		}
		catch
		{
		}
		try
		{
			this.animationContainer = (BaseAnimationContainer)info.GetValue("animationContainer", typeof(BaseAnimationContainer));
		}
		catch
		{
		}
		this.applySaveData(this.dynamicData);
	}

	// Token: 0x06000F8C RID: 3980 RVA: 0x00049410 File Offset: 0x00047610
	public override string getInspectDescription()
	{
		string text = base.getName();
		FactionControl.Faction faction = this.getFaction();
		if (this.isIllegal() && faction != null)
		{
			text = text + "\n\n[Owned: " + faction.getName() + "]";
		}
		return text;
	}

	// Token: 0x06000F8D RID: 3981 RVA: 0x0004944E File Offset: 0x0004764E
	public TextureTools.Sprite getOutline(int x, int y, Color32 color, TextureTools.TextureData input)
	{
		if (this.outlineDrawer == null)
		{
			this.outlineDrawer = new ImageOutlineDrawer();
		}
		return this.outlineDrawer.getOutline(x, y, color, input);
	}

	// Token: 0x06000F8E RID: 3982 RVA: 0x00049473 File Offset: 0x00047673
	private SKALDProjectData.Objects.ApperanceDataContainer.ApperanceData getApperanceData()
	{
		if (this.apperanceData == null)
		{
			this.apperanceData = GameData.getApperanceData(this.dynamicData.apperanceId);
		}
		return this.apperanceData;
	}

	// Token: 0x06000F8F RID: 3983 RVA: 0x00049499 File Offset: 0x00047699
	public bool hasBeenDug()
	{
		return this.dynamicData.dug;
	}

	// Token: 0x06000F90 RID: 3984 RVA: 0x000494A6 File Offset: 0x000476A6
	protected virtual string getDigTrigger()
	{
		return "";
	}

	// Token: 0x06000F91 RID: 3985 RVA: 0x000494AD File Offset: 0x000476AD
	public bool hasDigTrigger()
	{
		return this.getDigTrigger() != "";
	}

	// Token: 0x06000F92 RID: 3986 RVA: 0x000494BF File Offset: 0x000476BF
	public bool canDig()
	{
		return !this.hasBeenDug() && this.hasDigTrigger();
	}

	// Token: 0x06000F93 RID: 3987 RVA: 0x000494D6 File Offset: 0x000476D6
	public void fireDigTrigger()
	{
		if (this.hasBeenDug())
		{
			return;
		}
		base.processString(this.getDigTrigger(), null);
		this.dynamicData.dug = true;
	}

	// Token: 0x06000F94 RID: 3988 RVA: 0x000494FB File Offset: 0x000476FB
	public int getMaxHP()
	{
		return 5;
	}

	// Token: 0x06000F95 RID: 3989 RVA: 0x000494FE File Offset: 0x000476FE
	public int getCurrentHP()
	{
		return this.dynamicData.hitPoints;
	}

	// Token: 0x06000F96 RID: 3990 RVA: 0x0004950C File Offset: 0x0004770C
	private void setColorContainer()
	{
		SKALDProjectData.Objects.ApperanceDataContainer.ApperanceData apperanceData = this.getApperanceData();
		if (apperanceData != null)
		{
			this.colorContainer = new Prop.ColorContainer(apperanceData);
		}
	}

	// Token: 0x06000F97 RID: 3991 RVA: 0x0004952F File Offset: 0x0004772F
	private string getMainColor()
	{
		if (this.colorContainer == null)
		{
			this.setColorContainer();
		}
		if (this.colorContainer == null)
		{
			return "";
		}
		return this.colorContainer.getMainColor();
	}

	// Token: 0x06000F98 RID: 3992 RVA: 0x00049558 File Offset: 0x00047758
	private string getSecondaryColor()
	{
		if (this.colorContainer == null)
		{
			this.setColorContainer();
		}
		if (this.colorContainer == null)
		{
			return "";
		}
		return this.colorContainer.getSecondaryColor();
	}

	// Token: 0x06000F99 RID: 3993 RVA: 0x00049581 File Offset: 0x00047781
	private string getTertiaryColor()
	{
		if (this.colorContainer == null)
		{
			this.setColorContainer();
		}
		if (this.colorContainer == null)
		{
			return "";
		}
		return this.colorContainer.getTertiaryColor();
	}

	// Token: 0x06000F9A RID: 3994 RVA: 0x000495AA File Offset: 0x000477AA
	private string getSkinColor()
	{
		if (this.colorContainer == null)
		{
			this.setColorContainer();
		}
		if (this.colorContainer == null)
		{
			return "";
		}
		return this.colorContainer.getSkinColor();
	}

	// Token: 0x06000F9B RID: 3995 RVA: 0x000495D3 File Offset: 0x000477D3
	private string getHairColor()
	{
		if (this.colorContainer == null)
		{
			this.setColorContainer();
		}
		if (this.colorContainer == null)
		{
			return "";
		}
		return this.colorContainer.getHairColor();
	}

	// Token: 0x06000F9C RID: 3996 RVA: 0x000495FC File Offset: 0x000477FC
	public TextureTools.TextureData[,] getTileMatrix()
	{
		this.getModelTexture();
		if (this.tileMatrix == null || this.tileMatrix.GetLength(0) != this.getTileDrawWidth() || this.tileMatrix.GetLength(1) != this.getTileDrawHeight())
		{
			this.tileMatrix = new TextureTools.TextureData[this.getTileDrawWidth(), this.getTileDrawHeight()];
			for (int i = 0; i < this.getTileDrawWidth(); i++)
			{
				for (int j = 0; j < this.getTileDrawHeight(); j++)
				{
					this.tileMatrix[i, j] = new TextureTools.TextureData(16, 16);
				}
			}
		}
		this.drawTexture.cutIntoTiles(this.tileMatrix);
		return this.tileMatrix;
	}

	// Token: 0x06000F9D RID: 3997 RVA: 0x000496A8 File Offset: 0x000478A8
	public override TextureTools.TextureData getModelTexture()
	{
		if (this.drawTexture == null)
		{
			this.drawTexture = new TextureTools.TextureData();
		}
		int frame = 0;
		if (this.shouldBeAnimated())
		{
			frame = this.getFrame();
		}
		string text = this.buildBufferId(frame);
		if (TextureTools.propBufferContainsImage(text))
		{
			TextureTools.tryToGetPropBufferImage(text, this.drawTexture);
			return this.drawTexture;
		}
		TextureTools.TextureData textureData = null;
		if (this.shouldBeAnimated())
		{
			textureData = this.getAnimatedTexture(frame);
		}
		if (textureData == null)
		{
			textureData = this.getStillTexture();
		}
		if (this.colorSwap() && textureData != null)
		{
			textureData.paletteSwap(this.getSwapDictionary());
		}
		if (textureData != null)
		{
			textureData.copyToTarget(this.drawTexture);
			TextureTools.addToPropBuffer(text, textureData);
		}
		return this.drawTexture;
	}

	// Token: 0x06000F9E RID: 3998 RVA: 0x0004974B File Offset: 0x0004794B
	public bool shouldNotBeDrawn()
	{
		return this.getModelPath() == "" && this.getAnimationPath() == "";
	}

	// Token: 0x06000F9F RID: 3999 RVA: 0x00049774 File Offset: 0x00047974
	private string buildBufferId(int frame)
	{
		if (this.bufferIdBuilder == null)
		{
			this.bufferIdBuilder = new StringBuilder(128);
		}
		this.bufferIdBuilder.Clear();
		if (this.shouldBeAnimated())
		{
			this.bufferIdBuilder.Append(this.getAnimationPath());
		}
		else
		{
			this.bufferIdBuilder.Append(this.getModelPath());
		}
		this.bufferIdBuilder.Append("__");
		this.bufferIdBuilder.Append(frame);
		if (this.colorSwap())
		{
			this.bufferIdBuilder.Append(this.getMainColor());
			this.bufferIdBuilder.Append(this.getSecondaryColor());
			this.bufferIdBuilder.Append(this.getTertiaryColor());
			this.bufferIdBuilder.Append(this.getHairColor());
			this.bufferIdBuilder.Append(this.getSkinColor());
		}
		return this.bufferIdBuilder.ToString();
	}

	// Token: 0x06000FA0 RID: 4000 RVA: 0x00049860 File Offset: 0x00047A60
	private TextureTools.TextureData getStillTexture()
	{
		if (this.getModelPath() == "")
		{
			return null;
		}
		if (this.drawFullModel())
		{
			return TextureTools.loadTextureData(this.getBaseImagePath() + this.getModelPath());
		}
		return TextureTools.getSubImageTextureData(this.dynamicData.modelFrame, this.getBaseImagePath() + this.getModelPath());
	}

	// Token: 0x06000FA1 RID: 4001 RVA: 0x000498C4 File Offset: 0x00047AC4
	private Dictionary<Color32, Color32> getSwapDictionary()
	{
		if (this.swapDictionary == null)
		{
			this.swapDictionary = C64Color.getSwapDictionary(this.getSkinColor(), this.getHairColor(), this.getMainColor(), this.getSecondaryColor(), this.getTertiaryColor(), "", "", "", "");
		}
		return this.swapDictionary;
	}

	// Token: 0x06000FA2 RID: 4002 RVA: 0x0004991C File Offset: 0x00047B1C
	public virtual string getLoadOutId()
	{
		return "";
	}

	// Token: 0x06000FA3 RID: 4003 RVA: 0x00049923 File Offset: 0x00047B23
	private TextureTools.TextureData getAnimatedTexture(int frame)
	{
		return TextureTools.getSubImageTextureData(frame, this.getAnimationPath());
	}

	// Token: 0x06000FA4 RID: 4004 RVA: 0x00049934 File Offset: 0x00047B34
	public bool colorSwap()
	{
		SKALDProjectData.PropContainers.Prop prop = this.getRawData();
		return prop == null || prop.colorSwap;
	}

	// Token: 0x06000FA5 RID: 4005 RVA: 0x00049953 File Offset: 0x00047B53
	protected virtual string getBaseImagePath()
	{
		return "Images/Props/";
	}

	// Token: 0x06000FA6 RID: 4006 RVA: 0x0004995A File Offset: 0x00047B5A
	public bool drawFullModel()
	{
		return true;
	}

	// Token: 0x06000FA7 RID: 4007 RVA: 0x0004995D File Offset: 0x00047B5D
	protected SKALDProjectData.PropContainers.Prop getRawData()
	{
		return GameData.getPropRawData(this.getId());
	}

	// Token: 0x06000FA8 RID: 4008 RVA: 0x0004996A File Offset: 0x00047B6A
	public int getTileDrawWidth()
	{
		if (!this.drawFullModel())
		{
			return 1;
		}
		if (this.drawTexture == null)
		{
			this.getModelTexture();
		}
		return this.drawTexture.getTileWidth();
	}

	// Token: 0x06000FA9 RID: 4009 RVA: 0x00049990 File Offset: 0x00047B90
	public int getTileDrawHeight()
	{
		if (!this.drawFullModel())
		{
			return 1;
		}
		if (this.drawTexture == null)
		{
			this.getModelTexture();
		}
		return this.drawTexture.getTileHeight();
	}

	// Token: 0x06000FAA RID: 4010 RVA: 0x000499B8 File Offset: 0x00047BB8
	public int getTileHeight()
	{
		SKALDProjectData.PropContainers.Prop prop = this.getRawData();
		if (prop == null)
		{
			return 1;
		}
		return prop.height;
	}

	// Token: 0x06000FAB RID: 4011 RVA: 0x000499D8 File Offset: 0x00047BD8
	public string getLightModel()
	{
		SKALDProjectData.PropContainers.Prop prop = this.getRawData();
		if (prop == null || prop.lightModel == null || prop.lightModel == "")
		{
			return "Round2Yellow";
		}
		return prop.lightModel;
	}

	// Token: 0x06000FAC RID: 4012 RVA: 0x00049A18 File Offset: 0x00047C18
	public float getLightStrength()
	{
		if (this.getLight() == 0)
		{
			return 0f;
		}
		SKALDProjectData.PropContainers.Prop prop = this.getRawData();
		if (prop == null)
		{
			return 0f;
		}
		return prop.lightStrength;
	}

	// Token: 0x06000FAD RID: 4013 RVA: 0x00049A4C File Offset: 0x00047C4C
	public int getTileWidth()
	{
		SKALDProjectData.PropContainers.Prop prop = this.getRawData();
		if (prop == null)
		{
			return 1;
		}
		return prop.width;
	}

	// Token: 0x06000FAE RID: 4014 RVA: 0x00049A6C File Offset: 0x00047C6C
	public int getPixelXOffest()
	{
		SKALDProjectData.PropContainers.Prop prop = this.getRawData();
		if (prop == null)
		{
			return 1;
		}
		return prop.pixelXOffest;
	}

	// Token: 0x06000FAF RID: 4015 RVA: 0x00049A8C File Offset: 0x00047C8C
	public int getPixelYOffest()
	{
		SKALDProjectData.PropContainers.Prop prop = this.getRawData();
		if (prop == null)
		{
			return 1;
		}
		return prop.pixelYOffest;
	}

	// Token: 0x06000FB0 RID: 4016 RVA: 0x00049AAC File Offset: 0x00047CAC
	public bool preferForeground()
	{
		SKALDProjectData.PropContainers.Prop prop = this.getRawData();
		return prop == null || prop.preferForeground;
	}

	// Token: 0x06000FB1 RID: 4017 RVA: 0x00049ACC File Offset: 0x00047CCC
	public int getSpotDC()
	{
		SKALDProjectData.PropContainers.Prop prop = this.getRawData();
		if (prop == null)
		{
			return 0;
		}
		return prop.spotDC;
	}

	// Token: 0x06000FB2 RID: 4018 RVA: 0x00049AEB File Offset: 0x00047CEB
	public void clearHidden()
	{
		this.dynamicData.hidden = false;
		base.getVisualEffects().setForceHighlight(180);
		this.addVocalBark("I spotted something!");
	}

	// Token: 0x06000FB3 RID: 4019 RVA: 0x00049B15 File Offset: 0x00047D15
	public FactionControl.Faction getFaction()
	{
		return FactionControl.getFaction(this.dynamicData.faction);
	}

	// Token: 0x06000FB4 RID: 4020 RVA: 0x00049B28 File Offset: 0x00047D28
	public void setFaction(string factionId)
	{
		this.dynamicData.faction = factionId;
		FactionControl.Faction faction = this.getFaction();
		if (faction == null)
		{
			return;
		}
		this.dynamicData.apperanceId = faction.getApperancePackId();
	}

	// Token: 0x06000FB5 RID: 4021 RVA: 0x00049B60 File Offset: 0x00047D60
	public void makeFactionHostile()
	{
		if (!this.isIllegal())
		{
			return;
		}
		FactionControl.Faction faction = this.getFaction();
		if (faction == null)
		{
			return;
		}
		MainControl.getDataControl().makeFactionHostileIfSpotted(faction.getId());
	}

	// Token: 0x06000FB6 RID: 4022 RVA: 0x00049B94 File Offset: 0x00047D94
	protected bool isIllegal()
	{
		SKALDProjectData.PropContainers.Prop prop = this.getRawData();
		return prop != null && prop.illegal;
	}

	// Token: 0x06000FB7 RID: 4023 RVA: 0x00049BB3 File Offset: 0x00047DB3
	public virtual bool isPlayerInteractive()
	{
		return true;
	}

	// Token: 0x06000FB8 RID: 4024 RVA: 0x00049BB6 File Offset: 0x00047DB6
	public bool isHidden()
	{
		return this.dynamicData.hidden;
	}

	// Token: 0x06000FB9 RID: 4025 RVA: 0x00049BC3 File Offset: 0x00047DC3
	protected virtual string getFirstTimeTrigger()
	{
		return "";
	}

	// Token: 0x06000FBA RID: 4026 RVA: 0x00049BCA File Offset: 0x00047DCA
	protected virtual string getActivatedTrigger()
	{
		return "";
	}

	// Token: 0x06000FBB RID: 4027 RVA: 0x00049BD1 File Offset: 0x00047DD1
	public string addVocalBark(string bark)
	{
		return MainControl.getDataControl().addVocalBark(bark);
	}

	// Token: 0x06000FBC RID: 4028 RVA: 0x00049BDE File Offset: 0x00047DDE
	protected virtual string getDeactivatedTrigger()
	{
		return "";
	}

	// Token: 0x06000FBD RID: 4029 RVA: 0x00049BE5 File Offset: 0x00047DE5
	protected virtual bool canActivate()
	{
		return true;
	}

	// Token: 0x06000FBE RID: 4030 RVA: 0x00049BE8 File Offset: 0x00047DE8
	protected virtual bool canDeactivate()
	{
		return true;
	}

	// Token: 0x06000FBF RID: 4031 RVA: 0x00049BEB File Offset: 0x00047DEB
	public virtual void activate()
	{
		if (!this.dynamicData.activated && this.canActivate())
		{
			this.dynamicData.activated = true;
			this.processActivateTrigger();
		}
	}

	// Token: 0x06000FC0 RID: 4032 RVA: 0x00049C15 File Offset: 0x00047E15
	public virtual void deactivate()
	{
		if (this.dynamicData.activated && this.canDeactivate())
		{
			this.dynamicData.activated = false;
			this.processDeactivateTrigger();
		}
	}

	// Token: 0x06000FC1 RID: 4033 RVA: 0x00049C3F File Offset: 0x00047E3F
	public void toggle()
	{
		if (this.dynamicData.activated)
		{
			this.deactivate();
			return;
		}
		this.activate();
	}

	// Token: 0x06000FC2 RID: 4034 RVA: 0x00049C5B File Offset: 0x00047E5B
	public void setAnimation(string newAnimation)
	{
		this.dynamicData.animation = newAnimation;
	}

	// Token: 0x06000FC3 RID: 4035 RVA: 0x00049C69 File Offset: 0x00047E69
	public void setDynamicAnimation(string newAnimation)
	{
		this.getAnimationContainer().setDynamicAnimation(newAnimation);
	}

	// Token: 0x06000FC4 RID: 4036 RVA: 0x00049C77 File Offset: 0x00047E77
	public void setModel(string newModel)
	{
		this.instanceData.modelPath = newModel;
	}

	// Token: 0x06000FC5 RID: 4037 RVA: 0x00049C88 File Offset: 0x00047E88
	public void setLight(string light)
	{
		int num = -1;
		int.TryParse(light, out num);
		if (num != -1)
		{
			this.dynamicData.light = num;
		}
	}

	// Token: 0x06000FC6 RID: 4038 RVA: 0x00049CB0 File Offset: 0x00047EB0
	public void makePassable()
	{
		this.dynamicData.impassable = false;
	}

	// Token: 0x06000FC7 RID: 4039 RVA: 0x00049CBE File Offset: 0x00047EBE
	public void makeImpassable()
	{
		this.dynamicData.impassable = true;
	}

	// Token: 0x06000FC8 RID: 4040 RVA: 0x00049CCC File Offset: 0x00047ECC
	protected virtual string getTryEnterTrigger()
	{
		return "";
	}

	// Token: 0x06000FC9 RID: 4041 RVA: 0x00049CD3 File Offset: 0x00047ED3
	protected virtual string getCombatLaunchTrigger()
	{
		return "";
	}

	// Token: 0x06000FCA RID: 4042 RVA: 0x00049CDA File Offset: 0x00047EDA
	protected virtual string getVerbTrigger()
	{
		return "";
	}

	// Token: 0x06000FCB RID: 4043 RVA: 0x00049CE1 File Offset: 0x00047EE1
	protected virtual string getDeactivatedVerbTrigger()
	{
		return "";
	}

	// Token: 0x06000FCC RID: 4044 RVA: 0x00049CE8 File Offset: 0x00047EE8
	protected virtual string getActiveVerbTrigger()
	{
		return "";
	}

	// Token: 0x06000FCD RID: 4045 RVA: 0x00049CF0 File Offset: 0x00047EF0
	public bool isInteractive()
	{
		if (!this.isPlayerInteractive())
		{
			return false;
		}
		if (this.getVerbTrigger() != "")
		{
			return true;
		}
		if (this.getActiveVerbTrigger() != "")
		{
			return true;
		}
		if (this.getDeactivatedVerbTrigger() != "")
		{
			return true;
		}
		this.isContainer();
		return true;
	}

	// Token: 0x06000FCE RID: 4046 RVA: 0x00049D4C File Offset: 0x00047F4C
	public virtual bool shouldBeHighlighted()
	{
		if (this.isHidden())
		{
			return false;
		}
		SKALDProjectData.PropContainers.Prop prop = this.getRawData();
		return prop != null && !(prop.modelPath == "") && prop.highlight;
	}

	// Token: 0x06000FCF RID: 4047 RVA: 0x00049D89 File Offset: 0x00047F89
	public virtual Color32 getHighlightedColor()
	{
		return C64Color.Yellow;
	}

	// Token: 0x06000FD0 RID: 4048 RVA: 0x00049D90 File Offset: 0x00047F90
	public virtual bool shouldBeAnimated()
	{
		SKALDProjectData.PropContainers.Prop prop = this.getRawData();
		return prop != null && prop.animation != "";
	}

	// Token: 0x06000FD1 RID: 4049 RVA: 0x00049DBC File Offset: 0x00047FBC
	protected string getSpottedTrigger()
	{
		SKALDProjectData.PropContainers.Prop prop = this.getRawData();
		if (prop != null)
		{
			return prop.spottedTrigger;
		}
		return "";
	}

	// Token: 0x06000FD2 RID: 4050 RVA: 0x00049DDF File Offset: 0x00047FDF
	protected virtual string getEnterTrigger()
	{
		return "";
	}

	// Token: 0x06000FD3 RID: 4051 RVA: 0x00049DE6 File Offset: 0x00047FE6
	public string processEnterTrigger()
	{
		return base.processString(this.getEnterTrigger(), this);
	}

	// Token: 0x06000FD4 RID: 4052 RVA: 0x00049DF5 File Offset: 0x00047FF5
	public string processTryEnterTrigger()
	{
		return base.processString(this.getTryEnterTrigger(), this);
	}

	// Token: 0x06000FD5 RID: 4053 RVA: 0x00049E04 File Offset: 0x00048004
	public string processCombatLaunchTrigger()
	{
		return base.processString(this.getCombatLaunchTrigger(), this);
	}

	// Token: 0x06000FD6 RID: 4054 RVA: 0x00049E13 File Offset: 0x00048013
	private int getFrame()
	{
		if (this.getAnimationContainer().hasDynamicAnimation())
		{
			return this.getAnimationContainer().getDynamicAnimationFrame();
		}
		return this.getAnimationContainer().getBaseAnimation();
	}

	// Token: 0x06000FD7 RID: 4055 RVA: 0x00049E39 File Offset: 0x00048039
	public bool isActive()
	{
		return this.dynamicData.activated;
	}

	// Token: 0x06000FD8 RID: 4056 RVA: 0x00049E46 File Offset: 0x00048046
	public string processFirstTimeTrigger()
	{
		return base.processString(this.getFirstTimeTrigger(), this);
	}

	// Token: 0x06000FD9 RID: 4057 RVA: 0x00049E55 File Offset: 0x00048055
	public string processLeaveTrigger()
	{
		return base.processString(this.getLeaveTrigger(), this);
	}

	// Token: 0x06000FDA RID: 4058 RVA: 0x00049E64 File Offset: 0x00048064
	public string processSpottedTrigger()
	{
		return base.processString(this.getSpottedTrigger(), this);
	}

	// Token: 0x06000FDB RID: 4059 RVA: 0x00049E73 File Offset: 0x00048073
	private string processActivateTrigger()
	{
		return base.processString(this.getActivatedTrigger(), this);
	}

	// Token: 0x06000FDC RID: 4060 RVA: 0x00049E82 File Offset: 0x00048082
	public string processDeactivateTrigger()
	{
		return base.processString(this.getDeactivatedTrigger(), this);
	}

	// Token: 0x06000FDD RID: 4061 RVA: 0x00049E94 File Offset: 0x00048094
	protected string processVerbTrigger()
	{
		string text = "";
		text += base.processString(this.getVerbTrigger(), this);
		if (this.isActive())
		{
			text += base.processString(this.getActiveVerbTrigger(), this);
		}
		else
		{
			text += base.processString(this.getDeactivatedVerbTrigger(), this);
		}
		return text;
	}

	// Token: 0x06000FDE RID: 4062 RVA: 0x00049EEE File Offset: 0x000480EE
	public virtual string interactWithProp()
	{
		if (this.isHidden())
		{
			return "";
		}
		return this.processVerbTrigger();
	}

	// Token: 0x06000FDF RID: 4063 RVA: 0x00049F04 File Offset: 0x00048104
	protected virtual string getLeaveTrigger()
	{
		return "";
	}

	// Token: 0x06000FE0 RID: 4064 RVA: 0x00049F0C File Offset: 0x0004810C
	private string getAnimationPath()
	{
		string animation = this.dynamicData.animation;
		if (animation == "")
		{
			return animation;
		}
		return "Images/Animations/" + animation;
	}

	// Token: 0x06000FE1 RID: 4065 RVA: 0x00049F3F File Offset: 0x0004813F
	public void setVerb(string verb)
	{
		this.dynamicData.verb = verb;
	}

	// Token: 0x06000FE2 RID: 4066 RVA: 0x00049F4D File Offset: 0x0004814D
	public virtual string getVerb()
	{
		return this.dynamicData.verb;
	}

	// Token: 0x06000FE3 RID: 4067 RVA: 0x00049F5A File Offset: 0x0004815A
	public bool noVisibility()
	{
		return this.dynamicData.noVisiblity;
	}

	// Token: 0x06000FE4 RID: 4068 RVA: 0x00049F67 File Offset: 0x00048167
	public void setNoVisibility()
	{
		this.dynamicData.noVisiblity = true;
	}

	// Token: 0x06000FE5 RID: 4069 RVA: 0x00049F75 File Offset: 0x00048175
	public void clearNoVisibility()
	{
		this.dynamicData.noVisiblity = false;
	}

	// Token: 0x06000FE6 RID: 4070 RVA: 0x00049F83 File Offset: 0x00048183
	public bool impassable()
	{
		return !this.shouldBeRemovedFromGame() && this.dynamicData.impassable;
	}

	// Token: 0x06000FE7 RID: 4071 RVA: 0x00049F9A File Offset: 0x0004819A
	public virtual bool isContainer()
	{
		return false;
	}

	// Token: 0x06000FE8 RID: 4072 RVA: 0x00049F9D File Offset: 0x0004819D
	public virtual int getLight()
	{
		return this.dynamicData.light;
	}

	// Token: 0x06000FE9 RID: 4073 RVA: 0x00049FAA File Offset: 0x000481AA
	internal virtual string getNestedMapId()
	{
		return "";
	}

	// Token: 0x06000FEA RID: 4074 RVA: 0x00049FB1 File Offset: 0x000481B1
	private BaseAnimationContainer getAnimationContainer()
	{
		if (this.animationContainer == null)
		{
			this.animationContainer = new BaseAnimationContainer();
			this.animationContainer.setBaseAnimation("ANI_PropBase");
		}
		return this.animationContainer;
	}

	// Token: 0x06000FEB RID: 4075 RVA: 0x00049FDC File Offset: 0x000481DC
	public void applyLoadoutToTile(MapTile tile)
	{
		if (tile == null || this.dynamicData.loadoutApplied)
		{
			return;
		}
		GameData.applyLoadoutData(this.getLoadOutId(), tile.getInventory());
		this.dynamicData.loadoutApplied = true;
	}

	// Token: 0x040003E8 RID: 1000
	protected Prop.PropSaveData dynamicData;

	// Token: 0x040003E9 RID: 1001
	private SKALDProjectData.Objects.ApperanceDataContainer.ApperanceData apperanceData;

	// Token: 0x040003EA RID: 1002
	private SKALDProjectData.PropContainers.ContainerContainer rawData;

	// Token: 0x040003EB RID: 1003
	private Dictionary<Color32, Color32> swapDictionary;

	// Token: 0x040003EC RID: 1004
	private TextureTools.TextureData drawTexture;

	// Token: 0x040003ED RID: 1005
	private TextureTools.TextureData[,] tileMatrix;

	// Token: 0x040003EE RID: 1006
	private StringBuilder bufferIdBuilder;

	// Token: 0x040003EF RID: 1007
	private ImageOutlineDrawer outlineDrawer;

	// Token: 0x040003F0 RID: 1008
	private BaseAnimationContainer animationContainer;

	// Token: 0x040003F1 RID: 1009
	private Prop.ColorContainer colorContainer;

	// Token: 0x02000255 RID: 597
	[Serializable]
	private class ColorContainer
	{
		// Token: 0x06001986 RID: 6534 RVA: 0x0006FF64 File Offset: 0x0006E164
		public ColorContainer(SKALDProjectData.Objects.ApperanceDataContainer.ApperanceData appData)
		{
			this.mainColor = SkaldBaseObject.processStringFromOrList(appData.mainColors, null);
			this.secondaryColor = SkaldBaseObject.processStringFromOrList(appData.secColors, null);
			this.tertiaryColor = SkaldBaseObject.processStringFromOrList(appData.tertiaryColors, null);
			this.hairColor = SkaldBaseObject.processStringFromOrList(appData.hairColors, null);
			this.skinColor = SkaldBaseObject.processStringFromOrList(appData.skinColors, null);
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x0006FFD1 File Offset: 0x0006E1D1
		public string getMainColor()
		{
			return this.mainColor;
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x0006FFD9 File Offset: 0x0006E1D9
		public string getSecondaryColor()
		{
			return this.secondaryColor;
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x0006FFE1 File Offset: 0x0006E1E1
		public string getTertiaryColor()
		{
			return this.tertiaryColor;
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x0006FFE9 File Offset: 0x0006E1E9
		public string getHairColor()
		{
			return this.hairColor;
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x0006FFF1 File Offset: 0x0006E1F1
		public string getSkinColor()
		{
			return this.skinColor;
		}

		// Token: 0x04000922 RID: 2338
		public string mainColor;

		// Token: 0x04000923 RID: 2339
		public string secondaryColor;

		// Token: 0x04000924 RID: 2340
		public string tertiaryColor;

		// Token: 0x04000925 RID: 2341
		public string hairColor;

		// Token: 0x04000926 RID: 2342
		public string skinColor;
	}

	// Token: 0x02000256 RID: 598
	[Serializable]
	protected class PropSaveData : BaseSaveData
	{
		// Token: 0x0600198C RID: 6540 RVA: 0x0006FFFC File Offset: 0x0006E1FC
		public PropSaveData(SkaldWorldObject.WorldPosition position, SkaldBaseObject.CoreData coreData, SkaldInstanceObject.InstanceData instanceData) : base(position, coreData, instanceData)
		{
		}

		// Token: 0x04000927 RID: 2343
		public string animation = "";

		// Token: 0x04000928 RID: 2344
		public string verb = "";

		// Token: 0x04000929 RID: 2345
		public string apperanceId = "";

		// Token: 0x0400092A RID: 2346
		public string trap = "";

		// Token: 0x0400092B RID: 2347
		public string faction = "";

		// Token: 0x0400092C RID: 2348
		public bool noVisiblity;

		// Token: 0x0400092D RID: 2349
		public bool impassable;

		// Token: 0x0400092E RID: 2350
		public bool activated = true;

		// Token: 0x0400092F RID: 2351
		public bool locked;

		// Token: 0x04000930 RID: 2352
		public bool hidden;

		// Token: 0x04000931 RID: 2353
		public bool dug;

		// Token: 0x04000932 RID: 2354
		public bool loadoutApplied;

		// Token: 0x04000933 RID: 2355
		public int light;

		// Token: 0x04000934 RID: 2356
		public int modelFrame;

		// Token: 0x04000935 RID: 2357
		public int hitPoints = 5;

		// Token: 0x04000936 RID: 2358
		public bool drawFullModel;
	}
}
