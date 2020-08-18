using System;
using UnityEngine;

// Token: 0x020002CA RID: 714
public static class SchoolGlobals
{
	// Token: 0x060015C3 RID: 5571 RVA: 0x000B8890 File Offset: 0x000B6A90
	public static bool GetDemonActive(int demonID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_DemonActive_",
			demonID.ToString()
		}));
	}

	// Token: 0x060015C4 RID: 5572 RVA: 0x000B88CC File Offset: 0x000B6ACC
	public static void SetDemonActive(int demonID, bool value)
	{
		string text = demonID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_DemonActive_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_DemonActive_",
			text
		}), value);
	}

	// Token: 0x060015C5 RID: 5573 RVA: 0x000B8932 File Offset: 0x000B6B32
	public static int[] KeysOfDemonActive()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_DemonActive_");
	}

	// Token: 0x060015C6 RID: 5574 RVA: 0x000B8952 File Offset: 0x000B6B52
	public static bool GetGardenGraveOccupied(int graveID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_GardenGraveOccupied_",
			graveID.ToString()
		}));
	}

	// Token: 0x060015C7 RID: 5575 RVA: 0x000B898C File Offset: 0x000B6B8C
	public static void SetGardenGraveOccupied(int graveID, bool value)
	{
		string text = graveID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_GardenGraveOccupied_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_GardenGraveOccupied_",
			text
		}), value);
	}

	// Token: 0x060015C8 RID: 5576 RVA: 0x000B89F2 File Offset: 0x000B6BF2
	public static int[] KeysOfGardenGraveOccupied()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_GardenGraveOccupied_");
	}

	// Token: 0x170003EA RID: 1002
	// (get) Token: 0x060015C9 RID: 5577 RVA: 0x000B8A12 File Offset: 0x000B6C12
	// (set) Token: 0x060015CA RID: 5578 RVA: 0x000B8A32 File Offset: 0x000B6C32
	public static int KidnapVictim
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_KidnapVictim");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_KidnapVictim", value);
		}
	}

	// Token: 0x170003EB RID: 1003
	// (get) Token: 0x060015CB RID: 5579 RVA: 0x000B8A53 File Offset: 0x000B6C53
	// (set) Token: 0x060015CC RID: 5580 RVA: 0x000B8A73 File Offset: 0x000B6C73
	public static int Population
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Population");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Population", value);
		}
	}

	// Token: 0x170003EC RID: 1004
	// (get) Token: 0x060015CD RID: 5581 RVA: 0x000B8A94 File Offset: 0x000B6C94
	// (set) Token: 0x060015CE RID: 5582 RVA: 0x000B8AB4 File Offset: 0x000B6CB4
	public static bool RoofFence
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_RoofFence");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_RoofFence", value);
		}
	}

	// Token: 0x170003ED RID: 1005
	// (get) Token: 0x060015CF RID: 5583 RVA: 0x000B8AD5 File Offset: 0x000B6CD5
	// (set) Token: 0x060015D0 RID: 5584 RVA: 0x000B8AF5 File Offset: 0x000B6CF5
	public static float SchoolAtmosphere
	{
		get
		{
			return PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_SchoolAtmosphere");
		}
		set
		{
			PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_SchoolAtmosphere", value);
		}
	}

	// Token: 0x170003EE RID: 1006
	// (get) Token: 0x060015D1 RID: 5585 RVA: 0x000B8B16 File Offset: 0x000B6D16
	// (set) Token: 0x060015D2 RID: 5586 RVA: 0x000B8B36 File Offset: 0x000B6D36
	public static bool SchoolAtmosphereSet
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_SchoolAtmosphereSet");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_SchoolAtmosphereSet", value);
		}
	}

	// Token: 0x170003EF RID: 1007
	// (get) Token: 0x060015D3 RID: 5587 RVA: 0x000B8B57 File Offset: 0x000B6D57
	// (set) Token: 0x060015D4 RID: 5588 RVA: 0x000B8B77 File Offset: 0x000B6D77
	public static bool ReactedToGameLeader
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_ReactedToGameLeader");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_ReactedToGameLeader", value);
		}
	}

	// Token: 0x170003F0 RID: 1008
	// (get) Token: 0x060015D5 RID: 5589 RVA: 0x000B8B98 File Offset: 0x000B6D98
	// (set) Token: 0x060015D6 RID: 5590 RVA: 0x000B8BB8 File Offset: 0x000B6DB8
	public static bool HighSecurity
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_HighSecurity");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_HighSecurity", value);
		}
	}

	// Token: 0x170003F1 RID: 1009
	// (get) Token: 0x060015D7 RID: 5591 RVA: 0x000B8BD9 File Offset: 0x000B6DD9
	// (set) Token: 0x060015D8 RID: 5592 RVA: 0x000B8BF9 File Offset: 0x000B6DF9
	public static bool SCP
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_SCP");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_SCP", value);
		}
	}

	// Token: 0x060015D9 RID: 5593 RVA: 0x000B8C1C File Offset: 0x000B6E1C
	public static void DeleteAll()
	{
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_DemonActive_", SchoolGlobals.KeysOfDemonActive());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_GardenGraveOccupied_", SchoolGlobals.KeysOfGardenGraveOccupied());
		Globals.Delete("Profile_" + GameGlobals.Profile + "_KidnapVictim");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Population");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_RoofFence");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SchoolAtmosphere");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SchoolAtmosphereSet");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_ReactedToGameLeader");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_HighSecurity");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SCP");
	}

	// Token: 0x04001D86 RID: 7558
	private const string Str_DemonActive = "DemonActive_";

	// Token: 0x04001D87 RID: 7559
	private const string Str_GardenGraveOccupied = "GardenGraveOccupied_";

	// Token: 0x04001D88 RID: 7560
	private const string Str_KidnapVictim = "KidnapVictim";

	// Token: 0x04001D89 RID: 7561
	private const string Str_Population = "Population";

	// Token: 0x04001D8A RID: 7562
	private const string Str_RoofFence = "RoofFence";

	// Token: 0x04001D8B RID: 7563
	private const string Str_SchoolAtmosphere = "SchoolAtmosphere";

	// Token: 0x04001D8C RID: 7564
	private const string Str_SchoolAtmosphereSet = "SchoolAtmosphereSet";

	// Token: 0x04001D8D RID: 7565
	private const string Str_ReactedToGameLeader = "ReactedToGameLeader";

	// Token: 0x04001D8E RID: 7566
	private const string Str_SCP = "SCP";

	// Token: 0x04001D8F RID: 7567
	private const string Str_HighSecurity = "HighSecurity";
}
