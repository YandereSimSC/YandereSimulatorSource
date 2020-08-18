using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000029 RID: 41
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Examples/Item Database")]
public class InvDatabase : MonoBehaviour
{
	// Token: 0x17000014 RID: 20
	// (get) Token: 0x060000F1 RID: 241 RVA: 0x00012048 File Offset: 0x00010248
	public static InvDatabase[] list
	{
		get
		{
			if (InvDatabase.mIsDirty)
			{
				InvDatabase.mIsDirty = false;
				InvDatabase.mList = NGUITools.FindActive<InvDatabase>();
			}
			return InvDatabase.mList;
		}
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x00012066 File Offset: 0x00010266
	private void OnEnable()
	{
		InvDatabase.mIsDirty = true;
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x00012066 File Offset: 0x00010266
	private void OnDisable()
	{
		InvDatabase.mIsDirty = true;
	}

	// Token: 0x060000F4 RID: 244 RVA: 0x00012070 File Offset: 0x00010270
	private InvBaseItem GetItem(int id16)
	{
		int i = 0;
		int count = this.items.Count;
		while (i < count)
		{
			InvBaseItem invBaseItem = this.items[i];
			if (invBaseItem.id16 == id16)
			{
				return invBaseItem;
			}
			i++;
		}
		return null;
	}

	// Token: 0x060000F5 RID: 245 RVA: 0x000120B0 File Offset: 0x000102B0
	private static InvDatabase GetDatabase(int dbID)
	{
		int i = 0;
		int num = InvDatabase.list.Length;
		while (i < num)
		{
			InvDatabase invDatabase = InvDatabase.list[i];
			if (invDatabase.databaseID == dbID)
			{
				return invDatabase;
			}
			i++;
		}
		return null;
	}

	// Token: 0x060000F6 RID: 246 RVA: 0x000120E8 File Offset: 0x000102E8
	public static InvBaseItem FindByID(int id32)
	{
		InvDatabase database = InvDatabase.GetDatabase(id32 >> 16);
		if (!(database != null))
		{
			return null;
		}
		return database.GetItem(id32 & 65535);
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x00012118 File Offset: 0x00010318
	public static InvBaseItem FindByName(string exact)
	{
		int i = 0;
		int num = InvDatabase.list.Length;
		while (i < num)
		{
			InvDatabase invDatabase = InvDatabase.list[i];
			int j = 0;
			int count = invDatabase.items.Count;
			while (j < count)
			{
				InvBaseItem invBaseItem = invDatabase.items[j];
				if (invBaseItem.name == exact)
				{
					return invBaseItem;
				}
				j++;
			}
			i++;
		}
		return null;
	}

	// Token: 0x060000F8 RID: 248 RVA: 0x0001217C File Offset: 0x0001037C
	public static int FindItemID(InvBaseItem item)
	{
		int i = 0;
		int num = InvDatabase.list.Length;
		while (i < num)
		{
			InvDatabase invDatabase = InvDatabase.list[i];
			if (invDatabase.items.Contains(item))
			{
				return invDatabase.databaseID << 16 | item.id16;
			}
			i++;
		}
		return -1;
	}

	// Token: 0x04000297 RID: 663
	private static InvDatabase[] mList;

	// Token: 0x04000298 RID: 664
	private static bool mIsDirty = true;

	// Token: 0x04000299 RID: 665
	public int databaseID;

	// Token: 0x0400029A RID: 666
	public List<InvBaseItem> items = new List<InvBaseItem>();

	// Token: 0x0400029B RID: 667
	public UnityEngine.Object iconAtlas;
}
