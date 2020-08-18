using System;
using UnityEngine;

// Token: 0x0200049D RID: 1181
public class LookAtCamera : MonoBehaviour
{
	// Token: 0x06001E21 RID: 7713 RVA: 0x00178E89 File Offset: 0x00177089
	private void Start()
	{
		if (this.cameraToLookAt == null)
		{
			this.cameraToLookAt = Camera.main;
		}
	}

	// Token: 0x06001E22 RID: 7714 RVA: 0x00178EA4 File Offset: 0x001770A4
	private void Update()
	{
		Vector3 b = new Vector3(0f, this.cameraToLookAt.transform.position.y - base.transform.position.y, 0f);
		base.transform.LookAt(this.cameraToLookAt.transform.position - b);
	}

	// Token: 0x04003BFA RID: 15354
	public Camera cameraToLookAt;
}
