using System;
using System.Collections.Generic;

// Token: 0x02000028 RID: 40
public static class FrameOffsetControl
{
	// Token: 0x0600044D RID: 1101 RVA: 0x00013657 File Offset: 0x00011857
	public static FrameOffsetControl.OffsetData getTorsoOffset(int frame)
	{
		return FrameOffsetControl.getTorsoOffsetContainer().getOffset(frame);
	}

	// Token: 0x0600044E RID: 1102 RVA: 0x00013664 File Offset: 0x00011864
	public static FrameOffsetControl.OffsetData getCloakOffset(int frame)
	{
		return FrameOffsetControl.getCloakOffsetContainer().getOffset(frame);
	}

	// Token: 0x0600044F RID: 1103 RVA: 0x00013671 File Offset: 0x00011871
	public static FrameOffsetControl.OffsetData getArmsOffset(int frame)
	{
		return FrameOffsetControl.getArmsOffsetContainer().getOffset(frame);
	}

	// Token: 0x06000450 RID: 1104 RVA: 0x0001367E File Offset: 0x0001187E
	public static FrameOffsetControl.OffsetData getLegsOffset(int frame)
	{
		return FrameOffsetControl.getLegsOffsetContainer().getOffset(frame);
	}

	// Token: 0x06000451 RID: 1105 RVA: 0x0001368B File Offset: 0x0001188B
	public static FrameOffsetControl.OffsetData getHeadOffset(int frame)
	{
		return FrameOffsetControl.getHeadOffsetControl().getOffset(frame);
	}

	// Token: 0x06000452 RID: 1106 RVA: 0x00013698 File Offset: 0x00011898
	public static FrameOffsetControl.OffsetData getLightOffset(int frame)
	{
		return FrameOffsetControl.getLightOffsetControl().getOffset(frame);
	}

	// Token: 0x06000453 RID: 1107 RVA: 0x000136A5 File Offset: 0x000118A5
	public static FrameOffsetControl.OffsetData getShieldOffset(int frame)
	{
		return FrameOffsetControl.getShieldOffsetControl().getOffset(frame);
	}

	// Token: 0x06000454 RID: 1108 RVA: 0x000136B2 File Offset: 0x000118B2
	public static FrameOffsetControl.OffsetData getWeaponOffset(int frame)
	{
		return FrameOffsetControl.getWeaponOffsetControl().getOffset(frame);
	}

	// Token: 0x06000455 RID: 1109 RVA: 0x000136BF File Offset: 0x000118BF
	public static FrameOffsetControl.OffsetData getBannerOffset(int frame)
	{
		return FrameOffsetControl.getBannerOffsetControl().getOffset(frame);
	}

	// Token: 0x06000456 RID: 1110 RVA: 0x000136CC File Offset: 0x000118CC
	public static FrameOffsetControl.OffsetData getGlobalOffset(int frame)
	{
		return FrameOffsetControl.getGlobalOffsetContainer().getOffset(frame);
	}

	// Token: 0x06000457 RID: 1111 RVA: 0x000136D9 File Offset: 0x000118D9
	public static FrameOffsetControl.OffsetData getSwooshOffset(int frame)
	{
		return FrameOffsetControl.getSwooshOffsetControl().getOffset(frame);
	}

