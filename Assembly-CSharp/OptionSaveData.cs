using System;

// Token: 0x020003B7 RID: 951
[Serializable]
public class OptionSaveData
{
	// Token: 0x06001A04 RID: 6660 RVA: 0x000FDAD4 File Offset: 0x000FBCD4
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

	// Token: 0x06001A05 RID: 6661 RVA: 0x000FDB6C File Offset: 0x000FBD6C
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

	// Token: 0x040028B8 RID: 10424
	public bool disableBloom;

	// Token: 0x040028B9 RID: 10425
	public int disableFarAnimations = 5;

	// Token: 0x040028BA RID: 10426
	public bool disableOutlines;

	// Token: 0x040028BB RID: 10427
	public bool disablePostAliasing;

	// Token: 0x040028BC RID: 10428
	public bool enableShadows;

	// Token: 0x040028BD RID: 10429
	public int drawDistance;

	// Token: 0x040028BE RID: 10430
	public int drawDistanceLimit;

	// Token: 0x040028BF RID: 10431
	public bool fog;

	// Token: 0x040028C0 RID: 10432
	public int fpsIndex;

	// Token: 0x040028C1 RID: 10433
	public bool highPopulation;

	// Token: 0x040028C2 RID: 10434
	public int lowDetailStudents;

	// Token: 0x040028C3 RID: 10435
	public int particleCount;
}
