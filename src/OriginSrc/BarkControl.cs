using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200002D RID: 45
public class BarkControl
{
	// Token: 0x0600049B RID: 1179 RVA: 0x000160E8 File Offset: 0x000142E8
	public BarkControl(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	// Token: 0x0600049C RID: 1180 RVA: 0x0001616C File Offset: 0x0001436C
	public void addInfoBark(string message, int delay)
	{
		if (message == null || message == "")
		{
			return;
		}
		this.add(new BarkControl.Bark(message, this.x, this.y, this.infoColor, this.infoShadow, delay));
	}

	// Token: 0x0600049D RID: 1181 RVA: 0x000161B9 File Offset: 0x000143B9
	public void addInventoryBark(string message, int delay, int xOffset, int yOffset)
	{
		if (message == null || message == "")
		{
			return;
		}
		this.add(new BarkControl.BarkInventory(message, this.x + xOffset, this.y + yOffset));
	}

	// Token: 0x0600049E RID: 1182 RVA: 0x000161EC File Offset: 0x000143EC
	public void addNegativeBark(string message, int delay)
	{
		if (message == null || message == "")
		{
			return;
		}
		this.add(new BarkControl.Bark(message, this.x, this.y, this.negativeColor, this.negativeShadow, delay));
	}

	// Token: 0x0600049F RID: 1183 RVA: 0x0001623C File Offset: 0x0001443C
	public void addPositiveBark(string message, int delay)
	{
		if (message == null || message == "")
		{
			return;
		}
		this.add(new BarkControl.Bark(message, this.x, this.y, this.positiveColor, this.positiveShadow, delay));
	}

	// Token: 0x060004A0 RID: 1184 RVA: 0x0001628C File Offset: 0x0001448C
	public void addVocalBark(string message, int delay)
	{
		if (message == null || message == "")
		{
			return;
		}
		message = "\"" + message + "\"";
		this.add(new BarkControl.BarkLong(message, this.x, this.y, this.vocalColor, this.vocalShadow, delay));
	}

	// Token: 0x060004A1 RID: 1185 RVA: 0x000162EB File Offset: 0x000144EB
	private void add(BarkControl.Bark bark)
	{
		this.barks.Insert(0, bark);
	}

	// Token: 0x060004A2 RID: 1186 RVA: 0x000162FC File Offset: 0x000144FC
	private void removeBarkOverlap()
	{
		if (this.barks.Count <= 1)
		{
			return;
		}
		for (int i = 0; i < this.barks.Count - 1; i++)
		{
			BarkControl.Bark bark = this.barks[i];
			BarkControl.Bark bark2 = this.barks[i + 1];
			if (this.doBarksOverlap(bark, bark2) || bark2.getY() <= bark.getY())
			{
				bark2.setY(bark.getY() + bark.getHeight());
			}
		}
	}

	// Token: 0x060004A3 RID: 1187 RVA: 0x00016378 File Offset: 0x00014578
	public List<TextureTools.Sprite> getBarkSprites()
	{
		this.removeBarkOverlap();
		List<TextureTools.Sprite> list = new List<TextureTools.Sprite>();
		List<BarkControl.Bark> list2 = new List<BarkControl.Bark>();
		foreach (BarkControl.Bark bark in this.barks)
		{
			bark.update();
			if (bark.isDead())
			{
				list2.Add(bark);
			}
			else if (!bark.isDelaying())
			{
				list.Add(bark.getSprite());
			}
		}
		this.purgeByList(list2);
		return list;
	}

	// Token: 0x060004A4 RID: 1188 RVA: 0x0001640C File Offset: 0x0001460C
	private void purgeByList(List<BarkControl.Bark> purgeList)
	{
		foreach (BarkControl.Bark item in purgeList)
		{
			this.barks.Remove(item);
		}
	}

	// Token: 0x060004A5 RID: 1189 RVA: 0x00016460 File Offset: 0x00014660
	private bool doBarksOverlap(BarkControl.Bark bark1, BarkControl.Bark bark2)
	{
		return bark1.getY() == bark2.getY() || (bark1.getY() >= bark2.getY() && bark1.getY() <= bark2.getY() + bark2.getHeight()) || (bark1.getY() + bark1.getHeight() >= bark2.getY() && bark1.getY() + bark1.getHeight() <= bark2.getY() + bark2.getHeight());
	}

	// Token: 0x060004A6 RID: 1190 RVA: 0x000164D8 File Offset: 0x000146D8
	public bool waitForBarks()
	{
		using (List<BarkControl.Bark>.Enumerator enumerator = this.barks.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.shouldYouWait())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x040000FB RID: 251
	private List<BarkControl.Bark> barks = new List<BarkControl.Bark>();

	// Token: 0x040000FC RID: 252
	private Color32 infoColor = C64Color.White;

	// Token: 0x040000FD RID: 253
	private Color32 infoShadow = C64Color.GrayLight;

	// Token: 0x040000FE RID: 254
	private Color32 negativeColor = C64Color.RedLight;

	// Token: 0x040000FF RID: 255
	private Color32 negativeShadow = C64Color.Red;

	// Token: 0x04000100 RID: 256
	private Color32 positiveColor = C64Color.Yellow;

	// Token: 0x04000101 RID: 257
	private Color32 positiveShadow = C64Color.White;

	// Token: 0x04000102 RID: 258
	private Color32 vocalColor = C64Color.White;

	// Token: 0x04000103 RID: 259
	private Color32 vocalShadow = C64Color.GrayLight;

	// Token: 0x04000104 RID: 260
	private int x;

	// Token: 0x04000105 RID: 261
	private int y;

	// Token: 0x020001D8 RID: 472
	protected class BarkLong : BarkControl.Bark
	{
		// Token: 0x060016F5 RID: 5877 RVA: 0x0006690A File Offset: 0x00064B0A
		public BarkLong(string message, int x, int y, Color textColor, Color shadowColor, int delay) : base(message, x, y, textColor, shadowColor, delay)
		{
			base.setLife(100 + message.Length);
		}
	}

	// Token: 0x020001D9 RID: 473
	protected class Bark
	{
		// Token: 0x060016F6 RID: 5878 RVA: 0x0006692C File Offset: 0x00064B2C
		public Bark(string message, int x, int y, Color textColor, Color shadowColor, int delay)
		{
			this.x = x;
			this.y = y;
			this.maxY = y + 15;
			this.delay = delay;
			this.ySpeed = 1;
			int num = Mathf.RoundToInt(80f * GlobalSettings.getGamePlaySettings().getPopUpLifeModifer());
			this.setLife(num);
			this.barkTexture = StringPrinter.bakeFancyString(message, textColor, shadowColor);
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x0006699D File Offset: 0x00064B9D
		public TextureTools.Sprite getSprite()
		{
			return new TextureTools.Sprite(this.x - this.barkTexture.width / 2, this.y, this.barkTexture);
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x000669C4 File Offset: 0x00064BC4
		public void update()
		{
			if (this.isDelaying())
			{
				this.delay--;
			}
			else
			{
				this.updateLife();
				this.updatePosition();
			}
			if (this.shouldIBurn())
			{
				TextureTools.burnAwayPixels(this.barkTexture, 0.1f);
			}
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x00066A02 File Offset: 0x00064C02
		protected void setLife(int life)
		{
			this.life = life;
			this.burnThreshold = 20;
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x00066A13 File Offset: 0x00064C13
		public void moveUp(int amount)
		{
			this.y += amount;
			this.maxY += amount;
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x00066A31 File Offset: 0x00064C31
		public int getHeight()
		{
			return this.barkTexture.height;
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x00066A3E File Offset: 0x00064C3E
		public int getY()
		{
			return this.y;
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x00066A46 File Offset: 0x00064C46
		public void setY(int targetY)
		{
			this.y = targetY;
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x00066A4F File Offset: 0x00064C4F
		public bool isDelaying()
		{
			return this.delay > 0;
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x00066A5A File Offset: 0x00064C5A
		private void updateLife()
		{
			this.life--;
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x00066A6A File Offset: 0x00064C6A
		private void updatePosition()
		{
			if (this.y < this.maxY)
			{
				this.y += this.ySpeed;
			}
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x00066A8D File Offset: 0x00064C8D
		private bool shouldIBurn()
		{
			return this.life < this.burnThreshold;
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x00066A9D File Offset: 0x00064C9D
		public bool isDead()
		{
			return this.life < 1;
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x00066AA8 File Offset: 0x00064CA8
		public bool shouldYouWait()
		{
			return !this.shouldIBurn() && !this.isDead();
		}

		// Token: 0x04000742 RID: 1858
		private TextureTools.TextureData barkTexture;

		// Token: 0x04000743 RID: 1859
		private int life;

		// Token: 0x04000744 RID: 1860
		private int delay;

		// Token: 0x04000745 RID: 1861
		private int maxY;

		// Token: 0x04000746 RID: 1862
		private int burnThreshold;

		// Token: 0x04000747 RID: 1863
		private int x;

		// Token: 0x04000748 RID: 1864
		private int y;

		// Token: 0x04000749 RID: 1865
		private int ySpeed;
	}

	// Token: 0x020001DA RID: 474
	protected class BarkInventory : BarkControl.Bark
	{
		// Token: 0x06001704 RID: 5892 RVA: 0x00066ABD File Offset: 0x00064CBD
		public BarkInventory(string message, int x, int y) : base(message, x, y, C64Color.GreenLight, C64Color.Green, 0)
		{
			base.setLife(120);
		}
	}
}
