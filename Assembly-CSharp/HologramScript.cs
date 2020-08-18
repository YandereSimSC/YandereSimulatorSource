using System;
using UnityEngine;

// Token: 0x020002E4 RID: 740
public class HologramScript : MonoBehaviour
{
	// Token: 0x060016FD RID: 5885 RVA: 0x000C1D18 File Offset: 0x000BFF18
	public void UpdateHolograms()
	{
		GameObject[] holograms = this.Holograms;
		for (int i = 0; i < holograms.Length; i++)
		{
			holograms[i].SetActive(this.TrueFalse());
		}
	}

	// Token: 0x060016FE RID: 5886 RVA: 0x000C1D48 File Offset: 0x000BFF48
	private bool TrueFalse()
	{
		return UnityEngine.Random.value >= 0.5f;
	}

	// Token: 0x04001EF4 RID: 7924
	public GameObject[] Holograms;
}
