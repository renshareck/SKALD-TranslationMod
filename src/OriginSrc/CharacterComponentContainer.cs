using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

// Token: 0x02000012 RID: 18
[Serializable]
public abstract class CharacterComponentContainer : ISerializable
{
	// Token: 0x060000E0 RID: 224 RVA: 0x000061E4 File Offset: 0x000043E4
	public CharacterComponentContainer(Character owner)
	{
		this.setOwner(owner);
	}

	// Token: 0x060000E1 RID: 225 RVA: 0x000061FE File Offset: 0x000043FE
	public CharacterComponentContainer(SerializationInfo info, StreamingContext context)
	{
		this.componentIdList = (List<string>)info.GetValue("saveData", typeof(List<string>));
	}

	// Token: 0x060000E2 RID: 226 RVA: 0x00006231 File Offset: 0x00004431
	public void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("saveData", this.componentIdList, typeof(List<string>));
	}

	// Token: 0x060000E3 RID: 227 RVA: 0x0000624E File Offset: 0x0000444E
	public void setOwner(Character owner)
	{
		this.owner = owner;
	}

	// Token: 0x060000E4 RID: 228 RVA: 0x00006258 File Offset: 0x00004458
	protected List<string> getComponentIdList()
	{
		if (this.fullComponentIdList == null)
		{
			this.fullComponentIdList = new List<string>();
		}
		else
		{
			this.fullComponentIdList.Clear();
		}
		foreach (string item in this.componentIdList)
		{
			this.fullComponentIdList.Add(item);
		}
		foreach (string item2 in this.getItemComponentIdList())
		{
			if (this.allowDuplicatedItemComponents())
			{
				this.fullComponentIdList.Add(item2);
			}
			else if (!this.fullComponentIdList.Contains(item2))
			{
				this.fullComponentIdList.Add(item2);
			}
		}
		return this.fullComponentIdList;
	}

	// Token: 0x060000E5 RID: 229 RVA: 0x00006344 File Offset: 0x00004544
	protected virtual bool allowDuplicatedItemComponents()
	{
		return false;
	}

	// Token: 0x060000E6 RID: 230 RVA: 0x00006348 File Offset: 0x00004548
	public virtual SkaldDataList getDataList()
	{
		SkaldDataList skaldDataList = new SkaldDataList("Active Conditions", "These condtions that currently affects this character.");
		List<BaseCharacterComponent> componentList = this.getComponentList();
		if (componentList.Count == 0)
		{
			skaldDataList.addEntry("CONDITION", "-Empty-", "No condtions are currently active.");
		}
		else
		{
			foreach (BaseCharacterComponent baseCharacterComponent in componentList)
			{
				skaldDataList.addEntry(baseCharacterComponent.getId(), baseCharacterComponent.getName(), baseCharacterComponent.getFullDescription());
			}
		}
		return skaldDataList;
	}

	// Token: 0x060000E7 RID: 231
	protected abstract List<string> getItemComponentIdList();

	// Token: 0x060000E8 RID: 232 RVA: 0x000063E0 File Offset: 0x000045E0
	public virtual MapCutoutTemplate getTargetZoneCutout(Character user, int x, int y)
	{
		return null;
	}

	// Token: 0x060000E9 RID: 233 RVA: 0x000063E4 File Offset: 0x000045E4
	public virtual void deleteComponent(string id)
	{
		this.componentIdList.Remove(id);
		if (MainControl.debugFunctions)
		{
			MainControl.log(string.Concat(new string[]
			{
				"Removed Component ",
				C64Color.WHITE_TAG,
				id,
				"</color> for user ",
				this.owner.getId()
			}));
		}
	}

	// Token: 0x060000EA RID: 234 RVA: 0x0000643F File Offset: 0x0000463F
	protected void addComponent(BaseCharacterComponent component)
	{
		if (component == null)
		{
			return;
		}
		this.componentIdList.Add(component.getId());
	}

	// Token: 0x060000EB RID: 235 RVA: 0x00006456 File Offset: 0x00004656
	public bool hasComponentNatively(string id)
	{
		return this.componentIdList.Contains(id);
	}

	// Token: 0x060000EC RID: 236 RVA: 0x00006469 File Offset: 0x00004669
	public bool hasComponent(string id)
	{
		return this.getComponentIdList().Contains(id);
	}

	// Token: 0x060000ED RID: 237 RVA: 0x0000647C File Offset: 0x0000467C
	public virtual SkaldActionResult useCurrentComponent(Character user)
	{
		return new SkaldActionResult(false, false, "Was not performed!", true);
	}

	// Token: 0x060000EE RID: 238 RVA: 0x0000648C File Offset: 0x0000468C
	public List<UIButtonControlBase.ButtonData> getNonCombatActivatedComponentButtonDataList()
	{
		List<UIButtonControlBase.ButtonData> list = new List<UIButtonControlBase.ButtonData>();
		foreach (BaseCharacterComponent baseCharacterComponent in this.getNonCombatActivatedComponentList())
		{
			list.Add(baseCharacterComponent.getButtonData(this.owner));
		}
		return list;
	}

	// Token: 0x060000EF RID: 239 RVA: 0x000064F4 File Offset: 0x000046F4
	public List<UIButtonControlBase.ButtonData> getCombatActivatedComponentButtonDataList()
	{
		List<UIButtonControlBase.ButtonData> list = new List<UIButtonControlBase.ButtonData>();
		foreach (BaseCharacterComponent baseCharacterComponent in this.getCombatActivatedComponentList())
		{
			list.Add(baseCharacterComponent.getButtonData(this.owner));
		}
		return list;
	}

	// Token: 0x060000F0 RID: 240
	public abstract List<BaseCharacterComponent> getCombatActivatedComponentList();

	// Token: 0x060000F1 RID: 241
	public abstract List<BaseCharacterComponent> getNonCombatActivatedComponentList();

	// Token: 0x060000F2 RID: 242
	public abstract List<BaseCharacterComponent> getComponentList();

	// Token: 0x060000F3 RID: 243
	public abstract BaseCharacterComponent forcedGetComponentFromIdWithoutCheckingIfIHaveIt(string id);

	// Token: 0x060000F4 RID: 244 RVA: 0x0000655C File Offset: 0x0000475C
	public virtual bool tryToSetCombatAutoUseComponent(Character user)
	{
		this.currentComponent = null;
		List<AbilityActive> list = new List<AbilityActive>();
		foreach (BaseCharacterComponent baseCharacterComponent in this.getCombatActivatedComponentList())
		{
			if (baseCharacterComponent.isCombatActivated() && baseCharacterComponent is AbilityActive)
			{
				AbilityActive abilityActive = baseCharacterComponent as AbilityActive;
				if (abilityActive.canUserUseAbility(user).wasSuccess())
				{
					list.Add(abilityActive);
				}
			}
		}
		foreach (AbilityActive ability in list)
		{
			this.setAreaEffectSelectionIfAdvantageous(user, ability);
			if (this.hasAreaEffectSelectionSet())
			{
				this.currentComponent = ability;
				return true;
			}
		}
		return false;
	}

	// Token: 0x060000F5 RID: 245 RVA: 0x0000663C File Offset: 0x0000483C
	public bool hasAreaEffectSelectionSet()
	{
		return this.areaEffectSelection != null;
	}

	// Token: 0x060000F6 RID: 246 RVA: 0x00006648 File Offset: 0x00004848
	public string printListSingleLine()
	{
		string text = "";
		foreach (BaseCharacterComponent baseCharacterComponent in this.getComponentList())
		{
			text = text + baseCharacterComponent.getName() + ", ";
		}
		text = TextTools.removeTrailingComma(text);
		return text;
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x000066B4 File Offset: 0x000048B4
	public bool isEmpty()
	{
		return this.componentIdList.Count == 0;
	}

	// Token: 0x060000F8 RID: 248 RVA: 0x000066C4 File Offset: 0x000048C4
	public BaseCharacterComponent getCurrentComponent()
	{
		return this.currentComponent;
	}

	// Token: 0x060000F9 RID: 249 RVA: 0x000066CC File Offset: 0x000048CC
	public BaseCharacterComponent setNonCombatActivatedComponentByIndex(int i)
	{
		this.currentComponent = null;
		List<BaseCharacterComponent> nonCombatActivatedComponentList = this.getNonCombatActivatedComponentList();
		if (nonCombatActivatedComponentList == null || nonCombatActivatedComponentList.Count == 0 || i < 0 || i >= nonCombatActivatedComponentList.Count)
		{
			return null;
		}
		BaseCharacterComponent baseCharacterComponent = nonCombatActivatedComponentList[i];
		if (baseCharacterComponent == null)
		{
			return null;
		}
		this.currentComponent = baseCharacterComponent;
		return baseCharacterComponent;
	}

	// Token: 0x060000FA RID: 250 RVA: 0x00006718 File Offset: 0x00004918
	public BaseCharacterComponent setCombatActivatedComponentByIndex(int i)
	{
		this.currentComponent = null;
		List<BaseCharacterComponent> combatActivatedComponentList = this.getCombatActivatedComponentList();
		if (combatActivatedComponentList == null || combatActivatedComponentList.Count == 0 || i < 0 || i >= combatActivatedComponentList.Count)
		{
			return null;
		}
		BaseCharacterComponent baseCharacterComponent = combatActivatedComponentList[i];
		if (baseCharacterComponent == null)
		{
			return null;
		}
		this.currentComponent = baseCharacterComponent;
		return baseCharacterComponent;
	}

	// Token: 0x060000FB RID: 251 RVA: 0x00006762 File Offset: 0x00004962
	public string getTargetingString()
	{
		return this.getCurrentComponent().getTargetingString();
	}

	// Token: 0x060000FC RID: 252 RVA: 0x0000676F File Offset: 0x0000496F
	public virtual bool isCurrentComponentOverlandActivated()
	{
		return this.currentComponent != null && this.getCurrentComponent().isNonCombatActivated();
	}

	// Token: 0x060000FD RID: 253 RVA: 0x00006786 File Offset: 0x00004986
	public virtual bool isCurrentComponentCombatActivated()
	{
		return this.currentComponent != null && this.getCurrentComponent().isCombatActivated();
	}

	// Token: 0x060000FE RID: 254 RVA: 0x000067A0 File Offset: 0x000049A0
	public int getPowerLevel()
	{
		int num = 0;
		foreach (BaseCharacterComponent baseCharacterComponent in this.getComponentList())
		{
			num += baseCharacterComponent.getPowerLevel();
		}
		return num;
	}

	// Token: 0x060000FF RID: 255 RVA: 0x000067F8 File Offset: 0x000049F8
	public string getCurrentObjectFullDescriptionAndHeader()
	{
		if (this.getCurrentComponent() == null)
		{
			return "- List is empty -";
		}
		return this.getCurrentComponent().getFullDescriptionAndHeader();
	}

	// Token: 0x06000100 RID: 256 RVA: 0x00006813 File Offset: 0x00004A13
	public void clearAreaEffectSelection()
	{
		this.areaEffectSelection = null;
	}

	// Token: 0x06000101 RID: 257 RVA: 0x0000681C File Offset: 0x00004A1C
	public MapTile getAreaEffectBaseTile()
	{
		if (this.areaEffectSelection == null)
		{
			return null;
		}
		return this.areaEffectSelection.getBaseTile();
	}

	// Token: 0x06000102 RID: 258 RVA: 0x00006833 File Offset: 0x00004A33
	public bool isAreaEffectSelectionLegal()
	{
		return this.areaEffectSelection == null || this.areaEffectSelection.isAreaEffectSelectionLegal();
	}

	// Token: 0x06000103 RID: 259 RVA: 0x0000684C File Offset: 0x00004A4C
	private void setAreaEffectSelectionIfAdvantageous(Character user, AbilityActive ability)
	{
		this.clearAreaEffectSelection();
		Character targetOpponent = user.getTargetOpponent();
		MapCutoutTemplate targetZoneCutout;
		if (targetOpponent != null)
		{
			targetZoneCutout = ability.getTargetZoneCutout(user, targetOpponent.getTileX(), targetOpponent.getTileY());
		}
		else
		{
			targetZoneCutout = ability.getTargetZoneCutout(user, user.getTileX(), user.getTileY());
		}
		if (targetZoneCutout == null)
		{
			return;
		}
		CharacterComponentContainer.AreaEffectSelection areaEffectSelection = new CharacterComponentContainer.AreaEffectSelection(targetZoneCutout);
		List<Character> allCharactersInSelection = areaEffectSelection.getAllCharactersInSelection();
		foreach (Character target in allCharactersInSelection)
		{
			if (ability.isTargetCauseForAbortingUse(user, target))
			{
				return;
			}
		}
		foreach (Character target2 in allCharactersInSelection)
		{
			if (ability.isTargetAGoodCaseForUse(user, target2))
			{
				this.areaEffectSelection = areaEffectSelection;
				break;
			}
		}
	}

	// Token: 0x06000104 RID: 260 RVA: 0x0000693C File Offset: 0x00004B3C
	public bool areaEffectSelectionContains(MapTile tile)
	{
		return this.hasAreaEffectSelectionSet() && this.areaEffectSelection.containsTargetTile(tile);
	}

	// Token: 0x06000105 RID: 261 RVA: 0x00006954 File Offset: 0x00004B54
	public void setTargetSelection(Character character)
	{
		Character character2 = character.getTargetOpponent();
		if (character2 == null)
		{
			character2 = character;
		}
		this.setCombatTargetSelection(character, character2.getTileX(), character2.getTileY());
	}

	// Token: 0x06000106 RID: 262 RVA: 0x00006980 File Offset: 0x00004B80
	public void setCombatTargetSelection(Character character, int x, int y)
	{
		if (this.getCurrentComponent() == null)
		{
			return;
		}
		MapCutoutTemplate targetZoneCutout = this.getCurrentComponent().getTargetZoneCutout(character, x, y);
		if (targetZoneCutout != null)
		{
			this.areaEffectSelection = new CharacterComponentContainer.AreaEffectSelection(targetZoneCutout);
		}
	}

	// Token: 0x06000107 RID: 263 RVA: 0x000069B4 File Offset: 0x00004BB4
	public void setOutOfCombatTargetSelection(List<Character> targets)
	{
		if (this.getCurrentComponent() == null)
		{
			return;
		}
		this.areaEffectSelection = new CharacterComponentContainer.OutOfCombatEffectSelection(targets);
	}

	// Token: 0x06000108 RID: 264 RVA: 0x000069CB File Offset: 0x00004BCB
	public SkaldActionResult fireAbility(Character character)
	{
		if (!(this.getCurrentComponent() is AbilityActive))
		{
			MainControl.logError("trying to cast Ability as AbilityActive");
			return new SkaldActionResult(false, false, "Current Ability is not Activatable", true);
		}
		return this.fireAbility(character, this.getCurrentComponent() as AbilityActive);
	}

	// Token: 0x06000109 RID: 265 RVA: 0x00006A04 File Offset: 0x00004C04
	public SkaldActionResult fireAbility(Character character, AbilityActive ability)
	{
		if (character == null || ability == null)
		{
			return new SkaldActionResult(false, false, "Current component could not be fired!", true);
		}
		if (!this.hasComponent(ability.getId()))
		{
			return new SkaldActionResult(false, false, "Does not have component: " + ability.getId(), true);
		}
		if (this.currentComponent != ability)
		{
			this.currentComponent = ability;
		}
		if (!this.hasAreaEffectSelectionSet())
		{
			this.setTargetSelection(character);
		}
		SkaldActionResult result = ability.fireTrigger(character, this.areaEffectSelection);
		this.clearAreaEffectSelection();
		return result;
	}

	// Token: 0x0400000B RID: 11
	private List<string> componentIdList = new List<string>();

	// Token: 0x0400000C RID: 12
	private List<string> fullComponentIdList;

	// Token: 0x0400000D RID: 13
	private BaseCharacterComponent currentComponent;

	// Token: 0x0400000E RID: 14
	protected CharacterComponentContainer.EffectSelection areaEffectSelection;

	// Token: 0x0400000F RID: 15
	protected Character owner;

	// Token: 0x020001B9 RID: 441
	public abstract class EffectSelection
	{
		// Token: 0x0600161E RID: 5662 RVA: 0x00062EA0 File Offset: 0x000610A0
		public virtual bool containsTargetTile(MapTile tile)
		{
			return false;
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x00062EA3 File Offset: 0x000610A3
		public virtual List<MapTile> getMapTiles()
		{
			return new List<MapTile>();
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x00062EAA File Offset: 0x000610AA
		public virtual MapTile getBaseTile()
		{
			return null;
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x00062EAD File Offset: 0x000610AD
		public virtual bool isAreaEffectSelectionLegal()
		{
			return true;
		}

		// Token: 0x06001622 RID: 5666
		public abstract List<Character> getAllCharactersInSelection();
	}

	// Token: 0x020001BA RID: 442
	public class OutOfCombatEffectSelection : CharacterComponentContainer.EffectSelection
	{
		// Token: 0x06001624 RID: 5668 RVA: 0x00062EB8 File Offset: 0x000610B8
		public OutOfCombatEffectSelection(List<Character> targets)
		{
			this.targets = targets;
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x00062EC7 File Offset: 0x000610C7
		public override List<Character> getAllCharactersInSelection()
		{
			return this.targets;
		}

		// Token: 0x0400068F RID: 1679
		private List<Character> targets;
	}

	// Token: 0x020001BB RID: 443
	public class AreaEffectSelection : CharacterComponentContainer.EffectSelection
	{
		// Token: 0x06001626 RID: 5670 RVA: 0x00062ECF File Offset: 0x000610CF
		public AreaEffectSelection(MapCutoutTemplate cutout)
		{
			this.targetTiles = cutout.getCutout();
			this.baseTile = cutout.getBaseTile();
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x00062EFA File Offset: 0x000610FA
		public override bool containsTargetTile(MapTile tile)
		{
			return this.targetTiles.Contains(tile);
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x00062F08 File Offset: 0x00061108
		public override MapTile getBaseTile()
		{
			return this.baseTile;
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x00062F10 File Offset: 0x00061110
		public override List<Character> getAllCharactersInSelection()
		{
			List<Character> list = new List<Character>();
			foreach (MapTile mapTile in this.targetTiles)
			{
				Character character = mapTile.getCharacter();
				if (character != null && !list.Contains(character))
				{
					list.Add(character);
				}
			}
			return list;
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x00062F7C File Offset: 0x0006117C
		public override List<MapTile> getMapTiles()
		{
			return this.targetTiles;
		}

		// Token: 0x04000690 RID: 1680
		private List<MapTile> targetTiles = new List<MapTile>();

		// Token: 0x04000691 RID: 1681
		private MapTile baseTile;
	}
}
