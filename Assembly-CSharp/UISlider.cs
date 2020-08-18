using System;
using UnityEngine;

// Token: 0x02000064 RID: 100
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/NGUI Slider")]
public class UISlider : UIProgressBar
{
	// Token: 0x1700004E RID: 78
	// (get) Token: 0x06000302 RID: 770 RVA: 0x0001E2C0 File Offset: 0x0001C4C0
	public bool isColliderEnabled
	{
		get
		{
			Collider component = base.GetComponent<Collider>();
			if (component != null)
			{
				return component.enabled;
			}
			Collider2D component2 = base.GetComponent<Collider2D>();
			return component2 != null && component2.enabled;
		}
	}

	// Token: 0x1700004F RID: 79
	// (get) Token: 0x06000303 RID: 771 RVA: 0x0001C492 File Offset: 0x0001A692
	// (set) Token: 0x06000304 RID: 772 RVA: 0x0001C49A File Offset: 0x0001A69A
	[Obsolete("Use 'value' instead")]
	public float sliderValue
	{
		get
		{
			return base.value;
		}
		set
		{
			base.value = value;
		}
	}

	// Token: 0x17000050 RID: 80
	// (get) Token: 0x06000305 RID: 773 RVA: 0x0001E2FC File Offset: 0x0001C4FC
	// (set) Token: 0x06000306 RID: 774 RVA: 0x00002ACE File Offset: 0x00000CCE
	[Obsolete("Use 'fillDirection' instead")]
	public bool inverted
	{
		get
		{
			return base.isInverted;
		}
		set
		{
		}
	}

	// Token: 0x06000307 RID: 775 RVA: 0x0001E304 File Offset: 0x0001C504
	protected override void Upgrade()
	{
		if (this.direction != UISlider.Direction.Upgraded)
		{
			this.mValue = this.rawValue;
			if (this.foreground != null)
			{
				this.mFG = this.foreground.GetComponent<UIWidget>();
			}
			if (this.direction == UISlider.Direction.Horizontal)
			{
				this.mFill = (this.mInverted ? UIProgressBar.FillDirection.RightToLeft : UIProgressBar.FillDirection.LeftToRight);
			}
			else
			{
				this.mFill = (this.mInverted ? UIProgressBar.FillDirection.TopToBottom : UIProgressBar.FillDirection.BottomToTop);
			}
			this.direction = UISlider.Direction.Upgraded;
		}
	}

	// Token: 0x06000308 RID: 776 RVA: 0x0001E37C File Offset: 0x0001C57C
	protected override void OnStart()
	{
		UIEventListener uieventListener = UIEventListener.Get((this.mBG != null && (this.mBG.GetComponent<Collider>() != null || this.mBG.GetComponent<Collider2D>() != null)) ? this.mBG.gameObject : base.gameObject);
		uieventListener.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(uieventListener.onPress, new UIEventListener.BoolDelegate(this.OnPressBackground));
		uieventListener.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uieventListener.onDrag, new UIEventListener.VectorDelegate(this.OnDragBackground));
		if (this.thumb != null && (this.thumb.GetComponent<Collider>() != null || this.thumb.GetComponent<Collider2D>() != null) && (this.mFG == null || this.thumb != this.mFG.cachedTransform))
		{
			UIEventListener uieventListener2 = UIEventListener.Get(this.thumb.gameObject);
			uieventListener2.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(uieventListener2.onPress, new UIEventListener.BoolDelegate(this.OnPressForeground));
			uieventListener2.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uieventListener2.onDrag, new UIEventListener.VectorDelegate(this.OnDragForeground));
		}
	}

	// Token: 0x06000309 RID: 777 RVA: 0x0001E4C8 File Offset: 0x0001C6C8
	protected void OnPressBackground(GameObject go, bool isPressed)
	{
		if (UICamera.currentScheme == UICamera.ControlScheme.Controller)
		{
			return;
		}
		this.mCam = UICamera.currentCamera;
		base.value = base.ScreenToValue(UICamera.lastEventPosition);
		if (!isPressed && this.onDragFinished != null)
		{
			this.onDragFinished();
		}
	}

	// Token: 0x0600030A RID: 778 RVA: 0x0001E505 File Offset: 0x0001C705
	protected void OnDragBackground(GameObject go, Vector2 delta)
	{
		if (UICamera.currentScheme == UICamera.ControlScheme.Controller)
		{
			return;
		}
		this.mCam = UICamera.currentCamera;
		base.value = base.ScreenToValue(UICamera.lastEventPosition);
	}

	// Token: 0x0600030B RID: 779 RVA: 0x0001E52C File Offset: 0x0001C72C
	protected void OnPressForeground(GameObject go, bool isPressed)
	{
		if (UICamera.currentScheme == UICamera.ControlScheme.Controller)
		{
			return;
		}
		this.mCam = UICamera.currentCamera;
		if (isPressed)
		{
			this.mOffset = ((this.mFG == null) ? 0f : (base.value - base.ScreenToValue(UICamera.lastEventPosition)));
			return;
		}
		if (this.onDragFinished != null)
		{
			this.onDragFinished();
		}
	}

	// Token: 0x0600030C RID: 780 RVA: 0x0001E591 File Offset: 0x0001C791
	protected void OnDragForeground(GameObject go, Vector2 delta)
	{
		if (UICamera.currentScheme == UICamera.ControlScheme.Controller)
		{
			return;
		}
		this.mCam = UICamera.currentCamera;
		base.value = this.mOffset + base.ScreenToValue(UICamera.lastEventPosition);
	}

	// Token: 0x0600030D RID: 781 RVA: 0x0001E5BF File Offset: 0x0001C7BF
	public override void OnPan(Vector2 delta)
	{
		if (base.enabled && this.isColliderEnabled)
		{
			base.OnPan(delta);
		}
	}

	// Token: 0x04000454 RID: 1108
	[HideInInspector]
	[SerializeField]
	private Transform foreground;

	// Token: 0x04000455 RID: 1109
	[HideInInspector]
	[SerializeField]
	private float rawValue = 1f;

	// Token: 0x04000456 RID: 1110
	[HideInInspector]
	[SerializeField]
	private UISlider.Direction direction = UISlider.Direction.Upgraded;

	// Token: 0x04000457 RID: 1111
	[HideInInspector]
	[SerializeField]
	protected bool mInverted;

	// Token: 0x02000629 RID: 1577
	private enum Direction
	{
		// Token: 0x040044DF RID: 17631
		Horizontal,
		// Token: 0x040044E0 RID: 17632
		Vertical,
		// Token: 0x040044E1 RID: 17633
		Upgraded
	}
}
