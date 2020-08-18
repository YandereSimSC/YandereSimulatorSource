using System;
using UnityEngine;

// Token: 0x02000474 RID: 1140
public class OtherScript : MonoBehaviour
{
	// Token: 0x06001D4A RID: 7498 RVA: 0x0010CD1C File Offset: 0x0010AF1C
	private void Start()
	{
		if (this.JSON.Students[11].Name != "Reserved")
		{
			Application.Quit();
			this.Wow();
		}
		else
		{
			for (int i = 1; i < 101; i++)
			{
				if (this.JSON.Students[i].Gender == 0 && this.JSON.Students[i].Hairstyle == "20" && this.StudentManager.Students[i] != null)
				{
					this.StudentManager.Students[i].gameObject.SetActive(false);
				}
			}
		}
	}

	// Token: 0x06001D4B RID: 7499 RVA: 0x0010CDD8 File Offset: 0x0010AFD8
	private void Update()
	{
		if (this.Other.gameObject.activeInHierarchy)
		{
			this.Speed += Time.deltaTime * 0.01f;
			this.Other.position = Vector3.MoveTowards(this.Other.position, this.Yandere.position, Time.deltaTime * this.Speed);
			this.Other.LookAt(this.Yandere.position);
			if (Vector3.Distance(this.Other.position, this.Yandere.position) < 0.5f)
			{
				Application.Quit();
			}
		}
	}

	// Token: 0x06001D4C RID: 7500 RVA: 0x0010CE84 File Offset: 0x0010B084
	private void Wow()
	{
		if (!this.Other.gameObject.activeInHierarchy)
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
			this.Other.gameObject.SetActive(true);
			this.Jukebox.SetActive(false);
			this.HUD.enabled = false;
		}
	}

	// Token: 0x040024E3 RID: 9443
	public StudentManagerScript StudentManager;

	// Token: 0x040024E4 RID: 9444
	public JsonScript JSON;

	// Token: 0x040024E5 RID: 9445
	public UIPanel HUD;

	// Token: 0x040024E6 RID: 9446
	public GameObject Jukebox;

	// Token: 0x040024E7 RID: 9447
	public Transform Yandere;

	// Token: 0x040024E8 RID: 9448
	public Transform Other;

	// Token: 0x040024E9 RID: 9449
	public float Speed;
}
