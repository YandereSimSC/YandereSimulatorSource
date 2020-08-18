using System;
using UnityEngine;

// Token: 0x02000441 RID: 1089
public static class GameObjectUtils
{
	// Token: 0x06001CB4 RID: 7348 RVA: 0x00155734 File Offset: 0x00153934
	public static void SetLayerRecursively(GameObject obj, int newLayer)
	{
		obj.layer = newLayer;
		foreach (object obj2 in obj.transform)
		{
			GameObjectUtils.SetLayerRecursively(((Transform)obj2).gameObject, newLayer);
		}
	}

	// Token: 0x06001CB5 RID: 7349 RVA: 0x00155798 File Offset: 0x00153998
	public static void SetTagRecursively(GameObject obj, string newTag)
	{
		obj.tag = newTag;
		foreach (object obj2 in obj.transform)
		{
			GameObjectUtils.SetTagRecursively(((Transform)obj2).gameObject, newTag);
		}
	}
}
