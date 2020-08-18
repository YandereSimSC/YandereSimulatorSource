using System;
using UnityEngine;

// Token: 0x0200003E RID: 62
[AddComponentMenu("NGUI/Examples/Window Auto-Yaw")]
public class WindowAutoYaw : MonoBehaviour
{
	// Token: 0x06000145 RID: 325 RVA: 0x00013632 File Offset: 0x00011832
	private void OnDisable()
	{
		this.mTrans.localRotation = Quaternion.identity;
	}

	// Token: 0x06000146 RID: 326 RVA: 0x00013644 File Offset: 0x00011844
	private void OnEnable()
	{
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		this.mTrans = base.transform;
	}

	// Token: 0x06000147 RID: 327 RVA: 0x00013678 File Offset: 0x00011878
	private void Update()
	{
		if (this.uiCamera != null)
		{
			Vector3 vector = this.uiCamera.WorldToViewportPoint(this.mTrans.position);
			this.mTrans.localRotation = Quaternion.Euler(0f, (vector.x * 2f - 1f) * this.yawAmount, 0f);
		}
	}

	// Token: 0x040002D4 RID: 724
	public int updateOrder;

	// Token: 0x040002D5 RID: 725
	public Camera uiCamera;

	// Token: 0x040002D6 RID: 726
	public float yawAmount = 20f;

	// Token: 0x040002D7 RID: 727
	private Transform mTrans;
}
