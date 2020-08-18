using System;
using UnityEngine;

// Token: 0x020002D3 RID: 723
public static class CorkboardGlobals
{
	// Token: 0x060016C6 RID: 5830 RVA: 0x000BC088 File Offset: 0x000BA288
	public static void DeleteAll()
	{
		for (int i = 0; i < 100; i++)
		{
			PlayerPrefs.SetInt(string.Concat(new object[]
			{
				"Profile_",
				GameGlobals.Profile,
				"_CorkboardPhoto_",
				i,
				"_Exists"
			}), 0);
			PlayerPrefs.SetInt(string.Concat(new object[]
			{
				"Profile_",
				GameGlobals.Profile,
				"_CorkboardPhoto_",
				i,
				"_PhotoID"
			}), 0);
			PlayerPrefs.SetFloat(string.Concat(new object[]
			{
				"Profile_",
				GameGlobals.Profile,
				"_CorkboardPhoto_",
				i,
				"_PositionX"
			}), 0f);
			PlayerPrefs.SetFloat(string.Concat(new object[]
			{
				"Profile_",
				GameGlobals.Profile,
				"_CorkboardPhoto_",
				i,
				"_PositionY"
			}), 0f);
			PlayerPrefs.SetFloat(string.Concat(new object[]
			{
				"Profile_",
				GameGlobals.Profile,
				"_CorkboardPhoto_",
				i,
				"_PositionZ"
			}), 0f);
			PlayerPrefs.SetFloat(string.Concat(new object[]
			{
				"Profile_",
				GameGlobals.Profile,
				"_CorkboardPhoto_",
				i,
				"_RotationX"
			}), 0f);
			PlayerPrefs.SetFloat(string.Concat(new object[]
			{
				"Profile_",
				GameGlobals.Profile,
				"_CorkboardPhoto_",
				i,
				"_RotationY"
			}), 0f);
			PlayerPrefs.SetFloat(string.Concat(new object[]
			{
				"Profile_",
				GameGlobals.Profile,
				"_CorkboardPhoto_",
				i,
				"_RotationZ"
			}), 0f);
			PlayerPrefs.SetFloat(string.Concat(new object[]
			{
				"Profile_",
				GameGlobals.Profile,
				"_CorkboardPhoto_",
				i,
				"_ScaleX"
			}), 0f);
			PlayerPrefs.SetFloat(string.Concat(new object[]
			{
				"Profile_",
				GameGlobals.Profile,
				"_CorkboardPhoto_",
				i,
				"_ScaleY"
			}), 0f);
			PlayerPrefs.SetFloat(string.Concat(new object[]
			{
				"Profile_",
				GameGlobals.Profile,
				"_CorkboardPhoto_",
				i,
				"_ScaleZ"
			}), 0f);
			PlayerPrefs.SetInt(string.Concat(new object[]
			{
				"Profile_",
				GameGlobals.Profile,
				"_CorkboardString_",
				i,
				"_Exists"
			}), 0);
			PlayerPrefs.SetFloat(string.Concat(new object[]
			{
				"Profile_",
				GameGlobals.Profile,
				"_CorkboardString_",
				i,
				"_PositionX"
			}), 0f);
			PlayerPrefs.SetFloat(string.Concat(new object[]
			{
				"Profile_",
				GameGlobals.Profile,
				"_CorkboardString_",
				i,
				"_PositionY"
			}), 0f);
			PlayerPrefs.SetFloat(string.Concat(new object[]
			{
				"Profile_",
				GameGlobals.Profile,
				"_CorkboardString_",
				i,
				"_PositionZ"
			}), 0f);
			PlayerPrefs.SetFloat(string.Concat(new object[]
			{
				"Profile_",
				GameGlobals.Profile,
				"_CorkboardString2_",
				i,
				"_PositionX"
			}), 0f);
			PlayerPrefs.SetFloat(string.Concat(new object[]
			{
				"Profile_",
				GameGlobals.Profile,
				"_CorkboardString2_",
				i,
				"_PositionY"
			}), 0f);
			PlayerPrefs.SetFloat(string.Concat(new object[]
			{
				"Profile_",
				GameGlobals.Profile,
				"_CorkboardString2_",
				i,
				"_PositionZ"
			}), 0f);
		}
	}
}
