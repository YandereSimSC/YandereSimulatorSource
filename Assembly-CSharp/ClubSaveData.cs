using System;

// Token: 0x020003AE RID: 942
[Serializable]
public class ClubSaveData
{
	// Token: 0x060019E9 RID: 6633 RVA: 0x000FCFAC File Offset: 0x000FB1AC
	public static ClubSaveData ReadFromGlobals()
	{
		ClubSaveData clubSaveData = new ClubSaveData();
		clubSaveData.club = ClubGlobals.Club;
		foreach (ClubType clubType in ClubGlobals.KeysOfClubClosed())
		{
			if (ClubGlobals.GetClubClosed(clubType))
			{
				clubSaveData.clubClosed.Add(clubType);
			}
		}
		foreach (ClubType clubType2 in ClubGlobals.KeysOfClubKicked())
		{
			if (ClubGlobals.GetClubKicked(clubType2))
			{
				clubSaveData.clubKicked.Add(clubType2);
			}
		}
		foreach (ClubType clubType3 in ClubGlobals.KeysOfQuitClub())
		{
			if (ClubGlobals.GetQuitClub(clubType3))
			{
				clubSaveData.quitClub.Add(clubType3);
			}
		}
		return clubSaveData;
	}

	// Token: 0x060019EA RID: 6634 RVA: 0x000FD058 File Offset: 0x000FB258
	public static void WriteToGlobals(ClubSaveData data)
	{
		ClubGlobals.Club = data.club;
		foreach (ClubType clubID in data.clubClosed)
		{
			ClubGlobals.SetClubClosed(clubID, true);
		}
		foreach (ClubType clubID2 in data.clubKicked)
		{
			ClubGlobals.SetClubKicked(clubID2, true);
		}
		foreach (ClubType clubID3 in data.quitClub)
		{
			ClubGlobals.SetQuitClub(clubID3, true);
		}
	}

	// Token: 0x0400288F RID: 10383
	public ClubType club;

	// Token: 0x04002890 RID: 10384
	public ClubTypeHashSet clubClosed = new ClubTypeHashSet();

	// Token: 0x04002891 RID: 10385
	public ClubTypeHashSet clubKicked = new ClubTypeHashSet();

	// Token: 0x04002892 RID: 10386
	public ClubTypeHashSet quitClub = new ClubTypeHashSet();
}
