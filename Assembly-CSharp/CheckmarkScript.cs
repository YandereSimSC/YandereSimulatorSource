using System;
using UnityEngine;

// Token: 0x02000493 RID: 1171
public class CheckmarkScript : MonoBehaviour
{
	// Token: 0x06001E04 RID: 7684 RVA: 0x001781C7 File Offset: 0x001763C7
	private void Start()
	{
		while (this.ID < this.Checkmarks.Length)
		{
			this.Checkmarks[this.ID].SetActive(false);
			this.ID++;
		}
		this.ID = 0;
	}

	// Token: 0x06001E05 RID: 7685 RVA: 0x00178204 File Offset: 0x00176404
	private void Update()
	{
		if (Input.GetKeyDown("space") && this.ButtonPresses < 26)
		{
			this.ButtonPresses++;
			this.ID = UnityEngine.Random.Range(0, this.Checkmarks.Length - 4);
			while (this.Checkmarks[this.ID].active)
			{
				this.ID = UnityEngine.Random.Range(0, this.Checkmarks.Length - 4);
			}
			this.Checkmarks[this.ID].SetActive(true);
		}
	}

	// Token: 0x04003BDC RID: 15324
	public GameObject[] Checkmarks;

	// Token: 0x04003BDD RID: 15325
	public int ButtonPresses;

	// Token: 0x04003BDE RID: 15326
	public int ID;
}
