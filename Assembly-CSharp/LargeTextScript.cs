using System;
using UnityEngine;

// Token: 0x02000495 RID: 1173
public class LargeTextScript : MonoBehaviour
{
	// Token: 0x06001E09 RID: 7689 RVA: 0x001782EB File Offset: 0x001764EB
	private void Start()
	{
		this.Label.text = this.String[this.ID];
	}

	// Token: 0x06001E0A RID: 7690 RVA: 0x00178305 File Offset: 0x00176505
	private void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			this.ID++;
			this.Label.text = this.String[this.ID];
		}
	}

	// Token: 0x04003BE0 RID: 15328
	public UILabel Label;

	// Token: 0x04003BE1 RID: 15329
	public string[] String;

	// Token: 0x04003BE2 RID: 15330
	public int ID;
}
