using System;
using UnityEngine;

// Token: 0x02000034 RID: 52
[AddComponentMenu("NGUI/Examples/Look At Target")]
public class LookAtTarget : MonoBehaviour
{
	// Token: 0x06000129 RID: 297 RVA: 0x00012DE8 File Offset: 0x00010FE8
	private void Start()
	{
		this.mTrans = base.transform;
	}

	// Token: 0x0600012A RID: 298 RVA: 0x00012DF8 File Offset: 0x00010FF8
	private void LateUpdate()
	{
		if (this.target != null)
		{
			Vector3 forward = this.target.position - this.mTrans.position;
			if (forward.magnitude > 0.001f)
			{
				Quaternion b = Quaternion.LookRotation(forward);
				this.mTrans.rotation = Quaternion.Slerp(this.mTrans.rotation, b, Mathf.Clamp01(this.speed * Time.deltaTime));
			}
		}
	}

	// Token: 0x040002B9 RID: 697
	public int level;

	// Token: 0x040002BA RID: 698
	public Transform target;

	// Token: 0x040002BB RID: 699
	public float speed = 8f;

	// Token: 0x040002BC RID: 700
	private Transform mTrans;
}
