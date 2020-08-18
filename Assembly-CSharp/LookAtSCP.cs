using System;
using UnityEngine;

// Token: 0x0200049E RID: 1182
public class LookAtSCP : MonoBehaviour
{
	// Token: 0x06001E24 RID: 7716 RVA: 0x00178F09 File Offset: 0x00177109
	private void Start()
	{
		if (this.SCP == null)
		{
			this.SCP = GameObject.Find("SCPTarget").transform;
		}
		base.transform.LookAt(this.SCP);
	}

	// Token: 0x06001E25 RID: 7717 RVA: 0x00178F3F File Offset: 0x0017713F
	private void LateUpdate()
	{
		base.transform.LookAt(this.SCP);
	}

	// Token: 0x04003BFB RID: 15355
	public Transform SCP;
}
