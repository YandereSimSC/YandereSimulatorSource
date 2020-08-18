using System;
using UnityEngine;

// Token: 0x02000048 RID: 72
[AddComponentMenu("NGUI/Interaction/Button Offset")]
public class UIButtonOffset : MonoBehaviour
{
	// Token: 0x0600018D RID: 397 RVA: 0x00014C68 File Offset: 0x00012E68
	private void Start()
	{
		if (!this.mStarted)
		{
			this.mStarted = true;
			if (this.tweenTarget == null)
			{
				this.tweenTarget = base.transform;
			}
			this.mPos = this.tweenTarget.localPosition;
		}
	}

	// Token: 0x0600018E RID: 398 RVA: 0x00014CA4 File Offset: 0x00012EA4
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x0600018F RID: 399 RVA: 0x00014CC0 File Offset: 0x00012EC0
	private void OnDisable()
	{
		if (this.mStarted && this.tweenTarget != null)
		{
			TweenPosition component = this.tweenTarget.GetComponent<TweenPosition>();
			if (component != null)
			{
				component.value = this.mPos;
				component.enabled = false;
			}
		}
	}

	// Token: 0x06000190 RID: 400 RVA: 0x00014D0C File Offset: 0x00012F0C
	private void OnPress(bool isPressed)
	{
		this.mPressed = isPressed;
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenPosition.Begin(this.tweenTarget.gameObject, this.duration, isPressed ? (this.mPos + this.pressed) : (UICamera.IsHighlighted(base.gameObject) ? (this.mPos + this.hover) : this.mPos)).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x06000191 RID: 401 RVA: 0x00014D90 File Offset: 0x00012F90
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenPosition.Begin(this.tweenTarget.gameObject, this.duration, isOver ? (this.mPos + this.hover) : this.mPos).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x06000192 RID: 402 RVA: 0x00014DEB File Offset: 0x00012FEB
	private void OnDragOver()
	{
		if (this.mPressed)
		{
			TweenPosition.Begin(this.tweenTarget.gameObject, this.duration, this.mPos + this.hover).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x06000193 RID: 403 RVA: 0x00014E22 File Offset: 0x00013022
	private void OnDragOut()
	{
		if (this.mPressed)
		{
			TweenPosition.Begin(this.tweenTarget.gameObject, this.duration, this.mPos).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x06000194 RID: 404 RVA: 0x00014E4E File Offset: 0x0001304E
	private void OnSelect(bool isSelected)
	{
		if (base.enabled && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
		{
			this.OnHover(isSelected);
		}
	}

	// Token: 0x0400031A RID: 794
	public Transform tweenTarget;

	// Token: 0x0400031B RID: 795
	public Vector3 hover = Vector3.zero;

	// Token: 0x0400031C RID: 796
	public Vector3 pressed = new Vector3(2f, -2f);

	// Token: 0x0400031D RID: 797
	public float duration = 0.2f;

	// Token: 0x0400031E RID: 798
	[NonSerialized]
	private Vector3 mPos;

	// Token: 0x0400031F RID: 799
	[NonSerialized]
	private bool mStarted;

	// Token: 0x04000320 RID: 800
	[NonSerialized]
	private bool mPressed;
}
