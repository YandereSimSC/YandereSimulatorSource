using System;
using UnityEngine;

// Token: 0x020002C0 RID: 704
public static class DatingGlobals
{
	// Token: 0x17000393 RID: 915
	// (get) Token: 0x060014D0 RID: 5328 RVA: 0x000B5A0D File Offset: 0x000B3C0D
	// (set) Token: 0x060014D1 RID: 5329 RVA: 0x000B5A2D File Offset: 0x000B3C2D
	public static float Affection
	{
		get
		{
			return PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_Affection");
		}
		set
		{
			PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_Affection", value);
		}
	}

	// Token: 0x17000394 RID: 916
	// (get) Token: 0x060014D2 RID: 5330 RVA: 0x000B5A4E File Offset: 0x000B3C4E
	// (set) Token: 0x060014D3 RID: 5331 RVA: 0x000B5A6E File Offset: 0x000B3C6E
	public static float AffectionLevel
	{
		get
		{
			return PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_AffectionLevel");
		}
		set
		{
			PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_AffectionLevel", value);
		}
	}

	// Token: 0x060014D4 RID: 5332 RVA: 0x000B5A8F File Offset: 0x000B3C8F
	public static bool GetComplimentGiven(int complimentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_ComplimentGiven_",
			complimentID.ToString()
		}));
	}

	// Token: 0x060014D5 RID: 5333 RVA: 0x000B5AC8 File Offset: 0x000B3CC8
	public static void SetComplimentGiven(int complimentID, bool value)
	{
		string text = complimentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_ComplimentGiven_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_ComplimentGiven_",
			text
		}), value);
	}

	// Token: 0x060014D6 RID: 5334 RVA: 0x000B5B2E File Offset: 0x000B3D2E
	public static int[] KeysOfComplimentGiven()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_ComplimentGiven_");
	}

	// Token: 0x060014D7 RID: 5335 RVA: 0x000B5B4E File Offset: 0x000B3D4E
	public static bool GetSuitorCheck(int checkID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SuitorCheck_",
			checkID.ToString()
		}));
	}

	// Token: 0x060014D8 RID: 5336 RVA: 0x000B5B88 File Offset: 0x000B3D88
	public static void SetSuitorCheck(int checkID, bool value)
	{
		string text = checkID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_SuitorCheck_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SuitorCheck_",
			text
		}), value);
	}

	// Token: 0x060014D9 RID: 5337 RVA: 0x000B5BEE File Offset: 0x000B3DEE
	public static int[] KeysOfSuitorCheck()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_SuitorCheck_");
	}

	// Token: 0x17000395 RID: 917
	// (get) Token: 0x060014DA RID: 5338 RVA: 0x000B5C0E File Offset: 0x000B3E0E
	// (set) Token: 0x060014DB RID: 5339 RVA: 0x000B5C2E File Offset: 0x000B3E2E
	public static int SuitorProgress
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_SuitorProgress");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_SuitorProgress", value);
		}
	}

	// Token: 0x060014DC RID: 5340 RVA: 0x000B5C4F File Offset: 0x000B3E4F
	public static int GetSuitorTrait(int traitID)
	{
		return PlayerPrefs.GetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SuitorTrait_",
			traitID.ToString()
		}));
	}

	// Token: 0x060014DD RID: 5341 RVA: 0x000B5C88 File Offset: 0x000B3E88
	public static void SetSuitorTrait(int traitID, int value)
	{
		string text = traitID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_SuitorTrait_", text);
		PlayerPrefs.SetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SuitorTrait_",
			text
		}), value);
	}

	// Token: 0x060014DE RID: 5342 RVA: 0x000B5CEE File Offset: 0x000B3EEE
	public static int[] KeysOfSuitorTrait()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_SuitorTrait_");
	}

	// Token: 0x060014DF RID: 5343 RVA: 0x000B5D0E File Offset: 0x000B3F0E
	public static bool GetTopicDiscussed(int topicID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TopicDiscussed_",
			topicID.ToString()
		}));
	}

	// Token: 0x060014E0 RID: 5344 RVA: 0x000B5D48 File Offset: 0x000B3F48
	public static void SetTopicDiscussed(int topicID, bool value)
	{
		string text = topicID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_TopicDiscussed_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TopicDiscussed_",
			text
		}), value);
	}

	// Token: 0x060014E1 RID: 5345 RVA: 0x000B5DAE File Offset: 0x000B3FAE
	public static int[] KeysOfTopicDiscussed()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_TopicDiscussed_");
	}

	// Token: 0x060014E2 RID: 5346 RVA: 0x000B5DCE File Offset: 0x000B3FCE
	public static int GetTraitDemonstrated(int traitID)
	{
		return PlayerPrefs.GetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TraitDemonstrated_",
			traitID.ToString()
		}));
	}

	// Token: 0x060014E3 RID: 5347 RVA: 0x000B5E08 File Offset: 0x000B4008
	public static void SetTraitDemonstrated(int traitID, int value)
	{
		string text = traitID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_TraitDemonstrated_", text);
		PlayerPrefs.SetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TraitDemonstrated_",
			text
		}), value);
	}

	// Token: 0x060014E4 RID: 5348 RVA: 0x000B5E6E File Offset: 0x000B406E
	public static int[] KeysOfTraitDemonstrated()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_TraitDemonstrated_");
	}

	// Token: 0x17000396 RID: 918
	// (get) Token: 0x060014E5 RID: 5349 RVA: 0x000B5E8E File Offset: 0x000B408E
	// (set) Token: 0x060014E6 RID: 5350 RVA: 0x000B5EAE File Offset: 0x000B40AE
	public static int RivalSabotaged
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_RivalSabotaged");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_RivalSabotaged", value);
		}
	}

	// Token: 0x060014E7 RID: 5351 RVA: 0x000B5ED0 File Offset: 0x000B40D0
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Affection");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_AffectionLevel");
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_ComplimentGiven_", DatingGlobals.KeysOfComplimentGiven());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_SuitorCheck_", DatingGlobals.KeysOfSuitorCheck());
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SuitorProgress");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_RivalSabotaged");
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_SuitorTrait_", DatingGlobals.KeysOfSuitorTrait());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_TopicDiscussed_", DatingGlobals.KeysOfTopicDiscussed());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_TraitDemonstrated_", DatingGlobals.KeysOfTraitDemonstrated());
	}

	// Token: 0x04001D1B RID: 7451
	private const string Str_Affection = "Affection";

	// Token: 0x04001D1C RID: 7452
	private const string Str_AffectionLevel = "AffectionLevel";

	// Token: 0x04001D1D RID: 7453
	private const string Str_ComplimentGiven = "ComplimentGiven_";

	// Token: 0x04001D1E RID: 7454
	private const string Str_SuitorCheck = "SuitorCheck_";

	// Token: 0x04001D1F RID: 7455
	private const string Str_SuitorProgress = "SuitorProgress";

	// Token: 0x04001D20 RID: 7456
	private const string Str_SuitorTrait = "SuitorTrait_";

	// Token: 0x04001D21 RID: 7457
	private const string Str_TopicDiscussed = "TopicDiscussed_";

	// Token: 0x04001D22 RID: 7458
	private const string Str_TraitDemonstrated = "TraitDemonstrated_";

	// Token: 0x04001D23 RID: 7459
	private const string Str_RivalSabotaged = "RivalSabotaged";
}
