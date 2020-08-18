using System;
using UnityEngine;

// Token: 0x0200001F RID: 31
public class ClimbFollowScript : MonoBehaviour
{
	// Token: 0x060000CD RID: 205 RVA: 0x000114F0 File Offset: 0x0000F6F0
	private void Update()
	{
		base.transform.position = new Vector3(base.transform.position.x, this.Yandere.position.y, base.transform.position.z);
	}

	// Token: 0x0400026B RID: 619
	public Transform Yandere;
}
