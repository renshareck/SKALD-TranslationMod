using System;
using UnityEngine;

// Token: 0x0200010D RID: 269
[Serializable]
public class SkaldPhysicalObject : SkaldWorldObject
{
	// Token: 0x0600110D RID: 4365 RVA: 0x0004D144 File Offset: 0x0004B344
	protected SkaldPhysicalObject(SKALDProjectData.GameDataObject rawData) : base(rawData)
	{
	}

	// Token: 0x0600110E RID: 4366 RVA: 0x0004D158 File Offset: 0x0004B358
	protected SkaldPhysicalObject()
	{
	}

	// Token: 0x0600110F RID: 4367 RVA: 0x0004D16B File Offset: 0x0004B36B
	public VisualEffectsControl getVisualEffects()
	{
		if (this.visualEffects == null)
		{
			this.visualEffects = new VisualEffectsControl(this);
		}
		return this.visualEffects;
	}

	// Token: 0x06001110 RID: 4368 RVA: 0x0004D187 File Offset: 0x0004B387
	public virtual void updateParticleEffects(int xOffset, int yOffset, TextureTools.TextureData targetTexture)
	{
		if (this.visualEffects == null)
		{
			return;
		}
		this.getVisualEffects().updateVisualEffects(xOffset, yOffset, targetTexture);
	}

	// Token: 0x06001111 RID: 4369 RVA: 0x0004D1A0 File Offset: 0x0004B3A0
	public float getLightEffectStrength()
	{
		if (this.visualEffects == null)
		{
			return 0f;
		}
		return this.getVisualEffects().getLightStrength();
	}

	// Token: 0x06001112 RID: 4370 RVA: 0x0004D1BB File Offset: 0x0004B3BB
	public int getLightEffectDistance()
	{
		if (this.visualEffects == null)
		{
			return 0;
		}
		return (int)this.getVisualEffects().getLightDistance();
	}

	// Token: 0x06001113 RID: 4371 RVA: 0x0004D1D3 File Offset: 0x0004B3D3
	public override int getPixelX()
	{
		return this.physicsControl.getCurrentXInt();
	}

	// Token: 0x06001114 RID: 4372 RVA: 0x0004D1E0 File Offset: 0x0004B3E0
	public override int getPixelY()
	{
		return this.physicsControl.getCurrentYInt();
	}

	// Token: 0x06001115 RID: 4373 RVA: 0x0004D1ED File Offset: 0x0004B3ED
	public virtual int getEmitterX()
	{
		return this.getPixelX();
	}

	// Token: 0x06001116 RID: 4374 RVA: 0x0004D1F5 File Offset: 0x0004B3F5
	public virtual int getEmitterY()
	{
		return this.getPixelY();
	}

	// Token: 0x06001117 RID: 4375 RVA: 0x0004D1FD File Offset: 0x0004B3FD
	public int getBounceOffset()
	{
		if (MapIllustrator.SCROLL_SPEED > 2)
		{
			return 0;
		}
		return 1 - Mathf.RoundToInt(Mathf.Sin(6.2831855f * (this.physicsControl.getDistanceToTarget() % 16f / 16f)));
	}

	// Token: 0x06001118 RID: 4376 RVA: 0x0004D232 File Offset: 0x0004B432
	public override void setPixelPosition(int x, int y)
	{
		this.physicsControl.setPosition(x, y);
	}

	// Token: 0x06001119 RID: 4377 RVA: 0x0004D241 File Offset: 0x0004B441
	public override void setTilePosition(int x, int y, string mapId)
	{
		base.setTilePosition(x, y, mapId);
	}

	// Token: 0x0600111A RID: 4378 RVA: 0x0004D24C File Offset: 0x0004B44C
	public void setPixelTarget(int x, int y, float maxSpeed)
	{
		this.physicsControl.setTarget(x, y, maxSpeed);
	}

