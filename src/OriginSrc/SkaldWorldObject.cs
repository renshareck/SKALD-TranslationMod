using System;

// Token: 0x02000111 RID: 273
[Serializable]
public class SkaldWorldObject : SkaldInstanceObject
{
	// Token: 0x06001146 RID: 4422 RVA: 0x0004D8E4 File Offset: 0x0004BAE4
	protected SkaldWorldObject()
	{
	}

	// Token: 0x06001147 RID: 4423 RVA: 0x0004D8F7 File Offset: 0x0004BAF7
	protected SkaldWorldObject(int _tileX, int _tileY, string _containerMapId)
	{
		this.setTilePosition(_tileX, _tileY, _containerMapId);
	}

	// Token: 0x06001148 RID: 4424 RVA: 0x0004D913 File Offset: 0x0004BB13
	protected SkaldWorldObject(SKALDProjectData.GameDataObject rawData) : base(rawData)
	{
	}

	// Token: 0x06001149 RID: 4425 RVA: 0x0004D927 File Offset: 0x0004BB27
	public virtual void setPixelPosition(int x, int y)
	{
		this.worldPosition.setPixelPosition(x, y);
	}

	// Token: 0x0600114A RID: 4426 RVA: 0x0004D938 File Offset: 0x0004BB38
	public MapTile getMapTile()
	{
		Map map = GameData.getMap(this.worldPosition.getMapId(), this.getId());
		if (map != null)
		{
			return map.getTile(this.worldPosition.getTileX(), this.worldPosition.getTileY());
		}
		return null;
	}

	// Token: 0x0600114B RID: 4427 RVA: 0x0004D97D File Offset: 0x0004BB7D
	public bool checkIfOnPosition(string mapId, int x, int y)
	{
		return this.worldPosition.getMapId() == mapId && this.worldPosition.getTileX() == x && this.worldPosition.getTileY() == y;
	}

	// Token: 0x0600114C RID: 4428 RVA: 0x0004D9B0 File Offset: 0x0004BBB0
	public virtual int getPixelX()
	{
		return this.worldPosition.getPixelX();
	}

	// Token: 0x0600114D RID: 4429 RVA: 0x0004D9BD File Offset: 0x0004BBBD
	public virtual int getPixelY()
	{
		return this.worldPosition.getPixelY();
	}

	// Token: 0x0600114E RID: 4430 RVA: 0x0004D9CA File Offset: 0x0004BBCA
	public virtual void setTilePosition(int x, int y, string mapId)
	{
		this.worldPosition.setTilePosition(x, y, mapId);
	}

	// Token: 0x0600114F RID: 4431 RVA: 0x0004D9DA File Offset: 0x0004BBDA
	public virtual string getInspectDescription()
	{
		return this.getName();
	}

	// Token: 0x06001150 RID: 4432 RVA: 0x0004D9E2 File Offset: 0x0004BBE2
	public override void applySaveData(BaseSaveData baseSaveData)
	{
		base.applySaveData(baseSaveData);
		this.worldPosition = baseSaveData.position;
		if (this.worldPosition == null)
		{
			MainControl.logError("Missing world position!");
		}
	}

	// Token: 0x06001151 RID: 4433 RVA: 0x0004DA09 File Offset: 0x0004BC09
	public virtual void clearTilePosition()
	{
		this.worldPosition.clearTilePosition();
	}

	// Token: 0x06001152 RID: 4434 RVA: 0x0004DA16 File Offset: 0x0004BC16
	public virtual int getTileX()
	{
		return this.worldPosition.getTileX();
	}

	// Token: 0x06001153 RID: 4435 RVA: 0x0004DA23 File Offset: 0x0004BC23
	public virtual int getTileY()
	{
		return this.worldPosition.getTileY();
	}

	// Token: 0x06001154 RID: 4436 RVA: 0x0004DA30 File Offset: 0x0004BC30
	public string getContainerMapId()
	{
		return this.worldPosition.getMapId();
	}

	// Token: 0x06001155 RID: 4437 RVA: 0x0004DA40 File Offset: 0x0004BC40
	public override string printStatus()
	{
		return string.Concat(new string[]
		{
			base.printStatus(),
			" - ",
			this.getTileX().ToString(),
			" - ",
			this.getTileY().ToString(),
			" - ",
			this.getContainerMapId()
		});
	}

	// Token: 0x0400040B RID: 1035
	protected SkaldWorldObject.WorldPosition worldPosition = new SkaldWorldObject.WorldPosition();

	// Token: 0x0200025F RID: 607
	[Serializable]
	public class WorldPosition
	{
		// Token: 0x060019ED RID: 6637 RVA: 0x0007158F File Offset: 0x0006F78F
		public void setPixelPosition(int x, int y)
		{
			this.pixelX = x;
			this.pixelY = y;
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x0007159F File Offset: 0x0006F79F
		public int getPixelX()
		{
			return this.pixelX;
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x000715A7 File Offset: 0x0006F7A7
		public int getPixelY()
		{
			return this.pixelY;
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x000715AF File Offset: 0x0006F7AF
		public void setTilePosition(int x, int y, string mapId)
		{
			this.tileX = x;
			this.tileY = y;
			this.pixelX = this.tileX * 16;
			this.pixelY = this.tileY * 16;
			this.setMapId(mapId);
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x000715E4 File Offset: 0x0006F7E4
		public void clearTilePosition()
		{
			this.tileX = -1;
			this.tileY = -1;
			this.containerMapId = null;
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x000715FB File Offset: 0x0006F7FB
		public int getTileX()
		{
			return this.tileX;
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x00071603 File Offset: 0x0006F803
		public int getTileY()
		{
			return this.tileY;
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x0007160B File Offset: 0x0006F80B
		public string getMapId()
		{
			return this.containerMapId;
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x00071613 File Offset: 0x0006F813
		public void setMapId(string mapId)
		{
			this.containerMapId = mapId;
		}

		// Token: 0x04000961 RID: 2401
		protected string containerMapId;

		// Token: 0x04000962 RID: 2402
		protected int tileX = -1;

		// Token: 0x04000963 RID: 2403
		protected int tileY = -1;

		// Token: 0x04000964 RID: 2404
		private int pixelX;

		// Token: 0x04000965 RID: 2405
		private int pixelY;
	}
}
