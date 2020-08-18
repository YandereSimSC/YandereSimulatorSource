using System;
using UnityEngine;

// Token: 0x0200003B RID: 59
[AddComponentMenu("NGUI/Examples/Spin With Mouse")]
public class SpinWithMouse : MonoBehaviour
{
	// Token: 0x0600013D RID: 317 RVA: 0x000133D9 File Offset: 0x000115D9
	private void Start()
	{
		this.mTrans = base.transform;
	}

	// Token: 0x0600013E RID: 318 RVA: 0x000133E8 File Offset: 0x000115E8
	private void OnDrag(Vector2 delta)
	{
		UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
		if (this.target != null)
		{
			this.target.localRotation = Quaternion.Euler(0f, -0.5f * delta.x * this.speed, 0f) * this.target.localRotation;
			return;
		}
		this.mTrans.localRotation = Quaternion.Euler(0f, -0.5f * delta.x * this.speed, 0f) * this.mTrans.localRotation;
	}

	// Token: 0x040002CD RID: 717
	public Transform target;

	// Token: 0x040002CE RID: 718
	public float speed = 1f;

	// Token: 0x040002CF RID: 719
	private Transform mTrans;
}
