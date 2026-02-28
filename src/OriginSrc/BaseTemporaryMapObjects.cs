using System;

// Token: 0x020000EF RID: 239
public abstract class BaseTemporaryMapObjects : SkaldPhysicalObject
{
	// Token: 0x06000EEF RID: 3823 RVA: 0x00046B65 File Offset: 0x00044D65
	protected BaseTemporaryMapObjects(MapTile tile)
	{
		this.setTilePosition(tile.getTileX(), tile.getTileY(), tile.getContainerMapId());
		base.snapToGrid();
		this.initialize();
	}

	// Token: 0x06000EF0 RID: 3824 RVA: 0x00046B91 File Offset: 0x00044D91
	public bool isDead()
	{
		return !base.getVisualEffects().areAnyEffectsOrLightActive();
	}

	// Token: 0x06000EF1 RID: 3825 RVA: 0x00046BA1 File Offset: 0x00044DA1
	public override int getEmitterX()
	{
		return base.getEmitterX() + 8;
	}

	// Token: 0x06000EF2 RID: 3826 RVA: 0x00046BAB File Offset: 0x00044DAB
	public override int getEmitterY()
	{
		return base.getEmitterY() + 8;
	}

	// Token: 0x06000EF3 RID: 3827
	protected abstract void initialize();
}
