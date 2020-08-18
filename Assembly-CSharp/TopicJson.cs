using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x02000311 RID: 785
[Serializable]
public class TopicJson : JsonData
{
	// Token: 0x17000451 RID: 1105
	// (get) Token: 0x060017B8 RID: 6072 RVA: 0x000CFD05 File Offset: 0x000CDF05
	public static string FilePath
	{
		get
		{
			return Path.Combine(JsonData.FolderPath, "Topics.json");
		}
	}

	// Token: 0x060017B9 RID: 6073 RVA: 0x000CFD18 File Offset: 0x000CDF18
	public static TopicJson[] LoadFromJson(string path)
	{
		TopicJson[] array = new TopicJson[101];
		foreach (Dictionary<string, object> d in JsonData.Deserialize(path))
		{
			int num = TFUtils.LoadInt(d, "ID");
			if (num == 0)
			{
				break;
			}
			array[num] = new TopicJson();
			TopicJson topicJson = array[num];
			topicJson.topics = new int[26];
			for (int j = 1; j <= 25; j++)
			{
				topicJson.topics[j] = TFUtils.LoadInt(d, j.ToString());
			}
		}
		return array;
	}

	// Token: 0x17000452 RID: 1106
	// (get) Token: 0x060017BA RID: 6074 RVA: 0x000CFD9D File Offset: 0x000CDF9D
	public int[] Topics
	{
		get
		{
			return this.topics;
		}
	}

	// Token: 0x040021AD RID: 8621
	[SerializeField]
	private int[] topics;
}
