using System;
using UnityEngine;

// Token: 0x020002BF RID: 703
public static class DateGlobals
{
	// Token: 0x1700038F RID: 911
	// (get) Token: 0x060014C7 RID: 5319 RVA: 0x000B5883 File Offset: 0x000B3A83
	// (set) Token: 0x060014C8 RID: 5320 RVA: 0x000B58A3 File Offset: 0x000B3AA3
	public static int Week
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Week");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Week", value);
		}
	}

	// Token: 0x17000390 RID: 912
	// (get) Token: 0x060014C9 RID: 5321 RVA: 0x000B58C4 File Offset: 0x000B3AC4
	// (set) Token: 0x060014CA RID: 5322 RVA: 0x000B58E4 File Offset: 0x000B3AE4
	public static DayOfWeek Weekday
	{
		get
		{
			return GlobalsHelper.GetEnum<DayOfWeek>("Profile_" + GameGlobals.Profile + "_Weekday");
		}
		set
		{
			GlobalsHelper.SetEnum<DayOfWeek>("Profile_" + GameGlobals.Profile + "_Weekday", value);
		}
	}

	// Token: 0x17000391 RID: 913
	// (get) Token: 0x060014CB RID: 5323 RVA: 0x000B5905 File Offset: 0x000B3B05
	// (set) Token: 0x060014CC RID: 5324 RVA: 0x000B5925 File Offset: 0x000B3B25
	public static int PassDays
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_PassDays");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_PassDays", value);
		}
	}

	// Token: 0x17000392 RID: 914
	// (get) Token: 0x060014CD RID: 5325 RVA: 0x000B5946 File Offset: 0x000B3B46
	// (set) Token: 0x060014CE RID: 5326 RVA: 0x000B5966 File Offset: 0x000B3B66
	public static bool DayPassed
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_DayPassed");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_DayPassed", value);
		}
	}

	// Token: 0x060014CF RID: 5327 RVA: 0x000B5988 File Offset: 0x000B3B88
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Week");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Weekday");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_PassDays");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_DayPassed");
	}

	// Token: 0x04001D17 RID: 7447
	private const string Str_Week = "Week";

	// Token: 0x04001D18 RID: 7448
	private const string Str_Weekday = "Weekday";

	// Token: 0x04001D19 RID: 7449
	private const string Str_PassDays = "PassDays";

	// Token: 0x04001D1A RID: 7450
	private const string Str_DayPassed = "DayPassed";
}
