using System;
using UnityEngine;

// Token: 0x02000361 RID: 865
public class PhotographyClubScript : MonoBehaviour
{
	// Token: 0x060018E7 RID: 6375 RVA: 0x000E8360 File Offset: 0x000E6560
	private void Start()
	{
		if (SchoolGlobals.SchoolAtmosphere <= 0.8f)
		{
			this.InvestigationPhotos.SetActive(true);
			this.ArtsyPhotos.SetActive(false);
			this.CrimeScene.SetActive(true);
			this.StraightTables.SetActive(true);
			this.CrookedTables.SetActive(false);
			return;
		}
		this.InvestigationPhotos.SetActive(false);
		this.ArtsyPhotos.SetActive(true);
		this.CrimeScene.SetActive(false);
		this.StraightTables.SetActive(false);
		this.CrookedTables.SetActive(true);
	}

	// Token: 0x04002527 RID: 9511
	public GameObject CrimeScene;

	// Token: 0x04002528 RID: 9512
	public GameObject InvestigationPhotos;

	// Token: 0x04002529 RID: 9513
	public GameObject ArtsyPhotos;

	// Token: 0x0400252A RID: 9514
	public GameObject StraightTables;

	// Token: 0x0400252B RID: 9515
	public GameObject CrookedTables;
}
