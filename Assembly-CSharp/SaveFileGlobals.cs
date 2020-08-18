using System;
using UnityEngine;

// Token: 0x020002C8 RID: 712
public static class SaveFileGlobals
{
	// Token: 0x170003E7 RID: 999
	// (get) Token: 0x060015AC RID: 5548 RVA: 0x000B82F3 File Offset: 0x000B64F3
	// (set) Token: 0x060015AD RID: 5549 RVA: 0x000B8313 File Offset: 0x000B6513
	public static int CurrentSaveFile
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_CurrentSaveFile");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CurrentSaveFile", value);
		}
	}

	// Token: 0x060015AE RID: 5550 RVA: 0x000B8334 File Offset: 0x000B6534
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CurrentSaveFile");
	}

	// Token: 0x04001D7E RID: 7550
	private const string Str_CurrentSaveFile = "CurrentSaveFile";
}
