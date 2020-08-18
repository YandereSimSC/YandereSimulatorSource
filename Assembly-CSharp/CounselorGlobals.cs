using System;
using UnityEngine;

// Token: 0x020002D1 RID: 721
public static class CounselorGlobals
{
	// Token: 0x17000420 RID: 1056
	// (get) Token: 0x06001690 RID: 5776 RVA: 0x000BB6CD File Offset: 0x000B98CD
	// (set) Token: 0x06001691 RID: 5777 RVA: 0x000BB6ED File Offset: 0x000B98ED
	public static int DelinquentPunishments
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_DelinquentPunishments");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_DelinquentPunishments", value);
		}
	}

	// Token: 0x17000421 RID: 1057
	// (get) Token: 0x06001692 RID: 5778 RVA: 0x000BB70E File Offset: 0x000B990E
	// (set) Token: 0x06001693 RID: 5779 RVA: 0x000BB72E File Offset: 0x000B992E
	public static int CounselorPunishments
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_CounselorPunishments");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CounselorPunishments", value);
		}
	}

	// Token: 0x17000422 RID: 1058
	// (get) Token: 0x06001694 RID: 5780 RVA: 0x000BB74F File Offset: 0x000B994F
	// (set) Token: 0x06001695 RID: 5781 RVA: 0x000BB76F File Offset: 0x000B996F
	public static int CounselorVisits
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_CounselorVisits");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CounselorVisits", value);
		}
	}

	// Token: 0x17000423 RID: 1059
	// (get) Token: 0x06001696 RID: 5782 RVA: 0x000BB790 File Offset: 0x000B9990
	// (set) Token: 0x06001697 RID: 5783 RVA: 0x000BB7B0 File Offset: 0x000B99B0
	public static int CounselorTape
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_CounselorTape");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CounselorTape", value);
		}
	}

	// Token: 0x17000424 RID: 1060
	// (get) Token: 0x06001698 RID: 5784 RVA: 0x000BB7D1 File Offset: 0x000B99D1
	// (set) Token: 0x06001699 RID: 5785 RVA: 0x000BB7F1 File Offset: 0x000B99F1
	public static int ApologiesUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_ApologiesUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_ApologiesUsed", value);
		}
	}

	// Token: 0x17000425 RID: 1061
	// (get) Token: 0x0600169A RID: 5786 RVA: 0x000BB812 File Offset: 0x000B9A12
	// (set) Token: 0x0600169B RID: 5787 RVA: 0x000BB832 File Offset: 0x000B9A32
	public static int WeaponsBanned
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_WeaponsBanned");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_WeaponsBanned", value);
		}
	}

	// Token: 0x17000426 RID: 1062
	// (get) Token: 0x0600169C RID: 5788 RVA: 0x000BB853 File Offset: 0x000B9A53
	// (set) Token: 0x0600169D RID: 5789 RVA: 0x000BB873 File Offset: 0x000B9A73
	public static int BloodVisits
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_BloodVisits");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_BloodVisits", value);
		}
	}

	// Token: 0x17000427 RID: 1063
	// (get) Token: 0x0600169E RID: 5790 RVA: 0x000BB894 File Offset: 0x000B9A94
	// (set) Token: 0x0600169F RID: 5791 RVA: 0x000BB8B4 File Offset: 0x000B9AB4
	public static int InsanityVisits
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_InsanityVisits");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_InsanityVisits", value);
		}
	}

	// Token: 0x17000428 RID: 1064
	// (get) Token: 0x060016A0 RID: 5792 RVA: 0x000BB8D5 File Offset: 0x000B9AD5
	// (set) Token: 0x060016A1 RID: 5793 RVA: 0x000BB8F5 File Offset: 0x000B9AF5
	public static int LewdVisits
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_LewdVisits");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_LewdVisits", value);
		}
	}

	// Token: 0x17000429 RID: 1065
	// (get) Token: 0x060016A2 RID: 5794 RVA: 0x000BB916 File Offset: 0x000B9B16
	// (set) Token: 0x060016A3 RID: 5795 RVA: 0x000BB936 File Offset: 0x000B9B36
	public static int TheftVisits
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_TheftVisits");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_TheftVisits", value);
		}
	}

	// Token: 0x1700042A RID: 1066
	// (get) Token: 0x060016A4 RID: 5796 RVA: 0x000BB957 File Offset: 0x000B9B57
	// (set) Token: 0x060016A5 RID: 5797 RVA: 0x000BB977 File Offset: 0x000B9B77
	public static int TrespassVisits
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_TrespassVisits");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_TrespassVisits", value);
		}
	}

	// Token: 0x1700042B RID: 1067
	// (get) Token: 0x060016A6 RID: 5798 RVA: 0x000BB998 File Offset: 0x000B9B98
	// (set) Token: 0x060016A7 RID: 5799 RVA: 0x000BB9B8 File Offset: 0x000B9BB8
	public static int WeaponVisits
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_WeaponVisits");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_WeaponVisits", value);
		}
	}

	// Token: 0x1700042C RID: 1068
	// (get) Token: 0x060016A8 RID: 5800 RVA: 0x000BB9D9 File Offset: 0x000B9BD9
	// (set) Token: 0x060016A9 RID: 5801 RVA: 0x000BB9F9 File Offset: 0x000B9BF9
	public static int BloodExcuseUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_BloodExcuseUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_BloodExcuseUsed", value);
		}
	}

	// Token: 0x1700042D RID: 1069
	// (get) Token: 0x060016AA RID: 5802 RVA: 0x000BBA1A File Offset: 0x000B9C1A
	// (set) Token: 0x060016AB RID: 5803 RVA: 0x000BBA3A File Offset: 0x000B9C3A
	public static int InsanityExcuseUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_InsanityExcuseUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_InsanityExcuseUsed", value);
		}
	}

	// Token: 0x1700042E RID: 1070
	// (get) Token: 0x060016AC RID: 5804 RVA: 0x000BBA5B File Offset: 0x000B9C5B
	// (set) Token: 0x060016AD RID: 5805 RVA: 0x000BBA7B File Offset: 0x000B9C7B
	public static int LewdExcuseUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_LewdExcuseUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_LewdExcuseUsed", value);
		}
	}

	// Token: 0x1700042F RID: 1071
	// (get) Token: 0x060016AE RID: 5806 RVA: 0x000BBA9C File Offset: 0x000B9C9C
	// (set) Token: 0x060016AF RID: 5807 RVA: 0x000BBABC File Offset: 0x000B9CBC
	public static int TheftExcuseUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_TheftExcuseUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_TheftExcuseUsed", value);
		}
	}

	// Token: 0x17000430 RID: 1072
	// (get) Token: 0x060016B0 RID: 5808 RVA: 0x000BBADD File Offset: 0x000B9CDD
	// (set) Token: 0x060016B1 RID: 5809 RVA: 0x000BBAFD File Offset: 0x000B9CFD
	public static int TrespassExcuseUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_TrespassExcuseUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_TrespassExcuseUsed", value);
		}
	}

	// Token: 0x17000431 RID: 1073
	// (get) Token: 0x060016B2 RID: 5810 RVA: 0x000BBB1E File Offset: 0x000B9D1E
	// (set) Token: 0x060016B3 RID: 5811 RVA: 0x000BBB3E File Offset: 0x000B9D3E
	public static int WeaponExcuseUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_WeaponExcuseUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_WeaponExcuseUsed", value);
		}
	}

	// Token: 0x17000432 RID: 1074
	// (get) Token: 0x060016B4 RID: 5812 RVA: 0x000BBB5F File Offset: 0x000B9D5F
	// (set) Token: 0x060016B5 RID: 5813 RVA: 0x000BBB7F File Offset: 0x000B9D7F
	public static int BloodBlameUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_BloodBlameUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_BloodBlameUsed", value);
		}
	}

	// Token: 0x17000433 RID: 1075
	// (get) Token: 0x060016B6 RID: 5814 RVA: 0x000BBBA0 File Offset: 0x000B9DA0
	// (set) Token: 0x060016B7 RID: 5815 RVA: 0x000BBBC0 File Offset: 0x000B9DC0
	public static int InsanityBlameUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_InsanityBlameUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_InsanityBlameUsed", value);
		}
	}

	// Token: 0x17000434 RID: 1076
	// (get) Token: 0x060016B8 RID: 5816 RVA: 0x000BBBE1 File Offset: 0x000B9DE1
	// (set) Token: 0x060016B9 RID: 5817 RVA: 0x000BBC01 File Offset: 0x000B9E01
	public static int LewdBlameUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_LewdBlameUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_LewdBlameUsed", value);
		}
	}

	// Token: 0x17000435 RID: 1077
	// (get) Token: 0x060016BA RID: 5818 RVA: 0x000BBC22 File Offset: 0x000B9E22
	// (set) Token: 0x060016BB RID: 5819 RVA: 0x000BBC42 File Offset: 0x000B9E42
	public static int TheftBlameUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_TheftBlameUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_TheftBlameUsed", value);
		}
	}

	// Token: 0x17000436 RID: 1078
	// (get) Token: 0x060016BC RID: 5820 RVA: 0x000BBC63 File Offset: 0x000B9E63
	// (set) Token: 0x060016BD RID: 5821 RVA: 0x000BBC83 File Offset: 0x000B9E83
	public static int TrespassBlameUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_TrespassBlameUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_TrespassBlameUsed", value);
		}
	}

	// Token: 0x17000437 RID: 1079
	// (get) Token: 0x060016BE RID: 5822 RVA: 0x000BBCA4 File Offset: 0x000B9EA4
	// (set) Token: 0x060016BF RID: 5823 RVA: 0x000BBCC4 File Offset: 0x000B9EC4
	public static int WeaponBlameUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_WeaponBlameUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_WeaponBlameUsed", value);
		}
	}

	// Token: 0x060016C0 RID: 5824 RVA: 0x000BBCE8 File Offset: 0x000B9EE8
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_DelinquentPunishments");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CounselorPunishments");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CounselorVisits");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CounselorTape");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_ApologiesUsed");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_WeaponsBanned");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_BloodVisits");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_InsanityVisits");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_LewdVisits");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_TheftVisits");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_TrespassVisits");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_WeaponVisits");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_BloodExcuseUsed");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_InsanityExcuseUsed");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_LewdExcuseUsed");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_TheftExcuseUsed");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_TrespassExcuseUsed");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_WeaponExcuseUsed");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_BloodBlameUsed");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_InsanityBlameUsed");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_LewdBlameUsed");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_TheftBlameUsed");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_TrespassBlameUsed");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_WeaponBlameUsed");
	}

	// Token: 0x04001DDB RID: 7643
	private const string Str_DelinquentPunishments = "DelinquentPunishments";

	// Token: 0x04001DDC RID: 7644
	private const string Str_CounselorPunishments = "CounselorPunishments";

	// Token: 0x04001DDD RID: 7645
	private const string Str_CounselorVisits = "CounselorVisits";

	// Token: 0x04001DDE RID: 7646
	private const string Str_CounselorTape = "CounselorTape";

	// Token: 0x04001DDF RID: 7647
	private const string Str_ApologiesUsed = "ApologiesUsed";

	// Token: 0x04001DE0 RID: 7648
	private const string Str_WeaponsBanned = "WeaponsBanned";

	// Token: 0x04001DE1 RID: 7649
	private const string Str_BloodVisits = "BloodVisits";

	// Token: 0x04001DE2 RID: 7650
	private const string Str_InsanityVisits = "InsanityVisits";

	// Token: 0x04001DE3 RID: 7651
	private const string Str_LewdVisits = "LewdVisits";

	// Token: 0x04001DE4 RID: 7652
	private const string Str_TheftVisits = "TheftVisits";

	// Token: 0x04001DE5 RID: 7653
	private const string Str_TrespassVisits = "TrespassVisits";

	// Token: 0x04001DE6 RID: 7654
	private const string Str_WeaponVisits = "WeaponVisits";

	// Token: 0x04001DE7 RID: 7655
	private const string Str_BloodExcuseUsed = "BloodExcuseUsed";

	// Token: 0x04001DE8 RID: 7656
	private const string Str_InsanityExcuseUsed = "InsanityExcuseUsed";

	// Token: 0x04001DE9 RID: 7657
	private const string Str_LewdExcuseUsed = "LewdExcuseUsed";

	// Token: 0x04001DEA RID: 7658
	private const string Str_TheftExcuseUsed = "TheftExcuseUsed";

	// Token: 0x04001DEB RID: 7659
	private const string Str_TrespassExcuseUsed = "TrespassExcuseUsed";

	// Token: 0x04001DEC RID: 7660
	private const string Str_WeaponExcuseUsed = "WeaponExcuseUsed";

	// Token: 0x04001DED RID: 7661
	private const string Str_BloodBlameUsed = "BloodBlameUsed";

	// Token: 0x04001DEE RID: 7662
	private const string Str_InsanityBlameUsed = "InsanityBlameUsed";

	// Token: 0x04001DEF RID: 7663
	private const string Str_LewdBlameUsed = "LewdBlameUsed";

	// Token: 0x04001DF0 RID: 7664
	private const string Str_TheftBlameUsed = "TheftBlameUsed";

	// Token: 0x04001DF1 RID: 7665
	private const string Str_TrespassBlameUsed = "TrespassBlameUsed";

	// Token: 0x04001DF2 RID: 7666
	private const string Str_WeaponBlameUsed = "WeaponBlameUsed";
}
