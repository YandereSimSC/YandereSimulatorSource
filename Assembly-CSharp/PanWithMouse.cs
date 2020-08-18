﻿using System;
using UnityEngine;

// Token: 0x02000036 RID: 54
[AddComponentMenu("NGUI/Examples/Pan With Mouse")]
public class PanWithMouse : MonoBehaviour
{
	// Token: 0x0600012E RID: 302 RVA: 0x00012EBB File Offset: 0x000110BB
	private void Start()
	{
		this.mTrans = base.transform;
		this.mStart = this.mTrans.localRotation;
	}

	// Token: 0x0600012F RID: 303 RVA: 0x00012EDC File Offset: 0x000110DC
	private void Update()
	{
		float deltaTime = RealTime.deltaTime;
		Vector3 vector = UICamera.lastEventPosition;
		float num = (float)Screen.width * 0.5f;
		float num2 = (float)Screen.height * 0.5f;
		if (this.range < 0.1f)
		{
			this.range = 0.1f;
		}
		float x = Mathf.Clamp((vector.x - num) / num / this.range, -1f, 1f);
		float y = Mathf.Clamp((vector.y - num2) / num2 / this.range, -1f, 1f);
		this.mRot = Vector2.Lerp(this.mRot, new Vector2(x, y), deltaTime * 5f);
		this.mTrans.localRotation = this.mStart * Quaternion.Euler(-this.mRot.y * this.degrees.y, this.mRot.x * this.degrees.x, 0f);
	}

	// Token: 0x040002BD RID: 701
	public Vector2 degrees = new Vector2(5f, 3f);

	// Token: 0x040002BE RID: 702
	public float range = 1f;

	// Token: 0x040002BF RID: 703
	private Transform mTrans;

	// Token: 0x040002C0 RID: 704
	private Quaternion mStart;

	// Token: 0x040002C1 RID: 705
	private Vector2 mRot = Vector2.zero;
}
