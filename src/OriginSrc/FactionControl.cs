using System;
using System.Collections.Generic;

// Token: 0x020000B9 RID: 185
public static class FactionControl
{
	// Token: 0x06000B8D RID: 2957 RVA: 0x00036CE5 File Offset: 0x00034EE5
	public static SkaldObjectList getFactionList()
	{
		if (FactionControl.factionList == null)
		{
			FactionControl.populateFactionList();
		}
		return FactionControl.factionList;
	}

	// Token: 0x06000B8E RID: 2958 RVA: 0x00036CF8 File Offset: 0x00034EF8
	public static void nextFaction(int i)
	{
		FactionControl.factionList.setNextObject(i);
	}

	// Token: 0x06000B8F RID: 2959 RVA: 0x00036D08 File Offset: 0x00034F08
	public static void populateFactionList()
	{
		MainControl.log("Initializing Faction Control");
		FactionControl.factionList = new SkaldObjectList("Factions");
		List<SKALDProjectData.Objects.FactionDataContainer.FactionData> list = GameData.getFactionList();
		if (list != null && list.Count != 0)
		{
			foreach (SKALDProjectData.Objects.FactionDataContainer.FactionData data in list)
			{
				FactionControl.factionList.add(new FactionControl.Faction(data));
			}
		}
		MainControl.log("Completed Faction Control");
	}

	// Token: 0x06000B90 RID: 2960 RVA: 0x00036D94 File Offset: 0x00034F94
	public static string getFactionTitle()
	{
		return FactionControl.getFactionList().getCurrentObjectTitle();
	}

	// Token: 0x06000B91 RID: 2961 RVA: 0x00036DA0 File Offset: 0x00034FA0
	public static string getFactionDescription()
	{
		return FactionControl.getFactionList().getCurrentObjectFullDescriptionAndHeader();
	}

	// Token: 0x06000B92 RID: 2962 RVA: 0x00036DAC File Offset: 0x00034FAC
	public static string printFactions()
	{
		if (FactionControl.getFactionList() == null)
		{
			return "No factions.";
		}
		return FactionControl.getFactionList().printListSimplifiedString();
	}

	// Token: 0x06000B93 RID: 2963 RVA: 0x00036DC5 File Offset: 0x00034FC5
	public static FactionControl.Faction getFaction(string id)
	{
		if (id == null || id == "")
		{
			return null;
		}
		FactionControl.Faction faction = (FactionControl.Faction)FactionControl.getFactionList().getObject(id);
		if (faction == null)
		{
			MainControl.logError("Could not find faction " + id);
		}
		return faction;
	}

	// Token: 0x06000B94 RID: 2964 RVA: 0x00036DFC File Offset: 0x00034FFC
	public static bool isFactionHostile(string id)
	{
		FactionControl.Faction faction = FactionControl.getFaction(id);
		return faction != null && faction.isHostile();
	}

	// Token: 0x06000B95 RID: 2965 RVA: 0x00036E1C File Offset: 0x0003501C
	public static string makeFactionHostile(string id)
	{
		FactionControl.Faction faction = FactionControl.getFaction(id);
		if (faction == null)
		{
			return "No faction found!";
		}
		faction.makeHostile();
		return faction.getName() + " turns hostile!";
	}

	// Token: 0x06000B96 RID: 2966 RVA: 0x00036E50 File Offset: 0x00035050
	public static string makeFactionFriendly(string id)
	{
		FactionControl.Faction faction = FactionControl.getFaction(id);
		if (faction == null)
		{
			return "No faction found!";
		}
		faction.makeFriendly();
		return faction.getName() + " turns friendly!";
	}

	// Token: 0x06000B97 RID: 2967 RVA: 0x00036E83 File Offset: 0x00035083
	public static void setFactionList(SkaldObjectList list)
	{
		FactionControl.factionList = list;
	}

	// Token: 0x04000314 RID: 788
	private static SkaldObjectList factionList;

