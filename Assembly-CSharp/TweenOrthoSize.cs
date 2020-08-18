using System;
using UnityEngine;

// Token: 0x0200008F RID: 143
[RequireComponent(typeof(Camera))]
[AddComponentMenu("NGUI/Tween/Tween Orthographic Size")]
public class TweenOrthoSize : UITweener
{
	// Token: 0x170000D0 RID: 208
	// (get) Token: 0x060005EA RID: 1514 RVA: 0x000352BA File Offset: 0x000334BA
	public Camera cachedCamera
	{
		get
		{
			if (this.mCam == null)
			{
				this.mCam = base.GetComponent<Camera>();
			}
			return this.mCam;
		}
	}

	// Token: 0x170000D1 RID: 209
	// (get) Token: 0x060005EB RID: 1515 RVA: 0x000352DC File Offset: 0x000334DC
	// (set) Token: 0x060005EC RID: 1516 RVA: 0x000352E4 File Offset: 0x000334E4
	[Obsolete("Use 'value' instead")]
	public float orthoSize
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

	// Token: 0x170000D2 RID: 210
	// (get) Token: 0x060005ED RID: 1517 RVA: 0x000352ED File Offset: 0x000334ED
	// (set) Token: 0x060005EE RID: 1518 RVA: 0x000352FA File Offset: 0x000334FA
	public float value
	{
		get
		{
			return this.cachedCamera.orthographicSize;
		}
		set
		{
			this.cachedCamera.orthographicSize = value;
		}
	}

	// Token: 0x060005EF RID: 1519 RVA: 0x00035308 File Offset: 0x00033508
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = this.from * (1f - factor) + this.to * factor;
	}

	// Token: 0x060005F0 RID: 1520 RVA: 0x00035328 File Offset: 0x00033528
	public static TweenOrthoSize Begin(GameObject go, float duration, float to)
	{
		TweenOrthoSize tweenOrthoSize = UITweener.Begin<TweenOrthoSize>(go, duration, 0f);
		tweenOrthoSize.from = tweenOrthoSize.value;
		tweenOrthoSize.to = to;
		if (duration <= 0f)
		{
			tweenOrthoSize.Sample(1f, true);
			tweenOrthoSize.enabled = false;
		}
		return tweenOrthoSize;
	}

	// Token: 0x060005F1 RID: 1521 RVA: 0x00035371 File Offset: 0x00033571
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x060005F2 RID: 1522 RVA: 0x0003537F File Offset: 0x0003357F
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x040005E0 RID: 1504
	public float from = 1f;

	// Token: 0x040005E1 RID: 1505
	public float to = 1f;

	// Token: 0x040005E2 RID: 1506
	private Camera mCam;
}
