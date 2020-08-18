using System;
using UnityEngine;

// Token: 0x020000BF RID: 191
public class AccessoryGroupScript : MonoBehaviour
{
	// Token: 0x060009E1 RID: 2529 RVA: 0x0004D02C File Offset: 0x0004B22C
	public void SetPartsActive(bool active)
	{
		GameObject[] parts = this.Parts;
		for (int i = 0; i < parts.Length; i++)
		{
			parts[i].SetActive(active);
		}
	}

	// Token: 0x0400085C RID: 2140
	public GameObject[] Parts;
}
