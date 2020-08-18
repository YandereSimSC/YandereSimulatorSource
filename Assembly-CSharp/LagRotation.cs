using System;
using UnityEngine;

// Token: 0x02000032 RID: 50
[AddComponentMenu("NGUI/Examples/Lag Rotation")]
public class LagRotation : MonoBehaviour
{
	// Token: 0x06000122 RID: 290 RVA: 0x00012CF7 File Offset: 0x00010EF7
	public void OnRepositionEnd()
	{
		this.Interpolate(1000f);
	}

	// Token: 0x06000123 RID: 291 RVA: 0x00012D04 File Offset: 0x00010F04
	private void Interpolate(float delta)
	{
		if (this.mTrans != null)
		{
			Transform parent = this.mTrans.parent;
			if (parent != null)
			{
				this.mAbsolute = Quaternion.Slerp(this.mAbsolute, parent.rotation * this.mRelative, delta * this.speed);
				this.mTrans.rotation = this.mAbsolute;
			}
		}
	}

	// Token: 0x06000124 RID: 292 RVA: 0x00012D6F File Offset: 0x00010F6F
	private void Start()
	{
		this.mTrans = base.transform;
		this.mRelative = this.mTrans.localRotation;
		this.mAbsolute = this.mTrans.rotation;
	}

	// Token: 0x06000125 RID: 293 RVA: 0x00012D9F File Offset: 0x00010F9F
	private void Update()
	{
		this.Interpolate(this.ignoreTimeScale ? RealTime.deltaTime : Time.deltaTime);
	}

	// Token: 0x040002B3 RID: 691
	public float speed = 10f;

	// Token: 0x040002B4 RID: 692
	public bool ignoreTimeScale;

	// Token: 0x040002B5 RID: 693
	private Transform mTrans;

	// Token: 0x040002B6 RID: 694
	private Quaternion mRelative;

	// Token: 0x040002B7 RID: 695
	private Quaternion mAbsolute;
}
