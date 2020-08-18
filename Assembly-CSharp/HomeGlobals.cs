using System;

// Token: 0x020002C3 RID: 707
public static class HomeGlobals
{
	// Token: 0x170003AE RID: 942
	// (get) Token: 0x06001518 RID: 5400 RVA: 0x000B6813 File Offset: 0x000B4A13
	// (set) Token: 0x06001519 RID: 5401 RVA: 0x000B6833 File Offset: 0x000B4A33
	public static bool LateForSchool
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_LateForSchool");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_LateForSchool", value);
		}
	}

	// Token: 0x170003AF RID: 943
	// (get) Token: 0x0600151A RID: 5402 RVA: 0x000B6854 File Offset: 0x000B4A54
	// (set) Token: 0x0600151B RID: 5403 RVA: 0x000B6874 File Offset: 0x000B4A74
	public static bool Night
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_Night");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_Night", value);
		}
	}

	// Token: 0x170003B0 RID: 944
	// (get) Token: 0x0600151C RID: 5404 RVA: 0x000B6895 File Offset: 0x000B4A95
	// (set) Token: 0x0600151D RID: 5405 RVA: 0x000B68B5 File Offset: 0x000B4AB5
	public static bool StartInBasement
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_StartInBasement");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_StartInBasement", value);
		}
	}

	// Token: 0x170003B1 RID: 945
	// (get) Token: 0x0600151E RID: 5406 RVA: 0x000B68D6 File Offset: 0x000B4AD6
	// (set) Token: 0x0600151F RID: 5407 RVA: 0x000B68F6 File Offset: 0x000B4AF6
	public static bool MiyukiDefeated
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_MiyukiDefeated");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_MiyukiDefeated", value);
		}
	}

	// Token: 0x06001520 RID: 5408 RVA: 0x000B6918 File Offset: 0x000B4B18
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_LateForSchool");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Night");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_StartInBasement");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MiyukiDefeated");
	}

	// Token: 0x04001D3B RID: 7483
	private const string Str_LateForSchool = "LateForSchool";

	// Token: 0x04001D3C RID: 7484
	private const string Str_Night = "Night";

	// Token: 0x04001D3D RID: 7485
	private const string Str_StartInBasement = "StartInBasement";

	// Token: 0x04001D3E RID: 7486
	private const string Str_MiyukiDefeated = "MiyukiDefeated";
}
