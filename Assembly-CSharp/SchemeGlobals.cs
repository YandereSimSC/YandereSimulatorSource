using System;
using UnityEngine;

// Token: 0x020002C9 RID: 713
public static class SchemeGlobals
{
	// Token: 0x170003E8 RID: 1000
	// (get) Token: 0x060015AF RID: 5551 RVA: 0x000B8354 File Offset: 0x000B6554
	// (set) Token: 0x060015B0 RID: 5552 RVA: 0x000B8374 File Offset: 0x000B6574
	public static int CurrentScheme
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_CurrentScheme");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CurrentScheme", value);
		}
	}

	// Token: 0x170003E9 RID: 1001
	// (get) Token: 0x060015B1 RID: 5553 RVA: 0x000B8395 File Offset: 0x000B6595
	// (set) Token: 0x060015B2 RID: 5554 RVA: 0x000B83B5 File Offset: 0x000B65B5
	public static bool DarkSecret
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_DarkSecret");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_DarkSecret", value);
		}
	}

	// Token: 0x060015B3 RID: 5555 RVA: 0x000B83D6 File Offset: 0x000B65D6
	public static int GetSchemePreviousStage(int schemeID)
	{
		return PlayerPrefs.GetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SchemePreviousStage_",
			schemeID.ToString()
		}));
	}

	// Token: 0x060015B4 RID: 5556 RVA: 0x000B8410 File Offset: 0x000B6610
	public static void SetSchemePreviousStage(int schemeID, int value)
	{
		string text = schemeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_SchemePreviousStage_", text);
		PlayerPrefs.SetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SchemePreviousStage_",
			text
		}), value);
	}

	// Token: 0x060015B5 RID: 5557 RVA: 0x000B8476 File Offset: 0x000B6676
	public static int[] KeysOfSchemePreviousStage()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_SchemePreviousStage_");
	}

	// Token: 0x060015B6 RID: 5558 RVA: 0x000B8496 File Offset: 0x000B6696
	public static int GetSchemeStage(int schemeID)
	{
		return PlayerPrefs.GetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SchemeStage_",
			schemeID.ToString()
		}));
	}

	// Token: 0x060015B7 RID: 5559 RVA: 0x000B84D0 File Offset: 0x000B66D0
	public static void SetSchemeStage(int schemeID, int value)
	{
		string text = schemeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_SchemeStage_", text);
		PlayerPrefs.SetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SchemeStage_",
			text
		}), value);
	}

	// Token: 0x060015B8 RID: 5560 RVA: 0x000B8536 File Offset: 0x000B6736
	public static int[] KeysOfSchemeStage()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_SchemeStage_");
	}

	// Token: 0x060015B9 RID: 5561 RVA: 0x000B8556 File Offset: 0x000B6756
	public static bool GetSchemeStatus(int schemeID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SchemeStatus_",
			schemeID.ToString()
		}));
	}

	// Token: 0x060015BA RID: 5562 RVA: 0x000B8590 File Offset: 0x000B6790
	public static void SetSchemeStatus(int schemeID, bool value)
	{
		string text = schemeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_SchemeStatus_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SchemeStatus_",
			text
		}), value);
	}

	// Token: 0x060015BB RID: 5563 RVA: 0x000B85F6 File Offset: 0x000B67F6
	public static int[] KeysOfSchemeStatus()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_SchemeStatus_");
	}

	// Token: 0x060015BC RID: 5564 RVA: 0x000B8616 File Offset: 0x000B6816
	public static bool GetSchemeUnlocked(int schemeID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SchemeUnlocked_",
			schemeID.ToString()
		}));
	}

	// Token: 0x060015BD RID: 5565 RVA: 0x000B8650 File Offset: 0x000B6850
	public static void SetSchemeUnlocked(int schemeID, bool value)
	{
		string text = schemeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_SchemeUnlocked_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SchemeUnlocked_",
			text
		}), value);
	}

	// Token: 0x060015BE RID: 5566 RVA: 0x000B86B6 File Offset: 0x000B68B6
	public static int[] KeysOfSchemeUnlocked()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_SchemeUnlocked_");
	}

	// Token: 0x060015BF RID: 5567 RVA: 0x000B86D6 File Offset: 0x000B68D6
	public static bool GetServicePurchased(int serviceID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_ServicePurchased_",
			serviceID.ToString()
		}));
	}

	// Token: 0x060015C0 RID: 5568 RVA: 0x000B8710 File Offset: 0x000B6910
	public static void SetServicePurchased(int serviceID, bool value)
	{
		string text = serviceID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_ServicePurchased_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_ServicePurchased_",
			text
		}), value);
	}

	// Token: 0x060015C1 RID: 5569 RVA: 0x000B8776 File Offset: 0x000B6976
	public static int[] KeysOfServicePurchased()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_ServicePurchased_");
	}

	// Token: 0x060015C2 RID: 5570 RVA: 0x000B8798 File Offset: 0x000B6998
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CurrentScheme");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_DarkSecret");
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_SchemePreviousStage_", SchemeGlobals.KeysOfSchemePreviousStage());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_SchemeStage_", SchemeGlobals.KeysOfSchemeStage());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_SchemeStatus_", SchemeGlobals.KeysOfSchemeStatus());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_SchemeUnlocked_", SchemeGlobals.KeysOfSchemeUnlocked());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_ServicePurchased_", SchemeGlobals.KeysOfServicePurchased());
	}

	// Token: 0x04001D7F RID: 7551
	private const string Str_CurrentScheme = "CurrentScheme";

	// Token: 0x04001D80 RID: 7552
	private const string Str_DarkSecret = "DarkSecret";

	// Token: 0x04001D81 RID: 7553
	private const string Str_SchemePreviousStage = "SchemePreviousStage_";

	// Token: 0x04001D82 RID: 7554
	private const string Str_SchemeStage = "SchemeStage_";

	// Token: 0x04001D83 RID: 7555
	private const string Str_SchemeStatus = "SchemeStatus_";

	// Token: 0x04001D84 RID: 7556
	private const string Str_SchemeUnlocked = "SchemeUnlocked_";

	// Token: 0x04001D85 RID: 7557
	private const string Str_ServicePurchased = "ServicePurchased_";
}
