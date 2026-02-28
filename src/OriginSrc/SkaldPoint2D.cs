using System;

// Token: 0x02000180 RID: 384
public class SkaldPoint2D
{
	// Token: 0x06001456 RID: 5206 RVA: 0x0005A818 File Offset: 0x00058A18
	public SkaldPoint2D()
	{
	}

	// Token: 0x06001457 RID: 5207 RVA: 0x0005A820 File Offset: 0x00058A20
	public SkaldPoint2D(int x, int y)
	{
		this.X = x;
		this.Y = y;
	}

	// Token: 0x06001458 RID: 5208 RVA: 0x0005A836 File Offset: 0x00058A36
	public SkaldPoint2D(SkaldPoint2D pointToCopy)
	{
		this.X = pointToCopy.X;
		this.Y = pointToCopy.Y;
	}

	// Token: 0x17000037 RID: 55
	// (get) Token: 0x06001459 RID: 5209 RVA: 0x0005A856 File Offset: 0x00058A56
	// (set) Token: 0x0600145A RID: 5210 RVA: 0x0005A85E File Offset: 0x00058A5E
	public int X
	{
		get
		{
			return this.x;
		}
		set
		{
			this.x = value;
		}
	}

	// Token: 0x17000038 RID: 56
	// (get) Token: 0x0600145B RID: 5211 RVA: 0x0005A867 File Offset: 0x00058A67
	// (set) Token: 0x0600145C RID: 5212 RVA: 0x0005A86F File Offset: 0x00058A6F
	public int Y
	{
		get
		{
			return this.y;
		}
		set
		{
			this.y = value;
		}
	}

	// Token: 0x04000531 RID: 1329
	private int x;

	// Token: 0x04000532 RID: 1330
	private int y;
}
