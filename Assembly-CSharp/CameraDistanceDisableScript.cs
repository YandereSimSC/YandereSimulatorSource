using System;
using UnityEngine;

// Token: 0x02000224 RID: 548
public class CameraDistanceDisableScript : MonoBehaviour
{
	// Token: 0x0600120E RID: 4622 RVA: 0x0007F76D File Offset: 0x0007D96D
	private void Update()
	{
		if (Vector3.Distance(this.Yandere.position, this.RenderTarget.position) > 15f)
		{
			this.MyCamera.enabled = false;
			return;
		}
		this.MyCamera.enabled = true;
	}

	// Token: 0x0400152A RID: 5418
	public Transform RenderTarget;

	// Token: 0x0400152B RID: 5419
	public Transform Yandere;

	// Token: 0x0400152C RID: 5420
	public Camera MyCamera;
}
