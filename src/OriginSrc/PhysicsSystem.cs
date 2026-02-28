using System;
using UnityEngine;

// Token: 0x02000196 RID: 406
public class PhysicsSystem
{
	// Token: 0x0600152D RID: 5421 RVA: 0x0005DDE8 File Offset: 0x0005BFE8
	public void update()
	{
		this.applyGravity();
		this.applySpeed();
		this.testGroundColission();
	}

	// Token: 0x0600152E RID: 5422 RVA: 0x0005DDFC File Offset: 0x0005BFFC
	public void jump()
	{
		this.ySpeed = Random.Range(3f, 5f);
	}

	// Token: 0x0600152F RID: 5423 RVA: 0x0005DE13 File Offset: 0x0005C013
	public int getXPos()
	{
		return Mathf.RoundToInt(this.xPos);
	}

	// Token: 0x06001530 RID: 5424 RVA: 0x0005DE20 File Offset: 0x0005C020
	public int getYPos()
	{
		return Mathf.RoundToInt(this.yPos);
	}

	// Token: 0x06001531 RID: 5425 RVA: 0x0005DE2D File Offset: 0x0005C02D
	private void applyGravity()
	{
		this.ySpeed -= this.gravity;
	}

	// Token: 0x06001532 RID: 5426 RVA: 0x0005DE42 File Offset: 0x0005C042
	private void applySpeed()
	{
		this.xPos += this.xSpeed;
		this.yPos += this.ySpeed;
	}

	// Token: 0x06001533 RID: 5427 RVA: 0x0005DE6A File Offset: 0x0005C06A
	private void testGroundColission()
	{
		if (this.yPos <= this.groundY)
		{
			this.yPos = this.groundY;
			this.bounce();
		}
	}

	// Token: 0x06001534 RID: 5428 RVA: 0x0005DE8C File Offset: 0x0005C08C
	private void bounce()
	{
		float num = Mathf.Abs(this.ySpeed);
		if (num >= this.bounceTreshold)
		{
			this.ySpeed = num * this.bounceAmount;
			return;
		}
		this.ySpeed = 0f;
	}

	// Token: 0x0400056D RID: 1389
	private float gravity = 0.2f;

	// Token: 0x0400056E RID: 1390
	private float xSpeed;

	// Token: 0x0400056F RID: 1391
	private float ySpeed;

	// Token: 0x04000570 RID: 1392
	private float bounceAmount = 0.6f;

	// Token: 0x04000571 RID: 1393
	private float bounceTreshold = 0.01f;

	// Token: 0x04000572 RID: 1394
	private float groundY;

	// Token: 0x04000573 RID: 1395
	private float xPos;

	// Token: 0x04000574 RID: 1396
	private float yPos;
}
