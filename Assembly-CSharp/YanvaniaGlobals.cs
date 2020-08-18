using System;

// Token: 0x020002CE RID: 718
public static class YanvaniaGlobals
{
	// Token: 0x1700040E RID: 1038
	// (get) Token: 0x06001666 RID: 5734 RVA: 0x000BAF29 File Offset: 0x000B9129
	// (set) Token: 0x06001667 RID: 5735 RVA: 0x000BAF49 File Offset: 0x000B9149
	public static bool DraculaDefeated
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_DraculaDefeated");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_DraculaDefeated", value);
		}
	}

	// Token: 0x1700040F RID: 1039
	// (get) Token: 0x06001668 RID: 5736 RVA: 0x000BAF6A File Offset: 0x000B916A
	// (set) Token: 0x06001669 RID: 5737 RVA: 0x000BAF8A File Offset: 0x000B918A
	public static bool MidoriEasterEgg
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_MidoriEasterEgg");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_MidoriEasterEgg", value);
		}
	}

	// Token: 0x0600166A RID: 5738 RVA: 0x000BAFAB File Offset: 0x000B91AB
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_DraculaDefeated");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MidoriEasterEgg");
	}

	// Token: 0x04001DC8 RID: 7624
	private const string Str_DraculaDefeated = "DraculaDefeated";

	// Token: 0x04001DC9 RID: 7625
	private const string Str_MidoriEasterEgg = "MidoriEasterEgg";
}
