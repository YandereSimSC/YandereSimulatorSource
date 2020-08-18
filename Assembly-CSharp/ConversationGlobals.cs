using System;
using System.Collections.Generic;

// Token: 0x020002BE RID: 702
public static class ConversationGlobals
{
	// Token: 0x060014C0 RID: 5312 RVA: 0x000B55C5 File Offset: 0x000B37C5
	public static bool GetTopicDiscovered(int topicID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TopicDiscovered_",
			topicID.ToString()
		}));
	}

	// Token: 0x060014C1 RID: 5313 RVA: 0x000B5600 File Offset: 0x000B3800
	public static void SetTopicDiscovered(int topicID, bool value)
	{
		string text = topicID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_TopicDiscovered_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TopicDiscovered_",
			text
		}), value);
	}

	// Token: 0x060014C2 RID: 5314 RVA: 0x000B5666 File Offset: 0x000B3866
	public static int[] KeysOfTopicDiscovered()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_TopicDiscovered_");
	}

	// Token: 0x060014C3 RID: 5315 RVA: 0x000B5688 File Offset: 0x000B3888
	public static bool GetTopicLearnedByStudent(int topicID, int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TopicLearnedByStudent_",
			topicID.ToString(),
			"_",
			studentID.ToString()
		}));
	}

	// Token: 0x060014C4 RID: 5316 RVA: 0x000B56E0 File Offset: 0x000B38E0
	public static void SetTopicLearnedByStudent(int topicID, int studentID, bool value)
	{
		string text = topicID.ToString();
		string text2 = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_TopicLearnedByStudent_", text + "^" + text2);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TopicLearnedByStudent_",
			text,
			"_",
			text2
		}), value);
	}

	// Token: 0x060014C5 RID: 5317 RVA: 0x000B5768 File Offset: 0x000B3968
	public static IntAndIntPair[] KeysOfTopicLearnedByStudent()
	{
		KeyValuePair<int, int>[] keys = KeysHelper.GetKeys<int, int>("Profile_" + GameGlobals.Profile + "_TopicLearnedByStudent_");
		IntAndIntPair[] array = new IntAndIntPair[keys.Length];
		for (int i = 0; i < keys.Length; i++)
		{
			KeyValuePair<int, int> keyValuePair = keys[i];
			array[i] = new IntAndIntPair(keyValuePair.Key, keyValuePair.Value);
		}
		return array;
	}

	// Token: 0x060014C6 RID: 5318 RVA: 0x000B57CC File Offset: 0x000B39CC
	public static void DeleteAll()
	{
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_TopicDiscovered_", ConversationGlobals.KeysOfTopicDiscovered());
		foreach (IntAndIntPair intAndIntPair in ConversationGlobals.KeysOfTopicLearnedByStudent())
		{
			Globals.Delete(string.Concat(new object[]
			{
				"Profile_",
				GameGlobals.Profile,
				"_TopicLearnedByStudent_",
				intAndIntPair.first.ToString(),
				"_",
				intAndIntPair.second.ToString()
			}));
		}
		KeysHelper.Delete("Profile_" + GameGlobals.Profile + "_TopicLearnedByStudent_");
	}

	// Token: 0x04001D15 RID: 7445
	private const string Str_TopicDiscovered = "TopicDiscovered_";

	// Token: 0x04001D16 RID: 7446
	private const string Str_TopicLearnedByStudent = "TopicLearnedByStudent_";
}
