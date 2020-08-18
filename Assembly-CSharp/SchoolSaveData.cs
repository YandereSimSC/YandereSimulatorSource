using System;

// Token: 0x020003BC RID: 956
[Serializable]
public class SchoolSaveData
{
	// Token: 0x06001A13 RID: 6675 RVA: 0x000FE46C File Offset: 0x000FC66C
	public static SchoolSaveData ReadFromGlobals()
	{
		SchoolSaveData schoolSaveData = new SchoolSaveData();
		foreach (int num in SchoolGlobals.KeysOfDemonActive())
		{
			if (SchoolGlobals.GetDemonActive(num))
			{
				schoolSaveData.demonActive.Add(num);
			}
		}
		foreach (int num2 in SchoolGlobals.KeysOfGardenGraveOccupied())
		{
			if (SchoolGlobals.GetGardenGraveOccupied(num2))
			{
				schoolSaveData.gardenGraveOccupied.Add(num2);
			}
		}
		schoolSaveData.kidnapVictim = SchoolGlobals.KidnapVictim;
		schoolSaveData.population = SchoolGlobals.Population;
		schoolSaveData.roofFence = SchoolGlobals.RoofFence;
		schoolSaveData.schoolAtmosphere = SchoolGlobals.SchoolAtmosphere;
		schoolSaveData.schoolAtmosphereSet = SchoolGlobals.SchoolAtmosphereSet;
		schoolSaveData.scp = SchoolGlobals.SCP;
		return schoolSaveData;
	}

	// Token: 0x06001A14 RID: 6676 RVA: 0x000FE520 File Offset: 0x000FC720
	public static void WriteToGlobals(SchoolSaveData data)
	{
		foreach (int demonID in data.demonActive)
		{
			SchoolGlobals.SetDemonActive(demonID, true);
		}
		foreach (int graveID in data.gardenGraveOccupied)
		{
			SchoolGlobals.SetGardenGraveOccupied(graveID, true);
		}
		SchoolGlobals.KidnapVictim = data.kidnapVictim;
		SchoolGlobals.Population = data.population;
		SchoolGlobals.RoofFence = data.roofFence;
		SchoolGlobals.SchoolAtmosphere = data.schoolAtmosphere;
		SchoolGlobals.SchoolAtmosphereSet = data.schoolAtmosphereSet;
		SchoolGlobals.SCP = data.scp;
	}

	// Token: 0x040028E6 RID: 10470
	public IntHashSet demonActive = new IntHashSet();

	// Token: 0x040028E7 RID: 10471
	public IntHashSet gardenGraveOccupied = new IntHashSet();

	// Token: 0x040028E8 RID: 10472
	public int kidnapVictim;

	// Token: 0x040028E9 RID: 10473
	public int population;

	// Token: 0x040028EA RID: 10474
	public bool roofFence;

	// Token: 0x040028EB RID: 10475
	public float schoolAtmosphere;

	// Token: 0x040028EC RID: 10476
	public bool schoolAtmosphereSet;

	// Token: 0x040028ED RID: 10477
	public bool scp;
}
