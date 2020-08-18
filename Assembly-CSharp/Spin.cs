using System;
using UnityEngine;

// Token: 0x0200003A RID: 58
[AddComponentMenu("NGUI/Examples/Spin")]
public class Spin : MonoBehaviour
{
	// Token: 0x06000138 RID: 312 RVA: 0x000132E9 File Offset: 0x000114E9
	private void Start()
	{
		this.mTrans = base.transform;
		this.mRb = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06000139 RID: 313 RVA: 0x00013303 File Offset: 0x00011503
	private void Update()
	{
		if (this.mRb == null)
		{
			this.ApplyDelta(this.ignoreTimeScale ? RealTime.deltaTime : Time.deltaTime);
		}
	}

	// Token: 0x0600013A RID: 314 RVA: 0x0001332D File Offset: 0x0001152D
	private void FixedUpdate()
	{
		if (this.mRb != null)
		{
			this.ApplyDelta(Time.deltaTime);
		}
	}

	// Token: 0x0600013B RID: 315 RVA: 0x00013348 File Offset: 0x00011548
	public void ApplyDelta(float delta)
	{
		delta *= 360f;
		Quaternion rhs = Quaternion.Euler(this.rotationsPerSecond * delta);
		if (this.mRb == null)
		{
			this.mTrans.rotation = this.mTrans.rotation * rhs;
			return;
		}
		this.mRb.MoveRotation(this.mRb.rotation * rhs);
	}

	// Token: 0x040002C9 RID: 713
	public Vector3 rotationsPerSecond = new Vector3(0f, 0.1f, 0f);

	// Token: 0x040002CA RID: 714
	public bool ignoreTimeScale;

	// Token: 0x040002CB RID: 715
	private Rigidbody mRb;

	// Token: 0x040002CC RID: 716
	private Transform mTrans;
}
