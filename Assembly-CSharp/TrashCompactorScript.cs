using System;
using UnityEngine;

// Token: 0x02000430 RID: 1072
public class TrashCompactorScript : MonoBehaviour
{
	// Token: 0x06001C68 RID: 7272 RVA: 0x00153A18 File Offset: 0x00151C18
	private void Start()
	{
		if (this.StudentManager.Students[10] != null || this.StudentManager.Students[11] != null)
		{
			this.CompactTrash();
			return;
		}
		for (int i = 1; i < 101; i++)
		{
			if (this.StudentManager.Students[i] != null && !this.StudentManager.Students[i].Male && (this.StudentManager.Students[i].Cosmetic.Hairstyle == 20 || this.StudentManager.Students[i].Cosmetic.Hairstyle == 21 || this.StudentManager.Students[i].Persona == PersonaType.Protective))
			{
				this.CompactTrash();
			}
		}
	}

	// Token: 0x06001C69 RID: 7273 RVA: 0x00153AE4 File Offset: 0x00151CE4
	private void Update()
	{
		if (this.TrashCompactorObject.gameObject.activeInHierarchy)
		{
			this.Speed += Time.deltaTime * 0.01f;
			this.TrashCompactorObject.position = Vector3.MoveTowards(this.TrashCompactorObject.position, this.Yandere.position, Time.deltaTime * this.Speed);
			this.TrashCompactorObject.LookAt(this.Yandere.position);
			if (Vector3.Distance(this.TrashCompactorObject.position, this.Yandere.position) < 0.5f)
			{
				Application.Quit();
			}
		}
	}

	// Token: 0x06001C6A RID: 7274 RVA: 0x00153B90 File Offset: 0x00151D90
	private void CompactTrash()
	{
		Debug.Log("Taking out the garbage.");
		if (!this.TrashCompactorObject.gameObject.activeInHierarchy)
		{
			SchoolGlobals.SchoolAtmosphereSet = true;
			SchoolGlobals.SchoolAtmosphere = 0f;
			this.StudentManager.SetAtmosphere();
			foreach (StudentScript studentScript in this.StudentManager.Students)
			{
				if (studentScript != null)
				{
					studentScript.gameObject.SetActive(false);
				}
			}
			this.Yandere.gameObject.GetComponent<YandereScript>().NoDebug = true;
			this.TrashCompactorObject.gameObject.SetActive(true);
			this.Jukebox.SetActive(false);
			this.HUD.enabled = false;
		}
	}

	// Token: 0x04003555 RID: 13653
	public StudentManagerScript StudentManager;

	// Token: 0x04003556 RID: 13654
	public JsonScript JSON;

	// Token: 0x04003557 RID: 13655
	public UIPanel HUD;

	// Token: 0x04003558 RID: 13656
	public GameObject Jukebox;

	// Token: 0x04003559 RID: 13657
	public Transform TrashCompactorObject;

	// Token: 0x0400355A RID: 13658
	public Transform Yandere;

	// Token: 0x0400355B RID: 13659
	public float Speed;
}
