using System;
using System.Collections.Generic;

// Token: 0x02000040 RID: 64
public static class NewSpellsToLearnControl
{
	// Token: 0x06000800 RID: 2048 RVA: 0x00027C91 File Offset: 0x00025E91
	public static void addNewSpellsToLearn(string spellSchool, int tier, int number, Character target)
	{
		if (NewSpellsToLearnControl.spellsToLearnContainer == null)
		{
			NewSpellsToLearnControl.spellsToLearnContainer = new NewSpellsToLearnControl.NewSpellsToLearnContainer(target);
		}
		if (NewSpellsToLearnControl.spellsToLearnContainer.validateTarget(target))
		{
			NewSpellsToLearnControl.spellsToLearnContainer.addSpellEntry(spellSchool, tier, number);
			return;
		}
		MainControl.logError("Spells to learn container error with target.");
	}

	// Token: 0x06000801 RID: 2049 RVA: 0x00027CCA File Offset: 0x00025ECA
	public static bool hasSpellsToLearn()
	{
		return NewSpellsToLearnControl.spellsToLearnContainer != null;
	}

	// Token: 0x06000802 RID: 2050 RVA: 0x00027CD4 File Offset: 0x00025ED4
	public static void finalize()
	{
		if (NewSpellsToLearnControl.spellsToLearnContainer == null)
		{
			return;
		}
		NewSpellsToLearnControl.spellsToLearnContainer.finalize();
		NewSpellsToLearnControl.spellsToLearnContainer = null;
	}

	// Token: 0x040001AD RID: 429
	private static NewSpellsToLearnControl.NewSpellsToLearnContainer spellsToLearnContainer;

	// Token: 0x02000200 RID: 512
	public class NewSpellsToLearnContainer
	{
		// Token: 0x060017DF RID: 6111 RVA: 0x00069EBD File Offset: 0x000680BD
		public NewSpellsToLearnContainer(Character target)
		{
			this.target = target;
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x00069ED7 File Offset: 0x000680D7
		public bool validateTarget(Character target)
		{
			return this.target == target;
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x00069EE4 File Offset: 0x000680E4
		public void addSpellEntry(string spellSchool, int tier, int number)
		{
			foreach (NewSpellsToLearnControl.NewSpellsToLearnContainer.SchoolEntry schoolEntry in this.schoolEntries)
			{
				if (schoolEntry.isThisEntry(spellSchool))
				{
					schoolEntry.addTier(tier, number);
					return;
				}
			}
			this.schoolEntries.Add(new NewSpellsToLearnControl.NewSpellsToLearnContainer.SchoolEntry(spellSchool, tier, number));
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x00069F58 File Offset: 0x00068158
		public void finalize()
		{
			foreach (NewSpellsToLearnControl.NewSpellsToLearnContainer.SchoolEntry schoolEntry in this.schoolEntries)
			{
				schoolEntry.finalize(this.target);
			}
		}

		// Token: 0x040007DB RID: 2011
		private List<NewSpellsToLearnControl.NewSpellsToLearnContainer.SchoolEntry> schoolEntries = new List<NewSpellsToLearnControl.NewSpellsToLearnContainer.SchoolEntry>();

		// Token: 0x040007DC RID: 2012
		private Character target;

		// Token: 0x0200032E RID: 814
		private class SchoolEntry
		{
			// Token: 0x06001CA1 RID: 7329 RVA: 0x0007B0AE File Offset: 0x000792AE
			public SchoolEntry(string id, int tier, int number)
			{
				this.addTier(tier, number);
				this.id = id;
			}

			// Token: 0x06001CA2 RID: 7330 RVA: 0x0007B0DB File Offset: 0x000792DB
			public bool isThisEntry(string id)
			{
				return this.id == id;
			}

			// Token: 0x06001CA3 RID: 7331 RVA: 0x0007B0EC File Offset: 0x000792EC
			public void finalize(Character character)
			{
				this.tiers.Reverse();
				foreach (NewSpellsToLearnControl.NewSpellsToLearnContainer.SchoolEntry.TierEntry tierEntry in this.tiers)
				{
					PopUpControl.addPopUpNewSpells(this.id, tierEntry.getTier(), tierEntry.getNumber(), character);
				}
			}

			// Token: 0x06001CA4 RID: 7332 RVA: 0x0007B15C File Offset: 0x0007935C
			public void addTier(int tier, int number)
			{
				foreach (NewSpellsToLearnControl.NewSpellsToLearnContainer.SchoolEntry.TierEntry tierEntry in this.tiers)
				{
					if (tierEntry.isThisEntry(tier))
					{
						tierEntry.addNumber(number);
						return;
					}
				}
				NewSpellsToLearnControl.NewSpellsToLearnContainer.SchoolEntry.TierEntry tierEntry2 = new NewSpellsToLearnControl.NewSpellsToLearnContainer.SchoolEntry.TierEntry(tier, number);
				if (this.tiers.Count == 0)
				{
					this.tiers.Add(tierEntry2);
					return;
				}
				for (int i = 0; i < this.tiers.Count; i++)
				{
					if (tierEntry2.getTier() < this.tiers[i].getTier())
					{
						this.tiers.Insert(i, tierEntry2);
						return;
					}
				}
				this.tiers.Add(tierEntry2);
			}

			// Token: 0x04000ABC RID: 2748
			private string id = "";

			// Token: 0x04000ABD RID: 2749
			private List<NewSpellsToLearnControl.NewSpellsToLearnContainer.SchoolEntry.TierEntry> tiers = new List<NewSpellsToLearnControl.NewSpellsToLearnContainer.SchoolEntry.TierEntry>();

			// Token: 0x020003F2 RID: 1010
			private class TierEntry
			{
				// Token: 0x06001DB7 RID: 7607 RVA: 0x0007DA34 File Offset: 0x0007BC34
				public TierEntry(int tier, int number)
				{
					this.tier = tier;
					this.number = number;
				}

				// Token: 0x06001DB8 RID: 7608 RVA: 0x0007DA4A File Offset: 0x0007BC4A
				public void addNumber(int number)
				{
					this.number += number;
				}

				// Token: 0x06001DB9 RID: 7609 RVA: 0x0007DA5A File Offset: 0x0007BC5A
				public int getTier()
				{
					return this.tier;
				}

				// Token: 0x06001DBA RID: 7610 RVA: 0x0007DA62 File Offset: 0x0007BC62
				public int getNumber()
				{
					return this.number;
				}

				// Token: 0x06001DBB RID: 7611 RVA: 0x0007DA6A File Offset: 0x0007BC6A
				public bool isThisEntry(int tier)
				{
					return this.tier == tier;
				}

				// Token: 0x04000C95 RID: 3221
				private int tier;

				// Token: 0x04000C96 RID: 3222
				private int number;
			}
		}
	}
}
