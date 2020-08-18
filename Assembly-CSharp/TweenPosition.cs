using System;
using UnityEngine;

// Token: 0x02000090 RID: 144
[AddComponentMenu("NGUI/Tween/Tween Position")]
public class TweenPosition : UITweener
{
	// Token: 0x170000D3 RID: 211
	// (get) Token: 0x060005F4 RID: 1524 RVA: 0x000353AB File Offset: 0x000335AB
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

	// Token: 0x170000D4 RID: 212
	// (get) Token: 0x060005F5 RID: 1525 RVA: 0x000353CD File Offset: 0x000335CD
	// (set) Token: 0x060005F6 RID: 1526 RVA: 0x000353D5 File Offset: 0x000335D5
	[Obsolete("Use 'value' instead")]
	public Vector3 position
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

	// Token: 0x170000D5 RID: 213
	// (get) Token: 0x060005F7 RID: 1527 RVA: 0x000353DE File Offset: 0x000335DE
	// (set) Token: 0x060005F8 RID: 1528 RVA: 0x00035400 File Offset: 0x00033600
	public Vector3 value
	{
		get
		{
			if (!this.worldSpace)
			{
				return this.cachedTransform.localPosition;
			}
			return this.cachedTransform.position;
		}
		set
		{
			if (!(this.mRect == null) && this.mRect.isAnchored && !this.worldSpace)
			{
				value -= this.cachedTransform.localPosition;
				NGUIMath.MoveRect(this.mRect, value.x, value.y);
				return;
			}
			if (this.worldSpace)
			{
				this.cachedTransform.position = value;
				return;
			}
			this.cachedTransform.localPosition = value;
		}
	}

	// Token: 0x060005F9 RID: 1529 RVA: 0x0003547C File Offset: 0x0003367C
	private void Awake()
	{
		this.mRect = base.GetComponent<UIRect>();
	}

	// Token: 0x060005FA RID: 1530 RVA: 0x0003548A File Offset: 0x0003368A
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = this.from * (1f - factor) + this.to * factor;
	}

	// Token: 0x060005FB RID: 1531 RVA: 0x000354B8 File Offset: 0x000336B8
	public static TweenPosition Begin(GameObject go, float duration, Vector3 pos)
	{
		TweenPosition tweenPosition = UITweener.Begin<TweenPosition>(go, duration, 0f);
		tweenPosition.from = tweenPosition.value;
		tweenPosition.to = pos;
		if (duration <= 0f)
		{
			tweenPosition.Sample(1f, true);
			tweenPosition.enabled = false;
		}
		return tweenPosition;
	}

	// Token: 0x060005FC RID: 1532 RVA: 0x00035504 File Offset: 0x00033704
	public static TweenPosition Begin(GameObject go, float duration, Vector3 pos, bool worldSpace)
	{
		TweenPosition tweenPosition = UITweener.Begin<TweenPosition>(go, duration, 0f);
		tweenPosition.worldSpace = worldSpace;
		tweenPosition.from = tweenPosition.value;
		tweenPosition.to = pos;
		if (duration <= 0f)
		{
			tweenPosition.Sample(1f, true);
			tweenPosition.enabled = false;
		}
		return tweenPosition;
	}

	// Token: 0x060005FD RID: 1533 RVA: 0x00035554 File Offset: 0x00033754
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x060005FE RID: 1534 RVA: 0x00035562 File Offset: 0x00033762
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x060005FF RID: 1535 RVA: 0x00035570 File Offset: 0x00033770
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x06000600 RID: 1536 RVA: 0x0003557E File Offset: 0x0003377E
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x040005E3 RID: 1507
	public Vector3 from;

	// Token: 0x040005E4 RID: 1508
	public Vector3 to;

	// Token: 0x040005E5 RID: 1509
	[HideInInspector]
	public bool worldSpace;

	// Token: 0x040005E6 RID: 1510
	private Transform mTrans;

	// Token: 0x040005E7 RID: 1511
	private UIRect mRect;
}
