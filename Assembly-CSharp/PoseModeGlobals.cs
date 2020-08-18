using System;
using UnityEngine;

// Token: 0x020002C7 RID: 711
public static class PoseModeGlobals
{
	// Token: 0x170003E4 RID: 996
	// (get) Token: 0x060015A5 RID: 5541 RVA: 0x000B81C9 File Offset: 0x000B63C9
	// (set) Token: 0x060015A6 RID: 5542 RVA: 0x000B81E9 File Offset: 0x000B63E9
	public static Vector3 PosePosition
	{
		get
		{
			return GlobalsHelper.GetVector3("Profile_" + GameGlobals.Profile + "_PosePosition");
		}
		set
		{
			GlobalsHelper.SetVector3("Profile_" + GameGlobals.Profile + "_PosePosition", value);
		}
	}

	// Token: 0x170003E5 RID: 997
	// (get) Token: 0x060015A7 RID: 5543 RVA: 0x000B820A File Offset: 0x000B640A
	// (set) Token: 0x060015A8 RID: 5544 RVA: 0x000B822A File Offset: 0x000B642A
	public static Vector3 PoseRotation
	{
		get
		{
			return GlobalsHelper.GetVector3("Profile_" + GameGlobals.Profile + "_PoseRotation");
		}
		set
		{
			GlobalsHelper.SetVector3("Profile_" + GameGlobals.Profile + "_PoseRotation", value);
		}
	}

	// Token: 0x170003E6 RID: 998
	// (get) Token: 0x060015A9 RID: 5545 RVA: 0x000B824B File Offset: 0x000B644B
	// (set) Token: 0x060015AA RID: 5546 RVA: 0x000B826B File Offset: 0x000B646B
	public static Vector3 PoseScale
	{
		get
		{
			return GlobalsHelper.GetVector3("Profile_" + GameGlobals.Profile + "_PoseScale");
		}
		set
		{
			GlobalsHelper.SetVector3("Profile_" + GameGlobals.Profile + "_PoseScale", value);
		}
	}

	// Token: 0x060015AB RID: 5547 RVA: 0x000B828C File Offset: 0x000B648C
	public static void DeleteAll()
	{
		GlobalsHelper.DeleteVector3("Profile_" + GameGlobals.Profile + "_PosePosition");
		GlobalsHelper.DeleteVector3("Profile_" + GameGlobals.Profile + "_PoseRotation");
		GlobalsHelper.DeleteVector3("Profile_" + GameGlobals.Profile + "_PoseScale");
	}

	// Token: 0x04001D7B RID: 7547
	private const string Str_PosePosition = "PosePosition";

	// Token: 0x04001D7C RID: 7548
	private const string Str_PoseRotation = "PoseRotation";

	// Token: 0x04001D7D RID: 7549
	private const string Str_PoseScale = "PoseScale";
}
