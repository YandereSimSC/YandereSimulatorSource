using System;
using UnityEngine;

// Token: 0x020002B7 RID: 695
public static class GlobalsHelper
{
	// Token: 0x0600144C RID: 5196 RVA: 0x000B3B3D File Offset: 0x000B1D3D
	public static bool GetBool(string key)
	{
		return PlayerPrefs.GetInt(key) == 1;
	}

	// Token: 0x0600144D RID: 5197 RVA: 0x000B3B48 File Offset: 0x000B1D48
	public static void SetBool(string key, bool value)
	{
		PlayerPrefs.SetInt(key, value ? 1 : 0);
	}

	// Token: 0x0600144E RID: 5198 RVA: 0x000B3B57 File Offset: 0x000B1D57
	public static T GetEnum<T>(string key) where T : struct, IConvertible
	{
		return (T)((object)PlayerPrefs.GetInt(key));
	}

	// Token: 0x0600144F RID: 5199 RVA: 0x000B3B69 File Offset: 0x000B1D69
	public static void SetEnum<T>(string key, T value) where T : struct, IConvertible
	{
		PlayerPrefs.SetInt(key, (int)((object)value));
	}

	// Token: 0x06001450 RID: 5200 RVA: 0x000B3B7C File Offset: 0x000B1D7C
	public static Vector2 GetVector2(string key)
	{
		float @float = PlayerPrefs.GetFloat(key + "_X");
		float float2 = PlayerPrefs.GetFloat(key + "_Y");
		return new Vector2(@float, float2);
	}

	// Token: 0x06001451 RID: 5201 RVA: 0x000B3BB0 File Offset: 0x000B1DB0
	public static void SetVector2(string key, Vector2 value)
	{
		PlayerPrefs.SetFloat(key + "_X", value.x);
		PlayerPrefs.SetFloat(key + "_Y", value.y);
	}

	// Token: 0x06001452 RID: 5202 RVA: 0x000B3BDE File Offset: 0x000B1DDE
	public static void DeleteVector2(string key)
	{
		Globals.Delete(key + "_X");
		Globals.Delete(key + "_Y");
	}

	// Token: 0x06001453 RID: 5203 RVA: 0x000B3C00 File Offset: 0x000B1E00
	public static void DeleteVector2Collection(string key, int[] usedKeys)
	{
		foreach (int num in usedKeys)
		{
			GlobalsHelper.DeleteVector2(key + num.ToString());
		}
		KeysHelper.Delete(key);
	}

	// Token: 0x06001454 RID: 5204 RVA: 0x000B3C3C File Offset: 0x000B1E3C
	public static Vector3 GetVector3(string key)
	{
		float @float = PlayerPrefs.GetFloat(key + "_X");
		float float2 = PlayerPrefs.GetFloat(key + "_Y");
		float float3 = PlayerPrefs.GetFloat(key + "_Z");
		return new Vector3(@float, float2, float3);
	}

	// Token: 0x06001455 RID: 5205 RVA: 0x000B3C84 File Offset: 0x000B1E84
	public static void SetVector3(string key, Vector3 value)
	{
		PlayerPrefs.SetFloat(key + "_X", value.x);
		PlayerPrefs.SetFloat(key + "_Y", value.y);
		PlayerPrefs.SetFloat(key + "_Z", value.z);
	}

	// Token: 0x06001456 RID: 5206 RVA: 0x000B3CD3 File Offset: 0x000B1ED3
	public static void DeleteVector3(string key)
	{
		Globals.Delete(key + "_X");
		Globals.Delete(key + "_Y");
		Globals.Delete(key + "_Z");
	}

	// Token: 0x06001457 RID: 5207 RVA: 0x000B3D08 File Offset: 0x000B1F08
	public static void DeleteVector3Collection(string key, int[] usedKeys)
	{
		foreach (int num in usedKeys)
		{
			GlobalsHelper.DeleteVector3(key + num.ToString());
		}
		KeysHelper.Delete(key);
	}

	// Token: 0x06001458 RID: 5208 RVA: 0x000B3D44 File Offset: 0x000B1F44
	public static Vector4 GetVector4(string key)
	{
		float @float = PlayerPrefs.GetFloat(key + "_W");
		float float2 = PlayerPrefs.GetFloat(key + "_X");
		float float3 = PlayerPrefs.GetFloat(key + "_Y");
		float float4 = PlayerPrefs.GetFloat(key + "_Z");
		return new Vector4(@float, float2, float3, float4);
	}

	// Token: 0x06001459 RID: 5209 RVA: 0x000B3D9C File Offset: 0x000B1F9C
	public static void SetVector4(string key, Vector4 value)
	{
		PlayerPrefs.SetFloat(key + "_W", value.w);
		PlayerPrefs.SetFloat(key + "_X", value.x);
		PlayerPrefs.SetFloat(key + "_Y", value.y);
		PlayerPrefs.SetFloat(key + "_Z", value.z);
	}

	// Token: 0x0600145A RID: 5210 RVA: 0x000B3E04 File Offset: 0x000B2004
	public static void DeleteVector4(string key)
	{
		Globals.Delete(key + "_W");
		Globals.Delete(key + "_X");
		Globals.Delete(key + "_Y");
		Globals.Delete(key + "_Z");
	}

	// Token: 0x0600145B RID: 5211 RVA: 0x000B3E54 File Offset: 0x000B2054
	public static Color GetColor(string key)
	{
		float @float = PlayerPrefs.GetFloat(key + "_R");
		float float2 = PlayerPrefs.GetFloat(key + "_G");
		float float3 = PlayerPrefs.GetFloat(key + "_B");
		float float4 = PlayerPrefs.GetFloat(key + "_A");
		return new Color(@float, float2, float3, float4);
	}

	// Token: 0x0600145C RID: 5212 RVA: 0x000B3EAC File Offset: 0x000B20AC
	public static void SetColor(string key, Color value)
	{
		PlayerPrefs.SetFloat(key + "_R", value.r);
		PlayerPrefs.SetFloat(key + "_G", value.g);
		PlayerPrefs.SetFloat(key + "_B", value.b);
		PlayerPrefs.SetFloat(key + "_A", value.a);
	}

	// Token: 0x0600145D RID: 5213 RVA: 0x000B3F14 File Offset: 0x000B2114
	public static void DeleteColor(string key)
	{
		Globals.Delete(key + "_R");
		Globals.Delete(key + "_G");
		Globals.Delete(key + "_B");
		Globals.Delete(key + "_A");
	}

	// Token: 0x0600145E RID: 5214 RVA: 0x000B3F64 File Offset: 0x000B2164
	public static void DeleteColorCollection(string key, int[] usedKeys)
	{
		foreach (int num in usedKeys)
		{
			GlobalsHelper.DeleteColor(key + num.ToString());
		}
		KeysHelper.Delete(key);
	}
}
