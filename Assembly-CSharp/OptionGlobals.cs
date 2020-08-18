using System;
using UnityEngine;

// Token: 0x020002C5 RID: 709
public static class OptionGlobals
{
	// Token: 0x170003BC RID: 956
	// (get) Token: 0x06001539 RID: 5433 RVA: 0x000B6B70 File Offset: 0x000B4D70
	// (set) Token: 0x0600153A RID: 5434 RVA: 0x000B6B90 File Offset: 0x000B4D90
	public static bool DisableBloom
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_DisableBloom");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_DisableBloom", value);
		}
	}

	// Token: 0x170003BD RID: 957
	// (get) Token: 0x0600153B RID: 5435 RVA: 0x000B6BB1 File Offset: 0x000B4DB1
	// (set) Token: 0x0600153C RID: 5436 RVA: 0x000B6BD1 File Offset: 0x000B4DD1
	public static int DisableFarAnimations
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_DisableFarAnimations");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_DisableFarAnimations", value);
		}
	}

	// Token: 0x170003BE RID: 958
	// (get) Token: 0x0600153D RID: 5437 RVA: 0x000B6BF2 File Offset: 0x000B4DF2
	// (set) Token: 0x0600153E RID: 5438 RVA: 0x000B6C12 File Offset: 0x000B4E12
	public static bool DisableOutlines
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_DisableOutlines");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_DisableOutlines", value);
		}
	}

	// Token: 0x170003BF RID: 959
	// (get) Token: 0x0600153F RID: 5439 RVA: 0x000B6C33 File Offset: 0x000B4E33
	// (set) Token: 0x06001540 RID: 5440 RVA: 0x000B6C53 File Offset: 0x000B4E53
	public static bool DisablePostAliasing
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_DisablePostAliasing");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_DisablePostAliasing", value);
		}
	}

	// Token: 0x170003C0 RID: 960
	// (get) Token: 0x06001541 RID: 5441 RVA: 0x000B6C74 File Offset: 0x000B4E74
	// (set) Token: 0x06001542 RID: 5442 RVA: 0x000B6C94 File Offset: 0x000B4E94
	public static bool EnableShadows
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_EnableShadows");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_EnableShadows", value);
		}
	}

	// Token: 0x170003C1 RID: 961
	// (get) Token: 0x06001543 RID: 5443 RVA: 0x000B6CB5 File Offset: 0x000B4EB5
	// (set) Token: 0x06001544 RID: 5444 RVA: 0x000B6CD5 File Offset: 0x000B4ED5
	public static bool DisableObscurance
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_DisableObscurance");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_DisableObscurance", value);
		}
	}

	// Token: 0x170003C2 RID: 962
	// (get) Token: 0x06001545 RID: 5445 RVA: 0x000B6CF6 File Offset: 0x000B4EF6
	// (set) Token: 0x06001546 RID: 5446 RVA: 0x000B6D16 File Offset: 0x000B4F16
	public static int DrawDistance
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_DrawDistance");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_DrawDistance", value);
		}
	}

	// Token: 0x170003C3 RID: 963
	// (get) Token: 0x06001547 RID: 5447 RVA: 0x000B6D37 File Offset: 0x000B4F37
	// (set) Token: 0x06001548 RID: 5448 RVA: 0x000B6D57 File Offset: 0x000B4F57
	public static int DrawDistanceLimit
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_DrawDistanceLimit");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_DrawDistanceLimit", value);
		}
	}

	// Token: 0x170003C4 RID: 964
	// (get) Token: 0x06001549 RID: 5449 RVA: 0x000B6D78 File Offset: 0x000B4F78
	// (set) Token: 0x0600154A RID: 5450 RVA: 0x000B6D98 File Offset: 0x000B4F98
	public static bool Fog
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_Fog");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_Fog", value);
		}
	}

	// Token: 0x170003C5 RID: 965
	// (get) Token: 0x0600154B RID: 5451 RVA: 0x000B6DB9 File Offset: 0x000B4FB9
	// (set) Token: 0x0600154C RID: 5452 RVA: 0x000B6DD9 File Offset: 0x000B4FD9
	public static int FPSIndex
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_FPSIndex");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_FPSIndex", value);
		}
	}

	// Token: 0x170003C6 RID: 966
	// (get) Token: 0x0600154D RID: 5453 RVA: 0x000B6DFA File Offset: 0x000B4FFA
	// (set) Token: 0x0600154E RID: 5454 RVA: 0x000B6E1A File Offset: 0x000B501A
	public static bool HighPopulation
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_HighPopulation");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_HighPopulation", value);
		}
	}

	// Token: 0x170003C7 RID: 967
	// (get) Token: 0x0600154F RID: 5455 RVA: 0x000B6E3B File Offset: 0x000B503B
	// (set) Token: 0x06001550 RID: 5456 RVA: 0x000B6E5B File Offset: 0x000B505B
	public static int LowDetailStudents
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_LowDetailStudents");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_LowDetailStudents", value);
		}
	}

	// Token: 0x170003C8 RID: 968
	// (get) Token: 0x06001551 RID: 5457 RVA: 0x000B6E7C File Offset: 0x000B507C
	// (set) Token: 0x06001552 RID: 5458 RVA: 0x000B6E9C File Offset: 0x000B509C
	public static int ParticleCount
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_ParticleCount");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_ParticleCount", value);
		}
	}

	// Token: 0x170003C9 RID: 969
	// (get) Token: 0x06001553 RID: 5459 RVA: 0x000B6EBD File Offset: 0x000B50BD
	// (set) Token: 0x06001554 RID: 5460 RVA: 0x000B6EDD File Offset: 0x000B50DD
	public static bool RimLight
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_RimLight");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_RimLight", value);
		}
	}

	// Token: 0x170003CA RID: 970
	// (get) Token: 0x06001555 RID: 5461 RVA: 0x000B6EFE File Offset: 0x000B50FE
	// (set) Token: 0x06001556 RID: 5462 RVA: 0x000B6F1E File Offset: 0x000B511E
	public static bool DepthOfField
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_DepthOfField");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_DepthOfField", value);
		}
	}

	// Token: 0x170003CB RID: 971
	// (get) Token: 0x06001557 RID: 5463 RVA: 0x000B6F3F File Offset: 0x000B513F
	// (set) Token: 0x06001558 RID: 5464 RVA: 0x000B6F5F File Offset: 0x000B515F
	public static int Sensitivity
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Sensitivity");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Sensitivity", value);
		}
	}

	// Token: 0x170003CC RID: 972
	// (get) Token: 0x06001559 RID: 5465 RVA: 0x000B6F80 File Offset: 0x000B5180
	// (set) Token: 0x0600155A RID: 5466 RVA: 0x000B6FA0 File Offset: 0x000B51A0
	public static bool InvertAxis
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_InvertAxis");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_InvertAxis", value);
		}
	}

	// Token: 0x170003CD RID: 973
	// (get) Token: 0x0600155B RID: 5467 RVA: 0x000B6FC1 File Offset: 0x000B51C1
	// (set) Token: 0x0600155C RID: 5468 RVA: 0x000B6FE1 File Offset: 0x000B51E1
	public static bool TutorialsOff
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_TutorialsOff");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_TutorialsOff", value);
		}
	}

	// Token: 0x170003CE RID: 974
	// (get) Token: 0x0600155D RID: 5469 RVA: 0x000B7002 File Offset: 0x000B5202
	// (set) Token: 0x0600155E RID: 5470 RVA: 0x000B7022 File Offset: 0x000B5222
	public static bool ToggleRun
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_ToggleRun");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_ToggleRun", value);
		}
	}

	// Token: 0x0600155F RID: 5471 RVA: 0x000B7044 File Offset: 0x000B5244
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_DisableBloom");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_DisableFarAnimations");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_DisableOutlines");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_DisablePostAliasing");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_EnableShadows");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_DisableObscurance");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_DrawDistance");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_DrawDistanceLimit");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Fog");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_FPSIndex");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_HighPopulation");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_LowDetailStudents");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_ParticleCount");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_RimLight");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_DepthOfField");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Sensitivity");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_InvertAxis");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_TutorialsOff");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_ToggleRun");
	}

	// Token: 0x04001D4A RID: 7498
	private const string Str_DisableBloom = "DisableBloom";

	// Token: 0x04001D4B RID: 7499
	private const string Str_DisableFarAnimations = "DisableFarAnimations";

	// Token: 0x04001D4C RID: 7500
	private const string Str_DisableOutlines = "DisableOutlines";

	// Token: 0x04001D4D RID: 7501
	private const string Str_DisablePostAliasing = "DisablePostAliasing";

	// Token: 0x04001D4E RID: 7502
	private const string Str_EnableShadows = "EnableShadows";

	// Token: 0x04001D4F RID: 7503
	private const string Str_DisableObscurance = "DisableObscurance";

	// Token: 0x04001D50 RID: 7504
	private const string Str_DrawDistance = "DrawDistance";

	// Token: 0x04001D51 RID: 7505
	private const string Str_DrawDistanceLimit = "DrawDistanceLimit";

	// Token: 0x04001D52 RID: 7506
	private const string Str_Fog = "Fog";

	// Token: 0x04001D53 RID: 7507
	private const string Str_FPSIndex = "FPSIndex";

	// Token: 0x04001D54 RID: 7508
	private const string Str_HighPopulation = "HighPopulation";

	// Token: 0x04001D55 RID: 7509
	private const string Str_LowDetailStudents = "LowDetailStudents";

	// Token: 0x04001D56 RID: 7510
	private const string Str_ParticleCount = "ParticleCount";

	// Token: 0x04001D57 RID: 7511
	private const string Str_RimLight = "RimLight";

	// Token: 0x04001D58 RID: 7512
	private const string Str_DepthOfField = "DepthOfField";

	// Token: 0x04001D59 RID: 7513
	private const string Str_Sensitivity = "Sensitivity";

	// Token: 0x04001D5A RID: 7514
	private const string Str_InvertAxis = "InvertAxis";

	// Token: 0x04001D5B RID: 7515
	private const string Str_TutorialsOff = "TutorialsOff";

	// Token: 0x04001D5C RID: 7516
	private const string Str_ToggleRun = "ToggleRun";
}
