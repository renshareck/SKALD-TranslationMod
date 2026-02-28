using System;
using System.Collections.Generic;

// Token: 0x0200001D RID: 29
public class CharacterFeatureControl
{
	// Token: 0x060001BA RID: 442 RVA: 0x0000A308 File Offset: 0x00008508
	public CharacterClass getClass(string id)
	{
		return this.classes.getFeature(id);
	}

	// Token: 0x060001BB RID: 443 RVA: 0x0000A316 File Offset: 0x00008516
	public CharacterClassArchetype getArchetype(string id)
	{
		return this.archetypes.getFeature(id);
	}

	// Token: 0x060001BC RID: 444 RVA: 0x0000A324 File Offset: 0x00008524
	public CharacterBackground getBackground(string id)
	{
		return this.backgrounds.getFeature(id);
	}

	// Token: 0x060001BD RID: 445 RVA: 0x0000A332 File Offset: 0x00008532
	public CharacterRace getRace(string id)
	{
		return this.races.getFeature(id);
	}

	// Token: 0x060001BE RID: 446 RVA: 0x0000A340 File Offset: 0x00008540
	public void addClass(CharacterClass c)
	{
		this.classes.addFeature(c);
	}

	// Token: 0x060001BF RID: 447 RVA: 0x0000A34E File Offset: 0x0000854E
	public void addArchetype(CharacterClassArchetype c)
	{
		this.archetypes.addFeature(c);
	}

	// Token: 0x060001C0 RID: 448 RVA: 0x0000A35C File Offset: 0x0000855C
	public void addRace(CharacterRace c)
	{
		this.races.addFeature(c);
	}

	// Token: 0x060001C1 RID: 449 RVA: 0x0000A36A File Offset: 0x0000856A
	public void addBackground(CharacterBackground c)
	{
		this.backgrounds.addFeature(c);
	}

	// Token: 0x04000046 RID: 70
	private CharacterFeatureControl.FeatureList<CharacterClassArchetype> archetypes = new CharacterFeatureControl.FeatureList<CharacterClassArchetype>();

	// Token: 0x04000047 RID: 71
	private CharacterFeatureControl.FeatureList<CharacterClass> classes = new CharacterFeatureControl.FeatureList<CharacterClass>();

	// Token: 0x04000048 RID: 72
	private CharacterFeatureControl.FeatureList<CharacterBackground> backgrounds = new CharacterFeatureControl.FeatureList<CharacterBackground>();

	// Token: 0x04000049 RID: 73
	private CharacterFeatureControl.FeatureList<CharacterRace> races = new CharacterFeatureControl.FeatureList<CharacterRace>();

	// Token: 0x020001C1 RID: 449
	private class FeatureList<T> where T : CharacterFeature
	{
		// Token: 0x0600165A RID: 5722 RVA: 0x00064240 File Offset: 0x00062440
		public void addFeature(T feature)
		{
			if (!this.features.ContainsKey(feature.getId()))
			{
				this.features.Add(feature.getId(), feature);
			}
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x00064274 File Offset: 0x00062474
		public T getFeature(string id)
		{
			if (this.features.ContainsKey(id))
			{
				return this.features[id];
			}
			return default(T);
		}

		// Token: 0x040006B2 RID: 1714
		private Dictionary<string, T> features = new Dictionary<string, T>();
	}
}