	// Token: 0x02000241 RID: 577
	[Serializable]
	public class FactionRelationships
	{
		// Token: 0x0600191C RID: 6428 RVA: 0x0006E056 File Offset: 0x0006C256
		public FactionRelationships(Character character)
		{
			this.character = character;
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x0006E078 File Offset: 0x0006C278
		public string addFactionMembership(string id)
		{
			if (id == "")
			{
				return "No faction ID is given";
			}
			FactionControl.Faction faction = FactionControl.getFaction(id);
			if (faction == null)
			{
				return "No such faction: " + id;
			}
			if (this.isFactionMember(id))
			{
				return "Already a member of " + faction.getName();
			}
			this.factionList.add(faction);
			faction.applyData(this.character);
			return "Joined faction: " + faction.getName();
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x0006E0F4 File Offset: 0x0006C2F4
		public void makeAllFactionsHostile()
		{
			foreach (SkaldBaseObject skaldBaseObject in this.factionList.getObjectList())
			{
				((FactionControl.Faction)skaldBaseObject).makeHostile();
			}
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x0006E150 File Offset: 0x0006C350
		public bool areAnyFactionsHostile()
		{
			using (List<SkaldBaseObject>.Enumerator enumerator = this.factionList.getObjectList().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((FactionControl.Faction)enumerator.Current).isHostile())
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x0006E1B4 File Offset: 0x0006C3B4
		public bool isFactionMember(string id)
		{
			return this.factionList.containsObject(id);
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x0006E1C7 File Offset: 0x0006C3C7
		public string printFactions()
		{
			if (this.factionList.isEmpty())
			{
				return "Unaligned";
			}
			return this.factionList.printListSingleLine();
		}

		// Token: 0x040008B8 RID: 2232
		private SkaldObjectList factionList = new SkaldObjectList("Factions:");

		// Token: 0x040008B9 RID: 2233
		private Character character;
	}

	// Token: 0x02000242 RID: 578
	[Serializable]
	public class Faction : SkaldInstanceObject
	{
		// Token: 0x06001922 RID: 6434 RVA: 0x0006E1E7 File Offset: 0x0006C3E7
		public Faction(SKALDProjectData.Objects.FactionDataContainer.FactionData data) : base(data)
		{
			this.playerAttitude = data.playerRelation;
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x0006E204 File Offset: 0x0006C404
		private SKALDProjectData.Objects.FactionDataContainer.FactionData getRawData()
		{
			return GameData.getFactionRawData(this.getId());
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x0006E211 File Offset: 0x0006C411
		public bool isHostile()
		{
			return this.hostile;
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x0006E21C File Offset: 0x0006C41C
		private bool willTurnHostile()
		{
			SKALDProjectData.Objects.FactionDataContainer.FactionData rawData = this.getRawData();
			return rawData == null || rawData.willGoHostile;
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x0006E23C File Offset: 0x0006C43C
		private bool willTurnFriendly()
		{
			SKALDProjectData.Objects.FactionDataContainer.FactionData rawData = this.getRawData();
			return rawData == null || rawData.willGoFriendly;
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x0006E25C File Offset: 0x0006C45C
		private string getHostileTrigger()
		{
			SKALDProjectData.Objects.FactionDataContainer.FactionData rawData = this.getRawData();
			if (rawData == null)
			{
				return "";
			}
			return rawData.hostileTrigger;
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x0006E280 File Offset: 0x0006C480
		private string getFriendlyTrigger()
		{
			SKALDProjectData.Objects.FactionDataContainer.FactionData rawData = this.getRawData();
			if (rawData == null)
			{
				return "";
			}
			return rawData.friendlyTrigger;
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x0006E2A3 File Offset: 0x0006C4A3
		public void makeHostile()
		{
			if (!this.hostile && this.willTurnHostile())
			{
				this.hostile = true;
				base.processString(this.getHostileTrigger(), null);
				HoverElementControl.addHoverTextRed(this.getName(), "Faction has turned HOSTILE.");
			}
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x0006E2DA File Offset: 0x0006C4DA
		public void makeFriendly()
		{
			if (this.hostile && this.willTurnFriendly())
			{
				this.hostile = false;
				base.processString(this.getFriendlyTrigger(), null);
				HoverElementControl.addHoverText(this.getName(), "Faction no longer hostile.");
			}
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x0006E314 File Offset: 0x0006C514
		public override string getName()
		{
			SKALDProjectData.Objects.FactionDataContainer.FactionData rawData = this.getRawData();
			if (rawData == null)
			{
				return "";
			}
			return rawData.title;
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x0006E338 File Offset: 0x0006C538
		public override string getListName()
		{
			string text = this.getName();
			if (this.isHostile())
			{
				text += " [Hostile]";
			}
			return text;
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x0006E364 File Offset: 0x0006C564
		public override string getDescription()
		{
			string text = "";
			SKALDProjectData.Objects.FactionDataContainer.FactionData rawData = this.getRawData();
			if (rawData != null)
			{
				text += rawData.description;
			}
			if (this.isHostile())
			{
				text += "\n\nFaction is HOSTILE!";
			}
			else
			{
				text += "\n\nFaction is NEUTRAL.";
			}
			return text;
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x0006E3B4 File Offset: 0x0006C5B4
		public string getApperancePackId()
		{
			SKALDProjectData.Objects.FactionDataContainer.FactionData rawData = this.getRawData();
			if (rawData == null)
			{
				return "";
			}
			return rawData.apperancePack;
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x0006E3D8 File Offset: 0x0006C5D8
		private string getLoadoutId()
		{
			SKALDProjectData.Objects.FactionDataContainer.FactionData rawData = this.getRawData();
			if (rawData == null)
			{
				return "";
			}
			return rawData.loadout;
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x0006E3FB File Offset: 0x0006C5FB
		public virtual void applyData(Character character)
		{
			character.applyLoadout(this.getLoadoutId());
			character.applyApperancePack(this.getApperancePackId());
		}

		// Token: 0x040008BA RID: 2234
		private bool hostile;

		// Token: 0x040008BB RID: 2235
		private int playerAttitude = 50;
	}
}
