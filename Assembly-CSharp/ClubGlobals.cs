using System;

// Token: 0x020002BC RID: 700
public static class ClubGlobals
{
	// Token: 0x1700038C RID: 908
	// (get) Token: 0x06001493 RID: 5267 RVA: 0x000B486F File Offset: 0x000B2A6F
	// (set) Token: 0x06001494 RID: 5268 RVA: 0x000B488F File Offset: 0x000B2A8F
	public static ClubType Club
	{
		get
		{
			return GlobalsHelper.GetEnum<ClubType>("Profile_" + GameGlobals.Profile + "_Club");
		}
		set
		{
			GlobalsHelper.SetEnum<ClubType>("Profile_" + GameGlobals.Profile + "_Club", value);
		}
	}

	// Token: 0x06001495 RID: 5269 RVA: 0x000B48B0 File Offset: 0x000B2AB0
	public static bool GetClubClosed(ClubType clubID)
	{
		object[] array = new object[4];
		array[0] = "Profile_";
		array[1] = GameGlobals.Profile;
		array[2] = "_ClubClosed_";
		int num = 3;
		int num2 = (int)clubID;
		array[num] = num2.ToString();
		return GlobalsHelper.GetBool(string.Concat(array));
	}

	// Token: 0x06001496 RID: 5270 RVA: 0x000B48F8 File Offset: 0x000B2AF8
	public static void SetClubClosed(ClubType clubID, bool value)
	{
		int num = (int)clubID;
		string text = num.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_ClubClosed_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_ClubClosed_",
			text
		}), value);
	}

	// Token: 0x06001497 RID: 5271 RVA: 0x000B4960 File Offset: 0x000B2B60
	public static ClubType[] KeysOfClubClosed()
	{
		return KeysHelper.GetEnumKeys<ClubType>("Profile_" + GameGlobals.Profile + "_ClubClosed_");
	}

	// Token: 0x06001498 RID: 5272 RVA: 0x000B4980 File Offset: 0x000B2B80
	public static bool GetClubKicked(ClubType clubID)
	{
		object[] array = new object[4];
		array[0] = "Profile_";
		array[1] = GameGlobals.Profile;
		array[2] = "_ClubKicked_";
		int num = 3;
		int num2 = (int)clubID;
		array[num] = num2.ToString();
		return GlobalsHelper.GetBool(string.Concat(array));
	}

	// Token: 0x06001499 RID: 5273 RVA: 0x000B49C8 File Offset: 0x000B2BC8
	public static void SetClubKicked(ClubType clubID, bool value)
	{
		int num = (int)clubID;
		string text = num.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_ClubKicked_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_ClubKicked_",
			text
		}), value);
	}

	// Token: 0x0600149A RID: 5274 RVA: 0x000B4A30 File Offset: 0x000B2C30
	public static ClubType[] KeysOfClubKicked()
	{
		return KeysHelper.GetEnumKeys<ClubType>("Profile_" + GameGlobals.Profile + "_ClubKicked_");
	}

	// Token: 0x0600149B RID: 5275 RVA: 0x000B4A50 File Offset: 0x000B2C50
	public static bool GetQuitClub(ClubType clubID)
	{
		object[] array = new object[4];
		array[0] = "Profile_";
		array[1] = GameGlobals.Profile;
		array[2] = "_QuitClub_";
		int num = 3;
		int num2 = (int)clubID;
		array[num] = num2.ToString();
		return GlobalsHelper.GetBool(string.Concat(array));
	}

	// Token: 0x0600149C RID: 5276 RVA: 0x000B4A98 File Offset: 0x000B2C98
	public static void SetQuitClub(ClubType clubID, bool value)
	{
		int num = (int)clubID;
		string text = num.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_QuitClub_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_QuitClub_",
			text
		}), value);
	}

	// Token: 0x0600149D RID: 5277 RVA: 0x000B4B00 File Offset: 0x000B2D00
	public static ClubType[] KeysOfQuitClub()
	{
		return KeysHelper.GetEnumKeys<ClubType>("Profile_" + GameGlobals.Profile + "_QuitClub_");
	}

	// Token: 0x0600149E RID: 5278 RVA: 0x000B4B20 File Offset: 0x000B2D20
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Club");
		foreach (ClubType clubType in ClubGlobals.KeysOfClubClosed())
		{
			object[] array2 = new object[4];
			array2[0] = "Profile_";
			array2[1] = GameGlobals.Profile;
			array2[2] = "_ClubClosed_";
			int num = 3;
			int num2 = (int)clubType;
			array2[num] = num2.ToString();
			Globals.Delete(string.Concat(array2));
		}
		foreach (ClubType clubType2 in ClubGlobals.KeysOfClubKicked())
		{
			object[] array3 = new object[4];
			array3[0] = "Profile_";
			array3[1] = GameGlobals.Profile;
			array3[2] = "_ClubKicked_";
			int num3 = 3;
			int num2 = (int)clubType2;
			array3[num3] = num2.ToString();
			Globals.Delete(string.Concat(array3));
		}
		foreach (ClubType clubType3 in ClubGlobals.KeysOfQuitClub())
		{
			object[] array4 = new object[4];
			array4[0] = "Profile_";
			array4[1] = GameGlobals.Profile;
			array4[2] = "_QuitClub_";
			int num4 = 3;
			int num2 = (int)clubType3;
			array4[num4] = num2.ToString();
			Globals.Delete(string.Concat(array4));
		}
		KeysHelper.Delete("Profile_" + GameGlobals.Profile + "_ClubClosed_");
		KeysHelper.Delete("Profile_" + GameGlobals.Profile + "_ClubKicked_");
		KeysHelper.Delete("Profile_" + GameGlobals.Profile + "_QuitClub_");
	}

	// Token: 0x04001D05 RID: 7429
	private const string Str_Club = "Club";

	// Token: 0x04001D06 RID: 7430
	private const string Str_ClubClosed = "ClubClosed_";

	// Token: 0x04001D07 RID: 7431
	private const string Str_ClubKicked = "ClubKicked_";

	// Token: 0x04001D08 RID: 7432
	private const string Str_QuitClub = "QuitClub_";
}
