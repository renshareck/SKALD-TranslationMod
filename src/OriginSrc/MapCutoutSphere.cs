using System;

// Token: 0x020000E0 RID: 224
public class MapCutoutSphere : MapCutoutSquare
{
	// Token: 0x06000DE6 RID: 3558 RVA: 0x000403D9 File Offset: 0x0003E5D9
	public MapCutoutSphere(int x, int y, int radius, Character user) : base(x, y, radius, user)
	{
	}

	// Token: 0x06000DE7 RID: 3559 RVA: 0x000403E8 File Offset: 0x0003E5E8
	protected override int getRadius()
	{
		int num = base.getRadius();
		if (base.getUser() != null)
		{
			num += base.getUser().getRadiusBonusSphere();
		}
		return num;
	}
}
