using System;
using UnityEngine;

// Token: 0x020003A5 RID: 933
public class RooftopScript : MonoBehaviour
{
	// Token: 0x060019CF RID: 6607 RVA: 0x000FC5C4 File Offset: 0x000FA7C4
	private void Start()
	{
		if (SchoolGlobals.RoofFence)
		{
			GameObject[] dumpPoints = this.DumpPoints;
			for (int i = 0; i < dumpPoints.Length; i++)
			{
				dumpPoints[i].SetActive(false);
			}
			this.Railing.SetActive(false);
			this.Fence.SetActive(true);
		}
	}

	// Token: 0x04002861 RID: 10337
	public GameObject[] DumpPoints;

	// Token: 0x04002862 RID: 10338
	public GameObject Railing;

	// Token: 0x04002863 RID: 10339
	public GameObject Fence;
}
