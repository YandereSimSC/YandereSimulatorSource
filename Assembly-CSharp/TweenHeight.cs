using System;
using UnityEngine;

// Token: 0x0200008D RID: 141
[RequireComponent(typeof(UIWidget))]
[AddComponentMenu("NGUI/Tween/Tween Height")]
public class TweenHeight : UITweener
{
	// Token: 0x170000CD RID: 205
	// (get) Token: 0x060005D3 RID: 1491 RVA: 0x00034B1F File Offset: 0x00032D1F
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

	// Token: 0x170000CE RID: 206
	// (get) Token: 0x060005D4 RID: 1492 RVA: 0x00034B41 File Offset: 0x00032D41
	// (set) Token: 0x060005D5 RID: 1493 RVA: 0x00034B49 File Offset: 0x00032D49
	[Obsolete("Use 'value' instead")]
	public int height
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

	// Token: 0x170000CF RID: 207
	// (get) Token: 0x060005D6 RID: 1494 RVA: 0x00034B52 File Offset: 0x00032D52
	// (set) Token: 0x060005D7 RID: 1495 RVA: 0x00034B5F File Offset: 0x00032D5F
	public int value
	{
		get
		{
			return this.cachedWidget.height;
		}
		set
		{
			this.cachedWidget.height = value;
		}
	}

	// Token: 0x060005D8 RID: 1496 RVA: 0x00034B70 File Offset: 0x00032D70
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

	// Token: 0x060005D9 RID: 1497 RVA: 0x00034C28 File Offset: 0x00032E28
	public static TweenHeight Begin(UIWidget widget, float duration, int height)
	{
		TweenHeight tweenHeight = UITweener.Begin<TweenHeight>(widget.gameObject, duration, 0f);
		tweenHeight.from = widget.height;
		tweenHeight.to = height;
		if (duration <= 0f)
		{
			tweenHeight.Sample(1f, true);
			tweenHeight.enabled = false;
		}
		return tweenHeight;
	}

	// Token: 0x060005DA RID: 1498 RVA: 0x00034C76 File Offset: 0x00032E76
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x060005DB RID: 1499 RVA: 0x00034C84 File Offset: 0x00032E84
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x060005DC RID: 1500 RVA: 0x00034C92 File Offset: 0x00032E92
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x060005DD RID: 1501 RVA: 0x00034CA0 File Offset: 0x00032EA0
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x040005D2 RID: 1490
	public int from = 100;

	// Token: 0x040005D3 RID: 1491
	public int to = 100;

	// Token: 0x040005D4 RID: 1492
	[Tooltip("If set, 'from' value will be set to match the specified rectangle")]
	public UIWidget fromTarget;

	// Token: 0x040005D5 RID: 1493
	[Tooltip("If set, 'to' value will be set to match the specified rectangle")]
	public UIWidget toTarget;

	// Token: 0x040005D6 RID: 1494
	[Tooltip("Whether there is a table that should be updated")]
	public bool updateTable;

	// Token: 0x040005D7 RID: 1495
	private UIWidget mWidget;

	// Token: 0x040005D8 RID: 1496
	private UITable mTable;
}
