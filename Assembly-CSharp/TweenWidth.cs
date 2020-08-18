using System;
using UnityEngine;

// Token: 0x02000095 RID: 149
[RequireComponent(typeof(UIWidget))]
[AddComponentMenu("NGUI/Tween/Tween Width")]
public class TweenWidth : UITweener
{
	// Token: 0x170000DF RID: 223
	// (get) Token: 0x06000628 RID: 1576 RVA: 0x00035C32 File Offset: 0x00033E32
	public UIWidget cachedWidget
	{
		get
		{
			if (this.mWidget == null)
			{
				this.mWidget = base.GetComponent<UIWidget>();
			}
			return this.mWidget;
		}
	}

	// Token: 0x170000E0 RID: 224
	// (get) Token: 0x06000629 RID: 1577 RVA: 0x00035C54 File Offset: 0x00033E54
	// (set) Token: 0x0600062A RID: 1578 RVA: 0x00035C5C File Offset: 0x00033E5C
	[Obsolete("Use 'value' instead")]
	public int width
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

	// Token: 0x170000E1 RID: 225
	// (get) Token: 0x0600062B RID: 1579 RVA: 0x00035C65 File Offset: 0x00033E65
	// (set) Token: 0x0600062C RID: 1580 RVA: 0x00035C72 File Offset: 0x00033E72
	public int value
	{
		get
		{
			return this.cachedWidget.width;
		}
		set
		{
			this.cachedWidget.width = value;
		}
	}

	// Token: 0x0600062D RID: 1581 RVA: 0x00035C80 File Offset: 0x00033E80
	protected override void OnUpdate(float factor, bool isFinished)
	{
		if (this.fromTarget)
		{
			this.from = this.fromTarget.width;
		}
		if (this.toTarget)
		{
			this.to = this.toTarget.width;
		}
		this.value = Mathf.RoundToInt((float)this.from * (1f - factor) + (float)this.to * factor);
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

	// Token: 0x0600062E RID: 1582 RVA: 0x00035D38 File Offset: 0x00033F38
	public static TweenWidth Begin(UIWidget widget, float duration, int width)
	{
		TweenWidth tweenWidth = UITweener.Begin<TweenWidth>(widget.gameObject, duration, 0f);
		tweenWidth.from = widget.width;
		tweenWidth.to = width;
		if (duration <= 0f)
		{
			tweenWidth.Sample(1f, true);
			tweenWidth.enabled = false;
		}
		return tweenWidth;
	}

	// Token: 0x0600062F RID: 1583 RVA: 0x00035D86 File Offset: 0x00033F86
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x06000630 RID: 1584 RVA: 0x00035D94 File Offset: 0x00033F94
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x06000631 RID: 1585 RVA: 0x00035DA2 File Offset: 0x00033FA2
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x06000632 RID: 1586 RVA: 0x00035DB0 File Offset: 0x00033FB0
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x040005FB RID: 1531
	public int from = 100;

	// Token: 0x040005FC RID: 1532
	public int to = 100;

	// Token: 0x040005FD RID: 1533
	[Tooltip("If set, 'from' value will be set to match the specified rectangle")]
	public UIWidget fromTarget;

	// Token: 0x040005FE RID: 1534
	[Tooltip("If set, 'to' value will be set to match the specified rectangle")]
	public UIWidget toTarget;

	// Token: 0x040005FF RID: 1535
	[Tooltip("Whether there is a table that should be updated")]
	public bool updateTable;

	// Token: 0x04000600 RID: 1536
	private UIWidget mWidget;

	// Token: 0x04000601 RID: 1537
	private UITable mTable;
}
