using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

// Token: 0x020000F2 RID: 242
[Serializable]
public class Party : SkaldPhysicalObjectList
{
	// Token: 0x06000F04 RID: 3844 RVA: 0x00046E03 File Offset: 0x00045003
	public Party()
	{
		base.deactivateSorting();
	}

	// Token: 0x06000F05 RID: 3845 RVA: 0x00046E1C File Offset: 0x0004501C
	public void setFacing(int _facing)
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((Character)skaldBaseObject).setFacing(_facing);
		}
	}

	// Token: 0x06000F06 RID: 3846 RVA: 0x00046E74 File Offset: 0x00045074
	public void clearInteractParty()
	{
		this.interactParty = null;
	}

	// Token: 0x06000F07 RID: 3847 RVA: 0x00046E80 File Offset: 0x00045080
	public void addPercentageXPToNextLevelAll(int percentage)
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((Character)skaldBaseObject).addPercentageXPToNextLevel(percentage);
		}
	}

	// Token: 0x06000F08 RID: 3848 RVA: 0x00046ED8 File Offset: 0x000450D8
	public bool isInteractable()
	{
		return !base.isEmpty() && this.getCurrentCharacter().isInteractable();
	}

	// Token: 0x06000F09 RID: 3849 RVA: 0x00046EEF File Offset: 0x000450EF
	public NavigationCourse GetNavigationCourse()
	{
		if (base.isEmpty())
		{
			return null;
		}
		return this.getCurrentCharacter().GetNavigationCourse();
	}

	// Token: 0x06000F0A RID: 3850 RVA: 0x00046F06 File Offset: 0x00045106
	public bool canPlayerPartyFitMoreMembers()
	{
		return base.getCount() < Party.maxSizeForPlayerParty;
	}

	// Token: 0x06000F0B RID: 3851 RVA: 0x00046F18 File Offset: 0x00045118
	public void levelScale()
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((Character)skaldBaseObject).levelScale();
		}
	}

	// Token: 0x06000F0C RID: 3852 RVA: 0x00046F70 File Offset: 0x00045170
	public void fireApproachTrigger()
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((Character)skaldBaseObject).fireApproachTrigger();
		}
	}

	// Token: 0x06000F0D RID: 3853 RVA: 0x00046FC8 File Offset: 0x000451C8
	public void turnToPoint(int x)
	{
		int facing;
		if (this.getTileX() > x)
		{
			facing = 3;
		}
		else
		{
			facing = 1;
		}
		this.setFacing(facing);
	}

	// Token: 0x06000F0E RID: 3854 RVA: 0x00046FF0 File Offset: 0x000451F0
	public void facePoint(int x, int y)
	{
		int facing;
		if (this.getTileX() - Mathf.Abs(x) < this.getTileY() - Mathf.Abs(y))
		{
			if (x <= 0)
			{
				facing = 3;
			}
			else
			{
				facing = 1;
			}
		}
		else if (y <= 0)
		{
			facing = 2;
		}
		else
		{
			facing = 0;
		}
		this.setFacing(facing);
	}

	// Token: 0x06000F0F RID: 3855 RVA: 0x00047038 File Offset: 0x00045238
	public Point getIdealCharacterPlacement(int index)
	{
		if (base.getCount() == 1)
		{
			return new Point(this.getTileX(), this.getTileY());
		}
		int num = 3;
		int num2 = 0;
		int num3 = 0;
		if (this.getFacing() == 0)
		{
			num2 = -1 + index % num;
			num3 = -(index / num);
		}
		else if (this.getFacing() == 1)
		{
			num2 = -(index / num);
			num3 = 1 - index % num;
		}
		if (this.getFacing() == 2)
		{
			num2 = 1 - index % num;
			num3 = index / num;
		}
		else if (this.getFacing() == 3)
		{
			num2 = index / num;
			num3 = -1 + index % num;
		}
		return new Point(this.getTileX() + num2, this.getTileY() + num3);
	}

	// Token: 0x06000F10 RID: 3856 RVA: 0x000470CC File Offset: 0x000452CC
	public void clearNavigationCourse()
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((Character)skaldBaseObject).clearNavigationCourse();
		}
	}

	// Token: 0x06000F11 RID: 3857 RVA: 0x00047124 File Offset: 0x00045324
	public bool navigationCourseHasNodes()
	{
		return this.getCurrentCharacter().navigationCourseHasNodes();
	}

	// Token: 0x06000F12 RID: 3858 RVA: 0x00047134 File Offset: 0x00045334
	public void setNavigationCourse(NavigationCourse c)
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((Character)skaldBaseObject).setNavigationCourse(c);
		}
	}

	// Token: 0x06000F13 RID: 3859 RVA: 0x0004718C File Offset: 0x0004538C
	public Point popNavigationNode()
	{
		if (base.isEmpty())
		{
			return new Point(0, 0);
		}
		return this.getCurrentCharacter().popNavigationNode();
	}

	// Token: 0x06000F14 RID: 3860 RVA: 0x000471AC File Offset: 0x000453AC
	public override void clearTilePosition()
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((SkaldPhysicalObject)skaldBaseObject).clearTilePosition();
		}
	}

	// Token: 0x06000F15 RID: 3861 RVA: 0x00047204 File Offset: 0x00045404
	public override int getBounceOffset()
	{
		if (this.hasVehicle())
		{
			return 0;
		}
		return base.getBounceOffset();
	}

	// Token: 0x06000F16 RID: 3862 RVA: 0x00047218 File Offset: 0x00045418
	public override TextureTools.TextureData getModelTexture()
	{
		if (this.hasVehicle())
		{
			int num = 16;
			TextureTools.TextureData modelTexture = this.vehicle.getModelTexture();
			TextureTools.TextureData textureData = new TextureTools.TextureData(modelTexture.width + num * 2, modelTexture.height + num * 2);
			textureData.SetPixels(num, 0, modelTexture.width, modelTexture.height, modelTexture.colors);
			if (!this.getMainCharacter().spriteFacesRight())
			{
				textureData.flipHorizontally();
			}
			return textureData;
		}
		if (base.isEmpty())
		{
			return null;
		}
		return this.getCurrentCharacter().getModelTexture();
	}

	// Token: 0x06000F17 RID: 3863 RVA: 0x0004729A File Offset: 0x0004549A
	public bool canTraverseWater()
	{
		return this.hasVehicle();
	}

	// Token: 0x06000F18 RID: 3864 RVA: 0x000472A7 File Offset: 0x000454A7
	public string getVehicleNestedMap()
	{
		if (!this.hasVehicle())
		{
			return "";
		}
		return this.vehicle.getNestedMapId();
	}

	// Token: 0x06000F19 RID: 3865 RVA: 0x000472C2 File Offset: 0x000454C2
	public void mountInteractParty(Party p)
	{
		this.interactParty = p;
	}

	// Token: 0x06000F1A RID: 3866 RVA: 0x000472CB File Offset: 0x000454CB
	public bool isInteractPartyMounted()
	{
		return this.interactParty != null;
	}

	// Token: 0x06000F1B RID: 3867 RVA: 0x000472D8 File Offset: 0x000454D8
	public string setBreath()
	{
		this.breath = this.getMainCharacter().getCurrentAttributeValue(AttributesControl.CoreAttributes.ATT_Athletics) + 8;
		return this.breath.ToString() + " rounds of oxygen remaining.";
	}

	// Token: 0x06000F1C RID: 3868 RVA: 0x00047304 File Offset: 0x00045504
	public string updateBreath()
	{
		this.breath--;
		if (this.breath <= 0)
		{
			this.breath = 0;
			this.killAll();
		}
		return this.breath.ToString() + " rounds of oxygen remaining.";
	}

	// Token: 0x06000F1D RID: 3869 RVA: 0x0004733F File Offset: 0x0004553F
	public Party getInteractParty()
	{
		return this.interactParty;
	}

	// Token: 0x06000F1E RID: 3870 RVA: 0x00047347 File Offset: 0x00045547
	public int getFacing()
	{
		if (base.isEmpty())
		{
			return 0;
		}
		return this.getCurrentCharacter().getFacing();
	}

	// Token: 0x06000F1F RID: 3871 RVA: 0x00047360 File Offset: 0x00045560
	public string conditionallyAddAbilityToParty(string conditionId)
	{
		string text = "";
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			Character character = (Character)skaldBaseObject;
			text = text + character.conditionallyAddAbility(conditionId) + "\n";
		}
		return text;
	}

	// Token: 0x06000F20 RID: 3872 RVA: 0x000473CC File Offset: 0x000455CC
	public override bool isPersistent()
	{
		using (List<SkaldBaseObject>.Enumerator enumerator = this.objectList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((Character)enumerator.Current).isPersistent())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000F21 RID: 3873 RVA: 0x0004742C File Offset: 0x0004562C
	public string printRelationshipRanks()
	{
		string text = "";
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			Character character = (Character)skaldBaseObject;
			text = text + character.printRelationshipRank() + "\n";
		}
		return text;
	}

	// Token: 0x06000F22 RID: 3874 RVA: 0x00047498 File Offset: 0x00045698
	public bool isSpotted()
	{
		using (List<SkaldBaseObject>.Enumerator enumerator = this.objectList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((Character)enumerator.Current).isSpotted())
				{
					this.setSpotted();
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000F23 RID: 3875 RVA: 0x000474FC File Offset: 0x000456FC
	public void setSpotted()
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((Character)skaldBaseObject).setSpotted();
		}
	}

	// Token: 0x06000F24 RID: 3876 RVA: 0x00047554 File Offset: 0x00045754
	public override bool isUnique()
	{
		return !base.isEmpty() && this.getCurrentCharacter().isUnique();
	}

	// Token: 0x06000F25 RID: 3877 RVA: 0x0004756B File Offset: 0x0004576B
	public int getLightDistance()
	{
		if (base.isEmpty())
		{
			return 0;
		}
		return this.getCurrentCharacter().getLightDistance();
	}

	// Token: 0x06000F26 RID: 3878 RVA: 0x00047582 File Offset: 0x00045782
	public float getLightStrength()
	{
		if (base.isEmpty())
		{
			return 0f;
		}
		return this.getCurrentCharacter().getLightStrength();
	}

	// Token: 0x06000F27 RID: 3879 RVA: 0x0004759D File Offset: 0x0004579D
	public void toggleLightOnOff()
	{
		this.getInventory().toggleLight(this.getCurrentCharacter());
	}

	// Token: 0x06000F28 RID: 3880 RVA: 0x000475B0 File Offset: 0x000457B0
	public bool isAlert()
	{
		return !base.isEmpty() && this.getCurrentCharacter().isAlert();
	}

	// Token: 0x06000F29 RID: 3881 RVA: 0x000475CC File Offset: 0x000457CC
	public bool shouldTurnToFacePC()
	{
		return !base.isEmpty() && this.getCurrentCharacter().shouldTurnToFacePC();
	}

	// Token: 0x06000F2A RID: 3882 RVA: 0x000475E4 File Offset: 0x000457E4
	public void setAlert()
	{
		if (base.isEmpty())
		{
			return;
		}
		if (this.isHostile() && !this.getCurrentCharacter().isAlert())
		{
			this.getCurrentCharacter().getVisualEffects().setSweating();
		}
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((Character)skaldBaseObject).setAlert();
		}
	}

	// Token: 0x06000F2B RID: 3883 RVA: 0x00047668 File Offset: 0x00045868
	public void setGradualAlert()
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((Character)skaldBaseObject).setGradualAlert();
		}
	}

	// Token: 0x06000F2C RID: 3884 RVA: 0x000476C0 File Offset: 0x000458C0
	public int getHighestAttributeStatic(AttributesControl.CoreAttributes attribute)
	{
		int num = 0;
		foreach (SkaldBaseObject skaldBaseObject in base.getObjectList())
		{
			int currentAttributeValueStatic = ((Character)skaldBaseObject).getCurrentAttributeValueStatic(attribute);
			if (currentAttributeValueStatic > num)
			{
				num = currentAttributeValueStatic;
			}
		}
		return num;
	}

	// Token: 0x06000F2D RID: 3885 RVA: 0x00047720 File Offset: 0x00045920
	public bool isPartyFullyRested()
	{
		using (List<SkaldBaseObject>.Enumerator enumerator = this.objectList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!((Character)enumerator.Current).isFullyRested())
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x06000F2E RID: 3886 RVA: 0x00047780 File Offset: 0x00045980
	public void resolveVictoryTriggers()
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((Character)skaldBaseObject).victoryTrigger();
		}
	}

	// Token: 0x06000F2F RID: 3887 RVA: 0x000477D8 File Offset: 0x000459D8
	public bool isFactionMember(string id)
	{
		using (List<SkaldBaseObject>.Enumerator enumerator = this.objectList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((Character)enumerator.Current).isFactionMember(id))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000F30 RID: 3888 RVA: 0x00047838 File Offset: 0x00045A38
	public Character.MoveMode getMoveMode()
	{
		if (this.getCurrentCharacter() == null)
		{
			return Character.MoveMode.None;
		}
		return this.getCurrentCharacter().getMoveMode();
	}

	// Token: 0x06000F31 RID: 3889 RVA: 0x00047850 File Offset: 0x00045A50
	public void updateMoveMode()
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((Character)skaldBaseObject).updateMoveMode();
		}
	}

	// Token: 0x06000F32 RID: 3890 RVA: 0x000478A8 File Offset: 0x00045AA8
	public bool isPC()
	{
		using (List<SkaldBaseObject>.Enumerator enumerator = this.objectList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((Character)enumerator.Current).isPC())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000F33 RID: 3891 RVA: 0x00047908 File Offset: 0x00045B08
	public void setTargetHeight(int targetHeight)
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((Character)skaldBaseObject).setTargetHeight(targetHeight);
		}
	}

	// Token: 0x06000F34 RID: 3892 RVA: 0x00047960 File Offset: 0x00045B60
	public void setConcealmentOffset(int targetHeight)
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((Character)skaldBaseObject).setConcealmentOffset(targetHeight);
		}
	}

	// Token: 0x06000F35 RID: 3893 RVA: 0x000479B8 File Offset: 0x00045BB8
	public void setWading(bool wading)
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((Character)skaldBaseObject).setWading(wading);
		}
	}

	// Token: 0x06000F36 RID: 3894 RVA: 0x00047A10 File Offset: 0x00045C10
	public void setMoveMode(Character.MoveMode mode)
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((Character)skaldBaseObject).setMoveMode(mode);
		}
	}

	// Token: 0x06000F37 RID: 3895 RVA: 0x00047A68 File Offset: 0x00045C68
	public int getPowerLevel()
	{
		int num = 0;
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			Character character = (Character)skaldBaseObject;
			num += character.getPowerLevel();
		}
		return num;
	}

	// Token: 0x06000F38 RID: 3896 RVA: 0x00047AC8 File Offset: 0x00045CC8
	private int getCarringCapacity()
	{
		int num = 30;
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			Character character = (Character)skaldBaseObject;
			num += character.getCarryingCapacity();
		}
		return num;
	}

	// Token: 0x06000F39 RID: 3897 RVA: 0x00047B28 File Offset: 0x00045D28
	public bool isEncumbered()
	{
		float weight = this.getInventory().getWeight();
		int carringCapacity = this.getCarringCapacity();
		return (!this.isPC() || !GlobalSettings.getDifficultySettings().ignoreEncumberance()) && weight > (float)carringCapacity;
	}

	// Token: 0x06000F3A RID: 3898 RVA: 0x00047B68 File Offset: 0x00045D68
	public string printWeight()
	{
		string text = C64Color.GRAY_LIGHT_TAG + "Weight: </color>";
		if (GlobalSettings.getDifficultySettings().ignoreEncumberance())
		{
			text += "Unlimited";
		}
		else
		{
			string text2 = this.getInventory().getWeight().ToString("0.0") + "/" + this.getCarringCapacity().ToString();
			if (this.isEncumbered())
			{
				text = text + C64Color.RED_LIGHT_TAG + text2 + " lb</color> (Encum.)";
			}
			else
			{
				text = text + text2 + " lb";
			}
		}
		return text;
	}

	// Token: 0x06000F3B RID: 3899 RVA: 0x00047BFC File Offset: 0x00045DFC
	private int getNextXPLevel()
	{
		int num = 0;
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			Character character = (Character)skaldBaseObject;
			num += character.getNextLevel();
		}
		num /= this.objectList.Count;
		return num;
	}

	// Token: 0x06000F3C RID: 3900 RVA: 0x00047C68 File Offset: 0x00045E68
	public Character setMainCharacter()
	{
		if (base.isEmpty())
		{
			return null;
		}
		Character mainCharacter = this.getMainCharacter();
		if (mainCharacter != null)
		{
			mainCharacter.resurrect();
			this.setCurrentPC(mainCharacter);
			return this.currentObject as Character;
		}
		return this.setNextLiveCharacter();
	}

	// Token: 0x06000F3D RID: 3901 RVA: 0x00047CA8 File Offset: 0x00045EA8
	public Character getMainCharacter()
	{
		if (base.isEmpty())
		{
			return null;
		}
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			Character character = (Character)skaldBaseObject;
			if (character.isMainCharacter())
			{
				return character;
			}
		}
		return null;
	}

	// Token: 0x06000F3E RID: 3902 RVA: 0x00047D14 File Offset: 0x00045F14
	public int calculateOpponentXPValue(Party opponentParty, Party allyParty)
	{
		float num = this.comparePowerlevel(opponentParty);
		float num2 = 1f;
		int num3 = 6;
		if (allyParty != null && !allyParty.isEmpty())
		{
			num2 = 1f - num / (allyParty.comparePowerlevel(opponentParty) + num);
			if ((double)num2 < 0.5)
			{
				num2 = 0.5f;
			}
			else if (num2 > 1f)
			{
				num2 = 1f;
			}
		}
		return (int)((float)(this.getNextXPLevel() / num3) * num * num2);
	}

	// Token: 0x06000F3F RID: 3903 RVA: 0x00047D80 File Offset: 0x00045F80
	private float comparePowerlevel(Party compareParty)
	{
		float num = (float)compareParty.getPowerLevel();
		float num2 = (float)this.getPowerLevel();
		return num / num2;
	}

	// Token: 0x06000F40 RID: 3904 RVA: 0x00047DA0 File Offset: 0x00045FA0
	public int getXPFromOpponentParty(Party opponents, Party allies)
	{
		int num = this.calculateOpponentXPValue(opponents, allies);
		this.addCombatXp(num);
		return num;
	}

	// Token: 0x06000F41 RID: 3905 RVA: 0x00047DBE File Offset: 0x00045FBE
	public int getReactionScore()
	{
		if (base.isEmpty())
		{
			return 0;
		}
		return this.getCurrentCharacter().getReactionScore();
	}

	// Token: 0x06000F42 RID: 3906 RVA: 0x00047DD8 File Offset: 0x00045FD8
	public string printPowerLevel()
	{
		string text = "";
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			Character character = (Character)skaldBaseObject;
			text = string.Concat(new string[]
			{
				text,
				character.getName(),
				" \t- ",
				character.getPowerLevel().ToString(),
				"\n"
			});
		}
		text = text + "\nPARTY LEVEL: " + this.getPowerLevel().ToString();
		return text;
	}

	// Token: 0x06000F43 RID: 3907 RVA: 0x00047E84 File Offset: 0x00046084
	public Inventory getInventory()
	{
		if (base.isEmpty())
		{
			return new Inventory();
		}
		return this.getCurrentCharacter().getInventory();
	}

	// Token: 0x06000F44 RID: 3908 RVA: 0x00047E9F File Offset: 0x0004609F
	public Character removeCharacter()
	{
		if (this.objectList.Count == 1)
		{
			return null;
		}
		this.getCurrentCharacter().clearParty();
		return this.removeCurrentObject() as Character;
	}

	// Token: 0x06000F45 RID: 3909 RVA: 0x00047EC8 File Offset: 0x000460C8
	public Character removeCharacterById(string id)
	{
		return this.removeCharacter(base.getObject(id) as Character);
	}

	// Token: 0x06000F46 RID: 3910 RVA: 0x00047EDC File Offset: 0x000460DC
	public Character removeCharacter(Character c)
	{
		if (c == null)
		{
			return c;
		}
		c.clearParty();
		this.setCurrentPC(c);
		c.clearTilePosition();
		return this.removeCurrentObject() as Character;
	}

	// Token: 0x06000F47 RID: 3911 RVA: 0x00047F04 File Offset: 0x00046104
	public void purgeNPCs()
	{
		Character character = this.setMainCharacter();
		List<Character> list = new List<Character>();
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			Character character2 = (Character)skaldBaseObject;
			if (character2 != character)
			{
				list.Add(character2);
			}
		}
		foreach (Character c in list)
		{
			this.removeCharacter(c);
		}
	}

	// Token: 0x06000F48 RID: 3912 RVA: 0x00047FB0 File Offset: 0x000461B0
	public string addFaction(string id)
	{
		string text = "";
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			Character character = (Character)skaldBaseObject;
			text = string.Concat(new string[]
			{
				text,
				character.getName(),
				": ",
				character.addFactionMembership(id),
				"\n"
			});
		}
		return text;
	}

	// Token: 0x06000F49 RID: 3913 RVA: 0x0004803C File Offset: 0x0004623C
	public void killAll()
	{
		foreach (Character character in this.getLiveCharacters())
		{
			character.kill();
		}
	}

	// Token: 0x06000F4A RID: 3914 RVA: 0x0004808C File Offset: 0x0004628C
	public bool isPartyDead()
	{
		if (this.objectList.Count == 0)
		{
			return false;
		}
		using (List<SkaldBaseObject>.Enumerator enumerator = this.objectList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!((Character)enumerator.Current).isDead())
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x06000F4B RID: 3915 RVA: 0x000480FC File Offset: 0x000462FC
	public Point getTileWidthAndHeight()
	{
		if (this.getCurrentCharacter() == null)
		{
			return new Point(1, 1);
		}
		return new Point(this.getCurrentCharacter().getTileWidth(), this.getCurrentCharacter().getTileHeight());
	}

	// Token: 0x06000F4C RID: 3916 RVA: 0x00048129 File Offset: 0x00046329
	public bool isNotAlone()
	{
		return this.objectList.Count > 1;
	}

	// Token: 0x06000F4D RID: 3917 RVA: 0x0004813C File Offset: 0x0004633C
	public List<Character> getLiveCharacters()
	{
		List<Character> list = new List<Character>();
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			Character character = (Character)skaldBaseObject;
			if (!character.isDead())
			{
				list.Add(character);
			}
		}
		return list;
	}

	// Token: 0x06000F4E RID: 3918 RVA: 0x000481A4 File Offset: 0x000463A4
	private int getCharacterIndex()
	{
		for (int i = 0; i < this.objectList.Count; i++)
		{
			if (this.objectList[i] == this.currentObject)
			{
				return i;
			}
		}
		return 0;
	}

	// Token: 0x06000F4F RID: 3919 RVA: 0x000481E0 File Offset: 0x000463E0
	public void moveMember()
	{
		int characterIndex = this.getCharacterIndex();
		int num = characterIndex + 1;
		Character currentCharacter = this.getCurrentCharacter();
		currentCharacter.pass();
		if (num == this.objectList.Count)
		{
			this.objectList.Remove(currentCharacter);
			this.objectList.Insert(0, currentCharacter);
			return;
		}
		Character value = this.objectList[num] as Character;
		this.objectList[num] = currentCharacter;
		this.objectList[characterIndex] = value;
	}

	// Token: 0x06000F50 RID: 3920 RVA: 0x0004825C File Offset: 0x0004645C
	public string getHeShe(string upperCase)
	{
		if (base.isEmpty())
		{
			return "He";
		}
		return this.getCurrentCharacter().getHeShe(upperCase);
	}

	// Token: 0x06000F51 RID: 3921 RVA: 0x00048278 File Offset: 0x00046478
	public string getHimHer(string upperCase)
	{
		if (base.isEmpty())
		{
			return "Him";
		}
		return this.getCurrentCharacter().getHimHer(upperCase);
	}

	// Token: 0x06000F52 RID: 3922 RVA: 0x00048294 File Offset: 0x00046494
	public bool hasTalkTrigger()
	{
		return !base.isEmpty() && this.getCurrentCharacter().hasTalkTrigger();
	}

	// Token: 0x06000F53 RID: 3923 RVA: 0x000482AB File Offset: 0x000464AB
	public string processTalkTrigger()
	{
		if (base.isEmpty())
		{
			return "";
		}
		return this.getCurrentCharacter().processTalkTrigger();
	}

	// Token: 0x06000F54 RID: 3924 RVA: 0x000482C6 File Offset: 0x000464C6
	public string processContactTrigger()
	{
		if (base.isEmpty())
		{
			return "";
		}
		this.setSpotted();
		return this.getCurrentCharacter().processContactTrigger();
	}

	// Token: 0x06000F55 RID: 3925 RVA: 0x000482E7 File Offset: 0x000464E7
	public bool willTrade()
	{
		return !base.isEmpty() && this.getCurrentCharacter().willTrade();
	}

	// Token: 0x06000F56 RID: 3926 RVA: 0x000482FE File Offset: 0x000464FE
	public bool canBeAttacked()
	{
		return !base.isEmpty() && this.getCurrentCharacter().canAttack();
	}

	// Token: 0x06000F57 RID: 3927 RVA: 0x00048315 File Offset: 0x00046515
	public string getNameParty(string upperCase)
	{
		if (base.getCount() > 1)
		{
			if (upperCase == "")
			{
				return "the party";
			}
			return "The party";
		}
		else
		{
			if (upperCase == "")
			{
				return "you";
			}
			return "You";
		}
	}

	// Token: 0x06000F58 RID: 3928 RVA: 0x00048351 File Offset: 0x00046551
	public override string getName()
	{
		if (base.isEmpty())
		{
			return "Party";
		}
		return this.getCurrentCharacter().getName();
	}

	// Token: 0x06000F59 RID: 3929 RVA: 0x0004836C File Offset: 0x0004656C
	public string setHostile(bool state)
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((Character)skaldBaseObject).setHostile(state);
		}
		return "";
	}

	// Token: 0x06000F5A RID: 3930 RVA: 0x000483C8 File Offset: 0x000465C8
	public Character setNextLiveCharacter()
	{
		if (base.isEmpty())
		{
			return null;
		}
		if (!this.getCurrentCharacter().isDead())
		{
			return this.getCurrentCharacter();
		}
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			Character character = (Character)skaldBaseObject;
			if (!character.isDead())
			{
				this.setCurrentPC(character);
				return character;
			}
		}
		return null;
	}

	// Token: 0x06000F5B RID: 3931 RVA: 0x00048450 File Offset: 0x00046650
	public Character getNextLiveCharacterButDontSet(Character currentCharacter)
	{
		if (base.isEmpty())
		{
			return null;
		}
		if (currentCharacter == null)
		{
			return null;
		}
		if (base.getCount() == 1)
		{
			return currentCharacter;
		}
		int num = -1;
		for (int i = 0; i < this.objectList.Count; i++)
		{
			if (this.objectList[i] == currentCharacter)
			{
				num = i;
				break;
			}
		}
		if (num == -1)
		{
			return currentCharacter;
		}
		for (int num2 = num + 1; num2 != num; num2++)
		{
			if (num2 >= this.objectList.Count)
			{
				num2 = 0;
			}
			Character character = this.objectList[num2] as Character;
			if (!character.isDead())
			{
				return character;
			}
		}
		return currentCharacter;
	}

	// Token: 0x06000F5C RID: 3932 RVA: 0x000484E3 File Offset: 0x000466E3
	public void setVehicle(Vehicle v)
	{
		this.vehicle = v;
		if (this.vehicle != null)
		{
			this.vehicle.clearTilePosition();
		}
	}

	// Token: 0x06000F5D RID: 3933 RVA: 0x00048500 File Offset: 0x00046700
	public Vehicle transferVehicle()
	{
		Vehicle vehicle = this.vehicle;
		this.vehicle = null;
		if (vehicle != null)
		{
			vehicle.clearTilePosition();
		}
		return vehicle;
	}

	// Token: 0x06000F5E RID: 3934 RVA: 0x00048525 File Offset: 0x00046725
	public bool hasVehicle()
	{
		return this.vehicle != null;
	}

	// Token: 0x06000F5F RID: 3935 RVA: 0x00048530 File Offset: 0x00046730
	public override string getModelPath()
	{
		if (this.hasVehicle())
		{
			return this.vehicle.getModelPath();
		}
		if (base.isEmpty())
		{
			return "";
		}
		return this.getCurrentCharacter().getModelPath();
	}

	// Token: 0x06000F60 RID: 3936 RVA: 0x0004855F File Offset: 0x0004675F
	public bool isAfraid()
	{
		return !base.isEmpty() && this.getCurrentCharacter().isAfraid();
	}

	// Token: 0x06000F61 RID: 3937 RVA: 0x00048578 File Offset: 0x00046778
	public Party removeHostileCharacters()
	{
		if (base.isEmpty())
		{
			return null;
		}
		List<Character> list = new List<Character>();
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			Character character = (Character)skaldBaseObject;
			if (!character.isMainCharacter() && character.isHostile())
			{
				list.Add(character);
			}
		}
		if (list.Count == 0)
		{
			return null;
		}
		Party party = new Party();
		foreach (Character character2 in list)
		{
			party.add(this.removeCharacterById(character2.getId()));
		}
		return party;
	}

	// Token: 0x06000F62 RID: 3938 RVA: 0x00048650 File Offset: 0x00046850
	public void setCurrentPC(Character newCharacter)
	{
		if (this.objectList.Contains(newCharacter))
		{
			Character currentCharacter = this.getCurrentCharacter();
			this.currentObject = newCharacter;
			newCharacter.setAsCurrentPC(currentCharacter);
		}
	}

	// Token: 0x06000F63 RID: 3939 RVA: 0x00048680 File Offset: 0x00046880
	public void clearBeingObserved()
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((Character)skaldBaseObject).clearBeingObserved();
		}
	}

	// Token: 0x06000F64 RID: 3940 RVA: 0x000486D8 File Offset: 0x000468D8
	public void setBeingObserved()
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((Character)skaldBaseObject).setBeingObserved();
		}
	}

	// Token: 0x06000F65 RID: 3941 RVA: 0x00048730 File Offset: 0x00046930
	public void setCurrentPC(int i)
	{
		if (i < 0 || i >= this.objectList.Count)
		{
			return;
		}
		this.setCurrentPC(this.objectList[i] as Character);
	}

	// Token: 0x06000F66 RID: 3942 RVA: 0x0004875C File Offset: 0x0004695C
	public string add(Character character)
	{
		if (character == null)
		{
			return "";
		}
		base.add(character);
		return character.getName();
	}

	// Token: 0x06000F67 RID: 3943 RVA: 0x00048774 File Offset: 0x00046974
	public string addAndSetAsMainParty(Character character)
	{
		if (character == null)
		{
			return "";
		}
		character.setMainParty(this);
		return this.add(character);
	}

	// Token: 0x06000F68 RID: 3944 RVA: 0x0004878D File Offset: 0x0004698D
	public string addAndSetAsTileParty(Character character)
	{
		if (character == null)
		{
			return "";
		}
		character.setTileParty(this);
		return this.add(character);
	}

	// Token: 0x06000F69 RID: 3945 RVA: 0x000487A8 File Offset: 0x000469A8
	public void mergeInventory()
	{
		Inventory inventory = new Inventory();
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			Character character = (Character)skaldBaseObject;
			inventory.transferInventory(character.getInventory());
			character.setInventory(inventory);
		}
	}

	// Token: 0x06000F6A RID: 3946 RVA: 0x00048814 File Offset: 0x00046A14
	public virtual void addWithoutSettingPosition(Character character)
	{
		this.objectList.Add(character);
	}

	// Token: 0x06000F6B RID: 3947 RVA: 0x00048824 File Offset: 0x00046A24
	public List<Character> getPartyList()
	{
		List<Character> list = new List<Character>();
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			list.Add(skaldBaseObject as Character);
		}
		return list;
	}

	// Token: 0x06000F6C RID: 3948 RVA: 0x00048884 File Offset: 0x00046A84
	public Character getCurrentCharacter()
	{
		base.getCurrentObject();
		return this.currentObject as Character;
	}

	// Token: 0x06000F6D RID: 3949 RVA: 0x00048898 File Offset: 0x00046A98
	public string getCurrentImage()
	{
		return this.getCurrentCharacter().getImagePath();
	}

	// Token: 0x06000F6E RID: 3950 RVA: 0x000488A5 File Offset: 0x00046AA5
	public bool isHostile()
	{
		return !base.isEmpty() && this.getCurrentCharacter().isHostile();
	}

	// Token: 0x06000F6F RID: 3951 RVA: 0x000488BC File Offset: 0x00046ABC
	public string postCombatUpkeep()
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			Character character = (Character)skaldBaseObject;
			if (character.isSummoned())
			{
				this.setCurrentPC(character);
				this.removeCharacter();
				character.getMapTile().clearParty();
				character.setToBeRemoved();
			}
		}
		if (!this.isPartyDead())
		{
			this.resurrectAll();
			if (this.isPC() && GlobalSettings.getDifficultySettings().healFullyAfterCombat())
			{
				this.healFullAll();
			}
			else
			{
				this.restShortAll();
			}
		}
		this.getOriginalOrder();
		foreach (SkaldBaseObject skaldBaseObject2 in this.objectList)
		{
			((Character)skaldBaseObject2).endOfCombatUpkeep();
		}
		return "";
	}

	// Token: 0x06000F70 RID: 3952 RVA: 0x000489B4 File Offset: 0x00046BB4
	private void setOriginalOrder()
	{
		this.originalOrderList.Clear();
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			Character item = (Character)skaldBaseObject;
			this.originalOrderList.Add(item);
		}
	}

	// Token: 0x06000F71 RID: 3953 RVA: 0x00048A1C File Offset: 0x00046C1C
	private void getOriginalOrder()
	{
		this.objectList.Clear();
		foreach (Character item in this.originalOrderList)
		{
			this.objectList.Add(item);
		}
		foreach (Character character in this.originalOrderList)
		{
			if (character.isMainCharacter())
			{
				this.setCurrentPC(character);
				break;
			}
		}
	}

	// Token: 0x06000F72 RID: 3954 RVA: 0x00048ACC File Offset: 0x00046CCC
	public void addConditionToAll(string conditionId)
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((Character)skaldBaseObject).addConditionToCharacter(conditionId);
		}
	}

	// Token: 0x06000F73 RID: 3955 RVA: 0x00048B24 File Offset: 0x00046D24
	public override string getInspectDescription()
	{
		if (base.isEmpty())
		{
			return "No one is here!";
		}
		return this.getCurrentCharacter().getInspectDescription();
	}

	// Token: 0x06000F74 RID: 3956 RVA: 0x00048B40 File Offset: 0x00046D40
	public string getInspectDescriptionFromIndex(int index)
	{
		if (index < 0 || index > this.objectList.Count)
		{
			return "";
		}
		if (this.objectList[index] == null)
		{
			return "";
		}
		return (this.objectList[index] as Character).getInspectDescription();
	}

	// Token: 0x06000F75 RID: 3957 RVA: 0x00048B90 File Offset: 0x00046D90
	public void addCombatXp(int gainedXP)
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((Character)skaldBaseObject).addCombatXp(gainedXP);
		}
	}

	// Token: 0x06000F76 RID: 3958 RVA: 0x00048BEC File Offset: 0x00046DEC
	public void addXp(int gainedXP)
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((Character)skaldBaseObject).addXp(gainedXP);
		}
	}

	// Token: 0x06000F77 RID: 3959 RVA: 0x00048C48 File Offset: 0x00046E48
	public void takeDamageAllSilent(int dmg)
	{
		if (base.isEmpty())
		{
			return;
		}
		this.takeDamageAll(dmg, null);
		this.getCurrentCharacter().clearEffectsBarksAndAnimations();
	}

	// Token: 0x06000F78 RID: 3960 RVA: 0x00048C66 File Offset: 0x00046E66
	public void takeDamageAll(int dmg)
	{
		this.takeDamageAll(dmg, null);
	}

	// Token: 0x06000F79 RID: 3961 RVA: 0x00048C70 File Offset: 0x00046E70
	public void takeDamageAll(int dmg, List<string> type)
	{
		if (base.isEmpty())
		{
			return;
		}
		Character currentCharacter = this.getCurrentCharacter();
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			Character character = (Character)skaldBaseObject;
			character.takeDamage(dmg, type);
			if (character != currentCharacter)
			{
				character.clearEffectsBarksAndAnimations();
			}
		}
	}

	// Token: 0x06000F7A RID: 3962 RVA: 0x00048CE4 File Offset: 0x00046EE4
	public void healFullAll()
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((Character)skaldBaseObject).restoreAllFull();
		}
	}

	// Token: 0x06000F7B RID: 3963 RVA: 0x00048D3C File Offset: 0x00046F3C
	public void healPartialAll(float degree)
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((Character)skaldBaseObject).restoreAllPartially(degree);
		}
	}

	// Token: 0x06000F7C RID: 3964 RVA: 0x00048D94 File Offset: 0x00046F94
	public void clearNegativeConditionsAll()
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((Character)skaldBaseObject).clearNegativeConditions();
		}
	}

	// Token: 0x06000F7D RID: 3965 RVA: 0x00048DEC File Offset: 0x00046FEC
	public void restShortAll()
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((Character)skaldBaseObject).restShort();
		}
	}

	// Token: 0x06000F7E RID: 3966 RVA: 0x00048E44 File Offset: 0x00047044
	public int getFoodRequiement()
	{
		int num = 0;
		foreach (Character character in this.getPartyList())
		{
			num += character.getFoodRequiement();
		}
		return num;
	}

	// Token: 0x06000F7F RID: 3967 RVA: 0x00048E9C File Offset: 0x0004709C
	public string addVitalityAll(int vitality)
	{
		string text = "";
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			Character character = (Character)skaldBaseObject;
			text = text + character.addVitality(vitality) + "\n";
		}
		return text;
	}

	// Token: 0x06000F80 RID: 3968 RVA: 0x00048F08 File Offset: 0x00047108
	public void addAttunementAll(int attunement)
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((Character)skaldBaseObject).addAttunement(attunement);
		}
	}

	// Token: 0x06000F81 RID: 3969 RVA: 0x00048F60 File Offset: 0x00047160
	public void resurrectAll()
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((Character)skaldBaseObject).resurrect();
		}
	}

	// Token: 0x06000F82 RID: 3970 RVA: 0x00048FB8 File Offset: 0x000471B8
	public string changePC(int j)
	{
		if (base.isEmpty())
		{
			return null;
		}
		int num = 0;
		int i = 0;
		while (i < this.objectList.Count)
		{
			if (this.objectList[i] == this.currentObject)
			{
				num = i + j;
				if (num >= this.objectList.Count)
				{
					num = 0;
				}
				if (num < 0)
				{
					num = this.objectList.Count - 1;
					break;
				}
				break;
			}
			else
			{
				i++;
			}
		}
		this.setCurrentPC(this.objectList[num] as Character);
		return this.currentObject.getName();
	}

	// Token: 0x06000F83 RID: 3971 RVA: 0x00049048 File Offset: 0x00047248
	public void startOfRoundUpkeep()
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((Character)skaldBaseObject).startOfRoundUpkeep();
		}
	}

	// Token: 0x06000F84 RID: 3972 RVA: 0x000490A0 File Offset: 0x000472A0
	public void startOfCombatUpkeep(Party opponentParty)
	{
		this.setSpotted();
		this.setGradualAlert();
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			Character character = (Character)skaldBaseObject;
			character.setOpponentParty(opponentParty);
			character.setCombatAllyParty(this);
			character.startOfCombatUpkeep();
		}
		this.setOriginalOrder();
	}

	// Token: 0x06000F85 RID: 3973 RVA: 0x00049118 File Offset: 0x00047318
	internal void rollMoral()
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((Character)skaldBaseObject).rollMoral();
		}
	}

	// Token: 0x06000F86 RID: 3974 RVA: 0x00049170 File Offset: 0x00047370
	internal void makeBloodyAll()
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((Character)skaldBaseObject).makeBloody();
		}
	}

	// Token: 0x040003E3 RID: 995
	private List<Character> originalOrderList = new List<Character>();

	// Token: 0x040003E4 RID: 996
	private Vehicle vehicle;

	// Token: 0x040003E5 RID: 997
	private int breath;

	// Token: 0x040003E6 RID: 998
	private Party interactParty;

	// Token: 0x040003E7 RID: 999
	private static int maxSizeForPlayerParty = 6;
}
