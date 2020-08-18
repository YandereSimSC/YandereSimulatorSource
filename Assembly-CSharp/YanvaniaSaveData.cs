using System;

// Token: 0x020003C0 RID: 960
[Serializable]
public class YanvaniaSaveData
{
	// Token: 0x06001A1F RID: 6687 RVA: 0x000FF374 File Offset: 0x000FD574
	public static YanvaniaSaveData ReadFromGlobals()
	{
		return new YanvaniaSaveData
		{
			draculaDefeated = YanvaniaGlobals.DraculaDefeated,
			midoriEasterEgg = YanvaniaGlobals.MidoriEasterEgg
		};
	}

	// Token: 0x06001A20 RID: 6688 RVA: 0x000FF391 File Offset: 0x000FD591
	public static void WriteToGlobals(YanvaniaSaveData data)
	{
		YanvaniaGlobals.DraculaDefeated = data.draculaDefeated;
		YanvaniaGlobals.MidoriEasterEgg = data.midoriEasterEgg;
	}

	// Token: 0x04002917 RID: 10519
	public bool draculaDefeated;

	// Token: 0x04002918 RID: 10520
	public bool midoriEasterEgg;
}
