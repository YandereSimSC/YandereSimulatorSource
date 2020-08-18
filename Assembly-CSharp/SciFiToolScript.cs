using System;
using UnityEngine;

// Token: 0x020003CD RID: 973
public class SciFiToolScript : MonoBehaviour
{
	// Token: 0x06001A4F RID: 6735 RVA: 0x00101A1B File Offset: 0x000FFC1B
	private void Start()
	{
		this.Target = this.Student.StudentManager.ToolTarget;
	}

	// Token: 0x06001A50 RID: 6736 RVA: 0x00101A33 File Offset: 0x000FFC33
	private void Update()
	{
		if ((double)Vector3.Distance(this.Tip.position, this.Target.position) < 0.1)
		{
			this.Sparks.Play();
			return;
		}
		this.Sparks.Stop();
	}

	// Token: 0x040029AE RID: 10670
	public StudentScript Student;

	// Token: 0x040029AF RID: 10671
	public ParticleSystem Sparks;

	// Token: 0x040029B0 RID: 10672
	public Transform Target;

	// Token: 0x040029B1 RID: 10673
	public Transform Tip;
}
