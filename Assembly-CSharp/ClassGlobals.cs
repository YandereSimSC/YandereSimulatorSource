using System;
using UnityEngine;

// Token: 0x020002BB RID: 699
public static class ClassGlobals
{
	// Token: 0x1700037D RID: 893
	// (get) Token: 0x06001474 RID: 5236 RVA: 0x000B42CF File Offset: 0x000B24CF
	// (set) Token: 0x06001475 RID: 5237 RVA: 0x000B42EF File Offset: 0x000B24EF
	public static int Biology
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Biology");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Biology", value);
		}
	}

	// Token: 0x1700037E RID: 894
	// (get) Token: 0x06001476 RID: 5238 RVA: 0x000B4310 File Offset: 0x000B2510
	// (set) Token: 0x06001477 RID: 5239 RVA: 0x000B4330 File Offset: 0x000B2530
	public static int BiologyBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_BiologyBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_BiologyBonus", value);
		}
	}

	// Token: 0x1700037F RID: 895
	// (get) Token: 0x06001478 RID: 5240 RVA: 0x000B4351 File Offset: 0x000B2551
	// (set) Token: 0x06001479 RID: 5241 RVA: 0x000B4371 File Offset: 0x000B2571
	public static int BiologyGrade
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_BiologyGrade");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_BiologyGrade", value);
		}
	}

	// Token: 0x17000380 RID: 896
	// (get) Token: 0x0600147A RID: 5242 RVA: 0x000B4392 File Offset: 0x000B2592
	// (set) Token: 0x0600147B RID: 5243 RVA: 0x000B43B2 File Offset: 0x000B25B2
	public static int Chemistry
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Chemistry");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Chemistry", value);
		}
	}

	// Token: 0x17000381 RID: 897
	// (get) Token: 0x0600147C RID: 5244 RVA: 0x000B43D3 File Offset: 0x000B25D3
	// (set) Token: 0x0600147D RID: 5245 RVA: 0x000B43F3 File Offset: 0x000B25F3
	public static int ChemistryBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_ChemistryBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_ChemistryBonus", value);
		}
	}

	// Token: 0x17000382 RID: 898
	// (get) Token: 0x0600147E RID: 5246 RVA: 0x000B4414 File Offset: 0x000B2614
	// (set) Token: 0x0600147F RID: 5247 RVA: 0x000B4434 File Offset: 0x000B2634
	public static int ChemistryGrade
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_ChemistryGrade");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_ChemistryGrade", value);
		}
	}

	// Token: 0x17000383 RID: 899
	// (get) Token: 0x06001480 RID: 5248 RVA: 0x000B4455 File Offset: 0x000B2655
	// (set) Token: 0x06001481 RID: 5249 RVA: 0x000B4475 File Offset: 0x000B2675
	public static int Language
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Language");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Language", value);
		}
	}

	// Token: 0x17000384 RID: 900
	// (get) Token: 0x06001482 RID: 5250 RVA: 0x000B4496 File Offset: 0x000B2696
	// (set) Token: 0x06001483 RID: 5251 RVA: 0x000B44B6 File Offset: 0x000B26B6
	public static int LanguageBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_LanguageBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_LanguageBonus", value);
		}
	}

	// Token: 0x17000385 RID: 901
	// (get) Token: 0x06001484 RID: 5252 RVA: 0x000B44D7 File Offset: 0x000B26D7
	// (set) Token: 0x06001485 RID: 5253 RVA: 0x000B44F7 File Offset: 0x000B26F7
	public static int LanguageGrade
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_LanguageGrade");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_LanguageGrade", value);
		}
	}

	// Token: 0x17000386 RID: 902
	// (get) Token: 0x06001486 RID: 5254 RVA: 0x000B4518 File Offset: 0x000B2718
	// (set) Token: 0x06001487 RID: 5255 RVA: 0x000B4538 File Offset: 0x000B2738
	public static int Physical
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Physical");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Physical", value);
		}
	}

	// Token: 0x17000387 RID: 903
	// (get) Token: 0x06001488 RID: 5256 RVA: 0x000B4559 File Offset: 0x000B2759
	// (set) Token: 0x06001489 RID: 5257 RVA: 0x000B4579 File Offset: 0x000B2779
	public static int PhysicalBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_PhysicalBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_PhysicalBonus", value);
		}
	}

	// Token: 0x17000388 RID: 904
	// (get) Token: 0x0600148A RID: 5258 RVA: 0x000B459A File Offset: 0x000B279A
	// (set) Token: 0x0600148B RID: 5259 RVA: 0x000B45BA File Offset: 0x000B27BA
	public static int PhysicalGrade
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_PhysicalGrade");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_PhysicalGrade", value);
		}
	}

	// Token: 0x17000389 RID: 905
	// (get) Token: 0x0600148C RID: 5260 RVA: 0x000B45DB File Offset: 0x000B27DB
	// (set) Token: 0x0600148D RID: 5261 RVA: 0x000B45FB File Offset: 0x000B27FB
	public static int Psychology
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Psychology");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Psychology", value);
		}
	}

	// Token: 0x1700038A RID: 906
	// (get) Token: 0x0600148E RID: 5262 RVA: 0x000B461C File Offset: 0x000B281C
	// (set) Token: 0x0600148F RID: 5263 RVA: 0x000B463C File Offset: 0x000B283C
	public static int PsychologyBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_PsychologyBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_PsychologyBonus", value);
		}
	}

	// Token: 0x1700038B RID: 907
	// (get) Token: 0x06001490 RID: 5264 RVA: 0x000B465D File Offset: 0x000B285D
	// (set) Token: 0x06001491 RID: 5265 RVA: 0x000B467D File Offset: 0x000B287D
	public static int PsychologyGrade
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_PsychologyGrade");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_PsychologyGrade", value);
		}
	}

	// Token: 0x06001492 RID: 5266 RVA: 0x000B46A0 File Offset: 0x000B28A0
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Biology");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_BiologyBonus");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_BiologyGrade");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Chemistry");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_ChemistryBonus");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_ChemistryGrade");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Language");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_LanguageBonus");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_LanguageGrade");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Physical");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_PhysicalBonus");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_PhysicalGrade");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Psychology");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_PsychologyBonus");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_PsychologyGrade");
	}

	// Token: 0x04001CF6 RID: 7414
	private const string Str_Biology = "Biology";

	// Token: 0x04001CF7 RID: 7415
	private const string Str_BiologyBonus = "BiologyBonus";

	// Token: 0x04001CF8 RID: 7416
	private const string Str_BiologyGrade = "BiologyGrade";

	// Token: 0x04001CF9 RID: 7417
	private const string Str_Chemistry = "Chemistry";

	// Token: 0x04001CFA RID: 7418
	private const string Str_ChemistryBonus = "ChemistryBonus";

	// Token: 0x04001CFB RID: 7419
	private const string Str_ChemistryGrade = "ChemistryGrade";

	// Token: 0x04001CFC RID: 7420
	private const string Str_Language = "Language";

	// Token: 0x04001CFD RID: 7421
	private const string Str_LanguageBonus = "LanguageBonus";

	// Token: 0x04001CFE RID: 7422
	private const string Str_LanguageGrade = "LanguageGrade";

	// Token: 0x04001CFF RID: 7423
	private const string Str_Physical = "Physical";

	// Token: 0x04001D00 RID: 7424
	private const string Str_PhysicalBonus = "PhysicalBonus";

	// Token: 0x04001D01 RID: 7425
	private const string Str_PhysicalGrade = "PhysicalGrade";

	// Token: 0x04001D02 RID: 7426
	private const string Str_Psychology = "Psychology";

	// Token: 0x04001D03 RID: 7427
	private const string Str_PsychologyBonus = "PsychologyBonus";

	// Token: 0x04001D04 RID: 7428
	private const string Str_PsychologyGrade = "PsychologyGrade";
}
