using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000011 RID: 17
public abstract class BaseCharacterComponent : SkaldBaseObject
{
	// Token: 0x060000CE RID: 206 RVA: 0x00005FDC File Offset: 0x000041DC
	protected BaseCharacterComponent(SKALDProjectData.GameDataObject rawData) : base(rawData)
	{
		List<string> affectedAttributes = this.getAffectedAttributes();
		if (affectedAttributes != null && affectedAttributes.Count != 0 && !BaseCharacterComponent.componentAffectsDictionary.ContainsKey(rawData.id))
		{
			BaseCharacterComponent.componentAffectsDictionary.Add(rawData.id, affectedAttributes);
		}
	}

	// Token: 0x060000CF RID: 207 RVA: 0x00006025 File Offset: 0x00004225
	public static List<string> GetAffectedComponents(string id)
	{
		if (BaseCharacterComponent.componentAffectsDictionary.ContainsKey(id))
		{
			return BaseCharacterComponent.componentAffectsDictionary[id];
		}
		return null;
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x00006041 File Offset: 0x00004241
	public virtual MapCutoutTemplate getTargetZoneCutout(Character user, int x, int y)
	{
		return null;
	}

	// Token: 0x060000D1 RID: 209 RVA: 0x00006044 File Offset: 0x00004244
	public virtual string getTargetingString()
	{
		return "Select a target to execute maneuver!";
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x0000604B File Offset: 0x0000424B
	public virtual bool isCombatActivated()
	{
		return false;
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x0000604E File Offset: 0x0000424E
	public virtual bool isNonCombatActivated()
	{
		return false;
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x00006051 File Offset: 0x00004251
	public virtual int getPowerLevel()
	{
		return 10;
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x00006058 File Offset: 0x00004258
	public override string getName()
	{
		string text = base.getName();
		if (text == "")
		{
			text = this.getId();
		}
		return text;
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x00006081 File Offset: 0x00004281
	protected virtual List<Color32> gridIconBaseColor()
	{
		return new List<Color32>
		{
			C64Color.Red,
			C64Color.RedLight
		};
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x000060A0 File Offset: 0x000042A0
	public override TextureTools.TextureData getGridIcon()
	{
		if (this.gridIcon == null)
		{
			string imagePath = this.getImagePath();
			if (imagePath != "")
			{
				this.gridIcon = TextureTools.loadTextureData("Images/GUIIcons/AbilityIcons/" + imagePath);
			}
			else
			{
				this.gridIcon = TextureTools.loadTextureData("Images/GUIIcons/AbilityIcons/Ability1");
				StringPrinter.buildGridIcon(this.gridIcon, 0, this.getName(), this.gridIconBaseColor()[0], this.gridIconBaseColor()[1]);
			}
		}
		return this.gridIcon;
	}

	// Token: 0x060000D8 RID: 216 RVA: 0x00006121 File Offset: 0x00004321
	public virtual UIButtonControlBase.ButtonData getButtonData(Character owner)
	{
		return new UIButtonControlBase.ButtonData(this.getGridIcon(), this.getName());
	}

	// Token: 0x060000D9 RID: 217 RVA: 0x00006134 File Offset: 0x00004334
	protected virtual List<string> getAffectedAttributes()
	{
		return null;
	}

	// Token: 0x060000DA RID: 218
	protected abstract string printComponentType();

	// Token: 0x060000DB RID: 219 RVA: 0x00006137 File Offset: 0x00004337
	protected virtual string printComponentTypeFormated()
	{
		return C64Color.GRAY_LIGHT_TAG + "[" + this.printComponentType() + "]</color>";
	}

	// Token: 0x060000DC RID: 220 RVA: 0x00006153 File Offset: 0x00004353
	public override string getFullDescription()
	{
		return this.printComponentTypeFormated() + "\n\n" + base.getFullDescription();
	}

	// Token: 0x060000DD RID: 221 RVA: 0x0000616C File Offset: 0x0000436C
	public override string getFullDescriptionAndHeader()
	{
		string text = "";
		if (this.getName() != "")
		{
			text = string.Concat(new string[]
			{
				text,
				C64Color.HEADER_TAG,
				this.getName().ToUpper(),
				C64Color.HEADER_CLOSING_TAG,
				"\n"
			});
		}
		return text + this.getFullDescription();
	}

	// Token: 0x060000DE RID: 222 RVA: 0x000061D5 File Offset: 0x000043D5
	public virtual int getModifierToAttribute(string attributeId)
	{
		return 0;
	}

	// Token: 0x04000009 RID: 9
	protected static Dictionary<string, List<string>> componentAffectsDictionary = new Dictionary<string, List<string>>();

	// Token: 0x0400000A RID: 10
	protected TextureTools.TextureData gridIcon;
}
