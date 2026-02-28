using System;
using System.Collections.Generic;

// Token: 0x02000055 RID: 85
public abstract class PopUpSceneTestBase : PopUpSingleAttributeTestBase
{
	// Token: 0x06000873 RID: 2163 RVA: 0x00029330 File Offset: 0x00027530
	protected PopUpSceneTestBase(Scene scene, int exitNumber, string description, List<string> options) : base(description, options, scene.getExitBranchDifficulty(exitNumber), scene.getExitBranchAttribute(exitNumber))
	{
		this.successTarget = scene.getSceneTargets(exitNumber);
		this.failureTarget = scene.getAlternateSceneTargets(exitNumber);
		this.allowCharacterSwap = true;
	}

	// Token: 0x06000874 RID: 2164 RVA: 0x0002936A File Offset: 0x0002756A
	public override void succeed()
	{
		MainControl.getDataControl().mountScene(this.successTarget);
		base.succeed();
	}

	// Token: 0x06000875 RID: 2165 RVA: 0x00029383 File Offset: 0x00027583
	public override void fail()
	{
		MainControl.getDataControl().mountScene(this.failureTarget);
		base.fail();
	}

	// Token: 0x040001E2 RID: 482
	protected string successTarget;

	// Token: 0x040001E3 RID: 483
	protected string failureTarget;
}
