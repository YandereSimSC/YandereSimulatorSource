using System;

// Token: 0x020002C1 RID: 705
public static class EventGlobals
{
	// Token: 0x17000397 RID: 919
	// (get) Token: 0x060014E8 RID: 5352 RVA: 0x000B6004 File Offset: 0x000B4204
	// (set) Token: 0x060014E9 RID: 5353 RVA: 0x000B6024 File Offset: 0x000B4224
	public static bool BefriendConversation
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_BefriendConversation");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_BefriendConversation", value);
		}
	}

	// Token: 0x17000398 RID: 920
	// (get) Token: 0x060014EA RID: 5354 RVA: 0x000B6045 File Offset: 0x000B4245
	// (set) Token: 0x060014EB RID: 5355 RVA: 0x000B6065 File Offset: 0x000B4265
	public static bool OsanaEvent1
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_OsanaEvent1");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_OsanaEvent1", value);
		}
	}

	// Token: 0x17000399 RID: 921
	// (get) Token: 0x060014EC RID: 5356 RVA: 0x000B6086 File Offset: 0x000B4286
	// (set) Token: 0x060014ED RID: 5357 RVA: 0x000B60A6 File Offset: 0x000B42A6
	public static bool OsanaEvent2
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_OsanaEvent2");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_OsanaEvent2", value);
		}
	}

	// Token: 0x1700039A RID: 922
	// (get) Token: 0x060014EE RID: 5358 RVA: 0x000B60C7 File Offset: 0x000B42C7
	// (set) Token: 0x060014EF RID: 5359 RVA: 0x000B60E7 File Offset: 0x000B42E7
	public static bool Event1
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_Event1");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_Event1", value);
		}
	}

	// Token: 0x1700039B RID: 923
	// (get) Token: 0x060014F0 RID: 5360 RVA: 0x000B6108 File Offset: 0x000B4308
	// (set) Token: 0x060014F1 RID: 5361 RVA: 0x000B6128 File Offset: 0x000B4328
	public static bool Event2
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_Event2");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_Event2", value);
		}
	}

	// Token: 0x1700039C RID: 924
	// (get) Token: 0x060014F2 RID: 5362 RVA: 0x000B6149 File Offset: 0x000B4349
	// (set) Token: 0x060014F3 RID: 5363 RVA: 0x000B6169 File Offset: 0x000B4369
	public static bool KidnapConversation
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_KidnapConversation");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_KidnapConversation", value);
		}
	}

	// Token: 0x1700039D RID: 925
	// (get) Token: 0x060014F4 RID: 5364 RVA: 0x000B618A File Offset: 0x000B438A
	// (set) Token: 0x060014F5 RID: 5365 RVA: 0x000B61AA File Offset: 0x000B43AA
	public static bool LivingRoom
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_LivingRoom");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_LivingRoom", value);
		}
	}

	// Token: 0x060014F6 RID: 5366 RVA: 0x000B61CC File Offset: 0x000B43CC
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_BefriendConversation");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_OsanaEvent1");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_OsanaEvent2");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Event1");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Event2");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_KidnapConversation");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_LivingRoom");
	}

	// Token: 0x04001D24 RID: 7460
	private const string Str_BefriendConversation = "BefriendConversation";

	// Token: 0x04001D25 RID: 7461
	private const string Str_Event1 = "Event1";

	// Token: 0x04001D26 RID: 7462
	private const string Str_Event2 = "Event2";

	// Token: 0x04001D27 RID: 7463
	private const string Str_OsanaEvent1 = "OsanaEvent1";

	// Token: 0x04001D28 RID: 7464
	private const string Str_OsanaEvent2 = "OsanaEvent2";

	// Token: 0x04001D29 RID: 7465
	private const string Str_KidnapConversation = "KidnapConversation";

	// Token: 0x04001D2A RID: 7466
	private const string Str_LivingRoom = "LivingRoom";
}
