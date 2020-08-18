using System;
using UnityEngine;

// Token: 0x0200004A RID: 74
[AddComponentMenu("NGUI/Interaction/Button Scale")]
public class UIButtonScale : MonoBehaviour
{
	// Token: 0x0600019D RID: 413 RVA: 0x0001506D File Offset: 0x0001326D
	private void Start()
	{
		if (!this.mStarted)
		{
			this.mStarted = true;
			if (this.tweenTarget == null)
			{
				this.tweenTarget = base.transform;
			}
			this.mScale = this.tweenTarget.localScale;
		}
	}

	// Token: 0x0600019E RID: 414 RVA: 0x000150A9 File Offset: 0x000132A9
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x0600019F RID: 415 RVA: 0x000150C4 File Offset: 0x000132C4
	private void OnDisable()
	{
		if (this.mStarted && this.tweenTarget != null)
		{
			TweenScale component = this.tweenTarget.GetComponent<TweenScale>();
			if (component != null)
			{
				component.value = this.mScale;
				component.enabled = false;
			}
		}
	}

	// Token: 0x060001A0 RID: 416 RVA: 0x00015110 File Offset: 0x00013310
	private void OnPress(bool isPressed)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenScale.Begin(this.tweenTarget.gameObject, this.duration, isPressed ? Vector3.Scale(this.mScale, this.pressed) : (UICamera.IsHighlighted(base.gameObject) ? Vector3.Scale(this.mScale, this.hover) : this.mScale)).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x0001518C File Offset: 0x0001338C
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenScale.Begin(this.tweenTarget.gameObject, this.duration, isOver ? Vector3.Scale(this.mScale, this.hover) : this.mScale).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x000151E7 File Offset: 0x000133E7
	private void OnSelect(bool isSelected)
	{
		if (base.enabled && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
		{
			this.OnHover(isSelected);
		}
	}

	// Token: 0x04000327 RID: 807
	public Transform tweenTarget;

	// Token: 0x04000328 RID: 808
	public Vector3 hover = new Vector3(1.1f, 1.1f, 1.1f);

	// Token: 0x04000329 RID: 809
	public Vector3 pressed = new Vector3(1.05f, 1.05f, 1.05f);

	// Token: 0x0400032A RID: 810
	public float duration = 0.2f;

	// Token: 0x0400032B RID: 811
	private Vector3 mScale;

	// Token: 0x0400032C RID: 812
	private bool mStarted;
}
