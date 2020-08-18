using System;
using System.Collections.Generic;
using System.IO;
using JsonFx.Json;
using UnityEngine;

// Token: 0x0200030E RID: 782
public abstract class JsonData
{
	// Token: 0x1700043A RID: 1082
	// (get) Token: 0x06001791 RID: 6033 RVA: 0x000CF86C File Offset: 0x000CDA6C
	protected static string FolderPath
	{
		get
		{
			return Path.Combine(Application.streamingAssetsPath, "JSON");
		}
	}

	// Token: 0x06001792 RID: 6034 RVA: 0x000CF87D File Offset: 0x000CDA7D
	protected static Dictionary<string, object>[] Deserialize(string filename)
	{
		return JsonReader.Deserialize<Dictionary<string, object>[]>(File.ReadAllText(filename));
	}
}
