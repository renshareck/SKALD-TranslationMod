using System;
using UnityEngine;

// Token: 0x02000062 RID: 98
[Serializable]
public class SKALDKeyBindings : SkaldObjectList
{
	// Token: 0x060008B1 RID: 2225 RVA: 0x0002A3C4 File Offset: 0x000285C4
	public SKALDKeyBindings()
	{
		this.spellBook = (this.add(new SKALDKeyBindings.KeyBinding(KeyCode.G, "Spellbook", true)) as SKALDKeyBindings.KeyBinding);
		this.feats = (this.add(new SKALDKeyBindings.KeyBinding(KeyCode.F, "Feats", true)) as SKALDKeyBindings.KeyBinding);
		this.questLog = (this.add(new SKALDKeyBindings.KeyBinding(KeyCode.J, "Quests", true)) as SKALDKeyBindings.KeyBinding);
		this.nextCharacter = (this.add(new SKALDKeyBindings.KeyBinding(KeyCode.Q, "Next Character", true)) as SKALDKeyBindings.KeyBinding);
		this.characterSheet = (this.add(new SKALDKeyBindings.KeyBinding(KeyCode.C, "Character Sheet", true)) as SKALDKeyBindings.KeyBinding);
		this.levelUp = (this.add(new SKALDKeyBindings.KeyBinding(KeyCode.L, "Level Up", true)) as SKALDKeyBindings.KeyBinding);
		this.inventory = (this.add(new SKALDKeyBindings.KeyBinding(KeyCode.E, "Inventory", true)) as SKALDKeyBindings.KeyBinding);
		this.toggleEquipment = (this.add(new SKALDKeyBindings.KeyBinding(KeyCode.T, "Light Torch", true)) as SKALDKeyBindings.KeyBinding);
		this.quickLoad = (this.add(new SKALDKeyBindings.KeyBinding(KeyCode.F9, "Quick Load", true)) as SKALDKeyBindings.KeyBinding);
		this.quickSave = (this.add(new SKALDKeyBindings.KeyBinding(KeyCode.F5, "Quick Save", true)) as SKALDKeyBindings.KeyBinding);
		this.up = (this.add(new SKALDKeyBindings.KeyBinding(KeyCode.W, "Move N 1", true)) as SKALDKeyBindings.KeyBinding);
		this.down = (this.add(new SKALDKeyBindings.KeyBinding(KeyCode.S, "Move S 1", true)) as SKALDKeyBindings.KeyBinding);
		this.left = (this.add(new SKALDKeyBindings.KeyBinding(KeyCode.A, "Move W 1", true)) as SKALDKeyBindings.KeyBinding);
		this.right = (this.add(new SKALDKeyBindings.KeyBinding(KeyCode.D, "Move E 1", true)) as SKALDKeyBindings.KeyBinding);
		this.upAlt = (this.add(new SKALDKeyBindings.KeyBinding(KeyCode.UpArrow, "Move N 2", true)) as SKALDKeyBindings.KeyBinding);
		this.downAlt = (this.add(new SKALDKeyBindings.KeyBinding(KeyCode.DownArrow, "Move S 2", true)) as SKALDKeyBindings.KeyBinding);
		this.leftAlt = (this.add(new SKALDKeyBindings.KeyBinding(KeyCode.LeftArrow, "Move W 2", true)) as SKALDKeyBindings.KeyBinding);
		this.rightAlt = (this.add(new SKALDKeyBindings.KeyBinding(KeyCode.RightArrow, "Move E 2", true)) as SKALDKeyBindings.KeyBinding);
		this.highlight = (this.add(new SKALDKeyBindings.KeyBinding(KeyCode.LeftShift, "Highlight", true)) as SKALDKeyBindings.KeyBinding);
		this.console = (this.add(new SKALDKeyBindings.KeyBinding(KeyCode.Tab, "Console", true)) as SKALDKeyBindings.KeyBinding);
		this.hide = (this.add(new SKALDKeyBindings.KeyBinding(KeyCode.LeftControl, "Hide", true)) as SKALDKeyBindings.KeyBinding);
	}

