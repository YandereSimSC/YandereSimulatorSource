using System;
using System.Collections.Generic;

// Token: 0x02000009 RID: 9
[Serializable]
public class GameState
{
	// Token: 0x06000076 RID: 118 RVA: 0x000045E4 File Offset: 0x000027E4
	public GameState()
	{
		this.Health = 100f;
		this.Ratings = new Dictionary<DDRRating, int>();
		this.Ratings.Add(DDRRating.Early, 0);
		this.Ratings.Add(DDRRating.Good, 0);
		this.Ratings.Add(DDRRating.Great, 0);
		this.Ratings.Add(DDRRating.Miss, 0);
		this.Ratings.Add(DDRRating.Ok, 0);
		this.Ratings.Add(DDRRating.Perfect, 0);
	}

	// Token: 0x04000089 RID: 137
	public int Score;

	// Token: 0x0400008A RID: 138
	public float Health;

	// Token: 0x0400008B RID: 139
	public int LongestCombo;

	// Token: 0x0400008C RID: 140
	public int Combo;

	// Token: 0x0400008D RID: 141
	public Dictionary<DDRRating, int> Ratings;

	// Token: 0x0400008E RID: 142
	public DDRFinishStatus FinishStatus;
}
