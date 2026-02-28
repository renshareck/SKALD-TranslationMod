using System;

// Token: 0x0200002A RID: 42
public class IntroBackgroundParallaxControl : ParallaxControl
{
	// Token: 0x06000469 RID: 1129 RVA: 0x00015B30 File Offset: 0x00013D30
	public IntroBackgroundParallaxControl()
	{
		base.setBasePath("BackgroundParallax");
		base.addLayer("Sky", 0, 0);
		base.addLayer("Mountains", 2, 1);
		base.addFogLayer(8, 2, 40);
		base.addRainLayer("Characters", 8, 2);
		base.addLayer("Foreground", 16, 3);
		base.addFogLayer(0, 0, 0);
		base.toggleArrivalScroll();
	}
}
