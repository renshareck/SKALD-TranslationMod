using System;
using System.Collections.Generic;

// Token: 0x02000157 RID: 343
public abstract class UIGridLists : UIGridBase
{
	// Token: 0x06001326 RID: 4902 RVA: 0x00054AC6 File Offset: 0x00052CC6
	protected UIGridLists(int width, int height) : base(width, height)
	{
	}

	// Token: 0x06001327 RID: 4903 RVA: 0x00054AD0 File Offset: 0x00052CD0
	public void update(Inventory inventory, Character character)
	{
		base.update();
		if (inventory != null)
		{
			List<Item> list = new List<Item>();
			foreach (SkaldBaseObject skaldBaseObject in inventory.getObjectList())
			{
				Item item = (Item)skaldBaseObject;
				list.Add(item);
			}
			this.setButtons(list, inventory.getCurrentObject(), character);
		}
	}

	// Token: 0x06001328 RID: 4904 RVA: 0x00054B48 File Offset: 0x00052D48
	public void update(List<Item> items, Item currentObject, Character character)
	{
		base.update();
		this.setButtons(items, currentObject, character);
	}

	// Token: 0x06001329 RID: 4905 RVA: 0x00054B5C File Offset: 0x00052D5C
	private void setButtons(List<Item> items, Item currentObject, Character character)
	{
		if (items == null || items.Count == 0)
		{
			return;
		}
		List<Item> list = new List<Item>();
		int num = 0;
		for (int i = 0; i < items.Count; i++)
		{
			if (num >= base.getElements().Count)
			{
				return;
			}
			Item item = items[i];
			list.Add(item);
			if (i == items.Count - 1 || list.Count == this.width)
			{
				(base.getElements()[num] as UIGridBase.UIGridRow).setButtons(list, currentObject, character);
				list = new List<Item>();
				num++;
			}
		}
	}

	// Token: 0x0600132A RID: 4906 RVA: 0x00054BE8 File Offset: 0x00052DE8
	protected void setButton(Character character, Item item, int x, int y, string backupPath)
	{
		(base.getElements()[y] as UIGridBase.UIGridRow).setButton(character, item, x, backupPath);
	}
}
