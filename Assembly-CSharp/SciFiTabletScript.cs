using System;
using UnityEngine;

// Token: 0x020003CB RID: 971
public class SciFiTabletScript : MonoBehaviour
{
	// Token: 0x06001A49 RID: 6729 RVA: 0x001018C8 File Offset: 0x000FFAC8
	private void Start()
	{
		this.Holograms = this.Student.StudentManager.Holograms;
	}

	// Token: 0x06001A4A RID: 6730 RVA: 0x001018E0 File Offset: 0x000FFAE0
	private void Update()
	{
		if ((double)Vector3.Distance(this.Finger.position, base.transform.position) < 0.1)
		{
			if (!this.Updated)
			{
				this.Holograms.UpdateHolograms();
				this.Updated = true;
				return;
			}
		}
		else
		{
			this.Updated = false;
		}
	}

	// Token: 0x040029A6 RID: 10662
	public StudentScript Student;

	// Token: 0x040029A7 RID: 10663
	public HologramScript Holograms;

	// Token: 0x040029A8 RID: 10664
	public Transform Finger;

	// Token: 0x040029A9 RID: 10665
	public bool Updated;
}
