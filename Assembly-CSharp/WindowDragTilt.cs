using System;
using UnityEngine;

// Token: 0x0200003F RID: 63
[AddComponentMenu("NGUI/Examples/Window Drag Tilt")]
public class WindowDragTilt : MonoBehaviour
{
	// Token: 0x06000149 RID: 329 RVA: 0x000136F0 File Offset: 0x000118F0
	private void OnEnable()
	{
		this.mTrans = base.transform;
		this.mLastPos = this.mTrans.position;
	}

	// Token: 0x0600014A RID: 330 RVA: 0x00013710 File Offset: 0x00011910
	private void Update()
	{
		Vector3 vector = this.mTrans.position - this.mLastPos;
		this.mLastPos = this.mTrans.position;
		this.mAngle += vector.x * this.degrees;
		this.mAngle = NGUIMath.SpringLerp(this.mAngle, 0f, 20f, Time.deltaTime);
		this.mTrans.localRotation = Quaternion.Euler(0f, 0f, -this.mAngle);
	}

	// Token: 0x040002D8 RID: 728
	public int updateOrder;

	// Token: 0x040002D9 RID: 729
	public float degrees = 30f;

	// Token: 0x040002DA RID: 730
	private Vector3 mLastPos;

	// Token: 0x040002DB RID: 731
	private Transform mTrans;

	// Token: 0x040002DC RID: 732
	private float mAngle;
}
