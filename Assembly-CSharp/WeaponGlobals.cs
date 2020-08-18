using System;
using UnityEngine;

// Token: 0x020002CF RID: 719
public static class WeaponGlobals
{
	// Token: 0x0600166B RID: 5739 RVA: 0x000BAFE9 File Offset: 0x000B91E9
	public static int GetWeaponStatus(int weaponID)
	{
		return PlayerPrefs.GetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_WeaponStatus_",
			weaponID.ToString()
		}));
	}

	// Token: 0x0600166C RID: 5740 RVA: 0x000BB024 File Offset: 0x000B9224
	public static void SetWeaponStatus(int weaponID, int value)
	{
		string text = weaponID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_WeaponStatus_", text);
		PlayerPrefs.SetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_WeaponStatus_",
			text
		}), value);
	}

	// Token: 0x0600166D RID: 5741 RVA: 0x000BB08A File Offset: 0x000B928A
	public static int[] KeysOfWeaponStatus()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_WeaponStatus_");
	}

	// Token: 0x0600166E RID: 5742 RVA: 0x000BB0AA File Offset: 0x000B92AA
	public static void DeleteAll()
	{
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_WeaponStatus_", WeaponGlobals.KeysOfWeaponStatus());
	}

	// Token: 0x04001DCA RID: 7626
	private const string Str_WeaponStatus = "WeaponStatus_";
}
