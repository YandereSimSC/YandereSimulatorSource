using System;
using UnityEngine;

// Token: 0x020002D2 RID: 722
public static class YancordGlobals
{
	// Token: 0x17000438 RID: 1080
	// (get) Token: 0x060016C1 RID: 5825 RVA: 0x000BBFC5 File Offset: 0x000BA1C5
	// (set) Token: 0x060016C2 RID: 5826 RVA: 0x000BBFE5 File Offset: 0x000BA1E5
	public static bool JoinedYancord
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_JoinedYancord");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_JoinedYancord", value);
		}
	}

	// Token: 0x17000439 RID: 1081
	// (get) Token: 0x060016C3 RID: 5827 RVA: 0x000BC006 File Offset: 0x000BA206
	// (set) Token: 0x060016C4 RID: 5828 RVA: 0x000BC026 File Offset: 0x000BA226
	public static int CurrentConversation
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_CurrentConversation");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CurrentConversation", value);
		}
	}

	// Token: 0x060016C5 RID: 5829 RVA: 0x000BC047 File Offset: 0x000BA247
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_JoinedYancord");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CurrentConversation");
	}

	// Token: 0x04001DF3 RID: 7667
	private const string Str_JoinedYancord = "JoinedYancord";

	// Token: 0x04001DF4 RID: 7668
	private const string Str_CurrentConversation = "CurrentConversation";
}
