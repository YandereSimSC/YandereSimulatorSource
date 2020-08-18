using System;
using UnityEngine;

// Token: 0x020000D5 RID: 213
public class AudioListenerScript : MonoBehaviour
{
	// Token: 0x06000A34 RID: 2612 RVA: 0x000539D5 File Offset: 0x00051BD5
	private void Start()
	{
		this.mainCamera = Camera.main;
	}

	// Token: 0x06000A35 RID: 2613 RVA: 0x000539E2 File Offset: 0x00051BE2
	private void Update()
	{
		base.transform.position = this.Target.position;
		base.transform.eulerAngles = this.mainCamera.transform.eulerAngles;
	}

	// Token: 0x04000A53 RID: 2643
	public Transform Target;

	// Token: 0x04000A54 RID: 2644
	public Camera mainCamera;
}