	// Token: 0x0600111B RID: 4379 RVA: 0x0004D25C File Offset: 0x0004B45C
	public void setPixelTarget(SkaldPhysicalObject obj, float maxSpeed)
	{
		this.physicsControl.setTarget(obj, maxSpeed);
	}

	// Token: 0x0600111C RID: 4380 RVA: 0x0004D26B File Offset: 0x0004B46B
	public void setPixelTargetInterpolated(int x, int y, float maxSpeed, float interpolation)
	{
		this.physicsControl.setPixelTargetInterpolated(x, y, maxSpeed, interpolation);
	}

	// Token: 0x0600111D RID: 4381 RVA: 0x0004D27D File Offset: 0x0004B47D
	public virtual void updatePhysics()
	{
		this.setPixelTarget(this.getTileX() * 16, this.getTileY() * 16, (float)MapIllustrator.SCROLL_SPEED);
		this.physicsControl.update();
	}

	// Token: 0x0600111E RID: 4382 RVA: 0x0004D2A8 File Offset: 0x0004B4A8
	public void snapToGrid()
	{
		this.physicsControl.setPosition(this.getTileX() * 16, this.getTileY() * 16);
	}

	// Token: 0x0600111F RID: 4383 RVA: 0x0004D2C7 File Offset: 0x0004B4C7
	protected void clearPhysicsStates()
	{
		this.physicsControl.clearStates();
	}

	// Token: 0x06001120 RID: 4384 RVA: 0x0004D2D4 File Offset: 0x0004B4D4
	protected void resetPhysics()
	{
		int currentXInt = this.physicsControl.getCurrentXInt();
		int currentYInt = this.physicsControl.getCurrentYInt();
		this.physicsControl = new SkaldPhysicalObject.SkaldObjectPhysicsControl();
		this.setPixelPosition(currentXInt, currentYInt);
	}

	// Token: 0x06001121 RID: 4385 RVA: 0x0004D30C File Offset: 0x0004B50C
	public void setTargetHeight(int targetHeight)
	{
		this.physicsControl.setTargetHeight(targetHeight);
	}

	// Token: 0x06001122 RID: 4386 RVA: 0x0004D31A File Offset: 0x0004B51A
	public void setConcealmentOffset(int targetHeight)
	{
		this.physicsControl.setConcealmentOffset(targetHeight);
	}

	// Token: 0x06001123 RID: 4387 RVA: 0x0004D328 File Offset: 0x0004B528
	public int getConcealmentOffset()
	{
		return this.physicsControl.getConcealmentOffset();
	}

	// Token: 0x06001124 RID: 4388 RVA: 0x0004D335 File Offset: 0x0004B535
	public int getCurrentHeight()
	{
		return this.physicsControl.getCurrentHeight();
	}

	// Token: 0x06001125 RID: 4389 RVA: 0x0004D342 File Offset: 0x0004B542
	public virtual bool physicMovementComplete()
	{
		if (this.physicsControl.isDead())
		{
			return true;
		}
		if (this.physicsControl.isMoving())
		{
			return false;
		}
		this.getTileX();
		this.getTileY();
		return true;
	}

	// Token: 0x06001126 RID: 4390 RVA: 0x0004D371 File Offset: 0x0004B571
	public void applyImpulsePush(SkaldPhysicalObject obj)
	{
		this.physicsControl.applyImpulsePush(obj);
	}

	// Token: 0x06001127 RID: 4391 RVA: 0x0004D37F File Offset: 0x0004B57F
	protected void clearFlags()
	{
		this.physicsControl.clearHoming();
	}

	// Token: 0x04000405 RID: 1029
	private SkaldPhysicalObject.SkaldObjectPhysicsControl physicsControl = new SkaldPhysicalObject.SkaldObjectPhysicsControl();

	// Token: 0x04000406 RID: 1030
	private VisualEffectsControl visualEffects;

