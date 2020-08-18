using System;
using UnityEngine;

// Token: 0x02000265 RID: 613
public class DetectionCameraScript : MonoBehaviour
{
	// Token: 0x0600133A RID: 4922 RVA: 0x000A0794 File Offset: 0x0009E994
	private void Update()
	{
		base.transform.position = this.YandereChan.transform.position + Vector3.up * 100f;
		base.transform.eulerAngles = new Vector3(90f, base.transform.eulerAngles.y, base.transform.eulerAngles.z);
	}

	// Token: 0x040019F7 RID: 6647
	public Transform YandereChan;
}
