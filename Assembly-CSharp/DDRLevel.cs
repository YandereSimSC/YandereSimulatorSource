using System;
using UnityEngine;

// Token: 0x02000003 RID: 3
public class DDRLevel : ScriptableObject
{
	// Token: 0x0400003B RID: 59
	public string LevelName;

	// Token: 0x0400003C RID: 60
	public AudioClip Song;

	// Token: 0x0400003D RID: 61
	public Sprite LevelIcon;

	// Token: 0x0400003E RID: 62
	public DDRTrack[] Tracks;

	// Token: 0x0400003F RID: 63
	[Header("Points per score")]
	public int PerfectScorePoints;

	// Token: 0x04000040 RID: 64
	public int GreatScorePoints;

	// Token: 0x04000041 RID: 65
	public int GoodScorePoints;

	// Token: 0x04000042 RID: 66
	public int OkScorePoints;

	// Token: 0x04000043 RID: 67
	public int EarlyScorePoints;

	// Token: 0x04000044 RID: 68
	public int MissScorePoints;
}
