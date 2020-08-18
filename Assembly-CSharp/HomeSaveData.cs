using System;

// Token: 0x020003B5 RID: 949
[Serializable]
public class HomeSaveData
{
	// Token: 0x060019FE RID: 6654 RVA: 0x000FD919 File Offset: 0x000FBB19
	public static HomeSaveData ReadFromGlobals()
	{
		return new HomeSaveData
		{
			lateForSchool = HomeGlobals.LateForSchool,
			night = HomeGlobals.Night,
			startInBasement = HomeGlobals.StartInBasement
		};
	}

	// Token: 0x060019FF RID: 6655 RVA: 0x000FD941 File Offset: 0x000FBB41
	public static void WriteToGlobals(HomeSaveData data)
	{
		HomeGlobals.LateForSchool = data.lateForSchool;
		HomeGlobals.Night = data.night;
		HomeGlobals.StartInBasement = data.startInBasement;
	}

	// Token: 0x040028AC RID: 10412
	public bool lateForSchool;

	// Token: 0x040028AD RID: 10413
	public bool night;

	// Token: 0x040028AE RID: 10414
	public bool startInBasement;
}
