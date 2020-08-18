using System;
using UnityEngine;

// Token: 0x020002CB RID: 715
public static class SenpaiGlobals
{
	// Token: 0x170003F2 RID: 1010
	// (get) Token: 0x060015DA RID: 5594 RVA: 0x000B8D5F File Offset: 0x000B6F5F
	// (set) Token: 0x060015DB RID: 5595 RVA: 0x000B8D7F File Offset: 0x000B6F7F
	public static bool CustomSenpai
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_CustomSenpai");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_CustomSenpai", value);
		}
	}

	// Token: 0x170003F3 RID: 1011
	// (get) Token: 0x060015DC RID: 5596 RVA: 0x000B8DA0 File Offset: 0x000B6FA0
	// (set) Token: 0x060015DD RID: 5597 RVA: 0x000B8DC0 File Offset: 0x000B6FC0
	public static string SenpaiEyeColor
	{
		get
		{
			return PlayerPrefs.GetString("Profile_" + GameGlobals.Profile + "_SenpaiEyeColor");
		}
		set
		{
			PlayerPrefs.SetString("Profile_" + GameGlobals.Profile + "_SenpaiEyeColor", value);
		}
	}

	// Token: 0x170003F4 RID: 1012
	// (get) Token: 0x060015DE RID: 5598 RVA: 0x000B8DE1 File Offset: 0x000B6FE1
	// (set) Token: 0x060015DF RID: 5599 RVA: 0x000B8E01 File Offset: 0x000B7001
	public static int SenpaiEyeWear
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_SenpaiEyeWear");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_SenpaiEyeWear", value);
		}
	}

	// Token: 0x170003F5 RID: 1013
	// (get) Token: 0x060015E0 RID: 5600 RVA: 0x000B8E22 File Offset: 0x000B7022
	// (set) Token: 0x060015E1 RID: 5601 RVA: 0x000B8E42 File Offset: 0x000B7042
	public static int SenpaiFacialHair
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_SenpaiFacialHair");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_SenpaiFacialHair", value);
		}
	}

	// Token: 0x170003F6 RID: 1014
	// (get) Token: 0x060015E2 RID: 5602 RVA: 0x000B8E63 File Offset: 0x000B7063
	// (set) Token: 0x060015E3 RID: 5603 RVA: 0x000B8E83 File Offset: 0x000B7083
	public static string SenpaiHairColor
	{
		get
		{
			return PlayerPrefs.GetString("Profile_" + GameGlobals.Profile + "_SenpaiHairColor");
		}
		set
		{
			PlayerPrefs.SetString("Profile_" + GameGlobals.Profile + "_SenpaiHairColor", value);
		}
	}

	// Token: 0x170003F7 RID: 1015
	// (get) Token: 0x060015E4 RID: 5604 RVA: 0x000B8EA4 File Offset: 0x000B70A4
	// (set) Token: 0x060015E5 RID: 5605 RVA: 0x000B8EC4 File Offset: 0x000B70C4
	public static int SenpaiHairStyle
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_SenpaiHairStyle");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_SenpaiHairStyle", value);
		}
	}

	// Token: 0x170003F8 RID: 1016
	// (get) Token: 0x060015E6 RID: 5606 RVA: 0x000B8EE5 File Offset: 0x000B70E5
	// (set) Token: 0x060015E7 RID: 5607 RVA: 0x000B8F05 File Offset: 0x000B7105
	public static int SenpaiSkinColor
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_SenpaiSkinColor");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_SenpaiSkinColor", value);
		}
	}

	// Token: 0x060015E8 RID: 5608 RVA: 0x000B8F28 File Offset: 0x000B7128
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CustomSenpai");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SenpaiEyeColor");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SenpaiEyeWear");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SenpaiFacialHair");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SenpaiHairColor");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SenpaiHairStyle");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SenpaiSkinColor");
	}

	// Token: 0x04001D90 RID: 7568
	private const string Str_CustomSenpai = "CustomSenpai";

	// Token: 0x04001D91 RID: 7569
	private const string Str_SenpaiEyeColor = "SenpaiEyeColor";

	// Token: 0x04001D92 RID: 7570
	private const string Str_SenpaiEyeWear = "SenpaiEyeWear";

	// Token: 0x04001D93 RID: 7571
	private const string Str_SenpaiFacialHair = "SenpaiFacialHair";

	// Token: 0x04001D94 RID: 7572
	private const string Str_SenpaiHairColor = "SenpaiHairColor";

	// Token: 0x04001D95 RID: 7573
	private const string Str_SenpaiHairStyle = "SenpaiHairStyle";

	// Token: 0x04001D96 RID: 7574
	private const string Str_SenpaiSkinColor = "SenpaiSkinColor";
}
