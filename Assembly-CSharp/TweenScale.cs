using System;
using UnityEngine;

// Token: 0x02000092 RID: 146
[AddComponentMenu("NGUI/Tween/Tween Scale")]
public class TweenScale : UITweener
{
	// Token: 0x170000D9 RID: 217
	// (get) Token: 0x0600060E RID: 1550 RVA: 0x0003573F File Offset: 0x0003393F
	public Transform cachedTransform
	{
		get
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			return this.mTrans;
		}
	}

	// Token: 0x170000DA RID: 218
	// (get) Token: 0x0600060F RID: 1551 RVA: 0x00035761 File Offset: 0x00033961
	// (set) Token: 0x06000610 RID: 1552 RVA: 0x0003576E File Offset: 0x0003396E
	public Vector3 value
	{
		get
		{
			return this.cachedTransform.localScale;
		}
		set
		{
			this.cachedTransform.localScale = value;
		}
	}

	// Token: 0x170000DB RID: 219
	// (get) Token: 0x06000611 RID: 1553 RVA: 0x0003577C File Offset: 0x0003397C
	// (set) Token: 0x06000612 RID: 1554 RVA: 0x00035784 File Offset: 0x00033984
	[Obsolete("Use 'value' instead")]
	public Vector3 scale
	{
		get
		{
			return this.value;
		}
		set
		{
			this.value = value;
		}
	}

	// Token: 0x06000613 RID: 1555 RVA: 0x00035790 File Offset: 0x00033990
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = this.from * (1f - factor) + this.to * factor;
		if (this.updateTable)
		{
			if (this.mTable == null)
			{
				this.mTable = NGUITools.FindInParents<UITable>(base.gameObject);
				if (this.mTable == null)
				{
					this.updateTable = false;
					return;
				}
			}
			this.mTable.repositionNow = true;
		}
	}

	// Token: 0x06000614 RID: 1556 RVA: 0x00035810 File Offset: 0x00033A10
	public static TweenScale Begin(GameObject go, float duration, Vector3 scale)
	{
		TweenScale tweenScale = UITweener.Begin<TweenScale>(go, duration, 0f);
		tweenScale.from = tweenScale.value;
		tweenScale.to = scale;
		if (duration <= 0f)
		{
			tweenScale.Sample(1f, true);
			tweenScale.enabled = false;
		}
		return tweenScale;
	}

	// Token: 0x06000615 RID: 1557 RVA: 0x00035859 File Offset: 0x00033A59
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x06000616 RID: 1558 RVA: 0x00035867 File Offset: 0x00033A67
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x06000617 RID: 1559 RVA: 0x00035875 File Offset: 0x00033A75
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x06000618 RID: 1560 RVA: 0x00035883 File Offset: 0x00033A83
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x040005EC RID: 1516
	public Vector3 from = Vector3.one;

	// Token: 0x040005ED RID: 1517
	public Vector3 to = Vector3.one;

	// Token: 0x040005EE RID: 1518
	public bool updateTable;

	// Token: 0x040005EF RID: 1519
	private Transform mTrans;

	// Token: 0x040005F0 RID: 1520
	private UITable mTable;
}
