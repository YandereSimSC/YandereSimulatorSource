using System;

// Token: 0x020003B4 RID: 948
[Serializable]
public class GameSaveData
{
	// Token: 0x060019FB RID: 6651 RVA: 0x000FD8CE File Offset: 0x000FBACE
	public static GameSaveData ReadFromGlobals()
	{
		return new GameSaveData
		{
			loveSick = GameGlobals.LoveSick,
			masksBanned = GameGlobals.MasksBanned,
			paranormal = GameGlobals.Paranormal
		};
	}

	// Token: 0x060019FC RID: 6652 RVA: 0x000FD8F6 File Offset: 0x000FBAF6
	public static void WriteToGlobals(GameSaveData data)
	{
		GameGlobals.LoveSick = data.loveSick;
		GameGlobals.MasksBanned = data.masksBanned;
		GameGlobals.Paranormal = data.paranormal;
	}

	// Token: 0x040028A9 RID: 10409
	public bool loveSick;

	// Token: 0x040028AA RID: 10410
	public bool masksBanned;

	// Token: 0x040028AB RID: 10411
	public bool paranormal;
}