	// Token: 0x0200025B RID: 603
	[Serializable]
	private class SkaldObjectPhysicsControl
	{
		// Token: 0x060019CB RID: 6603 RVA: 0x00070F64 File Offset: 0x0006F164
		public int getCurrentXInt()
		{
			return Mathf.RoundToInt(this.currentX);
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x00070F71 File Offset: 0x0006F171
		public void jumpToPixelTarget()
		{
			this.currentX = this.targetX;
			this.currentY = this.targetY;
			this.currentHeight = this.targetHeight;
			this.currentConcealmentDegree = this.targetConcealmentDegree;
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x00070FA3 File Offset: 0x0006F1A3
		public int getCurrentHeight()
		{
			return this.currentHeight;
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x00070FAB File Offset: 0x0006F1AB
		public int getCurrentYInt()
		{
			return Mathf.RoundToInt(this.currentY);
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x00070FB8 File Offset: 0x0006F1B8
		public void setImpulse(float _speed, float _rotation)
		{
			this.currentSpeed = _speed;
			this.rotation = _rotation;
			this.gliding = true;
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x00070FCF File Offset: 0x0006F1CF
		public void clearStates()
		{
			this.gliding = false;
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x00070FD8 File Offset: 0x0006F1D8
		public void setPosition(int x, int y)
		{
			this.currentX = (float)x;
			this.currentY = (float)y;
			this.targetX = (float)x;
			this.targetY = (float)y;
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x00070FFA File Offset: 0x0006F1FA
		public void applyImpulsePush(SkaldPhysicalObject obj)
		{
			obj.physicsControl.setImpulse(Random.Range(1.5f, 2.5f), this.rotation);
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x0007101C File Offset: 0x0006F21C
		public float getDistanceToTarget()
		{
			return NavigationTools.getLinearDistance(this.currentX, this.currentY, this.targetX, this.targetY);
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x0007103B File Offset: 0x0006F23B
		public void setTargetHeight(int height)
		{
			this.targetHeight = height;
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x00071044 File Offset: 0x0006F244
		public void setConcealmentOffset(int target)
		{
			this.targetConcealmentDegree = target;
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x0007104D File Offset: 0x0006F24D
		public int getConcealmentOffset()
		{
			return this.currentConcealmentDegree;
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x00071055 File Offset: 0x0006F255
		private void adjustCurrentHeight()
		{
			if (this.currentHeight < this.targetHeight)
			{
				this.currentHeight++;
			}
			if (this.currentHeight > this.targetHeight)
			{
				this.currentHeight--;
			}
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x0007108F File Offset: 0x0006F28F
		private void adjustConcealment()
		{
			if (this.currentConcealmentDegree < this.targetConcealmentDegree)
			{
				this.currentConcealmentDegree++;
			}
			if (this.currentConcealmentDegree > this.targetConcealmentDegree)
			{
				this.currentConcealmentDegree--;
			}
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x000710C9 File Offset: 0x0006F2C9
		public void setTarget(int x, int y, float _maxSpeed)
		{
			if (this.homing)
			{
				return;
			}
			this.maxSpeed = _maxSpeed;
			this.targetX = (float)x;
			this.targetY = (float)y;
			if (this.getDistanceToTarget() > 32f)
			{
				this.jumpToPixelTarget();
				return;
			}
			this.homing = true;
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x00071108 File Offset: 0x0006F308
		public void setTarget(SkaldWorldObject targetObj, float _maxSpeed)
		{
			if (this.homing)
			{
				return;
			}
			if (targetObj == null)
			{
				return;
			}
			int x = (int)(this.currentX + ((float)targetObj.getPixelX() - this.currentX) * 0.5f);
			int y = (int)(this.currentY + ((float)targetObj.getPixelY() - this.currentY) * 0.5f);
			this.setTarget(x, y, _maxSpeed);
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x00071168 File Offset: 0x0006F368
		public void setPixelTargetInterpolated(int x, int y, float maxSpeed, float interpolation)
		{
			if (this.homing)
			{
				return;
			}
			if (interpolation <= 0f)
			{
				return;
			}
			x = Mathf.RoundToInt(this.currentX + ((float)x - this.currentX) * interpolation);
			y = Mathf.RoundToInt(this.currentY + ((float)y - this.currentY) * interpolation);
			this.setTarget(x, y, maxSpeed);
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x000711C8 File Offset: 0x0006F3C8
		public string printData()
		{
			return string.Concat(new string[]
			{
				"PHYSICS: Current: ",
				this.getCurrentXInt().ToString(),
				"/",
				this.getCurrentYInt().ToString(),
				" Target: ",
				this.targetX.ToString(),
				"/",
				this.targetY.ToString(),
				" Speed: ",
				this.currentSpeed.ToString()
			});
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x00071258 File Offset: 0x0006F458
		private void homeTowards(int targetX, int targetY)
		{
			float y = (float)(targetX - this.getCurrentXInt());
			float x = (float)(targetY - this.getCurrentYInt());
			this.rotation = Mathf.Atan2(y, x);
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x00071286 File Offset: 0x0006F486
		public bool isDead()
		{
			return this.gliding;
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x0007128E File Offset: 0x0006F48E
		public bool isMoving()
		{
			return !this.isDead() && (this.currentSpeed != 0f || !this.hasArrivedAtTarget());
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x000712B4 File Offset: 0x0006F4B4
		private bool hasArrivedAtTarget()
		{
			return this.currentX == this.targetX && this.currentY == this.targetY;
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x000712D5 File Offset: 0x0006F4D5
		public void clearHoming()
		{
			this.homing = false;
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x000712E0 File Offset: 0x0006F4E0
		public void update()
		{
			this.adjustCurrentHeight();
			this.adjustConcealment();
			if (this.gliding && this.currentSpeed > 0f)
			{
				this.currentSpeed -= this.drag;
			}
			if (this.currentSpeed < 0f + this.deadSpace)
			{
				this.currentSpeed = 0f;
			}
			if (this.homing && !this.gliding)
			{
				this.homeTowards((int)this.targetX, (int)this.targetY);
				if (this.currentX < this.targetX + this.maxSpeed && this.currentX > this.targetX - this.maxSpeed && this.currentY < this.targetY + this.maxSpeed && this.currentY > this.targetY - this.maxSpeed)
				{
					this.jumpToPixelTarget();
					this.currentSpeed = 0f;
				}
				else
				{
					this.homing = true;
					if (this.currentSpeed < this.maxSpeed)
					{
						this.currentSpeed += this.acceleration;
					}
					if (this.currentSpeed > this.maxSpeed)
					{
						this.currentSpeed = this.maxSpeed;
					}
				}
			}
			this.currentX += Mathf.Sin(this.rotation) * this.currentSpeed;
			this.currentY += Mathf.Cos(this.rotation) * this.currentSpeed;
		}

		// Token: 0x0400094F RID: 2383
		private float currentX;

		// Token: 0x04000950 RID: 2384
		private float currentY;

		// Token: 0x04000951 RID: 2385
		private float targetX;

		// Token: 0x04000952 RID: 2386
		private float targetY;

		// Token: 0x04000953 RID: 2387
		private float rotation;

		// Token: 0x04000954 RID: 2388
		private float acceleration = 2f;

		// Token: 0x04000955 RID: 2389
		private float currentSpeed;

		// Token: 0x04000956 RID: 2390
		private float drag = 0.2f;

		// Token: 0x04000957 RID: 2391
		private float maxSpeed = 8f;

		// Token: 0x04000958 RID: 2392
		private float deadSpace = 0.11f;

		// Token: 0x04000959 RID: 2393
		private bool homing;

		// Token: 0x0400095A RID: 2394
		private bool gliding;

		// Token: 0x0400095B RID: 2395
		private int targetHeight;

		// Token: 0x0400095C RID: 2396
		private int targetConcealmentDegree;

		// Token: 0x0400095D RID: 2397
		private int currentHeight;

		// Token: 0x0400095E RID: 2398
		private int currentConcealmentDegree;
	}
}
