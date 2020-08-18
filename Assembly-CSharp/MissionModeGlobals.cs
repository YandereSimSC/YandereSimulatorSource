using System;
using UnityEngine;

// Token: 0x020002C4 RID: 708
public static class MissionModeGlobals
{
	// Token: 0x06001521 RID: 5409 RVA: 0x000B699D File Offset: 0x000B4B9D
	public static int GetMissionCondition(int id)
	{
		return PlayerPrefs.GetInt("MissionCondition_" + id.ToString());
	}

	// Token: 0x06001522 RID: 5410 RVA: 0x000B69B8 File Offset: 0x000B4BB8
	public static void SetMissionCondition(int id, int value)
	{
		string text = id.ToString();
		KeysHelper.AddIfMissing("MissionCondition_", text);
		PlayerPrefs.SetInt("MissionCondition_" + text, value);
	}

	// Token: 0x06001523 RID: 5411 RVA: 0x000B69E9 File Offset: 0x000B4BE9
	public static int[] KeysOfMissionCondition()
	{
		return KeysHelper.GetIntegerKeys("MissionCondition_");
	}

	// Token: 0x170003B2 RID: 946
	// (get) Token: 0x06001524 RID: 5412 RVA: 0x000B69F5 File Offset: 0x000B4BF5
	// (set) Token: 0x06001525 RID: 5413 RVA: 0x000B6A01 File Offset: 0x000B4C01
	public static int MissionDifficulty
	{
		get
		{
			return PlayerPrefs.GetInt("MissionDifficulty");
		}
		set
		{
			PlayerPrefs.SetInt("MissionDifficulty", value);
		}
	}

	// Token: 0x170003B3 RID: 947
	// (get) Token: 0x06001526 RID: 5414 RVA: 0x000B6A0E File Offset: 0x000B4C0E
	// (set) Token: 0x06001527 RID: 5415 RVA: 0x000B6A1A File Offset: 0x000B4C1A
	public static bool MissionMode
	{
		get
		{
			return GlobalsHelper.GetBool("MissionMode");
		}
		set
		{
			GlobalsHelper.SetBool("MissionMode", value);
		}
	}

	// Token: 0x170003B4 RID: 948
	// (get) Token: 0x06001528 RID: 5416 RVA: 0x000B6A27 File Offset: 0x000B4C27
	// (set) Token: 0x06001529 RID: 5417 RVA: 0x000B6A33 File Offset: 0x000B4C33
	public static bool MultiMission
	{
		get
		{
			return GlobalsHelper.GetBool("MultiMission");
		}
		set
		{
			GlobalsHelper.SetBool("MultiMission", value);
		}
	}

	// Token: 0x170003B5 RID: 949
	// (get) Token: 0x0600152A RID: 5418 RVA: 0x000B6A40 File Offset: 0x000B4C40
	// (set) Token: 0x0600152B RID: 5419 RVA: 0x000B6A4C File Offset: 0x000B4C4C
	public static int MissionRequiredClothing
	{
		get
		{
			return PlayerPrefs.GetInt("MissionRequiredClothing");
		}
		set
		{
			PlayerPrefs.SetInt("MissionRequiredClothing", value);
		}
	}

	// Token: 0x170003B6 RID: 950
	// (get) Token: 0x0600152C RID: 5420 RVA: 0x000B6A59 File Offset: 0x000B4C59
	// (set) Token: 0x0600152D RID: 5421 RVA: 0x000B6A65 File Offset: 0x000B4C65
	public static int MissionRequiredDisposal
	{
		get
		{
			return PlayerPrefs.GetInt("MissionRequiredDisposal");
		}
		set
		{
			PlayerPrefs.SetInt("MissionRequiredDisposal", value);
		}
	}

	// Token: 0x170003B7 RID: 951
	// (get) Token: 0x0600152E RID: 5422 RVA: 0x000B6A72 File Offset: 0x000B4C72
	// (set) Token: 0x0600152F RID: 5423 RVA: 0x000B6A7E File Offset: 0x000B4C7E
	public static int MissionRequiredWeapon
	{
		get
		{
			return PlayerPrefs.GetInt("MissionRequiredWeapon");
		}
		set
		{
			PlayerPrefs.SetInt("MissionRequiredWeapon", value);
		}
	}

