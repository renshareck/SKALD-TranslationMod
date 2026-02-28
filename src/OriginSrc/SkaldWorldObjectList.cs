using System;

// Token: 0x02000112 RID: 274
[Serializable]
public class SkaldWorldObjectList : SkaldObjectList
{
	// Token: 0x06001156 RID: 4438 RVA: 0x0004DAA4 File Offset: 0x0004BCA4
	public override void setTilePosition(int x, int y, string mapId)
	{
		base.setTilePosition(x, y, mapId);
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((SkaldWorldObject)skaldBaseObject).setTilePosition(x, y, mapId);
		}
	}

	// Token: 0x06001157 RID: 4439 RVA: 0x0004DB08 File Offset: 0x0004BD08
	public new SkaldWorldObject getCurrentObject()
	{
		return base.getCurrentObject() as SkaldWorldObject;
	}

	// Token: 0x06001158 RID: 4440 RVA: 0x0004DB15 File Offset: 0x0004BD15
	public override int getTileX()
	{
		if (this.getCurrentObject() == null)
		{
			return base.getTileX();
		}
		return this.getCurrentObject().getTileX();
	}

	// Token: 0x06001159 RID: 4441 RVA: 0x0004DB34 File Offset: 0x0004BD34
	public override void setToBeRemoved()
	{
		base.setToBeRemoved();
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((SkaldWorldObject)skaldBaseObject).setToBeRemoved();
		}
	}

	// Token: 0x0600115A RID: 4442 RVA: 0x0004DB90 File Offset: 0x0004BD90
	public override int getTileY()
	{
		if (this.getCurrentObject() == null)
		{
			return base.getTileY();
		}
		return this.getCurrentObject().getTileY();
	}

	// Token: 0x0600115B RID: 4443 RVA: 0x0004DBAC File Offset: 0x0004BDAC
	public void add(SkaldWorldObject component)
	{
		component.setTilePosition(this.getTileX(), this.getTileY(), base.getContainerMapId());
		base.add(component);
	}
}
