using System;
using UnityEngine;

// Token: 0x020002BA RID: 698
public static class ApplicationGlobals
{
	// Token: 0x1700037C RID: 892
	// (get) Token: 0x06001471 RID: 5233 RVA: 0x000B426E File Offset: 0x000B246E
	// (set) Token: 0x06001472 RID: 5234 RVA: 0x000B428E File Offset: 0x000B248E
	public static float VersionNumber
	{
		get
		{
			return PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_VersionNumber");
		}
		set
		{
			PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_VersionNumber", value);
		}
	}

	// Token: 0x06001473 RID: 5235 RVA: 0x000B42AF File Offset: 0x000B24AF
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_VersionNumber");
	}

	// Token: 0x04001CF5 RID: 7413
	private const string Str_VersionNumber = "VersionNumber";
}
