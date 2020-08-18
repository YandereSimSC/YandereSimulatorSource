using System;

// Token: 0x020002D0 RID: 720
public static class TutorialGlobals
{
	// Token: 0x17000410 RID: 1040
	// (get) Token: 0x0600166F RID: 5743 RVA: 0x000BB0CF File Offset: 0x000B92CF
	// (set) Token: 0x06001670 RID: 5744 RVA: 0x000BB0EF File Offset: 0x000B92EF
	public static bool IgnoreClothing
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreClothing");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreClothing", value);
		}
	}

	// Token: 0x17000411 RID: 1041
	// (get) Token: 0x06001671 RID: 5745 RVA: 0x000BB110 File Offset: 0x000B9310
	// (set) Token: 0x06001672 RID: 5746 RVA: 0x000BB130 File Offset: 0x000B9330
	public static bool IgnoreCouncil
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreCouncil");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreCouncil", value);
		}
	}

	// Token: 0x17000412 RID: 1042
	// (get) Token: 0x06001673 RID: 5747 RVA: 0x000BB151 File Offset: 0x000B9351
	// (set) Token: 0x06001674 RID: 5748 RVA: 0x000BB171 File Offset: 0x000B9371
	public static bool IgnoreTeacher
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreTeacher");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreTeacher", value);
		}
	}

	// Token: 0x17000413 RID: 1043
	// (get) Token: 0x06001675 RID: 5749 RVA: 0x000BB192 File Offset: 0x000B9392
	// (set) Token: 0x06001676 RID: 5750 RVA: 0x000BB1B2 File Offset: 0x000B93B2
	public static bool IgnoreLocker
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreLocker");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreLocker", value);
		}
	}

	// Token: 0x17000414 RID: 1044
	// (get) Token: 0x06001677 RID: 5751 RVA: 0x000BB1D3 File Offset: 0x000B93D3
	// (set) Token: 0x06001678 RID: 5752 RVA: 0x000BB1F3 File Offset: 0x000B93F3
	public static bool IgnorePolice
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnorePolice");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnorePolice", value);
		}
	}

	// Token: 0x17000415 RID: 1045
	// (get) Token: 0x06001679 RID: 5753 RVA: 0x000BB214 File Offset: 0x000B9414
	// (set) Token: 0x0600167A RID: 5754 RVA: 0x000BB234 File Offset: 0x000B9434
	public static bool IgnoreSanity
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreSanity");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreSanity", value);
		}
	}

	// Token: 0x17000416 RID: 1046
	// (get) Token: 0x0600167B RID: 5755 RVA: 0x000BB255 File Offset: 0x000B9455
	// (set) Token: 0x0600167C RID: 5756 RVA: 0x000BB275 File Offset: 0x000B9475
	public static bool IgnoreSenpai
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreSenpai");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreSenpai", value);
		}
	}

	// Token: 0x17000417 RID: 1047
	// (get) Token: 0x0600167D RID: 5757 RVA: 0x000BB296 File Offset: 0x000B9496
	// (set) Token: 0x0600167E RID: 5758 RVA: 0x000BB2B6 File Offset: 0x000B94B6
	public static bool IgnoreVision
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreVision");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreVision", value);
		}
	}

	// Token: 0x17000418 RID: 1048
	// (get) Token: 0x0600167F RID: 5759 RVA: 0x000BB2D7 File Offset: 0x000B94D7
	// (set) Token: 0x06001680 RID: 5760 RVA: 0x000BB2F7 File Offset: 0x000B94F7
	public static bool IgnoreWeapon
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreWeapon");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreWeapon", value);
		}
	}

	// Token: 0x17000419 RID: 1049
	// (get) Token: 0x06001681 RID: 5761 RVA: 0x000BB318 File Offset: 0x000B9518
	// (set) Token: 0x06001682 RID: 5762 RVA: 0x000BB338 File Offset: 0x000B9538
	public static bool IgnoreBlood
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreBlood");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreBlood", value);
		}
	}

	// Token: 0x1700041A RID: 1050
	// (get) Token: 0x06001683 RID: 5763 RVA: 0x000BB359 File Offset: 0x000B9559
	// (set) Token: 0x06001684 RID: 5764 RVA: 0x000BB379 File Offset: 0x000B9579
	public static bool IgnoreClass
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreClass");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreClass", value);
		}
	}

	// Token: 0x1700041B RID: 1051
	// (get) Token: 0x06001685 RID: 5765 RVA: 0x000BB39A File Offset: 0x000B959A
	// (set) Token: 0x06001686 RID: 5766 RVA: 0x000BB3BA File Offset: 0x000B95BA
	public static bool IgnorePhoto
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnorePhoto");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnorePhoto", value);
		}
	}

	// Token: 0x1700041C RID: 1052
	// (get) Token: 0x06001687 RID: 5767 RVA: 0x000BB3DB File Offset: 0x000B95DB
	// (set) Token: 0x06001688 RID: 5768 RVA: 0x000BB3FB File Offset: 0x000B95FB
	public static bool IgnoreClub
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreClub");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreClub", value);
		}
	}

	// Token: 0x1700041D RID: 1053
	// (get) Token: 0x06001689 RID: 5769 RVA: 0x000BB41C File Offset: 0x000B961C
	// (set) Token: 0x0600168A RID: 5770 RVA: 0x000BB43C File Offset: 0x000B963C
	public static bool IgnoreInfo
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreInfo");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreInfo", value);
		}
	}

	// Token: 0x1700041E RID: 1054
	// (get) Token: 0x0600168B RID: 5771 RVA: 0x000BB45D File Offset: 0x000B965D
	// (set) Token: 0x0600168C RID: 5772 RVA: 0x000BB47D File Offset: 0x000B967D
	public static bool IgnorePool
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnorePool");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnorePool", value);
		}
	}

	// Token: 0x1700041F RID: 1055
	// (get) Token: 0x0600168D RID: 5773 RVA: 0x000BB49E File Offset: 0x000B969E
	// (set) Token: 0x0600168E RID: 5774 RVA: 0x000BB4BE File Offset: 0x000B96BE
	public static bool IgnoreRep
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreClass");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreClass", value);
		}
	}

	// Token: 0x0600168F RID: 5775 RVA: 0x000BB4E0 File Offset: 0x000B96E0
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreClothing");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreCouncil");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreTeacher");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreLocker");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnorePolice");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreSanity");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreSenpai");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreVision");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreWeapon");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreBlood");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreClass");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnorePhoto");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreClub");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreInfo");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnorePool");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreClass");
	}

	// Token: 0x04001DCB RID: 7627
	private const string Str_IgnoreClothing = "IgnoreClothing";

	// Token: 0x04001DCC RID: 7628
	private const string Str_IgnoreCouncil = "IgnoreCouncil";

	// Token: 0x04001DCD RID: 7629
	private const string Str_IgnoreTeacher = "IgnoreTeacher";

	// Token: 0x04001DCE RID: 7630
	private const string Str_IgnoreLocker = "IgnoreLocker";

	// Token: 0x04001DCF RID: 7631
	private const string Str_IgnorePolice = "IgnorePolice";

	// Token: 0x04001DD0 RID: 7632
	private const string Str_IgnoreSanity = "IgnoreSanity";

	// Token: 0x04001DD1 RID: 7633
	private const string Str_IgnoreSenpai = "IgnoreSenpai";

	// Token: 0x04001DD2 RID: 7634
	private const string Str_IgnoreVision = "IgnoreVision";

	// Token: 0x04001DD3 RID: 7635
	private const string Str_IgnoreWeapon = "IgnoreWeapon";

	// Token: 0x04001DD4 RID: 7636
	private const string Str_IgnoreBlood = "IgnoreBlood";

	// Token: 0x04001DD5 RID: 7637
	private const string Str_IgnoreClass = "IgnoreClass";

	// Token: 0x04001DD6 RID: 7638
	private const string Str_IgnorePhoto = "IgnorePhoto";

	// Token: 0x04001DD7 RID: 7639
	private const string Str_IgnoreClub = "IgnoreClub";

	// Token: 0x04001DD8 RID: 7640
	private const string Str_IgnoreInfo = "IgnoreInfo";

	// Token: 0x04001DD9 RID: 7641
	private const string Str_IgnorePool = "IgnorePool";

	// Token: 0x04001DDA RID: 7642
	private const string Str_IgnoreRep = "IgnoreClass";
}