	// Token: 0x170003B8 RID: 952
	// (get) Token: 0x06001530 RID: 5424 RVA: 0x000B6A8B File Offset: 0x000B4C8B
	// (set) Token: 0x06001531 RID: 5425 RVA: 0x000B6A97 File Offset: 0x000B4C97
	public static int MissionTarget
	{
		get
		{
			return PlayerPrefs.GetInt("MissionTarget");
		}
		set
		{
			PlayerPrefs.SetInt("MissionTarget", value);
		}
	}

	// Token: 0x170003B9 RID: 953
	// (get) Token: 0x06001532 RID: 5426 RVA: 0x000B6AA4 File Offset: 0x000B4CA4
	// (set) Token: 0x06001533 RID: 5427 RVA: 0x000B6AB0 File Offset: 0x000B4CB0
	public static string MissionTargetName
	{
		get
		{
			return PlayerPrefs.GetString("MissionTargetName");
		}
		set
		{
			PlayerPrefs.SetString("MissionTargetName", value);
		}
	}

	// Token: 0x170003BA RID: 954
	// (get) Token: 0x06001534 RID: 5428 RVA: 0x000B6ABD File Offset: 0x000B4CBD
	// (set) Token: 0x06001535 RID: 5429 RVA: 0x000B6AC9 File Offset: 0x000B4CC9
	public static int NemesisDifficulty
	{
		get
		{
			return PlayerPrefs.GetInt("NemesisDifficulty");
		}
		set
		{
			PlayerPrefs.SetInt("NemesisDifficulty", value);
		}
	}

	// Token: 0x170003BB RID: 955
	// (get) Token: 0x06001536 RID: 5430 RVA: 0x000B6AD6 File Offset: 0x000B4CD6
	// (set) Token: 0x06001537 RID: 5431 RVA: 0x000B6AE2 File Offset: 0x000B4CE2
	public static bool NemesisAggression
	{
		get
		{
			return GlobalsHelper.GetBool("NemesisAggression");
		}
		set
		{
			GlobalsHelper.SetBool("NemesisAggression", value);
		}
	}

	// Token: 0x06001538 RID: 5432 RVA: 0x000B6AF0 File Offset: 0x000B4CF0
	public static void DeleteAll()
	{
		Globals.DeleteCollection("MissionCondition_", MissionModeGlobals.KeysOfMissionCondition());
		Globals.Delete("MissionDifficulty");
		Globals.Delete("MissionMode");
		Globals.Delete("MissionRequiredClothing");
		Globals.Delete("MissionRequiredDisposal");
		Globals.Delete("MissionRequiredWeapon");
		Globals.Delete("MissionTarget");
		Globals.Delete("MissionTargetName");
		Globals.Delete("NemesisDifficulty");
		Globals.Delete("NemesisAggression");
		Globals.Delete("MultiMission");
	}

	// Token: 0x04001D3F RID: 7487
	private const string Str_MissionCondition = "MissionCondition_";

	// Token: 0x04001D40 RID: 7488
	private const string Str_MissionDifficulty = "MissionDifficulty";

	// Token: 0x04001D41 RID: 7489
	private const string Str_MissionMode = "MissionMode";

	// Token: 0x04001D42 RID: 7490
	private const string Str_MissionRequiredClothing = "MissionRequiredClothing";

	// Token: 0x04001D43 RID: 7491
	private const string Str_MissionRequiredDisposal = "MissionRequiredDisposal";

	// Token: 0x04001D44 RID: 7492
	private const string Str_MissionRequiredWeapon = "MissionRequiredWeapon";

	// Token: 0x04001D45 RID: 7493
	private const string Str_MissionTarget = "MissionTarget";

	// Token: 0x04001D46 RID: 7494
	private const string Str_MissionTargetName = "MissionTargetName";

	// Token: 0x04001D47 RID: 7495
	private const string Str_NemesisDifficulty = "NemesisDifficulty";

	// Token: 0x04001D48 RID: 7496
	private const string Str_NemesisAggression = "NemesisAggression";

	// Token: 0x04001D49 RID: 7497
	private const string Str_MultiMission = "MultiMission";
}
