using System;
using UnityEngine;

// Token: 0x0200008B RID: 139
[RequireComponent(typeof(Camera))]
[AddComponentMenu("NGUI/Tween/Tween Field of View")]
public class TweenFOV : UITweener
{
	// Token: 0x170000C9 RID: 201
	// (get) Token: 0x060005BF RID: 1471 RVA: 0x00034907 File Offset: 0x00032B07
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

	// Token: 0x170000CA RID: 202
	// (get) Token: 0x060005C0 RID: 1472 RVA: 0x00034929 File Offset: 0x00032B29
	// (set) Token: 0x060005C1 RID: 1473 RVA: 0x00034931 File Offset: 0x00032B31
	[Obsolete("Use 'value' instead")]
	public float fov
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

	// Token: 0x170000CB RID: 203
	// (get) Token: 0x060005C2 RID: 1474 RVA: 0x0003493A File Offset: 0x00032B3A
	// (set) Token: 0x060005C3 RID: 1475 RVA: 0x00034947 File Offset: 0x00032B47
	public float value
	{
		get
		{
			return this.cachedCamera.fieldOfView;
		}
		set
		{
			this.cachedCamera.fieldOfView = value;
		}
	}

	// Token: 0x060005C4 RID: 1476 RVA: 0x00034955 File Offset: 0x00032B55
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = this.from * (1f - factor) + this.to * factor;
	}

	// Token: 0x060005C5 RID: 1477 RVA: 0x00034974 File Offset: 0x00032B74
	public static TweenFOV Begin(GameObject go, float duration, float to)
	{
		TweenFOV tweenFOV = UITweener.Begin<TweenFOV>(go, duration, 0f);
		tweenFOV.from = tweenFOV.value;
		tweenFOV.to = to;
		if (duration <= 0f)
		{
			tweenFOV.Sample(1f, true);
			tweenFOV.enabled = false;
		}
		return tweenFOV;
	}

	// Token: 0x060005C6 RID: 1478 RVA: 0x000349BD File Offset: 0x00032BBD
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x060005C7 RID: 1479 RVA: 0x000349CB File Offset: 0x00032BCB
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x060005C8 RID: 1480 RVA: 0x000349D9 File Offset: 0x00032BD9
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x060005C9 RID: 1481 RVA: 0x000349E7 File Offset: 0x00032BE7
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x040005CB RID: 1483
	public float from = 45f;

	// Token: 0x040005CC RID: 1484
	public float to = 45f;

	// Token: 0x040005CD RID: 1485
	private Camera mCam;
}
