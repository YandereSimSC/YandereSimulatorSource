using System;
using UnityEngine;

// Token: 0x02000049 RID: 73
[AddComponentMenu("NGUI/Interaction/Button Rotation")]
public class UIButtonRotation : MonoBehaviour
{
	// Token: 0x06000196 RID: 406 RVA: 0x00014E9D File Offset: 0x0001309D
	private void Start()
	{
		if (!this.mStarted)
		{
			this.mStarted = true;
			if (this.tweenTarget == null)
			{
				this.tweenTarget = base.transform;
			}
			this.mRot = this.tweenTarget.localRotation;
		}
	}

	// Token: 0x06000197 RID: 407 RVA: 0x00014ED9 File Offset: 0x000130D9
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x06000198 RID: 408 RVA: 0x00014EF4 File Offset: 0x000130F4
	private void OnDisable()
	{
		if (this.mStarted && this.tweenTarget != null)
		{
			TweenRotation component = this.tweenTarget.GetComponent<TweenRotation>();
			if (component != null)
			{
				component.value = this.mRot;
				component.enabled = false;
			}
		}
	}

	// Token: 0x06000199 RID: 409 RVA: 0x00014F40 File Offset: 0x00013140
	private void OnPress(bool isPressed)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenRotation.Begin(this.tweenTarget.gameObject, this.duration, isPressed ? (this.mRot * Quaternion.Euler(this.pressed)) : (UICamera.IsHighlighted(base.gameObject) ? (this.mRot * Quaternion.Euler(this.hover)) : this.mRot)).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x0600019A RID: 410 RVA: 0x00014FC8 File Offset: 0x000131C8
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenRotation.Begin(this.tweenTarget.gameObject, this.duration, isOver ? (this.mRot * Quaternion.Euler(this.hover)) : this.mRot).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x0600019B RID: 411 RVA: 0x00015028 File Offset: 0x00013228
	private void OnSelect(bool isSelected)
	{
		if (base.enabled && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
		{
			this.OnHover(isSelected);
		}
	}

	// Token: 0x04000321 RID: 801
	public Transform tweenTarget;

	// Token: 0x04000322 RID: 802
	public Vector3 hover = Vector3.zero;

	// Token: 0x04000323 RID: 803
	public Vector3 pressed = Vector3.zero;

	// Token: 0x04000324 RID: 804
	public float duration = 0.2f;

	// Token: 0x04000325 RID: 805
	private Quaternion mRot;

	// Token: 0x04000326 RID: 806
	private bool mStarted;
}
