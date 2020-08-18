using System;
using UnityEngine;

// Token: 0x020002B9 RID: 697
public static class Globals
{
	// Token: 0x0600146B RID: 5227 RVA: 0x000B4156 File Offset: 0x000B2356
	public static bool KeyExists(string key)
	{
		return PlayerPrefs.HasKey(key);
	}

	// Token: 0x0600146C RID: 5228 RVA: 0x000B4160 File Offset: 0x000B2360
	public static void DeleteAll()
	{
		int profile = GameGlobals.Profile;
		ClassGlobals.DeleteAll();
		ClubGlobals.DeleteAll();
		CollectibleGlobals.DeleteAll();
		ConversationGlobals.DeleteAll();
		DateGlobals.DeleteAll();
		DatingGlobals.DeleteAll();
		EventGlobals.DeleteAll();
		GameGlobals.DeleteAll();
		HomeGlobals.DeleteAll();
		MissionModeGlobals.DeleteAll();
		PlayerGlobals.DeleteAll();
		PoseModeGlobals.DeleteAll();
		SchemeGlobals.DeleteAll();
		SchoolGlobals.DeleteAll();
		SenpaiGlobals.DeleteAll();
		StudentGlobals.DeleteAll();
		TaskGlobals.DeleteAll();
		YanvaniaGlobals.DeleteAll();
		WeaponGlobals.DeleteAll();
		TutorialGlobals.DeleteAll();
		CounselorGlobals.DeleteAll();
		YancordGlobals.DeleteAll();
		CorkboardGlobals.DeleteAll();
		GameGlobals.Profile = profile;
		DateGlobals.Week = 1;
	}

	// Token: 0x0600146D RID: 5229 RVA: 0x000B41F0 File Offset: 0x000B23F0
	public static void Delete(string key)
	{
		PlayerPrefs.DeleteKey(key);
	}

	// Token: 0x0600146E RID: 5230 RVA: 0x000B41F8 File Offset: 0x000B23F8
	public static void DeleteCollection(string key, int[] usedKeys)
	{
		foreach (int num in usedKeys)
		{
			PlayerPrefs.DeleteKey(key + num.ToString());
		}
		KeysHelper.Delete(key);
	}

	// Token: 0x0600146F RID: 5231 RVA: 0x000B4234 File Offset: 0x000B2434
	public static void DeleteCollection(string key, string[] usedKeys)
	{
		foreach (string str in usedKeys)
		{
			PlayerPrefs.DeleteKey(key + str);
		}
		KeysHelper.Delete(key);
	}

	// Token: 0x06001470 RID: 5232 RVA: 0x000B4267 File Offset: 0x000B2467
	public static void Save()
	{
		PlayerPrefs.Save();
	}
}
