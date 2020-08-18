using System;
using UnityEngine;

// Token: 0x02000093 RID: 147
[AddComponentMenu("NGUI/Tween/Tween Transform")]
public class TweenTransform : UITweener
{
	// Token: 0x0600061A RID: 1562 RVA: 0x000358B0 File Offset: 0x00033AB0
	protected override void OnUpdate(float factor, bool isFinished)
	{
		if (this.to != null)
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
				this.mPos = this.mTrans.position;
				this.mRot = this.mTrans.rotation;
				this.mScale = this.mTrans.localScale;
			}
			if (this.from != null)
			{
				this.mTrans.position = this.from.position * (1f - factor) + this.to.position * factor;
				this.mTrans.localScale = this.from.localScale * (1f - factor) + this.to.localScale * factor;
				this.mTrans.rotation = Quaternion.Slerp(this.from.rotation, this.to.rotation, factor);
			}
			else
			{
				this.mTrans.position = this.mPos * (1f - factor) + this.to.position * factor;
				this.mTrans.localScale = this.mScale * (1f - factor) + this.to.localScale * factor;
				this.mTrans.rotation = Quaternion.Slerp(this.mRot, this.to.rotation, factor);
			}
			if (this.parentWhenFinished && isFinished)
			{
				this.mTrans.parent = this.to;
			}
		}
	}

	// Token: 0x0600061B RID: 1563 RVA: 0x00035A6B File Offset: 0x00033C6B
	public static TweenTransform Begin(GameObject go, float duration, Transform to)
	{
		return TweenTransform.Begin(go, duration, null, to);
	}

	// Token: 0x0600061C RID: 1564 RVA: 0x00035A78 File Offset: 0x00033C78
	public static TweenTransform Begin(GameObject go, float duration, Transform from, Transform to)
	{
		TweenTransform tweenTransform = UITweener.Begin<TweenTransform>(go, duration, 0f);
		tweenTransform.from = from;
		tweenTransform.to = to;
		if (duration <= 0f)
		{
			tweenTransform.Sample(1f, true);
			tweenTransform.enabled = false;
		}
		return tweenTransform;
	}

	// Token: 0x040005F1 RID: 1521
	public Transform from;

	// Token: 0x040005F2 RID: 1522
	public Transform to;

	// Token: 0x040005F3 RID: 1523
	public bool parentWhenFinished;

	// Token: 0x040005F4 RID: 1524
	private Transform mTrans;

	// Token: 0x040005F5 RID: 1525
	private Vector3 mPos;

	// Token: 0x040005F6 RID: 1526
	private Quaternion mRot;

	// Token: 0x040005F7 RID: 1527
	private Vector3 mScale;
}
