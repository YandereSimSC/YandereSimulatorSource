using System;
using UnityEngine;

// Token: 0x020002CC RID: 716
public static class StudentGlobals
{
	// Token: 0x170003F9 RID: 1017
	// (get) Token: 0x060015E9 RID: 5609 RVA: 0x000B9007 File Offset: 0x000B7207
	// (set) Token: 0x060015EA RID: 5610 RVA: 0x000B9027 File Offset: 0x000B7227
	public static bool CustomSuitor
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_CustomSuitor");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_CustomSuitor", value);
		}
	}

	// Token: 0x170003FA RID: 1018
	// (get) Token: 0x060015EB RID: 5611 RVA: 0x000B9048 File Offset: 0x000B7248
	// (set) Token: 0x060015EC RID: 5612 RVA: 0x000B9068 File Offset: 0x000B7268
	public static int CustomSuitorAccessory
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_CustomSuitorAccessory");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CustomSuitorAccessory", value);
		}
	}

	// Token: 0x170003FB RID: 1019
	// (get) Token: 0x060015ED RID: 5613 RVA: 0x000B9089 File Offset: 0x000B7289
	// (set) Token: 0x060015EE RID: 5614 RVA: 0x000B90A9 File Offset: 0x000B72A9
	public static bool CustomSuitorBlonde
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_CustomSuitorBlonde");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_CustomSuitorBlonde", value);
		}
	}

	// Token: 0x170003FC RID: 1020
	// (get) Token: 0x060015EF RID: 5615 RVA: 0x000B90CA File Offset: 0x000B72CA
	// (set) Token: 0x060015F0 RID: 5616 RVA: 0x000B90EA File Offset: 0x000B72EA
	public static bool CustomSuitorBlack
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_CustomSuitorBlack");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_CustomSuitorBlack", value);
		}
	}

	// Token: 0x170003FD RID: 1021
	// (get) Token: 0x060015F1 RID: 5617 RVA: 0x000B910B File Offset: 0x000B730B
	// (set) Token: 0x060015F2 RID: 5618 RVA: 0x000B912B File Offset: 0x000B732B
	public static int CustomSuitorEyewear
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_CustomSuitorEyewear");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CustomSuitorEyewear", value);
		}
	}

	// Token: 0x170003FE RID: 1022
	// (get) Token: 0x060015F3 RID: 5619 RVA: 0x000B914C File Offset: 0x000B734C
	// (set) Token: 0x060015F4 RID: 5620 RVA: 0x000B916C File Offset: 0x000B736C
	public static int CustomSuitorHair
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_CustomSuitorHair");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CustomSuitorHair", value);
		}
	}

	// Token: 0x170003FF RID: 1023
	// (get) Token: 0x060015F5 RID: 5621 RVA: 0x000B918D File Offset: 0x000B738D
	// (set) Token: 0x060015F6 RID: 5622 RVA: 0x000B91AD File Offset: 0x000B73AD
	public static int CustomSuitorJewelry
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_CustomSuitorJewelry");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CustomSuitorJewelry", value);
		}
	}

	// Token: 0x17000400 RID: 1024
	// (get) Token: 0x060015F7 RID: 5623 RVA: 0x000B91CE File Offset: 0x000B73CE
	// (set) Token: 0x060015F8 RID: 5624 RVA: 0x000B91EE File Offset: 0x000B73EE
	public static bool CustomSuitorTan
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_CustomSuitorTan");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_CustomSuitorTan", value);
		}
	}

	// Token: 0x17000401 RID: 1025
	// (get) Token: 0x060015F9 RID: 5625 RVA: 0x000B920F File Offset: 0x000B740F
	// (set) Token: 0x060015FA RID: 5626 RVA: 0x000B922F File Offset: 0x000B742F
	public static int ExpelProgress
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_ExpelProgress");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_ExpelProgress", value);
		}
	}

	// Token: 0x17000402 RID: 1026
	// (get) Token: 0x060015FB RID: 5627 RVA: 0x000B9250 File Offset: 0x000B7450
	// (set) Token: 0x060015FC RID: 5628 RVA: 0x000B9270 File Offset: 0x000B7470
	public static int FemaleUniform
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_FemaleUniform");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_FemaleUniform", value);
		}
	}

	// Token: 0x17000403 RID: 1027
	// (get) Token: 0x060015FD RID: 5629 RVA: 0x000B9291 File Offset: 0x000B7491
	// (set) Token: 0x060015FE RID: 5630 RVA: 0x000B92B1 File Offset: 0x000B74B1
	public static int MaleUniform
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_MaleUniform");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_MaleUniform", value);
		}
	}

	// Token: 0x17000404 RID: 1028
	// (get) Token: 0x060015FF RID: 5631 RVA: 0x000B92D2 File Offset: 0x000B74D2
	// (set) Token: 0x06001600 RID: 5632 RVA: 0x000B92F2 File Offset: 0x000B74F2
	public static int MemorialStudents
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_MemorialStudents");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_MemorialStudents", value);
		}
	}

	// Token: 0x17000405 RID: 1029
	// (get) Token: 0x06001601 RID: 5633 RVA: 0x000B9313 File Offset: 0x000B7513
	// (set) Token: 0x06001602 RID: 5634 RVA: 0x000B9333 File Offset: 0x000B7533
	public static int MemorialStudent1
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent1");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent1", value);
		}
	}

	// Token: 0x17000406 RID: 1030
	// (get) Token: 0x06001603 RID: 5635 RVA: 0x000B9354 File Offset: 0x000B7554
	// (set) Token: 0x06001604 RID: 5636 RVA: 0x000B9374 File Offset: 0x000B7574
	public static int MemorialStudent2
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent2");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent2", value);
		}
	}

	// Token: 0x17000407 RID: 1031
	// (get) Token: 0x06001605 RID: 5637 RVA: 0x000B9395 File Offset: 0x000B7595
	// (set) Token: 0x06001606 RID: 5638 RVA: 0x000B93B5 File Offset: 0x000B75B5
	public static int MemorialStudent3
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent3");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent3", value);
		}
	}

	// Token: 0x17000408 RID: 1032
	// (get) Token: 0x06001607 RID: 5639 RVA: 0x000B93D6 File Offset: 0x000B75D6
	// (set) Token: 0x06001608 RID: 5640 RVA: 0x000B93F6 File Offset: 0x000B75F6
	public static int MemorialStudent4
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent4");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent4", value);
		}
	}

	// Token: 0x17000409 RID: 1033
	// (get) Token: 0x06001609 RID: 5641 RVA: 0x000B9417 File Offset: 0x000B7617
	// (set) Token: 0x0600160A RID: 5642 RVA: 0x000B9437 File Offset: 0x000B7637
	public static int MemorialStudent5
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent5");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent5", value);
		}
	}

	// Token: 0x1700040A RID: 1034
	// (get) Token: 0x0600160B RID: 5643 RVA: 0x000B9458 File Offset: 0x000B7658
	// (set) Token: 0x0600160C RID: 5644 RVA: 0x000B9478 File Offset: 0x000B7678
	public static int MemorialStudent6
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent6");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent6", value);
		}
	}

	// Token: 0x1700040B RID: 1035
	// (get) Token: 0x0600160D RID: 5645 RVA: 0x000B9499 File Offset: 0x000B7699
	// (set) Token: 0x0600160E RID: 5646 RVA: 0x000B94B9 File Offset: 0x000B76B9
	public static int MemorialStudent7
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent7");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent7", value);
		}
	}

	// Token: 0x1700040C RID: 1036
	// (get) Token: 0x0600160F RID: 5647 RVA: 0x000B94DA File Offset: 0x000B76DA
	// (set) Token: 0x06001610 RID: 5648 RVA: 0x000B94FA File Offset: 0x000B76FA
	public static int MemorialStudent8
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent8");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent8", value);
		}
	}

	// Token: 0x1700040D RID: 1037
	// (get) Token: 0x06001611 RID: 5649 RVA: 0x000B951B File Offset: 0x000B771B
	// (set) Token: 0x06001612 RID: 5650 RVA: 0x000B953B File Offset: 0x000B773B
	public static int MemorialStudent9
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent9");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent9", value);
		}
	}

	// Token: 0x06001613 RID: 5651 RVA: 0x000B955C File Offset: 0x000B775C
	public static string GetStudentAccessory(int studentID)
	{
		return PlayerPrefs.GetString(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentAccessory_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001614 RID: 5652 RVA: 0x000B9598 File Offset: 0x000B7798
	public static void SetStudentAccessory(int studentID, string value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentAccessory_", text);
		PlayerPrefs.SetString(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentAccessory_",
			text
		}), value);
	}

	// Token: 0x06001615 RID: 5653 RVA: 0x000B95FE File Offset: 0x000B77FE
	public static int[] KeysOfStudentAccessory()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentAccessory_");
	}

	// Token: 0x06001616 RID: 5654 RVA: 0x000B961E File Offset: 0x000B781E
	public static bool GetStudentArrested(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentArrested_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001617 RID: 5655 RVA: 0x000B9658 File Offset: 0x000B7858
	public static void SetStudentArrested(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentArrested_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentArrested_",
			text
		}), value);
	}

	// Token: 0x06001618 RID: 5656 RVA: 0x000B96BE File Offset: 0x000B78BE
	public static int[] KeysOfStudentArrested()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentArrested_");
	}

	// Token: 0x06001619 RID: 5657 RVA: 0x000B96DE File Offset: 0x000B78DE
	public static bool GetStudentBroken(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentBroken_",
			studentID.ToString()
		}));
	}

	// Token: 0x0600161A RID: 5658 RVA: 0x000B9718 File Offset: 0x000B7918
	public static void SetStudentBroken(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentBroken_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentBroken_",
			text
		}), value);
	}

	// Token: 0x0600161B RID: 5659 RVA: 0x000B977E File Offset: 0x000B797E
	public static int[] KeysOfStudentBroken()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentBroken_");
	}

	// Token: 0x0600161C RID: 5660 RVA: 0x000B979E File Offset: 0x000B799E
	public static float GetStudentBustSize(int studentID)
	{
		return PlayerPrefs.GetFloat(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentBustSize_",
			studentID.ToString()
		}));
	}

	// Token: 0x0600161D RID: 5661 RVA: 0x000B97D8 File Offset: 0x000B79D8
	public static void SetStudentBustSize(int studentID, float value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentBustSize_", text);
		PlayerPrefs.SetFloat(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentBustSize_",
			text
		}), value);
	}

	// Token: 0x0600161E RID: 5662 RVA: 0x000B983E File Offset: 0x000B7A3E
	public static int[] KeysOfStudentBustSize()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentBustSize_");
	}

	// Token: 0x0600161F RID: 5663 RVA: 0x000B985E File Offset: 0x000B7A5E
	public static Color GetStudentColor(int studentID)
	{
		return GlobalsHelper.GetColor(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentColor_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001620 RID: 5664 RVA: 0x000B9898 File Offset: 0x000B7A98
	public static void SetStudentColor(int studentID, Color value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentColor_", text);
		GlobalsHelper.SetColor(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentColor_",
			text
		}), value);
	}

	// Token: 0x06001621 RID: 5665 RVA: 0x000B98FE File Offset: 0x000B7AFE
	public static int[] KeysOfStudentColor()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentColor_");
	}

	// Token: 0x06001622 RID: 5666 RVA: 0x000B991E File Offset: 0x000B7B1E
	public static bool GetStudentDead(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentDead_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001623 RID: 5667 RVA: 0x000B9958 File Offset: 0x000B7B58
	public static void SetStudentDead(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentDead_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentDead_",
			text
		}), value);
	}

	// Token: 0x06001624 RID: 5668 RVA: 0x000B99BE File Offset: 0x000B7BBE
	public static int[] KeysOfStudentDead()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentDead_");
	}

	// Token: 0x06001625 RID: 5669 RVA: 0x000B99DE File Offset: 0x000B7BDE
	public static bool GetStudentDying(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentDying_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001626 RID: 5670 RVA: 0x000B9A18 File Offset: 0x000B7C18
	public static void SetStudentDying(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentDying_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentDying_",
			text
		}), value);
	}

	// Token: 0x06001627 RID: 5671 RVA: 0x000B9A7E File Offset: 0x000B7C7E
	public static int[] KeysOfStudentDying()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentDying_");
	}

	// Token: 0x06001628 RID: 5672 RVA: 0x000B9A9E File Offset: 0x000B7C9E
	public static bool GetStudentExpelled(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentExpelled_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001629 RID: 5673 RVA: 0x000B9AD8 File Offset: 0x000B7CD8
	public static void SetStudentExpelled(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentExpelled_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentExpelled_",
			text
		}), value);
	}

	// Token: 0x0600162A RID: 5674 RVA: 0x000B9B3E File Offset: 0x000B7D3E
	public static int[] KeysOfStudentExpelled()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentExpelled_");
	}

	// Token: 0x0600162B RID: 5675 RVA: 0x000B9B5E File Offset: 0x000B7D5E
	public static bool GetStudentExposed(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentExposed_",
			studentID.ToString()
		}));
	}

	// Token: 0x0600162C RID: 5676 RVA: 0x000B9B98 File Offset: 0x000B7D98
	public static void SetStudentExposed(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentExposed_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentExposed_",
			text
		}), value);
	}

	// Token: 0x0600162D RID: 5677 RVA: 0x000B9BFE File Offset: 0x000B7DFE
	public static int[] KeysOfStudentExposed()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentExposed_");
	}

	// Token: 0x0600162E RID: 5678 RVA: 0x000B9C1E File Offset: 0x000B7E1E
	public static Color GetStudentEyeColor(int studentID)
	{
		return GlobalsHelper.GetColor(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentEyeColor_",
			studentID.ToString()
		}));
	}

	// Token: 0x0600162F RID: 5679 RVA: 0x000B9C58 File Offset: 0x000B7E58
	public static void SetStudentEyeColor(int studentID, Color value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentEyeColor_", text);
		GlobalsHelper.SetColor(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentEyeColor_",
			text
		}), value);
	}

	// Token: 0x06001630 RID: 5680 RVA: 0x000B9CBE File Offset: 0x000B7EBE
	public static int[] KeysOfStudentEyeColor()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentEyeColor_");
	}

	// Token: 0x06001631 RID: 5681 RVA: 0x000B9CDE File Offset: 0x000B7EDE
	public static bool GetStudentGrudge(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentGrudge_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001632 RID: 5682 RVA: 0x000B9D18 File Offset: 0x000B7F18
	public static void SetStudentGrudge(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentGrudge_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentGrudge_",
			text
		}), value);
	}

	// Token: 0x06001633 RID: 5683 RVA: 0x000B9D7E File Offset: 0x000B7F7E
	public static int[] KeysOfStudentGrudge()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentGrudge_");
	}

	// Token: 0x06001634 RID: 5684 RVA: 0x000B9D9E File Offset: 0x000B7F9E
	public static string GetStudentHairstyle(int studentID)
	{
		return PlayerPrefs.GetString(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentHairstyle_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001635 RID: 5685 RVA: 0x000B9DD8 File Offset: 0x000B7FD8
	public static void SetStudentHairstyle(int studentID, string value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentHairstyle_", text);
		PlayerPrefs.SetString(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentHairstyle_",
			text
		}), value);
	}

	// Token: 0x06001636 RID: 5686 RVA: 0x000B9E3E File Offset: 0x000B803E
	public static int[] KeysOfStudentHairstyle()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentHairstyle_");
	}

	// Token: 0x06001637 RID: 5687 RVA: 0x000B9E5E File Offset: 0x000B805E
	public static bool GetStudentKidnapped(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentKidnapped_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001638 RID: 5688 RVA: 0x000B9E98 File Offset: 0x000B8098
	public static void SetStudentKidnapped(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentKidnapped_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentKidnapped_",
			text
		}), value);
	}

	// Token: 0x06001639 RID: 5689 RVA: 0x000B9EFE File Offset: 0x000B80FE
	public static int[] KeysOfStudentKidnapped()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentKidnapped_");
	}

	// Token: 0x0600163A RID: 5690 RVA: 0x000B9F1E File Offset: 0x000B811E
	public static bool GetStudentMissing(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentMissing_",
			studentID.ToString()
		}));
	}

	// Token: 0x0600163B RID: 5691 RVA: 0x000B9F58 File Offset: 0x000B8158
	public static void SetStudentMissing(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentMissing_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentMissing_",
			text
		}), value);
	}

	// Token: 0x0600163C RID: 5692 RVA: 0x000B9FBE File Offset: 0x000B81BE
	public static int[] KeysOfStudentMissing()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentMissing_");
	}

	// Token: 0x0600163D RID: 5693 RVA: 0x000B9FDE File Offset: 0x000B81DE
	public static string GetStudentName(int studentID)
	{
		return PlayerPrefs.GetString(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentName_",
			studentID.ToString()
		}));
	}

	// Token: 0x0600163E RID: 5694 RVA: 0x000BA018 File Offset: 0x000B8218
	public static void SetStudentName(int studentID, string value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentName_", text);
		PlayerPrefs.SetString(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentName_",
			text
		}), value);
	}

	// Token: 0x0600163F RID: 5695 RVA: 0x000BA07E File Offset: 0x000B827E
	public static int[] KeysOfStudentName()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentName_");
	}

	// Token: 0x06001640 RID: 5696 RVA: 0x000BA09E File Offset: 0x000B829E
	public static bool GetStudentPhotographed(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentPhotographed_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001641 RID: 5697 RVA: 0x000BA0D8 File Offset: 0x000B82D8
	public static void SetStudentPhotographed(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentPhotographed_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentPhotographed_",
			text
		}), value);
	}

	// Token: 0x06001642 RID: 5698 RVA: 0x000BA13E File Offset: 0x000B833E
	public static int[] KeysOfStudentPhotographed()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentPhotographed_");
	}

	// Token: 0x06001643 RID: 5699 RVA: 0x000BA15E File Offset: 0x000B835E
	public static bool GetStudentPhoneStolen(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentPhoneStolen_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001644 RID: 5700 RVA: 0x000BA198 File Offset: 0x000B8398
	public static void SetStudentPhoneStolen(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentPhoneStolen_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentPhoneStolen_",
			text
		}), value);
	}

	// Token: 0x06001645 RID: 5701 RVA: 0x000BA1FE File Offset: 0x000B83FE
	public static int[] KeysOfStudentPhoneStolen()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentPhoneStolen_");
	}

	// Token: 0x06001646 RID: 5702 RVA: 0x000BA21E File Offset: 0x000B841E
	public static bool GetStudentReplaced(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentReplaced_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001647 RID: 5703 RVA: 0x000BA258 File Offset: 0x000B8458
	public static void SetStudentReplaced(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentReplaced_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentReplaced_",
			text
		}), value);
	}

	// Token: 0x06001648 RID: 5704 RVA: 0x000BA2BE File Offset: 0x000B84BE
	public static int[] KeysOfStudentReplaced()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentReplaced_");
	}

	// Token: 0x06001649 RID: 5705 RVA: 0x000BA2DE File Offset: 0x000B84DE
	public static int GetStudentReputation(int studentID)
	{
		return PlayerPrefs.GetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentReputation_",
			studentID.ToString()
		}));
	}

	// Token: 0x0600164A RID: 5706 RVA: 0x000BA318 File Offset: 0x000B8518
	public static void SetStudentReputation(int studentID, int value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentReputation_", text);
		PlayerPrefs.SetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentReputation_",
			text
		}), value);
	}

	// Token: 0x0600164B RID: 5707 RVA: 0x000BA37E File Offset: 0x000B857E
	public static int[] KeysOfStudentReputation()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentReputation_");
	}

	// Token: 0x0600164C RID: 5708 RVA: 0x000BA39E File Offset: 0x000B859E
	public static float GetStudentSanity(int studentID)
	{
		return PlayerPrefs.GetFloat(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentSanity_",
			studentID.ToString()
		}));
	}

	// Token: 0x0600164D RID: 5709 RVA: 0x000BA3D8 File Offset: 0x000B85D8
	public static void SetStudentSanity(int studentID, float value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentSanity_", text);
		PlayerPrefs.SetFloat(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentSanity_",
			text
		}), value);
	}

	// Token: 0x0600164E RID: 5710 RVA: 0x000BA43E File Offset: 0x000B863E
	public static int[] KeysOfStudentSanity()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentSanity_");
	}

	// Token: 0x0600164F RID: 5711 RVA: 0x000BA45E File Offset: 0x000B865E
	public static int GetStudentSlave()
	{
		return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_StudentSlave");
	}

	// Token: 0x06001650 RID: 5712 RVA: 0x000BA47E File Offset: 0x000B867E
	public static int GetStudentFragileSlave()
	{
		return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_StudentFragileSlave");
	}

	// Token: 0x06001651 RID: 5713 RVA: 0x000BA49E File Offset: 0x000B869E
	public static void SetStudentSlave(int studentID)
	{
		PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_StudentSlave", studentID);
	}

	// Token: 0x06001652 RID: 5714 RVA: 0x000BA4BF File Offset: 0x000B86BF
	public static void SetStudentFragileSlave(int studentID)
	{
		PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_StudentFragileSlave", studentID);
	}

	// Token: 0x06001653 RID: 5715 RVA: 0x000BA4E0 File Offset: 0x000B86E0
	public static int[] KeysOfStudentSlave()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentSlave");
	}

	// Token: 0x06001654 RID: 5716 RVA: 0x000BA500 File Offset: 0x000B8700
	public static int GetFragileTarget()
	{
		return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_FragileTarget");
	}

	// Token: 0x06001655 RID: 5717 RVA: 0x000BA520 File Offset: 0x000B8720
	public static void SetFragileTarget(int value)
	{
		PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_FragileTarget", value);
	}

	// Token: 0x06001656 RID: 5718 RVA: 0x000BA541 File Offset: 0x000B8741
	public static Vector3 GetReputationTriangle(int studentID)
	{
		return GlobalsHelper.GetVector3(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_Student_",
			studentID,
			"_ReputatonTriangle"
		}));
	}

	// Token: 0x06001657 RID: 5719 RVA: 0x000BA584 File Offset: 0x000B8784
	public static void SetReputationTriangle(int studentID, Vector3 triangle)
	{
		GlobalsHelper.SetVector3(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_Student_",
			studentID,
			"_ReputatonTriangle"
		}), triangle);
	}

	// Token: 0x06001658 RID: 5720 RVA: 0x000BA5D0 File Offset: 0x000B87D0
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CustomSuitor");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CustomSuitorAccessory");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CustomSuitorBlonde");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CustomSuitorBlack");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CustomSuitorEyewear");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CustomSuitorHair");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CustomSuitorJewelry");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CustomSuitorTan");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_ExpelProgress");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_FemaleUniform");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MaleUniform");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_StudentSlave");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_StudentFragileSlave");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_FragileTarget");
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentAccessory_", StudentGlobals.KeysOfStudentAccessory());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentArrested_", StudentGlobals.KeysOfStudentArrested());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentBroken_", StudentGlobals.KeysOfStudentBroken());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentBustSize_", StudentGlobals.KeysOfStudentBustSize());
		GlobalsHelper.DeleteColorCollection("Profile_" + GameGlobals.Profile + "_StudentColor_", StudentGlobals.KeysOfStudentColor());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentDead_", StudentGlobals.KeysOfStudentDead());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentDying_", StudentGlobals.KeysOfStudentDying());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentExpelled_", StudentGlobals.KeysOfStudentExpelled());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentExposed_", StudentGlobals.KeysOfStudentExposed());
		GlobalsHelper.DeleteColorCollection("Profile_" + GameGlobals.Profile + "_StudentEyeColor_", StudentGlobals.KeysOfStudentEyeColor());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentGrudge_", StudentGlobals.KeysOfStudentGrudge());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentHairstyle_", StudentGlobals.KeysOfStudentHairstyle());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentKidnapped_", StudentGlobals.KeysOfStudentKidnapped());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentMissing_", StudentGlobals.KeysOfStudentMissing());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentName_", StudentGlobals.KeysOfStudentName());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentPhotographed_", StudentGlobals.KeysOfStudentPhotographed());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentPhoneStolen_", StudentGlobals.KeysOfStudentPhoneStolen());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentReplaced_", StudentGlobals.KeysOfStudentReplaced());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentReputation_", StudentGlobals.KeysOfStudentReputation());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentSanity_", StudentGlobals.KeysOfStudentSanity());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentSlave", StudentGlobals.KeysOfStudentSlave());
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MemorialStudents");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MemorialStudent1");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MemorialStudent2");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MemorialStudent3");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MemorialStudent4");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MemorialStudent5");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MemorialStudent6");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MemorialStudent7");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MemorialStudent8");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MemorialStudent9");
	}

	// Token: 0x04001D97 RID: 7575
	private const string Str_CustomSuitor = "CustomSuitor";

	// Token: 0x04001D98 RID: 7576
	private const string Str_CustomSuitorAccessory = "CustomSuitorAccessory";

	// Token: 0x04001D99 RID: 7577
	private const string Str_CustomSuitorBlonde = "CustomSuitorBlonde";

	// Token: 0x04001D9A RID: 7578
	private const string Str_CustomSuitorBlack = "CustomSuitorBlack";

	// Token: 0x04001D9B RID: 7579
	private const string Str_CustomSuitorEyewear = "CustomSuitorEyewear";

	// Token: 0x04001D9C RID: 7580
	private const string Str_CustomSuitorHair = "CustomSuitorHair";

	// Token: 0x04001D9D RID: 7581
	private const string Str_CustomSuitorJewelry = "CustomSuitorJewelry";

	// Token: 0x04001D9E RID: 7582
	private const string Str_CustomSuitorTan = "CustomSuitorTan";

	// Token: 0x04001D9F RID: 7583
	private const string Str_ExpelProgress = "ExpelProgress";

	// Token: 0x04001DA0 RID: 7584
	private const string Str_FemaleUniform = "FemaleUniform";

	// Token: 0x04001DA1 RID: 7585
	private const string Str_MaleUniform = "MaleUniform";

	// Token: 0x04001DA2 RID: 7586
	private const string Str_StudentAccessory = "StudentAccessory_";

	// Token: 0x04001DA3 RID: 7587
	private const string Str_StudentArrested = "StudentArrested_";

	// Token: 0x04001DA4 RID: 7588
	private const string Str_StudentBroken = "StudentBroken_";

	// Token: 0x04001DA5 RID: 7589
	private const string Str_StudentBustSize = "StudentBustSize_";

	// Token: 0x04001DA6 RID: 7590
	private const string Str_StudentColor = "StudentColor_";

	// Token: 0x04001DA7 RID: 7591
	private const string Str_StudentDead = "StudentDead_";

	// Token: 0x04001DA8 RID: 7592
	private const string Str_StudentDying = "StudentDying_";

	// Token: 0x04001DA9 RID: 7593
	private const string Str_StudentExpelled = "StudentExpelled_";

	// Token: 0x04001DAA RID: 7594
	private const string Str_StudentExposed = "StudentExposed_";

	// Token: 0x04001DAB RID: 7595
	private const string Str_StudentEyeColor = "StudentEyeColor_";

	// Token: 0x04001DAC RID: 7596
	private const string Str_StudentGrudge = "StudentGrudge_";

	// Token: 0x04001DAD RID: 7597
	private const string Str_StudentHairstyle = "StudentHairstyle_";

	// Token: 0x04001DAE RID: 7598
	private const string Str_StudentKidnapped = "StudentKidnapped_";

	// Token: 0x04001DAF RID: 7599
	private const string Str_StudentMissing = "StudentMissing_";

	// Token: 0x04001DB0 RID: 7600
	private const string Str_StudentName = "StudentName_";

	// Token: 0x04001DB1 RID: 7601
	private const string Str_StudentPhotographed = "StudentPhotographed_";

	// Token: 0x04001DB2 RID: 7602
	private const string Str_StudentPhoneStolen = "StudentPhoneStolen_";

	// Token: 0x04001DB3 RID: 7603
	private const string Str_StudentReplaced = "StudentReplaced_";

	// Token: 0x04001DB4 RID: 7604
	private const string Str_StudentReputation = "StudentReputation_";

	// Token: 0x04001DB5 RID: 7605
	private const string Str_StudentSanity = "StudentSanity_";

	// Token: 0x04001DB6 RID: 7606
	private const string Str_StudentSlave = "StudentSlave";

	// Token: 0x04001DB7 RID: 7607
	private const string Str_StudentFragileSlave = "StudentFragileSlave";

	// Token: 0x04001DB8 RID: 7608
	private const string Str_FragileTarget = "FragileTarget";

	// Token: 0x04001DB9 RID: 7609
	private const string Str_ReputationTriangle = "ReputatonTriangle";

	// Token: 0x04001DBA RID: 7610
	private const string Str_MemorialStudents = "MemorialStudents";

	// Token: 0x04001DBB RID: 7611
	private const string Str_MemorialStudent1 = "MemorialStudent1";

	// Token: 0x04001DBC RID: 7612
	private const string Str_MemorialStudent2 = "MemorialStudent2";

	// Token: 0x04001DBD RID: 7613
	private const string Str_MemorialStudent3 = "MemorialStudent3";

	// Token: 0x04001DBE RID: 7614
	private const string Str_MemorialStudent4 = "MemorialStudent4";

	// Token: 0x04001DBF RID: 7615
	private const string Str_MemorialStudent5 = "MemorialStudent5";

	// Token: 0x04001DC0 RID: 7616
	private const string Str_MemorialStudent6 = "MemorialStudent6";

	// Token: 0x04001DC1 RID: 7617
	private const string Str_MemorialStudent7 = "MemorialStudent7";

	// Token: 0x04001DC2 RID: 7618
	private const string Str_MemorialStudent8 = "MemorialStudent8";

	// Token: 0x04001DC3 RID: 7619
	private const string Str_MemorialStudent9 = "MemorialStudent9";
}
