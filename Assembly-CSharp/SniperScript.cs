using System;
using UnityEngine;

// Token: 0x020003E6 RID: 998
public class SniperScript : MonoBehaviour
{
	// Token: 0x06001AB7 RID: 6839 RVA: 0x0010C284 File Offset: 0x0010A484
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > 10f)
		{
			if (this.StudentManager.Students[10] != null)
			{
				this.StudentManager.Students[10].BecomeRagdoll();
			}
			if (this.StudentManager.Students[11] != null)
			{
				this.StudentManager.Students[11].BecomeRagdoll();
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04002B38 RID: 11064
	public StudentManagerScript StudentManager;

	// Token: 0x04002B39 RID: 11065
	public float Timer;
}
