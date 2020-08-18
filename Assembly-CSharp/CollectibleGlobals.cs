using System;
using UnityEngine;

// Token: 0x020002BD RID: 701
public static class CollectibleGlobals
{
	// Token: 0x0600149F RID: 5279 RVA: 0x000B4C9C File Offset: 0x000B2E9C
	public static bool GetHeadmasterTapeCollected(int tapeID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_HeadmasterTapeCollected_",
			tapeID.ToString()
		}));
	}

	// Token: 0x060014A0 RID: 5280 RVA: 0x000B4CD8 File Offset: 0x000B2ED8
	public static void SetHeadmasterTapeCollected(int tapeID, bool value)
	{
		string text = tapeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_HeadmasterTapeCollected_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_HeadmasterTapeCollected_",
			text
		}), value);
	}

	// Token: 0x060014A1 RID: 5281 RVA: 0x000B4D3E File Offset: 0x000B2F3E
	public static bool GetHeadmasterTapeListened(int tapeID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_HeadmasterTapeListened_",
			tapeID.ToString()
		}));
	}

	// Token: 0x060014A2 RID: 5282 RVA: 0x000B4D78 File Offset: 0x000B2F78
	public static void SetHeadmasterTapeListened(int tapeID, bool value)
	{
		string text = tapeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_HeadmasterTapeListened_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_HeadmasterTapeListened_",
			text
		}), value);
	}

	// Token: 0x060014A3 RID: 5283 RVA: 0x000B4DDE File Offset: 0x000B2FDE
	public static bool GetBasementTapeCollected(int tapeID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_BasementTapeCollected_",
			tapeID.ToString()
		}));
	}

	// Token: 0x060014A4 RID: 5284 RVA: 0x000B4E18 File Offset: 0x000B3018
	public static void SetBasementTapeCollected(int tapeID, bool value)
	{
		string text = tapeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_BasementTapeCollected_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_BasementTapeCollected_",
			text
		}), value);
	}

	// Token: 0x060014A5 RID: 5285 RVA: 0x000B4E7E File Offset: 0x000B307E
	public static int[] KeysOfBasementTapeCollected()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_BasementTapeCollected_");
	}

	// Token: 0x060014A6 RID: 5286 RVA: 0x000B4E9E File Offset: 0x000B309E
	public static bool GetBasementTapeListened(int tapeID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_BasementTapeListened_",
			tapeID.ToString()
		}));
	}

	// Token: 0x060014A7 RID: 5287 RVA: 0x000B4ED8 File Offset: 0x000B30D8
	public static void SetBasementTapeListened(int tapeID, bool value)
	{
		string text = tapeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_BasementTapeListened_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_BasementTapeListened_",
			text
		}), value);
	}

	// Token: 0x060014A8 RID: 5288 RVA: 0x000B4F3E File Offset: 0x000B313E
	public static int[] KeysOfBasementTapeListened()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_BasementTapeListened_");
	}

	// Token: 0x060014A9 RID: 5289 RVA: 0x000B4F5E File Offset: 0x000B315E
	public static bool GetMangaCollected(int mangaID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_MangaCollected_",
			mangaID.ToString()
		}));
	}

	// Token: 0x060014AA RID: 5290 RVA: 0x000B4F98 File Offset: 0x000B3198
	public static void SetMangaCollected(int mangaID, bool value)
	{
		string text = mangaID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_MangaCollected_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_MangaCollected_",
			text
		}), value);
	}

	// Token: 0x060014AB RID: 5291 RVA: 0x000B4FFE File Offset: 0x000B31FE
	public static bool GetGiftPurchased(int giftID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_GiftPurchased_",
			giftID.ToString()
		}));
	}

	// Token: 0x060014AC RID: 5292 RVA: 0x000B5038 File Offset: 0x000B3238
	public static void SetGiftPurchased(int giftID, bool value)
	{
		string text = giftID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_GiftPurchased_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_GiftPurchased_",
			text
		}), value);
	}

	// Token: 0x060014AD RID: 5293 RVA: 0x000B509E File Offset: 0x000B329E
	public static bool GetGiftGiven(int giftID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_GiftGiven_",
			giftID.ToString()
		}));
	}

	// Token: 0x060014AE RID: 5294 RVA: 0x000B50D8 File Offset: 0x000B32D8
	public static void SetGiftGiven(int giftID, bool value)
	{
		string text = giftID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_GiftGiven_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_GiftGiven_",
			text
		}), value);
	}

	// Token: 0x1700038D RID: 909
	// (get) Token: 0x060014AF RID: 5295 RVA: 0x000B513E File Offset: 0x000B333E
	// (set) Token: 0x060014B0 RID: 5296 RVA: 0x000B515E File Offset: 0x000B335E
	public static int MatchmakingGifts
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_MatchmakingGifts");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_MatchmakingGifts", value);
		}
	}

	// Token: 0x1700038E RID: 910
	// (get) Token: 0x060014B1 RID: 5297 RVA: 0x000B517F File Offset: 0x000B337F
	// (set) Token: 0x060014B2 RID: 5298 RVA: 0x000B519F File Offset: 0x000B339F
	public static int SenpaiGifts
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_SenpaiGifts");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_SenpaiGifts", value);
		}
	}

	// Token: 0x060014B3 RID: 5299 RVA: 0x000B51C0 File Offset: 0x000B33C0
	public static bool GetPantyPurchased(int giftID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_PantyPurchased_",
			giftID.ToString()
		}));
	}

	// Token: 0x060014B4 RID: 5300 RVA: 0x000B51FC File Offset: 0x000B33FC
	public static void SetPantyPurchased(int pantyID, bool value)
	{
		string text = pantyID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_PantyPurchased_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_PantyPurchased_",
			text
		}), value);
	}

	// Token: 0x060014B5 RID: 5301 RVA: 0x000B5262 File Offset: 0x000B3462
	public static int[] KeysOfMangaCollected()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_MangaCollected_");
	}

	// Token: 0x060014B6 RID: 5302 RVA: 0x000B5282 File Offset: 0x000B3482
	public static int[] KeysOfGiftPurchased()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_GiftPurchased_");
	}

	// Token: 0x060014B7 RID: 5303 RVA: 0x000B52A2 File Offset: 0x000B34A2
	public static int[] KeysOfGiftGiven()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_GiftGiven_");
	}

	// Token: 0x060014B8 RID: 5304 RVA: 0x000B52C2 File Offset: 0x000B34C2
	public static int[] KeysOfPantyPurchased()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_PantyPurchased_");
	}

	// Token: 0x060014B9 RID: 5305 RVA: 0x000B52E2 File Offset: 0x000B34E2
	public static bool GetTapeCollected(int tapeID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TapeCollected_",
			tapeID.ToString()
		}));
	}

	// Token: 0x060014BA RID: 5306 RVA: 0x000B531C File Offset: 0x000B351C
	public static void SetTapeCollected(int tapeID, bool value)
	{
		string text = tapeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_TapeCollected_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TapeCollected_",
			text
		}), value);
	}

	// Token: 0x060014BB RID: 5307 RVA: 0x000B5382 File Offset: 0x000B3582
	public static int[] KeysOfTapeCollected()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_TapeCollected_");
	}

	// Token: 0x060014BC RID: 5308 RVA: 0x000B53A2 File Offset: 0x000B35A2
	public static bool GetTapeListened(int tapeID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TapeListened_",
			tapeID.ToString()
		}));
	}

	// Token: 0x060014BD RID: 5309 RVA: 0x000B53DC File Offset: 0x000B35DC
	public static void SetTapeListened(int tapeID, bool value)
	{
		string text = tapeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_TapeListened_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TapeListened_",
			text
		}), value);
	}

	// Token: 0x060014BE RID: 5310 RVA: 0x000B5442 File Offset: 0x000B3642
	public static int[] KeysOfTapeListened()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_TapeListened_");
	}

	// Token: 0x060014BF RID: 5311 RVA: 0x000B5464 File Offset: 0x000B3664
	public static void DeleteAll()
	{
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_BasementTapeCollected_", CollectibleGlobals.KeysOfBasementTapeCollected());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_BasementTapeListened_", CollectibleGlobals.KeysOfBasementTapeListened());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_MangaCollected_", CollectibleGlobals.KeysOfMangaCollected());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_PantyPurchased_", CollectibleGlobals.KeysOfPantyPurchased());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_GiftPurchased_", CollectibleGlobals.KeysOfGiftPurchased());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_GiftGiven_", CollectibleGlobals.KeysOfGiftGiven());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_TapeCollected_", CollectibleGlobals.KeysOfTapeCollected());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_TapeListened_", CollectibleGlobals.KeysOfTapeListened());
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MatchmakingGifts");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SenpaiGifts");
	}

	// Token: 0x04001D09 RID: 7433
	private const string Str_HeadmasterTapeCollected = "HeadmasterTapeCollected_";

	// Token: 0x04001D0A RID: 7434
	private const string Str_HeadmasterTapeListened = "HeadmasterTapeListened_";

	// Token: 0x04001D0B RID: 7435
	private const string Str_BasementTapeCollected = "BasementTapeCollected_";

	// Token: 0x04001D0C RID: 7436
	private const string Str_BasementTapeListened = "BasementTapeListened_";

	// Token: 0x04001D0D RID: 7437
	private const string Str_MangaCollected = "MangaCollected_";

	// Token: 0x04001D0E RID: 7438
	private const string Str_GiftPurchased = "GiftPurchased_";

	// Token: 0x04001D0F RID: 7439
	private const string Str_GiftGiven = "GiftGiven_";

	// Token: 0x04001D10 RID: 7440
	private const string Str_MatchmakingGifts = "MatchmakingGifts";

	// Token: 0x04001D11 RID: 7441
	private const string Str_SenpaiGifts = "SenpaiGifts";

	// Token: 0x04001D12 RID: 7442
	private const string Str_PantyPurchased = "PantyPurchased_";

	// Token: 0x04001D13 RID: 7443
	private const string Str_TapeCollected = "TapeCollected_";

	// Token: 0x04001D14 RID: 7444
	private const string Str_TapeListened = "TapeListened_";
}
