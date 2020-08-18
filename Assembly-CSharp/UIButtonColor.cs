using System;
using UnityEngine;

// Token: 0x02000045 RID: 69
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Button Color")]
public class UIButtonColor : UIWidgetContainer
{
	// Token: 0x1700001F RID: 31
	// (get) Token: 0x0600016E RID: 366 RVA: 0x00014495 File Offset: 0x00012695
	// (set) Token: 0x0600016F RID: 367 RVA: 0x0001449D File Offset: 0x0001269D
	public UIButtonColor.State state
	{
		get
		{
			return this.mState;
		}
		set
		{
			this.SetState(value, false);
		}
	}

	// Token: 0x17000020 RID: 32
	// (get) Token: 0x06000170 RID: 368 RVA: 0x000144A7 File Offset: 0x000126A7
	// (set) Token: 0x06000171 RID: 369 RVA: 0x000144C0 File Offset: 0x000126C0
	public Color defaultColor
	{
		get
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			return this.mDefaultColor;
		}
		set
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			this.mDefaultColor = value;
			UIButtonColor.State state = this.mState;
			this.mState = UIButtonColor.State.Disabled;
			this.SetState(state, false);
		}
	}

	// Token: 0x17000021 RID: 33
	// (get) Token: 0x06000172 RID: 370 RVA: 0x000144F8 File Offset: 0x000126F8
	// (set) Token: 0x06000173 RID: 371 RVA: 0x00014500 File Offset: 0x00012700
	public virtual bool isEnabled
	{
		get
		{
			return base.enabled;
		}
		set
		{
			base.enabled = value;
		}
	}

	// Token: 0x06000174 RID: 372 RVA: 0x00014509 File Offset: 0x00012709
	public void ResetDefaultColor()
	{
		this.defaultColor = this.mStartingColor;
	}

	// Token: 0x06000175 RID: 373 RVA: 0x00014517 File Offset: 0x00012717
	public void CacheDefaultColor()
	{
		if (!this.mInitDone)
		{
			this.OnInit();
		}
	}

	// Token: 0x06000176 RID: 374 RVA: 0x00014527 File Offset: 0x00012727
	private void Start()
	{
		if (!this.mInitDone)
		{
			this.OnInit();
		}
		if (!this.isEnabled)
		{
			this.SetState(UIButtonColor.State.Disabled, true);
		}
	}

	// Token: 0x06000177 RID: 375 RVA: 0x00014548 File Offset: 0x00012748
	protected virtual void OnInit()
	{
		this.mInitDone = true;
		if (this.tweenTarget == null && !Application.isPlaying)
		{
			this.tweenTarget = base.gameObject;
		}
		if (this.tweenTarget != null)
		{
			this.mWidget = this.tweenTarget.GetComponent<UIWidget>();
		}
		if (this.mWidget != null)
		{
			this.mDefaultColor = this.mWidget.color;
			this.mStartingColor = this.mDefaultColor;
			return;
		}
		if (this.tweenTarget != null)
		{
			Renderer component = this.tweenTarget.GetComponent<Renderer>();
			if (component != null)
			{
				this.mDefaultColor = (Application.isPlaying ? component.material.color : component.sharedMaterial.color);
				this.mStartingColor = this.mDefaultColor;
				return;
			}
			Light component2 = this.tweenTarget.GetComponent<Light>();
			if (component2 != null)
			{
				this.mDefaultColor = component2.color;
				this.mStartingColor = this.mDefaultColor;
				return;
			}
			this.tweenTarget = null;
			this.mInitDone = false;
		}
	}

	// Token: 0x06000178 RID: 376 RVA: 0x0001465C File Offset: 0x0001285C
	protected virtual void OnEnable()
	{
		if (this.mInitDone)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
		if (UICamera.currentTouch != null)
		{
			if (UICamera.currentTouch.pressed == base.gameObject)
			{
				this.OnPress(true);
				return;
			}
			if (UICamera.currentTouch.current == base.gameObject)
			{
				this.OnHover(true);
			}
		}
	}

	// Token: 0x06000179 RID: 377 RVA: 0x000146C8 File Offset: 0x000128C8
	protected virtual void OnDisable()
	{
		if (this.mInitDone && this.mState != UIButtonColor.State.Normal)
		{
			this.SetState(UIButtonColor.State.Normal, true);
			if (this.tweenTarget != null)
			{
				TweenColor component = this.tweenTarget.GetComponent<TweenColor>();
				if (component != null)
				{
					component.value = this.mDefaultColor;
					component.enabled = false;
				}
			}
		}
	}

	// Token: 0x0600017A RID: 378 RVA: 0x00014723 File Offset: 0x00012923
	protected virtual void OnHover(bool isOver)
	{
		if (this.isEnabled)
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			if (this.tweenTarget != null)
			{
				this.SetState(isOver ? UIButtonColor.State.Hover : UIButtonColor.State.Normal, false);
			}
		}
	}

	// Token: 0x0600017B RID: 379 RVA: 0x00014758 File Offset: 0x00012958
	protected virtual void OnPress(bool isPressed)
	{
		if (this.isEnabled)
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			if (this.tweenTarget != null)
			{
				if (isPressed)
				{
					this.SetState(UIButtonColor.State.Pressed, false);
					return;
				}
				if (UICamera.currentTouch != null && UICamera.currentTouch.current == base.gameObject)
				{
					if (UICamera.currentScheme == UICamera.ControlScheme.Controller)
					{
						this.SetState(UIButtonColor.State.Hover, false);
						return;
					}
					if (UICamera.currentScheme == UICamera.ControlScheme.Mouse && UICamera.hoveredObject == base.gameObject)
					{
						this.SetState(UIButtonColor.State.Hover, false);
						return;
					}
					this.SetState(UIButtonColor.State.Normal, false);
					return;
				}
				else
				{
					this.SetState(UIButtonColor.State.Normal, false);
				}
			}
		}
	}

	// Token: 0x0600017C RID: 380 RVA: 0x000147FA File Offset: 0x000129FA
	protected virtual void OnDragOver()
	{
		if (this.isEnabled)
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			if (this.tweenTarget != null)
			{
				this.SetState(UIButtonColor.State.Pressed, false);
			}
		}
	}

	// Token: 0x0600017D RID: 381 RVA: 0x00014828 File Offset: 0x00012A28
	protected virtual void OnDragOut()
	{
		if (this.isEnabled)
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			if (this.tweenTarget != null)
			{
				this.SetState(UIButtonColor.State.Normal, false);
			}
		}
	}

	// Token: 0x0600017E RID: 382 RVA: 0x00014856 File Offset: 0x00012A56
	public virtual void SetState(UIButtonColor.State state, bool instant)
	{
		if (!this.mInitDone)
		{
			this.mInitDone = true;
			this.OnInit();
		}
		if (this.mState != state)
		{
			this.mState = state;
			this.UpdateColor(instant);
		}
	}

	// Token: 0x0600017F RID: 383 RVA: 0x00014884 File Offset: 0x00012A84
	public void UpdateColor(bool instant)
	{
		if (!this.mInitDone)
		{
			return;
		}
		if (this.tweenTarget != null)
		{
			TweenColor tweenColor;
			switch (this.mState)
			{
			case UIButtonColor.State.Hover:
				tweenColor = TweenColor.Begin(this.tweenTarget, this.duration, this.hover);
				break;
			case UIButtonColor.State.Pressed:
				tweenColor = TweenColor.Begin(this.tweenTarget, this.duration, this.pressed);
				break;
			case UIButtonColor.State.Disabled:
				tweenColor = TweenColor.Begin(this.tweenTarget, this.duration, this.disabledColor);
				break;
			default:
				tweenColor = TweenColor.Begin(this.tweenTarget, this.duration, this.mDefaultColor);
				break;
			}
			if (instant && tweenColor != null)
			{
				tweenColor.value = tweenColor.to;
				tweenColor.enabled = false;
			}
		}
	}

	// Token: 0x04000306 RID: 774
	public GameObject tweenTarget;

	// Token: 0x04000307 RID: 775
	public Color hover = new Color(0.88235295f, 0.78431374f, 0.5882353f, 1f);

	// Token: 0x04000308 RID: 776
	public Color pressed = new Color(0.7176471f, 0.6392157f, 0.48235294f, 1f);

	// Token: 0x04000309 RID: 777
	public Color disabledColor = Color.grey;

	// Token: 0x0400030A RID: 778
	public float duration = 0.2f;

	// Token: 0x0400030B RID: 779
	[NonSerialized]
	protected Color mStartingColor;

	// Token: 0x0400030C RID: 780
	[NonSerialized]
	protected Color mDefaultColor;

	// Token: 0x0400030D RID: 781
	[NonSerialized]
	protected bool mInitDone;

	// Token: 0x0400030E RID: 782
	[NonSerialized]
	protected UIWidget mWidget;

	// Token: 0x0400030F RID: 783
	[NonSerialized]
	protected UIButtonColor.State mState;

	// Token: 0x02000610 RID: 1552
	[DoNotObfuscateNGUI]
	public enum State
	{
		// Token: 0x0400447F RID: 17535
		Normal,
		// Token: 0x04004480 RID: 17536
		Hover,
		// Token: 0x04004481 RID: 17537
		Pressed,
		// Token: 0x04004482 RID: 17538
		Disabled
	}
}
