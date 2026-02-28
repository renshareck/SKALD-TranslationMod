using System;
using System.Runtime.Serialization;

// Token: 0x020000FD RID: 253
[Serializable]
public abstract class PropLockable : PropActivatable, ISerializable
{
	// Token: 0x06001033 RID: 4147 RVA: 0x0004A787 File Offset: 0x00048987
	public PropLockable(SKALDProjectData.PropContainers.LockableProp rawData) : base(rawData)
	{
		this.dynamicData.locked = rawData.locked;
	}

	// Token: 0x06001034 RID: 4148 RVA: 0x0004A7A1 File Offset: 0x000489A1
	public PropLockable()
	{
	}

	// Token: 0x06001035 RID: 4149 RVA: 0x0004A7A9 File Offset: 0x000489A9
	public PropLockable(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}

	// Token: 0x06001036 RID: 4150 RVA: 0x0004A7B4 File Offset: 0x000489B4
	public override string getInspectDescription()
	{
		string text = base.getInspectDescription();
		if (this.isLocked())
		{
			text += "\n\n[Locked]";
		}
		return text;
	}

	// Token: 0x06001037 RID: 4151 RVA: 0x0004A7E0 File Offset: 0x000489E0
	private new SKALDProjectData.PropContainers.LockableProp getRawData()
	{
		SKALDProjectData.PropContainers.Prop rawData = base.getRawData();
		if (rawData is SKALDProjectData.PropContainers.LockableProp)
		{
			return rawData as SKALDProjectData.PropContainers.LockableProp;
		}
		return null;
	}

	// Token: 0x06001038 RID: 4152 RVA: 0x0004A804 File Offset: 0x00048A04
	protected bool launchLockDialogue()
	{
		return this.isLocked() && (this.isForceable() || this.isPickable());
	}

	// Token: 0x06001039 RID: 4153 RVA: 0x0004A820 File Offset: 0x00048A20
	protected bool isForceable()
	{
		return this.getRawData().forceable;
	}

	// Token: 0x0600103A RID: 4154 RVA: 0x0004A82D File Offset: 0x00048A2D
	protected bool isPickable()
	{
		return this.getRawData().pickable;
	}

	// Token: 0x0600103B RID: 4155 RVA: 0x0004A83A File Offset: 0x00048A3A
	public bool isLocked()
	{
		return this.dynamicData.locked;
	}

	// Token: 0x0600103C RID: 4156 RVA: 0x0004A847 File Offset: 0x00048A47
	public bool readyToMount()
	{
		return !this.isLocked() && !base.isHidden();
	}

	// Token: 0x0600103D RID: 4157 RVA: 0x0004A85C File Offset: 0x00048A5C
	public bool tryLock()
	{
		base.makeFactionHostile();
		if (base.hasTrap())
		{
			base.addVocalBark("A trap!");
			base.triggerTrap();
			return true;
		}
		if (!this.isLocked())
		{
			return false;
		}
		if (this.testKey())
		{
			this.unlock();
			return false;
		}
		if (this.launchLockDialogue())
		{
			PopUpControl.addPopUpLock(this);
			return true;
		}
		base.addVocalBark("Locked!");
		return true;
	}

	// Token: 0x0600103E RID: 4158 RVA: 0x0004A8C4 File Offset: 0x00048AC4
	private int getPickDC()
	{
		SKALDProjectData.PropContainers.LockableProp rawData = this.getRawData();
		if (rawData == null)
		{
			return 0;
		}
		return rawData.pickDifficulty;
	}

	// Token: 0x0600103F RID: 4159 RVA: 0x0004A8E4 File Offset: 0x00048AE4
	private int getForceDC()
	{
		SKALDProjectData.PropContainers.LockableProp rawData = this.getRawData();
		if (rawData == null)
		{
			return 0;
		}
		return rawData.forceDifficulty;
	}

	// Token: 0x06001040 RID: 4160 RVA: 0x0004A903 File Offset: 0x00048B03
	private bool isLockBroken()
	{
		return base.getCurrentHP() <= 0;
	}

	// Token: 0x06001041 RID: 4161 RVA: 0x0004A911 File Offset: 0x00048B11
	public void dealDamageToLock(int amount)
	{
		this.dynamicData.hitPoints -= amount;
		if (this.isLockBroken())
		{
			this.unlock();
		}
	}

	// Token: 0x06001042 RID: 4162 RVA: 0x0004A934 File Offset: 0x00048B34
	public void dealDamageToStructure(int amount)
	{
		this.dynamicData.hitPoints -= amount;
		if (this.isLockBroken())
		{
			this.unlock();
			MainControl.getDataControl().shakeScreen("30");
			this.deactivate();
		}
	}

