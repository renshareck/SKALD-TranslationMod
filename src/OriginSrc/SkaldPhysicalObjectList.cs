using System;

// Token: 0x0200010E RID: 270
[Serializable]
public class SkaldPhysicalObjectList : SkaldWorldObjectList
{
	// Token: 0x06001128 RID: 4392 RVA: 0x0004D38C File Offset: 0x0004B58C
	public new SkaldPhysicalObject getCurrentObject()
	{
		return base.getCurrentObject() as SkaldPhysicalObject;
	}

	// Token: 0x06001129 RID: 4393 RVA: 0x0004D39C File Offset: 0x0004B59C
	public string printPositions()
	{
		string text = "";
		text += "POSITIONS";
		text = string.Concat(new string[]
		{
			text,
			"\nContainer tile pos: ",
			this.getTileX().ToString(),
			" , ",
			this.getTileY().ToString()
		});
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			SkaldPhysicalObject skaldPhysicalObject = (SkaldPhysicalObject)skaldBaseObject;
			text = string.Concat(new string[]
			{
				text,
				"\n",
				skaldPhysicalObject.getId(),
				" Tile pos: ",
				skaldPhysicalObject.getTileX().ToString(),
				" , ",
				skaldPhysicalObject.getTileY().ToString(),
				"\t Pixel pos: ",
				skaldPhysicalObject.getPixelX().ToString(),
				" , ",
				skaldPhysicalObject.getPixelY().ToString()
			});
		}
		text += "\n\n";
		return text;
	}

	// Token: 0x0600112A RID: 4394 RVA: 0x0004D4DC File Offset: 0x0004B6DC
	public override int getPixelX()
	{
		if (this.getCurrentObject() == null)
		{
			return 0;
		}
		return this.getCurrentObject().getPixelX();
	}

	// Token: 0x0600112B RID: 4395 RVA: 0x0004D4F3 File Offset: 0x0004B6F3
	public override int getPixelY()
	{
		if (this.getCurrentObject() == null)
		{
			return 0;
		}
		return this.getCurrentObject().getPixelY();
	}

	// Token: 0x0600112C RID: 4396 RVA: 0x0004D50C File Offset: 0x0004B70C
	public override void setTilePosition(int x, int y, string mapId)
	{
		base.setTilePosition(x, y, mapId);
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((SkaldPhysicalObject)skaldBaseObject).setTilePosition(x, y, mapId);
		}
	}

	// Token: 0x0600112D RID: 4397 RVA: 0x0004D570 File Offset: 0x0004B770
	public void snapToGrid()
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((SkaldPhysicalObject)skaldBaseObject).snapToGrid();
		}
	}

	// Token: 0x0600112E RID: 4398 RVA: 0x0004D5C8 File Offset: 0x0004B7C8
	public virtual int getBounceOffset()
	{
		return this.getCurrentObject().getBounceOffset();
	}

	// Token: 0x0600112F RID: 4399 RVA: 0x0004D5D8 File Offset: 0x0004B7D8
	public override void setPixelPosition(int x, int y)
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((SkaldPhysicalObject)skaldBaseObject).setPixelPosition(x, y);
		}
	}

	// Token: 0x06001130 RID: 4400 RVA: 0x0004D630 File Offset: 0x0004B830
	public void updatePhysics()
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((SkaldPhysicalObject)skaldBaseObject).updatePhysics();
		}
	}
}