	// Token: 0x060008B2 RID: 2226 RVA: 0x0002A668 File Offset: 0x00028868
	public void resetAll()
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((SKALDKeyBindings.KeyBinding)skaldBaseObject).reset();
		}
	}

	// Token: 0x060008B3 RID: 2227 RVA: 0x0002A6C0 File Offset: 0x000288C0
	public string updateKey(KeyCode newkey)
	{
		if (newkey == KeyCode.None)
		{
			return "This is not a valid value!";
		}
		if (this.getCurrentKey() == null)
		{
			return "No key selected.";
		}
		if (!this.getCurrentKey().canChangeKey())
		{
			return "This key cannot be changed.";
		}
		if (this.isValueLocked(newkey))
		{
			return "This value is taken by a key-binding that cannot be reassigned.";
		}
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			((SKALDKeyBindings.KeyBinding)skaldBaseObject).clearIfKey(newkey);
		}
		this.getCurrentKey().setKey(newkey);
		return "Updated key: " + this.getCurrentKey().getName();
	}

	// Token: 0x060008B4 RID: 2228 RVA: 0x0002A770 File Offset: 0x00028970
	private bool isValueLocked(KeyCode code)
	{
		foreach (SkaldBaseObject skaldBaseObject in this.objectList)
		{
			SKALDKeyBindings.KeyBinding keyBinding = (SKALDKeyBindings.KeyBinding)skaldBaseObject;
			if (keyBinding.getCurrentKey() == code && !keyBinding.canChangeKey())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060008B5 RID: 2229 RVA: 0x0002A7DC File Offset: 0x000289DC
	private SKALDKeyBindings.KeyBinding getCurrentKey()
	{
		if (base.getCurrentObject() != null)
		{
			return base.getCurrentObject() as SKALDKeyBindings.KeyBinding;
		}
		return null;
	}

	// Token: 0x060008B6 RID: 2230 RVA: 0x0002A7F3 File Offset: 0x000289F3
	public KeyCode getSpellBookKey()
	{
		return this.spellBook.getCurrentKey();
	}

	// Token: 0x060008B7 RID: 2231 RVA: 0x0002A800 File Offset: 0x00028A00
	public KeyCode getConsoleKey()
	{
		return this.console.getCurrentKey();
	}

	// Token: 0x060008B8 RID: 2232 RVA: 0x0002A80D File Offset: 0x00028A0D
	public KeyCode getFeatsKey()
	{
		return this.feats.getCurrentKey();
	}

	// Token: 0x060008B9 RID: 2233 RVA: 0x0002A81A File Offset: 0x00028A1A
	public KeyCode getQuestLogKey()
	{
		return this.questLog.getCurrentKey();
	}

	// Token: 0x060008BA RID: 2234 RVA: 0x0002A827 File Offset: 0x00028A27
	public KeyCode getNextCharacterKey()
	{
		return this.nextCharacter.getCurrentKey();
	}

	// Token: 0x060008BB RID: 2235 RVA: 0x0002A834 File Offset: 0x00028A34
	public KeyCode getCharacterSheetKey()
	{
		return this.characterSheet.getCurrentKey();
	}

	// Token: 0x060008BC RID: 2236 RVA: 0x0002A841 File Offset: 0x00028A41
	public KeyCode getLevelUpKey()
	{
		return this.levelUp.getCurrentKey();
	}

	// Token: 0x060008BD RID: 2237 RVA: 0x0002A84E File Offset: 0x00028A4E
	public KeyCode getHideKey()
	{
		return this.hide.getCurrentKey();
	}

	// Token: 0x060008BE RID: 2238 RVA: 0x0002A85B File Offset: 0x00028A5B
	public KeyCode getInventoryKey()
	{
		return this.inventory.getCurrentKey();
	}

	// Token: 0x060008BF RID: 2239 RVA: 0x0002A868 File Offset: 0x00028A68
	public KeyCode getToggleKey()
	{
		return this.toggleEquipment.getCurrentKey();
	}

	// Token: 0x060008C0 RID: 2240 RVA: 0x0002A875 File Offset: 0x00028A75
	public KeyCode getQuickLoadKey()
	{
		return this.quickLoad.getCurrentKey();
	}

	// Token: 0x060008C1 RID: 2241 RVA: 0x0002A882 File Offset: 0x00028A82
	public KeyCode getHighlightKey()
	{
		return this.highlight.getCurrentKey();
	}

	// Token: 0x060008C2 RID: 2242 RVA: 0x0002A88F File Offset: 0x00028A8F
	public KeyCode getQuickSaveKey()
	{
		return this.quickSave.getCurrentKey();
	}

	// Token: 0x060008C3 RID: 2243 RVA: 0x0002A89C File Offset: 0x00028A9C
	public KeyCode getUpKey()
	{
		return this.up.getCurrentKey();
	}

	// Token: 0x060008C4 RID: 2244 RVA: 0x0002A8A9 File Offset: 0x00028AA9
	public KeyCode getDownKey()
	{
		return this.down.getCurrentKey();
	}

	// Token: 0x060008C5 RID: 2245 RVA: 0x0002A8B6 File Offset: 0x00028AB6
	public KeyCode getLeftKey()
	{
		return this.left.getCurrentKey();
	}

	// Token: 0x060008C6 RID: 2246 RVA: 0x0002A8C3 File Offset: 0x00028AC3
	public KeyCode getRightKey()
	{
		return this.right.getCurrentKey();
	}

	// Token: 0x060008C7 RID: 2247 RVA: 0x0002A8D0 File Offset: 0x00028AD0
	public KeyCode getUpAltKey()
	{
		return this.upAlt.getCurrentKey();
	}

	// Token: 0x060008C8 RID: 2248 RVA: 0x0002A8DD File Offset: 0x00028ADD
	public KeyCode getDownAltKey()
	{
		return this.downAlt.getCurrentKey();
	}

	// Token: 0x060008C9 RID: 2249 RVA: 0x0002A8EA File Offset: 0x00028AEA
	public KeyCode getLeftAltKey()
	{
		return this.leftAlt.getCurrentKey();
	}

	// Token: 0x060008CA RID: 2250 RVA: 0x0002A8F7 File Offset: 0x00028AF7
	public KeyCode getRightAltKey()
	{
		return this.rightAlt.getCurrentKey();
	}

	// Token: 0x060008CB RID: 2251 RVA: 0x0002A904 File Offset: 0x00028B04
	public KeyCode getFeedbackKey()
	{
		if (this.feedback == null)
		{
			this.feedback = (this.add(new SKALDKeyBindings.KeyBinding(KeyCode.F2, "Feedback", true)) as SKALDKeyBindings.KeyBinding);
		}
		return this.feedback.getCurrentKey();
	}

	// Token: 0x040001F4 RID: 500
	private SKALDKeyBindings.KeyBinding spellBook;

	// Token: 0x040001F5 RID: 501
	private SKALDKeyBindings.KeyBinding feats;

	// Token: 0x040001F6 RID: 502
	private SKALDKeyBindings.KeyBinding questLog;

	// Token: 0x040001F7 RID: 503
	private SKALDKeyBindings.KeyBinding nextCharacter;

	// Token: 0x040001F8 RID: 504
	private SKALDKeyBindings.KeyBinding characterSheet;

	// Token: 0x040001F9 RID: 505
	private SKALDKeyBindings.KeyBinding levelUp;

	// Token: 0x040001FA RID: 506
	private SKALDKeyBindings.KeyBinding inventory;

	// Token: 0x040001FB RID: 507
	private SKALDKeyBindings.KeyBinding toggleEquipment;

	// Token: 0x040001FC RID: 508
	private SKALDKeyBindings.KeyBinding quickSave;

	// Token: 0x040001FD RID: 509
	private SKALDKeyBindings.KeyBinding quickLoad;

	// Token: 0x040001FE RID: 510
	private SKALDKeyBindings.KeyBinding up;

	// Token: 0x040001FF RID: 511
	private SKALDKeyBindings.KeyBinding down;

	// Token: 0x04000200 RID: 512
	private SKALDKeyBindings.KeyBinding left;

	// Token: 0x04000201 RID: 513
	private SKALDKeyBindings.KeyBinding right;

	// Token: 0x04000202 RID: 514
	private SKALDKeyBindings.KeyBinding upAlt;

	// Token: 0x04000203 RID: 515
	private SKALDKeyBindings.KeyBinding downAlt;

	// Token: 0x04000204 RID: 516
	private SKALDKeyBindings.KeyBinding leftAlt;

	// Token: 0x04000205 RID: 517
	private SKALDKeyBindings.KeyBinding rightAlt;

	// Token: 0x04000206 RID: 518
	private SKALDKeyBindings.KeyBinding highlight;

	// Token: 0x04000207 RID: 519
	private SKALDKeyBindings.KeyBinding console;

	// Token: 0x04000208 RID: 520
	private SKALDKeyBindings.KeyBinding feedback;

	// Token: 0x04000209 RID: 521
	private SKALDKeyBindings.KeyBinding hide;

	// Token: 0x0200020D RID: 525
	[Serializable]
	private class KeyBinding : SkaldBaseObject
	{
		// Token: 0x06001848 RID: 6216 RVA: 0x0006B32E File Offset: 0x0006952E
		public KeyBinding(KeyCode key, string label, bool canChange = true)
		{
			this.defaultKey = key;
			this.currentKey = key;
			this.setName(label);
			this.canChange = true;
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x0006B354 File Offset: 0x00069554
		public override string getName()
		{
			return string.Concat(new string[]
			{
				base.getName(),
				":\t",
				C64Color.WHITE_TAG,
				TextTools.adjustStringLength(this.currentKey.ToString(), 8),
				"</color>"
			});
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x0006B3A7 File Offset: 0x000695A7
		public override string getDescription()
		{
			if (this.canChange)
			{
				return "This binding can be changed.";
			}
			return "This binding cannot be changed.";
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x0006B3BC File Offset: 0x000695BC
		public bool canChangeKey()
		{
			return this.canChange;
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x0006B3C4 File Offset: 0x000695C4
		public void reset()
		{
			this.currentKey = this.defaultKey;
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x0006B3D2 File Offset: 0x000695D2
		public void setKey(KeyCode key)
		{
			this.currentKey = key;
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x0006B3DB File Offset: 0x000695DB
		public void clearKey()
		{
			this.currentKey = KeyCode.None;
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x0006B3E4 File Offset: 0x000695E4
		public KeyCode getCurrentKey()
		{
			return this.currentKey;
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x0006B3EC File Offset: 0x000695EC
		public void clearIfKey(KeyCode key)
		{
			if (this.getCurrentKey() == key)
			{
				this.clearKey();
			}
		}

		// Token: 0x040007EC RID: 2028
		private KeyCode defaultKey;

		// Token: 0x040007ED RID: 2029
		private KeyCode currentKey;

		// Token: 0x040007EE RID: 2030
		private bool canChange;
	}
}
