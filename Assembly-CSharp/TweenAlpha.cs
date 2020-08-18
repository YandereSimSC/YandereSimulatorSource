using System;
using UnityEngine;

// Token: 0x02000089 RID: 137
[AddComponentMenu("NGUI/Tween/Tween Alpha")]
public class TweenAlpha : UITweener
{
	// Token: 0x170000C5 RID: 197
	// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0003433B File Offset: 0x0003253B
	// (set) Token: 0x060005A9 RID: 1449 RVA: 0x00034343 File Offset: 0x00032543
	[Obsolete("Use 'value' instead")]
	public float alpha
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

	// Token: 0x060005AA RID: 1450 RVA: 0x0003434C File Offset: 0x0003254C
	private void OnDestroy()
	{
		if (this.autoCleanup && this.mMat != null && this.mShared != this.mMat)
		{
			UnityEngine.Object.Destroy(this.mMat);
			this.mMat = null;
		}
	}

	// Token: 0x060005AB RID: 1451 RVA: 0x0003438C File Offset: 0x0003258C
	private void Cache()
	{
		this.mCached = true;
		this.mRect = base.GetComponent<UIRect>();
		this.mSr = base.GetComponent<SpriteRenderer>();
		if (this.mRect == null && this.mSr == null)
		{
			this.mLight = base.GetComponent<Light>();
			if (this.mLight == null)
			{
				Renderer component = base.GetComponent<Renderer>();
				if (component != null)
				{
					this.mShared = component.sharedMaterial;
					this.mMat = component.material;
				}
				if (this.mMat == null)
				{
					this.mRect = base.GetComponentInChildren<UIRect>();
					return;
				}
			}
			else
			{
				this.mBaseIntensity = this.mLight.intensity;
			}
		}
	}

	// Token: 0x170000C6 RID: 198
	// (get) Token: 0x060005AC RID: 1452 RVA: 0x00034444 File Offset: 0x00032644
	// (set) Token: 0x060005AD RID: 1453 RVA: 0x000344E0 File Offset: 0x000326E0
	public float value
	{
		get
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			if (this.mRect != null)
			{
				return this.mRect.alpha;
			}
			if (this.mSr != null)
			{
				return this.mSr.color.a;
			}
			if (this.mMat == null)
			{
				return 1f;
			}
			if (string.IsNullOrEmpty(this.colorProperty))
			{
				return this.mMat.color.a;
			}
			return this.mMat.GetColor(this.colorProperty).a;
		}
		set
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			if (this.mRect != null)
			{
				this.mRect.alpha = value;
				return;
			}
			if (this.mSr != null)
			{
				Color color = this.mSr.color;
				color.a = value;
				this.mSr.color = color;
				return;
			}
			if (!(this.mMat != null))
			{
				if (this.mLight != null)
				{
					this.mLight.intensity = this.mBaseIntensity * value;
				}
				return;
			}
			if (string.IsNullOrEmpty(this.colorProperty))
			{
				Color color2 = this.mMat.color;
				color2.a = value;
				this.mMat.color = color2;
				return;
			}
			Color color3 = this.mMat.GetColor(this.colorProperty);
			color3.a = value;
			this.mMat.SetColor(this.colorProperty, color3);
		}
	}

	// Token: 0x060005AE RID: 1454 RVA: 0x000345CF File Offset: 0x000327CF
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = Mathf.Lerp(this.from, this.to, factor);
	}

	// Token: 0x060005AF RID: 1455 RVA: 0x000345EC File Offset: 0x000327EC
	public static TweenAlpha Begin(GameObject go, float duration, float alpha, float delay = 0f)
	{
		TweenAlpha tweenAlpha = UITweener.Begin<TweenAlpha>(go, duration, delay);
		tweenAlpha.from = tweenAlpha.value;
		tweenAlpha.to = alpha;
		if (duration <= 0f)
		{
			tweenAlpha.Sample(1f, true);
			tweenAlpha.enabled = false;
		}
		return tweenAlpha;
	}

	// Token: 0x060005B0 RID: 1456 RVA: 0x00034631 File Offset: 0x00032831
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x060005B1 RID: 1457 RVA: 0x0003463F File Offset: 0x0003283F
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x040005B9 RID: 1465
	[Range(0f, 1f)]
	public float from = 1f;

	// Token: 0x040005BA RID: 1466
	[Range(0f, 1f)]
	public float to = 1f;

	// Token: 0x040005BB RID: 1467
	[Tooltip("If used on a renderer, the material should probably be cleaned up after this script gets destroyed...")]
	public bool autoCleanup;

	// Token: 0x040005BC RID: 1468
	[Tooltip("Color to adjust")]
	public string colorProperty;

	// Token: 0x040005BD RID: 1469
	[NonSerialized]
	private bool mCached;

	// Token: 0x040005BE RID: 1470
	[NonSerialized]
	private UIRect mRect;

	// Token: 0x040005BF RID: 1471
	[NonSerialized]
	private Material mShared;

	// Token: 0x040005C0 RID: 1472
	[NonSerialized]
	private Material mMat;

	// Token: 0x040005C1 RID: 1473
	[NonSerialized]
	private Light mLight;

	// Token: 0x040005C2 RID: 1474
	[NonSerialized]
	private SpriteRenderer mSr;

	// Token: 0x040005C3 RID: 1475
	[NonSerialized]
	private float mBaseIntensity = 1f;
}
