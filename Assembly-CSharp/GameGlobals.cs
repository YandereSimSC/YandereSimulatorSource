using System;
using UnityEngine;

// Token: 0x020002C2 RID: 706
public static class GameGlobals
{
	// Token: 0x1700039E RID: 926
	// (get) Token: 0x060014F7 RID: 5367 RVA: 0x000B62AB File Offset: 0x000B44AB
	// (set) Token: 0x060014F8 RID: 5368 RVA: 0x000B62B7 File Offset: 0x000B44B7
	public static int Profile
	{
		get
		{
			return PlayerPrefs.GetInt("Profile");
		}
		set
		{
			PlayerPrefs.SetInt("Profile", value);
		}
	}

	// Token: 0x1700039F RID: 927
	// (get) Token: 0x060014F9 RID: 5369 RVA: 0x000B62C4 File Offset: 0x000B44C4
	// (set) Token: 0x060014FA RID: 5370 RVA: 0x000B62E4 File Offset: 0x000B44E4
	public static bool LoveSick
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_LoveSick");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_LoveSick", value);
		}
	}

	// Token: 0x170003A0 RID: 928
	// (get) Token: 0x060014FB RID: 5371 RVA: 0x000B6305 File Offset: 0x000B4505
	// (set) Token: 0x060014FC RID: 5372 RVA: 0x000B6325 File Offset: 0x000B4525
	public static bool MasksBanned
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_MasksBanned");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_MasksBanned", value);
		}
	}

	// Token: 0x170003A1 RID: 929
	// (get) Token: 0x060014FD RID: 5373 RVA: 0x000B6346 File Offset: 0x000B4546
	// (set) Token: 0x060014FE RID: 5374 RVA: 0x000B6366 File Offset: 0x000B4566
	public static bool Paranormal
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_Paranormal");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_Paranormal", value);
		}
	}

	// Token: 0x170003A2 RID: 930
	// (get) Token: 0x060014FF RID: 5375 RVA: 0x000B6387 File Offset: 0x000B4587
	// (set) Token: 0x06001500 RID: 5376 RVA: 0x000B63A7 File Offset: 0x000B45A7
	public static bool EasyMode
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_EasyMode");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_EasyMode", value);
		}
	}

	// Token: 0x170003A3 RID: 931
	// (get) Token: 0x06001501 RID: 5377 RVA: 0x000B63C8 File Offset: 0x000B45C8
	// (set) Token: 0x06001502 RID: 5378 RVA: 0x000B63E8 File Offset: 0x000B45E8
	public static bool HardMode
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_HardMode");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_HardMode", value);
		}
	}

	// Token: 0x170003A4 RID: 932
	// (get) Token: 0x06001503 RID: 5379 RVA: 0x000B6409 File Offset: 0x000B4609
	// (set) Token: 0x06001504 RID: 5380 RVA: 0x000B6429 File Offset: 0x000B4629
	public static bool EmptyDemon
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_EmptyDemon");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_EmptyDemon", value);
		}
	}

	// Token: 0x170003A5 RID: 933
	// (get) Token: 0x06001505 RID: 5381 RVA: 0x000B644A File Offset: 0x000B464A
	// (set) Token: 0x06001506 RID: 5382 RVA: 0x000B646A File Offset: 0x000B466A
	public static bool CensorBlood
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_CensorBlood");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_CensorBlood", value);
		}
	}

	// Token: 0x170003A6 RID: 934
	// (get) Token: 0x06001507 RID: 5383 RVA: 0x000B648B File Offset: 0x000B468B
	// (set) Token: 0x06001508 RID: 5384 RVA: 0x000B64AB File Offset: 0x000B46AB
	public static bool SpareUniform
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_SpareUniform");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_SpareUniform", value);
		}
	}

	// Token: 0x170003A7 RID: 935
	// (get) Token: 0x06001509 RID: 5385 RVA: 0x000B64CC File Offset: 0x000B46CC
	// (set) Token: 0x0600150A RID: 5386 RVA: 0x000B64EC File Offset: 0x000B46EC
	public static bool BlondeHair
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_BlondeHair");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_BlondeHair", value);
		}
	}

	// Token: 0x170003A8 RID: 936
	// (get) Token: 0x0600150B RID: 5387 RVA: 0x000B650D File Offset: 0x000B470D
	// (set) Token: 0x0600150C RID: 5388 RVA: 0x000B652D File Offset: 0x000B472D
	public static bool SenpaiMourning
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_SenpaiMourning");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_SenpaiMourning", value);
		}
	}

	// Token: 0x170003A9 RID: 937
	// (get) Token: 0x0600150D RID: 5389 RVA: 0x000B654E File Offset: 0x000B474E
	// (set) Token: 0x0600150E RID: 5390 RVA: 0x000B655A File Offset: 0x000B475A
	public static int RivalEliminationID
	{
		get
		{
			return PlayerPrefs.GetInt("RivalEliminationID");
		}
		set
		{
			PlayerPrefs.SetInt("RivalEliminationID", value);
		}
	}

	// Token: 0x170003AA RID: 938
	// (get) Token: 0x0600150F RID: 5391 RVA: 0x000B6567 File Offset: 0x000B4767
	// (set) Token: 0x06001510 RID: 5392 RVA: 0x000B6573 File Offset: 0x000B4773
	public static bool NonlethalElimination
	{
		get
		{
			return GlobalsHelper.GetBool("NonlethalElimination");
		}
		set
		{
			GlobalsHelper.SetBool("NonlethalElimination", value);
		}
	}

	// Token: 0x170003AB RID: 939
	// (get) Token: 0x06001511 RID: 5393 RVA: 0x000B6580 File Offset: 0x000B4780
	// (set) Token: 0x06001512 RID: 5394 RVA: 0x000B65A0 File Offset: 0x000B47A0
	public static bool ReputationsInitialized
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_ReputationsInitialized");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_ReputationsInitialized", value);
		}
	}

	// Token: 0x170003AC RID: 940
	// (get) Token: 0x06001513 RID: 5395 RVA: 0x000B65C1 File Offset: 0x000B47C1
	// (set) Token: 0x06001514 RID: 5396 RVA: 0x000B65E1 File Offset: 0x000B47E1
	public static bool AnswerSheetUnavailable
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_AnswerSheetUnavailable");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_AnswerSheetUnavailable", value);
		}
	}

	// Token: 0x170003AD RID: 941
	// (get) Token: 0x06001515 RID: 5397 RVA: 0x000B6602 File Offset: 0x000B4802
	// (set) Token: 0x06001516 RID: 5398 RVA: 0x000B6622 File Offset: 0x000B4822
	public static bool AlphabetMode
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_AlphabetMode");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_AlphabetMode", value);
		}
	}

	// Token: 0x06001517 RID: 5399 RVA: 0x000B6644 File Offset: 0x000B4844
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_LoveSick");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MasksBanned");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Paranormal");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_EasyMode");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_HardMode");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_EmptyDemon");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CensorBlood");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SpareUniform");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_BlondeHair");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SenpaiMourning");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_RivalEliminationID");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_NonlethalElimination");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_ReputationsInitialized");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_AnswerSheetUnavailable");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_AlphabetMode");
	}

	// Token: 0x04001D2B RID: 7467
	private const string Str_Profile = "Profile";

	// Token: 0x04001D2C RID: 7468
	private const string Str_LoveSick = "LoveSick";

	// Token: 0x04001D2D RID: 7469
	private const string Str_MasksBanned = "MasksBanned";

	// Token: 0x04001D2E RID: 7470
	private const string Str_Paranormal = "Paranormal";

	// Token: 0x04001D2F RID: 7471
	private const string Str_EasyMode = "EasyMode";

	// Token: 0x04001D30 RID: 7472
	private const string Str_HardMode = "HardMode";

	// Token: 0x04001D31 RID: 7473
	private const string Str_EmptyDemon = "EmptyDemon";

	// Token: 0x04001D32 RID: 7474
	private const string Str_CensorBlood = "CensorBlood";

	// Token: 0x04001D33 RID: 7475
	private const string Str_SpareUniform = "SpareUniform";

	// Token: 0x04001D34 RID: 7476
	private const string Str_BlondeHair = "BlondeHair";

	// Token: 0x04001D35 RID: 7477
	private const string Str_SenpaiMourning = "SenpaiMourning";

	// Token: 0x04001D36 RID: 7478
	private const string Str_RivalEliminationID = "RivalEliminationID";

	// Token: 0x04001D37 RID: 7479
	private const string Str_NonlethalElimination = "NonlethalElimination";

	// Token: 0x04001D38 RID: 7480
	private const string Str_ReputationsInitialized = "ReputationsInitialized";

	// Token: 0x04001D39 RID: 7481
	private const string Str_AnswerSheetUnavailable = "AnswerSheetUnavailable";

	// Token: 0x04001D3A RID: 7482
	private const string Str_AlphabetMode = "AlphabetMode";
}
