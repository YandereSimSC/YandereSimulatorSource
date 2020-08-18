using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x02000310 RID: 784
[Serializable]
public class CreditJson : JsonData
{
	// Token: 0x1700044E RID: 1102
	// (get) Token: 0x060017B3 RID: 6067 RVA: 0x000CFC7C File Offset: 0x000CDE7C
	public static string FilePath
	{
		get
		{
			return Path.Combine(JsonData.FolderPath, "Credits.json");
		}
	}

	// Token: 0x060017B4 RID: 6068 RVA: 0x000CFC90 File Offset: 0x000CDE90
	public static CreditJson[] LoadFromJson(string path)
	{
		List<CreditJson> list = new List<CreditJson>();
		foreach (Dictionary<string, object> dictionary in JsonData.Deserialize(path))
		{
			list.Add(new CreditJson
			{
				name = TFUtils.LoadString(dictionary, "Name"),
				size = TFUtils.LoadInt(dictionary, "Size")
			});
		}
		return list.ToArray();
	}

	// Token: 0x1700044F RID: 1103
	// (get) Token: 0x060017B5 RID: 6069 RVA: 0x000CFCF5 File Offset: 0x000CDEF5
	public string Name
	{
		get
		{
			return this.name;
		}
	}

	// Token: 0x17000450 RID: 1104
	// (get) Token: 0x060017B6 RID: 6070 RVA: 0x000CFCFD File Offset: 0x000CDEFD
	public int Size
	{
		get
		{
			return this.size;
		}
	}

	// Token: 0x040021AB RID: 8619
	[SerializeField]
	private string name;

	// Token: 0x040021AC RID: 8620
	[SerializeField]
	private int size;
}
