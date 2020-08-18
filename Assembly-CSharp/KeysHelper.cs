using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002B8 RID: 696
public static class KeysHelper
{
	// Token: 0x0600145F RID: 5215 RVA: 0x000B3F9D File Offset: 0x000B219D
	public static int[] GetIntegerKeys(string key)
	{
		return Array.ConvertAll<string, int>(KeysHelper.SplitList(KeysHelper.GetKeyList(KeysHelper.GetKeyListKey(key))), (string str) => int.Parse(str));
	}

	// Token: 0x06001460 RID: 5216 RVA: 0x000B3FD3 File Offset: 0x000B21D3
	public static string[] GetStringKeys(string key)
	{
		return KeysHelper.SplitList(KeysHelper.GetKeyList(KeysHelper.GetKeyListKey(key)));
	}

	// Token: 0x06001461 RID: 5217 RVA: 0x000B3FE5 File Offset: 0x000B21E5
	public static T[] GetEnumKeys<T>(string key) where T : struct, IConvertible
	{
		return Array.ConvertAll<string, T>(KeysHelper.SplitList(KeysHelper.GetKeyList(KeysHelper.GetKeyListKey(key))), (string str) => (T)((object)Enum.Parse(typeof(T), str)));
	}

	// Token: 0x06001462 RID: 5218 RVA: 0x000B401C File Offset: 0x000B221C
	public static KeyValuePair<T, U>[] GetKeys<T, U>(string key) where T : struct where U : struct
	{
		string[] array = KeysHelper.SplitList(KeysHelper.GetKeyList(KeysHelper.GetKeyListKey(key)));
		KeyValuePair<T, U>[] array2 = new KeyValuePair<T, U>[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			string[] array3 = array[i].Split(new char[]
			{
				'^'
			});
			array2[i] = new KeyValuePair<T, U>((T)((object)int.Parse(array3[0])), (U)((object)int.Parse(array3[1])));
		}
		return array2;
	}

	// Token: 0x06001463 RID: 5219 RVA: 0x000B4098 File Offset: 0x000B2298
	public static void AddIfMissing(string key, string id)
	{
		string keyListKey = KeysHelper.GetKeyListKey(key);
		string keyList = KeysHelper.GetKeyList(keyListKey);
		if (!KeysHelper.HasKey(KeysHelper.SplitList(keyList), id))
		{
			KeysHelper.AppendKey(keyListKey, keyList, id);
		}
	}

	// Token: 0x06001464 RID: 5220 RVA: 0x000B40C9 File Offset: 0x000B22C9
	public static void Delete(string key)
	{
		Globals.Delete(KeysHelper.GetKeyListKey(key));
	}

	// Token: 0x06001465 RID: 5221 RVA: 0x000B40D6 File Offset: 0x000B22D6
	private static string GetKeyListKey(string key)
	{
		return key + "Keys";
	}

	// Token: 0x06001466 RID: 5222 RVA: 0x000B40E3 File Offset: 0x000B22E3
	private static string GetKeyList(string keyListKey)
	{
		return PlayerPrefs.GetString(keyListKey);
	}

	// Token: 0x06001467 RID: 5223 RVA: 0x000B40EB File Offset: 0x000B22EB
	private static string[] SplitList(string keyList)
	{
		if (keyList.Length <= 0)
		{
			return new string[0];
		}
		return keyList.Split(new char[]
		{
			'|'
		});
	}

	// Token: 0x06001468 RID: 5224 RVA: 0x000B410E File Offset: 0x000B230E
	private static int FindKey(string[] keyListStrings, string key)
	{
		return Array.IndexOf<string>(keyListStrings, key);
	}

	// Token: 0x06001469 RID: 5225 RVA: 0x000B4117 File Offset: 0x000B2317
	private static bool HasKey(string[] keyListStrings, string key)
	{
		return KeysHelper.FindKey(keyListStrings, key) > -1;
	}

	// Token: 0x0600146A RID: 5226 RVA: 0x000B4124 File Offset: 0x000B2324
	private static void AppendKey(string keyListKey, string keyList, string key)
	{
		string value = (keyList.Length == 0) ? (keyList + key) : (keyList + "|" + key);
		PlayerPrefs.SetString(keyListKey, value);
	}

	// Token: 0x04001CF2 RID: 7410
	private const string KeyListPrefix = "Keys";

	// Token: 0x04001CF3 RID: 7411
	private const char KeyListSeparator = '|';

	// Token: 0x04001CF4 RID: 7412
	public const char PairSeparator = '^';
}
