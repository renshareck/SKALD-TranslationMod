using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

// Token: 0x02000015 RID: 21
[Serializable]
public class ConditionContainer : AbilityContainerPassive, ISerializable
{
	// Token: 0x06000140 RID: 320 RVA: 0x000079E0 File Offset: 0x00005BE0
	public ConditionContainer(Character owner) : base(owner)
	{
	}

	// Token: 0x06000141 RID: 321 RVA: 0x000079E9 File Offset: 0x00005BE9
	public ConditionContainer(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}

	// Token: 0x06000142 RID: 322 RVA: 0x000079F3 File Offset: 0x00005BF3
	public new void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		base.GetObjectData(info, context);
	}

	// Token: 0x06000143 RID: 323 RVA: 0x000079FD File Offset: 0x00005BFD
	protected override List<string> getItemComponentIdList()
	{
		return this.owner.getConferredConditionsFromItems();
	}

	// Token: 0x06000144 RID: 324 RVA: 0x00007A0C File Offset: 0x00005C0C
	public override SkaldDataList getDataList()
	{
		SkaldDataList skaldDataList = new SkaldDataList("Active Conditions", "The conditions that currently affects this character.");
		List<BaseCharacterComponent> componentList = this.getComponentList();
		if (componentList.Count == 0)
		{
			skaldDataList.addEntry("CONDITION", "-Empty-", "No conditions are currently active.");
		}
		else
		{
			foreach (BaseCharacterComponent baseCharacterComponent in componentList)
			{
				Condition condition = (Condition)baseCharacterComponent;
				string text = condition.getModelPath();
				if (text != "")
				{
					text = "Images/GUIIcons/PortraitIcons/" + text;
				}
				skaldDataList.addEntry(condition.getId(), condition.getName(), "", condition.getFullDescription(), text, "");
			}
		}
		return skaldDataList;
	}

	// Token: 0x06000145 RID: 325 RVA: 0x00007AD8 File Offset: 0x00005CD8
	public void addCondition(string conditionId, Character character)
	{
		if (base.hasComponent(conditionId))
		{
			return;
		}
		Condition condition = GameData.getCondition(conditionId);
		if (condition == null)
		{
			return;
		}
		if (condition.testResistance(character))
		{
			return;
		}
		condition.applyCondition(character);
		base.addComponent(condition);
	}

	// Token: 0x06000146 RID: 326 RVA: 0x00007B12 File Offset: 0x00005D12
	public override List<BaseCharacterComponent> getCombatActivatedComponentList()
	{
		return new List<BaseCharacterComponent>();
	}

	// Token: 0x06000147 RID: 327 RVA: 0x00007B1C File Offset: 0x00005D1C
	public int getLightRadius()
	{
		int num = 0;
		foreach (BaseCharacterComponent baseCharacterComponent in this.getComponentList())
		{
			Condition condition = (Condition)baseCharacterComponent;
			if (condition.getLightRadius() > num)
			{
				num = condition.getLightRadius();
			}
		}
		return num;
	}

	// Token: 0x06000148 RID: 328 RVA: 0x00007B80 File Offset: 0x00005D80
	public float getLightDegree()
	{
		float num = 0f;
		foreach (BaseCharacterComponent baseCharacterComponent in this.getComponentList())
		{
			Condition condition = (Condition)baseCharacterComponent;
			if (condition.getLightDegree() > num)
			{
				num = condition.getLightDegree();
			}
		}
		return num;
	}

	// Token: 0x06000149 RID: 329 RVA: 0x00007BE8 File Offset: 0x00005DE8
	public string getLightImage()
	{
		if ((double)this.getLightDegree() < 0.1)
		{
			return "";
		}
		foreach (BaseCharacterComponent baseCharacterComponent in this.getComponentList())
		{
			Condition condition = (Condition)baseCharacterComponent;
			if (condition.getLightImage() != "")
			{
				return condition.getLightImage();
			}
		}
		return "";
	}

	// Token: 0x0600014A RID: 330 RVA: 0x00007C74 File Offset: 0x00005E74
	public void clearEndOfCombatConditions()
	{
		foreach (BaseCharacterComponent baseCharacterComponent in this.getComponentList())
		{
			Condition condition = (Condition)baseCharacterComponent;
			if (condition.clearAtEndOfCombat(this.owner))
			{
				this.deleteCondition(condition);
			}
		}
	}

	// Token: 0x0600014B RID: 331 RVA: 0x00007CDC File Offset: 0x00005EDC
	public void clearRestConditions()
	{
		foreach (BaseCharacterComponent baseCharacterComponent in this.getComponentList())
		{
			Condition condition = (Condition)baseCharacterComponent;
			if (condition.clearAtRest(this.owner))
			{
				this.deleteCondition(condition);
			}
		}
	}

	// Token: 0x0600014C RID: 332 RVA: 0x00007D44 File Offset: 0x00005F44
	public void clearNegativeConditions()
	{
		foreach (BaseCharacterComponent baseCharacterComponent in this.getComponentList())
		{
			Condition condition = (Condition)baseCharacterComponent;
			if (!condition.isAdvantage())
			{
				this.deleteCondition(condition);
			}
		}
	}

	// Token: 0x0600014D RID: 333 RVA: 0x00007DA4 File Offset: 0x00005FA4
	public void clearAllConditions()
	{
		foreach (BaseCharacterComponent baseCharacterComponent in this.getComponentList())
		{
			Condition c = (Condition)baseCharacterComponent;
			this.deleteCondition(c);
		}
	}

	// Token: 0x0600014E RID: 334 RVA: 0x00007DFC File Offset: 0x00005FFC
	public void purgeInvisibility()
	{
		this.deleteByBaseConditionCaused("Invisible");
	}

	// Token: 0x0600014F RID: 335 RVA: 0x00007E0C File Offset: 0x0000600C
	public void deleteByBaseConditionCaused(string baseCondition)
	{
		foreach (BaseCharacterComponent baseCharacterComponent in this.getComponentList())
		{
			Condition condition = (Condition)baseCharacterComponent;
			if (condition.causesBaseCondition(baseCondition))
			{
				this.deleteCondition(condition);
			}
		}
	}

	// Token: 0x06000150 RID: 336 RVA: 0x00007E70 File Offset: 0x00006070
	public void clearEndOfTurnConditions()
	{
		foreach (BaseCharacterComponent baseCharacterComponent in this.getComponentList())
		{
			Condition condition = (Condition)baseCharacterComponent;
			if (condition.clearAtEndOfTurn(this.owner))
			{
				this.deleteCondition(condition);
			}
		}
	}

	// Token: 0x06000151 RID: 337 RVA: 0x00007ED8 File Offset: 0x000060D8
	public void clearStartOfTurnConditions()
	{
		foreach (BaseCharacterComponent baseCharacterComponent in this.getComponentList())
		{
			Condition condition = (Condition)baseCharacterComponent;
			if (condition.clearAtStartOfTurn())
			{
				this.deleteCondition(condition);
			}
		}
	}

	// Token: 0x06000152 RID: 338 RVA: 0x00007F38 File Offset: 0x00006138
	private void deleteCondition(Condition c)
	{
		this.owner.addInfoBark(c.getName().ToUpper() + " ended");
		CombatLog.addEntry(this.owner.getNameColored() + ": " + c.getName() + " ended");
		this.deleteComponent(c.getId());
	}

	// Token: 0x06000153 RID: 339 RVA: 0x00007F98 File Offset: 0x00006198
	public override List<BaseCharacterComponent> getComponentList()
	{
		List<BaseCharacterComponent> list = new List<BaseCharacterComponent>();
		foreach (string id in base.getComponentIdList())
		{
			list.Add(GameData.getCondition(id));
		}
		return list;
	}

	// Token: 0x06000154 RID: 340 RVA: 0x00007FF8 File Offset: 0x000061F8
	public override BaseCharacterComponent forcedGetComponentFromIdWithoutCheckingIfIHaveIt(string id)
	{
		return GameData.getCondition(id);
	}

	// Token: 0x06000155 RID: 341 RVA: 0x00008000 File Offset: 0x00006200
	public List<TextureTools.TextureData> getConditionIcons(List<TextureTools.TextureData> inputList)
	{
		List<string> list = new List<string>();
		foreach (BaseCharacterComponent baseCharacterComponent in this.getComponentList())
		{
			Condition condition = (Condition)baseCharacterComponent;
			string modelPath = condition.getModelPath();
			if (!list.Contains(modelPath))
			{
				TextureTools.TextureData icon = condition.getIcon();
				if (icon != null)
				{
					inputList.Add(icon);
					list.Add(modelPath);
				}
			}
		}
		return inputList;
	}

	// Token: 0x06000156 RID: 342 RVA: 0x00008084 File Offset: 0x00006284
	public bool hasNegativeCondition()
	{
		using (List<BaseCharacterComponent>.Enumerator enumerator = this.getComponentList().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!((Condition)enumerator.Current).isAdvantage())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000157 RID: 343 RVA: 0x000080E4 File Offset: 0x000062E4
	public bool isPoisoned()
	{
		using (List<BaseCharacterComponent>.Enumerator enumerator = this.getComponentList().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((Condition)enumerator.Current).isPoisoned())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000158 RID: 344 RVA: 0x00008144 File Offset: 0x00006344
	public bool isMarked()
	{
		using (List<BaseCharacterComponent>.Enumerator enumerator = this.getComponentList().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((Condition)enumerator.Current).isMarked())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000159 RID: 345 RVA: 0x000081A4 File Offset: 0x000063A4
	public bool isInvisible()
	{
		using (List<BaseCharacterComponent>.Enumerator enumerator = this.getComponentList().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((Condition)enumerator.Current).isInvisible())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600015A RID: 346 RVA: 0x00008204 File Offset: 0x00006404
	public bool isBleeding()
	{
		using (List<BaseCharacterComponent>.Enumerator enumerator = this.getComponentList().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((Condition)enumerator.Current).isBleeding())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600015B RID: 347 RVA: 0x00008264 File Offset: 0x00006464
	public bool isDefending()
	{
		using (List<BaseCharacterComponent>.Enumerator enumerator = this.getComponentList().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((Condition)enumerator.Current).isDefending())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600015C RID: 348 RVA: 0x000082C4 File Offset: 0x000064C4
	public bool isBlind()
	{
		using (List<BaseCharacterComponent>.Enumerator enumerator = this.getComponentList().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((Condition)enumerator.Current).isBlind())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600015D RID: 349 RVA: 0x00008324 File Offset: 0x00006524
	public bool isDeaf()
	{
		using (List<BaseCharacterComponent>.Enumerator enumerator = this.getComponentList().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((Condition)enumerator.Current).isDeaf())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600015E RID: 350 RVA: 0x00008384 File Offset: 0x00006584
	public bool isConfused()
	{
		using (List<BaseCharacterComponent>.Enumerator enumerator = this.getComponentList().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((Condition)enumerator.Current).isConfused())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600015F RID: 351 RVA: 0x000083E4 File Offset: 0x000065E4
	public bool isImmobilized()
	{
		using (List<BaseCharacterComponent>.Enumerator enumerator = this.getComponentList().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((Condition)enumerator.Current).isImmobilized())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000160 RID: 352 RVA: 0x00008444 File Offset: 0x00006644
	public bool isDiseased()
	{
		using (List<BaseCharacterComponent>.Enumerator enumerator = this.getComponentList().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((Condition)enumerator.Current).isDiseased())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000161 RID: 353 RVA: 0x000084A4 File Offset: 0x000066A4
	public bool isStunned()
	{
		using (List<BaseCharacterComponent>.Enumerator enumerator = this.getComponentList().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((Condition)enumerator.Current).isStunned())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000162 RID: 354 RVA: 0x00008504 File Offset: 0x00006704
	public bool isPanicked()
	{
		using (List<BaseCharacterComponent>.Enumerator enumerator = this.getComponentList().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((Condition)enumerator.Current).isPanicked())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000163 RID: 355 RVA: 0x00008564 File Offset: 0x00006764
	public bool isParalyzed()
	{
		using (List<BaseCharacterComponent>.Enumerator enumerator = this.getComponentList().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((Condition)enumerator.Current).isParalyzed())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000164 RID: 356 RVA: 0x000085C4 File Offset: 0x000067C4
	public bool isInsane()
	{
		using (List<BaseCharacterComponent>.Enumerator enumerator = this.getComponentList().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((Condition)enumerator.Current).isInsane())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000165 RID: 357 RVA: 0x00008624 File Offset: 0x00006824
	public bool isFlatFooted()
	{
		using (List<BaseCharacterComponent>.Enumerator enumerator = this.getComponentList().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((Condition)enumerator.Current).isFlatFooted())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000166 RID: 358 RVA: 0x00008684 File Offset: 0x00006884
	public bool isDefenceless()
	{
		using (List<BaseCharacterComponent>.Enumerator enumerator = this.getComponentList().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((Condition)enumerator.Current).isDefenceless())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000167 RID: 359 RVA: 0x000086E4 File Offset: 0x000068E4
	public bool isAsleep()
	{
		using (List<BaseCharacterComponent>.Enumerator enumerator = this.getComponentList().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((Condition)enumerator.Current).isAsleep())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000168 RID: 360 RVA: 0x00008744 File Offset: 0x00006944
	public bool isOccupied()
	{
		using (List<BaseCharacterComponent>.Enumerator enumerator = this.getComponentList().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((Condition)enumerator.Current).isOccupied())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000169 RID: 361 RVA: 0x000087A4 File Offset: 0x000069A4
	public bool isIntoxicated()
	{
		using (List<BaseCharacterComponent>.Enumerator enumerator = this.getComponentList().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((Condition)enumerator.Current).isIntoxicated())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600016A RID: 362 RVA: 0x00008804 File Offset: 0x00006A04
	public bool isCharmed()
	{
		using (List<BaseCharacterComponent>.Enumerator enumerator = this.getComponentList().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((Condition)enumerator.Current).isCharmed())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600016B RID: 363 RVA: 0x00008864 File Offset: 0x00006A64
	public bool isAfraid()
	{
		using (List<BaseCharacterComponent>.Enumerator enumerator = this.getComponentList().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((Condition)enumerator.Current).isAfraid())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600016C RID: 364 RVA: 0x000088C4 File Offset: 0x00006AC4
	public bool isSilenced()
	{
		using (List<BaseCharacterComponent>.Enumerator enumerator = this.getComponentList().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((Condition)enumerator.Current).isSilenced())
				{
					return true;
				}
			}
		}
		return false;
	}
}
