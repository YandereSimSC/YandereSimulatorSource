using System;
using UnityEngine;

// Token: 0x0200005C RID: 92
[AddComponentMenu("NGUI/Interaction/Play Sound")]
public class UIPlaySound : MonoBehaviour
{
	// Token: 0x1700002C RID: 44
	// (get) Token: 0x06000262 RID: 610 RVA: 0x00019514 File Offset: 0x00017714
	private bool canPlay
	{
		get
		{
			if (!base.enabled)
			{
				return false;
			}
			UIButton component = base.GetComponent<UIButton>();
			return component == null || component.isEnabled;
		}
	}

	// Token: 0x06000263 RID: 611 RVA: 0x00019543 File Offset: 0x00017743
	private void OnEnable()
	{
		if (this.trigger == UIPlaySound.Trigger.OnEnable)
		{
			NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x06000264 RID: 612 RVA: 0x00019566 File Offset: 0x00017766
	private void OnDisable()
	{
		if (this.trigger == UIPlaySound.Trigger.OnDisable)
		{
			NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x06000265 RID: 613 RVA: 0x0001958C File Offset: 0x0001778C
	private void OnHover(bool isOver)
	{
		if (this.trigger == UIPlaySound.Trigger.OnMouseOver)
		{
			if (this.mIsOver == isOver)
			{
				return;
			}
			this.mIsOver = isOver;
		}
		if (this.canPlay && ((isOver && this.trigger == UIPlaySound.Trigger.OnMouseOver) || (!isOver && this.trigger == UIPlaySound.Trigger.OnMouseOut)))
		{
			NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x06000266 RID: 614 RVA: 0x000195EC File Offset: 0x000177EC
	private void OnPress(bool isPressed)
	{
		if (this.trigger == UIPlaySound.Trigger.OnPress)
		{
			if (this.mIsOver == isPressed)
			{
				return;
			}
			this.mIsOver = isPressed;
		}
		if (this.canPlay && ((isPressed && this.trigger == UIPlaySound.Trigger.OnPress) || (!isPressed && this.trigger == UIPlaySound.Trigger.OnRelease)))
		{
			NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x06000267 RID: 615 RVA: 0x0001964B File Offset: 0x0001784B
	private void OnClick()
	{
		if (this.canPlay && this.trigger == UIPlaySound.Trigger.OnClick)
		{
			NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x06000268 RID: 616 RVA: 0x00019675 File Offset: 0x00017875
	private void OnSelect(bool isSelected)
	{
		if (this.canPlay && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
		{
			this.OnHover(isSelected);
		}
	}

	// Token: 0x06000269 RID: 617 RVA: 0x00019691 File Offset: 0x00017891
	public void Play()
	{
		NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
	}

	// Token: 0x040003CD RID: 973
	public AudioClip audioClip;

	// Token: 0x040003CE RID: 974
	public UIPlaySound.Trigger trigger;

	// Token: 0x040003CF RID: 975
	[Range(0f, 1f)]
	public float volume = 1f;

	// Token: 0x040003D0 RID: 976
	[Range(0f, 2f)]
	public float pitch = 1f;

	// Token: 0x040003D1 RID: 977
	private bool mIsOver;

	// Token: 0x0200061B RID: 1563
	[DoNotObfuscateNGUI]
	public enum Trigger
	{
		// Token: 0x040044AD RID: 17581
		OnClick,
		// Token: 0x040044AE RID: 17582
		OnMouseOver,
		// Token: 0x040044AF RID: 17583
		OnMouseOut,
		// Token: 0x040044B0 RID: 17584
		OnPress,
		// Token: 0x040044B1 RID: 17585
		OnRelease,
		// Token: 0x040044B2 RID: 17586
		Custom,
		// Token: 0x040044B3 RID: 17587
		OnEnable,
		// Token: 0x040044B4 RID: 17588
		OnDisable
	}
}