	// Token: 0x06000458 RID: 1112 RVA: 0x000136E8 File Offset: 0x000118E8
	private static FrameOffsetControl.OffsetContainer getGlobalOffsetContainer()
	{
		if (FrameOffsetControl.globalOffset == null)
		{
			FrameOffsetControl.globalOffset = new FrameOffsetControl.OffsetContainer(0, 0);
			FrameOffsetControl.globalOffset.Add(FrameOffsetControl.deadIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(-24, -18, 0, true, false)
			});
		}
		return FrameOffsetControl.globalOffset;
	}

	// Token: 0x06000459 RID: 1113 RVA: 0x00013734 File Offset: 0x00011934
	private static FrameOffsetControl.OffsetContainer getLegsOffsetContainer()
	{
		if (FrameOffsetControl.legsOffset == null)
		{
			FrameOffsetControl.legsOffset = new FrameOffsetControl.OffsetContainer(0, 0);
			FrameOffsetControl.legsOffset.Add(FrameOffsetControl.bounceIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(1),
				new FrameOffsetControl.OffsetData(1)
			});
			FrameOffsetControl.legsOffset.Add(FrameOffsetControl.idle2HIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(1),
				new FrameOffsetControl.OffsetData(1)
			});
			FrameOffsetControl.legsOffset.Add(FrameOffsetControl.swingIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(4),
				new FrameOffsetControl.OffsetData(4),
				new FrameOffsetControl.OffsetData(5),
				new FrameOffsetControl.OffsetData(5),
				new FrameOffsetControl.OffsetData(6),
				new FrameOffsetControl.OffsetData(4)
			});
			FrameOffsetControl.legsOffset.Add(FrameOffsetControl.punchIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(4),
				new FrameOffsetControl.OffsetData(5),
				new FrameOffsetControl.OffsetData(6),
				new FrameOffsetControl.OffsetData(4)
			});
			FrameOffsetControl.legsOffset.Add(FrameOffsetControl.stabIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(4),
				new FrameOffsetControl.OffsetData(5),
				new FrameOffsetControl.OffsetData(6),
				new FrameOffsetControl.OffsetData(4)
			});
			FrameOffsetControl.legsOffset.Add(FrameOffsetControl.twohandedSwingIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(4),
				new FrameOffsetControl.OffsetData(5),
				new FrameOffsetControl.OffsetData(6),
				new FrameOffsetControl.OffsetData(4)
			});
			FrameOffsetControl.legsOffset.Add(FrameOffsetControl.bowIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.legsOffset.Add(FrameOffsetControl.idleSimpleIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(12),
				new FrameOffsetControl.OffsetData(12),
				new FrameOffsetControl.OffsetData(12),
				new FrameOffsetControl.OffsetData(12)
			});
			FrameOffsetControl.legsOffset.Add(FrameOffsetControl.idleOverlandIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(12),
				new FrameOffsetControl.OffsetData(12),
				new FrameOffsetControl.OffsetData(12),
				new FrameOffsetControl.OffsetData(12)
			});
			FrameOffsetControl.legsOffset.Add(FrameOffsetControl.idleFirmIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(17),
				new FrameOffsetControl.OffsetData(17),
				new FrameOffsetControl.OffsetData(17),
				new FrameOffsetControl.OffsetData(17)
			});
			FrameOffsetControl.legsOffset.Add(FrameOffsetControl.idleFemaleIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(2, 0, 14),
				new FrameOffsetControl.OffsetData(2, 0, 14),
				new FrameOffsetControl.OffsetData(15),
				new FrameOffsetControl.OffsetData(15),
				new FrameOffsetControl.OffsetData(-2, 0, 16),
				new FrameOffsetControl.OffsetData(-2, 0, 16)
			});
			FrameOffsetControl.legsOffset.Add(FrameOffsetControl.idleTimidIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(15),
				new FrameOffsetControl.OffsetData(15),
				new FrameOffsetControl.OffsetData(15)
			});
			FrameOffsetControl.legsOffset.Add(FrameOffsetControl.idleDandyIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(2, 0, 14),
				new FrameOffsetControl.OffsetData(15),
				new FrameOffsetControl.OffsetData(-2, 0, 16),
				new FrameOffsetControl.OffsetData(-2, 0, 16)
			});
			FrameOffsetControl.legsOffset.Add(FrameOffsetControl.panicIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(17),
				new FrameOffsetControl.OffsetData(17),
				new FrameOffsetControl.OffsetData(17)
			});
			FrameOffsetControl.legsOffset.Add(FrameOffsetControl.waitressIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(16)
			});
			FrameOffsetControl.legsOffset.Add(FrameOffsetControl.musicDrumIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(1)
			});
			FrameOffsetControl.legsOffset.Add(FrameOffsetControl.musicLuteIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(15),
				new FrameOffsetControl.OffsetData(15)
			});
			FrameOffsetControl.legsOffset.Add(FrameOffsetControl.musicFluteIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(15),
				new FrameOffsetControl.OffsetData(15)
			});
			FrameOffsetControl.legsOffset.Add(FrameOffsetControl.jugglingIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(15),
				new FrameOffsetControl.OffsetData(15),
				new FrameOffsetControl.OffsetData(15),
				new FrameOffsetControl.OffsetData(15)
			});
			FrameOffsetControl.legsOffset.Add(FrameOffsetControl.carousingIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(12),
				new FrameOffsetControl.OffsetData(12),
				new FrameOffsetControl.OffsetData(12)
			});
			FrameOffsetControl.legsOffset.Add(FrameOffsetControl.hurtIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(2)
			});
			FrameOffsetControl.legsOffset.Add(FrameOffsetControl.hurtUnarmedIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(2)
			});
			FrameOffsetControl.legsOffset.Add(FrameOffsetControl.deadIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(-8, 5, 0)
			});
			FrameOffsetControl.legsOffset.Add(FrameOffsetControl.spellIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(8),
				new FrameOffsetControl.OffsetData(8),
				new FrameOffsetControl.OffsetData(8),
				new FrameOffsetControl.OffsetData(8)
			});
			FrameOffsetControl.legsOffset.Add(FrameOffsetControl.prayIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(9),
				new FrameOffsetControl.OffsetData(9)
			});
			FrameOffsetControl.legsOffset.Add(FrameOffsetControl.despairIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(12),
				new FrameOffsetControl.OffsetData(4),
				new FrameOffsetControl.OffsetData(10),
				new FrameOffsetControl.OffsetData(11),
				new FrameOffsetControl.OffsetData(11),
				new FrameOffsetControl.OffsetData(11)
			});
			FrameOffsetControl.legsOffset.Add(FrameOffsetControl.idleSadIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(13),
				new FrameOffsetControl.OffsetData(13)
			});
			FrameOffsetControl.legsOffset.Add(FrameOffsetControl.idleGuardIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(9),
				new FrameOffsetControl.OffsetData(9)
			});
		}
		return FrameOffsetControl.legsOffset;
	}

	// Token: 0x0600045A RID: 1114 RVA: 0x00013D24 File Offset: 0x00011F24
	private static FrameOffsetControl.OffsetContainer getTorsoOffsetContainer()
	{
		if (FrameOffsetControl.torsoOffset == null)
		{
			FrameOffsetControl.torsoOffset = new FrameOffsetControl.OffsetContainer(FrameOffsetControl.getLegsOffsetContainer(), 4, 12);
			FrameOffsetControl.torsoOffset.Add(FrameOffsetControl.bounceIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0, -1),
				new FrameOffsetControl.OffsetData(0, -1)
			});
			FrameOffsetControl.torsoOffset.Add(FrameOffsetControl.idle2HIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0, -1),
				new FrameOffsetControl.OffsetData(0, -1)
			});
			FrameOffsetControl.torsoOffset.Add(FrameOffsetControl.swingIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(-1, -1),
				new FrameOffsetControl.OffsetData(-1, -1),
				new FrameOffsetControl.OffsetData(0, -1),
				new FrameOffsetControl.OffsetData(0, -1),
				new FrameOffsetControl.OffsetData(1, -1),
				new FrameOffsetControl.OffsetData(-1, -1)
			});
			FrameOffsetControl.torsoOffset.Add(FrameOffsetControl.punchIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(-1, -1),
				new FrameOffsetControl.OffsetData(-1, -1),
				new FrameOffsetControl.OffsetData(0, -1),
				new FrameOffsetControl.OffsetData(0, -1),
				new FrameOffsetControl.OffsetData(1, -1),
				new FrameOffsetControl.OffsetData(-1, -1)
			});
			FrameOffsetControl.torsoOffset.Add(FrameOffsetControl.stabIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(-1, -1),
				new FrameOffsetControl.OffsetData(0, -1),
				new FrameOffsetControl.OffsetData(1, -1),
				new FrameOffsetControl.OffsetData(-1, -1)
			});
			FrameOffsetControl.torsoOffset.Add(FrameOffsetControl.twohandedSwingIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(-1, -1),
				new FrameOffsetControl.OffsetData(0, -1),
				new FrameOffsetControl.OffsetData(1, -1),
				new FrameOffsetControl.OffsetData(-1, -1)
			});
			FrameOffsetControl.torsoOffset.Add(FrameOffsetControl.bowIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.torsoOffset.Add(FrameOffsetControl.idleSimpleIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.torsoOffset.Add(FrameOffsetControl.idleOverlandIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.torsoOffset.Add(FrameOffsetControl.idleFirmIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.torsoOffset.Add(FrameOffsetControl.idleFemaleIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.torsoOffset.Add(FrameOffsetControl.idleTimidIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.torsoOffset.Add(FrameOffsetControl.idleDandyIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.torsoOffset.Add(FrameOffsetControl.panicIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.torsoOffset.Add(FrameOffsetControl.waitressIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.torsoOffset.Add(FrameOffsetControl.musicDrumIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0, -1)
			});
			FrameOffsetControl.torsoOffset.Add(FrameOffsetControl.musicLuteIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0, -1),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.torsoOffset.Add(FrameOffsetControl.musicFluteIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0, -1)
			});
			FrameOffsetControl.torsoOffset.Add(FrameOffsetControl.jugglingIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.torsoOffset.Add(FrameOffsetControl.carousingIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(-1, 0, 0),
				new FrameOffsetControl.OffsetData(1, 0, 0)
			});
			FrameOffsetControl.torsoOffset.Add(FrameOffsetControl.hurtIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(-4, -1)
			});
			FrameOffsetControl.torsoOffset.Add(FrameOffsetControl.hurtUnarmedIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(-4, -1)
			});
			FrameOffsetControl.torsoOffset.Add(FrameOffsetControl.deadIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.torsoOffset.Add(FrameOffsetControl.spellIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0, -1, 0),
				new FrameOffsetControl.OffsetData(0, -1, 0),
				new FrameOffsetControl.OffsetData(0, -1, 0),
				new FrameOffsetControl.OffsetData(0, -1, 0)
			});
			FrameOffsetControl.torsoOffset.Add(FrameOffsetControl.prayIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.torsoOffset.Add(FrameOffsetControl.despairIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(-1, -2),
				new FrameOffsetControl.OffsetData(0, -3),
				new FrameOffsetControl.OffsetData(-1, -4),
				new FrameOffsetControl.OffsetData(-2, -4),
				new FrameOffsetControl.OffsetData(0, -4)
			});
			FrameOffsetControl.torsoOffset.Add(FrameOffsetControl.idleSadIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(-1, 0, 0),
				new FrameOffsetControl.OffsetData(-1, 0, 0)
			});
			FrameOffsetControl.torsoOffset.Add(FrameOffsetControl.idleGuardIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0)
			});
		}
		return FrameOffsetControl.torsoOffset;
	}

	// Token: 0x0600045B RID: 1115 RVA: 0x00014320 File Offset: 0x00012520
	private static FrameOffsetControl.OffsetContainer getCloakOffsetContainer()
	{
		if (FrameOffsetControl.cloakOffset == null)
		{
			FrameOffsetControl.cloakOffset = new FrameOffsetControl.OffsetContainer(FrameOffsetControl.getTorsoOffsetContainer(), -7, -10);
			FrameOffsetControl.cloakOffset.Add(FrameOffsetControl.bounceIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(1),
				new FrameOffsetControl.OffsetData(2),
				new FrameOffsetControl.OffsetData(3)
			});
			FrameOffsetControl.cloakOffset.Add(FrameOffsetControl.idle2HIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(1),
				new FrameOffsetControl.OffsetData(2),
				new FrameOffsetControl.OffsetData(3)
			});
			FrameOffsetControl.cloakOffset.Add(FrameOffsetControl.swingIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.cloakOffset.Add(FrameOffsetControl.punchIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.cloakOffset.Add(FrameOffsetControl.stabIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.cloakOffset.Add(FrameOffsetControl.twohandedSwingIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.cloakOffset.Add(FrameOffsetControl.bowIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.cloakOffset.Add(FrameOffsetControl.idleSimpleIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(4),
				new FrameOffsetControl.OffsetData(4),
				new FrameOffsetControl.OffsetData(4),
				new FrameOffsetControl.OffsetData(4)
			});
			FrameOffsetControl.cloakOffset.Add(FrameOffsetControl.idleOverlandIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(4),
				new FrameOffsetControl.OffsetData(4),
				new FrameOffsetControl.OffsetData(4),
				new FrameOffsetControl.OffsetData(4)
			});
			FrameOffsetControl.cloakOffset.Add(FrameOffsetControl.idleFirmIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(4),
				new FrameOffsetControl.OffsetData(4),
				new FrameOffsetControl.OffsetData(4),
				new FrameOffsetControl.OffsetData(4)
			});
			FrameOffsetControl.cloakOffset.Add(FrameOffsetControl.idleFemaleIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(4),
				new FrameOffsetControl.OffsetData(4),
				new FrameOffsetControl.OffsetData(4),
				new FrameOffsetControl.OffsetData(4),
				new FrameOffsetControl.OffsetData(4),
				new FrameOffsetControl.OffsetData(4)
			});
			FrameOffsetControl.cloakOffset.Add(FrameOffsetControl.idleTimidIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(4),
				new FrameOffsetControl.OffsetData(4),
				new FrameOffsetControl.OffsetData(4)
			});
			FrameOffsetControl.cloakOffset.Add(FrameOffsetControl.idleDandyIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(4),
				new FrameOffsetControl.OffsetData(4),
				new FrameOffsetControl.OffsetData(4),
				new FrameOffsetControl.OffsetData(4)
			});
			FrameOffsetControl.cloakOffset.Add(FrameOffsetControl.panicIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.cloakOffset.Add(FrameOffsetControl.waitressIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(4)
			});
			FrameOffsetControl.cloakOffset.Add(FrameOffsetControl.musicDrumIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(4),
				new FrameOffsetControl.OffsetData(4)
			});
			FrameOffsetControl.cloakOffset.Add(FrameOffsetControl.musicLuteIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(4),
				new FrameOffsetControl.OffsetData(4)
			});
			FrameOffsetControl.cloakOffset.Add(FrameOffsetControl.musicFluteIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(4),
				new FrameOffsetControl.OffsetData(4)
			});
			FrameOffsetControl.cloakOffset.Add(FrameOffsetControl.jugglingIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(4),
				new FrameOffsetControl.OffsetData(4),
				new FrameOffsetControl.OffsetData(4),
				new FrameOffsetControl.OffsetData(4)
			});
			FrameOffsetControl.cloakOffset.Add(FrameOffsetControl.carousingIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(4),
				new FrameOffsetControl.OffsetData(4),
				new FrameOffsetControl.OffsetData(4)
			});
			FrameOffsetControl.cloakOffset.Add(FrameOffsetControl.hurtIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.cloakOffset.Add(FrameOffsetControl.hurtUnarmedIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.cloakOffset.Add(FrameOffsetControl.deadIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.cloakOffset.Add(FrameOffsetControl.spellIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(1),
				new FrameOffsetControl.OffsetData(2),
				new FrameOffsetControl.OffsetData(3)
			});
			FrameOffsetControl.cloakOffset.Add(FrameOffsetControl.prayIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(4),
				new FrameOffsetControl.OffsetData(4)
			});
			FrameOffsetControl.cloakOffset.Add(FrameOffsetControl.despairIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.cloakOffset.Add(FrameOffsetControl.idleSadIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(4),
				new FrameOffsetControl.OffsetData(4)
			});
			FrameOffsetControl.cloakOffset.Add(FrameOffsetControl.idleGuardIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(4),
				new FrameOffsetControl.OffsetData(4)
			});
		}
		return FrameOffsetControl.cloakOffset;
	}

	// Token: 0x0600045C RID: 1116 RVA: 0x000148E0 File Offset: 0x00012AE0
	private static FrameOffsetControl.OffsetContainer getArmsOffsetContainer()
	{
		if (FrameOffsetControl.armsOffset == null)
		{
			FrameOffsetControl.armsOffset = new FrameOffsetControl.OffsetContainer(FrameOffsetControl.getTorsoOffsetContainer(), -5, -5);
			FrameOffsetControl.armsOffset.Add(FrameOffsetControl.bounceIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.armsOffset.Add(FrameOffsetControl.idle2HIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(1),
				new FrameOffsetControl.OffsetData(1),
				new FrameOffsetControl.OffsetData(1),
				new FrameOffsetControl.OffsetData(1)
			});
			FrameOffsetControl.armsOffset.Add(FrameOffsetControl.swingIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(4),
				new FrameOffsetControl.OffsetData(5),
				new FrameOffsetControl.OffsetData(6),
				new FrameOffsetControl.OffsetData(7),
				new FrameOffsetControl.OffsetData(8),
				new FrameOffsetControl.OffsetData(9)
			});
			FrameOffsetControl.armsOffset.Add(FrameOffsetControl.punchIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(12),
				new FrameOffsetControl.OffsetData(13),
				new FrameOffsetControl.OffsetData(14),
				new FrameOffsetControl.OffsetData(12)
			});
			FrameOffsetControl.armsOffset.Add(FrameOffsetControl.stabIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(7),
				new FrameOffsetControl.OffsetData(8),
				new FrameOffsetControl.OffsetData(9),
				new FrameOffsetControl.OffsetData(7)
			});
			FrameOffsetControl.armsOffset.Add(FrameOffsetControl.twohandedSwingIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(16),
				new FrameOffsetControl.OffsetData(17),
				new FrameOffsetControl.OffsetData(18),
				new FrameOffsetControl.OffsetData(19)
			});
			FrameOffsetControl.armsOffset.Add(FrameOffsetControl.bowIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(20),
				new FrameOffsetControl.OffsetData(1, 0, 21),
				new FrameOffsetControl.OffsetData(1, 0, 22),
				new FrameOffsetControl.OffsetData(23)
			});
			FrameOffsetControl.armsOffset.Add(FrameOffsetControl.idleSimpleIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(28),
				new FrameOffsetControl.OffsetData(29),
				new FrameOffsetControl.OffsetData(30),
				new FrameOffsetControl.OffsetData(31)
			});
			FrameOffsetControl.armsOffset.Add(FrameOffsetControl.idleOverlandIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.armsOffset.Add(FrameOffsetControl.idleFirmIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(35),
				new FrameOffsetControl.OffsetData(29),
				new FrameOffsetControl.OffsetData(30),
				new FrameOffsetControl.OffsetData(31)
			});
			FrameOffsetControl.armsOffset.Add(FrameOffsetControl.idleFemaleIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(30),
				new FrameOffsetControl.OffsetData(31),
				new FrameOffsetControl.OffsetData(34),
				new FrameOffsetControl.OffsetData(30),
				new FrameOffsetControl.OffsetData(31),
				new FrameOffsetControl.OffsetData(34)
			});
			FrameOffsetControl.armsOffset.Add(FrameOffsetControl.idleTimidIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(32),
				new FrameOffsetControl.OffsetData(33),
				new FrameOffsetControl.OffsetData(36)
			});
			FrameOffsetControl.armsOffset.Add(FrameOffsetControl.idleDandyIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(30),
				new FrameOffsetControl.OffsetData(31),
				new FrameOffsetControl.OffsetData(30),
				new FrameOffsetControl.OffsetData(31)
			});
			FrameOffsetControl.armsOffset.Add(FrameOffsetControl.waitressIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(40)
			});
			FrameOffsetControl.armsOffset.Add(FrameOffsetControl.musicDrumIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(44),
				new FrameOffsetControl.OffsetData(45)
			});
			FrameOffsetControl.armsOffset.Add(FrameOffsetControl.musicLuteIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(52),
				new FrameOffsetControl.OffsetData(53)
			});
			FrameOffsetControl.armsOffset.Add(FrameOffsetControl.musicFluteIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(46),
				new FrameOffsetControl.OffsetData(47)
			});
			FrameOffsetControl.armsOffset.Add(FrameOffsetControl.jugglingIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(48),
				new FrameOffsetControl.OffsetData(49),
				new FrameOffsetControl.OffsetData(50),
				new FrameOffsetControl.OffsetData(51)
			});
			FrameOffsetControl.armsOffset.Add(FrameOffsetControl.carousingIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(41),
				new FrameOffsetControl.OffsetData(42),
				new FrameOffsetControl.OffsetData(43)
			});
			FrameOffsetControl.armsOffset.Add(FrameOffsetControl.hurtIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(9)
			});
			FrameOffsetControl.armsOffset.Add(FrameOffsetControl.hurtUnarmedIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(9)
			});
			FrameOffsetControl.armsOffset.Add(FrameOffsetControl.deadIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(11)
			});
			FrameOffsetControl.armsOffset.Add(FrameOffsetControl.spellIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0, 1, 24),
				new FrameOffsetControl.OffsetData(0, 1, 25),
				new FrameOffsetControl.OffsetData(0, 1, 26),
				new FrameOffsetControl.OffsetData(0, 1, 27)
			});
			FrameOffsetControl.armsOffset.Add(FrameOffsetControl.prayIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(37),
				new FrameOffsetControl.OffsetData(37)
			});
			FrameOffsetControl.armsOffset.Add(FrameOffsetControl.despairIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(39),
				new FrameOffsetControl.OffsetData(39),
				new FrameOffsetControl.OffsetData(39),
				new FrameOffsetControl.OffsetData(39),
				new FrameOffsetControl.OffsetData(39)
			});
			FrameOffsetControl.armsOffset.Add(FrameOffsetControl.panicIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(39),
				new FrameOffsetControl.OffsetData(39),
				new FrameOffsetControl.OffsetData(39)
			});
			FrameOffsetControl.armsOffset.Add(FrameOffsetControl.idleSadIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(32),
				new FrameOffsetControl.OffsetData(33)
			});
			FrameOffsetControl.armsOffset.Add(FrameOffsetControl.idleGuardIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(28),
				new FrameOffsetControl.OffsetData(28)
			});
		}
		return FrameOffsetControl.armsOffset;
	}

	// Token: 0x0600045D RID: 1117 RVA: 0x00014EE0 File Offset: 0x000130E0
	private static FrameOffsetControl.OffsetContainer getHeadOffsetControl()
	{
		if (FrameOffsetControl.headOffset == null)
		{
			FrameOffsetControl.headOffset = new FrameOffsetControl.OffsetContainer(FrameOffsetControl.getTorsoOffsetContainer(), -2, 2);
			FrameOffsetControl.headOffset.Add(FrameOffsetControl.bounceIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0, -1),
				new FrameOffsetControl.OffsetData(0, -1),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.headOffset.Add(FrameOffsetControl.idle2HIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0, -1),
				new FrameOffsetControl.OffsetData(0, -1),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.headOffset.Add(FrameOffsetControl.swingIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0, -1),
				new FrameOffsetControl.OffsetData(0, -1),
				new FrameOffsetControl.OffsetData(0, -1),
				new FrameOffsetControl.OffsetData(0, -1),
				new FrameOffsetControl.OffsetData(0, -1)
			});
			FrameOffsetControl.headOffset.Add(FrameOffsetControl.punchIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0, -1),
				new FrameOffsetControl.OffsetData(0, -1),
				new FrameOffsetControl.OffsetData(0, -1)
			});
			FrameOffsetControl.headOffset.Add(FrameOffsetControl.stabIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0, -1),
				new FrameOffsetControl.OffsetData(0, -1),
				new FrameOffsetControl.OffsetData(0, -1)
			});
			FrameOffsetControl.headOffset.Add(FrameOffsetControl.twohandedSwingIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0, -1),
				new FrameOffsetControl.OffsetData(0, -1),
				new FrameOffsetControl.OffsetData(0, -1)
			});
			FrameOffsetControl.headOffset.Add(FrameOffsetControl.bowIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.headOffset.Add(FrameOffsetControl.idleSimpleIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.headOffset.Add(FrameOffsetControl.idleOverlandIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.headOffset.Add(FrameOffsetControl.carousingIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.headOffset.Add(FrameOffsetControl.waitressIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.headOffset.Add(FrameOffsetControl.musicDrumIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0, -1)
			});
			FrameOffsetControl.headOffset.Add(FrameOffsetControl.musicLuteIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0, -1),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.headOffset.Add(FrameOffsetControl.musicFluteIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.headOffset.Add(FrameOffsetControl.jugglingIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.headOffset.Add(FrameOffsetControl.idleFemaleIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.headOffset.Add(FrameOffsetControl.idleTimidIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.headOffset.Add(FrameOffsetControl.idleDandyIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.headOffset.Add(FrameOffsetControl.panicIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.headOffset.Add(FrameOffsetControl.idleFirmIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.headOffset.Add(FrameOffsetControl.hurtIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(1, -1, 0)
			});
			FrameOffsetControl.headOffset.Add(FrameOffsetControl.hurtUnarmedIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(1, -1, 0)
			});
			FrameOffsetControl.headOffset.Add(FrameOffsetControl.deadIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.headOffset.Add(FrameOffsetControl.spellIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.headOffset.Add(FrameOffsetControl.prayIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0, -1, 0)
			});
			FrameOffsetControl.headOffset.Add(FrameOffsetControl.despairIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0, -1, 0),
				new FrameOffsetControl.OffsetData(0, -1, 0),
				new FrameOffsetControl.OffsetData(0, -1, 0)
			});
			FrameOffsetControl.headOffset.Add(FrameOffsetControl.idleSadIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(1, 0, 0),
				new FrameOffsetControl.OffsetData(1, -1, 0)
			});
			FrameOffsetControl.headOffset.Add(FrameOffsetControl.idleGuardIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0, 0, 0, false, true)
			});
		}
		return FrameOffsetControl.headOffset;
	}

	// Token: 0x0600045E RID: 1118 RVA: 0x000154B8 File Offset: 0x000136B8
	private static FrameOffsetControl.OffsetContainer getShieldOffsetControl()
	{
		if (FrameOffsetControl.shieldOffset == null)
		{
			FrameOffsetControl.shieldOffset = new FrameOffsetControl.OffsetContainer(FrameOffsetControl.getArmsOffsetContainer(), 8, -1);
			FrameOffsetControl.shieldOffset.Add(FrameOffsetControl.bounceIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.shieldOffset.Add(FrameOffsetControl.swingIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.shieldOffset.Add(FrameOffsetControl.hurtIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0)
			});
			FrameOffsetControl.shieldOffset.Add(FrameOffsetControl.idleOverlandIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(0)
			});
		}
		return FrameOffsetControl.shieldOffset;
	}

	// Token: 0x0600045F RID: 1119 RVA: 0x000155C0 File Offset: 0x000137C0
	private static FrameOffsetControl.OffsetContainer getLightOffsetControl()
	{
		if (FrameOffsetControl.lightOffset == null)
		{
			FrameOffsetControl.lightOffset = new FrameOffsetControl.OffsetContainer(FrameOffsetControl.getArmsOffsetContainer(), 9, 0);
			FrameOffsetControl.lightOffset.Add(FrameOffsetControl.bounceIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(1, -1, 0),
				new FrameOffsetControl.OffsetData(1, -1, 0),
				new FrameOffsetControl.OffsetData(1, -1, 0),
				new FrameOffsetControl.OffsetData(1, -1, 0)
			});
		}
		return FrameOffsetControl.lightOffset;
	}

	// Token: 0x06000460 RID: 1120 RVA: 0x0001562C File Offset: 0x0001382C
	private static FrameOffsetControl.OffsetContainer getWeaponOffsetControl()
	{
		if (FrameOffsetControl.weaponOffset == null)
		{
			FrameOffsetControl.weaponOffset = new FrameOffsetControl.OffsetContainer(FrameOffsetControl.getArmsOffsetContainer(), -5, -2);
			FrameOffsetControl.weaponOffset.Add(FrameOffsetControl.bounceIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(-2, -3, 0),
				new FrameOffsetControl.OffsetData(-2, -3, 0),
				new FrameOffsetControl.OffsetData(-2, -3, 0),
				new FrameOffsetControl.OffsetData(-2, -3, 0)
			});
			FrameOffsetControl.weaponOffset.Add(FrameOffsetControl.idle2HIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0, 0, 0),
				new FrameOffsetControl.OffsetData(0, 0, 0),
				new FrameOffsetControl.OffsetData(0, 0, 0),
				new FrameOffsetControl.OffsetData(0, 0, 0)
			});
			FrameOffsetControl.weaponOffset.Add(FrameOffsetControl.swingIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(-1, 8, 2),
				new FrameOffsetControl.OffsetData(-2, 8, 3),
				new FrameOffsetControl.OffsetData(1, 8, 3),
				new FrameOffsetControl.OffsetData(10, 6, 2),
				new FrameOffsetControl.OffsetData(9, -1, 1),
				new FrameOffsetControl.OffsetData(-1, -3, 1)
			});
			FrameOffsetControl.weaponOffset.Add(FrameOffsetControl.twohandedSwingIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(-1, 4, 0),
				new FrameOffsetControl.OffsetData(-3, 7, 3),
				new FrameOffsetControl.OffsetData(8, -2, 1),
				new FrameOffsetControl.OffsetData(3, -3, 1)
			});
			FrameOffsetControl.weaponOffset.Add(FrameOffsetControl.idleOverlandIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0, -3, 4),
				new FrameOffsetControl.OffsetData(0, -3, 4),
				new FrameOffsetControl.OffsetData(0, -3, 4),
				new FrameOffsetControl.OffsetData(0, -3, 4)
			});
			FrameOffsetControl.weaponOffset.Add(FrameOffsetControl.bowIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(6, -6, 1),
				new FrameOffsetControl.OffsetData(9, 1, 0, false, true),
				new FrameOffsetControl.OffsetData(9, 1, 0, false, true),
				new FrameOffsetControl.OffsetData(5, -5, 1)
			});
			FrameOffsetControl.weaponOffset.Add(FrameOffsetControl.hurtIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(-1, -3, 0)
			});
			FrameOffsetControl.weaponOffset.Add(FrameOffsetControl.idleGuardIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(1, -4, 0),
				new FrameOffsetControl.OffsetData(0, -4, 0)
			});
		}
		return FrameOffsetControl.weaponOffset;
	}

	// Token: 0x06000461 RID: 1121 RVA: 0x00015860 File Offset: 0x00013A60
	private static FrameOffsetControl.OffsetContainer getSwooshOffsetControl()
	{
		if (FrameOffsetControl.swooshOffset == null)
		{
			FrameOffsetControl.swooshOffset = new FrameOffsetControl.OffsetContainer(FrameOffsetControl.getArmsOffsetContainer(), -5, 2);
			FrameOffsetControl.swooshOffset.Add(FrameOffsetControl.swingIndex + 3, new FrameOffsetControl.OffsetData(11, 7, 1));
			FrameOffsetControl.swooshOffset.Add(FrameOffsetControl.swingIndex + 4, new FrameOffsetControl.OffsetData(11, 0, 0));
		}
		return FrameOffsetControl.swooshOffset;
	}

	// Token: 0x06000462 RID: 1122 RVA: 0x000158C0 File Offset: 0x00013AC0
	private static FrameOffsetControl.OffsetContainer getBannerOffsetControl()
	{
		if (FrameOffsetControl.bannerOffset == null)
		{
			FrameOffsetControl.bannerOffset = new FrameOffsetControl.OffsetContainer(null, -3, 17);
			FrameOffsetControl.bannerOffset.Add(FrameOffsetControl.idleOverlandIndex, new FrameOffsetControl.OffsetData[]
			{
				new FrameOffsetControl.OffsetData(0),
				new FrameOffsetControl.OffsetData(1),
				new FrameOffsetControl.OffsetData(2),
				new FrameOffsetControl.OffsetData(3)
			});
		}
		return FrameOffsetControl.bannerOffset;
	}

	// Token: 0x040000C5 RID: 197
	private static FrameOffsetControl.OffsetContainer torsoOffset;

	// Token: 0x040000C6 RID: 198
	private static FrameOffsetControl.OffsetContainer cloakOffset;

	// Token: 0x040000C7 RID: 199
	private static FrameOffsetControl.OffsetContainer armsOffset;

	// Token: 0x040000C8 RID: 200
	private static FrameOffsetControl.OffsetContainer legsOffset;

	// Token: 0x040000C9 RID: 201
	private static FrameOffsetControl.OffsetContainer headOffset;

	// Token: 0x040000CA RID: 202
	private static FrameOffsetControl.OffsetContainer shieldOffset;

	// Token: 0x040000CB RID: 203
	private static FrameOffsetControl.OffsetContainer lightOffset;

	// Token: 0x040000CC RID: 204
	private static FrameOffsetControl.OffsetContainer weaponOffset;

	// Token: 0x040000CD RID: 205
	private static FrameOffsetControl.OffsetContainer bannerOffset;

	// Token: 0x040000CE RID: 206
	private static FrameOffsetControl.OffsetContainer swooshOffset;

	// Token: 0x040000CF RID: 207
	private static FrameOffsetControl.OffsetContainer globalOffset;

	// Token: 0x040000D0 RID: 208
	private static int bounceIndex = 0;

	// Token: 0x040000D1 RID: 209
	private static int swingIndex = 4;

	// Token: 0x040000D2 RID: 210
	private static int stabIndex = 12;

	// Token: 0x040000D3 RID: 211
	private static int twohandedSwingIndex = 16;

	// Token: 0x040000D4 RID: 212
	private static int bowIndex = 20;

	// Token: 0x040000D5 RID: 213
	private static int hurtIndex = 28;

	// Token: 0x040000D6 RID: 214
	private static int hurtUnarmedIndex = 98;

	// Token: 0x040000D7 RID: 215
	private static int deadIndex = 31;

	// Token: 0x040000D8 RID: 216
	private static int spellIndex = 34;

	// Token: 0x040000D9 RID: 217
	private static int prayIndex = 38;

	// Token: 0x040000DA RID: 218
	private static int despairIndex = 40;

	// Token: 0x040000DB RID: 219
	private static int idleSimpleIndex = 50;

	// Token: 0x040000DC RID: 220
	private static int idleFirmIndex = 54;

	// Token: 0x040000DD RID: 221
	private static int idleFemaleIndex = 58;

	// Token: 0x040000DE RID: 222
	private static int waitressIndex = 64;

	// Token: 0x040000DF RID: 223
	private static int carousingIndex = 66;

	// Token: 0x040000E0 RID: 224
	private static int idle2HIndex = 70;

	// Token: 0x040000E1 RID: 225
	private static int idleSadIndex = 78;

	// Token: 0x040000E2 RID: 226
	private static int idleGuardIndex = 82;

	// Token: 0x040000E3 RID: 227
	private static int punchIndex = 90;

	// Token: 0x040000E4 RID: 228
	private static int idleOverlandIndex = 104;

	// Token: 0x040000E5 RID: 229
	private static int idleDandyIndex = 110;

	// Token: 0x040000E6 RID: 230
	private static int panicIndex = 120;

	// Token: 0x040000E7 RID: 231
	private static int idleTimidIndex = 130;

	// Token: 0x040000E8 RID: 232
	private static int musicDrumIndex = 140;

	// Token: 0x040000E9 RID: 233
	private static int jugglingIndex = 150;

	// Token: 0x040000EA RID: 234
	private static int musicFluteIndex = 160;

	// Token: 0x040000EB RID: 235
	private static int musicLuteIndex = 170;

	// Token: 0x020001CD RID: 461
	private class OffsetContainer
	{
		// Token: 0x060016A7 RID: 5799 RVA: 0x000658D2 File Offset: 0x00063AD2
		public OffsetContainer(FrameOffsetControl.OffsetContainer anchorContainer, int baseXOffset, int baseYOffset) : this(baseXOffset, baseYOffset)
		{
			this.anchorContainer = anchorContainer;
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x000658E3 File Offset: 0x00063AE3
		public OffsetContainer(int baseXOffset, int baseYOffset)
		{
			this.baseXOffset = baseXOffset;
			this.baseYOffset = baseYOffset;
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x00065904 File Offset: 0x00063B04
		public void Add(int startingFrame, FrameOffsetControl.OffsetData[] offsetDataArray)
		{
			if (offsetDataArray == null)
			{
				return;
			}
			for (int i = 0; i < offsetDataArray.Length; i++)
			{
				this.Add(startingFrame + i, offsetDataArray[i]);
			}
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x00065930 File Offset: 0x00063B30
		public void Add(int frame, FrameOffsetControl.OffsetData offsetData)
		{
			offsetData.x += this.baseXOffset;
			offsetData.y += this.baseYOffset;
			if (!this.offsets.ContainsKey(frame))
			{
				if (this.anchorContainer != null)
				{
					offsetData.addAnchorOffset(this.anchorContainer.getOffset(frame));
				}
				this.offsets.Add(frame, offsetData);
				return;
			}
			MainControl.logError("Adding frame to offsetcontainer twice!");
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x000659A3 File Offset: 0x00063BA3
		public FrameOffsetControl.OffsetData getOffset(int frame)
		{
			if (this.offsets == null || !this.offsets.ContainsKey(frame))
			{
				return null;
			}
			return this.offsets[frame];
		}

		// Token: 0x04000714 RID: 1812
		private Dictionary<int, FrameOffsetControl.OffsetData> offsets = new Dictionary<int, FrameOffsetControl.OffsetData>();

		// Token: 0x04000715 RID: 1813
		private FrameOffsetControl.OffsetContainer anchorContainer;

		// Token: 0x04000716 RID: 1814
		private int baseXOffset;

		// Token: 0x04000717 RID: 1815
		private int baseYOffset;
	}

	// Token: 0x020001CE RID: 462
	public class OffsetData
	{
		// Token: 0x060016AC RID: 5804 RVA: 0x000659C9 File Offset: 0x00063BC9
		public OffsetData(int x, int y, int frame, bool rotateLeft, bool flipped) : this(x, y, frame)
		{
			this.rotateLeft = rotateLeft;
			this.flipped = flipped;
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x000659E4 File Offset: 0x00063BE4
		public OffsetData(int x, int y, int frame)
		{
			this.x = x;
			this.y = y;
			this.frame = frame;
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x00065A01 File Offset: 0x00063C01
		public OffsetData(int x, int y) : this(x, y, 0)
		{
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x00065A0C File Offset: 0x00063C0C
		public OffsetData(int frame)
		{
			this.frame = frame;
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x00065A1B File Offset: 0x00063C1B
		public OffsetData()
		{
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x00065A23 File Offset: 0x00063C23
		public void addAnchorOffset(FrameOffsetControl.OffsetData anchorOffset)
		{
			if (anchorOffset != null)
			{
				this.x += anchorOffset.x;
				this.y += anchorOffset.y;
			}
		}

		// Token: 0x04000718 RID: 1816
		public int x;

		// Token: 0x04000719 RID: 1817
		public int y;

		// Token: 0x0400071A RID: 1818
		public int frame;

		// Token: 0x0400071B RID: 1819
		public bool rotateLeft;

		// Token: 0x0400071C RID: 1820
		public bool flipped;
	}
}
