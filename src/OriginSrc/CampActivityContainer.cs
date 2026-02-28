using System;
using System.Collections.Generic;

// Token: 0x02000003 RID: 3
public static class CampActivityContainer
{
	// Token: 0x0600001C RID: 28 RVA: 0x00002644 File Offset: 0x00000844
	private static List<CampActivityContainer.CampActivity> getActivityList()
	{
		if (CampActivityContainer.activityList == null)
		{
			CampActivityContainer.activityList = new List<CampActivityContainer.CampActivity>();
			CampActivityContainer.activityList.Add(new CampActivityContainer.ActivityTrain());
			CampActivityContainer.activityList.Add(new CampActivityContainer.ActivityForage());
			CampActivityContainer.activityList.Add(new CampActivityContainer.ActivityFletch());
			CampActivityContainer.activityList.Add(new CampActivityContainer.ActivityEntertain());
		}
		return CampActivityContainer.activityList;
	}

	// Token: 0x0600001D RID: 29 RVA: 0x000026A4 File Offset: 0x000008A4
	private static CampActivityContainer.CampActivity getActivity(CampActivityContainer.CampActivities activityEnum)
	{
		foreach (CampActivityContainer.CampActivity campActivity in CampActivityContainer.getActivityList())
		{
			if (campActivity.getEnum() == activityEnum)
			{
				return campActivity;
			}
		}
		return new CampActivityContainer.ActivityTrain();
	}

	// Token: 0x0600001E RID: 30 RVA: 0x00002704 File Offset: 0x00000904
	public static SkaldActionResult performPreferredCampActivity(CampActivityContainer.CampActivities activityEnum, Character character)
	{
		return CampActivityContainer.getActivity(activityEnum).performActivity(character);
	}

