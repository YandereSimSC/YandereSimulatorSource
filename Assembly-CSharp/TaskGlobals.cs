using System;
using UnityEngine;

// Token: 0x020002CD RID: 717
public static class TaskGlobals
{
	// Token: 0x06001659 RID: 5721 RVA: 0x000BAB8C File Offset: 0x000B8D8C
	public static bool GetGuitarPhoto(int photoID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_GuitarPhoto_",
			photoID.ToString()
		}));
	}

	// Token: 0x0600165A RID: 5722 RVA: 0x000BABC8 File Offset: 0x000B8DC8
	public static void SetGuitarPhoto(int photoID, bool value)
	{
		string text = photoID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_GuitarPhoto_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_GuitarPhoto_",
			text
		}), value);
	}

	// Token: 0x0600165B RID: 5723 RVA: 0x000BAC2E File Offset: 0x000B8E2E
	public static int[] KeysOfGuitarPhoto()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_GuitarPhoto_");
	}

	// Token: 0x0600165C RID: 5724 RVA: 0x000BAC4E File Offset: 0x000B8E4E
	public static bool GetKittenPhoto(int photoID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_KittenPhoto_",
			photoID.ToString()
		}));
	}

	// Token: 0x0600165D RID: 5725 RVA: 0x000BAC88 File Offset: 0x000B8E88
	public static void SetKittenPhoto(int photoID, bool value)
	{
		string text = photoID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_KittenPhoto_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_KittenPhoto_",
			text
		}), value);
	}

	// Token: 0x0600165E RID: 5726 RVA: 0x000BACEE File Offset: 0x000B8EEE
	public static int[] KeysOfKittenPhoto()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_KittenPhoto_");
	}

	// Token: 0x0600165F RID: 5727 RVA: 0x000BAD0E File Offset: 0x000B8F0E
	public static bool GetHorudaPhoto(int photoID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_HorudaPhoto_",
			photoID.ToString()
		}));
	}

	// Token: 0x06001660 RID: 5728 RVA: 0x000BAD48 File Offset: 0x000B8F48
	public static void SetHorudaPhoto(int photoID, bool value)
	{
		string text = photoID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_HorudaPhoto_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_HorudaPhoto_",
			text
		}), value);
	}

	// Token: 0x06001661 RID: 5729 RVA: 0x000BADAE File Offset: 0x000B8FAE
	public static int[] KeysOfHorudaPhoto()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_HorudaPhoto_");
	}

	// Token: 0x06001662 RID: 5730 RVA: 0x000BADCE File Offset: 0x000B8FCE
	public static int GetTaskStatus(int taskID)
	{
		return PlayerPrefs.GetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TaskStatus_",
			taskID.ToString()
		}));
	}

	// Token: 0x06001663 RID: 5731 RVA: 0x000BAE08 File Offset: 0x000B9008
	public static void SetTaskStatus(int taskID, int value)
	{
		string text = taskID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_TaskStatus_", text);
		PlayerPrefs.SetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TaskStatus_",
			text
		}), value);
	}

	// Token: 0x06001664 RID: 5732 RVA: 0x000BAE6E File Offset: 0x000B906E
	public static int[] KeysOfTaskStatus()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_TaskStatus_");
	}

	// Token: 0x06001665 RID: 5733 RVA: 0x000BAE90 File Offset: 0x000B9090
	public static void DeleteAll()
	{
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_GuitarPhoto_", TaskGlobals.KeysOfGuitarPhoto());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_KittenPhoto_", TaskGlobals.KeysOfKittenPhoto());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_HorudaPhoto_", TaskGlobals.KeysOfHorudaPhoto());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_TaskStatus_", TaskGlobals.KeysOfTaskStatus());
	}

	// Token: 0x04001DC4 RID: 7620
	private const string Str_GuitarPhoto = "GuitarPhoto_";

	// Token: 0x04001DC5 RID: 7621
	private const string Str_KittenPhoto = "KittenPhoto_";

	// Token: 0x04001DC6 RID: 7622
	private const string Str_HorudaPhoto = "HorudaPhoto_";

	// Token: 0x04001DC7 RID: 7623
	private const string Str_TaskStatus = "TaskStatus_";
}
