using System;
using UnityEngine;

// Token: 0x0200038A RID: 906
public class RedStringScript : MonoBehaviour
{
	// Token: 0x0600198C RID: 6540 RVA: 0x000F8C34 File Offset: 0x000F6E34
	private void LateUpdate()
	{
		base.transform.LookAt(this.Target.position);
		base.transform.localScale = new Vector3(1f, 1f, Vector3.Distance(base.transform.position, this.Target.position));
	}

	// Token: 0x0400275E RID: 10078
	public Transform Target;
}
