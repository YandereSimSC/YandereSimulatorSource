using System;
using UnityEngine;

// Token: 0x0200008C RID: 140
[RequireComponent(typeof(UIBasicSprite))]
[AddComponentMenu("NGUI/Tween/Tween Fill")]
public class TweenFill : UITweener
{
	// Token: 0x060005CB RID: 1483 RVA: 0x00034A13 File Offset: 0x00032C13
	private void Cache()
	{
		this.mCached = true;
		this.mSprite = base.GetComponent<UISprite>();
	}

	// Token: 0x170000CC RID: 204
	// (get) Token: 0x060005CC RID: 1484 RVA: 0x00034A28 File Offset: 0x00032C28
	// (set) Token: 0x060005CD RID: 1485 RVA: 0x00034A57 File Offset: 0x00032C57
	public float value
	{
		get
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			if (this.mSprite != null)
			{
				return this.mSprite.fillAmount;
			}
			return 0f;
		}
		set
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			if (this.mSprite != null)
			{
				this.mSprite.fillAmount = value;
			}
		}
	}

	// Token: 0x060005CE RID: 1486 RVA: 0x00034A81 File Offset: 0x00032C81
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = Mathf.Lerp(this.from, this.to, factor);
	}

	// Token: 0x060005CF RID: 1487 RVA: 0x00034A9C File Offset: 0x00032C9C
	public static TweenFill Begin(GameObject go, float duration, float fill)
	{
		TweenFill tweenFill = UITweener.Begin<TweenFill>(go, duration, 0f);
		tweenFill.from = tweenFill.value;
		tweenFill.to = fill;
		if (duration <= 0f)
		{
			tweenFill.Sample(1f, true);
			tweenFill.enabled = false;
		}
		return tweenFill;
	}

	// Token: 0x060005D0 RID: 1488 RVA: 0x00034AE5 File Offset: 0x00032CE5
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x060005D1 RID: 1489 RVA: 0x00034AF3 File Offset: 0x00032CF3
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x040005CE RID: 1486
	[Range(0f, 1f)]
	public float from = 1f;

	// Token: 0x040005CF RID: 1487
	[Range(0f, 1f)]
	public float to = 1f;

	// Token: 0x040005D0 RID: 1488
	private bool mCached;

	// Token: 0x040005D1 RID: 1489
	private UIBasicSprite mSprite;
}
