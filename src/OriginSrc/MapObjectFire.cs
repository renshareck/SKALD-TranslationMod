using System;

// Token: 0x020000F1 RID: 241
public class MapObjectFire : BaseTemporaryMapObjects
{
	// Token: 0x06000F02 RID: 3842 RVA: 0x00046DEC File Offset: 0x00044FEC
	public MapObjectFire(MapTile tile) : base(tile)
	{
	}

	// Token: 0x06000F03 RID: 3843 RVA: 0x00046DF5 File Offset: 0x00044FF5
	protected override void initialize()
	{
		base.getVisualEffects().setFireFlashMedium(this);
	}
}