	// Token: 0x06001043 RID: 4163 RVA: 0x0004A970 File Offset: 0x00048B70
	public override string getDescription()
	{
		return string.Concat(new string[]
		{
			this.coreData.description,
			" (",
			C64Color.ATTRIBUTE_VALUE_TAG,
			base.getCurrentHP().ToString(),
			" HP</color>)"
		});
	}

	// Token: 0x06001044 RID: 4164 RVA: 0x0004A9C0 File Offset: 0x00048BC0
	public string getLockDescription()
	{
		string text = "";
		Character currentPC = MainControl.getDataControl().getCurrentPC();
		int currentAttributeValue = currentPC.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_Thievery);
		int currentAttributeValue2 = currentPC.getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_Athletics);
		if (this.isPickable())
		{
			text = string.Concat(new string[]
			{
				text,
				C64Color.ATTRIBUTE_NAME_TAG,
				"PICK</color>: Thievery ",
				C64Color.ATTRIBUTE_VALUE_TAG,
				currentAttributeValue.ToString(),
				"</color> vs Difficulty ",
				C64Color.ATTRIBUTE_VALUE_TAG,
				this.getPickDC().ToString(),
				"</color>\n"
			});
		}
		else
		{
			text += "Not Pickable.\n";
		}
		if (this.isForceable())
		{
			text = string.Concat(new string[]
			{
				text,
				C64Color.ATTRIBUTE_NAME_TAG,
				"FORCE</color>:  Athletics ",
				C64Color.ATTRIBUTE_VALUE_TAG,
				currentAttributeValue2.ToString(),
				"</color> vs Difficulty ",
				C64Color.ATTRIBUTE_VALUE_TAG,
				this.getForceDC().ToString(),
				"</color>\n"
			});
		}
		else
		{
			text += "Not Forceable.\n\n";
		}
		return text;
	}

	// Token: 0x06001045 RID: 4165 RVA: 0x0004AAD4 File Offset: 0x00048CD4
	public SkaldTestBase pick()
	{
		if (!this.isPickable())
		{
			return new SkaldTestAutoFail("This lock cannot be picked!");
		}
		if (!this.isLocked())
		{
			return new SkaldTestAutoFail("This lock is already open!");
		}
		SkaldTestBase skaldTestBase = MainControl.getDataControl().getCurrentPC().pickLock(this.getPickDC());
		base.getMapTile().playMoveSound();
		if (skaldTestBase.wasSuccess())
		{
			this.unlock();
		}
		if (!this.isLocked())
		{
			MainControl.getDataControl().getParty().addXp(this.getPickDC());
		}
		return skaldTestBase;
	}

	// Token: 0x06001046 RID: 4166 RVA: 0x0004AB54 File Offset: 0x00048D54
	public SkaldTestBase force()
	{
		if (!this.isForceable())
		{
			return new SkaldTestAutoFail("This lock cannot be forced!");
		}
		if (!this.isLocked())
		{
			return new SkaldTestAutoFail("This lock is already open!");
		}
		SkaldTestBase skaldTestBase = MainControl.getDataControl().getCurrentPC().forceLock(this.getForceDC());
		if (skaldTestBase.wasSuccess())
		{
			this.dealDamageToStructure(skaldTestBase.getDegreeOfResult());
			MainControl.getDataControl().alertNearbyAll();
		}
		if (!this.isLocked())
		{
			MainControl.getDataControl().getParty().addXp(this.getForceDC());
			AudioControl.playSound("ChestBreaking1");
		}
		else
		{
			AudioControl.playSound("ChestHit1");
		}
		return skaldTestBase;
	}

	// Token: 0x06001047 RID: 4167 RVA: 0x0004ABF0 File Offset: 0x00048DF0
	private string getKey()
	{
		SKALDProjectData.PropContainers.LockableProp rawData = this.getRawData();
		if (rawData == null)
		{
			return "";
		}
		return rawData.key;
	}

	// Token: 0x06001048 RID: 4168 RVA: 0x0004AC13 File Offset: 0x00048E13
	private bool testKey()
	{
		return this.getKey() != "" && MainControl.getDataControl().getInventory().containsObject(this.getRawData().key);
	}

	// Token: 0x06001049 RID: 4169 RVA: 0x0004AC43 File Offset: 0x00048E43
	public void unlock()
	{
		this.dynamicData.locked = false;
		base.addVocalBark("Unlocked");
		AudioControl.playUnlockSound();
	}

	// Token: 0x0600104A RID: 4170 RVA: 0x0004AC62 File Offset: 0x00048E62
	public override string interactWithProp()
	{
		if (base.isHidden())
		{
			return "";
		}
		string result = base.processVerbTrigger();
		if (base.shouldAutoToggle())
		{
			this.deactivate();
		}
		return result;
	}
}
