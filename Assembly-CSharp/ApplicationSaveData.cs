using System;

// Token: 0x020003AC RID: 940
[Serializable]
public class ApplicationSaveData
{
	// Token: 0x060019E3 RID: 6627 RVA: 0x000FCE1E File Offset: 0x000FB01E
	public static ApplicationSaveData ReadFromGlobals()
	{
		return new ApplicationSaveData
		{
			versionNumber = ApplicationGlobals.VersionNumber
		};
	}

	// Token: 0x060019E4 RID: 6628 RVA: 0x000FCE30 File Offset: 0x000FB030
	public static void WriteToGlobals(ApplicationSaveData data)
	{
		ApplicationGlobals.VersionNumber = data.versionNumber;
	}

	// Token: 0x0400287F RID: 10367
	public float versionNumber;
}