	// Token: 0x0600001F RID: 31 RVA: 0x00002714 File Offset: 0x00000914
	public static CampActivityContainer.CampActivities cycleCampActivity(CampActivityContainer.CampActivities currentActivityEnum, int direction)
	{
		if (CampActivityContainer.getActivityList().Count == 0)
		{
			return CampActivityContainer.CampActivities.Train;
		}
		int num = 0;
		using (List<CampActivityContainer.CampActivity>.Enumerator enumerator = CampActivityContainer.getActivityList().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.getEnum() == currentActivityEnum)
				{
					break;
				}
				num++;
			}
		}
		num += direction;
		if (num < 0)
		{
			num = CampActivityContainer.getActivityList().Count - 1;
		}
		else if (num >= CampActivityContainer.getActivityList().Count)
		{
			num = 0;
		}
		return CampActivityContainer.getActivityList()[num].getEnum();
	}

	// Token: 0x06000020 RID: 32 RVA: 0x000027B4 File Offset: 0x000009B4
	public static string getCampActivityName(CampActivityContainer.CampActivities activityEnum)
	{
		CampActivityContainer.CampActivity activity = CampActivityContainer.getActivity(activityEnum);
		if (activity == null)
		{
			return "";
		}
		return activity.getName();
	}

	// Token: 0x06000021 RID: 33 RVA: 0x000027D8 File Offset: 0x000009D8
	public static string getCampActivityDescription(CampActivityContainer.CampActivities activityEnum, Character c)
	{
		CampActivityContainer.CampActivity activity = CampActivityContainer.getActivity(activityEnum);
		if (activity == null)
		{
			return "";
		}
		return "ACTIVITY: " + activity.getName() + "\n\n" + activity.getDescription(c);
	}

	// Token: 0x04000003 RID: 3
	private static List<CampActivityContainer.CampActivity> activityList;

	// Token: 0x020001B1 RID: 433
	public enum CampActivities
	{
		// Token: 0x04000684 RID: 1668
		Train,
		// Token: 0x04000685 RID: 1669
		Forage,
		// Token: 0x04000686 RID: 1670
		Fletch,
		// Token: 0x04000687 RID: 1671
		Entertain
	}

	// Token: 0x020001B2 RID: 434
	private class ActivityForage : CampActivityContainer.CampActivity
	{
		// Token: 0x060015EB RID: 5611 RVA: 0x00061F0A File Offset: 0x0006010A
		public override CampActivityContainer.CampActivities getEnum()
		{
			return CampActivityContainer.CampActivities.Forage;
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x00061F0D File Offset: 0x0006010D
		public override string getDescription(Character c)
		{
			return "The character has a chance to find some extra food depending on their Survival skill. The food found cannot be consumed until the next rest period.\n\n" + base.getDescription(c);
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x00061F20 File Offset: 0x00060120
		protected override AttributesControl.CoreAttributes getTestAttribute()
		{
			return AttributesControl.CoreAttributes.ATT_Survival;
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x00061F24 File Offset: 0x00060124
		public override SkaldActionResult performActivity(Character c)
		{
			SkaldTestRandomVsStatic skaldTestRandomVsStatic = base.rollTest(c);
			string resultString;
			if (skaldTestRandomVsStatic.wasSuccess())
			{
				int num = skaldTestRandomVsStatic.getDegreeOfResult() / 4;
				if (num == 0)
				{
					num = 1;
				}
				string text = "ration";
				if (num > 1)
				{
					text = "rations";
				}
				resultString = string.Concat(new string[]
				{
					c.getName(),
					" found ",
					C64Color.CYAN_TAG,
					num.ToString(),
					"</color> ",
					text,
					"!"
				});
				Item item = GameData.instantiateItem("ITE_FoodRation");
				if (item != null)
				{
					item.setCount(num);
					c.getInventory().addItem(item);
				}
			}
			else
			{
				resultString = c.getName() + " failed to find any food.";
			}
			return new SkaldActionResult(true, skaldTestRandomVsStatic.wasSuccess(), resultString, true);
		}
	}

	// Token: 0x020001B3 RID: 435
	private class ActivityTrain : CampActivityContainer.CampActivity
	{
		// Token: 0x060015F0 RID: 5616 RVA: 0x00061FFB File Offset: 0x000601FB
		public override CampActivityContainer.CampActivities getEnum()
		{
			return CampActivityContainer.CampActivities.Train;
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x00061FFE File Offset: 0x000601FE
		public override string getDescription(Character c)
		{
			return "The character attempts to instruct the party using their Lore skill. Success gives a small XP reward.\n\n" + base.getDescription(c);
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x00062011 File Offset: 0x00060211
		protected override AttributesControl.CoreAttributes getTestAttribute()
		{
			return AttributesControl.CoreAttributes.ATT_Lore;
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x00062018 File Offset: 0x00060218
		public override SkaldActionResult performActivity(Character c)
		{
			SkaldTestRandomVsStatic skaldTestRandomVsStatic = base.rollTest(c);
			string resultString;
			if (skaldTestRandomVsStatic.wasSuccess())
			{
				int gainedXP = c.getLevel() * 2 + skaldTestRandomVsStatic.getDegreeOfResult() * 4;
				c.getMainParty().addXp(gainedXP);
				resultString = string.Concat(new string[]
				{
					c.getName(),
					" successfully instructed their companions giving them ",
					C64Color.CYAN_TAG,
					gainedXP.ToString(),
					"</color> XP!"
				});
			}
			else
			{
				resultString = c.getName() + " failed to instruct their companions.";
			}
			return new SkaldActionResult(true, skaldTestRandomVsStatic.wasSuccess(), resultString, true);
		}
	}

	// Token: 0x020001B4 RID: 436
	private class ActivityFletch : CampActivityContainer.CampActivity
	{
		// Token: 0x060015F5 RID: 5621 RVA: 0x000620B9 File Offset: 0x000602B9
		public override CampActivityContainer.CampActivities getEnum()
		{
			return CampActivityContainer.CampActivities.Fletch;
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x000620BC File Offset: 0x000602BC
		public override string getDescription(Character c)
		{
			return "The character has a chance to create a small number of Hunting Arrows depending on their Crafting skill.\n\n" + base.getDescription(c);
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x000620CF File Offset: 0x000602CF
		protected override AttributesControl.CoreAttributes getTestAttribute()
		{
			return AttributesControl.CoreAttributes.ATT_Crafting;
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x000620D4 File Offset: 0x000602D4
		public override SkaldActionResult performActivity(Character c)
		{
			SkaldTestRandomVsStatic skaldTestRandomVsStatic = base.rollTest(c);
			string resultString;
			if (skaldTestRandomVsStatic.wasSuccess())
			{
				int num = skaldTestRandomVsStatic.getDegreeOfResult() / 2;
				if (num == 0)
				{
					num = 1;
				}
				string text = "arrow";
				if (num > 1)
				{
					text = "arrows";
				}
				resultString = string.Concat(new string[]
				{
					c.getName(),
					" fletched ",
					C64Color.CYAN_TAG,
					num.ToString(),
					"</color> ",
					text,
					"!"
				});
				Item item = GameData.instantiateItem("ITE_ArrowHunting");
				if (item != null)
				{
					item.setCount(num);
					c.getInventory().addItem(item);
				}
			}
			else
			{
				resultString = c.getName() + " failed to fletch arrows.";
			}
			return new SkaldActionResult(true, skaldTestRandomVsStatic.wasSuccess(), resultString, true);
		}
	}

	// Token: 0x020001B5 RID: 437
	private class ActivityEntertain : CampActivityContainer.CampActivity
	{
		// Token: 0x060015FA RID: 5626 RVA: 0x000621AB File Offset: 0x000603AB
		public override CampActivityContainer.CampActivities getEnum()
		{
			return CampActivityContainer.CampActivities.Entertain;
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x000621AE File Offset: 0x000603AE
		public override string getDescription(Character c)
		{
			return "The character tries to lift their comrades' spirits using their Diplomacy skill.\n\nSuccess adds the Motivated Condition to all party-members.\n\n" + base.getDescription(c);
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x000621C1 File Offset: 0x000603C1
		protected override AttributesControl.CoreAttributes getTestAttribute()
		{
			return AttributesControl.CoreAttributes.ATT_Diplomacy;
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x000621C4 File Offset: 0x000603C4
		protected override int getTestDifficulty()
		{
			return 12;
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x000621C8 File Offset: 0x000603C8
		public override SkaldActionResult performActivity(Character c)
		{
			SkaldTestRandomVsStatic skaldTestRandomVsStatic = base.rollTest(c);
			string resultString;
			if (skaldTestRandomVsStatic.wasSuccess())
			{
				resultString = c.getName() + " successfully entertained their companions!";
				MainControl.getDataControl().addConditionToAll("CON_Motivated");
			}
			else
			{
				resultString = c.getName() + " failed to entertain their companions.";
			}
			return new SkaldActionResult(true, skaldTestRandomVsStatic.wasSuccess(), resultString, true);
		}
	}

	// Token: 0x020001B6 RID: 438
	private abstract class CampActivity
	{
		// Token: 0x06001600 RID: 5632
		public abstract CampActivityContainer.CampActivities getEnum();

		// Token: 0x06001601 RID: 5633 RVA: 0x00062234 File Offset: 0x00060434
		public virtual string getName()
		{
			return this.getEnum().ToString();
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x00062258 File Offset: 0x00060458
		public virtual string getDescription(Character c)
		{
			return string.Concat(new string[]
			{
				c.getName(),
				" must roll",
				C64Color.CYAN_TAG,
				" 2d6 + ",
				c.getCurrentAttributeValue(this.getTestAttribute()).ToString(),
				" ",
				GameData.getAttributeName(this.getTestAttribute()),
				"</color> versus ",
				C64Color.WHITE_TAG,
				this.getTestDifficulty().ToString(),
				"</color> Difficulty."
			});
		}

		// Token: 0x06001603 RID: 5635
		public abstract SkaldActionResult performActivity(Character c);

		// Token: 0x06001604 RID: 5636
		protected abstract AttributesControl.CoreAttributes getTestAttribute();

		// Token: 0x06001605 RID: 5637 RVA: 0x000622EA File Offset: 0x000604EA
		protected virtual int getTestDifficulty()
		{
			return 10;
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x000622EE File Offset: 0x000604EE
		protected SkaldTestRandomVsStatic rollTest(Character c)
		{
			return new SkaldTestRandomVsStatic(c, this.getTestAttribute(), this.getTestDifficulty(), 1);
		}
	}
}
