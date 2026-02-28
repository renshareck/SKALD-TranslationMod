using System;

// Token: 0x0200010B RID: 267
[Serializable]
public class SkaldInstanceObject : SkaldBaseObject
{
	// Token: 0x060010E5 RID: 4325 RVA: 0x0004CA8A File Offset: 0x0004AC8A
	public SkaldInstanceObject()
	{
	}

	// Token: 0x060010E6 RID: 4326 RVA: 0x0004CA9D File Offset: 0x0004AC9D
	public SkaldInstanceObject(SKALDProjectData.GameDataObject rawData) : base(rawData)
	{
		this.instanceData.modelPath = base.processString(rawData.modelPath, null);
	}

	// Token: 0x060010E7 RID: 4327 RVA: 0x0004CAC9 File Offset: 0x0004ACC9
	public override void applySaveData(BaseSaveData baseSaveData)
	{
		base.applySaveData(baseSaveData);
		this.instanceData = baseSaveData.instanceData;
		if (this.instanceData == null)
		{
			MainControl.logError("Missing Instance Data!");
		}
	}

	// Token: 0x060010E8 RID: 4328 RVA: 0x0004CAF0 File Offset: 0x0004ACF0
	public virtual string printStatus()
	{
		string text = this.getId();
		if (this.instanceData.unique)
		{
			text += " U";
		}
		if (this.instanceData.remove)
		{
			text += " R";
		}
		if (this.instanceData.persistent)
		{
			text += " P";
		}
		return text;
	}

	// Token: 0x060010E9 RID: 4329 RVA: 0x0004CB50 File Offset: 0x0004AD50
	public bool canBeRemovedFromGame()
	{
		return !this.isPersistent() && !this.isUnique();
	}

	// Token: 0x060010EA RID: 4330 RVA: 0x0004CB65 File Offset: 0x0004AD65
	public virtual bool shouldBeRemovedFromGame()
	{
		return this.canBeRemovedFromGame() && this.instanceData.remove;
	}

	// Token: 0x060010EB RID: 4331 RVA: 0x0004CB7C File Offset: 0x0004AD7C
	public virtual string getModelPath()
	{
		return this.instanceData.modelPath;
	}

	// Token: 0x060010EC RID: 4332 RVA: 0x0004CB89 File Offset: 0x0004AD89
	public virtual void setModelPath(string s)
	{
		this.instanceData.modelPath = s;
	}

	// Token: 0x060010ED RID: 4333 RVA: 0x0004CB97 File Offset: 0x0004AD97
	public virtual TextureTools.TextureData getModelTexture()
	{
		return TextureTools.getSubImageTextureData(0, "Images/" + this.getModelPath());
	}

	// Token: 0x060010EE RID: 4334 RVA: 0x0004CBAF File Offset: 0x0004ADAF
	public virtual void setToBeRemoved()
	{
		if (this.canBeRemovedFromGame())
		{
			this.instanceData.remove = true;
		}
	}

	// Token: 0x060010EF RID: 4335 RVA: 0x0004CBC5 File Offset: 0x0004ADC5
	public void setUnique(bool b)
	{
		this.instanceData.unique = b;
	}

	// Token: 0x060010F0 RID: 4336 RVA: 0x0004CBD3 File Offset: 0x0004ADD3
	public void setPersistent(bool b)
	{
		this.instanceData.persistent = b;
	}

	// Token: 0x060010F1 RID: 4337 RVA: 0x0004CBE1 File Offset: 0x0004ADE1
	public virtual bool isUnique()
	{
		return this.instanceData.unique;
	}

	// Token: 0x060010F2 RID: 4338 RVA: 0x0004CBEE File Offset: 0x0004ADEE
	public virtual bool isPersistent()
	{
		return this.instanceData.persistent;
	}

	// Token: 0x04000401 RID: 1025
	public SkaldInstanceObject.InstanceData instanceData = new SkaldInstanceObject.InstanceData();

	// Token: 0x0200025A RID: 602
	[Serializable]
	public class InstanceData
	{
		// Token: 0x0400094A RID: 2378
		public bool unique;

		// Token: 0x0400094B RID: 2379
		public bool persistent;

		// Token: 0x0400094C RID: 2380
		public bool remove;

		// Token: 0x0400094D RID: 2381
		public string imagePath = "";

		// Token: 0x0400094E RID: 2382
		public string modelPath = "";
	}
}
