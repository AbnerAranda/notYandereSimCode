using System;

// Token: 0x020003BC RID: 956
[Serializable]
public class OptionSaveData
{
	// Token: 0x06001A25 RID: 6693 RVA: 0x001001D8 File Offset: 0x000FE3D8
	public static OptionSaveData ReadFromGlobals()
	{
		return new OptionSaveData
		{
			disableBloom = OptionGlobals.DisableBloom,
			disableFarAnimations = OptionGlobals.DisableFarAnimations,
			disableOutlines = OptionGlobals.DisableOutlines,
			disablePostAliasing = OptionGlobals.DisablePostAliasing,
			enableShadows = OptionGlobals.EnableShadows,
			drawDistance = OptionGlobals.DrawDistance,
			drawDistanceLimit = OptionGlobals.DrawDistanceLimit,
			fog = OptionGlobals.Fog,
			fpsIndex = OptionGlobals.FPSIndex,
			highPopulation = OptionGlobals.HighPopulation,
			lowDetailStudents = OptionGlobals.LowDetailStudents,
			particleCount = OptionGlobals.ParticleCount
		};
	}

	// Token: 0x06001A26 RID: 6694 RVA: 0x00100270 File Offset: 0x000FE470
	public static void WriteToGlobals(OptionSaveData data)
	{
		OptionGlobals.DisableBloom = data.disableBloom;
		OptionGlobals.DisableFarAnimations = data.disableFarAnimations;
		OptionGlobals.DisableOutlines = data.disableOutlines;
		OptionGlobals.DisablePostAliasing = data.disablePostAliasing;
		OptionGlobals.EnableShadows = data.enableShadows;
		OptionGlobals.DrawDistance = data.drawDistance;
		OptionGlobals.DrawDistanceLimit = data.drawDistanceLimit;
		OptionGlobals.Fog = data.fog;
		OptionGlobals.FPSIndex = data.fpsIndex;
		OptionGlobals.HighPopulation = data.highPopulation;
		OptionGlobals.LowDetailStudents = data.lowDetailStudents;
		OptionGlobals.ParticleCount = data.particleCount;
	}

	// Token: 0x04002915 RID: 10517
	public bool disableBloom;

	// Token: 0x04002916 RID: 10518
	public int disableFarAnimations = 5;

	// Token: 0x04002917 RID: 10519
	public bool disableOutlines;

	// Token: 0x04002918 RID: 10520
	public bool disablePostAliasing;

	// Token: 0x04002919 RID: 10521
	public bool enableShadows;

	// Token: 0x0400291A RID: 10522
	public int drawDistance;

	// Token: 0x0400291B RID: 10523
	public int drawDistanceLimit;

	// Token: 0x0400291C RID: 10524
	public bool fog;

	// Token: 0x0400291D RID: 10525
	public int fpsIndex;

	// Token: 0x0400291E RID: 10526
	public bool highPopulation;

	// Token: 0x0400291F RID: 10527
	public int lowDetailStudents;

	// Token: 0x04002920 RID: 10528
	public int particleCount;
}
